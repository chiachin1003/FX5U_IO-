using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Scheduling;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FX5U_IOMonitor.Data.GlobalMachineHub;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;

namespace FX5U_IOMonitor.panel_control
{
    public partial class Machine_monitoring_interface_card : Form
    {
        private CancellationTokenSource? _cts;
        ScheduleFrequency history_Frequency;
        private Dictionary<string, MachineActiveCard> cardMap = new();
        private Dictionary<string, MachinePowerCard> powerCardMap = new();
        private readonly Dictionary<string, MachineInfoCard> InfoCardMap = new();

        public Machine_monitoring_interface_card(ScheduleFrequency scheduleFrequency)
        {
            InitializeComponent();
            history_Frequency = scheduleFrequency;
            this.Load += Machine_monitoring_interface_card_Load;
            this.FormClosing += Machine_monitoring_interface_card_FormClosing;
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            SwitchLanguage();
            LanguageManager.LanguageChanged += OnLanguageChanged;

        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
        }
        private void Machine_monitoring_interface_card_Load(object sender, EventArgs e)
        {
            var existingContext = GetContext("Drill") as IMachineContext;
            if (existingContext != null && existingContext.IsConnected)
            {
                InitCards(); 
                InitPowerCards();
                InitInfoCards();
                _cts = new CancellationTokenSource();
                _ = Task.Run(() => AutoUpdateAsync(_cts.Token)); // 啟動背景更新任務

            }
            else
            {
                InitCards(); 
                InitPowerCards();
                InitInfoCards();
            }


        }
        private void Machine_monitoring_interface_card_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _cts?.Cancel(); // 關閉時自動取消背景任務
        }


        private List<(string ParamName, string LangKey)> timeCardSourceList = new()
        {
            ("Drill_servo_usetime", "DrillInfo_DrillservousetimeText"),
            ("Drill_spindle1_usetime", "DrillInfo_Drillspindle1usetimeText"),
            ("Drill_spindle2_usetime", "DrillInfo_Drillspindle2usetimeText"),
            ("Drill_spindle3_usetime", "DrillInfo_Drillspindle3usetimeText"),
            ("Drill_plc_usetime", "DrillInfo_DrillplcusetimeText"),
            ("Drill_inverter", "DrillInfo_DrillinverterText"),
            ("Drill_outverter", "DrillInfo_DrilloutverterText"),
            ("Drill_total_Time", "DrillInfo_DrilltotalTimeText"),

        };
        private List<(string ParamName, string LangKey)> countCardSourceList = new()
        {
            ("Drill_origin","DrillInfo_DrilloriginText"),
            ("Drill_loose_tools_1","DrillInfo_DrillloosetoolsText"),
            ("Drill_loose_tools_2","DrillInfo_DrillloosetoolsText_2"),
            ("Drill_loose_tools_3","DrillInfo_DrillloosetoolsText_3"),

            ("Drill_measurement_1","DrillInfo_DrillmeasurementText"),
            ("Drill_measurement_2","DrillInfo_DrillmeasurementText_2"),
            ("Drill_measurement_3","DrillInfo_DrillmeasurementText_3"),

            ("Drill_clamping","DrillInfo_DrillclampingText")

        };
        private readonly List<(string Param, string LangKey, string Unit)> powerCardList = new()
        {
            ("electricity",    "DrillInfo_ElectricityText",   "(kWh)")
        };
        private readonly List<(string Param, string LangKey, string Unit)> infoCardList = new()
        {
            ("voltage",     "DrillInfo_VoltageText",    "(V)"),
            ("current",      "DrillInfo_CurrentText",     "(A)"),
            ("power",      "DrillInfo_PowerText",     "(kW)"),
        };

