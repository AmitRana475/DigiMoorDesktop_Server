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
       public class RopeDiscardListViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private BackgroundWorker _ExportRopeDiscard;
              public ICommand HelpCommand { get; private set; }
              public RopeDiscardListViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            _ExportRopeDiscard = new BackgroundWorker();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     viewCommand = new RelayCommand<RopeDiscardRecordClass>(Viewropediscard);
                     editCommand = new RelayCommand<RopeDiscardRecordClass>(EditRopeDiscard);
                     deleteCommand = new RelayCommand<RopeDiscardRecordClass>(DeleteRopeDiscard);

                     this._ExportRopeDiscardCommands = new RelayCommand(() => _ExportRopeDiscard.RunWorkerAsync(), () => !_ExportRopeDiscard.IsBusy);
                     this._ExportRopeDiscard.DoWork += new DoWorkEventHandler(ExportRopeDiscardMethod);


                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     GetRopeDiscardList();


              }
              private void Viewropediscard(RopeDiscardRecordClass mw)
              {
                     try
                     {
                            //ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch(mw);
                            StaticHelper.ViewId = mw.RopeId;
                            StaticHelper.RopeTailId = 0;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewRopeDiscard());

                            //ChildWindowManager.Instance.CloseChildWindow();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void EditRopeDiscard(RopeDiscardRecordClass mw)
              {
                     try
                     {
                            RopeDiscardRecordViewModel vm = new RopeDiscardRecordViewModel(mw);

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              //ExportExcel
              private ICommand _ExportRopeDiscardCommands;
              public ICommand ExportRopeDiscardCommands
              {
                     get
                     {

                            return _ExportRopeDiscardCommands;
                     }
              }
              private ICommand viewCommand;
              public ICommand ViewCommand
              {
                     get { return viewCommand; }
                     set { viewCommand = value; }
              }

              private void Below21RopesAtDeleteTime()
              {
                     try
                     {   // minimum required of 21 Ropes ---------------------------------
                            SqlDataAdapter adp = new SqlDataAdapter("select COUNT(*) from MooringRopeDetail where OutofServiceDate is null and RopeTail=0 and DeleteStatus=0", sc.con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            if (dt.Rows.Count >= 0)
                            {
                                   int cnt = Convert.ToInt32(dt.Rows[0][0]);
                                   if (cnt < StaticHelper.MinimumRope)
                                   {
                                          var notification = "Active lines below minimum required of " + StaticHelper.MinimumRope + " Lines including spare";

                                          SqlDataAdapter act = new SqlDataAdapter("select COUNT(*) from Notifications where Notification='Active lines below minimum required of " + StaticHelper.MinimumRope + " Lines including spare'", sc.con);
                                          DataTable dd = new DataTable();
                                          act.Fill(dd);

                                          int cntnoti = Convert.ToInt32(dd.Rows[0][0]);
                                          if (cntnoti == 0)
                                          {
                                                 NotificationsClass noti = new NotificationsClass();
                                                 noti.Acknowledge = false;
                                                 noti.AckRecord = "Not yet acknowledged";
                                                 // noti.AckRecord = "Please insert minimum of {21} active ropes";
                                                 noti.Notification = notification;
                                                 noti.RopeId = 0;
                                                 noti.IsActive = true;
                                                 noti.NotificationType = 1;
                                                 //noti.NotificationDueDate = notidueMonth;
                                                 noti.CreatedDate = DateTime.Now;
                                                 noti.CreatedBy = "Admin";
                                                 noti.NotificationAlertType = (int)NotificationAlertType.Minimum21RopeCount;
                                                 sc.Notifications.Add(noti);
                                                 sc.SaveChanges();

                                                 StaticHelper.AlarmFunction(1, true);
                                          }

                                          act.Dispose();
                                          dd.Dispose();
                                   }
                                   else
                                   {
                                          // delete Above Notification
                                          SqlDataAdapter act = new SqlDataAdapter("delete from Notifications where NotificationAlertType = 17", sc.con);
                                          DataTable dd = new DataTable();
                                          act.Fill(dd);
                                          act.Dispose();
                                          dd.Dispose();
                                   }
                            }

                            dt.Dispose();
                            adp.Dispose();


                     }
                     catch { }
              }

              private void DeleteRopeDiscard(RopeDiscardRecordClass mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   MooringWinchRopeClass findrank = sc.MooringWinchRope.Where(x => x.Id == mw.RopeId && x.DeleteStatus == false).FirstOrDefault();
                                   if (findrank != null)
                                   {
                                          SqlDataAdapter adp = new SqlDataAdapter("select * from RopeDisposal where IsActive=1 and RopeId=" + mw.RopeId + "", sc.con);
                                          DataTable dt = new DataTable();
                                          adp.Fill(dt);
                                          if (dt.Rows.Count > 0)
                                          {
                                                 MessageBox.Show("You can not delete this record ! Firstly remove from Line Disposal. ", "Mooring Line", MessageBoxButton.OK, MessageBoxImage.Information);
                                          }
                                          else
                                          {
                           // SqlDataAdapter adp15 = new SqlDataAdapter("update mooringropedetail set outofservicedate= null,ModifiedDate='" + DateTime.Now + "' where id =" + mw.RopeId + "", sc.con);
                            SqlDataAdapter adp15 = new SqlDataAdapter("UpdatetRopeDiscard", sc.con);
                            adp15.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp15.SelectCommand.Parameters.AddWithValue("@RopeId", mw.RopeId);
                            DataTable dt15 = new DataTable();
                            adp15.Fill(dt15);

                            //var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == mw.RopeId && b.DeleteStatus == false);
                            //if (result != null)
                            //{

                            //       result.OutofServiceDate = null;
                            //       result.ModifiedDate = DateTime.Now;
                            //       sc.SaveChanges();


                            //       Below21RopesAtDeleteTime();
                            //}

                            try
                                                 {

                                                        //var result1 = sc.AssignRopetoWinch.SingleOrDefault(b => b.RopeId == mw.RopeId);
                                                        //if (result1 != null)
                                                        //{

                                                        //    result1.IsActive = true;
                                                        //    result1.ModifiedDate = DateTime.Now;
                                                        //    sc.SaveChanges();
                                                        //}

                                                        var result11 = sc.RopeDamage.SingleOrDefault(b => b.RopeId == mw.RopeId);
                                                        if (result11 != null)
                                                        {

                                                               int ropeid = result11.RopeId;
                                                               int notifiid = Convert.ToInt32(result11.NotificationId);


                                                               result11.IsActive = true;
                                                               result11.ModifiedDate = DateTime.Now;
                                                               sc.SaveChanges();
                                                        }

                                                        SqlDataAdapter adpkk = new SqlDataAdapter("select NotificationAlertType from Notifications where RopeId=" + mw.RopeId + "", sc.con);
                                                        DataTable dtkk = new DataTable();
                                                        adpkk.Fill(dtkk);
                                                        for (int i = 0; i < dtkk.Rows.Count; i++)
                                                        {
                                                               int gg = Convert.ToInt32(dtkk.Rows[i][0]);
                                                               int[] RopesNTails = { 5, 6, 7, 8, 9, 95, 10 };
                                                               if (RopesNTails.Contains(gg) == true)
                                                               {
                                                                      //SqlDataAdapter adp1 = new SqlDataAdapter("update Notifications set IsActive='true' where NotificationAlertType=" + gg + "", sc.con);
                                                                      //DataTable dt1 = new DataTable();
                                                                      //adp1.Fill(dt1);
                                        SqlDataAdapter adp1 = new SqlDataAdapter("UpdatetNotifications1", sc.con);
                                        adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
                                        adp1.SelectCommand.Parameters.AddWithValue("@NotificationAlertType", gg);
                                        DataTable dt1 = new DataTable();
                                        adp1.Fill(dt1);
                                    }
                                                        }
                                                        SqlDataAdapter adp17 = new SqlDataAdapter("delete from notifications where NotificationAlertType=41 and ropeid=" + mw.RopeId + "", sc.con);
                                                        DataTable dt17 = new DataTable();
                                                        adp17.Fill(dt17);

                                                 }
                                                 catch { }
                                                 MessageBox.Show("Record deleted successfully ", "Mooring Line", MessageBoxButton.OK, MessageBoxImage.Information);
                                                 RopeDiscardListViewModel ss = new RopeDiscardListViewModel();
                                                 ss.GetRopeDiscardList();
                                          }



                                          //var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.ToList().Where(x => x.DeleteStatus == false));
                                          //MooringWinchRopeViewModel cc = new MooringWinchRopeViewModel(lostdata);


                                          //var lostdata = new ObservableCollection<RopeDiscardRecordClass>(sc.MooringWinchRope.ToList().Where(x => x.DeleteStatus == false));
                                          //MooringWinchRopeViewModel cc = new MooringWinchRopeViewModel(lostdata);



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
              public RopeDiscardListViewModel(ObservableCollection<RopeDiscardRecordClass> ass)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     GetRopeDiscardList();

              }



              public static ObservableCollection<RopeDiscardRecordClass> loadUserAccess = new ObservableCollection<RopeDiscardRecordClass>();
              public ObservableCollection<RopeDiscardRecordClass> LoadUserAccess
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

              public void GetRopeDiscardList()
              {
                     try
                     {
                            LoadUserAccess.Clear();
                            SqlCommand cmd = new SqlCommand("GetRopeDiscardList", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", 0);

                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Rows)
                            {
                                   LoadUserAccess.Add(new RopeDiscardRecordClass()
                                   {
                                          RopeId = (int)row["RopeId"],
                                          RopeType = (string)row["RopeType"],
                                          CertificateNumber = (string)row["CertificateNumber"],
                                          //OutofServiceDate = (DateTime)row["outofservicedate"],

                                          OutofServiceDate1 = ((string)row["OutofServiceDate"]),
                                          //OutofServiceDate1 = ((DateTime)row["OutofServiceDate"]).ToString("yyyy-MM-dd"),
                                          // OutofServiceDate = ((DateTime)row["OutofServiceDate"]),
                                          ReasonOutofService = (row["ReasonOutofService"] == DBNull.Value) ? string.Empty : row["ReasonOutofService"].ToString(),

                                          UniqueId = (row["UniqueId"] == DBNull.Value) ? null : row["UniqueId"].ToString(),

                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }
              }

              private void ExportRopeDiscardMethod(object sender, DoWorkEventArgs eb)
              {
                     try
                     {
                            ExportRopeDiscardMethod1();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }

              }
              private void ExportRopeDiscardMethod1()
              {
                     try
                     {
                            SqlCommand cmd = new SqlCommand("ViewRopeDiscardListExcel", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RopeTail", 0);

                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = "MooringLine_Discard_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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

                                          dt.TableName = "Line Discard";
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
                     catch
                     {

                     }
              }

              new public event PropertyChangedEventHandler PropertyChanged;
              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }

       }
}

