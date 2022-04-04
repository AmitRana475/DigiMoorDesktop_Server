using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("HoliDayGroupName")]
    public class HoliDayGroupNameClass
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
}
