using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Data
{
    public class DataStore 
    {

        public static connect_Summary Drill_connect_Summary = new connect_Summary();
        public static connect_Summary Swing_connect_Summary = new connect_Summary();
        public static List<now_single> alarm = new List<now_single>();

    }
  
    public class connect_Summary //總連接數介面
    {
        public int connect { get; set; }
        public int disconnect { get; set; }
        public int total_number { get; set; }
        public string read_time { get; set; }

    }
    public class IO_DataBase //元件監控時間說明
    {
        public string address { get; set; } //連結地址

        public bool IO { get; set; } // Ture = 輸入信號 ，False = 輸出信號表

        public bool IsMechanical { get; set; } // Ture = 機械式，False = 電子式
        public int MaxLife { get; set; } // 最大壽命
        public string ClassTag { get; set; } //分組類別
        public string Comment { get; set; } // 當前資料
        public string equipmentDescription { get; set; } //設備描述
        public int equipment_use { get; set; } // 當前設備使用次數

        public string Part_InstallationTime { get; set; } // 安裝時間
        public string Part_RemovalTime { get; set; }  // 元件移除時間
        public string Historical_usage { get; set; } // 歷史使用次數
        public string[] Historical_usage_times { get; set; } // 歷史使用時間
        public bool? current_single { get; set; } //當前讀取數值or信號

        public int Setting_green { get; set; } //使用者設定健康健康狀態百分比
        public int Setting_yellow { get; set; }//使用者設定健康黃燈百分比
        public int Setting_red { get; set; } //使用者設定健康異警百分比

        public float current_HI_state { get; set; } //當前元件健康狀態

        public double percent { get; set; }

        public double RUL { get; set; } //剩餘使用壽命


    }

    public class HistoryRecord
    {
        public string StartTime { get; set; }  // 元件啟用時間
        public string EndTime { get; set; }    // 元件結束時間
        public int UsageCount { get; set; }    // 該元件總使用次數
    }

 
    public class Swing_Status //鋸床10種狀態監控
    {
        public string motorcurrent { get; set; }  //馬達電流
        public string cuttingspeed { get; set; }    // 切削速度
        public string avg_V { get; set; }    // 電壓平均
        public string avg_mA { get; set; }    // 電流平均
        public string oil_pressure { get; set; }   // 油壓張力

        public string power { get; set; }// 消耗功率
        public string power_consumption { get; set; }// 累積用電度數
        public string Sawing_countdown_time { get; set; }// 鋸切倒數
        public string Remaining_Dutting_tools { get; set; }  // 加工剩餘刀數

        public string Runtime { get; set; }  // 總運轉時間

    }

    public class Drill_status //鑽床10種狀態監控
    {
        // 10種鑽床顯示介面
        public string Runtime { get; set; }  // 機器使用時間
        public string Frequency_Converter_usetime { get; set; }  // 變頻器使用時間
        public string PLC_usetime { get; set; }  // PLC使用時間
        public string Spindle_usetime { get; set; }  // 主軸啟動累積時間
        public string Servo_drives_usetime { get; set; }  // 伺服驅動器介面累積時間

        public int origin { get; set; }  // 回原點次數
        public int loose_tools { get; set; }    // 主軸鬆刀次數
        public int measurement { get; set; }    // 刀具量測次數
        public int clamping { get; set; }  // 送料台夾料檢知次數
        public int feeder { get; set; }    // 送料機夾鬆次數

        // 主頁面資料

        public string Current { get; set; }    // 電流

        public string Voltage { get; set; }    // 切削速度
        public string power { get; set; } //消耗功率
        public string du { get; set; } //度電


    }
    public class SawBand_Status //鋸帶10種狀態監控
    {
        public string Sawband_brand { get; set; }  // 鋸帶廠牌
        public string Saw_teeth { get; set; }  // 鋸帶齒數
        public string Saw_blade_material { get; set; }  // 鋸帶材質
        public string Sawband_speed { get; set; }  // 鋸帶速度
        public string saw_motors_usetime { get; set; }  // 鋸帶馬達使用累積時間

        public string power { get; set; }  // 鋸帶馬達鋸切馬力
        public string Maximum_current { get; set; }    // 鋸帶馬達鋸切最大電流
        public string area { get; set; }    // 鋸切累積面積
        public string saw_blade_life { get; set; }  // 鋸帶壽命
        public string usage { get; set; }    // 鋸帶張力使用累計

        public string Average { get; set; }    // 鋸帶電流
        public string Voltage { get; set; }    // 鋸帶電壓
        public string total_time { get; set; }    // 鋸帶電壓

    }
    //public class alarm_single //元件監控時間說明
    //{
    //    public string address { get; set; }
    //    public bool value { get; set; }

    //}
    public class now_single // 當前連接總數
    {
        public string address { get; set; }
        public bool current_single { get; set; }

    }
    // 監控資料庫是否更新用參數
    public class MachineParameterChangedEventArgs : EventArgs
    {
        public string Name { get; set; } = "";
        public string OldValue { get; set; } = "";
        public string NewValue { get; set; } = "";
    }
    public class IOSectionInfo
    {
        public string Prefix { get; set; } = "";        // "X" 或 "Y"
        public int StartAddress { get; set; }           // 開始地址（十進位）
        public int EndAddress { get; set; }             // 結束地址（十進位）
        public int BlockCount { get; set; }             // 區塊總數
        public List<string> SplitPoints { get; set; } = new(); // 切斷點（已格式化字串）
    }
    public class MachineStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
