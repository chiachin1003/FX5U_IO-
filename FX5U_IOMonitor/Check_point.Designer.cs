namespace FX5U_IOMonitor
{
    partial class Check_point
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            TableLayoutPanel tlp = (TableLayoutPanel)sender;
            using (Pen p = new Pen(Color.Black, 1)) // 格子內線
            using (Pen thickPen = new Pen(Color.Black, 3)) // 外框線加粗
            {
                // 畫橫線
                for (int i = 1; i < tlp.RowCount; i++)
                {
                    int y = tlp.GetRowHeights().Take(i).Sum();
                    e.Graphics.DrawLine(p, 0, y, tlp.Width, y);
                }

                // 畫直線
                for (int i = 1; i < tlp.ColumnCount; i++)
                {
                    int x = tlp.GetColumnWidths().Take(i).Sum();
                    e.Graphics.DrawLine(p, x, 0, x, tlp.Height);
                }

                // 畫加粗的外框線
                e.Graphics.DrawRectangle(thickPen, 0, 0, tlp.Width - 1, tlp.Height - 1);
            }
        }
        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            lab_sawbrand_time = new Label();
            lab_sawbrand = new Label();
            lab_Sawing_main_time = new Label();
            lab_Sawing_main = new Label();
            lab_Sawing_element_time = new Label();
            lab_Sawing_element = new Label();
            lab_Drill_mail_time = new Label();
            lab_Drill_mail = new Label();
            lab_Drill_element_time = new Label();
            lab_Drill_element = new Label();
            lab_main_time = new Label();
            lab_main = new Label();
            lab_title = new Label();
            lab_titleText = new Label();
            lab_time = new Label();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Dock = DockStyle.Fill;
            panel1.Font = new Font("微軟正黑體", 9.75F);
            panel1.ForeColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(504, 415);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.None;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lab_sawbrand_time, 1, 6);
            tableLayoutPanel1.Controls.Add(lab_sawbrand, 0, 6);
            tableLayoutPanel1.Controls.Add(lab_Sawing_main_time, 1, 5);
            tableLayoutPanel1.Controls.Add(lab_Sawing_main, 0, 5);
            tableLayoutPanel1.Controls.Add(lab_Sawing_element_time, 1, 4);
            tableLayoutPanel1.Controls.Add(lab_Sawing_element, 0, 4);
            tableLayoutPanel1.Controls.Add(lab_Drill_mail_time, 1, 3);
            tableLayoutPanel1.Controls.Add(lab_Drill_mail, 0, 3);
            tableLayoutPanel1.Controls.Add(lab_Drill_element_time, 1, 2);
            tableLayoutPanel1.Controls.Add(lab_Drill_element, 0, 2);
            tableLayoutPanel1.Controls.Add(lab_main_time, 1, 1);
            tableLayoutPanel1.Controls.Add(lab_main, 0, 1);
            tableLayoutPanel1.Controls.Add(lab_title, 1, 0);
            tableLayoutPanel1.Controls.Add(lab_titleText, 0, 0);
            tableLayoutPanel1.Font = new Font("微軟正黑體", 9.75F);
            tableLayoutPanel1.ForeColor = SystemColors.ActiveCaptionText;
            tableLayoutPanel1.Location = new Point(42, 25);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(418, 359);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // lab_sawbrand_time
            // 
            lab_sawbrand_time.BackColor = Color.Transparent;
            lab_sawbrand_time.Dock = DockStyle.Fill;
            lab_sawbrand_time.Font = new Font("微軟正黑體", 9.75F);
            lab_sawbrand_time.ForeColor = SystemColors.ActiveCaptionText;
            lab_sawbrand_time.Location = new Point(212, 269);
            lab_sawbrand_time.Name = "lab_sawbrand_time";
            lab_sawbrand_time.Size = new Size(203, 44);
            lab_sawbrand_time.TabIndex = 13;
            lab_sawbrand_time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_sawbrand
            // 
            lab_sawbrand.BackColor = Color.Transparent;
            lab_sawbrand.Dock = DockStyle.Fill;
            lab_sawbrand.Font = new Font("微軟正黑體", 9.75F);
            lab_sawbrand.ForeColor = SystemColors.ActiveCaptionText;
            lab_sawbrand.Location = new Point(3, 269);
            lab_sawbrand.Name = "lab_sawbrand";
            lab_sawbrand.Size = new Size(203, 44);
            lab_sawbrand.TabIndex = 12;
            lab_sawbrand.Text = "鋸帶監控元件總時間";
            lab_sawbrand.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Sawing_main_time
            // 
            lab_Sawing_main_time.BackColor = Color.Transparent;
            lab_Sawing_main_time.Dock = DockStyle.Fill;
            lab_Sawing_main_time.Font = new Font("微軟正黑體", 9.75F);
            lab_Sawing_main_time.ForeColor = SystemColors.ActiveCaptionText;
            lab_Sawing_main_time.Location = new Point(212, 225);
            lab_Sawing_main_time.Name = "lab_Sawing_main_time";
            lab_Sawing_main_time.Size = new Size(203, 44);
            lab_Sawing_main_time.TabIndex = 11;
            lab_Sawing_main_time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_Sawing_main
            // 
            lab_Sawing_main.BackColor = Color.Transparent;
            lab_Sawing_main.Dock = DockStyle.Fill;
            lab_Sawing_main.Font = new Font("微軟正黑體", 9.75F);
            lab_Sawing_main.ForeColor = SystemColors.ActiveCaptionText;
            lab_Sawing_main.Location = new Point(3, 225);
            lab_Sawing_main.Name = "lab_Sawing_main";
            lab_Sawing_main.Size = new Size(203, 44);
            lab_Sawing_main.TabIndex = 10;
            lab_Sawing_main.Text = "鋸床資訊監控總時間";
            lab_Sawing_main.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Sawing_element_time
            // 
            lab_Sawing_element_time.BackColor = Color.Transparent;
            lab_Sawing_element_time.Dock = DockStyle.Fill;
            lab_Sawing_element_time.Font = new Font("微軟正黑體", 9.75F);
            lab_Sawing_element_time.ForeColor = SystemColors.ActiveCaptionText;
            lab_Sawing_element_time.Location = new Point(212, 181);
            lab_Sawing_element_time.Name = "lab_Sawing_element_time";
            lab_Sawing_element_time.Size = new Size(203, 44);
            lab_Sawing_element_time.TabIndex = 9;
            lab_Sawing_element_time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_Sawing_element
            // 
            lab_Sawing_element.BackColor = Color.Transparent;
            lab_Sawing_element.Dock = DockStyle.Fill;
            lab_Sawing_element.Font = new Font("微軟正黑體", 9.75F);
            lab_Sawing_element.ForeColor = SystemColors.ActiveCaptionText;
            lab_Sawing_element.Location = new Point(3, 181);
            lab_Sawing_element.Name = "lab_Sawing_element";
            lab_Sawing_element.Size = new Size(203, 44);
            lab_Sawing_element.TabIndex = 8;
            lab_Sawing_element.Text = "鋸床監控元件總時間";
            lab_Sawing_element.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_mail_time
            // 
            lab_Drill_mail_time.BackColor = Color.Transparent;
            lab_Drill_mail_time.Dock = DockStyle.Fill;
            lab_Drill_mail_time.Font = new Font("微軟正黑體", 9.75F);
            lab_Drill_mail_time.ForeColor = SystemColors.ActiveCaptionText;
            lab_Drill_mail_time.Location = new Point(212, 137);
            lab_Drill_mail_time.Name = "lab_Drill_mail_time";
            lab_Drill_mail_time.Size = new Size(203, 44);
            lab_Drill_mail_time.TabIndex = 7;
            lab_Drill_mail_time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_Drill_mail
            // 
            lab_Drill_mail.BackColor = Color.Transparent;
            lab_Drill_mail.Dock = DockStyle.Fill;
            lab_Drill_mail.Font = new Font("微軟正黑體", 9.75F);
            lab_Drill_mail.ForeColor = SystemColors.ActiveCaptionText;
            lab_Drill_mail.Location = new Point(3, 137);
            lab_Drill_mail.Name = "lab_Drill_mail";
            lab_Drill_mail.Size = new Size(203, 44);
            lab_Drill_mail.TabIndex = 6;
            lab_Drill_mail.Text = "鑽床資訊監控時間";
            lab_Drill_mail.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_element_time
            // 
            lab_Drill_element_time.BackColor = Color.Transparent;
            lab_Drill_element_time.Dock = DockStyle.Fill;
            lab_Drill_element_time.Font = new Font("微軟正黑體", 9.75F);
            lab_Drill_element_time.ForeColor = SystemColors.ActiveCaptionText;
            lab_Drill_element_time.Location = new Point(212, 93);
            lab_Drill_element_time.Name = "lab_Drill_element_time";
            lab_Drill_element_time.Size = new Size(203, 44);
            lab_Drill_element_time.TabIndex = 5;
            lab_Drill_element_time.Text = " ";
            lab_Drill_element_time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_Drill_element
            // 
            lab_Drill_element.BackColor = Color.Transparent;
            lab_Drill_element.Dock = DockStyle.Fill;
            lab_Drill_element.Font = new Font("微軟正黑體", 9.75F);
            lab_Drill_element.ForeColor = SystemColors.ActiveCaptionText;
            lab_Drill_element.Location = new Point(3, 93);
            lab_Drill_element.Name = "lab_Drill_element";
            lab_Drill_element.Size = new Size(203, 44);
            lab_Drill_element.TabIndex = 4;
            lab_Drill_element.Text = "鑽床監控元件總時間";
            lab_Drill_element.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_main_time
            // 
            lab_main_time.BackColor = Color.Transparent;
            lab_main_time.Dock = DockStyle.Fill;
            lab_main_time.Font = new Font("微軟正黑體", 9.75F);
            lab_main_time.ForeColor = SystemColors.ActiveCaptionText;
            lab_main_time.Location = new Point(212, 49);
            lab_main_time.Name = "lab_main_time";
            lab_main_time.Size = new Size(203, 44);
            lab_main_time.TabIndex = 3;
            lab_main_time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_main
            // 
            lab_main.AutoSize = true;
            lab_main.BackColor = Color.Transparent;
            lab_main.Dock = DockStyle.Fill;
            lab_main.Font = new Font("微軟正黑體", 9.75F);
            lab_main.ForeColor = SystemColors.ActiveCaptionText;
            lab_main.Location = new Point(3, 49);
            lab_main.Name = "lab_main";
            lab_main.Size = new Size(203, 44);
            lab_main.TabIndex = 2;
            lab_main.Text = "主頁面監控時間";
            lab_main.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_title
            // 
            lab_title.BackColor = Color.Transparent;
            lab_title.Dock = DockStyle.Fill;
            lab_title.Font = new Font("微軟正黑體", 12F, FontStyle.Bold);
            lab_title.ForeColor = SystemColors.ActiveCaptionText;
            lab_title.Location = new Point(212, 0);
            lab_title.Name = "lab_title";
            lab_title.Size = new Size(203, 49);
            lab_title.TabIndex = 1;
            lab_title.Text = "偵測時間";
            lab_title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_titleText
            // 
            lab_titleText.BackColor = Color.Transparent;
            lab_titleText.Dock = DockStyle.Fill;
            lab_titleText.Font = new Font("微軟正黑體", 12F, FontStyle.Bold);
            lab_titleText.ForeColor = SystemColors.ActiveCaptionText;
            lab_titleText.Location = new Point(3, 0);
            lab_titleText.Name = "lab_titleText";
            lab_titleText.Size = new Size(203, 49);
            lab_titleText.TabIndex = 0;
            lab_titleText.Text = "監控總表";
            lab_titleText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_time
            // 
            lab_time.AutoSize = true;
            lab_time.Font = new Font("微軟正黑體", 9.75F);
            lab_time.ForeColor = SystemColors.ActiveCaptionText;
            lab_time.Location = new Point(36, 9);
            lab_time.Name = "lab_time";
            lab_time.Size = new Size(0, 17);
            lab_time.TabIndex = 1;
            // 
            // Check_point
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(504, 415);
            Controls.Add(lab_time);
            Controls.Add(panel1);
            Margin = new Padding(4);
            Name = "Check_point";
            StartPosition = FormStartPosition.CenterParent;
            Text = "查核點指標";
            Load += Main_Load;
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lab_main;
        private Label lab_title;
        private Label lab_titleText;
        private Label lab_Drill_mail_time;
        private Label lab_Drill_mail;
        private Label lab_Drill_element_time;
        private Label lab_Drill_element;
        private Label lab_main_time;
        private Label lab_sawbrand_time;
        private Label lab_sawbrand;
        private Label lab_Sawing_main_time;
        private Label lab_Sawing_main;
        private Label lab_Sawing_element_time;
        private Label lab_Sawing_element;
        private Label lab_time;
    }
}

