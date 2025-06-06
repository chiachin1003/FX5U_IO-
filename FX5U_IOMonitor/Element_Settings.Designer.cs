namespace FX5U_IOMonitor.Resources
{
    partial class Element_Settings
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
            lab_green = new Label();
            btn_add = new Button();
            btn_update = new Button();
            lab_yellowText = new Label();
            lab_redText = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            comb_type = new ComboBox();
            lab_elementType = new Label();
            comb_machine = new ComboBox();
            lab_machineType = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            txb_max_number = new TextBox();
            lab_maxlifesetting = new Label();
            txb_comment = new TextBox();
            lab_describe = new Label();
            txb_description = new TextBox();
            lab_equipment = new Label();
            comb_class = new ComboBox();
            lab_class = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            txb_yellow_light = new NumericUpDown();
            lab_yellow = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            txb_red_light = new NumericUpDown();
            lab_red = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            txb_address = new TextBox();
            comb_io = new ComboBox();
            lab_elementLocal = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txb_yellow_light).BeginInit();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txb_red_light).BeginInit();
            tableLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // lab_green
            // 
            lab_green.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_green.Location = new Point(23, 324);
            lab_green.Name = "lab_green";
            lab_green.Size = new Size(430, 26);
            lab_green.TabIndex = 8;
            lab_green.Text = "綠燈健康狀態為黃燈設定值以上";
            // 
            // btn_add
            // 
            btn_add.Location = new Point(352, 505);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(78, 36);
            btn_add.TabIndex = 25;
            btn_add.Text = "新增";
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Click += btn_add_Click;
            // 
            // btn_update
            // 
            btn_update.Location = new Point(265, 505);
            btn_update.Name = "btn_update";
            btn_update.Size = new Size(81, 36);
            btn_update.TabIndex = 32;
            btn_update.Text = "更新";
            btn_update.UseVisualStyleBackColor = true;
            btn_update.Click += btn_update_Click;
            // 
            // lab_yellowText
            // 
            lab_yellowText.AutoSize = true;
            lab_yellowText.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yellowText.Location = new Point(28, 403);
            lab_yellowText.Name = "lab_yellowText";
            lab_yellowText.Size = new Size(164, 17);
            lab_yellowText.TabIndex = 33;
            lab_yellowText.Text = "設定值以下時觸發黃燈警報";
            lab_yellowText.TextAlign = ContentAlignment.TopRight;
            // 
            // lab_redText
            // 
            lab_redText.AutoSize = true;
            lab_redText.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_redText.Location = new Point(28, 468);
            lab_redText.Name = "lab_redText";
            lab_redText.Size = new Size(164, 17);
            lab_redText.TabIndex = 34;
            lab_redText.Text = "設定值以下時觸發紅燈警報";
            lab_redText.TextAlign = ContentAlignment.TopRight;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.8820648F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.1179352F));
            tableLayoutPanel1.Controls.Add(comb_type, 1, 1);
            tableLayoutPanel1.Controls.Add(lab_elementType, 0, 1);
            tableLayoutPanel1.Controls.Add(comb_machine, 1, 0);
            tableLayoutPanel1.Controls.Add(lab_machineType, 0, 0);
            tableLayoutPanel1.Location = new Point(23, 28);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(407, 75);
            tableLayoutPanel1.TabIndex = 35;
            // 
            // comb_type
            // 
            comb_type.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comb_type.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_type.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            comb_type.FormattingEnabled = true;
            comb_type.Items.AddRange(new object[] { "電子式", "機械式" });
            comb_type.Location = new Point(263, 42);
            comb_type.Name = "comb_type";
            comb_type.Size = new Size(141, 28);
            comb_type.TabIndex = 32;
            comb_type.TabStop = false;
            // 
            // lab_elementType
            // 
            lab_elementType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lab_elementType.AutoSize = true;
            lab_elementType.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_elementType.Location = new Point(3, 43);
            lab_elementType.Name = "lab_elementType";
            lab_elementType.Size = new Size(254, 26);
            lab_elementType.TabIndex = 31;
            lab_elementType.Text = "元件類型：";
            // 
            // comb_machine
            // 
            comb_machine.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comb_machine.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_machine.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            comb_machine.FormattingEnabled = true;
            comb_machine.Items.AddRange(new object[] { "鑽床", "鋸床" });
            comb_machine.Location = new Point(263, 4);
            comb_machine.Name = "comb_machine";
            comb_machine.Size = new Size(141, 28);
            comb_machine.TabIndex = 29;
            comb_machine.TabStop = false;
            comb_machine.SelectedIndexChanged += comb_machine_SelectedIndexChanged;
            // 
            // lab_machineType
            // 
            lab_machineType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lab_machineType.AutoSize = true;
            lab_machineType.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_machineType.Location = new Point(3, 5);
            lab_machineType.Name = "lab_machineType";
            lab_machineType.Size = new Size(254, 26);
            lab_machineType.TabIndex = 28;
            lab_machineType.Text = "機台型態：";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.12776F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.8722343F));
            tableLayoutPanel2.Controls.Add(txb_max_number, 1, 3);
            tableLayoutPanel2.Controls.Add(lab_maxlifesetting, 0, 3);
            tableLayoutPanel2.Controls.Add(txb_comment, 1, 2);
            tableLayoutPanel2.Controls.Add(lab_describe, 0, 2);
            tableLayoutPanel2.Controls.Add(txb_description, 1, 1);
            tableLayoutPanel2.Controls.Add(lab_equipment, 0, 1);
            tableLayoutPanel2.Controls.Add(comb_class, 1, 0);
            tableLayoutPanel2.Controls.Add(lab_class, 0, 0);
            tableLayoutPanel2.Location = new Point(23, 156);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Size = new Size(407, 158);
            tableLayoutPanel2.TabIndex = 36;
            // 
            // txb_max_number
            // 
            txb_max_number.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txb_max_number.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            txb_max_number.Location = new Point(265, 123);
            txb_max_number.Margin = new Padding(4);
            txb_max_number.Name = "txb_max_number";
            txb_max_number.Size = new Size(138, 28);
            txb_max_number.TabIndex = 32;
            txb_max_number.Text = "10000";
            txb_max_number.TextAlign = HorizontalAlignment.Center;
            // 
            // lab_maxlifesetting
            // 
            lab_maxlifesetting.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lab_maxlifesetting.AutoSize = true;
            lab_maxlifesetting.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_maxlifesetting.Location = new Point(3, 124);
            lab_maxlifesetting.Name = "lab_maxlifesetting";
            lab_maxlifesetting.Size = new Size(255, 26);
            lab_maxlifesetting.TabIndex = 31;
            lab_maxlifesetting.Text = "當前最大壽命設定(次)：";
            // 
            // txb_comment
            // 
            txb_comment.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txb_comment.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            txb_comment.Location = new Point(264, 83);
            txb_comment.Name = "txb_comment";
            txb_comment.Size = new Size(140, 28);
            txb_comment.TabIndex = 30;
            // 
            // lab_describe
            // 
            lab_describe.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lab_describe.AutoSize = true;
            lab_describe.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_describe.Location = new Point(3, 84);
            lab_describe.Name = "lab_describe";
            lab_describe.Size = new Size(255, 26);
            lab_describe.TabIndex = 29;
            lab_describe.Text = "元件描述：";
            // 
            // txb_description
            // 
            txb_description.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txb_description.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            txb_description.Location = new Point(264, 44);
            txb_description.Name = "txb_description";
            txb_description.Size = new Size(140, 28);
            txb_description.TabIndex = 21;
            // 
            // lab_equipment
            // 
            lab_equipment.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lab_equipment.AutoSize = true;
            lab_equipment.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_equipment.Location = new Point(3, 45);
            lab_equipment.Name = "lab_equipment";
            lab_equipment.Size = new Size(255, 26);
            lab_equipment.TabIndex = 20;
            lab_equipment.Text = "元件料號：";
            // 
            // comb_class
            // 
            comb_class.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comb_class.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_class.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            comb_class.FormattingEnabled = true;
            comb_class.Location = new Point(264, 5);
            comb_class.Name = "comb_class";
            comb_class.Size = new Size(140, 28);
            comb_class.TabIndex = 19;
            // 
            // lab_class
            // 
            lab_class.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lab_class.AutoSize = true;
            lab_class.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_class.Location = new Point(3, 6);
            lab_class.Name = "lab_class";
            lab_class.Size = new Size(255, 26);
            lab_class.TabIndex = 12;
            lab_class.Text = "分類群集：";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.8820648F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.1179352F));
            tableLayoutPanel3.Controls.Add(txb_yellow_light, 1, 0);
            tableLayoutPanel3.Controls.Add(lab_yellow, 0, 0);
            tableLayoutPanel3.Location = new Point(23, 360);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(407, 43);
            tableLayoutPanel3.TabIndex = 37;
            // 
            // txb_yellow_light
            // 
            txb_yellow_light.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txb_yellow_light.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            txb_yellow_light.Location = new Point(264, 7);
            txb_yellow_light.Margin = new Padding(4);
            txb_yellow_light.Name = "txb_yellow_light";
            txb_yellow_light.Size = new Size(139, 28);
            txb_yellow_light.TabIndex = 22;
            txb_yellow_light.TextAlign = HorizontalAlignment.Center;
            txb_yellow_light.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // lab_yellow
            // 
            lab_yellow.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lab_yellow.AutoSize = true;
            lab_yellow.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yellow.Location = new Point(3, 8);
            lab_yellow.Name = "lab_yellow";
            lab_yellow.Size = new Size(254, 26);
            lab_yellow.TabIndex = 14;
            lab_yellow.Text = "黃燈健康狀態設定(%)：";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.8820648F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.1179352F));
            tableLayoutPanel4.Controls.Add(txb_red_light, 1, 0);
            tableLayoutPanel4.Controls.Add(lab_red, 0, 0);
            tableLayoutPanel4.Location = new Point(23, 423);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(407, 42);
            tableLayoutPanel4.TabIndex = 38;
            // 
            // txb_red_light
            // 
            txb_red_light.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txb_red_light.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            txb_red_light.Location = new Point(264, 7);
            txb_red_light.Margin = new Padding(4);
            txb_red_light.Name = "txb_red_light";
            txb_red_light.Size = new Size(139, 28);
            txb_red_light.TabIndex = 23;
            txb_red_light.TextAlign = HorizontalAlignment.Center;
            txb_red_light.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lab_red
            // 
            lab_red.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lab_red.AutoSize = true;
            lab_red.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_red.Location = new Point(3, 8);
            lab_red.Name = "lab_red";
            lab_red.Size = new Size(254, 26);
            lab_red.TabIndex = 15;
            lab_red.Text = "紅燈健康狀態設定(%)：";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 3;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85.6060638F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.393939F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 102F));
            tableLayoutPanel5.Controls.Add(txb_address, 2, 0);
            tableLayoutPanel5.Controls.Add(comb_io, 1, 0);
            tableLayoutPanel5.Controls.Add(lab_elementLocal, 0, 0);
            tableLayoutPanel5.Location = new Point(23, 116);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(407, 34);
            tableLayoutPanel5.TabIndex = 39;
            // 
            // txb_address
            // 
            txb_address.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txb_address.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            txb_address.Location = new Point(307, 3);
            txb_address.Name = "txb_address";
            txb_address.Size = new Size(97, 28);
            txb_address.TabIndex = 25;
            txb_address.KeyPress += txb_address_KeyPress;
            // 
            // comb_io
            // 
            comb_io.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comb_io.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_io.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            comb_io.FormattingEnabled = true;
            comb_io.Items.AddRange(new object[] { "X", "Y" });
            comb_io.Location = new Point(264, 3);
            comb_io.Name = "comb_io";
            comb_io.Size = new Size(37, 28);
            comb_io.TabIndex = 18;
            comb_io.TabStop = false;
            // 
            // lab_elementLocal
            // 
            lab_elementLocal.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lab_elementLocal.AutoSize = true;
            lab_elementLocal.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_elementLocal.Location = new Point(3, 4);
            lab_elementLocal.Name = "lab_elementLocal";
            lab_elementLocal.Size = new Size(255, 26);
            lab_elementLocal.TabIndex = 11;
            lab_elementLocal.Text = "元件位置：";
            // 
            // Element_Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 553);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(lab_yellowText);
            Controls.Add(lab_redText);
            Controls.Add(btn_update);
            Controls.Add(btn_add);
            Controls.Add(lab_green);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel5);
            Name = "Element_Settings";
            StartPosition = FormStartPosition.CenterParent;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txb_yellow_light).EndInit();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txb_red_light).EndInit();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lab_green;
        private Button btn_add;
        private Button btn_update;
        private Label lab_yellowText;
        private Label lab_redText;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox comb_type;
        private Label lab_elementType;
        private ComboBox comb_machine;
        private Label lab_machineType;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox txb_max_number;
        private Label lab_maxlifesetting;
        private TextBox txb_comment;
        private Label lab_describe;
        private TextBox txb_description;
        private Label lab_equipment;
        private ComboBox comb_class;
        private Label lab_class;
        private TableLayoutPanel tableLayoutPanel3;
        private NumericUpDown txb_yellow_light;
        private Label lab_yellow;
        private TableLayoutPanel tableLayoutPanel4;
        private NumericUpDown txb_red_light;
        private Label lab_red;
        private TableLayoutPanel tableLayoutPanel5;
        private TextBox txb_address;
        private ComboBox comb_io;
        private Label lab_elementLocal;
    }
}