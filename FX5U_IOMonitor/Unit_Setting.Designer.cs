namespace FX5U_IOMonitor
{
	partial class Unit_Setting
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
            panel_unit = new Panel();
            btn_unit = new Button();
            comb_unit = new ComboBox();
            panel_unit.SuspendLayout();
            SuspendLayout();
            // 
            // panel_unit
            // 
            panel_unit.BackColor = SystemColors.ButtonHighlight;
            panel_unit.Controls.Add(btn_unit);
            panel_unit.Controls.Add(comb_unit);
            panel_unit.Dock = DockStyle.Fill;
            panel_unit.Location = new Point(0, 0);
            panel_unit.Name = "panel_unit";
            panel_unit.Size = new Size(216, 148);
            panel_unit.TabIndex = 17;
            // 
            // btn_unit
            // 
            btn_unit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btn_unit.BackColor = SystemColors.ButtonHighlight;
            btn_unit.Location = new Point(89, 96);
            btn_unit.Name = "btn_unit";
            btn_unit.Size = new Size(97, 28);
            btn_unit.TabIndex = 17;
            btn_unit.Text = "單位切換";
            btn_unit.UseVisualStyleBackColor = false;
            btn_unit.Click += btn_unit_Click;
            // 
            // comb_unit
            // 
            comb_unit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comb_unit.BackColor = SystemColors.ButtonHighlight;
            comb_unit.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_unit.Font = new Font("Microsoft JhengHei UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            comb_unit.FormattingEnabled = true;
            comb_unit.Location = new Point(23, 28);
            comb_unit.Name = "comb_unit";
            comb_unit.Size = new Size(163, 43);
            comb_unit.TabIndex = 18;
            // 
            // Unit_Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(216, 148);
            Controls.Add(panel_unit);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Unit_Setting";
            StartPosition = FormStartPosition.CenterParent;
            Text = "單位設定";
            panel_unit.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_unit;
        private Button btn_unit;
        private ComboBox comb_unit;
    }
}