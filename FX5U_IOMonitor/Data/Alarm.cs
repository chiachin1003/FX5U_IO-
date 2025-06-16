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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string SourceMachine { get; set; }  // "Drill_IO" or "Swing_IO"
        public string address { get; set; }       // 監控故障的位置
        public string IPC_table { get; set; }
        public string Description { get; set; }       // 更換料件
        public string Error { get; set; }       // 故障內容

        public string Possible { get; set; }       // 原因

        public string Repair_steps { get; set; }  // 維修步驟
        public string classTag { get; set; }  // 料件所屬組別
        public int AlarmNotifyClass { get; set; }  // 單一警告通知項目
        public string? AlarmNotifyuser { get; set; }  // 警告通知使用者

        public bool current_single { get; set; }  // 當前狀態

        public virtual ICollection<AlarmHistory> AlarmHistories { get; set; } = new List<AlarmHistory>();
    }

    public class AlarmHistory : SyncableEntity
    {
        [Key]
        public int Id { get; set; }  // 主鍵建議使用 Id
        public int AlarmId { get; set; }             // 外鍵
        public virtual Alarm Alarm { get; set; }     // 導覽屬性
        public DateTime StartTime { get; set; }    // 故障發生時間
        public DateTime? EndTime { get; set; }     // 故障排除時間
        public TimeSpan? Duration { get; set; }    // 故障持續時間
        public DateTime RecordTime { get; set; }   // 記錄時間
        public int Records { get; set; }   // 記錄警告發送次數

    }

    public class AlarmHistoryViewModel
    {
        public string IPC_table { get; set; }
        public string Error { get; set; }
        public string classTag { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? Duration { get; set; }    // 故障持續時間

    }
}
