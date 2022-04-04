using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("InputGeneralParticulars")]
    public class InputGeneralParticulars
    {
        [Key]
        public int Id { get; set; }
        public int PortId { get; set; }
        public string PortName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MainValue { get; set; }
        [NotMapped]
        public object MainValue1 { get; set; }
        public Nullable<decimal> DefaultValue { get; set; }
        public string Units { get; set; }
        
        public DateTime InputDate { get; set; }
        public int OldInputId { get; set; }

    }
}
