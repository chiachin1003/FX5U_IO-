using MachineIO_CRUD.DataAccess;
using MachineIO_CRUD.Models;
using MachineIO_CRUD.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MachineIO_CRUD
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			using var db = new ApplicationDbContext();
			db.Database.EnsureCreated();

			UpdateSearchComboBox();
		}

		void UpdateSearchComboBox()
		{
			using ApplicationDbContext db = new();
			var machines = db.Machines.ToList();
			_cbSelectMachine.Items.Clear();
			foreach( var machine in machines ) {
				_cbSelectMachine.Items.Add( machine.Name );
			}
			_cbSelectMachine.SelectedIndex = -1;

			_cbSelectIOType.Items.Clear();
			_cbSelectIOType.Items.AddRange( Enum.GetNames( typeof( IOType ) ) );
			_cbSelectIOType.SelectedIndex = 0;

			_cbSelectRelayType.Items.Clear();
			_cbSelectRelayType.Items.AddRange( Enum.GetNames( typeof( RelayType ) ) );
			_cbSelectRelayType.SelectedIndex = 0;
		}

		private void _btnAdd_Click( object sender, EventArgs e )
		{
			// Check if machine name is empty
			if( string.IsNullOrEmpty( _txtMachineName.Text ) ) {
				MessageBox.Show( "Please enter a machine name." );
				return;
			}

			// Select file through OpenFileDialog
			OpenFileDialog openFileDialog = new();
			openFileDialog.Filter = "Csv Files|*.csv";
			openFileDialog.Multiselect = false;
			openFileDialog.Title = "Select a Machine IO Csv file";
			if( openFileDialog.ShowDialog() != DialogResult.OK ) {
				return;
			}

			using MachineIOService machineIOService = new();
			var ErrorCode = machineIOService.AddMachineIOFromFile( _txtMachineName.Text, openFileDialog.FileName );

			AddDataErrorCodeHandler( machineIOService.ErrorLine, ErrorCode );

			UpdateSearchComboBox();
		}

		private static void AddDataErrorCodeHandler( int errorLine, AddMachineIODataErrorCode ErrorCode )
		{
			switch( ErrorCode ) {
				case AddMachineIODataErrorCode.WrongFileFormat:
					MessageBox.Show( $"Wrong file format. Please check line {errorLine}." );
					break;
				case AddMachineIODataErrorCode.InvalidLifeTimeData:
					MessageBox.Show( $"Invalid Life Time data format. Please check line {errorLine}." );
					break;
				case AddMachineIODataErrorCode.None:
					MessageBox.Show( "Data added successfully." );
					break;
				default:
					MessageBox.Show( "Unknown error." );
					break;
			}
		}

		private void _btnSearch_Click( object sender, EventArgs e )
		{
			using ApplicationDbContext db = new();
			var machineName = _cbSelectMachine.Text;
			var ioType = (IOType)Enum.Parse( typeof( IOType ), _cbSelectIOType.Text );
			var relayType = (RelayType)Enum.Parse( typeof( RelayType ), _cbSelectRelayType.Text );

			var machineIOs = db.MachineIOs
				.Include( m => m.Machine )
				.Where( m => m.Machine.Name == machineName )
				.Where( m => m.IOType == ioType )
				.Where( m => m.RelayType == relayType )
				.ToList();

			// Clear previous data
			_dgvMachineIOs.DataSource = new BindingList<MachineIO>( machineIOs );
			_dgvMachineIOs.AutoResizeColumns();
		}
	}
}
