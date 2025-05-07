using SLMP;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using static FX5U_IOMonitor.connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using FX5U_IO元件監控;
using static FX5U_IO元件監控.Part_Search;
using FX5U_IOMonitor.Login;



namespace FX5U_IOMonitor
{
    public partial class Main_form : Form
    {


        private Saw_band Saw_band;
        private Part_Search Part_Search;
        private Drill_Info drill_Info;

        public Main_form()
        {
            InitializeComponent();
            btn_Drill_lifesetting.Enabled = false;
            btn_Sawing_lifesetting.Enabled = false;
            Main.Instance.LoginSucceeded += Main_LoginSucceeded;
            Main.Instance.LogoutSucceeded += Main_LogoutSucceeded;


        }


        private void Main_Load(object sender, EventArgs e)
        {

            reset_lab_connectText();


        }
        private void Main_LoginSucceeded(object? sender, EventArgs e)
        {
            if (UserService<ApplicationDB>.CurrentRole == SD.Role_Admin)
            {
                btn_Drill_lifesetting.Enabled = true;
                btn_Sawing_lifesetting.Enabled = true;
            }
            else
            {
                btn_Drill_lifesetting.Enabled = false;
                btn_Sawing_lifesetting.Enabled = false;
            }
        }
        private void Main_LogoutSucceeded(object? sender, EventArgs e)
        {
            btn_Drill_lifesetting.Enabled = false;
            btn_Sawing_lifesetting.Enabled = false;
        }

        private void OnChanged(object? sender, SwingUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => OnChanged(sender, e));
                return;
            }
            connect_isOK.swingstatus = e.NewStatus;

