using FX5U_IOMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SLMP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static FX5U_IOMonitor.connect_PLC;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
       
        public class SwingUpdateEventArgs : EventArgs
        {
            public required Swing_Status NewStatus { get; set; }
            public required SawBand_Status NewStatus_band { get; set; }
        }
       

        public class DrillUpdateEventArgs : EventArgs
        {
            public required Drill_status NewStatus_Drill { get; set; }
        }
        public class MachineStatusChangedEventArgs
        {
            public string Name { get; set; }
            public string NewValue { get; set; }
        }
        public class MonitorService
        {

            // 宣告事件：通知程式「某個 IO 資料變了」
            public event EventHandler<IOUpdateEventArgs> IOUpdated; //鋸床實體元件事件

            public event EventHandler<IOUpdateEventArgs> alarm_event;  


            // 宣告事件：通知程式「讀取地址空間資料的位置改變」-鋸床及鋸帶
            public event EventHandler<SwingUpdateEventArgs> SwingStatusUpdated;
            public event EventHandler<DrillUpdateEventArgs> DrillStatusUpdated;
            public event EventHandler<MachineParameterChangedEventArgs>? MachineParameterChanged;


            private SlmpClient plc;



            // 宣告目標 plc 
            public MonitorService(SlmpClient PLC)
            {
                this.plc = PLC;
            }


            ///  <summary>
            /// IO點位更新事件-FX5U硬件
            /// </summary>
            private readonly object readLock = new object();

            public async Task FX5U_MonitoringLoop(CancellationToken token, List<now_single> old_single)
            {
                while (!token.IsCancellationRequested)
                {
                    Sawing_Monitoring(old_single);
                    await Task.Delay(50); // 每 500 毫秒執行一次
                }
            }
            public void Sawing_Monitoring(List<now_single> old_single)
            {
                var Sawing = Calculate.AnalyzeIOSections_8();
                var sectionGroups = Sawing
                    .GroupBy(s => s.Prefix)
                    .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));
                Stopwatch stopwatch = Stopwatch.StartNew();

                lock (readLock)
                {
                    foreach (var prefix in sectionGroups.Keys)
                    {
                        var blocks = sectionGroups[prefix];
                        stopwatch = Stopwatch.StartNew();
                        foreach (var block in blocks)
                        {
                            string device = prefix + block.Start;

                            bool[] plc_result = plc.ReadBitDevice(device, 256);


                            var result = Calculate.ConvertPlcToNowSingle(plc_result, prefix, block.Start);

                            // 更新資料並觸發事件（如果有變化）
                            UpdateIODataBaseFromNowSingle(result, old_single);

                        }
                        stopwatch.Stop();
                    }
                }

                // ✅ 更新耗時資訊
                connect_isOK.Swing_total.read_time = $"讀取時間: {stopwatch.ElapsedMilliseconds} ms";

            }



            private readonly object Drill_ReadLock = new object();
            private readonly object Drill_WriteLock = new object();
            public async Task FX5U_Drill_MonitoringLoop(CancellationToken token, List<now_single> old_single)
            {
                while (!token.IsCancellationRequested)
                {
                    Drill_Monitoring(old_single);
                    await Task.Delay(50); // 每 500 毫秒執行一次
                }
            }
            public void Drill_Monitoring(List<now_single> old_single)
            {
                var Drill = Calculate.Drill_test(); //這邊是八進制，實測時要改成16進制
                var sectionGroups = Drill
                    .GroupBy(s => s.Prefix)
                    .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));

                Stopwatch stopwatch = Stopwatch.StartNew();
                lock (Drill_ReadLock)
                {
                    foreach (var prefix in sectionGroups.Keys)
                    {
                        var blocks = sectionGroups[prefix];

                        foreach (var block in blocks)
                        {
                            // ✅ 格式化位址為八進位補0，例如 X010
                            string device = prefix + block.Start;

                            bool[] plc_result = plc.ReadBitDevice(device, 256);

                            var result = Calculate.ConvertPlcToNowSingle(plc_result, prefix, block.Start);

                            UpdateIODataBaseFromNowSingle(result, old_single);
                        }
                        stopwatch.Stop();

                        // ✅ 顯示第一個切割點起始值
                        var firstBlock = blocks.FirstOrDefault();

                    }

                    // ✅ 更新斷線數與耗時資訊
                    int disconnect = DBfunction.GetTableRowCount("Drill") - connect_isOK.Drill_total.connect;

                    connect_isOK.Drill_total.read_time = $"讀取時間: {stopwatch.ElapsedMilliseconds} ms";
                    Debug.WriteLine($"讀取時間: {stopwatch.ElapsedMilliseconds} ms");
                    Debug.WriteLine($"✅ Drill 監控完成，連線 {connect_isOK.Drill_total.connect} 筆，斷線 {disconnect} 筆");
                }
            }


            private void UpdateIODataBaseFromNowSingle(List<now_single> nowList, List<now_single> oldList)
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
                    }

                }

            }

            /// <summary>
            /// 警告監控設定
            /// </summary>
            /// <param name="token"></param>
            /// <returns></returns>
            private List<now_single> alarmaddress;
            public async Task alarm_MonitoringLoop(CancellationToken token, List<now_single> alarmaddress)
            {
                this.alarmaddress = alarmaddress;
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        alarm_start_monitoring(alarmaddress);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"錯誤：{ex.Message}");
                    }

                    await Task.Delay(100); // 每100ms執行一次
                }
            }
            public void alarm_start_monitoring(List<now_single> old_single)
            {
                var alarm = Calculate.alarm_trans();
                var sectionGroups = alarm
                    .GroupBy(s => s.Prefix)
                    .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));
                Stopwatch stopwatch = Stopwatch.StartNew();

                lock (Drill_ReadLock)
                {
                    foreach (var prefix in sectionGroups.Keys)
                    {
                        var blocks = sectionGroups[prefix];
                        stopwatch = Stopwatch.StartNew();
                        foreach (var block in blocks)
                        {
                            string device = prefix + block.Start;

                            bool[] plc_result = plc.ReadBitDevice(device, 256);


                            var result = Calculate.Convert_alarmsingle(plc_result, prefix, block.Start);

                            // 更新資料並觸發事件（如果有變化）
                            alarm_NowSingle(result, old_single);

                            int updated = Calculate.UpdateIOCurrentSingleToDB(result, "alarm");


                        }
                        stopwatch.Stop();
                    }
                }

                return;
            }

            private void alarm_NowSingle(List<now_single> now_single, List<now_single> old_single)
            {
                if (now_single == null || now_single.Count == 0 || old_single == null || old_single.Count == 0)
                {
                    Console.WriteLine("Error: nowList 或 ioList 為空.");
                }

                int updatedCount = 0;
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
                                updatedCount++;

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






            public async Task machineMonitoringLoop(CancellationToken token)   /// 鋸床及鋸帶暫存器資料狀態更新事件

            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        using var context = new ApplicationDB();
                        var parameters = context.MachineParameters.ToList();

                        foreach (var param in parameters)
                        {
                            string address = DBfunction.Get_Machine_read_address(param.Name);
                            if (string.IsNullOrWhiteSpace(address))
                                continue;
                            lock (Drill_ReadLock)
                            {

                                try
                                {
                                    string newValue = "";

                                    // 特殊處理字串型資料
                                    if (param.Name == "Sawband_brand")
                                    {
                                        ushort[] raw = plc.ReadWordDevice(address, 20);
                                        newValue = Status.ConvertUShortArrayToAsciiString(raw);
                                    }
                                    else if (param.Name == "Sawblade_material")
                                    {
                                        ushort[] raw = plc.ReadWordDevice(address, 10);
                                        newValue = Status.ConvertUShortArrayToAsciiString(raw);
                                    }
                                    else if (param.Name == "Sawblade_teeth")
                                    {
                                        ushort[] raw = plc.ReadWordDevice(address, 2);
                                        newValue = $"{raw[0]} / {raw[1]}";
                                    }
                                    else
                                    {
                                        ushort raw = plc.ReadWordDevice(address);
                                        newValue = raw.ToString(); // 預設為 string 儲存，或改為 int 處理
                                        string history_raw = DBfunction.Get_Machine_number(param.Name).ToString();
                                        DBfunction.Set_Machine_now_string(param.Name, history_raw);
                                    }




                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"❌ 錯誤：{param.Name} 讀取/比對錯誤：{ex.Message}");
                                }
                            }





                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"❌ 監控主迴圈錯誤：{ex.Message}");
                    }

                    await Task.Delay(500); // 每 0.5 秒監控一次，可依需求調整
                }
            }




            /// <summary>
            /// 鋸床及鋸帶暫存器資料狀態更新事件
            /// </summary>

            private DateTime lastWriteTime = DateTime.MinValue;


            public async Task StartSawingMonitoringLoop(CancellationToken token)   /// 鋸床及鋸帶暫存器資料狀態更新事件

            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        if (connect_isOK.swingstatus != null && connect_isOK.sawband != null)
                        {
                            Swing_Status lastSwingStatus = connect_isOK.swingstatus;
                            SawBand_Status lastSawBandStatus = connect_isOK.sawband;
                            lock (Drill_ReadLock)
                            {

                                Swing_Status now_Swing_status = Status.update_swing_Status(plc);
                                SawBand_Status now_SawBand_status = Status.update_SawBand_Status(plc);


                                // 判斷有無變更（可選）
                                if (lastSwingStatus == null || !IsSameStatus(lastSwingStatus, now_Swing_status) || lastSawBandStatus == null || !IsSawbandStatus(lastSawBandStatus, now_SawBand_status))
                                {
                                    lastSwingStatus = now_Swing_status;
                                    lastSawBandStatus = now_SawBand_status;
                                    // 發送事件給主程式或 UI
                                    SwingStatusUpdated?.Invoke(this, new SwingUpdateEventArgs
                                    {
                                        NewStatus = now_Swing_status,
                                        NewStatus_band = now_SawBand_status
                                    });
                                }
                                if ((DateTime.Now - lastWriteTime).TotalSeconds >= 10)
                                {
                                    lock (Drill_WriteLock)
                                    {
                                        Status.Write_Swing_Status(plc);
                                    }

                                    lastWriteTime = DateTime.Now;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Swing 狀態監控錯誤：{ex.Message}");
                    }

                    await Task.Delay(100); // 每100ms執行一次
                }
            }


            
           
         
            public async Task DrillMonitoring(CancellationToken token)
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        if (connect_isOK.drillstatus != null)
                        {
                            Drill_status lastDrillStatus = connect_isOK.drillstatus;
                            lock (Drill_ReadLock)
                            {

                                Drill_status now_Drill_status = Status.update_drill_Status(plc);

                                // 判斷有無變更（可選）
                                if (lastDrillStatus == null || !IsSameStatus_drill(lastDrillStatus, now_Drill_status))
                                {
                                    lastDrillStatus = now_Drill_status;

                                    // 發送事件給主程式或 UI
                                    DrillStatusUpdated?.Invoke(this, new DrillUpdateEventArgs
                                    {
                                        NewStatus_Drill = now_Drill_status,
                                    });
                                }
                                if ((DateTime.Now - lastWriteTime).TotalSeconds >= 10)
                                {
                                    lock (Drill_WriteLock)
                                    {
                                        Status.Write_Drill_Status(plc);
                                    }

                                    lastWriteTime = DateTime.Now;
                                }

                            }
                        }
                    }


                    catch (Exception ex)
                    {
                        Console.WriteLine($"Drill 狀態監控錯誤：{ex.Message}");
                    }

                    await Task.Delay(100); // 每100ms執行一次
                }
            }

            private bool IsSameStatus_drill(Drill_status a, Drill_status b)
            {
                return a.Servo_drives_usetime == b.Servo_drives_usetime &&
                       a.Spindle_usetime == b.Spindle_usetime &&
                       a.PLC_usetime == b.PLC_usetime &&
                       a.Frequency_Converter_usetime == b.Frequency_Converter_usetime &&
                       a.Runtime == b.Runtime &&
                       a.origin == b.origin &&
                       a.loose_tools == b.loose_tools &&
                       a.measurement == b.measurement &&
                       a.Runtime == b.Runtime &&
                       a.Current == b.Current &&
                       a.Voltage == b.Voltage &&
                       a.power == b.power &&
                       a.du == b.du;
            }
            private bool IsSameStatus(Swing_Status a, Swing_Status b)
            {
                return a.motorcurrent == b.motorcurrent &&
                       a.cuttingspeed == b.cuttingspeed &&
                       a.avg_V == b.avg_V &&
                       a.avg_mA == b.avg_mA &&
                       a.oil_pressure == b.oil_pressure &&
                       a.power == b.power &&
                       a.Remaining_Dutting_tools == b.Remaining_Dutting_tools &&
                       a.Sawing_countdown_time == b.Sawing_countdown_time &&
                       a.Runtime == b.Runtime;
            }
            private bool IsSawbandStatus(SawBand_Status a, SawBand_Status b)
            {
                return a.Sawband_brand == b.Sawband_brand &&
                       a.Saw_teeth == b.Saw_teeth &&
                       a.Saw_blade_material == b.Saw_blade_material &&
                       a.Sawband_speed == b.Sawband_speed &&
                       a.saw_motors_usetime == b.saw_motors_usetime &&
                       a.power == b.power &&
                       a.Maximum_current == b.Maximum_current;
                //a.Sawing_countdown_time == b.Sawing_countdown_time &&
                //a.Runtime == b.Runtime;
            }

           
        }


    }

  
}

