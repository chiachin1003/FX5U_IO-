using static FX5U_IOMonitor.Main;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using SLMP;
using System.IO.Ports;
using Modbus.Device; // 來自 NModbus4
using static FX5U_IOMonitor.Models.MonitoringService;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Threading;
using System.Net;
using System.Timers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FX5U_IOMonitor
{
    public partial class connect_PLC : Form
    {
   
        public class connect_isOK //提供各介面共用的X_IO監控數據
        {
            public static bool Sawing_connect =false;
            public static bool Drill_connect = false;

        }


        public class Connect_Rs485
        {
            public string PortName { get; set; }
            public int BaudRate { get; set; }

        }

        public connect_PLC(Main main)
        {

            InitializeComponent();
            UpdateConnectmachinComboBox();
            //control_choose.SelectedIndex = 0;
            //connect_choose.SelectedIndex = 0;
            //comb_language.SelectedIndex = 0;

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


        private void btn_connect_ethernet_Click(object sender, EventArgs e)
        {
            string connect_machine = control_choose.Text;

            // 先判斷 Drill 是否已經連線
            var existingContext = MachineHub.Get(connect_machine);
            if (existingContext != null && existingContext.IsConnected)
            {
                return;

            }
            var plc = SLMP_connect(txb_IP.Text.Trim(), int.Parse(txb_port.Text));
            if (plc == null)
            {
                MessageBox.Show($"連線失敗，請檢查硬體IP及位置後重新連線");
                return;
            }

            // 註冊機台與自動掛上監控器
            MachineHub.RegisterMachine(connect_machine, plc);

            // 取得註冊後的 context
            var context = MachineHub.Get(connect_machine);
            if (context == null || !context.IsConnected)
            {
                MessageBox.Show($"註冊後讀取 {connect_machine} 資訊失敗");
                return;
            }

            // 告知 Monitor 要使用對應 Lock
            context.Monitor.SetExternalLock(context.LockObject);

            // 啟動監控任務
            _ = Task.Run(() => context.Monitor.MonitoringLoop(context.TokenSource.Token, context.MachineName));
            if (context.IsMaster) // 只針對主機執行 alarm 監控
            {
                _ = Task.Run(() => context.Monitor.alarm_MonitoringLoop(
                    context.TokenSource.Token));
            }

            int[] writemodes = DBfunction.Get_Machine_Calculate_type(context.MachineName);
            int[] read_modes = DBfunction.Get_Machine_Readview_type(context.MachineName);

            _ = Task.Run(() => context.Monitor.Read_Bit_Monitor_AllModesAsync(context.MachineName, writemodes, context.TokenSource.Token));
            _ = Task.Run(() => context.Monitor.Read_Word_Monitor_AllModesAsync(context.MachineName, read_modes, context.TokenSource.Token));
            _ = Task.Run(() => context.Monitor.Write_Word_Monitor_AllModesAsync(context.MachineName, writemodes, context.TokenSource.Token));
            _ = Task.Run(() => context.Monitor.Read_None_Monitor_AllModesAsync(context.MachineName, context.TokenSource.Token));

            // 註冊變更事件
            context.Monitor.IOUpdated += DB_update_change;

        }
        /// <summary>
        /// 監控及記錄當前實體元件的使用次數
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DB_update_change(object? sender, IOUpdateEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => DB_update_change(sender, e)));
                return;
            }

            try
            {
                var monitor = sender as MonitorService;
                string? datatable = monitor?.MachineName;

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
            string connect_machine = control_choose.Text;

            // 註冊機台與自動掛上監控器
            MachineHub.UnregisterMachine(connect_machine);

        }
        private SerialPort serialPort;
        private IModbusSerialMaster modbusMaster;
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


        private void Initialize_Aalarm(SlmpClient plcdevice)
        {
            var alarm = Calculate.Alarm_trans();
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

                    int updated = Calculate.UpdatealarmCurrentSingleToDB(result, "alarm");
                    return;

                }

            }
        }

        private void btn_addmachine_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txb_machine.Text))
            {
                MessageBox.Show("請為需監控的機台進行命名後得以匯入實體元件");
                return;
            }
            string targetMachineName = txb_machine.Text.Trim();

            // 檢查是否已存在重複名稱
            using (var context = new ApplicationDB())
            {
                bool isDuplicate = context.Machine_IO.Any(m => m.Machine_name == targetMachineName);
                if (isDuplicate)
                {
                    MessageBox.Show($"❌ 機台名稱「{targetMachineName}」已存在，請重新命名後再匯入。");
                    return;
                }
            }
            // Select file through OpenFileDialog
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Csv Files|*.csv";
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select a Machine IO Csv file";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Csv2Db.Initialization_MachineElementFromCSV(txb_machine.Text, openFileDialog.FileName);
            UpdateConnectmachinComboBox();

        }
        private void UpdateConnectmachinComboBox()
        {
            using (var context = new ApplicationDB())
            {
                //var machineNames = context.Machine_IO
                //                    .Select(io => io.Machine_name)  // 只取 Machine_name 欄位
                //                    .Distinct()                     // 過濾重複值
                //                    .ToList();                      // 轉成 List<string>
                var machineNames = context.index
                                   .Select(io => io.Name);
                           
                control_choose.Items.Clear();

                foreach (var machine in machineNames)
                {
                    control_choose.Items.Add(machine);
                }
                control_choose.SelectedIndex = -1;
            }


        }

      
    }
}



