using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Models
{
    public class UnitManager
    {
        public static event EventHandler? UnitChanged;

        public static void SetUnit(string newUnit)
        {
            if (Properties.Settings.Default.UnitSetting != newUnit)
            {
                Properties.Settings.Default.UnitSetting = newUnit;
                Properties.Settings.Default.Save();

                // 廣播事件
                UnitChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public static string CurrentUnit => Properties.Settings.Default.UnitSetting;
    }
}
