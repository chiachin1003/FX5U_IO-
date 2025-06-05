using FX5U_IOMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FX5U_IOMonitor.panel_control
{
    public partial class MachinePanelGroup : UserControl
    {
        public string MachineType { get; private set; } = "";
        public string ClassTag { get; private set; } = "";
        public float[] ChartIndex { get; private set; }

        public MachinePanelGroup(string machineType, string btn_Text, string classTag, float[] chartIndex)
        {
            InitializeComponent();
            this.MachineType = machineType;
            this.ClassTag = classTag;
            this.ChartIndex = chartIndex;

            CreateComponents(btn_Text);
        }

        private void CreateComponents(string btn_Text)
        {
            // 建立 Button
            Button btn = new Button
            {
                BackColor = SystemColors.ButtonFace,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold),
                Size = new Size(115, 55),
                Location = new Point(0, 135),
                Text = btn_Text,
                Name = $"btn_{ClassTag}"
            };
            btn.Click += Btn_Click;
            this.Controls.Add(btn);

            // 取得圓環圖資料
            int[] number = ChartIndex.Select(v => Convert.ToInt32(v)).ToArray();

            // 建立 Doughnut Chart
            PictureBox chartPanel = panel_design.CreateDoughnutChartPanel(110, ChartIndex,
                new Color[] { Color.LightGreen, Color.Yellow, Color.Red });
            chartPanel.Location = new Point(0, 00);
            chartPanel.Name = $"Color_{ChartIndex}";
            this.Controls.Add(chartPanel);

            // 建立圖例
            TableLayoutPanel legendPanel = panel_design.CreateColorLegendPanel(
                number[0].ToString(), number[1].ToString(), number[2].ToString());
            legendPanel.Location = new Point(0, 110);
            legendPanel.Name = $"ColorLegend_{ChartIndex}";
            this.Controls.Add(legendPanel);

            // 設定 UserControl 尺寸
            //this.Size = new Size(Math.Max(chartPanel.Width, btn.Width), legendPanel.Bottom);
            this.Size = new Size(Math.Max(chartPanel.Width, btn.Width), btn.Bottom + 10);

        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            List<string> search = DBfunction.GetClassTag_address(MachineType, ClassTag);
            var searchControl = new UserSearchControl();
            searchControl.LoadData(search, MachineType);
            Main.Instance.UpdatePanel(searchControl);
        }

        public void UpdateDisplay(float[] newValues)
        {
            if (newValues == null || newValues.Length < 3) return;

            // 更新 chart 資料
            int[] number = newValues.Select(v => Convert.ToInt32(v)).ToArray();

            // 重新建立圖表（或你可以加上 chartPanel.Refresh 支援）
            var newChart = panel_design.CreateDoughnutChartPanel(110, newValues,
                new Color[] { Color.LightGreen, Color.Yellow, Color.Red });

            // 替換舊圖表
            var oldChart = this.Controls.OfType<PictureBox>().FirstOrDefault(c => c.Name.StartsWith("Color_"));
            if (oldChart != null)
            {
                int locX = oldChart.Location.X, locY = oldChart.Location.Y;
                this.Controls.Remove(oldChart);
                newChart.Location = new Point(locX, locY);
                newChart.Name = oldChart.Name;
                this.Controls.Add(newChart);
                newChart.BringToFront();
            }

            // 替換圖例
            var oldLegend = this.Controls.OfType<TableLayoutPanel>().FirstOrDefault(c => c.Name.StartsWith("ColorLegend_"));
            if (oldLegend != null)
            {
                int locX = oldLegend.Location.X, locY = oldLegend.Location.Y;
                this.Controls.Remove(oldLegend);

                var newLegend = panel_design.CreateColorLegendPanel(
                    number[0].ToString(), number[1].ToString(), number[2].ToString());

                newLegend.Location = new Point(locX, locY);
                newLegend.Name = oldLegend.Name;
                this.Controls.Add(newLegend);
                newLegend.BringToFront();
            }
        }

        public void UpdateButtonLabel(string newDisplayText)
        {
            var btn = this.Controls.OfType<Button>().FirstOrDefault();
            if (btn != null)
                btn.Text = newDisplayText;
        }
    }
    
}
