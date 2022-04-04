using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("ResidualLabTest")]
   public class ResidualLabTestClass
    {
        [Key]
        public int Id { get; set; }       
        public int? RopeId { get; set; }
        public int? RopeTypeId { get; set; }  
        public int ManufacturerId { get; set; }
        public string Location { get; set; }
       // public DateTime? ServiceTestDate { get; set; }

              public decimal? ServiceTestDate { get; set; }
              //public int? RunningHours { get; set; }

              public decimal? RunningHours { get; set; }
        public DateTime? LabTestDate { get; set; }
        public decimal? TestResults { get; set; }
        public string Remarks { get; set; }
        public int? RopeTail { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }

             // public decimal? MonthInServiceDt { get; set; }

              [NotMapped]
        public string CertificateNumber { get; set; }
        [NotMapped]
        public string UniqueId { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string RopeType { get; set; }

        [NotMapped]
        public int RowId { get; set; }
        [NotMapped]
        public string LabTestDate1 { get; set; }
        [NotMapped]
        public string AttachmentPath { get; set; }

        [NotMapped]
        public string AttachmentVisibility { get; set; }
    }
}
