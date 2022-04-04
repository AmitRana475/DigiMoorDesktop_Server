using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("Holidays")]
    public class HolidaysClass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public  DateTime  HolidayDate { get; set; }
        public int Gid { get; set; }
    }
}
