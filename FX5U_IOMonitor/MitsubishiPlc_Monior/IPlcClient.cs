using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.MitsubishiPlc_Monior
{
    public interface IPlcClient : IDisposable
    {
        bool IsConnected { get; }
        bool Connect();
        void Disconnect();

        // bit/word 統一呼叫面
        bool[] ReadBits(string address, int bitCount);
        ushort[] ReadWords(string address, int wordCount);

        void WriteWord(string address, ushort value);
        void WriteWords(string address, ushort[] values);
    }

}
