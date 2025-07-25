﻿using CsvHelper.Configuration;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Resources;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Data.GlobalMachineHub;
using static FX5U_IOMonitor.Models.MonitoringService;
using static FX5U_IOMonitor.Resources.Element_Settings;

namespace FX5U_IOMonitor
{

    public partial class Machine_main : Form
    {
        private List<MachinePanelGroup> groupList = new(); //儲存每個圖標的資料
        private CancellationTokenSource? _cts;             //儲存分組類別更新

        private string MachineType;                        //當前機台資料
        private List<string> matchedBtnTags;               //當前語系標籤

        private static Dictionary<string, Machine_main> _instances = new(); // 指定固定視窗

        public static Machine_main GetInstance(string machineType)
        {
            if (!_instances.ContainsKey(machineType) || _instances[machineType].IsDisposed)
            {
                _instances[machineType] = new Machine_main(machineType);
            }
            return _instances[machineType];
        }

        public Machine_main(string machineType)
        {

            InitializeComponent();
            this.MachineType = machineType;
            tableLayoutPanel1.BorderStyle = BorderStyle.FixedSingle; // 單線框
            tableLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel3.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel4.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel5.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel6.BorderStyle = BorderStyle.FixedSingle;
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            LanguageManager.LanguageChanged += OnLanguageChanged;
            SwitchLanguage();
            Monitor_alarm();
        }
        private void OnLanguageChanged(string cultureName)
        {
            reset_labText();
            SwitchLanguage();

        }


        private void lab_red_Click(object sender, EventArgs e)
        {
            List<string> alarms = DBfunction.Get_Red_addressList(MachineType);

            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(alarms, MachineType);          //  將資料傳入模組

            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面


        }

        private void reset_labText()//更新主頁面連接狀況
        {
            lab_green.Text = DBfunction.Get_Green_number(MachineType).ToString();
            lab_yellow.Text = DBfunction.Get_Yellow_number(MachineType).ToString();
            lab_red.Text = DBfunction.Get_Red_number(MachineType).ToString();
            lab_sum.Text = DBfunction.GetMachineRowCount(MachineType).ToString();

            var existingContext = GlobalMachineHub.GetContext(MachineType) as IMachineContext;

            if (existingContext != null && existingContext.IsConnected)
            {
                lab_connectOK.Text = LanguageManager.Translate("Mainform_connect");
                lab_connectOK.ForeColor = Color.Green;

                lab_connect.Text = existingContext.ConnectSummary.connect.ToString();
                List<string> drill_breakdowm_part = DBfunction.Get_breakdown_part(MachineType);
                lab_partalarm.Text = DBfunction.Get_address_ByBreakdownParts(MachineType, drill_breakdowm_part).Count.ToString();

            }
            else
            {

                lab_connectOK.Text = LanguageManager.Translate("Mainform_disconnect");
                lab_connectOK.ForeColor = Color.Red;

                lab_connect.Text = "0";
                lab_partalarm.Text = "0";
            }

            var Drill_Context = GlobalMachineHub.GetContext("Drill") as IMachineContext;

            if (Drill_Context != null && Drill_Context.IsConnected)
            {
                List<string> drill_breakdowm_part = DBfunction.Get_breakdown_part(MachineType);
                lab_partalarm.Text = DBfunction.Get_address_ByBreakdownParts(MachineType, drill_breakdowm_part).Count.ToString();
            }




        }
        bool isEventRegistered = false;

