using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("Vessel")]
    public class VesselClass
    {
        [Key]
        public int Vid { get; set; }
        public int vessel_ID { get; set; }
        public string VesselName { get; set; }
        public int imo { get; set; }
        public string Flag { get; set; }
        public string Email { get; set; }
        public string website { get; set; }

    }
}
