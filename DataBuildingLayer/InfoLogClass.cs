using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("InfoLog")]
    public class InfoLogClass
    {
        [Key]
        public int LogId { get; set; }
        [Display(Name = "Date / Time")]
        public DateTime dt { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string ModuleName { get; set; }
        public string ActionName { get; set; }
        public string Description { get; set; }

        [Display(Name = "Edit date")]
        public Nullable<DateTime> Editdate { get; set; }
    }
}
