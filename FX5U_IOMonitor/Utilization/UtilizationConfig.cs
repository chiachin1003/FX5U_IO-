using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Utilization
{
    public class UtilizationConfig
    {
        public string Machine { get; set; }
        public string ReadBitAddress { get; set; }
    }

    public class MachineStatusRecord
    {
        public DateTime Timestamp { get; set; }
        public int Status { get; set; } // 0 = Idle, 1 = Cutting
    }
    public class UtilizationShiftConfig
    {
        public int ShiftNo { get; set; }        // 1=早班, 2=中班, 3=晚班
        public string Start { get; set; }       // "HH:mm" 例如 "08:00"
        public string End { get; set; }       // "HH:mm" 例如 "16:00"
        public bool Enabled { get; set; } = false;
    }
    public class ShiftsFile
    {
        public string Timezone { get; set; } = "Asia/Taipei";
        public List<UtilizationShiftConfig> Shifts { get; set; } = new();
    }
    public class UtilizationResult
    {
        public int ShiftNo { get; set; }      //班別

        public int CuttingSeconds { get; set; }//切削秒數
        public int IdleSeconds { get; set; }  //未切削秒數
        public int TotalSeconds { get; set; }
        public int DenominatorSeconds { get; set; }
        public double UtilizationRate { get; set; }
    }
    public class ShiftResult
    {
        public string Machinename { get; set; } = "";
        public int ShiftNo { get; set; }
        public string ShiftName { get; set; } = "";
        public DateTime Date { get; set; }     // 哪一天
        public float CuttingSeconds { get; set; }
        public float UtilizationPercent { get; set; }
    }

}
