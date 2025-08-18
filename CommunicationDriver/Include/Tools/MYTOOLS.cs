
/***********************************************************

 Class:  MYTOOLS

 Description:
   <Insert description here>

 Author: Alex Wu

 Version: 170527A

***********************************************************/

using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;



namespace CommunicationDriver.Include.Tools
{
    public class CMYTOOLS
    {


        public CMYTOOLS()
        {

        }

        ~CMYTOOLS()
        {

        }


        public static int BittoNUM16(bool[] bBit, out int[] iValue)
        {
            int iSize =0;
            if ((bBit.Count() % 16) != 0) iSize = (bBit.Count() / 16) + 1;
            else iSize = (bBit.Count() / 16);

            iValue = new int[iSize];

            bool[] bTEMP = new bool[iSize * 16];
            bool[] bTEMP16 = new bool[16];
            Array.Copy(bBit, 0, bTEMP, 0, bBit.Length);

            for (int j = 0; j < iSize; j++)
            {
                Array.Copy(bTEMP, j * 16, bTEMP16, 0, 16);

                for (int i = 0; i < 16; i++)
                {
                    if (bTEMP16[i]) iValue[j] = iValue[j] + (Int16)System.Math.Pow(2, i);
                }
            }

            return iSize;
        }

        public static int BittoNUM16(bool[] bBit)
        {
            int iValue = 0;
            for (int i = 0; i < 16; i++)
            {
                if (bBit[i]) iValue = iValue + (Int16)System.Math.Pow(2, i);
            }
            return iValue;
        }

        public static bool[] NUM16toBit(short iValue)
        {
            bool[] bBuff = new bool[16];
            bool[] bTemp = new bool[16];
            bBuff = Convert.ToString((UInt16)iValue % 0x10000, 2).Select(s => s.Equals('1')).ToArray();
            Array.Reverse(bBuff);
            bTemp = new bool[16];
            Array.Copy(bBuff, 0, bTemp, 0, bBuff.Length);

            if (bTemp != null) return bTemp;
            else return new bool[16];
        }

        public static bool[] NUM16toBit(short[] iValue)
        {
            bool[] bBuff = new bool[16];
            bool[] bTemp = new bool[16];
            bool[] bData = new bool[iValue.Length * 16];
            int iPOSLen = 0;

            for (int i = 0; i < iValue.Length;i++ )
            {
                bBuff = new bool[16];
                bBuff = Convert.ToString((UInt16)iValue[i] % 0x10000, 2).Select(s => s.Equals('1')).ToArray();
                //bBuff = Convert.ToString(iValue[i], 2).Select(s => s.Equals('1')).ToArray();
                Array.Reverse(bBuff);
                bTemp = new bool[16];
                try
                {
                    if (bBuff.Length > 16) iPOSLen = 16;
                    else iPOSLen = bBuff.Length;
                    Array.Copy(bBuff, 0, bTemp, 0, iPOSLen);
                    Array.Copy(bTemp, 0, bData, i * 16, 16);
                }
                catch(Exception) { }
            }
            return bData;
        }

        public static byte[] NUM16toByte(short[] iValue)
        {
            byte[] btBuff = new byte[iValue.Length * 2];

            for (int i = 0; i < iValue.Length; i++)
            {
                btBuff[(i * 2) + 1] = (byte)(iValue[i] / 0x100);
                btBuff[(i * 2)] = (byte)(iValue[i] % 0x100);
            }

            return btBuff;
        }

        public static string NUM16toASCII(short[] iValue, int iLen)
        {
            string sBUFF = "";
            string sOut = "";

            if (iLen == 0) sBUFF = System.Text.Encoding.Default.GetString(NUM16toByte(iValue), 0, iValue.Length * 2);
            else sBUFF = System.Text.Encoding.Default.GetString(NUM16toByte(iValue), 0, iLen);


            try
            {
                if (sBUFF.IndexOf('\0') >= 0)
                {
                    sOut = sBUFF.Substring(0, sBUFF.IndexOf('\0')).Trim();
                }
                else
                {
                    sOut = sBUFF;
                }

            }
            catch (Exception)
            {
                sOut = "";
            }

            return sOut;

        }

