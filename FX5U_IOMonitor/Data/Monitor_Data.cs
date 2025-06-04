using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Data
{
   
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

    public class now_single // 當前連接總數
    {
        public string address { get; set; }
        public bool current_single { get; set; }

    }
    public class now_number // 當前連接總數
    {
        public string address { get; set; }
        public ushort current_number { get; set; }

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
  
}
