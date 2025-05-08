namespace MachineIO_CRUD
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			_dgvMachineIOs = new DataGridView();
			_txtMachineName = new TextBox();
			label1 = new Label();
			_btnAdd = new Button();
			_cbSelectMachine = new ComboBox();
			_cbSelectIOType = new ComboBox();
			_cbSelectRelayType = new ComboBox();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			_btnSearch = new Button();
			( (System.ComponentModel.ISupportInitialize)_dgvMachineIOs ).BeginInit();
			SuspendLayout();
			// 
			// _dgvMachineIOs
			// 
			_dgvMachineIOs.AllowUserToAddRows = false;
			_dgvMachineIOs.AllowUserToDeleteRows = false;
			_dgvMachineIOs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			_dgvMachineIOs.Location = new Point( 12, 12 );
			_dgvMachineIOs.Name = "_dgvMachineIOs";
			_dgvMachineIOs.ReadOnly = true;
			_dgvMachineIOs.Size = new Size( 776, 303 );
			_dgvMachineIOs.TabIndex = 0;
			// 
			// _txtMachineName
			// 
			_txtMachineName.Location = new Point( 80, 334 );
			_txtMachineName.Name = "_txtMachineName";
			_txtMachineName.Size = new Size( 100, 23 );
			_txtMachineName.TabIndex = 1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point( 12, 337 );
			label1.Name = "label1";
			label1.Size = new Size( 62, 15 );
			label1.TabIndex = 2;
			label1.Text = "機台名稱:";
			// 
			// _btnAdd
			// 
			_btnAdd.Location = new Point( 198, 327 );
			_btnAdd.Name = "_btnAdd";
			_btnAdd.Size = new Size( 75, 34 );
			_btnAdd.TabIndex = 3;
			_btnAdd.Text = "新增";
			_btnAdd.UseVisualStyleBackColor = true;
			_btnAdd.Click +=  _btnAdd_Click ;
			// 
			// _cbSelectMachine
			// 
			_cbSelectMachine.DropDownStyle = ComboBoxStyle.DropDownList;
			_cbSelectMachine.FormattingEnabled = true;
			_cbSelectMachine.Location = new Point( 462, 331 );
			_cbSelectMachine.Name = "_cbSelectMachine";
			_cbSelectMachine.Size = new Size( 111, 23 );
			_cbSelectMachine.TabIndex = 4;
			// 
			// _cbSelectIOType
			// 
			_cbSelectIOType.DropDownStyle = ComboBoxStyle.DropDownList;
			_cbSelectIOType.FormattingEnabled = true;
			_cbSelectIOType.Location = new Point( 462, 363 );
			_cbSelectIOType.Name = "_cbSelectIOType";
			_cbSelectIOType.Size = new Size( 111, 23 );
			_cbSelectIOType.TabIndex = 5;
			// 
			// _cbSelectRelayType
			// 
			_cbSelectRelayType.DropDownStyle = ComboBoxStyle.DropDownList;
			_cbSelectRelayType.FormattingEnabled = true;
			_cbSelectRelayType.Location = new Point( 462, 401 );
			_cbSelectRelayType.Name = "_cbSelectRelayType";
			_cbSelectRelayType.Size = new Size( 111, 23 );
			_cbSelectRelayType.TabIndex = 6;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point( 379, 334 );
			label2.Name = "label2";
			label2.Size = new Size( 36, 15 );
			label2.TabIndex = 7;
			label2.Text = "機台:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point( 379, 366 );
			label3.Name = "label3";
			label3.Size = new Size( 62, 15 );
			label3.TabIndex = 8;
			label3.Text = "設備種類:";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point( 379, 404 );
			label4.Name = "label4";
			label4.Size = new Size( 75, 15 );
			label4.TabIndex = 9;
			label4.Text = "繼電器種類:";
			// 
			// _btnSearch
			// 
			_btnSearch.Location = new Point( 596, 324 );
			_btnSearch.Name = "_btnSearch";
			_btnSearch.Size = new Size( 75, 34 );
			_btnSearch.TabIndex = 10;
			_btnSearch.Text = "搜尋";
			_btnSearch.UseVisualStyleBackColor = true;
			_btnSearch.Click +=  _btnSearch_Click ;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF( 7F, 15F );
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size( 800, 442 );
			Controls.Add( _btnSearch );
			Controls.Add( label4 );
			Controls.Add( label3 );
			Controls.Add( label2 );
			Controls.Add( _cbSelectRelayType );
			Controls.Add( _cbSelectIOType );
			Controls.Add( _cbSelectMachine );
			Controls.Add( _btnAdd );
			Controls.Add( label1 );
			Controls.Add( _txtMachineName );
			Controls.Add( _dgvMachineIOs );
			Name = "Form1";
			Text = "Form1";
			( (System.ComponentModel.ISupportInitialize)_dgvMachineIOs ).EndInit();
			ResumeLayout( false );
			PerformLayout();
		}

		#endregion

		private DataGridView _dgvMachineIOs;
		private TextBox _txtMachineName;
		private Label label1;
		private Button _btnAdd;
		private ComboBox _cbSelectMachine;
		private ComboBox _cbSelectIOType;
		private ComboBox _cbSelectRelayType;
		private Label label2;
		private Label label3;
		private Label label4;
		private Button _btnSearch;
	}
}
