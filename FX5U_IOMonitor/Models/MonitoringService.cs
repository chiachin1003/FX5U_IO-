using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SLMP;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static FX5U_IOMonitor.Check_point;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Models.MonitorFunction;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace FX5U_IOMonitor.Models
{

    public class MonitoringService
    {
        //資料變更事件
        public class IOUpdateEventArgs : EventArgs
        {
            public string Address { get; set; }
            public bool NewValue { get; set; }
            public bool OldValue { get; set; }
        }

        public class MonitorService
        {

            // 宣告事件：通知程式「某個 IO 資料變了」
            public event EventHandler<IOUpdateEventArgs> IOUpdated; //實體元件事件(X輸入及Y輸出)
            public event EventHandler<IOUpdateEventArgs> alarm_event; //警告事件事件
            public event EventHandler<IOUpdateEventArgs> machine_event;
            public event EventHandler<RULThresholdCrossedEventArgs>? RULThresholdCrossed;

            private readonly Dictionary<string, string> _lastRULState = new();// 紀錄每個元件上次 Message 燈號狀態（green/yellow/red）
            private Dictionary<string, RULThresholdInfo> _rulCache = new();
            private DateTime _lastRULCacheTime = DateTime.MinValue;
            private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

            public string MachineName { get; }

            public event EventHandler<MachineParameterChangedEventArgs>? MachineParameterChanged;


            private SlmpClient plc;
            private object? externalLock;
            private bool isFirstRead = true; // 實體元件監控是否初始化
            private bool alarmFirstRead = true;

            public void SetExternalLock(object locker)
            {
                this.externalLock = locker;
            }


            // 宣告目標 plc 
            public MonitorService(SlmpClient PLC, string machineName)
            {
                this.plc = PLC;
                this.MachineName = machineName;
                bool isFirstRead = true;
                bool alarmFirstRead = true; // 實體元件監控是否初始化

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task MonitoringLoop(CancellationToken token, string machinname)
            {
                
                while (!token.IsCancellationRequested)
                {
                    Monitoring(machinname);
                    await Task.Delay(500); // 每 500 毫秒執行一次
                }
            }

            /// <summary>
            ///  PLC 讀取實體元件監控 + 轉換通用式
            /// </summary>
            /// <param name="old_single"></param>
            public void Monitoring(string machinname)
            {
                List<now_single> old_single = DBfunction.Get_Machine_current_single_all(machinname);
                string format = DBfunction.Get_Element_baseType(machinname);
                var Drill = Calculate.AnalyzeIOSections(machinname, format); 
                var sectionGroups = Drill
                    .GroupBy(s => s.Prefix)
                    .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));

                Stopwatch stopwatch = Stopwatch.StartNew();
                lock (externalLock ?? new object())
                {
                    //if ((DateTime.Now - _lastRULCacheTime) > _cacheDuration || _rulCache.Count == 0)
                    //{
                    //    _rulCache = RULNotifier.GetRULMapByMachine(machinname);
                    //    _lastRULCacheTime = DateTime.Now;
                    //}
                    _rulCache = RULNotifier.GetRULMapByMachine(machinname);
                    foreach (var prefix in sectionGroups.Keys)
                    {
                        var blocks = sectionGroups[prefix];

                        foreach (var block in blocks)
                        {
                            string device = prefix + block.Start;
                            try
                            {
                                bool[] plc_result = plc.ReadBitDevice(device, 256);
                                var result = Calculate.ConvertPlcToNowSingle(plc_result, prefix, block.Start, format);
                                if (isFirstRead)
                                {
                                    int updated = Calculate.UpdateIOCurrentSingleToDB(result, machinname);
                                    // 初始化 Message 狀態快取
                                    foreach (var now in result)
                                    {
                                        if (_rulCache.TryGetValue(now.address, out var info))
                                        {
                                            string initialState = GetRULState(info); 
                                            _lastRULState[now.address] = initialState;
                                        }
                                    }
                                    var context = MachineHub.Get(machinname);
                                    if (context != null)
                                    {
                                     
                                        context.ConnectSummary.connect += updated;
                                    }

                                }
                                else
                                {
                                    //UpdateIODataBaseFromNowSingle(result, old_single);
                                    UpdateIODataBaseFromNowSingle(result, old_single, _rulCache);
                                }


                            }
                            catch
                            {
                                isFirstRead = true; // 斷線時設定下次重新初始化
                                break;
                                //return; // 中止此次讀取
                            }
                        }

                    }
                    stopwatch.Stop();
                    // ✅ 更新斷線數與耗時資訊
                    var monition = MachineHub.Get(machinname);
                    if (monition != null)
                    {
                        monition.ConnectSummary.read_time = $" {stopwatch.ElapsedMilliseconds}";
                        monition.ConnectSummary.disconnect = DBfunction.GetMachineRowCount(monition.MachineName) - monition.ConnectSummary.connect;
                        
                    }
                }
                isFirstRead = false; // ✅ 完成第一次後設定為 false
            }

            /// <summary>
            /// 實體元件IO狀態比較與事件觸發
            /// </summary>
            /// <param name="nowList"></param>
            /// <param name="oldList"></param>
            private void UpdateIODataBaseFromNowSingle(List<now_single> nowList, List<now_single> oldList, Dictionary<string, RULThresholdInfo> rulMap)
            {

                if (nowList == null || nowList.Count == 0 || oldList == null || oldList.Count == 0)
                {
                    Console.WriteLine("Error: nowList 或 ioList 為空.");
                }

                int updatedCount = 0;
                foreach (var now in nowList)
                {
                    var ioMatch = oldList.FirstOrDefault(io => io.address == now.address);
                    if (ioMatch != null)
                    {
                        //比對當前信號值
                        if (ioMatch.current_single is bool oldVal)
                        {
                            bool newVal = now.current_single;

                            if (oldVal != newVal)
                            {
                                ioMatch.current_single = newVal;
                                updatedCount++;

                                IOUpdated?.Invoke(this, new IOUpdateEventArgs
                                {
                                    Address = now.address,
                                    OldValue = oldVal,
                                    NewValue = newVal
                                });

                            }
                        }
                        // 檢查 Message 狀態是否發生變化
                        if (rulMap.TryGetValue(ioMatch.address, out var info))
                        {
                            string currentRULState = GetRULState(info); // green / yellow / red
                            string key = ioMatch.address;


                            // 只有在狀態變化時，才更新並觸發通知
                            if (_lastRULState[ioMatch.address] != currentRULState)
                            {
                                _lastRULState[key] = currentRULState; // 更新快取狀態

                                // 僅當新的狀態是 yellow / red 時才發送通知
                                if (currentRULState == "yellow" || currentRULState == "red")
                                {
                                    RULThresholdCrossed?.Invoke(this, new RULThresholdCrossedEventArgs
                                    {
                                        Address = ioMatch.address,
                                        Machine = MachineName,
                                        RUL = info.RUL,
                                        State = currentRULState
                                    });
                                }
                            }


                        }

                    }

                }
            }
            private string GetRULState(RULThresholdInfo info)
            {
                if (info.SetY < 0 || info.SetR < 0) return "unknown";
                if (info.RUL <= info.SetR) return "red";
                if (info.RUL <= info.SetY) return "yellow";
                return "green";
            }


            /// <summary>
            /// 警告監控輪詢與延遲
            /// </summary>
            /// <param name="token"></param>
            /// <returns></returns>
            public async Task alarm_MonitoringLoop(CancellationToken token)
            {
                
                while (!token.IsCancellationRequested)
                {
                    Alarm_Monitoring();
                    await Task.Delay(50); // 每 500 毫秒執行一次
                }
            }
            /// <summary>
            /// 負責 PLC 警告讀取 + 轉換
            /// </summary>
            /// <param name="old_single"></param>
            public void Alarm_Monitoring()
            {
                List<now_single> old_single = DBfunction.Get_alarm_current_single_all();

                var alarm = Calculate.Alarm_trans();
                var sectionGroups = alarm
                    .GroupBy(s => s.Prefix)
                    .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));


                lock (externalLock ?? new object())
                {
                    foreach (var prefix in sectionGroups.Keys)
                    {
                        var blocks = sectionGroups[prefix];

                        foreach (var block in blocks)
                        {
                            string device = prefix + block.Start;
                            try
                            {
                                bool[] plc_result = plc.ReadBitDevice(device, 256);
                                var result = Calculate.Convert_alarmsingle(plc_result, prefix, block.Start);
                                if (isFirstRead)
                                {
                                   Calculate.UpdatealarmCurrentSingleToDB(result);
                                }
                                else
                                {

                                    alarm_NowSingle(result, old_single);
                                }

                            }
                            catch
                            {
                                alarmFirstRead = true; // 斷線時設定下次重新初始化
                                return; // 中止此次讀取
                            }
                        }
                    }

                }
                alarmFirstRead = false; // ✅ 完成第一次後設定為 false

                return;
            }

            /// <summary>
            /// 狀態比較與事件觸發
            /// </summary>
            /// <param name="now_single"></param>
            /// <param name="old_single"></param>
            private void alarm_NowSingle(List<now_single> now_single, List<now_single> old_single)
            {
                if (now_single == null || now_single.Count == 0 || old_single == null || old_single.Count == 0)
                {
                    Console.WriteLine("Error: nowList 或 ioList 為空.");
                }

                foreach (var now in now_single)
                {
                    var ioMatch = old_single.FirstOrDefault(io => io.address == now.address);
                    if (ioMatch != null)
                    {

                        if (ioMatch.current_single is bool oldVal)
                        {
                            bool newVal = now.current_single;

                            if (oldVal != newVal)
                            {
                                ioMatch.current_single = newVal;

                                alarm_event?.Invoke(this, new IOUpdateEventArgs
                                {
                                    Address = now.address,
                                    OldValue = oldVal,
                                    NewValue = newVal
                                });

                            }
                        }
                    }

                }
            }



            private readonly Dictionary<string, bool> lastStates = new();
            private readonly Dictionary<string, RuntimebitTimer> timer_bit = new();
            private readonly Dictionary<string, RuntimewordTimer> timer_word = new();

            public async Task Write_Word_Monitor_AllModesAsync(string machine_name, int[] WriteTypes, CancellationToken? token = null)
            {
                // 初始化對應監控清單
                var modeAddressMap = new Dictionary<int, List<(string name, string address, int? address_index)>>();

                // 初始化對應監控清單
                foreach (int now_writeType in WriteTypes.Distinct())
                {
                    var names = DBfunction.Get_Machine_write_view(machine_name, now_writeType);
                    var addresses = DBfunction.Get_Write_word_machineparameter_address(machine_name, names);
                    modeAddressMap[now_writeType] = addresses;
                }

                while (token == null || !token.Value.IsCancellationRequested)
                {
                    try
                    {

                        foreach (var kv in modeAddressMap)
                        {
                            int type = kv.Key;
                            var paramList = kv.Value;
                            var prefixes = paramList
                                    .Select(a => new string(a.address.TakeWhile(char.IsLetter).ToArray()))
                                    .Distinct() // 去除重複
                                    .ToList();

                            var paramLists = Calculate.SplitAddressSections(paramList.Select(p => p.address).ToList());
                            var sectionGroups = paramLists
                                                .GroupBy(s => s.Prefix)
                                                .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));

                            foreach (var prefix in sectionGroups.Keys)
                            {
                                var blocks = sectionGroups[prefix];

                                ushort[] readResults;

                                foreach (var block in blocks)
                                {
                                    string device = prefix + block.Start;

                                    lock (externalLock ?? new object())
                                    {
                                        readResults = plc.ReadWordDevice(device, 256);
                                    }

                                    List<now_number> result = Calculate.Convert_wordsingle(readResults, prefix, block.Start);

                                    // 對目前這個區塊內的參數做處理（篩選 paramList 中對應此區塊的）
                                    var relevantParams = paramList
                                        .Where(p => p.address.StartsWith(prefix))
                                        .ToList();


                                    foreach (var (name, address, address_index) in relevantParams)
                                    {
                                        // 從 result 中找出對應位址的值
                                        var match = result.FirstOrDefault(r => r.address == address);
                                        if (match == null) continue;
                                        if (address_index == -1 || address_index == 0)  // ✅ 你指定的不處理值
                                        {
                                            //Debug.WriteLine($"⏭️ [{name}] address_index = {address_index}，跳過寫入。");
                                            continue;
                                        }
                                        try
                                        {
                                            if (address_index == 1)
                                            {
                                                // 寫入固定值、遞增值、時間戳等你自訂邏輯
                                                ushort value = (ushort)(DBfunction.Get_History_NumericValue(machine_name, name));  // 範例：寫入秒數
                                                lock (externalLock ?? new object())
                                                {
                                                    plc.WriteWordDevice(match.address, value);
                                                }
                                                //Debug.WriteLine($" [{name}] Machine=1 → 寫入次數：{value}");
                                            }
                                            else
                                            {
                                                // 寫入多變數
                                                int? value = DBfunction.Get_History_NumericValue(machine_name, name);
                                                if (!value.HasValue)
                                                {
                                                    Debug.WriteLine($"❌ 寫入失敗 [{name}] 位址：{match.address}，原因：資料為 null");
                                                    return;
                                                }

                                                ushort[] write2plc = MonitorFunction.SmartWordSplit(value.Value, 2,1);

                                                lock (externalLock ?? new object())
                                                {
                                                    plc.WriteWordDevice(match.address, write2plc);
                                                }
                                                //ushort[] write2plc = MonitorFunction.SmartWordSplit(value, (int)address_index);
                                                //lock (externalLock ?? new object())
                                                //{
                                                //    plc.WriteWordDevice(match.address, write2plc);
                                                //}
                                                ////Debug.WriteLine($" [{name}] 寫入位址 {match.address}，值：{value} → WordData = [{string.Join(", ", write2plc)}]");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Debug.WriteLine($"❌ 寫入失敗 [{name}] 位址：{match.address}，錯誤：{ex.Message}");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"❌ 全域寫入例外（{machine_name}）：{ex.Message}");

                    }

                }

            }



            /// <summary>
            /// 讀取PLC bit的監控方式
            /// </summary>
            /// <param name="plc"></param>
            /// <param name="table"></param>
            /// <param name="calculateType"></param>
            /// <param name="token"></param>
            /// <returns></returns>
            /// 

            public async Task Read_Bit_Monitor_AllModesAsync(string machine_name, int[] calculateTypes, CancellationToken? token = null)
            {
                // 初始化對應監控清單
                var modeAddressMap = new Dictionary<int, List<(string name, string address)>>();


                foreach (int type in calculateTypes.Distinct())
                {
                    var names = DBfunction.Get_Machine_Calculate_type(type, machine_name);
                    var addresses = DBfunction.Get_Calculate_Readbit_address(names);
                    modeAddressMap[type] = addresses;
                }

                bool isFirstCycle = true; // 第一次循環旗標

                while (token == null || !token.Value.IsCancellationRequested)
                {
                    try
                    {

                        foreach (var kv in modeAddressMap)
                        {
                            int type = kv.Key;
                            var paramList = kv.Value;
                            var prefixes = paramList
                                        .Select(a => a.address)  // 取得每個 Read_address 的第一個字元
                                        .Distinct()                                   // 去除重複
                                        .ToList();

                            var paramLists = Calculate.SplitAddressSections(prefixes);
                            var sectionGroups = paramLists
                               .GroupBy(s => s.Prefix)
                               .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));

                            foreach (var prefix in sectionGroups.Keys)
                            {
                                var blocks = sectionGroups[prefix];
                                bool[] readResults;

                                foreach (var block in blocks)
                                {
                                    string device = prefix + block.Start;
                                    Checkpoint_time.Start("Drill_main");

                                    lock (externalLock ?? new object())
                                    {
                                        readResults = plc.ReadBitDevice(device, 256);
                                    }

                                    List<now_single> result = Calculate.Convert_alarmsingle(readResults, prefix, block.Start);

                                    // 對目前這個區塊內的參數做處理（篩選 paramList 中對應此區塊的）
                                    var relevantParams = paramList
                                        .Where(p => p.address.StartsWith(prefix))
                                        .ToList();
                                    Checkpoint_time.Stop("Drill_main");


                                    foreach (var (name, address) in relevantParams)
                                    {

                                        // 從 result 中找出對應位址的值
                                        var match = result.FirstOrDefault(r => r.address == address);
                                        if (match == null) continue;
                                       

                                        bool newVal = match.current_single;
                                        bool oldVal = lastStates.ContainsKey(name) ? lastStates[name] : false;

                                        if (isFirstCycle && type==2)
                                        {
                                            int now_time = DBfunction.Get_Machine_NowValue(machine_name, name);
                                            int history_time = DBfunction.Get_Machine_History_NumericValue(machine_name, name);
                                            DBfunction.Set_Machine_History_NumericValue(machine_name, name, (ushort)(now_time+ history_time));
                                            DBfunction.Set_Machine_now_number(machine_name, name, 0);

                                            continue;
                                        }
                                        if (isFirstCycle && type == 1)
                                        {
                                            lastStates[name] = newVal;
                                            continue;
                                        }

                                        if (type == 1)
                                        {
                                            if (oldVal != newVal && newVal == true)
                                            {
                                                machine_event?.Invoke(this, new IOUpdateEventArgs
                                                {
                                                    Address = name,
                                                    OldValue = oldVal,
                                                    NewValue = newVal
                                                });

                                                //Debug.WriteLine($"⚠️ 變化：{name} {oldVal} ➜ {newVal}");
                                                int historyVal = DBfunction.Get_Machine_History_NumericValue(name);

                                                DBfunction.Set_Machine_History_NumericValue(machine_name,name, historyVal + 1);

                                            }

                                            lastStates[name] = newVal;
                                        }
                                        else if (type == 2)
                                        {
                                            if (!timer_bit.ContainsKey(name))
                                            {
                                                int historyVal = DBfunction.Get_Machine_History_NumericValue(name);
                                                timer_bit[name] = new MonitorFunction.RuntimebitTimer
                                                {
                                                    HistoryValue = historyVal
                                                };

                                            }

                                            var timer = timer_bit[name];

                                            if (newVal == true)
                                            {
                                                timer.IsCounting = true;

                                                // 實際經過的秒數
                                                TimeSpan elapsed = DateTime.UtcNow - timer.LastUpdateTime;
                                                if (elapsed.TotalSeconds >= 1)
                                                {
                                                    timer.NowValue += (int)elapsed.TotalSeconds;
                                                    timer.LastUpdateTime = DateTime.UtcNow;
                                                    //Debug.WriteLine($"{timer.LastUpdateTime}、{timer.NowValue}");
                                                    ushort now_total = (ushort)(DBfunction.Get_Machine_NowValue(machine_name, name)+ (ushort)elapsed.TotalSeconds);
                                                    DBfunction.Set_Machine_now_number(machine_name, name, now_total);

                                                    //Debug.WriteLine($"⏱ {name} 累加中：{timer.NowValue}");
                                                    
                                                    //Debug.WriteLine($"⏱ {name} 當前歷史資料：{DBfunction.Get_Machine_History_NumericValue(name)}");

                                                }

                                                if (timer.NowValue>= 30)
                                                {

                                                    ushort HistoryValue = (ushort)(DBfunction.Get_Machine_History_NumericValue(machine_name, name) + timer.NowValue);//確定經過的時間為30s
                                                    DBfunction.Set_Machine_History_NumericValue(machine_name,name, HistoryValue);
                                                    timer.NowValue = 0;
                                                    DBfunction.Set_Machine_now_number(machine_name, name, 0);
                                                    //Debug.WriteLine($"📥 {name} 滿 30 秒：累積為 {timer.HistoryValue}");

                                                }

                                            }
                                            else
                                            {
                                                if (timer.IsCounting && timer.NowValue > 0)
                                                {
                                                    DBfunction.Inital_MachineParameters_number(machine_name, name);

                                                    int now_time = DBfunction.Get_Machine_NowValue(machine_name, name);
                                                    int history_time = DBfunction.Get_Machine_History_NumericValue(machine_name, name);
                                                    DBfunction.Set_Machine_History_NumericValue(machine_name, name, (ushort)(now_time + history_time));

                                                    //DBfunction.Set_Machine_History_NumericValue(machine_name, name, (ushort)timer.HistoryValue);
                                                    DBfunction.Set_Machine_now_number(machine_name, name, 0);
                                                    timer.NowValue = 0;
                                                }

                                                timer.IsCounting = false;
                                                timer.LastUpdateTime = (DateTime.UtcNow); // 記錄重置時間

                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"❌ Bit 監控錯誤：{ex.Message}");
                    }
                    isFirstCycle = false; // ✅ 只在第一次後設為 false

                    await Task.Delay(100, token ?? CancellationToken.None); // 輪詢節流
                }
            }

            /// <summary>
            /// 讀取字串格式的變數
            /// </summary>
            /// <param name="viewIndex"></param>
            /// <param name="machineName"></param>
            /// <param name="token"></param>
            /// <returns></returns>
            /// 
            public async Task Read_Word_Monitor_AllModesAsync(string machine_name, int[] ReadTypes, CancellationToken? token = null)
            {
                while (token == null || !token.Value.IsCancellationRequested)
                {
                    // 同時獲取公制和英制的地址映射
                    var modeAddressMap_Metric = new Dictionary<int, List<(string name, string address, ushort address_index)>>();
                    var modeAddressMap_Imperial = new Dictionary<int, List<(string name, string address, ushort address_index)>>();

                    // 獲取當前單位制
                    string currentUnit = UnitManager.CurrentUnit; // 假設這是獲取當前單位的方式

                    foreach (int now_readType in ReadTypes.Distinct())
                    {
                        var names = DBfunction.Get_Machine_read_view(now_readType, machine_name);
                        var addresses = DBfunction.Get_Read_word_machineparameter_address(machine_name, names);
                        // 分別獲取公制和英制的地址
                        var addresses_metric = DBfunction.Get_Read_word_machineparameter_address_WithUnit(machine_name, names, "Metric");
                        var addresses_imperial = DBfunction.Get_Read_word_machineparameter_address_WithUnit(machine_name, names, "Imperial");

                        modeAddressMap_Metric[now_readType] = addresses_metric;
                        modeAddressMap_Imperial[now_readType] = addresses_imperial;

                    }

                    try
                    {
                        // 處理公制數值
                        await ProcessUnitValues(modeAddressMap_Metric, machine_name, "Metric", currentUnit);

                        // 處理英制數值
                        await ProcessUnitValues(modeAddressMap_Imperial, machine_name, "Imperial", currentUnit);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"❌ Word 監控錯誤：{ex.Message}");
                    }

                    await Task.Delay(100, token ?? CancellationToken.None); // 輪詢節流
                }

            }
            /// <summary>
            /// Word讀值方式更新
            /// </summary>
            /// <param name="modeAddressMap"></param>
            /// <param name="machine_name"></param>
            /// <param name="unit"></param>
            /// <param name="currentDisplayUnit"></param>
            /// <returns></returns>
            private async Task ProcessUnitValues(Dictionary<int, List<(string name, string address, ushort address_index)>> modeAddressMap,
                                   string machine_name, string unit, string currentDisplayUnit)
            {
                foreach (var kv in modeAddressMap)
                {
                    int type = kv.Key;
                    var paramList = kv.Value;

                    if (!paramList.Any()) continue; // 如果沒有此單位的參數，跳過

                    var prefixes = paramList
                            .Select(a => new string(a.address.TakeWhile(char.IsLetter).ToArray()))
                            .Distinct()
                            .ToList();

                    var paramLists = Calculate.SplitAddressSections(paramList.Select(p => p.address).ToList());
                    var sectionGroups = paramLists
                                        .GroupBy(s => s.Prefix)
                                        .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));

                    foreach (var prefix in sectionGroups.Keys)
                    {
                        var blocks = sectionGroups[prefix];
                        ushort[] readResults;

                        foreach (var block in blocks)
                        {
                            string device = prefix + block.Start;
                            Checkpoint_time.Start("Saw_main");

                            lock (externalLock ?? new object())
                            {
                                readResults = plc.ReadWordDevice(device, 256);
                            }

                            List<now_number> result = Calculate.Convert_wordsingle(readResults, prefix, block.Start);

                            var relevantParams = paramList
                                .Where(p => p.address.StartsWith(prefix))
                                .ToList();

                            Checkpoint_time.Stop("Saw_main");
                            Checkpoint_time.Start("Saw_brand");

                            foreach (var (name, address, address_index) in relevantParams)
                            {
                                var match = result.FirstOrDefault(r => r.address == address);
                                if (match == null) continue;

                                try
                                {
                                    switch (type)
                                    {
                                        case 0:
                                            if (address_index == 1)
                                            {
                                                ushort val = match.current_number;
                                                double resultVal = val * DBfunction.Get_Unit_transfer(machine_name, name);

                                                // 根據單位制儲存到不同位置
                                                if (unit == "Imperial")
                                                {
                                                    DBfunction.Set_Machine_now_number(machine_name, name, val);
                                                }
                                                else // Metric
                                                {
                                                    DBfunction.Set_Machine_History_NumericValue(machine_name, name, val);
                                                }

                                                // 只有當前顯示單位才更新字串顯示
                                                if (unit == currentDisplayUnit)
                                                {
                                                    DBfunction.Set_Machine_now_string(machine_name, name, resultVal.ToString("F1"));
                                                }
                                            }
                                            else if (address_index == 2)
                                            {
                                                string nextAddress = GenerateNextAddress(address);
                                                var nextMatch = result.FirstOrDefault(r => r.address == nextAddress);
                                                if (nextMatch != null)
                                                {
                                                    ushort[] values = { match.current_number, nextMatch.current_number };
                                                    double merged = MonitorFunction.mergenumber(values) * DBfunction.Get_Unit_transfer(machine_name, name);
                                                    int currentvalue = (int)MonitorFunction.mergenumber(values);

                                                    // 根據單位制儲存到不同位置
                                                    if (unit == "Imperial")
                                                    {
                                                        DBfunction.Set_Machine_now_number(machine_name, name, currentvalue);
                                                    }
                                                    else // Metric
                                                    {
                                                        DBfunction.Set_Machine_History_NumericValue(machine_name, name, currentvalue);
                                                    }

                                                    // 只有當前顯示單位才更新字串顯示
                                                    if (unit == currentDisplayUnit)
                                                    {
                                                        DBfunction.Set_Machine_now_string(machine_name, name, merged.ToString("F1"));
                                                    }
                                                }
                                                else
                                                {
                                                    Debug.WriteLine($"❗ 無法取得 {address} 的第二段：{nextAddress}");
                                                }
                                            }
                                            else
                                            {
                                                Debug.WriteLine($"❌ 無效 Machine：{address_index}");
                                            }
                                            break;

                                        case 1:
                                            if (address_index == 2)
                                            {
                                                string nextAddress = GenerateNextAddress(address);
                                                var nextMatch = result.FirstOrDefault(r => r.address == nextAddress);
                                                if (nextMatch != null)
                                                {
                                                    ushort[] values = { match.current_number, nextMatch.current_number };
                                                    string formatted = MonitorFunction.FormatPlcTime(values);
                                                    int currentvalue = (int)MonitorFunction.mergenumber(values);

                                                    // 根據單位制儲存到不同位置
                                                    if (unit == "Imperial")
                                                    {
                                                        DBfunction.Set_Machine_now_number(machine_name, name, currentvalue);
                                                    }
                                                    else // Metric
                                                    {
                                                        DBfunction.Set_Machine_History_NumericValue(machine_name, name, currentvalue);
                                                    }

                                                    // 只有當前顯示單位才更新字串顯示
                                                    if (unit == currentDisplayUnit)
                                                    {
                                                        DBfunction.Set_Machine_now_string(machine_name, name, formatted);
                                                    }
                                                }
                                                else
                                                {
                                                    Debug.WriteLine($"❗ 時間格式地址錯誤：缺少 {nextAddress}");
                                                }
                                            }
                                            else
                                            {
                                                Debug.WriteLine($"[1] {name} 超出範圍");
                                            }
                                            break;

                                        case 2:
                                            if (address_index == 2)
                                            {
                                                string nextAddress = GenerateNextAddress(address);
                                                var nextMatch = result.FirstOrDefault(r => r.address == nextAddress);
                                                if (nextMatch != null)
                                                {
                                                    ushort[] values = { match.current_number, nextMatch.current_number };
                                                    ushort resultVal = (ushort)(values.Max() - values.Min());

                                                    // 根據單位制儲存到不同位置
                                                    if (unit == "Imperial")
                                                    {
                                                        DBfunction.Set_Machine_now_number(machine_name, name, resultVal);
                                                    }
                                                    else // Metric
                                                    {
                                                        // 如果需要的話可以加上 History_NumericValue 的儲存
                                                        DBfunction.Set_Machine_History_NumericValue(machine_name, name, resultVal);
                                                    }

                                                    // 只有當前顯示單位才更新字串顯示
                                                    if (unit == currentDisplayUnit)
                                                    {
                                                        DBfunction.Set_Machine_now_string(machine_name, name, resultVal.ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    Debug.WriteLine($"❗ 區間計算缺少第二字：{nextAddress}");
                                                }
                                            }
                                            else
                                            {
                                                Debug.WriteLine($"[2] {name} 超出範圍");
                                            }
                                            break;

                                        case 3:
                                            {
                                                string timerKey = $"{name}_{unit}"; // 為不同單位創建不同的 timer key

                                                if (!timer_word.ContainsKey(timerKey))
                                                {
                                                    // 根據單位制決定從哪裡讀取歷史值
                                                    int historyVal;
                                                    if (unit == "Imperial")
                                                    {
                                                        historyVal = DBfunction.Get_Machine_number(machine_name, name);
                                                    }
                                                    else
                                                    {
                                                        historyVal = DBfunction.Get_History_NumericValue(machine_name, name);
                                                    }

                                                    timer_word[timerKey] = new MonitorFunction.RuntimewordTimer
                                                    {
                                                        HistoryValue = historyVal,
                                                        LastUpdateTime = (DateTime.UtcNow),
                                                        AverageBuffer = new List<double>()
                                                    };
                                                }

                                                var timer = timer_word[timerKey];
                                                timer.IsCounting = true;
                                                if (((DateTime.UtcNow) - timer.LastUpdateTime).TotalSeconds >= 1)
                                                {
                                                    timer.LastUpdateTime = (DateTime.UtcNow);
                                                    ushort val = match.current_number;

                                                    // 只有當前顯示單位才更新即時顯示
                                                    if (unit == currentDisplayUnit)
                                                    {
                                                        DBfunction.Set_Machine_now_number(machine_name, name, val);
                                                        DBfunction.Set_Machine_now_string(machine_name, name, val.ToString("F2"));
                                                    }

                                                    timer.AverageBuffer.Add(val);

                                                    if (timer.AverageBuffer.Count >= 10)
                                                    {
                                                        double avg = timer.AverageBuffer.Average();
                                                        timer.HistoryValue = (int)Math.Round(avg);

                                                        // 根據單位制儲存到不同位置
                                                        if (unit == "Imperial")
                                                        {
                                                            DBfunction.Set_Machine_now_number(machine_name, name, timer.HistoryValue);
                                                        }
                                                        else
                                                        {
                                                            DBfunction.Set_Machine_History_NumericValue(machine_name, name, timer.HistoryValue);
                                                        }

                                                        timer.AverageBuffer.Clear();
                                                    }
                                                }
                                                break;
                                            }
                                        case 4:
                                            {
                                                int val = match.current_number;
                                                string input = name switch
                                                {
                                                    "oil_pressure" => MonitorFunction.oil_press_transfer(val),
                                                    "Sawband_brand" => DBfunction.Get_Blade_brand_name(val),
                                                    "Sawblade_material" => DBfunction.Get_Blade_brand_material(val),
                                                    "Sawblade_type" => DBfunction.Get_Blade_brand_type(
                                                        DBfunction.Get_Machine_number("Sawband_brand"),
                                                        DBfunction.Get_Machine_number("Sawblade_material"),
                                                        val),
                                                    "Sawblade_teeth" => DBfunction.Get_Blade_TPI_type(val),
                                                    _ => "未知參數"
                                                };

                                                // Case 4 通常不分單位制
                                                if (unit == currentDisplayUnit)
                                                {
                                                    DBfunction.Set_Machine_now_number(machine_name, name, val);
                                                    DBfunction.Set_Machine_now_string(machine_name, name, input);
                                                    Debug.WriteLine($"[4] {name} = {input}+{val}");
                                                }
                                                break;
                                            }
                                        case 5:
                                            Debug.WriteLine($"[{type}] 尚未實作 {name}");
                                            break;

                                        default:
                                            Debug.WriteLine($"❌ 未支援的讀取類型：{type}");
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"❗ 錯誤處理 {name} ({unit})：{ex.Message}");
                                }
                            }
                            Checkpoint_time.Stop("Saw_brand");
                        }
                    }
                }
            }
            /// <summary>
            /// 計算用電量及功率的函數
            /// </summary>
            /// <param name="machine_name"></param>
            /// <param name="token"></param>
            /// <returns></returns>

            public async Task Read_None_Monitor_AllModesAsync(string machine_name, CancellationToken? token = null)
            {

                var parameterList = DBfunction.Get_None_machineparameter_Name(machine_name);

                while (token == null || !token.Value.IsCancellationRequested)
                {
                    foreach (var (name, type) in parameterList)
                    {
                        if (name == "power")
                        {
                            string now = DateTime.UtcNow.ToString("HH:mm:ss");

                            double voltage = DBfunction.Get_History_NumericValue(machine_name, "voltage");
                            double current = DBfunction.Get_History_NumericValue(machine_name, "current");
                            double cos = Properties.Settings.Default.COSValue;

                            if (voltage == 0 || current == 0)
                            {
                                Debug.WriteLine($"[{now}] ⚠️ 無法計算：電壓或電流為 0");
                                return;
                            }

                            // 計算當前功率
                            double currentPower = (Math.Sqrt(3) * voltage * current * cos) / 1000.0; // 單位 kW
                            // 計算每分鐘用電（度）= kW × 1/60（小時）
                            double currentElectricity = currentPower / 60;

                            //  顯示即時功率（僅顯示功率，不顯示單次電度）
                            DBfunction.Set_Machine_now_string(machine_name, name, currentPower.ToString("F2"));

                            //Debug.WriteLine($"[{now}] [Type3] {machine_name}-{name} 即時功率 = {currentPower:F2} kW");

                            // 讀取目前累積度數（存成 int，單位 0.01 度）
                            int previousSum = DBfunction.Get_History_NumericValue(machine_name, "electricity");
                            int newSum = previousSum + (int)Math.Round(currentElectricity * 100);  // 例如 0.03 度 → 加入 3


                            //  寫入累積電度數
                            DBfunction.Set_Machine_History_NumericValue(machine_name, "electricity", newSum);
                            // 顯示目前總累積（轉回 kWh）
                            double totalElectricity = newSum / 100.0;
                            DBfunction.Set_Machine_now_string(machine_name, "electricity", totalElectricity.ToString("F1"));

                            Debug.WriteLine($"[{now}] 🔋 累積用電：{totalElectricity:F1} kWh");


                        }
                    }
                    await Task.Delay(60000, token ?? CancellationToken.None); // 輪詢節流
                }


            }

        }
        private static string GenerateNextAddress(string address)
        {
            string prefix = new string(address.TakeWhile(char.IsLetter).ToArray());
            string number = new string(address.SkipWhile(char.IsLetter).ToArray());
            return prefix + (int.Parse(number) + 1);
        }

    }
}
