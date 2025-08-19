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
            lab_emailsetting = new Label();
            _txtEmail = new TextBox();
            lab_line = new Label();
            _txt_Line = new TextBox();
            pictureBox_Official_Account = new PictureBox();
            lab_hint = new Label();
            ((System.ComponentModel.ISupportInitialize)_dgvUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Official_Account).BeginInit();
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
            _dgvUsers.Size = new Size(577, 90);
            _dgvUsers.TabIndex = 0;
            _dgvUsers.CellDoubleClick += _dgvUsers_CellDoubleClick;
            // 
            // _lblAccount
            // 
            _lblAccount.AutoSize = true;
            _lblAccount.BackColor = SystemColors.ButtonHighlight;
            _lblAccount.Location = new Point(15, 177);
            _lblAccount.Name = "_lblAccount";
            _lblAccount.Size = new Size(34, 15);
            _lblAccount.TabIndex = 1;
            _lblAccount.Text = "帳號:";
            // 
            // _lblPassword
            // 
            _lblPassword.AutoSize = true;
            _lblPassword.BackColor = SystemColors.ButtonHighlight;
            _lblPassword.Location = new Point(15, 207);
            _lblPassword.Name = "_lblPassword";
            _lblPassword.Size = new Size(34, 15);
            _lblPassword.TabIndex = 2;
            _lblPassword.Text = "密碼:";
            // 
            // _lblConfirmPassword
            // 
            _lblConfirmPassword.AutoSize = true;
            _lblConfirmPassword.BackColor = SystemColors.ButtonHighlight;
            _lblConfirmPassword.Location = new Point(15, 239);
            _lblConfirmPassword.Name = "_lblConfirmPassword";
            _lblConfirmPassword.Size = new Size(58, 15);
            _lblConfirmPassword.TabIndex = 3;
            _lblConfirmPassword.Text = "確認密碼:";
            // 
            // _txtAccount
            // 
            _txtAccount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtAccount.BackColor = SystemColors.ButtonHighlight;
            _txtAccount.BorderStyle = BorderStyle.FixedSingle;
            _txtAccount.CausesValidation = false;
            _txtAccount.Location = new Point(149, 169);
            _txtAccount.Name = "_txtAccount";
            _txtAccount.Size = new Size(139, 23);
            _txtAccount.TabIndex = 4;
            // 
            // _txtPassword
            // 
            _txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtPassword.BackColor = SystemColors.ButtonHighlight;
            _txtPassword.BorderStyle = BorderStyle.FixedSingle;
            _txtPassword.CausesValidation = false;
            _txtPassword.Location = new Point(149, 204);
            _txtPassword.Name = "_txtPassword";
            _txtPassword.Size = new Size(139, 23);
            _txtPassword.TabIndex = 5;
            _txtPassword.UseSystemPasswordChar = true;
            // 
            // _txtConfirmPassword
            // 
            _txtConfirmPassword.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtConfirmPassword.BackColor = SystemColors.ButtonHighlight;
            _txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            _txtConfirmPassword.Location = new Point(149, 236);
            _txtConfirmPassword.Name = "_txtConfirmPassword";
            _txtConfirmPassword.Size = new Size(139, 23);
            _txtConfirmPassword.TabIndex = 6;
            _txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // _lblRole
            // 
            _lblRole.AutoSize = true;
            _lblRole.BackColor = SystemColors.ButtonHighlight;
            _lblRole.Location = new Point(15, 276);
            _lblRole.Name = "_lblRole";
            _lblRole.Size = new Size(34, 15);
            _lblRole.TabIndex = 7;
            _lblRole.Text = "權限:";
            // 
            // _cbRole
            // 
            _cbRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cbRole.BackColor = SystemColors.ButtonHighlight;
            _cbRole.CausesValidation = false;
            _cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbRole.FormattingEnabled = true;
            _cbRole.Location = new Point(149, 273);
            _cbRole.Name = "_cbRole";
            _cbRole.Size = new Size(139, 23);
            _cbRole.TabIndex = 8;
            // 
            // _btnAdd
            // 
            _btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnAdd.BackColor = SystemColors.ButtonHighlight;
            _btnAdd.Location = new Point(149, 350);
            _btnAdd.Name = "_btnAdd";
            _btnAdd.Size = new Size(62, 33);
            _btnAdd.TabIndex = 9;
            _btnAdd.Text = "新增";
            _btnAdd.UseVisualStyleBackColor = false;
            _btnAdd.Click += _btnAdd_Click;
            // 
            // _btnDelete
            // 
            _btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnDelete.BackColor = SystemColors.ButtonHighlight;
            _btnDelete.Location = new Point(224, 350);
            _btnDelete.Name = "_btnDelete";
            _btnDelete.Size = new Size(64, 33);
            _btnDelete.TabIndex = 10;
            _btnDelete.Text = "刪除";
            _btnDelete.UseVisualStyleBackColor = false;
            _btnDelete.Click += _btnDelete_Click;
            // 
            // _cbSelectedRole
            // 
            _cbSelectedRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _cbSelectedRole.BackColor = SystemColors.ButtonHighlight;
            _cbSelectedRole.DropDownStyle = ComboBoxStyle.DropDownList;
            _cbSelectedRole.FormattingEnabled = true;
            _cbSelectedRole.Location = new Point(451, 17);
            _cbSelectedRole.Name = "_cbSelectedRole";
            _cbSelectedRole.Size = new Size(139, 23);
            _cbSelectedRole.TabIndex = 12;
            _cbSelectedRole.SelectedIndexChanged += _cbSelectedRole_SelectedIndexChanged;
            // 
            // _lblSelectedRole
            // 
            _lblSelectedRole.AutoSize = true;
            _lblSelectedRole.BackColor = SystemColors.ButtonHighlight;
            _lblSelectedRole.Location = new Point(376, 20);
            _lblSelectedRole.Name = "_lblSelectedRole";
            _lblSelectedRole.Size = new Size(34, 15);
            _lblSelectedRole.TabIndex = 11;
            _lblSelectedRole.Text = "權限:";
            // 
            // lab_emailsetting
            // 
            lab_emailsetting.AutoSize = true;
            lab_emailsetting.BackColor = SystemColors.ButtonHighlight;
            lab_emailsetting.Location = new Point(15, 320);
            lab_emailsetting.Name = "lab_emailsetting";
            lab_emailsetting.Size = new Size(34, 15);
            lab_emailsetting.TabIndex = 13;
            lab_emailsetting.Text = "信箱:";
            // 
            // _txtEmail
            // 
            _txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtEmail.BackColor = SystemColors.ButtonHighlight;
            _txtEmail.BorderStyle = BorderStyle.FixedSingle;
            _txtEmail.Location = new Point(149, 316);
            _txtEmail.Name = "_txtEmail";
            _txtEmail.Size = new Size(139, 23);
            _txtEmail.TabIndex = 14;
            // 
            // lab_line
            // 
            lab_line.AutoSize = true;
            lab_line.BackColor = SystemColors.ButtonHighlight;
            lab_line.Location = new Point(296, 171);
            lab_line.Name = "lab_line";
            lab_line.Size = new Size(114, 15);
            lab_line.TabIndex = 15;
            lab_line.Text = "LINE Access Token:";
            // 
            // _txt_Line
            // 
            _txt_Line.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txt_Line.BackColor = SystemColors.ButtonHighlight;
            _txt_Line.BorderStyle = BorderStyle.FixedSingle;
            _txt_Line.Location = new Point(451, 169);
            _txt_Line.Name = "_txt_Line";
            _txt_Line.Size = new Size(139, 23);
            _txt_Line.TabIndex = 16;
            // 
            // pictureBox_Official_Account
            // 
            pictureBox_Official_Account.Location = new Point(451, 263);
            pictureBox_Official_Account.Name = "pictureBox_Official_Account";
            pictureBox_Official_Account.Size = new Size(132, 120);
            pictureBox_Official_Account.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_Official_Account.TabIndex = 17;
            pictureBox_Official_Account.TabStop = false;
            // 
            // lab_hint
            // 
            lab_hint.AutoSize = true;
            lab_hint.Location = new Point(296, 207);
            lab_hint.MaximumSize = new Size(294, 0);
            lab_hint.Name = "lab_hint";
            lab_hint.Size = new Size(294, 30);
            lab_hint.TabIndex = 0;
            lab_hint.Text = "ℹ️ 請掃描下方 QR Code 加入 Line Notify 官方帳號，\r\n加入後可取得您的發送權杖（Token）用於訊息通知。";
            // 
            // UserManageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(602, 416);
            Controls.Add(pictureBox_Official_Account);
            Controls.Add(lab_hint);
            Controls.Add(_txt_Line);
            Controls.Add(lab_line);
            Controls.Add(_txtEmail);
            Controls.Add(lab_emailsetting);
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserManageForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "使用者管理";
            ((System.ComponentModel.ISupportInitialize)_dgvUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Official_Account).EndInit();
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
        private Label lab_emailsetting;
        private TextBox _txtEmail;
        private Label lab_line;
        private TextBox _txt_Line;
        private PictureBox pictureBox_Official_Account;
        private Label lab_hint;
    }
}