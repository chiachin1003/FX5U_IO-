using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FX5U_IOMonitor.Data;
using Microsoft.EntityFrameworkCore;
using FX5U_IOMonitor.Login;
using Microsoft.AspNetCore.Identity;
using FX5U_IOMonitor.Models;
using static FX5U_IOMonitor.Message.Notify_Message;
using static FX5U_IOMonitor.Message.Send_mode;
using Microsoft.VisualBasic.ApplicationServices;

namespace FX5U_IOMonitor.Message
{
    public class Message_function
    {
        /// <summary>
        /// 所有使用者信箱
        /// </summary>
        /// <returns></returns>
      
        public static List<string> GetAllUserEmails()
        {
            using var context = new ApplicationDB();

            return context.Users
                .Where(u => !string.IsNullOrWhiteSpace(u.Email) && u.NotifyByEmail == true)
                .Select(u => u.Email!)
                .Distinct()
                .ToList();
        }

        public static List<string> GetUserEmails(List<string> usernames)
        {
            using var context = new ApplicationDB();
            
            // 只取 Message_function 並建立清單
            var emails = context.Users
                .Where(u => usernames.Contains(u.UserName) && !string.IsNullOrWhiteSpace(u.Email) && u.NotifyByEmail==true)
                .Select(u => u.Email!)
                .Distinct() // 去除重複
                .ToList();

            return emails;
        }
        public static List<string> GetAllUserLineAsync()
        {
            using var context = new ApplicationDB();

            var lines = context.Users
                .Where(u => !string.IsNullOrWhiteSpace(u.LineNotifyToken) && u.NotifyByLine == true)  // 避免空值
                .Select(u => u.LineNotifyToken!)
                .Distinct()
                .ToList();

            return lines;
        }
        public static List<string> GetUserLine(List<string> usernames)
        {
            using var context = new ApplicationDB();
            var lines = context.Users
                .Where(u => usernames.Contains(u.UserName) && !string.IsNullOrWhiteSpace(u.LineNotifyToken) && u.NotifyByEmail == true)
                .Select(u => u.LineNotifyToken!)
                .Distinct() 
                .ToList();

            return lines;
        }
        /// <summary>
        /// 非同步發送郵件
        /// </summary>
        /// <param name="receiver"></param>寄件人
        /// <param name="subject"></param>主旨
        /// <param name="body"></param>內容
        /// <returns></returns>
        public static async Task SendAsync(string receiver, string subject, string body)
        {
            using var client = new SmtpClient(Properties.Settings.Default.Gmail_SMTP_server)
            {
                Port = Properties.Settings.Default.TLS_port,

                Credentials = new NetworkCredential(Properties.Settings.Default.senderEmail, Properties.Settings.Default.senderPassword),

                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(Properties.Settings.Default.senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = false // 如果是 HTML 郵件，請改為 true
            };

            mail.To.Add(receiver);

            await client.SendMailAsync(mail);
        }
        public static async Task SendAsync(List<string> receivers, string subject, string body)
        {
            foreach (var to in receivers)
            {
                await SendAsync(to, subject, body);
            }
        }

        /// <summary>
        /// 故障訊息郵件發送
        /// </summary>
        /// <param name="receivers"></param>
        /// <param name="machineName"></param>
        /// <param name="partNumber"></param>
        /// <param name="addressList"></param>
        /// <param name="faultLocation"></param>
        /// <param name="possibleReasons"></param>
        /// <param name="suggestions"></param>
        public static void SendFailureAlertMail(
        List<string> receivers,
        string machineName,             // 設備名稱
        string partNumber,              // 更換料號名稱
        List<string> addressList,       // 多個元件位置
        string faultLocation,           // 故障發生位置
        List<string> possibleReasons,   // 可能原因（可選）
        List<string> suggestions        // 建議處理方式（可選）
        )
        {
            try
            {

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(Properties.Settings.Default.senderEmail),
                    Subject = "【故障通報】元件無法正常運作"
                };

                foreach (string receiver in receivers)
                {
                    if (!string.IsNullOrWhiteSpace(receiver))
                        mail.To.Add(receiver);
                }

                // 格式化項目清單（列點）
                string reasonText = possibleReasons != null && possibleReasons.Count > 0
                    ? string.Join(Environment.NewLine, possibleReasons.Select(r => "- " + r))
                    : "- （尚未提供）";

                string suggestionText = suggestions != null && suggestions.Count > 0
                    ? string.Join(Environment.NewLine, suggestions.Select((s, i) => $"{i + 1}. {s}"))
                    : "（尚未提供建議）";

                string body = $@"
                    發送通知時間：{DateTime.Now:yyyy/MM/dd HH:mm:ss}
                    設備名稱：{machineName}
                    更換料號名稱：{partNumber}
                    元件儲存器位置：{string.Join("、", addressList)}
                    故障信息為：{faultLocation}

                    系統判定此元件處於「故障狀態」。
                    可能故障原因：
                    {reasonText}

                    建議處理方式：
                    {suggestionText}

                    （自動通報信息）
                    ";

                mail.Body = body.Trim();

                SmtpClient smtpClient = new SmtpClient(Properties.Settings.Default.Gmail_SMTP_server)
                {
                    Port = Properties.Settings.Default.TLS_port,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(Properties.Settings.Default.senderEmail, Properties.Settings.Default.senderPassword)
                };

                smtpClient.Send(mail);
                Console.WriteLine("✅ 故障通知郵件發送成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ 故障通知發送失敗：" + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("🔍 內部錯誤：" + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 故障訊息郵件發送
        /// </summary>
        /// <param name="receivers"></param>
        /// <param name="machineName"></param>
        /// <param name="partNumber"></param>
        /// <param name="addressList"></param>
        /// <param name="faultLocation"></param>
        /// <param name="possibleReasons"></param>
        /// <param name="suggestions"></param>
        public static async Task SendFailureAlarmMail(
        List<string> receivers,
        string machineName,             // 設備名稱
        string partNumber,              // 更換料號名稱
        List<string> addressList,       // 多個元件位置
        string faultLocation,           // 故障發生位置
        List<string> possibleReasons,   // 可能原因（可選）
        List<string> suggestions        // 建議處理方式（可選）
        )
        {
            try
            {
                //選擇發送郵件的主旨格式

                MessageSubjectType selectedType = MessageSubjectType.TriggeredAlarm;

                string subject = MessageSubjectHelper.GetSubject(selectedType);

                // 格式化項目清單（列點）
                string reasonText = possibleReasons != null && possibleReasons.Count > 0
                    ? string.Join(Environment.NewLine, possibleReasons.Select(r => "- " + r))
                    : "- （尚未提供）";

                string suggestionText = suggestions != null && suggestions.Count > 0
                    ? string.Join(Environment.NewLine, suggestions.Select((s, i) => $"{i + 1}. {s}"))
                    : "（尚未提供建議）";

                string body = $@"
                    發送通知時間：{DateTime.Now:yyyy/MM/dd HH:mm:ss}
                    設備名稱：{machineName}
                    更換料號名稱：{partNumber}
                    元件儲存器位置：{string.Join("、", addressList)}
                    故障信息為：{faultLocation}

                    系統判定此元件處於「故障狀態」。
                    可能故障原因：
                    {reasonText}

                    建議處理方式：
                    {suggestionText}

                    （自動通報信息）
                    ";
                // 統整要送出的收件人跟資訊
                var mailInfo = new MessageInfo
                {
                    Receivers = receivers,
                    Subject = subject,
                    Body = body
                };
                int port = Properties.Settings.Default.TLS_port;

                await(port switch
                {
                    587 => SendViaSmtp587Async(mailInfo),
                    465 => SendViaSmtp465Async(mailInfo),
                    _ => throw new NotSupportedException($"不支援的 SMTP Port：{port}")
                });
                Console.WriteLine("✅ 故障通知郵件發送成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ 故障通知發送失敗：" + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("🔍 內部錯誤：" + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 單一元件壽命即將到期警告郵件通知
        /// </summary>
        /// <param name="receivers"></param>
        /// <param name="machineName"></param>
        /// <param name="partNumber"></param>
        /// <param name="address"></param>
        /// <param name="lastInstallTime"></param>
        /// <param name="maxUsage"></param>
        /// <param name="currentUsage"></param>
        public static void SendLifeWarningMail(
        List<string> receivers,
        string machineName,         // 設備名稱
        string partNumber,          // 更換料號名稱
        string address,             // 元件儲存器位置
        DateTime lastInstallTime,   // 上次安裝時間
        int maxUsage,               // 最大使用次數
        int currentUsage            // 目前已使用次數
        )
        {
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(Properties.Settings.Default.senderEmail),
                    Subject = "【系統提醒】元件壽命即將耗盡"
                };

                // 加入收件人清單
                foreach (string receiver in receivers)
                {
                    if (!string.IsNullOrWhiteSpace(receiver))
                        mail.To.Add(receiver);
                }

                double usagePercent = (double)currentUsage / maxUsage * 100;

                // 建立信件內容（可用 $ 字串內插）
                string body = $@"
                                發送通知時間：{DateTime.UtcNow:yyyy/MM/dd HH:mm:ss}
                                設備名稱：{machineName}
                                更換料號名稱：{partNumber}
                                元件儲存器位置：{address}
                                上一次安裝時間：{lastInstallTime:yyyy/MM/dd HH:mm:ss}
                                最大使用次數：{maxUsage:N0}
                                目前已使用：{currentUsage:N0} 次，當前壽命百分比：{usagePercent:F0} %

                                該元件壽命即將耗盡，請預做更換準備。

                                若已更換新元件，請更新系統壽命資訊以避免誤判通知。
                                （本提醒由設備壽命監控模組自動發出）
                                ";

                mail.Body = body.Trim(); // 清除前後空白

                SmtpClient smtpClient = new SmtpClient(Properties.Settings.Default.Gmail_SMTP_server)
                {
                    Port = Properties.Settings.Default.TLS_port,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(Properties.Settings.Default.senderEmail, Properties.Settings.Default.senderPassword)
                };
                smtpClient.Send(mail);
                Console.WriteLine("✅ 郵件已成功發送！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ 郵件發送失敗：" + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("🔍 內部錯誤：" + ex.InnerException.Message);
            }
        }





    }

}