        public static int ASCIItoNUM16(string sValue, out Int16[] iValue)
        {
            int iSize = sValue.Length / 2;
            if ((sValue.Length % 2) > 0) iSize = iSize + 1;

            Int16[] iValueBUFF = new Int16[iSize];
            byte[] btBUFF = ASCIIEncoding.ASCII.GetBytes(sValue);

            try
            {
                for (int i = 0; i < iSize; i++)
                {
                    iValueBUFF[i] = (Int16)btBUFF[i * 2];
                    if (((i * 2) + 1) < sValue.Length) iValueBUFF[i] = (Int16)(iValueBUFF[i] + btBUFF[(i * 2) + 1] * 0x100);

                }
            }
            catch (Exception)
            {
                iValueBUFF = new Int16[iSize];
            }

            iValue = iValueBUFF;

            return iSize;
        }

        public static int JudgeCMD(string sCMD, ref ushort iPos, ref ushort iON_OFF)
        {
            //EXP:FACILITY FLAG,1
            //EXP:DI_0,1
            int iErr = 1;
            string[] sTemp = new string[3];
            if (sCMD != "")
            {
                try
                {
                    sTemp = sCMD.Split('_', ',');
                    iPos = Convert.ToUInt16(sTemp[1]);
                    iON_OFF = Convert.ToUInt16(sTemp[2]);
                }
                catch { }
            }
            else iErr = -1;

            return iErr;
        }

        public static int JudgeCMD2(string sCMD, ref ushort iPos, ref ushort iON_OFF)
        {
            //EXP:FACILITY FLAG,1
            //EXP:Y00,1
            int iTEMP = 0;
            int iErr = 1;
            string[] sTemp = new string[3];
            if (sCMD != "")
            {
                try
                {
                    sTemp = sCMD.Split('Y', ',');
                    iTEMP = ((Convert.ToUInt16(sTemp[1]) / 10) * 8) + (Convert.ToUInt16(sTemp[1]) % 10);
                    iPos = (ushort)iTEMP;
                    iON_OFF = Convert.ToUInt16(sTemp[2]);
                }
                catch { }
            }
            else iErr = -1;

            return iErr;
        }

        public static int JudgeCMD(string sCMD, ref ushort iCH, ref double dbValue)
        {
            //EXP:CH_0,1.42
            int iErr = 1;
            string[] sTemp = new string[3];
            if (sCMD != "")
            {
                try
                {
                    sTemp = sCMD.Split('_', ',');
                    iCH = Convert.ToUInt16(sTemp[1]);
                    dbValue = Convert.ToDouble(sTemp[2]);
                }
                catch { }
            }
            else iErr = -1;

            return iErr;
        }

        public static int JudgeCMDABB(string sCMD, ref ushort iPOS, ref bool dON_OFF)
        {
            int iErr = 1;
            string[] sTemp = new string[3];
            if (sCMD != "")
            {
                try
                {
                    sTemp = sCMD.Split(',');
                    iPOS = Convert.ToUInt16(sTemp[0]);
                    if (sTemp[1] == "1") dON_OFF = true;
                    else dON_OFF = false;
                    iErr = 1;
                }
                catch { }
            }
            else iErr = -1;

            return iErr;
        }

        public static List<string[]> StringToArray(string sData, char cSeparator)
        {
            string[] sDataLine;

            sDataLine = sData.Split(new Char[] { '\n' });


            List<string[]> sValAry = new List<string[]>();
            try
            {
                if (sDataLine.Length > 0)
                {
                    for (int i = 0; i < sDataLine.Length; i++)
                    {
                        sValAry.Add(sDataLine[i].Trim('\r').Split(new Char[] { cSeparator }));
                    }
                }
            }
            catch
            {
                sValAry = null;
            }

            return sValAry;
        }

