using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.MitsubishiPlc_Monior;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Utilization;
using Org.BouncyCastle.Utilities;
using SLMP;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;
using static FX5U_IOMonitor.Check_point;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;
using static FX5U_IOMonitor.Utilization.UtilizationRateCalculate;
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
            btn_yesterday.Text = LanguageManager.Translate("UtilizationRate_lab_yesterday");
            btn_today.Text = LanguageManager.Translate("UtilizationRate_lab_today");
            btn_thisweek.Text = LanguageManager.Translate("UtilizationRate_lab_thisweek");
            btn_lastweek.Text = LanguageManager.Translate("UtilizationRate_lab_lastweek");
            lab_Date.Text = LanguageManager.Translate("UtilizationRate_lab_Date");
            lab_Class.Text = LanguageManager.Translate("UtilizationRate_lab_Class");
            btn_setting.Text = LanguageManager.Translate("UtilizationRate_btn_setting");
            Text_design.SafeAdjustFont(btn_setting, btn_setting.Text);


        }

        private void UtilizationRate_Load(object sender, EventArgs e)
        {
            UtilizationRate_Inital();
            // 讀取預設模式（1~4）
            int mode = Properties.Settings.Default.DefaultUtilizationRate;
            _machineList = DBfunction.GetMachineIndexes();
            // 重新讀取班別設定
            Workshift = UtilizationConfigLoader.LoadShifts("UtilizationShiftConfig.json").Shifts;

            // 刷新下拉選單
            RefreshShiftComboBox(comb_class, Workshift);

            //comb_class.DataSource = Workshift
            //   .Where(s => s.Enabled)
            //   .Select(s => new { s.ShiftNo, Display = $"No. {s.ShiftNo} ({s.Start}-{s.End})" })
            //   .ToList();

            //comb_class.DisplayMember = "Display";
            //comb_class.ValueMember = "ShiftNo";
            //comb_class.SelectedValue = 1; // 預設第一班

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

                if (!shift.Enabled)
                {
                    startPicker.Enabled = false;
                    endPicker.Enabled = false;
                    button.Enabled = false;

                    button.BackColor = Color.LightGray;
                    button.ForeColor = Color.DarkGray;
                }
                else 
                {
                    startPicker.Enabled = true;
                    endPicker.Enabled = true;
                    button.Enabled = true;

                    // 🔹 啟用時綠色
                    button.BackColor = Color.MediumSeaGreen;
                    button.ForeColor = Color.White;
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
            if (shift == null)
            {
                shiftNo = 0;
            }

            DateTime selectedDate = dateTime_start.Value.Date;
          

            // 計算分母（該班別時間長度）
            var (sp, ep, button) = ResolvePickersByMode(shiftNo);
           
            float denom = _currentScope switch
            {
                TimeScope.Today => 3600 * 24f,
                TimeScope.Yesterday => 3600 * 24f,
                TimeScope.ThisWeek => 3600 * 24f * 7,
                TimeScope.LastWeek => 3600 * 24f * 7,
                _ => 3600 * 24f // 預設情況不變
            };

            // 排版
            int x = 60, y = 70;
            int spacingX = 220, spacingY = 220;
            int perRow = Math.Max(1, (this.Width - 120) / spacingX);
            int i = 0;
            List<ShiftResult> results = new();
            float cutting = 0;
            foreach (var machine in _machineList)
            {
                string machineName = machine.Name;
                if ( shiftNo == 0 )
                {
                    cutting = _currentScope switch
                    {
                        TimeScope.Today => UtilizationCalculator.GetDailyUtilization(machineName, selectedDate).RunSeconds,
                        TimeScope.Yesterday => UtilizationCalculator.GetDailyUtilization(machineName, (selectedDate.AddDays(-1))).RunSeconds,
                        TimeScope.ThisWeek => UtilizationCalculator.GetWeeklyUtilization(machineName, true).RunSeconds,
                        TimeScope.LastWeek => UtilizationCalculator.GetWeeklyUtilization(machineName, false).RunSeconds,
                        _ => UtilizationCalculator.GetDailyUtilization(machineName, selectedDate).RunSeconds // 預設值，避免 null
                    };
                }
                else 
                {
                    results = _currentScope switch
                    {
                        TimeScope.Today when selectedDate.Date != DateTime.Now.Date
                               => UtilizationRateCalculate.GetTodayCutting(machineName, Workshift, selectedDate),
                        TimeScope.Today
                               => UtilizationRateCalculate.GetTodayCutting(machineName, Workshift),
                        TimeScope.Yesterday => UtilizationRateCalculate.GetYesterdayCutting(machineName, Workshift, selectedDate),
                        // ✅ 本週但日期不在當前週 → 改走上週邏輯
                        TimeScope.ThisWeek when !IsSameWeek(selectedDate, DateTime.Now)
                            => UtilizationRateCalculate.GetThisWeekCutting(machineName, Workshift, selectedDate),
                        // ✅ 正常本週
                        TimeScope.ThisWeek
                            => UtilizationRateCalculate.GetThisWeekCutting(machineName, Workshift),
                        TimeScope.LastWeek => UtilizationRateCalculate.GetLastWeekCutting(machineName, Workshift, selectedDate),
                        _ => new List<ShiftResult>() // 預設值，避免 null
                    };
                    cutting = results.FirstOrDefault(r => r.ShiftNo == shiftNo)?.CuttingSeconds ?? 0;

                }

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
        private void RefreshShiftComboBox(System.Windows.Forms.ComboBox comb_class, List<UtilizationShiftConfig> Workshift)
        {
            // 建立資料來源，先加上「Total」選項
            var data = new List<object>
            {
                new { ShiftNo = 0, Display = "Total (00:00 - 23:59)" } // ⬅️ 第一個固定項目
            };

            // 加入所有啟用的班別
            data.AddRange(
                Workshift
                    .Where(s => s.Enabled)
                    .Select(s => new
                    {
                        s.ShiftNo,
                        Display = $"No. {s.ShiftNo} ({s.Start}-{s.End})"
                    })
                    .ToList()
            );

            // 設定 ComboBox 資料來源
            comb_class.DataSource = data;
            comb_class.DisplayMember = "Display";
            comb_class.ValueMember = "ShiftNo";

            // 預設選擇第一筆（Total）
            comb_class.SelectedValue = 0;
        }
        private static bool IsSameWeek(DateTime date1, DateTime date2)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            var calendar = culture.Calendar;

            var week1 = calendar.GetWeekOfYear(date1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var week2 = calendar.GetWeekOfYear(date2, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            return week1 == week2 && date1.Year == date2.Year;
        }
    }
}
