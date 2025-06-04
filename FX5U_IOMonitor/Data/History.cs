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
    public class History : SyncableEntity
    {
        [Key]
        public int Id { get; set; }

        public string SourceDbName { get; set; }  // "Drill" or "Sawing" or else
        public string Address { get; set; }       // X31A 這類地址

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int usetime { get; set; }       // 歷史使用次數

        public int MachineIOId { get; set; }

    


    }
}
