using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("comment_ofVS")]
    public class CommentofVSClass
    {
        [Key]
    
        public int Comnt_ID { get; set; }
        public int Vessel_ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        public string DepartmentName { get; set; }
        public DateTime NC_Date { get; set; }
        public string Office_Comment { get; set; }
        public DateTime Comment_Date { get; set; }
        public string Acknowledge_Status { get; set; }
        public bool AdminAlarm { get; set; }
        public bool MasterAlarm { get; set; }
        public bool HODAlarm { get; set; }
        public string Acknowledge_Status_MASTER { get; set; }
        public string Acknowledge_Status_HOD { get; set; }

        [NotMapped]
        public bool IsCheckedV { get; set; }
        [NotMapped]
        public int Id { get; set; }


    }
}
