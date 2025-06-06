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

namespace FX5U_IOMonitor.Models
{
    public class TableImportExportManager
    {
        private readonly ApplicationDB _context;

        public TableImportExportManager(ApplicationDB context)
        {
            _context = context;
        }

        public static string Local_IpAddress = "localhost";
        public static string Local_Port = "5430";
        public static string Local_UserName = "postgres";
        public static string Local_Password = "963200";
        /// <summary>
        /// 下載當前資料庫資料表
        /// </summary>
        /// <param name="tableName"> 要下載的資料表名稱</param> 
        /// <param name="columns"> 是否指定column</param> 
        /// <param name="mode"> "manual"=提供使用者選擇 ，"auto" 自動至下載資料夾</param> 

        public static void ExportTableToCsv(string tableName, string mode = "auto", string[]? columns = null)
        {
            string connString = $"Host={Local_IpAddress};Port={Local_Port};Database=element.db;Username={Local_UserName};Password={Local_Password}";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            string quotedTable = QuotePostgresIdentifier(tableName);

            string selectFields = columns != null && columns.Length > 0
                ? string.Join(", ", columns.Select(QuotePostgresIdentifier))
                : "*";

            bool hasIdColumn = false;
            if (columns == null || columns.Contains("Id", StringComparer.OrdinalIgnoreCase))
            {
                var checkCmd = new NpgsqlCommand($"SELECT column_name FROM information_schema.columns WHERE table_name = lower(@name)", conn);
                checkCmd.Parameters.AddWithValue("@name", tableName);
                using var readerCheck = checkCmd.ExecuteReader();
                while (readerCheck.Read())
                {
                    if (readerCheck.GetString(0).Equals("Id", StringComparison.OrdinalIgnoreCase))
                    {
                        hasIdColumn = true;
                        break;
                    }
                }
                readerCheck.Close();
            }

            string sql = $"SELECT {selectFields} FROM {quotedTable}" + (hasIdColumn ? $" ORDER BY {QuotePostgresIdentifier("Id")}" : "");
            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            string filePath;

            if (mode.ToLower() == "manual")
            {
                // 手動選擇儲存位置
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
                // 自動儲存到下載資料夾
                string downloadFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Downloads"
                );

                Directory.CreateDirectory(downloadFolder); // 保險起見建立資料夾
                filePath = Path.Combine(downloadFolder, $"{tableName}.csv");
            }

            using var writer = new StreamWriter(filePath, false, new UTF8Encoding(true));

            // 寫入欄位名稱
            for (int i = 0; i < reader.FieldCount; i++)
            {
                writer.Write(reader.GetName(i));
                if (i < reader.FieldCount - 1)
                    writer.Write(",");
            }
            writer.WriteLine();

            // 寫入每一列資料
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    writer.Write(reader[i]?.ToString()?.Replace(",", "，")); // 防止欄位錯裂
                    if (i < reader.FieldCount - 1)
                        writer.Write(",");
                }
                writer.WriteLine();
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
        public void ImportCsvToTable<TEntity, TCsvModel>(
            string tableName,
            DbSet<TEntity> dbSet,
            Func<TCsvModel, int, TEntity> mapFunction,
            Func<TEntity, int> keySelector,
            bool enableSync = true)
            where TEntity : class
            where TCsvModel : class
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

                // 處理ID驗證和分配
                var processedRecords = ValidateAndAssignCsvIds(records, dbSet, keySelector);
                if (processedRecords == null) return; // 使用者取消

                // 執行匯入
                var result = ApplyImportChanges(dbSet, processedRecords, mapFunction, keySelector, enableSync);

                // 顯示結果
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
        /// 處理ID驗證和分配
        /// </summary>
        private List<TCsvModel> ValidateAndAssignCsvIds<TEntity, TCsvModel>(
            List<TCsvModel> records,
            DbSet<TEntity> dbSet,
            Func<TEntity, int> keySelector)
            where TEntity : class
            where TCsvModel : class
        {
            // 取得ID屬性
            var idProperty = typeof(TCsvModel).GetProperty("Id");
            if (idProperty == null)
            {
                MessageBox.Show("❌ CSV 模型缺少 Id 屬性", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // 取得資料庫最大ID
            int maxDbId = dbSet.Any() ? dbSet.Max(keySelector) : 0;
            int nextId = maxDbId + 1;

            // 檢查並補齊ID
            var idSet = new HashSet<int>();
            foreach (var record in records)
            {
                var currentId = (int)idProperty.GetValue(record);

                if (currentId <= 0)
                {
                    idProperty.SetValue(record, nextId++);
                    currentId = nextId - 1;
                }

                if (!idSet.Add(currentId))
                {
                    MessageBox.Show($"❌ 匯入失敗：CSV 中出現重複的 Id: {currentId}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            // 檢查ID連續性
            if (!CheckIdContinuity(idSet))
                return null;

            return records;
        }

        /// <summary>
        /// 檢查ID連續性
        /// </summary>
        private bool CheckIdContinuity(HashSet<int> idSet)
        {
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

                    return result == DialogResult.Yes;
                }
            }

            return true;
        }

        /// <summary>
        /// 資料新增/更新/刪除的實作	
        /// </summary>
        private ImportResult ApplyImportChanges<TEntity, TCsvModel>(
            DbSet<TEntity> dbSet,
            List<TCsvModel> records,
            Func<TCsvModel, int, TEntity> mapFunction,
            Func<TEntity, int> keySelector,
            bool enableSync)
            where TEntity : class
            where TCsvModel : class
        {
            var result = new ImportResult();
            var idProperty = typeof(TCsvModel).GetProperty("Id");

            // 取得現有資料
            var existingData = dbSet.AsEnumerable()
                .GroupBy(keySelector)
                .ToDictionary(g => g.Key, g => g.First());

            // 處理新增和更新
            foreach (var record in records)
            {
                var recordId = (int)idProperty.GetValue(record);

                if (existingData.TryGetValue(recordId, out var existing))
                {
                    // 更新現有記錄
                    var newEntity = mapFunction(record, recordId);
                    if (UpdateEntityProperties(existing, newEntity))
                    {
                        result.UpdateCount++;
                    }
                }
                else
                {
                    // 新增記錄
                    var newEntity = mapFunction(record, recordId);
                    dbSet.Add(newEntity);
                    result.InsertCount++;
                }
            }

            // 同步刪除
            if (enableSync)
            {
                var csvIds = records.Select(r => (int)idProperty.GetValue(r)).ToHashSet();
                var toDelete = dbSet.AsEnumerable().Where(d => !csvIds.Contains(keySelector(d))).ToList();

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
        private bool UpdateEntityProperties<TEntity>(TEntity existing, TEntity newEntity) where TEntity : class
        {
            bool hasChanges = false;
            var properties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && p.Name != "Id");

            foreach (var property in properties)
            {
                var existingValue = property.GetValue(existing);
                var newValue = property.GetValue(newEntity);

                if (!Equals(existingValue, newValue))
                {
                    property.SetValue(existing, newValue);
                    hasChanges = true;
                }
            }

            return hasChanges;
        }
    }

    /// <summary>
    /// 匯入結果
    /// </summary>
    public class ImportResult
    {
        public int InsertCount { get; set; }
        public int UpdateCount { get; set; }
        public int DeleteCount { get; set; }
    }
     
    }
