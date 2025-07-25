using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using SLMP;
using System.Diagnostics;
using System.Threading;
using static FX5U_IOMonitor.Check_point;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Email.DailyTask_config;
using static FX5U_IOMonitor.Models.MonitoringService;
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
        // 資訊卡加入(累計次數/或面積，純粹數值類型)
        private readonly Dictionary<string, MachineActiveCard> countCardMap = new();

        private readonly ScheduleFrequency history_Frequency = ScheduleFrequency.Monthly;

        // 依序加入所需欄位(靜態)
        private readonly List<(string Param, string LangKey, string Unit)> infoCardList = new()
        {
            ("Sawband_brand",      "SawingInfo_SawbandbrandText",     ""),
            ("Sawblade_material",  "SawingInfo_SawbladematerialText", ""),
            ("Sawblade_type",      "SawingInfo_SawbladetypeText",     ""),
            ("Sawblade_teeth",     "SawingInfo_SawbladeteethText",    "(TOOL / INCH)"),
            ("Sawband_speed",      "SawingInfo_SawbandspeedText",     "(m/min)"),
            ("Sawband_power",      "SawingInfo_SawbandpowerText",     "(Hp)"),
            ("Sawband_current",    "SawingInfo_SawbandcurrentText",   "(A)")
        };

        // 依序加入所需欄位(動態時間)
        private readonly List<(string Param, string LangKey)> timeCardList = new()
        {
            ("Sawband_life",           "SawingInfo_sawlifeText"),
            ("Sawband_tension",        "SawingInfo_SawbandtensionText"),
            ("Sawband_motors_usetime", "SawingInfo_SawbandmotorsusetimeText")
        };

        // 依序加入所需欄位(數值)
        private readonly List<(string Param, string LangKey)> countCardList = new()
        {
            ("Sawband_area", "SawingInfo_SawbandareaText")
        };

        public Saw_Info()
        {
            InitializeComponent();
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
            InitCountCards();

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
                string val = DBfunction.Get_Machine_text(param);
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

        private void InitCountCards()
        {
            foreach (var (param, key) in countCardList)
            {
                var card = new MachineActiveCard { DisplayMode = CardDisplayMode.Count };
                FillCountCard(card, param, key);
                countCardMap[param] = card;
                flowLayoutPanel1.Controls.Add(card);
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
      
        private void UpdateTimeCards()
        {
            foreach (var (param, key) in timeCardList)
            {
                if (!timeCardMap.TryGetValue(param, out var card)) continue;
                FillTimeCard(card, param, key);
            }
        }
        private void UpdateCountCards()
        {
            foreach (var (param, key) in countCardList)
            {
                if (!countCardMap.TryGetValue(param, out var card)) continue;
                FillCountCard(card, param, key);
            }
        }

        /* ========== 加入卡片資訊 ========== */

        /// <summary>共用邏輯：依 paramName 填入時間卡內容</summary>
        private void FillTimeCard(MachineActiveCard card, string paramName, string langKey)
        {
            // 當前累積秒數（歷史 + 即時）
            int totalSec = DBfunction.Get_Machine_History_NumericValue(paramName) +
                           DBfunction.Get_Machine_number(paramName);

            string nowTime = MonitorFunction.ConvertSecondsToDHMS(totalSec);

            // 取最新兩筆歷史紀錄
            var latest = DBfunction.GetLatestHistoryRecordByName(paramName, history_Frequency);
            var second = DBfunction.GetSecondLatestHistoryRecordByName(paramName, history_Frequency);

            int prevVal = second?.Delta ?? 0;
            int thisVal = latest?.Delta ?? totalSec;

            string range = $"{DBfunction.Get_Machine_creatTime(paramName):yyyy/MM/dd HH:mm} ~ " +
                           $"{DateTime.UtcNow:yyyy/MM/dd HH:mm}";

            card.SetData(LanguageManager.Translate(langKey), nowTime,
                         prevVal, thisVal, range, history_Frequency);
        }
        private void FillCountCard(MachineActiveCard card, string paramName, string langKey)
        {
            int totalCount = DBfunction.Get_Machine_History_NumericValue(paramName);

            var latest = DBfunction.GetLatestHistoryRecordByName(paramName, history_Frequency);
            var second = DBfunction.GetSecondLatestHistoryRecordByName(paramName, history_Frequency);

            int prevVal = second?.Delta ?? 0;
            int thisVal = latest?.Delta ?? totalCount;

            string range = $"{DBfunction.Get_Machine_creatTime(paramName):yyyy/MM/dd HH:mm} ~ " +
                           $"{DateTime.UtcNow:yyyy/MM/dd HH:mm}";

            card.SetData(LanguageManager.Translate(langKey), totalCount.ToString(),
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
                            UpdateCountCards();

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
            this.Text = LanguageManager.Translate("SawingInfo_FormText");
            UpdateInfoCards();
            UpdateTimeCards();
            UpdateCountCards();

        }
    }
}
