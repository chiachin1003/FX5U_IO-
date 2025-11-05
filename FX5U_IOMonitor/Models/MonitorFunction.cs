using SLMP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FX5U_IOMonitor.Models
{
    internal class MonitorFunction
    {

        public static string ConvertSecondsToDHMS(int totalSeconds)
        {
            //int days = totalSeconds / 86400;
            int totalHours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;
            TimeSpan span = TimeSpan.FromSeconds(totalSeconds);
            string note = LanguageManager.Translate("Time_Notes"); 

           
            //return $" {span.Hours:D2} : {span.Minutes:D2} : {span.Seconds:D2}   {note}";
            return $" {totalHours:D2} : {span.Minutes:D2} : {span.Seconds:D2}";

        }

        /// <summary>
        /// 從 PLC 指定地址讀取兩個 Word，組成總秒數並轉換為 TimeSpan（天/時/分/秒）。
        /// </summary>
        /// <returns>TimeSpan 結構，包含天數、時、分、秒</returns>
        public static string FormatPlcTime(ushort[] wordData) //read_view = 1
        {
            if (wordData == null || wordData.Length != 2)
                throw new ArgumentException("wordData 必須是長度為 2 的 ushort 陣列");

            // 合併成 32-bit 無號整數（小端序）
            uint totalSeconds = ((uint)wordData[1] << 16) | wordData[0];

            // 轉為 TimeSpan
            TimeSpan span = TimeSpan.FromSeconds(totalSeconds);

            // 回傳格式化字串
            // return $"{span:hh\\:mm\\:ss} {LanguageManager.Translate("Time_Notes")}";
            return $"{span:hh\\:mm\\:ss}";

        }
        ///
        /// 
        public static uint mergenumber(ushort[] plc2read)
        {
            if (plc2read == null || plc2read.Length != 2)
                throw new ArgumentException("wordData 必須是長度為 2 的 ushort 陣列");

            // 合併成 32-bit 無號整數（小端序）
            uint totalnumber = ((uint)plc2read[1] << 16) | plc2read[0];

            // 回傳
            return totalnumber;
        }
        /// <summary>
        /// 將秒數轉為兩個 Word（ushort）並寫入 PLC 指定地址。
        /// </summary>
        public static ushort[] WriteTimeToPlc( int totalSeconds)
        {
            // 拆成小端序的 ushort[]
            ushort low = (ushort)(totalSeconds & 0xFFFF);
            ushort high = (ushort)((totalSeconds >> 16) & 0xFFFF);
            ushort[] wordData = new ushort[] { low, high };

            return wordData;
        }
        /// <summary>
        /// 高低位數值切割，支援32Bit
        /// </summary>
        /// <param name="value"></param>當前數值
        /// <param name="wordCount"></param>轉換成多少個字節進行輸入
        /// <param name="inputnumber"></param>高低位元首字母切割轉換
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static ushort[] SmartWordSplit(int value, int wordCount, int inputnumber = 0)
        {
            if (wordCount < 1 || wordCount > 2)
                throw new ArgumentOutOfRangeException(nameof(wordCount), "int 僅支援 wordCount 為 1 或 2");

            List<ushort> result = new List<ushort>();

            if (value >= 0 && value <= ushort.MaxValue)
            {
                // 單 word
                result.Add((ushort)value);
            }
            else
            {
                // 兩 word（補正 signed int）
                ushort low = (ushort)(value & 0xFFFF);
                ushort high = (ushort)((value >> 16) & 0xFFFF);
                result.Add(low);
                result.Add(high);
            }
            //for (int i = 0; i < wordCount; i++)
            //{
            //    ushort word = (ushort)((value >> (i * 16)) & 0xFFFF);
            //    result.Add(word);
            //}

            if (inputnumber == 1)
                result.Reverse(); // 大端序

            //Debug.WriteLine($"📘 SmartWordSplit(int) value={value}, wordCount={wordCount}, endian={(inputnumber == 0 ? "Little" : "Big")}");
            //Debug.WriteLine($"➡️ wordData = [{string.Join(", ", result)}]");

            return result.ToArray();
        }
        /// <summary>
        /// 高低位數值切割，支援64Bit
        /// </summary>
        /// <param name="value"></param>
        /// <param name="wordCount"></param>
        /// <param name="inputnumber"></param>
        /// <returns></returns>
        public static ushort[] SmartWordSplit(ulong value, int wordCount, int inputnumber = 0)
        {
            
            List<ushort> result = new List<ushort>();

            for (int i = 0; i < wordCount; i++)
            {
                ushort word = (ushort)((value >> (i * 16)) & 0xFFFF);
                result.Add(word);
            }

            if (inputnumber == 1)
                result.Reverse(); // 高位在前（大端序）

            //Debug.WriteLine($"📘 SmartWordSplit value={value}, wordCount={wordCount}, endian={(inputnumber == 0 ? "Little" : "Big")}");
            //Debug.WriteLine($"➡️ wordData = [{string.Join(", ", result)}]");

            return result.ToArray();
        }
        public static string oil_press_transfer(int code)
        {
            return code switch
            {
                0 => "正常",
                1 => "崩齒",
                2 => "鋸帶斷裂",
                3 => "歪斜",
                4 => "工件表面粗糙",
                5 => "更換鋸帶",
                6 => "維修",
                _ => "未知狀態"
            };
        }
       
        public class RuntimebitTimer
        {
            public int NowValue { get; set; } = 0;         // 當前秒數（每秒 +1）
            public int HistoryValue { get; set; } = 0;     // 歷史秒數（累積後寫回）
            public bool IsCounting { get; set; } = false;  // 是否正在計數
            public DateTime LastUpdateTime { get; set; } = (DateTime.UtcNow);

        }
        public class RuntimewordTimer
        {
            public int NowValue { get; set; } = 0;         // 當前秒數（每秒 +1）
            public int HistoryValue { get; set; } = 0;     // 歷史秒數（累積後寫回）
            public bool IsCounting { get; set; } = false;  // 是否正在計數
            public DateTime LastUpdateTime { get; set; } =(DateTime.UtcNow);

            public List<double> AverageBuffer = new List<double>();  // 用來存 10 秒資料
        }





        }
}
