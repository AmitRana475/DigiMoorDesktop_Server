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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WorkShipVersionII.Commands;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
   public class ResidualLabTestListViewModel:ViewModelBase
    {

        private readonly ShipmentContaxt sc;
        private static int itemPerPage = 15;
        private int itemcount;
        public ICommand HelpCommand { get; private set; }
        public ResidualLabTestListViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            downloadCommand = new RelayCommand<ResidualLabTestClass>(Downloadfile);
            viewCommand = new RelayCommand<ResidualLabTestClass>(ViewResLabTest);
            editCommand = new RelayCommand<ResidualLabTestClass>(EditMooringWinch);
            deleteCommand = new RelayCommand<ResidualLabTestClass>(DeleteMooringWinch);


            GetResidualtestList();
          

           

            NextCommand = new NextPageCommandResidual(this);
            PreviousCommand = new PreviousPageCommandResidual(this);
            FirstCommand = new FirstPageCommandResidual(this);
            LastCommand = new LastPageCommandResidual(this);
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

        }

        public ResidualLabTestListViewModel(ObservableCollection<ResidualLabTestClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

         
            LoadUserAccess.Clear();
            downloadCommand = new RelayCommand<ResidualLabTestClass>(Downloadfile);
            GetResidualtestList();
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

        }
        #region PaginationWork

        public ICommand PreviousCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand LastCommand { get; private set; }

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

        private void View_Filter(object sender, FilterEventArgs e)
        {
            try
            {
                //int index = ((MOperationBirthDetail)e.Item).OPId + 1;
                //if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
                int index = ((ResidualLabTestClass)e.Item).RowId - 1;
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

        #endregion


        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }
        private static string searchCrew1;
        private ICommand editCommand;
        public ICommand EditCommand
        {
            get { return editCommand; }
            set { editCommand = value; }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }
        private ICommand downloadCommand;
        public ICommand DownloadCommand
        {
            get { return downloadCommand; }
            set { downloadCommand = value; }
        }
        private static ObservableCollection<MooringWinchRopeClass> loadMooringWinchList = new ObservableCollection<MooringWinchRopeClass>();

        public ObservableCollection<MooringWinchRopeClass> LoadMooringWinchRopeList
        {
            get
            {
                return loadMooringWinchList;
            }
            set
            {
                loadMooringWinchList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringWinchRopeList"));

            }
        }


        private void ViewResLabTest(ResidualLabTestClass mw)
        {
            try
            {
                StaticHelper.ViewId = mw.Id;
                StaticHelper.RopeTailId = 0;
                ChildWindowManager.Instance.ShowChildWindow(new ViewResidualLabTest());

            }
            catch (Exception ex)
            {
               // sc.ErrorLog(ex);
            }
        }
        // public static CollectionViewSource ViewList { get; set; }
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
        public static ObservableCollection<ResidualLabTestClass> loadUserAccess = new ObservableCollection<ResidualLabTestClass>();

       // public static ObservableCollection<ResidualLabTestClass> loadUserAccess = new ObservableCollection<ResidualLabTestClass>();
        public ObservableCollection<ResidualLabTestClass> LoadUserAccess
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



        private void Downloadfile(ResidualLabTestClass obj)
        {
            try
            {
                // AddMooringWinchRopeViewModel vm = new AddMooringWinchRopeViewModel(obj);
                StaticHelper.ViewId = obj.Id;

                if (obj.AttachmentPath == "")
                {
                    System.Windows.Forms.MessageBox.Show("Attachment not found !", "Mooring Line", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {

                    string ServerName = StaticHelper.ServerName;
                    string path = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + obj.AttachmentPath + "";

                    System.Diagnostics.Process.Start(path);
                    // ChildWindowManager.Instance.ShowChildWindow(new ViewTrainingAttachment());
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Attachment file not found on this path !", "Residual Line", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                //sc.ErrorLog(ex);
            }
        }
        public void GetResidualtestList()
        {
            try
            {
                int rowid = 1;
                LoadUserAccess.Clear();
                SqlCommand cmd = new SqlCommand("ResidualList", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RopeTail", 0);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess.Add(new ResidualLabTestClass()
                    {
                        RowId=rowid++,
                        Id = (int)row["Id"],
                        CertificateNumber = (string)row["certificatenumber"],
                        UniqueId = (string)row["UniqueId"],
                        TestResults = Convert.ToDecimal((row["TestResults"] == DBNull.Value) ? 0.00 : row["TestResults"]),

                        RopeType = (string)row["RopeType"],
                        RopeTypeId = (int)row["RopeTypeId"],
                        ManufacturerId = (int)row["ManufacturerId"],
                        LabTestDate = (Convert.ToDateTime(row["LabTestDate"])),
                        RunningHours = Convert.ToInt32(row["RunningHours"] == DBNull.Value ? null : row["RunningHours"]),

                        Remarks = (row["Remarks"] == DBNull.Value) ? string.Empty : row["Remarks"].ToString(),
                        Name = (row["name"] == DBNull.Value) ? string.Empty : row["name"].ToString(),
                        Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                        AttachmentPath = (row["AttachmentPath"] == DBNull.Value) ? "" : row["AttachmentPath"].ToString(),
                        AttachmentVisibility = (row["AttachmentPath"] == DBNull.Value) ? "Hidden" : "Visible",
                    });
                }
                // OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                RaisePropertyChanged("LoadUserAccess");



                ViewList = new CollectionViewSource
                {
                    Source = LoadUserAccess
                };


                ViewList.Filter += new FilterEventHandler(View_Filter);


                itemcount = LoadUserAccess.Count(); //sc.Notifications.Count();
                CalculateTotalPages();
                ViewList.View.Refresh();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }
        }


     
        private void EditMooringWinch(ResidualLabTestClass mw)
        {
            try
            {
                //AddMooringWinchRopeViewModel vm = new AddMooringWinchRopeViewModel(mw);
                //ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchRopeView() { DataContext = vm });
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void DeleteMooringWinch(ResidualLabTestClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ResidualLabTestClass findrank = sc.ResidualLabTestTbl.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findrank != null)
                    {
                        sc.Entry(findrank).State = EntityState.Deleted;
                        sc.SaveChanges();

                        int NotiAlertType = (int)NotificationAlertType.RopeResidual_StrengthCheck;
                        var result = sc.Notifications.Where(x => x.RopeId == mw.RopeId & x.NotificationAlertType == NotiAlertType).FirstOrDefault();

                        if (result != null)
                        {
                            sc.Notifications.Remove(result);
                            sc.SaveChanges();

                        }

                        try
                        {
                            SqlDataAdapter adp223 = new SqlDataAdapter("select * from MooringRopeAttachment where ResidualID='" + mw.Id + "' and LineResidual='ResidualLine' and RopeTail=1", sc.con);
                            DataTable dt223 = new DataTable();
                            adp223.Fill(dt223);
                            if (dt223.Rows.Count > 0)
                            {
                                string filename = dt223.Rows[0]["AttachmentPath"].ToString();

                                string ServerName = StaticHelper.ServerName;
                                string mypath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + filename + "";

                                FileInfo file = new FileInfo(mypath);
                                if (file.Exists)//check file exsit or not  
                                {
                                    file.Delete();


                                    SqlDataAdapter adp221 = new SqlDataAdapter("delete from MooringRopeAttachment where ResidualID='" + mw.Id + "' and LineResidual='ResidualLine' and RopeTail=1", sc.con);
                                    DataTable dt221 = new DataTable();
                                    adp221.Fill(dt221);

                                }
                            }
                        }
                        catch { }



                        MessageBox.Show("Record deleted successfully ", "Residual Line", MessageBoxButton.OK, MessageBoxImage.Information);

                        //.....Refresh DataGrid........

                        GetResidualtestList();

                        //var lostdata = new ObservableCollection<ResidualLabTestClass>(sc.ResidualLabTestTbl.ToList());
                        //ResidualLabTestListViewModel cc = new ResidualLabTestListViewModel(lostdata);


                    }
                    else
                    {
                        MessageBox.Show("Record is not found ", "Delete Residual ", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }

            }
            catch (Exception ex)
            {
                //sc.ErrorLog(ex);
            }
        }


        private void Below21RopesAtDeleteTime()
        {
            try
            {   // minimum required of 21 Ropes ---------------------------------
                SqlDataAdapter adp = new SqlDataAdapter("select COUNT(*) from MooringRopeDetail where OutofServiceDate is null and RopeTail=0 and DeleteStatus=0", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count >= 0)
                {
                    int cnt = Convert.ToInt32(dt.Rows[0][0]);
                    if (cnt < StaticHelper.MinimumRope)
                    {
                        var notification = "Active lines below minimum required of "+StaticHelper.MinimumRope+" Lines including spare";

                        SqlDataAdapter act = new SqlDataAdapter("select COUNT(*) from Notifications where Notification='Active lines below minimum required of " + StaticHelper.MinimumRope + " Lines including spare'", sc.con);
                        DataTable dd = new DataTable();
                        act.Fill(dd);

                        int cntnoti = Convert.ToInt32(dd.Rows[0][0]);
                        if (cntnoti == 0)
                        {
                            NotificationsClass noti = new NotificationsClass();
                            noti.Acknowledge = false;
                            noti.AckRecord = "Not yet acknowledged";
                            noti.Notification = notification;
                            noti.RopeId = 0;
                            noti.IsActive = true;
                            noti.NotificationType = 1;
                            //noti.NotificationDueDate = notidueMonth;
                            noti.CreatedDate = DateTime.Now;
                            noti.CreatedBy = "Admin";
                            noti.NotificationAlertType = (int)NotificationAlertType.Minimum21RopeCount;
                            sc.Notifications.Add(noti);
                            sc.SaveChanges();

                            StaticHelper.AlarmFunction(1, true);

                        }

                        act.Dispose();
                        dd.Dispose();
                    }
                    else
                    {
                        // delete Above Notification
                        SqlDataAdapter act = new SqlDataAdapter("delete from Notifications where NotificationAlertType = 17", sc.con);
                        DataTable dd = new DataTable();
                        act.Fill(dd);
                        act.Dispose();
                        dd.Dispose();
                    }
                }

                dt.Dispose();
                adp.Dispose();


            }
            catch { }
        }




        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }
}
