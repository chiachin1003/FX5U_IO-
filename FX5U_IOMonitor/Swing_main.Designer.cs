using System.Windows.Forms;

namespace FX5U_IOMonitor
{
    partial class Swing_main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_cabinet = new Button();
            tableLayoutPanel6 = new TableLayoutPanel();
            label2 = new Label();
            lab_yellow = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            label3 = new Label();
            lab_green = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            label4 = new Label();
            lab_partalarm = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            label5 = new Label();
            lab_connect = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            lab_red = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label6 = new Label();
            lab_sum = new Label();
            btn_common = new Button();
            btn_panel = new Button();
            lab_connectOK = new Label();
            label_txt = new Label();
            btn_search = new Button();
            txB_search = new TextBox();
            tableLayoutPanel7 = new TableLayoutPanel();
            panel1 = new Panel();
            panel2 = new Panel();
            lab_r = new Label();
            label7 = new Label();
            panel3 = new Panel();
            label8 = new Label();
            tableLayoutPanel8 = new TableLayoutPanel();
            label9 = new Label();
            panel4 = new Panel();
            label10 = new Label();
            panel5 = new Panel();
            label11 = new Label();
            panel6 = new Panel();
            tableLayoutPanel9 = new TableLayoutPanel();
            label12 = new Label();
            panel7 = new Panel();
            label13 = new Label();
            panel8 = new Panel();
            label14 = new Label();
            panel9 = new Panel();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            SuspendLayout();
            // 
            // btn_cabinet
            // 
            btn_cabinet.FlatStyle = FlatStyle.Flat;
            btn_cabinet.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_cabinet.Location = new Point(718, 327);
            btn_cabinet.Margin = new Padding(4);
            btn_cabinet.Name = "btn_cabinet";
            btn_cabinet.RightToLeft = RightToLeft.Yes;
            btn_cabinet.Size = new Size(130, 40);
            btn_cabinet.TabIndex = 2;
            btn_cabinet.Text = "主電器箱";
            btn_cabinet.UseVisualStyleBackColor = true;
            btn_cabinet.Click += btn_cabinet_Click;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Controls.Add(label2, 0, 1);
            tableLayoutPanel6.Controls.Add(lab_yellow, 0, 0);
            tableLayoutPanel6.Location = new Point(647, 482);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.Size = new Size(118, 108);
            tableLayoutPanel6.TabIndex = 32;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonFace;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label2.Location = new Point(3, 68);
            label2.Name = "label2";
            label2.Size = new Size(112, 40);
            label2.TabIndex = 23;
            label2.Text = "黃燈總數";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_yellow
            // 
            lab_yellow.AutoSize = true;
            lab_yellow.BackColor = SystemColors.ButtonFace;
            lab_yellow.Dock = DockStyle.Fill;
            lab_yellow.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_yellow.Location = new Point(3, 0);
            lab_yellow.Name = "lab_yellow";
            lab_yellow.Size = new Size(112, 68);
            lab_yellow.TabIndex = 21;
            lab_yellow.TextAlign = ContentAlignment.MiddleCenter;
            lab_yellow.Click += lab_yellow_Click;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(label3, 0, 1);
            tableLayoutPanel5.Controls.Add(lab_green, 0, 0);
            tableLayoutPanel5.Location = new Point(492, 482);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel5.Size = new Size(118, 108);
            tableLayoutPanel5.TabIndex = 31;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonFace;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label3.Location = new Point(3, 68);
            label3.Name = "label3";
            label3.Size = new Size(112, 40);
            label3.TabIndex = 23;
            label3.Text = "綠燈總數";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_green
            // 
            lab_green.AutoSize = true;
            lab_green.BackColor = SystemColors.ButtonFace;
            lab_green.Dock = DockStyle.Fill;
            lab_green.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_green.Location = new Point(3, 0);
            lab_green.Name = "lab_green";
            lab_green.Size = new Size(112, 68);
            lab_green.TabIndex = 21;
            lab_green.TextAlign = ContentAlignment.MiddleCenter;
            lab_green.Click += lab_green_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(label4, 0, 1);
            tableLayoutPanel4.Controls.Add(lab_partalarm, 0, 0);
            tableLayoutPanel4.Location = new Point(336, 482);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(118, 108);
            tableLayoutPanel4.TabIndex = 30;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ButtonFace;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label4.Location = new Point(3, 68);
            label4.Name = "label4";
            label4.Size = new Size(112, 40);
            label4.TabIndex = 23;
            label4.Text = "元件異常";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_partalarm
            // 
            lab_partalarm.AutoSize = true;
            lab_partalarm.BackColor = SystemColors.ButtonFace;
            lab_partalarm.Dock = DockStyle.Fill;
            lab_partalarm.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_partalarm.Location = new Point(3, 0);
            lab_partalarm.Name = "lab_partalarm";
            lab_partalarm.Size = new Size(112, 68);
            lab_partalarm.TabIndex = 21;
            lab_partalarm.TextAlign = ContentAlignment.MiddleCenter;
            lab_partalarm.Click += lab_partalarm_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(label5, 0, 1);
            tableLayoutPanel3.Controls.Add(lab_connect, 0, 0);
            tableLayoutPanel3.Location = new Point(180, 482);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(118, 108);
            tableLayoutPanel3.TabIndex = 29;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ButtonFace;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label5.Location = new Point(3, 68);
            label5.Name = "label5";
            label5.Size = new Size(112, 40);
            label5.TabIndex = 23;
            label5.Text = "連接總數";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_connect
            // 
            lab_connect.AutoSize = true;
            lab_connect.BackColor = SystemColors.ButtonFace;
            lab_connect.Dock = DockStyle.Fill;
            lab_connect.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_connect.Location = new Point(3, 0);
            lab_connect.Name = "lab_connect";
            lab_connect.Size = new Size(112, 68);
            lab_connect.TabIndex = 21;
            lab_connect.TextAlign = ContentAlignment.MiddleCenter;
            lab_connect.Click += lab_connect_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(label1, 0, 1);
            tableLayoutPanel2.Controls.Add(lab_red, 0, 0);
            tableLayoutPanel2.Location = new Point(807, 482);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(118, 108);
            tableLayoutPanel2.TabIndex = 28;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonFace;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label1.Location = new Point(3, 68);
            label1.Name = "label1";
            label1.Size = new Size(112, 40);
            label1.TabIndex = 22;
            label1.Text = "紅燈總數";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_red
            // 
            lab_red.AutoSize = true;
            lab_red.BackColor = SystemColors.ButtonFace;
            lab_red.Dock = DockStyle.Fill;
            lab_red.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_red.Location = new Point(3, 0);
            lab_red.Name = "lab_red";
            lab_red.Size = new Size(112, 68);
            lab_red.TabIndex = 21;
            lab_red.TextAlign = ContentAlignment.MiddleCenter;
            lab_red.Click += lab_red_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label6, 0, 1);
            tableLayoutPanel1.Controls.Add(lab_sum, 0, 0);
            tableLayoutPanel1.Location = new Point(25, 482);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 63.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 36.6666679F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(118, 108);
            tableLayoutPanel1.TabIndex = 27;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ButtonFace;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label6.Location = new Point(3, 68);
            label6.Name = "label6";
            label6.Size = new Size(112, 40);
            label6.TabIndex = 23;
            label6.Text = "監控總數";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_sum
            // 
            lab_sum.AutoSize = true;
            lab_sum.BackColor = SystemColors.ButtonFace;
            lab_sum.Dock = DockStyle.Fill;
            lab_sum.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_sum.Location = new Point(3, 0);
            lab_sum.Name = "lab_sum";
            lab_sum.Size = new Size(112, 68);
            lab_sum.TabIndex = 21;
            lab_sum.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_common
            // 
            btn_common.FlatStyle = FlatStyle.Flat;
            btn_common.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_common.Location = new Point(106, 327);
            btn_common.Margin = new Padding(4);
            btn_common.Name = "btn_common";
            btn_common.RightToLeft = RightToLeft.Yes;
            btn_common.Size = new Size(130, 40);
            btn_common.TabIndex = 0;
            btn_common.Text = "共用元件";
            btn_common.UseVisualStyleBackColor = true;
            btn_common.Click += btn_common_Click;
            // 
            // btn_panel
            // 
            btn_panel.FlatStyle = FlatStyle.Flat;
            btn_panel.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_panel.Location = new Point(406, 327);
            btn_panel.Margin = new Padding(4);
            btn_panel.Name = "btn_panel";
            btn_panel.RightToLeft = RightToLeft.Yes;
            btn_panel.Size = new Size(130, 40);
            btn_panel.TabIndex = 1;
            btn_panel.Text = "操作面板";
            btn_panel.UseVisualStyleBackColor = true;
            btn_panel.Click += btn_panel_Click;
            // 
            // lab_connectOK
            // 
            lab_connectOK.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_connectOK.AutoSize = true;
            lab_connectOK.BackColor = SystemColors.ButtonFace;
            lab_connectOK.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_connectOK.Location = new Point(12, 604);
            lab_connectOK.Name = "lab_connectOK";
            lab_connectOK.Size = new Size(75, 26);
            lab_connectOK.TabIndex = 33;
            lab_connectOK.Text = "未連接";
            lab_connectOK.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_txt
            // 
            label_txt.AutoSize = true;
            label_txt.Font = new Font("微軟正黑體", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label_txt.Location = new Point(587, 17);
            label_txt.Margin = new Padding(4, 0, 4, 0);
            label_txt.Name = "label_txt";
            label_txt.Size = new Size(73, 17);
            label_txt.TabIndex = 35;
            label_txt.Text = "文字搜尋：";
            label_txt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_search
            // 
            btn_search.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_search.Location = new Point(887, 10);
            btn_search.Margin = new Padding(4);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(44, 28);
            btn_search.TabIndex = 34;
            btn_search.Text = "搜尋";
            btn_search.UseVisualStyleBackColor = true;
            btn_search.Click += btn_search_Click;
            // 
            // txB_search
            // 
            txB_search.Location = new Point(661, 13);
            txB_search.Margin = new Padding(4);
            txB_search.Name = "txB_search";
            txB_search.Size = new Size(221, 23);
            txB_search.TabIndex = 36;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 6;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel7.Controls.Add(label8, 5, 0);
            tableLayoutPanel7.Controls.Add(panel3, 4, 0);
            tableLayoutPanel7.Controls.Add(label7, 3, 0);
            tableLayoutPanel7.Controls.Add(panel2, 2, 0);
            tableLayoutPanel7.Controls.Add(lab_r, 1, 0);
            tableLayoutPanel7.Controls.Add(panel1, 0, 0);
            tableLayoutPanel7.Location = new Point(106, 298);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel7.Size = new Size(130, 22);
            tableLayoutPanel7.TabIndex = 42;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Red;
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(15, 16);
            panel1.TabIndex = 38;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Yellow;
            panel2.Location = new Point(45, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(13, 15);
            panel2.TabIndex = 45;
            // 
            // lab_r
            // 
            lab_r.AutoSize = true;
            lab_r.Dock = DockStyle.Fill;
            lab_r.Location = new Point(24, 0);
            lab_r.Name = "lab_r";
            lab_r.Size = new Size(15, 22);
            lab_r.TabIndex = 44;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Location = new Point(66, 0);
            label7.Name = "label7";
            label7.Size = new Size(15, 22);
            label7.TabIndex = 46;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Green;
            panel3.Location = new Point(87, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(13, 15);
            panel3.TabIndex = 47;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Dock = DockStyle.Fill;
            label8.Location = new Point(108, 0);
            label8.Name = "label8";
            label8.Size = new Size(19, 22);
            label8.TabIndex = 48;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 6;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel8.Controls.Add(label9, 5, 0);
            tableLayoutPanel8.Controls.Add(panel4, 4, 0);
            tableLayoutPanel8.Controls.Add(label10, 3, 0);
            tableLayoutPanel8.Controls.Add(panel5, 2, 0);
            tableLayoutPanel8.Controls.Add(label11, 1, 0);
            tableLayoutPanel8.Controls.Add(panel6, 0, 0);
            tableLayoutPanel8.Location = new Point(406, 301);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel8.Size = new Size(130, 22);
            tableLayoutPanel8.TabIndex = 43;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = DockStyle.Fill;
            label9.Location = new Point(108, 0);
            label9.Name = "label9";
            label9.Size = new Size(19, 22);
            label9.TabIndex = 48;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Green;
            panel4.Location = new Point(87, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(13, 15);
            panel4.TabIndex = 47;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Dock = DockStyle.Fill;
            label10.Location = new Point(66, 0);
            label10.Name = "label10";
            label10.Size = new Size(15, 22);
            label10.TabIndex = 46;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Yellow;
            panel5.Location = new Point(45, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(13, 15);
            panel5.TabIndex = 45;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Dock = DockStyle.Fill;
            label11.Location = new Point(24, 0);
            label11.Name = "label11";
            label11.Size = new Size(15, 22);
            label11.TabIndex = 44;
            // 
            // panel6
            // 
            panel6.BackColor = Color.Red;
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(3, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(15, 16);
            panel6.TabIndex = 38;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.ColumnCount = 6;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel9.Controls.Add(label12, 5, 0);
            tableLayoutPanel9.Controls.Add(panel7, 4, 0);
            tableLayoutPanel9.Controls.Add(label13, 3, 0);
            tableLayoutPanel9.Controls.Add(panel8, 2, 0);
            tableLayoutPanel9.Controls.Add(label14, 1, 0);
            tableLayoutPanel9.Controls.Add(panel9, 0, 0);
            tableLayoutPanel9.Location = new Point(718, 301);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel9.Size = new Size(130, 22);
            tableLayoutPanel9.TabIndex = 44;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Dock = DockStyle.Fill;
            label12.Location = new Point(108, 0);
            label12.Name = "label12";
            label12.Size = new Size(19, 22);
            label12.TabIndex = 48;
            // 
            // panel7
            // 
            panel7.BackColor = Color.Green;
            panel7.Location = new Point(87, 3);
            panel7.Name = "panel7";
            panel7.Size = new Size(13, 15);
            panel7.TabIndex = 47;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Dock = DockStyle.Fill;
            label13.Location = new Point(66, 0);
            label13.Name = "label13";
            label13.Size = new Size(15, 22);
            label13.TabIndex = 46;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Yellow;
            panel8.Location = new Point(45, 3);
            panel8.Name = "panel8";
            panel8.Size = new Size(13, 15);
            panel8.TabIndex = 45;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Dock = DockStyle.Fill;
            label14.Location = new Point(24, 0);
            label14.Name = "label14";
            label14.Size = new Size(15, 22);
            label14.TabIndex = 44;
            // 
            // panel9
            // 
            panel9.BackColor = Color.Red;
            panel9.Dock = DockStyle.Fill;
            panel9.Location = new Point(3, 3);
            panel9.Name = "panel9";
            panel9.Size = new Size(15, 16);
            panel9.TabIndex = 38;
            // 
            // Swing_main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(936, 664);
            Controls.Add(tableLayoutPanel9);
            Controls.Add(tableLayoutPanel8);
            Controls.Add(tableLayoutPanel7);
            Controls.Add(label_txt);
            Controls.Add(btn_search);
            Controls.Add(txB_search);
            Controls.Add(lab_connectOK);
            Controls.Add(btn_cabinet);
            Controls.Add(btn_panel);
            Controls.Add(btn_common);
            Controls.Add(tableLayoutPanel6);
            Controls.Add(tableLayoutPanel5);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "Swing_main";
            Text = "s";
            Load += Swing_main_Load;
            VisibleChanged += Swing_main_VisibleChanged;
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel9.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_cabinet;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label2;
        private Label lab_yellow;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label3;
        private Label lab_green;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label4;
        private Label lab_partalarm;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label5;
        private Label lab_connect;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Label lab_red;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label6;
        private Label lab_sum;
        private Button btn_common;
        private Button btn_panel;
        private Label lab_connectOK;
        private Label label_txt;
        private Button btn_search;
        private TextBox txB_search;
        private TableLayoutPanel tableLayoutPanel7;
        private Panel panel1;
        private Label label8;
        private Panel panel3;
        private Label label7;
        private Panel panel2;
        private Label lab_r;
        private TableLayoutPanel tableLayoutPanel8;
        private Label label9;
        private Panel panel4;
        private Label label10;
        private Panel panel5;
        private Label label11;
        private Panel panel6;
        private TableLayoutPanel tableLayoutPanel9;
        private Label label12;
        private Panel panel7;
        private Label label13;
        private Panel panel8;
        private Label label14;
        private Panel panel9;
    }
}