namespace FX5U_IOMonitor
{
    partial class Drill_Info
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
            lab_feeder = new Label();
            label12 = new Label();
            lab_clamping = new Label();
            label11 = new Label();
            lab_measurement = new Label();
            label10 = new Label();
            lab_loose_tools = new Label();
            label9 = new Label();
            lab_origin = new Label();
            label8 = new Label();
            lab_Runtime = new Label();
            label7 = new Label();
            lab_Frequency_Converter_usetime = new Label();
            label6 = new Label();
            lab_PLC_usetime = new Label();
            label5 = new Label();
            lab_Spindle_usetime = new Label();
            label4 = new Label();
            lab_Servo_drives_usetime = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Location = new Point(33, 38);
            panel1.Name = "panel1";
            panel1.Size = new Size(655, 549);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lab_feeder, 1, 10);
            tableLayoutPanel1.Controls.Add(label12, 0, 10);
            tableLayoutPanel1.Controls.Add(lab_clamping, 1, 9);
            tableLayoutPanel1.Controls.Add(label11, 0, 9);
            tableLayoutPanel1.Controls.Add(lab_measurement, 1, 8);
            tableLayoutPanel1.Controls.Add(label10, 0, 8);
            tableLayoutPanel1.Controls.Add(lab_loose_tools, 1, 7);
            tableLayoutPanel1.Controls.Add(label9, 0, 7);
            tableLayoutPanel1.Controls.Add(lab_origin, 1, 6);
            tableLayoutPanel1.Controls.Add(label8, 0, 6);
            tableLayoutPanel1.Controls.Add(lab_Runtime, 1, 5);
            tableLayoutPanel1.Controls.Add(label7, 0, 5);
            tableLayoutPanel1.Controls.Add(lab_Frequency_Converter_usetime, 1, 4);
            tableLayoutPanel1.Controls.Add(label6, 0, 4);
            tableLayoutPanel1.Controls.Add(lab_PLC_usetime, 1, 3);
            tableLayoutPanel1.Controls.Add(label5, 0, 3);
            tableLayoutPanel1.Controls.Add(lab_Spindle_usetime, 1, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 2);
            tableLayoutPanel1.Controls.Add(lab_Servo_drives_usetime, 1, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 1);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 11;
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
            tableLayoutPanel1.Size = new Size(655, 549);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // lab_feeder
            // 
            lab_feeder.BackColor = Color.Transparent;
            lab_feeder.Dock = DockStyle.Fill;
            lab_feeder.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_feeder.Location = new Point(330, 495);
            lab_feeder.Name = "lab_feeder";
            lab_feeder.Size = new Size(322, 54);
            lab_feeder.TabIndex = 21;
            lab_feeder.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            label12.BackColor = Color.Transparent;
            label12.Dock = DockStyle.Fill;
            label12.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label12.Location = new Point(3, 495);
            label12.Name = "label12";
            label12.Size = new Size(321, 54);
            label12.TabIndex = 20;
            label12.Text = "送料機夾鬆次數";
            label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_clamping
            // 
            lab_clamping.BackColor = Color.Transparent;
            lab_clamping.Dock = DockStyle.Fill;
            lab_clamping.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_clamping.Location = new Point(330, 446);
            lab_clamping.Name = "lab_clamping";
            lab_clamping.Size = new Size(322, 49);
            lab_clamping.TabIndex = 19;
            lab_clamping.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            label11.BackColor = Color.Transparent;
            label11.Dock = DockStyle.Fill;
            label11.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label11.Location = new Point(3, 446);
            label11.Name = "label11";
            label11.Size = new Size(321, 49);
            label11.TabIndex = 18;
            label11.Text = "送料台夾料檢知次數";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_measurement
            // 
            lab_measurement.BackColor = Color.Transparent;
            lab_measurement.Dock = DockStyle.Fill;
            lab_measurement.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_measurement.Location = new Point(330, 397);
            lab_measurement.Name = "lab_measurement";
            lab_measurement.Size = new Size(322, 49);
            lab_measurement.TabIndex = 17;
            lab_measurement.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            label10.BackColor = Color.Transparent;
            label10.Dock = DockStyle.Fill;
            label10.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label10.Location = new Point(3, 397);
            label10.Name = "label10";
            label10.Size = new Size(321, 49);
            label10.TabIndex = 16;
            label10.Text = "刀具量測次數";
            label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_loose_tools
            // 
            lab_loose_tools.BackColor = Color.Transparent;
            lab_loose_tools.Dock = DockStyle.Fill;
            lab_loose_tools.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_loose_tools.Location = new Point(330, 348);
            lab_loose_tools.Name = "lab_loose_tools";
            lab_loose_tools.Size = new Size(322, 49);
            lab_loose_tools.TabIndex = 15;
            lab_loose_tools.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.BackColor = Color.Transparent;
            label9.Dock = DockStyle.Fill;
            label9.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label9.Location = new Point(3, 348);
            label9.Name = "label9";
            label9.Size = new Size(321, 49);
            label9.TabIndex = 14;
            label9.Text = "主軸鬆刀次數";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_origin
            // 
            lab_origin.BackColor = Color.Transparent;
            lab_origin.Dock = DockStyle.Fill;
            lab_origin.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_origin.Location = new Point(330, 299);
            lab_origin.Name = "lab_origin";
            lab_origin.Size = new Size(322, 49);
            lab_origin.TabIndex = 13;
            lab_origin.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            label8.BackColor = Color.Transparent;
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label8.Location = new Point(3, 299);
            label8.Name = "label8";
            label8.Size = new Size(321, 49);
            label8.TabIndex = 12;
            label8.Text = "機台回原點次數";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Runtime
            // 
            lab_Runtime.BackColor = Color.Transparent;
            lab_Runtime.Dock = DockStyle.Fill;
            lab_Runtime.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Runtime.Location = new Point(330, 250);
            lab_Runtime.Name = "lab_Runtime";
            lab_Runtime.Size = new Size(322, 49);
            lab_Runtime.TabIndex = 11;
            lab_Runtime.Text = " 天  時  分  秒";
            lab_Runtime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.BackColor = Color.Transparent;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label7.Location = new Point(3, 250);
            label7.Name = "label7";
            label7.Size = new Size(321, 49);
            label7.TabIndex = 10;
            label7.Text = "機器使用時間";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Frequency_Converter_usetime
            // 
            lab_Frequency_Converter_usetime.BackColor = Color.Transparent;
            lab_Frequency_Converter_usetime.Dock = DockStyle.Fill;
            lab_Frequency_Converter_usetime.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Frequency_Converter_usetime.Location = new Point(330, 201);
            lab_Frequency_Converter_usetime.Name = "lab_Frequency_Converter_usetime";
            lab_Frequency_Converter_usetime.Size = new Size(322, 49);
            lab_Frequency_Converter_usetime.TabIndex = 9;
            lab_Frequency_Converter_usetime.Text = " 天  時  分  秒";
            lab_Frequency_Converter_usetime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.BackColor = Color.Transparent;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label6.Location = new Point(3, 201);
            label6.Name = "label6";
            label6.Size = new Size(321, 49);
            label6.TabIndex = 8;
            label6.Text = "變頻器使用時間";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_PLC_usetime
            // 
            lab_PLC_usetime.BackColor = Color.Transparent;
            lab_PLC_usetime.Dock = DockStyle.Fill;
            lab_PLC_usetime.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_PLC_usetime.Location = new Point(330, 152);
            lab_PLC_usetime.Name = "lab_PLC_usetime";
            lab_PLC_usetime.Size = new Size(322, 49);
            lab_PLC_usetime.TabIndex = 7;
            lab_PLC_usetime.Text = " 天  時  分  秒";
            lab_PLC_usetime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label5.Location = new Point(3, 152);
            label5.Name = "label5";
            label5.Size = new Size(321, 49);
            label5.TabIndex = 6;
            label5.Text = "PLC總使用時間";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Spindle_usetime
            // 
            lab_Spindle_usetime.BackColor = Color.Transparent;
            lab_Spindle_usetime.Dock = DockStyle.Fill;
            lab_Spindle_usetime.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Spindle_usetime.Location = new Point(330, 103);
            lab_Spindle_usetime.Name = "lab_Spindle_usetime";
            lab_Spindle_usetime.Size = new Size(322, 49);
            lab_Spindle_usetime.TabIndex = 5;
            lab_Spindle_usetime.Text = " 天  時  分  秒";
            lab_Spindle_usetime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label4.Location = new Point(3, 103);
            label4.Name = "label4";
            label4.Size = new Size(321, 49);
            label4.TabIndex = 4;
            label4.Text = "主軸啟動累積時間";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Servo_drives_usetime
            // 
            lab_Servo_drives_usetime.BackColor = Color.Transparent;
            lab_Servo_drives_usetime.Dock = DockStyle.Fill;
            lab_Servo_drives_usetime.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Servo_drives_usetime.Location = new Point(330, 54);
            lab_Servo_drives_usetime.Name = "lab_Servo_drives_usetime";
            lab_Servo_drives_usetime.Size = new Size(322, 49);
            lab_Servo_drives_usetime.TabIndex = 3;
            lab_Servo_drives_usetime.Text = " 天  時  分  秒";
            lab_Servo_drives_usetime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label3.Location = new Point(3, 54);
            label3.Name = "label3";
            label3.Size = new Size(321, 49);
            label3.TabIndex = 2;
            label3.Text = "伺服驅動器介面使用時間";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label2.Location = new Point(330, 0);
            label2.Name = "label2";
            label2.Size = new Size(322, 54);
            label2.TabIndex = 1;
            label2.Text = "目前累積時間";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(321, 54);
            label1.TabIndex = 0;
            label1.Text = "設備與周邊元件壽命總表";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Drill_Info
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(730, 630);
            Controls.Add(panel1);
            Margin = new Padding(4);
            Name = "Drill_Info";
            StartPosition = FormStartPosition.CenterParent;
            Text = "鑽床資料";
            Load += Main_Load;
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label lab_PLC_usetime;
        private Label label5;
        private Label lab_Spindle_usetime;
        private Label label4;
        private Label lab_Servo_drives_usetime;
        private Label lab_feeder;
        private Label label12;
        private Label lab_clamping;
        private Label label11;
        private Label lab_measurement;
        private Label label10;
        private Label lab_loose_tools;
        private Label label9;
        private Label lab_origin;
        private Label label8;
        private Label lab_Runtime;
        private Label label7;
        private Label lab_Frequency_Converter_usetime;
        private Label label6;
    }
}

