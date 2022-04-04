using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("MooringRopeInspection")]
    public class MooringRopeInspectionClass
    {

        [Key]
        public int Id { get; set; }
        public string InspectBy { get; set; }
        public DateTime? InspectDate { get; set; }

        public int RopeId { get; set; }
        public int WinchId { get; set; }


      

        public int ExternalRating_A { get; set; }
        public int InternalRating_A { get; set; }
        public int AverageRating_A { get; set; }
        public decimal? LengthOFAbrasion_A { get; set; }
        public decimal? DistanceOutboard_A { get; set; }
        public decimal? CutYarnCount_A { get; set; }
        public decimal? LengthOFGlazing_A { get; set; }
        public int? RopeTail  { get; set; }
        public int ExternalRating_B { get; set; }
        public int InternalRating_B { get; set; }
        public int AverageRating_B { get; set; }
        public decimal? LengthOFAbrasion_B { get; set; }
        public decimal? DistanceOutboard_B { get; set; }
        public decimal? CutYarnCount_B { get; set; }
        public decimal? LengthOFGlazing_B { get; set; }

        public string Chafe_guard_condition { get; set; }

        public int Twist { get; set; }

        //public string Photo1 { get; set; }
        //public string Photo2 { get; set; }

        public byte[] Photo1 { get; set; }
        public byte[] Photo2 { get; set; }

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public int? NotificationId { get; set; }
        public int? InspectionId { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public string AssignNumber { get; set; }
        [NotMapped]
        public string Location { get; set; }
        [NotMapped]
        public string RpoeType { get; set; }
        [NotMapped]
        public string Certi_No { get; set; }

        [NotMapped]
        public string UniqueId { get; set; }

        [NotMapped]
        public bool IsCheckedV { get; set; }

        [NotMapped]
        public string InspectDate1 { get; set; }

        [NotMapped]
        public string Photo11 { get; set; }
        [NotMapped]
        public string Photo12 { get; set; }

        [NotMapped]
        public string Showbutton1 { get; set; }
        [NotMapped]
        public string Showbutton2 { get; set; }

        [NotMapped]
        public string IsEnable { get; set; }

        [NotMapped]
        public string ButtonContent1 { get; set; } = "Browse";

        [NotMapped]
        public string ButtonContent2 { get; set; } = "Browse";


    }
}