        public static string ArrayToString(string[] sValAry, char cSeparator)
        {
            string sOut="";
            sOut = string.Join(cSeparator.ToString(), sValAry) + "\r\n";
            return sOut;
        }

        public static void AppendCRC(ref byte[] bData, int iCount)
        {

            int i = 0;
            uint CRC = 0xFFFF;
            int iCountBuf = 0;
            uint iBuff = 0;

            while (iCount > iCountBuf)
            {
                CRC = CRC ^ (uint)bData[iCountBuf];
                for (i = 8; i > 0; i--)
                {
                    iBuff = CRC & 0x0001;
                    if (iBuff > 0)
                    {
                        CRC >>= 1;
                        CRC = CRC ^ 0xA001;
                    }
                    else CRC >>= 1;
                }
                iCountBuf++;
            }

            bData[iCount] = (Byte)(CRC % 0x100);
            bData[iCount + 1] = (Byte)(CRC / 0x100);
        }

        public static string ChkSum(ref string sChkSumTmp)
        {
            int iChkSum = 0;
            char[] charArr = sChkSumTmp.ToCharArray();
            for (int i = 0; i < sChkSumTmp.Length; i++)
            {
                iChkSum = iChkSum + (int)charArr[i];
            }
            iChkSum = iChkSum % 0x100;
            sChkSumTmp = sChkSumTmp + iChkSum.ToString("X2");
            return iChkSum.ToString("X2");
        }

        public static List<string[]> Transpose(List<string[]> sValArray)
        {
            int i, j;

            string[] sBuff = new string[sValArray[0].Length];
            string[] sTemp = new string[sValArray.Count];
            List<string[]> sTempArray = new List<string[]>();

            for (i = 0; i < sValArray[0].Length; i++)
            {
                for (j = 0; j < sValArray.Count; j++)
                {
                    sBuff = sValArray[j];
                    sTemp[j] = sBuff[i];
                }
                sTempArray.Add(sTemp);
            }

            return sTempArray;
        }

        public static List<string[]> Rotate(List<string[]> sValArray)
        {
            int i, j;

            List<string[]> sTempArray = new List<string[]>();

            for (i = 0; i < sValArray[0].Length; i++)
            {
                string[] sBuff = new string[sValArray.Count];

                for (j = 0; j < sValArray.Count; j++)
                {
                    try
                    {
                        if (i < sValArray[j].Length) sBuff[j] = sValArray[j][i];
                    }
                    catch (Exception)
                    {


                    }
                }
                sTempArray.Add(sBuff);
            }

            return sTempArray;
        }


