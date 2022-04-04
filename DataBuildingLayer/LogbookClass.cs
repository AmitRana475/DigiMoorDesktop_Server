using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("Logbook")]
    public class LogbookClass
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string EventName { get; set; }
        public string UserName { get; set; }
        public string CrewGroup { get; set; }
        public string CrewGroup1 { get; set; }
        public string Status { get; set; }

        public string DateFrom1 { get; set; }
        public string DateTo1 { get; set; }

        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }

        public string IDLFrom { get; set; }
        public string IDLTo { get; set; }

        //[NotMapped]
        //public object DateFromTime { get; set; } = "12:00";

        //[NotMapped]
        //public object DateToTime { get; set; } = "12:00";

    }
}
