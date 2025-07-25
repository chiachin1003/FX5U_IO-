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

            try
            {
                using var scope = _provider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<CloudDbContext>();
                if (!context.Database.CanConnect())
                {
                    MessageBox.Show("❌ 無法連線至雲端資料庫，請確認連線設定是否正確！", "連線失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _provider = null; // 重設，避免後續誤用
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ 初始化雲端資料庫時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _provider = null;
            }
        }

        public static CloudDbContext? GetContext()
        {
            try
            {
                return _provider.GetRequiredService<CloudDbContext>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
