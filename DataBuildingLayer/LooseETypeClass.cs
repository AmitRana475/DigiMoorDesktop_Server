using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("LooseEType")]
    public class LooseETypeClass
    {
        [Key]
        public int Id { get; set; }
        public string LooseEquipmentType { get; set; }
    }
}
