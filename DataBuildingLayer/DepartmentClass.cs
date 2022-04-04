using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("Department")]
    public class DepartmentClass
    {
        [Key]
        public int did { get; set; }
        public string DeptName { get; set; }
    }
}
