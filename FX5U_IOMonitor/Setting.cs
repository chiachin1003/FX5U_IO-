using CsvHelper;
using System.Globalization;
using static FX5U_IOMonitor.Main;
using FX5U_IOMonitor.Data;
using Newtonsoft.Json;
using System.Text;
using System.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Resources;
using System.Windows.Forms;
using FX5U_IO元件監控;


namespace FX5U_IOMonitor
{

    public partial class Setting : Form
    {
        public Setting()
        {

            InitializeComponent();
            
        }

        private void btn_file_download_Click(object sender, EventArgs e)
        {
            using (var form = new File_Settings())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }

        }

        private void btn_Mail_Manager_Click(object sender, EventArgs e)
        {
            using (var form = new Email_Settings())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }

        }

        private void btn_Alrm_Notify_Click(object sender, EventArgs e)
        {
            using (var form = new Alarm_Notify())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }
        }

        private void btn_usersetting_Click(object sender, EventArgs e)
        {
            using (var form = new UserManageForm())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);

            }
        }

        private void btn_history_Click(object sender, EventArgs e)
        {
            using (var form = new History_record())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);
            }
        }
    }

}



