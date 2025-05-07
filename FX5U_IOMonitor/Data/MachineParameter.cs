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

        public string Name { get; set; } = ""; // 參數名稱，例如 "Sawing_electricity"
        public bool calculate { get; set; } = false;// 是否需要計算

        public string read_address { get; set; } = ""; // PLC Address，例如 "D100"
        public string write_address { get; set; } = ""; // PLC Address，例如 "D100"

        public string write_type { get; set; } = ""; // 

        public string history_TextValue { get; set; } = ""; //
        public ushort history_NumericValue { get; set; }  // 

        public ushort? now_NumericValue { get; set; } // 數值型
        public string? now_TextValue { get; set; } // 文字型（如果是 string 類型）


    }
  
    
        
            
            

        

}
