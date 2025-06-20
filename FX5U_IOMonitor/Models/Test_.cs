using CsvHelper.Configuration;
using CsvHelper;
using FX5U_IOMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SLMP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static FX5U_IOMonitor.Models.MonitorFunction;
using static FX5U_IOMonitor.Models.MonitoringService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Npgsql.Internal;
using Npgsql;
using System.Windows.Forms;

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
        private readonly Dictionary<string, bool> lastStates = new();
        /// <summary>
        /// 測試用，從監控程式的地方移過來的
        /// </summary>
        /// <param name="table"> </param> 指定監控機台資料表
        /// <param name="calculateType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Write_Bit_Monitor_Async(SlmpClient plc, string table = "Drill", int calculateType = 1, CancellationToken? token = null)
        {
            while (token == null || !token.Value.IsCancellationRequested)
            {
                try
                {
                    var machine_output = DBfunction.Get_Machine_Calculate_type(calculateType, table);
                    var paramList = DBfunction.Get_Calculate_Readbit_address(machine_output);

                    foreach (var (name, address) in paramList)
                    {
                        bool newVal = plc.ReadBitDevice(address);

                        if (lastStates.TryGetValue(name, out bool oldVal))
                        {
                            if (oldVal != newVal && newVal == true)
                            {

                                //machine_event?.Invoke(this, new IOUpdateEventArgs
                                //{
                                //    Address = name,
                                //    OldValue = oldVal,
                                //    NewValue = newVal
                                //});
                                // 狀態有變化才做事
                                //Debug.WriteLine($"⚠ 變化：{name} {oldVal} ➜ {newVal}");

                                lastStates[name] = newVal; // 更新狀態

                                int historyVal = DBfunction.Get_Machine_History_NumericValue(name);
                                int newValue = historyVal + 1;
                                DBfunction.Set_Machine_History_NumericValue(table, name, (ushort)newValue);

                            }
                            else
                            {
                                lastStates[name] = newVal; // 更新狀態
                            }
                        }
                        else
                        {
                            lastStates[name] = newVal; // 第一次加入不觸發
                            Debug.WriteLine($"🆕 初始化 {name} = {newVal}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"❌ Bit 監控錯誤：{ex.Message}");
                }

                await Task.Delay(100, token ?? CancellationToken.None); // 每 100ms 輪詢
            }
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
        // 警告寫入的開發階段
        //string dbtable = DBfunction.FindTableWithAddress("L0");
        //if (dbtable == "") return;

        //// 移除警告通知時間更新
        //DBfunction.SetCurrentTimeAsUnmountTime(dbtable, "L0");

        //// 寫入警告通知進歷史資料
        //DBfunction.SetCurrentTimeAsMountTime(dbtable, "L0");
        //DBfunction.SetAlarmStartTime(dbtable, "L0","alarm");

        //// 寫入警告移除時間進歷史資料
        //DBfunction.SetAlarmEndTime(dbtable, "L0");
        //DBfunction.SetCurrentTimeAsUnmountTime(dbtable, "L0");


        // 寫入警告歷史資料
        //DBfunction.SetMachineIOToHistory(dbtable, "L0", "alarm");




    }







}

