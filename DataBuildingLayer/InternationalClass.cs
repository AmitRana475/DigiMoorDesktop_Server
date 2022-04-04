using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("International")]
    public class InternationalClass
    {
        [Key]
        public int ID { get; set; }
        public DateTime RDate { get; set; }
        public string RET_ADV { get; set; } = "RETARD";
    }
}