        public static void GetTcpConnectionINFO()
        {

            /*
            //下列範例顯示使用中 TCP 連線的端點資訊。
            Console.WriteLine("Active TCP Connections");
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

            TcpStatistics tcpstat = properties.GetUdpIPv4Statistics();
           
            foreach (TcpConnectionInformation c in connections)
            {
                
                //Console.WriteLine("{0} <==> {1}",
                //    c.LocalEndPoint.ToString(),
                //    c.RemoteEndPoint.ToString());
                 

                if (c.RemoteEndPoint.Port == 1433)
                {
                   

                }

            }


            TcpStatistics tcpstat = null;
            Console.WriteLine("");
            NetworkInterfaceComponent version = NetworkInterfaceComponent.IPv4;

            switch (version)
            {
                case NetworkInterfaceComponent.IPv4:
                    tcpstat = properties.GetTcpIPv4Statistics();
                    Console.WriteLine("TCP/IPv4 Statistics:");
                    break;
                case NetworkInterfaceComponent.IPv6:
                    tcpstat = properties.GetTcpIPv6Statistics();
                    Console.WriteLine("TCP/IPv6 Statistics:");
                    break;
                default:
                    throw new ArgumentException("version");
                //    break;
            }
            Console.WriteLine("  Minimum Transmission Timeout............. : {0}",
                tcpstat.MinimumTransmissionTimeout);
            Console.WriteLine("  Maximum Transmission Timeout............. : {0}",
                tcpstat.MaximumTransmissionTimeout);

            Console.WriteLine("  Connection Data:");
            Console.WriteLine("      Current  ............................ : {0}",
            tcpstat.CurrentConnections);
            Console.WriteLine("      Cumulative .......................... : {0}",
                tcpstat.CumulativeConnections);
            Console.WriteLine("      Initiated ........................... : {0}",
                tcpstat.ConnectionsInitiated);
            Console.WriteLine("      Accepted ............................ : {0}",
                tcpstat.ConnectionsAccepted);
            Console.WriteLine("      Failed Attempts ..................... : {0}",
                tcpstat.FailedConnectionAttempts);
            Console.WriteLine("      Reset ............................... : {0}",
                tcpstat.ResetConnections);

            Console.WriteLine("");
            Console.WriteLine("  Segment Data:");
            Console.WriteLine("      Received  ........................... : {0}",
                tcpstat.SegmentsReceived);
            Console.WriteLine("      Sent ................................ : {0}",
                tcpstat.SegmentsSent);
            Console.WriteLine("      Retransmitted ....................... : {0}",
                tcpstat.SegmentsResent);

            Console.WriteLine("");
            */
                      
        }

        public static bool IsInternet()
        {
            bool bInternet = false;

            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                int iFlag = 0;
                iFlag = CMYGETOSINFO.InternetGetConnectedState();
                /*
                INTERNET_CONNECTION_CONFIGURED：0x40	本機電腦有一個合法的連線，但目前可能尚未連線
                INTERNET_CONNECTION_LAN：0x02	本機電腦是透過區域網路方式連至網際網路
                INTERNET_CONNECTION_MODEM：0x01	本機電腦是使用數據機方式連至網際網路
                INTERNET_CONNECTION_MODEM_BUSY：0x08	數據機忙線中無法使用
                INTERNET_CONNECTION_OFFLINE：0x20	本機電腦目前處於離線狀態
                INTERNET_CONNECTION_PROXY：0x04	本機電腦透過代理伺服器方式連至網際網路
                */
                //else if ((iFlag == 0x12) || (iFlag == 0x72)|| (iFlag == 0x52))
                if (iFlag == 0x01) bInternet = true;
                else if (iFlag == 0x02) bInternet = true;
                else if (iFlag == 0x04) bInternet = true;
                else
                {
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                    TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

                    int iRemoteCount = 0;
                    int iErrCount = 0;
                    string sLocalIP = "";
                    string sRemoteIP = "";
                    foreach (TcpConnectionInformation c in connections)
                    {
                        sLocalIP = c.LocalEndPoint.ToString();
                        sRemoteIP = c.RemoteEndPoint.ToString();
                        if (
                            (c.State == TcpState.Established) &&
                            (sRemoteIP.IndexOf("127.0.0.1") == -1) &&
                            (sRemoteIP.IndexOf("0.0.0.0") == -1) &&
                            (sLocalIP.Substring(0, 7) != sRemoteIP.Substring(0, 7))
                            )
                        {
                            iRemoteCount++;
                            if (iRemoteCount >= 1)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (
                            (c.State == TcpState.Established) &&
                            (sRemoteIP.IndexOf("127.0.0.1") == -1) &&
                            (sRemoteIP.IndexOf("0.0.0.0") == -1)
                            )
                            {
                                iErrCount++;
                            }

                        }
                    }

                    if (iRemoteCount >= 1)
                    {
                        bInternet = true;
                    }
                    else
                    {
                        if (iErrCount >= 1)
                        {
                            try
                            {
                                //Dns.GetHostEntry("www.google.com");
                                //bInternet = true;
                            }
                            catch (SocketException) { }
                        }
                    }
                }

            }
            else
            {
                bInternet = false;
            }
            return bInternet;
        }

