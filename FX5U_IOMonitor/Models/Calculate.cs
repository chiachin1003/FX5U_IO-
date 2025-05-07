
using CsvHelper.Configuration;
using FX5U_IOMonitor.Data;
using System.Xml.Linq;
using static FX5U_IOMonitor.Models.Test_;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FX5U_IOMonitor.Models
{
    internal class Calculate
    {

        /// <summary>
        /// 更新plc讀取的資料(8進制)僅限FX5U
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="InorOut"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<now_single> ConvertPlcToNowSingle(bool[] plc, string InorOut, int startIndex)
        {
            if (plc == null || plc.Length == 0 || string.IsNullOrEmpty(InorOut))
            {
                throw new ArgumentException("plc 陣列或 InorOut 不能為空.");
            }
            if (startIndex < 0)
            {
                throw new ArgumentException("startIndex 不能為負數.");
            }

            return plc.Select((value, index) => new now_single
            {
                address = InorOut + Convert.ToString(startIndex + index, 8).PadLeft(2, '0'), // 轉換索引為 8 進制並補 0
                current_single = value
            }).ToList();
        }
        /// <summary>
        /// 更新plc讀取的資料(16進制)R16要更換
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="InorOut"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<now_single> Convert_16_PlcNowSingle(bool[] plc, string InorOut, int startIndex)
        {
            if (plc == null || plc.Length == 0 || string.IsNullOrEmpty(InorOut))
            {
                throw new ArgumentException("plc 陣列或 InorOut 不能為空.");
            }
            if (startIndex < 0)
            {
                throw new ArgumentException("startIndex 不能為負數.");
            }

            return plc.Select((value, index) => new now_single
            {
                address = InorOut + Convert.ToString(startIndex + index, 16).ToUpper().PadLeft(2, '0'), // 轉換索引為 16 進制並補 0
                current_single = value
            }).ToList();
        }
        /// <summary>
        /// 比對監控當前信號值
        /// </summary>
        /// <param name="nowList"></param>
        /// <param name="ioList"></param>
       
        public static int UpdateIOCurrentSingleToDB(List<now_single> nowList, string tableName)
        {
            if (nowList == null || nowList.Count == 0)
            {
                Console.WriteLine("⚠️ nowList 為空，未更新資料庫。");
                return 0;
            }

            using var context = new ApplicationDB();

            int updatedCount = 0;

            switch (tableName)
            {
                case "Drill":
                    {
                        var ioList = context.Drill_IO.ToList();
                        foreach (var now in nowList)
                        {
                            var io = ioList.FirstOrDefault(d => d.address == now.address);
                            if (io != null)
                            {
                                io.current_single = now.current_single;
                                updatedCount++;
                            }
                        }
                        break;
                    }
                case "Sawing":
                    {
                        var ioList = context.Sawing_IO.ToList();
                        foreach (var now in nowList)
                        {
                            var io = ioList.FirstOrDefault(d => d.address == now.address);
                            if (io != null)
                            {
                                io.current_single = now.current_single;
                                updatedCount++;
                            }
                        }
                        break;
                    }
                case "alarm":
                    {
                        var ioList = context.alarm.ToList();
                        foreach (var now in nowList)
                        {
                            var io = ioList.FirstOrDefault(d => d.M_Address == now.address);
                            if (io != null)
                            {
                                io.current_single = now.current_single;
                                updatedCount++;
                            }
                        }
                        break;
                    }
                default:
                    throw new ArgumentException($"未知的資料表名稱：{tableName}");
            }

            context.SaveChanges(); // ✅ 寫入資料庫
            Console.WriteLine($"✅ 成功更新 {updatedCount} 筆 current_single 至資料表 {tableName}。");

            return updatedCount;
        }

        public static void Update_alarm_Single(List<now_single> now_single, List<now_single> old_single)
        {
            if (now_single == null || now_single.Count == 0 || old_single == null || old_single.Count == 0)
            {
                Console.WriteLine("Error: now_single 或 old_single 為空.");
            }

            foreach (var now in now_single)
            {
                var ioMatch = old_single.FirstOrDefault(io => io.address == now.address);
                if (ioMatch != null)
                {
                    ioMatch.current_single = now.current_single; // 更新對應 address 的 current_single
                }

            }
        }
        public static List<now_single> Convert_alarmsingle(bool[] plc, string InorOut, int startIndex)
        {
            if (plc == null || plc.Length == 0 || string.IsNullOrEmpty(InorOut))
            {
                throw new ArgumentException("plc 陣列或 InorOut 不能為空.");
            }
            if (startIndex < 0)
            {
                throw new ArgumentException("startIndex 不能為負數.");
            }

            return plc.Select((value, index) => new now_single
            {
                address = InorOut + (startIndex + index).ToString(),  // ✅ 改成直接十進位字串
                current_single = value
            }).ToList();
        }


       

        public static List<IOSectionInfo> AnalyzeIOSections_16()
        {
            using var context = new ApplicationDB();
            var drillList = context.Drill_IO.ToList();

            var result = new List<IOSectionInfo>();

            var xList = drillList
                .Where(d => d.address.StartsWith("X"))
                .Select(d => Convert.ToInt32(d.address.TrimStart('X'), 16))
                .OrderBy(a => a)
                .ToList();

            if (xList.Any())
                result.Add(BuildSectionFormatted("X", xList, "hex"));

            var yList = drillList
                .Where(d => d.address.StartsWith("Y"))
                .Select(d => Convert.ToInt32(d.address.TrimStart('Y'), 16))
                .OrderBy(a => a)
                .ToList();

            if (yList.Any())
                result.Add(BuildSectionFormatted("Y", yList, "hex"));

            return result;
        }

        public static List<IOSectionInfo> AnalyzeIOSections_8()
        {
            using var context = new ApplicationDB();
            var SawingList = context.Sawing_IO.ToList();

            var result = new List<IOSectionInfo>();

            var xList = SawingList
                .Where(d => d.address.StartsWith("X"))
                .Select(d => Convert.ToInt32(d.address.TrimStart('X'), 8))
                .OrderBy(a => a)
                .ToList();

            if (xList.Any())
                result.Add(BuildSectionFormatted("X", xList, "oct"));

            var yList = SawingList
                .Where(d => d.address.StartsWith("Y"))
                .Select(d => Convert.ToInt32(d.address.TrimStart('Y'), 8))
                .OrderBy(a => a)
                .ToList();

            if (yList.Any())
                result.Add(BuildSectionFormatted("Y", yList, "oct"));

            return result;
        }

        public static List<IOSectionInfo> alarm_trans()
        {
            using (var context = new ApplicationDB())
            {
                var alarmList = context.alarm.ToList();

                var result = new List<IOSectionInfo>();

                var LList = alarmList
                    .Where(d => d.M_Address.StartsWith("L"))
                    .Select(d => Convert.ToInt32(d.M_Address.TrimStart('L')))
                    .OrderBy(a => a)
                    .ToList();

                if (LList.Any())
                {
                    result.Add(BuildSectionFormatted("L", LList, "dec"));
                }
                return result;
            };
           
        }
        private static IOSectionInfo BuildSectionFormatted(string prefix, List<int> addrList, string baseType)
        {
            int start = addrList.Min();
            int end = addrList.Max();
            int blockCount = (int)Math.Ceiling((end - start + 1) / 256.0);

            var section = new IOSectionInfo
            {
                Prefix = prefix,
                StartAddress = start,
                EndAddress = end,
                BlockCount = blockCount
            };

            if (blockCount > 1)
            {
                section.SplitPoints = Enumerable.Range(1, blockCount - 1)
                    .Select(i => start + i * 256)
                    .Where(p => p <= end)
                    .Select(p => Calculate.Format(prefix, p, baseType))
                    .ToList();
            }

            return section;
        }

        /// <summary>
        /// 將數值格式化為指定進位制（八進位、十六進位、十進位）的地址字串
        /// </summary>
        /// <param name="prefix">X / Y / M / D ...</param>
        /// <param name="value">數值位址（已轉為 int）</param>
        /// <param name="baseType">"oct" 八進位, "hex" 十六進位, "dec" 十進位</param>
        /// <param name="padding">補零長度，預設 3 位</param>
        /// <returns>格式化後字串，如 X100</returns>
        public static string Format(string prefix, int value, string baseType = "oct", int padding = 3)
        {
            string formatted = baseType.ToLower() switch
            {
                "oct" => Convert.ToString(value, 8).PadLeft(padding, '0'),
                "hex" => Convert.ToString(value, 16).ToUpper().PadLeft(padding, '0'),
                "dec" => value.ToString().PadLeft(padding, '0'),
                _ => throw new ArgumentException($"未知的進位格式：{baseType}")
            };

            return $"{prefix}{formatted}";
        }
        public class AddressBlockRange
        {
            public string Prefix { get; set; }
            public int Start { get; set; }
            public int End { get; set; }

            public override string ToString() => $"{Prefix}{Start:X3} ~ {Prefix}{End:X3}";
        }

        public static class IOBlockUtils
        {
            public static List<AddressBlockRange> ExpandToBlockRanges(IOSectionInfo section, int blockSize = 256)
            {
                var result = new List<AddressBlockRange>();

                for (int i = 0; i < section.BlockCount; i++)
                {
                    int start = section.StartAddress + i * blockSize;
                    int end = Math.Min(start + blockSize - 1, section.EndAddress);

                    result.Add(new AddressBlockRange
                    {
                        Prefix = section.Prefix,
                        Start = start,
                        End = end
                    });
                }

                return result;
            }
        }
        public static List<IOSectionInfo> Drill_test() ///測試用(實機測試不用)
        {
            using var context = new ApplicationDB();
            var drillList = context.Drill_IO.ToList();


            var result = new List<IOSectionInfo>();

            var xList = drillList
                .Where(d => d.address.StartsWith("X"))
                .Select(d => SafeParseAddressStrictly(d.address, 'X', 8)) // or base 8
                .Where(val => val.HasValue)
                .Select(val => val.Value)
                .ToList();


            if (xList.Any())
                result.Add(BuildSectionFormatted("X", xList, "hex"));

            var yList = drillList
                .Where(d => d.address.StartsWith("Y"))
                .Select(d => SafeParseAddressStrictly(d.address, 'Y', 8)) // or base 8
                .Where(val => val.HasValue)
                .Select(val => val.Value)
                .ToList();

            if (yList.Any())
                result.Add(BuildSectionFormatted("Y", yList, "hex"));

            return result;
        }
        public static int? SafeParseAddressStrictly(string address, char prefix, int baseFormat) ///測試用(實機測試不用)
        {
            if (string.IsNullOrWhiteSpace(address) || !address.StartsWith(prefix))
                return null;

            string raw = address.Trim().TrimStart(prefix);

            if (baseFormat == 8)
            {
                // 僅允許 0–7 的字元（八進位限制）
                if (!raw.All(c => "01234567".Contains(c)))
                    return null;
            }

            try
            {
                return Convert.ToInt32(raw, baseFormat);
            }
            catch
            {
                return null;
            }
        }
    }
}
