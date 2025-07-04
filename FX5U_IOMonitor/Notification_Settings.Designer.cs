namespace FX5U_IOMonitor.Resources
{
    partial class Notification_Settings
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
            btn_Update = new Button();
            lab_Email = new Label();
            txb_senderEmail = new TextBox();
            lab_Password = new Label();
            txb_senderPassword = new TextBox();
            lab_Protocal = new Label();
            comboBox1 = new ComboBox();
            lab_Port = new Label();
            comb_port = new ComboBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            btn_update_line = new Button();
            label5 = new Label();
            btn_QRcode = new Button();
            pictureBox1 = new PictureBox();
            txb_channelAccessToken = new TextBox();
            lab_line = new Label();
            tabPage3 = new TabPage();
            tableLayoutPanel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btn_Update
            // 
            btn_Update.BackColor = SystemColors.ButtonHighlight;
            btn_Update.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_Update.Location = new Point(401, 195);
            btn_Update.Name = "btn_Update";
            btn_Update.Size = new Size(82, 36);
            btn_Update.TabIndex = 39;
            btn_Update.Text = "更新";
            btn_Update.UseVisualStyleBackColor = false;
            btn_Update.Click += btn_setting_Click;
            // 
            // lab_Email
            // 
            lab_Email.AutoSize = true;
            lab_Email.BackColor = SystemColors.ButtonHighlight;
            lab_Email.Dock = DockStyle.Fill;
            lab_Email.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            lab_Email.Location = new Point(3, 0);
            lab_Email.Name = "lab_Email";
            lab_Email.Size = new Size(146, 39);
            lab_Email.TabIndex = 31;
            lab_Email.Text = "寄件者信箱：";
            lab_Email.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txb_senderEmail
            // 
            txb_senderEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txb_senderEmail.BackColor = SystemColors.ButtonHighlight;
            txb_senderEmail.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            txb_senderEmail.Location = new Point(155, 6);
            txb_senderEmail.Name = "txb_senderEmail";
            txb_senderEmail.Size = new Size(307, 27);
            txb_senderEmail.TabIndex = 32;
            // 
            // lab_Password
            // 
            lab_Password.AutoSize = true;
            lab_Password.BackColor = SystemColors.ButtonHighlight;
            lab_Password.Dock = DockStyle.Fill;
            lab_Password.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            lab_Password.Location = new Point(3, 39);
            lab_Password.Name = "lab_Password";
            lab_Password.Size = new Size(146, 39);
            lab_Password.TabIndex = 33;
            lab_Password.Text = "應用程式密碼：";
            lab_Password.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txb_senderPassword
            // 
            txb_senderPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txb_senderPassword.BackColor = SystemColors.ButtonHighlight;
            txb_senderPassword.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            txb_senderPassword.Location = new Point(155, 45);
            txb_senderPassword.Name = "txb_senderPassword";
            txb_senderPassword.Size = new Size(307, 27);
            txb_senderPassword.TabIndex = 34;
            txb_senderPassword.Text = "jvvswkymcyokcvug";
            // 
            // lab_Protocal
            // 
            lab_Protocal.AutoSize = true;
            lab_Protocal.BackColor = SystemColors.ButtonHighlight;
            lab_Protocal.Dock = DockStyle.Fill;
            lab_Protocal.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            lab_Protocal.Location = new Point(3, 78);
            lab_Protocal.Name = "lab_Protocal";
            lab_Protocal.Size = new Size(146, 39);
            lab_Protocal.TabIndex = 41;
            lab_Protocal.Text = "郵件協議設定：";
            lab_Protocal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.BackColor = SystemColors.ButtonHighlight;
            comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "SMTP" });
            comboBox1.Location = new Point(155, 83);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(307, 28);
            comboBox1.TabIndex = 42;
            // 
            // lab_Port
            // 
            lab_Port.AutoSize = true;
            lab_Port.BackColor = SystemColors.ButtonHighlight;
            lab_Port.Dock = DockStyle.Fill;
            lab_Port.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            lab_Port.Location = new Point(3, 117);
            lab_Port.Name = "lab_Port";
            lab_Port.Size = new Size(146, 39);
            lab_Port.TabIndex = 43;
            lab_Port.Text = "郵件發送通訊埠：";
            lab_Port.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comb_port
            // 
            comb_port.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comb_port.BackColor = SystemColors.ButtonHighlight;
            comb_port.DrawMode = DrawMode.OwnerDrawFixed;
            comb_port.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_port.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            comb_port.FormattingEnabled = true;
            comb_port.Items.AddRange(new object[] { "465", "587" });
            comb_port.Location = new Point(155, 122);
            comb_port.Name = "comb_port";
            comb_port.Size = new Size(307, 28);
            comb_port.TabIndex = 44;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.68817F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67.31183F));
            tableLayoutPanel1.Controls.Add(comb_port, 1, 3);
            tableLayoutPanel1.Controls.Add(lab_Port, 0, 3);
            tableLayoutPanel1.Controls.Add(comboBox1, 1, 2);
            tableLayoutPanel1.Controls.Add(lab_Protocal, 0, 2);
            tableLayoutPanel1.Controls.Add(txb_senderPassword, 1, 1);
            tableLayoutPanel1.Controls.Add(lab_Password, 0, 1);
            tableLayoutPanel1.Controls.Add(txb_senderEmail, 1, 0);
            tableLayoutPanel1.Controls.Add(lab_Email, 0, 0);
            tableLayoutPanel1.Location = new Point(18, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(465, 156);
            tableLayoutPanel1.TabIndex = 42;
            // 
            // tabControl1
            // 
            tabControl1.Alignment = TabAlignment.Bottom;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(516, 318);
            tabControl1.TabIndex = 43;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = SystemColors.ButtonHighlight;
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Controls.Add(btn_Update);
            tabPage1.Location = new Point(4, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(508, 290);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Email";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btn_update_line);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(btn_QRcode);
            tabPage2.Controls.Add(pictureBox1);
            tabPage2.Controls.Add(txb_channelAccessToken);
            tabPage2.Controls.Add(lab_line);
            tabPage2.Location = new Point(4, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(508, 290);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Line";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_update_line
            // 
            btn_update_line.BackColor = SystemColors.ButtonHighlight;
            btn_update_line.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_update_line.Location = new Point(397, 237);
            btn_update_line.Name = "btn_update_line";
            btn_update_line.Size = new Size(82, 36);
            btn_update_line.TabIndex = 40;
            btn_update_line.Text = "更新";
            btn_update_line.UseVisualStyleBackColor = false;
            btn_update_line.Click += btn_update_line_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            label5.Location = new Point(22, 27);
            label5.Name = "label5";
            label5.Size = new Size(234, 19);
            label5.TabIndex = 6;
            label5.Text = "輸入欲發送通知的Line官方帳號：";
            // 
            // btn_QRcode
            // 
            btn_QRcode.Location = new Point(21, 65);
            btn_QRcode.Name = "btn_QRcode";
            btn_QRcode.Size = new Size(75, 28);
            btn_QRcode.TabIndex = 5;
            btn_QRcode.Text = "QR Code";
            btn_QRcode.UseVisualStyleBackColor = true;
            btn_QRcode.Click += btn_QRcode_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(359, 27);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(120, 113);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // txb_channelAccessToken
            // 
            txb_channelAccessToken.Location = new Point(19, 179);
            txb_channelAccessToken.Name = "txb_channelAccessToken";
            txb_channelAccessToken.Size = new Size(460, 23);
            txb_channelAccessToken.TabIndex = 1;
            // 
            // lab_line
            // 
            lab_line.AutoSize = true;
            lab_line.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            lab_line.Location = new Point(21, 149);
            lab_line.Name = "lab_line";
            lab_line.Size = new Size(186, 19);
            lab_line.TabIndex = 0;
            lab_line.Text = "請輸入 Line Notify 權杖：";
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 4);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(508, 290);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Wechat";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // Notification_Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(516, 318);
            Controls.Add(tabControl1);
            Name = "Notification_Settings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "郵件設定";
            Load += Email_Settings_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Update;
        private Label lab_Email;
        private TextBox txb_senderEmail;
        private Label lab_Password;
        private TextBox txb_senderPassword;
        private Label lab_Protocal;
        private ComboBox comboBox1;
        private Label lab_Port;
        private ComboBox comb_port;
        private TableLayoutPanel tableLayoutPanel1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label5;
        private Button btn_QRcode;
        private PictureBox pictureBox1;
        private TextBox txb_channelAccessToken;
        private Label lab_line;
        private TabPage tabPage3;
        private Button btn_update_line;
    }
}