using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    public class OutputWindForceCoefficients
    {   
        [Key]
        public int Id { get; set; }
        public int PortId { get; set; }
        public string PortName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string Description1 { get; set; }
        public object Values { get; set; }
        public string Units { get; set; }
        public DateTime OutputDate { get; set; }
    }
}
