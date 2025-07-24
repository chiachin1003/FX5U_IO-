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

            //services.AddDbContext< ApplicationDB>(options =>
            //    options.UseNpgsql(connStr),
            //    ServiceLifetime.Singleton);

          
            //// 註冊 UserManager 和 RoleManager
            //services.AddIdentityCore<ApplicationUser>(options =>
            //{
            //    //options.Password.RequireDigit = true;
            //    //options.Password.RequiredLength = 6;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    //options.Password.RequireLowercase = true;
            //})
            //.AddRoles<IdentityRole>() // 註冊角色管理
            //.AddEntityFrameworkStores<ApplicationDB>(); // 使用 Entity Framework 儲存使用者和角色

            // 註冊 DbContext 與 Identity
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