        public static bool Is64bit()
        {
            return IntPtr.Size == 8;
        }

        public static int SQLSplit(string sSQL,out string[] sValAryOut)
        {
           
            string[] sValAry = new string[3];
            int iErr=0;
            int iStartPOS = 0;
            int iPOS=0;
            int iCount = 0;
            string sBUFF ="";

            sBUFF = sSQL;
            try
            {
                if (sBUFF.IndexOf("INSERT INTO ", 0) >= 0)
                {
                    //"INSERT INTO " + sTable + " ( " + sItem + " ) VALUES ( " + sValue + " );";
                    iErr = 0;
                    iStartPOS = 12;
                    iPOS = sBUFF.IndexOf(" (", iStartPOS);
                    iCount = iPOS - iStartPOS;
                    sValAry[0] = sBUFF.Substring(iStartPOS, iCount).Trim();

                    iStartPOS = iPOS + 2;
                    iPOS = sBUFF.IndexOf(" VALUES (", iStartPOS);
                    iCount = iPOS - iStartPOS;
                    sValAry[1] = sBUFF.Substring(iStartPOS, iCount).Trim().Replace("(", "").Replace(")", "").Trim();

                    iStartPOS = iPOS + 9;
                    iPOS = sBUFF.IndexOf(");", iStartPOS);
                    iCount = iPOS - iStartPOS;
                    sValAry[2] = sBUFF.Substring(iStartPOS, iCount).Trim();

                }
                else if (sBUFF.IndexOf("UPDATE ", 0) >= 0)
                {
                    //"UPDATE " + sTable + " SET " + sItemValue + " WHERE " + sConditions + ";";
                    iErr = 1;
                    iStartPOS = 7;
                    iPOS = sBUFF.IndexOf(" SET ", iStartPOS);
                    iCount = iPOS - iStartPOS;
                    sValAry[0] = sBUFF.Substring(iStartPOS, iCount).Trim();

                    iStartPOS = iPOS + 5;
                    iPOS = sBUFF.IndexOf(" WHERE ", iStartPOS);
                    iCount = iPOS - iStartPOS;
                    sValAry[1] = sBUFF.Substring(iStartPOS, iCount).Trim().Replace("(","").Replace(")","").Trim();

                    iStartPOS = iPOS + 7;
                    iPOS = sBUFF.IndexOf(";", iStartPOS);
                    iCount = iPOS - iStartPOS;
                    sValAry[2] = sBUFF.Substring(iStartPOS, iCount).Trim();

                }
                else if (sBUFF.IndexOf("DELETE FROM ", 0) >= 0)
                {
                    //"DELETE FROM " + sTable + " WHERE " + sConditions + ";";
                    iErr = 2;

                }
                else iErr = -1;
            }
            catch (Exception) 
            {
                iErr = -2;
            }

            sValAryOut = sValAry;

            return iErr;
        }

        public static double IEEE754toSingle(short[] iValue)
        {
            double dbErr = 0;

            try
            {
                Int32 iBUFF = (Int32)((iValue[0] * 0x10000) + iValue[1]);
                byte[] btArray = BitConverter.GetBytes(iBUFF);
                Array.Reverse(btArray);
                dbErr = (double)BitConverter.ToSingle(btArray, 0);
            }
            catch (Exception)
            {
                dbErr = 0;
            }

            return dbErr;
        }

        public static string StringTrim(string sString)
        {
            int iPOS = 0;
            string sOUT = ""; ;
            string sBUFF = sString.Trim();
            iPOS = sBUFF.IndexOf("\0");
            if (iPOS > 0)
            {
                sOUT = sBUFF.Substring(0, iPOS);
            }
            else sOUT = sBUFF;

            return sOUT;
        }


