using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
       [Table("Notifications")]
       public class NotificationsClass
       {
              [Key]
              public int Id { get; set; }
              public string Notification { get; set; }
              public int NotificationType { get; set; }
              public string ShipActionTaken { get; set; }
              public bool Acknowledge { get; set; }
              public string AckRecord { get; set; }
              //public string NotificationLogic { get; set; }  

              public DateTime? NotificationDueDate { get; set; }
              public DateTime? CreatedDate { get; set; }
              public string CreatedBy { get; set; }
              //public DateTime? ModifiedDate { get; set; }
              //public string ModifiedBy { get; set; }
              public bool IsActive { get; set; }

              public int? RopeId { get; set; }
              public int NotificationAlertType { get; set; }
              public string LooseCertificateNum { get; set; }

              [NotMapped]
              public bool IsCheckedV { get; set; }
              [NotMapped]
              public object NCTypeStatus { get; set; }

              [NotMapped]
              public int IndexId { get; set; }

              [NotMapped]
              public string NotificationDate { get; set; }

        [NotMapped]
        public string OfficeCmnt { get; set; }
        [NotMapped]
        public string ShipCmnt { get; set; }

        public int LooseEqType { get; set; }

        [NotMapped]
        public int RowId { get; set; }

        public int? MOP_Id { get; set; }
    }
}
