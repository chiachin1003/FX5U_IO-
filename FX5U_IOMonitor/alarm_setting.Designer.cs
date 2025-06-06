namespace FX5U_IOMonitor
{
    partial class alarm_setting
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
            panel1 = new Panel();
            lab_Description = new Label();
            btn_update = new Button();
            label4 = new Label();
            lab_class = new Label();
            txB_Possible = new TextBox();
            label2 = new Label();
            txB_Error = new TextBox();
            label1 = new Label();
            lab_Possible = new Label();
            lab_Error = new Label();
            txB_Step = new TextBox();
            lab_Repair_step = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lab_Description);
            panel1.Controls.Add(btn_update);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(lab_class);
            panel1.Controls.Add(txB_Possible);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txB_Error);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lab_Possible);
            panel1.Controls.Add(lab_Error);
            panel1.Controls.Add(txB_Step);
            panel1.Controls.Add(lab_Repair_step);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(463, 580);
            panel1.TabIndex = 0;
            // 
            // lab_Description
            // 
            lab_Description.AutoSize = true;
            lab_Description.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Description.Location = new Point(128, 18);
            lab_Description.Name = "lab_Description";
            lab_Description.Size = new Size(77, 26);
            lab_Description.TabIndex = 11;
            lab_Description.Text = "             ";
            // 
            // btn_update
            // 
            btn_update.Anchor = AnchorStyles.None;
            btn_update.Location = new Point(377, 529);
            btn_update.Name = "btn_update";
            btn_update.Size = new Size(61, 39);
            btn_update.TabIndex = 2;
            btn_update.Text = "更新";
            btn_update.UseVisualStyleBackColor = true;
            btn_update.Click += btn_update_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label4.Location = new Point(128, 18);
            label4.Name = "label4";
            label4.Size = new Size(77, 26);
            label4.TabIndex = 10;
            label4.Text = "             ";
            // 
            // lab_class
            // 
            lab_class.AutoSize = true;
            lab_class.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_class.Location = new Point(128, 61);
            lab_class.Name = "lab_class";
            lab_class.Size = new Size(77, 26);
            lab_class.TabIndex = 9;
            lab_class.Text = "             ";
            // 
            // txB_Possible
            // 
            txB_Possible.AcceptsReturn = true;
            txB_Possible.AcceptsTab = true;
            txB_Possible.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txB_Possible.Location = new Point(128, 162);
            txB_Possible.MaxLength = 30;
            txB_Possible.Multiline = true;
            txB_Possible.Name = "txB_Possible";
            txB_Possible.Size = new Size(310, 77);
            txB_Possible.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label2.Location = new Point(19, 162);
            label2.Name = "label2";
            label2.Size = new Size(117, 26);
            label2.TabIndex = 7;
            label2.Text = "可能原因：";
            // 
            // txB_Error
            // 
            txB_Error.AcceptsReturn = true;
            txB_Error.AcceptsTab = true;
            txB_Error.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txB_Error.Location = new Point(136, 99);
            txB_Error.MaxLength = 20;
            txB_Error.Name = "txB_Error";
            txB_Error.Size = new Size(302, 34);
            txB_Error.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(19, 99);
            label1.Name = "label1";
            label1.Size = new Size(117, 26);
            label1.TabIndex = 5;
            label1.Text = "故障內容：";
            // 
            // lab_Possible
            // 
            lab_Possible.AutoSize = true;
            lab_Possible.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Possible.Location = new Point(19, 61);
            lab_Possible.Name = "lab_Possible";
            lab_Possible.Size = new Size(117, 26);
            lab_Possible.TabIndex = 4;
            lab_Possible.Text = "元件位置：";
            // 
            // lab_Error
            // 
            lab_Error.AutoSize = true;
            lab_Error.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Error.Location = new Point(19, 18);
            lab_Error.Name = "lab_Error";
            lab_Error.Size = new Size(117, 26);
            lab_Error.TabIndex = 3;
            lab_Error.Text = "元件料號：";
            // 
            // txB_Step
            // 
            txB_Step.AcceptsReturn = true;
            txB_Step.AcceptsTab = true;
            txB_Step.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txB_Step.Location = new Point(19, 286);
            txB_Step.Multiline = true;
            txB_Step.Name = "txB_Step";
            txB_Step.ScrollBars = ScrollBars.Vertical;
            txB_Step.Size = new Size(419, 235);
            txB_Step.TabIndex = 1;
            // 
            // lab_Repair_step
            // 
            lab_Repair_step.AutoSize = true;
            lab_Repair_step.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Repair_step.Location = new Point(19, 257);
            lab_Repair_step.Name = "lab_Repair_step";
            lab_Repair_step.Size = new Size(159, 26);
            lab_Repair_step.TabIndex = 0;
            lab_Repair_step.Text = "故障維護步驟：";
            // 
            // alarm_setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(463, 580);
            Controls.Add(panel1);
            Name = "alarm_setting";
            StartPosition = FormStartPosition.CenterParent;
            Text = "警告維護";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btn_update;
        private TextBox txB_Step;
        private Label lab_Repair_step;
        private Label lab_Possible;
        private Label lab_Error;
        private TextBox txB_Possible;
        private Label label2;
        private TextBox txB_Error;
        private Label label1;
        private Label label4;
        private Label lab_class;
        private Label lab_Description;
    }
}