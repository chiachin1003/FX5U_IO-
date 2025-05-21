using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FX5U_IOMonitor.Resources;
using static FX5U_IOMonitor.Resources.Element_Settings;
using System.Net;


namespace FX5U_IO元件監控
{

    public partial class Alarm_Notify : Form
    {
        private string datatable = "";
        public Alarm_Notify()
        {

            InitializeComponent();
            UpdatemachineComboBox();
            datatable = control_choose.Text;
            BuildTree(datatable);
            SetupDataGridView();
        }

        private void UpdatemachineComboBox()
        {
            using (var context = new ApplicationDB())
            {

                var machineNames = context.index
                                   .Select(io => io.Name);

                control_choose.Items.Clear();

                foreach (var machine in machineNames)
                {
                    control_choose.Items.Add(machine);
                }
                control_choose.SelectedIndex = 0;
            }


        }

        private void control_choose_SelectedIndexChanged(object sender, EventArgs e)
        {
            datatable = control_choose.Text;
            BuildTree(datatable);
        }
        private void BuildTree(string machineName)
        {
            treeView1.Nodes.Clear();

            // 抓取所有分類（包含空白 → other）
            List<string> classList = DBfunction.Get_alarm_class(machineName).OrderBy(c => c)
                                       .ToList(); ;

            foreach (var className in classList)
            {
                TreeNode classNode = new TreeNode(className); // 可加上翻譯：LanguageManager.Translate(className)
                classNode.Tag = className;

                // 每個 classTag 底下加錯誤內容節點
                List<string> errorList = DBfunction.Get_alarm_error_by_class(machineName, className);

                foreach (var error in errorList)
                {
                    TreeNode errorNode = new TreeNode(error);
                    errorNode.Tag = error;
                    classNode.Nodes.Add(errorNode);
                }

                treeView1.Nodes.Add(classNode);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var form = new Email_Settings())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                var result = form.ShowDialog(this);

            }
        }
        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Email", "收件人 Email");
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }
        private void btn_AddRecipient_Click(object sender, EventArgs e)
        {
            string email = txb_NewRecipient.Text.Trim();

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                MessageBox.Show("請輸入有效的電子郵件地址！");
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Email"].Value?.ToString() == email)
                {
                    MessageBox.Show("收件人已在列表中。");
                    return;
                }
            }
            dataGridView1.Rows.Add(email);
            txb_NewRecipient.Clear();
        }

        private void btn_RemoveSelected_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("請先選取要移除的收件人。");
            }
        }
    }
}



