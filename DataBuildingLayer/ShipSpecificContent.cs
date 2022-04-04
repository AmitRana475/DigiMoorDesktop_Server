using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
       [Table("tblShipSpecificContent")]
       public class ShipSpecificContent
       {

              [Key]
              public int Id { get; set; }
              public int MId { get; set; }
              public string ShipId { get; set; }
              public string Content { get; set; }
              public DateTime? CreatedDate { get; set; }

       }
}
