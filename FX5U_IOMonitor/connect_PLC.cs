using static FX5U_IOMonitor.Main;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using SLMP;
using static FX5U_IOMonitor.Models.MonitoringService;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Threading;

namespace FX5U_IOMonitor
{
    public partial class connect_PLC : Form
    {
        SlmpClient Drill_PLC_Device;
        SlmpClient Swing_PLC_Device;

        public class connect_isOK //提供各介面共用的X_IO監控數據
        {
            public static connect_Summary Drill_total = new connect_Summary();
            public static connect_Summary Swing_total = new connect_Summary();

            public static bool Drill_connect = false;
            public static bool Swing_connect = false;

            public static Swing_Status swingstatus = new Swing_Status();
            public static Drill_status drillstatus = new Drill_status();
            public static SawBand_Status sawband = new SawBand_Status();

        }


        public class Connect_Rs485
        {
            public string PortName { get; set; }
            public int BaudRate { get; set; }

        }

        public connect_PLC(Main main)
        {

            InitializeComponent();
            control_choose.SelectedIndex = 0;
            connect_choose.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;

        }

        private void connect_choose_SelectedIndexChanged(object sender, EventArgs e)
        {
            connect_choose.SelectedIndexChanged += connect_choose_SelectedIndexChanged;

            if (connect_choose.SelectedIndex == 0)
            {
                panel_Ethernet.Visible = true;
                panel_RS485.Visible = false;
                button_FILE.Visible = false;
                button2.Visible = false;

            }
            if (connect_choose.SelectedIndex == 1)
            {
                panel_RS485.Visible = true;
                panel_Ethernet.Visible = false;
                button_FILE.Visible = false;
                button2.Visible = false;

            }
            if (connect_choose.SelectedIndex == 2)
            {
                panel_RS485.Visible = true;
                panel_Ethernet.Visible = false;
                button_FILE.Visible = false;
                button2.Visible = false;



            }
            if (connect_choose.SelectedIndex == 2 && control_choose.SelectedIndex == 2)
            {
                button_FILE.Visible = true;
                button2.Visible = true;

            }

        }
        private SlmpClient? SLMP_connect(string IP, int port)
        {
            SlmpConfig cfg = new(IP, port);
            cfg.ConnTimeout = 3000;
            SlmpClient _plc = new(cfg);
            try
            {
                _plc.Connect();
                return _plc;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        // 確認連線機制
        private CancellationTokenSource? Sawing_CancelToken;
        private Task? Sawing_open_task;
        private CancellationTokenSource? Drill_CancelToken;
        private Task? Drill_IO_task;
        private Task? Drill_status_task;
        private Task? swing_lab_task;
        private Task? Alarm_task;

        private void btn_connect_ethernet_Click(object sender, EventArgs e)
        {

            if (control_choose.SelectedIndex == 0)
            {
                if (connect_isOK.Drill_connect == true)
                {
                    return;
                }
                Drill_PLC_Device = SLMP_connect(txb_IP.Text.ToString(), int.Parse(txb_port.Text.ToString()));

                if (Drill_PLC_Device != null)
                {
                    connect_isOK.Drill_connect = true;

                    if (connect_isOK.Drill_connect == true)
                    {
                        // 初始化資料庫內的信號值
                        InitializeDrillMonitoring(Drill_PLC_Device); //元件當前狀態更新

                        // 初始化機械當前警告值
                        Initialize_Aalarm(Drill_PLC_Device); //警告信息


                        // 初始化機械當前連線數值
                        Initialize_machine_prameter(Drill_PLC_Device);
                        //connect_isOK.drillstatus = Status.update_drill_Status(Drill_PLC_Device);   //10種鑽床狀態更新
                        //connect_isOK.swingstatus = Status.update_swing_Status(Drill_PLC_Device); //10種鋸床狀態更新
                        //connect_isOK.sawband = Status.update_SawBand_Status(Drill_PLC_Device);  //10種鋸帶狀態更新


                        //// 定義監控 monitor 
                        MonitorHub.AddMonitor("Drill", Drill_PLC_Device);

                        var Drilll_monitor = MonitorHub.GetMonitor("Drill");
                        if (Drilll_monitor != null)
                        {
                            Drill_CancelToken = new CancellationTokenSource();
                            Drill_IO_task = Task.Run(() => Drilll_monitor.FX5U_Drill_MonitoringLoop(Drill_CancelToken.Token, DBfunction.Get_Drill_current_single_all()));
                            Alarm_task = Task.Run(() => Drilll_monitor.alarm_MonitoringLoop(Drill_CancelToken.Token, DBfunction.Get_alarm_current_single_all()));

                            Drill_status_task = Task.Run(() => Drilll_monitor.DrillMonitoring(Drill_CancelToken.Token));
                            swing_lab_task = Task.Run(() => Drilll_monitor.StartSawingMonitoringLoop(Drill_CancelToken.Token));


                            var DB_update = MonitorHub.GetMonitor("Drill");
                            DB_update.IOUpdated += DB_update_change;


                        }
                        else
                        {
                            MessageBox.Show("找不到名稱為 'Drill' 的監控器！");
                        }


                    }
                    else
                    {
                        connect_isOK.Swing_connect = false;
                        MessageBox.Show($"連線失敗，請檢查硬體IP及位置後重新連線");

                    }
                }

            }
       
            if (control_choose.SelectedIndex == 1)
            {
                if (connect_isOK.Swing_connect == true)
                {
                    return;
                }
                Swing_PLC_Device = SLMP_connect(txb_IP.Text.ToString(), int.Parse(txb_port.Text.ToString()));
                if (Swing_PLC_Device != null)
                {
                    connect_isOK.Swing_connect = true;


                    if (connect_isOK.Swing_connect == true)
                    {


                        // 初始化資料庫內的信號值
                        InitializeSawingMonitoring(Swing_PLC_Device);

                        // 定義監控 monitor 
                        MonitorHub.AddMonitor("Sawing", Swing_PLC_Device);


                        var monitor = MonitorHub.GetMonitor("Sawing");
                        if (monitor != null)
                        {
                            Sawing_CancelToken = new CancellationTokenSource();
                            Sawing_open_task = Task.Run(() => monitor.FX5U_MonitoringLoop(Sawing_CancelToken.Token, DBfunction.Get_Sawing_current_single_all()));
                            var DB_update1 = MonitorHub.GetMonitor("Sawing");
                            DB_update1.IOUpdated += DB_update_change;

                        }
                        else
                        {
                            MessageBox.Show("找不到名稱為 'Swing' 的監控器！");
                        }

                    }
                    else
                    {
                        connect_isOK.Swing_connect = false;
                        MessageBox.Show($"連線失敗，請檢查硬體IP及位置後重新連線");

                    }
                }
            }

        }
       
        private void DB_update_change(object? sender, IOUpdateEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => DB_update_change(sender, e)));
                return;
            }

