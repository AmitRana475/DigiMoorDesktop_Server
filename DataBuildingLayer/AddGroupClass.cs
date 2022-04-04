using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBuildingLayer
{
    [Table("AddGroup")]
    public class AddGroupClass
    {
        [Key]
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupMember { get; set; }
        public string GRank { get; set; }
        public string GFullName { get; set; }
        public string GUser { get; set; }

     

       
    }
    public class AddGroupErrorMessages
    {
        public string GroupNameMessage { get; set; }
        public string GroupMemberMessage { get; set; }
      

    }


}
