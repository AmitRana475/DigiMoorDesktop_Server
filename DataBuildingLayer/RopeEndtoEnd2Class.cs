using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("RopeEndtoEnd2")]
    public class RopeEndtoEnd2Class
    {
        [Key]
        public int Id { get; set; }
        public int RopeId { get; set; }      
        public DateTime? EndtoEndDoneDate { get; set; }
        public bool? CurrentOutboadEndinUse { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public int? DamageId { get; set; }
        public int? MOpid { get; set; }

        [NotMapped]
        public bool? OutboadEndinUse { get; set; }
        [NotMapped]
        public string AssignedLocation { get; set; }
        [NotMapped]
        public DateTime AssignedDate { get; set; }
        [NotMapped]
        public string EndtoEndDoneDate1 { get; set; }
        //[NotMapped]
        //public int WinchId { get; set; }
        [NotMapped]
        public string AssignedNumber { get; set; }
        [NotMapped]
        public string CertificateNumber { get; set; }
        [NotMapped]
        public string UniqueId { get; set; }
        [NotMapped]
        public string CurrentOutboadEndinUse1 { get; set; }

        [NotMapped]
        public string WasRopeShifted { get; set; }
        [NotMapped]
        public bool Outboard { get; set; }
        public int? WinchId { get; set; }

    }
}