        private async Task AutoUpdateAsync(CancellationToken token)
        {
            List<float[]>? lastClassValue = null;

            var stopwatch = Stopwatch.StartNew();
            while (!token.IsCancellationRequested)
            {
                try
                {
                    // 1. 在背景取得資料
                    List<float[]> classvalue = update_class();

                    // 2. 檢查是否與前次結果相同（若相同則跳過更新）
                    bool isChanged = IsDifferent(classvalue, lastClassValue);

                    if (isChanged && this.IsHandleCreated && !this.IsDisposed)
                    {
                        lastClassValue = classvalue;

                        this.BeginInvoke(() =>
                        {
                            int minCount = Math.Min(groupList.Count, Math.Min(matchedBtnTags.Count, classvalue.Count));

                            for (int i = 0; i < minCount; i++)
                            {
                                Debug.WriteLine($"更新 {matchedBtnTags[i]} => {string.Join(", ", classvalue[i])}");
                                groupList[i].UpdateDisplay(classvalue[i]);
                            }
                          
                        });
                    }

                    await Task.Delay(500, token);
                }
                catch (OperationCanceledException)
                {
                    break; // 正常取消任務
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("背景更新錯誤：" + ex.Message);
                }
            }



        }
        private bool IsDifferent(List<float[]> current, List<float[]>? previous)
        {
            if (previous == null || current.Count != previous.Count)
                return true;

            for (int i = 0; i < current.Count; i++)
            {
                float[] curArr = current[i];
                float[] preArr = previous[i];

                if (curArr.Length != preArr.Length)
                    return true;

                for (int j = 0; j < curArr.Length; j++)
                {
                    if (!curArr[j].Equals(preArr[j])) // 或使用 Math.Abs(curArr[j] - preArr[j]) > 0.01f
                        return true;
                }
            }

            return false; // 完全一致
        }
        private List<float[]> update_class()
        {
            List<string> classTags = DBfunction.GetMachineClassTags(MachineType);

            List<float[]> chartValues = new List<float[]>();
            foreach (string classTag in classTags)
            {
                // 計算當前 ClassTag 的狀態數據
                List<string> search_number = DBfunction.GetClassTag_address(MachineType, classTag);

                int Green = DBfunction.Get_Green_search(MachineType, search_number);
                int yellow = DBfunction.Get_Yellow_search(MachineType, search_number);
                int red = DBfunction.Get_Red_search(MachineType, search_number);

                // 轉換為 float[]
                chartValues.Add(new float[] { Green, yellow, red });
            }
            return chartValues;
        }

        private void lab_yellow_Click(object sender, EventArgs e)
        {
            List<string> warn = DBfunction.Get_Yellow_addressList(MachineType);

            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(warn, MachineType);          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面

        }

