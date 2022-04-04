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
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WorkShipVersionII.Commands;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;
using WorkShipVersionII.WorkHoursView;
namespace WorkShipVersionII.WorkHoursViewModel
{
       public class MooringOPRListingViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private static int itemPerPage = 10;
              private int itemcount;
              DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
              public ICommand HelpCommand { get; private set; }
              public MooringOPRListingViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     editCommand = new RelayCommand<MOperationBirthDetail>(EditMooringBirthD);
                     deleteCommand = new RelayCommand<MOperationBirthDetail>(DeleteMooringBirhD);

                     viewCommand = new RelayCommand<MOperationBirthDetail>(ViewDamageRope);

                     viewCommand1 = new RelayCommand<MOperationBirthDetail>(ViewMooringOP);

                     SearchCommand = new RelayCommand(Searchmethod);

                     // StaticHelper.HelpFor = @"LMPR\rope\4.1.1  Mooring Operation Records.htm";
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

                     //LoadMooringOpBirthList = null;
                     //RaisePropertyChanged("LoadMooringOpBirthList");

                     GetMooringOperationBirthD();

                     ViewList = new CollectionViewSource();
                     ViewList.Source = loadMooringOpBList;
                     ViewList.Filter += new FilterEventHandler(View_Filter);

                     CurrentPageIndex = 0;
                     itemcount = loadMooringOpBList.Count;
                     CalculateTotalPages();

            setupchanges();
            GetPortName();
                     AutoPortName = string.Empty;
                     RaisePropertyChanged("AutoPortName");

                     SDateFrom = null; RaisePropertyChanged("SDateFrom");
                     SDateTo = null; RaisePropertyChanged("SDateTo");

                     //FacilityName = null;
                     //RaisePropertyChanged("FacilityName");

                     NextCommand = new NextPageCommandMOperation(this);
                     PreviousCommand = new PreviousPageCommandMOperation(this);
                     FirstCommand = new FirstPageCommandMOperation(this);
                     LastCommand = new LastPageCommandMOperation(this);

                     //ViewList.View.Refresh();

                     RaisePropertyChanged("MOperationBirth");
                     OnPropertyChanged(new PropertyChangedEventArgs("MOperationBirth"));

