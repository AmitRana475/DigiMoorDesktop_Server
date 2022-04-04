using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("RopeTail")]
    public class RopeTailClass
    {
        [Key]
        public int Id { get; set; }
        public int? LooseETypeId { get; set; }       
        public string ManufactureName { get; set; }
        public string RopeConstruction { get; set; }
        public decimal? DiaMeter { get; set; }
        public decimal? Length { get; set; }
        public decimal? MBL { get; set; }
        public decimal? LDBF { get; set; }
        public decimal? WLL { get; set; }
        public int? MaxRunHours { get; set; }
        public int? MaxYearServiceMonth { get; set; }
        public string CertificateNumber { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? InstalledDate { get; set; }
        public string RopeTagging { get; set; }
        public DateTime? OutofServiceDate { get; set; }
        public string ReasonOutofService { get; set; }
        public string OtherReason { get; set; }
        public string DamageObserved { get; set; }
        [NotMapped]
        public string MooringOperation { get; set; }

        [NotMapped]
        public string ReceivedDate1 { get; set; }
        [NotMapped]
        public string InstalledDate1 { get; set; }

        [NotMapped]
        public decimal? DiaMeter1 { get; set; }

        public string Remarks { get; set; }
        public int? MOpId { get; set; }
        public string UniqueID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
       [NotMapped]
        public string OutofServiceDate1 { get; set; }

        public DateTime? InspectionDueDate { get; set; }

        [NotMapped]
        public string InspectionDueDate1 { get; set; }

        [NotMapped]
        public string IsRopeInstalled { get; set; }

        public bool DeleteStatus { get; set; }
    }
}
