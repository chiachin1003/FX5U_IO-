using System.Windows.Forms;

namespace FX5U_IOMonitor
{
    partial class Connect_PLC
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
            btn_disconnect_ethernet = new Button();
            btn_connect_ethernet = new Button();
            txb_port = new TextBox();
            txb_IP = new TextBox();
            lab_Enthernetport = new Label();
            label_IP = new Label();
            panel_RS485 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            comb_StopBits = new ComboBox();
            lab_StopBits = new Label();
            comb_Parity = new ComboBox();
            lab_Parity = new Label();
            comb_Bits = new ComboBox();
            lab_Bits = new Label();
            comb_Baudrate = new ComboBox();
            label_BaudRate = new Label();
            txb_comport = new TextBox();
            label_COM = new Label();
            btn_disconnect_RS485 = new Button();
            btn_connect_RS485 = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            comb_language = new ComboBox();
            label2 = new Label();
            label5 = new Label();
            panel1 = new Panel();
            button_FILE = new Button();
            button2 = new Button();
            btn_delete = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            connect_choose = new ComboBox();
            lab_Type = new Label();
            control_choose = new ComboBox();
            lab_MachineType = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            btn_addmachine = new Button();
            txb_machine = new TextBox();
            lab_Add_Machine = new Label();
            btn_mishubishi = new Button();
            panel_Ethernet.SuspendLayout();
            panel_RS485.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel_Ethernet
            // 
            panel_Ethernet.BackColor = SystemColors.ButtonHighlight;
            panel_Ethernet.Controls.Add(btn_disconnect_ethernet);
            panel_Ethernet.Controls.Add(btn_connect_ethernet);
            panel_Ethernet.Controls.Add(txb_port);
            panel_Ethernet.Controls.Add(txb_IP);
            panel_Ethernet.Controls.Add(lab_Enthernetport);
            panel_Ethernet.Controls.Add(label_IP);
            panel_Ethernet.Location = new Point(78, 223);
            panel_Ethernet.Name = "panel_Ethernet";
            panel_Ethernet.Size = new Size(558, 363);
            panel_Ethernet.TabIndex = 16;
            panel_Ethernet.Visible = false;
            // 
            // btn_disconnect_ethernet
            // 
            btn_disconnect_ethernet.BackColor = SystemColors.ButtonHighlight;
            btn_disconnect_ethernet.FlatStyle = FlatStyle.Flat;
            btn_disconnect_ethernet.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            btn_disconnect_ethernet.Location = new Point(418, 289);
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
            btn_connect_ethernet.Location = new Point(299, 289);
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
            txb_port.Size = new Size(504, 34);
            txb_port.TabIndex = 4;
            txb_port.Text = "2000";
            txb_port.TextAlign = HorizontalAlignment.Center;
            // 
            // txb_IP
            // 
            txb_IP.BackColor = SystemColors.ButtonHighlight;
            txb_IP.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            txb_IP.Location = new Point(24, 80);
            txb_IP.Name = "txb_IP";
            txb_IP.Size = new Size(504, 34);
            txb_IP.TabIndex = 3;
            txb_IP.Text = "192.168.9.1";
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
            panel_RS485.Controls.Add(tableLayoutPanel2);
            panel_RS485.Controls.Add(btn_disconnect_RS485);
            panel_RS485.Controls.Add(btn_connect_RS485);
            panel_RS485.Location = new Point(84, 200);
            panel_RS485.Name = "panel_RS485";
            panel_RS485.Size = new Size(552, 404);
            panel_RS485.TabIndex = 17;
            panel_RS485.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.1876144F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.8123856F));
            tableLayoutPanel2.Controls.Add(comb_StopBits, 1, 4);
            tableLayoutPanel2.Controls.Add(lab_StopBits, 0, 4);
            tableLayoutPanel2.Controls.Add(comb_Parity, 1, 3);
            tableLayoutPanel2.Controls.Add(lab_Parity, 0, 3);
            tableLayoutPanel2.Controls.Add(comb_Bits, 1, 2);
            tableLayoutPanel2.Controls.Add(lab_Bits, 0, 2);
            tableLayoutPanel2.Controls.Add(comb_Baudrate, 1, 1);
            tableLayoutPanel2.Controls.Add(label_BaudRate, 0, 1);
            tableLayoutPanel2.Controls.Add(txb_comport, 1, 0);
            tableLayoutPanel2.Controls.Add(label_COM, 0, 0);
            tableLayoutPanel2.Location = new Point(0, 23);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.Size = new Size(549, 296);
            tableLayoutPanel2.TabIndex = 14;
            // 
            // comb_StopBits
            // 
            comb_StopBits.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comb_StopBits.BackColor = SystemColors.ButtonHighlight;
            comb_StopBits.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_StopBits.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            comb_StopBits.FormattingEnabled = true;
            comb_StopBits.Items.AddRange(new object[] { "One", "Two" });
            comb_StopBits.Location = new Point(295, 249);
            comb_StopBits.Name = "comb_StopBits";
            comb_StopBits.Size = new Size(251, 34);
            comb_StopBits.TabIndex = 17;
            // 
            // lab_StopBits
            // 
            lab_StopBits.AutoSize = true;
            lab_StopBits.BackColor = SystemColors.ButtonHighlight;
            lab_StopBits.Dock = DockStyle.Fill;
            lab_StopBits.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_StopBits.Location = new Point(3, 236);
            lab_StopBits.Name = "lab_StopBits";
            lab_StopBits.Size = new Size(286, 60);
            lab_StopBits.TabIndex = 16;
            lab_StopBits.Text = "停止位元(S)：";
            lab_StopBits.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comb_Parity
            // 
            comb_Parity.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comb_Parity.BackColor = SystemColors.ButtonHighlight;
            comb_Parity.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_Parity.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            comb_Parity.FormattingEnabled = true;
            comb_Parity.Items.AddRange(new object[] { "None", "Even", "Odd" });
            comb_Parity.Location = new Point(295, 189);
            comb_Parity.Name = "comb_Parity";
            comb_Parity.Size = new Size(251, 34);
            comb_Parity.TabIndex = 15;
            // 
            // lab_Parity
            // 
            lab_Parity.AutoSize = true;
            lab_Parity.BackColor = SystemColors.ButtonHighlight;
            lab_Parity.Dock = DockStyle.Fill;
            lab_Parity.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Parity.Location = new Point(3, 177);
            lab_Parity.Name = "lab_Parity";
            lab_Parity.Size = new Size(286, 59);
            lab_Parity.TabIndex = 14;
            lab_Parity.Text = "同位檢查(P)：";
            lab_Parity.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comb_Bits
            // 
            comb_Bits.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comb_Bits.BackColor = SystemColors.ButtonHighlight;
            comb_Bits.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_Bits.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            comb_Bits.FormattingEnabled = true;
            comb_Bits.Items.AddRange(new object[] { "7", "8" });
            comb_Bits.Location = new Point(295, 130);
            comb_Bits.Name = "comb_Bits";
            comb_Bits.Size = new Size(251, 34);
            comb_Bits.TabIndex = 13;
            // 
            // lab_Bits
            // 
            lab_Bits.AutoSize = true;
            lab_Bits.BackColor = SystemColors.ButtonHighlight;
            lab_Bits.Dock = DockStyle.Fill;
            lab_Bits.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Bits.Location = new Point(3, 118);
            lab_Bits.Name = "lab_Bits";
            lab_Bits.Size = new Size(286, 59);
            lab_Bits.TabIndex = 12;
            lab_Bits.Text = "資料位元(D)：";
            lab_Bits.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comb_Baudrate
            // 
            comb_Baudrate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comb_Baudrate.BackColor = SystemColors.ButtonHighlight;
            comb_Baudrate.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_Baudrate.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            comb_Baudrate.FormattingEnabled = true;
            comb_Baudrate.Items.AddRange(new object[] { "9600", "14400", "19200", "38400", "57600", "115200", "230400", "460800" });
            comb_Baudrate.Location = new Point(295, 71);
            comb_Baudrate.Name = "comb_Baudrate";
            comb_Baudrate.Size = new Size(251, 34);
            comb_Baudrate.TabIndex = 11;
            // 
            // label_BaudRate
            // 
            label_BaudRate.AutoSize = true;
            label_BaudRate.BackColor = SystemColors.ButtonHighlight;
            label_BaudRate.Dock = DockStyle.Fill;
            label_BaudRate.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            label_BaudRate.Location = new Point(3, 59);
            label_BaudRate.Name = "label_BaudRate";
            label_BaudRate.Size = new Size(286, 59);
            label_BaudRate.TabIndex = 5;
            label_BaudRate.Text = "每秒傳輸位元(B)：";
            label_BaudRate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txb_comport
            // 
            txb_comport.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txb_comport.BackColor = SystemColors.ButtonHighlight;
            txb_comport.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            txb_comport.Location = new Point(295, 12);
            txb_comport.Name = "txb_comport";
            txb_comport.Size = new Size(251, 34);
            txb_comport.TabIndex = 4;
            txb_comport.Text = "COM4";
            txb_comport.TextAlign = HorizontalAlignment.Center;
            // 
            // label_COM
            // 
            label_COM.AutoSize = true;
            label_COM.BackColor = SystemColors.ButtonHighlight;
            label_COM.Dock = DockStyle.Fill;
            label_COM.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            label_COM.Location = new Point(3, 0);
            label_COM.Name = "label_COM";
            label_COM.Size = new Size(286, 59);
            label_COM.TabIndex = 1;
            label_COM.Text = "通訊埠編號：";
            label_COM.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btn_disconnect_RS485
            // 
            btn_disconnect_RS485.BackColor = SystemColors.ButtonHighlight;
            btn_disconnect_RS485.FlatStyle = FlatStyle.Flat;
            btn_disconnect_RS485.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            btn_disconnect_RS485.Location = new Point(442, 332);
            btn_disconnect_RS485.Name = "btn_disconnect_RS485";
            btn_disconnect_RS485.Size = new Size(110, 42);
            btn_disconnect_RS485.TabIndex = 6;
            btn_disconnect_RS485.Text = "斷線";
            btn_disconnect_RS485.UseVisualStyleBackColor = false;
            btn_disconnect_RS485.Click += btn_disconnect_RS485_Click;
            // 
            // btn_connect_RS485
            // 
            btn_connect_RS485.BackColor = SystemColors.ButtonHighlight;
            btn_connect_RS485.FlatStyle = FlatStyle.Flat;
            btn_connect_RS485.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            btn_connect_RS485.Location = new Point(295, 332);
            btn_connect_RS485.Name = "btn_connect_RS485";
            btn_connect_RS485.Size = new Size(94, 42);
            btn_connect_RS485.TabIndex = 5;
            btn_connect_RS485.Text = "連線";
            btn_connect_RS485.UseVisualStyleBackColor = false;
            btn_connect_RS485.Click += btn_connect_RS485_Click;
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
            // button_FILE
            // 
            button_FILE.BackColor = SystemColors.ButtonHighlight;
            button_FILE.FlatStyle = FlatStyle.Flat;
            button_FILE.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            button_FILE.Location = new Point(793, 176);
            button_FILE.Name = "button_FILE";
            button_FILE.Size = new Size(89, 31);
            button_FILE.TabIndex = 22;
            button_FILE.Text = "檔案寫入";
            button_FILE.UseVisualStyleBackColor = false;
            button_FILE.Visible = false;
            button_FILE.Click += button_FILE_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ButtonHighlight;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            button2.Location = new Point(889, 176);
            button2.Name = "button2";
            button2.Size = new Size(55, 31);
            button2.TabIndex = 23;
            button2.Text = "刪除";
            button2.UseVisualStyleBackColor = false;
            button2.Visible = false;
            // 
            // btn_delete
            // 
            btn_delete.BackColor = SystemColors.ButtonHighlight;
            btn_delete.FlatStyle = FlatStyle.Flat;
            btn_delete.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_delete.Location = new Point(651, 33);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(117, 43);
            btn_delete.TabIndex = 27;
            btn_delete.Text = "刪除";
            btn_delete.UseVisualStyleBackColor = false;
            btn_delete.Click += btn_delete_Click;
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
            control_choose.Items.AddRange(new object[] { "R16ENCPU(鑽床)", "FX5U_(鋸床)", "M800W" });
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
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.0416679F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.9583321F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 71F));
            tableLayoutPanel3.Controls.Add(btn_addmachine, 2, 0);
            tableLayoutPanel3.Controls.Add(txb_machine, 1, 0);
            tableLayoutPanel3.Controls.Add(lab_Add_Machine, 0, 0);
            tableLayoutPanel3.Location = new Point(84, 33);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 47.5806465F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 52.4193535F));
            tableLayoutPanel3.Size = new Size(552, 47);
            tableLayoutPanel3.TabIndex = 29;
            // 
            // btn_addmachine
            // 
            btn_addmachine.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btn_addmachine.BackColor = SystemColors.ButtonHighlight;
            btn_addmachine.FlatStyle = FlatStyle.Flat;
            btn_addmachine.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            btn_addmachine.Location = new Point(483, 3);
            btn_addmachine.Name = "btn_addmachine";
            btn_addmachine.Size = new Size(66, 41);
            btn_addmachine.TabIndex = 28;
            btn_addmachine.Text = "新增";
            btn_addmachine.UseVisualStyleBackColor = false;
            btn_addmachine.Click += btn_addmachine_Click;
            // 
            // txb_machine
            // 
            txb_machine.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txb_machine.BackColor = SystemColors.ButtonHighlight;
            txb_machine.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            txb_machine.Location = new Point(296, 6);
            txb_machine.Name = "txb_machine";
            txb_machine.Size = new Size(181, 34);
            txb_machine.TabIndex = 27;
            txb_machine.TextAlign = HorizontalAlignment.Center;
            // 
            // lab_Add_Machine
            // 
            lab_Add_Machine.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lab_Add_Machine.AutoSize = true;
            lab_Add_Machine.BackColor = SystemColors.ButtonHighlight;
            lab_Add_Machine.Font = new Font("微軟正黑體", 15.75F, FontStyle.Bold);
            lab_Add_Machine.Location = new Point(3, 10);
            lab_Add_Machine.Name = "lab_Add_Machine";
            lab_Add_Machine.Size = new Size(287, 26);
            lab_Add_Machine.TabIndex = 26;
            lab_Add_Machine.Text = "新增機台：";
            // 
            // btn_mishubishi
            // 
            btn_mishubishi.BackColor = SystemColors.ButtonHighlight;
            btn_mishubishi.FlatStyle = FlatStyle.Flat;
            btn_mishubishi.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_mishubishi.Location = new Point(651, 88);
            btn_mishubishi.Name = "btn_mishubishi";
            btn_mishubishi.Size = new Size(117, 52);
            btn_mishubishi.TabIndex = 30;
            btn_mishubishi.Text = "切換監控\r\n三菱控制器";
            btn_mishubishi.UseVisualStyleBackColor = false;
            btn_mishubishi.Click += btn_mishubishi_Click;
            // 
            // Connect_PLC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1221, 664);
            Controls.Add(btn_mishubishi);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btn_delete);
            Controls.Add(panel_Ethernet);
            Controls.Add(panel_RS485);
            Controls.Add(button2);
            Controls.Add(button_FILE);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Connect_PLC";
            Text = "s";
            panel_Ethernet.ResumeLayout(false);
            panel_Ethernet.PerformLayout();
            panel_RS485.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
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
        private System.Windows.Forms.Button btn_connect_RS485;
        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private ComboBox comb_language;
        private Label label2;
        private Label label5;
        private Panel panel1;
        private Button button_FILE;
        private Button button2;
        private System.Windows.Forms.Timer timer1;
        private Button btn_delete;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox connect_choose;
        private Label lab_Type;
        private ComboBox control_choose;
        private Label lab_MachineType;
        private TableLayoutPanel tableLayoutPanel2;
        private ComboBox comb_StopBits;
        private Label lab_StopBits;
        private ComboBox comb_Parity;
        private Label lab_Parity;
        private ComboBox comb_Bits;
        private Label lab_Bits;
        private ComboBox comb_Baudrate;
        private Label label_BaudRate;
        private TextBox txb_comport;
        private Label label_COM;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btn_addmachine;
        private TextBox txb_machine;
        private Label lab_Add_Machine;
        private Button btn_mishubishi;
    }
}