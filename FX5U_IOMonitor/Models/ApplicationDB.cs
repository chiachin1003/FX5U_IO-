using FX5U_IOMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FX5U_IOMonitor.Login;

namespace FX5U_IOMonitor.Models
{
    public class ApplicationDB : IdentityDbContext<IdentityUser>
    {
        readonly string _dbFullName;

        public ApplicationDB() : base()
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
            _dbFullName = Path.Combine(projectRoot, "Database", "element.db");
        }

        public DbSet<Drill_MachineIO> Drill_IO { get; set; }
        public DbSet<Sawing_MachineIO> Sawing_IO { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Alarm> alarm { get; set; }
        public DbSet<MachineParameter> MachineParameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ScheduleTag>().HasData(
                new ScheduleTag { Id = 1, Name = SD.Default_Schedule_Tag });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Set database directory
            var dbPath = Path.GetDirectoryName(_dbFullName);
            if (dbPath != null)
            {
                Directory.CreateDirectory(dbPath);
            }

            optionsBuilder.UseSqlite($"Data Source={_dbFullName}");
        }
    }
}
