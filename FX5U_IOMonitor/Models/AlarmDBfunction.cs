using FX5U_IOMonitor.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Models
{
    public class Alarm_sendmail
    {
       
        /// <summary>
        /// 找出警告對應的使用者去對應信箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async Task<List<string>> GetAlarmNotifyEmails(string? alarmNotifyUserField)
        {
            if (string.IsNullOrWhiteSpace(alarmNotifyUserField))
                return new List<string>();

            // 1. 拆解 UserName 字串為陣列
            var userNames = alarmNotifyUserField.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                .Select(u => u.Trim())
                                                .ToList();

            // 2. 查詢對應的 email
            using var userService = new UserService<ApplicationDB>();
            var allUsers = userService.GetAllUser();

            var emails = allUsers
                .Where(u => userNames.Contains(u.UserName))
                .Select(u => u.Email ?? "")
                .ToList();

            return emails;
        }
        public static string Get_AlarmNotifyuser_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.address == address);
                return alarm?.AlarmNotifyuser ?? "";
            }
        }
        public static string Get_Machine_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.address == address);
                return alarm?.SourceMachine ?? "";
            }
        }
        public static string Get_Description_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.address == address);
                return alarm?.Description ?? "";
            }
        }

        public static string Get_Error_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.address == address);
                return alarm?.Error ?? "";
            }
        }

        public static string Get_Possible_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.address == address);
                return alarm?.Possible ?? "";
            }
        }


        public static string Get_Repair_steps_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.address == address);
                return alarm?.Repair_steps ?? "";
            }
        }
    }
}
