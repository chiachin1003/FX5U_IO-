using FX5U_IOMonitor.Data;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Font = System.Drawing.Font;

namespace FX5U_IOMonitor.Models
{
    internal class MachineInfo
    {
        private static string machine_name = "";
        public static event EventHandler? NotifyShowdetailToRefresh;

        public static class PanelFactory
        {
            /// <summary>
            /// 動態生成標準化 Panel
            /// </summary>
            /// <param name="location">Panel 的位置</param>
            /// <param name="equipmentName">設備名稱</param>
            /// <param name="lightColor">燈號顏色</param>
            /// <param name="percent">百分比數值</param>
            /// <param name="rulPercent">剩餘壽命數值</param>
            /// <param name="effect">影響數值</param>
            /// <returns>返回生成的標準化 Panel</returns>
            /// 
            //需要壽命監控時的按鈕創造
            public static Panel CreatePanel(Point location,string dbtable, bool Electronic, string equipmentName, string percent, string rulPercent, string effect, string address, bool? state)
            {
                machine_name = dbtable;
                // 初始化 Panel
                Panel panel = new Panel
                {
                    Location = location,
                    Size = new Size(200, 100),
                    BorderStyle = BorderStyle.FixedSingle,
                };

                // 初始化 lab_equipment
                Label labEquipment = new Label
                {
                    AutoSize = true,
                    Font = new Font("微軟正黑體", 12F, FontStyle.Bold),
                    MaximumSize = new System.Drawing.Size(150, 0), // 限制最大寬度為 50px，高度自適應
                    Location = new Point(9, 11),
                    Name = "lab_equipment",
                    BackColor = Color.Transparent, // ✅ 背景透明
                    Text = equipmentName
                };
                panel.Controls.Add(labEquipment);

                // 初始化 lab_effect
                Label labEffect = new Label
                {
                    AutoSize = true,
                    MaximumSize = new System.Drawing.Size(120, 0), // 限制最大寬度為 50px，高度自適應
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                    Text = effect,
                    Font = new Font("微軟正黑體", 11F, FontStyle.Bold),
                    Location = new Point(9, 33),
                    Name = "lab_effect",
                };
                Text_design.AdjustFontToFitBox(labEffect, effect, minFontSize: 10f);
                panel.Controls.Add(labEffect);

                // 初始化 panel_light
                Panel panelLight = new Panel
                {
                    Location = new Point(153, 11),
                    Name = "panel_light",
                    Size = new Size(40, 40),
                    BackColor = (Color)SetColor(percent, DBfunction.Get_SetG_ByAddress(dbtable, address)
                    , DBfunction.Get_SetY_ByAddress(dbtable, address)
                    , DBfunction.Get_SetR_ByAddress(dbtable, address))
                };

                SetCircularShape(panelLight, 30);
                panel.Controls.Add(panelLight);
                panelLight.Tag = address;  // 使用 Tag 屬性來存儲設備名稱(指定對應的列)

                panelLight.Click += (sender, e) => PanelLight_Click(sender, e);
                Panel panel_ON = new Panel
                {
                    Location = new Point(181, 41),
                    Name = "panel_ON",
                    Size = new Size(8, 8),
                    BackColor = (Color)SetPointColor(state)
                };
                //SetCircularShape(panel_ON, 20);
                panel.Controls.Add(panel_ON);

                // 初始化 RUL_precent
                ProgressBar rulLabel = new ProgressBar
                {
                    Location = new Point(145, 50),
                    Name = "RUL_precent",
                    Size = new Size(45, 12),
                    Value = ProgressBarValue(percent)
                };
                panel.Controls.Add(rulLabel);

                // 初始化 label_percent
                Label labelPercent = new Label
                {
                    AutoSize = true,
                    Font = new Font("微軟正黑體", 10F, FontStyle.Bold),
                    Location = new Point(135, 65),
                    Name = "label_percent",
                    Text = $"{percent} %"
                };
                panel.Controls.Add(labelPercent);


                // 設定 Panel 實際大小
                panel.Size = new Size(228, 110);

                // 原始設計大小
                float scaleX = panel.Width / 200f;
                float scaleY = panel.Height / 100f;

                // 套用縮放
                ResizeControls(panel, scaleX, scaleY);
                return panel;

            }



