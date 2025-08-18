/***********************************************************

 Class:  SOCKETNET

 Description:
   <Insert description here>

 Author: Alex Wu

 Version: 170401A

***********************************************************/

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CommunicationDriver.Include.Driver
{
    public class CSOCKETNET
    {
        private bool m_bLOGFlag = true;

        public enum LINKMODE { TCP, UDP, TCP_SERVER, UDP_SERVER };

        private TcpClient m_TCPCLIENT = null;
        private NetworkStream m_STREAM = null;

        private UdpClient m_UDPCLIENT = null;
        private IPEndPoint m_RemoteIpEndPoint = null;


        private Queue<string> m_RXTXLog = new Queue<string>();
        private int m_iRetry;
        private int m_iPORT;
        private string m_sADDRESS;
        private int m_iRECETIMEOUT;
        private int m_iSENDTIMEOUT;
        private LINKMODE m_eMODE;

        public CSOCKETNET()
        {

        }

        private void RXTXLog(string sLabe1, string sLabe2, int iCycleTime, byte[] bData, int iLenght)
        {
            if (m_bLOGFlag)
            {
                try
                {
                    string sData = "";
                    try
                    {
                        if (bData != null) sData = BitConverter.ToString(bData, 0, iLenght).Replace("-", " ");
                        else sData = "";
                    }
                    catch (Exception)
                    {
                        sData = "";
                    }

                    string sLogBuff;
                    sLogBuff = "NET " + sLabe1 + " " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "][" + iCycleTime.ToString() + "] = " + sData + "-" + sLabe2;
                    if (m_RXTXLog.Count > 100) m_RXTXLog.Clear();
                    m_RXTXLog.Enqueue(sLogBuff);
                }
                catch (Exception)
                {

                }
            }
        }


        public int CreateNET(LINKMODE eMODE, string sADDRESS, int iPORT, int iRECETIMEOUT, int iSENDTIMEOUT, int iRetry)
        {
            int iErr = 0;

            if (iRetry < 3) m_iRetry = 3;
            else m_iRetry = iRetry;

            m_eMODE = eMODE;
            m_iPORT = iPORT;
            m_sADDRESS = sADDRESS;
            m_iRECETIMEOUT = iRECETIMEOUT;
            m_iSENDTIMEOUT = iSENDTIMEOUT;

            try
            {
                if (m_eMODE == LINKMODE.TCP)
                {
                    try
                    {

                        m_TCPCLIENT = new TcpClient();
                        //m_TCPCLIENT.ReceiveTimeout = m_iRECETIMEOUT;
                        //m_TCPCLIENT.SendTimeout = m_iRECETIMEOUT;

                        IAsyncResult MyResult = m_TCPCLIENT.BeginConnect(m_sADDRESS, m_iPORT, null, null);
                        MyResult.AsyncWaitHandle.WaitOne(m_iRECETIMEOUT * 10, true);

                        if (m_TCPCLIENT.Connected == true)
                        {
                            //作連上線的事
                            m_STREAM = m_TCPCLIENT.GetStream();
                            m_STREAM.ReadTimeout = m_iSENDTIMEOUT;
                            m_STREAM.WriteTimeout = m_iSENDTIMEOUT;
                            if (m_STREAM != null)
                            {
                                iErr = 1;
                                RXTXLog("CREATE TCP CLIENT CONNECT ADDRESS = " + m_sADDRESS + " , PORT = " + m_iPORT, "SUCCESS", 0, null, 0);

                            }
                            else
                            {
                                iErr = -2;
                                CloseNET();
                            }
                        }
                        else
                        {
                            //作如果沒連上線的事
                            iErr = -2;
                            Thread.Sleep(3000);
                            CloseNET();
                        }
                    }
                    catch (Exception)
                    {
                        iErr = -2;
                        Thread.Sleep(1000);
                        CloseNET();
                    }

                }
                else if (m_eMODE == LINKMODE.UDP)
                {
                    try
                    {
                        m_UDPCLIENT = new UdpClient();
                        m_RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                        m_UDPCLIENT.Connect(m_sADDRESS, m_iPORT);
                        //m_RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(m_sADDRESS), m_iPORT);
                        //m_UDPCLIENT.Connect(m_RemoteIpEndPoint);
                        m_UDPCLIENT.Client.ReceiveTimeout = m_iSENDTIMEOUT;
                        m_UDPCLIENT.Client.SendTimeout = m_iSENDTIMEOUT;
                        iErr = 1;
                        RXTXLog("CREATE UDP CLIENT CONNECT ADDRESS = " + m_sADDRESS + " , PORT = " + m_iPORT, "SUCCESS", 0, null, 0);
                    }
                    catch (Exception)
                    {
                        CloseNET();
                        RXTXLog("CREATE UDP CLIENT CONNECT ADDRESS = " + m_sADDRESS + " , PORT = " + m_iPORT, "FAIL", 0, null, 0);
                        Thread.Sleep(1000);
                        iErr = -3;
                    }
                }
            }
            catch (Exception)
            {
                iErr = -1;
            }


            return iErr;
        }


        public int CloseNET()
        {
            int iErr = 0;

            try
            {
                if (m_STREAM != null) m_STREAM.Close();
                if (m_TCPCLIENT != null) m_TCPCLIENT.Close();
                if (m_UDPCLIENT != null) m_UDPCLIENT.Close();
                if (m_eMODE == LINKMODE.TCP) RXTXLog("CLOSE TCP CLIENT CONNECT ADDRESS = " + m_sADDRESS + " , PORT = " + m_iPORT, "SUCCESS", 0, null, 0);
                else RXTXLog("CLOSE UDP CLIENT CONNECT ADDRESS = " + m_sADDRESS + " , PORT = " + m_iPORT, "SUCCESS", 0, null, 0);
                iErr = 1;
            }
            catch (Exception)
            {
                iErr = -1;
                //break;
            }

            return iErr;
        }

        public bool IsConnect()
        {
            bool bErr = false;

            try
            {
                if (m_eMODE == LINKMODE.TCP)
                {
                    if (m_STREAM != null)
                    {
                        if (m_TCPCLIENT != null)
                        {
                            if (m_TCPCLIENT.Client != null)
                            {
                                if ((m_TCPCLIENT.Connected) && (m_STREAM.CanRead == true) && (m_STREAM.CanWrite == true))
                                {
                                    bErr = true;
                                }
                            }
                        }
                    }
                    else bErr = false;
                }
                else
                {
                    if (m_UDPCLIENT != null)
                    {
                        bErr = true;
                    }
                }
                
            }
            catch
            {
                bErr = false;
            }

            return bErr;
        }


        public int Write(byte[] btBuffer,int iOffset,int iSize)
        {
            int iErr = 0;
            try
            {
                if (IsConnect())
                {
                    if (m_eMODE == LINKMODE.TCP)
                    {
                        try
                        {
                            m_STREAM.Write(btBuffer, iOffset, iSize);
                            iErr = iSize;
                        }
                        catch (Exception)
                        {
                            iErr = -1;
                        }
                    }
                    else
                    {
                        try
                        {
                            m_UDPCLIENT.Send(btBuffer, iSize);
                            iErr = iSize;
                        }
                        catch (Exception)
                        {
                            iErr = -1;
                        }
                    }
                }
                else iErr = -1;
            }
            catch (Exception)
            {
                iErr = -1;
            }
            return iErr;
        }

        public int Write(string sData)
        {
            int iErr = 0;
            try
            {
                if (IsConnect())
                {
                    if (m_eMODE == LINKMODE.TCP)
                    {

                    }
                    else
                    {
                        try
                        {
                            byte[] btBuffer = Encoding.UTF8.GetBytes(sData);
                            int iSize = sData.Length;
                            m_UDPCLIENT.Send(btBuffer, iSize);
                            iErr = iSize;
                        }
                        catch (Exception)
                        {
                            iErr = -1;
                        }
                    }
                }
                else iErr = -1;
            }
            catch (Exception)
            {
                iErr = -1;
            }
            return iErr;
        }


        public int Read(ref byte[] btBuffer, int iOffset, int iSize)
        {
            int iErr = 0;

            if (IsConnect())
            {
                if (m_eMODE == LINKMODE.TCP)
                {
                    try
                    {
                        iErr = m_STREAM.Read(btBuffer, iOffset, iSize);
                    }
                    catch (Exception) 
                    {
                        iErr = -1;
                    }
                }
                else
                {
                    try
                    {
                        btBuffer = m_UDPCLIENT.Receive(ref m_RemoteIpEndPoint);
                        iErr = btBuffer.Length;
                    }
                    catch (Exception)
                    {
                        iErr = -1;
                    }
                }
            }
            else iErr = -1;

            return iErr;
        }


    }
}