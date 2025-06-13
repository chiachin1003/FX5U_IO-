using FX5U_IOMonitor.Data;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static FX5U_IOMonitor.Models.MonitoringService;

namespace FX5U_IOMonitor.Models
{
    public class ModbusMonitorService
    {
        public event EventHandler<IOUpdateEventArgs>? IOUpdated;
        public string MachineName { get; }
        public event EventHandler<MachineParameterChangedEventArgs>? MachineParameterChanged;

        private IModbusSerialMaster master;
        private byte slaveId;
        private object? externalLock;
        private bool isFirstRead = true;

        public void SetExternalLock(object locker)
        {
            this.externalLock = locker;
        }

        public ModbusMonitorService(IModbusSerialMaster master, byte slaveId, string machineName)
        {
            this.master = master;
            this.slaveId = slaveId;
            this.MachineName = machineName;
        }

        public async Task MonitoringLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Monitoring();
                await Task.Delay(100); // 100 ms 間隔
            }
        }

        public void Monitoring()
        {
            List<now_single> old_single = DBfunction.Get_Machine_current_single_all(MachineName);
            var blocks = Calculate.AnalyzeIOSections(MachineName, "oct");
            var sectionGroups = blocks
                .GroupBy(s => s.Prefix)
                .ToDictionary(g => g.Key, g => Calculate.IOBlockUtils.ExpandToBlockRanges(g.First()));
            Stopwatch stopwatch = Stopwatch.StartNew();

            lock (externalLock ?? new object())
            {
                foreach (var prefix in sectionGroups.Keys)
                {
                    foreach (var block in sectionGroups[prefix])
                    {
                        try
                        {
                            ushort startAddr = (ushort)(block.Start);
                            ushort count = 256;

                            bool[] data = prefix switch
                            {
                                "X" => master.ReadInputs(slaveId, startAddr, count),
                                "Y" => master.ReadCoils(slaveId, startAddr, count),
                                _ => throw new NotSupportedException($"不支援的前綴: {prefix}")
                            };

                            var result = Calculate.ConvertPlcToNowSingle(data, prefix, block.Start, "oct");
                            if (isFirstRead)
                            {
                                int updated = Calculate.UpdateIOCurrentSingleToDB(result, MachineName);
                                var ctx = ModbusMachineHub.Get(MachineName);
                                if (ctx != null)
                                    ctx.ConnectSummary.connect += updated;
                            }
                            else
                            {
                                UpdateIODataBaseFromNowSingle(result, old_single);
                            }
                        }
                        catch
                        {
                            isFirstRead = true;
                            return;
                        }
                    }
                }
                stopwatch.Stop();
                var monitor = ModbusMachineHub.Get(MachineName);
                if (monitor != null)
                {
                    monitor.ConnectSummary.read_time = $" {stopwatch.ElapsedMilliseconds} ";

                    monitor.ConnectSummary.disconnect = DBfunction.GetMachineRowCount(MachineName) - monitor.ConnectSummary.connect;
                }
              
            }
            isFirstRead = false;
        }
        private void UpdateIODataBaseFromNowSingle(List<now_single> nowList, List<now_single> oldList)
        {

            if (nowList == null || nowList.Count == 0 || oldList == null || oldList.Count == 0)
            {
                Console.WriteLine("Error: nowList 或 ioList 為空.");
            }

            int updatedCount = 0;
            foreach (var now in nowList)
            {
                var ioMatch = oldList.FirstOrDefault(io => io.address == now.address);
                if (ioMatch != null)
                {

                    if (ioMatch.current_single is bool oldVal)
                    {
                        bool newVal = now.current_single;

                        if (oldVal != newVal)
                        {
                            ioMatch.current_single = newVal;
                            updatedCount++;

                            IOUpdated?.Invoke(this, new IOUpdateEventArgs
                            {
                                Address = now.address,
                                OldValue = oldVal,
                                NewValue = newVal
                            });

                        }
                    }
                }

            }

        }
        
    }
}
