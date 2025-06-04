namespace FX5U_IOMonitor
{
	partial class UserManageForm
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
            _dgvUsers = new DataGridView();
            _lblAccount = new Label();
            _lblPassword = new Label();
            _lblConfirmPassword = new Label();
            _txtAccount = new TextBox();
            _txtPassword = new TextBox();
            _txtConfirmPassword = new TextBox();
            _lblRole = new Label();
            _cbRole = new ComboBox();
            _btnAdd = new Button();
            _btnDelete = new Button();
            _cbSelectedRole = new ComboBox();
            _lblSelectedRole = new Label();
            label1 = new Label();
            _txtEmail = new TextBox();
            ((System.ComponentModel.ISupportInitialize)_dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // _dgvUsers
            // 
            _dgvUsers.AllowUserToAddRows = false;
            _dgvUsers.AllowUserToDeleteRows = false;
            _dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvUsers.Location = new Point(12, 51);
            _dgvUsers.Name = "_dgvUsers";
            _dgvUsers.ReadOnly = true;
            _dgvUsers.Size = new Size(278, 312);
            _dgvUsers.TabIndex = 0;
            // 
            // _lblAccount
            // 
            _lblAccount.AutoSize = true;
            _lblAccount.Location = new Point(313, 21);
            _lblAccount.Name = "_lblAccount";
            _lblAccount.Size = new Size(34, 15);
            _lblAccount.TabIndex = 1;
            _lblAccount.Text = "帳號:";
            // 
            // _lblPassword
            // 
            _lblPassword.AutoSize = true;
            _lblPassword.Location = new Point(313, 51);
            _lblPassword.Name = "_lblPassword";
            _lblPassword.Size = new Size(34, 15);
            _lblPassword.TabIndex = 2;
            _lblPassword.Text = "密碼:";
            // 
            // _lblConfirmPassword
            // 
            _lblConfirmPassword.AutoSize = true;
            _lblConfirmPassword.Location = new Point(313, 83);
            _lblConfirmPassword.Name = "_lblConfirmPassword";
            _lblConfirmPassword.Size = new Size(58, 15);
            _lblConfirmPassword.TabIndex = 3;
            _lblConfirmPassword.Text = "確認密碼:";
            // 
            // _txtAccount
            // 
            _txtAccount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtAccount.Location = new Point(422, 19);
            _txtAccount.Name = "_txtAccount";
            _txtAccount.Size = new Size(139, 23);
            _txtAccount.TabIndex = 4;
            // 
            // _txtPassword
            // 
            _txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtPassword.Location = new Point(422, 49);
            _txtPassword.Name = "_txtPassword";
            _txtPassword.Size = new Size(139, 23);
            _txtPassword.TabIndex = 5;
            _txtPassword.UseSystemPasswordChar = true;
            // 
            // _txtConfirmPassword
            // 
            _txtConfirmPassword.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtConfirmPassword.Location = new Point(422, 81);
            _txtConfirmPassword.Name = "_txtConfirmPassword";
            _txtConfirmPassword.Size = new Size(139, 23);
            _txtConfirmPassword.TabIndex = 6;
            _txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // _lblRole
            // 
            _lblRole.AutoSize = true;
            _lblRole.Location = new Point(313, 120);
            _lblRole.Name = "_lblRole";
            _lblRole.Size = new Size(34, 15);
            _lblRole.TabIndex = 7;
            _lblRole.Text = "權限:";
            // 
            // _cbRole
            // 
            _cbRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbRole.FormattingEnabled = true;
            _cbRole.Location = new Point(422, 118);
            _cbRole.Name = "_cbRole";
            _cbRole.Size = new Size(139, 23);
            _cbRole.TabIndex = 8;
            // 
            // _btnAdd
            // 
            _btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnAdd.Location = new Point(486, 220);
            _btnAdd.Name = "_btnAdd";
            _btnAdd.Size = new Size(75, 33);
            _btnAdd.TabIndex = 9;
            _btnAdd.Text = "新增";
            _btnAdd.UseVisualStyleBackColor = true;
            _btnAdd.Click += _btnAdd_Click;
            // 
            // _btnDelete
            // 
            _btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnDelete.Location = new Point(486, 259);
            _btnDelete.Name = "_btnDelete";
            _btnDelete.Size = new Size(75, 33);
            _btnDelete.TabIndex = 10;
            _btnDelete.Text = "刪除";
            _btnDelete.UseVisualStyleBackColor = true;
            _btnDelete.Click += _btnDelete_Click;
            // 
            // _cbSelectedRole
            // 
            _cbSelectedRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cbSelectedRole.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbSelectedRole.FormattingEnabled = true;
            _cbSelectedRole.Location = new Point(97, 19);
            _cbSelectedRole.Name = "_cbSelectedRole";
            _cbSelectedRole.Size = new Size(119, 23);
            _cbSelectedRole.TabIndex = 12;
            _cbSelectedRole.SelectedIndexChanged += _cbSelectedRole_SelectedIndexChanged;
            // 
            // _lblSelectedRole
            // 
            _lblSelectedRole.AutoSize = true;
            _lblSelectedRole.Location = new Point(12, 22);
            _lblSelectedRole.Name = "_lblSelectedRole";
            _lblSelectedRole.Size = new Size(34, 15);
            _lblSelectedRole.TabIndex = 11;
            _lblSelectedRole.Text = "權限:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(313, 164);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 13;
            label1.Text = "使用者信箱:";
            // 
            // _txtEmail
            // 
            _txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtEmail.Location = new Point(422, 161);
            _txtEmail.Name = "_txtEmail";
            _txtEmail.Size = new Size(139, 23);
            _txtEmail.TabIndex = 14;
            // 
            // UserManageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(585, 381);
            Controls.Add(_txtEmail);
            Controls.Add(label1);
            Controls.Add(_cbSelectedRole);
            Controls.Add(_lblSelectedRole);
            Controls.Add(_btnDelete);
            Controls.Add(_btnAdd);
            Controls.Add(_cbRole);
            Controls.Add(_lblRole);
            Controls.Add(_txtConfirmPassword);
            Controls.Add(_txtPassword);
            Controls.Add(_txtAccount);
            Controls.Add(_lblConfirmPassword);
            Controls.Add(_lblPassword);
            Controls.Add(_lblAccount);
            Controls.Add(_dgvUsers);
            Name = "UserManageForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "使用者管理";
            ((System.ComponentModel.ISupportInitialize)_dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView _dgvUsers;
		private Label _lblAccount;
		private Label _lblPassword;
		private Label _lblConfirmPassword;
		private TextBox _txtAccount;
		private TextBox _txtPassword;
		private TextBox _txtConfirmPassword;
		private Label _lblRole;
		private ComboBox _cbRole;
		private Button _btnAdd;
		private Button _btnDelete;
		private ComboBox _cbSelectedRole;
		private Label _lblSelectedRole;
        private Label label1;
        private TextBox _txtEmail;
    }
}