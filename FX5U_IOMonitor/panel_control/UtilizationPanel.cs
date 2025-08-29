using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FX5U_IOMonitor.panel_control
{
    [DefaultProperty(nameof(CurrentValue))]
    [ToolboxItem(true)]
    public partial class UtilizationPanel : UserControl
    {
        public UtilizationPanel()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            BackColor = Color.Transparent;
            ForeColor = Color.Black;
            Font = new Font("Microsoft JhengHei", 10f, FontStyle.Bold);
        }

        // --------- 可設屬性 ---------
        private float _currentValue = 30f;
        private float _totalValue = 100f;
        private string _centerText = "Equipment";

        private Color _usedColor = Color.Green;
        private Color _remainingColor = Color.LightGray;

        private float _startAngle = -90f; // 從上方開始
        private float _holeRatio = 0.55f; // 0~0.9
        private int _ringPadding = 5;

        private float _centerTextFontSize = 36f;


        [Category("Data"), Description("已使用值")]
        public float CurrentValue
        {
            get => _currentValue;
            set { _currentValue = Math.Max(0, value); Invalidate(); }
        }

        [Category("Data"), Description("總量（分母）")]
        public float TotalValue
        {
            get => _totalValue;
            set { _totalValue = Math.Max(0.0001f, value); Invalidate(); }
        }

        [Category("Appearance"), Description("中心文字")]
        public string CenterText
        {
            get => _centerText;
            set { _centerText = value ?? string.Empty; Invalidate(); }
        }

        [Category("Appearance"), Description("已使用區塊顏色")]
        public Color UsedColor
        {
            get => _usedColor;
            set { _usedColor = value; Invalidate(); }
        }

        [Category("Appearance"), Description("剩餘區塊顏色")]
        public Color RemainingColor
        {
            get => _remainingColor;
            set { _remainingColor = value; Invalidate(); }
        }

        [Category("Appearance"), Description("圓環起始角度（度）")]
        [DefaultValue(-90f)]
        public float StartAngle
        {
            get => _startAngle;
            set { _startAngle = value; Invalidate(); }
        }

       
        [Category("Layout"), Description("外圈與邊界的內距像素")]
        [DefaultValue(5)]
        public int RingPadding
        {
            get => _ringPadding;
            set { _ringPadding = Math.Max(0, value); Invalidate(); }
        }
        // ===== 新增欄位 =====
        private float _middleRingRatio = 0.68f;      // 必須大於 HoleRatio，建議 0.60~0.75
        private Color _middleRingColor = Color.White;

        // ===== 既有 HoleRatio 的 setter 建議改成確保 < MiddleRingRatio =====
        [Category("Appearance"), Description("內圈孔洞比例（0~0.9），必須小於 MiddleRingRatio")]
        [DefaultValue(0.55f)]
        public float HoleRatio
        {
            get => _holeRatio;
            set
            {
                // 內孔必須比白色中環再小一點（留 0.02 的安全間距）
                float maxAllowed = Math.Min(0.88f, _middleRingRatio - 0.02f);
                if (float.IsNaN(value)) value = 0f;
                _holeRatio = Math.Max(0f, Math.Min(maxAllowed, value));
                Invalidate();
            }
        }

        // ===== 新增可設定的白色圓圈屬性 =====
        [Category("Appearance"), Description("中間白色圓圈外徑比例（0~0.9），需大於 HoleRatio")]
        [DefaultValue(0.68f)]
        public float MiddleRingRatio
        {
            get => _middleRingRatio;
            set
            {
                if (float.IsNaN(value)) value = 0.68f;
                // 確保中環比內孔大、且不超過 0.9
                float minAllowed = Math.Min(0.88f, _holeRatio + 0.02f);
                _middleRingRatio = Math.Max(minAllowed, Math.Min(0.9f, value));
                Invalidate();
            }
        }
        [Category("Appearance"), Description("中間白色圓圈顏色")]
        public Color MiddleRingColor
        {
            get => _middleRingColor;
            set { _middleRingColor = value; Invalidate(); }
        }
        /// <summary>一次設定使用值/總值與（可選）中心文字。</summary>
        public void SetValues(float used, float total, string centerText = null)
        {
            _currentValue = Math.Max(0, used);
            _totalValue = Math.Max(0.0001f, total);
            if (centerText != null) _centerText = centerText;
            Invalidate();
        }
        [Category("Appearance"), Description("中心文字字級（pt）")]
        [DefaultValue(36f)]
        public float CenterTextFontSize
        {
            get => _centerTextFontSize;
            set { _centerTextFontSize = Math.Max(1f, value); Invalidate(); }
        }
        // 新增這個重載：一次設定數值＋樣式（顏色/比例）
        public void SetValues(
            float used,
            float total,
            string centerText = null,
            Color? usedColor = null,
            Color? remainingColor = null,
            Color? middleRingColor = null,
            float? middleRingRatio = null,
            float? holeRatio = null, float? centerTextFontSize = null)
        {
            _currentValue = Math.Max(0, used);
            _totalValue = Math.Max(0.0001f, total);
            if (centerText != null) _centerText = centerText;

            // 透過屬性設定，沿用內建的合法化/重繪邏輯
            if (usedColor.HasValue) this.UsedColor = usedColor.Value;
            if (remainingColor.HasValue) this.RemainingColor = remainingColor.Value;
            if (middleRingColor.HasValue) this.MiddleRingColor = middleRingColor.Value;

            // 順序：先設 MiddleRingRatio 再設 HoleRatio，避免邏輯衝突
            if (middleRingRatio.HasValue) this.MiddleRingRatio = middleRingRatio.Value;
            if (holeRatio.HasValue) this.HoleRatio = holeRatio.Value;
            if (centerTextFontSize.HasValue) this.CenterTextFontSize = Math.Max(1f, centerTextFontSize.Value);

            Invalidate();
        }
        // 繪製甜甜圈
        // ===== OnPaint：改成呼叫新的 DrawDoughnutChart (多兩個參數) =====
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            var bounds = ClientRectangle;
            if (bounds.Width <= 2 || bounds.Height <= 2) return;

            // 依指定字級建立中心文字的字型（和控制項 Font 同字族/樣式）
            using var centerFont = new Font(this.Font.FontFamily, _centerTextFontSize, this.Font.Style, GraphicsUnit.Point);

            DrawDoughnutChart(
                g, bounds,
                _currentValue, _totalValue,
                _usedColor, _remainingColor,
                _startAngle,
                _middleRingRatio,
                _holeRatio,
                _ringPadding,
                _centerText, centerFont, this.ForeColor,
                this.Parent?.BackColor ?? this.BackColor,
                _middleRingColor
            );
        }

        private static void DrawDoughnutChart(
            Graphics g, Rectangle bounds,
            float used, float total,
            Color usedColor, Color remainingColor,
            float startAngleDeg,
            float middleRingRatio,     // 新：白色中環外徑比例（相對於外框）
            float holeRatio,           // 既有：最內圈（文字區）外徑比例
            int padding,
            string centerText, Font font, Color textColor,
            Color innerFillColor,      // 內孔填滿色（通常取父容器背景）
            Color middleRingColor      // 新：白色中環顏色
        )
        {
            if (total <= 0.0001f) total = 0.0001f;
            used = Math.Max(0, Math.Min(used, total));
            float remaining = total - used;

            // 外框取正方形
            int size = Math.Min(bounds.Width, bounds.Height);
            var square = new Rectangle(
                bounds.X + (bounds.Width - size) / 2 + padding,
                bounds.Y + (bounds.Height - size) / 2 + padding,
                size - padding * 2, size - padding * 2);

            // 角度
            float usedSweep = (used / total) * 360f;
            float remainSweep = 360f - usedSweep;

            // 畫外圈進度（完整圓），稍後會被中間的圓覆蓋一部分形成環
            using (var br = new SolidBrush(remainingColor))
                g.FillPie(br, square, startAngleDeg, remainSweep);
            using (var br = new SolidBrush(usedColor))
                g.FillPie(br, square, startAngleDeg + remainSweep, usedSweep);

            // ---- 白色中環（外徑）----
            // 先畫一顆「白色實心圓」來覆蓋外圈的內半徑，剩下外側就是第一圈進度環
            int midInset = (int)(square.Width * Math.Max(0f, Math.Min(0.9f, middleRingRatio)));
            midInset = Math.Max(0, Math.Min(square.Width / 2 - 1, midInset));
            var middleOuterRect = new Rectangle(
                square.X + midInset, square.Y + midInset,
                square.Width - 2 * midInset, square.Height - 2 * midInset);

            using (var br = new SolidBrush(middleRingColor))
                g.FillEllipse(br, middleOuterRect);

            // ---- 內孔（文字區）----
            int holeInset = (int)(square.Width * Math.Max(0f, Math.Min(0.9f, holeRatio)));
            holeInset = Math.Max(0, Math.Min(square.Width / 2 - 1, holeInset));
            // 保底：確保內孔小於白色中環外徑
            holeInset = Math.Min(holeInset, Math.Max(0, midInset - 1));

            var innerRect = new Rectangle(
                square.X + holeInset, square.Y + holeInset,
                square.Width - 2 * holeInset, square.Height - 2 * holeInset);

            // 挖內孔（讓中環變成一圈）
            using (var br = new SolidBrush(innerFillColor))
                g.FillEllipse(br, innerRect);

            // 文字置中畫在內孔範圍
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (var brText = new SolidBrush(textColor))
            {
                g.DrawString(centerText ?? string.Empty, font, brText, innerRect, sf);
            }
        }
    }
}
