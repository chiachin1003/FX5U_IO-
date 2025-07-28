using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Message
{
    public interface IRULNotifier
    {
        void Enqueue(RULThresholdCrossedEventArgs args);
    }
}
