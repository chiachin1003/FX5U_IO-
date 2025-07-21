using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Email;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Resources;
using FX5U_IO元件監控;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using static FX5U_IOMonitor.Data.Recordmode;
using static FX5U_IOMonitor.Email.Notify_Message;
using static FX5U_IOMonitor.Models.Csv2Db;
using static FX5U_IOMonitor.Models.MonitorFunction;
using static FX5U_IOMonitor.Models.UI_Display;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;


namespace FX5U_IOMonitor
{
    public partial class Main : Form
    {
        private Connect_PLC plcForm; // 連接介面
        private Search_main search_main;
        private Main_form main_Form;
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

        public Main()
        {

            InitializeComponent();


            try
            {
                Initialization_BladeTPIFromCSV("Blade_brand_TPI.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Blade TPI 初始化失敗：{ex.Message}", "初始化錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            try
            {
                Initialization_BladeBrandFromCSV("Blade_brand.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Blade Brand 初始化失敗：{ex.Message}", "初始化錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            try
            {
                Initialization_AlarmFromCSV("alarm.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarm 初始化失敗：{ex.Message}", "初始化錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }



            try
            {
                Initialization_MachineprameterFromCSV("Machine_monction_data.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Machine parameter 初始化失敗：{ex.Message}", "初始化錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

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
                        MessageBox.Show("✅ 機台資料匯入完成。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Machine_IO 初始化或檢查失敗：{ex.Message}", "初始化錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                panel_choose.Controls.SetChildIndex(btn, indexBelowMain); // ✅ 固定插在原本 btn_Main 的位置
            }

            InitLanguageComboBox();


            _instance = this;  // 確保單例指向目前的主視窗
            plcForm = new Connect_PLC(this);
            Connect_PLC.AutoConnectAllMachines(plcForm); //自動連線

            search_main = new Search_main();

            this.Shown += MainForm_Shown;

            //DBfunction.Initiali_current_single();

            main_Form = new Main_form();
            main_Form.TopLevel = false; // 禁止作為獨立窗口
            main_Form.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            main_Form.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            panel_main.Controls.Clear(); // 清空 Panel
            panel_main.Controls.Add(main_Form); // 添加子窗體
            main_Form.Show(); // 顯示子窗體


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



        private void Main_Load(object sender, EventArgs e)
        {
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
            using (var userService = new UserService<ApplicationDB>())
            {
                await userService.CreateDefaultUserAsync();
            }

        }


        /// <summary>
        /// 登入機能的設定尚未完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void log_in_Setting()
        {
            using (var form = new UserLoginForm())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
                if (result == DialogResult.OK)
                {


                    if (UserService<ApplicationDB>.CurrentRole == SD.Role_Admin)
                    {
                        btn_search.Enabled = true;

                    }
                    else if (UserService<ApplicationDB>.CurrentRole == SD.Role_Operator)
                    {

                    }
                    else if (UserService<ApplicationDB>.CurrentRole == SD.Role_User)
                    {

                    }
                    LoginSucceeded?.Invoke(this, EventArgs.Empty);

                    MessageBox.Show($"{LanguageManager.Translate("User_Login_Welcome")} {UserService<ApplicationDB>.CurrentRole}: {UserService<ApplicationDB>.CurrentUser.UserName}");
                }
            }
        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            using (var userService = new UserService<ApplicationDB>())
            {
                userService.Logout();
            }
            LogoutSucceeded?.Invoke(this, EventArgs.Empty);

            this.Hide();            // 隱藏當前主畫面

            bool loginSucceeded = false;

            while (!loginSucceeded)
            {
                var loginForm = new UserLoginForm();
                var result = loginForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    loginSucceeded = true;
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



        private void InitLanguageComboBox()
        {
            comb_language.DataSource = new BindingSource(LanguageManager.LanguageMap, null);
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

      

       
    }
}
