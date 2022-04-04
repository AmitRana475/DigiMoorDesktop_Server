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
       public class RopeCroppingListViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _ExportRopeCropping;
              public ICommand HelpCommand { get; private set; }
              public RopeCroppingListViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportRopeCropping = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     viewCommand = new RelayCommand<RopeCroppingClass>(Viewropecropping);
                     editCommand = new RelayCommand<RopeCroppingClass>(Editropecrope);
                     deleteCommand = new RelayCommand<RopeCroppingClass>(Deleteropecrope);

                     this._ExportRopeCroppingCommands = new RelayCommand(() => _ExportRopeCropping.RunWorkerAsync(), () => !_ExportRopeCropping.IsBusy);
                     this._ExportRopeCropping.DoWork += new DoWorkEventHandler(ExportRopeCroppingMethod);

                     GetropecropeList();
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

              }
              public RopeCroppingListViewModel(ObservableCollection<RopeCroppingClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     //LoadUserAccess.Clear();
                     GetropecropeList();
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
              }

              private void Viewropecropping(RopeCroppingClass mw)
              {
                     try
                     {
                            //ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch(mw);
                            StaticHelper.ViewId = mw.Id;
                            StaticHelper.RopeTailId = 0;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewRopeCropping());

                            //ChildWindowManager.Instance.CloseChildWindow();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              //ExportExcel
              private ICommand _ExportRopeCroppingCommands;
              public ICommand ExportRopeCroppingCommands
              {
                     get
                     {

                            return _ExportRopeCroppingCommands;
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

              private static ObservableCollection<RopeCroppingClass> loadropecropList = new ObservableCollection<RopeCroppingClass>();

              public ObservableCollection<RopeCroppingClass> LoadRopeCroppingList
              {
                     get
                     {
                            return loadropecropList;
                     }
                     set
                     {
                            loadropecropList = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadRopeCroppingList"));

                     }
              }


              public static ObservableCollection<RopeCroppingClass> loadUserAccess = new ObservableCollection<RopeCroppingClass>();
              public ObservableCollection<RopeCroppingClass> LoadUserAccess
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

              //private ObservableCollection<RopeCroppingClass> GetropecropeList()
              //{

              //    try
              //    {
              //        ObservableCollection<RopeCroppingClass> cropelist = new ObservableCollection<RopeCroppingClass>();

              //        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
              //        SqlCommand cmd = new SqlCommand("GetRopeCropping", sc.con);
              //        cmd.CommandType = CommandType.StoredProcedure;


              //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
              //        DataTable ds = new DataTable();
              //        adp.Fill(ds);
              //        foreach (DataRow row in ds.Rows)
              //        {
              //            cropelist.Add(new RopeCroppingClass()
              //            {
              //                Id = (int)row["Id"],
              //                AssignedNumber = (string)row["AssignedNumber"],
              //                CertificateNumber = (string)row["CertificateNumber"],
              //                AssignedLocation = (string)row["AssignedLocation"],
              //                CroppedDate = (DateTime)row["CroppedDate"],
              //                CroppedOutboardEnd = (string)row["CroppedOutboardEnd"],
              //                LengthofCroppedRope = (int)row["LengthofCroppedRope"],
              //                ReasonofCropping = (string)row["reasonofcropping"],
              //            });
              //        }

              //        return cropelist;
              //    }
              //    catch (Exception ex)
              //    {
              //        sc.ErrorLog(ex);
              //        return null;
              //    }

              //}

              public void GetropecropeList()
              {
                     try
                     {
                            LoadUserAccess.Clear();
                            SqlCommand cmd = new SqlCommand("GetRopeCropping", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", 0);
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Rows)
                            {
                                   LoadUserAccess.Add(new RopeCroppingClass()
                                   {
                                          Id = (int)row["Id"],
                                          AssignedNumber = (row["AssignedNumber"] == DBNull.Value) ? "Not Assigned" : row["AssignedNumber"].ToString(),
                                          CertificateNumber = (row["CertificateNumber"] == DBNull.Value) ? "Not Assigned" : row["CertificateNumber"].ToString(),
                                          AssignedLocation = (row["AssignedLocation"] == DBNull.Value) ? "Not Assigned" : row["AssignedLocation"].ToString(),


                                          UniqueId = (row["UniqueId"] == DBNull.Value) ? "Not Assigned" : row["UniqueId"].ToString(),

                                          //CroppedDate = (DateTime)row["CroppedDate"],
                                          CroppedDate1 = ((string)row["CroppedDate"]),
                                          //CroppedDate1 = ((DateTime)row["CroppedDate"]).ToString("yyyy-MM-dd"),
                                          //CroppedOutboardEnd = (string)row["CroppedOutboardEnd"],
                                          CroppedOutboardEnd = (row["CroppedOutboardEnd"] == DBNull.Value) ? "" : row["CroppedOutboardEnd"].ToString(),
                                          LengthofCroppedRope = (decimal)row["LengthofCroppedRope"],
                                          //ReasonofCropping = (string)row["reasonofcropping"],
                                          ReasonofCropping = (row["reasonofcropping"] == DBNull.Value) ? "" : row["reasonofcropping"].ToString(),
                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void Editropecrope(RopeCroppingClass mw)
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

              private void Deleteropecrope(RopeCroppingClass mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   RopeCroppingClass findcrs = sc.RopeCropping.Where(x => x.Id == mw.Id).FirstOrDefault();
                                   if (findcrs != null)
                                   {

                                          var result2 = sc.RopeCropping.SingleOrDefault(b => b.Id == mw.Id && b.RopeTail == 0 && b.IsActive == true);
                                          if (result2 != null)
                                          {

                                                 result2.IsActive = false;
                                                 result2.ModifiedBy = "Admin";

                                                 result2.ModifiedDate = DateTime.Now;
                                                 sc.SaveChanges();
                                          }


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


                                          MessageBox.Show("Record deleted successfully ", "Delete RopeCropping", MessageBoxButton.OK, MessageBoxImage.Information);

                                          var lostdata = new ObservableCollection<RopeCroppingClass>(sc.RopeCropping.ToList());
                                          RopeCroppingListViewModel cc = new RopeCroppingListViewModel(lostdata);
                                   }
                                   else
                                   {
                                          MessageBox.Show("Record is not found ", "Delete RopeCropping ", MessageBoxButton.OK, MessageBoxImage.Information);
                                   }

                            }

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }


              //ExportRopeSplicingMethod
              private void ExportRopeCroppingMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportRopeCroppingMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void ExportRopeCroppingMethod1()
              {
                     try
                     {
                            SqlCommand cmd = new SqlCommand("ViewRopeCroppingExcel", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", 0);

                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "MooringLineCropping_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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

                                          dt.TableName = "Line Cropping";
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
