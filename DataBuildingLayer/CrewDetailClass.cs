using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("CrewDetail")]
    public class CrewDetailClass
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public string UserName { get; set; }
        public string position { get; set; }
        public string department { get; set; }
        public DateTime ServiceFrom { get; set; }
        public DateTime ServiceTo { get; set; }
        public string CDC { get; set; }
        public string empno { get; set; }
        public string pswd { get; set; }

        [NotMapped]
        public string Confirmpswd { get; set; }
        public string comments { get; set; }
        public bool overtime { get; set; }
        public string SeaWk { get; set; }
        public string SeaNWK { get; set; }
        public string PortWk { get; set; }
        public string PortNWK { get; set; }
        public DateTime dates { get; set; }
        public decimal SeaWH { get; set; } = 0;
        public decimal portWH { get; set; } = 0;
        public int did { get; set; }
        public int rid { get; set; }
        public string SeaWk1 { get; set; }
        public string SeaNWK1 { get; set; }
        public string PortWk1 { get; set; }
        public string PortNWK1 { get; set; }
        public bool OpaStatus { get; set; }
        public bool CertificateView { get; set; }
        public bool CertificateAdd { get; set; }
        public bool CertificateEdit { get; set; }
        public bool CertificateDelete { get; set; }
        public DateTime DOB { get; set; }
        public bool chkyoungs { get; set; }
        public string SeaWkYoung { get; set; }
        public string SeaNWKYoung { get; set; }
        public string PortWkYoung { get; set; }
        public string PortNWKYoung { get; set; }
        public string Remarks { get; set; }
        public bool WatchKeeper { get; set; } = true;
        [NotMapped]
        public bool WatchKeeper1 { get; set; } = false;

        [NotMapped]
        public bool IsChecked { get; set; }

        [NotMapped]
        public string SeaNC { get; set; }
        [NotMapped]
        public string PortNC { get; set; }

        [NotMapped]
        public int Vessel_ID { get; set; }


        [NotMapped]
        public object DateAvailble { get; set; }

        [NotMapped]
        public string ActualorPlanner { get; set; }


    }
}
