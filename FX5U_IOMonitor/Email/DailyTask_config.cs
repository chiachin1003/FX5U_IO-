using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FX5U_IOMonitor.Email.Notify_Message;
using static FX5U_IOMonitor.Email.Send_mode;
using System.Diagnostics;


namespace FX5U_IOMonitor.Email
{

    internal class DailyTask_config
    {
        public enum ScheduleTaskType { CustomTask }

        public enum ScheduleFrequency { Minutely, Hourly, Daily, Weekly, Monthly, Custom }

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

        public class TaskResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public DateTime ExecutionTime { get; set; }
            public Exception Exception { get; set; }
        }

        public delegate Task<TaskResult> TaskExecutor(TaskConfiguration config);

        public class FlexibleScheduler
        {
            private readonly Dictionary<string, System.Threading.Timer> _timers = new();
            private readonly Dictionary<ScheduleTaskType, TaskExecutor> _taskExecutors = new();
            private readonly List<TaskConfiguration> _taskConfigurations = new();

            public event Action<TaskConfiguration, TaskResult> TaskCompleted;
            public event Action<TaskConfiguration, Exception> TaskFailed;

            public FlexibleScheduler() => _taskExecutors[ScheduleTaskType.CustomTask] = ExecuteCustomTaskAsync;

            public void AddTask(TaskConfiguration config)
            {
                if (config == null || string.IsNullOrEmpty(config.TaskName)) return;
                _taskConfigurations.Add(config);
                if (config.IsEnabled) StartTask(config);
            }

            public void StartTask(TaskConfiguration config)
            {
                if (!config.IsEnabled) return;
                StopTask(config);

                var delay = ShouldExecuteImmediately(config)
                       ? TimeSpan.Zero                    // 立即執行一次
                       : GetInitialDelay(config);        // 等到下一次時間

                var period = GetPeriod(config.Frequency);
                var timer = new System.Threading.Timer(async _ =>
                {
                    config.LastExecutionTime = DateTime.UtcNow;  // 更新執行時間
                    await ExecuteTaskAsync(config);
                }, null, delay, period);

                _timers[config.TaskName] = timer;
            }

            public void StopTask(TaskConfiguration config)
            {
                if (_timers.TryGetValue(config.TaskName, out var timer))
                {
                    timer?.Dispose();
                    _timers.Remove(config.TaskName);
                }
            }

            public List<TaskConfiguration> GetAllTasks() => _taskConfigurations.ToList();

            private bool ShouldExecuteImmediately(TaskConfiguration config)
            {
                var now = DateTime.UtcNow;
                var todayExecTime = now.Date + config.ExecutionTime;

                // ➤ 如果尚未執行過 或 尚未到今天的排定時間 → 不立即執行
                if (!config.LastExecutionTime.HasValue)
                    return false;

                // ➤ 今天已執行過 → 不需要立即執行
                if (config.LastExecutionTime.Value.Date == now.Date &&
                    config.LastExecutionTime.Value >= todayExecTime)
                    return false;

                // ➤ 現在已經超過排定時間，且今天尚未執行 → 應立即執行
                return now > todayExecTime;
            }

            private TimeSpan GetInitialDelay(TaskConfiguration config)
            {
                var now = DateTime.UtcNow;
                var nextExecutionTime = GetNextExecutionTime(config, now);
                var delay = nextExecutionTime - now;

                return delay > TimeSpan.Zero ? delay : TimeSpan.Zero;
            }

            private DateTime GetNextExecutionTime(TaskConfiguration config, DateTime currentTime) => config.Frequency switch
            {
                ScheduleFrequency.Daily => currentTime.Date + config.ExecutionTime,
                ScheduleFrequency.Weekly => currentTime.Date.AddDays(7 - (int)currentTime.DayOfWeek) + config.ExecutionTime,
                ScheduleFrequency.Monthly => new DateTime(currentTime.Year, currentTime.Month, 1).AddMonths(1) + config.ExecutionTime,
                ScheduleFrequency.Hourly => currentTime.AddHours(1),
                _ => currentTime.Add(config.ExecutionTime)
            };

