using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Login;
using FX5U_IOMonitor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Org.BouncyCastle.Tls;


namespace FX5U_IOMonitor.DatabaseProvider
{
    public static class LocalDbProvider
    {
        private static IServiceProvider? _provider;

        public static void Init()
        {
            if (_provider != null) return;

            var services = new ServiceCollection();

            string connStr = $"Host={DbConfig.Local.IpAddress};Port={DbConfig.Local.Port};Database=element;Username={DbConfig.Local.UserName};Password={DbConfig.Local.Password}";

            services.AddDbContext<ApplicationDB>(options => options.UseNpgsql(connStr));

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDB>();

            _provider = services.BuildServiceProvider();

        }

        public static UserService<ApplicationDB> GetUserService()
        {
            if (_provider == null)
                throw new InvalidOperationException("❌ LocalDbProvider 尚未初始化");

            return new UserService<ApplicationDB>(_provider);
        }
    }

}
