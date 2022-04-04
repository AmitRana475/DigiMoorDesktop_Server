using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("MooringWinchDetail")]
    public class MooringWinchClass
    {
        
        [Key]
        public int Id { get; set; }
        public string AssignedNumber { get; set; }
        public string Location { get; set; }
        public decimal? MBL { get; set; }
        public int? SortingOrder { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        public string Lead { get; set; }
    }
}
