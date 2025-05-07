
using FX5U_IOMonitor.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FX5U_IOMonitor.Models
{
    public static class panel_design_Setting
    {
        static string datatable = "";
        public static Panel CreateSettingPanel(string address)
        {
            

            datatable = DBfunction.FindTableWithAddress(address);
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
                Font = new Font("Microsoft JhengHei", 16, FontStyle.Bold),
                Location = new Point(15, 25)
            };

            label_detail.Text =
                 $"{"元件儲存器地址：".PadRight(10)} {address}\n" +
                 $"{"更換料號　　　：".PadRight(10)} {DBfunction.Get_Decription_ByAddress(datatable, address)}\n" +
                 $"{"設備細節描述　：".PadRight(10)} {DBfunction.Get_Comment_ByAddress(datatable, address)}";

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
            Panel settingTable = CreateSettingTableWithScale(address, 1.0f);
            settingTable.Location = new Point(10, 160);
            mainPanel.Controls.Add(settingTable);

            return mainPanel;
        }



        private static Panel CreateSettingTableWithScale(string address ,float scale )
        {
            int baseWidth = 520;
            int baseHeight = 225;
            List<NumericUpDown> numBoxes = new List<NumericUpDown>();  // 儲存所有欄位控制項

            int Setting_green = DBfunction.Get_SetG_ByAddress(datatable, address);
            int Setting_yellow = DBfunction.Get_SetY_ByAddress(datatable, address);
            int Setting_red = DBfunction.Get_SetR_ByAddress(datatable, address);
            int maxvalue = DBfunction.Get_MaxLife_ByAddress(datatable, address);


            int[] thresholds = { Setting_green, Setting_yellow, Setting_red };


            Panel outerPanel = new Panel
            {
                Size = new Size((int)(baseWidth * scale), (int)(baseHeight * scale)),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding((int)(10 * scale)),
                BackColor = Color.White
            };

            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 4,
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize,

            };

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 260F * scale)); // Label 欄寬
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F * scale)); // 控制欄寬
            int rowCount = 4;

            string[] labels = {
                "當前最大壽命設定(次)：",
                "黃燈警告狀態設定(%)：",
                "黃燈警告狀態設定(%)：",
                "紅燈異常狀態設定(%)："
                };

            for (int i = 0; i < 4; i++)
            {
                //table.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F * scale)); // 每列依 scale 縮放
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rowCount));

                Label lbl = new Label
                {
                    Text = labels[i],
                    Font = new Font("微軟正黑體", 16F * scale, FontStyle.Bold),
                    AutoSize = false,
                    Size = new Size((int)(260 * scale), (int)(45 * scale)),
                    TextAlign = ContentAlignment.MiddleRight,
                    Anchor = AnchorStyles.Right,
                    Margin = new Padding((int)(5 * scale)),
                };

                NumericUpDown num = new NumericUpDown
                {
                    Font = new Font("微軟正黑體", 16F * scale, FontStyle.Bold),
                    Maximum = (i == 0) ? 100000000 : 100,
                    Value = (i == 0) ? maxvalue : thresholds[i - 1],
                    Size = new Size((int)(160 * scale), (int)(35 * scale)),  // 固定大小
                    Anchor = AnchorStyles.Left,
                    TextAlign = HorizontalAlignment.Center,
                    Margin = new Padding((int)(5 * scale))
                };
                numBoxes.Add(num);  // 儲存這個欄位

                // 第 0 行放「重置」、第 2 行放「儲存」
                System.Windows.Forms.Button? btn = null;
                if (i == 0)
                {
                    btn = new System.Windows.Forms.Button
                    {
                        Text = "重置",
                        Font = new Font("微軟正黑體", 12F * scale, FontStyle.Bold),
                        Size = new Size((int)(50 * scale), (int)(35 * scale)),
                        Anchor = AnchorStyles.None,
                        BackColor = Color.White,
                        Margin = new Padding((int)(5 * scale))
                    };
                    btn.Click += (s, e) =>
                    {
                        for (int j = 0; j < numBoxes.Count; j++)
                        {
                          
                            if (j == 0)
                                numBoxes[0].Value = DBfunction.Get_MaxLife_ByAddress(datatable, address);
                            else if (j == 1)
                                numBoxes[1].Value = DBfunction.Get_SetG_ByAddress(datatable, address);
                            else if (j == 2)
                                numBoxes[2].Value = DBfunction.Get_SetY_ByAddress(datatable, address);
                            else if (j == 3)
                                numBoxes[3].Value = DBfunction.Get_SetR_ByAddress(datatable, address);
                        }
                        //MessageBox.Show("已重置為預設值！");
                    };
                }
                else if (i == 2)
                {
                    btn = new System.Windows.Forms.Button
                    {
                        Text = "更新",
                        Font = new Font("微軟正黑體", 12F * scale, FontStyle.Bold),
                        Size = new Size((int)(50 * scale), (int)(35 * scale)),
                        Anchor = AnchorStyles.None,
                        BackColor = Color.White,
                        Margin = new Padding((int)(5 * scale))
                    };
                    btn.Click += (s, e) =>
                    {
                        DBfunction.Set_MaxLife_ByAddress(datatable, address, (int)numBoxes[0].Value);
                        DBfunction.Set_SetG_ByAddress(datatable, address, (int)numBoxes[1].Value);
                        DBfunction.Set_SetY_ByAddress(datatable, address, (int)numBoxes[2].Value);
                        DBfunction.Set_SetR_ByAddress(datatable, address, (int)numBoxes[3].Value);

                    };
                }
                table.Controls.Add(lbl, 0, i);
                table.Controls.Add(num, 1, i);
                if (btn != null)
                    table.Controls.Add(btn, 2, i);
            }
            
            outerPanel.Controls.Add(table);
            return outerPanel;
        }

    }
}
