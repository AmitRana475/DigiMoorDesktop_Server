using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("CrewRank")]
    public class CrewRankClass
    {
        [Key]
        public int cid { get; set; }
        public int SrNo { get; set; }
        public string Rank { get; set; }
    }
}
