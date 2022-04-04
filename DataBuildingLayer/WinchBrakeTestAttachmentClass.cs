using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
       [Table("WinchBrakeTestAttachment")]
       public class WinchBrakeTestAttachmentClass
       {
              [Key]
              public int Id { get; set; }
              public string AttachmentName { get; set; }
              public string AttachmentPath { get; set; }
              public DateTime? CreatedDate { get; set; }

              [NotMapped]
              public int RowId { get; set; }
       }
}
