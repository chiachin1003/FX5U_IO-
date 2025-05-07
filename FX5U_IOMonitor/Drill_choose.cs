using static FX5U_IOMonitor.Main;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using System.Windows.Forms;
using FX5U_IOMonitor.panel_control;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace FX5U_IOMonitor
{
    public partial class Drill_choose : Form
    {
        Main main_form;

        public Drill_choose(Main main)
        {
            InitializeComponent();
            this.main_form = main;
        }

        public void UpdateTotal_label()
        {


            label_G.Text = $"綠燈\n數量 \n" + DBfunction.Get_Green_number("Drill").ToString();
            label_Y.Text = $"黃燈\n數量 \n" + DBfunction.Get_Yellow_number("Drill").ToString();
            label_R.Text = $"紅燈\n數量 \n" + DBfunction.Get_Red_number("Drill").ToString();


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



        private void label_R_Click(object sender, EventArgs e)
        {

            //List<IO_DataBase> alarms = Calculate.Find_Alarm(DataStore.Drill_DataList);
            //var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            //searchControl.LoadData(alarms);          //  將資料傳入模組
            //Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void comb_class_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        string searchText;
        private void txB_search_TextChanged(object sender, EventArgs e)
        {

            searchText = txB_search.Text.Trim();

        }


        private void label_G_Click(object sender, EventArgs e)
        {
            //List<IO_DataBase> Healthy = Calculate.Find_green(DataStore.Drill_DataList);
            //var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            //searchControl.LoadData(Healthy);          //  將資料傳入模組
            //Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void label_Y_Click(object sender, EventArgs e)
        {
            //List<IO_DataBase> Healthy = Calculate.Find_warn(DataStore.Drill_DataList);
            //var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            //searchControl.LoadData(Healthy);          //  將資料傳入模組
            //Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            searchText = txB_search.Text.Trim();

            List<string> search_data = DBfunction.Search_IOFromDB("Drill", searchText);
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search_data, "Drill");          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面


        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txB_search_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
