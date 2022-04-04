using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("LooseEDamageRecord")]
    public class LooseEDamageRecordClass
    {
        [Key]
        public int Id { get; set; }
        public int LooseETypeId { get; set; }
        public string CertificateNumber { get; set; }
        public string DamageObserved { get; set; }

        public string DamageReason { get; set; }
        [NotMapped]
        public string MooringOperation { get; set; }

    
        public int? MOpId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? DamageDate { get; set; }
        public string IncidentReport { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? NotificationId { get; set; }
        
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public string AssignedLocation { get; set; }
        [NotMapped]
        public string AssignedNumber { get; set; }
        [NotMapped]
        public string CertificateNumber1 { get; set; }

        [NotMapped]
        public string LooseEtype { get; set; }
        [NotMapped]
        public string DamageDate1 { get; set; }

    }
}
