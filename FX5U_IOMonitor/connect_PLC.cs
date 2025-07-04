
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Email;
using SLMP;
using System.IO.Ports;
using Modbus.Device; // 來自 NModbus4
using static FX5U_IOMonitor.Models.MonitoringService;
using static FX5U_IOMonitor.Models.ModbusMonitorService;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Resources;
using System.Windows.Forms;
using System.Diagnostics;



namespace FX5U_IOMonitor
{
    public partial class Connect_PLC : Form
    {


        Main main_control;

        public Connect_PLC(Main main)
        {
            main_control = main;
            InitializeComponent();
            UpdateConnectmachinComboBox();
            comb_Baudrate.SelectedItem = "115200";
            comb_Bits.SelectedItem = "8";
            comb_Parity.SelectedItem = "None";
            comb_StopBits.SelectedItem = "One";
            //control_choose.SelectedIndex = 0;
            //connect_choose.SelectedIndex = 0;
            //comb_language.SelectedIndex = 0;

            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            SwitchLanguage();
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel2);
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel3);
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel1);



            LanguageManager.LanguageChanged += OnLanguageChanged;

            connect_choose.SelectedIndexChanged += connect_choose_SelectedIndexChanged;
            control_choose.SelectedIndexChanged += control_choose_SelectedIndexChanged;

        }

        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel2);
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel3);
            ApplyAutoFontShrinkToTableLabels(tableLayoutPanel1);

        }
        /// <summary>
        /// 自動連線當前機台
        /// </summary>
        public static void AutoConnectAllMachines(Connect_PLC plcForm)
        {
            using var context = new ApplicationDB();
            var machineList = context.Machine.ToList();

            var ipPortSet = new HashSet<string>();

            foreach (var machine in machineList)
            {
                if (string.IsNullOrWhiteSpace(machine.IP_address) || machine.Port == 0)
                    continue;

                string ipPortKey = $"{machine.IP_address}:{machine.Port}";
                if (ipPortSet.Contains(ipPortKey))
                {
                    Debug.WriteLine($"⚠ IP/Port 重複：{ipPortKey}");
                    continue;
                }
                ipPortSet.Add(ipPortKey);

                // 檢查是否已連線
                var existing = MachineHub.Get(machine.Name);
                if (existing != null && existing.IsConnected)
                    continue;

                var plc = SLMP_connect(machine.IP_address, machine.Port);
                if (plc == null)
                {
                    Debug.WriteLine($"❌ {machine.Name} 無法連線");
                    continue;
                }

                MachineHub.RegisterMachine(machine.Name, plc);
                var contextItem = MachineHub.Get(machine.Name);
                if (contextItem == null || !contextItem.IsConnected)
                {
                    Debug.WriteLine($"❌ {machine.Name} 註冊失敗");
                    continue;
                }

                contextItem.Monitor.SetExternalLock(contextItem.LockObject);
                _ = Task.Run(() => contextItem.Monitor.MonitoringLoop(contextItem.TokenSource.Token, contextItem.MachineName));
                
                if (contextItem.IsMaster) // 只針對主機執行 alarm 監控
                {
                    //補上若是程式關閉時解除警報的話則寫入解除時間
                    DBfunction.Fix_UnclosedAlarms_ByCurrentState();

                    //開始進行警告監控
                    _ = Task.Run(() => contextItem.Monitor.alarm_MonitoringLoop(
                        contextItem.TokenSource.Token));
                    //發送警告訊息等功用
                    contextItem.Monitor.alarm_event += plcForm.FailureAlertMail;
                }

                int[] writemodes = DBfunction.Get_Machine_Calculate_type(contextItem.MachineName);
                int[] read_modes = DBfunction.Get_Machine_Readview_type(contextItem.MachineName);



                _ = Task.Run(() => contextItem.Monitor.Read_Bit_Monitor_AllModesAsync(contextItem.MachineName, writemodes, contextItem.TokenSource.Token));
                _ = Task.Run(() => contextItem.Monitor.Read_Word_Monitor_AllModesAsync(contextItem.MachineName, read_modes, contextItem.TokenSource.Token));
                _ = Task.Run(() => contextItem.Monitor.Read_None_Monitor_AllModesAsync(contextItem.MachineName, contextItem.TokenSource.Token));
                _ = Task.Run(() => contextItem.Monitor.Write_Word_Monitor_AllModesAsync(contextItem.MachineName, writemodes, contextItem.TokenSource.Token));




                contextItem.Monitor.IOUpdated += DB_update_change;

                Debug.WriteLine($"✅ 自動連線 {machine.Name} 成功");
            }
        }
        private void connect_choose_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                combobox_text_center();


            }
            if (connect_choose.SelectedIndex == 2)
            {
                panel_RS485.Visible = true;
                panel_Ethernet.Visible = false;
                button_FILE.Visible = false;
                button2.Visible = false;
                combobox_text_center();

            }
            if (connect_choose.SelectedIndex == 2 && control_choose.SelectedIndex == 2)
            {
                button_FILE.Visible = true;
                button2.Visible = true;

            }

        }
        private static SlmpClient? SLMP_connect(string IP, int port)
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
                //補上若是程式關閉時解除警報的話則寫入解除時間
                DBfunction.Fix_UnclosedAlarms_ByCurrentState();

                //開始進行警告監控
                _ = Task.Run(() => context.Monitor.alarm_MonitoringLoop(
                    context.TokenSource.Token));
                //發送警告訊息等功用
                context.Monitor.alarm_event += FailureAlertMail;
            }

            int[] writemodes = DBfunction.Get_Machine_Calculate_type(context.MachineName);
            int[] read_modes = DBfunction.Get_Machine_Readview_type(context.MachineName);



            _ = Task.Run(() => context.Monitor.Read_Bit_Monitor_AllModesAsync(context.MachineName, writemodes, context.TokenSource.Token));
            _ = Task.Run(() => context.Monitor.Read_Word_Monitor_AllModesAsync(context.MachineName, read_modes, context.TokenSource.Token));
            _ = Task.Run(() => context.Monitor.Read_None_Monitor_AllModesAsync(context.MachineName, context.TokenSource.Token));
            _ = Task.Run(() => context.Monitor.Write_Word_Monitor_AllModesAsync(context.MachineName, writemodes, context.TokenSource.Token));

            string? errorMessage;
            bool result = DBfunction.SetMachineIP(connect_machine, txb_IP.Text.Trim(), txb_port.Text, out errorMessage);

            if (!result)
            {
                MessageBox.Show($"❌ 儲存失敗：{errorMessage}");
            }

            // 註冊變更事件
            context.Monitor.IOUpdated += DB_update_change;

        }
        /// <summary>
        /// 監控及記錄當前實體元件的使用次數
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 移除 this.Invoke，改為全程背景處理
        private static void DB_update_change(object? sender, IOUpdateEventArgs e)
        {
            Task.Run(() => ProcessIOUpdate(sender, e));
        }

        private static void ProcessIOUpdate(object? sender, IOUpdateEventArgs e)
        {
            try
            {
                string? datatable = sender switch
                {
                    MonitorService slmp => slmp.MachineName,
                    ModbusMonitorService modbus => modbus.MachineName,
                    _ => null
                };

                if (string.IsNullOrWhiteSpace(datatable))
                    return;

                int number = DBfunction.Get_use_ByAddress(datatable, e.Address);
                if (number < 0)
                {
                    DBfunction.Set_use_ByAddress(datatable, e.Address, 0);
                }
                else
                {
                    DBfunction.Set_use_ByAddress(datatable, e.Address, number + 1);
                }

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

        private void btn_disconnect_RS485_Click(object sender, EventArgs e)
        {
            string connect_machine = control_choose.Text;

            // 註冊機台與自動掛上監控器
            ModbusMachineHub.UnregisterModbusMachine(connect_machine);
        }

        private void btn_connect_RS485_Click(object sender, EventArgs e)
        {

            var baudRate = int.Parse(comb_Baudrate.SelectedItem?.ToString() ?? "115200");
            var dataBits = int.Parse(comb_Bits.SelectedItem?.ToString() ?? "8");
            // 同位元轉換字串 → Enum
            var parity = Enum.TryParse<Parity>(comb_Parity.SelectedItem?.ToString(), out var parsedParity)
                         ? parsedParity : Parity.None;

            // 停止位元轉換字串 → Enum
            var stopBits = Enum.TryParse<StopBits>(comb_StopBits.SelectedItem?.ToString(), out var parsedStopBits)
                           ? parsedStopBits : StopBits.One;


            //var port = new SerialPort(txb_comport.Text, 115200, Parity.None, 8, StopBits.One);
            var port = new SerialPort(txb_comport.Text, baudRate, parity, dataBits, stopBits);

            port.Open();

            // 建立 Modbus 主站
            var master = ModbusSerialMaster.CreateRtu(port);

            string connect_machine = control_choose.Text;

            // 註冊機台到 ModbusMachineHub
            ModbusMachineHub.RegisterModbusMachine(connect_machine, master, slaveId: 1);

            // 啟動監控
            var monitor = ModbusMachineHub.GetModbusMonitor(connect_machine);
            var token = ModbusMachineHub.Get(connect_machine)?.TokenSource.Token;
            if (monitor != null && token.HasValue)
            {
                _ = monitor.MonitoringLoop(token.Value);

                // 註冊變更事件
                monitor.IOUpdated += DB_update_change;
            }

        }


        private void control_choose_SelectedIndexChanged(object sender, EventArgs e)
        {
            combobox_text_center();

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
            DBfunction.AddMachineKeyIfNotExist($"Mainform_{txb_machine.Text}", txb_machine.Text);
            MachineButton.UpdateMachineButtons(main_control.panel_choose, main_control.btn_Main, main_control.panel_main);
            UpdateConnectmachinComboBox();

        }
        private void UpdateConnectmachinComboBox()
        {
            using (var context = new ApplicationDB())
            {
                var machineNames = context.Machine
                                   .Select(io => io.Name);

                control_choose.Items.Clear();

                foreach (var machine in machineNames)
                {
                    control_choose.Items.Add(machine);
                }
                control_choose.SelectedIndex = -1;
            }


        }

        /// <summary>
        /// 發出警告郵件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void FailureAlertMail(object? sender, IOUpdateEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => FailureAlertMail(sender, e)));
                return;
            }
            try
            {
                string? datatable = sender switch
                {
                    MonitorService slmp => slmp.MachineName,
                    ModbusMonitorService modbus => modbus.MachineName,
                    _ => null
                };

                if (string.IsNullOrWhiteSpace(datatable))
                {
                    return;
                }

                DBfunction.Set_alarm_current_single_ByAddress(e.Address, e.NewValue);
                //MessageBox.Show($"📡 偵測到 I/O 變化：{e.Address} from {e.OldValue} ➜ {e.NewValue}");

                if (e.NewValue == true)
                {
                    DBfunction.Set_Alarm_StartTimeByAddress(e.Address);
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await HandleAlarmAndSendEmailAsync(e);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"❌ 背景郵件任務錯誤：{ex.Message}");
                        }
                    });

                }
                else 
                {
                    DBfunction.Set_Alarm_EndTimeByAddress(e.Address);

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Monitor_DBuse_Updated 發生例外：{ex.Message}");
            }




        }
        public async Task HandleAlarmAndSendEmailAsync(IOUpdateEventArgs e)
        {
            try
            {
                string notifyUsers = DBfunction.Get_AlarmNotifyuser_ByAddress(e.Address); // 例如從 DB 查出 user1,user2
                var alarm = new Alarm_config();
                List<string> receivers = await alarm.GetAlarmNotifyEmails(notifyUsers);
                string machineName = DBfunction.Get_Machine_ByAddress(e.Address); // 例如從 DB 查出 user1,user2
                string partNumber = DBfunction.Get_Description_ByAddress(e.Address);
                string addressList = e.Address;
                string faultLocation = DBfunction.Get_Error_ByAddress(e.Address);
                string possibleReasons = DBfunction.Get_Possible_ByAddress(e.Address);
                string suggestions = DBfunction.Get_Repair_steps_ByAddress(e.Address);

                if (receivers == null || receivers.Count == 0)
                {
                    Console.WriteLine($"⚠️ 無收件者，警報 {e.Address} 未發送。");
                    return;
                }
                try
                {
                    await email.SendFailureAlarmMail(
                    receivers,
                    machineName,
                    partNumber,
                    addressList: new List<string> { e.Address },
                    faultLocation,
                    possibleReasons: new List<string> { possibleReasons },
                    suggestions: new List<string> { suggestions });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ 寄送郵件失敗：{ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ HandleAlarmAndSendEmailAsync 執行失敗：{ex.Message}");
            };
        }

      

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string targetMachine = control_choose.Text;
            // 確認刪除
            var confirm = MessageBox.Show($"⚠️ 是否確定要刪除機台「{targetMachine}」及其所有資料？", "刪除確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
                return;

            // 資料庫刪除
            DBfunction.DeleteMachineByName(targetMachine);

            // 重新更新所有機台按鈕（可確保同步）
            MachineButton.UpdateMachineButtons(main_control.panel_choose, main_control.btn_Main, main_control.panel_main);

            // 重建下拉選單
            UpdateConnectmachinComboBox();

            MessageBox.Show("✅ 機台刪除完成");

        }


        private void SwitchLanguage()
        {
            lab_Add_Machine.Text = LanguageManager.Translate("Connect_Add_Machine_name");
            lab_MachineType.Text = LanguageManager.Translate("Connect_MachineType");
            lab_Type.Text = LanguageManager.Translate("Connect_Type");
            label_IP.Text = LanguageManager.Translate("Connect_Enthernetaddress");
            lab_Enthernetport.Text = LanguageManager.Translate("Connect_Enthernetport");
            btn_connect_ethernet.Text = LanguageManager.Translate("Connect_Connect");
            btn_disconnect_ethernet.Text = LanguageManager.Translate("Connect_Disconnect");
            btn_connect_RS485.Text = LanguageManager.Translate("Connect_Connect");
            btn_disconnect_RS485.Text = LanguageManager.Translate("Connect_Disconnect");
            label_COM.Text = LanguageManager.Translate("Connect_RS_Port");
            label_BaudRate.Text = LanguageManager.Translate("Connect_RS_BaudRate");
            lab_Bits.Text = LanguageManager.Translate("Connect_RS_Bits");
            lab_Parity.Text = LanguageManager.Translate("Connect_RS_Parity");
            lab_StopBits.Text = LanguageManager.Translate("Connect_RS_StopBits");
            btn_addmachine.Text = LanguageManager.Translate("Element_btn_add");

        }
        private void combobox_text_center()
        {

            Text_design.SetComboBoxCenteredDraw(comb_Parity);
            Text_design.SetComboBoxCenteredDraw(comb_StopBits);
            Text_design.SetComboBoxCenteredDraw(comb_Baudrate);
            Text_design.SetComboBoxCenteredDraw(comb_Bits);

        }
        private void ApplyAutoFontShrinkToTableLabels(TableLayoutPanel panel)
        {
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is Label lbl)
                {
                    lbl.Dock = DockStyle.Fill;
                    lbl.AutoSize = false;
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                    Text_design.FitFontToLabel(lbl);
                }
            }
        }

        private void btn_mishubishi_Click(object sender, EventArgs e)
        {

            var cncForm = new Connect_CNC();

            cncForm.TopLevel = false; // 禁止作為獨立窗口
            cncForm.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            cncForm.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            main_control.panel_main.Controls.Clear(); // 清空 Panel
            main_control.panel_main.Controls.Add(cncForm); // 添加子窗體
            cncForm.Show(); // 顯示子窗體
        }

    }
      
}



