using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("tblCommon")]
   public class TblCommonClass
    {

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
}
