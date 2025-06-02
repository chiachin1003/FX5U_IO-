using System;
using System.Collections;
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
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using static FX5U_IOMonitor.Models.MonitoringService;

namespace FX5U_IOMonitor.panel_control
{
   
    public partial class UserSearchControl : UserControl
    {
        private bool isUpdating = false;
        private string source_table;
        private List<string> DataList;
        private Dictionary<string, Panel> panelMap = new();
        private MonitorService? monitor;
        private bool isEventRegistered = false;
        public UserSearchControl()
        {
            InitializeComponent();
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageCSV("language.csv", lang);
            SwitchLanguage();
            LanguageManager.LanguageChanged += OnLanguageChanged;

        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
        }

        public void LoadData(List<string> io_DataList, string datatable)
        {
            if (io_DataList == null) return;
            source_table = datatable;
            DataList = io_DataList;
            List<string> breakdown_part = DBfunction.Get_breakdown_part(source_table);
            List<string> breakdown_address = new();

            if (source_table == "Drill")
            {
                breakdown_address = DBfunction.Get_address_ByBreakdownParts("Drill", breakdown_part);
            }
            else if (source_table == "Sawing")
            {
                breakdown_address =DBfunction.Get_address_ByBreakdownParts("Sawing", breakdown_part);
            }


            Update_Flow(io_DataList);

            // 將故障對應的項目標紅與綁定點擊事件
            foreach (string addr in breakdown_address)
            {
                if (panelMap.TryGetValue(addr, out var panel))
                {
                    HighlightPanel(panel, breakdown_part);
                }
            }

            tableLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel5.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel6.BorderStyle = BorderStyle.FixedSingle;
            lab_green.Text = DBfunction.Get_Green_search(datatable, io_DataList).ToString();
            lab_yellow.Text = DBfunction.Get_Yellow_search(datatable, io_DataList).ToString();
            lab_red.Text = DBfunction.Get_Red_search(datatable, io_DataList).ToString();

            //設定哪個監控
            var monitor = GlobalMachineHub.GetMonitor(datatable);

            if (monitor is MonitorService slmp)
            {
                slmp.IOUpdated += OnIOChanged;
                isEventRegistered = true;


            }
            else if (monitor is ModbusMonitorService modbus)
            {
                modbus.IOUpdated += OnIOChanged;
                isEventRegistered = true;

            }



        }
        /// <summary>
        /// 綁定故障元件功能
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="breakdown_part_address"></param>
        private void HighlightPanel(Panel panel, List<string> breakdown_part_address)
        {
            var labEquipment = panel.Controls.Find("lab_equipment", true).FirstOrDefault() as Label;

            if (labEquipment != null)
            {
                labEquipment.ForeColor = Color.Red;
                // 將 Address 傳遞給 Label.Tag
                labEquipment.Tag = panel.Tag;  // 假設 panel.Tag 已經是 address 字串

                // 移除重複事件綁定保險
                // 保險移除現有事件（無法移除 lambda，只能這樣保留一次註冊）
                labEquipment.Click -= (s, e) => { }; // 通常略過

                // ✅ 使用 lambda 包含 address 傳遞進去
                labEquipment.Click += (s, e) => LabEquipment_Click(s, e, breakdown_part_address, labEquipment.Text);

            }
        }

