using System;

namespace DataBuildingLayer
{
    public class WorkHoursNC
    {
        public int Id { get; set; }
        public int wid { get; set; }
        public string WRID { get; set; }
        public int vessel_ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime Dates { get; set; }
        public string NonConfirmity { get; set; }
        public object NCType { get; set; }
        public object NCTypeStatus { get; set; }
        public object Acknowledge { get; set; }
        public string UserType { get; set; }
        public string AckAdmin { get; set; }
        public string AckMaster { get; set; }
        public string AckHOD { get; set; }
        public string position { get; set; }
        public string Department { get; set; }
        public int ROW_NUM { get; set; }
        public bool IsCheckedV { get; set; }

    }
}
