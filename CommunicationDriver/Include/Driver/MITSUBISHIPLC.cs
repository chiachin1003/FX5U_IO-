/***********************************************************

 Class:  MITSUBISHIPLC

 Description:
   <Insert description here>

 Author: Alex Wu

 Version: 160416A

***********************************************************/
using CommunicationDriver.Include.Tools;
using System.Text.RegularExpressions;

namespace CommunicationDriver.Include.Driver
{
	public class CMITSUBISHIPLC : IMitsubishiPlc
	{
		private bool m_bLOGFlag;

		private CSOCKETNET m_cNet = new CSOCKETNET();

		private Queue<string> m_RXTXLog = new Queue<string>();
		private int m_iErr;
		private int m_iWTD;
		private int m_iRetry;
		private int m_iPORT;
		private string m_sADDRESS;
		private int m_iRECETIMEOUT;
		private int m_iSENDTIMEOUT;
		private CSOCKETNET.LINKMODE m_eMODE;

		private bool m_bCHKConnectFlag = false;

		public CMITSUBISHIPLC()
		{
			m_bLOGFlag = false;
			m_iErr = 0;
			m_iWTD = 0;
		}

		~CMITSUBISHIPLC()
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

		private byte GetHeader( int iHeader )
		{
			switch( iHeader ) {
				case 0:         //D
					return 0xA8;
				case 1:         //R
					return 0xAF;
				case 2:         //ZR
					return 0xB0;
				case 3:         //W
					return 0xB4;
				case 4:         //X
					return 0x9C;
				case 5:         //Y
					return 0x9D;
				case 6:         //M
					return 0x90;
				case 7:         //L
					return 0x92;
				case 8:         //B
					return 0xA0;

			}
			return 0x0;
		}
        private static readonly Dictionary<string, DEVICETYPE> DeviceMap = new(StringComparer.OrdinalIgnoreCase)
		{
			["X"] = DEVICETYPE.X,
			["Y"] = DEVICETYPE.Y,
			["M"] = DEVICETYPE.M,
			["D"] = DEVICETYPE.D,
			["B"] = DEVICETYPE.B,
			["S"] = DEVICETYPE.S,
            ["ZR"] = DEVICETYPE.ZR,
            ["R"] = DEVICETYPE.R,
            ["L"] = DEVICETYPE.L,
            ["C"] = DEVICETYPE.C,
        };

        private static bool TryParseDevice(string code, out DEVICETYPE dev)
            => DeviceMap.TryGetValue(code.Trim(), out dev);

        // X/Y/B 以 16 進位、M/S/D 以 10 進位
        private static int ParseAddress(DEVICETYPE dev, string addressText)
        {
            int numberBase = (dev == DEVICETYPE.X || dev == DEVICETYPE.Y || dev == DEVICETYPE.B) ? 16 : 10;
            return Convert.ToInt32(addressText.Trim(), numberBase);
        }

        // 支援 "M100" 合在一起的字串
        private static bool TryParseTag(string tag, out DEVICETYPE dev, out int address)
        {
            dev = default; address = 0;

            // 依 DeviceMap 產生（長碼優先）
            var keys = DeviceMap.Keys.OrderByDescending(k => k.Length);
            var pattern = @"^\s*(?<dev>" + string.Join("|", keys) + @")\s*(?<addr>[0-9A-Fa-f]+)\s*$";

            var m = Regex.Match(tag, pattern, RegexOptions.IgnoreCase);
            if (!m.Success) return false;

            var code = m.Groups["dev"].Value;
            var addrText = m.Groups["addr"].Value;

            if (!TryParseDevice(code, out dev)) return false;
            address = ParseAddress(dev, addrText); // X/Y/B 走 16進位，其餘 10進位（含 L）
            return true;
        }

