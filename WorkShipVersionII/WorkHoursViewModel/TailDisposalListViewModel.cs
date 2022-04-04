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
       public class TailDisposalListViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _ExportTailDisposalList;
              public ICommand HelpCommand { get; private set; }
              public TailDisposalListViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportTailDisposalList = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     viewCommand = new RelayCommand<RopeDisposalClass>(ViewRopeDisposalMethod);
                     editCommand = new RelayCommand<RopeDisposalClass>(EditRopeDisposal);
                     deleteCommand = new RelayCommand<RopeDisposalClass>(DeleteRopeDisposal);

                     this._ExportTailDisposalListCommands = new RelayCommand(() => _ExportTailDisposalList.RunWorkerAsync(), () => !_ExportTailDisposalList.IsBusy);
                     this._ExportTailDisposalList.DoWork += new DoWorkEventHandler(ExportTailDisposalListMethod);

                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

                     //LoadMooringWinchList = GetMooringWinchList();           
                     GetRopeDisposalList();
              }

              public TailDisposalListViewModel(ObservableCollection<RopeDisposalClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     //LoadUserAccess.Clear();

                     GetRopeDisposalList();
                     OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

              }
              private void ViewRopeDisposalMethod(RopeDisposalClass mw)
              {
                     try
                     {
                            StaticHelper.ViewId = mw.Id;
                            StaticHelper.RopeTailId = 1;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewRopeDisposal());


                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              //ExportExcel
              private ICommand _ExportTailDisposalListCommands;
              public ICommand ExportTailDisposalListCommands
              {
                     get
                     {

                            return _ExportTailDisposalListCommands;
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

                            var ropelist = sc.MooringWinchRope.Where(x => x.RopeTail == 1).ToList();

                            var data = sc.RopeDisposals.Where(x => x.RopeTail == 1).ToList().OrderByDescending(x => x.DisposalDate); ;
                            foreach (var item in data)
                            {
                                   LoadUserAccess.Add(new RopeDisposalClass()
                                   {
                                          Id = item.Id,
                                          RopeId = item.RopeId,
                                          CertificateNo = sc.MooringWinchRope.Where(x => x.Id == item.RopeId && x.RopeTail == 1 && x.DeleteStatus == false).Select(x => x.CertificateNumber).SingleOrDefault(),
                                          UniqueId = sc.MooringWinchRope.Where(x => x.Id == item.RopeId && x.RopeTail == 1 && x.DeleteStatus == false).Select(x => x.UniqueID).SingleOrDefault(),
                                          DisposalPortName = item.DisposalPortName,
                                          ReceptionFacilityName = item.ReceptionFacilityName
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
                            AddTailDisposalViewModel vm = new AddTailDisposalViewModel(mw);
                            ChildWindowManager.Instance.ShowChildWindow(new AddTailDisposalView() { DataContext = vm });
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

                                          //var checkDepartment = sc.CrewDetails.Where(x => x.did.Equals(mw.Id)).FirstOrDefault();
                                          //if (checkDepartment != null)
                                          //{
                                          //    MessageBox.Show("Sorry you can't delete this Department, as it is assigned to some crew members.", "Delete  Department", MessageBoxButton.OK, MessageBoxImage.Stop);
                                          //}
                                          //else
                                          //{

                                          sc.Entry(findrank).State = EntityState.Deleted;
                                          sc.SaveChanges();

                                          MessageBox.Show("Record deleted successfully ", "Delete Department", MessageBoxButton.OK, MessageBoxImage.Information);

                                          //.....Refresh DataGrid........

                                          var lostdata = new ObservableCollection<RopeDisposalClass>(sc.RopeDisposals.ToList());
                                          TailDisposalListViewModel cc = new TailDisposalListViewModel(lostdata);





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

              private void ExportTailDisposalListMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportTailDisposalListMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void ExportTailDisposalListMethod1()
              {
                     try
                     {
                            string qry = "Select a.UniqueID as [Unique Ident. No],a.CertificateNumber as[Certificate No.],b.DisposalPortName as[Disposal PortName],b.ReceptionFacilityName as [Reception facility Name],b.DisposalDate as [Disposal Date] from MooringRopeDetail a Inner Join RopeDisposal b on a.Id=b.RopeId where a.DeleteStatus = 0 and a.RopeTail = 1 and a.OutofServiceDate is not null and b.IsActive=1";
                            SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "RopeTailDisposal_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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

                                          dt.TableName = "RopeTail Disposal";
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
