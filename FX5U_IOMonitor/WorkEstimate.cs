using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.DatabaseProvider;
using FX5U_IOMonitor.MitsubishiPlc_Monior;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Utilization;
using Org.BouncyCastle.Asn1.Cmp;
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
    public partial class WorkEstimate : Form
    {

        private System.Windows.Forms.Timer syncTimer;
        private bool isSyncing = false;
        private ProjectSummary projectSummary;

        public WorkEstimate()
        {
            InitializeComponent();

            //初始化專案資料
            InitProjectComboAsync();

            combo_project.SelectedIndexChanged += async (s, e) =>
            {
                string selectedProject = combo_project.SelectedItem?.ToString() ?? "";
                LoadProjectDetails(selectedProject);
                
            };
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
            //this.Text = LanguageManager.Translate("UtilizationRate_Main");
            //lab_Total.Text = LanguageManager.Translate("UtilizationRate_StartTime");
            //lab_Completed.Text = LanguageManager.Translate("UtilizationRate_EndTime");


        }
        private void LoadProjectDetails(string projecttitle)
        {
            var list = DBfunction.GetOrderDetailsByProject(projecttitle);

            dataGridView1.DataSource = list;

            // 美化欄位名稱
            dataGridView1.Columns[nameof(OrderDetailDisplay.No)].HeaderText = "No.";
            dataGridView1.Columns[nameof(OrderDetailDisplay.Piecename)].HeaderText = "加工支號";
            dataGridView1.Columns[nameof(OrderDetailDisplay.Piececount)].HeaderText = "支數";
            dataGridView1.Columns[nameof(OrderDetailDisplay.Estimatedtime)].HeaderText = "預計時間";
            dataGridView1.Columns[nameof(OrderDetailDisplay.Actualtime)].HeaderText = "實際時間";
            dataGridView1.Columns[nameof(OrderDetailDisplay.Processingstarttime)].HeaderText = "加工開始時間";
            dataGridView1.Columns[nameof(OrderDetailDisplay.Processingendtime)].HeaderText = "加工截止時間";

            dataGridView1.Columns["No"].Width = 60;
            dataGridView1.Columns["Piecename"].Width = 150;
            dataGridView1.Columns["Piececount"].Width = 60;
            dataGridView1.Columns["Estimatedtime"].Width = 100;
            dataGridView1.Columns["Actualtime"].Width = 100;
            dataGridView1.Columns["Processingstarttime"].Width = 160;
            dataGridView1.Columns["Processingendtime"].Width = 160;
            // ✅ 內容置中對齊
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // ✅ 美化標頭與列距
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 10, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("微軟正黑體", 10, FontStyle.Regular);
            dataGridView1.RowTemplate.Height = 30; // 行高

            // ✅ 邊框與顏色
            dataGridView1.GridColor = Color.Gray;
            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // ✅ 時間格式化顯示
            dataGridView1.Columns["Estimatedtime"].DefaultCellStyle.Format = @"hh\:mm\:ss";
            dataGridView1.Columns["Actualtime"].DefaultCellStyle.Format = @"hh\:mm\:ss";
            dataGridView1.Columns["Processingstarttime"].DefaultCellStyle.Format = "yyyy/MM/dd tt hh:mm";
            dataGridView1.Columns["Processingendtime"].DefaultCellStyle.Format = "yyyy/MM/dd tt hh:mm";

            // 顯示統計資訊
            var summary = ShowProjectSummary(projecttitle, list);

            lab_Total_numb.Text = summary.Total.ToString();
            lab_Completed_numb.Text = summary.Completed.ToString();
            lab_Uncompleted_numb.Text = summary.Uncompleted.ToString();
            lab_TotalEstimated_numb.Text = summary.TotalEstimated.ToString();
            lab_TotalActual_numb.Text = summary.TotalActual.ToString();
            lab_CompletionRate_numb.Text = summary.CompletionRate.ToString()+"%";

        }
        private async void WorkEstimate_Load(object sender, EventArgs e)
        {
            await RunSync();
            projectSummary = DBfunction.GetProjectSummary();
            // 啟動每分鐘同步一次的 Timer
            syncTimer = new System.Windows.Forms.Timer();
            syncTimer.Interval = 60 * 1000;
            syncTimer.Tick += async (s, ev) => await RunSync();
            syncTimer.Start();

        }
        private async Task RunSync()
        {
            if (isSyncing) return;
            isSyncing = true;
            try
            {
                using var local = new ApplicationDB();
                await SyncCloudToLocal(local);
                Console.WriteLine("預估工時資料已同步");
            }
            finally
            {
                isSyncing = false;
            }
        }
        public static async Task SyncCloudToLocal(ApplicationDB _)
        {
            try
            {
                using (var local1 = new ApplicationDB())
                {
                    var Order = await TableSyncAPI.SyncFromCloudToLocalNew<Order>(
                        local1,
                        "Order",
                        keySelector: m => m.orderid,
                        changeDetector: (a, b) => a.updatedat != b.updatedat,
                        ignoreProperties: new[] { "" }
                    );
                }

                using (var local2 = new ApplicationDB())
                {
                    var OrderDetail = await TableSyncAPI.SyncFromCloudToLocalNew<OrderDetails>(
                        local2,
                        "OrderDetails",
                        keySelector: m => m.orderdetailid,
                        changeDetector: (a, b) => a.updatedat != b.updatedat,
                        ignoreProperties: new[] { "" }
                    );
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void InitProjectComboAsync()
        {
            try
            {
                var summary = DBfunction.GetProjectSummary();

                // 清空舊項目
                combo_project.Items.Clear();

                // 加入全部不重複專案
                foreach (var title in summary.ProjectList)
                    combo_project.Items.Add(title);

                // 自動選中最新專案
                if (!string.IsNullOrEmpty(summary.LatestProject))
                {
                    combo_project.SelectedItem = summary.LatestProject;
                    LoadProjectDetails(summary.LatestProject);
                }



                Console.WriteLine($"✅ 專案清單初始化完成，共 {summary.DistinctCount} 筆");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化 combo_project 失敗：{ex.Message}");
            }
        }

        private ProjectSummaryResult ShowProjectSummary(string projecttitle, List<OrderDetailDisplay> list)
        {
            var total = list.Count;
            var completed = list.Count(d => d.Actualtime.HasValue && d.Actualtime.Value.TotalMinutes > 0);
            var uncompleted = total - completed;

            var totalEstimated = list
                .Where(d => d.Estimatedtime.HasValue)
                .Aggregate(TimeSpan.Zero, (sum, t) => sum + t.Estimatedtime.Value);

            var totalActual = list
                .Where(d => d.Actualtime.HasValue)
                .Aggregate(TimeSpan.Zero, (sum, t) => sum + t.Actualtime.Value);

            double completionRate = total == 0 ? 0 : (double)completed / total * 100;

            return new ProjectSummaryResult
            {
                Total = total,
                Completed = completed,
                Uncompleted = uncompleted,
                TotalEstimated = totalEstimated,
                TotalActual = totalActual,
                CompletionRate = completionRate
            };
        }


    }
}
