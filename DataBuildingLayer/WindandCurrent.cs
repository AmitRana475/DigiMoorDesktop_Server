using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataBuildingLayer
{
       [Table("tblWindandCurrent")]
       public class WindandCurrent
       {
              [Key]
              public int Id { get; set; }
              public string Name { get; set; }
              public string Description { get; set; }
        public decimal MainValue { get; set; } = 0;

        [NotMapped]
        public object MainValue1 { get; set; } = 0;
              public Nullable<decimal> DefaultValue { get; set; }
              public string Units { get; set; }
       }
}
