using System.Data;
using FX5U_IOMonitor.Resources;
using FX5U_IO元件監控;
using static FX5U_IOMonitor.Email.Notify_Message;
using FX5U_IOMonitor.Email;


using static FX5U_IOMonitor.Email.Send_mode;
using static FX5U_IOMonitor.Email.DailyTask_config;
using FX5U_IOMonitor.Models;
using Microsoft.EntityFrameworkCore;
using FX5U_IOMonitor.Login;




namespace FX5U_IOMonitor
{

    public partial class Setting : Form
    {
        public Setting()
        {

            InitializeComponent();

        }

        private void btn_file_download_Click(object sender, EventArgs e)
        {
            using (var form = new File_Settings())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }

        }

        private void btn_Mail_Manager_Click(object sender, EventArgs e)
        {
            using (var form = new Email_Settings())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }

        }

        private void btn_Alrm_Notify_Click(object sender, EventArgs e)
        {
            using (var form = new Alarm_Notify())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }
        }

        private void btn_usersetting_Click(object sender, EventArgs e)
        {
            using (var form = new UserManageForm())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);

            }
        }

        private void btn_history_Click(object sender, EventArgs e)
        {
            using (var form = new History_record())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }
        }

        private void Setting_Load(object sender, EventArgs e)
        {

        }

        private void btn_update_Click(object sender, EventArgs e)
        {

        }

        private void btn_checkpoint_Click(object sender, EventArgs e)
        {
            using (var form = new Check_point())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }
        }

        private async void btn_emailtest_Click(object sender, EventArgs e)
        {

            //選擇郵件的接收者
            List<string> allUser = email.GetAllUserEmailsAsync();

            //選擇發送郵件的主旨格式
            MessageSubjectType selectedType = MessageSubjectType.DailyHealthStatus;
            string subject = MessageSubjectHelper.GetSubject(selectedType);
            // 發送郵件的內容
            String body1 = Notify_Message.GenerateYellowComponentGroupSummary();
            String body2 = Notify_Message.GenerateRedComponentGroupSummary();
            String body = body1 + "\n----------------------------------------------------------------\n\n" + body2;

            var mailInfo = new MailInfo
            {
                Receivers = allUser,
                Subject = subject,
                Body = body
            };

            int port = Properties.Settings.Default.TLS_port;
            await (port switch
            {
                587 => SendViaSmtp587Async(mailInfo),
                465 => SendViaSmtp465Async(mailInfo),
                _ => throw new NotSupportedException($"不支援的 SMTP Port：{port}")
            });


        }
        private FlexibleScheduler _scheduler;


        private void button1_Click(object sender, EventArgs e)
        {
            _scheduler = new FlexibleScheduler();

            const string taskName = "alarmTask";

            // 若任務已存在就不重複加
            if (_scheduler.GetAllTasks().Any(t => t.TaskName == taskName))
            {
                MessageBox.Show("任務已經存在，將不重複啟動。");
                return;
            }

            var config1 = new TaskConfiguration
            {
                TaskName = taskName,
                TaskType = ScheduleTaskType.CustomTask,
                Frequency = ScheduleFrequency.Minutely, // 或其他
                ExecutionTime = TimeSpan.Zero,
                Parameters = new Dictionary<string, object>
                {
                    ["CustomAction"] = new Func<Task<TaskResult>>(SendDailyAlarmSummaryAsync)
                }
            };

            _scheduler.AddTask(config1);
            //_scheduler.StartTask(config1);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            const string taskName = "Email_Element_Task";
            _scheduler = new FlexibleScheduler();

            // 若任務已存在就不重複加
            if (_scheduler.GetAllTasks().Any(t => t.TaskName == taskName))
            {
                MessageBox.Show("任務已經存在，將不重複啟動。");
                return;
            }

            var config = new TaskConfiguration
            {
                TaskName = taskName,
                TaskType = ScheduleTaskType.CustomTask,
                Frequency = ScheduleFrequency.Minutely, // 或其他
                ExecutionTime = TimeSpan.Zero,
                Parameters = new Dictionary<string, object>
                {
                    ["CustomAction"] = new Func<Task<TaskResult>>(SendElementEmailAsync)
                }
            };
            _scheduler.AddTask(config);
            
        }

        private void btn_notify_Click(object sender, EventArgs e)
        {

        }

        private async void btn_alarm_Click(object sender, EventArgs e)
        {
            using var db = new ApplicationDB();
            var now = DateTime.UtcNow;

            // 查詢所有尚未排除的警告，且今天尚未發送提醒過
            var histories = db.AlarmHistories
                .Include(h => h.Alarm)  // 載入關聯 Alarm 資料
                .ThenInclude(a => a.Translations) // ✅ 載入翻譯
                .Where(h => h.EndTime == null)
                .ToList();

            // 如果沒有未排除的警告，不需要發送
            if (!histories.Any())
            {
                return;
            }

            // 根據每筆警告的 AlarmNotifyuser 欄位分組
            var groupedByUsers = histories
                .Where(h => !string.IsNullOrWhiteSpace(h.Alarm.AlarmNotifyuser))  // 排除未設定收件者
                .GroupBy(h => h.Alarm.AlarmNotifyuser);                           // 以通知對象分組

            bool emailSent = false;


            foreach (var group in groupedByUsers)
            {
                // 取得收件者 email 清單（支援 , 或 ; 分隔）
                var users = group.Key.Split(',', ';', StringSplitOptions.RemoveEmptyEntries)
                                     .Select(x => x.Trim())
                                     .ToList();
                List<string> allUser = email.GetUserEmails(users);

                // 建立該使用者對應的彙總信件內容
                var body = BuildEmailBody(group.ToList());

                //選擇發送郵件的主旨格式
                MessageSubjectType selectedType = MessageSubjectType.UnresolvedWarnings;
                string subject = MessageSubjectHelper.GetSubject(selectedType);

                // 統整要送出的收件人跟資訊
                var mailInfo = new MailInfo
                {
                    Receivers = allUser,
                    Subject = subject,
                    Body = body
                };
                try
                {
                    int port = Properties.Settings.Default.TLS_port;
                    await (port switch
                    {
                        587 => SendViaSmtp587Async(mailInfo),
                        465 => SendViaSmtp465Async(mailInfo),
                        _ => throw new NotSupportedException($"不支援的 SMTP Port：{port}")
                    });

                    // 更新每筆紀錄的發送時間與次數
                    foreach (var h in group)
                    {
                        h.RecordTime = DateTime.UtcNow;
                        h.Records += 1;
                    }
                    emailSent = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ 寄送失敗 - 收件人：{group.Key}，錯誤：{ex.Message}");
                }
            }

            // 寫回資料庫
            if (emailSent)
            {
                db.SaveChanges();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            _scheduler = new FlexibleScheduler();

            const string taskName = "Param_historyTask";

            if (_scheduler.GetAllTasks().Any(t => t.TaskName == taskName))
            {
                MessageBox.Show("任務已經存在，將不重複啟動。");
                return;
            }

            var config1 = new TaskConfiguration
            {
                TaskName = taskName,
                TaskType = ScheduleTaskType.CustomTask,
                Frequency = ScheduleFrequency.Minutely,
                ExecutionTime = TimeSpan.Zero,
                Parameters = new Dictionary<string, object>
                {
                    ["CustomAction"] = new Func<Task<TaskResult>>(() => RecordCurrentParameterSnapshotAsync(ScheduleFrequency.Minutely))
                }
            };

            _scheduler.AddTask(config1);
        }
    }


}



