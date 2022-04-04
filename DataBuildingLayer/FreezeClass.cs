using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("Freeze")]
    public class FreezeClass
    {
        [Key]
        public int Id { get; set; }
        public int FreezeDays { get; set; }
        public string FreezeStatus { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
