using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.Scheduling;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Resources;
using FX5U_IO元件監控;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;
using static FX5U_IOMonitor.Message.Notify_Message;
using static FX5U_IOMonitor.Message.Send_mode;
using static FX5U_IOMonitor.Models.Test_;
using FX5U_IOMonitor.Message;




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
            using var userService = LocalDbProvider.GetUserService();
            var user = UserService<ApplicationDB>.CurrentUser;


            using (var form = new Receive_Notification(userService.UserManager, user))
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }
        }

        private async void btn_alarm_Click(object sender, EventArgs e)
        {
            Csv2Db.Initialization_MachineprameterFromCSV("Machine_monction_data.csv");

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

        private void button4_Click(object sender, EventArgs e)
        {
            using (var form = new UtilizationRate())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }
        }
    }


}



