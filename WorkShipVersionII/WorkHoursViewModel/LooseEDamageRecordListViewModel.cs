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

       public class LooseEDamageRecordListViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _ExportLooseEDamagerRecord;
              public ICommand HelpCommand { get; private set; }
              public LooseEDamageRecordListViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportLooseEDamagerRecord = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     viewCommand = new RelayCommand<LooseEDamageRecordClass>(ViewropeSplicing);
                     viewCommand1 = new RelayCommand<LooseEDamageRecordClass>(ViewLooseEDiscard);
                     editCommand = new RelayCommand<LooseEDamageRecordClass>(Editropedamage);
                     deleteCommand = new RelayCommand<LooseEDamageRecordClass>(Deleteropedamage);
                     this._ExportLooseEDamagerRecordCommands = new RelayCommand(() => _ExportLooseEDamagerRecord.RunWorkerAsync(), () => !_ExportLooseEDamagerRecord.IsBusy);
                     this._ExportLooseEDamagerRecord.DoWork += new DoWorkEventHandler(ExportLooseEDamagerRecordMethod);



                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

                     GetRopeDamageList();

              }
              public LooseEDamageRecordListViewModel(ObservableCollection<LooseEDamageRecordClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     GetRopeDamageList();

              }

              private ICommand _ExportLooseEDamagerRecordCommands;
              public ICommand ExportLooseEDamagerRecordCommands
              {
                     get
                     {
                            return _ExportLooseEDamagerRecordCommands;
                     }
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

              private void ViewropeSplicing(LooseEDamageRecordClass mw)
              {
                     try
                     {
                            StaticHelper.ViewId = mw.Id;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewLooseEDamage());


                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void ViewLooseEDiscard(LooseEDamageRecordClass mw)
              {
                     try
                     {
                            StaticHelper.ViewId = mw.Id;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewLooseEDiscard());


                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private static ObservableCollection<LooseEDamageRecordClass> loadRopedamageList = new ObservableCollection<LooseEDamageRecordClass>();

              public ObservableCollection<LooseEDamageRecordClass> LoadRopeDamageList
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


              public static ObservableCollection<LooseEDamageRecordClass> loadUserAccess = new ObservableCollection<LooseEDamageRecordClass>();
              public ObservableCollection<LooseEDamageRecordClass> LoadUserAccess
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
                            //ObservableCollection<LooseEDamageRecordClass> rpdamagelist = new ObservableCollection<LooseEDamageRecordClass>();

                            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                            SqlCommand cmd = new SqlCommand("GetDamageLooseE", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;


                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Rows)
                            {
                                   LoadUserAccess.Add(new LooseEDamageRecordClass()
                                   {
                                          Id = (int)row["Id"],
                                          //AssignedNumber = (string)row["AssignedNumber"],
                                          //CertificateNumber = (string)row["CertificateNumber"],

                                          CertificateNumber = (row["CertificateNumber"] == DBNull.Value) ? string.Empty : row["CertificateNumber"].ToString(),

                                          DamageDate = Convert.ToDateTime((row["DamageDate"] == DBNull.Value) ? string.Empty : row["DamageDate"]),
                                          //AssignedLocation = (string)row["AssignedLocation"],
                                          // DamageObserved = (string)row["DamageObserved"],
                                          DamageObserved = (row["DamageObserved"] == DBNull.Value) ? string.Empty : row["DamageObserved"].ToString(),
                                          DamageReason = (row["DamageReason"] == DBNull.Value) ? string.Empty : row["DamageReason"].ToString(),
                                          LooseEtype = (string)row["LooseEtype"],

                                          // mo = (row["MooringOperation"] == DBNull.Value) ? string.Empty : row["MooringOperation"].ToString(),
                                          //MooringOperation = (string)row["MooringOperation"],
                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                            //return rpdamagelist;
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                            //return null;
                     }

              }

              private void Editropedamage(LooseEDamageRecordClass mw)
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

              private void Deleteropedamage(LooseEDamageRecordClass mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   LooseEDamageRecordClass findcrs = sc.LooseEDamageR.Where(x => x.Id == mw.Id).FirstOrDefault();
                                   if (findcrs != null)
                                   {
                                          sc.Entry(findcrs).State = EntityState.Deleted;
                                          sc.SaveChanges();

                                          try
                                          {
                                                 var notiid = findcrs.NotificationId;
                                                 var notidelete = sc.Notifications.Where(x => x.Id == notiid).FirstOrDefault();
                                                 sc.Entry(notidelete).State = EntityState.Deleted;
                                                 sc.SaveChanges();
                                          }
                                          catch { }


                                          MessageBox.Show("Record deleted successfully ", "Delete RopeDamage", MessageBoxButton.OK, MessageBoxImage.Information);
                                          GetRopeDamageList();
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
              private void ExportLooseEDamagerRecordMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportLooseEDamagerRecordMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void ExportLooseEDamagerRecordMethod1()
              {
                     try
                     {
                            string qry = "Select LooseEquipmentType as [Loose Type],CertificateNumber as [Certificate Number] ,DamageObserved as [Damaged Observed],DamageReason as [Damage Reason],DamageDate as [Damage Date],IncidentReport as [Incident Report],(select (d.portname + ' - '+ convert(varchar, d.fastdatetime, 106)) as Operation from MOperationBirthDetail d where d.IsActive=1 and d.OPId = a.mopid) as Mooringoperation from LooseEDamageRecord a Inner Join LooseEType b on a.LooseETypeId=b.Id";
                            SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);

                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "LooseEDamageRecord_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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
                                          dt.TableName = "LooseEDamage Record";
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
