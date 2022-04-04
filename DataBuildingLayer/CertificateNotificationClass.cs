using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("CertificateNotification")]
    public class CertificateNotificationClass
    {
        [Key]
        public int Id { get; set; }
        public string CName { get; set; }
        public DateTime DOI { get; set; }
        public DateTime DOE { get; set; }
        public DateTime DOS { get; set; }
        public string AlertFrequency { get; set; }
        public string AdminAck { get; set; }
        public string MasterAck { get; set; }
        public string HODAck { get; set; }
    }
}
