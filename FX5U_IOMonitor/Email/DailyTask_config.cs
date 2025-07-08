using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static FX5U_IOMonitor.Email.Notify_Message;
using static FX5U_IOMonitor.Email.Send_mode;
using Timer = System.Threading.Timer;
using FX5U_IOMonitor.Email;
using System.Diagnostics;


namespace FX5U_IOMonitor.Email
{
    internal class DailyTask_config
    {
        // 定義任務類型枚舉
        public enum ScheduleTaskType
        {
            CustomTask
        }
        // 定義排程頻率
        public enum ScheduleFrequency
        {
            Minutely,   
            Daily,
            Weekly,
            Monthly,
            Hourly,
            Custom
        }

        // 任務配置類別
        public class TaskConfiguration
        {
            public ScheduleTaskType TaskType { get; set; }
            public ScheduleFrequency Frequency { get; set; }
            public TimeSpan ExecutionTime { get; set; }
            public string TaskName { get; set; }
            public bool IsEnabled { get; set; } = true;
            public Dictionary<string, object> Parameters { get; set; } = new();
            public DateTime? LastExecutionTime { get; set; }
            public int ExecutionCount { get; set; }
        }

        // 任務執行結果
        public class TaskResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public DateTime ExecutionTime { get; set; }
            public Exception Exception { get; set; }
        }

        // 任務執行委派
        public delegate Task<TaskResult> TaskExecutor(TaskConfiguration config);

        // 主要的排程器類別
        public class FlexibleScheduler
        {
            private readonly Dictionary<string, Timer> _timers = new();
            private readonly Dictionary<ScheduleTaskType, TaskExecutor> _taskExecutors = new();
            private readonly List<TaskConfiguration> _taskConfigurations = new();

            public event Action<TaskConfiguration, TaskResult> TaskCompleted;
            public event Action<TaskConfiguration, Exception> TaskFailed;

            public FlexibleScheduler()
            {
                InitializeDefaultTaskExecutors();
            }

            // 初始化預設任務執行器
            private void InitializeDefaultTaskExecutors()
            {
                _taskExecutors[ScheduleTaskType.CustomTask] = ExecuteCustomTaskAsync;
            }

            // 註冊自定義任務執行器
            public void RegisterTaskExecutor(ScheduleTaskType taskType, TaskExecutor executor)
            {
                _taskExecutors[taskType] = executor;
            }

            // 新增任務配置
            public void AddTask(TaskConfiguration config)
            {
                if (config == null) throw new ArgumentNullException(nameof(config));
                if (string.IsNullOrEmpty(config.TaskName)) throw new ArgumentException("TaskName cannot be null or empty");

                _taskConfigurations.Add(config);

                if (config.IsEnabled)
                {
                    StartTask(config);
                }
            }

            // 移除任務
            public void RemoveTask(string taskName)
            {
                var config = _taskConfigurations.Find(t => t.TaskName == taskName);
                if (config != null)
                {
                    StopTask(config);
                    _taskConfigurations.Remove(config);
                }
            }

            // 啟動特定任務
            public void StartTask(TaskConfiguration config)
            {
                if (!config.IsEnabled) return;

                StopTask(config); // 先停止現有的計時器

                if (ShouldExecuteImmediately(config))
                {
                    Task.Run(async () => await ExecuteTaskAsync(config));
                }

                var delay = ShouldExecuteImmediately(config)
                    ? GetPeriod(config.Frequency) // 排程下次執行就好
                    : GetInitialDelay(config);
                var period = GetPeriod(config.Frequency);

                var timer = new Timer(async _ => await ExecuteTaskAsync(config), null, delay, period);
                _timers[config.TaskName] = timer;
            }

            // 停止特定任務
            public void StopTask(TaskConfiguration config)
            {
                if (_timers.TryGetValue(config.TaskName, out var timer))
                {
                    timer?.Dispose();
                    _timers.Remove(config.TaskName);
                }
            }