        private void InitCards()
        {
            flowLayoutPanel1.Controls.Clear();
            cardMap.Clear();

            foreach (var (paramName, langKey) in timeCardSourceList)
            {
                string title = LanguageManager.Translate(langKey);
                int seconds = DBfunction.Get_Machine_History_NumericValue("Drill", paramName) + DBfunction.Get_Machine_number(paramName);
                string time = MonitorFunction.ConvertSecondsToDHMS(seconds);

                var card = new MachineActiveCard();
                card.DisplayMode = CardDisplayMode.Time;
                var record_late = DBfunction.GetLatestHistoryRecordByName("Drill",paramName, history_Frequency);
                var SecondLate = DBfunction.GetSecondLatestHistoryRecordByName("Drill", paramName, history_Frequency);
                DateTime now = DateTime.UtcNow;
                string re = DBfunction.Get_Machine_creatTime(paramName).ToString("yyyy/MM/dd HH:mm") + "~" + now.ToString("yyyy/MM/dd HH:mm");
                
                
                if (SecondLate == null)
                {
                    record_late.Delta = seconds;
                    int secondDelta = 0;
                    card.SetData(title, time, secondDelta, record_late.Delta, re, history_Frequency);
                }
                else
                {
                    var secondDelta = SecondLate.Delta;
                    card.SetData(title, time, secondDelta, record_late.Delta, re, history_Frequency);
                }

                cardMap[paramName] = card;
                flowLayoutPanel1.Controls.Add(card);

            }

            foreach (var (paramName, langKey) in countCardSourceList)
            {
                string title = LanguageManager.Translate(langKey);
                int count = DBfunction.Get_Machine_History_NumericValue("Drill", paramName);
                string display = count.ToString(); // 直接顯示次數
                var card_count = new MachineActiveCard();
                card_count.DisplayMode = CardDisplayMode.Count;
                var record_late = DBfunction.GetLatestHistoryRecordByName("Drill", paramName, history_Frequency);
                var SecondLate = DBfunction.GetSecondLatestHistoryRecordByName("Drill", paramName, history_Frequency);
                DateTime now = DateTime.UtcNow;
                string re = DBfunction.Get_Machine_creatTime(paramName).ToString("yyyy/MM/dd HH:mm") + "~" + now.ToString("yyyy/MM/dd HH:mm");
                if (SecondLate == null)
                {
                    record_late.Delta = count;
                    int secondDelta = 0;
                    card_count.SetData(title, display, secondDelta, record_late.Delta, re, history_Frequency);

                }
                else
                {
                    var secondDelta = SecondLate.Delta;
                    card_count.SetData(title, display, secondDelta, record_late.Delta, re, history_Frequency);
                }
                cardMap[paramName] = card_count;
                flowLayoutPanel1.Controls.Add(card_count);
            }
        }
        private void InitPowerCards()
        {
            powerCardMap.Clear(); // 確保不重複

            foreach (var (param, key, unit) in powerCardList)
            {

                var card = new MachinePowerCard();
                string title = LanguageManager.Translate(key);
                string val = DBfunction.Get_Machine_now_string("Drill", param);

                var record_late = DBfunction.GetLatestHistoryRecordByName("Drill", param, history_Frequency);
                var SecondLate = DBfunction.GetSecondLatestHistoryRecordByName("Drill", param, history_Frequency);

                DateTime now = DateTime.UtcNow;
                string re = DBfunction.Get_Machine_creatTime(param).ToString("yyyy/MM/dd HH:mm") + "~" + now.ToString("yyyy/MM/dd HH:mm");
                
                if (SecondLate == null)
                {
                    int secondDelta = 0;
                    if (record_late == null)
                    {
                        int record_lateDelta = 0;
                        card.SetData(title, val, unit, re, secondDelta, record_lateDelta, history_Frequency);
                    }
                    else
                    {
                        card.SetData(title, val, unit, re, secondDelta, record_late.Delta, history_Frequency);
                    }
                }
                else
                {
                    var secondDelta = SecondLate.Delta;
                    card.SetData(title, val, unit, re, secondDelta, record_late.Delta, history_Frequency);
                }


                powerCardMap[param] = card;
                flowLayoutPanel1.Controls.Add(card);
            }

           
        }
        private void InitInfoCards()
        {
            InfoCardMap.Clear(); // 確保不重複

            foreach (var (param, key, unit) in infoCardList)
            {

                var card = new MachineInfoCard();
                string title = LanguageManager.Translate(key);
                string val = DBfunction.Get_Machine_now_string("Drill", param);

                var record_late = DBfunction.GetLatestHistoryRecordByName("Drill", param, history_Frequency);
                var SecondLate = DBfunction.GetSecondLatestHistoryRecordByName("Drill", param, history_Frequency);

                DateTime now = DateTime.UtcNow;
              

                if (SecondLate == null)
                {
                    int secondDelta = 0;
                    if (record_late == null)
                    {
                        int record_lateDelta = 0;
                        card.SetData(title, val, unit);
                    }
                    else
                    {
                        card.SetData(title, val, unit);
                    }
                }
                else
                {
                    var secondDelta = SecondLate.Delta;
                    card.SetData(title, val, unit);
                }


                InfoCardMap[param] = card;
                flowLayoutPanel1.Controls.Add(card);
            }


        }

