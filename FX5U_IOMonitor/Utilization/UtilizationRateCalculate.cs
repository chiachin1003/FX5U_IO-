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
        //要加入防呆
       
        public static List<ShiftResult> GetCuttingByRange(string machineName,List<UtilizationShiftConfig> shifts, DateTime startLocal,DateTime endLocal)
        {
            if (string.IsNullOrWhiteSpace(machineName)) return new List<ShiftResult>();

            var tz = GetTaipeiTimeZone();
            var startUtc = TimeZoneInfo.ConvertTimeToUtc(startLocal, tz);
            var endUtc = TimeZoneInfo.ConvertTimeToUtc(endLocal, tz);

            using var db = new ApplicationDB();

            var query = db.UtilizationStatusRecord
                .AsNoTracking()
               .Where(r => r.Machinename == machineName &&
                r.StartTime >= startUtc &&
                r.EndTime <= endUtc)
                .ToList();

            var results = new List<ShiftResult>();

            foreach (var shift in shifts.Where(s => s.Enabled))
            {
                var shiftStartLocal = startLocal.Date.Add(TimeSpan.Parse(shift.Start));
                var shiftEndLocal = startLocal.Date.Add(TimeSpan.Parse(shift.End));

                if (shiftEndLocal <= shiftStartLocal)
                    shiftEndLocal = shiftEndLocal.AddDays(1);

                var shiftStartUtc = TimeZoneInfo.ConvertTimeToUtc(shiftStartLocal, tz);
                var shiftEndUtc = TimeZoneInfo.ConvertTimeToUtc(shiftEndLocal, tz);

                float cuttingSeconds = 0;

                foreach (var r in query)
                {
                    var s = r.StartTime < shiftStartUtc ? shiftStartUtc : r.StartTime;
                    var e = (r.EndTime) > shiftEndUtc ? shiftEndUtc : (r.EndTime);

                    if (e > s && r.Status == 1)
                    {
                        cuttingSeconds += (float)(e - s).TotalSeconds;
                    }
                }

                results.Add(new ShiftResult
                {
                    Machinename = machineName,
                    ShiftNo = shift.ShiftNo,
                    CuttingSeconds = cuttingSeconds
                });
            }

            return results;
        }

        public static List<ShiftResult> GetTodayCutting(string machineName, List<UtilizationShiftConfig> shifts)
        {
            var tz = GetTaipeiTimeZone();
            var nowTpe = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);

            var startLocal = nowTpe.Date;   // 今天 00:00
            var endLocal = nowTpe;          // 現在

            return GetCuttingByRange(machineName, shifts, startLocal, endLocal);
        }

        // 昨天
        //public static List<ShiftResult> GetYesterdayCutting(string machineName, List<UtilizationShiftConfig> shifts)
        //{
        //    var tz = GetTaipeiTimeZone();
        //    var nowTpe = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);

        //    var startLocal = nowTpe.Date.AddDays(-1); // 昨天 00:00
        //    var endLocal = nowTpe.Date;               // 今天 00:00

        //    return GetCuttingByRange(machineName, shifts, startLocal, endLocal);
        //}

        // 本週（週一 00:00 ~ 今天現在）
        public static List<ShiftResult> GetThisWeekCutting(string machineName, List<UtilizationShiftConfig> shifts)
        {
            var tz = GetTaipeiTimeZone();
            var nowTpe = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);

            int diff = (int)nowTpe.DayOfWeek == 0 ? 6 : (int)nowTpe.DayOfWeek - 1; // 星期一=0, 星期日=6
            var startLocal = nowTpe.Date.AddDays(-diff);  // 本週一 00:00
            var endLocal = nowTpe;                        // 現在

            return GetCuttingByRange(machineName, shifts, startLocal, endLocal);
        }

        //// 上週（週一 00:00 ~ 週日 23:59:59）
        //public static List<ShiftResult> GetLastWeekCutting(string machineName, List<UtilizationShiftConfig> shifts)
        //{
        //    var tz = GetTaipeiTimeZone();
        //    var nowTpe = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);

        //    int diff = (int)nowTpe.DayOfWeek == 0 ? 6 : (int)nowTpe.DayOfWeek - 1;
        //    var thisMonday = nowTpe.Date.AddDays(-diff);   // 本週一 00:00
        //    var lastMonday = thisMonday.AddDays(-7);       // 上週一 00:00
        //    var lastSundayEnd = thisMonday;                // 本週一 (上週的結束)

        //    return GetCuttingByRange(machineName, shifts, lastMonday, lastSundayEnd);
        //}



        // 昨天
        public static List<ShiftResult> GetYesterdayCutting(string machineName, List<UtilizationShiftConfig> shifts, DateTime selectedDate)
        {
            
            var tz = GetTaipeiTimeZone();
            var nowTpe = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz); // ✅ 明確從 UTC 轉為台北時間
            
            var targetDate = DateTime.SpecifyKind(selectedDate, DateTimeKind.Unspecified);

            // 起始時間：指定日期的 00:00
            var startLocal = targetDate.AddDays(-1); ;

            // 結束時間：指定日期的隔天 00:00（完整一天）
            var endLocal = targetDate;

            return GetCuttingByRange(machineName, shifts, startLocal, endLocal);

        }
       

        // 上週（週一 00:00 ~ 週日 23:59:59）
        public static List<ShiftResult> GetLastWeekCutting(string machineName, List<UtilizationShiftConfig> shifts, DateTime selectedDate)
        {
            var tz = GetTaipeiTimeZone();
            var nowTpe = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);
            // 若未指定日期，預設為今天
            var targetDate = DateTime.SpecifyKind(selectedDate, DateTimeKind.Unspecified);

            // 將星期日視為第 7 天
            int dayOfWeek = (int)targetDate.DayOfWeek;
            if (dayOfWeek == 0) dayOfWeek = 7; // 星期日=7

            // 計算該日期所在週的星期一
            var thisMonday = targetDate.AddDays(-(dayOfWeek - 1));

            // 取得上週的起訖時間
            var lastMonday = thisMonday.AddDays(-7);   // 上週一 00:00
            var lastSundayEnd = thisMonday;            // 本週一 (上週結束)


            return GetCuttingByRange(machineName, shifts, lastMonday, lastSundayEnd);
        }

        public class DailyUtilizationResult
        {
            public string MachineName { get; set; } = "";
            public float RunSeconds { get; set; }
            public float IdleSeconds { get; set; }
            public float UtilizationPercent { get; set; }
        }
        public class WeeklyUtilizationResult
        {
            public string MachineName { get; set; } = "";
            public DateTime WeekStart { get; set; }
            public DateTime WeekEnd { get; set; }
            public float RunSeconds { get; set; }
            public float IdleSeconds { get; set; }
            public float UtilizationPercent { get; set; }
        }
        public static class UtilizationCalculator
        {
            /// <summary>
            /// 取得指定機台、指定日期的稼動率資訊（含運轉、閒置、百分比）
            /// </summary>
            public static DailyUtilizationResult GetDailyUtilization(string machineName, DateTime selectedDate)
            {
                const float SecondsPerDay = 3600 * 24f;

                var tz = GetTaipeiTimeZone();
                var nowTpe = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);
                var Selectdate = DateTime.SpecifyKind(selectedDate, DateTimeKind.Unspecified);

                var startUtc = TimeZoneInfo.ConvertTimeToUtc(Selectdate);
                DateTime endUtc = startUtc.AddDays(1);
                using var db = new ApplicationDB();
                
                // 取出該機台在指定日期的所有紀錄
                var records = db.UtilizationStatusRecord
                     .Where(r => r.Machinename == machineName &&
                r.StartTime >= startUtc &&
                r.StartTime < endUtc).ToList();

                float runSeconds = records.Where(x => x.Status == 1).Sum(x => x.DurationSeconds);
                float idleSeconds = records.Where(x => x.Status == 0).Sum(x => x.DurationSeconds);


                // 分母固定 24 小時 = 86400 秒
                float utilization = (runSeconds / SecondsPerDay) * 100f;

                return new DailyUtilizationResult
                {
                    MachineName = machineName,
                    RunSeconds = runSeconds,
                    IdleSeconds = idleSeconds,
                    UtilizationPercent = (float)Math.Round(utilization, 2)
                };

            }
            public static WeeklyUtilizationResult GetWeeklyUtilization(string machineName, bool isThisWeek)
            {
                const float SecondsPerWeek = 3600 * 24f * 7f;  // 分母固定 7 天

                // 取得台北時區
                var tz = GetTaipeiTimeZone();
                var nowTpe = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);

                // 找出本週週日
                int diff = DayOfWeek.Sunday - nowTpe.DayOfWeek;
                if (diff > 0) diff -= 7; // 若今天是星期一～六，要往前推到上個週日
                DateTime thisWeekStartTpe = nowTpe.Date.AddDays(diff);
                DateTime thisWeekEndTpe = thisWeekStartTpe.AddDays(7);

                // 計算上週的週日～週六
                DateTime lastWeekStartTpe = thisWeekStartTpe.AddDays(-7);
                DateTime lastWeekEndTpe = thisWeekStartTpe;

                // 決定要查哪一週
                DateTime startTpe = isThisWeek ? thisWeekStartTpe : lastWeekStartTpe;
                DateTime endTpe = isThisWeek ? thisWeekEndTpe : lastWeekEndTpe;

                // 轉成 UTC 方便與資料庫比對
                DateTime startUtc = TimeZoneInfo.ConvertTimeToUtc(startTpe, tz);
                DateTime endUtc = TimeZoneInfo.ConvertTimeToUtc(endTpe, tz);

                using var db = new ApplicationDB();

                // 查詢該週的紀錄
                var records = db.UtilizationStatusRecord
                    .Where(r => r.Machinename == machineName &&
                                r.StartTime >= startUtc &&
                                r.StartTime < endUtc)
                    .ToList();

                float runSeconds = records.Where(x => x.Status == 1).Sum(x => x.DurationSeconds);
                float idleSeconds = records.Where(x => x.Status == 0).Sum(x => x.DurationSeconds);
                float utilization = (runSeconds / SecondsPerWeek) * 100f;

                return new WeeklyUtilizationResult
                {
                    MachineName = machineName,
                    WeekStart = startTpe,
                    WeekEnd = endTpe,
                    RunSeconds = runSeconds,
                    IdleSeconds = idleSeconds,
                    UtilizationPercent = (float)Math.Round(utilization, 2)
                };
            }
        }

    }
}
