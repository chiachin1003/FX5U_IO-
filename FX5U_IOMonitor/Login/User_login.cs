using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using FX5U_IOMonitor.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;


namespace FX5U_IOMonitor.Login
{
   
    public class ScheduleTag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
    public static class SD
    {
        public const int ROUND_DIGIT = 1;

        public const string Admin_Account = "admin";
        public const string Admin_Password = "admin123*";

        public const string Role_Admin = "Admin";
        public const string Role_Operator = "Operator";
        public const string Role_User = "User";

        public const string Default_Schedule_Tag = "None";

        public const string Default_Email = "jenny963200@hotmail.com";
        public const string Default_Line ="U9941555f9b9a028b5e89b587ef8877cf";

    }
     
    public enum UserErrorCode
    {
        None,
        NotExist,
        PasswordError,
    }

    public class ApplicationUser: IdentityUser
    {
        public string? LineNotifyToken { get; set; }
        public bool NotifyByEmail { get; set; } 
        public bool NotifyByLine { get; set; } 
    }
    
    public partial class UserService<TContext> : IDisposable where TContext : IdentityDbContext<ApplicationUser>
    {
       
        public UserService()
        {
            // 建立 DI 容器
            var services = new ServiceCollection();

            // 註冊服務
            ConfigureServices(services);

            // 建立服務提供者
            var serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetRequiredService<TContext>();
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        }

        public static ApplicationUser CurrentUser => _curUser;
        public static string CurrentRole => _curRole;
        public UserManager<ApplicationUser> UserManager => _userManager;

        public async Task CreateDefaultUserAsync()
        {
            // Check if the admin user already exists
            var adminUser = await _userManager.FindByNameAsync(SD.Admin_Account);
                  
            
            if (adminUser is not null)
            {
                return;
            }

            // Ensure the roles exist
            bool isRoleExist = await _roleManager.RoleExistsAsync(SD.Role_Admin);
            if (isRoleExist == false)
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Operator));
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_User));
            }

            await CreateUserAsync(SD.Admin_Account, SD.Admin_Password, SD.Role_Admin ,SD.Default_Email ,SD.Default_Line);
        }
        public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<UserErrorCode> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName) as ApplicationUser;

            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, password);

                if (result)
                {
                    _curUser = user;
                    _curRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    return UserErrorCode.None;
                }
                else
                {
                    return UserErrorCode.PasswordError;
                }
            }
            else
            {
                return UserErrorCode.NotExist;
            }
        }

        public void Logout()
        {
            _curUser = null;
            _curRole = null;
        }

        public async Task<List<ApplicationUser>> GetAllAsync(string role)
        {
            var users = _userManager.Users.ToList(); // Fetch users from the database
            var result = new List<ApplicationUser>();
            foreach (var user in users)
            {
                bool isInRole = await _userManager.IsInRoleAsync(user, role);
                if (isInRole)
                {
                    result.Add(new ApplicationUser { UserName = user.UserName, Email = user.Email, LineNotifyToken = user.LineNotifyToken });
                }
            }
            return result;
        }

       
        public List<ApplicationUser> GetAllUser()
        {
            var users = _userManager.Users.ToList(); // Fetch users from the database
            var result = new List<ApplicationUser>();
            foreach (var user in users)
            {
                result.Add(new ApplicationUser { UserName = user.UserName, Email = user.Email, LineNotifyToken = user.LineNotifyToken });
            }
            return result;
        }
       
        public bool CheckUserExist(string userName)
        {
            var user = _userManager.Users
                .FirstOrDefault(u => u.UserName == userName);
            return user != null;
        }
        /// <summary>
        /// 創建帳號
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task CreateUserAsync(string userName, string password, string role ,string email, string line)
        {
            bool hasEmail = !string.IsNullOrWhiteSpace(email);
            bool hasLine = !string.IsNullOrWhiteSpace(line);
            // Create the admin user
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,        // ✅ 設定 email
                EmailConfirmed = true,       // ✅ 若你不需要驗證流程，可以直接標記已驗證
                LineNotifyToken = line,
                NotifyByEmail = hasEmail,
                NotifyByLine = hasLine
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            else
            {
                // Handle errors if needed
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
        }
        /// <summary>
        /// 刪除使用者
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task DeleteUserAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
       
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                    _userManager.Dispose();
                    _roleManager.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~UserService()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public partial class UserService<TContext>
    {
        readonly TContext _context;
        readonly UserManager<ApplicationUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;

        static ApplicationUser _curUser = null;
        static string _curRole = null;

        bool disposedValue;

        private static void ConfigureServices(ServiceCollection services)
        {
            // 註冊 DbContext
            services.AddDbContext<TContext>();

            // 註冊 UserManager 和 RoleManager
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                //options.Password.RequireDigit = true;
                //options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                //options.Password.RequireLowercase = true;
            })
            .AddRoles<IdentityRole>() // 註冊角色管理
            .AddEntityFrameworkStores<TContext>(); // 使用 Entity Framework 儲存使用者和角色
        }

    }

}
