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
       public class RopeStatusViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private static int itemPerPage = 5;
              private int itemcount;
              public ICommand HelpCommand { get; private set; }
              public RopeStatusViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     editCommand = new RelayCommand<MooringWinchRopeClass>(EditMooringWinch);
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     GetMooringWinchRopeList(0,"Line");
                     GetMooringWinchRopeList1(0,"Line");
                     NextCommand = new NextPageCommandRopeStatus(this);
                     PreviousCommand = new PreviousPageCommandRopeStatus(this);
                     FirstCommand = new FirstPageCommandRopeStatus(this);
                     LastCommand = new LastPageCommandRopeStatus(this);

              }

              public RopeStatusViewModel(ObservableCollection<MooringWinchRopeClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     LoadUserAccess.Clear();

            GetMooringWinchRopeList(0, "Line");
            GetMooringWinchRopeList1(0, "Line");

            NextCommand = new NextPageCommandRopeStatus(this);
                     PreviousCommand = new PreviousPageCommandRopeStatus(this);
                     FirstCommand = new FirstPageCommandRopeStatus(this);
                     LastCommand = new LastPageCommandRopeStatus(this);


              }
             
              private ICommand editCommand;
              public ICommand EditCommand
              {
                     get { return editCommand; }
                     set { editCommand = value; }
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

              private decimal TotalCroped(int Ropeid)
              {
                     decimal crop = 0;
                     string qry = @"select SUM(LengthofCroppedRope)as TotalCroped,b.Length,a.RopeId from RopeCropping a join MooringRopeDetail b on a.RopeId=b.Id where a.IsActive=1 and a.RopeId=@RopeId
group by b.Length,a.RopeId";
                     SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
                     sda.SelectCommand.Parameters.AddWithValue("@RopeId", Ropeid);
                     DataTable tbl = new DataTable();
                     sda.Fill(tbl);
                     if(tbl.Rows.Count>0)
                     {
                            decimal totalCrop = Convert.ToDecimal(tbl.Rows[0]["TotalCroped"]);
                            decimal Length = Convert.ToDecimal(tbl.Rows[0]["Length"]);
                            crop = Length - totalCrop;
                            
                     }
                   
                     return crop;
              }

              public int LineRopeTail { get; set; }
        public string LineRes { get; set; }

        public void GetMooringWinchRopeList(int RopeTail,string LineResidual)
              {
                     try
                     {
                            LineRopeTail = RopeTail;
                LineRes = LineResidual;
                            LoadUserAccess.Clear();
                            SqlCommand cmd = new SqlCommand("GetMooringRopeDetailList", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", RopeTail);
                cmd.Parameters.AddWithValue("@AttachedText", LineResidual);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                   LoadUserAccess.Add(new MooringWinchRopeClass()
                                   {
                                          // var data = Convert.ToDateTime(row["StartOn"].ToString()).ToString("MMM dd").ToString()
                                          Id = (int)row["Id"],
                                          //AssignedWinch = (string)row["AssignedWinch"],
                                          CertificateNumber = (string)row["CertificateNumber"],
                                          UniqueID = (string)row["UniqueID"],
                                          LDBF = (decimal)row["LDBF"],
                                          MBL = (decimal)row["MBL"],
                                          DiaMeter = (decimal)row["DiaMeter"],
                                          Length = (decimal)row["Length"],
                                          CurrentLength = TotalCroped(Convert.ToInt32(row["Id"])) == 0? Convert.ToDecimal(row["Length"]) : TotalCroped(Convert.ToInt32(row["Id"])),
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
                                          //CurrentRunningHours = row["CurrentRunningHours"] == DBNull.Value ? 0 : (int)row["CurrentRunningHours"],
                                          CurrentRunningHours = row["CurrentRunningHours"] == DBNull.Value ? 0 : (decimal)row["CurrentRunningHours"],
                                          Remarks = (row["Remarks"] == DBNull.Value) ? string.Empty : row["Remarks"].ToString(),
                                          RopeTail = (int)row["RopeTail"],
                                          InspectionDueDate1 = (row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"].ToString(),

                                          //InspectionDueDate = Convert.ToDateTime((row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"]),
                                          //InspectionDueDate1 = ((DateTime) (row["InspectionDueDate"]) == null) ? null : ((DateTime)row["InspectionDueDate"]).ToString("d MMM, yyyy"),
                                          //InspectionDueDate1 = Convert.ToDateTime( (row["InspectionDueDate"] == DBNull.Value) ? "" : row["InspectionDueDate"]).ToString("d MMM, yyyy"),
                                          ManufacturerName = (row["ManufacturerName"] == DBNull.Value) ? string.Empty : row["ManufacturerName"].ToString(),
                                          Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                                          AssignedWinch = (row["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : row["AssignedWinch"].ToString(),
                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }
              }
              public void GetMooringWinchRopeList(string Searchtext)
              {
                     try
                     {
                            LoadUserAccess.Clear();
                            SqlCommand cmd = new SqlCommand("GetMooringRopeDetailListSearch", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", LineRopeTail);
                            cmd.Parameters.AddWithValue("@Searchtext", Searchtext);
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                   LoadUserAccess.Add(new MooringWinchRopeClass()
                                   {
                                          // var data = Convert.ToDateTime(row["StartOn"].ToString()).ToString("MMM dd").ToString()
                                          Id = (int)row["Id"],
                                          //AssignedWinch = (string)row["AssignedWinch"],
                                          CertificateNumber = (string)row["CertificateNumber"],
                                          UniqueID = (string)row["UniqueID"],
                                          LDBF = (decimal)row["LDBF"],
                                          MBL = (decimal)row["MBL"],
                                          DiaMeter = (decimal)row["DiaMeter"],
                                          Length = (decimal)row["Length"],
                                          CurrentLength = TotalCroped(Convert.ToInt32(row["Id"])) == 0 ? Convert.ToDecimal(row["Length"]) : TotalCroped(Convert.ToInt32(row["Id"])),
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
                                          //CurrentRunningHours = row["CurrentRunningHours"] == DBNull.Value ? 0 : (int)row["CurrentRunningHours"],
                                          CurrentRunningHours = row["CurrentRunningHours"] == DBNull.Value ? 0 : (decimal)row["CurrentRunningHours"],
                                          Remarks = (row["Remarks"] == DBNull.Value) ? string.Empty : row["Remarks"].ToString(),
                                          RopeTail = (int)row["RopeTail"],
                                          InspectionDueDate1 = (row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"].ToString(),

                                          //InspectionDueDate = Convert.ToDateTime((row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"]),
                                          //InspectionDueDate1 = ((DateTime) (row["InspectionDueDate"]) == null) ? null : ((DateTime)row["InspectionDueDate"]).ToString("d MMM, yyyy"),
                                          //InspectionDueDate1 = Convert.ToDateTime( (row["InspectionDueDate"] == DBNull.Value) ? "" : row["InspectionDueDate"]).ToString("d MMM, yyyy"),
                                          ManufacturerName = (row["ManufacturerName"] == DBNull.Value) ? string.Empty : row["ManufacturerName"].ToString(),
                                          Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                                          AssignedWinch = (row["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : row["AssignedWinch"].ToString(),
                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }
              }
              public void GetMooringWinchRopeList1(int RopeTail, string LineResidual)
              {
                     try
                     {
                            LineRopeTail = RopeTail;
                LineRes = LineResidual;
                int rowid = 1;
                            LoadUserAccess1.Clear();
                            SqlCommand cmd = new SqlCommand("GetMooringRopeDetailList", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", RopeTail);
                cmd.Parameters.AddWithValue("@AttachedText", LineResidual);


                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Tables[1].Rows)
                            {
                                   LoadUserAccess1.Add(new MooringWinchRopeClass()
                                   {
                                          RowId = rowid++,
                                          // var data = Convert.ToDateTime(row["StartOn"].ToString()).ToString("MMM dd").ToString()
                                          Id = (int)row["Id"],
                                          //AssignedWinch = (string)row["AssignedWinch"],
                                          CertificateNumber = (string)row["CertificateNumber"],
                                          UniqueID = (string)row["UniqueID"],
                                          LDBF = (decimal)row["LDBF"],
                                          MBL = (decimal)row["MBL"],
                                          DiaMeter = (decimal)row["DiaMeter"],
                                          Length = (decimal)row["Length"],
                                          CurrentLength = TotalCroped(Convert.ToInt32(row["Id"])) == 0 ? Convert.ToDecimal(row["Length"]) : TotalCroped(Convert.ToInt32(row["Id"])),
                                          WLL = (decimal)row["WLL"],
                                          ReceivedDate = (DateTime)row["ReceivedDate"],
                                          RopeTagging = (string)row["RopeTagging"],
                                          RopeConstruction = (string)row["RopeConstruction"],
                                          // InstalledDate = (DateTime)row["InstalledDate"],
                                          RopeType = (string)row["RopeType"],
                                          RopeTypeId = (int)row["RopeTypeId"],
                                          ManufacturerId = (int)row["ManufacturerId"],
                                          // InstalledDate1 = ((DateTime)row["InstalledDate"]).ToString("d MMM, yyyy"),
                                          InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),
                                          CurrentRunningHours = row["CurrentRunningHours"] == DBNull.Value ? 0 : (decimal)row["CurrentRunningHours"],
                                          Remarks = (row["Remarks"] == DBNull.Value) ? string.Empty : row["Remarks"].ToString(),
                                          //CurrentRunningHours = (int)row["CurrentRunningHours"],
                                          //Remarks = (string)row["Remarks"],

                                          //InspectionDueDate1 = (row["InspectionDueDate"] == DBNull.Value) ? "" : row["InspectionDueDate"].ToString(),
                                          //InspectionDueDate = Convert.ToDateTime((row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"]),
                                          RopeTail = (int)row["RopeTail"],
                                          InspectionDueDate1 = (row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"].ToString(),
                                          ManufacturerName = (row["ManufacturerName"] == DBNull.Value) ? string.Empty : row["ManufacturerName"].ToString(),
                                          Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                                          AssignedWinch = (row["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : row["AssignedWinch"].ToString(),
                                   });
                            }
                            //OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess1"));
                            RaisePropertyChanged("LoadMooringOpBirthList");



                            ViewList = new CollectionViewSource
                            {
                                   Source = LoadUserAccess1
                            };


                            ViewList.Filter += new FilterEventHandler(View_Filter);


                            itemcount = LoadUserAccess1.Count(); //sc.Notifications.Count();
                            CalculateTotalPages();
                            ViewList.View.Refresh();

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }
              }

              public void GetMooringWinchRopeList1(string Searchtext)
              {
                     try
                     {
                          
                            int rowid = 1;
                            LoadUserAccess1.Clear();
                            SqlCommand cmd = new SqlCommand("GetMooringRopeDetailListSearch", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", LineRopeTail);
                            cmd.Parameters.AddWithValue("@Searchtext", Searchtext);
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Tables[1].Rows)
                            {
                                   LoadUserAccess1.Add(new MooringWinchRopeClass()
                                   {
                                          RowId = rowid++,
                                          // var data = Convert.ToDateTime(row["StartOn"].ToString()).ToString("MMM dd").ToString()
                                          Id = (int)row["Id"],
                                          //AssignedWinch = (string)row["AssignedWinch"],
                                          CertificateNumber = (string)row["CertificateNumber"],
                                          UniqueID = (string)row["UniqueID"],
                                          LDBF = (decimal)row["LDBF"],
                                          MBL = (decimal)row["MBL"],
                                          DiaMeter = (decimal)row["DiaMeter"],
                                          Length = (decimal)row["Length"],
                                          CurrentLength = TotalCroped(Convert.ToInt32(row["Id"])) == 0 ? Convert.ToDecimal(row["Length"]) : TotalCroped(Convert.ToInt32(row["Id"])),
                                          WLL = (decimal)row["WLL"],
                                          ReceivedDate = (DateTime)row["ReceivedDate"],
                                          RopeTagging = (string)row["RopeTagging"],
                                          RopeConstruction = (string)row["RopeConstruction"],
                                          // InstalledDate = (DateTime)row["InstalledDate"],
                                          RopeType = (string)row["RopeType"],
                                          RopeTypeId = (int)row["RopeTypeId"],
                                          ManufacturerId = (int)row["ManufacturerId"],
                                          // InstalledDate1 = ((DateTime)row["InstalledDate"]).ToString("d MMM, yyyy"),
                                          InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),
                                          CurrentRunningHours = row["CurrentRunningHours"] == DBNull.Value ? 0 : (decimal)row["CurrentRunningHours"],
                                          Remarks = (row["Remarks"] == DBNull.Value) ? string.Empty : row["Remarks"].ToString(),
                                          //CurrentRunningHours = (int)row["CurrentRunningHours"],
                                          //Remarks = (string)row["Remarks"],

                                          //InspectionDueDate1 = (row["InspectionDueDate"] == DBNull.Value) ? "" : row["InspectionDueDate"].ToString(),
                                          //InspectionDueDate = Convert.ToDateTime((row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"]),
                                          RopeTail = (int)row["RopeTail"],
                                          InspectionDueDate1 = (row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"].ToString(),
                                          ManufacturerName = (row["ManufacturerName"] == DBNull.Value) ? string.Empty : row["ManufacturerName"].ToString(),
                                          Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                                          AssignedWinch = (row["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : row["AssignedWinch"].ToString(),
                                   });
                            }
                            //OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess1"));
                            RaisePropertyChanged("LoadMooringOpBirthList");



                            ViewList = new CollectionViewSource
                            {
                                   Source = LoadUserAccess1
                            };


                            ViewList.Filter += new FilterEventHandler(View_Filter);


                            itemcount = LoadUserAccess1.Count(); //sc.Notifications.Count();
                            CalculateTotalPages();
                            ViewList.View.Refresh();

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
                                   GetMooringWinchRopeList(searchCrew);

                            }
                            else
                            {
                                 
                                   GetMooringWinchRopeList(LineRopeTail,LineRes);
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
                                   GetMooringWinchRopeList1(searchCrew1);

                            }
                            else
                            {
                                   GetMooringWinchRopeList1(LineRopeTail,LineRes);
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
                            AddMooringWinchRopeViewModel vm = new AddMooringWinchRopeViewModel(mw);
                            //ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchRopeView() { DataContext = vm });
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