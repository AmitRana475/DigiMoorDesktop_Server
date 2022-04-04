using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("AssignLooseEquipToWinch")]
  public class AssignLooseEquipTypeClass
    {
        [Key]
        public int Id { get; set; }
        public int LooseETypeId { get; set; }      
      
        public int AssignWinchId { get; set; }
        public string AssignedLocation { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public string AssignedNumber { get; set; }
        [NotMapped]
        public string Location { get; set; }
        [NotMapped]

        public string Looseequipmenttype { get; set; }
        [NotMapped]
        public string CreatedDate1 { get; set; }
    }
}
