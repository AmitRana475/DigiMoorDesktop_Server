using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

namespace DataBuildingLayer
{
    [Table("ReportsMataData")]
    public class ReportsMataDataClass
    {
        [Key]
        public int Id { get; set; }
        public string Company { get; set; }
        public string ReportName { get; set; }
        public ImageSource LOGO { get; set; }
        public ImageSource Middle_RightImage { get; set; }
        public ImageSource FooterImage { get; set; }
        public string FooterLine { get; set; }
    }
}
