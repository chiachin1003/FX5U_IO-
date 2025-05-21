using System.Windows.Forms;

namespace FX5U_IO元件監控
{
    partial class Alarm_Notify
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
            radioButton_alluser = new RadioButton();
            panel1 = new Panel();
            panel3 = new Panel();
            label1 = new Label();
            btn_reset = new Button();
            btn_update = new Button();
            radioButton_DesignatedUser = new RadioButton();
            radioButton_special = new RadioButton();
            panel2 = new Panel();
            control_choose = new ComboBox();
            button4 = new Button();
            label2 = new Label();
            panel4 = new Panel();
            treeView1 = new TreeView();
            panel5 = new Panel();
            dataGridView1 = new DataGridView();
            button3 = new Button();
            btn_RemoveSelected = new Button();
            btn_AddRecipient = new Button();
            txb_NewRecipient = new TextBox();
            btn_add = new Button();
            lab_Error = new Label();
            button5 = new Button();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // radioButton_alluser
            // 
            radioButton_alluser.AutoSize = true;
            radioButton_alluser.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            radioButton_alluser.Location = new Point(18, 52);
            radioButton_alluser.Name = "radioButton_alluser";
            radioButton_alluser.Size = new Size(171, 24);
            radioButton_alluser.TabIndex = 0;
            radioButton_alluser.TabStop = true;
            radioButton_alluser.Text = "所有使用者接收訊息";
            radioButton_alluser.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(btn_reset);
            panel1.Controls.Add(btn_update);
            panel1.Controls.Add(radioButton_DesignatedUser);
            panel1.Controls.Add(radioButton_special);
            panel1.Controls.Add(radioButton_alluser);
            panel1.Location = new Point(26, 53);
            panel1.Name = "panel1";
            panel1.Size = new Size(482, 158);
            panel1.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ScrollBar;
            panel3.Controls.Add(label1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(480, 37);
            panel3.TabIndex = 5;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(18, 9);
            label1.Name = "label1";
            label1.Size = new Size(99, 19);
            label1.TabIndex = 0;
            label1.Text = "通知傳送方式";
            // 
            // btn_reset
            // 
            btn_reset.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold);
            btn_reset.Location = new Point(394, 108);
            btn_reset.Name = "btn_reset";
            btn_reset.Size = new Size(75, 36);
            btn_reset.TabIndex = 4;
            btn_reset.Text = "重置";
            btn_reset.UseVisualStyleBackColor = true;
            // 
            // btn_update
            // 
            btn_update.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold);
            btn_update.Location = new Point(394, 52);
            btn_update.Name = "btn_update";
            btn_update.Size = new Size(75, 36);
            btn_update.TabIndex = 3;
            btn_update.Text = "更新";
            btn_update.UseVisualStyleBackColor = true;
            btn_update.Click += btn_update_Click;
            // 
            // radioButton_DesignatedUser
            // 
            radioButton_DesignatedUser.AutoSize = true;
            radioButton_DesignatedUser.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            radioButton_DesignatedUser.Location = new Point(18, 120);
            radioButton_DesignatedUser.Name = "radioButton_DesignatedUser";
            radioButton_DesignatedUser.Size = new Size(251, 24);
            radioButton_DesignatedUser.TabIndex = 2;
            radioButton_DesignatedUser.TabStop = true;
            radioButton_DesignatedUser.Text = "每種訊息個別發送至指定使用者";
            radioButton_DesignatedUser.UseVisualStyleBackColor = true;
            // 
            // radioButton_special
            // 
            radioButton_special.AutoSize = true;
            radioButton_special.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            radioButton_special.Location = new Point(18, 86);
            radioButton_special.Name = "radioButton_special";
            radioButton_special.Size = new Size(171, 24);
            radioButton_special.TabIndex = 1;
            radioButton_special.TabStop = true;
            radioButton_special.Text = "特定使用者接受訊息";
            radioButton_special.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(control_choose);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel1);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(535, 625);
            panel2.TabIndex = 3;
            // 
            // control_choose
            // 
            control_choose.DropDownStyle = ComboBoxStyle.DropDownList;
            control_choose.Font = new Font("微軟正黑體", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            control_choose.FormattingEnabled = true;
            control_choose.Location = new Point(124, 10);
            control_choose.Name = "control_choose";
            control_choose.Size = new Size(316, 32);
            control_choose.TabIndex = 15;
            control_choose.SelectedIndexChanged += control_choose_SelectedIndexChanged;
            // 
            // button4
            // 
            button4.Location = new Point(450, 10);
            button4.Name = "button4";
            button4.Size = new Size(57, 37);
            button4.TabIndex = 4;
            button4.Text = "應用";
            button4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("微軟正黑體", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label2.Location = new Point(26, 14);
            label2.Name = "label2";
            label2.Size = new Size(105, 24);
            label2.TabIndex = 14;
            label2.Text = "機台選擇：";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(treeView1);
            panel4.Location = new Point(26, 217);
            panel4.Name = "panel4";
            panel4.Size = new Size(481, 394);
            panel4.TabIndex = 2;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(479, 392);
            treeView1.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.Controls.Add(dataGridView1);
            panel5.Controls.Add(button3);
            panel5.Controls.Add(btn_RemoveSelected);
            panel5.Controls.Add(btn_AddRecipient);
            panel5.Controls.Add(txb_NewRecipient);
            panel5.Controls.Add(btn_add);
            panel5.Controls.Add(lab_Error);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(535, 263);
            panel5.Name = "panel5";
            panel5.Size = new Size(385, 362);
            panel5.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(18, 147);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(345, 150);
            dataGridView1.TabIndex = 55;
            // 
            // button3
            // 
            button3.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold);
            button3.Location = new Point(235, 314);
            button3.Name = "button3";
            button3.Size = new Size(128, 36);
            button3.TabIndex = 54;
            button3.Text = "信件發送";
            button3.UseVisualStyleBackColor = true;
            // 
            // btn_RemoveSelected
            // 
            btn_RemoveSelected.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold);
            btn_RemoveSelected.Location = new Point(97, 314);
            btn_RemoveSelected.Name = "btn_RemoveSelected";
            btn_RemoveSelected.Size = new Size(128, 36);
            btn_RemoveSelected.TabIndex = 53;
            btn_RemoveSelected.Text = "移除選中";
            btn_RemoveSelected.UseVisualStyleBackColor = true;
            btn_RemoveSelected.Click += btn_RemoveSelected_Click;
            // 
            // btn_AddRecipient
            // 
            btn_AddRecipient.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold);
            btn_AddRecipient.Location = new Point(235, 97);
            btn_AddRecipient.Name = "btn_AddRecipient";
            btn_AddRecipient.Size = new Size(128, 36);
            btn_AddRecipient.TabIndex = 52;
            btn_AddRecipient.Text = "新增收件人";
            btn_AddRecipient.UseVisualStyleBackColor = true;
            btn_AddRecipient.Click += btn_AddRecipient_Click;
            // 
            // txb_NewRecipient
            // 
            txb_NewRecipient.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            txb_NewRecipient.Location = new Point(97, 49);
            txb_NewRecipient.Name = "txb_NewRecipient";
            txb_NewRecipient.Size = new Size(266, 32);
            txb_NewRecipient.TabIndex = 51;
            // 
            // btn_add
            // 
            btn_add.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold);
            btn_add.Location = new Point(101, 97);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(128, 36);
            btn_add.TabIndex = 50;
            btn_add.Text = "選擇收件人";
            btn_add.UseVisualStyleBackColor = true;
            // 
            // lab_Error
            // 
            lab_Error.AutoSize = true;
            lab_Error.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold);
            lab_Error.Location = new Point(18, 60);
            lab_Error.Name = "lab_Error";
            lab_Error.Size = new Size(86, 17);
            lab_Error.TabIndex = 47;
            lab_Error.Text = "新增收件人：";
            // 
            // button5
            // 
            button5.Location = new Point(780, 217);
            button5.Name = "button5";
            button5.Size = new Size(128, 37);
            button5.TabIndex = 5;
            button5.Text = "寄件人資料維護";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // Alarm_Notify
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(920, 625);
            Controls.Add(button5);
            Controls.Add(panel5);
            Controls.Add(panel2);
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Alarm_Notify";
            StartPosition = FormStartPosition.CenterParent;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RadioButton radioButton_alluser;
        private Panel panel1;
        private Button btn_reset;
        private Button btn_update;
        private RadioButton radioButton_DesignatedUser;
        private RadioButton radioButton_special;
        private Panel panel2;
        private Panel panel3;
        private Label label1;
        private Panel panel4;
        private TreeView treeView1;
        private Button button4;
        private ComboBox control_choose;
        private Label label2;
        private Panel panel5;
        private DataGridView dataGridView1;
        private Button button3;
        private Button btn_RemoveSelected;
        private Button btn_AddRecipient;
        private TextBox txb_NewRecipient;
        private Button btn_add;
        private Label lab_Error;
        private Button button5;
    }
}