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
            tbPanel_Swing_totaltime = new TableLayoutPanel();
            lb_Swing_totaltime = new Label();
            label10 = new Label();
            tbPanel_Swing_sum = new TableLayoutPanel();
            label11 = new Label();
            lab_sum_swing = new Label();
            tbPanel_Swing_connect = new TableLayoutPanel();
            label17 = new Label();
            lab_connect_swing = new Label();
            panel4 = new Panel();
            btn_SawBand = new Button();
            button2 = new Button();
            button1 = new Button();
            tbPanel_Swing_red = new TableLayoutPanel();
            label15 = new Label();
            lab_red_swing = new Label();
            tbPanel_Swing_disconnect = new TableLayoutPanel();
            label13 = new Label();
            lab_disconnect_swing = new Label();
            tbPanel_Swing_green = new TableLayoutPanel();
            label9 = new Label();
            lab_green_swing = new Label();
            tbPanel_Swing_yellow = new TableLayoutPanel();
            label7 = new Label();
            lab_yellow_swing = new Label();
            panel11 = new Panel();
            tbPanel_Drill_sum = new TableLayoutPanel();
            label6 = new Label();
            lab_sum = new Label();
            tbPanel_Drill_connect = new TableLayoutPanel();
            label5 = new Label();
            lab_connect = new Label();
            tbPanel_Drill_red = new TableLayoutPanel();
            label1 = new Label();
            lab_red = new Label();
            tbPanel_Drill_disconnect = new TableLayoutPanel();
            label4 = new Label();
            lab_disconnect = new Label();
            tbPanel_Drill_green = new TableLayoutPanel();
            label3 = new Label();
            lab_green = new Label();
            tbPanel_Drill_yellow = new TableLayoutPanel();
            label2 = new Label();
            lab_yellow = new Label();
            panel1 = new Panel();
            btn_Drill_lifesetting = new Button();
            label19 = new Label();
            btn_partLifeSetting = new Button();
            panel2 = new Panel();
            btn_Drill_Info = new Button();
            btn_machInfo = new Button();
            panel_SwingTime = new Panel();
            tbPanel_countdowntools = new TableLayoutPanel();
            lb_swing_Remaining_Dutting_tools = new Label();
            label29 = new Label();
            tbPanel_countdown = new TableLayoutPanel();
            lb_time = new Label();
            label30 = new Label();
            tbPanel_lifesetting = new TableLayoutPanel();
            label34 = new Label();
            btn_Sawing_lifesetting = new Button();
            tbPanel_Electricity = new TableLayoutPanel();
            lb_swingdu = new Label();
            label35 = new Label();
            tbPanel_powerconsumption = new TableLayoutPanel();
            lb_swingpower = new Label();
            label36 = new Label();
            tbPanel_oilpressure = new TableLayoutPanel();
            lb_oilpress = new Label();
            label37 = new Label();
            tbPanel_Ammeter = new TableLayoutPanel();
            lb_swing_current = new Label();
            label_Ammeter = new Label();
            tbPanel_Voltage = new TableLayoutPanel();
            lb_swing_Voltage = new Label();
            lb_Voltage = new Label();
            tbPanel_cuting_speed = new TableLayoutPanel();
            lb_swing_cutingspeed = new Label();
            label27 = new Label();
            tbPanel_motor_current = new TableLayoutPanel();
            lb_swing_motor_current = new Label();
            label21 = new Label();
            panel_Swing = new Panel();
            panel_DrillTime = new Panel();
            tbPanel_drillpower = new TableLayoutPanel();
            lb_drillpower = new Label();
            label28 = new Label();
            tbPanel_drilldu = new TableLayoutPanel();
            lb_drill_du = new Label();
            label25 = new Label();
            tbPanel_drillcurrent = new TableLayoutPanel();
            lb_drill_current = new Label();
            label20 = new Label();
            tbPanel_Drill_totaltime = new TableLayoutPanel();
            lb_Drill_totaltime = new Label();
            label16 = new Label();
            tbPanel_drillvoltage = new TableLayoutPanel();
            lb_drill_Voltage = new Label();
            label24 = new Label();
            tbPanel_cutingtime = new TableLayoutPanel();
            lb_cutingtime = new Label();
            label12 = new Label();
            panel_Drill = new Panel();
            panel12.SuspendLayout();
            tbPanel_Swing_totaltime.SuspendLayout();
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
            tbPanel_countdowntools.SuspendLayout();
            tbPanel_countdown.SuspendLayout();
            tbPanel_lifesetting.SuspendLayout();
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
            panel12.BorderStyle = BorderStyle.FixedSingle;
            panel12.Controls.Add(tbPanel_Swing_totaltime);
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
            // tbPanel_Swing_totaltime
            // 
            tbPanel_Swing_totaltime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_Swing_totaltime.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_totaltime.ColumnCount = 1;
            tbPanel_Swing_totaltime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_totaltime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_totaltime.Controls.Add(lb_Swing_totaltime, 0, 1);
            tbPanel_Swing_totaltime.Controls.Add(label10, 0, 0);
            tbPanel_Swing_totaltime.Location = new Point(17, 217);
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
            tbPanel_Swing_totaltime.Size = new Size(220, 65);
            tbPanel_Swing_totaltime.TabIndex = 68;
            // 
            // lb_Swing_totaltime
            // 
            lb_Swing_totaltime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lb_Swing_totaltime.AutoSize = true;
            lb_Swing_totaltime.BackColor = SystemColors.ButtonFace;
            lb_Swing_totaltime.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_Swing_totaltime.Location = new Point(3, 30);
            lb_Swing_totaltime.Name = "lb_Swing_totaltime";
            lb_Swing_totaltime.Size = new Size(212, 33);
            lb_Swing_totaltime.TabIndex = 47;
            lb_Swing_totaltime.Text = " 天  時  分  秒";
            lb_Swing_totaltime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = SystemColors.ButtonFace;
            label10.Dock = DockStyle.Fill;
            label10.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label10.Location = new Point(3, 0);
            label10.Name = "label10";
            label10.Size = new Size(212, 30);
            label10.TabIndex = 46;
            label10.Text = "總運轉時間";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Swing_sum
            // 
            tbPanel_Swing_sum.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_sum.ColumnCount = 1;
            tbPanel_Swing_sum.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_sum.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_sum.Controls.Add(label11, 0, 1);
            tbPanel_Swing_sum.Controls.Add(lab_sum_swing, 0, 0);
            tbPanel_Swing_sum.Location = new Point(21, 12);
            tbPanel_Swing_sum.Name = "tbPanel_Swing_sum";
            tbPanel_Swing_sum.RowCount = 2;
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_sum.Size = new Size(100, 90);
            tbPanel_Swing_sum.TabIndex = 33;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = SystemColors.ButtonFace;
            label11.Dock = DockStyle.Fill;
            label11.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label11.Location = new Point(3, 55);
            label11.Name = "label11";
            label11.Size = new Size(92, 33);
            label11.TabIndex = 23;
            label11.Text = "監控總數";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_sum_swing
            // 
            lab_sum_swing.AutoSize = true;
            lab_sum_swing.BackColor = SystemColors.ButtonFace;
            lab_sum_swing.Dock = DockStyle.Fill;
            lab_sum_swing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_sum_swing.Location = new Point(3, 0);
            lab_sum_swing.Name = "lab_sum_swing";
            lab_sum_swing.Size = new Size(92, 55);
            lab_sum_swing.TabIndex = 21;
            lab_sum_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Swing_connect
            // 
            tbPanel_Swing_connect.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_connect.ColumnCount = 1;
            tbPanel_Swing_connect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_connect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_connect.Controls.Add(label17, 0, 1);
            tbPanel_Swing_connect.Controls.Add(lab_connect_swing, 0, 0);
            tbPanel_Swing_connect.Location = new Point(137, 13);
            tbPanel_Swing_connect.Name = "tbPanel_Swing_connect";
            tbPanel_Swing_connect.RowCount = 2;
            tbPanel_Swing_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Swing_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Swing_connect.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_connect.Size = new Size(100, 90);
            tbPanel_Swing_connect.TabIndex = 35;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.BackColor = SystemColors.ButtonFace;
            label17.Dock = DockStyle.Fill;
            label17.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label17.Location = new Point(3, 55);
            label17.Name = "label17";
            label17.Size = new Size(92, 33);
            label17.TabIndex = 23;
            label17.Text = "連接總數";
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_connect_swing
            // 
            lab_connect_swing.AutoSize = true;
            lab_connect_swing.BackColor = SystemColors.ButtonFace;
            lab_connect_swing.Dock = DockStyle.Fill;
            lab_connect_swing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connect_swing.Location = new Point(3, 0);
            lab_connect_swing.Name = "lab_connect_swing";
            lab_connect_swing.Size = new Size(92, 55);
            lab_connect_swing.TabIndex = 21;
            lab_connect_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(btn_SawBand);
            panel4.Controls.Add(button2);
            panel4.Controls.Add(button1);
            panel4.Location = new Point(256, 217);
            panel4.Name = "panel4";
            panel4.Size = new Size(100, 65);
            panel4.TabIndex = 48;
            // 
            // btn_SawBand
            // 
            btn_SawBand.BackColor = SystemColors.ButtonFace;
            btn_SawBand.FlatStyle = FlatStyle.Flat;
            btn_SawBand.Location = new Point(11, 18);
            btn_SawBand.Margin = new Padding(4);
            btn_SawBand.Name = "btn_SawBand";
            btn_SawBand.Size = new Size(75, 31);
            btn_SawBand.TabIndex = 54;
            btn_SawBand.Text = "鋸帶資料";
            btn_SawBand.UseVisualStyleBackColor = false;
            btn_SawBand.Click += btn_SawBand_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button2.BackColor = SystemColors.ButtonFace;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(17, 13);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(0, 6);
            button2.TabIndex = 40;
            button2.Text = "鋸帶資料";
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = SystemColors.ButtonFace;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(16, 10);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(0, 6);
            button1.TabIndex = 39;
            button1.Text = "機台資訊";
            button1.UseVisualStyleBackColor = false;
            // 
            // tbPanel_Swing_red
            // 
            tbPanel_Swing_red.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_red.ColumnCount = 1;
            tbPanel_Swing_red.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_red.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_red.Controls.Add(label15, 0, 1);
            tbPanel_Swing_red.Controls.Add(lab_red_swing, 0, 0);
            tbPanel_Swing_red.Location = new Point(256, 115);
            tbPanel_Swing_red.Name = "tbPanel_Swing_red";
            tbPanel_Swing_red.RowCount = 2;
            tbPanel_Swing_red.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Swing_red.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Swing_red.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_red.Size = new Size(100, 90);
            tbPanel_Swing_red.TabIndex = 34;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = SystemColors.ButtonFace;
            label15.Dock = DockStyle.Fill;
            label15.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label15.Location = new Point(3, 55);
            label15.Name = "label15";
            label15.Size = new Size(92, 33);
            label15.TabIndex = 22;
            label15.Text = "紅燈總數";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_red_swing
            // 
            lab_red_swing.AutoSize = true;
            lab_red_swing.BackColor = SystemColors.ButtonFace;
            lab_red_swing.Dock = DockStyle.Fill;
            lab_red_swing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_red_swing.Location = new Point(3, 0);
            lab_red_swing.Name = "lab_red_swing";
            lab_red_swing.Size = new Size(92, 55);
            lab_red_swing.TabIndex = 21;
            lab_red_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Swing_disconnect
            // 
            tbPanel_Swing_disconnect.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_disconnect.ColumnCount = 1;
            tbPanel_Swing_disconnect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_disconnect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_disconnect.Controls.Add(label13, 0, 1);
            tbPanel_Swing_disconnect.Controls.Add(lab_disconnect_swing, 0, 0);
            tbPanel_Swing_disconnect.Location = new Point(256, 12);
            tbPanel_Swing_disconnect.Name = "tbPanel_Swing_disconnect";
            tbPanel_Swing_disconnect.RowCount = 2;
            tbPanel_Swing_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Swing_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Swing_disconnect.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_disconnect.Size = new Size(100, 90);
            tbPanel_Swing_disconnect.TabIndex = 36;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = SystemColors.ButtonFace;
            label13.Dock = DockStyle.Fill;
            label13.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label13.Location = new Point(3, 55);
            label13.Name = "label13";
            label13.Size = new Size(92, 33);
            label13.TabIndex = 23;
            label13.Text = "元件異常";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_disconnect_swing
            // 
            lab_disconnect_swing.AutoSize = true;
            lab_disconnect_swing.BackColor = SystemColors.ButtonFace;
            lab_disconnect_swing.Dock = DockStyle.Fill;
            lab_disconnect_swing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_disconnect_swing.Location = new Point(3, 0);
            lab_disconnect_swing.Name = "lab_disconnect_swing";
            lab_disconnect_swing.Size = new Size(92, 55);
            lab_disconnect_swing.TabIndex = 21;
            lab_disconnect_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Swing_green
            // 
            tbPanel_Swing_green.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_green.ColumnCount = 1;
            tbPanel_Swing_green.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_green.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_green.Controls.Add(label9, 0, 1);
            tbPanel_Swing_green.Controls.Add(lab_green_swing, 0, 0);
            tbPanel_Swing_green.Location = new Point(21, 115);
            tbPanel_Swing_green.Name = "tbPanel_Swing_green";
            tbPanel_Swing_green.RowCount = 2;
            tbPanel_Swing_green.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Swing_green.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Swing_green.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_green.Size = new Size(100, 90);
            tbPanel_Swing_green.TabIndex = 37;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.ButtonFace;
            label9.Dock = DockStyle.Fill;
            label9.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label9.Location = new Point(3, 55);
            label9.Name = "label9";
            label9.Size = new Size(92, 33);
            label9.TabIndex = 23;
            label9.Text = "綠燈總數";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_green_swing
            // 
            lab_green_swing.AutoSize = true;
            lab_green_swing.BackColor = SystemColors.ButtonFace;
            lab_green_swing.Dock = DockStyle.Fill;
            lab_green_swing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_green_swing.Location = new Point(3, 0);
            lab_green_swing.Name = "lab_green_swing";
            lab_green_swing.Size = new Size(92, 55);
            lab_green_swing.TabIndex = 21;
            lab_green_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Swing_yellow
            // 
            tbPanel_Swing_yellow.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_yellow.ColumnCount = 1;
            tbPanel_Swing_yellow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_yellow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Swing_yellow.Controls.Add(label7, 0, 1);
            tbPanel_Swing_yellow.Controls.Add(lab_yellow_swing, 0, 0);
            tbPanel_Swing_yellow.Location = new Point(137, 116);
            tbPanel_Swing_yellow.Name = "tbPanel_Swing_yellow";
            tbPanel_Swing_yellow.RowCount = 2;
            tbPanel_Swing_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Swing_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Swing_yellow.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Swing_yellow.Size = new Size(100, 90);
            tbPanel_Swing_yellow.TabIndex = 38;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.ButtonFace;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label7.Location = new Point(3, 55);
            label7.Name = "label7";
            label7.Size = new Size(92, 33);
            label7.TabIndex = 23;
            label7.Text = "黃燈總數";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_yellow_swing
            // 
            lab_yellow_swing.AutoSize = true;
            lab_yellow_swing.BackColor = SystemColors.ButtonFace;
            lab_yellow_swing.Dock = DockStyle.Fill;
            lab_yellow_swing.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yellow_swing.Location = new Point(3, 0);
            lab_yellow_swing.Name = "lab_yellow_swing";
            lab_yellow_swing.Size = new Size(92, 55);
            lab_yellow_swing.TabIndex = 21;
            lab_yellow_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel11
            // 
            panel11.AutoSizeMode = AutoSizeMode.GrowAndShrink;
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
            // 
            // tbPanel_Drill_sum
            // 
            tbPanel_Drill_sum.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_sum.ColumnCount = 1;
            tbPanel_Drill_sum.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_sum.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_sum.Controls.Add(label6, 0, 1);
            tbPanel_Drill_sum.Controls.Add(lab_sum, 0, 0);
            tbPanel_Drill_sum.Location = new Point(21, 15);
            tbPanel_Drill_sum.Name = "tbPanel_Drill_sum";
            tbPanel_Drill_sum.RowCount = 2;
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_sum.Size = new Size(100, 90);
            tbPanel_Drill_sum.TabIndex = 27;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ButtonFace;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label6.Location = new Point(3, 55);
            label6.Name = "label6";
            label6.Size = new Size(92, 33);
            label6.TabIndex = 23;
            label6.Text = "監控總數";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_sum
            // 
            lab_sum.AutoSize = true;
            lab_sum.BackColor = SystemColors.ButtonFace;
            lab_sum.Dock = DockStyle.Fill;
            lab_sum.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_sum.Location = new Point(3, 0);
            lab_sum.Name = "lab_sum";
            lab_sum.Size = new Size(92, 55);
            lab_sum.TabIndex = 21;
            lab_sum.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Drill_connect
            // 
            tbPanel_Drill_connect.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_connect.ColumnCount = 1;
            tbPanel_Drill_connect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_connect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_connect.Controls.Add(label5, 0, 1);
            tbPanel_Drill_connect.Controls.Add(lab_connect, 0, 0);
            tbPanel_Drill_connect.Location = new Point(137, 14);
            tbPanel_Drill_connect.Name = "tbPanel_Drill_connect";
            tbPanel_Drill_connect.RowCount = 2;
            tbPanel_Drill_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Drill_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Drill_connect.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_connect.Size = new Size(100, 90);
            tbPanel_Drill_connect.TabIndex = 29;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ButtonFace;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label5.Location = new Point(3, 55);
            label5.Name = "label5";
            label5.Size = new Size(92, 33);
            label5.TabIndex = 23;
            label5.Text = "連接總數";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_connect
            // 
            lab_connect.AutoSize = true;
            lab_connect.BackColor = SystemColors.ButtonFace;
            lab_connect.Dock = DockStyle.Fill;
            lab_connect.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connect.Location = new Point(3, 0);
            lab_connect.Name = "lab_connect";
            lab_connect.Size = new Size(92, 55);
            lab_connect.TabIndex = 21;
            lab_connect.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Drill_red
            // 
            tbPanel_Drill_red.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_red.ColumnCount = 1;
            tbPanel_Drill_red.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_red.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_red.Controls.Add(label1, 0, 1);
            tbPanel_Drill_red.Controls.Add(lab_red, 0, 0);
            tbPanel_Drill_red.Location = new Point(256, 121);
            tbPanel_Drill_red.Name = "tbPanel_Drill_red";
            tbPanel_Drill_red.RowCount = 2;
            tbPanel_Drill_red.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Drill_red.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Drill_red.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_red.Size = new Size(100, 90);
            tbPanel_Drill_red.TabIndex = 28;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonFace;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(3, 55);
            label1.Name = "label1";
            label1.Size = new Size(92, 33);
            label1.TabIndex = 22;
            label1.Text = "紅燈總數";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_red
            // 
            lab_red.AutoSize = true;
            lab_red.BackColor = SystemColors.ButtonFace;
            lab_red.Dock = DockStyle.Fill;
            lab_red.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_red.Location = new Point(3, 0);
            lab_red.Name = "lab_red";
            lab_red.Size = new Size(92, 55);
            lab_red.TabIndex = 21;
            lab_red.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Drill_disconnect
            // 
            tbPanel_Drill_disconnect.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_disconnect.ColumnCount = 1;
            tbPanel_Drill_disconnect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_disconnect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_disconnect.Controls.Add(label4, 0, 1);
            tbPanel_Drill_disconnect.Controls.Add(lab_disconnect, 0, 0);
            tbPanel_Drill_disconnect.Location = new Point(256, 13);
            tbPanel_Drill_disconnect.Name = "tbPanel_Drill_disconnect";
            tbPanel_Drill_disconnect.RowCount = 2;
            tbPanel_Drill_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Drill_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Drill_disconnect.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_disconnect.Size = new Size(100, 90);
            tbPanel_Drill_disconnect.TabIndex = 30;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ButtonFace;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label4.Location = new Point(3, 55);
            label4.Name = "label4";
            label4.Size = new Size(92, 33);
            label4.TabIndex = 23;
            label4.Text = "元件異常";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_disconnect
            // 
            lab_disconnect.AutoSize = true;
            lab_disconnect.BackColor = SystemColors.ButtonFace;
            lab_disconnect.Dock = DockStyle.Fill;
            lab_disconnect.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_disconnect.Location = new Point(3, 0);
            lab_disconnect.Name = "lab_disconnect";
            lab_disconnect.Size = new Size(92, 55);
            lab_disconnect.TabIndex = 21;
            lab_disconnect.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Drill_green
            // 
            tbPanel_Drill_green.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_green.ColumnCount = 1;
            tbPanel_Drill_green.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_green.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_green.Controls.Add(label3, 0, 1);
            tbPanel_Drill_green.Controls.Add(lab_green, 0, 0);
            tbPanel_Drill_green.Location = new Point(21, 121);
            tbPanel_Drill_green.Name = "tbPanel_Drill_green";
            tbPanel_Drill_green.RowCount = 2;
            tbPanel_Drill_green.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Drill_green.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Drill_green.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_green.Size = new Size(100, 90);
            tbPanel_Drill_green.TabIndex = 31;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonFace;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label3.Location = new Point(3, 55);
            label3.Name = "label3";
            label3.Size = new Size(92, 33);
            label3.TabIndex = 23;
            label3.Text = "綠燈總數";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_green
            // 
            lab_green.AutoSize = true;
            lab_green.BackColor = SystemColors.ButtonFace;
            lab_green.Dock = DockStyle.Fill;
            lab_green.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_green.Location = new Point(3, 0);
            lab_green.Name = "lab_green";
            lab_green.Size = new Size(92, 55);
            lab_green.TabIndex = 21;
            lab_green.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Drill_yellow
            // 
            tbPanel_Drill_yellow.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_yellow.ColumnCount = 1;
            tbPanel_Drill_yellow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_yellow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_yellow.Controls.Add(label2, 0, 1);
            tbPanel_Drill_yellow.Controls.Add(lab_yellow, 0, 0);
            tbPanel_Drill_yellow.Location = new Point(137, 122);
            tbPanel_Drill_yellow.Name = "tbPanel_Drill_yellow";
            tbPanel_Drill_yellow.RowCount = 2;
            tbPanel_Drill_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tbPanel_Drill_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tbPanel_Drill_yellow.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Drill_yellow.Size = new Size(100, 90);
            tbPanel_Drill_yellow.TabIndex = 32;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonFace;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label2.Location = new Point(3, 55);
            label2.Name = "label2";
            label2.Size = new Size(92, 33);
            label2.TabIndex = 23;
            label2.Text = "黃燈總數";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_yellow
            // 
            lab_yellow.AutoSize = true;
            lab_yellow.BackColor = SystemColors.ButtonFace;
            lab_yellow.Dock = DockStyle.Fill;
            lab_yellow.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yellow.Location = new Point(3, 0);
            lab_yellow.Name = "lab_yellow";
            lab_yellow.Size = new Size(92, 55);
            lab_yellow.TabIndex = 21;
            lab_yellow.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btn_Drill_lifesetting);
            panel1.Controls.Add(label19);
            panel1.Controls.Add(btn_partLifeSetting);
            panel1.Location = new Point(17, 230);
            panel1.Name = "panel1";
            panel1.Size = new Size(220, 45);
            panel1.TabIndex = 41;
            // 
            // btn_Drill_lifesetting
            // 
            btn_Drill_lifesetting.BackColor = SystemColors.ButtonFace;
            btn_Drill_lifesetting.FlatStyle = FlatStyle.Flat;
            btn_Drill_lifesetting.Location = new Point(142, 6);
            btn_Drill_lifesetting.Margin = new Padding(4);
            btn_Drill_lifesetting.Name = "btn_Drill_lifesetting";
            btn_Drill_lifesetting.Size = new Size(58, 31);
            btn_Drill_lifesetting.TabIndex = 54;
            btn_Drill_lifesetting.Text = "設定";
            btn_Drill_lifesetting.UseVisualStyleBackColor = false;
            btn_Drill_lifesetting.Click += btn_Drill_lifesetting_Click;
            // 
            // label19
            // 
            label19.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label19.AutoSize = true;
            label19.BackColor = SystemColors.ButtonFace;
            label19.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label19.Location = new Point(17, 13);
            label19.Name = "label19";
            label19.Size = new Size(99, 19);
            label19.TabIndex = 43;
            label19.Text = "元件壽命設定";
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_partLifeSetting
            // 
            btn_partLifeSetting.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_partLifeSetting.BackColor = SystemColors.ButtonFace;
            btn_partLifeSetting.FlatStyle = FlatStyle.Flat;
            btn_partLifeSetting.Location = new Point(142, 10);
            btn_partLifeSetting.Margin = new Padding(4);
            btn_partLifeSetting.Name = "btn_partLifeSetting";
            btn_partLifeSetting.Size = new Size(70, 0);
            btn_partLifeSetting.TabIndex = 40;
            btn_partLifeSetting.Text = "設定";
            btn_partLifeSetting.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(btn_Drill_Info);
            panel2.Controls.Add(btn_machInfo);
            panel2.Location = new Point(253, 230);
            panel2.Name = "panel2";
            panel2.Size = new Size(103, 45);
            panel2.TabIndex = 42;
            // 
            // btn_Drill_Info
            // 
            btn_Drill_Info.BackColor = SystemColors.ButtonFace;
            btn_Drill_Info.FlatStyle = FlatStyle.Flat;
            btn_Drill_Info.Location = new Point(13, 6);
            btn_Drill_Info.Margin = new Padding(4);
            btn_Drill_Info.Name = "btn_Drill_Info";
            btn_Drill_Info.Size = new Size(75, 31);
            btn_Drill_Info.TabIndex = 53;
            btn_Drill_Info.Text = "機台資訊";
            btn_Drill_Info.UseVisualStyleBackColor = false;
            btn_Drill_Info.Click += btn_Drill_Info_Click;
            // 
            // btn_machInfo
            // 
            btn_machInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_machInfo.BackColor = SystemColors.ButtonFace;
            btn_machInfo.FlatStyle = FlatStyle.Flat;
            btn_machInfo.Location = new Point(16, 10);
            btn_machInfo.Margin = new Padding(4);
            btn_machInfo.Name = "btn_machInfo";
            btn_machInfo.Size = new Size(0, 0);
            btn_machInfo.TabIndex = 39;
            btn_machInfo.Text = "鑽床資料";
            btn_machInfo.UseVisualStyleBackColor = false;
            // 
            // panel_SwingTime
            // 
            panel_SwingTime.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_SwingTime.BorderStyle = BorderStyle.FixedSingle;
            panel_SwingTime.Controls.Add(tbPanel_countdowntools);
            panel_SwingTime.Controls.Add(tbPanel_countdown);
            panel_SwingTime.Controls.Add(tbPanel_lifesetting);
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
            panel_SwingTime.Paint += panel_SwingTime_Paint;
            // 
            // tbPanel_countdowntools
            // 
            tbPanel_countdowntools.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_countdowntools.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_countdowntools.ColumnCount = 2;
            tbPanel_countdowntools.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65.16393F));
            tbPanel_countdowntools.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.8360672F));
            tbPanel_countdowntools.Controls.Add(lb_swing_Remaining_Dutting_tools, 1, 0);
            tbPanel_countdowntools.Controls.Add(label29, 0, 0);
            tbPanel_countdowntools.Location = new Point(5, 256);
            tbPanel_countdowntools.Name = "tbPanel_countdowntools";
            tbPanel_countdowntools.RowCount = 1;
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Percent, 60.5263176F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Percent, 39.4736824F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 15F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdowntools.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_countdowntools.Size = new Size(297, 35);
            tbPanel_countdowntools.TabIndex = 71;
            // 
            // lb_swing_Remaining_Dutting_tools
            // 
            lb_swing_Remaining_Dutting_tools.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lb_swing_Remaining_Dutting_tools.AutoSize = true;
            lb_swing_Remaining_Dutting_tools.BackColor = SystemColors.ButtonFace;
            lb_swing_Remaining_Dutting_tools.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swing_Remaining_Dutting_tools.Location = new Point(195, 0);
            lb_swing_Remaining_Dutting_tools.Name = "lb_swing_Remaining_Dutting_tools";
            lb_swing_Remaining_Dutting_tools.Size = new Size(97, 33);
            lb_swing_Remaining_Dutting_tools.TabIndex = 48;
            lb_swing_Remaining_Dutting_tools.Text = "0 刀";
            lb_swing_Remaining_Dutting_tools.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.BackColor = SystemColors.ButtonFace;
            label29.Dock = DockStyle.Fill;
            label29.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label29.Location = new Point(3, 0);
            label29.Name = "label29";
            label29.Size = new Size(186, 33);
            label29.TabIndex = 47;
            label29.Text = "加工倒數刀數：";
            label29.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbPanel_countdown
            // 
            tbPanel_countdown.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_countdown.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_countdown.ColumnCount = 1;
            tbPanel_countdown.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_countdown.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_countdown.Controls.Add(lb_time, 0, 1);
            tbPanel_countdown.Controls.Add(label30, 0, 0);
            tbPanel_countdown.Location = new Point(5, 212);
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
            tbPanel_countdown.Size = new Size(297, 41);
            tbPanel_countdown.TabIndex = 70;
            // 
            // lb_time
            // 
            lb_time.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lb_time.AutoSize = true;
            lb_time.BackColor = SystemColors.ButtonFace;
            lb_time.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_time.Location = new Point(3, 18);
            lb_time.Name = "lb_time";
            lb_time.Size = new Size(289, 21);
            lb_time.TabIndex = 47;
            lb_time.Text = "  時  分  秒";
            lb_time.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.BackColor = SystemColors.ButtonFace;
            label30.Dock = DockStyle.Fill;
            label30.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label30.Location = new Point(3, 0);
            label30.Name = "label30";
            label30.Size = new Size(289, 18);
            label30.TabIndex = 46;
            label30.Text = "鋸切倒數計時";
            label30.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_lifesetting
            // 
            tbPanel_lifesetting.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_lifesetting.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_lifesetting.ColumnCount = 1;
            tbPanel_lifesetting.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_lifesetting.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_lifesetting.Controls.Add(label34, 0, 0);
            tbPanel_lifesetting.Controls.Add(btn_Sawing_lifesetting, 0, 1);
            tbPanel_lifesetting.Location = new Point(235, 116);
            tbPanel_lifesetting.Name = "tbPanel_lifesetting";
            tbPanel_lifesetting.RowCount = 2;
            tbPanel_lifesetting.RowStyles.Add(new RowStyle(SizeType.Percent, 62.5F));
            tbPanel_lifesetting.RowStyles.Add(new RowStyle(SizeType.Percent, 37.5F));
            tbPanel_lifesetting.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_lifesetting.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_lifesetting.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_lifesetting.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_lifesetting.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_lifesetting.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_lifesetting.Size = new Size(65, 90);
            tbPanel_lifesetting.TabIndex = 69;
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.BackColor = SystemColors.ButtonFace;
            label34.Dock = DockStyle.Fill;
            label34.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label34.Location = new Point(3, 0);
            label34.Name = "label34";
            label34.Size = new Size(57, 55);
            label34.TabIndex = 24;
            label34.Text = "元件\r\n壽命\r\n設定\r\n";
            label34.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_Sawing_lifesetting
            // 
            btn_Sawing_lifesetting.BackColor = SystemColors.ButtonFace;
            btn_Sawing_lifesetting.FlatStyle = FlatStyle.Flat;
            btn_Sawing_lifesetting.Location = new Point(4, 59);
            btn_Sawing_lifesetting.Margin = new Padding(4);
            btn_Sawing_lifesetting.Name = "btn_Sawing_lifesetting";
            btn_Sawing_lifesetting.Size = new Size(55, 25);
            btn_Sawing_lifesetting.TabIndex = 55;
            btn_Sawing_lifesetting.Text = "設定";
            btn_Sawing_lifesetting.UseVisualStyleBackColor = false;
            btn_Sawing_lifesetting.Click += button3_Click;
            // 
            // tbPanel_Electricity
            // 
            tbPanel_Electricity.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_Electricity.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Electricity.ColumnCount = 1;
            tbPanel_Electricity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Electricity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Electricity.Controls.Add(lb_swingdu, 0, 1);
            tbPanel_Electricity.Controls.Add(label35, 0, 0);
            tbPanel_Electricity.Location = new Point(159, 115);
            tbPanel_Electricity.Name = "tbPanel_Electricity";
            tbPanel_Electricity.RowCount = 2;
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Percent, 63.5416679F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Percent, 36.4583321F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_Electricity.Size = new Size(70, 90);
            tbPanel_Electricity.TabIndex = 68;
            // 
            // lb_swingdu
            // 
            lb_swingdu.AutoSize = true;
            lb_swingdu.BackColor = SystemColors.ButtonFace;
            lb_swingdu.Dock = DockStyle.Fill;
            lb_swingdu.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swingdu.Location = new Point(3, 55);
            lb_swingdu.Name = "lb_swingdu";
            lb_swingdu.Size = new Size(62, 33);
            lb_swingdu.TabIndex = 27;
            lb_swingdu.Text = "0\r\n度";
            lb_swingdu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.BackColor = SystemColors.ButtonFace;
            label35.Dock = DockStyle.Fill;
            label35.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label35.Location = new Point(3, 0);
            label35.Name = "label35";
            label35.Size = new Size(62, 55);
            label35.TabIndex = 24;
            label35.Text = "累積\r\n用電\r\n度數";
            label35.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_powerconsumption
            // 
            tbPanel_powerconsumption.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_powerconsumption.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_powerconsumption.ColumnCount = 1;
            tbPanel_powerconsumption.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_powerconsumption.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_powerconsumption.Controls.Add(lb_swingpower, 0, 1);
            tbPanel_powerconsumption.Controls.Add(label36, 0, 0);
            tbPanel_powerconsumption.Location = new Point(84, 115);
            tbPanel_powerconsumption.Name = "tbPanel_powerconsumption";
            tbPanel_powerconsumption.RowCount = 2;
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Percent, 58.3333321F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Percent, 41.6666679F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_powerconsumption.Size = new Size(70, 90);
            tbPanel_powerconsumption.TabIndex = 67;
            // 
            // lb_swingpower
            // 
            lb_swingpower.AutoSize = true;
            lb_swingpower.BackColor = SystemColors.ButtonFace;
            lb_swingpower.Dock = DockStyle.Fill;
            lb_swingpower.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swingpower.Location = new Point(3, 51);
            lb_swingpower.Name = "lb_swingpower";
            lb_swingpower.Size = new Size(62, 37);
            lb_swingpower.TabIndex = 29;
            lb_swingpower.Text = "0\r\n千瓦小時";
            lb_swingpower.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.BackColor = SystemColors.ButtonFace;
            label36.Dock = DockStyle.Fill;
            label36.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label36.Location = new Point(3, 0);
            label36.Name = "label36";
            label36.Size = new Size(62, 51);
            label36.TabIndex = 24;
            label36.Text = "消耗\r\n功率";
            label36.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_oilpressure
            // 
            tbPanel_oilpressure.AccessibleRole = AccessibleRole.None;
            tbPanel_oilpressure.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_oilpressure.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_oilpressure.ColumnCount = 1;
            tbPanel_oilpressure.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_oilpressure.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_oilpressure.Controls.Add(lb_oilpress, 0, 1);
            tbPanel_oilpressure.Controls.Add(label37, 0, 0);
            tbPanel_oilpressure.Location = new Point(8, 114);
            tbPanel_oilpressure.Name = "tbPanel_oilpressure";
            tbPanel_oilpressure.RowCount = 2;
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Percent, 59.375F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Percent, 40.625F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tbPanel_oilpressure.Size = new Size(70, 90);
            tbPanel_oilpressure.TabIndex = 66;
            // 
            // lb_oilpress
            // 
            lb_oilpress.AutoSize = true;
            lb_oilpress.BackColor = SystemColors.ButtonFace;
            lb_oilpress.Dock = DockStyle.Fill;
            lb_oilpress.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_oilpress.Location = new Point(3, 52);
            lb_oilpress.Name = "lb_oilpress";
            lb_oilpress.Size = new Size(62, 36);
            lb_oilpress.TabIndex = 28;
            lb_oilpress.Text = "0\r\n公斤力";
            lb_oilpress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.BackColor = SystemColors.ButtonFace;
            label37.Dock = DockStyle.Fill;
            label37.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label37.Location = new Point(3, 0);
            label37.Name = "label37";
            label37.Size = new Size(62, 52);
            label37.TabIndex = 24;
            label37.Text = "油壓\r\n張力";
            label37.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Ammeter
            // 
            tbPanel_Ammeter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_Ammeter.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Ammeter.ColumnCount = 1;
            tbPanel_Ammeter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Ammeter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Ammeter.Controls.Add(lb_swing_current, 0, 1);
            tbPanel_Ammeter.Controls.Add(label_Ammeter, 0, 0);
            tbPanel_Ammeter.Location = new Point(236, 12);
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
            tbPanel_Ammeter.Size = new Size(65, 90);
            tbPanel_Ammeter.TabIndex = 65;
            // 
            // lb_swing_current
            // 
            lb_swing_current.AutoSize = true;
            lb_swing_current.BackColor = SystemColors.ButtonFace;
            lb_swing_current.Dock = DockStyle.Fill;
            lb_swing_current.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swing_current.Location = new Point(3, 46);
            lb_swing_current.Name = "lb_swing_current";
            lb_swing_current.Size = new Size(57, 42);
            lb_swing_current.TabIndex = 25;
            lb_swing_current.Text = "0\r\n安培";
            lb_swing_current.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_Ammeter
            // 
            label_Ammeter.AutoSize = true;
            label_Ammeter.BackColor = SystemColors.ButtonFace;
            label_Ammeter.Dock = DockStyle.Fill;
            label_Ammeter.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_Ammeter.Location = new Point(3, 0);
            label_Ammeter.Name = "label_Ammeter";
            label_Ammeter.Size = new Size(57, 46);
            label_Ammeter.TabIndex = 24;
            label_Ammeter.Text = "電流\r\n平均";
            label_Ammeter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Voltage
            // 
            tbPanel_Voltage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_Voltage.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Voltage.ColumnCount = 1;
            tbPanel_Voltage.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Voltage.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Voltage.Controls.Add(lb_swing_Voltage, 0, 1);
            tbPanel_Voltage.Controls.Add(lb_Voltage, 0, 0);
            tbPanel_Voltage.Location = new Point(159, 11);
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
            tbPanel_Voltage.Size = new Size(70, 90);
            tbPanel_Voltage.TabIndex = 64;
            // 
            // lb_swing_Voltage
            // 
            lb_swing_Voltage.AutoSize = true;
            lb_swing_Voltage.BackColor = SystemColors.ButtonFace;
            lb_swing_Voltage.Dock = DockStyle.Fill;
            lb_swing_Voltage.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swing_Voltage.Location = new Point(3, 46);
            lb_swing_Voltage.Name = "lb_swing_Voltage";
            lb_swing_Voltage.Size = new Size(62, 42);
            lb_swing_Voltage.TabIndex = 26;
            lb_swing_Voltage.Text = "0\r\n伏特";
            lb_swing_Voltage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_Voltage
            // 
            lb_Voltage.AutoSize = true;
            lb_Voltage.BackColor = SystemColors.ButtonFace;
            lb_Voltage.Dock = DockStyle.Fill;
            lb_Voltage.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_Voltage.Location = new Point(3, 0);
            lb_Voltage.Name = "lb_Voltage";
            lb_Voltage.Size = new Size(62, 46);
            lb_Voltage.TabIndex = 24;
            lb_Voltage.Text = "電壓\r\n平均";
            lb_Voltage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_cuting_speed
            // 
            tbPanel_cuting_speed.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_cuting_speed.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_cuting_speed.ColumnCount = 1;
            tbPanel_cuting_speed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_cuting_speed.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_cuting_speed.Controls.Add(lb_swing_cutingspeed, 0, 1);
            tbPanel_cuting_speed.Controls.Add(label27, 0, 0);
            tbPanel_cuting_speed.Location = new Point(84, 11);
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
            tbPanel_cuting_speed.Size = new Size(70, 90);
            tbPanel_cuting_speed.TabIndex = 63;
            // 
            // lb_swing_cutingspeed
            // 
            lb_swing_cutingspeed.AutoSize = true;
            lb_swing_cutingspeed.BackColor = SystemColors.ButtonFace;
            lb_swing_cutingspeed.Dock = DockStyle.Fill;
            lb_swing_cutingspeed.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swing_cutingspeed.Location = new Point(3, 46);
            lb_swing_cutingspeed.Name = "lb_swing_cutingspeed";
            lb_swing_cutingspeed.Size = new Size(62, 42);
            lb_swing_cutingspeed.TabIndex = 27;
            lb_swing_cutingspeed.Text = "0\r\nm/min";
            lb_swing_cutingspeed.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.BackColor = SystemColors.ButtonFace;
            label27.Dock = DockStyle.Fill;
            label27.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label27.Location = new Point(3, 0);
            label27.Name = "label27";
            label27.Size = new Size(62, 46);
            label27.TabIndex = 24;
            label27.Text = "切削\r\n速度";
            label27.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_motor_current
            // 
            tbPanel_motor_current.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_motor_current.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_motor_current.ColumnCount = 1;
            tbPanel_motor_current.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_motor_current.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_motor_current.Controls.Add(lb_swing_motor_current, 0, 1);
            tbPanel_motor_current.Controls.Add(label21, 0, 0);
            tbPanel_motor_current.Location = new Point(9, 11);
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
            tbPanel_motor_current.Size = new Size(70, 90);
            tbPanel_motor_current.TabIndex = 62;
            // 
            // lb_swing_motor_current
            // 
            lb_swing_motor_current.AutoSize = true;
            lb_swing_motor_current.BackColor = SystemColors.ButtonFace;
            lb_swing_motor_current.Dock = DockStyle.Fill;
            lb_swing_motor_current.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_swing_motor_current.Location = new Point(3, 46);
            lb_swing_motor_current.Name = "lb_swing_motor_current";
            lb_swing_motor_current.Size = new Size(62, 42);
            lb_swing_motor_current.TabIndex = 27;
            lb_swing_motor_current.Text = "0\r\n安培";
            lb_swing_motor_current.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.BackColor = SystemColors.ButtonFace;
            label21.Dock = DockStyle.Fill;
            label21.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label21.Location = new Point(3, 0);
            label21.Name = "label21";
            label21.Size = new Size(62, 46);
            label21.TabIndex = 24;
            label21.Text = "馬達\r\n電流";
            label21.TextAlign = ContentAlignment.MiddleCenter;
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
            tbPanel_drillpower.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_drillpower.ColumnCount = 1;
            tbPanel_drillpower.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillpower.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillpower.Controls.Add(lb_drillpower, 0, 1);
            tbPanel_drillpower.Controls.Add(label28, 0, 0);
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
            lb_drillpower.BackColor = SystemColors.ButtonFace;
            lb_drillpower.Dock = DockStyle.Fill;
            lb_drillpower.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            lb_drillpower.Location = new Point(3, 24);
            lb_drillpower.Name = "lb_drillpower";
            lb_drillpower.Size = new Size(135, 49);
            lb_drillpower.TabIndex = 27;
            lb_drillpower.Text = "0\r\n千瓦/小時";
            lb_drillpower.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.BackColor = SystemColors.ButtonFace;
            label28.Dock = DockStyle.Fill;
            label28.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label28.Location = new Point(3, 0);
            label28.Name = "label28";
            label28.Size = new Size(135, 24);
            label28.TabIndex = 24;
            label28.Text = "消耗功率";
            label28.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_drilldu
            // 
            tbPanel_drilldu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_drilldu.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_drilldu.ColumnCount = 1;
            tbPanel_drilldu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drilldu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drilldu.Controls.Add(lb_drill_du, 0, 1);
            tbPanel_drilldu.Controls.Add(label25, 0, 0);
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
            lb_drill_du.BackColor = SystemColors.ButtonFace;
            lb_drill_du.Dock = DockStyle.Fill;
            lb_drill_du.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            lb_drill_du.Location = new Point(3, 21);
            lb_drill_du.Name = "lb_drill_du";
            lb_drill_du.Size = new Size(135, 52);
            lb_drill_du.TabIndex = 27;
            lb_drill_du.Text = "0\r\n千瓦/小時";
            lb_drill_du.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.BackColor = SystemColors.ButtonFace;
            label25.Dock = DockStyle.Fill;
            label25.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label25.Location = new Point(3, 0);
            label25.Name = "label25";
            label25.Size = new Size(135, 21);
            label25.TabIndex = 24;
            label25.Text = "累積用電度數";
            label25.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_drillcurrent
            // 
            tbPanel_drillcurrent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_drillcurrent.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_drillcurrent.ColumnCount = 1;
            tbPanel_drillcurrent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillcurrent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillcurrent.Controls.Add(lb_drill_current, 0, 1);
            tbPanel_drillcurrent.Controls.Add(label20, 0, 0);
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
            lb_drill_current.BackColor = SystemColors.ButtonFace;
            lb_drill_current.Dock = DockStyle.Fill;
            lb_drill_current.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            lb_drill_current.Location = new Point(3, 23);
            lb_drill_current.Name = "lb_drill_current";
            lb_drill_current.Size = new Size(139, 50);
            lb_drill_current.TabIndex = 25;
            lb_drill_current.Text = "0\r\n安培";
            lb_drill_current.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.BackColor = SystemColors.ButtonFace;
            label20.Dock = DockStyle.Fill;
            label20.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label20.Location = new Point(3, 0);
            label20.Name = "label20";
            label20.Size = new Size(139, 23);
            label20.TabIndex = 24;
            label20.Text = "電流平均";
            label20.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Drill_totaltime
            // 
            tbPanel_Drill_totaltime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_Drill_totaltime.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_totaltime.ColumnCount = 1;
            tbPanel_Drill_totaltime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_totaltime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_Drill_totaltime.Controls.Add(lb_Drill_totaltime, 0, 1);
            tbPanel_Drill_totaltime.Controls.Add(label16, 0, 0);
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
            lb_Drill_totaltime.BackColor = SystemColors.ButtonFace;
            lb_Drill_totaltime.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_Drill_totaltime.Location = new Point(3, 24);
            lb_Drill_totaltime.Name = "lb_Drill_totaltime";
            lb_Drill_totaltime.Size = new Size(292, 26);
            lb_Drill_totaltime.TabIndex = 47;
            lb_Drill_totaltime.Text = " 天  時  分  秒";
            lb_Drill_totaltime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.BackColor = SystemColors.ButtonFace;
            label16.Dock = DockStyle.Fill;
            label16.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label16.Location = new Point(3, 0);
            label16.Name = "label16";
            label16.Size = new Size(292, 24);
            label16.TabIndex = 46;
            label16.Text = "總運轉時間";
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_drillvoltage
            // 
            tbPanel_drillvoltage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_drillvoltage.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_drillvoltage.ColumnCount = 1;
            tbPanel_drillvoltage.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillvoltage.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_drillvoltage.Controls.Add(lb_drill_Voltage, 0, 1);
            tbPanel_drillvoltage.Controls.Add(label24, 0, 0);
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
            lb_drill_Voltage.BackColor = SystemColors.ButtonFace;
            lb_drill_Voltage.Dock = DockStyle.Fill;
            lb_drill_Voltage.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            lb_drill_Voltage.Location = new Point(3, 22);
            lb_drill_Voltage.Name = "lb_drill_Voltage";
            lb_drill_Voltage.Size = new Size(139, 51);
            lb_drill_Voltage.TabIndex = 26;
            lb_drill_Voltage.Text = "0\r\n伏特";
            lb_drill_Voltage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.BackColor = SystemColors.ButtonFace;
            label24.Dock = DockStyle.Fill;
            label24.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label24.Location = new Point(3, 0);
            label24.Name = "label24";
            label24.Size = new Size(139, 22);
            label24.TabIndex = 24;
            label24.Text = "電壓平均";
            label24.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_cutingtime
            // 
            tbPanel_cutingtime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbPanel_cutingtime.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_cutingtime.ColumnCount = 1;
            tbPanel_cutingtime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_cutingtime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tbPanel_cutingtime.Controls.Add(lb_cutingtime, 0, 1);
            tbPanel_cutingtime.Controls.Add(label12, 0, 0);
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
            lb_cutingtime.BackColor = SystemColors.ButtonFace;
            lb_cutingtime.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lb_cutingtime.Location = new Point(3, 24);
            lb_cutingtime.Name = "lb_cutingtime";
            lb_cutingtime.Size = new Size(292, 26);
            lb_cutingtime.TabIndex = 47;
            lb_cutingtime.Text = " 天  時  分  秒";
            lb_cutingtime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = SystemColors.ButtonFace;
            label12.Dock = DockStyle.Fill;
            label12.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label12.Location = new Point(3, 0);
            label12.Name = "label12";
            label12.Size = new Size(292, 24);
            label12.TabIndex = 46;
            label12.Text = "實際加工時間";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel_Drill
            // 
            panel_Drill.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_Drill.BackgroundImage = Properties.Resources.Drill;
            panel_Drill.BackgroundImageLayout = ImageLayout.Stretch;
            panel_Drill.BorderStyle = BorderStyle.FixedSingle;
            panel_Drill.Location = new Point(4, 10);
            panel_Drill.Name = "panel_Drill";
            panel_Drill.Size = new Size(240, 297);
            panel_Drill.TabIndex = 47;
            // 
            // Main_form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(956, 625);
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
            VisibleChanged += Main_form_VisibleChanged;
            panel12.ResumeLayout(false);
            tbPanel_Swing_totaltime.ResumeLayout(false);
            tbPanel_Swing_totaltime.PerformLayout();
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
            tbPanel_countdowntools.ResumeLayout(false);
            tbPanel_countdowntools.PerformLayout();
            tbPanel_countdown.ResumeLayout(false);
            tbPanel_countdown.PerformLayout();
            tbPanel_lifesetting.ResumeLayout(false);
            tbPanel_lifesetting.PerformLayout();
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
        }

        #endregion
        private Button btn_SawBand;
        private Panel panel12;
        private TableLayoutPanel tbPanel_Swing_sum;
        private Label label11;
        private Label lab_sum_swing;
        private TableLayoutPanel tbPanel_Swing_connect;
        private Label label17;
        private Label lab_connect_swing;
        private Panel panel4;
        private Button button2;
        private Button button1;
        private TableLayoutPanel tbPanel_Swing_red;
        private Label label15;
        private Label lab_red_swing;
        private TableLayoutPanel tbPanel_Swing_disconnect;
        private Label label13;
        private Label lab_disconnect_swing;
        private TableLayoutPanel tbPanel_Swing_green;
        private Label label9;
        private Label lab_green_swing;
        private TableLayoutPanel tbPanel_Swing_yellow;
        private Label label7;
        private Label lab_yellow_swing;
        private Panel panel11;
        private TableLayoutPanel tbPanel_Drill_sum;
        private Label label6;
        private Label lab_sum;
        private TableLayoutPanel tbPanel_Drill_connect;
        private Label label5;
        private Label lab_connect;
        private TableLayoutPanel tbPanel_Drill_red;
        private Label label1;
        private Label lab_red;
        private TableLayoutPanel tbPanel_Drill_disconnect;
        private Label label4;
        private Label lab_disconnect;
        private TableLayoutPanel tbPanel_Drill_green;
        private Label label3;
        private Label lab_green;
        private TableLayoutPanel tbPanel_Drill_yellow;
        private Label label2;
        private Label lab_yellow;
        private Panel panel1;
        private Label label19;
        private Button btn_partLifeSetting;
        private Panel panel2;
        private Button btn_machInfo;
        private Panel panel_SwingTime;
        private Panel panel_Swing;
        private Panel panel_DrillTime;
        private Panel panel_Drill;
        private Button btn_Drill_Info;
        private TableLayoutPanel tbPanel_motor_current;
        private TableLayoutPanel tbPanel_Ammeter;
        private Label label_Ammeter;
        private TableLayoutPanel tbPanel_Voltage;
        private Label lb_Voltage;
        private TableLayoutPanel tbPanel_cuting_speed;
        private Label label27;
        private Label label21;
        private TableLayoutPanel tbPanel_lifesetting;
        private Label label34;
        private TableLayoutPanel tbPanel_Electricity;
        private Label label35;
        private TableLayoutPanel tbPanel_powerconsumption;
        private Label label36;
        private TableLayoutPanel tbPanel_oilpressure;
        private Label label37;
        private Button btn_Drill_lifesetting;
        private Label lb_swing_current;
        private Label lb_swing_Voltage;
        private Button btn_Sawing_lifesetting;
        private Label lb_swingdu;
        private Label lb_swingpower;
        private Label lb_oilpress;
        private Label lb_swing_cutingspeed;
        private Label lb_swing_motor_current;
        private TableLayoutPanel tbPanel_Swing_totaltime;
        private Label lb_Swing_totaltime;
        private Label label10;
        private TableLayoutPanel tbPanel_Drill_totaltime;
        private Label lb_Drill_totaltime;
        private Label label16;
        private TableLayoutPanel tbPanel_cutingtime;
        private Label lb_cutingtime;
        private Label label12;
        private TableLayoutPanel tbPanel_drilldu;
        private Label lb_drill_du;
        private Label label25;
        private TableLayoutPanel tbPanel_drillcurrent;
        private Label lb_drill_current;
        private Label label20;
        private TableLayoutPanel tbPanel_drillvoltage;
        private Label lb_drill_Voltage;
        private Label label24;
        private TableLayoutPanel tbPanel_countdowntools;
        private Label lb_swing_Remaining_Dutting_tools;
        private Label label29;
        private TableLayoutPanel tbPanel_countdown;
        private Label lb_time;
        private Label label30;
        private TableLayoutPanel tbPanel_drillpower;
        private Label lb_drillpower;
        private Label label28;
    }
}

