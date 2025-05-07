using FX5U_IOMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FX5U_IOMonitor
{
    public partial class alarm_setting : Form
    {
        private string equipmentTag;

        public alarm_setting(string Tag)
        {
            InitializeComponent();
            equipmentTag = Tag;

            update_interface();

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            DBfunction.Set_Error_ByAddress(equipmentTag, txB_Error.Text);
            DBfunction.Set_Possible_ByAddress(equipmentTag, txB_Possible.Text);
            DBfunction.Set_RepairStep_ByAddress(equipmentTag, txB_Step.Text);
            update_interface();
        }
        private void update_interface()
        {
            lab_Description.Text = DBfunction.Get_Description_ByAddress(equipmentTag);
            lab_class.Text = DBfunction.Get_classTag_ByAddress(equipmentTag);
            txB_Error.Text = DBfunction.Get_Error_ByAddress(equipmentTag);
            txB_Possible.Text = DBfunction.Get_Possible_ByAddress(equipmentTag);
            txB_Step.Text = DBfunction.Get_RepairStep_ByAddress(equipmentTag);
        }

   
    }
}
