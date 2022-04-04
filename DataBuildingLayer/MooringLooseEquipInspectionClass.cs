using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    
    [Table("MooringLooseEquipInspection")]
    public class MooringLooseEquipInspectionClass
    {

        [Key]
        public int Id { get; set; }
        public string InspectBy { get; set; }
        public DateTime? InspectDate { get; set; }
        public int LooseETypeId { get; set; }
        public string Number { get; set; }
        public string Condition { get; set; }
        public string Remarks { get; set; }
        public byte[] Photo1 { get; set; }
        public byte[] Photo2 { get; set; }

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public DateTime? CreatedDate { get; set; }      
        public bool IsActive { get; set; }
        public int? NotificationId { get; set; }
        
        [NotMapped]
        public string looseequipmenttype { get; set; }

        [NotMapped]
        public string InspectDate1 { get; set; }

        [NotMapped]
        public int InspectionFrequency { get; set; }
        [NotMapped]
        public int MaximumRunningHours { get; set; }

        [NotMapped]
        public string UniqueId { get; set; }
        [NotMapped]
        public int MaximumMonthsAllowed { get; set; }

        [NotMapped]
        public string Photo11 { get; set; }
        [NotMapped]
        public string Photo12 { get; set; }
        [NotMapped]
        public bool IsCheckedV { get; set; }

        public int LooseEtbPK { get; set; }
    }
}
