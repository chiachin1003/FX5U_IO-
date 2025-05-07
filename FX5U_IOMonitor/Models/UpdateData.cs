using System.Text;
using System.Data;
using FX5U_IOMonitor.Data;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FX5U_IOMonitor.Models
{
    internal class UpdateData
    {

        /// <summary>
        /// 初始化 IO_data 資料，並匯入 DataStore 的靜態清單
        /// 數據初始化
        /// </summary>
        /// <param name="filePath">Excel 文件路徑</param>
        public static List<IO_DataBase> Initiali_DataFromDB(string tableName)
        {
            List<IO_DataBase> dataList = new List<IO_DataBase>();

            try
            {
                using (var context = new ApplicationDB())
                {
                    if (tableName == "Drill")
                    {
                        var machineData = context.Drill_IO.ToList();

                        foreach (var row in machineData)
                        {
                            dataList.Add(ConvertToIOData(row));
                        }
                    }
                    else if (tableName == "Sawing")
                    {
                        var machineData = context.Sawing_IO.ToList();

                        foreach (var row in machineData)
                        {
                            dataList.Add(ConvertToIOData(row));
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"未知的資料表名稱：{tableName}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"讀取資料庫失敗: {ex.Message}");
            }

            return dataList;
        }
        //轉換公式
        internal static IO_DataBase ConvertToIOData(dynamic row)
        {
            return new IO_DataBase
            {
                address = row.address,
                IO = row.IOType,
                IsMechanical = row.RelayType == RelayType.Machanical,
                equipmentDescription = row.Description ?? "未設定",
                MaxLife = row.MaxLife,
                equipment_use = row.equipment_use,
                Part_InstallationTime = row.MountTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Part_RemovalTime = row.UnmountTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Setting_green = row.Setting_green,
                Setting_yellow = row.Setting_yellow,
                Setting_red = row.Setting_red,
                ClassTag = row.ClassTag ?? "未分類",
                RUL = row.RemainingLifeTime,
                percent = row.percent,
                current_single = row.current_single,
                current_HI_state = row.RemainingLifeTime,
                Comment = row.Comment ?? "無描述"
            };
        }
       

        // 完全斷線後狀態監控
        public static void ResetCurrentSingle(List<IO_DataBase> dataList)
        {
            if (dataList == null || dataList.Count == 0)
            {
                return;
            }

            foreach (var item in dataList)
            {
                item.current_single = null;
            }
        }
       


        /// 將csv檔資料轉譯成DB格式
        public static void SaveMachineIODb(List<IO_DataBase> machineData, string tableName)
        {
            using (var context = new ApplicationDB())
            {
                switch (tableName)
                {
                    case "Drill":


                        foreach (var item in machineData)
                        {
                            var newIO = new Drill_MachineIO
                            {
                                address = item.address,
                                IOType = item.IO,  // 明確轉換
                                RelayType = item.IsMechanical ? RelayType.Machanical : RelayType.Electronic,
                                Description = item.equipmentDescription,
                                Comment = item.Comment,
                                ClassTag = item.ClassTag,
                                MaxLife = item.MaxLife,
                                equipment_use = item.equipment_use,
                                Setting_green = item.Setting_green,
                                Setting_yellow = item.Setting_yellow,
                                Setting_red = item.Setting_red,
                                percent = item.percent,
                                MountTime = DateTime.TryParse(item.Part_InstallationTime, out DateTime mountTime)
                                    ? mountTime : DateTime.Now,
                                UnmountTime = DateTime.TryParse(item.Part_RemovalTime, out DateTime unmountTime)
                                    ? unmountTime : DateTime.Now.AddDays(30)
                            };

                            context.Drill_IO.Add(newIO);
                        }
                        break;
                    case "Sawing":


                        foreach (var item in machineData)
                        { 
                            var newSwing = new Sawing_MachineIO
                            {
                                address = item.address,
                                IOType = item.IO,  // 明確轉換
                                RelayType = item.IsMechanical ? RelayType.Machanical : RelayType.Electronic,
                                Description = item.equipmentDescription,
                                Comment = item.Comment,
                                ClassTag = item.ClassTag,
                                MaxLife = item.MaxLife,
                                equipment_use = item.equipment_use,
                                Setting_green = item.Setting_green,
                                Setting_yellow = item.Setting_yellow,
                                Setting_red = item.Setting_red,
                                percent = item.percent,
                                MountTime = DateTime.TryParse(item.Part_InstallationTime, out DateTime mountTime)
                                    ? mountTime : DateTime.Now,
                                UnmountTime = DateTime.TryParse(item.Part_RemovalTime, out DateTime unmountTime)
                                    ? unmountTime : DateTime.Now.AddDays(30)
                            };
                        context.Sawing_IO.Add(newSwing);
                        }
                        break;
                    default:
                        throw new ArgumentException($"未知的表格名稱: {tableName}");
                }
                context.SaveChanges();

            }
               
            

        }
        public static void InitializeMachine_Database()
        {
            using (var context = new ApplicationDB())
            {

                // 檢查是否已經有資料
                if (!context.MachineParameters.Any())
                {
                    var initialParameters = new List<MachineParameter>
                    {
                        //鋸床監控讀取
                        new MachineParameter { Name = "Sawing_motor_current", read_address = "R20000"},
                        new MachineParameter { Name = "Sawing_cuttingspeed", read_address = "R20002"},
                        new MachineParameter { Name = "Sawing_voltage", read_address = "R25004", calculate= true, write_address = "R20004"},
                        new MachineParameter { Name = "Sawing_current", read_address = "R25006", calculate= true, write_address = "R20006"},
                        new MachineParameter { Name = "Sawing_oil_pressure", read_address = "R20008"},
                        new MachineParameter { Name = "Sawing_power", calculate= true, write_address = "R20010"},
                        new MachineParameter { Name = "Sawing_electricity", calculate= true, write_address = "R20012"},
                        new MachineParameter { Name = "Sawing_total_time", read_address = "R20014", calculate= true, write_address = "R20014"},
                        new MachineParameter { Name = "Sawing_remain_tools", read_address = "R20016"},
                        new MachineParameter { Name = "Sawing_countdown_time", read_address = "R20018"},

                        //鋸帶監控 
                        new MachineParameter{ Name = "Sawband_brand", read_address = "R20100"},
                        new MachineParameter{ Name = "Sawblade_material", read_address = "R20140"},
                        new MachineParameter{ Name = "Sawblade_teeth", read_address = "R20160"},
                        new MachineParameter{ Name = "Sawband_speed", read_address = "R20162"},
                        new MachineParameter{ Name = "Sawband_motors_usetime", read_address = "R25040", calculate= true, write_address = "R20164"},

                        new MachineParameter{ Name = "Sawband_power", read_address = "R20166"},
                        new MachineParameter{ Name = "Sawband_current", read_address = "R20168"},
                        new MachineParameter{ Name = "Sawband_area", read_address = "R25046", calculate= true, write_address = "R20170"}, //會更新
                        new MachineParameter{ Name = "Sawband_life", read_address = "R25042", calculate= true, write_address = "R20172"},
                        new MachineParameter{ Name = "Sawband_tension", read_address = "R25044", calculate= true, write_address = "R20174"},

                        //鑽床監控 
                        new MachineParameter { Name = "Drill_servo_usetime",   read_address = "R25080", calculate= true, write_address = "R20080"},
                        new MachineParameter { Name = "Drill_spindle_usetime", read_address = "R25082", calculate= true, write_address = "R20082"},
                        new MachineParameter { Name = "Drill_plc_usetime", read_address = "R25084", calculate= true, write_address = "R20084"},
                        new MachineParameter { Name = "Drill_inverter", read_address = "R25086", calculate= true, write_address = "R20086"},
                        new MachineParameter { Name = "Drill_total_Time", read_address = "R25088", calculate= true, write_address = "R20088"},

                         new MachineParameter { Name = "Drill_origin", read_address = "R25090", calculate= true, write_address = "R20090"},
                         new MachineParameter { Name = "Drill_loose_tools", read_address = "R25092", calculate= true, write_address = "R20092"},
                         new MachineParameter { Name = "Drill_measurement", read_address = "R25094", calculate= true, write_address = "R20094"},
                         new MachineParameter { Name = "Drill_clamping", read_address = "R25096", calculate= true, write_address = "R20096"},
                         new MachineParameter { Name = "Drill_feeder", read_address = "R25098", calculate= true, write_address = "R20098"},

                         new MachineParameter { Name = "Drill_current", read_address = "R25100", calculate= true, write_address = "R20100"},
                         new MachineParameter { Name = "Drill_voltage", read_address = "R25102", calculate= true, write_address = "R20102"},

                         new MachineParameter { Name = "Drill_cuttingSpeed", read_address = "R25200"},
                         new MachineParameter { Name = "Drill_power", calculate= true, write_address = "R20224"},
                         new MachineParameter { Name = "Drill_electricity", calculate= true, write_address = "R20220"},



                    };

                    context.MachineParameters.AddRange(initialParameters);
                    context.SaveChanges(); // 儲存到資料庫
                }
            }
        }

    }
}
