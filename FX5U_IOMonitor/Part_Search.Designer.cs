using System.Windows.Forms;

namespace FX5U_IO元件監控
{
    partial class Part_Search
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
            label_txt = new Label();
            txB_search = new TextBox();
            label1 = new Label();
            label2 = new Label();
            comb_dataGridview = new ComboBox();
            comb_class = new ComboBox();
            panel1 = new Panel();
            btn_delete = new Button();
            btn_add_element = new Button();
            dataGridView1 = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label_txt
            // 
            label_txt.AutoSize = true;
            label_txt.Font = new Font("微軟正黑體", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label_txt.Location = new Point(556, 90);
            label_txt.Margin = new Padding(4, 0, 4, 0);
            label_txt.Name = "label_txt";
            label_txt.Size = new Size(73, 17);
            label_txt.TabIndex = 1;
            label_txt.Text = "文字搜尋：";
            // 
            // txB_search
            // 
            txB_search.Location = new Point(648, 90);
            txB_search.Margin = new Padding(4);
            txB_search.Name = "txB_search";
            txB_search.Size = new Size(236, 23);
            txB_search.TabIndex = 2;
            txB_search.TextChanged += txB_search_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("微軟正黑體", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label1.Location = new Point(510, 24);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(112, 17);
            label1.TabIndex = 3;
            label1.Text = "選擇搜尋資料表：";
            label1.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("微軟正黑體", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label2.Location = new Point(556, 55);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(73, 17);
            label2.TabIndex = 4;
            label2.Text = "分類選擇：";
            // 
            // comb_dataGridview
            // 
            comb_dataGridview.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_dataGridview.Font = new Font("微軟正黑體", 9F, FontStyle.Bold, GraphicsUnit.Point, 136);
            comb_dataGridview.FormattingEnabled = true;
            comb_dataGridview.Items.AddRange(new object[] { "鑽床", "鋸床" });
            comb_dataGridview.Location = new Point(649, 15);
            comb_dataGridview.Margin = new Padding(4);
            comb_dataGridview.Name = "comb_dataGridview";
            comb_dataGridview.Size = new Size(235, 24);
            comb_dataGridview.TabIndex = 5;
            comb_dataGridview.Visible = false;
            // 
            // comb_class
            // 
            comb_class.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_class.Font = new Font("微軟正黑體", 9F, FontStyle.Bold, GraphicsUnit.Point, 136);
            comb_class.FormattingEnabled = true;
            comb_class.Location = new Point(648, 52);
            comb_class.Margin = new Padding(4);
            comb_class.Name = "comb_class";
            comb_class.Size = new Size(235, 24);
            comb_class.TabIndex = 6;
            comb_class.SelectedIndexChanged += comb_class_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(btn_delete);
            panel1.Controls.Add(btn_add_element);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(comb_class);
            panel1.Controls.Add(label_txt);
            panel1.Controls.Add(comb_dataGridview);
            panel1.Controls.Add(txB_search);
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(920, 132);
            panel1.TabIndex = 7;
            // 
            // btn_delete
            // 
            btn_delete.Location = new Point(23, 73);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(98, 34);
            btn_delete.TabIndex = 8;
            btn_delete.Text = "元件刪除";
            btn_delete.UseVisualStyleBackColor = true;
            btn_delete.Click += btn_delete_Click;
            // 
            // btn_add_element
            // 
            btn_add_element.Location = new Point(23, 24);
            btn_add_element.Name = "btn_add_element";
            btn_add_element.Size = new Size(98, 34);
            btn_add_element.TabIndex = 7;
            btn_add_element.Text = "元件新增/更新";
            btn_add_element.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 132);
            dataGridView1.Margin = new Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(920, 493);
            dataGridView1.TabIndex = 8;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // Part_Search
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(920, 625);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Part_Search";
            StartPosition = FormStartPosition.CenterParent;
            Text = "元件設定";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label label_txt;
        private TextBox txB_search;
        private Label label1;
        private Label label2;
        private ComboBox comb_dataGridview;
        private ComboBox comb_class;
        private Panel panel1;
        private DataGridView dataGridView1;
        private Button btn_delete;
        private Button btn_add_element;
    }
}