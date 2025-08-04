using System.Windows.Forms;

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
            lab_startTime = new Label();
            lab_endTime = new Label();
            panel1 = new Panel();
            lab_unit = new Label();
            comb_unit = new ComboBox();
            label4 = new Label();
            comb_name = new ComboBox();
            lab_metricType = new Label();
            comb_record = new ComboBox();
            btn_exportCsv = new Button();
            lab_historyType = new Label();
            choose_event = new ComboBox();
            dateTime_end = new DateTimePicker();
            btn_search = new Button();
            dateTime_start = new DateTimePicker();
            panel2 = new Panel();
            dataGridView1 = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lab_startTime
            // 
            lab_startTime.AutoSize = true;
            lab_startTime.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            lab_startTime.Location = new Point(392, 23);
            lab_startTime.Margin = new Padding(4, 0, 4, 0);
            lab_startTime.Name = "lab_startTime";
            lab_startTime.Size = new Size(99, 17);
            lab_startTime.TabIndex = 3;
            lab_startTime.Text = "搜尋起始日期：";
            // 
            // lab_endTime
            // 
            lab_endTime.AutoSize = true;
            lab_endTime.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            lab_endTime.Location = new Point(392, 56);
            lab_endTime.Margin = new Padding(4, 0, 4, 0);
            lab_endTime.Name = "lab_endTime";
            lab_endTime.Size = new Size(99, 17);
            lab_endTime.TabIndex = 4;
            lab_endTime.Text = "搜尋結束日期：";
            // 
            // panel1
            // 
            panel1.Controls.Add(lab_unit);
            panel1.Controls.Add(comb_unit);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(comb_name);
            panel1.Controls.Add(lab_metricType);
            panel1.Controls.Add(comb_record);
            panel1.Controls.Add(btn_exportCsv);
            panel1.Controls.Add(lab_historyType);
            panel1.Controls.Add(choose_event);
            panel1.Controls.Add(dateTime_end);
            panel1.Controls.Add(btn_search);
            panel1.Controls.Add(dateTime_start);
            panel1.Controls.Add(lab_startTime);
            panel1.Controls.Add(lab_endTime);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(817, 118);
            panel1.TabIndex = 7;
            // 
            // lab_unit
            // 
            lab_unit.AutoSize = true;
            lab_unit.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            lab_unit.Location = new Point(13, 87);
            lab_unit.Margin = new Padding(4, 0, 4, 0);
            lab_unit.Name = "lab_unit";
            lab_unit.Size = new Size(73, 17);
            lab_unit.TabIndex = 25;
            lab_unit.Text = "單位選擇：";
            lab_unit.Visible = false;
            // 
            // comb_unit
            // 
            comb_unit.BackColor = SystemColors.ButtonHighlight;
            comb_unit.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_unit.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            comb_unit.FormattingEnabled = true;
            comb_unit.Items.AddRange(new object[] { "每周", "每月" });
            comb_unit.Location = new Point(208, 88);
            comb_unit.Name = "comb_unit";
            comb_unit.Size = new Size(172, 25);
            comb_unit.TabIndex = 24;
            comb_unit.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            label4.Location = new Point(13, 122);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(73, 17);
            label4.TabIndex = 23;
            label4.Text = "選擇參數：";
            label4.Visible = false;
            // 
            // comb_name
            // 
            comb_name.BackColor = SystemColors.ButtonHighlight;
            comb_name.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_name.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            comb_name.FormattingEnabled = true;
            comb_name.Items.AddRange(new object[] { "每周", "每月" });
            comb_name.Location = new Point(179, 123);
            comb_name.Name = "comb_name";
            comb_name.Size = new Size(172, 25);
            comb_name.TabIndex = 22;
            comb_name.Visible = false;
            // 
            // lab_metricType
            // 
            lab_metricType.AutoSize = true;
            lab_metricType.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            lab_metricType.Location = new Point(13, 54);
            lab_metricType.Margin = new Padding(4, 0, 4, 0);
            lab_metricType.Name = "lab_metricType";
            lab_metricType.Size = new Size(99, 17);
            lab_metricType.TabIndex = 21;
            lab_metricType.Text = "選擇記錄間隔：";
            // 
            // comb_record
            // 
            comb_record.BackColor = SystemColors.ButtonHighlight;
            comb_record.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_record.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            comb_record.FormattingEnabled = true;
            comb_record.Items.AddRange(new object[] { "每周", "每月" });
            comb_record.Location = new Point(208, 57);
            comb_record.Name = "comb_record";
            comb_record.Size = new Size(172, 25);
            comb_record.TabIndex = 20;
            // 
            // btn_exportCsv
            // 
            btn_exportCsv.Location = new Point(733, 56);
            btn_exportCsv.Name = "btn_exportCsv";
            btn_exportCsv.Size = new Size(58, 23);
            btn_exportCsv.TabIndex = 19;
            btn_exportCsv.Text = "匯出";
            btn_exportCsv.UseVisualStyleBackColor = true;
            btn_exportCsv.Click += btn_exportCsv_Click;
            // 
            // lab_historyType
            // 
            lab_historyType.AutoSize = true;
            lab_historyType.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            lab_historyType.Location = new Point(13, 24);
            lab_historyType.Margin = new Padding(4, 0, 4, 0);
            lab_historyType.Name = "lab_historyType";
            lab_historyType.Size = new Size(125, 17);
            lab_historyType.TabIndex = 18;
            lab_historyType.Text = "查詢歷史資料類別：";
            // 
            // choose_event
            // 
            choose_event.BackColor = SystemColors.ButtonHighlight;
            choose_event.DropDownStyle = ComboBoxStyle.DropDownList;
            choose_event.Font = new Font("微軟正黑體", 9.75F, FontStyle.Bold);
            choose_event.FormattingEnabled = true;
            choose_event.Items.AddRange(new object[] { "警告事件", "監控參數" });
            choose_event.Location = new Point(208, 21);
            choose_event.Name = "choose_event";
            choose_event.Size = new Size(172, 25);
            choose_event.TabIndex = 17;
            choose_event.SelectedIndexChanged += choose_event_SelectedIndexChanged;
            // 
            // dateTime_end
            // 
            dateTime_end.Location = new Point(532, 54);
            dateTime_end.Name = "dateTime_end";
            dateTime_end.Size = new Size(171, 23);
            dateTime_end.TabIndex = 10;
            // 
            // btn_search
            // 
            btn_search.Location = new Point(733, 21);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(58, 23);
            btn_search.TabIndex = 7;
            btn_search.Text = "搜尋";
            btn_search.UseVisualStyleBackColor = true;
            btn_search.Click += btn_add_element_Click;
            // 
            // dateTime_start
            // 
            dateTime_start.Location = new Point(532, 22);
            dateTime_start.Name = "dateTime_start";
            dateTime_start.Size = new Size(171, 23);
            dateTime_start.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.Controls.Add(dataGridView1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 118);
            panel2.Name = "panel2";
            panel2.Size = new Size(817, 507);
            panel2.TabIndex = 9;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Margin = new Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(817, 507);
            dataGridView1.TabIndex = 9;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;
            // 
            // History_record
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(817, 625);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "History_record";
            StartPosition = FormStartPosition.CenterParent;
            Text = "歷史紀錄查詢";
            Load += History_record_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lab_startTime;
        private Label lab_endTime;
        private Panel panel1;
        private Button btn_search;
        private DateTimePicker dateTime_end;
        private DateTimePicker dateTime_start;
        private Label lab_historyType;
        private ComboBox choose_event;
        private Button btn_exportCsv;
        private Label lab_metricType;
        private ComboBox comb_record;
        private Panel panel2;
        private DataGridView dataGridView1;
        private Label lab_unit;
        private ComboBox comb_unit;
        private Label label4;
        private ComboBox comb_name;
    }
}