            // 啟動所有任務
            public void StartAllTasks()
            {
                foreach (var config in _taskConfigurations.Where(c => c.IsEnabled))
                {
                    StartTask(config);
                }
            }

            // 停止所有任務
            public void StopAllTasks()
            {
                foreach (var timer in _timers.Values)
                {
                    timer?.Dispose();
                }
                _timers.Clear();
            }

            // 手動執行任務
            public async Task<TaskResult> ExecuteTaskManuallyAsync(string taskName)
            {
                var config = _taskConfigurations.Find(t => t.TaskName == taskName);
                if (config == null)
                {
                    return new TaskResult
                    {
                        Success = false,
                        Message = $"Task '{taskName}' not found",
                        ExecutionTime = DateTime.UtcNow
                    };
                }

                return await ExecuteTaskAsync(config);
            }

            // 更新任務配置
            public void UpdateTask(string taskName, Action<TaskConfiguration> updateAction)
            {
                var config = _taskConfigurations.Find(t => t.TaskName == taskName);
                if (config != null)
                {
                    updateAction(config);

                    if (config.IsEnabled)
                    {
                        StartTask(config);
                    }
                    else
                    {
                        StopTask(config);
                    }
                }
            }

            // 取得任務狀態
            public List<TaskConfiguration> GetAllTasks()
            {
                return _taskConfigurations.ToList();
            }

            /// <summary>
            /// 檢查是否需要立即執行
            /// </summary>
            /// <param name="config"></param>
            /// <returns></returns>
            private bool ShouldExecuteImmediately(TaskConfiguration config)
            {
                if (!config.LastExecutionTime.HasValue)
                {
                    return true;
                }

                var now = DateTime.UtcNow;
                return config.Frequency switch
                {
                    ScheduleFrequency.Daily => config.LastExecutionTime.Value.Date < now.Date,
                    ScheduleFrequency.Weekly => (now - config.LastExecutionTime.Value).TotalDays >= 7,
                    ScheduleFrequency.Monthly => config.LastExecutionTime.Value.Month != now.Month ||
                                               config.LastExecutionTime.Value.Year != now.Year,
                    ScheduleFrequency.Hourly => (now - config.LastExecutionTime.Value).TotalHours >= 1,
                    _ => false
                };
            }

            // 計算初始延遲時間
            private TimeSpan GetInitialDelay(TaskConfiguration config)
            {
                var now = DateTime.UtcNow;
                var targetDateTime = GetNextExecutionTime(config, now);
                return targetDateTime - now;
            }

            /// <summary>
            /// 計算下次執行時間
            /// </summary>
            /// <param name="config"></param>
            /// <param name="currentTime"></param>
            /// <returns></returns>
            private DateTime GetNextExecutionTime(TaskConfiguration config, DateTime currentTime)
            {
                return config.Frequency switch
                {
                    ScheduleFrequency.Daily => GetNextDailyExecution(config.ExecutionTime, currentTime),
                    ScheduleFrequency.Weekly => GetNextWeeklyExecution(config.ExecutionTime, currentTime),
                    ScheduleFrequency.Monthly => GetNextMonthlyExecution(config.ExecutionTime, currentTime),
                    ScheduleFrequency.Hourly => currentTime.AddHours(1),
                    _ => currentTime.Add(config.ExecutionTime)
                };
            }

            private DateTime GetNextDailyExecution(TimeSpan targetTime, DateTime currentTime)
            {
                var targetDateTime = currentTime.Date + targetTime;
                return currentTime > targetDateTime ? targetDateTime.AddDays(1) : targetDateTime;
            }

            private DateTime GetNextWeeklyExecution(TimeSpan targetTime, DateTime currentTime)
            {
                var targetDateTime = currentTime.Date + targetTime;
                var daysUntilMonday = ((int)DayOfWeek.Monday - (int)currentTime.DayOfWeek + 7) % 7;
                return targetDateTime.AddDays(daysUntilMonday == 0 && currentTime > targetDateTime ? 7 : daysUntilMonday);
            }

