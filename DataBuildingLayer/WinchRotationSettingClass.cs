using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("tblwinchrotationsetting")]
    public class WinchRotationSettingClass
       {
        [Key]
              public int Id { get; set; }
              public int MooringRopeType { get; set; }

              public int ManufacturerType { get; set; }
              public int MaximumRunningHours { get; set; }
              public int MaximumMonthsAllowed { get; set; }
              public string LeadFrom { get; set; }
              public string LeadTo { get; set; }

        [NotMapped]
        public string RopeType { get; set; }
        [NotMapped]
        public string Manufacturer { get; set; }

    }
}
