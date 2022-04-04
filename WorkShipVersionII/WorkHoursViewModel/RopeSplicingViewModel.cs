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
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class RopeSplicingViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _ExportRopeSplicing;
              public ICommand HelpCommand { get; private set; }
              public RopeSplicingViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportRopeSplicing = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     viewCommand = new RelayCommand<RopeSplicingClass>(ViewropeSplicing);
                     editCommand = new RelayCommand<RopeSplicingClass>(EditRopeSplicing);
                     deleteCommand = new RelayCommand<RopeSplicingClass>(DeleteRopeSplicing);

                     this._ExportRopeSplicingCommands = new RelayCommand(() => _ExportRopeSplicing.RunWorkerAsync(), () => !_ExportRopeSplicing.IsBusy);
                     this._ExportRopeSplicing.DoWork += new DoWorkEventHandler(ExportRopeSplicingMethod);

                     GetRopeSplicingList();
                     //GetRopeSplicingList();
                     //StaticHelper.HelpFor = @"LMPR\rope\4.2.5  ROPE SPLICING.htm";
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
              }

              public RopeSplicingViewModel(ObservableCollection<RopeSplicingClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     //StaticHelper.HelpFor = @"LMPR\rope\4.2.5  ROPE SPLICING.htm";
                     //HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     LoadUserAccess.Clear();

                     GetRopeSplicingList();

              }

              private void ViewropeSplicing(RopeSplicingClass mw)
              {
                     try
                     {
                            //ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch(mw);
                            StaticHelper.ViewId = mw.Id;
                            StaticHelper.RopeTailId = 0;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewRopeSplicing());

                            //ChildWindowManager.Instance.CloseChildWindow();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              //ExportExcel
              private ICommand _ExportRopeSplicingCommands;
              public ICommand ExportRopeSplicingCommands
              {
                     get
                     {

                            return _ExportRopeSplicingCommands;
                     }
              }

              private ICommand viewCommand;
              public ICommand ViewCommand
              {
                     get { return viewCommand; }
                     set { viewCommand = value; }
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

              private static ObservableCollection<RopeSplicingClass> loadRopeSplicingList = new ObservableCollection<RopeSplicingClass>();

              public ObservableCollection<RopeSplicingClass> LoadRopeSplicingList
              {
                     get
                     {
                            return loadRopeSplicingList;
                     }
                     set
                     {
                            loadRopeSplicingList = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadRopeSplicingList"));

                     }
              }


              public static ObservableCollection<RopeSplicingClass> loadUserAccess = new ObservableCollection<RopeSplicingClass>();
              public ObservableCollection<RopeSplicingClass> LoadUserAccess
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

              //private ObservableCollection<RopeSplicingClass> GetRopeSplicingList()
              //{

              //    try
              //    {
              //        ObservableCollection<RopeSplicingClass> rpsplcinglist = new ObservableCollection<RopeSplicingClass>();

              //        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
              //        SqlCommand cmd = new SqlCommand("GetRopeSplicing", sc.con);
              //        cmd.CommandType = CommandType.StoredProcedure;


              //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
              //        DataTable ds = new DataTable();
              //        adp.Fill(ds);
              //        foreach (DataRow row in ds.Rows)
              //        {
              //            rpsplcinglist.Add(new RopeSplicingClass()
              //            {
              //                Id = (int)row["Id"],
              //                AssignedNumber = (string)row["AssignedNumber"],
              //                CertificateNumber = (string)row["CertificateNumber"],
              //                AssignedLocation = (string)row["AssignedLocation"],
              //                SplicingDoneDate = (DateTime)row["SplicingDoneDate"],                       
              //              SplicingDoneBy = (row["SplicingDoneBy"] == DBNull.Value) ? string.Empty : row["SplicingDoneBy"].ToString(),
              //                SplicingMethod = (row["SplicingMethod"] == DBNull.Value) ? string.Empty : row["SplicingMethod"].ToString(),

              //            });
              //        }

              //        return rpsplcinglist;
              //    }
              //    catch (Exception ex)
              //    {
              //        sc.ErrorLog(ex);
              //        return null;
              //    }

              //}

              public void GetRopeSplicingList()
              {

                     try
                     {
                            LoadUserAccess.Clear();
                            //ObservableCollection<RopeSplicingClass> rpsplcinglist = new ObservableCollection<RopeSplicingClass>();

                            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                            SqlCommand cmd = new SqlCommand("GetRopeSplicing", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", 0);

                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Rows)
                            {
                                   LoadUserAccess.Add(new RopeSplicingClass()
                                   {
                                          Id = (int)row["Id"],
                                          //AssignedNumber = (string)row["AssignedNumber"],
                                          AssignedNumber = (row["AssignedNumber"] == DBNull.Value) ? "Not Assigned" : row["AssignedNumber"].ToString(),
                                          CertificateNumber = (row["CertificateNumber"] == DBNull.Value) ? "Not Assigned" : row["CertificateNumber"].ToString(),


                                          UniqueId = (row["UniqueId"] == DBNull.Value) ? "Not Assigned" : row["UniqueId"].ToString(),

                                          AssignedLocation = (row["AssignedLocation"] == DBNull.Value) ? "Not Assigned" : row["AssignedLocation"].ToString(),
                                          //AssignedLocation = (string)row["AssignedLocation"],
                                          //SplicingDoneDate = (DateTime)row["SplicingDoneDate"],

                                          SplicingDoneDate1 = ((string)row["SplicingDoneDate"]),
                                          //SplicingDoneDate1 = ((DateTime)row["SplicingDoneDate"]).ToString("yyyy-MM-dd"),
                                          SplicingDoneBy = (row["SplicingDoneBy"] == DBNull.Value) ? string.Empty : row["SplicingDoneBy"].ToString(),
                                          SplicingMethod = (row["SplicingMethod"] == DBNull.Value) ? string.Empty : row["SplicingMethod"].ToString(),

                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }

              }

              private void EditRopeSplicing(RopeSplicingClass mw)
              {
                     try
                     {
                            //AddRopeEndtoEndViewModel vm = new AddRopeEndtoEndViewModel(mw);
                            //ChildWindowManager.Instance.ShowChildWindow(new AssignRopeToWinchView() { DataContext = vm });
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void DeleteRopeSplicing(RopeSplicingClass mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   RopeSplicingClass findcrs = sc.RopeSplicing.Where(x => x.Id == mw.Id).FirstOrDefault();
                                   if (findcrs != null)
                                   {

                                          var result1 = sc.RopeSplicing.SingleOrDefault(b => b.Id == mw.Id && b.RopeTail == 0 && b.IsActive == true);
                                          if (result1 != null)
                                          {

                                                 result1.IsActive = false;
                                                 result1.ModifiedBy = "Admin";

                                                 result1.ModifiedDate = DateTime.Now;
                                                 sc.SaveChanges();
                                          }

                                          var result2 = sc.RopeCropping.SingleOrDefault(b => b.SplicedId == mw.Id && b.RopeTail == 0 && b.IsActive == true);
                                          if (result2 != null)
                                          {

                                                 result2.IsActive = false;
                                                 result2.ModifiedBy = "Admin";

                                                 result2.ModifiedDate = DateTime.Now;
                                                 sc.SaveChanges();
                                          }

                                          //var result21 = sc.RopeCropping.SingleOrDefault(b => b.SplicedId == mw.Id && b.RopeTail == 0 && b.IsActive == true);
                                          //if (result21 != null)
                                          //{

                                          //    result21.IsActive = false;
                                          //    result21.ModifiedBy = "Admin";

                                          //    result21.ModifiedDate = DateTime.Now;
                                          //    sc.SaveChanges();
                                          //}

                                          //sc.Entry(findcrs).State = EntityState.Deleted;
                                          //sc.SaveChanges();


                                          try
                                          {
                                                 var notiid = findcrs.NotificationId;
                                                 var notidelete = sc.Notifications.Where(x => x.Id == notiid).FirstOrDefault();
                                                 sc.Entry(notidelete).State = EntityState.Deleted;
                                                 sc.SaveChanges();
                                          }
                                          catch { }

                                          MessageBox.Show("Record deleted successfully ", "Delete CrossShifting", MessageBoxButton.OK, MessageBoxImage.Information);

                                          //.....Refresh DataGrid........

                                          //var lostdata = new ObservableCollection<RopeSplicingClass>(sc.RopeSplicing.ToList());
                                          //RopeSplicingViewModel cc = new RopeSplicingViewModel(lostdata);


                                          var lostdata = new ObservableCollection<RopeSplicingClass>(sc.RopeSplicing.ToList());
                                          RopeSplicingViewModel cc = new RopeSplicingViewModel(lostdata);

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


              private void ExportRopeSplicingMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportRopeSplicingMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void ExportRopeSplicingMethod1()
              {
                     try
                     {
                            SqlCommand cmd = new SqlCommand("ViewRopeSplicingExcel", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", 0);

                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "MooringLineSplicing_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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

                                          dt.TableName = "Line Splicing";
                                          var protectedsheet = wb.Worksheets.Add(dt);
                                          protectedsheet.Name = dt.TableName;
                                          var projection = protectedsheet.Protect("49WEB$TREET#");
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
       }
}
