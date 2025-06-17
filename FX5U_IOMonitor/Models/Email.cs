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

namespace FX5U_IOMonitor.Models
{
    public class Email
    {
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
        /// 每日定期發送尚未排除的警告
        /// </summary>
        public class AlarmDailySummaryScheduler
        {
            private System.Threading.Timer? _timer; // 定時器，用於排程
            private readonly TimeSpan _runTime;     // 使用者設定的每日執行時間

            // 建構函式，指定每日執行的時間（如：早上8點）
            public AlarmDailySummaryScheduler(TimeSpan userDefinedTime)
            {
                _runTime = userDefinedTime;
            }

            // 啟動排程任務
            public void Start()
            {
                // 檢查是否需要立即發送（NotifyTime 為空或今日尚未發送）
                if (ShouldSendImmediately())
                {
                    // 立即發送
                    Task.Run(async () => await SendDailyAlarmSummaryAsync());
                }

                // 計算從現在到下一次排程時間的延遲
                TimeSpan delay = GetInitialDelay(_runTime);

                // 建立 Timer，第一次執行在 delay 之後，之後每天執行一次
                _timer = new System.Threading.Timer(async _ =>
                {
                    await SendDailyAlarmSummaryAsync();  // 每日執行的邏輯
                }, null, delay, TimeSpan.FromDays(1));    // 週期為每日一次
            }

            // 停止排程任務
            public void Stop()
            {
                _timer?.Dispose();  // 釋放 Timer 資源
            }

            // 計算從現在到下次執行時間的延遲時間
            private TimeSpan GetInitialDelay(TimeSpan targetTime)
            {
                var now = DateTime.UtcNow;
                var targetDateTime = now.Date + targetTime;  // 今天的目標時間

                // 如果已超過今天的目標時間，或今天已經發送過，則延到明天
                if (now > targetDateTime || !ShouldSendToday())
                {
                    targetDateTime = targetDateTime.AddDays(1);
                }

                return targetDateTime - now;
            }

            // 檢查今天是否還需要發送
            private bool ShouldSendToday()
            {
                var lastNotifyTime = Properties.Settings.Default.NotifyTime;
                var today = DateTime.UtcNow.Date;

                // 如果今天還沒發送過，則需要發送
                return lastNotifyTime.Date < today;
            }

            // 檢查是否需要立即發送
            private bool ShouldSendImmediately()
            {
                var lastNotifyTime = Properties.Settings.Default.NotifyTime;

                // 如果 NotifyTime 為空或空字串，需要立即發送
                if (lastNotifyTime == DateTime.MinValue || string.IsNullOrEmpty(lastNotifyTime.ToString()))
                {
                    return true;
                }

                // 如果上次發送時間不是今天，需要發送
                var today = DateTime.UtcNow.Date;
                if (lastNotifyTime.Date < today)
                {
                    return true;
                }

                return false;
            }

           

            // 實際執行每日通知邏輯
            private async Task SendDailyAlarmSummaryAsync()
            {
                using var db = new ApplicationDB();
                var now = DateTime.UtcNow;

                // 查詢所有尚未排除的警告，且今天尚未發送提醒過
                var histories = db.AlarmHistories
                    .Include(h => h.Alarm)  // 載入關聯 Alarm 資料
                    .Where(h => h.EndTime == null && h.RecordTime.Date != now.Date)
                    .ToList();

                // 如果沒有未排除的警告，不需要發送
                if (!histories.Any())
                {
                    // 仍需更新 NotifyTime，表示今天已檢查過
                    UpdateNotifyTime(now);
                    return;
                }

                // 根據每筆警告的 AlarmNotifyuser 欄位分組
                var groupedByUsers = histories
                    .Where(h => !string.IsNullOrWhiteSpace(h.Alarm.AlarmNotifyuser))  // 排除未設定收件者
                    .GroupBy(h => h.Alarm.AlarmNotifyuser);                           // 以通知對象分組

                bool emailSent = false;

                foreach (var group in groupedByUsers)
                {
                    // 取得收件者 Email 清單（支援 , 或 ; 分隔）
                    var users = group.Key.Split(',', ';', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(x => x.Trim())
                                         .ToList();

                    // 建立該使用者對應的彙總信件內容
                    var body = BuildEmailBody(group.ToList());
                    var subject = $"📬 每日未排除警告摘要（{now:yyyy/MM/dd}）";

                    // 發送 Email 給每位收件人
                    foreach (var email in users)
                    {
                        await SendAsync(email, subject, body);
                        emailSent = true;
                    }

                    // 更新每筆紀錄的發送時間與次數
                    foreach (var h in group)
                    {
                        h.RecordTime = now;
                        h.Records += 1;
                    }
                }

                // 寫回資料庫
                if (emailSent)
                {
                    db.SaveChanges();
                }

                UpdateNotifyTime(now);

            }
            // 更新系統設定中的 NotifyTime
            private void UpdateNotifyTime(DateTime notifyTime)
            {
                Properties.Settings.Default.NotifyTime = notifyTime;
                Properties.Settings.Default.Save();
            }

            // 建立 Email 內容（將多筆警告合併為一封摘要）
            private string BuildEmailBody(List<AlarmHistory> alarms)
            {
                var sb = new StringBuilder();
                sb.AppendLine("📌 以下為尚未排除的警告摘要：\n");

                foreach (var h in alarms)
                {
                    sb.AppendLine($"故障地址：{h.Alarm.address}");                        // 警告位置
                    sb.AppendLine($"警告描述：{h.Alarm.Description}");                  // 更換料件
                    sb.AppendLine($"可能錯誤錯誤：{h.Alarm.Error}");                        // 錯誤內容
                    sb.AppendLine($"發生時間：{h.StartTime:yyyy-MM-dd HH:mm}");   // 發生時間
                    sb.AppendLine($"已發送次數：{h.Records + 1}");                 // 預估下一次寄送是第幾次
                    sb.AppendLine("-------------------------------------------");
                }

                return sb.ToString();
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



