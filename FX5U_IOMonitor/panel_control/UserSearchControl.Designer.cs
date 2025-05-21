namespace FX5U_IOMonitor.panel_control
{
    partial class UserSearchControl
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

        #region Component Designer generated code
        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            tableLayoutPanel6 = new TableLayoutPanel();
            label2 = new Label();
            lab_yellow = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            label3 = new Label();
            lab_green = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            lab_red = new Label();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            panel_example = new Panel();
            lab_effect = new Label();
            Progress_RUL_precent = new ProgressBar();
            label_percent = new Label();
            panel_light = new Panel();
            lab_equipment = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            panel1.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel_example.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel6);
            panel1.Controls.Add(tableLayoutPanel5);
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 529);
            panel1.Name = "panel1";
            panel1.Size = new Size(925, 108);
            panel1.TabIndex = 1;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Controls.Add(label2, 0, 1);
            tableLayoutPanel6.Controls.Add(lab_yellow, 0, 0);
            tableLayoutPanel6.Location = new Point(132, 10);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.Size = new Size(100, 90);
            tableLayoutPanel6.TabIndex = 29;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonFace;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label2.Location = new Point(3, 45);
            label2.Name = "label2";
            label2.Size = new Size(94, 45);
            label2.TabIndex = 23;
            label2.Text = "黃燈總數";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_yellow
            // 
            lab_yellow.AutoSize = true;
            lab_yellow.BackColor = SystemColors.ButtonFace;
            lab_yellow.Dock = DockStyle.Fill;
            lab_yellow.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yellow.Location = new Point(3, 0);
            lab_yellow.Name = "lab_yellow";
            lab_yellow.Size = new Size(94, 45);
            lab_yellow.TabIndex = 21;
            lab_yellow.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(label3, 0, 1);
            tableLayoutPanel5.Controls.Add(lab_green, 0, 0);
            tableLayoutPanel5.Location = new Point(12, 10);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 46.6666679F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 53.3333321F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel5.Size = new Size(100, 90);
            tableLayoutPanel5.TabIndex = 28;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonFace;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label3.Location = new Point(3, 42);
            label3.Name = "label3";
            label3.Size = new Size(94, 48);
            label3.TabIndex = 23;
            label3.Text = "綠燈總數";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_green
            // 
            lab_green.AutoSize = true;
            lab_green.BackColor = SystemColors.ButtonFace;
            lab_green.Dock = DockStyle.Fill;
            lab_green.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_green.Location = new Point(3, 0);
            lab_green.Name = "lab_green";
            lab_green.Size = new Size(94, 42);
            lab_green.TabIndex = 21;
            lab_green.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(label1, 0, 1);
            tableLayoutPanel2.Controls.Add(lab_red, 0, 0);
            tableLayoutPanel2.Location = new Point(252, 10);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 47.77778F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 52.22222F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(100, 90);
            tableLayoutPanel2.TabIndex = 27;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonFace;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(3, 43);
            label1.Name = "label1";
            label1.Size = new Size(94, 47);
            label1.TabIndex = 22;
            label1.Text = "紅燈總數";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_red
            // 
            lab_red.AutoSize = true;
            lab_red.BackColor = SystemColors.ButtonFace;
            lab_red.Dock = DockStyle.Fill;
            lab_red.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_red.Location = new Point(3, 0);
            lab_red.Name = "lab_red";
            lab_red.Size = new Size(94, 43);
            lab_red.TabIndex = 21;
            lab_red.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // panel_example
            // 
            panel_example.Controls.Add(lab_effect);
            panel_example.Controls.Add(Progress_RUL_precent);
            panel_example.Controls.Add(label_percent);
            panel_example.Controls.Add(panel_light);
            panel_example.Controls.Add(lab_equipment);
            panel_example.Location = new Point(4, 4);
            panel_example.Margin = new Padding(4);
            panel_example.Name = "panel_example";
            panel_example.Size = new Size(233, 122);
            panel_example.TabIndex = 2;
            panel_example.Visible = false;
            // 
            // lab_effect
            // 
            lab_effect.AutoSize = true;
            lab_effect.Font = new Font("微軟正黑體", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_effect.Location = new Point(10, 61);
            lab_effect.Margin = new Padding(4, 0, 4, 0);
            lab_effect.Name = "lab_effect";
            lab_effect.Size = new Size(63, 19);
            lab_effect.TabIndex = 4;
            lab_effect.Text = "123456";
            // 
            // Progress_RUL_precent
            // 
            Progress_RUL_precent.Location = new Point(117, 66);
            Progress_RUL_precent.Margin = new Padding(4);
            Progress_RUL_precent.Name = "Progress_RUL_precent";
            Progress_RUL_precent.Size = new Size(96, 19);
            Progress_RUL_precent.TabIndex = 3;
            // 
            // label_percent
            // 
            label_percent.AutoSize = true;
            label_percent.Font = new Font("微軟正黑體", 7F, FontStyle.Bold);
            label_percent.Location = new Point(131, 89);
            label_percent.Margin = new Padding(4, 0, 4, 0);
            label_percent.Name = "label_percent";
            label_percent.Size = new Size(52, 15);
            label_percent.TabIndex = 2;
            label_percent.Text = "            %";
            // 
            // panel_light
            // 
            panel_light.Location = new Point(146, 14);
            panel_light.Margin = new Padding(4);
            panel_light.Name = "panel_light";
            panel_light.Size = new Size(46, 45);
            panel_light.TabIndex = 1;
            // 
            // lab_equipment
            // 
            lab_equipment.AutoSize = true;
            lab_equipment.Font = new Font("微軟正黑體", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_equipment.Location = new Point(10, 14);
            lab_equipment.Margin = new Padding(4, 0, 4, 0);
            lab_equipment.Name = "lab_equipment";
            lab_equipment.Size = new Size(69, 19);
            lab_equipment.TabIndex = 0;
            lab_equipment.Text = "設備名稱";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoScroll = true;
            flowLayoutPanel2.Controls.Add(panel_example);
            flowLayoutPanel2.Dock = DockStyle.Top;
            flowLayoutPanel2.Location = new Point(0, 0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(925, 529);
            flowLayoutPanel2.TabIndex = 3;
            // 
            // UserSearchControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(flowLayoutPanel2);
            Margin = new Padding(4);
            Name = "UserSearchControl";
            Size = new Size(925, 749);
            panel1.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel_example.ResumeLayout(false);
            panel_example.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label2;
        private Label lab_yellow;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label3;
        private Label lab_green;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Label lab_red;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private Panel panel_example;
        private Label lab_effect;
        private ProgressBar Progress_RUL_precent;
        private Label label_percent;
        private Panel panel_light;
        private Label lab_equipment;
        private FlowLayoutPanel flowLayoutPanel2;
    }
}
