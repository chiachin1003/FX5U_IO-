using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Data
{
   
   
    public class UtilizationStatusRecord : SyncableEntity
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

    }

}
