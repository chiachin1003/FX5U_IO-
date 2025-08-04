using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Config
{
    internal class Message_Config
    {
        private static readonly object _logLock = new(); // 🔒 保護寫入的 lock
        private static readonly string _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorMessage.txt");

        public static void LogMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return; // 不寫入空訊息

            try
            {
                lock (_logLock)
                {
                    // 確保資料夾存在
                    Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath)!);

                    // 寫入訊息到檔案
                    File.AppendAllText(_logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ 寫入 log 檔案失敗：{ex.Message}");
            }
        }
    }
}
