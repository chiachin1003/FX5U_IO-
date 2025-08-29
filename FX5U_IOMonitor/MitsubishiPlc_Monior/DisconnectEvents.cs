using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.MitsubishiPlc_Monior
{
    public static class DisconnectEvents
    {
        /// MonitoringService 達到失敗門檻時觸發
        public static event Action<string>? FailureConnect;
        /// 通訊失敗事件
        public static event Action<string, string>? CommunicationFailureOnce;
        public static void RaiseFailureConnect(string machineName)
         => FailureConnect?.Invoke(machineName);
        public static void RaiseCommunicationFailureOnce(string machineName, string message)
        => CommunicationFailureOnce?.Invoke(machineName, message);
    }

  
}
