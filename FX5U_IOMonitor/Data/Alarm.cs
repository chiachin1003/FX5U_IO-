using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FX5U_IOMonitor.Models;

namespace FX5U_IOMonitor.Data
{
    public class Alarm : SyncableEntity
    {
        [Key]
        public int Id { get; set; }

        public string SourceDbName { get; set; }  // "Drill_IO" or "Swing_IO"
        public string M_Address { get; set; }       // 監控故障的位置
        public string Description { get; set; }       // 更換料件
        public string Error { get; set; }       // 故障內容

        public string Possible { get; set; }       // 原因

        public string Repair_steps { get; set; }  // 維修步驟
        public string classTag { get; set; }  // 料件所屬組別
        public int AlarmNotifyClass { get; set; }  // 單一警告通知項目
        public string? AlarmNotifyuser { get; set; }  // 警告通知使用者


        public DateTime MountTime { get; set; }  //發生時間
        public DateTime UnmountTime { get; set; }    //異常排除時間


        public bool current_single { get; set; }  // 當前狀態


        [NotMapped]
        public ICollection<History> Histories { get; set; } = new List<History>();
      
        

    }

}
