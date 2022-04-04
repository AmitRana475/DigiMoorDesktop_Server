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

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class AddRopeDamageRecordViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;

        public AddRopeDamageRecordViewModel(RopeDamageRecordClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<RopeDamageRecordClass>(UpdateRopeDamage);
            cancelCommand = new RelayCommand(CancelMooringWinch);

            // MooringWinchRopeClass data = sc.MooringWinchRope.Where(x => x.Id == edeps.Id).FirstOrDefault();


            //SRopeType = edeps.RopeType;
            //SRopeConst = edeps.RopeConstruction;
            //SRopeDiameter = edeps.DiaMeter;
            //SRopeReasonoutofs = edeps.ReasonOutofService;

            //ReceivedDate = edeps.ReceivedDate;

            RopeDamage = new RopeDamageRecordClass()
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

            //RaisePropertyChanged("AddRopeEndtoEndView");
           // OnPropertyChanged(new PropertyChangedEventArgs("AddRopeEndtoEndView"));
            //EditMooringWinch(edeps);
        }
        public AddRopeDamageRecordViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<RopeDamageRecordClass>(Saveropespl);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            resetCommand = new RelayCommand(resetRopeDamage);
             GetRopeType();
            GetMooringOperation();
            assignrope = GetAssRope();
            resetRopeDamage();
        }
        public static MooringOperationCombo smoorop;// = new Ropetypecombo();
        public MooringOperationCombo Smoorop
        {
            get
            {

                if (smoorop != null)
                {

                    RopeDamage.MOPId = smoorop.OPId;





                    OnPropertyChanged(new PropertyChangedEventArgs("RopeDamage"));
                }
                return smoorop;

            }
            set
            {

                smoorop = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Smoorop"));

                //var data = sc.AssignRopetoWinch.Where(x => x.RopeId == SRopeType.Id).FirstOrDefault();
            }
        }

        private static Nullable<DateTime> _DamageDate = null;
        public Nullable<DateTime> DamageDate
        {
            get
            {
                if (_DamageDate == null)
                {
                    _DamageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                RopeDamage.DamageDate = (DateTime)_DamageDate;
                return _DamageDate;
            }
            set
            {
                _DamageDate = value;
                RaisePropertyChanged("DamageDate");
            }
        }

        private static ObservableCollection<MooringOperationCombo> moorOperationBind = new ObservableCollection<MooringOperationCombo>();
        public ObservableCollection<MooringOperationCombo> MoorOperationBind
        {
            get
            {
                return moorOperationBind;
            }
            set
            {
                moorOperationBind = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MoorOperationBind"));
            }
        }
        public void GetMooringOperation()
        {
            moorOperationBind.Clear();
            //ObservableCollection<MooringOperationCombo> AddMoorOP = new ObservableCollection<MooringOperationCombo>();
            MooringOperationCombo Op;
            SqlDataAdapter adp = new SqlDataAdapter("BindMooringOpertaion", sc.con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                Op = new MooringOperationCombo();
                Op.OPId = (int)row["OPId"];
                Op.Operation = (string)row["Operation"];
                moorOperationBind.Add(Op);
            }
            OnPropertyChanged(new PropertyChangedEventArgs("moorOperationBind"));
            //return moorOperationBind;
        }
        public class MooringOperationCombo
        {
            public int OPId { get; set; }
            public string Operation { get; set; }
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


        public void resetRopeDamage()
        {
            try
            {

                erinfo = 0;
                RopeDamage = new RopeDamageRecordClass();
                RaisePropertyChanged("RopeDamage");



                DamageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("DamageDate");

                
                ComboValue = null; RaisePropertyChanged("ComboValue");
                ComboValue1 = null; RaisePropertyChanged("ComboValue1");
                SRopeType = null; RaisePropertyChanged("SRopeType");
                Smoorop = null; RaisePropertyChanged("Smoorop"); 
                //SManuFName = null; RaisePropertyChanged("SManuFName");
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }




        public static RopeDamageRecordClass _ropedamageClass = new RopeDamageRecordClass();

        public RopeDamageRecordClass RopeDamage
        {
            get
            {

                return _ropedamageClass;
            }
            set
            {
                _ropedamageClass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RopeDamage"));
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
                        RopeDamage.RopeId = data.RopeId;
                        //CrossShifting.OutboadEndinUse = data.Outboard;


                    }
                    else
                    {
                        RopeDamage.RopeId = sropetype.Id;
                    }
                    //RopeEndToEnd.OutboardEndinUse = data1.Outboard;
                    OnPropertyChanged(new PropertyChangedEventArgs("RopeDamage"));
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


        public static string _sdamgeobs;
        public string ComboValue
        {
           
            get
            {
                if (_sdamgeobs != null)
                    RopeDamage.DamageObserved = _sdamgeobs;


                return _sdamgeobs;
            }

            set
            {
                _sdamgeobs = value;
                if (_sdamgeobs != null)
                    RopeDamage.DamageObserved = _sdamgeobs;
                    OnPropertyChanged(new PropertyChangedEventArgs("ComboValue"));
            }
        }

        public static string _sincrep;
        public string ComboValue1
        {

            get
            {
                if (_sincrep != null)
                    RopeDamage.IncidentReport = _sincrep;


                return _sincrep;
            }

            set
            {
                _sincrep = value;
                if (_sincrep != null)
                    RopeDamage.IncidentReport = _sincrep;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue1"));
            }
        }

        public static string _smoorop;
        //public string ComboValue2
        //{

        //    get
        //    {
        //        if (_smoorop != null)
        //            RopeDamage.MooringOperation = _smoorop;


        //        return _smoorop;
        //    }

        //    set
        //    {
        //        _smoorop = value;
        //        if (_smoorop != null)
        //            RopeDamage.MooringOperation = _smoorop;
        //        OnPropertyChanged(new PropertyChangedEventArgs("ComboValue2"));
        //    }
        //}
        public ObservableCollection<Winchcombo> GetAssRope()
        {
            ObservableCollection<Winchcombo> AddWinchId = new ObservableCollection<Winchcombo>();
            var data = sc.MooringWinch.Where(x=>x.IsActive==true).Select(x => new { x.Id, x.AssignedNumber }).ToList();

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



        //public static Nullable<DateTime> _SplicingDoneDate = null;
        //public Nullable<DateTime> SplicingDoneDate
        //{
        //    get
        //    {
        //        if (_SplicingDoneDate == null)
        //        {
        //            _SplicingDoneDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        //        }
        //        _ropesplicingClass.SplicingDoneDate = (DateTime)_SplicingDoneDate;
        //        return _SplicingDoneDate;
        //    }
        //    set
        //    {
        //        _SplicingDoneDate = value;
        //        RaisePropertyChanged("SplicingDoneDate");
        //    }
        //}


        #endregion
        private void Saveropespl(RopeDamageRecordClass rpspc)
        {
            try
            {
                MainViewModelWorkHours.CommonValue = false;
                //var maxNotiid = sc.Notifications.Select(x => x.Id).Max();
                //int notiid = maxNotiid + 1;

                int notiid = sc.NextNotiId();

                if (rpspc.DamageObserved==null)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please Choose Damage Observed ", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (rpspc.DamageLocation == null || rpspc.DamageLocation=="--Select--")
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please Choose Damage Location ", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (rpspc.DamageReason == null || rpspc.DamageReason == "--Select--")
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please Choose Damage Reason ", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (rpspc.IncidentReport == null)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please Choose Incident Report ", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (rpspc.DamageObserved == "Mooring Operation")
                {
                    if (rpspc.MOPId == null)
                    {
                        MainViewModelWorkHours.CommonValue = true;
                        MessageBox.Show("Please Choose Mooring Operation ", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                rpspc.RopeId = rpspc.RopeId;
                //rpspc.SplicingDoneBy = rpspc.SplicingDoneBy;
                //rpspc.SplicingDoneDate = rpspc.SplicingDoneDate;
                //rpspc.SplicingMethod = rpspc.SplicingMethod;

                rpspc.CreatedDate = DateTime.Now;
                rpspc.CreatedBy = "Admin";
                rpspc.IsActive = true;
                rpspc.RopeTail = 0;
                rpspc.NotificationId = notiid;

                rpspc.DamageLocation = rpspc.DamageLocation;               
                rpspc.DamageReason = rpspc.DamageReason;
                rpspc.MOPId = rpspc.MOPId;

                if(rpspc.MOPId != null)
                {
                    var datemop = sc.MOperationBirthDetailTbl.Where(x => x.OPId == rpspc.MOPId).Select(x => x.FastDatetime).SingleOrDefault();

                    rpspc.DamageDate = datemop;
                }

                sc.RopeDamage.Add(rpspc);
                sc.SaveChanges();
                MessageBox.Show("Record saved successfully ", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Information);

                
                try
                {
                    var mrRope = sc.MooringWinchRope.Where(x => x.DeleteStatus == false).ToList();
                    var winchid = sc.AssignRopetoWinch.Where(x => x.RopeId == rpspc.RopeId && x.IsActive == true).Select(x => x.WinchId).SingleOrDefault();
                    //var length = sc.MooringWinchRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.Length).SingleOrDefault();
                    //var percent = (length * 10) / 100;
                    //var crplength = sc.RopeCropping.Where(x => x.RopeId == rpspc.RopeId).Select(x => x.LengthofCroppedRope).Sum();
                    var ropetypeid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.RopeTypeId).SingleOrDefault();
                    var manufacid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.ManufacturerId).SingleOrDefault();
                    //var ropename = sc.MooringRopeType.Where(x => x.Id == ropetypeid).Select(x => x.RopeType).SingleOrDefault();
                    var ropename = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.CertificateNumber + " - " + x.UniqueID).SingleOrDefault();
                    var WinchName = sc.MooringWinch.Where(x => x.Id == winchid).Select(x => x.AssignedNumber).SingleOrDefault();
                    var WinchLocation = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Location).SingleOrDefault();

                    var notification = "";
                    if (WinchName != null && WinchName != "" && WinchName != "Not Assigned")
                    {

                        notification = "Damaged - Line " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                    }
                    else
                    {
                        notification = "Damaged - Line " + ropename + "";
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
                    noti.NotificationAlertType = (int)NotificationAlertType.RopeDamage;
                    sc.Notifications.Add(noti);
                    sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, true);
                }
                catch { }



                CancelMooringWinch();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void UpdateRopeDamage(RopeDamageRecordClass moorwinch)
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

                    CreatedDate = DateTime.Now,

                    ModifiedDate = DateTime.Now,
                    IsActive = true,

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


        private void EditRopedamage(RopeDamageRecordClass moorwinch)
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
            var lostdata = new ObservableCollection<RopeDamageRecordClass>(sc.RopeDamage.ToList());
            RopeDamageRecordViewModel cc = new RopeDamageRecordViewModel(lostdata);

            ChildWindowManager.Instance.CloseChildWindow();
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