            ////更新顯示方式
            reset_lab_connectText();
            reset_status();


        }
        private void On_Changed(object? sender, DrillUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => On_Changed(sender, e));
                return;
            }
            connect_isOK.drillstatus = e.NewStatus_Drill;
            ////更新顯示方式
            reset_lab_connectText();
            Drill_main_update();

            reset_status();


        }

        private void reset_lab_connectText()//更新主頁面連接狀況
        {

            lab_green.Text = DBfunction.Get_Green_number("Drill").ToString();
            lab_yellow.Text = DBfunction.Get_Yellow_number("Drill").ToString();
            lab_red.Text = DBfunction.Get_Red_number("Drill").ToString();
            lab_sum.Text = DBfunction.GetTableRowCount("Drill").ToString();

            lab_sum_swing.Text = DBfunction.GetTableRowCount("Sawing").ToString();
            lab_red_swing.Text = DBfunction.Get_Red_number("Sawing").ToString();
            lab_yellow_swing.Text = DBfunction.Get_Yellow_number("Sawing").ToString();
            lab_green_swing.Text = DBfunction.Get_Green_number("Sawing").ToString();

            if (connect_isOK.Drill_connect == false && connect_isOK.Swing_connect == false)
            {
                lab_connect.Text = "0";
                //lab_disconnect.Text = DataStore.Drill_DataList.Count.ToString();
                lab_disconnect.Text = "0";
                lab_connect_swing.Text = "0";
                lab_disconnect_swing.Text = "0";
                //lab_disconnect_swing.Text = DataStore.Swing_DataList.Count.ToString();
            }
            else if (connect_isOK.Drill_connect == true && connect_isOK.Swing_connect == false)
            {
                lab_connect.Text = connect_isOK.Drill_total.connect.ToString();
                //lab_disconnect.Text = connect_isOK.Drill_total.disconnect.ToString();
                lab_disconnect.Text = "0";
                lab_connect_swing.Text = "0";
                lab_disconnect_swing.Text = "0";

                //lab_disconnect_swing.Text = DataStore.Swing_DataList.Count.ToString();
            }
            else if (connect_isOK.Drill_connect == true && connect_isOK.Swing_connect == true)
            {
                lab_connect.Text = connect_isOK.Drill_total.connect.ToString();
                //lab_disconnect.Text = connect_isOK.Drill_total.disconnect.ToString();
                lab_disconnect.Text = "0";

                lab_connect_swing.Text = connect_isOK.Swing_total.connect.ToString();
                lab_disconnect_swing.Text = "0";
                lab_disconnect_swing.Text = connect_isOK.Swing_total.disconnect.ToString();
            }
            else if (connect_isOK.Drill_connect == false && connect_isOK.Swing_connect == true)
            {
                lab_connect.Text = "0";
                //lab_disconnect.Text = DataStore.Drill_DataList.Count.ToString();
                lab_disconnect.Text = "0";

                lab_connect_swing.Text = connect_isOK.Swing_total.connect.ToString();
                lab_disconnect_swing.Text = "0";
                //lab_disconnect_swing.Text = connect_isOK.Swing_total.disconnect.ToString();
            }


        }
        private void reset_status()//更新主頁面連接狀況
        {



            if (connect_isOK.Drill_connect == false && connect_isOK.Swing_connect == false)
            {
                return;

            }
            else if (connect_isOK.Drill_connect == true && connect_isOK.Swing_connect == false)
            {
                Drill_main_update();
                swing_main_update();

            }
            else if (connect_isOK.Drill_connect == true && connect_isOK.Swing_connect == true)
            {
                Drill_main_update();
                swing_main_update();
            }
            else if (connect_isOK.Drill_connect == false && connect_isOK.Swing_connect == true)
            {
                swing_main_update();
            }


        }

        private void swing_main_update()
        {

            lb_swing_current.Text = DBfunction.Get_Machine_now_string("Sawing_current") + "\n安培";
            lb_swing_Voltage.Text = DBfunction.Get_Machine_now_string("Sawin_voltage") + "\n伏特";
            lb_swing_cutingspeed.Text = DBfunction.Get_Machine_now_string("Sawing_cuttingspeed") + "\nm/min";
            lb_swing_motor_current.Text = DBfunction.Get_Machine_now_string("Sawing_current") + "\n安培";
            lb_swingpower.Text = DBfunction.Get_Machine_now_string("Sawing_power") + "\n千瓦小時";
            lb_oilpress.Text = DBfunction.Get_Machine_now_string("Sawing_oil_pressure") + "\n公斤力";
            lb_Swing_totaltime.Text = DBfunction.Get_Machine_now_string("Sawing_total_time");
            lb_time.Text = DBfunction.Get_Machine_now_string("Sawing_countdown_time");
            lb_swing_Remaining_Dutting_tools.Text = DBfunction.Get_Machine_now_string("Sawing_remain_tools") + "刀";



            //lb_swing_current.Text = connect_isOK.swingstatus.avg_mA + "\n安培";
            //lb_swing_Voltage.Text = connect_isOK.swingstatus.avg_V + "\n伏特";
            //lb_swing_cutingspeed.Text = connect_isOK.swingstatus.cuttingspeed.ToString() + "\nm/min";
            //lb_swing_motor_current.Text = connect_isOK.swingstatus.motorcurrent.ToString() + "\n安培";
            ////lb_swingpower.Text = connect_isOK.swingstatus.power.ToString() + "\n千瓦小時";
            //lb_oilpress.Text = connect_isOK.swingstatus.oil_pressure.ToString() + "\n公斤力";
            //lb_Swing_totaltime.Text = connect_isOK.swingstatus.Runtime.ToString();
            //lb_time.Text = connect_isOK.swingstatus.Sawing_countdown_time;
            //lb_swing_Remaining_Dutting_tools.Text = connect_isOK.swingstatus.Remaining_Dutting_tools + "刀";
        }
        private void Drill_main_update()
        {
            lb_cutingtime.Text = DBfunction.Get_Machine_now_string("spindle_usetime");
            lb_Drill_totaltime.Text = DBfunction.Get_Machine_now_string("Drill_total_Time");
            lb_drill_Voltage.Text = DBfunction.Get_Machine_now_string("Drill_voltage") + "\n伏特";
            lb_drill_current.Text = DBfunction.Get_Machine_now_string("Drill_current") + "\n安培 ";
            lb_drillpower.Text = DBfunction.Get_Machine_now_string("Drill_power") + "\n千瓦小時 ";
            lb_drill_du.Text = DBfunction.Get_Machine_now_string("Drill_electricity") + "\n度";


            //lb_cutingtime.Text = connect_isOK.drillstatus.Spindle_usetime;
            //lb_Drill_totaltime.Text = connect_isOK.drillstatus.Runtime;
            //lb_drill_Voltage.Text = connect_isOK.drillstatus.Voltage + "\n伏特";
            //lb_drill_current.Text = connect_isOK.drillstatus.Current +"\n安培 ";
            //lb_drillpower.Text = connect_isOK.drillstatus.power+"\n千瓦小時 ";
            //lb_drill_du.Text = connect_isOK.drillstatus.du+"\n度";
        }


        private void Main_form_VisibleChanged(object sender, EventArgs e)
        {
            reset_lab_connectText();
            reset_status();

            MonitorService monitor_drill = MonitorHub.GetMonitor("Drill");
            if (monitor_drill != null)
            {
                monitor_drill.DrillStatusUpdated += On_Changed;
                monitor_drill.SwingStatusUpdated += OnChanged;
            }
        }

        private void Main_form_TextChanged(object sender, EventArgs e)
        {
            reset_lab_connectText();
            reset_status();

        }

        private void btn_SawBand_Click(object sender, EventArgs e)
        {
            if (add_saw_Form == null || add_saw_Form.IsDisposed)
            {
                add_saw_Form = new Saw_band();
                add_saw_Form.Show();
            }
            else
            {
                add_saw_Form.BringToFront();  // 若已開啟，拉到最前面
            }
           
        }

        private Part_Search? part_Search = null;
        private Part_Search? part_Search_1 = null;

        private Drill_Info? addInfo_Form = null;
        private Saw_band? add_saw_Form = null;

        private void btn_Drill_lifesetting_Click(object sender, EventArgs e)
        {
            if (part_Search == null || part_Search.IsDisposed)
            {
                part_Search = new Part_Search(ShowPage.Drill);
                part_Search.Show();
            }
            else
            {
                part_Search.BringToFront();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (part_Search_1 == null || part_Search_1.IsDisposed)
            {
                part_Search_1 = new Part_Search(ShowPage.Sawing);
                part_Search_1.Show();
            }
            else
            {
                part_Search_1.BringToFront();  // 若已開啟，拉到最前面
            }
           
        }

        private void btn_Drill_Info_Click(object sender, EventArgs e)
        {
            if (addInfo_Form == null || addInfo_Form.IsDisposed)
            {
                addInfo_Form = new Drill_Info();
                addInfo_Form.Show();
            }
            else
            {
                addInfo_Form.BringToFront();  // 若已開啟，拉到最前面
            }
           
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_SwingTime_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
