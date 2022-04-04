using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using WorkShipVersionII.Commands;
using WorkShipVersionII.Views;


namespace WorkShipVersionII.ViewModel
{
    public class NotificationsViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private int itemPerPage = 10;
        private int itemcount;

        // static StudentModel StudentList = new StudentModel();



        public ICommand HelpCommand { get; private set; }
        //private int itemPerPageCerti = 5;
        //private int itemcountCerti;

        //private int itemPerPageOffi = 5;
        //private int itemcountOffi;
        int count = 0;
        public NotificationsViewModel(string refresh)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();

                sc.Configuration.ProxyCreationEnabled = false;
            }

            DataTable dd = sc.dynamicCheckMinimumRopeandTails();
            if (dd.Rows.Count > 0)
            {
                StaticHelper.MinimumRope = Convert.ToInt32(dd.Rows[0]["MinimumRopes"]);
                StaticHelper.MinimumRopeTail = Convert.ToInt32(dd.Rows[0]["MinimumRopeTails"]);
            }

            AcknowledgeCommand = new RelayCommand(AcknowledgeMethod);
            ArchivesCommand = new RelayCommand<object>(ArchivesMethod);
            GoCommand = new RelayCommand(Searchmethod);


            commentsCommand = new RelayCommand<NotificationsClass>(ViewComments);

            commentsCommand1 = new RelayCommand<NotificationsClass>(ViewComments1);


            GetNotificationList();




            NextCommand = new NextPageCommand(this);
            PreviousCommand = new PreviousPageCommand(this);
            FirstCommand = new FirstPageCommand(this);
            LastCommand = new LastPageCommand(this);

            ViewList.View.Refresh();


        }



        public NotificationsViewModel(ObservableCollection<NotificationsClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            DataTable dd = sc.dynamicCheckMinimumRopeandTails();
            if (dd.Rows.Count > 0)
            {
                StaticHelper.MinimumRope = Convert.ToInt32(dd.Rows[0]["MinimumRopes"]);
                StaticHelper.MinimumRopeTail = Convert.ToInt32(dd.Rows[0]["MinimumRopeTails"]);
            }

            LoadUserAccess.Clear();

            GetNotificationList();


            // var cnt = loadUserAccess.Count;
            CurrentPageIndex = 0;
            //itemcount = loadUserAccess.Count / 2;
            itemcount = loadUserAccess.Count();
            CalculateTotalPages();

            NextCommand = new NextPageCommand(this);
            PreviousCommand = new PreviousPageCommand(this);
            FirstCommand = new FirstPageCommand(this);
            LastCommand = new LastPageCommand(this);

            ViewList.View.Refresh();

        }
        public NotificationsViewModel()
        {
            try
            {


                if (sc == null)
                {
                    sc = new ShipmentContaxt();
                    // sc1 = new NotificationContaxt();
                    sc.Configuration.ProxyCreationEnabled = false;
                }

                DataTable dd = sc.dynamicCheckMinimumRopeandTails();
                if (dd.Rows.Count > 0)
                {
                    StaticHelper.MinimumRope = Convert.ToInt32(dd.Rows[0]["MinimumRopes"]);
                    StaticHelper.MinimumRopeTail = Convert.ToInt32(dd.Rows[0]["MinimumRopeTails"]);
                }



                //*********** Notification Timer and After Login****************** 
                DispatcherTimer timer = new DispatcherTimer();
                //timer.Interval = TimeSpan.FromSeconds(3);
                timer.Interval = TimeSpan.FromHours(24);
                //timer.Tick += timer_Tick;                   //Rope Replacement and Rope Rotation
                timer.Tick += timer_Tick1;                    // Inspection due 7 or 1 days for Rope and RopeTail
                timer.Tick += timer_Tick2_Loose_Equipment;                   // JoiningShackle , Loose Equipment
                timer.Tick += timer_Tick_Discard;              // Rope and RopeTail DisCard Required 
                timer.Tick += timer_Tick_EndtoEnd;
                timer.Tick += timer_Tick_WinchRotation;            // WinchRotation Required for Ropes
                timer.Start();

                InspectionDue7daysOROverdue();
                NorificationRopeDiscard();
                NotificationEndToEnd();
                LooseEquipmentNoti();
                WinchrotationSetting_UpdateRunningHours();
                //*********** Notification Timer and After Login****************** 

                AcknowledgeCommand = new RelayCommand(AcknowledgeMethod);
                ArchivesCommand = new RelayCommand<object>(ArchivesMethod);
                GoCommand = new RelayCommand(Searchmethod);

                //StaticHelper.HelpFor = @"General\2.0 Notification\2.1 NOTIFICATIONS.htm";
                //StaticHelper.HelpFor = @"2.1 NOTIFICATIONS.htm";
                //StaticHelper.HelpFor = @"Notification\3.0 CREW MANAGEMENT\3.0.2 ADD CREW.htm";
                HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

                commentsCommand = new RelayCommand<NotificationsClass>(ViewComments);

                commentsCommand1 = new RelayCommand<NotificationsClass>(ViewComments1);


                GetNotificationList();



                ViewList = new CollectionViewSource
                {
                    Source = loadUserAccess
                };
                ViewList.Filter += new FilterEventHandler(View_Filter);

                CurrentPageIndex = 0;

                CurrentPage = CurrentPageIndex;
                RaisePropertyChanged("CurrentPage");
                RaisePropertyChanged("CurrentPageIndex");

                itemcount = loadUserAccess.Count;
                CalculateTotalPages();

                NextCommand = new NextPageCommand(this);
                PreviousCommand = new PreviousPageCommand(this);
                FirstCommand = new FirstPageCommand(this);
                LastCommand = new LastPageCommand(this);

                ViewList.View.Refresh();




            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private ICommand commentsCommand;
        public ICommand CommentsCommand
        {
            get { return commentsCommand; }
            set { commentsCommand = value; }
        }
        private ICommand commentsCommand1;
        public ICommand CommentsCommand1
        {
            get { return commentsCommand1; }
            set { commentsCommand1 = value; }
        }


        public void refreshdata()
        {
            try
            {

                GetNotificationList();

                AcknowledgeCommand = new RelayCommand(AcknowledgeMethod);
                ArchivesCommand = new RelayCommand<object>(ArchivesMethod);
                GoCommand = new RelayCommand(Searchmethod);


                commentsCommand = new RelayCommand<NotificationsClass>(ViewComments);

                commentsCommand1 = new RelayCommand<NotificationsClass>(ViewComments1);
                NextCommand = new NextPageCommand(this);
                PreviousCommand = new PreviousPageCommand(this);
                FirstCommand = new FirstPageCommand(this);
                LastCommand = new LastPageCommand(this);


                ViewList = new CollectionViewSource
                {
                    Source = LoadUserAccess
                };
                ViewList.Filter += new FilterEventHandler(View_Filter);


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void ViewComments(NotificationsClass mw)
        {
            //NotificationCommentsViewModel vm = new NotificationCommentsViewModel(mw.Id, 1);
            NotificationCommentsViewModel vm = new NotificationCommentsViewModel(mw.IndexId, 1);
            ChildWindowManager.Instance.ShowChildWindow(new NotificationCommentsView() { DataContext = vm });
        }

        private void ViewComments1(NotificationsClass mw)
        {
            //NotificationCommentsViewModel vm = new NotificationCommentsViewModel(mw.Id, 2);
            if (mw.OfficeCmnt != "No Comment(s)")
            {
                NotificationCommentsViewModel vm = new NotificationCommentsViewModel(mw.IndexId, 2);
                ChildWindowManager.Instance.ShowChildWindow(new NotificationCommentsView() { DataContext = vm });
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

                var abc = LoadUserAccess.Where(x => x.IsCheckedV == true).ToList();

                //office = abc.Where(x => x.IsCheckedV && x.Acknowledge_Status == "To be Acknowledged").ToList();
                string dt = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
                abc.ForEach(x => { x.AckRecord = "Acknowledged on " + dt + " Hours"; x.Acknowledge = true; });



               // string sqry = "update Notifications set AckRecord=@AckRecord, Acknowledge = @Acknowledge where id=@id and NotificationType = @NotificationType";
                string sqry = "UpdateNotifications";
                if (sc.con.State == ConnectionState.Closed)
                {
                    sc.con.Open();
                }
                foreach (var item in abc)
                {
                    int[] notAckid = { 6, 8, 10, 17, 18, 43 };
                    //if (item.NotificationAlertType == 17 || item.NotificationAlertType == 18)
                    if (notAckid.Contains(item.NotificationAlertType) == true)
                    {
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand(sqry, sc.con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AckRecord", item.AckRecord);
                        cmd.Parameters.AddWithValue("@Acknowledge", item.Acknowledge);
                        cmd.Parameters.AddWithValue("@id", item.IndexId);
                        cmd.Parameters.AddWithValue("@NotificationType", item.NotificationType);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }


                }

                //LoadUserAccess.Clear();
                //LoadUserAccess = GetNotificationList();
                GetNotificationList();



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
                    CurrentPageIndex = 0;
                    GetNotificationListArchive((DateTime)SDateFrom, (DateTime)SDateTo);





                }
                else
                {
                    _BtnName = "Archives";
                    CurrentPageIndex = 0;
                    GetNotificationList();



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
                    GetNotificationListArchive((DateTime)SDateFrom, (DateTime)SDateTo);


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

        //public static CollectionViewSource ViewList { get; set; }

        private CollectionViewSource _ViewList = new CollectionViewSource();
        public CollectionViewSource ViewList
        {
            get { return _ViewList; }
            set
            {
                _ViewList = value;
                RaisePropertyChanged("ViewList");
            }
        }
        private static ObservableCollection<NotificationsClass> loadUserAccess = new ObservableCollection<NotificationsClass>();
        public ObservableCollection<NotificationsClass> LoadUserAccess
        {
            get
            {
                return loadUserAccess;
            }
            set
            {
                loadUserAccess = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
            }
        }


        public System.Data.DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            System.Data.DataTable table = new System.Data.DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in Linqlist)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }

            return table;
        }

        public static List<NotificationsClass> NotificationList = new List<NotificationsClass>();
        public void GetNotificationList()
        {

            try
            {
                //ObservableCollection<NotificationsClass> notilist = new ObservableCollection<NotificationsClass>();

                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                //loadUserAccess.Clear();

                // int rowid = 1;

                NotificationList.Clear();
                loadUserAccess.Clear();


                // RaisePropertyChanged("LoadUserAccess");
                SqlCommand cmd = new SqlCommand("NotificationsListingkk", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@DateFrom", "");
                //cmd.Parameters.AddWithValue("@DateTo", "");
                //cmd.Parameters.AddWithValue("@Action", "");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                int counter = 1;
                foreach (DataRow row in ds.Rows)
                {
                    int NotifiAlertType = Convert.ToInt32(row["NotificationAlertType"] == DBNull.Value ? null : row["NotificationAlertType"]);

                    int RopeId = Convert.ToInt32(row["RopeId"] == DBNull.Value ? null : row["RopeId"]);

                    //int NotifiAlertType = Convert.ToInt32( (int)row["NotificationAlertType"]== DBNull.Value ? 0 : (int)row["NotificationAlertType"]);
                    object NotificationDueDate = row["NotificationDueDate"];
                    string certifiNumber = (row["LooseCertificateNum"] == DBNull.Value) ? "" : row["LooseCertificateNum"].ToString();

                    if (RopeId == 0)
                    {
                        //DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-7);
                        //if (due <= DateTime.Now.Date)
                        NotificationList.Add(new NotificationsClass()
                        {
                            //  RowId = rowid++,
                            Id = (int)row["Id"],
                            IndexId = (int)row["Id"],
                            RopeId = (int)row["RopeId"],
                            Notification = (string)row["Notification"],
                            NotificationType = (int)row["NotificationType"],
                            Acknowledge = (bool)row["Acknowledge"],
                            AckRecord = (string)row["AckRecord"],
                            NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                            IsCheckedV = false,
                            NotificationAlertType = NotifiAlertType,
                            LooseCertificateNum = certifiNumber,
                            OfficeCmnt = (string)row["OfficeCmnt"],
                            ShipCmnt = (string)row["ShipCmnt"],

                            //NotificationLogic = (string)row["NotificationLogic"],
                        });
                    }
                }
                foreach (DataRow row in ds.Rows)
                {

                    int NotifiAlertType = Convert.ToInt32(row["NotificationAlertType"] == DBNull.Value ? null : row["NotificationAlertType"]);

                    int RopeId = Convert.ToInt32(row["RopeId"] == DBNull.Value ? null : row["RopeId"]);

                    //int NotifiAlertType = Convert.ToInt32( (int)row["NotificationAlertType"]== DBNull.Value ? 0 : (int)row["NotificationAlertType"]);
                    object NotificationDueDate = row["NotificationDueDate"];
                    string certifiNumber = (row["LooseCertificateNum"] == DBNull.Value) ? "" : row["LooseCertificateNum"].ToString();

                    if (RopeId == 0)
                    {
                        //DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-7);
                        //if (due <= DateTime.Now.Date)
                        //NotificationList.Add(new NotificationsClass()
                        //{
                        //    Id = (int)row["Id"],
                        //    IndexId = (int)row["Id"],
                        //    RopeId = (int)row["RopeId"],
                        //    Notification = (string)row["Notification"],
                        //    NotificationType = (int)row["NotificationType"],
                        //    Acknowledge = (bool)row["Acknowledge"],
                        //    AckRecord = (string)row["AckRecord"],
                        //    NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                        //    IsCheckedV = false,
                        //    NotificationAlertType = NotifiAlertType,
                        //    LooseCertificateNum = certifiNumber,
                        //    OfficeCmnt = (string)row["OfficeCmnt"],
                        //    ShipCmnt = (string)row["ShipCmnt"],

                        //    //NotificationLogic = (string)row["NotificationLogic"],
                        //});
                    }
                    else if (NotifiAlertType == 1 & NotificationDueDate != null)
                    {
                        DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-7);
                        if (due <= DateTime.Now.Date)
                            NotificationList.Add(new NotificationsClass()
                            {
                                // RowId = rowid++,
                                Id = (int)row["Id"],
                                IndexId = (int)row["Id"],
                                RopeId = (int)row["RopeId"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                NotificationAlertType = NotifiAlertType,
                                LooseCertificateNum = certifiNumber,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],

                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                    }
                    else if (NotifiAlertType == 2 & NotificationDueDate != null)
                    {
                        DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-1);
                        if (due <= DateTime.Now.Date)
                            NotificationList.Add(new NotificationsClass()
                            {
                                // RowId = rowid++,
                                Id = (int)row["Id"],
                                IndexId = (int)row["Id"],
                                RopeId = (int)row["RopeId"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                NotificationAlertType = NotifiAlertType,
                                LooseCertificateNum = certifiNumber,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],
                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                    }
                    else if (NotifiAlertType == 3 & NotificationDueDate != null)
                    {
                        DateTime due = Convert.ToDateTime(NotificationDueDate);
                        if (due <= DateTime.Now.Date)
                            NotificationList.Add(new NotificationsClass()
                            {
                                //  RowId = rowid++,
                                Id = (int)row["Id"],
                                IndexId = (int)row["Id"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],
                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                    }
                    else
                    {

                        //  NotificationAlertType
                        if (NotifiAlertType == 21 & NotificationDueDate != null)
                        {
                            DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-7);
                            if (due <= DateTime.Now.Date)
                                NotificationList.Add(new NotificationsClass()
                                {
                                    // RowId = rowid++,
                                    Id = (int)row["Id"],
                                    IndexId = (int)row["Id"],
                                    RopeId = (int)row["RopeId"],
                                    Notification = (string)row["Notification"],
                                    NotificationType = (int)row["NotificationType"],
                                    Acknowledge = (bool)row["Acknowledge"],
                                    AckRecord = (string)row["AckRecord"],
                                    NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                    IsCheckedV = false,
                                    NotificationAlertType = NotifiAlertType,
                                    LooseCertificateNum = certifiNumber,
                                    OfficeCmnt = (string)row["OfficeCmnt"],
                                    ShipCmnt = (string)row["ShipCmnt"],
                                    //NotificationLogic = (string)row["NotificationLogic"],
                                });
                        }
                        else if (NotifiAlertType == 22 & NotificationDueDate != null)
                        {
                            DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-1);
                            if (due <= DateTime.Now.Date)
                                NotificationList.Add(new NotificationsClass()
                                {
                                    //  RowId = rowid++,
                                    Id = (int)row["Id"],
                                    IndexId = (int)row["Id"],
                                    RopeId = (int)row["RopeId"],
                                    Notification = (string)row["Notification"],
                                    NotificationType = (int)row["NotificationType"],
                                    Acknowledge = (bool)row["Acknowledge"],
                                    AckRecord = (string)row["AckRecord"],
                                    NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                    IsCheckedV = false,
                                    NotificationAlertType = NotifiAlertType,
                                    LooseCertificateNum = certifiNumber,
                                    OfficeCmnt = (string)row["OfficeCmnt"],
                                    ShipCmnt = (string)row["ShipCmnt"],
                                    //NotificationLogic = (string)row["NotificationLogic"],
                                });
                        }
                        else if (NotifiAlertType == 23 & NotificationDueDate != null)
                        {
                            DateTime due = Convert.ToDateTime(NotificationDueDate);
                            if (due <= DateTime.Now.Date)
                                NotificationList.Add(new NotificationsClass()
                                {
                                    // RowId = rowid++,
                                    Id = (int)row["Id"],
                                    IndexId = (int)row["Id"],
                                    Notification = (string)row["Notification"],
                                    NotificationType = (int)row["NotificationType"],
                                    Acknowledge = (bool)row["Acknowledge"],
                                    AckRecord = (string)row["AckRecord"],
                                    NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                    IsCheckedV = false,
                                    LooseCertificateNum = certifiNumber,
                                    OfficeCmnt = (string)row["OfficeCmnt"],
                                    ShipCmnt = (string)row["ShipCmnt"],
                                    //NotificationLogic = (string)row["NotificationLogic"],
                                });
                        }
                        else
                        {
                            NotificationList.Add(new NotificationsClass()
                            {
                                //RowId = rowid++,
                                Id = (int)row["Id"],
                                IndexId = (int)row["Id"],
                                RopeId = (int)row["RopeId"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                NotificationAlertType = NotifiAlertType,
                                LooseCertificateNum = certifiNumber,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],
                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                        }
                    }

                }

                List<NotificationsClass> list = NotificationList;
                // var tables = LINQResultToDataTable(list);

                loadUserAccess.Clear();

                string Ropes = "Please insert minimum of {" + StaticHelper.MinimumRope + "} active lines"; string Tails = "Please insert minimum of {" + StaticHelper.MinimumRopeTail + "} active rope tails";
                // var list1 = list.Where(x => x.AckRecord.ToString().Trim() == "Not yet acknowledged").OrderByDescending(x => x.Id).ToList();
                // var list2 = list.Where(x => x.AckRecord.ToString().Trim() != "Not yet acknowledged").OrderByDescending(x => x.Id).ToList();





                int RedAck = 0;
                string Redqry = " select * from Notifications where (AckRecord = 'Not yet acknowledged' or AckRecord = 'This notification cannot be acknowledged, kindly discard it' or NotificationAlertType = 17 or NotificationAlertType=18) and isactive=1";
                using (SqlDataAdapter sda = new SqlDataAdapter(Redqry, sc.con))
                {
                    DataTable dtprr = new DataTable();
                    sda.Fill(dtprr);
                    if (dtprr.Rows.Count > 0)
                        RedAck = dtprr.Rows.Count;
                }

                tobeAcknowledged = "(" + RedAck + ")";
                RaisePropertyChanged("TobeAcknowledged");
                // acknowledged = "(" + list2.Count() + ")"; 




                int Ack = 0;
                string qry = "select * from Notifications where  AckRecord like 'Acknowledged on%' and isactive=1";
                using (SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con))
                {
                    DataTable dtprr = new DataTable();
                    sda.Fill(dtprr);
                    if (dtprr.Rows.Count > 0)
                        Ack = dtprr.Rows.Count;
                }

                acknowledged = "(" + Ack + ")";
                RaisePropertyChanged("Acknowledged");
                //================================================

                var unrdcmnt = sc.NotificationCommnent.Where(x => x.IsRead == false && x.CommentsType == 2).ToList();
                ttlunreadOfccmnt = "(" + unrdcmnt.Count() + ")";
                RaisePropertyChanged("TotalUnreadOfcCmnt");

                list.ForEach(x =>
                {
                    if (x.NotificationAlertType == 17)
                        x.AckRecord = Ropes;
                    if (x.NotificationAlertType == 18)
                        x.AckRecord = Tails;
                });

                var finalList = list; //list1.Concat(list2);
                                      // var finalList = list2.Concat(list1);

                //if (list1.Count > 0)
                //{
                //    count = count + 1;
                //}

                if (finalList.Count > 0 && StaticHelper.AlarmCheck == true)
                {
                    //StaticHelper.AlarmCheck = false;
                    StaticHelper.AlarmFunction(finalList.Count, StaticHelper.AlarmCheck);
                }



                finalList.ToList().ForEach(x => x.Id = counter++);
                //var tables = LINQResultToDataTable(finalList);
                sc.ObservableCollectionList(loadUserAccess, finalList.ToList());


                var ackcheck = sc.Notifications.Where(x => x.AckRecord == "Not yet acknowledged" & x.IsActive == true).Count();

                if (ackcheck > 0)
                {

                    MainViewModel.colorchange = "Hidden";
                    MainViewModel.colorchangeRed = "Visible";

                    OnPropertyChanged("ColorChange");
                    OnPropertyChanged("ColorChangeRed");

                    RaisePropertyChanged("ColorChange");
                    RaisePropertyChanged("ColorChangeRed");
                }
                else
                {
                    MainViewModel.colorchange = "Visible";
                    MainViewModel.colorchangeRed = "Hidden";


                    //MainViewModel.colorchange = new CollectionViewSource();
                    //MainViewModel.colorchange.Source = "Visible";

                    //MainViewModel.colorchangeRed = new CollectionViewSource();
                    //MainViewModel.colorchangeRed.Source = "Hidden";


                    OnPropertyChanged("ColorChange");
                    OnPropertyChanged("ColorChangeRed");

                    RaisePropertyChanged("ColorChange");
                    RaisePropertyChanged("ColorChangeRed");
                }




                ViewList = new CollectionViewSource
                {
                    Source = LoadUserAccess
                };


                ViewList.Filter += new FilterEventHandler(View_Filter);


                itemcount = loadUserAccess.Count(); //sc.Notifications.Count();
                CalculateTotalPages();
                ViewList.View.Refresh();

                //itemcount = 25;
                //CalculateTotalPages();
                //ViewList.View.Refresh();
            }
            catch (Exception ex)
            {
                //sc.ErrorLog(ex);

            }

        }



        public event PropertyChangedEventHandler PropertyChanged1;
        public virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged1;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void GetNotificationListArchive(DateTime DFrom, DateTime DTo)
        {

            try
            {
                // int rowid = 1;
                NotificationList.Clear();
                loadUserAccess.Clear();


                // RaisePropertyChanged("LoadUserAccess");
                SqlCommand cmd = new SqlCommand("NotificationsListingkk_Archive", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DateFrom", DFrom);
                cmd.Parameters.AddWithValue("@DateTo", DTo);
                //cmd.Parameters.AddWithValue("@Action", "");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                int counter = 1;
                foreach (DataRow row in ds.Rows)
                {
                    int NotifiAlertType = Convert.ToInt32(row["NotificationAlertType"] == DBNull.Value ? null : row["NotificationAlertType"]);

                    int RopeId = Convert.ToInt32(row["RopeId"] == DBNull.Value ? null : row["RopeId"]);

                    //int NotifiAlertType = Convert.ToInt32( (int)row["NotificationAlertType"]== DBNull.Value ? 0 : (int)row["NotificationAlertType"]);
                    object NotificationDueDate = row["NotificationDueDate"];
                    string certifiNumber = (row["LooseCertificateNum"] == DBNull.Value) ? "" : row["LooseCertificateNum"].ToString();

                    if (RopeId == 0)
                    {
                        //DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-7);
                        //if (due <= DateTime.Now.Date)
                        NotificationList.Add(new NotificationsClass()
                        {
                            // RowId = rowid++,
                            Id = (int)row["Id"],
                            IndexId = (int)row["Id"],
                            RopeId = (int)row["RopeId"],
                            Notification = (string)row["Notification"],
                            NotificationType = (int)row["NotificationType"],
                            Acknowledge = (bool)row["Acknowledge"],
                            AckRecord = (string)row["AckRecord"],
                            NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                            IsCheckedV = false,
                            NotificationAlertType = NotifiAlertType,
                            LooseCertificateNum = certifiNumber,
                            OfficeCmnt = (string)row["OfficeCmnt"],
                            ShipCmnt = (string)row["ShipCmnt"],

                            //NotificationLogic = (string)row["NotificationLogic"],
                        });
                    }
                }
                foreach (DataRow row in ds.Rows)
                {

                    int NotifiAlertType = Convert.ToInt32(row["NotificationAlertType"] == DBNull.Value ? null : row["NotificationAlertType"]);

                    int RopeId = Convert.ToInt32(row["RopeId"] == DBNull.Value ? null : row["RopeId"]);

                    //int NotifiAlertType = Convert.ToInt32( (int)row["NotificationAlertType"]== DBNull.Value ? 0 : (int)row["NotificationAlertType"]);
                    object NotificationDueDate = row["NotificationDueDate"];
                    string certifiNumber = (row["LooseCertificateNum"] == DBNull.Value) ? "" : row["LooseCertificateNum"].ToString();

                    if (RopeId == 0)
                    {
                        //DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-7);
                        //if (due <= DateTime.Now.Date)
                        //NotificationList.Add(new NotificationsClass()
                        //{
                        //    Id = (int)row["Id"],
                        //    IndexId = (int)row["Id"],
                        //    RopeId = (int)row["RopeId"],
                        //    Notification = (string)row["Notification"],
                        //    NotificationType = (int)row["NotificationType"],
                        //    Acknowledge = (bool)row["Acknowledge"],
                        //    AckRecord = (string)row["AckRecord"],
                        //    NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                        //    IsCheckedV = false,
                        //    NotificationAlertType = NotifiAlertType,
                        //    LooseCertificateNum = certifiNumber,
                        //    OfficeCmnt = (string)row["OfficeCmnt"],
                        //    ShipCmnt = (string)row["ShipCmnt"],

                        //    //NotificationLogic = (string)row["NotificationLogic"],
                        //});
                    }
                    else if (NotifiAlertType == 1 & NotificationDueDate != null)
                    {
                        DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-7);
                        if (due <= DateTime.Now.Date)
                            NotificationList.Add(new NotificationsClass()
                            {
                                // RowId = rowid++,
                                Id = (int)row["Id"],
                                IndexId = (int)row["Id"],
                                RopeId = (int)row["RopeId"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                NotificationAlertType = NotifiAlertType,
                                LooseCertificateNum = certifiNumber,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],

                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                    }
                    else if (NotifiAlertType == 2 & NotificationDueDate != null)
                    {
                        DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-1);
                        if (due <= DateTime.Now.Date)
                            NotificationList.Add(new NotificationsClass()
                            {
                                // RowId = rowid++,
                                Id = (int)row["Id"],
                                IndexId = (int)row["Id"],
                                RopeId = (int)row["RopeId"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                NotificationAlertType = NotifiAlertType,
                                LooseCertificateNum = certifiNumber,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],
                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                    }
                    else if (NotifiAlertType == 3 & NotificationDueDate != null)
                    {
                        DateTime due = Convert.ToDateTime(NotificationDueDate);
                        if (due <= DateTime.Now.Date)
                            NotificationList.Add(new NotificationsClass()
                            {
                                // RowId = rowid++,
                                Id = (int)row["Id"],
                                IndexId = (int)row["Id"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],
                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                    }
                    else
                    {

                        //  NotificationAlertType
                        if (NotifiAlertType == 21 & NotificationDueDate != null)
                        {
                            DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-7);
                            if (due <= DateTime.Now.Date)
                                NotificationList.Add(new NotificationsClass()
                                {
                                    //  RowId = rowid++,
                                    Id = (int)row["Id"],
                                    IndexId = (int)row["Id"],
                                    RopeId = (int)row["RopeId"],
                                    Notification = (string)row["Notification"],
                                    NotificationType = (int)row["NotificationType"],
                                    Acknowledge = (bool)row["Acknowledge"],
                                    AckRecord = (string)row["AckRecord"],
                                    NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                    IsCheckedV = false,
                                    NotificationAlertType = NotifiAlertType,
                                    LooseCertificateNum = certifiNumber,
                                    OfficeCmnt = (string)row["OfficeCmnt"],
                                    ShipCmnt = (string)row["ShipCmnt"],
                                    //NotificationLogic = (string)row["NotificationLogic"],
                                });
                        }
                        else if (NotifiAlertType == 22 & NotificationDueDate != null)
                        {
                            DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-1);
                            if (due <= DateTime.Now.Date)
                                NotificationList.Add(new NotificationsClass()
                                {
                                    // RowId = rowid++,
                                    Id = (int)row["Id"],
                                    IndexId = (int)row["Id"],
                                    RopeId = (int)row["RopeId"],
                                    Notification = (string)row["Notification"],
                                    NotificationType = (int)row["NotificationType"],
                                    Acknowledge = (bool)row["Acknowledge"],
                                    AckRecord = (string)row["AckRecord"],
                                    NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                    IsCheckedV = false,
                                    NotificationAlertType = NotifiAlertType,
                                    LooseCertificateNum = certifiNumber,
                                    OfficeCmnt = (string)row["OfficeCmnt"],
                                    ShipCmnt = (string)row["ShipCmnt"],
                                    //NotificationLogic = (string)row["NotificationLogic"],
                                });
                        }
                        else if (NotifiAlertType == 23 & NotificationDueDate != null)
                        {
                            DateTime due = Convert.ToDateTime(NotificationDueDate);
                            if (due <= DateTime.Now.Date)
                                NotificationList.Add(new NotificationsClass()
                                {
                                    // RowId = rowid++,
                                    Id = (int)row["Id"],
                                    IndexId = (int)row["Id"],
                                    Notification = (string)row["Notification"],
                                    NotificationType = (int)row["NotificationType"],
                                    Acknowledge = (bool)row["Acknowledge"],
                                    AckRecord = (string)row["AckRecord"],
                                    NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                    IsCheckedV = false,
                                    LooseCertificateNum = certifiNumber,
                                    OfficeCmnt = (string)row["OfficeCmnt"],
                                    ShipCmnt = (string)row["ShipCmnt"],
                                    //NotificationLogic = (string)row["NotificationLogic"],
                                });
                        }
                        else
                        {
                            NotificationList.Add(new NotificationsClass()
                            {
                                // RowId = rowid++,
                                Id = (int)row["Id"],
                                IndexId = (int)row["Id"],
                                RopeId = (int)row["RopeId"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                NotificationAlertType = NotifiAlertType,
                                LooseCertificateNum = certifiNumber,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],
                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                        }
                    }

                }

                List<NotificationsClass> list = NotificationList;

                loadUserAccess.Clear();

                string Ropes = "Please insert minimum of {" + StaticHelper.MinimumRope + "} active lines"; string Tails = "Please insert minimum of {" + StaticHelper.MinimumRopeTail + "} active rope tails";
                // var list1 = list.Where(x => x.AckRecord.ToString().Trim() == "Not yet acknowledged").OrderByDescending(x => x.Id).ToList();
                // var list2 = list.Where(x => x.AckRecord.ToString().Trim() != "Not yet acknowledged").OrderByDescending(x => x.Id).ToList();


                //int Ack = 0;
                //string qry = " select * from Notifications where (AckRecord = 'Not yet acknowledged' or AckRecord = 'This notification cannot be acknowledged, kindly discard it' or NotificationAlertType = 17 or NotificationAlertType=18) and isactive=1";
                //using (SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con))
                //{
                //    DataTable dtprr = new DataTable();
                //    sda.Fill(dtprr);
                //    if (dtprr.Rows.Count > 0)
                //        Ack = dtprr.Rows.Count;
                //}

                //tobeAcknowledged = "(" + Ack + ")";
                //RaisePropertyChanged("TobeAcknowledged");
                //acknowledged = "(" + list2.Count() + ")"; ;
                //RaisePropertyChanged("Acknowledged");

                int RedAck = 0;
                string Redqry = @" select * from Notifications where (AckRecord = 'Not yet acknowledged' or AckRecord = 'This notification cannot be acknowledged, kindly discard it' or NotificationAlertType = 17 or NotificationAlertType=18) and isactive=1" +
                    "and CreatedDate BETWEEN @DateFrom and @DateTo ";
                using (SqlDataAdapter sda = new SqlDataAdapter(Redqry, sc.con))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@DateFrom", DFrom);
                    sda.SelectCommand.Parameters.AddWithValue("@DateTo", DTo);
                    DataTable dtprr = new DataTable();
                    sda.Fill(dtprr);
                    if (dtprr.Rows.Count > 0)
                        RedAck = dtprr.Rows.Count;
                }

                tobeAcknowledged = "(" + RedAck + ")";
                RaisePropertyChanged("TobeAcknowledged");
                // acknowledged = "(" + list2.Count() + ")"; 


                int Ack = 0;
                string qry = "select * from Notifications where  AckRecord like 'Acknowledged on%' and isactive=1 and CreatedDate BETWEEN @DateFrom and @DateTo ";
                using (SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@DateFrom", DFrom);
                    sda.SelectCommand.Parameters.AddWithValue("@DateTo", DTo);

                    DataTable dtprr = new DataTable();
                    sda.Fill(dtprr);
                    if (dtprr.Rows.Count > 0)
                        Ack = dtprr.Rows.Count;
                }

                acknowledged = "(" + Ack + ")";
                RaisePropertyChanged("Acknowledged");
                //================================================

                var unrdcmnt = sc.NotificationCommnent.Where(x => x.IsRead == false && x.CommentsType == 2).ToList();
                ttlunreadOfccmnt = "(" + unrdcmnt.Count() + ")";
                RaisePropertyChanged("TotalUnreadOfcCmnt");

                list.ForEach(x =>
                {
                    if (x.NotificationAlertType == 17)
                        x.AckRecord = Ropes;
                    if (x.NotificationAlertType == 18)
                        x.AckRecord = Tails;
                });

                var finalList = list;// list1.Concat(list2);
                                     // var finalList = list2.Concat(list1);

                //if (list1.Count > 0)
                //{
                //    count = count + 1;
                //}

                finalList.ToList().ForEach(x => x.Id = counter++);
                sc.ObservableCollectionList(loadUserAccess, finalList.ToList());



                ViewList = new CollectionViewSource
                {
                    Source = LoadUserAccess
                };


                ViewList.Filter += new FilterEventHandler(View_Filter);


                itemcount = loadUserAccess.Count();
                CalculateTotalPages();
                CurrentPage = _currentPageIndex;
                RaisePropertyChanged("CurrentPage");
                ViewList.View.Refresh();


            }
            catch (Exception ex)
            {
                //sc.ErrorLog(ex);

            }

        }

        private DataTable CommentsCounter(int NId)
        {
            DataTable dtprr;
            string qry = @"Select officeComment = (select Count(Comments) from NotificationComment where CommentsType=1 and id = @NId ), ShipComment = (
  select Count(Comments) from NotificationComment where CommentsType=0 and id = @NId )";
            using (SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con))
            {
                sda.SelectCommand.Parameters.AddWithValue("@NId", NId);

                dtprr = new DataTable();
                sda.Fill(dtprr);
                //if (dtprr.Rows.Count > 0)
                //       Ack = dtprr.Rows.Count;
            }

            return dtprr;
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        private void GetNotificationList(DateTime datefrom, DateTime dateto)
        {

            try
            {

                NotificationList.Clear();

                // RaisePropertyChanged("LoadUserAccess");
                SqlCommand cmd = new SqlCommand("NotificationsListing", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DateFrom", datefrom);
                cmd.Parameters.AddWithValue("@DateTo", dateto.AddDays(1));
                cmd.Parameters.AddWithValue("@Action", "Search");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                int counter = 0;
                foreach (DataRow row in ds.Rows)
                {

                    int NotifiAlertType = Convert.ToInt32(row["NotificationAlertType"] == DBNull.Value ? null : row["NotificationAlertType"]);
                    //int NotifiAlertType = Convert.ToInt32( (int)row["NotificationAlertType"]== DBNull.Value ? 0 : (int)row["NotificationAlertType"]);
                    object NotificationDueDate = row["NotificationDueDate"];
                    string certifiNumber = (row["LooseCertificateNum"] == DBNull.Value) ? "" : row["LooseCertificateNum"].ToString();

                    if (NotifiAlertType == 1 & NotificationDueDate != null)
                    {
                        DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-7);
                        if (due <= DateTime.Now.Date)
                            NotificationList.Add(new NotificationsClass()
                            {
                                Id = (int)row["Id"],
                                IndexId = (int)row["Id"],
                                RopeId = (int)row["RopeId"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                NotificationAlertType = NotifiAlertType,
                                LooseCertificateNum = certifiNumber,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],

                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                    }
                    else if (NotifiAlertType == 2 & NotificationDueDate != null)
                    {
                        DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-1);
                        if (due <= DateTime.Now.Date)
                            NotificationList.Add(new NotificationsClass()
                            {
                                Id = (int)row["Ixd"],
                                IndexId = (int)row["Id"],
                                RopeId = (int)row["RopeId"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                NotificationAlertType = NotifiAlertType,
                                LooseCertificateNum = certifiNumber,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],
                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                    }
                    else if (NotifiAlertType == 3 & NotificationDueDate != null)
                    {
                        DateTime due = Convert.ToDateTime(NotificationDueDate);
                        if (due <= DateTime.Now.Date)
                            NotificationList.Add(new NotificationsClass()
                            {
                                Id = (int)row["Id"],
                                IndexId = (int)row["Id"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],
                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                    }
                    else
                    {

                        //  NotificationAlertType
                        if (NotifiAlertType == 21 & NotificationDueDate != null)
                        {
                            DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-7);
                            if (due <= DateTime.Now.Date)
                                NotificationList.Add(new NotificationsClass()
                                {
                                    Id = (int)row["Id"],
                                    IndexId = (int)row["Id"],
                                    RopeId = (int)row["RopeId"],
                                    Notification = (string)row["Notification"],
                                    NotificationType = (int)row["NotificationType"],
                                    Acknowledge = (bool)row["Acknowledge"],
                                    AckRecord = (string)row["AckRecord"],
                                    NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                    IsCheckedV = false,
                                    NotificationAlertType = NotifiAlertType,
                                    LooseCertificateNum = certifiNumber,
                                    OfficeCmnt = (string)row["OfficeCmnt"],
                                    ShipCmnt = (string)row["ShipCmnt"],
                                    //NotificationLogic = (string)row["NotificationLogic"],
                                });
                        }
                        else if (NotifiAlertType == 22 & NotificationDueDate != null)
                        {
                            DateTime due = Convert.ToDateTime(NotificationDueDate).AddDays(-1);
                            if (due <= DateTime.Now.Date)
                                NotificationList.Add(new NotificationsClass()
                                {
                                    Id = (int)row["Id"],
                                    IndexId = (int)row["Id"],
                                    RopeId = (int)row["RopeId"],
                                    Notification = (string)row["Notification"],
                                    NotificationType = (int)row["NotificationType"],
                                    Acknowledge = (bool)row["Acknowledge"],
                                    AckRecord = (string)row["AckRecord"],
                                    NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                    IsCheckedV = false,
                                    NotificationAlertType = NotifiAlertType,
                                    LooseCertificateNum = certifiNumber,
                                    OfficeCmnt = (string)row["OfficeCmnt"],
                                    ShipCmnt = (string)row["ShipCmnt"],
                                    //NotificationLogic = (string)row["NotificationLogic"],
                                });
                        }
                        else if (NotifiAlertType == 23 & NotificationDueDate != null)
                        {
                            DateTime due = Convert.ToDateTime(NotificationDueDate);
                            if (due <= DateTime.Now.Date)
                                NotificationList.Add(new NotificationsClass()
                                {
                                    Id = (int)row["Id"],
                                    IndexId = (int)row["Id"],
                                    Notification = (string)row["Notification"],
                                    NotificationType = (int)row["NotificationType"],
                                    Acknowledge = (bool)row["Acknowledge"],
                                    AckRecord = (string)row["AckRecord"],
                                    NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                    IsCheckedV = false,
                                    LooseCertificateNum = certifiNumber,
                                    OfficeCmnt = (string)row["OfficeCmnt"],
                                    ShipCmnt = (string)row["ShipCmnt"],
                                    //NotificationLogic = (string)row["NotificationLogic"],
                                });
                        }
                        else
                        {
                            NotificationList.Add(new NotificationsClass()
                            {
                                Id = (int)row["Id"],
                                IndexId = (int)row["Id"],
                                RopeId = (int)row["RopeId"],
                                Notification = (string)row["Notification"],
                                NotificationType = (int)row["NotificationType"],
                                Acknowledge = (bool)row["Acknowledge"],
                                AckRecord = (string)row["AckRecord"],
                                NotificationDate = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                                IsCheckedV = false,
                                NotificationAlertType = NotifiAlertType,
                                LooseCertificateNum = certifiNumber,
                                OfficeCmnt = (string)row["OfficeCmnt"],
                                ShipCmnt = (string)row["ShipCmnt"],
                                //NotificationLogic = (string)row["NotificationLogic"],
                            });
                        }
                    }

                }

                List<NotificationsClass> list = NotificationList;

                loadUserAccess.Clear();
                //foreach (DataRow row in ds.Rows)
                //{
                //    foreach (var item in loadUserAccess)
                //    {
                //        if (item.AckRecord == "Not yet acknowledged")
                //        {

                //        }
                //    }
                //}
                //var list1 = sc.Notifications.Where(x => x.AckRecord.ToString().Trim() == "Not yet acknowledged").OrderByDescending(x => x.Id).ToList();
                //var list2 = sc.Notifications.Where(x => x.AckRecord.ToString().Trim() != "Not yet acknowledged").OrderByDescending(x => x.Id).ToList();

                var list1 = list.Where(x => x.AckRecord.ToString().Trim() == "Not yet acknowledged").OrderByDescending(x => x.Id).ToList();
                var list2 = list.Where(x => x.AckRecord.ToString().Trim() != "Not yet acknowledged").OrderByDescending(x => x.Id).ToList();


                tobeAcknowledged = "(" + list1.Count() + ")";
                RaisePropertyChanged("TobeAcknowledged");
                acknowledged = "(" + list2.Count() + ")"; ;
                RaisePropertyChanged("Acknowledged");

                var unrdcmnt = sc.NotificationCommnent.Where(x => x.IsRead == false && x.CommentsType == 2).ToList();
                ttlunreadOfccmnt = "(" + unrdcmnt.Count() + ")";
                RaisePropertyChanged("TotalUnreadOfcCmnt");

                //List<NotificationsClass> notilist = new List<NotificationsClass>();
                //notilist = ConvertDataTable<NotificationsClass>(ds);

                var finalList = list1.Concat(list2);

                if (list1.Count > 0)
                {
                    count = count + 1;
                }


                if (list1.Count > 0 && StaticHelper.AlarmCheck == true)
                {
                    //StaticHelper.AlarmCheck = false;
                    StaticHelper.AlarmFunction(list1.Count, StaticHelper.AlarmCheck);
                }

                finalList.ToList().ForEach(x => x.Id = counter++);
                sc.ObservableCollectionList(loadUserAccess, finalList.ToList());



                ViewList = new CollectionViewSource
                {
                    Source = LoadUserAccess
                };


                ViewList.Filter += new FilterEventHandler(View_Filter);

                itemcount = loadUserAccess.Count();
                CalculateTotalPages();
                CurrentPage = _currentPageIndex;
                RaisePropertyChanged("CurrentPage");
                ViewList.View.Refresh();

                //NextCommand = new NextPageCommand(this);
                //PreviousCommand = new PreviousPageCommand(this);
                //FirstCommand = new FirstPageCommand(this);
                //LastCommand = new LastPageCommand(this);


            }
            catch (Exception ex)
            {
                //sc.ErrorLog(ex);

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



        //public static CollectionViewSource ViewList { get; set; }

        //private static ObservableCollection<NotificationsClass> peopleList = new ObservableCollection<NotificationsClass>();
        //public ObservableCollection<NotificationsClass> PeopleList
        //{
        //    get
        //    {
        //        return peopleList;
        //    }
        //    set
        //    {
        //        peopleList = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("PeopleList"));
        //    }
        //}


        public static CollectionViewSource ViewCertiList { get; set; }



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


        //private static List<WorkHoursNC> deviationList = new List<WorkHoursNC>();
        //public List<WorkHoursNC> DeviationList
        //{
        //    get
        //    {
        //        if (deviationList == null)
        //            if (UserTypeClass.UserTypes == "HOD")
        //            {
        //                string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');


        //                var list = sc1.GetDeviationList();
        //                list = list.Where(x => depart.Contains(x.Department)).ToList();
        //                deviationList = list;
        //            }
        //            else
        //            {
        //                deviationList = sc1.GetDeviationList();
        //            }

        //        return deviationList;
        //    }
        //    set
        //    {
        //        deviationList = value;
        //    }
        //}



        public void ShowNextPage()
        {
            try
            {
                CurrentPageIndex++;
                ViewList.View.Refresh();

                //LoadUserAccess
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






        private void View_Filter(object sender, FilterEventArgs e)
        {
            try
            {
                // int index = ((NotificationsClass)e.Item).RowId + 1;
                int index = ((NotificationsClass)e.Item).Id - 1;
                if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }

                //int index = ((NotificationsClass)e.Item).Id - 1;

                //            //if (index == 0)
                //            //{
                //            //    index = 1;
                //            //}
                //            if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
                //            {
                //                   e.Accepted = true;
                //            }
                //            else
                //            {
                //                   e.Accepted = false;
                //            }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void View_Filter1(object sender, FilterEventArgs e)
        {
            try
            {
                int index = ((NotificationsClass)e.Item).Id - 1;
                if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }

                //int index = ((NotificationsClass)e.Item).Id - 1;

                //            //if (index == 0)
                //            //{
                //            //    index = 1;
                //            //}
                //            if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
                //            {
                //                   e.Accepted = true;
                //            }
                //            else
                //            {
                //                   e.Accepted = false;
                //            }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        //private void View_FilterCerti(object sender, FilterEventArgs e)
        //{
        //    try
        //    {
        //        int index = ((CertificateNC)e.Item).Cid - 1;
        //        if (index >= itemPerPageCerti * CurrentPageCertiIndex && index < itemPerPageCerti * (CurrentPageCertiIndex + 1))
        //        {
        //            e.Accepted = true;
        //        }
        //        else
        //        {
        //            e.Accepted = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //    }
        //}

        //private void View_FilterOffi(object sender, FilterEventArgs e)
        //{
        //    try
        //    {
        //        int index = ((CommentofVSClass)e.Item).Id - 1;
        //        if (index >= itemPerPageOffi * CurrentPageOffiIndex && index < itemPerPageOffi * (CurrentPageOffiIndex + 1))
        //        {
        //            e.Accepted = true;
        //        }
        //        else
        //        {
        //            e.Accepted = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //    }
        //}

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


        //private void CalculateTotalCertiPages()
        //{
        //    try
        //    {
        //        if (itemcountCerti % itemPerPageCerti == 0)
        //        {
        //            TotalCertiPages = (itemcountCerti / itemPerPageCerti);
        //        }
        //        else
        //        {
        //            TotalCertiPages = (itemcountCerti / itemPerPageCerti) + 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //    }
        //}


        //private void CalculateTotalOffiPages()
        //{
        //    try
        //    {
        //        if (itemcountOffi % itemPerPageOffi == 0)
        //        {
        //            TotalOffiPages = (itemcountOffi / itemPerPageOffi);
        //        }
        //        else
        //        {
        //            TotalOffiPages = (itemcountOffi / itemPerPageOffi) + 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //    }
        //}






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
                    loadUserAccess.ToList().ForEach(x => x.IsCheckedV = value);

                    List<NotificationsClass> bb = loadUserAccess.ToList();
                    loadUserAccess.Clear();
                    bb.ToList().ForEach(loadUserAccess.Add);
                    RaisePropertyChanged("LoadUserAccess");
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

        private static string ttlunreadOfccmnt;
        public string TotalUnreadOfcCmnt
        {
            get
            {
                return ttlunreadOfccmnt;
            }
            set
            {
                ttlunreadOfccmnt = value;
                RaisePropertyChanged("TotalUnreadOfcCmnt");
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



        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


        void timer_Tick_WinchRotation(object sender, EventArgs e)
        {
            WinchrotationSetting_UpdateRunningHours();
        }

        private void WinchrotationSetting_UpdateRunningHours()
        {

            string qry = @"select distinct a.WinchId,b.AssignedNumber,b.Location, b.lead,a.AssignedDate,R.Id as RopeID,R.CurrentLeadRunningHours,R.ManufacturerId,R.RopeTypeId,R.UniqueID,R.CertificateNumber,
	T.RopeType,M.Name from AssignRopeToWinch a inner join MooringWinchDetail b on a.WinchId=b.id
and a.IsActive=1 and a.IsDelete=0 and a.RopeTail=0 join MooringRopeDetail R on R.Id=a.RopeId 
and R.RopeTail=0 and R.DeleteStatus=0  and R.OutofServiceDate is null
join tblCommon M on M.Id=R.ManufacturerId join MooringRopeType T on T.Id=R.RopeTypeId";
            SqlDataAdapter ssda = new SqlDataAdapter(qry, sc.con);
            DataTable tbls = new DataTable();
            ssda.Fill(tbls);
            if (tbls.Rows.Count > 0)
            {
                //int ropetypeid = Convert.ToInt32(tbls.Rows[0]["RopeTypeId"]);
                //int manuFid = Convert.ToInt32(tbls.Rows[0]["ManufacturerId"]);
                //decimal CurrentLeadRunningHours = Convert.ToInt32(tbls.Rows[0]["ManufacturerId"]);
                //string lead = tbls.Rows[0]["lead"].ToString();
                //int winchid = Convert.ToInt32(tbls.Rows[0]["WinchId"]);
                int ApprochingCount = 0; int ExceedCount = 0;
                for (int i = 0; i < tbls.Rows.Count; i++)
                {
                    int ManufacturerId = Convert.ToInt32(tbls.Rows[i]["ManufacturerId"]);
                    int RopeTypeId = Convert.ToInt32(tbls.Rows[i]["RopeTypeId"]);
                    int ropeid = Convert.ToInt32(tbls.Rows[i]["RopeID"]);
                    decimal CurrentLeadRunningHours = string.IsNullOrEmpty(tbls.Rows[i]["CurrentLeadRunningHours"].ToString()) == true ? 0 : Convert.ToDecimal(tbls.Rows[i]["CurrentLeadRunningHours"]);
                    string Lead = tbls.Rows[i]["lead"].ToString();
                    Lead = Lead.Replace(System.Environment.NewLine, "").Trim();
                    DateTime AssignedDate = Convert.ToDateTime(tbls.Rows[i]["AssignedDate"]);
                    string UniqueID = tbls.Rows[i]["UniqueID"].ToString();
                    string CertificateNum = tbls.Rows[i]["CertificateNumber"].ToString();
                    string WinchAssignedNumber = tbls.Rows[i]["AssignedNumber"].ToString();


                    SqlDataAdapter pp1 = new SqlDataAdapter("select * from tblWinchRotationSetting where mooringropetype = " + RopeTypeId + " and ManufacturerType= " + ManufacturerId + " and LeadFrom='" + Lead + "'", sc.con);
                    DataTable WRSetting = new DataTable();
                    pp1.Fill(WRSetting);
                    if (WRSetting.Rows.Count > 0)
                    {
                        //int maxrunhrs = Convert.ToInt32(dd1.Rows[0]["MaximumRunningHours"]);
                        //int maxmnthallowed = Convert.ToInt32(dd1.Rows[0]["MaximumMonthsAllowed"]);
                        string leadFrom = WRSetting.Rows[0]["LeadFrom"].ToString();
                        string leadto = WRSetting.Rows[0]["LeadTo"].ToString();

                        int maxrunhrs = Convert.ToInt32(WRSetting.Rows[0]["MaximumRunningHours"]);
                        int maxmnthallowed = Convert.ToInt32(WRSetting.Rows[0]["MaximumMonthsAllowed"]);

                        var AssignedDateAppro = AssignedDate.AddMonths(maxmnthallowed - 2);
                        var AssignedDateExceed = AssignedDate.AddMonths(maxmnthallowed);
                        var CurrentDate = DateTime.Now.Date;//.AddMonths(maxmnthallowed);



                        //Approching Count Start
                        #region
                        int RB = 0; int MA = 0;
                       var WinchMonthdiff = StaticHelper.DateDiffInMonths(AssignedDate, DateTime.Now.Date);//
                        if (CurrentDate >= AssignedDateAppro && CurrentDate < AssignedDateExceed)
                        {
                            
                            var winchrotation = "Winch Rotation is Approaching for Line " + CertificateNum + " - " + UniqueID + " currently on Winch " + WinchAssignedNumber + " lead  " + Lead + " , Current " + WinchMonthdiff + " month / Rotation was due at " + maxmnthallowed + " month,  Please shift from " + Lead + " to " + leadto + "";

                            int NotiAlertType = (int)NotificationAlertType.Winch_Rotation_approching;
                            InspectNotification(ropeid, winchrotation, NotiAlertType);
                            MA++;

                        }

                        if (maxrunhrs > 0)
                        {
                            var maxrunhrs1 = maxrunhrs * 90 / 100;
                            if (CurrentLeadRunningHours > maxrunhrs1)
                            {

                                var winchrotation = "Winch Rotation is Approaching for Line " + CertificateNum + " - " + UniqueID + " currently on Winch " + WinchAssignedNumber + " lead  " + Lead + " , Current " + CurrentLeadRunningHours + " hrs / Rotation was due at " + maxrunhrs + " hrs,  Please shift from " + Lead + " to " + leadto + "";

                                int NotiAlertType = (int)NotificationAlertType.Winch_Rotation_approching;
                                InspectNotification(ropeid, winchrotation, NotiAlertType);
                                RB++;
                            }
                        }

                        if (RB + MA > 0)
                        {
                            ApprochingCount++;
                        }

                        //Approching Count End
                        #endregion
                        //*********************************************************

                        //Exceeded Count Start
                        #region
                        int RB2 = 0; int MA2 = 0;
                        if (CurrentDate >= AssignedDateExceed)
                        {
                            var winchrotation = "Winch Rotation was Exceeded for Line " + CertificateNum + " - " + UniqueID + " currently on Winch " + WinchAssignedNumber + " lead  " + Lead + " , Current " + WinchMonthdiff + " month / Rotation was due at " + maxmnthallowed + " month,  Please shift from " + Lead + " to " + leadto + "";

                            int NotiAlertType = (int)NotificationAlertType.Winch_Rotation_exceed;
                            InspectNotification(ropeid, winchrotation, NotiAlertType);
                            MA2++;
                        }

                        if (maxrunhrs > 0)
                        {
                            //var maxrunhrs1 = maxrunhrs * 90 / 100;
                            if (CurrentLeadRunningHours > maxrunhrs)
                            {
                                var winchrotation = "Winch Rotation was Exceeded for Line " + CertificateNum + " - " + UniqueID + " currently on Winch " + WinchAssignedNumber + " lead  " + Lead + " , Current " + CurrentLeadRunningHours + " hrs / Rotation was due at " + maxrunhrs + " hrs,  Please shift from " + Lead + " to " + leadto + "";

                                int NotiAlertType = (int)NotificationAlertType.Winch_Rotation_exceed;
                                InspectNotification(ropeid, winchrotation, NotiAlertType);
                                RB2++;
                            }
                        }

                        if (RB2 + MA2 > 0)
                        {
                            ExceedCount++;
                        }

                        //Exceeded Count End
                        #endregion


                    }
                }
            }
        }

        void timer_Tick_EndtoEnd(object sender, EventArgs e)
        {
            NotificationEndToEnd();
        }

        public void NotificationEndToEnd()
        {
            try
            {   // For Rope =====================================================
                try
                {
                    string assignednumber = ""; string location = "Not Assigned"; int ropeid = 0; string certificatenumber = ""; string uniqueId = "";


                    SqlDataAdapter dd = new SqlDataAdapter("select * from mooringropedetail where OutofServiceDate is null and DeleteStatus = 0 and RopeTail = 0", sc.con);
                    DataTable dds = new DataTable();
                    dd.Fill(dds);
                    for (int j = 0; j < dds.Rows.Count; j++)
                    {
                        int RopeTid = Convert.ToInt32(dds.Rows[j]["RopeTypeId"]);
                        int ManuFId = Convert.ToInt32(dds.Rows[j]["ManufacturerId"]);
                        ropeid = Convert.ToInt32(dds.Rows[j]["Id"]);
                        certificatenumber = dds.Rows[j]["CertificateNumber"].ToString();
                        uniqueId = dds.Rows[j]["UniqueID"].ToString();

                        int End2EndMonth = 0, ropetypeid = 0, mid = 0, RotationWinchMonth = 0;
                        SqlDataAdapter adp = new SqlDataAdapter("select * from tblRopeInspectionSetting where MooringRopeType=" + RopeTid + " and ManufacturerType=" + ManuFId + "", sc.con);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //for (int i = 0; i < dt.Rows.Count; i++)
                            //{
                            End2EndMonth = Convert.ToInt32(dt.Rows[0]["EndtoEndMonth"]);
                            RotationWinchMonth = Convert.ToInt32(dt.Rows[0]["RotationOnWinches"]);
                            ropetypeid = Convert.ToInt32(dt.Rows[0]["MooringRopeType"]);
                            mid = Convert.ToInt32(dt.Rows[0]["ManufacturerType"]);

                            if (End2EndMonth == 0)
                            {
                                continue;

                            }

                            try
                            {
                                //SqlDataAdapter adp1 = new SqlDataAdapter("select * from MooringRopeDetail where RopeTypeId=" + ropetypeid + " and ManufacturerId=" + mid + "  ", sc.con);
                                //DataTable dt1 = new DataTable();
                                //adp1.Fill(dt1);
                                //if (dt1.Rows.Count > 0)
                                //{
                                try
                                {

                                    DateTime AssignDate;
                                    //ropeid = Convert.ToInt32(dt1.Rows[i]["Id"]);
                                    //certificatenumber = dt1.Rows[i]["CertificateNumber"].ToString();

                                    // string qry = "select * from AssignRopeToWinch where IsActive=1";

                                    SqlDataAdapter pp = new SqlDataAdapter("select a.*,b.AssignedNumber,b.Location from AssignRopeToWinch a inner join MooringWinchDetail b on a.WinchId = b.Id where a.IsActive=1 and a.RopeId =" + ropeid + "", sc.con);
                                    DataTable dtt = new DataTable();
                                    pp.Fill(dtt);
                                    if (dtt.Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dtt.Rows.Count; k++)
                                        {

                                            AssignDate = Convert.ToDateTime(dtt.Rows[k]["AssignedDate"]);
                                            assignednumber = dtt.Rows[k]["AssignedNumber"].ToString();
                                            location = dtt.Rows[k]["Location"].ToString();

                                            DateTime AssignEndTOEndDate; //DateTime rotationdt;

                                            SqlDataAdapter hh = new SqlDataAdapter("select MAX(EndtoEndDoneDate) from RopeEndtoEnd2 where IsActive=1 and  RopeId=" + ropeid + "", sc.con);
                                            DataTable dh = new DataTable();
                                            hh.Fill(dh);
                                            if (dh.Rows.Count > 0)
                                            {
                                                string MaxE2E = dh.Rows[0][0].ToString();
                                                if (!string.IsNullOrEmpty(MaxE2E))
                                                {
                                                    AssignEndTOEndDate = Convert.ToDateTime(MaxE2E);
                                                }
                                                else
                                                {
                                                    AssignEndTOEndDate = AssignDate;
                                                }
                                            }
                                            else
                                            {
                                                AssignEndTOEndDate = AssignDate; //Convert.ToDateTime(dds.Rows[j]["InstalledDate"]);

                                            }


                                            int monthdiff = 0; //int monthdiff1 = 0;
                                                               //SqlDataAdapter uu = new SqlDataAdapter("SELECT DATEDIFF(MONTH, '" + AssignEndTOEndDate.ToString("yyyy-MM-dd") + "', GETDATE()) AS DateDiff", sc.con);
                                                               //DataTable kk = new DataTable();
                                                               //uu.Fill(kk);
                                                               //if (kk.Rows.Count > 0)
                                                               //{
                                            monthdiff = StaticHelper.DateDiffInMonths(AssignEndTOEndDate, DateTime.Now.Date);// Convert.ToInt32(kk.Rows[0][0]);

                                            if (monthdiff >= (End2EndMonth - 1))
                                            {
                                                var End_to_end_approaching = "Line End to End Approaching (Current - " + monthdiff + "/ Reqd - " + End2EndMonth + " Months) for Line '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                                int NotiAlertType = (int)NotificationAlertType.End_to_End_Approaching;
                                                InspectNotification(ropeid, End_to_end_approaching, NotiAlertType);
                                            }
                                            if (monthdiff > End2EndMonth)
                                            {
                                                var End_to_end_overdue = "Line End to End Overdue (Current - " + monthdiff + "/ Reqd - " + End2EndMonth + " Months) for Line '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                                int NotiAlertType = (int)NotificationAlertType.End_to_End_Overdue;
                                                InspectNotification(ropeid, End_to_end_overdue, NotiAlertType);
                                            }



                                        } // End K-loop

                                    }
                                }
                                catch (Exception ex)
                                { }
                                //}
                            }
                            catch (Exception ex)
                            { }

                        }

                        // }

                    }
                }
                catch { }


                // For RopeTail======================================================
                try
                {
                    string assignednumber = ""; string location = "Not Assigned"; int ropeid = 0; string certificatenumber = ""; string uniqueId = "";


                    SqlDataAdapter dd = new SqlDataAdapter("select * from mooringropedetail where OutofServiceDate is null and DeleteStatus = 0 and RopeTail = 1", sc.con);
                    DataTable dds = new DataTable();
                    dd.Fill(dds);
                    for (int j = 0; j < dds.Rows.Count; j++)
                    {
                        int RopeTid = Convert.ToInt32(dds.Rows[j]["RopeTypeId"]);
                        int ManuFId = Convert.ToInt32(dds.Rows[j]["ManufacturerId"]);
                        ropeid = Convert.ToInt32(dds.Rows[j]["Id"]);
                        certificatenumber = dds.Rows[j]["CertificateNumber"].ToString();
                        uniqueId = dds.Rows[j]["UniqueID"].ToString();

                        int End2EndMonth = 0, ropetypeid = 0, mid = 0, RotationWinchMonth = 0;
                        SqlDataAdapter adp = new SqlDataAdapter("select * from tblRopeTailInspectionSetting where MooringRopeType=" + RopeTid + " and ManufacturerType=" + ManuFId + "", sc.con);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //for (int i = 0; i < dt.Rows.Count; i++)
                            //{
                            End2EndMonth = Convert.ToInt32(dt.Rows[0]["EndtoEndMonth"]);
                            RotationWinchMonth = Convert.ToInt32(dt.Rows[0]["RotationOnWinches"]);
                            ropetypeid = Convert.ToInt32(dt.Rows[0]["MooringRopeType"]);
                            mid = Convert.ToInt32(dt.Rows[0]["ManufacturerType"]);

                            if (End2EndMonth == 0)
                            {
                                continue;

                            }
                            try
                            {
                                //SqlDataAdapter adp1 = new SqlDataAdapter("select * from MooringRopeDetail where RopeTypeId=" + ropetypeid + " and ManufacturerId=" + mid + "  ", sc.con);
                                //DataTable dt1 = new DataTable();
                                //adp1.Fill(dt1);
                                //if (dt1.Rows.Count > 0)
                                //{
                                try
                                {

                                    DateTime AssignDate;
                                    //ropeid = Convert.ToInt32(dt1.Rows[i]["Id"]);
                                    //certificatenumber = dt1.Rows[i]["CertificateNumber"].ToString();

                                    // string qry = "select * from AssignRopeToWinch where IsActive=1";

                                    SqlDataAdapter pp = new SqlDataAdapter("select a.*,b.AssignedNumber,b.Location from AssignRopeToWinch a inner join MooringWinchDetail b on a.WinchId = b.Id where a.IsActive=1 and a.RopeId =" + ropeid + "", sc.con);
                                    DataTable dtt = new DataTable();
                                    pp.Fill(dtt);
                                    if (dtt.Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dtt.Rows.Count; k++)
                                        {

                                            AssignDate = Convert.ToDateTime(dtt.Rows[k]["AssignedDate"]);
                                            assignednumber = dtt.Rows[k]["AssignedNumber"].ToString();
                                            location = dtt.Rows[k]["Location"].ToString();

                                            DateTime AssignEndTOEndDate; //DateTime rotationdt;

                                            SqlDataAdapter hh = new SqlDataAdapter("select MAX(EndtoEndDoneDate) from RopeEndtoEnd2 where IsActive=1 and  RopeId=" + ropeid + "", sc.con);
                                            DataTable dh = new DataTable();
                                            hh.Fill(dh);
                                            if (dh.Rows.Count > 0)
                                            {
                                                string MaxE2E = dh.Rows[0][0].ToString();
                                                if (!string.IsNullOrEmpty(MaxE2E))
                                                {
                                                    AssignEndTOEndDate = Convert.ToDateTime(MaxE2E);
                                                }
                                                else
                                                {
                                                    AssignEndTOEndDate = AssignDate;
                                                }
                                            }
                                            else
                                            {
                                                AssignEndTOEndDate = AssignDate; //Convert.ToDateTime(dds.Rows[j]["InstalledDate"]);

                                            }


                                            int monthdiff = 0; //int monthdiff1 = 0;
                                                               //SqlDataAdapter uu = new SqlDataAdapter("SELECT DATEDIFF(MONTH, '" + AssignEndTOEndDate.ToString("yyyy-MM-dd") + "', GETDATE()) AS DateDiff", sc.con);
                                                               //DataTable kk = new DataTable();
                                                               //uu.Fill(kk);
                                                               //if (kk.Rows.Count > 0)
                                                               //{
                                            monthdiff = StaticHelper.DateDiffInMonths(AssignEndTOEndDate, DateTime.Now.Date);// Convert.ToInt32(kk.Rows[0][0]);

                                            if (monthdiff >= (End2EndMonth - 1))
                                            {
                                                var End_to_end_approaching = "Rope-Tail End to End Approaching (Current - " + monthdiff + "/ Reqd - " + End2EndMonth + " Months) for Rope-Tail '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                                int NotiAlertType = (int)NotificationAlertType.End_to_End_Approaching;
                                                InspectNotification(ropeid, End_to_end_approaching, NotiAlertType);
                                            }
                                            if (monthdiff > End2EndMonth)
                                            {
                                                var End_to_end_overdue = "Rope-Tail End to End Overdue (Current - " + monthdiff + "/ Reqd - " + End2EndMonth + " Months) for Rope-Tail '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                                int NotiAlertType = (int)NotificationAlertType.End_to_End_Overdue;
                                                InspectNotification(ropeid, End_to_end_overdue, NotiAlertType);
                                            }



                                          

                                        } // End K-loop

                                    }
                                }
                                catch (Exception ex)
                                { }
                                //}
                            }
                            catch (Exception ex)
                            { }

                        }

                        // }

                    }
                }
                catch { }
            }
            catch { }
        }

        void timer_Tick_Discard(object sender, EventArgs e)
        {
            NorificationRopeDiscard();
        }

        private void NorificationRopeDiscard()
        {
            try
            {
                // Rope Discard Required Only...........

                //SqlDataAdapter dd = new SqlDataAdapter("select * from mooringropedetail where OutofServiceDate is null and DeleteStatus = 0 and RopeTail = 0", sc.con);
                string qrys = @"SELECT distinct rope.Id,rope.RopeTypeId,rope.ManufacturerId,rope.CertificateNumber,rope.UniqueID, rope.InstalledDate,rope.CurrentRunningHours,rope.InspectionDueDate,
seting.MooringRopeType,seting.ManufacturerType,seting.MaximumRunningHours,seting.MaximumMonthsAllowed from MooringRopeDetail rope INNER JOIN tblRopeInspectionSetting seting on rope.RopeTypeId = seting.MooringRopeType 
INNER JOIN  tblRopeInspectionSetting Setter on Setter.ManufacturerType = rope.ManufacturerId
 where rope.OutofServiceDate is null and rope.DeleteStatus = 0 and RopeTail = 0";
                SqlDataAdapter dd = new SqlDataAdapter(qrys, sc.con);
                DataTable dds = new DataTable();
                dd.Fill(dds);
                for (int j = 0; j < dds.Rows.Count; j++)
                {
                    decimal CurrentRunningHours = 0;
                    int Ropeid = Convert.ToInt32(dds.Rows[j]["Id"]);
                    int RopeTid = Convert.ToInt32(dds.Rows[j]["RopeTypeId"]);
                    int ManuFId = Convert.ToInt32(dds.Rows[j]["ManufacturerId"]);
                    string certificatenumber = dds.Rows[j]["CertificateNumber"].ToString();
                    string uniqueId = dds.Rows[j]["UniqueID"].ToString();
                    string rnghr = dds.Rows[j]["CurrentRunningHours"].ToString();
                    if (!string.IsNullOrEmpty(rnghr))
                    {
                        CurrentRunningHours = Convert.ToDecimal(rnghr);
                    }

                    //if(Ropeid==54)
                    //{
                    //    int kl = 0;
                    //}

                    int ropetypeid = Convert.ToInt32(dds.Rows[j]["MooringRopeType"]);
                    int mid = Convert.ToInt32(dds.Rows[j]["ManufacturerType"]);

                    int MaxFixMonth = Convert.ToInt32(dds.Rows[j]["MaximumMonthsAllowed"]);

                    int maxrnghrs = Convert.ToInt32(dds.Rows[j]["MaximumRunningHours"]);

                    if (MaxFixMonth == 0)
                    {
                        continue;
                    }
                    if (maxrnghrs == 0)
                    {
                        continue;
                    }

                    try
                    {


                        string assignednumber = ""; string location = "Not Assigned"; //string certificatenumber = "";
                        SqlDataAdapter pp = new SqlDataAdapter(" select b.AssignedNumber,b.Location from AssignRopeToWinch a join MooringWinchDetail b on a.WinchId = b.Id where a.IsActive=1 and a.RopeId = " + Ropeid + "", sc.con);
                        DataTable dtt = new DataTable();
                        pp.Fill(dtt);
                        if (dtt.Rows.Count > 0)
                        {

                            assignednumber = dtt.Rows[0]["AssignedNumber"].ToString();
                            location = dtt.Rows[0]["Location"].ToString();

                        }




                        int total = (maxrnghrs * 80) / 100;


                        if (CurrentRunningHours >= total)
                        {     // approaching
                            if (assignednumber != null && assignednumber != "")
                            {

                                var Max_running_hours_Approaching = "Max running hours approaching (Current - " + CurrentRunningHours + "hrs / Max - " + maxrnghrs + " hrs) for Line '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Approaching;
                                InspectNotification(Ropeid, Max_running_hours_Approaching, NotiAlertType);
                            }
                            else
                            {
                                var Max_running_hours_Approaching = "Max running hours approaching (Current - " + CurrentRunningHours + "hrs / Max - " + maxrnghrs + " hrs) for Line '" + certificatenumber + " - " + uniqueId + "'";
                                int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Approaching;
                                InspectNotification(Ropeid, Max_running_hours_Approaching, NotiAlertType);
                            }


                        }
                        else
                        {
                            approaching_exceeded_check(Ropeid);
                        }

                        if (CurrentRunningHours >= maxrnghrs)
                        { // exceeded
                            if (assignednumber != null && assignednumber != "")
                            {
                                var Max_running_hours_exceeded = "Max running hours exceeded (Current - " + CurrentRunningHours + "hrs / Max - " + maxrnghrs + " hrs) for Line '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Exceeded;
                                InspectNotification3(Ropeid, Max_running_hours_exceeded, NotiAlertType);
                            }
                            else
                            {
                                var Max_running_hours_exceeded = "Max running hours exceeded (Current - " + CurrentRunningHours + "hrs / Max - " + maxrnghrs + " hrs) for Line '" + certificatenumber + " - " + uniqueId + "'";
                                int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Exceeded;
                                InspectNotification3(Ropeid, Max_running_hours_exceeded, NotiAlertType);
                            }
                        }
                        else
                        {
                            approaching_exceeded_check(Ropeid);
                        }


                        //// max month allowed notification
                        string CheckIntallDT = dds.Rows[j]["InstalledDate"].ToString();
                        if (!string.IsNullOrEmpty(CheckIntallDT))
                        {
                            DateTime installdt = Convert.ToDateTime(CheckIntallDT).Date;
                            int monthdiff = 0;
                            //SqlDataAdapter uu = new SqlDataAdapter("SELECT DATEDIFF(MONTH, '" + installdt.ToString("yyyy-MM-dd") + "', GETDATE()) AS DateDiff", sc.con);
                            //DataTable kk = new DataTable();
                            //uu.Fill(kk);
                            //if (kk.Rows.Count > 0)
                            //{
                            monthdiff = StaticHelper.DateDiffInMonths(installdt, DateTime.Now.Date);// Convert.ToInt32(kk.Rows[0][0]);
                            int approachingmonthdiff = MaxFixMonth - 6;


                            if (monthdiff >= approachingmonthdiff)
                            {

                                if (assignednumber != null && assignednumber != "")
                                {
                                    var Max_allowable_time_approaching = "Max allowable time approaching (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for Line '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Approaching;
                                    InspectNotification(Ropeid, Max_allowable_time_approaching, NotiAlertType);
                                }
                                else
                                {
                                    var Max_allowable_time_approaching = "Max allowable time approaching (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for Line '" + certificatenumber + " - " + uniqueId + "'";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Approaching;
                                    InspectNotification(Ropeid, Max_allowable_time_approaching, NotiAlertType);
                                }
                            }
                            else
                            {
                                time_approaching_exceeded_check(Ropeid);
                            }

                            if (monthdiff > MaxFixMonth)
                            {
                                if (assignednumber != null && assignednumber != "")
                                {
                                    var Max_allowable_time_exceeded = "Max allowable time exceeded (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for Line '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ")";
                                    // var Max_allowable_time_exceeded = "Max allowable time of " + monthdiff + " month exceeded for Rope '" + certificatenumber + "' located at " + assignednumber + " (" + location + ") ";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Exceeded;
                                    InspectNotification3(Ropeid, Max_allowable_time_exceeded, NotiAlertType);
                                }
                                else
                                {
                                    var Max_allowable_time_exceeded = "Max allowable time exceeded (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for Line '" + certificatenumber + " - " + uniqueId + "'";
                                    // var Max_allowable_time_exceeded = "Max allowable time of " + monthdiff + " month exceeded for Rope '" + certificatenumber + "'";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Exceeded;
                                    InspectNotification3(Ropeid, Max_allowable_time_exceeded, NotiAlertType);
                                }
                            }
                            else
                            {
                                time_approaching_exceeded_check(Ropeid);
                            }

                            //}
                        }
                        //}
                    }
                    catch { }




                    //  }
                    //}
                }
            }
            catch { }

            //=========================================================================================

            try
            {
                // RopeTail Discard Required Only...........

                //SqlDataAdapter dd = new SqlDataAdapter("select * from mooringropedetail where OutofServiceDate is null and DeleteStatus = 0 and RopeTail = 0", sc.con);
                string qrys = @"SELECT distinct rope.Id,rope.RopeTypeId,rope.ManufacturerId,rope.CertificateNumber,rope.UniqueID, rope.InstalledDate,rope.CurrentRunningHours,rope.InspectionDueDate,
seting.MooringRopeType,seting.ManufacturerType,seting.MaximumRunningHours,seting.MaximumMonthsAllowed from MooringRopeDetail rope INNER JOIN tblRopeTailInspectionSetting seting on rope.RopeTypeId = seting.MooringRopeType 
INNER JOIN  tblRopeTailInspectionSetting Setter on Setter.ManufacturerType = rope.ManufacturerId
 where rope.OutofServiceDate is null and rope.DeleteStatus = 0 and RopeTail = 1";
                SqlDataAdapter dd = new SqlDataAdapter(qrys, sc.con);
                DataTable dds = new DataTable();
                dd.Fill(dds);
                for (int j = 0; j < dds.Rows.Count; j++)
                {
                    decimal CurrentRunningHours = 0;
                    int Ropeid = Convert.ToInt32(dds.Rows[j]["Id"]);
                    int RopeTid = Convert.ToInt32(dds.Rows[j]["RopeTypeId"]);
                    int ManuFId = Convert.ToInt32(dds.Rows[j]["ManufacturerId"]);
                    string certificatenumber = dds.Rows[j]["CertificateNumber"].ToString();
                    string uniqueId = dds.Rows[j]["UniqueID"].ToString();
                    string rnghr = dds.Rows[j]["CurrentRunningHours"].ToString();

                    if (!string.IsNullOrEmpty(rnghr))
                    {
                        CurrentRunningHours = Convert.ToDecimal(rnghr);
                    }


                    int ropetypeid = Convert.ToInt32(dds.Rows[j]["MooringRopeType"]);
                    int mid = Convert.ToInt32(dds.Rows[j]["ManufacturerType"]);

                    int MaxFixMonth = Convert.ToInt32(dds.Rows[j]["MaximumMonthsAllowed"]);

                    int maxrnghrs = Convert.ToInt32(dds.Rows[j]["MaximumRunningHours"]);

                    if (MaxFixMonth == 0)
                    {
                        continue;
                    }
                    if (maxrnghrs == 0)
                    {
                        continue;
                    }

                    try
                    {


                        string assignednumber = ""; string location = "Not Assigned"; //string certificatenumber = "";
                        SqlDataAdapter pp = new SqlDataAdapter(" select b.AssignedNumber,b.Location from AssignRopeToWinch a join MooringWinchDetail b on a.WinchId = b.Id where a.IsActive=1 and a.RopeId = " + Ropeid + "", sc.con);
                        DataTable dtt = new DataTable();
                        pp.Fill(dtt);
                        if (dtt.Rows.Count > 0)
                        {

                            assignednumber = dtt.Rows[0]["AssignedNumber"].ToString();
                            location = dtt.Rows[0]["Location"].ToString();

                        }




                        int total = (maxrnghrs * 80) / 100;

                        if (CurrentRunningHours >= total)
                        {     // approaching
                            if (assignednumber != null && assignednumber != "")
                            {

                                var Max_running_hours_Approaching = "Max running hours approaching (Current - " + CurrentRunningHours + "hrs / Max - " + maxrnghrs + " hrs) for RopeTail '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Approaching;
                                InspectNotification(Ropeid, Max_running_hours_Approaching, NotiAlertType);
                            }
                            else
                            {
                                var Max_running_hours_Approaching = "Max running hours approaching (Current - " + CurrentRunningHours + "hrs / Max - " + maxrnghrs + " hrs) for RopeTail '" + certificatenumber + " - " + uniqueId + "'";
                                int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Approaching;
                                InspectNotification(Ropeid, Max_running_hours_Approaching, NotiAlertType);
                            }


                        }
                        else
                        {
                            approaching_exceeded_check(Ropeid);
                        }

                        if (CurrentRunningHours >= maxrnghrs)
                        { // exceeded
                            if (assignednumber != null && assignednumber != "")
                            {
                                var Max_running_hours_exceeded = "Max running hours exceeded (Current - " + CurrentRunningHours + "hrs / Max - " + maxrnghrs + " hrs) for RopeTail '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Exceeded;
                                InspectNotification3(Ropeid, Max_running_hours_exceeded, NotiAlertType);
                            }
                            else
                            {
                                var Max_running_hours_exceeded = "Max running hours exceeded (Current - " + CurrentRunningHours + "hrs / Max - " + maxrnghrs + " hrs) for RopeTail '" + certificatenumber + " - " + uniqueId + "'";
                                int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Exceeded;
                                InspectNotification3(Ropeid, Max_running_hours_exceeded, NotiAlertType);
                            }
                        }
                        else
                        {
                            approaching_exceeded_check(Ropeid);
                        }


                        //// max month allowed notification
                        string CheckIntallDT = dds.Rows[j]["InstalledDate"].ToString();
                        if (!string.IsNullOrEmpty(CheckIntallDT))
                        {
                            DateTime installdt = Convert.ToDateTime(CheckIntallDT).Date;
                            int monthdiff = 0;
                            //SqlDataAdapter uu = new SqlDataAdapter("SELECT DATEDIFF(MONTH, '" + installdt.ToString("yyyy-MM-dd") + "', GETDATE()) AS DateDiff", sc.con);
                            //DataTable kk = new DataTable();
                            //uu.Fill(kk);
                            //if (kk.Rows.Count > 0)
                            //{

                            monthdiff = StaticHelper.DateDiffInMonths(installdt, DateTime.Now.Date);// Convert.ToInt32(kk.Rows[0][0]);
                            int approachingmonthdiff = MaxFixMonth - 6;


                            if (monthdiff >= approachingmonthdiff)
                            {

                                if (assignednumber != null && assignednumber != "")
                                {
                                    var Max_allowable_time_approaching = "Max allowable time approaching (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for RopeTail '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Approaching;
                                    InspectNotification(Ropeid, Max_allowable_time_approaching, NotiAlertType);
                                }
                                else
                                {
                                    var Max_allowable_time_approaching = "Max allowable time approaching (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for RopeTail '" + certificatenumber + " - " + uniqueId + "'";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Approaching;
                                    InspectNotification(Ropeid, Max_allowable_time_approaching, NotiAlertType);

                                }

                            }
                            //else
                            //{
                            //    time_approaching_exceeded_check(Ropeid);
                            //}

                            if (monthdiff > MaxFixMonth)
                            {
                                if (assignednumber != null && assignednumber != "")
                                {
                                    //string ddd = "Max allowable time exceeded (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for RopeTail '" + certificatenumber + "' located at " + assignednumber + " (" + location + ")";
                                    //var Max_allowable_time_exceeded = "Max allowable time of " + monthdiff + " month exceeded for RopeTail '" + certificatenumber + "' located at " + assignednumber + " (" + location + ") ";
                                    var Max_allowable_time_exceeded = "Max allowable time exceeded (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for RopeTail '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ")";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Exceeded;
                                    InspectNotification3(Ropeid, Max_allowable_time_exceeded, NotiAlertType);
                                }
                                else
                                {
                                    var Max_allowable_time_exceeded = "Max allowable time exceeded (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for RopeTail '" + certificatenumber + " - " + uniqueId + "'";
                                    //var Max_allowable_time_exceeded = "Max allowable time of " + monthdiff + " month exceeded for RopeTail '" + certificatenumber + "'";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Exceeded;
                                    InspectNotification3(Ropeid, Max_allowable_time_exceeded, NotiAlertType);
                                }
                            }
                            //else
                            //{
                            //    time_approaching_exceeded_check(Ropeid);
                            //}

                            //}
                        }
                        //}
                    }
                    catch (Exception ex)
                    { }

                    //  }
                    //}
                }
            }
            catch (Exception ex)
            { }
        }


        private void time_approaching_exceeded_check(int ropeId)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("select * from Notifications where Notification like '%time approaching%' and RopeId=" + ropeId + "", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    int notiid = Convert.ToInt32(dt.Rows[0]["Id"]);

                    SqlDataAdapter pp = new SqlDataAdapter("delete from Notifications where id=" + notiid + "", sc.con);
                    DataTable dd = new DataTable();
                    pp.Fill(dd);
                }

                SqlDataAdapter adp1 = new SqlDataAdapter("select * from Notifications where Notification like '%time exceeded%' and RopeId=" + ropeId + "", sc.con);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    int notiid1 = Convert.ToInt32(dt1.Rows[0]["Id"]);

                    SqlDataAdapter pp1 = new SqlDataAdapter("delete from Notifications where id=" + notiid1 + "", sc.con);
                    DataTable dd1 = new DataTable();
                    pp1.Fill(dd1);
                }
            }
            catch { }
        }
        private void approaching_exceeded_check(int ropeId)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("select * from Notifications where Notification like '%hours approaching%' and RopeId=" + ropeId + "", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    int notiid = Convert.ToInt32(dt.Rows[0]["Id"]);

                    SqlDataAdapter pp = new SqlDataAdapter("delete from Notifications where id=" + notiid + "", sc.con);
                    DataTable dd = new DataTable();
                    pp.Fill(dd);
                }

                SqlDataAdapter adp1 = new SqlDataAdapter("select * from Notifications where Notification like '%hours exceeded%' and RopeId=" + ropeId + "", sc.con);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    int notiid1 = Convert.ToInt32(dt1.Rows[0]["Id"]);

                    SqlDataAdapter pp1 = new SqlDataAdapter("delete from Notifications where id=" + notiid1 + "", sc.con);
                    DataTable dd1 = new DataTable();
                    pp1.Fill(dd1);
                }
            }
            catch { }
        }

        private void InspectionDue7DayOrOerLooseEquipment(string Notification, DateTime DueDate, string LoosEqID, int LooseEqType, int AlertType)
        {
            int[] N7Day = { 21, 24, 27, 30, 33 };
            int[] N1Day = { 22, 25, 28, 31, 34 };
            int[] NOver = { 23, 26, 29, 32, 35 };
            int[] Nin = { 21, 24, 27, 30, 33, 22, 25, 28, 31, 34, 23, 26, 29, 32, 35 };
            string Certifinum = LoosEqID;
            if (N7Day.Contains(AlertType) == true && DateTime.Now.Date >= DueDate.Date.AddDays(-7))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter("select * from Notifications where LooseEqType = " + LooseEqType + " and LooseCertificateNum = '" + LoosEqID + "' and NotificationAlertType = " + AlertType + "", sc.con))
                {
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        NotificationsClass noti = new NotificationsClass();
                        noti.Acknowledge = false;
                        noti.AckRecord = "Not yet acknowledged";
                        noti.Notification = Notification;
                        noti.RopeId = 0;
                        noti.IsActive = true;
                        noti.NotificationType = 1;
                        noti.NotificationDueDate = DueDate;
                        noti.CreatedDate = DateTime.Now;
                        noti.CreatedBy = "Admin";
                        noti.NotificationAlertType = AlertType;
                        noti.LooseCertificateNum = Certifinum;
                        noti.LooseEqType = LooseEqType;
                        sc.Notifications.Add(noti);
                        sc.SaveChanges();


                        StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                    }
                }
            }
            else if (N1Day.Contains(AlertType) == true && DateTime.Now.Date >= DueDate.Date.AddDays(-1))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter("select * from Notifications where LooseEqType = " + LooseEqType + " and LooseCertificateNum = '" + LoosEqID + "' and NotificationAlertType = " + AlertType + "", sc.con))
                {
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        NotificationsClass noti = new NotificationsClass();
                        noti.LooseCertificateNum = Certifinum;
                        noti.LooseEqType = LooseEqType;
                        noti.Acknowledge = false;
                        noti.AckRecord = "Not yet acknowledged";
                        noti.Notification = Notification;
                        noti.RopeId = 0;
                        noti.IsActive = true;
                        noti.NotificationType = 1;
                        noti.NotificationDueDate = DueDate;
                        noti.CreatedDate = DateTime.Now;
                        noti.CreatedBy = "Admin";
                        noti.NotificationAlertType = AlertType;
                        sc.Notifications.Add(noti);
                        sc.SaveChanges();

                        StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                    }
                }
            }
            else if (NOver.Contains(AlertType) == true && DateTime.Now.Date >= DueDate.Date.AddDays(1))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter("select * from Notifications where LooseEqType = " + LooseEqType + " and LooseCertificateNum = '" + LoosEqID + "' and NotificationAlertType = " + AlertType + "", sc.con))
                {
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        NotificationsClass noti = new NotificationsClass();
                        noti.LooseCertificateNum = Certifinum;
                        noti.LooseEqType = LooseEqType;
                        noti.Acknowledge = false;
                        noti.AckRecord = "Not yet acknowledged";
                        noti.Notification = Notification;
                        noti.RopeId = 0;
                        noti.IsActive = true;
                        noti.NotificationType = 1;
                        noti.NotificationDueDate = DueDate;
                        noti.CreatedDate = DateTime.Now;
                        noti.CreatedBy = "Admin";
                        noti.NotificationAlertType = AlertType;
                        sc.Notifications.Add(noti);
                        sc.SaveChanges();

                        StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                    }
                }
            }
            else if (Nin.Contains(AlertType) == false)
            {
                using (SqlDataAdapter adp = new SqlDataAdapter("select * from Notifications where LooseEqType = " + LooseEqType + " and LooseCertificateNum = '" + LoosEqID + "' and NotificationAlertType = " + AlertType + "", sc.con))
                {
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        NotificationsClass noti = new NotificationsClass();
                        noti.LooseCertificateNum = Certifinum;
                        noti.LooseEqType = LooseEqType;
                        noti.Acknowledge = false;
                        noti.AckRecord = "Not yet acknowledged";
                        noti.Notification = Notification;
                        noti.RopeId = 0;
                        noti.IsActive = true;
                        noti.NotificationType = 1;
                        noti.NotificationDueDate = DueDate;
                        noti.CreatedDate = DateTime.Now;
                        noti.CreatedBy = "Admin";
                        noti.NotificationAlertType = AlertType;
                        sc.Notifications.Add(noti);
                        sc.SaveChanges();

                        StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                    }
                }
            }



        }

        void timer_Tick2_Loose_Equipment(object sender, EventArgs e)
        {
            try
            {
                LooseEquipmentNoti();

            }
            catch { }
        }

        private void LooseEquipmentNoti()
        {
            //// JoiningShackle Notification
            try
            {

                SqlDataAdapter adp = new SqlDataAdapter("Select * from JoiningShackle where DeleteStatus = 0  and OutofServiceDate is null and CAST( InspectionDueDate AS date) < DATEADD(day,1+7,CAST(GETDATE() AS date))", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string JoiningShackleID = dt.Rows[i]["Id"].ToString();
                        int LoosType = 1;
                        string InstallDate = dt.Rows[i]["DateInstalled"].ToString();
                        string InspectionDueDate = dt.Rows[i]["InspectionDueDate"].ToString();
                        string CertificatNum = dt.Rows[i]["CertificateNumber"].ToString();
                        if (!string.IsNullOrEmpty(InspectionDueDate))
                        {
                            DateTime insduedt = Convert.ToDateTime(InspectionDueDate);



                            int AlertTp7 = (int)NotificationAlertType.JoiningShackle_LooseEquipment7Day;
                            var notification7 = "Inspection Due in 7 days- For JoiningShackle Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notification7, insduedt, JoiningShackleID, LoosType, AlertTp7);

                            int AlertTp1 = (int)NotificationAlertType.JoiningShackle_LooseEquipment1Day;
                            var notification1 = "Inspection Due in 1 day- For JoiningShackle Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notification1, insduedt, JoiningShackleID, LoosType, AlertTp1);

                            int AlertTpOv = (int)NotificationAlertType.JoiningShackle_LooseEquipmentOver;
                            var notificationOv = "Inspection Overdue- For JoiningShackle Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notificationOv, insduedt, JoiningShackleID, LoosType, AlertTpOv);

                            //}
                        }


                    }

                }
                adp.Dispose();
                dt.Dispose();

                using (SqlDataAdapter adp5 = new SqlDataAdapter("Select * from JoiningShackle where DeleteStatus = 0  and OutofServiceDate is null ", sc.con))
                {
                    DataTable dt5 = new DataTable();
                    adp5.Fill(dt5);
                    if (dt5.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {

                            string JoiningShackleID = dt5.Rows[i]["Id"].ToString();
                            int LoosType = 1;
                            string InstallDate = dt5.Rows[i]["DateInstalled"].ToString();

                            string CertificatNum = dt5.Rows[i]["CertificateNumber"].ToString();

                            LooseEquipmentDisCard(InstallDate, LoosType, Convert.ToInt32(JoiningShackleID), CertificatNum);

                        }
                    }
                }


            }
            catch (Exception ex) { }


            //////////// RopeStopper,FireWire,Messanger Rope Notification

            try
            {

                SqlDataAdapter adp = new SqlDataAdapter("Select * from RopeTail where DeleteStatus = 0  and OutofServiceDate is null and CAST( InspectionDueDate AS date) < DATEADD(day,1+7,CAST(GETDATE() AS date))", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string InspectionDueDate = dt.Rows[i]["InspectionDueDate"].ToString();
                        string RopeStopper_FireWire_Messanger = dt.Rows[i]["Id"].ToString();
                        int LoosType = Convert.ToInt32(dt.Rows[i]["LooseETypeId"]);
                        string InstallDate = dt.Rows[i]["InstalledDate"].ToString();
                        string LoosEqpName = "";

                        if (LoosType == 3)
                        {
                            LoosEqpName = "Messenger Rope";

                        }
                        else if (LoosType == 4)
                        {
                            LoosEqpName = "Rope Stopper";

                        }
                        else if (LoosType == 6)
                        {
                            LoosEqpName = "FireWire";

                        }
                        else if (LoosType == 9)
                        {
                            LoosEqpName = "Towing Rope";

                        }
                        else if (LoosType == 10)
                        {
                            LoosEqpName = "Suez Rope";

                        }
                        else if (LoosType == 11)
                        {
                            LoosEqpName = "Pennant Rope";

                        }
                        else if (LoosType == 12)
                        {
                            LoosEqpName = "Grommet Rope";

                        }

                        string CertificatNum = dt.Rows[i]["CertificateNumber"].ToString();
                        if (!string.IsNullOrEmpty(InspectionDueDate))
                        {
                            DateTime insduedt = Convert.ToDateTime(InspectionDueDate);



                            int AlertTp7 = (int)NotificationAlertType.RopeStopper_FireWire_Messanger_LooseEq7Day;
                            var notification7 = "Inspection Due in 7 days- For " + LoosEqpName + " Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notification7, insduedt, RopeStopper_FireWire_Messanger, LoosType, AlertTp7);

                            int AlertTp1 = (int)NotificationAlertType.RopeStopper_FireWire_Messanger_LooseEq1Day;
                            var notification1 = "Inspection Due in 1 day- For " + LoosEqpName + " Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notification1, insduedt, RopeStopper_FireWire_Messanger, LoosType, AlertTp1);

                            int AlertTpOv = (int)NotificationAlertType.RopeStopper_FireWire_Messanger_LooseEqOver;
                            var notificationOv = "Inspection Overdue- For " + LoosEqpName + " Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notificationOv, insduedt, RopeStopper_FireWire_Messanger, LoosType, AlertTpOv);


                            //}
                        }


                    }

                }


                using (SqlDataAdapter adp5 = new SqlDataAdapter("Select * from RopeTail where DeleteStatus = 0  and OutofServiceDate is null ", sc.con))
                {
                    DataTable dt5 = new DataTable();
                    adp5.Fill(dt5);
                    if (dt5.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {

                            string RopeStopper_FireWire_Messanger = dt5.Rows[i]["Id"].ToString();
                            int LoosType = Convert.ToInt32(dt5.Rows[i]["LooseETypeId"]);
                            string InstallDate = dt5.Rows[i]["InstalledDate"].ToString();

                            string CertificatNum = dt5.Rows[i]["CertificateNumber"].ToString();

                            LooseEquipmentDisCard(InstallDate, LoosType, Convert.ToInt32(RopeStopper_FireWire_Messanger), CertificatNum);

                        }
                    }
                }

                adp.Dispose();
                dt.Dispose();

            }
            catch (Exception exc)
            {
            }

            //////////// ChainStopper Notification


            try
            {

                SqlDataAdapter adp = new SqlDataAdapter("Select * from ChainStopper where DeleteStatus = 0  and OutofServiceDate is null and CAST( InspectionDueDate AS date) < DATEADD(day,1+7,CAST(GETDATE() AS date))", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string ChainStopperID = dt.Rows[i]["Id"].ToString();

                        int LoosType = Convert.ToInt32(dt.Rows[i]["LooseETypeId"]);
                        string InstallDate = dt.Rows[i]["DateInstalled"].ToString();

                        string InspectionDueDate = dt.Rows[i]["InspectionDueDate"].ToString();

                        string CertificatNum = dt.Rows[i]["CertificateNumber"].ToString();

                        if (!string.IsNullOrEmpty(InspectionDueDate))
                        {
                            DateTime insduedt = Convert.ToDateTime(InspectionDueDate);



                            int AlertTp7 = (int)NotificationAlertType.ChainStopper_LooseEq7Day;
                            var notification7 = "Inspection Due in 7 days- For ChainStopper Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notification7, insduedt, ChainStopperID, LoosType, AlertTp7);

                            int AlertTp1 = (int)NotificationAlertType.ChainStopper_LooseEq1Day;
                            var notification1 = "Inspection Due in 1 day- For ChainStopper Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notification1, insduedt, ChainStopperID, LoosType, AlertTp1);

                            int AlertTpOv = (int)NotificationAlertType.ChainStopper_LooseEqOver;
                            var notificationOv = "Inspection Overdue- For ChainStopper Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notificationOv, insduedt, ChainStopperID, LoosType, AlertTpOv);

                            //}
                        }



                    }

                }

                adp.Dispose();
                dt.Dispose();

                using (SqlDataAdapter adp5 = new SqlDataAdapter("Select * from ChainStopper where DeleteStatus = 0  and OutofServiceDate is null ", sc.con))
                {
                    DataTable dt5 = new DataTable();
                    adp5.Fill(dt5);
                    if (dt5.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {

                            string ChainStopperID = dt5.Rows[i]["Id"].ToString();
                            int LoosType = Convert.ToInt32(dt5.Rows[i]["LooseETypeId"]);
                            string InstallDate = dt5.Rows[i]["DateInstalled"].ToString();

                            string CertificatNum = dt5.Rows[i]["CertificateNumber"].ToString();


                            LooseEquipmentDisCard(InstallDate, LoosType, Convert.ToInt32(ChainStopperID), CertificatNum);

                        }
                    }
                }

            }
            catch (Exception ex) { }


            //////////// ChafeGuard Notification


            try
            {

                SqlDataAdapter adp = new SqlDataAdapter("Select * from ChafeGuard where DeleteStatus = 0  and OutofServiceDate is null and CAST( InspectionDueDate AS date) < DATEADD(day,1+7,CAST(GETDATE() AS date))", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string ChafeGuardID = dt.Rows[i]["Id"].ToString();
                        string InspectionDueDate = dt.Rows[i]["InspectionDueDate"].ToString();

                        int LoosType = 7;
                        string InstallDate = dt.Rows[i]["InstalledDate"].ToString();
                        string CertificatNum = dt.Rows[i]["CertificateNumber"].ToString();

                        if (!string.IsNullOrEmpty(InspectionDueDate))
                        {
                            DateTime insduedt = Convert.ToDateTime(InspectionDueDate);




                            int AlertTp7 = (int)NotificationAlertType.ChafeGuard_LooseEq7Day;
                            var notification7 = "Inspection Due in 7 days- For ChafeGuard Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notification7, insduedt, ChafeGuardID, LoosType, AlertTp7);

                            int AlertTp1 = (int)NotificationAlertType.ChafeGuard_LooseEq1Day;
                            var notification1 = "Inspection Due in 1 day- For ChafeGuard Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notification1, insduedt, ChafeGuardID, LoosType, AlertTp1);

                            int AlertTpOv = (int)NotificationAlertType.ChafeGuard_LooseEqOver;
                            var notificationOv = "Inspection Overdue- For ChafeGuard Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notificationOv, insduedt, ChafeGuardID, LoosType, AlertTpOv);

                            //}
                        }


                    }

                }

                adp.Dispose();
                dt.Dispose();

                using (SqlDataAdapter adp5 = new SqlDataAdapter("Select * from ChafeGuard where DeleteStatus = 0  and OutofServiceDate is null ", sc.con))
                {
                    DataTable dt5 = new DataTable();
                    adp5.Fill(dt5);
                    if (dt5.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {

                            string ChafeGuardID = dt5.Rows[i]["Id"].ToString();
                            int LoosType = 7;
                            string InstallDate = dt5.Rows[i]["InstalledDate"].ToString();

                            string CertificatNum = dt5.Rows[i]["CertificateNumber"].ToString();

                            LooseEquipmentDisCard(InstallDate, LoosType, Convert.ToInt32(ChafeGuardID), CertificatNum);

                        }
                    }
                }
            }
            catch (Exception ex) { }


            //////////// WinchBreakTestKit Notification


            try
            {

                SqlDataAdapter adp = new SqlDataAdapter("Select * from WinchBreakTestKit where DeleteStatus = 0  and OutofServiceDate is null and CAST( InspectionDueDate AS date) < DATEADD(day,1+7,CAST(GETDATE() AS date))", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string WinchBreakTestKitID = dt.Rows[i]["Id"].ToString();
                        string InspectionDueDate = dt.Rows[i]["InspectionDueDate"].ToString();

                        int LoosType = 8;
                        string InstallDate = dt.Rows[i]["InstalledDate"].ToString();


                        string CertificatNum = dt.Rows[i]["CertificateNumber"].ToString();

                        if (!string.IsNullOrEmpty(InspectionDueDate))
                        {
                            DateTime insduedt = Convert.ToDateTime(InspectionDueDate);


                            int AlertTp7 = (int)NotificationAlertType.WinchBreakTest_LooseEq7Day;
                            var notification7 = "Inspection Due in 7 days- For Winch Break-Test-Kit Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notification7, insduedt, WinchBreakTestKitID, LoosType, AlertTp7);

                            int AlertTp1 = (int)NotificationAlertType.WinchBreakTest_LooseEq1Day;
                            var notification1 = "Inspection Due in 1 day- For Winch Break-Test-Kit Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notification1, insduedt, WinchBreakTestKitID, LoosType, AlertTp1);

                            int AlertTpOv = (int)NotificationAlertType.WinchBreakTest_LooseEqOver;
                            var notificationOv = "Inspection Overdue- For Winch Break-Test-Kit Loose Equipment #" + CertificatNum + "";
                            InspectionDue7DayOrOerLooseEquipment(notificationOv, insduedt, WinchBreakTestKitID, LoosType, AlertTpOv);

                            //}
                        }


                    }

                }

                adp.Dispose();
                dt.Dispose();

                using (SqlDataAdapter adp5 = new SqlDataAdapter("Select * from WinchBreakTestKit where DeleteStatus = 0  and OutofServiceDate is null ", sc.con))
                {
                    DataTable dt5 = new DataTable();
                    adp5.Fill(dt5);
                    if (dt5.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {

                            string WinchBreakTestKitID = dt5.Rows[i]["Id"].ToString();
                            int LoosType = 8;
                            string InstallDate = dt5.Rows[i]["InstalledDate"].ToString();

                            string CertificatNum = dt5.Rows[i]["CertificateNumber"].ToString();

                            LooseEquipmentDisCard(InstallDate, LoosType, Convert.ToInt32(WinchBreakTestKitID), CertificatNum);
                            // LooseEquipmentDisCard(InstallDate, LoosType, Convert.ToInt32(ChafeGuardID), CertificatNum);

                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void LooseEquipmentDisCard(string CheckIntallDT, int LooseEqType, int LID, string CertificatNum)
        {
            try
            {


                string LoosEqpName = ""; int NotiAlertTypeApproching = 0; int NotiAlertTypeApproching_Exceeded = 0;
                if (LooseEqType == 1)
                {
                    LoosEqpName = "Joining Shackle";
                    NotiAlertTypeApproching = (int)NotificationAlertType.JoiningShackle_LooseEquipmentDisCard;
                }
                else if (LooseEqType == 2)
                {
                    LoosEqpName = "Rope Tail";
                }
                else if (LooseEqType == 3)
                {
                    LoosEqpName = "Messenger Rope";
                    NotiAlertTypeApproching = (int)NotificationAlertType.RopeStopper_FireWire_Messanger_LooseEquipmentDisCard;
                }
                else if (LooseEqType == 4)
                {
                    LoosEqpName = "Rope Stopper";
                    NotiAlertTypeApproching = (int)NotificationAlertType.RopeStopper_FireWire_Messanger_LooseEquipmentDisCard;
                }
                else if (LooseEqType == 5)
                {
                    LoosEqpName = "Chain Stopper";
                    NotiAlertTypeApproching = (int)NotificationAlertType.ChainStopper_LooseEquipmentDisCard;
                }
                else if (LooseEqType == 6)
                {
                    LoosEqpName = "FireWire";
                    NotiAlertTypeApproching = (int)NotificationAlertType.RopeStopper_FireWire_Messanger_LooseEquipmentDisCard;
                }
                else if (LooseEqType == 7)
                {
                    LoosEqpName = "Chafe Guard";
                    NotiAlertTypeApproching = (int)NotificationAlertType.ChafeGuard_LooseEquipmentDisCard;
                }
                else if (LooseEqType == 8)
                {
                    LoosEqpName = "Winch Break Test Kit";
                    NotiAlertTypeApproching = (int)NotificationAlertType.WinchBreakTest_LooseEquipmentDisCard;
                }
                else if (LooseEqType == 9)
                {
                    LoosEqpName = "Towing Rope";
                    NotiAlertTypeApproching = (int)NotificationAlertType.TowingRope_LooseEquipmentDisCard;
                }
                else if (LooseEqType == 10)
                {
                    LoosEqpName = "Suez Rope";
                    NotiAlertTypeApproching = (int)NotificationAlertType.SuezRope_LooseEquipmentDisCard;
                }
                else if (LooseEqType == 11)
                {
                    LoosEqpName = "Pennant Rope";
                    NotiAlertTypeApproching = (int)NotificationAlertType.PennantRope_LooseEquipmentDisCard;
                }
                else if (LooseEqType == 12)
                {
                    LoosEqpName = "Grommet Rope";
                    NotiAlertTypeApproching = (int)NotificationAlertType.GrommetRope_LooseEquipmentDisCard;
                }

                int InspectionFrequency = 0; int MaximumMonthsAllowed = 0; int MaximumRunningHours = 0;
                SqlDataAdapter pp = new SqlDataAdapter(" select EquipmentType,InspectionFrequency,MaximumMonthsAllowed,MaximumRunningHours from tblLooseEquipInspectionSetting where EquipmentType = " + LooseEqType + "", sc.con);
                DataTable dtt = new DataTable();
                pp.Fill(dtt);
                if (dtt.Rows.Count > 0)
                {

                    InspectionFrequency = Convert.ToInt32(dtt.Rows[0]["InspectionFrequency"]);
                    MaximumRunningHours = Convert.ToInt32(dtt.Rows[0]["MaximumRunningHours"]);
                    MaximumMonthsAllowed = Convert.ToInt32(dtt.Rows[0]["MaximumMonthsAllowed"]);

                    if (MaximumRunningHours == 0)
                    {
                        return;
                    }
                    if (MaximumMonthsAllowed == 0)
                    {
                        return;
                    }
                    //// max month allowed notification

                    if (!string.IsNullOrEmpty(CheckIntallDT))
                    {
                        DateTime installdt = Convert.ToDateTime(CheckIntallDT);
                        int monthdiff = 0;
                        //SqlDataAdapter uu = new SqlDataAdapter("SELECT DATEDIFF(MONTH, '" + installdt.ToString("yyyy-MM-dd") + "', GETDATE()) AS DateDiff", sc.con);
                        //DataTable kk = new DataTable();
                        //uu.Fill(kk);
                        //if (kk.Rows.Count > 0)
                        //{
                        monthdiff = StaticHelper.DateDiffInMonths(installdt, DateTime.Now.Date);// Convert.ToInt32(kk.Rows[0][0]);
                        int approachingmonthdiff = MaximumMonthsAllowed - 6;


                        if (monthdiff >= approachingmonthdiff)
                        {

                            var Max_allowable_time_approaching = "Max allowable time approaching (Current - " + monthdiff + " month / Max - " + MaximumMonthsAllowed + " Months) For " + LoosEqpName + " Loose Equipment # " + CertificatNum + "";

                            using (SqlDataAdapter adp = new SqlDataAdapter("select * from Notifications where LooseEqType = " + LooseEqType + " and LooseCertificateNum = '" + LID + "' and NotificationAlertType = " + NotiAlertTypeApproching + "", sc.con))
                            {
                                DataTable dt = new DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count == 0)
                                {
                                    NotificationsClass noti = new NotificationsClass();
                                    noti.Acknowledge = false;
                                    noti.AckRecord = "Not yet acknowledged";
                                    noti.Notification = Max_allowable_time_approaching;
                                    noti.RopeId = 0;
                                    noti.IsActive = true;
                                    noti.NotificationType = 1;
                                    noti.NotificationDueDate = DateTime.Now.Date;
                                    noti.CreatedDate = DateTime.Now;
                                    noti.CreatedBy = "Admin";
                                    noti.NotificationAlertType = NotiAlertTypeApproching;
                                    noti.LooseCertificateNum = LID.ToString();
                                    noti.LooseEqType = LooseEqType;
                                    sc.Notifications.Add(noti);
                                    sc.SaveChanges();

                                    StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                                }
                            }



                        }

                        if (monthdiff > MaximumMonthsAllowed)
                        {

                            var Max_allowable_time_exceeded = LoosEqpName + " Max allowable time of " + monthdiff + " month exceeded For " + LoosEqpName + " Loose Equipment # " + CertificatNum + "";
                            NotiAlertTypeApproching_Exceeded = NotiAlertTypeApproching + (int)NotificationAlertType.All_LooseEquipmentExceeded;
                            using (SqlDataAdapter adp = new SqlDataAdapter("select * from Notifications where LooseEqType = " + LooseEqType + " and LooseCertificateNum = '" + LID + "' and NotificationAlertType = " + NotiAlertTypeApproching_Exceeded + "", sc.con))
                            {
                                DataTable dt = new DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count == 0)
                                {
                                    NotificationsClass noti = new NotificationsClass();
                                    noti.Acknowledge = false;
                                    //noti.AckRecord = "Not yet acknowledged";
                                    noti.AckRecord = "This notification cannot be acknowledged, kindly discard it";
                                    noti.Notification = Max_allowable_time_exceeded;
                                    noti.RopeId = 0;
                                    noti.IsActive = true;
                                    noti.NotificationType = 1;
                                    noti.NotificationDueDate = DateTime.Now.Date;
                                    noti.CreatedDate = DateTime.Now;
                                    noti.CreatedBy = "Admin";
                                    noti.NotificationAlertType = NotiAlertTypeApproching_Exceeded;
                                    noti.LooseCertificateNum = LID.ToString();
                                    noti.LooseEqType = LooseEqType;
                                    sc.Notifications.Add(noti);
                                    sc.SaveChanges();

                                    StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                                }
                            }


                        }

                        //}

                    }
                }

            }
            catch (Exception ex) { }
        }

        void timer_Tick1(object sender, EventArgs e)
        {
            StaticHelper.AlarmCheck = true;

            InspectionDue7daysOROverdue(); // This is only for Rope
                                           // Do same function for Tail also;
        }


        private void InspectionDue7daysOROverdue()
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("Select * from MooringRopeDetail where DeleteStatus = 0  and OutofServiceDate is null and CAST( InspectionDueDate AS date) < DATEADD(day,1+7,CAST(GETDATE() AS date))", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    // RopeTail = 0 then Rope, else RopeTail

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var RT = Convert.ToInt32(dt.Rows[i]["RopeTail"]);
                        string Tail = RT == 0 ? "Line" : "RopeTail";

                        int RopeId = Convert.ToInt32(dt.Rows[i]["Id"]);
                        string assignednumber = ""; string location = "Not Assigned";


                        using (SqlDataAdapter pp = new SqlDataAdapter(" select b.AssignedNumber,b.Location from AssignRopeToWinch a join MooringWinchDetail b on a.WinchId = b.Id where a.IsActive=1 and a.RopeId = " + RopeId + "", sc.con))
                        {
                            DataTable dtt = new DataTable();
                            pp.Fill(dtt);
                            if (dtt.Rows.Count > 0)
                            {

                                assignednumber = dtt.Rows[0]["AssignedNumber"].ToString();
                                location = dtt.Rows[0]["Location"].ToString();

                            }
                        }




                        DateTime DueDateInsp = Convert.ToDateTime(dt.Rows[i]["InspectionDueDate"]).Date;

                        if (assignednumber != null && assignednumber != "")
                        {




                            var notification7 = "Inspection Due in 7 days- For " + Tail + " #" + dt.Rows[i]["CertificateNumber"] + " located at " + assignednumber + " Winch (" + location + ")";
                            // var notification7 = "Inspection Due in 7 days- For Rope #" + dt.Rows[i]["CertificateNumber"] + " on Winch " + assignednumber + " located at " + location;
                            int NotiAlertType = (int)NotificationAlertType.Inspection7Day;
                            InspectNotification(RopeId, notification7, NotiAlertType, DueDateInsp);


                            var notification1 = "Inspection Due in 1 day- For " + Tail + " #" + dt.Rows[i]["CertificateNumber"] + " located at " + assignednumber + " Winch (" + location + ")";
                            int NotiAlertType1 = (int)NotificationAlertType.Inspection1Day;
                            InspectNotification(RopeId, notification1, NotiAlertType1, DueDateInsp);


                            var notificationOv = "Inspection Overdue- For " + Tail + " #" + dt.Rows[i]["CertificateNumber"] + " located at " + assignednumber + " Winch (" + location + ")";
                            int NotiAlertType3 = (int)NotificationAlertType.InspectionOver;
                            InspectNotification(RopeId, notificationOv, NotiAlertType3, DueDateInsp);

                        }
                        else
                        {
                            var notification7 = "Inspection Due in 7 days- For " + Tail + " #" + dt.Rows[i]["CertificateNumber"] + "";
                            // var notification7 = "Inspection Due in 7 days- For Rope #" + dt.Rows[i]["CertificateNumber"] + " on Winch " + assignednumber + " located at " + location;
                            int NotiAlertType = (int)NotificationAlertType.Inspection7Day;
                            InspectNotification(RopeId, notification7, NotiAlertType, DueDateInsp);


                            var notification1 = "Inspection Due in 1 day- For " + Tail + " #" + dt.Rows[i]["CertificateNumber"] + "";
                            int NotiAlertType1 = (int)NotificationAlertType.Inspection1Day;
                            InspectNotification(RopeId, notification1, NotiAlertType1, DueDateInsp);


                            var notificationOv = "Inspection Overdue- For " + Tail + " #" + dt.Rows[i]["CertificateNumber"] + "";
                            int NotiAlertType3 = (int)NotificationAlertType.InspectionOver;
                            InspectNotification(RopeId, notificationOv, NotiAlertType3, DueDateInsp);
                        }
                    }
                }
            }
            catch { }
        }

        public void InspectNotification(int RopeID, string NotiMsg, int NotiAlertType, DateTime DueDate)
        {
            var result = sc.Notifications.Where(x => x.RopeId == RopeID & x.NotificationDueDate == DueDate & x.NotificationAlertType == NotiAlertType).FirstOrDefault();
            if (result == null)
            {
                if (NotiAlertType == 1 && DateTime.Now.Date >= DueDate.Date.AddDays(-7))
                {
                    NotificationsClass noti = new NotificationsClass();
                    noti.Acknowledge = false;
                    noti.AckRecord = "Not yet acknowledged";
                    noti.Notification = NotiMsg;
                    noti.RopeId = RopeID;
                    noti.IsActive = true;
                    noti.NotificationDueDate = DueDate;

                    // var notiDate = NotiAlertType == 1 ? DateTime.Now : NotiAlertType == 2 ? DueDate.AddDays(-1) : DueDate.AddDays(-1);
                    noti.CreatedDate = DateTime.Now;
                    noti.CreatedBy = "Admin";
                    noti.NotificationAlertType = NotiAlertType;
                    noti.NotificationType = 1;
                    sc.Notifications.Add(noti);
                    sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                }
                if (NotiAlertType == 2 && DateTime.Now.Date >= DueDate.Date.AddDays(-1))
                {
                    NotificationsClass noti = new NotificationsClass();
                    noti.Acknowledge = false;
                    noti.AckRecord = "Not yet acknowledged";
                    noti.Notification = NotiMsg;
                    noti.RopeId = RopeID;
                    noti.IsActive = true;
                    noti.NotificationDueDate = DueDate;

                    // var notiDate = NotiAlertType == 1 ? DateTime.Now : NotiAlertType == 2 ? DueDate.AddDays(-1) : DueDate.AddDays(-1);
                    noti.CreatedDate = DateTime.Now;
                    noti.CreatedBy = "Admin";
                    noti.NotificationAlertType = NotiAlertType;
                    noti.NotificationType = 1;
                    sc.Notifications.Add(noti);
                    sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                }
                if (NotiAlertType == 3 && DateTime.Now.Date >= DueDate.Date.AddDays(1))
                {
                    NotificationsClass noti = new NotificationsClass();
                    noti.Acknowledge = false;
                    noti.AckRecord = "Not yet acknowledged";
                    noti.Notification = NotiMsg;
                    noti.RopeId = RopeID;
                    noti.IsActive = true;
                    noti.NotificationDueDate = DueDate;

                    // var notiDate = NotiAlertType == 1 ? DateTime.Now : NotiAlertType == 2 ? DueDate.AddDays(-1) : DueDate.AddDays(-1);
                    noti.CreatedDate = DateTime.Now;
                    noti.CreatedBy = "Admin";
                    noti.NotificationAlertType = NotiAlertType;
                    noti.NotificationType = 1;
                    sc.Notifications.Add(noti);
                    sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                }
                if (NotiAlertType > 3)
                {
                    NotificationsClass noti = new NotificationsClass();
                    noti.Acknowledge = false;
                    noti.AckRecord = "Not yet acknowledged";
                    noti.Notification = NotiMsg;
                    noti.RopeId = RopeID;
                    noti.IsActive = true;
                    noti.NotificationDueDate = DueDate;

                    // var notiDate = NotiAlertType == 1 ? DateTime.Now : NotiAlertType == 2 ? DueDate.AddDays(-1) : DueDate.AddDays(-1);
                    noti.CreatedDate = DateTime.Now;
                    noti.CreatedBy = "Admin";
                    noti.NotificationAlertType = NotiAlertType;
                    noti.NotificationType = 1;
                    sc.Notifications.Add(noti);
                    sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                }

            }
        }
        public void InspectNotification(int RopeID, string NotiMsg, int NotiAlertType)
        {
            var result = sc.Notifications.Where(x => x.RopeId == RopeID & x.NotificationAlertType == NotiAlertType).FirstOrDefault();
            if (result == null)
            {
                NotificationsClass noti = new NotificationsClass();
                noti.Acknowledge = false;
                noti.AckRecord = "Not yet acknowledged";
                noti.Notification = NotiMsg;
                noti.RopeId = RopeID;
                noti.IsActive = true;
                noti.NotificationDueDate = DateTime.Now.Date;
                noti.CreatedDate = DateTime.Now;
                noti.CreatedBy = "Admin";
                noti.NotificationAlertType = NotiAlertType;
                noti.NotificationType = 1;
                sc.Notifications.Add(noti);
                sc.SaveChanges();

                StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
            }
        }

        public void InspectNotification3(int RopeID, string NotiMsg, int NotiAlertType)
        {
            var result = sc.Notifications.Where(x => x.RopeId == RopeID & x.NotificationAlertType == NotiAlertType).FirstOrDefault();
            if (result == null)
            {
                NotificationsClass noti = new NotificationsClass();
                noti.Acknowledge = false;
                //noti.AckRecord = "Not yet acknowledged";
                noti.AckRecord = "This notification cannot be acknowledged, kindly discard it";
                noti.Notification = NotiMsg;
                noti.RopeId = RopeID;
                noti.IsActive = true;
                noti.NotificationDueDate = DateTime.Now.Date;
                noti.CreatedDate = DateTime.Now;
                noti.CreatedBy = "Admin";
                noti.NotificationAlertType = NotiAlertType;
                noti.NotificationType = 1;
                sc.Notifications.Add(noti);
                sc.SaveChanges();

                StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                var assignRope = sc.AssignRopetoWinch.Where(x => x.IsActive == true && x.RopeTail == 0).ToList();
                var mrRope = sc.MooringWinchRope.Where(x => x.DeleteStatus == false).ToList();
                var rpInspSetting = sc.RopeInspectionSetting.ToList();
                var ropeType = sc.MooringRopeType.ToList();
                var winchtb = sc.MooringWinch.Where(x => x.IsActive == true).ToList();

                var rpendtoend = sc.RopeEndtoEnd2.ToList();

                foreach (var item in assignRope)
                {
                    var ropetypeid = mrRope.Where(x => x.Id == item.RopeId).Select(x => x.RopeTypeId).SingleOrDefault();
                    var manufacid = mrRope.Where(x => x.Id == item.RopeId).Select(x => x.ManufacturerId).SingleOrDefault();
                    //var ropename = ropeType.Where(x => x.Id == ropetypeid).Select(x => x.RopeType).SingleOrDefault();

                    var ropename = mrRope.Where(x => x.Id == item.RopeId).Select(x => x.CertificateNumber).SingleOrDefault();

                    var WinchName = winchtb.Where(x => x.Id == item.WinchId).Select(x => x.AssignedNumber).SingleOrDefault();
                    var WinchLocation = winchtb.Where(x => x.Id == item.WinchId).Select(x => x.Location).SingleOrDefault();
                    var instdt = mrRope.Where(x => x.Id == item.RopeId).Select(x => x.InstalledDate).SingleOrDefault();

                    var enetoenddonedt = rpendtoend.Where(x => x.RopeId == item.RopeId).Select(x => x.EndtoEndDoneDate).SingleOrDefault();

                    DateTime firstDate = Convert.ToDateTime(instdt);
                    DateTime secondDate = DateTime.Now;
                    DateTime now = DateTime.UtcNow;
                    DateTime past = firstDate;
                    int monthDiff = GetMonthDifference(now, past);

                    int settingmnth = rpInspSetting.Where(x => x.MooringRopeType == ropetypeid && x.ManufacturerType == manufacid).Select(x => x.MaximumMonthsAllowed).SingleOrDefault();

                    if (settingmnth != 0)
                    {
                        if (settingmnth >= monthDiff)
                        {
                            decimal percentchek = settingmnth - (Convert.ToDecimal(settingmnth * 10) / 100);

                            int mnth = Convert.ToInt32(percentchek);
                            if (monthDiff > percentchek)
                            {
                                var notification = "Line getting due for replacement completed - '" + mnth + "' monhts- Line '" + ropename + "' on Winch " + WinchName + " located at " + WinchLocation + " ";
                                NotificationsClass noti = new NotificationsClass();
                                noti.Acknowledge = false;
                                noti.AckRecord = "Not yet acknowledged";
                                noti.Notification = notification;
                                noti.NotificationType = 1;
                                noti.RopeId = item.RopeId;
                                noti.IsActive = true;
                                //noti.NotificationDueDate = DBNull.Value;
                                noti.CreatedDate = DateTime.Now;
                                noti.CreatedBy = "Admin";
                                sc.Notifications.Add(noti);
                                sc.SaveChanges();

                                StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);

                            }
                            else
                            {
                                var notification = "Line getting due for replacement completed - '" + mnth + "' monhts- Line '" + ropename + "' on Winch " + WinchName + " located at " + WinchLocation + " ";
                                NotificationsClass noti = new NotificationsClass();
                                noti.Acknowledge = false;
                                noti.AckRecord = "Not yet acknowledged";
                                noti.Notification = notification;
                                noti.NotificationType = 2;
                                noti.RopeId = item.RopeId;
                                noti.IsActive = true;
                                //noti.NotificationDueDate = DBNull.Value;
                                noti.CreatedDate = DateTime.Now;
                                noti.CreatedBy = "Admin";
                                sc.Notifications.Add(noti);
                                sc.SaveChanges();

                                StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                            }
                        }

                        //// Notification send on the bases of RopeEnd to End

                        DateTime firstDate1 = Convert.ToDateTime(enetoenddonedt);
                        DateTime secondDate1 = DateTime.Now;
                        DateTime now1 = DateTime.UtcNow;
                        DateTime past1 = firstDate1;
                        int monthDiff1 = GetMonthDifference(now1, past1);

                        int settingmnth1 = rpInspSetting.Where(x => x.MooringRopeType == ropetypeid && x.ManufacturerType == manufacid).Select(x => x.MaximumMonthsAllowed).SingleOrDefault();

                        if (settingmnth1 != 0)
                        {
                            if (settingmnth1 >= monthDiff1)
                            {
                                decimal percentchek = settingmnth1 - (Convert.ToDecimal(settingmnth1 * 10) / 100);

                                int mnth = Convert.ToInt32(percentchek);
                                if (monthDiff1 > percentchek)
                                {
                                    var notification = "Line getting due for rotation and end to end 30days before";
                                    NotificationsClass noti = new NotificationsClass();
                                    noti.Acknowledge = false;
                                    noti.AckRecord = "Not yet acknowledged";
                                    noti.Notification = notification;
                                    noti.NotificationType = 1;
                                    noti.RopeId = item.RopeId;
                                    noti.IsActive = true;
                                    //noti.NotificationDueDate = DBNull.Value;
                                    noti.CreatedDate = DateTime.Now;
                                    noti.CreatedBy = "Admin";
                                    sc.Notifications.Add(noti);
                                    sc.SaveChanges();

                                    StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);

                                }
                                else
                                {
                                    var notification = "Lines rotation and end to end - Overdue";
                                    NotificationsClass noti = new NotificationsClass();
                                    noti.Acknowledge = false;
                                    noti.AckRecord = "Not yet acknowledged";
                                    noti.Notification = notification;
                                    noti.NotificationType = 2;
                                    noti.RopeId = item.RopeId;
                                    noti.IsActive = true;
                                    //noti.NotificationDueDate = DBNull.Value;
                                    noti.CreatedDate = DateTime.Now;
                                    noti.CreatedBy = "Admin";
                                    sc.Notifications.Add(noti);
                                    sc.SaveChanges();

                                    StaticHelper.AlarmFunction(1, StaticHelper.AlarmCheck);
                                }
                            }



                        }
                    }
                }
            }
            catch { }
        }


        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }



    }

}
