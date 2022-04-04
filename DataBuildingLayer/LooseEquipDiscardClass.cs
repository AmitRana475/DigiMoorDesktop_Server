using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
   public class LooseEquipDiscardClass
    {
        public int Id { get; set; }
        public int LooseETypeId { get; set; }
        public DateTime? OutofServiceDate { get; set; }
        public string ReasonOutofService { get; set; }
        public string OtherReason { get; set; }
        public string DamageObserved { get; set; }
        public string MooringOperation { get; set; }

        public int? MOPId { get; set; }
        public string CertificateNumber { get; set; }
    }
}