            private TimeSpan GetPeriod(ScheduleFrequency freq) => freq switch
            {
                ScheduleFrequency.Minutely => TimeSpan.FromMinutes(1),
                ScheduleFrequency.Hourly => TimeSpan.FromHours(1),
                ScheduleFrequency.Daily => TimeSpan.FromDays(1),
                ScheduleFrequency.Weekly => TimeSpan.FromDays(7),
                ScheduleFrequency.Monthly => TimeSpan.FromDays(30),
                _ => TimeSpan.FromMinutes(1)
            };

            private async Task<TaskResult> ExecuteTaskAsync(TaskConfiguration config)
            {

                try
                {
                    // ⏳ 在執行任務前補齊歷史紀錄
                    if (config.Parameters.TryGetValue("AutoFillHistory", out var fill) && fill is bool doFill && doFill)
                    {
                        var fillResult = await RecordAllMissingParameterSnapshotsAsync(config.Frequency);
                        Debug.WriteLine($"補齊歷史資料結果：{fillResult.Message}");
                    }

                    if (!_taskExecutors.TryGetValue(config.TaskType, out var executor))
                        throw new InvalidOperationException("No executor found for task type");

                    var result = await executor(config);
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

        private async Task<TaskResult> ExecuteCustomTaskAsync(TaskConfiguration config)
            {
                if (config.Parameters.TryGetValue("CustomAction", out var act) && act is Func<Task<TaskResult>> action)
                    return await action();
                return new TaskResult { Success = true, Message = "No action", ExecutionTime = DateTime.UtcNow };
            }
        }

        /// <summary>
        /// 自動補齊所有缺漏的排程週期紀錄
        /// </summary>
        public static async Task<TaskResult> RecordAllMissingParameterSnapshotsAsync(ScheduleFrequency frequency)
        {
            using var db = new ApplicationDB();

            var lastTime = db.MachineParameterHistoryRecodes
                .Where(r => r.PeriodTag.EndsWith("_Metric") && r.PeriodTag.Contains(frequency.ToString()))
                .Max(r => (DateTime?)r.StartTime);

            DateTime startCursor = lastTime.HasValue
                ? GetNextPeriodStartAfter(lastTime.Value, frequency)
                : GetPeriodStart(DateTime.UtcNow, frequency);

            DateTime now = DateTime.UtcNow;
            int count = 0;

            while (startCursor < now)
            {
                DateTime end = GetPeriodEnd(startCursor, frequency);
                await RecordCurrentParameterSnapshotInternalAsync(startCursor, end, frequency);
                startCursor = GetNextPeriodStartAfter(startCursor, frequency);
                count++;
            }

            return new TaskResult
            {
                Success = true,
                Message = $"成功補齊 {count} 筆 {frequency} 快照",
                ExecutionTime = DateTime.UtcNow
            };
        }

        private static DateTime GetPeriodStart(DateTime time, ScheduleFrequency freq) => freq switch
        {
            ScheduleFrequency.Minutely => new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0, DateTimeKind.Utc),
            ScheduleFrequency.Hourly => new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0, DateTimeKind.Utc),
            ScheduleFrequency.Daily => time.Date,
            ScheduleFrequency.Weekly => time.Date.AddDays(-(int)time.DayOfWeek),
            ScheduleFrequency.Monthly => new DateTime(time.Year, time.Month, 1, 0, 0, 0, DateTimeKind.Utc),
            _ => throw new NotSupportedException()
        };

        private static DateTime GetNextPeriodStartAfter(DateTime time, ScheduleFrequency freq) => freq switch
        {
            ScheduleFrequency.Minutely => time.AddMinutes(1),
            ScheduleFrequency.Hourly => time.AddHours(1),
            ScheduleFrequency.Daily => time.AddDays(1),
            ScheduleFrequency.Weekly => time.AddDays(7),
            ScheduleFrequency.Monthly => time.AddMonths(1),
            _ => throw new NotSupportedException()
        };

