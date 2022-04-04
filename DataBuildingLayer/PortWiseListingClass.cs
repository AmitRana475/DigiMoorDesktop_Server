using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{

    public class PortWiseListingClass
    {
        [Key]

        public int Id { get; set; }

        public int PortId { get; set; }
        [NotMapped]
        public string PortName { get; set; }
        [NotMapped]
        public DateTime InputDate { get; set; }
    }
}
