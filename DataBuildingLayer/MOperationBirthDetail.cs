using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("MOperationBirthDetail")]
    public class MOperationBirthDetail
    {
        [Key]
        public int OPId { get; set; }

        public string PortName { get; set; }
        public DateTime FastDatetime { get; set; }
        public DateTime CastDatetime { get; set; }

        public string BirthName { get; set; }
        public string BirthType { get; set; }
        public string MooringType { get; set; }
        public decimal? DraftArrivalFWD { get; set; }
        public decimal? DraftArrivalAFT { get; set; }
        public decimal? DraftDepartureFWD { get; set; }
        public decimal? DraftDepartureAFT { get; set; }
        public decimal? DepthAtBerth { get; set; }
        public string BerthSide { get; set; }
        public string VesselCondition { get; set; }
        public string ShipAccess { get; set; }
        public decimal? RangOfTide { get; set; }
        public string WindDirection { get; set; }
        public int WindSpeed { get; set; }

        public string AnySquall { get; set; }
        public decimal? CurrentSpeed { get; set; }
        public string Berth_exposed_SeaSwell { get; set; }
        public string SurgingObserved { get; set; }
        public string Any_Affect_Passing_Traffic { get; set; }
        public string Ship_was_continuously_contact_with_fender { get; set; }
        public string Any_Rope_Damaged { get; set; }

        public bool IsActive { get; set; } = true;

        
        public string PortDetails { get; set; }

        public string FacilityName { get; set; }

        public string Others { get; set; }
        public decimal? AirTemperature { get; set; }

        public bool AirTempCentigrate { get; set; } = true;


        [NotMapped]
        public string FastDatetime1 { get; set; }

        [NotMapped]
        public string CastDatetime1 { get; set; }

        [NotMapped]
        public int RowId { get; set; }

        [NotMapped]
        public string CheckMooringDamage { get; set; }
    }
}
