using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("RopeEndtoEnd")]
    public class RopeEndtoEndClass
    {
        [Key]
        public int Id { get; set; }

        // public int RopeId { get; set; }
        public string CertificateNumber { get; set; }
        public bool? OutboardEndinUse { get; set; }
        public string AssignedWinch { get; set; }
        public string AssignedLocation { get; set; }
        public DateTime? EndtoEndDoneDate { get; set; }
        public bool? CurrentOutboadEndinUse { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        //[NotMapped]
        //public string OutboardEndinUse1 { get; set; }
        //[NotMapped]
        //public string CurrentOutboadEndinUse1 { get; set; }
    }
}
