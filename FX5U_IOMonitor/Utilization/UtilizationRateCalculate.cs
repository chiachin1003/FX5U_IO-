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

        public static string ShowUtilizationRate(string machineName, RangeChoice range)
        {
            // 讀取預設模式（1~4）
            int mode = Properties.Settings.Default.DefaultUtilizationRate;
            if (mode < 1 || mode > 4) mode = 1; // 防呆
            var (sp, ep) = GetShiftFromSettings(mode);

            var intervals = BuildIntervals(mode, range);
            // 分母：班別總時長（秒）
            double denom = intervals.Sum(iv => (iv.endLocal - iv.startLocal).TotalSeconds);

            if (range == RangeChoice.Yesterday || range == RangeChoice.LastWeek)
            {
                // 分子：逐區間查 DB 後加總（以 UTC 查）
                float numerator = 0;
                foreach (var (startLocal, endLocal) in intervals)
                {
                    var (utcStart, utcEnd) = ToUtcRange(startLocal, endLocal); // 你專案裡已有
                    numerator += Get_UtilizationRate(utcStart, utcEnd, machineName);
                }

                double pct = denom <= 0 ? 0 : (numerator / denom) * 100.0;
                string percentage = pct.ToString("0");
                return percentage;
            }
            else 
            {
                int last = 0;
                if (range == RangeChoice.ThisWeek)
                {
                    last = UtilizationRateCalculate.GetLastWeekLastValue(machineName);
                }
                else
                {
                    last = UtilizationRateCalculate.GetYesterdayLastValue(machineName);
                }
                int current = UtilizationRateCalculate.GetCurrentUtilization(machineName);
                float numerator = (float)(current - last);
                double pct = denom <= 0 ? 0 : (numerator / denom) * 100.0;
                string percentage = pct.ToString("0");
                return percentage;
            }


        }
      
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

        private static (TimeSpan start, TimeSpan end) GetShiftFromSettings(int row) => row switch
        {
            1 => (Properties.Settings.Default.UtilizationStart1,
                  Properties.Settings.Default.UtilizationEnd1),
            2 => (Properties.Settings.Default.UtilizationStart2,
                  Properties.Settings.Default.UtilizationEnd2),
            3 => (Properties.Settings.Default.UtilizationStart3,
                  Properties.Settings.Default.UtilizationEnd3),
            4 => (Properties.Settings.Default.UtilizationStart4,
                  Properties.Settings.Default.UtilizationEnd4),
            _ => (Properties.Settings.Default.UtilizationStart1,
                  Properties.Settings.Default.UtilizationEnd1),
        };

        // 計算某日期的班別起迄 DateTime（處理跨夜）
        private static (DateTime start, DateTime end) BuildShiftOnDate(int row, DateTime date, bool allowOvernight = true)
        {
            var (s, e) = GetShiftFromSettings(row);

            var start = date.Date + s;   // ＝ DateTime.Today + s（若 date=Today）
            var end = date.Date + e;

            // 若允許跨夜，且 end <= start，則視為隔天結束
            if (allowOvernight && end <= start) end = end.AddDays(1);

            return (start, end);
        }

        // 取得一週起始日（預設星期一）
        private static DateTime StartOfWeek(DateTime any, DayOfWeek weekStart = DayOfWeek.Monday)
        {
            int diff = (7 + (any.DayOfWeek - weekStart)) % 7;
            return any.Date.AddDays(-diff);
        }

        // 建出「這週 / 上週」某列班別的 7 天時段清單
        public static List<(DateTime start, DateTime end)> BuildShiftForWeek(int row, bool lastWeek = false,
            DayOfWeek weekStart = DayOfWeek.Monday, bool allowOvernight = true)
        {
            var week0 = StartOfWeek(DateTime.Today, weekStart);
            if (lastWeek) week0 = week0.AddDays(-7);

            var list = new List<(DateTime start, DateTime end)>(7);
            for (int i = 0; i < 7; i++)
                list.Add(BuildShiftOnDate(row, week0.AddDays(i), allowOvernight));
            return list;
        }

        // 回傳要計算的「本地時間區間」清單
        public static List<(DateTime startLocal, DateTime endLocal)> BuildIntervals(int r, RangeChoice choice)
        {
            var list = new List<(DateTime, DateTime)>();
            switch (choice)
            {
                case RangeChoice.Today:
                    {
                        var (st, ed) = BuildShiftOnDate(row: r, date: DateTime.Today); // 你已有的方法（本地）
                        list.Add((st, ed));
                        break;
                    }
                case RangeChoice.Yesterday:
                    {
                        var d = DateTime.Today.AddDays(-1);
                        var (st, ed) = BuildShiftOnDate(row: r, date: d);
                        list.Add((st, ed));
                        break;
                    }
                case RangeChoice.ThisWeek:
                    {
                        var week = BuildShiftForWeek(row: r, lastWeek: false); // 回傳 7 筆 (st, ed)
                        list.AddRange(week);
                        break;
                    }
                case RangeChoice.LastWeek:
                    {
                        var week = BuildShiftForWeek(row: r, lastWeek: true);
                        list.AddRange(week);
                        break;
                    }
            }
            return list;
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


    }
}
