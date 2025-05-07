using CsvHelper.Configuration;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;

using System.Windows.Forms;
using static FX5U_IOMonitor.connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using static System.Windows.Forms.AxHost;


namespace FX5U_IOMonitor
{

    public partial class Drill_main : Form
    {

        Main main;

        public Drill_main(Main main)
        {

            InitializeComponent();
            tableLayoutPanel1.BorderStyle = BorderStyle.FixedSingle; // 單線框
            tableLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel3.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel4.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel5.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel6.BorderStyle = BorderStyle.FixedSingle;

        }



        private void btn_SP1_Click(object sender, EventArgs e)
        {

            List<string> search = DBfunction.GetAllClassTags("Drill", "SP1");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }

        private void btn_SP2_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "SP2");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_SP3_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "SP3");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_SP4_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "SP4");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_common_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "COMMON");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_Panel_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "Panel");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_Peripheral_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "Peripheral");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_Cabinet_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "Cabinet");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
          
        }

        private void button11_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "Infeed_PNL");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }

        private void btn_Infeed_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "INFEED");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_InfeedBox_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "Infeed_BOX");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_Outfeed_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "OUTFEED");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_OutfeedBox_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "Outfeed_box");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_OutfeedPnl_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetAllClassTags("Drill", "Outfeed_PNL");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }




        private void Drill_main_Load(object sender, EventArgs e)
        {

            reset_labText();
            //輸入當前顯示的不同數值
            List<float[]> chartValues = update_class();


            // 總數量
            int totalCharts = 14;
            int chartsPerRow = 7;
            int spacingX = 130, spacingY = 220;
            int startX = 35, startY = 40;
            int lab_start_x = 30 ,lab_start_y = 150;

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
                PictureBox chartPanel = panel_design.CreateDoughnutChartPanel(110,
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

            Monitor_alarm();
            //if (connect_isOK.Drill_connect == true)
            //{
            //    //開啟監控
            //    var DB_update = MonitorHub.GetMonitor("Drill");
            //    DB_update.alarm_event += Warning_signs;
            //}

        }
        private bool isEventRegistered = false;
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
                        var DB_update = MonitorHub.GetMonitor("Drill");
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
                        else if (!connected && isEventRegistered )
                        {
                            DB_update.alarm_event -= Warning_signs;
                            isEventRegistered = false;
                        }
                       
                    });
                }
            });
        }

        private void lab_red_Click(object sender, EventArgs e)
        {
            List<string> alarms = DBfunction.Get_Red_addressList("Drill");

            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(alarms, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
            //OpenSearch(alarms);

        }
        private void reset_labText()//更新主頁面連接狀況
        {


            lab_green.Text = DBfunction.Get_Green_number("Drill").ToString();
            lab_yellow.Text = DBfunction.Get_Yellow_number("Drill").ToString();
            lab_red.Text = DBfunction.Get_Red_number("Drill").ToString();
            lab_sum.Text = DBfunction.GetTableRowCount("Drill").ToString();


            if (connect_isOK.Drill_connect == false)
            {
                lab_connectOK.Text = "未連接";
                lab_connectOK.ForeColor = Color.Red;

                lab_connect.Text = "0";
                lab_partalarm.Text = "0";

                //lab_partalarm.Text = DataStore.Drill_DataList.Count.ToString();
            }
            else
            {
                lab_connectOK.Text = "已連接";
                lab_connectOK.ForeColor = Color.Green;

                lab_connect.Text = connect_isOK.Drill_total.connect.ToString();
                List<string> drill_breakdowm_part = DBfunction.Get_breakdown_part("Drill");
                lab_partalarm.Text = DBfunction.Get_address_ByBreakdownParts("Drill", drill_breakdowm_part).Count.ToString(); ;
            }


        }
        private List<float[]> update_class()
        {
            List<string> classTags = new List<string>
            {
                "SP1", "SP2", "SP3", "SP4", "COMMON", "Panel","Peripheral",
                "Cabinet", "INFEED", "Infeed_BOX", "Infeed_PNL", "OUTFEED", "Outfeed_PNL", "Outfeed_box"
            };
            List<float[]> chartValues = new List<float[]>();


            foreach (string classTag in classTags)
            {

                List<string> search_number = DBfunction.GetAllClassTags("Drill", classTag);

                int Green = DBfunction.Get_Green_classnumber("Drill", classTag, search_number);
                int yellow = DBfunction.Get_Yellow_classnumber("Drill", classTag, search_number);
                int red = DBfunction.Get_Red_classnumber("Drill", classTag, search_number);

                // 轉換為 float[]
                chartValues.Add(new float[] { Green, yellow, red });

            }
            return chartValues;
        }

        private void lab_yellow_Click(object sender, EventArgs e)
        {

            List<string> warn = DBfunction.Get_Yellow_addressList("Drill");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(warn, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }

        private void lab_green_Click(object sender, EventArgs e)
        {
            List<string> green = DBfunction.Get_Green_addressList("Drill");

            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(green, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
            //OpenSearch(green);
        }

        private void lab_connect_Click(object sender, EventArgs e)
        {
            if (connect_isOK.Drill_connect == false)
            {
                MessageBox.Show("未連線");

            }
            else
            {
                MessageBox.Show(connect_isOK.Swing_total.read_time);

            }
        }


        private void Drill_main_VisibleChanged(object sender, EventArgs e)
        {
            reset_labText();

        }

        private void lab_sum_Click(object sender, EventArgs e)
        {
            if (connect_isOK.Drill_connect == false)
            {
                MessageBox.Show("當前無資料更新");
                reset_labText();


            }
            else
            {
                MessageBox.Show(connect_isOK.Drill_total.read_time);
                reset_labText();

            }
        }

        private void Warning_signs(object? sender, IOUpdateEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Warning_signs(sender, e)));
                return;
            }

            if (e.NewValue == true && e.OldValue == false)
            {
                // 顯示變化
                MessageBox.Show($"📡 偵測到 I/O 變化：{e.Address} from {e.OldValue} ➜ {e.NewValue}");

                // 查出這個 address 對應的 Description
                string des = DBfunction.Get_Description_ByAddress(e.Address);

                // 比對查出 Alarm 表中對應的 address & table（Drill/Swing）
                (string matchedAddress, string table) = DBfunction.FindIOByAlarmDescription(des);

                if (!string.IsNullOrEmpty(matchedAddress) && !string.IsNullOrEmpty(table))
                {
                    string Possible = DBfunction.Get_Possible_ByAddress(e.Address);
                    string error = DBfunction.Get_Error_ByDescription(des);
                    string comment = DBfunction.Get_Comment_ByAddress(table, matchedAddress);

                    MessageBox.Show(
                        $"⚠️ 錯誤警告\n來源：{table} | 位址：{matchedAddress}\n料件：{des}\n錯誤訊息：{error}\n描述：{comment}\n可能原因：{Possible}",
                        "I/O 錯誤偵測",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    MessageBox.Show($"⚠ 找不到對應 Description：{des} 的 Drill 或 Swing 資料。");
                }
            }



        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label_txt_Click(object sender, EventArgs e)
        {

        }
        string searchText;

        private void btn_search_Click(object sender, EventArgs e)
        {
            searchText = txB_search.Text.Trim();

            List<string> search_data = DBfunction.Search_IOFromDB("Drill", searchText);
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search_data, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void lab_partalarm_Click(object sender, EventArgs e)
        {
            if (connect_isOK.Drill_connect == true)
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
    }
}



