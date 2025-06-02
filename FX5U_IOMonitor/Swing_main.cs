using CsvHelper.Configuration;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using System.Data;
using System.Windows.Forms;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Data.GlobalMachineHub;
using static FX5U_IOMonitor.Models.MonitoringService;

namespace FX5U_IOMonitor
{

    public partial class Swing_main : Form
    {

        public Swing_main(Main main)
        {

            InitializeComponent();
            tableLayoutPanel1.BorderStyle = BorderStyle.FixedSingle; // 單線框
            tableLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel3.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel4.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel5.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel6.BorderStyle = BorderStyle.FixedSingle;
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageCSV("language.csv", lang);
            SwitchLanguage();
            LanguageManager.LanguageChanged += OnLanguageChanged;

        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
        }

        private void btn_common_Click(object sender, EventArgs e)
        {
            List<string> classtag = DBfunction.GetClassTag_address("Sawing", "COMMON");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(classtag, "Sawing");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }



        private void btn_panel_Click(object sender, EventArgs e)
        {
            List<string> classtag = DBfunction.GetClassTag_address("Sawing", "Panel");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(classtag, "Sawing");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_cabinet_Click(object sender, EventArgs e)
        {
            List<string> classtag = DBfunction.GetClassTag_address("Sawing", "CABINET");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(classtag, "Sawing");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void lab_red_Click(object sender, EventArgs e)
        {
            List<string> alarms = DBfunction.Get_Red_addressList("Sawing");

            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(alarms, "Sawing");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }

        private void reset_labText()//更新主頁面連接狀況
        {
            lab_green.Text = DBfunction.Get_Green_number("Sawing").ToString();
            lab_yellow.Text = DBfunction.Get_Yellow_number("Sawing").ToString();
            lab_red.Text = DBfunction.Get_Red_number("Sawing").ToString();
            lab_sum.Text = DBfunction.GetMachineRowCount("Sawing").ToString();

            var existingContext = GlobalMachineHub.GetContext("Sawing") as IMachineContext;
            //var existingContext = MachineHub.Get("Sawing");
            if (existingContext != null && existingContext.IsConnected)
            {
                lab_connectOK.Text = "已連接";
                lab_connectOK.ForeColor = Color.Green;

                lab_connect.Text = existingContext.ConnectSummary.connect.ToString();
                List<string> drill_breakdowm_part = DBfunction.Get_breakdown_part("Sawing");
                lab_partalarm.Text = DBfunction.Get_address_ByBreakdownParts("Sawing", drill_breakdowm_part).Count.ToString();

            }
            else
            {

                lab_connectOK.Text = "未連接";
                lab_connectOK.ForeColor = Color.Red;

                lab_connect.Text = "0";
                lab_partalarm.Text = "0";
            }

        }
        private List<float[]> update_class()
        {
            List<string> classTags = new List<string>
            {
                "COMMON", "Panel", "CABINET"
            };
            List<float[]> chartValues = new List<float[]>();
            foreach (string classTag in classTags)
            {
                // 計算當前 ClassTag 的狀態數據
                List<string> search_number = DBfunction.GetClassTag_address("Sawing", classTag);

                int Green = DBfunction.Get_Green_classnumber("Sawing", classTag, search_number);
                int yellow = DBfunction.Get_Yellow_classnumber("Sawing", classTag, search_number);
                int red = DBfunction.Get_Red_classnumber("Sawing", classTag, search_number);

                // 轉換為 float[]
                chartValues.Add(new float[] { Green, yellow, red });
            }
            return chartValues;
        }

        private void lab_yellow_Click(object sender, EventArgs e)
        {
            List<string> warn = DBfunction.Get_Yellow_addressList("Sawing");

            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(warn, "Sawing");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }

        private void lab_green_Click(object sender, EventArgs e)
        {
            List<string> green = DBfunction.Get_Green_addressList("Sawing");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(green, "Sawing");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void Swing_main_Load(object sender, EventArgs e)
        {


            reset_labText();
            //輸入當前顯示的不同數值
            List<float[]> chartValues = update_class();
            // 總數量
            int totalCharts = 3;
            int chartsPerRow = 3;
            int spacingX = 300, spacingY = 0;
            int startX = 106, startY = 160;
            int lab_start_x = 106, lab_start_y = 300;

            for (int i = 0; i < totalCharts; i++)
            {
                int row = i / chartsPerRow;
                int col = i % chartsPerRow;

                int x = startX + (col * spacingX);
                int y = startY + (row * spacingY);

                // **使用預定義的數據**
                float[] values = chartValues[i];
                int[] number = new int[values.Length];

                for (int j = 0; j < values.Length; j++)
                {
                    number[j] = Convert.ToInt32(values[j]);  // 會四捨五入
                }

                PictureBox chartPanel = panel_design.CreateDoughnutChartPanel(120,
                                       values,
                                       new Color[] { Color.LightGreen, Color.Yellow, Color.Red });

                chartPanel.Location = new Point(x, y);
                this.Controls.Add(chartPanel);

                int table_x = lab_start_x + (col * spacingX);
                int table_y = lab_start_y + (row * spacingY);

                TableLayoutPanel tableLayoutPanel = panel_design.CreateColorLegendPanel(number[0].ToString(), number[1].ToString(), number[2].ToString());
                tableLayoutPanel.Location = new Point(table_x, table_y);
                this.Controls.Add(tableLayoutPanel);
            }
            //Monitor_alarm();

        }

        private void lab_connect_Click(object sender, EventArgs e)
        {

          


        }

        private void Swing_main_VisibleChanged(object sender, EventArgs e)
        {
            reset_labText();
        }
        string searchText;
        private void btn_search_Click(object sender, EventArgs e)
        {
            searchText = txB_search.Text.Trim();
            List<string> search_data = DBfunction.Search_IOFromDB("Sawing", searchText);
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search_data, "Sawing");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void lab_partalarm_Click(object sender, EventArgs e)
        {
            var existingContext = MachineHub.Get("Drill");
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
        bool isEventRegistered = false;
        private void Monitor_alarm()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000); // 每秒檢查一次

                    bool connected = connect_isOK.Drill_connect;
                    this.Invoke(() =>
                    {
                        var DB_update = MachineHub.GetMonitor("Drill");

                        //var DB_update = MonitorHub.GetMonitor("Drill");
                        if (DB_update == null)
                        {
                            Console.WriteLine("⚠️ MonitorHub 尚未註冊 Drill 監控對象");
                            return;
                        }

                        if (connected && !isEventRegistered)
                        {
                            DB_update.alarm_event += Warning_signs;
                            isEventRegistered = true;
                        }
                        else if (!connected && isEventRegistered)
                        {
                            DB_update.alarm_event -= Warning_signs;
                            isEventRegistered = false;
                        }

                    });
                }
            });
        }

