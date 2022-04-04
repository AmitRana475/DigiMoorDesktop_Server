using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataBuildingLayer
{
    [Table("DefineRules")]
    public class DefineRulesClass
    {
        [Key]
        public int Id { get; set; }
        public decimal Rule6Hrs { get; set; } = 0;
        public decimal Rule10Hrs { get; set; } = 0;
        public bool Rule10DevideTwo { get; set; } = false;
        public bool Manila10DevideThree { get; set; } = false;
        public decimal Rule77Hrs { get; set; } = 0;
        public decimal Manila70Hrs { get; set; } = 0;


    }
}
