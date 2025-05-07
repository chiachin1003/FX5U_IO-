using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using static FX5U_IOMonitor.Main;
using static FX5U_IOMonitor.Models.MonitoringService;


namespace FX5U_IOMonitor
{
    public partial class swing_setting : Form
    {
        Main main_form;
      
        string searchText;

        public swing_setting(Main main)
        {
            InitializeComponent();
            this.main_form = main;
        }

        public void UpdateTotal_label()
        {

            label_G.Text = $"綠燈\n數量 \n" + DBfunction.Get_Green_number("Sawing").ToString();
            label_Y.Text = $"黃燈\n數量 \n" + DBfunction.Get_Green_number("Sawing").ToString();
            label_R.Text = $"紅燈\n數量 \n" + DBfunction.Get_Green_number("Sawing").ToString();


            label_G.TextAlign = ContentAlignment.MiddleCenter;
            label_Y.TextAlign = ContentAlignment.MiddleCenter;
            label_R.TextAlign = ContentAlignment.MiddleCenter;

        }
        public void Updateconnect_label(connect_Summary connect_Summary)
        {

            label_Connect.Text = $"連接\n數量 \n" + connect_Summary.connect.ToString();
            label_disconnect.Text = $"未連接數量 \n" + connect_Summary.disconnect.ToString();
            label_Connect.TextAlign = ContentAlignment.MiddleCenter;
            label_disconnect.TextAlign = ContentAlignment.MiddleCenter;

        }
        private void btn_X_IO_Click(object sender, EventArgs e)
        {
            main_form.TargetPanel.Controls.Clear();
           


        }

        private void btn_x_setting_Click(object sender, EventArgs e)
        {
           

        }

        private void btn_Y_IO_Click(object sender, EventArgs e)
        {
            main_form.TargetPanel.Controls.Clear();

        }

        private void Drill_choose_Load(object sender, EventArgs e)
        {
          
        }
        //顯示於主畫面
        private void ShowChildForm(Form childForm)
        {
            if (main_form.TargetPanel == null || !main_form.TargetPanel.Visible || main_form.TargetPanel.Width == 0 || main_form.TargetPanel.Height == 0)
            {
                MessageBox.Show("TargetPanel 未正確初始化或不可見");
                return;
            }

            // 隱藏其他視窗
            foreach (Control control in main_form.TargetPanel.Controls)
            {
                if (control is Form form)
                {
                    form.Hide();
                }
            }

            // 初始化子視窗
            if (childForm == null || childForm.IsDisposed)
            {
                childForm = new Form();
                MessageBox.Show("視窗未正確初始化");
                return;
            }

            // 設置屬性以嵌入 Panel
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // 僅在需要時添加
            if (!main_form.TargetPanel.Controls.Contains(childForm))
            {
                main_form.TargetPanel.Controls.Add(childForm);
            }

            // 顯示並刷新
            childForm.Show();
            childForm.BringToFront();
            main_form.TargetPanel.Invalidate();
            main_form.TargetPanel.Update();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_G_Click(object sender, EventArgs e)
        {
            //List<IO_DataBase> Healthy = Calculate.Find_green(DataStore.Swing_DataList);
            //OpenSearch(Healthy);
        }

        private void label_Y_Click(object sender, EventArgs e)
        {
            //List<IO_DataBase> Healthy = Calculate.Find_warn(DataStore.Swing_DataList);
            //OpenSearch(Healthy);
        }

        private void label_R_Click(object sender, EventArgs e)
        {
            //List<IO_DataBase> alarms = Calculate.Find_Alarm(DataStore.Swing_DataList);
            //OpenSearch(alarms);
        }
      
        private void btn_search_Click(object sender, EventArgs e)
        {
            searchText = txB_search.Text.Trim();
            List<string> search_data = DBfunction.Search_IOFromDB("Sawing", searchText);
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search_data, "Sawing");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
            //OpenSearch(search_data);
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
    }
}
