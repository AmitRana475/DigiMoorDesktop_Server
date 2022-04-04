using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;
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
        public byte[] LOGO { get; set; }
        public byte[] Middle_RightImage { get; set; }
        public byte[] FooterImage { get; set; }
        public string FooterLine { get; set; }
    }
}
