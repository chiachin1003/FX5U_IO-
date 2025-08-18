using CommunicationDriver.Include.Tools;

namespace CommunicationDriver.Include.Driver
{
	public interface IMitsubishiPlc
	{
		// Device type enum (you may need to use a shared enum or alias)
		// If the DEVICETYPE enums differ, consider extracting a common one.
		// For now, use object for DEVICETYPE to allow both.

		int CreatePLC( CSOCKETNET.LINKMODE eMODE, string sADDRESS, int iPORT, int iRECETIMEOUT, int iSENDTIMEOUT, int iRetry );
		int ClosePLC();
		bool IsConnect();
		int GetWTD();
		string RepLog();
		int RepLogCount();
		void SETDEBUG();
		void RSTDEBUG();
		int ReadBatchPLC( ref short[] pw, DEVICETYPE eHeader, int iAddress, int iNUMOfDevice );
		int ReadBatchPLC(ref short[] pw, string deviceCode, int address, int wordCount, bool alignBitHead = true);
		int ReadBitDevice(ref short[] pw, string tag, int wordCount, bool alignBitHead = true);

		int ReadBitsPLC(ref bool[] bits, DEVICETYPE eHeader, int iAddress, int iNUMOfDevice);

		int ReadBitsPLC(out bool[] bits, string tag, int bitCount);

        int WriteBatchPLC(short[] pw, DEVICETYPE eHeader, int iAddress, int iNUMOfDevice );
		int WriteBatchPLC(short[] pw, string deviceCode, int address, int wordCount, bool alignBitHead = true);
        int WriteBatchPLC(short[] pw, string tag, int wordCount, bool alignBitHead = true);

        int WriteBitPLC( int iNO_OFF, DEVICETYPE eHeader, int iAddress );
	}
}