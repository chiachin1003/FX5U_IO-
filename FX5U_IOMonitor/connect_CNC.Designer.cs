using System.Windows.Forms;

namespace FX5U_IOMonitor
{
    partial class Connect_CNC
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
            panel_Ethernet = new Panel();
            label3 = new Label();
            panel2 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            lab_5 = new Label();
            label11 = new Label();
            lab_4 = new Label();
            label9 = new Label();
            lab_Z = new Label();
            label7 = new Label();
            lab_Y = new Label();
            label4 = new Label();
            lab_X = new Label();
            label1 = new Label();
            btn_disconnect_ethernet = new Button();
            btn_connect_ethernet = new Button();
            txb_port = new TextBox();
            txb_IP = new TextBox();
            lab_Enthernetport = new Label();
            label_IP = new Label();
            panel_RS485 = new Panel();
            lab_Error = new Label();
            label8 = new Label();
            txb_file_name = new TextBox();
            txb_file_address = new TextBox();
            label6 = new Label();
            btn_disconnect_RS485 = new Button();
            button_FILE = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            comb_language = new ComboBox();
            label2 = new Label();
            label5 = new Label();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            connect_choose = new ComboBox();
            lab_Type = new Label();
            control_choose = new ComboBox();
            lab_MachineType = new Label();
            btn_mishubishi = new Button();
            panel_Ethernet.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel_RS485.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel_Ethernet
            // 
            panel_Ethernet.BackColor = SystemColors.ButtonHighlight;
            panel_Ethernet.Controls.Add(label3);
            panel_Ethernet.Controls.Add(panel2);
            panel_Ethernet.Controls.Add(btn_disconnect_ethernet);
            panel_Ethernet.Controls.Add(btn_connect_ethernet);
            panel_Ethernet.Controls.Add(txb_port);
            panel_Ethernet.Controls.Add(txb_IP);
            panel_Ethernet.Controls.Add(lab_Enthernetport);
            panel_Ethernet.Controls.Add(label_IP);
            panel_Ethernet.Location = new Point(56, 211);
            panel_Ethernet.Name = "panel_Ethernet";
            panel_Ethernet.Size = new Size(580, 363);
            panel_Ethernet.TabIndex = 16;
            panel_Ethernet.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonHighlight;
            label3.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            label3.Location = new Point(358, 23);
            label3.Name = "label3";
            label3.Size = new Size(180, 26);
            label3.TabIndex = 8;
            label3.Text = "監控機械座標值：";
            // 
            // panel2
            // 
            panel2.Controls.Add(tableLayoutPanel2);
            panel2.Location = new Point(358, 80);
            panel2.Name = "panel2";
            panel2.Size = new Size(181, 208);
            panel2.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.7569065F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.2430954F));
            tableLayoutPanel2.Controls.Add(lab_5, 1, 4);
            tableLayoutPanel2.Controls.Add(label11, 0, 4);
            tableLayoutPanel2.Controls.Add(lab_4, 1, 3);
            tableLayoutPanel2.Controls.Add(label9, 0, 3);
            tableLayoutPanel2.Controls.Add(lab_Z, 1, 2);
            tableLayoutPanel2.Controls.Add(label7, 0, 2);
            tableLayoutPanel2.Controls.Add(lab_Y, 1, 1);
            tableLayoutPanel2.Controls.Add(label4, 0, 1);
            tableLayoutPanel2.Controls.Add(lab_X, 1, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.Size = new Size(181, 203);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // lab_5
            // 
            lab_5.AutoSize = true;
            lab_5.BackColor = SystemColors.ButtonHighlight;
            lab_5.Dock = DockStyle.Fill;
            lab_5.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            lab_5.Location = new Point(47, 161);
            lab_5.Name = "lab_5";
            lab_5.Size = new Size(130, 41);
            lab_5.TabIndex = 10;
            lab_5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = SystemColors.ButtonHighlight;
            label11.Dock = DockStyle.Fill;
            label11.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label11.Location = new Point(4, 161);
            label11.Name = "label11";
            label11.Size = new Size(36, 41);
            label11.TabIndex = 9;
            label11.Text = "5 :";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_4
            // 
            lab_4.AutoSize = true;
            lab_4.BackColor = SystemColors.ButtonHighlight;
            lab_4.Dock = DockStyle.Fill;
            lab_4.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            lab_4.Location = new Point(47, 121);
            lab_4.Name = "lab_4";
            lab_4.Size = new Size(130, 39);
            lab_4.TabIndex = 8;
            lab_4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.ButtonHighlight;
            label9.Dock = DockStyle.Fill;
            label9.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label9.Location = new Point(4, 121);
            label9.Name = "label9";
            label9.Size = new Size(36, 39);
            label9.TabIndex = 7;
            label9.Text = "4 :";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Z
            // 
            lab_Z.AutoSize = true;
            lab_Z.BackColor = SystemColors.ButtonHighlight;
            lab_Z.Dock = DockStyle.Fill;
            lab_Z.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            lab_Z.Location = new Point(47, 81);
            lab_Z.Name = "lab_Z";
            lab_Z.Size = new Size(130, 39);
            lab_Z.TabIndex = 6;
            lab_Z.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.ButtonHighlight;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label7.Location = new Point(4, 81);
            label7.Name = "label7";
            label7.Size = new Size(36, 39);
            label7.TabIndex = 5;
            label7.Text = "Z :";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Y
            // 
            lab_Y.AutoSize = true;
            lab_Y.BackColor = SystemColors.ButtonHighlight;
            lab_Y.Dock = DockStyle.Fill;
            lab_Y.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            lab_Y.Location = new Point(47, 41);
            lab_Y.Name = "lab_Y";
            lab_Y.Size = new Size(130, 39);
            lab_Y.TabIndex = 4;
            lab_Y.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ButtonHighlight;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label4.Location = new Point(4, 41);
            label4.Name = "label4";
            label4.Size = new Size(36, 39);
            label4.TabIndex = 3;
            label4.Text = "Y :";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_X
            // 
            lab_X.AutoSize = true;
            lab_X.BackColor = SystemColors.ButtonHighlight;
            lab_X.Dock = DockStyle.Fill;
            lab_X.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            lab_X.Location = new Point(47, 1);
            lab_X.Name = "lab_X";
            lab_X.Size = new Size(130, 39);
            lab_X.TabIndex = 2;
            lab_X.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonHighlight;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label1.Location = new Point(4, 1);
            label1.Name = "label1";
            label1.Size = new Size(36, 39);
            label1.TabIndex = 1;
            label1.Text = "X :";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btn_disconnect_ethernet
            // 
            btn_disconnect_ethernet.BackColor = SystemColors.ButtonHighlight;
            btn_disconnect_ethernet.FlatStyle = FlatStyle.Flat;
            btn_disconnect_ethernet.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            btn_disconnect_ethernet.Location = new Point(229, 271);
            btn_disconnect_ethernet.Name = "btn_disconnect_ethernet";
            btn_disconnect_ethernet.Size = new Size(110, 43);
            btn_disconnect_ethernet.TabIndex = 6;
            btn_disconnect_ethernet.Text = "斷線";
            btn_disconnect_ethernet.UseVisualStyleBackColor = false;
            btn_disconnect_ethernet.Click += btn_disconnect_ethernet_Click;
            // 
            // btn_connect_ethernet
            // 
            btn_connect_ethernet.BackColor = SystemColors.ButtonHighlight;
            btn_connect_ethernet.FlatStyle = FlatStyle.Flat;
            btn_connect_ethernet.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            btn_connect_ethernet.Location = new Point(103, 271);
            btn_connect_ethernet.Name = "btn_connect_ethernet";
            btn_connect_ethernet.Size = new Size(94, 43);
            btn_connect_ethernet.TabIndex = 5;
            btn_connect_ethernet.Text = "連線";
            btn_connect_ethernet.UseVisualStyleBackColor = false;
            btn_connect_ethernet.Click += btn_connect_ethernet_Click;
            // 
            // txb_port
            // 
            txb_port.BackColor = SystemColors.ButtonHighlight;
            txb_port.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            txb_port.Location = new Point(24, 201);
            txb_port.Name = "txb_port";
            txb_port.Size = new Size(315, 34);
            txb_port.TabIndex = 4;
            txb_port.Text = "683";
            txb_port.TextAlign = HorizontalAlignment.Center;
            // 
            // txb_IP
            // 
            txb_IP.BackColor = SystemColors.ButtonHighlight;
            txb_IP.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            txb_IP.Location = new Point(24, 80);
            txb_IP.Name = "txb_IP";
            txb_IP.Size = new Size(315, 34);
            txb_IP.TabIndex = 3;
            txb_IP.Text = "192.168.60.1";
            txb_IP.TextAlign = HorizontalAlignment.Center;
            // 
            // lab_Enthernetport
            // 
            lab_Enthernetport.AutoSize = true;
            lab_Enthernetport.BackColor = SystemColors.ButtonHighlight;
            lab_Enthernetport.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Enthernetport.Location = new Point(24, 151);
            lab_Enthernetport.Name = "lab_Enthernetport";
            lab_Enthernetport.Size = new Size(117, 26);
            lab_Enthernetport.TabIndex = 2;
            lab_Enthernetport.Text = "連接埠號：";
            // 
            // label_IP
            // 
            label_IP.AutoSize = true;
            label_IP.BackColor = SystemColors.ButtonHighlight;
            label_IP.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            label_IP.Location = new Point(24, 23);
            label_IP.Name = "label_IP";
            label_IP.Size = new Size(159, 26);
            label_IP.TabIndex = 0;
            label_IP.Text = "網際協議地址：";
            // 
            // panel_RS485
            // 
            panel_RS485.BackColor = SystemColors.ButtonHighlight;
            panel_RS485.Controls.Add(lab_Error);
            panel_RS485.Controls.Add(label8);
            panel_RS485.Controls.Add(txb_file_name);
            panel_RS485.Controls.Add(txb_file_address);
            panel_RS485.Controls.Add(label6);
            panel_RS485.Controls.Add(btn_disconnect_RS485);
            panel_RS485.Controls.Add(button_FILE);
            panel_RS485.Location = new Point(84, 208);
            panel_RS485.Name = "panel_RS485";
            panel_RS485.Size = new Size(549, 288);
            panel_RS485.TabIndex = 17;
            panel_RS485.Visible = false;
            // 
            // lab_Error
            // 
            lab_Error.AutoSize = true;
            lab_Error.BackColor = SystemColors.ButtonHighlight;
            lab_Error.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Error.ForeColor = Color.Red;
            lab_Error.Location = new Point(32, 207);
            lab_Error.Name = "lab_Error";
            lab_Error.Size = new Size(0, 20);
            lab_Error.TabIndex = 27;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = SystemColors.ButtonHighlight;
            label8.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            label8.Location = new Point(28, 118);
            label8.Name = "label8";
            label8.Size = new Size(117, 26);
            label8.TabIndex = 26;
            label8.Text = "檔案名稱：";
            // 
            // txb_file_name
            // 
            txb_file_name.BackColor = SystemColors.ButtonHighlight;
            txb_file_name.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            txb_file_name.Location = new Point(28, 158);
            txb_file_name.Name = "txb_file_name";
            txb_file_name.Size = new Size(505, 34);
            txb_file_name.TabIndex = 25;
            txb_file_name.Text = "O9991";
            // 
            // txb_file_address
            // 
            txb_file_address.BackColor = SystemColors.ButtonHighlight;
            txb_file_address.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            txb_file_address.Location = new Point(28, 66);
            txb_file_address.Name = "txb_file_address";
            txb_file_address.Size = new Size(505, 34);
            txb_file_address.TabIndex = 24;
            txb_file_address.Text = "M01:\\PRG\\USER\\";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ButtonHighlight;
            label6.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            label6.Location = new Point(28, 23);
            label6.Name = "label6";
            label6.Size = new Size(159, 26);
            label6.TabIndex = 23;
            label6.Text = "檔案輸入地址：";
            // 
            // btn_disconnect_RS485
            // 
            btn_disconnect_RS485.BackColor = SystemColors.ButtonHighlight;
            btn_disconnect_RS485.FlatStyle = FlatStyle.Flat;
            btn_disconnect_RS485.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            btn_disconnect_RS485.Location = new Point(440, 217);
            btn_disconnect_RS485.Name = "btn_disconnect_RS485";
            btn_disconnect_RS485.Size = new Size(93, 42);
            btn_disconnect_RS485.TabIndex = 6;
            btn_disconnect_RS485.Text = "檔案移除";
            btn_disconnect_RS485.UseVisualStyleBackColor = false;
            btn_disconnect_RS485.Click += btn_disconnect_RS485_Click;
            // 
            // button_FILE
            // 
            button_FILE.BackColor = SystemColors.ButtonHighlight;
            button_FILE.FlatStyle = FlatStyle.Flat;
            button_FILE.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            button_FILE.Location = new Point(316, 217);
            button_FILE.Name = "button_FILE";
            button_FILE.Size = new Size(91, 42);
            button_FILE.TabIndex = 22;
            button_FILE.Text = "檔案寫入";
            button_FILE.UseVisualStyleBackColor = false;
            button_FILE.Click += button_FILE_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonHighlight;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            button1.Location = new Point(8, 121);
            button1.Name = "button1";
            button1.Size = new Size(135, 29);
            button1.TabIndex = 7;
            button1.Text = "數值模擬寫入";
            button1.UseVisualStyleBackColor = false;
            button1.Visible = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ButtonHighlight;
            textBox1.Font = new Font("微軟正黑體", 9F, FontStyle.Bold, GraphicsUnit.Point, 136);
            textBox1.Location = new Point(94, 83);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(37, 23);
            textBox1.TabIndex = 18;
            textBox1.Text = "300";
            textBox1.Visible = false;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.ButtonHighlight;
            textBox2.Font = new Font("微軟正黑體", 9F, FontStyle.Bold, GraphicsUnit.Point, 136);
            textBox2.Location = new Point(94, 37);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(37, 23);
            textBox2.TabIndex = 18;
            textBox2.Text = "100";
            textBox2.Visible = false;
            // 
            // comb_language
            // 
            comb_language.BackColor = SystemColors.ButtonHighlight;
            comb_language.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_language.Font = new Font("微軟正黑體", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            comb_language.FormattingEnabled = true;
            comb_language.Items.AddRange(new object[] { "資料暫存器寫入(D)", "輔助繼電器寫入(M)" });
            comb_language.Location = new Point(3, 8);
            comb_language.Name = "comb_language";
            comb_language.Size = new Size(142, 23);
            comb_language.TabIndex = 20;
            comb_language.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label2.Location = new Point(9, 40);
            label2.Name = "label2";
            label2.Size = new Size(73, 17);
            label2.TabIndex = 7;
            label2.Text = "地址位置：";
            label2.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ButtonHighlight;
            label5.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label5.Location = new Point(40, 88);
            label5.Name = "label5";
            label5.Size = new Size(47, 17);
            label5.TabIndex = 21;
            label5.Text = "數值：";
            label5.Visible = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.Controls.Add(comb_language);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(textBox2);
            panel1.Location = new Point(787, 11);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(163, 160);
            panel1.TabIndex = 22;
            panel1.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52.8985519F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47.1014481F));
            tableLayoutPanel1.Controls.Add(connect_choose, 1, 1);
            tableLayoutPanel1.Controls.Add(lab_Type, 0, 1);
            tableLayoutPanel1.Controls.Add(control_choose, 1, 0);
            tableLayoutPanel1.Controls.Add(lab_MachineType, 0, 0);
            tableLayoutPanel1.Location = new Point(84, 86);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 47.5806465F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.4193535F));
            tableLayoutPanel1.Size = new Size(552, 108);
            tableLayoutPanel1.TabIndex = 28;
            // 
            // connect_choose
            // 
            connect_choose.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            connect_choose.BackColor = SystemColors.ButtonHighlight;
            connect_choose.DropDownStyle = ComboBoxStyle.DropDownList;
            connect_choose.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            connect_choose.FormattingEnabled = true;
            connect_choose.Items.AddRange(new object[] { "Enthernet", "RS 485 / RS422", "RS 232" });
            connect_choose.Location = new Point(295, 62);
            connect_choose.Name = "connect_choose";
            connect_choose.Size = new Size(254, 34);
            connect_choose.TabIndex = 16;
            // 
            // lab_Type
            // 
            lab_Type.AutoSize = true;
            lab_Type.BackColor = SystemColors.ButtonHighlight;
            lab_Type.Dock = DockStyle.Fill;
            lab_Type.Font = new Font("微軟正黑體", 15.75F, FontStyle.Bold);
            lab_Type.Location = new Point(3, 51);
            lab_Type.Name = "lab_Type";
            lab_Type.Size = new Size(286, 57);
            lab_Type.TabIndex = 15;
            lab_Type.Text = "連線方式：";
            lab_Type.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // control_choose
            // 
            control_choose.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            control_choose.BackColor = SystemColors.ButtonHighlight;
            control_choose.DropDownStyle = ComboBoxStyle.DropDownList;
            control_choose.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            control_choose.FormattingEnabled = true;
            control_choose.Items.AddRange(new object[] { "M700-L", "M700-M", "M800-L", "M800-M" });
            control_choose.Location = new Point(295, 8);
            control_choose.Name = "control_choose";
            control_choose.Size = new Size(254, 34);
            control_choose.TabIndex = 14;
            // 
            // lab_MachineType
            // 
            lab_MachineType.AutoSize = true;
            lab_MachineType.BackColor = SystemColors.ButtonHighlight;
            lab_MachineType.Dock = DockStyle.Fill;
            lab_MachineType.Font = new Font("微軟正黑體", 15.75F, FontStyle.Bold);
            lab_MachineType.Location = new Point(3, 0);
            lab_MachineType.Name = "lab_MachineType";
            lab_MachineType.Size = new Size(286, 51);
            lab_MachineType.TabIndex = 1;
            lab_MachineType.Text = "選擇機台：";
            lab_MachineType.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btn_mishubishi
            // 
            btn_mishubishi.BackColor = SystemColors.ButtonHighlight;
            btn_mishubishi.FlatStyle = FlatStyle.Flat;
            btn_mishubishi.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_mishubishi.Location = new Point(651, 88);
            btn_mishubishi.Name = "btn_mishubishi";
            btn_mishubishi.Size = new Size(108, 52);
            btn_mishubishi.TabIndex = 30;
            btn_mishubishi.Text = "切換監控\r\n三菱PLC";
            btn_mishubishi.UseVisualStyleBackColor = false;
            btn_mishubishi.Click += btn_mishubishi_Click;
            // 
            // Connect_CNC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1221, 664);
            Controls.Add(btn_mishubishi);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel_Ethernet);
            Controls.Add(panel_RS485);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Connect_CNC";
            Text = "s";
            panel_Ethernet.ResumeLayout(false);
            panel_Ethernet.PerformLayout();
            panel2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel_RS485.ResumeLayout(false);
            panel_RS485.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panel_Ethernet;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.TextBox txb_port;
        private System.Windows.Forms.TextBox txb_IP;
        private System.Windows.Forms.Label lab_Enthernetport;
        private System.Windows.Forms.Button btn_disconnect_ethernet;
        private System.Windows.Forms.Button btn_connect_ethernet;
        private System.Windows.Forms.Panel panel_RS485;
        private System.Windows.Forms.Button btn_disconnect_RS485;
        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private ComboBox comb_language;
        private Label label2;
        private Label label5;
        private Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox connect_choose;
        private Label lab_Type;
        private ComboBox control_choose;
        private Label lab_MachineType;
        private Button btn_mishubishi;
        private Button button_FILE;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lab_5;
        private Label label11;
        private Label lab_4;
        private Label label9;
        private Label lab_Z;
        private Label label7;
        private Label lab_Y;
        private Label label4;
        private Label lab_X;
        private Label label1;
        private Label label3;
        private Label label8;
        private TextBox txb_file_name;
        private TextBox txb_file_address;
        private Label label6;
        private Label lab_Error;
    }
}