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

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class AddRopeCroppingViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;

              public AddRopeCroppingViewModel(RopeCroppingClass edeps)
              {
                     StaticHelper.Editing = true;

                     sc = new ShipmentContaxt();
                     sc.Configuration.ProxyCreationEnabled = false;

                     saveCommand = new RelayCommand<RopeCroppingClass>(UpdateRopeSplicing);
                     cancelCommand = new RelayCommand(CancelMooringWinch);



                     RopeCropping = new RopeCroppingClass()
                     {
                            Id = edeps.Id,
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


              }
              public AddRopeCroppingViewModel()
              {
                     StaticHelper.Editing = true;

                     sc = new ShipmentContaxt();
                     sc.Configuration.ProxyCreationEnabled = false;

                     saveCommand = new RelayCommand<RopeCroppingClass>(Saveropespl);
                     cancelCommand = new RelayCommand(CancelMooringWinch);
                     resetCommand = new RelayCommand(resetRopeCropping);
                     GetRopeType();
                     assignrope = GetAssRope();

                     Items = CroppingReason();
                     OnPropertyChanged(new PropertyChangedEventArgs("Items"));

                     //SelectedItems = CroppingReason();
                     //SelectedItems.Add("All", "0");
                     //OnPropertyChanged(new PropertyChangedEventArgs("SelectedItems"));

                     resetRopeCropping();
              }

              public void resetRopeCropping()
              {
                     try
                     {

                            erinfo = 0;
                            RopeCropping = new RopeCroppingClass();
                            RaisePropertyChanged("RopeCropping");



                            CroppedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("CroppedDate");


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
                     //return AddRopeType;
              }
              #endregion







              public static RopeCroppingClass _ropecroppingClass = new RopeCroppingClass();

              public  RopeCroppingClass RopeCropping
              {
                     get
                     {

                            return _ropecroppingClass;
                     }
                     set
                     {
                            _ropecroppingClass = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("RopeCropping"));
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
                                   var data = sc.AssignRopetoWinch.Where(x => x.RopeId == sropetype.Id).FirstOrDefault();
                                   if (data != null)
                                   {
                                          var data1 = sc.MooringWinchRope.Where(x => x.Id == sropetype.Id && x.DeleteStatus == false).FirstOrDefault();
                                          RopeCropping.RopeId = data.RopeId;
                                          //CrossShifting.OutboadEndinUse = data.Outboard;


                                   }
                                   else
                                   {
                                          RopeCropping.RopeId = sropetype.Id;
                                   }
                                   //RopeEndToEnd.OutboardEndinUse = data1.Outboard;
                                   OnPropertyChanged(new PropertyChangedEventArgs("RopeCroppingClass"));
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
                            //  if (_sdoneby != null)
                            //RopeSplicing.SplicingDoneBy = _sdoneby;


                            return _sdoneby;
                     }

                     set
                     {
                            // _sdoneby = value;
                            //if (_sdoneby != null)
                            // .SplicingDoneBy = _sdoneby;
                            OnPropertyChanged(new PropertyChangedEventArgs("ComboValue"));
                     }
              }

              public ObservableCollection<Winchcombo> GetAssRope()
              {
                     ObservableCollection<Winchcombo> AddWinchId = new ObservableCollection<Winchcombo>();
                     var data = sc.MooringWinch.Where(x=> x.IsActive==true).Select(x => new { x.Id, x.AssignedNumber }).ToList();

                     Winchcombo rop;
                     foreach (var item in data)
                     {

                            rop = new Winchcombo();
                            rop.Id = item.Id;
                            rop.AssignedNumber = item.AssignedNumber;
                            AddWinchId.Add(rop);
                     }

                     return AddWinchId;
              }



              public static Nullable<DateTime> _CroppedDate = null;
              public Nullable<DateTime> CroppedDate
              {
                     get
                     {
                            if (_CroppedDate == null)
                            {
                                   _CroppedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            }
                            _ropecroppingClass.CroppedDate = (DateTime)_CroppedDate;
                            return _CroppedDate;
                     }
                     set
                     {
                            _CroppedDate = value;
                            RaisePropertyChanged("CroppedDate");
                     }
              }


        #endregion
        private void Saveropespl(RopeCroppingClass rpspc)
        {
            try
            {
                MainViewModelWorkHours.CommonValue = false;
                //var maxNotiid = sc.Notifications.Select(x => x.Id).Max();
                //int notiid = maxNotiid + 1;

                int notiid = sc.NextNotiId();

                rpspc.RopeId = rpspc.RopeId;

                if (rpspc.CroppedOutboardEnd == "True")
                {
                    rpspc.CroppedOutboardEnd = "A";
                }
                if (rpspc.CroppedOutboardEnd1 == "True")
                {
                    rpspc.CroppedOutboardEnd = "B";
                }

                rpspc.CreatedDate = DateTime.Now;
                rpspc.CreatedBy = "Admin";
                rpspc.IsActive = true;
                rpspc.RopeTail = 0;
                rpspc.NotificationId = notiid;
                if (rpspc.LengthofCroppedRope == null)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please enter length of cropped line", "Cropped Line", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (rpspc.ReasonofCropping == null || rpspc.ReasonofCropping == "")
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please Choose atleast 1 Reason.", "Cropped Line", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                sc.RopeCropping.Add(rpspc);
                sc.SaveChanges();
                MessageBox.Show("Record saved successfully ", "Line Cropping", MessageBoxButton.OK, MessageBoxImage.Information);

                try
                {
                    var mrRope = sc.MooringWinchRope.Where(x => x.DeleteStatus == false & x.Id == rpspc.RopeId).FirstOrDefault();
                    var winchid = sc.AssignRopetoWinch.Where(x => x.RopeId == rpspc.RopeId && x.IsActive == true).Select(x => x.WinchId).SingleOrDefault();
                    //var length = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.Length).SingleOrDefault();
                    var length = mrRope.Length;
                    var percent = (length * 10) / 100;
                    var crplength = sc.RopeCropping.Where(x => x.RopeId == rpspc.RopeId && x.IsActive == true).Select(x => x.LengthofCroppedRope).Sum();

                    //var ropetypeid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.RopeTypeId).SingleOrDefault();
                    var ropetypeid = mrRope.RopeTypeId;
                    //var manufacid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.ManufacturerId).SingleOrDefault();
                    var manufacid = mrRope.ManufacturerId;
                    //var ropename = sc.MooringRopeType.Where(x => x.Id == ropetypeid).Select(x => x.RopeType).SingleOrDefault();
                    var ropename = mrRope.CertificateNumber;
                    var uniqueid = mrRope.UniqueID;
                    var winchDetail = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).FirstOrDefault();
                    string WinchName = ""; string WinchLocation = " (Not Assigned)";
                    if (winchDetail != null)
                    {
                        WinchName = winchDetail.AssignedNumber;
                        WinchLocation = winchDetail.Location;
                    }


                    //if (crplength < percent)
                    //{
                    //    var notification = "";
                    //    if (WinchName != null && WinchName != "" && WinchName != "Not Assigned")
                    //    {

                    //        notification = "Cropped more than 10% - Rope " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                    //    }
                    //    else
                    //    {
                    //        notification = "Cropped more than 10% - Rope " + ropename + "";
                    //    }
                    //    NotificationsClass noti = new NotificationsClass();
                    //    noti.Acknowledge = false;
                    //    noti.AckRecord = "Not yet acknowledged";
                    //    noti.Notification = notification;
                    //    noti.NotificationType = 1;
                    //    noti.RopeId = rpspc.RopeId;
                    //    noti.IsActive = true;
                    //    //noti.NotificationDueDate = DBNull.Value;
                    //    noti.CreatedDate = DateTime.Now;
                    //    noti.CreatedBy = "Admin";
                    //    noti.NotificationAlertType = (int)NotificationAlertType.Over_Cropping;
                    //    sc.Notifications.Add(noti);
                    //    sc.SaveChanges();
                    //}

                    if (crplength >= percent)
                    {
                        var notification = "";
                        if (WinchName != null)
                        {

                            notification = "Cropped more than 10% - Line " + ropename + " - "+uniqueid+" on winch " + WinchName + " located at " + WinchLocation + "";
                        }
                        else
                        {
                            notification = "Cropped more than 10% - Line " + ropename + " - "+uniqueid+"";
                        }
                        NotificationsClass noti = new NotificationsClass();
                        noti.Acknowledge = false;
                        noti.AckRecord = "Not yet acknowledged";
                        noti.Notification = notification;
                        noti.NotificationType = 2;
                        noti.RopeId = rpspc.RopeId;
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
                catch (Exception es) { }

                CancelMooringWinch();

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


              private void UpdateRopeSplicing(RopeCroppingClass moorwinch)
              {
                     try
                     {

                            var local = sc.Set<RopeCroppingClass>().Local.FirstOrDefault(f => f.Id == moorwinch.Id);
                            if (local != null)
                            {
                                   sc.Entry(local).State = EntityState.Detached;
                            }

                            var UpdatedRopedetails = new RopeCroppingClass()
                            {
                                   Id = moorwinch.Id,
                                   //CertificateNumber = moorwinch.CertificateNumber,
                                   ModifiedDate = DateTime.Now,
                                CreatedDate = DateTime.Now,
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


              private void EditMooringWinch(RopeCroppingClass moorwinch)
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

              private void CancelMooringWinch()
              {
            MainViewModelWorkHours.CommonValue = false;
                     var lostdata = new ObservableCollection<RopeCroppingClass>(sc.RopeCropping.ToList());
                     RopeCroppingListViewModel cc = new RopeCroppingListViewModel(lostdata);

                     ChildWindowManager.Instance.CloseChildWindow();
              }


              new public event PropertyChangedEventHandler PropertyChanged;
              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }


       }
}
