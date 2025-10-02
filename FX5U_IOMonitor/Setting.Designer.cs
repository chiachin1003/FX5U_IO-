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
            btn_alarm = new Button();
            button3 = new Button();
            btn_unit = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btn_UtilizationRate = new Button();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // npgsqlCommandBuilder1
            // 
            npgsqlCommandBuilder1.QuotePrefix = "\"";
            npgsqlCommandBuilder1.QuoteSuffix = "\"";
            // 
            // btn_file_download
            // 
            btn_file_download.Enabled = false;
            btn_file_download.Location = new Point(13, 157);
            btn_file_download.Name = "btn_file_download";
            btn_file_download.Size = new Size(124, 42);
            btn_file_download.TabIndex = 8;
            btn_file_download.Text = "檔案下載及更新";
            btn_file_download.UseVisualStyleBackColor = true;
            btn_file_download.Click += btn_file_download_Click;
            // 
            // btn_Mail_Manager
            // 
            btn_Mail_Manager.Enabled = false;
            btn_Mail_Manager.Location = new Point(13, 109);
            btn_Mail_Manager.Name = "btn_Mail_Manager";
            btn_Mail_Manager.Size = new Size(124, 42);
            btn_Mail_Manager.TabIndex = 8;
            btn_Mail_Manager.Text = "寄件者設定";
            btn_Mail_Manager.UseVisualStyleBackColor = true;
            btn_Mail_Manager.Click += btn_Mail_Manager_Click;
            // 
            // btn_usersetting
            // 
            btn_usersetting.Enabled = false;
            btn_usersetting.Location = new Point(13, 61);
            btn_usersetting.Name = "btn_usersetting";
            btn_usersetting.Size = new Size(124, 42);
            btn_usersetting.TabIndex = 9;
            btn_usersetting.Text = "使用者與權限管理";
            btn_usersetting.UseVisualStyleBackColor = true;
            btn_usersetting.Click += btn_usersetting_Click;
            // 
            // btn_Alrm_Notify
            // 
            btn_Alrm_Notify.Enabled = false;
            btn_Alrm_Notify.Location = new Point(13, 13);
            btn_Alrm_Notify.Name = "btn_Alrm_Notify";
            btn_Alrm_Notify.Size = new Size(124, 42);
            btn_Alrm_Notify.TabIndex = 10;
            btn_Alrm_Notify.Text = "警告通知設定";
            btn_Alrm_Notify.UseVisualStyleBackColor = true;
            btn_Alrm_Notify.Click += btn_Alrm_Notify_Click;
            // 
            // btn_history
            // 
            btn_history.Location = new Point(13, 295);
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
            btn_emailtest.Visible = false;
            btn_emailtest.Click += btn_emailtest_Click;
            // 
            // btn_checkpoint
            // 
            btn_checkpoint.Location = new Point(13, 343);
            btn_checkpoint.Name = "btn_checkpoint";
            btn_checkpoint.Size = new Size(124, 39);
            btn_checkpoint.TabIndex = 13;
            btn_checkpoint.Text = "介面更新速度";
            btn_checkpoint.UseVisualStyleBackColor = true;
            btn_checkpoint.Click += btn_checkpoint_Click;
            // 
            // button1
            // 
            button1.Location = new Point(768, 394);
            button1.Name = "button1";
            button1.Size = new Size(124, 33);
            button1.TabIndex = 14;
            button1.Text = "雲端下載檔案";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            // 
            // button2
            // 
            button2.Location = new Point(768, 473);
            button2.Name = "button2";
            button2.Size = new Size(124, 33);
            button2.TabIndex = 15;
            button2.Text = "函數測試";
            button2.UseVisualStyleBackColor = true;
            button2.Visible = false;
            // 
            // btn_notify
            // 
            btn_notify.Location = new Point(13, 205);
            btn_notify.Name = "btn_notify";
            btn_notify.Size = new Size(124, 39);
            btn_notify.TabIndex = 16;
            btn_notify.Text = "通知格式設定";
            btn_notify.UseVisualStyleBackColor = true;
            btn_notify.Click += btn_notify_Click;
            // 
            // btn_alarm
            // 
            btn_alarm.Location = new Point(813, 348);
            btn_alarm.Name = "btn_alarm";
            btn_alarm.Size = new Size(79, 40);
            btn_alarm.TabIndex = 17;
            btn_alarm.Text = "更新參數表";
            btn_alarm.UseVisualStyleBackColor = true;
            btn_alarm.Click += btn_alarm_Click;
            // 
            // button3
            // 
            button3.Location = new Point(815, 434);
            button3.Name = "button3";
            button3.Size = new Size(77, 33);
            button3.TabIndex = 19;
            button3.Text = "設定排程";
            button3.UseVisualStyleBackColor = true;
            button3.Visible = false;
            // 
            // btn_unit
            // 
            btn_unit.Location = new Point(13, 250);
            btn_unit.Name = "btn_unit";
            btn_unit.Size = new Size(124, 39);
            btn_unit.TabIndex = 20;
            btn_unit.Text = "單位顯示設定";
            btn_unit.UseVisualStyleBackColor = true;
            btn_unit.Click += btn_unit_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.AutoScrollMargin = new Size(20, 20);
            flowLayoutPanel1.Controls.Add(btn_Alrm_Notify);
            flowLayoutPanel1.Controls.Add(btn_usersetting);
            flowLayoutPanel1.Controls.Add(btn_Mail_Manager);
            flowLayoutPanel1.Controls.Add(btn_file_download);
            flowLayoutPanel1.Controls.Add(btn_notify);
            flowLayoutPanel1.Controls.Add(btn_unit);
            flowLayoutPanel1.Controls.Add(btn_history);
            flowLayoutPanel1.Controls.Add(btn_checkpoint);
            flowLayoutPanel1.Controls.Add(btn_UtilizationRate);
            flowLayoutPanel1.Location = new Point(47, 39);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(10);
            flowLayoutPanel1.Size = new Size(196, 467);
            flowLayoutPanel1.TabIndex = 21;
            // 
            // btn_UtilizationRate
            // 
            btn_UtilizationRate.Location = new Point(13, 388);
            btn_UtilizationRate.Name = "btn_UtilizationRate";
            btn_UtilizationRate.Size = new Size(124, 39);
            btn_UtilizationRate.TabIndex = 21;
            btn_UtilizationRate.Text = "稼動率對照圖";
            btn_UtilizationRate.UseVisualStyleBackColor = true;
            btn_UtilizationRate.Click += button4_Click;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(936, 664);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(button3);
            Controls.Add(btn_alarm);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btn_emailtest);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "Setting";
            Load += Setting_Load;
            flowLayoutPanel1.ResumeLayout(false);
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
        private Button btn_alarm;
        private Button button3;
        private Button btn_unit;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btn_UtilizationRate;
    }
}