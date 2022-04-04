using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataBuildingLayer
{
       [Table("tblMooringLines")]
       public class MooringLines
       {
              public MooringLines()
              {
                     decimal val = 0.00m;
                     Xch = val;
                     Ych = val;
                     Zch = val;
                     Xbl = val;
                     Ybl = val;
                     Zbl = val;
                     l0 = val;
                     E = val;
                     n = val;
                     a = val;
                     MBSrope = val;
              }

              [Key]
              public int Id { get; set; }
              [NotMapped]
              public string AssignNumber { get; set; }
              [NotMapped]
              public string Location { get; set; }        
              [NotMapped]
              public string Certi_No { get; set; }
              [NotMapped]
              public string UniqueId { get; set; }
              //public Nullable<decimal> Xch { get; set; }
              //public Nullable<decimal> Ych { get; set; }
              public decimal Xch { get; set; }
              public decimal Ych { get; set; }
              public decimal Zch { get; set; }
              public decimal Xbl { get; set; }
              public decimal Ybl { get; set; }
              public decimal Zbl { get; set; }
              //public Nullable<decimal> l0 { get; set; }
              public decimal l0 { get; set; }
              public decimal E { get; set; }
              public decimal n { get; set; }
              public decimal a { get; set; }
              public decimal MBSrope { get; set; }
              public int RopeId { get; set; }


              [NotMapped]
              public object Xch1 { get; set; }
              [NotMapped]
              public object Ych1 { get; set; }
              [NotMapped]
              public object Zch1 { get; set; }
              [NotMapped]
              public object Xbl1 { get; set; }
              [NotMapped]
              public object Ybl1 { get; set; }
              [NotMapped]
              public object Zbl1 { get; set; }
              [NotMapped]
              public object l01 { get; set; }
              [NotMapped]
              public object E1 { get; set; }
              [NotMapped]
              public object n1 { get; set; }
              [NotMapped]
              public object a1 { get; set; }
              [NotMapped]
              public object MBSrope1 { get; set; }

       }
}
