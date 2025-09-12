using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Data
{
    public class FrequencyConverAlarm : SyncableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public int FrequencyAlarmID { get; set; }
        public string FrequencyAlarmInfo { get; set; }

    }
    public class ServoDriveAlarm : SyncableEntity
    {
        [Key]
        public int Id { get; set; }

        public required string ServoDriveAlarmId { get; set; }
        public required string ServoDriveAlarmInfo { get; set; }

    }

}
