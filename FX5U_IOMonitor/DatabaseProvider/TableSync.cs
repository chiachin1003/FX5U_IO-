using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.DatabaseProvider
{
    public static class TableSync
    {
        /// <summary>
        /// 地端更新雲端
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="local"></param>
        /// <param name="cloud"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static async Task<SyncResult> SyncFromLocalToCloud<T>
        (ApplicationDB local, CloudDbContext cloud, string tableName,  params string[] ignoreProperties) where T : class
        {
           

            var result = new SyncResult { TableName = tableName };

            // 取得本地與雲端資料
            var localData = await local.Set<T>().AsNoTracking().ToListAsync();

            // 自動建立資料表與欄位
            await EnsureTableAndColumnsExist<T>(cloud, tableName);

            // 雲端目前資料
            var cloudSet = cloud.Set<T>();
            var cloudData = await cloudSet.AsNoTracking().ToListAsync();

            // 以 Id 為主鍵比對
            var localDict = localData.ToDictionary(d => GetPrimaryKeyValue(d));
            var cloudDict = cloudData.ToDictionary(d => GetPrimaryKeyValue(d));
            var toUpdate = new List<T>();
            var toAdd = new List<T>();

            foreach (var kv in localDict)
            {
                var key = kv.Key;
                var localValue = kv.Value;

                if (cloudDict.TryGetValue(key, out var cloudValue))
                {
                    // 判斷是否真的有差異（可自訂比對邏輯）
                    if (!AreEntitiesEqual(localValue, cloudValue, ignoreProperties))
                    {
                        var cloudEntity = CloneEntity(localValue);

                        var prop = cloudEntity.GetType().GetProperty("IsSynced");
                        if (prop != null)
                            prop.SetValue(cloudEntity, true);

                        var updatedAtProp = cloudEntity.GetType().GetProperty("UpdatedAt");
                        if (updatedAtProp != null &&
                            (updatedAtProp.PropertyType == typeof(DateTime) || updatedAtProp.PropertyType == typeof(DateTime?)))
                            updatedAtProp.SetValue(cloudEntity, DateTime.UtcNow);

                        toUpdate.Add(cloudEntity);
                        result.Updated++;
                    }
                }
                else
                {
                    var newEntity = CloneEntity(localValue);

                    var prop = newEntity.GetType().GetProperty("IsSynced");
                    if (prop != null)
                        prop.SetValue(newEntity, true);

                    var updatedAtProp = newEntity.GetType().GetProperty("UpdatedAt");
                    if (updatedAtProp != null &&
                        (updatedAtProp.PropertyType == typeof(DateTime) || updatedAtProp.PropertyType == typeof(DateTime?)))
                        updatedAtProp.SetValue(newEntity, DateTime.UtcNow);

                    toAdd.Add(newEntity);
                    result.Added++;
                }
            }

            try
            {
                // ✅ 用 Attach + Modified 方式避免 UpdateRange 錯誤
                foreach (var entity in toUpdate)
                {
                    cloudSet.Attach(entity); // 附加到雲端 DbContext
                    cloud.Entry(entity).State = EntityState.Modified; // 標記為修改
                }

                // ✅ 新增的資料仍可直接使用 AddRange
                await cloudSet.AddRangeAsync(toAdd);

                // ✅ 寫入資料庫
                await cloud.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var inner = ex.InnerException?.Message ?? "❓ 無內部錯誤細節";
                MessageBox.Show($"❌ 同步失敗：{ex.Message}\n\n👉 InnerException：{inner}", "同步錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ 同步時發生未預期錯誤：{ex.Message}", "同步錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;

        }

        private static T CloneEntity<T>(T source) where T : class
        {
            var target = Activator.CreateInstance<T>(); // 不需 new()
            var props = typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite);

            foreach (var prop in props)
            {
                var value = prop.GetValue(source);
                prop.SetValue(target, value);
            }

            return target;
        }
        /// <summary>
        /// 只新增不更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="local"></param>
        /// <param name="cloud"></param>
        /// <param name="tableName"></param>
        /// <param name="ignoreProperties"></param>
        /// <returns></returns>
        public static async Task<SyncResult> SyncFromLocalToCloud_AddOnly<T>(
    ApplicationDB local, CloudDbContext cloud, string tableName, params string[] ignoreProperties) where T : class
        {
            var result = new SyncResult { TableName = tableName };

            // 取得本地與雲端資料
            var localData = await local.Set<T>().AsNoTracking().ToListAsync();

            // 自動建立資料表與欄位
            await EnsureTableAndColumnsExist<T>(cloud, tableName);

            // 雲端目前資料
            var cloudSet = cloud.Set<T>();
            var cloudData = await cloudSet.AsNoTracking().ToListAsync();

            // 以 Id 為主鍵比對
            var localDict = localData.ToDictionary(d => GetPrimaryKeyValue(d));
            var cloudDict = cloudData.ToDictionary(d => GetPrimaryKeyValue(d));
            var toAdd = new List<T>();

            foreach (var kv in localDict)
            {
                var key = kv.Key;
                var localValue = kv.Value;

                if (!cloudDict.ContainsKey(key))
                {
                    var prop = localValue.GetType().GetProperty("IsSynced");
                    if (prop != null)
                        prop.SetValue(localValue, true);
                    toAdd.Add(localValue);
                    result.Added++;
                }
            }

            try
            {
                // 清除已追蹤的實體
                foreach (var entry in cloud.ChangeTracker.Entries().ToList())
                {
                    entry.State = EntityState.Detached;
                }

                // 只新增，不更新
                cloudSet.AddRange(toAdd);
                await cloud.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var inner = ex.InnerException?.Message ?? "❓ 無內部錯誤細節";
                MessageBox.Show($"❌ 新增同步失敗：{ex.Message}\n\n👉 InnerException：{inner}", "同步錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ 新增同步時發生未預期錯誤：{ex.Message}", "同步錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }
        /// <summary>
        /// 雲端更新地端
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="local"></param>
        /// <param name="cloud"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static async Task<SyncResult> SyncFromCloudToLocal<T>(ApplicationDB local, CloudDbContext cloud, string tableName, params string[] ignoreProperties) where T : class
        {
            var result = new SyncResult { TableName = tableName };

            // 取得雲端與地端資料
            var cloudData = await cloud.Set<T>().AsNoTracking().ToListAsync();
            var localSet = local.Set<T>();
            var localData = await localSet.AsNoTracking().ToListAsync();

            var cloudDict = cloudData.ToDictionary(d => GetPrimaryKeyValue(d));
            var localDict = localData.ToDictionary(d => GetPrimaryKeyValue(d));

            var toUpdate = new List<T>();
            var toAdd = new List<T>();

            foreach (var kv in cloudDict)
            {
                var key = kv.Key;
                var cloudValue = kv.Value;

                if (localDict.TryGetValue(key, out var localValue))
                {
                    if (!AreEntitiesEqual(cloudValue, localValue, ignoreProperties))
                    {
                        NormalizeDateTimeProperties(cloudValue);

                        var prop = cloudValue.GetType().GetProperty("IsSynced");
                        if (prop != null)
                            prop.SetValue(cloudValue, true);

                        toUpdate.Add(cloudValue);
                        result.Updated++;
                    }
                }
                else
                {
                    NormalizeDateTimeProperties(cloudValue);

                    var prop = cloudValue.GetType().GetProperty("IsSynced");
                    if (prop != null)
                        prop.SetValue(cloudValue, true);

                    toAdd.Add(cloudValue);
                    result.Added++;
                }
            }

            try
            {
                foreach (var entry in cloud.ChangeTracker.Entries().ToList())
                {
                    entry.State = EntityState.Detached;
                }
                localSet.UpdateRange(toUpdate);
                localSet.AddRange(toAdd);
                await local.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var inner = ex.InnerException?.Message ?? "❓ 無內部錯誤細節";
                MessageBox.Show($"❌ 反向同步失敗：{ex.Message}\n\n👉 InnerException：{inner}", "同步錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ 反向同步發生未預期錯誤：{ex.Message}", "同步錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }
        /// <summary>
        /// 時區轉換成Utc
        /// </summary>
        /// <param name="entity"></param>
        private static void NormalizeDateTimeProperties(object entity)
        {
            
            var props = entity.GetType().GetProperties()
                       .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));

            foreach (var prop in props)
            {
                var value = prop.GetValue(entity);
                if (value is DateTime dt)
                {
                    if (dt.Kind == DateTimeKind.Unspecified)
                    {
                        var utcDateTime = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                        prop.SetValue(entity, utcDateTime);
                    }
                }
            }
        }
        /// <summary>
        /// 比對差異
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool AreEntitiesEqual<T>(T a, T b, params string[] ignoreProperties)
        {
            var ignoreSet = new HashSet<string>(ignoreProperties ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase)
            {
                "Id", "IsSynced", "UpdatedAt", "CreatedAt"
            };

            var props = typeof(T).GetProperties()
                .Where(p => !ignoreSet.Contains(p.Name) &&
                            !Attribute.IsDefined(p, typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute)));

            foreach (var prop in props)
            {
                var aValue = prop.GetValue(a);
                var bValue = prop.GetValue(b);

                if (aValue == null && bValue == null)
                    continue;

                if (aValue == null || bValue == null)
                    return false;

                if (aValue is DateTime aTime && bValue is DateTime bTime)
                {
                    if (aTime.ToString("yyyy-MM-dd HH:mm:ss") != bTime.ToString("yyyy-MM-dd HH:mm:ss"))
                        return false;
                }
                else if (aValue is string aStr && bValue is string bStr)
                {
                    if (!string.Equals(aStr?.Trim(), bStr?.Trim(), StringComparison.Ordinal))
                        return false;
                }
                else if (aValue is double aDouble && bValue is double bDouble)
                {
                    if (Math.Abs(aDouble - bDouble) > 0.0001)
                        return false;
                }
                else if (!aValue.Equals(bValue))
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static object GetPrimaryKeyValue(object entity)
        {
            var type = entity.GetType();

            // 優先找有 [Key] 的屬性
            var keyProp = type
                .GetProperties()
                .FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute)));

            // fallback：找名稱為 "Id" 的欄位
            if (keyProp == null)
            {
                keyProp = type.GetProperty("Id");
            }

            if (keyProp == null)
            {
                throw new Exception($"❌ 類型 {type.Name} 找不到主鍵屬性（[Key] 或 Id）");
            }

            return keyProp.GetValue(entity) ?? throw new Exception($"❌ 主鍵欄位 {keyProp.Name} 的值為 null");
        }
        /// <summary>
        /// 確保表格的column存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        private static async Task EnsureTableAndColumnsExist<T>(DbContext dbContext, string tableName) where T : class
        {
            var conn = dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            // 建表（如果不存在）
            using (var createCmd = conn.CreateCommand())
            {
                createCmd.CommandText = $"CREATE TABLE IF NOT EXISTS \"{tableName}\" (\"Id\" SERIAL PRIMARY KEY);";
                await createCmd.ExecuteNonQueryAsync();
            }

            // 取得現有欄位
            var existingColumns = new HashSet<string>();
            using (var columnCmd = conn.CreateCommand())
            {
                columnCmd.CommandText = $@"
                                            SELECT column_name 
                                            FROM information_schema.columns
                                            WHERE table_name = '{tableName.ToLower()}'";

                using var reader = await columnCmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    existingColumns.Add(reader.GetString(0).ToLower());
                }
            }

            // 欄位同步
            var props = typeof(T).GetProperties().Where
            (p =>
                p.Name != "Id" &&
                !Attribute.IsDefined(p, typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute)) &&
                (p.PropertyType == typeof(string) || !typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType)) &&
                (!p.PropertyType.IsClass || p.PropertyType == typeof(string)) // 避免 navigation property
            );

            foreach (var prop in props)
            {
                var columnName = prop.Name;
                if (existingColumns.Contains(columnName.ToLower()))
                    continue;

                var type = prop.PropertyType;
                type = Nullable.GetUnderlyingType(type) ?? type;

                string columnType = type switch
                {
                    Type t when t == typeof(string) => "TEXT",
                    Type t when t == typeof(int) => "INTEGER",
                    Type t when t == typeof(long) => "BIGINT",
                    Type t when t == typeof(float) => "REAL",
                    Type t when t == typeof(double) => "DOUBLE PRECISION",
                    Type t when t == typeof(decimal) => "NUMERIC",
                    Type t when t == typeof(bool) => "BOOLEAN",
                    Type t when t == typeof(DateTime) => "TIMESTAMP",
                    Type t when t == typeof(TimeSpan) => "INTERVAL",
                    Type t when t.IsEnum => "INTEGER",
                    _ => throw new NotSupportedException($"❌ 不支援的欄位型別：{prop.PropertyType}")
                };

                using var alterCmd = conn.CreateCommand();
                alterCmd.CommandText = $"ALTER TABLE \"{tableName}\" ADD COLUMN \"{columnName}\" {columnType};";
                try
                {
                    await alterCmd.ExecuteNonQueryAsync();
                }
                catch (PostgresException ex) when (ex.SqlState == "42701") // duplicate_column
                {
                    // 欄位已存在（ignore）
                }
            }

            await conn.CloseAsync();
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
        public static void LogSyncResult(SyncResult? result = null, int direction =0)
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
                message = $"[{result.TableName}]的地端資料表上傳雲端失敗";
            }
            else
            {
                message = $"[{result.TableName}]的雲端資料表同步地端失敗";
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
                    TableSync.LogSyncResult(direction: 2);
                    return;
                }

                // 執行同步
                var Machine = await TableSync.SyncFromLocalToCloud<Machine_number>(local, cloud, "Machine");
                var MachineParameters = await TableSync.SyncFromLocalToCloud<MachineParameter>(local, cloud, "MachineParameters");
                var Machine_IO = await TableSync.SyncFromLocalToCloud<MachineIO>(local, cloud, "Machine_IO");
                var MachineIOTranslations = await TableSync.SyncFromLocalToCloud<MachineIOTranslation>(local, cloud, "MachineIOTranslation");
                var alarm = await TableSync.SyncFromLocalToCloud<Alarm>(local, cloud, "alarm", "IPC_table");
                var Histories = await TableSync.SyncFromLocalToCloud_AddOnly<History>(local, cloud, "Histories");
                var MachineParameterHistoryRecode = await TableSync.SyncFromLocalToCloud_AddOnly<MachineParameterHistoryRecode>(local, cloud, "MachineParameterHistoryRecodes");
                var AlarmHistories = await TableSync.SyncFromLocalToCloud_AddOnly<AlarmHistory>(local, cloud, "AlarmHistories");

                // 記錄 log
                TableSync.LogSyncResult(Machine);
                TableSync.LogSyncResult(Histories);
                TableSync.LogSyncResult(MachineParameters);
                TableSync.LogSyncResult(AlarmHistories);
                TableSync.LogSyncResult(Machine_IO);
                TableSync.LogSyncResult(MachineParameterHistoryRecode);
                TableSync.LogSyncResult(MachineIOTranslations);
                TableSync.LogSyncResult(alarm);


                //var alarm = await TableSync.SyncFromLocalToCloud<Alarm>(local, cloud, "alarm", "IPC_table");
                //var AlarmTranslation = await TableSync.SyncFromLocalToCloud<AlarmTranslation>(local, cloud, "AlarmTranslation","AlarmId","Id");
                //var Blade_brand = await TableSync.SyncFromLocalToCloud<Blade_brand>(local, cloud, "Blade_brand");
                //var Blade_brand_TPI = await TableSync.SyncFromLocalToCloud<Blade_brand_TPI>(local, cloud, "Blade_brand_TPI");
                //var Language = await TableSync.SyncFromLocalToCloud<Language>(local, cloud, "Language");

                //TableSync.LogSyncResult(Blade_brand);
                //TableSync.LogSyncResult(Blade_brand_TPI);
                //TableSync.LogSyncResult(Language);
                //TableSync.LogSyncResult(alarm);
                //TableSync.LogSyncResult(AlarmTranslation);
            }
            catch (Exception ex)
            {
                TableSync.LogSyncResult(direction: 2);
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
                TableSync.LogSyncResult(direction: 3);
                return;
            }
            try
            {
                var Blade_brand = await TableSync.SyncFromCloudToLocal<Blade_brand>(local, cloud, "Blade_brand");
                var Blade_brand_TPI = await TableSync.SyncFromCloudToLocal<Blade_brand_TPI>(local, cloud, "Blade_brand_TPI");
                var Language = await TableSync.SyncFromCloudToLocal<Language>(local, cloud, "Language");
                var MachineIOTranslations = await TableSync.SyncFromCloudToLocal<MachineIOTranslation>(local, cloud, "MachineIOTranslation");
                var AlarmTranslation = await TableSync.SyncFromCloudToLocal<AlarmTranslation>(local, cloud, "AlarmTranslation");

                TableSync.LogSyncResult(Blade_brand, 1);
                TableSync.LogSyncResult(Blade_brand_TPI, 1);
                TableSync.LogSyncResult(Language, 1);
                TableSync.LogSyncResult(MachineIOTranslations, 1);
                TableSync.LogSyncResult(AlarmTranslation, 1);
            }
            catch (Exception)
            {
                TableSync.LogSyncResult(direction: 3); // 失敗 log
            }
        }
    }

    public class SyncResult
    {
        public string TableName { get; set; } = "";
        public int Added { get; set; }
        public int Updated { get; set; }
        public int Total => Added + Updated;

        public override string ToString()
        {
            return $"表格 [{TableName}] 同步完成：新增 {Added} 筆，更新 {Updated} 筆，總計 {Total} 筆";
        }
        

    }

    
}
