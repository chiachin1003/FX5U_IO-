/***********************************************************

 Class:  MITSUBISHIPLC1E

 Description:
   <Insert description here>

 Author: Alex Wu

 Version: 160416A

***********************************************************/
using CommunicationDriver.Include.Tools;
using System.Text.RegularExpressions;

namespace CommunicationDriver.Include.Driver
{
	public class CMITSUBISHIPLC1E : IMitsubishiPlc
	{
		private const bool _DEDBUG_ = false;

		private bool m_bLOGFlag;

		private CSOCKETNET m_cNet = new CSOCKETNET();

		private Queue<string> m_RXTXLog = new Queue<string>();
		private CSOCKETNET.LINKMODE m_eMODE;

		private int m_iErr;
		private int m_iWTD;
		private int m_iRetry;
		private int m_iPORT;
		private string m_sADDRESS;
		private int m_iRECETIMEOUT;
		private int m_iSENDTIMEOUT;

		private bool m_bCHKConnectFlag = false;

		public CMITSUBISHIPLC1E()
		{
			m_bLOGFlag = false;
			m_iErr = 0;
			m_iWTD = 0;
		}

		~CMITSUBISHIPLC1E()
		{
			ClosePLC();
		}

		public int CreatePLC( CSOCKETNET.LINKMODE eMODE, string sADDRESS, int iPORT, int iRECETIMEOUT, int iSENDTIMEOUT, int iRetry )
		{
			int iErr = 0;

			if( iRetry < 3 )
				m_iRetry = 3;
			else
				m_iRetry = iRetry;
			m_eMODE = eMODE;

			m_iPORT = iPORT;
			m_sADDRESS = sADDRESS;
			m_iRECETIMEOUT = iRECETIMEOUT;
			m_iSENDTIMEOUT = iSENDTIMEOUT;
			m_iErr = 0;
			m_iWTD = 0;

			m_bCHKConnectFlag = false;

			try {
				iErr = m_cNet.CreateNET( m_eMODE, m_sADDRESS, iPORT, m_iRECETIMEOUT, m_iSENDTIMEOUT, 3 );
				if( iErr > 0 ) {
					short[] pw = new short[ 1 ];
					if( ReadBatchPLC( ref pw, DEVICETYPE.D, 0, 1 ) > 0 ) {
						m_bCHKConnectFlag = true;
					}
					else {
						iErr = -2;
					}
				}

			}
			catch {
				iErr = -1;
			}


			return iErr;
		}

		public int ClosePLC()
		{
			int iErr = 0;

			try {
				iErr = m_cNet.CloseNET();
				m_bCHKConnectFlag = false;
			}
			catch( Exception ) {
				iErr = -1;
				//break;
			}

			return iErr;
		}

		private int ReConnect()
		{
			int iErr = 0;

			m_iErr++;

			if( m_iErr > m_iRetry ) {
				Thread.Sleep( 1000 );

				if( ClosePLC() > 0 ) {
					iErr = CreatePLC( m_eMODE, m_sADDRESS, m_iPORT, m_iRECETIMEOUT, m_iSENDTIMEOUT, m_iRetry );
					m_iErr = 0;
					Thread.Sleep( 100 );
				}
				else
					iErr = -1;

			}

			return iErr;
		}

		public bool IsConnect()
		{
			return ( m_cNet.IsConnect() && ( m_bCHKConnectFlag ) );
		}

		private void UpDataWTD()
		{
			if( m_iWTD > 1000 )
				m_iWTD = 0;
			else
				m_iWTD++;
		}

		public int GetWTD()
		{
			return m_iWTD;
		}

		public string RepLog()
		{
			return m_RXTXLog.Dequeue();
		}

		public int RepLogCount()
		{
			return m_RXTXLog.Count;
		}

