using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WorkShipVersionII.WorkHoursViewModel.TimeSlotModels
{
    public interface ITimeSlotManager
    {
        void TimeSlotPageLoadMethod(WorkHoursClass obj, List<WorkHoursClass> WorkHoursList, CrewDetailClass CrewDetailList, Grid DynamicGrid, DefineRulesClass DefineRules, string Opa90Rule, List<InternationalClass> CheckIDL, DateTime? MaxFutureDate);
        Tuple<DateTime, string, string> CheckIDLBack(DateTime dates, string IDLs, List<InternationalClass> CheckIDL);
        Tuple<DateTime, string, string> CheckIDLNext(DateTime dates, string IDLs, List<InternationalClass> CheckIDL);
        int CheckRetard(string Dateid, DateTime Dt11, List<RetardtblClass> CheckRetar);
        void SaveMethod1(WorkHoursClass ob);
        void SaveMethod2(WorkHoursClass ob);
        void Update(WorkHoursClass ob);
        
        //void UpdateSqlCe(WorkHoursClass ob);
    }
}
