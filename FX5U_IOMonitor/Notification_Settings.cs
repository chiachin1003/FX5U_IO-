using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Net.Mail;
using FX5U_IOMonitor.Properties;
using Newtonsoft.Json.Linq;

namespace FX5U_IOMonitor.Resources
{
    public partial class Notification_Settings : Form
    {


        public Notification_Settings()
        {
            InitializeComponent();

            txb_senderEmail.Text = Properties.Settings.Default.senderEmail;
            txb_senderPassword.Text = Properties.Settings.Default.senderPassword;

            txb_senderPassword.PasswordChar = '*';  // 隱藏密碼
            comboBox1.SelectedIndex = 0;
            comb_port.SelectedIndex = 0;

            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            LanguageManager.LanguageChanged += OnLanguageChanged;
            SwitchLanguage();

        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
        }


        private void btn_setting_Click(object sender, EventArgs e)
        {

            // 設定寄件人 email 與密碼 (注意：若使用 Gmail，請確認帳戶已開啟「低安全性應用程式存取」或使用應用程式密碼)
            Properties.Settings.Default.senderEmail = txb_senderEmail.Text;
            Properties.Settings.Default.senderPassword = txb_senderPassword.Text; // 建議使用應用程式專用密碼

            Properties.Settings.Default.Gmail_SMTP_server = "smtp.gmail.com";
            Properties.Settings.Default.TLS_port = Convert.ToInt32(comb_port.Text); // 建議使用應用程式專用密碼
            Properties.Settings.Default.Save(); // ✅ 寫入設定檔
            MessageBox.Show("Gmail 設定已儲存！", "設定成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void SwitchLanguage()
        {
            this.Text = LanguageManager.Translate("Email_SetForm_Title");
            lab_Email.Text = LanguageManager.Translate("Email_SetForm_Email");
            lab_Password.Text = LanguageManager.Translate("Email_SetForm_Password");
            lab_Protocal.Text = LanguageManager.Translate("Email_SetForm_Protocal");
            lab_Port.Text = LanguageManager.Translate("Email_SetForm_Port");
            btn_Update.Text = LanguageManager.Translate("Email_SetForm_Update");
            btn_update_line.Text = LanguageManager.Translate("Email_SetForm_Update");


        }
        private void Email_Settings_Load(object sender, EventArgs e)
        {
            txb_senderEmail.Text = Properties.Settings.Default.senderEmail;
            txb_senderPassword.Text = Properties.Settings.Default.senderPassword;
            txb_channelAccessToken.Text = Properties.Settings.Default.LineAccessToken;
            string savedPath = Properties.Settings.Default.LineImagePath;
            if (File.Exists(savedPath))
            {
                pictureBox1.Image = Image.FromFile(savedPath);
            }


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

            comb_port.DrawItem += (s, e) =>
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    string text = comb_port.Items[e.Index].ToString();

                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;        // 水平置中
                        sf.LineAlignment = StringAlignment.Center;    // 垂直置中

                        e.Graphics.DrawString(text, comb_port.Font, Brushes.Black, e.Bounds, sf);
                    }
                }

                e.DrawFocusRectangle();
            };
        }


        private void btn_update_line_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txb_channelAccessToken.Text.Trim()))
            {
                MessageBox.Show("請輸入權杖");
                return;
            }

            Properties.Settings.Default.LineAccessToken = txb_channelAccessToken.Text.Trim();
            Properties.Settings.Default.Save();

        }

        private void btn_QRcode_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "選擇圖片";
                ofd.Filter = "圖片檔案 (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = ofd.FileName;

                    string targetFolder = Path.Combine(Application.StartupPath, "UserImages");
                    Directory.CreateDirectory(targetFolder);

                    string fixedFileName = "user_image" + Path.GetExtension(selectedPath); // 保留副檔名
                    string savedImagePath = Path.Combine(targetFolder, fixedFileName);

                    // ✅ 若 PictureBox 已載入圖片，先釋放資源（否則會鎖定圖檔）
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }

                    // ✅ 若目標圖檔已存在，先刪除（安全做法）
                    if (File.Exists(savedImagePath))
                    {
                        File.Delete(savedImagePath);
                    }

                    File.Copy(selectedPath, savedImagePath, true);

                    using (FileStream fs = new FileStream(savedImagePath, FileMode.Open, FileAccess.Read))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            ms.Position = 0;
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }

                    // ✅ 儲存路徑（固定只有一筆）
                    Properties.Settings.Default.LineImagePath = savedImagePath;
                    Properties.Settings.Default.Save();

                }
            }

        }
        /// <summary>
        /// 載入圖片到指定 PictureBox（若有儲存的圖）
        /// </summary>
        public static void LoadUserImageTo(PictureBox picBox)
        {
            string path = Properties.Settings.Default.LineImagePath;

            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    picBox.Image = Image.FromStream(fs);
                }
            }
        }
    }
}