        private void lab_green_Click(object sender, EventArgs e)
        {
            List<string> green = DBfunction.Get_Green_addressList(MachineType);
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(green, MachineType);          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void Machine_main_Load(object sender, EventArgs e)
        {


            reset_labText();

            List<string> classTags = DBfunction.GetMachineClassTags(MachineType); //確定當前分類 

            List<string> btnTags = DBfunction.GetClassTagLanguageKeys();  //確定語系資料

            // 對應 classTag → btnKey，例如 "Infeed" → "ClassTag_Infeed"
            matchedBtnTags = classTags.Select(tag =>
            {
                string expectedKey = $"ClassTag_{tag}";
                return btnTags.FirstOrDefault(k => k.Equals(expectedKey, StringComparison.OrdinalIgnoreCase)) ?? expectedKey;
            }).ToList();

            //輸入當前顯示的不同數值
            List<float[]> chartValues = update_class();
            // 總數量
            int totalCharts = classTags.Count;
            int chartsPerRow = 7;
            int spacingX = 130, spacingY = 220;
            int startX = 35, startY = 40;
            int lab_start_x = 30, lab_start_y = 150;

            for (int i = 0; i < totalCharts; i++)
            {
                int row = i / chartsPerRow;
                int col = i % chartsPerRow;

                int x = startX + (col * spacingX);
                int y = startY + (row * spacingY);

                string classTag = classTags[i];

                string btnTag = LanguageManager.Translate(matchedBtnTags[i]);

                var group = new MachinePanelGroup(MachineType, btnTag, classTag, chartValues[i])
                {
                    Location = new Point(startX + col * spacingX, startY + row * spacingY)
                };

                this.Controls.Add(group);
                groupList.Add(group);
            }
            _cts = new CancellationTokenSource();
            _ = Task.Run(() => AutoUpdateAsync(_cts.Token)); // 啟動背景更新任務

            //Monitor_alarm();

        }

        private void lab_connect_Click(object sender, EventArgs e)
        {
            var context = GlobalMachineHub.GetContext(MachineType) as IMachineContext;

            if (context != null && context.IsConnected)
            {
                if (int.Parse(context.ConnectSummary.read_time) > 70)
                {
                    MessageBox.Show("當前監控總數更新時間：70ms");

                }
                else
                {
                    MessageBox.Show("當前監控總數更新時間：" + context.ConnectSummary.read_time + "ms");
                }
            }
            else
            {
                MessageBox.Show("當前無資料監控與更新");
            }
        }

        private void Swing_main_VisibleChanged(object sender, EventArgs e)
        {
            reset_labText();
        }
        string searchText;
        private void btn_search_Click(object sender, EventArgs e)
        {
            searchText = txB_search.Text.Trim();
            List<string> search_data = DBfunction.Search_IOFromDB(MachineType, searchText);
            var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
            searchControl.LoadData(search_data, MachineType);          //  將資料傳入模組
            Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
        }

        private void lab_partalarm_Click(object sender, EventArgs e)
        {

            var existingContext = MachineHub.Get("Drill");
            if (existingContext != null && existingContext.IsConnected)
            {
                List<string> breakdown_part = DBfunction.Get_breakdown_part(MachineType);
                if (breakdown_part.Count != 0)
                {
                    List<string> breakdown_address = DBfunction.Get_address_ByBreakdownParts(MachineType, breakdown_part);
                    var searchControl = new UserSearchControl(); //  是 UserControl，不是 Form
                    searchControl.LoadData(breakdown_address, MachineType);          //  將資料傳入模組
                    Main.Instance.UpdatePanel(searchControl); //  嵌入到主畫面
                }
                else
                {
                    MessageBox.Show("目前料件未出現異常");
                }
            }
            else
            {
                MessageBox.Show("鑽床機尚未連線，請連線後再確認故障元件");
            }


        }
        private void Monitor_alarm()
        {
            Task.Run(async () =>
            {
                while (!this.IsDisposed)
                {
                    await Task.Delay(1000); // 每秒檢查連線

                    this.Invoke(() =>
                    {
                        var hub = MachineHub.Get("Drill");
                        if (hub?.IsConnected == true)
                        {
                            AlarmMonitorManager.EnsureRegistered("Drill", OnDrillAlarm);
                        }
                    });
                }
            });
        }
        private void OnDrillAlarm(IOUpdateEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(() => OnDrillAlarm(e));
                return;
            }

            // Reset 警告 UI
            reset_labText();
            List<string> breakdowm_part = DBfunction.Get_breakdown_part(MachineType);
            lab_partalarm.Text = DBfunction.Get_address_ByBreakdownParts(MachineType, breakdowm_part).Count.ToString();

            if (e.NewValue == true && e.OldValue == false)
            {
                string des = DBfunction.Get_Description_ByAddress(e.Address);
                (string table, string Description) = DBfunction.Get_AlarmInfo_ByAddress(e.Address);
                string IOelement = DBfunction.Get_Address_ByDecription(table, Description);

                if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(Description))
                {
                    string error = DBfunction.Get_Error_ByAddress(e.Address);
                    string comment = DBfunction.Get_Comment_ByAddress(table, IOelement);
                    string possible = DBfunction.Get_Possible_ByAddress(e.Address);
                    string repair = DBfunction.Get_Repair_steps_ByAddress(e.Address);

                    MessageBox.Show(
                        $"⚠️ 錯誤警告\n來源：{table} | 錯誤信息位址：{e.Address}\n錯誤料件：{Description}\n" +
                        $"錯誤訊息：{error}\n料件描述：{comment}\n可能原因：{possible}\n" +
                        $"錯誤排除步驟：\n{repair}",
                        "偵測警告系統已觸發錯誤",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    MessageBox.Show($"⚠ 找不到對應 Description：{des} 的 Drill 或 Saw 資料。");
                }
            }
        }
        //private void Monitor_alarm()
        //{
        //    Task.Run(async () =>
        //    {
        //        while (true)
        //        {
        //            await Task.Delay(1000); // 每秒檢查一次

