using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineNotification.NotificationManagerModule
{
    public interface INotificationManager
    {
        Task<bool> SendToSingleUserAsync(string userId, string message);
        Task<bool> SendToMultipleUsersAsync(string[] userIds, string message);
    }
}
