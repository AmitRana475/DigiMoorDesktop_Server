using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("UserAccess")]
    public class UserAccessClass
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public bool CrewManagement { get; set; } = true;
        public bool CrewDetail { get; set; } = true;
        public bool CrewRank { get; set; }
        public bool Department { get; set; }
        public bool HolidayGroup { get; set; }
        public bool HOD { get; set; }
        public bool ResetPassword { get; set; } = true;
        public bool ResetPasswordAll { get; set; }
        public bool FreezeUnfreeze { get; set; } = true;
        public bool FreezeUnfreezeAll { get; set; }
        public bool Report { get; set; } = true;
        public bool OverView { get; set; } = true;
        public bool OverViewAll { get; set; }
        public bool OverTime { get; set; } = true;
        public bool CrewWorkHours { get; set; } = true;
        public bool NonConfirmity { get; set; } = true;
        public bool WorkSchedule { get; set; } = true;
        public bool RestHours { get; set; } = true;
        public bool WorkandResthour { get; set; } = true;
        public bool WorkandRestHoursAll { get; set; }
        public bool Administration { get; set; } = true;
        public bool ImportExport { get; set; }
        public bool BackupRestore { get; set; }
        public bool ApplicationLog { get; set; }
        public bool Rules { get; set; }
        public bool MainCertificate { get; set; }
        public bool Certificate { get; set; }
        public bool Lincenc { get; set; }
        public bool Notification { get; set; } = true;
        public bool NCNotification { get; set; } = true;
        public bool CerNotification { get; set; }
        public bool OCNotification { get; set; }
        public bool NonConformityAll { get; set; }
        public bool CrewDetailAll { get; set; }
        public bool CertificateAdd { get; set; }
        public bool CertificateEdit { get; set; }
        public bool CertificateDelete { get; set; }
        public bool ErrorLog { get; set; }
        public bool ManilaRules { get; set; } = true;
        public bool GroupPlanning { get; set; }
        public bool SelectAll { get; set; }
        public string HODName { get; set; }
        public string DepartmentName { get; set; }
        
        [NotMapped]
        public string Departments { get; set; }
       


    }
    
}
