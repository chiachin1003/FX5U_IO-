using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper.Configuration;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static FX5U_IOMonitor.Models.MonitoringService;

namespace FX5U_IOMonitor.panel_control
{
    public partial class ClassControl : UserControl
    {

        private bool isUpdating = false; // ✅ 記錄是否正在更新
        private MonitorService? monitor;
        private Dictionary<string, Panel> panelMap = new();
        private bool isEventRegistered = false;

        private string source_table;


        public ClassControl()
        {
            InitializeComponent();
           
        }
        public void LoadData(List<string> io_DataList, string classTag, string datatable)
        {
            if (io_DataList == null) return;
            source_table = datatable;

            
            tableLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel5.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel6.BorderStyle = BorderStyle.FixedSingle;
            lab_green.Text = DBfunction.Get_Green_classnumber(datatable, classTag, io_DataList).ToString();
            lab_yellow.Text = DBfunction.Get_Yellow_classnumber(datatable, classTag, io_DataList).ToString();
            lab_red.Text = DBfunction.Get_Red_classnumber(datatable, classTag, io_DataList).ToString();

          
            Update_Flow(io_DataList, classTag);


            monitor = MonitorHub.GetMonitor(datatable);
            if (monitor != null)
            {
                monitor.IOUpdated += OnIOChanged;

                isEventRegistered = true;

            }

        }
        private void OnIOChanged(object? sender, IOUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => OnIOChanged(sender, e));
                return;
            }

            // 顯示更新內容
            //MessageBox.Show($"[{e.Address}] {e.OldValue} → {e.NewValue}");
            bool NewValue = DBfunction.Get_current_single_ByAddress(source_table,e.Address);
            //更新元件顯示方式
            if (panelMap.TryGetValue(e.Address, out var panel))
            {
                UpdatePanelState(panel, NewValue);
            }
            


        }

       
        const int MaxPanelCount = 1000;

        public void Update_Flow(List<string> DataList, string classTag)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Update_Flow(DataList, classTag)));
                return;
            }
           
            if (isUpdating)
                return;
            isUpdating = true;

            try
            {
                if (DataList == null || DataList.Count == 0)
                {
                    Console.WriteLine("傳入的資料為空！");
                    return;
                }
                foreach (Control ctrl in flowLayoutPanel1.Controls)
                {
                    ctrl.Dispose();
                }
                flowLayoutPanel1.Controls.Clear(); // 清除應該在 try 裡
                panelMap.Clear();
                if (monitor != null && isEventRegistered)
                {
                    monitor.IOUpdated -= OnIOChanged;
                    isEventRegistered = false;
                }

                string keyword = classTag?.Trim().ToLower() ?? "";

                using var context = new ApplicationDB();
                if (source_table =="Drill") 
                {
                    var driilIOList = context.Drill_IO.ToList();
                    var filteredList = driilIOList.Where(io =>
                                    !string.IsNullOrEmpty(io.ClassTag) &&
                                    io.ClassTag.ToLower().Contains(keyword) &&
                                    DataList.Contains(io.address))
                                    .ToList();

                    foreach (var item in filteredList)
                    {
                        if (this.IsDisposed) break;

                        Panel panel = MachineInfo.PanelFactory.CreatePanel(
                            location: new Point(0, 0),
                            Electronic: item.IOType,
                            equipmentName: item.Description,
                            percent: item.RUL.ToString(),
                            rulPercent: item.RUL.ToString(),
                            effect: item.Comment,
                            address: item.address,
                            state: item.current_single
                        );

                        panel.Tag = item.address;
                        panelMap[item.address] = panel;
                        flowLayoutPanel1.Controls.Add(panel);
                    }
             
                }
                if (source_table == "Sawing")
                {
                    var SawingIOList = context.Sawing_IO.ToList();
                    var filteredList = SawingIOList.Where(io =>
                                    !string.IsNullOrEmpty(io.ClassTag) &&
                                    io.ClassTag.ToLower().Contains(keyword) &&
                                    DataList.Contains(io.address))
                                    .ToList();

                    foreach (var item in filteredList)
                    {
                        if (this.IsDisposed) break;

                        Panel panel = MachineInfo.PanelFactory.CreatePanel(
                            location: new Point(0, 0),
                            Electronic: item.IOType,
                            equipmentName: item.Description,
                            percent: item.RUL.ToString(),
                            rulPercent: item.RUL.ToString(),
                            effect: item.Comment,
                            address: item.address,
                            state: item.current_single
                        );

                        panel.Tag = item.address;
                        panelMap[item.address] = panel;
                        flowLayoutPanel1.Controls.Add(panel);
                    }

                }
            }
            finally
            {
                isUpdating = false;
            }
        }
      
    
        private void UpdatePanelState(Panel panel, bool newValue)
        {
      
            // ✅ 方法二：找裡面的 label 或 control 更新文字
            var labelStatus = panel.Controls
                .OfType<Panel>()
                .FirstOrDefault(l => l.Name == "panel_ON");

            if (labelStatus != null)
            {
                labelStatus.BackColor = newValue ? Color.White : Color.Black;
            }
        }
        
    }
}
