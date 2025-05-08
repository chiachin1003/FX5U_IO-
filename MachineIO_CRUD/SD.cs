using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineIO_CRUD
{
	public static class SD
	{
		public const string MechanicalRelay = "TRUE";
		public const string ElectronicRelay = "FALSE";
		public const string Input = "1";
		public const string Output = "0";
		public const int Num_Of_New_MachineIO_Property = 7;
	}

	public enum AddMachineIODataErrorCode
	{
		None,
		WrongFileFormat,
		InvalidLifeTimeData,
	}

	public enum RelayType
	{
		Mechanical,
		Electronic
	}

	public enum IOType
	{
		X,
		Y
	}
}
