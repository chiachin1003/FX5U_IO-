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
            lab_Possible = new Label();
            txb_senderEmail = new TextBox();
            txb_senderPassword = new TextBox();
            label7 = new Label();
            label6 = new Label();
            txb_TLS_port = new TextBox();
            btn_setting = new Button();
            label1 = new Label();
            comboBox1 = new ComboBox();
            SuspendLayout();
            // 
            // lab_Possible
            // 
            lab_Possible.AutoSize = true;
            lab_Possible.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Possible.Location = new Point(12, 59);
            lab_Possible.Name = "lab_Possible";
            lab_Possible.Size = new Size(159, 26);
            lab_Possible.TabIndex = 10;
            lab_Possible.Text = "應用程式密碼：";
            // 
            // txb_senderEmail
            // 
            txb_senderEmail.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            txb_senderEmail.Location = new Point(164, 18);
            txb_senderEmail.Name = "txb_senderEmail";
            txb_senderEmail.Size = new Size(266, 27);
            txb_senderEmail.TabIndex = 19;
            txb_senderEmail.Text = "webform@mechalogix.com";
            // 
            // txb_senderPassword
            // 
            txb_senderPassword.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            txb_senderPassword.Location = new Point(164, 58);
            txb_senderPassword.Name = "txb_senderPassword";
            txb_senderPassword.Size = new Size(266, 27);
            txb_senderPassword.TabIndex = 24;
            txb_senderPassword.Text = "jvvswkymcyokcvug";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label7.Location = new Point(12, 19);
            label7.Name = "label7";
            label7.Size = new Size(138, 26);
            label7.TabIndex = 30;
            label7.Text = "寄件者信箱：";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label6.Location = new Point(95, 142);
            label6.Name = "label6";
            label6.Size = new Size(76, 26);
            label6.TabIndex = 28;
            label6.Text = "Port：";
            // 
            // txb_TLS_port
            // 
            txb_TLS_port.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            txb_TLS_port.Location = new Point(164, 142);
            txb_TLS_port.Name = "txb_TLS_port";
            txb_TLS_port.Size = new Size(266, 27);
            txb_TLS_port.TabIndex = 38;
            txb_TLS_port.Text = "465";
            txb_TLS_port.TextAlign = HorizontalAlignment.Center;
            // 
            // btn_setting
            // 
            btn_setting.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_setting.Location = new Point(349, 186);
            btn_setting.Name = "btn_setting";
            btn_setting.Size = new Size(82, 36);
            btn_setting.TabIndex = 39;
            btn_setting.Text = "設定";
            btn_setting.UseVisualStyleBackColor = true;
            btn_setting.Click += btn_setting_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(12, 100);
            label1.Name = "label1";
            label1.Size = new Size(159, 26);
            label1.TabIndex = 40;
            label1.Text = "郵件協議設定：";
            // 
            // comboBox1
            // 
            comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "SMTP" });
            comboBox1.Location = new Point(164, 100);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(267, 28);
            comboBox1.TabIndex = 41;
            // 
            // Email_Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(443, 244);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(btn_setting);
            Controls.Add(txb_TLS_port);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(txb_senderPassword);
            Controls.Add(txb_senderEmail);
            Controls.Add(lab_Possible);
            Name = "Email_Settings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "郵件設定";
            Load += Email_Settings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lab_Possible;
        private TextBox txb_senderEmail;
        private TextBox txb_senderPassword;
        private Label label7;
        private Label label6;
        private TextBox txb_TLS_port;
        private Button btn_setting;
        private Label label1;
        private ComboBox comboBox1;
    }
}