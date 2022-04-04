using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("tblMenuName")]
   public class MenuNameClass
    {
        public int Id { get; set; }
        public int Mid { get; set; }
        public string MenuName { get; set; }

        public int Type { get; set; }
        [NotMapped]
        public string Content { get; set; }
    }
}
