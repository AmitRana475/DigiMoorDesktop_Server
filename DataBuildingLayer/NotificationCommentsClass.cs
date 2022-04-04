using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("NotificationComment")]
   public class NotificationCommentsClass
    {
        [Key]
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public int CommentsType { get; set; }
        public string Comments { get; set; }      
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }       
        public bool IsActive { get; set; }

        public bool IsRead { get; set; }
    }
}
