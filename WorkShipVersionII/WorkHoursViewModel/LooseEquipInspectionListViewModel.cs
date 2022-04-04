using ClosedXML.Excel;
using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WorkShipVersionII.Commands;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;
using static WorkShipVersionII.WorkHoursViewModel.LooseEquipInspectionDetailsViewModel;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class LooseEquipInspectionListViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _ExportLooseEquipmentList;
              private static int itemPerPage = 1;
              private int itemcount;
              public ICommand HelpCommand { get; private set; }
              int looseetypeid = 1;
              public LooseEquipInspectionListViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                            _ExportLooseEquipmentList = new BackgroundWorker();
                     }
                     searchcmmand = new RelayCommand(GetInspectList);
                     yearname = sc.GetYear();
                     GetInspectDate(DateTime.Now.Year.ToString());
                     //syearname = null;

                     string yr = SYearName;
                     viewCommand = new RelayCommand<MooringLooseEquipInspectionClass>(ViewLooseEInspection);

                     viewCommandL1 = new RelayCommand<MooringLooseEquipInspectionClass>(Viewimage);
                     viewCommandL2 = new RelayCommand<MooringLooseEquipInspectionClass>(Viewimage1);
                     editCommand = new RelayCommand<MooringLooseEquipInspectionClass>(EditInspection);
                     deleteCommand = new RelayCommand<MooringLooseEquipInspectionClass>(DeleteInspection);
                     GetInspectionList(looseetypeid, yr);

                     ropetype = GetRopeType();
                     SRopeType = ropetype.Where(x => x.Id == looseetypeid).FirstOrDefault();
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));


                     _ExportLooseEquipmentListCommands = new RelayCommand(() => _ExportLooseEquipmentList.RunWorkerAsync(), () => !_ExportLooseEquipmentList.IsBusy);
                     _ExportLooseEquipmentList.DoWork += new DoWorkEventHandler(ExportLooseEquipmentListMethod);


                     //NextCommand = new NextPageCommandLooseEInspection(this);
                     //PreviousCommand = new PreviousPageCommandLooseEInspection(this);
                     //FirstCommand = new FirstPageCommandLooseEInspection(this);
                     //LastCommand = new LastPageCommandLooseEInspection(this);
              }
              private ICommand _ExportLooseEquipmentListCommands;
              public ICommand ExportLooseEquipmentListCommands
              {
                     get
                     {

                            return _ExportLooseEquipmentListCommands;
                     }
              }

              public DataTable ExportTable { get; set; }

              private void ExportLooseEquipmentListMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportLooseEquipmentInspection();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              public void ExportLooseEquipmentInspection()
              {
                     //   SRopeType

                    

                     try
                     {
                            string Sheetname = sropetype.LooseEquipmentType.Replace(" ", "");

                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "LooseEqInspection_Export" + Sheetname + DateTime.Now.ToString("dd-MMM-yyyy");
                            sfd.DefaultExt = "xlsx";
                            sfd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                            if (sfd.ShowDialog() == true)
                            {
                                   SqlDataAdapter adp = new SqlDataAdapter("select a.InspectBy,a.InspectDate,b.looseequipmenttype as [Loose Eq. Type],a.Number,a.Condition,a.Remarks from MooringLooseEquipInspection a inner join LooseEType b on a.LooseETypeId=b.Id where LooseETypeId=" + sropetype.Id + "", sc.con);

                                   DataTable dt = new DataTable();
                                   adp.Fill(dt);

                                   if (dt.Rows.Count > 0)
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

                                         
                                          //DataTable dt = ExportTable;
                                         
                                          using (XLWorkbook wb = new XLWorkbook())
                                          {
                                                 dt.TableName = Sheetname;// SRopeType.LooseEquipmentType;
                                                 var protectedsheet = wb.Worksheets.Add(dt);
                                                 protectedsheet.Name = Sheetname; // ExportTable.TableName;
                                                 var projection = protectedsheet.Protect("49WEB$TREET#");
                                                 projection.InsertColumns = true;
                                                 projection.InsertRows = true;

                                                 wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                                 wb.Style.Font.Bold = true;
                                                 wb.SaveAs(sfd.FileName);
                                                 MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                                          }
                                   }
                                   else
                                   {
                                          MessageBox.Show("Data not found for export.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                                   }
                            }

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
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
              private void GetInspectList()
              {
                     loadUserAccess.Clear();
                     GetInspectionList(looseetypeid, syearname);
                     OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
              }
              private void Viewimage(MooringLooseEquipInspectionClass mw)
              {
                     try
                     {

                            StaticHelper.ViewId = mw.Id;
                            StaticHelper.Photos = "Image1";
                            StaticHelper.TbName = "MooringLooseEquipInspection";
                            ChildWindowManager.Instance.ShowChildWindow(new ViewPhoto());


                     }
                     catch (Exception ex)
                     {

                     }
              }

              private void Viewimage1(MooringLooseEquipInspectionClass mw)
              {
                     try
                     {

                            StaticHelper.ViewId = mw.Id;
                            StaticHelper.Photos = "Image2";
                            StaticHelper.TbName = "MooringLooseEquipInspection";
                            ChildWindowManager.Instance.ShowChildWindow(new ViewPhoto());


                     }
                     catch (Exception ex)
                     {

                     }
              }
              #region PaginationWork

              public ICommand PreviousCommand { get; private set; }
              public ICommand NextCommand { get; private set; }
              public ICommand FirstCommand { get; private set; }
              public ICommand LastCommand { get; private set; }

              private int _currentPageIndex;
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




              private int _CurrentPage;
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
              private int _totalPages;
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
                            int index = ((MooringLooseEquipInspectionClass)e.Item).Id - 1;
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
              private ICommand searchcmmand;

              public ICommand SearchCommand
              {
                     get { return searchcmmand; }
                     set { searchcmmand = value; }
              }
              public ObservableCollection<string> GetYear()
              {
                     var years = new ObservableCollection<string>();
                     int year = DateTime.Now.Year;
                     for (int i = year - 7; i <= year + 7; i++)
                     {
                            years.Add(i.ToString());
                     }
                     return years;
              }
              private void GetInspectDate(string mysdate)
              {
                     //var Dtlist = new ObservableCollection<string>();

                     // var data = sc.MooringRopeInspectionTbl.Where(x=> x.InspectDate  )

                     string qry = "select distinct InspectDate from MooringLooseEquipInspection where DATEPART(YYYY,InspectDate) = '" + mysdate + "'";
                     SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
                     DataTable datatbl = new DataTable();
                     sda.Fill(datatbl);

                     for (int i = 0; i < datatbl.Rows.Count; i++)
                     {
                            var date = Convert.ToDateTime(datatbl.Rows[i]["InspectDate"]).ToString("yyyy-MM-dd");
                            DateList.Add(date);
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
              public LooseEquipInspectionListViewModel(ObservableCollection<MooringLooseEquipInspectionClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     GetInspectionList(1, syearname);
                     ropetype = GetRopeType();
              }

              private static ObservableCollection<LooseETypeC> ropetype = new ObservableCollection<LooseETypeC>();
              public ObservableCollection<LooseETypeC> RopeType
              {
                     get
                     {
                            return ropetype;
                     }
                     set
                     {
                            // var data = sc.AssignRopetoWinch.Where(x => x.Id == ropetype).FirstOrDefault();
                            ropetype = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
                     }
              }


              public static LooseETypeC sropetype;// = new Ropetypecombo();
              public LooseETypeC SRopeType
              {
                     get
                     {

                            if (sropetype != null)
                            {
                                   //ViewList.View.Refresh();
                                   GetInspectionList(sropetype.Id, syearname);
                                 
                                   //ViewList = new CollectionViewSource
                                   //{
                                   //    Source = LoadUserAccess
                                   //};


                                   //ViewList.Filter += new FilterEventHandler(View_Filter);


                                   //itemcount = LoadUserAccess.Count(); //sc.Notifications.Count();
                                   //CalculateTotalPages();
                                   //ViewList.View.Refresh();

                                   //NextCommand = new NextPageCommandLooseEInspection(this);
                                   //PreviousCommand = new PreviousPageCommandLooseEInspection(this);
                                   //FirstCommand = new FirstPageCommandLooseEInspection(this);
                                   //LastCommand = new LastPageCommandLooseEInspection(this);

                                   looseetypeid = sropetype.Id;
                                   StaticHelper.LooseEId = sropetype.Id;
                                   StaticHelper.TbName = sropetype.LooseEquipmentType;
                                   //assignrope.Clear();
                                   //assignrope = GetAssRope(data.WinchId);
                                   RaisePropertyChanged("AssignRope");




                                   OnPropertyChanged(new PropertyChangedEventArgs("MooringInspect"));
                            }
                            return sropetype;

                     }
                     set
                     {

                            sropetype = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("SRopeType"));

                            //var data = sc.AssignRopetoWinch.Where(x => x.RopeId == SRopeType.Id).FirstOrDefault();
                     }
              }
              private ObservableCollection<LooseETypeC> GetRopeType()
              {
                     ObservableCollection<LooseETypeC> AddRopeType = new ObservableCollection<LooseETypeC>();
                     //var data = sc.MooringWinchRope.Select(x => new { x.Id, x.CertificateNumber }).ToList();

                     LooseETypeC rop;

                     SqlDataAdapter adp = new SqlDataAdapter("select * from LooseEType where id !=2", sc.con);
                     System.Data.DataTable ds = new System.Data.DataTable();
                     adp.Fill(ds);

                     foreach (DataRow row in ds.Rows)
                     {


                            rop = new LooseETypeC();
                            rop.Id = (int)row["Id"];
                            rop.LooseEquipmentType = (string)row["LooseEquipmentType"];
                            AddRopeType.Add(rop);

                     }




                     return AddRopeType;
              }
              private void ViewLooseEInspection(MooringLooseEquipInspectionClass mw)
              {
                     try
                     {

                            StaticHelper.ViewId = mw.Id;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewLooseEInspection());


                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private ICommand viewCommand;
              public ICommand ViewCommand
              {
                     get { return viewCommand; }
                     set { viewCommand = value; }
              }

              private ICommand viewCommandL1;
              public ICommand ViewCommandL1
              {
                     get { return viewCommandL1; }
                     set { viewCommandL1 = value; }
              }

              private ICommand viewCommandL2;
              public ICommand ViewCommandL2
              {
                     get { return viewCommandL2; }
                     set { viewCommandL2 = value; }
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


              public static CollectionViewSource ViewList { get; set; }
              public static ObservableCollection<MooringLooseEquipInspectionClass> loadUserAccess = new ObservableCollection<MooringLooseEquipInspectionClass>();
              // public static ObservableCollection<MooringLooseEquipInspectionClass> loadUserAccess = new ObservableCollection<MooringLooseEquipInspectionClass>();
              public ObservableCollection<MooringLooseEquipInspectionClass> LoadUserAccess
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
              public void GetInspectionList(int looseEtype, string year)
              {
                     try
                     {
                            //ViewList.View.Refresh();
                            loadUserAccess.Clear();
                            SqlCommand cmd = new SqlCommand("GetLooseEquipInspectionList", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@LooseEtype", looseEtype);
                            cmd.Parameters.AddWithValue("@Year", year);
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);
                            if(ds.Rows.Count>0)
                            {
                                   ExportTable = ds;
                            }
                            foreach (DataRow row in ds.Rows)
                            {
                                   loadUserAccess.Add(new MooringLooseEquipInspectionClass()
                                   {
                                          Id = (int)row["Id"],
                                          InspectBy = (string)row["InspectBy"],
                                          // Condition = (string)row["Condition"],

                                          Condition = (row["Condition"] == DBNull.Value) ? " " : row["Condition"].ToString(),
                                          looseequipmenttype = (string)row["looseequipmenttype"],
                                          InspectDate1 = (string)row["InspectDate"],
                                          // Remarks = (string)row["Remarks"],

                                          Remarks = (row["Remarks"] == DBNull.Value) ? " " : row["Remarks"].ToString(),

                                          Photo11 = (row["Photo11"] == DBNull.Value) ? "Not Assigned" : row["Photo11"].ToString(),
                                          Photo12 = (row["Photo12"] == DBNull.Value) ? "Not Assigned" : row["Photo12"].ToString(),
                                          //Photo11 = datatbl.Rows[i]["Photo11"].ToString(),
                                          //Photo12 = datatbl.Rows[i]["Photo12"].ToString(),
                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));


                            //RaisePropertyChanged("LoadUserAccess");



                            //ViewList = new CollectionViewSource
                            //{
                            //    Source = loadUserAccess
                            //};


                            //ViewList.Filter += new FilterEventHandler(View_Filter);


                            //itemcount = loadUserAccess.Count(); //sc.Notifications.Count();
                            //CalculateTotalPages();
                            //ViewList.View.Refresh();

                            //NextCommand = new NextPageCommandLooseEInspection(this);
                            //PreviousCommand = new PreviousPageCommandLooseEInspection(this);
                            //FirstCommand = new FirstPageCommandLooseEInspection(this);
                            //LastCommand = new LastPageCommandLooseEInspection(this);

                     }
                     catch (Exception ex)
                     {

                            sc.ErrorLog(ex);
                     }
              }
              private void EditInspection(MooringLooseEquipInspectionClass mw)
              {
                     try
                     {
                            LooseEquipInspectionDetailsViewModel vm = new LooseEquipInspectionDetailsViewModel(mw);
                            // ChildWindowManager.Instance.ShowChildWindow(new LooseEquipInspectionDetailsView() { DataContext = vm });
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void DeleteInspection(MooringLooseEquipInspectionClass mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   MooringLooseEquipInspectionClass findLooseEI = sc.LooseEquipInspection.Where(x => x.Id == mw.Id).FirstOrDefault();
                                   if (findLooseEI != null)
                                   {
                                          sc.Entry(findLooseEI).State = EntityState.Deleted;
                                          sc.SaveChanges();


                                          try
                                          {
                                                 var notiid = findLooseEI.NotificationId;
                                                 var notidelete = sc.Notifications.Where(x => x.Id == notiid).FirstOrDefault();
                                                 sc.Entry(notidelete).State = EntityState.Deleted;
                                                 sc.SaveChanges();
                                          }
                                          catch { }

                                          MessageBox.Show("Record deleted successfully ", "Delete Inspection", MessageBoxButton.OK, MessageBoxImage.Information);

                                          looseetypeid = mw.LooseETypeId;
                                          var lostdata = new ObservableCollection<MooringLooseEquipInspectionClass>(sc.LooseEquipInspection.ToList());
                                          LooseEquipInspectionListViewModel cc = new LooseEquipInspectionListViewModel(lostdata);


                                   }
                                   else
                                   {
                                          MessageBox.Show("Record is not found ", "Delete Inspection ", MessageBoxButton.OK, MessageBoxImage.Information);
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
