using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Message;
using FX5U_IOMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FX5U_IOMonitor.Data.GlobalMachineHub;
using static FX5U_IOMonitor.Models.MonitoringService;

namespace FX5U_IOMonitor.MitsubishiPlc_Monior
{
    public sealed class MachineSupervisor
    {
        private readonly string _machineName;
        private readonly object _lock = new();
        private readonly SemaphoreSlim _reconnectGate = new(1, 1);

        private CancellationTokenSource? _machineCts;      // 控制「這台機台所有子任務」
        private CancellationTokenSource? _monitorsCts;     // 控制「監控任務群」
        private Task? _supervisorTask;                     // 單一監督迴圈
        private bool _started;
        public event EventHandler<RULThresholdCrossedEventArgs>? RULThresholdCrossed;
        public MachineSupervisor(string machineName)
        {
            _machineName = machineName;
        }

        public void Start()
        {
            lock (_lock)
            {
                if (_started) return;
                _started = true;

                _machineCts = new CancellationTokenSource();
                _supervisorTask = Task.Run(() => SupervisorLoopAsync(_machineName, _machineCts.Token));
            }
        }

        public async Task StopAsync()
        {
            lock (_lock)
            {
                if (!_started) return;
                _started = false;
                _machineCts?.Cancel();
            }

            if (_supervisorTask != null)
            {
                try { await _supervisorTask; } catch { /* ignore */ }
            }

            CancelMonitors(); // 收掉監控子任務
        }

        private async Task SupervisorLoopAsync(string machineName, CancellationToken token)
        {
            // 「唯一」的監督循環：負責連線→註冊→啟動監控；斷線→整包重來
            while (!token.IsCancellationRequested)
            {
                try
                {
                    // 1) 連線 + 註冊（具備重試與節流）
                    var ok = await EnsureConnectedAndRegisteredAsync(machineName, token);
                    if (!ok)
                    {
                        await Task.Delay(1000, token); // 稍等再重試
                        continue;
                    }

                    // 2) 啟動監控任務（只在連線確認後）
                    StartMonitorTasks(machineName, token);

                    // 3) 等待到「任何一個」條件發生：斷線、例外、或外部取消
                    await WaitForDisconnectOrCancelAsync(machineName, token);

                    // 落到這裡代表：斷線 or 要重啟 → 整包取消監控任務，然後進到下一輪重連
                    CancelMonitors();
                }
                catch (OperationCanceledException) { break; }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[Supervisor] {_machineName} 例外：{ex.Message}");
                    CancelMonitors();
                    await Task.Delay(1000, token);
                }
            }
        }

        private async Task<bool> EnsureConnectedAndRegisteredAsync(string machineName, CancellationToken token)
        {
            await _reconnectGate.WaitAsync(token);
            try
            {
                // 檢查是否已經有註冊且可用
                var ctx = MachineHub.Get(machineName);
                if (ctx != null && ctx.IsConnected) return true;

                // 讀 DB 配置
                using var db = new ApplicationDB();
                var m = db.Machine.FirstOrDefault(x => x.Name == machineName);
                if (m == null || string.IsNullOrWhiteSpace(m.IP_address) || m.Port <= 0) return false;

                // 建立 PLC client
                var plc = PlcClientFactory.CreateByFrame(m.MC_Type, m.IP_address, m.Port);

                // 連線（可加上 timeout / 重試）
                var isconnect = plc.Connect();
                if (!isconnect)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ {machineName} 無法連線");
                    return false;
                }

                // 註冊 MachineHub（idempotent：先 Unregister 再 Register）
                MachineHub.UnregisterMachine(machineName);
                MachineHub.RegisterMachine(machineName, plc);

                // 驗證註冊結果
                var contextItem = MachineHub.Get(machineName);
                if (contextItem == null || !contextItem.IsConnected)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ {machineName} 註冊失敗");
                    MachineHub.UnregisterMachine(machineName);
                    return false;
                }

                // 綁一次性的事件（避免重複 +=）
                //WireUpEventsOnce(contextItem);

