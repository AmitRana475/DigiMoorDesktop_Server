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
       public class RopeEndtoEndViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _ExportRopeEndToEnd;
              public ICommand HelpCommand { get; private set; }
              public RopeEndtoEndViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportRopeEndToEnd = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     viewCommand = new RelayCommand<RopeEndtoEnd2Class>(Viewendtoend);
                     editCommand = new RelayCommand<RopeEndtoEnd2Class>(EditMooringWinch);
                     deleteCommand = new RelayCommand<RopeEndtoEnd2Class>(DeleteMooringWinch);

                     this._ExportRopeEndToEndCommands = new RelayCommand(() => _ExportRopeEndToEnd.RunWorkerAsync(), () => !_ExportRopeEndToEnd.IsBusy);
                     this._ExportRopeEndToEnd.DoWork += new DoWorkEventHandler(ExportRopeEndToEndMethod);
                     // StaticHelper.HelpFor = @"LMPR\rope\4.2.4 ROPE END TO END.htm";
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     // LoadMooringWinchList = GetEndtoEndList();

                     GetEndtoEndList();


              }

              public RopeEndtoEndViewModel(ObservableCollection<RopeEndtoEnd2Class> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     //StaticHelper.HelpFor = @"LMPR\rope\4.2.4 ROPE END TO END.htm";
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     // LoadUserAccess.Clear();

                     GetEndtoEndList();

              }

              //ExportExcel
              private ICommand _ExportRopeEndToEndCommands;
              public ICommand ExportRopeEndToEndCommands
              {
                     get
                     {

                            return _ExportRopeEndToEndCommands;
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

              private static ObservableCollection<RopeEndtoEnd2Class> loadMooringWinchList = new ObservableCollection<RopeEndtoEnd2Class>();

              public ObservableCollection<RopeEndtoEnd2Class> LoadMooringWinchList
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

              private void Viewendtoend(RopeEndtoEnd2Class mw)
              {
                     try
                     {
                            //ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch(mw);
                            StaticHelper.ViewId = mw.Id;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewrRopeEndtoEnd());

                            //ChildWindowManager.Instance.CloseChildWindow();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              public static ObservableCollection<RopeEndtoEnd2Class> loadUserAccess = new ObservableCollection<RopeEndtoEnd2Class>();
              public ObservableCollection<RopeEndtoEnd2Class> LoadUserAccess
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

              //private ObservableCollection<RopeEndtoEnd2Class> GetEndtoEndList()
              //{

              //    try
              //    {
              //        ObservableCollection<RopeEndtoEnd2Class> moringlist = new ObservableCollection<RopeEndtoEnd2Class>();

              //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
              //        SqlCommand cmd = new SqlCommand("GetRopeEndtoEnd", con);
              //          cmd.CommandType = CommandType.StoredProcedure;


              //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
              //        DataTable ds = new DataTable();
              //        adp.Fill(ds);
              //        // var data = sc.AssignRopetoWinch.ToList();          
              //        foreach (DataRow row in ds.Rows)
              //        {
              //            moringlist.Add(new RopeEndtoEnd2Class()
              //            {
              //                Id = (int)row["Id"],
              //                AssignedNumber = (string)row["AssignedNumber"],
              //                CertificateNumber = (string)row["CertificateNumber"],
              //                AssignedLocation = (string)row["Location"],
              //                //EndtoEndDoneDate = (DateTime)row["EndtoEndDoneDate"].ToString("d MMM, yyyy"),
              //                EndtoEndDoneDate1 = ((DateTime)row["EndtoEndDoneDate"]).ToString("d MMM, yyyy"),
              //                CurrentOutboadEndinUse1 = (string)row["CurrentOutboadEndinUse1"],
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

              public void GetEndtoEndList()
              {

                     try
                     {
                            LoadUserAccess.Clear();
                            // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                            SqlCommand cmd = new SqlCommand("GetRopeEndtoEnd", sc.con);
                            cmd.Parameters.AddWithValue("@Action", "Listing");
                            cmd.Parameters.AddWithValue("@Id", 0);

                            cmd.CommandType = CommandType.StoredProcedure;


                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);
                            // var data = sc.AssignRopetoWinch.ToList();          
                            foreach (DataRow row in ds.Rows)
                            {
                                   LoadUserAccess.Add(new RopeEndtoEnd2Class()
                                   {
                                          Id = (int)row["Id"],
                                          AssignedNumber = (string)row["AssignedNumber"],
                                          CertificateNumber = (string)row["CertificateNumber"],
                                          AssignedLocation = (string)row["Location"],

                                          UniqueId = (row["UniqueId"] == DBNull.Value) ? string.Empty : row["UniqueId"].ToString(),
                                          CurrentOutboadEndinUse1 = (row["CurrentOutboadEndinUse1"] == DBNull.Value) ? string.Empty : row["CurrentOutboadEndinUse1"].ToString(),
                                          //EndtoEndDoneDate = (DateTime)row["EndtoEndDoneDate"].ToString("d MMM, yyyy"),
                                          //EndtoEndDoneDate1 = ((DateTime)row["EndtoEndDoneDate"]).ToString("yyyy-MM-dd"),
                                          EndtoEndDoneDate1 = ((string)row["EndtoEndDoneDate"]),
                                          //CurrentOutboadEndinUse1 = (string)row["CurrentOutboadEndinUse1"],
                                   });
                            }

                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                            /// return null;
                     }

              }

              private void ExportRopeEndToEndMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportRopeEndToEndMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }

              }
              private void ExportRopeEndToEndMethod1()
              {
                     try
                     {
                            SqlDataAdapter adp = new SqlDataAdapter("GetRopeEndtoEndExcel", sc.con);
                            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp.SelectCommand.Parameters.AddWithValue("@Action", "Listing");
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "MooringLineEndToEnd_Export_" + DateTime.Now.ToString("dd-MMM-yyyy");
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
                                          dt.TableName = "Line EndtoEnd";
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

              //private static DBManager dm;
              //public DataSet GetHolidays()
              //{
              //    dm.Open();
              //    var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetHolidays");
              //    dm.Close();

              //    return data;
              //}
              private void EditMooringWinch(RopeEndtoEnd2Class mw)
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

              private void DeleteMooringWinch(RopeEndtoEnd2Class mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   RopeEndtoEnd2Class findropeend = sc.RopeEndtoEnd2.Where(x => x.Id == mw.Id).FirstOrDefault();
                                   if (findropeend != null)
                                   {

                                          var result2 = sc.RopeEndtoEnd2.SingleOrDefault(b => b.Id == mw.Id && b.IsActive == true);
                                          if (result2 != null)
                                          {

                                                 result2.IsActive = false;
                                                 sc.SaveChanges();
                                          }

                                          //sc.Entry(findropeend).State = EntityState.Deleted;
                                          //sc.SaveChanges();

                                          MessageBox.Show("Record deleted successfully ", "Delete RopeEndtoEnd", MessageBoxButton.OK, MessageBoxImage.Information);

                                          //.....Refresh DataGrid........


                                          var lostdata = new ObservableCollection<RopeEndtoEnd2Class>(sc.RopeEndtoEnd2.ToList());
                                          RopeEndtoEndViewModel cc = new RopeEndtoEndViewModel(lostdata);
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




              new public event PropertyChangedEventHandler PropertyChanged;
              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }

       }
}
