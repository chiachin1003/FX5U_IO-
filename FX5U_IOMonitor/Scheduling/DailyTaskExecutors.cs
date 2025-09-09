using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Message;
using FX5U_IOMonitor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FX5U_IOMonitor.Message.Notify_Message;
using static FX5U_IOMonitor.Message.Send_mode;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;

namespace FX5U_IOMonitor.Scheduling
{
    internal class DailyTaskExecutors
    {
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
                        Debug.WriteLine("開始執行元件壽命提示");
                        //MessageBox.Show("正在執行 SendElementEmailAsync() 任務", "排程提醒");
                    });
                }

                List<string> allUser = Message_function.GetAllUserEmails();
                List<string> allUser_line = Message_function.GetAllUserLineAsync();

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
                // 取得收件者 Message_function 清單（支援 , 或 ; 分隔）
                var users = group.Key.Split(',', ';', StringSplitOptions.RemoveEmptyEntries)
                                     .Select(x => x.Trim())
                                     .ToList();
                List<string> allUser = Message_function.GetUserEmails(users);
                List<string> User_line = Message_function.GetUserLine(users);

                // 建立該使用者對應的彙總信件內容
                var body = BuildDateAlarmBody(group.ToList());

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
        public static string BuildDateAlarmBody(List<AlarmHistory> alarms)
        {
            var sb = new StringBuilder();
            sb.AppendLine(LanguageManager.Translate("Alarm_Message_UnresolvedSummary") +"\n");

            foreach (var h in alarms)
            {
                sb.AppendLine(LanguageManager.Translate("Alarm_Message_Error_Address") + $"：{h.Alarm.address}");                       // 警告位置
                sb.AppendLine(LanguageManager.Translate("Alarm_Message_Error_Item")+$"：{h.Alarm.Description}");                  // 更換料件
                sb.AppendLine(LanguageManager.Translate("Alarm_Message_Error_Message")+$"：{h.Alarm.Error}");                        // 錯誤內容
                sb.AppendLine(LanguageManager.Translate("Alarm_Message_Possible_Cause") + $"：{h.Alarm.Possible}");                     // 錯誤內容
                sb.AppendLine(LanguageManager.Translate("Alarm_Message_Repair_Steps") + $"：{h.Alarm.Repair_steps}");                     // 錯誤內容
                sb.AppendLine(LanguageManager.Translate("Alarm_Message_SentTime") + $"：{h.StartTime:yyyy-MM-dd HH:mm}");   // 發生時間
                sb.AppendLine(LanguageManager.Translate("Alarm_Message_SentCount") + $"：{h.Records + 1}");                 // 預估下一次寄送是第幾次
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

                DateTime roundedStartTime;
                DateTime roundedEndTime;

                bool useDefaultZero = false;

                switch (config)
                {
                    case ScheduleFrequency.Minutely:
                        var lastMinute = now.AddMinutes(-1);
                        roundedStartTime = new DateTime(lastMinute.Year, lastMinute.Month, lastMinute.Day, lastMinute.Hour, lastMinute.Minute, 0, DateTimeKind.Utc);
                        roundedEndTime = roundedStartTime.AddSeconds(59);
                        break;
                    case ScheduleFrequency.Hourly:
                        var lastHour = now.AddHours(-1);
                        roundedStartTime = new DateTime(lastHour.Year, lastHour.Month, lastHour.Day, lastHour.Hour, 0, 0, DateTimeKind.Utc);
                        roundedEndTime = roundedStartTime.AddMinutes(59).AddSeconds(59);
                        break;
                    case ScheduleFrequency.Daily:
                        var yesterday = now.Date.AddDays(-1);
                        roundedStartTime = yesterday;
                        roundedEndTime = yesterday.AddHours(23).AddMinutes(59).AddSeconds(59);
                        break;
                    case ScheduleFrequency.Weekly:
                        int daysToLastMonday = ((int)now.DayOfWeek + 6) % 7 + 7; // 上週一
                        roundedStartTime = now.Date.AddDays(-daysToLastMonday);
                        roundedEndTime = roundedStartTime.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);
                        break;
                    case ScheduleFrequency.Monthly:
                        var prevMonth = now.AddMonths(-1);
                        var monthStart = new DateTime(prevMonth.Year, prevMonth.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                        var lastDayPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
                        var monthEnd = new DateTime(prevMonth.Year, prevMonth.Month, lastDayPrevMonth, 23, 59, 59, DateTimeKind.Utc);

                        // 查出最早參數建立時間
                        DateTime earliestParamTime = db.MachineParameters.Min(p => p.CreatedAt).ToUniversalTime();

                        if (monthEnd < earliestParamTime)
                        {
                            //  記錄值為 0 的初始化
                            roundedStartTime = monthStart;
                            roundedEndTime = monthEnd;
                            useDefaultZero = true;
                        }
                        else
                        {
                            // 正常紀錄
                            roundedStartTime = monthStart < earliestParamTime ? earliestParamTime : monthStart;
                            roundedEndTime = monthEnd;
                        }
                        break; 
                    default:
                        throw new NotSupportedException($"不支援的排程頻率：{config}");
                }

                string currentPeriodTag = $"{roundedStartTime:yyyyMMdd_HHmm}_{config}_Metric";

                // 查詢最近的相同頻率紀錄（只看 Metric 的）
                var latestRecord = db.MachineParameterHistoryRecodes
                                     .Where(r => r.PeriodTag == currentPeriodTag)
                                     .OrderByDescending(r => r.StartTime)
                                     .FirstOrDefault();

                if (latestRecord != null)
                {
                    if (latestRecord.PeriodTag == currentPeriodTag)
                    {
                        return new TaskResult
                        {
                            Success = true,
                            Message = $"⏳ 已存在相同週期快照（{currentPeriodTag}），無需重複紀錄。",
                            ExecutionTime = DateTime.UtcNow
                        };
                    }
                    else if (latestRecord.StartTime >= roundedStartTime && DateTime.UtcNow <= roundedEndTime)
                    {
                        return new TaskResult
                        {
                            Success = true,
                            Message = $"✅ 最近快照（{latestRecord.PeriodTag}）仍在週期內，略過紀錄。",
                            ExecutionTime = DateTime.UtcNow
                        };
                    }
                    // 否則落後、超時 ➜ 繼續紀錄
                }

                var parameters = db.MachineParameters.ToList();

                foreach (var param in parameters)
                {
                    bool alreadyExists = db.MachineParameterHistoryRecodes.Any(r =>
                        r.MachineParameterId == param.Id &&
                        r.StartTime == roundedStartTime &&
                        r.PeriodTag.EndsWith("_Metric"));

                    if (alreadyExists)
                        continue;

                    if (param.Read_type == "bit")
                    {
                        // 取得目前的值：
                        int currentValue = useDefaultZero ? 0
                            : DBfunction.
                            Get_Machine_History_NumericValue(param.Machine_Name, param.Name) + DBfunction.Get_Machine_number(param.Machine_Name, param.Name);
                        //寫入紀錄
                        db.MachineParameterHistoryRecodes.Add(new MachineParameterHistoryRecode
                        {
                            MachineParameterId = param.Id,
                            StartTime = roundedStartTime,
                            EndTime = roundedEndTime,
                            History_NumericValue = currentValue,
                            ResetBy = "SystemRecord_Metric",
                            PeriodTag = $"{roundedStartTime:yyyyMMdd_HHmm}_{config}_Metric"
                        });
                    }

                    if (param.Read_type == "word")
                    {

                        // === 公制記錄（從 History_NumericValue 取得） ===
                        int currentValue = useDefaultZero ? 0
                            : DBfunction.Get_Machine_History_NumericValue(param.Machine_Name, param.Name);

                        db.MachineParameterHistoryRecodes.Add(new MachineParameterHistoryRecode
                        {
                            MachineParameterId = param.Id,
                            StartTime = roundedStartTime,
                            EndTime = roundedEndTime,
                            History_NumericValue = currentValue,
                            ResetBy = "SystemRecord_Metric",
                            PeriodTag = $"{roundedStartTime:yyyyMMdd_HHmm}_{config}_Metric"
                        });

                        // === 英制記錄（若支援，直接從 now_NumericValue 取得） ===
                        if (!string.IsNullOrWhiteSpace(param.Read_addr) && param.Imperial_transfer.HasValue)
                        {
                            // 直接從 now_NumericValue 取得英制數值，不再進行轉換計算
                            int? currentImperial = useDefaultZero ? 0
                                : (param.now_NumericValue.HasValue ? param.now_NumericValue.Value : null);

                            db.MachineParameterHistoryRecodes.Add(new MachineParameterHistoryRecode
                            {
                                MachineParameterId = param.Id,
                                StartTime = roundedStartTime,
                                EndTime = roundedEndTime,
                                History_NumericValue = currentImperial,
                                ResetBy = "SystemRecord_Imperial",
                                PeriodTag = $"{roundedStartTime:yyyyMMdd_HHmm}_{config}_Imperial"
                            });
                        }
                    }
                }

                await db.SaveChangesAsync();
                Message_Config.LogMessage($"✅ 成功紀錄 {config} 快照（{roundedStartTime:yyyy-MM-dd HH:mm} ~ {roundedEndTime:HH:mm}）");

                return new TaskResult
                {
                    Success = true,
                    Message = $"{config} 快照完成，共處理 {parameters.Count} 筆參數",
                    ExecutionTime = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                //MessageBox.Show("失敗");
                Message_Config.LogMessage($"紀錄失敗{ex.Message}");

                return new TaskResult
                {
                    Success = false,
                    Message = $"記錄參數快照失敗：{ex.Message}",
                    ExecutionTime = DateTime.UtcNow
                };

            }
        }

        public static async Task<TaskResult> RecordCurrentUtilizationRatedata(ScheduleFrequency config)
        {
            try
            {
                using var db = new ApplicationDB();
                var now = DateTime.UtcNow;

                DateTime roundedStartTime;
                DateTime roundedEndTime;

                bool useDefaultZero = false;

                switch (config)
                {
                    case ScheduleFrequency.Minutely:
                        var lastMinute = now.AddMinutes(-1);
                        roundedStartTime = new DateTime(lastMinute.Year, lastMinute.Month, lastMinute.Day, lastMinute.Hour, lastMinute.Minute, 0, DateTimeKind.Utc);
                        roundedEndTime = roundedStartTime.AddSeconds(59);
                        break;
                    case ScheduleFrequency.Hourly:
                        var lastHour = now.AddHours(-1);
                        roundedStartTime = new DateTime(lastHour.Year, lastHour.Month, lastHour.Day, lastHour.Hour, 0, 0, DateTimeKind.Utc);
                        roundedEndTime = roundedStartTime.AddMinutes(59).AddSeconds(59);
                        break;
                    case ScheduleFrequency.Daily:
                        var yesterday = now.Date.AddDays(-1);
                        roundedStartTime = yesterday;
                        roundedEndTime = yesterday.AddHours(23).AddMinutes(59).AddSeconds(59);
                        break;
                    case ScheduleFrequency.Weekly:
                        int daysToLastMonday = ((int)now.DayOfWeek + 6) % 7 + 7; // 上週一
                        roundedStartTime = now.Date.AddDays(-daysToLastMonday);
                        roundedEndTime = roundedStartTime.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);
                        break;
                    case ScheduleFrequency.Monthly:
                        var prevMonth = now.AddMonths(-1);
                        var monthStart = new DateTime(prevMonth.Year, prevMonth.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                        var lastDayPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
                        var monthEnd = new DateTime(prevMonth.Year, prevMonth.Month, lastDayPrevMonth, 23, 59, 59, DateTimeKind.Utc);

                        // 查出最早參數建立時間
                        DateTime earliestParamTime = db.MachineParameters.Min(p => p.CreatedAt).ToUniversalTime();

                        if (monthEnd < earliestParamTime)
                        {
                            //  記錄值為 0 的初始化
                            roundedStartTime = monthStart;
                            roundedEndTime = monthEnd;
                            useDefaultZero = true;
                        }
                        else
                        {
                            // 正常紀錄
                            roundedStartTime = monthStart < earliestParamTime ? earliestParamTime : monthStart;
                            roundedEndTime = monthEnd;
                        }
                        break;
                    default:
                        throw new NotSupportedException($"不支援的排程頻率：{config}");
                }

                string currentPeriodTag = $"{roundedStartTime:yyyyMMdd_HHmm}_{config}_Metric";

               
                var parameters = db.MachineParameters.ToList();

                foreach (var param in parameters)
                {
                    bool alreadyExists = db.MachineParameterHistoryRecodes.Any(r =>
                        r.MachineParameterId == param.Id &&
                        r.StartTime == roundedStartTime &&
                        r.PeriodTag.EndsWith("_Metric"));

                    if (alreadyExists)
                        continue;

                    if (param.Read_type == "bit" && param.Name == "Drill_plc_usetime")
                    {
                        // 取得目前的值：
                        int currentValue = useDefaultZero ? 0
                            : DBfunction.
                            Get_Machine_History_NumericValue(param.Machine_Name, param.Name) + DBfunction.Get_Machine_number(param.Machine_Name, param.Name);
                        //寫入紀錄
                        db.UtilizationRate.Add(new Utilization_Record
                        {
                            MachineParameterId = param.Id,
                            Machine_Name = param.Machine_Name,
                            StartTime = roundedStartTime,
                            EndTime = roundedEndTime,
                            History_NumericValue = currentValue,
                            Unit = "Metric",
                            PeriodTag = $"{roundedStartTime:yyyyMMdd_HHmm}"
                        });
                    }
                    if (param.Read_type == "word" && param.Read_view == 5)
                    {
                        int currentValue = useDefaultZero ? 0
                            : DBfunction.Get_Machine_NowValue(param.Machine_Name, param.Name);
                        int historyValue = useDefaultZero ? 0
                            : DBfunction.Get_Machine_History_NumericValue(param.Machine_Name, param.Name);
                        db.UtilizationRate.Add(new Utilization_Record
                        {
                            MachineParameterId = param.Id,
                            Machine_Name = param.Machine_Name,
                            StartTime = roundedStartTime,
                            EndTime = roundedEndTime,
                            History_NumericValue = ( currentValue + historyValue ),
                            Unit = "Metric",
                            PeriodTag = $"{roundedStartTime:yyyyMMdd_HHmm}"
                        });

                    }
                }

                await db.SaveChangesAsync();

                return new TaskResult
                {
                    Success = true,
                    Message = $"{config} 快照完成，共處理 {parameters.Count} 筆參數",
                    ExecutionTime = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                //MessageBox.Show("失敗");
                Message_Config.LogMessage($"紀錄失敗{ex.Message}");

                return new TaskResult
                {
                    Success = false,
                    Message = $"記錄參數快照失敗：{ex.Message}",
                    ExecutionTime = DateTime.UtcNow
                };

            }
        }



        public static (string Subject, string Body) BuildSingleAlarmMessage(
            string machineName,
            string partNumber,
            List<string> addressList,
            string faultLocation,
            List<string> possibleReasons,
            List<string> suggestions)
        {
            var subject = MessageSubjectHelper.GetSubject(MessageSubjectType.TriggeredAlarm);

            string reasonText = possibleReasons != null && possibleReasons.Count > 0
                ? string.Join(Environment.NewLine, possibleReasons.Select(r => "- " + r))
                : "- （尚未提供）";

            string suggestionText = suggestions != null && suggestions.Count > 0
                ? string.Join(Environment.NewLine, suggestions.Select((s, i) => $"{i + 1}. {s}"))
                : "（尚未提供建議）";

            string body = $@"
            {LanguageManager.Translate("Alarm_Message_Error_Warning")}

            📣 {LanguageManager.Translate("Alarm_Message_SentTime")}：{DateTime.Now:yyyy/MM/dd HH:mm:ss}
            {LanguageManager.Translate("Alarm_Message_Source")}：{machineName}
            {LanguageManager.Translate("Alarm_Message_Error_Item")}：{partNumber}
            {LanguageManager.Translate("Alarm_Message_Error_Address")}：{string.Join("、", addressList)}
            {LanguageManager.Translate("Alarm_Message_Error_Message")}：{faultLocation}

            {LanguageManager.Translate("Alarm_Message_FaultState")}

            {LanguageManager.Translate("Alarm_Message_Possible_Cause")}：
            - {reasonText.Replace("\n", "\n- ")}

            {LanguageManager.Translate("Alarm_Message_Repair_Steps")}：
            1. {suggestionText.Replace("\n", "\n2. ")}

            {LanguageManager.Translate("Alarm_Message_AutoNotification")}
            ";

            return (subject, body);
        }
    }
}
