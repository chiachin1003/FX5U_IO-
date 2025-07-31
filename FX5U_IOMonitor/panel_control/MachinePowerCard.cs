using FX5U_IOMonitor.Models;
using static FX5U_IOMonitor.Scheduling.DailyTask_config;

namespace FX5U_IOMonitor.panel_control
{

    public class MachinePowerCard : Panel
    {
        private Label lblTitle;
        private Label lblValue;
        private Label lblUnit;
        private Label lblRecordTime;
        private Label lblPrevMonth;
        private Label lblThisMonth;

        public MachinePowerCard()
        {
            this.Size = new Size(250, 150);
            this.Margin = new Padding(5);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;

            lblTitle = new Label
            {
                Text = "參數名稱",
                Font = new Font("微軟正黑體", 9, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(230, 20),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            Text_design.FitFontToLabel(lblTitle);

            lblValue = new Label
            {
                Text = "0",
                Font = new Font("微軟正黑體", 22, FontStyle.Bold),
                Location = new Point(10, 30),
                Size = new Size(170, 40),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            };

            lblRecordTime = new Label
            {
                Text = "紀錄時間",
                Font = new Font("微軟正黑體", 8),
                ForeColor = Color.Gray,
                Location = new Point(10, 70),
                Size = new Size(230, 20),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblPrevMonth = new Label
            {
                Font = new Font("微軟正黑體", 8),
                Location = new Point(10, 110),
                Size = new Size(230, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblThisMonth = new Label
            {
                Font = new Font("微軟正黑體", 8),
                Location = new Point(10, 130),
                Size = new Size(230, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblUnit = new Label
            {
                Text = "(A)",
                Font = new Font("微軟正黑體", 8),
                ForeColor = Color.Gray,
                Location = new Point(170, 60),
                Size = new Size(60, 15),
                TextAlign = ContentAlignment.MiddleRight
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblValue);
            this.Controls.Add(lblUnit);
            this.Controls.Add(lblRecordTime);
            this.Controls.Add(lblPrevMonth);
            this.Controls.Add(lblThisMonth);
        }

        public void SetData(string title, string value, string unit, string recordTime, int lastMonth, int thisMonth, ScheduleFrequency scheduleFrequency)
        {
            lblTitle.Text = title;
            lblValue.Text = value;
            lblUnit.Text = unit;
            string lblPrevMonthtitle = "";
            string lblThisMonthtitle = "";

            if (scheduleFrequency == ScheduleFrequency.Weekly || scheduleFrequency == ScheduleFrequency.Minutely)
            {
                lblPrevMonthtitle = LanguageManager.Translate("DrillInfo_LastWeek_TotalCount");
                lblThisMonthtitle = LanguageManager.Translate("DrillInfo_ThisWeek_TotalCount");
            }
            else if (scheduleFrequency == ScheduleFrequency.Monthly || scheduleFrequency == ScheduleFrequency.Daily)
            {
                lblPrevMonthtitle = LanguageManager.Translate("DrillInfo_LastMonth_TotalCount");
                lblThisMonthtitle = LanguageManager.Translate("DrillInfo_ThisMonth_TotalCount");
            }


            lblPrevMonth.Text = lblPrevMonthtitle + $"{lastMonth} ";
            lblPrevMonth.ForeColor = Color.Black;

            string arrowThis = thisMonth > lastMonth ? "↑" : (thisMonth < lastMonth ? "↓" : "—");
            lblThisMonth.Text = lblThisMonthtitle + $"{thisMonth} {arrowThis}";
            lblThisMonth.ForeColor = thisMonth > lastMonth ? Color.Red : (thisMonth < lastMonth ? Color.Green : Color.Gray);

            lblRecordTime.Text = LanguageManager.Translate("DrillInfo_RecordTime") + $"\n{recordTime}";

        }

    }
}
