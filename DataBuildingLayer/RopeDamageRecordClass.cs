using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("RopeDamageRecord")]
   public class RopeDamageRecordClass
    {
        [Key]
        public int Id { get; set; }
        public int RopeId { get; set; }
        public string DamageObserved { get; set; }
        public string IncidentReport { get; set; }
       // public string MooringOperation { get; set; }
        public string DamageLocation { get; set; }
        public string DamageReason { get; set; }
        public int? MOPId { get; set; }
        public int? RopeTail { get; set; }
        public DateTime? DamageDate { get; set; }
        public int? NotificationId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public string IncidentActlion { get; set; }

       

        [NotMapped]
        public string AssignedLocation { get; set; }
        [NotMapped]
        public string AssignedNumber { get; set; }
        [NotMapped]
        public string CertificateNumber { get; set; }
        [NotMapped]
        public string UniqueId { get; set; }

        [NotMapped]
        public string DamageDate1 { get; set; }


        public int? WinchId { get; set; }
    }
}
