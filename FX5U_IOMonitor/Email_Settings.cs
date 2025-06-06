using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Net.Mail;
using FX5U_IOMonitor.Properties;

namespace FX5U_IOMonitor.Resources
{
    public partial class Email_Settings : Form
    {


        public Email_Settings()
        {
            InitializeComponent();
            txb_senderPassword.PasswordChar = '*';  // 隱藏密碼
            comboBox1.SelectedIndex = 0;
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            LanguageManager.LanguageChanged += OnLanguageChanged;
            SwitchLanguage();

        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
        }



        private void SendEmail()
        {
            try
            {

                string Gmail_SMTP_server = Properties.Settings.Default.Gmail_SMTP_server;
                int TLS_port = Properties.Settings.Default.TLS_port;
                string senderEmail = Properties.Settings.Default.senderEmail;
                string senderPassword = Properties.Settings.Default.senderPassword;

                // 設定 Gmail SMTP 伺服器
                SmtpClient smtpClient = new SmtpClient(Gmail_SMTP_server);
                smtpClient.Port = TLS_port;  // 使用 TLS 通訊埠
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.EnableSsl = true;

                // 建立郵件內容
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                //mail.To.Add(receiverEmail);
                mail.Subject = "測試 Email";
                mail.Body = "這是一封由 WinForms 程式發送的測試郵件。";

                // 發送郵件
                smtpClient.Send(mail);

                Console.WriteLine("郵件發送成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("發送郵件時發生錯誤：" + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("內部錯誤：" + ex.InnerException.Message);
            }
        }


        private void btn_setting_Click(object sender, EventArgs e)
        {

            // 設定寄件人 Email 與密碼 (注意：若使用 Gmail，請確認帳戶已開啟「低安全性應用程式存取」或使用應用程式密碼)
            Properties.Settings.Default.senderEmail = txb_senderEmail.Text;
            Properties.Settings.Default.senderPassword = txb_senderPassword.Text; // 建議使用應用程式專用密碼

            Properties.Settings.Default.Gmail_SMTP_server = "smtp.gmail.com"; 
            Properties.Settings.Default.TLS_port = Convert.ToInt32(txb_TLS_port.Text); // 建議使用應用程式專用密碼
            Properties.Settings.Default.Save(); // ✅ 寫入設定檔

        }
        private void SwitchLanguage()
        {
            this.Text = LanguageManager.Translate("Email_SetForm_Title");
            lab_Email.Text = LanguageManager.Translate("Email_SetForm_Email");
            lab_Password.Text = LanguageManager.Translate("Email_SetForm_Password");
            lab_Protocal.Text = LanguageManager.Translate("Email_SetForm_Protocal");
            lab_Port.Text = LanguageManager.Translate("Email_SetForm_Port");
            btn_Update.Text = LanguageManager.Translate("Email_SetForm_Update");

        }
        private void Email_Settings_Load(object sender, EventArgs e)
        {
            comboBox1.DrawItem += (s, e) =>
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    string text = comboBox1.Items[e.Index].ToString();

                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;        // 水平置中
                        sf.LineAlignment = StringAlignment.Center;    // 垂直置中

                        e.Graphics.DrawString(text, comboBox1.Font, Brushes.Black, e.Bounds, sf);
                    }
                }

                e.DrawFocusRectangle();
            };
        }
    }
}
