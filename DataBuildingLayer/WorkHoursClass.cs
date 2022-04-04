using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;

namespace DataBuildingLayer
{
    [Table("WorkHours")]
    public class WorkHoursClass
    {
        [Key]
        public int wid { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public decimal TotalHours { get; set; } = 0;
        public decimal RestHours { get; set; } = 48;
        public string options { get; set; } = "At Sea";
        public string Remarks { get; set; }
        public DateTime dates { get; set; }
        public string NonConfirmities { get; set; }
        public string hrs { get; set; }
        public string Colors { get; set; }

        //public string RemarksPlanner { get; set; }
        //public string NonConfirmitiesPlanner { get; set; }
        //public string hrsPlanner { get; set; }
        //public string ColorsPlanner { get; set; }

        public string ColorName { get; set; }
        public string Department { get; set; }
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }
        public string Col5 { get; set; }
        public string Col6 { get; set; }
        public string Col7 { get; set; }
        public string Col8 { get; set; }
        public string Col9 { get; set; }
        public string Col10 { get; set; }
        public string Col11 { get; set; }
        public string Col12 { get; set; }
        public string Col13 { get; set; }
        public string Col14 { get; set; }
        public string Col15 { get; set; }
        public string Col16 { get; set; }
        public string Col17 { get; set; }
        public string Col18 { get; set; }
        public string Col19 { get; set; }
        public string Col20 { get; set; }
        public string Col21 { get; set; }
        public string Col22 { get; set; }
        public string Col23 { get; set; }
        public string Col24 { get; set; }
        public string Col25 { get; set; }
        public string Col26 { get; set; }
        public string Col27 { get; set; }
        public string Col28 { get; set; }
        public string Col29 { get; set; }
        public string Col30 { get; set; }
        public string Col31 { get; set; }
        public string Col32 { get; set; }
        public string Col33 { get; set; }
        public string Col34 { get; set; }
        public string Col35 { get; set; }
        public string Col36 { get; set; }
        public string Col37 { get; set; }
        public string Col38 { get; set; }
        public string Col39 { get; set; }
        public string Col40 { get; set; }
        public string Col41 { get; set; }
        public string Col42 { get; set; }
        public string Col43 { get; set; }
        public string Col44 { get; set; }
        public string Col45 { get; set; }
        public string Col46 { get; set; }
        public string Col47 { get; set; }
        public string Col48 { get; set; }
        public string Col49 { get; set; }
        public string Col50 { get; set; }
        public string Col51 { get; set; }
        public string Col52 { get; set; }
        public string Col53 { get; set; }
        public string Col54 { get; set; }
        public string Col55 { get; set; }
        public string Col56 { get; set; }
        public string Col57 { get; set; }
        public string Col58 { get; set; }
        public string Col59 { get; set; }
        public string Col60 { get; set; }
        public string Col61 { get; set; }
        public string Col62 { get; set; }
        public string Col63 { get; set; }
        public string Col64 { get; set; }
        public string Col65 { get; set; }
        public string Col66 { get; set; }
        public string Col67 { get; set; }
        public string Col68 { get; set; }
        public string Col69 { get; set; }
        public string Col70 { get; set; }
        public string Col71 { get; set; }
        public string Col72 { get; set; }
        public string Col73 { get; set; }
        public string Col74 { get; set; }
        public string Col75 { get; set; }
        public string Col76 { get; set; }
        public string Col77 { get; set; }
        public string Col78 { get; set; }
        public string Col79 { get; set; }
        public string Col80 { get; set; }
        public string Col81 { get; set; }
        public string Col82 { get; set; }
        public string Col83 { get; set; }
        public string Col84 { get; set; }
        public string Col85 { get; set; }
        public string Col86 { get; set; }
        public string Col87 { get; set; }
        public string Col88 { get; set; }
        public string Col89 { get; set; }
        public string Col90 { get; set; }
        public string Col91 { get; set; }
        public string Col92 { get; set; }
        public string Col93 { get; set; }
        public string Col94 { get; set; }
        public string Col95 { get; set; }
        public string Col96 { get; set; }
        public string Col97 { get; set; }
        public string Col98 { get; set; }
        public string Col99 { get; set; }
        public string Col100 { get; set; }
        public string Col101 { get; set; }
        public string Col102 { get; set; }
        public string Col103 { get; set; }
        public string Col104 { get; set; }
        public string Col105 { get; set; }
        public string Col106 { get; set; }
        public string Col107 { get; set; }
        public string Col108 { get; set; }
        public string Col109 { get; set; }
        public string Col110 { get; set; }
        public string Col111 { get; set; }
        public string Col112 { get; set; }
        public string Col113 { get; set; }
        public string Col114 { get; set; }
        public string Col115 { get; set; }
        public string Col116 { get; set; }
        public string Col117 { get; set; }
        public string Col118 { get; set; }
        public string Col119 { get; set; }
        public string Col120 { get; set; }

