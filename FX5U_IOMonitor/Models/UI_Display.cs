using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Models
{
    public class UI_Display
    {
        public class DisplayValuePair<T>
        {
            public string Display { get; set; }    // 顯示在 UI 上的名稱
            public T Value { get; set; }           // 程式邏輯用的值（可為 int, enum, object）

            public DisplayValuePair(string display, T value)
            {
                Display = display;
                Value = value;
            }

            public override string ToString() => Display; // ComboBox 自動顯示這個
        }
    }
}
