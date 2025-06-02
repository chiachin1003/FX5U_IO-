using SLMP;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json.Linq;



namespace FX5U_IOMonitor
{
    public partial class Drill_Info : Form
    {
        private CancellationTokenSource? _cts;
        private double elapsedSeconds;
        public Drill_Info()
        {
            InitializeComponent();
            this.Load += Main_Load;
            this.FormClosing += Drill_Info_FormClosing;
            string lang = Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageCSV("language.csv", lang);
            SwitchLanguage();
            LanguageManager.LanguageChanged += OnLanguageChanged;

        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
        }


        private void Main_Load(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            var existingContext = MachineHub.Get("Drill");
            if (existingContext != null && existingContext.IsConnected)
            {
                reset_lab_connectText(); // 初始顯示一次

                _cts = new CancellationTokenSource();
                _ = Task.Run(() => AutoUpdateAsync(_cts.Token)); // 啟動背景更新任務

            }


        }

        private async Task AutoUpdateAsync(CancellationToken token)
        {
            var stopwatch = Stopwatch.StartNew(); 
            while (!token.IsCancellationRequested)
            {
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
            stopwatch.Stop(); 

            elapsedSeconds = stopwatch.ElapsedMilliseconds;
           
        }


        private void reset_lab_connectText()
        {

            lab_Drill_servo_usetime.Text = MonitorFunction.ConvertSecondsToDHMS((DBfunction.Get_History_NumericValue("Drill_servo_usetime") + (DBfunction.Get_Machine_number("Drill_servo_usetime"))));
            lab_Drill_spindle_usetime.Text = MonitorFunction.ConvertSecondsToDHMS((DBfunction.Get_History_NumericValue("Drill_spindle_usetime") + (DBfunction.Get_Machine_number("Drill_spindle_usetime"))));
            lab_Drill_plc_usetime.Text = MonitorFunction.ConvertSecondsToDHMS((DBfunction.Get_History_NumericValue("Drill_plc_usetime") + (DBfunction.Get_Machine_number("Drill_plc_usetime"))));
            lab_Drill_inverter.Text = MonitorFunction.ConvertSecondsToDHMS((DBfunction.Get_History_NumericValue("Drill_inverter") + (DBfunction.Get_Machine_number("Drill_inverter"))));
            lab_Drill_total_Time.Text = MonitorFunction.ConvertSecondsToDHMS((DBfunction.Get_History_NumericValue("Drill_total_Time") + (DBfunction.Get_Machine_number("Drill_total_Time"))));

            lab_Drill_origin.Text = DBfunction.Get_History_NumericValue("Drill_origin").ToString() + " 次 ";
            lab_Drill_loose_tools.Text = DBfunction.Get_History_NumericValue("Drill_loose_tools").ToString() + " 次 ";
            lab_Drill_measurement.Text = DBfunction.Get_History_NumericValue("Drill_measurement").ToString() + " 次 ";
            lab_Drill_clamping.Text = DBfunction.Get_History_NumericValue("Drill_clamping").ToString() + " 次 ";
            lab_Drill_feeder.Text = DBfunction.Get_History_NumericValue("Drill_feeder").ToString() + " 次 ";

        }


        private void Drill_Info_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _cts?.Cancel(); // 關閉時自動取消背景任務
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("當前介面更新速度"+ elapsedSeconds.ToString() +"毫秒");
        }
        private void SwitchLanguage()
        {
            lab_titleText.Text = LanguageManager.Translate("DrillInfo_title");
            lab_title.Text = LanguageManager.Translate("DrillInfo_titleTime");
            lab_Drill_servo_usetimeText.Text = LanguageManager.Translate("DrillInfo_DrillservousetimeText");
            lab_Drill_spindle_usetimeText.Text = LanguageManager.Translate("DrillInfo_DrillspindleusetimeText");
            lab_Drill_plc_usetimeText.Text = LanguageManager.Translate("DrillInfo_DrillplcusetimeText");
            lab_Drill_inverterText.Text = LanguageManager.Translate("DrillInfo_DrillinverterText");
            lab_Drill_total_TimeText.Text = LanguageManager.Translate("DrillInfo_DrilltotalTimeText");
            lab_Drill_originText.Text = LanguageManager.Translate("DrillInfo_DrilloriginText");
            lab_Drill_loose_toolsText.Text = LanguageManager.Translate("DrillInfo_DrillloosetoolsText");
            lab_Drill_measurementText.Text = LanguageManager.Translate("DrillInfo_DrillmeasurementText");
            lab_Drill_clampingText.Text = LanguageManager.Translate("DrillInfo_DrillclampingText");
            lab_Drill_feederText.Text = LanguageManager.Translate("DrillInfo_DrillfeederText");
        }
    }
}