            // 這裡才安全顯示 UI（主執行緒）
            //MessageBox.Show($"📡 偵測到 I/O 變化：{e.Address} from {e.OldValue} ➜ {e.NewValue}");

            try
            {
                string? datatable = DBfunction.FindTableWithAddress(e.Address);
                if (string.IsNullOrWhiteSpace(datatable))
                    return;

                int number = DBfunction.Get_use_ByAddress(datatable, e.Address);
                if (number < 0)
                {
                    DBfunction.Set_use_ByAddress(datatable, e.Address, 0);
                    return;
                }

                DBfunction.Set_use_ByAddress(datatable, e.Address, number + 1);
                DBfunction.Set_current_single_ByAddress(datatable, e.Address, e.NewValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Monitor_DBuse_Updated 發生例外：{ex.Message}");
            }

        }

        private void btn_disconnect_ethernet_Click(object sender, EventArgs e)
        {
            if (control_choose.SelectedIndex == 0)
            {

                if (connect_isOK.Drill_connect == true)
                {
                    connect_isOK.Drill_connect = false;
                    // 停止監控任務
                    Drill_CancelToken?.Cancel();
                    Drill_IO_task?.Wait();              // 等待任務結束
                    Drill_status_task?.Wait();
                    swing_lab_task?.Wait();
                    Alarm_task?.Wait();
                    Drill_CancelToken?.Dispose();
                    Drill_CancelToken = null;
                    Sawing_open_task = null;
                    // 關閉 PLC
                    Drill_PLC_Device.Disconnect();
                    connect_isOK.Drill_total.connect = 0;
                    //移除監控器
                    MonitorHub.RemoveMonitor("Drill");

                    //UpdateData.ResetCurrentSingle(DataStore.Drill_DataList);
                    return;
                }
                else
                {
                    return;
                }

            }
            if (control_choose.SelectedIndex == 1)
            {
                if (connect_isOK.Swing_connect == true)
                {
                    connect_isOK.Swing_connect = false;

                    // 停止監控任務
                    Sawing_CancelToken?.Cancel();
                    Sawing_open_task?.Wait();              // 等待任務結束
                    Sawing_CancelToken?.Dispose();
                    Sawing_CancelToken = null;
                    Sawing_open_task = null;

                    // 關閉 PLC
                    Swing_PLC_Device.Disconnect();
                    connect_isOK.Swing_total.connect = 0;

                    // 移除監控器
                    MonitorHub.RemoveMonitor("Swing");

                    // 清空畫面資料
                    //UpdateData.ResetCurrentSingle(DataStore.Swing_DataList);
                    return;
                }



            }

        }

