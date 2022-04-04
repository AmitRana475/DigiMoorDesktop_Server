using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("RopeDisposal")]
  public class RopeDisposalClass
    {
        [Key]
        public int Id { get; set; }
        public int RopeId { get; set; }
        public string DisposalPortName { get; set; }
        public string ReceptionFacilityName { get; set; }
        public DateTime? DisposalDate { get; set; }
        public int? RopeTail { get; set; }
      //  public int? NotificationId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }     
        public bool IsActive { get; set; }
        [NotMapped]
        public string CertificateNo { get; set; }
        [NotMapped]
        public string UniqueId { get; set; }
        public int? WinchId { get; set; }

    }
}
