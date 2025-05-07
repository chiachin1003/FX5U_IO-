
using System.Net.Mail;
using System.Windows.Forms;
using System.Reflection.Emit;


namespace FX5U_IOMonitor.panel_control
{
    public partial class EmailNotificationUserControl : UserControl
    {
        private List<string> emailList = new List<string>();
        public EmailNotificationUserControl()
        {
            InitializeComponent();
            InitializeUI();
        }
        private void InitializeUI()
        {
            // Label 與寄件者設定欄位
            System.Windows.Forms.Label lblSender = new System.Windows.Forms.Label
            {
                Text = "寄件者 Email:",
                AutoSize = true,
                Font = new Font("Microsoft JhengHei", 10, FontStyle.Bold),  // 粗體、10pt
                Margin = new Padding(5, 10, 0, 0),
                ForeColor = Color.Black
            };
            TextBox txtSenderEmail = new TextBox { Width = 250 };
            System.Windows.Forms.Label lblPassword = new System.Windows.Forms.Label { Text = "應用程式密碼:", AutoSize = true };
            TextBox txtPassword = new TextBox { Width = 250, UseSystemPasswordChar = true };

            // 收件者輸入與已知選單
            TextBox txtEmailInput = new TextBox { Width = 250, PlaceholderText = "請輸入 Email" };
            ComboBox cbKnownEmails = new ComboBox { Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            cbKnownEmails.Items.AddRange(new string[] { "user1@example.com", "user2@example.com", "admin@example.com" });

            // DataGridView 顯示清單
            DataGridView dgvRecipients = new DataGridView
            {
                Width = 400,
                Height = 120,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvRecipients.Columns.Add("Email", "收件人 Email");

            // 新增收件人按鈕
            Button btnAdd = new Button { Text = "新增收件人", Width = 120 };
            btnAdd.Click += (s, e) =>
            {
                string email = txtEmailInput.Text.Trim();
                if (IsValidEmail(email) && !emailList.Contains(email))
                {
                    emailList.Add(email);
                    dgvRecipients.Rows.Add(email);
                    txtEmailInput.Clear();
                }
                else
                {
                    MessageBox.Show("請輸入有效且未重複的 Email。", "提示");
                }
            };

            // 選擇加入按鈕
            Button btnSelect = new Button { Text = "選擇加入", Width = 120 };
            btnSelect.Click += (s, e) =>
            {
                string selected = cbKnownEmails.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selected) && !emailList.Contains(selected))
                {
                    emailList.Add(selected);
                    dgvRecipients.Rows.Add(selected);
                }
            };

            // 發送測試信按鈕
            Button btnSend = new Button { Text = "發送測試信", Width = 120 };
            btnSend.Click += (s, e) =>
            {
                string senderEmail = txtSenderEmail.Text.Trim();
                string password = txtPassword.Text;

                if (!IsValidEmail(senderEmail) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("請輸入正確的寄件者資訊。", "錯誤");
                    return;
                }

                foreach (string email in emailList)
                {
                    try
                    {
                        MailMessage mail = new MailMessage(senderEmail, email)
                        {
                            Subject = "測試通知",
                            Body = "這是系統發出的測試通知信。"
                        };

                        SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new System.Net.NetworkCredential(senderEmail, password),
                            EnableSsl = true
                        };
                        smtp.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"寄送給 {email} 時發生錯誤：{ex.Message}");
                    }
                }
            };

            // 刪除選中收件人
            Button btnRemove = new Button { Text = "移除選中", Width = 120 };
            btnRemove.Click += (s, e) =>
            {
                foreach (DataGridViewRow row in dgvRecipients.SelectedRows)
                {
                    string email = row.Cells["Email"].Value.ToString();
                    emailList.Remove(email);
                    dgvRecipients.Rows.Remove(row);
                }
            };

            // 建立 Layout
            FlowLayoutPanel layout = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true
            };

            layout.Controls.Add(lblSender);
            layout.Controls.Add(txtSenderEmail);
            layout.Controls.Add(lblPassword);
            layout.Controls.Add(txtPassword);
            layout.Controls.Add(txtEmailInput);
            layout.Controls.Add(cbKnownEmails);
            layout.Controls.Add(btnSelect);
            layout.Controls.Add(btnAdd);
            layout.Controls.Add(dgvRecipients);
            layout.Controls.Add(btnRemove);
            layout.Controls.Add(btnSend);

            this.Controls.Add(layout);
        }

        private bool IsValidEmail(string email)
        {
            try { return new MailAddress(email).Address == email; }
            catch { return false; }
        }
    }
}
