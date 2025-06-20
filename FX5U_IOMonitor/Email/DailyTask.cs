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
    internal class DailyTask
    {
        private FlexibleScheduler _scheduler;

        private void InitializeScheduler()
        {
            _scheduler = new FlexibleScheduler();

            
            // 設定預設任務
            SetupDefaultTasks();

        }

        private void SetupDefaultTasks()
        {
            // 1. 每日警告摘要任務（你原本的功能）
            _scheduler.AddTask(new TaskConfiguration
            {
                TaskName = "DailyAlarmSummary",
                TaskType = ScheduleTaskType.AlarmDailySummary,
                Frequency = ScheduleFrequency.Daily,
                ExecutionTime = new TimeSpan(8, 0, 0), // 每天早上8點
                IsEnabled = true,
                Parameters = new Dictionary<string, object>
                {
                    ["DatabaseConnectionString"] = "your_connection_string",
                    ["EmailSettings"] = "your_email_config"
                }
            });

         
         
            // 自定義任務範例
            _scheduler.AddTask(new TaskConfiguration
            {
                TaskName = "CustomCleanup",
                TaskType = ScheduleTaskType.CustomTask,
                Frequency = ScheduleFrequency.Daily,
                ExecutionTime = new TimeSpan(23, 30, 0), // 每天晚上11:30
                IsEnabled = true,
                Parameters = new Dictionary<string, object>
                {
                    ["CustomAction"] = new Func<Task<TaskResult>>(async () =>
                    {
                        // 自定義清理邏輯
                        await Task.Delay(500);

                        // 例如：清理暫存檔案
                        var tempFiles = System.IO.Directory.GetFiles(System.IO.Path.GetTempPath(), "*.tmp");
                        foreach (var file in tempFiles.Take(10)) // 限制數量避免長時間執行
                        {
                            try { System.IO.File.Delete(file); } catch { }
                        }

                        return new TaskResult
                        {
                            Success = true,
                            Message = $"Cleaned {Math.Min(tempFiles.Length, 10)} temp files",
                            ExecutionTime = DateTime.UtcNow
                        };
                    })
                }
            });
        }

     
   

    }


}

