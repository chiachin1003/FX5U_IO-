namespace FX5U_IOMonitor
{
	partial class UserLoginForm
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
            _lblAccount = new Label();
            _lblPassword = new Label();
            _txtAccount = new TextBox();
            _txtPassword = new TextBox();
            _btnLogin = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // _lblAccount
            // 
            _lblAccount.AutoSize = true;
            _lblAccount.BackColor = SystemColors.ButtonHighlight;
            _lblAccount.Location = new Point(15, 21);
            _lblAccount.Name = "_lblAccount";
            _lblAccount.Size = new Size(34, 15);
            _lblAccount.TabIndex = 0;
            _lblAccount.Text = "帳號:";
            // 
            // _lblPassword
            // 
            _lblPassword.AutoSize = true;
            _lblPassword.BackColor = SystemColors.ButtonHighlight;
            _lblPassword.Location = new Point(15, 55);
            _lblPassword.Name = "_lblPassword";
            _lblPassword.Size = new Size(34, 15);
            _lblPassword.TabIndex = 1;
            _lblPassword.Text = "密碼:";
            // 
            // _txtAccount
            // 
            _txtAccount.BackColor = SystemColors.ButtonHighlight;
            _txtAccount.BorderStyle = BorderStyle.FixedSingle;
            _txtAccount.Location = new Point(96, 18);
            _txtAccount.Name = "_txtAccount";
            _txtAccount.Size = new Size(129, 23);
            _txtAccount.TabIndex = 2;
            // 
            // _txtPassword
            // 
            _txtPassword.BackColor = SystemColors.ButtonHighlight;
            _txtPassword.BorderStyle = BorderStyle.FixedSingle;
            _txtPassword.Location = new Point(96, 53);
            _txtPassword.Name = "_txtPassword";
            _txtPassword.Size = new Size(129, 23);
            _txtPassword.TabIndex = 3;
            _txtPassword.UseSystemPasswordChar = true;
            // 
            // _btnLogin
            // 
            _btnLogin.BackColor = SystemColors.ButtonHighlight;
            _btnLogin.Location = new Point(150, 91);
            _btnLogin.Name = "_btnLogin";
            _btnLogin.Size = new Size(75, 31);
            _btnLogin.TabIndex = 4;
            _btnLogin.Text = "登入";
            _btnLogin.UseVisualStyleBackColor = false;
            _btnLogin.Click += _btnLogin_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(12, 91);
            button1.Name = "button1";
            button1.Size = new Size(37, 31);
            button1.TabIndex = 5;
            button1.Text = "1";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // UserLoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(254, 137);
            Controls.Add(button1);
            Controls.Add(_btnLogin);
            Controls.Add(_txtPassword);
            Controls.Add(_txtAccount);
            Controls.Add(_lblPassword);
            Controls.Add(_lblAccount);
            Name = "UserLoginForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "使用者登入";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label _lblAccount;
		private Label _lblPassword;
		private TextBox _txtAccount;
		private TextBox _txtPassword;
		private Button _btnLogin;
        private Button button1;
    }
}