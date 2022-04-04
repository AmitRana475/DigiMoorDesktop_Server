using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
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
using System.Configuration;
using System.Collections.Generic;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class AddRopeSplicingViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;

              public AddRopeSplicingViewModel(RopeSplicingClass edeps)
              {



                     StaticHelper.Editing = true;

                     sc = new ShipmentContaxt();
                     sc.Configuration.ProxyCreationEnabled = false;

                     saveCommand = new RelayCommand<RopeSplicingClass>(UpdateRopeSplicing);
                     cancelCommand = new RelayCommand(CancelMooringWinch);

                     // MooringWinchRopeClass data = sc.MooringWinchRope.Where(x => x.Id == edeps.Id).FirstOrDefault();


                     //SRopeType = edeps.RopeType;
                     //SRopeConst = edeps.RopeConstruction;
                     //SRopeDiameter = edeps.DiaMeter;
                     //SRopeReasonoutofs = edeps.ReasonOutofService;

                     //ReceivedDate = edeps.ReceivedDate;

                     RopeSplicing = new RopeSplicingClass()
                     {
                            Id = edeps.Id,
                            //AssignedLocation = edeps.AssignedLocation,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = "Admin",
                            CreatedDate = edeps.CreatedDate,
                            CreatedBy = edeps.CreatedBy
                     };

                     Ropetypecombo abc = new Ropetypecombo()
                     {
                            Id = edeps.Id,
                            //CertificateNo = edeps.CertificateNumber
                     };
                     SRopeType = abc;

                     RaisePropertyChanged("AddRopeEndtoEndView");
                     OnPropertyChanged(new PropertyChangedEventArgs("AddRopeEndtoEndView"));
                     //EditMooringWinch(edeps);
              }
              public AddRopeSplicingViewModel()
              {
           

            StaticHelper.Editing = true;

                     sc = new ShipmentContaxt();
                     sc.Configuration.ProxyCreationEnabled = false;

                     saveCommand = new RelayCommand<RopeSplicingClass>(Saveropespl);
                     cancelCommand = new RelayCommand(CancelMooringWinch);
                     resetCommand = new RelayCommand(resetRopeSplicing);
                     GetRopeType();


                     Items = CroppingReason();
                     OnPropertyChanged(new PropertyChangedEventArgs("Items"));

                     //SelectedItems = CroppingReason();
                     //SelectedItems.Add("All", "0");
                     //OnPropertyChanged(new PropertyChangedEventArgs("SelectedItems"));

                     //assignrope = 
                     GetAssRope();
                     resetRopeSplicing();
              }
              public void resetRopeSplicing()
              {
                     try
                     {

                            erinfo = 0;
                            RopeSplicing = new RopeSplicingClass();
                            RaisePropertyChanged("RopeSplicing");

                            //AddMooringWinchRopeMessages = new AddMooringRopeErrorMessages();
                            //RaisePropertyChanged("AddMooringWinchRopeMessages");


                            SplicingDoneDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("SplicingDoneDate");
                            //InstalledDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("InstalledDate");
                            //ComboValue
                             ComboValue = null; RaisePropertyChanged("ComboValue");
                            SRopeType = null; RaisePropertyChanged("SRopeType");
                            //SManuFName = null; RaisePropertyChanged("SManuFName");
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              #region Bind Ropetype
              private static ObservableCollection<Ropetypecombo> ropetype = new ObservableCollection<Ropetypecombo>();
              public ObservableCollection<Ropetypecombo> RopeType
              {
                     get
                     {
                            return ropetype;
                     }
                     set
                     {
                            // var data = sc.AssignRopetoWinch.Where(x => x.Id == ropetype).FirstOrDefault();
                            ropetype = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
                     }
              }



              public void GetRopeType()
              {
                     ropetype.Clear();
                     //ObservableCollection<Ropetypecombo> AddRopeType = new ObservableCollection<Ropetypecombo>();
                     Ropetypecombo rop;
                     //SqlDataAdapter adp = new SqlDataAdapter("GetActiveAssignRopeType", sc.con);
                     SqlDataAdapter adp = new SqlDataAdapter("RopeBinding", sc.con);
                     adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                     adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                     DataTable ds = new DataTable();
                     adp.Fill(ds);

                     foreach (DataRow row in ds.Rows)
                     {
                            rop = new Ropetypecombo();
                            rop.Id = (int)row["Id"];
                            rop.CertificateNo = (string)row["certificatenumber"];
                            ropetype.Add(rop);
                     }
                     OnPropertyChanged(new PropertyChangedEventArgs("ropetype"));
                     // return AddRopeType;
              }

              //private ObservableCollection<Ropetypecombo> GetRopeType()
              //{
              //    ObservableCollection<Ropetypecombo> AddRopeType = new ObservableCollection<Ropetypecombo>();
              //    var data = sc.MooringWinchRope.Select(x => new { x.Id, x.CertificateNumber }).ToList();


              //    Ropetypecombo rop;
              //    foreach (var item in data)
              //    {

              //        rop = new Ropetypecombo();
              //        rop.Id = item.Id;
              //        rop.CertificateNo = item.CertificateNumber;
              //        AddRopeType.Add(rop);
              //    }

              //    return AddRopeType;
              //}

              #endregion







              public static RopeSplicingClass _ropesplicingClass = new RopeSplicingClass();

              public RopeSplicingClass RopeSplicing
              {
                     get
                     {

                            return _ropesplicingClass;
                     }
                     set
                     {
                            _ropesplicingClass = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("RopeSplicing"));
                     }
              }


              public static int erinfo { get; set; }
              private ICommand cancelCommand;
              public ICommand CancelCommand
              {
                     get { return cancelCommand; }
              }
              private ICommand resetCommand;
              public ICommand ResetCommand
              {
                     get { return resetCommand; }
              }

              private ICommand saveCommand;
              public ICommand SaveCommand
              {
                     get { return saveCommand; }
              }

              //private static MooringWinchRopeClass _AddMooringWinchRope = new MooringWinchRopeClass();
              //public CrewDetailClass AddCrewDetail
              //{

              //}


              #region Bind Properties

              public static Ropetypecombo sropetype;// = new Ropetypecombo();
              public Ropetypecombo SRopeType
              {
                     get
                     {

                            if (sropetype != null)
                            {
                                   var data = sc.AssignRopetoWinch.Where(x => x.RopeId == sropetype.Id && x.IsActive == true).FirstOrDefault();
                                   if (data != null)
                                   {
                                          var data1 = sc.MooringWinchRope.Where(x => x.Id == sropetype.Id && x.DeleteStatus == false).FirstOrDefault();
                                          RopeSplicing.RopeId = data.RopeId;
                                          //CrossShifting.OutboadEndinUse = data.Outboard;


                                   }
                                   else
                                   {
                                          RopeSplicing.RopeId = sropetype.Id;
                                   }
                                   //RopeEndToEnd.OutboardEndinUse = data1.Outboard;
                                   OnPropertyChanged(new PropertyChangedEventArgs("RopeSplicing"));
                            }
                            return sropetype;

                     }
                     set
                     {

                            sropetype = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("SRopeType"));

                            //var data = sc.AssignRopetoWinch.Where(x => x.RopeId == SRopeType.Id).FirstOrDefault();
                     }
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


              public static string _sdoneby;
              public string ComboValue
              {
                     //get
                     //{
                     //    if (_sropeass != null)
                     //    {                   

                     //        OnPropertyChanged(new PropertyChangedEventArgs("RopeSplicing"));
                     //    }
                     //    return _sropeass;
                     //}
                     //set
                     //{
                     //    _sropeass = value;
                     //    OnPropertyChanged(new PropertyChangedEventArgs("ComboValue"));
                     //}

                     get
                     {
                            if (_sdoneby != null)
                                   RopeSplicing.SplicingDoneBy = _sdoneby;


                            return _sdoneby;
                     }

                     set
                     {
                            _sdoneby = value;
                            if (_sdoneby != null)
                                   RopeSplicing.SplicingDoneBy = _sdoneby;
                            OnPropertyChanged(new PropertyChangedEventArgs("ComboValue"));
                     }
              }

              public void GetAssRope()
              {
                     // ObservableCollection<Winchcombo> AddWinchId = new ObservableCollection<Winchcombo>();
                     var data = sc.MooringWinch.Where(x=> x.IsActive==true).Select(x => new { x.Id, x.AssignedNumber }).ToList();
                     assignrope.Clear();
                     Winchcombo rop;
                     foreach (var item in data)
                     {

                            rop = new Winchcombo();
                            rop.Id = item.Id;
                            rop.AssignedNumber = item.AssignedNumber;
                            assignrope.Add(rop);
                     }

                     OnPropertyChanged(new PropertyChangedEventArgs("AssignRope"));
                     //return AddWinchId;
              }



              public static Nullable<DateTime> _SplicingDoneDate = null;
              public Nullable<DateTime> SplicingDoneDate
              {
                     get
                     {
                            if (_SplicingDoneDate == null)
                            {
                                   _SplicingDoneDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            }
                            _ropesplicingClass.SplicingDoneDate = (DateTime)_SplicingDoneDate;
                            return _SplicingDoneDate;
                     }
                     set
                     {
                            _SplicingDoneDate = value;
                            RaisePropertyChanged("SplicingDoneDate");
                     }
              }

        //public int NextNotiId()
        //{
        //    int notiid = 0;
        //    try
        //    {
        //        SqlDataAdapter adp = new SqlDataAdapter("SELECT IDENT_CURRENT('Notifications') + IDENT_INCR('Notifications') as Notiid", sc.con);
        //        DataTable dt = new DataTable();
        //        adp.Fill(dt);
        //         notiid = Convert.ToInt32( dt.Rows[0][0]);

        //        return notiid;

        //    }
        //    catch { return notiid; }

        //}  

        #endregion
        private void Saveropespl(RopeSplicingClass rpspc)
        {
            try
            {
                MainViewModelWorkHours.CommonValue = false;
                //var maxNotiid = sc.Notifications.Select(x => x.Id).Max();
                //int notiid = maxNotiid + 1;


                int notiid = sc.NextNotiId();

                if (rpspc.SplicingDoneBy == null || rpspc.SplicingMethod == null || rpspc.IsCropped==null)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please Choose all field", "Line Splicing", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                rpspc.RopeId = rpspc.RopeId;
                    rpspc.SplicingDoneBy = rpspc.SplicingDoneBy;
                    rpspc.SplicingDoneDate = rpspc.SplicingDoneDate;
                    rpspc.SplicingMethod = rpspc.SplicingMethod;

                    rpspc.CreatedDate = DateTime.Now;
                    rpspc.CreatedBy = "Admin";
                    rpspc.IsActive = true;
                    rpspc.RopeTail = 0;
                    rpspc.NotificationId = notiid;

                if (rpspc.IsCropped == "Yes")
                {
                    if (rpspc.CroppedOutboardEnd == null && rpspc.CroppedOutboardEnd1 == null)
                    {
                        MainViewModelWorkHours.CommonValue = true;
                        MessageBox.Show("Please choose outboardend", "Line Splicing", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (rpspc.ReasonofCropping == null || rpspc.ReasonofCropping == "")
                    {
                        MainViewModelWorkHours.CommonValue = true;
                        MessageBox.Show("Please Choose atleast 1 Reason of Cropping", "Line Splicing", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (rpspc.LengthofCroppedRope == null)
                    {
                        MainViewModelWorkHours.CommonValue = true;
                        MessageBox.Show("Please enter length of cropped rope", "Line Splicing", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                    sc.RopeSplicing.Add(rpspc);
                    sc.SaveChanges();



                ///======= Add in Rope Cropping Table
                ///

                if (rpspc.IsCropped == "Yes")
                {
                    var maxSplicedId = sc.RopeSplicing.DefaultIfEmpty().Max(r => r == null ? 1 : r.Id);
                    //var maxSplicedId = sc.RopeSplicing.Select(x => x.Id).Max();
                    RopeCroppingClass rpcrp = new RopeCroppingClass();
                    int notiid1 = sc.NextNotiId();
                    rpcrp.RopeId = rpspc.RopeId;
                    if (rpspc.CroppedOutboardEnd == "True")
                    {
                        rpcrp.CroppedOutboardEnd = "A";
                    }
                    if (rpspc.CroppedOutboardEnd1 == "True")
                    {
                        rpcrp.CroppedOutboardEnd = "B";
                    }
                    rpcrp.CreatedDate = DateTime.Now;
                    rpcrp.CreatedBy = "Admin";
                    rpcrp.IsActive = true;
                    rpcrp.CroppedDate = rpspc.SplicingDoneDate;
                    rpcrp.WinchId = rpspc.WinchId;
                    rpcrp.LengthofCroppedRope = rpspc.LengthofCroppedRope;
                    rpcrp.ReasonofCropping = rpspc.ReasonofCropping;
                    rpcrp.RopeTail = 0;
                    rpcrp.SplicedId = maxSplicedId;
                    rpcrp.NotificationId = notiid1;


                    sc.RopeCropping.Add(rpcrp);
                    sc.SaveChanges();
                }

                    ///  End




                    try
                    {
                        var mrRope = sc.MooringWinchRope.Where(x => x.DeleteStatus == false).ToList();
                        var winchid = sc.AssignRopetoWinch.Where(x => x.RopeId == rpspc.RopeId && x.IsActive == true).Select(x => x.WinchId).SingleOrDefault();
                        var length = sc.MooringWinchRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.Length).SingleOrDefault();
                        var percent = (length * 10) / 100;
                        var crplength = sc.RopeCropping.Where(x => x.RopeId == rpspc.RopeId && x.IsActive == true).Select(x => x.LengthofCroppedRope).Sum();
                        var ropetypeid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.RopeTypeId).SingleOrDefault();
                        var manufacid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.ManufacturerId).SingleOrDefault();
                    // var ropename = sc.MooringRopeType.Where(x => x.Id == ropetypeid).Select(x => x.RopeType).SingleOrDefault();
                    var ropename = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.CertificateNumber + " - "+ x.UniqueID).SingleOrDefault();
                    var WinchName = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.AssignedNumber).SingleOrDefault();
                        var WinchLocation = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Location).SingleOrDefault();


                        //if (crplength <= percent)
                        //{
                        //    var notification2 = "Cropped more than 10% - Rope " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                        //    NotificationsClass noti2 = new NotificationsClass();
                        //    noti2.Acknowledge = false;
                        //    noti2.AckRecord = "Not yet acknowledged";
                        //    noti2.Notification = notification2;
                        //    noti2.NotificationType = 2;
                        //    noti2.RopeId = rpspc.RopeId;
                        //    noti2.IsActive = true;
                        //    //noti.NotificationDueDate = DBNull.Value;
                        //    noti2.CreatedDate = DateTime.Now;
                        //    noti2.CreatedBy = "Admin";
                        //    sc.Notifications.Add(noti2);
                        //    sc.SaveChanges();
                        //}

                        if (rpspc.IsCropped == "Yes")
                        {
                            //if (crplength <= percent)
                            //{

                            //    var notificationc = "";
                            //    if (WinchName != null && WinchName != "" && WinchName != "Not Assigned")
                            //    {
                            //        notificationc = "Cropped more than 10% - Rope " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                            //    }
                            //    else
                            //    {
                            //        notificationc = "Cropped more than 10% - Rope " + ropename + "";
                            //    }


                            //    NotificationsClass notic = new NotificationsClass();
                            //    notic.Acknowledge = false;
                            //    notic.AckRecord = "Not yet acknowledged";
                            //    notic.Notification = notificationc;
                            //    notic.NotificationType = 1;
                            //    notic.RopeId = rpspc.RopeId;
                            //    notic.IsActive = true;
                            //    //noti.NotificationDueDate = DBNull.Value;
                            //    notic.CreatedDate = DateTime.Now;
                            //    notic.CreatedBy = "Admin";
                            //    notic.NotificationAlertType = (int)NotificationAlertType.Over_Cropping;
                            //    sc.Notifications.Add(notic);
                            //    sc.SaveChanges();
                            //}

                            if (crplength >= percent)
                            {
                                var notificationc = "";
                                if (WinchName != null && WinchName != "" && WinchName != "Not Assigned")
                                {
                                    notificationc = "Cropped more than 10% - Line " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                                }
                                else
                                {
                                    notificationc = "Cropped more than 10% - Line " + ropename + "";
                                }

                                NotificationsClass notic = new NotificationsClass();
                                notic.Acknowledge = false;
                                notic.AckRecord = "Not yet acknowledged";
                                notic.Notification = notificationc;
                                notic.NotificationType = 2;
                                notic.RopeId = rpspc.RopeId;
                                notic.IsActive = true;
                                //noti.NotificationDueDate = DBNull.Value;
                                notic.CreatedDate = DateTime.Now;
                                notic.CreatedBy = "Admin";
                                notic.NotificationAlertType = (int)NotificationAlertType.Over_Cropping;
                                sc.Notifications.Add(notic);
                                sc.SaveChanges();

                            StaticHelper.AlarmFunction(1, true);
                        }
                        }
                        var notification = "";
                        if (WinchName != null && WinchName != "" && WinchName != "Not Assigned")
                        {

                            notification = "Spliced - Line " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                        }
                        else
                        {
                            notification = "Spliced - Line " + ropename + "";
                        }
                        NotificationsClass noti = new NotificationsClass();
                        noti.Acknowledge = false;
                        noti.AckRecord = "Not yet acknowledged";
                        noti.Notification = notification;
                        noti.NotificationType = 1;
                        noti.RopeId = rpspc.RopeId;
                        noti.IsActive = true;
                        //noti.NotificationDueDate = DBNull.Value;
                        noti.CreatedDate = DateTime.Now;
                        noti.CreatedBy = "Admin";
                        sc.Notifications.Add(noti);
                        sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, true);

                    //var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == rpspc.RopeId && b.DeleteStatus == false && b.RopeTail == 0);
                    //var ss = sc.MooringWinchRope.Where(b => b.Id == rpspc.RopeId && b.DeleteStatus == false && b.RopeTail == 0).FirstOrDefault();
                    //var totalropelength = ss.Length;
                    //var ttlcrpdrp = ss.TtlCroppedLength;
                    //decimal ttl = 0;
                    //if (ttlcrpdrp == null)
                    //{
                    //    ttl = 0;
                    //}
                    //else
                    //{
                    //    ttl = Convert.ToDecimal(ttlcrpdrp);
                    //}

                    //if (result != null)
                    //{

                    //    result.TtlCroppedLength = rpspc.LengthofCroppedRope + ttl;
                    //    result.ModifiedDate = DateTime.Now;
                    //    sc.SaveChanges();
                    //}

                    //var percentcheck = (totalropelength * 10) / 100;
                    //if (result.TtlCroppedLength > percentcheck)
                    //{
                    //    var notification1 = "Cropped more than 10% - Rope " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                    //    //NotificationsClass noti = new NotificationsClass();
                    //    noti.Acknowledge = false;
                    //    noti.AckRecord = "Not yet acknowledged";
                    //    noti.Notification = notification1;
                    //    noti.NotificationType = 2;
                    //    noti.RopeId = rpspc.RopeId;
                    //    noti.IsActive = true;
                    //    //noti.NotificationDueDate = DBNull.Value;
                    //    noti.CreatedDate = DateTime.Now;
                    //    noti.CreatedBy = "Admin";
                    //    sc.Notifications.Add(noti);
                    //    sc.SaveChanges();
                    //}

                    MessageBox.Show("Record saved successfully ", "Line Splicing", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                    catch { }


                    CancelMooringWinch();

                //}
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



                     return AddDepartments;
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
              private void UpdateRopeSplicing(RopeSplicingClass moorwinch)
              {
                     try
                     {
                            //moorwinch.AssignedNumber = moorwinch.AssignedNumber != null ? moorwinch.AssignedNumber.Trim() : moorwinch.AssignedNumber;
                            //if (!string.IsNullOrEmpty(moorwinch.AssignedNumber))
                            //{

                            //    var findrank = sc.MooringWinch.Where(x => x.Id == moorwinch.Id).FirstOrDefault();

                            //    if (findrank != null)
                            //    {
                            //        moorwinch.AssignedNumber = textinfo.ToTitleCase(moorwinch.AssignedNumber.ToLower());



                            var local = sc.Set<RopeSplicingClass>()
                             .Local
                             .FirstOrDefault(f => f.Id == moorwinch.Id);
                            if (local != null)
                            {
                                   sc.Entry(local).State = EntityState.Detached;
                            }

                            var UpdatedRopedetails = new RopeSplicingClass()
                            {

                                   //    RopeEndtoEndClass etoe = new RopeEndtoEndClass();

                                   //etoe.CertificateNumber = comboRank.Text;
                                   //etoe.OutboardEndinUse = txtOutboard.Text == "A" ? true : false;
                                   //etoe.AssignedWinch = txtAssignedWinch.Text;
                                   //etoe.AssignedLocation = txtAssignedLocation.Text;
                                   //etoe.EndtoEndDoneDate = AddRopeEndtoEndViewModel._ReceivedDate;
                                   //etoe.CurrentOutboadEndinUse = txtOutboardcurrent.Text == "A" ? true : false;
                                   ////etoe.CertificateNumber = comboRank.SelectedValuePath;
                                   //etoe.CreatedDate = DateTime.Now;
                                   //etoe.CreatedBy = "Admin";
                                   //etoe.ModifiedDate = DateTime.Now;
                                   //etoe.ModifiedBy = "Admin";
                                   //etoe.IsActive = true;


                                   //sc.RopeEndtoEndTable.Add(etoe);
                                   //sc.SaveChanges();



                                   Id = moorwinch.Id,
                                   //CertificateNumber = moorwinch.CertificateNumber,



                                   ModifiedDate = DateTime.Now,
                                   IsActive = true,
                                   RopeTail = 0,

                                   CreatedBy = moorwinch.CreatedBy
                            };

                            sc.Entry(UpdatedRopedetails).State = EntityState.Modified;
                            sc.SaveChanges();


                            StaticHelper.Editing = false;
                            MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);



                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }


              private void EditMooringWinch(RopeSplicingClass moorwinch)
              {
                     try
                     {

                            var findrank = sc.MooringWinch.Where(x => x.Id == moorwinch.Id && x.IsActive == true).FirstOrDefault();
                            // AddMooringWinch.AssignedNumber = findrank.AssignedNumber;
                            // AddMooringWinch.Id = findrank.Id;
                            OnPropertyChanged(new PropertyChangedEventArgs("AddRopeDetail"));
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }


        }



        //public ICommand RadioBTNCommand { get; private set; }
        //private static AssignModuleToWinchClass _Assignmoduletowinch = new AssignModuleToWinchClass();
        //private void RadioBTNmethod(object parameter)
        //{
        //    var bb = (string)parameter;

        //    if (bb == "Outboard")
        //    {
        //        RopeSplicing.CroppedOutboardEnd = true;
        //        RopeSplicing.CroppedOutboardEnd1 = false;
        //        StaticHelper.Wathckeeping = true;
        //    }
        //    else
        //    {
        //        _Assignmoduletowinch.Outboard = false;
        //        _Assignmoduletowinch.Outboard1 = true;
        //        StaticHelper.Wathckeeping = false;
        //    }

        //    OnPropertyChanged(new PropertyChangedEventArgs("AssignRopeToWinch"));
        //}

        private void CancelMooringWinch()
              {
            MainViewModelWorkHours.CommonValue = false;
            var lostdata = new ObservableCollection<RopeSplicingClass>(sc.RopeSplicing.ToList());
                     RopeSplicingViewModel cc = new RopeSplicingViewModel(lostdata);

                     ChildWindowManager.Instance.CloseChildWindow();
              }


              new public event PropertyChangedEventHandler PropertyChanged;
              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }


       }
}
