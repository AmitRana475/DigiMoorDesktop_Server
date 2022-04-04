using System;

namespace DataBuildingLayer
{
    public class CertificateNC
    {
        public int Cid { get; set; }
        public int Id { get; set; }
        public string CertificateName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime SurveyDate { get; set; }
        public object AlertFrequencyType { get; set; }
        public object Acknowledge { get; set; }
        public string UserType { get; set; }
        public string NCType { get; set; }
        public bool IsCheckedV { get; set; }


        //public int id { get; set; }
        public int Vessel_ID { get; set; }
        public string CName { get; set; }
        public DateTime DOI { get; set; }
        public DateTime DOS { get; set; }
        public DateTime DOE { get; set; }
        public string AlertFrequency { get; set; }
        public string AdminAck { get; set; }
        public string MasterAck { get; set; }
        public string HODAck { get; set; }



    }
}
