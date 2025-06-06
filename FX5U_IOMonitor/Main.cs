using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Resources;
using FX5U_IO元件監控;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Windows.Forms;
using static FX5U_IOMonitor.Models.Csv2Db;
using static FX5U_IOMonitor.Models.MonitorFunction;


namespace FX5U_IOMonitor
{
    public partial class Main : Form
    {
        private Connect_PLC plcForm; // 連接介面
        private Search_main search_main;
        private Main_form main_Form;
        public event EventHandler? LoginSucceeded;
        public event EventHandler? LogoutSucceeded;


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
            InitLanguageComboBox();


            //using (var form = new UserLoginForm())
            //{
            //    form.StartPosition = FormStartPosition.CenterParent;
            //    var result = form.ShowDialog(this);
            //    if (result == DialogResult.OK)
            //    {


            //        if (UserService<ApplicationDB>.CurrentRole == SD.Role_Admin)
            //        {
            //            btn_search.Enabled = true;
            //            //btn_connect.Enabled = true;

            //        }
            //        else if (UserService<ApplicationDB>.CurrentRole == SD.Role_Operator)
            //        {

            //        }
            //        else if (UserService<ApplicationDB>.CurrentRole == SD.Role_User)
            //        {

            //        }
            //        LoginSucceeded?.Invoke(this, EventArgs.Empty);

            //        MessageBox.Show($"{LanguageManager.Translate("UserManageForm_Msg_Welcome")} {UserService<ApplicationDB>.CurrentRole}: {UserService<ApplicationDB>.CurrentUser.UserName}");
            //    }
            //}

            InitMachineInfoDatabase();
            Initialization_BladeTPIFromCSV("鋸帶齒數 ID 定義.csv");
            Initialization_BladeBrandFromCSV("鋸帶廠牌、材質 ID 定義.csv");
            Initialization_AlarmFromCSV("alarm.csv");
            Initialization_MachineprameterFromCSV("Machine_monction_data.csv");

            // 檢查是否已初始化
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
                    Debug.WriteLine($"❌ 機台已初始化，請重新命名後再匯入。");
                }
                else
                {
                    Csv2Db.Initialization_MachineElementFromCSV("Drill", "Drill_Data.csv");
                    Csv2Db.Initialization_MachineElementFromCSV("Sawing", "Sawing_Data.csv");
                }
            }

            _instance = this;  // 確保單例指向目前的主視窗
            plcForm = new Connect_PLC(this);
            search_main = new Search_main();

            this.Shown += MainForm_Shown;

            DBfunction.Initiali_current_single();

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
            panel_select.Controls.Clear();
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
        {
            // 清空 Panel 的內容
            panel_main.Controls.Clear();
            panel_select.Controls.Clear();

            main_Form.TopLevel = false; // 禁止作為獨立窗口
            main_Form.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            main_Form.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            panel_main.Controls.Clear(); // 清空 Panel
            panel_main.Controls.Add(main_Form); // 添加子窗體
            main_Form.Show(); // 顯示子窗體
            panel_select.Visible = true;  // 🔴 隱藏整個 panel_select


            panel_language.Visible = true;
            panel_select.Controls.Add(panel_language);

            panel_select.Controls.Add(btn_user);
            panel_select.Controls.Add(btn_log_in);
            panel_select.Controls.Add(btn_log_out);

            panel_language.Controls.Add(comb_language);
            panel_language.Controls.Add(btn_language);


            btn_user.Visible = true;
            btn_log_in.Visible = true;
            panel_language.Visible = true;
        }


        private void btn_Drill_Click(object sender, EventArgs e)
        {

            string machine = "Drill";
            var form = Machine_main.GetInstance(machine); // ✅ 呼叫單例

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panel_main.Controls.Clear();
            panel_main.Controls.Add(form);
            form.Show();
        }



        private void Main_Load(object sender, EventArgs e)
        {
        }



        private void lb_connect_Click(object sender, EventArgs e)
        {

        }

        private void button_swing_Click(object sender, EventArgs e)
        {
            string machine = "Sawing";
            var form = Machine_main.GetInstance(machine); // ✅ 呼叫單例

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panel_main.Controls.Clear();
            panel_main.Controls.Add(form);
            form.Show();

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




        private void btn_user_Click(object sender, EventArgs e)
        {
            using (var form = new UserManageForm())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);

            }
        }

        /// <summary>
        /// 登入機能的設定尚未完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_log_in_Click(object sender, EventArgs e)
        {
            //using (var form = new UserLoginForm())
            //{
            //    form.StartPosition = FormStartPosition.CenterParent;
            //    var result = form.ShowDialog(this);
            //    if (result == DialogResult.OK)
            //    {


            //        if (UserService<ApplicationDB>.CurrentRole == SD.Role_Admin)
            //        {
            //            btn_search.Enabled = true;
            //            //btn_connect.Enabled = true;

            //        }
            //        else if (UserService<ApplicationDB>.CurrentRole == SD.Role_Operator)
            //        {

            //        }
            //        else if (UserService<ApplicationDB>.CurrentRole == SD.Role_User)
            //        {

            //        }
            //        LoginSucceeded?.Invoke(this, EventArgs.Empty);

            //        MessageBox.Show($"{ResMapper.GetLocalizedString("MainForm::Msg::Welcome")} {UserService<ApplicationDB>.CurrentRole}: {UserService<ApplicationDB>.CurrentUser.UserName}");
            //    }
            //}
        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            using (var userService = new UserService<ApplicationDB>())
            {
                userService.Logout();
            }
            btn_search.Enabled = false;
            btn_connect.Enabled = false;
            LogoutSucceeded?.Invoke(this, EventArgs.Empty);

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

        void InitMachineInfoDatabase()
        {
            using (var context = new ApplicationDB())
            {
                context.Database.EnsureCreated();
            }
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

            btn_log_in.Text = LanguageManager.Translate("Mainform_UserLogin");
            btn_Main.Text = LanguageManager.Translate("Mainform_main");
            btn_Drill.Text = LanguageManager.Translate("Mainform_Drill");
            button_swing.Text = LanguageManager.Translate("Mainform_Saw");
            btn_search.Text = LanguageManager.Translate("Mainform_Troubleshooting");
            btn_email.Text = LanguageManager.Translate("Mainform_EmailSetting");
            btn_connect.Text = LanguageManager.Translate("Mainform_Connect");
            btn_user.Text = LanguageManager.Translate("Mainform_Permission");
            btn_log_out.Text = LanguageManager.Translate("Mainform_Logout");
            btn_language.Text = LanguageManager.Translate("Mainform_language");
            btn_setting.Text = LanguageManager.Translate("Mainform_Settings");

            this.Text = LanguageManager.Translate("Mainform_title");

        }

        private void btn_language_Click(object sender, EventArgs e)
        {
            if (comb_language.SelectedValue is string selectedLang)
            {
                LanguageManager.LoadLanguageFromDatabase(selectedLang);
                Properties.Settings.Default.LanguageSetting = selectedLang;
                Properties.Settings.Default.Save(); // ✅ 寫入設定檔
                SwitchLanguage();
            }
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            //using (var form = new Email_Settings())
            //{
            //    form.StartPosition = FormStartPosition.CenterParent;
            //    var result = form.ShowDialog(this);
            //}

            using (var form = new File_Settings())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }
        }
    }
}
