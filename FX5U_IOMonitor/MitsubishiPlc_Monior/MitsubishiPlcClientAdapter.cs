using CommunicationDriver.Include.Driver;
using SLMP;

namespace FX5U_IOMonitor.MitsubishiPlc_Monior
{
    public sealed class MitsubishiPlcClientAdapter : IPlcClient
    {
        private readonly IMitsubishiPlc _plc;
        private readonly CSOCKETNET.LINKMODE _mode;
        private readonly string _ip;
        private readonly int _port, _recvTimeout, _sendTimeout, _retry;

        public MitsubishiPlcClientAdapter(
            IMitsubishiPlc plc,
            CSOCKETNET.LINKMODE mode,
            string ip,
            int port,
            int recvTimeout = 3000,
            int sendTimeout = 3000,
            int retry = 1)
        {
            _plc = plc ?? throw new ArgumentNullException(nameof(plc));
            _mode = mode;
            _ip = ip;
            _port = port;
            _recvTimeout = recvTimeout;
            _sendTimeout = sendTimeout;
            _retry = retry;
        }

       
        public bool IsConnected => _plc.IsConnect();

        // ✅ 無參數 Connect()，用建構子存好的設定呼叫 CreatePLC
        public bool Connect()
        {
            if (!_plc.IsConnect())
            {
                int rc = _plc.CreatePLC(_mode, _ip, _port, _recvTimeout, _sendTimeout, _retry);
                if (rc < 0)
                {
                    // 失敗時不要直接 throw，而是回傳 false，讓呼叫者自行決定怎麼做
                    Console.WriteLine($"[警告] 建立 PLC 連線失敗 (rc={rc})");
                    return false;
                }
            }

            return true;
        }

        public void Disconnect() => _plc.ClosePLC();
        public void Dispose() { try { _plc?.ClosePLC(); } catch { } }

        // ---- 以下照你現有 IMitsubishiPlc 的 API 做包裝 ----
        public bool[] ReadBits(string tag, int bitCount)
        {

            if (bitCount <= 0) return Array.Empty<bool>();
            var rc = _plc.ReadBitsPLC(out var bits, tag, bitCount);
            if (rc < 0) throw new IOException($"ReadBitsPLC failed, rc={rc}");
            return bits;
        }

        public ushort[] ReadWords(string tag, int wordCount)
        {
            if (wordCount <= 0) return Array.Empty<ushort>();
            short[] buf = new short[wordCount];
            var rc = _plc.ReadBitDevice(ref buf, tag, wordCount, alignBitHead: true);
            if (rc < 0) throw new IOException($"ReadBitDevice failed, rc={rc}");

            // short[] -> ushort[]（不改實際位元）
            var u = new ushort[wordCount];
            Buffer.BlockCopy(buf, 0, u, 0, wordCount * sizeof(short));
            return u;
        }

        public void WriteWord(string tag, ushort value) => WriteWords(tag, new ushort[] { value });

        public void WriteWords(string tag, ushort[] values)
        {
            if (values == null || values.Length == 0) return;
            var s = new short[values.Length];
            Buffer.BlockCopy(values, 0, s, 0, values.Length * sizeof(ushort));
            var rc = _plc.WriteBatchPLC(s, tag, values.Length, alignBitHead: true);
            if (rc < 0) throw new IOException($"WriteBatchPLC failed, rc={rc}");
        }
    }
}
