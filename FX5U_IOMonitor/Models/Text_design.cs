using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Models
{
    internal class Text_design
    {
        /// <summary>
        /// 設定combox的字體在正中央
        /// </summary>
        /// <param name="comboBox"></param>
        public static void SetComboBoxCenteredDraw(ComboBox comboBox)
        {
            // 設定 DrawMode 為 OwnerDrawFixed 才能使用自訂繪製
            comboBox.DrawMode = DrawMode.OwnerDrawFixed;

            comboBox.DrawItem += (s, e) =>
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    string text = comboBox.Items[e.Index].ToString();

                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;        // 水平置中
                        sf.LineAlignment = StringAlignment.Center;    // 垂直置中

                        e.Graphics.DrawString(text, comboBox.Font, Brushes.Black, e.Bounds, sf);
                    }
                }

                e.DrawFocusRectangle();
            };
        }

        /// <summary>
        /// 字體調整
        /// </summary>
        /// <param name="label"></param>
        /// <param name="text"></param>
        public static void SafeAdjustFont(Label? label, string text)
        {
            try
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
            catch (ObjectDisposedException)
            {
                // 可寫入 Log 或忽略
            }
        }

        public static void FitFontToLabel(Label label)
        {
            if (string.IsNullOrEmpty(label.Text)) return;

            using (Graphics g = label.CreateGraphics())
            {
                float maxWidth = label.Width - 4; // 留些 padding
                float fontSize = label.Font.Size;
                Font testFont = label.Font;

                while (fontSize > 6) // 最小不要小於6
                {
                    SizeF textSize = g.MeasureString(label.Text, testFont);
                    if (textSize.Width <= maxWidth)
                        break;

                    fontSize -= 0.5f;
                    testFont = new Font(label.Font.FontFamily, fontSize, label.Font.Style);
                }

                label.Font = testFont;
            }
        }

        public static void AdjustFontToFitBox(Label label, string text, float maxFontSize = 14f, float minFontSize = 8f)
        {
            if (label == null || label.IsDisposed || string.IsNullOrEmpty(text))
                return;

            Size clientSize = label.ClientSize;
            float fontSize = maxFontSize;
            Font testFont = label.Font;
            Font? prevFont = null;

            using (Graphics g = label.CreateGraphics())
            {
                while (fontSize >= minFontSize)
                {
                    prevFont?.Dispose();
                    prevFont = testFont;

                    testFont = new Font(label.Font.FontFamily, fontSize, label.Font.Style);
                    SizeF textSize = g.MeasureString(text, testFont);

                    if (textSize.Width <= clientSize.Width - 4 && textSize.Height <= clientSize.Height - 2)
                        break;

                    fontSize -= 0.5f;
                }
            }

            label.Font = testFont;
            prevFont?.Dispose();
        }

        /// <summary>
        /// 根據 Button 大小自動調整字體大小（適用多語系）
        /// </summary>
        /// <param name="button">目標按鈕</param>
        /// <param name="text">要顯示的文字</param>
        public static void SafeAdjustFont(Button? button, string text)
        {
            try
            {
                
                    if (button == null || string.IsNullOrEmpty(text)) return;

                    button.Padding = new Padding(0); // ❗ 避免被 Padding 影響
                    button.FlatStyle = FlatStyle.Standard; // ❗ 避免系統樣式干擾

                    Size proposedSize = button.ClientSize;
                    float baseFontSize = button.Font.Size;
                    float fontSize = baseFontSize;
                    Font? finalFont = null;

                    float marginFactor = 0.9f; // ✅ 加容錯，不用塞到滿

                    while (fontSize > 5f)
                    {
                        using (Font testFont = new Font(button.Font.FontFamily, fontSize, button.Font.Style))
                        {
                            Size textSize = TextRenderer.MeasureText(text, testFont,
                                proposedSize,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);

                            if (textSize.Width <= proposedSize.Width * marginFactor &&
                                textSize.Height <= proposedSize.Height * marginFactor)
                            {
                                finalFont = new Font(testFont.FontFamily, fontSize, testFont.Style);
                                break;
                            }

                            fontSize -= 0.5f;
                        }
                    }

                    // ✅ 不讓字變太小
                    if (finalFont != null && finalFont.Size >= 6f)
                    {
                        button.Font = finalFont;
                    }

                    button.Text = text;
            }
            catch (ObjectDisposedException)
            {
                // 控制項已釋放，忽略
            }
        }
        /// <summary>
        /// 根據設定檔將單位標籤（如 "m/min"）轉換為對應單位（如 "inch/min"）
        /// </summary>
        public static string ConvertUnitLabel(string metricLabel)
        {

            if (UnitManager.CurrentUnit == "Metric")
                return metricLabel;

            return metricLabel switch
            {
                "(mm)" => "(inch)",
                "(mm/min)" => "(inch/min)",
                "(m/min)" => "(feet/min)",
                "(°C)" => "(°F)",
                "(m²)" => "(inch²)",
                _ => metricLabel // 預設不轉換
            };
        }
    }

}
