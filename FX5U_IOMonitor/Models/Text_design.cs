using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Models
{
    internal class Text_design
    {
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
    }

}
