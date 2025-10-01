using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;     // 如果你要用 AjaxResult 物件也要引入
using SsioAPILib; // 假設 DLL 裡的 namespace 叫這個
using SsioAPILib.Services;
using SsioAPILib.Dto;


namespace FX5U_IOMonitor.DatabaseProvider
{
    public static class TableSyncAPI
    {

        /// <summary>
        /// 地端更新雲端 - API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="local"></param>
        /// <param name="cloud"></param>
        /// <param name="tableName"></param>
        /// <param name="keySelector"></param>
        /// <param name="changeDetector"></param>
        /// <param name="ignoreProperties"></param>
        /// <returns></returns>
        public static async Task<SyncResult> SyncFromLocalToCloudNew<T>(
            ApplicationDB local,
            string tableName,
            Func<T, object> keySelector,  // 主鍵選擇器，例如 e => e.Id
            Func<T, T, bool> changeDetector,  // 變更檢測，例如 (a, b) => a.UpdatedAt != b.UpdatedAt
            params string[] ignoreProperties)  // 忽略屬性，可在SQL生成中使用
            where T : class
        {
            var result = new SyncResult { TableName = tableName };

            // 取得本地資料
            var localData = await local.Set<T>().AsNoTracking().ToListAsync();
            string getsql = $@"SELECT * FROM ""{tableName}"";";
            // 取得雲端資料
            //var cloudData = await cloud.Set<T>().AsNoTracking().ToListAsync();
            AjaxResult? apiResult = await ApiService.SendPostRequest(getsql);
            var jsonString = apiResult.Data.ToString();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true  // 忽略大小寫差異
            };
            List<T> cloudData = JsonSerializer.Deserialize<List<T>>(jsonString, options);


            // 泛型比較
            var compareResult = SyncComparer.Compare(
                localData,
                cloudData,
                keySelector,
                changeDetector,
                0
            );

            var sqlList = new List<string>();


            int batchSize = 100;

            AddBatchedSql(
                compareResult.ToUpdate,
                batchSize,
                batch => SqlSyncGenerator.GenerateBatchUpdateSQLMerged(batch, tableName, ignoreProperties),
                sqlList
            );

            AddBatchedSql(
                compareResult.ToAdd,
                batchSize,
                batch => SqlSyncGenerator.GenerateBatchInsertSQLG(batch, tableName, ignoreProperties),
                sqlList
            );
            // sqlList.Add(SqlSyncGenerator.GenerateBatchInsertSQLG(compareResult.ToAdd, tableName, ignoreProperties));
            // sqlList.Add(SqlSyncGenerator.GenerateBatchUpdateSQLMerged(compareResult.ToUpdate, tableName, ignoreProperties));


            // 執行所有SQL的API請求
            foreach (var sql in sqlList.Where(s => !string.IsNullOrWhiteSpace(s)))
            {
                //AjaxResult? apiResult = await ApiService.SendPostRequest(sql);
                MessageBox.Show("[" + tableName + "]\r\n " + sql);
                // 根據apiResult處理錯誤（例如記錄到result）
                //if (apiResult?.Success != true)
                //{
                //    apiResult.Message = ($"SQL執行失敗: {sql}");
                //}
            }

            return result;
        }


        public static async Task<SyncResult> SyncFromCloudToLocalNew<T>(
            ApplicationDB local,
            string tableName,
            Func<T, object> keySelector,  // 主鍵選擇器，例如 e => e.Id
            Func<T, T, bool> changeDetector,  // 變更檢測，例如 (a, b) => a.UpdatedAt != b.UpdatedAt
            params string[] ignoreProperties)  // 忽略屬性，可在SQL生成中使用
            where T : class
        {
            var result = new SyncResult { TableName = tableName };
            var localSet = local.Set<T>();
            // 取得本地資料
            var localData = await local.Set<T>().AsNoTracking().ToListAsync();
            // 取得雲端資料
            string getsql = $@"SELECT * FROM ""{tableName}"";";
            AjaxResult? apiResult = await ApiService.SendPostRequest(getsql);
            var jsonString = apiResult.Data.ToString();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true  // 忽略大小寫差異
            };
            List<T> cloudData = JsonSerializer.Deserialize<List<T>>(jsonString, options);


