using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("MOUsedWinchTbl")]
    public class MOUsedWinchTbl
    {
        [Key]
        public int Id { get; set; }
        public int OperationID { get; set; }
        public string GridID { get; set; }

        public DateTime OPDateFrom { get; set; }

        public DateTime OPDateTo { get; set; }
        public int  WinchId { get; set; }
        public int RopeId { get; set; }
       // public int? RunningHours { get; set; }

        public decimal? RunningHours { get; set; }

        public int? NotificationId { get; set; }        
        public int? RopeTail { get; set; }
        public string Lead { get; set; }
        public string Lead1 { get; set; }
        public bool Outboard { get; set; }
    }
}
