
using FX5U_IOMonitor.Data;
using System.Reflection.Emit;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace FX5U_IOMonitor.Models
{
    internal class Panel_design
    {

       
        //環型顯示窗格設計
        public static PictureBox CreateDoughnutChartPanel(int size, float[] values, Color[] colors)
        {
            // 計算總和
            float total = 0;
            foreach (var val in values) total += val;

            // 設置 PictureBox 作為環形圖顯示區域
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(size, size),
                Location = new Point(10, 10),
            };
            pictureBox.Paint += (sender, e) => DrawDoughnutChart(e.Graphics, pictureBox.ClientRectangle, values, colors, total);

          
            return pictureBox;
        }

        private static void DrawDoughnutChart(Graphics g, Rectangle bounds, float[] values, Color[] colors, float total)
        {
            int padding = 10;
            int holeRatio = 4;
            float center = values.Sum();
            Rectangle outerRect = new Rectangle(bounds.X + padding, bounds.Y + padding,
                bounds.Width - 2 * padding, bounds.Height - 2 * padding);

            int holeSize = outerRect.Width / holeRatio;
            Rectangle innerRect = new Rectangle(
                outerRect.X + holeSize / 2,
                outerRect.Y + holeSize / 2,
                outerRect.Width - holeSize,
                outerRect.Height - holeSize);

            float startAngle = -90;
            for (int i = 0; i < values.Length; i++)
            {
                float sweepAngle = values[i] / total * 360;
                using (SolidBrush brush = new SolidBrush(colors[i]))
                {
                    g.FillPie(brush, outerRect, startAngle, sweepAngle);
                }
                startAngle += sweepAngle;
            }

            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            {
                g.FillEllipse(whiteBrush, innerRect);
            }

            using (Font font = new Font("Arial", bounds.Width / 6, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(Color.DimGray))
            {
                SizeF textSize = g.MeasureString(center.ToString(), font);
                PointF textPos = new PointF(
                    innerRect.X + (innerRect.Width - textSize.Width) / 2,
                    innerRect.Y + (innerRect.Height - textSize.Height) / 2);
                g.DrawString(center.ToString(), font, textBrush, textPos);
            }
        }

        private static void CenterAlignNumericUpDown(NumericUpDown numericUpDown)
        {
            // 獲取內部的 TextBox 控件
            var textBox = numericUpDown.Controls[1] as TextBox;
            if (textBox != null)
            {
                // 設置文字置中對齊
                textBox.TextAlign = HorizontalAlignment.Center;
            }
        }
        /// <summary>
        /// 歷史詳細資料表的圖形
        /// </summary>
        /// <param name="address"></param>
        /// <param name="equipmentDescription"></param>
        /// <param name="maxLife"></param>
        /// <param name="currentUse"></param>
        /// <param name="comment"></param>
        /// <param name="historyList"></param>
        /// <returns></returns>
        public static Panel CreateUsagePanel(
         string address,
         string equipmentDescription,
         int maxLife,
         int currentUse,
         string comment,
         List<History> historyList)
        {
            // 主 Panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                //BackColor = Color.White,
                Padding = new Padding(10)
            };

            // ▶ 上方 Panel（環形圖 + 資訊）
            Panel topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 160,
                //BackColor = Color.White
            };

            // ▶▶ 環形圖區塊
            Panel chartPanel = new Panel
            {
                Size = new Size(120, 120),
                Location = new Point(10, 10),
                //BackColor = Color.White
            };

            chartPanel.Paint += (sender, e) =>
            {
                float used = currentUse;
                float total = maxLife;
                float[] values = new float[] { total, used };
                Color[] colors = new Color[] { Color.LightGray, Color.Green };

                DrawDoughnutChartTO_history(e.Graphics, chartPanel.ClientRectangle, values, colors, total, equipmentDescription);
            };

            // ▶▶ 資訊 Label（微軟正黑體，整合為一個 label）
            Label label_detail = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft JhengHei", 12, FontStyle.Bold),
                Location = new Point(150, 10),
                MaximumSize = new Size(400, 0),
                AutoEllipsis = false,
                Text =
               $"{LanguageManager.Translate("ShowDetail_lb_address").PadRight(6)} {address}\n" +
               $"{LanguageManager.Translate("ShowDetail_lb_descript").PadRight(6)} {equipmentDescription}\n" +
               $"{LanguageManager.Translate("CreatPanel_lb_maxCount").PadRight(6)} {maxLife}\n" +
               $"{LanguageManager.Translate("ShowDetail_label_use").PadRight(6)} {currentUse}\n" +
               $"{LanguageManager.Translate("ShowDetail_label_remain").PadRight(6)} {maxLife - currentUse}\n" +
               $"{LanguageManager.Translate("ShowDetail_history_lb_Detail").PadRight(6)} \n{comment}"
            };

            //label_detail.Text =
            //    $"{"元件儲存器地址：".PadRight(10)} {address}\n" +
            //    $"{"更換料號　　　：".PadRight(10)} {equipmentDescription}\n" +
            //    $"{"預計可觸發次數：".PadRight(10)} {maxLife}\n" +
            //    $"{"目前已觸發次數：".PadRight(10)} {currentUse} 次\n" +
            //    $"{"剩餘可使用次數：".PadRight(10)} {maxLife - currentUse} 次\n" +
            //    $"{"設備細節描述　：".PadRight(10)} {comment}\n\n";
           

            topPanel.Controls.Add(chartPanel);
            topPanel.Controls.Add(label_detail);

            // ▶ 下方表格 Panel
            Panel bottomPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.White
            };

            DataGridView dataGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                GridColor = Color.White,
                CellBorderStyle = DataGridViewCellBorderStyle.Single,
                EnableHeadersVisualStyles = false
            };

            dataGrid.DefaultCellStyle.Font = new Font("微軟正黑體", 10);
            dataGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 10, FontStyle.Bold);
            dataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // 加入欄位（不設定 width，讓 Fill 自動調整）
            dataGrid.Columns.Add("Index", "");
            dataGrid.Columns["Index"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGrid.Columns["Index"].Width = 40;
            dataGrid.Columns.Add("StartTime", LanguageManager.Translate("ShowDetail_StartTime"));
            dataGrid.Columns.Add("EndTime", LanguageManager.Translate("ShowDetail_EndTime"));
            dataGrid.Columns.Add("TotalUsage", LanguageManager.Translate("ShowDetail_TotalUsage"));
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 寫入資料與補空列
            int totalRows = 6;
            for (int i = 0; i < historyList.Count; i++)
            {
                var h = historyList[i];
                dataGrid.Rows.Add(
                    (i + 1).ToString(),
                    h.StartTime.ToString("yyyy/MM/dd-HH:mm"),
                    h.EndTime.ToString(),
                    h.usetime.ToString());
            }
            for (int i = historyList.Count; i < totalRows; i++)
            {
                dataGrid.Rows.Add((i + 1).ToString(), "", "", "");
            }

            // ✅ 設定行高平均分布
            dataGrid.RowTemplate.Height = bottomPanel.Height / totalRows;
            bottomPanel.Controls.Add(dataGrid);

            // ▶ 組合 Panel
            mainPanel.Controls.Add(bottomPanel);
            mainPanel.Controls.Add(topPanel);

            return mainPanel;
        }
        private static void DrawDoughnutChartTO_history(Graphics g, Rectangle bounds, float[] values, Color[] colors, float total, string centerText)
        {
            int padding = 5;
            int holeSize = bounds.Width / 10;
            Rectangle outerRect = new Rectangle(padding, padding, bounds.Width - 2 * padding, bounds.Height - 2 * padding);
            Rectangle innerRect = new Rectangle(outerRect.X + holeSize, outerRect.Y + holeSize, outerRect.Width - 2 * holeSize, outerRect.Height - 2 * holeSize);

            float startAngle = 270;
            for (int i = 0; i < values.Length; i++)
            {
                float sweepAngle = values[i] / total * 360;
                using (SolidBrush brush = new SolidBrush(colors[i]))
                {
                    g.FillPie(brush, outerRect, startAngle, sweepAngle);
                }
                startAngle += sweepAngle;
            }

            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            {
                g.FillEllipse(whiteBrush, innerRect);
            }

            using (Font font = new Font("Microsoft JhengHei", 10, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(Color.Black))
            {
                SizeF textSize = g.MeasureString(centerText, font);
                PointF textPos = new PointF(
                    innerRect.X + (innerRect.Width - textSize.Width) / 2,
                    innerRect.Y + (innerRect.Height - textSize.Height) / 2);
                g.DrawString(centerText, font, textBrush, textPos);
            }
        }


        public static Panel CreateShowMainPanel(
         string address,
         string equipmentDescription,
         int maxLife,
         int currentUse,
         string comment,string equipmentStartTime)
        {
            // 主 Panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                //BackColor = Color.White,
                Padding = new Padding(10)
            };

            int y = 20;

            void AddLabel(string title, string value)
            {
                Label lbl = new Label
                {
                    AutoSize = true,
                    //AutoSize = false, // 關掉 AutoSize 才能控制寬度
                    //Width = 400,      // ❗你可以依畫面需求調整寬度
                    Font = new Font("Microsoft JhengHei", 15, FontStyle.Bold),
                    Location = new Point(6, y),
                    Text = $"{title}{value}",
                    TextAlign = ContentAlignment.MiddleLeft
                };
                AdjustFontToFit(lbl);

                mainPanel.Controls.Add(lbl);
                y += 35;
            }

            //AddLabel("元件儲存器地址", address);
            //AddLabel("更換料號　　　", equipmentDescription);
            //AddLabel("預計可觸發次數", maxLife.ToString()+"次");
            AddLabel(LanguageManager.Translate("ShowDetail_lb_address"),address);
            AddLabel(LanguageManager.Translate("ShowDetail_lb_descript"),equipmentDescription);
            AddLabel(LanguageManager.Translate("CreatPanel_lb_maxCount"),maxLife.ToString()+LanguageManager.Translate("CreatPanel_lb_Count"));
            int remain = maxLife - currentUse;
            // 加入可更新的 currentUse Label
            Label lbl_useCount = new Label
            {
                Name = "lb_useCount",
                AutoSize = true,
                Font = new Font("Microsoft JhengHei", 15, FontStyle.Bold),
                Location = new Point(6, y),
                //Text=$"目前已觸發次數   ：{currentUse} 次"
                Text = LanguageManager.TranslateFormat("ShowDetail_lb_useCount", currentUse)
            };
            mainPanel.Controls.Add(lbl_useCount);
            y += 35;
            Label lbl_remainCount = new Label
            {
                Name = "lb_remainCount",
                AutoSize = true,
                Font = new Font("Microsoft JhengHei", 15, FontStyle.Bold),
                Location = new Point(6, y),
                //Text = $"剩餘可使用次數   ：" + (maxLife - currentUse).ToString() + " 次"
                Text = LanguageManager.TranslateFormat("ShowDetail_lb_remainCount", remain)

            }; 
            mainPanel.Controls.Add(lbl_remainCount);
            y += 35;

            Label lbl_comment = new Label
            {
                Name = "lbl_comment",
                AutoSize = true,
                Font = new Font("Microsoft JhengHei", 15, FontStyle.Bold),
                Location = new Point(6, y),
                //Text = $"設備細節描述　   ："
                Text = LanguageManager.Translate("ShowDetail_lb_Detail")
            };

            mainPanel.Controls.Add(lbl_comment);

            Label lbl_comment_string = new Label
            {
                Name = "lbl_comment_string",
                AutoSize = true,                                  // ✅ 自動展開高度
                Font = new Font("Microsoft JhengHei", 15, FontStyle.Bold),
                Location = new Point(175, y),
                MaximumSize = new Size(300, 0),                   // ✅ 限制最大寬度（會自動換行）
                Text =comment,
                TextAlign = ContentAlignment.TopLeft,
            };

            mainPanel.Controls.Add(lbl_comment_string);
            lbl_comment_string.BringToFront();

            y += 125;

            AddLabel(LanguageManager.Translate("ShowDetail_lb_StartTime"), equipmentStartTime);

            return mainPanel;
        }
     

        public static TableLayoutPanel CreateColorLegendPanel(string redText, string yellowText, string greenText)
        {
            TableLayoutPanel legendPanel = new TableLayoutPanel
            {
                ColumnCount = 6,
                RowCount = 1,
                Size = new Size(115, 22),
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0),
                Padding = new Padding(0),
                BackColor = Color.Transparent
            };

            legendPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 13));
            legendPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 27));
            legendPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 13));
            legendPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25));
            legendPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 13));
            legendPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25));
            legendPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Panel CreateBox(Color color) => new Panel
            {
                BackColor = color,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(13, 13),
                Margin = new Padding(1),
                Anchor = AnchorStyles.None
            };

            Label CreateLabel(string text, string name) => new Label
            {
                Name = name,
                Text = text,
                Font = new Font("Microsoft JhengHei", 10f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Width = 25,
                Height = 15,
                Margin = new Padding(0),
                Anchor = AnchorStyles.Left
            };

            legendPanel.Controls.Add(CreateBox(Color.Green), 0, 0);
            legendPanel.Controls.Add(CreateLabel(redText, "lblRed"), 1, 0);

            legendPanel.Controls.Add(CreateBox(Color.Yellow), 2, 0);
            legendPanel.Controls.Add(CreateLabel(yellowText, "lblYellow"), 3, 0);

            legendPanel.Controls.Add(CreateBox(Color.Red), 4, 0);
            legendPanel.Controls.Add(CreateLabel(greenText, "lblGreen"), 5, 0);

            return legendPanel;

        }


        static void AdjustFontToFit(Label label)
        {
            int maxFontSize = 16;
            int minFontSize = 8;
            Size proposedSize = new Size(label.Width, int.MaxValue);
            using (Graphics g = label.CreateGraphics())
            {
                for (int fontSize = maxFontSize; fontSize >= minFontSize; fontSize--)
                {
                    Font testFont = new Font(label.Font.FontFamily, fontSize, label.Font.Style);
                    SizeF textSize = g.MeasureString(label.Text, testFont, label.Width);
                    if (textSize.Height <= label.Height && textSize.Width <= label.Width)
                    {
                        label.Font = testFont;
                        break;
                    }
                }
            }
        }
    } 
}
