namespace FX5U_IOMonitor
{
    partial class Sawingband_Info
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
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
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            lab_Sawband_tension = new Label();
            lab_Sawband_tensionText = new Label();
            lab_saw_life = new Label();
            lab_saw_lifeText = new Label();
            lab_Sawband_area = new Label();
            lab_Sawband_areaText = new Label();
            lab_Sawband_current = new Label();
            lab_Sawband_currentText = new Label();
            lab_Sawband_power = new Label();
            lab_Sawband_powerText = new Label();
            lab_Sawband_motors_usetime = new Label();
            lab_Sawband_motors_usetimeText = new Label();
            lab_Sawband_speed = new Label();
            lab_Sawband_speedText = new Label();
            lab_Sawblade_teeth = new Label();
            lab_Sawblade_teethText = new Label();
            lab_Sawblade_type = new Label();
            lab_Sawblade_typeText = new Label();
            lab_Sawblade_material = new Label();
            lab_Sawblade_materialText = new Label();
            lab_Sawband_brand = new Label();
            lab_Sawband_brandText = new Label();
            lab_time = new Label();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Location = new Point(33, 38);
            panel1.Name = "panel1";
            panel1.Size = new Size(655, 549);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.79389F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.20611F));
            tableLayoutPanel1.Controls.Add(lab_Sawband_tension, 1, 10);
            tableLayoutPanel1.Controls.Add(lab_Sawband_tensionText, 0, 10);
            tableLayoutPanel1.Controls.Add(lab_saw_life, 1, 9);
            tableLayoutPanel1.Controls.Add(lab_saw_lifeText, 0, 9);
            tableLayoutPanel1.Controls.Add(lab_Sawband_area, 1, 8);
            tableLayoutPanel1.Controls.Add(lab_Sawband_areaText, 0, 8);
            tableLayoutPanel1.Controls.Add(lab_Sawband_current, 1, 7);
            tableLayoutPanel1.Controls.Add(lab_Sawband_currentText, 0, 7);
            tableLayoutPanel1.Controls.Add(lab_Sawband_power, 1, 6);
            tableLayoutPanel1.Controls.Add(lab_Sawband_powerText, 0, 6);
            tableLayoutPanel1.Controls.Add(lab_Sawband_motors_usetime, 1, 5);
            tableLayoutPanel1.Controls.Add(lab_Sawband_motors_usetimeText, 0, 5);
            tableLayoutPanel1.Controls.Add(lab_Sawband_speed, 1, 4);
            tableLayoutPanel1.Controls.Add(lab_Sawband_speedText, 0, 4);
            tableLayoutPanel1.Controls.Add(lab_Sawblade_teeth, 1, 3);
            tableLayoutPanel1.Controls.Add(lab_Sawblade_teethText, 0, 3);
            tableLayoutPanel1.Controls.Add(lab_Sawblade_type, 1, 2);
            tableLayoutPanel1.Controls.Add(lab_Sawblade_typeText, 0, 2);
            tableLayoutPanel1.Controls.Add(lab_Sawblade_material, 1, 1);
            tableLayoutPanel1.Controls.Add(lab_Sawblade_materialText, 0, 1);
            tableLayoutPanel1.Controls.Add(lab_Sawband_brand, 1, 0);
            tableLayoutPanel1.Controls.Add(lab_Sawband_brandText, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 11;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(655, 549);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // lab_Sawband_tension
            // 
            lab_Sawband_tension.BackColor = Color.Transparent;
            lab_Sawband_tension.Dock = DockStyle.Fill;
            lab_Sawband_tension.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Sawband_tension.Location = new Point(244, 495);
            lab_Sawband_tension.Name = "lab_Sawband_tension";
            lab_Sawband_tension.Size = new Size(408, 54);
            lab_Sawband_tension.TabIndex = 21;
            lab_Sawband_tension.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lab_Sawband_tensionText
            // 
            lab_Sawband_tensionText.BackColor = Color.Transparent;
            lab_Sawband_tensionText.Dock = DockStyle.Fill;
            lab_Sawband_tensionText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Sawband_tensionText.Location = new Point(3, 495);
            lab_Sawband_tensionText.Name = "lab_Sawband_tensionText";
            lab_Sawband_tensionText.Size = new Size(235, 54);
            lab_Sawband_tensionText.TabIndex = 20;
            lab_Sawband_tensionText.Text = "鋸帶張力使用累計";
            lab_Sawband_tensionText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_saw_life
            // 
            lab_saw_life.BackColor = Color.Transparent;
            lab_saw_life.Dock = DockStyle.Fill;
            lab_saw_life.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_saw_life.Location = new Point(244, 446);
            lab_saw_life.Name = "lab_saw_life";
            lab_saw_life.Size = new Size(408, 49);
            lab_saw_life.TabIndex = 19;
            lab_saw_life.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lab_saw_lifeText
            // 
            lab_saw_lifeText.BackColor = Color.Transparent;
            lab_saw_lifeText.Dock = DockStyle.Fill;
            lab_saw_lifeText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_saw_lifeText.Location = new Point(3, 446);
            lab_saw_lifeText.Name = "lab_saw_lifeText";
            lab_saw_lifeText.Size = new Size(235, 49);
            lab_saw_lifeText.TabIndex = 18;
            lab_saw_lifeText.Text = "鋸帶壽命";
            lab_saw_lifeText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Sawband_area
            // 
            lab_Sawband_area.BackColor = Color.Transparent;
            lab_Sawband_area.Dock = DockStyle.Fill;
            lab_Sawband_area.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Sawband_area.Location = new Point(244, 397);
            lab_Sawband_area.Name = "lab_Sawband_area";
            lab_Sawband_area.Size = new Size(408, 49);
            lab_Sawband_area.TabIndex = 17;
            lab_Sawband_area.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lab_Sawband_areaText
            // 
            lab_Sawband_areaText.BackColor = Color.Transparent;
            lab_Sawband_areaText.Dock = DockStyle.Fill;
            lab_Sawband_areaText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Sawband_areaText.Location = new Point(3, 397);
            lab_Sawband_areaText.Name = "lab_Sawband_areaText";
            lab_Sawband_areaText.Size = new Size(235, 49);
            lab_Sawband_areaText.TabIndex = 16;
            lab_Sawband_areaText.Text = "鋸切累計面積";
            lab_Sawband_areaText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Sawband_current
            // 
            lab_Sawband_current.BackColor = Color.Transparent;
            lab_Sawband_current.Dock = DockStyle.Fill;
            lab_Sawband_current.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Sawband_current.Location = new Point(244, 348);
            lab_Sawband_current.Name = "lab_Sawband_current";
            lab_Sawband_current.Size = new Size(408, 49);
            lab_Sawband_current.TabIndex = 15;
            lab_Sawband_current.Tag = "";
            lab_Sawband_current.Text = "(A)";
            lab_Sawband_current.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lab_Sawband_currentText
            // 
            lab_Sawband_currentText.BackColor = Color.Transparent;
            lab_Sawband_currentText.Dock = DockStyle.Fill;
            lab_Sawband_currentText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Sawband_currentText.Location = new Point(3, 348);
            lab_Sawband_currentText.Name = "lab_Sawband_currentText";
            lab_Sawband_currentText.Size = new Size(235, 49);
            lab_Sawband_currentText.TabIndex = 14;
            lab_Sawband_currentText.Text = "鋸帶馬達鋸切電流";
            lab_Sawband_currentText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Sawband_power
            // 
            lab_Sawband_power.BackColor = Color.Transparent;
            lab_Sawband_power.Dock = DockStyle.Fill;
            lab_Sawband_power.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Sawband_power.Location = new Point(244, 299);
            lab_Sawband_power.Name = "lab_Sawband_power";
            lab_Sawband_power.Size = new Size(408, 49);
            lab_Sawband_power.TabIndex = 13;
            lab_Sawband_power.Tag = "";
            lab_Sawband_power.Text = "(Hp)";
            lab_Sawband_power.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lab_Sawband_powerText
            // 
            lab_Sawband_powerText.BackColor = Color.Transparent;
            lab_Sawband_powerText.Dock = DockStyle.Fill;
            lab_Sawband_powerText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Sawband_powerText.Location = new Point(3, 299);
            lab_Sawband_powerText.Name = "lab_Sawband_powerText";
            lab_Sawband_powerText.Size = new Size(235, 49);
            lab_Sawband_powerText.TabIndex = 12;
            lab_Sawband_powerText.Text = "鋸帶馬達鋸切馬力";
            lab_Sawband_powerText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Sawband_motors_usetime
            // 
            lab_Sawband_motors_usetime.BackColor = Color.Transparent;
            lab_Sawband_motors_usetime.Dock = DockStyle.Fill;
            lab_Sawband_motors_usetime.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Sawband_motors_usetime.Location = new Point(244, 250);
            lab_Sawband_motors_usetime.Name = "lab_Sawband_motors_usetime";
            lab_Sawband_motors_usetime.Size = new Size(408, 49);
            lab_Sawband_motors_usetime.TabIndex = 11;
            lab_Sawband_motors_usetime.Tag = "";
            lab_Sawband_motors_usetime.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lab_Sawband_motors_usetimeText
            // 
            lab_Sawband_motors_usetimeText.BackColor = Color.Transparent;
            lab_Sawband_motors_usetimeText.Dock = DockStyle.Fill;
            lab_Sawband_motors_usetimeText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Sawband_motors_usetimeText.Location = new Point(3, 250);
            lab_Sawband_motors_usetimeText.Name = "lab_Sawband_motors_usetimeText";
            lab_Sawband_motors_usetimeText.Size = new Size(235, 49);
            lab_Sawband_motors_usetimeText.TabIndex = 10;
            lab_Sawband_motors_usetimeText.Text = "鋸帶馬達使用累計";
            lab_Sawband_motors_usetimeText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Sawband_speed
            // 
            lab_Sawband_speed.BackColor = Color.Transparent;
            lab_Sawband_speed.Dock = DockStyle.Fill;
            lab_Sawband_speed.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Sawband_speed.Location = new Point(244, 201);
            lab_Sawband_speed.Name = "lab_Sawband_speed";
            lab_Sawband_speed.Size = new Size(408, 49);
            lab_Sawband_speed.TabIndex = 9;
            lab_Sawband_speed.Text = "(m/min)";
            lab_Sawband_speed.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lab_Sawband_speedText
            // 
            lab_Sawband_speedText.BackColor = Color.Transparent;
            lab_Sawband_speedText.Dock = DockStyle.Fill;
            lab_Sawband_speedText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Sawband_speedText.Location = new Point(3, 201);
            lab_Sawband_speedText.Name = "lab_Sawband_speedText";
            lab_Sawband_speedText.Size = new Size(235, 49);
            lab_Sawband_speedText.TabIndex = 8;
            lab_Sawband_speedText.Text = "鋸帶速度";
            lab_Sawband_speedText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Sawblade_teeth
            // 
            lab_Sawblade_teeth.BackColor = Color.Transparent;
            lab_Sawblade_teeth.Dock = DockStyle.Fill;
            lab_Sawblade_teeth.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Sawblade_teeth.Location = new Point(244, 152);
            lab_Sawblade_teeth.Name = "lab_Sawblade_teeth";
            lab_Sawblade_teeth.Size = new Size(408, 49);
            lab_Sawblade_teeth.TabIndex = 7;
            lab_Sawblade_teeth.Text = " (TOOL / INCH)";
            lab_Sawblade_teeth.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lab_Sawblade_teethText
            // 
            lab_Sawblade_teethText.BackColor = Color.Transparent;
            lab_Sawblade_teethText.Dock = DockStyle.Fill;
            lab_Sawblade_teethText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Sawblade_teethText.Location = new Point(3, 152);
            lab_Sawblade_teethText.Name = "lab_Sawblade_teethText";
            lab_Sawblade_teethText.Size = new Size(235, 49);
            lab_Sawblade_teethText.TabIndex = 6;
            lab_Sawblade_teethText.Text = "鋸帶齒數";
            lab_Sawblade_teethText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Sawblade_type
            // 
            lab_Sawblade_type.BackColor = Color.Transparent;
            lab_Sawblade_type.Dock = DockStyle.Fill;
            lab_Sawblade_type.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Sawblade_type.Location = new Point(244, 103);
            lab_Sawblade_type.Name = "lab_Sawblade_type";
            lab_Sawblade_type.Size = new Size(408, 49);
            lab_Sawblade_type.TabIndex = 5;
            lab_Sawblade_type.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lab_Sawblade_typeText
            // 
            lab_Sawblade_typeText.BackColor = Color.Transparent;
            lab_Sawblade_typeText.Dock = DockStyle.Fill;
            lab_Sawblade_typeText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Sawblade_typeText.Location = new Point(3, 103);
            lab_Sawblade_typeText.Name = "lab_Sawblade_typeText";
            lab_Sawblade_typeText.Size = new Size(235, 49);
            lab_Sawblade_typeText.TabIndex = 4;
            lab_Sawblade_typeText.Text = "鋸帶型號";
            lab_Sawblade_typeText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Sawblade_material
            // 
            lab_Sawblade_material.BackColor = Color.Transparent;
            lab_Sawblade_material.Dock = DockStyle.Fill;
            lab_Sawblade_material.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Sawblade_material.Location = new Point(244, 54);
            lab_Sawblade_material.Name = "lab_Sawblade_material";
            lab_Sawblade_material.Size = new Size(408, 49);
            lab_Sawblade_material.TabIndex = 3;
            lab_Sawblade_material.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lab_Sawblade_materialText
            // 
            lab_Sawblade_materialText.AutoSize = true;
            lab_Sawblade_materialText.BackColor = Color.Transparent;
            lab_Sawblade_materialText.Dock = DockStyle.Fill;
            lab_Sawblade_materialText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Sawblade_materialText.Location = new Point(3, 54);
            lab_Sawblade_materialText.Name = "lab_Sawblade_materialText";
            lab_Sawblade_materialText.Size = new Size(235, 49);
            lab_Sawblade_materialText.TabIndex = 2;
            lab_Sawblade_materialText.Text = "鋸帶材質";
            lab_Sawblade_materialText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_Sawband_brand
            // 
            lab_Sawband_brand.BackColor = Color.Transparent;
            lab_Sawband_brand.Dock = DockStyle.Fill;
            lab_Sawband_brand.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold);
            lab_Sawband_brand.Location = new Point(244, 0);
            lab_Sawband_brand.Name = "lab_Sawband_brand";
            lab_Sawband_brand.Size = new Size(408, 54);
            lab_Sawband_brand.TabIndex = 1;
            lab_Sawband_brand.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lab_Sawband_brandText
            // 
            lab_Sawband_brandText.BackColor = Color.Transparent;
            lab_Sawband_brandText.Dock = DockStyle.Fill;
            lab_Sawband_brandText.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lab_Sawband_brandText.Location = new Point(3, 0);
            lab_Sawband_brandText.Name = "lab_Sawband_brandText";
            lab_Sawband_brandText.Size = new Size(235, 54);
            lab_Sawband_brandText.TabIndex = 0;
            lab_Sawband_brandText.Text = "鋸帶廠牌";
            lab_Sawband_brandText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lab_time
            // 
            lab_time.AutoSize = true;
            lab_time.Location = new Point(583, 599);
            lab_time.Name = "lab_time";
            lab_time.Size = new Size(0, 15);
            lab_time.TabIndex = 1;
            lab_time.Visible = false;
            // 
            // Sawingband_Info
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(730, 630);
            Controls.Add(lab_time);
            Controls.Add(panel1);
            Margin = new Padding(4);
            Name = "Sawingband_Info";
            StartPosition = FormStartPosition.CenterParent;
            Text = "鋸帶資料";
            Load += sawband_Info_Load;
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lab_Sawblade_materialText;
        private Label lab_Sawband_brand;
        private Label lab_Sawband_brandText;
        private Label lab_Sawblade_teeth;
        private Label lab_Sawblade_teethText;
        private Label lab_Sawblade_type;
        private Label lab_Sawblade_typeText;
        private Label lab_Sawblade_material;
        private Label lab_Sawband_tensionText;
        private Label lab_saw_life;
        private Label lab_saw_lifeText;
        private Label lab_Sawband_area;
        private Label lab_Sawband_areaText;
        private Label lab_Sawband_current;
        private Label lab_Sawband_currentText;
        private Label lab_Sawband_power;
        private Label lab_Sawband_powerText;
        private Label lab_Sawband_motors_usetime;
        private Label lab_Sawband_motors_usetimeText;
        private Label lab_Sawband_speed;
        private Label lab_Sawband_speedText;
        private Label lab_Sawband_tension;
        private Label lab_time;
    }
}

