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

        public string Error { get; set; } = ""; // ✅ 預設值非 null
        public string Possible { get; set; } = "";
        public string Repair_steps { get; set; } = ""; // ✅ 一定要非 null
    }
}
