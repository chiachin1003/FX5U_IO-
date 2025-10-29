using System.Windows.Forms;

namespace FX5U_IOMonitor
{
    partial class Machine_main
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
            tableLayoutPanel6 = new TableLayoutPanel();
            label2 = new Label();
            lab_yellow = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            label3 = new Label();
            lab_green = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            label4 = new Label();
            lab_partalarm = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            label5 = new Label();
            lab_connect = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            lab_red = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label6 = new Label();
            lab_sum = new Label();
            lab_connectOK = new Label();
            label_txt = new Label();
            btn_search = new Button();
            txB_search = new TextBox();
            btn_addElement = new Button();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(label2, 0, 1);
            tableLayoutPanel6.Controls.Add(lab_yellow, 0, 0);
            tableLayoutPanel6.Location = new Point(647, 482);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 53.3284264F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 46.67157F));
            tableLayoutPanel6.Size = new Size(118, 108);
            tableLayoutPanel6.TabIndex = 32;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label2.Location = new Point(3, 57);
            label2.Name = "label2";
            label2.Size = new Size(112, 51);
            label2.TabIndex = 23;
            label2.Text = "黃燈總數";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_yellow
            // 
            lab_yellow.AutoSize = true;
            lab_yellow.BackColor = SystemColors.ButtonHighlight;
            lab_yellow.Dock = DockStyle.Fill;
            lab_yellow.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_yellow.Location = new Point(3, 0);
            lab_yellow.Name = "lab_yellow";
            lab_yellow.Size = new Size(112, 57);
            lab_yellow.TabIndex = 21;
            lab_yellow.TextAlign = ContentAlignment.MiddleCenter;
            lab_yellow.Click += lab_yellow_Click;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(label3, 0, 1);
            tableLayoutPanel5.Controls.Add(lab_green, 0, 0);
            tableLayoutPanel5.Location = new Point(492, 482);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 54.98404F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 45.01596F));
            tableLayoutPanel5.Size = new Size(118, 108);
            tableLayoutPanel5.TabIndex = 31;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonHighlight;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label3.Location = new Point(3, 59);
            label3.Name = "label3";
            label3.Size = new Size(112, 49);
            label3.TabIndex = 23;
            label3.Text = "綠燈總數";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_green
            // 
            lab_green.AutoSize = true;
            lab_green.BackColor = SystemColors.ButtonHighlight;
            lab_green.Dock = DockStyle.Fill;
            lab_green.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_green.Location = new Point(3, 0);
            lab_green.Name = "lab_green";
            lab_green.Size = new Size(112, 59);
            lab_green.TabIndex = 21;
            lab_green.TextAlign = ContentAlignment.MiddleCenter;
            lab_green.Click += lab_green_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(label4, 0, 1);
            tableLayoutPanel4.Controls.Add(lab_partalarm, 0, 0);
            tableLayoutPanel4.Location = new Point(336, 482);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tableLayoutPanel4.Size = new Size(118, 108);
            tableLayoutPanel4.TabIndex = 30;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ButtonHighlight;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label4.Location = new Point(3, 59);
            label4.Name = "label4";
            label4.Size = new Size(112, 49);
            label4.TabIndex = 23;
            label4.Text = "元件異常";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_partalarm
            // 
            lab_partalarm.AutoSize = true;
            lab_partalarm.BackColor = SystemColors.ButtonHighlight;
            lab_partalarm.Dock = DockStyle.Fill;
            lab_partalarm.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_partalarm.Location = new Point(3, 0);
            lab_partalarm.Name = "lab_partalarm";
            lab_partalarm.Size = new Size(112, 59);
            lab_partalarm.TabIndex = 21;
            lab_partalarm.TextAlign = ContentAlignment.MiddleCenter;
            lab_partalarm.Click += lab_partalarm_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(label5, 0, 1);
            tableLayoutPanel3.Controls.Add(lab_connect, 0, 0);
            tableLayoutPanel3.Location = new Point(180, 482);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tableLayoutPanel3.Size = new Size(118, 108);
            tableLayoutPanel3.TabIndex = 29;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ButtonHighlight;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label5.Location = new Point(3, 59);
            label5.Name = "label5";
            label5.Size = new Size(112, 49);
            label5.TabIndex = 23;
            label5.Text = "連接總數";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_connect
            // 
            lab_connect.AutoSize = true;
            lab_connect.BackColor = SystemColors.ButtonHighlight;
            lab_connect.Dock = DockStyle.Fill;
            lab_connect.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_connect.Location = new Point(3, 0);
            lab_connect.Name = "lab_connect";
            lab_connect.Size = new Size(112, 59);
            lab_connect.TabIndex = 21;
            lab_connect.TextAlign = ContentAlignment.MiddleCenter;
            lab_connect.Click += lab_connect_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(label1, 0, 1);
            tableLayoutPanel2.Controls.Add(lab_red, 0, 0);
            tableLayoutPanel2.Location = new Point(807, 482);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 54.98404F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 45.01596F));
            tableLayoutPanel2.Size = new Size(118, 108);
            tableLayoutPanel2.TabIndex = 28;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonHighlight;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label1.Location = new Point(3, 59);
            label1.Name = "label1";
            label1.Size = new Size(112, 49);
            label1.TabIndex = 22;
            label1.Text = "紅燈總數";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_red
            // 
            lab_red.AutoSize = true;
            lab_red.BackColor = SystemColors.ButtonHighlight;
            lab_red.Dock = DockStyle.Fill;
            lab_red.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_red.Location = new Point(3, 0);
            lab_red.Name = "lab_red";
            lab_red.Size = new Size(112, 59);
            lab_red.TabIndex = 21;
            lab_red.TextAlign = ContentAlignment.MiddleCenter;
            lab_red.Click += lab_red_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label6, 0, 1);
            tableLayoutPanel1.Controls.Add(lab_sum, 0, 0);
            tableLayoutPanel1.Location = new Point(25, 482);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9816246F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0183754F));
            tableLayoutPanel1.Size = new Size(118, 108);
            tableLayoutPanel1.TabIndex = 27;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ButtonHighlight;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            label6.Location = new Point(3, 59);
            label6.Name = "label6";
            label6.Size = new Size(112, 49);
            label6.TabIndex = 23;
            label6.Text = "監控總數";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_sum
            // 
            lab_sum.AutoSize = true;
            lab_sum.BackColor = SystemColors.ButtonHighlight;
            lab_sum.Dock = DockStyle.Fill;
            lab_sum.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            lab_sum.Location = new Point(3, 0);
            lab_sum.Name = "lab_sum";
            lab_sum.Size = new Size(112, 59);
            lab_sum.TabIndex = 21;
            lab_sum.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_connectOK
            // 
            lab_connectOK.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_connectOK.AutoSize = true;
            lab_connectOK.BackColor = SystemColors.ButtonHighlight;
            lab_connectOK.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_connectOK.Location = new Point(12, 604);
            lab_connectOK.Name = "lab_connectOK";
            lab_connectOK.Size = new Size(75, 26);
            lab_connectOK.TabIndex = 33;
            lab_connectOK.Text = "未連接";
            lab_connectOK.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_txt
            // 
            label_txt.AutoSize = true;
            label_txt.BackColor = SystemColors.ButtonHighlight;
            label_txt.Font = new Font("微軟正黑體", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label_txt.Location = new Point(9, 20);
            label_txt.Margin = new Padding(4, 0, 4, 0);
            label_txt.Name = "label_txt";
            label_txt.Size = new Size(73, 17);
            label_txt.TabIndex = 35;
            label_txt.Text = "文字搜尋：";
            label_txt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_search
            // 
            btn_search.BackColor = SystemColors.ButtonHighlight;
            btn_search.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_search.Location = new Point(312, 13);
            btn_search.Margin = new Padding(4);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(67, 28);
            btn_search.TabIndex = 34;
            btn_search.Text = "搜尋";
            btn_search.UseVisualStyleBackColor = false;
            btn_search.Click += btn_search_Click;
            // 
            // txB_search
            // 
            txB_search.Location = new Point(83, 16);
            txB_search.Margin = new Padding(4);
            txB_search.Name = "txB_search";
            txB_search.Size = new Size(221, 23);
            txB_search.TabIndex = 36;
            // 
            // btn_addElement
            // 
            btn_addElement.BackColor = SystemColors.ButtonHighlight;
            btn_addElement.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_addElement.Location = new Point(779, 11);
            btn_addElement.Margin = new Padding(4);
            btn_addElement.Name = "btn_addElement";
            btn_addElement.Size = new Size(143, 30);
            btn_addElement.TabIndex = 37;
            btn_addElement.Text = "新增元件";
            btn_addElement.UseVisualStyleBackColor = false;
            btn_addElement.Click += btn_addElement_Click;
            // 
            // Machine_main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(936, 664);
            Controls.Add(btn_addElement);
            Controls.Add(label_txt);
            Controls.Add(btn_search);
            Controls.Add(txB_search);
            Controls.Add(lab_connectOK);
            Controls.Add(tableLayoutPanel6);
            Controls.Add(tableLayoutPanel5);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "Machine_main";
            Text = "s";
            Load += Machine_main_Load;
            VisibleChanged += Swing_main_VisibleChanged;
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel6;
        private Label label2;
        private Label lab_yellow;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label3;
        private Label lab_green;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label4;
        private Label lab_partalarm;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label5;
        private Label lab_connect;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Label lab_red;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label6;
        private Label lab_sum;
        private Label lab_connectOK;
        private Label label_txt;
        private Button btn_search;
        private TextBox txB_search;
        private Button btn_addElement;
    }
}