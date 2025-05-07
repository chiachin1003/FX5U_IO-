using SLMP;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using static FX5U_IOMonitor.connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;



namespace FX5U_IOMonitor
{
    public partial class Drill_Info : Form
    {

        public Drill_Info()
        {
            InitializeComponent();
           
        }


        private void Main_Load(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            if(connect_isOK.Drill_connect==true)
            {
                reset_lab_connectText();
                MonitorService monitor = MonitorHub.GetMonitor("Drill");
                if (monitor != null)
                {
                    monitor.DrillStatusUpdated += OnChanged;
                    monitor.DrillStatusUpdated += OnChanged;

                }
            }

        }


        private void OnChanged(object? sender, DrillUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => OnChanged(sender, e));
                return;
            }
            connect_isOK.drillstatus = e.NewStatus_Drill;
            //MessageBox.Show("更新");
           
            ////更新元件顯示方式
            reset_lab_connectText();

        }

        private void reset_lab_connectText()
        {


            lab_Servo_drives_usetime.Text = DBfunction.Get_Machine_now_string("Drill_servo_usetime");
            lab_Spindle_usetime.Text = DBfunction.Get_Machine_now_string("Drill_spindle_usetime");
            lab_PLC_usetime.Text = DBfunction.Get_Machine_now_string("Drill_plc_usetime");
            lab_Frequency_Converter_usetime.Text = DBfunction.Get_Machine_now_string("Drill_inverter");
            lab_Runtime.Text = DBfunction.Get_Machine_now_string("Drill_total_Time");

            lab_origin.Text = DBfunction.Get_Machine_number("Drill_origin").ToString() + " 次 ";
            lab_loose_tools.Text = DBfunction.Get_Machine_number("Drill_loose_tools").ToString() + " 次 ";
            lab_measurement.Text = DBfunction.Get_Machine_number("Drill_measurement").ToString() + " 次 ";
            lab_clamping.Text = DBfunction.Get_Machine_number("Drill_clamping").ToString() + " 次 ";
            lab_feeder.Text = DBfunction.Get_Machine_number("Drill_feeder").ToString() + " 次 ";


            //lab_Servo_drives_usetime.Text = connect_isOK.drillstatus.Servo_drives_usetime;
            //lab_Spindle_usetime.Text = connect_isOK.drillstatus.Spindle_usetime;
            //lab_PLC_usetime.Text = connect_isOK.drillstatus.PLC_usetime;
            //lab_Frequency_Converter_usetime.Text = connect_isOK.drillstatus.Frequency_Converter_usetime;
            //lab_Runtime.Text = connect_isOK.drillstatus.Runtime;
            //lab_origin.Text = connect_isOK.drillstatus.origin.ToString() + " 次 ";
            //lab_loose_tools.Text = connect_isOK.drillstatus.loose_tools.ToString() + " 次 ";
            //lab_measurement.Text = connect_isOK.drillstatus.measurement.ToString() + " 次 ";
            //lab_clamping.Text = connect_isOK.drillstatus.clamping.ToString() + " 次 ";
            //lab_feeder.Text = connect_isOK.drillstatus.feeder.ToString() + " 次 ";

        }

       


    }
}
