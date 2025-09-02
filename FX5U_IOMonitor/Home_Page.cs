using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.MitsubishiPlc_Monior;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Resources;
using FX5U_IO元件監控;
using SLMP;
using System.Diagnostics;
using System.Windows.Forms;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Data.GlobalMachineHub;
using static FX5U_IOMonitor.Models.MonitoringService;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;
using static FX5U_IO元件監控.Part_Search;



namespace FX5U_IOMonitor
{
    public partial class Home_Page : Form
    {
        private CancellationTokenSource? _cts;
        private Sawband_Info? add_sawband_Form = null;
        private Saw_Info? add_saw_Form = null;
        private Stopwatch stopwatch;

        // 同步功能呼叫變數
        private static System.Timers.Timer? _syncTimer;
        private static readonly SemaphoreSlim _syncLock = new(1, 1);
        private static ApplicationDB? _SysLocal;
        private static CloudDbContext? _SysCloud;

        public Home_Page()
        {
            InitializeComponent();
            this.Load += Main_Load;
            this.FormClosing += Info_FormClosing;
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            SwitchLanguage();
            LanguageManager.LanguageChanged -= OnLanguageChanged;
            LanguageManager.LanguageChanged += OnLanguageChanged;
        }
        private void OnLanguageChanged(string cultureName)
        {
            reset_lab_connectText();

            SwitchLanguage();
        }

        private void Info_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _cts?.Cancel(); // 關閉時自動取消背景任務
        }

        private void Main_Load(object sender, EventArgs e)
        {


            reset_lab_connectText();
            _cts = new CancellationTokenSource();
            _ = Task.Run(() => AutoUpdateAsync(_cts.Token)); // 啟動背景更新任務
            _ = ConnectAndStartSyncAsync(btn_toggle);
            //_ = Task.Run(async () =>
            //{
            //    await ParameterHistoryScheduler.InitializeMonthlySchedule();
            //});


        }

        private async Task AutoUpdateAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                stopwatch = Stopwatch.StartNew(); // ✅ 計時開始

