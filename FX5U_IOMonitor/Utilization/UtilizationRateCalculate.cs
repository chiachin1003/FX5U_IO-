using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using Microsoft.EntityFrameworkCore;

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

            // 分子：逐區間查 DB 後加總（以 UTC 查）
            float numerator = 0;
            foreach (var (startLocal, endLocal) in intervals)
            {
                var (utcStart, utcEnd) = ToUtcRange(startLocal, endLocal); // 你專案裡已有
                numerator += DBfunction.Get_UtilizationRate(utcStart, utcEnd, machineName);
            }

            double pct = denom <= 0 ? 0 : (numerator / denom) * 100.0;
            string percentage = pct.ToString("0");
            return percentage;
        }
        public static TimeZoneInfo GetTaipeiTimeZone()
        {
            try { return TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time"); }  // Windows
            catch { return TimeZoneInfo.FindSystemTimeZoneById("Asia/Taipei"); }         // Linux
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
       

    }
}
