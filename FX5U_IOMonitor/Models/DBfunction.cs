using CsvHelper.Configuration;
using FX5U_IOMonitor.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FX5U_IOMonitor.Models
{
    internal class DBfunction
    {
        //------------找是否存在地址------------//

        public static string FindTableWithAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                if (context.alarm.Any(m => m.M_Address == address))
                    return "alarm";
                return ""; // 沒找到
            }
        }

      
        //-----------尋找資料表內容-------------------------//

        public static int GetTableRowCount(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return tableName switch
                {
                    "History" => context.Histories.Count(),
                    "Alarm" => context.alarm.Count(),
                    _ => throw new ArgumentException($"未知資料表名稱：{tableName}")
                };
            }
        }
        public static int GetMachineRowCount(string machineName)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO.Where(a=>a.Machine_name==machineName).Count();
            }
        }


        //----------------啟用時間----------------
        public static string FormatNullableDateTime(DateTime? dt)
        {
            return dt?.ToString("yyyy/M/d HH:mm:ss") ?? "未記錄當前時間";
        }
        public static DateTime? GetMountTimeByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var io = context.Machine_IO
                       .FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                return io?.MountTime;
            }
        }

        public static void SetCurrentTimeAsMountTime(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.Machine_IO
                    .FirstOrDefault(m => m.Machine_name == tableName && m.address == address);

                if (machine != null)
                {
                    machine.MountTime = DateTime.UtcNow;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的啟用時間設為現在：{machine.MountTime:yyyy-MM-dd HH:mm:ss}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }

        //----------------結束時間-----------------------
        public static DateTime? GetUnmountTimeByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var io = context.Machine_IO
                        .FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                return io?.UnmountTime;
            }
        }

        public static void SetCurrentTimeAsUnmountTime(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.Machine_IO
                                .FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                if (machine != null)
                {
                    machine.UnmountTime = DateTime.UtcNow;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的卸除時間設為現在：{machine.UnmountTime:yyyy-MM-dd HH:mm:ss}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }

        //---------------------歷史資料-----------------------
        public static void SetMachineIOToHistory(string tableName, string address, int use_time)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.Machine_IO.FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                if (machine == null)
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");      
                    return;
                }

                History history = new History
                {
                    MachineIOId = machine.Id,
                    SourceDbName = tableName,
                    Address = machine.address,
                    usetime = use_time,
                    StartTime = machine.MountTime,
                    EndTime = machine.UnmountTime
                };

                context.Histories.Add(history);
                context.SaveChanges();

            }
        }
        public static void SetAlarmStartTime(string tableName, string address, string sourceDbName)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
                if (machine == null)
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                    return;
                }

                History history = new History
                {
                    MachineIOId = machine.Id,
                    SourceDbName = sourceDbName,
                    Address = machine.M_Address,
                    StartTime = machine.MountTime,
                };

                context.Histories.Add(history);
                context.SaveChanges();
            }
        }
      
        public static void SetAlarmEndTime(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
                if (machine == null)
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                    return;
                }

                // 找到尚未設定 EndTime 的 alarm 記錄
                var target = context.Histories
                    .FirstOrDefault(h => h.SourceDbName == "alarm" &&
                                         h.Address == address &&
                                         h.EndTime == null);

                if (target != null)
                {
                    target.EndTime = machine.UnmountTime; // 或用 DateTime.Now 也可以
                    context.SaveChanges();

                    Console.WriteLine($"已更新 {address} 的 EndTime 為 {target.EndTime}");
                }
                else
                {
                    Console.WriteLine($"找不到尚未結束的 alarm 記錄（{address}）");
                }
            }
        }
        // 讀取歷史資料
        public static List<History> GetHistoryBySourceAndAddress(string sourceDbName, string address)
        {
            try
            {
                using (var context = new ApplicationDB())
                {
                    var list = context.Histories
                        .Where(h => h.SourceDbName == sourceDbName && h.Address == address)
                        .OrderBy(h => h.StartTime)
                        .ToList();

                    Console.WriteLine($"在資料表 {sourceDbName} 中，找到 {list.Count} 筆 address 為 {address} 的歷史資料");
                    return list;
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"❗ 錯誤：{ex.Message}");
                MessageBox.Show("資料表 Histories 不存在，請先建立或執行初始化！");
                return new List<History>();
            }
        }

        // 共用方法：根據 tableName 傳回對應資料
        private static dynamic GetMachineByAddress(ApplicationDB context, string tableName, string address)
        {
            switch (tableName)
            {
               
                case "alarm":
                    return context.alarm.FirstOrDefault(m => m.M_Address == address);
                default:
                    throw new ArgumentException($"未知的資料表名稱：{tableName}");
            }
        }

        public static List<string> GetMachineClassTags(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                var list = context.Machine_IO
                .Where(d => !string.IsNullOrEmpty(d.ClassTag) &&
                            d.Machine_name == tableName)
                .Select(d => d.ClassTag.Trim())
                .Distinct()
                .OrderBy(tag => tag)
                .ToList();

                return list;
            }
        }
        //查詢表內的class有多少個class類別
        public static List<string> GetClassTag_address(string tableName, string keyword)
        {
            using var context = new ApplicationDB();
            keyword = keyword?.Trim().ToLower();

            List<string> list = context.Machine_IO
                .Where(d => !string.IsNullOrEmpty(d.ClassTag) &&
                                d.Machine_name == tableName &&
                                d.ClassTag.ToLower().Contains(keyword) &&
                        d.ClassTag.Length == keyword.Length)
                    .Select(d => d.address)
                    .ToList();
           
            return list.ToList();
        }

        public static List<string> GetAllClassTags(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO
                                    .Where(d => !string.IsNullOrEmpty(d.ClassTag) && d.Machine_name == tableName)
                                    .Select(d => d.ClassTag)
                                    .ToList() // 先拉進記憶體
                                    .Distinct(StringComparer.OrdinalIgnoreCase)
                                    .OrderBy(tag => tag)
                                    .ToList();
               
            }
        }
        //------尋找綠燈數量--------------
        public static int Get_Yellow_number(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO
                        .Where(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow && io.Machine_name ==tableName)
                        .Count();
            }
        }

        public static int Get_Red_number(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO
                        .Where(io => io.RUL < io.Setting_red && io.Machine_name == tableName)
                        .Count();
            }
        }
        public static int Get_Green_number(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO
                        .Where(io => io.RUL > io.Setting_yellow && io.Machine_name == tableName)
                        .Count();
            }
        }

        public static int Get_Green_search(string tableName, List<string> stringaddress)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO
                                .Where(io => io.RUL > io.Setting_yellow && stringaddress.Contains(io.address) && io.Machine_name == tableName)
                                .Count();
            }
        }
        public static int Get_Yellow_search(string tableName, List<string> stringaddress)
        {

            using (var context = new ApplicationDB())
            {
                return context.Machine_IO
                                .Where(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow && stringaddress.Contains(io.address) && io.Machine_name == tableName)
                                .Count();
            }
        }
        public static int Get_Red_search(string tableName, List<string> stringaddress)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO
                                .Where(io => io.RUL < io.Setting_red && stringaddress.Contains(io.address) && io.Machine_name == tableName)
                               .Count();
                    
            }
        }

        public static int Get_Green_classnumber(string machineName, string classTag, List<string> classaddress)
        {
            using (var context = new ApplicationDB())
            {
                string normalizedTag = classTag?.Trim().ToLower() ?? "";

                int number = context.Machine_IO
                                .Where(io =>
                                    !string.IsNullOrEmpty(io.ClassTag) &&
                                    io.Machine_name== machineName &&
                                    io.ClassTag.ToLower() == normalizedTag &&
                                    classaddress.Contains(io.address)
                                )
                                .AsEnumerable()
                                .Count(io => io.RUL > io.Setting_yellow);
                return number;
            }
        }
        public static int Get_Red_classnumber(string tableName, string classTag, List<string> classaddress)
        {
            using (var context = new ApplicationDB())
            {
                string normalizedTag = classTag?.Trim().ToLower() ?? "";

                return context.Machine_IO
                                .Where(io =>
                                    !string.IsNullOrEmpty(io.ClassTag) &&
                                    io.Machine_name == tableName &&
                                    io.ClassTag.ToLower() == normalizedTag &&
                                    classaddress.Contains(io.address))
                                .AsEnumerable()
                                .Count(io => io.RUL < io.Setting_red);

            }
            
        }

        public static int Get_Yellow_classnumber(string tableName, string classTag, List<string> classaddress)
        {
            using var context = new ApplicationDB();
            string normalizedTag = classTag?.Trim().ToLower() ?? "";

            return context.Machine_IO
                                .Where(io =>
                             !string.IsNullOrEmpty(io.ClassTag) &&
                             io.Machine_name == tableName &&
                             io.ClassTag.ToLower() == normalizedTag &&
                             classaddress.Contains(io.address))
                         .AsEnumerable()
                         .Count(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow);

        }
        //-----獲取元件壽命
        public static double Get_RUL_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var value = context.Machine_IO
                  .Where(m => m.Machine_name == tableName && m.address == address)
                  .Select(m => m.RUL)  // 轉成 nullable int
                  .FirstOrDefault();

                return value;
            }
        }
        public static void Set_RUL_ByAddress(string tableName, string address, double number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.Machine_IO
                   .FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                 if (machine != null)
                {
                    machine.RUL = number;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的說明欄位更新為：{number}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }

        //----------獲取使用者設定綠燈---------
        public static int Get_SetG_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var value = context.Machine_IO
                        .Where(m => m.Machine_name == tableName && m.address == address)
                        .Select(m => m.Setting_green)  // 轉成 nullable int
                        .FirstOrDefault();
                if (value == null)
                {
                    Console.WriteLine("找不到指定 address");
                    return -1;
                }

                return value;
            }
        }
        public static void Set_SetG_ByAddress(string tableName, string address, int number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.Machine_IO.FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                if (machine != null)
                {
                    machine.Setting_green = number;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的說明欄位更新為：{number}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }
        //----------獲取使用者設定黃燈---------
        public static int Get_SetY_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var value = context.Machine_IO
                       .Where(m => m.Machine_name == tableName && m.address == address)
                       .Select(m => m.Setting_yellow)  // 轉成 nullable int
                       .FirstOrDefault();
                if (value == null)
                {
                    Console.WriteLine("找不到指定 address");
                    return -1;
                }

                return value;
            }
        }
        public static void Set_SetY_ByAddress(string tableName, string address, int number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.Machine_IO.FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                if (machine != null)
                {
                    machine.Setting_yellow = number;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的說明欄位更新為：{number}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }
        //----------獲取使用者設定紅燈---------
        public static int Get_SetR_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var value = context.Machine_IO
                       .Where(m => m.Machine_name == tableName && m.address == address)
                       .Select(m => m.Setting_red)  // 轉成 nullable int
                       .FirstOrDefault();
                if (value == null)
                {
                    Console.WriteLine("找不到指定 address");
                    return -1;
                }

                return value;
            }
        }
        public static void Set_SetR_ByAddress(string tableName, string address, int number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.Machine_IO.FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                if (machine != null)
                {
                    machine.Setting_red = number;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的說明欄位更新為：{number}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }
        //
        public static string Get_Element_baseType(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                var value = context.Machine_IO
                       .Where(m => m.Machine_name == tableName)
                       .Select(m => m.baseType)  // 轉成 nullable int
                       .FirstOrDefault();
                if (value == null)
                {
                    Console.WriteLine("找不到指定 address");
                    return "";
                }

                return value;
            }
        }
        //----------獲取使用者設定最大壽命數量---------
        public static int Get_MaxLife_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var value = context.Machine_IO
                       .Where(m => m.Machine_name == tableName && m.address == address)
                       .Select(m => m.MaxLife)  // 轉成 nullable int
                       .FirstOrDefault();
                if (value == null)
                {
                    Console.WriteLine("找不到指定 address");
                    return -1;
                }

                return value;
            }
        }
        public static void Set_MaxLife_ByAddress(string tableName, string address, int number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.Machine_IO.FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                if (machine != null)
                {
                    machine.MaxLife = number;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的說明欄位更新為：{number}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }
        public static bool Get_current_single_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                bool? result = context.Machine_IO
                            .Where(m => m.Machine_name == tableName && m.address == address)
                            .Select(m => m.current_single)
                            .FirstOrDefault();

                return result ?? false;  // 或 return result.GetValueOrDefault();
            }
        }
        public static void Set_current_single_ByAddress(string tableName, string address, bool current_single)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.Machine_IO.FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                if (machine != null)
                {
                    machine.current_single = current_single;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }

        //----------獲取使用者當前使用次數---------
        public static int Get_use_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var value = context.Machine_IO
                      .Where(m => m.Machine_name == tableName && m.address == address)
                      .Select(m => m.equipment_use)  // 轉成 nullable int
                      .FirstOrDefault();
                if (value == null)
                {
                    Console.WriteLine("找不到指定 address");
                    return -1;
                }

                return value;

            }
        }
        public static void Set_use_ByAddress(string tableName, string address, int equipment_use)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.Machine_IO.FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                if (machine != null)
                {
                    machine.equipment_use = equipment_use;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的說明欄位更新為：{equipment_use}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }
        //----------獲取設備料號---------
        public static string Get_Decription_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var value = context.Machine_IO
                      .Where(m => m.Machine_name == tableName && m.address == address)
                      .Select(m => m.Description)  // 轉成 nullable int
                      .FirstOrDefault();
                if (value == null)
                {
                    Console.WriteLine("找不到指定 address");
                    return "";
                }

                return value;
               
            }
        }
        public static string Get_Address_ByDecription(string tableName, string decription)
        {
            using (var context = new ApplicationDB())
            {
                var value = context.Machine_IO
                      .Where(m => m.Machine_name == tableName && m.Description == decription)
                      .Select(m => m.address)  // 轉成 nullable int
                      .FirstOrDefault();
                if (value == null)
                {
                    Console.WriteLine("找不到指定 address");
                    return "";
                }

                return value;

            }
        }
        public static void Set_Decription_ByAddress(string tableName, string address, string description)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.Machine_IO.FirstOrDefault(m => m.Machine_name == tableName && m.address == address);
                if (machine != null)
                {
                    machine.Description = description;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的說明欄位更新為：{description}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }

        //----------獲取設備描述---------
        public static string Get_Comment_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var value = context.Machine_IO
                     .Where(m => m.Machine_name == tableName && m.address == address)
                     .Select(m => m.Comment)  // 轉成 nullable int
                     .FirstOrDefault();
                if (value == null)
                {
                    Console.WriteLine("找不到指定 address");
                    return "";
                }

                return value;
            }
        }

        public static string Get_Comment_ByAddress(string tableName, string address, string languageCode = "US")
        {
            using (var context = new ApplicationDB())
            {
                var io = context.Machine_IO
                    .Include(m => m.Translations)
                    .FirstOrDefault(m => m.Machine_name == tableName && m.address == address);

                if (io == null)
                {
                    Console.WriteLine($"❌ 找不到 address {address} 對應的項目");
                    return "";
                }

                // 優先找該語系的翻譯
                string translated = io.Translations
                    .FirstOrDefault(t => t.LanguageCode == languageCode)?
                    .Comment;

                return !string.IsNullOrWhiteSpace(translated)
                    ? translated
                    : io.Comment; // fallback 主欄位 comment
            }
        }
        /// <summary>
        ///  警告維護表用到的
        /// </summary>
        /// <param name="distinct"></param>
        /// <returns></returns>
        //-----------初始化用-----找到alarm的地址--------//
        public static List<string> Get_alarm_Addresses(bool distinct = true)
        {
            using (var db = new ApplicationDB())
            {
                var query = db.alarm.Select(a => a.M_Address);

                return distinct ? query.Distinct().ToList() : query.ToList();
            }
        }

        //-----------找到alarm資料表中的料件(用於後續比對)--------//
        public static List<string> Get_alarm_class(string machine_name)
        {
            using (var context = new ApplicationDB())
            {
                var alarmClasses = context.alarm.Where(a =>a.SourceDbName == machine_name)
                    .Select(a => string.IsNullOrWhiteSpace(a.classTag) ? "other" : a.classTag)
                    .Distinct() // 2. 去除重複
                    .ToList();
                return alarmClasses;
            }
        }
        public static int Get_alarm_Notifyclass(string machine_name)
        {
            using (var context = new ApplicationDB())
            {
                var alarmClass = context.alarm
                    .Where(a => a.SourceDbName == machine_name)
                    .Select(a => a.AlarmNotifyClass)
                    .Distinct()
                    .FirstOrDefault(); // 取第一筆，若沒有資料回傳 0

                return alarmClass;
            }
        }
        public static string Get_alarm_AlarmNotifyuser(string machine_name)
        {
            using (var context = new ApplicationDB())
            {
                var AlarmNotifyuser = context.alarm
                    .Where(a => a.SourceDbName == machine_name)
                    .Select(a => a.AlarmNotifyuser)
                    .Distinct()
                    .FirstOrDefault(); // 取第一筆，若沒有資料回傳 0

                return AlarmNotifyuser;
            }
        }
        public static List<string> Get_alarm_error_by_class(string machine_name, string className)
        {
            using (var context = new ApplicationDB())
            {
                return context.alarm
                    .Where(a => a.SourceDbName == machine_name &&
                                (string.IsNullOrWhiteSpace(a.classTag) ? "other" : a.classTag) == className)
                    .Select(a => a.Error) 
                    .Distinct()
                    .ToList();
            }
        }
        public static string Get_Description_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.M_Address == address);
                return alarm?.Description ?? "";
            }
        }
        public static (string SourceDbName ,string Description) Get_AlarmInfo_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.M_Address == address);
                return (alarm?.SourceDbName ?? "",alarm?.Description ?? "");
            }
        }
        public static List<string>? Get_Addresses_ByCurrentSingle(string description)
        {
            using (var context = new ApplicationDB())
            {
                var matches = context.alarm
                    .Where(a => a.Description == description && a.current_single == true)
                    .Select(a => a.M_Address)
                    .ToList();

                return matches.Count > 0 ? matches : null;
            }
        }
        //-----------找到alarm資料表中的警告(用於顯示於元件項目)--------//

        public static string Get_Error_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.M_Address == address);
                return alarm?.Error ?? "無對應說明";
            }
        }
        public static string Get_Possible_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.M_Address == address);
                return alarm?.Possible ?? "無對應說明";
            }
        }
        public static string Get_classTag_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.M_Address == address);
                return alarm?.classTag ?? "無對應說明";
            }
        }
        public static string Get_RepairStep_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.M_Address == address);
                return alarm?.Repair_steps ?? "無對應說明";
            }
        }

        public static List<string> Get_address_ByBreakdownParts(string database, List<string> breakdownParts_address) // 取得所有故障料件名稱
        {
            using (var context = new ApplicationDB())
            {
                // 1. 取出 alarm 表中符合 address 的 Description
                var alarmDescriptions = context.alarm
                                                .Where(a => breakdownParts_address.Contains(a.M_Address))
                                                .Select(a => a.Description)
                                                .Distinct()
                                                .ToList();
              
                    var matchedDrillAddresses = context.Machine_IO
                                                  .Where(d => alarmDescriptions.Contains(d.Description) &&d.Machine_name == database)
                                                  .Select(d => d.address)
                                                  .Distinct()
                                                  .ToList();
                    return matchedDrillAddresses;
            }
               
        }

        //--------找到警告中的故障料件-----------//
        public static List<string> Get_breakdown_part(string datatable, bool distinct = true)
        {
            using (var db = new ApplicationDB())
            {

                var query = db.alarm
                             .Where(a => a.current_single == true && !string.IsNullOrEmpty(a.M_Address) && a.SourceDbName == datatable)
                             .Select(a => a.M_Address)
                             .Distinct().ToList();
                return query;

            }
        }
        public static string Get_Error_ByDescription(string description)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.Description == description);
                return alarm?.Error ?? "";
            }
        }
       
        public static string Get_Description_ByAddress(string address, string description)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.M_Address == address);
                return alarm?.Description ?? "";
            }
        }
        public static void Set_Error_ByAddress(string address, string Error)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.M_Address == address);

                if (alarm != null)
                {
                    alarm.Repair_steps = Error;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的說明欄位更新為：{Error}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }
        public static void SetAlarmNotifyType(int AlarmNotify)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.AlarmNotifyClass == AlarmNotify);
                alarm.AlarmNotifyClass = AlarmNotify;
                context.SaveChanges();
            }
        }
        public static void Set_RepairStep_ByAddress(string address, string steps)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.M_Address == address);

                if (alarm != null)
                {
                    alarm.Repair_steps = steps;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的說明欄位更新為：{steps}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }
        public static void Set_Possible_ByAddress(string address, string Possible)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.M_Address == address);

                if (alarm != null)
                {
                    alarm.Possible = Possible;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的說明欄位更新為：{Possible}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }
        

        public static List<now_single> Get_Machine_current_single_all(string tablename)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO.Where(a => a.Machine_name == tablename && a.current_single.HasValue).ToList()
                    .Select(a => new now_single
                    {
                        address = a.address,
                        current_single = (bool)a.current_single
                    })
                    .ToList();
            }
        }
       

       
        public static List<now_single> Get_alarm_current_single_all()
        {
            using var context = new ApplicationDB();
            return context.alarm
                .Select(a => new now_single
                {
                    address = a.M_Address,
                    current_single = a.current_single
                })
                .ToList();
        }

        public static void Set_alarm_current_single_ByAddress(string address, bool current_single)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.alarm.FirstOrDefault(m =>m.M_Address == address);
                if (machine != null)
                {
                    machine.current_single = current_single;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
            }
        }
        public static void Initiali_current_single()
        {

            using ( var context = new ApplicationDB())
            {
                var io = context.Machine_IO.ToList();

                foreach (var IO in io)
                {
                    IO.current_single = null;
                }

                var Alarm_io = context.alarm.ToList();

                foreach (var alarm_io in Alarm_io)
                {
                    alarm_io.current_single = false;
                }
                context.SaveChanges(); // ✅ 儲存變更
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static List<string> Search_IOFromDB(string tableName, string searchText)
        {
            using var context = new ApplicationDB();
            searchText = searchText?.Trim().ToLower() ?? "";

            return context.Machine_IO
                .Where(d => d.Machine_name== tableName &&
                    (!string.IsNullOrEmpty(d.address) && d.address.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(d.ClassTag) && d.ClassTag.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(d.Comment) && d.Comment.ToLower().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(d.Description) && d.Description.ToLower().Contains(searchText)))
                .Select(d => d.address)
                .ToList();
           
        }

        public static List<string> Get_Green_addressList(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO
                                .Where(io => io.RUL > io.Setting_yellow && io.Machine_name ==tableName)
                                .Select(io => io.address)
                                .ToList();
            }
        }

        public static List<string> Get_Yellow_addressList(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO
                                    .Where(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow && io.Machine_name == tableName)
                                    .Select(io => io.address)
                                    .ToList();

            }
        }

        public static List<string> Get_Red_addressList(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO
                                    .Where(io => io.RUL < io.Setting_red && io.Machine_name == tableName)
                                    .Select(io => io.address)
                                    .ToList();

            }
        }


        public static void Set_Machine_string(string name, string number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name);

                if (machine != null)
                {
                    machine.now_TextValue = number;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {name} 的說明欄位更新為：{number}");
                }
                else
                {
                    Console.WriteLine($"找不到 name 為 {number} 的元件");
                }
            }
        }

        public static string Get_Machine_now_string(string name)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name);
                return machine?.now_TextValue ?? "0";
            }
        }

        public static string Get_Machine_now_string(string machine ,string name)
        {
            using (var context = new ApplicationDB())
            {
                var machine_single = context.MachineParameters.FirstOrDefault(a => a.Name == name && a.Machine_Name == machine);
                return machine_single?.now_TextValue ?? "0";
            }
        }


        public static List<string> Get_total_part(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return context.Machine_IO.Where(io => io.Machine_name == tableName).Select(io => io.address).ToList();
            }
        }


        //取得重複的地址
        public static List<MachineIO> GetDuplicateDrillRowsByAddress(string talbename)
        {
            using var context = new ApplicationDB();

            var duplicateAddresses = context.Machine_IO
                .Where(a =>a.Machine_name== talbename)
                .GroupBy(d => d.address)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            // 把這些重複地址的資料都撈出來
            return context.Machine_IO
                .Where(d => duplicateAddresses.Contains(d.address))
                .ToList();
        }

        // 清空指定表格的特定欄位
        public static void ClearColumn<T>(Expression<Func<T, bool>> predicate, string columnName) where T : class
        {
            using (var context = new ApplicationDB())
            {
                var dbSet = context.Set<T>();
                var list = dbSet.Where(predicate).ToList();

                foreach (var item in list)
                {
                    var property = typeof(T).GetProperty(columnName);
                    if (property != null && property.CanWrite)
                    {
                        // 針對 string 欄位設為 null（也可改為 ""）
                        if (property.PropertyType == typeof(string))
                            property.SetValue(item, null);
                        // 可加入更多型別處理邏輯
                    }
                }

                context.SaveChanges();
                Console.WriteLine($"{typeof(T).Name}.{columnName} 欄位已清除 ({list.Count} 筆資料)");
            }
        }
        /// <summary>
        ///  Machineparameter 相關database使用
        /// </summary>
        /// <param name="machine_name"></param>
        /// <returns></returns>
        public static List<string> Get_Machine_montion(string machine_name)
        {
            using (var context = new ApplicationDB())
            {
                // 取得所有鋸床或鑽床的變數
                List<string> Machineprameter = context.MachineParameters.Where(io => io.Machine_Name == machine_name)
                                                    .Select(io => io.Name)
                                                    .ToList();
                return Machineprameter;

            }
        }

        public static List<string> Get_Machine_read_view(int read_view, string machine_name)
        {
            using (var context = new ApplicationDB())
            {
                var result = context.MachineParameters
                    .Where(io => io.Read_view == read_view && io.Machine_Name == machine_name)
                    .Select(io => io.Name)
                    .ToList();

                return result;
            }
        }
        public static List<string> Get_Machine_write_view(string machine_name, int write_type)
        {
            using (var context = new ApplicationDB())
            {
                var result = context.MachineParameters
                    .Where(io => io. Calculate== true && io.Machine_Name == machine_name && io.Calculate_type == write_type)
                    .Select(io => io.Name)
                    .ToList();

                return result;
            }
        }
        public static int Get_Machine_NowValue(string machine_name, string parameterName)
        {
            using (var context = new ApplicationDB())
            {
                var result = context.MachineParameters
                    .Where(io => io.Machine_Name == machine_name && io.Name == parameterName)
                    .Select(io => io.now_NumericValue)
                    .FirstOrDefault();

                return result.HasValue ? result.Value : 0;
            }
        }

        public static int[] Get_Machine_Calculate_type(string machine_name)
        {
            using (var context = new ApplicationDB())
            {
                var result = context.MachineParameters
                    .Where(io => io.Machine_Name == machine_name && io.Calculate == true)
                    .Select(io => io.Calculate_type)
                    .ToArray();

                return result;
            }
        }
        public static int[] Get_Machine_Readview_type(string machine_name)
        {
            using (var context = new ApplicationDB())
            {
                var result = context.MachineParameters
                    .Where(io => io.Machine_Name == machine_name )
                    .Select(io => io.Read_view)
                    .ToArray();

                return result;
            }
        }
        public static List<(string, string, ushort)> Get_Read_word_machineparameter_address(string machine_name, List<string> Machineprameter_name)
        {
            using (var context = new ApplicationDB())
            {
                var result = context.MachineParameters
                            .Where(m => Machineprameter_name.Contains(m.Name) && m.Read_type == "word" && m.Machine_Name == machine_name)
                            .Select(m => new { m.Name, m.Read_address, m.Read_address_index })
                            .ToList()
                            .Select(x => (x.Name, x.Read_address, (ushort)x.Read_address_index))
                            .ToList();

                return result;
            }
        }
        public static List<(string, string, int?)> Get_Write_word_machineparameter_address(string machine_name, List<string> Machineprameter_name)
        {
            using (var context = new ApplicationDB())
            {
                var result = context.MachineParameters
                            .Where(m => Machineprameter_name.Contains(m.Name) && m.Calculate == true && m.Machine_Name == machine_name)
                            .Select(m => new { m.Name, m.Write_address, m.Write_address_index })
                            .ToList()
                            .Select(x => (x.Name, x.Write_address, x.Write_address_index))
                            .ToList();

                return result;
            }
        }
        public static List<(string,int)> Get_None_machineparameter_Name(string machine_name)
        {
            using (var context = new ApplicationDB())
            {
                var result = context.MachineParameters
                            .Where(m =>  m.Read_type == "None" && m.Machine_Name == machine_name)
                            .Select(m => new { m.Name, m.Calculate_type })
                            .ToList()
                            .Select(x => (x.Name,x.Calculate_type))
                            .ToList();

                return result;
            }
        }
        public static List<string> Get_Read_bit_machineparameter(List<string> Machineprameter)
        {
            using (var context = new ApplicationDB())
            {
                var result = context.MachineParameters
                            .Where(m => Machineprameter.Contains(m.Name) && m.Read_type == "bit")
                            .Select(m => new { m.Read_address, m.Read_address_index })
                            .ToList()
                            .Select(x => (x.Read_address))
                            .ToList();

                return result;
            }
        }
        public static void Inital_MachineParameters_number(string machine_name, string name)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name && a.Machine_Name == machine_name);
                if (machine != null)
                {
                    machine.History_NumericValue = (machine.now_NumericValue + machine.History_NumericValue);
                    machine.now_NumericValue = 0;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"找不到元件");
                }
            }
        }
        public static void Set_Machine_now_number(string machine_name,string name, int number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name && a.Machine_Name==machine_name);

                if (machine != null)
                {
                    machine.now_NumericValue = number;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {name} 的說明欄位更新為：{number}");
                }
                else
                {
                    Console.WriteLine($"找不到的元件");
                }
            }
        }


        public static void Set_Machine_now_string(string name, string text)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name);

                if (machine != null)
                {
                    machine.now_TextValue = text;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"找不到 name 為 {text} 的元件");
                }
            }
        }
        public static void Set_Machine_now_string(string machine_name, string name, string text)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name &&a.Machine_Name== machine_name);

                if (machine != null)
                {
                    machine.now_TextValue = text;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"找不到 name 為 {text} 的元件");
                }
            }
        }
       
        public static void Set_Machine_History_NumericValue(string machine_name, string name, int number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name && a.Machine_Name == machine_name);

                if (machine != null)
                {
                    machine.History_NumericValue = number;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {name} 的說明欄位更新為：{number}");
                }
                else
                {
                    Console.WriteLine($"找不到 name 為 {number} 的元件");
                }
            }
        }
       
        public static int Get_Machine_number(string name)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name);
                return machine?.now_NumericValue ?? 0;
            }
        }
        public static string Get_Machine_text(string name)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name);
                return machine?.now_TextValue ?? "無當前資訊";
            }
        }
        public static double Get_Unit_transfer(string name)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name);
                return machine?.Unit_transfer ?? 0;
            }
        }

        public static string Get_Blade_brand_name(int brand_id)
        {
            using (var context = new ApplicationDB())
            {
                var name = context.Blade_brand.FirstOrDefault(a => a.blade_brand_id == (int)brand_id);
                return name?.blade_brand_name ?? "無比對資料";
            }
        }

        public static string Get_Blade_brand_material(int material_id)
        {
            using (var context = new ApplicationDB())
            {
                var name = context.Blade_brand.FirstOrDefault(a => a.blade_material_id == (int)material_id);
                return name?.blade_material_name ?? "無比對資料";
            }
        }
        public static string Get_Blade_brand_type(int brand_id, int material_id, int type_id)
        {
            using (var context = new ApplicationDB())
            {
                var name = context.Blade_brand.FirstOrDefault(a => a.blade_brand_id == brand_id && a.blade_material_id == material_id && a.blade_Type_id == type_id);
                return name?.blade_Type_name ?? "無比對資料";
            }
        }

        public static string Get_Blade_TPI_type(int TPI_id)
        {
            using (var context = new ApplicationDB())
            {
                var name = context.Blade_brand_TPI.FirstOrDefault(a => a.blade_TPI_id == (int)TPI_id);
                return name?.blade_TPI_name ?? "無比對資料";
            }
        }
        public static List<string> Get_Machine_Calculate_type(int calculate_type, string machine_name)
        {
            using (var context = new ApplicationDB())
            {
                var result = context.MachineParameters
                    .Where(io => io.Calculate_type == calculate_type && io.Calculate == true)
                    .Select(io => io.Name)
                    .ToList();

                return result;
            }
        }
      
        public static List<(string, string)> Get_Calculate_Readbit_address(List<string> Machineprameter_name)
        {
            using (var context = new ApplicationDB())
            {
                var result = context.MachineParameters
                            .Where(m => Machineprameter_name.Contains(m.Name) && m.Read_type == "bit")
                            .Select(m => new { m.Name, m.Read_address })
                            .ToList()
                            .Select(x => (x.Name, x.Read_address))
                            .ToList();

                return result;
            }
        }
        public static int Get_Machine_History_NumericValue(string Machineprameter_name)
        {
            using (var context = new ApplicationDB())
            {
                var name = context.MachineParameters.FirstOrDefault(a => a.Name == Machineprameter_name).History_NumericValue;
                return name ?? 0;

            }

        }
        public static int Get_Machine_History_NumericValue(string machine_name, string Machineprameter_name)
        {
            using (var context = new ApplicationDB())
            {



                var result = context.MachineParameters
                    .Where(io => io.Machine_Name == machine_name && io.Name == Machineprameter_name)
                    .Select(io => io.History_NumericValue)
                    .FirstOrDefault();

                return result.HasValue ? result.Value : 0;

            }
        }
        public static int Get_History_NumericValue(string Machine_Name,string Machineprameter_name)
        {
            using (var context = new ApplicationDB())
            {
                var name = context.MachineParameters.FirstOrDefault(a => a.Name == Machineprameter_name && a.Machine_Name == Machine_Name).History_NumericValue;
                return name ?? 0;

            }
        }
        //查詢
        public static List<string> GetClassTagLanguageKeys()
        {
            using var context = new ApplicationDB();

            var list = context.Language
                .Where(l => l.Key.Contains("ClassTag_"))
                .Select(l => l.Key)
                .ToList();

            return list;
        }

        public static List<Machine_number> GetMachineIndexes()
        {
            using var context = new ApplicationDB();
            return context.index
                .Select(x => new Machine_number
                {
                    Name = x.Name,
                })
                .ToList();
        }

        public static void AddMachineKeyIfNotExist(string key, string usValue)
        {
            using (var context = new ApplicationDB())
            {
                // 檢查是否已存在
                bool exists = context.Language.Any(l => l.Key == key);
                if (exists) return;

                var newLang = new Language
                {
                    Key = key,
                    US = usValue,
                };

                context.Language.Add(newLang);
                context.SaveChanges();

            }

        }


        public static void DeleteMachineByName(string machineName)
        {
            using var context = new ApplicationDB();

            // 刪除 Machine_IO 中與此機台相關資料
            var ioItems = context.Machine_IO.Where(m => m.Machine_name == machineName).ToList();
            context.Machine_IO.RemoveRange(ioItems);

            var ioItem = context.index.FirstOrDefault(m => m.Name == machineName);
            if (ioItem != null)
            {
                context.index.Remove(ioItem);

            }

            // 刪除語系表（可選）
            string langKey = $"Mainform_{machineName}";
            var langEntry = context.Language.FirstOrDefault(l => l.Key == langKey);
            if (langEntry != null)
            {
                context.Language.Remove(langEntry);
            }

            context.SaveChanges();
        }



    }

}
