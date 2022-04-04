using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("AdminLogin")]
    public class AdminLoginClass
    {
        [Key]
        public int Aid { get; set; }
        public string uname { get; set; }
        public string pswd { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Loginfo { get; set; }
        public string productinfo { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }

    }
}
