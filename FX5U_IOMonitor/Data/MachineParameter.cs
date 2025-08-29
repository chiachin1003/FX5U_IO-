using FX5U_IOMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Data
{
   
    public class MachineParameter : SyncableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Machine_Name { get; set; } = ""; //哪一台機檯
        public string Name { get; set; } = ""; // 監控的參數名稱，例如 "Sawing_electricity"
        public bool Calculate { get; set; } = false;// 是否需要計算
        public int Calculate_type { get; set; } //計算的型態

        public string Read_type { get; set; } = ""; // 讀取型態
        public int Read_view { get; set; }  // PLC Address word 一次讀取N個

        public string Read_address { get; set; } = ""; // 讀取地址(公制)
        public double Unit_transfer { get; set; } //公制轉換

        public string? Read_addr { get; set; } = ""; // 讀取地址(英制)
        public double? Imperial_transfer { get; set; } //英制轉換

        public int Read_address_index { get; set; }  // PLC Address word 一次讀取N個

        public string Write_address { get; set; } = ""; // 寫入地址
        public int? Write_address_index { get; set; }  // PLC Address word 一次寫入N個

        public int? History_NumericValue { get; set; } //歷史當前紀錄

        public int? now_NumericValue { get; set; } // 數值型
        public string? now_TextValue { get; set; } // 文字型（如果是 string 類型）
        public virtual ICollection<MachineParameterHistoryRecode> HistoryRecodes { get; set; } = new List<MachineParameterHistoryRecode>();
        /// <summary>
        /// 根據目前單位回傳對應的位址。若英制位址為空，則 fallback 使用公制位址。
        /// </summary>
        public string GetAddress(string unit)
        {
            if (unit == "Imperial" && !string.IsNullOrWhiteSpace(Read_addr))
                return Read_addr;

            return Read_address;
        }
        /// <summary>
        /// 根據目前單位回傳對應倍率。若英制倍率為空或0，則 fallback 使用公制倍率。
        /// </summary>
        public double GetScale(string unit)
        {
            if (unit == "Imperial" && !string.IsNullOrWhiteSpace(Read_addr))
            {
                return Imperial_transfer.HasValue && Imperial_transfer.Value > 0
                    ? Imperial_transfer.Value
                    : Unit_transfer;
            }

            return Unit_transfer;
        }
    }
   
    public class MachineParameterHistoryRecode : SyncableEntity
    {
        [Key]
        public int Id { get; set; }  // 主鍵建議使用 Id

        // 外鍵關聯
        public int MachineParameterId { get; set; }
        public virtual MachineParameter MachineParameter { get; set; }

        // 統計時間區間
        public DateTime StartTime { get; set; }    // 歷史統計開始
        public DateTime EndTime { get; set; }      // 歷史統計結束

        // 歷史值（例如計次、用電總量等）
        public int? History_NumericValue { get; set; }

        // 歸零的時間（如為定時歸零，或是手動重置）
        public DateTime? ResetTime { get; set; }

        // 備註（可用來說明這段紀錄的情境）
        public string? ResetBy { get; set; }  // "系統自動"、"使用者帳號"
        public string? PeriodTag { get; set; } // 例如："2025M06" 或 "2025W24"

    }

  
    public class Blade_brand : SyncableEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public int blade_brand_id { get; set; }
        public string blade_brand_name { get; set; } = "";// 是否需要計算

        public int blade_material_id { get; set; } 
        public string blade_material_name { get; set; } = "";

        public int blade_Type_id { get; set; }

        public string blade_Type_name { get; set; } = ""; 

        public int Machine_Number { get; set; }
       
    }

    public class Blade_brand_TPI : SyncableEntity
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        public int blade_TPI_id { get; set; }

        public string blade_TPI_name { get; set; } = "";
        public int Machine_Number { get; set; }


    }

    public class HistoryRecordDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Delta { get; set; }
    }
}
