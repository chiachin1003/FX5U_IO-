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
            label2 = new Label();
            label1 = new Label();
            lab_Possible = new Label();
            lab_Error = new Label();
            lab_Repair_step = new Label();
            label3 = new Label();
            label4 = new Label();
            comb_io = new ComboBox();
            comb_class = new ComboBox();
            txb_description = new TextBox();
            txb_yellow_light = new NumericUpDown();
            txb_red_light = new NumericUpDown();
            txb_max_number = new TextBox();
            txb_address = new TextBox();
            btn_add = new Button();
            comb_machine = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            txb_comment = new TextBox();
            comb_type = new ComboBox();
            label7 = new Label();
            btn_update = new Button();
            label8 = new Label();
            label9 = new Label();
            ((System.ComponentModel.ISupportInitialize)txb_yellow_light).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txb_red_light).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label2.Location = new Point(23, 286);
            label2.Name = "label2";
            label2.Size = new Size(236, 26);
            label2.TabIndex = 12;
            label2.Text = "當前最大壽命設定(次)：";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(23, 156);
            label1.Name = "label1";
            label1.Size = new Size(117, 26);
            label1.TabIndex = 11;
            label1.Text = "分類群集：";
            // 
            // lab_Possible
            // 
            lab_Possible.AutoSize = true;
            lab_Possible.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Possible.Location = new Point(23, 111);
            lab_Possible.Name = "lab_Possible";
            lab_Possible.Size = new Size(117, 26);
            lab_Possible.TabIndex = 10;
            lab_Possible.Text = "元件位置：";
            // 
            // lab_Error
            // 
            lab_Error.AutoSize = true;
            lab_Error.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Error.Location = new Point(23, 200);
            lab_Error.Name = "lab_Error";
            lab_Error.Size = new Size(117, 26);
            lab_Error.TabIndex = 9;
            lab_Error.Text = "元件料號：";
            // 
            // lab_Repair_step
            // 
            lab_Repair_step.AutoSize = true;
            lab_Repair_step.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Repair_step.Location = new Point(23, 331);
            lab_Repair_step.Name = "lab_Repair_step";
            lab_Repair_step.Size = new Size(306, 26);
            lab_Repair_step.TabIndex = 8;
            lab_Repair_step.Text = "綠燈健康狀態為黃燈設定值以上";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label3.Location = new Point(23, 372);
            label3.Name = "label3";
            label3.Size = new Size(234, 26);
            label3.TabIndex = 13;
            label3.Text = "黃燈健康狀態設定(%)：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label4.Location = new Point(23, 425);
            label4.Name = "label4";
            label4.Size = new Size(234, 26);
            label4.TabIndex = 14;
            label4.Text = "紅燈健康狀態設定(%)：";
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
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label5.Location = new Point(23, 29);
            label5.Name = "label5";
            label5.Size = new Size(117, 26);
            label5.TabIndex = 27;
            label5.Text = "機台型態：";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label6.Location = new Point(25, 243);
            label6.Name = "label6";
            label6.Size = new Size(117, 26);
            label6.TabIndex = 28;
            label6.Text = "元件描述：";
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
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label7.Location = new Point(23, 69);
            label7.Name = "label7";
            label7.Size = new Size(117, 26);
            label7.TabIndex = 30;
            label7.Text = "元件類型：";
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
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label8.Location = new Point(73, 398);
            label8.Name = "label8";
            label8.Size = new Size(164, 17);
            label8.TabIndex = 33;
            label8.Text = "設定值以下時觸發黃燈警報";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label9.Location = new Point(73, 451);
            label9.Name = "label9";
            label9.Size = new Size(164, 17);
            label9.TabIndex = 34;
            label9.Text = "設定值以下時觸發紅燈警報";
            // 
            // Element_Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 512);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(btn_update);
            Controls.Add(comb_type);
            Controls.Add(label7);
            Controls.Add(txb_comment);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(comb_machine);
            Controls.Add(btn_add);
            Controls.Add(txb_address);
            Controls.Add(txb_max_number);
            Controls.Add(txb_red_light);
            Controls.Add(txb_yellow_light);
            Controls.Add(txb_description);
            Controls.Add(comb_class);
            Controls.Add(comb_io);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lab_Possible);
            Controls.Add(lab_Error);
            Controls.Add(lab_Repair_step);
            Name = "Element_Settings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "新增監控元件";
            ((System.ComponentModel.ISupportInitialize)txb_yellow_light).EndInit();
            ((System.ComponentModel.ISupportInitialize)txb_red_light).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private Label label1;
        private Label lab_Possible;
        private Label lab_Error;
        private Label lab_Repair_step;
        private Label label3;
        private Label label4;
        private ComboBox comb_io;
        private ComboBox comb_class;
        private TextBox txb_description;
        private NumericUpDown txb_yellow_light;
        private NumericUpDown txb_red_light;
        private TextBox txb_max_number;
        private TextBox txb_address;
        private Button btn_add;
        private ComboBox comb_machine;
        private Label label5;
        private Label label6;
        private TextBox txb_comment;
        private ComboBox comb_type;
        private Label label7;
        private Button btn_update;
        private Label label8;
        private Label label9;
    }
}