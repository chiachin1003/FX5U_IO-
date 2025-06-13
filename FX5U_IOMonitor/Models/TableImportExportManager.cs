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
                MessageBox.Show($"❌ 未定義 {tableName} 的欄位匯出順序。", "匯出失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }

    public class ImportResult
    {
    public int InsertCount { get; set; }
    public int UpdateCount { get; set; }
    public int DeleteCount { get; set; }
    }

     

}
