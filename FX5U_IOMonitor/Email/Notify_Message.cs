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
            public static readonly Dictionary<MessageSubjectType, string> SubjectMap = new()
            {
                { MessageSubjectType.DailyHealthStatus, "【每週通知】元件健康狀態" },
                { MessageSubjectType.UnresolvedWarnings, "【每日提醒】尚未解除的警告維護" },
                { MessageSubjectType.TriggeredAlarm, "【警告】已觸發的異常警告" },
                { MessageSubjectType.TriggeredLifeNotification, "【壽命提醒】元件壽命即將耗盡" }
            };

            public static string GetSubject(MessageSubjectType type)
            {
                return SubjectMap.TryGetValue(type, out var subject) ? subject : "【系統通知】未定義主旨";
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
                    return "✅ 目前無黃燈狀態元件";

                var groupedByMachine = yellowItems
                    .GroupBy(io => io.Machine_name)
                    .OrderBy(g => g.Key);

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"發送通知時間：{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
                sb.AppendLine("系統判定此元件處於「黃燈狀態」\n");

                foreach (var group in groupedByMachine)
                {
                    string machineName = group.Key;
                    var descriptions = group.Select(io => io.Description).Distinct();

                    sb.AppendLine($"設備名稱：{machineName}");
                    sb.AppendLine($"欲更換料號名稱：{string.Join("、", descriptions)}\n");
                    sb.AppendLine("元件儲存器位置：");

                    int index = 1;
                    foreach (var item in group)
                    {
                        string address = item.address;
                        string description = item.Description;
                        int maxUsage = item.MaxLife > 0 ? item.MaxLife : 1;
                        int currentUsage = item.equipment_use;
                        double usagePercent = item.RUL; ;

                        sb.AppendLine($"{index}. {address}、{description}、最大使用次數：{maxUsage:N0}、目前使用次數：{currentUsage:N0}、剩餘壽命百分比：{usagePercent:F2} %");
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
                    return "✅ 目前無紅燈狀態元件";

                var groupedByMachine = yellowItems
                    .GroupBy(io => io.Machine_name)
                    .OrderBy(g => g.Key);

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"發送通知時間：{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
                sb.AppendLine("系統判定此元件處於「紅燈狀態」\n");

                foreach (var group in groupedByMachine)
                {
                    string machineName = group.Key;
                    var descriptions = group.Select(io => io.Description).Distinct();

                    sb.AppendLine($"設備名稱：{machineName}");
                    sb.AppendLine($"欲更換料號名稱：{string.Join("、", descriptions)}\n");
                    sb.AppendLine("元件儲存器位置：");

                    int index = 1;
                    foreach (var item in group)
                    {
                        string address = item.address;
                        string description = item.Description;
                        int maxUsage = item.MaxLife > 0 ? item.MaxLife : 1;
                        int currentUsage = item.equipment_use;

                        double usagePercent = item.RUL; ;

                        sb.AppendLine($"{index}. {address}、{description}、最大使用次數：{maxUsage:N0}、目前使用次數：{currentUsage:N0}、剩餘壽命百分比：{usagePercent:F2} %");
                        index++;
                    }

                    sb.AppendLine(); // 每組結尾加空行
                }

                return sb.ToString().Trim();
            }
        }




    }
}
