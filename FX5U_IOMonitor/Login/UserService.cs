using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public const string Default_Email = null;
        public const string Default_Line = null;
        public const bool NotifyByEmail = false;
        public const bool NotifyByLine = false;

    }

    public enum UserErrorCode
    {
        None,
        NotExist,
        PasswordError,
    }

    public class ApplicationUser : IdentityUser
    {
        public string? LineNotifyToken { get; set; }
        public bool NotifyByEmail { get; set; }
        public bool NotifyByLine { get; set; }
    }


   

    public partial class UserService<TContext> : IDisposable where TContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IServiceScope _scope;
        private readonly TContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private static ApplicationUser _curUser;
        private static string _curRole;

        public static ApplicationUser CurrentUser => _curUser;
        public static string CurrentRole => _curRole;
        public UserManager<ApplicationUser> UserManager => _userManager;

        public UserService(IServiceProvider rootProvider)
        {
            // ✅ 建立 Scope 保留整個生命週期
            _scope = rootProvider.CreateScope();
            var provider = _scope.ServiceProvider;

            _context = provider.GetRequiredService<TContext>();
            _userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
        }



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

            await CreateUserAsync(SD.Admin_Account, SD.Admin_Password, SD.Role_Admin, SD.Default_Email, SD.Default_Line);
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
                    result.Add(new ApplicationUser {Id = user.Id, UserName = user.UserName, Email = user.Email, LineNotifyToken = user.LineNotifyToken });
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
                result.Add(new ApplicationUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    LineNotifyToken = user.LineNotifyToken,
                    NotifyByEmail = user.NotifyByEmail,
                    NotifyByLine = user.NotifyByLine
                });
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
        public async Task CreateUserAsync(string userName, string password, string role, string email, string line)
        {
            bool hasEmail = !string.IsNullOrWhiteSpace(email);
            bool hasLine = !string.IsNullOrWhiteSpace(line);
            // Create the admin user
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,        // ✅ 設定 Message_function
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
                MessageBox.Show("Please confirm whether the password contains special symbols, English letters and numbers");
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

       
        public void Dispose()
        {
            _scope?.Dispose(); // 一併釋放

        }
    }


}
