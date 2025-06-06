using FX5U_IOMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Data
{
    public class MachineIOTranslation : SyncableEntity
    {
        [Key]
        public int Id { get; set; }
        public int MachineIOId { get; set; }
        public string LanguageCode { get; set; }
        public string Comment { get; set; }

        public virtual MachineIO MachineIO { get; set; }
    }
}
