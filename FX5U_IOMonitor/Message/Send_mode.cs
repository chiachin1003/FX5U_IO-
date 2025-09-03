
using System.Net.Mail;
using System.Net;
using MailKit.Security;
using MimeKit;
using LineNotification.NotificationManagerModule;
using LineNotificationModule;
using FX5U_IOMonitor.Config;


namespace FX5U_IOMonitor.Message
{
    public class Send_mode
    {

        public class MessageInfo
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
        public static async Task SendViaSmtp587Async(MessageInfo mailInfo)
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
        public static async Task SendViaSmtp465Async(MessageInfo mailInfo)
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
                string resultMessage = "SSL 錯誤：無法建立安全連線，請確認伺服器憑證是否有效，或考慮改用 Port 587。";
                Message_Config.LogMessage($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}] {resultMessage}");
                Console.WriteLine(sslEx); 
            }
            catch (AuthenticationException authEx)
            {
                string resultMessage = "驗證失敗：請確認帳號密碼是否正確。";
                Message_Config.LogMessage($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}] {resultMessage}");
                Console.WriteLine(authEx);
            }
            catch (Exception ex)
            {
                string resultMessage = $"郵件發送訊息的過程出錯：{ex.Message}";
                Message_Config.LogMessage($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}] {resultMessage}");
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
        /// <param name="messageInfo"></param>
        /// <returns></returns>
        public static async Task<bool> SendLineNotificationAsync(MessageInfo messageInfo)
        {
            string channelAccessToken = Properties.Settings.Default.LineAccessToken;
            var lineClient = new LineNotifyClient(channelAccessToken);
            var notificationManager = new LineNotificationManager(lineClient);

            if (messageInfo == null || !messageInfo.Receivers.Any())
            {
                Message_Config.LogMessage($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}] 目前沒有任何使用者的Line被勾選需要被通知信息！");
                return false;
            }

            if (messageInfo == null || string.IsNullOrWhiteSpace(messageInfo.Body))
            {
                Message_Config.LogMessage($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}] 訊息發送的資料為空");
                return false;
            }

            // 將主旨與內文組合為一段文字（可依需求格式化）
            string message = $"📢 {messageInfo.Subject}\n\n{messageInfo.Body}";

            try
            {
                bool allSuccess = true;

                foreach (var userId in messageInfo.Receivers)
                {
                    bool success = await notificationManager.SendToSingleUserAsync(userId, message);
                    allSuccess &= success; // 只要有一個失敗就為 false
                }
                string resultMessage = allSuccess ? "Line 通知全部發送成功！" : "部分 Line 通知發送失敗！";

                Message_Config.LogMessage($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}] {resultMessage}");
                return allSuccess;
            }
            catch (Exception ex)
            {
                string resultMessage = $"Line發送訊息的過程出錯：{ex.Message}";
                Message_Config.LogMessage($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}] {resultMessage}");

                return false;
            }
        }



    }
}
