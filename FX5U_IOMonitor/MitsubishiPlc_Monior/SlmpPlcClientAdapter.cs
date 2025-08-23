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

        private readonly SlmpClient _plc;



        public SlmpPlcClientAdapter(string ip, int port, int timeoutMs = 3000)
        {
            _ip = ip;
            _port = port;
            _connTimeout = timeoutMs;
            SlmpConfig cfg = new SlmpConfig(ip, port);
            cfg.ConnTimeout = timeoutMs;
            this._plc = new SlmpClient(cfg);

        }
        public bool IsConnected => _plc.IsConnected();
        public bool Connect()
        {
            if (!_plc.IsConnected())
            {
                try
                {
                    _plc.Connect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[警告] PLC 連線失敗：{ex.Message}");
                    return false;
                }
            }

            // 最後回傳是否已連線
            return _plc.IsConnected();
        }
       
        public void Disconnect() => _plc.Disconnect();

        public bool[] ReadBits(string address, int bitCount) 
        {
            
            return _plc.ReadBitDevice(address, (ushort)bitCount);
        
        }
        public ushort[] ReadWords(string address, int wordCount) 
        {
            
            return _plc.ReadWordDevice(address, (ushort)wordCount);
        }
        public ushort ReadWords(string address)
        {

            return _plc.ReadWordDevice(address);
        }
        public void WriteWord(string address, ushort value) 
        {

            _plc.WriteWordDevice(address, value);
        }
        public void WriteWords(string address, ushort[] values)
        {
            _plc.WriteWordDevice(address, values);
        }

        public void Dispose() { try { _plc?.Disconnect(); } catch { } }
    }
}
