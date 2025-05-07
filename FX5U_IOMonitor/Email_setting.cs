using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace FX5U_IOMonitor.Resources
{
    public partial class Email_Settings : Form
    {
        private string? viewAddress;
        private string? tableName;
        private AddressInputMode currentInputMode = AddressInputMode.Hexadecimal;
        private ElementMode currentMode;
        public Action? OnDataUpdated; // ✅ 刷新資料事件

        public enum ElementMode { Add, ViewOnly }
        public enum AddressInputMode { Hexadecimal, Octal }

        public Email_Settings(ElementMode mode = ElementMode.Add, string? table = null, string? address = null)
        {
            InitializeComponent();
          ;

            currentMode = mode;
            viewAddress = address;
            tableName = table;
            // 設定 comb_machine 的 SelectedIndex
         


        }

        private void btn_add_Click(object sender, EventArgs e)
        {
           
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
            
        }

        private void btn_update_Click(object sender, EventArgs e) 
        {
            
        }

    }
}
