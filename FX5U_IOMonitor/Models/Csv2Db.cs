using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using FX5U_IOMonitor.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FX5U_IOMonitor.Models
{
    internal class Csv2Db
    {

        /// <summary>
        /// 初始化 IO_data 資料，並匯入 DataStore 的靜態清單
        /// 數據初始化
        /// </summary>
        /// <param name="filePath">Excel 文件路徑</param>
        internal static List<IO_DataBase> Initiali_Data(string filePath)
        {
            List<IO_DataBase> DataList = new List<IO_DataBase>();
            try
            {
                DataTable excelData = LoadCsv(filePath);

                foreach (DataRow row in excelData.Rows)
                {
                    bool machine = bool.TryParse(row["機械式/電子式"]?.ToString(), out var temp) ? temp : false;
                    int ioInt = int.TryParse(row["點位輸出入"]?.ToString(), out var i) ? i : 0;
                    bool IO = ioInt == 1;

                    string address = row["資料地址"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["資料地址"].ToString()) ? row["資料地址"].ToString() : "未知地址";
                    string equipmentDescription = row["設備描述"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["設備描述"].ToString()) ? row["設備描述"].ToString() : "無描述";
                    string Name = row["更換料號"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["更換料號"].ToString()) ? row["更換料號"].ToString() : "未設定";

                    int equipment_use = row["當前使用次數"] != DBNull.Value && int.TryParse(row["當前使用次數"].ToString(), out int useValue) ? useValue : 0;
                    int MaxLife = row["最大壽命"] != DBNull.Value && int.TryParse(row["最大壽命"].ToString(), out int maxValue) ? maxValue : 100;
                    string equipmentTag = row["分類"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["分類"].ToString()) ? row["分類"].ToString() : "未分類";

                    int green = row["綠燈登錄"] != DBNull.Value && int.TryParse(row["綠燈登錄"].ToString(), out int Green) ? Green : 80;
                    int yellow = row["黃燈登錄"] != DBNull.Value && int.TryParse(row["黃燈登錄"].ToString(), out int Yellow) ? Yellow : 20;
                    int red = row["紅燈登錄"] != DBNull.Value && int.TryParse(row["紅燈登錄"].ToString(), out int Red) ? Red : 10;
                    double rul = row["剩餘壽命"] != DBNull.Value && double.TryParse(row["剩餘壽命"].ToString(), out double RUL) ? RUL : 100;

                    string Part_InstallationTime = row["當前零件安裝時間"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["當前零件安裝時間"].ToString()) ? row["當前零件安裝時間"].ToString() : "未設定";
                    string Part_RemovalTime = row["當前零件卸除時間"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["當前零件卸除時間"].ToString()) ? row["當前零件卸除時間"].ToString() : "未設定";
                    string use = row["歷史使用次數"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["歷史使用次數"].ToString()) ? row["歷史使用次數"].ToString() : "";



                    var ioData = new IO_DataBase
                    {
                        address = address,
                        IO = IO,
                        IsMechanical = machine,
                        equipmentDescription = Name,
                        MaxLife = MaxLife,
                        equipment_use = equipment_use,
                        Part_InstallationTime = Part_InstallationTime,
                        Part_RemovalTime = Part_RemovalTime,
                        Setting_green = green,
                        Setting_yellow = yellow,
                        Setting_red = red,
                        ClassTag = equipmentTag,
                        Historical_usage = use,
                        RUL = rul,
                        Comment = equipmentDescription,

                        //current_single = false
                    };


                    DataList.Add(ioData);
                }

                return DataList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤: {ex.Message}");
                return new List<IO_DataBase>();
            }
        }

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
        //更新整個Database
        public static void SaveToCsv(string filePath, List<IO_DataBase> dataList)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                // 寫入 CSV 標題
                writer.WriteLine("資料地址,點位輸出入,機械式/電子式,分類,更換料號,設備描述,最大壽命,當前使用次數,剩餘壽命,綠燈登錄,黃燈登錄,紅燈登錄,當前零件安裝時間,當前零件卸除時間,歷史使用時間,歷史使用次數");

                foreach (var item in dataList)
                {
                    string line = $"{item.address},{item.IO},{item.IsMechanical},{item.ClassTag},{item.equipmentDescription},{item.Comment}," +
                                  $"{item.MaxLife},{item.equipment_use},{item.RUL}," +
                                  $"{item.Setting_green},{item.Setting_yellow},{item.Setting_red}," +
                                  $"{item.Part_InstallationTime},{item.Part_RemovalTime}," +
                                  $"{item.Historical_usage_times},{item.Historical_usage}";

                    writer.WriteLine(line);
                }
            }

            Console.WriteLine($"已成功寫入 CSV: {filePath}");
        }

        //更新單資料地址資料
        public static void UpdateCsv(string filePath, List<IO_DataBase> dataList, string targetAddress)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("CSV 檔案不存在，無法更新");
                return;
            }

            // 讀取整個 CSV 檔案
            List<string> lines = File.ReadAllLines(filePath, Encoding.UTF8).ToList();

            // 確保 CSV 至少有標題
            if (lines.Count < 2)
            {
                Console.WriteLine("CSV 檔案內容異常");
                return;
            }

            // 找到對應 Address 的資料
            for (int i = 1; i < lines.Count; i++) // 跳過標題行，從第 2 行開始遍歷
            {
                string[] columns = lines[i].Split(',');

                if (columns.Length > 0 && columns[0] == targetAddress) // 確保有欄位且 Address 匹配
                {
                    var item = dataList.FirstOrDefault(d => d.address == targetAddress);
                    if (item != null)
                    {
                        // 更新該行
                        lines[i] = $"{item.address},{item.IO},{item.IsMechanical},{item.ClassTag},{item.equipmentDescription},{item.Comment}," +
                       $"{item.MaxLife},{item.equipment_use},{item.RUL}," +
                       $"{item.Setting_green},{item.Setting_yellow},{item.Setting_red}," +
                       $"{item.Part_InstallationTime},{item.Part_RemovalTime}," +
                       $"{item.Historical_usage_times} ," +
                       $"\"{item.Historical_usage.Replace("\"", "\"\"")}\"";

                        Console.WriteLine($"已更新 {targetAddress} 的數據");
                        break; // 找到並更新後跳出迴圈
                    }
                }
            }

            // 將更新後的內容寫回 CSV
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }

        // 將 List<HistoryRecord> 轉換成 XML 字串
        public static string SerializeToXml(List<HistoryRecord> records)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<HistoryRecord>));

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", ""); // 移除 xmlns:xsi 和 xmlns:xsd

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, records, ns); // 序列化時不加入 xmlns
                return writer.ToString();
            }
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



        /// 將csv檔資料轉譯成DB格式
        public static void SaveMachineIODb(List<IO_DataBase> machineData, string tableName)
        {
            using (var context = new ApplicationDB())
            {
                context.Database.EnsureCreated();
                switch (tableName)
                {
                    case "Drill":


                        foreach (var item in machineData)
                        {
                            var newIO = new Drill_MachineIO
                            {
                                address = item.address,
                                IOType = item.IO,
                                RelayType = item.IsMechanical ? RelayType.Machanical : RelayType.Electronic,
                                Description = item.equipmentDescription,
                                Comment = item.Comment,
                                ClassTag = item.ClassTag,
                                MaxLife = item.MaxLife,
                                equipment_use = item.equipment_use,
                                Setting_green = item.Setting_green,
                                Setting_yellow = item.Setting_yellow,
                                Setting_red = item.Setting_red,
                                percent = item.percent,
                                MountTime = DateTime.TryParse(item.Part_InstallationTime, out DateTime mountTime)
                                    ? mountTime : DateTime.Now,
                                UnmountTime = DateTime.TryParse(item.Part_RemovalTime, out DateTime unmountTime)
                                    ? unmountTime : DateTime.Now.AddDays(30)
                            };

                            context.Drill_IO.Add(newIO);
                        }
                        break;
                    case "Swing":


                        foreach (var item in machineData)
                        {
                            var newSwing = new Sawing_MachineIO
                            {
                                address = item.address,
                                IOType = item.IO,  // 明確轉換
                                RelayType = item.IsMechanical ? RelayType.Machanical : RelayType.Electronic,
                                Description = item.equipmentDescription,
                                Comment = item.Comment,
                                ClassTag = item.ClassTag,
                                MaxLife = item.MaxLife,
                                equipment_use = item.equipment_use,
                                Setting_green = item.Setting_green,
                                Setting_yellow = item.Setting_yellow,
                                Setting_red = item.Setting_red,
                                percent = item.percent,
                                MountTime = DateTime.TryParse(item.Part_InstallationTime, out DateTime mountTime)
                                    ? mountTime : DateTime.Now,
                                UnmountTime = DateTime.TryParse(item.Part_RemovalTime, out DateTime unmountTime)
                                    ? unmountTime : DateTime.Now.AddDays(30)
                            };
                            context.Sawing_IO.Add(newSwing);
                        }
                        break;
                    default:
                        throw new ArgumentException($"未知的表格名稱: {tableName}");
                }
                context.SaveChanges();

            }



        }


        public static class AlarmImporter
        {
            public static void ImportFromCSV(string csvPath)
            {
                using var reader = new StreamReader(csvPath, Encoding.UTF8);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

               

                var records = csv.GetRecords<AlarmCsvRow>().ToList();

                using var context = new ApplicationDB();
                foreach (var row in records)
                {
                    if (string.IsNullOrWhiteSpace(row.M_CODE)) continue;

                    string drillType = row.SawingDrill?.Trim().ToLower();
                    string sourceDb = drillType == "drill" ? "Drill_IO" :
                                      drillType == "sawing" ? "Sawing_IO" :
                                      row.M_CODE.StartsWith("L8") ? "Drill_IO" :
                                      row.M_CODE.StartsWith("L9") ? "Sawing_IO" :
                                      "Unknown_IO";
                    var alarm = new Alarm
                    {
                        SourceDbName = sourceDb,
                        M_Address = row.M_CODE,
                        Description = row.料號,
                        Error = row.故障內容,
                        Possible = row.可能原因,
                        Repair_steps = row.維修步驟,
                        MountTime = DateTime.Now,
                        UnmountTime = DateTime.Now.AddMinutes(1),
                        classTag = row.ClassTag
                    };

                    context.alarm.Add(alarm);
                }

                context.SaveChanges();
                Console.WriteLine("✅ Alarm 資料已成功匯入！");
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
        }



    }
}