        private static bool IsBitDevice(DEVICETYPE dev)
		 => dev == DEVICETYPE.X || dev == DEVICETYPE.Y || dev == DEVICETYPE.B
		 || dev == DEVICETYPE.M || dev == DEVICETYPE.S || dev == DEVICETYPE.L; 

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
        public int ReadBitsPLC(ref bool[] bits, DEVICETYPE eHeader, int iAddress, int iNUMOfDevice)
        {
            int iErr = 0;

            byte[] bReadCMD = new byte[21];
            //byte[] bf = new byte[2048];
            byte[] bf = new byte[(iNUMOfDevice * 2) + 11];  // 根據 iNUMOfDevice 動態調整回傳緩衝區大小

            int iDevice = iNUMOfDevice / 960;
            int iTemp;
            int arrayIndex = 0; // 用來追蹤 bits 陣列的正確索引位置

            // 如果資料不足960個則需要額外處理
            if ((iNUMOfDevice % 960) > 0)
                iDevice = iDevice + 1;

            for (int j = 0; j < iDevice; j++)
            {
                // 填寫讀取命令
                bReadCMD[0] = 0x50;
                bReadCMD[1] = 0x0;
                bReadCMD[2] = 0x0;
                bReadCMD[3] = 0xFF;
                bReadCMD[4] = 0xFF;
                bReadCMD[5] = 0x3;
                bReadCMD[6] = 0x0;
                bReadCMD[7] = 0x0C;
                bReadCMD[8] = 0x0;
                bReadCMD[9] = 0x10;
                bReadCMD[10] = 0x0;
                bReadCMD[11] = 0x1;
                bReadCMD[12] = 0x4;
                bReadCMD[13] = 0x0;
                bReadCMD[14] = 0x0;

                bReadCMD[15] = (Byte)((iAddress + (j * 960)) % 0x100);
                bReadCMD[16] = (Byte)((iAddress + (j * 960)) / 0x100);

                bReadCMD[17] = 0x0;
                bReadCMD[18] = GetHeader((int)eHeader);

                // 判斷需要讀取的設備數量
                if (((j + 1) == iDevice) && ((iNUMOfDevice % 960) != 0))
                {
                    bReadCMD[19] = (Byte)((iNUMOfDevice % 960) % 0x100);
                    bReadCMD[20] = (Byte)((iNUMOfDevice % 960) / 0x100);
                    iTemp = (iNUMOfDevice % 960);
                }
                else
                {
                    bReadCMD[19] = (Byte)(960 % 0x100);
                    bReadCMD[20] = (Byte)(960 / 0x100);
                    iTemp = 960;
                }

                try
                {
                    RXTXLog("READ BATCH", "DEFINE", 0, null, 0);
                    iErr = ReadWritePLC(ref bf, bReadCMD, 21, (iTemp * 2) + 11);
                }
                catch (Exception)
                {
                    RXTXLog("READ BATCH", "FAIL", 0, null, 0);
                    iErr = -2;
                    ReConnect();
                    break;
                }

                try
                {
                    if (iErr > 0 && (iErr == (iTemp * 2) + 11))
                    {
                        // 將每個 short 轉換為 bool[]
                        for (int i = 0; i < iTemp; i++)
                        {
                            short value = BitConverter.ToInt16(bf, 11 + (i * 2));
                            for (int bitIndex = 0; bitIndex < 16; bitIndex++)
                            {
                                // 將每個 bit 轉換為 bool 值，並填入 bits 陣列
                                bool bit = (value & (1 << bitIndex)) != 0;
                                bits[arrayIndex] = bit;
                                arrayIndex++;
                            }
                        }
                        RXTXLog("READ BATCH CMD RX", "SUCCESS", 0, bf, iErr);
                        UpDataWTD();
                        m_iErr = 0;

                        if ((j + 1) == iDevice)
                        {
                            return arrayIndex; // 返回實際讀取的 bit 數量
                        }
                    }
                    else
                    {
                        iErr = -4; // 讀取失敗
                        break;
                    }
                }
                catch (Exception)
                {
                    iErr = -3;
                    break;
                }
            }

            return iErr;
        }

       
        public int ReadBitsPLC(out bool[] bits, string deviceCodeAndAddress, int bitCount)
        {
            bits = new bool[bitCount];

            // 解析設備代碼和地址（假設格式為 "X100"）
            string deviceCode = deviceCodeAndAddress.Substring(0, 1);  // 提取設備代碼
            int iAddress = int.Parse(deviceCodeAndAddress.Substring(1));  // 提取地址，假設地址部分為數字

            // 使用 DeviceMap 查找設備代碼對應的 DEVICETYPE
            if (!DeviceMap.TryGetValue(deviceCode, out var devType))
            {
                return -99;  // 無法解析設備代碼，返回錯誤
            }

            // 呼叫原本的 ReadBitsPLC 方法
            return ReadBitsPLC(ref bits, devType, iAddress, 20);
        }
        public int ReadBatchPLC( ref short[] pw, DEVICETYPE eHeader, int iAddress, int iNUMOfDevice )
		{


			int iErr = 0;

			byte[] bReadCMD = new byte[ 21 ];
			byte[] bf = new byte[ 2048 ];
			//int iCHKTime = 0;

			int iDevice = iNUMOfDevice / 960;
			int iTemp;
            int arrayIndex = 0; // 新增：用來追蹤 pw 陣列的正確索引位置

            //if ((int)eHeader <= 3)
            {
                if ( ( iNUMOfDevice % 960 ) > 0 )
					iDevice = iDevice + 1;

				for( int j = 0; j < iDevice; j++ ) {
					bReadCMD[ 0 ] = 0x50;
					bReadCMD[ 1 ] = 0x0;
					bReadCMD[ 2 ] = 0x0;
					bReadCMD[ 3 ] = 0xFF;
					bReadCMD[ 4 ] = 0xFF;
					bReadCMD[ 5 ] = 0x3;
					bReadCMD[ 6 ] = 0x0;
					bReadCMD[ 7 ] = 0x0C;
					bReadCMD[ 8 ] = 0x0;
					bReadCMD[ 9 ] = 0x10;
					bReadCMD[ 10 ] = 0x0;
					bReadCMD[ 11 ] = 0x1;
					bReadCMD[ 12 ] = 0x4;
					bReadCMD[ 13 ] = 0x0;
					bReadCMD[ 14 ] = 0x0;

                    bReadCMD[ 15 ] = (Byte)( ( iAddress + ( j * 960 ) ) % 0x100 );
					bReadCMD[ 16 ] = (Byte)( ( iAddress + ( j * 960 ) ) / 0x100 );

					bReadCMD[ 17 ] = 0x0;
					bReadCMD[ 18 ] = GetHeader( (int)eHeader );

					if( ( ( j + 1 ) == iDevice ) && ( ( iNUMOfDevice % 960 ) != 0 ) ) {
						bReadCMD[ 19 ] = (Byte)( ( iNUMOfDevice % 960 ) % 0x100 );
						bReadCMD[ 20 ] = (Byte)( ( iNUMOfDevice % 960 ) / 0x100 );
						iTemp = ( iNUMOfDevice % 960 );
					}
					else {
						bReadCMD[ 19 ] = (Byte)( 960 % 0x100 );
						bReadCMD[ 20 ] = (Byte)( 960 / 0x100 );
						iTemp = 960;
					}

					try {
						RXTXLog( "READ BATCH", "DEFINE", 0, null, 0 );
						iErr = ReadWritePLC( ref bf, bReadCMD, 21, ( iTemp * 2 ) + 11 );
					}
					catch( Exception ) {
						RXTXLog( "READ BATCH", "FAIL", 0, null, 0 );
						iErr = -2;
						ReConnect();
						break;
					}

					try {
						if( iErr > 0 && ( iErr == ( iTemp * 2 ) + 11 ) ) {
                            for (int i = 0; i < iTemp; i++)
                            {
                                pw[arrayIndex] = BitConverter.ToInt16(bf, 11 + (i * 2));
                                arrayIndex++; // 遞增陣列索引
                            }
                            RXTXLog( "READ BATCH CMD RX", "SUCCESS", 0, bf, iErr );
							UpDataWTD();
							m_iErr = 0;
                            if ((j + 1) == iDevice)
                            {
                                return arrayIndex; // 返回實際讀取的 word 數量
                            }
                            
						}
                        else
                        {
                            iErr = -4; // 讀取失敗
                            break;
                        }
                    }
					catch( Exception ) {
						iErr = -3;
						break;
					}

				}
			}
			//else iErr = -1;

			return iErr;

		}

