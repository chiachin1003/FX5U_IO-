
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
        public static List<now_single> ConvertPlcToNowSingle(bool[] plc, string InorOut, int startIndex, string radix = "oct")
        {
            if (plc == null || plc.Length == 0 || string.IsNullOrEmpty(InorOut))
            {
                throw new ArgumentException("plc 陣列或 InorOut 不能為空.");
            }
            if (startIndex < 0)
            {
                throw new ArgumentException("startIndex 不能為負數.");
            }
            int baseNum = radix.ToLower() switch
            {
                "hex" => 16,
                "dec" => 10,
                _ => 8 // 預設為八進位
            };

            return plc.Select((value, index) =>
            {
                // 根據指定進位 (例如 8, 10, 16) 轉換 Machine
                string numberPart = Convert.ToString(startIndex + index, baseNum);

                // 如果是 16 進位 → 轉為大寫
                if (baseNum == 16)
                    numberPart = numberPart.ToUpper();

                // 最後組合位址字串
                return new now_single
                {
                    address = InorOut + numberPart.PadLeft(2, '0'),
                    current_single = value
                };
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
            var ioList = context.Machine_IO.ToList();
            foreach (var now in nowList)
            {
                var io = ioList.FirstOrDefault(d =>d.Machine_name == tableName && d.address == now.address);
                if (io != null)
                {
                    io.current_single = now.current_single;
                    updatedCount++;
                }
            }

        
            context.SaveChanges(); // ✅ 寫入資料庫
            Console.WriteLine($"✅ 成功更新 {updatedCount} 筆 current_single 至資料表 {tableName}。");

            return updatedCount;
        }
        public static void UpdatealarmCurrentSingleToDB(List<now_single> nowList)
        {
            if (nowList == null || nowList.Count == 0)
            {
                Console.WriteLine("⚠️ nowList 為空，未更新資料庫。");
            }

            using var context = new ApplicationDB();

            var ioList = context.alarm.ToList();
            foreach (var now in nowList)
            {
                var io = ioList.FirstOrDefault(d => d.address == now.address);
                if (io != null)
                {
                    io.current_single = now.current_single;
                }
            }
                      
            context.SaveChanges(); // ✅ 寫入資料庫

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
        public static List<now_single> Convert_Single(bool[] plc, string InorOut, int startIndex)
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

        public static List<now_number> Convert_wordsingle(ushort[] plcWords, string prefix, int startIndex)
        {
            if (plcWords == null || plcWords.Length == 0 || string.IsNullOrEmpty(prefix))
            {
                throw new ArgumentException("plcWords 或 prefix 不能為空.");
            }
            if (startIndex < 0)
            {
                throw new ArgumentException("startIndex 不能為負數.");
            }

            return plcWords.Select((value, index) => new now_number
            {
                address = prefix + (startIndex + index),   // 例：D100、ZR500
                current_number = value                         // 擴充欄位：數值型
            }).ToList();
        }

        public static List<IOSectionInfo> AnalyzeIOSections(string Machine_Name, string format)
        {
            using var context = new ApplicationDB();

            // 根據來源選擇對應的表
            var ioList = context.Machine_IO
                .Where(a => a.Machine_name == Machine_Name)
                .ToList();
          

            // 根據格式選擇進位制轉換方式
            int radix = format.ToLower() switch
            {
                "hex" => 16,
                "oct" => 8,
                _ => throw new ArgumentException("Invalid format. Use 'hex' or 'oct'.")
            };

            var result = new List<IOSectionInfo>();

            foreach (var prefix in new[] { "X", "Y" })
            {
               
                var list = ioList
                    .Where(d => d.address.StartsWith(prefix))
                    .Select(d => SafeParseAddressStrictly(d.address, prefix[0], radix))
                    .Where(val => val.HasValue)
                    .Select(val => val.Value)
                    .ToList();

                if (list.Any())
                {
                    result.Add(BuildSectionFormatted(prefix, list, format));
                }
            }

            return result;
        }
       
        public static List<IOSectionInfo> Alarm_trans()
        {
            using (var context = new ApplicationDB())
            {
                var alarmList = context.alarm.ToList();

                var result = new List<IOSectionInfo>();

                var LList = alarmList
                    .Where(d => d.address.StartsWith("L"))
                    .Select(d => Convert.ToInt32(d.address.TrimStart('L')))
                    .OrderBy(a => a)
                    .ToList();

                if (LList.Any())
                {
                    result.Add(BuildSectionFormatted("L", LList, "dec"));
                }
                return result;
            };
           
        }

       
        /// <summary>
        /// 地址數值轉換及切割
        /// </summary>
        /// <param name="addresses"></param>
        /// <param name="numberBase"></param>
        /// <returns></returns>
        public static List<IOSectionInfo> SplitAddressSections(List<string> addresses, string numberBase = "dec") // dec / hex / oct
        {
            var result = new List<IOSectionInfo>();

            // 依 prefix 分組（用所有開頭字母）
            var prefixGroups = addresses
                .GroupBy(a => new string(a.TakeWhile(char.IsLetter).ToArray()));  // 可處理 ZR類型

            foreach (var group in prefixGroups)
            {
                string prefix = group.Key;

                // 去除 prefix，保留數字部分（如 D100 → 100）
                var numericList = group
                    .Select(addr =>
                    {
                        var numPart = new string(addr.SkipWhile(char.IsLetter).ToArray());
                        return numberBase switch
                        {
                            "hex" => Convert.ToInt32(numPart, 16),
                            "oct" => Convert.ToInt32(numPart, 8),
                            _ => Convert.ToInt32(numPart)
                        };
                    })
                    .Distinct()
                    .OrderBy(val => val)
                    .ToList();

                // 每 256 筆一段
                for (int i = 0; i < numericList.Count; i += 256)
                {
                    var segment = numericList.Skip(i).Take(256).ToList();
                    result.Add(BuildSectionFormatted(prefix, segment, numberBase));
                }
            }
            return result;
        }
      
        /// <summary>
        /// 取實體元件單次循環讀取需要多少個迴圈
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="addrList"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
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
            public int Range { get; set; }


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
            public static List<AddressBlockRange> ExpandToBlockRanges(
            IOSectionInfo section,
            string mcType,
            int defaultBlockSize = 256)
            {
                var result = new List<AddressBlockRange>();

                // 依機型決定單位：MC1E=128，其它=defaultBlockSize(預設256)
                int unit = string.Equals(mcType, "MC1E", StringComparison.OrdinalIgnoreCase) ? 128 : defaultBlockSize;

                // 將 [StartAddress..EndAddress] 依 unit 切段
                int total = section.EndAddress - section.StartAddress + 1;   // 總長度（含頭尾）
                if (total <= 0) return result;

                int blocks = (total + unit - 1) / unit; // ceil(total / unit)

                for (int i = 0; i < blocks; i++)
                {
                    int start = section.StartAddress + i * unit;
                    int end = Math.Min(start + unit - 1, section.EndAddress);

                    result.Add(new AddressBlockRange
                    {
                        Prefix = section.Prefix,
                        Start = start,
                        End = end,
                        Range = end - start + 1 // 可能是 unit，也可能是最後一段不足 unit
                    });
                }

                return result;
            }
        }
     
        public static int? SafeParseAddressStrictly(string address, char prefix, int baseFormat) 
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
