using FX5U_IOMonitor.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Threading.Timer;

namespace FX5U_IOMonitor.Models
{
    public static class ParameterHistoryManager
    {
        public static async Task RecordDailyHistory()
        {
            using var db = new ApplicationDB();

            var parameters = db.MachineParameters.Include(p => p.HistoryRecodes).ToList();
            var now = DateTime.UtcNow;
            string todayTag = now.ToString("yyyyMMdd");

            foreach (var param in parameters)
            {
                int nowVal = (int)param.now_NumericValue;
                int hisVal = (int)param.History_NumericValue;

                bool alreadyExists = db.MachineParameterHistoryRecodes.Any(r =>
                    r.MachineParameterId == param.Id && r.PeriodTag == todayTag);
                if (alreadyExists) continue;

                db.MachineParameterHistoryRecodes.Add(new MachineParameterHistoryRecode
                {
                    MachineParameterId = param.Id,
                    StartTime = now.Date,
                    EndTime = now,
                    History_NumericValue = nowVal + hisVal,
                    PeriodTag = todayTag,
                    ResetBy = "系統自動"
                });
            }
            await db.SaveChangesAsync();
        }

        public static async Task RecordMonthlySummary()
        {
            using var db = new ApplicationDB();
            var now = DateTime.UtcNow;
            var monthTag = now.ToString("yyyyMM");

            var parameters = db.MachineParameters.Include(p => p.HistoryRecodes).ToList();

            foreach (var param in parameters)
            {
                bool exists = db.MachineParameterHistoryRecodes.Any(r =>
                    r.MachineParameterId == param.Id && r.PeriodTag == monthTag);
                if (exists) continue;

                var lastRecord = db.MachineParameterHistoryRecodes
                    .Where(r => r.MachineParameterId == param.Id && r.PeriodTag.StartsWith(now.ToString("yyyy")))
                    .OrderByDescending(r => r.StartTime)
                    .FirstOrDefault();

                DateTime startOfMonth = lastRecord?.EndTime.AddSeconds(1)
                    ?? new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

                DateTime endOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddMonths(1).AddSeconds(-1);

                var monthlyTotal = db.MachineParameterHistoryRecodes
                    .Where(r => r.MachineParameterId == param.Id &&
                                r.StartTime >= startOfMonth && r.EndTime <= endOfMonth &&
                                r.PeriodTag.Length == 8) // 只抓 daily 資料彙整
                    .Sum(r => r.History_NumericValue ?? 0);

                db.MachineParameterHistoryRecodes.Add(new MachineParameterHistoryRecode
                {
                    MachineParameterId = param.Id,
                    StartTime = startOfMonth,
                    EndTime = endOfMonth,
                    History_NumericValue = monthlyTotal,
                    PeriodTag = monthTag,
                    ResetBy = "系統月彙整"
                });
            }
            await db.SaveChangesAsync();
        }

        public static async Task StartAutoDailyRecordingLoop()
        {
            while (true)
            {
                await RecordDailyHistory();
                await RecordMonthlySummary();

                await Task.Delay(TimeSpan.FromHours(24));
            }
        }

        public static List<MachineParameterHistoryRecode> GetHistoryByMonth(string machineName, string parameterName, int month, int year)
        {
            using var db = new ApplicationDB();
            var param = db.MachineParameters.Include(p => p.HistoryRecodes)
                            .FirstOrDefault(p => p.Machine_Name == machineName && p.Name == parameterName);

            if (param == null) return new List<MachineParameterHistoryRecode>();

            var monthStart = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
            var monthEnd = monthStart.AddMonths(1);

            return db.MachineParameterHistoryRecodes
                .Where(r => r.MachineParameterId == param.Id &&
                            r.StartTime >= monthStart && r.EndTime < monthEnd)
                .OrderBy(r => r.StartTime)
                .ToList();
        }
        /// <summary>
        /// 自動判斷這個月是否已記錄過
        /// </summary>
        /// <returns></returns>
        //public static async Task RecordMonthlySummaryIfNotExists()
        //{
        //    using var db = new ApplicationDB();
        //    var now = DateTime.UtcNow;
        //    var monthTag = now.ToString("yyyyMM");

        //    bool alreadyHas = db.MachineParameterHistoryRecodes.Any(r => r.PeriodTag == monthTag && r.ResetBy == "系統月彙整");
        //    if (!alreadyHas)
        //    {
        //        await RecordMonthlySummary(); // ➤ 執行你原本的彙整邏輯
        //    }
        //}
        //public static class ParameterHistoryScheduler
        //{
        //    private static Timer? _monthlyTimer;

        //    public static async Task InitializeMonthlySchedule()
        //    {
        //        await ParameterHistoryManager.RecordMonthlySummaryIfNotExists();

        //        ScheduleNextRun(); // 安排下次執行
        //    }

        //    private static void ScheduleNextRun()
        //    {
        //        DateTime now = DateTime.Now;

        //        // ➤ 找下個月的 1 號 08:00
        //        DateTime nextRunTime = new DateTime(now.Year, now.Month, 1, 8, 0, 0).AddMonths(1);

        //        TimeSpan timeUntilNextRun = nextRunTime - now;
        //        if (timeUntilNextRun.TotalMilliseconds <= 0)
        //            timeUntilNextRun = TimeSpan.FromMinutes(1); // 安全容錯（萬一時間錯誤）

        //        _monthlyTimer = new Timer(async _ =>
        //        {
        //            await ParameterHistoryManager.RecordMonthlySummaryIfNotExists();

        //            // 執行完後，重新安排下一次
        //            ScheduleNextRun();

        //        }, null, timeUntilNextRun, Timeout.InfiniteTimeSpan); // 一次性任務
        //    }
        //}
    }

}
