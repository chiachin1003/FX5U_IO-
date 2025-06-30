using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Login
{
    internal class User_Validation
    {
        // 請求驗證模型
        public class CreateUserRequest
        {
            [Required(ErrorMessage = "使用者名稱必填")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "使用者名稱長度必須介於 3-50 字元")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "電子郵件必填")]
            [EmailAddress(ErrorMessage = "電子郵件格式不正確")]
            public string Email { get; set; }

            [Required(ErrorMessage = "密碼必填")]
            [StringLength(100, MinimumLength = 8, ErrorMessage = "密碼長度必須至少 8 字元")]
            public string Password { get; set; }

            [Required(ErrorMessage = "角色必填")]
            public string Role { get; set; }
        }

        public class LoginRequest
        {
            [Required(ErrorMessage = "使用者名稱必填")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "密碼必填")]
            public string Password { get; set; }
        }

        // 結果模型
        public class UserResult
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public List<string> Errors { get; set; } = new();

            public static UserResult Success(string message = "操作成功")
                => new() { IsSuccess = true, Message = message };

            public static UserResult Failure(string message, List<string> errors = null)
                => new() { IsSuccess = false, Message = message, Errors = errors ?? new() };
        }

        public class LoginResult : UserResult
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string Role { get; set; }
        }
    }
}