            // 泛型比較
            var compareResult = SyncComparer.Compare(
                localData,
                cloudData,
                keySelector,
                changeDetector,
                1
            );

            var sqlList = new List<string>();

            int batchSize = 100;

            AddBatchedSql(
                compareResult.ToUpdate,
                batchSize,
                batch => SqlSyncGenerator.GenerateBatchUpdateSQLMerged(batch, tableName, ignoreProperties),
                sqlList
            );

            AddBatchedSql(
                compareResult.ToAdd,
                batchSize,
                batch => SqlSyncGenerator.GenerateBatchInsertSQLG(batch, tableName, ignoreProperties),
                sqlList
            );
            // sqlList.Add(SqlSyncGenerator.GenerateBatchInsertSQLG(compareResult.ToAdd, tableName, ignoreProperties));
            // sqlList.Add(SqlSyncGenerator.GenerateBatchUpdateSQLMerged(compareResult.ToUpdate, tableName, ignoreProperties));

            // 執行所有SQL的API請求
            foreach (var sql in sqlList.Where(s => !string.IsNullOrWhiteSpace(s)))
            {
                //AjaxResult? apiResult = await ApiService.SendPostRequest(sql);
                MessageBox.Show("[" + tableName + "]\r\n " + sql);
                // 根據apiResult處理錯誤（例如記錄到result）
                //if (apiResult?.Success != true)
                //{
                //    apiResult.Message = ($"SQL執行失敗: {sql}");
                //}
            }

