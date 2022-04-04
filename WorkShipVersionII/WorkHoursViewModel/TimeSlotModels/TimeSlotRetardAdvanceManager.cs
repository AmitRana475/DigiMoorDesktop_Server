using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace WorkShipVersionII.WorkHoursViewModel.TimeSlotModels
{
    public class TimeSlotRetardAdvanceManager : CommanConditionsTimeSlot, ITimeSlotSettingManager
    {
        private ShipmentContaxt sc;

        public TimeSlotRetardAdvanceManager()
        {
            if (sc == null)
                sc = new ShipmentContaxt();

        }


        public void TimeSlotPageSettingDeleteMethod(DateTime startdate, object enddate, BackgroundWorker Workersave, Grid DynamicGrid)
        {
            int ib = 2;
            Workersave.ReportProgress(ib);

            try
            {



                string monthname = Convert.ToDateTime(startdate).ToString("MMMM");
                string yearname = Convert.ToDateTime(startdate).ToString("yyyy");

                List<WorkHoursClass> WorkhoursListmain;
                List<WorkHoursPlannerClass> WorkhoursListmainPlanner;
                List<CrewDetailClass> CrewList;
                List<RetardtblClass> CheckRetar;
                using (ShipmentContaxt sc1 = new ShipmentContaxt())
                {
                    WorkhoursListmainPlanner = sc1.WorkHourPlanners.ToList();
                    WorkhoursListmain = sc1.WorkHourss.ToList();
                    CrewList = sc1.CrewDetails.ToList();
                    CheckRetar = sc1.Retardtbls.ToList();
                }



                //......Planner.....

                var WorkhoursListPlanner = WorkhoursListmainPlanner.Where(x => x.dates.Month == startdate.Month && x.dates.Year == startdate.Year).ToList();
                var workhoursUsersPlanner = WorkhoursListmainPlanner.Where(x => x.dates.Month == startdate.Month && x.dates.Year == startdate.Year).Select(x => x.UserName).Distinct().ToList();

                int con1 = workhoursUsersPlanner.Count();
                con1 = (con1 * 14);

                //......End Planner.....

                var WorkhoursList = WorkhoursListmain.Where(x => x.dates.Month == startdate.Month && x.dates.Year == startdate.Year).ToList();
                var workhoursUsers = WorkhoursListmain.Where(x => x.dates.Month == startdate.Month && x.dates.Year == startdate.Year).Select(x => x.UserName).Distinct().ToList();

                int con = workhoursUsers.Count();
                //con = con > 4 ? con - 4 : con;
                con = (con * 14);
                con = con + con1;

                foreach (var li1 in workhoursUsers)
                {
                    //ib = ib + 1;
                    //Workersave.ReportProgress(ib);

                    var obj1 = WorkhoursList.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Month == startdate.Month && x.dates.Year == startdate.Year).ToList();
                    if (obj1 != null)
                    {
                        var obj = WorkhoursList.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Date == startdate.Date && x.WRID == enddate.ToString()).ToList();
                        foreach (var ob in obj)
                        {

                            var local = sc.Set<WorkHoursClass>().Local.FirstOrDefault(c => c.wid == ob.wid);
                            if (local != null)
                            {
                                sc.Entry(local).State = EntityState.Detached;
                            }


                            sc.Entry(ob).State = EntityState.Deleted;
                            sc.SaveChanges();


                        }

                        var user = WorkhoursList.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Date == startdate.Date).FirstOrDefault();

                        if (user == null)
                            user = WorkhoursList.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Month == startdate.Month).FirstOrDefault();

                        var checkuser1 = CrewList.Where(x => x.UserName == li1).FirstOrDefault();
                        if (checkuser1 != null)
                        {
                            bool chkyoungs = Convert.ToBoolean(checkuser1.chkyoungs);
                            if (chkyoungs == true)
                            {
                                user.WRID = enddate.ToString();
                                int Cellcount = CheckRetardYoung(user.WRID, user.dates, CheckRetar);

                                string label42 = string.Empty;
                                for (int i = 0; i < Cellcount; i++)
                                {

                                    label42 += "0" + ",";
                                }

                                user.Cellcount = Cellcount;
                                user.hrs = label42;
                                user.dates = startdate.Date;



                                sc.WorkHourss.Add(user);
                                sc.SaveChanges();


                                ITimeSlotManager WorkhoursInstanse;

                                if (RuleName == "Normal")
                                    WorkhoursInstanse = new NormalTimeSlotYoungManager();
                                else
                                    WorkhoursInstanse = new ManilaTimeSlotYoungManager();






                                string username = user.UserName;
                                DateTime dt11 = startdate;
                                string wrid = enddate.ToString();

                                var CrewDetailList1 = CrewDetailList(user);


                                WorkHoursList1 = WorkHoursList(user);
                                var MaxFutureDate1 = MaxFutureDate;

                                user = WorkHoursList1.Where(x => x.UserName.Equals(username) && x.dates.Date == startdate.Date && x.WRID == enddate.ToString()).FirstOrDefault();

                                int wid = user.wid;

                                WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);

                                user.wid = wid;
                                WorkhoursInstanse.Update(user);



                                DateTime firstDay = startdate;
                                string IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;


                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLNext(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;


                                    WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;

                                    user = WorkHoursList1.Where(x => x.UserName.Equals(username) && x.dates == user.dates && x.WRID == user.WRID).FirstOrDefault();


                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.Update(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;

                                    }

                                }

                                IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;
                                firstDay = startdate;
                                user.dates = startdate;
                                user.WRID = enddate.ToString(); ;

                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLBack(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;


                                    WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;


                                    user = WorkHoursList1.Where(x => x.UserName.Equals(username) && x.dates.Date == user.dates.Date && x.WRID == user.WRID).FirstOrDefault();

                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.Update(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;
                                    }


                                }

                            }
                            else
                            {

                                // Non Young seaferer......

                                user.WRID = enddate.ToString();
                                int Cellcount = CheckRetard(user.WRID, user.dates, CheckRetar);

                                string label42 = string.Empty;
                                for (int i = 0; i < Cellcount; i++)
                                {

                                    label42 += "0" + ",";
                                }

                                user.Cellcount = Cellcount;
                                user.hrs = label42;
                                user.dates = startdate.Date;


                                sc.WorkHourss.Add(user);
                                sc.SaveChanges();



                                ITimeSlotManager WorkhoursInstanse;

                                if (RuleName == "Normal")
                                    WorkhoursInstanse = new NormalTimeSlotManager();
                                else
                                    WorkhoursInstanse = new ManilaTimeSlotManager();






                                string username = user.UserName;
                                DateTime dt11 = startdate;
                                string wrid = enddate.ToString();

                                var CrewDetailList1 = CrewDetailList(user);
                                //con = 14;

                                WorkHoursList1 = WorkHoursList(user);
                                var MaxFutureDate1 = MaxFutureDate;

                                user = WorkHoursList1.Where(x => x.UserName.Equals(username) && x.dates.Date == startdate.Date && x.WRID == enddate.ToString()).FirstOrDefault();

                                int wid = user.wid;

                                WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);

                                user.wid = wid;
                                WorkhoursInstanse.Update(user);



                                DateTime firstDay = startdate;
                                string IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;

                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLNext(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;

                                  
                                    WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;

                                    user = WorkHoursList1.Where(x => x.UserName.Equals(username) && x.dates == user.dates && x.WRID == user.WRID).FirstOrDefault();


                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.Update(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;
                                       
                                    }


                                }


                                IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;
                                firstDay = startdate;
                                user.dates = startdate;
                                user.WRID = enddate.ToString(); ;


                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLBack(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;

                                    WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;

                                    user = WorkHoursList1.Where(x => x.UserName.Equals(username) && x.dates.Date == user.dates.Date && x.WRID == user.WRID).FirstOrDefault();

                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.Update(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;
                                       
                                    }


                                }




                            }


                        }

                    }

                }


                // ..........Conditions for the Planner......................

                foreach (var li1 in workhoursUsersPlanner)
                {

                    var obj1 = WorkhoursListPlanner.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Month == startdate.Month && x.dates.Year == startdate.Year).ToList();
                    if (obj1 != null)
                    {
                        var obj = WorkhoursListPlanner.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Date == startdate.Date && x.WRID == enddate.ToString()).ToList();
                        foreach (var ob in obj)
                        {

                            var local = sc.Set<WorkHoursPlannerClass>().Local.FirstOrDefault(c => c.wid == ob.wid);
                            if (local != null)
                            {
                                sc.Entry(local).State = EntityState.Detached;
                            }


                            sc.Entry(ob).State = EntityState.Deleted;
                            sc.SaveChanges();


                        }

                        var user = WorkhoursListPlanner.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Date == startdate.Date).FirstOrDefault();

                        if (user == null)
                            user = WorkhoursListPlanner.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Month == startdate.Month).FirstOrDefault();

                        var checkuser1 = CrewList.Where(x => x.UserName == li1).FirstOrDefault();
                        if (checkuser1 != null)
                        {
                            bool chkyoungs = Convert.ToBoolean(checkuser1.chkyoungs);
                            if (chkyoungs == true)
                            {
                                user.WRID = enddate.ToString();
                                int Cellcount = CheckRetardYoung(user.WRID, user.dates, CheckRetar);

                                string label42 = string.Empty;
                                for (int i = 0; i < Cellcount; i++)
                                {

                                    label42 += "0" + ",";
                                }

                                user.Cellcount = Cellcount;
                                user.hrs = label42;
                                user.dates = startdate.Date;



                                sc.WorkHourPlanners.Add(user);
                                sc.SaveChanges();


                                ITimeSlotManagerPlanner WorkhoursInstanse;

                                if (RuleName == "Normal")
                                    WorkhoursInstanse = new NormalTimeSlotYoungPlannerManager();
                                else
                                    WorkhoursInstanse = new ManilaTimeSlotYoungPlannerManager();



                                string username = user.UserName;
                                DateTime dt11 = startdate;
                                string wrid = enddate.ToString();

                                var CrewDetailList1 = CrewDetailListPlanner(user.UserName, user.Position, user.Department);


                                WorkHoursList1P = WorkHoursListP(user.UserName, user.Position, user.Department);
                                var MaxFutureDate1 = MaxFutureDate;

                                user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates.Date == startdate.Date && x.WRID == enddate.ToString()).FirstOrDefault();

                                int wid = user.wid;

                                WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);

                                user.wid = wid;
                                WorkhoursInstanse.UpdatePlanner(user);



                                DateTime firstDay = startdate;
                                string IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;


                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLNext(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;


                                    //WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;

                                    user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates == user.dates && x.WRID == user.WRID).FirstOrDefault();


                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.UpdatePlanner(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursPlannerClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;

                                    }

                                }

                                IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;
                                firstDay = startdate;
                                user.dates = startdate;
                                user.WRID = enddate.ToString(); ;

                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                   
                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLBack(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;


                                    //WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;


                                    user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates.Date == user.dates.Date && x.WRID == user.WRID).FirstOrDefault();

                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.UpdatePlanner(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursPlannerClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;
                                    }


                                }

                            }
                            else
                            {

                                // Non Young seaferer......

                                user.WRID = enddate.ToString();
                                int Cellcount = CheckRetard(user.WRID, user.dates, CheckRetar);

                                string label42 = string.Empty;
                                for (int i = 0; i < Cellcount; i++)
                                {

                                    label42 += "0" + ",";
                                }

                                user.Cellcount = Cellcount;
                                user.hrs = label42;
                                user.dates = startdate.Date;


                                sc.WorkHourPlanners.Add(user);
                                sc.SaveChanges();



                                ITimeSlotManagerPlanner WorkhoursInstanse;

                                if (RuleName == "Normal")
                                    WorkhoursInstanse = new NormalTimeSlotPlannerManager();
                                else
                                    WorkhoursInstanse = new ManilaTimeSlotPlannerManager();






                                string username = user.UserName;
                                DateTime dt11 = startdate;
                                string wrid = enddate.ToString();

                                //var CrewDetailList1 = CrewDetailList(user);
                                //con = 14;/
                                //WorkHoursList1 = WorkHoursList(user);


                                var CrewDetailList1 = CrewDetailListPlanner(user.UserName, user.Position, user.Department);
                                WorkHoursList1 = WorkHoursListPlanner(user.UserName, user.Position, user.Department);
                                WorkHoursList1P = WorkHoursListP(user.UserName, user.Position, user.Department);



                                var MaxFutureDate1 = MaxFutureDate;

                                user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates.Date == startdate.Date && x.WRID == enddate.ToString()).FirstOrDefault();
                                int wid = user.wid;

                                WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);

                                user.wid = wid;
                                WorkhoursInstanse.UpdatePlanner(user);



                                DateTime firstDay = startdate;
                                string IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;

                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLNext(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;

                                    //WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;

                                    user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates == user.dates && x.WRID == user.WRID).FirstOrDefault();


                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.UpdatePlanner(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursPlannerClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;

                                    }


                                }


                                IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;
                                firstDay = startdate;
                                user.dates = startdate;
                                user.WRID = enddate.ToString(); ;


                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLBack(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;

                                    //WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;



                                    user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates.Date == user.dates.Date && x.WRID == user.WRID).FirstOrDefault();

                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.UpdatePlanner(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursPlannerClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;

                                    }


                                }




                            }


                        }

                    }

                }


                //......... End Conditions for the Planner...................

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void TimeSlotPageSettingDeleteMethodPlanner(DateTime startdate, object enddate, BackgroundWorker Workersave, Grid DynamicGrid)
        {
            int ib = 2;
            Workersave.ReportProgress(ib);

            try
            {

                string monthname = Convert.ToDateTime(startdate).ToString("MMMM");
                string yearname = Convert.ToDateTime(startdate).ToString("yyyy");

                List<WorkHoursPlannerClass> WorkhoursListmainPlanner;
                List<CrewDetailClass> CrewList;
                List<RetardtblClass> CheckRetar;
                using (ShipmentContaxt sc1 = new ShipmentContaxt())
                {
                    WorkhoursListmainPlanner = sc1.WorkHourPlanners.ToList();
                    CrewList = sc1.CrewDetails.ToList();
                    CheckRetar = sc1.Retardtbls.ToList();
                }


                var WorkhoursListPlanner = WorkhoursListmainPlanner.Where(x => x.dates.Month == startdate.Month && x.dates.Year == startdate.Year).ToList();
                var workhoursUsersPlanner = WorkhoursListmainPlanner.Where(x => x.dates.Month == startdate.Month && x.dates.Year == startdate.Year).Select(x => x.UserName).Distinct().ToList();

                int con = workhoursUsersPlanner.Count();
                con = (con * 14);
                foreach (var li1 in workhoursUsersPlanner)
                {

                    var obj1 = WorkhoursListPlanner.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Month == startdate.Month && x.dates.Year == startdate.Year).ToList();
                    if (obj1 != null)
                    {
                        var obj = WorkhoursListPlanner.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Date == startdate.Date && x.WRID == enddate.ToString()).ToList();
                        foreach (var ob in obj)
                        {

                            var local = sc.Set<WorkHoursPlannerClass>().Local.FirstOrDefault(c => c.wid == ob.wid);
                            if (local != null)
                            {
                                sc.Entry(local).State = EntityState.Detached;
                            }


                            sc.Entry(ob).State = EntityState.Deleted;
                            sc.SaveChanges();


                        }

                        var user = WorkhoursListPlanner.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Date == startdate.Date).FirstOrDefault();

                        if (user == null)
                            user = WorkhoursListPlanner.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && x.dates.Month == startdate.Month).FirstOrDefault();

                        var checkuser1 = CrewList.Where(x => x.UserName == li1).FirstOrDefault();
                        if (checkuser1 != null)
                        {
                            bool chkyoungs = Convert.ToBoolean(checkuser1.chkyoungs);
                            if (chkyoungs == true)
                            {
                                user.WRID = enddate.ToString();
                                int Cellcount = CheckRetardYoung(user.WRID, user.dates, CheckRetar);

                                string label42 = string.Empty;
                                for (int i = 0; i < Cellcount; i++)
                                {

                                    label42 += "0" + ",";
                                }

                                user.Cellcount = Cellcount;
                                user.hrs = label42;
                                user.dates = startdate.Date;



                                sc.WorkHourPlanners.Add(user);
                                sc.SaveChanges();


                                ITimeSlotManagerPlanner WorkhoursInstanse;

                                if (RuleName == "Normal")
                                    WorkhoursInstanse = new NormalTimeSlotYoungPlannerManager();
                                else
                                    WorkhoursInstanse = new ManilaTimeSlotYoungPlannerManager();



                                string username = user.UserName;
                                DateTime dt11 = startdate;
                                string wrid = enddate.ToString();

                                var CrewDetailList1 = CrewDetailListPlanner(user.UserName,user.Position,user.Department);


                                WorkHoursList1P = WorkHoursListP(user.UserName, user.Position, user.Department);
                                var MaxFutureDate1 = MaxFutureDate;

                                user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates.Date == startdate.Date && x.WRID == enddate.ToString()).FirstOrDefault();

                                int wid = user.wid;

                                WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);

                                user.wid = wid;
                                WorkhoursInstanse.UpdatePlanner(user);



                                DateTime firstDay = startdate;
                                string IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;


                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLNext(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;


                                    //WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;

                                    user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates == user.dates && x.WRID == user.WRID).FirstOrDefault();


                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.UpdatePlanner(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursPlannerClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;

                                    }

                                }

                                IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;
                                firstDay = startdate;
                                user.dates = startdate;
                                user.WRID = enddate.ToString(); ;

                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                    //if (iDatema == 1)
                                    //    firstDay = dt11;

                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLBack(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;


                                    //WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;


                                    user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates.Date == user.dates.Date && x.WRID == user.WRID).FirstOrDefault();

                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.UpdatePlanner(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursPlannerClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;
                                    }


                                }

                            }
                            else
                            {

                                // Non Young seaferer......

                                user.WRID = enddate.ToString();
                                int Cellcount = CheckRetard(user.WRID, user.dates, CheckRetar);

                                string label42 = string.Empty;
                                for (int i = 0; i < Cellcount; i++)
                                {

                                    label42 += "0" + ",";
                                }

                                user.Cellcount = Cellcount;
                                user.hrs = label42;
                                user.dates = startdate.Date;


                                sc.WorkHourPlanners.Add(user);
                                sc.SaveChanges();



                                ITimeSlotManagerPlanner WorkhoursInstanse;

                                if (RuleName == "Normal")
                                    WorkhoursInstanse = new NormalTimeSlotPlannerManager();
                                else
                                    WorkhoursInstanse = new ManilaTimeSlotPlannerManager();






                                string username = user.UserName;
                                DateTime dt11 = startdate;
                                string wrid = enddate.ToString();

                                //var CrewDetailList1 = CrewDetailList(user);
                                //con = 14;/
                                //WorkHoursList1 = WorkHoursList(user);


                                var CrewDetailList1 = CrewDetailListPlanner(user.UserName, user.Position, user.Department);
                                WorkHoursList1 = WorkHoursListPlanner(user.UserName, user.Position, user.Department);
                                WorkHoursList1P = WorkHoursListP(user.UserName, user.Position, user.Department);



                                var MaxFutureDate1 = MaxFutureDate;

                                user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates.Date == startdate.Date && x.WRID == enddate.ToString()).FirstOrDefault();
                                int wid = user.wid;

                                WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);

                                user.wid = wid;
                                WorkhoursInstanse.UpdatePlanner(user);



                                DateTime firstDay = startdate;
                                string IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;

                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLNext(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;

                                    //WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;

                                    user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates == user.dates && x.WRID == user.WRID).FirstOrDefault();


                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.UpdatePlanner(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursPlannerClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;
                                       
                                    }


                                }


                                IDL = enddate.ToString().Contains("IDL") == true ? "IDL" : null;
                                firstDay = startdate;
                                user.dates = startdate;
                                user.WRID = enddate.ToString(); ;


                                for (int iDatema = 1; iDatema < 8; iDatema++)
                                {

                                    ib = ib + 1;
                                    Workersave.ReportProgress(ib * (100 / con));

                                    var checkIDL = WorkhoursInstanse.CheckIDLBack(firstDay, IDL, CheckIDL);
                                    firstDay = checkIDL.Item1;
                                    user.dates = checkIDL.Item1;
                                    user.WRID = checkIDL.Item2;
                                    IDL = checkIDL.Item3;

                                    //WorkHoursList1 = WorkHoursList(user);
                                    MaxFutureDate = null;



                                    user = WorkHoursList1P.Where(x => x.UserName.Equals(username) && x.dates.Date == user.dates.Date && x.WRID == user.WRID).FirstOrDefault();

                                    if (user != null)
                                    {
                                        wid = user.wid;
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(user, WorkHoursList1, CrewDetailList1, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                                        user.wid = wid;
                                        WorkhoursInstanse.UpdatePlanner(user);
                                    }
                                    else
                                    {
                                        user = new WorkHoursPlannerClass();
                                        user.dates = checkIDL.Item1;
                                        user.WRID = checkIDL.Item2;
                                        user.wid = wid;
                                       
                                    }


                                }




                            }


                        }

                    }

                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }



        private static string ruleName;
        public string RuleName
        {
            get
            {
                if (string.IsNullOrEmpty(ruleName))
                {
                    var rule = sc.RuleTables.Select(x => x.Status).FirstOrDefault();
                    ruleName = rule == false ? "Normal" : "Manila";
                }
                return ruleName;
            }

        }
        private static string opa90Rule;
        public string Opa90Rule
        {
            get
            {
                if (string.IsNullOrEmpty(opa90Rule))
                {
                    var checkopa = sc.RuleTables.Select(x => x.StatusOPA90).FirstOrDefault();
                    if (checkopa == null)
                        checkopa = "OPA90option1";
                    opa90Rule = checkopa;
                }
                return opa90Rule;
            }

        }


        private static List<InternationalClass> _CheckIDL;
        public List<InternationalClass> CheckIDL
        {
            get
            {
                if (_CheckIDL == null)
                    _CheckIDL = sc.Internationals.ToList();
                return _CheckIDL;
            }
            set
            {
                _CheckIDL = value;
            }
        }
        public CrewDetailClass CrewDetailList(WorkHoursClass obj)
        {
            var _CrewDetailList = sc.CrewDetails.Where(x => x.UserName.Equals(obj.UserName) && x.position.Equals(obj.Position) && x.department.Equals(obj.Department)).FirstOrDefault();
            return _CrewDetailList;

        }


        public CrewDetailClass CrewDetailListPlanner(string UserName, string Position, string Department)
        {
            var _CrewDetailList = sc.CrewDetails.Where(x => x.UserName.Equals(UserName) && x.position.Equals(Position) && x.department.Equals(Department)).FirstOrDefault();
            return _CrewDetailList;

        }

        public List<WorkHoursClass> WorkHoursList(WorkHoursClass obj)
        {
            DateTime nowdate = DateTime.Now;
            DateTime stdate = nowdate.AddMonths(-2);
            DateTime estdate = nowdate.AddMonths(2);

            using (ShipmentContaxt sc1 = new ShipmentContaxt())
            {

                var data = sc1.WorkHourss.Where(x => x.UserName.Equals(obj.UserName) && x.Position.Equals(obj.Position) && x.Department.Equals(obj.Department) && (x.dates >= stdate && x.dates <= estdate)).ToList();

                WorkHoursList1 = data;

                return data;
            }

        }

        public List<WorkHoursClass> WorkHoursListPlanner(string UserName, string Position, string Department)
        {
            DateTime nowdate = DateTime.Now;
            DateTime stdate = nowdate.AddMonths(-2);
            DateTime estdate = nowdate.AddMonths(2);

            using (ShipmentContaxt sc1 = new ShipmentContaxt())
            {

                var data = sc1.WorkHourss.Where(x => x.UserName.Equals(UserName) && x.Position.Equals(Position) && x.Department.Equals(Department) && (x.dates >= stdate && x.dates <= estdate)).ToList();
                WorkHoursList1 = data;
                return data;
            }

        }

        public List<WorkHoursPlannerClass> WorkHoursListP(string UserName,string Position,string Department)
        {
            DateTime nowdate = DateTime.Now;
            DateTime stdate = nowdate.AddMonths(-2);
            DateTime estdate = nowdate.AddMonths(2);

            using (ShipmentContaxt sc1 = new ShipmentContaxt())
            {

                var data = sc1.WorkHourPlanners.Where(x => x.UserName.Equals(UserName) && x.Position.Equals(Position) && x.Department.Equals(Department) && (x.dates >= stdate && x.dates <= estdate)).ToList();

                WorkHoursList1P = data;
                return data;
            }

        }

        private List<WorkHoursClass> workHoursList1;
        public List<WorkHoursClass> WorkHoursList1
        {
            get { return workHoursList1; }
            set { workHoursList1 = value; }
        }

        private List<WorkHoursPlannerClass> workHoursList1P;
        public List<WorkHoursPlannerClass> WorkHoursList1P
        {
            get { return workHoursList1P; }
            set { workHoursList1P = value; }
        }

        private static DefineRulesClass _DefineRules;
        public DefineRulesClass DefineRules
        {
            get
            {
                if (_DefineRules == null)
                    _DefineRules = sc.DefineRules.SqlQuery("SELECT * FROM DefineRules").FirstOrDefault();
                //_DefineRules = sc.DefineRules.FirstOrDefault();
                return _DefineRules;
            }

        }


        private DateTime? maxFutureDate;
        public DateTime? MaxFutureDate
        {
            get
            {
                if (maxFutureDate == null)
                {
                    var dt = WorkHoursList1.Where(p => p.TotalHours > 0 && p.dates > DateTime.Now).ToList();
                    maxFutureDate = dt.Count > 0 ? dt.Max(x => x.dates).Date : DateTime.Now.Date;
                }
                return maxFutureDate;
            }
            set
            {
                maxFutureDate = value;
            }
        }




        public void TimeSlotPageSettingSaveMethod(DateTime startdate, object enddate, BackgroundWorker Workersave, Grid DynamicGrid)
        {
            //throw new NotImplementedException();
        }
    }
}
