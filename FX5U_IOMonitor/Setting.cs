using CsvHelper;
using System.Globalization;
using static FX5U_IOMonitor.Main;
using FX5U_IOMonitor.Data;
using Newtonsoft.Json;
using System.Text;
using System.Data;
using FX5U_IOMonitor.Resources;
using System.Windows.Forms;
using FX5U_IO元件監控;
using static FX5U_IOMonitor.Email.Notify_Message;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using FX5U_IOMonitor.Email;
using static FX5U_IOMonitor.Email.Send_mode;
using FX5U_IOMonitor.Login;
using static FX5U_IOMonitor.Email.DailyTask_config;
using System;
using static Org.BouncyCastle.Math.EC.ECCurve;


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
            _scheduler.StartTask(config1);


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
                    ["CustomAction"] = new Func<Task<TaskResult>>(SendTestEmailAsync)
                }
            };

            _scheduler.AddTask(config);
            _scheduler.StartTask(config); // ★啟動



        }

        private void btn_notify_Click(object sender, EventArgs e)
        {

        }
    }


}



