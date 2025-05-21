using System.Data;
using System.Text;
using static FX5U_IOMonitor.Main;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.Data;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Xml.Linq;
using static FX5U_IOMonitor.Models.panel_design_Setting;
using System.Diagnostics;
using FX5U_IOMonitor.panel_control;


namespace FX5U_IOMonitor
{
    public enum ShowDetailPage
    {
        Default,
        MainInfo,
        LifeSetting,
        History
    }
    public partial class ShowDetail : Form
    {
        private string equipmentTag;
        private string dbtable;
        private CancellationTokenSource? usageMonitorCts;
        private Label? lbl_useCount;
        private Label? lbl_remainCount;
        private readonly Action? onClosedCallback;

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            onClosedCallback?.Invoke(); // ✅ 呼叫外部傳入的委派
            base.OnFormClosed(e);
        }
        public ShowDetail(string machine, string Tag, ShowDetailPage defaultPage = ShowDetailPage.Default, Action? onClosed = null)
        {
            InitializeComponent();
            dbtable = machine;
            equipmentTag = Tag;
            onClosedCallback = onClosed;

            switch (defaultPage)
            {
                case ShowDetailPage.MainInfo:
                    btn_showmain_Click(null, EventArgs.Empty);

                    break;
                case ShowDetailPage.LifeSetting:
                    btn_lifeSetting_Click(null, EventArgs.Empty);
                    break;
                case ShowDetailPage.History:
                    btn_history_Click(null, EventArgs.Empty);
                    break;
                default:


                    string numberReplace = DBfunction.GetHistoryBySourceAndAddress(dbtable, equipmentTag).Count.ToString();
                    string StartTime = DBfunction.GetMountTimeByAddress(dbtable, equipmentTag) + "；第" + numberReplace + "次";

                    var usagePanel = panel_design.CreateShowMainPanel(
                        equipmentTag,
                        DBfunction.Get_Decription_ByAddress(dbtable, equipmentTag),
                        DBfunction.Get_MaxLife_ByAddress(dbtable, equipmentTag),
                        DBfunction.Get_use_ByAddress(dbtable, equipmentTag),
                        DBfunction.Get_Comment_ByAddress(dbtable, equipmentTag),
                        StartTime
                    );
                    // 記得先抓 lbl_useCount 才能背景更新
                    lbl_useCount = usagePanel.Controls.Find("lb_useCount", true).FirstOrDefault() as Label;
                    lbl_remainCount = usagePanel.Controls.Find("lb_remainCount", true).FirstOrDefault() as Label;
                    panel_main.Controls.Add(usagePanel);

                    // 啟動背景監聽（如果先前已存在就先取消）
                    usageMonitorCts?.Cancel();
                    usageMonitorCts = new CancellationTokenSource();
                    StartUseMonitorLoop(usageMonitorCts.Token);


                    break;
            }

        }


        private void btn_history_Click(object sender, EventArgs e)
        {
            panel_main.Controls.Clear();


            List<History> history = DBfunction.GetHistoryBySourceAndAddress(dbtable, equipmentTag);

            var usagePanel = panel_design.CreateUsagePanel(
                equipmentTag,
                DBfunction.Get_Decription_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_MaxLife_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_use_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_Comment_ByAddress(dbtable, equipmentTag), history

            );
            panel_main.Controls.Add(usagePanel);


        }

        private void btn_timereset_Click(object sender, EventArgs e)
        {


            // 當前使用次數
            int equipment_use = DBfunction.Get_use_ByAddress(dbtable, equipmentTag);

            // 移除元件時間更新
            DBfunction.SetCurrentTimeAsUnmountTime(dbtable, equipmentTag);
            // 寫入元件歷史資料
            DBfunction.SetMachineIOToHistory(dbtable, equipmentTag, equipment_use);
            // 更新元件啟用時間資料
            DBfunction.SetCurrentTimeAsMountTime(dbtable, equipmentTag);
            // 顯示當前設備啟用時間
            string Part_InstallationTime = (DBfunction.GetMountTimeByAddress(dbtable, equipmentTag)).ToString();

            string numberReplace = DBfunction.GetHistoryBySourceAndAddress(dbtable, equipmentTag).Count.ToString();

            // 更新元件使用次數 
            DBfunction.Set_use_ByAddress(dbtable, equipmentTag, 0);


            //更新頁面
            panel_main.Controls.Clear();

            string StartTime = DBfunction.GetMountTimeByAddress(dbtable, equipmentTag) + "；第" + numberReplace + "次";
            var usagePanel = panel_design.CreateShowMainPanel(
                equipmentTag,
                DBfunction.Get_Decription_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_MaxLife_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_use_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_Comment_ByAddress(dbtable, equipmentTag), StartTime
                );

            panel_main.Controls.Add(usagePanel);

        }



        private void btn_lifeSetting_Click(object sender, EventArgs e)
        {
            panel_main.Controls.Clear();
           

            Panel Settings = panel_design_Setting.CreateSettingPanel(dbtable ,equipmentTag);
            panel_main.Controls.Add(Settings);
        }

        private void btn_showmain_Click(object sender, EventArgs e)
        {
            panel_main.Controls.Clear();
            usageMonitorCts?.Cancel();
            usageMonitorCts = new CancellationTokenSource();

            string numberReplace = DBfunction.GetHistoryBySourceAndAddress(dbtable, equipmentTag).Count.ToString();
            string StartTime = DBfunction.GetMountTimeByAddress(dbtable, equipmentTag) + "；第" + numberReplace + "次";

            var usagePanel = panel_design.CreateShowMainPanel(
                equipmentTag,
                DBfunction.Get_Decription_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_MaxLife_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_use_ByAddress(dbtable, equipmentTag),
                DBfunction.Get_Comment_ByAddress(dbtable, equipmentTag),
                StartTime
            );
            // 抓取 Label
            lbl_useCount = usagePanel.Controls.Find("lb_useCount", true).FirstOrDefault() as Label;
            lbl_remainCount = usagePanel.Controls.Find("lb_remainCount", true).FirstOrDefault() as Label;

            panel_main.Controls.Add(usagePanel);
            StartUseMonitorLoop(usageMonitorCts.Token);

        }
        private void StartUseMonitorLoop(CancellationToken token)
        {
            _ = Task.Run(async () =>
            {

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        int currentUse = DBfunction.Get_use_ByAddress(dbtable, equipmentTag);
                        int remainCount = (DBfunction.Get_MaxLife_ByAddress(dbtable, equipmentTag) - currentUse);
                        if (lbl_useCount != null && lbl_useCount.IsHandleCreated && !lbl_useCount.IsDisposed)
                        {
                            if (lbl_useCount.InvokeRequired)
                            {
                                lbl_useCount.Invoke(() =>
                                {
                                    lbl_useCount.Text = $"目前已觸發次數   ：{currentUse} 次";
                                    lbl_remainCount.Text = $"剩餘可使用次數   ：{remainCount} 次";

                                });
                            }
                        }
                        else
                        {
                            lbl_useCount.Text = $"目前已觸發次數   ：{currentUse} 次";
                            lbl_remainCount.Text = $"剩餘可使用次數   ：{remainCount} 次";
                        }

                        await Task.Delay(500, token); // 每秒查詢一次
                    }
                    catch (OperationCanceledException)
                    {
                        // 正常結束
                        break;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"使用次數監聽錯誤：{ex.Message}");
                    }
                }
            }, token);
        }


    }
}
