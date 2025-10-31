namespace FX5U_IOMonitor
{
    partial class Home_Page
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
            panel6 = new Panel();
            lab_connectrecord1 = new Label();
            lab_IP_Port2 = new Label();
            lab_connect_2 = new Label();
            tbPanel_Swing_sum = new TableLayoutPanel();
            lab_sumS = new Label();
            lab_sum_swing = new Label();
            tbPanel_Swing_connect = new TableLayoutPanel();
            lab_connectS = new Label();
            lab_connect_swing = new Label();
            panel4 = new Panel();
            btn_Saw = new Button();
            btn_Sawband = new Button();
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
            panel5 = new Panel();
            lab_connectrecord = new Label();
            lab_IP_Port1 = new Label();
            lab_connect_1 = new Label();
            panel2 = new Panel();
            btn_Drill_Info = new Button();
            btn_machInfo = new Button();
            panel_Swing = new Panel();
            btn_toggle = new Button();
            lab_Db_update = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            lab_today_ratio1 = new Label();
            lab_today1 = new Label();
            lab_yesterday_ratio1 = new Label();
            lab_yesterday1 = new Label();
            lab_this_ratio1 = new Label();
            lab_thisweek1 = new Label();
            lab_last_ratio1 = new Label();
            lab_lastweek1 = new Label();
            panel_Drill = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            lab_today_ratio = new Label();
            lab_today = new Label();
            lab_yesterday_ratio = new Label();
            lab_yesterday = new Label();
            lab_this_ratio = new Label();
            lab_thisweek = new Label();
            lab_last_ratio = new Label();
            lab_lastweek = new Label();
            lab_vision = new Label();
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
            panel_Swing.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel_Drill.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
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
            panel12.Location = new Point(463, 322);
            panel12.Name = "panel12";
            panel12.Size = new Size(385, 296);
            panel12.TabIndex = 48;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ButtonHighlight;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(panel6);
            panel3.Controls.Add(lab_connectrecord1);
            panel3.Controls.Add(lab_IP_Port2);
            panel3.Controls.Add(lab_connect_2);
            panel3.Location = new Point(21, 218);
            panel3.Name = "panel3";
            panel3.Size = new Size(216, 73);
            panel3.TabIndex = 57;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel6.BackgroundImage = Properties.Resources.圖片2;
            panel6.BackgroundImageLayout = ImageLayout.Stretch;
            panel6.Location = new Point(185, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(26, 33);
            panel6.TabIndex = 48;
            panel6.Click += panel6_Click;
            // 
            // lab_connectrecord1
            // 
            lab_connectrecord1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_connectrecord1.AutoSize = true;
            lab_connectrecord1.BackColor = SystemColors.ButtonHighlight;
            lab_connectrecord1.Font = new Font("Microsoft JhengHei UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connectrecord1.Location = new Point(4, 37);
            lab_connectrecord1.Name = "lab_connectrecord1";
            lab_connectrecord1.Size = new Size(128, 14);
            lab_connectrecord1.TabIndex = 47;
            lab_connectrecord1.Text = "斷線時間or重新連線時間";
            lab_connectrecord1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_IP_Port2
            // 
            lab_IP_Port2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_IP_Port2.AutoSize = true;
            lab_IP_Port2.BackColor = SystemColors.ButtonHighlight;
            lab_IP_Port2.Font = new Font("Microsoft JhengHei UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_IP_Port2.Location = new Point(4, 4);
            lab_IP_Port2.Name = "lab_IP_Port2";
            lab_IP_Port2.Size = new Size(192, 14);
            lab_IP_Port2.TabIndex = 44;
            lab_IP_Port2.Text = "Auto connection is not configured.";
            lab_IP_Port2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_connect_2
            // 
            lab_connect_2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_connect_2.AutoSize = true;
            lab_connect_2.BackColor = SystemColors.ButtonHighlight;
            lab_connect_2.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connect_2.Location = new Point(2, 18);
            lab_connect_2.Name = "lab_connect_2";
            lab_connect_2.Size = new Size(144, 19);
            lab_connect_2.TabIndex = 43;
            lab_connect_2.Text = "是否連線的顯示設定";
            lab_connect_2.TextAlign = ContentAlignment.MiddleCenter;
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
            lab_sumS.ForeColor = Color.FromArgb(51, 51, 51);
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
            lab_sum_swing.ForeColor = Color.FromArgb(51, 51, 51);
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
            lab_connectS.ForeColor = Color.FromArgb(51, 51, 51);
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
            lab_connect_swing.ForeColor = Color.FromArgb(51, 51, 51);
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
            panel4.Controls.Add(btn_Saw);
            panel4.Controls.Add(btn_Sawband);
            panel4.Location = new Point(253, 218);
            panel4.Name = "panel4";
            panel4.Size = new Size(103, 67);
            panel4.TabIndex = 48;
            // 
            // btn_Saw
            // 
            btn_Saw.BackColor = SystemColors.ButtonHighlight;
            btn_Saw.FlatStyle = FlatStyle.Flat;
            btn_Saw.Location = new Point(54, 3);
            btn_Saw.Margin = new Padding(4);
            btn_Saw.Name = "btn_Saw";
            btn_Saw.Size = new Size(45, 59);
            btn_Saw.TabIndex = 55;
            btn_Saw.Text = "鋸床";
            btn_Saw.UseVisualStyleBackColor = false;
            btn_Saw.Click += btn_saw_Click;
            // 
            // btn_Sawband
            // 
            btn_Sawband.BackColor = SystemColors.ButtonHighlight;
            btn_Sawband.FlatStyle = FlatStyle.Flat;
            btn_Sawband.Location = new Point(2, 3);
            btn_Sawband.Margin = new Padding(4);
            btn_Sawband.Name = "btn_Sawband";
            btn_Sawband.Size = new Size(51, 59);
            btn_Sawband.TabIndex = 54;
            btn_Sawband.Text = "鋸帶";
            btn_Sawband.UseVisualStyleBackColor = false;
            btn_Sawband.Click += btn_SawBand_Click;
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
            lab_rS.ForeColor = Color.FromArgb(211, 47, 47);
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
            lab_red_swing.ForeColor = Color.FromArgb(211, 47, 47);
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
            lab_disS.ForeColor = Color.FromArgb(229, 57, 53);
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
            lab_disconnect_sawing.ForeColor = Color.FromArgb(229, 57, 53);
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
            lab_gS.ForeColor = Color.FromArgb(67, 160, 71);
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
            lab_green_swing.ForeColor = Color.FromArgb(67, 160, 71);
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
            lab_yS.ForeColor = Color.FromArgb(251, 192, 45);
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
            lab_yellow_swing.ForeColor = Color.FromArgb(251, 192, 45);
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
            panel11.Location = new Point(47, 321);
            panel11.Name = "panel11";
            panel11.Size = new Size(385, 297);
            panel11.TabIndex = 49;
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
            lab_sumD.ForeColor = Color.FromArgb(51, 51, 51);
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
            lab_sum.ForeColor = Color.FromArgb(51, 51, 51);
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
            lab_connectD.ForeColor = Color.FromArgb(51, 51, 51);
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
            lab_connect.ForeColor = Color.FromArgb(51, 51, 51);
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
            lab_rD.ForeColor = Color.FromArgb(211, 47, 47);
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
            lab_red.ForeColor = Color.FromArgb(211, 47, 47);
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
            lab_disD.ForeColor = Color.FromArgb(229, 57, 53);
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
            lab_disconnect.ForeColor = Color.FromArgb(229, 57, 53);
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
            lab_gD.ForeColor = Color.FromArgb(67, 160, 71);
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
            lab_green.ForeColor = Color.FromArgb(67, 160, 71);
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
            lab_yD.ForeColor = Color.FromArgb(251, 192, 45);
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
            lab_yellow.ForeColor = Color.FromArgb(251, 192, 45);
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
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(lab_connectrecord);
            panel1.Controls.Add(lab_IP_Port1);
            panel1.Controls.Add(lab_connect_1);
            panel1.Location = new Point(21, 219);
            panel1.Name = "panel1";
            panel1.Size = new Size(216, 73);
            panel1.TabIndex = 41;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel5.BackgroundImage = Properties.Resources.圖片2;
            panel5.BackgroundImageLayout = ImageLayout.Stretch;
            panel5.Location = new Point(184, 4);
            panel5.Name = "panel5";
            panel5.Size = new Size(26, 33);
            panel5.TabIndex = 2;
            panel5.Click += panel5_Click;
            // 
            // lab_connectrecord
            // 
            lab_connectrecord.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_connectrecord.AutoSize = true;
            lab_connectrecord.BackColor = SystemColors.ButtonHighlight;
            lab_connectrecord.Font = new Font("Microsoft JhengHei UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connectrecord.Location = new Point(4, 40);
            lab_connectrecord.Name = "lab_connectrecord";
            lab_connectrecord.Size = new Size(128, 14);
            lab_connectrecord.TabIndex = 46;
            lab_connectrecord.Text = "斷線時間or重新連線時間";
            lab_connectrecord.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_IP_Port1
            // 
            lab_IP_Port1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_IP_Port1.AutoSize = true;
            lab_IP_Port1.BackColor = SystemColors.ButtonHighlight;
            lab_IP_Port1.Font = new Font("Microsoft JhengHei UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_IP_Port1.Location = new Point(5, 4);
            lab_IP_Port1.Name = "lab_IP_Port1";
            lab_IP_Port1.Size = new Size(95, 14);
            lab_IP_Port1.TabIndex = 45;
            lab_IP_Port1.Text = "尚未設定自動連線";
            lab_IP_Port1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_connect_1
            // 
            lab_connect_1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_connect_1.AutoSize = true;
            lab_connect_1.BackColor = SystemColors.ButtonHighlight;
            lab_connect_1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connect_1.Location = new Point(2, 18);
            lab_connect_1.Name = "lab_connect_1";
            lab_connect_1.Size = new Size(144, 19);
            lab_connect_1.TabIndex = 43;
            lab_connect_1.Text = "是否連線的顯示設定";
            lab_connect_1.TextAlign = ContentAlignment.MiddleCenter;
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
            btn_Drill_Info.Location = new Point(2, 2);
            btn_Drill_Info.Margin = new Padding(4);
            btn_Drill_Info.Name = "btn_Drill_Info";
            btn_Drill_Info.Size = new Size(96, 50);
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
            // panel_Swing
            // 
            panel_Swing.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_Swing.BackColor = SystemColors.ButtonHighlight;
            panel_Swing.BackgroundImage = Properties.Resources.Swaing;
            panel_Swing.BackgroundImageLayout = ImageLayout.Center;
            panel_Swing.BorderStyle = BorderStyle.FixedSingle;
            panel_Swing.Controls.Add(btn_toggle);
            panel_Swing.Controls.Add(lab_Db_update);
            panel_Swing.Controls.Add(tableLayoutPanel2);
            panel_Swing.Location = new Point(463, 12);
            panel_Swing.Name = "panel_Swing";
            panel_Swing.Size = new Size(385, 297);
            panel_Swing.TabIndex = 51;
            // 
            // btn_toggle
            // 
            btn_toggle.FlatStyle = FlatStyle.Flat;
            btn_toggle.Location = new Point(6, 8);
            btn_toggle.Name = "btn_toggle";
            btn_toggle.Size = new Size(21, 20);
            btn_toggle.TabIndex = 56;
            btn_toggle.UseVisualStyleBackColor = false;
            btn_toggle.Click += btn_toggle_Click_1;
            // 
            // lab_Db_update
            // 
            lab_Db_update.AutoSize = true;
            lab_Db_update.Location = new Point(34, 11);
            lab_Db_update.Name = "lab_Db_update";
            lab_Db_update.Size = new Size(0, 15);
            lab_Db_update.TabIndex = 57;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.36709F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.63291F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.2405052F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.7594948F));
            tableLayoutPanel2.Controls.Add(lab_today_ratio1, 1, 3);
            tableLayoutPanel2.Controls.Add(lab_today1, 0, 3);
            tableLayoutPanel2.Controls.Add(lab_yesterday_ratio1, 1, 2);
            tableLayoutPanel2.Controls.Add(lab_yesterday1, 0, 2);
            tableLayoutPanel2.Controls.Add(lab_this_ratio1, 1, 1);
            tableLayoutPanel2.Controls.Add(lab_thisweek1, 0, 1);
            tableLayoutPanel2.Controls.Add(lab_last_ratio1, 1, 0);
            tableLayoutPanel2.Controls.Add(lab_lastweek1, 0, 0);
            tableLayoutPanel2.Location = new Point(226, 213);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(158, 83);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // lab_today_ratio1
            // 
            lab_today_ratio1.BackColor = Color.Transparent;
            lab_today_ratio1.Dock = DockStyle.Fill;
            lab_today_ratio1.Font = new Font("微軟正黑體", 9.75F);
            lab_today_ratio1.ForeColor = SystemColors.ActiveCaptionText;
            lab_today_ratio1.Location = new Point(81, 62);
            lab_today_ratio1.Name = "lab_today_ratio1";
            lab_today_ratio1.Size = new Size(74, 21);
            lab_today_ratio1.TabIndex = 15;
            lab_today_ratio1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_today1
            // 
            lab_today1.BackColor = Color.Transparent;
            lab_today1.Dock = DockStyle.Fill;
            lab_today1.Font = new Font("微軟正黑體", 9.75F);
            lab_today1.ForeColor = SystemColors.ActiveCaptionText;
            lab_today1.Location = new Point(3, 62);
            lab_today1.Name = "lab_today1";
            lab_today1.Size = new Size(72, 21);
            lab_today1.TabIndex = 14;
            lab_today1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_yesterday_ratio1
            // 
            lab_yesterday_ratio1.BackColor = Color.Transparent;
            lab_yesterday_ratio1.Dock = DockStyle.Fill;
            lab_yesterday_ratio1.Font = new Font("微軟正黑體", 9.75F);
            lab_yesterday_ratio1.ForeColor = SystemColors.ActiveCaptionText;
            lab_yesterday_ratio1.Location = new Point(81, 42);
            lab_yesterday_ratio1.Name = "lab_yesterday_ratio1";
            lab_yesterday_ratio1.Size = new Size(74, 20);
            lab_yesterday_ratio1.TabIndex = 13;
            lab_yesterday_ratio1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_yesterday1
            // 
            lab_yesterday1.BackColor = Color.Transparent;
            lab_yesterday1.Dock = DockStyle.Fill;
            lab_yesterday1.Font = new Font("微軟正黑體", 9.75F);
            lab_yesterday1.ForeColor = SystemColors.ActiveCaptionText;
            lab_yesterday1.Location = new Point(3, 42);
            lab_yesterday1.Name = "lab_yesterday1";
            lab_yesterday1.Size = new Size(72, 20);
            lab_yesterday1.TabIndex = 12;
            lab_yesterday1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_this_ratio1
            // 
            lab_this_ratio1.BackColor = Color.Transparent;
            lab_this_ratio1.Dock = DockStyle.Fill;
            lab_this_ratio1.Font = new Font("微軟正黑體", 9.75F);
            lab_this_ratio1.ForeColor = SystemColors.ActiveCaptionText;
            lab_this_ratio1.Location = new Point(81, 21);
            lab_this_ratio1.Name = "lab_this_ratio1";
            lab_this_ratio1.Size = new Size(74, 21);
            lab_this_ratio1.TabIndex = 11;
            lab_this_ratio1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_thisweek1
            // 
            lab_thisweek1.BackColor = Color.Transparent;
            lab_thisweek1.Dock = DockStyle.Fill;
            lab_thisweek1.Font = new Font("微軟正黑體", 9.75F);
            lab_thisweek1.ForeColor = SystemColors.ActiveCaptionText;
            lab_thisweek1.Location = new Point(3, 21);
            lab_thisweek1.Name = "lab_thisweek1";
            lab_thisweek1.Size = new Size(72, 21);
            lab_thisweek1.TabIndex = 10;
            lab_thisweek1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_last_ratio1
            // 
            lab_last_ratio1.BackColor = Color.Transparent;
            lab_last_ratio1.Dock = DockStyle.Fill;
            lab_last_ratio1.Font = new Font("微軟正黑體", 9.75F);
            lab_last_ratio1.ForeColor = SystemColors.ActiveCaptionText;
            lab_last_ratio1.Location = new Point(81, 0);
            lab_last_ratio1.Name = "lab_last_ratio1";
            lab_last_ratio1.Size = new Size(74, 21);
            lab_last_ratio1.TabIndex = 9;
            lab_last_ratio1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_lastweek1
            // 
            lab_lastweek1.BackColor = Color.Transparent;
            lab_lastweek1.Dock = DockStyle.Fill;
            lab_lastweek1.Font = new Font("微軟正黑體", 9.75F);
            lab_lastweek1.ForeColor = SystemColors.ActiveCaptionText;
            lab_lastweek1.Location = new Point(3, 0);
            lab_lastweek1.Name = "lab_lastweek1";
            lab_lastweek1.Size = new Size(72, 21);
            lab_lastweek1.TabIndex = 8;
            lab_lastweek1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel_Drill
            // 
            panel_Drill.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_Drill.BackColor = SystemColors.ButtonHighlight;
            panel_Drill.BackgroundImage = Properties.Resources.Drill;
            panel_Drill.BackgroundImageLayout = ImageLayout.Zoom;
            panel_Drill.BorderStyle = BorderStyle.FixedSingle;
            panel_Drill.Controls.Add(tableLayoutPanel1);
            panel_Drill.Location = new Point(47, 12);
            panel_Drill.Name = "panel_Drill";
            panel_Drill.Size = new Size(385, 297);
            panel_Drill.TabIndex = 47;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.16456F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.83544F));
            tableLayoutPanel1.Controls.Add(lab_today_ratio, 1, 3);
            tableLayoutPanel1.Controls.Add(lab_today, 0, 3);
            tableLayoutPanel1.Controls.Add(lab_yesterday_ratio, 1, 2);
            tableLayoutPanel1.Controls.Add(lab_yesterday, 0, 2);
            tableLayoutPanel1.Controls.Add(lab_this_ratio, 1, 1);
            tableLayoutPanel1.Controls.Add(lab_thisweek, 0, 1);
            tableLayoutPanel1.Controls.Add(lab_last_ratio, 1, 0);
            tableLayoutPanel1.Controls.Add(lab_lastweek, 0, 0);
            tableLayoutPanel1.Location = new Point(222, 209);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(158, 83);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // lab_today_ratio
            // 
            lab_today_ratio.BackColor = Color.Transparent;
            lab_today_ratio.Dock = DockStyle.Fill;
            lab_today_ratio.Font = new Font("微軟正黑體", 9.75F);
            lab_today_ratio.ForeColor = SystemColors.ActiveCaptionText;
            lab_today_ratio.Location = new Point(87, 62);
            lab_today_ratio.Name = "lab_today_ratio";
            lab_today_ratio.Size = new Size(68, 21);
            lab_today_ratio.TabIndex = 14;
            lab_today_ratio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_today
            // 
            lab_today.BackColor = Color.Transparent;
            lab_today.Dock = DockStyle.Fill;
            lab_today.Font = new Font("微軟正黑體", 9.75F);
            lab_today.ForeColor = SystemColors.ActiveCaptionText;
            lab_today.Location = new Point(3, 62);
            lab_today.Name = "lab_today";
            lab_today.Size = new Size(78, 21);
            lab_today.TabIndex = 13;
            lab_today.Text = "今日";
            lab_today.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_yesterday_ratio
            // 
            lab_yesterday_ratio.BackColor = Color.Transparent;
            lab_yesterday_ratio.Dock = DockStyle.Fill;
            lab_yesterday_ratio.Font = new Font("微軟正黑體", 9.75F);
            lab_yesterday_ratio.ForeColor = SystemColors.ActiveCaptionText;
            lab_yesterday_ratio.Location = new Point(87, 42);
            lab_yesterday_ratio.Name = "lab_yesterday_ratio";
            lab_yesterday_ratio.Size = new Size(68, 20);
            lab_yesterday_ratio.TabIndex = 12;
            lab_yesterday_ratio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_yesterday
            // 
            lab_yesterday.BackColor = Color.Transparent;
            lab_yesterday.Dock = DockStyle.Fill;
            lab_yesterday.Font = new Font("微軟正黑體", 9.75F);
            lab_yesterday.ForeColor = SystemColors.ActiveCaptionText;
            lab_yesterday.Location = new Point(3, 42);
            lab_yesterday.Name = "lab_yesterday";
            lab_yesterday.Size = new Size(78, 20);
            lab_yesterday.TabIndex = 11;
            lab_yesterday.Text = "昨日:";
            lab_yesterday.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_this_ratio
            // 
            lab_this_ratio.BackColor = Color.Transparent;
            lab_this_ratio.Dock = DockStyle.Fill;
            lab_this_ratio.Font = new Font("微軟正黑體", 9.75F);
            lab_this_ratio.ForeColor = SystemColors.ActiveCaptionText;
            lab_this_ratio.Location = new Point(87, 21);
            lab_this_ratio.Name = "lab_this_ratio";
            lab_this_ratio.Size = new Size(68, 21);
            lab_this_ratio.TabIndex = 10;
            lab_this_ratio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_thisweek
            // 
            lab_thisweek.BackColor = Color.Transparent;
            lab_thisweek.Dock = DockStyle.Fill;
            lab_thisweek.Font = new Font("微軟正黑體", 9.75F);
            lab_thisweek.ForeColor = SystemColors.ActiveCaptionText;
            lab_thisweek.Location = new Point(3, 21);
            lab_thisweek.Name = "lab_thisweek";
            lab_thisweek.Size = new Size(78, 21);
            lab_thisweek.TabIndex = 9;
            lab_thisweek.Text = "這週:";
            lab_thisweek.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_last_ratio
            // 
            lab_last_ratio.BackColor = Color.Transparent;
            lab_last_ratio.Dock = DockStyle.Fill;
            lab_last_ratio.Font = new Font("微軟正黑體", 9.75F);
            lab_last_ratio.ForeColor = SystemColors.ActiveCaptionText;
            lab_last_ratio.Location = new Point(87, 0);
            lab_last_ratio.Name = "lab_last_ratio";
            lab_last_ratio.Size = new Size(68, 21);
            lab_last_ratio.TabIndex = 8;
            lab_last_ratio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_lastweek
            // 
            lab_lastweek.BackColor = Color.Transparent;
            lab_lastweek.Dock = DockStyle.Fill;
            lab_lastweek.Font = new Font("微軟正黑體", 9.75F);
            lab_lastweek.ForeColor = SystemColors.ActiveCaptionText;
            lab_lastweek.Location = new Point(3, 0);
            lab_lastweek.Name = "lab_lastweek";
            lab_lastweek.Size = new Size(78, 21);
            lab_lastweek.TabIndex = 7;
            lab_lastweek.Text = "上週:";
            lab_lastweek.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_vision
            // 
            lab_vision.AutoSize = true;
            lab_vision.Location = new Point(47, 621);
            lab_vision.Name = "lab_vision";
            lab_vision.Size = new Size(42, 15);
            lab_vision.TabIndex = 52;
            lab_vision.Text = "V1.0.1";
            // 
            // Home_Page
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(956, 639);
            Controls.Add(lab_vision);
            Controls.Add(panel12);
            Controls.Add(panel11);
            Controls.Add(panel_Swing);
            Controls.Add(panel_Drill);
            Margin = new Padding(4);
            Name = "Home_Page";
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
            panel_Swing.ResumeLayout(false);
            panel_Swing.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            panel_Drill.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_Sawband;
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
        private Label lab_connect_1;
        private Panel panel2;
        private Button btn_machInfo;
        private Panel panel_Swing;
        private Panel panel_Drill;
        private Button btn_Drill_Info;
        private Panel panel3;
        private Label lab_IP_Port2;
        private Label lab_IP_Port1;
        private Button btn_Saw;
        private Button btn_toggle;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lab_today_ratio;
        private Label lab_today;
        private Label lab_yesterday_ratio;
        private Label lab_yesterday;
        private Label lab_this_ratio;
        private Label lab_thisweek;
        private Label lab_last_ratio;
        private Label lab_lastweek;
        private Label lab_today_ratio1;
        private Label lab_today1;
        private Label lab_yesterday_ratio1;
        private Label lab_yesterday1;
        private Label lab_this_ratio1;
        private Label lab_thisweek1;
        private Label lab_last_ratio1;
        private Label lab_lastweek1;
        private Label lab_connectrecord;
        private Label lab_connect_2;
        private Panel panel5;
        private Panel panel6;
        private Label lab_connectrecord1;
        private Label lab_Db_update;
        private Label lab_vision;
    }
}