		private void RXTXLog( string sLabe1, string sLabe2, int iCycleTime, byte[] bData, int iLenght )
		{
			if( m_bLOGFlag ) {
				try {
					string sData = "";
					try {
						sData = BitConverter.ToString( bData, 0, iLenght ).Replace( "-", " " );
					}
					catch {
						sData = "";
					}

					string sLogBuff;
					sLogBuff = "MITSUBISHI " + sLabe1 + " " + DateTime.Now.ToString( "yyyy/MM/dd hh:mm:ss" ) + "][" + iCycleTime.ToString() + "] = " + sData + "-" + sLabe2;
					if( m_RXTXLog.Count > 100 )
						m_RXTXLog.Clear();
					m_RXTXLog.Enqueue( sLogBuff );
				}
				catch {

				}
			}
		}

		public void SETDEBUG()
		{
			m_bLOGFlag = true;
		}

		public void RSTDEBUG()
		{
			m_bLOGFlag = false;
		}

		private int GetHeader( int iHeader )
		{

			//Y->(59H, 20H)
			//L->H
			//[0] = 0x5920 % 0x100 = 0x20
			//[1] = 0x5920 / 0x100 = 0x59

			switch( iHeader ) {
				case 0:         //D
					return 0x4420;
				case 1:         //R
					return 0x5220;
					;
				case 4:         //X
					return 0x5820;
				case 5:         //Y
					return 0x5920;
				case 6:         //M
					return 0x4D20;
			}
			return 0x0;
		}
        private static readonly Dictionary<string, DEVICETYPE> DeviceMap =
		new(StringComparer.OrdinalIgnoreCase)
		{
			["X"] = DEVICETYPE.X,
			["Y"] = DEVICETYPE.Y,
			["M"] = DEVICETYPE.M,
			["D"] = DEVICETYPE.D,
			["B"] = DEVICETYPE.B,
			["S"] = DEVICETYPE.S,
		};

        private static bool TryParseDevice(string code, out DEVICETYPE dev)
            => DeviceMap.TryGetValue(code.Trim(), out dev);

        // X/Y/B 用 16 進位，M/S/D 用 10 進位
        private static int ParseAddress(DEVICETYPE dev, string addressText)
        {
            int numberBase = (dev == DEVICETYPE.X || dev == DEVICETYPE.Y || dev == DEVICETYPE.B) ? 16 : 10;
            return Convert.ToInt32(addressText.Trim(), numberBase);
        }

        // 解析 "M100" 這種合併寫法
        private static bool TryParseTag(string tag, out DEVICETYPE dev, out int address)
        {
            dev = default; address = 0;
            var m = Regex.Match(tag, @"^\s*([A-Za-z])\s*([0-9A-Fa-f]+)\s*$");
            if (!m.Success) return false;

            var code = m.Groups[1].Value;
            var addr = m.Groups[2].Value;

            if (!TryParseDevice(code, out dev)) return false;
            address = ParseAddress(dev, addr);
            return true;
        }
        private static bool IsBitDevice(DEVICETYPE dev)
		  => dev == DEVICETYPE.X || dev == DEVICETYPE.Y || dev == DEVICETYPE.M;
        private Object m_fThisLock = new Object();

		private int ReadWritePLC( ref byte[] bRev, byte[] bCMD, int iCMDLen, int iRECVLen )
		{
			lock( m_fThisLock ) {
				int iErr = 0;
				int iCHKTime = 0;
				try {
					iErr = m_cNet.Write( bCMD, 0, iCMDLen );
					RXTXLog( "READ/WRITE PLC TX", "SUCCESS", 0, bCMD, iCMDLen );
				}
				catch {
					RXTXLog( "READ/WRITE PLC TX", "FAIL", 0, bCMD, iCMDLen );
					iErr = -1;
					ReConnect();
					return iErr;
				}

				iCHKTime = System.Environment.TickCount;

				do {
					try {
						iErr = m_cNet.Read( ref bRev, 0, iRECVLen );
						if( iErr > 0 && ( iErr == iRECVLen ) ) {
							RXTXLog( "READ/WRITE PLC RX", "SUCCESS", System.Environment.TickCount - iCHKTime, bRev, iErr );
							UpDataWTD();
							m_iErr = 0;
							break;
						}

					}
					catch( Exception ) {
						iErr = -2;
					}

				} while( !( ( System.Environment.TickCount - iCHKTime ) > m_iRECETIMEOUT ) );

				if( ( ( System.Environment.TickCount - iCHKTime ) > m_iRECETIMEOUT ) && ( iErr < 0 ) ) {
					RXTXLog( "READ/WRITE PLC RX", "FAIL", System.Environment.TickCount - iCHKTime, bRev, iErr );
					ReConnect();
					return iErr = -3;
				}

				return iErr;
			}
		}

