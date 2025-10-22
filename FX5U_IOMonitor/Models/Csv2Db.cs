using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FX5U_IOMonitor.Models
{
    internal class Csv2Db
    {

        internal static DataTable LoadCsv(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("找不到 CSV 檔案：" + filePath);

            DataTable dataTable = new DataTable();
            string delimiter = DetectDelimiter(filePath);
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
            {
                // 讀取所有行
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                if (lines.Length == 0)
                    throw new Exception("CSV 檔案為空");
                bool isHeader = true;
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // 使用自動檢測的分隔符號拆分行
                    string[] values = line.Split(new string[] { delimiter }, StringSplitOptions.None);

                    if (isHeader)
                    {
                        // 建立 DataTable 欄位名稱
                        foreach (string header in values)
                        {
                            string columnName = string.IsNullOrWhiteSpace(header) ? $"Column{dataTable.Columns.Count + 1}" : header.Trim();
                            if (columnName == "歷史使用次數")
                            {
                                //ReadXml();
                            }
                            dataTable.Columns.Add(columnName);
                        }
                        isHeader = false;
                    }
                    else
                    {
                        // 確保每列數量對應標題數
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            dataRow[i] = i < values.Length ? values[i].Trim() : "";
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }

                return dataTable;
            }

        }


        private static string DetectDelimiter(string filePath)
        {
            string firstLine = File.ReadLines(filePath, Encoding.UTF8).FirstOrDefault();
            if (string.IsNullOrEmpty(firstLine)) return ",";

            return firstLine.Contains(";") ? ";" : ",";
        }




        //轉換Historical_usage_times 到 Historical_usage
        public static List<HistoryRecord> ConvertToHistoryRecords(string[] data)
        {
            List<HistoryRecord> records = new List<HistoryRecord>();

            foreach (var line in data)
            {
                var parts = line.Split(';'); // 使用逗號分隔字串
                if (parts.Length == 3) // 確保資料格式正確
                {
                    if (int.TryParse(parts[2], out int usageCount)) // 確保 UsageCount 可轉換為數字
                    {
                        records.Add(new HistoryRecord
                        {
                            StartTime = parts[0],   // 設定開始時間
                            EndTime = parts[1],     // 設定結束時間
                            UsageCount = usageCount // 設定使用次數
                        });
                    }
                }
            }

            return records;
        }


        // 完全斷線後狀態監控
        public static void ResetCurrentSingle(List<IO_DataBase> dataList)
        {
            if (dataList == null || dataList.Count == 0)
            {
                return;
            }

            foreach (var item in dataList)
            {
                item.current_single = null;
            }
        }



        /// <summary>
        /// 警告資料初始化(excel轉DB)
        /// </summary>
        public static void Initialization_AlarmFromCSV(string csvPath)
        {
            using var reader = new StreamReader(csvPath, Encoding.UTF8);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            try
            {
                var records = csv.GetRecords<dynamic>().ToList(); // 先用 dynamic 解析欄位名稱
                var headers = csv.Context.Reader.HeaderRecord;

                // 判斷語系欄位（e.g., Error_en-US）
                var translationGroups = headers
                    .Where(h => h.StartsWith("Error_") || h.StartsWith("Possible_") || h.StartsWith("Repair_steps_"))
                    .GroupBy(h => h.Split('_')[1]) // 語系碼
                    .ToDictionary(
                        g => g.Key, // en-US
                        g => g.ToList()
                    );

                using var context = new ApplicationDB();

                int updateCount = 0, insertCount = 0;

                foreach (var row in records)
                {
                    string? address = row.address;
                    string? sourceMachine = row.SourceMachine;

                    if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(sourceMachine))
                        continue;

                    string drillType = sourceMachine.Trim().ToLower();
                    string sourceDb = drillType == "drill" ? "Drill" :
                                      drillType == "sawing" ? "Sawing" :
                                      address.StartsWith("L8") ? "Drill" :
                                      address.StartsWith("L9") ? "Sawing" : "Unknown";

                    string? ipcTable = row.IPC_table;
                    if (string.IsNullOrWhiteSpace(ipcTable))
                        continue;

                    var alarm = context.alarm
                        .Include(a => a.Translations)
                        .FirstOrDefault(a => a.IPC_table == ipcTable && a.SourceMachine == sourceDb);

                    if (alarm != null)
                    {
                        // 更新基本資料
                        alarm.IPC_table = row.IPC_table;
                        alarm.Description = row.Description;
                        alarm.Error = GetDynamicValue(row, "Error") ?? "";
                        alarm.Possible = GetDynamicValue(row, "Possible") ?? "";
                        alarm.Repair_steps = GetDynamicValue(row, "Repair_steps") ?? "";
                        alarm.classTag = row.ClassTag;

                        // 多語系翻譯更新
                        foreach (var lang in translationGroups.Keys)
                        {
                            string? error = GetDynamicValue(row, $"Error_{lang}");
                            string? possible = GetDynamicValue(row, $"Possible_{lang}");
                            string? steps = GetDynamicValue(row, $"Repair_steps_{lang}");

                            if (!string.IsNullOrWhiteSpace(error) || !string.IsNullOrWhiteSpace(possible) || !string.IsNullOrWhiteSpace(steps))
                            {
                                alarm.Setalarm_trans_language(lang, error ?? "", possible ?? "", steps ?? "");
                            }
                        }

                        updateCount++;
                    }
                    else
                    {
                        // 新增資料
                        var newAlarm = new Alarm
                        {
                            SourceMachine = sourceDb,
                            address = address,
                            IPC_table = row.IPC_table,
                            Description = row.Description,
                            Error = GetDynamicValue(row, "Error") ?? "",
                            Possible = GetDynamicValue(row, "Possible") ?? "",
                            Repair_steps = GetDynamicValue(row, "Repair_steps") ?? "",
                            classTag = row.ClassTag,
                            AlarmNotifyClass = 1,
                            AlarmNotifyuser = SD.Admin_Account
                        };

                        // 多語系初始化
                        foreach (var lang in translationGroups.Keys)
                        {
                            string? error = GetDynamicValue(row, $"Error_{lang}");
                            string? possible = GetDynamicValue(row, $"Possible_{lang}");
                            string? steps = GetDynamicValue(row, $"Repair_steps_{lang}");

                            if (!string.IsNullOrWhiteSpace(error) || !string.IsNullOrWhiteSpace(possible) || !string.IsNullOrWhiteSpace(steps))
                            {
                                newAlarm.Translations.Add(new AlarmTranslation
                                {
                                    LanguageCode = lang,
                                    Error = error ?? "",
                                    Possible = possible ?? "",
                                    Repair_steps = steps ?? "",
                                    AlarmId = newAlarm.Id
                                });
                            }
                        }

                        context.alarm.Add(newAlarm);
                        insertCount++;
                    }
                }

                context.SaveChanges();

                Console.WriteLine($"✅ 新增 {insertCount} 筆，更新 {updateCount} 筆 Alarm 資料。");
            }
            catch (HeaderValidationException ex)
            {
                Console.WriteLine("⚠️ CSV欄位不一致：" + ex.Message);
            }
        }

        // 工具：從 dynamic record 取值
        private static string? GetDynamicValue(dynamic row, string key)
        {
            var dict = row as IDictionary<string, object>;
            if (dict != null && dict.TryGetValue(key, out object? value))
                return value?.ToString();
            return null;
        }

        private class AlarmCsvRow
        {
            public string SourceMachine { get; set; }
            public string address { get; set; }

            [Name("IPC_table")]
            public string IPC_table { get; set; }

            public string Description { get; set; }
            public string Error { get; set; }
            public string Possible { get; set; }
            public string Repair_steps { get; set; }
            public string ClassTag { get; set; }


        }


        /// <summary>
        /// 初始化鋸帶齒數資料
        /// </summary>
        /// <param name="csvPath"></param>
        public static void Initialization_BladeTPIFromCSV(string csvPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Encoding = System.Text.Encoding.UTF8
            };

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, config);
            try
            {
                var records = csv.GetRecords<BladeTpiCsv>().ToList();

                using var context = new ApplicationDB();

                // 先抓出已存在的 blade_TPI_id 清單
                var existingTpiIds = context.Blade_brand_TPI.Select(t => t.blade_TPI_id).ToHashSet();

                int addCount = 0;
                foreach (var row in records)
                {
                    if (!existingTpiIds.Contains(row.blade_TPI_id))
                    {
                        var tpi = new Blade_brand_TPI
                        {
                            blade_TPI_id = row.blade_TPI_id,
                            blade_TPI_name = row.blade_TPI_name,
                            Machine_Number = 1
                        };

                        context.Blade_brand_TPI.Add(tpi);
                        addCount++;
                    }
                }

                context.SaveChanges();

                Console.WriteLine(addCount > 0
                    ? $"✅ 新增 {addCount} 筆 Blade_brand_TPI 資料。"
                    : "🟡 所有 Blade_brand_TPI 資料已存在，未新增任何資料。");
            }
            catch (HeaderValidationException ex)
            {
                Console.WriteLine("⚠️ CSV欄位不一致：" + ex.Message);
            }
        }
        private class BladeTpiCsv
        {

            public int blade_TPI_id { get; set; }
            public string blade_TPI_name { get; set; }
        }
        /// <summary>
        /// 初始化鋸帶廠牌型號資料
        /// </summary>
        /// <param name="csvPath"></param>
        public static void Initialization_BladeBrandFromCSV(string csvPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Encoding = System.Text.Encoding.UTF8,
                PrepareHeaderForMatch = args => args.Header.Trim(), // 🔥 去除首尾空白
                MissingFieldFound = null,
                HeaderValidated = null
            };

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, config);
            try
            {
                var records = csv.GetRecords<Bladebrandcsv>().ToList();

                using var context = new ApplicationDB();

                // 防呆：先查出已存在的品牌 ID
                var existingIds = context.Blade_brand.Select(b => b.blade_brand_id).ToHashSet();

                int addCount = 0;
                foreach (var row in records)
                {
                    if (!existingIds.Contains(row.blade_brand_id))
                    {
                        var brand = new Blade_brand
                        {

                            blade_brand_id = row.blade_brand_id,
                            blade_brand_name = row.blade_brand_name,
                            blade_material_id = row.blade_material_id,
                            blade_material_name = row.blade_material_name,
                            blade_Type_id = row.blade_Type_id,
                            blade_Type_name = row.blade_Type_name,
                            Machine_Number = 1
                        };

                        context.Blade_brand.Add(brand);
                        addCount++;
                    }
                }

                context.SaveChanges();

                Console.WriteLine(addCount > 0
                    ? $"✅ 新增 {addCount} 筆 Blade_brand 資料。"
                    : "🟡 所有 Blade_brand 資料已存在，未新增任何資料。");
            }
            catch (HeaderValidationException ex)
            {
                Console.WriteLine("⚠️ CSV欄位不一致：" + ex.Message);
            }
        }


        private class Bladebrandcsv
        {
            public int Id { get; set; }
            public int blade_brand_id { get; set; }
            public string blade_brand_name { get; set; } = "";
            public int blade_material_id { get; set; }
            public string blade_material_name { get; set; } = "";
            public int blade_Type_id { get; set; }
            public string blade_Type_name { get; set; } = "";
        }
        private class alarmcsv
        {
            public string SourceMachine { get; set; } = "";
            public string address { get; set; } = "";
            public string IPC_table { get; set; }
            public string Description { get; set; } = "";
            public string Error { get; set; } = "";
            public string? Possible { get; set; }
            public string? Repair_steps { get; set; }

            public string? classTag { get; set; }
            public int AlarmNotifyClass { get; set; }
            public string? AlarmNotifyuser { get; set; }


        }
        public static void UpdateTable(string tableName)
        {
            using var context = new ApplicationDB();
            var importService = new TableImportExportManager(context);

            switch (tableName)
            {
                case "Blade_brand_TPI":
                    importService.ImportCsvToTable<Blade_brand_TPI, BladeTpiCsv, int>(
                        tableName: tableName,
                        dbSet: context.Blade_brand_TPI,
                        mapFunction: csvRecord => new Blade_brand_TPI
                        {
                            blade_TPI_id = csvRecord.blade_TPI_id,
                            blade_TPI_name = csvRecord.blade_TPI_name,
                            Machine_Number = 1
                        },
                        entityKeySelector: entity => entity.blade_TPI_id,
                        recordKeySelector: csv => csv.blade_TPI_id,
                        enableSync: true
                    );
                    break;

                case "Blade_brand":
                    importService.ImportCsvToTable<Blade_brand, Bladebrandcsv, int>(
                          tableName: tableName,
                          dbSet: context.Blade_brand,
                          mapFunction: csvRecord => new Blade_brand
                          {
                              Id = csvRecord.Id,
                              blade_brand_id = csvRecord.blade_brand_id,
                              blade_brand_name = csvRecord.blade_brand_name,
                              blade_material_id = csvRecord.blade_material_id,
                              blade_material_name = csvRecord.blade_material_name,
                              blade_Type_id = csvRecord.blade_Type_id,
                              blade_Type_name = csvRecord.blade_Type_name,
                              Machine_Number = 1
                          },
                          entityKeySelector: entity => entity.Id,
                          recordKeySelector: csv => csv.Id,
                          enableSync: true
                      );
                    break;


                case "alarm":
                    importService.ImportdynamicCsvToTable<Alarm>(
                                tableName: tableName,
                                dbSetQuery: context.alarm.Include(a => a.Translations),
                                recordKeySelector: row => (string)((IDictionary<string, object>)row)["IPC_table"],
                                entityKeySelector: entity => entity.IPC_table,
                                mapFunction: (row, existing) => importService.MapAlarmWithTranslations(row, existing),
                                enableSync: true
                    );
                    break;
                case "MachineParameters":
                    importService.ImportCsvToTable<MachineParameter, MachineParameter, int>(
                           tableName: tableName,
                           dbSet: context.MachineParameters,
                           mapFunction: csvRecord => new MachineParameter
                           {
                               Id = csvRecord.Id,
                               Calculate = csvRecord.Calculate,
                               Calculate_type = csvRecord.Calculate_type,
                               Unit_transfer = csvRecord.Unit_transfer,
                               Read_type = csvRecord.Read_type,
                               Read_view = csvRecord.Read_view,
                               Read_address = csvRecord.Read_address,
                               Read_address_index = csvRecord.Read_address_index,
                               Read_addr = csvRecord.Read_addr,
                               Imperial_transfer = csvRecord.Imperial_transfer,
                               Write_address = csvRecord.Write_address,
                               Write_address_index = csvRecord.Write_address_index,
                           },
                           entityKeySelector: entity => entity.Id,
                           recordKeySelector: csv => csv.Id,
                           enableSync: true
                     );
                    break;
                default:
                    MessageBox.Show($"未支援的 tableName: {tableName}");
                    break;
            }
        }

        /// <summary>
        /// 初始化監控參數資料
        /// </summary>
        /// <param name="csvPath"></param>
        public static void Initialization_MachineprameterFromCSV(string csvPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Encoding = System.Text.Encoding.UTF8,
                HeaderValidated = null,
                MissingFieldFound = null,
                PrepareHeaderForMatch = args => args.Header.Trim(),
                IgnoreBlankLines = true
            };

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, config);
            try
            {
                var records = csv.GetRecords<MachineParameter>().ToList();

                using var context = new ApplicationDB();

                // 先取出資料庫中已有的 Machine_Name + blade_TPI_name 組合
                var existingKeys = context.MachineParameters
                    .Select(p => new { p.Machine_Name, p.Name })
                    .ToHashSet();

                int addCount = 0, updateCount = 0;

                foreach (var row in records)
                {
                    var existing = context.MachineParameters
                        .FirstOrDefault(p => p.Machine_Name == row.Machine_Name && p.Name == row.Name);

                    if (existing != null)
                    {
                        // 更新
                        existing.Calculate = row.Calculate;
                        existing.Calculate_type = row.Calculate_type;
                        existing.Unit_transfer = row.Unit_transfer;
                        existing.Read_type = row.Read_type;
                        existing.Read_view = row.Read_view;
                        existing.Read_address = row.Read_address;
                        existing.Read_address_index = row.Read_address_index;
                        existing.Read_addr = row.Read_addr;
                        existing.Imperial_transfer = row.Imperial_transfer;
                        existing.Write_address = row.Write_address;
                        existing.Write_address_index = row.Write_address_index;
                        existing.History_NumericValue = row.History_NumericValue;

                        updateCount++;
                    }
                    else
                    {
                        // 新增
                        var tpi = new MachineParameter
                        {
                            Machine_Name = row.Machine_Name,
                            Name = row.Name,
                            Calculate = row.Calculate,
                            Calculate_type = row.Calculate_type,
                            Unit_transfer = row.Unit_transfer,
                            Read_type = row.Read_type,
                            Read_view = row.Read_view,
                            Read_address = row.Read_address,
                            Read_address_index = row.Read_address_index,
                            Read_addr = row.Read_addr,
                            Imperial_transfer = row.Imperial_transfer,
                            Write_address = row.Write_address,
                            Write_address_index = row.Write_address_index,
                            History_NumericValue = row.History_NumericValue
                        };

                        context.MachineParameters.Add(tpi);
                        addCount++;
                    }
                }

                context.SaveChanges();

                Console.WriteLine($"✅ 新增 {addCount} 筆，更新 {updateCount} 筆 MachineParameters。");

            }
            catch (HeaderValidationException ex)
            {
                Console.WriteLine("⚠️ CSV欄位不一致：" + ex.Message);
            }
        }
        /// <summary>
        /// 初始化監控實體元件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="tableName"></param>
        public static void Initialization_MachineElementFromCSV(string targetMachine, string filePath)
        {
            DataTable excelData = LoadCsv(filePath);
            var errorBuilder = new System.Text.StringBuilder();

            using (var context = new ApplicationDB())
            {
                // 自動偵測語系欄位（以 Comment_ 開頭）
                var languageColumns = excelData.Columns.Cast<DataColumn>()
                    .Where(c => c.ColumnName.StartsWith("Comment_"))
                    .ToDictionary(
                        c => c.ColumnName,            // e.g., Comment_en-US
                        c => c.ColumnName.Replace("Comment_", "") // e.g., en-US
                    );

                // 建立機台記錄
                Machine_number? machine = context.Machine.FirstOrDefault(m => m.Name == targetMachine);
                if (machine == null)
                {
                    machine = new() { Name = targetMachine, IP_address = "", Port = 0, MC_Type = "MC3E" };
                    context.Machine.Add(machine);
                    context.SaveChanges();
                }

                // 統一地址型別判定
                var allAddresses = excelData.Rows.Cast<DataRow>()
                    .Select(row => row["Address"]?.ToString()?.Trim() ?? "")
                    .ToList();
                string unifiedBaseType = DetectUnifiedAddressBase(allAddresses);

                int rowIndex = 0;

                foreach (DataRow row in excelData.Rows)
                {
                    rowIndex++;

                    try
                    {
                        bool Type = bool.TryParse(row["MechanicalOrElectronic"]?.ToString(), out var temp) ? temp : false;
                        int ioInt = int.TryParse(row["IOType"]?.ToString(), out var i) ? i : 0;
                        bool IO = ioInt == 1;
                        string address = row["Address"]?.ToString()?.Trim() ?? $"未知_{rowIndex}";
                        string classTag = row["ClassTag"]?.ToString()?.Trim() ?? "未分類";
                        string name = row["Description"]?.ToString()?.Trim() ?? "未設定";
                        int maxLife = int.TryParse(row["MaxLife"]?.ToString(), out int maxVal) ? maxVal : 100;
                        int yellow = int.TryParse(row["YellowLimit"]?.ToString(), out int y) ? y : 20;
                        int red = int.TryParse(row["RedLimit"]?.ToString(), out int r) ? r : 10;

                        // 使用預設語系 comment 為主欄位內容
                        string defaultLang = "zh-TW";
                        string fallbackComment = languageColumns
                            .Where(lc => lc.Value == defaultLang)
                            .Select(lc => row[lc.Key]?.ToString()?.Trim())
                            .FirstOrDefault() ?? "";

                        var _IO = new MachineIO
                        {
                            address = address,
                            IOType = IO,
                            RelayType = Type ? RelayType.Machanical : RelayType.Electronic,
                            Machine_name = targetMachine,
                            Description = name,
                            baseType = unifiedBaseType,
                            Comment = fallbackComment,
                            ClassTag = classTag,
                            MaxLife = maxLife,
                            MountTime = DateTime.UtcNow,
                            UnmountTime = DateTime.UtcNow.AddMinutes(1),
                            Setting_yellow = yellow,
                            Setting_red = red,
                            Translations = languageColumns
                                .Select(lc =>
                                {
                                    string commentVal = row[lc.Key]?.ToString()?.Trim();
                                    if (string.IsNullOrWhiteSpace(commentVal)) return null;

                                    return new MachineIOTranslation
                                    {
                                        LanguageCode = lc.Value,
                                        Comment = commentVal
                                    };
                                })
                                .Where(t => t != null)
                                .ToList()
                        };

                        context.Machine_IO.Add(_IO);
                    }
                    catch (Exception ex)
                    {
                        errorBuilder.AppendLine($"❌ 第 {rowIndex} 行資料處理錯誤：{ex.Message}");
                    }
                }
                // 最後一次性顯示錯誤訊息
                if (errorBuilder.Length > 0)
                {
                    MessageBox.Show(errorBuilder.ToString(), "資料匯入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                context.SaveChanges();
                Console.WriteLine("✅ 資料已成功匯入資料庫。");
            }
        }

        /// <summary>
        /// 確定實體元件的資料格式為何
        /// </summary>
        /// <param name="allAddresses"></param>
        /// <returns></returns>
        public static string DetectUnifiedAddressBase(IEnumerable<string> allAddresses)
        {
            bool hasHex = false;
            bool hasDec = false;

            foreach (var addr in allAddresses)
            {
                if (string.IsNullOrWhiteSpace(addr)) continue;

                string raw = new string(addr
                    .Where(char.IsLetterOrDigit)
                    .SkipWhile(char.IsLetter)
                    .ToArray());

                if (raw.Any(c => "ABCDEFabcdef".Contains(c)))
                    hasHex = true;
                else if (raw.Any(c => "89".Contains(c)))
                    hasDec = true;
            }

            if (hasHex) return "hex";
            if (hasDec) return "dec";
            return "oct"; // 全部只含 0-7 的話視為八進位
        }

        public static void Initialization_FrequencyConverAlarmFromCSV(string csvPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Encoding = System.Text.Encoding.UTF8,
                PrepareHeaderForMatch = args => args.Header.Trim(), // 🔥 去除首尾空白
                MissingFieldFound = null,
                HeaderValidated = null
            };

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, config);
            try
            {
                var records = csv.GetRecords<FrequencyConverAlarmcsv>().ToList();

                using var context = new ApplicationDB();

                // 防呆：先查出已存在的品牌 ID
                var existingIds = context.FrequencyConverAlarm.Select(b => b.Id).ToHashSet();

                int addCount = 0;
                foreach (var row in records)
                {
                    if (!existingIds.Contains(row.Id))
                    {
                        var brand = new FrequencyConverAlarm
                        {
                            Id = row.Id,
                            FrequencyErrorDetail = row.FrequencyErrorDetail,
                            FrequencyAlarmID = Convert.ToInt32(row.FrequencyAlarmID, 16),
                            FrequencyAlarmInfo = row.FrequencyAlarmInfo,
                            FrequencyStatus = row.FrequencyStatus,
                            FrequencySolution = row.FrequencySolution

                        };

                        context.FrequencyConverAlarm.Add(brand);
                        addCount++;
                    }
                }

                context.SaveChanges();
            }
            catch (HeaderValidationException ex)
            {
                Console.WriteLine("⚠️ CSV欄位不一致：" + ex.Message);
            }
        }


        private class FrequencyConverAlarmcsv
        {
            public int Id { get; set; }
            public string FrequencyStatus { get; set; }
            public string FrequencyAlarmID { get; set; }
            public string FrequencyAlarmInfo { get; set; }
            public string FrequencyErrorDetail { get; set; }
            public string FrequencySolution { get; set; }

        }

        public static void Initialization_ServoDriveAlarmFromCSV(string csvPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Encoding = System.Text.Encoding.UTF8,
                PrepareHeaderForMatch = args => args.Header.Trim(),
                MissingFieldFound = null,
                HeaderValidated = null
            };

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, config);
            try
            {
                var records = csv.GetRecords<ServoDriveAlarmcsv>().ToList();

                using var context = new ApplicationDB();

                // 防呆：先查出已存在的品牌 ID
                var existingIds = context.ServoDriveAlarm.Select(b => b.Id).ToHashSet();

                int addCount = 0;
                foreach (var row in records)
                {
                    if (!existingIds.Contains(row.Id))
                    {
                        var Servoalarm = new ServoDriveAlarm
                        {
                            Id = row.Id,
                            ServoDriveAlarmId = Convert.ToInt32(row.ServoDriveAlarmId, 16),
                            ServoDriveAlarmInfo = row.ServoDriveAlarmInfo,
                            ServoDriveErrorDetail = row.ServoDriveErrorDetail,
                            ServoDriveSolution = row.ServoDriveSolution
                        };

                        context.ServoDriveAlarm.Add(Servoalarm);
                        addCount++;
                    }
                }

                context.SaveChanges();
            }
            catch (HeaderValidationException ex)
            {
                Console.WriteLine("⚠️ CSV欄位不一致：" + ex.Message);
            }
        }


        private class ServoDriveAlarmcsv
        {
            public int Id { get; set; }
            public required string ServoDriveAlarmId { get; set; }
            public required string ServoDriveAlarmInfo { get; set; }
            public required string ServoDriveErrorDetail { get; set; }
            public required string ServoDriveSolution { get; set; }

        }

    }

    

}