            private DateTime GetNextMonthlyExecution(TimeSpan targetTime, DateTime currentTime)
            {
                var targetDateTime = new DateTime(currentTime.Year, currentTime.Month, 1) + targetTime;
                return currentTime > targetDateTime ? targetDateTime.AddMonths(1) : targetDateTime;
            }

            /// <summary>
            /// 取得執行週期
            /// </summary>
            /// <param name="frequency"></param>
            /// <returns></returns>
            private TimeSpan GetPeriod(ScheduleFrequency frequency)
            {
                return frequency switch
                {
                    ScheduleFrequency.Daily => TimeSpan.FromDays(1),
                    ScheduleFrequency.Weekly => TimeSpan.FromDays(7),
                    ScheduleFrequency.Monthly => TimeSpan.FromDays(30), // 近似值
                    ScheduleFrequency.Hourly => TimeSpan.FromHours(1),
                    ScheduleFrequency.Minutely => TimeSpan.FromMinutes(1),   // 新增

                    _ => TimeSpan.FromDays(1)
                };
            }

            // 執行任務的核心方法
            private async Task<TaskResult> ExecuteTaskAsync(TaskConfiguration config)
            {

                try
                {
                    if (!_taskExecutors.TryGetValue(config.TaskType, out var executor))
                    {
                        throw new InvalidOperationException($"No executor found for task type: {config.TaskType}");
                    }

                    var result = await executor(config);

                    // 更新執行資訊
                    config.LastExecutionTime = DateTime.UtcNow;
                    config.ExecutionCount++;

                    TaskCompleted?.Invoke(config, result);
                    return result;
                }
                catch (Exception ex)
                {
                    var result = new TaskResult
                    {
                        Success = false,
                        Message = ex.Message,
                        ExecutionTime = DateTime.UtcNow,
                        Exception = ex
                    };

                    TaskFailed?.Invoke(config, ex);
                    return result;
                }
            }
            /// <summary>
            /// 自定義創建需固定時間維護的資料
            /// </summary>
            /// <param name="config"></param>
            /// <returns></returns>
            private async Task<TaskResult> ExecuteCustomTaskAsync(TaskConfiguration config)
            {
                // 從參數中取得自定義邏輯
                if (config.Parameters.TryGetValue("CustomAction", out var actionObj) &&
                    actionObj is Func<Task<TaskResult>> customAction)
                {
                    return await customAction();
                }

                return new TaskResult
                {
                    Success = true,
                    Message = "Custom task completed",
                    ExecutionTime = DateTime.UtcNow
                };
            }

