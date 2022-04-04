using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("RuleTable")]
    public class RuleTableClass
    {
        [Key]
        public int ID { get; set; }
        public string RuleNames { get; set; }
        public bool Status { get; set; }
        public string StatusOPA90 { get; set; }
        public bool OneHour { get; set; }
    }
}
