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

            UpdateConnectmachinComboBox();
            currentMode = mode;
            viewAddress = address;
            tableName = table;
            
            if (currentMode == ElementMode.Add && !string.IsNullOrEmpty(tableName))
            {
                comb_machine.Text = table;
                comb_machine.Enabled = false;

            }

            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            SwitchLanguage();
            LanguageManager.LanguageChanged += OnLanguageChanged;

        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            bool add_OK = ValidateInputFields();
            string fullAddress = comb_io.Text.Trim() + txb_address.Text.Trim();

            if (!add_OK) return;

            using (var context = new ApplicationDB())
            {

                if (context.Machine_IO.Any(d => d.address == fullAddress && d.Machine_name == this.tableName))
                {
                    MessageBox.Show($"⚠️ 地址 '{fullAddress}' 已存在，請重新設定！", 
                        LanguageManager.Translate("Message_Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var newDrillIO = new MachineIO
                {
                    Machine_name = comb_machine.Text,
                    ClassTag = comb_class.Text,
                    Comment = txb_comment.Text,
                    Description = txb_description.Text,
                    IOType = GetSelectedIOState(comb_io),
                    MaxLife = Convert.ToInt32(txb_max_number.Text),
                    MountTime = (DateTime.UtcNow),
                    RelayType = GetSelectedRelayType(comb_type),
                    Setting_red = Convert.ToInt32(txb_red_light.Text),
                    Setting_yellow = Convert.ToInt32(txb_yellow_light.Text),
                    UnmountTime = DateTime.UtcNow.AddDays(10),
                    address = fullAddress
                };
                var translations = new Dictionary<string, string>
                {
                    { LanguageManager.Currentlanguge, txb_comment.Text?.Trim() }
                    
                }.Where(t => !string.IsNullOrWhiteSpace(t.Value))
                .ToDictionary(t => t.Key, t => t.Value);
                try
                {
                    AddMachineElement(context, comb_machine.Text, newDrillIO, translations);
                    context.SaveChanges();
                    MessageBox.Show("✅ 新增成功！", LanguageManager.Translate("Message_Success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnDataUpdated?.Invoke();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ 新增失敗：{ex.Message}", LanguageManager.Translate("Message_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

            Update_combobox(comb_machine.Text);
            if (comb_machine.Text == "Drill")
            {
                currentInputMode = AddressInputMode.Hexadecimal;
            }
            else 
            {
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
                MessageBox.Show("⚠️ 請確實填寫所有欄位，不能有空白！", LanguageManager.Translate("Message_Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (Convert.ToInt32(txb_max_number.Text) <= 0 ||
                Convert.ToInt32(txb_yellow_light.Text) <= 0 ||
                Convert.ToInt32(txb_red_light.Text) <= 0)
            {
                MessageBox.Show("⚠️ 數值設定不能小於或等於0！", LanguageManager.Translate("Message_Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                var data = context.Machine_IO.FirstOrDefault(d => d.address == address && d.Machine_name == tableName);
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

            };

        }

       
        private void btn_update_Click(object sender, EventArgs e)
        {
            bool add_OK = ValidateInputFields();
            if (!add_OK) return;
            string fullAddress = comb_io.Text.Trim() + txb_address.Text.Trim();
            using (var context = new ApplicationDB())
            {

                var data = context.Machine_IO.FirstOrDefault(d => d.address == fullAddress && d.Machine_name == this.tableName);
                if (data != null)
                {
                    // ✅ 防呆：若修改後的新地址與其他筆資料重複
                    if (fullAddress != viewAddress &&
                        context.Machine_IO.Any(d => d.address == fullAddress && d.Machine_name == this.tableName))
                    {
                        MessageBox.Show($"⚠️ 新地址 '{fullAddress}' 已存在，請重新設定！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    data.ClassTag = comb_class.Text;
                    data.Comment = txb_comment.Text;
                    data.Description = txb_description.Text;
                    data.IOType = GetSelectedIOState(comb_io);
                    data.MaxLife = Convert.ToInt32(txb_max_number.Text);
                    data.MountTime = (DateTime.UtcNow);
                    data.RelayType = GetSelectedRelayType(comb_type);
                    data.Setting_red = Convert.ToInt32(txb_red_light.Text);
                    data.Setting_yellow = Convert.ToInt32(txb_yellow_light.Text);
                    data.UnmountTime = DateTime.UtcNow.AddDays(10);
                    data.address = fullAddress;


                    context.SaveChanges(); //  儲存
                    OnDataUpdated?.Invoke();
                    MessageBox.Show("✅資料已成功更新！");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("⚠️ 找不到要更新的資料",
                        LanguageManager.Translate("Message_Success"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            };

        }


        private void SwitchLanguage()
        {

            lab_machineType.Text = LanguageManager.Translate("Element_lab_machineType");
            lab_elementType.Text = LanguageManager.Translate("Element_lab_elementType");
            lab_elementLocal.Text = LanguageManager.Translate("Element_lab_elementLocal");
            lab_class.Text = LanguageManager.Translate("Element_lab_class");
            lab_equipment.Text = LanguageManager.Translate("Element_lab_equipment");
            lab_describe.Text = LanguageManager.Translate("Element_lab_describe");
            lab_maxlifesetting.Text = LanguageManager.Translate("Element_lab_maxlifesetting");
            lab_green.Text = LanguageManager.Translate("Element_lab_green");
            lab_yellow.Text = LanguageManager.Translate("Element_lab_yellow");
            lab_yellowText.Text = LanguageManager.Translate("Element_lab_yellowText");
            lab_red.Text = LanguageManager.Translate("Element_lab_red");
            lab_redText.Text = LanguageManager.Translate("Element_lab_redText");
            btn_update.Text = LanguageManager.Translate("Element_btn_update");
            btn_add.Text = LanguageManager.Translate("Element_btn_add");
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel1);
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel2);
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel3);
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel4);
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel5);
            Text_design.SafeAdjustFont(lab_green, lab_green.Text);



        }

        private void ApplyAutoFontShrinkToTableLabels(TableLayoutPanel panel)
        {
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is Label lbl)
                {
                    lbl.Dock = DockStyle.Fill;
                    lbl.AutoSize = false;
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                    FitFontToLabel(lbl);
                }
            }
        }
        private void FitFontToLabel(Label label)
        {
            if (string.IsNullOrEmpty(label.Text)) return;

            using (Graphics g = label.CreateGraphics())
            {
                float fontSize = label.Font.Size;
                Font testFont = label.Font;
                Font prevFont = null;

                while (fontSize > 6)
                {
                    if (prevFont != null) prevFont.Dispose();
                    prevFont = testFont;

                    testFont = new Font(label.Font.FontFamily, fontSize, label.Font.Style);
                    SizeF textSize = g.MeasureString(label.Text, testFont);

                    if (textSize.Width <= label.Width - 4 && textSize.Height <= label.Height - 4)
                        break;

                    fontSize -= 0.5f;
                }

                label.Font = testFont;
                if (prevFont != null && prevFont != testFont) prevFont.Dispose();
            }
        }
        private void UpdateConnectmachinComboBox()
        {
            using (var context = new ApplicationDB())
            {
                var machineNames = context.Machine
                                   .Select(io => io.Name);

                comb_machine.Items.Clear();

                foreach (var machine in machineNames)
                {
                    comb_machine.Items.Add(machine);
                }
                comb_machine.SelectedIndex = -1;
            }


        }
        /// <summary>
        /// 添加單個機台元件並支援多語系
        /// </summary>
        private static void AddMachineElement(ApplicationDB context, string targetMachine, MachineIO elementData, Dictionary<string, string> translations)
        {
            try
            {
                // 檢查機台是否存在
                var machine = context.Machine.FirstOrDefault(m => m.Name == targetMachine);
                if (machine == null)
                {
                    machine = new Machine_number { Name = targetMachine, IP_address = "", Port = 0 };
                    context.Machine.Add(machine);
                    context.SaveChanges();
                }

                // 統一地址型別
                string unifiedBaseType = Csv2Db.DetectUnifiedAddressBase(new List<string> { elementData.address });

                // 創建 MachineIO
                var newIO = new MachineIO
                {
                    Machine_name = targetMachine,
                    ClassTag = elementData.ClassTag?.Trim() ?? "未分類",
                    Comment = translations.ContainsKey("zh-TW") ? translations["zh-TW"] : elementData.Comment?.Trim() ?? "",
                    Description = elementData.Description?.Trim() ?? "未設定",
                    IOType = elementData.IOType,
                    RelayType = elementData.RelayType,
                    MaxLife = elementData.MaxLife,
                    MountTime = DateTime.UtcNow,
                    UnmountTime = DateTime.UtcNow.AddDays(10),
                    Setting_red = elementData.Setting_red,
                    Setting_yellow = elementData.Setting_yellow,
                    address = elementData.address?.Trim() ?? $"未知_{DateTime.Now.Ticks}",
                    baseType = unifiedBaseType,
                    Translations = translations
                        .Where(t => !string.IsNullOrWhiteSpace(t.Value))
                        .Select(t => new MachineIOTranslation
                        {
                            LanguageCode = t.Key,
                            Comment = t.Value
                        }).ToList()
                };

                context.Machine_IO.Add(newIO);
            }
            catch (Exception ex)
            {
                throw new Exception($"元件添加失敗：{ex.Message}", ex);
            }
        }

    }
}
