using FX5U_IOMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FX5U_IOMonitor.Login;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using Npgsql;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.X86;
using System.Timers;

namespace FX5U_IOMonitor.Models
{
    public class ApplicationDB : IdentityDbContext<IdentityUser>
    {
        readonly string _dbFullName;

        public ApplicationDB() : base()
        {
            //var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
            //_dbFullName = Path.Combine(projectRoot, "Database", "element.db");
            _dbFullName = "element.db";
        }
        public DbSet<MachineIO> Machine_IO { get; set; }
        public DbSet<Machine_number> index { get; set; }
        public DbSet<History> Histories { get; set; }
       
        public DbSet<Alarm> alarm { get; set; }

        public DbSet<Blade_brand> Blade_brand { get; set; }
        public DbSet<Blade_brand_TPI> Blade_brand_TPI { get; set; }
        public DbSet<MachineParameter> MachineParameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ScheduleTag>().HasData(
                new ScheduleTag { Id = 1, Name = SD.Default_Schedule_Tag });

           
        }

        //public static string IpAddress = "ssiopgsql.postgres.database.azure.com";
        //public static string Port = "5432";
        //public static string UserName = "itritus";
        //public static string Password = "Itrics687912O@";

        public static string IpAddress = "localhost";
        public static string Port = "5430";
        public static string UserName = "postgres";
        public static string Password = "963200";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql($"Host={IpAddress};Port={Port};Database={_dbFullName};Username={UserName};Password={Password};TrustServerCertificate=True");

            //// Set database directory
            //var dbPath = Path.GetDirectoryName(_dbFullName);
            //if (dbPath != null)
            //{
            //    Directory.CreateDirectory(dbPath);
            //}
            //optionsBuilder.UseSqlite($"Data Source={_dbFullName}");
        }

       

    }
}
