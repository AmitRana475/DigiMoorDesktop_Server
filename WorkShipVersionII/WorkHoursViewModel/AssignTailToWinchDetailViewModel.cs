using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;
using ClosedXML.Excel;
using System.IO;
using Microsoft.Win32;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class AssignTailToWinchDetailViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _AssignTailToWinchDetail;
              public ICommand HelpCommand { get; private set; }

              public AssignTailToWinchDetailViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _AssignTailToWinchDetail = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     viewCommand = new RelayCommand<AssignModuleToWinchClass>(ViewMooringWinch);
                     editCommand = new RelayCommand<AssignModuleToWinchClass>(EditMooringWinch);
                     deleteCommand = new RelayCommand<AssignModuleToWinchClass>(DeleteMooringWinch);
                     changestatuscommand = new RelayCommand<AssignModuleToWinchClass>(ShifttoInactive);

                     this._ExportAssignTailToWinchDetailCommands = new RelayCommand(() => _AssignTailToWinchDetail.RunWorkerAsync(), () => !_AssignTailToWinchDetail.IsBusy);
                     this._AssignTailToWinchDetail.DoWork += new DoWorkEventHandler(ExportAssignTailToWinchDetailMethod);


                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     GetAssignList();
                     GetAssignList1();
                     // OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

              }

              public AssignTailToWinchDetailViewModel(ObservableCollection<AssignModuleToWinchClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     //LoadUserAccess.Clear();
                     //LoadUserAccess1.Clear();

                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     GetAssignList();
                     GetAssignList1();

                     //foreach (var item in ass)

                     //{
                     //    LoadUserAccess.Add(new AssignModuleToWinchClass() { Id = item.Id, AssignedNumber=item.AssignedNumber,CertificateNumber=item.CertificateNumber,CreatedDate=item.CreatedDate });
                     //}

              }

              //ExportExcel
              private ICommand _ExportAssignTailToWinchDetailCommands;
              public ICommand ExportAssignTailToWinchDetailCommands
              {
                     get
                     {

                            return _ExportAssignTailToWinchDetailCommands;
                     }
              }

              private ICommand changestatuscommand;
              public ICommand ChangeStatusCommand
              {
                     get { return changestatuscommand; }
                     set { changestatuscommand = value; }
              }


              private void ShifttoInactive(AssignModuleToWinchClass mw)
              {
                     try
                     {

                            var result1 = sc.AssignRopetoWinch.SingleOrDefault(b => b.RopeId == mw.RopeId && b.WinchId == mw.WinchId && b.RopeTail == 1 && b.IsActive == true);
                            if (result1 != null)
                            {

                                   result1.IsActive = false;
                                   result1.ModifiedBy = "Admin";

                                   result1.ModifiedDate = DateTime.Now;
                                   sc.SaveChanges();
                            }


                            MessageBox.Show("Rope Tail Shifted to InActive ", "Assign RopeTailtoWinch", MessageBoxButton.OK, MessageBoxImage.Information);

                            //.....Refresh DataGrid........


                            var lostdata = new ObservableCollection<AssignModuleToWinchClass>(sc.AssignRopetoWinch.ToList());
                            AssignTailToWinchDetailViewModel cc = new AssignTailToWinchDetailViewModel(lostdata);

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void ViewMooringWinch(AssignModuleToWinchClass mw)
              {
                     try
                     {
                            ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch(mw);
                            StaticHelper.ViewId = mw.Id;
                            StaticHelper.RopeTailId = 1;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewAssignRopetoWinch() { DataContext = vm });

                            //ChildWindowManager.Instance.CloseChildWindow();
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

              private static ObservableCollection<AssignModuleToWinchClass> loadMooringWinchList = new ObservableCollection<AssignModuleToWinchClass>();

              public ObservableCollection<AssignModuleToWinchClass> LoadMooringWinchList
              {
                     get
                     {
                            return loadMooringWinchList;
                     }
                     set
                     {
                            loadMooringWinchList = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringWinchList"));

                     }
              }


              public static ObservableCollection<AssignModuleToWinchClass> loadUserAccess = new ObservableCollection<AssignModuleToWinchClass>();
              public ObservableCollection<AssignModuleToWinchClass> LoadUserAccess
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

              public static ObservableCollection<AssignModuleToWinchClass> loadUserAccess1 = new ObservableCollection<AssignModuleToWinchClass>();
              public ObservableCollection<AssignModuleToWinchClass> LoadUserAccess1
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

              public void GetAssignList()
              {

                     try
                     {
                            LoadUserAccess.Clear();
                            //ObservableCollection<AssignModuleToWinchClass> moringlist = new ObservableCollection<AssignModuleToWinchClass>();

                            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                            //SqlDataAdapter adp = new SqlDataAdapter("select a.id,a.Outboard,a.RopeId,a.WinchId,a.CreatedBy, a.AssignedLocation,a.createddate, b.certificatenumber,c.assignednumber from AssignRopeToWinch a inner join MooringRopeDetail b on a.RopeId=b.Id inner join MooringWinchDetail c on a.WinchId=c.Id", con);

                            SqlDataAdapter adp = new SqlDataAdapter("GetAssignRopeToWinch", sc.con);
                            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 1);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            // var data = sc.AssignRopetoWinch.ToList();          
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                   LoadUserAccess.Add(new AssignModuleToWinchClass()
                                   {
                                          Id = (int)row["Id"],
                                          AssignedNumber = (string)row["assignednumber"],
                                          CertificateNumber = (string)row["certificatenumber"],
                                          UniqueId = (string)row["UniqueId"],
                                          //AssignedDate = (DateTime)row["AssignedDate"],

                                          AssignedDate1 = ((string)row["AssignedDate"]),
                                          //AssignedDate1 = ((DateTime)row["AssignedDate"]).ToString("yyyy-MM-dd"),
                                          CreatedDate = (DateTime)row["createddate"],
                                          RopeId = (int)row["RopeId"],
                                          WinchId = (int)row["WinchId"],
                                          CreatedBy = (string)row["Createdby"],
                                          Status = (string)row["Status"]

                                   });
                            }

                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                            //return null;
                     }

              }

              public void GetAssignList1()
              {
                     try
                     {
                            LoadUserAccess1.Clear();
                            //ObservableCollection<AssignModuleToWinchClass> moringlist = new ObservableCollection<AssignModuleToWinchClass>();

                            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                            //SqlDataAdapter adp = new SqlDataAdapter("select a.id,a.Outboard,a.RopeId,a.WinchId,a.CreatedBy, a.AssignedLocation,a.createddate, b.certificatenumber,c.assignednumber from AssignRopeToWinch a inner join MooringRopeDetail b on a.RopeId=b.Id inner join MooringWinchDetail c on a.WinchId=c.Id", con);

                            SqlDataAdapter adp = new SqlDataAdapter("GetAssignRopeToWinch", sc.con);
                            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 1);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            // var data = sc.AssignRopetoWinch.ToList();          
                            foreach (DataRow row in ds.Tables[1].Rows)
                            {
                                   LoadUserAccess1.Add(new AssignModuleToWinchClass()
                                   {
                                          Id = (int)row["Id"],
                                          AssignedNumber = (string)row["assignednumber"],
                                          CertificateNumber = (string)row["certificatenumber"],
                                          UniqueId = (string)row["UniqueId"],
                                          //AssignedDate = (DateTime)row["AssignedDate"],
                                          AssignedDate1 = ((string)row["AssignedDate"]),
                                          //AssignedDate1 = ((DateTime)row["AssignedDate"]).ToString("yyyy-MM-dd"),
                                          CreatedDate = (DateTime)row["createddate"],
                                          RopeId = (int)row["RopeId"],
                                          WinchId = (int)row["WinchId"],
                                          CreatedBy = (string)row["Createdby"],
                                          Status = (string)row["Status"]

                                   });
                            }

                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess1"));
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                            //return null;
                     }

              }
              //private static DBManager dm;
              //public DataSet GetHolidays()
              //{
              //    dm.Open();
              //    var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetHolidays");
              //    dm.Close();

              //    return data;
              //}
              private void EditMooringWinch(AssignModuleToWinchClass mw)
              {
                     try
                     {
                            AssignRopeToWinchViewModel vm = new AssignRopeToWinchViewModel(mw);
                            ChildWindowManager.Instance.ShowChildWindow(new AssignRopeToWinchView() { DataContext = vm });
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void DeleteMooringWinch(AssignModuleToWinchClass mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   AssignModuleToWinchClass findrank = sc.AssignRopetoWinch.Where(x => x.Id == mw.Id).FirstOrDefault();
                                   if (findrank != null)
                                   {

                                          //var checkDepartment = sc.AssignRopetoWinch.Where(x => x.Id.Equals(mw.Id)).FirstOrDefault();
                                          //if (checkDepartment != null)
                                          //{
                                          //    MessageBox.Show("Sorry you can't delete this Department, as it is assigned to some crew members.", "Delete  Department", MessageBoxButton.OK, MessageBoxImage.Stop);
                                          //}
                                          //else
                                          //{

                                          //SqlDataAdapter adp = new SqlDataAdapter("update AssignRopeToWinch set isdelete='True' , IsActive='False' where Id =" + mw.Id + " ", sc.con);
                                          //DataTable dt = new DataTable();
                                          //adp.Fill(dt);
                        SqlDataAdapter adp = new SqlDataAdapter("UpdatetAssignRopeToWinch", sc.con);
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);

                        //sc.Entry(findrank).State = EntityState.Deleted;
                        sc.SaveChanges();

                                          MessageBox.Show("Record deleted successfully ", "Delete Assign RopetailtoWinch", MessageBoxButton.OK, MessageBoxImage.Information);

                                          //.....Refresh DataGrid........


                                          var lostdata = new ObservableCollection<AssignModuleToWinchClass>(sc.AssignRopetoWinch.ToList());
                                          AssignTailToWinchDetailViewModel cc = new AssignTailToWinchDetailViewModel(lostdata);



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
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void ExportAssignTailToWinchDetailMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportAssignTailToWinchDetailMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void ExportAssignTailToWinchDetailMethod1()
              {
            try
            {


                DataSet ds = null;
                ds = new DataSet("General");
                ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

                //string qry = "ViewAssignRopeToWinchExcel";
                //SqlCommand cmd = new SqlCommand(qry, sc.con);
                DataSet dt = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter("ViewAssignTailToWinchExcel", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 1);
                //cmd.Parameters.AddWithValue("@RopeTail", 0);
                //adp.SelectCommand = cmd;
                adp.Fill(dt);

                dt.Tables[0].TableName = "CurrentRecord";
                dt.Tables[1].TableName = "PastRecord";

                ds = dt;

                //cmd.Dispose();
                adp.Dispose();
                dt.Dispose();
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "AssignRopeTailToWinchDetails_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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
