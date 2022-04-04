using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("CrossShiftingWinch")]
    public class CrossShiftingWinchClass
    {
        [Key]
        public int Id { get; set; }
        public int RopeId { get; set; }
        public int WinchId { get; set; }
        public bool OutboardEndinUse { get; set; }
        public DateTime? DateofShifting { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

       
        [NotMapped]
        public string AssignedLocation { get; set; }
        [NotMapped]
        public string AssignedNumber { get; set; }
        [NotMapped]
        public string CertificateNumber { get; set; }
        [NotMapped]
        public bool OutboardEndinUse1 { get; set; }

        [NotMapped]
        public string CurrentOutboardEndinUse { get; set; }
    }
}
