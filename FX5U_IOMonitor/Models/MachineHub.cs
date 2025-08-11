using FX5U_IOMonitor.Data;
using SLMP;
using CommunicationDriver.Include.Driver;
using static FX5U_IOMonitor.Data.GlobalMachineHub;
using static FX5U_IOMonitor.Models.MonitoringService;

namespace FX5U_IOMonitor.Models
{
    public static class MachineHub
    {
        private static readonly Dictionary<string, MachineContext> machines = new();
       

        /// <summary>
        /// 註冊機台並初始化監控器與連線狀態
        /// </summary>
       
        public static void RegisterMachine(string name, IMitsubishiPlc plc)
        {
            var context = new MachineContext
            {
                MachineName = name,
                plc = plc,
                TokenSource = new CancellationTokenSource(),
                LockObject = new object(),
                Monitor = new MonitorService(plc, name),
                ConnectSummary = new connect_Summary(),
                IsMaster = (name == "Drill")
            };

            machines[name] = context;
        }

        /// <summary>
        /// 移除註冊機台與監控器
        /// </summary>
        public static void UnregisterMachine(string name)
        {
            if (!machines.TryGetValue(name, out var context))
                return;

            try
            {
                // 停止所有任務
                context.TokenSource.Cancel();

                // 關閉 PLC 連線
                context.plc? .ClosePLC(); 

                // 移除 context
                machines.Remove(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ UnregisterMachine 發生錯誤：{ex.Message}");
            }
        }
        /// <summary>
        /// 檢查當前機台是否有被註冊
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HasMachine(string name)
        {
            return machines.ContainsKey(name);  
        }
        /// <summary>
        /// 取得對應名稱的監控服務
        /// </summary>
        public static MonitorService? GetMonitor(string name)
        {
            return machines.TryGetValue(name, out var context) ? context.Monitor : null;
        }

        /// <summary>
        /// 取得對應名稱的 
        /// </summary>
        public static IMitsubishiPlc? GetPlc(string name)
        {
            return machines.TryGetValue(name, out var context) ? context.plc : null;
        }

        /// <summary>
        /// 取得對應名稱的取消 Token
        /// </summary>
        public static CancellationTokenSource? GetToken(string name)
        {
            return machines.TryGetValue(name, out var context) ? context.TokenSource : null;
        }

        /// <summary>
        /// 取得對應名稱的機台連線摘要
        /// </summary>
        public static connect_Summary? GetConnectSummary(string name)
        {
            return machines.TryGetValue(name, out var context) ? context.ConnectSummary : null;
        }

        /// <summary>
        /// 取得完整機台上下文
        /// </summary>
        public static MachineContext? Get(string name)
        {
            return machines.TryGetValue(name, out var context) ? context : null;
        }

        /// <summary>
        /// 取得所有註冊的機台資訊
        /// </summary>
        public static IEnumerable<MachineContext> GetAll() => machines.Values;
    }

    /// <summary>
    /// 每台機台的監控環境與資源統整
    /// </summary>
    public class MachineContext : IMachineContext
    {
        public string MachineName { get; set; } = string.Empty;
        public IMitsubishiPlc? plc { get; set; }

        public CancellationTokenSource TokenSource { get; set; }
        public object LockObject { get; set; } = new object();
        public MonitorService Monitor { get; set; }
        public connect_Summary ConnectSummary { get; set; }
        public bool IsConnected => plc != null;

        public bool IsMaster { get; set; } = false;  // 新增主機標記(產線主機台，鑽床)


    }
}
