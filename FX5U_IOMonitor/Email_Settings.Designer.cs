namespace FX5U_IOMonitor.Resources
{
    partial class Email_Settings
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
            tableLayoutPanel1 = new TableLayoutPanel();
            lab_Port = new Label();
            comboBox1 = new ComboBox();
            lab_Protocal = new Label();
            txb_senderPassword = new TextBox();
            lab_Password = new Label();
            txb_senderEmail = new TextBox();
            lab_Email = new Label();
            comb_port = new ComboBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_Update
            // 
            btn_Update.BackColor = SystemColors.ButtonHighlight;
            btn_Update.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_Update.Location = new Point(395, 183);
            btn_Update.Name = "btn_Update";
            btn_Update.Size = new Size(82, 36);
            btn_Update.TabIndex = 39;
            btn_Update.Text = "更新";
            btn_Update.UseVisualStyleBackColor = false;
            btn_Update.Click += btn_setting_Click;
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
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(465, 156);
            tableLayoutPanel1.TabIndex = 42;
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
            // Email_Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(500, 242);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btn_Update);
            Name = "Email_Settings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "郵件設定";
            Load += Email_Settings_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button btn_Update;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lab_Email;
        private Label lab_Port;
        private ComboBox comboBox1;
        private Label lab_Protocal;
        private TextBox txb_senderPassword;
        private Label lab_Password;
        private TextBox txb_senderEmail;
        private ComboBox comb_port;
    }
}