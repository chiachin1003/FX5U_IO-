using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace FX5U_IOMonitor.DatabaseProvider
{
    public static class CloudDbProvider
    {
        private static IServiceProvider? _provider;

        public static void Init()
        {
            if (_provider != null) return; // 避免重複初始化

            var services = new ServiceCollection();

            string connStr = $"Host={DbConfig.Cloud.IpAddress};Port={DbConfig.Cloud.Port};Database=element;Username={DbConfig.Cloud.UserName};Password={DbConfig.Cloud.Password}";

            services.AddDbContext<CloudDbContext>(options =>
                options.UseNpgsql(connStr),
                ServiceLifetime.Singleton);

            _provider = services.BuildServiceProvider();
        }

        public static CloudDbContext GetContext()
        {
            if (_provider == null)
                throw new InvalidOperationException("❌ CloudDbProvider 未初始化！請先呼叫 CloudDbProvider.Init()");

            return _provider.GetRequiredService<CloudDbContext>();
        }
    }
}
