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
        public string FrequencyErrorDetail { get; set; }
        public string FrequencySolution { get; set; }
        public string FrequencyStatus { get; set; }

    }
    public class ServoDriveAlarm : SyncableEntity
    {
        [Key]
        public int Id { get; set; }

        public required int ServoDriveAlarmId { get; set; }
        public required string ServoDriveAlarmInfo { get; set; }
        public required string ServoDriveErrorDetail { get; set; }
        public required string ServoDriveSolution { get; set; }
    }

    public class ControlAlarm : SyncableEntity
    {
        [Key]
        public int Id { get; set; }

        public required int ControlAlarmId { get; set; }
        public required string ControlAlarmInfo { get; set; }
        public required string ControlErrorDetail { get; set; }
        public required string ControlSolution { get; set; }
    }
}
