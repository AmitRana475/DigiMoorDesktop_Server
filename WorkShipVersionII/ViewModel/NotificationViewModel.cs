using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using WorkShipVersionII.Commands;

namespace WorkShipVersionII.ViewModel
{
    public class NotificationViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private readonly NotificationContaxt sc1;
        DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private int itemPerPage = 25;
        private int itemcount;

        private int itemPerPageCerti = 5;
        private int itemcountCerti;

        private int itemPerPageOffi = 5;
        private int itemcountOffi;
        int count = 0;
        public NotificationViewModel(string refresh)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc1 = new NotificationContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            //MainViewModel._CommonDeviation.DeviationRed = "Collapsed";

            itemPerPage = 25;
            itemcount = 0;

            itemPerPageCerti = 5;
            itemcountCerti = 0;

            itemPerPageOffi = 5;
            itemcountOffi = 0;


            AcknowledgeCommand = new RelayCommand(AcknowledgeMethod);
            ArchivesCommand = new RelayCommand<object>(ArchivesMethod);
            GoCommand = new RelayCommand(Searchmethod);

            NextCommand = new NextPageCommand(this);
            PreviousCommand = new PreviousPageCommand(this);
            FirstCommand = new FirstPageCommand(this);
            LastCommand = new LastPageCommand(this);

            NextCertiCommand = new NextPageCommandCerti(this);
            PreviousCertiCommand = new PreviousPageCommandCerti(this);
            FirstCertiCommand = new FirstPageCommandCerti(this);
            LastCertiCommand = new LastPageCommandCerti(this);

