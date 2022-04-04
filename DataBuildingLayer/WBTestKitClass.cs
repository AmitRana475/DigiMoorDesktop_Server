
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataBuildingLayer
{
    [Table("WinchBreakTestKit")]
    public class WBTestKitClass
    {
        [Key]
        public int Id { get; set; }
        public string ManufacturerName { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public string ReasonOutofService { get; set; }
        public string OtherReason { get; set; }

        public DateTime? InstalledDate { get; set; }
        public DateTime? InspectionDueDate { get; set; }

        public string UniqueID { get; set; }

        [NotMapped]
        public string IsRopeInstalled { get; set; }
        [NotMapped]
        public string InstalledDate1 { get; set; }

        [NotMapped]
        public string InspectionDueDate1 { get; set; }

        [NotMapped]
        public string ReceivedDate1 { get; set; }
       

        public DateTime? OutofServiceDate { get; set; }

        public DateTime? ReceivedDate { get; set; }
        public bool DeleteStatus { get; set; }

        public string CertificateNumber { get; set; }

    }
}
