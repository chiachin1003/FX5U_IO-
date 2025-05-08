using System.ComponentModel.DataAnnotations;

namespace MachineIO_CRUD.Models
{
	public class Machine
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
