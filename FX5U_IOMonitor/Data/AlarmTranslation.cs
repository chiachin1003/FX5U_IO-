using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Data
{
    public class AlarmTranslation
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Alarm")]
        public int AlarmId { get; set; }  // ✅ int 型別外鍵，對應 Alarm.Id

        public virtual Alarm Alarm { get; set; }

        public string LanguageCode { get; set; } // e.g. "zh-TW", "en-US"

        public string Error { get; set; }         // 當地語系下的錯誤訊息
        public string Possible { get; set; }      // 當地語系下的可能原因
        public string Repair_steps { get; set; }   // 當地語系下的維修步驟
    }
}