                System.Diagnostics.Debug.WriteLine($"✅ 連線並註冊 {machineName} 成功");
                return true;
            }
            finally
            {
                _reconnectGate.Release();
            }
        }

        private void StartMonitorTasks(string machineName, CancellationToken token)
        {
            var ctx = MachineHub.Get(machineName);
            if (ctx == null || !ctx.IsConnected) return;

            // 每輪監控任務都有自己的 CTS，方便在斷線時整包取消
            _monitorsCts = CancellationTokenSource.CreateLinkedTokenSource(token);
            var mtoken = _monitorsCts.Token;

            // 設外部鎖
            ctx.Monitor.SetExternalLock(ctx.LockObject);

            // 啟動你的各種監控任務（只在此時啟動）
            _ = Task.Run(() => ctx.Monitor.MonitoringLoop(mtoken, ctx.MachineName), mtoken);

            var notifier = new RULNotifier();
            // （事件在 WireUpEventsOnce 做一次性綁定，避免重複）

            if (ctx.IsMaster)
            {
                DBfunction.Fix_UnclosedAlarms_ByCurrentState();
                _ = Task.Run(() => ctx.Monitor.alarm_MonitoringLoop(mtoken), mtoken);
            }

            int[] writemodes = DBfunction.Get_Machine_Calculate_type(ctx.MachineName);
            int[] read_modes = DBfunction.Get_Machine_Readview_type(ctx.MachineName);

            _ = Task.Run(() => ctx.Monitor.Read_Bit_Monitor_AllModesAsync(ctx.MachineName, writemodes, mtoken), mtoken);
            _ = Task.Run(() => ctx.Monitor.Read_Word_Monitor_AllModesAsync(ctx.MachineName, read_modes, mtoken), mtoken);
            _ = Task.Run(() => ctx.Monitor.Read_None_Monitor_AllModesAsync(ctx.MachineName, mtoken), mtoken);
            _ = Task.Run(() => ctx.Monitor.Write_Word_Monitor_AllModesAsync(ctx.MachineName, writemodes, mtoken), mtoken);

            ctx.Monitor.IOUpdated += DB_update_change;
        }

        private void CancelMonitors()
        {
            try
            {
                _monitorsCts?.Cancel();
            }
            catch { /* ignore */ }
            finally
            {
                _monitorsCts?.Dispose();
                _monitorsCts = null;
            }
        }

        /// <summary>
        /// 等待「斷線」或「取消」的訊號。
        /// 作法：週期性檢查 PLC 連線 or 由 Machine_context 回報的 Disconnected。
        /// </summary>
        private async Task WaitForDisconnectOrCancelAsync(string machineName, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var ctx = MachineHub.Get(machineName);
                if (ctx == null || !ctx.IsConnected)
                {
                    System.Diagnostics.Debug.WriteLine($"⚠ {_machineName} 偵測到斷線，準備重啟監控。");
                    return;
                }
                await Task.Delay(500, token);
            }
        }

      
        private static void DB_update_change(object? sender, IOUpdateEventArgs e)
        {
            Task.Run(() => ProcessIOUpdate(sender, e));
        }

        private static void ProcessIOUpdate(object? sender, IOUpdateEventArgs e)
        {
            try
            {
                string? datatable = sender switch
                {
                    MonitorService slmp => slmp.MachineName,
                    ModbusMonitorService modbus => modbus.MachineName,
                    _ => null
                };

                if (string.IsNullOrWhiteSpace(datatable))
                    return;

                int number = DBfunction.Get_use_ByAddress(datatable, e.Address);
                if (number < 0)
                {
                    DBfunction.Set_use_ByAddress(datatable, e.Address, 0);
                }
                else
                {
                    DBfunction.Set_use_ByAddress(datatable, e.Address, number + 1);
                }

                DBfunction.Set_current_single_ByAddress(datatable, e.Address, e.NewValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Monitor_DBuse_Updated 發生例外：{ex.Message}");
            }
        }

        private void FailureAlertMail(object? sender, IOUpdateEventArgs e)
        {
           
            try
            {
                string? datatable = sender switch
                {
                    MonitorService slmp => slmp.MachineName,
                    ModbusMonitorService modbus => modbus.MachineName,
                    _ => null
                };

                if (string.IsNullOrWhiteSpace(datatable))
                {
                    return;
                }

                DBfunction.Set_alarm_current_single_ByAddress(e.Address, e.NewValue);
                //MessageBox.Show($"📡 偵測到 I/O 變化：{e.Address} from {e.OldValue} ➜ {e.NewValue}");

                if (e.NewValue == true)
                {
                    // 根據是否有額外數值來決定呼叫哪個資料庫函數
                    if (e.AdditionalValue.HasValue && !string.IsNullOrEmpty(e.AdditionalAddress))
                    {
                        // 呼叫帶有額外數值的函數
                        DBfunction.Set_Alarm_Note_ByAddress(e.Address, e.AdditionalValue.Value.ToString());
                    }
                    else
                    {
                        // 原本的函數
                        DBfunction.Set_Alarm_StartTimeByAddress(e.Address);
                    }
                

                }
                else
                {
                    DBfunction.Set_Alarm_EndTimeByAddress(e.Address);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Monitor_DBuse_Updated 發生例外：{ex.Message}");
            }

        }
    }

}
