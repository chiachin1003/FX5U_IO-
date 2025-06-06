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
            checkcombobox_special = new CheckedComboBoxDemo.checkcombobox();
            panel3 = new Panel();
            lab_alarm_notify = new Label();
            btn_update = new Button();
            radioButton_DesignatedUser = new RadioButton();
            radioButton_special = new RadioButton();
            panel2 = new Panel();
            control_choose = new ComboBox();
            btn_apply = new Button();
            lab_machine = new Label();
            panel4 = new Panel();
            treeView1 = new TreeView();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // radioButton_alluser
            // 
            radioButton_alluser.AutoSize = true;
            radioButton_alluser.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            radioButton_alluser.Location = new Point(18, 49);
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
            panel1.Controls.Add(checkcombobox_special);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(btn_update);
            panel1.Controls.Add(radioButton_DesignatedUser);
            panel1.Controls.Add(radioButton_special);
            panel1.Controls.Add(radioButton_alluser);
            panel1.Location = new Point(26, 53);
            panel1.Name = "panel1";
            panel1.Size = new Size(482, 192);
            panel1.TabIndex = 1;
            // 
            // checkcombobox_special
            // 
            checkcombobox_special.Location = new Point(18, 119);
            checkcombobox_special.Name = "checkcombobox_special";
            checkcombobox_special.Size = new Size(358, 23);
            checkcombobox_special.TabIndex = 16;
            checkcombobox_special.Visible = false;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ScrollBar;
            panel3.Controls.Add(lab_alarm_notify);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(480, 37);
            panel3.TabIndex = 5;
            // 
            // lab_alarm_notify
            // 
            lab_alarm_notify.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lab_alarm_notify.AutoSize = true;
            lab_alarm_notify.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_alarm_notify.Location = new Point(18, 9);
            lab_alarm_notify.Name = "lab_alarm_notify";
            lab_alarm_notify.Size = new Size(99, 19);
            lab_alarm_notify.TabIndex = 0;
            lab_alarm_notify.Text = "通知傳送方式";
            // 
            // btn_update
            // 
            btn_update.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Bold);
            btn_update.Location = new Point(397, 109);
            btn_update.Name = "btn_update";
            btn_update.Size = new Size(69, 33);
            btn_update.TabIndex = 3;
            btn_update.Text = "更新";
            btn_update.UseVisualStyleBackColor = true;
            btn_update.Click += btn_update_Click;
            // 
            // radioButton_DesignatedUser
            // 
            radioButton_DesignatedUser.AutoSize = true;
            radioButton_DesignatedUser.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            radioButton_DesignatedUser.Location = new Point(18, 152);
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
            radioButton_special.Location = new Point(18, 89);
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
            panel2.Controls.Add(btn_apply);
            panel2.Controls.Add(lab_machine);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(578, 625);
            panel2.TabIndex = 3;
            // 
            // control_choose
            // 
            control_choose.DropDownStyle = ComboBoxStyle.DropDownList;
            control_choose.Font = new Font("微軟正黑體", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            control_choose.FormattingEnabled = true;
            control_choose.Location = new Point(137, 11);
            control_choose.Name = "control_choose";
            control_choose.Size = new Size(307, 32);
            control_choose.TabIndex = 15;
            control_choose.SelectedIndexChanged += control_choose_SelectedIndexChanged;
            // 
            // btn_apply
            // 
            btn_apply.Location = new Point(450, 10);
            btn_apply.Name = "btn_apply";
            btn_apply.Size = new Size(57, 37);
            btn_apply.TabIndex = 4;
            btn_apply.Text = "應用";
            btn_apply.UseVisualStyleBackColor = true;
            // 
            // lab_machine
            // 
            lab_machine.AutoSize = true;
            lab_machine.Font = new Font("微軟正黑體", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_machine.Location = new Point(26, 14);
            lab_machine.Name = "lab_machine";
            lab_machine.Size = new Size(105, 24);
            lab_machine.TabIndex = 14;
            lab_machine.Text = "選擇機台：";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(treeView1);
            panel4.Location = new Point(26, 263);
            panel4.Name = "panel4";
            panel4.Size = new Size(481, 348);
            panel4.TabIndex = 2;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(479, 346);
            treeView1.TabIndex = 0;
            treeView1.BeforeCheck += treeView1_BeforeCheck;
            treeView1.AfterCheck += treeView1_AfterCheck;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // 
            // Alarm_Notify
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 625);
            Controls.Add(panel2);
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Alarm_Notify";
            StartPosition = FormStartPosition.CenterParent;
            Text = "警告通知設定";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private RadioButton radioButton_alluser;
        private Panel panel1;
        private Button btn_update;
        private RadioButton radioButton_DesignatedUser;
        private RadioButton radioButton_special;
        private Panel panel2;
        private Panel panel3;
        private Label lab_alarm_notify;
        private Panel panel4;
        private TreeView treeView1;
        private Button btn_apply;
        private ComboBox control_choose;
        private Label lab_machine;
        private CheckedComboBoxDemo.checkcombobox checkcombobox_special;
    }
}