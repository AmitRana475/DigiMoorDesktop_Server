using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("LooseEDisposal")]
    public class LooseEDisposalClass
    {
        [Key]
        public int Id { get; set; }
        public int LooseETypeId { get; set; }
        public string LooseECertiNo { get; set; }        
        public string DisposalPortName { get; set; }
        public string ReceptionFacilityName { get; set; }
        public DateTime? DisposalDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public string CertificateNo { get; set; }
        public DateTime? DiscardedDate { get; set; }
    }
}