        public string ColNA { get; set; }//not using
        public string MonthName { get; set; }
        public string YearValue { get; set; }
        public int cid { get; set; } //not using
        public int Normal_WKH { get; set; }
        public string DaysOfMonth { get; set; }
        public decimal overtime { get; set; }
        public bool StatusCrew { get; set; }
        public bool CrewNC { get; set; }
        public bool MasterCrewNC { get; set; }
        public bool HODCrewNC { get; set; }
        public DateTime datetimes { get; set; }
        public string UserType { get; set; }
        public bool opa { get; set; }
        public decimal RestHour7day { get; set; } = 168;
        public decimal NormalOT1 { get; set; }
        public string RuleNames { get; set; }
        public string NC1 { get; set; }
        public DateTime? NCD1 { get; set; }
        public string NC2 { get; set; }
        public DateTime? NCD2 { get; set; }
        public string NC3 { get; set; }
        public DateTime? NCD3 { get; set; }
        public string NC4 { get; set; }
        public DateTime? NCD4 { get; set; }
        public string NC5 { get; set; }
        public DateTime? NCD5 { get; set; }
        public string NC6 { get; set; }
        public DateTime? NCD6 { get; set; }
        public string NC7 { get; set; }
        public DateTime? NCD7 { get; set; }
        public string NC8 { get; set; }
        public DateTime? NCD8 { get; set; }
        public string NC9 { get; set; }
        public DateTime? NCD9 { get; set; }
        public string NC10 { get; set; }
        public DateTime? NCD10 { get; set; }
        public string NC11 { get; set; }
        public DateTime? NCD11 { get; set; }
        public string NC12 { get; set; }
        public DateTime? NCD12 { get; set; }
        public string NC13 { get; set; }
        public DateTime? NCD13 { get; set; }
        public string NC14 { get; set; }
        public DateTime? NCD14 { get; set; }
        public string NC15 { get; set; }
        public DateTime? NCD15 { get; set; }
        public string NC16 { get; set; }
        public DateTime? NCD16 { get; set; }
        public string NC17 { get; set; }
        public DateTime? NCD17 { get; set; }
        public string NC18 { get; set; }
        public DateTime? NCD18 { get; set; }
        public string YNC1 { get; set; }
        public DateTime? YNCD1 { get; set; }
        public string YNC2 { get; set; }
        public DateTime? YNCD2 { get; set; }
        public string YNC3 { get; set; }
        public DateTime? YNCD3 { get; set; }
        public string YNC4 { get; set; }
        public DateTime? YNCD4 { get; set; }
        public string YNC5 { get; set; }
        public DateTime? YNCD5 { get; set; }
        public string YNC6 { get; set; }
        public DateTime? YNCD6 { get; set; }
        public string YNC7 { get; set; }
        public DateTime? YNCD7 { get; set; }
        public string YNC8 { get; set; }
        public DateTime? YNCD8 { get; set; }

