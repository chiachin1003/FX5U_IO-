using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
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



    }
}
