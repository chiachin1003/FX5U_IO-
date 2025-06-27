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
            btn_history = new Button();
            btn_emailtest = new Button();
            btn_checkpoint = new Button();
            button1 = new Button();
            button2 = new Button();
            btn_notify = new Button();
            SuspendLayout();
            // 
            // npgsqlCommandBuilder1
            // 
            npgsqlCommandBuilder1.QuotePrefix = "\"";
            npgsqlCommandBuilder1.QuoteSuffix = "\"";
            // 
            // btn_file_download
            // 
            btn_file_download.Location = new Point(73, 75);
            btn_file_download.Name = "btn_file_download";
            btn_file_download.Size = new Size(124, 42);
            btn_file_download.TabIndex = 8;
            btn_file_download.Text = "檔案下載及更新";
            btn_file_download.UseVisualStyleBackColor = true;
            btn_file_download.Click += btn_file_download_Click;
            // 
            // btn_Mail_Manager
            // 
            btn_Mail_Manager.Location = new Point(228, 75);
            btn_Mail_Manager.Name = "btn_Mail_Manager";
            btn_Mail_Manager.Size = new Size(124, 42);
            btn_Mail_Manager.TabIndex = 8;
            btn_Mail_Manager.Text = "發送郵件管理人";
            btn_Mail_Manager.UseVisualStyleBackColor = true;
            btn_Mail_Manager.Click += btn_Mail_Manager_Click;
            // 
            // btn_usersetting
            // 
            btn_usersetting.Location = new Point(73, 203);
            btn_usersetting.Name = "btn_usersetting";
            btn_usersetting.Size = new Size(124, 42);
            btn_usersetting.TabIndex = 9;
            btn_usersetting.Text = "權限管理";
            btn_usersetting.UseVisualStyleBackColor = true;
            btn_usersetting.Click += btn_usersetting_Click;
            // 
            // btn_Alrm_Notify
            // 
            btn_Alrm_Notify.Location = new Point(228, 139);
            btn_Alrm_Notify.Name = "btn_Alrm_Notify";
            btn_Alrm_Notify.Size = new Size(124, 42);
            btn_Alrm_Notify.TabIndex = 10;
            btn_Alrm_Notify.Text = "警告通知設定";
            btn_Alrm_Notify.UseVisualStyleBackColor = true;
            btn_Alrm_Notify.Click += btn_Alrm_Notify_Click;
            // 
            // btn_history
            // 
            btn_history.Location = new Point(73, 139);
            btn_history.Name = "btn_history";
            btn_history.Size = new Size(124, 42);
            btn_history.TabIndex = 11;
            btn_history.Text = "歷史紀錄查詢";
            btn_history.UseVisualStyleBackColor = true;
            btn_history.Click += btn_history_Click;
            // 
            // btn_emailtest
            // 
            btn_emailtest.Location = new Point(768, 512);
            btn_emailtest.Name = "btn_emailtest";
            btn_emailtest.Size = new Size(124, 33);
            btn_emailtest.TabIndex = 12;
            btn_emailtest.Text = "郵件發送測試鈕";
            btn_emailtest.UseVisualStyleBackColor = true;
            btn_emailtest.Click += btn_emailtest_Click;
            // 
            // btn_checkpoint
            // 
            btn_checkpoint.Location = new Point(12, 613);
            btn_checkpoint.Name = "btn_checkpoint";
            btn_checkpoint.Size = new Size(91, 39);
            btn_checkpoint.TabIndex = 13;
            btn_checkpoint.Text = "介面更新速度";
            btn_checkpoint.UseVisualStyleBackColor = true;
            btn_checkpoint.Click += btn_checkpoint_Click;
            // 
            // button1
            // 
            button1.Location = new Point(768, 584);
            button1.Name = "button1";
            button1.Size = new Size(124, 33);
            button1.TabIndex = 14;
            button1.Text = "每分定期發送通知";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(768, 473);
            button2.Name = "button2";
            button2.Size = new Size(124, 33);
            button2.TabIndex = 15;
            button2.Text = "每分";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // btn_notify
            // 
            btn_notify.Location = new Point(228, 206);
            btn_notify.Name = "btn_notify";
            btn_notify.Size = new Size(124, 39);
            btn_notify.TabIndex = 16;
            btn_notify.Text = "通訊方式選擇";
            btn_notify.UseVisualStyleBackColor = true;
            btn_notify.Click += btn_notify_Click;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(936, 664);
            Controls.Add(btn_notify);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btn_checkpoint);
            Controls.Add(btn_emailtest);
            Controls.Add(btn_history);
            Controls.Add(btn_Alrm_Notify);
            Controls.Add(btn_usersetting);
            Controls.Add(btn_Mail_Manager);
            Controls.Add(btn_file_download);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "Setting";
            Load += Setting_Load;
            ResumeLayout(false);
        }

        #endregion

        private Npgsql.NpgsqlCommandBuilder npgsqlCommandBuilder1;
        private Button btn_file_download;
        private Button btn_Mail_Manager;
        private Button btn_usersetting;
        private Button btn_Alrm_Notify;
        private Button btn_history;
        private Button btn_emailtest;
        private Button btn_checkpoint;
        private Button button1;
        private Button button2;
        private Button btn_notify;
    }
}