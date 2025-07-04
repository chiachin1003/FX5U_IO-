
using System.Net.Mail;
using System.Net;
using MailKit.Security;
using MimeKit;
using LineNotification.NotificationManagerModule;
using LineNotificationModule;


namespace FX5U_IOMonitor.Email
{
    public class Send_mode
    {

        public class MailInfo
        {
            public List<string> Receivers { get; set; } = new();
            public string Subject { get; set; } = "";   //文章標題
            public string Body { get; set; } = "";   //文章內容
        }

        /// <summary>
        /// 使用Smtp 通訊格式，使用正式郵件系統標準通訊(IETF)；加密方式：STARTTLS
        /// </summary>
        /// <param name="mailInfo"></param>
        /// <returns></returns>
        public static async Task SendViaSmtp587Async(MailInfo mailInfo)
        {
            using var client = new SmtpClient(Properties.Settings.Default.Gmail_SMTP_server)
            {
                Port = 587,
                Credentials = new NetworkCredential(Properties.Settings.Default.senderEmail, Properties.Settings.Default.senderPassword),
                EnableSsl = true
            };

            foreach (var receiver in mailInfo.Receivers)
            {
                using var mail = new MailMessage
                {
                    From = new MailAddress(Properties.Settings.Default.senderEmail),
                    Subject = mailInfo.Subject,
                    Body = mailInfo.Body,
                    IsBodyHtml = false
                };
                mail.To.Add(receiver);

                await client.SendMailAsync(mail);
            }
        }
        /// <summary>
        /// 使用Smtp 通訊格式，非正式郵件系統標準通訊；加密方式：SSL/TLS(立即加密)
        /// </summary>
        /// <param name="mailInfo"></param>
        /// <returns></returns>
        public static async Task SendViaSmtp465Async(MailInfo mailInfo)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("你的名稱", Properties.Settings.Default.senderEmail));

            foreach (var receiver in mailInfo.Receivers)
            {
                message.To.Add(new MailboxAddress("", receiver));
            }
            message.Subject = mailInfo.Subject;
            message.Body = new TextPart("plain") { Text = mailInfo.Body };

            using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await client.ConnectAsync(Properties.Settings.Default.Gmail_SMTP_server, 465, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(Properties.Settings.Default.senderEmail, Properties.Settings.Default.senderPassword);
                await client.SendAsync(message);
            }
            catch (SslHandshakeException sslEx)
            {
                // 可改為寫入 log 或顯示給使用者
                MessageBox.Show("SSL 錯誤：無法建立安全連線，請確認伺服器憑證是否有效，或考慮改用 Port 587。", "連線錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(sslEx); // 可改為 log
            }
            catch (AuthenticationException authEx)
            {
                MessageBox.Show("驗證失敗：請確認帳號密碼是否正確。", "帳號錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(authEx);
            }
            catch (Exception ex)
            {
                MessageBox.Show("發送失敗：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }
            finally
            {
                if (client.IsConnected)
                    await client.DisconnectAsync(true);
            }

           
        }



        /// <summary>
        /// Line_notify通知功能
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="userId"></param>
        /// <param name="mailInfo"></param>
        /// <returns></returns>
        public static async Task<bool> SendLineNotificationAsync(MailInfo mailInfo)
        {
            string channelAccessToken = Properties.Settings.Default.LineAccessToken;
            var lineClient = new LineNotifyClient(channelAccessToken);
            var notificationManager = new LineNotificationManager(lineClient);

            if (mailInfo == null || !mailInfo.Receivers.Any())
            {
                MessageBox.Show("沒有接收到使用者！");
                return false;
            }

            if (mailInfo == null || string.IsNullOrWhiteSpace(mailInfo.Body))
            {
                MessageBox.Show("推播內容不可為空！");
                return false;
            }

            // 將主旨與內文組合為一段文字（可依需求格式化）
            string message = $"📢 {mailInfo.Subject}\n\n{mailInfo.Body}";

            try
            {
                bool allSuccess = true;

                foreach (var userId in mailInfo.Receivers)
                {
                    bool success = await notificationManager.SendToSingleUserAsync(userId, message);
                    allSuccess &= success; // 只要有一個失敗就為 false
                }

                MessageBox.Show(allSuccess ? "Line 通知全部發送成功！" : "部分 Line 通知發送失敗！");
                return allSuccess;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"推播過程出錯：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



    }
}
