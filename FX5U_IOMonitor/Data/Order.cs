using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Data
{
    

    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderdetailid { get; set; }

        [Required]
        [MaxLength(35)]
        public string projecttitle { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string piecename { get; set; } = string.Empty;

        [Required]
        public int piececount { get; set; }

        public TimeSpan? estimatedtime { get; set; }

        public TimeSpan? actualtime { get; set; }

        public DateTime? processingstarttime { get; set; }

        public DateTime? processingendtime { get; set; }

        public int? processingstatus { get; set; }

        public bool issynced { get; set; } = false;

        public DateTime createdat { get; set; } = DateTime.UtcNow;

        public DateTime updatedat { get; set; } = DateTime.UtcNow;
    }

    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderid { get; set; }

        [Required]
        [MaxLength(35)]
        public string projecttitle { get; set; } = string.Empty;

        public int pendingpiececount { get; set; }

        public int completedpiececount { get; set; }

        public int totalpiececount { get; set; }

        public TimeSpan? plannedtotaltime { get; set; }

        public TimeSpan? actualtotaltime { get; set; }

        public bool issynced { get; set; } = false;

        public DateTime createdat { get; set; } = DateTime.UtcNow;

        public DateTime updatedat { get; set; } = DateTime.UtcNow;
    }
   
    public class ProjectSummary
    {
        public string? LatestProject { get; set; }          // 最新專案名稱
        public int DistinctCount { get; set; }              // 不重複的專案數量
        public List<string> ProjectList { get; set; } = new(); // 所有不重複專案名稱列表
    }
    public class ProjectSummaryResult
    {
        public int Total { get; set; }
        public int Completed { get; set; }
        public int Uncompleted { get; set; }
        public TimeSpan TotalEstimated { get; set; }
        public TimeSpan TotalActual { get; set; }
        public double CompletionRate { get; set; }
    }
    public class OrderDetailDisplay
    {
        public int No { get; set; }
        public string Piecename { get; set; } = "";
        public int Piececount { get; set; }
        public TimeSpan? Estimatedtime { get; set; }
        public TimeSpan? Actualtime { get; set; }
        public DateTime? Processingstarttime { get; set; }
        public DateTime? Processingendtime { get; set; }
    }
}
