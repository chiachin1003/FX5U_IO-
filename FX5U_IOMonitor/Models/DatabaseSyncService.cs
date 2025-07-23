using FX5U_IOMonitor.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Models
{
    public abstract class SyncableEntity
    {
        public bool IsSynced { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
    public enum SyncMode
    {
        /// <summary>
        /// 只將地端新增/修改的資料同步到雲端
        /// </summary>
        LocalToCloud,
        /// <summary>
        /// 雲端完全與地端同步（刪除雲端多餘資料）
        /// </summary>
        CompleteSync,
        /// <summary>
        /// 雙向同步（保留兩邊所有資料）
        /// </summary>
        Bidirectional
    }
    public class DatabaseSyncService
    {
        private System.Windows.Forms.Timer _syncTimer;
        private bool _isSyncing = false;
        private readonly object _lockObject = new object();

        // 事件，用於通知同步狀態
        public event EventHandler<SyncStatusEventArgs> SyncStatusChanged;
        public event EventHandler<string> LogMessage;

        public bool IsRunning { get; private set; }
        public SyncMode CurrentSyncMode { get; set; } = SyncMode.CompleteSync;

        public DatabaseSyncService()
        {
            _syncTimer = new System.Windows.Forms.Timer();
            _syncTimer.Interval = 30000; // 30秒
            _syncTimer.Tick += OnTimerTick;
        }

        public void Start()
        {
            if (!IsRunning)
            {
                _syncTimer.Start();
                IsRunning = true;
                OnLogMessage($"資料庫同步服務已啟動 (模式: {CurrentSyncMode})");
                OnSyncStatusChanged(new SyncStatusEventArgs { IsRunning = true, Message = "同步服務已啟動" });
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                _syncTimer.Stop();
                IsRunning = false;
                OnLogMessage("資料庫同步服務已停止");
                OnSyncStatusChanged(new SyncStatusEventArgs { IsRunning = false, Message = "同步服務已停止" });
            }
        }

        private async void OnTimerTick(object sender, EventArgs e)
        {
            // 防止重複執行
            lock (_lockObject)
            {
                if (_isSyncing)
                    return;
                _isSyncing = true;
            }

            try
            {
                await SyncDatabasesAsync();
            }
            catch (Exception ex)
            {
                OnLogMessage($"同步錯誤: {ex.Message}");
                Debug.WriteLine(($"同步錯誤: {ex.Message}"));
                OnSyncStatusChanged(new SyncStatusEventArgs { IsRunning = true, Message = $"同步錯誤: {ex.Message}", HasError = true });
            }
            finally
            {
                lock (_lockObject)
                {
                    _isSyncing = false;
                }
            }
        }
        public async Task SyncDatabasesAsync()
        {
            try
            {
                OnSyncStatusChanged(new SyncStatusEventArgs { IsRunning = true, Message = "開始同步...", IsSyncing = true });

                using var localContext = new ApplicationDB();
                using var cloudContext = new CloudDbContext();
                var syncResult = new SyncResult();

                var dbSetProperties = typeof(ApplicationDB)
                    .GetProperties()
                    .Where(p => p.PropertyType.IsGenericType &&
                                p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                    .ToList();

                foreach (var prop in dbSetProperties)
                {
                    var entityType = prop.PropertyType.GenericTypeArguments[0];

                    // 只處理繼承 SyncableEntity 的表格
                    if (!typeof(SyncableEntity).IsAssignableFrom(entityType))
                        continue;

                    string tableName = prop.Name;

                    var safeSyncMethod = typeof(DatabaseSyncService)
                        .GetMethod(nameof(SafeSync), BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.MakeGenericMethod(entityType);

                    var localAccessorMethod = typeof(DatabaseSyncService)
                        .GetMethod(nameof(BuildDbSetAccessor), BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.MakeGenericMethod(typeof(ApplicationDB), entityType);

                    var cloudAccessorMethod = typeof(DatabaseSyncService)
                        .GetMethod(nameof(BuildDbSetAccessor), BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.MakeGenericMethod(typeof(CloudDbContext), entityType);

                    var localAccessor = localAccessorMethod?.Invoke(this, new object[] { prop });
                    var cloudProp = typeof(CloudDbContext).GetProperty(tableName);
                    if (cloudProp == null)
                    {
                        OnLogMessage($"⚠️ 雲端資料表 {tableName} 不存在於 CloudDbContext，略過");
                        continue;
                    }

                    var cloudAccessor = cloudAccessorMethod?.Invoke(this, new object[] { cloudProp });

                    await (Task)safeSyncMethod?.Invoke(this, new object[]
                    {
                localContext,
                cloudContext,
                localAccessor,
                cloudAccessor,
                tableName,
                syncResult
                    });
                }

                string message = $"✅ 同步完成 - {DateTime.UtcNow:HH:mm:ss} (新增: {syncResult.Added}, 更新: {syncResult.Updated}, 刪除: {syncResult.Deleted})";
                OnLogMessage(message);
                OnSyncStatusChanged(new SyncStatusEventArgs { IsRunning = true, Message = message, IsSyncing = false });
            }
            catch (Exception ex)
            {
                OnLogMessage($"❌ 同步資料庫時發生錯誤: {ex.Message}");
                throw;
            }
        }
        private Func<TContext, DbSet<T>> BuildDbSetAccessor<TContext, T>(PropertyInfo prop)
             where TContext : DbContext
             where T : class
        {
            var param = Expression.Parameter(typeof(TContext), "ctx");
            var body = Expression.Property(param, prop);
            var lambda = Expression.Lambda<Func<TContext, DbSet<T>>>(body, param);
            return lambda.Compile();
        }
        private async Task SyncTable<T>(
            ApplicationDB localContext,
            CloudDbContext cloudContext,
            Func<ApplicationDB, DbSet<T>> getLocalSet,
            Func<CloudDbContext, DbSet<T>> getCloudSet,
            string tableName,
            SyncResult syncResult) where T : class
        {
            var localSet = getLocalSet(localContext);
            var cloudSet = getCloudSet(cloudContext);

            OnLogMessage($"開始同步 {tableName} 表格...");

            switch (CurrentSyncMode)
            {
                case SyncMode.LocalToCloud:
                    await SyncLocalToCloud<T>(localContext, cloudContext, localSet, cloudSet, tableName, syncResult);
                    break;

                case SyncMode.CompleteSync:
                    await SyncComplete<T>(localContext, cloudContext, localSet, cloudSet, tableName, syncResult);
                    break;

                case SyncMode.Bidirectional:
                    await SyncBidirectional<T>(localContext, cloudContext, localSet, cloudSet, tableName, syncResult);
                    break;
            }
        }
        private async Task SafeSync<T>(
                     ApplicationDB localContext,
                     CloudDbContext cloudContext,
                     Func<ApplicationDB, DbSet<T>> localSelector,
                     Func<CloudDbContext, DbSet<T>> cloudSelector,
                     string tableName,
                     SyncResult syncResult) where T : class
        {
            try
            {
                await SyncTable(localContext, cloudContext, localSelector, cloudSelector, tableName, syncResult);
            }
            catch (Exception ex)
            {
                string error = $"❌ 表 [{tableName}] 同步失敗: {ex.Message}";
                OnLogMessage(error);
            }
        }
        /// <summary>
        /// 只將地端資料同步到雲端
        /// </summary>
        private async Task SyncLocalToCloud<T>(
            ApplicationDB localContext,
            CloudDbContext cloudContext,
            DbSet<T> localSet,
            DbSet<T> cloudSet,
            string tableName,
            SyncResult syncResult) where T : class
        {
            if (!typeof(SyncableEntity).IsAssignableFrom(typeof(T)))
            {
                OnLogMessage($"{tableName} 表格未繼承 SyncableEntity，略過同步");
                return;
            }

            var unsyncedData = await localSet
                .Cast<SyncableEntity>()
                .Where(x => !x.IsSynced)
                .Cast<T>()
                .ToListAsync();

            if (!unsyncedData.Any())
                return;

            foreach (var item in unsyncedData)
            {
                try
                {
                    var keyValues = GetEntityKeyValues(localContext, item);
                    var existingItem = await FindEntityByKey(cloudContext, cloudSet, keyValues);

                    if (existingItem != null)
                    {
                        cloudContext.Entry(existingItem).CurrentValues.SetValues(item);
                        syncResult.Updated++;
                    }
                    else
                    {
                        cloudSet.Add(item);
                        syncResult.Added++;
                    }
                }
                catch (Exception ex)
                {
                    OnLogMessage($"同步 {tableName} 表格項目時發生錯誤: {ex.Message}");
                    continue;
                }
            }

            await cloudContext.SaveChangesAsync();

            // 標記本地資料為已同步
            foreach (var item in unsyncedData.Cast<SyncableEntity>())
            {
                item.IsSynced = true;
            }
            await localContext.SaveChangesAsync();
        }
        /// <summary>
        /// 自動補資料表欄位
        /// </summary>
        /// <param name="cloudContext"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private async Task<List<string>> GetCloudTableColumnsAsync(CloudDbContext cloudContext, string tableName)
        {
            var sql = $@"
        SELECT column_name
        FROM information_schema.columns
        WHERE table_name = '{tableName.ToLower()}';";

            using var command = cloudContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;
            if (command.Connection.State != System.Data.ConnectionState.Open)
                await command.Connection.OpenAsync();

            var result = new List<string>();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(reader.GetString(0));
            }

            return result;
        }
        /// <summary>
        /// 自動補表的欄位
        /// </summary>
        /// <param name="cloudContext"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private async Task<bool> CloudTableExistsAsync(CloudDbContext cloudContext, string tableName)
        {
            var sql = $@"
        SELECT EXISTS (
            SELECT 1
            FROM information_schema.tables 
            WHERE table_name = '{tableName.ToLower()}'
        );";

            using var command = cloudContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;
            if (command.Connection.State != System.Data.ConnectionState.Open)
                await command.Connection.OpenAsync();

            var exists = (bool?)await command.ExecuteScalarAsync();
            return exists == true;
        }
        /// <summary>
        /// 型別邏輯
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetPostgresColumnType(Type type)
        {
            if (type == typeof(int) || type == typeof(int?))
                return "INTEGER";
            if (type == typeof(long) || type == typeof(long?))
                return "BIGINT";
            if (type == typeof(float) || type == typeof(float?))
                return "REAL";
            if (type == typeof(double) || type == typeof(double?))
                return "DOUBLE PRECISION";
            if (type == typeof(decimal) || type == typeof(decimal?))
                return "NUMERIC";
            if (type == typeof(string))
                return "TEXT";
            if (type == typeof(bool) || type == typeof(bool?))
                return "BOOLEAN";
            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return "TIMESTAMP";

            return "TEXT"; // fallback 型別
        }
        /// <summary>
        /// 自動建表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string GenerateCreateTableSql<T>(string tableName)
        {
            var props = typeof(T).GetProperties()
                .Where(p => p.PropertyType.IsPrimitive || p.PropertyType == typeof(string) || p.PropertyType == typeof(DateTime))
                .ToList();

            var columns = new List<string>();
            foreach (var prop in props)
            {
                string name = prop.Name;
                string type = GetPostgresColumnType(prop.PropertyType);
                string nullability = IsNullable(prop.PropertyType) ? "" : "NOT NULL";
                columns.Add($"\"{name}\" {type} {nullability}".Trim());
            }

            // 主鍵：自動用 Id 當主鍵
            if (props.Any(p => p.Name == "Id"))
                columns.Add("PRIMARY KEY (\"Id\")");

            return $"CREATE TABLE \"{tableName}\" (\n  {string.Join(",\n  ", columns)}\n);";
        }

        private bool IsNullable(Type type) => !type.IsValueType || Nullable.GetUnderlyingType(type) != null;

        /// <summary>
        /// 完整同步：讓雲端資料完全與地端相同
        /// </summary>
        private async Task SyncComplete<T>(
            ApplicationDB localContext,
            CloudDbContext cloudContext,
            DbSet<T> localSet,
            DbSet<T> cloudSet,
            string tableName,
            SyncResult syncResult) where T : class
        {
            

            bool tableExists = await CloudTableExistsAsync(cloudContext, tableName);
            if (!tableExists)
            {
                try
                {
                    string createTableSql = GenerateCreateTableSql<T>(tableName);
                    await cloudContext.Database.ExecuteSqlRawAsync(createTableSql);
                    OnLogMessage($"✅ 已自動建立雲端資料表 {tableName}");
                }
                catch (Exception ex)
                {
                    OnLogMessage($"❌ 建立表格 {tableName} 時發生錯誤: {ex.Message}");
                    return;
                }
            }

            // 獲取所有地端資料
            var localData = await localSet.ToListAsync();

            // 獲取所有雲端資料
            var cloudData = await cloudSet.ToListAsync();
            var localProps = typeof(T)
                            .GetProperties()
                            .Where(p => p.PropertyType.IsPrimitive || p.PropertyType == typeof(string) || p.PropertyType == typeof(DateTime))
                            .ToList();

            // 取得 cloud 目前欄位
            var cloudColumns = await GetCloudTableColumnsAsync(cloudContext, tableName);

            // 找出雲端沒有的欄位
            var missingColumns = localProps
                .Where(p => !cloudColumns.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                .ToList();
            if (cloudColumns.Count!=missingColumns.Count)
            {
                // 如果有缺少欄位，就動態補上
                foreach (var prop in missingColumns)
                {
                    try
                    {
                        string columnName = prop.Name;
                        string columnType = GetPostgresColumnType(prop.PropertyType);
                        string alterSql = $"ALTER TABLE \"{tableName}\" ADD COLUMN \"{columnName}\" {columnType};";

                        await cloudContext.Database.ExecuteSqlRawAsync(alterSql);
                        OnLogMessage($"🔧 雲端表格 {tableName} 補上欄位: {columnName} ({columnType})");
                    }
                    catch (Exception ex)
                    {
                        OnLogMessage($"❌ 雲端補欄位失敗：{prop.Name}，原因：{ex.Message}");
                    }
                }
            }
           

            // 如果是 SyncableEntity，比較主鍵來判斷資料
            if (typeof(SyncableEntity).IsAssignableFrom(typeof(T)))
            {
                // 找出需要新增的資料（地端有，雲端沒有）
                var toAdd = new List<T>();
                var toUpdate = new List<(T local, T cloud)>();

                foreach (var localItem in localData)
                {
                    var localKeys = GetEntityKeyValues(localContext, localItem);
                    var existingCloudItem = await FindEntityByKey(cloudContext, cloudSet, localKeys);

                    if (existingCloudItem == null)
                    {
                        toAdd.Add(localItem);
                    }
                    else
                    {
                        // 檢查是否需要更新
                        var localEntity = (SyncableEntity)(object)localItem;
                        var cloudEntity = (SyncableEntity)(object)existingCloudItem;

                        if (localEntity.UpdatedAt > cloudEntity.UpdatedAt)
                        {
                            toUpdate.Add((localItem, existingCloudItem));
                        }
                    }
                }

                // 找出需要刪除的資料（雲端有，地端沒有）
                var toDelete = new List<T>();
                foreach (var cloudItem in cloudData)
                {
                    var cloudKeys = GetEntityKeyValues(cloudContext, cloudItem);
                    var existingLocalItem = await FindEntityByKey(localContext, localSet, cloudKeys);

                    if (existingLocalItem == null)
                    {
                        toDelete.Add(cloudItem);
                    }
                }

                // 執行新增
                if (toAdd.Any())
                {
                    cloudSet.AddRange(toAdd);
                    syncResult.Added += toAdd.Count;
                    await cloudContext.SaveChangesAsync(); // 確保主鍵生成

                }
             
                // 執行更新
                foreach (var (local, cloud) in toUpdate)
                {
                    cloudContext.Entry(cloud).CurrentValues.SetValues(local);
                    syncResult.Updated++;
                }

                // 執行刪除
                if (toDelete.Any())
                {
                    cloudSet.RemoveRange(toDelete);
                    syncResult.Deleted += toDelete.Count;
                }

                await cloudContext.SaveChangesAsync();

                // 標記所有地端資料為已同步
                foreach (var item in localData.Cast<SyncableEntity>())
                {
                    item.IsSynced = true;
                }
                await localContext.SaveChangesAsync();
            }
            else
            {
                // 對於非 SyncableEntity 的表格，進行完整替換
                cloudSet.RemoveRange(cloudData);
                cloudSet.AddRange(localData);
                await cloudContext.SaveChangesAsync();

                syncResult.Deleted += cloudData.Count;
                syncResult.Added += localData.Count;
            }

            OnLogMessage($"{tableName} 完整同步完成 (新增: {syncResult.Added}, 更新: {syncResult.Updated}, 刪除: {syncResult.Deleted})");
        }

        /// <summary>
        /// 雙向同步：保留兩邊所有資料
        /// </summary>
        private async Task SyncBidirectional<T>(
            ApplicationDB localContext,
            CloudDbContext cloudContext,
            DbSet<T> localSet,
            DbSet<T> cloudSet,
            string tableName,
            SyncResult syncResult) where T : class
        {
            if (!typeof(SyncableEntity).IsAssignableFrom(typeof(T)))
            {
                OnLogMessage($"{tableName} 表格未繼承 SyncableEntity，無法進行雙向同步");
                return;
            }

            var localData = await localSet.ToListAsync();
            var cloudData = await cloudSet.ToListAsync();

            // 從雲端同步到地端
            foreach (var cloudItem in cloudData)
            {
                var cloudKeys = GetEntityKeyValues(cloudContext, cloudItem);
                var existingLocalItem = await FindEntityByKey(localContext, localSet, cloudKeys);

                if (existingLocalItem == null)
                {
                    // 雲端有，地端沒有，新增到地端
                    localSet.Add(cloudItem);
                    syncResult.Added++;
                }
                else
                {
                    // 比較更新時間
                    var localEntity = (SyncableEntity)(object)existingLocalItem;
                    var cloudEntity = (SyncableEntity)(object)cloudItem;

                    if (cloudEntity.UpdatedAt > localEntity.UpdatedAt)
                    {
                        localContext.Entry(existingLocalItem).CurrentValues.SetValues(cloudItem);
                        syncResult.Updated++;
                    }
                }
            }

            // 從地端同步到雲端
            foreach (var localItem in localData)
            {
                var localKeys = GetEntityKeyValues(localContext, localItem);
                var existingCloudItem = await FindEntityByKey(cloudContext, cloudSet, localKeys);

                if (existingCloudItem == null)
                {
                    // 地端有，雲端沒有，新增到雲端
                    cloudSet.Add(localItem);
                    syncResult.Added++;
                }
                else
                {
                    // 比較更新時間
                    var localEntity = (SyncableEntity)(object)localItem;
                    var cloudEntity = (SyncableEntity)(object)existingCloudItem;

                    if (localEntity.UpdatedAt > cloudEntity.UpdatedAt)
                    {
                        cloudContext.Entry(existingCloudItem).CurrentValues.SetValues(localItem);
                        syncResult.Updated++;
                    }
                }
            }

            await localContext.SaveChangesAsync();
            await cloudContext.SaveChangesAsync();
        }

        private object[] GetEntityKeyValues(DbContext context, object entity)
        {
            var entityType = context.Model.FindEntityType(entity.GetType());
            var key = entityType.FindPrimaryKey();
            return key.Properties.Select(p => p.PropertyInfo.GetValue(entity)).ToArray();
        }

        private async Task<T> FindEntityByKey<T>(DbContext context, DbSet<T> dbSet, object[] keyValues) where T : class
        {
            return await dbSet.FindAsync(keyValues);
        }

        protected virtual void OnSyncStatusChanged(SyncStatusEventArgs e)
        {
            SyncStatusChanged?.Invoke(this, e);
        }
        private readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sync_log.txt");

        protected virtual void OnLogMessage(string message)
        {
            LogMessage?.Invoke(this, message);
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
      
        public void Dispose()
        {
            Stop();
            _syncTimer?.Dispose();
        }
    }

    public class SyncStatusEventArgs : EventArgs
    {
        public bool IsRunning { get; set; }
        public bool IsSyncing { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }
    }

    public class SyncResult
    {
        public int Added { get; set; } = 0;
        public int Updated { get; set; } = 0;
        public int Deleted { get; set; } = 0;
    }

  
}
