using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("RopeBasedCalculations")]
    public class RopeBasedCalculationsClass
    {
        [Key]
        public int Id { get; set; }
        public int RopeId { get; set; }
        public string CertificateNumber { get; set; }
        public string AssignedNumber { get; set; }
        public string Location { get; set; }
    }
}
