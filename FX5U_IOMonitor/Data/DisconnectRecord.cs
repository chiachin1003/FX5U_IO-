using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Data
{
    public class DisconnectRecord :SyncableEntity
    {
        [Key]
        public int Id { get; set; }  
        public string ConnectOriginate { get; set; }            

        public DateTime StartTime { get; set; }    
        public DateTime? EndTime { get; set; }
        public int Records { get; set; }   

        public string? Note { get; set; }   // 備註

    }
}
