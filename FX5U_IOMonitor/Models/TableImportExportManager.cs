using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using FX5U_IOMonitor.Config;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Login;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace FX5U_IOMonitor.Models
{
    public class TableImportExportManager
    {
        private readonly ApplicationDB _context;

        public TableImportExportManager(ApplicationDB context)
        {
            _context = context;
        }
        /// <summary>
        /// 下載帶語系的警告資料表
        /// </summary>
        /// <param name="exportPath"></param>
        public static void Export_AlarmToCSV(string mode = "auto")
        {
            using var context = new ApplicationDB();

            var alarms = context.alarm
                .Include(a => a.Translations)
                .ToList();

            // 收集所有用到的語系
            var languageCodes = alarms
                .SelectMany(a => a.Translations.Select(t => t.LanguageCode))
                .Distinct()
                .ToList();

            // 動態物件建立
            var exportRows = new List<Dictionary<string, object>>();

            foreach (var alarm in alarms)
            {
                var row = new Dictionary<string, object>
                {
                    ["address"] = alarm.address,
                    ["SourceMachine"] = alarm.SourceMachine,
                    ["IPC_table"] = alarm.IPC_table,
                    ["Description"] = alarm.Description,
                    ["classTag"] = alarm.classTag
                };

                // 加入各語系翻譯欄位
                foreach (var lang in languageCodes)
                {
                    var trans = alarm.Translations.FirstOrDefault(t => t.LanguageCode == lang);

                    row[$"Error_{lang}"] = trans?.Error ?? "";
                    row[$"Possible_{lang}"] = trans?.Possible ?? "";
                    row[$"Repair_steps_{lang}"] = trans?.Repair_steps ?? "";
                }

                exportRows.Add(row);
            }

            // 寫入 CSV

            string filePath;
            if (mode.ToLower() == "manual")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV 檔案 (*.csv)|*.csv",
                    FileName = $"Alarm.csv",
                    Title = $"儲存資料表為Alarm"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                filePath = saveFileDialog.FileName;
            }
            else
            {
                string downloadFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Downloads"
                );

                Directory.CreateDirectory(downloadFolder);
                filePath = Path.Combine(downloadFolder, $"Alarm.csv");
            }


            try
            {
                using var writer = new StreamWriter(filePath, false, new UTF8Encoding(true));

                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

                if (exportRows.Count > 0)
                {
                    var headerKeys = exportRows.First().Keys.ToList();

                    foreach (var header in headerKeys)
                    {
                        csv.WriteField(header);
                    }

                    csv.NextRecord();

                    foreach (var row in exportRows)
                    {
                        foreach (var key in headerKeys)
                        {
                            csv.WriteField(row[key]);
                        }
                        csv.NextRecord();
                    }
                }
                MessageBox.Show($"匯出完成：\n📄 {filePath}", "匯出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex) 
            {
                MessageBox.Show($"匯出失敗");

            }
        }

        /// <summary>
        /// 下載當前資料庫資料表
        /// </summary>
        /// <param name="tableName"> 要下載的資料表名稱</param> 
        /// <param name="columns"> 是否指定column</param> 
        /// <param name="mode"> "manual"=提供使用者選擇 ，"auto" 自動至下載資料夾</param> 

        public static void ExportTableToCsv(string tableName, string mode = "auto")
        {
            // 對應每個資料表匯出的欄位
            var exportColumns = new Dictionary<string, string[]>
            {
                ["Blade_brand_TPI"] = new[] { "blade_TPI_id", "blade_TPI_name" },
                ["Blade_brand"] = new[] {"Id",
            "blade_brand_id", "blade_brand_name",
            "blade_material_id", "blade_material_name",
            "blade_Type_id", "blade_Type_name"
        },
                ["alarm"] = new[] {
            "SourceMachine", "address","IPC_table", "Description",
            "Error", "Possible", "Repair_steps",
            "classTag"}
            };

            string connString = $"Host={DbConfig.Local.IpAddress};Port={DbConfig.Local.Port};Database=element;Username={DbConfig.Local.UserName};Password={DbConfig.Local.Password}";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            string quotedTable = QuotePostgresIdentifier(tableName);

            if (!exportColumns.TryGetValue(tableName, out var columns) || columns.Length == 0)
            {
                //MessageBox.Show($"❌ 未定義 {tableName} 的欄位匯出順序。", "匯出失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 取得 Id 欄位
            var checkCmd = new NpgsqlCommand(@"
                        SELECT column_name FROM information_schema.columns 
                        WHERE table_name = lower(@name)", conn);
            checkCmd.Parameters.AddWithValue("@name", tableName);

            bool hasId = false;
            using (var reader = checkCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader.GetString(0).Equals("Id", StringComparison.OrdinalIgnoreCase))
                    {
                        hasId = true;
                        break;
                    }
                }
            }

            string selectFields = string.Join(", ", columns.Select(QuotePostgresIdentifier));
            string sql = hasId
                ? $"SELECT {selectFields} FROM {quotedTable} ORDER BY {QuotePostgresIdentifier("Id")}"
                : $"SELECT {selectFields} FROM {quotedTable}";

            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader2 = cmd.ExecuteReader();

            string filePath;
            if (mode.ToLower() == "manual")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV 檔案 (*.csv)|*.csv",
                    FileName = $"{tableName}.csv",
                    Title = $"儲存資料表為 {tableName}"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                filePath = saveFileDialog.FileName;
            }
            else
            {
                string downloadFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Downloads"
                );

                Directory.CreateDirectory(downloadFolder);
                filePath = Path.Combine(downloadFolder, $"{tableName}.csv");
            }

            using var writer = new StreamWriter(filePath, false, new UTF8Encoding(true));

            // 寫入欄位名稱（依順序）
            writer.WriteLine(string.Join(",", columns));

            // 寫入資料
            while (reader2.Read())
            {
                var row = columns.Select(col => reader2[col]?.ToString()?.Replace(",", "，"));
                writer.WriteLine(string.Join(",", row));
            }

            MessageBox.Show($"✅ 匯出完成：\n📄 {filePath}", "匯出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 為 PostgreSQL 保留大小寫正確加上雙引號的識別字（如表名、欄位名）
        /// </summary>
        public static string QuotePostgresIdentifier(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException("識別字不能為空");

            identifier = identifier.Trim();

            if (identifier.StartsWith("\"") && identifier.EndsWith("\""))
                return identifier; // 已加引號則略過

            return $"\"{identifier}\"";
        }


        /// <summary>
        /// 通用CSV匯入方法
        /// </summary>
        /// <typeparam name="TEntity">資料庫實體類型</typeparam>
        /// <typeparam name="TCsvModel">CSV模型類型</typeparam>
        /// <param name="tableName">資料表名稱（用於顯示）</param>
        /// <param name="dbSet">資料庫DbSet</param>
        /// <param name="mapFunction">CSV模型轉換為實體的映射函數</param>
        /// <param name="keySelector">主鍵選擇器</param>
        /// <param name="enableSync">是否啟用同步刪除（刪除CSV中不存在的記錄）</param>
        public void ImportCsvToTable<TEntity, TCsvModel, TKey>(
                 string tableName,
                 DbSet<TEntity> dbSet,
                 Func<TCsvModel, TEntity> mapFunction,
                 Func<TEntity, TKey> entityKeySelector,
                 Func<TCsvModel, TKey> recordKeySelector,
                 bool enableSync = true)
                 where TEntity : class
                 where TCsvModel : class
                 where TKey : notnull
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV 檔案 (*.csv)|*.csv",
                Title = $"選擇要匯入的 {tableName} CSV 檔案"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

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
                var records = csv.GetRecords<TCsvModel>().ToList();

                if (!records.Any())
                {
                    MessageBox.Show("❌ CSV 檔案沒有資料", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = ApplyImportChanges(dbSet, records, mapFunction, entityKeySelector, recordKeySelector, enableSync);

                MessageBox.Show(
                    $"✅ {tableName} 匯入完成：\n" +
                    $"📝 新增 {result.InsertCount} 筆\n" +
                    $"🔄 更新 {result.UpdateCount} 筆\n" +
                    $"🗑️ 刪除 {result.DeleteCount} 筆",
                    "匯入成功",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ {tableName} 匯入失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 多語系表格匯入方式
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="dbSetQuery"></param>
        /// <param name="recordKeySelector"></param>
        /// <param name="entityKeySelector"></param>
        /// <param name="mapFunction"></param>
        /// <param name="enableSync"></param>
        public void ImportdynamicCsvToTable<TEntity>(
                string tableName,
                IQueryable<TEntity> dbSetQuery,
                Func<dynamic, object> recordKeySelector,
                Func<TEntity, object> entityKeySelector,
                Func<dynamic, TEntity?, TEntity> mapFunction,
                bool enableSync = false
            ) where TEntity : class, new()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV 檔案 (*.csv)|*.csv",
                Title = $"選擇要匯入的 {tableName} CSV 檔案"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
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

                var records = csv.GetRecords<dynamic>().ToList();
                var dbEntities = dbSetQuery.ToList(); // ← 將查詢轉為 List 儲存比對用

                int insertCount = 0, updateCount = 0, deleteCount = 0;

                //  建立 CSV 中出現的主鍵集合
                var csvKeys = new HashSet<object>(records.Select(recordKeySelector));

                foreach (var record in records)
                {
                    var recordKey = recordKeySelector(record);
                    var existingEntity = dbEntities.FirstOrDefault(e => entityKeySelector(e).Equals(recordKey));

                    var entity = mapFunction(record, existingEntity);

                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                        updateCount++;
                    }
                    else
                    {
                        _context.Set<TEntity>().Add(entity);
                        insertCount++;
                    }
                }

                //  比對資料庫中但 CSV 中缺少的項目 → 執行刪除
                var toDelete = dbEntities
                    .Where(e => !csvKeys.Contains(entityKeySelector(e)))
                    .ToList();

                if (toDelete.Any())
                {
                    _context.Set<TEntity>().RemoveRange(toDelete);
                    deleteCount = toDelete.Count;
                }

                _context.SaveChanges();

                MessageBox.Show($"✅ Table [{tableName}] 匯入成功，新增 {insertCount} 筆，更新 {updateCount} 筆，刪除：{deleteCount} 筆");
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.ToString() ?? "(無內部錯誤)";
                MessageBox.Show($"❌ {tableName} 匯入失敗：{ex.Message}\n\n【內部例外】\n{inner}",
                                "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 資料新增/更新/刪除的實作	
        /// </summary>
        private ImportResult ApplyImportChanges<TEntity, TCsvModel, TKey>(
            DbSet<TEntity> dbSet,
            List<TCsvModel> records,
            Func<TCsvModel, TEntity> mapFunction,
            Func<TEntity, TKey> entityKeySelector,
            Func<TCsvModel, TKey> recordKeySelector,
            bool enableSync)
            where TEntity : class
            where TCsvModel : class
            where TKey : notnull

        {
            var result = new ImportResult();
            // 取得現有資料（以主鍵為 key）
            var existingData = dbSet.AsEnumerable()
                .GroupBy(entityKeySelector)
                .ToDictionary(g => g.Key, g => g.First());

            // 處理新增和更新
            foreach (var record in records)
            {
                var key = recordKeySelector(record);

                if (existingData.TryGetValue(key, out var existing))
                {
                    var newEntity = mapFunction(record);
                    if (UpdateEntityProperties(existing, newEntity))
                    {
                        result.UpdateCount++;
                    }
                }
                else
                {
                    var newEntity = mapFunction(record);
                    dbSet.Add(newEntity);
                    result.InsertCount++;
                }
            }

            // 刪除資料（啟用同步）
            if (enableSync)
            {
                var recordKeys = records.Select(recordKeySelector).ToHashSet();
                var toDelete = dbSet.AsEnumerable()
                    .Where(e => !recordKeys.Contains(entityKeySelector(e)))
                    .ToList();

                if (toDelete.Any())
                {
                    dbSet.RemoveRange(toDelete);
                    result.DeleteCount = toDelete.Count;
                }
            }

            _context.SaveChanges();
            return result;
        }

        /// <summary>
        /// 更新實體屬性
        /// </summary>
        private bool UpdateEntityProperties<TEntity>(TEntity target, TEntity source)
        {
            bool hasChanged = false;
            var properties = typeof(TEntity).GetProperties()
                .Where(p => p.CanRead && p.CanWrite && p.Name != "Id"); // 忽略主鍵

            foreach (var prop in properties)
            {
                var oldValue = prop.GetValue(target);
                var newValue = prop.GetValue(source);

                if (!Equals(oldValue, newValue))
                {
                    prop.SetValue(target, newValue);
                    hasChanged = true;
                }
            }

            return hasChanged;
        }


       
        public Alarm MapAlarmWithTranslations(dynamic row, Alarm? existing)
        {
            var dict = row as IDictionary<string, object>;

            var alarm = existing ?? new Alarm
            { 
                AlarmNotifyClass = 2,
                AlarmNotifyuser = SD.Admin_Account,
                Translations = new List<AlarmTranslation>(),

                // 預設空字串防止 null
                Error = "",
                Possible = "",
                Repair_steps = ""
            };
            alarm.SourceMachine = dict.TryGetValue("SourceMachine", out var sm) ? sm?.ToString() ?? "" : "";
            alarm.address = dict.TryGetValue("address", out var addr) ? addr?.ToString() ?? "" : "";
            alarm.IPC_table = dict.TryGetValue("IPC_table", out var ipc) ? ipc?.ToString() ?? "" : "";

            alarm.Description = dict.TryGetValue("Description", out var desc) ? desc?.ToString() ?? "" : "";
            alarm.classTag = dict.TryGetValue("classTag", out var tag) ? tag?.ToString() ?? "" : "";

            // --------------------------
            // ❌ 不補主表欄位（保持為空）
            // --------------------------
            alarm.Error = "";
            alarm.Possible = "";
            alarm.Repair_steps = "";

            // --------------------------
            // 直接刪除既有翻譯
            //alarm.Translations.Clear();
            var existingTranslations = alarm.Translations.ToDictionary(t => t.LanguageCode);

            var langDict = new Dictionary<string, (string error, string possible, string steps)>();

            foreach (var kv in dict)
            {
                if (kv.Key.StartsWith("Error_"))
                {
                    var lang = kv.Key.Substring("Error_".Length);
                    string value = kv.Value?.ToString() ?? "";
                    if (!langDict.ContainsKey(lang))
                        langDict[lang] = (value, "", "");
                    else
                        langDict[lang] = (value, langDict[lang].possible, langDict[lang].steps);
                }
                else if (kv.Key.StartsWith("Possible_"))
                {
                    var lang = kv.Key.Substring("Possible_".Length);
                    string value = kv.Value?.ToString() ?? "";
                    if (!langDict.ContainsKey(lang))
                        langDict[lang] = ("", value, "");
                    else
                        langDict[lang] = (langDict[lang].error, value, langDict[lang].steps);
                }
                else if (kv.Key.StartsWith("Repair_steps_"))
                {
                    var lang = kv.Key.Substring("Repair_steps_".Length);
                    string value = kv.Value?.ToString() ?? "";
                    if (!langDict.ContainsKey(lang))
                        langDict[lang] = ("", "", value);
                    else
                        langDict[lang] = (langDict[lang].error, langDict[lang].possible, value);
                }

            }
            // 建立翻譯資料
            foreach (var kv in langDict)
            {
                string lang = kv.Key;
                string error = kv.Value.error;
                string possible = kv.Value.possible;
                string steps = kv.Value.steps;

                if (!string.IsNullOrWhiteSpace(error) ||
                    !string.IsNullOrWhiteSpace(possible) ||
                    !string.IsNullOrWhiteSpace(steps))
                {
                    if (existingTranslations.TryGetValue(lang, out var translation))
                    {
                        // 已存在 → 更新內容（保留原 ID）
                        translation.Error = error;
                        translation.Possible = possible;
                        translation.Repair_steps = steps;
                    }
                    else
                    {
                        alarm.Translations.Add(new AlarmTranslation
                        {
                            LanguageCode = lang,
                            Error = error,
                            Possible = possible,
                            Repair_steps = steps
                        });
                    }
                }
            }

            return alarm;
        }

    }

    public class ImportResult
    {
    public int InsertCount { get; set; }
    public int UpdateCount { get; set; }
    public int DeleteCount { get; set; }
    public bool Skip { get; set; }
    public string Message = "";
    }
      
}
