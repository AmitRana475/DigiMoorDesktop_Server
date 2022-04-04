using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Data.Entity;
using System.Threading;
using WorkShipVersionII.WorkHoursViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WorkShipVersionII.WorkHoursViewModel.TimeSlotModels;

namespace WorkShipVersionII.ViewModelAdministration
{
    public class RulesViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private BackgroundWorker _Rules;
        private BackgroundWorker _Opa90;
        private Grid DynamicGrid = new Grid();
        public RulesViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            _RulesCommand = new RelayCommand(() => _Rules.RunWorkerAsync(), () => !_Rules.IsBusy);
            _Rules = new BackgroundWorker();
            _Rules.WorkerReportsProgress = true;
            _Rules.DoWork += RulesMethod;
            _Rules.ProgressChanged += new ProgressChangedEventHandler(RulesProgress);
            _Rules.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RulesCompleted);




            _Opa90Command = new RelayCommand(() => _Opa90.RunWorkerAsync(), () => !_Opa90.IsBusy);
            _Opa90 = new BackgroundWorker();
            _Opa90.WorkerReportsProgress = true;
            _Opa90.DoWork += OPa90Method;
            _Opa90.ProgressChanged += new ProgressChangedEventHandler(Opa90ProgressChanged);
            _Opa90.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Opa90Completed);

            RadioBTNCommand = new RelayCommand<object>(RadioBTNmethod);


            CheckedOpa90(RulesInfo);
            RaisePropertyChanged("OpaRuleName");
        }

        private void RadioBTNmethod(object parameter)
        {

            OpaRuleNames = (string)parameter;
            RaisePropertyChanged("OpaRuleNames");

        }

        public ICommand RadioBTNCommand { get; private set; }
        private ICommand _RulesCommand;
        public ICommand RulesCommand
        {
            get
            {
                return _RulesCommand;
            }

        }

        private ICommand _Opa90Command;
        public ICommand Opa90Command
        {
            get
            {
                return _Opa90Command;
            }

        }


        private double _RuleProgress;
        public double RuleProgress
        {
            get
            {

                return _RuleProgress;
            }
            set
            {
                _RuleProgress = value;
                RaisePropertyChanged("RuleProgress");
            }
        }

        private double _Opa90Progress;
        public double Opa90Progress
        {
            get
            {

                return _Opa90Progress;
            }
            set
            {
                _Opa90Progress = value;
                RaisePropertyChanged("Opa90Progress");
            }
        }



        private string ruleVisibles;
        public string RuleVisibles
        {
            get
            {
                if (ruleVisibles == null)
                {
                    ruleVisibles = "Collapsed";
                }
                return ruleVisibles;
            }
            set
            {
                ruleVisibles = value;
                RaisePropertyChanged("RuleVisibles");
            }
        }

        private string opaVisibles;
        public string OpaVisibles
        {
            get
            {
                if (opaVisibles == null)
                {
                    opaVisibles = "Collapsed";
                }
                return opaVisibles;
            }
            set
            {
                opaVisibles = value;
                RaisePropertyChanged("OpaVisibles");
            }
        }

        private OpaRuleTypes _OpaRuleName;
        public OpaRuleTypes OpaRuleName
        {
            get
            {
                if (_OpaRuleName == null)
                    _OpaRuleName = new OpaRuleTypes();
                return _OpaRuleName;

            }
            set
            {
                _OpaRuleName = value;
                RaisePropertyChanged("OpaRuleName");
            }
        }

        private string _OpaRuleNames;
        public string OpaRuleNames
        {
            get
            {
                if (_OpaRuleNames == null)
                {
                    var checkrule = sc.RuleTables.Select(s => s.StatusOPA90).FirstOrDefault();
                    _OpaRuleNames = checkrule == null ? "OPA90option1" : checkrule;
                }
                return _OpaRuleNames;

            }
            set
            {
                _OpaRuleNames = value;
                RaisePropertyChanged("OpaRuleNames");
            }
        }

        private static string ruleName;
        public string RuleName
        {
            get
            {
                if (ruleName == null)
                    ruleName = RulesInfo != null && RulesInfo.Status ? "Click to Deactiviate STCW Exception" : "Click to Activiate STCW Exception";
                return ruleName;
            }
            set
            {
                ruleName = value;
                RaisePropertyChanged("RuleName");
            }
        }


        private static RuleTableClass _RulesInfo;
        public RuleTableClass RulesInfo
        {
            get
            {
                if (_RulesInfo == null)
                    _RulesInfo = sc.RuleTables.FirstOrDefault();
                CheckedOpa90(_RulesInfo);
                RaisePropertyChanged("OpaRuleName");
                return _RulesInfo;
            }
            set
            {
                _RulesInfo = value;
                RaisePropertyChanged("RulesInfo");
            }
        }

        private List<WorkHoursClass> WorkHoursList { get; set; }
        private ITimeSlotManager WorkhoursInstanse { get; set; }

        private List<WorkHoursPlannerClass> WorkHoursListPlanner { get; set; }
        private ITimeSlotManagerPlanner WorkhoursInstansePlanner { get; set; }


        private DateTime? maxFutureDate;
        public DateTime? MaxFutureDate
        {
            get
            {
                if (maxFutureDate == null)
                {
                    var dt = WorkHoursList.Where(p => p.TotalHours > 0 && p.dates > DateTime.Now).ToList();
                    maxFutureDate = dt.Count > 0 ? dt.Max(x => x.dates).Date : DateTime.Now.Date;
                }
                return maxFutureDate;
            }
            set
            {
                maxFutureDate = value;
            }
        }

        private static ITimeSlotSettingManager oPA90Instanse;
        public ITimeSlotSettingManager OPA90Instanse
        {
            get
            {
                if (oPA90Instanse == null)
                    oPA90Instanse = new TimeSlotOPA90Manager();

                return oPA90Instanse;
            }

        }
        private void CheckedOpa90(RuleTableClass RulesInfo)
        {
            if (RulesInfo != null)
            {
                OpaRuleName.OPA90option1 = RulesInfo.StatusOPA90 == "OPA90option1" ? true : false;
                OpaRuleName.OPA90option2 = RulesInfo.StatusOPA90 == "OPA90option2" ? true : false;
                OpaRuleName.OPA90option3 = RulesInfo.StatusOPA90 == "OPA90option3" ? true : false;
            }
        }

        private void Opa90Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Opa90Completed  " + OpaRuleNames);
            OpaVisibles = "Collapsed";

            new OPA90ViewModel();
        }

        private void Opa90ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _Opa90Progress = e.ProgressPercentage;
            RaisePropertyChanged("Opa90Progress");
        }

        private void RulesProgress(object sender, ProgressChangedEventArgs e)
        {
            _RuleProgress = e.ProgressPercentage;
            RaisePropertyChanged("RuleProgress");
        }

        private void RulesCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (RuleName == "Click to Deactiviate STCW Exception")
                MessageBox.Show("STCW Exception has been Activated", "", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("STCW Exception has been Deactivated", "", MessageBoxButton.OK, MessageBoxImage.Information);
            RuleVisibles = "Collapsed";
        }




        private void RulesMethod(object sender, DoWorkEventArgs e)
        {

            //...........
            int ib = 1;
            _Rules.ReportProgress(ib);
            RuleVisibles = "Visible";
            //...............

            // Refresh Instance to the TimeSlot's page
            TimeSlotViewModel.workhoursInstanse = null;
            TimeSlotYoungViewModel.workhoursInstanse = null;

            var checkrule = sc.RuleTables.FirstOrDefault();
            if (checkrule == null)
            {
                checkrule = new RuleTableClass();
                var data = new RuleTableClass()
                {
                    RuleNames = "Manila",
                    Status = false,
                    StatusOPA90 = "OPA90option1",
                    OneHour = true
                };
                sc.RuleTables.Add(data);
                sc.SaveChanges();

            }
            else
            {
                checkrule.Status = checkrule.Status == true ? false : true;


                //var user = new RuleTableClass() { ID = checkrule.ID, Status = checkrule.Status };
                sc.RuleTables.Attach(checkrule);
                sc.Entry(checkrule).State = EntityState.Modified;
                sc.SaveChanges();

                RulesInfo = sc.RuleTables.FirstOrDefault();
                RuleName = RulesInfo != null && RulesInfo.Status == true ? "Click to Deactiviate STCW Exception" : "Click to Activiate STCW Exception";

                RaisePropertyChanged("RuleName");




            }

            DefineRulesClass DefineRules = sc.DefineRules.FirstOrDefault();
            CrewDetailClass CrewDetailList;
            List<WorkHoursClass> WorkhoursListmain;
            List<WorkHoursPlannerClass> WorkhoursListmainPlanner;
            List<CrewDetailClass> CrewList;
            List<InternationalClass> CheckIDL;


            if (checkrule.Status)
            {

                //Manila...



                WorkhoursListmain = sc.WorkHourss.ToList();
                WorkhoursListmainPlanner = sc.WorkHourPlanners.ToList();
                CrewList = sc.CrewDetails.ToList();
                CheckIDL = sc.Internationals.ToList();


                if (WorkhoursListmain != null || WorkhoursListmainPlanner != null)
                {
                    DateTime maxDate = WorkhoursListmain.Max(m => m.dates);
                    DateTime maxDate1 = WorkhoursListmainPlanner.Max(m => m.dates);
                    DateTime minDate = DateTime.Now.Date;

                    //......Planner....

                    int Days1 =
                     (from d in WorkhoursListmainPlanner select (maxDate1 - minDate).Days).FirstOrDefault();
                    Days1 = Days1 > 0 ? Days1 : 0;

                    var workhoursUsersPlanner = WorkhoursListmainPlanner.Where(x => (x.dates.Date >= minDate.Date && x.dates.Date <= maxDate.Date)).Select(x => x.UserName).Distinct().ToList();

                    int con1 = workhoursUsersPlanner.Count();
                    con1 = (con1 * Days1);

                    //......End planner....


                    int Days =
                      (from d in WorkhoursListmain select (maxDate - minDate).Days).FirstOrDefault();
                    Days = Days > 0 ? Days : 0;

                    var workhoursUsers = WorkhoursListmain.Where(x => (x.dates.Date >= minDate.Date && x.dates.Date <= maxDate.Date)).Select(x => x.UserName).Distinct().ToList();

                    int con = workhoursUsers.Count();
                    con = (con * Days);

                    con = con + con1;



                    foreach (var li1 in workhoursUsers)
                    {
                        ib = ib + 1;
                        _Rules.ReportProgress(ib * (100 / con));

                        CrewDetailList = CrewList.Where(x => x.UserName == li1).FirstOrDefault();

                        var data = WorkhoursListmain.Where(x => x.UserName == li1 && (x.dates.Date >= minDate.Date && x.dates.Date <= Convert.ToDateTime(maxDate).Date)).ToList();
                        foreach (var obj in data)
                        {

                            ib = ib + 1;
                            _Rules.ReportProgress(ib * (100 / con));

                            WorkHoursList = null;
                            MaxFutureDate = null;

                            DateTime nowdate = obj.dates;
                            DateTime stdate = nowdate.AddMonths(-2);
                            DateTime estdate = nowdate.AddMonths(2);

                            using (ShipmentContaxt sc2 = new ShipmentContaxt())
                            {
                                WorkHoursList = sc2.WorkHourss.Where(x => x.UserName.Equals(obj.UserName) && x.Position.Equals(obj.Position) && x.Department.Equals(obj.Department) && (x.dates >= stdate && x.dates <= estdate)).ToList();
                            }


                            var checkuser1 = CrewList.Where(x => x.UserName == li1).FirstOrDefault();
                            if (checkuser1 != null)
                            {
                                bool chkyoungs = Convert.ToBoolean(checkuser1.chkyoungs);
                                if (chkyoungs == true)
                                {
                                    //Young Seaferer

                                    DateTime d1 = Convert.ToDateTime(checkuser1.DOB);
                                    DateTime d2 = obj.dates; //DateTime.Now;
                                    if (CheckDateYoung(d1, d2))
                                    {
                                    }
                                    else
                                    {
                                        //youngseafere has become above 18 years...

                                        if (obj != null)
                                        {
                                            WorkhoursInstanse = new ManilaTimeSlotYoungManager();

                                            WorkhoursInstanse.TimeSlotPageLoadMethod(obj, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, checkrule.StatusOPA90, CheckIDL, MaxFutureDate);
                                            WorkhoursInstanse.Update(obj);
                                        }

                                    }



                                }
                                else
                                {
                                    //Non Young Seaferer
                                    WorkhoursInstanse = new ManilaTimeSlotManager();
                                    if (obj != null)
                                    {
                                        WorkhoursInstanse.TimeSlotPageLoadMethod(obj, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, checkrule.StatusOPA90, CheckIDL, MaxFutureDate);
                                        WorkhoursInstanse.Update(obj);
                                    }
                                }

                            }


                        }


                    }

                    // ..........Start Conditions for the Planner..............



                    WorkHoursList = null;

                    foreach (var li1 in workhoursUsersPlanner)
                    {
                        ib = ib + 1;
                        _Rules.ReportProgress(ib * (100 / con));

                        CrewDetailList = CrewList.Where(x => x.UserName == li1).FirstOrDefault();

                        var data = WorkhoursListmainPlanner.Where(x => x.UserName == li1 && (x.dates.Date >= minDate.Date && x.dates.Date <= maxDate.Date)).ToList();
                        foreach (var obj in data)
                        {

                            ib = ib + 1;
                            _Rules.ReportProgress(ib * (100 / con));

                            //WorkHoursList = null;
                            MaxFutureDate = null;

                            DateTime nowdate = obj.dates;
                            DateTime stdate = nowdate.AddMonths(-2);
                            DateTime estdate = nowdate.AddMonths(2);

                            if (WorkHoursList == null)
                            {
                                using (ShipmentContaxt sc2 = new ShipmentContaxt())
                                {
                                    WorkHoursList = sc2.WorkHourss.Where(x => x.UserName.Equals(obj.UserName) && x.Position.Equals(obj.Position) && x.Department.Equals(obj.Department)).ToList();
                                }
                            }


                            var checkuser1 = CrewList.Where(x => x.UserName == li1).FirstOrDefault();
                            if (checkuser1 != null)
                            {
                                bool chkyoungs = Convert.ToBoolean(checkuser1.chkyoungs);
                                if (chkyoungs == true)
                                {
                                    //Young Seaferer

                                    DateTime d1 = Convert.ToDateTime(checkuser1.DOB);
                                    DateTime d2 = obj.dates; //DateTime.Now;
                                    if (CheckDateYoung(d1, d2))
                                    {
                                    }
                                    else
                                    {
                                        //youngseafere has become above 18 years...

                                        if (obj != null)
                                        {
                                            WorkhoursInstansePlanner = new ManilaTimeSlotYoungPlannerManager();

                                            WorkhoursInstansePlanner.TimeSlotPageLoadMethod(obj, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, checkrule.StatusOPA90, CheckIDL, MaxFutureDate);
                                            WorkhoursInstansePlanner.UpdatePlanner(obj);
                                        }

                                    }



                                }
                                else
                                {
                                    //Non Young Seaferer
                                    if (obj != null)
                                    {
                                        WorkhoursInstansePlanner = new ManilaTimeSlotPlannerManager();

                                        WorkhoursInstansePlanner.TimeSlotPageLoadMethod(obj, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, checkrule.StatusOPA90, CheckIDL, MaxFutureDate);
                                        WorkhoursInstansePlanner.UpdatePlanner(obj);
                                    }

                                }

                            }


                        }//Foreach


                    }


                    // ..........End Conditions for the Planner..............

                }







            }
            else
            {
                //Normal


                //List<CrewDetailClass> CrewList;
                //List<InternationalClass> CheckIDL;

                WorkhoursListmainPlanner = sc.WorkHourPlanners.ToList();
                WorkhoursListmain = sc.WorkHourss.ToList();
                CrewList = sc.CrewDetails.ToList();
                CheckIDL = sc.Internationals.ToList();



                if (WorkhoursListmain != null || WorkhoursListmainPlanner != null)
                {
                    DateTime maxDate = WorkhoursListmain.Max(m => m.dates);
                    DateTime minDate = DateTime.Now.Date;

                    //...Planner.....

                    int Days1 =
                     (from d in WorkhoursListmainPlanner select (maxDate - minDate).Days).FirstOrDefault();
                    Days1 = Days1 > 0 ? Days1 : 0;

                    var workhoursUsersPlanner = WorkhoursListmainPlanner.Where(x => (x.dates.Date >= minDate.Date && x.dates.Date <= maxDate.Date)).Select(x => x.UserName).Distinct().ToList();

                    int con1 = workhoursUsersPlanner.Count();
                    con1 = (con1 * Days1);

                    //...End Planner...


                    int Days =
                      (from d in WorkhoursListmain select (maxDate - minDate).Days).FirstOrDefault();
                    Days = Days > 0 ? Days : 0;

                    var workhoursUsers = WorkhoursListmain.Where(x => (x.dates.Date >= minDate.Date && x.dates.Date <= maxDate.Date)).Select(x => x.UserName).Distinct().ToList();

                    int con = workhoursUsers.Count();
                    con = (con * Days);


                    con = con + con1;


                    foreach (var li1 in workhoursUsers)
                    {
                        ib = ib + 1;
                        _Rules.ReportProgress(ib * (100 / con));

                        CrewDetailList = CrewList.Where(x => x.UserName == li1).FirstOrDefault();

                        var data = WorkhoursListmain.Where(x => x.UserName == li1 && (x.dates.Date >= minDate.Date && x.dates.Date <= maxDate.Date)).ToList();
                        foreach (var obj in data)
                        {

                            ib = ib + 1;
                            _Rules.ReportProgress(ib * (100 / con));

                            WorkHoursList = null;
                            MaxFutureDate = null;

                            DateTime nowdate = obj.dates;
                            DateTime stdate = nowdate.AddMonths(-2);
                            DateTime estdate = nowdate.AddMonths(2);

                            using (ShipmentContaxt sc2 = new ShipmentContaxt())
                            {
                                WorkHoursList = sc2.WorkHourss.Where(x => x.UserName.Equals(obj.UserName) && x.Position.Equals(obj.Position) && x.Department.Equals(obj.Department) && (x.dates >= stdate && x.dates <= estdate)).ToList();
                            }


                            var checkuser1 = CrewList.Where(x => x.UserName == li1).FirstOrDefault();
                            if (checkuser1 != null)
                            {
                                bool chkyoungs = Convert.ToBoolean(checkuser1.chkyoungs);
                                if (chkyoungs == true)
                                {
                                    //Young Seaferer

                                    DateTime d1 = Convert.ToDateTime(checkuser1.DOB);
                                    DateTime d2 = obj.dates; //DateTime.Now;
                                    if (CheckDateYoung(d1, d2))
                                    {
                                    }
                                    else
                                    {
                                        //youngseafere has become above 18 years...

                                        if (obj != null)
                                        {
                                            WorkhoursInstanse = new NormalTimeSlotYoungManager();

                                            WorkhoursInstanse.TimeSlotPageLoadMethod(obj, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, checkrule.StatusOPA90, CheckIDL, MaxFutureDate);
                                            WorkhoursInstanse.Update(obj);
                                        }

                                    }



                                }
                                else
                                {
                                    //Non Young Seaferer
                                    if (obj != null)
                                    {
                                        WorkhoursInstanse = new NormalTimeSlotManager();

                                        WorkhoursInstanse.TimeSlotPageLoadMethod(obj, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, checkrule.StatusOPA90, CheckIDL, MaxFutureDate);
                                        WorkhoursInstanse.Update(obj);
                                    }

                                }

                            }


                        }//Foreach


                    }



                    // ..........Start Conditions for the Planner..............


                    WorkHoursList = null;

                    foreach (var li1 in workhoursUsersPlanner)
                    {
                        ib = ib + 1;
                        _Rules.ReportProgress(ib * (100 / con));

                        CrewDetailList = CrewList.Where(x => x.UserName == li1).FirstOrDefault();

                        var data = WorkhoursListmainPlanner.Where(x => x.UserName == li1 && (x.dates.Date >= minDate.Date && x.dates.Date <= maxDate.Date)).ToList();
                        foreach (var obj in data)
                        {

                            ib = ib + 1;
                            _Rules.ReportProgress(ib * (100 / con));

                            //WorkHoursList = null;
                            MaxFutureDate = null;

                            DateTime nowdate = obj.dates;
                            DateTime stdate = nowdate.AddMonths(-2);
                            DateTime estdate = nowdate.AddMonths(2);

                            if (WorkHoursList == null)
                            {
                                using (ShipmentContaxt sc2 = new ShipmentContaxt())
                                {
                                    WorkHoursList = sc2.WorkHourss.Where(x => x.UserName.Equals(obj.UserName) && x.Position.Equals(obj.Position) && x.Department.Equals(obj.Department)).ToList();
                                }
                            }


                            var checkuser1 = CrewList.Where(x => x.UserName == li1).FirstOrDefault();
                            if (checkuser1 != null)
                            {
                                bool chkyoungs = Convert.ToBoolean(checkuser1.chkyoungs);
                                if (chkyoungs == true)
                                {
                                    //Young Seaferer

                                    DateTime d1 = Convert.ToDateTime(checkuser1.DOB);
                                    DateTime d2 = obj.dates; //DateTime.Now;
                                    if (CheckDateYoung(d1, d2))
                                    {
                                    }
                                    else
                                    {
                                        //youngseafere has become above 18 years...

                                        if (obj != null)
                                        {
                                            WorkhoursInstansePlanner = new NormalTimeSlotYoungPlannerManager();

                                            WorkhoursInstansePlanner.TimeSlotPageLoadMethod(obj, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, checkrule.StatusOPA90, CheckIDL, MaxFutureDate);
                                            WorkhoursInstansePlanner.UpdatePlanner(obj);
                                        }

                                    }



                                }
                                else
                                {
                                    //Non Young Seaferer
                                    if (obj != null)
                                    {
                                        WorkhoursInstansePlanner = new NormalTimeSlotPlannerManager();

                                        WorkhoursInstansePlanner.TimeSlotPageLoadMethod(obj, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, checkrule.StatusOPA90, CheckIDL, MaxFutureDate);
                                        WorkhoursInstansePlanner.UpdatePlanner(obj);
                                    }

                                }

                            }


                        }//Foreach


                    }

                    // ..........End Conditions for the Planner..............


                }





            }



            //Thread.Sleep(2000);

            _Rules.ReportProgress(20 * (100 / 20));

            //throw new NotImplementedException();
        }

        private bool CheckDateYoung(DateTime dt1, DateTime dt2)
        {
            var ssa = dt2.AddYears(-18) < dt1;
            return ssa;
        }

        private void OPa90Method(object sender, DoWorkEventArgs e)
        {
            try
            {
                //...........
                //int ib = 1;
                _Opa90.ReportProgress(1);
                OpaVisibles = "Visible";
                //...............

                var checkrule = sc.RuleTables.FirstOrDefault();
                if (checkrule == null)
                {
                    var data = new RuleTableClass()
                    {
                        RuleNames = "Manila",
                        Status = false,
                        StatusOPA90 = OpaRuleNames,
                        OneHour = true
                    };
                    sc.RuleTables.Add(data);
                    sc.SaveChanges();

                }
                else
                {
                    checkrule.StatusOPA90 = OpaRuleNames;
                    sc.RuleTables.Attach(checkrule);
                    sc.Entry(checkrule).State = EntityState.Modified;
                    sc.SaveChanges();

                }

                if (checkrule.StatusOPA90 == "OPA90option1")
                {
                    // Make Rule for selected date
                    var findSDate = sc.OPAStartStops.ToList();
                    int ib = 1;
                    int con = findSDate.Count;
                    con = ib + con;
                    foreach (var item in findSDate)
                    {
                        ib = ib + 1;
                        _Opa90.ReportProgress(ib * (100 / con));

                        var startdate = item.OPAStart;
                        var stopdate = item.OPAStop;

                        OPA90Instanse.TimeSlotPageSettingSaveMethod(startdate, stopdate, _Opa90, DynamicGrid);
                    }



                }
                else if (checkrule.StatusOPA90 == "OPA90option2")
                {
                    // make rule for all days 
                    // _Opa90.ReportProgress(2);
                    var WorkhoursListmain = sc.WorkHourss.ToList();

                    DateTime startdate = WorkhoursListmain.Min(m => m.dates);
                    DateTime stopdate = WorkhoursListmain.Max(m => m.dates);

                    OPA90Instanse.TimeSlotPageSettingSaveMethod(startdate, stopdate, _Opa90, DynamicGrid);


                }
                else if (checkrule.StatusOPA90 == "OPA90option3")
                {
                    //make rure to disable for all days 
                    _Opa90.ReportProgress(2);
                    var WorkhoursListmain = sc.WorkHourss.ToList();

                    DateTime startdate = WorkhoursListmain.Min(m => m.dates);
                    DateTime stopdate = WorkhoursListmain.Max(m => m.dates);

                    OPA90Instanse.TimeSlotPageSettingDeleteMethod(startdate, stopdate, _Opa90, DynamicGrid);
                }



                _Opa90.ReportProgress(20 * (100 / 20));


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }


    }

    public class OpaRuleTypes
    {
        public bool OPA90option1 { get; set; }
        public bool OPA90option2 { get; set; }
        public bool OPA90option3 { get; set; }


    }


}