                try
                {
                    // 主執行緒呼叫 UI 更新
                    if (this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.Invoke(() =>
                        {
                            reset_lab_connectText(); // 每次自動更新畫面數值
                        });
                    }

                }
                catch (OperationCanceledException)
                {
                    break; // 正常取消任務
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("背景更新錯誤：" + ex.Message);
                }
                stopwatch.Stop(); // ✅ 計時結束
                await Task.Delay(900, token); // 每900毫秒更新一次


            }
        }

        private void reset_lab_connectText()//更新主頁面連接狀況
        {
            DateTime last = Properties.Settings.Default.Last_cloudupdatetime;
            lb_Last_cloudupdatetime.Text = LanguageManager.Translate("Mainform_Database_update") + "\n"
                + last.ToString("yyyy/MM/dd") + "\n" + last.ToString("HH:mm:ss");

            lab_green.Text = DBfunction.Get_Green_number("Drill").ToString();
            lab_yellow.Text = DBfunction.Get_Yellow_number("Drill").ToString();
            lab_red.Text = DBfunction.Get_Red_number("Drill").ToString();
            lab_sum.Text = DBfunction.GetMachineRowCount("Drill").ToString();

            

            lab_sum_swing.Text = DBfunction.GetMachineRowCount("Sawing").ToString();
            lab_red_swing.Text = DBfunction.Get_Red_number("Sawing").ToString();
            lab_yellow_swing.Text = DBfunction.Get_Yellow_number("Sawing").ToString();
            lab_green_swing.Text = DBfunction.Get_Green_number("Sawing").ToString();
            var machineList = DBfunction.GetMachineIP("Drill");
            if (machineList.IP_address == null || machineList.Port == null)
            {
                lab_IP_Port1.Text = LanguageManager.Translate("Mainform_IP_Port_none");
                lab_IP_Port1.Text = $"IP = {machineList.IP_address}；Port = {machineList.Port} ";
            }
            else
            {
                lab_IP_Port1.Text = $"IP = {machineList.IP_address}；Port = {machineList.Port} ";
            }
            machineList = DBfunction.GetMachineIP("Sawing");
            if (machineList.IP_address == null || machineList.Port == null)
            {
                lab_IP_Port2.Text = LanguageManager.Translate("Mainform_IP_Port_none");
                lab_IP_Port2.Text = $"IP = {machineList.IP_address}；Port = {machineList.Port} ";

            }
            else
            {
                lab_IP_Port2.Text = $"IP = {machineList.IP_address}；Port = {machineList.Port} ";
            }

            var existingContext = GetContext("Drill") as IMachineContext;

            if (existingContext != null && existingContext.IsConnected)
            {
                lab_connect.Text = existingContext.ConnectSummary.connect.ToString();
                List<string> breakdowm_part = DBfunction.Get_breakdown_part(existingContext.MachineName);
                lab_disconnect.Text = DBfunction.Get_address_ByBreakdownParts(existingContext.MachineName, breakdowm_part).Count.ToString();
                List<string> sawingbreakdowm_part = DBfunction.Get_breakdown_part("Sawing");
                lab_disconnect_sawing.Text = DBfunction.Get_address_ByBreakdownParts("Sawing", sawingbreakdowm_part).Count.ToString();
                lab_connect_1.Text = LanguageManager.Translate("Mainform_connect");
                lab_connect_1.ForeColor = Color.Green;
            }
            else
            {
                lab_connect_1.Text = LanguageManager.Translate("Mainform_disconnect");
                lab_connect_1.ForeColor = Color.Red;
                lab_connect.Text = "0";
                lab_disconnect.Text = "0";

            }

            existingContext = GetContext("Sawing") as IMachineContext;
            if (existingContext != null && existingContext.IsConnected)
            {
                lab_connect_2.Text = LanguageManager.Translate("Mainform_connect");
                lab_connect_2.ForeColor = Color.Green;
                lab_connect_swing.Text = existingContext.ConnectSummary.connect.ToString();

            }
            else
            {
                lab_connect_2.Text = LanguageManager.Translate("Mainform_disconnect");
                lab_connect_2.ForeColor = Color.Red;
                lab_connect_swing.Text = "0";
                lab_disconnect_sawing.Text = "0";
            }



        }



        private void Main_form_TextChanged(object sender, EventArgs e)
        {
            reset_lab_connectText();
        }

        private void btn_SawBand_Click(object sender, EventArgs e)
        {
            // 建立選單
            var menu = new ContextMenuStrip();

            // 加入「每週記錄」
            menu.Items.Add(LanguageManager.Translate("DrillInfo_Week_Record"), null, (_, __) =>
            {
                OpeSawbandInfoface(ScheduleFrequency.Weekly);
            });

            // 加入「每月記錄」
            menu.Items.Add(LanguageManager.Translate("DrillInfo_Month_Record"), null, (_, __) =>
            {
                OpeSawbandInfoface(ScheduleFrequency.Monthly);
            });

            // 顯示在滑鼠點擊處（或按鈕位置）
            var btn = (Button)sender;
            menu.Show(btn, new Point(0, btn.Height)); // 顯示在按鈕下方

        }


        private void btn_Drill_Info_Click(object sender, EventArgs e)
        {

            // 建立選單
            var menu = new ContextMenuStrip();

            // 加入「每週記錄」
            menu.Items.Add(LanguageManager.Translate("DrillInfo_Week_Record"), null, (_, __) =>
            {
                OpenMonitoringInterface(ScheduleFrequency.Weekly);
            });

            // 加入「每月記錄」
            menu.Items.Add(LanguageManager.Translate("DrillInfo_Month_Record"), null, (_, __) =>
            {
                OpenMonitoringInterface(ScheduleFrequency.Monthly);
            });

            // 顯示在滑鼠點擊處（或按鈕位置）
            var btn = (Button)sender;
            menu.Show(btn, new Point(0, btn.Height)); // 顯示在按鈕下方

        }
        private void OpenMonitoringInterface(ScheduleFrequency freq)
        {
            using var form = new Machine_monitoring_interface_card(freq);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);
        }
        private void OpeSawbandInfoface(ScheduleFrequency freq)
        {
            using var form = new Sawband_Info(freq);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);
        }
        private void OpeSawInfoface(ScheduleFrequency freq)
        {
            using var form = new Saw_Info(freq);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);
        }
        private void lab_disconnect_Click(object sender, EventArgs e)
        {
            var existingContext = MachineHub.Get("Drill");
            if (existingContext != null && existingContext.IsConnected)
            {

                List<string> breakdown_part = DBfunction.Get_breakdown_part("Drill");
                if (breakdown_part.Count != 0)
                {
                    List<string> breakdown_address = DBfunction.Get_address_ByBreakdownParts("Drill", breakdown_part);
                    var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
                    searchControl.LoadData(breakdown_address, "Drill");          //  將資料傳入模組
                    Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
                }
                else
                {
                    MessageBox.Show(LanguageManager.Translate("Machine_main_Message_Noabnormalities"));
                }
            }
            else
            {
                MessageBox.Show(LanguageManager.Translate("Machine_main_Message_Drill_Notconnect"));
            }
        }

        private void lab_disconnect_sawing_Click(object sender, EventArgs e)
        {
            var existingContext = MachineHub.Get("Sawing");
            if (existingContext != null && existingContext.IsConnected)
            {

                List<string> breakdown_part = DBfunction.Get_breakdown_part("Sawing");
                if (breakdown_part.Count != 0)
                {
                    List<string> breakdown_address = DBfunction.Get_address_ByBreakdownParts("Sawing", breakdown_part);
                    var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
                    searchControl.LoadData(breakdown_address, "Sawing");          //  將資料傳入模組
                    Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
                }
                else
                {
                    MessageBox.Show(LanguageManager.Translate("Machine_main_Message_Noabnormalities"));
                }
            }
            else
            {
                MessageBox.Show(LanguageManager.Translate("Machine_main_Message_Drill_Notconnect"));
            }
        }

        private void SwitchLanguage()
        {

            lab_sumD.Text = LanguageManager.Translate("Mainform_MonitoredItems");
            lab_sumS.Text = LanguageManager.Translate("Mainform_MonitoredItems");
            lab_connectD.Text = LanguageManager.Translate("Mainform_Connections");
            lab_connectS.Text = LanguageManager.Translate("Mainform_Connections");
            lab_disD.Text = LanguageManager.Translate("Mainform_ComponentFaults");
            lab_disS.Text = LanguageManager.Translate("Mainform_ComponentFaults");
            lab_rD.Text = LanguageManager.Translate("Mainform_RedLights");
            lab_rS.Text = LanguageManager.Translate("Mainform_RedLights");
            lab_yD.Text = LanguageManager.Translate("Mainform_YellowLights");
            lab_yS.Text = LanguageManager.Translate("Mainform_YellowLights");
            lab_gD.Text = LanguageManager.Translate("Mainform_GreenLights");
            lab_gS.Text = LanguageManager.Translate("Mainform_GreenLights");
            btn_Drill_Info.Text = LanguageManager.Translate("Mainform_MachineInfo");

            btn_Sawband.Text = LanguageManager.Translate("Mainform_SawBladeInfo");
            btn_Saw.Text = LanguageManager.Translate("Mainform_Sawing");

        }


        // 歸零
        private void lb_drill_du_Click(object sender, EventArgs e)
        {
            //Drill_Info.ConfirmAndResetUsetime("Drill", "electricity", "確定要將用電紀錄歸零嗎?", "用電紀錄已成功歸零");
            DBfunction.Set_Machine_now_string("Drill", "electricity", "0");
        }

        private void btn_saw_Click(object sender, EventArgs e)
        {

            // 建立選單
            var menu = new ContextMenuStrip();

            // 加入「每週記錄」
            menu.Items.Add(LanguageManager.Translate("DrillInfo_Week_Record"), null, (_, __) =>
            {
                OpeSawInfoface(ScheduleFrequency.Weekly);
            });

            // 加入「每月記錄」
            menu.Items.Add(LanguageManager.Translate("DrillInfo_Month_Record"), null, (_, __) =>
            {
                OpeSawInfoface(ScheduleFrequency.Monthly);
            });

            // 顯示在滑鼠點擊處（或按鈕位置）
            var btn = (Button)sender;
            menu.Show(btn, new Point(0, btn.Height)); // 顯示在按鈕下方
        }
        private void lab_power_Click(object sender, EventArgs e)
        {
            // 顯示目前值，並要求輸入新值
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "請輸入 COS 值（小數）：", "修改 COS 值", Properties.Settings.Default.COSValue.ToString());

            if (double.TryParse(input, out double newValue))
            {
                Properties.Settings.Default.COSValue = newValue;
                Properties.Settings.Default.Save(); // ✅ 寫入設定檔
                MessageBox.Show($"COS 值已更新為 {newValue:F2}");
            }
            else
            {
                MessageBox.Show("請輸入有效的數字格式！");
            }
        }
        private WeakReference<Button>? _statusBtnRef; 
        private async void btn_toggle_Click(object sender, EventArgs e)
        {

            btn_toggle.Enabled = false;

            try
            {
                // 若尚未建立或不可連線，先嘗試連線一次
                var connected = _SysCloud != null && await _SysCloud.Database.CanConnectAsync();
                if (!connected)
                {
                    await ConnectAndStartSyncAsync(btn_toggle);   
                    connected = _SysCloud != null && await _SysCloud.Database.CanConnectAsync();
                    if (!connected)
                    {
                        btn_toggle.Text = "Disconnected";
                        btn_toggle.BackColor = Color.Gainsboro;
                        btn_toggle.ForeColor = Color.Black;
                        btn_toggle.Enabled = true;
                        return;
                    }
                }

                // 開始連線-切換同步狀態
                if (_autoSyncRunning)
                {
                    // 停止同步
                    btn_toggle.Text = "Stopping...";
                    await StopAutoSyncAsync();

                    btn_toggle.Text = "Start Sync";
                    btn_toggle.BackColor = Color.Gainsboro;
                    btn_toggle.ForeColor = Color.Black;
                    btn_toggle.Enabled = true;
                }
                else
                {
                    // 開始同步
                    btn_toggle.Text = "Starting...";
                    await StartAutoSyncAsync(TimeSpan.FromMinutes(0.5)); // 自訂間隔

                    btn_toggle.Text = "Stop Sync";
                    btn_toggle.BackColor = Color.DodgerBlue;
                    btn_toggle.ForeColor = Color.White;
                    btn_toggle.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"切換同步時發生錯誤：{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 回復按鈕狀態
                btn_toggle.Text = _autoSyncRunning ? "Stop Sync" : "Start Sync";
                btn_toggle.Enabled = true;
            }

        }
        private CancellationTokenSource? _autoSyncCts;
        private Task? _autoSyncTask;
        private volatile bool _autoSyncRunning;   // 方便判斷目前是否在自動同步
       
        private void SetUiConnectedFromAnyThread()
        {
            if (_statusBtnRef != null && _statusBtnRef.TryGetTarget(out var btn))
            {
                if (btn.IsHandleCreated && btn.InvokeRequired)
                {
                    btn.BeginInvoke(new Action(() =>
                    {
                        btn.Text = "Connected";
                        btn.BackColor = Color.DodgerBlue;
                        btn.ForeColor = Color.White;
                        btn.Enabled = true;
                    }));
                }
                else
                {
                    btn.Text = "Connected";
                    btn.BackColor = Color.DodgerBlue;
                    btn.ForeColor = Color.White;
                    btn.Enabled = true;
                }
            }
        }
        /// <summary>
        /// 重新連線同步的功能
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private async Task ConnectAndStartSyncAsync(Button control)
        {
            _statusBtnRef = new WeakReference<Button>(control); 
            try
            {
                await StopAutoSyncAsync(); 
               
                DbConfig.LoadFromJson("DbConfig.json");
                // 先把舊的 context 正確釋放
                if (_SysCloud != null) { await _SysCloud.DisposeAsync(); _SysCloud = null; }
                if (_SysLocal != null) { await _SysLocal.DisposeAsync(); _SysLocal = null; }

                _SysCloud = new CloudDbContext();
                _SysLocal = new ApplicationDB();

                SetToggleConnecting();

                //control.Text = "Connecting";
                //control.BackColor = Color.LightBlue;
                //control.ForeColor = Color.Black;
                //control.Enabled = false;

                // 先檢查本地與雲端是否能連
                var cloudOk = await _SysCloud.Database.CanConnectAsync(); 

                if (!cloudOk)
                {
                    SetToggleDisconnected();
                    return;
                    //control.Text = "Disconnected";
                    //control.BackColor = Color.Gainsboro;
                    //control.ForeColor = Color.Black;
                    //btn_toggle.Enabled = true;         
                    //return;
                }

                //control.Text = "Syncing...";
                //control.BackColor = Color.DodgerBlue;
                //control.ForeColor = Color.Black;
                SetToggleSyncing();

                await TableSync.SyncCloudToLocalAllTables(_SysLocal, _SysCloud);
                Properties.Settings.Default.Last_cloudupdatetime = DateTime.Now;
                Properties.Settings.Default.Save();

                SetToggleSyncing();
                await TableSync.SyncLocalToCloudAllTables(_SysLocal, _SysCloud);

                await StartAutoSyncAsync(TimeSpan.FromSeconds(30)); // ★ 改用 async 版
                SetUiConnectedFromAnyThread();
                SetToggleConnected();
                //control.Text = "Connected";
                //control.BackColor = Color.DodgerBlue;
                //control.ForeColor = Color.White;
                //control.Enabled = true;

            }
            catch (Exception ex)
            {
                SetToggleDisconnected();

                //control.Text = "Disconnected";
                //control.BackColor = Color.Gainsboro;
                //control.ForeColor = Color.Black;
                //btn_toggle.Enabled = true;
            }
        }
        /// <summary>
        /// 開啟同步功能
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        private Task StartAutoSyncAsync(TimeSpan? interval = null)
        {
            if (_autoSyncRunning) return Task.CompletedTask; // 已在跑就略過
            _autoSyncRunning = true;

            _autoSyncCts = new CancellationTokenSource();
            var token = _autoSyncCts.Token;
            var gap = interval ?? TimeSpan.FromMinutes(0.5);   // 同步間隔

            _autoSyncTask = Task.Run(async () =>
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        bool stillConnected = true;

                        try
                        {
                            stillConnected = await TableSync.SyncLocalToCloudAllTables(_SysLocal, _SysCloud, token);

                            // 開啟同步
                            //await TableSync.SyncLocalToCloudAllTables(_SysLocal, _SysCloud);   
                        }
                        catch (OperationCanceledException)
                        {
                            break; // 被取消就跳出
                        }
                        catch (Exception ex)
                        {
                            // 記一下錯誤，但不中斷整體 loop
                            Message_Config.LogMessage($"AutoSync error: {ex.Message}");
                            stillConnected = await TableSync.SafeCanConnectAsync(_SysCloud, token);

                        }
                        if (!stillConnected)
                        {
                            SetToggleDisconnected();
                            break;
                        }
                        SetToggleConnected();

                        try { await Task.Delay(gap, token); }
                        catch (OperationCanceledException) { break; }
                    }
                }
                finally
                {
                    _autoSyncRunning = false;
                    _autoSyncCts?.Dispose();
                    _autoSyncCts = null;
                }
            }, token);

            return Task.CompletedTask;
        }
        /// <summary>
        /// 關閉同步
        /// </summary>
        /// <returns></returns>
        private async Task StopAutoSyncAsync()
        {
            if (!_autoSyncRunning) return;

            try
            {
                _autoSyncCts?.Cancel();
                if (_autoSyncTask != null)
                    await _autoSyncTask.ConfigureAwait(false);
            }
            finally
            {
                _autoSyncTask = null;
                _autoSyncCts?.Dispose();
                _autoSyncCts = null;
                _autoSyncRunning = false;
            }
        }
        /// <summary>
        /// 以下為同步的按鈕切換
        /// </summary>
        /// <param name="action"></param>
        private void WithToggle(Action<Button> action)
        {
            if (btn_toggle.IsDisposed) return;

            if (btn_toggle.IsHandleCreated && btn_toggle.InvokeRequired)
                btn_toggle.BeginInvoke(new Action(() => action(btn_toggle)));
            else
                action(btn_toggle);
        }

        private void SetToggleDisconnected()
        {
            WithToggle(b =>
            {
                b.Text = "Disconnected";
                b.BackColor = Color.Gainsboro;
                b.ForeColor = Color.Black;
                b.Enabled = true;
            });
        }

        private void SetToggleConnected()
        {
            WithToggle(b =>
            {
                b.Text = "Connected";
                b.BackColor = Color.DodgerBlue;
                b.ForeColor = Color.White;
                b.Enabled = true;
            });
        }

        private void SetToggleConnecting()
        {
            WithToggle(b =>
            {
                b.Text = "Connecting";
                b.BackColor = Color.LightBlue;
                b.ForeColor = Color.Black;
                b.Enabled = false;
            });
        }

        private void SetToggleSyncing()
        {
            WithToggle(b =>
            {
                b.Text = "Syncing...";
                b.BackColor = Color.DodgerBlue;
                b.ForeColor = Color.Black;
                b.Enabled = false;
            });
        }




    }
}
