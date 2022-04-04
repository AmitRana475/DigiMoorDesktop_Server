using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorkShipVersionII.WorkHoursViewModel.TimeSlotModels
{
   public interface ITimeSlotManagerPlanner
    {
        void TimeSlotPageLoadMethod(WorkHoursPlannerClass obj, List<WorkHoursClass> WorkHoursList, CrewDetailClass CrewDetailList, Grid DynamicGrid, DefineRulesClass DefineRules, string Opa90Rule, List<InternationalClass> CheckIDL, DateTime? MaxFutureDate);
        Tuple<DateTime, string, string> CheckIDLBack(DateTime dates, string IDLs, List<InternationalClass> CheckIDL);
        Tuple<DateTime, string, string> CheckIDLNext(DateTime dates, string IDLs, List<InternationalClass> CheckIDL);
        int CheckRetard(string Dateid, DateTime Dt11, List<RetardtblClass> CheckRetar);
        void SaveMethod1Planner(WorkHoursPlannerClass ob);
        void SaveMethod2Planner(WorkHoursPlannerClass ob);
        void UpdatePlanner(WorkHoursPlannerClass ob);
    }
}
