using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("RetardtblClass")]
    public class RetardtblClass
    {
        [Key]
        public int Id { get; set; }
        public string DateID { get; set; }
        public DateTime RDate { get; set; } = DateTime.Now;
        public string RET_ADV { get; set; } = "RETARD";
        public string Times { get; set; } = "0.5";
        [NotMapped]
        public object RDate1 { get; set; }

    }
}
