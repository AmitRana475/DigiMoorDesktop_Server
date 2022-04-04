using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    public class MODamageRopeClass
    {
        public int WinchId { get; set; }
        public string DamageLocation { get; set; }
        public string DamageObserved { get; set; }
        public string DamageObserved1 { get; set; }
        public string DamageReason { get; set; }
        public int RopeId { get; set; }
        public DateTime? SplicedDate { get; set; }
        public string IncidentReport { get; set; }
        public string SplicedMethod { get; set; }
        public string ActionAfterDamage { get; set; }
        public string IncidentAction { get; set; }
        public int MOPId { get; set; }
    
        public string SplicingDoneBy { get; set; }

        public DateTime? CroppedDate { get; set; }
        public string CroppedOutboardEnd { get; set; }
        public decimal? LengthofCroppedRope { get; set; }
        public string ReasonofCropping { get; set; }

        public DateTime? OutofServiceDate { get; set; }
        public string ReasonOutofService { get; set; }

        public string otherReason { get; set; }
        public string MooringOperation { get; set; }
        public DateTime? EndtoEndDoneDate { get; set; }
        public bool? CurrentOutboadEndinUse { get; set; }

        public decimal? LengthofCroppedRope1 { get; set; }
        public DateTime? DiscaredDate { get; set; }
      

        [NotMapped]
        public string IsCropped { get; set; }
    }
}
