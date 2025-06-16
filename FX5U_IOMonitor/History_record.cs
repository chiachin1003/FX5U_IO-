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
using Microsoft.EntityFrameworkCore;


namespace FX5U_IO元件監控
{

    public partial class History_record : Form
    {
        private string datatable = "";
        public History_record()
        {

            InitializeComponent();

        }


        private void btn_add_element_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();   // ✅ 清除舊選擇（避免看起來沒變）

            DateTime startDate = dateTime_start.Value.Date.ToUniversalTime();
            DateTime endDate = dateTime_end.Value.Date.AddDays(1).AddTicks(-1).ToUniversalTime();

            if (choose_event.SelectedIndex ==0)
            {
                
                var result = DBfunction.Get_Searchalarm_Records(startDate, endDate);
                dataGridView1.DataSource = result;

            }
            else 
            {
                var result = DBfunction.Get_Searchparam_HistoryRecords(startDate, endDate);
                dataGridView1.DataSource = result;

            }


        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString(); // 從 1 開始編號

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            Rectangle headerBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                grid.RowHeadersWidth,
                e.RowBounds.Height);

            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

    
    }
}



