using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using System.Windows;
using DataBuildingLayer;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows.Controls;
using WorkShipVersionII.WorkHoursViewModel.TimeSlotModels;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class OPA90ViewModel : ViewModelBase
    {
        private ShipmentContaxt sc;
        private BackgroundWorker _Workersave;
        private BackgroundWorker _Workerdelete;
        int sa = 0;
        Grid DynamicGrid;
        public OPA90ViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            if (DynamicGrid == null)
                DynamicGrid = new Grid();
            //saveCommand = new RelayCommand<OPAStartStopClass>(SaveMethod);
            printCommand = new RelayCommand<OPAStartStopClass>(PrintMethod);
            archivesCommand = new RelayCommand<object>(Archivesmethod);
            //deleteCommand = new RelayCommand<OPAStartStopClass>(DeleteMethod);

            yearname = sc.GetYear();
            monthname = sc.GetMonth();
            yearnameTo = sc.GetYear();
            monthnameTo = sc.GetMonth();

            sc.ObservableCollectionList(loadOPA90, GetOpalist().ToList());
            //loadOPA90 = new ObservableCollection<OPAStartStopClass>(GetOpalist().ToList());

            saveCommand = new RelayCommand(() => _Workersave.RunWorkerAsync(OPAStartStop), () => !_Workersave.IsBusy);
            _Workersave = new BackgroundWorker();
            _Workersave.WorkerReportsProgress = true;
            _Workersave.DoWork += (s, e) => SaveMethod(OPAStartStop);
            _Workersave.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            _Workersave.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SaveCompleted);

            deleteCommand = new RelayCommand(() => _Workerdelete.RunWorkerAsync(SelectedItem), () => !_Workerdelete.IsBusy);
            _Workerdelete = new BackgroundWorker();
            _Workerdelete.WorkerReportsProgress = true;
            _Workerdelete.DoWork += (s, e) => DeleteMethod(SelectedItem);
            _Workerdelete.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            _Workerdelete.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DeleteCompleted);


            _CanvasVisible = "Collapsed";

            var checkopa = sc.RuleTables.Select(x => x.StatusOPA90).FirstOrDefault();
            if (checkopa != null)
            {
                bool visible = checkopa == "OPA90option2" ? false : true;
                string hidden = checkopa == "OPA90option3" ? "Collapsed" : "Visible";
                MainViewModelClockSetting._CommonData1.OPA90Visibles = visible;
                MainViewModelClockSetting._CommonData1.OPA90Hidden = hidden;
            }
            else
            {
                MainViewModelClockSetting._CommonData1.OPA90Visibles = true;
                MainViewModelClockSetting._CommonData1.OPA90Hidden = "Visible";
            }


        }



        private List<OPAStartStopClass> GetOpalist()
        {
            try
            {
                var opalist = sc.OPAStartStops.ToList();
                opalist.ForEach(x => { if (x.startDateID.Contains("IDL")) { x.OPAStart1 = x.OPAStart.ToString("dd-MMM-yyyy HH:mm") + " (IDL) "; } else { x.OPAStart1 = x.OPAStart.ToString("dd-MMM-yyyy HH:mm"); } if (x.LongLimit) { x.OPAStop1 = "Unlimited"; } else { if (x.stopDateID.Contains("IDL")) { x.OPAStop1 = x.OPAStop.ToString("dd-MMM-yyyy HH:mm") + " (IDL) "; } else { x.OPAStop1 = x.OPAStop.ToString("dd-MMM-yyyy HH:mm"); } } });
                return opalist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                return null;

            }

        }


        private void DeleteCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (sa > 0)
                {
                    loadOPA90.Clear();
                    oPAStartStop = new OPAStartStopClass();
                    IDLDateFrom = null;
                    IDLDateTo = null;
                    sc.ObservableCollectionList(loadOPA90, GetOpalist().ToList());
                    OnPropertyChanged(new PropertyChangedEventArgs("LoadOPA90"));

                    MessageBox.Show("Record has been Deleted!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    IVisibles = "Hidden";
                    sa = 0;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void SaveCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (sa > 0)
                {
                    loadOPA90.Clear();
                    oPAStartStop = new OPAStartStopClass();
                    IDLDateFrom = null;
                    IDLDateTo = null;
                    sc.ObservableCollectionList(loadOPA90, GetOpalist().ToList());
                    OnPropertyChanged(new PropertyChangedEventArgs("LoadOPA90"));

                    MessageBox.Show("Record has been Submited!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    IVisibles = "Hidden";
                    sa = 0;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                _CurrentProgress = e.ProgressPercentage;
                RaisePropertyChanged("CurrentProgress");
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }




        private void PrintMethod(OPAStartStopClass obj)
        {
            try
            {
                MessageBox.Show("Print Method");
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void SaveMethod(OPAStartStopClass obj)
        {
            try
            {
                if (obj.LongLimit)
                    obj.OPAStop = obj.OPAStop.AddYears(99);

                if (obj.OPAStart >= obj.OPAStop)
                {
                    MessageBox.Show("Stop Date & Time cannot be grater then or equal to Start Date & Time", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var findSDate = sc.OPAStartStops.Where(x => (obj.OPAStart >= x.OPAStart && obj.OPAStart <= x.OPAStop) || (obj.OPAStop >= x.OPAStart && obj.OPAStop <= x.OPAStop)).FirstOrDefault();
                    if (findSDate == null)
                    {

                        _Workersave.ReportProgress(1);
                        IVisibles = "Visible";
                        sa = 1;

                        sc.OPAStartStops.Add(obj);
                        sc.SaveChanges();


                        //LoadOPA90 = new ObservableCollection<OPAStartStopClass>(GetOpalist().ToList());


                        OPA90Instanse.TimeSlotPageSettingSaveMethod(obj.OPAStart, obj.OPAStop, _Workersave, DynamicGrid);
                        _Workersave.ReportProgress(2 * (100 / 2));

                        //MessageBox.Show("Record saved successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        MessageBox.Show("This date already exist", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                sc.ErrorLog(ex);

            }
        }

        private void DeleteMethod(OPAStartStopClass obj)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    OPAStartStopClass findIDL = sc.OPAStartStops.Where(x => x.id == obj.id).FirstOrDefault();
                    if (findIDL != null)
                    {

                        _Workerdelete.ReportProgress(1);
                        IVisibles = "Visible";
                        sa = 1;

                        sc.Entry(findIDL).State = EntityState.Deleted;
                        sc.SaveChanges();


                        OPA90Instanse.TimeSlotPageSettingDeleteMethod(obj.OPAStart, obj.OPAStop, _Workerdelete, DynamicGrid);
                        _Workerdelete.ReportProgress(2 * (100 / 2));
                        // MessageBox.Show("Record deleted successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        MessageBox.Show("Record is not found ", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sc.ErrorLog(ex);
            }
        }







        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }

        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }

        }



        private ICommand printCommand;
        public ICommand PrintCommand
        {
            get { return printCommand; }
        }

        private ICommand archivesCommand;
        public ICommand ArchivesCommand
        {
            get
            {
                return archivesCommand;
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


        private OPAStartStopClass oPAStartStop = new OPAStartStopClass();
        public OPAStartStopClass OPAStartStop
        {
            get
            {
                return oPAStartStop;
            }
            set
            {
                oPAStartStop = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OPAStartStop"));
            }
        }

        private OPAStartStopClass selecteditem;
        public OPAStartStopClass SelectedItem
        {
            get { return selecteditem; }
            set
            {
                selecteditem = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedItem"));
            }
        }





        public static ObservableCollection<OPAStartStopClass> loadOPA90 = new ObservableCollection<OPAStartStopClass>();
        public ObservableCollection<OPAStartStopClass> LoadOPA90
        {
            get
            {
                return loadOPA90;
            }
            set
            {
                loadOPA90 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadOPA90"));

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


        private string _IDLName;
        public string IDLName
        {
            get
            {
                if (_IDLName == null)
                    _IDLName = " Archives ";

                return _IDLName;
            }
            set
            {
                _IDLName = value;
                RaisePropertyChanged("IDLName");
            }
        }



        //GridVisible
        private string _GridVisible;
        public string GridVisible
        {
            get
            {
                if (_IDLName == " Archives ")
                {
                    _GridVisible = "Visible";
                    GridHeight = "350";
                }
                else
                {
                    _GridVisible = "Collapsed";
                    GridHeight = "0";
                }

                return _GridVisible;
            }
            set
            {
                _GridVisible = value;
                RaisePropertyChanged("GridVisible");
            }
        }

        private string _GridHeight;
        public string GridHeight
        {
            get
            {


                return _GridHeight;
            }
            set
            {
                _GridHeight = value;
                RaisePropertyChanged("GridHeight");
            }
        }

        private string _CanvasVisible;
        public string CanvasVisible
        {
            get
            {
                if (_IDLName == " Archives ")
                    _CanvasVisible = "Collapsed";
                else
                    _CanvasVisible = "Visible";

                return _CanvasVisible;
            }
            set
            {
                _CanvasVisible = value;
                RaisePropertyChanged("CanvasVisible");
            }
        }

        private void Archivesmethod(object obj)
        {
            _IDLName = (string)obj;
            if (_IDLName == " Archives ")
                _IDLName = "  Go Back  ";
            else
                _IDLName = " Archives ";
            RaisePropertyChanged("IDLName");
            RaisePropertyChanged("CanvasVisible");
            if (_IDLName == " Archives ")
            {
                _GridVisible = "Visible";
                _GridHeight = "210";
            }
            else
            {
                _GridVisible = "Collapsed";
                _GridHeight = "0";
            }
            RaisePropertyChanged("GridVisible");
            RaisePropertyChanged("GridHeight");




        }

        DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private static Nullable<DateTime> _DateFrom = null;
        public Nullable<DateTime> DateFrom
        {
            get
            {
                if (_DateFrom == null)
                {

                    _DateFrom = start;
                }

                return _DateFrom;
            }
            set
            {
                _DateFrom = value;
                RaisePropertyChanged("DateFrom");
            }
        }

        private Nullable<DateTime> _DateTo = null;
        public Nullable<DateTime> DateTo
        {
            get
            {
                if (_DateTo == null)
                {

                    _DateTo = start.AddMonths(1).AddDays(-1);
                }
                //_AddCrewDetail.ServiceTo = (DateTime)_MyServiceTo;
                return _DateTo;
            }
            set
            {
                _DateTo = value;
                RaisePropertyChanged("DateTo");
            }
        }

        private Nullable<DateTime> _IDLDateFrom = null;
        public Nullable<DateTime> IDLDateFrom
        {
            get
            {
                if (_IDLDateFrom == null)
                {
                    _IDLDateFrom = DateTime.Now;
                    oPAStartStop.startDateID = DateTime.Now.Day.ToString();

                }

                oPAStartStop.OPAStart = Convert.ToDateTime(Convert.ToDateTime(_IDLDateFrom).ToShortDateString() + " " + timefrom);
                return _IDLDateFrom;
            }
            set
            {
                _IDLDateFrom = value;
                RaisePropertyChanged("IDLDateFrom");
            }
        }

        private static Nullable<DateTime> _IDLDateTo = null;
        public Nullable<DateTime> IDLDateTo
        {
            get
            {
                if (_IDLDateTo == null)
                {
                    _IDLDateTo = DateTime.Now;
                    oPAStartStop.stopDateID = DateTime.Now.Day.ToString();

                }

                oPAStartStop.OPAStop = Convert.ToDateTime(Convert.ToDateTime(_IDLDateTo).ToShortDateString() + " " + timeto);
                return _IDLDateTo;
            }
            set
            {
                _IDLDateTo = value;
                RaisePropertyChanged("IDLDateTo");
            }
        }

        private static string timefrom = "12:00";
        public string TimeFrom
        {
            get
            {
                oPAStartStop.OPAStart = Convert.ToDateTime(Convert.ToDateTime(_IDLDateFrom).ToShortDateString() + " " + timefrom);
                return timefrom;
            }
            set
            {
                timefrom = value;
                RaisePropertyChanged("TimeFrom");
            }
        }
        private static string timeto = "12:00";
        public string TimeTo
        {
            get
            {
                oPAStartStop.OPAStop = Convert.ToDateTime(Convert.ToDateTime(_IDLDateTo).ToShortDateString() + " " + timeto);
                return timeto;
            }
            set
            {
                timeto = value;
                RaisePropertyChanged("TimeTo");
            }
        }



        public static ObservableCollection<GridClass> loadCalender = new ObservableCollection<GridClass>();
        public ObservableCollection<GridClass> LoadCalender
        {
            get
            {
                return loadCalender;
            }
            set
            {
                loadCalender = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCalender"));

            }
        }

        private static ObservableCollection<string> yearname = new ObservableCollection<string>();
        public ObservableCollection<string> YearName
        {
            get
            {
                return yearname;
            }
            set
            {
                yearname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("YearName"));
            }
        }

        private static ObservableCollection<string> monthname = new ObservableCollection<string>();
        public ObservableCollection<string> MonthName
        {
            get
            {
                return monthname;
            }
            set
            {
                monthname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MonthName"));
            }
        }


        private static string syearname;
        public string SYearName
        {
            get
            {
                if (syearname == null)
                    syearname = DateTime.Now.Year.ToString();
                BindCalender(syearname, smonthname);
                return syearname;
            }

            set
            {
                syearname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SYearName"));
            }
        }

        private static string smonthname;
        public string SMonthName
        {
            get
            {
                if (smonthname == null)
                    smonthname = DateTime.Now.ToString("MMMM");
                BindCalender(syearname, smonthname);
                return smonthname;
            }

            set
            {
                smonthname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SMonthName"));
            }
        }


        public static ObservableCollection<GridClass> loadCalenderTo = new ObservableCollection<GridClass>();
        public ObservableCollection<GridClass> LoadCalenderTo
        {
            get
            {
                return loadCalenderTo;
            }
            set
            {
                loadCalenderTo = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCalenderTo"));

            }
        }

        private static ObservableCollection<string> yearnameTo = new ObservableCollection<string>();
        public ObservableCollection<string> YearNameTo
        {
            get
            {
                return yearnameTo;
            }
            set
            {
                yearnameTo = value;
                OnPropertyChanged(new PropertyChangedEventArgs("YearNameTo"));
            }
        }

        private static ObservableCollection<string> monthnameTo = new ObservableCollection<string>();
        public ObservableCollection<string> MonthNameTo
        {
            get
            {
                return monthnameTo;
            }
            set
            {
                monthnameTo = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MonthNameTo"));
            }
        }


        private static string syearnameTo;
        public string SYearNameTo
        {
            get
            {
                if (syearnameTo == null)
                    syearnameTo = DateTime.Now.Year.ToString();
                BindCalenderTo(syearnameTo, smonthnameTo);
                return syearnameTo;
            }

            set
            {
                syearnameTo = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SYearNameTo"));
            }
        }

        private static string smonthnameTo;
        public string SMonthNameTo
        {
            get
            {
                if (smonthnameTo == null)
                    smonthnameTo = DateTime.Now.ToString("MMMM");
                BindCalenderTo(syearnameTo, smonthnameTo);
                return smonthnameTo;
            }

            set
            {
                smonthnameTo = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SMonthNameTo"));
            }
        }


        private List<WorkHoursClass> _UserList;
        public List<WorkHoursClass> UserList
        {
            get
            {
                //if (_UserList == null)
                _UserList = sc.WorkHourss.Where(x => (DbFunctions.TruncateTime(x.dates) >= OPAStartStop.OPAStart.Date && DbFunctions.TruncateTime(x.dates) <= OPAStartStop.OPAStop.Date)).OrderBy(o => o.dates).ToList();
                return _UserList;
            }
            set
            {
                _UserList = value;
            }
        }
        private List<WorkHoursClass> _UserListMain;
        public List<WorkHoursClass> UserListMain
        {
            get
            {
                //if (_UserListMain == null)
                _UserListMain = sc.WorkHourss.Where(x => (DbFunctions.TruncateTime(x.dates) >= OPAStartStop.OPAStart.Date.AddDays(-15) && DbFunctions.TruncateTime(x.dates) <= OPAStartStop.OPAStop.Date.AddDays(15))).OrderBy(o => o.dates).ToList();
                return _UserListMain;
            }
            set
            {
                _UserListMain = value;
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
        private void BindCalender(string syearname, string smonthname)
        {
            string[] arr = sc.CommonCalender(syearname, smonthname);

            int fularr = arr.Count();
            loadCalender.Clear();

            for (int k = 0; k < fularr; k++)
            {


                loadCalender.Add(new GridClass() { Day1 = fularr - k >= 0 ? arr[k] : string.Empty, Day2 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day3 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day4 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day5 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day6 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day7 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty });


            }

        }

        private void BindCalenderTo(string syearname, string smonthname)
        {
            string[] arr = sc.CommonCalender(syearname, smonthname);

            int fularr = arr.Count();
            loadCalenderTo.Clear();

            for (int k = 0; k < fularr; k++)
            {


                loadCalenderTo.Add(new GridClass() { Day1 = fularr - k >= 0 ? arr[k] : string.Empty, Day2 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day3 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day4 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day5 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day6 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day7 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty });


            }

        }









        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }

}
