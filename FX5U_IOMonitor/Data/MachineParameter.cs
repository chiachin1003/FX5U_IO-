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
        public double Unit_transfer { get; set; }

        public string Read_type { get; set; } = ""; // PLC Address，例如 "D100"
        public int Read_view { get; set; }  // PLC Address word 一次讀取N個

        public string Read_address { get; set; } = ""; // PLC Address，例如 "D100"
        public int Read_address_index { get; set; }  // PLC Address word 一次讀取N個

        public string Write_address { get; set; } = ""; // PLC Address，例如 "D100"
        public int? Write_address_index { get; set; }  // PLC Address word 一次讀取N個


        public int? History_NumericValue { get; set; }

        public int? now_NumericValue { get; set; } // 數值型
        public string? now_TextValue { get; set; } // 文字型（如果是 string 類型）

       


    }



    public class Blade_brand : SyncableEntity
    {

        [Key]
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
        public int Id { get; set; }
        public int blade_TPI_id { get; set; }

        public string blade_TPI_name { get; set; } = "";
        public int Machine_Number { get; set; }


    }

  
}