        private static DateTime GetPeriodEnd(DateTime start, ScheduleFrequency freq) => freq switch
        {
            ScheduleFrequency.Minutely => start.AddSeconds(59),
            ScheduleFrequency.Hourly => start.AddMinutes(59).AddSeconds(59),
            ScheduleFrequency.Daily => start.AddHours(23).AddMinutes(59).AddSeconds(59),
            ScheduleFrequency.Weekly => start.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59),
            ScheduleFrequency.Monthly => new DateTime(start.Year, start.Month, DateTime.DaysInMonth(start.Year, start.Month), 23, 59, 59, DateTimeKind.Utc),
            _ => throw new NotSupportedException()
        };

        private static async Task RecordCurrentParameterSnapshotInternalAsync(DateTime startTime, DateTime endTime, ScheduleFrequency config)
        {
            using var db = new ApplicationDB();
            var parameters = db.MachineParameters.ToList();

            foreach (var param in parameters)
            {
                bool alreadyExists = db.MachineParameterHistoryRecodes.Any(r =>
                    r.MachineParameterId == param.Id &&
                    r.StartTime == startTime &&
                    r.PeriodTag.EndsWith("_Metric"));

                if (alreadyExists)
                    continue;

                int currentValue = DBfunction.Get_Machine_History_NumericValue(param.Name) +
                                    DBfunction.Get_Machine_number(param.Name);

                db.MachineParameterHistoryRecodes.Add(new MachineParameterHistoryRecode
                {
                    MachineParameterId = param.Id,
                    StartTime = startTime,
                    EndTime = endTime,
                    History_NumericValue = currentValue,
                    ResetBy = "SystemRecord_Metric",
                    PeriodTag = $"{startTime:yyyyMMdd_HHmm}_{config}"
                });

                if (!string.IsNullOrWhiteSpace(param.Read_addr) && param.Imperial_transfer.HasValue)
                {
                    double imperialFactor = param.Imperial_transfer.Value / param.Unit_transfer;
                    int? currentImperial = param.now_NumericValue.HasValue
                        ? (int?)(param.now_NumericValue.Value * imperialFactor)
                        : null;

                    db.MachineParameterHistoryRecodes.Add(new MachineParameterHistoryRecode
                    {
                        MachineParameterId = param.Id,
                        StartTime = startTime,
                        EndTime = endTime,
                        History_NumericValue = currentImperial,
                        ResetBy = "SystemRecord_Imperial",
                        PeriodTag = $"{startTime:yyyyMMdd_HHmm}_{config}_Imperial"
                    });
                }
            }

            await db.SaveChangesAsync();
        }
    }
    
}

//internal class DailyTask_config
//{
//    // 定義任務類型枚舉
//    public enum ScheduleTaskType
//    {
//        CustomTask
//    }
//    // 定義排程頻率
//    public enum ScheduleFrequency
//    {
//        Minutely,   
//        Daily,
//        Weekly,
//        Monthly,
//        Hourly,
//        Custom
//    }

//    // 任務配置類別
//    public class TaskConfiguration
//    {
//        public ScheduleTaskType TaskType { get; set; }
//        public ScheduleFrequency Frequency { get; set; }
//        public TimeSpan ExecutionTime { get; set; }
//        public string TaskName { get; set; }
//        public bool IsEnabled { get; set; } = true;
//        public Dictionary<string, object> Parameters { get; set; } = new();
//        public DateTime? LastExecutionTime { get; set; }
//        public int ExecutionCount { get; set; }
//    }

//    // 任務執行結果
//    public class TaskResult
//    {
//        public bool Success { get; set; }
//        public string Message { get; set; }
//        public DateTime ExecutionTime { get; set; }
//        public Exception Exception { get; set; }
//    }

//    // 任務執行委派
//    public delegate Task<TaskResult> TaskExecutor(TaskConfiguration config);

//    // 主要的排程器類別
//    public class FlexibleScheduler
//    {
//        private readonly Dictionary<string, Timer> _timers = new();
//        private readonly Dictionary<ScheduleTaskType, TaskExecutor> _taskExecutors = new();
//        private readonly List<TaskConfiguration> _taskConfigurations = new();

//        public event Action<TaskConfiguration, TaskResult> TaskCompleted;
//        public event Action<TaskConfiguration, Exception> TaskFailed;

//        public FlexibleScheduler()
//        {
//            InitializeDefaultTaskExecutors();
//        }

//        // 初始化預設任務執行器
//        private void InitializeDefaultTaskExecutors()
//        {
//            _taskExecutors[ScheduleTaskType.CustomTask] = ExecuteCustomTaskAsync;
//        }

//        // 註冊自定義任務執行器
//        public void RegisterTaskExecutor(ScheduleTaskType taskType, TaskExecutor executor)
//        {
//            _taskExecutors[taskType] = executor;
//        }

