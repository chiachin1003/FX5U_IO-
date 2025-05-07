using CsvHelper.Configuration;
using FX5U_IOMonitor.Data;
using FX5U_IOMonitor.Migrations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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
                if (context.Drill_IO.Any(m => m.address == address))
                    return "Drill";
                if (context.Sawing_IO.Any(m => m.address == address))
                    return "Sawing";
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
                    "Drill" => context.Drill_IO.Count(),
                    "Sawing" => context.Sawing_IO.Count(),
                    "History" => context.Histories.Count(),
                    "Alarm" => context.alarm.Count(),
                    _ => throw new ArgumentException($"未知資料表名稱：{tableName}")
                };
            }
        }

        public static int Get_currentsingle_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.current_single ?? "";
            }
        }


        //----------------啟用時間----------------
        public static DateTime? GetMountTimeByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.MountTime;
            }
        }

        public static void SetCurrentTimeAsMountTime(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
                if (machine != null)
                {
                    machine.MountTime = DateTime.Now;
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
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.UnmountTime;
            }
        }

        public static void SetCurrentTimeAsUnmountTime(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
                if (machine != null)
                {
                    machine.UnmountTime = DateTime.Now;
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
        public static void SetMachineIOToHistory(string tableName, string address, int use_time )
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
                case "Drill":
                    return context.Drill_IO.FirstOrDefault(m => m.address == address);
                case "Sawing":
                    return context.Sawing_IO.FirstOrDefault(m => m.address == address);
                case "alarm":
                    return context.alarm.FirstOrDefault(m => m.M_Address == address);
                default:
                    throw new ArgumentException($"未知的資料表名稱：{tableName}");
            }
        }


        // 讀取指定資料表內的特定欄

        private static dynamic GetMachineClassTag(string tableName, string classtag)
        {
            using var context = new ApplicationDB();

            switch (tableName)
            {
                case "Drill":
                    return context.Drill_IO.FirstOrDefault(m => m.ClassTag == classtag);
                case "Sawing":
                    return context.Sawing_IO.FirstOrDefault(m => m.ClassTag == classtag);
                default:
                    throw new ArgumentException($"未知的資料表名稱：{tableName}");
            }
        }

        //查詢表內的class有多少個class類別
        public static List<string> GetAllClassTags(string tableName, string keyword)
        {
            using var context = new ApplicationDB();
            keyword = keyword?.Trim().ToLower();

            IQueryable<string> query = tableName switch
            {
                "Drill" => context.Drill_IO
                    .Where(d => !string.IsNullOrEmpty(d.ClassTag) &&
                                d.ClassTag.ToLower().Contains(keyword) &&
                        d.ClassTag.Length == keyword.Length)
                    .Select(d => d.address),

                "Sawing" => context.Sawing_IO
                    .Where(s => !string.IsNullOrEmpty(s.ClassTag) &&
                                s.ClassTag.ToLower().Contains(keyword) &&
                        s.ClassTag.Length == keyword.Length)
                    .Select(s => s.address),

                _ => throw new ArgumentException($"未知的資料表名稱：{tableName}")
            };

            return query.ToList();
        }

        public static List<string> GetAllClassTags(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return tableName switch
                {
                    "Drill" => context.Drill_IO
                                    .Where(d => !string.IsNullOrEmpty(d.ClassTag))
                                    .Select(d => d.ClassTag)
                                    .ToList() // 先拉進記憶體
                                    .Distinct(StringComparer.OrdinalIgnoreCase)
                                    .OrderBy(tag => tag)
                                    .ToList(),
                    "Sawing" => context.Sawing_IO
                                    .Where(s => !string.IsNullOrEmpty(s.ClassTag))
                                    .Select(s => s.ClassTag)
                                    .ToList() // 先拉進記憶體
                                    .Distinct(StringComparer.OrdinalIgnoreCase)
                                    .OrderBy(tag => tag)
                                    .ToList(),
                    _ => throw new ArgumentException($"未知的資料表名稱：{tableName}")
                };
            }
        }
        //------尋找綠燈數量--------------
        public static int Get_Yellow_number(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return tableName switch
                {
                    "Drill" => context.Drill_IO
                                .Where(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow)
                                .Count(),
                    "Sawing" => context.Sawing_IO
                                .Where(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow)
                                .Count(),
                    _ => throw new ArgumentException($"未知的資料表名稱：{tableName}")
                };
            }
        }

        public static int Get_Red_number(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return tableName switch
                {
                    "Drill" => context.Drill_IO
                                .Where(io => io.RUL < io.Setting_red)
                                .Count(),
                    "Sawing" => context.Sawing_IO
                                .Where(io => io.RUL < io.Setting_red)
                                .Count(),
                    _ => throw new ArgumentException($"未知的資料表名稱：{tableName}")
                };
            }
        }
        public static int Get_Green_number(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return tableName switch
                {
                    "Drill" => context.Drill_IO
                                .Where(io => io.RUL > io.Setting_yellow)
                                .Count(),
                    "Sawing" => context.Sawing_IO
                                .Where(io => io.RUL > io.Setting_yellow)
                                .Count(),
                    _ => throw new ArgumentException($"未知的資料表名稱：{tableName}")
                };
            }
        }

        public static int Get_Green_search(string tableName, List<string> stringaddress)
        {
            using (var context = new ApplicationDB())
            {
                return tableName switch
                {
                    "Drill" => context.Drill_IO
                                .Where(io => io.RUL > io.Setting_yellow && stringaddress.Contains(io.address))
                                .Count(),
                    "Sawing" => context.Sawing_IO
                                .Where(io => io.RUL > io.Setting_yellow && stringaddress.Contains(io.address))
                                .Count(),
                    _ => throw new ArgumentException($"未知的資料表名稱：{tableName}")
                };
            }
        }
        public static int Get_Yellow_search(string tableName, List<string> stringaddress)
        {
            using (var context = new ApplicationDB())
            {
                return tableName switch
                {
                    "Drill" => context.Drill_IO
                                .Where(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow && stringaddress.Contains(io.address))
                                .Count(),
                    "Sawing" => context.Sawing_IO
                                .Where(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow && stringaddress.Contains(io.address))
                                .Count(),
                    _ => throw new ArgumentException($"未知的資料表名稱：{tableName}")
                };
            }
        }
        public static int Get_Red_search(string tableName, List<string> stringaddress)
        {
            using (var context = new ApplicationDB())
            {
                return tableName switch
                {
                    "Drill" => context.Drill_IO
                                .Where(io => io.RUL < io.Setting_red && stringaddress.Contains(io.address))
                                .Count(),
                    "Sawing" => context.Sawing_IO
                                .Where(io => io.RUL < io.Setting_red && stringaddress.Contains(io.address))
                                .Count(),
                    _ => throw new ArgumentException($"未知的資料表名稱：{tableName}")
                };
            }
        }

        public static int Get_Green_classnumber(string tableName, string classTag, List<string> classaddress)
        {
            using var context = new ApplicationDB();

            string normalizedTag = classTag?.Trim().ToLower() ?? "";

            return tableName switch
            {
                "Drill" => context.Drill_IO
                            .Where(io =>
                                !string.IsNullOrEmpty(io.ClassTag) &&
                                io.ClassTag.ToLower() == normalizedTag &&
                                classaddress.Contains(io.address)
                            )
                            .AsEnumerable()
                            .Count(io => io.RUL > io.Setting_yellow),

                "Sawing" => context.Sawing_IO
                            .Where(io =>
                                !string.IsNullOrEmpty(io.ClassTag) &&
                                io.ClassTag.ToLower() == normalizedTag &&
                                classaddress.Contains(io.address)
                            )
                            .AsEnumerable()
                            .Count(io => io.RUL > io.Setting_yellow),

                _ => throw new ArgumentException($"未知的資料表名稱：{tableName}")
            };
        }
        public static int Get_Red_classnumber(string tableName, string classTag, List<string> classaddress)
        {
            using var context = new ApplicationDB();
            string normalizedTag = classTag?.Trim().ToLower() ?? "";

            return tableName switch
            {
                "Drill" => context.Drill_IO
                            .Where(io =>
                                !string.IsNullOrEmpty(io.ClassTag) &&
                                io.ClassTag.ToLower() == normalizedTag &&
                                classaddress.Contains(io.address))
                            .AsEnumerable()
                            .Count(io => io.RUL < io.Setting_red),

                "Sawing" => context.Sawing_IO
                            .Where(io =>
                                !string.IsNullOrEmpty(io.ClassTag) &&
                                io.ClassTag.ToLower() == normalizedTag &&
                                classaddress.Contains(io.address))
                            .AsEnumerable()
                            .Count(io => io.RUL < io.Setting_red),

                _ => throw new ArgumentException($"未知的資料表名稱：{tableName}")
            };
        }

        public static int Get_Yellow_classnumber(string tableName, string classTag, List<string> classaddress)
        {
            using var context = new ApplicationDB();
            string normalizedTag = classTag?.Trim().ToLower() ?? "";

            return tableName switch
            {
                "Drill" => context.Drill_IO
                            .Where(io =>
                                !string.IsNullOrEmpty(io.ClassTag) &&
                                io.ClassTag.ToLower() == normalizedTag &&
                                classaddress.Contains(io.address))
                            .AsEnumerable()
                            .Count(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow),

                "Sawing" => context.Sawing_IO
                            .Where(io =>
                                !string.IsNullOrEmpty(io.ClassTag) &&
                                io.ClassTag.ToLower() == normalizedTag &&
                                classaddress.Contains(io.address))
                            .AsEnumerable()
                            .Count(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow),

                _ => throw new ArgumentException($"未知的資料表名稱：{tableName}")
            };
        }
        //-----獲取元件壽命
        public static double Get_RUL_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.RUL ?? "";
            }
        }
        public static void Set_RUL_ByAddress(string tableName, string address, double number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
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
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.Setting_green ?? "";
            }
        }
        public static void Set_SetG_ByAddress(string tableName, string address, int number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
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
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.Setting_yellow ?? "";
            }
        }
        public static void Set_SetY_ByAddress(string tableName, string address, int number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
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
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.Setting_red ?? "";
            }
        }
        public static void Set_SetR_ByAddress(string tableName, string address, int number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
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
        //----------獲取使用者設定最大壽命數量---------
        public static int Get_MaxLife_ByAddress(string tableName, string address)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.MaxLife ?? "";
            }
        }
        public static void Set_MaxLife_ByAddress(string tableName, string address, int number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
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
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.current_single ?? "";
            }
        }
        public static void Set_current_single_ByAddress(string tableName, string address, bool current_single)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
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
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.equipment_use ?? "";
            }
        }
        public static void Set_use_ByAddress(string tableName, string address, int equipment_use)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
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
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.Description ?? "";
            }
        }
        public static void Set_Decription_ByAddress(string tableName, string address, string description)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
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
                var machine = GetMachineByAddress(context, tableName, address);
                return machine?.Comment ?? "";
            }
        }
        public static void Set_Comment_ByAddress(string tableName, string address, string Comment)
        {
            using (var context = new ApplicationDB())
            {
                var machine = GetMachineByAddress(context, tableName, address);
                if (machine != null)
                {
                    machine.Comment = Comment;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {address} 的說明欄位更新為：{Comment}");
                }
                else
                {
                    Console.WriteLine($"找不到 address 為 {address} 的元件");
                }
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

        public static string Get_Description_ByAddress(string address)
        {
            using (var context = new ApplicationDB())
            {
                var alarm = context.alarm.FirstOrDefault(a => a.M_Address == address);
                return alarm?.Description ?? "";
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
        //--------找到警告的錯誤料件-----------//

        public static (string? Address, string? TableName) FindIOByAlarmDescription(string alarmDescription)
        {
            using (var context = new ApplicationDB())
            {
                // 先查 Drill_IO
                var drillMatch = context.Drill_IO.FirstOrDefault(io => io.Description == alarmDescription);
                if (drillMatch != null)
                    return (drillMatch.address, "Drill");

                // 再查 Sawing
                var sawingMatch = context.Sawing_IO.FirstOrDefault(io => io.Description == alarmDescription);
                if (sawingMatch != null)
                    return (sawingMatch.address, "Sawing");

                return (null, null); // 沒有找到
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
                if (database == "Drill")
                {
                    var matchedDrillAddresses = context.Drill_IO
                                                  .Where(d => alarmDescriptions.Contains(d.Description))
                                                  .Select(d => d.address)
                                                  .Distinct()
                                                  .ToList();
                    return matchedDrillAddresses;
                }
                // 2. 比對 Drill_IO 中相同的 Description，取 address
                else if (database == "Sawing")
                {
                    var matchedDrillAddresses = context.Sawing_IO
                                                  .Where(d => alarmDescriptions.Contains(d.Description))
                                                  .Select(d => d.address)
                                                  .Distinct()
                                                  .ToList();
                    return matchedDrillAddresses;
                }
                else 
                { 
                    return new(); 
                }
            }
        }

        //--------找到警告中的故障料件-----------//
        public static List<string> Get_breakdown_part(string datatable, bool distinct = true)
        {
            using (var db = new ApplicationDB())
            {
                if (datatable == "Drill")
                {
                    var query = db.alarm
                             .Where(a => a.current_single == true && a.SourceDbName == "Drill_IO")   
                             .Select(a => a.M_Address);

                    return distinct ? query.Distinct().ToList() : query.ToList();
                }
                else if (datatable == "Sawing")
                {
                    var query = db.alarm
                             .Where(a => a.current_single == true && a.SourceDbName == "Sawing_IO")  
                             .Select(a => a.M_Address);

                    return distinct ? query.Distinct().ToList() : query.ToList();
                }
            }
            return new();
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

        public static List<now_single> Get_Sawing_current_single_all()
        {
            using var context = new ApplicationDB();
            return context.Sawing_IO
                .Select(a => new now_single
                {
                    address = a.address,
                    current_single = (bool)a.current_single
                })
                .ToList();
        }
        public static List<now_single> Get_Drill_current_single_all()
        {
            using var context = new ApplicationDB();
            return context.Drill_IO
            .Where(a => a.current_single != null)
            .Select(a => new now_single
            {
                address = a.address,
                current_single = (bool)a.current_single.Value
            })
            .ToList();
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


        public static void Initiali_current_single()
        {

            using (var context = new ApplicationDB())
            {

                // 取得整個 Drill_IO 清單
                var Drill_io = context.Drill_IO.ToList();

                foreach (var drill_io in Drill_io)
                {
                    drill_io.current_single = null; // ✅ 對模型屬性設值
                }
                var Sawing_io = context.Sawing_IO.ToList();

                foreach (var sawing_io in Sawing_io)
                {
                    sawing_io.current_single = null; // ✅ 對模型屬性設值
                }
                var Alarm_io = context.alarm.ToList();

                foreach (var alarm_io in Alarm_io)
                {
                    alarm_io.current_single = false; // ✅ 對模型屬性設值
                }
                context.SaveChanges(); // ✅ 儲存變更
            }


        }

        public static object Get_Class_IO(string tableName, string classtag)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("資料表名稱不得為空");

            using (var context = new ApplicationDB())
            {
                try
                {
                    return tableName switch
                    {
                        "Drill" => string.IsNullOrWhiteSpace(classtag)
                            ? context.Drill_IO.ToList()
                            : context.Drill_IO
                                .Where(d => d.ClassTag != null && d.ClassTag.ToLower().Contains(classtag.ToLower()))
                                .ToList(),

                        "Sawing" => string.IsNullOrWhiteSpace(classtag)
                            ? context.Sawing_IO.ToList()
                            : context.Sawing_IO
                                .Where(d => d.ClassTag != null && d.ClassTag.ToLower().Contains(classtag.ToLower()))
                                .ToList(),

                        _ => throw new ArgumentException($"❌ 未知的資料表名稱：{tableName}")
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ 查詢時發生錯誤：{ex.Message}");
                    return new List<object>(); // 或 return null; 看你系統如何設計
                }
            }

        }




        public static List<string> Search_IOFromDB(string table, string searchText)
        {
            using var context = new ApplicationDB();
            searchText = searchText?.Trim().ToLower() ?? "";

            if (table == "Drill")
            {
                return context.Drill_IO
                    .Where(d =>
                        (!string.IsNullOrEmpty(d.address) && d.address.ToLower().Contains(searchText)) ||
                        (!string.IsNullOrEmpty(d.ClassTag) && d.ClassTag.ToLower().Contains(searchText)) ||
                        (!string.IsNullOrEmpty(d.Comment) && d.Comment.ToLower().Contains(searchText)) ||
                        (!string.IsNullOrEmpty(d.Description) && d.Description.ToLower().Contains(searchText)))
                    .Select(d => d.address)
                    .ToList();
            }
            else if (table == "Sawing")
            {
                return context.Sawing_IO
                    .Where(s =>
                        (!string.IsNullOrEmpty(s.address) && s.address.ToLower().Contains(searchText)) ||
                        (!string.IsNullOrEmpty(s.ClassTag) && s.ClassTag.ToLower().Contains(searchText)) ||
                        (!string.IsNullOrEmpty(s.Comment) && s.Comment.ToLower().Contains(searchText)) ||
                        (!string.IsNullOrEmpty(s.Description) && s.Description.ToLower().Contains(searchText)))
                    .Select(d => d.address)
                    .ToList();
            }

            return new List<string>();
        }

        public static List<string> Get_Green_addressList(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return tableName switch
                {
                    "Drill" => context.Drill_IO
                                    .Where(io => io.RUL > io.Setting_yellow)
                                    .Select(io => io.address)
                                    .ToList(),

                    "Sawing" => context.Sawing_IO
                                    .Where(io => io.RUL > io.Setting_yellow)
                                    .Select(io => io.address)
                                    .ToList(),

                    _ => throw new ArgumentException($"❌ 未知的資料表名稱：{tableName}")
                };
            }
        }

        public static List<string> Get_Yellow_addressList(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return tableName switch
                {
                    "Drill" => context.Drill_IO
                                    .Where(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow)
                                    .Select(io => io.address)
                                    .ToList(),

                    "Sawing" => context.Sawing_IO
                                    .Where(io => io.RUL >= io.Setting_red && io.RUL <= io.Setting_yellow)
                                    .Select(io => io.address)
                                    .ToList(),

                    _ => throw new ArgumentException($"❌ 未知的資料表名稱：{tableName}")
                };
            }
        }

        public static List<string> Get_Red_addressList(string tableName)
        {
            using (var context = new ApplicationDB())
            {
                return tableName switch
                {
                    "Drill" => context.Drill_IO
                                    .Where(io => io.RUL < io.Setting_red)
                                    .Select(io => io.address)
                                    .ToList(),

                    "Sawing" => context.Sawing_IO
                                    .Where(io => io.RUL < io.Setting_red)
                                    .Select(io => io.address)
                                    .ToList(),

                    _ => throw new ArgumentException($"❌ 未知的資料表名稱：{tableName}")
                };
            }
        }

        ///
        /// 修改機台狀態的資料表
        ///

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
                return machine?.now_TextValue ?? "無對應說明";
            }
        }
        public static void Set_Machine_now_number(string name, ushort number)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name);

                if (machine != null)
                {
                    machine.now_NumericValue = number;
                    context.SaveChanges();
                    Console.WriteLine($"已將 {name} 的說明欄位更新為：{number}");
                }
                else
                {
                    Console.WriteLine($"找不到 name 為 {number} 的元件");
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

        public static ushort Get_Machine_number(string name)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name);
                return machine?.now_NumericValue ?? 0;
            }
        }


        public static string Get_Machine_read_address(string name)
        {
            using (var context = new ApplicationDB())
            {
                var machine = context.MachineParameters.FirstOrDefault(a => a.Name == name);
                return machine?.read_address ?? "0";
            }
        }
     
    

        public static List<string> Get_total_part(string datatable)
        {
            using (var context = new ApplicationDB())
            {
                if (datatable == "Drill")
                {
                    List<string> query = context.Drill_IO.Select(io => io.address).ToList();


                    return query;
                }
                else if (datatable == "Sawing")
                {
                    var query = context.Sawing_IO.Select(io => io.address).ToList();

                    return query;
                }
            }
            return new();
        }


        //取得重複的地址
        public static List<Drill_MachineIO> GetDuplicateDrillRowsByAddress()
        {
            using var context = new ApplicationDB();

            var duplicateAddresses = context.Drill_IO
                .GroupBy(d => d.address)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            // 把這些重複地址的資料都撈出來
            return context.Drill_IO
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

    }


  
  
}
