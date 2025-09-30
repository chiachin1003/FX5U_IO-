namespace FX5U_IOMonitor
{
    partial class Home_Page2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home_Page2));
            panel12 = new Panel();
            tbPanel_Swing_connect = new TableLayoutPanel();
            lab_connectS = new Label();
            lab_connect_swing = new Label();
            panel3 = new Panel();
            panel6 = new Panel();
            lab_connectrecord1 = new Label();
            lab_IP_Port2 = new Label();
            lab_connect_2 = new Label();
            panel4 = new Panel();
            btn_Saw = new Button();
            btn_Sawband = new Button();
            tbPanel_Swing_yellow = new TableLayoutPanel();
            lab_yS = new Label();
            lab_yellow_swing = new Label();
            tbPanel_Swing_sum = new TableLayoutPanel();
            lab_sumS = new Label();
            lab_sum_swing = new Label();
            tbPanel_Swing_green = new TableLayoutPanel();
            lab_gS = new Label();
            lab_green_swing = new Label();
            tbPanel_Swing_disconnect = new TableLayoutPanel();
            lab_disS = new Label();
            lab_disconnect_sawing = new Label();
            tbPanel_Swing_red = new TableLayoutPanel();
            lab_rS = new Label();
            lab_red_swing = new Label();
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
            panel1 = new Panel();
            panel5 = new Panel();
            lab_connectrecord = new Label();
            lab_IP_Port1 = new Label();
            lab_connect_1 = new Label();
            panel2 = new Panel();
            btn_Drill_Info = new Button();
            btn_machInfo = new Button();
            tbPanel_Drill_yellow = new TableLayoutPanel();
            lab_yD = new Label();
            lab_yellow = new Label();
            panel_Swing = new Panel();
            pictureBox2 = new PictureBox();
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
            pictureBox1 = new PictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            lab_today_ratio = new Label();
            lab_today = new Label();
            lab_yesterday_ratio = new Label();
            lab_yesterday = new Label();
            lab_this_ratio = new Label();
            lab_thisweek = new Label();
            lab_last_ratio = new Label();
            lab_lastweek = new Label();
            btn_toggle = new Button();
            lb_Last_cloudupdatetime = new Label();
            panel7 = new Panel();
            pictureBox3 = new PictureBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            tableLayoutPanel9 = new TableLayoutPanel();
            label22 = new Label();
            label23 = new Label();
            tableLayoutPanel8 = new TableLayoutPanel();
            label20 = new Label();
            label21 = new Label();
            tableLayoutPanel7 = new TableLayoutPanel();
            label18 = new Label();
            label19 = new Label();
            tableLayoutPanel6 = new TableLayoutPanel();
            label16 = new Label();
            label17 = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            label14 = new Label();
            label15 = new Label();
            panel13 = new Panel();
            button1 = new Button();
            button2 = new Button();
            panel9 = new Panel();
            panel14 = new Panel();
            tableLayoutPanel4 = new TableLayoutPanel();
            label9 = new Label();
            label10 = new Label();
            panel8 = new Panel();
            panel12.SuspendLayout();
            tbPanel_Swing_connect.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            tbPanel_Swing_yellow.SuspendLayout();
            tbPanel_Swing_sum.SuspendLayout();
            tbPanel_Swing_green.SuspendLayout();
            tbPanel_Swing_disconnect.SuspendLayout();
            tbPanel_Swing_red.SuspendLayout();
            panel11.SuspendLayout();
            tbPanel_Drill_sum.SuspendLayout();
            tbPanel_Drill_connect.SuspendLayout();
            tbPanel_Drill_red.SuspendLayout();
            tbPanel_Drill_disconnect.SuspendLayout();
            tbPanel_Drill_green.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            tbPanel_Drill_yellow.SuspendLayout();
            panel_Swing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            panel_Drill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            panel13.SuspendLayout();
            panel9.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // panel12
            // 
            panel12.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel12.BackColor = SystemColors.ButtonHighlight;
            panel12.BorderStyle = BorderStyle.FixedSingle;
            panel12.Controls.Add(tbPanel_Swing_connect);
            panel12.Controls.Add(panel3);
            panel12.Controls.Add(panel4);
            panel12.Controls.Add(tbPanel_Swing_yellow);
            panel12.Controls.Add(tbPanel_Swing_sum);
            panel12.Controls.Add(tbPanel_Swing_green);
            panel12.Controls.Add(tbPanel_Swing_disconnect);
            panel12.Controls.Add(tbPanel_Swing_red);
            panel12.Location = new Point(542, 343);
            panel12.Margin = new Padding(5);
            panel12.Name = "panel12";
            panel12.Size = new Size(430, 477);
            panel12.TabIndex = 48;
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
            tbPanel_Swing_connect.Location = new Point(152, 14);
            tbPanel_Swing_connect.Margin = new Padding(5);
            tbPanel_Swing_connect.Name = "tbPanel_Swing_connect";
            tbPanel_Swing_connect.RowCount = 2;
            tbPanel_Swing_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tbPanel_Swing_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tbPanel_Swing_connect.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tbPanel_Swing_connect.Size = new Size(123, 118);
            tbPanel_Swing_connect.TabIndex = 35;
            // 
            // lab_connectS
            // 
            lab_connectS.AutoSize = true;
            lab_connectS.BackColor = SystemColors.ButtonHighlight;
            lab_connectS.Dock = DockStyle.Fill;
            lab_connectS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connectS.ForeColor = Color.FromArgb(51, 51, 51);
            lab_connectS.Location = new Point(5, 58);
            lab_connectS.Margin = new Padding(5, 0, 5, 0);
            lab_connectS.Name = "lab_connectS";
            lab_connectS.Size = new Size(111, 58);
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
            lab_connect_swing.Location = new Point(5, 0);
            lab_connect_swing.Margin = new Padding(5, 0, 5, 0);
            lab_connect_swing.Name = "lab_connect_swing";
            lab_connect_swing.Size = new Size(111, 58);
            lab_connect_swing.TabIndex = 21;
            lab_connect_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ButtonHighlight;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(panel6);
            panel3.Controls.Add(lab_connectrecord1);
            panel3.Controls.Add(lab_IP_Port2);
            panel3.Controls.Add(lab_connect_2);
            panel3.Location = new Point(13, 269);
            panel3.Margin = new Padding(5);
            panel3.Name = "panel3";
            panel3.Size = new Size(404, 111);
            panel3.TabIndex = 57;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel6.BackgroundImage = Properties.Resources.圖片2;
            panel6.BackgroundImageLayout = ImageLayout.Stretch;
            panel6.Location = new Point(327, 17);
            panel6.Margin = new Padding(5);
            panel6.Name = "panel6";
            panel6.Size = new Size(55, 80);
            panel6.TabIndex = 48;
            panel6.Click += panel6_Click;
            // 
            // lab_connectrecord1
            // 
            lab_connectrecord1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_connectrecord1.AutoSize = true;
            lab_connectrecord1.BackColor = SystemColors.ButtonHighlight;
            lab_connectrecord1.Font = new Font("Microsoft JhengHei UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connectrecord1.Location = new Point(6, 57);
            lab_connectrecord1.Margin = new Padding(5, 0, 5, 0);
            lab_connectrecord1.Name = "lab_connectrecord1";
            lab_connectrecord1.Size = new Size(198, 22);
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
            lab_IP_Port2.Location = new Point(6, 6);
            lab_IP_Port2.Margin = new Padding(5, 0, 5, 0);
            lab_IP_Port2.Name = "lab_IP_Port2";
            lab_IP_Port2.Size = new Size(300, 22);
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
            lab_connect_2.Location = new Point(3, 28);
            lab_connect_2.Margin = new Padding(5, 0, 5, 0);
            lab_connect_2.Name = "lab_connect_2";
            lab_connect_2.Size = new Size(220, 29);
            lab_connect_2.TabIndex = 43;
            lab_connect_2.Text = "是否連線的顯示設定";
            lab_connect_2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ButtonHighlight;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(btn_Saw);
            panel4.Controls.Add(btn_Sawband);
            panel4.Location = new Point(17, 391);
            panel4.Margin = new Padding(5);
            panel4.Name = "panel4";
            panel4.Size = new Size(400, 67);
            panel4.TabIndex = 48;
            // 
            // btn_Saw
            // 
            btn_Saw.BackColor = SystemColors.ButtonHighlight;
            btn_Saw.FlatStyle = FlatStyle.Flat;
            btn_Saw.Location = new Point(196, 5);
            btn_Saw.Margin = new Padding(6);
            btn_Saw.Name = "btn_Saw";
            btn_Saw.Size = new Size(190, 54);
            btn_Saw.TabIndex = 55;
            btn_Saw.Text = "鋸床";
            btn_Saw.UseVisualStyleBackColor = false;
            btn_Saw.Click += btn_saw_Click;
            // 
            // btn_Sawband
            // 
            btn_Sawband.BackColor = SystemColors.ButtonHighlight;
            btn_Sawband.FlatStyle = FlatStyle.Flat;
            btn_Sawband.Location = new Point(3, 5);
            btn_Sawband.Margin = new Padding(6);
            btn_Sawband.Name = "btn_Sawband";
            btn_Sawband.Size = new Size(181, 54);
            btn_Sawband.TabIndex = 54;
            btn_Sawband.Text = "鋸帶";
            btn_Sawband.UseVisualStyleBackColor = false;
            btn_Sawband.Click += btn_SawBand_Click;
            // 
            // tbPanel_Swing_yellow
            // 
            tbPanel_Swing_yellow.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Swing_yellow.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_yellow.ColumnCount = 1;
            tbPanel_Swing_yellow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Swing_yellow.Controls.Add(lab_yS, 0, 1);
            tbPanel_Swing_yellow.Controls.Add(lab_yellow_swing, 0, 0);
            tbPanel_Swing_yellow.Location = new Point(152, 142);
            tbPanel_Swing_yellow.Margin = new Padding(5);
            tbPanel_Swing_yellow.Name = "tbPanel_Swing_yellow";
            tbPanel_Swing_yellow.RowCount = 2;
            tbPanel_Swing_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 52.655674F));
            tbPanel_Swing_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 47.34432F));
            tbPanel_Swing_yellow.Size = new Size(123, 118);
            tbPanel_Swing_yellow.TabIndex = 38;
            // 
            // lab_yS
            // 
            lab_yS.AutoSize = true;
            lab_yS.BackColor = SystemColors.ButtonHighlight;
            lab_yS.Dock = DockStyle.Fill;
            lab_yS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yS.ForeColor = Color.FromArgb(251, 192, 45);
            lab_yS.Location = new Point(5, 61);
            lab_yS.Margin = new Padding(5, 0, 5, 0);
            lab_yS.Name = "lab_yS";
            lab_yS.Size = new Size(111, 55);
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
            lab_yellow_swing.Location = new Point(5, 0);
            lab_yellow_swing.Margin = new Padding(5, 0, 5, 0);
            lab_yellow_swing.Name = "lab_yellow_swing";
            lab_yellow_swing.Size = new Size(111, 61);
            lab_yellow_swing.TabIndex = 21;
            lab_yellow_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Swing_sum
            // 
            tbPanel_Swing_sum.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Swing_sum.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_sum.ColumnCount = 1;
            tbPanel_Swing_sum.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Swing_sum.Controls.Add(lab_sumS, 0, 1);
            tbPanel_Swing_sum.Controls.Add(lab_sum_swing, 0, 0);
            tbPanel_Swing_sum.Location = new Point(13, 13);
            tbPanel_Swing_sum.Margin = new Padding(5);
            tbPanel_Swing_sum.Name = "tbPanel_Swing_sum";
            tbPanel_Swing_sum.RowCount = 2;
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 52.1074562F));
            tbPanel_Swing_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 47.8925438F));
            tbPanel_Swing_sum.Size = new Size(123, 118);
            tbPanel_Swing_sum.TabIndex = 33;
            // 
            // lab_sumS
            // 
            lab_sumS.AutoSize = true;
            lab_sumS.BackColor = SystemColors.ButtonHighlight;
            lab_sumS.Dock = DockStyle.Fill;
            lab_sumS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_sumS.ForeColor = Color.FromArgb(51, 51, 51);
            lab_sumS.Location = new Point(5, 60);
            lab_sumS.Margin = new Padding(5, 0, 5, 0);
            lab_sumS.Name = "lab_sumS";
            lab_sumS.Size = new Size(111, 56);
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
            lab_sum_swing.Location = new Point(5, 0);
            lab_sum_swing.Margin = new Padding(5, 0, 5, 0);
            lab_sum_swing.Name = "lab_sum_swing";
            lab_sum_swing.Size = new Size(111, 60);
            lab_sum_swing.TabIndex = 21;
            lab_sum_swing.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbPanel_Swing_green
            // 
            tbPanel_Swing_green.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Swing_green.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_green.ColumnCount = 1;
            tbPanel_Swing_green.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Swing_green.Controls.Add(lab_gS, 0, 1);
            tbPanel_Swing_green.Controls.Add(lab_green_swing, 0, 0);
            tbPanel_Swing_green.Location = new Point(13, 141);
            tbPanel_Swing_green.Margin = new Padding(5);
            tbPanel_Swing_green.Name = "tbPanel_Swing_green";
            tbPanel_Swing_green.RowCount = 2;
            tbPanel_Swing_green.RowStyles.Add(new RowStyle(SizeType.Percent, 52.655674F));
            tbPanel_Swing_green.RowStyles.Add(new RowStyle(SizeType.Percent, 47.34432F));
            tbPanel_Swing_green.Size = new Size(123, 118);
            tbPanel_Swing_green.TabIndex = 37;
            // 
            // lab_gS
            // 
            lab_gS.AutoSize = true;
            lab_gS.BackColor = SystemColors.ButtonHighlight;
            lab_gS.Dock = DockStyle.Fill;
            lab_gS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_gS.ForeColor = Color.FromArgb(67, 160, 71);
            lab_gS.Location = new Point(5, 61);
            lab_gS.Margin = new Padding(5, 0, 5, 0);
            lab_gS.Name = "lab_gS";
            lab_gS.Size = new Size(111, 55);
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
            lab_green_swing.Location = new Point(5, 0);
            lab_green_swing.Margin = new Padding(5, 0, 5, 0);
            lab_green_swing.Name = "lab_green_swing";
            lab_green_swing.Size = new Size(111, 61);
            lab_green_swing.TabIndex = 21;
            lab_green_swing.TextAlign = ContentAlignment.MiddleCenter;
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
            tbPanel_Swing_disconnect.Location = new Point(294, 15);
            tbPanel_Swing_disconnect.Margin = new Padding(5);
            tbPanel_Swing_disconnect.Name = "tbPanel_Swing_disconnect";
            tbPanel_Swing_disconnect.RowCount = 2;
            tbPanel_Swing_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 53.4090919F));
            tbPanel_Swing_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 46.5909081F));
            tbPanel_Swing_disconnect.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tbPanel_Swing_disconnect.Size = new Size(123, 118);
            tbPanel_Swing_disconnect.TabIndex = 36;
            // 
            // lab_disS
            // 
            lab_disS.AutoSize = true;
            lab_disS.BackColor = SystemColors.ButtonHighlight;
            lab_disS.Dock = DockStyle.Fill;
            lab_disS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_disS.ForeColor = Color.FromArgb(229, 57, 53);
            lab_disS.Location = new Point(5, 61);
            lab_disS.Margin = new Padding(5, 0, 5, 0);
            lab_disS.Name = "lab_disS";
            lab_disS.Size = new Size(111, 55);
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
            lab_disconnect_sawing.Location = new Point(5, 0);
            lab_disconnect_sawing.Margin = new Padding(5, 0, 5, 0);
            lab_disconnect_sawing.Name = "lab_disconnect_sawing";
            lab_disconnect_sawing.Size = new Size(111, 61);
            lab_disconnect_sawing.TabIndex = 21;
            lab_disconnect_sawing.TextAlign = ContentAlignment.MiddleCenter;
            lab_disconnect_sawing.Click += lab_disconnect_sawing_Click;
            // 
            // tbPanel_Swing_red
            // 
            tbPanel_Swing_red.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Swing_red.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Swing_red.ColumnCount = 1;
            tbPanel_Swing_red.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Swing_red.Controls.Add(lab_rS, 0, 1);
            tbPanel_Swing_red.Controls.Add(lab_red_swing, 0, 0);
            tbPanel_Swing_red.Location = new Point(294, 143);
            tbPanel_Swing_red.Margin = new Padding(5);
            tbPanel_Swing_red.Name = "tbPanel_Swing_red";
            tbPanel_Swing_red.RowCount = 2;
            tbPanel_Swing_red.RowStyles.Add(new RowStyle(SizeType.Percent, 52.1074562F));
            tbPanel_Swing_red.RowStyles.Add(new RowStyle(SizeType.Percent, 47.8925438F));
            tbPanel_Swing_red.Size = new Size(123, 118);
            tbPanel_Swing_red.TabIndex = 34;
            // 
            // lab_rS
            // 
            lab_rS.AutoSize = true;
            lab_rS.BackColor = SystemColors.ButtonHighlight;
            lab_rS.Dock = DockStyle.Fill;
            lab_rS.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_rS.ForeColor = Color.FromArgb(211, 47, 47);
            lab_rS.Location = new Point(5, 60);
            lab_rS.Margin = new Padding(5, 0, 5, 0);
            lab_rS.Name = "lab_rS";
            lab_rS.Size = new Size(111, 56);
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
            lab_red_swing.Location = new Point(5, 0);
            lab_red_swing.Margin = new Padding(5, 0, 5, 0);
            lab_red_swing.Name = "lab_red_swing";
            lab_red_swing.Size = new Size(111, 60);
            lab_red_swing.TabIndex = 21;
            lab_red_swing.TextAlign = ContentAlignment.MiddleCenter;
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
            panel11.Controls.Add(panel1);
            panel11.Controls.Add(panel2);
            panel11.Controls.Add(tbPanel_Drill_yellow);
            panel11.Location = new Point(27, 343);
            panel11.Margin = new Padding(5);
            panel11.Name = "panel11";
            panel11.Size = new Size(430, 477);
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
            tbPanel_Drill_sum.Location = new Point(18, 15);
            tbPanel_Drill_sum.Margin = new Padding(5);
            tbPanel_Drill_sum.Name = "tbPanel_Drill_sum";
            tbPanel_Drill_sum.RowCount = 2;
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 57.4018135F));
            tbPanel_Drill_sum.RowStyles.Add(new RowStyle(SizeType.Percent, 42.59819F));
            tbPanel_Drill_sum.Size = new Size(123, 118);
            tbPanel_Drill_sum.TabIndex = 27;
            // 
            // lab_sumD
            // 
            lab_sumD.AutoSize = true;
            lab_sumD.BackColor = SystemColors.ButtonHighlight;
            lab_sumD.Dock = DockStyle.Fill;
            lab_sumD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_sumD.ForeColor = Color.FromArgb(51, 51, 51);
            lab_sumD.Location = new Point(5, 66);
            lab_sumD.Margin = new Padding(5, 0, 5, 0);
            lab_sumD.Name = "lab_sumD";
            lab_sumD.Size = new Size(111, 50);
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
            lab_sum.Location = new Point(5, 0);
            lab_sum.Margin = new Padding(5, 0, 5, 0);
            lab_sum.Name = "lab_sum";
            lab_sum.Size = new Size(111, 66);
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
            tbPanel_Drill_connect.Location = new Point(151, 15);
            tbPanel_Drill_connect.Margin = new Padding(5);
            tbPanel_Drill_connect.Name = "tbPanel_Drill_connect";
            tbPanel_Drill_connect.RowCount = 2;
            tbPanel_Drill_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 57.4018135F));
            tbPanel_Drill_connect.RowStyles.Add(new RowStyle(SizeType.Percent, 42.59819F));
            tbPanel_Drill_connect.Size = new Size(123, 118);
            tbPanel_Drill_connect.TabIndex = 29;
            // 
            // lab_connectD
            // 
            lab_connectD.AutoSize = true;
            lab_connectD.BackColor = SystemColors.ButtonHighlight;
            lab_connectD.Dock = DockStyle.Fill;
            lab_connectD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connectD.ForeColor = Color.FromArgb(51, 51, 51);
            lab_connectD.Location = new Point(5, 66);
            lab_connectD.Margin = new Padding(5, 0, 5, 0);
            lab_connectD.Name = "lab_connectD";
            lab_connectD.Size = new Size(111, 50);
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
            lab_connect.Location = new Point(5, 0);
            lab_connect.Margin = new Padding(5, 0, 5, 0);
            lab_connect.Name = "lab_connect";
            lab_connect.Size = new Size(111, 66);
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
            tbPanel_Drill_red.Location = new Point(286, 142);
            tbPanel_Drill_red.Margin = new Padding(5);
            tbPanel_Drill_red.Name = "tbPanel_Drill_red";
            tbPanel_Drill_red.RowCount = 2;
            tbPanel_Drill_red.RowStyles.Add(new RowStyle(SizeType.Percent, 53.9132652F));
            tbPanel_Drill_red.RowStyles.Add(new RowStyle(SizeType.Percent, 46.0867348F));
            tbPanel_Drill_red.Size = new Size(123, 118);
            tbPanel_Drill_red.TabIndex = 28;
            // 
            // lab_rD
            // 
            lab_rD.AutoSize = true;
            lab_rD.BackColor = SystemColors.ButtonHighlight;
            lab_rD.Dock = DockStyle.Fill;
            lab_rD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_rD.ForeColor = Color.FromArgb(211, 47, 47);
            lab_rD.Location = new Point(5, 62);
            lab_rD.Margin = new Padding(5, 0, 5, 0);
            lab_rD.Name = "lab_rD";
            lab_rD.Size = new Size(111, 54);
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
            lab_red.Location = new Point(5, 0);
            lab_red.Margin = new Padding(5, 0, 5, 0);
            lab_red.Name = "lab_red";
            lab_red.Size = new Size(111, 62);
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
            tbPanel_Drill_disconnect.Location = new Point(286, 15);
            tbPanel_Drill_disconnect.Margin = new Padding(5);
            tbPanel_Drill_disconnect.Name = "tbPanel_Drill_disconnect";
            tbPanel_Drill_disconnect.RowCount = 2;
            tbPanel_Drill_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 57.4018135F));
            tbPanel_Drill_disconnect.RowStyles.Add(new RowStyle(SizeType.Percent, 42.59819F));
            tbPanel_Drill_disconnect.Size = new Size(123, 118);
            tbPanel_Drill_disconnect.TabIndex = 30;
            // 
            // lab_disD
            // 
            lab_disD.AutoSize = true;
            lab_disD.BackColor = SystemColors.ButtonHighlight;
            lab_disD.Dock = DockStyle.Fill;
            lab_disD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_disD.ForeColor = Color.FromArgb(229, 57, 53);
            lab_disD.Location = new Point(5, 66);
            lab_disD.Margin = new Padding(5, 0, 5, 0);
            lab_disD.Name = "lab_disD";
            lab_disD.Size = new Size(111, 50);
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
            lab_disconnect.Location = new Point(5, 0);
            lab_disconnect.Margin = new Padding(5, 0, 5, 0);
            lab_disconnect.Name = "lab_disconnect";
            lab_disconnect.Size = new Size(111, 66);
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
            tbPanel_Drill_green.Location = new Point(18, 143);
            tbPanel_Drill_green.Margin = new Padding(5);
            tbPanel_Drill_green.Name = "tbPanel_Drill_green";
            tbPanel_Drill_green.RowCount = 2;
            tbPanel_Drill_green.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tbPanel_Drill_green.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tbPanel_Drill_green.Size = new Size(123, 118);
            tbPanel_Drill_green.TabIndex = 31;
            // 
            // lab_gD
            // 
            lab_gD.AutoSize = true;
            lab_gD.BackColor = SystemColors.ButtonHighlight;
            lab_gD.Dock = DockStyle.Fill;
            lab_gD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_gD.ForeColor = Color.FromArgb(67, 160, 71);
            lab_gD.Location = new Point(5, 63);
            lab_gD.Margin = new Padding(5, 0, 5, 0);
            lab_gD.Name = "lab_gD";
            lab_gD.Size = new Size(111, 53);
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
            lab_green.Location = new Point(5, 0);
            lab_green.Margin = new Padding(5, 0, 5, 0);
            lab_green.Name = "lab_green";
            lab_green.Size = new Size(111, 63);
            lab_green.TabIndex = 21;
            lab_green.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(lab_connectrecord);
            panel1.Controls.Add(lab_IP_Port1);
            panel1.Controls.Add(lab_connect_1);
            panel1.Location = new Point(18, 270);
            panel1.Margin = new Padding(5);
            panel1.Name = "panel1";
            panel1.Size = new Size(391, 111);
            panel1.TabIndex = 41;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel5.BackgroundImage = Properties.Resources.圖片2;
            panel5.BackgroundImageLayout = ImageLayout.Stretch;
            panel5.Location = new Point(315, 17);
            panel5.Margin = new Padding(5);
            panel5.Name = "panel5";
            panel5.Size = new Size(55, 80);
            panel5.TabIndex = 2;
            panel5.Click += panel5_Click;
            // 
            // lab_connectrecord
            // 
            lab_connectrecord.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_connectrecord.AutoSize = true;
            lab_connectrecord.BackColor = SystemColors.ButtonHighlight;
            lab_connectrecord.Font = new Font("Microsoft JhengHei UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connectrecord.Location = new Point(6, 61);
            lab_connectrecord.Margin = new Padding(5, 0, 5, 0);
            lab_connectrecord.Name = "lab_connectrecord";
            lab_connectrecord.Size = new Size(198, 22);
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
            lab_IP_Port1.Location = new Point(8, 6);
            lab_IP_Port1.Margin = new Padding(5, 0, 5, 0);
            lab_IP_Port1.Name = "lab_IP_Port1";
            lab_IP_Port1.Size = new Size(146, 22);
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
            lab_connect_1.Location = new Point(3, 28);
            lab_connect_1.Margin = new Padding(5, 0, 5, 0);
            lab_connect_1.Name = "lab_connect_1";
            lab_connect_1.Size = new Size(220, 29);
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
            panel2.Location = new Point(18, 391);
            panel2.Margin = new Padding(5);
            panel2.Name = "panel2";
            panel2.Size = new Size(395, 67);
            panel2.TabIndex = 42;
            // 
            // btn_Drill_Info
            // 
            btn_Drill_Info.BackColor = SystemColors.ButtonHighlight;
            btn_Drill_Info.FlatStyle = FlatStyle.Flat;
            btn_Drill_Info.Location = new Point(6, 6);
            btn_Drill_Info.Margin = new Padding(6);
            btn_Drill_Info.Name = "btn_Drill_Info";
            btn_Drill_Info.Size = new Size(374, 53);
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
            btn_machInfo.Location = new Point(25, 15);
            btn_machInfo.Margin = new Padding(6);
            btn_machInfo.Name = "btn_machInfo";
            btn_machInfo.Size = new Size(234, 0);
            btn_machInfo.TabIndex = 39;
            btn_machInfo.Text = "鑽床資料";
            btn_machInfo.UseVisualStyleBackColor = false;
            // 
            // tbPanel_Drill_yellow
            // 
            tbPanel_Drill_yellow.BackColor = SystemColors.ButtonHighlight;
            tbPanel_Drill_yellow.BorderStyle = BorderStyle.FixedSingle;
            tbPanel_Drill_yellow.ColumnCount = 1;
            tbPanel_Drill_yellow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tbPanel_Drill_yellow.Controls.Add(lab_yD, 0, 1);
            tbPanel_Drill_yellow.Controls.Add(lab_yellow, 0, 0);
            tbPanel_Drill_yellow.Location = new Point(151, 143);
            tbPanel_Drill_yellow.Margin = new Padding(5);
            tbPanel_Drill_yellow.Name = "tbPanel_Drill_yellow";
            tbPanel_Drill_yellow.RowCount = 2;
            tbPanel_Drill_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tbPanel_Drill_yellow.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tbPanel_Drill_yellow.Size = new Size(123, 118);
            tbPanel_Drill_yellow.TabIndex = 32;
            // 
            // lab_yD
            // 
            lab_yD.AutoSize = true;
            lab_yD.BackColor = SystemColors.ButtonHighlight;
            lab_yD.Dock = DockStyle.Fill;
            lab_yD.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yD.ForeColor = Color.FromArgb(251, 192, 45);
            lab_yD.Location = new Point(5, 63);
            lab_yD.Margin = new Padding(5, 0, 5, 0);
            lab_yD.Name = "lab_yD";
            lab_yD.Size = new Size(111, 53);
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
            lab_yellow.Location = new Point(5, 0);
            lab_yellow.Margin = new Padding(5, 0, 5, 0);
            lab_yellow.Name = "lab_yellow";
            lab_yellow.Size = new Size(111, 63);
            lab_yellow.TabIndex = 21;
            lab_yellow.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel_Swing
            // 
            panel_Swing.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_Swing.BackColor = SystemColors.ButtonHighlight;
            panel_Swing.BackgroundImageLayout = ImageLayout.Center;
            panel_Swing.BorderStyle = BorderStyle.FixedSingle;
            panel_Swing.Controls.Add(pictureBox2);
            panel_Swing.Controls.Add(tableLayoutPanel2);
            panel_Swing.Location = new Point(542, 21);
            panel_Swing.Margin = new Padding(5);
            panel_Swing.Name = "panel_Swing";
            panel_Swing.Size = new Size(430, 315);
            panel_Swing.TabIndex = 51;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(-1, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(221, 305);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 60;
            pictureBox2.TabStop = false;
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
            tableLayoutPanel2.Location = new Point(243, 99);
            tableLayoutPanel2.Margin = new Padding(5);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel2.Size = new Size(161, 127);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // lab_today_ratio1
            // 
            lab_today_ratio1.BackColor = Color.Transparent;
            lab_today_ratio1.Dock = DockStyle.Fill;
            lab_today_ratio1.Font = new Font("微軟正黑體", 9.75F);
            lab_today_ratio1.ForeColor = SystemColors.ActiveCaptionText;
            lab_today_ratio1.Location = new Point(84, 95);
            lab_today_ratio1.Margin = new Padding(5, 0, 5, 0);
            lab_today_ratio1.Name = "lab_today_ratio1";
            lab_today_ratio1.Size = new Size(72, 32);
            lab_today_ratio1.TabIndex = 15;
            lab_today_ratio1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_today1
            // 
            lab_today1.BackColor = Color.Transparent;
            lab_today1.Dock = DockStyle.Fill;
            lab_today1.Font = new Font("微軟正黑體", 9.75F);
            lab_today1.ForeColor = SystemColors.ActiveCaptionText;
            lab_today1.Location = new Point(5, 95);
            lab_today1.Margin = new Padding(5, 0, 5, 0);
            lab_today1.Name = "lab_today1";
            lab_today1.Size = new Size(69, 32);
            lab_today1.TabIndex = 14;
            lab_today1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_yesterday_ratio1
            // 
            lab_yesterday_ratio1.BackColor = Color.Transparent;
            lab_yesterday_ratio1.Dock = DockStyle.Fill;
            lab_yesterday_ratio1.Font = new Font("微軟正黑體", 9.75F);
            lab_yesterday_ratio1.ForeColor = SystemColors.ActiveCaptionText;
            lab_yesterday_ratio1.Location = new Point(84, 64);
            lab_yesterday_ratio1.Margin = new Padding(5, 0, 5, 0);
            lab_yesterday_ratio1.Name = "lab_yesterday_ratio1";
            lab_yesterday_ratio1.Size = new Size(72, 31);
            lab_yesterday_ratio1.TabIndex = 13;
            lab_yesterday_ratio1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_yesterday1
            // 
            lab_yesterday1.BackColor = Color.Transparent;
            lab_yesterday1.Dock = DockStyle.Fill;
            lab_yesterday1.Font = new Font("微軟正黑體", 9.75F);
            lab_yesterday1.ForeColor = SystemColors.ActiveCaptionText;
            lab_yesterday1.Location = new Point(5, 64);
            lab_yesterday1.Margin = new Padding(5, 0, 5, 0);
            lab_yesterday1.Name = "lab_yesterday1";
            lab_yesterday1.Size = new Size(69, 31);
            lab_yesterday1.TabIndex = 12;
            lab_yesterday1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_this_ratio1
            // 
            lab_this_ratio1.BackColor = Color.Transparent;
            lab_this_ratio1.Dock = DockStyle.Fill;
            lab_this_ratio1.Font = new Font("微軟正黑體", 9.75F);
            lab_this_ratio1.ForeColor = SystemColors.ActiveCaptionText;
            lab_this_ratio1.Location = new Point(84, 32);
            lab_this_ratio1.Margin = new Padding(5, 0, 5, 0);
            lab_this_ratio1.Name = "lab_this_ratio1";
            lab_this_ratio1.Size = new Size(72, 32);
            lab_this_ratio1.TabIndex = 11;
            lab_this_ratio1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_thisweek1
            // 
            lab_thisweek1.BackColor = Color.Transparent;
            lab_thisweek1.Dock = DockStyle.Fill;
            lab_thisweek1.Font = new Font("微軟正黑體", 9.75F);
            lab_thisweek1.ForeColor = SystemColors.ActiveCaptionText;
            lab_thisweek1.Location = new Point(5, 32);
            lab_thisweek1.Margin = new Padding(5, 0, 5, 0);
            lab_thisweek1.Name = "lab_thisweek1";
            lab_thisweek1.Size = new Size(69, 32);
            lab_thisweek1.TabIndex = 10;
            lab_thisweek1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_last_ratio1
            // 
            lab_last_ratio1.BackColor = Color.Transparent;
            lab_last_ratio1.Dock = DockStyle.Fill;
            lab_last_ratio1.Font = new Font("微軟正黑體", 9.75F);
            lab_last_ratio1.ForeColor = SystemColors.ActiveCaptionText;
            lab_last_ratio1.Location = new Point(84, 0);
            lab_last_ratio1.Margin = new Padding(5, 0, 5, 0);
            lab_last_ratio1.Name = "lab_last_ratio1";
            lab_last_ratio1.Size = new Size(72, 32);
            lab_last_ratio1.TabIndex = 9;
            lab_last_ratio1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_lastweek1
            // 
            lab_lastweek1.BackColor = Color.Transparent;
            lab_lastweek1.Dock = DockStyle.Fill;
            lab_lastweek1.Font = new Font("微軟正黑體", 9.75F);
            lab_lastweek1.ForeColor = SystemColors.ActiveCaptionText;
            lab_lastweek1.Location = new Point(5, 0);
            lab_lastweek1.Margin = new Padding(5, 0, 5, 0);
            lab_lastweek1.Name = "lab_lastweek1";
            lab_lastweek1.Size = new Size(69, 32);
            lab_lastweek1.TabIndex = 8;
            lab_lastweek1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel_Drill
            // 
            panel_Drill.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel_Drill.BackColor = SystemColors.ButtonHighlight;
            panel_Drill.BackgroundImageLayout = ImageLayout.Zoom;
            panel_Drill.BorderStyle = BorderStyle.FixedSingle;
            panel_Drill.Controls.Add(pictureBox1);
            panel_Drill.Controls.Add(tableLayoutPanel1);
            panel_Drill.Location = new Point(27, 21);
            panel_Drill.Margin = new Padding(5);
            panel_Drill.Name = "panel_Drill";
            panel_Drill.Size = new Size(430, 315);
            panel_Drill.TabIndex = 47;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-1, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(221, 305);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 60;
            pictureBox1.TabStop = false;
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
            tableLayoutPanel1.Location = new Point(242, 99);
            tableLayoutPanel1.Margin = new Padding(5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel1.Size = new Size(161, 127);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // lab_today_ratio
            // 
            lab_today_ratio.BackColor = Color.Transparent;
            lab_today_ratio.Dock = DockStyle.Fill;
            lab_today_ratio.Font = new Font("微軟正黑體", 9.75F);
            lab_today_ratio.ForeColor = SystemColors.ActiveCaptionText;
            lab_today_ratio.Location = new Point(90, 95);
            lab_today_ratio.Margin = new Padding(5, 0, 5, 0);
            lab_today_ratio.Name = "lab_today_ratio";
            lab_today_ratio.Size = new Size(66, 32);
            lab_today_ratio.TabIndex = 14;
            lab_today_ratio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_today
            // 
            lab_today.BackColor = Color.Transparent;
            lab_today.Dock = DockStyle.Fill;
            lab_today.Font = new Font("微軟正黑體", 9.75F);
            lab_today.ForeColor = SystemColors.ActiveCaptionText;
            lab_today.Location = new Point(5, 95);
            lab_today.Margin = new Padding(5, 0, 5, 0);
            lab_today.Name = "lab_today";
            lab_today.Size = new Size(75, 32);
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
            lab_yesterday_ratio.Location = new Point(90, 64);
            lab_yesterday_ratio.Margin = new Padding(5, 0, 5, 0);
            lab_yesterday_ratio.Name = "lab_yesterday_ratio";
            lab_yesterday_ratio.Size = new Size(66, 31);
            lab_yesterday_ratio.TabIndex = 12;
            lab_yesterday_ratio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_yesterday
            // 
            lab_yesterday.BackColor = Color.Transparent;
            lab_yesterday.Dock = DockStyle.Fill;
            lab_yesterday.Font = new Font("微軟正黑體", 9.75F);
            lab_yesterday.ForeColor = SystemColors.ActiveCaptionText;
            lab_yesterday.Location = new Point(5, 64);
            lab_yesterday.Margin = new Padding(5, 0, 5, 0);
            lab_yesterday.Name = "lab_yesterday";
            lab_yesterday.Size = new Size(75, 31);
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
            lab_this_ratio.Location = new Point(90, 32);
            lab_this_ratio.Margin = new Padding(5, 0, 5, 0);
            lab_this_ratio.Name = "lab_this_ratio";
            lab_this_ratio.Size = new Size(66, 32);
            lab_this_ratio.TabIndex = 10;
            lab_this_ratio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_thisweek
            // 
            lab_thisweek.BackColor = Color.Transparent;
            lab_thisweek.Dock = DockStyle.Fill;
            lab_thisweek.Font = new Font("微軟正黑體", 9.75F);
            lab_thisweek.ForeColor = SystemColors.ActiveCaptionText;
            lab_thisweek.Location = new Point(5, 32);
            lab_thisweek.Margin = new Padding(5, 0, 5, 0);
            lab_thisweek.Name = "lab_thisweek";
            lab_thisweek.Size = new Size(75, 32);
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
            lab_last_ratio.Location = new Point(90, 0);
            lab_last_ratio.Margin = new Padding(5, 0, 5, 0);
            lab_last_ratio.Name = "lab_last_ratio";
            lab_last_ratio.Size = new Size(66, 32);
            lab_last_ratio.TabIndex = 8;
            lab_last_ratio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_lastweek
            // 
            lab_lastweek.BackColor = Color.Transparent;
            lab_lastweek.Dock = DockStyle.Fill;
            lab_lastweek.Font = new Font("微軟正黑體", 9.75F);
            lab_lastweek.ForeColor = SystemColors.ActiveCaptionText;
            lab_lastweek.Location = new Point(5, 0);
            lab_lastweek.Margin = new Padding(5, 0, 5, 0);
            lab_lastweek.Name = "lab_lastweek";
            lab_lastweek.Size = new Size(75, 32);
            lab_lastweek.TabIndex = 7;
            lab_lastweek.Text = "上週:";
            lab_lastweek.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btn_toggle
            // 
            btn_toggle.Location = new Point(135, 845);
            btn_toggle.Margin = new Padding(5);
            btn_toggle.Name = "btn_toggle";
            btn_toggle.Size = new Size(154, 48);
            btn_toggle.TabIndex = 56;
            btn_toggle.UseVisualStyleBackColor = false;
            btn_toggle.Click += btn_toggle_Click;
            // 
            // lb_Last_cloudupdatetime
            // 
            lb_Last_cloudupdatetime.Location = new Point(38, 900);
            lb_Last_cloudupdatetime.Margin = new Padding(5, 0, 5, 0);
            lb_Last_cloudupdatetime.MaximumSize = new Size(141, 0);
            lb_Last_cloudupdatetime.Name = "lb_Last_cloudupdatetime";
            lb_Last_cloudupdatetime.Size = new Size(141, 27);
            lb_Last_cloudupdatetime.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel7.BackColor = SystemColors.ButtonHighlight;
            panel7.BackgroundImageLayout = ImageLayout.Center;
            panel7.BorderStyle = BorderStyle.FixedSingle;
            panel7.Controls.Add(pictureBox3);
            panel7.Controls.Add(tableLayoutPanel3);
            panel7.Location = new Point(1040, 21);
            panel7.Margin = new Padding(5);
            panel7.Name = "panel7";
            panel7.Size = new Size(430, 315);
            panel7.TabIndex = 58;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(-1, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(221, 305);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 61;
            pictureBox3.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.36709F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.63291F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39.2405052F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.7594948F));
            tableLayoutPanel3.Controls.Add(label1, 1, 3);
            tableLayoutPanel3.Controls.Add(label2, 0, 3);
            tableLayoutPanel3.Controls.Add(label3, 1, 2);
            tableLayoutPanel3.Controls.Add(label4, 0, 2);
            tableLayoutPanel3.Controls.Add(label5, 1, 1);
            tableLayoutPanel3.Controls.Add(label6, 0, 1);
            tableLayoutPanel3.Controls.Add(label7, 1, 0);
            tableLayoutPanel3.Controls.Add(label8, 0, 0);
            tableLayoutPanel3.Location = new Point(243, 99);
            tableLayoutPanel3.Margin = new Padding(5);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 4;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel3.Size = new Size(161, 127);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("微軟正黑體", 9.75F);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(84, 95);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(72, 32);
            label1.TabIndex = 15;
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("微軟正黑體", 9.75F);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(5, 95);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(69, 32);
            label2.TabIndex = 14;
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("微軟正黑體", 9.75F);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.Location = new Point(84, 64);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(72, 31);
            label3.TabIndex = 13;
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("微軟正黑體", 9.75F);
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.Location = new Point(5, 64);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(69, 31);
            label4.TabIndex = 12;
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("微軟正黑體", 9.75F);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.Location = new Point(84, 32);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(72, 32);
            label5.TabIndex = 11;
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.BackColor = Color.Transparent;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("微軟正黑體", 9.75F);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.Location = new Point(5, 32);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(69, 32);
            label6.TabIndex = 10;
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.BackColor = Color.Transparent;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("微軟正黑體", 9.75F);
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.Location = new Point(84, 0);
            label7.Margin = new Padding(5, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(72, 32);
            label7.TabIndex = 9;
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            label8.BackColor = Color.Transparent;
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("微軟正黑體", 9.75F);
            label8.ForeColor = SystemColors.ActiveCaptionText;
            label8.Location = new Point(5, 0);
            label8.Margin = new Padding(5, 0, 5, 0);
            label8.Name = "label8";
            label8.Size = new Size(69, 32);
            label8.TabIndex = 8;
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label13.AutoSize = true;
            label13.BackColor = SystemColors.ButtonHighlight;
            label13.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label13.Location = new Point(4, 28);
            label13.Margin = new Padding(5, 0, 5, 0);
            label13.Name = "label13";
            label13.Size = new Size(220, 29);
            label13.TabIndex = 43;
            label13.Text = "是否連線的顯示設定";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.BackColor = SystemColors.ButtonHighlight;
            label12.Font = new Font("Microsoft JhengHei UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label12.Location = new Point(7, 6);
            label12.Margin = new Padding(5, 0, 5, 0);
            label12.Name = "label12";
            label12.Size = new Size(300, 22);
            label12.TabIndex = 44;
            label12.Text = "Auto connection is not configured.";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.BackColor = SystemColors.ButtonHighlight;
            label11.Font = new Font("Microsoft JhengHei UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label11.Location = new Point(7, 57);
            label11.Margin = new Padding(5, 0, 5, 0);
            label11.Name = "label11";
            label11.Size = new Size(198, 22);
            label11.TabIndex = 47;
            label11.Text = "斷線時間or重新連線時間";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel9.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel9.ColumnCount = 1;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel9.Controls.Add(label22, 0, 1);
            tableLayoutPanel9.Controls.Add(label23, 0, 0);
            tableLayoutPanel9.Location = new Point(294, 143);
            tableLayoutPanel9.Margin = new Padding(5);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 2;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 52.1074562F));
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 47.8925438F));
            tableLayoutPanel9.Size = new Size(123, 118);
            tableLayoutPanel9.TabIndex = 34;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.BackColor = SystemColors.ButtonHighlight;
            label22.Dock = DockStyle.Fill;
            label22.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label22.ForeColor = Color.FromArgb(211, 47, 47);
            label22.Location = new Point(5, 60);
            label22.Margin = new Padding(5, 0, 5, 0);
            label22.Name = "label22";
            label22.Size = new Size(111, 56);
            label22.TabIndex = 22;
            label22.Text = "紅燈總數";
            label22.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.BackColor = SystemColors.ButtonHighlight;
            label23.Dock = DockStyle.Fill;
            label23.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label23.ForeColor = Color.FromArgb(211, 47, 47);
            label23.Location = new Point(5, 0);
            label23.Margin = new Padding(5, 0, 5, 0);
            label23.Name = "label23";
            label23.Size = new Size(111, 60);
            label23.TabIndex = 21;
            label23.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel8.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel8.ColumnCount = 1;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Controls.Add(label20, 0, 1);
            tableLayoutPanel8.Controls.Add(label21, 0, 0);
            tableLayoutPanel8.Location = new Point(294, 15);
            tableLayoutPanel8.Margin = new Padding(5);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 2;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 53.4090919F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 46.5909081F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel8.Size = new Size(123, 118);
            tableLayoutPanel8.TabIndex = 36;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.BackColor = SystemColors.ButtonHighlight;
            label20.Dock = DockStyle.Fill;
            label20.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label20.ForeColor = Color.FromArgb(229, 57, 53);
            label20.Location = new Point(5, 61);
            label20.Margin = new Padding(5, 0, 5, 0);
            label20.Name = "label20";
            label20.Size = new Size(111, 55);
            label20.TabIndex = 23;
            label20.Text = "元件異常";
            label20.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.BackColor = SystemColors.ButtonHighlight;
            label21.Dock = DockStyle.Fill;
            label21.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label21.ForeColor = Color.FromArgb(229, 57, 53);
            label21.Location = new Point(5, 0);
            label21.Margin = new Padding(5, 0, 5, 0);
            label21.Name = "label21";
            label21.Size = new Size(111, 61);
            label21.TabIndex = 21;
            label21.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel7.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(label18, 0, 1);
            tableLayoutPanel7.Controls.Add(label19, 0, 0);
            tableLayoutPanel7.Location = new Point(13, 141);
            tableLayoutPanel7.Margin = new Padding(5);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 2;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 52.655674F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 47.34432F));
            tableLayoutPanel7.Size = new Size(123, 118);
            tableLayoutPanel7.TabIndex = 37;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = SystemColors.ButtonHighlight;
            label18.Dock = DockStyle.Fill;
            label18.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label18.ForeColor = Color.FromArgb(67, 160, 71);
            label18.Location = new Point(5, 61);
            label18.Margin = new Padding(5, 0, 5, 0);
            label18.Name = "label18";
            label18.Size = new Size(111, 55);
            label18.TabIndex = 23;
            label18.Text = "綠燈總數";
            label18.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.BackColor = SystemColors.ButtonHighlight;
            label19.Dock = DockStyle.Fill;
            label19.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label19.ForeColor = Color.FromArgb(67, 160, 71);
            label19.Location = new Point(5, 0);
            label19.Margin = new Padding(5, 0, 5, 0);
            label19.Name = "label19";
            label19.Size = new Size(111, 61);
            label19.TabIndex = 21;
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel6.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(label16, 0, 1);
            tableLayoutPanel6.Controls.Add(label17, 0, 0);
            tableLayoutPanel6.Location = new Point(13, 13);
            tableLayoutPanel6.Margin = new Padding(5);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 52.1074562F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 47.8925438F));
            tableLayoutPanel6.Size = new Size(123, 118);
            tableLayoutPanel6.TabIndex = 33;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.BackColor = SystemColors.ButtonHighlight;
            label16.Dock = DockStyle.Fill;
            label16.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label16.ForeColor = Color.FromArgb(51, 51, 51);
            label16.Location = new Point(5, 60);
            label16.Margin = new Padding(5, 0, 5, 0);
            label16.Name = "label16";
            label16.Size = new Size(111, 56);
            label16.TabIndex = 23;
            label16.Text = "監控總數";
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.BackColor = SystemColors.ButtonHighlight;
            label17.Dock = DockStyle.Fill;
            label17.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label17.ForeColor = Color.FromArgb(51, 51, 51);
            label17.Location = new Point(5, 0);
            label17.Margin = new Padding(5, 0, 5, 0);
            label17.Name = "label17";
            label17.Size = new Size(111, 60);
            label17.TabIndex = 21;
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel5.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(label14, 0, 1);
            tableLayoutPanel5.Controls.Add(label15, 0, 0);
            tableLayoutPanel5.Location = new Point(152, 142);
            tableLayoutPanel5.Margin = new Padding(5);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 52.655674F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 47.34432F));
            tableLayoutPanel5.Size = new Size(123, 118);
            tableLayoutPanel5.TabIndex = 38;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = SystemColors.ButtonHighlight;
            label14.Dock = DockStyle.Fill;
            label14.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label14.ForeColor = Color.FromArgb(251, 192, 45);
            label14.Location = new Point(5, 61);
            label14.Margin = new Padding(5, 0, 5, 0);
            label14.Name = "label14";
            label14.Size = new Size(111, 55);
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
            label15.Location = new Point(5, 0);
            label15.Margin = new Padding(5, 0, 5, 0);
            label15.Name = "label15";
            label15.Size = new Size(111, 61);
            label15.TabIndex = 21;
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel13
            // 
            panel13.BackColor = SystemColors.ButtonHighlight;
            panel13.BorderStyle = BorderStyle.FixedSingle;
            panel13.Controls.Add(button1);
            panel13.Controls.Add(button2);
            panel13.Location = new Point(17, 391);
            panel13.Margin = new Padding(5);
            panel13.Name = "panel13";
            panel13.Size = new Size(400, 67);
            panel13.TabIndex = 48;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonHighlight;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(196, 5);
            button1.Margin = new Padding(6);
            button1.Name = "button1";
            button1.Size = new Size(190, 54);
            button1.TabIndex = 55;
            button1.Text = "鋸床";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ButtonHighlight;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(3, 5);
            button2.Margin = new Padding(6);
            button2.Name = "button2";
            button2.Size = new Size(181, 54);
            button2.TabIndex = 54;
            button2.Text = "鋸帶";
            button2.UseVisualStyleBackColor = false;
            // 
            // panel9
            // 
            panel9.BackColor = SystemColors.ButtonHighlight;
            panel9.BorderStyle = BorderStyle.FixedSingle;
            panel9.Controls.Add(panel14);
            panel9.Controls.Add(label11);
            panel9.Controls.Add(label13);
            panel9.Controls.Add(label12);
            panel9.Location = new Point(13, 269);
            panel9.Margin = new Padding(5);
            panel9.Name = "panel9";
            panel9.Size = new Size(404, 111);
            panel9.TabIndex = 57;
            // 
            // panel14
            // 
            panel14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel14.BackgroundImage = Properties.Resources.圖片2;
            panel14.BackgroundImageLayout = ImageLayout.Stretch;
            panel14.Location = new Point(335, 17);
            panel14.Margin = new Padding(5);
            panel14.Name = "panel14";
            panel14.Size = new Size(55, 80);
            panel14.TabIndex = 49;
            panel14.Click += panel14_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel4.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(label9, 0, 1);
            tableLayoutPanel4.Controls.Add(label10, 0, 0);
            tableLayoutPanel4.Location = new Point(152, 14);
            tableLayoutPanel4.Margin = new Padding(5);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel4.Size = new Size(123, 118);
            tableLayoutPanel4.TabIndex = 35;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.ButtonHighlight;
            label9.Dock = DockStyle.Fill;
            label9.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label9.ForeColor = Color.FromArgb(51, 51, 51);
            label9.Location = new Point(5, 58);
            label9.Margin = new Padding(5, 0, 5, 0);
            label9.Name = "label9";
            label9.Size = new Size(111, 58);
            label9.TabIndex = 23;
            label9.Text = "連接總數";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = SystemColors.ButtonHighlight;
            label10.Dock = DockStyle.Fill;
            label10.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label10.ForeColor = Color.FromArgb(51, 51, 51);
            label10.Location = new Point(5, 0);
            label10.Margin = new Padding(5, 0, 5, 0);
            label10.Name = "label10";
            label10.Size = new Size(111, 58);
            label10.TabIndex = 21;
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel8
            // 
            panel8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel8.BackColor = SystemColors.ButtonHighlight;
            panel8.BorderStyle = BorderStyle.FixedSingle;
            panel8.Controls.Add(tableLayoutPanel4);
            panel8.Controls.Add(panel9);
            panel8.Controls.Add(panel13);
            panel8.Controls.Add(tableLayoutPanel5);
            panel8.Controls.Add(tableLayoutPanel6);
            panel8.Controls.Add(tableLayoutPanel7);
            panel8.Controls.Add(tableLayoutPanel8);
            panel8.Controls.Add(tableLayoutPanel9);
            panel8.Location = new Point(1040, 343);
            panel8.Margin = new Padding(5);
            panel8.Name = "panel8";
            panel8.Size = new Size(430, 477);
            panel8.TabIndex = 59;
            // 
            // Home_Page2
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1502, 980);
            Controls.Add(panel8);
            Controls.Add(panel7);
            Controls.Add(lb_Last_cloudupdatetime);
            Controls.Add(btn_toggle);
            Controls.Add(panel12);
            Controls.Add(panel11);
            Controls.Add(panel_Swing);
            Controls.Add(panel_Drill);
            Margin = new Padding(6);
            Name = "Home_Page2";
            Text = "視覺化元件監控";
            Load += Main_Load;
            TextChanged += Main_form_TextChanged;
            panel12.ResumeLayout(false);
            tbPanel_Swing_connect.ResumeLayout(false);
            tbPanel_Swing_connect.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            tbPanel_Swing_yellow.ResumeLayout(false);
            tbPanel_Swing_yellow.PerformLayout();
            tbPanel_Swing_sum.ResumeLayout(false);
            tbPanel_Swing_sum.PerformLayout();
            tbPanel_Swing_green.ResumeLayout(false);
            tbPanel_Swing_green.PerformLayout();
            tbPanel_Swing_disconnect.ResumeLayout(false);
            tbPanel_Swing_disconnect.PerformLayout();
            tbPanel_Swing_red.ResumeLayout(false);
            tbPanel_Swing_red.PerformLayout();
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
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            tbPanel_Drill_yellow.ResumeLayout(false);
            tbPanel_Drill_yellow.PerformLayout();
            panel_Swing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            panel_Drill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel9.PerformLayout();
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            panel13.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            panel8.ResumeLayout(false);
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
        private Label lb_Last_cloudupdatetime;
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
        private Panel panel7;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label13;
        private Label label12;
        private Label label11;
        private TableLayoutPanel tableLayoutPanel9;
        private Label label22;
        private Label label23;
        private TableLayoutPanel tableLayoutPanel8;
        private Label label20;
        private Label label21;
        private TableLayoutPanel tableLayoutPanel7;
        private Label label18;
        private Label label19;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label16;
        private Label label17;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label14;
        private Label label15;
        private Panel panel13;
        private Button button1;
        private Button button2;
        private Panel panel9;
        private Panel panel14;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label9;
        private Label label10;
        private Panel panel8;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
    }
}

