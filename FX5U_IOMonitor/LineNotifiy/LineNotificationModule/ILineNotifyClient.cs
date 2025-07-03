using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineNotification.LineNotificationModule
{
    public interface ILineNotifyClient
    {
        Task<bool> SendNotificationAsync(string userId, string message);
        Task<bool> SendBroadcastAsync(string[] userIds, string message);
    }
}