        private void Warning_signs(object? sender, IOUpdateEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Warning_signs(sender, e)));
                return;
            }
            reset_labText();
            List<string> breakdowm_part = DBfunction.Get_breakdown_part("Sawing");
            lab_partalarm.Text = DBfunction.Get_address_ByBreakdownParts("Sawing", breakdowm_part).Count.ToString();
            //if (e.NewValue == true && e.OldValue == false)
            //{
            //    // 顯示變化
            //    MessageBox.Show($"📡 偵測到 I/O 變化：{e.Address} from {e.OldValue} ➜ {e.NewValue}");

            //    // 查出這個 address 對應的 Description
            //    string des = DBfunction.Get_Description_ByAddress(e.Address);

            //    // 比對查出 Alarm 表中對應的 address & table（Drill/Swing）
            //    (string matchedAddress, string table) = DBfunction.FindIOByAlarmDescription(des);

            //    if (!string.IsNullOrEmpty(matchedAddress) && !string.IsNullOrEmpty(table))
            //    {
            //        string Possible = DBfunction.Get_Possible_ByAddress(e.Address);
            //        string error = DBfunction.Get_Error_ByDescription(des);
            //        string comment = DBfunction.Get_Comment_ByAddress(table, matchedAddress);

            //        MessageBox.Show(
            //            $"⚠️ 錯誤警告\n來源：{table} | 位址：{matchedAddress}\n料件：{des}\n錯誤訊息：{error}\n描述：{comment}\n可能原因：{Possible}",
            //            "I/O 錯誤偵測",
            //            MessageBoxButtons.OK,
            //            MessageBoxIcon.Warning
            //        );
            //    }
            //    else
            //    {
            //        MessageBox.Show($"⚠ 找不到對應 Description：{des} 的 Drill 或 Swing 資料。");
            //    }
            //}



        }

        private void lab_sum_Click(object sender, EventArgs e)
        {

            var context = GlobalMachineHub.GetContext("Sawing") as IMachineContext;

            if (context != null && context.IsConnected)
            {
                MessageBox.Show("當前監控總數更新時間：" + context.ConnectSummary.read_time.ToString());
            }
            else
            {
                MessageBox.Show("當前無資料監控與更新");
            }

        }

        private void SwitchLanguage()
        {
           
            btn_common.Text = LanguageManager.Translate("Drillmain_common");
            btn_cabinet.Text = LanguageManager.Translate("Drillmain_Cabinet");
            btn_panel.Text = LanguageManager.Translate("Drillmain_Panel");

            label1.Text = LanguageManager.Translate("Mainform_RedLights");
            label2.Text = LanguageManager.Translate("Mainform_YellowLights");
            label3.Text = LanguageManager.Translate("Mainform_GreenLights");
            label4.Text = LanguageManager.Translate("Mainform_ComponentFaults");
            label5.Text = LanguageManager.Translate("Mainform_Connections");
            label6.Text = LanguageManager.Translate("Mainform_MonitoredItems");
            label_txt.Text = LanguageManager.Translate("Mainform_TextSearch");
            btn_search.Text = LanguageManager.Translate("Mainform_Search");

        }
    }
}