            NextOffiCommand = new NextPageCommandOffi(this);
            PreviousOffiCommand = new PreviousPageCommandOffi(this);
            FirstOffiCommand = new FirstPageCommandOffi(this);
            LastOffiCommand = new LastPageCommandOffi(this);
        }



        public NotificationViewModel()
        {
            try
            {
                if (sc == null)
                {
                    sc = new ShipmentContaxt();
                    sc1 = new NotificationContaxt();
                    sc.Configuration.ProxyCreationEnabled = false;
                }

                //MainViewModel._CommonDeviation.DeviationRed = "Collapsed";

                ArchivesCommand = new RelayCommand<object>(ArchivesMethod);
                GoCommand = new RelayCommand(Searchmethod);
                DeviationList = null;
                CertificationList = null;
                OfficeList = null;

                GetDeviationList();

                ViewList = new CollectionViewSource
                {
                    Source = PeopleList
                };
                ViewList.Filter += new FilterEventHandler(View_Filter);

                CurrentPageIndex = 0;
                itemcount = peopleList.Count;
                CalculateTotalPages();

                NextCommand = new NextPageCommand(this);
                PreviousCommand = new PreviousPageCommand(this);
                FirstCommand = new FirstPageCommand(this);
                LastCommand = new LastPageCommand(this);

                ViewList.View.Refresh();


                if (UserTypeClass.UserTypes == "HOD")
                {
                    if (UserTypeClass.HODAccess.CerNotification)
                    {
                        //......Certification.....
                        GetCertificateList();

                        ViewCertiList = new CollectionViewSource
                        {
                            Source = LoadCertificateComment
                        };
                        ViewCertiList.Filter += new FilterEventHandler(View_FilterCerti);

                        CurrentPageCertiIndex = 0;
                        itemcountCerti = loadCertificateComment.Count;
                        CalculateTotalCertiPages();

                        NextCertiCommand = new NextPageCommandCerti(this);
                        PreviousCertiCommand = new PreviousPageCommandCerti(this);
                        FirstCertiCommand = new FirstPageCommandCerti(this);
                        LastCertiCommand = new LastPageCommandCerti(this);

                        ViewCertiList.View.Refresh();

                    }
                    if (UserTypeClass.HODAccess.OCNotification)
                    {
                        //..Office.....

                        GetOfficeList();

                        ViewOffiList = new CollectionViewSource
                        {
                            Source = loadOfficeComment
                        };
                        ViewOffiList.Filter += new FilterEventHandler(View_FilterOffi);

                        CurrentPageOffiIndex = 0;
                        itemcountOffi = loadOfficeComment.Count;
                        CalculateTotalOffiPages();

                        NextOffiCommand = new NextPageCommandOffi(this);
                        PreviousOffiCommand = new PreviousPageCommandOffi(this);
                        FirstOffiCommand = new FirstPageCommandOffi(this);
                        LastOffiCommand = new LastPageCommandOffi(this);
                        ViewOffiList.View.Refresh();
                    }

                }
                else
                {

                    //......Certification.....
                    GetCertificateList();

                    ViewCertiList = new CollectionViewSource
                    {
                        Source = LoadCertificateComment
                    };
                    ViewCertiList.Filter += new FilterEventHandler(View_FilterCerti);

                    CurrentPageCertiIndex = 0;
                    itemcountCerti = loadCertificateComment.Count;
                    CalculateTotalCertiPages();

                    NextCertiCommand = new NextPageCommandCerti(this);
                    PreviousCertiCommand = new PreviousPageCommandCerti(this);
                    FirstCertiCommand = new FirstPageCommandCerti(this);
                    LastCertiCommand = new LastPageCommandCerti(this);

                    ViewCertiList.View.Refresh();




                    //..Office.....

                    GetOfficeList();

                    ViewOffiList = new CollectionViewSource
                    {
                        Source = loadOfficeComment
                    };
                    ViewOffiList.Filter += new FilterEventHandler(View_FilterOffi);

                    CurrentPageOffiIndex = 0;
                    itemcountOffi = loadOfficeComment.Count;
                    CalculateTotalOffiPages();

                    NextOffiCommand = new NextPageCommandOffi(this);
                    PreviousOffiCommand = new PreviousPageCommandOffi(this);
                    FirstOffiCommand = new FirstPageCommandOffi(this);
                    LastOffiCommand = new LastPageCommandOffi(this);
                    ViewOffiList.View.Refresh();
                }

                if (MainViewModel._CommonDeviation == null)
                {
                    MainViewModel._CommonDeviation = new CommonPropertiesDeviation();
                }

                if (count > 0)
                {
                    MainViewModel._CommonDeviation.DeviationRed = "Visible";
                    MainViewModel._CommonDeviation.Deviation = "Hidden";
                }
                else
                {

                    MainViewModel._CommonDeviation.DeviationRed = "Hidden";
                    MainViewModel._CommonDeviation.Deviation = "Visible";
                }
                count = 0;

                if (UserTypeClass.UserTypes == "HOD")
                {
                    if (UserTypeClass.HODAccess.CerNotification)
                    {
                        _ComCertification.CerNotification = "Visible";
                        _ComCertification.CerNotiWidth = "34*";
                    }
                    else
                    {
                        _ComCertification.CerNotification = "Collapsed";
                        _ComCertification.CerNotiWidth = "0";
                    }

                    if (UserTypeClass.HODAccess.OCNotification)
                    {
                        _ComCertification.OCNotification = "Visible";
                        _ComCertification.OCNotiWidth = "34*";
                    }
                    else
                    {
                        _ComCertification.OCNotification = "Collapsed";
                        _ComCertification.OCNotiWidth = "0";
                    }

                    RaisePropertyChanged("ComCertification");


                }


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }


        private static CommonPropertiesDeviation _ComCertification;
        public CommonPropertiesDeviation ComCertification
        {
            get
            {
                if (_ComCertification == null)
                    _ComCertification = new CommonPropertiesDeviation();
                return _ComCertification;
            }
            set
            {

                _ComCertification = value;
                RaisePropertyChanged("ComCertification");
            }
        }


        public ICommand AcknowledgeCommand { get; private set; }
        public ICommand ArchivesCommand { get; private set; }
        public ICommand GoCommand { get; private set; }

        public ICommand PreviousCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand LastCommand { get; private set; }


        public ICommand PreviousOffiCommand { get; private set; }
        public ICommand NextOffiCommand { get; private set; }
        public ICommand FirstOffiCommand { get; private set; }
        public ICommand LastOffiCommand { get; private set; }


        public ICommand PreviousCertiCommand { get; private set; }
        public ICommand NextCertiCommand { get; private set; }
        public ICommand FirstCertiCommand { get; private set; }
        public ICommand LastCertiCommand { get; private set; }





        private void AcknowledgeMethod()
        {
            try
            {

                var bms = ViewList;
                var dt = DateTime.Now.ToString("dd-MMM-yyyy hh:mm ");
                var deviation = peopleList.Where(x => x.IsCheckedV && x.NCTypeStatus == null).ToList();
                sc1.AcknowledgeDeviation(deviation);

                DeviationList = null;
                AllSelected = false;
                if (_BtnName == "Go Back")
                    GetDeviationList((DateTime)SDateFrom, (DateTime)SDateTo);
                else
                    GetDeviationList();

                CurrentPageIndex = 0;
                itemcount = peopleList.Count;
                CalculateTotalPages();
                ViewList.View.Refresh();

                var certification = loadCertificateComment.Where(x => x.IsCheckedV).ToList();
                sc1.AcknowledgeCertificate(certification);

                CertificationList = null;
                AllSelectedCerti = false;
                if (_BtnName == "Go Back")
                    GetCertificateList((DateTime)SDateFrom, (DateTime)SDateTo);
                else
                    GetCertificateList();

                CurrentPageCertiIndex = 0;
                itemcountCerti = loadCertificateComment.Count;
                CalculateTotalCertiPages();
                ViewCertiList.View.Refresh();


                var office = loadOfficeComment.ToList();
                if (UserTypeClass.UserTypes == "admin")
                {
                    office = office.Where(x => x.IsCheckedV && x.Acknowledge_Status == "To be Acknowledged").ToList();
                    office.ForEach(x => { x.Acknowledge_Status = "Acknowledged on " + dt + "Hours"; x.AdminAlarm = true; });
                }
                else if (UserTypeClass.UserTypes == "MASTER")
                {
                    office = office.Where(x => x.IsCheckedV && x.Acknowledge_Status == "To be Acknowledged").ToList();
                    office.ForEach(x => { x.Acknowledge_Status_MASTER = "Acknowledged on " + dt + "Hours"; x.MasterAlarm = true; });
                }
                else if (UserTypeClass.UserTypes == "HOD")
                {
                    office = office.Where(x => x.IsCheckedV && x.Acknowledge_Status == "To be Acknowledged").ToList();
                    office.ForEach(x => { x.Acknowledge_Status_HOD = "Acknowledged on " + dt + "Hours"; x.HODAlarm = true; });
                }
                foreach (var item in office)
                {
                    //var user = new CommentofVSClass() { Comnt_ID = item.Comnt_ID, Acknowledge_Status = item.Acknowledge_Status, AdminAlarm = item.AdminAlarm };
                    // sc.Entry(user).Property(x => x.Acknowledge_Status).IsModified = true;
                    //sc.Entry(user).Property(x => x.AdminAlarm).IsModified = true;

                    sc.CommentofVs.Attach(item);
                    sc.Entry(item).State = EntityState.Modified;
                    sc.SaveChanges();

                }


                OfficeList = null;
                AllSelectedOffi = false;

                if (_BtnName == "Go Back")
                    GetOfficeList((DateTime)SDateFrom, (DateTime)SDateTo);
                else
                    GetOfficeList();

                CurrentPageOffiIndex = 0;
                itemcountOffi = loadOfficeComment.Count;
                CalculateTotalOffiPages();
                ViewOffiList.View.Refresh();

                if (count > 0)
                {

                    if (MainViewModel._CommonDeviation == null)
                    {
                        MainViewModel._CommonDeviation = new CommonPropertiesDeviation();
                    }
                    MainViewModel._CommonDeviation.DeviationRed = "Visible";
                    MainViewModel._CommonDeviation.Deviation = "Hidden";
                }
                else
                {
                    if (MainViewModel._CommonDeviation == null)
                    {
                        MainViewModel._CommonDeviation = new CommonPropertiesDeviation();
                    }
                    MainViewModel._CommonDeviation.DeviationRed = "Hidden";
                    MainViewModel._CommonDeviation.Deviation = "Visible";
                }

                count = 0;

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void ArchivesMethod(object obj)
        {
            try
            {
                BtnName = (string)obj;
                if (_BtnName == "Archives")
                {
                    _BtnName = "Go Back";

                    SDateFrom = start;
                    SDateTo = start.AddMonths(1).AddDays(-1);

                    GetDeviationList((DateTime)SDateFrom, (DateTime)SDateTo);
                    CurrentPageIndex = 0;
                    itemcount = peopleList.Count;
                    CalculateTotalPages();
                    ViewList.View.Refresh();


                    GetCertificateList((DateTime)SDateFrom, (DateTime)SDateTo);
                    CurrentPageCertiIndex = 0;
                    itemcountCerti = loadCertificateComment.Count;
                    CalculateTotalCertiPages();
                    ViewCertiList.View.Refresh();

                    GetOfficeList((DateTime)SDateFrom, (DateTime)SDateTo);
                    CurrentPageOffiIndex = 0;
                    itemcountOffi = loadOfficeComment.Count;
                    CalculateTotalOffiPages();
                    ViewOffiList.View.Refresh();

                }
                else
                {
                    _BtnName = "Archives";

                    GetDeviationList();
                    CurrentPageIndex = 0;
                    itemcount = peopleList.Count;
                    CalculateTotalPages();
                    ViewList.View.Refresh();


                    GetCertificateList();
                    CurrentPageCertiIndex = 0;
                    itemcountCerti = loadCertificateComment.Count;
                    CalculateTotalCertiPages();
                    ViewCertiList.View.Refresh();

                    GetOfficeList();
                    CurrentPageOffiIndex = 0;
                    itemcountOffi = loadOfficeComment.Count;
                    CalculateTotalOffiPages();
                    ViewOffiList.View.Refresh();
                }
                RaisePropertyChanged("BtnName");

                if (count > 0)
                {

                    if (MainViewModel._CommonDeviation == null)
                    {
                        MainViewModel._CommonDeviation = new CommonPropertiesDeviation();
                    }
                    MainViewModel._CommonDeviation.DeviationRed = "Visible";
                    MainViewModel._CommonDeviation.Deviation = "Hidden";
                }
                else
                {
                    if (MainViewModel._CommonDeviation == null)
                    {
                        MainViewModel._CommonDeviation = new CommonPropertiesDeviation();
                    }
                    MainViewModel._CommonDeviation.DeviationRed = "Hidden";
                    MainViewModel._CommonDeviation.Deviation = "Visible";
                }
                count = 0;

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void Searchmethod()
        {
            try
            {
                if (_BtnName == "Go Back")
                {
                    GetDeviationList((DateTime)_SDateFrom, (DateTime)SDateTo);
                    CurrentPageIndex = 0;
                    itemcount = peopleList.Count;
                    CalculateTotalPages();
                    ViewList.View.Refresh();


                    GetCertificateList((DateTime)_SDateFrom, (DateTime)_SDateTo);
                    CurrentPageCertiIndex = 0;
                    itemcountCerti = loadCertificateComment.Count;
                    CalculateTotalCertiPages();
                    ViewCertiList.View.Refresh();

                    GetOfficeList((DateTime)_SDateFrom, (DateTime)_SDateTo);
                    CurrentPageOffiIndex = 0;
                    itemcountOffi = loadOfficeComment.Count;
                    CalculateTotalOffiPages();
                    ViewOffiList.View.Refresh();
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private static int _currentPageIndex;
        public int CurrentPageIndex
        {
            get
            {
                CurrentPage = _currentPageIndex;
                return _currentPageIndex;
            }
            set
            {
                _currentPageIndex = value;
                RaisePropertyChanged("CurrentPageIndex");
            }
        }
        private static int _CurrentPage;
        public int CurrentPage
        {
            get
            {
                if (_totalPages > 0)
                    return _currentPageIndex + 1;
                return _CurrentPage;
            }
            set
            {
                _CurrentPage = value;
                RaisePropertyChanged("CurrentPage");
            }
        }

        private static int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            private set
            {
                _totalPages = value;
                RaisePropertyChanged("TotalPages");
            }
        }





        private static int _currentPageCertiIndex;
        public int CurrentPageCertiIndex
        {
            get
            {
                CurrentCertiPage = _currentPageCertiIndex;
                return _currentPageCertiIndex;
            }
            set
            {
                _currentPageCertiIndex = value;
                RaisePropertyChanged("CurrentPageCertiIndex");
            }
        }
        private static int _CurrentCertiPage;
        public int CurrentCertiPage
        {
            get
            {
                if (_totalCertiPages > 0)
                    return _currentPageCertiIndex + 1;
                return _CurrentCertiPage;
            }
            set
            {
                _CurrentCertiPage = value;
                RaisePropertyChanged("CurrentCertiPage");
            }
        }

        private static int _totalCertiPages;
        public int TotalCertiPages
        {
            get { return _totalCertiPages; }
            private set
            {
                _totalCertiPages = value;
                RaisePropertyChanged("TotalCertiPages");
            }
        }





        private static int _currentPageOffiIndex;
        public int CurrentPageOffiIndex
        {
            get
            {
                CurrentOffiPage = _currentPageOffiIndex;
                return _currentPageOffiIndex;
            }
            set
            {
                _currentPageOffiIndex = value;
                RaisePropertyChanged("CurrentPageOffiIndex");
            }
        }
        private static int _CurrentOffiPage;
        public int CurrentOffiPage
        {
            get
            {
                if (_totalOffiPages > 0)
                    return _currentPageOffiIndex + 1;
                return _CurrentOffiPage;
            }
            set
            {
                _CurrentOffiPage = value;
                RaisePropertyChanged("CurrentOffiPage");
            }
        }

        private static int _totalOffiPages;
        public int TotalOffiPages
        {
            get { return _totalOffiPages; }
            private set
            {
                _totalOffiPages = value;
                RaisePropertyChanged("TotalOffiPages");
            }
        }



        public static CollectionViewSource ViewList { get; set; }

        private static ObservableCollection<WorkHoursNC> peopleList = new ObservableCollection<WorkHoursNC>();
        public ObservableCollection<WorkHoursNC> PeopleList
        {
            get
            {
                return peopleList;
            }
            set
            {
                peopleList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PeopleList"));
            }
        }


        public static CollectionViewSource ViewCertiList { get; set; }

        private static ObservableCollection<CertificateNC> loadCertificateComment = new ObservableCollection<CertificateNC>();
        public ObservableCollection<CertificateNC> LoadCertificateComment
        {
            get
            {
                return loadCertificateComment;
            }
            set
            {
                loadCertificateComment = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCertificateComment"));

            }
        }

        public static CollectionViewSource ViewOffiList { get; set; }

        private static ObservableCollection<CommentofVSClass> loadOfficeComment = new ObservableCollection<CommentofVSClass>();
        public ObservableCollection<CommentofVSClass> LoadOfficeComment
        {
            get
            {
                return new ObservableCollection<CommentofVSClass>(loadOfficeComment);
            }
            set
            {
                loadOfficeComment = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadOfficeComment"));

            }
        }


        private static List<WorkHoursNC> deviationList = new List<WorkHoursNC>();
        public List<WorkHoursNC> DeviationList
        {
            get
            {
                if (deviationList == null)
                    if (UserTypeClass.UserTypes == "HOD")
                    {
                        string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');


                        var list = sc1.GetDeviationList();
                        list = list.Where(x => depart.Contains(x.Department)).ToList();
                        deviationList = list;
                    }
                    else
                    {
                        deviationList = sc1.GetDeviationList();
                    }

                return deviationList;
            }
            set
            {
                deviationList = value;
            }
        }

        private static List<CertificateNC> certificationList = new List<CertificateNC>();
        public List<CertificateNC> CertificationList
        {
            get
            {
                if (certificationList == null)
                    certificationList = sc1.GetCertificationList();
                return certificationList;
            }
            set
            {
                certificationList = value;
            }
        }


        private static List<CommentofVSClass> officeList = new List<CommentofVSClass>();
        public List<CommentofVSClass> OfficeList
        {
            get
            {
                if (officeList == null)
                    officeList = sc.CommentofVs.ToList();
                return officeList;
            }
            set
            {
                officeList = value;
            }
        }


        public void ShowNextPage()
        {
            try
            {
                CurrentPageIndex++;
                ViewList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void ShowPreviousPage()
        {
            try
            {
                CurrentPageIndex--;
                ViewList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void ShowFirstPage()
        {
            try
            {
                CurrentPageIndex = 0;
                ViewList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void ShowLastPage()
        {
            try
            {
                CurrentPageIndex = TotalPages - 1;
                ViewList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        public void ShowOffiNextPage()
        {
            try
            {
                CurrentPageOffiIndex++;
                ViewOffiList.View.Refresh();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void ShowOffiPreviousPage()
        {
            try
            {
                CurrentPageOffiIndex--;
                ViewOffiList.View.Refresh();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void ShowOffiFirstPage()
        {
            try
            {
                CurrentPageOffiIndex = 0;
                ViewOffiList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void ShowOffiLastPage()
        {
            try
            {
                CurrentPageOffiIndex = TotalOffiPages - 1;
                ViewOffiList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        public void ShowCertiNextPage()
        {
            try
            {
                CurrentPageCertiIndex++;
                ViewCertiList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void ShowCertiPreviousPage()
        {
            try
            {
                CurrentPageCertiIndex--;
                ViewCertiList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void ShowCertiFirstPage()
        {
            try
            {
                CurrentPageCertiIndex = 0;
                ViewCertiList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        public void ShowCertiLastPage()
        {
            try
            {
                CurrentPageCertiIndex = TotalCertiPages - 1;
                ViewCertiList.View.Refresh();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void View_Filter(object sender, FilterEventArgs e)
        {
            try
            {
                int index = ((WorkHoursNC)e.Item).Id - 1;
                if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void View_FilterCerti(object sender, FilterEventArgs e)
        {
            try
            {
                int index = ((CertificateNC)e.Item).Cid - 1;
                if (index >= itemPerPageCerti * CurrentPageCertiIndex && index < itemPerPageCerti * (CurrentPageCertiIndex + 1))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void View_FilterOffi(object sender, FilterEventArgs e)
        {
            try
            {
                int index = ((CommentofVSClass)e.Item).Id - 1;
                if (index >= itemPerPageOffi * CurrentPageOffiIndex && index < itemPerPageOffi * (CurrentPageOffiIndex + 1))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void CalculateTotalPages()
        {
            try
            {
                if (itemcount % itemPerPage == 0)
                {
                    TotalPages = (itemcount / itemPerPage);
                }
                else
                {
                    TotalPages = (itemcount / itemPerPage) + 1;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void CalculateTotalCertiPages()
        {
            try
            {
                if (itemcountCerti % itemPerPageCerti == 0)
                {
                    TotalCertiPages = (itemcountCerti / itemPerPageCerti);
                }
                else
                {
                    TotalCertiPages = (itemcountCerti / itemPerPageCerti) + 1;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void CalculateTotalOffiPages()
        {
            try
            {
                if (itemcountOffi % itemPerPageOffi == 0)
                {
                    TotalOffiPages = (itemcountOffi / itemPerPageOffi);
                }
                else
                {
                    TotalOffiPages = (itemcountOffi / itemPerPageOffi) + 1;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }






        private bool allSelected;
        public bool AllSelected
        {
            get
            {
                return allSelected;
            }
            set
            {
                try
                {
                    allSelected = value;
                    peopleList.ToList().ForEach(x => x.IsCheckedV = value);

                    List<WorkHoursNC> bb = peopleList.ToList();
                    peopleList.Clear();
                    bb.ToList().ForEach(peopleList.Add);
                    RaisePropertyChanged("PeopleList");
                    ViewList.View.Refresh();
                }
                catch (Exception ex)
                {
                    sc.ErrorLog(ex);
                    allSelected = value;
                }
                // RaisePropertyChanged(() => this.PeopleList);


            }
        }

        private bool allSelectedCerti;
        public bool AllSelectedCerti
        {
            get
            {
                return allSelectedCerti;
            }
            set
            {
                try
                {
                    var num = CurrentPageCertiIndex * 5;
                    allSelectedCerti = value;
                    loadCertificateComment.Skip(num).ToList().ForEach(x => x.IsCheckedV = value);

                    var bb = loadCertificateComment.ToList();
                    loadCertificateComment.Clear();
                    bb.ToList().ForEach(loadCertificateComment.Add);
                    RaisePropertyChanged("LoadCertificateComment");
                    ViewCertiList.View.Refresh();


                }
                catch (Exception ex)
                {
                    sc.ErrorLog(ex);
                    allSelectedCerti = value;
                }


            }
        }

        private bool allSelectedOffi;
        public bool AllSelectedOffi
        {
            get
            {
                return allSelectedOffi;
            }
            set
            {
                try
                {
                    var num = CurrentPageOffiIndex * 5;
                    allSelectedOffi = value;
                    loadOfficeComment.Skip(num).ToList().ForEach(x => x.IsCheckedV = value);

                    var bb = loadOfficeComment.ToList();
                    loadOfficeComment.Clear();
                    bb.ToList().ForEach(loadOfficeComment.Add);
                    RaisePropertyChanged("LoadOfficeComment");
                    ViewOffiList.View.Refresh();
                }
                catch (Exception ex)
                {
                    sc.ErrorLog(ex);
                    allSelectedOffi = value;
                }
                //RaisePropertyChanged(() => this.LoadOfficeComment);

            }
        }


        private static Nullable<DateTime> _SDateFrom = null;
        public Nullable<DateTime> SDateFrom
        {
            get
            {
                if (_SDateFrom == null)
                    _SDateFrom = start;
                //if (_BtnName == "Go Back")
                //{
                //    populateList((DateTime)_SDateFrom, (DateTime)SDateTo);

                //    CurrentPageIndex = 0;
                //    itemcount = peopleList.Count;
                //    CalculateTotalPages();
                //    ViewList.View.Refresh();
                //}
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

                //if (_BtnName == "Go Back")
                //{
                //    populateList((DateTime)_SDateFrom, (DateTime)_SDateTo);

                //    CurrentPageIndex = 0;
                //    itemcount = peopleList.Count;
                //    CalculateTotalPages();
                //    ViewList.View.Refresh();
                //}
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
        public Nullable<DateTime> DatesTo
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
                RaisePropertyChanged("DatesTo");
            }
        }


        private string _BtnName;
        public string BtnName
        {
            get
            {
                if (_BtnName == null)
                    _BtnName = "Archives";

                return _BtnName;
            }
            set
            {
                _BtnName = value;
                RaisePropertyChanged("BtnName");
            }
        }

        private static string acknowledged;
        public string Acknowledged
        {
            get
            {
                return acknowledged;
            }
            set
            {
                acknowledged = value;
                RaisePropertyChanged("Acknowledged");
            }

        }

        private static string tobeAcknowledged;
        public string TobeAcknowledged
        {
            get
            {
                return tobeAcknowledged;
            }
            set
            {
                tobeAcknowledged = value;
                RaisePropertyChanged("TobeAcknowledged");
            }
        }


        private static string offiacknowledged;
        public string OffiAcknowledged
        {
            get
            {
                return offiacknowledged;
            }
            set
            {
                offiacknowledged = value;
                RaisePropertyChanged("OffiAcknowledged");
            }

        }

        private static string offitobeAcknowledged;
        public string OffiTobeAcknowledged
        {
            get
            {
                return offitobeAcknowledged;
            }
            set
            {
                offitobeAcknowledged = value;
                RaisePropertyChanged("OffiTobeAcknowledged");
            }
        }


        private static string certiacknowledged;
        public string CertiAcknowledged
        {
            get
            {
                return certiacknowledged;
            }
            set
            {
                certiacknowledged = value;
                RaisePropertyChanged("CertiAcknowledged");
            }

        }

        private static string certitobeAcknowledged;
        public string CertiTobeAcknowledged
        {
            get
            {
                return certitobeAcknowledged;
            }
            set
            {
                certitobeAcknowledged = value;
                RaisePropertyChanged("CertiTobeAcknowledged");
            }
        }



        private void GetCertificateList()
        {
            try
            {
                List<CertificateNC> list = CertificationList.ToList();

                DateTime date = DateTime.Now;
                DateTime firstdate = new DateTime(date.Year, date.Month, 1).AddMonths(0);
                DateTime lastdate = firstdate.AddMonths(1).AddDays(-1);

                if (list.Count > 0)
                {
                    int counter = 1;
                    loadCertificateComment.Clear();

                    var list1 = list.Where(x => x.Acknowledge.ToString() == "To be Acknowledged").OrderByDescending(x => x.IssueDate).ToList();
                    var list2 = list.Where(x => x.Acknowledge.ToString() != "To be Acknowledged" && (Convert.ToDateTime(x.IssueDate).Date >= firstdate.Date && Convert.ToDateTime(x.IssueDate).Date <= lastdate.Date)).OrderByDescending(x => x.IssueDate).ToList();

                    certitobeAcknowledged = "(" + list1.Count() + ")";
                    RaisePropertyChanged("CertiTobeAcknowledged");
                    certiacknowledged = "(" + list2.Count() + ")";
                    RaisePropertyChanged("CertiAcknowledged");

                    var finalList = list1.Concat(list2);

                    if (list1.Count > 0)
                    {
                        count = count + 1;
                    }




                    finalList.ToList().ForEach(x => x.Cid = counter++);
                    sc.ObservableCollectionList(loadCertificateComment, finalList.ToList());
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void GetCertificateList(DateTime datefrom, DateTime dateto)
        {
            try
            {
                List<CertificateNC> list = CertificationList.Where(x => (x.IssueDate >= datefrom.Date && x.IssueDate <= dateto.Date)).ToList();

                int counter = 1;
                loadCertificateComment.Clear();

                var list1 = list.Where(x => x.Acknowledge.ToString().Trim() == "To be Acknowledged").OrderByDescending(x => x.IssueDate).ToList();
                var list2 = list.Where(x => x.Acknowledge.ToString().Trim() != "To be Acknowledged").OrderByDescending(x => x.IssueDate).ToList();

                certitobeAcknowledged = "(" + list1.Count() + ")";
                RaisePropertyChanged("CertiTobeAcknowledged");
                certiacknowledged = "(" + list2.Count() + ")"; ;
                RaisePropertyChanged("CertiAcknowledged");

                var finalList = list1.Concat(list2);

                if (list1.Count > 0)
                {
                    count = count + 1;
                }

                finalList.ToList().ForEach(x => x.Cid = counter++);
                sc.ObservableCollectionList(loadCertificateComment, finalList.ToList());

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void GetOfficeList()
        {
            try
            {
                List<CommentofVSClass> list = OfficeList;

                DateTime date = DateTime.Now;
                DateTime firstdate = new DateTime(date.Year, date.Month, 1).AddMonths(0);
                DateTime lastdate = firstdate.AddMonths(1).AddDays(-1);


                int counter = 1;
                loadOfficeComment.Clear();

                if (UserTypeClass.UserTypes == "MASTER")
                {
                    list.ForEach(x => { x.Acknowledge_Status = string.IsNullOrEmpty(x.Acknowledge_Status_MASTER) ? "To be Acknowledged" : x.Acknowledge_Status_MASTER; });

                    var list1 = list.Where(x => x.Acknowledge_Status == "To be Acknowledged").OrderByDescending(x => x.NC_Date).ToList();
                    var list2 = list.Where(x => x.Acknowledge_Status != "To be Acknowledged" && (Convert.ToDateTime(x.NC_Date).Date >= firstdate.Date && Convert.ToDateTime(x.NC_Date).Date <= lastdate.Date)).OrderByDescending(x => x.NC_Date).ToList();

                    offitobeAcknowledged = "(" + list1.Count() + ")";
                    RaisePropertyChanged("OffiTobeAcknowledged");
                    offiacknowledged = "(" + list2.Count() + ")";
                    RaisePropertyChanged("OffiAcknowledged");

                    var finalList = list1.Concat(list2);

                    if (list1.Count > 0)
                    {
                        count = count + 1;
                    }

                    finalList.ToList().ForEach(x => { x.Id = counter++; });
                    sc.ObservableCollectionList(loadOfficeComment, finalList.ToList());
                }
                else if (UserTypeClass.UserTypes == "HOD")
                {
                    list.ForEach(x => { x.Acknowledge_Status = string.IsNullOrEmpty(x.Acknowledge_Status_HOD) ? "To be Acknowledged" : x.Acknowledge_Status_HOD; });

                    var list1 = list.Where(x => x.Acknowledge_Status == "To be Acknowledged").OrderByDescending(x => x.NC_Date).ToList();
                    var list2 = list.Where(x => x.Acknowledge_Status != "To be Acknowledged" && (Convert.ToDateTime(x.NC_Date).Date >= firstdate.Date && Convert.ToDateTime(x.NC_Date).Date <= lastdate.Date)).OrderByDescending(x => x.NC_Date).ToList();

                    offitobeAcknowledged = "(" + list1.Count() + ")";
                    RaisePropertyChanged("OffiTobeAcknowledged");
                    offiacknowledged = "(" + list2.Count() + ")";
                    RaisePropertyChanged("OffiAcknowledged");

                    var finalList = list1.Concat(list2);

                    if (list1.Count > 0)
                    {
                        count = count + 1;
                    }

                    finalList.ToList().ForEach(x => { x.Id = counter++; });
                    sc.ObservableCollectionList(loadOfficeComment, finalList.ToList());
                }
                else
                {
                    list.ForEach(x => { x.Acknowledge_Status = string.IsNullOrEmpty(x.Acknowledge_Status) ? "To be Acknowledged" : x.Acknowledge_Status; });

                    var list1 = list.Where(x => x.Acknowledge_Status == "To be Acknowledged").OrderByDescending(x => x.NC_Date).ToList();
                    var list2 = list.Where(x => x.Acknowledge_Status != "To be Acknowledged" && (Convert.ToDateTime(x.NC_Date).Date >= firstdate.Date && Convert.ToDateTime(x.NC_Date).Date <= lastdate.Date)).OrderByDescending(x => x.NC_Date).ToList();

                    offitobeAcknowledged = "(" + list1.Count() + ")";
                    RaisePropertyChanged("OffiTobeAcknowledged");
                    offiacknowledged = "(" + list2.Count() + ")";
                    RaisePropertyChanged("OffiAcknowledged");

                    var finalList = list1.Concat(list2);

                    if (list1.Count > 0)
                    {
                        count = count + 1;
                    }

                    finalList.ToList().ForEach(x => { x.Id = counter++; });
                    sc.ObservableCollectionList(loadOfficeComment, finalList.ToList());
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void GetOfficeList(DateTime datefrom, DateTime dateto)
        {
            try
            {
                List<CommentofVSClass> list = OfficeList.Where(x => (x.NC_Date >= datefrom.Date && x.NC_Date <= dateto.Date)).ToList();

                int counter = 1;
                loadOfficeComment.Clear();

                if (UserTypeClass.UserTypes == "MASTER")
                {
                    list.ForEach(x => { x.Acknowledge_Status = string.IsNullOrEmpty(x.Acknowledge_Status_MASTER) ? "To be Acknowledged" : x.Acknowledge_Status_MASTER; });

                    var list1 = list.Where(x => x.Acknowledge_Status == "To be Acknowledged").OrderByDescending(x => x.NC_Date).ToList();
                    var list2 = list.Where(x => x.Acknowledge_Status != "To be Acknowledged").OrderByDescending(x => x.NC_Date).ToList();

                    offitobeAcknowledged = "(" + list1.Count() + ")";
                    RaisePropertyChanged("OffiTobeAcknowledged");
                    offiacknowledged = "(" + list2.Count() + ")";
                    RaisePropertyChanged("OffiAcknowledged");

                    var finalList = list1.Concat(list2);

                    if (list1.Count > 0)
                    {
                        count = count + 1;
                    }

                    finalList.ToList().ForEach(x => { x.Id = counter++; });
                    sc.ObservableCollectionList(loadOfficeComment, finalList.ToList());
                }
                else if (UserTypeClass.UserTypes == "HOD")
                {
                    list.ForEach(x => { x.Acknowledge_Status = string.IsNullOrEmpty(x.Acknowledge_Status_HOD) ? "To be Acknowledged" : x.Acknowledge_Status_HOD; });

                    var list1 = list.Where(x => x.Acknowledge_Status == "To be Acknowledged").OrderByDescending(x => x.NC_Date).ToList();
                    var list2 = list.Where(x => x.Acknowledge_Status != "To be Acknowledged").OrderByDescending(x => x.NC_Date).ToList();

                    offitobeAcknowledged = "(" + list1.Count() + ")";
                    RaisePropertyChanged("OffiTobeAcknowledged");
                    offiacknowledged = "(" + list2.Count() + ")";
                    RaisePropertyChanged("OffiAcknowledged");

                    var finalList = list1.Concat(list2);

                    if (list1.Count > 0)
                    {
                        count = count + 1;
                    }

                    finalList.ToList().ForEach(x => { x.Id = counter++; });
                    sc.ObservableCollectionList(loadOfficeComment, finalList.ToList());
                }
                else
                {
                    list.ForEach(x => { x.Acknowledge_Status = string.IsNullOrEmpty(x.Acknowledge_Status) ? "To be Acknowledged" : x.Acknowledge_Status; });


                    var list1 = list.Where(x => x.Acknowledge_Status.Trim() == "To be Acknowledged").OrderByDescending(x => x.NC_Date).ToList();
                    var list2 = list.Where(x => x.Acknowledge_Status.Trim() != "To be Acknowledged").OrderByDescending(x => x.NC_Date).ToList();

                    offitobeAcknowledged = "(" + list1.Count() + ")";
                    RaisePropertyChanged("OffiTobeAcknowledged");
                    offiacknowledged = "(" + list2.Count() + ")";
                    RaisePropertyChanged("OffiAcknowledged");

                    var finalList = list1.Concat(list2);
                    if (list1.Count > 0)
                    {
                        count = count + 1;
                    }

                    finalList.ToList().ForEach(x => { x.Id = counter++; });
                    sc.ObservableCollectionList(loadOfficeComment, finalList.ToList());
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }


        private void GetDeviationList()
        {
            try
            {
                List<WorkHoursNC> list = DeviationList;

                DateTime date = DateTime.Now;
                DateTime firstdate = new DateTime(date.Year, date.Month, 1).AddMonths(0);
                DateTime lastdate = firstdate.AddMonths(1).AddDays(-1);


                int counter = 1;
                peopleList.Clear();

                var list1 = list.Where(x => x.Acknowledge.ToString().Trim() == "To be Acknowledged").OrderByDescending(x => x.Dates).ToList();
                var list2 = list.Where(x => x.Acknowledge.ToString().Trim() != "To be Acknowledged" && (Convert.ToDateTime(x.Dates).Date >= firstdate.Date && Convert.ToDateTime(x.Dates).Date <= lastdate.Date)).OrderByDescending(x => x.Dates).ToList();

                tobeAcknowledged = "(" + list1.Count() + ")";
                RaisePropertyChanged("TobeAcknowledged");
                acknowledged = "(" + list2.Count() + ")"; ;
                RaisePropertyChanged("Acknowledged");

                var finalList = list1.Concat(list2);

                if (list1.Count > 0)
                {
                    count = count + 1;
                }

                finalList.ToList().ForEach(x => x.Id = counter++);
                sc.ObservableCollectionList(peopleList, finalList.ToList());

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void GetDeviationList(DateTime datefrom, DateTime dateto)
        {
            try
            {
                List<WorkHoursNC> list = DeviationList.Where(x => (x.Dates >= datefrom.Date && x.Dates <= dateto.Date)).ToList();

                int counter = 1;
                peopleList.Clear();

                var list1 = list.Where(x => x.Acknowledge.ToString().Trim() == "To be Acknowledged").OrderByDescending(x => x.Dates).ToList();
                var list2 = list.Where(x => x.Acknowledge.ToString().Trim() != "To be Acknowledged").OrderByDescending(x => x.Dates).ToList();

                tobeAcknowledged = "(" + list1.Count() + ")";
                RaisePropertyChanged("TobeAcknowledged");
                acknowledged = "(" + list2.Count() + ")"; ;
                RaisePropertyChanged("Acknowledged");

                var finalList = list1.Concat(list2);

                if (list1.Count > 0)
                {
                    count = count + 1;
                }

                finalList.ToList().ForEach(x => x.Id = counter++);
                sc.ObservableCollectionList(peopleList, finalList.ToList());
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }

}
