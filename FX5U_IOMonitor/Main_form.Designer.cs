namespace FX5U_IOMonitor
{
    partial class Main_form
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Size baseSize = new Size(956, 625);
        private Dictionary<Control, Rectangle> originalBounds = new();
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            SaveOriginalBounds(this);
        }
        private void SaveOriginalBounds(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                originalBounds[ctrl] = ctrl.Bounds;

                if (ctrl.HasChildren)
                    SaveOriginalBounds(ctrl);
            }
        }
        private void Main_form_Resize(object? sender, EventArgs e)
        {
            float scaleX = (float)this.Width / baseSize.Width;
            float scaleY = (float)this.Height / baseSize.Height;
            float scale = Math.Min(scaleX, scaleY);

            ResizeControls(this, scale);
        }
        private void ResizeControls(Control parent, float scale)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (originalBounds.ContainsKey(ctrl))
                {
                    Rectangle original = originalBounds[ctrl];
                    ctrl.SetBounds(
                        (int)(original.X * scale),
                        (int)(original.Y * scale),
                        (int)(original.Width * scale),
                        (int)(original.Height * scale)
                    );

                    // 字體大小等比例縮放
                    ctrl.Font = new Font(ctrl.Font.FontFamily, Math.Max(6f, ctrl.Font.Size * scale), ctrl.Font.Style);
                }

                if (ctrl.HasChildren)
                    ResizeControls(ctrl, scale);
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
            panel12 = new Panel();
            panel3 = new Panel();
            btn_Sawing_lifesetting = new Button();
            lab_Drill_lifesetting = new Label();
            tbPanel_Swing_sum = new TableLayoutPanel();
            lab_sumS = new Label();
            lab_sum_swing = new Label();
            tbPanel_Swing_connect = new TableLayoutPanel();
            lab_connectS = new Label();
            lab_connect_swing = new Label();
            panel4 = new Panel();
            btn_SawBand = new Button();
            tbPanel_Swing_red = new TableLayoutPanel();
            lab_rS = new Label();
            lab_red_swing = new Label();
            tbPanel_Swing_disconnect = new TableLayoutPanel();
            lab_disS = new Label();
            lab_disconnect_sawing = new Label();
            tbPanel_Swing_green = new TableLayoutPanel();
            lab_gS = new Label();
            lab_green_swing = new Label();
            tbPanel_Swing_yellow = new TableLayoutPanel();
            lab_yS = new Label();
            lab_yellow_swing = new Label();
            panel11 = new Panel();
            tbPanel_Drill_sum = new TableLayoutPanel();
            lab_sumD = new Label();
            lab_sum = new Label();
            tbPanel_Drill_connect = new TableLayoutPanel();
            lab_connectD = new Label();
            lab_connect = new Label();
            tbPanel_Drill_red = new TableLayoutPanel();
            lab_rD = new Label();
            lab_red = new Label();
            tbPanel_Drill_disconnect = new TableLayoutPanel();
            lab_disD = new Label();
            lab_disconnect = new Label();
            tbPanel_Drill_green = new TableLayoutPanel();
            lab_gD = new Label();
            lab_green = new Label();
            tbPanel_Drill_yellow = new TableLayoutPanel();
            lab_yD = new Label();
            lab_yellow = new Label();
            panel1 = new Panel();
            btn_Drill_lifesetting = new Button();
            lab_Sawing_lifesetting = new Label();
            panel2 = new Panel();
            btn_Drill_Info = new Button();
            btn_machInfo = new Button();
            panel_SwingTime = new Panel();
            tbPanel_Swing_totaltime = new TableLayoutPanel();
            lb_totaltime = new Label();
            lb_totaltimeText = new Label();
            tbPanel_countdowntools = new TableLayoutPanel();
            lb_remain_tools = new Label();
            lb_remain_toolsText = new Label();
            tbPanel_countdown = new TableLayoutPanel();
            lb_countdown_time = new Label();
            lb_countdown_timeText = new Label();
            tbPanel_Electricity = new TableLayoutPanel();
            lb_electricity = new Label();
            lab_reset1 = new Label();
            tbPanel_powerconsumption = new TableLayoutPanel();
            lb_swingpower = new Label();
            lab_power1 = new Label();
            tbPanel_oilpressure = new TableLayoutPanel();
            lb_oilpress = new Label();
            lb_oilpressText = new Label();
            tbPanel_Ammeter = new TableLayoutPanel();
            lb_swing_current = new Label();
            label_Ammeter1 = new Label();
            tbPanel_Voltage = new TableLayoutPanel();
            lb_swing_Voltage = new Label();
            lb_Voltage1 = new Label();
            tbPanel_cuting_speed = new TableLayoutPanel();
            lb_sawing_cutingspeed = new Label();
            lb_sawing_cutingspeedText = new Label();
            tbPanel_motor_current = new TableLayoutPanel();
            lb_swing_motor_current = new Label();
            lb_swing_motor_currentText = new Label();
            panel_Swing = new Panel();
            panel_DrillTime = new Panel();
            tbPanel_drillpower = new TableLayoutPanel();
            lb_drillpower = new Label();
            lab_power = new Label();
            tbPanel_drilldu = new TableLayoutPanel();
            lb_drill_du = new Label();
            lab_reset = new Label();
            tbPanel_drillcurrent = new TableLayoutPanel();
            lb_drill_current = new Label();
            label_Ammeter = new Label();
            tbPanel_Drill_totaltime = new TableLayoutPanel();
            lb_Drill_totaltime = new Label();
            lb_Drill_totaltimeText = new Label();
            tbPanel_drillvoltage = new TableLayoutPanel();
            lb_drill_Voltage = new Label();
            lb_Voltage = new Label();
            tbPanel_cutingtime = new TableLayoutPanel();
            lb_cutingtime = new Label();
            lb_cutingtimeText = new Label();
            panel_Drill = new Panel();
            lab_time = new Label();
            panel12.SuspendLayout();
            panel3.SuspendLayout();
            tbPanel_Swing_sum.SuspendLayout();
            tbPanel_Swing_connect.SuspendLayout();
            panel4.SuspendLayout();
            tbPanel_Swing_red.SuspendLayout();
            tbPanel_Swing_disconnect.SuspendLayout();
            tbPanel_Swing_green.SuspendLayout();
            tbPanel_Swing_yellow.SuspendLayout();
            panel11.SuspendLayout();
            tbPanel_Drill_sum.SuspendLayout();
            tbPanel_Drill_connect.SuspendLayout();
            tbPanel_Drill_red.SuspendLayout();
            tbPanel_Drill_disconnect.SuspendLayout();
            tbPanel_Drill_green.SuspendLayout();
            tbPanel_Drill_yellow.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel_SwingTime.SuspendLayout();
            tbPanel_Swing_totaltime.SuspendLayout();
            tbPanel_countdowntools.SuspendLayout();
            tbPanel_countdown.SuspendLayout();
            tbPanel_Electricity.SuspendLayout();
            tbPanel_powerconsumption.SuspendLayout();
            tbPanel_oilpressure.SuspendLayout();
            tbPanel_Ammeter.SuspendLayout();
            tbPanel_Voltage.SuspendLayout();
            tbPanel_cuting_speed.SuspendLayout();
            tbPanel_motor_current.SuspendLayout();
            panel_DrillTime.SuspendLayout();
            tbPanel_drillpower.SuspendLayout();
            tbPanel_drilldu.SuspendLayout();
            tbPanel_drillcurrent.SuspendLayout();
            tbPanel_Drill_totaltime.SuspendLayout();
            tbPanel_drillvoltage.SuspendLayout();
            tbPanel_cutingtime.SuspendLayout();
            SuspendLayout();
            // 
            // panel12
            // 
            panel12.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel12.BackColor = SystemColors.ButtonHighlight;
            panel12.BorderStyle = BorderStyle.FixedSingle;
            panel12.Controls.Add(panel3);
            panel12.Controls.Add(tbPanel_Swing_sum);
            panel12.Controls.Add(tbPanel_Swing_connect);
            panel12.Controls.Add(panel4);
            panel12.Controls.Add(tbPanel_Swing_red);
            panel12.Controls.Add(tbPanel_Swing_disconnect);
            panel12.Controls.Add(tbPanel_Swing_green);
            panel12.Controls.Add(tbPanel_Swing_yellow);
            panel12.Location = new Point(566, 319);
            panel12.Name = "panel12";
            panel12.Size = new Size(385, 296);
            panel12.TabIndex = 48;
            panel12.Paint += panel12_Paint;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ButtonHighlight;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(btn_Sawing_lifesetting);
            panel3.Controls.Add(lab_Drill_lifesetting);
            panel3.Location = new Point(21, 218);
            panel3.Name = "panel3";
            panel3.Size = new Size(220, 63);
            panel3.TabIndex = 57;
            // 
            // btn_Sawing_lifesetting
            // 
            btn_Sawing_lifesetting.BackColor = SystemColors.ButtonHighlight;
            btn_Sawing_lifesetting.FlatStyle = FlatStyle.Flat;
            btn_Sawing_lifesetting.Location = new Point(119, 17);
            btn_Sawing_lifesetting.Margin = new Padding(4);
            btn_Sawing_lifesetting.Name = "btn_Sawing_lifesetting";
            btn_Sawing_lifesetting.Size = new Size(91, 28);
            btn_Sawing_lifesetting.TabIndex = 57;
            btn_Sawing_lifesetting.Text = "設定";
            btn_Sawing_lifesetting.UseVisualStyleBackColor = false;
            btn_Sawing_lifesetting.Click += btn_Sawing_lifesetting_Click;
            // 
            // lab_Drill_lifesetting
            // 
            lab_Drill_lifesetting.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_Drill_lifesetting.AutoSize = true;
            lab_Drill_lifesetting.BackColor = SystemColors.ButtonHighlight;
            lab_Drill_lifesetting.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Drill_lifesetting.Location = new Point(13, 21);
            lab_Drill_lifesetting.Name = "lab_Drill_lifesetting";
            lab_Drill_lifesetting.Size = new Size(99, 19);
            lab_Drill_lifesetting.TabIndex = 43;
            lab_Drill_lifesetting.Text = "元件壽命設定";
            lab_Drill_lifesetting.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Swing_sum
            // 
            tbPanel_Swing_sum.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Swing_sum.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_sum.ColumnCount = 1;
            tbPanel_Swing_sum.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Swing_sum.Controls.Add(lab_sumS, 0, 1);
            tbPanel_Swing_sum.Controls.Add(lab_sum_swing, 0, 0);
            tbPanel_Swing_sum.Location = new Point(21, 12);
            tbPanel_Swing_sum.Name = "tbPanel_Swing_sum";
            tbPanel_Swing_sum.RowCount = 2;
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 52.1074562F));
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 47.8925438F));
            tbPanel_Swing_sum.Size = new Size(100, 90);
            tbPanel_Swing_sum.TabIndex = 33;
            // 
            // lab_sumS
            // 
            lab_sumS.AutoSize = true;
            lab_sumS.BackColor = SystemColors.ButtonHighlight;
            lab_sumS.Dock = DockStyle.Fill;
            lab_sumS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_sumS.Location = new Point(3, 45);
            lab_sumS.Name = "lab_sumS";
            lab_sumS.Size = new Size(92, 43);
            lab_sumS.TabIndex = 23;
            lab_sumS.Text = "監控總數";
            lab_sumS.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_sum_swing
            // 
            lab_sum_swing.AutoSize = true;
            lab_sum_swing.BackColor = SystemColors.ButtonHighlight;
            lab_sum_swing.Dock = DockStyle.Fill;
            lab_sum_swing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_sum_swing.Location = new Point(3, 0);
            lab_sum_swing.Name = "lab_sum_swing";
            lab_sum_swing.Size = new Size(92, 45);
            lab_sum_swing.TabIndex = 21;
            lab_sum_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Swing_connect
            // 
            tbPanel_Swing_connect.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Swing_connect.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_connect.ColumnCount = 1;
            tbPanel_Swing_connect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_connect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_connect.Controls.Add(lab_connectS, 0, 1);
            tbPanel_Swing_connect.Controls.Add(lab_connect_swing, 0, 0);
            tbPanel_Swing_connect.Location = new Point(137, 13);
            tbPanel_Swing_connect.Name = "tbPanel_Swing_connect";
            tbPanel_Swing_connect.RowCount = 2;
            tbPanel_Swing_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tbPanel_Swing_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tbPanel_Swing_connect.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_connect.Size = new Size(100, 90);
            tbPanel_Swing_connect.TabIndex = 35;
            // 
            // lab_connectS
            // 
            lab_connectS.AutoSize = true;
            lab_connectS.BackColor = SystemColors.ButtonHighlight;
            lab_connectS.Dock = DockStyle.Fill;
            lab_connectS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connectS.Location = new Point(3, 44);
            lab_connectS.Name = "lab_connectS";
            lab_connectS.Size = new Size(92, 44);
            lab_connectS.TabIndex = 23;
            lab_connectS.Text = "連接總數";
            lab_connectS.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_connect_swing
            // 
            lab_connect_swing.AutoSize = true;
            lab_connect_swing.BackColor = SystemColors.ButtonHighlight;
            lab_connect_swing.Dock = DockStyle.Fill;
            lab_connect_swing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connect_swing.Location = new Point(3, 0);
            lab_connect_swing.Name = "lab_connect_swing";
            lab_connect_swing.Size = new Size(92, 44);
            lab_connect_swing.TabIndex = 21;
            lab_connect_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ButtonHighlight;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(btn_SawBand);
            panel4.Location = new Point(253, 218);
            panel4.Name = "panel4";
            panel4.Size = new Size(103, 63);
            panel4.TabIndex = 48;
            // 
            // btn_SawBand
            // 
            btn_SawBand.BackColor = SystemColors.ButtonHighlight;
            btn_SawBand.FlatStyle = FlatStyle.Flat;
            btn_SawBand.Location = new Point(6, 17);
            btn_SawBand.Margin = new Padding(4);
            btn_SawBand.Name = "btn_SawBand";
            btn_SawBand.Size = new Size(89, 28);
            btn_SawBand.TabIndex = 54;
            btn_SawBand.Text = "鋸帶資料";
            btn_SawBand.UseVisualStyleBackColor = false;
            btn_SawBand.Click += btn_SawBand_Click;
            // 
            // tbPanel_Swing_red
            // 
            tbPanel_Swing_red.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Swing_red.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_red.ColumnCount = 1;
            tbPanel_Swing_red.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Swing_red.Controls.Add(lab_rS, 0, 1);
            tbPanel_Swing_red.Controls.Add(lab_red_swing, 0, 0);
            tbPanel_Swing_red.Location = new Point(256, 115);
            tbPanel_Swing_red.Name = "tbPanel_Swing_red";
            tbPanel_Swing_red.RowCount = 2;
            tbPanel_Swing_red.RowStyles.Add(new RowStyle(SizeType.Percent, 52.1074562F));
            tbPanel_Swing_red.RowStyles.Add(new RowStyle(SizeType.Percent, 47.8925438F));
            tbPanel_Swing_red.Size = new Size(100, 90);
            tbPanel_Swing_red.TabIndex = 34;
            // 
            // lab_rS
            // 
            lab_rS.AutoSize = true;
            lab_rS.BackColor = SystemColors.ButtonHighlight;
            lab_rS.Dock = DockStyle.Fill;
            lab_rS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_rS.Location = new Point(3, 45);
            lab_rS.Name = "lab_rS";
            lab_rS.Size = new Size(92, 43);
            lab_rS.TabIndex = 22;
            lab_rS.Text = "紅燈總數";
            lab_rS.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_red_swing
            // 
            lab_red_swing.AutoSize = true;
            lab_red_swing.BackColor = SystemColors.ButtonHighlight;
            lab_red_swing.Dock = DockStyle.Fill;
            lab_red_swing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_red_swing.Location = new Point(3, 0);
            lab_red_swing.Name = "lab_red_swing";
            lab_red_swing.Size = new Size(92, 45);
            lab_red_swing.TabIndex = 21;
            lab_red_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Swing_disconnect
            // 
            tbPanel_Swing_disconnect.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Swing_disconnect.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_disconnect.ColumnCount = 1;
            tbPanel_Swing_disconnect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_disconnect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_disconnect.Controls.Add(lab_disS, 0, 1);
            tbPanel_Swing_disconnect.Controls.Add(lab_disconnect_sawing, 0, 0);
            tbPanel_Swing_disconnect.Location = new Point(256, 12);
            tbPanel_Swing_disconnect.Name = "tbPanel_Swing_disconnect";
            tbPanel_Swing_disconnect.RowCount = 2;
            tbPanel_Swing_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 53.4090919F));
            tbPanel_Swing_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 46.5909081F));
            tbPanel_Swing_disconnect.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_disconnect.Size = new Size(100, 90);
            tbPanel_Swing_disconnect.TabIndex = 36;
            // 
            // lab_disS
            // 
            lab_disS.AutoSize = true;
            lab_disS.BackColor = SystemColors.ButtonHighlight;
            lab_disS.Dock = DockStyle.Fill;
            lab_disS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_disS.Location = new Point(3, 47);
            lab_disS.Name = "lab_disS";
            lab_disS.Size = new Size(92, 41);
            lab_disS.TabIndex = 23;
            lab_disS.Text = "元件異常";
            lab_disS.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_disconnect_sawing
            // 
            lab_disconnect_sawing.AutoSize = true;
            lab_disconnect_sawing.BackColor = SystemColors.ButtonHighlight;
            lab_disconnect_sawing.Dock = DockStyle.Fill;
            lab_disconnect_sawing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_disconnect_sawing.Location = new Point(3, 0);
            lab_disconnect_sawing.Name = "lab_disconnect_sawing";
            lab_disconnect_sawing.Size = new Size(92, 47);
            lab_disconnect_sawing.TabIndex = 21;
            lab_disconnect_sawing.TextAlign = ContentAlignment.MiddleCenter;
            lab_disconnect_sawing.Click += lab_disconnect_sawing_Click;
            // 
            // tbPanel_Swing_green
            // 
            tbPanel_Swing_green.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Swing_green.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_green.ColumnCount = 1;
            tbPanel_Swing_green.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Swing_green.Controls.Add(lab_gS, 0, 1);
            tbPanel_Swing_green.Controls.Add(lab_green_swing, 0, 0);
            tbPanel_Swing_green.Location = new Point(21, 115);
            tbPanel_Swing_green.Name = "tbPanel_Swing_green";
            tbPanel_Swing_green.RowCount = 2;
            tbPanel_Swing_green.RowStyles.Add(new RowStyle(SizeType.Percent, 52.655674F));
            tbPanel_Swing_green.RowStyles.Add(new RowStyle(SizeType.Percent, 47.34432F));
            tbPanel_Swing_green.Size = new Size(100, 90);
            tbPanel_Swing_green.TabIndex = 37;
            // 
            // lab_gS
            // 
            lab_gS.AutoSize = true;
            lab_gS.BackColor = SystemColors.ButtonHighlight;
            lab_gS.Dock = DockStyle.Fill;
            lab_gS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_gS.Location = new Point(3, 46);
            lab_gS.Name = "lab_gS";
            lab_gS.Size = new Size(92, 42);
            lab_gS.TabIndex = 23;
            lab_gS.Text = "綠燈總數";
            lab_gS.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_green_swing
            // 
            lab_green_swing.AutoSize = true;
            lab_green_swing.BackColor = SystemColors.ButtonHighlight;
            lab_green_swing.Dock = DockStyle.Fill;
            lab_green_swing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_green_swing.Location = new Point(3, 0);
            lab_green_swing.Name = "lab_green_swing";
            lab_green_swing.Size = new Size(92, 46);
            lab_green_swing.TabIndex = 21;
            lab_green_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Swing_yellow
            // 
            tbPanel_Swing_yellow.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Swing_yellow.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_yellow.ColumnCount = 1;
            tbPanel_Swing_yellow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Swing_yellow.Controls.Add(lab_yS, 0, 1);
            tbPanel_Swing_yellow.Controls.Add(lab_yellow_swing, 0, 0);
            tbPanel_Swing_yellow.Location = new Point(137, 116);
            tbPanel_Swing_yellow.Name = "tbPanel_Swing_yellow";
            tbPanel_Swing_yellow.RowCount = 2;
            tbPanel_Swing_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 52.655674F));
            tbPanel_Swing_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 47.34432F));
            tbPanel_Swing_yellow.Size = new Size(100, 90);
            tbPanel_Swing_yellow.TabIndex = 38;
            // 
            // lab_yS
            // 
            lab_yS.AutoSize = true;
            lab_yS.BackColor = SystemColors.ButtonHighlight;
            lab_yS.Dock = DockStyle.Fill;
            lab_yS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yS.Location = new Point(3, 46);
            lab_yS.Name = "lab_yS";
            lab_yS.Size = new Size(92, 42);
            lab_yS.TabIndex = 23;
            lab_yS.Text = "黃燈總數";
            lab_yS.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_yellow_swing
            // 
            lab_yellow_swing.AutoSize = true;
            lab_yellow_swing.BackColor = SystemColors.ButtonHighlight;
            lab_yellow_swing.Dock = DockStyle.Fill;
            lab_yellow_swing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yellow_swing.Location = new Point(3, 0);
            lab_yellow_swing.Name = "lab_yellow_swing";
            lab_yellow_swing.Size = new Size(92, 46);
            lab_yellow_swing.TabIndex = 21;
            lab_yellow_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel11
            // 
            panel11.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel11.BackColor = SystemColors.ButtonHighlight;
            panel11.BorderStyle = BorderStyle.FixedSingle;
            panel11.Controls.Add(tbPanel_Drill_sum);
            panel11.Controls.Add(tbPanel_Drill_connect);
            panel11.Controls.Add(tbPanel_Drill_red);
            panel11.Controls.Add(tbPanel_Drill_disconnect);
            panel11.Controls.Add(tbPanel_Drill_green);
            panel11.Controls.Add(tbPanel_Drill_yellow);
            panel11.Controls.Add(panel1);
            panel11.Controls.Add(panel2);
            panel11.Location = new Point(566, 10);
            panel11.Name = "panel11";
            panel11.Size = new Size(385, 297);
            panel11.TabIndex = 49;
            panel11.Paint += panel11_Paint;
            // 
            // tbPanel_Drill_sum
            // 
            tbPanel_Drill_sum.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Drill_sum.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_sum.ColumnCount = 1;
            tbPanel_Drill_sum.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Drill_sum.Controls.Add(lab_sumD, 0, 1);
            tbPanel_Drill_sum.Controls.Add(lab_sum, 0, 0);
            tbPanel_Drill_sum.Location = new Point(21, 15);
            tbPanel_Drill_sum.Name = "tbPanel_Drill_sum";
            tbPanel_Drill_sum.RowCount = 2;
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 57.4018135F));
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 42.59819F));
            tbPanel_Drill_sum.Size = new Size(100, 90);
            tbPanel_Drill_sum.TabIndex = 27;
            // 
            // lab_sumD
            // 
            lab_sumD.AutoSize = true;
            lab_sumD.BackColor = SystemColors.ButtonHighlight;
            lab_sumD.Dock = DockStyle.Fill;
            lab_sumD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_sumD.Location = new Point(3, 50);
            lab_sumD.Name = "lab_sumD";
            lab_sumD.Size = new Size(92, 38);
            lab_sumD.TabIndex = 23;
            lab_sumD.Text = "監控總數";
            lab_sumD.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_sum
            // 
            lab_sum.AutoSize = true;
            lab_sum.BackColor = SystemColors.ButtonHighlight;
            lab_sum.Dock = DockStyle.Fill;
            lab_sum.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_sum.Location = new Point(3, 0);
            lab_sum.Name = "lab_sum";
            lab_sum.Size = new Size(92, 50);
            lab_sum.TabIndex = 21;
            lab_sum.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Drill_connect
            // 
            tbPanel_Drill_connect.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Drill_connect.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_connect.ColumnCount = 1;
            tbPanel_Drill_connect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Drill_connect.Controls.Add(lab_connectD, 0, 1);
            tbPanel_Drill_connect.Controls.Add(lab_connect, 0, 0);
            tbPanel_Drill_connect.Location = new Point(137, 14);
            tbPanel_Drill_connect.Name = "tbPanel_Drill_connect";
            tbPanel_Drill_connect.RowCount = 2;
            tbPanel_Drill_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 57.4018135F));
            tbPanel_Drill_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 42.59819F));
            tbPanel_Drill_connect.Size = new Size(100, 90);
            tbPanel_Drill_connect.TabIndex = 29;
            // 
            // lab_connectD
            // 
            lab_connectD.AutoSize = true;
            lab_connectD.BackColor = SystemColors.ButtonHighlight;
            lab_connectD.Dock = DockStyle.Fill;
            lab_connectD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connectD.Location = new Point(3, 50);
            lab_connectD.Name = "lab_connectD";
            lab_connectD.Size = new Size(92, 38);
            lab_connectD.TabIndex = 23;
            lab_connectD.Text = "連接總數";
            lab_connectD.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_connect
            // 
            lab_connect.AutoSize = true;
            lab_connect.BackColor = SystemColors.ButtonHighlight;
            lab_connect.Dock = DockStyle.Fill;
            lab_connect.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connect.Location = new Point(3, 0);
            lab_connect.Name = "lab_connect";
            lab_connect.Size = new Size(92, 50);
            lab_connect.TabIndex = 21;
            lab_connect.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Drill_red
            // 
            tbPanel_Drill_red.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Drill_red.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_red.ColumnCount = 1;
            tbPanel_Drill_red.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Drill_red.Controls.Add(lab_rD, 0, 1);
            tbPanel_Drill_red.Controls.Add(lab_red, 0, 0);
            tbPanel_Drill_red.Location = new Point(256, 121);
            tbPanel_Drill_red.Name = "tbPanel_Drill_red";
            tbPanel_Drill_red.RowCount = 2;
            tbPanel_Drill_red.RowStyles.Add(new RowStyle(SizeType.Percent, 53.9132652F));
            tbPanel_Drill_red.RowStyles.Add(new RowStyle(SizeType.Percent, 46.0867348F));
            tbPanel_Drill_red.Size = new Size(100, 90);
            tbPanel_Drill_red.TabIndex = 28;
            // 
            // lab_rD
            // 
            lab_rD.AutoSize = true;
            lab_rD.BackColor = SystemColors.ButtonHighlight;
            lab_rD.Dock = DockStyle.Fill;
            lab_rD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_rD.Location = new Point(3, 47);
            lab_rD.Name = "lab_rD";
            lab_rD.Size = new Size(92, 41);
            lab_rD.TabIndex = 22;
            lab_rD.Text = "紅燈總數";
            lab_rD.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_red
            // 
            lab_red.AutoSize = true;
            lab_red.BackColor = SystemColors.ButtonHighlight;
            lab_red.Dock = DockStyle.Fill;
            lab_red.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_red.Location = new Point(3, 0);
            lab_red.Name = "lab_red";
            lab_red.Size = new Size(92, 47);
            lab_red.TabIndex = 21;
            lab_red.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Drill_disconnect
            // 
            tbPanel_Drill_disconnect.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Drill_disconnect.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_disconnect.ColumnCount = 1;
            tbPanel_Drill_disconnect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Drill_disconnect.Controls.Add(lab_disD, 0, 1);
            tbPanel_Drill_disconnect.Controls.Add(lab_disconnect, 0, 0);
            tbPanel_Drill_disconnect.Location = new Point(256, 13);
            tbPanel_Drill_disconnect.Name = "tbPanel_Drill_disconnect";
            tbPanel_Drill_disconnect.RowCount = 2;
            tbPanel_Drill_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 57.4018135F));
            tbPanel_Drill_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 42.59819F));
            tbPanel_Drill_disconnect.Size = new Size(100, 90);
            tbPanel_Drill_disconnect.TabIndex = 30;
            // 
            // lab_disD
            // 
            lab_disD.AutoSize = true;
            lab_disD.BackColor = SystemColors.ButtonHighlight;
            lab_disD.Dock = DockStyle.Fill;
            lab_disD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_disD.Location = new Point(3, 50);
            lab_disD.Name = "lab_disD";
            lab_disD.Size = new Size(92, 38);
            lab_disD.TabIndex = 23;
            lab_disD.Text = "元件異常";
            lab_disD.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_disconnect
            // 
            lab_disconnect.AutoSize = true;
            lab_disconnect.BackColor = SystemColors.ButtonHighlight;
            lab_disconnect.Dock = DockStyle.Fill;
            lab_disconnect.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_disconnect.Location = new Point(3, 0);
            lab_disconnect.Name = "lab_disconnect";
            lab_disconnect.Size = new Size(92, 50);
            lab_disconnect.TabIndex = 21;
            lab_disconnect.TextAlign = ContentAlignment.MiddleCenter;
            lab_disconnect.Click += lab_disconnect_Click;
            // 
            // tbPanel_Drill_green
            // 
            tbPanel_Drill_green.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Drill_green.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_green.ColumnCount = 1;
            tbPanel_Drill_green.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Drill_green.Controls.Add(lab_gD, 0, 1);
            tbPanel_Drill_green.Controls.Add(lab_green, 0, 0);
            tbPanel_Drill_green.Location = new Point(21, 121);
            tbPanel_Drill_green.Name = "tbPanel_Drill_green";
            tbPanel_Drill_green.RowCount = 2;
            tbPanel_Drill_green.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tbPanel_Drill_green.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tbPanel_Drill_green.Size = new Size(100, 90);
            tbPanel_Drill_green.TabIndex = 31;
            // 
            // lab_gD
            // 
            lab_gD.AutoSize = true;
            lab_gD.BackColor = SystemColors.ButtonHighlight;
            lab_gD.Dock = DockStyle.Fill;
            lab_gD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_gD.Location = new Point(3, 48);
            lab_gD.Name = "lab_gD";
            lab_gD.Size = new Size(92, 40);
            lab_gD.TabIndex = 23;
            lab_gD.Text = "綠燈總數";
            lab_gD.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_green
            // 
            lab_green.AutoSize = true;
            lab_green.BackColor = SystemColors.ButtonHighlight;
            lab_green.Dock = DockStyle.Fill;
            lab_green.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_green.Location = new Point(3, 0);
            lab_green.Name = "lab_green";
            lab_green.Size = new Size(92, 48);
            lab_green.TabIndex = 21;
            lab_green.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Drill_yellow
            // 
            tbPanel_Drill_yellow.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Drill_yellow.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_yellow.ColumnCount = 1;
            tbPanel_Drill_yellow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Drill_yellow.Controls.Add(lab_yD, 0, 1);
            tbPanel_Drill_yellow.Controls.Add(lab_yellow, 0, 0);
            tbPanel_Drill_yellow.Location = new Point(137, 122);
            tbPanel_Drill_yellow.Name = "tbPanel_Drill_yellow";
            tbPanel_Drill_yellow.RowCount = 2;
            tbPanel_Drill_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tbPanel_Drill_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tbPanel_Drill_yellow.Size = new Size(100, 90);
            tbPanel_Drill_yellow.TabIndex = 32;
            // 
            // lab_yD
            // 
            lab_yD.AutoSize = true;
            lab_yD.BackColor = SystemColors.ButtonHighlight;
            lab_yD.Dock = DockStyle.Fill;
            lab_yD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yD.Location = new Point(3, 48);
            lab_yD.Name = "lab_yD";
            lab_yD.Size = new Size(92, 40);
            lab_yD.TabIndex = 23;
            lab_yD.Text = "黃燈總數";
            lab_yD.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_yellow
            // 
            lab_yellow.AutoSize = true;
            lab_yellow.BackColor = SystemColors.ButtonHighlight;
            lab_yellow.Dock = DockStyle.Fill;
            lab_yellow.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yellow.Location = new Point(3, 0);
            lab_yellow.Name = "lab_yellow";
            lab_yellow.Size = new Size(92, 48);
            lab_yellow.TabIndex = 21;
            lab_yellow.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btn_Drill_lifesetting);
            panel1.Controls.Add(lab_Sawing_lifesetting);
            panel1.Location = new Point(17, 230);
            panel1.Name = "panel1";
            panel1.Size = new Size(220, 56);
            panel1.TabIndex = 41;
            // 
            // btn_Drill_lifesetting
            // 
            btn_Drill_lifesetting.BackColor = SystemColors.ButtonHighlight;
            btn_Drill_lifesetting.FlatStyle = FlatStyle.Flat;
            btn_Drill_lifesetting.Location = new Point(119, 14);
            btn_Drill_lifesetting.Margin = new Padding(4);
            btn_Drill_lifesetting.Name = "btn_Drill_lifesetting";
            btn_Drill_lifesetting.Size = new Size(95, 26);
            btn_Drill_lifesetting.TabIndex = 54;
            btn_Drill_lifesetting.Text = "設定";
            btn_Drill_lifesetting.UseVisualStyleBackColor = false;
            btn_Drill_lifesetting.Click += btn_Drill_lifesetting_Click;
            // 
            // lab_Sawing_lifesetting
            // 
            lab_Sawing_lifesetting.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_Sawing_lifesetting.AutoSize = true;
            lab_Sawing_lifesetting.BackColor = SystemColors.ButtonHighlight;
            lab_Sawing_lifesetting.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Sawing_lifesetting.Location = new Point(17, 18);
            lab_Sawing_lifesetting.Name = "lab_Sawing_lifesetting";
            lab_Sawing_lifesetting.Size = new Size(99, 19);
            lab_Sawing_lifesetting.TabIndex = 43;
            lab_Sawing_lifesetting.Text = "元件壽命設定";
            lab_Sawing_lifesetting.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonHighlight;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(btn_Drill_Info);
            panel2.Controls.Add(btn_machInfo);
            panel2.Location = new Point(253, 230);
            panel2.Name = "panel2";
            panel2.Size = new Size(103, 56);
            panel2.TabIndex = 42;
            // 
            // btn_Drill_Info
            // 
            btn_Drill_Info.BackColor = SystemColors.ButtonHighlight;
            btn_Drill_Info.FlatStyle = FlatStyle.Flat;
            btn_Drill_Info.Location = new Point(5, 14);
            btn_Drill_Info.Margin = new Padding(4);
            btn_Drill_Info.Name = "btn_Drill_Info";
            btn_Drill_Info.Size = new Size(90, 26);
            btn_Drill_Info.TabIndex = 53;
            btn_Drill_Info.Text = "機台資訊";
            btn_Drill_Info.UseVisualStyleBackColor = false;
            btn_Drill_Info.Click += btn_Drill_Info_Click;
            // 
            // btn_machInfo
            // 
            btn_machInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_machInfo.BackColor = SystemColors.ButtonHighlight;
            btn_machInfo.FlatStyle = FlatStyle.Flat;
            btn_machInfo.Location = new Point(16, 10);
            btn_machInfo.Margin = new Padding(4);
            btn_machInfo.Name = "btn_machInfo";
            btn_machInfo.Size = new Size(0, 11);
            btn_machInfo.TabIndex = 39;
            btn_machInfo.Text = "鑽床資料";
            btn_machInfo.UseVisualStyleBackColor = false;
            // 
            // panel_SwingTime
            // 
            panel_SwingTime.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_SwingTime.BackColor = SystemColors.ButtonHighlight;
            panel_SwingTime.BorderStyle = BorderStyle.FixedSingle;
            panel_SwingTime.Controls.Add(tbPanel_Swing_totaltime);
            panel_SwingTime.Controls.Add(tbPanel_countdowntools);
            panel_SwingTime.Controls.Add(tbPanel_countdown);
            panel_SwingTime.Controls.Add(tbPanel_Electricity);
            panel_SwingTime.Controls.Add(tbPanel_powerconsumption);
            panel_SwingTime.Controls.Add(tbPanel_oilpressure);
            panel_SwingTime.Controls.Add(tbPanel_Ammeter);
            panel_SwingTime.Controls.Add(tbPanel_Voltage);
            panel_SwingTime.Controls.Add(tbPanel_cuting_speed);
            panel_SwingTime.Controls.Add(tbPanel_motor_current);
            panel_SwingTime.Location = new Point(247, 319);
            panel_SwingTime.Name = "panel_SwingTime";
            panel_SwingTime.Size = new Size(308, 296);
            panel_SwingTime.TabIndex = 52;
            // 
            // tbPanel_Swing_totaltime
            // 
            tbPanel_Swing_totaltime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbPanel_Swing_totaltime.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Swing_totaltime.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_totaltime.ColumnCount = 1;
            tbPanel_Swing_totaltime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_totaltime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_totaltime.Controls.Add(lb_totaltime, 0, 1);
            tbPanel_Swing_totaltime.Controls.Add(lb_totaltimeText, 0, 0);
            tbPanel_Swing_totaltime.Location = new Point(3, 247);
            tbPanel_Swing_totaltime.Name = "tbPanel_Swing_totaltime";
            tbPanel_Swing_totaltime.RowCount = 2;
            tbPanel_Swing_totaltime.RowStyles.Add(new RowStyle(SizeType.Percent, 48.1481476F));
            tbPanel_Swing_totaltime.RowStyles.Add(new RowStyle(SizeType.Percent, 51.8518524F));
            tbPanel_Swing_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tbPanel_Swing_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_totaltime.Size = new Size(299, 43);
            tbPanel_Swing_totaltime.TabIndex = 72;
            // 
            // lb_totaltime
            // 
            lb_totaltime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lb_totaltime.AutoSize = true;
            lb_totaltime.BackColor = SystemColors.ButtonHighlight;
            lb_totaltime.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_totaltime.Location = new Point(3, 19);
            lb_totaltime.Name = "lb_totaltime";
            lb_totaltime.Size = new Size(291, 22);
            lb_totaltime.TabIndex = 47;
            lb_totaltime.TextAlign = ContentAlignment.MiddleCenter;
            lb_totaltime.Click += lb_totaltime_Click;
            // 
            // lb_totaltimeText
            // 
            lb_totaltimeText.AutoSize = true;
            lb_totaltimeText.BackColor = SystemColors.ButtonHighlight;
            lb_totaltimeText.Dock = DockStyle.Fill;
            lb_totaltimeText.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_totaltimeText.Location = new Point(3, 0);
            lb_totaltimeText.Name = "lb_totaltimeText";
            lb_totaltimeText.Size = new Size(291, 19);
            lb_totaltimeText.TabIndex = 46;
            lb_totaltimeText.Text = "總運轉時間";
            lb_totaltimeText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_countdowntools
            // 
            tbPanel_countdowntools.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbPanel_countdowntools.BackColor = SystemColors.ButtonHighlight;
            tbPanel_countdowntools.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_countdowntools.ColumnCount = 1;
            tbPanel_countdowntools.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65.16393F));
            tbPanel_countdowntools.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.8360672F));
            tbPanel_countdowntools.Controls.Add(lb_remain_tools, 0, 1);
            tbPanel_countdowntools.Controls.Add(lb_remain_toolsText, 0, 0);
            tbPanel_countdowntools.Location = new Point(4, 156);
            tbPanel_countdowntools.Name = "tbPanel_countdowntools";
            tbPanel_countdowntools.RowCount = 2;
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Percent, 48.8372078F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Percent, 51.1627922F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdowntools.Size = new Size(148, 45);
            tbPanel_countdowntools.TabIndex = 71;
            // 
            // lb_remain_tools
            // 
            lb_remain_tools.AutoSize = true;
            lb_remain_tools.BackColor = SystemColors.ButtonHighlight;
            lb_remain_tools.Dock = DockStyle.Fill;
            lb_remain_tools.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_remain_tools.Location = new Point(3, 21);
            lb_remain_tools.Name = "lb_remain_tools";
            lb_remain_tools.Size = new Size(140, 22);
            lb_remain_tools.TabIndex = 49;
            lb_remain_tools.Text = "0 刀";
            lb_remain_tools.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_remain_toolsText
            // 
            lb_remain_toolsText.AutoSize = true;
            lb_remain_toolsText.BackColor = SystemColors.ButtonHighlight;
            lb_remain_toolsText.Dock = DockStyle.Fill;
            lb_remain_toolsText.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_remain_toolsText.Location = new Point(3, 0);
            lb_remain_toolsText.Name = "lb_remain_toolsText";
            lb_remain_toolsText.Size = new Size(140, 21);
            lb_remain_toolsText.TabIndex = 47;
            lb_remain_toolsText.Text = "剩餘加工刀數";
            lb_remain_toolsText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_countdown
            // 
            tbPanel_countdown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbPanel_countdown.BackColor = SystemColors.ButtonHighlight;
            tbPanel_countdown.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_countdown.ColumnCount = 1;
            tbPanel_countdown.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_countdown.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_countdown.Controls.Add(lb_countdown_time, 0, 1);
            tbPanel_countdown.Controls.Add(lb_countdown_timeText, 0, 0);
            tbPanel_countdown.Location = new Point(3, 203);
            tbPanel_countdown.Name = "tbPanel_countdown";
            tbPanel_countdown.RowCount = 2;
            tbPanel_countdown.RowStyles.Add(new RowStyle(SizeType.Percent, 48.1481476F));
            tbPanel_countdown.RowStyles.Add(new RowStyle(SizeType.Percent, 51.8518524F));
            tbPanel_countdown.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tbPanel_countdown.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdown.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdown.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdown.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdown.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdown.Size = new Size(299, 42);
            tbPanel_countdown.TabIndex = 70;
            // 
            // lb_countdown_time
            // 
            lb_countdown_time.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lb_countdown_time.AutoSize = true;
            lb_countdown_time.BackColor = SystemColors.ButtonHighlight;
            lb_countdown_time.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_countdown_time.Location = new Point(3, 19);
            lb_countdown_time.Name = "lb_countdown_time";
            lb_countdown_time.Size = new Size(291, 21);
            lb_countdown_time.TabIndex = 47;
            lb_countdown_time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_countdown_timeText
            // 
            lb_countdown_timeText.AutoSize = true;
            lb_countdown_timeText.BackColor = SystemColors.ButtonHighlight;
            lb_countdown_timeText.Dock = DockStyle.Fill;
            lb_countdown_timeText.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_countdown_timeText.Location = new Point(3, 0);
            lb_countdown_timeText.Name = "lb_countdown_timeText";
            lb_countdown_timeText.Size = new Size(291, 19);
            lb_countdown_timeText.TabIndex = 46;
            lb_countdown_timeText.Text = "鋸切倒數計時";
            lb_countdown_timeText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Electricity
            // 
            tbPanel_Electricity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbPanel_Electricity.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Electricity.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Electricity.ColumnCount = 1;
            tbPanel_Electricity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Electricity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Electricity.Controls.Add(lb_electricity, 0, 1);
            tbPanel_Electricity.Controls.Add(lab_reset1, 0, 0);
            tbPanel_Electricity.Location = new Point(154, 156);
            tbPanel_Electricity.Name = "tbPanel_Electricity";
            tbPanel_Electricity.RowCount = 2;
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Percent, 43.5897446F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Percent, 56.4102554F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.Size = new Size(148, 45);
            tbPanel_Electricity.TabIndex = 68;
            // 
            // lb_electricity
            // 
            lb_electricity.AutoSize = true;
            lb_electricity.BackColor = SystemColors.ButtonHighlight;
            lb_electricity.Dock = DockStyle.Fill;
            lb_electricity.Font = new Font("Microsoft JhengHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_electricity.Location = new Point(3, 18);
            lb_electricity.Name = "lb_electricity";
            lb_electricity.Size = new Size(140, 25);
            lb_electricity.TabIndex = 27;
            lb_electricity.Text = "0 千瓦小時";
            lb_electricity.TextAlign = ContentAlignment.MiddleCenter;
            lb_electricity.Click += lb_electricity_Click;
            // 
            // lab_reset1
            // 
            lab_reset1.AutoSize = true;
            lab_reset1.BackColor = SystemColors.ButtonHighlight;
            lab_reset1.Dock = DockStyle.Fill;
            lab_reset1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_reset1.Location = new Point(3, 0);
            lab_reset1.Name = "lab_reset1";
            lab_reset1.Size = new Size(140, 18);
            lab_reset1.TabIndex = 24;
            lab_reset1.Text = "累積用電度數";
            lab_reset1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_powerconsumption
            // 
            tbPanel_powerconsumption.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbPanel_powerconsumption.BackColor = SystemColors.ButtonHighlight;
            tbPanel_powerconsumption.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_powerconsumption.ColumnCount = 1;
            tbPanel_powerconsumption.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_powerconsumption.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_powerconsumption.Controls.Add(lb_swingpower, 0, 1);
            tbPanel_powerconsumption.Controls.Add(lab_power1, 0, 0);
            tbPanel_powerconsumption.Location = new Point(154, 107);
            tbPanel_powerconsumption.Name = "tbPanel_powerconsumption";
            tbPanel_powerconsumption.RowCount = 2;
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Percent, 44.4444427F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Percent, 55.5555573F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.Size = new Size(148, 45);
            tbPanel_powerconsumption.TabIndex = 67;
            // 
            // lb_swingpower
            // 
            lb_swingpower.AutoSize = true;
            lb_swingpower.BackColor = SystemColors.ButtonHighlight;
            lb_swingpower.Dock = DockStyle.Fill;
            lb_swingpower.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swingpower.Location = new Point(3, 19);
            lb_swingpower.Name = "lb_swingpower";
            lb_swingpower.Size = new Size(140, 24);
            lb_swingpower.TabIndex = 29;
            lb_swingpower.Text = "0 千瓦";
            lb_swingpower.TextAlign = ContentAlignment.MiddleCenter;
            lb_swingpower.Click += lb_swingpower_Click;
            // 
            // lab_power1
            // 
            lab_power1.AutoSize = true;
            lab_power1.BackColor = SystemColors.ButtonHighlight;
            lab_power1.Dock = DockStyle.Fill;
            lab_power1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_power1.Location = new Point(3, 0);
            lab_power1.Name = "lab_power1";
            lab_power1.Size = new Size(140, 19);
            lab_power1.TabIndex = 24;
            lab_power1.Text = "消耗功率";
            lab_power1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_oilpressure
            // 
            tbPanel_oilpressure.AccessibleRole = AccessibleRole.None;
            tbPanel_oilpressure.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbPanel_oilpressure.BackColor = SystemColors.ButtonHighlight;
            tbPanel_oilpressure.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_oilpressure.ColumnCount = 1;
            tbPanel_oilpressure.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_oilpressure.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_oilpressure.Controls.Add(lb_oilpress, 0, 1);
            tbPanel_oilpressure.Controls.Add(lb_oilpressText, 0, 0);
            tbPanel_oilpressure.Location = new Point(4, 107);
            tbPanel_oilpressure.Name = "tbPanel_oilpressure";
            tbPanel_oilpressure.RowCount = 2;
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Percent, 47.22222F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Percent, 52.77778F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.Size = new Size(148, 45);
            tbPanel_oilpressure.TabIndex = 66;
            // 
            // lb_oilpress
            // 
            lb_oilpress.AutoSize = true;
            lb_oilpress.BackColor = SystemColors.ButtonHighlight;
            lb_oilpress.Dock = DockStyle.Fill;
            lb_oilpress.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_oilpress.Location = new Point(3, 20);
            lb_oilpress.Name = "lb_oilpress";
            lb_oilpress.Size = new Size(140, 23);
            lb_oilpress.TabIndex = 28;
            lb_oilpress.Text = "未連線";
            lb_oilpress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_oilpressText
            // 
            lb_oilpressText.AutoSize = true;
            lb_oilpressText.BackColor = SystemColors.ButtonHighlight;
            lb_oilpressText.Dock = DockStyle.Fill;
            lb_oilpressText.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_oilpressText.Location = new Point(3, 0);
            lb_oilpressText.Name = "lb_oilpressText";
            lb_oilpressText.Size = new Size(140, 20);
            lb_oilpressText.TabIndex = 24;
            lb_oilpressText.Text = "油壓張力狀態";
            lb_oilpressText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Ammeter
            // 
            tbPanel_Ammeter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbPanel_Ammeter.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Ammeter.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Ammeter.ColumnCount = 1;
            tbPanel_Ammeter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Ammeter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Ammeter.Controls.Add(lb_swing_current, 0, 1);
            tbPanel_Ammeter.Controls.Add(label_Ammeter1, 0, 0);
            tbPanel_Ammeter.Location = new Point(154, 57);
            tbPanel_Ammeter.Name = "tbPanel_Ammeter";
            tbPanel_Ammeter.RowCount = 2;
            tbPanel_Ammeter.RowStyles.Add(new RowStyle(SizeType.Percent, 53.3333321F));
            tbPanel_Ammeter.RowStyles.Add(new RowStyle(SizeType.Percent, 46.6666679F));
            tbPanel_Ammeter.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Ammeter.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Ammeter.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Ammeter.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Ammeter.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Ammeter.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Ammeter.Size = new Size(148, 45);
            tbPanel_Ammeter.TabIndex = 65;
            // 
            // lb_swing_current
            // 
            lb_swing_current.AutoSize = true;
            lb_swing_current.BackColor = SystemColors.ButtonHighlight;
            lb_swing_current.Dock = DockStyle.Fill;
            lb_swing_current.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swing_current.Location = new Point(3, 22);
            lb_swing_current.Name = "lb_swing_current";
            lb_swing_current.Size = new Size(140, 21);
            lb_swing_current.TabIndex = 25;
            lb_swing_current.Text = "0 安培";
            lb_swing_current.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_Ammeter1
            // 
            label_Ammeter1.AutoSize = true;
            label_Ammeter1.BackColor = SystemColors.ButtonHighlight;
            label_Ammeter1.Dock = DockStyle.Fill;
            label_Ammeter1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_Ammeter1.Location = new Point(3, 0);
            label_Ammeter1.Name = "label_Ammeter1";
            label_Ammeter1.Size = new Size(140, 22);
            label_Ammeter1.TabIndex = 24;
            label_Ammeter1.Text = "平均電流值";
            label_Ammeter1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Voltage
            // 
            tbPanel_Voltage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbPanel_Voltage.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Voltage.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Voltage.ColumnCount = 1;
            tbPanel_Voltage.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Voltage.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Voltage.Controls.Add(lb_swing_Voltage, 0, 1);
            tbPanel_Voltage.Controls.Add(lb_Voltage1, 0, 0);
            tbPanel_Voltage.Location = new Point(154, 7);
            tbPanel_Voltage.Name = "tbPanel_Voltage";
            tbPanel_Voltage.RowCount = 2;
            tbPanel_Voltage.RowStyles.Add(new RowStyle(SizeType.Percent, 53.3333321F));
            tbPanel_Voltage.RowStyles.Add(new RowStyle(SizeType.Percent, 46.6666679F));
            tbPanel_Voltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Voltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Voltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Voltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Voltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Voltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Voltage.Size = new Size(148, 45);
            tbPanel_Voltage.TabIndex = 64;
            // 
            // lb_swing_Voltage
            // 
            lb_swing_Voltage.AutoSize = true;
            lb_swing_Voltage.BackColor = SystemColors.ButtonHighlight;
            lb_swing_Voltage.Dock = DockStyle.Fill;
            lb_swing_Voltage.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swing_Voltage.Location = new Point(3, 22);
            lb_swing_Voltage.Name = "lb_swing_Voltage";
            lb_swing_Voltage.Size = new Size(140, 21);
            lb_swing_Voltage.TabIndex = 26;
            lb_swing_Voltage.Text = "0 (V)";
            lb_swing_Voltage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_Voltage1
            // 
            lb_Voltage1.AutoSize = true;
            lb_Voltage1.BackColor = SystemColors.ButtonHighlight;
            lb_Voltage1.Dock = DockStyle.Fill;
            lb_Voltage1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_Voltage1.Location = new Point(3, 0);
            lb_Voltage1.Name = "lb_Voltage1";
            lb_Voltage1.Size = new Size(140, 22);
            lb_Voltage1.TabIndex = 24;
            lb_Voltage1.Text = "平均電壓值";
            lb_Voltage1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_cuting_speed
            // 
            tbPanel_cuting_speed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbPanel_cuting_speed.BackColor = SystemColors.ButtonHighlight;
            tbPanel_cuting_speed.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_cuting_speed.ColumnCount = 1;
            tbPanel_cuting_speed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_cuting_speed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_cuting_speed.Controls.Add(lb_sawing_cutingspeed, 0, 1);
            tbPanel_cuting_speed.Controls.Add(lb_sawing_cutingspeedText, 0, 0);
            tbPanel_cuting_speed.Location = new Point(4, 57);
            tbPanel_cuting_speed.Name = "tbPanel_cuting_speed";
            tbPanel_cuting_speed.RowCount = 2;
            tbPanel_cuting_speed.RowStyles.Add(new RowStyle(SizeType.Percent, 53.3333321F));
            tbPanel_cuting_speed.RowStyles.Add(new RowStyle(SizeType.Percent, 46.6666679F));
            tbPanel_cuting_speed.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_cuting_speed.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_cuting_speed.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_cuting_speed.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_cuting_speed.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_cuting_speed.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_cuting_speed.Size = new Size(148, 45);
            tbPanel_cuting_speed.TabIndex = 63;
            // 
            // lb_sawing_cutingspeed
            // 
            lb_sawing_cutingspeed.AutoSize = true;
            lb_sawing_cutingspeed.BackColor = SystemColors.ButtonHighlight;
            lb_sawing_cutingspeed.Dock = DockStyle.Fill;
            lb_sawing_cutingspeed.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_sawing_cutingspeed.Location = new Point(3, 22);
            lb_sawing_cutingspeed.Name = "lb_sawing_cutingspeed";
            lb_sawing_cutingspeed.Size = new Size(140, 21);
            lb_sawing_cutingspeed.TabIndex = 27;
            lb_sawing_cutingspeed.Text = "0  (m/min)";
            lb_sawing_cutingspeed.TextAlign = ContentAlignment.MiddleCenter;
            lb_sawing_cutingspeed.Click += lb_sawing_cutingspeed_Click;
            // 
            // lb_sawing_cutingspeedText
            // 
            lb_sawing_cutingspeedText.AutoSize = true;
            lb_sawing_cutingspeedText.BackColor = SystemColors.ButtonHighlight;
            lb_sawing_cutingspeedText.Dock = DockStyle.Fill;
            lb_sawing_cutingspeedText.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_sawing_cutingspeedText.Location = new Point(3, 0);
            lb_sawing_cutingspeedText.Name = "lb_sawing_cutingspeedText";
            lb_sawing_cutingspeedText.Size = new Size(140, 22);
            lb_sawing_cutingspeedText.TabIndex = 24;
            lb_sawing_cutingspeedText.Text = "切削速度";
            lb_sawing_cutingspeedText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_motor_current
            // 
            tbPanel_motor_current.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tbPanel_motor_current.BackColor = SystemColors.ButtonHighlight;
            tbPanel_motor_current.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_motor_current.ColumnCount = 1;
            tbPanel_motor_current.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_motor_current.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_motor_current.Controls.Add(lb_swing_motor_current, 0, 1);
            tbPanel_motor_current.Controls.Add(lb_swing_motor_currentText, 0, 0);
            tbPanel_motor_current.Location = new Point(4, 7);
            tbPanel_motor_current.Name = "tbPanel_motor_current";
            tbPanel_motor_current.RowCount = 2;
            tbPanel_motor_current.RowStyles.Add(new RowStyle(SizeType.Percent, 53.3333321F));
            tbPanel_motor_current.RowStyles.Add(new RowStyle(SizeType.Percent, 46.6666679F));
            tbPanel_motor_current.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_motor_current.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_motor_current.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_motor_current.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_motor_current.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_motor_current.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_motor_current.Size = new Size(148, 45);
            tbPanel_motor_current.TabIndex = 62;
            // 
            // lb_swing_motor_current
            // 
            lb_swing_motor_current.AutoSize = true;
            lb_swing_motor_current.BackColor = SystemColors.ButtonHighlight;
            lb_swing_motor_current.Dock = DockStyle.Fill;
            lb_swing_motor_current.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swing_motor_current.Location = new Point(3, 22);
            lb_swing_motor_current.Name = "lb_swing_motor_current";
            lb_swing_motor_current.Size = new Size(140, 21);
            lb_swing_motor_current.TabIndex = 27;
            lb_swing_motor_current.Text = "0  安培";
            lb_swing_motor_current.TextAlign = ContentAlignment.MiddleCenter;
            lb_swing_motor_current.Click += lb_swing_motor_current_Click;
            // 
            // lb_swing_motor_currentText
            // 
            lb_swing_motor_currentText.AutoSize = true;
            lb_swing_motor_currentText.BackColor = SystemColors.ButtonHighlight;
            lb_swing_motor_currentText.Dock = DockStyle.Fill;
            lb_swing_motor_currentText.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swing_motor_currentText.Location = new Point(3, 0);
            lb_swing_motor_currentText.Name = "lb_swing_motor_currentText";
            lb_swing_motor_currentText.Size = new Size(140, 22);
            lb_swing_motor_currentText.TabIndex = 24;
            lb_swing_motor_currentText.Text = "馬達電流值";
            lb_swing_motor_currentText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel_Swing
            // 
            panel_Swing.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_Swing.BackColor = SystemColors.ButtonHighlight;
            panel_Swing.BackgroundImage = Properties.Resources.Swaing;
            panel_Swing.BackgroundImageLayout = ImageLayout.Center;
            panel_Swing.BorderStyle = BorderStyle.FixedSingle;
            panel_Swing.Location = new Point(4, 318);
            panel_Swing.Name = "panel_Swing";
            panel_Swing.Size = new Size(240, 297);
            panel_Swing.TabIndex = 51;
            // 
            // panel_DrillTime
            // 
            panel_DrillTime.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_DrillTime.BackColor = SystemColors.ButtonHighlight;
            panel_DrillTime.BorderStyle = BorderStyle.FixedSingle;
            panel_DrillTime.Controls.Add(tbPanel_drillpower);
            panel_DrillTime.Controls.Add(tbPanel_drilldu);
            panel_DrillTime.Controls.Add(tbPanel_drillcurrent);
            panel_DrillTime.Controls.Add(tbPanel_Drill_totaltime);
            panel_DrillTime.Controls.Add(tbPanel_drillvoltage);
            panel_DrillTime.Controls.Add(tbPanel_cutingtime);
            panel_DrillTime.Location = new Point(247, 10);
            panel_DrillTime.Name = "panel_DrillTime";
            panel_DrillTime.Size = new Size(313, 297);
            panel_DrillTime.TabIndex = 50;
            // 
            // tbPanel_drillpower
            // 
            tbPanel_drillpower.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_drillpower.BackColor = SystemColors.ButtonHighlight;
            tbPanel_drillpower.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_drillpower.ColumnCount = 1;
            tbPanel_drillpower.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillpower.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillpower.Controls.Add(lb_drillpower, 0, 1);
            tbPanel_drillpower.Controls.Add(lab_power, 0, 0);
            tbPanel_drillpower.Location = new Point(7, 94);
            tbPanel_drillpower.Name = "tbPanel_drillpower";
            tbPanel_drillpower.RowCount = 2;
            tbPanel_drillpower.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tbPanel_drillpower.RowStyles.Add(new RowStyle(SizeType.Percent, 66.6666641F));
            tbPanel_drillpower.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillpower.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillpower.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillpower.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillpower.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillpower.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillpower.Size = new Size(143, 75);
            tbPanel_drillpower.TabIndex = 72;
            // 
            // lb_drillpower
            // 
            lb_drillpower.AutoSize = true;
            lb_drillpower.BackColor = SystemColors.ButtonHighlight;
            lb_drillpower.Dock = DockStyle.Fill;
            lb_drillpower.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            lb_drillpower.Location = new Point(3, 24);
            lb_drillpower.Name = "lb_drillpower";
            lb_drillpower.Size = new Size(135, 49);
            lb_drillpower.TabIndex = 27;
            lb_drillpower.Text = "0\r\n千瓦";
            lb_drillpower.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_power
            // 
            lab_power.AutoSize = true;
            lab_power.BackColor = SystemColors.ButtonHighlight;
            lab_power.Dock = DockStyle.Fill;
            lab_power.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_power.Location = new Point(3, 0);
            lab_power.Name = "lab_power";
            lab_power.Size = new Size(135, 24);
            lab_power.TabIndex = 24;
            lab_power.Text = "當前消耗功率";
            lab_power.TextAlign = ContentAlignment.MiddleCenter;
            lab_power.Click += lab_power_Click;
            // 
            // tbPanel_drilldu
            // 
            tbPanel_drilldu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_drilldu.BackColor = SystemColors.ButtonHighlight;
            tbPanel_drilldu.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_drilldu.ColumnCount = 1;
            tbPanel_drilldu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drilldu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drilldu.Controls.Add(lb_drill_du, 0, 1);
            tbPanel_drilldu.Controls.Add(lab_reset, 0, 0);
            tbPanel_drilldu.Location = new Point(7, 11);
            tbPanel_drilldu.Name = "tbPanel_drilldu";
            tbPanel_drilldu.RowCount = 2;
            tbPanel_drilldu.RowStyles.Add(new RowStyle(SizeType.Percent, 29.87013F));
            tbPanel_drilldu.RowStyles.Add(new RowStyle(SizeType.Percent, 70.12987F));
            tbPanel_drilldu.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drilldu.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drilldu.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drilldu.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drilldu.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drilldu.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drilldu.Size = new Size(143, 75);
            tbPanel_drilldu.TabIndex = 71;
            // 
            // lb_drill_du
            // 
            lb_drill_du.AutoSize = true;
            lb_drill_du.BackColor = SystemColors.ButtonHighlight;
            lb_drill_du.Dock = DockStyle.Fill;
            lb_drill_du.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            lb_drill_du.Location = new Point(3, 21);
            lb_drill_du.Name = "lb_drill_du";
            lb_drill_du.Size = new Size(135, 52);
            lb_drill_du.TabIndex = 27;
            lb_drill_du.Text = "0\r\n千瓦/小時";
            lb_drill_du.TextAlign = ContentAlignment.MiddleCenter;
            lb_drill_du.Click += lb_drill_du_Click;
            // 
            // lab_reset
            // 
            lab_reset.AutoSize = true;
            lab_reset.BackColor = SystemColors.ButtonHighlight;
            lab_reset.Dock = DockStyle.Fill;
            lab_reset.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_reset.Location = new Point(3, 0);
            lab_reset.Name = "lab_reset";
            lab_reset.Size = new Size(135, 21);
            lab_reset.TabIndex = 24;
            lab_reset.Text = "累積用電度數";
            lab_reset.TextAlign = ContentAlignment.MiddleCenter;
            lab_reset.Click += lab_reset_Click;
            // 
            // tbPanel_drillcurrent
            // 
            tbPanel_drillcurrent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_drillcurrent.BackColor = SystemColors.ButtonHighlight;
            tbPanel_drillcurrent.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_drillcurrent.ColumnCount = 1;
            tbPanel_drillcurrent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillcurrent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillcurrent.Controls.Add(lb_drill_current, 0, 1);
            tbPanel_drillcurrent.Controls.Add(label_Ammeter, 0, 0);
            tbPanel_drillcurrent.Location = new Point(159, 95);
            tbPanel_drillcurrent.Name = "tbPanel_drillcurrent";
            tbPanel_drillcurrent.RowCount = 2;
            tbPanel_drillcurrent.RowStyles.Add(new RowStyle(SizeType.Percent, 32F));
            tbPanel_drillcurrent.RowStyles.Add(new RowStyle(SizeType.Percent, 68F));
            tbPanel_drillcurrent.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillcurrent.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillcurrent.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillcurrent.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillcurrent.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillcurrent.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillcurrent.Size = new Size(147, 75);
            tbPanel_drillcurrent.TabIndex = 67;
            // 
            // lb_drill_current
            // 
            lb_drill_current.AutoSize = true;
            lb_drill_current.BackColor = SystemColors.ButtonHighlight;
            lb_drill_current.Dock = DockStyle.Fill;
            lb_drill_current.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            lb_drill_current.Location = new Point(3, 23);
            lb_drill_current.Name = "lb_drill_current";
            lb_drill_current.Size = new Size(139, 50);
            lb_drill_current.TabIndex = 25;
            lb_drill_current.Text = "0\r\n";
            lb_drill_current.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_Ammeter
            // 
            label_Ammeter.AutoSize = true;
            label_Ammeter.BackColor = SystemColors.ButtonHighlight;
            label_Ammeter.Dock = DockStyle.Fill;
            label_Ammeter.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_Ammeter.Location = new Point(3, 0);
            label_Ammeter.Name = "label_Ammeter";
            label_Ammeter.Size = new Size(139, 23);
            label_Ammeter.TabIndex = 24;
            label_Ammeter.Text = "平均電流值";
            label_Ammeter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Drill_totaltime
            // 
            tbPanel_Drill_totaltime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_Drill_totaltime.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Drill_totaltime.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_totaltime.ColumnCount = 1;
            tbPanel_Drill_totaltime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_totaltime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_totaltime.Controls.Add(lb_Drill_totaltime, 0, 1);
            tbPanel_Drill_totaltime.Controls.Add(lb_Drill_totaltimeText, 0, 0);
            tbPanel_Drill_totaltime.Location = new Point(6, 235);
            tbPanel_Drill_totaltime.Name = "tbPanel_Drill_totaltime";
            tbPanel_Drill_totaltime.RowCount = 2;
            tbPanel_Drill_totaltime.RowStyles.Add(new RowStyle(SizeType.Percent, 48.1481476F));
            tbPanel_Drill_totaltime.RowStyles.Add(new RowStyle(SizeType.Percent, 51.8518524F));
            tbPanel_Drill_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tbPanel_Drill_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_totaltime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_totaltime.Size = new Size(300, 52);
            tbPanel_Drill_totaltime.TabIndex = 70;
            // 
            // lb_Drill_totaltime
            // 
            lb_Drill_totaltime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lb_Drill_totaltime.AutoSize = true;
            lb_Drill_totaltime.BackColor = SystemColors.ButtonHighlight;
            lb_Drill_totaltime.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_Drill_totaltime.Location = new Point(3, 24);
            lb_Drill_totaltime.Name = "lb_Drill_totaltime";
            lb_Drill_totaltime.Size = new Size(292, 26);
            lb_Drill_totaltime.TabIndex = 47;
            lb_Drill_totaltime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_Drill_totaltimeText
            // 
            lb_Drill_totaltimeText.AutoSize = true;
            lb_Drill_totaltimeText.BackColor = SystemColors.ButtonHighlight;
            lb_Drill_totaltimeText.Dock = DockStyle.Fill;
            lb_Drill_totaltimeText.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_Drill_totaltimeText.Location = new Point(3, 0);
            lb_Drill_totaltimeText.Name = "lb_Drill_totaltimeText";
            lb_Drill_totaltimeText.Size = new Size(292, 24);
            lb_Drill_totaltimeText.TabIndex = 46;
            lb_Drill_totaltimeText.Text = "總運轉時間";
            lb_Drill_totaltimeText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_drillvoltage
            // 
            tbPanel_drillvoltage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_drillvoltage.BackColor = SystemColors.ButtonHighlight;
            tbPanel_drillvoltage.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_drillvoltage.ColumnCount = 1;
            tbPanel_drillvoltage.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillvoltage.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillvoltage.Controls.Add(lb_drill_Voltage, 0, 1);
            tbPanel_drillvoltage.Controls.Add(lb_Voltage, 0, 0);
            tbPanel_drillvoltage.Location = new Point(159, 12);
            tbPanel_drillvoltage.Name = "tbPanel_drillvoltage";
            tbPanel_drillvoltage.RowCount = 2;
            tbPanel_drillvoltage.RowStyles.Add(new RowStyle(SizeType.Percent, 30.666666F));
            tbPanel_drillvoltage.RowStyles.Add(new RowStyle(SizeType.Percent, 69.3333359F));
            tbPanel_drillvoltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillvoltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillvoltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillvoltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillvoltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillvoltage.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_drillvoltage.Size = new Size(147, 75);
            tbPanel_drillvoltage.TabIndex = 66;
            // 
            // lb_drill_Voltage
            // 
            lb_drill_Voltage.AutoSize = true;
            lb_drill_Voltage.BackColor = SystemColors.ButtonHighlight;
            lb_drill_Voltage.Dock = DockStyle.Fill;
            lb_drill_Voltage.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            lb_drill_Voltage.Location = new Point(3, 22);
            lb_drill_Voltage.Name = "lb_drill_Voltage";
            lb_drill_Voltage.Size = new Size(139, 51);
            lb_drill_Voltage.TabIndex = 26;
            lb_drill_Voltage.Text = "0";
            lb_drill_Voltage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_Voltage
            // 
            lb_Voltage.AutoSize = true;
            lb_Voltage.BackColor = SystemColors.ButtonHighlight;
            lb_Voltage.Dock = DockStyle.Fill;
            lb_Voltage.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_Voltage.Location = new Point(3, 0);
            lb_Voltage.Name = "lb_Voltage";
            lb_Voltage.Size = new Size(139, 22);
            lb_Voltage.TabIndex = 24;
            lb_Voltage.Text = "平均電壓值";
            lb_Voltage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_cutingtime
            // 
            tbPanel_cutingtime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_cutingtime.BackColor = SystemColors.ButtonHighlight;
            tbPanel_cutingtime.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_cutingtime.ColumnCount = 1;
            tbPanel_cutingtime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_cutingtime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_cutingtime.Controls.Add(lb_cutingtime, 0, 1);
            tbPanel_cutingtime.Controls.Add(lb_cutingtimeText, 0, 0);
            tbPanel_cutingtime.Location = new Point(6, 177);
            tbPanel_cutingtime.Name = "tbPanel_cutingtime";
            tbPanel_cutingtime.RowCount = 2;
            tbPanel_cutingtime.RowStyles.Add(new RowStyle(SizeType.Percent, 48.1481476F));
            tbPanel_cutingtime.RowStyles.Add(new RowStyle(SizeType.Percent, 51.8518524F));
            tbPanel_cutingtime.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tbPanel_cutingtime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_cutingtime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_cutingtime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_cutingtime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_cutingtime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_cutingtime.Size = new Size(300, 52);
            tbPanel_cutingtime.TabIndex = 69;
            // 
            // lb_cutingtime
            // 
            lb_cutingtime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lb_cutingtime.AutoSize = true;
            lb_cutingtime.BackColor = SystemColors.ButtonHighlight;
            lb_cutingtime.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_cutingtime.Location = new Point(3, 24);
            lb_cutingtime.Name = "lb_cutingtime";
            lb_cutingtime.Size = new Size(292, 26);
            lb_cutingtime.TabIndex = 47;
            lb_cutingtime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_cutingtimeText
            // 
            lb_cutingtimeText.AutoSize = true;
            lb_cutingtimeText.BackColor = SystemColors.ButtonHighlight;
            lb_cutingtimeText.Dock = DockStyle.Fill;
            lb_cutingtimeText.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_cutingtimeText.Location = new Point(3, 0);
            lb_cutingtimeText.Name = "lb_cutingtimeText";
            lb_cutingtimeText.Size = new Size(292, 24);
            lb_cutingtimeText.TabIndex = 46;
            lb_cutingtimeText.Text = "實際加工時間";
            lb_cutingtimeText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel_Drill
            // 
            panel_Drill.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_Drill.BackColor = SystemColors.ButtonHighlight;
            panel_Drill.BackgroundImage = Properties.Resources.Drill;
            panel_Drill.BackgroundImageLayout = ImageLayout.Stretch;
            panel_Drill.BorderStyle = BorderStyle.FixedSingle;
            panel_Drill.Location = new Point(4, 10);
            panel_Drill.Name = "panel_Drill";
            panel_Drill.Size = new Size(240, 297);
            panel_Drill.TabIndex = 47;
            // 
            // lab_time
            // 
            lab_time.AutoSize = true;
            lab_time.BackColor = SystemColors.ButtonHighlight;
            lab_time.Location = new Point(826, 618);
            lab_time.Name = "lab_time";
            lab_time.Size = new Size(0, 15);
            lab_time.TabIndex = 53;
            // 
            // Main_form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(956, 625);
            Controls.Add(lab_time);
            Controls.Add(panel12);
            Controls.Add(panel11);
            Controls.Add(panel_SwingTime);
            Controls.Add(panel_Swing);
            Controls.Add(panel_DrillTime);
            Controls.Add(panel_Drill);
            Margin = new Padding(4);
            Name = "Main_form";
            Text = "視覺化元件監控";
            Load += Main_Load;
            TextChanged += Main_form_TextChanged;
            panel12.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            tbPanel_Swing_sum.ResumeLayout(false);
            tbPanel_Swing_sum.PerformLayout();
            tbPanel_Swing_connect.ResumeLayout(false);
            tbPanel_Swing_connect.PerformLayout();
            panel4.ResumeLayout(false);
            tbPanel_Swing_red.ResumeLayout(false);
            tbPanel_Swing_red.PerformLayout();
            tbPanel_Swing_disconnect.ResumeLayout(false);
            tbPanel_Swing_disconnect.PerformLayout();
            tbPanel_Swing_green.ResumeLayout(false);
            tbPanel_Swing_green.PerformLayout();
            tbPanel_Swing_yellow.ResumeLayout(false);
            tbPanel_Swing_yellow.PerformLayout();
            panel11.ResumeLayout(false);
            tbPanel_Drill_sum.ResumeLayout(false);
            tbPanel_Drill_sum.PerformLayout();
            tbPanel_Drill_connect.ResumeLayout(false);
            tbPanel_Drill_connect.PerformLayout();
            tbPanel_Drill_red.ResumeLayout(false);
            tbPanel_Drill_red.PerformLayout();
            tbPanel_Drill_disconnect.ResumeLayout(false);
            tbPanel_Drill_disconnect.PerformLayout();
            tbPanel_Drill_green.ResumeLayout(false);
            tbPanel_Drill_green.PerformLayout();
            tbPanel_Drill_yellow.ResumeLayout(false);
            tbPanel_Drill_yellow.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel_SwingTime.ResumeLayout(false);
            tbPanel_Swing_totaltime.ResumeLayout(false);
            tbPanel_Swing_totaltime.PerformLayout();
            tbPanel_countdowntools.ResumeLayout(false);
            tbPanel_countdowntools.PerformLayout();
            tbPanel_countdown.ResumeLayout(false);
            tbPanel_countdown.PerformLayout();
            tbPanel_Electricity.ResumeLayout(false);
            tbPanel_Electricity.PerformLayout();
            tbPanel_powerconsumption.ResumeLayout(false);
            tbPanel_powerconsumption.PerformLayout();
            tbPanel_oilpressure.ResumeLayout(false);
            tbPanel_oilpressure.PerformLayout();
            tbPanel_Ammeter.ResumeLayout(false);
            tbPanel_Ammeter.PerformLayout();
            tbPanel_Voltage.ResumeLayout(false);
            tbPanel_Voltage.PerformLayout();
            tbPanel_cuting_speed.ResumeLayout(false);
            tbPanel_cuting_speed.PerformLayout();
            tbPanel_motor_current.ResumeLayout(false);
            tbPanel_motor_current.PerformLayout();
            panel_DrillTime.ResumeLayout(false);
            tbPanel_drillpower.ResumeLayout(false);
            tbPanel_drillpower.PerformLayout();
            tbPanel_drilldu.ResumeLayout(false);
            tbPanel_drilldu.PerformLayout();
            tbPanel_drillcurrent.ResumeLayout(false);
            tbPanel_drillcurrent.PerformLayout();
            tbPanel_Drill_totaltime.ResumeLayout(false);
            tbPanel_Drill_totaltime.PerformLayout();
            tbPanel_drillvoltage.ResumeLayout(false);
            tbPanel_drillvoltage.PerformLayout();
            tbPanel_cutingtime.ResumeLayout(false);
            tbPanel_cutingtime.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_SawBand;
        private Panel panel12;
        private TableLayoutPanel tbPanel_Swing_sum;
        private Label lab_sumS;
        private Label lab_sum_swing;
        private TableLayoutPanel tbPanel_Swing_connect;
        private Label lab_connectS;
        private Label lab_connect_swing;
        private Panel panel4;
        private TableLayoutPanel tbPanel_Swing_red;
        private Label lab_rS;
        private Label lab_red_swing;
        private TableLayoutPanel tbPanel_Swing_disconnect;
        private Label lab_disS;
        private Label lab_disconnect_sawing;
        private TableLayoutPanel tbPanel_Swing_green;
        private Label lab_gS;
        private Label lab_green_swing;
        private TableLayoutPanel tbPanel_Swing_yellow;
        private Label lab_yS;
        private Label lab_yellow_swing;
        private Panel panel11;
        private TableLayoutPanel tbPanel_Drill_sum;
        private Label lab_sumD;
        private Label lab_sum;
        private TableLayoutPanel tbPanel_Drill_connect;
        private Label lab_connectD;
        private Label lab_connect;
        private TableLayoutPanel tbPanel_Drill_red;
        private Label lab_rD;
        private Label lab_red;
        private TableLayoutPanel tbPanel_Drill_disconnect;
        private Label lab_disD;
        private Label lab_disconnect;
        private TableLayoutPanel tbPanel_Drill_green;
        private Label lab_gD;
        private Label lab_green;
        private TableLayoutPanel tbPanel_Drill_yellow;
        private Label lab_yD;
        private Label lab_yellow;
        private Panel panel1;
        private Label lab_Sawing_lifesetting;
        private Panel panel2;
        private Button btn_machInfo;
        private Panel panel_SwingTime;
        private Panel panel_Swing;
        private Panel panel_DrillTime;
        private Panel panel_Drill;
        private Button btn_Drill_Info;
        private TableLayoutPanel tbPanel_motor_current;
        private TableLayoutPanel tbPanel_Ammeter;
        private Label label_Ammeter1;
        private TableLayoutPanel tbPanel_Voltage;
        private Label lb_Voltage1;
        private TableLayoutPanel tbPanel_cuting_speed;
        private Label lb_sawing_cutingspeedText;
        private Label lb_swing_motor_currentText;
        private TableLayoutPanel tbPanel_Electricity;
        private Label lab_reset1;
        private TableLayoutPanel tbPanel_powerconsumption;
        private Label lab_power1;
        private TableLayoutPanel tbPanel_oilpressure;
        private Label lb_oilpressText;
        private Button btn_Drill_lifesetting;
        private Label lb_swing_current;
        private Label lb_swing_Voltage;
        private Label lb_swingpower;
        private Label lb_oilpress;
        private Label lb_sawing_cutingspeed;
        private TableLayoutPanel tbPanel_Drill_totaltime;
        private Label lb_Drill_totaltime;
        private Label lb_Drill_totaltimeText;
        private TableLayoutPanel tbPanel_cutingtime;
        private Label lb_cutingtime;
        private Label lb_cutingtimeText;
        private TableLayoutPanel tbPanel_drilldu;
        private Label lb_drill_du;
        private Label lab_reset;
        private TableLayoutPanel tbPanel_drillcurrent;
        private Label lb_drill_current;
        private Label label_Ammeter;
        private TableLayoutPanel tbPanel_drillvoltage;
        private Label lb_drill_Voltage;
        private Label lb_Voltage;
        private TableLayoutPanel tbPanel_countdowntools;
        private Label lb_remain_toolsText;
        private TableLayoutPanel tbPanel_countdown;
        private Label lb_countdown_time;
        private Label lb_countdown_timeText;
        private TableLayoutPanel tbPanel_drillpower;
        private Label lb_drillpower;
        private Label lab_power;
        private Label lb_electricity;
        private Label lb_swing_motor_current;
        private Panel panel3;
        private Button btn_Sawing_lifesetting;
        private Label lab_Drill_lifesetting;
        private TableLayoutPanel tbPanel_Swing_totaltime;
        private Label lb_totaltime;
        private Label lb_totaltimeText;
        private Label lb_remain_tools;
        private Label lab_time;
    }
}

