using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineIO_CRUD.Models
{
	public class MachineIO
	{
		[Key]
		public int Id { get; set; }

		[Required, Browsable( false )]
		public int MachineId { get; set; }

		[Browsable( false )]
		public Machine? Machine { get; set; }

		[NotMapped]
		public string MachineName => Machine?.Name ?? string.Empty;


		public RelayType RelayType { get; set; }
		public IOType IOType { get; set; }
		public string Address { get; set; }
		public string ClassTag { get; set; } = string.Empty;
		public string MaterialSerialNo { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int TotalLifeTime { get; set; }
		public int RemainingLifeTime { get; set; }
	}
}
