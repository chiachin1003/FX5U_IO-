using FX5U_IOMonitor.Data;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FX5U_IOMonitor.Data.GlobalMachineHub;

namespace FX5U_IOMonitor.Models
{
    public class ModbusMachineHub
    {
        private static readonly Dictionary<string, ModbusMachineContext> machines = new();

        public static void RegisterModbusMachine(string name, IModbusSerialMaster master, byte slaveId)
        {
            var context = new ModbusMachineContext
            {
                MachineName = name,
                ModbusMaster = master,
                TokenSource = new CancellationTokenSource(),
                LockObject = new object(),
                ModbusMonitor = new ModbusMonitorService(master, slaveId, name),
                ConnectSummary = new connect_Summary(),
                IsMaster = (name == "Drill")
            };

            machines[name] = context;
        }
        public static void UnregisterModbusMachine(string name)
        {
            if (machines.TryGetValue(name, out var context))
            {
                try
                {
                    // 停止監控迴圈（取消 token）
                    context.TokenSource.Cancel();

                    // 關閉通訊埠（如需）
                    context.ModbusMaster?.Transport?.Dispose();  // 或主動斷線

                    // 移除該機台
                    machines.Remove(name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ 無法移除 {name} 的 Modbus 監控：{ex.Message}");
                }
            }
        }
        public static ModbusMonitorService? GetModbusMonitor(string name)
        {
            return machines.TryGetValue(name, out var context) ? context.ModbusMonitor : null;
        }

        public static ModbusMachineContext? Get(string name)
        {
            return machines.TryGetValue(name, out var context) ? context : null;
        }
    }

    public class ModbusMachineContext : IMachineContext
    {
        public string MachineName { get; set; } = string.Empty;
        public IModbusSerialMaster? ModbusMaster { get; set; }
        public CancellationTokenSource TokenSource { get; set; }
        public object LockObject { get; set; } = new object();
        public ModbusMonitorService? ModbusMonitor { get; set; }
        public connect_Summary ConnectSummary { get; set; }
        public bool IsConnected => ModbusMaster != null;
        public bool IsMaster { get; set; } = false;
    }
}

