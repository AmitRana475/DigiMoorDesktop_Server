using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class MooringOPDamagedRopeViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              public ICommand HelpCommand { get; private set; }
              TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;



              public static int CurruntOPID { get; set; }
              public MooringOPDamagedRopeViewModel(int OpId)
              {
                     sc = new ShipmentContaxt();
                     sc.Configuration.ProxyCreationEnabled = false;

                     MooringOPDamagedRopeViewModel._MOPDamageRope.MOPId = OpId;

                     CurruntOPID = OpId;
                     BindingSubDates(OpId);
                     Items = CroppingReason();
                     OnPropertyChanged(new PropertyChangedEventArgs("Items"));

                     //SelectedItems = CroppingReason();
                     //SelectedItems.Add("All", "0");
                     //OnPropertyChanged(new PropertyChangedEventArgs("SelectedItems"));

                     backToListCommand = new RelayCommand(GoBackToList);
                     addDamageCommand = new RelayCommand(AddDamageFilds);

                     viewCommand = new RelayCommand<RopeDamageRecordClass>(Viewropediscard);
                     saveCommand = new RelayCommand<MODamageRopeClass>(SaveMODamageRope);
                     cancelCommand = new RelayCommand(CancelMooringWinch);
                     deleteCommand = new RelayCommand<RopeDamageRecordClass>(DeleteMooringBirhD);
                     GetMooringOperationDamagedList();
                     assignrope = GetAssRope();


                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
              }


              private void Viewropediscard(RopeDamageRecordClass mw)
              {
                     try
                     {
                            //ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch(mw);
                            StaticHelper.ViewId = mw.RopeId;
                            StaticHelper.RopeTailId = 0;
                            ChildWindowManager.Instance.ShowChildWindow(new ViewMOpDamage());

                            //ChildWindowManager.Instance.CloseChildWindow();
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private ICommand deleteCommand;
              public ICommand DeleteCommand
              {
                     get { return deleteCommand; }
                     set { deleteCommand = value; }
              }
              private ICommand viewCommand;
              public ICommand ViewCommand
              {
                     get { return viewCommand; }
                     set { viewCommand = value; }
              }
              private static ObservableCollection<RopeDamageRecordClass> loadMooringOpBDamagedList = new ObservableCollection<RopeDamageRecordClass>();

              public ObservableCollection<RopeDamageRecordClass> LoadMooringOpBDamagedList
              {
                     get
                     {
                            return loadMooringOpBDamagedList;
                     }
                     set
                     {
                            loadMooringOpBDamagedList = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringOpBDamagedList"));

                     }
              }
              private void DeleteMooringBirhD(RopeDamageRecordClass mw)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {

                                   //var findrank = sc.RopeDamage.Where(x => x.DamageObserved == "Mooring Operation" & mw.MOPId == CurruntOPID).FirstOrDefault();
                                   //if (findrank != null)
                                   //{


                                   //       sc.Entry(findrank).State = EntityState.Deleted;
                                   //       sc.SaveChanges();

                                   //if(mw.IncidentActlion=="Spliced")
                                   //{

                                   //}

                                   RopeDamageRecordClass findcrs = sc.RopeDamage.Where(x => x.Id == mw.Id & x.DamageObserved == "Mooring Operation" & x.MOPId == CurruntOPID).FirstOrDefault();
                                   if (findcrs != null)
                                   {
                                          var result2 = sc.RopeDamage.SingleOrDefault(b => b.Id == mw.Id && b.DamageObserved == "Mooring Operation" & b.MOPId == CurruntOPID && b.RopeTail == 0 && b.IsActive == true);
                                          if (result2 != null)
                                          {

                                                 result2.IsActive = false;
                                                 result2.ModifiedBy = "Admin";

                                                 result2.ModifiedDate = DateTime.Now;
                                                 sc.SaveChanges();

                                          }

                                          try
                                          {
                                                 var notiid = findcrs.NotificationId;
                                                 var notidelete = sc.Notifications.Where(x => x.Id == notiid).FirstOrDefault();
                                                 sc.Entry(notidelete).State = EntityState.Deleted;
                                                 sc.SaveChanges();
                                          }
                                          catch { }


                                          //GetMooringOperationBirthD();
                                          GetMooringOperationDamagedList();
                                          MessageBox.Show("Record deleted successfully ", "Delete Department", MessageBoxButton.OK, MessageBoxImage.Information);


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


              private void GetMooringOperationDamagedList()
              {

                     try
                     {

                            loadMooringOpBDamagedList.Clear();

                            string qry = "MooringDamageRecord";
                            SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con);
                            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp.SelectCommand.Parameters.AddWithValue("@MOpid", CurruntOPID);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);
                            foreach (DataRow row in ds.Rows)
                            {
                                   loadMooringOpBDamagedList.Add(new RopeDamageRecordClass()
                                   {
                                          Id = (int)row["Id"],
                                          //AssignedNumber = (string)row["AssignedNumber"],
                                          AssignedNumber = (row["AssignedNumber"] == DBNull.Value) ? "Not Assigned" : row["AssignedNumber"].ToString(),
                                          AssignedLocation = (row["AssignedLocation"] == DBNull.Value) ? "Not Assigned" : row["AssignedLocation"].ToString(),
                                          CertificateNumber = (string)row["CertificateNumber"],
                                          //  AssignedLocation = (string)row["AssignedLocation"],
                                          //DamageObserved = (string)row["DamageObserved"],
                                          DamageObserved = (row["DamageObserved"] == DBNull.Value) ? string.Empty : row["DamageObserved"].ToString(),
                                          IncidentActlion = (row["IncidentActlion"] == DBNull.Value) ? string.Empty : row["IncidentActlion"].ToString(),
                                          IncidentReport = (row["IncidentReport"] == DBNull.Value) ? string.Empty : row["IncidentReport"].ToString(),
                                          // MooringOperation = (string)row["MooringOperation"],
                                          MOPId = CurruntOPID
                                   });
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringOpBDamagedList"));

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);

                     }

              }

              private string fieldBox2visibility = "Collapsed";

              public string FieldBox2Visibility
              {
                     get { return fieldBox2visibility; }
                     set
                     {
                            fieldBox2visibility = value;
                            RaisePropertyChanged("FieldBox2Visibility");
                     }
              }

              private string gListBox1visibility = "Visible";

              public string GListBox1Visibility
              {
                     get { return gListBox1visibility; }
                     set
                     {
                            gListBox1visibility = value;
                            RaisePropertyChanged("GListBox1Visibility");
                     }
              }

              private void GoBackToList()
              {
                     gListBox1visibility = "Visible";
                     RaisePropertyChanged("GListBox1Visibility");

                     fieldBox2visibility = "Collapsed";
                     RaisePropertyChanged("FieldBox2Visibility");

              }

              private void AddDamageFilds()
              {
                     gListBox1visibility = "Collapsed";
                     RaisePropertyChanged("GListBox1Visibility");

                     fieldBox2visibility = "Visible";
                     RaisePropertyChanged("FieldBox2Visibility");
              }


              private void SaveMODamageRope(MODamageRopeClass moorwinchrope)
              {
                     try
                     {
                            //var maxNotiid = sc.Notifications.Select(x => x.Id).Max();
                            //int notiid = maxNotiid + 1;

                            int notiid = sc.NextNotiId();


                            if (moorwinchrope.RopeId == 0)
                            {
                                   // moorwinchrope.IncidentAction = "Spliced";

                                   MessageBox.Show("Select Winch on which damaged line was reeled", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                   return;
                            }
                            if (moorwinchrope.DamageLocation == "--Select--" || moorwinchrope.DamageLocation == null)
                            {
                                   // moorwinchrope.IncidentAction = "Spliced";
                                   MessageBox.Show("Select Damage Location", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                   return;
                            }

                            if (moorwinchrope.DamageReason == "--Select--" || moorwinchrope.DamageReason == null)
                            {

                                   MessageBox.Show("Select Damage reason", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                   return;
                            }

                            if (moorwinchrope.IncidentReport == "--Select--" || moorwinchrope.IncidentReport == null)
                            {
                                   MessageBox.Show("Select Incident Report", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                   return;
                            }

                            if (moorwinchrope.IncidentAction == "--Select--" || moorwinchrope.IncidentAction == null)
                            {
                                   // moorwinchrope.IncidentAction = "Spliced";
                                   MessageBox.Show("Select Action to the line after damage", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                   return;
                            }

                            if (moorwinchrope.ActionAfterDamage == "Spliced")
                            {

                                   if (moorwinchrope.SplicedMethod == "" || moorwinchrope.SplicedMethod == null)
                                   {
                                          // moorwinchrope.IncidentAction = "Spliced";
                                          MessageBox.Show("Enter Spliced Method", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          return;
                                   }

                                   if (moorwinchrope.SplicingDoneBy == null || moorwinchrope.SplicingDoneBy == "--Select--")
                                   {
                                          MessageBox.Show("Select Spliced By", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          return;
                                   }


                                   // moorwinchrope.IsCropped;

                                   if (moorwinchrope.IsCropped == "--Select--" || moorwinchrope.IsCropped == null)
                                   {
                                          // moorwinchrope.IncidentAction = "Spliced";
                                          MessageBox.Show("Select Is Rope Cropped", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          return;
                                   }


                                   if (moorwinchrope.IsCropped == "Yes")
                                   {


                                          if (moorwinchrope.CroppedOutboardEnd == null || moorwinchrope.CroppedOutboardEnd == "--Select--")
                                          {
                                                 // rpcrp.CroppedOutboardEnd = "A";
                                                 MessageBox.Show("Please Enter Cropped Outboard End", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                 return;

                                          }
                                          if (moorwinchrope.LengthofCroppedRope1 == null || moorwinchrope.LengthofCroppedRope1 == 0)
                                          {
                                                 MessageBox.Show("Please Enter Length of Cropping !", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                 return;
                                          }
                                          if (moorwinchrope.ReasonofCropping == null || moorwinchrope.ReasonofCropping == "")
                                          {
                                                 MessageBox.Show("Please Enter Reason of Cropping", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                 return;
                                          }


                                   }


                            }

                            if (moorwinchrope.ActionAfterDamage == "Cropped")
                            {
                                   if (moorwinchrope.CroppedOutboardEnd == null || moorwinchrope.CroppedOutboardEnd == "--Select--")
                                   {
                                          // rpcrp.CroppedOutboardEnd = "A";
                                          MessageBox.Show("Please Enter Cropped Outboard End", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          return;

                                   }
                                   if (moorwinchrope.LengthofCroppedRope == null || moorwinchrope.LengthofCroppedRope == 0)
                                   {
                                          MessageBox.Show("Please Enter Length of Cropped !", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          return;
                                   }
                                   if (moorwinchrope.ReasonofCropping == null || moorwinchrope.ReasonofCropping == "")
                                   {
                                          MessageBox.Show("Please Enter Reason of Cropping", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          return;
                                   }
                            }

                            if (moorwinchrope.ActionAfterDamage == "Discarded")
                            {
                                   //var k=  moorwinchrope.DiscaredDate;
                                   //var k3 = moorwinchrope.MOPId;


                                   if (moorwinchrope.ReasonOutofService == "--Select--" || moorwinchrope.ReasonOutofService == null)
                                   {
                                          // moorwinchrope.IncidentAction = "Spliced";
                                          MessageBox.Show("Select Reason for out of service", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          return;
                                   }
                                   else if (moorwinchrope.ReasonOutofService == "Damaged")
                                   {

                                          if (moorwinchrope.DamageObserved1 == "--Select--" || moorwinchrope.DamageObserved1 == null)
                                          {

                                                 MessageBox.Show("Select Damage Observed ", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                 return;
                                          }
                                          //else if (moorwinchrope.DamageObserved1 == "Mooring Operation")
                                          //{                                              
                                          //       if (moorwinchrope.MooringOperation == "--Select--" || moorwinchrope.MooringOperation == null)
                                          //       {                                                       
                                          //              MessageBox.Show("Select Mooring Operation", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          //              return;
                                          //       }
                                          //}

                                   }
                                   else if (moorwinchrope.ReasonOutofService == "Other")
                                   {
                                          if (moorwinchrope.otherReason == "" || moorwinchrope.otherReason == null)
                                          {

                                                 MessageBox.Show("Enter Other Reason", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                 return;
                                          }
                                   }


                            }

                            if (moorwinchrope.ActionAfterDamage == "End-to-end")
                            {
                                   // rpcrp.EndtoEndDoneDate = moorwinchrope.EndtoEndDoneDate;

                                   if (moorwinchrope.CurrentOutboadEndinUse == null)
                                   {

                                          MessageBox.Show("Outboad End can not be Null.", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                                          return;
                                   }
                            }



                            ////For Notification Work
                            var mrRope = sc.MooringWinchRope.ToList();
                            var winchid = sc.AssignRopetoWinch.Where(x => x.RopeId == moorwinchrope.RopeId && x.IsActive == true).Select(x => x.WinchId).SingleOrDefault();
                            var ropetypeid = mrRope.Where(x => x.Id == moorwinchrope.RopeId).Select(x => x.RopeTypeId).SingleOrDefault();
                            var manufacid = mrRope.Where(x => x.Id == moorwinchrope.RopeId).Select(x => x.ManufacturerId).SingleOrDefault();
                            //var ropename = sc.MooringRopeType.Where(x => x.Id == ropetypeid).Select(x => x.RopeType).SingleOrDefault();
                            var ropename = mrRope.Where(x => x.Id == moorwinchrope.RopeId).Select(x => x.CertificateNumber + " - " + x.UniqueID).SingleOrDefault();
                            var WinchName = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.AssignedNumber).SingleOrDefault();
                            var WinchLocation = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Location).SingleOrDefault();





                            RopeDamageRecordClass rdm = new RopeDamageRecordClass();
                            rdm.RopeId = moorwinchrope.RopeId;
                            rdm.MOPId = moorwinchrope.MOPId;
                            rdm.DamageLocation = moorwinchrope.DamageLocation;
                            rdm.DamageObserved = moorwinchrope.DamageObserved;
                            rdm.DamageReason = moorwinchrope.DamageReason;
                            rdm.DamageDate = DateTime.Now;
                            rdm.IncidentReport = moorwinchrope.IncidentReport;
                            rdm.IncidentActlion = moorwinchrope.IncidentAction;
                            rdm.WinchId = winchid;
                            rdm.CreatedDate = DateTime.Now;
                            rdm.CreatedBy = "Admin";
                            rdm.IsActive = true;
                            rdm.RopeTail = 0;
                            rdm.NotificationId = notiid;
                            sc.RopeDamage.Add(rdm);

                            sc.SaveChanges();


                            var damageid = sc.RopeDamage.Select(x => x.Id).Max();
                              


                            if (moorwinchrope.ActionAfterDamage == "Spliced")
                            {

                                   RopeSplicingClass rpspl = new RopeSplicingClass();
                                   rpspl.RopeId = moorwinchrope.RopeId;
                                   rpspl.SplicingDoneDate = moorwinchrope.SplicedDate;
                                   rpspl.SplicingDoneBy = moorwinchrope.SplicingDoneBy;
                                   rpspl.SplicingMethod = moorwinchrope.SplicedMethod;
                                   rpspl.IsCropped = moorwinchrope.IsCropped;
                                   rpspl.WinchId = winchid;

                                   if (rpspl.IsCropped == "Yes")

                                   {
                                          var maxSplicedId = sc.RopeSplicing.DefaultIfEmpty().Max(r => r == null ? 1 : r.Id);
                                          //var maxSplicedId = sc.RopeSplicing.Select(x => x.Id).Max();


                                          RopeCroppingClass rpcrp = new RopeCroppingClass();
                                          rpcrp.RopeId = moorwinchrope.RopeId;
                                          rpcrp.CroppedOutboardEnd = moorwinchrope.CroppedOutboardEnd;

                                          rpcrp.LengthofCroppedRope = moorwinchrope.LengthofCroppedRope1;

                                          rpcrp.CroppedOutboardEnd = moorwinchrope.CroppedOutboardEnd;

                                          rpcrp.CroppedDate = moorwinchrope.SplicedDate;
                                          rpcrp.ReasonofCropping = moorwinchrope.ReasonofCropping;
                                          rpcrp.WinchId = winchid;
                                          rpcrp.CreatedDate = DateTime.Now;
                                          rpcrp.CreatedBy = "Admin";
                                          rpcrp.IsActive = true;
                                          rpcrp.NotificationId = notiid;
                                          rpcrp.RopeTail = 0;
                                          rpcrp.SplicedId = maxSplicedId;
                                          rpcrp.MOpid = moorwinchrope.MOPId;
                                          rpcrp.DamageId = damageid;
                                          sc.RopeCropping.Add(rpcrp);

                                          var length = mrRope.Where(x => x.Id == moorwinchrope.RopeId).Select(x => x.Length).SingleOrDefault();

                                          var percent = (length * 10) / 100;
                                          var crplength = sc.RopeCropping.Where(x => x.RopeId == moorwinchrope.RopeId && x.IsActive == true).Select(x => x.LengthofCroppedRope).Sum();

                                          if (crplength >= percent)
                                          {
                                                 var notification = "";
                                                 if (WinchName != null)
                                                 {

                                                        notification = "Cropped more than 10% - Line " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                                                 }
                                                 else
                                                 {
                                                        notification = "Cropped more than 10% - Line " + ropename + "";
                                                 }
                                                 NotificationsClass noti = new NotificationsClass();
                                                 noti.Acknowledge = false;
                                                 noti.AckRecord = "Not yet acknowledged";
                                                 noti.Notification = notification;
                                                 noti.NotificationType = 2;
                                                 noti.RopeId = moorwinchrope.RopeId;
                                                 noti.IsActive = true;
                                                 //noti.NotificationDueDate = DBNull.Value;
                                                 noti.CreatedDate = DateTime.Now;
                                                 noti.CreatedBy = "Admin";
                                                 noti.NotificationAlertType = (int)NotificationAlertType.Over_Cropping;
                                                 sc.Notifications.Add(noti);
                                                 sc.SaveChanges();

                                                 StaticHelper.AlarmFunction(1, true);
                                          }


                                   }


                                   rpspl.CreatedDate = DateTime.Now;
                                   rpspl.CreatedBy = "Admin";
                                   rpspl.IsActive = true;
                                   rpspl.RopeTail = 0;
                                   rpspl.MOpid = moorwinchrope.MOPId;
                                   rpspl.DamageId = damageid;
                                   rpspl.NotificationId = notiid;
                                   sc.RopeSplicing.Add(rpspl);
                                   sc.SaveChanges();

                                   try
                                   {
                                          //var mrRope = sc.MooringWinchRope.ToList();
                                          //var winchid = sc.AssignRopetoWinch.Where(x => x.RopeId == moorwinchrope.RopeId && x.IsActive == true).Select(x => x.WinchId).SingleOrDefault();
                                          ////var length = sc.MooringWinchRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.Length).SingleOrDefault();
                                          ////var percent = (length * 10) / 100;
                                          ////var crplength = sc.RopeCropping.Where(x => x.RopeId == rpspc.RopeId).Select(x => x.LengthofCroppedRope).Sum();
                                          //var ropetypeid = mrRope.Where(x => x.Id == moorwinchrope.RopeId).Select(x => x.RopeTypeId).SingleOrDefault();
                                          //var manufacid = mrRope.Where(x => x.Id == moorwinchrope.RopeId).Select(x => x.ManufacturerId).SingleOrDefault();
                                          //var ropename = sc.MooringRopeType.Where(x => x.Id == ropetypeid).Select(x => x.RopeType).SingleOrDefault();
                                          //var WinchName = sc.MooringWinch.Where(x => x.Id == winchid).Select(x => x.AssignedNumber).SingleOrDefault();
                                          //var WinchLocation = sc.MooringWinch.Where(x => x.Id == winchid).Select(x => x.Location).SingleOrDefault();


                                          var notification = "";
                                          if (WinchName != null && WinchName != "" && WinchName != "Not Assigned")
                                          {
                                                 notification = "Spliced - Line " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                                          }
                                          else
                                          {
                                                 notification = "Spliced - Line " + ropename + "";
                                          }

                                          // var notification = "Spliced - Rope " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                                          NotificationsClass noti = new NotificationsClass();
                                          noti.Acknowledge = false;
                                          noti.AckRecord = "Not yet acknowledged";
                                          noti.Notification = notification;
                                          noti.NotificationType = 1;
                                          //noti.NotificationDueDate = DBNull.Value;
                                          noti.CreatedDate = DateTime.Now;
                                          noti.CreatedBy = "Admin";
                                          noti.IsActive = true;
                                          noti.RopeId = moorwinchrope.RopeId;
                                          sc.Notifications.Add(noti);
                                          sc.SaveChanges();

                                          StaticHelper.AlarmFunction(1, true);

                                   }
                                   catch { }

                            }

                            if (moorwinchrope.ActionAfterDamage == "Cropped")
                            {
                                   RopeCroppingClass rpcrp = new RopeCroppingClass();
                                   rpcrp.RopeId = moorwinchrope.RopeId;
                                   rpcrp.CroppedOutboardEnd = moorwinchrope.CroppedOutboardEnd;
                                   rpcrp.LengthofCroppedRope = moorwinchrope.LengthofCroppedRope;
                                   rpcrp.CroppedDate = moorwinchrope.CroppedDate;
                                   rpcrp.ReasonofCropping = moorwinchrope.ReasonofCropping;
                                   rpcrp.CreatedDate = DateTime.Now;
                                   rpcrp.CreatedBy = "Admin";
                                   rpcrp.IsActive = true;
                                   rpcrp.NotificationId = notiid;
                                   rpcrp.RopeTail = 0;
                                   rpcrp.WinchId = winchid;
                                   rpcrp.MOpid = moorwinchrope.MOPId;
                                   rpcrp.DamageId = damageid;
                                   sc.RopeCropping.Add(rpcrp);
                                   sc.SaveChanges();

                                   try
                                   {
                                          //var mrRope = sc.MooringWinchRope.ToList();
                                          //var winchid = sc.AssignRopetoWinch.Where(x => x.RopeId == moorwinchrope.RopeId && x.IsActive == true).Select(x => x.WinchId).SingleOrDefault();
                                          var length = sc.MooringWinchRope.Where(x => x.Id == moorwinchrope.RopeId).Select(x => x.Length).SingleOrDefault();
                                          var percent = (length * 10) / 100;
                                          var crplength = sc.RopeCropping.Where(x => x.RopeId == moorwinchrope.RopeId).Select(x => x.LengthofCroppedRope).Sum();

                                          if (crplength >= percent)
                                          {
                                                 var notification = "";
                                                 if (WinchName != null && WinchName != "" && WinchName != "Not Assigned")
                                                 {
                                                        notification = "Cropped more than 10% - Line " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                                                 }
                                                 else
                                                 {
                                                        notification = "Cropped more than 10% - Line " + ropename + "";
                                                 }
                                                 NotificationsClass noti = new NotificationsClass();
                                                 noti.Acknowledge = false;
                                                 noti.AckRecord = "Not yet acknowledged";
                                                 noti.Notification = notification;
                                                 noti.NotificationType = 2;
                                                 //noti.NotificationDueDate = DBNull.Value;
                                                 noti.CreatedDate = DateTime.Now;
                                                 noti.CreatedBy = "Admin";
                                                 noti.IsActive = true;
                                                 noti.RopeId = moorwinchrope.RopeId;
                                                 noti.NotificationAlertType = (int)NotificationAlertType.Over_Cropping;
                                                 sc.Notifications.Add(noti);
                                                 sc.SaveChanges();
                                          }

                                          //if (crplength <= percent)
                                          //{
                                          //       var notification = "";
                                          //       if (WinchName != null && WinchName != "" && WinchName != "Not Assigned")
                                          //       {
                                          //              notification = "Cropped more than 10% - Rope " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                                          //       }
                                          //       else
                                          //       {
                                          //              notification = "Cropped more than 10% - Rope " + ropename + "";
                                          //       }

                                          //       // var notification = "Cropped more than 10% - Rope " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                                          //       NotificationsClass noti = new NotificationsClass();
                                          //       noti.Acknowledge = false;
                                          //       noti.AckRecord = "Not yet acknowledged";
                                          //       noti.Notification = notification;
                                          //       noti.NotificationType = 1;
                                          //       //noti.NotificationDueDate = DBNull.Value;
                                          //       noti.CreatedDate = DateTime.Now;
                                          //       noti.CreatedBy = "Admin";
                                          //       noti.IsActive = true;
                                          //       noti.RopeId = moorwinchrope.RopeId;
                                          //       noti.NotificationAlertType = (int)NotificationAlertType.Over_Cropping;
                                          //       sc.Notifications.Add(noti);
                                          //       sc.SaveChanges();
                                          //}


                                   }
                                   catch { }



                            }
                            if (moorwinchrope.ActionAfterDamage == "Discarded")
                            {
                                   var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == moorwinchrope.RopeId);
                                   if (result != null)
                                   {
                                          result.OutofServiceDate = moorwinchrope.DiscaredDate;
                                          result.DamageObserved = moorwinchrope.DamageObserved1;
                                          result.MooringOperationID = moorwinchrope.MOPId;
                                          result.ReasonOutofService = moorwinchrope.ReasonOutofService;
                                          result.OtherReason = moorwinchrope.otherReason;
                                          result.ModifiedBy = "Admin";
                                          result.WinchId = winchid;
                                          result.ModifiedDate = DateTime.Now;
                                          sc.SaveChanges();


                                          try
                                          {
                                                 //var mrRope = sc.MooringWinchRope.ToList();
                                                 //var winchid = sc.AssignRopetoWinch.Where(x => x.RopeId == rpspc.RopeId && x.IsActive == true).Select(x => x.WinchId).SingleOrDefault();
                                                 ////var length = sc.MooringWinchRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.Length).SingleOrDefault();
                                                 ////var percent = (length * 10) / 100;
                                                 ////var crplength = sc.RopeCropping.Where(x => x.RopeId == rpspc.RopeId).Select(x => x.LengthofCroppedRope).Sum();
                                                 //var ropetypeid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.RopeTypeId).SingleOrDefault();
                                                 //var manufacid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.ManufacturerId).SingleOrDefault();
                                                 //var ropename = sc.MooringRopeType.Where(x => x.Id == ropetypeid).Select(x => x.RopeType).SingleOrDefault();
                                                 //var WinchName = sc.MooringWinch.Where(x => x.Id == winchid).Select(x => x.AssignedNumber).SingleOrDefault();
                                                 //var WinchLocation = sc.MooringWinch.Where(x => x.Id == winchid).Select(x => x.Location).SingleOrDefault();

                                                 var notification = "";
                                                 if (WinchName != null && WinchName != "" && WinchName != "Not Assigned")
                                                 {
                                                        notification = "Out of Service / discarded - Line " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                                                 }
                                                 else
                                                 {
                                                        notification = "Out of Service / discarded - Line " + ropename + "";
                                                 }

                                                 //var notification = "Out of Service / discarded - Rope " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                                                 NotificationsClass noti = new NotificationsClass();
                                                 noti.Acknowledge = false;
                                                 noti.AckRecord = "Not yet acknowledged";
                                                 noti.Notification = notification;
                                                 noti.NotificationType = 1;
                                                 //noti.NotificationDueDate = DBNull.Value;
                                                 noti.CreatedDate = DateTime.Now;
                                                 noti.CreatedBy = "Admin";
                                                 noti.NotificationAlertType = (int)NotificationAlertType.OutofService_discarded_Rope;
                                                 noti.IsActive = true;
                                                 noti.RopeId = moorwinchrope.RopeId;
                                                 sc.Notifications.Add(noti);
                                                 sc.SaveChanges();
                                          }
                                          catch { }
                                   }

                            }
                            if (moorwinchrope.ActionAfterDamage == "End-to-end")
                            {
                                   RopeEndtoEnd2Class rpcrp = new RopeEndtoEnd2Class();
                                   rpcrp.RopeId = moorwinchrope.RopeId;
                                   rpcrp.EndtoEndDoneDate = moorwinchrope.EndtoEndDoneDate;
                                   rpcrp.CurrentOutboadEndinUse = moorwinchrope.CurrentOutboadEndinUse;

                                   rpcrp.CreatedDate = DateTime.Now;
                                   rpcrp.CreatedBy = "Admin";
                                   rpcrp.IsActive = true;
                                   rpcrp.WinchId = winchid;
                                   //rpcrp.RopeTail = 0;
                                   rpcrp.MOpid = moorwinchrope.MOPId;
                                   rpcrp.DamageId = damageid;
                                   sc.RopeEndtoEnd2.Add(rpcrp);

                            }



                            sc.SaveChanges();
                            GetMooringOperationDamagedList();

                            try
                            {
                                   var notification = "";
                                   if (WinchName != null && WinchName != "" && WinchName != "Not Assigned")
                                   {
                                          notification = "Damaged - Line " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                                   }
                                   else
                                   {
                                          notification = "Damaged - Line " + ropename + "";
                                   }
                                   // var notification = "Damaged - Rope " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                                   NotificationsClass noti = new NotificationsClass();
                                   noti.Acknowledge = false;
                                   noti.AckRecord = "Not yet acknowledged";
                                   noti.Notification = notification;
                                   noti.NotificationType = 1;


                                   //noti.NotificationDueDate = DBNull.Value;
                                   noti.CreatedDate = DateTime.Now;
                                   noti.CreatedBy = "Admin";
                                   noti.IsActive = true;
                                   noti.RopeId = moorwinchrope.RopeId;
                                   sc.Notifications.Add(noti);
                                   sc.SaveChanges();
                            }
                            catch { }



                            MessageBox.Show("Record saved successfully ", "Line EndtoEnd", MessageBoxButton.OK, MessageBoxImage.Information);

                            GoBackToList();

                            // CancelMooringWinch();

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private Dictionary<string, object> CroppingReason()
              {
                     var AddDepartments = new Dictionary<string, object>();
                     //int counter = 0;           
                     AddDepartments.Add("Cut Strands", 0);
                     AddDepartments.Add("Kinked", 1);
                     AddDepartments.Add("Abrasion", 2);
                     AddDepartments.Add("Paint Damage", 3);
                     AddDepartments.Add("Deformation", 4);
                     AddDepartments.Add("Residual Testing", 5);

                     return AddDepartments;
              }

              private static ObservableCollection<Winchcombo> assignrope = new ObservableCollection<Winchcombo>();
              public ObservableCollection<Winchcombo> AssignRope
              {
                     get
                     {
                            return assignrope;
                     }
                     set
                     {
                            assignrope = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("AssignRope"));
                     }
              }



              public static Winchcombo _sropeass;
              public Winchcombo SRopeAss
              {
                     get
                     {

                            if (_sropeass != null)
                            {
                                   var data = sc.MooringWinchRope.Where(x => x.Id == _sropeass.Id).FirstOrDefault();
                                   //var data = sc.MooringWinch.Where(x => x.Id == _sropeass.Id).FirstOrDefault();
                                   if (data != null)
                                   {
                                          //var data1 = sc.MooringWinchRope.Where(x => x.Id == sropetype.Id).FirstOrDefault();
                                          // MOPDamageRope.WinchId = data.Id;
                                          MOPDamageRope.RopeId = data.Id;
                                          //CrossShifting.AssignedLocation = data.Location;


                                   }

                                   OnPropertyChanged(new PropertyChangedEventArgs("AssignRopeToWinch"));
                            }
                            return _sropeass;
                     }
                     set
                     {
                            _sropeass = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("SRopeAss"));
                     }
              }


              public void BindingSubDates(int OpID)
              {
                     var MoorRecod = sc.MOperationBirthDetailTbl.Where(x => x.OPId == OpID && x.IsActive == true).FirstOrDefault();

                     DateTime fd = MoorRecod.FastDatetime;  // Convert.ToDateTime(to);
                     DateTime cd = MoorRecod.CastDatetime; //Convert.ToDateTime(Frm);
                     var Day_Diff = (int)(cd - fd).TotalDays;

                     subDates.Clear();
                     OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));
                     for (int i = 0; i < Day_Diff + 1; i++)
                     {
                            // DateTime d1 = fd.AddDays(i);
                            // var dd = d1.AddDays(i);

                            subDates.Add(fd.AddDays(i).ToShortDateString());
                            OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));
                     }
                     FastDate = subDates.FirstOrDefault();
              }

              public static ObservableCollection<string> subDates = new ObservableCollection<string>();
              public ObservableCollection<string> SubDates
              {
                     get
                     {

                            return subDates;
                     }
                     set
                     {
                            subDates = value;
                            //RaisePropertyChanged("SubDates");
                            OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));

                     }
              }

              public static string _FastDate;

              public string FastDate
              {
                     get { return _FastDate; }
                     set
                     {
                            _FastDate = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("FastDate"));
                     }
              }

              private ObservableCollection<Winchcombo> GetAssRope()
              {
                     ObservableCollection<Winchcombo> AddWinchId = new ObservableCollection<Winchcombo>();
                     Winchcombo rop;
                     //SqlDataAdapter adp = new SqlDataAdapter("GetAssignedRopeTail", sc.con);
                     //SqlDataAdapter adp = new SqlDataAdapter("select a.Id,a.AssignedNumber,b.WinchId from MooringWinchDetail a inner join AssignRopeToWinch b on a.Id=b.WinchId  where b.IsActive=1 and b.RopeTail=0 and b.RopeId not in (select RopeId from RopeDamageRecord)", sc.con);
                     using (SqlDataAdapter adp = new SqlDataAdapter("RopeinMopDamage", sc.con))
                     {
                            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp.SelectCommand.Parameters.AddWithValue("@OperationId", CurruntOPID);
                            DataTable ds = new DataTable();
                            adp.Fill(ds);

                            foreach (DataRow row in ds.Rows)
                            {
                                   rop = new Winchcombo();
                                   rop.Id = (int)row["Id"];
                                   rop.AssignedNumber = (string)row["AssignedNumber"];
                                   AddWinchId.Add(rop);
                            }

                     }
                     return AddWinchId;

                     //ObservableCollection<Winchcombo> AddWinchId = new ObservableCollection<Winchcombo>();
                     //var data = sc.MooringWinch.Select(x => new { x.Id, x.AssignedNumber }).ToList();

                     //Winchcombo rop;
                     //foreach (var item in data)
                     //{

                     //    rop = new Winchcombo();
                     //    rop.Id = item.Id;
                     //    rop.AssignedNumber = item.AssignedNumber;
                     //    AddWinchId.Add(rop);
                     //}

                     //return AddWinchId;
              }

              public static MODamageRopeClass _MOPDamageRope = new MODamageRopeClass();
              //private MODamageRopeClass _MOPDamageRope = new MODamageRopeClass();
              public MODamageRopeClass MOPDamageRope
              {
                     get
                     {
                            return _MOPDamageRope;
                     }
                     set
                     {
                            _MOPDamageRope = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("MOPDamageRope"));
                     }
              }
              private Dictionary<string, object> _items;
              private Dictionary<string, object> _selectedItems;

              public Dictionary<string, object> Items
              {
                     get
                     {
                            return _items;
                     }
                     set
                     {
                            _items = value;
                            RaisePropertyChanged("Items");
                     }
              }

              public Dictionary<string, object> SelectedItems
              {
                     get
                     {
                            return _selectedItems;
                     }
                     set
                     {
                            _selectedItems = value;
                            RaisePropertyChanged("SelectedItems");
                     }
              }


              private ICommand cancelCommand;
              public ICommand CancelCommand
              {
                     get { return cancelCommand; }
              }


              private ICommand addDamageCommand;
              public ICommand AddDamageCommand
              {
                     get { return addDamageCommand; }
              }

              private ICommand backToListCommand;
              public ICommand BackToListCommand
              {
                     get { return backToListCommand; }
              }

              private ICommand saveCommand;
              public ICommand SaveCommand
              {
                     get { return saveCommand; }
              }

              private void CancelMooringWinch()
              {
                     //var lostdata = new ObservableCollection<DepartmentClass>(sc.Departments.ToList());
                     //DepartmentViewModel cc = new DepartmentViewModel(lostdata);

                     updateDamagedInMooringOP(CurruntOPID);

                     //GetMooringOperationBirthD();

                     GetMooringOperationDamagedList();
                     ChildWindowManager.Instance.CloseChildWindow();
              }

              private void updateDamagedInMooringOP(int MoorOPID)
              {

                     if (loadMooringOpBDamagedList.Count == 0)
                     {
                            string qry = "update MOperationBirthDetail set Any_Rope_Damaged = 'No' where OPId =  " + MoorOPID + " ";
                            using (SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con))
                            {
                                   // adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                   // adp.SelectCommand.Parameters.AddWithValue("@OperationId", CurruntOPID);
                                   DataTable ds = new DataTable();
                                   adp.Fill(ds);

                            }
                     }
              }


              public void GetMooringOperationBirthD()
              {

                     try
                     {
                            //ObservableCollection<MOperationBirthDetail> moringlist = new ObservableCollection<MOperationBirthDetail>();
                            MooringOPRListingViewModel.loadMooringOpBList.Clear();
                            var data = sc.MOperationBirthDetailTbl.Where(x => x.IsActive == true).ToList().OrderByDescending(x => x.OPId);
                            foreach (var item in data)
                            {
                                   MooringOPRListingViewModel.loadMooringOpBList.Add(new MOperationBirthDetail()
                                   {
                                          OPId = item.OPId,
                                          PortName = item.PortName,
                                          FastDatetime1 = item.FastDatetime.ToString("yyyy-MM-dd"),
                                          CastDatetime1 = item.CastDatetime.ToString("yyyy-MM-dd"),
                                          BirthName = item.BirthName,
                                          BirthType = item.BirthType,
                                          MooringType = item.MooringType,
                                          DraftArrivalFWD = item.DraftArrivalFWD,
                                          DraftArrivalAFT = item.DraftArrivalAFT,
                                          DepthAtBerth = item.DepthAtBerth,
                                          DraftDepartureAFT = item.DraftDepartureAFT,
                                          DraftDepartureFWD = item.DraftDepartureFWD,
                                          BerthSide = item.BerthSide,
                                          Any_Affect_Passing_Traffic = item.Any_Affect_Passing_Traffic,
                                          Berth_exposed_SeaSwell = item.Berth_exposed_SeaSwell,
                                          Any_Rope_Damaged = item.Any_Rope_Damaged,
                                          AnySquall = item.AnySquall,
                                          CurrentSpeed = item.CurrentSpeed,
                                          VesselCondition = item.VesselCondition,
                                          WindDirection = item.WindDirection,
                                          WindSpeed = item.WindSpeed,
                                          RangOfTide = item.RangOfTide,
                                          ShipAccess = item.ShipAccess,
                                          Ship_was_continuously_contact_with_fender = item.Ship_was_continuously_contact_with_fender,
                                          SurgingObserved = item.SurgingObserved,
                                   });


                            }
                            RaisePropertyChanged("LoadMooringOpBirthList");
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
