
using FX5U_IOMonitor.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FX5U_IOMonitor.Models
{
    public static class Panel_design_Setting
    {
        static string datatable = "";
        public static Panel CreateSettingPanel( string machine ,string address,string currentLang)
        {
            datatable = machine;

           
            // 主容器 Panel
            Panel mainPanel = new Panel
            {
                Size = new Size(600, 400),
                //BackColor = Color.White
            };

            // 1️ 上層Panel（圖 + 資訊）
            Panel topPanel = new Panel
            {
                Size = new Size(580, 130),
                Location = new Point(10, 10)
            };

            // 文字資訊區

            // ▶▶ 資訊 Label（微軟正黑體，整合為一個 label）
            Label label_detail = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold),
                MaximumSize = new Size(560, 0),                   // ✅ 限制最大寬度（會自動換行）
                Location = new Point(15, 25)

            };

            label_detail.Text =
                 $"{LanguageManager.Translate("ShowDetail_lb_address").PadRight(10)} {address}\n" +
                 $"{LanguageManager.Translate("ShowDetail_lb_descript").PadRight(10)} {DBfunction.Get_Decription_ByAddress(datatable, address)}\n" +
                 $"{LanguageManager.Translate("ShowDetail_history_lb_Detail").PadRight(10)} {DBfunction.Get_Comment_ByAddress(datatable, address)}";

            topPanel.Controls.Add(label_detail);

            mainPanel.Controls.Add(topPanel);

            // 2️ 分隔線（可選）
            Label line = new Label
            {
                BorderStyle = BorderStyle.Fixed3D,
                Height = 2,
                Width = 560,
                Location = new Point(10, 150)
            };
            mainPanel.Controls.Add(line);

            // 3️ 下方設定區（呼叫你之前定義的 CreateSettingTable 函式）
            //Panel settingTable = CreateSettingTableWithScale(address, 1.0f);
            Panel settingTable = CreateSettingPanelWithCoordinates(address, 1.0f);

            settingTable.Location = new Point(10, 160);
            mainPanel.Controls.Add(settingTable);

            return mainPanel;
        }

        private static Panel CreateSettingPanelWithCoordinates(string address, float scale)
        {
            // 資料初始化
            int maxvalue = DBfunction.Get_MaxLife_ByAddress(datatable, address);
            int yellowValue = DBfunction.Get_SetY_ByAddress(datatable, address);
            int redValue = DBfunction.Get_SetR_ByAddress(datatable, address);

            // 建立 Panel，背景白色
            Panel panel = new Panel
            {
                Size = new Size((int)(520 * scale), (int)(225 * scale)),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Font labelFont = new Font("Microsoft JhengHei UI", 15.75F * scale, FontStyle.Bold);
            Font smallFont = new Font("Microsoft JhengHei UI", 9.75F * scale, FontStyle.Bold);
            Font inputFont = new Font("微軟正黑體", 14.25F * scale, FontStyle.Bold);

            // label2：最大壽命
            Label label2 = new Label
            {
                Text = LanguageManager.Translate("ShowDetail_Setting_MaxLife"),
                Font = labelFont,
                Location = new Point((int)(15 * scale), (int)(15 * scale)),
                AutoSize = true
            };
            panel.Controls.Add(label2);

            // txb_max_number
            NumericUpDown txb_max_number = new NumericUpDown
            {
                Font = inputFont,
                Location = new Point((int)(253 * scale), (int)(15 * scale)),
                Size = new Size((int)(176 * scale), (int)(33 * scale)),
                Maximum = 10000000000000,
                Value = maxvalue,
                TextAlign = HorizontalAlignment.Center
            };
            panel.Controls.Add(txb_max_number);

            // lab_Repair_step
            Label lab_Repair_step = new Label
            {
                Text = LanguageManager.Translate("ShowDetail_Element_lab_green"),
                Size = new Size((int)(450 * scale), (int)(33 * scale)),
                Font = labelFont,
                Location = new Point((int)(15 * scale), (int)(60 * scale)),
                AutoSize = true
            };
            AdjustLabelFontToFit(lab_Repair_step, lab_Repair_step.Text);

            panel.Controls.Add(lab_Repair_step);

            // label3：黃燈設定
            Label Setting_MaxLife = new Label
            {
                Text = LanguageManager.Translate("ShowDetail_Setting_YellowLight"),
                Font = labelFont,
                Location = new Point((int)(15 * scale), (int)(100 * scale)),
                AutoSize = true
            };
            panel.Controls.Add(Setting_MaxLife);

            // txb_yellow_light
            NumericUpDown txb_yellow_light = new NumericUpDown
            {
                Font = inputFont,
                Location = new Point((int)(253 * scale), (int)(100 * scale)),
                Size = new Size((int)(176 * scale), (int)(33 * scale)),
                Maximum = 100,
                Value = yellowValue,
                TextAlign = HorizontalAlignment.Center
            };
            panel.Controls.Add(txb_yellow_light);

            // label8
            Label Setting_YellowLight = new Label
            {
                Text = LanguageManager.Translate("ShowDetail_Setting_YellowNote"),
                Font = smallFont,
                Location = new Point((int)(17 * scale), (int)(130 * scale)),
                AutoSize = true
            };
            panel.Controls.Add(Setting_YellowLight);

            // label4：紅燈設定
            Label Setting_RedLight = new Label
            {
                Text = LanguageManager.Translate("ShowDetail_Setting_RedLight"),
                Font = labelFont,
                Location = new Point((int)(15 * scale), (int)(160 * scale)),
                AutoSize = true
            };
            panel.Controls.Add(Setting_RedLight);

            // txb_red_light
            NumericUpDown txb_red_light = new NumericUpDown
            {
                Font = inputFont,
                Location = new Point((int)(253 * scale), (int)(160 * scale)),
                Size = new Size((int)(176 * scale), (int)(33 * scale)),
                Maximum = 100,
                Value = redValue,
                TextAlign = HorizontalAlignment.Center
            };
            panel.Controls.Add(txb_red_light);

            // label9
            Label ShowDetail_Setting_RedNote = new Label
            {
                Text = LanguageManager.Translate("ShowDetail_Setting_RedNote"),
                Font = smallFont,
                Location = new Point((int)(17 * scale), (int)(190 * scale)),
                AutoSize = true
            };
            panel.Controls.Add(ShowDetail_Setting_RedNote);

            // btn_update
            System.Windows.Forms.Button btn_update = new System.Windows.Forms.Button
            {
                Text = LanguageManager.Translate("ShowDetail_btn_Reset"),
                Font = inputFont,
                Location = new Point((int)(450 * scale), (int)(100 * scale)),
                Size = new Size((int)(61 * scale), (int)(36 * scale))
            };
            btn_update.Click += (s, e) =>
            {
                txb_max_number.Value = DBfunction.Get_MaxLife_ByAddress(datatable, address);
                txb_yellow_light.Value = DBfunction.Get_SetY_ByAddress(datatable, address);
                txb_red_light.Value = DBfunction.Get_SetR_ByAddress(datatable, address);

                MessageBox.Show("設定已重置");
            };
            panel.Controls.Add(btn_update);

            // 
            System.Windows.Forms.Button btn_add = new System.Windows.Forms.Button
            {
                Text = LanguageManager.Translate("ShowDetail_btn_Update"),
                Font = inputFont,
                Location = new Point((int)(450 * scale), (int)(160 * scale)),
                Size = new Size((int)(61 * scale), (int)(36 * scale))
            };
            btn_add.Click += (s, e) =>
            {
                DBfunction.Set_MaxLife_ByAddress(datatable, address, (int)txb_max_number.Value);
                DBfunction.Set_SetY_ByAddress(datatable, address, (int)txb_yellow_light.Value);
                DBfunction.Set_SetR_ByAddress(datatable, address, (int)txb_red_light.Value);
                MessageBox.Show("資料已更新");
            };
            panel.Controls.Add(btn_add);

            return panel;
        }

        private static void AdjustLabelFontToFit(Label label, string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            Size proposedSize = label.ClientSize;
            float fontSize = 20f; // 起始字體大小，可自訂
            Font font;

            using (Graphics g = label.CreateGraphics())
            {
                do
                {
                    font = new Font(label.Font.FontFamily, fontSize, label.Font.Style);
                    SizeF textSize = g.MeasureString(text, font);

                    if (textSize.Width <= proposedSize.Width && textSize.Height <= proposedSize.Height)
                        break;

                    fontSize -= 0.5f; // 遞減縮小
                }
                while (fontSize > 6f); // 最小字體限制
            }

            label.Font = font;
        }

    }
}
