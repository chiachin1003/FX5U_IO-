using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.MitsubishiPlc_Monior;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Utilization;
using SLMP;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using static FX5U_IOMonitor.Check_point;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace FX5U_IOMonitor
{
    public partial class UtilizationRate : Form
    {
        private CancellationTokenSource? _cts;
        private FX5U_IOMonitor.panel_control.UtilizationPanel _drillPanel;
        private FX5U_IOMonitor.panel_control.UtilizationPanel _sawingPanel;
        private bool _suppressAutoSave = false;
        private float Drill_UtilizationRate;
        private float Sawing_UtilizationRate;
        private string Utilization_Shift;

        // 依序加入所需欄位(靜態)

        public UtilizationRate()
        {
            InitializeComponent();
            InitUtilizationPickers();
            Utilization_Shift = "UtilizationShiftConfig.json";
            var ShiftsFile = UtilizationConfigLoader.LoadShifts(Utilization_Shift);     

            this.FormClosing += (_, __) => _cts?.Cancel();

            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            SwitchLanguage();
            LanguageManager.LanguageChanged += _ => SwitchLanguage();
        }



        /* ---------- 語系切換 ---------- */
        private void SwitchLanguage()
        {
            this.Text = LanguageManager.Translate("UtilizationRate_Main");
            lab_start.Text = LanguageManager.Translate("UtilizationRate_StartTime");
            lab_end.Text = LanguageManager.Translate("UtilizationRate_EndTime");
            lab_Saw_UtilizationRate.Text = LanguageManager.Translate("UtilizationRate_SawingUtilization");
            lab_Drill_UtilizationRate.Text = LanguageManager.Translate("UtilizationRate_DrillUtilization");
            Text_design.SafeAdjustFont(lab_Drill_UtilizationRate, lab_Drill_UtilizationRate.Text,36);
            Text_design.SafeAdjustFont(lab_Saw_UtilizationRate, lab_Saw_UtilizationRate.Text,36);

        }

        private void UtilizationRate_Load(object sender, EventArgs e)
        {
            UtilizationRate_Inital();
            // 讀取預設模式（1~4）
            int mode = Properties.Settings.Default.DefaultUtilizationRate;
            if (mode < 1 || mode > 4) mode = 1; // 防呆
            var (sp, ep, button) = ResolvePickersByMode(mode);
            float denom = UtilizationRateCalculate.GetDurationSeconds_ByMinuteDiff_NoOvernight(sp, ep);
            // 避免除以 0
            if (denom <= 0)
            {
                lab_recordtime.Text = LanguageManager.Translate("UtilizationRate_lab_error");//不可計算<0的數值
                return;
            }
            var (startLocal, endLocal) = BuildStartEndLocal(dateTime_start, sp, ep,
                                                       allowOvernight: false, equalMeansOneHour: false);

            var (utcStart, utcEnd) = UtilizationRateCalculate.ToUtcRange(startLocal, endLocal);

            //計算時間為:
            lab_recordtime.Text = LanguageManager.Translate("UtilizationRate_lab_short") + "：\n" +
                  $"{startLocal:yyyy/MM/dd}{Environment.NewLine}{startLocal:HH:mm}-{endLocal:HH:mm}";

            int last = UtilizationRateCalculate.GetLastWeekLastValue("Drill");
            int current = UtilizationRateCalculate.GetCurrentUtilization("Drill");

            //上周運行
            float Drill_LastUtilizationRate = (float)(Convert.ToInt32(UtilizationRateCalculate.GetCurrentUtilization("Drill") - last));
            last = UtilizationRateCalculate.GetLastWeekLastValue("Sawing");
            current = UtilizationRateCalculate.GetCurrentUtilization("Sawing");

            float Sawing_LastUtilizationRate = (float)(Convert.ToInt32(UtilizationRateCalculate.GetCurrentUtilization("Sawing") - UtilizationRateCalculate.GetLastWeekLastValue("Sawing")));
            //當前運行狀態
            Drill_UtilizationRate = (float)(Convert.ToInt32(UtilizationRateCalculate.GetCurrentUtilization("Drill") - UtilizationRateCalculate.GetYesterdayLastValue("Drill")));
            Sawing_UtilizationRate = (float)(Convert.ToInt32(UtilizationRateCalculate.GetCurrentUtilization("Sawing") - UtilizationRateCalculate.GetYesterdayLastValue("Sawing")));

            RenderUtilizationGauge(Drill_UtilizationRate, denom, ref _drillPanel, this, "drillPanel", new Point(60, 40), new Size(150, 150));
            RenderUtilizationGauge(Sawing_UtilizationRate, denom, ref _sawingPanel, this, "sawingPanel", new Point(300, 40), new Size(150, 150));

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

            //分母 allowOvernight 不允許跨日
            float denom = UtilizationRateCalculate.GetDurationSeconds_ByMinuteDiff_NoOvernight(dateTimePicker_start1, dateTimePicker_end1);

            // 避免除以 0
            if (denom <= 0)
            {
                lab_recordtime.Text = LanguageManager.Translate("UtilizationRate_lab_error");
                return;
            }
            var (startLocal, endLocal) = BuildStartEndLocal(dateTime_start, dateTimePicker_start1, dateTimePicker_end1,
                                                       allowOvernight: false, equalMeansOneHour: false);

            // 再轉 UTC
            var (utcStart, utcEnd) = UtilizationRateCalculate.ToUtcRange(startLocal, endLocal);
            SaveUtilizationPair(dateTimePicker_start1, dateTimePicker_end1, 1);

            Drill_UtilizationRate = UtilizationRateCalculate.Get_UtilizationRate(utcStart, utcEnd, "Drill");
            Sawing_UtilizationRate = UtilizationRateCalculate.Get_UtilizationRate(utcStart, utcEnd, "Sawing");
            RenderUtilizationGauge(Drill_UtilizationRate, denom, ref _drillPanel, this, "drillPanel", new Point(60, 40), new Size(150, 150));
            RenderUtilizationGauge(Sawing_UtilizationRate, denom, ref _sawingPanel, this, "sawingPanel", new Point(300, 40), new Size(150, 150));

            Properties.Settings.Default.Save();
            lab_recordtime.Text = LanguageManager.Translate("UtilizationRate_lab_short") + ":\n" +
                $"{startLocal:yyyy/MM/dd}{Environment.NewLine}{startLocal:HH:mm}-{endLocal:HH:mm}";

        }
        private void btn_calculate2_Click(object sender, EventArgs e)
        {
            //分母 allowOvernight 不允許跨日
            float denom = UtilizationRateCalculate.GetDurationSeconds_ByMinuteDiff_NoOvernight(dateTimePicker_start2, dateTimePicker_end2);
            // 避免除以 0
            if (denom <= 0)
            {
                lab_recordtime.Text = LanguageManager.Translate("UtilizationRate_lab_error");
                return;
            }

            var (startLocal, endLocal) = BuildStartEndLocal(dateTime_start, dateTimePicker_start2, dateTimePicker_end2,
                                                       allowOvernight: false, equalMeansOneHour: false);
            // 再轉 UTC
            var (utcStart, utcEnd) = UtilizationRateCalculate.ToUtcRange(startLocal, endLocal);
            SaveUtilizationPair(dateTimePicker_start2, dateTimePicker_end2, 2);

            Drill_UtilizationRate = UtilizationRateCalculate.Get_UtilizationRate(utcStart, utcEnd, "Drill");
            Sawing_UtilizationRate = UtilizationRateCalculate.Get_UtilizationRate(utcStart, utcEnd, "Sawing");

            RenderUtilizationGauge(Drill_UtilizationRate, denom, ref _drillPanel, this, "drillPanel", new Point(60, 40), new Size(150, 150));
            RenderUtilizationGauge(Sawing_UtilizationRate, denom, ref _sawingPanel, this, "sawingPanel", new Point(300, 40), new Size(150, 150));


            Properties.Settings.Default.Save();
            lab_recordtime.Text = LanguageManager.Translate("UtilizationRate_lab_short") + ":\n" +
                $"{startLocal:yyyy/MM/dd}{Environment.NewLine}{startLocal:HH:mm}-{endLocal:HH:mm}";
        }

        private void btn_calculate3_Click(object sender, EventArgs e)
        {
            //分母 allowOvernight 不允許跨日
            float denom = UtilizationRateCalculate.GetDurationSeconds_ByMinuteDiff_NoOvernight(dateTimePicker_start3, dateTimePicker_end3);
            // 避免除以 0
            if (denom <= 0)
            {
                lab_recordtime.Text = LanguageManager.Translate("UtilizationRate_lab_error");
                return;
            }

            var (startLocal, endLocal) = BuildStartEndLocal(dateTime_start, dateTimePicker_start3, dateTimePicker_end3,
                                                       allowOvernight: false, equalMeansOneHour: false);

            // 再轉 UTC
            var (utcStart, utcEnd) = UtilizationRateCalculate.ToUtcRange(startLocal, endLocal);
            SaveUtilizationPair(dateTimePicker_start3, dateTimePicker_end3, 3);

            Drill_UtilizationRate = UtilizationRateCalculate.Get_UtilizationRate(utcStart, utcEnd, "Drill");
            Sawing_UtilizationRate = UtilizationRateCalculate.Get_UtilizationRate(utcStart, utcEnd, "Sawing");
            RenderUtilizationGauge(Drill_UtilizationRate, denom, ref _drillPanel, this, "drillPanel", new Point(60, 40), new Size(150, 150));
            RenderUtilizationGauge(Sawing_UtilizationRate, denom, ref _sawingPanel, this, "sawingPanel", new Point(300, 40), new Size(150, 150));
            Properties.Settings.Default.DefaultUtilizationRate = 3;
            Properties.Settings.Default.Save();

            lab_recordtime.Text = LanguageManager.Translate("UtilizationRate_lab_short") + ":\n" +
                $"{startLocal:yyyy/MM/dd}{Environment.NewLine}{startLocal:HH:mm}-{endLocal:HH:mm}";
        }

        private void btn_calculate4_Click(object sender, EventArgs e)
        {
            float denom = UtilizationRateCalculate.GetDurationSeconds_ByMinuteDiff_NoOvernight(dateTimePicker_start4, dateTimePicker_end4);
            // 避免除以 0
            if (denom <= 0)
            {
                lab_recordtime.Text = LanguageManager.Translate("UtilizationRate_lab_error");
                return;
            }

            var (startLocal, endLocal) = BuildStartEndLocal(dateTime_start, dateTimePicker_start4, dateTimePicker_end4,
                                                       allowOvernight: false, equalMeansOneHour: false);

            // 再轉 UTC
            var (utcStart, utcEnd) = UtilizationRateCalculate.ToUtcRange(startLocal, endLocal);
            Drill_UtilizationRate = UtilizationRateCalculate.Get_UtilizationRate(utcStart, utcEnd, "Drill");
            Sawing_UtilizationRate = UtilizationRateCalculate.Get_UtilizationRate(utcStart, utcEnd, "Sawing");
            RenderUtilizationGauge(Drill_UtilizationRate, denom, ref _drillPanel, this, "drillPanel", new Point(60, 40), new Size(150, 150));
            RenderUtilizationGauge(Sawing_UtilizationRate, denom, ref _sawingPanel, this, "sawingPanel", new Point(300, 40), new Size(150, 150));

        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitUtilizationPickers()
        {
            _suppressAutoSave = true;

            var shifts = UtilizationConfigLoader.LoadShifts();

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
                case 1: return (dateTimePicker_start1, dateTimePicker_end1,btn_calculate1);
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
        /// 建立頁面
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        // 建立或取回面板（不存在就 new 並加入到表單）
        private UtilizationPanel EnsurePanel(
            ref UtilizationPanel panel, Control parent, string name, Point location, Size size)
        {
            if (panel == null || panel.IsDisposed)
            {
                panel = new UtilizationPanel
                {
                    Name = name,
                    Location = location,
                    Size = size,
                };
                parent.Controls.Add(panel);
            }
            return panel;
        }

        /// <summary>
        /// 計算並更新單一機台的稼動率圓環圖
        /// </summary>
        /// <param name="machineName"></param>
        /// <param name="utcStart"></param>
        /// <param name="utcEnd"></param>
        /// <param name="denom"></param>
        /// <param name="panel"></param>
        /// <param name="parent"></param>
        /// <param name="panelName"></param>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="usedColor"></param>
        /// <param name="remainingColor"></param>
        /// <param name="middleRingColor"></param>
        /// <param name="middleRingRatio"></param>
        /// <param name="holeRatio"></param>
        /// <param name="centerTextFontSize"></param>
        private void RenderUtilizationGauge(
            float numerator,
            float denom,
            ref UtilizationPanel panel,
            Control parent, string panelName, Point location, Size size,
            Color? usedColor = null, Color? remainingColor = null, Color? middleRingColor = null,
            float middleRingRatio = 0.2f, float holeRatio = 0.12f, float centerTextFontSize = 20f)
        {
            // 分子

            // 百分比（保護 denom）
            double pct = denom <= 0 ? 0 : (numerator / denom) * 100.0;
            string percentage = pct.ToString("0");

            // 確保面板存在
            var p = EnsurePanel(ref panel, parent, panelName, location, size);

            // 套值
            p.SetValues(
                used: numerator,
                total: denom,
                centerText: percentage + "%",
                usedColor: usedColor ?? Color.ForestGreen,
                remainingColor: remainingColor ?? Color.Gainsboro,
                middleRingColor: middleRingColor ?? Color.White,
                middleRingRatio: middleRingRatio,
                holeRatio: holeRatio,
                centerTextFontSize: centerTextFontSize
            );
        }
        private void SaveUtilizationPair( DateTimePicker startPicker, DateTimePicker endPicker,int shiftNo,string jsonPath = "UtilizationShiftConfig.json")
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
        private static void SyncEndNotEarlier(
            DateTimePicker start,
            DateTimePicker end,
            bool timeOnly = true,
            bool allowEqual = true)
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
        private static void WireTimePair(
            DateTimePicker start,
            DateTimePicker end,
            bool timeOnly = true,
            bool allowEqual = true)
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

    }
}
