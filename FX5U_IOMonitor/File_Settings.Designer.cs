namespace FX5U_IOMonitor.Resources
{
    partial class File_Settings
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
            label7 = new Label();
            btn_download = new Button();
            comb_datatable = new ComboBox();
            btn_update = new Button();
            label1 = new Label();
            comb_select = new ComboBox();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label7.Location = new Point(20, 19);
            label7.Name = "label7";
            label7.Size = new Size(138, 26);
            label7.TabIndex = 30;
            label7.Text = "資料表選擇：";
            // 
            // btn_download
            // 
            btn_download.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_download.Location = new Point(257, 104);
            btn_download.Name = "btn_download";
            btn_download.Size = new Size(86, 36);
            btn_download.TabIndex = 39;
            btn_download.Text = "下載";
            btn_download.UseVisualStyleBackColor = true;
            btn_download.Click += btn_setting_Click;
            // 
            // comb_datatable
            // 
            comb_datatable.DrawMode = DrawMode.OwnerDrawFixed;
            comb_datatable.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_datatable.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            comb_datatable.FormattingEnabled = true;
            comb_datatable.Location = new Point(164, 19);
            comb_datatable.Name = "comb_datatable";
            comb_datatable.Size = new Size(266, 28);
            comb_datatable.TabIndex = 41;
            // 
            // btn_update
            // 
            btn_update.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_update.Location = new Point(349, 104);
            btn_update.Name = "btn_update";
            btn_update.Size = new Size(82, 36);
            btn_update.TabIndex = 42;
            btn_update.Text = "更新";
            btn_update.UseVisualStyleBackColor = true;
            btn_update.Click += btn_update_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(41, 61);
            label1.Name = "label1";
            label1.Size = new Size(117, 26);
            label1.TabIndex = 43;
            label1.Text = "儲存位置：";
            // 
            // comb_select
            // 
            comb_select.DrawMode = DrawMode.OwnerDrawFixed;
            comb_select.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_select.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold);
            comb_select.FormattingEnabled = true;
            comb_select.Location = new Point(164, 61);
            comb_select.Name = "comb_select";
            comb_select.Size = new Size(266, 28);
            comb_select.TabIndex = 44;
            // 
            // File_Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(443, 170);
            Controls.Add(comb_select);
            Controls.Add(label1);
            Controls.Add(btn_update);
            Controls.Add(comb_datatable);
            Controls.Add(btn_download);
            Controls.Add(label7);
            Name = "File_Settings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "檔案更新";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label7;
        private Button btn_download;
        private ComboBox comb_datatable;
        private Button btn_update;
        private Label label1;
        private ComboBox comb_select;
    }
}