//        // 新增任務配置
//        public void AddTask(TaskConfiguration config)
//        {
//            if (config == null) throw new ArgumentNullException(nameof(config));
//            if (string.IsNullOrEmpty(config.TaskName)) throw new ArgumentException("TaskName cannot be null or empty");

//            _taskConfigurations.Add(config);

//            if (config.IsEnabled)
//            {
//                StartTask(config);
//            }
//        }

//        // 移除任務
//        public void RemoveTask(string taskName)
//        {
//            var config = _taskConfigurations.Find(t => t.TaskName == taskName);
//            if (config != null)
//            {
//                StopTask(config);
//                _taskConfigurations.Remove(config);
//            }
//        }

//        // 啟動特定任務
//        public void StartTask(TaskConfiguration config)
//        {
//            if (!config.IsEnabled) return;

//            StopTask(config); // 先停止現有的計時器

//            //if (ShouldExecuteImmediately(config))
//            //{
//            //    Task.Run(async () => await ExecuteTaskAsync(config));
//            //}

//            var delay = ShouldExecuteImmediately(config)
//                ? GetPeriod(config.Frequency) // 排程下次執行就好
//                : GetInitialDelay(config);
//            var period = GetPeriod(config.Frequency);

//            var timer = new Timer(async _ => await ExecuteTaskAsync(config), null, delay, period);
//            _timers[config.TaskName] = timer;
//        }

//        // 停止特定任務
//        public void StopTask(TaskConfiguration config)
//        {
//            if (_timers.TryGetValue(config.TaskName, out var timer))
//            {
//                timer?.Dispose();
//                _timers.Remove(config.TaskName);
//            }
//        }

//        // 啟動所有任務
//        public void StartAllTasks()
//        {
//            foreach (var config in _taskConfigurations.Where(c => c.IsEnabled))
//            {
//                StartTask(config);
//            }
//        }

//        // 停止所有任務
//        public void StopAllTasks()
//        {
//            foreach (var timer in _timers.Values)
//            {
//                timer?.Dispose();
//            }
//            _timers.Clear();
//        }

//        // 手動執行任務
//        public async Task<TaskResult> ExecuteTaskManuallyAsync(string taskName)
//        {
//            var config = _taskConfigurations.Find(t => t.TaskName == taskName);
//            if (config == null)
//            {
//                return new TaskResult
//                {
//                    Success = false,
//                    Message = $"Task '{taskName}' not found",
//                    ExecutionTime = DateTime.UtcNow
//                };
//            }

//            return await ExecuteTaskAsync(config);
//        }

//        // 更新任務配置
//        public void UpdateTask(string taskName, Action<TaskConfiguration> updateAction)
//        {
//            var config = _taskConfigurations.Find(t => t.TaskName == taskName);
//            if (config != null)
//            {
//                updateAction(config);

//                if (config.IsEnabled)
//                {
//                    StartTask(config);
//                }
//                else
//                {
//                    StopTask(config);
//                }
//            }
//        }

//        // 取得任務狀態
//        public List<TaskConfiguration> GetAllTasks()
//        {
//            return _taskConfigurations.ToList();
//        }

//        /// <summary>
//        /// 檢查是否需要立即執行
//        /// </summary>
//        /// <param name="config"></param>
//        /// <returns></returns>
//        private bool ShouldExecuteImmediately(TaskConfiguration config)
//        {
//            if (!config.LastExecutionTime.HasValue)
//            {
//                return true;
//            }

//            var now = DateTime.UtcNow;
//            return config.Frequency switch
//            {
//                ScheduleFrequency.Daily => config.LastExecutionTime.Value.Date < now.Date,
//                ScheduleFrequency.Weekly => (now - config.LastExecutionTime.Value).TotalDays >= 7,
//                ScheduleFrequency.Monthly => config.LastExecutionTime.Value.Month != now.Month ||
//                                           config.LastExecutionTime.Value.Year != now.Year,
//                ScheduleFrequency.Hourly => (now - config.LastExecutionTime.Value).TotalHours >= 1,
//                _ => false
//            };
//        }

//        // 計算初始延遲時間
//        private TimeSpan GetInitialDelay(TaskConfiguration config)
//        {
//            var now = DateTime.UtcNow;
//            var targetDateTime = GetNextExecutionTime(config, now);
//            return targetDateTime - now;
//        }

