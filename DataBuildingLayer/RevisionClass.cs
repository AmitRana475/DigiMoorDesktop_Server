using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("Revision")]
    public class RevisionClass
    {
        [Key]
        public int Id { get; set; }
        public string RPrefix { get; set; }
        public decimal? RNumber { get; set; }
        public DateTime? ReviseDate { get; set; }
        public DateTime? ApproveDate { get; set; }      
        public string CreateBy { get; set; }
        public string ApprovedBy { get; set; }
        public int Mid { get; set; }
        public string RevisionType { get; set; }

        public string Content { get; set; }
        public string ContentPath { get; set; }
        public string Status { get; set; }
        public string RevisionText { get; set; }

        [NotMapped]
        public int RowId { get; set; }
    }
}
