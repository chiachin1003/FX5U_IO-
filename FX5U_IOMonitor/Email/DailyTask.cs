using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static FX5U_IOMonitor.Email.DailyTask_config;

namespace FX5U_IOMonitor.Email
{
    internal static class DailyTask
    {
        private static FlexibleScheduler _scheduler;


        public static void StartAlarmScheduler()
        {
            _scheduler = new FlexibleScheduler();

            const string taskName = "alarmTask";

            if (_scheduler.GetAllTasks().Any(t => t.TaskName == taskName))
                return;

            var config = new TaskConfiguration
            {
                TaskName = taskName,
                TaskType = ScheduleTaskType.CustomTask,
                Frequency = ScheduleFrequency.Daily,
                ExecutionTime = new TimeSpan(8, 0, 0), 
                IsEnabled = true,
                Parameters = new Dictionary<string, object>
                {
                    ["CustomAction"] = new Func<Task<TaskResult>>(SendDailyAlarmSummaryEmailAsync)
                }
            };

            _scheduler.AddTask(config); // 🔁 加了就會自動開始（你剛剛有設定 IsEnabled 啟動）
        }
        public static void StartElementScheduler()
        {
            _scheduler = new FlexibleScheduler();
            const string taskName = "Email_Element_Task";

            if (_scheduler.GetAllTasks().Any(t => t.TaskName == taskName))
            {
                MessageBox.Show("任務已經存在，將不重複啟動。");
                return;
            }

            var config = new TaskConfiguration
            {
                TaskName = taskName,
                TaskType = ScheduleTaskType.CustomTask,
                Frequency = ScheduleFrequency.Daily, // 或其他
                ExecutionTime = new TimeSpan(8, 0, 0),
                Parameters = new Dictionary<string, object>
                {
                    ["CustomAction"] = new Func<Task<TaskResult>>(SendElementEmailAsync)
                }
            };
            _scheduler.AddTask(config);
        }
        public static void StartParam_historyTaskScheduler()
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
                Frequency = ScheduleFrequency.Hourly,
                ExecutionTime = TimeSpan.Zero,
                Parameters = new Dictionary<string, object>
                {
                    ["CustomAction"] = new Func<Task<TaskResult>>(() => RecordCurrentParameterSnapshotAsync(ScheduleFrequency.Hourly))
                }
            };

            _scheduler.AddTask(config1);
        }

        //private FlexibleScheduler _scheduler;

        //private void InitializeScheduler()
        //{
        //    _scheduler = new FlexibleScheduler();


        //    // 設定預設任務
        //    SetupDefaultTasks();

        //}

        //private void SetupDefaultTasks()
        //{
        //    // 1. 每日警告摘要任務（你原本的功能）
        //    _scheduler.AddTask(new TaskConfiguration
        //    {
        //        TaskName = "DailyAlarmSummary",
        //        TaskType = ScheduleTaskType.AlarmDailySummary,
        //        Frequency = ScheduleFrequency.Daily,
        //        ExecutionTime = new TimeSpan(8, 0, 0), // 每天早上8點
        //        IsEnabled = true,
        //        Parameters = new Dictionary<string, object>
        //        {
        //            ["DatabaseConnectionString"] = "your_connection_string",
        //            ["EmailSettings"] = "your_email_config"
        //        }
        //    });



        //    // 自定義任務範例
        //    _scheduler.AddTask(new TaskConfiguration
        //    {
        //        TaskName = "CustomCleanup",
        //        TaskType = ScheduleTaskType.CustomTask,
        //        Frequency = ScheduleFrequency.Daily,
        //        ExecutionTime = new TimeSpan(23, 30, 0), // 每天晚上11:30
        //        IsEnabled = true,
        //        Parameters = new Dictionary<string, object>
        //        {
        //            ["CustomAction"] = new Func<Task<TaskResult>>(async () =>
        //            {
        //                // 自定義清理邏輯
        //                await Task.Delay(500);

        //                // 例如：清理暫存檔案
        //                var tempFiles = System.IO.Directory.GetFiles(System.IO.Path.GetTempPath(), "*.tmp");
        //                foreach (var file in tempFiles.Take(10)) // 限制數量避免長時間執行
        //                {
        //                    try { System.IO.File.Delete(file); } catch { }
        //                }

        //                return new TaskResult
        //                {
        //                    Success = true,
        //                    Message = $"Cleaned {Math.Min(tempFiles.Length, 10)} temp files",
        //                    ExecutionTime = DateTime.UtcNow
        //                };
        //            })
        //        }
        //    });
        //}




    }


    }

