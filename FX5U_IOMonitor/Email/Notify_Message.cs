using FX5U_IOMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Email
{
    public class Notify_Message
    {
        public enum MessageSubjectType
        {
            DailyHealthStatus,         // 每日定期通知(元件健康狀態)
            UnresolvedWarnings,        // 尚未解除的警告維護
            TriggeredAlarm,            // 已觸發警告
            TriggeredLifeNotification  // 已觸發的壽命通知
        }
        public static class MessageSubjectHelper
        {
            //public static readonly Dictionary<MessageSubjectType, string> SubjectMap = new()
            //{
            //    { MessageSubjectType.DailyHealthStatus, LanguageManager.TranslateFormat("MessageSubjectType_DailyHealthStatus")},
            //    { MessageSubjectType.UnresolvedWarnings, LanguageManager.TranslateFormat("MessageSubjectType_UnresolvedWarnings") },
            //    { MessageSubjectType.TriggeredAlarm,  LanguageManager.TranslateFormat("MessageSubjectType_TriggeredAlarm")},
            //    { MessageSubjectType.TriggeredLifeNotification, LanguageManager.TranslateFormat("MessageSubjectType_TriggeredLifeNotification") }
            //};

            public static string GetSubject(MessageSubjectType type)
            {
                return type switch
                {
                    MessageSubjectType.DailyHealthStatus => LanguageManager.TranslateFormat("MessageSubjectType_DailyHealthStatus"),
                    MessageSubjectType.UnresolvedWarnings => LanguageManager.TranslateFormat("MessageSubjectType_UnresolvedWarnings"),
                    MessageSubjectType.TriggeredAlarm => LanguageManager.TranslateFormat("MessageSubjectType_TriggeredAlarm"),
                    MessageSubjectType.TriggeredLifeNotification => LanguageManager.TranslateFormat("MessageSubjectType_TriggeredLifeNotification"),
                    _ => LanguageManager.TranslateFormat("MessageSubjectType_System")
                };
            }

        }
        
        /// <summary>
        /// 每日統計當前黃燈數量信息
        /// </summary>
        /// <returns></returns>
        public static string GenerateYellowComponentGroupSummary()
        {
            using (var context = new ApplicationDB())
            {
                var yellowItems = context.Machine_IO
                    .Where(io => io.RUL > io.Setting_red && io.RUL <= io.Setting_yellow)
                    .ToList();

                if (!yellowItems.Any())
                    return LanguageManager.TranslateFormat("Element_Message_NoFaultDetected");

                var groupedByMachine = yellowItems
                    .GroupBy(io => io.Machine_name)
                    .OrderBy(g => g.Key);

                StringBuilder sb = new StringBuilder();

                sb.AppendLine(LanguageManager.TranslateFormat("Element_Message_SentTime") + $"{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
                sb.AppendLine(LanguageManager.TranslateFormat("Element_Message_YellowStatus"));

                foreach (var group in groupedByMachine)
                {
                    string machineName = group.Key;
                    var descriptions = group.Select(io => io.Description).Distinct();

                    sb.AppendLine(LanguageManager.TranslateFormat("Element_Message_MachineName") + $"{machineName}");
                    sb.AppendLine(LanguageManager.TranslateFormat("Element_Message_ComponentReplaced") +$"{string.Join("、", descriptions)}\n");
                    sb.AppendLine(LanguageManager.TranslateFormat("Element_Message_ComponentLocation"));

                    int index = 1;
                    foreach (var item in group)
                    {
                        string address = item.address;
                        string description = item.Description;
                        int maxUsage = item.MaxLife > 0 ? item.MaxLife : 1;
                        int currentUsage = item.equipment_use;
                        double usagePercent = item.RUL; ;

                        sb.AppendLine($"{index}. {address}、{description}、" + LanguageManager.TranslateFormat("Element_Message_MaxUsage") +
                            $"{maxUsage:N0}、" + LanguageManager.TranslateFormat("Element_Message_CurrentUsage") +
                            $"{currentUsage:N0}、" + LanguageManager.TranslateFormat("Element_Message_RemainingLife") +
                            $"{usagePercent:F2} %");
                        index++;
                    }

                    sb.AppendLine(); // 每組結尾加空行
                }

                return sb.ToString().Trim();
            }
        }
        /// <summary>
        /// 每日統計當前紅燈訊息
        /// </summary>
        /// <returns></returns>
        public static string GenerateRedComponentGroupSummary()
        {
            using (var context = new ApplicationDB())
            {
                var yellowItems = context.Machine_IO
                    .Where(io => io.RUL <= io.Setting_red)
                    .ToList();

                if (!yellowItems.Any())
                    return LanguageManager.TranslateFormat("Element_Message_NoFaultDetected");

                var groupedByMachine = yellowItems
                    .GroupBy(io => io.Machine_name)
                    .OrderBy(g => g.Key);

                StringBuilder sb = new StringBuilder();

                sb.AppendLine(LanguageManager.TranslateFormat("Element_Message_SentTime") + $"{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
                sb.AppendLine(LanguageManager.TranslateFormat("Element_Message_RedStatus"));

                foreach (var group in groupedByMachine)
                {
                    string machineName = group.Key;
                    var descriptions = group.Select(io => io.Description).Distinct();

                    sb.AppendLine(LanguageManager.TranslateFormat("Element_Message_MachineName") + $"{machineName}");
                    sb.AppendLine(LanguageManager.TranslateFormat("Element_Message_ComponentReplaced") + $"{string.Join("、", descriptions)}\n");
                    sb.AppendLine(LanguageManager.TranslateFormat("Element_Message_ComponentLocation"));

                    int index = 1;
                    foreach (var item in group)
                    {
                        string address = item.address;
                        string description = item.Description;
                        int maxUsage = item.MaxLife > 0 ? item.MaxLife : 1;
                        int currentUsage = item.equipment_use;

                        double usagePercent = item.RUL; ;

                        sb.AppendLine($"{index}. {address}、{description}、" + LanguageManager.TranslateFormat("Element_Message_MaxUsage") +
                             $"{maxUsage:N0}、" + LanguageManager.TranslateFormat("Element_Message_CurrentUsage") +
                             $"{currentUsage:N0}、" + LanguageManager.TranslateFormat("Element_Message_RemainingLife") +
                             $"{usagePercent:F2} %");
                        index++;
                    }

                    sb.AppendLine(); // 每組結尾加空行
                }

                return sb.ToString().Trim();
            }
        }




    }
}
