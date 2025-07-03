using LineNotification.LineNotificationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LineNotificationModule;

namespace LineNotification.NotificationManagerModule
{
    public class LineNotificationManager : INotificationManager
    {
        private readonly ILineNotifyClient _lineClient;

        public LineNotificationManager(ILineNotifyClient lineClient)
        {
            _lineClient = lineClient ?? throw new ArgumentNullException(nameof(lineClient));
        }

        public async Task<bool> SendToSingleUserAsync(string userId, string message)
        {
            return await _lineClient.SendNotificationAsync(userId, message);
        }

        public async Task<bool> SendToMultipleUsersAsync(string[] userIds, string message)
        {
            return await _lineClient.SendBroadcastAsync(userIds, message);
        }
    }
}
