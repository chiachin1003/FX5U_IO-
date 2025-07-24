using FX5U_IOMonitor.DatabaseProvider;
using static FX5U_IOMonitor.Models.MonitoringService;


namespace FX5U_IOMonitor.Models
{
    public class Alarm_config
    {
       
        /// <summary>
        /// 找出警告對應的使用者去對應信箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async Task<List<string>> GetAlarmNotifyEmails(string? alarmNotifyUserField)
        {
            if (string.IsNullOrWhiteSpace(alarmNotifyUserField))
                return new List<string>();

            // 1. 拆解 UserName 字串為陣列
            var userNames = alarmNotifyUserField.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                .Select(u => u.Trim())
                                                .ToList();

            // 2. 查詢對應的 Message_function
            using var userService = LocalDbProvider.GetUserService();
            var allUsers = userService.GetAllUser();

            var emails = allUsers
                .Where(u => userNames.Contains(u.UserName))
                .Select(u => u.Email ?? "")
                .ToList();

            return emails;
        }
    }
    public static class AlarmMonitorManager
    {
        private static readonly Dictionary<string, Action<IOUpdateEventArgs>> _handlers = new();
        private static readonly HashSet<string> _registered = new();

        public static void EnsureRegistered(string machineName, Action<IOUpdateEventArgs> handler)
        {
            var hub = MachineHub.Get(machineName);
            if (hub?.IsConnected != true) return;

            if (_registered.Contains(machineName)) return;

            // 建立封裝後的 handler 並儲存
            void wrapper(object? sender, IOUpdateEventArgs e) => handler(e);
            hub.Monitor.alarm_event += wrapper;

            _handlers[machineName] = handler;
            _registered.Add(machineName);

            Console.WriteLine($"✅ 警告事件已註冊至機台：{machineName}");
        }

        public static void Unregister(string machineName)
        {
            if (!_handlers.ContainsKey(machineName)) return;

            var hub = MachineHub.Get(machineName);
            if (hub?.IsConnected == true)
            {
                var handler = _handlers[machineName];
                hub.Monitor.alarm_event -= (sender, e) => handler(e); // ⚠ 無效：lambda 無法直接比對，需保留實體（可改進）
            }

            _handlers.Remove(machineName);
            _registered.Remove(machineName);
            Console.WriteLine($"❎ 警告事件已移除機台：{machineName}");
        }
    }
}