		public int ReadBatchPLC( ref short[] pw, DEVICETYPE eHeader, int iAddress, int iNUMOfDevice )
		{
			//X,Y,M = Make sure to set a multiple of 16 as the head device number of bit devices.
			//read only address/16. exp=1312%16=0

			int iErr = 0;

			byte[] bReadCMD = new byte[ 12 ];
			byte[] bf = new byte[ 256 ];
			int iAddrBUFF = 0;
			int iDIV = 0;
			int iMOD = 0;
			int iBitSize = 0;
			bool iMEMMoveFlag = false;


			if( ( eHeader == DEVICETYPE.X ) || ( eHeader == DEVICETYPE.Y ) || ( eHeader == DEVICETYPE.M ) ) {
				iAddrBUFF = iAddress;

				if( ( iAddress % 16 ) != 0 ) {
					iMEMMoveFlag = true;

					iDIV = iAddress / 16;
					iMOD = iAddress % 16;
					iBitSize = ( iNUMOfDevice * 16 ) + iMOD;

					if( ( iBitSize % 16 ) > 0 )
						iNUMOfDevice = ( iBitSize / 16 ) + 1;
					else
						iNUMOfDevice = iBitSize / 16;

					iAddress = iDIV * 16;
				}

			}


			int iDevice = iNUMOfDevice / 64;
			int iTemp;

			if( ( (int)eHeader == 0 ) || ( (int)eHeader == 1 ) || ( (int)eHeader == 4 ) || ( (int)eHeader == 5 ) || ( (int)eHeader == 6 ) ) {
				if( ( iNUMOfDevice % 64 ) > 0 )
					iDevice = iDevice + 1;

				for( int j = 0; j < iDevice; j++ ) {
					bReadCMD[ 0 ] = 0x01;                         //SubHeader-Batch reading
					bReadCMD[ 1 ] = 0xFF;                         //PLC No.
					bReadCMD[ 2 ] = 0x0A;                         //Monitoring timer-L 2500ms
					bReadCMD[ 3 ] = 0x00;                         //Monitoring timer-H

					//L->H

					bReadCMD[ 4 ] = (Byte)( ( ( iAddress + ( j * 64 ) ) % 0x10000 ) % 0x100 ); //Address
					bReadCMD[ 5 ] = (Byte)( ( ( iAddress + ( j * 64 ) ) % 0x10000 ) / 0x100 );
					bReadCMD[ 6 ] = (Byte)( ( ( iAddress + ( j * 64 ) ) / 0x10000 ) % 0x100 );
					bReadCMD[ 7 ] = (Byte)( ( ( iAddress + ( j * 64 ) ) / 0x10000 ) / 0x100 );


					bReadCMD[ 8 ] = (Byte)( GetHeader( (int)eHeader ) % 0x100 );
					bReadCMD[ 9 ] = (Byte)( GetHeader( (int)eHeader ) / 0x100 );

					if( ( ( j + 1 ) == iDevice ) && ( ( iNUMOfDevice % 64 ) != 0 ) ) {
						bReadCMD[ 10 ] = (Byte)( iNUMOfDevice % 64 );
						iTemp = ( iNUMOfDevice % 64 );
					}
					else {
						bReadCMD[ 10 ] = 64;
						iTemp = 64;
					}


					bReadCMD[ 11 ] = 0x0;
					try {
						RXTXLog( "READ BATCH", "DEFINE", 0, null, 0 );
						bf = new byte[ ( iTemp * 2 ) + 2 ];
						iErr = ReadWritePLC( ref bf, bReadCMD, 12, ( iTemp * 2 ) + 2 );
					}
					catch {
						RXTXLog( "READ BATCH", "FAIL", 0, null, 0 );
						iErr = -1;
						ReConnect();
						return iErr;
					}

					if( iErr > 0 && ( iErr == ( iTemp * 2 ) + 2 ) ) {
						if( ( bf[ 0 ] == 0x81 ) & ( bf[ 1 ] == 0x00 ) ) {
							for( int i = 0; i < iTemp; i++ ) {
								try {
									pw[ i + ( j * 64 ) ] = BitConverter.ToInt16( bf, 2 + ( i * 2 ) );
								}
								catch {
									return iErr = -4;
								}
							}
							iErr = bf.Length;
						}
						else
							return iErr = -3;


						if( ( j + 1 ) == iDevice ) {

							if( iMEMMoveFlag ) {
								try {
									bool[] bTMP = new bool[ iNUMOfDevice * 16 ];
									bool[] bDataBUFF = new bool[ iNUMOfDevice * 16 ];
									bTMP = CMYTOOLS.NUM16toBit( pw );
									Array.Copy( bTMP, iMOD, bDataBUFF, 0, ( iNUMOfDevice * 16 ) - iMOD );
									//MYTOOLS.CMYTOOLS.BittoNUM16(bDataBUFF,out (int[])pw);

									for( int i = 0; i < iNUMOfDevice; i++ ) {
										bTMP = new bool[ 16 ];
										Array.Copy( bDataBUFF, i * 16, bTMP, 0, 16 );
										pw[ i ] = (short)CMYTOOLS.BittoNUM16( bTMP );
									}
								}
								catch {
									return iErr = -5;
								}

							}
							return iErr;
						}
					}
					else
						return iErr = -2;
				}
			}
			else
				iErr = -1;


			return iErr;

		}
        public int ReadBatchPLC(ref short[] pw, string deviceCode, int address, int wordCount, bool alignBitHead = true)
        {
            if (!TryParseDevice(deviceCode, out var dev)) return -99; // 未知裝置

            int start = address;
            int count = wordCount;

            // bit 裝置可先對齊到 16 的倍數（word 起點）；若不想外層對齊，傳 alignBitHead:false
            if (alignBitHead && IsBitDevice(dev) && (start % 16 != 0))
                start = (start / 16) * 16;

            return ReadBatchPLC(ref pw, dev, start, count);
        }      
        public int ReadBatchPLC(ref short[] pw, string tag, int wordCount, bool alignBitHead = true)
        {
            if (!TryParseTag(tag, out var dev, out var address)) return -98; // 解析失敗

            int start = address;
            int count = wordCount;

            if (alignBitHead && IsBitDevice(dev) && (start % 16 != 0))
                start = (start / 16) * 16;

            return ReadBatchPLC(ref pw, dev, start, count);
        }
        public int WriteBatchPLC( short[] pw, DEVICETYPE eHeader, int iAddress, int iNUMOfDevice )
		{
			int iErr = 0;
			byte[] bWriteCMD = new byte[ 256 ];
			byte[] bf = new byte[ 2 ];

			int iDevice = iNUMOfDevice / 64;
			int iTemp;

			if( ( (int)eHeader == 0 ) || ( (int)eHeader == 1 ) || ( (int)eHeader == 4 ) || ( (int)eHeader == 5 ) || ( (int)eHeader == 6 ) ) {

				if( ( iNUMOfDevice % 64 ) > 0 )
					iDevice = iDevice + 1;

				for( int j = 0; j < iDevice; j++ ) {
					bWriteCMD[ 0 ] = 0x03;                         //SubHeader-Batch write
					bWriteCMD[ 1 ] = 0xFF;                         //PLC No.
					bWriteCMD[ 2 ] = 0x0A;                         //Monitoring timer-L 2500ms
					bWriteCMD[ 3 ] = 0x00;                         //Monitoring timer-H

					//L->H
					bWriteCMD[ 4 ] = (Byte)( ( ( iAddress + ( j * 64 ) ) % 0x10000 ) % 0x100 ); //Address
					bWriteCMD[ 5 ] = (Byte)( ( ( iAddress + ( j * 64 ) ) % 0x10000 ) / 0x100 );
					bWriteCMD[ 6 ] = (Byte)( ( ( iAddress + ( j * 64 ) ) / 0x10000 ) % 0x100 );
					bWriteCMD[ 7 ] = (Byte)( ( ( iAddress + ( j * 64 ) ) / 0x10000 ) / 0x100 );

					bWriteCMD[ 8 ] = (Byte)( GetHeader( (int)eHeader ) % 0x100 );
					bWriteCMD[ 9 ] = (Byte)( GetHeader( (int)eHeader ) / 0x100 );

					if( ( ( j + 1 ) == iDevice ) && ( ( iNUMOfDevice % 64 ) != 0 ) ) {
						bWriteCMD[ 10 ] = (Byte)( iNUMOfDevice % 64 );
						iTemp = ( iNUMOfDevice % 64 );
					}
					else {
						bWriteCMD[ 10 ] = 64;
						iTemp = 64;
					}
					bWriteCMD[ 11 ] = 0x0;

					for( int i = 0; i < iTemp; i++ ) {
						bWriteCMD[ ( i * 2 ) + 12 ] = (Byte)( pw[ i + ( j * 64 ) ] % 0x100 );
						bWriteCMD[ ( i * 2 ) + 12 + 1 ] = (Byte)( pw[ i + ( j * 64 ) ] / 0x100 );
					}

					try {
						RXTXLog( "WRITE BATCH", "DEFINE", 0, null, 0 );
						iErr = ReadWritePLC( ref bf, bWriteCMD, ( 12 + ( iTemp * 2 ) ), bf.Length );
					}
					catch {
						RXTXLog( "WRITE BATCH", "FAIL", 0, null, 0 );
						iErr = -1;
						ReConnect();
						return iErr;
					}


					if( iErr > 0 && ( iErr == 2 ) ) {
						if( ( bf[ 0 ] == 0x83 ) & ( bf[ 1 ] == 0x00 ) )
							iErr = bf.Length;
						else
							return iErr = -4;
						if( ( j + 1 ) == iDevice )
							return iErr;
						//else break;
					}
					else
						iErr = -3;
				}
			}
			else
				iErr = -2;

			return iErr;
		}
        public int WriteBatchPLC(short[] pw, string deviceCode, int address, int wordCount, bool alignBitHead = true)
        {
            if (pw == null || pw.Length == 0) return -97;            // 空資料
            if (!TryParseDevice(deviceCode, out var dev)) return -99; // 未知裝置

            int start = address;

            // bit 裝置用 word 打包寫入，預設對齊到 16 的倍數（與 Read 一致）
            if (alignBitHead && IsBitDevice(dev) && (start % 16 != 0))
                start = (start / 16) * 16;

            int count = Math.Min(wordCount, pw.Length);              // 防越界
            return WriteBatchPLC(pw, dev, start, count);             // 呼叫你原本 1E 的 Write
        }

