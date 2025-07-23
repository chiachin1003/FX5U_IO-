using CsvHelper;
using CsvHelper.Configuration;
using FX5U_IOMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using Npgsql.Internal;
using SLMP;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static FX5U_IOMonitor.Models.MonitorFunction;
using static FX5U_IOMonitor.Models.MonitoringService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FX5U_IOMonitor.Models
{
    internal class Test_
    {

       



        void read_view1()
        {
            SlmpConfig cfg = new("192.168.9.1", 2000);
            cfg.ConnTimeout = 3000;
            SlmpClient plc = new(cfg);
            plc.Connect();

            List<string> machine_output = DBfunction.Get_Machine_read_view(1, "Sawing");
            List<(string, string, ushort)> a = DBfunction.Get_Read_word_machineparameter_address("Sawing", machine_output);

            foreach (var (name, address, index) in a)
            {
                if (index == 2)
                {
                    ushort[] number = plc.ReadWordDevice(address, index);
                    string input_day = MonitorFunction.FormatPlcTime(number);
                    DBfunction.Set_Machine_now_string(name, input_day.ToString());

                }
                else
                {
                    Debug.WriteLine("超過監控區間");
                }

            }
        }

        void read_view2()
        {
            SlmpConfig cfg = new("192.168.9.1", 2000);
            cfg.ConnTimeout = 3000;
            SlmpClient plc = new(cfg);
            plc.Connect();

            List<string> machine_output = DBfunction.Get_Machine_read_view(2, "Sawing");
            List<(string, string, ushort)> a = DBfunction.Get_Read_word_machineparameter_address("Sawing", machine_output);

            foreach (var (name, address, index) in a)
            {
                if (index == 2)
                {
                    ushort[] number = plc.ReadWordDevice(address, index);
                    ushort maxValue = number.Max();
                    ushort minValue = number.Min();

                    ushort input_day = (ushort)(maxValue - minValue);
                    //DBfunction.Set_Machine_now_number(name, input_day);

                }
                else
                {
                    Debug.WriteLine("超過監控區間");
                }

            }

        }
        void read_view4()
        {
            SlmpConfig cfg = new("192.168.9.1", 2000);
            cfg.ConnTimeout = 3000;
            SlmpClient plc = new(cfg);
            plc.Connect();
            List<string> machine_output = DBfunction.Get_Machine_read_view(4, "Sawing");
            List<(string, string, ushort)> a = DBfunction.Get_Read_word_machineparameter_address("Sawing", machine_output);

            foreach (var (name, address, index) in a)
            {
                ushort value;
                string input;
                switch (name)
                {
                    case "oil_pressure":
                        value = plc.ReadWordDevice(address);
                        input = MonitorFunction.oil_press_transfer(value);
                        DBfunction.Set_Machine_now_string(name, input);
                        Debug.WriteLine($"{input}");
                        break;
                    case "Sawband_brand":
                        value = plc.ReadWordDevice(address);
                        input = DBfunction.Get_Blade_brand_name(value);
                        DBfunction.Set_Machine_now_string(name, input);
                        //DBfunction.Set_Machine_now_number(name, value);
                        Debug.WriteLine($"{input}+{value}");
                        break;

                    case "Sawblade_material":
                        value = plc.ReadWordDevice(address);
                        input = DBfunction.Get_Blade_brand_material(value);
                        DBfunction.Set_Machine_now_string(name, input);
                        //DBfunction.Set_Machine_now_number(name, value);
                        Debug.WriteLine($"{input}+{value}");

                        break;
                    case "Sawblade_type":
                        value = plc.ReadWordDevice(address);
                        int brand = DBfunction.Get_Machine_number("Sawband_brand");
                        int material = DBfunction.Get_Machine_number("Sawblade_material");
                        input = DBfunction.Get_Blade_brand_type(brand, material, value);
                        DBfunction.Set_Machine_now_string(name, input);
                        //DBfunction.Set_Machine_now_number(name, value);
                        Debug.WriteLine($"{input}+{value}");

                        break;
                    case "Sawblade_teeth":
                        value = plc.ReadWordDevice(address);
                        input = DBfunction.Get_Blade_TPI_type(value);
                        //DBfunction.Set_Machine_now_number(name, value);
                        DBfunction.Set_Machine_now_string(name, input);
                        Debug.WriteLine($"{input}+{value}");

                        break;
                }
            }

        }
        void Test()
        {
            using var context = new ApplicationDB();

            var driilIOList = context.Machine_IO.Where(d => d.Machine_name == "Drill").ToList();
            var inputStartAddr = driilIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("X"))
                .Select(a => Convert.ToInt32(a.TrimStart('X'), 16))
                .Min();

            var inputEndAddress = driilIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("X"))
                .Select(a => Convert.ToInt32(a.TrimStart('X'), 16))
                .Max();

            var outputStartAddr = driilIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("Y"))
                .Select(a => Convert.ToInt32(a.TrimStart('Y'), 16))
                .Min();

            var outputEndAddress = driilIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("Y"))
                .Select(a => Convert.ToInt32(a.TrimStart('Y'), 16))
                .Max();

            int input_index = (int)Math.Ceiling((inputEndAddress - inputStartAddr + 1) / 256.0);

            int output_index = (int)Math.Ceiling((outputEndAddress - outputStartAddr + 1) / 256.0);


            if (input_index > 1)
            {
                var inputSplitPoints = Enumerable.Range(1, input_index - 1)
                    .Select(i => inputStartAddr + i * 256)
                    .Where(addr => addr <= inputEndAddress)
                    .ToList();

                foreach (var point in inputSplitPoints)
                {
                    MessageBox.Show($"X{Convert.ToString(point, 16).PadLeft(3, '0')}");
                }
            }

            if (output_index > 1)
            {
                var outputSplitPoints = Enumerable.Range(1, output_index - 1)
                    .Select(i => outputStartAddr + i * 256)
                    .Where(addr => addr <= outputEndAddress)
                    .ToList();

                foreach (var point in outputSplitPoints)
                {
                    MessageBox.Show($"Y{Convert.ToString(point, 16).PadLeft(3, '0')}");
                }
            }

        }

        public static Dictionary<string, RuntimebitTimer> timers = new();
        System.Timers.Timer checkTimer;
        public static bool isProcessing = false;
        /// <summary>
        /// 偵測到啟動時計時，測試用
        /// </summary>
        /// <param name="plc"></param>
        public static void CheckTimers(SlmpClient plc)
        {

            if (isProcessing) return; // 防止重入

            isProcessing = true;

            try
            {
                List<string> machine_output = DBfunction.Get_Machine_Calculate_type(2, "Drill");
                List<(string name, string address)> a = DBfunction.Get_Calculate_Readbit_address(machine_output);

                foreach (var (name, address) in a)
                {
                    bool isOn = plc.ReadBitDevice(address);

                    if (!timers.ContainsKey(name))
                    {
                        int historyVal = DBfunction.Get_Machine_History_NumericValue(name);
                        timers[name] = new MonitorFunction.RuntimebitTimer
                        {
                            HistoryValue = historyVal
                        };
                    }

                    var timer = timers[name];

                    if (isOn)
                    {
                        timer.IsCounting = true;
                        timer.NowValue += 1;

                        //DBfunction.Set_Machine_now_number(name, (ushort)timer.NowValue);
                        Debug.WriteLine($"🟢 {name} 計時中：{timer.NowValue}");

                        if ((timer.NowValue) >= 30)
                        {
                            timer.HistoryValue += timer.NowValue;
                            //DBfunction.Set_Machine_History_NumericValue(name, (ushort)timer.HistoryValue);
                            timer.NowValue = 0;
                            //DBfunction.Set_Machine_now_number(name, 0);
                            Debug.WriteLine($"📥 {name} 達 600 秒：寫入歷史 = {timer.HistoryValue}");
                        }
                    }
                    else
                    {
                        if (timer.IsCounting && timer.NowValue > 0)
                        {
                            timer.HistoryValue += timer.NowValue;
                            //DBfunction.Set_Machine_History_NumericValue(name, (ushort)timer.HistoryValue);
                            //DBfunction.Set_Machine_now_number(name, 0);
                            timer.NowValue = 0;
                            Debug.WriteLine($"⏹ {name} 停止：寫入歷史 = {timer.HistoryValue}");
                        }

                        timer.IsCounting = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ 背景 Timer 發生錯誤：{ex.Message}");
            }
            finally
            {
                isProcessing = false;
            }
        }

        void test2()
        {
            using var context = new ApplicationDB();

            var SawingIOList = context.Machine_IO.Where(d => d.Machine_name == "Sawing").ToList();
            var inputStartAddr = SawingIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("X"))
                .Select(a => Convert.ToInt32(a.TrimStart('X'), 8))
                .Min();

            var inputEndAddress = SawingIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("X"))
                .Select(a => Convert.ToInt32(a.TrimStart('X'), 8))
                .Max();

            var outputStartAddr = SawingIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("Y"))
                .Select(a => Convert.ToInt32(a.TrimStart('Y'), 8))
                .Min();

            var outputEndAddress = SawingIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("Y"))
                .Select(a => Convert.ToInt32(a.TrimStart('Y'), 8))
                .Max();

            double input_index = (int)Math.Ceiling((inputEndAddress - inputStartAddr + 1) / 256.0);

            double output_index = (int)Math.Ceiling((outputEndAddress - outputStartAddr + 1) / 256.0);

        }

        public static class TableSyncHelper
        {
            public static async Task<SyncResult> SyncTableAsync<T>(ApplicationDB local, CloudDbContext cloud, string tableName) where T : class
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
            /// 比對差異
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            private static bool AreEntitiesEqual<T>(T a, T b,params string[] ignoreProperties)
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
        void test()
        {
            using var context = new ApplicationDB();

            var SawingIOList = context.Machine_IO.ToList();
            var inputStartAddr = 0;

            var inputEndAddress = 265;

            var outputStartAddr = 10;

            var outputEndAddress = 700;

            int input_index = (int)Math.Ceiling((inputEndAddress - inputStartAddr + 1) / 256.0);

            int output_index = (int)Math.Ceiling((outputEndAddress - outputStartAddr + 1) / 256.0);

            if (input_index > 1)
            {
                var inputSplitPoints = Enumerable.Range(1, input_index - 1)
                    .Select(i => inputStartAddr + i * 256)
                    .Where(addr => addr <= inputEndAddress)
                    .ToList();

                foreach (var point in inputSplitPoints)
                {
                    MessageBox.Show($"X{Convert.ToString(point, 8).PadLeft(3, '0')}");
                }
            }

            if (output_index > 1)
            {
                var outputSplitPoints = Enumerable.Range(1, output_index - 1)
                    .Select(i => outputStartAddr + i * 256)
                    .Where(addr => addr <= outputEndAddress)
                    .ToList();

                Console.WriteLine("📌 Y 切斷點：");
                foreach (var point in outputSplitPoints)
                {
                    MessageBox.Show($"Y{Convert.ToString(point, 8).PadLeft(3, '0')}");
                }
            }

        }

        private static IOSectionInfo BuildSectionFormatted(string prefix, List<int> addrList, string baseType)
        {
            int start = addrList.Min();
            int end = addrList.Max();
            int blockCount = (int)Math.Ceiling((end - start + 1) / 256.0);

            var section = new IOSectionInfo
            {
                Prefix = prefix,
                StartAddress = start,
                EndAddress = end,
                BlockCount = blockCount
            };

            if (blockCount > 1)
            {
                section.SplitPoints = Enumerable.Range(1, blockCount - 1)
                    .Select(i => start + i * 256)
                    .Where(p => p <= end)
                    .Select(p => Calculate.Format(prefix, p, baseType))
                    .ToList();
            }

            return section;
        }
     
        /// <summary>
        /// 動態刪除並重整資料庫
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableSelector"></param>
        /// <param name="csvFilePath"></param>
        private void DeleteAndInitialize<T>(Func<ApplicationDB, DbSet<T>> tableSelector, string csvFilePath) where T : class
        {
            using (var context = new ApplicationDB())
            {
                var dbSet = tableSelector(context);          // 動態選取 DbSet
                var allItems = dbSet.ToList();               // 撈出所有資料
                dbSet.RemoveRange(allItems);                 // 批次移除
                context.SaveChanges();                       // 寫入變更
            }

            // 初始化資料（依資料表類型呼叫對應初始化方法）
            if (typeof(T) == typeof(MachineParameter))
            {
                Csv2Db.Initialization_MachineprameterFromCSV(csvFilePath);
            }
            else if (typeof(T) == typeof(Alarm))
            {
                Csv2Db.Initialization_AlarmFromCSV(csvFilePath);
            }
            else if (typeof(T) == typeof(HistoryRecord))
            {
                MessageBox.Show($"已經清空歷史資料表");
            }
            else if (typeof(T) == typeof(Blade_brand))
            {
                Csv2Db.Initialization_BladeBrandFromCSV(csvFilePath);
            }
            else if (typeof(T) == typeof(Blade_brand_TPI))
            {
                Csv2Db.Initialization_BladeTPIFromCSV(csvFilePath);
            }
            else
            {
                MessageBox.Show($"尚未支援初始化 {typeof(T).Name} 的對應 CSV 方法！");
            }


        }
       
    }







}

