using SLMP;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Login;

using static FX5U_IOMonitor.connect_PLC;
using System.Windows.Forms;
using static FX5U_IOMonitor.Models.Csv2Db;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;
using System.Globalization;
using FX5U_IOMonitor.Resources;
using FX5U_IOMonitor.panel_control;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.PortableExecutable;




namespace FX5U_IOMonitor
{
    public partial class Main : Form
    {
        private connect_PLC plcForm; // 連接介面

        private Drill_choose Drill_setting; //鑽床監控轉換介面
        private swing_setting Swing_setting;
        private Swing_main swing_main;
        private Drill_main drill_main;
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

        public Main()
        {

            InitializeComponent();

            //SlmpConfig cfg = new("192.168.9.1", 2000);
            //cfg.ConnTimeout = 3000;
            //SlmpClient plc = new(cfg);
            //plc.Connect();
            //string dbtable = DBfunction.FindTableWithAddress("L0");
            //if (dbtable == "") return;

            //// 移除警告通知時間更新
            //DBfunction.SetCurrentTimeAsUnmountTime(dbtable, "L0");

            //// 寫入警告通知進歷史資料
            //DBfunction.SetCurrentTimeAsMountTime(dbtable, "L0");
            //DBfunction.SetAlarmStartTime(dbtable, "L0","alarm");

            //// 寫入警告移除時間進歷史資料
            //DBfunction.SetAlarmEndTime(dbtable, "L0");
            //DBfunction.SetCurrentTimeAsUnmountTime(dbtable, "L0");


            // 寫入警告歷史資料
            //DBfunction.SetMachineIOToHistory(dbtable, "L0", "alarm");




            _instance = this;  // 確保單例指向目前的主視窗

            plcForm = new connect_PLC(this);
            Drill_setting = new Drill_choose(this);
            Swing_setting = new swing_setting(this);
            swing_main = new Swing_main(this);
            drill_main = new Drill_main(this);
            search_main = new Search_main();
            this.Shown += MainForm_Shown;

            btn_search.Enabled = false;
            //btn_connect.Enabled = false;


            // 實體元件資料初始化(excel轉DB)
            //string filePath = @"Drill_Data.csv";
            //List<IO_DataBase> Drill_DataList = Csv2Db.Initiali_Data(filePath); //資料初始化→資料庫初始化
            //string filePath2 = @"Swing_Data.csv";
            //List<IO_DataBase> Swing_DataList = Csv2Db.Initiali_Data(filePath2); //資料初始化→資料庫初始化
            //using (var context = new ApplicationDB())
            //{
            //    context.Database.EnsureCreated();
            //}
            //using (var context = new ApplicationDB())
            //{
            //    var allAlarms = context.Drill_IO.ToList(); // 取得所有資料
            //    context.Drill_IO.RemoveRange(allAlarms);   // 移除全部

            //    context.SaveChanges();                   // 寫入資料庫
            //}
            //UpdateData.SaveMachineIODb(Drill_DataList, "Drill");

            //using (var context = new ApplicationDB())
            //{
            //    var allAlarms = context.alarm.ToList(); // 取得所有資料
            //    context.alarm.RemoveRange(allAlarms);   // 移除全部
            //    context.SaveChanges();                   // 寫入資料庫
            //}
            //// 警告資料初始化(excel轉DB)
            //AlarmImporter.ImportFromCSV("arlam.csv");


            DBfunction.Initiali_current_single();

            connect_isOK.Drill_total.disconnect = DBfunction.GetTableRowCount("Drill");
            connect_isOK.Swing_total.disconnect = DBfunction.GetTableRowCount("Sawing");

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

            panel_select.Controls.Add(btn_user);
            panel_select.Controls.Add(btn_log_in);
            panel_select.Controls.Add(btn_log_out);


            btn_user.Visible = true;
            btn_log_in.Visible = true;


        }


        private void btn_Drill_Click(object sender, EventArgs e)
        {
            // 設置子窗體屬性以嵌入 Panel
            Drill_setting.TopLevel = false; // 禁止作為獨立窗口
            Drill_setting.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            Drill_setting.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            //panel_select.Controls.Clear(); // 清空 Panel
            //panel_select.Controls.Add(Drill_setting); // 添加子窗體
            //Drill_setting.Show(); // 顯示子窗體
            Drill_setting.UpdateTotal_label();
            Drill_setting.Updateconnect_label(DataStore.Drill_connect_Summary);

            //if( connect_PLC.connect_isOK.Ethernet.isOK == false ) {
            //	int totalCount = DataStore.Drill_connect_Summary.connect + DataStore.Drill_connect_Summary.disconnect;
            //	var isconnect = new connect_Summary
            //	{
            //		total_number = totalCount
            //	};

            //	DataStore.Drill_connect_Summary = isconnect;
            //	Drill_setting.Updateconnect_label( DataStore.Drill_connect_Summary );
            //}
            //else { return; }

            // 設置子窗體屬性以嵌入 Panel
            drill_main.TopLevel = false; // 禁止作為獨立窗口
            drill_main.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            drill_main.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            panel_main.Controls.Clear(); // 清空 Panel
            panel_main.Controls.Add(drill_main); // 添加子窗體
            drill_main.Show(); // 顯示子窗體

        }







        private void Main_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }



        private void lb_connect_Click(object sender, EventArgs e)
        {

        }

        private void button_swing_Click(object sender, EventArgs e)
        {
            // 設置子窗體屬性以嵌入 Panel
            Swing_setting.TopLevel = false; // 禁止作為獨立窗口
            Swing_setting.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            Swing_setting.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            //panel_select.Controls.Clear(); // 清空 Panel
            //panel_select.Controls.Add(Swing_setting); // 添加子窗體
            //Swing_setting.Show(); // 顯示子窗體
            Swing_setting.UpdateTotal_label();
            Swing_setting.Updateconnect_label(DataStore.Swing_connect_Summary);
            //if( connect_PLC.connect_isOK.Ethernet.isOK == false ) {
            //	int totalCount = DataStore.Swing_connect_Summary.connect + DataStore.Swing_connect_Summary.disconnect;
            //	var isconnect = new connect_Summary
            //	{
            //		total_number = totalCount
            //	};

            //	DataStore.Swing_connect_Summary = isconnect;
            //	Drill_setting.Updateconnect_label( DataStore.Swing_connect_Summary);
            //}
            //else { return; }

            // 設置子窗體屬性以嵌入 Panel
            swing_main.TopLevel = false; // 禁止作為獨立窗口
            swing_main.FormBorderStyle = FormBorderStyle.None; // 移除邊框
            swing_main.Dock = DockStyle.Fill; // 填滿 Panel

            // 將子窗體添加到 Panel 並顯示
            panel_main.Controls.Clear(); // 清空 Panel
            panel_main.Controls.Add(swing_main); // 添加子窗體
            swing_main.Show(); // 顯示子窗體



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

        private void btn_log_in_Click(object sender, EventArgs e)
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
                        //btn_connect.Enabled = true;

                    }
                    else if (UserService<ApplicationDB>.CurrentRole == SD.Role_Operator)
                    {

                    }
                    else if (UserService<ApplicationDB>.CurrentRole == SD.Role_User)
                    {

                    }
                    LoginSucceeded?.Invoke(this, EventArgs.Empty);

                    MessageBox.Show($"{ResMapper.GetLocalizedString("MainForm::Msg::Welcome")} {UserService<ApplicationDB>.CurrentRole}: {UserService<ApplicationDB>.CurrentUser.UserName}");
                }
            }
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

            using (var form = new Email_Settings())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);

            }
        }
    }
}
