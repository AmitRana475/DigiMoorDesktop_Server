using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("OPAStartStop")]
    public class OPAStartStopClass
    {
        [Key]
        public int id { get; set; }
        public DateTime OPAStart { get; set; }
        public DateTime OPAStop { get; set; }
        [NotMapped]
        public object OPAStart1 { get; set; }
        [NotMapped]
        public object OPAStop1 { get; set; }
        public string startDateID { get; set; }
        public string stopDateID { get; set; }
        public bool LongLimit { get; set; }
    }
}
