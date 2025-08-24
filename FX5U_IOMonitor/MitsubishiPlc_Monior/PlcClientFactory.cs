using CommunicationDriver.Include.Driver;
using SLMP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.MitsubishiPlc_Monior
{
    public enum MitsubishiFrame { MC1E, MC3E }
    internal class PlcClientFactory
    {
        // 供 UI 傳字串時使用（"MC1E" / "MC3E" 不分大小寫）
        public static IPlcClient CreateByFrame(
            string frame,
            string ip,
            int port,
            CSOCKETNET.LINKMODE mode = CSOCKETNET.LINKMODE.TCP,
            int recvTimeout = 3000,
            int sendTimeout = 3000,
            int retry = 1)
        {
            if (!Enum.TryParse<MitsubishiFrame>(frame, ignoreCase: true, out var f))
                throw new ArgumentException($"未知的 Frame: {frame}");

            return CreateByFrame(f, ip, port, mode, recvTimeout, sendTimeout, retry);
        }

        // 安全創建
        public static IPlcClient CreateByFrame(
            MitsubishiFrame frame,
            string ip,
            int port,
            CSOCKETNET.LINKMODE mode = CSOCKETNET.LINKMODE.TCP,
            int recvTimeout = 3000,
            int sendTimeout = 3000,
            int retry = 1)
        {
            switch (frame)
            {
                case MitsubishiFrame.MC3E:
                    {
                        return new SlmpPlcClientAdapter(ip, port); 
                    }

                case MitsubishiFrame.MC1E:
                    {
                        IMitsubishiPlc impl = new CMITSUBISHIPLC1E();
                        return new MitsubishiPlcClientAdapter(impl, mode, ip, port, recvTimeout, sendTimeout, retry);
                    }

                default:
                    throw new NotSupportedException($"未支援的 Frame: {frame}");
            }
        }
    }
}
