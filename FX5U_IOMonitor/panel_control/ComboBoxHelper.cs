using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.panel_control
{
    public static class ComboBoxHelper
    {
        /// <summary>
        /// 設定 ComboBox 的選項來源
        /// </summary>
        /// <param name="comboBox">要綁定的 ComboBox</param>
        /// <param name="items">顯示與實際值的對應</param>
        /// <param name="defaultIndex">預設選擇索引</param>
        public static void BindDisplayValueItems<T>(
        ComboBox comboBox,
        IEnumerable<(string Display, T Value)> items,
        int defaultIndex = 0)
        {
            var data = items.Select(i => new { Text = i.Display, Value = i.Value }).ToList();

            comboBox.DataSource = data;
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = defaultIndex;
        }

        /// <summary>
        /// 取得目前選中的實際值（Value）
        /// </summary>
        public static T? GetSelectedValue<T>(ComboBox comboBox)
        {
            try
            {
                if (comboBox.SelectedValue == null || comboBox.SelectedValue == DBNull.Value)
                    return default;

                return (T)Convert.ChangeType(comboBox.SelectedValue, typeof(T));
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 取得目前顯示文字（Text）
        /// </summary>
        public static string? GetSelectedText(ComboBox comboBox)
        {
            return comboBox.Text;
        }
    }
}
