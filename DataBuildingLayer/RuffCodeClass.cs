using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("RuffCode")]
    public class RuffCodeClass
    {
        [Key]
        public int Id { get; set; }
        public string keyno { get; set; }
        public string keycode { get; set; }
    }
}
