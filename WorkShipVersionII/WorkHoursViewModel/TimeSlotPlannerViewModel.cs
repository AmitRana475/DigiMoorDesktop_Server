using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewModelReports;
using WorkShipVersionII.WorkHoursView;
using WorkShipVersionII.WorkHoursViewModel.TimeSlotModels;


namespace WorkShipVersionII.WorkHoursViewModel
{
    public class TimeSlotPlannerViewModel : ViewModelBase
    {

        TSUserClass obj;
        private ShipmentContaxt sc;
        private BackgroundWorker _Workersave;
        int sa = 0;
        public TimeSlotPlannerViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            obj = new TSUserClass()
            {
                UserName = _AddWorkHours.UserName,
                FullName = _AddWorkHours.FullName,
                Dt11 = _AddWorkHours.dates.Date,
                DateId = _AddWorkHours.WRID,
                Department = _AddWorkHours.Department,
                Position = _AddWorkHours.Position,
            };

        }

        public TimeSlotPlannerViewModel(TSUserClass obj1)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            ruleName = string.Empty;

            obj = obj1;
            RadioBTNCommand = new RelayCommand<object>(RadioBTNmethod);
            _cancelWorkHoursCommand = new RelayCommand(CancelWorkHours);
            _fillRoutineCommand = new RelayCommand<WorkHoursPlannerClass>(FillRoutineMethod);
            _NextCommand = new RelayCommand<WorkHoursPlannerClass>(NextMethod);
            _BackCommand = new RelayCommand<WorkHoursPlannerClass>(BackMethod);
            _AddWorkHours = new WorkHoursPlannerClass()
            {
                UserName = obj.UserName,
                FullName = obj.FullName,
                dates = obj.Dt11.Date,
                WRID = obj.DateId,
                Department = obj.Department,
                Position = obj.Position,
            };

            RaisePropertyChanged("AddWorkHours");
            IDL = obj.IDL;