            private static void SetCircularShape(Control control, int num)
            {
                // 使用 GraphicsPath 設置圓形區域
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, num, num);
                control.Region = new Region(path);
            }
            public static int ProgressBarValue(string percent)
            {
                if (string.IsNullOrWhiteSpace(percent))
                    return 0;

                // 嘗試轉為 double，若失敗 (無法轉型)，也回傳 Color.Gray
                if (!double.TryParse(percent, out double dPercent))
                    return 0;

                double value = double.Parse(percent);
                int intValue = (int)value;
                if (intValue >= 100) return 100;
                else if (intValue <= 0) return 0;
                else return intValue;

            }
            public static object SetColor(string percent, int Green, int yellow, int red)
            {
                // 若沒有傳值進來 (null 或空字串)，直接回傳 Color.Gray
                if (string.IsNullOrWhiteSpace(percent))
                    return Color.Gray;

                // 嘗試轉為 double，若失敗 (無法轉型)，也回傳 Color.Gray
                if (double.TryParse(percent, out double dPercent))
                {
                    double roundedValue = Math.Round(dPercent, 3);
                    // 3. 判斷數值範圍
                    if (roundedValue <= red) return Color.Red;
                    else if (roundedValue > red && roundedValue <= yellow) return Color.Yellow;
                    else return Color.Green;
                }
                return Color.Gray;

            }
            private static object SetPointColor(bool? Ture)
            {
                // 若沒有傳值進來 (null 或空)，直接回傳 Color.Gray
                // 若輸入為 null，直接回傳 Color.Gray
                if (!Ture.HasValue)
                    return Color.Gray;

                // 若輸入為 true，回傳白色；否則回傳黑色
                return Ture.Value ? Color.White : Color.Black;

            }

            private static void PanelLight_Click(object sender, EventArgs e)
            {

                Panel panelLight = sender as Panel;
                if (panelLight != null)
                {
                    string equipmentTag = panelLight.Tag.ToString();  // 從 Tag 屬性中獲取設備名稱
                    ShowDetail detailForm = new ShowDetail(machine_name,equipmentTag);
                    detailForm.FormShowDetailClosed += (s, e) =>
                    {
                        // C 關閉後通知 A
                        NotifyShowdetailToRefresh?.Invoke(detailForm, EventArgs.Empty);
                    };
                    detailForm.ShowDialog();
                }
            }

            private static void ResizeControls(Control parent, float scaleX, float scaleY)
            {
                foreach (Control ctrl in parent.Controls)
                {
                    // 位置與大小縮放
                    ctrl.Location = new Point((int)(ctrl.Left * scaleX), (int)(ctrl.Top * scaleY));
                    ctrl.Size = new Size((int)(ctrl.Width * scaleX), (int)(ctrl.Height * scaleY));

                    // 字型縮放
                    if (ctrl.Font != null)
                    {
                        float newSize = ctrl.Font.Size * Math.Min(scaleX, scaleY);
                        ctrl.Font = new Font(ctrl.Font.FontFamily, newSize, ctrl.Font.Style);
                    }
                    if (ctrl is Label lbl && lbl.MaximumSize != Size.Empty)
                    {
                        int maxWidth = (int)(lbl.MaximumSize.Width * scaleX);
                        int maxHeight = (int)(lbl.MaximumSize.Height * scaleY);
                        lbl.MaximumSize = new Size(maxWidth, maxHeight);
                    }
                    // 遞迴縮放子控制項
                    if (ctrl.HasChildren)
                    {
                        ResizeControls(ctrl, scaleX, scaleY);
                    }
                }
            }
            public static void UpdatePanelData(Panel panel, MachineIO item)
            {
                string percentText = item.RUL.ToString("F2");
                int value = ProgressBarValue(percentText);
                Color lightColor = (Color)SetColor(percentText, item.Setting_green, item.Setting_yellow, item.Setting_red);

                // 更新百分比 Label
                var labelPercent = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "label_percent");
                if (labelPercent != null)
                    labelPercent.Text = percentText + "%";

                // 更新進度條
                var progressBar = panel.Controls.OfType<ProgressBar>().FirstOrDefault(p => p.Name == "RUL_precent");
                if (progressBar != null)
                    progressBar.Value = value;

                // 更新燈號
                var light = panel.Controls.OfType<Panel>().FirstOrDefault(p => p.Name == "panel_light");
                if (light != null)
                {
                    light.BackColor = lightColor;
                }
                // 更新開關狀態
                var panelON = panel.Controls.OfType<Panel>().FirstOrDefault(p => p.Name == "panel_ON");

                if (panelON != null && item.current_single.HasValue)
                {
                    panelON.BackColor = item.current_single.Value ? Color.White : Color.Black;
                }
                var labEffect = panel.Controls.OfType<Label>().FirstOrDefault(p => p.Name == "lab_effect");
                if (labEffect != null)
                    labEffect.Text = item.GetComment(Properties.Settings.Default.LanguageSetting);
            }

        }
    }
}
