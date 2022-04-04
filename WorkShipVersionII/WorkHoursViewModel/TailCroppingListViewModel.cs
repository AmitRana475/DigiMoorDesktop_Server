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
       public class TailCroppingListViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _ExportTailCropping;
              public ICommand HelpCommand { get; private set; }
              public TailCroppingListViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportTailCropping = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     viewCommand = new RelayCommand<RopeCroppingClass>(Viewropecropping);
                     editCommand = new RelayCommand<RopeCroppingClass>(Editropecrope);
                     deleteCommand = new RelayCommand<RopeCroppingClass>(Deleteropecrope);
                     GetropecropeList();


                     this._ExportTailCroppingCommands = new RelayCommand(() => _ExportTailCropping.RunWorkerAsync(), () => !_ExportTailCropping.IsBusy);
                     this._ExportTailCropping.DoWork += new DoWorkEventHandler(ExportTailCroppingMethod);


                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));


              }

              //ExportExcel
              private ICommand _ExportTailCroppingCommands;
              public ICommand ExportTailCroppingCommands
              {
                     get
                     {

                            return _ExportTailCroppingCommands;
                     }
              }
              private ICommand viewCommand;
              public ICommand ViewCommand
              {
                     get { return viewCommand; }
                     set { viewCommand = value; }
              }
              private void Viewropecropping(RopeCroppingClass mw)
              {
                     try
                     {

                            StaticHelper.ViewId = mw.Id;
                            StaticHelper.RopeTailId = 1;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewRopeCropping());


                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              public TailCroppingListViewModel(ObservableCollection<RopeCroppingClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     //LoadUserAccess.Clear();
                     GetropecropeList();

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
                            cmd.Parameters.AddWithValue("@RopeTail", 1);
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

                                          //var checkDepartment = sc.AssignRopetoWinch.Where(x => x.Id.Equals(mw.Id)).FirstOrDefault();
                                          //if (checkDepartment != null)
                                          //{
                                          //    MessageBox.Show("Sorry you can't delete this Department, as it is assigned to some crew members.", "Delete  Department", MessageBoxButton.OK, MessageBoxImage.Stop);
                                          //}
                                          //else
                                          //{

                                          sc.Entry(findcrs).State = EntityState.Deleted;
                                          sc.SaveChanges();

                                          MessageBox.Show("Record deleted successfully ", "Delete CrossShifting", MessageBoxButton.OK, MessageBoxImage.Information);

                                          var lostdata = new ObservableCollection<RopeCroppingClass>(sc.RopeCropping.ToList());
                                          TailCroppingListViewModel cc = new TailCroppingListViewModel(lostdata);
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

              private void ExportTailCroppingMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportTailCroppingMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void ExportTailCroppingMethod1()
              {
                     try
                     {
                            SqlCommand cmd = new SqlCommand("ViewTailCroppingExcel", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", 1);

                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "RopeTailCropping_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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

                                          dt.TableName = "Rope Tail Cropping";
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