            return result;
        }

        private static string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sync_log.txt");

        private static void OnLogMessage(string message)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(logFilePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);
                    File.AppendAllText(logFilePath, $"[{DateTime.Now:yyyy-MM-dd-HH-mm-ss}] {message}{Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ 寫入同步 log 檔案失敗：{ex.Message}");
            }
        }

        public static void LogSyncResult(SyncResult? result = null, string Table ="" , int direction = 0)
        {
            string message;

            if (direction == 0)
            {
                message = $"地端資料上傳至雲端資料庫表格 [{result.TableName}] 完成：新增 {result.Added} 筆，更新 {result.Updated} 筆，共 {result.Total} 筆";
            }
            else if (direction == 1)
            {
                message = $"雲端資料庫已同步至地端資料庫表格 [{result.TableName}] 完成：新增 {result.Added} 筆，更新 {result.Updated} 筆，共 {result.Total} 筆";
            }
            else if (direction == 2)
            {
                message = Table + ": " +$"地端資料表上傳雲端失敗" ;
            }
            else
            {
                message = $"雲端資料表同步地端失敗";
            }
            OnLogMessage(message);
        }

        /// <summary>
        /// 地端同步所有資料表到雲端資料庫
        /// </summary>
        /// <param name="local"></param>
        /// <param name="cloud"></param>
        /// <returns></returns>
        public static async Task SyncLocalToCloudAllTables(ApplicationDB local, CloudDbContext cloud)
        {
            try
            {

                if (cloud == null)
                {
                    TableSyncAPI.LogSyncResult(direction: 2);
                    return;
                }

                var Machine = await TableSyncAPI.SyncFromLocalToCloudNew<Machine_number>(
                    local,
                    "Machine",
                    keySelector: m => m.Id,
                    changeDetector: (a, b) => a.UpdatedAt != b.UpdatedAt,
                    ignoreProperties: new[] { "mcFrame" }
                );

                var MachineParameters = await TableSyncAPI.SyncFromLocalToCloudNew<MachineParameter>(
                    local,
                    "MachineParameters",
                    keySelector: m => m.Id,
                    changeDetector: (a, b) => a.UpdatedAt != b.UpdatedAt,
                    ignoreProperties: new[] { "HistoryRecodes" }
                );

                var Machine_IO = await TableSyncAPI.SyncFromLocalToCloudNew<MachineIO>(
                    local,
                    "Machine_IO",
                    keySelector: m => m.Id,
                    changeDetector: (a, b) => a.UpdatedAt != b.UpdatedAt,
                    ignoreProperties: new[] { "" }
                );

                // 下列 Code 執行會報錯，因為雲端 DB 的同步欄位為 NULL 從 1111 筆後資料開始看
                //var MachineIOTranslations = await TableSyncAPI.SyncFromLocalToCloudNew<MachineIOTranslation>(
                //    local,
                //    "MachineIOTranslation",
                //    keySelector: m => m.Id,
                //    changeDetector: (a, b) => JsonSerializer.Serialize(a) != JsonSerializer.Serialize(b),
                //    ignoreProperties: new[] { "" }
                //);

                var alarm = await TableSyncAPI.SyncFromLocalToCloudNew<Alarm>(
                    local,
                    "alarm",
                    keySelector: m => m.Id,
                    changeDetector: (a, b) => a.UpdatedAt != b.UpdatedAt,
                    ignoreProperties: new[] { "Translations", "AlarmHistories" }
                );

                var Histories = await TableSyncAPI.SyncFromLocalToCloudNew<History>(
                    local,
                    "Histories",
                    keySelector: m => m.Id,
                    changeDetector: (a, b) => a.UpdatedAt != b.UpdatedAt,
                    ignoreProperties: new[] { "" }
                );

                var MachineParameterHistoryRecode = await TableSyncAPI.SyncFromLocalToCloudNew<MachineParameterHistoryRecode>(
                    local,
                    "MachineParameterHistoryRecodes",
                    keySelector: m => m.Id,
                    changeDetector: (a, b) => a.UpdatedAt != b.UpdatedAt,
                    ignoreProperties: new[] { "MachineParameter" }
                );

                var AlarmHistories = await TableSyncAPI.SyncFromLocalToCloudNew<AlarmHistory>(
                    local,
                    "AlarmHistories",
                    keySelector: m => m.Id,
                    changeDetector: (a, b) => a.UpdatedAt != b.UpdatedAt,
                    ignoreProperties: new[] { "Alarm" }
                );

                //var AlarmTranslation = await TableSyncAPI.SyncFromLocalToCloudNew<AlarmTranslation>(
                //    local,
                //    "AlarmTranslation",
                //    keySelector: m => m.Id,
                //    changeDetector: (a, b) => JsonSerializer.Serialize(a) != JsonSerializer.Serialize(b),
                //    ignoreProperties: new[] { "" }     
                //);

                //var Blade_brand = await TableSyncAPI.SyncFromLocalToCloudNew<Blade_brand>(
                //    local,
                //    "Blade_brand",
                //    keySelector: m => m.Id,
                //    changeDetector: (a, b) => a.UpdatedAt != b.UpdatedAt,
                //    ignoreProperties: new[] { "" }
                //);

                //var Blade_brand_TPI = await TableSyncAPI.SyncFromLocalToCloudNew<Blade_brand_TPI>(
                //    local,
                //    "Blade_brand_TPI",
                //    keySelector: m => m.Id,
                //    changeDetector: (a, b) => a.UpdatedAt != b.UpdatedAt,
                //    ignoreProperties: new[] { "" }
                //);

                //var Language = await TableSyncAPI.SyncFromLocalToCloudNew<Language>(
                //    local,
                //    "Language",
                //    keySelector: m => m.Id,
                //    changeDetector: (a, b) => a.UpdatedAt != b.UpdatedAt,
                //    ignoreProperties: new[] { "" }
                //);


                // 記錄 log
                TableSyncAPI.LogSyncResult(Machine, "Machine");
                TableSyncAPI.LogSyncResult(Histories, "Histories");
                TableSyncAPI.LogSyncResult(MachineParameters, "MachineParameters");
                TableSyncAPI.LogSyncResult(AlarmHistories, "AlarmHistories");
                TableSyncAPI.LogSyncResult(Machine_IO, "Machine_IO");
                TableSyncAPI.LogSyncResult(MachineParameterHistoryRecode, "MachineParameterHistoryRecode");
                // TableSyncAPI.LogSyncResult(MachineIOTranslations);
                TableSyncAPI.LogSyncResult(alarm, "alarm");

                //TableSyncAPI.LogSyncResult(Blade_brand);
                //TableSyncAPI.LogSyncResult(Blade_brand_TPI);
                //TableSyncAPI.LogSyncResult(Language);
                //TableSyncAPI.LogSyncResult(alarm);
                //TableSyncAPI.LogSyncResult(AlarmTranslation);
            }
            catch (Exception ex)
            {
                TableSyncAPI.LogSyncResult(direction: 2);
            }
        }

        /// <summary>
        /// 雲端同步所有資料表到地端資料庫
        /// </summary>
        /// <returns></returns>
        public static async Task SyncCloudToLocalAllTables(ApplicationDB local, CloudDbContext cloud)
        {

            if (cloud == null)
            {
                TableSyncAPI.LogSyncResult(direction: 3);
                return;
            }
            try
            {
                //var Blade_brand = await TableSyncAPI.SyncFromCloudToLocalNew<Blade_brand>(
                //    local,
                //    "Blade_brand",
                //    keySelector: m => m.Id,
                //    changeDetector: (a, b) => a.UpdatedAt != b.UpdatedAt,
                //    ignoreProperties: new[] { "" }     // { "CreatedAt", "IsSynced" }
                //);


                //var Blade_brand = await TableSyncAPI.SyncFromCloudToLocal<Blade_brand>(local, cloud, "Blade_brand");
                //var Blade_brand_TPI = await TableSyncAPI.SyncFromCloudToLocal<Blade_brand_TPI>(local, cloud, "Blade_brand_TPI");
                //var Language = await TableSyncAPI.SyncFromCloudToLocal<Language>(local, cloud, "Language");
                //var MachineIOTranslations = await TableSyncAPI.SyncFromCloudToLocal<MachineIOTranslation>(local, cloud, "MachineIOTranslation");
                //var AlarmTranslation = await TableSyncAPI.SyncFromCloudToLocal<AlarmTranslation>(local, cloud, "AlarmTranslation");

                //TableSyncAPI.LogSyncResult(Blade_brand, 1);
                //TableSyncAPI.LogSyncResult(Blade_brand_TPI, 1);
                //TableSyncAPI.LogSyncResult(Language, 1);
                //TableSyncAPI.LogSyncResult(MachineIOTranslations, 1);
                //TableSyncAPI.LogSyncResult(AlarmTranslation, 1);
            }
            catch (Exception)
            {
                TableSyncAPI.LogSyncResult(direction: 3); // 失敗 log
            }
        }

        private static void AddBatchedSql<T>(
        List<T> items,
        int batchSize,
        Func<List<T>, string> generateSqlFunc,
        List<string> sqlList)
        {
            for (int i = 0; i < items.Count; i += batchSize)
            {
                var batch = items.Skip(i).Take(batchSize).ToList();
                var sql = generateSqlFunc(batch);
                sqlList.Add(sql);
            }
        }
    }

  

    public class CompareResult<T>
    {
        public List<T> ToAdd { get; set; } = new();
        public List<T> ToUpdate { get; set; } = new();
        public List<T> ToDelete { get; set; } = new();
    }

    public static class SyncComparer
    {
        public static CompareResult<T> Compare<T>(
            List<T> localList,
            List<T> cloudList,
            Func<T, object> keySelector,
            Func<T, T, bool> isModified,
            int Type)
        {
            var result = new CompareResult<T>();

            var localDict = localList.ToDictionary(keySelector);
            var cloudDict = cloudList.ToDictionary(keySelector);

            if (Type == 0)
            {
                foreach (var kv in localDict)
                {
                    var key = kv.Key;
                    var localItem = kv.Value;

                    if (!cloudDict.TryGetValue(key, out var cloudItem))
                    {
                        SetSyncMetadata(localItem);
                        result.ToAdd.Add(localItem);
                    }
                    else if (isModified(localItem, cloudItem))
                    {
                        SetSyncMetadata(localItem);
                        result.ToUpdate.Add(localItem);
                    }
                }
                // 刪除
                //foreach (var kv in cloudDict)
                //{
                //    if (!localDict.ContainsKey(kv.Key))
                //    {
                //        result.ToDelete.Add(kv.Value); // 刪除
                //    }
                //}
            }
            else if (Type == 1)
            {
                foreach (var kv in cloudDict)
                {
                    var key = kv.Key;
                    var cloudItem = kv.Value;

                    if (!localDict.TryGetValue(key, out var localItem))
                    {
                        SetSyncMetadata(cloudItem);
                        result.ToAdd.Add(cloudItem);
                    }
                    else if (isModified(cloudItem, localItem))
                    {
                        SetSyncMetadata(cloudItem);
                        result.ToUpdate.Add(cloudItem);
                    }
                }
            }
            return result;
        }

        private static void SetSyncMetadata(object item)
        {
            var type = item.GetType();

            var isSyncedProp = type.GetProperty("IsSynced");
            if (isSyncedProp != null)
                isSyncedProp.SetValue(item, true);

            var updatedAtProp = type.GetProperty("UpdatedAt");
            if (updatedAtProp != null &&
                (updatedAtProp.PropertyType == typeof(DateTime) || updatedAtProp.PropertyType == typeof(DateTime?)))
                updatedAtProp.SetValue(item, DateTime.UtcNow);
        }

    }

    public static class SqlSyncGenerator
    {
        /// <summary>
        /// 09/04 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="tableName"></param>
        /// <param name="ignoreProperties"></param>
        /// <returns></returns>
        public static string GenerateBatchInsertSQLG<T>(List<T> entities, string tableName, params string[] ignoreProperties)
        {
            if (entities == null || !entities.Any()) return string.Empty;

            var props = typeof(T).GetProperties()
                .Where(p => !ignoreProperties.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                .ToList();

            var columns = string.Join(", ", props.Select(p => $"\"{p.Name}\""));

            var sb = new StringBuilder();
            sb.AppendLine($"INSERT INTO \"{tableName}\" ({columns}) VALUES");

            var valueLines = entities.Select(entity =>
            {
                var values = props.Select(p =>
                {
                    var value = p.GetValue(entity);
                    return value switch
                    {
                        null => "NULL",
                        string s => $"'{Escape(s)}'",
                        bool b => b ? "true" : "false",
                        DateTime dt => $"'{dt:yyyy-MM-dd HH:mm:ss}'",
                        DateTimeOffset dto => $"'{dto:yyyy-MM-dd HH:mm:sszzz}'",
                        _ => value.ToString()
                    };
                });
                return $"({string.Join(", ", values)})";
            });

            sb.AppendLine(string.Join(",\n", valueLines));
            sb.Append(';');
            return sb.ToString();
        }

        public static string GenerateBatchUpdateSQLMerged<T>(List<T> entities, string tableName, params string[] ignoreProperties)
        {
            if (entities == null || !entities.Any()) return string.Empty;

            var type = typeof(T);
            var keyProp = type.GetProperty("Id");
            if (keyProp == null) throw new Exception("Entity must have an Id property for update");

            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p =>
                    !Attribute.IsDefined(p, typeof(NotMappedAttribute)) &&
                    !ignoreProperties.Contains(p.Name, StringComparer.OrdinalIgnoreCase) &&
                    p.Name != "Id")
                .ToList();

            var allColumns = new List<PropertyInfo> { keyProp! };
            allColumns.AddRange(props);

            var sb = new StringBuilder();

            sb.AppendLine($@"UPDATE ""{tableName}"" AS t");
            sb.AppendLine("SET");
            sb.AppendLine(string.Join(",\n    ", props.Select(p =>
                $"\"{p.Name}\" = v.\"{p.Name}\"")));

            sb.AppendLine("FROM (");
            sb.AppendLine("VALUES");

            var valueLines = new List<string>();
            foreach (var entity in entities)
            {
                var values = allColumns.Select(p =>
                {
                    var value = p.GetValue(entity);
                    return value switch
                    {
                        null => "NULL",
                        string s => $"'{Escape(s)}'",
                        bool b => b ? "true" : "false",
                        DateTime dt => $"'{dt:yyyy-MM-dd HH:mm:ss}'::timestamptz",
                        DateTimeOffset dto => $"'{dto:yyyy-MM-dd HH:mm:sszzz}'::timestamptz",
                        _ => value?.ToString() ?? "NULL"
                    };
                });

                valueLines.Add($"    ({string.Join(", ", values)})");
            }

            sb.AppendLine(string.Join(",\n", valueLines));

            sb.AppendLine($") AS v ({string.Join(", ", allColumns.Select(p => $"\"{p.Name}\""))})");
            sb.AppendLine("WHERE t.\"Id\" = v.\"Id\";");

            return sb.ToString();
        }


        private static string Escape(string? input)
        {
            return (input ?? "").Replace("'", "''"); // SQL escape 單引號
        }
    }


}