        private void btn_connect_RS485_Click(object sender, EventArgs e)
        {
            if (control_choose.SelectedIndex == 0)
            {


            }
            if (control_choose.SelectedIndex == 1)
            {



            }

            //try
            //{
            //    // 1. 建立 Modbus TCP 客戶端，指定伺服器 IP 和埠號
            //    ModbusClient modbusClient = new ModbusClient("192.168.9.136", 502); // 替換為伺服器的 IP 和埠
            //    modbusClient.Connect(); // 連接到 Modbus TCP 伺服器

            //    // 判斷是否連接成功
            //    if (modbusClient.Connected)
            //    {
            //        Console.WriteLine("成功連接到 Modbus TCP 伺服器！");

            //        // 2. 讀取保持暫存器（功能碼 0x03）
            //        int startAddress = 0;       // 起始暫存器地址
            //        int numberOfRegisters = 5; // 要讀取的暫存器數量
            //        int[] holdingRegisters = modbusClient.ReadHoldingRegisters(startAddress, numberOfRegisters);

            //        // 3. 顯示讀取到的暫存器數值
            //        Console.WriteLine("讀取保持暫存器的數值：");
            //        for (int i = 0; i < holdingRegisters.Length; i++)
            //        {
            //            Console.WriteLine($"Register {startAddress + i}: {holdingRegisters[i]}");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("無法連接到 Modbus TCP 伺服器，請檢查連線設定！");
            //    }

            //    // 4. 斷開連接
            //    modbusClient.Disconnect();
            //    Console.WriteLine("已斷開與 Modbus TCP 伺服器的連接。");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"錯誤：{ex.Message}");
            //}

        }


        private void control_choose_SelectedIndexChanged(object sender, EventArgs e)
        {
            control_choose.SelectedIndexChanged += control_choose_SelectedIndexChanged;

            if (control_choose.SelectedIndex == 0)
            {
                panel1.Visible = true;
            }
            if (control_choose.SelectedIndex == 1)
            {
                panel1.Visible = true;
            }
            if (control_choose.SelectedIndex == 2)
            {
                panel1.Visible = false;
            }
            if (control_choose.SelectedIndex == 2 && connect_choose.SelectedIndex == 2)
            {
                panel1.Visible = false;
                button_FILE.Visible = true;
                button2.Visible = true;

            }

        }

        private void button_FILE_Click(object sender, EventArgs e)
        {
            //WriteFile();
        }





        private void InitializeSawingMonitoring(SlmpClient plc)
        {

            // 載入區段資訊後依照區段(X/Y)分組，並同時初始化所有區塊資料
            var Sawing = Calculate.AnalyzeIOSections_8();
            var sectionGroups = Sawing
                .GroupBy(s => s.Prefix)
                .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));
            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (var prefix in sectionGroups.Keys)
            {
                var blocks = sectionGroups[prefix];
                stopwatch = Stopwatch.StartNew();
                foreach (var block in blocks)
                {
                    // ✅ 格式化位址為八進位補0，例如 X010
                    string device = prefix + block.Start;

                    bool[] plc_result = plc.ReadBitDevice(device, 256);


                    var result = Calculate.ConvertPlcToNowSingle(plc_result, prefix, block.Start);

                    int updated = Calculate.UpdateIOCurrentSingleToDB(result, "Sawing");

                    connect_isOK.Swing_total.connect += updated;
                }

                // ✅ 顯示第一個切割點起始值
                var firstBlock = blocks.FirstOrDefault();
                stopwatch.Stop();
            }