                     // ViewList.View.Refresh();
              }

              public MooringOPRListingViewModel(ObservableCollection<MOperationBirthDetail> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     //LoadMooringOpBirthList.Clear();

                     GetMooringOperationBirthD();

            setupchanges();

                     //ViewList = new CollectionViewSource();
                     //ViewList.Source = loadMooringOpBList;
                     //ViewList.Filter += new FilterEventHandler(View_Filter);

            //CurrentPageIndex = 0;
            //itemcount = loadMooringOpBList.Count;
            //CalculateTotalPages();

            //StaticHelper.HelpFor = @"General\2.0 Notification\2.1 NOTIFICATIONS.htm";
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

                     NextCommand = new NextPageCommandMOperation(this);
                     PreviousCommand = new PreviousPageCommandMOperation(this);
                     FirstCommand = new FirstPageCommandMOperation(this);
                     LastCommand = new LastPageCommandMOperation(this);

                     //ViewList.View.Refresh();

                     AutoPortName = string.Empty;
                     RaisePropertyChanged("AutoPortName");
                     //FacilityName = null;
                     //RaisePropertyChanged("FacilityName");

                     RaisePropertyChanged("MOperationBirth");
                     OnPropertyChanged(new PropertyChangedEventArgs("MOperationBirth"));

              }

        private void setupchanges()
        {
            try
            {
              

                using (SqlDataAdapter adp = new SqlDataAdapter("update mooringropedetail set CurrentRunningHours =0 where CurrentRunningHours is null", sc.con))
                {
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                };


//                SqlDataAdapter adp1 = new SqlDataAdapter("select SubVersions from version_tbl", sc.con);
                
//                    DataTable dt1 = new DataTable();
//                    adp1.Fill(dt1);
//                if (dt1.Rows.Count > 0)
//                {
//                    string subver = dt1.Rows[0][0].ToString();
//                    if (subver == "0")
//                    {

//                        using (SqlDataAdapter adp = new SqlDataAdapter(@"

//declare @counting int, @totalcount  decimal(18,2)
//set @counting= ( select count(*) from mooringropedetail where DeleteStatus=0 and OutofServiceDate is null)

//DECLARE @LoopCounter INT = (select top(1) Id from MooringRopeDetail where DeleteStatus=0 and OutofServiceDate is null), 
//@MaxEmployeeId INT = 500 , @RopeId INT,
//     @startcounterhrs  decimal(18,2), @curntrnighrs  decimal(18,2), @totalrnghrs  decimal(18,2)
 
//WHILE(@LoopCounter <= @counting)
//BEGIN

//  (SELECT @RopeId=Id, @startcounterhrs = StartCounterHours,@curntrnighrs= CurrentRunningHours
//     FROM mooringropedetail WHERE Id = @LoopCounter)

//  set @startcounterhrs= (select StartCounterHours from MooringRopeDetail where id=@RopeId)
//  set @totalrnghrs= (select sum(runninghours) from MOUsedWinchTbl where ropeid=@RopeId group by ropeid )

//set @totalcount=@startcounterhrs+@totalrnghrs;

//update MooringRopeDetail set CurrentRunningHours=@totalcount where Id=@RopeId


//   SET @LoopCounter  = @LoopCounter  + 1        
//END", sc.con))
//                        {
//                            DataTable dt = new DataTable();
//                            adp.Fill(dt);
//                        };
//                    }
//                }

//                using (SqlDataAdapter adp = new SqlDataAdapter("update version_tbl set versions='1.1.1',ClientVersions= '1.1.1',SubVersions=1", sc.con))
//                {
//                    DataTable dt = new DataTable();
//                    adp.Fill(dt);
//                };

            }
            catch
            {

            }
        }
              private void Searchmethod()
              {
                     try
                     {
                            string portname = MOperationBirth.PortName;
                            string facilityname = MOperationBirth.FacilityName;
                            //if (_BtnName == "Go Back")
                            //{
                            //    GetNotificationListArchive((DateTime)SDateFrom, (DateTime)SDateTo);


                            //}

                            if (portname != null && SDateFrom == null)
                            {
                                   //if (facilityname != null)
                                   //{
                                   GetMooringOperationBirthD(portname, facilityname);
                                   //}
                                   //else
                                   //{
                                   //    MessageBox.Show("Please Choose Facility Name !", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Information);
                                   //}
                            }
                            if (SDateFrom != null && portname == "")
                            {
                                   if (SDateTo != null)
                                   {
                                          GetMooringOperationBirthD((DateTime)SDateFrom, (DateTime)SDateTo);
                                   }
                                   else
                                   {
                                          MessageBox.Show("Please Choose DateTo !", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Information);
                                   }
                            }

                            if (portname != "" && SDateFrom != null && facilityname != "" && SDateTo != null)
                            {
                                   GetMooringOperationBirthD(portname, facilityname, (DateTime)SDateFrom, (DateTime)SDateTo);
                            }
                            if (portname == "" && SDateFrom == null && SDateTo == null)
                            {
                                   GetMooringOperationBirthD();
                            }



                            NextCommand = new NextPageCommandMOperation(this);
                            PreviousCommand = new PreviousPageCommandMOperation(this);
                            FirstCommand = new FirstPageCommandMOperation(this);
                            LastCommand = new LastPageCommandMOperation(this);

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }


              private string listVisible;
              public string ListVisible
              {
                     get { return listVisible; }
                     set
                     {
                            listVisible = value;
                            RaisePropertyChanged("ListVisible");
                     }
              }



              public ICommand SearchCommand { get; private set; }

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
              private ICommand viewCommand;
              public ICommand ViewCommand
              {
                     get { return viewCommand; }
                     set { viewCommand = value; }
              }

              private ICommand viewCommand1;
              public ICommand ViewCommand1
              {
                     get { return viewCommand1; }
                     set { viewCommand1 = value; }
              }

              private void ViewDamageRope(MOperationBirthDetail mw)
              {
                     try
                     {
                            MooringOPDamagedRopeViewModel vm = new MooringOPDamagedRopeViewModel(mw.OPId);
                            ChildWindowManager.Instance.ShowChildWindow(new MooringOPDamagedRopeView() { DataContext = vm });
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void ViewMooringOP(MOperationBirthDetail mw)
              {
                     try
                     {
                            StaticHelper.ViewId = mw.OPId;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewMooringOperation());
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }
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
                            TotalPages = (itemcount / itemPerPage);
                            if (itemcount % itemPerPage != 0)
                            {
                                   TotalPages += 1;
                            }

                            //if (itemcount % itemPerPage == 0)
                            //{
                            //    TotalPages = (itemcount / itemPerPage);
                            //}
                            //else
                            //{
                            //    TotalPages = (itemcount / itemPerPage) + 1;
                            //}
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
                            int index = ((MOperationBirthDetail)e.Item).RowId - 1;
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


              private void View_Filter1(object sender, FilterEventArgs e)
              {
                     try
                     {
                            //int index = ((MOperationBirthDetail)e.Item).OPId + 1;
                            //if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
                            int index = ((MOperationBirthDetail)e.Item).RowId - 1;
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

              string[] MoorOpTables = { "RopeSplicingRecord", "RopeDamageRecord", "RopeCropping", "RopeEndtoEnd2", "MooringRopeDetail" };


              private void DeleteMooringBirhD(MOperationBirthDetail mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "Mooring Operation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {

                                   var findrank = sc.MOperationBirthDetailTbl.Where(x => x.OPId == mw.OPId && x.IsActive == true).FirstOrDefault();
                                   if (findrank != null)
                                   {

                        updaterunnighrs(mw.OPId);
                                          int counter = 0; string Msg = "";
                                          foreach (var item in MoorOpTables)
                                          {
                                                 if (item != "MooringRopeDetail")
                                                 {
                                                        string sqlqry = "select * from " + item + " where MOpid = " + mw.OPId + " and IsActive=1 ";
                                                        using (SqlDataAdapter sda = new SqlDataAdapter(sqlqry, sc.con))
                                                        {
                                                               DataTable dtp = new DataTable();
                                                               sda.Fill(dtp);
                                                               if (dtp.Rows.Count > 0)
                                                               {
                                                                      counter++;
                                                                      string Mvalue = "";
                                                                      if(item== "RopeSplicingRecord")
                                                                      {
                                                                             Mvalue = "Splicing";
                                                                      }
                                                                      if (item == "RopeDamageRecord")
                                                                      {
                                                                             Mvalue = "Damaged";
                                                                      }
                                                                      if (item == "RopeCropping")
                                                                      {
                                                                             Mvalue = "Cropping";
                                                                      }
                                                                      if (item == "RopeEndtoEnd2")
                                                                      {
                                                                             Mvalue = "End to End";
                                                                      }
                                                                      Msg = Msg + Mvalue + ",";
                                                               }

                                                        }
                                                 }
                                                 else
                                                 {
                                                        string sqlqry = "select * from MooringRopeDetail where  MooringOperationID = " + mw.OPId + " and IsActive=1";
                                                        using (SqlDataAdapter sda = new SqlDataAdapter(sqlqry, sc.con))
                                                        {
                                                               DataTable dtp = new DataTable();
                                                               sda.Fill(dtp);
                                                               if (dtp.Rows.Count > 0)
                                                               {
                                                                      counter++;
                                                                      string Mvalue = "";
                                                                      if (item == "MooringRopeDetail")
                                                                      {
                                                                             Mvalue = "Mooring Line Detail";
                                                                      }
                                                                      Msg = Msg + Mvalue + ",";
                                                               }

                                                        }

                                                 }
                                          }

                                          if (counter == 0)
                                          {

                                                 SqlDataAdapter adp2 = new SqlDataAdapter("delete from Notifications where MOP_Id=" + mw.OPId + "", sc.con);
                                                 DataTable dt2 = new DataTable();
                                                 adp2.Fill(dt2);


                                                 var result11 = sc.MOperationBirthDetailTbl.SingleOrDefault(b => b.OPId == mw.OPId && b.IsActive == true);
                                                 if (result11 != null)
                                                 {
                                                        result11.IsActive = false;
                                                        sc.SaveChanges();

                                                        SqlDataAdapter adp = new SqlDataAdapter("delete from  MOUsedWinchTbl where OperationID=" + mw.OPId + "", sc.con);
                                                        DataTable dt = new DataTable();
                                                        adp.Fill(dt);
                                                 }

                                                 var result1 = sc.RopeSplicing.SingleOrDefault(b => b.MOpid == mw.OPId && b.RopeTail == 0 && b.IsActive == true);
                                                 if (result1 != null)
                                                 {

                                                        result1.IsActive = false;
                                                        result1.ModifiedBy = "Admin";

                                                        result1.ModifiedDate = DateTime.Now;
                                                        sc.SaveChanges();
                                                 }

                                                 var result2 = sc.RopeCropping.SingleOrDefault(b => b.MOpid == mw.OPId && b.RopeTail == 0 && b.IsActive == true);
                                                 if (result2 != null)
                                                 {

                                                        result2.IsActive = false;
                                                        result2.ModifiedBy = "Admin";

                                                        result2.ModifiedDate = DateTime.Now;
                                                        sc.SaveChanges();
                                                 }

                                                 var result21 = sc.RopeDamage.SingleOrDefault(b => b.MOPId == mw.OPId && b.RopeTail == 0 && b.IsActive == true);
                                                 if (result21 != null)
                                                 {

                                                        result21.IsActive = false;
                                                        result21.ModifiedBy = "Admin";

                                                        result21.ModifiedDate = DateTime.Now;
                                                        sc.SaveChanges();
                                                 }


                                                 var lostdata = new ObservableCollection<MOperationBirthDetail>(sc.MOperationBirthDetailTbl.ToList());
                                                 MooringOPRListingViewModel cc = new MooringOPRListingViewModel(lostdata);

                                                 GetMooringOperationBirthD();

                                                 NextCommand = new NextPageCommandMOperation(this);
                                                 PreviousCommand = new PreviousPageCommandMOperation(this);
                                                 FirstCommand = new FirstPageCommandMOperation(this);
                                                 LastCommand = new LastPageCommandMOperation(this);

                                                 MessageBox.Show("Record deleted successfully ", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Information);

                                          }
                                          else
                                          {
                                                 MessageBox.Show("There was a rope / rope tail  are reported in "+Msg.TrimEnd(',')+ " of this operation, you cannot delete this operation before deleting the " + Msg.TrimEnd(',') + " records", "Mooring Operation ", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          }
                                   }
                                   else
                                   {
                                          MessageBox.Show("Record is not found ", "Mooring Operation ", MessageBoxButton.OK, MessageBoxImage.Information);
                                   }

                            }

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

        private void updaterunnighrs(int operationid)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("select * from  MOUsedWinchTbl where OperationID=" + operationid + "", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                decimal runnghrs = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int ropeid = Convert.ToInt32(dt.Rows[i]["RopeId"]);
                    decimal rnghrs = Convert.ToDecimal(dt.Rows[i]["RunningHours"]);


                    SqlDataAdapter adp1 = new SqlDataAdapter("select CurrentRunningHours from  mooringropedetail where id =" + ropeid + " ", sc.con);
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    if(dt1.Rows.Count> 0)
                    {
                        runnghrs= Convert.ToDecimal(dt1.Rows[0][0]) - rnghrs;



                        //SqlDataAdapter adp11 = new SqlDataAdapter("update mooringropedetail set CurrentRunningHours=" + runnghrs + " where ID =" + ropeid + " ", sc.con);
                        SqlDataAdapter adp11 = new SqlDataAdapter("UpdateRunningHrs", sc.con);
                        adp11.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp11.SelectCommand.Parameters.AddWithValue("@ropeid", ropeid);
                        adp11.SelectCommand.Parameters.AddWithValue("@rnghrs", runnghrs);
                        DataTable dt11 = new DataTable();
                        adp11.Fill(dt11);

                    }


                }

            }
            catch { }
        }

              //public static CollectionViewSource ViewList { get; set; }
              public static CollectionViewSource ViewList1 { get; set; }
              public static ObservableCollection<MOperationBirthDetail> loadMooringOpBList = new ObservableCollection<MOperationBirthDetail>();

              public ObservableCollection<MOperationBirthDetail> LoadMooringOpBirthList
              {
                     get
                     {
                            return loadMooringOpBList;
                     }
                     set
                     {
                            loadMooringOpBList = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringOpBirthList"));

                     }
              }
              public static MOperationBirthDetail mOperationBirths = new MOperationBirthDetail();

              public MOperationBirthDetail MOperationBirth
              {
                     get { return mOperationBirths; }
                     set
                     {
                            mOperationBirths = value;
                            RaisePropertyChanged("MOperationBirth");

                     }
              }
              public void GetPortName()
              {
                     // ObservableCollection<RopeTypeCombo1> AddRopeType = new ObservableCollection<RopeTypeCombo1>();
                     portname.Clear();
                     PortNameCombo rop;
                     SqlDataAdapter adp = new SqlDataAdapter("select distinct portname from PortList", sc.con);
                     DataTable ds = new DataTable();
                     adp.Fill(ds);

                     foreach (DataRow row in ds.Rows)
                     {
                            rop = new PortNameCombo();
                            //rop.Id = (int)row["Id"];
                            rop.PortName = (string)row["portname"];
                            portname.Add(rop);
                     }

                     OnPropertyChanged(new PropertyChangedEventArgs("portname"));
                     //return AddRopeType;


              }

              private static ObservableCollection<PortNameCombo> portname = new ObservableCollection<PortNameCombo>();
              public ObservableCollection<PortNameCombo> PortName
              {
                     get
                     {
                            return portname;
                     }
                     set
                     {
                            // var data = sc.AssignRopetoWinch.Where(x => x.Id == ropetype).FirstOrDefault();
                            portname = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("PortName"));
                     }
              }

              public void GetFacilityName(string portname)
              {
                     try
                     {
                            facilityname.Clear();
                            PortNameCombo rop;
                            SqlDataAdapter adp = new SqlDataAdapter("select facilityname from PortList where PortName='" + portname + "'", sc.con);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);

                            foreach (DataRow row in ds.Rows)
                            {
                                   rop = new PortNameCombo();
                                   //rop.Id = (int)row["Id"];
                                   rop.FacilityName = (string)row["facilityname"];
                                   facilityname.Add(rop);
                            }

                            OnPropertyChanged(new PropertyChangedEventArgs("facilityname"));
                            //return AddRopeType;

                     }
                     catch { }
              }
              private static ObservableCollection<PortNameCombo> facilityname = new ObservableCollection<PortNameCombo>();
              public ObservableCollection<PortNameCombo> FacilityName
              {
                     get
                     {
                            return facilityname;
                     }
                     set
                     {
                            // var data = sc.AssignRopetoWinch.Where(x => x.Id == ropetype).FirstOrDefault();
                            facilityname = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("FacilityName"));
                     }
              }
              public class PortNameCombo
              {
                     public int Id { get; set; }
                     public string PortName { get; set; }
                     public string FacilityName { get; set; }
              }



              public static Nullable<DateTime> _SDateFrom = null;
              public Nullable<DateTime> SDateFrom
              {
                     get
                     {
                            if (_SDateFrom == null)
                                   _SDateFrom = null;

                            return _SDateFrom;
                     }
                     set
                     {
                            _SDateFrom = value;
                            RaisePropertyChanged("SDateFrom");
                     }
              }

              public static Nullable<DateTime> _SDateTo = null;
              public Nullable<DateTime> SDateTo
              {
                     get
                     {
                            if (_SDateTo == null)
                                   _SDateTo = null;


                            return _SDateTo;
                     }
                     set
                     {
                            _SDateTo = value;
                            RaisePropertyChanged("SDateTo");
                     }
              }


              public static Nullable<DateTime> _DateFrom = null;
              public Nullable<DateTime> DateFrom
              {
                     get
                     {
                            if (_DateFrom == null)
                            {

                                   _DateFrom = null;
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

              public static Nullable<DateTime> _DateTo = null;
              public Nullable<DateTime> DatesTo
              {
                     get
                     {
                            if (_DateTo == null)
                            {

                                   _DateTo = null;
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


              public static PortNameCombo sportname;// = new Ropetypecombo();
              public PortNameCombo SPortName
              {
                     get
                     {

                            if (sportname != null)
                            {

                                   MOperationBirth.PortName = sportname.PortName;
                                   OnPropertyChanged(new PropertyChangedEventArgs("MOperationBirth"));
                            }
                            return sportname;

                     }
                     set
                     {

                            sportname = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("SPortName"));


                     }
              }


              public static PortNameCombo sfacilityname;// = new Ropetypecombo();
              public PortNameCombo SFacilityName
              {
                     get
                     {

                            if (sfacilityname != null)
                            {

                                   MOperationBirth.FacilityName = sfacilityname.FacilityName;
                                   OnPropertyChanged(new PropertyChangedEventArgs("MOperationBirth"));
                            }
                            return sfacilityname;

                     }
                     set
                     {

                            sfacilityname = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("SFacilityName"));


                     }
              }
              private static string autoPartname;
              public string AutoPortName
              {
                     get
                     {
                            if (StaticHelper.Autoportname == null)
                            {
                                   autoPartname = "";
                            }

                            if (autoPartname != null)
                            {
                                   RaisePropertyChanged("FacilityName");
                                   StaticHelper.Autoportname = "1";
                                   MOperationBirth.PortName = autoPartname;
                                   AutoPortNames = GetPortName(autoPartname);
                                   GetFacilityName(autoPartname);
                            }
                            return autoPartname;
                     }

                     set
                     {
                            autoPartname = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("AutoPortName"));
                     }
              }


              private static string otherPortname;
              public string OtherPortName
              {
                     get
                     {
                            //if (StaticHelper.Autoportname == null)
                            //{
                            //    otherPortname = "";
                            //}

                            if (otherPortname != null)
                            {
                                   //RaisePropertyChanged("FacilityName");

                                   MOperationBirth.PortName = otherPortname;
                                   //AutoPortNames = GetPortName(autoPartname);
                                   //GetFacilityName(autoPartname);
                            }
                            return otherPortname;
                     }

                     set
                     {
                            otherPortname = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("OtherPortName"));
                     }
              }

              private static ObservableCollection<string> autoPortNames = new ObservableCollection<string>();
              public ObservableCollection<string> AutoPortNames
              {
                     get
                     {
                            return autoPortNames;
                     }
                     set
                     {
                            autoPortNames = value;
                            RaisePropertyChanged("AutoPortNames");

                     }
              }

              private ObservableCollection<string> GetPortName(string autoPortName)
              {
                     var PortNames = new ObservableCollection<string>();
                     var data = sc.PortListtbl.Where(x => x.PortName.ToLower().Contains(autoPortName.Trim().ToLower())).Select(x => new { x.PortName }).ToList().Distinct();

                     foreach (var item in data)
                     {
                            PortNames.Add(item.PortName);

                     }
                     var dat = sc.PortListtbl.Where(x => x.PortName.ToLower().Equals(autoPortName.Trim().ToLower())).Select(x => new { x.Id, x.PortName }).Distinct().FirstOrDefault();
                     if (dat != null)
                     {
                            MOperationBirth.PortName = dat.PortName;
                            MOperationBirth.OPId = dat.Id;
                     }
                     else
                     {
                            MOperationBirth.PortName = string.Empty;
                            MOperationBirth.OPId = 0;

                     }

                     if (PortNames.Count > 0)
                     {

                            ListVisible = "Visible";
                     }
                     else
                     {
                            ListVisible = "Collapsed";
                            //MessageBox.Show("PortName not exist of " + autoPortName + " !", "Wrong Input", MessageBoxButton.OK, MessageBoxImage.Warning);

                     }

                     RaisePropertyChanged("MOperationBirth");
                     return PortNames;
              }

              //private string listVisible;
              //public string ListVisible
              //{
              //    get { return listVisible; }
              //    set
              //    {
              //        listVisible = value;
              //        RaisePropertyChanged("ListVisible");
              //    }
              //}

              public void GetMooringOperationBirthD(DateTime datefrom, DateTime dateto)
              {

                     try
                     {


                            int rowid = 1;

                            loadMooringOpBList.Clear();

                            var data = sc.MOperationBirthDetailTbl.Where(x => x.IsActive == true && x.FastDatetime >= datefrom && x.FastDatetime <= dateto).ToList().OrderByDescending(x => x.OPId);

                            foreach (var item in data)
                            {

                                   loadMooringOpBList.Add(new MOperationBirthDetail()
                                   {
                                          RowId = rowid++,
                                          OPId = item.OPId,
                                          PortName = item.PortName,
                                          FastDatetime1 = item.FastDatetime.ToString("yyyy-MM-dd"),
                                          CastDatetime1 = item.CastDatetime.ToString("yyyy-MM-dd"),
                                          BirthName = item.BirthName,
                                          BirthType = item.BirthType,
                                          MooringType = item.MooringType,
                                          DraftArrivalFWD = item.DraftArrivalFWD,
                                          DraftArrivalAFT = item.DraftArrivalAFT,
                                          DepthAtBerth = item.DepthAtBerth,
                                          DraftDepartureAFT = item.DraftDepartureAFT,
                                          DraftDepartureFWD = item.DraftDepartureFWD,
                                          BerthSide = item.BerthSide,
                                          Any_Affect_Passing_Traffic = item.Any_Affect_Passing_Traffic,
                                          Berth_exposed_SeaSwell = item.Berth_exposed_SeaSwell,
                                          Any_Rope_Damaged = item.Any_Rope_Damaged,
                                          AnySquall = item.AnySquall,
                                          CurrentSpeed = item.CurrentSpeed,
                                          VesselCondition = item.VesselCondition,
                                          WindDirection = item.WindDirection,
                                          WindSpeed = item.WindSpeed,
                                          RangOfTide = item.RangOfTide,
                                          ShipAccess = item.ShipAccess,
                                          Ship_was_continuously_contact_with_fender = item.Ship_was_continuously_contact_with_fender,
                                          SurgingObserved = item.SurgingObserved,
                                          CheckMooringDamage = CheckDamageRecord(item.OPId),
                                          // (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                                   });


                            }



                            RaisePropertyChanged("LoadMooringOpBirthList");



                            ViewList = new CollectionViewSource
                            {
                                   Source = loadMooringOpBList
                            };


                            ViewList.Filter += new FilterEventHandler(View_Filter);


                            itemcount = loadMooringOpBList.Count(); //sc.Notifications.Count();
                            CalculateTotalPages();
                            ViewList.View.Refresh();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }

              }

              public void GetMooringOperationBirthD(string portname, string facilityname)
              {

                     try
                     {



                            int rowid = 1;

                            loadMooringOpBList.Clear();



                            var data = sc.MOperationBirthDetailTbl.Where(x => x.IsActive == true && x.PortName == portname || x.FacilityName == facilityname).ToList().OrderByDescending(x => x.OPId);

                            foreach (var item in data)
                            {

                                   loadMooringOpBList.Add(new MOperationBirthDetail()
                                   {
                                          RowId = rowid++,
                                          OPId = item.OPId,
                                          PortName = item.PortName,
                                          FacilityName = item.FacilityName,
                                          FastDatetime1 = item.FastDatetime.ToString("yyyy-MM-dd"),
                                          CastDatetime1 = item.CastDatetime.ToString("yyyy-MM-dd"),
                                          BirthName = item.BirthName,
                                          BirthType = item.BirthType,
                                          MooringType = item.MooringType,
                                          DraftArrivalFWD = item.DraftArrivalFWD,
                                          DraftArrivalAFT = item.DraftArrivalAFT,
                                          DepthAtBerth = item.DepthAtBerth,
                                          DraftDepartureAFT = item.DraftDepartureAFT,
                                          DraftDepartureFWD = item.DraftDepartureFWD,
                                          BerthSide = item.BerthSide,
                                          Any_Affect_Passing_Traffic = item.Any_Affect_Passing_Traffic,
                                          Berth_exposed_SeaSwell = item.Berth_exposed_SeaSwell,
                                          Any_Rope_Damaged = item.Any_Rope_Damaged,
                                          AnySquall = item.AnySquall,
                                          CurrentSpeed = item.CurrentSpeed,
                                          VesselCondition = item.VesselCondition,
                                          WindDirection = item.WindDirection,
                                          WindSpeed = item.WindSpeed,
                                          RangOfTide = item.RangOfTide,
                                          ShipAccess = item.ShipAccess,
                                          Ship_was_continuously_contact_with_fender = item.Ship_was_continuously_contact_with_fender,
                                          SurgingObserved = item.SurgingObserved,
                                          CheckMooringDamage = CheckDamageRecord(item.OPId),
                                          // (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                                   });


                            }



                            RaisePropertyChanged("LoadMooringOpBirthList");



                            ViewList1 = new CollectionViewSource
                            {
                                   Source = loadMooringOpBList
                            };


                            ViewList1.Filter += new FilterEventHandler(View_Filter1);


                            itemcount = loadMooringOpBList.Count(); //sc.Notifications.Count();
                            CalculateTotalPages();
                            ViewList1.View.Refresh();


                            NextCommand = new NextPageCommandMOperation(this);
                            PreviousCommand = new PreviousPageCommandMOperation(this);
                            FirstCommand = new FirstPageCommandMOperation(this);
                            LastCommand = new LastPageCommandMOperation(this);
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }

              }


              public void GetMooringOperationBirthD(string portname, string facilityname, DateTime datefrom, DateTime dateto)
              {

                     try
                     {



                            int rowid = 1;

                            loadMooringOpBList.Clear();


                            var data = sc.MOperationBirthDetailTbl.Where(x => x.IsActive == true && x.PortName == portname && x.FacilityName == facilityname && x.FastDatetime >= datefrom && x.FastDatetime <= dateto).ToList().OrderByDescending(x => x.OPId);

                            foreach (var item in data)
                            {

                                   loadMooringOpBList.Add(new MOperationBirthDetail()
                                   {
                                          RowId = rowid++,
                                          OPId = item.OPId,
                                          PortName = item.PortName,
                                          FastDatetime1 = item.FastDatetime.ToString("yyyy-MM-dd"),
                                          CastDatetime1 = item.CastDatetime.ToString("yyyy-MM-dd"),
                                          BirthName = item.BirthName,
                                          BirthType = item.BirthType,
                                          MooringType = item.MooringType,
                                          DraftArrivalFWD = item.DraftArrivalFWD,
                                          DraftArrivalAFT = item.DraftArrivalAFT,
                                          DepthAtBerth = item.DepthAtBerth,
                                          DraftDepartureAFT = item.DraftDepartureAFT,
                                          DraftDepartureFWD = item.DraftDepartureFWD,
                                          BerthSide = item.BerthSide,
                                          Any_Affect_Passing_Traffic = item.Any_Affect_Passing_Traffic,
                                          Berth_exposed_SeaSwell = item.Berth_exposed_SeaSwell,
                                          Any_Rope_Damaged = item.Any_Rope_Damaged,
                                          AnySquall = item.AnySquall,
                                          CurrentSpeed = item.CurrentSpeed,
                                          VesselCondition = item.VesselCondition,
                                          WindDirection = item.WindDirection,
                                          WindSpeed = item.WindSpeed,
                                          RangOfTide = item.RangOfTide,
                                          ShipAccess = item.ShipAccess,
                                          Ship_was_continuously_contact_with_fender = item.Ship_was_continuously_contact_with_fender,
                                          SurgingObserved = item.SurgingObserved,
                                          CheckMooringDamage = CheckDamageRecord(item.OPId),
                                          // (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                                   });


                            }



                            RaisePropertyChanged("LoadMooringOpBirthList");



                            ViewList = new CollectionViewSource
                            {
                                   Source = loadMooringOpBList
                            };


                            ViewList.Filter += new FilterEventHandler(View_Filter);


                            itemcount = loadMooringOpBList.Count(); //sc.Notifications.Count();
                            CalculateTotalPages();
                            ViewList.View.Refresh();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }

              }

              private string CheckDamageRecord(int opId)
              {
                     string CheckMooringDamage = "Add damage record";
                     using (SqlDataAdapter adp = new SqlDataAdapter("MooringDamageRecord", sc.con))
                     {
                            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp.SelectCommand.Parameters.AddWithValue("@MOpid", opId);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);
                            if (ds.Rows.Count > 0)
                            {
                                   CheckMooringDamage = "View damage rope";
                            }
                     }

                     return CheckMooringDamage;
              }

              public void GetMooringOperationBirthD()
              {

                     try
                     {

                            int rowid = 1;
                            //ObservableCollection<MOperationBirthDetail> moringlist = new ObservableCollection<MOperationBirthDetail>();
                            loadMooringOpBList.Clear();
                            var data = sc.MOperationBirthDetailTbl.Where(x => x.IsActive == true).ToList().OrderByDescending(x => x.OPId);
                            foreach (var item in data)
                            {

                                   loadMooringOpBList.Add(new MOperationBirthDetail()
                                   {
                                          RowId = rowid++,
                                          OPId = item.OPId,
                                          PortName = item.PortName,
                                          FastDatetime1 = item.FastDatetime.ToString("yyyy-MM-dd"),
                                          CastDatetime1 = item.CastDatetime.ToString("yyyy-MM-dd"),
                                          BirthName = item.BirthName,
                                          BirthType = item.BirthType,
                                          MooringType = item.MooringType,
                                          DraftArrivalFWD = item.DraftArrivalFWD,
                                          DraftArrivalAFT = item.DraftArrivalAFT,
                                          DepthAtBerth = item.DepthAtBerth,
                                          DraftDepartureAFT = item.DraftDepartureAFT,
                                          DraftDepartureFWD = item.DraftDepartureFWD,
                                          BerthSide = item.BerthSide,
                                          Any_Affect_Passing_Traffic = item.Any_Affect_Passing_Traffic,
                                          Berth_exposed_SeaSwell = item.Berth_exposed_SeaSwell,
                                          Any_Rope_Damaged = item.Any_Rope_Damaged,
                                          AnySquall = item.AnySquall,
                                          CurrentSpeed = item.CurrentSpeed,
                                          VesselCondition = item.VesselCondition,
                                          WindDirection = item.WindDirection,
                                          WindSpeed = item.WindSpeed,
                                          RangOfTide = item.RangOfTide,
                                          ShipAccess = item.ShipAccess,
                                          Ship_was_continuously_contact_with_fender = item.Ship_was_continuously_contact_with_fender,
                                          SurgingObserved = item.SurgingObserved,
                                          CheckMooringDamage = CheckDamageRecord(item.OPId),
                                          FacilityName = item.FacilityName,
                                          // (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                                   });


                            }
                            RaisePropertyChanged("LoadMooringOpBirthList");




                            ViewList = new CollectionViewSource
                            {
                                   Source = loadMooringOpBList
                            };


                            //ViewList.Filter += new FilterEventHandler(View_Filter);


                            //itemcount = loadMooringOpBList.Count(); //sc.Notifications.Count();
                            //CalculateTotalPages();
                            //ViewList.View.Refresh();


                            //_ViewList = new CollectionViewSource
                            //{
                            //    Source = loadMooringOpBList
                            //};


                            _ViewList.Filter += new FilterEventHandler(View_Filter);


                            itemcount = loadMooringOpBList.Count(); //sc.Notifications.Count();
                            CalculateTotalPages();
                            _ViewList.View.Refresh();
                            RaisePropertyChanged("ViewList");
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }

              }


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
              private void EditMooringBirthD(MOperationBirthDetail mw)
              {
                     try
                     {
                            MooringOPRViewModel vm = new MooringOPRViewModel(mw);
                            ChildWindowManager.Instance.ShowChildWindow(new MooringOPRView() { DataContext = vm });
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