using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("certificates")]
    public class CertificatesClass
    {
        [Key]
        public int Id { get; set; }
        public int SrNo { get; set; }
        public string CName { get; set; }
        public DateTime DOI { get; set; }
        public DateTime DOE { get; set; }
        public DateTime DOS { get; set; }
        public string Remarks { get; set; }
        public string Acknowledgements { get; set; } = "To be Acknowledged";
        public bool Status { get; set; }
        public DateTime? AcknowledgDate { get; set; } 
        public bool month6 { get; set; }
        public string AckDOE6m { get; set; }
        public string NCDOE6m { get; set; }
        public bool month3 { get; set; }
        public string AckDOE3m { get; set; }
        public string NCDOE3m { get; set; }
        public bool month1 { get; set; }
        public string AckDOE1m { get; set; }
        public string NCDOE1m { get; set; }
        public bool Days15 { get; set; }
        public string AckDOE15d { get; set; }
        public string NCDOE15d { get; set; }
        public bool Days7 { get; set; }
        public string AckDOE7d { get; set; }
        public string NCDOE7d { get; set; }
        public bool Days1 { get; set; } = true;
        public string AckDOE1d { get; set; }
        public string NCDOE1d { get; set; }
        public bool DOSmonth6 { get; set; }
        public string AckDOS6m { get; set; }
        public string NCDOS6m { get; set; }
        public bool DOSmonth3 { get; set; }
        public string AckDOS3m { get; set; }
        public string NCDOS3m { get; set; }
        public bool DOSmonth1 { get; set; }
        public string AckDOS1m { get; set; }
        public string NCDOS1m { get; set; }
        public bool DOSDays15 { get; set; }
        public string AckDOS15d { get; set; }
        public string NCDOS15d { get; set; }
        public bool DOSDays7 { get; set; }
        public string AckDOS7d { get; set; }
        public string NCDOS7d { get; set; }
        public bool DOSDays1 { get; set; } = true;
        public string AckDOS1d { get; set; }
        public string NCDOS1d { get; set; }
        public string OverDOE { get; set; }
        public string OverDOS { get; set; }
        public string AckDOEOver { get; set; }
        public string AckDOSOver { get; set; }
        public bool AlarmNCDOE6m { get; set; }
        public bool AlarmNCDOE3m { get; set; }
        public bool AlarmNCDOE1m { get; set; }
        public bool AlarmNCDOE15d { get; set; }
        public bool AlarmNCDOE7d { get; set; }
        public bool AlarmNCDOE1d { get; set; }
        public bool AlarmNCDOEXXX { get; set; }
        public bool AlarmNCDOS6m { get; set; }
        public bool AlarmNCDOS3m { get; set; }
        public bool AlarmNCDOS1m { get; set; }
        public bool AlarmNCDOS15d { get; set; }
        public bool AlarmNCDOS7d { get; set; }
        public bool AlarmNCDOS1d { get; set; }
        public bool AlarmNCDOSXXX { get; set; }
        public string AckDOE6m_MASTER { get; set; }
        public string AckDOE3m_MASTER { get; set; }
        public string AckDOE1m_MASTER { get; set; }
        public string AckDOE15d_MASTER { get; set; }
        public string AckDOE7d_MASTER { get; set; }
        public string AckDOE1d_MASTER { get; set; }
        public string AckDOEOver_MASTER { get; set; }
        public string AckDOS6m_MASTER { get; set; }
        public string AckDOS3m_MASTER { get; set; }
        public string AckDOS1m_MASTER { get; set; }
        public string AckDOS15d_MASTER { get; set; }
        public string AckDOS7d_MASTER { get; set; }
        public string AckDOS1d_MASTER { get; set; }
        public string AckDOSOver_MASTER { get; set; }
        public string AckDOE6m_HOD { get; set; }
        public string AckDOE3m_HOD { get; set; }
        public string AckDOE1m_HOD { get; set; }
        public string AckDOE15d_HOD { get; set; }
        public string AckDOE7d_HOD { get; set; }
        public string AckDOE1d_HOD { get; set; }
        public string AckDOEOver_HOD { get; set; }
        public string AckDOS6m_HOD { get; set; }
        public string AckDOS3m_HOD { get; set; }
        public string AckDOS1m_HOD { get; set; }
        public string AckDOS15d_HOD { get; set; }
        public string AckDOS7d_HOD { get; set; }
        public string AckDOS1d_HOD { get; set; }
        public string AckDOSOver_HOD { get; set; }
        public bool AlarmNCDOE6m_MASTER { get; set; }
        public bool AlarmNCDOE3m_MASTER { get; set; }
        public bool AlarmNCDOE1m_MASTER { get; set; }
        public bool AlarmNCDOE15d_MASTER { get; set; }
        public bool AlarmNCDOE7d_MASTER { get; set; }
        public bool AlarmNCDOE1d_MASTER { get; set; }
        public bool AlarmNCDOEXXX_MASTER { get; set; }
        public bool AlarmNCDOS6m_MASTER { get; set; }
        public bool AlarmNCDOS3m_MASTER { get; set; }
        public bool AlarmNCDOS1m_MASTER { get; set; }
        public bool AlarmNCDOS15d_MASTER { get; set; }
        public bool AlarmNCDOS7d_MASTER { get; set; }
        public bool AlarmNCDOS1d_MASTER { get; set; }
        public bool AlarmNCDOSXXX_MASTER { get; set; }
        public bool AlarmNCDOE6m_HOD { get; set; }
        public bool AlarmNCDOE3m_HOD { get; set; }
        public bool AlarmNCDOE1m_HOD { get; set; }
        public bool AlarmNCDOE15d_HOD { get; set; }
        public bool AlarmNCDOE7d_HOD { get; set; }
        public bool AlarmNCDOE1d_HOD { get; set; }
        public bool AlarmNCDOEXXX_HOD { get; set; }
        public bool AlarmNCDOS6m_HOD { get; set; }
        public bool AlarmNCDOS3m_HOD { get; set; }
        public bool AlarmNCDOS1m_HOD { get; set; }
        public bool AlarmNCDOS15d_HOD { get; set; }
        public bool AlarmNCDOS7d_HOD { get; set; }
        public bool AlarmNCDOS1d_HOD { get; set; }
        public bool AlarmNCDOSXXX_HOD { get; set; }
        public bool ExpiryStatus { get; set; }

    }
}
