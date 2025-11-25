using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Message;
using FX5U_IOMonitor.MitsubishiPlc_Monior;
using FX5U_IOMonitor.Utilization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Net;
using SLMP;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static FX5U_IOMonitor.Check_point;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Models.MonitorFunction;
using static MCProtocol.Mitsubishi;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Org.BouncyCastle.Math.EC.ECCurve;
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
            public int? AdditionalValue { get; set; } // 新增：對應的字串
            public string? AdditionalAddress { get; set; } // 新增：對應的地址
            public string? AlarmType { get; set; } // Frequency / Control / ServoDrive
        }

        public class MonitorService
        {

            // 宣告事件：通知程式「某個 IO 資料變了」
            public event EventHandler<IOUpdateEventArgs> IOUpdated; //實體元件事件(X輸入及Y輸出)
            public event EventHandler<IOUpdateEventArgs> alarm_event; //警告事件事件
            public event EventHandler<IOUpdateEventArgs> machine_event; //寫入元件歷史紀錄
            public event EventHandler<RULThresholdCrossedEventArgs>? RULThresholdCrossed; 

            private readonly Dictionary<string, string> _lastRULState = new();// 紀錄每個元件上次 Message 燈號狀態（green/yellow/red）
            private readonly AlarmMappingConfig _alarmconfig;
            private Dictionary<string, RULThresholdInfo> _rulCache = new();


            public string MachineName { get; }

            public event EventHandler<MachineParameterChangedEventArgs>? MachineParameterChanged;


            private IPlcClient plc;
            private object? externalLock;
            private bool isFirstRead = true; // 實體元件監控是否初始化
            private bool alarmFirstRead = true;
            private readonly object _ioLock = new();
            private object _lockRef;

            private int _failureCount = 0;      // 累計失敗次數
            private int _triggerGate = 0;       // 0=未觸發；1=已觸發（防止重複）
            private DateTime _firstFailureTime = DateTime.MinValue;
            private List<UtilizationShiftConfig> _enabledShifts;

            /// <summary>
            /// 任一來源報告「一次失敗」時呼叫（可被多個 Timer/執行緒呼叫）
            /// </summary>
            public void ReportOneFailure(string? reason = null)
            {
                int current = Interlocked.Increment(ref _failureCount);

                if (!string.IsNullOrWhiteSpace(reason))
                    DisconnectEvents.RaiseCommunicationFailureOnce(MachineName, reason);

                if (current >= 3 && Interlocked.Exchange(ref _triggerGate, 1) == 0)
                {
                    DisconnectEvents.RaiseFailureConnect(MachineName);
                }
            }

            /// <summary>
            /// 成功重連後重置門檻（由視窗B或外部呼叫）
            /// </summary>
            public void ResetFailureGate()
            {
                Interlocked.Exchange(ref _failureCount, 0);
                Interlocked.Exchange(ref _triggerGate, 0);
            }
            public void SetExternalLock(object? locker)
            {
                _lockRef = locker ?? _ioLock;
            }


            // 宣告目標 plc 
            public MonitorService(IPlcClient PLC, string machineName)
            {
                this.plc = PLC;
                this.MachineName = machineName;
                this.isFirstRead = true; 
                this.alarmFirstRead = true;
                _lockRef = _ioLock;   // 先用保底鎖
                var shiftsFile = UtilizationConfigLoader.LoadShifts();
                _enabledShifts = shiftsFile.Shifts
                    .Where(s => s.Enabled)
                    .ToList();

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

                    Monitoring(machinname); // 你的讀取流程          
                    await Task.Delay(500, token);
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
                format = "oct";
                var Drill = Calculate.AnalyzeIOSections(machinname, format);
                string mcType = DBfunction.GetMachineType(machinname); // 判斷使用哪個

                var sectionGroups = Drill
                    .GroupBy(s => s.Prefix)
                    .ToDictionary(g => g.Key,
                    g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First(), mcType));

                Stopwatch stopwatch = Stopwatch.StartNew();
                
                _rulCache = RULNotifier.GetRULMapByMachine(machinname);
                foreach (var prefix in sectionGroups.Keys)
                {
                    var blocks = sectionGroups[prefix];

                    foreach (var block in blocks)
                    {
                        string device = prefix + block.Start;
                        bool[] plc_result;
                        try
                        {
                            lock (_lockRef)
                            {
                                //bool[] plc_result = plc.ReadBitDevice(device, (ushort)block.Range);
                                plc_result = plc.ReadBits(device, (ushort)block.Range);
                            }

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


            private AlarmMappingConfig alarmMapping;
            /// <summary>
            /// 警告監控輪詢與延遲
            /// </summary>
            /// <param name="token"></param>
            /// <returns></returns>
            public async Task alarm_MonitoringLoop(CancellationToken token, string machine, AlarmMappingConfig config)
            {
                alarmMapping = config;
                while (!token.IsCancellationRequested)
                {
                    Alarm_Monitoring(machine, alarmMapping);
                    await Task.Delay(50); // 每 500 毫秒執行一次
                }
            }
            /// <summary>
            /// 負責 PLC 警告讀取 + 轉換
            /// </summary>
            /// <param name="old_single"></param>
            public void Alarm_Monitoring(string machine, AlarmMappingConfig config)
            {
                List<now_single> old_single = DBfunction.Get_alarm_current_single_all(machine);

                var alarm = Calculate.Alarm_trans(machine);

                var sectionGroups = alarm
                    .GroupBy(s => s.Prefix)
                    .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));
                foreach (var prefix in sectionGroups.Keys)
                {
                    var blocks = sectionGroups[prefix];

                    foreach (var block in blocks)
                    {
                        string device = prefix + block.Start;
                        bool[] plc_result;
                        try
                        {
                            lock (_lockRef)
                            {
                                //bool[] plc_result = plc.ReadBitDevice(device, 256);
                                plc_result = plc.ReadBits(device, 256); //256為SLMP_一次讀取的bit的最大值
                            }
                            var result = Calculate.Convert_Single(plc_result, prefix, block.Start);
                            if (isFirstRead)
                            {
                                Calculate.UpdatealarmCurrentSingleToDB(result, machine);
                            }
                            else
                            {

                                alarm_NowSingle(result, old_single, machine, config);
                            }

                        }
                        catch
                        {
                            alarmFirstRead = true; // 斷線時設定下次重新初始化
                            return; // 中止此次讀取

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
            private void alarm_NowSingle(List<now_single> now_single, List<now_single> old_single,string machine, AlarmMappingConfig config)
            {
                if (now_single == null || now_single.Count == 0 || old_single == null || old_single.Count == 0)
                {
                    Console.WriteLine("Error: nowList 或 ioList 為空.");
                }

                foreach (var now in now_single)
                {
                    var ioMatch = old_single.FirstOrDefault(io => io.address == now.address && io.machine == machine);
                    if (ioMatch != null)
                    {

                        if (ioMatch.current_single is bool oldVal)
                        {
                            bool newVal = now.current_single;

                            if (oldVal != newVal)
                            {
                                ioMatch.current_single = newVal;
                                // 檢查是否需要讀取對應的數值
                                int? additionalValue = null;
                                string additionalAddress = null;

                                if (config.AlarmLookMapping.TryGetValue(now.address, out var mapping))
                                {
                                    ushort[] result;
                                    lock (_lockRef)
                                    {
                                        result = plc.ReadWords(mapping.ReadAddress, 1);
                                        additionalValue = result[0];
                                    }
                                    // 讀取對應的數值
                                    alarm_event?.Invoke(this, new IOUpdateEventArgs
                                    {
                                        Address = now.address,
                                        OldValue = oldVal,
                                        NewValue = newVal,
                                        AdditionalAddress = additionalAddress,
                                        AdditionalValue = additionalValue,
                                        AlarmType = mapping.Type
                                    });
                                }
                                else
                                {
                                    alarm_event?.Invoke(this, new IOUpdateEventArgs
                                    {
                                        Address = now.address,
                                        OldValue = oldVal,
                                        NewValue = newVal,
                                        AdditionalAddress = additionalAddress,
                                        AdditionalValue = additionalValue,
                                    });
                                }
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

                                    lock (_lockRef)
                                    {
                                        //readResults = plc.ReadWordDevice(device, 256);
                                        readResults = plc.ReadWords(device, 256);
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
                                                lock (_lockRef)
                                                {
                                                    // plc.WriteWordDevice(match.address, value);
                                                    plc.WriteWord(match.address, value);

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

                                                ushort[] write2plc = MonitorFunction.SmartWordSplit(value.Value, 2, 1);

                                                lock (_lockRef)
                                                {
                                                    //plc.WriteWordDevice(match.address, write2plc);
                                                    plc.WriteWords(match.address, write2plc);

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
                        //ReportOneFailure();
                    }
                    await Task.Delay(500, token ?? CancellationToken.None); // 輪詢節流

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
                            string mcType = DBfunction.GetMachineType(machine_name); 

                            var sectionGroups = paramLists
                                .GroupBy(s => s.Prefix)
                                .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First(), mcType));

                            foreach (var prefix in sectionGroups.Keys)
                            {
                                var blocks = sectionGroups[prefix];
                                bool[] readResults;

                                foreach (var block in blocks)
                                {
                                    string device = prefix + block.Start;
                                    Checkpoint_time.Start("Drill_main");

                                    lock (_lockRef)
                                    {
                                        //readResults = plc.ReadBitDevice(device, (ushort)block.Range);
                                        readResults = plc.ReadBits(device, (ushort)block.Range);

                                    }

                                    List<now_single> result = Calculate.Convert_Single(readResults, prefix, block.Start);

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
                                            DBfunction.Set_Machine_History_NumericValue(machine_name, name, now_time+ history_time);
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
                                                int historyVal = DBfunction.Get_Machine_History_NumericValue(machine_name, name);

                                                DBfunction.Set_Machine_History_NumericValue(machine_name,name, historyVal + 1);

                                            }

                                            lastStates[name] = newVal;
                                        }
                                        else if (type == 2)
                                        {
                                            if (!timer_bit.ContainsKey(name))
                                            {
                                                int historyVal = DBfunction.Get_Machine_History_NumericValue(machine_name, name);
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
                                                    int now_total = (int)(DBfunction.Get_Machine_NowValue(machine_name, name)+ (int)elapsed.TotalSeconds);
                                                    DBfunction.Set_Machine_now_number(machine_name, name, now_total);
                                                }

                                                if (timer.NowValue>= 30)
                                                {

                                                    int HistoryValue = (int)(DBfunction.Get_Machine_History_NumericValue(machine_name, name) + timer.NowValue);//確定經過的時間為30s
                                                    DBfunction.Set_Machine_History_NumericValue(machine_name,name, HistoryValue);
                                                    timer.NowValue = 0;
                                                    DBfunction.Set_Machine_now_number(machine_name, name, 0);

                                                }

                                            }
                                            else
                                            {
                                                if (timer.IsCounting && timer.NowValue > 0)
                                                {
                                                    DBfunction.Inital_MachineParameters_number(machine_name, name);

                                                    int now_time = DBfunction.Get_Machine_NowValue(machine_name, name);
                                                    int history_time = DBfunction.Get_Machine_History_NumericValue(machine_name, name);
                                                    DBfunction.Set_Machine_History_NumericValue(machine_name, name, (now_time + history_time));
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
                        //ReportOneFailure();
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
                        //ReportOneFailure();
                    }

                    await Task.Delay(500, token ?? CancellationToken.None); // 輪詢節流
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

                            lock (_lockRef )
                            {
                                //readResults = plc.ReadWordDevice(device, 256);
                                readResults = plc.ReadWords(device, 256);
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
                                                    //Debug.WriteLine($"[4] {name} = {input}+{val}");
                                                }
                                                break;
                                            }
                                        case 5:
                                            if (address_index == 2)
                                            {
                                                string nextAddress = GenerateNextAddress(address);
                                                var nextMatch = result.FirstOrDefault(r => r.address == nextAddress);
                                                if (nextMatch != null)
                                                {
                                                    ushort[] values = { match.current_number, nextMatch.current_number };
                                                    int currentvalue = (int)MonitorFunction.mergenumber(values);
                                                    int currentrecordvalue = int.Parse(DBfunction.Get_Machine_now_string(machine_name, name).Trim());

                                                    if (currentvalue >= currentrecordvalue)
                                                    {
                                                        DBfunction.Set_Machine_now_number(machine_name, name, currentvalue); // 當前該鋸帶壽命的紀錄值
                                                        DBfunction.Set_Machine_now_string(machine_name, name, currentvalue.ToString()); //當前該鋸帶壽命的監控值

                                                    }
                                                    if ( currentvalue < currentrecordvalue ) 
                                                    {
                                                        int History_NumericValue = DBfunction.Get_Machine_History_NumericValue(machine_name, name);
                                                        int accumulationvalue = History_NumericValue + currentrecordvalue; 
                                                        DBfunction.Set_Machine_History_NumericValue(machine_name, name, accumulationvalue);  //現在的總壽命值
                                                        DBfunction.Set_Machine_now_string(machine_name, name, "0"); //當前該鋸帶壽命的監控值

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



            private readonly HashSet<string> _processedShiftPoints = new HashSet<string>();

            /// <summary>
            /// 稼動率狀態監控
            /// </summary>
            /// <param name="address"></param>
            /// <param name="token"></param>
            /// <returns></returns>
            public async Task Read_Utilization(string address, CancellationToken? token = null)
            {

                int? lastStatus = null;
                DateTime lastStartUtc;
                long? lastRecordId = null;
                DateTime lastWriteTimeUtc = DateTime.UtcNow;  // 記錄上次更新時間

                // 先從 DB 讀最後一筆（若有），只用來初始化，不要在 while 內每圈查
                var lastRecord = DBfunction.GetLastRecord(MachineName, address);
                if (lastRecord != null)
                {
                    lastStatus = lastRecord.Status;
                    // 保持一致用 UTC
                    lastStartUtc = lastRecord.EndTime.Kind == DateTimeKind.Utc
                        ? lastRecord.EndTime
                        : lastRecord.EndTime.ToUniversalTime();

                    lastRecordId = lastRecord.Id;
                }
                else
                {
                    // 第一次啟動：讀當下狀態，但「不要」寫任何 0 秒紀錄
                    int initStatus;
                    lock (_lockRef)
                    {
                        var read = plc.ReadBits(address, 1);
                        initStatus = read[0] ? 1 : 0;
                    }
                    lastStatus = initStatus;
                    lastStartUtc = DateTime.UtcNow;
                }

                while (token == null || !token.Value.IsCancellationRequested)
                {
                    int currentStatus;
                    lock (_lockRef)
                    {
                        var read = plc.ReadBits(address, 1);
                        currentStatus = read[0] ? 1 : 0;
                    }
                    var nowUtc = DateTime.UtcNow;

                    if (currentStatus != lastStatus.Value)
                    {
                        nowUtc = DateTime.UtcNow;
                        _ = DBfunction.SaveStatusRecordAsync(
                            MachineName,
                            address,
                            lastStatus.Value,
                            lastStartUtc,
                            nowUtc
                        );

                        // 更新當前段的起點
                        lastStatus = currentStatus;
                        lastStartUtc = nowUtc;
                        lastRecordId = null;
                        lastWriteTimeUtc = nowUtc;
                    }
                    else
                    {
                        // 狀態沒變 → 檢查是否需要建立新紀錄或更新現有紀錄
                        if (!lastRecordId.HasValue)
                        {
                            // 第一次進入持續段 → 新建紀錄
                            lastRecordId = await DBfunction.InsertNewStatusAsync(
                                MachineName, address, currentStatus, lastStartUtc);
                            lastWriteTimeUtc = nowUtc;
                        }
                        else if ((nowUtc - lastWriteTimeUtc).TotalSeconds >= 60)
                        {
                            // ✅ 每分鐘更新 EndTime
                            await DBfunction.UpdateEndTimeAsync(lastRecordId.Value, nowUtc);
                            lastWriteTimeUtc = nowUtc;
                        }
                    }


                    // === 判斷班別開頭/結尾 ===
                    var now = DateTime.UtcNow;
                    foreach (var shift in _enabledShifts.Where(s => s.Enabled))
                    {
                        var (startUtc, endUtc) = ResolveShiftRange(shift);

                        // 生成唯一 key，避免重複補
                        var startKey = $"{shift.ShiftNo}:{startUtc:O}:start";
                        var endKey = $"{shift.ShiftNo}:{endUtc:O}:end";

                        // ① 班別開頭：若已跨過 startUtc，且上一段起點在 startUtc 之前，且沒補過，就補一筆到 startUtc
                        if (now >= startUtc && lastStartUtc < startUtc && _processedShiftPoints.Add(startKey))
                        {
                            _ = DBfunction.SaveStatusRecordAsync(
                                MachineName, address,
                                lastStatus!.Value,          // 補「目前持續的上一段狀態」
                                lastStartUtc, startUtc
                            );
                            lastStartUtc = startUtc;       // 下一段從班別起點開始
                                                           // Console.WriteLine($"Start fixed: Shift {shift.ShiftNo} {startUtc:O}");
                        }

                        // ② 班別結尾：若已跨過 endUtc，且上一段起點在 endUtc 之前，且沒補過，就補一筆到 endUtc
                        if (now >= endUtc && lastStartUtc < endUtc && _processedShiftPoints.Add(endKey))
                        {
                            _ = DBfunction.SaveStatusRecordAsync(
                                MachineName, address,
                                lastStatus!.Value,
                                lastStartUtc, endUtc
                            );
                            lastStartUtc = endUtc;         // 下一段從班別終點開始
                                                           // Console.WriteLine($"End fixed: Shift {shift.ShiftNo} {endUtc:O}");
                        }
                    }
                    await Task.Delay(100, token ?? CancellationToken.None);
                }

                // 程式被取消/結束時，把最後一段補上
                if (lastStatus.HasValue)
                {
                    
                    await DBfunction.SaveStatusRecordAsync(MachineName, address, lastStatus.Value, lastStartUtc, DateTime.UtcNow);
                }


            }

        }
        private static (DateTime shiftStart, DateTime shiftEnd) ResolveShiftRange(UtilizationShiftConfig shift)
        {
            var today = DateTime.Today;
            var start = today + TimeSpan.Parse(shift.Start);
            var end = today + TimeSpan.Parse(shift.End);

            if (end <= start)
                end = end.AddDays(1); // 跨日補一天

            return (start, end);
        }
        private static string GenerateNextAddress(string address)
        {
            string prefix = new string(address.TakeWhile(char.IsLetter).ToArray());
            string number = new string(address.SkipWhile(char.IsLetter).ToArray());
            return prefix + (int.Parse(number) + 1);
        }
      


    }
}
