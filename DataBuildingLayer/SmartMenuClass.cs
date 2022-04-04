using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBuildingLayer
{
    public class SmartMenuClass
    {
        public string deleted { get; set; }
        public string slug { get; set; }
        public string href { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public string id { get; set; }
        //public string deleted { get; set; }
        //public string deleted { get; set; }
        //public string deleted { get; set; }
    }

    public class Child2
    {
        public int deleted { get; set; }
        public int @new { get; set; }
        public int slug { get; set; }
        public string href { get; set; }
        public int type { get; set; }
        public string text { get; set; }
        public int id { get; set; }
    }

    public class Child
    {
        public int deleted { get; set; }
        public int @new { get; set; }
        public int slug { get; set; }
        public string href { get; set; }
        public int type { get; set; }
        public string text { get; set; }
        public int id { get; set; }
        public List<Child2> children { get; set; }
    }

    public class RootObject
    {
        public int deleted { get; set; }
        public int @new { get; set; }
        public int slug { get; set; }
        public string href { get; set; }
        public int type { get; set; }
        public string text { get; set; }
        public int id { get; set; }
        public List<Child> children { get; set; }
    }
}
