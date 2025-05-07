using FX5U_IOMonitor.Data;
using SLMP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FX5U_IOMonitor.Models.MonitoringService;

namespace FX5U_IOMonitor.Models
{
    public static class MonitorHub 
    {
        // 監控器初始化
        private static Dictionary<string, MonitorService> monitorMap = new();

        //添加對應的監控器
        public static bool AddMonitor(string name, SlmpClient plc)
        {
            if (!monitorMap.ContainsKey(name))
            {
                var monitor = new MonitorService(plc);
                monitorMap[name] = monitor;
                return true;
            }
            return false;
        }

        //取得要監控的元件
        public static MonitorService? GetMonitor(string name)
        {
            return monitorMap.TryGetValue(name, out var monitor) ? monitor : null;
        }
        public static void RemoveMonitor(string name)
        {
            if (monitorMap.TryGetValue(name, out var monitor))
            {
                monitorMap.Remove(name);
            }
        }
    }
}
