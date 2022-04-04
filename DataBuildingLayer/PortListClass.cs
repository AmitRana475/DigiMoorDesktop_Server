using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("PortList")]
    public class PortListClass
    {
        [Key]
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string PortName { get; set; }
        public string FacilityName { get; set; }
        public string IMOPortFacilityNumber { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
