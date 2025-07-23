using FX5U_IOMonitor.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.DatabaseProvider
{
    public static class TableSyncHelper
    {
        /// <summary>
        /// 地端更新雲端
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="local"></param>
        /// <param name="cloud"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static async Task<SyncResult> SyncFromLocalToCloud<T>(ApplicationDB local, CloudDbContext cloud, string tableName) where T : class
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
                    if (!AreEntitiesEqual(localValue, cloudValue))
                    {
                        var prop = localValue.GetType().GetProperty("IsSynced");
                        if (prop != null)
                            prop.SetValue(localValue, true);
                        toUpdate.Add(localValue);
                        result.Updated++;
                    }
                }
                else
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
                // 統一更新與新增
                cloudSet.UpdateRange(toUpdate);
                cloudSet.AddRange(toAdd);
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
        /// <summary>
        /// 雲端更新地端
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="local"></param>
        /// <param name="cloud"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static async Task<SyncResult> SyncFromCloudToLocal<T>(ApplicationDB local, CloudDbContext cloud, string tableName) where T : class
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
                    if (!AreEntitiesEqual(cloudValue, localValue))
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
                OnLogMessage("開始儲存");
                localSet.UpdateRange(toUpdate);
                localSet.AddRange(toAdd);
                await local.SaveChangesAsync();
                OnLogMessage("儲存完成");
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
                .Where(p => p.PropertyType == typeof(DateTime));

            foreach (var prop in props)
            {
                var value = (DateTime)prop.GetValue(entity);
                if (value.Kind == DateTimeKind.Unspecified)
                {
                    prop.SetValue(entity, DateTime.SpecifyKind(value, DateTimeKind.Utc));
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
                    "Id"
                };
            var props = typeof(T).GetProperties()
                        .Where(p =>
                            !ignoreSet.Contains(p.Name) &&
                            !Attribute.IsDefined(p, typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute)));

            foreach (var prop in props)
            {
                var aValue = prop.GetValue(a);
                var bValue = prop.GetValue(b);

                if (aValue == null && bValue == null)
                    continue;

                if (aValue == null || bValue == null || !aValue.Equals(bValue))
                    return false;
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
                    File.AppendAllText(logFilePath, $"[{DateTime.Now:yyyy-MM-dd}] {message}{Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ 寫入同步 log 檔案失敗：{ex.Message}");
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
