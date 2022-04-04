using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("RopeSplicingRecord")]
    public class RopeSplicingClass
    {
        [Key]
        public int Id { get; set; }
        public int RopeId { get; set; }
        public string SplicingMethod { get; set; }
        public string SplicingDoneBy { get; set; }
        public DateTime? SplicingDoneDate { get; set; }
        [NotMapped]
        public decimal? LengthofCroppedRope { get; set; }
        [NotMapped]
        public string ReasonofCropping { get; set; }
        [NotMapped]
        public string IsCropped { get; set; }
        [NotMapped]
        public string CroppedOutboardEnd { get; set; }
        [NotMapped]
        public string CroppedOutboardEnd1 { get; set; }
        public int? RopeTail { get; set; }
        public int? NotificationId { get; set; }
        public int? DamageId { get; set; }
        public int? MOpid { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public string AssignedLocation { get; set; }
        [NotMapped]
        public string SplicingDoneDate1 { get; set; }
        [NotMapped]
        public string AssignedNumber { get; set; }
        [NotMapped]
        public string CertificateNumber { get; set; }

        [NotMapped]
        public string UniqueId { get; set; }

        public int? WinchId { get; set; }

    }
}
