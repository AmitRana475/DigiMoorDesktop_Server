using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("version_tbl")]
    public class versionClass
    {
        [Key]
        public int vids { get; set; }
        public string versions { get; set; }
        public string ClientVersions { get; set; }

        public string SubVersions { get; set; }
    }
}
