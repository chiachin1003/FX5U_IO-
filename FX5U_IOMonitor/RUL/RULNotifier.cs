using FX5U_IOMonitor.Email;
using FX5U_IOMonitor.Models;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FX5U_IOMonitor.Email.DailyTask_config;
using static FX5U_IOMonitor.Email.Notify_Message;
using static FX5U_IOMonitor.Email.Send_mode;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Timer = System.Threading.Timer;

namespace FX5U_IOMonitor.RUL
{
    public class RULNotifier : IRULNotifier
    {
        private readonly ConcurrentQueue<QueuedNotification> _queue = new();
        private readonly Dictionary<string, DateTime> _cooldownMap = new(); // address_state -> time
        private readonly Timer _timer;
        private readonly TimeSpan _cooldown = TimeSpan.FromMinutes(3);

        public class QueuedNotification
        {
            public RULThresholdCrossedEventArgs Args { get; set; } = null!;
            public DateTime NextTriggerTime { get; set; } // 幾時可以真正送出
        }
        public RULNotifier()
        {
            _timer = new Timer(ProcessQueue, null, 0, 5000); // 每5秒處理通知
        }

        public void Enqueue(RULThresholdCrossedEventArgs args)
        {
            if (args == null)
            {
                Console.WriteLine("❌ 忽略空的 args！");
                return;
            }

            Console.WriteLine($"✅ 嘗試通知：{args.Machine}, {args.Address}, {args.State}");
            string key = $"{args.Address}_{args.State}";
            DateTime now = DateTime.Now;

            if (_cooldownMap.TryGetValue(key, out DateTime lastTime) && (now - lastTime) < _cooldown)
            {
                
                var delay = _cooldown - (now - lastTime);
                Console.WriteLine($"⚠️ 冷卻中，延後 {delay.TotalSeconds:F1} 秒後通知");
                _queue.Enqueue(new QueuedNotification
                {
                    Args = args,
                    NextTriggerTime = lastTime + _cooldown
                });
                return;
               
            }

            // 立即觸發
            _cooldownMap[key] = now;
            _queue.Enqueue(new QueuedNotification
            {
                Args = args,
                NextTriggerTime = now
            });
        }

        private async void ProcessQueue(object? state)
        {
            var dueNotifications = new List<QueuedNotification>();
            var now = DateTime.Now;

            while (_queue.TryDequeue(out var queued))
            {
                if (now >= queued.NextTriggerTime)
                    dueNotifications.Add(queued);
                else
                    _queue.Enqueue(queued); // 還沒到時間 → 放回去
            }
            if (dueNotifications.Count == 0)
                return;

            List<string> messages = dueNotifications.Select(q =>
            {
                var args = q.Args;
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return $"{timestamp} - [即時RUL通知] {args.Machine} - {args.Address}：{args.State} 區，RUL = {args.RUL:F2}";
            }).ToList();

            string mergedMessage = string.Join("\n\n", messages);
            
            Application.OpenForms[0]?.BeginInvoke(() =>
            {
                MessageBox.Show(mergedMessage, "RUL 即時通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            });

            List<string> allUser = Message_function.GetAllUserEmails();
            List<string> allUser_line = Message_function.GetAllUserLineAsync();

            MessageSubjectType selectedType = MessageSubjectType.TriggeredLifeNotification;
            string subject = MessageSubjectHelper.GetSubject(selectedType);


            var mailInfo = new MessageInfo
            {
                Receivers = allUser,
                Subject = subject,
                Body = mergedMessage
            };
            var lineInfo = new MessageInfo
            {
                Receivers = allUser_line,
                Subject = subject,
                Body = mergedMessage
            };

            int port = Properties.Settings.Default.TLS_port;
            await (port switch
            {
                587 => SendViaSmtp587Async(mailInfo),
                465 => SendViaSmtp465Async(mailInfo),
                _ => throw new NotSupportedException($"不支援的 SMTP Port：{port}")
            });

            await SendLineNotificationAsync(lineInfo);

            
        }

        public static Dictionary<string, RULThresholdInfo> GetRULMapByMachine(string machineName)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO
                    .Where(m => m.Machine_name == machineName)
                    .ToDictionary(
                        m => m.address,  
                        m => new RULThresholdInfo  
                        {
                            RUL = m.RUL,
                            SetY = m.Setting_yellow,
                            SetR = m.Setting_red 
                        });
            }
        }
    }
}
