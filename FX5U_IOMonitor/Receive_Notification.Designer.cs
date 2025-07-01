namespace FX5U_IOMonitor
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
            check_email = new CheckBox();
            check_line = new CheckBox();
            SuspendLayout();
            // 
            // check_email
            // 
            check_email.AutoSize = true;
            check_email.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            check_email.Location = new Point(12, 25);
            check_email.Name = "check_email";
            check_email.Size = new Size(79, 28);
            check_email.TabIndex = 5;
            check_email.Text = "Email";
            check_email.UseVisualStyleBackColor = true;
            // 
            // check_line
            // 
            check_line.AutoSize = true;
            check_line.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            check_line.Location = new Point(12, 70);
            check_line.Name = "check_line";
            check_line.Size = new Size(181, 28);
            check_line.TabIndex = 6;
            check_line.Text = "Line Notification";
            check_line.UseVisualStyleBackColor = true;
            // 
            // Receive_Notification
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(254, 137);
            Controls.Add(check_line);
            Controls.Add(check_email);
            Name = "Receive_Notification";
            StartPosition = FormStartPosition.CenterParent;
            Text = "接收通知設定";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox check_email;
        private CheckBox check_line;
    }
}