        private void UpdateCardValues()
        {
            foreach (var (paramName, langKey) in timeCardSourceList)
            {
                if (!cardMap.ContainsKey(paramName)) continue;

                int seconds = DBfunction.Get_Machine_History_NumericValue("Drill",paramName) + DBfunction.Get_Machine_number("Drill",paramName);
                string time = MonitorFunction.ConvertSecondsToDHMS(seconds);
                var record_late = DBfunction.GetLatestHistoryRecordByName("Drill",paramName, history_Frequency);
                var SecondLate = DBfunction.GetSecondLatestHistoryRecordByName("Drill", paramName, history_Frequency);
                DateTime now = DateTime.UtcNow;

                string re = DBfunction.Get_Machine_creatTime(paramName).ToString("yyyy/MM/dd HH:mm") + "~" + now.ToString("yyyy/MM/dd HH:mm");
                if (SecondLate == null)
                {
                    record_late.Delta = seconds;
                    int secondDelta = 0;
                    cardMap[paramName].SetData(LanguageManager.Translate(langKey), time, secondDelta, record_late.Delta, re, history_Frequency);
                }
                else
                {
                    var secondDelta = SecondLate.Delta;
                    cardMap[paramName].SetData(LanguageManager.Translate(langKey), time, secondDelta, record_late.Delta, re, history_Frequency);
                }



            }

            foreach (var (paramName, langKey) in countCardSourceList)
            {
                if (!cardMap.ContainsKey(paramName)) continue;

                int count = DBfunction.Get_Machine_History_NumericValue("Drill",paramName);
                var record_late = DBfunction.GetLatestHistoryRecordByName("Drill", paramName, history_Frequency);
                var SecondLate = DBfunction.GetSecondLatestHistoryRecordByName("Drill", paramName, history_Frequency);
                DateTime now = DateTime.UtcNow;
                string re = DBfunction.Get_Machine_creatTime(paramName).ToString("yyyy/MM/dd HH:mm") + "~" + now.ToString("yyyy/MM/dd HH:mm");
                if (SecondLate == null)
                {
                    record_late.Delta = count;
                    int secondDelta = 0;
                    cardMap[paramName].SetData(LanguageManager.Translate(langKey), count.ToString(), secondDelta, record_late.Delta, re, history_Frequency);

                }
                else
                {
                    var secondDelta = SecondLate.Delta;
                    cardMap[paramName].SetData(LanguageManager.Translate(langKey), count.ToString(), SecondLate.Delta, record_late.Delta, re, history_Frequency);

                }

            }
        }
        private void UpdatepowerCards()
        {
            foreach (var (param, key, unit) in powerCardList)
            {
                if (!powerCardMap.TryGetValue(param, out var card)) continue;

                // 即時值
                string val = DBfunction.Get_Machine_now_string("Drill", param);

                // 安全轉 float（避免顯示錯誤）
                if (!float.TryParse(val, out float valNum))
                    valNum = 0;
                string valStr = valNum.ToString("0.##");

                // 歷史統計資料
                var record_late = DBfunction.GetLatestHistoryRecordByName("Drill", param, history_Frequency);
                var SecondLate = DBfunction.GetSecondLatestHistoryRecordByName("Drill", param, history_Frequency);

                DateTime now = DateTime.UtcNow;
                string re = DBfunction.Get_Machine_creatTime(param).ToString("yyyy/MM/dd HH:mm") + "~" + now.ToString("yyyy/MM/dd HH:mm");

                int deltaNow = record_late?.Delta ?? 0;
                int deltaPrev = SecondLate?.Delta ?? 0;

                card.SetData(
                    LanguageManager.Translate(key),
                    valStr,
                    Text_design.ConvertUnitLabel(unit),
                    re,
                    deltaPrev,
                    deltaNow,
                    history_Frequency
                );
            }
        }
        private void UpdateInfoCards()
        {
            foreach (var (param, key, unit) in infoCardList)
            {
                if (!InfoCardMap.TryGetValue(param, out var card)) continue;

                // 即時值
                string val = DBfunction.Get_Machine_now_string("Drill", param);

                // 安全轉 float（避免顯示錯誤）
                if (!float.TryParse(val, out float valNum))
                    valNum = 0;
                string valStr = valNum.ToString("0.##");

                card.SetData(
                    LanguageManager.Translate(key),
                    valStr,
                    Text_design.ConvertUnitLabel(unit)                  
                );
            }
        }
        private async Task AutoUpdateAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {

                try
                {
                    // 主執行緒呼叫 UI 更新
                    if (this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.Invoke(() =>
                        {
                            UpdateCardValues(); // 每次自動更新畫面數值
                            UpdatepowerCards();
                            UpdateInfoCards();
                        });
                    }

                    await Task.Delay(500, token); // 每秒更新一次
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
        private void SwitchLanguage()
        {
            this.Text = LanguageManager.Translate("DrillInfo_FormText");

        }

    }
}
