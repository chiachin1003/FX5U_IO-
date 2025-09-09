using CsvHelper;
using System.Globalization;
using static FX5U_IOMonitor.Main;
using FX5U_IOMonitor.Data;
using Newtonsoft.Json;
using System.Text;
using System.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.MitsubishiPlc_Monior;


namespace FX5U_IOMonitor
{

    public partial class DisconnectTable : Form
    {
        public DisconnectTable(string machine)
        {

            InitializeComponent();
            dataGridView1.DataSource = Disconnecttable.GetDisconnectEvent(machine);

        }




    }

}



