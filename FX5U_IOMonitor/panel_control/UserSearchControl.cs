
using System.Data;
using System.Diagnostics;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using Microsoft.EntityFrameworkCore;
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
        string currentLang;

        public UserSearchControl()
        {
            InitializeComponent();
        }
        private bool isLanguageRegistered = false;

        public void LoadData(List<string> io_DataList, string datatable)
        {
            if (io_DataList == null) return;
            source_table = datatable;
            DataList = io_DataList;
            Update_Flow(io_DataList);

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
            // 防重複綁定語系事件
            if (!isLanguageRegistered)
            {
                LanguageManager.LanguageChanged += OnLanguageChanged;
                isLanguageRegistered = true;
            }

            SwitchLanguage();

        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
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

                //// 移除重複事件綁定保險
                labEquipment.Click -= LabEquipment_Click_Handler;
                labEquipment.Click += LabEquipment_Click_Handler;

            }
            else
            {
                Debug.WriteLine($"✅ 成功綁定紅字與事件於：{labEquipment.Text}");
            }
        }

        /// <summary>
        /// 新增元件點擊後關閉的異常標準
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="breakdown_part_address"></param>
        /// <param name="description"></param>
        private void LabEquipment_Click_Handler(object? sender, EventArgs e)
        {
            if (sender is Label lbl && lbl.Tag is string address)
            {
                var alarms = DBfunction.Get_Addresses_ByCurrentSingle(lbl.Text);

                if (alarms == null || alarms.Count == 0)
                {
                    MessageBox.Show($"{LanguageManager.Translate("Alarm_Message_No_anomaly_data")}：{lbl.Text}）", 
                        $"{LanguageManager.Translate("Alarm_Message_LookupResult")}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (alarms.Count == 1)
                {
                    var alarm = alarms[0].ToString();
                    string possible = DBfunction.Get_Possible_ByAddress(alarm);
                    string error = DBfunction.Get_Error_ByAddress(alarm);
                    string repair = DBfunction.Get_Repair_steps_ByAddress(alarm);

                    MessageBox.Show(
                        $"{LanguageManager.Translate("Alarm_Message_Error_Warning")}\n" +
                        $"{LanguageManager.Translate("Alarm_Message_Error_Item")}：{lbl.Text}\n" +
                        $"{LanguageManager.Translate("Alarm_Message_Error_Message")}：{error}\n" +
                        $"{LanguageManager.Translate("Alarm_Message_Possible_Cause")}：{possible}\n" +
                        $"{LanguageManager.Translate("Alarm_Message_Repair_Steps")}：\n{repair}",
                        LanguageManager.Translate("Alarm_Message_Error_Window_Title"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    string all = $"{LanguageManager.Translate("Alarm_Message_Error_Warning")}\n" +
                                 $"{LanguageManager.Translate("Alarm_Message_Error_Item")}：{lbl.Text}\n" +
                                 $"{LanguageManager.Translate("Alarm_Message_AnomalyLine")}：{alarms.Count}\n\n";


                    foreach (var alarm in alarms)
                    {
                        string possible = DBfunction.Get_Possible_ByAddress(alarm.ToString());
                        string error = DBfunction.Get_Error_ByAddress(alarm.ToString());
                        all += $"\n\n{LanguageManager.Translate("Alarm_Message_Error_Message")}：{error}\n" +
                            $"{LanguageManager.Translate("Alarm_Message_Possible_Cause")}：{possible}\n--------------------\n";
                    }

                    MessageBox.Show(all, $"{LanguageManager.Translate("Alarm_Message_MultipleErrors")}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                using (var context = new ApplicationDB())
                {
                    var filteredList = context.Machine_IO.Where(io => io.Machine_name == source_table &&
                                    DataList.Contains(io.address)).Include(io => io.Translations)
                                    .Take(MaxPanelCount)
                                    .ToList();

                    var newAddresses = filteredList.Select(i => i.address).ToHashSet();
                    var oldAddresses = panelMap.Keys.ToHashSet();

                    var toRemove = oldAddresses.Except(newAddresses).ToList();

                    foreach (var addr in toRemove)
                    {
                        var panel = panelMap[addr];
                        flowLayoutPanel2.Controls.Remove(panel);
                        panel.Dispose();
                        panelMap.Remove(addr);
                    }
                    // 🟢 更新或新增
                    foreach (var item in filteredList)
                    {
                        if (this.IsDisposed) break;

                        if (panelMap.TryGetValue(item.address, out var existingPanel))
                        {
                            MachineInfo.PanelFactory.UpdatePanelData(existingPanel, item); // ✅ 請實作此函式以更新屬性
                        }
                        else
                        {
                            Panel panel = MachineInfo.PanelFactory.CreatePanel(
                                location: new Point(0, 0),
                                source_table,
                                Electronic: item.IOType,
                                equipmentName: item.Description,
                                percent: item.RUL.ToString("F2"),
                                rulPercent: item.RUL.ToString("F2"),
                                effect: item.GetComment(Properties.Settings.Default.LanguageSetting),
                                address: item.address,
                                state: item.current_single,
                                onDeleted: (deletedAddress) =>
                                {
                                    // 刪除後更新 panelMap 與畫面
                                    if (panelMap.TryGetValue(deletedAddress, out var deletedPanel))
                                    {
                                        flowLayoutPanel2.Controls.Remove(deletedPanel);
                                        deletedPanel.Dispose();
                                        panelMap.Remove(deletedAddress);
                                    }
                                    Update_Flow(DataList);
                                }
                            );
                            panel.Tag = item.address;
                            panelMap[item.address] = panel;
                            flowLayoutPanel2.Controls.Add(panel);
                        }
                    }

                    // 🔴 重建錯誤標註
                    var breakdown_part = DBfunction.Get_breakdown_part(source_table);
                    var breakdown_address = DBfunction.Get_address_ByBreakdownParts(source_table, breakdown_part);
                    foreach (string addr in breakdown_address)
                    {
                        if (panelMap.TryGetValue(addr, out var panel))
                        {
                            HighlightPanel(panel, breakdown_part);
                        }
                    }
                }
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
        public static void Update_searchPercentPanel(Panel panel, string percent, int green, int yellow, int red)
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
                        panel_light.BackColor = (Color)MachineInfo.PanelFactory.SetColor(percent, green, yellow, red);
                    }
                }
            }



        }
        private void SwitchLanguage()
        {
            this.currentLang = Properties.Settings.Default.LanguageSetting;

            label1.Text = LanguageManager.Translate("Mainform_RedLights");
            label2.Text = LanguageManager.Translate("Mainform_YellowLights");
            label3.Text = LanguageManager.Translate("Mainform_GreenLights");
            if (DataList != null && DataList.Count > 0)
            {
                Update_Flow(DataList);
            }
        }

       
    }
}
