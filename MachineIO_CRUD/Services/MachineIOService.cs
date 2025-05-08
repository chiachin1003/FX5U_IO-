using MachineIO_CRUD.DataAccess;
using MachineIO_CRUD.Models;

namespace MachineIO_CRUD.Services
{
	public partial class MachineIOService : IDisposable
	{
		public MachineIOService()
		{
			_db = new ApplicationDbContext();
		}

		public int ErrorLine { get; private set; }

		public AddMachineIODataErrorCode AddMachineIOFromFile( string targetMachine, string fileName )
		{
			// Check target machine exists
			Machine? machine = _db.Machines.FirstOrDefault( m => m.Name == targetMachine );
			if( machine == null ) {
				machine = new() { Name = targetMachine };
				_db.Machines.Add( machine );
				_db.SaveChanges();
			}

			// Read file by stream
			using StreamReader reader = new( fileName );

			// Read header line
			string header = reader.ReadLine();

			string line;
			List<MachineIO> machineIOs = new();
			int curLineNo = 0;

			while( ( line = reader.ReadLine() ) != null ) {
				curLineNo++;

				// Split line by comma
				string[] values = line.Split( ',' );

				if( values.Length < SD.Num_Of_New_MachineIO_Property ) {
					ErrorLine = curLineNo;
					return AddMachineIODataErrorCode.WrongFileFormat;
				}

				// Create new MachineIO object
				MachineIO ioData = new();
				ioData.RelayType = values[ 0 ] switch
				{
					SD.MechanicalRelay => RelayType.Mechanical,
					SD.ElectronicRelay => RelayType.Electronic,
					_ => throw new ArgumentOutOfRangeException( values[ 0 ] )
				};
				ioData.IOType = values[ 1 ] switch
				{
					SD.Input => IOType.X,
					SD.Output => IOType.Y,
					_ => throw new ArgumentOutOfRangeException( values[ 1 ] )
				};
				ioData.Address = values[ 2 ].Substring( 1 );
				ioData.ClassTag = values[ 3 ];
				ioData.MaterialSerialNo = values[ 4 ];
				ioData.Description = values[ 5 ];

				if( !int.TryParse( values[ 6 ], out int remainingLifeTime ) ) {
					ErrorLine = curLineNo;
					return AddMachineIODataErrorCode.InvalidLifeTimeData;
				}
				ioData.TotalLifeTime = remainingLifeTime;

				ioData.MachineId = machine.Id;

				machineIOs.Add( ioData );
			}

			// Add new MachineIO to database
			_db.MachineIOs.AddRange( machineIOs );
			_db.SaveChanges();

			return AddMachineIODataErrorCode.None;
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~MachineIOService()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose( disposing: true );
			GC.SuppressFinalize( this );
		}
	}

	// protected functions and variables
	public partial class MachineIOService
	{
		protected virtual void Dispose( bool disposing )
		{
			if( !disposedValue ) {
				if( disposing ) {
					// TODO: dispose managed state (managed objects)
					_db?.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}
	}

	// private functions and variables
	public partial class MachineIOService
	{
		readonly ApplicationDbContext _db;
		private bool disposedValue;
	}
}
