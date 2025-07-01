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
            lab_Drill_inverter = new Label();
            lab_Drill_inverterText = new Label();
            lab_Drill_clamping = new Label();
            lab_Drill_clampingText = new Label();
            lab_Drill_measurement = new Label();
            lab_Drill_measurementText = new Label();
            lab_Drill_loose_tools = new Label();
            lab_Drill_loose_toolsText = new Label();
            lab_Drill_originText = new Label();
            lab_Drill_origin = new Label();
            lab_Drill_total_Time = new Label();
            lab_Drill_total_TimeText = new Label();
            lab_Drill_outverter = new Label();
            lab_Drill_outverterText = new Label();
            lab_Drill_plc_usetime = new Label();
            lab_Drill_plc_usetimeText = new Label();
            lab_Drill_spindle3_usetime = new Label();
            lab_Drill_spindle3_usetimeText = new Label();
            lab_Drill_spindle2_usetime = new Label();
            lab_Drill_spindle2_usetimeText = new Label();
            lab_Drill_spindle1_usetime = new Label();
            lab_Drill_spindle1_usetimeText = new Label();
            lab_Drill_servo_usetime = new Label();
            lab_Drill_servo_usetimeText = new Label();
            lab_title = new Label();
            lab_titleText = new Label();
            lab_duringtime = new Label();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(706, 606);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lab_Drill_inverter, 1, 6);
            tableLayoutPanel1.Controls.Add(lab_Drill_inverterText, 0, 6);
            tableLayoutPanel1.Controls.Add(lab_Drill_clamping, 1, 12);
            tableLayoutPanel1.Controls.Add(lab_Drill_clampingText, 0, 12);
            tableLayoutPanel1.Controls.Add(lab_Drill_measurement, 1, 11);
            tableLayoutPanel1.Controls.Add(lab_Drill_measurementText, 0, 11);
            tableLayoutPanel1.Controls.Add(lab_Drill_loose_tools, 1, 10);
            tableLayoutPanel1.Controls.Add(lab_Drill_loose_toolsText, 0, 10);
            tableLayoutPanel1.Controls.Add(lab_Drill_originText, 0, 9);
            tableLayoutPanel1.Controls.Add(lab_Drill_origin, 1, 9);
            tableLayoutPanel1.Controls.Add(lab_Drill_total_Time, 1, 8);
            tableLayoutPanel1.Controls.Add(lab_Drill_total_TimeText, 0, 8);
            tableLayoutPanel1.Controls.Add(lab_Drill_outverter, 1, 7);
            tableLayoutPanel1.Controls.Add(lab_Drill_outverterText, 0, 7);
            tableLayoutPanel1.Controls.Add(lab_Drill_plc_usetime, 1, 5);
            tableLayoutPanel1.Controls.Add(lab_Drill_plc_usetimeText, 0, 5);
            tableLayoutPanel1.Controls.Add(lab_Drill_spindle3_usetime, 1, 4);
            tableLayoutPanel1.Controls.Add(lab_Drill_spindle3_usetimeText, 0, 4);
            tableLayoutPanel1.Controls.Add(lab_Drill_spindle2_usetime, 1, 3);
            tableLayoutPanel1.Controls.Add(lab_Drill_spindle2_usetimeText, 0, 3);
            tableLayoutPanel1.Controls.Add(lab_Drill_spindle1_usetime, 1, 2);
            tableLayoutPanel1.Controls.Add(lab_Drill_spindle1_usetimeText, 0, 2);
            tableLayoutPanel1.Controls.Add(lab_Drill_servo_usetime, 1, 1);
            tableLayoutPanel1.Controls.Add(lab_Drill_servo_usetimeText, 0, 1);
            tableLayoutPanel1.Controls.Add(lab_title, 1, 0);
            tableLayoutPanel1.Controls.Add(lab_titleText, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 13;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.704736F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.60793924F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(706, 606);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // lab_Drill_inverter
            // 
            lab_Drill_inverter.BackColor = Color.Transparent;
            lab_Drill_inverter.Dock = DockStyle.Fill;
            lab_Drill_inverter.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_inverter.Location = new Point(356, 282);
            lab_Drill_inverter.Name = "lab_Drill_inverter";
            lab_Drill_inverter.Size = new Size(347, 46);
            lab_Drill_inverter.TabIndex = 28;
            lab_Drill_inverter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_Drill_inverterText
            // 
            lab_Drill_inverterText.BackColor = Color.Transparent;
            lab_Drill_inverterText.Dock = DockStyle.Fill;
            lab_Drill_inverterText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_inverterText.Location = new Point(3, 282);
            lab_Drill_inverterText.Name = "lab_Drill_inverterText";
            lab_Drill_inverterText.Size = new Size(347, 46);
            lab_Drill_inverterText.TabIndex = 27;
            lab_Drill_inverterText.Text = "入料變頻器使用時間";
            lab_Drill_inverterText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_clamping
            // 
            lab_Drill_clamping.BackColor = Color.Transparent;
            lab_Drill_clamping.Dock = DockStyle.Fill;
            lab_Drill_clamping.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_clamping.Location = new Point(356, 558);
            lab_Drill_clamping.Name = "lab_Drill_clamping";
            lab_Drill_clamping.Size = new Size(347, 48);
            lab_Drill_clamping.TabIndex = 26;
            lab_Drill_clamping.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_Drill_clampingText
            // 
            lab_Drill_clampingText.BackColor = Color.Transparent;
            lab_Drill_clampingText.Dock = DockStyle.Fill;
            lab_Drill_clampingText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_clampingText.Location = new Point(3, 558);
            lab_Drill_clampingText.Name = "lab_Drill_clampingText";
            lab_Drill_clampingText.Size = new Size(347, 48);
            lab_Drill_clampingText.TabIndex = 25;
            lab_Drill_clampingText.Text = "送料台夾料檢知次數";
            lab_Drill_clampingText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_measurement
            // 
            lab_Drill_measurement.BackColor = Color.Transparent;
            lab_Drill_measurement.Dock = DockStyle.Fill;
            lab_Drill_measurement.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_measurement.Location = new Point(356, 512);
            lab_Drill_measurement.Name = "lab_Drill_measurement";
            lab_Drill_measurement.Size = new Size(347, 46);
            lab_Drill_measurement.TabIndex = 24;
            lab_Drill_measurement.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_Drill_measurementText
            // 
            lab_Drill_measurementText.BackColor = Color.Transparent;
            lab_Drill_measurementText.Dock = DockStyle.Fill;
            lab_Drill_measurementText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_measurementText.Location = new Point(3, 512);
            lab_Drill_measurementText.Name = "lab_Drill_measurementText";
            lab_Drill_measurementText.Size = new Size(347, 46);
            lab_Drill_measurementText.TabIndex = 23;
            lab_Drill_measurementText.Text = "刀具量測次數";
            lab_Drill_measurementText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_loose_tools
            // 
            lab_Drill_loose_tools.BackColor = Color.Transparent;
            lab_Drill_loose_tools.Dock = DockStyle.Fill;
            lab_Drill_loose_tools.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_loose_tools.Location = new Point(356, 466);
            lab_Drill_loose_tools.Name = "lab_Drill_loose_tools";
            lab_Drill_loose_tools.Size = new Size(347, 46);
            lab_Drill_loose_tools.TabIndex = 22;
            lab_Drill_loose_tools.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_Drill_loose_toolsText
            // 
            lab_Drill_loose_toolsText.BackColor = Color.Transparent;
            lab_Drill_loose_toolsText.Dock = DockStyle.Fill;
            lab_Drill_loose_toolsText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_loose_toolsText.Location = new Point(3, 466);
            lab_Drill_loose_toolsText.Name = "lab_Drill_loose_toolsText";
            lab_Drill_loose_toolsText.Size = new Size(347, 46);
            lab_Drill_loose_toolsText.TabIndex = 21;
            lab_Drill_loose_toolsText.Text = "主軸鬆刀次數";
            lab_Drill_loose_toolsText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_originText
            // 
            lab_Drill_originText.BackColor = Color.Transparent;
            lab_Drill_originText.Dock = DockStyle.Fill;
            lab_Drill_originText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_originText.Location = new Point(3, 420);
            lab_Drill_originText.Name = "lab_Drill_originText";
            lab_Drill_originText.Size = new Size(347, 46);
            lab_Drill_originText.TabIndex = 20;
            lab_Drill_originText.Text = "機台回原點次數";
            lab_Drill_originText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_origin
            // 
            lab_Drill_origin.BackColor = Color.Transparent;
            lab_Drill_origin.Dock = DockStyle.Fill;
            lab_Drill_origin.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_origin.Location = new Point(356, 420);
            lab_Drill_origin.Name = "lab_Drill_origin";
            lab_Drill_origin.Size = new Size(347, 46);
            lab_Drill_origin.TabIndex = 19;
            lab_Drill_origin.TextAlign = ContentAlignment.MiddleCenter;
            lab_Drill_origin.Click += lab_Drill_clamping_Click;
            // 
            // lab_Drill_total_Time
            // 
            lab_Drill_total_Time.BackColor = Color.Transparent;
            lab_Drill_total_Time.Dock = DockStyle.Fill;
            lab_Drill_total_Time.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_total_Time.Location = new Point(356, 374);
            lab_Drill_total_Time.Name = "lab_Drill_total_Time";
            lab_Drill_total_Time.Size = new Size(347, 46);
            lab_Drill_total_Time.TabIndex = 17;
            lab_Drill_total_Time.TextAlign = ContentAlignment.MiddleCenter;
            lab_Drill_total_Time.Click += lab_Drill_measurement_Click;
            // 
            // lab_Drill_total_TimeText
            // 
            lab_Drill_total_TimeText.BackColor = Color.Transparent;
            lab_Drill_total_TimeText.Dock = DockStyle.Fill;
            lab_Drill_total_TimeText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_total_TimeText.Location = new Point(3, 374);
            lab_Drill_total_TimeText.Name = "lab_Drill_total_TimeText";
            lab_Drill_total_TimeText.Size = new Size(347, 46);
            lab_Drill_total_TimeText.TabIndex = 16;
            lab_Drill_total_TimeText.Text = "機器使用時間";
            lab_Drill_total_TimeText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_outverter
            // 
            lab_Drill_outverter.BackColor = Color.Transparent;
            lab_Drill_outverter.Dock = DockStyle.Fill;
            lab_Drill_outverter.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_outverter.Location = new Point(356, 328);
            lab_Drill_outverter.Name = "lab_Drill_outverter";
            lab_Drill_outverter.Size = new Size(347, 46);
            lab_Drill_outverter.TabIndex = 15;
            lab_Drill_outverter.TextAlign = ContentAlignment.MiddleCenter;
            lab_Drill_outverter.Click += lab_Drill_loose_tools_Click;
            // 
            // lab_Drill_outverterText
            // 
            lab_Drill_outverterText.BackColor = Color.Transparent;
            lab_Drill_outverterText.Dock = DockStyle.Fill;
            lab_Drill_outverterText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_outverterText.Location = new Point(3, 328);
            lab_Drill_outverterText.Name = "lab_Drill_outverterText";
            lab_Drill_outverterText.Size = new Size(347, 46);
            lab_Drill_outverterText.TabIndex = 14;
            lab_Drill_outverterText.Text = "出料變頻器使用時間";
            lab_Drill_outverterText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_plc_usetime
            // 
            lab_Drill_plc_usetime.BackColor = Color.Transparent;
            lab_Drill_plc_usetime.Dock = DockStyle.Fill;
            lab_Drill_plc_usetime.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_plc_usetime.Location = new Point(356, 236);
            lab_Drill_plc_usetime.Name = "lab_Drill_plc_usetime";
            lab_Drill_plc_usetime.Size = new Size(347, 46);
            lab_Drill_plc_usetime.TabIndex = 11;
            lab_Drill_plc_usetime.Text = " 天  時  分  秒";
            lab_Drill_plc_usetime.TextAlign = ContentAlignment.MiddleCenter;
            lab_Drill_plc_usetime.Click += lab_Drill_total_Time_Click;
            // 
            // lab_Drill_plc_usetimeText
            // 
            lab_Drill_plc_usetimeText.BackColor = Color.Transparent;
            lab_Drill_plc_usetimeText.Dock = DockStyle.Fill;
            lab_Drill_plc_usetimeText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_plc_usetimeText.Location = new Point(3, 236);
            lab_Drill_plc_usetimeText.Name = "lab_Drill_plc_usetimeText";
            lab_Drill_plc_usetimeText.Size = new Size(347, 46);
            lab_Drill_plc_usetimeText.TabIndex = 10;
            lab_Drill_plc_usetimeText.Text = "PLC總使用時間";
            lab_Drill_plc_usetimeText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_spindle3_usetime
            // 
            lab_Drill_spindle3_usetime.BackColor = Color.Transparent;
            lab_Drill_spindle3_usetime.Dock = DockStyle.Fill;
            lab_Drill_spindle3_usetime.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_spindle3_usetime.Location = new Point(356, 190);
            lab_Drill_spindle3_usetime.Name = "lab_Drill_spindle3_usetime";
            lab_Drill_spindle3_usetime.Size = new Size(347, 46);
            lab_Drill_spindle3_usetime.TabIndex = 9;
            lab_Drill_spindle3_usetime.Text = " 天  時  分  秒";
            lab_Drill_spindle3_usetime.TextAlign = ContentAlignment.MiddleCenter;
            lab_Drill_spindle3_usetime.Click += lab_Drill_inverter_Click;
            // 
            // lab_Drill_spindle3_usetimeText
            // 
            lab_Drill_spindle3_usetimeText.BackColor = Color.Transparent;
            lab_Drill_spindle3_usetimeText.Dock = DockStyle.Fill;
            lab_Drill_spindle3_usetimeText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_spindle3_usetimeText.Location = new Point(3, 190);
            lab_Drill_spindle3_usetimeText.Name = "lab_Drill_spindle3_usetimeText";
            lab_Drill_spindle3_usetimeText.Size = new Size(347, 46);
            lab_Drill_spindle3_usetimeText.TabIndex = 8;
            lab_Drill_spindle3_usetimeText.Text = "主軸3啟動累積時間";
            lab_Drill_spindle3_usetimeText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_spindle2_usetime
            // 
            lab_Drill_spindle2_usetime.BackColor = Color.Transparent;
            lab_Drill_spindle2_usetime.Dock = DockStyle.Fill;
            lab_Drill_spindle2_usetime.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_spindle2_usetime.Location = new Point(356, 144);
            lab_Drill_spindle2_usetime.Name = "lab_Drill_spindle2_usetime";
            lab_Drill_spindle2_usetime.Size = new Size(347, 46);
            lab_Drill_spindle2_usetime.TabIndex = 7;
            lab_Drill_spindle2_usetime.Text = " 天  時  分  秒";
            lab_Drill_spindle2_usetime.TextAlign = ContentAlignment.MiddleCenter;
            lab_Drill_spindle2_usetime.Click += lab_Drill_plc_usetime_Click;
            // 
            // lab_Drill_spindle2_usetimeText
            // 
            lab_Drill_spindle2_usetimeText.BackColor = Color.Transparent;
            lab_Drill_spindle2_usetimeText.Dock = DockStyle.Fill;
            lab_Drill_spindle2_usetimeText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_spindle2_usetimeText.Location = new Point(3, 144);
            lab_Drill_spindle2_usetimeText.Name = "lab_Drill_spindle2_usetimeText";
            lab_Drill_spindle2_usetimeText.Size = new Size(347, 46);
            lab_Drill_spindle2_usetimeText.TabIndex = 6;
            lab_Drill_spindle2_usetimeText.Text = "主軸2啟動累積時間";
            lab_Drill_spindle2_usetimeText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_spindle1_usetime
            // 
            lab_Drill_spindle1_usetime.BackColor = Color.Transparent;
            lab_Drill_spindle1_usetime.Dock = DockStyle.Fill;
            lab_Drill_spindle1_usetime.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_spindle1_usetime.Location = new Point(356, 98);
            lab_Drill_spindle1_usetime.Name = "lab_Drill_spindle1_usetime";
            lab_Drill_spindle1_usetime.Size = new Size(347, 46);
            lab_Drill_spindle1_usetime.TabIndex = 5;
            lab_Drill_spindle1_usetime.Text = " 天  時  分  秒";
            lab_Drill_spindle1_usetime.TextAlign = ContentAlignment.MiddleCenter;
            lab_Drill_spindle1_usetime.Click += lab_Drill_spindle_usetime_Click;
            // 
            // lab_Drill_spindle1_usetimeText
            // 
            lab_Drill_spindle1_usetimeText.BackColor = Color.Transparent;
            lab_Drill_spindle1_usetimeText.Dock = DockStyle.Fill;
            lab_Drill_spindle1_usetimeText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_spindle1_usetimeText.Location = new Point(3, 98);
            lab_Drill_spindle1_usetimeText.Name = "lab_Drill_spindle1_usetimeText";
            lab_Drill_spindle1_usetimeText.Size = new Size(347, 46);
            lab_Drill_spindle1_usetimeText.TabIndex = 4;
            lab_Drill_spindle1_usetimeText.Text = "主軸1啟動累積時間";
            lab_Drill_spindle1_usetimeText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Drill_servo_usetime
            // 
            lab_Drill_servo_usetime.BackColor = Color.Transparent;
            lab_Drill_servo_usetime.Dock = DockStyle.Fill;
            lab_Drill_servo_usetime.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_servo_usetime.Location = new Point(356, 52);
            lab_Drill_servo_usetime.Name = "lab_Drill_servo_usetime";
            lab_Drill_servo_usetime.Size = new Size(347, 46);
            lab_Drill_servo_usetime.TabIndex = 3;
            lab_Drill_servo_usetime.Text = " 天  時  分  秒";
            lab_Drill_servo_usetime.TextAlign = ContentAlignment.MiddleCenter;
            lab_Drill_servo_usetime.Click += lab_Drill_servo_usetime_Click;
            // 
            // lab_Drill_servo_usetimeText
            // 
            lab_Drill_servo_usetimeText.AutoSize = true;
            lab_Drill_servo_usetimeText.BackColor = Color.Transparent;
            lab_Drill_servo_usetimeText.Dock = DockStyle.Fill;
            lab_Drill_servo_usetimeText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_servo_usetimeText.Location = new Point(3, 52);
            lab_Drill_servo_usetimeText.Name = "lab_Drill_servo_usetimeText";
            lab_Drill_servo_usetimeText.Size = new Size(347, 46);
            lab_Drill_servo_usetimeText.TabIndex = 2;
            lab_Drill_servo_usetimeText.Text = "伺服驅動器介面使用時間";
            lab_Drill_servo_usetimeText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_title
            // 
            lab_title.BackColor = Color.Transparent;
            lab_title.Dock = DockStyle.Fill;
            lab_title.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_title.Location = new Point(356, 0);
            lab_title.Name = "lab_title";
            lab_title.Size = new Size(347, 52);
            lab_title.TabIndex = 1;
            lab_title.Text = "目前累積時間";
            lab_title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_titleText
            // 
            lab_titleText.BackColor = Color.Transparent;
            lab_titleText.Dock = DockStyle.Fill;
            lab_titleText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_titleText.Location = new Point(3, 0);
            lab_titleText.Name = "lab_titleText";
            lab_titleText.Size = new Size(347, 52);
            lab_titleText.TabIndex = 0;
            lab_titleText.Text = "鑽床設備與周邊元件紀錄";
            lab_titleText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_duringtime
            // 
            lab_duringtime.AutoSize = true;
            lab_duringtime.Location = new Point(437, 48);
            lab_duringtime.Name = "lab_duringtime";
            lab_duringtime.Size = new Size(67, 15);
            lab_duringtime.TabIndex = 1;
            lab_duringtime.Text = "累積期間：";
            // 
            // Drill_Info
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(730, 630);
            Controls.Add(lab_duringtime);
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
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lab_Drill_servo_usetimeText;
        private Label lab_title;
        private Label lab_titleText;
        private Label lab_Drill_spindle2_usetime;
        private Label lab_Drill_spindle2_usetimeText;
        private Label lab_Drill_spindle1_usetime;
        private Label lab_Drill_spindle1_usetimeText;
        private Label lab_Drill_servo_usetime;
        private Label lab_Drill_origin;
        private Label lab_Drill_total_Time;
        private Label lab_Drill_total_TimeText;
        private Label lab_Drill_outverter;
        private Label lab_Drill_outverterText;
        private Label lab_Drill_plc_usetime;
        private Label lab_Drill_plc_usetimeText;
        private Label lab_Drill_spindle3_usetime;
        private Label lab_Drill_spindle3_usetimeText;
        private Label lab_Drill_originText;
        private Label lab_Drill_measurement;
        private Label lab_Drill_measurementText;
        private Label lab_Drill_loose_tools;
        private Label lab_Drill_loose_toolsText;
        private Label lab_Drill_clamping;
        private Label lab_Drill_clampingText;
        private Label lab_Drill_inverter;
        private Label lab_Drill_inverterText;
        private Label lab_duringtime;
    }
}

