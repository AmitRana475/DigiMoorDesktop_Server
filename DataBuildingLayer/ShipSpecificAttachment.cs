using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("tblShipSpecificAttachment")]
    public class ShipSpecificAttachment
    {

        [Key]
        public int Id { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public int MId { get; set; }
        public string ShipId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreateBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Type { get; set; }  // Ship or Shore [Shore Type from office Attachment]





    }
}