        public string RestHourAny24 { get; set; }
        public string RestHourAny7day { get; set; }
        public string options1 { get; set; }
        public bool opayoung { get; set; }
        public DateTime? MNCCD1 { get; set; }
        public DateTime? MNCCD2 { get; set; }
        public DateTime? MNCCD3 { get; set; }
        public DateTime? MNCCD4 { get; set; }
        public DateTime? MNCCD5 { get; set; }
        public DateTime? MNCCD6 { get; set; }
        public DateTime? MNCCD7 { get; set; }
        public DateTime? MNCCD8 { get; set; }
        public DateTime? MNCCD9 { get; set; }
        public DateTime? MNCCD10 { get; set; }
        public DateTime? MNCCD11 { get; set; }
        public DateTime? MNCCD12 { get; set; }
        public DateTime? MNCCD13 { get; set; }
        public DateTime? MNCCD14 { get; set; }
        public DateTime? MNCCD15 { get; set; }
        public DateTime? MNCCD16 { get; set; }
        public DateTime? MNCCD17 { get; set; }
        public DateTime? MNCCD18 { get; set; }
        public DateTime? MYNCCD1 { get; set; }
        public DateTime? MYNCCD2 { get; set; }
        public DateTime? MYNCCD3 { get; set; }
        public DateTime? MYNCCD4 { get; set; }
        public DateTime? MYNCCD5 { get; set; }
        public DateTime? MYNCCD6 { get; set; }
        public DateTime? MYNCCD7 { get; set; }
        public DateTime? MYNCCD8 { get; set; }
        public DateTime? Last_update { get; set; }
        public bool AdminAlarm { get; set; }
        public bool MasterAlarm { get; set; }
        public bool HODAlarm { get; set; }
        public DateTime? HNCCD1 { get; set; }
        public DateTime? HNCCD2 { get; set; }
        public DateTime? HNCCD3 { get; set; }
        public DateTime? HNCCD4 { get; set; }
        public DateTime? HNCCD5 { get; set; }
        public DateTime? HNCCD6 { get; set; }
        public DateTime? HNCCD7 { get; set; }
        public DateTime? HNCCD8 { get; set; }
        public DateTime? HNCCD9 { get; set; }
        public DateTime? HNCCD10 { get; set; }
        public DateTime? HNCCD11 { get; set; }
        public DateTime? HNCCD12 { get; set; }
        public DateTime? HNCCD13 { get; set; }
        public DateTime? HNCCD14 { get; set; }
        public DateTime? HNCCD15 { get; set; }
        public DateTime? HNCCD16 { get; set; }
        public DateTime? HNCCD17 { get; set; }
        public DateTime? HNCCD18 { get; set; }
        public DateTime? HYNCCD1 { get; set; }
        public DateTime? HYNCCD2 { get; set; }
        public DateTime? HYNCCD3 { get; set; }
        public DateTime? HYNCCD4 { get; set; }
        public DateTime? HYNCCD5 { get; set; }
        public DateTime? HYNCCD6 { get; set; }
        public DateTime? HYNCCD7 { get; set; }
        public DateTime? HYNCCD8 { get; set; }
        public string WRID { get; set; }
        public string Days { get; set; }
        public int Cellcount { get; set; }
        public DateTime? UnFreezeDates { get; set; }
        public string NonConfirmities1 { get; set; }

        [NotMapped]
        public Grid DynamicGrid1 { get; set; }
        [NotMapped]
        public Grid TimeSlotGrid1 { get; set; }

        [NotMapped]
        public Grid DynamicGridPlanner1 { get; set; }
        [NotMapped]
        public Grid TimeSlotGridPlanner1 { get; set; }

        [NotMapped]
        public string IDL { get; set; }

        [NotMapped]
        public string Planner { get; set; }

        //[NotMapped]
        //public IQueryable<CrewDetailClass> CrewDetailList { get; set; }
        //[NotMapped]
        //public IQueryable<InternationalClass> InternationalList { get; set; }


    }
}
