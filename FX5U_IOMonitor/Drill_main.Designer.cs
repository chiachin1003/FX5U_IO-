using System.Windows.Forms;

namespace FX5U_IOMonitor
{
    partial class Drill_main
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
            btn_SP1 = new Button();
            btn_OutfeedPnl = new Button();
            btn_InfeedBox = new Button();
            btn_Infeed = new Button();
            btn_Cabinet = new Button();
            btn_Peripheral = new Button();
            btn_Panel = new Button();
            btn_common = new Button();
            btn_SP4 = new Button();
            btn_SP3 = new Button();
            btn_SP2 = new Button();
            btn_infeed_PNL = new Button();
            btn_Outfeed = new Button();
            btn_OutfeedBox = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            label6 = new Label();
            lab_sum = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            lab_red = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            label5 = new Label();
            lab_connect = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            label4 = new Label();
            lab_partalarm = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            label3 = new Label();
            lab_green = new Label();
            tableLayoutPanel6 = new TableLayoutPanel();
            label2 = new Label();
            lab_yellow = new Label();
            lab_connectOK = new Label();
            label_txt = new Label();
            btn_search = new Button();
            txB_search = new TextBox();
            panel6 = new Panel();
            label9 = new Label();
            panel5 = new Panel();
            label8 = new Label();
            panel4 = new Panel();
            label7 = new Label();
            tableLayoutPanel8 = new TableLayoutPanel();
            panel1 = new Panel();
            lab_r = new Label();
            panel2 = new Panel();
            lab_y = new Label();
            panel3 = new Panel();
            lab_g = new Label();
            tableLayoutPanel7 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            SuspendLayout();
            // 
            // btn_SP1
            // 
            btn_SP1.BackColor = SystemColors.ButtonFace;
            btn_SP1.FlatStyle = FlatStyle.Flat;
            btn_SP1.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_SP1.Location = new Point(30, 175);
            btn_SP1.Margin = new Padding(4);
            btn_SP1.Name = "btn_SP1";
            btn_SP1.Size = new Size(115, 55);
            btn_SP1.TabIndex = 0;
            btn_SP1.Text = "主軸1";
            btn_SP1.UseVisualStyleBackColor = false;
            btn_SP1.Click += btn_SP1_Click;
            // 
            // btn_OutfeedPnl
            // 
            btn_OutfeedPnl.BackColor = SystemColors.ButtonFace;
            btn_OutfeedPnl.FlatStyle = FlatStyle.Flat;
            btn_OutfeedPnl.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_OutfeedPnl.Location = new Point(680, 395);
            btn_OutfeedPnl.Margin = new Padding(4);
            btn_OutfeedPnl.Name = "btn_OutfeedPnl";
            btn_OutfeedPnl.Size = new Size(115, 55);
            btn_OutfeedPnl.TabIndex = 1;
            btn_OutfeedPnl.Text = "出料移載平台\r\n操作面板";
            btn_OutfeedPnl.UseVisualStyleBackColor = false;
            btn_OutfeedPnl.Click += btn_OutfeedPnl_Click;
            // 
            // btn_InfeedBox
            // 
            btn_InfeedBox.BackColor = SystemColors.ButtonFace;
            btn_InfeedBox.FlatStyle = FlatStyle.Flat;
            btn_InfeedBox.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_InfeedBox.Location = new Point(290, 395);
            btn_InfeedBox.Margin = new Padding(4);
            btn_InfeedBox.Name = "btn_InfeedBox";
            btn_InfeedBox.Size = new Size(115, 55);
            btn_InfeedBox.TabIndex = 2;
            btn_InfeedBox.Text = "入料移載平台\r\n接線盒";
            btn_InfeedBox.UseVisualStyleBackColor = false;
            btn_InfeedBox.Click += btn_InfeedBox_Click;
            // 
            // btn_Infeed
            // 
            btn_Infeed.BackColor = SystemColors.ButtonFace;
            btn_Infeed.FlatStyle = FlatStyle.Flat;
            btn_Infeed.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_Infeed.Location = new Point(160, 395);
            btn_Infeed.Margin = new Padding(4);
            btn_Infeed.Name = "btn_Infeed";
            btn_Infeed.Size = new Size(115, 55);
            btn_Infeed.TabIndex = 3;
            btn_Infeed.Text = "入料移載平台";
            btn_Infeed.UseVisualStyleBackColor = false;
            btn_Infeed.Click += btn_Infeed_Click;
            // 
            // btn_Cabinet
            // 
            btn_Cabinet.BackColor = SystemColors.ButtonFace;
            btn_Cabinet.FlatStyle = FlatStyle.Flat;
            btn_Cabinet.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_Cabinet.Location = new Point(30, 395);
            btn_Cabinet.Margin = new Padding(4);
            btn_Cabinet.Name = "btn_Cabinet";
            btn_Cabinet.Size = new Size(115, 55);
            btn_Cabinet.TabIndex = 4;
            btn_Cabinet.Text = "主電氣箱";
            btn_Cabinet.UseVisualStyleBackColor = false;
            btn_Cabinet.Click += btn_Cabinet_Click;
            // 
            // btn_Peripheral
            // 
            btn_Peripheral.BackColor = SystemColors.ButtonFace;
            btn_Peripheral.FlatStyle = FlatStyle.Flat;
            btn_Peripheral.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_Peripheral.Location = new Point(810, 175);
            btn_Peripheral.Margin = new Padding(4);
            btn_Peripheral.Name = "btn_Peripheral";
            btn_Peripheral.Size = new Size(115, 55);
            btn_Peripheral.TabIndex = 5;
            btn_Peripheral.Text = "週邊設備";
            btn_Peripheral.UseVisualStyleBackColor = false;
            btn_Peripheral.Click += btn_Peripheral_Click;
            // 
            // btn_Panel
            // 
            btn_Panel.BackColor = SystemColors.ButtonFace;
            btn_Panel.FlatStyle = FlatStyle.Flat;
            btn_Panel.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_Panel.Location = new Point(680, 175);
            btn_Panel.Margin = new Padding(4);
            btn_Panel.Name = "btn_Panel";
            btn_Panel.Size = new Size(115, 55);
            btn_Panel.TabIndex = 6;
            btn_Panel.Text = "操作面板";
            btn_Panel.UseVisualStyleBackColor = false;
            btn_Panel.Click += btn_Panel_Click;
            // 
            // btn_common
            // 
            btn_common.BackColor = SystemColors.ButtonFace;
            btn_common.FlatStyle = FlatStyle.Flat;
            btn_common.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_common.Location = new Point(550, 175);
            btn_common.Margin = new Padding(4);
            btn_common.Name = "btn_common";
            btn_common.Size = new Size(115, 55);
            btn_common.TabIndex = 7;
            btn_common.Text = "共用介面";
            btn_common.UseVisualStyleBackColor = false;
            btn_common.Click += btn_common_Click;
            // 
            // btn_SP4
            // 
            btn_SP4.BackColor = SystemColors.ButtonFace;
            btn_SP4.FlatStyle = FlatStyle.Flat;
            btn_SP4.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_SP4.Location = new Point(420, 175);
            btn_SP4.Margin = new Padding(4);
            btn_SP4.Name = "btn_SP4";
            btn_SP4.Size = new Size(115, 55);
            btn_SP4.TabIndex = 8;
            btn_SP4.Text = "夾持軸";
            btn_SP4.UseVisualStyleBackColor = false;
            btn_SP4.Click += btn_SP4_Click;
            // 
            // btn_SP3
            // 
            btn_SP3.BackColor = SystemColors.ButtonFace;
            btn_SP3.FlatStyle = FlatStyle.Flat;
            btn_SP3.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_SP3.Location = new Point(290, 175);
            btn_SP3.Margin = new Padding(4);
            btn_SP3.Name = "btn_SP3";
            btn_SP3.Size = new Size(115, 55);
            btn_SP3.TabIndex = 9;
            btn_SP3.Text = "主軸3";
            btn_SP3.UseVisualStyleBackColor = false;
            btn_SP3.Click += btn_SP3_Click;
            // 
            // btn_SP2
            // 
            btn_SP2.BackColor = SystemColors.ButtonFace;
            btn_SP2.FlatStyle = FlatStyle.Flat;
            btn_SP2.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_SP2.Location = new Point(160, 175);
            btn_SP2.Margin = new Padding(4);
            btn_SP2.Name = "btn_SP2";
            btn_SP2.Size = new Size(115, 55);
            btn_SP2.TabIndex = 10;
            btn_SP2.Text = "主軸2";
            btn_SP2.UseVisualStyleBackColor = false;
            btn_SP2.Click += btn_SP2_Click;
            // 
            // btn_infeed_PNL
            // 
            btn_infeed_PNL.BackColor = SystemColors.ButtonFace;
            btn_infeed_PNL.FlatStyle = FlatStyle.Flat;
            btn_infeed_PNL.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_infeed_PNL.Location = new Point(420, 395);
            btn_infeed_PNL.Margin = new Padding(4);
            btn_infeed_PNL.Name = "btn_infeed_PNL";
            btn_infeed_PNL.Size = new Size(115, 55);
            btn_infeed_PNL.TabIndex = 11;
            btn_infeed_PNL.Text = "入料移載平台\r\n操作面板";
            btn_infeed_PNL.UseVisualStyleBackColor = false;
            btn_infeed_PNL.Click += button11_Click;
            // 
            // btn_Outfeed
            // 
            btn_Outfeed.BackColor = SystemColors.ButtonFace;
            btn_Outfeed.FlatStyle = FlatStyle.Flat;
            btn_Outfeed.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_Outfeed.Location = new Point(550, 395);
            btn_Outfeed.Margin = new Padding(4);
            btn_Outfeed.Name = "btn_Outfeed";
            btn_Outfeed.Size = new Size(115, 55);
            btn_Outfeed.TabIndex = 12;
            btn_Outfeed.Text = "出料移載平台";
            btn_Outfeed.UseVisualStyleBackColor = false;
            btn_Outfeed.Click += btn_Outfeed_Click;
            // 
            // btn_OutfeedBox
            // 
            btn_OutfeedBox.BackColor = SystemColors.ButtonFace;
            btn_OutfeedBox.FlatStyle = FlatStyle.Flat;
            btn_OutfeedBox.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btn_OutfeedBox.Location = new Point(810, 395);
            btn_OutfeedBox.Margin = new Padding(4);
            btn_OutfeedBox.Name = "btn_OutfeedBox";
            btn_OutfeedBox.Size = new Size(115, 55);
            btn_OutfeedBox.TabIndex = 13;
            btn_OutfeedBox.Text = "出料移載平台接線盒";
            btn_OutfeedBox.UseVisualStyleBackColor = false;
            btn_OutfeedBox.Click += btn_OutfeedBox_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label6, 0, 1);
            tableLayoutPanel1.Controls.Add(lab_sum, 0, 0);
            tableLayoutPanel1.Location = new Point(25, 482);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tableLayoutPanel1.Size = new Size(118, 108);
            tableLayoutPanel1.TabIndex = 21;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ButtonFace;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label6.Location = new Point(3, 59);
            label6.Name = "label6";
            label6.Size = new Size(112, 49);
            label6.TabIndex = 23;
            label6.Text = "監控總數";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            label6.Click += label6_Click;
            // 
            // lab_sum
            // 
            lab_sum.AutoSize = true;
            lab_sum.BackColor = SystemColors.ButtonFace;
            lab_sum.Dock = DockStyle.Fill;
            lab_sum.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_sum.Location = new Point(3, 0);
            lab_sum.Name = "lab_sum";
            lab_sum.Size = new Size(112, 59);
            lab_sum.TabIndex = 21;
            lab_sum.TextAlign = ContentAlignment.MiddleCenter;
            lab_sum.Click += lab_sum_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(label1, 0, 1);
            tableLayoutPanel2.Controls.Add(lab_red, 0, 0);
            tableLayoutPanel2.Location = new Point(807, 482);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tableLayoutPanel2.Size = new Size(118, 108);
            tableLayoutPanel2.TabIndex = 22;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonFace;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label1.Location = new Point(3, 59);
            label1.Name = "label1";
            label1.Size = new Size(112, 49);
            label1.TabIndex = 22;
            label1.Text = "紅燈總數";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_red
            // 
            lab_red.AutoSize = true;
            lab_red.BackColor = SystemColors.ButtonFace;
            lab_red.Dock = DockStyle.Fill;
            lab_red.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_red.Location = new Point(3, 0);
            lab_red.Name = "lab_red";
            lab_red.Size = new Size(112, 59);
            lab_red.TabIndex = 21;
            lab_red.TextAlign = ContentAlignment.MiddleCenter;
            lab_red.Click += lab_red_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(label5, 0, 1);
            tableLayoutPanel3.Controls.Add(lab_connect, 0, 0);
            tableLayoutPanel3.Location = new Point(180, 482);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tableLayoutPanel3.Size = new Size(118, 108);
            tableLayoutPanel3.TabIndex = 23;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ButtonFace;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label5.Location = new Point(3, 59);
            label5.Name = "label5";
            label5.Size = new Size(112, 49);
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
            lab_connect.Size = new Size(112, 59);
            lab_connect.TabIndex = 21;
            lab_connect.TextAlign = ContentAlignment.MiddleCenter;
            lab_connect.Click += lab_connect_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(label4, 0, 1);
            tableLayoutPanel4.Controls.Add(lab_partalarm, 0, 0);
            tableLayoutPanel4.Location = new Point(336, 482);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tableLayoutPanel4.Size = new Size(118, 108);
            tableLayoutPanel4.TabIndex = 24;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ButtonFace;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label4.Location = new Point(3, 59);
            label4.Name = "label4";
            label4.Size = new Size(112, 49);
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
            lab_partalarm.Size = new Size(112, 59);
            lab_partalarm.TabIndex = 21;
            lab_partalarm.TextAlign = ContentAlignment.MiddleCenter;
            lab_partalarm.Click += lab_partalarm_Click;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(label3, 0, 1);
            tableLayoutPanel5.Controls.Add(lab_green, 0, 0);
            tableLayoutPanel5.Location = new Point(492, 482);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tableLayoutPanel5.Size = new Size(118, 108);
            tableLayoutPanel5.TabIndex = 25;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonFace;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label3.Location = new Point(3, 59);
            label3.Name = "label3";
            label3.Size = new Size(112, 49);
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
            lab_green.Size = new Size(112, 59);
            lab_green.TabIndex = 21;
            lab_green.TextAlign = ContentAlignment.MiddleCenter;
            lab_green.Click += lab_green_Click;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(label2, 0, 1);
            tableLayoutPanel6.Controls.Add(lab_yellow, 0, 0);
            tableLayoutPanel6.Location = new Point(647, 482);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tableLayoutPanel6.Size = new Size(118, 108);
            tableLayoutPanel6.TabIndex = 26;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonFace;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label2.Location = new Point(3, 59);
            label2.Name = "label2";
            label2.Size = new Size(112, 49);
            label2.TabIndex = 23;
            label2.Text = "黃燈總數";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_yellow
            // 
            lab_yellow.AutoSize = true;
            lab_yellow.BackColor = SystemColors.ButtonFace;
            lab_yellow.Dock = DockStyle.Fill;
            lab_yellow.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yellow.Location = new Point(3, 0);
            lab_yellow.Name = "lab_yellow";
            lab_yellow.Size = new Size(112, 59);
            lab_yellow.TabIndex = 21;
            lab_yellow.TextAlign = ContentAlignment.MiddleCenter;
            lab_yellow.Click += lab_yellow_Click;
            // 
            // lab_connectOK
            // 
            lab_connectOK.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_connectOK.AutoSize = true;
            lab_connectOK.BackColor = SystemColors.ButtonFace;
            lab_connectOK.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_connectOK.Location = new Point(12, 604);
            lab_connectOK.Name = "lab_connectOK";
            lab_connectOK.Size = new Size(75, 26);
            lab_connectOK.TabIndex = 27;
            lab_connectOK.Text = "未連接";
            lab_connectOK.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_txt
            // 
            label_txt.AutoSize = true;
            label_txt.Font = new Font("微軟正黑體", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label_txt.Location = new Point(549, 20);
            label_txt.Margin = new Padding(4, 0, 4, 0);
            label_txt.Name = "label_txt";
            label_txt.Size = new Size(73, 17);
            label_txt.TabIndex = 29;
            label_txt.Text = "文字搜尋：";
            label_txt.TextAlign = ContentAlignment.MiddleCenter;
            label_txt.Click += label_txt_Click;
            // 
            // btn_search
            // 
            btn_search.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_search.Location = new Point(849, 13);
            btn_search.Margin = new Padding(4);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(73, 28);
            btn_search.TabIndex = 28;
            btn_search.Text = "搜尋";
            btn_search.UseVisualStyleBackColor = true;
            btn_search.Click += btn_search_Click;
            // 
            // txB_search
            // 
            txB_search.Location = new Point(623, 16);
            txB_search.Margin = new Padding(4);
            txB_search.Name = "txB_search";
            txB_search.Size = new Size(221, 23);
            txB_search.TabIndex = 30;
            // 
            // panel6
            // 
            panel6.BackColor = Color.Red;
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(3, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(13, 16);
            panel6.TabIndex = 38;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = DockStyle.Fill;
            label9.Location = new Point(22, 0);
            label9.Name = "label9";
            label9.Size = new Size(13, 22);
            label9.TabIndex = 44;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Yellow;
            panel5.Location = new Point(41, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(13, 15);
            panel5.TabIndex = 45;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Dock = DockStyle.Fill;
            label8.Location = new Point(60, 0);
            label8.Name = "label8";
            label8.Size = new Size(13, 22);
            label8.TabIndex = 46;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Green;
            panel4.Location = new Point(79, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(13, 15);
            panel4.TabIndex = 47;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Location = new Point(98, 0);
            label7.Name = "label7";
            label7.Size = new Size(14, 22);
            label7.TabIndex = 48;
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
            tableLayoutPanel8.Controls.Add(label7, 5, 0);
            tableLayoutPanel8.Controls.Add(panel4, 4, 0);
            tableLayoutPanel8.Controls.Add(label8, 3, 0);
            tableLayoutPanel8.Controls.Add(panel5, 2, 0);
            tableLayoutPanel8.Controls.Add(label9, 1, 0);
            tableLayoutPanel8.Controls.Add(panel6, 0, 0);
            tableLayoutPanel8.Location = new Point(30, 368);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel8.Size = new Size(115, 22);
            tableLayoutPanel8.TabIndex = 46;
            tableLayoutPanel8.Visible = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Red;
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(13, 16);
            panel1.TabIndex = 38;
            // 
            // lab_r
            // 
            lab_r.AutoSize = true;
            lab_r.Dock = DockStyle.Fill;
            lab_r.Font = new Font("Microsoft JhengHei UI", 6F, FontStyle.Regular, GraphicsUnit.Point, 136);
            lab_r.Location = new Point(22, 0);
            lab_r.Name = "lab_r";
            lab_r.Size = new Size(13, 22);
            lab_r.TabIndex = 44;
            lab_r.Text = "12";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Yellow;
            panel2.Location = new Point(41, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(13, 15);
            panel2.TabIndex = 45;
            // 
            // lab_y
            // 
            lab_y.AutoSize = true;
            lab_y.Dock = DockStyle.Fill;
            lab_y.Location = new Point(60, 0);
            lab_y.Name = "lab_y";
            lab_y.Size = new Size(13, 22);
            lab_y.TabIndex = 46;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Green;
            panel3.Location = new Point(79, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(13, 15);
            panel3.TabIndex = 47;
            // 
            // lab_g
            // 
            lab_g.AutoSize = true;
            lab_g.Dock = DockStyle.Fill;
            lab_g.Location = new Point(98, 0);
            lab_g.Name = "lab_g";
            lab_g.Size = new Size(14, 22);
            lab_g.TabIndex = 48;
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
            tableLayoutPanel7.Controls.Add(lab_g, 5, 0);
            tableLayoutPanel7.Controls.Add(panel3, 4, 0);
            tableLayoutPanel7.Controls.Add(lab_y, 3, 0);
            tableLayoutPanel7.Controls.Add(panel2, 2, 0);
            tableLayoutPanel7.Controls.Add(lab_r, 1, 0);
            tableLayoutPanel7.Controls.Add(panel1, 0, 0);
            tableLayoutPanel7.Location = new Point(30, 150);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel7.Size = new Size(115, 22);
            tableLayoutPanel7.TabIndex = 45;
            tableLayoutPanel7.Visible = false;
            // 
            // Drill_main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(936, 664);
            Controls.Add(tableLayoutPanel8);
            Controls.Add(tableLayoutPanel7);
            Controls.Add(label_txt);
            Controls.Add(btn_search);
            Controls.Add(txB_search);
            Controls.Add(lab_connectOK);
            Controls.Add(tableLayoutPanel6);
            Controls.Add(tableLayoutPanel5);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btn_OutfeedBox);
            Controls.Add(btn_Outfeed);
            Controls.Add(btn_infeed_PNL);
            Controls.Add(btn_SP2);
            Controls.Add(btn_SP3);
            Controls.Add(btn_SP4);
            Controls.Add(btn_common);
            Controls.Add(btn_Panel);
            Controls.Add(btn_Peripheral);
            Controls.Add(btn_Cabinet);
            Controls.Add(btn_Infeed);
            Controls.Add(btn_InfeedBox);
            Controls.Add(btn_OutfeedPnl);
            Controls.Add(btn_SP1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "Drill_main";
            Text = "u";
            Load += Drill_main_Load;
            VisibleChanged += Drill_main_VisibleChanged;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_SP1;
        private Button btn_OutfeedPnl;
        private Button btn_InfeedBox;
        private Button btn_Infeed;
        private Button btn_Cabinet;
        private Button btn_Peripheral;
        private Button btn_Panel;
        private Button btn_common;
        private Button btn_SP4;
        private Button btn_SP3;
        private Button btn_SP2;
        private Button btn_infeed_PNL;
        private Button btn_Outfeed;
        private Button btn_OutfeedBox;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lab_sum;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lab_red;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lab_connect;
        private TableLayoutPanel tableLayoutPanel4;
        private Label lab_partalarm;
        private TableLayoutPanel tableLayoutPanel5;
        private Label lab_green;
        private TableLayoutPanel tableLayoutPanel6;
        private Label lab_yellow;
        private Label label6;
        private Label label1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label lab_connectOK;
        private Label label_txt;
        private Button btn_search;
        private TextBox txB_search;
        private Panel panel6;
        private Label label9;
        private Panel panel5;
        private Label label8;
        private Panel panel4;
        private Label label7;
        private TableLayoutPanel tableLayoutPanel8;
        private Panel panel1;
        private Label lab_r;
        private Panel panel2;
        private Label lab_y;
        private Panel panel3;
        private Label lab_g;
        private TableLayoutPanel tableLayoutPanel7;
    }
}