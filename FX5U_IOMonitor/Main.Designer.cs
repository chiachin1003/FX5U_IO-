namespace FX5U_IOMonitor
{
    partial class Main
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            panel_choose = new Panel();
            btn_setting = new Button();
            btn_email = new Button();
            btn_search = new Button();
            btn_Main = new Button();
            btn_connect = new Button();
            panel_main = new Panel();
            panel_select = new Panel();
            panel_language = new Panel();
            comb_language = new ComboBox();
            btn_language = new Button();
            btn_log_out = new Button();
            panel_choose.SuspendLayout();
            panel_select.SuspendLayout();
            panel_language.SuspendLayout();
            SuspendLayout();
            // 
            // panel_choose
            // 
            panel_choose.BackColor = SystemColors.ButtonHighlight;
            panel_choose.Controls.Add(btn_setting);
            panel_choose.Controls.Add(btn_email);
            panel_choose.Controls.Add(btn_search);
            panel_choose.Controls.Add(btn_Main);
            panel_choose.Controls.Add(btn_connect);
            panel_choose.Dock = DockStyle.Left;
            panel_choose.Location = new Point(0, 0);
            panel_choose.Margin = new Padding(4);
            panel_choose.Name = "panel_choose";
            panel_choose.Size = new Size(68, 691);
            panel_choose.TabIndex = 0;
            // 
            // btn_setting
            // 
            btn_setting.BackColor = SystemColors.ButtonHighlight;
            btn_setting.Dock = DockStyle.Bottom;
            btn_setting.FlatStyle = FlatStyle.Flat;
            btn_setting.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_setting.Location = new Point(0, 601);
            btn_setting.Margin = new Padding(4);
            btn_setting.Name = "btn_setting";
            btn_setting.Size = new Size(68, 45);
            btn_setting.TabIndex = 7;
            btn_setting.Text = "設定";
            btn_setting.UseVisualStyleBackColor = false;
            btn_setting.Click += btn_setting_Click;
            // 
            // btn_email
            // 
            btn_email.BackColor = SystemColors.ButtonHighlight;
            btn_email.Dock = DockStyle.Top;
            btn_email.FlatStyle = FlatStyle.Flat;
            btn_email.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_email.Location = new Point(0, 100);
            btn_email.Margin = new Padding(4);
            btn_email.Name = "btn_email";
            btn_email.Size = new Size(68, 50);
            btn_email.TabIndex = 6;
            btn_email.Text = "信箱\r\n設定";
            btn_email.UseVisualStyleBackColor = false;
            btn_email.Visible = false;
            btn_email.Click += btn_email_Click;
            // 
            // btn_search
            // 
            btn_search.BackColor = SystemColors.ButtonHighlight;
            btn_search.Dock = DockStyle.Top;
            btn_search.FlatStyle = FlatStyle.Flat;
            btn_search.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_search.Location = new Point(0, 50);
            btn_search.Margin = new Padding(4);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(68, 50);
            btn_search.TabIndex = 5;
            btn_search.Text = "故障\r\n排除表";
            btn_search.UseVisualStyleBackColor = false;
            btn_search.Visible = false;
            // 
            // btn_Main
            // 
            btn_Main.BackColor = SystemColors.ButtonHighlight;
            btn_Main.Dock = DockStyle.Top;
            btn_Main.FlatStyle = FlatStyle.Flat;
            btn_Main.Font = new Font("微軟正黑體", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_Main.Location = new Point(0, 0);
            btn_Main.Margin = new Padding(4);
            btn_Main.Name = "btn_Main";
            btn_Main.Size = new Size(68, 50);
            btn_Main.TabIndex = 17;
            btn_Main.Text = "主頁面";
            btn_Main.UseVisualStyleBackColor = false;
            btn_Main.Click += btn_Main_Click;
            // 
            // btn_connect
            // 
            btn_connect.BackColor = SystemColors.ButtonHighlight;
            btn_connect.Dock = DockStyle.Bottom;
            btn_connect.FlatStyle = FlatStyle.Flat;
            btn_connect.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_connect.Location = new Point(0, 646);
            btn_connect.Margin = new Padding(4);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(68, 45);
            btn_connect.TabIndex = 1;
            btn_connect.Text = "連線";
            btn_connect.UseVisualStyleBackColor = false;
            btn_connect.Click += btn_connect_Click;
            // 
            // panel_main
            // 
            panel_main.BackColor = SystemColors.ButtonHighlight;
            panel_main.Dock = DockStyle.Fill;
            panel_main.Location = new Point(68, 50);
            panel_main.Margin = new Padding(4);
            panel_main.Name = "panel_main";
            panel_main.Size = new Size(966, 641);
            panel_main.TabIndex = 3;
            // 
            // panel_select
            // 
            panel_select.BackColor = SystemColors.ButtonHighlight;
            panel_select.Controls.Add(panel_language);
            panel_select.Controls.Add(btn_log_out);
            panel_select.Dock = DockStyle.Top;
            panel_select.Location = new Point(68, 0);
            panel_select.Margin = new Padding(4);
            panel_select.Name = "panel_select";
            panel_select.Size = new Size(966, 50);
            panel_select.TabIndex = 1;
            // 
            // panel_language
            // 
            panel_language.BackColor = SystemColors.ButtonHighlight;
            panel_language.Controls.Add(comb_language);
            panel_language.Controls.Add(btn_language);
            panel_language.Dock = DockStyle.Right;
            panel_language.Location = new Point(813, 0);
            panel_language.Name = "panel_language";
            panel_language.Size = new Size(87, 50);
            panel_language.TabIndex = 15;
            // 
            // comb_language
            // 
            comb_language.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            comb_language.BackColor = SystemColors.ButtonHighlight;
            comb_language.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_language.FormattingEnabled = true;
            comb_language.Location = new Point(9, 3);
            comb_language.Name = "comb_language";
            comb_language.Size = new Size(75, 23);
            comb_language.TabIndex = 16;
            // 
            // btn_language
            // 
            btn_language.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_language.BackColor = SystemColors.ButtonHighlight;
            btn_language.Location = new Point(9, 28);
            btn_language.Name = "btn_language";
            btn_language.Size = new Size(75, 22);
            btn_language.TabIndex = 15;
            btn_language.Text = "語系切換";
            btn_language.UseVisualStyleBackColor = false;
            btn_language.Click += btn_language_Click;
            // 
            // btn_log_out
            // 
            btn_log_out.BackColor = SystemColors.ButtonHighlight;
            btn_log_out.Dock = DockStyle.Right;
            btn_log_out.FlatStyle = FlatStyle.Flat;
            btn_log_out.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_log_out.Location = new Point(900, 0);
            btn_log_out.Margin = new Padding(4);
            btn_log_out.Name = "btn_log_out";
            btn_log_out.Size = new Size(66, 50);
            btn_log_out.TabIndex = 11;
            btn_log_out.Text = "登出";
            btn_log_out.UseVisualStyleBackColor = false;
            btn_log_out.Click += btn_log_out_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1034, 691);
            Controls.Add(panel_main);
            Controls.Add(panel_select);
            Controls.Add(panel_choose);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "視覺化元件監控";
            WindowState = FormWindowState.Maximized;
            Shown += Main_Shown;
            panel_choose.ResumeLayout(false);
            panel_select.ResumeLayout(false);
            panel_language.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.Panel panel_choose;
        private System.Windows.Forms.Panel panel_select;
        public System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Button btn_search;
        private Button btn_log_out;
        private Button btn_email;
        private Panel panel_language;
        private Button btn_language;
        private ComboBox comb_language;
        private Button btn_setting;
        public Button btn_Main;
    }
}

