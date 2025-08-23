using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.MitsubishiPlc_Monior;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using SLMP;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;
using static FX5U_IOMonitor.Check_point;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace FX5U_IOMonitor
{
    public partial class Saw_Info : Form
    {
        private CancellationTokenSource? _cts;

        //  資訊卡加入(靜態)
        private readonly Dictionary<string, MachineInfoCard> infoCardMap = new();
        // 資訊卡加入(累計時間)
        private readonly Dictionary<string, MachineActiveCard> timeCardMap = new();
        private readonly Dictionary<string, MachinePowerCard> powerCardMap = new();

        // 資訊卡加入(累計次數/或面積，純粹數值類型)

        private readonly ScheduleFrequency history_Frequency ;

        // 依序加入所需欄位(靜態)
        private readonly List<(string Param, string LangKey, string Unit)> infoCardList = new()
        {
            ("cuttingspeed",  "SawInfo_CuttingSpeed", "(mm/min)"),
            ("remain_tools",    "SawInfo_RemainingTools",   ""),
            ("oil_pressure",      "SawInfo_HydraulicTensionStatus",     "")
        };
        private readonly List<(string Param, string LangKey, string Unit)> PowerCardList = new()
        {
             ("motor_current",      "SawInfo_MotorCurrent",     "(A)"),
            ("current",     "DrillInfo_CurrentText",    "(A)"),
            ("voltage",      "SawInfo_Voltage",     "(V)"),
            ("power",      "SawInfo_Power",     "(kW)"),
            ("electricity",    "SawInfo_PowerConsumption",   "kWh")

        };

        // 依序加入所需欄位(動態時間)
        private readonly List<(string Param, string LangKey)> timeCardList = new()
        {
            ("total_time",      "SawInfo_TotalRuntime"),
             ("countdown_time",      "SawInfo_ProcessingTime")


        };
     

        public Saw_Info(ScheduleFrequency Frequency)
        {
            InitializeComponent();
            history_Frequency = Frequency;
            this.Load += Saw_Info_Load;
            this.FormClosing += (_, __) => _cts?.Cancel();

            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            SwitchLanguage();
            LanguageManager.LanguageChanged += _ => SwitchLanguage();
        }

        /* ---------- 表單載入 ---------- */
        private void Saw_Info_Load(object? sender, EventArgs e)
        {
            InitInfoCards();
            InitTimeCards();
            InitPowerCards();
            // 如果 PLC 已連線就開始背景更新
            if (MachineHub.Get("Sawing")?.IsConnected == true)
            {
                _cts = new CancellationTokenSource();
                _ = Task.Run(() => AutoUpdateAsync(_cts.Token));
            }
        }

        /* ========== 靜態卡片：MachineInfoCard ========== */
        private void InitInfoCards()
        {
            flowLayoutPanel1.Controls.Clear();
            infoCardMap.Clear();

            foreach (var (param, key, unit) in infoCardList)
            {
                var card = new MachineInfoCard();
                string val = DBfunction.Get_Machine_now_string("Sawing", param);
                card.SetData(LanguageManager.Translate(key), val, Text_design.ConvertUnitLabel(unit));

                infoCardMap[param] = card;
                flowLayoutPanel1.Controls.Add(card);
            }
        }
        private void InitTimeCards()
        {
            timeCardMap.Clear();

            foreach (var (param, key) in timeCardList)
            {
                var card = new MachineActiveCard { DisplayMode = CardDisplayMode.Time };
                FillTimeCard(card, param, key);  // 共用方法
                timeCardMap[param] = card;
                flowLayoutPanel1.Controls.Add(card);
            }
        }
        private void InitPowerCards()
        {
            powerCardMap.Clear(); // 確保不重複

            foreach (var (param, key, unit) in PowerCardList)
            {

                var powercard = new MachinePowerCard();
                string title = LanguageManager.Translate(key);
                string val = DBfunction.Get_Machine_now_string("Sawing", param);

                var record_late = DBfunction.GetLatestHistoryRecordByName("Sawing", param, history_Frequency);
                var SecondLate = DBfunction.GetSecondLatestHistoryRecordByName("Sawing", param, history_Frequency);

                DateTime now = DateTime.UtcNow;
                string re = DBfunction.Get_Machine_creatTime(param).ToString("yyyy/MM/dd HH:mm") + "~" + now.ToString("yyyy/MM/dd HH:mm");

                if (SecondLate == null)
                {
                    int secondDelta = 0;
                    powercard.SetData(title, val, unit, re, secondDelta, record_late.Delta, history_Frequency);
                }
                else
                {
                    var secondDelta = SecondLate.Delta;
                    powercard.SetData(title, val, unit, re, secondDelta, record_late.Delta, history_Frequency);
                }


                powerCardMap[param] = powercard;
                flowLayoutPanel1.Controls.Add(powercard);
            }


        }
        /* ========== 更新卡片資訊 ========== */

        private void UpdateInfoCards()
        {
            foreach (var (param, key, unit) in infoCardList)
            {
                if (!infoCardMap.TryGetValue(param, out var card)) continue;
                string val = DBfunction.Get_Machine_text(param);
                card.SetData(LanguageManager.Translate(key), val, Text_design.ConvertUnitLabel(unit));
            }
        }
        private void UpdatePowerCards()
        {
            foreach (var (param, key, unit) in PowerCardList)
            {
                if (!powerCardMap.TryGetValue(param, out var card))
                    continue;

                string val = DBfunction.Get_Machine_now_string("Sawing", param);

                var record_late = DBfunction.GetLatestHistoryRecordByName("Sawing", param, history_Frequency);
                var secondLate = DBfunction.GetSecondLatestHistoryRecordByName("Sawing", param, history_Frequency);

                string range = $"{DBfunction.Get_Machine_creatTime(param):yyyy/MM/dd HH:mm} ~ {DateTime.UtcNow:yyyy/MM/dd HH:mm}";

                int secondDelta = secondLate?.Delta ?? 0;
                int latestDelta = record_late?.Delta ?? 0;

                card.SetData(
                    LanguageManager.Translate(key),
                    val,
                    unit,
                    range,
                    secondDelta,
                    latestDelta,
                    history_Frequency
                );
            }
        }
        private void UpdateTimeCards()
        {
            foreach (var (param, key) in timeCardList)
            {
                if (!timeCardMap.TryGetValue(param, out var card)) continue;
                FillTimeCard(card, param, key);
            }
        }
       
        /* ========== 加入卡片資訊 ========== */

        /// <summary>共用邏輯：依 paramName 填入時間卡內容</summary>
        private void FillTimeCard(MachineActiveCard card, string paramName, string langKey)
        {
            // 當前累積秒數（歷史 + 即時）
            int totalSec = DBfunction.Get_Machine_History_NumericValue("Sawing",paramName) +
                           DBfunction.Get_Machine_number(paramName);

            string nowTime = MonitorFunction.ConvertSecondsToDHMS(totalSec);

            // 取最新兩筆歷史紀錄
            var latest = DBfunction.GetLatestHistoryRecordByName("Sawing", paramName, history_Frequency);
            var second = DBfunction.GetSecondLatestHistoryRecordByName("Sawing", paramName, history_Frequency);

            int prevVal = second?.Delta ?? 0;
            int thisVal = latest?.Delta ?? totalSec;

            string range = $"{DBfunction.Get_Machine_creatTime(paramName):yyyy/MM/dd HH:mm} ~ " +
                           $"{DateTime.UtcNow:yyyy/MM/dd HH:mm}";

            card.SetData(LanguageManager.Translate(langKey), nowTime,
                         prevVal, thisVal, range, history_Frequency);
        }
        /* ---------- 背景更新 ---------- */
        private async Task AutoUpdateAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (IsHandleCreated && !IsDisposed)
                        Invoke(() =>
                        {
                            UpdateInfoCards();
                            UpdateTimeCards();
                            UpdatePowerCards();

                        });

                    await Task.Delay(900, token);
                }
                catch (OperationCanceledException) { break; }
                catch (Exception ex) { Debug.WriteLine($"BG Error: {ex.Message}"); }
            }
        }

        /* ---------- 語系切換 ---------- */
        private void SwitchLanguage()
        {
            this.Text = LanguageManager.Translate("Mainform_SawInfo");
            UpdateInfoCards();
            UpdateTimeCards();
            UpdatePowerCards();
        }
    }
}
