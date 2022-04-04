using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("certificate_order")]
    public class CertificateOrderClass
    {
        [Key]
        public int serial_ID { get; set; }
        public int certificate_ID { get; set; }
    }
}
