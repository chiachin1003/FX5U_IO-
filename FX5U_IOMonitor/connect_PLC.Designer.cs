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
            label1 = new Label();
            control_choose = new ComboBox();
            connect_choose = new ComboBox();
            label4 = new Label();
            panel_Ethernet = new Panel();
            btn_disconnect_ethernet = new Button();
            btn_connect_ethernet = new Button();
            txb_port = new TextBox();
            txb_IP = new TextBox();
            label3 = new Label();
            label_IP = new Label();
            panel_RS485 = new Panel();
            comb_StopBits = new ComboBox();
            comb_Parity = new ComboBox();
            comb_Bits = new ComboBox();
            comb_Baudrate = new ComboBox();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            btn_disconnect_RS485 = new Button();
            btn_connect_RS485 = new Button();
            txb_comport = new TextBox();
            label_BaudRate = new Label();
            label_COM = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            comb_language = new ComboBox();
            label2 = new Label();
            label5 = new Label();
            panel1 = new Panel();
            button_FILE = new Button();
            button2 = new Button();
            txb_machine = new TextBox();
            label6 = new Label();
            btn_addmachine = new Button();
            panel_Ethernet.SuspendLayout();
            panel_RS485.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("微軟正黑體", 18F, FontStyle.Bold);
            label1.Location = new Point(81, 99);
            label1.Name = "label1";
            label1.Size = new Size(182, 31);
            label1.TabIndex = 0;
            label1.Text = "連線機台選擇：";
            // 
            // control_choose
            // 
            control_choose.DropDownStyle = ComboBoxStyle.DropDownList;
            control_choose.Font = new Font("微軟正黑體", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            control_choose.FormattingEnabled = true;
            control_choose.Items.AddRange(new object[] { "R16ENCPU(鑽床)", "FX5U_(鋸床)", "M800W" });
            control_choose.Location = new Point(269, 96);
            control_choose.Name = "control_choose";
            control_choose.Size = new Size(364, 39);
            control_choose.TabIndex = 13;
            control_choose.SelectedIndexChanged += control_choose_SelectedIndexChanged;
            // 
            // connect_choose
            // 
            connect_choose.DropDownStyle = ComboBoxStyle.DropDownList;
            connect_choose.Font = new Font("微軟正黑體", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            connect_choose.FormattingEnabled = true;
            connect_choose.Items.AddRange(new object[] { "乙太網路", "RS 485 / RS422", "RS 232" });
            connect_choose.Location = new Point(269, 160);
            connect_choose.Name = "connect_choose";
            connect_choose.Size = new Size(364, 39);
            connect_choose.TabIndex = 15;
            connect_choose.SelectedIndexChanged += connect_choose_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("微軟正黑體", 18F, FontStyle.Bold);
            label4.Location = new Point(81, 163);
            label4.Name = "label4";
            label4.Size = new Size(182, 31);
            label4.TabIndex = 14;
            label4.Text = "連線方式設定：";
            // 
            // panel_Ethernet
            // 
            panel_Ethernet.Controls.Add(btn_disconnect_ethernet);
            panel_Ethernet.Controls.Add(btn_connect_ethernet);
            panel_Ethernet.Controls.Add(txb_port);
            panel_Ethernet.Controls.Add(txb_IP);
            panel_Ethernet.Controls.Add(label3);
            panel_Ethernet.Controls.Add(label_IP);
            panel_Ethernet.Location = new Point(81, 228);
            panel_Ethernet.Name = "panel_Ethernet";
            panel_Ethernet.Size = new Size(552, 363);
            panel_Ethernet.TabIndex = 16;
            panel_Ethernet.Visible = false;
            // 
            // btn_disconnect_ethernet
            // 
            btn_disconnect_ethernet.FlatStyle = FlatStyle.Flat;
            btn_disconnect_ethernet.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            btn_disconnect_ethernet.Location = new Point(435, 289);
            btn_disconnect_ethernet.Name = "btn_disconnect_ethernet";
            btn_disconnect_ethernet.Size = new Size(93, 43);
            btn_disconnect_ethernet.TabIndex = 6;
            btn_disconnect_ethernet.Text = "斷線";
            btn_disconnect_ethernet.UseVisualStyleBackColor = true;
            btn_disconnect_ethernet.Click += btn_disconnect_ethernet_Click;
            // 
            // btn_connect_ethernet
            // 
            btn_connect_ethernet.FlatStyle = FlatStyle.Flat;
            btn_connect_ethernet.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            btn_connect_ethernet.Location = new Point(322, 289);
            btn_connect_ethernet.Name = "btn_connect_ethernet";
            btn_connect_ethernet.Size = new Size(94, 43);
            btn_connect_ethernet.TabIndex = 5;
            btn_connect_ethernet.Text = "連線";
            btn_connect_ethernet.UseVisualStyleBackColor = true;
            btn_connect_ethernet.Click += btn_connect_ethernet_Click;
            // 
            // txb_port
            // 
            txb_port.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            txb_port.Location = new Point(24, 201);
            txb_port.Name = "txb_port";
            txb_port.Size = new Size(504, 38);
            txb_port.TabIndex = 4;
            txb_port.Text = "2000";
            txb_port.TextAlign = HorizontalAlignment.Center;
            // 
            // txb_IP
            // 
            txb_IP.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            txb_IP.Location = new Point(24, 80);
            txb_IP.Name = "txb_IP";
            txb_IP.Size = new Size(504, 38);
            txb_IP.TabIndex = 3;
            txb_IP.Text = "192.168.9.1";
            txb_IP.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label3.Location = new Point(24, 151);
            label3.Name = "label3";
            label3.Size = new Size(133, 30);
            label3.TabIndex = 2;
            label3.Text = "連接埠號：";
            // 
            // label_IP
            // 
            label_IP.AutoSize = true;
            label_IP.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label_IP.Location = new Point(24, 23);
            label_IP.Name = "label_IP";
            label_IP.Size = new Size(181, 30);
            label_IP.TabIndex = 0;
            label_IP.Text = "網際協議地址：";
            // 
            // panel_RS485
            // 
            panel_RS485.Controls.Add(comb_StopBits);
            panel_RS485.Controls.Add(comb_Parity);
            panel_RS485.Controls.Add(comb_Bits);
            panel_RS485.Controls.Add(comb_Baudrate);
            panel_RS485.Controls.Add(label9);
            panel_RS485.Controls.Add(label8);
            panel_RS485.Controls.Add(label7);
            panel_RS485.Controls.Add(btn_disconnect_RS485);
            panel_RS485.Controls.Add(btn_connect_RS485);
            panel_RS485.Controls.Add(txb_comport);
            panel_RS485.Controls.Add(label_BaudRate);
            panel_RS485.Controls.Add(label_COM);
            panel_RS485.Location = new Point(84, 228);
            panel_RS485.Name = "panel_RS485";
            panel_RS485.Size = new Size(549, 404);
            panel_RS485.TabIndex = 17;
            panel_RS485.Visible = false;
            // 
            // comb_StopBits
            // 
            comb_StopBits.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            comb_StopBits.FormattingEnabled = true;
            comb_StopBits.Items.AddRange(new object[] { "One", "Two" });
            comb_StopBits.Location = new Point(222, 270);
            comb_StopBits.Name = "comb_StopBits";
            comb_StopBits.Size = new Size(257, 38);
            comb_StopBits.TabIndex = 13;
            // 
            // comb_Parity
            // 
            comb_Parity.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            comb_Parity.FormattingEnabled = true;
            comb_Parity.Items.AddRange(new object[] { "None", "Even", "Odd" });
            comb_Parity.Location = new Point(222, 208);
            comb_Parity.Name = "comb_Parity";
            comb_Parity.Size = new Size(257, 38);
            comb_Parity.TabIndex = 12;
            // 
            // comb_Bits
            // 
            comb_Bits.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            comb_Bits.FormattingEnabled = true;
            comb_Bits.Items.AddRange(new object[] { "7", "8" });
            comb_Bits.Location = new Point(222, 152);
            comb_Bits.Name = "comb_Bits";
            comb_Bits.Size = new Size(257, 38);
            comb_Bits.TabIndex = 11;
            // 
            // comb_Baudrate
            // 
            comb_Baudrate.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            comb_Baudrate.FormattingEnabled = true;
            comb_Baudrate.Items.AddRange(new object[] { "9600", "14400", "19200", "38400", "57600", "115200", "230400", "460800" });
            comb_Baudrate.Location = new Point(222, 93);
            comb_Baudrate.Name = "comb_Baudrate";
            comb_Baudrate.Size = new Size(257, 38);
            comb_Baudrate.TabIndex = 10;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label9.Location = new Point(67, 273);
            label9.Name = "label9";
            label9.Size = new Size(163, 30);
            label9.TabIndex = 9;
            label9.Text = "停止位元(S)：";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label8.Location = new Point(66, 211);
            label8.Name = "label8";
            label8.Size = new Size(164, 30);
            label8.TabIndex = 8;
            label8.Text = "同位檢查(P)：";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label7.Location = new Point(62, 152);
            label7.Name = "label7";
            label7.Size = new Size(168, 30);
            label7.TabIndex = 7;
            label7.Text = "資料位元(D)：";
            // 
            // btn_disconnect_RS485
            // 
            btn_disconnect_RS485.FlatStyle = FlatStyle.Flat;
            btn_disconnect_RS485.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            btn_disconnect_RS485.Location = new Point(435, 332);
            btn_disconnect_RS485.Name = "btn_disconnect_RS485";
            btn_disconnect_RS485.Size = new Size(93, 42);
            btn_disconnect_RS485.TabIndex = 6;
            btn_disconnect_RS485.Text = "斷線";
            btn_disconnect_RS485.UseVisualStyleBackColor = true;
            btn_disconnect_RS485.Click += btn_disconnect_RS485_Click;
            // 
            // btn_connect_RS485
            // 
            btn_connect_RS485.FlatStyle = FlatStyle.Flat;
            btn_connect_RS485.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            btn_connect_RS485.Location = new Point(322, 332);
            btn_connect_RS485.Name = "btn_connect_RS485";
            btn_connect_RS485.Size = new Size(94, 42);
            btn_connect_RS485.TabIndex = 5;
            btn_connect_RS485.Text = "連線";
            btn_connect_RS485.UseVisualStyleBackColor = true;
            btn_connect_RS485.Click += btn_connect_RS485_Click;
            // 
            // txb_comport
            // 
            txb_comport.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            txb_comport.Location = new Point(222, 39);
            txb_comport.Name = "txb_comport";
            txb_comport.Size = new Size(257, 38);
            txb_comport.TabIndex = 3;
            txb_comport.Text = "COM4";
            txb_comport.TextAlign = HorizontalAlignment.Center;
            // 
            // label_BaudRate
            // 
            label_BaudRate.AutoSize = true;
            label_BaudRate.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label_BaudRate.Location = new Point(18, 96);
            label_BaudRate.Name = "label_BaudRate";
            label_BaudRate.Size = new Size(212, 30);
            label_BaudRate.TabIndex = 2;
            label_BaudRate.Text = "每秒傳輸位元(B)：";
            // 
            // label_COM
            // 
            label_COM.AutoSize = true;
            label_COM.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label_COM.Location = new Point(73, 42);
            label_COM.Name = "label_COM";
            label_COM.Size = new Size(157, 30);
            label_COM.TabIndex = 0;
            label_COM.Text = "通訊埠編號：";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            button1.Location = new Point(8, 121);
            button1.Name = "button1";
            button1.Size = new Size(135, 29);
            button1.TabIndex = 7;
            button1.Text = "數值模擬寫入";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            // 
            // textBox1
            // 
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
            button_FILE.FlatStyle = FlatStyle.Flat;
            button_FILE.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            button_FILE.Location = new Point(793, 176);
            button_FILE.Name = "button_FILE";
            button_FILE.Size = new Size(89, 31);
            button_FILE.TabIndex = 22;
            button_FILE.Text = "檔案寫入";
            button_FILE.UseVisualStyleBackColor = true;
            button_FILE.Visible = false;
            button_FILE.Click += button_FILE_Click;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            button2.Location = new Point(889, 176);
            button2.Name = "button2";
            button2.Size = new Size(55, 31);
            button2.TabIndex = 23;
            button2.Text = "刪除";
            button2.UseVisualStyleBackColor = true;
            button2.Visible = false;
            // 
            // txb_machine
            // 
            txb_machine.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            txb_machine.Location = new Point(269, 36);
            txb_machine.Name = "txb_machine";
            txb_machine.Size = new Size(252, 38);
            txb_machine.TabIndex = 24;
            txb_machine.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("微軟正黑體", 18F, FontStyle.Bold);
            label6.Location = new Point(81, 43);
            label6.Name = "label6";
            label6.Size = new Size(182, 31);
            label6.TabIndex = 25;
            label6.Text = "監控機台型號：";
            // 
            // btn_addmachine
            // 
            btn_addmachine.FlatStyle = FlatStyle.Flat;
            btn_addmachine.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            btn_addmachine.Location = new Point(539, 34);
            btn_addmachine.Name = "btn_addmachine";
            btn_addmachine.Size = new Size(94, 43);
            btn_addmachine.TabIndex = 26;
            btn_addmachine.Text = "新增";
            btn_addmachine.UseVisualStyleBackColor = true;
            btn_addmachine.Click += btn_addmachine_Click;
            // 
            // Connect_PLC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 664);
            Controls.Add(btn_addmachine);
            Controls.Add(label6);
            Controls.Add(txb_machine);
            Controls.Add(panel_Ethernet);
            Controls.Add(panel_RS485);
            Controls.Add(button2);
            Controls.Add(button_FILE);
            Controls.Add(connect_choose);
            Controls.Add(label4);
            Controls.Add(panel1);
            Controls.Add(control_choose);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Connect_PLC";
            Text = "s";
            panel_Ethernet.ResumeLayout(false);
            panel_Ethernet.PerformLayout();
            panel_RS485.ResumeLayout(false);
            panel_RS485.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox control_choose;
        private System.Windows.Forms.ComboBox connect_choose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel_Ethernet;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.TextBox txb_port;
        private System.Windows.Forms.TextBox txb_IP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_disconnect_ethernet;
        private System.Windows.Forms.Button btn_connect_ethernet;
        private System.Windows.Forms.Panel panel_RS485;
        private System.Windows.Forms.Button btn_disconnect_RS485;
        private System.Windows.Forms.Button btn_connect_RS485;
        private System.Windows.Forms.TextBox txb_comport;
        private System.Windows.Forms.Label label_BaudRate;
        private System.Windows.Forms.Label label_COM;
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
        private TextBox txb_machine;
        private Label label6;
        private Button btn_addmachine;
        private Label label9;
        private Label label8;
        private Label label7;
        private ComboBox comb_StopBits;
        private ComboBox comb_Parity;
        private ComboBox comb_Bits;
        private ComboBox comb_Baudrate;
    }
}