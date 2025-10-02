using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.MitsubishiPlc_Monior;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Utilization;
using Org.BouncyCastle.Utilities;
using SLMP;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using static FX5U_IOMonitor.Check_point;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace FX5U_IOMonitor
{
    public partial class UtilizationRate : Form
    {
        private CancellationTokenSource? _cts;

        private bool _suppressAutoSave = false;

        private List<UtilizationShiftConfig> Workshift;

        // 放在 class UtilizationRate 內
        private readonly Dictionary<string, UtilizationPanel> _machinePanels = new();
        private List<Machine_number> _machineList;
        private int _currentShiftNo = 1;   // 由你的班別按鈕切換
        private TimeScope _currentScope = TimeScope.Today;  // 由四個時間範圍按鈕切換
        private enum TimeScope { Today, Yesterday, ThisWeek, LastWeek }

        public UtilizationRate()
        {
            InitializeComponent();
            InitUtilizationPickers();

            this.FormClosing += (_, __) => _cts?.Cancel();

            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            SwitchLanguage();
            LanguageManager.LanguageChanged += _ => SwitchLanguage();
        }
        /// <summary>
        /// 語系切換
        /// </summary>
        private void SwitchLanguage()
        {
            this.Text = LanguageManager.Translate("UtilizationRate_Main");
            lab_start.Text = LanguageManager.Translate("UtilizationRate_StartTime");
            lab_end.Text = LanguageManager.Translate("UtilizationRate_EndTime");
            lab_UtilizationRate.Text = LanguageManager.Translate("UtilizationRate_DrillUtilization");
            Text_design.SafeAdjustFont(lab_UtilizationRate, lab_UtilizationRate.Text, 36);


        }

        private void UtilizationRate_Load(object sender, EventArgs e)
        {
            UtilizationRate_Inital();
            // 讀取預設模式（1~4）
            int mode = Properties.Settings.Default.DefaultUtilizationRate;
            _machineList = DBfunction.GetMachineIndexes();

            comb_class.DataSource = Workshift
               .Where(s => s.Enabled)
               .Select(s => new { s.ShiftNo, Display = $"第{s.ShiftNo}班 ({s.Start}-{s.End})" })
               .ToList();

            comb_class.DisplayMember = "Display";
            comb_class.ValueMember = "ShiftNo";
            comb_class.SelectedValue = 1; // 預設第一班

            RenderAllMachines(TimeScope.Today);
        }


        private void UtilizationRate_Inital()
        {

            WireTimePair(dateTimePicker_start1, dateTimePicker_end1);
            WireTimePair(dateTimePicker_start2, dateTimePicker_end2);
            WireTimePair(dateTimePicker_start3, dateTimePicker_end3);
            WireTimePair(dateTimePicker_start4, dateTimePicker_end4);

        }


        private void btn_calculate1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultUtilizationRate = 1;
            Properties.Settings.Default.Save();

        }
        private void btn_calculate2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultUtilizationRate = 2;
            Properties.Settings.Default.Save();
        }

        private void btn_calculate3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultUtilizationRate = 3;
            Properties.Settings.Default.Save();
        }

        private void btn_calculate4_Click(object sender, EventArgs e)
        {
            SaveUtilizationPair(dateTimePicker_start4, dateTimePicker_end4, 4);
            Properties.Settings.Default.DefaultUtilizationRate = 4;
            Properties.Settings.Default.Save();
            //    float denom = UtilizationRateCalculate.GetDurationSeconds_ByMinuteDiff_NoOvernight(dateTimePicker_start4, dateTimePicker_end4);
            //    // 避免除以 0
            //    if (denom <= 0)
            //    {
            //        lab_recordtime.Text = LanguageManager.Translate("UtilizationRate_lab_error");
            //        return;
            //    }

            //var (startLocal, endLocal) = BuildStartEndLocal(dateTime_start, dateTimePicker_start4, dateTimePicker_end4,
            //                                               allowOvernight: false, equalMeansOneHour: false);

            //    // 再轉 UTC
            //    var (utcStart, utcEnd) = UtilizationRateCalculate.ToUtcRange(startLocal, endLocal);
            //    Drill_UtilizationRate = UtilizationRateCalculate.Get_UtilizationRate(utcStart, utcEnd, "Drill");
            //    Sawing_UtilizationRate = UtilizationRateCalculate.Get_UtilizationRate(utcStart, utcEnd, "Sawing");
            //    RenderUtilizationGauge(Drill_UtilizationRate, denom, ref _drillPanel, this, "drillPanel", new Point(60, 40), new Size(150, 150));
            //    RenderUtilizationGauge(Sawing_UtilizationRate, denom, ref _sawingPanel, this, "sawingPanel", new Point(300, 40), new Size(150, 150));

        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitUtilizationPickers()
        {
            _suppressAutoSave = true;

            var shifts = UtilizationConfigLoader.LoadShifts();
            Workshift = shifts.Shifts;
            foreach (var shift in shifts.Shifts)
            {
                var (startPicker, endPicker, button) = ResolvePickersByMode(shift.ShiftNo);

                ConfigurePicker(startPicker, shift.Start);
                ConfigurePicker(endPicker, shift.End);

                // 你也可以加上 Enabled 判斷 → 比如禁用 Picker
                if (!shift.Enabled)
                {
                    startPicker.Enabled = false;
                    endPicker.Enabled = false;
                    button.Enabled = false;
                }
            }

            _suppressAutoSave = false;

        }
        private (DateTimePicker start, DateTimePicker end, System.Windows.Forms.Button button) ResolvePickersByMode(int mode)
        {
            switch (mode)
            {
                case 1: return (dateTimePicker_start1, dateTimePicker_end1, btn_calculate1);
                case 2: return (dateTimePicker_start2, dateTimePicker_end2, btn_calculate2);
                case 3: return (dateTimePicker_start3, dateTimePicker_end3, btn_calculate3);
                case 4: return (dateTimePicker_start4, dateTimePicker_end4, btn_calculate4);
                default: return (dateTimePicker_start1, dateTimePicker_end1, btn_calculate1); // fallback
            }
        }
        private static void ConfigurePicker(DateTimePicker picker, string timeText)
        {
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "HH:mm";
            picker.ShowUpDown = true;

            if (TimeSpan.TryParse(timeText, out var ts))
            {
                picker.Value = DateTime.Today.Add(ts); // 轉成今天的時間顯示
            }
            else
            {
                picker.Value = DateTime.Today; // 失敗就給個預設值
            }
        }


        /// <summary>
        /// 組合當天的稼動率顯示
        /// </summary>
        /// <param name="dpDate"></param>
        /// <param name="tpStart"></param>
        /// <param name="tpEnd"></param>
        /// <param name="allowOvernight"></param>
        /// <param name="equalMeansOneHour"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        private static (DateTime startLocal, DateTime endLocal) BuildStartEndLocal(
            DateTimePicker dpDate, DateTimePicker tpStart, DateTimePicker tpEnd,
            bool allowOvernight = false, bool equalMeansOneHour = false)
        {
            if (dpDate == null || tpStart == null || tpEnd == null
                || dpDate.IsDisposed || tpStart.IsDisposed || tpEnd.IsDisposed)
                throw new ArgumentNullException("Date/Time picker 不可為 null 或已釋放");

            var date = dpDate.Value.Date;                // 只取年月日
            var startLocal = date + new TimeSpan(tpStart.Value.Hour, tpStart.Value.Minute, 0);
            var endLocal = date + new TimeSpan(tpEnd.Value.Hour, tpEnd.Value.Minute, 59);

            // 不允許跨日：End < Start 視為錯誤
            if (!allowOvernight && endLocal < startLocal)
                throw new InvalidOperationException("End 不得小於 Start（不允許跨日）。");

            // 允許跨日：End < Start 就+1天
            if (allowOvernight && endLocal < startLocal)
                endLocal = endLocal.AddDays(1);

            // Start == End 的處理（若你想視為 1 小時）
            if (equalMeansOneHour && endLocal == startLocal)
                endLocal = endLocal.AddHours(1);

            return (startLocal, endLocal);
        }
    
        /// <summary>
        /// 設定json檔裡面的數值
        /// </summary>
        /// <param name="startPicker"></param>
        /// <param name="endPicker"></param>
        /// <param name="shiftNo"></param>
        /// <param name="jsonPath"></param>
        /// <exception cref="ArgumentException"></exception>
        private void SaveUtilizationPair(DateTimePicker startPicker, DateTimePicker endPicker, int shiftNo, string jsonPath = "UtilizationShiftConfig.json")
        {
            var json = File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

            var shiftsFile = JsonSerializer.Deserialize<ShiftsFile>(json, options)
                             ?? new ShiftsFile();

            // 找對應班別
            var shift = shiftsFile.Shifts.FirstOrDefault(s => s.ShiftNo == shiftNo);
            if (shift == null)
                throw new ArgumentException($"ShiftNo={shiftNo} 在 shifts.json 中不存在");

            // 更新時間（格式化成 "HH:mm"）
            shift.Start = startPicker.Value.ToString("HH:mm");
            shift.End = endPicker.Value.ToString("HH:mm");

            // 存回 JSON
            File.WriteAllText(jsonPath, JsonSerializer.Serialize(shiftsFile, options));
        }
        /// <summary>
        /// 確保 end 不早於 start。
        /// timeOnly=true 時只比對時:分；false 則比完整日期時間。
        /// allowEqual=true 允許相等；false 則強制 end > start。
        /// </summary>
        private static void SyncEndNotEarlier(DateTimePicker start, DateTimePicker end, bool timeOnly = true, bool allowEqual = true)
        {
            if (start == null || end == null || start.IsDisposed || end.IsDisposed) return;

            var s = start.Value;
            var e = end.Value;

            // 只取時分比較（避免不同日期影響）
            var sKey = timeOnly
                ? new DateTime(1, 1, 1, s.Hour, s.Minute, 0)
                : new DateTime(s.Year, s.Month, s.Day, s.Hour, s.Minute, 0);

            var eKey = timeOnly
                ? new DateTime(1, 1, 1, e.Hour, e.Minute, 0)
                : new DateTime(e.Year, e.Month, e.Day, e.Hour, e.Minute, 0);

            bool needFix = eKey < sKey || (!allowEqual && eKey == sKey);
            if (needFix)
            {
                // 調整 end 的時分（保留 end 原本的日期）
                end.Value = new DateTime(e.Year, e.Month, e.Day, s.Hour, s.Minute, 0);
            }
        }
        /// <summary>
        /// 幫一組 start/end 綁定 ValueChanged 事件，任何一邊改動都會自動校正 end。
        /// </summary>
        private static void WireTimePair(DateTimePicker start, DateTimePicker end, bool timeOnly = true, bool allowEqual = true)
        {
            bool updating = false; // 防止事件互相觸發造成遞迴

            void handler(object _, EventArgs __)
            {
                if (updating) return;
                updating = true;
                try
                {
                    SyncEndNotEarlier(start, end, timeOnly, allowEqual);
                }
                finally
                {
                    updating = false;
                }
            }

            start.ValueChanged += handler;
            end.ValueChanged += handler;

            // 先跑一次，確保初始狀態正確
            handler(null, EventArgs.Empty);
        }

        private void btn_today_Click(object sender, EventArgs e)
        {
            RenderAllMachines(TimeScope.Today);
        }
        /// <summary>
        /// 繪製所有機台的稼動率圖形
        /// </summary>
        private void RenderAllMachines(TimeScope _currentScope)
        {
            if (_machineList == null || !_machineList.Any()) return;
            if (Workshift == null) return;
            
            int shiftNo = Convert.ToInt32(comb_class.SelectedValue); //選定當前班別
            var shift = Workshift.FirstOrDefault(s => s.ShiftNo == shiftNo);
            if (shift == null) return;

            DateTime selectedDate = dateTime_start.Value.Date;
          

            // 計算分母（該班別時間長度）
            var (sp, ep, button) = ResolvePickersByMode(shiftNo);
            float denom = UtilizationRateCalculate.GetDurationSeconds_ByMinuteDiff_NoOvernight(sp, ep); //分母
            // 避免除以 0
            if (denom <= 0)
            {
                return;
            }
            denom = _currentScope switch
            {
                TimeScope.Today => denom,
                TimeScope.Yesterday => denom,
                TimeScope.ThisWeek => denom * 7,
                TimeScope.LastWeek => denom * 7,
                _ => denom // 預設情況不變
            };

            // 排版
            int x = 60, y = 70;
            int spacingX = 220, spacingY = 220;
            int perRow = Math.Max(1, (this.Width - 120) / spacingX);
            int i = 0;
            List<ShiftResult> results = new();

            foreach (var machine in _machineList)
            {
                string machineName = machine.Name;

                results = _currentScope switch
                {
                    TimeScope.Today => UtilizationRateCalculate.GetTodayCutting(machineName, Workshift),
                    TimeScope.Yesterday => UtilizationRateCalculate.GetYesterdayCutting(machineName, Workshift, selectedDate),
                    TimeScope.ThisWeek => UtilizationRateCalculate.GetThisWeekCutting(machineName, Workshift),
                    TimeScope.LastWeek => UtilizationRateCalculate.GetLastWeekCutting(machineName, Workshift, selectedDate),
                    _ => new List<ShiftResult>() // 預設值，避免 null
                };


                float cutting = results.FirstOrDefault(r => r.ShiftNo == shiftNo)?.CuttingSeconds ?? 0;

                int col = i % perRow;
                int row = i / perRow;
                var location = new Point(x + col * spacingX, y + row * spacingY);

                RenderOrUpdatePanel(machineName, cutting, denom, location, new Size(150, 150));
                i++;
            }
        }
        /// <summary>
        /// 更新稼動率圖表內容
        /// </summary>
        /// <param name="machineName"></param>
        /// <param name="numerator"></param>
        /// <param name="denom"></param>
        /// <param name="location"></param>
        /// <param name="size"></param>
        private void RenderOrUpdatePanel(string machineName, float numerator, float denom, Point location, Size size)
        {
            if (!_machinePanels.TryGetValue(machineName, out var panel) || panel == null || panel.IsDisposed)
            {
                panel = new UtilizationPanel
                {
                    Name = $"{machineName}_Panel",
                    Location = location,
                    Size = size
                };
                _machinePanels[machineName] = panel;
                this.Controls.Add(panel);
            }
            else
            {
                panel.Location = location;
                panel.Size = size;
            }

            double pct = denom <= 0 ? 0 : (numerator / denom) * 100.0;

            panel.SetValues(
                used: numerator,
                total: denom,
                centerText: $"{machineName}\n{pct:0.0}%",
                usedColor: Color.ForestGreen,
                remainingColor: Color.Gainsboro,
                middleRingColor: Color.White,
                middleRingRatio: 0.2f,
                holeRatio: 0.12f,
                centerTextFontSize: 16f
            );
        }

        private void btn_yesterday_Click(object sender, EventArgs e)
        {
            RenderAllMachines(TimeScope.Yesterday);

        }

        private void btn_thisweek_Click(object sender, EventArgs e)
        {
            RenderAllMachines(TimeScope.ThisWeek);

        }

        private void btn_lastweek_Click(object sender, EventArgs e)
        {
            RenderAllMachines(TimeScope.LastWeek);
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            SaveUtilizationPair(dateTimePicker_start1, dateTimePicker_end1, 1);
            SaveUtilizationPair(dateTimePicker_start2, dateTimePicker_end2, 2);
            SaveUtilizationPair(dateTimePicker_start3, dateTimePicker_end3, 3);
            SaveUtilizationPair(dateTimePicker_start4, dateTimePicker_end4, 4);

        }
    }
}
