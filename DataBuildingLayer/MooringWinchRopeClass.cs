using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("MooringRopeDetail")]
    public class MooringWinchRopeClass
    {
        [Key]
        public int Id { get; set; }
        //public string RopeType { get; set; }

        public int? RopeTypeId { get; set; }
        public string RopeConstruction { get; set; }
        public decimal? DiaMeter { get; set; }
        public decimal? Length { get; set; }
        // public decimal? TtlCroppedLength { get; set; }
        public decimal? MBL { get; set; }
        public decimal? LDBF { get; set; }
        public decimal? WLL { get; set; }
        //public string ManufacturerName { get; set; }
        public int ManufacturerId { get; set; }
        public string CertificateNumber { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? InstalledDate { get; set; }
        public string RopeTagging { get; set; }
        public DateTime? OutofServiceDate { get; set; }
        public string ReasonOutofService { get; set; }
        public string OtherReason { get; set; }
        public string DamageObserved { get; set; }
        public string IncidentReport { get; set; }

        [NotMapped]
        public string MooringOperation { get; set; }
        public int? MooringOperationID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        //public int? CurrentRunningHours { get; set; }

        public decimal? CurrentRunningHours { get; set; } = 0;


        public decimal? CurrentLeadRunningHours { get; set; } = 0;

        public int? MaxRunningHours { get; set; }
        public int? MaxMonthsAllowed { get; set; }

        // public int? StartCounterHours { get; set; }
        public decimal? StartCounterHours { get; set; } = 0;
        public int? RopeTail { get; set; }
        public bool DeleteStatus { get; set; }

        public string Remarks { get; set; }
        public string UniqueID { get; set; }
        public DateTime? InspectionDueDate { get; set; }
        [NotMapped]
        public string IsRopeOutOfS { get; set; }
        [NotMapped]
        public string RopeType { get; set; }
        [NotMapped]
        public string ManufacturerName { get; set; }
        [NotMapped]
        public string Location { get; set; }
        [NotMapped]
        public string AssignedWinch { get; set; }
        [NotMapped]
        public int WinchId { get; set; }

        [NotMapped]
        public string InstalledDate1 { get; set; }

        [NotMapped]
        public string InspectionDueDate1 { get; set; }

        [NotMapped]
        public string IsRopeInstalled { get; set; }
        [NotMapped]
        public int RowId { get; set; }
        [NotMapped]
        public decimal? CurrentLength { get; set; }

        [NotMapped]
        public string AttachmentPath { get; set; }
        [NotMapped]
        public string LineResidual { get; set; }
        [NotMapped]
        public int ResidualID { get; set; }

        [NotMapped]
        public string AttachmentVisibility { get; set; }
        //public int? OutOfService_WinchId { get; set; }
    }
}
