﻿namespace FX5U_IOMonitor
{
	partial class Receive_Notification
    {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            check_Email = new CheckBox();
            check_Line = new CheckBox();
            btn_Confirm = new Button();
            lab_CurrentUser = new Label();
            SuspendLayout();
            // 
            // check_Email
            // 
            check_Email.AutoSize = true;
            check_Email.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            check_Email.Location = new Point(12, 54);
            check_Email.Name = "check_Email";
            check_Email.Size = new Size(131, 28);
            check_Email.TabIndex = 5;
            check_Email.Text = "Scheduling";
            check_Email.UseVisualStyleBackColor = true;
            // 
            // check_Line
            // 
            check_Line.AutoSize = true;
            check_Line.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            check_Line.Location = new Point(12, 107);
            check_Line.Name = "check_Line";
            check_Line.Size = new Size(181, 28);
            check_Line.TabIndex = 6;
            check_Line.Text = "Line Notification";
            check_Line.UseVisualStyleBackColor = true;
            // 
            // btn_Confirm
            // 
            btn_Confirm.Location = new Point(290, 106);
            btn_Confirm.Name = "btn_Confirm";
            btn_Confirm.Size = new Size(75, 29);
            btn_Confirm.TabIndex = 7;
            btn_Confirm.Text = "設定";
            btn_Confirm.UseVisualStyleBackColor = true;
            btn_Confirm.Click += btn_Confirm_Click;
            // 
            // lab_CurrentUser
            // 
            lab_CurrentUser.AutoSize = true;
            lab_CurrentUser.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_CurrentUser.Location = new Point(10, 11);
            lab_CurrentUser.Name = "lab_CurrentUser";
            lab_CurrentUser.Size = new Size(0, 19);
            lab_CurrentUser.TabIndex = 8;
            // 
            // Receive_Notification
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(388, 170);
            Controls.Add(lab_CurrentUser);
            Controls.Add(btn_Confirm);
            Controls.Add(check_Line);
            Controls.Add(check_Email);
            Name = "Receive_Notification";
            StartPosition = FormStartPosition.CenterParent;
            Text = "接收通知設定";
            Load += Receive_Notification_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox check_Email;
        private CheckBox check_Line;
        private Button btn_Confirm;
        private Label lab_CurrentUser;
    }
}