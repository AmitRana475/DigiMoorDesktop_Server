using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    [Table("TblMSMPMenus")]
    public class MSMPMenus
    {
        public MSMPMenus()
        {
            Menus1 = new List<SubMenu>();
        }
        [Key]
        public int MId { get; set; }
        public string MenuName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string AreaName { get; set; }
        public string Role { get; set; }
        public List<SubMenu> Menus1 { get; set; }


    }

    [Table("TblSubMenu")]
    public class SubMenu
    {
        [Key]
        public int SubId { get; set; }
        public int MId { get; set; }
        public string SubMenuName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string AreaName { get; set; }
        public string Role { get; set; }
    }

    [Table("tblsmartmenus")]
    public class SmartMenu
    {
        [Key]
        public int Id { get; set; }       
        public string SmartMenuContent { get; set; }
        public string HtmlContent { get; set; }
      
    }
}
