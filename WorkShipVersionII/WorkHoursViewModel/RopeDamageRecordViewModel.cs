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
       public class RopeDamageRecordViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _ExportRopeDamageRecord;
              public ICommand HelpCommand { get; private set; }
              public RopeDamageRecordViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportRopeDamageRecord = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     viewCommand = new RelayCommand<RopeDamageRecordClass>(Viewropedamage);
                     editCommand = new RelayCommand<RopeDamageRecordClass>(Editropedamage);
                     deleteCommand = new RelayCommand<RopeDamageRecordClass>(Deleteropedamage);

                     this._ExportRopeDamageRecordCommands = new RelayCommand(() => _ExportRopeDamageRecord.RunWorkerAsync(), () => !_ExportRopeDamageRecord.IsBusy);
                     this._ExportRopeDamageRecord.DoWork += new DoWorkEventHandler(ExportRopeDamageRecordMethod);

                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     GetRopeDamageList();

              }
              public RopeDamageRecordViewModel(ObservableCollection<RopeDamageRecordClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     //LoadUserAccess.Clear();

                     //var lostdata = new ObservableCollection<MOperationBirthDetail>(sc.MOperationBirthDetailTbl.ToList());
                     //MooringOPRListingViewModel cc = new MooringOPRListingViewModel(lostdata);

                     GetRopeDamageList();
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

              }
              private void Viewropedamage(RopeDamageRecordClass mw)
              {
                     try
                     {
                            //ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch(mw);
                            StaticHelper.ViewId = mw.Id;
                            StaticHelper.RopeTailId = 0;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewRopeDamage());

                            //ChildWindowManager.Instance.CloseChildWindow();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              //ExportExcel
              private ICommand _ExportRopeDamageRecordCommands;
              public ICommand ExportRopeDamageRecordCommands
              {
                     get
                     {

                            return _ExportRopeDamageRecordCommands;
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

              private static ObservableCollection<RopeDamageRecordClass> loadRopedamageList = new ObservableCollection<RopeDamageRecordClass>();

              public ObservableCollection<RopeDamageRecordClass> LoadRopeDamageList
              {
                     get
                     {
                            return loadRopedamageList;
                     }
                     set
                     {
                            loadRopedamageList = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadRopeDamageList"));

                     }
              }


              public static ObservableCollection<RopeDamageRecordClass> loadUserAccess = new ObservableCollection<RopeDamageRecordClass>();
              public ObservableCollection<RopeDamageRecordClass> LoadUserAccess
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


              public void GetRopeDamageList()
              {
                     try
                     {
                            LoadUserAccess.Clear();
                            SqlCommand cmd = new SqlCommand("GetDamageRope", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", 0);

                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Rows)
                            {
                                   LoadUserAccess.Add(new RopeDamageRecordClass()
                                   {
                                          Id = (int)row["Id"],
                                          RopeId = (int)row["RopeId"],
                                          AssignedNumber = (row["AssignedNumber"] == DBNull.Value) ? "Not Assigned" : row["AssignedNumber"].ToString(),
                                          CertificateNumber = (row["CertificateNumber"] == DBNull.Value) ? "Not Assigned" : row["CertificateNumber"].ToString(),
                                          UniqueId = (row["UniqueId"] == DBNull.Value) ? "Not Assigned" : row["UniqueId"].ToString(),
                                          AssignedLocation = (row["AssignedLocation"] == DBNull.Value) ? "Not Assigned" : row["AssignedLocation"].ToString(),
                                          DamageDate = Convert.ToDateTime((row["DamageDate"] == DBNull.Value) ? row["CreatedDate"] : row["DamageDate"]),
                                          //DamageObserved = (string)row["DamageObserved"],
                                          DamageObserved = (row["DamageObserved"] == DBNull.Value) ? "Inspection" : row["DamageObserved"].ToString(),

                                          IncidentReport = (row["IncidentReport"] == DBNull.Value) ? string.Empty : row["IncidentReport"].ToString(),
                                          // MooringOperation = (string)row["MooringOperation"],
                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }

              }

              //private ObservableCollection<RopeDamageRecordClass> GetRopeDamageList()
              //{
              //    try
              //    {
              //        ObservableCollection<RopeDamageRecordClass> rpdamagelist = new ObservableCollection<RopeDamageRecordClass>();

              //        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
              //        SqlCommand cmd = new SqlCommand("GetDamageRope", sc.con);
              //        cmd.CommandType = CommandType.StoredProcedure;


              //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
              //        DataTable ds = new DataTable();
              //        adp.Fill(ds);
              //        foreach (DataRow row in ds.Rows)
              //        {
              //            rpdamagelist.Add(new RopeDamageRecordClass()
              //            {
              //                Id = (int)row["Id"],
              //                AssignedNumber = (string)row["AssignedNumber"],
              //                CertificateNumber = (string)row["CertificateNumber"],
              //                AssignedLocation = (string)row["AssignedLocation"],
              //                DamageObserved = (string)row["DamageObserved"],
              //                IncidentReport = (string)row["IncidentReport"],
              //               // MooringOperation = (string)row["MooringOperation"],
              //            });
              //        }

              //        return rpdamagelist;
              //    }
              //    catch (Exception ex)
              //    {
              //        sc.ErrorLog(ex);
              //        return null;
              //    }

              //}




              private void Editropedamage(RopeDamageRecordClass mw)
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

              private void Deleteropedamage(RopeDamageRecordClass mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   RopeDamageRecordClass findcrs = sc.RopeDamage.Where(x => x.Id == mw.Id).FirstOrDefault();
                                   if (findcrs != null)
                                   {
                                          var result2 = sc.RopeDamage.SingleOrDefault(b => b.Id == mw.Id && b.RopeTail == 0 && b.IsActive == true);
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


                                          MessageBox.Show("Record deleted successfully ", "Delete RopeDamage", MessageBoxButton.OK, MessageBoxImage.Information);

                                          var lostdata = new ObservableCollection<RopeDamageRecordClass>(sc.RopeDamage.ToList());
                                          RopeDamageRecordViewModel cc = new RopeDamageRecordViewModel(lostdata);
                                   }
                                   else
                                   {
                                          MessageBox.Show("Record is not found ", "Delete RopeDamage ", MessageBoxButton.OK, MessageBoxImage.Information);
                                   }

                            }

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              //ExportRopeDamageRecordMethod
              private void ExportRopeDamageRecordMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportRopeDamageRecordMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void ExportRopeDamageRecordMethod1()
              {
                     try
                     {
                            SqlCommand cmd = new SqlCommand("ViewDamageRopeTailExcel", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", 0);

                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "MooringLineDamage_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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

                                          dt.TableName = "Line Damage";
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
