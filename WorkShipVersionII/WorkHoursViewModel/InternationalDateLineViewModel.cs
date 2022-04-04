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
    public class InternationalDateLineViewModel : ViewModelBase
    {
        private  ShipmentContaxt sc;
        DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private BackgroundWorker Workersave;
        private BackgroundWorker Workerdelete;
        int sa = 0;
        Grid DynamicGrid = new Grid();
        public InternationalDateLineViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }


            RadioBTNCommand = new RelayCommand<object>(RadioBTNmethod);
            ArchivesCommand = new RelayCommand<object>(Archivesmethod);
            printCommand = new RelayCommand<InternationalClass>(PrintMethod);
            //saveCommand = new RelayCommand<InternationalClass>(SaveMethod);
            //deleteCommand = new RelayCommand<InternationalClass>(DeleteMethod);

            _CanvasVisible = "Collapsed";

            DateTime end = start.AddMonths(1).AddDays(-1);
            GetIDLList(start, end);

            saveCommand = new RelayCommand(() => Workersave.RunWorkerAsync(AddRETADV), () => !Workersave.IsBusy);
            Workersave = new BackgroundWorker();
            Workersave.DoWork += (s, e) => SaveMethod(AddRETADV);
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
                    GetIDLList(start, end);
                    MessageBox.Show("Record deleted successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    OnPropertyChanged(new PropertyChangedEventArgs("LoadIDLList"));
                    
                    IVisibles = "Hidden";
                    sa = 0;
                    new RetardAdvanceViewModel();
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
                    GetIDLList(start, end);
                    MessageBox.Show("Record saved successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    OnPropertyChanged(new PropertyChangedEventArgs("LoadIDLList"));

                    IVisibles = "Hidden";
                    sa = 0;

                    new RetardAdvanceViewModel();
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



        private void DeleteMethod(InternationalClass obj)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    InternationalClass findIDL = sc.Internationals.Where(x => x.ID == obj.ID).FirstOrDefault();
                    if (findIDL != null)
                    {
                        Workerdelete.ReportProgress(1);
                        IVisibles = "Visible";
                        sa = 1;

                        sc.Entry(findIDL).State = EntityState.Deleted;
                        sc.SaveChanges();

                        Workerdelete.ReportProgress(3);
                        IDLInstanse.TimeSlotPageSettingDeleteMethod(obj.RDate, obj.RET_ADV, Workerdelete, DynamicGrid);

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

        private void SaveMethod(InternationalClass obj)
        {
            try
            {
                
                var findRDate = sc.Internationals.Where(x => System.Data.Entity.DbFunctions.TruncateTime(x.RDate) == obj.RDate.Date).FirstOrDefault();
                if (findRDate == null)
                {

                    Workersave.ReportProgress(1);
                    IVisibles = "Visible";
                    sa = 1;



                    obj.RDate = obj.RDate.Date;
                    sc.Internationals.Add(obj);
                    sc.SaveChanges();

                    Workersave.ReportProgress(3);

                    IDLInstanse.TimeSlotPageSettingSaveMethod(obj.RDate, obj.RET_ADV, Workersave, DynamicGrid);


                    if (obj.RET_ADV.ToLower() == "advance")
                    {
                        var ret = sc.Retardtbls.Where(x => System.Data.Entity.DbFunctions.TruncateTime(x.RDate) == obj.RDate.Date).ToList();
                        sc.Retardtbls.RemoveRange(ret);
                        sc.SaveChanges();
                    }

                    Workersave.ReportProgress(4 * (100 / 4));

                    IDLDate = DateTime.Now;
                    RaisePropertyChanged("IDLDate");

                   

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

        private void PrintMethod(InternationalClass obj)
        {
            MessageBox.Show("Print Method");
        }
        

        private InternationalClass selecteditem;
        public InternationalClass SelectedItem
        {
            get { return selecteditem; }
            set
            {
                selecteditem = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedItem"));
            }
        }


        public ICommand RadioBTNCommand { get; private set; }
        public ICommand ArchivesCommand { get; private set; }

        private ICommand printCommand;
        public ICommand PrintCommand
        {
            get { return printCommand; }
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



        private static ITimeSlotSettingManager iDLInstanse;
        public ITimeSlotSettingManager IDLInstanse
        {
            get
            {
                if (iDLInstanse == null)
                    iDLInstanse = new TimeSlotInternationalDLManager();

                return iDLInstanse;
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


        private InternationalClass _AddRETADV = new InternationalClass();
        public InternationalClass AddRETADV
        {
            get
            {

                return _AddRETADV;
            }
            set
            {
                _AddRETADV = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddRETADV"));



            }
        }

        private static ObservableCollection<InternationalClass> loadIDLList;
        public ObservableCollection<InternationalClass> LoadIDLList
        {
            get
            {
                if (loadIDLList == null)
                    loadIDLList = new ObservableCollection<InternationalClass>();

                return loadIDLList;
            }
            set
            {

                loadIDLList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadIDLList"));
            }
        }

        private void GetIDLList(DateTime datefrom, DateTime dateto)
        {
            try
            {

                using (ShipmentContaxt sc1 = new ShipmentContaxt())
                {

                    var data = sc1.Internationals.Where(x => (System.Data.Entity.DbFunctions.TruncateTime(x.RDate) >= datefrom.Date && System.Data.Entity.DbFunctions.TruncateTime(x.RDate) <= dateto.Date)).ToList();

                    //loadIDLList = new ObservableCollection<InternationalClass>();
                    LoadIDLList.Clear();
                    
                    sc1.ObservableCollectionList(LoadIDLList, data.OrderByDescending(x => x.RDate).ToList());
                    OnPropertyChanged(new PropertyChangedEventArgs("LoadIDLList"));

                  
                    //loadIDLList = new ObservableCollection<InternationalClass>();
                    //foreach (var item in data)
                    //{
                    //    loadIDLList.Add(new InternationalClass() { ID = item.ID, RDate = item.RDate, RET_ADV = item.RET_ADV });
                    //}
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
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
                    GridHeight = "130";
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

        private void RadioBTNmethod(object parameter)
        {
            try
            {
                _AddRETADV.RET_ADV = (string)parameter;
                OnPropertyChanged(new PropertyChangedEventArgs("AddRETADV"));

                RaisePropertyChanged("IDLName");
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }

        private void Archivesmethod(object obj)
        {
            try
            {
                _IDLName = (string)obj;
                if (_IDLName == " Archives ")
                {
                    _IDLName = "  Go Back  ";
                    GetIDLList((DateTime)SDateFrom, (DateTime)SDateTo);
                }
                else
                {
                    _IDLName = " Archives ";
                    DateTime end = start.AddMonths(1).AddDays(-1);
                    GetIDLList(start, end);
                }
                RaisePropertyChanged("IDLName");
                RaisePropertyChanged("CanvasVisible");
                if (_IDLName == " Archives ")
                {
                    _GridVisible = "Visible";
                    _GridHeight = "130";
                }
                else
                {
                    _GridVisible = "Collapsed";
                    _GridHeight = "0";
                }
                RaisePropertyChanged("GridVisible");
                RaisePropertyChanged("GridHeight");

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }

        //DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        private static Nullable<DateTime> _SDateFrom = null;
        public Nullable<DateTime> SDateFrom
        {
            get
            {
                if (_SDateFrom == null)
                    _SDateFrom = start;
                if (_IDLName == "  Go Back  ")
                    GetIDLList((DateTime)_SDateFrom, (DateTime)SDateTo);
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
                    GetIDLList((DateTime)_SDateFrom, (DateTime)_SDateTo);
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

        private Nullable<DateTime> _IDLDate = null;
        

        public Nullable<DateTime> IDLDate
        {
            get
            {
                if (_IDLDate == null)
                {
                    _IDLDate = DateTime.Now;
                }
                _AddRETADV.RDate = (DateTime)_IDLDate;
                return _IDLDate;
            }
            set
            {
                _IDLDate = value;
                RaisePropertyChanged("IDLDate");
            }
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }
}
