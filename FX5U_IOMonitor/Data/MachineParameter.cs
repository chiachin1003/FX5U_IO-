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
   
    public class MachineParameter
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



    public class Blade_brand
    {

        [Key]
        public int Id { get; set; }

        public int Brand_Id { get; set; }
        public string Brand_Name { get; set; } = "";// 是否需要計算

        public int Material_Id { get; set; } 
        public string Material_Name { get; set; } = "";

        public int Type_Id { get; set; }

        public string Type_Name { get; set; } = ""; 

        public int Machine_Number { get; set; } 

    }
    public class Blade_brand_TPI
    {

        [Key]
        public int Id { get; set; }
        public int TPI_Id { get; set; }

        public string Name { get; set; } = "";
        public int Machine_Number { get; set; }

    }

  
}
