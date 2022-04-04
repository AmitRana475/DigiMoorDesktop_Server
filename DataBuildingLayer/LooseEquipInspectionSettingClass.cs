using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("tblLooseEquipInspectionSetting")]
    public class LooseEquipInspectionSettingClass
    {
        [Key]
        public int Id { get; set; }
        public int EquipmentType { get; set; }
        public int InspectionFrequency { get; set; }
        public int MaximumRunningHours { get; set; }
        public int MaximumMonthsAllowed { get; set; }
       
    }
}
