using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FX5U_IOMonitor.Utilization
{
    public class UtilizationRateCalculate
    {
        /// <summary>
        /// 計算 start 與 end 之間經過的秒數。
        /// timeOnly=true：只比對一天中的時間（忽略日期）
        /// allowOvernight=true：若 end < start，視為跨日（+24 小時）
        /// 回傳值 >= 0；若無效區間則回傳 0。
        /// </summary>
        public static float GetDurationSeconds_ByMinuteDiff_NoOvernight(
              DateTimePicker start,
              DateTimePicker end)
        {
            if (start == null || end == null || start.IsDisposed || end.IsDisposed)
                return 0f;

            int sTotalMin = start.Value.Hour * 60 + start.Value.Minute;
            int eTotalMin = end.Value.Hour * 60 + end.Value.Minute;

            int diffMin = eTotalMin - sTotalMin;

            if (diffMin < 0)
                return 0f;          // 不允許跨日：結束早於開始 → 視為 0

            if (diffMin == 0)
                return 1f;          // 開始等於結束 → 視為 1（你指定的規則）

            return diffMin * 60f;    // 其餘：分鐘差 × 60（秒）
        }
        public enum RangeChoice { Yesterday, Today, LastWeek, ThisWeek }

      
      
        public static (DateTime utcStart, DateTime utcEnd) ToUtcRange(DateTime startLocal, DateTime endLocal)
        {
            var tz = GetTaipeiTimeZone();

            // DateTimePicker 組出來的是 Unspecified，這裡明確指定為「台北時區的本地時間」
            var startUnspec = DateTime.SpecifyKind(startLocal, DateTimeKind.Unspecified);
            var endUnspec = DateTime.SpecifyKind(endLocal, DateTimeKind.Unspecified);

            var utcStart = TimeZoneInfo.ConvertTimeToUtc(startUnspec, tz);
            var utcEnd = TimeZoneInfo.ConvertTimeToUtc(endUnspec, tz);
            return (utcStart, utcEnd);
        }

        


        /// <summary>
        /// 尋找上周最後一筆的稼動率紀錄
        /// </summary>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static int GetLastWeekLastValue(string machineName, bool mondayStart = true)
        {
            if (string.IsNullOrWhiteSpace(machineName)) return 0;

            var tz = GetTaipeiTimeZone();
            var nowTpe = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);

            // 取得「上週」的本地起訖（左閉右開）
            var (wkLocalStart, wkLocalEnd) = GetLastWeekLocalRange(nowTpe, mondayStart);

            // 轉成 UTC 查資料庫（你的 StartTime 存 UTC）
            var wkUtcStart = TimeZoneInfo.ConvertTimeToUtc(wkLocalStart, tz);
            var wkUtcEnd = TimeZoneInfo.ConvertTimeToUtc(wkLocalEnd, tz);

            using var db = new ApplicationDB();

            // 若同一 StartTime 可能多筆：先聚合再取最後一個時間點的總和值
            var lastSum = db.UtilizationRate
                .AsNoTracking()
                .Where(r => r.Machine_Name == machineName &&
                            r.StartTime >= wkUtcStart &&
                            r.StartTime < wkUtcEnd)
                .GroupBy(r => r.StartTime)
                .Select(g => new { Time = g.Key, SumVal = (int?)g.Sum(x => (int?)x.History_NumericValue) ?? 0 })
                .OrderByDescending(x => x.Time)
                .Select(x => x.SumVal)
                .FirstOrDefault();

            return lastSum; // 上週區間內「最後一筆」的值
        }
        /// <summary>
        /// 尋找昨日最後的稼動率總紀錄量
        /// </summary>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static int GetYesterdayLastValue(string machineName)
        {
            if (string.IsNullOrWhiteSpace(machineName)) return 0;

            var tz = GetTaipeiTimeZone();                       // 以台北日界線計算「昨天」
            var nowTpe = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);
            var yLocalStart = nowTpe.Date.AddDays(-1);          // 昨天 00:00 (台北)
            var yLocalEnd = nowTpe.Date;                      // 今天 00:00 (台北)
            var yUtcStart = TimeZoneInfo.ConvertTimeToUtc(yLocalStart, tz);
            var yUtcEnd = TimeZoneInfo.ConvertTimeToUtc(yLocalEnd, tz);

            using var db = new ApplicationDB();

            // 若同一 StartTime 可能有多筆，先聚合（Sum），再取「最後一個時間點」的總和值
            var lastSum = db.UtilizationRate
                .AsNoTracking()
                .Where(r => r.Machine_Name == machineName &&
                            r.StartTime >= yUtcStart &&
                            r.StartTime < yUtcEnd)
                .GroupBy(r => r.StartTime)
                .Select(g => new { Time = g.Key, SumVal = (int?)g.Sum(x => (int?)x.History_NumericValue) ?? 0 })
                .OrderByDescending(x => x.Time)
                .Select(x => x.SumVal)
                .FirstOrDefault();

            return lastSum;   // 就是「昨天的最後一筆（總和）」
        }
        private static (DateTime localStart, DateTime localEnd) GetLastWeekLocalRange(DateTime nowLocal, bool mondayStart)
        {
            if (mondayStart)
            {
                // 以週一為週首
                int daysSinceMonday = ((int)nowLocal.DayOfWeek + 6) % 7; // Mon=0,...Sun=6
                var thisWeekMon = nowLocal.Date.AddDays(-daysSinceMonday);
                var lastWeekMon = thisWeekMon.AddDays(-7);
                return (lastWeekMon, thisWeekMon);
            }
            else
            {
                // 以週日為週首
                int daysSinceSunday = (int)nowLocal.DayOfWeek; // Sun=0,...Sat=6
                var thisWeekSun = nowLocal.Date.AddDays(-daysSinceSunday);
                var lastWeekSun = thisWeekSun.AddDays(-7);
                return (lastWeekSun, thisWeekSun);
            }
        }


        public static TimeZoneInfo GetTaipeiTimeZone()
        {
            try { return TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time"); }  // Windows
            catch { return TimeZoneInfo.FindSystemTimeZoneById("Asia/Taipei"); }         // Linux
        }
        public static int Get_UtilizationRate(DateTime utcStart, DateTime utcEnd, string Machinename)
        {
            if (utcEnd <= utcStart)
                throw new ArgumentException("utcEnd 必須大於 utcStart");
            if (string.IsNullOrWhiteSpace(Machinename))
                return 0;

            using var db = new ApplicationDB();

            // 只抓到需要的欄位
            var q = db.UtilizationRate
                .AsNoTracking()
                .Where(r => r.StartTime >= utcStart &&
                            r.StartTime < utcEnd &&
                            r.Machine_Name == Machinename);

            // 先「同一時間點」聚合相加，再取首尾
            var firstSum = q.GroupBy(r => r.StartTime)
                 .Select(g => new { Time = g.Key, SumVal = g.Sum(x => (int?)x.History_NumericValue) ?? 0 })
                 .OrderBy(x => x.Time)
                 .Select(x => x.SumVal)
                 .FirstOrDefault();
            var lastSum = q.GroupBy(r => r.StartTime)
                .Select(g => new { Time = g.Key, SumVal = g.Sum(x => (int?)x.History_NumericValue) ?? 0 })
                .OrderByDescending(x => x.Time)
                .Select(x => x.SumVal)
                .FirstOrDefault();

            return lastSum - firstSum;

        }


        /// <summary>
        /// 取得指定機台的 currentValue（HistoryValue + NowValue）。
        /// </summary>
        public static int GetCurrentUtilization(string machineName)
        {
            if (string.IsNullOrWhiteSpace(machineName)) return 0;
            using var db = new ApplicationDB();
            if (machineName == "Drill")
            {
                int? val = db.MachineParameters
                .AsNoTracking()
                .Where(p => p.Name == "Drill_plc_usetime")
                .Select(p => ((int?)p.History_NumericValue ?? 0) + ((int?)p.now_NumericValue ?? 0))
                .SingleOrDefault();

                //這裡理論上只有一筆，請幫我補上
                return val ?? 0;

            }
            else
            {
                //這裡可能會搜尋到多筆，每一筆的History_NumericValue ?? 0)相加後再總和
                int total = db.MachineParameters
                 .AsNoTracking()
                 .Where(p => p.Machine_Name == machineName &&
                             p.Read_type == "word" && p.Read_view == 5)
                 .Select(p => (p.History_NumericValue ?? 0) + (p.now_NumericValue ?? 0))
                 .Sum(x => (int)x);

                return total;

            }
        }

        public UtilizationResult CalculateUtilization( List<MachineStatusRecord> statusRecords, DateTime shiftStart,DateTime shiftEnd)
        {
            if (statusRecords == null || statusRecords.Count < 2)
                throw new ArgumentException("statusRecords 至少需要兩筆(起始+結束)");

            int cuttingSeconds = 0;
            int idleSeconds = 0;

            for (int i = 0; i < statusRecords.Count - 1; i++)
            {
                var current = statusRecords[i];
                var next = statusRecords[i + 1];

                int duration = (int)(next.Timestamp - current.Timestamp).TotalSeconds;

                if (current.Status == 1)
                    cuttingSeconds += duration;
                else
                    idleSeconds += duration;
            }

            int totalSeconds = cuttingSeconds + idleSeconds;
            int denominatorSeconds = (int)(shiftEnd - shiftStart).TotalSeconds;

            double utilizationRate = denominatorSeconds > 0
                ? (double)cuttingSeconds / denominatorSeconds
                : 0.0;

            return new UtilizationResult
            {
                CuttingSeconds = cuttingSeconds,
                IdleSeconds = idleSeconds,
                TotalSeconds = totalSeconds,
                DenominatorSeconds = denominatorSeconds,
                UtilizationRate = Math.Round(utilizationRate, 4)
            };
        }
    }
}
