using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("MinimumRopeAndTailsSetting")]
    public class MinimumRopeAndTailsClass
    {
        [Key]
        public int Id { get; set; }
        //public int? RopeTail { get; set; }
        public int? MinimumRopes { get; set; }
        public int? MinimumRopeTails { get; set; }
        
    }
}
