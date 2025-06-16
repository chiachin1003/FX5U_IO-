namespace FX5U_IOMonitor
{
    partial class ShowDetail
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
            tableLayoutPanel1 = new TableLayoutPanel();
            btn_showmain = new Button();
            btn_timereset = new Button();
            btn_history = new Button();
            btn_lifeSetting = new Button();
            start = new DataGridViewTextBoxColumn();
            End = new DataGridViewTextBoxColumn();
            use = new DataGridViewTextBoxColumn();
            panel_main = new Panel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(btn_showmain, 3, 0);
            tableLayoutPanel1.Controls.Add(btn_timereset, 0, 0);
            tableLayoutPanel1.Controls.Add(btn_history, 1, 0);
            tableLayoutPanel1.Controls.Add(btn_lifeSetting, 2, 0);
            tableLayoutPanel1.Location = new Point(12, 412);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(534, 69);
            tableLayoutPanel1.TabIndex = 12;
            // 
            // btn_showmain
            // 
            btn_showmain.Dock = DockStyle.Fill;
            btn_showmain.Font = new Font("微軟正黑體", 11.25F, FontStyle.Bold);
            btn_showmain.Location = new Point(402, 3);
            btn_showmain.Name = "btn_showmain";
            btn_showmain.Size = new Size(129, 63);
            btn_showmain.TabIndex = 3;
            btn_showmain.Text = "元件資訊";
            btn_showmain.UseVisualStyleBackColor = true;
            btn_showmain.Click += btn_showmain_Click;
            // 
            // btn_timereset
            // 
            btn_timereset.Dock = DockStyle.Fill;
            btn_timereset.Font = new Font("微軟正黑體", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btn_timereset.Location = new Point(3, 3);
            btn_timereset.Name = "btn_timereset";
            btn_timereset.Size = new Size(127, 63);
            btn_timereset.TabIndex = 0;
            btn_timereset.Text = "時間重置";
            btn_timereset.UseVisualStyleBackColor = true;
            btn_timereset.Click += btn_timereset_Click;
            // 
            // btn_history
            // 
            btn_history.Dock = DockStyle.Fill;
            btn_history.Font = new Font("微軟正黑體", 11.25F, FontStyle.Bold);
            btn_history.Location = new Point(136, 3);
            btn_history.Name = "btn_history";
            btn_history.Size = new Size(127, 63);
            btn_history.TabIndex = 1;
            btn_history.Text = "歷史資料";
            btn_history.UseVisualStyleBackColor = true;
            btn_history.Click += btn_history_Click;
            // 
            // btn_lifeSetting
            // 
            btn_lifeSetting.Dock = DockStyle.Fill;
            btn_lifeSetting.Font = new Font("微軟正黑體", 11.25F, FontStyle.Bold);
            btn_lifeSetting.Location = new Point(269, 3);
            btn_lifeSetting.Name = "btn_lifeSetting";
            btn_lifeSetting.Size = new Size(127, 63);
            btn_lifeSetting.TabIndex = 2;
            btn_lifeSetting.Text = "壽命設定";
            btn_lifeSetting.UseVisualStyleBackColor = true;
            btn_lifeSetting.Click += btn_lifeSetting_Click;
            // 
            // start
            // 
            start.Name = "start";
            // 
            // End
            // 
            End.Name = "End";
            // 
            // use
            // 
            use.Name = "use";
            // 
            // panel_main
            // 
            panel_main.Location = new Point(12, 12);
            panel_main.Name = "panel_main";
            panel_main.Size = new Size(534, 397);
            panel_main.TabIndex = 13;
            // 
            // ShowDetail
            // 
            AutoScaleDimensions = new SizeF(6F, 11F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(559, 493);
            Controls.Add(panel_main);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("新細明體", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ShowDetail";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private Button btn_timereset;
        private Button btn_history;
        private Button btn_lifeSetting;
        private DataGridViewTextBoxColumn start;
        private DataGridViewTextBoxColumn End;
        private DataGridViewTextBoxColumn use;
        private Button btn_showmain;
        private Panel panel_main;
    }
}