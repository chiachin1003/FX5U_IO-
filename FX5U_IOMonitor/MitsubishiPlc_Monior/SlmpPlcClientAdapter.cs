using CommunicationDriver.Include.Driver;
using SLMP;
using static MCProtocol.Mitsubishi;

namespace FX5U_IOMonitor.MitsubishiPlc_Monior
{
    public sealed class SlmpPlcClientAdapter : IPlcClient
    {

        private readonly object _sync = new();
        private readonly string _ip;
        private readonly int _port;
        private readonly int _connTimeout;

        private SlmpClient? slmp_plc;
        private bool _disposed;



        public SlmpPlcClientAdapter(string ip, int port, int timeoutMs = 3000)
        {
            _ip = ip;
            _port = port;
            _connTimeout = timeoutMs;
            slmp_plc = CreateClient();
            SlmpConfig cfg = new SlmpConfig(ip, port);
            cfg.ConnTimeout = timeoutMs ;
            this.slmp_plc = new SlmpClient(cfg);

        }
        public bool IsConnected => slmp_plc.IsConnected();
        public bool Connect()
        {
            // 防呆：物件不存在時，重建一支
            if (slmp_plc == null)
            {
                slmp_plc = CreateClient();
            }

            // 如果已經連線，就直接回傳 true
            if (slmp_plc.IsConnected())
            {
                return true;
            }

            try
            {
                slmp_plc = CreateClient();   
                slmp_plc.Connect();

                return slmp_plc.IsConnected(); // 成功回 true，否則 false
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[警告] PLC 連線失敗：{ex.Message}");
                return false;
            }
        }
        private SlmpClient CreateClient()
        {
            var cfg = new SlmpConfig(_ip, _port)
            {
                ConnTimeout = _connTimeout,
                RecvTimeout = _connTimeout,
                SendTimeout = _connTimeout
            };
            return new SlmpClient(cfg);
        }
        public void Disconnect() => slmp_plc.Disconnect();

        public bool[] ReadBits(string address, int bitCount) 
        {
            
            return slmp_plc.ReadBitDevice(address, (ushort)bitCount);
        
        }
        public ushort[] ReadWords(string address, int wordCount) 
        {
            
            return slmp_plc.ReadWordDevice(address, (ushort)wordCount);
        }
        public ushort ReadWords(string address)
        {

            return slmp_plc.ReadWordDevice(address);
        }
        public void WriteWord(string address, ushort value) 
        {

            slmp_plc.WriteWordDevice(address, value);
        }
        public void WriteWords(string address, ushort[] values)
        {
            slmp_plc.WriteWordDevice(address, values);
        }

        public void Dispose() { try { slmp_plc?.Disconnect(); } catch { } }
    }
}
