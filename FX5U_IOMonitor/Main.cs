using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.MitsubishiPlc_Monior;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Resources;
using FX5U_IO元件監控;
using MailKit;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Windows.Forms;
using static FX5U_IOMonitor.Data.Recordmode;
using static FX5U_IOMonitor.Message.Notify_Message;
using static FX5U_IOMonitor.Models.Csv2Db;
using static FX5U_IOMonitor.Models.MonitorFunction;
using static FX5U_IOMonitor.Models.MonitoringService;
using static FX5U_IOMonitor.Models.UI_Display;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Timer = System.Windows.Forms.Timer;


namespace FX5U_IOMonitor
{
    public partial class Main : Form
    {
        private Connect_PLC plcForm; // 連接介面
        private Search_main search_main;
        private Home_Page main_Form;
        public event EventHandler? LoginSucceeded;
        public event EventHandler? LogoutSucceeded;
        public List<Button> machineButtons;

        private static Main _instance;
        public static Main Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Main();
                }
                return _instance;
            }
        }

        public Panel TargetPanel => this.panel_main;  // 讓其他視窗存取 panel_main
        public void UpdatePanel(Control newContent)
        {

            this.panel_main.Controls.Clear();
            newContent.Dock = DockStyle.Fill;

            this.panel_main.Controls.Add(newContent);

        }
        private readonly Dictionary<string, RuntimewordTimer> timer_word = new();
        private readonly MonitoringService _service;
        private Timer _checkTimer;

        public Main()
        {

            InitializeComponent();

            // 檢查是否已初始化
            try
            {
                using (var context = new ApplicationDB())
                {
                    string[] targetMachines = { "Drill", "Sawing" };
                    var duplicated = context.Machine_IO
                                             .Where(m => targetMachines.Contains(m.Machine_name))
                                             .Select(m => m.Machine_name)
                                             .ToList();

                    if (duplicated.Any())
                    {
                        string duplicatedNames = string.Join("、", duplicated.Distinct());
                        //MessageBox.Show($"❌ 機台已初始化（{duplicatedNames}），請重新命名後再匯入。", "資料重複", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Csv2Db.Initialization_MachineElementFromCSV("Drill", "Drill_Data2.csv");
                        Csv2Db.Initialization_MachineElementFromCSV("Sawing", "Saw_Data2.csv");
                        //MessageBox.Show("✅ 機台資料匯入完成。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Machine_IO 初始化或檢查失敗：{ex.Message}", "初始化錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            // 從資料庫取得當前機台數量（index_name + display_name）
            List<Machine_number> machineList = DBfunction.GetMachineIndexes();
            machineButtons = new List<Button>(machineList.Count);

            // 取得插入點（btn_Main 下方）
            int indexBelowMain = panel_choose.Controls.GetChildIndex(btn_Main);

            // ✅ 順序建立，但每次插到 btn_Main 原本位置，讓 btn_Main 往上推
            for (int i = 0; i < machineList.Count; i++)
            {
                string indexName = machineList[i].Name;

                Button btn = MachineButton.CreateMachineButton(indexName, panel_main);
                machineButtons.Add(btn);

                panel_choose.Controls.Add(btn);
                panel_choose.Controls.SetChildIndex(btn, indexBelowMain); // 順序向下插入原本 btn_Main 的位置
            }

            InitLanguageComboBox();


            _instance = this; 
            plcForm = new Connect_PLC(this);
            DisconnectEvents.FailureConnect += OnFailureConnect;

            _ = Connect_PLC.AutoConnectAllMachines(plcForm); //自動連線




            search_main = new Search_main();

            this.Shown += MainForm_Shown;

            main_Form = new Home_Page();
            main_Form.TopLevel = false; // 禁止作為獨立窗口
            main_Form.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            main_Form.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            panel_main.Controls.Clear(); // 清空 Panel
            panel_main.Controls.Add(main_Form); // 添加子窗體
            main_Form.Show(); // 顯示子窗體

            this.Shown += Main_Shown;

        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            // 設置子窗體屬性以嵌入 Panel
            plcForm.TopLevel = false; // 禁止作為獨立窗口
            plcForm.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            plcForm.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            panel_main.Controls.Clear(); // 清空 Panel
            panel_main.Controls.Add(plcForm); // 添加子窗體
            plcForm.Show(); // 顯示子窗體
        }
        

        private async void OnFailureConnect(string machineName)
        {
            MachineHub.UnregisterMachine(machineName);
            await Task.Delay(1000);

            int repeat = Connect_PLC.AutoConnectAllMachines(plcForm, machineName); //自動連線

            // 2. 第一次嘗試失敗 → 通知一次
            if (repeat != 0)
            {
                await Task.Delay(10000); // 等待 10 秒再跳通知
                MessageBox.Show(machineName + LanguageManager.Translate("Main_Message_AutoConnect"));
            }
            // 3. 持續重連，直到成功
            int i = 1;
            while (repeat != 0)
            {
                await Task.Delay(10000); // 每 10 秒重試
                Debug.WriteLine($"第 {i} 次連線測試");

                repeat = Connect_PLC.AutoConnectAllMachines(plcForm, machineName);

                if (repeat == 0)
                {
                    Debug.WriteLine("連線成功");
                    break;
                }

                i++;
            }

           
        }
      
    

        private void btn_Main_Click(object sender, EventArgs e)
        { // 清空 Panel 的內容
            panel_main.Controls.Clear();
            main_Form.TopLevel = false; // 禁止作為獨立窗口
            main_Form.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            main_Form.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            panel_main.Controls.Clear(); // 清空 Panel
            panel_main.Controls.Add(main_Form); // 添加子窗體
            main_Form.Show(); // 顯示子窗體
            DisplayLanguage();
        }


        private void DisplayLanguage()
        {
            panel_language.Visible = true;
            panel_select.Controls.Add(panel_language);
            panel_select.Controls.Add(btn_log_out);

            panel_language.Controls.Add(comb_language);
            panel_language.Controls.Add(btn_language);

            panel_language.Visible = true;
        }




        private void btn_search_Click(object sender, EventArgs e)
        {
            panel_select.Controls.Clear(); // 清空 Panel
                                           // 設置子窗體屬性以嵌入 Panel
            search_main.TopLevel = false; // 禁止作為獨立窗口
            search_main.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            search_main.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            panel_main.Controls.Clear(); // 清空 Panel
            panel_main.Controls.Add(search_main); // 添加子窗體
            search_main.Show(); // 顯示子窗體
        }


        async void MainForm_Shown(object? sender, EventArgs e) //初始化預設使用者
        {
            using (var userService = LocalDbProvider.GetUserService())
            {
                await userService.CreateDefaultUserAsync();
            }

        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            using (var userService = LocalDbProvider.GetUserService())
            {
                userService.Logout();
            }
            LogoutSucceeded?.Invoke(this, EventArgs.Empty);

            this.Enabled = false;

            bool loginSucceeded = false;

            while (!loginSucceeded)
            {
                var loginForm = new UserLoginForm();
                var result = loginForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    loginSucceeded = true;
                    this.Enabled = true;

                }
                else
                {
                    var retry = MessageBox.Show(LanguageManager.Translate("User_Login_Form_Message"), LanguageManager.Translate("User_Login_Form_hint"), MessageBoxButtons.YesNo);
                    if (retry == DialogResult.No)
                    {
                        this.Close(); // 使用者選擇不重試 => 關閉主畫面 => 結束程式
                        return;
                    }
                }
            }

            this.Show();  // 成功登入後再顯示主畫面

        }

        private void btn_email_Click(object sender, EventArgs e)
        {
            var form = new Alarm_Notify();

            panel_select.Controls.Clear(); // 清空 Panel
                                           // 設置子窗體屬性以嵌入 Panel
            form.TopLevel = false; // 禁止作為獨立窗口
            form.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            form.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            panel_main.Controls.Clear(); // 清空 Panel
            panel_main.Controls.Add(form); // 添加子窗體
            form.Show(); // 顯示子窗體


        }



        public void InitLanguageComboBox()
        {
            LanguageManager.SyncAvailableLanguages(LanguageManager.Currentlanguge);

            comb_language.DataSource = LanguageManager.LanguageMap.ToList();
            comb_language.DisplayMember = "Value"; // 顯示語言名稱
            comb_language.ValueMember = "Key";     // 實際語系代碼
            string lang = Properties.Settings.Default.LanguageSetting;
            comb_language.SelectedValue = lang;
            LanguageManager.LoadLanguageFromDatabase(lang);

            SwitchLanguage();


        }


        private void SwitchLanguage()
        {

            btn_Main.Text = LanguageManager.Translate("Mainform_main");
            btn_search.Text = LanguageManager.Translate("Mainform_Troubleshooting");
            btn_email.Text = LanguageManager.Translate("Mainform_EmailSetting");
            btn_connect.Text = LanguageManager.Translate("Mainform_Connect");
            btn_log_out.Text = LanguageManager.Translate("Mainform_Logout");
            btn_language.Text = LanguageManager.Translate("Mainform_language");
            btn_setting.Text = LanguageManager.Translate("Mainform_Settings");
            this.Text = LanguageManager.Translate("Mainform_title");
            for (int i = 0; i < machineButtons.Count && i < machineButtons.Count; i++)
            {
                string displayName = LanguageManager.Translate(machineButtons[i].Name);

                MachinePanelGroup.UpdateButtonLabel(machineButtons[i], displayName);
            }


        }

        private void btn_language_Click(object sender, EventArgs e)
        {
            if (comb_language.SelectedValue is string selectedLang)
            {
                //LanguageManager.LoadLanguageFromDatabase(selectedLang);
                //Properties.Settings.Default.LanguageSetting = selectedLang;
                //Properties.Settings.Default.Save(); // ✅ 寫入設定檔

                LanguageManager.SetLanguage(selectedLang); // ✅ 自動載入 + 儲存 + 觸發事件

                SwitchLanguage();
                InitLanguageComboBox();
            }
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            var form = new Setting();

            panel_select.Controls.Clear(); // 清空 Panel
                                           // 設置子窗體屬性以嵌入 Panel
            form.TopLevel = false; // 禁止作為獨立窗口
            form.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            form.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            panel_main.Controls.Clear(); // 清空 Panel
            panel_main.Controls.Add(form); // 添加子窗體
            form.Show(); // 顯示子窗體
            //DisplayLanguage();
        }

        private bool _loginDone = false;
        private void Main_Shown(object sender, EventArgs e)
        {
            // 避免多次觸發（例如重新顯示/Activate）
            if (_loginDone) return;
            _loginDone = true;

            // 先禁用整個主視窗互動，避免使用者在未授權時亂點
            this.Enabled = false;

            // 若主畫面有背景初始化（例如載入儀表/佈局），可先 await 完成
            // await InitUiAsync();

            bool loggedIn = ShowLoginLoop(); // 成功才會回傳 true

            if (!loggedIn)
            {
                // 使用者取消且不重試 → 關閉主視窗（結束應用）
                this.Close();
                return;
            }

            // ✅ 登入成功，開放互動
            this.Enabled = true;
        }
        /// <summary>
        /// 顯示登入對話框；回傳是否登入成功。
        /// 失敗時提供重試，否則關閉。
        /// </summary>
        private bool ShowLoginLoop()
        {
            while (true)
            {
                using (var loginForm = new UserLoginForm())
                {
                    var result = loginForm.ShowDialog();
                    if (result == DialogResult.OK && loginForm.CurrentUser != null)
                    {
                        var CurrentUser = loginForm.CurrentUser;
                        return true; // ✅ 登入成功
                    }
                }

                // 失敗或關閉，詢問是否重試
                var retry = MessageBox.Show(
                    LanguageManager.Translate("User_Login_Form_Message"),
                    LanguageManager.Translate("User_Login_Form_hint"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (retry == DialogResult.No)
                {
                    return false; // ❌ 不重試
                }
            }
        }

    }
}
