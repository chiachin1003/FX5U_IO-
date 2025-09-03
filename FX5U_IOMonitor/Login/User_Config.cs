using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Login
{
    // 設定選項類別
    public class DefaultAdminOptions
    {
        public string Admin_Account { get; set; } = "admin";
        public string Default_Email { get; set; } = "jenny963200@hotmail.com";
        public string Admin_Password { get; set; } = "admin123*";
    }

    public class UserRoleOptions
    {
        public string Admin { get; set; } = "Admin";
        public string Operator { get; set; } = "Operator";
        public string User { get; set; } = "User";
    }

    // 常數類別 (簡化版)
    public static class AppConstants
    {
        public const int ROUND_DIGIT = 1;
        public const string Default_Schedule_Tag = "None";
    }
}