        public int ReadBatchPLC(ref short[] pw, string deviceCode, int address, int wordCount, bool alignBitHead = true)
        {
            if (!TryParseDevice(deviceCode, out var dev)) return -99; // 未知裝置

            int start = address;
            int count = wordCount;

            // 若是 bit 裝置，必要時對齊到 16 的倍數（word 起點）
            if (alignBitHead && IsBitDevice(dev) && (start % 16 != 0))
                start = (start / 16) * 16;

            return ReadBatchPLC(ref pw, dev, start, count);
        }

        //  合併標籤（"X1A" / "D100"） 讀取 word 數
        public int ReadBitDevice(ref short[] pw, string tag, int wordCount, bool alignBitHead = true)
        {
            if (!TryParseTag(tag, out var dev, out var address)) return -98; // 解析失敗

            int start = address;
            int count = wordCount;

            if (alignBitHead && IsBitDevice(dev) && (start % 16 != 0))
                start = (start / 16) * 16;

            return ReadBatchPLC(ref pw, dev, start, count);
        }
        public int WriteBatchPLC(short[] pw, DEVICETYPE eHeader, int iAddress, int iNUMOfDevice )
		{
			int iErr = 0;

			//int iCHKTime = 0;
			byte[] bWriteCMD = new byte[ 2048 ];
			byte[] bf = new byte[ 32 ];

			int iDevice = 0;
			int iTemp;
			int i = 0;
			int j = 0;

			if( (int)eHeader <= 3 ) {
				if( ( iNUMOfDevice % 960 ) > 0 ) {
					iDevice = iNUMOfDevice / 960;
					iDevice = iDevice + 1;
				}
				else {
					iDevice = iNUMOfDevice / 960;
				}

				if( iDevice <= 0 )
					iDevice = 1;


				for( j = 0; j < iDevice; j++ ) {
					bWriteCMD[ 0 ] = 0x50;
					bWriteCMD[ 1 ] = 0x0;
					bWriteCMD[ 2 ] = 0x0;
					bWriteCMD[ 3 ] = 0xFF;
					bWriteCMD[ 4 ] = 0xFF;
					bWriteCMD[ 5 ] = 0x3;
					bWriteCMD[ 6 ] = 0x0;
					bWriteCMD[ 7 ] = 0x16;
					bWriteCMD[ 8 ] = 0x0;
					bWriteCMD[ 9 ] = 0x10;
					bWriteCMD[ 10 ] = 0x0;
					bWriteCMD[ 11 ] = 0x1;
					bWriteCMD[ 12 ] = 0x14;
					bWriteCMD[ 13 ] = 0x0;
					bWriteCMD[ 14 ] = 0x0;

					bWriteCMD[ 15 ] = (Byte)( ( iAddress + ( j * 960 ) ) % 0x100 );
					bWriteCMD[ 16 ] = (Byte)( ( iAddress + ( j * 960 ) ) / 0x100 );

					bWriteCMD[ 17 ] = 0x0;
					bWriteCMD[ 18 ] = GetHeader( (int)eHeader );

					if( ( ( j + 1 ) == iDevice ) && ( ( iNUMOfDevice % 960 ) != 0 ) ) {
						bWriteCMD[ 19 ] = (Byte)( ( iNUMOfDevice % 960 ) % 0x100 );
						bWriteCMD[ 20 ] = (Byte)( ( iNUMOfDevice % 960 ) / 0x100 );
						iTemp = ( iNUMOfDevice % 960 );
					}
					else {
						bWriteCMD[ 19 ] = (Byte)( 960 % 0x100 );
						bWriteCMD[ 20 ] = (Byte)( 960 / 0x100 );
						iTemp = 960;
					}

					bWriteCMD[ 7 ] = (Byte)( ( 0x0C + ( iTemp * 2 ) ) % 0x100 );
					bWriteCMD[ 8 ] = (Byte)( ( 0x0C + ( iTemp * 2 ) ) / 0x100 );

					i = 0;
					for( i = 0; i < iTemp; i++ ) {
						bWriteCMD[ ( i * 2 ) + 21 ] = (Byte)( pw[ i + ( j * 960 ) ] % 0x100 );
						bWriteCMD[ ( i * 2 ) + 21 + 1 ] = (Byte)( pw[ i + ( j * 960 ) ] / 0x100 );
					}

					try {
						RXTXLog( "WRITE BATCH", "DEFINE", 0, null, 0 );
						iErr = ReadWritePLC( ref bf, bWriteCMD, 21 + ( iTemp * 2 ), 11 );
					}
					catch( Exception ) {
						RXTXLog( "WRITE BATCH", "FAIL", 0, null, 0 );
						iErr = -2;
						ReConnect();
						break;
					}

					if( iErr > 0 && ( iErr == 11 ) ) {
						RXTXLog( "WRITE BATCH CMD RX", "SUCCESS", 0, bf, iErr );
						UpDataWTD();
						m_iErr = 0;
						if( ( j + 1 ) == iDevice )
							return iErr;
						else
							break;
					}
					else {
						RXTXLog( "WRITE BATCH CMD RX j=" + j.ToString(), "FAIL", 0, bf, 0 );
						ReConnect();
						iErr = -3;
						break;
					}

				}
			}
			else
				iErr = -1;

			return iErr;
		}

