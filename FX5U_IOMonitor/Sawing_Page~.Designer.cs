namespace FX5U_IOMonitor
{
    partial class Sawing_Page
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
            panel_Swing = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            lab_today_ratio1 = new Label();
            lab_today1 = new Label();
            lab_yesterday_ratio1 = new Label();
            lab_yesterday1 = new Label();
            lab_this_ratio1 = new Label();
            lab_thisweek1 = new Label();
            lab_last_ratio1 = new Label();
            lab_lastweek1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel5 = new Panel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label4 = new Label();
            label5 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            label6 = new Label();
            label7 = new Label();
            panel7 = new Panel();
            button1 = new Button();
            button2 = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            label8 = new Label();
            label9 = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            label10 = new Label();
            label11 = new Label();
            tableLayoutPanel6 = new TableLayoutPanel();
            label12 = new Label();
            label13 = new Label();
            tableLayoutPanel7 = new TableLayoutPanel();
            label14 = new Label();
            label15 = new Label();
            panel8 = new Panel();
            tableLayoutPanel8 = new TableLayoutPanel();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            label19 = new Label();
            label20 = new Label();
            label21 = new Label();
            label22 = new Label();
            label23 = new Label();
            panel12.SuspendLayout();
            panel3.SuspendLayout();
            tbPanel_Swing_sum.SuspendLayout();
            tbPanel_Swing_connect.SuspendLayout();
            panel4.SuspendLayout();
            tbPanel_Swing_red.SuspendLayout();
            tbPanel_Swing_disconnect.SuspendLayout();
            tbPanel_Swing_green.SuspendLayout();
            tbPanel_Swing_yellow.SuspendLayout();
            panel_Swing.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel7.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            panel8.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
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
            panel12.Location = new Point(86, 322);
            panel12.Name = "panel12";
            panel12.Size = new Size(364, 296);
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
            // panel_Swing
            // 
            panel_Swing.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_Swing.BackColor = SystemColors.ButtonHighlight;
            panel_Swing.BackgroundImage = Properties.Resources.Swaing;
            panel_Swing.BackgroundImageLayout = ImageLayout.Center;
            panel_Swing.BorderStyle = BorderStyle.FixedSingle;
            panel_Swing.Controls.Add(tableLayoutPanel2);
            panel_Swing.Location = new Point(86, 12);
            panel_Swing.Name = "panel_Swing";
            panel_Swing.Size = new Size(364, 297);
            panel_Swing.TabIndex = 51;
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
            tableLayoutPanel2.Location = new Point(205, 213);
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
            // panel1
            // 
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(tableLayoutPanel3);
            panel1.Controls.Add(panel7);
            panel1.Controls.Add(tableLayoutPanel4);
            panel1.Controls.Add(tableLayoutPanel5);
            panel1.Controls.Add(tableLayoutPanel6);
            panel1.Controls.Add(tableLayoutPanel7);
            panel1.Location = new Point(468, 322);
            panel1.Name = "panel1";
            panel1.Size = new Size(364, 296);
            panel1.TabIndex = 58;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonHighlight;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label3);
            panel2.Location = new Point(21, 218);
            panel2.Name = "panel2";
            panel2.Size = new Size(226, 73);
            panel2.TabIndex = 57;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel5.BackgroundImage = Properties.Resources.圖片2;
            panel5.BackgroundImageLayout = ImageLayout.Stretch;
            panel5.Location = new Point(195, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(26, 33);
            panel5.TabIndex = 48;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonHighlight;
            label1.Font = new Font("Microsoft JhengHei UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(4, 37);
            label1.Name = "label1";
            label1.Size = new Size(128, 14);
            label1.TabIndex = 47;
            label1.Text = "斷線時間or重新連線時間";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.Font = new Font("Microsoft JhengHei UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label2.Location = new Point(4, 4);
            label2.Name = "label2";
            label2.Size = new Size(192, 14);
            label2.TabIndex = 44;
            label2.Text = "Auto connection is not configured.";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonHighlight;
            label3.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label3.Location = new Point(2, 18);
            label3.Name = "label3";
            label3.Size = new Size(144, 19);
            label3.TabIndex = 43;
            label3.Text = "是否連線的顯示設定";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label4, 0, 1);
            tableLayoutPanel1.Controls.Add(label5, 0, 0);
            tableLayoutPanel1.Location = new Point(21, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.1074562F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 47.8925438F));
            tableLayoutPanel1.Size = new Size(100, 90);
            tableLayoutPanel1.TabIndex = 33;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ButtonHighlight;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label4.ForeColor = Color.FromArgb(51, 51, 51);
            label4.Location = new Point(3, 45);
            label4.Name = "label4";
            label4.Size = new Size(92, 43);
            label4.TabIndex = 23;
            label4.Text = "監控總數";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ButtonHighlight;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label5.ForeColor = Color.FromArgb(51, 51, 51);
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(92, 45);
            label5.TabIndex = 21;
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel3.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(label6, 0, 1);
            tableLayoutPanel3.Controls.Add(label7, 0, 0);
            tableLayoutPanel3.Location = new Point(137, 13);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(100, 90);
            tableLayoutPanel3.TabIndex = 35;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ButtonHighlight;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label6.ForeColor = Color.FromArgb(51, 51, 51);
            label6.Location = new Point(3, 44);
            label6.Name = "label6";
            label6.Size = new Size(92, 44);
            label6.TabIndex = 23;
            label6.Text = "連接總數";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.ButtonHighlight;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label7.ForeColor = Color.FromArgb(51, 51, 51);
            label7.Location = new Point(3, 0);
            label7.Name = "label7";
            label7.Size = new Size(92, 44);
            label7.TabIndex = 21;
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            panel7.BackColor = SystemColors.ButtonHighlight;
            panel7.BorderStyle = BorderStyle.FixedSingle;
            panel7.Controls.Add(button1);
            panel7.Controls.Add(button2);
            panel7.Location = new Point(253, 218);
            panel7.Name = "panel7";
            panel7.Size = new Size(103, 73);
            panel7.TabIndex = 48;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonHighlight;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(56, 7);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(45, 59);
            button1.TabIndex = 55;
            button1.Text = "鋸床";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ButtonHighlight;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(2, 7);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(51, 59);
            button2.TabIndex = 54;
            button2.Text = "鋸帶";
            button2.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel4.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(label8, 0, 1);
            tableLayoutPanel4.Controls.Add(label9, 0, 0);
            tableLayoutPanel4.Location = new Point(256, 115);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 52.1074562F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 47.8925438F));
            tableLayoutPanel4.Size = new Size(100, 90);
            tableLayoutPanel4.TabIndex = 34;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = SystemColors.ButtonHighlight;
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label8.ForeColor = Color.FromArgb(211, 47, 47);
            label8.Location = new Point(3, 45);
            label8.Name = "label8";
            label8.Size = new Size(92, 43);
            label8.TabIndex = 22;
            label8.Text = "紅燈總數";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.ButtonHighlight;
            label9.Dock = DockStyle.Fill;
            label9.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label9.ForeColor = Color.FromArgb(211, 47, 47);
            label9.Location = new Point(3, 0);
            label9.Name = "label9";
            label9.Size = new Size(92, 45);
            label9.TabIndex = 21;
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel5.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(label10, 0, 1);
            tableLayoutPanel5.Controls.Add(label11, 0, 0);
            tableLayoutPanel5.Location = new Point(256, 12);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 53.4090919F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 46.5909081F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel5.Size = new Size(100, 90);
            tableLayoutPanel5.TabIndex = 36;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = SystemColors.ButtonHighlight;
            label10.Dock = DockStyle.Fill;
            label10.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label10.ForeColor = Color.FromArgb(229, 57, 53);
            label10.Location = new Point(3, 47);
            label10.Name = "label10";
            label10.Size = new Size(92, 41);
            label10.TabIndex = 23;
            label10.Text = "元件異常";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = SystemColors.ButtonHighlight;
            label11.Dock = DockStyle.Fill;
            label11.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label11.ForeColor = Color.FromArgb(229, 57, 53);
            label11.Location = new Point(3, 0);
            label11.Name = "label11";
            label11.Size = new Size(92, 47);
            label11.TabIndex = 21;
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel6.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(label12, 0, 1);
            tableLayoutPanel6.Controls.Add(label13, 0, 0);
            tableLayoutPanel6.Location = new Point(21, 115);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 52.655674F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 47.34432F));
            tableLayoutPanel6.Size = new Size(100, 90);
            tableLayoutPanel6.TabIndex = 37;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = SystemColors.ButtonHighlight;
            label12.Dock = DockStyle.Fill;
            label12.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label12.ForeColor = Color.FromArgb(67, 160, 71);
            label12.Location = new Point(3, 46);
            label12.Name = "label12";
            label12.Size = new Size(92, 42);
            label12.TabIndex = 23;
            label12.Text = "綠燈總數";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = SystemColors.ButtonHighlight;
            label13.Dock = DockStyle.Fill;
            label13.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label13.ForeColor = Color.FromArgb(67, 160, 71);
            label13.Location = new Point(3, 0);
            label13.Name = "label13";
            label13.Size = new Size(92, 46);
            label13.TabIndex = 21;
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel7.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(label14, 0, 1);
            tableLayoutPanel7.Controls.Add(label15, 0, 0);
            tableLayoutPanel7.Location = new Point(137, 116);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 2;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 52.655674F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 47.34432F));
            tableLayoutPanel7.Size = new Size(100, 90);
            tableLayoutPanel7.TabIndex = 38;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = SystemColors.ButtonHighlight;
            label14.Dock = DockStyle.Fill;
            label14.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label14.ForeColor = Color.FromArgb(251, 192, 45);
            label14.Location = new Point(3, 46);
            label14.Name = "label14";
            label14.Size = new Size(92, 42);
            label14.TabIndex = 23;
            label14.Text = "黃燈總數";
            label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = SystemColors.ButtonHighlight;
            label15.Dock = DockStyle.Fill;
            label15.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label15.ForeColor = Color.FromArgb(251, 192, 45);
            label15.Location = new Point(3, 0);
            label15.Name = "label15";
            label15.Size = new Size(92, 46);
            label15.TabIndex = 21;
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel8
            // 
            panel8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel8.BackColor = SystemColors.ButtonHighlight;
            panel8.BackgroundImage = Properties.Resources.Swaing;
            panel8.BackgroundImageLayout = ImageLayout.Center;
            panel8.BorderStyle = BorderStyle.FixedSingle;
            panel8.Controls.Add(tableLayoutPanel8);
            panel8.Location = new Point(468, 12);
            panel8.Name = "panel8";
            panel8.Size = new Size(364, 297);
            panel8.TabIndex = 59;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            tableLayoutPanel8.ColumnCount = 2;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.36709F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.63291F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.2405052F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.7594948F));
            tableLayoutPanel8.Controls.Add(label16, 1, 3);
            tableLayoutPanel8.Controls.Add(label17, 0, 3);
            tableLayoutPanel8.Controls.Add(label18, 1, 2);
            tableLayoutPanel8.Controls.Add(label19, 0, 2);
            tableLayoutPanel8.Controls.Add(label20, 1, 1);
            tableLayoutPanel8.Controls.Add(label21, 0, 1);
            tableLayoutPanel8.Controls.Add(label22, 1, 0);
            tableLayoutPanel8.Controls.Add(label23, 0, 0);
            tableLayoutPanel8.Location = new Point(367, 408);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 4;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel8.Size = new Size(158, 83);
            tableLayoutPanel8.TabIndex = 2;
            // 
            // label16
            // 
            label16.BackColor = Color.Transparent;
            label16.Dock = DockStyle.Fill;
            label16.Font = new Font("微軟正黑體", 9.75F);
            label16.ForeColor = SystemColors.ActiveCaptionText;
            label16.Location = new Point(81, 62);
            label16.Name = "label16";
            label16.Size = new Size(74, 21);
            label16.TabIndex = 15;
            label16.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            label17.BackColor = Color.Transparent;
            label17.Dock = DockStyle.Fill;
            label17.Font = new Font("微軟正黑體", 9.75F);
            label17.ForeColor = SystemColors.ActiveCaptionText;
            label17.Location = new Point(3, 62);
            label17.Name = "label17";
            label17.Size = new Size(72, 21);
            label17.TabIndex = 14;
            label17.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            label18.BackColor = Color.Transparent;
            label18.Dock = DockStyle.Fill;
            label18.Font = new Font("微軟正黑體", 9.75F);
            label18.ForeColor = SystemColors.ActiveCaptionText;
            label18.Location = new Point(81, 42);
            label18.Name = "label18";
            label18.Size = new Size(74, 20);
            label18.TabIndex = 13;
            label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            label19.BackColor = Color.Transparent;
            label19.Dock = DockStyle.Fill;
            label19.Font = new Font("微軟正黑體", 9.75F);
            label19.ForeColor = SystemColors.ActiveCaptionText;
            label19.Location = new Point(3, 42);
            label19.Name = "label19";
            label19.Size = new Size(72, 20);
            label19.TabIndex = 12;
            label19.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            label20.BackColor = Color.Transparent;
            label20.Dock = DockStyle.Fill;
            label20.Font = new Font("微軟正黑體", 9.75F);
            label20.ForeColor = SystemColors.ActiveCaptionText;
            label20.Location = new Point(81, 21);
            label20.Name = "label20";
            label20.Size = new Size(74, 21);
            label20.TabIndex = 11;
            label20.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            label21.BackColor = Color.Transparent;
            label21.Dock = DockStyle.Fill;
            label21.Font = new Font("微軟正黑體", 9.75F);
            label21.ForeColor = SystemColors.ActiveCaptionText;
            label21.Location = new Point(3, 21);
            label21.Name = "label21";
            label21.Size = new Size(72, 21);
            label21.TabIndex = 10;
            label21.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            label22.BackColor = Color.Transparent;
            label22.Dock = DockStyle.Fill;
            label22.Font = new Font("微軟正黑體", 9.75F);
            label22.ForeColor = SystemColors.ActiveCaptionText;
            label22.Location = new Point(81, 0);
            label22.Name = "label22";
            label22.Size = new Size(74, 21);
            label22.TabIndex = 9;
            label22.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            label23.BackColor = Color.Transparent;
            label23.Dock = DockStyle.Fill;
            label23.Font = new Font("微軟正黑體", 9.75F);
            label23.ForeColor = SystemColors.ActiveCaptionText;
            label23.Location = new Point(3, 0);
            label23.Name = "label23";
            label23.Size = new Size(72, 21);
            label23.TabIndex = 8;
            label23.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Sawing_Page
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(956, 639);
            Controls.Add(panel1);
            Controls.Add(panel12);
            Controls.Add(panel8);
            Controls.Add(panel_Swing);
            Margin = new Padding(4);
            Name = "Sawing_Page";
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
            panel_Swing.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            panel7.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            panel8.ResumeLayout(false);
            tableLayoutPanel8.ResumeLayout(false);
            ResumeLayout(false);
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
        private Panel panel_Swing;
        private Panel panel3;
        private Label lab_IP_Port2;
        private Button btn_Saw;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lab_today_ratio1;
        private Label lab_today1;
        private Label lab_yesterday_ratio1;
        private Label lab_yesterday1;
        private Label lab_this_ratio1;
        private Label lab_thisweek1;
        private Label lab_last_ratio1;
        private Label lab_lastweek1;
        private Label lab_connect_2;
        private Panel panel6;
        private Label lab_connectrecord1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel5;
        private Label label1;
        private Label label2;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label4;
        private Label label5;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label6;
        private Label label7;
        private Panel panel7;
        private Button button1;
        private Button button2;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label8;
        private Label label9;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label10;
        private Label label11;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label12;
        private Label label13;
        private TableLayoutPanel tableLayoutPanel7;
        private Label label14;
        private Label label15;
        private Panel panel8;
        private TableLayoutPanel tableLayoutPanel8;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
    }
}

