using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("JoiningShackle")]
   public class JoiningShackleClass
    {
        [Key]
        public int Id { get; set; }
        public int? LooseETypeId { get; set; }
        public string IdentificationNumber { get; set; }
        public string ManufactureName { get; set; }
        public decimal? MBL { get; set; }
        public string Type { get; set; }
        public string CertificateNumber { get; set; }
        public DateTime? DateReceived { get; set; }
        public string UniqueID { get; set; }
        public string Remarks { get; set; }
        public DateTime? DateInstalled { get; set; }
        public DateTime? OutofServiceDate { get; set; }
        public string ReasonOutofService { get; set; }
        public string OtherReason { get; set; }
        public string DamagedObserved { get; set; }

        [NotMapped]
        public string MooringOperation { get; set; }

        public int? MOpId { get; set; }
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
        public string DateInstalled1 { get; set; }

        [NotMapped]
        public string DateReceived1 { get; set; }

        [NotMapped]
        public string IsRopeInstalled { get; set; }

        public bool DeleteStatus { get; set; }

    }
}
