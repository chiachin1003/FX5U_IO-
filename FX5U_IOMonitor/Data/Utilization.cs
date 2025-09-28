using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Data
{
   
    public class Utilization_Record 
    {
        [Key]
        public int Id { get; set; }  // 主鍵建議使用 Id

        // 外鍵關聯
        public int MachineParameterId { get; set; }
        public string Machine_Name { get; set; }

        // 統計時間區間
        [Column(TypeName = "timestamptz")]
        public DateTime StartTime { get; set; }    // 歷史統計開始
        [Column(TypeName = "timestamptz")]
        public DateTime EndTime { get; set; }      // 歷史統計結束

        // 歷史值（例如計次、用電總量等）
        public long? History_NumericValue { get; set; }
        public string PeriodTag { get; set; }
        public string Unit { get; set; }

        [Column(TypeName = "timestamptz")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
    public class UtilizationStatusRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(100)]
        public string Machinename { get; set; }

        [Required, MaxLength(50)]
        public string ReadBitAddress { get; set; }

        public int Status { get; set; } // 0=Idle, 1=Cutting

        public DateTime StartTime { get; set; } //狀態開始時間
        public DateTime EndTime { get; set; } //狀態結束時間

        public int DurationSeconds { get; set; } //狀態持續秒數

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 系統寫入時間

    }

}
