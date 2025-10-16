namespace FX5U_IOMonitor
{
    partial class UtilizationRate
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Panel mainPanel;
        private Panel topPanel;
        private Panel chartPanel;

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
            dateTime_start = new DateTimePicker();
            lab_UtilizationRate = new Label();
            label1 = new Label();
            comb_class = new ComboBox();
            label2 = new Label();
            btn_today = new Button();
            btn_yesterday = new Button();
            btn_lastweek = new Button();
            btn_thisweek = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            lab_end = new Label();
            lab_start = new Label();
            btn_calculate4 = new Button();
            dateTimePicker_start4 = new DateTimePicker();
            dateTimePicker_end4 = new DateTimePicker();
            btn_calculate3 = new Button();
            dateTimePicker_start3 = new DateTimePicker();
            dateTimePicker_end3 = new DateTimePicker();
            btn_calculate2 = new Button();
            dateTimePicker_start2 = new DateTimePicker();
            dateTimePicker_end2 = new DateTimePicker();
            btn_calculate1 = new Button();
            dateTimePicker_start1 = new DateTimePicker();
            dateTimePicker_end1 = new DateTimePicker();
            btn_setting = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // dateTime_start
            // 
            dateTime_start.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            dateTime_start.Location = new Point(376, 312);
            dateTime_start.Name = "dateTime_start";
            dateTime_start.Size = new Size(179, 27);
            dateTime_start.TabIndex = 11;
            // 
            // lab_UtilizationRate
            // 
            lab_UtilizationRate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_UtilizationRate.BackColor = SystemColors.ButtonHighlight;
            lab_UtilizationRate.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_UtilizationRate.Location = new Point(34, 22);
            lab_UtilizationRate.Name = "lab_UtilizationRate";
            lab_UtilizationRate.Size = new Size(120, 26);
            lab_UtilizationRate.TabIndex = 34;
            lab_UtilizationRate.Text = "稼動率";
            lab_UtilizationRate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonHighlight;
            label1.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            label1.Location = new Point(376, 258);
            label1.Name = "label1";
            label1.Size = new Size(117, 26);
            label1.TabIndex = 59;
            label1.Text = "選擇日期：";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comb_class
            // 
            comb_class.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            comb_class.FormattingEnabled = true;
            comb_class.Location = new Point(376, 401);
            comb_class.Name = "comb_class";
            comb_class.Size = new Size(179, 28);
            comb_class.TabIndex = 60;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            label2.Location = new Point(376, 356);
            label2.Name = "label2";
            label2.Size = new Size(75, 26);
            label2.TabIndex = 61;
            label2.Text = "班別：";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_today
            // 
            btn_today.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_today.Location = new Point(580, 306);
            btn_today.Name = "btn_today";
            btn_today.Size = new Size(120, 33);
            btn_today.TabIndex = 62;
            btn_today.Text = "今天";
            btn_today.UseVisualStyleBackColor = true;
            btn_today.Click += btn_today_Click;
            // 
            // btn_yesterday
            // 
            btn_yesterday.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_yesterday.Location = new Point(580, 253);
            btn_yesterday.Name = "btn_yesterday";
            btn_yesterday.Size = new Size(120, 33);
            btn_yesterday.TabIndex = 66;
            btn_yesterday.Text = "昨天";
            btn_yesterday.UseVisualStyleBackColor = true;
            btn_yesterday.Click += btn_yesterday_Click;
            // 
            // btn_lastweek
            // 
            btn_lastweek.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_lastweek.Location = new Point(580, 396);
            btn_lastweek.Name = "btn_lastweek";
            btn_lastweek.Size = new Size(120, 33);
            btn_lastweek.TabIndex = 68;
            btn_lastweek.Text = "上週";
            btn_lastweek.UseVisualStyleBackColor = true;
            btn_lastweek.Click += btn_lastweek_Click;
            // 
            // btn_thisweek
            // 
            btn_thisweek.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_thisweek.Location = new Point(580, 349);
            btn_thisweek.Name = "btn_thisweek";
            btn_thisweek.Size = new Size(120, 33);
            btn_thisweek.TabIndex = 67;
            btn_thisweek.Text = "本週";
            btn_thisweek.UseVisualStyleBackColor = true;
            btn_thisweek.Click += btn_thisweek_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.3449478F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.2369347F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.Controls.Add(lab_end, 2, 0);
            tableLayoutPanel1.Controls.Add(lab_start, 1, 0);
            tableLayoutPanel1.Controls.Add(btn_calculate4, 0, 4);
            tableLayoutPanel1.Controls.Add(dateTimePicker_start4, 1, 4);
            tableLayoutPanel1.Controls.Add(dateTimePicker_end4, 2, 4);
            tableLayoutPanel1.Controls.Add(btn_calculate3, 0, 3);
            tableLayoutPanel1.Controls.Add(dateTimePicker_start3, 1, 3);
            tableLayoutPanel1.Controls.Add(dateTimePicker_end3, 2, 3);
            tableLayoutPanel1.Controls.Add(btn_calculate2, 0, 2);
            tableLayoutPanel1.Controls.Add(dateTimePicker_start2, 1, 2);
            tableLayoutPanel1.Controls.Add(dateTimePicker_end2, 2, 2);
            tableLayoutPanel1.Controls.Add(btn_calculate1, 0, 1);
            tableLayoutPanel1.Controls.Add(dateTimePicker_start1, 1, 1);
            tableLayoutPanel1.Controls.Add(dateTimePicker_end1, 2, 1);
            tableLayoutPanel1.Controls.Add(btn_setting, 0, 0);
            tableLayoutPanel1.Location = new Point(45, 250);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(287, 189);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lab_end
            // 
            lab_end.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_end.AutoSize = true;
            lab_end.BackColor = SystemColors.ButtonHighlight;
            lab_end.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_end.Location = new Point(174, 0);
            lab_end.Name = "lab_end";
            lab_end.Size = new Size(110, 37);
            lab_end.TabIndex = 58;
            lab_end.Text = "結束時間";
            lab_end.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_start
            // 
            lab_start.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_start.AutoSize = true;
            lab_start.BackColor = SystemColors.ButtonHighlight;
            lab_start.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_start.Location = new Point(70, 0);
            lab_start.Name = "lab_start";
            lab_start.Size = new Size(98, 37);
            lab_start.TabIndex = 57;
            lab_start.Text = "開始時間";
            lab_start.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_calculate4
            // 
            btn_calculate4.Dock = DockStyle.Fill;
            btn_calculate4.FlatStyle = FlatStyle.Flat;
            btn_calculate4.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_calculate4.Location = new Point(3, 151);
            btn_calculate4.Name = "btn_calculate4";
            btn_calculate4.Size = new Size(61, 35);
            btn_calculate4.TabIndex = 52;
            btn_calculate4.Text = "4";
            btn_calculate4.UseVisualStyleBackColor = true;
            btn_calculate4.Click += btn_calculate4_Click;
            // 
            // dateTimePicker_start4
            // 
            dateTimePicker_start4.CustomFormat = "HH:mm";
            dateTimePicker_start4.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            dateTimePicker_start4.Format = DateTimePickerFormat.Custom;
            dateTimePicker_start4.Location = new Point(70, 151);
            dateTimePicker_start4.Name = "dateTimePicker_start4";
            dateTimePicker_start4.ShowUpDown = true;
            dateTimePicker_start4.Size = new Size(89, 34);
            dateTimePicker_start4.TabIndex = 53;
            // 
            // dateTimePicker_end4
            // 
            dateTimePicker_end4.CustomFormat = "HH:mm";
            dateTimePicker_end4.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            dateTimePicker_end4.Format = DateTimePickerFormat.Custom;
            dateTimePicker_end4.Location = new Point(174, 151);
            dateTimePicker_end4.Name = "dateTimePicker_end4";
            dateTimePicker_end4.ShowUpDown = true;
            dateTimePicker_end4.Size = new Size(95, 34);
            dateTimePicker_end4.TabIndex = 54;
            // 
            // btn_calculate3
            // 
            btn_calculate3.Dock = DockStyle.Fill;
            btn_calculate3.FlatStyle = FlatStyle.Flat;
            btn_calculate3.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_calculate3.Location = new Point(3, 114);
            btn_calculate3.Name = "btn_calculate3";
            btn_calculate3.Size = new Size(61, 31);
            btn_calculate3.TabIndex = 49;
            btn_calculate3.Text = "3";
            btn_calculate3.UseVisualStyleBackColor = true;
            btn_calculate3.Click += btn_calculate3_Click;
            // 
            // dateTimePicker_start3
            // 
            dateTimePicker_start3.CustomFormat = "HH:mm";
            dateTimePicker_start3.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            dateTimePicker_start3.Format = DateTimePickerFormat.Custom;
            dateTimePicker_start3.Location = new Point(70, 114);
            dateTimePicker_start3.Name = "dateTimePicker_start3";
            dateTimePicker_start3.ShowUpDown = true;
            dateTimePicker_start3.Size = new Size(89, 34);
            dateTimePicker_start3.TabIndex = 50;
            // 
            // dateTimePicker_end3
            // 
            dateTimePicker_end3.CustomFormat = "HH:mm";
            dateTimePicker_end3.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            dateTimePicker_end3.Format = DateTimePickerFormat.Custom;
            dateTimePicker_end3.Location = new Point(174, 114);
            dateTimePicker_end3.Name = "dateTimePicker_end3";
            dateTimePicker_end3.ShowUpDown = true;
            dateTimePicker_end3.Size = new Size(95, 34);
            dateTimePicker_end3.TabIndex = 51;
            // 
            // btn_calculate2
            // 
            btn_calculate2.Dock = DockStyle.Fill;
            btn_calculate2.FlatStyle = FlatStyle.Flat;
            btn_calculate2.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_calculate2.Location = new Point(3, 77);
            btn_calculate2.Name = "btn_calculate2";
            btn_calculate2.Size = new Size(61, 31);
            btn_calculate2.TabIndex = 46;
            btn_calculate2.Text = "2";
            btn_calculate2.UseVisualStyleBackColor = true;
            btn_calculate2.Click += btn_calculate2_Click;
            // 
            // dateTimePicker_start2
            // 
            dateTimePicker_start2.CustomFormat = "HH:mm";
            dateTimePicker_start2.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            dateTimePicker_start2.Format = DateTimePickerFormat.Custom;
            dateTimePicker_start2.Location = new Point(70, 77);
            dateTimePicker_start2.Name = "dateTimePicker_start2";
            dateTimePicker_start2.ShowUpDown = true;
            dateTimePicker_start2.Size = new Size(89, 34);
            dateTimePicker_start2.TabIndex = 47;
            // 
            // dateTimePicker_end2
            // 
            dateTimePicker_end2.CustomFormat = "HH:mm";
            dateTimePicker_end2.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            dateTimePicker_end2.Format = DateTimePickerFormat.Custom;
            dateTimePicker_end2.Location = new Point(174, 77);
            dateTimePicker_end2.Name = "dateTimePicker_end2";
            dateTimePicker_end2.ShowUpDown = true;
            dateTimePicker_end2.Size = new Size(95, 34);
            dateTimePicker_end2.TabIndex = 48;
            // 
            // btn_calculate1
            // 
            btn_calculate1.Dock = DockStyle.Fill;
            btn_calculate1.FlatStyle = FlatStyle.Flat;
            btn_calculate1.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_calculate1.Location = new Point(3, 40);
            btn_calculate1.Name = "btn_calculate1";
            btn_calculate1.Size = new Size(61, 31);
            btn_calculate1.TabIndex = 40;
            btn_calculate1.Text = "1";
            btn_calculate1.UseVisualStyleBackColor = true;
            btn_calculate1.Click += btn_calculate1_Click;
            // 
            // dateTimePicker_start1
            // 
            dateTimePicker_start1.CustomFormat = "HH:mm";
            dateTimePicker_start1.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            dateTimePicker_start1.Format = DateTimePickerFormat.Custom;
            dateTimePicker_start1.Location = new Point(70, 40);
            dateTimePicker_start1.Name = "dateTimePicker_start1";
            dateTimePicker_start1.ShowUpDown = true;
            dateTimePicker_start1.Size = new Size(89, 34);
            dateTimePicker_start1.TabIndex = 41;
            // 
            // dateTimePicker_end1
            // 
            dateTimePicker_end1.CustomFormat = "HH:mm";
            dateTimePicker_end1.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            dateTimePicker_end1.Format = DateTimePickerFormat.Custom;
            dateTimePicker_end1.Location = new Point(174, 40);
            dateTimePicker_end1.Name = "dateTimePicker_end1";
            dateTimePicker_end1.ShowUpDown = true;
            dateTimePicker_end1.Size = new Size(95, 34);
            dateTimePicker_end1.TabIndex = 42;
            // 
            // btn_setting
            // 
            btn_setting.Dock = DockStyle.Fill;
            btn_setting.FlatStyle = FlatStyle.Flat;
            btn_setting.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_setting.Location = new Point(3, 3);
            btn_setting.Name = "btn_setting";
            btn_setting.Size = new Size(61, 31);
            btn_setting.TabIndex = 59;
            btn_setting.Text = "設定";
            btn_setting.UseVisualStyleBackColor = true;
            btn_setting.Click += btn_setting_Click;
            // 
            // UtilizationRate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(800, 477);
            Controls.Add(btn_lastweek);
            Controls.Add(btn_thisweek);
            Controls.Add(btn_yesterday);
            Controls.Add(btn_today);
            Controls.Add(label2);
            Controls.Add(comb_class);
            Controls.Add(label1);
            Controls.Add(lab_UtilizationRate);
            Controls.Add(dateTime_start);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UtilizationRate";
            StartPosition = FormStartPosition.CenterParent;
            Text = "鋸帶資料";
            Load += UtilizationRate_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DateTimePicker dateTime_start;
        private Label lab_UtilizationRate;
        private Label label1;
        private ComboBox comb_class;
        private Label label2;
        private Button btn_today;
        private Button btn_yesterday;
        private Button btn_lastweek;
        private Button btn_thisweek;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lab_end;
        private Label lab_start;
        private DateTimePicker dateTimePicker_start4;
        private DateTimePicker dateTimePicker_end4;
        private DateTimePicker dateTimePicker_start3;
        private DateTimePicker dateTimePicker_end3;
        private DateTimePicker dateTimePicker_start2;
        private DateTimePicker dateTimePicker_end2;
        private DateTimePicker dateTimePicker_start1;
        private DateTimePicker dateTimePicker_end1;
        private Button btn_calculate4;
        private Button btn_calculate3;
        private Button btn_calculate2;
        private Button btn_calculate1;
        private Button btn_setting;
    }
}

