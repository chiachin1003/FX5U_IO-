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
using Microsoft.AspNetCore.Identity;




namespace FX5U_IOMonitor
{

    public partial class Setting : Form
    {
        public Setting()
        {

            InitializeComponent();
            SwitchLanguage();
            LanguageManager.LanguageChanged += OnLanguageChanged;
        }
        private void OnLanguageChanged(string cultureName)
        {

            SwitchLanguage();
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
            using (var form = new Notification_Settings())
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
            if (UserService<ApplicationDB>.CurrentRole == SD.Role_Admin)
            {
                btn_file_download.Enabled = true;
                btn_Alrm_Notify.Enabled = true;
                btn_usersetting.Enabled = true;
                btn_Mail_Manager.Enabled = true;

            }
            else if (UserService<ApplicationDB>.CurrentRole == SD.Role_Operator)
            {
                btn_file_download.Enabled = true;
                btn_Alrm_Notify.Enabled = true;
                btn_usersetting.Enabled = false;
                btn_Mail_Manager.Enabled = false;
            }
            else if (UserService<ApplicationDB>.CurrentRole == SD.Role_User)
            {
                btn_file_download.Enabled = false;
                btn_Alrm_Notify.Enabled = false;
                btn_usersetting.Enabled = false;
                btn_Mail_Manager.Enabled = false;
            }
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
            List<string> allEmailUser = Message_function.GetAllUserEmails();
            List<string> allLineUser = Message_function.GetAllUserLineAsync();

            //選擇發送郵件的主旨格式
            MessageSubjectType selectedType = MessageSubjectType.DailyHealthStatus;
            string subject = MessageSubjectHelper.GetSubject(selectedType);
            // 發送郵件的內容
            String body1 = Notify_Message.GenerateYellowComponentGroupSummary();
            String body2 = Notify_Message.GenerateRedComponentGroupSummary();
            String body = body1 + "\n----------------------------------------------------------------\n\n" + body2;

            var lineInfo = new MessageInfo
            {
                Receivers = allLineUser,
                Subject = subject,
                Body = body
            };

            var mailInfo = new MessageInfo
            {
                Receivers = allEmailUser,
                Subject = subject,
                Body = body
            };
            await SendLineNotificationAsync(lineInfo);

            int port = Properties.Settings.Default.TLS_port;
            await (port switch
            {
                587 => SendViaSmtp587Async(mailInfo),
                465 => SendViaSmtp465Async(mailInfo),
                _ => throw new NotSupportedException($"不支援的 SMTP Port：{port}")
            });


        }
        //private FlexibleScheduler _scheduler;


        private void button1_Click(object sender, EventArgs e)
        {
            //_scheduler = new FlexibleScheduler();

            //const string taskName = "alarmTask";

            //// 若任務已存在就不重複加
            //if (_scheduler.GetAllTasks().Any(t => t.TaskName == taskName))
            //{
            //    MessageBox.Show("任務已經存在，將不重複啟動。");
            //    return;
            //}

            //var config1 = new TaskConfiguration
            //{
            //    TaskName = taskName,
            //    TaskType = ScheduleTaskType.CustomTask,
            //    Frequency = ScheduleFrequency.Minutely, // 或其他
            //    ExecutionTime = TimeSpan.Zero,
            //    Parameters = new Dictionary<string, object>
            //    {
            //        ["CustomAction"] = new Func<Task<TaskResult>>(DailyTaskExecutors.SendDailyAlarmSummaryEmailAsync)
            //    }
            //};

            //_scheduler.AddTask(config1);

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await DailyTaskExecutors.SendElementEmailAsync();



            //const string taskName = "Email_Element_Task";
            //_scheduler = new FlexibleScheduler();

            //// 若任務已存在就不重複加
            //if (_scheduler.GetAllTasks().Any(t => t.TaskName == taskName))
            //{
            //    MessageBox.Show("任務已經存在，將不重複啟動。");
            //    return;
            //}

            //var config = new TaskConfiguration
            //{
            //    TaskName = taskName,
            //    TaskType = ScheduleTaskType.CustomTask,
            //    Frequency = ScheduleFrequency.Minutely, // 或其他
            //    ExecutionTime = TimeSpan.Zero,
            //    Parameters = new Dictionary<string, object>
            //    {
            //        ["CustomAction"] = new Func<Task<TaskResult>>(SendElementEmailAsync)
            //    }
            //};
            //_scheduler.AddTask(config);

        }

        private void btn_notify_Click(object sender, EventArgs e)
        {
            using var userService = new UserService<ApplicationDB>();
            var user = UserService<ApplicationDB>.CurrentUser;


            using (var form = new Receive_Notification(userService.UserManager, user))
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }
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
                // 取得收件者 Message_function 清單（支援 , 或 ; 分隔）
                var users = group.Key.Split(',', ';', StringSplitOptions.RemoveEmptyEntries)
                                     .Select(x => x.Trim())
                                     .ToList();
                List<string> allUser = Message_function.GetUserEmails(users);

                // 建立該使用者對應的彙總信件內容
                var body = DailyTaskExecutors.BuildEmailBody(group.ToList());

                //選擇發送郵件的主旨格式
                MessageSubjectType selectedType = MessageSubjectType.UnresolvedWarnings;
                string subject = MessageSubjectHelper.GetSubject(selectedType);

                // 統整要送出的收件人跟資訊
                var mailInfo = new MessageInfo
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
            //_scheduler = new FlexibleScheduler();

            //const string taskName = "Param_historyTask";

            //if (_scheduler.GetAllTasks().Any(t => t.TaskName == taskName))
            //{
            //    MessageBox.Show("任務已經存在，將不重複啟動。");
            //    return;
            //}

            //var config1 = new TaskConfiguration
            //{
            //    TaskName = taskName,
            //    TaskType = ScheduleTaskType.CustomTask,
            //    Frequency = ScheduleFrequency.Daily,
            //    ExecutionTime = TimeSpan.Zero,
            //    Parameters = new Dictionary<string, object>
            //    {
            //        ["CustomAction"] = new Func<Task<TaskResult>>(() => DailyTaskExecutors.RecordCurrentParameterSnapshotAsync(ScheduleFrequency.Daily))
            //    }
            //};

            //_scheduler.AddTask(config1);

        }

        private void SwitchLanguage()
        {
            btn_Mail_Manager.Text = LanguageManager.Translate("Email_SetForm_Title");
            btn_usersetting.Text = LanguageManager.Translate("UserManageForm_Title");
            btn_checkpoint.Text = LanguageManager.Translate("Settingmanu_checkpoint");
            btn_file_download.Text = LanguageManager.Translate("Settingmanu_filedownload");
            btn_Alrm_Notify.Text = LanguageManager.Translate("Alarm_Notify_title");
            btn_notify.Text = LanguageManager.Translate("Receive_Notification");
            btn_history.Text = LanguageManager.Translate("Settingmanu_history");
            btn_unit.Text = LanguageManager.Translate("Unit_Setting_Setting");
        }

        private void btn_unit_Click(object sender, EventArgs e)
        {
            using (var form = new Unit_Setting())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }
        }
    }


}



