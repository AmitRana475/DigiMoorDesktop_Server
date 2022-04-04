using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;
using System.Data.Entity;
using WorkShipVersionII.Commands;
using System.Windows.Data;
using ClosedXML.Excel;
using System.IO;
using Microsoft.Win32;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class RopeInspectionListViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private static int itemPerPage = 15;
              private int itemcount;
              private BackgroundWorker _ExportRopeInspection;
              public ICommand HelpCommand { get; private set; }
              public RopeInspectionListViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportRopeInspection = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     deletecommand = new RelayCommand<TotalInspections>(DeleteRopeInspection);
                     searchcmmand = new RelayCommand(GetInspectList);

                     yearname = sc.GetYear();
                     GetInspectDate(DateTime.Now.Year.ToString());
                     syearname = null;

                     LoadInspections.Clear();


                     GetMooringInspection(DateTime.Now.Year.ToString());

                     viewCommand = new RelayCommand<TotalInspections>(Viewropedamage);

                     this._ExportRopeInspectionCommands = new RelayCommand(() => _ExportRopeInspection.RunWorkerAsync(), () => !_ExportRopeInspection.IsBusy);
                     this._ExportRopeInspection.DoWork += new DoWorkEventHandler(ExportRopeInspectionMethod);

                     //StaticHelper.HelpFor = @"LMPR\rope\4.2.1  MOORING ROPE INSPECTION.htm";
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     //LoadInspections = GetMooringInspection();

                     NextCommand = new NextPageCommandMooringRopeInspection(this);
                     PreviousCommand = new PreviousPageCommandMooringRopeInspection(this);
                     FirstCommand = new FirstPageCommandMooringRopeInspection(this);
                     LastCommand = new LastPageCommandMooringRopeInspection(this);
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
                            int index = ((TotalInspections)e.Item).RowId - 1;
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
              private ICommand _ExportRopeInspectionCommands;
              public ICommand ExportRopeInspectionCommands
              {
                     get
                     {

                            return _ExportRopeInspectionCommands;
                     }
              }

              private ICommand viewCommand;
              public ICommand ViewCommand
              {
                     get { return viewCommand; }
                     set { viewCommand = value; }
              }

              public RopeInspectionListViewModel(ObservableCollection<MooringRopeInspectionClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     LoadInspections.Clear();

                     deletecommand = new RelayCommand<TotalInspections>(DeleteRopeInspection);
                     GetMooringInspection(DateTime.Now.Year.ToString());

                     viewCommand = new RelayCommand<TotalInspections>(Viewropedamage);

              }
              private void Viewropedamage(TotalInspections mw)
              {
                     try
                     {

                            //StaticHelper.ViewId = mw.id;
                            //StaticHelper.InspectionDate = mw.InspectDate;
                            //ChildWindowManager.Instance.ShowChildWindow(new ViewMooringRopeInspection());

                            ModelViewMooringRopeInspection vm = new ModelViewMooringRopeInspection(mw);

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }


              private void GetInspectList()
              {
                     LoadInspections.Clear();
                     GetMooringInspection(syearname);
                     OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));
              }


              private void UpdInspDate(int inspectionid, string inspectdate)
              {
                     try
                     {
                            //SqlDataAdapter adp = new SqlDataAdapter("select * from mooringropeinspection where InspectionId=" + inspectionid + "", sc.con);
                            SqlDataAdapter adp = new SqlDataAdapter("select * from mooringropeinspection where InspectDate='" + inspectdate + "' and RopeTail = 0", sc.con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            decimal avg = 0;
                            var Ropelist = sc.MooringWinchRope.Where(x => x.DeleteStatus == false && x.OutofServiceDate == null && x.RopeTail == 0).ToList();
                            var InspecSetting = sc.RopeInspectionSetting.ToList();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                   int ropeid = Convert.ToInt32(dt.Rows[i]["RopeId"]);
                                   int firstvalue = Convert.ToInt32(dt.Rows[i]["ExternalRating_A"]);
                                   int secondvalue = Convert.ToInt32(dt.Rows[i]["InternalRating_A"]);

                                   decimal finalvalue = (firstvalue + secondvalue);
                                   finalvalue = finalvalue / 2;

                                   finalvalue = Math.Round(finalvalue, 0, MidpointRounding.AwayFromZero);

                                   int avrgratingA = Convert.ToInt32(finalvalue);


                                   //int firstvalue1 = Convert.ToInt32(item.ExternalRating_B);
                                   // int secondvalue1 = Convert.ToInt32(item.InternalRating_B);
                                   int firstvalue1 = Convert.ToInt32(dt.Rows[i]["ExternalRating_B"]);
                                   int secondvalue1 = Convert.ToInt32(dt.Rows[i]["InternalRating_B"]);

                                   decimal finalvalue1 = (firstvalue1 + secondvalue1);
                                   finalvalue1 = finalvalue1 / 2;

                                   finalvalue1 = Math.Round(finalvalue1, 0, MidpointRounding.AwayFromZero);

                                   int avrgratingB = Convert.ToInt32(finalvalue1);

                                   //DateTime addmonth =Convert.ToDateTime(ss.InspectDate);

                                   decimal avRA = avrgratingA;
                                   decimal avRB = avrgratingB;

                                   if (avRA >= avRB)
                                   {
                                          avg = avRA;
                                   }
                                   if (avRB >= avRA)
                                   {
                                          avg = avRB;
                                   }

                                   var RopeId = Ropelist.Where(x => x.Id == Convert.ToInt32(dt.Rows[i]["RopeId"]) && x.RopeTail == 0).FirstOrDefault();
                                   //var ratingcheck = InspecSetting.Where(x => x.MooringRopeType == RopeId.Id && x.ManufacturerType == RopeId.ManufacturerId).FirstOrDefault();

                                   var ratingcheck = InspecSetting.Where(x => x.MooringRopeType == RopeId.RopeTypeId && x.ManufacturerType == RopeId.ManufacturerId).FirstOrDefault();


                                   var certinumber = Ropelist.Where(x => x.Id == Convert.ToInt32(dt.Rows[i]["RopeId"]) && x.RopeTail == 0).Select(x => x.CertificateNumber).SingleOrDefault();




                                   if (ratingcheck != null)
                                   {
                                          try
                                          {
                                                 decimal[] array = new decimal[7] { ratingcheck.Rating1, ratingcheck.Rating2, ratingcheck.Rating3, ratingcheck.Rating4, ratingcheck.Rating5, ratingcheck.Rating6, ratingcheck.Rating7 };

                                                 var nearest = array.OrderBy(x => Math.Abs((long)x - avg)).First();

                                                 string rating = "Rating" + avg;

                                                 int near = Convert.ToInt32(nearest);
                                                 SqlDataAdapter pp = new SqlDataAdapter("select " + rating + " from tblRopeInspectionSetting where mooringropetype=" + RopeId.RopeTypeId + " and manufacturertype=" + RopeId.ManufacturerId + "", sc.con);
                                                 System.Data.DataTable rtc = new System.Data.DataTable();
                                                 pp.Fill(rtc);
                                                 if (rtc.Rows.Count > 0)
                                                 {
                                                        decimal perchk = Convert.ToDecimal(rtc.Rows[0][0]) * 30 / 100;
                                                        perchk = perchk * 100;
                                                        //near = Convert.ToInt32(rtc.Rows[0][0]);
                                                        near = Convert.ToInt32(perchk);
                                                 }

                                                 try
                                                 {
                                                        var inspectdate1 = "";
                                                        SqlDataAdapter adp11 = new SqlDataAdapter("select  Max(InspectDate) from MooringRopeInspection where RopeId ='" + Convert.ToInt32(dt.Rows[i]["RopeId"]) + "' and InspectionId !='" + Convert.ToInt32(dt.Rows[i]["InspectionId"]) + "' and IsActive=1 and RopeTail=0", sc.con);
                                                        DataTable dt11 = new DataTable();
                                                        adp11.Fill(dt11);
                                                        if (dt11.Rows.Count > 0)
                                                        {
                                                               //inspectdate1 = Convert.ToDateTime(dt11.Rows[0][0]);
                                                               inspectdate1 = dt11.Rows[0][0] == DBNull.Value ? null : dt11.Rows[0][0].ToString();

                                                               if (inspectdate1 == null)
                                                               {
                                                                      var ssss = Ropelist.Where(x => x.Id == Convert.ToInt32(dt.Rows[i]["RopeId"])).Select(x => x.InstalledDate).SingleOrDefault();

                                                                      inspectdate1 = (ssss) == null ? null : (ssss).ToString();
                                                               }
                                                        }
                                                        else
                                                        {
                                                               var ssss = Ropelist.Where(x => x.Id == Convert.ToInt32(dt.Rows[i]["RopeId"])).Select(x => x.InstalledDate).SingleOrDefault();

                                                               inspectdate1 = (ssss) == null ? null : (ssss).ToString();
                                                               //inspectdate1 = Convert.ToDateTime(ssss);
                                                        }




                                                        DateTime notidueMonth = Convert.ToDateTime(inspectdate1).Date.AddDays(near);
                                                        int rpid = Convert.ToInt32(dt.Rows[i]["RopeId"]);


                                                        DateTime crntdt = DateTime.Now;
                                                        if (notidueMonth <= crntdt)
                                                        {
                                                               notidueMonth = DateTime.Now;
                                                        }
                                                        var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == rpid && b.IsActive == true && b.RopeTail == 0);
                                                        if (result != null)
                                                        {
                                                               result.InspectionDueDate = notidueMonth;
                                                               result.ModifiedBy = "Admin";
                                                               result.ModifiedDate = DateTime.Now;
                                                               sc.SaveChanges();
                                                        }
                                                 }
                                                 catch { }
                                          }
                                          catch (Exception ex) { }
                                   }

                            }
                     }
                     catch { }
              }
              private void DeleteRopeInspection(TotalInspections mw)
              {
                     try
                     {
                            if (System.Windows.MessageBox.Show("Do you want to Delete?", "RopeInspection", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                            {
                                   //SqlDataAdapter adp1 = new SqlDataAdapter("select MAX(inspectionid) from MooringRopeInspection where IsActive=1 and RopeTail = 0", sc.con);
                                   //DataTable dt1 = new DataTable();
                                   //adp1.Fill(dt1);
                                   //int inspectionid = Convert.ToInt32(dt1.Rows[0][0]);


                                   SqlDataAdapter adp1 = new SqlDataAdapter("select InspectDate from MooringRopeInspection where inspectionid='" + mw.id + "' and IsActive=1 and RopeTail=0", sc.con);
                                   DataTable dt1 = new DataTable();
                                   adp1.Fill(dt1);
                                   DateTime inspectdate = Convert.ToDateTime(dt1.Rows[0][0]);

                                   //SqlDataAdapter adp11 = new SqlDataAdapter("select MAX(InspectDate) from MooringRopeInspection where InspectionId ='" + mw.id + "' and IsActive=1 and RopeTail=0", sc.con);
                                   //DataTable dt11 = new DataTable();
                                   //adp11.Fill(dt11);
                                   //DateTime inspectdate1 = Convert.ToDateTime(dt11.Rows[0][0]);

                                   var dd = Convert.ToDateTime(inspectdate).ToString("yyyy-MM-dd");

                                   //SqlDataAdapter adp = new SqlDataAdapter("update MooringRopeInspection set IsActive=0,ModifiedDate=GETDATE(),ModifiedBy='Admin' where InspectionId='" + mw.id + "'", sc.con);
                    SqlDataAdapter adp = new SqlDataAdapter("DeleteMooringRopeInspection", sc.con);
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adp.SelectCommand.Parameters.AddWithValue("@InspectionId", mw.id);
                    DataTable dt = new DataTable();
                                   adp.Fill(dt);



                                   UpdInspDate(mw.id, dd);

                                   //var result1 = sc.MooringRopeInspectionTbl.SingleOrDefault(b => b.InspectionId == mw.id && b.RopeTail == 0 && b.IsActive == true);
                                   //if (result1 != null)
                                   //{

                                   //    result1.IsActive = false;
                                   //    result1.ModifiedBy = "Admin";

                                   //    result1.ModifiedDate = DateTime.Now;
                                   //    sc.SaveChanges();

                                   System.Windows.MessageBox.Show("Record deleted successfully ", "Delete line inspection", MessageBoxButton.OK, MessageBoxImage.Information);

                                   //.....Refresh DataGrid........


                                   var lostdata = new ObservableCollection<MooringRopeInspectionClass>(sc.MooringRopeInspectionTbl.ToList());
                                   RopeInspectionListViewModel cc = new RopeInspectionListViewModel(lostdata);



                                   /// }

                                   //else
                                   //{
                                   //    //MessageBox.Show("Record is not found ", "Delete Department ", MessageBoxButton.OK, MessageBoxImage.Information);
                                   //}

                            }

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private ICommand searchcmmand;

              public ICommand SearchCommand
              {
                     get { return searchcmmand; }
                     set { searchcmmand = value; }
              }
              private ICommand deletecommand;

              public ICommand DeleteCommand
              {
                     get { return deletecommand; }
                     set { deletecommand = value; }
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
              public static ObservableCollection<TotalInspections> loadUserAccess = new ObservableCollection<TotalInspections>();
              //public static ObservableCollection<TotalInspections> loadUserAccess = new ObservableCollection<TotalInspections>();
              public ObservableCollection<TotalInspections> LoadInspections
              {
                     get
                     {
                            return loadUserAccess;
                     }
                     set
                     {
                            loadUserAccess = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));
                     }
              }

              public void GetMooringInspection(string syears)
              {
                     int rowid = 1;
                     LoadInspections.Clear();
                     //string qry = "select distinct id, InspectBy, InspectDate from MooringRopeInspection where DATEPART(YYYY,InspectDate) = '" + syears + "' and ropetail=0";
                     string qry = "select distinct InspectionId, InspectBy, InspectDate from MooringRopeInspection where DATEPART(YYYY,InspectDate) = '" + syears + "' and ropetail=0 and IsActive=1 order by InspectDate desc";
                     SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
                     DataTable datatbl = new DataTable();
                     sda.Fill(datatbl);

                     //var ranklist = new ObservableCollection<MooringRopeInspectionClass>();

                     // var abc = sc.MooringRopeInspectionTbl.Where(x=> x.InspectDate == dd).Select(s=> new { s.InspectBy,s.InspectDate}).Distinct().ToList();
                     //var abc = sc.MooringRopeInspectionTbl.ToList();
                     for (int i = 0; i < datatbl.Rows.Count; i++)
                     {
                            LoadInspections.Add(new TotalInspections()
                            {
                                   RowId = rowid++,
                                   //id= Convert.ToInt32(datatbl.Rows[i]["id"]),
                                   id = Convert.ToInt32(datatbl.Rows[i]["InspectionId"]),
                                   InspectBy = datatbl.Rows[i]["InspectBy"].ToString(),
                                   InspectDate = Convert.ToDateTime(datatbl.Rows[i]["InspectDate"])

                            });

                     }


                     RaisePropertyChanged("LoadInspections");



                     ViewList = new CollectionViewSource
                     {
                            Source = LoadInspections
                     };


                     ViewList.Filter += new FilterEventHandler(View_Filter);


                     itemcount = LoadInspections.Count(); //sc.Notifications.Count();
                     CalculateTotalPages();
                     ViewList.View.Refresh();

                     // OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));

                     //for (int i = 0; i < datatbl.Rows.Count; i++)
                     //{
                     //       ranklist.Add(new MooringRopeInspectionClass()
                     //       {

                     //              InspectBy = "Kuldeep",
                     //              InspectDate = dd,
                     //              AssignNumber = datatbl.Rows[i]["AssignedNumber"].ToString(),
                     //              Location = datatbl.Rows[i]["Location"].ToString(),
                     //              RpoeType = datatbl.Rows[i]["RopeType"].ToString(),
                     //              Certi_No = datatbl.Rows[i]["CertificateNumber"].ToString(),

                     //              ExternalRating_A = 0,
                     //              InternalRating_A = 0,
                     //              AverageRating_A = 0,
                     //              LengthOFAbrasion_A = 0.00m,
                     //              DistanceOutboard_A = 0.00m,
                     //              CutYarnCount_A = 0.00m,
                     //              LengthOFGlazing_A = 0.00m,

                     //              ExternalRating_B = 0,
                     //              InternalRating_B = 0,
                     //              AverageRating_B = 0,
                     //              LengthOFAbrasion_B = 0.00m,
                     //              DistanceOutboard_B = 0.00m,
                     //              CutYarnCount_B = 0.00m,
                     //              LengthOFGlazing_B = 0.00m,

                     //              Chafe_guard_condition = "AAA",

                     //              Twist = 0
                     //       });


                     //}

                     //return ranklist;
              }

              private void GetInspectDate(string mysdate)
              {
                     //var Dtlist = new ObservableCollection<string>();

                     // var data = sc.MooringRopeInspectionTbl.Where(x=> x.InspectDate  )

                     string qry = "select distinct InspectDate from MooringRopeInspection where DATEPART(YYYY,InspectDate) = '" + mysdate + "'";
                     SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
                     DataTable datatbl = new DataTable();
                     sda.Fill(datatbl);

                     for (int i = 0; i < datatbl.Rows.Count; i++)
                     {
                            var date = Convert.ToDateTime(datatbl.Rows[i]["InspectDate"]).ToString("yyyy-MM-dd");
                            DateList.Add(date);
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

              private static ObservableCollection<string> dateList = new ObservableCollection<string>();
              public ObservableCollection<string> DateList
              {
                     get
                     {
                            return dateList;
                     }
                     set
                     {
                            dateList = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("DateList"));
                     }
              }


              private static string syearname;
              public string SYearName
              {
                     get
                     {
                            if (syearname == null)
                            {
                                   syearname = DateTime.Now.Year.ToString();
                            }




                            return syearname;
                     }

                     set
                     {

                            syearname = value;
                            GetInspectDate(syearname);
                            OnPropertyChanged(new PropertyChangedEventArgs("SYearName"));
                     }
              }

              private static string sdates;
              public string SDates
              {
                     get
                     {
                            //if (sdates == null)
                            //       sdates = DateTime.Now.Year.ToString();

                            return sdates;
                     }

                     set
                     {
                            sdates = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("SDates"));
                     }
              }


              private void ExportRopeInspectionMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportRopeInspectionMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
        private void ExportRopeInspectionMethod1()
        {
            try
            {

                string qry = "Select a.InspectBy,a.InspectDate as [Inpection Date],b.UniqueID as [UIdent. No.],c.AssignedNumber as [Winch No.],c.Location,b.CertificateNumber as [C.No.],a.ExternalRating_A as [External],a.InternalRating_A as [Internal],a.AverageRating_A as [Average],a.LengthOFAbrasion_A as [Length of Abrasion],a.CutYarnCount_A as [Cut Yarn counted],a.LengthOFGlazing_A as [Length of Glazing],a.Chafe_guard_condition as [Chafe Guard],a.Twist,Image1 as [Photo1],Image2 as [Photo2] from MooringRopeInspection a Inner Join MooringRopeDetail b on a.RopeId = b.Id left Join MooringWinchDetail c on a.WinchId = c.Id where b.DeleteStatus = 0 and a.RopeTail = 0 and a.IsActive=1";
                SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                string qry1 = "Select a.InspectBy,a.InspectDate as [Inpection Date],b.UniqueID as [UIdent. No.],c.AssignedNumber as [Winch No.],c.Location,b.CertificateNumber as [C.No.], a.ExternalRating_B as [External],a.InternalRating_B as [Internal],a.AverageRating_B as [Average],a.LengthOFAbrasion_B as [Length of Abrasion],a.DistanceOutboard_B as [Distance from outboard eye],a.CutYarnCount_B as [Cut Yarn counted],a.LengthOFGlazing_B as [Length of Glazing],a.Chafe_guard_condition as [Chafe Guard],a.Twist,Image1 as [Photo1],Image2 as [Photo2] from MooringRopeInspection a Inner Join MooringRopeDetail b on a.RopeId = b.Id left Join MooringWinchDetail c on a.WinchId = c.Id where b.DeleteStatus = 0 and a.RopeTail = 0 and a.IsActive=1";
                SqlDataAdapter adp1 = new SqlDataAdapter(qry1, sc.con);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "MooringLineInspection_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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
                        dt.TableName = "ZONE-A";
                        var protectedsheet = wb.Worksheets.Add(dt);
                        protectedsheet.Name = dt.TableName;
                        var projection = protectedsheet.Protect("49WEB$TREET#");
                        projection.InsertColumns = true;
                        projection.InsertRows = true;

                        dt1.TableName = "ZONE-B";
                        var protectedsheet1 = wb.Worksheets.Add(dt1);
                        protectedsheet1.Name = dt1.TableName;
                        var projection1 = protectedsheet1.Protect("49WEB$TREET#");
                        projection.InsertColumns = true;
                        projection.InsertRows = true;

                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;
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

              ~RopeInspectionListViewModel()
              {

              }
       }

       public class TotalInspections
       {
              public int id { get; set; }
              public string InspectBy { get; set; }
              public DateTime InspectDate { get; set; }

              public int RowId { get; set; }

       }
}