            // 釋放資源
            public void Dispose()
            {
                StopAllTasks();
            }
      
        }
        /// <summary>
        /// 寄出每日健康元件系統總結
        /// </summary>
        /// <returns></returns>
        public static async Task<TaskResult> SendElementEmailAsync()
        {
            try
            {
                if (Application.OpenForms.Count > 0)
                {
                    var form = Application.OpenForms[0];
                    form.Invoke(() =>
                    {
                        MessageBox.Show("正在執行 SendElementEmailAsync() 任務", "排程提醒");
                    });
                }
                List<string> allUser = email.GetAllUserEmailsAsync();
                List<string> allUser_line = email.GetAllUserLineAsync();

                MessageSubjectType selectedType = MessageSubjectType.DailyHealthStatus;
                string subject = MessageSubjectHelper.GetSubject(selectedType);
                string body1 = Notify_Message.GenerateYellowComponentGroupSummary();
                string body2 = Notify_Message.GenerateRedComponentGroupSummary();
                string body = body1 + "\n----------------------------------------------------------------\n\n" + body2;

                var mailInfo = new MessageInfo
                {
                    Receivers = allUser,
                    Subject = subject,
                    Body = body
                };
                var lineInfo = new MessageInfo
                {
                    Receivers = allUser_line,
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

                await SendLineNotificationAsync(lineInfo);

                return new TaskResult
                {
                    Success = true,
                    Message = "測試郵件寄送成功",
                    ExecutionTime = DateTime.UtcNow
                };

            }
            catch (Exception ex)
            {
                return new TaskResult
                {
                    Success = false,
                    Message = $"寄送失敗：{ex.Message}",
                    ExecutionTime = DateTime.UtcNow,
                    Exception = ex
                };
            }
        }

        /// <summary>
        /// 寄出尚未排除警告的總結
        /// </summary>
        /// <returns></returns>
        public static async Task<TaskResult> SendDailyAlarmSummaryEmailAsync()
        {
            if (Application.OpenForms.Count > 0)
            {
                var form = Application.OpenForms[0];
                form.Invoke(() =>
                {
                    //MessageBox.Show("正在執行 SendDailyAlarmSummaryEmailAsync() 任務", "排程提醒");
                    Debug.WriteLine("開始執行每日警告提示");
                });
            }
            using var db = new ApplicationDB();
            var now = DateTime.UtcNow;

            // 查詢所有尚未排除的警告，且今天尚未發送提醒過
            var histories = db.AlarmHistories
                .Include(h => h.Alarm)  // 載入關聯 Alarm 資料
                .Where(h => h.EndTime == null && h.RecordTime != now)
                .ToList();

            // 如果沒有未排除的警告，不需要發送
            if (!histories.Any())
            {

                return new TaskResult
                {
                    Success = false,
                    Message = $"無警告需要寄送",
                    ExecutionTime = DateTime.UtcNow,
                };
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
                List<string> User_line = email.GetUserEmails(users);

                // 建立該使用者對應的彙總信件內容
                var body = BuildEmailBody(group.ToList());

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
                var lineInfo = new MessageInfo
                {
                    Receivers = User_line,
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
                await SendLineNotificationAsync(lineInfo);

                // 更新每筆紀錄的發送時間與次數
                foreach (var h in group)
                {
                    h.RecordTime = DateTime.UtcNow;
                    h.Records += 1;
                }
            }

            // 寫回資料庫
            if (emailSent)
            {
                db.SaveChanges();
            }
            return new TaskResult
            {
                Success = true,
                Message = "郵件寄送成功",
                ExecutionTime = DateTime.UtcNow
            };

        }
       
        /// <summary>
        /// 建立 多筆警告信件內容及摘要(將多筆警告合併為一封摘要)
        /// </summary>
        /// <param name="alarms"></param>
        /// <returns></returns>
        public static string BuildEmailBody(List<AlarmHistory> alarms)
        {
            var sb = new StringBuilder();
            sb.AppendLine("📌 以下為尚未排除的警告摘要：\n");

            foreach (var h in alarms)
            {
                sb.AppendLine($"故障地址：{h.Alarm.address}");                       // 警告位置
                sb.AppendLine($"警告描述：{h.Alarm.Description}");                  // 更換料件
                sb.AppendLine($"錯誤內容：{h.Alarm.Error}");                        // 錯誤內容
                sb.AppendLine($"錯誤可能原因：{h.Alarm.Possible}");                     // 錯誤內容
                sb.AppendLine($"錯誤維修方式：{h.Alarm.Repair_steps}");                     // 錯誤內容
                sb.AppendLine($"發生時間：{h.StartTime:yyyy-MM-dd HH:mm}");   // 發生時間
                sb.AppendLine($"已發送次數：{h.Records + 1}");                 // 預估下一次寄送是第幾次
                sb.AppendLine("-------------------------------------------");
            }

            return sb.ToString();
        }
        /// <summary>
        /// 定時紀錄機台參數
        /// </summary>
        /// <returns></returns>
        public static async Task<TaskResult> RecordCurrentParameterSnapshotAsync(ScheduleFrequency config)
        {
            try
            {
                using var db = new ApplicationDB();
                var now = DateTime.UtcNow;
                var roundedTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Utc);
                TimeSpan period = config switch
                {
                    ScheduleFrequency.Minutely => TimeSpan.FromMinutes(1),
                    ScheduleFrequency.Hourly => TimeSpan.FromHours(1),
                    ScheduleFrequency.Daily => TimeSpan.FromDays(1),
                    ScheduleFrequency.Weekly => TimeSpan.FromDays(7),
                    ScheduleFrequency.Monthly => TimeSpan.FromDays(30), // 建議自行根據月份再計算
                    _ => throw new NotSupportedException($"不支援的排程頻率：{config}")
                };

                var previousTime = roundedTime - period;
                var parameters = db.MachineParameters.ToList();


                foreach (var param in parameters)
                {
                    // 防止重複寫入

                    bool alreadyExists = db.MachineParameterHistoryRecodes.Any(r =>
                r.MachineParameterId == param.Id &&
                r.StartTime == roundedTime &&
                r.PeriodTag.EndsWith("_Metric"));

                    if (alreadyExists)
                        continue;

                    int currentValue = DBfunction.Get_Machine_History_NumericValue(param.Name) +
                                       DBfunction.Get_Machine_number(param.Name);

                    var lastMetricRecord = db.MachineParameterHistoryRecodes
                        .FirstOrDefault(r =>
                            r.MachineParameterId == param.Id &&
                            r.StartTime == previousTime &&
                            r.PeriodTag.EndsWith("_Metric"));

                    if (lastMetricRecord == null)
                    {
                        db.MachineParameterHistoryRecodes.Add(new MachineParameterHistoryRecode
                        {
                            MachineParameterId = param.Id,
                            StartTime = roundedTime,
                            EndTime = roundedTime.Add(period).AddSeconds(-1),
                            History_NumericValue = currentValue,
                            ResetBy = "SystemRecord_Metric",
                            PeriodTag = $"{roundedTime:yyyyMMdd_HHmm}_{config}"
                        });

                        continue;
                    }
                    // === 英制（若支援） ===
                    if (!string.IsNullOrWhiteSpace(param.Read_addr) && param.Imperial_transfer.HasValue)
                    {
                        double imperialFactor = param.Imperial_transfer.Value / param.Unit_transfer;
                        int? currentImperial = param.now_NumericValue.HasValue
                            ? (int?)(param.now_NumericValue.Value * imperialFactor)
                            : null;

                        var lastImperialRecord = db.MachineParameterHistoryRecodes
                            .FirstOrDefault(r =>
                                r.MachineParameterId == param.Id &&
                                r.StartTime == previousTime &&
                                r.PeriodTag.EndsWith("_Imperial"));

                        int? deltaImperial = null;
                        if (lastImperialRecord != null && currentImperial.HasValue)
                        {
                            deltaImperial = currentImperial.Value;
                        }

                        db.MachineParameterHistoryRecodes.Add(new MachineParameterHistoryRecode
                        {
                            MachineParameterId = param.Id,
                            StartTime = roundedTime,
                            EndTime = roundedTime.Add(period).AddSeconds(-1),
                            History_NumericValue = deltaImperial,
                            ResetBy = "SystemRecord_Imperial",
                            PeriodTag = $"{roundedTime:yyyyMMdd_HHmm}"
                        });
                    }
                }
                await db.SaveChangesAsync();
                MessageBox.Show("成功");
                return new TaskResult
                {
                    Success = true,
                    Message = $"每分鐘快照完成，共處理 {parameters.Count} 筆參數",
                    ExecutionTime = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("失敗");

                return new TaskResult
                {
                    Success = false,
                    Message = $"記錄參數快照失敗：{ex.Message}",
                    ExecutionTime = DateTime.UtcNow
                };

            }
        }



    }
}
