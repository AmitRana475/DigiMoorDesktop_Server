using ClosedXML.Excel;
using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
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

       public class MooringWinchTailViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private static int itemPerPage = 5;
              private BackgroundWorker _ExportMooringWinchTail;
              private int itemcount;
              public ICommand HelpCommand { get; private set; }

              public MooringWinchTailViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportMooringWinchTail = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
            downloadCommand = new RelayCommand<MooringWinchRopeClass>(Downloadfile);
            editCommand = new RelayCommand<MooringWinchRopeClass>(EditMooringWinch);
                     deleteCommand = new RelayCommand<MooringWinchRopeClass>(DeleteMooringWinch);

                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     //LoadMooringWinchList.Clear();

                     // LoadMooringWinchList = sc.MooringWinch.ToList();
                     //var data = sc.MooringWinch.ToList();
                     // sc.ObservableCollectionList(LoadMooringWinchList, GetMooringWinchList());
                     //GetMooringWinchRopeList();
                     // OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringWinchList"));
                     GetMooringWinchRopeList();
                     GetMooringWinchRopeList2();

                     this._ExportMooringWinchTailCommands = new RelayCommand(() => _ExportMooringWinchTail.RunWorkerAsync(), () => !_ExportMooringWinchTail.IsBusy);
                     this._ExportMooringWinchTail.DoWork += new DoWorkEventHandler(ExportMooringWinchTailMethod);


                     NextCommand = new NextPageCommandMooringRopeTail(this);
                     PreviousCommand = new PreviousPageCommandMooringRopeTail(this);
                     FirstCommand = new FirstPageCommandMooringRopeTail(this);
                     LastCommand = new LastPageCommandMooringRopeTail(this);

              }

              public MooringWinchTailViewModel(ObservableCollection<MooringWinchRopeClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     LoadUserAccess.Clear();
            downloadCommand = new RelayCommand<MooringWinchRopeClass>(Downloadfile);
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

                     GetMooringWinchRopeList();
                     GetMooringWinchRopeList2();

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
                            int index = ((MooringWinchRopeClass)e.Item).RowId - 1;
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


              //ExportExcel
              private ICommand _ExportMooringWinchTailCommands;
              public ICommand ExportMooringWinchTailCommands
              {
                     get
                     {

                            return _ExportMooringWinchTailCommands;
                     }
              }
        private ICommand downloadCommand;
        public ICommand DownloadCommand
        {
            get { return downloadCommand; }
            set { downloadCommand = value; }
        }
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


              public static ObservableCollection<MooringWinchRopeClass> loadUserAccess = new ObservableCollection<MooringWinchRopeClass>();
              public ObservableCollection<MooringWinchRopeClass> LoadUserAccess
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
              public static ObservableCollection<MooringWinchRopeClass> loadUserAccess1 = new ObservableCollection<MooringWinchRopeClass>();

              // public static ObservableCollection<MooringWinchRopeClass> loadUserAccess1 = new ObservableCollection<MooringWinchRopeClass>();
              public ObservableCollection<MooringWinchRopeClass> LoadUserAccess1
              {
                     get
                     {
                            return loadUserAccess1;
                     }
                     set
                     {
                            loadUserAccess1 = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess1"));
                     }
              }
        //private ObservableCollection<MooringWinchRopeClass> GetMooringWinchRopeList()
        //{

        //    try
        //    {
        //        ObservableCollection<MooringWinchRopeClass> moringlist = new ObservableCollection<MooringWinchRopeClass>();

        //        var data = sc.MooringWinchRope.ToList();
        //        foreach (var item in data)
        //        {
        //            moringlist.Add(new MooringWinchRopeClass()
        //            {
        //                Id = item.Id,
        //                RopeType = item.RopeType,
        //                RopeConstruction = item.RopeConstruction,
        //                DiaMeter = item.DiaMeter,
        //                Length = item.Length,
        //                MBL = item.MBL,
        //                LDBF = item.LDBF,
        //                WLL = item.WLL,
        //                ManufacturerName = item.ManufacturerName,
        //                CertificateNumber = item.CertificateNumber,
        //                ReceivedDate = item.ReceivedDate,

        //                InstalledDate = item.InstalledDate,
        //                RopeTagging = item.RopeTagging,

        //                OutofServiceDate = item.OutofServiceDate,
        //                ReasonOutofService = item.ReasonOutofService,
        //                DamageObserved = item.DamageObserved,
        //                MooringOperation = item.MooringOperation,

        //            });


        //        }
        //        return moringlist;
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //        return null;
        //    }

        //}

        
        private void Downloadfile(MooringWinchRopeClass obj)
        {
            try
            {
                //AddMooringWinchRopeViewModel vm = new AddMooringWinchRopeViewModel(obj);
                StaticHelper.ViewId = obj.Id;

                if (obj.AttachmentPath == "" || obj.AttachmentPath == null)
                {
                    MessageBox.Show("Attachment not found !", "Mooring RopeTail", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("Attachment file not found on this path !", "Mooring RopeTail", MessageBoxButton.OK, MessageBoxImage.Information);
                //sc.ErrorLog(ex);
            }
        }

        private void GetMooringWinchRopeList1()
              {

                     try
                     {
                            LoadUserAccess.Clear();

                            var data = sc.MooringWinchRope.Where(x => x.RopeTail == 1 && x.DeleteStatus == false).ToList();
                            foreach (var item in data)
                            {
                                   LoadUserAccess.Add(new MooringWinchRopeClass()
                                   {
                                          Id = item.Id,
                                          RopeTypeId = item.RopeTypeId,
                                          RopeType = sc.MooringRopeType.Where(x => x.Id == item.RopeTypeId).Select(x => x.RopeType).SingleOrDefault(),
                                          RopeConstruction = item.RopeConstruction,
                                          DiaMeter = item.DiaMeter,
                                          Length = item.Length,
                                          MBL = item.MBL,
                                          LDBF = item.LDBF,
                                          WLL = item.WLL,
                                          ManufacturerId = item.ManufacturerId,
                                          ManufacturerName = sc.CommonManuF.Where(x => x.Id == item.ManufacturerId).Select(x => x.Name).SingleOrDefault(),
                                          CertificateNumber = item.CertificateNumber,
                                          ReceivedDate = item.ReceivedDate,

                                          InstalledDate = item.InstalledDate,
                                          RopeTagging = item.RopeTagging,

                                          OutofServiceDate = item.OutofServiceDate,
                                          ReasonOutofService = item.ReasonOutofService,
                                          DamageObserved = item.DamageObserved,
                                          MooringOperation = item.MooringOperation,

                                          Location = sc.AssignRopetoWinch.Where(x => x.RopeId == item.Id).Select(x => x.AssignedLocation).SingleOrDefault(),

                                          WinchId = sc.AssignRopetoWinch.Where(x => x.RopeId == item.Id).Select(x => x.WinchId).SingleOrDefault(),

                                          //AssignedWinch=sc.MooringWinch.Where(x=> x.Id==WinchId).se

                                   });


                            }

                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                            // return moringlist;
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                            // return null;
                     }

              }

              public void GetMooringWinchRopeList()
              {
                     try
                     {
                            LoadUserAccess.Clear();
                            SqlCommand cmd = new SqlCommand("GetMooringRopeDetailList", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", 1);
                cmd.Parameters.AddWithValue("@AttachedText", "RopeTail");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            if (ds.Tables[0].Rows.Count < StaticHelper.MinimumRopeTail)
                            {
                                   Below21TailsAtDeleteTime();
                            }
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                   LoadUserAccess.Add(new MooringWinchRopeClass()
                                   {
                                          // var data = Convert.ToDateTime(row["StartOn"].ToString()).ToString("MMM dd").ToString()
                                          Id = (int)row["Id"],
                                          RopeTail = 1,
                                          //AssignedWinch = (string)row["AssignedWinch"],
                                          CertificateNumber = (string)row["CertificateNumber"],
                                          UniqueID = (row["UniqueID"] == DBNull.Value) ? "" : row["UniqueID"].ToString(),
                                          LDBF = (decimal)row["LDBF"],
                                          //MBL = (decimal)row["MBL"],
                                          DiaMeter = (decimal)row["DiaMeter"],
                                          Length = (decimal)row["Length"],
                                          WLL = (decimal)row["WLL"],
                                          ReceivedDate = (DateTime)row["ReceivedDate"],
                                          RopeTagging = (string)row["RopeTagging"],
                                          RopeConstruction = (string)row["RopeConstruction"],
                                          //InstalledDate = (DateTime)row["InstalledDate"],
                                          RopeType = (string)row["RopeType"],
                                          RopeTypeId = (int)row["RopeTypeId"],
                                          ManufacturerId = (int)row["ManufacturerId"],
                                          //InstalledDate1 = ((DateTime)row["InstalledDate"]).ToString("d MMM, yyyy"),
                                          InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),
                                          CurrentRunningHours = (row["CurrentRunningHours"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["CurrentRunningHours"]),
                                          StartCounterHours = (row["StartCounterHours"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["StartCounterHours"]),
                                          ManufacturerName = (row["ManufacturerName"] == DBNull.Value) ? string.Empty : row["ManufacturerName"].ToString(),
                                          InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? DateTime.Now : row["InstalledDate"]),
                                          Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),

                                          Remarks = (row["Remarks"] == DBNull.Value) ? string.Empty : row["Remarks"].ToString(),

                                          AssignedWinch = (row["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : row["AssignedWinch"].ToString(),

                                       AttachmentPath = (row["attachment"] == DBNull.Value) ? string.Empty : row["attachment"].ToString(),
                                       AttachmentVisibility = (row["attachment"] == DBNull.Value) ? "Hidden" : "Visible",
                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }
              }


              public void GetMooringWinchRopeList2()
              {
                     try
                     {
                            int rowid = 1;
                            LoadUserAccess1.Clear();
                            SqlCommand cmd = new SqlCommand("GetMooringRopeDetailList", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", 1);
                cmd.Parameters.AddWithValue("@AttachedText", "RopeTail");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            if (ds.Tables[1].Rows.Count < StaticHelper.MinimumRopeTail)
                            {
                                   Below21TailsAtDeleteTime();
                            }
                            foreach (DataRow row in ds.Tables[1].Rows)
                            {
                                   LoadUserAccess1.Add(new MooringWinchRopeClass()
                                   {
                                          // var data = Convert.ToDateTime(row["StartOn"].ToString()).ToString("MMM dd").ToString()
                                          RowId = rowid++,
                                          RopeTail = 1,
                                          Id = (int)row["Id"],
                                          //AssignedWinch = (string)row["AssignedWinch"],
                                          CertificateNumber = (string)row["CertificateNumber"],
                                          UniqueID = (row["UniqueID"] == DBNull.Value) ? "" : row["UniqueID"].ToString(),
                                          LDBF = (decimal)row["LDBF"],
                                          //MBL = (decimal)row["MBL"],
                                          DiaMeter = (decimal)row["DiaMeter"],
                                          Length = (decimal)row["Length"],
                                          WLL = (decimal)row["WLL"],
                                          ReceivedDate = (DateTime)row["ReceivedDate"],
                                          RopeTagging = (string)row["RopeTagging"],
                                          RopeConstruction = (string)row["RopeConstruction"],
                                          // InstalledDate = (DateTime)row["InstalledDate"],
                                          RopeType = (string)row["RopeType"],
                                          RopeTypeId = (int)row["RopeTypeId"],
                                          ManufacturerId = (int)row["ManufacturerId"],
                                          // InstalledDate1 = ((DateTime)row["InstalledDate"]).ToString("d MMM, yyyy"),
                                          Remarks = (row["Remarks"] == DBNull.Value) ? string.Empty : row["Remarks"].ToString(),
                                          InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? DateTime.Now : row["InstalledDate"]),
                                          InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),
                                          CurrentRunningHours = (row["CurrentRunningHours"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["CurrentRunningHours"]),
                                          ManufacturerName = (row["ManufacturerName"] == DBNull.Value) ? string.Empty : row["ManufacturerName"].ToString(),
                                          Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                                          AssignedWinch = (row["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : row["AssignedWinch"].ToString(),
                                       AttachmentPath = (row["attachment"] == DBNull.Value) ? string.Empty : row["attachment"].ToString(),
                                       AttachmentVisibility = (row["attachment"] == DBNull.Value) ? "Hidden" : "Visible",
                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess1"));

                            //RaisePropertyChanged("LoadUserAccess1");



                            //ViewList = new CollectionViewSource
                            //{
                            //       Source = LoadUserAccess1
                            //};


                            //ViewList.Filter += new FilterEventHandler(View_Filter);


                            //itemcount = LoadUserAccess1.Count(); //sc.Notifications.Count();
                            //CalculateTotalPages();
                            //ViewList.View.Refresh();

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }
              }


              private static string searchCrew;
              public string SearchCrew
              {
                     get
                     {

                            if (!string.IsNullOrEmpty(searchCrew))
                            {
                                   var data = sc.GetMooringWinchRopeTail(searchCrew).ToList();
                                   LoadUserAccess.Clear();
                                   sc.ObservableCollectionList(LoadUserAccess, data);
                                   RaisePropertyChanged("LoadUserAccess");

                            }
                            else
                            {
                                   loadUserAccess.Clear();
                                   RaisePropertyChanged("LoadUserAccess");
                                   GetMooringWinchRopeList();
                            }
                            return searchCrew;


                     }

                     set
                     {
                            searchCrew = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("SearchCrew"));
                            //GetMooringWinchRopeList();
                     }
              }

              private static string searchCrew1;
              public string SearchCrew1
              {
                     get
                     {

                            if (!string.IsNullOrEmpty(searchCrew1))
                            {
                                   var data = sc.GetMooringWinchRopeTail1(searchCrew1).ToList();
                                   LoadUserAccess1.Clear();
                                   sc.ObservableCollectionList(LoadUserAccess1, data);
                                   RaisePropertyChanged("LoadUserAccess1");

                            }
                            else
                            {
                                   loadUserAccess1.Clear();
                                   RaisePropertyChanged("LoadUserAccess1");
                                   GetMooringWinchRopeList2();
                            }
                            return searchCrew1;
                     }

                     set
                     {
                            searchCrew1 = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("SearchCrew1"));
                     }
              }


              private void EditMooringWinch(MooringWinchRopeClass mw)
              {
                     try
                     {
                            AddMooringWinchTailViewModel vm = new AddMooringWinchTailViewModel(mw);
                            //ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchRopeView() { DataContext = vm });
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

        static string msgcheck = "";
        static void connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            // this gets the print statements (maybe the error statements?)
            msgcheck = e.Message;


            string[] stringSeparators = new string[] { "\r\n" };
            string text = msgcheck;
            string[] lines = text.Split(stringSeparators, StringSplitOptions.None);
            // msgcheck = "\n" + e.Message;

            msgcheck = lines[0];

            if (msgcheck != "")
            {
                return;
            }
        }

        private void DeleteMooringWinch(MooringWinchRopeClass mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {

                    SqlDataAdapter adp2 = new SqlDataAdapter("select distinct OperationId from MOUsedWinchTbl where RopeId=" + mw.Id + "", sc.con);
                    //adp2.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //adp2.SelectCommand.Parameters.AddWithValue("@RopeId", mw.Id);
                    DataTable dt2 = new DataTable();
                    adp2.Fill(dt2);

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        int operationid = Convert.ToInt32(dt2.Rows[i][0]);

                        SqlDataAdapter adp22 = new SqlDataAdapter("select * from MOperationBirthDetail where opid=" + operationid + " and IsActive=1", sc.con);
                        DataTable dt22 = new DataTable();
                        adp22.Fill(dt22);

                        if (dt22.Rows.Count > 0)
                        {
                            MessageBox.Show("This ropetail cannot be deleted, as already marked as operation, please delete from operation records first", "Mooring Line", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }


                    //using (var command = new SqlCommand("DeleteCheckInMooringRopeDetail", sc.con))
                    //{
                    //    command.CommandType = CommandType.StoredProcedure;
                    //    command.Parameters.AddWithValue("@RopeId", mw.Id);
                    //    command.Parameters.AddWithValue("@Ropetail", "RopeTail");

                    //    sc.con.Open();
                    //    // wire up an event handler to the connection.InfoMessage event
                    //    sc.con.InfoMessage += connection_InfoMessage;
                    //    var result = command.ExecuteNonQuery();
                    //    sc.con.Close();
                    //}

                    SqlDataAdapter adp228 = new SqlDataAdapter("DeleteCheckInMooringRopeDetail", sc.con);
                    adp228.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adp228.SelectCommand.Parameters.AddWithValue("@RopeId", mw.Id);
                    adp228.SelectCommand.Parameters.AddWithValue("@Ropetail", "RopeTail");
                    //    command.Parameters.AddWithValue("@Ropetail", "Line");
                    DataTable dt228 = new DataTable();
                    adp228.Fill(dt228);

                    if (dt228.Rows.Count > 0)
                    {
                        msgcheck = dt228.Rows[0][0].ToString();
                    }

                    try
                    {
                        SqlDataAdapter adp223 = new SqlDataAdapter("select * from MooringRopeAttachment where ropeid='" + mw.Id + "' and LineResidual='RopeTail' and RopeTail=1", sc.con);
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


                                SqlDataAdapter adp221 = new SqlDataAdapter("delete from MooringRopeAttachment where ropeid='" + mw.Id + "' and LineResidual='RopeTail' and RopeTail=1", sc.con);
                                DataTable dt221 = new DataTable();
                                adp221.Fill(dt221);

                            }
                        }
                    }
                    catch { }


                    if (msgcheck != "")
                    {
                        //MessageBox.Show("This line cannot be deleted, as already marked as inspected, please delete from inspection records first", "Mooring Line", MessageBoxButton.OK, MessageBoxImage.Information);
                        MessageBox.Show(msgcheck, "Mooring Line", MessageBoxButton.OK, MessageBoxImage.Information);
                        msgcheck = "";
                        return;
                    }
                    else
                    {
                        MooringWinchRopeClass findrank = sc.MooringWinchRope.Where(x => x.Id == mw.Id && x.DeleteStatus == false).FirstOrDefault();
                                          if (findrank != null)
                                          {

                                                 var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == mw.Id && b.DeleteStatus == false);
                                                 if (result != null)
                                                 {

                                                        result.DeleteStatus = true;
                                                        result.ModifiedDate = DateTime.Now;
                                                        sc.SaveChanges();

                                                        Below21TailsAtDeleteTime();
                                                 }

                                                 try
                                                 {
                                // SqlDataAdapter adp1 = new SqlDataAdapter("update notifications set IsActive=0 where ropeid=" + mw.Id + "", sc.con);

                                //DataTable dt1 = new DataTable();
                                //adp1.Fill(dt1);
                                SqlDataAdapter adp1 = new SqlDataAdapter("Deletenotifications", sc.con);
                                adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
                                adp1.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                                DataTable dt1 = new DataTable();
                                adp1.Fill(dt1);
                            }
                                                 catch { }
                                                 MessageBox.Show("Record deleted successfully ", "Delete Tail", MessageBoxButton.OK, MessageBoxImage.Information);

                                                 //.....Refresh DataGrid........


                                                 var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.Where(x => x.DeleteStatus == false).ToList());
                                                 MooringWinchTailViewModel cc = new MooringWinchTailViewModel(lostdata);



                                                 //sc.ObservableCollectionList(LoadMooringWinchList, GetMooringWinchList);

                                                 //.....End Refresh DataGrid........
                                                 //}
                                          }
                                          else
                                          {
                                                 MessageBox.Show("Record is not found ", "Delete Department ", MessageBoxButton.OK, MessageBoxImage.Information);
                                          }

                                   }
                            }

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void Below21TailsAtDeleteTime()
              {
                     try
                     {
                            // minimum required of 21 Tails ---------------------------------
                            SqlDataAdapter adp1 = new SqlDataAdapter("select COUNT(*) from MooringRopeDetail where OutofServiceDate is null and RopeTail=1 and DeleteStatus=0", sc.con);
                            DataTable dt1 = new DataTable();
                            adp1.Fill(dt1);
                            if (dt1.Rows.Count >= 0)
                            {
                                   int cnt = Convert.ToInt32(dt1.Rows[0][0]);
                                   if (cnt < StaticHelper.MinimumRopeTail)
                                   {
                                          var notification = "Active rope tail below minimum required of " + StaticHelper.MinimumRopeTail + " Rope tail including spare";

                                          SqlDataAdapter act = new SqlDataAdapter("select COUNT(*) from Notifications where Notification='Active rope tail below minimum required of " + StaticHelper.MinimumRopeTail + " Rope tail including spare'", sc.con);
                                          DataTable dd = new DataTable();
                                          act.Fill(dd);

                                          int cntnoti = Convert.ToInt32(dd.Rows[0][0]);
                                          if (cntnoti == 0)
                                          {
                                                 NotificationsClass noti = new NotificationsClass();
                                                 noti.Acknowledge = false;
                                                 noti.AckRecord = "Not yet acknowledged";
                                                 //noti.AckRecord = "Please insert minimum of {21} active rope tails";
                                                 noti.Notification = notification;
                                                 noti.RopeId = 0;
                                                 noti.IsActive = true;
                                                 noti.NotificationType = 1;
                                                 //noti.NotificationDueDate = notidueMonth;
                                                 noti.CreatedDate = DateTime.Now;
                                                 noti.CreatedBy = "Admin";
                                                 noti.NotificationAlertType = (int)NotificationAlertType.Minimum21TailCount;
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
                                          SqlDataAdapter act = new SqlDataAdapter("delete from Notifications where NotificationAlertType = 18", sc.con);
                                          DataTable dd = new DataTable();
                                          act.Fill(dd);
                                          act.Dispose();
                                          dd.Dispose();
                                   }
                            }
                     }
                     catch { }
              }
              private void ExportMooringWinchTailMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportMooringWinchTailMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void ExportMooringWinchTailMethod1()
              {
                     try
                     {
                            DataSet ds = null;
                            ds = new DataSet("General");
                            ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

                           // string qry = "GetMooringRopeDetailListExcel";

                string qry = @"Select * into #tbtemp from ( select a.Id, m.ropetype as [Line Type],a.ropeconstruction as [Line Construction],a.diameter as [Diameter],a.Length as [Length(mtrs)],a.MBL as [MBL(T)],a.LDBF as [TDBF(T)],a.WLL as [WLL(T)],k.Name as ManufacturerName,a.CertificateNumber as [Certificate Number],a.UniqueID as [Unique Identification No],
a.ReceivedDate as [Received Date],a.InstalledDate,a.RopeTagging as [Line Tagging],a.StartCounterHours as [Existing running hours (Start Counter)],a.Remarks
from MooringRopeDetail a left join tblCommon k on a.ManufacturerId=k.Id inner join MooringRopeType m on a.ropetypeid=m.id
where OutofServiceDate is null and a.DeleteStatus=0 and a.ropetail=1 ) as dd
select * from #tbtemp order by InstalledDate desc
select * into #tbtemp2 from( select a.Id,m.ropetype as [Line Type],a.ropeconstruction as [Line Construction],a.diameter as [Diameter],
a.Length as [Length(mtrs)],a.MBL as [MBL(T)],a.LDBF as [TDBF(T)],a.WLL as [WLL(T)],k.Name as ManufacturerName,a.CertificateNumber as [Certificate Number],a.UniqueID as [Unique Identification No],a.ReceivedDate ,CASE
WHEN a.InstalledDate IS null THEN 'Not Assigned'
WHEN a.InstalledDate IS Not null THEN convert(varchar, a.InstalledDate , 106)
end as [Installed Date],a.RopeTagging as [Line Tagging], a.Remarks,a.StartCounterHours as [Existing running hours (Start Counter)],a.InstalledDate
from MooringRopeDetail a inner join tblCommon k on a.ManufacturerId=k.Id inner join MooringRopeType m on a.ropetypeid=m.id

where a.deletestatus=0 and a.OutofServiceDate is not null and a.DeleteStatus=0 and a.ropetail=1)as hh

select * from #tbtemp2 order by InstalledDate desc

drop table #tbtemp
drop table #tbtemp2";

                //string qry = "Select RopeType as [Line Type], RopeConstruction as [Line Construction],Diameter as [Diameter(mm], Length as [Length(mtrs)],MBL as [MBL(T)],LDBF as [LDBF(T)],WLL as [WLL(T)],Name as [Manufacturer Name], CertificateNumber as [Certificate Number],UniqueID as [Unique Identification No],ReceivedDate as [Received Date],RopeTagging as [Line Tagging],StartCounterHours as [Existing running hours (Start Counter)],Remarks as [Remarks] from MooringRopeDetail INNER JOIN MooringRopeType ON MooringRopeDetail.RopeTypeId=MooringRopeType.Id INNER JOIN tblCommon On MooringRopeDetail.ManufacturerId=tblCommon.Id";
                SqlCommand cmd = new SqlCommand(qry, sc.con);
                            //SqlCommand cmd = new SqlCommand("GetMooringRopeDetailList", sc.con);
                            //cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@RopeTail", 1);
                            //DataTable dt = new DataTable();
                            DataSet dt = new DataSet();
                            SqlDataAdapter adp = new SqlDataAdapter();
                            adp.SelectCommand = cmd;
                            adp.Fill(dt);

                            dt.Tables[0].TableName = "RopeTail In Use(incl.spare)";

                            dt.Tables[1].TableName = "DiscardedRopeTail";

                            ds = dt;

                            cmd.Dispose();
                            adp.Dispose();
                            dt.Dispose();


                            //SqlCommand cmd1 = new SqlCommand("GetMooringRopeDetailList", sc.con);
                            //cmd1.CommandType = CommandType.StoredProcedure;
                            //cmd1.Parameters.AddWithValue("@RopeTail", 1);
                            ////DataTable dt1 = new DataTable();
                            //DataSet dt1 = new DataSet();
                            //SqlDataAdapter adp1 = new SqlDataAdapter();
                            //adp1.SelectCommand = cmd1;
                            //adp1.Fill(dt1);

                            //dt1.Tables[1].TableName = "DiscardedTailDetail";



                            //ds = dt1;

                            //cmd1.Dispose();
                            //adp1.Dispose();
                            //dt1.Dispose();


                            //SqlDataAdapter adp = new SqlDataAdapter("Select RopeType as [RopeTailType], RopeConstruction as [Ropetail Construction],Diameter as [Diameter(mm)], Length as [Length(mtrs)],MBL as [MBL(T)],LDBF as [TDBF(T)],WLL as [WLL(T)],Name as [Manufacture Name], CertificateNumber as [Certificate Number],UniqueID as [Unique Identification No],ReceivedDate as [Received Date],RopeTagging as [RopeTailTagging],StartCounterHours as [Existing running hours (Start Counter)],Remarks as [Remarks] from MooringRopeDetail INNER JOIN MooringRopeType ON MooringRopeDetail.RopeTypeId=MooringRopeType.Id INNER JOIN tblCommon On MooringRopeDetail.ManufacturerId=tblCommon.Id", sc.con);
                            // DataTable dt = new DataTable();
                            //adp.Fill(dt);

                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "MooringRopeTailDetail_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
                            sfd.DefaultExt = "xlsx";
                            sfd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                            if (sfd.ShowDialog() == true)
                            {
                                   if (File.Exists(sfd.FileName))
                                   {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch
                        {
                            MessageBox.Show("It seems that the earlier excel file is open, please close the excel file and download again to replace !", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }

                                   using (XLWorkbook wb = new XLWorkbook())
                                   {

                                          foreach (DataTable item in ds.Tables)
                                          {
                                                 var mytbl = item.TableName;
                                                 var protectedsheet = wb.Worksheets.Add(item);
                                                 protectedsheet.Name = item.TableName;
                                                 var projection = protectedsheet.Protect("49WEB$TREET#");
                                                 projection.InsertColumns = true;
                                                 projection.InsertRows = true;

                                                 wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                                 wb.Style.Font.Bold = true;
                                          }
                                          wb.SaveAs(sfd.FileName);
                                          MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);

                                   }
                            }

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
