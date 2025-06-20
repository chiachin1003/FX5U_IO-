using SLMP;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using System.Diagnostics;
using static FX5U_IOMonitor.Check_point;



namespace FX5U_IOMonitor
{
    public partial class Sawingband_Info : Form
    {
        private CancellationTokenSource? _cts;

        public Sawingband_Info()
        {
            InitializeComponent();
            this.Load += sawband_Info_Load;
            this.FormClosing += Sawingband_Info_FormClosing;
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            
            SwitchLanguage();
        }


        private void sawband_Info_Load(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;


            var existingContext = MachineHub.Get("Sawing");
            if (existingContext != null && existingContext.IsConnected)
            {
                reset_lab_connectText(); // 初始顯示一次

                _cts = new CancellationTokenSource();
                _ = Task.Run(() => AutoUpdateAsync(_cts.Token)); // 啟動背景更新任務

            }
            else
            {
                reset_lab_connectText(); // 初始顯示一次

            }
        }


        private async Task AutoUpdateAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Checkpoint_time.Start("Sawband_main");

                try
                {
                    // 主執行緒呼叫 UI 更新
                    if (this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.Invoke(() =>
                        {
                            reset_lab_connectText(); // 每次自動更新畫面數值
                        });
                    }

                    await Task.Delay(900, token); // 每900毫秒更新一次
                }
                catch (OperationCanceledException)
                {
                    break; // 正常取消任務
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("背景更新錯誤：" + ex.Message);
                }
                Checkpoint_time.Stop("Sawband_main");
                this.Invoke(() =>
                {
                    if (Checkpoint_time.GetElapsedMilliseconds("Sawband_main") < 550)
                    {
                        lab_time.Text = Checkpoint_time.GetFormattedTime("Sawband_main");
                    }
                   
                });
            }
        }

        private void reset_lab_connectText()
        {
            lab_Sawband_brand.Text = DBfunction.Get_Machine_text("Sawband_brand");
            lab_Sawblade_material.Text = DBfunction.Get_Machine_text("Sawblade_material");
            lab_Sawblade_type.Text = DBfunction.Get_Machine_text("Sawblade_type");
            lab_Sawblade_teeth.Text = DBfunction.Get_Machine_text("Sawblade_teeth") + "  (TOOL / INCH)";
            lab_Sawband_speed.Text = DBfunction.Get_Machine_text("Sawband_speed") + Text_design.ConvertUnitLabel("(m/min)");
            lab_Sawband_motors_usetime.Text = DBfunction.Get_Machine_text("Sawband_motors_usetime");

            lab_Sawband_power.Text = DBfunction.Get_Machine_text("Sawband_power").ToString() + " (Hp)";
            lab_Sawband_current.Text = DBfunction.Get_Machine_text("Sawband_current").ToString() + "(A)";
            lab_Sawband_area.Text = DBfunction.Get_Machine_text("Sawband_area").ToString() + Text_design.ConvertUnitLabel("(m²)");
            lab_saw_life.Text = DBfunction.Get_Machine_text("Sawband_life").ToString();
            lab_Sawband_tension.Text = DBfunction.Get_Machine_text("Sawband_tension").ToString();


        }

        private void Sawingband_Info_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _cts?.Cancel(); // 關閉時自動取消背景任務
        }

        private void SwitchLanguage()
        {
            string lang = Properties.Settings.Default.LanguageSetting;
            this.Text = LanguageManager.Translate("SawingInfo_FormText");
            lab_Sawband_brandText.Text = LanguageManager.Translate("SawingInfo_SawbandbrandText");
            lab_Sawblade_materialText.Text = LanguageManager.Translate("SawingInfo_SawbladematerialText");
            lab_Sawblade_typeText.Text = LanguageManager.Translate("SawingInfo_SawbladetypeText");
            lab_Sawblade_teethText.Text = LanguageManager.Translate("SawingInfo_SawbladeteethText");
            lab_Sawband_speedText.Text = LanguageManager.Translate("SawingInfo_SawbandspeedText");
            lab_Sawband_motors_usetimeText.Text = LanguageManager.Translate("SawingInfo_SawbandmotorsusetimeText");
            lab_Sawband_powerText.Text = LanguageManager.Translate("SawingInfo_SawbandpowerText");
            lab_Sawband_currentText.Text = LanguageManager.Translate("SawingInfo_SawbandcurrentText");
            lab_Sawband_areaText.Text = LanguageManager.Translate("SawingInfo_SawbandareaText");
            lab_saw_lifeText.Text = LanguageManager.Translate("SawingInfo_sawlifeText");
            lab_Sawband_tensionText.Text = LanguageManager.Translate("SawingInfo_SawbandtensionText");

            // 設定字體大小（英文縮小）
            float fontSize = (lang == "en-US") ? 10.5f : 12.0f;
            // 這些是你要縮放字體的 Label
            Label[] labelsToResize =
            {
                lab_Sawband_brandText,
                lab_Sawblade_materialText,
                lab_Sawblade_typeText,
                lab_Sawblade_teethText,
                lab_Sawband_speedText,
                lab_Sawband_motors_usetimeText,
                lab_Sawband_powerText,
                lab_Sawband_currentText,
                lab_Sawband_areaText,
                lab_saw_lifeText,
                lab_Sawband_tensionText
            };

            foreach (var label in labelsToResize)
            {
                label.Font = new Font(label.Font.FontFamily, fontSize, label.Font.Style);
            }
        }
    }
}
