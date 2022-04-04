using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("AssignRopeToWinch")]
   public class AssignModuleToWinchClass
    {
        [Key]
        public int Id { get; set; }
        public int RopeId { get; set; }
     
        //public string Outboard { get; set; }

        public bool Outboard { get; set; } = true;
        [NotMapped]
        public bool Outboard1 { get; set; } = false;
        public int WinchId { get; set; }
        public int? RopeTail { get; set; }
        public string AssignedLocation { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public bool IsDelete { get; set; } = false;

        [NotMapped]
        public string AssignedNumber { get; set; }
        [NotMapped]
        public string CertificateNumber { get; set; }
        [NotMapped]
        public string UniqueId { get; set; }
        [NotMapped]
        public string Status { get; set; }
        [NotMapped]
        public string AssignedDate1 { get; set; }

      //  public string Lead { get; set; }
    }
}
