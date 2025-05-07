using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace FX5U_IOMonitor.Resources
{
    public partial class Element_Settings : Form
    {
        private string? viewAddress;
        private string? tableName;
        private AddressInputMode currentInputMode = AddressInputMode.Hexadecimal;
        private ElementMode currentMode;
        public Action? OnDataUpdated; // ✅ 刷新資料事件

        public enum ElementMode { Add, ViewOnly }
        public enum AddressInputMode { Hexadecimal, Octal }

        public Element_Settings(ElementMode mode = ElementMode.Add, string? table = null, string? address = null)
        {
            InitializeComponent();
            SetupCenteredComboBox(comb_machine);
            SetupCenteredComboBox(comb_io);
            SetupCenteredComboBox(comb_type);

            currentMode = mode;
            viewAddress = address;
            tableName = table;
            // 設定 comb_machine 的 SelectedIndex
            if (string.Equals(tableName, "Drill", StringComparison.OrdinalIgnoreCase))
            {
                comb_machine.SelectedIndex = 0;
                comb_machine.Enabled = false;  // ✅ 鎖定不可選擇

            }
            else if (string.Equals(tableName, "Sawing", StringComparison.OrdinalIgnoreCase))
            {
                comb_machine.SelectedIndex = 1;
                comb_machine.Enabled = false;
            }

            if (currentMode == ElementMode.ViewOnly && !string.IsNullOrEmpty(viewAddress) && !string.IsNullOrEmpty(tableName))
            {
                LoadDataFromDatabase(viewAddress, tableName);
               
            }

            if (currentMode == ElementMode.Add && !string.IsNullOrEmpty(tableName))
            {
                if (string.Equals(tableName, "Drill", StringComparison.OrdinalIgnoreCase))
                {
                    comb_machine.SelectedIndex = 0;
                    comb_machine.Enabled = false;  // ✅ 鎖定不可選擇

                }
                else if (string.Equals(tableName, "Sawing", StringComparison.OrdinalIgnoreCase))
                {
                    comb_machine.SelectedIndex = 1;
                    comb_machine.Enabled = false;
                }

            }

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            bool add_OK = ValidateInputFields();
            string fullAddress = comb_io.Text.Trim() + txb_address.Text.Trim();

            if (!add_OK) return;

            using (var context = new ApplicationDB())
            {
                if (comb_machine.SelectedIndex == 0)
                {
                    if (context.Drill_IO.Any(d => d.address == fullAddress))
                    {
                        MessageBox.Show($"⚠️ 地址 '{fullAddress}' 已存在，請重新設定！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var newDrillIO = new Drill_MachineIO
                    {
                        ClassTag = comb_class.Text,
                        Comment = txb_comment.Text,
                        Description = txb_description.Text,
                        IOType = GetSelectedIOState(comb_io),
                        MaxLife = Convert.ToInt32(txb_max_number.Text),
                        MountTime = DateTime.Now,
                        RelayType = GetSelectedRelayType(comb_type),
                        Setting_red = Convert.ToInt32(txb_red_light.Text),
                        Setting_yellow = Convert.ToInt32(txb_yellow_light.Text),
                        UnmountTime = DateTime.Now.AddDays(10),
                        address = fullAddress
                    };
                    context.Drill_IO.Add(newDrillIO);
                }
                else if (comb_machine.SelectedIndex == 1)
                {
                    if (context.Sawing_IO.Any(d => d.address == fullAddress))
                    {
                        MessageBox.Show($"⚠️ 地址 '{fullAddress}' 已存在，請重新設定！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var newIO = new Sawing_MachineIO
                    {
                        ClassTag = comb_class.Text,
                        Comment = txb_comment.Text,
                        Description = txb_description.Text,
                        IOType = GetSelectedIOState(comb_io),
                        MaxLife = Convert.ToInt32(txb_max_number.Text),
                        MountTime = DateTime.Now,
                        RelayType = GetSelectedRelayType(comb_type),
                        Setting_red = Convert.ToInt32(txb_red_light.Text),
                        Setting_yellow = Convert.ToInt32(txb_yellow_light.Text),
                        UnmountTime = DateTime.Now.AddDays(10),
                        address = fullAddress
                    };
                    context.Sawing_IO.Add(newIO);
                }
                context.SaveChanges();
                MessageBox.Show("✅ 新增成功！");
                OnDataUpdated?.Invoke(); // ✅ 呼叫刷新事件
                this.Close();
            }
        }

        private void txb_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if (char.IsControl(keyChar)) return;

            switch (currentInputMode)
            {
                case AddressInputMode.Octal:
                    e.Handled = !(keyChar >= '0' && keyChar <= '7');
                    break;
                case AddressInputMode.Hexadecimal:
                    e.KeyChar = char.ToUpper(keyChar);
                    e.Handled = !((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                                  (e.KeyChar >= 'A' && e.KeyChar <= 'F'));
                    break;
            }
        }

        private void txb_max_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void Update_combobox(string datatable)
        {
            var classTags = DBfunction.GetAllClassTags(datatable);
            comb_class.Items.Clear();
            comb_class.Items.AddRange(classTags.ToArray());
            if (comb_class.Items.Count > 0) comb_class.SelectedIndex = 0;
        }

        private void comb_machine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comb_machine.SelectedIndex == 0)
            {
                Update_combobox("Drill");
                currentInputMode = AddressInputMode.Hexadecimal;
            }
            else if (comb_machine.SelectedIndex == 1)
            {
                Update_combobox("Sawing");
                currentInputMode = AddressInputMode.Octal;
            }
        }

        private bool ValidateInputFields()
        {
            if (string.IsNullOrWhiteSpace(comb_machine.Text) ||
                string.IsNullOrWhiteSpace(comb_io.Text) ||
                string.IsNullOrWhiteSpace(comb_type.Text) ||
                string.IsNullOrWhiteSpace(comb_class.Text) ||
                string.IsNullOrWhiteSpace(txb_address.Text) ||
                string.IsNullOrWhiteSpace(txb_description.Text) ||
                string.IsNullOrWhiteSpace(txb_comment.Text))
            {
                MessageBox.Show("⚠️ 請確實填寫所有欄位，不能有空白！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (Convert.ToInt32(txb_max_number.Text) <= 0 ||
                Convert.ToInt32(txb_yellow_light.Text) <= 0 ||
                Convert.ToInt32(txb_red_light.Text) <= 0)
            {
                MessageBox.Show("⚠️ 數值設定不能小於或等於0！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool GetSelectedIOState(ComboBox comb_io) => comb_io.SelectedIndex == 0;

        private RelayType GetSelectedRelayType(ComboBox comb_type) =>
            comb_type.SelectedIndex switch
            {
                0 => RelayType.Electronic,
                1 => RelayType.Machanical,
                _ => throw new InvalidOperationException("❗請選擇有效的 RelayType 項目！")
            };

        private void SetupCenteredComboBox(ComboBox comboBox)
        {
            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.DrawItem -= ComboBox_DrawItem_Custom;
            comboBox.DrawItem += ComboBox_DrawItem_Custom;
        }

        private void ComboBox_DrawItem_Custom(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            var comboBox = (ComboBox)sender;
            string text = comboBox.Items[e.Index].ToString();
            e.DrawBackground();
            e.Graphics.FillRectangle(new SolidBrush((e.State & DrawItemState.Selected) == DrawItemState.Selected ? Color.FromArgb(30, 144, 255) : Color.White), e.Bounds);
            e.Graphics.DrawString(text, e.Font, new SolidBrush((e.State & DrawItemState.Selected) == DrawItemState.Selected ? Color.White : Color.Black),
                e.Bounds.Left + (e.Bounds.Width - e.Graphics.MeasureString(text, e.Font).Width) / 2,
                e.Bounds.Top + (e.Bounds.Height - e.Graphics.MeasureString(text, e.Font).Height) / 2);
            e.DrawFocusRectangle();
        }

        private void LoadDataFromDatabase(string address, string table)
        {
            using (var context = new ApplicationDB())
            {
                if (table == "Drill")
                {
                    var data = context.Drill_IO.FirstOrDefault(d => d.address == address);
                    if (data != null)
                    {
                        comb_machine.SelectedIndex = 0;
                        comb_io.Text = data.IOType ? "X" : "Y";
                        comb_type.SelectedIndex = data.RelayType == RelayType.Electronic ? 0 : data.RelayType == RelayType.Machanical ? 1 : -1;
                        comb_class.Text = data.ClassTag;
                        txb_address.Text = address.Substring(1);
                        txb_description.Text = data.Description;
                        txb_comment.Text = data.Comment;
                        txb_max_number.Text = data.MaxLife.ToString();
                        txb_yellow_light.Text = data.Setting_yellow.ToString();
                        txb_red_light.Text = data.Setting_green.ToString();
                    }
                }
                else if (table == "Sawing")
                {
                    var data = context.Sawing_IO.FirstOrDefault(s => s.address == address);
                    if (data != null)
                    {
                        comb_machine.SelectedIndex = 1;
                        comb_io.Text = data.IOType ? "X" : "Y";
                        comb_type.SelectedIndex = data.RelayType == RelayType.Electronic ? 0 : data.RelayType == RelayType.Machanical ? 1 : -1;
                        comb_class.Text = data.ClassTag;
                        txb_address.Text = address.Substring(1);
                        txb_description.Text = data.Description;
                        txb_comment.Text = data.Comment;
                        txb_max_number.Text = data.MaxLife.ToString();
                        txb_yellow_light.Text = data.Setting_yellow.ToString();
                        txb_red_light.Text = data.Setting_green.ToString();
                    }
                }
            };
         
        }

        private void btn_update_Click(object sender, EventArgs e) 
        {
            bool add_OK = ValidateInputFields();
            if (!add_OK) return;
            string fullAddress = comb_io.Text.Trim() + txb_address.Text.Trim();
            using (var context = new ApplicationDB()) 
            {
                if (this.tableName == "Drill")
                {
                    var data = context.Drill_IO.FirstOrDefault(d => d.address == fullAddress);
                    if (data != null)
                    {
                        // ✅ 防呆：若修改後的新地址與其他筆資料重複
                        if (fullAddress != viewAddress &&
                            context.Drill_IO.Any(d => d.address == fullAddress))
                        {
                            MessageBox.Show($"⚠️ 新地址 '{fullAddress}' 已存在，請重新設定！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        data.ClassTag = comb_class.Text;
                        data.Comment = txb_comment.Text;
                        data.Description = txb_description.Text;
                        data.IOType = GetSelectedIOState(comb_io);
                        data.MaxLife = Convert.ToInt32(txb_max_number.Text);
                        data.MountTime = DateTime.Now;
                        data.RelayType = GetSelectedRelayType(comb_type);
                        data.Setting_red = Convert.ToInt32(txb_red_light.Text);
                        data.Setting_yellow = Convert.ToInt32(txb_yellow_light.Text);
                        data.UnmountTime = DateTime.Now.AddDays(10);
                        data.address = fullAddress;
                      

                        context.SaveChanges(); //  儲存
                        OnDataUpdated?.Invoke(); 
                        MessageBox.Show("✅鑽床資料已成功更新！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("⚠️ 找不到要更新的鑽床資料", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (this.tableName == "Sawing")
                {
                    var data = context.Sawing_IO.FirstOrDefault(s => s.address == fullAddress);
                    if (data != null)
                    {
                        // ✅ 防呆：若修改後的新地址與其他筆資料重複
                        if (fullAddress != viewAddress &&
                            context.Sawing_IO.Any(d => d.address == fullAddress))
                        {
                            MessageBox.Show($"⚠️ 新地址 '{fullAddress}' 已存在，請重新設定！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        data.ClassTag = comb_class.Text;
                        data.Comment = txb_comment.Text;
                        data.Description = txb_description.Text;
                        data.IOType = GetSelectedIOState(comb_io);
                        data.MaxLife = Convert.ToInt32(txb_max_number.Text);
                        data.MountTime = DateTime.Now;
                        data.RelayType = GetSelectedRelayType(comb_type);
                        data.Setting_red = Convert.ToInt32(txb_red_light.Text);
                        data.Setting_yellow = Convert.ToInt32(txb_yellow_light.Text);
                        data.UnmountTime = DateTime.Now.AddDays(10);
                        data.address = fullAddress;
                        context.SaveChanges(); // 儲存
                        OnDataUpdated?.Invoke(); // 
                        MessageBox.Show("✅鋸床資料已成功更新！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("⚠️ 找不到要更新的鋸床資料", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
          
        }

    }
}
