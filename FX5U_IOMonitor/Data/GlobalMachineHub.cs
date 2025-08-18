using FX5U_IOMonitor.MitsubishiPlc_Monior;
using FX5U_IOMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FX5U_IOMonitor.Data
{
    public static class GlobalMachineHub
    {
        /// <summary>
        /// 統一取得監控服務（可為 SLMP 或 Modbus）
        /// </summary>
        public static object? GetMonitor(string machineName)
        {
            return MachineHub.GetMonitor(machineName) as object
                ?? ModbusMachineHub.GetModbusMonitor(machineName);
        }

        /// <summary>
        /// 統一取得機台上下文（可為 MachineContext 或 ModbusMachineContext）
        /// </summary>
        public static object? GetContext(string machineName)
        {
            return MachineHub.Get(machineName) as object
                ?? ModbusMachineHub.Get(machineName);
        }

        /// <summary>
        /// 判斷是否為 Modbus 機台
        /// </summary>
        public static bool IsModbus(string machineName)
        {
            return ModbusMachineHub.Get(machineName) != null;
        }

        /// <summary>
        /// 判斷是否為 SLMP 機台
        /// </summary>
        public static bool IsSLMP(string machineName)
        {
            return MachineHub.Get(machineName) != null;
        }

        /// <summary>
        /// 判斷是否已註冊任一類型
        /// </summary>
        public static bool Exists(string machineName)
        {
            return IsModbus(machineName) || IsSLMP(machineName);
        }
        public interface IMachineContext
        {
            string MachineName { get; }
            bool IsConnected { get; }
            connect_Summary ConnectSummary { get; }
        }

    }
}
