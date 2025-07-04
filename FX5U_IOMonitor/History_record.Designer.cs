﻿using System.Windows.Forms;

namespace FX5U_IO元件監控
{
    partial class History_record
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
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            label3 = new Label();
            choose_event = new ComboBox();
            dateTime_end = new DateTimePicker();
            btn_search = new Button();
            dateTime_start = new DateTimePicker();
            dataGridView1 = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            label1.Location = new Point(14, 57);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(99, 17);
            label1.TabIndex = 3;
            label1.Text = "搜尋起始日期：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            label2.Location = new Point(14, 90);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(99, 17);
            label2.TabIndex = 4;
            label2.Text = "搜尋結束日期：";
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Controls.Add(choose_event);
            panel1.Controls.Add(dateTime_end);
            panel1.Controls.Add(btn_search);
            panel1.Controls.Add(dateTime_start);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(816, 126);
            panel1.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            label3.Location = new Point(13, 19);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(125, 17);
            label3.TabIndex = 18;
            label3.Text = "查詢歷史資料類別：";
            // 
            // choose_event
            // 
            choose_event.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            choose_event.BackColor = SystemColors.ButtonHighlight;
            choose_event.DropDownStyle = ComboBoxStyle.DropDownList;
            choose_event.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            choose_event.FormattingEnabled = true;
            choose_event.Items.AddRange(new object[] { "警告事件", "監控參數歸零事件" });
            choose_event.Location = new Point(179, 17);
            choose_event.Name = "choose_event";
            choose_event.Size = new Size(171, 25);
            choose_event.TabIndex = 17;
            // 
            // dateTime_end
            // 
            dateTime_end.Location = new Point(179, 85);
            dateTime_end.Name = "dateTime_end";
            dateTime_end.Size = new Size(171, 23);
            dateTime_end.TabIndex = 10;
            // 
            // btn_search
            // 
            btn_search.Location = new Point(372, 85);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(58, 23);
            btn_search.TabIndex = 7;
            btn_search.Text = "搜尋";
            btn_search.UseVisualStyleBackColor = true;
            btn_search.Click += btn_add_element_Click;
            // 
            // dateTime_start
            // 
            dateTime_start.Location = new Point(179, 53);
            dateTime_start.Name = "dateTime_start";
            dateTime_start.Size = new Size(171, 23);
            dateTime_start.TabIndex = 9;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 130);
            dataGridView1.Margin = new Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(816, 491);
            dataGridView1.TabIndex = 8;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;
            // 
            // History_record
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(817, 625);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "History_record";
            StartPosition = FormStartPosition.CenterParent;
            Text = "歷史紀錄查詢";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private Label label2;
        private Panel panel1;
        private DataGridView dataGridView1;
        private Button btn_search;
        private DateTimePicker dateTime_end;
        private DateTimePicker dateTime_start;
        private Label label3;
        private ComboBox choose_event;
    }
}