            // ✅ 更新斷線數與耗時資訊
            int disconnect = DBfunction.GetTableRowCount("Sawing") - connect_isOK.Swing_total.connect;
            connect_isOK.Swing_total.disconnect = disconnect;
            connect_isOK.Swing_total.read_time = $"讀取時間: {stopwatch.ElapsedMilliseconds} ms";
            Debug.WriteLine($"讀取時間: {stopwatch.ElapsedMilliseconds} ms");
            Debug.WriteLine($"✅ Sawing 監控完成，連線 {connect_isOK.Swing_total.connect} 筆，斷線 {disconnect} 筆");

        }
        private void InitializeDrillMonitoring(SlmpClient plc)
        {

            // 載入區段資訊後依照區段(X/Y)分組，並同時初始化所有區塊資料
            var Drill = Calculate.Drill_test(); //這邊是八進制，實測時要改成16進制
            var sectionGroups = Drill
                .GroupBy(s => s.Prefix)
                .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));

            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (var prefix in sectionGroups.Keys)
            {
                var blocks = sectionGroups[prefix];
               
                foreach (var block in blocks)
                {
                    // ✅ 格式化位址為八進位補0，例如 X010
                    string device = prefix + block.Start;

                    bool[] plc_result = plc.ReadBitDevice(device, 256);

                    var result = Calculate.ConvertPlcToNowSingle(plc_result, prefix, block.Start);

                    int updated = Calculate.UpdateIOCurrentSingleToDB(result, "Drill");

                    connect_isOK.Drill_total.connect += updated;
                }
                stopwatch.Stop();

                // ✅ 顯示第一個切割點起始值
                var firstBlock = blocks.FirstOrDefault();
            }

            // ✅ 更新斷線數與耗時資訊
            int disconnect = DBfunction.GetTableRowCount("Drill") - connect_isOK.Drill_total.connect;
            connect_isOK.Drill_total.disconnect = disconnect;
            connect_isOK.Drill_total.read_time = $"讀取時間: {stopwatch.ElapsedMilliseconds} ms";
            Debug.WriteLine($"讀取時間: {stopwatch.ElapsedMilliseconds} ms");
            Debug.WriteLine($"✅ Drill 監控完成，連線 {connect_isOK.Drill_total.connect} 筆，斷線 {disconnect} 筆");

        }


        private void Initialize_Aalarm(SlmpClient plcdevice)
        {
            var alarm = Calculate.alarm_trans();
            var sectionGroups = alarm
                .GroupBy(s => s.Prefix)
                .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));
         
            foreach (var prefix in sectionGroups.Keys)
            {
                var blocks = sectionGroups[prefix];

                foreach (var block in blocks)
                {
                    // ✅ 格式化位址為八進位補0，例如 X010
                    string device = prefix + block.Start;

                    bool[] plc_result = plcdevice.ReadBitDevice(device, 256);

                    var result = Calculate.Convert_alarmsingle(plc_result, prefix, block.Start);

                    int updated = Calculate.UpdateIOCurrentSingleToDB(result, "alarm");

                }

                // ✅ 顯示第一個切割點起始值
                var firstBlock = blocks.FirstOrDefault();
            }
        }

        private void Initialize_machine_prameter(SlmpClient plc)
        {
            //初始化machine參數
            using (var context = new ApplicationDB())
            {
                var parameters_read = context.MachineParameters.ToList();
                foreach (var param in parameters_read)
                {
                    if (param.Name == "Sawband_brand")
                    {
                        string address = DBfunction.Get_Machine_read_address(param.Name);
                        ushort[] rawValue = plc.ReadWordDevice(address, 20);
                        string Sawband_brand = Status.ConvertUShortArrayToAsciiString(rawValue);
                        DBfunction.Set_Machine_string("Sawband_brand", Sawband_brand);
                        continue;
                    }
                    if (param.Name == "Sawblade_material")
                    {
                        string address = DBfunction.Get_Machine_read_address(param.Name);
                        ushort[] rawValue = plc.ReadWordDevice(address, 10);
                        string Saw_blade_material = Status.ConvertUShortArrayToAsciiString(rawValue);
                        DBfunction.Set_Machine_string("Sawband_brand", Saw_blade_material);
                        continue;
                    }
                    if (param.Name == "Sawblade_teeth")
                    {
                        string address = DBfunction.Get_Machine_read_address(param.Name);
                        ushort[] rawValue = plc.ReadWordDevice(address, 2);
                        DBfunction.Set_Machine_string("Sawblade_teeth", rawValue[0].ToString() + " / " + rawValue[1].ToString());
                        continue;
                    }

                    try
                    {
                        string address = DBfunction.Get_Machine_read_address(param.Name);
                        ushort rawValue = plc.ReadWordDevice(address);
                        DBfunction.Set_Machine_now_number(param.Name, rawValue);

                        if (string.IsNullOrWhiteSpace(address))
                        {
                            continue;
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"❌ 錯誤：參數 {param.Name} 發生例外：{ex.Message}");
                    }
                }
               
            }

        }
    }
}



