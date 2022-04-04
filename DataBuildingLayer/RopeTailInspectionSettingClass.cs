using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("tblRopeTailInspectionSetting")]
   public class RopeTailInspectionSettingClass
    {
        [Key]
        public int Id { get; set; }
        public int MooringRopeType { get; set; }
        public int ManufacturerType { get; set; }
        public int MaximumRunningHours { get; set; }
        public int MaximumMonthsAllowed { get; set; }
        public decimal Rating1 { get; set; }
        public decimal Rating2 { get; set; }
        public decimal Rating3 { get; set; }
        public decimal Rating4 { get; set; }
        public decimal Rating5 { get; set; }
        public decimal Rating6 { get; set; }
        public decimal Rating7 { get; set; }

    }
}
