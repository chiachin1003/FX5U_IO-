using CsvHelper;
using FX5U_IOMonitor;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Models;
using FX5U_IOMonitor.panel_control;
using FX5U_IOMonitor.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using static FX5U_IOMonitor.Resources.Element_Settings;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace FX5U_IO元件監控
{

    public partial class History_record : Form
    {
        string? frequency;

        public History_record()
        {

            InitializeComponent();
            string lang = FX5U_IOMonitor.Properties.Settings.Default.LanguageSetting;
            LanguageManager.LoadLanguageFromDatabase(lang);
            LanguageManager.LanguageChanged -= OnLanguageChanged;
            LanguageManager.LanguageChanged += OnLanguageChanged;
            SwitchLanguage();
        }
        private void OnLanguageChanged(string cultureName)
        {
            SwitchLanguage();
        }

        string mode = "";
        private void btn_add_element_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.DataSource = null;

            DateTime startDate = dateTime_start.Value.Date.ToUniversalTime();
            DateTime endDate = dateTime_end.Value.Date.AddDays(1).AddTicks(-1).ToUniversalTime();

            if (choose_event.SelectedIndex == 0)
            {

                var result = DBfunction.Get_Searchalarm_Records(startDate, endDate);
                dataGridView1.DataSource = result;

            }
            else
            {
                
                frequency = ComboBoxHelper.GetSelectedValue<string>(comb_record);
                
                var result = DBfunction.Get_Searchparam_HistoryRecords(startDate, endDate, frequency);
                dataGridView1.DataSource = result;

            }
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dataGridView1.AutoResizeColumns(); // 自動調整欄寬（可選）


        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString(); // 從 1 開始編號

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            Rectangle headerBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                grid.RowHeadersWidth,
                e.RowBounds.Height);

            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void btn_exportCsv_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show(LanguageManager.Translate("History_record_No_Message"),
                    LanguageManager.Translate("Prompt_Title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV 檔 (*.csv)|*.csv";
                sfd.FileName = "匯出資料.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportDataGridViewToCsv(dataGridView1, sfd.FileName);
                        MessageBox.Show(LanguageManager.Translate("History_record_Message_Success"),
                            LanguageManager.Translate("Completed_Title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(LanguageManager.Translate("History_record_Message_Failed") + ex.Message,
                             LanguageManager.Translate("Error_Title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportDataGridViewToCsv(DataGridView dgv, string filePath)
        {
            var sb = new StringBuilder();

            // 標題列
            var headers = dgv.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(c => "\"" + c.HeaderText + "\"")));

            // 每一列資料
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>();
                    sb.AppendLine(string.Join(",", cells.Select(c => "\"" + c.Value?.ToString()?.Replace("\"", "\"\"") + "\"")));
                }
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void choose_event_SelectedIndexChanged(object sender, EventArgs e)
        {
            mode = ComboBoxHelper.GetSelectedValue<string>(choose_event);
            if (choose_event.SelectedIndex == 0)
            {
                lab_metricType.Visible = false;
                comb_record.Visible = false;
            }
            else
            {
                lab_metricType.Visible = true;
                comb_record.Visible = true;
                comb_record.SelectedIndex = 0;
            }
        }

        private void History_record_Load(object sender, EventArgs e)
        {
            choose_event.SelectedIndex = 0;
            lab_metricType.Visible = false;
            comb_record.Visible = false;

            ComboBoxHelper.BindDisplayValueItems(comb_unit, new[]
                {
                    ("公制", "SystemRecord_Metric"),
                    ("英制", "SystemRecord_Imperial")
                });
            ComboBoxHelper.BindDisplayValueItems(comb_record, new[]
               {
                    (LanguageManager.Translate("History_record_Weekly"), "Weekly"),
                    (LanguageManager.Translate("History_record_Monthly"), "Monthly")
                });
            ComboBoxHelper.BindDisplayValueItems(choose_event, new[]
              {
                    (LanguageManager.Translate("History_record_btn_alarm"), 0),
                    (LanguageManager.Translate("History_record_btn_Montion"), 1)
                });
            ComboBoxHelper.BindDisplayValueItems(comb_name, new[]
            {
                ("鋸床馬達電流",1),
                ("鋸床切削速度",2),
                ("鋸床電壓平均值",3),
                ("鋸床電流平均值",4),
                ("鋸床油壓張力",5),
                ("鋸床消耗功率",6),
                ("鋸床累積用電度數",7),
                ("鋸床總運轉時間",8),
                ("鋸床加工剩餘刀數",9),
                ("鋸床鋸切倒數時間",10),
                (LanguageManager.Translate("SawingInfo_SawbandbrandText"),11),
                (LanguageManager.Translate("SawingInfo_SawbladematerialText"),12),
                (LanguageManager.Translate("SawingInfo_SawbladetypeText"),13),
                (LanguageManager.Translate("SawingInfo_SawbladeteethText"),14),
                (LanguageManager.Translate("SawingInfo_SawbandspeedText"),15),
                (LanguageManager.Translate("SawingInfo_SawbandmotorsusetimeText"),16),
                (LanguageManager.Translate("SawingInfo_SawbandpowerText"),17),
                (LanguageManager.Translate("SawingInfo_SawbandcurrentText"),18),
                (LanguageManager.Translate("SawingInfo_SawbandareaText"),19),
                (LanguageManager.Translate("SawingInfo_sawlifeText"),20),
                (LanguageManager.Translate("SawingInfo_SawbandtensionText"),21),
                (LanguageManager.Translate("DrillInfo_DrillservousetimeText"),22),
                (LanguageManager.Translate("DrillInfo_Drillspindle1usetimeText"),23),
                (LanguageManager.Translate("DrillInfo_Drillspindle2usetimeText"),24),
                (LanguageManager.Translate("DrillInfo_Drillspindle3usetimeText"),25),
                (LanguageManager.Translate("DrillInfo_DrillplcusetimeText"),26),
                (LanguageManager.Translate("DrillInfo_DrillinverterText"),27),
                (LanguageManager.Translate("DrillInfo_DrilloutverterText"),28),
                (LanguageManager.Translate("DrillInfo_DrilltotalTimeText"),29),
                (LanguageManager.Translate("DrillInfo_DrilloriginText"),30),
                (LanguageManager.Translate("DrillInfo_DrillloosetoolsText"),31),
                (LanguageManager.Translate("DrillInfo_DrillmeasurementText"),32),
                (LanguageManager.Translate("DrillInfo_DrillclampingText"),33),
                ("鑽床電壓",34),
                ("鑽床電流",35),
                ("鑽床用電度數",36),
                ("鑽床馬力",37),

            });
        }

       
        private void SwitchLanguage()
        {
            this.Text = LanguageManager.Translate("History_record_Title");
            lab_startTime.Text = LanguageManager.Translate("History_record_lab_startTime");
            lab_endTime.Text = LanguageManager.Translate("History_record_lab_endTime");
            lab_historyType.Text = LanguageManager.Translate("History_record_lab_historyType");
            lab_metricType.Text = LanguageManager.Translate("History_record_lab_metricType");
            lab_unit.Text = LanguageManager.Translate("History_record_lab_unit");
            btn_search.Text = LanguageManager.Translate("History_record_btn_search");
            btn_exportCsv.Text = LanguageManager.Translate("History_record_btn_exportCsv");
          

        }

    }
}