        public static string DateTimeToString(int iType, bool bQuotes, DateTime dtDateTime)
        {
            string sDatetime = "";

            if (iType == 0) sDatetime = dtDateTime.ToString("yyyy/MM/dd");
            else if (iType == 1) sDatetime = dtDateTime.ToString("yyyy/MM/dd HH:mm:ss");
            else if (iType == 2) sDatetime = dtDateTime.ToString("HH:mm:ss");
            else if (iType == 3) sDatetime = dtDateTime.ToString("yyyy/MM/dd HH:00:00");

            else if (iType == 4) sDatetime = dtDateTime.ToString("yyyy/MM/dd zzz");
            else if (iType == 5) sDatetime = dtDateTime.ToString("yyyy/MM/dd HH:mm:ss zzz");
            else if (iType == 6) sDatetime = dtDateTime.ToString("HH:mm:ss zzz");
            else if (iType == 7) sDatetime = dtDateTime.ToString("yyyy/MM/dd HH:00:00 zzz");
            else sDatetime = dtDateTime.ToString("yyyy/MM/dd HH:mm:ss");

            if (bQuotes) sDatetime = "'" + sDatetime + "'";

            return sDatetime;
        }

        public static string StringReverse(string sString)
        {
            char[] cArray = sString.ToCharArray();
            Array.Reverse(cArray);            //將char array中的元素位置反轉
            string strReverse = new string(cArray); //將反轉完的char array轉回字串
            return strReverse;
        }

        public static List<string> StringToList(string sData, string strSeparator)
        {
            string[] sDataLine;
            List<string> sValList = new List<string>();

            sData = sData.Replace("\n", "");

            try
            {
                if ((strSeparator == "") || (strSeparator == null))
                {
                    sDataLine = sData.Split(new Char[] { '\r' });
                }
                else
                {
                    char[] cArray = strSeparator.ToCharArray();
                    sDataLine = sData.Split(cArray);
                }

                try
                {
                    if (sDataLine.Length > 0)
                    {
                        for (int i = 0; i < sDataLine.Length; i++)
                        {
                            sValList.Add(sDataLine[i].Replace("\r", ""));
                        }
                    }
                }
                catch (Exception e)
                {
                    sValList = null;
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
            }
            catch (Exception e)
            {
                sValList = null;
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return sValList;
        }


        public static string ToMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(str);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            //md5.Dispose();
           
            return sb.ToString();
        }

        public static List<string[]> JsonToArray(string sJson)
        {
            string[] sDataLine;
            string sORGJson = sJson;

            sJson = sJson.Replace("},{", "&");
            sJson = sJson.Replace("{", "");
            sJson = sJson.Replace("}", "");

            sDataLine = sJson.Split(new char[] { '&' });
            
            string sBUFF="";
            string[] sAryBUFF;

            List<string> listBUFF = new List<string>();

            List<string[]> sValAry = new List<string[]>();


            if (sJson != "[]")
            {
                try
                {
                    if (sDataLine.Length > 0)
                    {
                        for (int i = 0; i < sDataLine.Length; i++)
                        {
                            sBUFF = sDataLine[i].Trim().Trim('[').Trim(']').Trim('\r').Trim('\n').Replace("\":", "&").Replace(",\"", "&").Replace(":\"", "&").Replace("\"", "");


                            sAryBUFF = sBUFF.Split(new char[] { '&' });
                            listBUFF = new List<string>();
                            for (int j = 0; j < sAryBUFF.Length; j++)
                            {
                                if (j % 2 == 1)
                                {
                                    if (sAryBUFF[j] == "null") listBUFF.Add("");
                                    else listBUFF.Add(sAryBUFF[j]);
                                }
                            }

                            sValAry.Add(listBUFF.ToArray<string>());
                        }
                    }
                }
                catch
                {
                    sValAry = null;
                }
            }
             

            return sValAry;
        }

    }
}
