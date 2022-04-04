using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
       [Table("tblGeneralP")]
       public class GeneralP
       {
              [Key]
              public int Id { get; set; }
              public string Name { get; set; }
              public string Description { get; set; }
              public decimal MainValue { get; set; }
              [NotMapped]
              public object MainValue1 { get; set; }
              public Nullable<decimal> DefaultValue { get; set; }
              public string Units { get; set; }


       }
}
