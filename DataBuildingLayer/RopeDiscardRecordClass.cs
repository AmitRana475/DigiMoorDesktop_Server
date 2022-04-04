using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
   public class RopeDiscardRecordClass
    {
        public int Id { get; set; }
        public int RopeId { get; set; }
        public DateTime? OutofServiceDate { get; set; }
        public string ReasonOutofService { get; set; }
        public string OtherReason { get; set; }
        public string DamageObserved { get; set; }
        public string MooringOperation { get; set; }
        public int MOpId { get; set; }
        [NotMapped]
        public string CertificateNumber { get; set; }
        [NotMapped]
        public string RopeType { get; set; }
        [NotMapped]
        public string OutofServiceDate1 { get; set; }

        [NotMapped]
        public string UniqueId { get; set; }

        public int LooseETypeId { get; set; }

        public string looseequipmenttype { get; set; }

       // public int OutOfService_WinchId { get; set; }

    }
}
