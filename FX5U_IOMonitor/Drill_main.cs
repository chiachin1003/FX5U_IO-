using CsvHelper.Configuration;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;

using System.Windows.Forms;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Data.GlobalMachineHub;
using static FX5U_IOMonitor.Models.MonitoringService;
using static System.Windows.Forms.AxHost;


namespace FX5U_IOMonitor
{

    public partial class Drill_main : Form
    {

        Main main;
        private CancellationTokenSource? _cts;
        private string machine;

        public Drill_main(Main main)
        {

            InitializeComponent();
            tableLayoutPanel1.BorderStyle = BorderStyle.FixedSingle; // 單線框
            tableLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel3.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel4.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel5.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel6.BorderStyle = BorderStyle.FixedSingle;
            this.Load += Drill_main_Load;
            this.FormClosing += Drill_FormClosing;
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            SwitchLanguage();
            LanguageManager.LanguageChanged += OnLanguageChanged;

        }
        private void OnLanguageChanged(string cultureName)
        {
            reset_labText();
            SwitchLanguage();
        }
        private void Drill_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _cts?.Cancel(); // 關閉時自動取消背景任務
        }
        private async Task AutoUpdateAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    // 主執行緒呼叫 UI 更新
                    if (this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.Invoke(() =>
                        {
                            reset_labText(); // 每次自動更新畫面數值
                        });
                    }