        //            this.Invoke(() =>
        //            {
        //                var DB_update = MachineHub.Get("Drill");
        //                if (DB_update == null)
        //                {
        //                    Console.WriteLine("⚠️ MonitorHub 尚未註冊 Drill 監控對象");
        //                    return;
        //                }

        //                if (DB_update.IsConnected && !isEventRegistered)
        //                {
        //                    DB_update.Monitor.alarm_event += Warning_signs;
        //                    isEventRegistered = true;
        //                }
        //                else if (!DB_update.IsConnected && isEventRegistered)
        //                {
        //                    DB_update.Monitor.alarm_event -= Warning_signs;
        //                    isEventRegistered = false;
        //                }

        //            });


        //        }
        //    });
        //}

        //private void Warning_signs(object? sender, IOUpdateEventArgs e)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.Invoke(new Action(() => Warning_signs(sender, e)));
        //        return;
        //    }
        //    reset_labText();
        //    List<string> breakdowm_part = DBfunction.Get_breakdown_part(MachineType);
        //    lab_partalarm.Text = DBfunction.Get_address_ByBreakdownParts(MachineType, breakdowm_part).Count.ToString();
        //    if (e.NewValue == true && e.OldValue == false)
        //    {
        //        // 顯示變化
        //        MessageBox.Show($"📡 偵測到 I/O 變化：{e.Address} from {e.OldValue} ➜ {e.NewValue}");

        //        // 查出這個 address 對應的 Description
        //        string des = DBfunction.Get_Description_ByAddress(e.Address);

        //        // 比對查出 Alarm 表中對應的 address & table（Drill/Swing）
        //        (string table, string Description) = DBfunction.Get_AlarmInfo_ByAddress(e.Address);
        //        //取得元件的料號位置
        //        string IOelement = DBfunction.Get_Address_ByDecription(table, Description);

        //        if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(Description))
        //        {
        //            string error = DBfunction.Get_Error_ByAddress(e.Address);
        //            string comment = DBfunction.Get_Comment_ByAddress(table, IOelement);
        //            string Possible = DBfunction.Get_Possible_ByAddress(e.Address);
        //            string Repair_steps = DBfunction.Get_Repair_steps_ByAddress(e.Address);
        //            MessageBox.Show(
        //                $"⚠️ 錯誤警告\n來源：{table} | 錯誤信息位址：{e.Address}\n錯誤料件：{Description}\n" +
        //                $"錯誤訊息：{error}\n料件描述：{comment}\n可能原因：{Possible}\n " +
        //                $"錯誤排除步驟：\n{Repair_steps}",
        //                "偵測警告系統已觸發錯誤",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Warning
        //            );
        //        }
        //        else
        //        {
        //            MessageBox.Show($"⚠ 找不到對應 Description：{des} 的 Drill 或 saw 資料。");
        //        }
        //    }
        //}

        private void lab_sum_Click(object sender, EventArgs e)
        {



        }

        private void SwitchLanguage()
        {


            label1.Text = LanguageManager.Translate("Mainform_RedLights");
            label2.Text = LanguageManager.Translate("Mainform_YellowLights");
            label3.Text = LanguageManager.Translate("Mainform_GreenLights");
            label4.Text = LanguageManager.Translate("Mainform_ComponentFaults");
            label5.Text = LanguageManager.Translate("Mainform_Connections");
            label6.Text = LanguageManager.Translate("Mainform_MonitoredItems");
            label_txt.Text = LanguageManager.Translate("Mainform_TextSearch");
            btn_search.Text = LanguageManager.Translate("Mainform_Search");
            for (int i = 0; i < groupList.Count && i < matchedBtnTags.Count; i++)
            {
                groupList[i].UpdateButtonLabel(LanguageManager.Translate(matchedBtnTags[i]));
            }

        }

        private void btn_addElement_Click(object sender, EventArgs e)
        {
            using (var form = new Element_Settings(ElementMode.Add, MachineType))
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
            }
        }
    }
}