//        /// <summary>
//        /// 計算下次執行時間
//        /// </summary>
//        /// <param name="config"></param>
//        /// <param name="currentTime"></param>
//        /// <returns></returns>
//        private DateTime GetNextExecutionTime(TaskConfiguration config, DateTime currentTime)
//        {
//            return config.Frequency switch
//            {
//                ScheduleFrequency.Daily => GetNextDailyExecution(config.ExecutionTime, currentTime),
//                ScheduleFrequency.Weekly => GetNextWeeklyExecution(config.ExecutionTime, currentTime),
//                ScheduleFrequency.Monthly => GetNextMonthlyExecution(config.ExecutionTime, currentTime),
//                ScheduleFrequency.Hourly => currentTime.AddHours(1),
//                _ => currentTime.Add(config.ExecutionTime)
//            };
//        }

//        private DateTime GetNextDailyExecution(TimeSpan targetTime, DateTime currentTime)
//        {
//            var targetDateTime = currentTime.Date + targetTime;
//            return currentTime > targetDateTime ? targetDateTime.AddDays(1) : targetDateTime;
//        }

//        private DateTime GetNextWeeklyExecution(TimeSpan targetTime, DateTime currentTime)
//        {
//            var targetDateTime = currentTime.Date + targetTime;
//            var daysUntilMonday = ((int)DayOfWeek.Monday - (int)currentTime.DayOfWeek + 7) % 7;
//            return targetDateTime.AddDays(daysUntilMonday == 0 && currentTime > targetDateTime ? 7 : daysUntilMonday);
//        }

//        private DateTime GetNextMonthlyExecution(TimeSpan targetTime, DateTime currentTime)
//        {
//            var targetDateTime = new DateTime(currentTime.Year, currentTime.Month, 1) + targetTime;
//            return currentTime > targetDateTime ? targetDateTime.AddMonths(1) : targetDateTime;
//        }

//        /// <summary>
//        /// 取得執行週期
//        /// </summary>
//        /// <param name="frequency"></param>
//        /// <returns></returns>
//        private TimeSpan GetPeriod(ScheduleFrequency frequency)
//        {
//            return frequency switch
//            {
//                ScheduleFrequency.Daily => TimeSpan.FromDays(1),
//                ScheduleFrequency.Weekly => TimeSpan.FromDays(7),
//                ScheduleFrequency.Monthly => TimeSpan.FromDays(30), // 近似值
//                ScheduleFrequency.Hourly => TimeSpan.FromHours(1),
//                ScheduleFrequency.Minutely => TimeSpan.FromMinutes(1),   // 新增

//                _ => TimeSpan.FromDays(1)
//            };
//        }

//        // 執行任務的核心方法
//        private async Task<TaskResult> ExecuteTaskAsync(TaskConfiguration config)
//        {

//            try
//            {
//                if (!_taskExecutors.TryGetValue(config.TaskType, out var executor))
//                {
//                    throw new InvalidOperationException($"No executor found for task type: {config.TaskType}");
//                }

//                var result = await executor(config);

//                // 更新執行資訊
//                config.LastExecutionTime = DateTime.UtcNow;
//                config.ExecutionCount++;

//                TaskCompleted?.Invoke(config, result);
//                return result;
//            }
//            catch (Exception ex)
//            {
//                var result = new TaskResult
//                {
//                    Success = false,
//                    Message = ex.Message,
//                    ExecutionTime = DateTime.UtcNow,
//                    Exception = ex
//                };

//                TaskFailed?.Invoke(config, ex);
//                return result;
//            }
//        }
//        /// <summary>
//        /// 自定義創建需固定時間維護的資料
//        /// </summary>
//        /// <param name="config"></param>
//        /// <returns></returns>
//        private async Task<TaskResult> ExecuteCustomTaskAsync(TaskConfiguration config)
//        {
//            // 從參數中取得自定義邏輯
//            if (config.Parameters.TryGetValue("CustomAction", out var actionObj) &&
//                actionObj is Func<Task<TaskResult>> customAction)
//            {
//                return await customAction();
//            }

//            return new TaskResult
//            {
//                Success = true,
//                Message = "Custom task completed",
//                ExecutionTime = DateTime.UtcNow
//            };
//        }

//        // 釋放資源
//        public void Dispose()
//        {
//            StopAllTasks();
//        }

//    }


//}
