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
            lab_maxlifesetting = new Label();
            lab_class = new Label();
            lab_elementLocal = new Label();
            lab_equipment = new Label();
            lab_green = new Label();
            lab_yellow = new Label();
            lab_red = new Label();
            comb_io = new ComboBox();
            comb_class = new ComboBox();
            txb_description = new TextBox();
            txb_yellow_light = new NumericUpDown();
            txb_red_light = new NumericUpDown();
            txb_max_number = new TextBox();
            txb_address = new TextBox();
            btn_add = new Button();
            comb_machine = new ComboBox();
            lab_machineType = new Label();
            lab_describe = new Label();
            txb_comment = new TextBox();
            comb_type = new ComboBox();
            lab_elementType = new Label();
            btn_update = new Button();
            lab_yellowText = new Label();
            lab_redText = new Label();
            ((System.ComponentModel.ISupportInitialize)txb_yellow_light).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txb_red_light).BeginInit();
            SuspendLayout();
            // 
            // lab_maxlifesetting
            // 
            lab_maxlifesetting.AutoSize = true;
            lab_maxlifesetting.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_maxlifesetting.Location = new Point(23, 286);
            lab_maxlifesetting.Name = "lab_maxlifesetting";
            lab_maxlifesetting.Size = new Size(236, 26);
            lab_maxlifesetting.TabIndex = 12;
            lab_maxlifesetting.Text = "當前最大壽命設定(次)：";
            // 
            // lab_class
            // 
            lab_class.AutoSize = true;
            lab_class.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_class.Location = new Point(23, 156);
            lab_class.Name = "lab_class";
            lab_class.Size = new Size(117, 26);
            lab_class.TabIndex = 11;
            lab_class.Text = "分類群集：";
            // 
            // lab_elementLocal
            // 
            lab_elementLocal.AutoSize = true;
            lab_elementLocal.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_elementLocal.Location = new Point(23, 111);
            lab_elementLocal.Name = "lab_elementLocal";
            lab_elementLocal.Size = new Size(117, 26);
            lab_elementLocal.TabIndex = 10;
            lab_elementLocal.Text = "元件位置：";
            // 
            // lab_equipment
            // 
            lab_equipment.AutoSize = true;
            lab_equipment.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_equipment.Location = new Point(23, 200);
            lab_equipment.Name = "lab_equipment";
            lab_equipment.Size = new Size(117, 26);
            lab_equipment.TabIndex = 9;
            lab_equipment.Text = "元件料號：";
            // 
            // lab_green
            // 
            lab_green.AutoSize = true;
            lab_green.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_green.Location = new Point(23, 331);
            lab_green.Name = "lab_green";
            lab_green.Size = new Size(306, 26);
            lab_green.TabIndex = 8;
            lab_green.Text = "綠燈健康狀態為黃燈設定值以上";
            // 
            // lab_yellow
            // 
            lab_yellow.AutoSize = true;
            lab_yellow.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yellow.Location = new Point(23, 372);
            lab_yellow.Name = "lab_yellow";
            lab_yellow.Size = new Size(234, 26);
            lab_yellow.TabIndex = 13;
            lab_yellow.Text = "黃燈健康狀態設定(%)：";
            // 
            // lab_red
            // 
            lab_red.AutoSize = true;
            lab_red.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_red.Location = new Point(23, 425);
            lab_red.Name = "lab_red";
            lab_red.Size = new Size(234, 26);
            lab_red.TabIndex = 14;
            lab_red.Text = "紅燈健康狀態設定(%)：";
            // 
            // comb_io
            // 
            comb_io.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_io.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            comb_io.FormattingEnabled = true;
            comb_io.Items.AddRange(new object[] { "X", "Y" });
            comb_io.Location = new Point(146, 109);
            comb_io.Name = "comb_io";
            comb_io.Size = new Size(46, 32);
            comb_io.TabIndex = 17;
            comb_io.TabStop = false;
            // 
            // comb_class
            // 
            comb_class.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_class.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            comb_class.FormattingEnabled = true;
            comb_class.Location = new Point(146, 154);
            comb_class.Name = "comb_class";
            comb_class.Size = new Size(284, 32);
            comb_class.TabIndex = 18;
            // 
            // txb_description
            // 
            txb_description.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txb_description.Location = new Point(146, 194);
            txb_description.Name = "txb_description";
            txb_description.Size = new Size(284, 32);
            txb_description.TabIndex = 19;
            // 
            // txb_yellow_light
            // 
            txb_yellow_light.Font = new Font("微軟正黑體", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txb_yellow_light.Location = new Point(254, 372);
            txb_yellow_light.Margin = new Padding(4);
            txb_yellow_light.Name = "txb_yellow_light";
            txb_yellow_light.Size = new Size(176, 33);
            txb_yellow_light.TabIndex = 21;
            txb_yellow_light.TextAlign = HorizontalAlignment.Center;
            txb_yellow_light.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // txb_red_light
            // 
            txb_red_light.Font = new Font("微軟正黑體", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txb_red_light.Location = new Point(254, 423);
            txb_red_light.Margin = new Padding(4);
            txb_red_light.Name = "txb_red_light";
            txb_red_light.Size = new Size(176, 33);
            txb_red_light.TabIndex = 22;
            txb_red_light.TextAlign = HorizontalAlignment.Center;
            txb_red_light.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // txb_max_number
            // 
            txb_max_number.Font = new Font("微軟正黑體", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txb_max_number.Location = new Point(254, 286);
            txb_max_number.Margin = new Padding(4);
            txb_max_number.Name = "txb_max_number";
            txb_max_number.Size = new Size(176, 33);
            txb_max_number.TabIndex = 23;
            txb_max_number.Text = "10000";
            txb_max_number.TextAlign = HorizontalAlignment.Center;
            txb_max_number.KeyPress += txb_max_number_KeyPress;
            // 
            // txb_address
            // 
            txb_address.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txb_address.Location = new Point(198, 109);
            txb_address.Name = "txb_address";
            txb_address.Size = new Size(232, 32);
            txb_address.TabIndex = 24;
            txb_address.KeyPress += txb_address_KeyPress;
            // 
            // btn_add
            // 
            btn_add.Location = new Point(369, 468);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(61, 36);
            btn_add.TabIndex = 25;
            btn_add.Text = "新增";
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Click += btn_add_Click;
            // 
            // comb_machine
            // 
            comb_machine.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_machine.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            comb_machine.FormattingEnabled = true;
            comb_machine.Items.AddRange(new object[] { "鑽床", "鋸床" });
            comb_machine.Location = new Point(146, 28);
            comb_machine.Name = "comb_machine";
            comb_machine.Size = new Size(284, 32);
            comb_machine.TabIndex = 26;
            comb_machine.TabStop = false;
            comb_machine.SelectedIndexChanged += comb_machine_SelectedIndexChanged;
            // 
            // lab_machineType
            // 
            lab_machineType.AutoSize = true;
            lab_machineType.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_machineType.Location = new Point(23, 29);
            lab_machineType.Name = "lab_machineType";
            lab_machineType.Size = new Size(117, 26);
            lab_machineType.TabIndex = 27;
            lab_machineType.Text = "機台型態：";
            // 
            // lab_describe
            // 
            lab_describe.AutoSize = true;
            lab_describe.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_describe.Location = new Point(25, 243);
            lab_describe.Name = "lab_describe";
            lab_describe.Size = new Size(117, 26);
            lab_describe.TabIndex = 28;
            lab_describe.Text = "元件描述：";
            // 
            // txb_comment
            // 
            txb_comment.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txb_comment.Location = new Point(146, 241);
            txb_comment.Name = "txb_comment";
            txb_comment.Size = new Size(284, 32);
            txb_comment.TabIndex = 29;
            // 
            // comb_type
            // 
            comb_type.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_type.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            comb_type.FormattingEnabled = true;
            comb_type.Items.AddRange(new object[] { "電子式", "機械式" });
            comb_type.Location = new Point(148, 68);
            comb_type.Name = "comb_type";
            comb_type.Size = new Size(282, 32);
            comb_type.TabIndex = 31;
            comb_type.TabStop = false;
            // 
            // lab_elementType
            // 
            lab_elementType.AutoSize = true;
            lab_elementType.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_elementType.Location = new Point(23, 69);
            lab_elementType.Name = "lab_elementType";
            lab_elementType.Size = new Size(117, 26);
            lab_elementType.TabIndex = 30;
            lab_elementType.Text = "元件類型：";
            // 
            // btn_update
            // 
            btn_update.Location = new Point(302, 468);
            btn_update.Name = "btn_update";
            btn_update.Size = new Size(61, 36);
            btn_update.TabIndex = 32;
            btn_update.Text = "更新";
            btn_update.UseVisualStyleBackColor = true;
            btn_update.Click += btn_update_Click;
            // 
            // lab_yellowText
            // 
            lab_yellowText.AutoSize = true;
            lab_yellowText.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_yellowText.Location = new Point(73, 398);
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
            lab_redText.Location = new Point(73, 451);
            lab_redText.Name = "lab_redText";
            lab_redText.Size = new Size(164, 17);
            lab_redText.TabIndex = 34;
            lab_redText.Text = "設定值以下時觸發紅燈警報";
            lab_redText.TextAlign = ContentAlignment.TopRight;
            // 
            // Element_Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 512);
            Controls.Add(lab_redText);
            Controls.Add(lab_yellowText);
            Controls.Add(btn_update);
            Controls.Add(comb_type);
            Controls.Add(lab_elementType);
            Controls.Add(txb_comment);
            Controls.Add(lab_describe);
            Controls.Add(lab_machineType);
            Controls.Add(comb_machine);
            Controls.Add(btn_add);
            Controls.Add(txb_address);
            Controls.Add(txb_max_number);
            Controls.Add(txb_red_light);
            Controls.Add(txb_yellow_light);
            Controls.Add(txb_description);
            Controls.Add(comb_class);
            Controls.Add(comb_io);
            Controls.Add(lab_red);
            Controls.Add(lab_yellow);
            Controls.Add(lab_maxlifesetting);
            Controls.Add(lab_class);
            Controls.Add(lab_elementLocal);
            Controls.Add(lab_equipment);
            Controls.Add(lab_green);
            Name = "Element_Settings";
            StartPosition = FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)txb_yellow_light).EndInit();
            ((System.ComponentModel.ISupportInitialize)txb_red_light).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lab_maxlifesetting;
        private Label lab_class;
        private Label lab_elementLocal;
        private Label lab_equipment;
        private Label lab_green;
        private Label lab_yellow;
        private Label lab_red;
        private ComboBox comb_io;
        private ComboBox comb_class;
        private TextBox txb_description;
        private NumericUpDown txb_yellow_light;
        private NumericUpDown txb_red_light;
        private TextBox txb_max_number;
        private TextBox txb_address;
        private Button btn_add;
        private ComboBox comb_machine;
        private Label lab_machineType;
        private Label lab_describe;
        private TextBox txb_comment;
        private ComboBox comb_type;
        private Label lab_elementType;
        private Button btn_update;
        private Label lab_yellowText;
        private Label lab_redText;
    }
}