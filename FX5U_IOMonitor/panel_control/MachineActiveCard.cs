﻿using FX5U_IOMonitor.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.panel_control
{
    public enum CardDisplayMode
    {
        Time,   // 顯示時間格式 hh:mm:ss
        Count   // 顯示次數，例如 105 次
    }
    public class MachineActiveCard : Panel
    {
        private Label lblTitle;
        private Label lblValue;
        private Label lblPrevMonth;
        private Label lblThisMonth;
        private Label lblExtra;

        public CardDisplayMode DisplayMode { get; set; } = CardDisplayMode.Time;

        public MachineActiveCard()
        {
            this.Size = new Size(250, 150);
            this.Margin = new Padding(5);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;

            lblTitle = new Label
            {
                Text = "監控時間 (時間)",
                Font = new Font("微軟正黑體", 9, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(230, 20),  // 限定寬度與高度
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            AutoFitLabelFont(lblTitle);

            lblValue = new Label
            {
                Text = "0",
                Font = new Font("微軟正黑體", 22, FontStyle.Bold),
                AutoSize = false,                     
                Size = new Size(230, 40),             
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(0, 30)            
            };

            lblPrevMonth = new Label
            {
                Font = new Font("微軟正黑體", 8),
                Location = new Point(10, 70),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft

            };

            lblThisMonth = new Label
            {
                Font = new Font("微軟正黑體", 8),
                Location = new Point(10, 90),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft

            };

            lblExtra = new Label
            {
                Font = new Font("微軟正黑體", 8),
                ForeColor = Color.Gray,
                Location = new Point(10, 110),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblValue);
            this.Controls.Add(lblPrevMonth);
            this.Controls.Add(lblThisMonth);
            this.Controls.Add(lblExtra);
        }

        public void SetData(string Title, string value, int Yesterday, int Today, string recordtime)
        {
            lblTitle.Text = Title;
            if (DisplayMode == CardDisplayMode.Time)
            {
                lblValue.Text = value;
                lblValue.Location = new Point(0, 30);     

            }
            else if (DisplayMode == CardDisplayMode.Count)
            {
                lblValue.Text = value ;
                lblValue.Location = new Point(8, 30); 

            }

            lblPrevMonth.Text = $"上月累計 {MonitorFunction.ConvertSecondsToDHMS(Yesterday)} ";
            lblPrevMonth.ForeColor =  Color.Black;

            string arrowThis = Today > Yesterday ? "↑" : (Today < Yesterday ? "↓" : "—");
            lblThisMonth.Text = $"本月累計 {MonitorFunction.ConvertSecondsToDHMS(Today)} {arrowThis}";
            lblThisMonth.ForeColor = Today > Yesterday ? Color.Red : (Today < Yesterday ? Color.Green : Color.Gray);

            lblExtra.Text = $"紀錄時間： {recordtime}";

            //    string arrowPrev = Yesterday > 0 ? "↑" : (Yesterday < 0 ? "↓" : "→");
            //    lblPrevMonth.Text = $"上月累計 {Math.Abs(Yesterday)}% {arrowPrev}";
            //    lblPrevMonth.ForeColor = Yesterday > 0 ? Color.Red : (Yesterday < 0 ? Color.Green : Color.Gray);

            //    string arrowThis = Today > 0 ? "↑" : (Today < 0 ? "↓" : "→");
            //    lblThisMonth.Text = $"本月累計 {Math.Abs(Today)}% {arrowThis}";
            //    lblThisMonth.ForeColor = Today > 0 ? Color.Red : (Today < 0 ? Color.Green : Color.Gray);

            //    lblExtra.Text = $"紀錄時間： {newUser}";
        }
        private void AutoFitLabelFont(Label label)
        {
            if (label == null || string.IsNullOrEmpty(label.Text)) return;

            float fontSize = label.Font.Size;
            int maxWidth = label.Width;

            using (Graphics g = label.CreateGraphics())
            {
                while (fontSize > 6f) // 最小字體限制
                {
                    Font testFont = new Font(label.Font.FontFamily, fontSize, label.Font.Style);
                    Size textSize = TextRenderer.MeasureText(label.Text, testFont);

                    if (textSize.Width <= maxWidth)
                    {
                        label.Font = testFont;
                        break;
                    }

                    fontSize -= 0.5f;
                }
            }
        }
    }
}
