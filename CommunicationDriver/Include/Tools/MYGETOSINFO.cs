
/***********************************************************

 Class:  MYGETOSINFO

 Description:
   <Insert description here>

 Author: Alex Wu

 Version: 170405A

***********************************************************/

using System.Runtime.InteropServices;

namespace CommunicationDriver.Include.Tools
{
    public class CMYGETOSINFO
    {
        /*
        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public extern static IntPtr LoadLibrary(string libraryName);

        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        public extern static IntPtr GetProcAddress(IntPtr hwnd, string procedureName);


        private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, [Out] out bool isWow64Process);
        */

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public CMYGETOSINFO()
        {

        }

        ~CMYGETOSINFO()
        {

        }

        public static string GetOS()
        {
            return Environment.OSVersion.VersionString;
        }

        public static bool IsOS64Bit()
        {       
            return IntPtr.Size == 8;
        }

        //public static bool IsConnectedToInternet( )

        public static int InternetGetConnectedState()
        {
            int iFlags = 0;
            bool bErr = false;

            try
            {
                bErr = InternetGetConnectedState(out iFlags, 0);

                /*
                INTERNET_CONNECTION_CONFIGURED：0x40	本機電腦有一個合法的連線，但目前可能尚未連線
                INTERNET_CONNECTION_LAN：0x02	本機電腦是透過區域網路方式連至網際網路
                INTERNET_CONNECTION_MODEM：0x01	本機電腦是使用數據機方式連至網際網路
                INTERNET_CONNECTION_MODEM_BUSY：0x08	數據機忙線中無法使用
                INTERNET_CONNECTION_OFFLINE：0x20	本機電腦目前處於離線狀態
                INTERNET_CONNECTION_PROXY：0x04	本機電腦透過代理伺服器方式連至網際網路
                */
                if (bErr == false) iFlags = -1;
            }
            catch (Exception)
            {
                iFlags = -2;
            }

            return iFlags;
        }




    }
}
