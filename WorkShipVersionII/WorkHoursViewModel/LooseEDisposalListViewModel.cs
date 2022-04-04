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
       public class LooseEDisposalListViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _ExportLooseEDisposalList;
              public ICommand HelpCommand { get; private set; }
              public LooseEDisposalListViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportLooseEDisposalList = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     viewCommand = new RelayCommand<LooseEDisposalClass>(ViewRopeDisposalMethod);
                     editCommand = new RelayCommand<LooseEDisposalClass>(EditLooseEDisposal);
                     deleteCommand = new RelayCommand<LooseEDisposalClass>(DeleteLooseEDisposal);

                     this._ExportLooseEDisposalListCommands = new RelayCommand(() => _ExportLooseEDisposalList.RunWorkerAsync(), () => !_ExportLooseEDisposalList.IsBusy);
                     this._ExportLooseEDisposalList.DoWork += new DoWorkEventHandler(ExportLooseEDisposalListMethod);


                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

                     //LoadMooringWinchList = GetMooringWinchList();           
                     GetLooseEDisposalList();
              }
              private void ViewRopeDisposalMethod(LooseEDisposalClass mw)
              {
                     try
                     {
                            //ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch(mw);
                            StaticHelper.ViewId = mw.Id;

                            ChildWindowManager.Instance.ShowChildWindow(new ViewLooseEDisposal());

                            //ChildWindowManager.Instance.CloseChildWindow();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private ICommand _ExportLooseEDisposalListCommands;
              public ICommand ExportLooseEDisposalListCommands
              {
                     get
                     {
                            return _ExportLooseEDisposalListCommands;
                     }
              }
              private ICommand viewCommand;
              public ICommand ViewCommand
              {
                     get { return viewCommand; }
                     set { viewCommand = value; }
              }
              public LooseEDisposalListViewModel(ObservableCollection<LooseEDisposalClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     //LoadUserAccess.Clear();

                     GetLooseEDisposalList();
                     OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

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

              private static ObservableCollection<LooseEDisposalClass> loadMooringWinchList = new ObservableCollection<LooseEDisposalClass>();

              public ObservableCollection<LooseEDisposalClass> LoadMooringWinchList
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


              public static ObservableCollection<LooseEDisposalClass> loadUserAccess = new ObservableCollection<LooseEDisposalClass>();
              public ObservableCollection<LooseEDisposalClass> LoadUserAccess
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

              public void GetLooseEDisposalList()
              {

                     try
                     {
                            // ObservableCollection<LooseEDisposalClass> RpDlist = new ObservableCollection<LooseEDisposalClass>();
                            LoadUserAccess.Clear();
                            var data = sc.LooseEDisposals.ToList();
                            foreach (var item in data)
                            {
                                   LoadUserAccess.Add(new LooseEDisposalClass()
                                   {
                                          Id = item.Id,
                                          LooseETypeId = item.LooseETypeId,
                                          LooseECertiNo = item.LooseECertiNo,
                                          //CertificateNo = sc.MooringWinchRope.Where(x => x.Id == item.LooseETypeId).Select(x => x.CertificateNumber).SingleOrDefault(),
                                          DisposalPortName = item.DisposalPortName,
                                          ReceptionFacilityName = item.ReceptionFacilityName
                                   });


                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }

              }

              private void EditLooseEDisposal(LooseEDisposalClass mw)
              {
                     try
                     {
                            AddLooseEDisposalViewModel vm = new AddLooseEDisposalViewModel(mw);
                            ChildWindowManager.Instance.ShowChildWindow(new AddLooseEDisposalView() { DataContext = vm });
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void DeleteLooseEDisposal(LooseEDisposalClass mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   LooseEDisposalClass findrank = sc.LooseEDisposals.Where(x => x.Id == mw.Id).FirstOrDefault();
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

                                          var lostdata = new ObservableCollection<LooseEDisposalClass>(sc.LooseEDisposals.ToList());
                                          LooseEDisposalListViewModel cc = new LooseEDisposalListViewModel(lostdata);



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
              private void ExportLooseEDisposalListMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportLooseEDisposalListMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void ExportLooseEDisposalListMethod1()
              {
                     try
                     {
                            string qry = "Select b.LooseEquipmentType as [Loose Eq. Type], a.LooseECertiNo as [Loose Eq. Cert. No],a.DiscardedDate as [Discarded Date],a.DisposalPortName as [Disposal Port name],a.ReceptionFacilityName as [ Disposal Reception Facility Name],a.DisposalDate as [Disposal Date] from LooseEDisposal a Inner Join LooseEType b on a.LooseETypeId = b.Id";
                            SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);

                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "LooseEquipmentDisposal_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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
                                          dt.TableName = "Loose Equipment Disposal";
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