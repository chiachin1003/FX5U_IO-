using System.Windows.Forms;

namespace FX5U_IOMonitor
{
    partial class Setting
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
            npgsqlCommandBuilder1 = new Npgsql.NpgsqlCommandBuilder();
            btn_file_download = new Button();
            btn_Mail_Manager = new Button();
            btn_usersetting = new Button();
            btn_Alrm_Notify = new Button();
            SuspendLayout();
            // 
            // npgsqlCommandBuilder1
            // 
            npgsqlCommandBuilder1.QuotePrefix = "\"";
            npgsqlCommandBuilder1.QuoteSuffix = "\"";
            // 
            // btn_file_download
            // 
            btn_file_download.Location = new Point(73, 118);
            btn_file_download.Name = "btn_file_download";
            btn_file_download.Size = new Size(124, 42);
            btn_file_download.TabIndex = 8;
            btn_file_download.Text = "檔案下載及更新";
            btn_file_download.UseVisualStyleBackColor = true;
            btn_file_download.Click += btn_file_download_Click;
            // 
            // btn_Mail_Manager
            // 
            btn_Mail_Manager.Location = new Point(234, 118);
            btn_Mail_Manager.Name = "btn_Mail_Manager";
            btn_Mail_Manager.Size = new Size(124, 42);
            btn_Mail_Manager.TabIndex = 8;
            btn_Mail_Manager.Text = "發送郵件管理人";
            btn_Mail_Manager.UseVisualStyleBackColor = true;
            btn_Mail_Manager.Click += btn_Mail_Manager_Click;
            // 
            // btn_usersetting
            // 
            btn_usersetting.Location = new Point(542, 118);
            btn_usersetting.Name = "btn_usersetting";
            btn_usersetting.Size = new Size(124, 42);
            btn_usersetting.TabIndex = 9;
            btn_usersetting.Text = "權限管理";
            btn_usersetting.UseVisualStyleBackColor = true;
            btn_usersetting.Click += btn_usersetting_Click;
            // 
            // btn_Alrm_Notify
            // 
            btn_Alrm_Notify.Location = new Point(386, 118);
            btn_Alrm_Notify.Name = "btn_Alrm_Notify";
            btn_Alrm_Notify.Size = new Size(124, 42);
            btn_Alrm_Notify.TabIndex = 10;
            btn_Alrm_Notify.Text = "警告通知設定";
            btn_Alrm_Notify.UseVisualStyleBackColor = true;
            btn_Alrm_Notify.Click += btn_Alrm_Notify_Click;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(936, 664);
            Controls.Add(btn_Alrm_Notify);
            Controls.Add(btn_usersetting);
            Controls.Add(btn_Mail_Manager);
            Controls.Add(btn_file_download);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "Setting";
            ResumeLayout(false);
        }

        #endregion

        private Npgsql.NpgsqlCommandBuilder npgsqlCommandBuilder1;
        private Button btn_file_download;
        private Button btn_Mail_Manager;
        private Button btn_usersetting;
        private Button btn_Alrm_Notify;
    }
}