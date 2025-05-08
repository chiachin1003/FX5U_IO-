using MachineIO_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace MachineIO_CRUD.DataAccess
{
	public class ApplicationDbContext : DbContext
	{
		string _dbName;

		public ApplicationDbContext() : base()
		{
			_dbName = "MachineIoTest";
		}

		public static string IpAddress = "localhost";
		public static string Port = "5432";
		public static string UserName = "postgres";
		public static string Password = "Admin123*";

		protected override void OnModelCreating( ModelBuilder modelBuilder )
		{
			base.OnModelCreating( modelBuilder );
		}

		protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
		{
			optionsBuilder
				.UseNpgsql( $"Host={IpAddress};Port={Port};Database={_dbName};Username={UserName};Password={Password};TrustServerCertificate=True" );
		}

		public DbSet<MachineIO> MachineIOs { get; set; }
		public DbSet<Machine> Machines { get; set; }
	}
}
