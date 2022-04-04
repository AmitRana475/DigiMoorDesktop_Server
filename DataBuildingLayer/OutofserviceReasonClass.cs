using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("OutofserviceReason")]
   public class OutofserviceReasonClass
    {
        [Key]
        public int Id { get; set; }
        public string Reason { get; set; }
    }
}
