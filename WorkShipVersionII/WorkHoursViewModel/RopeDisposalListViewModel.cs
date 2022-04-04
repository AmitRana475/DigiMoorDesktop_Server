using ClosedXML.Excel;
using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class RopeDisposalListViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _ExportRopeDisposal;
              public ICommand HelpCommand { get; private set; }
              public RopeDisposalListViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportRopeDisposal = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     viewCommand = new RelayCommand<RopeDisposalClass>(ViewRopeDisposalMethod);
                     editCommand = new RelayCommand<RopeDisposalClass>(EditRopeDisposal);
                     deleteCommand = new RelayCommand<RopeDisposalClass>(DeleteRopeDisposal);

                     this._ExportRopeDisposalCommands = new RelayCommand(() => _ExportRopeDisposal.RunWorkerAsync(), () => !_ExportRopeDisposal.IsBusy);
                     this._ExportRopeDisposal.DoWork += new DoWorkEventHandler(ExportRopeDisposalMethod);


                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

                     //LoadMooringWinchList = GetMooringWinchList();           
                     GetRopeDisposalList();
              }

              public RopeDisposalListViewModel(ObservableCollection<RopeDisposalClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     //LoadUserAccess.Clear();


                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     GetRopeDisposalList();
                     OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

              }

              private void ViewRopeDisposalMethod(RopeDisposalClass mw)
              {
                     try
                     {
                            //ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch(mw);
                            StaticHelper.ViewId = mw.Id;
                            StaticHelper.RopeTailId = 0;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewRopeDisposal());

                            //ChildWindowManager.Instance.CloseChildWindow();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              //ExportExcel
              private ICommand _ExportRopeDisposalCommands;
              public ICommand ExportRopeDisposalCommands
              {
                     get
                     {

                            return _ExportRopeDisposalCommands;
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

              private static ObservableCollection<RopeDisposalClass> loadMooringWinchList = new ObservableCollection<RopeDisposalClass>();

              public ObservableCollection<RopeDisposalClass> LoadMooringWinchList
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


              public static ObservableCollection<RopeDisposalClass> loadUserAccess = new ObservableCollection<RopeDisposalClass>();
              public ObservableCollection<RopeDisposalClass> LoadUserAccess
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

              //private ObservableCollection<RopeDisposalClass> GetRopeDisposalList()
              //{

              //    try
              //    {
              //        ObservableCollection<RopeDisposalClass> RpDlist = new ObservableCollection<RopeDisposalClass>();

              //        var data = sc.RopeDisposals.ToList();
              //        foreach (var item in data)
              //        {
              //            RpDlist.Add(new RopeDisposalClass()
              //            {
              //                Id = item.Id,
              //                RopeId=item.RopeId,
              //                CertificateNo=sc.MooringWinchRope.Where(x=> x.Id==item.RopeId).Select(x=>x.CertificateNumber).SingleOrDefault(),
              //                DisposalPortName = item.DisposalPortName,
              //                ReceptionFacilityName = item.ReceptionFacilityName
              //            });


              //        }
              //        return RpDlist;
              //    }
              //    catch (Exception ex)
              //    {
              //        sc.ErrorLog(ex);
              //        return null;
              //    }

              //}

              public void GetRopeDisposalList()
              {

                     try
                     {
                            LoadUserAccess.Clear();

                            var ropelist = sc.MooringWinchRope.Where(x => x.RopeTail == 0).ToList();

                            var data = sc.RopeDisposals.Where(x => x.RopeTail == 0 && x.IsActive == true).ToList().OrderByDescending(x => x.DisposalDate);
                            foreach (var item in data)
                            {
                                   LoadUserAccess.Add(new RopeDisposalClass()
                                   {
                                          Id = item.Id,
                                          RopeId = item.RopeId,
                                          CertificateNo = ropelist.Where(x => x.Id == item.RopeId && x.RopeTail == 0 && x.DeleteStatus == false).Select(x => x.CertificateNumber).SingleOrDefault(),
                                          UniqueId = ropelist.Where(x => x.Id == item.RopeId && x.RopeTail == 0 && x.DeleteStatus == false).Select(x => x.UniqueID).SingleOrDefault(),
                                          DisposalPortName = item.DisposalPortName,
                                          ReceptionFacilityName = item.ReceptionFacilityName,
                                          DisposalDate=item.DisposalDate
                                   });


                            }

                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                            //return RpDlist;
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                            // return null;
                     }

              }

              private void EditRopeDisposal(RopeDisposalClass mw)
              {
                     try
                     {
                            AddRopeDisposalViewModel vm = new AddRopeDisposalViewModel(mw);
                            ChildWindowManager.Instance.ShowChildWindow(new AddRopeDisposalView() { DataContext = vm });
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void DeleteRopeDisposal(RopeDisposalClass mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   RopeDisposalClass findrank = sc.RopeDisposals.Where(x => x.Id == mw.Id).FirstOrDefault();
                                   if (findrank != null)
                                   {

                                          var result2 = sc.RopeDisposals.SingleOrDefault(b => b.Id == mw.Id && b.RopeTail == 0 && b.IsActive == true);
                                          if (result2 != null)
                                          {

                                                 result2.IsActive = false;
                                                 sc.SaveChanges();
                                          }

                                          //sc.Entry(findrank).State = EntityState.Deleted;
                                          //sc.SaveChanges();

                                          MessageBox.Show("Record deleted successfully ", "Delete Department", MessageBoxButton.OK, MessageBoxImage.Information);

                                          //.....Refresh DataGrid........

                                          var lostdata = new ObservableCollection<RopeDisposalClass>(sc.RopeDisposals.ToList());
                                          RopeDisposalListViewModel cc = new RopeDisposalListViewModel(lostdata);

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

              private void ExportRopeDisposalMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportRopeDisposalMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void ExportRopeDisposalMethod1()
              {
                     try
                     {
                            SqlDataAdapter adp = new SqlDataAdapter("Select b.UniqueId as [Unique Ident. No],b.CertificateNumber as [Certificate No.], a.DisposalPortName as [Disposal PortName],a.ReceptionFacilityName as [Reception Facility Name],a.DisposalDate as [Disposal Date] from  RopeDisposal a Inner Join MooringRopeDetail b On a.RopeId=b.Id where b.DeleteStatus = 0 and a.RopeTail = 0 and b.OutofServiceDate is not null and a.IsActive=1", sc.con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);

                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "MooringLine_Disposal_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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

                                          dt.TableName = "Line Disposal";
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
