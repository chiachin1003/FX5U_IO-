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

            // 隱藏密碼
            txb_senderPassword.PasswordChar = '*';
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

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
