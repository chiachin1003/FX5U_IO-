using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using FX5U_IOMonitor.Data;
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
                var records = csv.GetRecords<AlarmCsvRow>().ToList();

                using var context = new ApplicationDB();

                // 先取得已存在的 M_Address + SourceDbName 組合
                var existingKeys = context.alarm
                    .Select(a => new { a.M_Address, a.SourceDbName })
                    .ToHashSet();

                int addCount = 0;
                foreach (var row in records)
                {
                    if (string.IsNullOrWhiteSpace(row.M_CODE))
                        continue;

                    string drillType = row.SawingDrill?.Trim().ToLower();
                    string sourceDb = drillType == "drill" ? "Drill" :
                                      drillType == "sawing" ? "Sawing" :
                                      row.M_CODE.StartsWith("L8") ? "Drill" :
                                      row.M_CODE.StartsWith("L9") ? "Sawing" :
                                      "Unknown";

                    // 防呆：若此 M_Address + SourceDbName 已存在，就跳過
                    var key = new { M_Address = row.M_CODE, SourceDbName = sourceDb };
                    if (existingKeys.Contains(key))
                        continue;

                    var alarm = new Alarm
                    {
                        SourceDbName = sourceDb,
                        M_Address = row.M_CODE,
                        Description = row.料號,
                        Error = row.故障內容,
                        Possible = row.可能原因,
                        Repair_steps = row.維修步驟,
                        MountTime = DateTime.UtcNow,
                        UnmountTime = DateTime.UtcNow.AddMinutes(1),
                        classTag = row.ClassTag,
                        AlarmNotifyClass = 1,
                        AlarmNotifyuser = ""
                    };

                    context.alarm.Add(alarm);
                    addCount++;
                }

                context.SaveChanges();

                Console.WriteLine(addCount > 0
                    ? $"✅ 新增 {addCount} 筆 Alarm 資料。"
                    : "🟡 所有 Alarm 資料已存在，未新增任何資料。");
            }
            catch (HeaderValidationException ex)
            {
                Console.WriteLine("⚠️ CSV欄位不一致：" + ex.Message);
            }
        }

        private class AlarmCsvRow
        {
            public string SawingDrill { get; set; }
            public string M_CODE { get; set; }

            [Name("IPC table")]
            public string IPC_table { get; set; }

            public string 料號 { get; set; }
            public string 故障內容 { get; set; }
            public string 可能原因 { get; set; }
            public string 維修步驟 { get; set; }
            public string ClassTag { get; set; }


            [Name("發生時間(M_BIT ON)")]
            public string 發生時間 { get; set; }

            [Name("結束時間(M_BIT OFF)")]
            public string 結束時間 { get; set; }
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
            public int Id { get; set; }

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
            public int Id { get; set; }
            public string SourceDbName { get; set; } = "";
            public string M_Address { get; set; } = "";
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
                    importService.ImportCsvToTable<Blade_brand_TPI, BladeTpiCsv>(
                        tableName: tableName,
                        dbSet: context.Blade_brand_TPI,
                        mapFunction: (csvRecord, id) => new Blade_brand_TPI
                        {
                            Id = csvRecord.Id,
                            blade_TPI_id = csvRecord.blade_TPI_id,
                            blade_TPI_name = csvRecord.blade_TPI_name,
                            Machine_Number = 1
                        },
                        keySelector: entity => entity.Id,
                        enableSync: true
                    );
                    break;

                case "Blade_brand":
                    importService.ImportCsvToTable<Blade_brand, Bladebrandcsv>(
                        tableName: tableName,
                        dbSet: context.Blade_brand,
                        mapFunction: (csvRecord, id) => new Blade_brand
                        {
                            Id = id,
                            blade_brand_id = csvRecord.blade_brand_id,
                            blade_brand_name = csvRecord.blade_brand_name,
                            blade_material_id = csvRecord.blade_material_id,
                            blade_material_name = csvRecord.blade_material_name,
                            blade_Type_id = csvRecord.blade_Type_id,
                            blade_Type_name = csvRecord.blade_Type_name,
                            Machine_Number = 1
                        },
                        keySelector: entity => entity.Id,
                        enableSync: true
                    );
                    break;

                case "alarm":
                    importService.ImportCsvToTable<Alarm, alarmcsv>(
                        tableName: tableName,
                        dbSet: context.alarm,
                        mapFunction: (csvRecord, id) => new Alarm
                        {
                            Id = id,
                            SourceDbName = csvRecord.SourceDbName,
                            M_Address = csvRecord.M_Address,
                            Description = csvRecord.Description,
                            Error = csvRecord.Error,
                            Possible = csvRecord.Possible,
                            Repair_steps = csvRecord.Repair_steps,
                            classTag = csvRecord.classTag,
                            AlarmNotifyClass = csvRecord.AlarmNotifyClass,
                            AlarmNotifyuser = csvRecord.AlarmNotifyuser
                        },
                        keySelector: entity => entity.Id,
                        enableSync: true
                    );
                    break;

                //case "MachineParameters":
                //    importService.ImportCsvToTable<MachineParameter, MachineParameter>(
                //        tableName: tableName,
                //        dbSet: context.MachineParameters,
                //        mapFunction: (csvRecord, id) => new MachineParameter
                //        {
                //            Id = csvRecord.Id,
                //            Machine_Name = csvRecord.Machine_Name,
                //            Name = csvRecord.Name,
                //            Calculate = csvRecord.Calculate,
                //            Calculate_type = csvRecord.Calculate_type,
                //            Unit_transfer = csvRecord.Unit_transfer,
                //            Read_type = csvRecord.Read_type,
                //            Read_view = csvRecord.Read_view,
                //            Read_address = csvRecord.Read_address,
                //            Read_address_index = csvRecord.Read_address_index,
                //            Write_address = csvRecord.Write_address,
                //            Write_address_index = csvRecord.Write_address_index,
                //            History_NumericValue = csvRecord.History_NumericValue
                //        },
                //        keySelector: entity => entity.Id,
                //        enableSync: true
                //    );
                //    break;

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

                int addCount = 0;
                foreach (var row in records)
                {
                    var key = new { row.Machine_Name, row.Name };
                    if (existingKeys.Contains(key))
                        continue;

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
                        Write_address = row.Write_address,
                        Write_address_index = row.Write_address_index,
                        History_NumericValue = row.History_NumericValue
                    };

                    context.MachineParameters.Add(tpi);
                    addCount++;
                }

                context.SaveChanges();

                Console.WriteLine(addCount > 0
                    ? $"✅ 新增 {addCount} 筆 MachineParameters 資料。"
                    : "🟡 所有 MachineParameters 資料已存在，未新增任何資料。");
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
            using (var context = new ApplicationDB())
            {
                // 確認目標機台是否已經存在
                Machine_number? machine = context.index.FirstOrDefault(m => m.Name == targetMachine);
                if (machine == null)
                {
                    machine = new() { Name = targetMachine };
                    context.index.Add(machine);
                    context.SaveChanges();
                }
                // 確認監控元件的輸出及輸入繼電器屬於哪一個型態
                var allAddresses = excelData.Rows.Cast<DataRow>()
                                .Select(row => row["資料地址"]?.ToString()?.Trim() ?? "")
                                .ToList();

                // 全部地址統一偵測進位格式
                string unifiedBaseType = DetectUnifiedAddressBase(allAddresses);

                int rowIndex = 0;
                foreach (DataRow row in excelData.Rows)
                {

                    rowIndex++;
                    // 檢查所有必須欄位是否存在
                    string[] requiredColumns = new[]
                    {
                        "機械式/電子式", "點位輸出入", "資料地址", "設備描述", "分類", "更換料號",
                        "最大壽命",  "黃燈登錄", "紅燈登錄"
                    };

                    foreach (var colName in requiredColumns)
                    {
                        if (!excelData.Columns.Contains(colName))
                        {
                            MessageBox.Show($"❌ 第 {rowIndex} 行資料錯誤：找不到必要欄位「{colName}」");
                        }
                    }

                    try
                    {
                        bool Type = bool.TryParse(row["機械式/電子式"]?.ToString(), out var temp) ? temp : false;
                        int ioInt = int.TryParse(row["點位輸出入"]?.ToString(), out var i) ? i : 0;
                        string machine_name = targetMachine;
                        bool IO = ioInt == 1;
                        string address = row["資料地址"]?.ToString()?.Trim() ?? "未知地址";

                        bool isDuplicate = context.Machine_IO.Any(io => io.address == address);

                        string description = row["設備描述"]?.ToString()?.Trim() ?? "無描述";
                        string comment = description;
                        string classTag = row["分類"]?.ToString()?.Trim() ?? "未分類";
                        string name = row["更換料號"]?.ToString()?.Trim() ?? "未設定";

                        int maxLife = int.TryParse(row["最大壽命"]?.ToString(), out int maxVal) ? maxVal : 100;
                        int yellow = int.TryParse(row["黃燈登錄"]?.ToString(), out int y) ? y : 20;
                        int red = int.TryParse(row["紅燈登錄"]?.ToString(), out int r) ? r : 10;


                        var _IO = new MachineIO
                        {
                            address = address,
                            IOType = IO,
                            RelayType = Type ? RelayType.Machanical : RelayType.Electronic,
                            Machine_name = machine_name,
                            Description = name,
                            baseType = unifiedBaseType,
                            Comment = comment,
                            ClassTag = classTag,
                            MaxLife = maxLife,
                            MountTime = DateTime.UtcNow,
                            UnmountTime = DateTime.UtcNow.AddMinutes(1),
                            Setting_yellow = yellow,
                            Setting_red = red

                        };

                        context.Machine_IO.Add(_IO);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"❌ 第 {rowIndex} 行處理資料時發生錯誤：{ex.Message}");
                    }
                }


                context.SaveChanges();
                Console.WriteLine("✅ 資料已成功匯入資料庫。");
            };


        }
        /// <summary>
        /// 確定實體元件的資料格式為何
        /// </summary>
        /// <param name="allAddresses"></param>
        /// <returns></returns>
        private static string DetectUnifiedAddressBase(IEnumerable<string> allAddresses)
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

        public static void importcsv()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV 檔案 (*.csv)|*.csv",
                Title = "選擇要匯入的 CSV 檔案"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true,
                        Encoding = System.Text.Encoding.UTF8,
                        PrepareHeaderForMatch = args => args.Header.Trim(),
                        MissingFieldFound = null,
                        HeaderValidated = null
                    };

                    using var reader = new StreamReader(openFileDialog.FileName);
                    using var csv = new CsvReader(reader, config);
                    var records = csv.GetRecords<Bladebrandcsv>().ToList();

                    using var context = new ApplicationDB();

                    // 防呆1：取得目前資料庫最大 Id
                    int maxDbId = context.Blade_brand.Any()
                        ? context.Blade_brand.Max(b => b.Id)
                        : 0;
                    int nextId = maxDbId + 1;

                    // 防呆2：檢查 CSV 內部是否重複，並補齊 Id
                    var idSet = new HashSet<int>();
                    foreach (var row in records)
                    {
                        if (row.Id <= 0)
                        {
                            row.Id = nextId++;
                        }

                        if (!idSet.Add(row.Id))
                        {
                            MessageBox.Show($"❌ 匯入失敗：CSV 中出現重複的 Id: {row.Id}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    // 防呆3：檢查 Id 是否為連續數列

                    var idListSorted = idSet.OrderBy(id => id).ToList();
                    for (int i = 1; i < idListSorted.Count; i++)
                    {
                        if (idListSorted[i] != idListSorted[i - 1] + 1)
                        {
                            var result = MessageBox.Show(
                                $"⚠️ 偵測到 CSV 中的 Id 不連續（例如 {idListSorted[i - 1]} ➜ {idListSorted[i]}）\n是否仍要繼續匯入？",
                                "Id 連續性警告",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

                            if (result == DialogResult.No)
                                return;
                            else
                                break;
                        }
                    }
                    var dbData = context.Blade_brand.ToList();
                    var dbDict = dbData.ToDictionary(b => b.Id);

                    var existingData = context.Blade_brand
                        .AsEnumerable()
                        .GroupBy(b => b.Id) // 如果有重複會擋下來
                        .ToDictionary(g => g.Key, g => g.First());

                    int insertCount = 0, updateCount = 0, deleteCount = 0;

                    foreach (var row in records)
                    {
                        if (existingData.TryGetValue(row.Id, out var existing))
                        {
                            bool updated = false;
                            if (existing.blade_brand_id != row.blade_brand_id) { existing.blade_brand_id = row.blade_brand_id; updated = true; }
                            if (existing.blade_brand_name != row.blade_brand_name) { existing.blade_brand_name = row.blade_brand_name; updated = true; }
                            if (existing.blade_material_id != row.blade_material_id) { existing.blade_material_id = row.blade_material_id; updated = true; }
                            if (existing.blade_material_name != row.blade_material_name) { existing.blade_material_name = row.blade_material_name; updated = true; }
                            if (existing.blade_Type_id != row.blade_Type_id) { existing.blade_Type_id = row.blade_Type_id; updated = true; }
                            if (existing.blade_Type_name != row.blade_Type_name) { existing.blade_Type_name = row.blade_Type_name; updated = true; }

                            if (updated) updateCount++;
                        }
                        else
                        {
                            context.Blade_brand.Add(new Blade_brand
                            {
                                Id = row.Id,
                                blade_brand_id = row.blade_brand_id,
                                blade_brand_name = row.blade_brand_name,
                                blade_material_id = row.blade_material_id,
                                blade_material_name = row.blade_material_name,
                                blade_Type_id = row.blade_Type_id,
                                blade_Type_name = row.blade_Type_name,
                                Machine_Number = 1
                            });
                            insertCount++;
                        }
                    }
                    // 同步刪除資料庫中多餘的
                    var csvIds = records.Select(r => r.Id).ToHashSet();
                    var toDelete = dbData.Where(d => !csvIds.Contains(d.Id)).ToList();

                    if (toDelete.Any())
                    {
                        context.Blade_brand.RemoveRange(toDelete);
                        deleteCount = toDelete.Count;
                    }

                    context.SaveChanges();

                    MessageBox.Show($"✅ 匯入完成：新增 {insertCount} 筆、更新 {updateCount} 筆、刪除 {deleteCount} 筆！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ 匯入失敗：" + ex.Message);
                }
            }
        }





      

    }

    

}