        public int WriteBatchPLC(short[] pw, string tag, int wordCount, bool alignBitHead = true)
        {
            if (pw == null || pw.Length == 0) return -97;
            if (!TryParseTag(tag, out var dev, out var address)) return -98; // 解析失敗

            int start = address;

            if (alignBitHead && IsBitDevice(dev) && (start % 16 != 0))
                start = (start / 16) * 16;

            int count = Math.Min(wordCount, pw.Length);
            return WriteBatchPLC(pw, dev, start, count);
        }

        public int WriteBitPLC( int iNO_OFF, DEVICETYPE eHeader, int iAddress )
		{
			int iErr = 0;
			byte[] bWriteCMD = new byte[ 13 ];
			byte[] bf = new byte[ 2 ];

			if( ( (int)eHeader == 4 ) || ( (int)eHeader == 5 ) || ( (int)eHeader == 6 ) ) {
				bWriteCMD[ 0 ] = 0x02;                         //SubHeader-bit Unit write
				bWriteCMD[ 1 ] = 0xFF;                         //PLC No.
				bWriteCMD[ 2 ] = 0x0A;                         //Monitoring timer-L 2500ms
				bWriteCMD[ 3 ] = 0x00;                         //Monitoring timer-H

				//L->H
				bWriteCMD[ 4 ] = (Byte)( ( iAddress % 0x10000 ) % 0x100 ); //Address
				bWriteCMD[ 5 ] = (Byte)( ( iAddress % 0x10000 ) / 0x100 );
				bWriteCMD[ 6 ] = (Byte)( ( iAddress / 0x10000 ) % 0x100 );
				bWriteCMD[ 7 ] = (Byte)( ( iAddress / 0x10000 ) / 0x100 );

				bWriteCMD[ 8 ] = (Byte)( GetHeader( (int)eHeader ) % 0x100 );
				bWriteCMD[ 9 ] = (Byte)( GetHeader( (int)eHeader ) / 0x100 );

				bWriteCMD[ 10 ] = 0x01;

				bWriteCMD[ 11 ] = 0x0;

				bWriteCMD[ 12 ] = (Byte)( iNO_OFF * 0x10 );


				try {
					RXTXLog( "WRITE RST/SET CMD", "DEFINE", 0, null, 0 );
					iErr = ReadWritePLC( ref bf, bWriteCMD, 13, bf.Length );
				}
				catch {
					RXTXLog( "WRITE RST/SET CMD", "FAIL", 0, null, 0 );
					iErr = -1;
					ReConnect();
					return iErr;
				}


				if( iErr > 0 && ( iErr == 2 ) ) {
					if( ( bf[ 0 ] == 0x82 ) & ( bf[ 1 ] == 0x00 ) )
						iErr = bf.Length;
					else
						return iErr = -4;
				}



			}
			else
				iErr = -3;

			return iErr;
		}
        public int ReadBitsPLC(ref bool[] bits, DEVICETYPE eHeader, int iAddress, int iNUMOfDevice)
        {
            int iErr = 0;

            byte[] bReadCMD = new byte[12];
            byte[] bf = new byte[256];

            int iAddrBUFF = 0;
            int iDIV = 0;
            int iMOD = 0;
            int iBitSize = 0;
            bool iMEMMoveFlag = false;

            if ((eHeader == DEVICETYPE.X) || (eHeader == DEVICETYPE.Y) || (eHeader == DEVICETYPE.M))
            {
                iAddrBUFF = iAddress;

                // Adjust address for bit devices (X, Y, M)
                if ((iAddress % 16) != 0)
                {
                    iMEMMoveFlag = true;
                    iDIV = iAddress / 16;
                    iMOD = iAddress % 16;
                    iBitSize = (iNUMOfDevice * 16) + iMOD;

                    if ((iBitSize % 16) > 0)
                        iNUMOfDevice = (iBitSize / 16) + 1;
                    else
                        iNUMOfDevice = iBitSize / 16;

                    iAddress = iDIV * 16;
                }
            }

            int iDevice = iNUMOfDevice / 64;
            int iTemp;

            if (((int)eHeader == 0) || ((int)eHeader == 1) || ((int)eHeader == 4) || ((int)eHeader == 5) || ((int)eHeader == 6))
            {
                if ((iNUMOfDevice % 64) > 0)
                    iDevice = iDevice + 1;

                for (int j = 0; j < iDevice; j++)
                {
                    bReadCMD[0] = 0x01; // SubHeader-Batch reading
                    bReadCMD[1] = 0xFF; // PLC No.
                    bReadCMD[2] = 0x0A; // Monitoring timer-L 2500ms
                    bReadCMD[3] = 0x00; // Monitoring timer-H

                    // L->H (Address)
                    bReadCMD[4] = (byte)((iAddress + (j * 64)) % 0x100); // Address L
                    bReadCMD[5] = (byte)((iAddress + (j * 64)) / 0x100); // Address H
                    bReadCMD[6] = (byte)((iAddress + (j * 64)) / 0x10000);
                    bReadCMD[7] = (byte)((iAddress + (j * 64)) / 0x10000);

                    bReadCMD[8] = (byte)(GetHeader((int)eHeader) % 0x100);
                    bReadCMD[9] = (byte)(GetHeader((int)eHeader) / 0x100);

                    if (((j + 1) == iDevice) && ((iNUMOfDevice % 64) != 0))
                    {
                        bReadCMD[10] = (byte)(iNUMOfDevice % 64);
                        iTemp = (iNUMOfDevice % 64);
                    }
                    else
                    {
                        bReadCMD[10] = 64;
                        iTemp = 64;
                    }

                    bReadCMD[11] = 0x0;

                    try
                    {
                        RXTXLog("READ BATCH", "DEFINE", 0, null, 0);
                        bf = new byte[(iTemp * 2) + 2];
                        iErr = ReadWritePLC(ref bf, bReadCMD, 12, (iTemp * 2) + 2);
                    }
                    catch
                    {
                        RXTXLog("READ BATCH", "FAIL", 0, null, 0);
                        iErr = -1;
                        ReConnect();
                        return iErr;
                    }

                    if (iErr > 0 && (iErr == (iTemp * 2) + 2))
                    {
                        if ((bf[0] == 0x81) & (bf[1] == 0x00))
                        {
                            for (int i = 0; i < iTemp; i++)
                            {
                                try
                                {
                                    // Convert each byte to bits and store them in the 'bits' array
                                    short value = BitConverter.ToInt16(bf, 2 + (i * 2));
                                    for (int bitIndex = 0; bitIndex < 16; bitIndex++)
                                    {
                                        int bitPosition = (i * 16) + bitIndex + (j * 64); // Global bit position
                                        bits[bitPosition] = (value & (1 << bitIndex)) != 0;
                                    }
                                }
                                catch
                                {
                                    return iErr = -4;
                                }
                            }
                            iErr = bf.Length;
                        }
                        else
                        {
                            return iErr = -3;
                        }

                        if ((j + 1) == iDevice)
                        {
                            if (iMEMMoveFlag)
                            {
                                try
                                {
                                    bool[] bTMP = new bool[iNUMOfDevice * 16];
                                    bool[] bDataBUFF = new bool[iNUMOfDevice * 16];
                                }
                                catch
                                {
                                    return iErr = -5;
                                }
                            }
                            return iErr;
                        }
                    }
                    else
                    {
                        return iErr = -2;
                    }
                }
            }
            else
            {
                iErr = -1;
            }

            return iErr;
        }
       

        // 用標籤呼叫 ReadBitsPLC
        public int ReadBitsPLC(out bool[] bits, string tag, int bitCount)
        {
            bits = Array.Empty<bool>();
            if (!TryParseTag(tag, out var dev, out var addr)) return -98; // 解析標籤失敗
            return ReadBitsPLC(ref bits, dev, addr, bitCount);  // 呼叫原有的 ReadBitsPLC，並傳遞 DEVICETYPE 和位址
        }

    }
}
