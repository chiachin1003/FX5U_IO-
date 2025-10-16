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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Timer = System.Threading.Timer;
using System.Diagnostics;
using System.Linq.Expressions;
using FX5U_IOMonitor.Config;

namespace FX5U_IOMonitor.Models
{

   
    /// <summary>
    /// 地端資料庫
    /// </summary>
    public class ApplicationDB : IdentityDbContext<ApplicationUser>  
    {
        readonly string _dbFullName;
        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options){}
        public ApplicationDB() : base()
        {
            _dbFullName = "element";
        }
        public DbSet<Machine_number> Machine { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Order { get; set; }

        public DbSet<MachineIO> Machine_IO { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Utilization_Record> UtilizationRate { get; set; }
        public DbSet<UtilizationStatusRecord> UtilizationStatusRecord { get; set; }
        public DbSet<ServoDriveAlarm> ServoDriveAlarm { get; set; }
        public DbSet<ControlAlarm> ControlAlarm { get; set; }

        public DbSet<Alarm> alarm { get; set; }
        public DbSet<AlarmHistory> AlarmHistories { get; set; }
        public DbSet<MachineParameter> MachineParameters { get; set; }
        public DbSet<FrequencyConverAlarm> FrequencyConverAlarm { get; set; }
        public DbSet<DisconnectRecord> DisconnectRecords { get; set; }

        public DbSet<MachineParameterHistoryRecode> MachineParameterHistoryRecodes { get; set; }
        public DbSet<Blade_brand> Blade_brand { get; set; }
        public DbSet<Blade_brand_TPI> Blade_brand_TPI { get; set; }
        public DbSet<Language> Language { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ScheduleTag>().HasData(
                new ScheduleTag { Id = 1, Name = SD.Default_Schedule_Tag });

            // OnModelCreating
            modelBuilder.Entity<MachineIO>()
                .HasIndex(x => new { x.Machine_name, x.address })
                .IsUnique();

            modelBuilder.Entity<MachineIOTranslation>()
                .HasIndex(x => new { x.MachineIOId, x.LanguageCode })
                .IsUnique(); // 依你的實務唯一鍵調整

            // 為所有實體表添加同步欄位
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(SyncableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(SyncableEntity.IsSynced))
                        .HasDefaultValue(false);

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(SyncableEntity.CreatedAt))
                        .HasDefaultValueSql("NOW()");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(SyncableEntity.UpdatedAt))
                        .HasDefaultValueSql("NOW()");
                }
            }

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql($"Host={DbConfig.Local.IpAddress};Port={DbConfig.Local.Port};" +
                    $"Database={_dbFullName};Username={DbConfig.Local.UserName};Password={DbConfig.Local.Password}");
            }
        }
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is SyncableEntity &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (SyncableEntity)entry.Entity;
                entity.UpdatedAt = DateTime.UtcNow;

                if (entry.State == EntityState.Modified)
                {
                    entity.IsSynced = false; // 標記為需要同步
                }
            }
        }
    }
    /// <summary>
    /// 雲端資料庫
    /// </summary>
    public class CloudDbContext : IdentityDbContext<ApplicationUser>
    {
        readonly string _dbFullName = "element";

        public DbSet<Machine_number> Machine { get; set; }

        public DbSet<MachineIO> Machine_IO { get; set; }
        //public DbSet<MachineIOTranslation> MachineIOTranslations { get; set; }
        public DbSet<History> Histories { get; set; }

        public DbSet<Alarm> alarm { get; set; }
        public DbSet<AlarmHistory> AlarmHistories { get; set; }
        public DbSet<MachineParameter> MachineParameters { get; set; }
        public DbSet<Utilization_Record> UtilizationRate { get; set; }

        public DbSet<MachineParameterHistoryRecode> MachineParameterHistoryRecodes { get; set; }
        public DbSet<Blade_brand> Blade_brand { get; set; }
        public DbSet<Blade_brand_TPI> Blade_brand_TPI { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<FrequencyConverAlarm> FrequencyConverAlarm { get; set; }
        public DbSet<DisconnectRecord> DisconnectRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            if (!optionsBuilder.IsConfigured)
            {
                string Cloud_IpAddress = DbConfig.Cloud.IpAddress;
                string Cloud_Port = DbConfig.Cloud.Port;
                string Cloud_UserName = DbConfig.Cloud.UserName;
                string Cloud_Password = DbConfig.Cloud.Password;

                optionsBuilder.UseNpgsql($"Host={Cloud_IpAddress};Port={Cloud_Port};Database={_dbFullName};Username={Cloud_UserName};Password={Cloud_Password}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityTypes = typeof(CloudDbContext).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(SyncableEntity).IsAssignableFrom(t));

            foreach (var entityType in entityTypes)
            {
                var entity = modelBuilder.Model.FindEntityType(entityType);
                if (entity == null)
                {
                    modelBuilder.Entity(entityType);
                }
                //if (!modelBuilder.Model.FindEntityType(entityType.Name).IsOwned())
                //{
                //    modelBuilder.Entity(entityType);
                //}
            }

            // 為所有實體表添加同步欄位（與 ApplicationDB 保持一致）
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(SyncableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(SyncableEntity.IsSynced))
                        .HasDefaultValue(false);

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(SyncableEntity.CreatedAt))
                        .HasDefaultValueSql("NOW()");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(SyncableEntity.UpdatedAt))
                        .HasDefaultValueSql("NOW()");
                }
            }

        }
        public static CloudDbContext? TryConnectCloud(out string? error)
        {
            try
            {
                var context = new CloudDbContext(); // ✅ 自動走 OnConfiguring()
                if (!context.Database.CanConnect())
                {
                    error = "資料庫無法連線（CanConnect 回傳 false）";
                    return null;
                }

                error = null;
                return context;
            }
            catch (Exception ex)
            {
                error = $"連線過程中發生錯誤：{ex.Message}";
                return null;
            }
        }
    }

}

//public static string Local_IpAddress = "localhost";
//public static string Local_Port = "5430";
//public static string Local_UserName = "postgres";
//public static string Local_Password = "963200";

//public static string Local_IpAddress = "ssiopgsql.postgres.database.azure.com";
//public static string Local_Port = "5432";
//public static string Local_UserName = "itritus";
//public static string Local_Password = "Itrics687912O@";