        /// <summary>
        /// 新增元件點擊後關閉的異常標準
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="breakdown_part_address"></param>
        /// <param name="description"></param>
        private void LabEquipment_Click(object? sender, EventArgs e, List<string> breakdown_part_address, string description)
        {
            if (sender is Label lbl)
            {
                // 從 address 查出 Description
                var alarms = DBfunction.Get_Addresses_ByCurrentSingle(description);

                if (alarms == null || alarms.Count == 0)
                {
                    MessageBox.Show($"🔍 未找到該設備（{description}）的異常資料。", "查詢結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (alarms.Count == 1)
                {
                    var alarm = (alarms[0].ToString());
                    string possible = DBfunction.Get_Possible_ByAddress(alarm);
                    string error = DBfunction.Get_Error_ByAddress(alarm);
                    MessageBox.Show(
                        $"⚠️ 錯誤警告\n料件：{description}\n\n錯誤訊息：{error}\n可能原因：{possible}",
                        "I/O 錯誤偵測",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    // 多筆顯示整合訊息
                    string all = $"⚠️ 錯誤警告\n料件：{description}\n共發現 {alarms.Count} 筆異常：\n\n";

                    foreach (var alarm in alarms)
                    {
                        string possible = DBfunction.Get_Possible_ByAddress(alarm.ToString());
                        string error = DBfunction.Get_Error_ByAddress(alarm.ToString());
                        all += $"\n\n錯誤訊息：{error}\n可能原因：{possible}\n--------------------\n";
                    }

                    MessageBox.Show(all, "多筆 I/O 錯誤偵測", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        const int MaxPanelCount = 1000;

        /// <summary>
        /// 更新實體監控元件總數
        /// </summary>
        /// <param name="DataList"></param>
        public void Update_Flow(List<string> DataList)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Update_Flow(DataList)));
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
                foreach (Control ctrl in flowLayoutPanel2.Controls)
                {
                    ctrl.Dispose();
                }
                flowLayoutPanel2.Controls.Clear();
                panelMap.Clear();


                using (var context = new ApplicationDB())
                {
                    var filteredList = context.Machine_IO.Where(io => io.Machine_name == source_table &&
                                    DataList.Contains(io.address))
                                    .ToList();
                    foreach (var item in filteredList.Take(MaxPanelCount))
                    {
                        if (this.IsDisposed) break;

                        Panel panel = MachineInfo.PanelFactory.CreatePanel(
                            location: new Point(0, 0),
                            source_table,
                            Electronic: item.IOType,
                            equipmentName: item.Description,
                            percent: item.RUL.ToString("F2"),
                            rulPercent: item.RUL.ToString("F2"),
                            effect: item.Comment,
                            address: item.address,
                            state: item.current_single
                        );

                        panel.Tag = item.address;
                        panelMap[item.address] = panel;
                        flowLayoutPanel2.Controls.Add(panel);

                    }
                };

            }
            finally
            {
                isUpdating = false;
            }
        }

        /// <summary>
        /// 訂閱實體元件監控事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnIOChanged(object? sender, IOUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => OnIOChanged(sender, e));
                return;
            }

            // 顯示更新內容
            //MessageBox.Show($"[{e.Address}] {e.OldValue} → {e.NewValue}");
            bool NewValue = DBfunction.Get_current_single_ByAddress(source_table, e.Address);
            double percent = DBfunction.Get_RUL_ByAddress(source_table, e.Address);
            int green = DBfunction.Get_SetG_ByAddress(source_table, e.Address);
            int yellow = DBfunction.Get_SetY_ByAddress(source_table, e.Address);
            int red = DBfunction.Get_SetR_ByAddress(source_table, e.Address);

            //更新元件顯示方式
            if (panelMap.TryGetValue(e.Address, out var panel))
            {
                UpdatePanelState(panel, NewValue);
                Update_searchPercentPanel(panel, percent.ToString("F2"), green, yellow, red);
            }
        }
        /// <summary>
        /// 更新實體元件當前狀態
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="newValue"></param>
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
        /// <summary>
        /// 更新實體元件監控壽命百分比
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="percent"></param>
        /// <param name="green"></param>
        /// <param name="yellow"></param>
        /// <param name="red"></param>
        private void Update_searchPercentPanel(Panel panel, string percent, int green, int yellow, int red)
        {

            // ✅ 方法二：找裡面的 label 或 control 更新文字
            var labelpercent = panel.Controls
                .OfType<Label>()
                .FirstOrDefault(l => l.Name == "label_percent");

            if (labelpercent != null)
            {
                if (!string.IsNullOrEmpty(percent))
                {

                    if (labelpercent.Name == "label_percent")
                    {
                        labelpercent.Text = percent + "%";

                    }
                }
            }
            var labelRUL = panel.Controls
               .OfType<ProgressBar>()
               .FirstOrDefault(l => l.Name == "RUL_precent");

            if (labelRUL != null)
            {
                if (!string.IsNullOrEmpty(percent))
                {

                    if (labelRUL.Name == "RUL_precent")
                    {
                        labelRUL.Value = MachineInfo.PanelFactory.ProgressBarValue(percent);

                    }
                }
            }
            var panel_light = panel.Controls
              .OfType<Panel>()
              .FirstOrDefault(l => l.Name == "panel_light");
            if (panel_light != null)
            {
                if (!string.IsNullOrEmpty(percent))
                {

                    if (panel_light.Name == "panel_light")
                    {
                        labelRUL.BackColor = (Color)MachineInfo.PanelFactory.SetColor(percent, green, yellow, red);
                    }
                }
            }



        }
        private void SwitchLanguage()
        {
            label1.Text = LanguageManager.Translate("Mainform_RedLights");
            label2.Text = LanguageManager.Translate("Mainform_YellowLights");
            label3.Text = LanguageManager.Translate("Mainform_GreenLights");
        }

       
    }
}
