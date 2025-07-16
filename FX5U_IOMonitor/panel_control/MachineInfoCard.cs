using FX5U_IOMonitor.Models;

namespace FX5U_IOMonitor.panel_control
{

    public class MachineInfoCard : Panel
    {
        private Label lblTitle;
        private Label lblValue;
        private Label lblUnit;

        public MachineInfoCard()
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
                TextAlign = ContentAlignment.MiddleLeft
            };
            Text_design.FitFontToLabel(lblTitle);

            lblValue = new Label
            {
                Text = "0",
                Font = new Font("微軟正黑體", 28, FontStyle.Bold),
                Location = new Point(10, 35),
                Size = new Size(230, 70),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };

            lblUnit = new Label
            {
                Text = "(m/min)",
                Font = new Font("微軟正黑體", 8),
                ForeColor = Color.Gray,
                AutoSize = false,
                Size = new Size(230, 20),
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(10, 120)
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblValue);
            this.Controls.Add(lblUnit);
        }

        public void SetData(string title, string value, string unit)
        {
            lblTitle.Text = title;
            lblValue.Text = value;
            lblUnit.Text = unit;

            Text_design.SafeAdjustFont(lblTitle, title, 9);
            Text_design.SafeAdjustFont(lblTitle, value, 28);
            Text_design.SafeAdjustFont(lblTitle, unit );


        }

    }
}
