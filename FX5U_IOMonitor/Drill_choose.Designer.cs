using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace FX5U_IOMonitor
{
    partial class Drill_choose
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private static void SetCircularShape(Control control)
        {
            // 使用 GraphicsPath 設置圓形區域
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, 30, 30);
            control.Region = new Region(path);
        }
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
            btn_X_IO = new Button();
            btn_Y_IO = new Button();
            panel1 = new Panel();
            label_Connect = new Label();
            panel2 = new Panel();
            label_G = new Label();
            panel3 = new Panel();
            label_Y = new Label();
            panel4 = new Panel();
            label_R = new Label();
            panel5 = new Panel();
            label_disconnect = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            txB_search = new TextBox();
            label_txt = new Label();
            btn_search = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_X_IO
            // 
            btn_X_IO.Dock = DockStyle.Left;
            btn_X_IO.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_X_IO.Location = new Point(0, 0);
            btn_X_IO.Margin = new Padding(4);
            btn_X_IO.Name = "btn_X_IO";
            btn_X_IO.Size = new Size(44, 34);
            btn_X_IO.TabIndex = 0;
            btn_X_IO.Text = "輸入監控";
            btn_X_IO.UseVisualStyleBackColor = true;
            btn_X_IO.Visible = false;
            btn_X_IO.Click += btn_X_IO_Click;
            // 
            // btn_Y_IO
            // 
            btn_Y_IO.Dock = DockStyle.Left;
            btn_Y_IO.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_Y_IO.Location = new Point(44, 0);
            btn_Y_IO.Margin = new Padding(4);
            btn_Y_IO.Name = "btn_Y_IO";
            btn_Y_IO.Size = new Size(50, 34);
            btn_Y_IO.TabIndex = 1;
            btn_Y_IO.Text = "輸出監控";
            btn_Y_IO.UseVisualStyleBackColor = true;
            btn_Y_IO.Visible = false;
            btn_Y_IO.Click += btn_Y_IO_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(label_Connect);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(94, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(49, 34);
            panel1.TabIndex = 5;
            panel1.Visible = false;
            // 
            // label_Connect
            // 
            label_Connect.AutoSize = true;
            label_Connect.Dock = DockStyle.Fill;
            label_Connect.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label_Connect.Location = new Point(0, 0);
            label_Connect.Margin = new Padding(4, 0, 4, 0);
            label_Connect.Name = "label_Connect";
            label_Connect.Size = new Size(31, 48);
            label_Connect.TabIndex = 0;
            label_Connect.Text = "連接\r\n數量\r\n0";
            label_Connect.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(label_G);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(143, 0);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(57, 34);
            panel2.TabIndex = 6;
            panel2.Visible = false;
            // 
            // label_G
            // 
            label_G.AutoSize = true;
            label_G.Dock = DockStyle.Fill;
            label_G.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label_G.Location = new Point(0, 0);
            label_G.Margin = new Padding(4, 0, 4, 0);
            label_G.Name = "label_G";
            label_G.Size = new Size(31, 32);
            label_G.TabIndex = 0;
            label_G.Text = "綠燈\r\n數量";
            label_G.TextAlign = ContentAlignment.TopCenter;
            label_G.Click += label_G_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(label_Y);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(200, 0);
            panel3.Margin = new Padding(4);
            panel3.Name = "panel3";
            panel3.Size = new Size(51, 34);
            panel3.TabIndex = 7;
            panel3.Visible = false;
            // 
            // label_Y
            // 
            label_Y.AutoSize = true;
            label_Y.Dock = DockStyle.Fill;
            label_Y.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label_Y.Location = new Point(0, 0);
            label_Y.Margin = new Padding(4, 0, 4, 0);
            label_Y.Name = "label_Y";
            label_Y.Size = new Size(31, 32);
            label_Y.TabIndex = 0;
            label_Y.Text = "黃燈\r\n數量";
            label_Y.Click += label_Y_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(label_R);
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(251, 0);
            panel4.Margin = new Padding(4);
            panel4.Name = "panel4";
            panel4.Size = new Size(54, 34);
            panel4.TabIndex = 8;
            panel4.Visible = false;
            // 
            // label_R
            // 
            label_R.AutoSize = true;
            label_R.Dock = DockStyle.Fill;
            label_R.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label_R.Location = new Point(0, 0);
            label_R.Margin = new Padding(4, 0, 4, 0);
            label_R.Name = "label_R";
            label_R.Size = new Size(31, 32);
            label_R.TabIndex = 0;
            label_R.Text = "紅燈\r\n數量";
            label_R.Click += label_R_Click;
            // 
            // panel5
            // 
            panel5.Controls.Add(label_disconnect);
            panel5.Dock = DockStyle.Left;
            panel5.Location = new Point(305, 0);
            panel5.Margin = new Padding(4);
            panel5.Name = "panel5";
            panel5.Size = new Size(77, 34);
            panel5.TabIndex = 9;
            panel5.Visible = false;
            // 
            // label_disconnect
            // 
            label_disconnect.AutoSize = true;
            label_disconnect.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label_disconnect.Location = new Point(2, 16);
            label_disconnect.Margin = new Padding(4, 0, 4, 0);
            label_disconnect.Name = "label_disconnect";
            label_disconnect.Size = new Size(55, 16);
            label_disconnect.TabIndex = 0;
            label_disconnect.Text = "未連接數";
            label_disconnect.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.5243912F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.47561F));
            tableLayoutPanel1.Controls.Add(txB_search, 1, 0);
            tableLayoutPanel1.Controls.Add(label_txt, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Left;
            tableLayoutPanel1.Location = new Point(382, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(311, 34);
            tableLayoutPanel1.TabIndex = 11;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // txB_search
            // 
            txB_search.Dock = DockStyle.Fill;
            txB_search.Location = new Point(86, 4);
            txB_search.Margin = new Padding(4);
            txB_search.Name = "txB_search";
            txB_search.Size = new Size(221, 23);
            txB_search.TabIndex = 24;
            txB_search.TextChanged += txB_search_TextChanged_1;
            // 
            // label_txt
            // 
            label_txt.AutoSize = true;
            label_txt.Dock = DockStyle.Fill;
            label_txt.Font = new Font("微軟正黑體", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label_txt.Location = new Point(4, 0);
            label_txt.Margin = new Padding(4, 0, 4, 0);
            label_txt.Name = "label_txt";
            label_txt.Size = new Size(74, 34);
            label_txt.TabIndex = 23;
            label_txt.Text = "文字搜尋：";
            label_txt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_search
            // 
            btn_search.Dock = DockStyle.Left;
            btn_search.Font = new Font("微軟正黑體", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btn_search.Location = new Point(693, 0);
            btn_search.Margin = new Padding(4);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(44, 34);
            btn_search.TabIndex = 12;
            btn_search.Text = "搜尋";
            btn_search.UseVisualStyleBackColor = true;
            btn_search.Click += btn_search_Click;
            // 
            // Drill_choose
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 34);
            Controls.Add(btn_search);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btn_Y_IO);
            Controls.Add(btn_X_IO);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "Drill_choose";
            Text = "Form1";
            Load += Drill_choose_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btn_X_IO;
        private System.Windows.Forms.Button btn_Y_IO;
        private Panel panel1;
        private Label label_Connect;
        private Panel panel2;
        private Label label_G;
        private Panel panel3;
        private Label label_Y;
        private Panel panel4;
        private Label label_R;
        private Panel panel5;
        private Label label_disconnect;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btn_search;
        private TextBox txB_search;
        private Label label_txt;
    }

}