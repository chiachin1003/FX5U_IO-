using System.Windows.Forms;

namespace FX5U_IOMonitor
{
    partial class connect_PLC
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
            components = new System.ComponentModel.Container();
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
            btn_disconnect_RS485 = new Button();
            btn_connect_RS485 = new Button();
            txb_BaudRate = new TextBox();
            txb_COM = new TextBox();
            label_BaudRate = new Label();
            label_COM = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            comboBox1 = new ComboBox();
            label2 = new Label();
            label5 = new Label();
            panel1 = new Panel();
            button_FILE = new Button();
            button2 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            panel_Ethernet.SuspendLayout();
            panel_RS485.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("微軟正黑體", 18F, FontStyle.Bold);
            label1.Location = new Point(137, 40);
            label1.Name = "label1";
            label1.Size = new Size(134, 31);
            label1.TabIndex = 0;
            label1.Text = "型號選擇：";
            // 
            // control_choose
            // 
            control_choose.DropDownStyle = ComboBoxStyle.DropDownList;
            control_choose.Font = new Font("微軟正黑體", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            control_choose.FormattingEnabled = true;
            control_choose.Items.AddRange(new object[] { "R16ENCPU(鑽床)", "FX5U_(鋸床)", "M800W" });
            control_choose.Location = new Point(265, 37);
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
            connect_choose.Items.AddRange(new object[] { "乙太網路", "RS 485", "RS 422" });
            connect_choose.Location = new Point(265, 101);
            connect_choose.Name = "connect_choose";
            connect_choose.Size = new Size(364, 39);
            connect_choose.TabIndex = 15;
            connect_choose.SelectedIndexChanged += connect_choose_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("微軟正黑體", 18F, FontStyle.Bold);
            label4.Location = new Point(137, 104);
            label4.Name = "label4";
            label4.Size = new Size(134, 31);
            label4.TabIndex = 14;
            label4.Text = "連線機制：";
            // 
            // panel_Ethernet
            // 
            panel_Ethernet.Controls.Add(btn_disconnect_ethernet);
            panel_Ethernet.Controls.Add(btn_connect_ethernet);
            panel_Ethernet.Controls.Add(txb_port);
            panel_Ethernet.Controls.Add(txb_IP);
            panel_Ethernet.Controls.Add(label3);
            panel_Ethernet.Controls.Add(label_IP);
            panel_Ethernet.Location = new Point(126, 169);
            panel_Ethernet.Name = "panel_Ethernet";
            panel_Ethernet.Size = new Size(342, 387);
            panel_Ethernet.TabIndex = 16;
            panel_Ethernet.Visible = false;
            // 
            // btn_disconnect_ethernet
            // 
            btn_disconnect_ethernet.FlatStyle = FlatStyle.Flat;
            btn_disconnect_ethernet.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            btn_disconnect_ethernet.Location = new Point(173, 235);
            btn_disconnect_ethernet.Name = "btn_disconnect_ethernet";
            btn_disconnect_ethernet.Size = new Size(142, 43);
            btn_disconnect_ethernet.TabIndex = 6;
            btn_disconnect_ethernet.Text = "斷線";
            btn_disconnect_ethernet.UseVisualStyleBackColor = true;
            btn_disconnect_ethernet.Click += btn_disconnect_ethernet_Click;
            // 
            // btn_connect_ethernet
            // 
            btn_connect_ethernet.FlatStyle = FlatStyle.Flat;
            btn_connect_ethernet.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            btn_connect_ethernet.Location = new Point(24, 235);
            btn_connect_ethernet.Name = "btn_connect_ethernet";
            btn_connect_ethernet.Size = new Size(143, 43);
            btn_connect_ethernet.TabIndex = 5;
            btn_connect_ethernet.Text = "連線";
            btn_connect_ethernet.UseVisualStyleBackColor = true;
            btn_connect_ethernet.Click += btn_connect_ethernet_Click;
            // 
            // txb_port
            // 
            txb_port.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            txb_port.Location = new Point(24, 169);
            txb_port.Name = "txb_port";
            txb_port.Size = new Size(291, 38);
            txb_port.TabIndex = 4;
            txb_port.Text = "2000";
            // 
            // txb_IP
            // 
            txb_IP.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            txb_IP.Location = new Point(24, 73);
            txb_IP.Name = "txb_IP";
            txb_IP.Size = new Size(291, 38);
            txb_IP.TabIndex = 3;
            txb_IP.Text = "192.168.9.1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label3.Location = new Point(24, 136);
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
            panel_RS485.Controls.Add(btn_disconnect_RS485);
            panel_RS485.Controls.Add(btn_connect_RS485);
            panel_RS485.Controls.Add(txb_BaudRate);
            panel_RS485.Controls.Add(txb_COM);
            panel_RS485.Controls.Add(label_BaudRate);
            panel_RS485.Controls.Add(label_COM);
            panel_RS485.Location = new Point(502, 169);
            panel_RS485.Name = "panel_RS485";
            panel_RS485.Size = new Size(335, 387);
            panel_RS485.TabIndex = 17;
            panel_RS485.Visible = false;
            // 
            // btn_disconnect_RS485
            // 
            btn_disconnect_RS485.FlatStyle = FlatStyle.Flat;
            btn_disconnect_RS485.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            btn_disconnect_RS485.Location = new Point(181, 235);
            btn_disconnect_RS485.Name = "btn_disconnect_RS485";
            btn_disconnect_RS485.Size = new Size(129, 42);
            btn_disconnect_RS485.TabIndex = 6;
            btn_disconnect_RS485.Text = "斷線";
            btn_disconnect_RS485.UseVisualStyleBackColor = true;
            // 
            // btn_connect_RS485
            // 
            btn_connect_RS485.FlatStyle = FlatStyle.Flat;
            btn_connect_RS485.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            btn_connect_RS485.Location = new Point(18, 235);
            btn_connect_RS485.Name = "btn_connect_RS485";
            btn_connect_RS485.Size = new Size(143, 42);
            btn_connect_RS485.TabIndex = 5;
            btn_connect_RS485.Text = "連線";
            btn_connect_RS485.UseVisualStyleBackColor = true;
            btn_connect_RS485.Click += btn_connect_RS485_Click;
            // 
            // txb_BaudRate
            // 
            txb_BaudRate.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            txb_BaudRate.Location = new Point(18, 169);
            txb_BaudRate.Name = "txb_BaudRate";
            txb_BaudRate.Size = new Size(292, 38);
            txb_BaudRate.TabIndex = 4;
            txb_BaudRate.Text = "115200";
            // 
            // txb_COM
            // 
            txb_COM.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            txb_COM.Location = new Point(18, 73);
            txb_COM.Name = "txb_COM";
            txb_COM.Size = new Size(292, 38);
            txb_COM.TabIndex = 3;
            txb_COM.Text = "COM6";
            // 
            // label_BaudRate
            // 
            label_BaudRate.AutoSize = true;
            label_BaudRate.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label_BaudRate.Location = new Point(18, 121);
            label_BaudRate.Name = "label_BaudRate";
            label_BaudRate.Size = new Size(109, 30);
            label_BaudRate.TabIndex = 2;
            label_BaudRate.Text = "波特率：";
            // 
            // label_COM
            // 
            label_COM.AutoSize = true;
            label_COM.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label_COM.Location = new Point(18, 23);
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
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("微軟正黑體", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "資料暫存器寫入(D)", "輔助繼電器寫入(M)" });
            comboBox1.Location = new Point(3, 8);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(142, 23);
            comboBox1.TabIndex = 20;
            comboBox1.Visible = false;
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
            panel1.Controls.Add(comboBox1);
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
            // connect_PLC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 664);
            Controls.Add(button2);
            Controls.Add(panel_RS485);
            Controls.Add(panel_Ethernet);
            Controls.Add(button_FILE);
            Controls.Add(connect_choose);
            Controls.Add(label4);
            Controls.Add(panel1);
            Controls.Add(control_choose);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "connect_PLC";
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
        private System.Windows.Forms.TextBox txb_BaudRate;
        private System.Windows.Forms.TextBox txb_COM;
        private System.Windows.Forms.Label label_BaudRate;
        private System.Windows.Forms.Label label_COM;
        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private ComboBox comboBox1;
        private Label label2;
        private Label label5;
        private Panel panel1;
        private Button button_FILE;
        private Button button2;
        private System.Windows.Forms.Timer timer1;
    }
}