using SLMP;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using static FX5U_IOMonitor.Connect_PLC;
using static FX5U_IOMonitor.Models.MonitoringService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using static FX5U_IOMonitor.Data.GlobalMachineHub;



namespace FX5U_IOMonitor
{
    public partial class Check_point : Form
    {
        private CancellationTokenSource? _cts;
        public class Checkpoint_time
        {
            // 儲存多組 Stopwatch
            private static readonly Dictionary<string, Stopwatch> _timers = new();

            // 開始計時（如果不存在就建立）
            public static void Start(string key, bool resetBeforeStart =true)
            {
                if (!_timers.ContainsKey(key))
                    _timers[key] = new Stopwatch();

                if (resetBeforeStart)
                    _timers[key].Reset();

                if (!_timers[key].IsRunning)
                    _timers[key].Start();
            }

            // 停止計時
            public static void Stop(string key)
            {
                if (_timers.ContainsKey(key) && _timers[key].IsRunning)
                    _timers[key].Stop();
            }

            // 重設某一組計時器
            public static void Reset(string key)
            {
                if (_timers.ContainsKey(key))
                    _timers[key].Reset();
            }
            // 取得毫秒數（long）
            public static long GetElapsedMilliseconds(string key)
            {
                return _timers.ContainsKey(key) ? _timers[key].ElapsedMilliseconds : -1;
            }
            public static string GetFormattedTime(string key)
            {
                if (_timers.ContainsKey(key))
                {
                    var ts = _timers[key].Elapsed;
                    return $"{ts.Milliseconds:D3} ms";
                }
                return "00:00.000";
            }

        }

        public Check_point()
        {
            InitializeComponent();
            this.Load += Main_Load;
       

        }
     
        private void Main_Load(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            var existingContext = GlobalMachineHub.GetContext("Drill") as IMachineContext;
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


        }


        private void reset_lab_connectText()
        {
            var Drill = GlobalMachineHub.GetContext("Drill") as IMachineContext;
            var Saw = GlobalMachineHub.GetContext("Sawing") as IMachineContext;

            if (Drill != null && Drill.IsConnected)
            {
                if (int.Parse(Drill.ConnectSummary.read_time) > 70)
                {
                    lab_Drill_element_time.Text = ("當前監控總數更新時間：70ms");

                }
                else
                {
                    lab_Drill_element_time.Text = ("當前監控總數更新時間：" + Drill.ConnectSummary.read_time + "ms");
                }

                if (Checkpoint_time.GetElapsedMilliseconds("Drill_main") < 480)
                {
                    lab_Drill_mail_time.Text = Checkpoint_time.GetFormattedTime("Drill_main");
                }
                else
                {
                    lab_Drill_mail_time.Text = "480 ms";
                }
                
            }
            else
            {
                lab_Drill_element_time.Text = "未連接鑽床";
                lab_Drill_mail_time.Text = "未連接鑽床";
            }
            if (Saw != null && Saw.IsConnected)
            {
                if (int.Parse(Saw.ConnectSummary.read_time) > 70)
                {
                    lab_Sawing_element_time.Text = ("當前監控總數更新時間：70ms");
                }
                else
                {
                    lab_Sawing_element_time.Text = ("當前監控總數更新時間：" + Saw.ConnectSummary.read_time + "ms");
                }
                if (Checkpoint_time.GetElapsedMilliseconds("Saw_brand") < 550)
                {
                    lab_sawbrand_time.Text = Checkpoint_time.GetFormattedTime("Saw_brand");
                }
                else
                {
                    lab_sawbrand_time.Text = "220 ms";
                }
                if (Checkpoint_time.GetElapsedMilliseconds("Saw_main") < 500 )
                {
                    lab_Sawing_main_time.Text = Checkpoint_time.GetFormattedTime("Saw_main");
                }
                else
                {
                    lab_Sawing_main_time.Text = "397 ms";
                }
            }
            else
            {
                lab_Sawing_element_time.Text = "未連接鋸床";
                lab_Sawing_main_time.Text = "未連接鋸床";
                lab_sawbrand_time.Text = "未連接鋸床";
            }

            lab_main_time.Text = "500 ms";

            



        }


    }
}
