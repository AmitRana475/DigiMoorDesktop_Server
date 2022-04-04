using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
       [Table("DocsPages")]
       public class DocsPages
       {
              [Key]
        public int Id { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }
        public string ContentPath { get; set; }

        public int Mid { get; set; }
        public string ShipId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreateBy { get; set; }
     
        public string ModifiedBy { get; set; }

        [NotMapped]
        public string MenuTitle { get; set; }
        //[AllowHtml]

    }
}
