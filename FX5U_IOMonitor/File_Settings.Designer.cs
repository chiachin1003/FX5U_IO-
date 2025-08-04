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
            lab_TableSelect = new Label();
            btn_download = new Button();
            comb_datatable = new ComboBox();
            btn_update = new Button();
            lab_save_location = new Label();
            comb_select = new ComboBox();
            btn_cloud = new Button();
            lab_cloudstatus = new Label();
            SuspendLayout();
            // 
            // lab_TableSelect
            // 
            lab_TableSelect.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_TableSelect.Location = new Point(20, 19);
            lab_TableSelect.Name = "lab_TableSelect";
            lab_TableSelect.Size = new Size(138, 26);
            lab_TableSelect.TabIndex = 30;
            lab_TableSelect.Text = "資料表選擇：";
            // 
            // btn_download
            // 
            btn_download.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_download.Location = new Point(20, 112);
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
            btn_update.Location = new Point(121, 112);
            btn_update.Name = "btn_update";
            btn_update.Size = new Size(82, 36);
            btn_update.TabIndex = 42;
            btn_update.Text = "更新";
            btn_update.UseVisualStyleBackColor = true;
            btn_update.Click += btn_update_Click;
            // 
            // lab_save_location
            // 
            lab_save_location.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_save_location.Location = new Point(20, 63);
            lab_save_location.Name = "lab_save_location";
            lab_save_location.Size = new Size(138, 26);
            lab_save_location.TabIndex = 43;
            lab_save_location.Text = "儲存位置：";
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
            // btn_cloud
            // 
            btn_cloud.Location = new Point(307, 117);
            btn_cloud.Name = "btn_cloud";
            btn_cloud.Size = new Size(123, 29);
            btn_cloud.TabIndex = 45;
            btn_cloud.Text = "載入雲端資料";
            btn_cloud.UseVisualStyleBackColor = true;
            btn_cloud.Click += btn_cloud_Click;
            // 
            // lab_cloudstatus
            // 
            lab_cloudstatus.AutoSize = true;
            lab_cloudstatus.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_cloudstatus.Location = new Point(24, 163);
            lab_cloudstatus.Name = "lab_cloudstatus";
            lab_cloudstatus.Size = new Size(0, 20);
            lab_cloudstatus.TabIndex = 46;
            // 
            // File_Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(443, 197);
            Controls.Add(lab_cloudstatus);
            Controls.Add(btn_cloud);
            Controls.Add(comb_select);
            Controls.Add(lab_save_location);
            Controls.Add(btn_update);
            Controls.Add(comb_datatable);
            Controls.Add(btn_download);
            Controls.Add(lab_TableSelect);
            Name = "File_Settings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "檔案更新";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lab_TableSelect;
        private Button btn_download;
        private ComboBox comb_datatable;
        private Button btn_update;
        private Label lab_save_location;
        private ComboBox comb_select;
        private Button btn_cloud;
        private Label lab_cloudstatus;
    }
}