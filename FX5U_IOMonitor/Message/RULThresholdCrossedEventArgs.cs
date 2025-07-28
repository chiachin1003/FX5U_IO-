using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Message
{
    public class RULThresholdCrossedEventArgs : EventArgs
    {
        public string Address { get; set; } = "";
        public string Machine { get; set; } = "";
        public double RUL { get; set; }
        public string State { get; set; } = ""; // "yellow" or "red"
    }
}