		public int WriteBitPLC( int iNO_OFF, DEVICETYPE eHeader, int iAddress )
		{
			int iErr = 0;
			//int iCHKTime = 0;
			byte[] bWriteCMD = new byte[ 32 ];
			byte[] bf = new byte[ 32 ];

			if( (int)eHeader > 3 ) {
				bWriteCMD[ 0 ] = 0x50;
				bWriteCMD[ 1 ] = 0x0;
				bWriteCMD[ 2 ] = 0x0;
				bWriteCMD[ 3 ] = 0xFF;
				bWriteCMD[ 4 ] = 0xFF;
				bWriteCMD[ 5 ] = 0x3;
				bWriteCMD[ 6 ] = 0x0;
				bWriteCMD[ 7 ] = 0xC;
				bWriteCMD[ 8 ] = 0x0;
				bWriteCMD[ 9 ] = 0x10;
				bWriteCMD[ 10 ] = 0x0;
				bWriteCMD[ 11 ] = 0x2;
				bWriteCMD[ 12 ] = 0x14;
				bWriteCMD[ 13 ] = 0x1;
				bWriteCMD[ 14 ] = 0x0;
				bWriteCMD[ 15 ] = 0x1;
				bWriteCMD[ 16 ] = (Byte)( ( iAddress ) % 0x100 );
				bWriteCMD[ 17 ] = (Byte)( ( iAddress ) / 0x100 );
				bWriteCMD[ 18 ] = 0x0;
				bWriteCMD[ 19 ] = GetHeader( (int)eHeader );
				bWriteCMD[ 20 ] = (Byte)iNO_OFF;



				try {
					RXTXLog( "WRITE BATCH", "DEFINE", 0, null, 0 );
					iErr = ReadWritePLC( ref bf, bWriteCMD, 21, 11 );
				}
				catch( Exception ) {
					RXTXLog( "WRITE BATCH", "FAIL", 0, null, 0 );
					iErr = -2;
					ReConnect();
					return iErr;
				}

				if( iErr > 0 && ( iErr == 11 ) ) {
					RXTXLog( "WRITE RST/SET CMD RX", "SUCCESS", 0, bf, iErr );
					UpDataWTD();
					return iErr;
				}
				else {
					RXTXLog( "WRITE RST/SET CMD RX", "FAIL", 0, bf, 0 );
					ReConnect();
					return iErr = -3;
				}
			}
			else
				iErr = -1;

			return iErr;
		}

        public int WriteBatchPLC(short[] pw, string deviceCode, int address, int wordCount, bool alignBitHead = true)
        {
            if (pw == null || pw.Length == 0) return -97;      // 資料不可為空
            if (!TryParseDevice(deviceCode, out var dev)) return -99; // 未知裝置

            int start = address;

            // bit 裝置用 word 單位寫入，預設先對齊 head 到 16 的倍數，避免內部再位移
            if (alignBitHead && IsBitDevice(dev) && (start % 16 != 0))
                start = (start / 16) * 16;

            int count = Math.Min(wordCount, pw.Length);  // 安全起見，不超出陣列
            return WriteBatchPLC(pw, dev, start, count);
        }

        // B) 用合併標籤（例如 "D100"、"X1A"）
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
    }
}
