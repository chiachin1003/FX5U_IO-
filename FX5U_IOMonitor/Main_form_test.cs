using SLMP;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using FX5U_IO元件監控;
using static FX5U_IO元件監控.Part_Search;
using FX5U_IOMonitor.Login;
using System.Diagnostics;
using FX5U_IOMonitor.panel_control;
using static FX5U_IOMonitor.Data.GlobalMachineHub;
using static FX5U_IOMonitor.Models.ParameterHistoryManager;
using FX5U_IOMonitor.Resources;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;



namespace FX5U_IOMonitor
{
    public partial class Main_form_test : Form
    {
        private CancellationTokenSource? _cts;
        private Sawband_Info? add_sawband_Form = null;
        private Saw_Info? add_saw_Form = null;
        private Stopwatch stopwatch;

        public Main_form_test()
        {
            InitializeComponent();
            this.Load += Main_Load;
            this.FormClosing += Info_FormClosing;
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            SwitchLanguage();
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

            var existingContext = GlobalMachineHub.GetContext("Drill") as IMachineContext;

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

            existingContext = GlobalMachineHub.GetContext("Sawing") as IMachineContext;
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
            if (add_sawband_Form == null || add_sawband_Form.IsDisposed)
            {
                add_sawband_Form = new Sawband_Info();
                add_sawband_Form.Show();
            }
            else
            {
                add_sawband_Form.BringToFront();  // 若已開啟，拉到最前面
            }

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
                    MessageBox.Show("目前料件未出現異常");
                }
            }
            else
            {
                MessageBox.Show("請連線機台");
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
                    MessageBox.Show("目前料件未出現異常");
                }
            }
            else
            {
                MessageBox.Show("請連線機台");
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

            if (add_saw_Form == null || add_saw_Form.IsDisposed)
            {
                add_saw_Form = new Saw_Info();
                add_saw_Form.Show();
            }
            else
            {
                add_saw_Form.BringToFront();  // 若已開啟，拉到最前面
            }
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
    }
}
