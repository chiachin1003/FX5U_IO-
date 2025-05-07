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
            label1 = new Label();
            lab_Possible = new Label();
            lab_Error = new Label();
            comb_class = new ComboBox();
            txb_senderEmail = new TextBox();
            txb_senderPassword = new TextBox();
            btn_add = new Button();
            label6 = new Label();
            txb_comment = new TextBox();
            label7 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(12, 153);
            label1.Name = "label1";
            label1.Size = new Size(138, 26);
            label1.TabIndex = 11;
            label1.Text = "預設收件人：";
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
            // lab_Error
            // 
            lab_Error.AutoSize = true;
            lab_Error.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Error.Location = new Point(12, 195);
            lab_Error.Name = "lab_Error";
            lab_Error.Size = new Size(138, 26);
            lab_Error.TabIndex = 9;
            lab_Error.Text = "新增收件人：";
            // 
            // comb_class
            // 
            comb_class.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_class.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            comb_class.FormattingEnabled = true;
            comb_class.Location = new Point(164, 147);
            comb_class.Name = "comb_class";
            comb_class.Size = new Size(266, 32);
            comb_class.TabIndex = 18;
            // 
            // txb_senderEmail
            // 
            txb_senderEmail.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txb_senderEmail.Location = new Point(164, 18);
            txb_senderEmail.Name = "txb_senderEmail";
            txb_senderEmail.Size = new Size(266, 32);
            txb_senderEmail.TabIndex = 19;
            txb_senderEmail.Text = "itrid400itri@gmail.com";
            // 
            // txb_senderPassword
            // 
            txb_senderPassword.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txb_senderPassword.Location = new Point(164, 58);
            txb_senderPassword.Name = "txb_senderPassword";
            txb_senderPassword.Size = new Size(266, 32);
            txb_senderPassword.TabIndex = 24;
            txb_senderPassword.Text = "wmfisqognlhnhcsc";
            txb_senderPassword.KeyPress += txb_address_KeyPress;
            // 
            // btn_add
            // 
            btn_add.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_add.Location = new Point(164, 237);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(128, 36);
            btn_add.TabIndex = 25;
            btn_add.Text = "選擇收件人";
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Click += btn_add_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label6.Location = new Point(12, 99);
            label6.Name = "label6";
            label6.Size = new Size(159, 26);
            label6.TabIndex = 28;
            label6.Text = "郵件發送頻率：";
            // 
            // txb_comment
            // 
            txb_comment.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txb_comment.Location = new Point(164, 189);
            txb_comment.Name = "txb_comment";
            txb_comment.Size = new Size(266, 32);
            txb_comment.TabIndex = 29;
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
            // button1
            // 
            button1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            button1.Location = new Point(302, 237);
            button1.Name = "button1";
            button1.Size = new Size(128, 36);
            button1.TabIndex = 33;
            button1.Text = "新增收件人";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            button2.Location = new Point(164, 446);
            button2.Name = "button2";
            button2.Size = new Size(128, 36);
            button2.TabIndex = 34;
            button2.Text = "移除選中";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            button3.Location = new Point(302, 446);
            button3.Name = "button3";
            button3.Size = new Size(128, 36);
            button3.TabIndex = 35;
            button3.Text = "信件發送";
            button3.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "每天", "每二天", "每週", "每月" });
            comboBox1.Location = new Point(164, 99);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(266, 32);
            comboBox1.TabIndex = 36;
            comboBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 284);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(418, 150);
            dataGridView1.TabIndex = 37;
            // 
            // Email_Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 512);
            Controls.Add(dataGridView1);
            Controls.Add(comboBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label7);
            Controls.Add(txb_comment);
            Controls.Add(label6);
            Controls.Add(btn_add);
            Controls.Add(txb_senderPassword);
            Controls.Add(txb_senderEmail);
            Controls.Add(comb_class);
            Controls.Add(label1);
            Controls.Add(lab_Possible);
            Controls.Add(lab_Error);
            Name = "Email_Settings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "郵件設定";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label lab_Possible;
        private Label lab_Error;
        private ComboBox comb_class;
        private TextBox txb_senderEmail;
        private TextBox txb_senderPassword;
        private Button btn_add;
        private Label label6;
        private TextBox txb_comment;
        private Label label7;
        private Button button1;
        private Button button2;
        private Button button3;
        private ComboBox comboBox1;
        private DataGridView dataGridView1;
    }
}