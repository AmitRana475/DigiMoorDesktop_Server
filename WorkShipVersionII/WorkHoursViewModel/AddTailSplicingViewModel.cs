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
    public class AddTailSplicingViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;

        public AddTailSplicingViewModel(RopeSplicingClass edeps)
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
        public AddTailSplicingViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<RopeSplicingClass>(Saveropespl);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            resetCommand = new RelayCommand(resetRopeSplicing);

            Items = CroppingReason();
            OnPropertyChanged(new PropertyChangedEventArgs("Items"));

            //SelectedItems = CroppingReason();
            //SelectedItems.Add("All", "0");
            //OnPropertyChanged(new PropertyChangedEventArgs("SelectedItems"));

            GetRopeType();

            assignrope = GetAssRope();
            resetRopeSplicing();
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
           // ObservableCollection<Ropetypecombo> AddRopeType = new ObservableCollection<Ropetypecombo>();
            Ropetypecombo rop;
            //SqlDataAdapter adp = new SqlDataAdapter("GetActiveAssignRopeType", sc.con);
            SqlDataAdapter adp = new SqlDataAdapter("RopeBinding", sc.con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 1);
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
                    var data = sc.AssignRopetoWinch.Where(x => x.RopeId == sropetype.Id && x.IsActive==true && x.RopeTail==1).FirstOrDefault();
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


        #endregion
        private void Saveropespl(RopeSplicingClass rpspc)
        {
            try
            {
                MainViewModelWorkHours.CommonValue = false;
                //rpspc.RopeId = rpspc.RopeId;
                //rpspc.SplicingDoneBy = rpspc.SplicingDoneBy;
                //rpspc.SplicingDoneDate = rpspc.SplicingDoneDate;
                ////rpspc.SplicingMethod = rpspc.SplicingMethod;

                //rpspc.CreatedDate = DateTime.Now;
                //rpspc.CreatedBy = "Admin";
                //rpspc.IsActive = true;
                //rpspc.RopeTail = 1;

                //sc.RopeSplicing.Add(rpspc);
                //sc.SaveChanges();
              

                    int notiid = sc.NextNotiId();

                if (rpspc.SplicingDoneBy == null || rpspc.SplicingMethod == null || rpspc.IsCropped == null)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please Choose all field", "RopeTail Splicing", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                rpspc.RopeId = rpspc.RopeId;
                    rpspc.SplicingDoneBy = rpspc.SplicingDoneBy;
                    rpspc.SplicingDoneDate = rpspc.SplicingDoneDate;
                    rpspc.SplicingMethod = rpspc.SplicingMethod;

                    rpspc.CreatedDate = DateTime.Now;
                    rpspc.CreatedBy = "Admin";
                    rpspc.IsActive = true;
                    rpspc.RopeTail = 1;
                    rpspc.NotificationId = notiid;


                if (rpspc.IsCropped == "Yes")
                {
                    if (rpspc.IsCropped == "Yes")
                    {
                        //if (rpspc.CroppedOutboardEnd == null && rpspc.CroppedOutboardEnd1 == null)
                        //{
                        //    MainViewModelWorkHours.CommonValue = true;
                        //    MessageBox.Show("Please choose outboardend", "Cropped Rope", MessageBoxButton.OK, MessageBoxImage.Warning);
                        //    return;
                        //}
                        if (rpspc.ReasonofCropping == null || rpspc.ReasonofCropping == "")
                        {
                            MainViewModelWorkHours.CommonValue = true;
                            MessageBox.Show("Please Choose atleast 1 Reason of Cropping", "RopeTail Splicing", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        if (rpspc.LengthofCroppedRope == null)
                        {
                            MainViewModelWorkHours.CommonValue = true;
                            MessageBox.Show("Please enter length of cropped rope", "RopeTail Splicing", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                }
                    sc.RopeSplicing.Add(rpspc);
                    sc.SaveChanges();



                ///======= Add in Rope Cropping Table
                ///



                if (rpspc.IsCropped == "Yes")
                {
                    //var maxSplicedId = sc.RopeSplicing.Select(x => x.Id).Max();
                    var maxSplicedId = sc.RopeSplicing.DefaultIfEmpty().Max(r => r == null ? 1 : r.Id);


                    RopeCroppingClass rpcrp = new RopeCroppingClass();

                    int notiid1 = sc.NextNotiId();
                    rpcrp.RopeId = rpspc.RopeId;

                    rpcrp.WinchId = rpspc.WinchId;
                    rpcrp.CreatedDate = DateTime.Now;
                    rpcrp.CreatedBy = "Admin";
                    rpcrp.IsActive = true;
                    rpcrp.CroppedDate = rpspc.SplicingDoneDate;
                    rpcrp.LengthofCroppedRope = rpspc.LengthofCroppedRope;
                    rpcrp.ReasonofCropping = rpspc.ReasonofCropping;
                    rpcrp.RopeTail = 1;
                    rpcrp.SplicedId = maxSplicedId;
                    rpcrp.NotificationId = notiid1;
                   

                    sc.RopeCropping.Add(rpcrp);
                    sc.SaveChanges();

                    var mrRope = sc.MooringWinchRope.Where(x => x.RopeTail == 1 && x.DeleteStatus == false).ToList();
                    var winchid = sc.AssignRopetoWinch.Where(x => x.RopeId == rpspc.RopeId && x.IsActive == true && x.RopeTail == 1).Select(x => x.WinchId).SingleOrDefault();
                    var length = sc.MooringWinchRope.Where(x => x.Id == rpspc.RopeId && x.RopeTail == 1 && x.DeleteStatus == false).Select(x => x.Length).SingleOrDefault();
                    var percent = (length * 10) / 100;
                    var crplength = sc.RopeCropping.Where(x => x.RopeId == rpspc.RopeId && x.RopeTail == 1).Select(x => x.LengthofCroppedRope).Sum();
                    var ropetypeid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.RopeTypeId).SingleOrDefault();
                    var manufacid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.ManufacturerId).SingleOrDefault();
                    //var ropename = sc.MooringRopeType.Where(x => x.Id == ropetypeid).Select(x => x.RopeType).SingleOrDefault();	
                    var ropename = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.CertificateNumber + " - "+ x.UniqueID).SingleOrDefault();
                    var WinchName = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.AssignedNumber).SingleOrDefault();
                    var WinchLocation = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Location).SingleOrDefault();
                    if (crplength >= percent)
                    {
                        var notification = "";
                        if (WinchName != null)
                        {
                            notification = "Cropped more than 10% - RopeTail " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                        }
                        else
                        {
                            notification = "Cropped more than 10% - RopeTail " + ropename + "";
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
                //}


                MessageBox.Show("Record saved successfully ", "RopeTail Splicing", MessageBoxButton.OK, MessageBoxImage.Information);

                    try
                    {
                        var mrRope = sc.MooringWinchRope.Where(x => x.DeleteStatus == false).ToList();
                        var winchid = sc.AssignRopetoWinch.Where(x => x.RopeId == rpspc.RopeId && x.IsActive == true).Select(x => x.WinchId).SingleOrDefault();
                    //var length = sc.MooringWinchRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.Length).SingleOrDefault();
                    //var percent = (length * 10) / 100;
                    //var crplength = sc.RopeCropping.Where(x => x.RopeId == rpspc.RopeId).Select(x => x.LengthofCroppedRope).Sum();

                    var ropetypeid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.RopeTypeId).SingleOrDefault();


                    //var ropetypeid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.RopeTypeId).SingleOrDefault();
                        var manufacid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.ManufacturerId).SingleOrDefault();
                    //var ropename = sc.MooringRopeType.Where(x => x.Id == ropetypeid).Select(x => x.RopeType).SingleOrDefault();
                    var ropename = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.CertificateNumber + " - "+ x.UniqueID).SingleOrDefault();
                    var WinchName = sc.MooringWinch.Where(x => x.Id == winchid).Select(x => x.AssignedNumber).SingleOrDefault();
                        var WinchLocation = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Location).SingleOrDefault();

                        var notification = "";
                        if (WinchName != null && WinchName != "Not Assigned")
                        {
                            notification = "Spliced - RopeTail " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                        }
                        else
                        {
                            notification = "Spliced - RopeTail " + ropename + "";
                        }

                        // notification = "Spliced - RopeTail " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
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
                    noti.NotificationAlertType = (int)NotificationAlertType.RopeSplicing;
                    sc.Notifications.Add(noti);
                        sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, true);

                }

                    catch (Exception ex) { }


                    CancelMooringWinch();


               // }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
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
                    RopeTail = 1,

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

        private void CancelMooringWinch()
        {
            MainViewModelWorkHours.CommonValue = false;
            var lostdata = new ObservableCollection<RopeSplicingClass>(sc.RopeSplicing.ToList());
            TailSplicingViewModel cc = new TailSplicingViewModel(lostdata);

            ChildWindowManager.Instance.CloseChildWindow();
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }
}
