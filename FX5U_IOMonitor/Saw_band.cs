using SLMP;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using static FX5U_IOMonitor.connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;



namespace FX5U_IOMonitor
{
    public partial class Saw_band : Form
    {

        public Saw_band()
        {
            InitializeComponent();

        }


        private void Main_Load(object sender, EventArgs e)
        {
            reset_lab_connectText();
            MonitorService monitor = MonitorHub.GetMonitor("Sawing");
            if (monitor != null)
            {
                monitor.SwingStatusUpdated += OnChanged;
            }

        }


        private void OnChanged(object? sender, SwingUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => OnChanged(sender, e));
                return;
            }
            connect_isOK.sawband = e.NewStatus_band;
           
            ////更新元件顯示方式
            reset_lab_connectText();

        }

        private void reset_lab_connectText()
        {

            lab_Sawband.Text = DBfunction.Get_Machine_read_address("Sawband_brand");
            lab_Saw_teeth.Text = DBfunction.Get_Machine_read_address("Sawblade_teeth") + "\nTOOL/INCH";
            lab_Saw_blade.Text = DBfunction.Get_Machine_read_address("Sawblade_material");
            lab_speed.Text = DBfunction.Get_Machine_read_address("Sawband_speed") + "\nRPM";
            lab_motors_usetime.Text = DBfunction.Get_Machine_read_address("Sawband_motors_usetime");
            lab_power.Text = "\n" + DBfunction.Get_Machine_read_address("Sawband_power") + "\n\n千瓦";
            lab_current.Text = "\n" + DBfunction.Get_Machine_read_address("Sawband_current") + "\n\n安培(A)";
            lab_area.Text = "累積面積\n\n" + DBfunction.Get_Machine_read_address("Sawband_area");
            lab_usage.Text = DBfunction.Get_Machine_read_address("Sawband_tension");
            lab_saw_life.Text = DBfunction.Get_Machine_read_address("Sawband_life");



            //lab_Sawband.Text = connect_isOK.sawband.Sawband_brand ;
            //lab_Saw_teeth.Text = connect_isOK.sawband.Saw_teeth + "\nTOOL/INCH";
            //lab_Saw_blade.Text = connect_isOK.sawband.Saw_blade_material;
            //lab_speed.Text =  connect_isOK.sawband.Sawband_speed+ "\nRPM";
            //lab_motors_usetime.Text = connect_isOK.sawband.saw_motors_usetime;
            //lab_power.Text = "\n" + connect_isOK.sawband.power + "\n\n千瓦";
            //lab_current.Text = "\n"+connect_isOK.sawband.Maximum_current+"\n\n安培(A)";
            //lab_area.Text = "累積面積\n\n" + connect_isOK.sawband.area;
            //lab_usage.Text = connect_isOK.sawband.usage;
            //lab_saw_life.Text = connect_isOK.sawband.saw_blade_life;

        }


     
    }
}