            _saveWorkHoursCommand = new RelayCommand(() => _Workersave.RunWorkerAsync(AddWorkHours), () => !_Workersave.IsBusy);
            _Workersave = new BackgroundWorker();
            _Workersave.WorkerReportsProgress = true;
            _Workersave.DoWork += (s, e) => SaveWorkHours(AddWorkHours);
            _Workersave.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            _Workersave.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SaveCompleted);


        }

        public int sa1 { get; set; } = 1;
        private void SaveCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (sa > 0)
            {
                MessageBox.Show("Data has been Submited!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                IVisibles = "Hidden";
                //NextMethod(_AddWorkHours);
                sa = 0;
                sa1 = 0;
                new NotificationViewModel();


            }

        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _CurrentProgress = e.ProgressPercentage;
            RaisePropertyChanged("CurrentProgress");
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

        private string iVisibles;
        public string IVisibles
        {
            get
            {
                if (iVisibles == null)
                {
                    iVisibles = "Hidden";
                }
                return iVisibles;
            }
            set
            {
                iVisibles = value;
                RaisePropertyChanged("IVisibles");
            }
        }
        private double _CurrentProgress;
        public double CurrentProgress
        {
            get
            {

                return _CurrentProgress;
            }
            set
            {
                _CurrentProgress = value;
                RaisePropertyChanged("CurrentProgress");
            }
        }

        public static ITimeSlotManagerPlanner workhoursInstanse;
        public ITimeSlotManagerPlanner WorkhoursInstanse
        {
            get
            {
                if (workhoursInstanse == null)
                    if (RuleName == "Normal")
                        workhoursInstanse = new NormalTimeSlotPlannerManager();
                    else
                        workhoursInstanse = new ManilaTimeSlotPlannerManager();

                return workhoursInstanse;
            }

        }


        private static List<InternationalClass> _CheckIDL;
        public List<InternationalClass> CheckIDL
        {
            get
            {
                if (_CheckIDL == null)
                    _CheckIDL = sc.Internationals.ToList();
                // _CheckIDL = sc.Internationals.SqlQuery("SELECT * FROM International").ToList();
                return _CheckIDL;
            }
            set
            {
                _CheckIDL = value;
            }
        }

        private static List<RetardtblClass> _CheckRetar;
        public List<RetardtblClass> CheckRetar
        {
            get
            {
                if (_CheckRetar == null)
                    // _CheckRetar = sc.Retardtbls.SqlQuery("SELECT * FROM RetardtblClass").ToList();
                    _CheckRetar = sc.Retardtbls.ToList();
                return _CheckRetar;
            }
            set
            {
                _CheckRetar = value;
            }
        }

        private CrewDetailClass _CrewDetailList;
        public CrewDetailClass CrewDetailList
        {
            get
            {
                if (_CrewDetailList == null)

                    _CrewDetailList = sc.CrewDetails.Where(x => x.UserName.Equals(obj.UserName) && x.position.Equals(obj.Position) && x.department.Equals(obj.Department)).FirstOrDefault();
                //_CrewDetailList = sc.CrewDetails.SqlQuery("SELECT * FROM CrewDetail").Where(x => x.UserName.Equals(obj.UserName) && x.position.Equals(obj.Position) && x.department.Equals(obj.Department)).FirstOrDefault();
                return _CrewDetailList;
            }
            set
            {
                _CrewDetailList = value;
            }
        }
        private List<WorkHoursClass> _WorkHoursList;
        public List<WorkHoursClass> WorkHoursList
        {
            get
            {
                DateTime nowdate = DateTime.Now;
                DateTime stdate = nowdate.AddMonths(-2);
                DateTime estdate = nowdate.AddMonths(2);
                if (_WorkHoursList == null)
                {
                    using (ShipmentContaxt sc1 = new ShipmentContaxt())
                    {
                        //_WorkHoursList = sc.WorkHourss.SqlQuery("SELECT * FROM WorkHours").Where(x => x.UserName.Equals(obj.UserName) && x.Position.Equals(obj.Position) && x.Department.Equals(obj.Department) && (x.dates >= stdate && x.dates <= estdate)).ToList();
                        _WorkHoursList = sc1.WorkHourss.Where(x => x.UserName.Equals(obj.UserName.Trim()) && x.Position.Equals(obj.Position.Trim()) && (x.dates >= stdate && x.dates <= estdate)).ToList();
                    }
                }
                return _WorkHoursList;
            }
            set
            {
                _WorkHoursList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("WorkHoursList"));
            }
        }

        private List<WorkHoursPlannerClass> _WorkHoursListPlanner;
        public List<WorkHoursPlannerClass> WorkHoursListPlanner
        {
            get
            {
                DateTime nowdate = DateTime.Now;
                DateTime stdate = nowdate.AddMonths(-2);
                DateTime estdate = nowdate.AddMonths(2);
                if (_WorkHoursListPlanner == null)
                {
                    using (ShipmentContaxt sc1 = new ShipmentContaxt())
                    {
                        //_WorkHoursList = sc.WorkHourss.SqlQuery("SELECT * FROM WorkHours").Where(x => x.UserName.Equals(obj.UserName) && x.Position.Equals(obj.Position) && x.Department.Equals(obj.Department) && (x.dates >= stdate && x.dates <= estdate)).ToList();
                        _WorkHoursListPlanner = sc1.WorkHourPlanners.Where(x => x.UserName.Equals(obj.UserName.Trim()) && x.Position.Equals(obj.Position.Trim()) && (x.dates >= stdate && x.dates <= estdate)).ToList();
                    }
                }
                return _WorkHoursListPlanner;
            }
            set
            {
                _WorkHoursListPlanner = value;
                OnPropertyChanged(new PropertyChangedEventArgs("WorkHoursListPlanner"));
            }
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
            set
            {
                _DefineRules = value;
            }
        }


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


        private Grid dynamicGrid = new Grid();
        public Grid DynamicGrid
        {
            get
            {
                //if (_AddWorkHours.DynamicGrid1.Children.OfType<Border>().FirstOrDefault() != null)
                //    _AddWorkHours.DynamicGrid1 = DynamicGrid;
                return dynamicGrid;
            }
            set
            {
                dynamicGrid = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DynamicGrid"));
            }
        }



        public Border pnlTemp { get; set; }
        public string label42 { get; set; }
        public static CommonProperties _CommonData1;
        public CommonProperties CommonData1
        {
            get
            {
                if (_CommonData1 == null)
                    _CommonData1 = new CommonProperties();
                _AddWorkHours.Remarks = _CommonData1.Remarks;
                return _CommonData1;
            }
            set
            {

                _CommonData1 = value;
                RaisePropertyChanged("CommonData1");
            }
        }

        private static WorkHoursPlannerClass _AddWorkHours = new WorkHoursPlannerClass();
        public WorkHoursPlannerClass AddWorkHours
        {
            get
            {


                return _AddWorkHours;
            }
            set
            {
                _AddWorkHours = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddCertificates"));



            }
        }

        private string idl;
        public string IDL
        {
            get
            {
                return idl;
            }
            set
            {
                idl = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IDL"));
            }
        }


        public ICommand RadioBTNCommand { get; private set; }
        private ICommand _saveWorkHoursCommand;
        public ICommand SaveCommand
        {
            get { return _saveWorkHoursCommand; }
        }

        private ICommand _fillRoutineCommand;
        public ICommand FillRoutineCommand
        {
            get { return _fillRoutineCommand; }
        }

        private ICommand _BackCommand;
        public ICommand BackCommand
        {
            get { return _BackCommand; }
        }
        private ICommand _NextCommand;
        public ICommand NextCommand
        {
            get { return _NextCommand; }
        }

        private ICommand _cancelWorkHoursCommand;
        public ICommand CancelCommand
        {
            get { return _cancelWorkHoursCommand; }
        }


        private void RadioBTNmethod(object parameter)
        {
            _AddWorkHours.options = (string)parameter;
            OnPropertyChanged(new PropertyChangedEventArgs("AddWorkHours"));
        }
        private void FillRoutineMethod(WorkHoursPlannerClass obj1)
        {
            try
            {
                DynamicGrid = _AddWorkHours.DynamicGrid1;
                string label42 = string.Empty;

                var data = sc.CrewDetails.Select(x => new { x.SeaNWK, x.PortNWK, x.UserName }).Where(u => u.UserName.Equals(obj1.UserName)).FirstOrDefault();

                if (data != null)
                {

                    label42 = (_AddWorkHours.options != "At Sea" && data.PortNWK != null) ? data.PortNWK.ToString() : data.SeaNWK.ToString();
                    label42 = label42.Replace("2", "1");
                }
                else
                {
                    label42 = "";
                    for (int i = 0; i <= 47; i++)
                    {

                        label42 += "0" + ",";
                    }
                }

                FillColorsInColumns(label42, DynamicGrid);

                obj1.hrs = label42;

                obj1.IDL = (_AddWorkHours.WRID.Length > 3) ? _AddWorkHours.WRID.Substring(_AddWorkHours.WRID.Length - 3, 3) : null;
                obj1.Cellcount = DynamicGrid.Children.OfType<Border>().Count();


                WorkhoursInstanse.TimeSlotPageLoadMethod(obj1, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);

                _CommonData1.NonConfirmities = obj1.NonConfirmities;
                _CommonData1.Remarks = obj1.Remarks;
                _CommonData1.TotalHours = obj1.TotalHours;
                _CommonData1.RestHours = obj1.RestHours;
                _CommonData1.RestHour7day = obj1.RestHour7day;
                OnPropertyChanged(new PropertyChangedEventArgs("CommonData1"));

                sa1 = 1;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void BackMethod(WorkHoursPlannerClass obj1)
        {
            try
            {
                //sc = new ShipmentContaxt();
                WorkHoursList = null;
                MaxFutureDate = null;
                OnPropertyChanged(new PropertyChangedEventArgs("WorkHoursList"));
                OnPropertyChanged(new PropertyChangedEventArgs("MaxFutureDate"));


                var checkIDL = WorkhoursInstanse.CheckIDLBack(obj1.dates, idl, CheckIDL);

                if (checkIDL.Item1.Date >= DateTime.Now.Date)
                {
                    _AddWorkHours.dates = checkIDL.Item1;
                    _AddWorkHours.WRID = checkIDL.Item2;
                    _AddWorkHours.IDL = checkIDL.Item3;
                    idl = checkIDL.Item3;
                    var cellcount = WorkhoursInstanse.CheckRetard(checkIDL.Item2, checkIDL.Item1, CheckRetar);
                    _AddWorkHours.Cellcount = cellcount;

                    RaisePropertyChanged("AddWorkHours");
                    RaisePropertyChanged("IDL");

                    obj.Dt11 = checkIDL.Item1;
                    obj.DateId = checkIDL.Item2;
                    obj.IDL = checkIDL.Item3;
                    obj.Cellcount = cellcount;


                    CreateTimeSlot_Load(obj, obj1.TimeSlotGrid1);
                }
                else
                    MessageBox.Show("Sorry! You cannot create past date planners.", "Crew Planner", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void NextMethod(WorkHoursPlannerClass obj1)
        {
            try
            {
                if (sa1 > 0)
                {
                    if (MessageBox.Show(" Do you want to save your Work hour inputs before shuffling dates?", "Working Hours", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {

                        WorkHoursList = null;
                        MaxFutureDate = null;
                        OnPropertyChanged(new PropertyChangedEventArgs("WorkHoursList"));
                        OnPropertyChanged(new PropertyChangedEventArgs("MaxFutureDate"));


                        var checkIDL = WorkhoursInstanse.CheckIDLNext(obj1.dates, idl, CheckIDL);

                        _AddWorkHours.dates = checkIDL.Item1;
                        _AddWorkHours.WRID = checkIDL.Item2;
                        _AddWorkHours.IDL = checkIDL.Item3;
                        idl = checkIDL.Item3;
                        var cellcount = WorkhoursInstanse.CheckRetard(checkIDL.Item2, checkIDL.Item1, CheckRetar);
                        _AddWorkHours.Cellcount = cellcount;

                        RaisePropertyChanged("AddWorkHours");
                        RaisePropertyChanged("IDL");

                        obj.Dt11 = checkIDL.Item1;
                        obj.DateId = checkIDL.Item2;
                        obj.IDL = checkIDL.Item3;
                        obj.Cellcount = cellcount;

                        CreateTimeSlot_Load(obj, obj1.TimeSlotGrid1);

                        sa1 = 0;
                    }
                }
                else
                {
                    WorkHoursList = null;
                    MaxFutureDate = null;
                    OnPropertyChanged(new PropertyChangedEventArgs("WorkHoursList"));
                    OnPropertyChanged(new PropertyChangedEventArgs("MaxFutureDate"));


                    var checkIDL = WorkhoursInstanse.CheckIDLNext(obj1.dates, idl, CheckIDL);

                    _AddWorkHours.dates = checkIDL.Item1;
                    _AddWorkHours.WRID = checkIDL.Item2;
                    _AddWorkHours.IDL = checkIDL.Item3;
                    idl = checkIDL.Item3;
                    var cellcount = WorkhoursInstanse.CheckRetard(checkIDL.Item2, checkIDL.Item1, CheckRetar);
                    _AddWorkHours.Cellcount = cellcount;

                    RaisePropertyChanged("AddWorkHours");
                    RaisePropertyChanged("IDL");

                    obj.Dt11 = checkIDL.Item1;
                    obj.DateId = checkIDL.Item2;
                    obj.IDL = checkIDL.Item3;
                    obj.Cellcount = cellcount;

                    CreateTimeSlot_Load(obj, obj1.TimeSlotGrid1);
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void SaveWorkHours(WorkHoursPlannerClass obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj.Remarks))
                {

                    int ib = 2;
                    _Workersave.ReportProgress(ib);
                    IVisibles = "Visible";
                    sa = 1;
                    sa1 = 0;
                    obj.opayoung = false;

                    var checkuser = WorkHoursListPlanner.Where(x => x.UserName.Equals(obj.UserName) && x.Position.Equals(obj.Position) && x.dates == obj.dates && x.WRID.Equals(obj.WRID)).FirstOrDefault();
                    if (checkuser != null)
                    {
                        WorkhoursInstanse.UpdatePlanner(obj);
                        Refreshnextday(obj);


                    }
                    else
                    {
                        Save(obj);


                    }

                    _Workersave.ReportProgress(ib * (100 / 2));

                }
                else
                {
                    MessageBox.Show("Please enter the Remarks.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void Save(WorkHoursPlannerClass obj)
        {
            try
            {

                var RestHour7day = obj.RestHour7day;
                var RestHourAny7day = obj.RestHourAny7day;
                obj.opayoung = false;


                int ib = 2;
                _Workersave.ReportProgress(ib);
                IVisibles = "Visible";

                DateTime dt11 = obj.dates;
                string wrid = obj.WRID;

                int year = obj.dates.Year;
                int month = obj.dates.Month;

                string m = obj.dates.ToString("MMMM");
                string daysInMonth = DateTime.DaysInMonth(year, month).ToString();
                int days = Convert.ToInt32(daysInMonth);


                var cons = 30;

                WorkhoursInstanse.SaveMethod1Planner(obj);
                ib = ib + 1;
                _Workersave.ReportProgress(ib * (100 / cons));


                DateTime firstDay = new DateTime(year, month, 1).AddDays(-1);
                DateTime lastDay = new DateTime(year, month, days);
                string IDL = null;

                for (int j = 1; j <= 62; j++)
                {
                    ib = ib + 1;
                    //if (ib >= cons)
                    _Workersave.ReportProgress(ib * (100 / cons));

                    if (firstDay >= lastDay)
                        break;



                    var checkIDL = WorkhoursInstanse.CheckIDLNext(firstDay, IDL, CheckIDL);
                    firstDay = checkIDL.Item1;
                    obj.dates = checkIDL.Item1;
                    obj.WRID = checkIDL.Item2;
                    IDL = checkIDL.Item3;

                    int columns = WorkhoursInstanse.CheckRetard(obj.WRID, firstDay, CheckRetar);
                    obj.Cellcount = columns;
                    string hrs = string.Empty;
                    for (int i = 0; i < columns; i++)
                    {
                        hrs += "0" + ",";
                    }
                    obj.hrs = hrs;
                    obj.Colors = hrs;
                    obj.NonConfirmities = string.Empty;
                    obj.NonConfirmities1 = string.Empty;

                    WorkhoursInstanse.SaveMethod2Planner(obj);//For blank insertion...

                }


                var dt = dt11.AddDays(7);
                var user1 = sc.WorkHourPlanners.Where(x => x.UserName == obj.UserName && (x.dates > dt11 && x.dates < dt)).ToList();

                if (user1 != null)
                {
                    //Update Multiple Rows at a time. 
                    user1.ForEach(x => { x.RestHour7day = RestHour7day; x.RestHourAny7day = RestHourAny7day; });
                    sc.SaveChanges();
                }



                _Workersave.ReportProgress(20 * (100 / 20));

                AddWorkHours.dates = dt11;
                AddWorkHours.WRID = wrid;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        [STAThread]
        private void Refreshnextday(WorkHoursPlannerClass obj)
        {
            try
            {
                int ib = 2;
                _Workersave.ReportProgress(ib);

                string username = obj.UserName;
                DateTime dt11 = obj.dates;
                string wrid = obj.WRID;

                int con = 14;


                DateTime firstDay = obj.dates;
                string IDL = null;

                for (int iDatema = 1; iDatema < 8; iDatema++)
                {

                    ib = ib + 1;
                    _Workersave.ReportProgress(ib * (100 / con));

                    var checkIDL = WorkhoursInstanse.CheckIDLNext(firstDay, IDL, CheckIDL);
                    firstDay = checkIDL.Item1;
                    obj.dates = checkIDL.Item1;
                    obj.WRID = checkIDL.Item2;
                    IDL = checkIDL.Item3;


                    //sc = new ShipmentContaxt();
                    WorkHoursList = null;
                    MaxFutureDate = null;
                    WorkHoursListPlanner = null;
                    OnPropertyChanged(new PropertyChangedEventArgs("WorkHoursList"));
                    OnPropertyChanged(new PropertyChangedEventArgs("MaxFutureDate"));
                    OnPropertyChanged(new PropertyChangedEventArgs("WorkHoursListPlanner"));


                    obj = WorkHoursListPlanner.Where(x => x.UserName.Equals(username) && x.dates == obj.dates && x.WRID == obj.WRID).FirstOrDefault();


                    if (obj != null)
                    {
                        WorkhoursInstanse.TimeSlotPageLoadMethod(obj, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                        obj.opayoung = false;
                        WorkhoursInstanse.UpdatePlanner(obj);
                    }
                    else
                        obj = new WorkHoursPlannerClass();



                }


                for (int iDatema = 1; iDatema < 8; iDatema++)
                {

                    if (iDatema == 1)
                        firstDay = dt11;

                    ib = ib + 1;
                    _Workersave.ReportProgress(ib * (100 / con));

                    var checkIDL = WorkhoursInstanse.CheckIDLBack(firstDay, IDL, CheckIDL);
                    firstDay = checkIDL.Item1;
                    obj.dates = checkIDL.Item1;
                    obj.WRID = checkIDL.Item2;
                    IDL = checkIDL.Item3;

                    //sc = new ShipmentContaxt();
                    WorkHoursList = null;
                    MaxFutureDate = null;
                    WorkHoursListPlanner = null;
                    OnPropertyChanged(new PropertyChangedEventArgs("WorkHoursList"));
                    OnPropertyChanged(new PropertyChangedEventArgs("MaxFutureDate"));
                    OnPropertyChanged(new PropertyChangedEventArgs("WorkHoursListPlanner"));

                    obj = WorkHoursListPlanner.Where(x => x.UserName.Equals(username) && x.dates == obj.dates && x.WRID == obj.WRID).FirstOrDefault();


                    if (obj != null)
                    {
                        WorkhoursInstanse.TimeSlotPageLoadMethod(obj, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);
                        obj.opayoung = false;
                        WorkhoursInstanse.UpdatePlanner(obj);
                    }
                    else
                        obj = new WorkHoursPlannerClass();



                }

                AddWorkHours.dates = dt11;
                AddWorkHours.WRID = wrid;

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }


        private void CancelWorkHours()
        {
            sa1 = 0;
            _CommonData1 = new CommonProperties();
            _AddWorkHours = new WorkHoursPlannerClass();
            new DetailWorkRestHoursViewModel();
            // new NotificationViewModel();
            //CrewDetailClass oobj = new CrewDetailClass()
            //{
            //    UserName = obj.UserName,
            //    name = obj.FullName,
            //    position = obj.Position,
            //    department = obj.Department
            //};
            //new WorkHoursCalenderViewModel(oobj);

            ChildWindowManager.Instance.CloseChildWindow();
        }

        //..............................

        public void CreateTimeSlot_Load(TSUserClass obj, Grid TimeSlotGrid)
        {
            try
            {

                int columns = 0;
                label42 = string.Empty;

                var checkuser = WorkHoursListPlanner.Select(s => new { s.UserName, s.dates, s.Position, s.Remarks, s.NonConfirmities, s.hrs, s.Colors, s.options, s.WRID, s.Cellcount, s.TotalHours, s.RestHours, s.RestHour7day }).Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(obj.Dt11) && x.WRID.Equals(obj.DateId) && x.Position.Equals(obj.Position)).FirstOrDefault();
                if (checkuser != null)
                {
                    columns = checkuser.Cellcount;
                    _AddWorkHours.NonConfirmities = checkuser.NonConfirmities;
                    _AddWorkHours.hrs = checkuser.hrs;
                    _AddWorkHours.Remarks = checkuser.Remarks;
                    _AddWorkHours.Colors = checkuser.Colors;
                    label42 = checkuser.hrs;
                    _AddWorkHours.Cellcount = columns;
                    _AddWorkHours.opayoung = false;
                    RaisePropertyChanged("AddWorkHours");

                    _CommonData1.TotalHours = checkuser.TotalHours;
                    _CommonData1.RestHours = checkuser.RestHours;
                    _CommonData1.RestHour7day = checkuser.RestHour7day;
                    _CommonData1.Remarks = checkuser.Remarks;

                    RaisePropertyChanged("CommonData1");

                }
                else
                {
                    columns = WorkhoursInstanse.CheckRetard(obj.DateId, obj.Dt11, CheckRetar);
                    for (int i = 0; i < columns; i++)
                    {
                        label42 += "0" + ",";
                    }

                    _AddWorkHours.Cellcount = columns;
                    _AddWorkHours.hrs = label42;
                    _AddWorkHours.Colors = label42;
                    _AddWorkHours.Remarks = string.Empty;
                    // _AddWorkHours.ColorsPlanner = label42;
                    //_AddWorkHours.Planner = "Planner";
                    _AddWorkHours.opayoung = false;
                    RaisePropertyChanged("AddWorkHours");

                }


                TimeSlotGrid.Children.Clear();
                TimeSlotGrid.ColumnDefinitions.Clear();
                TimeSlotGrid.RowDefinitions.Clear();

                AddWorkHours.TimeSlotGrid1 = TimeSlotGrid;

                columns = columns > 0 ? columns : WorkhoursInstanse.CheckRetard(obj.DateId, obj.Dt11, CheckRetar);

                float lenth = (float)1010 / columns;


                Canvas canvas = new Canvas();
                canvas.Height = 95;
                canvas.Width = 1030;
                canvas.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ededef"));

                TextBlock txtblack;
                double width = 6;
                double txtcol = columns / 2;
                for (int i = 0; i <= txtcol; i++)
                {
                    txtblack = new TextBlock()
                    {
                        Name = "TB" + i,
                        Text = i.ToString()
                    };

                    Canvas.SetTop(txtblack, 18);
                    Canvas.SetLeft(txtblack, width);
                    canvas.Children.Add(txtblack);

                    if (i == (txtcol - 1))
                        width = width + (lenth * 2) - 2;
                    else if (i > 8)
                        width = width + (lenth * 2) - 0.5;
                    else
                        width = width + (lenth * 2);
                }

                TextBlock txtblack1;
                double width1 = 20;
                double txtcol1 = columns / 2;
                int con = columns % 2 == 0 ? columns / 2 : (columns / 2) + 1;
                for (int i = 0; i < con; i++)
                {
                    txtblack1 = new TextBlock()
                    {
                        Name = "TB1" + i,
                        Text = ((float)i + 0.5).ToString(),
                        FontSize = 11

                    };

                    Canvas.SetBottom(txtblack1, 8);
                    Canvas.SetLeft(txtblack1, width1);
                    canvas.Children.Add(txtblack1);

                    if (i == (txtcol1 - 1))
                        width1 = width1 + (lenth * 2) - 2;
                    else if (i > 8)
                        width1 = width1 + (lenth * 2) + 0.5;
                    else
                        width1 = width1 + (lenth * 2);
                }

                DynamicGrid = new Grid();
                DynamicGrid.Height = 28;


                //var rowDefinition = new RowDefinition();
                //rowDefinition.Height = GridLength.Auto;
                //grid.RowDefinitions.Add(rowDefinition);


                canvas.Children.Add(DynamicGrid);

                for (int i = 0; i < columns; i++)
                {
                    var gridcol = new ColumnDefinition()
                    {
                        Width = new GridLength(lenth)
                    };
                    DynamicGrid.ColumnDefinitions.Add(gridcol);
                }


                int count1 = DynamicGrid.ColumnDefinitions.Count;
                //for (int i = 0; i < DynamicGrid.RowDefinitions.Count; i++)
                //{
                for (int j = 0; j < count1; j++)     //it will creates panels dynamically according number of cells of tablelayoutpanel
                {
                    // Border pnlTemp = new Border();
                    pnlTemp = new Border()
                    {
                        BorderThickness = new Thickness()
                        {
                            Bottom = 1,
                            Left = 1,
                            Right = 0,
                            Top = 1
                        },
                        BorderBrush = new SolidColorBrush(Colors.Gray)
                    };
                    pnlTemp.Name = "BP" + j;
                    pnlTemp.AllowDrop = true;
                    if (j == count1 - 1)
                        pnlTemp.BorderThickness = new Thickness(1);

                    pnlTemp.Background = new SolidColorBrush(Colors.White);


                    pnlTemp.MouseDown += PnlTemp_MouseDown;
                    pnlTemp.DragEnter += PnlTemp_DragEnter;



                    pnlTemp.SetValue(Grid.ColumnProperty, j);
                    pnlTemp.SetValue(Grid.RowProperty, 0);

                    DynamicGrid.Children.Add(pnlTemp);
                }


                Canvas.SetTop(DynamicGrid, 38);
                Canvas.SetLeft(DynamicGrid, 10);

                _AddWorkHours.DynamicGrid1 = DynamicGrid;

                TimeSlotGrid.Children.Add(canvas);


                FillColorsInColumns(label42, DynamicGrid);
                WorkhoursInstanse.TimeSlotPageLoadMethod(_AddWorkHours, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);

                //_AddWorkHours.Colors = GetColorsInColumns(_AddWorkHours.hrs, DynamicGrid);
                _CommonData1.NonConfirmities = _AddWorkHours.NonConfirmities;
                _CommonData1.Remarks = _AddWorkHours.Remarks;
                _CommonData1.TotalHours = _AddWorkHours.TotalHours;
                _CommonData1.RestHours = _AddWorkHours.RestHours;
                _CommonData1.RestHour7day = _AddWorkHours.RestHour7day;
                OnPropertyChanged(new PropertyChangedEventArgs("CommonData1"));


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }
        private void PnlTemp_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                var aa = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                var bb = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));

                string colorValue1 = ((SolidColorBrush)aa).Color.ToString();
                string colorValue2 = ((SolidColorBrush)bb).Color.ToString();


                var ss = ((Border)sender).Background;
                if (((SolidColorBrush)ss).Color.ToString() == colorValue2)
                {
                    ((Border)sender).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366")); //FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                    e.Effects = DragDropEffects.Copy;


                }
                else if (((SolidColorBrush)ss).Color.ToString() == colorValue1 || ((SolidColorBrush)ss).Color.ToString() == new SolidColorBrush(Colors.Red).ToString())
                {
                    ((Border)sender).Background = new SolidColorBrush(Colors.White);//FromArgb(0, 51, 102); //Color.DarkSlateBlue;
                    e.Effects = DragDropEffects.Copy;

                }


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }
        private void PnlTemp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragDrop.DoDragDrop(pnlTemp, pnlTemp.Background, DragDropEffects.Copy);


                var aa = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                var bb = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));

                string colorValue1 = ((SolidColorBrush)aa).Color.ToString();
                string colorValue2 = ((SolidColorBrush)bb).Color.ToString();

                string lblNWkSe = string.Empty;
                foreach (Border item in DynamicGrid.Children.OfType<Border>())
                {
                    var ss1 = item.Background;
                    if (((SolidColorBrush)ss1).Color.ToString() == colorValue1)
                    {
                        lblNWkSe += "1" + ",";
                    }
                    else if (((SolidColorBrush)ss1).Color.ToString() == new SolidColorBrush(Colors.Red).ToString())
                    {
                        lblNWkSe += "1" + ",";
                    }
                    else
                    {
                        lblNWkSe += "0" + ",";
                    }
                }


                _AddWorkHours.IDL = (_AddWorkHours.WRID.Length > 3) ? _AddWorkHours.WRID.Substring(_AddWorkHours.WRID.Length - 3, 3) : null;
                _AddWorkHours.Cellcount = DynamicGrid.Children.OfType<Border>().Count();
                RaisePropertyChanged("AddWorkHours");

                WorkHoursPlannerClass obj1 = _AddWorkHours;
                obj1.hrs = lblNWkSe;
                //bj1.hrsPlanner = lblNWkSe;

                WorkhoursInstanse.TimeSlotPageLoadMethod(obj1, WorkHoursList, CrewDetailList, DynamicGrid, DefineRules, Opa90Rule, CheckIDL, MaxFutureDate);

                //obj1.Colors = GetColorsInColumns(obj1.hrs, DynamicGrid);

                _CommonData1.NonConfirmities = obj1.NonConfirmities;
                _CommonData1.Remarks = obj1.Remarks;
                _CommonData1.TotalHours = obj1.TotalHours;
                _CommonData1.RestHours = obj1.RestHours;
                _CommonData1.RestHour7day = obj1.RestHour7day;
                OnPropertyChanged(new PropertyChangedEventArgs("CommonData1"));

                sa1 = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillColorsInColumns(string label42, Grid DynamicGrid)
        {
            string[] date1 = label42.TrimEnd(',').Split(',');

            var itemMain = DynamicGrid.Children.OfType<Border>().ToList();
            var coun = date1.Length;

            for (int i = 0; i < coun; i++)
            {
                if (coun > i)
                {
                    var tag = "BP" + i.ToString();
                    Border item = itemMain.Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();


                    if (date1[i] == "1")
                    {
                        if (item != null)
                            item.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));

                    }
                    if (date1[i] == "2")
                    {
                        if (item != null)
                            item.Background = new SolidColorBrush(Colors.Red);
                    }

                    if (date1[i] == "0")
                    {
                        if (item != null)
                            item.Background = new SolidColorBrush(Colors.White);
                    }
                }
            }
        }

        private string GetColorsInColumns(string label42, Grid DynamicGrid)
        {
            string[] date1 = label42.TrimEnd(',').Split(',');

            var itemMain = DynamicGrid.Children.OfType<Border>().ToList();
            var coun = date1.Length;

            var aa = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            var bb = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF")); //white 
            var cc = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000")); //Red 

            string colorValue1 = ((SolidColorBrush)aa).Color.ToString();
            string colorValue2 = ((SolidColorBrush)bb).Color.ToString();
            string colorValue3 = ((SolidColorBrush)cc).Color.ToString();

            string label42Color = string.Empty;

            for (int i = 0; i < coun; i++)
            {
                if (coun > i)
                {

                    var tag = "BP" + i.ToString();
                    Border item = itemMain.Where(x => x.Name.Equals(tag.ToString())).FirstOrDefault();

                    var ss = item.Background;
                    if (((SolidColorBrush)ss).Color.ToString() == colorValue2)
                    {
                        label42Color += "0" + ",";
                    }
                    else if (((SolidColorBrush)ss).Color.ToString() == colorValue1)
                    {
                        label42Color += "1" + ",";
                    }
                    else if (((SolidColorBrush)ss).Color.ToString() == colorValue3)
                    {
                        label42Color += "2" + ",";
                    }



                }
            }

            return label42Color;
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }
}
