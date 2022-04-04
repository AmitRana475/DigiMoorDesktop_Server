using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("OverTime")]
    public class OverTimeClass
    {
        [Key]
        public int cid { get; set; }
        public string name { get; set; }
        public string UserName { get; set; }
        public int NrmlWHrs { get; set; }
        public int SatWHrs { get; set; }
        public int SunWhrs { get; set; }
        public int HolidayWHrs { get; set; }

        public int NrmlRates { get; set; }
        public int SatRates { get; set; }
        public int SunRates { get; set; }
        public int HolidayRates { get; set; }

        public int FixedOverTime { get; set; }
        public decimal HourlyRate { get; set; }
        public string Currency { get; set; }
        public string holidays { get; set; }
        public int did { get; set; }

        //public string position { get; set; }
        //public DateTime ServiceFrom { get; set; }
        //public DateTime ServiceTo { get; set; }
        //public string CDC { get; set; }
        //public string empno { get; set; }
        //public string pswd { get; set; }
        //public string comments { get; set; }
        //public string department { get; set; }
        //public string SeaWk { get; set; }
        //public string SeaNWK { get; set; }
        //public string PortWk { get; set; }
        //public string PortNWK { get; set; }
        //public DateTime dates { get; set; }
        //public decimal SeaWH { get; set; }
        //public decimal portWH { get; set; }
        //public int Gid { get; set; }
        //public int rid { get; set; }
    }
}
