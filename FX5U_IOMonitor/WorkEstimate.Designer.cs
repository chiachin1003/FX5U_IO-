namespace FX5U_IOMonitor
{
    partial class WorkEstimate
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Panel mainPanel;
        private Panel topPanel;
        private Panel chartPanel;

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            TableLayoutPanel tlp = (TableLayoutPanel)sender;
            using (Pen p = new Pen(Color.Black, 1)) // 格子內線
            using (Pen thickPen = new Pen(Color.Black, 3)) // 外框線加粗
            {
                // 畫橫線
                for (int i = 1; i < tlp.RowCount; i++)
                {
                    int y = tlp.GetRowHeights().Take(i).Sum();
                    e.Graphics.DrawLine(p, 0, y, tlp.Width, y);
                }

                // 畫直線
                for (int i = 1; i < tlp.ColumnCount; i++)
                {
                    int x = tlp.GetColumnWidths().Take(i).Sum();
                    e.Graphics.DrawLine(p, x, 0, x, tlp.Height);
                }

                // 畫加粗的外框線
                e.Graphics.DrawRectangle(thickPen, 0, 0, tlp.Width - 1, tlp.Height - 1);
            }
        }
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
            lab_project = new Label();
            dataGridView1 = new DataGridView();
            combo_project = new ComboBox();
            lab_CompletionRate_numb = new Label();
            lab_TotalActual_numb = new Label();
            lab_TotalEstimated_numb = new Label();
            lab_TotalEstimated = new Label();
            lab_TotalActual = new Label();
            lab_CompletionRate = new Label();
            lab_Uncompleted_numb = new Label();
            lab_Completed_numb = new Label();
            lab_Total_numb = new Label();
            lab_Total = new Label();
            lab_Completed = new Label();
            lab_Uncompleted = new Label();
            npgsqlCommandBuilder1 = new Npgsql.NpgsqlCommandBuilder();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lab_project
            // 
            lab_project.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_project.AutoSize = true;
            lab_project.BackColor = SystemColors.ButtonHighlight;
            lab_project.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_project.Location = new Point(30, 20);
            lab_project.Name = "lab_project";
            lab_project.Size = new Size(117, 26);
            lab_project.TabIndex = 59;
            lab_project.Text = "專案編號：";
            lab_project.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(27, 61);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(673, 292);
            dataGridView1.TabIndex = 70;
            // 
            // combo_project
            // 
            combo_project.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            combo_project.FormattingEnabled = true;
            combo_project.Location = new Point(229, 17);
            combo_project.Name = "combo_project";
            combo_project.Size = new Size(172, 34);
            combo_project.TabIndex = 71;
            // 
            // lab_CompletionRate_numb
            // 
            lab_CompletionRate_numb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_CompletionRate_numb.AutoSize = true;
            lab_CompletionRate_numb.BackColor = SystemColors.ButtonHighlight;
            lab_CompletionRate_numb.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_CompletionRate_numb.Location = new Point(647, 418);
            lab_CompletionRate_numb.Name = "lab_CompletionRate_numb";
            lab_CompletionRate_numb.Size = new Size(0, 24);
            lab_CompletionRate_numb.TabIndex = 77;
            lab_CompletionRate_numb.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_TotalActual_numb
            // 
            lab_TotalActual_numb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_TotalActual_numb.AutoSize = true;
            lab_TotalActual_numb.BackColor = SystemColors.ButtonHighlight;
            lab_TotalActual_numb.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_TotalActual_numb.Location = new Point(401, 418);
            lab_TotalActual_numb.Name = "lab_TotalActual_numb";
            lab_TotalActual_numb.Size = new Size(0, 24);
            lab_TotalActual_numb.TabIndex = 76;
            lab_TotalActual_numb.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_TotalEstimated_numb
            // 
            lab_TotalEstimated_numb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_TotalEstimated_numb.AutoSize = true;
            lab_TotalEstimated_numb.BackColor = SystemColors.ButtonHighlight;
            lab_TotalEstimated_numb.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_TotalEstimated_numb.Location = new Point(134, 418);
            lab_TotalEstimated_numb.Name = "lab_TotalEstimated_numb";
            lab_TotalEstimated_numb.Size = new Size(0, 24);
            lab_TotalEstimated_numb.TabIndex = 75;
            lab_TotalEstimated_numb.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_TotalEstimated
            // 
            lab_TotalEstimated.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_TotalEstimated.BackColor = SystemColors.ButtonHighlight;
            lab_TotalEstimated.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            lab_TotalEstimated.Location = new Point(27, 418);
            lab_TotalEstimated.Name = "lab_TotalEstimated";
            lab_TotalEstimated.Size = new Size(91, 24);
            lab_TotalEstimated.TabIndex = 73;
            lab_TotalEstimated.Text = "預計工時:";
            lab_TotalEstimated.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_TotalActual
            // 
            lab_TotalActual.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_TotalActual.BackColor = SystemColors.ButtonHighlight;
            lab_TotalActual.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            lab_TotalActual.Location = new Point(247, 418);
            lab_TotalActual.Name = "lab_TotalActual";
            lab_TotalActual.Size = new Size(129, 24);
            lab_TotalActual.TabIndex = 72;
            lab_TotalActual.Text = "累計實際工時:";
            lab_TotalActual.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_CompletionRate
            // 
            lab_CompletionRate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_CompletionRate.BackColor = SystemColors.ButtonHighlight;
            lab_CompletionRate.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold);
            lab_CompletionRate.Location = new Point(514, 418);
            lab_CompletionRate.Name = "lab_CompletionRate";
            lab_CompletionRate.Size = new Size(110, 24);
            lab_CompletionRate.TabIndex = 74;
            lab_CompletionRate.Text = "完成百分比:";
            lab_CompletionRate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_Uncompleted_numb
            // 
            lab_Uncompleted_numb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_Uncompleted_numb.AutoSize = true;
            lab_Uncompleted_numb.BackColor = SystemColors.ButtonHighlight;
            lab_Uncompleted_numb.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Uncompleted_numb.Location = new Point(531, 370);
            lab_Uncompleted_numb.Name = "lab_Uncompleted_numb";
            lab_Uncompleted_numb.Size = new Size(0, 24);
            lab_Uncompleted_numb.TabIndex = 83;
            lab_Uncompleted_numb.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Completed_numb
            // 
            lab_Completed_numb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_Completed_numb.AutoSize = true;
            lab_Completed_numb.BackColor = SystemColors.ButtonHighlight;
            lab_Completed_numb.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Completed_numb.Location = new Point(322, 370);
            lab_Completed_numb.Name = "lab_Completed_numb";
            lab_Completed_numb.Size = new Size(0, 24);
            lab_Completed_numb.TabIndex = 82;
            lab_Completed_numb.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Total_numb
            // 
            lab_Total_numb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_Total_numb.AutoSize = true;
            lab_Total_numb.BackColor = SystemColors.ButtonHighlight;
            lab_Total_numb.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Total_numb.Location = new Point(127, 370);
            lab_Total_numb.Name = "lab_Total_numb";
            lab_Total_numb.Size = new Size(0, 24);
            lab_Total_numb.TabIndex = 81;
            lab_Total_numb.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Total
            // 
            lab_Total.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_Total.BackColor = SystemColors.ButtonHighlight;
            lab_Total.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Total.Location = new Point(27, 370);
            lab_Total.Name = "lab_Total";
            lab_Total.Size = new Size(86, 24);
            lab_Total.TabIndex = 79;
            lab_Total.Text = "總支數：";
            lab_Total.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_Completed
            // 
            lab_Completed.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_Completed.BackColor = SystemColors.ButtonHighlight;
            lab_Completed.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Completed.Location = new Point(189, 370);
            lab_Completed.Name = "lab_Completed";
            lab_Completed.Size = new Size(124, 24);
            lab_Completed.TabIndex = 78;
            lab_Completed.Text = "已完成支數：";
            lab_Completed.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lab_Uncompleted
            // 
            lab_Uncompleted.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lab_Uncompleted.BackColor = SystemColors.ButtonHighlight;
            lab_Uncompleted.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Uncompleted.Location = new Point(395, 370);
            lab_Uncompleted.Name = "lab_Uncompleted";
            lab_Uncompleted.Size = new Size(140, 24);
            lab_Uncompleted.TabIndex = 80;
            lab_Uncompleted.Text = "未完成支數：";
            lab_Uncompleted.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // npgsqlCommandBuilder1
            // 
            npgsqlCommandBuilder1.QuotePrefix = "\"";
            npgsqlCommandBuilder1.QuoteSuffix = "\"";
            // 
            // WorkEstimate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(723, 490);
            Controls.Add(lab_Uncompleted_numb);
            Controls.Add(lab_Completed_numb);
            Controls.Add(lab_Total_numb);
            Controls.Add(lab_Total);
            Controls.Add(lab_Completed);
            Controls.Add(lab_Uncompleted);
            Controls.Add(lab_CompletionRate_numb);
            Controls.Add(lab_TotalActual_numb);
            Controls.Add(lab_TotalEstimated_numb);
            Controls.Add(lab_TotalEstimated);
            Controls.Add(lab_TotalActual);
            Controls.Add(lab_CompletionRate);
            Controls.Add(combo_project);
            Controls.Add(dataGridView1);
            Controls.Add(lab_project);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WorkEstimate";
            StartPosition = FormStartPosition.CenterParent;
            Text = "預估工時";
            Load += WorkEstimate_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lab_project;
        private DataGridView dataGridView1;
        private ComboBox combo_project;
        private Label lab_CompletionRate_numb;
        private Label lab_TotalActual_numb;
        private Label lab_TotalEstimated_numb;
        private Label lab_TotalEstimated;
        private Label lab_TotalActual;
        private Label lab_CompletionRate;
        private Label lab_Uncompleted_numb;
        private Label lab_Completed_numb;
        private Label lab_Total_numb;
        private Label lab_Total;
        private Label lab_Completed;
        private Label lab_Uncompleted;
        private Npgsql.NpgsqlCommandBuilder npgsqlCommandBuilder1;
    }
}