                    await Task.Delay(900, token); // 每900毫秒更新一次
                }
                catch (OperationCanceledException)
                {
                    break; // 正常取消任務
                }
                catch (Exception ex)
                {
                    Console.WriteLine("背景更新錯誤：" + ex.Message);
                }
            }
        }


        private void btn_SP1_Click(object sender, EventArgs e)
        {

            List<string> search = DBfunction.GetClassTag_address("Drill", "SP1");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }

        private void btn_SP2_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "SP2");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_SP3_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "SP3");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_SP4_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "SP4");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_common_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "COMMON");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_Panel_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "Panel");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_Peripheral_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "Peripheral");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_Cabinet_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "Cabinet");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }

        private void button11_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "Infeed_PNL");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }

        private void btn_Infeed_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "INFEED");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_InfeedBox_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "Infeed_BOX");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_Outfeed_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "OUTFEED");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_OutfeedBox_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "Outfeed_box");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_OutfeedPnl_Click(object sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address("Drill", "Outfeed_PNL");
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }


        private void Drill_main_Load(object sender, EventArgs e)
        {

            //輸入當前顯示的不同數值
            chartValues = update_class();
            reset_labText();


            // 總數量
            int totalCharts = 14;
            int chartsPerRow = 7;
            int spacingX = 130, spacingY = 220;
            int startX = 35, startY = 40;
            int lab_start_x = 30, lab_start_y = 150;

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
                chartPanel.Name = $"Color_{i}";

                this.Controls.Add(chartPanel);

                int table_x = lab_start_x + (col * spacingX);
                int table_y = lab_start_y + (row * spacingY);

                TableLayoutPanel tableLayoutPanel = panel_design.CreateColorLegendPanel(number[0].ToString(), number[1].ToString(), number[2].ToString());
                tableLayoutPanel.Name = $"ColorLegend_{i}";
                tableLayoutPanel.Location = new Point(table_x, table_y);
                this.Controls.Add(tableLayoutPanel);
            }


            _cts = new CancellationTokenSource();
            _ = Task.Run(() => AutoUpdateAsync(_cts.Token)); // 啟動背景更新任務
          
        }
        private bool isEventRegistered = false;
       
        private List<float[]> chartValues;

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
            chartValues = update_class();
            for (int i = 0; i < chartValues.Count; i++)
            {
                string panelName = $"ColorLegend_{i}";
                var legendPanel = this.Controls.Find(panelName, true).FirstOrDefault() as TableLayoutPanel;
                if (legendPanel != null)
                {
                    float[] values = chartValues[i];
                    int[] number = values.Select(v => Convert.ToInt32(v)).ToArray();

                    var lblRed = legendPanel.Controls.Find("lblRed", true).FirstOrDefault() as Label;
                    var lblYellow = legendPanel.Controls.Find("lblYellow", true).FirstOrDefault() as Label;
                    var lblGreen = legendPanel.Controls.Find("lblGreen", true).FirstOrDefault() as Label;

                    if (lblRed != null) lblRed.Text = number[0].ToString();
                    if (lblYellow != null) lblYellow.Text = number[1].ToString();
                    if (lblGreen != null) lblGreen.Text = number[2].ToString();
                }
            }

            lab_green.Text = DBfunction.Get_Green_number("Drill").ToString();
            lab_yellow.Text = DBfunction.Get_Yellow_number("Drill").ToString();
            lab_red.Text = DBfunction.Get_Red_number("Drill").ToString();
            lab_sum.Text = DBfunction.GetMachineRowCount("Drill").ToString();



            var existingContext = GlobalMachineHub.GetContext("Drill") as IMachineContext;

            //var existingContext = MachineHub.Get("Drill");
            if (existingContext != null && existingContext.IsConnected)
            {
                lab_connectOK.Text = LanguageManager.Translate("Mainform_connect");
                lab_connectOK.ForeColor = Color.Green;

                lab_connect.Text = existingContext.ConnectSummary.connect.ToString();
                List<string> drill_breakdowm_part = DBfunction.Get_breakdown_part("Drill");
                lab_partalarm.Text = DBfunction.Get_address_ByBreakdownParts("Drill", drill_breakdowm_part).Count.ToString();

            }
            else
            {

                lab_connectOK.Text = LanguageManager.Translate("Mainform_disconnect");
                lab_connectOK.ForeColor = Color.Red;

                lab_connect.Text = "0";
                lab_partalarm.Text = "0";
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

                List<string> search_number = DBfunction.GetClassTag_address("Drill", classTag);

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

        }


        private void Drill_main_VisibleChanged(object sender, EventArgs e)
        {
            reset_labText();

        }

        private void lab_sum_Click(object sender, EventArgs e)
        {
            var context = GlobalMachineHub.GetContext("Drill") as IMachineContext;

            if (context != null && context.IsConnected)
            {
                MessageBox.Show("當前監控總數更新時間：" + context.ConnectSummary.read_time.ToString());
            }
            else
            {
                MessageBox.Show("當前無資料監控與更新");
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


                //// 比對查出 Alarm 表中對應的 address & table（Drill/Swing）
                //(string matchedAddress, string table) = DBfunction.FindIOByAlarmDescription(des);

                // 查出這個 address 對應的 Description
                string des = DBfunction.Get_Description_ByAddress(e.Address);
                // 比對
                (string table, string Description) = DBfunction.Get_AlarmInfo_ByAddress(e.Address);
                string matchedAddress = DBfunction.Get_Address_ByDecription(table, Description);

                if (!string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(table))
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
        private void SwitchLanguage()
        {
            btn_SP1.Text = LanguageManager.Translate("ClassTag_SP1");
            btn_SP2.Text = LanguageManager.Translate("ClassTag_SP2");
            btn_SP3.Text = LanguageManager.Translate("ClassTag_SP3");
            btn_SP4.Text = LanguageManager.Translate("ClassTag_SP4");
            btn_common.Text = LanguageManager.Translate("ClassTag_common");
            btn_Panel.Text = LanguageManager.Translate("ClassTag_Panel");
            btn_Peripheral.Text = LanguageManager.Translate("ClassTag_Peripheral");
            btn_Cabinet.Text = LanguageManager.Translate("ClassTag_Cabinet");
            btn_Infeed.Text = LanguageManager.Translate("ClassTag_Infeed");
            btn_InfeedBox.Text = LanguageManager.Translate("ClassTag_InfeedBox");
            btn_infeed_PNL.Text = LanguageManager.Translate("ClassTag_infeed_PNL");
            btn_Outfeed.Text = LanguageManager.Translate("ClassTag_Outfeed");
            btn_OutfeedPnl.Text = LanguageManager.Translate("ClassTag_OutfeedPnl");
            btn_OutfeedBox.Text = LanguageManager.Translate("ClassTag_OutfeedBox");
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



