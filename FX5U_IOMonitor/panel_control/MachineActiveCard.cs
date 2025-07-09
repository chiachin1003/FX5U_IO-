using FX5U_IOMonitor.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static FX5U_IOMonitor.Email.DailyTask_config;

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
            Text_design.FitFontToLabel(lblTitle);

            lblValue = new Label
            {
                Text = "0",
                Font = new Font("微軟正黑體", 22, FontStyle.Bold),
                AutoSize = false,                     
                Size = new Size(230, 40),             
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(0, 30)            
            };
            lblExtra = new Label
            {
                Font = new Font("微軟正黑體", 8),
                ForeColor = Color.Gray,
                Location = new Point(10, 70),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };


            lblPrevMonth = new Label
            {
                Font = new Font("微軟正黑體", 8),
                Location = new Point(10, 110),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft

            };

            lblThisMonth = new Label
            {
                Font = new Font("微軟正黑體", 8),
                Location = new Point(10, 130),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft

            };

          
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblValue);
            this.Controls.Add(lblPrevMonth);
            this.Controls.Add(lblThisMonth);
            this.Controls.Add(lblExtra);
        }

        public void SetData(string Title, string value, int Yesterday, int Today, string recordtime, ScheduleFrequency scheduleFrequency)
        {
            lblTitle.Text = Title;
            string lblPrevMonthtitle = "";
            string lblThisMonthtitle = "";

         
            if (DisplayMode == CardDisplayMode.Time)
            {
                lblValue.Text = value;
                lblValue.Location = new Point(0, 30);
                if (scheduleFrequency == ScheduleFrequency.Weekly || scheduleFrequency == ScheduleFrequency.Minutely)
                {
                    lblPrevMonthtitle = LanguageManager.Translate("DrillInfo_LastWeek_TotalTime");
                    lblThisMonthtitle = LanguageManager.Translate("DrillInfo_ThisWeek_TotalTime");
                }
                else if(scheduleFrequency == ScheduleFrequency.Monthly || scheduleFrequency == ScheduleFrequency.Daily)
                {
                    lblPrevMonthtitle = LanguageManager.Translate("DrillInfo_LastMonth_TotalTime");
                    lblThisMonthtitle = LanguageManager.Translate("DrillInfo_ThisMonth_TotalTime");
                }
                lblPrevMonth.Text = lblPrevMonthtitle + $"{MonitorFunction.ConvertSecondsToDHMS(Yesterday)} ";
                lblPrevMonth.ForeColor = Color.Black;

                string arrowThis = Today > Yesterday ? "↑" : (Today < Yesterday ? "↓" : "—");
                lblThisMonth.Text = lblThisMonthtitle + $"{MonitorFunction.ConvertSecondsToDHMS(Today)} {arrowThis}";
                lblThisMonth.ForeColor = Today > Yesterday ? Color.Red : (Today < Yesterday ? Color.Green : Color.Gray);

                lblExtra.Text = LanguageManager.Translate("DrillInfo_RecordTime") +$"\n{recordtime}";


            }
            else if (DisplayMode == CardDisplayMode.Count)
            {
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
                lblValue.Text = value ;
                lblValue.Location = new Point(8, 30);
                lblPrevMonth.Text = lblPrevMonthtitle + $"{Yesterday} ";
                lblPrevMonth.ForeColor = Color.Black;

                string arrowThis = Today > Yesterday ? "↑" : (Today < Yesterday ? "↓" : "—");
                lblThisMonth.Text = lblThisMonthtitle + $"{Today} {arrowThis}";
                lblThisMonth.ForeColor = Today > Yesterday ? Color.Red : (Today < Yesterday ? Color.Green : Color.Gray);

                lblExtra.Text = LanguageManager.Translate("DrillInfo_RecordTime") + $"\n{recordtime}";


            }


            //    string arrowPrev = Yesterday > 0 ? "↑" : (Yesterday < 0 ? "↓" : "→");
            //    lblPrevMonth.Text = $"上月累計 {Math.Abs(Yesterday)}% {arrowPrev}";
            //    lblPrevMonth.ForeColor = Yesterday > 0 ? Color.Red : (Yesterday < 0 ? Color.Green : Color.Gray);

            //    string arrowThis = Today > 0 ? "↑" : (Today < 0 ? "↓" : "→");
            //    lblThisMonth.Text = $"本月累計 {Math.Abs(Today)}% {arrowThis}";
            //    lblThisMonth.ForeColor = Today > 0 ? Color.Red : (Today < 0 ? Color.Green : Color.Gray);

            //    lblExtra.Text = $"紀錄時間： {newUser}";
        }
       
    }
}
