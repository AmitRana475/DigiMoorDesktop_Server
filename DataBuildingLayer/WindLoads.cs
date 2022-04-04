using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
       [Table("[tblWindLoads]")]
       public class WindLoads
       {
              [Key]
              public int Id { get; set; }
              public string Name { get; set; }
              public string Notation { get; set; }
              public Nullable<decimal> MainValue { get; set; }
              public string Units { get; set; }
       }
}
