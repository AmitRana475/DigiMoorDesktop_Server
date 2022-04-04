using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorkShipVersionII.WorkHoursViewModel.TimeSlotModels;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class RetardAdvanceViewModel:ViewModelBase
    {
        private  ShipmentContaxt sc;
        DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private BackgroundWorker Workersave;
        private BackgroundWorker Workerdelete;
        int sa = 0;
        Grid DynamicGrid = new Grid();
        public RetardAdvanceViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            RadioBTNCommand = new RelayCommand<object>(RadioBTNmethod);
            ArchivesCommand = new RelayCommand<object>(Archivesmethod);
            //saveCommand = new RelayCommand<RetardtblClass>(SaveMethod);
            //deleteCommand = new RelayCommand<RetardtblClass>(DeleteMethod);
            printCommand = new RelayCommand<RetardtblClass>(PrintMethod);

            _CanvasVisible = "Collapsed";
            yearname = sc.GetYear();
            monthname = sc.GetMonth();
            DateTime end = start.AddMonths(1).AddDays(-1);
            GetRetardList(start, end);


            saveCommand = new RelayCommand(() => Workersave.RunWorkerAsync(AddRETADV1), () => !Workersave.IsBusy);
            Workersave = new BackgroundWorker();
            Workersave.DoWork += (s, e) => SaveMethod(AddRETADV1);
            Workersave.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            Workersave.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SaveCompleted);
            Workersave.WorkerReportsProgress = true;
            Workersave.WorkerSupportsCancellation = true;


            deleteCommand = new RelayCommand(() => Workerdelete.RunWorkerAsync(SelectedItem), () => !Workerdelete.IsBusy);
            Workerdelete = new BackgroundWorker();
            Workerdelete.DoWork += (s, e) => DeleteMethod(SelectedItem);
            Workerdelete.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            Workerdelete.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DeleteCompleted);
            Workerdelete.WorkerReportsProgress = true;
            Workerdelete.WorkerSupportsCancellation = true;



        }

      

        private void DeleteCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (sa > 0)
                {
                    DateTime end = start.AddMonths(1).AddDays(-1);
                    GetRetardList(start, end);
                    MessageBox.Show("Record deleted successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    OnPropertyChanged(new PropertyChangedEventArgs("LoadRetardList"));

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
                    DateTime end = start.AddMonths(1).AddDays(-1);
                    GetRetardList(start, end);
                    MessageBox.Show("Record saved successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    OnPropertyChanged(new PropertyChangedEventArgs("LoadRetardList"));

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

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        private ICommand printCommand;
        public ICommand PrintCommand
        {
            get { return printCommand; }
        }


        public ICommand RadioBTNCommand { get; private set; }
        public ICommand ArchivesCommand { get; private set; }

        private static RetardtblClass _AddRETADV1 = new RetardtblClass();
        public RetardtblClass AddRETADV1
        {
            get
            {

                return _AddRETADV1;
            }
            set
            {
                _AddRETADV1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddRETADV1"));



            }
        }

        private static ObservableCollection<RetardtblClass> loadRetardList = new ObservableCollection<RetardtblClass>();
        public ObservableCollection<RetardtblClass> LoadRetardList
        {
            get
            {
                return loadRetardList;
            }
            set
            {
                loadRetardList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadRetardList"));
            }
        }


        private static ITimeSlotSettingManager iDLInstanse;
        public ITimeSlotSettingManager IDLInstanse
        {
            get
            {
                if (iDLInstanse == null)
                    iDLInstanse = new TimeSlotRetardAdvanceManager();

                return iDLInstanse;
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

       
        private string _GridVisible;
        public string GridVisible
        {
            get
            {
                if (_IDLName == " Archives ")
                {
                    _GridVisible = "Visible";
                    GridHeight = "210";
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

        private RetardtblClass selecteditem;
        public RetardtblClass SelectedItem
        {
            get { return selecteditem; }
            set
            {
                selecteditem = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedItem"));
            }
        }

        private static Nullable<DateTime> _SDateFrom = null;
        public Nullable<DateTime> SDateFrom
        {
            get
            {
                if (_SDateFrom == null)
                    _SDateFrom = start;
                if (_IDLName == "  Go Back  ")
                    GetRetardList((DateTime)_SDateFrom, (DateTime)SDateTo);
                return _SDateFrom;
            }
            set
            {
                _SDateFrom = value;
                RaisePropertyChanged("SDateFrom");
            }
        }

        private Nullable<DateTime> _SDateTo = null;
        public Nullable<DateTime> SDateTo
        {
            get
            {
                if (_SDateTo == null)
                    _SDateTo = start.AddMonths(1).AddDays(-1);

                if (_IDLName == "  Go Back  ")
                    GetRetardList((DateTime)_SDateFrom, (DateTime)_SDateTo);
                return _SDateTo;
            }
            set
            {
                _SDateTo = value;
                RaisePropertyChanged("SDateTo");
            }
        }


        private static Nullable<DateTime> _DateFrom = null;
        public Nullable<DateTime> DateFrom
        {
            get
            {
                if (_DateFrom == null)
                {

                    _DateFrom = start;
                }
                //_AddCrewDetail.ServiceFrom = (DateTime)_MyServiceFrom;
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

        private static Nullable<DateTime> _IDLDate1 = null;
        public Nullable<DateTime> IDLDate1
        {
            get
            {
                if (_IDLDate1 == null)
                {
                    _IDLDate1 = DateTime.Now;
                    _AddRETADV1.DateID = ((int)DateTime.Now.Day).ToString();
                }
                _AddRETADV1.RDate = (DateTime)_IDLDate1;

                return _IDLDate1;
            }
            set
            {
                _IDLDate1 = value;
                RaisePropertyChanged("IDLDate1");
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
        

        private void RadioBTNmethod(object parameter)
        {
            _AddRETADV1.RET_ADV = (string)parameter;

            OnPropertyChanged(new PropertyChangedEventArgs("AddRETADV1"));


            RaisePropertyChanged("IDLName");

        }

        private void Archivesmethod(object obj)
        {
            _IDLName = (string)obj;
            if (_IDLName == " Archives ")
            {
                _IDLName = "  Go Back  ";
                GetRetardList((DateTime)SDateFrom, (DateTime)SDateTo);
            }
            else
            {
                _IDLName = " Archives ";
                DateTime end = start.AddMonths(1).AddDays(-1);
                GetRetardList(start, end);
            }

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
        private void GetRetardList(DateTime datefrom, DateTime dateto)
        {
            try
            {
                using (ShipmentContaxt sc1 = new ShipmentContaxt())
                {

                    var data = sc1.Retardtbls.Where(x => (System.Data.Entity.DbFunctions.TruncateTime(x.RDate) >= datefrom.Date && System.Data.Entity.DbFunctions.TruncateTime(x.RDate) <= dateto.Date)).ToList();
                    LoadRetardList.Clear();
                    sc1.ObservableCollectionList(LoadRetardList, data.OrderByDescending(x => x.RDate).ToList());

                    OnPropertyChanged(new PropertyChangedEventArgs("LoadRetardList"));

                    //foreach (var item in data)
                    //{
                    //    loadRetardList.Add(new RetardtblClass() { Id = item.Id, DateID = item.DateID, RDate = item.RDate, RET_ADV = item.RET_ADV, Times = item.Times });
                    //}
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

     
        private void SaveMethod(RetardtblClass obj)
        {
            try
            {
                var findRDate = sc.Retardtbls.Where(x => System.Data.Entity.DbFunctions.TruncateTime(x.RDate) == obj.RDate.Date && x.DateID == obj.DateID).FirstOrDefault();
                if (findRDate == null)
                {
                    Workersave.ReportProgress(1);
                    IVisibles = "Visible";
                    sa = 1;

                  


                    obj.RDate = obj.RDate.Date;
                    sc.Retardtbls.Add(obj);
                    sc.SaveChanges();




                 

                    Workersave.ReportProgress(3);

                    IDLInstanse.TimeSlotPageSettingDeleteMethod(obj.RDate, obj.DateID, Workersave, DynamicGrid);

                    Workersave.ReportProgress(20 * (100 / 20));


                    //IDLDate1 = DateTime.Now;
                    //RaisePropertyChanged("IDLDate1");
                }
                else
                {
                    MessageBox.Show("This date already exist", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

      
        private void DeleteMethod(RetardtblClass obj)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    RetardtblClass findIDL = sc.Retardtbls.Where(x => x.Id == obj.Id).FirstOrDefault();
                    if (findIDL != null)
                    {
                        Workerdelete.ReportProgress(1);
                        IVisibles = "Visible";
                        sa = 1;

                        sc.Entry(findIDL).State = EntityState.Deleted;
                        sc.SaveChanges();

                        Workerdelete.ReportProgress(3);
                        IDLInstanse.TimeSlotPageSettingDeleteMethod(obj.RDate, obj.DateID, Workerdelete, DynamicGrid);

                        Workerdelete.ReportProgress(4 * (100 / 4));


                    }
                    else
                    {
                        MessageBox.Show("Record is not found ", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void PrintMethod(RetardtblClass obj)
        {

            Grid gg = new Grid();

            MessageBox.Show("Print Method");
        }

        

        new public  event PropertyChangedEventHandler PropertyChanged;
        protected   virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
