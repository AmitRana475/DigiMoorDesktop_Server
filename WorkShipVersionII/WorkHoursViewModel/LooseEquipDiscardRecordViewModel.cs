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
    public class LooseEquipDiscardRecordViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ICommand HelpCommand { get; private set; }
        public LooseEquipDiscardRecordViewModel(LooseEDamageRecordClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<LooseEDamageRecordClass>(UpdateRopeDamage);
            cancelCommand = new RelayCommand(CancelMooringWinch);


            LooseEtypecombo abc = new LooseEtypecombo()
            {
                Id = edeps.Id,
                LooseEquipmentType = edeps.LooseEtype
            };
            SRopeType = abc;
            GetMooringOperation();


            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

            RopeDamage = new LooseEquipDiscardClass();
            RaisePropertyChanged("RopeDamage");
        }
        public LooseEquipDiscardRecordViewModel()
        {
            RopeDamage = new LooseEquipDiscardClass();
            RaisePropertyChanged("RopeDamage");



            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

            OutofServiceDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("OutofServiceDate");

            OutofServiceReason = null; RaisePropertyChanged("OutofServiceReason");
            SRopeType = null; RaisePropertyChanged("SRopeType");
            AssignRope = null; RaisePropertyChanged("AssignRope");

            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<LooseEquipDiscardClass>(Saveropespl);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            looseEtype = GetLooseEType();

            GetMooringOperation();
            assignrope = GetAssRope();
            outofsreason = GetOutofSReason();


        }

        public void refreshform()
        {
            RopeDamage = new LooseEquipDiscardClass();
            RaisePropertyChanged("RopeDamage");

            OutofServiceDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("OutofServiceDate");

            SRopeReasonoutofs = null; RaisePropertyChanged("SRopeReasonoutofs");


            ComboValue = null; RaisePropertyChanged("ComboValue");
            Smoorop = null; RaisePropertyChanged("Smoorop");
            // LooseEType = null; RaisePropertyChanged("LooseEType");
            ComboValue2 = null; RaisePropertyChanged("ComboValue2");
            SRopeType = null; RaisePropertyChanged("SRopeType");


        }
        #region BindMoorOperation



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

        #endregion

        private static Nullable<DateTime> _OutofServiceDate = null;
        public Nullable<DateTime> OutofServiceDate
        {
            get
            {
                if (_OutofServiceDate == null)
                {
                    _OutofServiceDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                _ropedamageClass.OutofServiceDate = (DateTime)_OutofServiceDate;
                return _OutofServiceDate;
            }
            set
            {
                _OutofServiceDate = value;
                RaisePropertyChanged("OutofServiceDate");
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

        //private static ObservableCollection<string> looseEtype = new ObservableCollection<string>();
        //public ObservableCollection<string> LooseEType
        //{
        //    get
        //    {
        //        return looseEtype;
        //    }
        //    set
        //    {
        //        looseEtype = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("LooseEType"));
        //    }
        //}
        private static ObservableCollection<LooseEtypecombo> looseEtype = new ObservableCollection<LooseEtypecombo>();
        public ObservableCollection<LooseEtypecombo> LooseEType
        {
            get
            {
                return looseEtype;
            }
            set
            {
                looseEtype = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LooseEType"));
            }
        }
        private static int slooseEtype;
        public int SLooseEType
        {
            get
            {
                if (slooseEtype != 0)
                    RopeDamage.LooseETypeId = slooseEtype;
                //if (slooseEtype == "MASTER")
                //    AddMooringWinchRope.RopeType = "All";

                return slooseEtype;
            }

            set
            {
                slooseEtype = value;
                if (slooseEtype != 0)
                    RopeDamage.LooseETypeId = slooseEtype;
                OnPropertyChanged(new PropertyChangedEventArgs("LooseEType"));
            }
        }
        public ObservableCollection<LooseEtypecombo> GetLooseEType()
        {
            ObservableCollection<LooseEtypecombo> AddLooseEType = new ObservableCollection<LooseEtypecombo>();
            //var AddLooseEType = new ObservableCollection<string>();
            // var data = sc.LooseETypes.OrderBy(s => s.Id).Select(x => x.LooseEquipmentType).ToList();

            var data = sc.LooseETypes.Where(x => x.Id != 2).Select(x => new { x.Id, x.LooseEquipmentType }).ToList();


            LooseEtypecombo rop;
            foreach (var item in data)
            {

                rop = new LooseEtypecombo();
                rop.Id = item.Id;
                rop.LooseEquipmentType = item.LooseEquipmentType;
                AddLooseEType.Add(rop);
            }


            //foreach (var item in data)
            //{
            //    AddLooseEType.Add(item);

            //}

            return AddLooseEType;
        }

        private ObservableCollection<Ropetypecombo> GetRopeType()
        {
            ObservableCollection<Ropetypecombo> AddRopeType = new ObservableCollection<Ropetypecombo>();
            var data = sc.MooringWinchRope.Where(x => x.DeleteStatus == false).Select(x => new { x.Id, x.CertificateNumber }).ToList();
            Ropetypecombo rop;
            foreach (var item in data)
            {
                rop = new Ropetypecombo();
                rop.Id = item.Id;
                rop.CertificateNo = item.CertificateNumber;
                AddRopeType.Add(rop);
            }
            return AddRopeType;
        }

        #endregion





        private static ObservableCollection<string> outofsreason = new ObservableCollection<string>();
        public ObservableCollection<string> OutofServiceReason
        {
            get
            {
                return outofsreason;
            }
            set
            {
                outofsreason = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OutofServiceReason"));
            }
        }

        public ObservableCollection<string> GetOutofSReason()
        {
            var AddoutofSReason = new ObservableCollection<string>();
            var data = sc.OutofserviceReason.OrderBy(s => s.Id).Select(x => x.Reason).ToList();

            foreach (var item in data)
            {
                AddoutofSReason.Add(item);

            }

            return AddoutofSReason;
        }

        private static string srreasonoutofservice;
        public string SRopeReasonoutofs
        {
            get
            {
                if (srreasonoutofservice != null)
                    RopeDamage.ReasonOutofService = srreasonoutofservice;
                if (srreasonoutofservice == "MASTER")
                    RopeDamage.ReasonOutofService = "All";

                return srreasonoutofservice;
            }

            set
            {
                srreasonoutofservice = value;
                if (srreasonoutofservice != null)
                    RopeDamage.ReasonOutofService = srreasonoutofservice;
                OnPropertyChanged(new PropertyChangedEventArgs("ReasonOutofService"));
            }
        }

        public static LooseEquipDiscardClass _ropedamageClass = new LooseEquipDiscardClass();

        public LooseEquipDiscardClass RopeDamage
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

        public void Refreshmethod()
        {
            RopeDamage = new LooseEquipDiscardClass();
            RaisePropertyChanged("RopeDamage");


            SRopeReasonoutofs = null; RaisePropertyChanged("SRopeReasonoutofs");
            ComboValue = null; RaisePropertyChanged("ComboValue");
            Smoorop = null; RaisePropertyChanged("Smoorop");


        }

        public static int erinfo { get; set; }
        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
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

        public static LooseEtypecombo sropetype;// = new Ropetypecombo();
        public LooseEtypecombo SRopeType
        {
            get
            {

                if (sropetype != null)
                {
                    //var data = sc.AssignRopetoWinch.Where(x => x.RopeId == sropetype.Id).FirstOrDefault();
                    //if (data != null)
                    //{
                    //    var data1 = sc.MooringWinchRope.Where(x => x.Id == sropetype.Id).FirstOrDefault();
                    //    RopeDamage.LooseETypeId = data.RopeId;
                    //}


                    var data1 = sc.LooseETypes.Where(x => x.Id == sropetype.Id).FirstOrDefault();
                    RopeDamage.LooseETypeId = data1.Id;

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

        //public static string _sincrep;
        //public string ComboValue1
        //{

        //    get
        //    {
        //        if (_sincrep != null)
        //            RopeDamage.IncidentReport = _sincrep;


        //        return _sincrep;
        //    }

        //    set
        //    {
        //        _sincrep = value;
        //        if (_sincrep != null)
        //            RopeDamage.IncidentReport = _sincrep;
        //        OnPropertyChanged(new PropertyChangedEventArgs("ComboValue1"));
        //    }
        //}

        public static string _smoorop;
        public string ComboValue2
        {

            get
            {
                if (_smoorop != null)
                    RopeDamage.MooringOperation = _smoorop;


                return _smoorop;
            }

            set
            {
                _smoorop = value;
                if (_smoorop != null)
                    RopeDamage.MooringOperation = _smoorop;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue2"));
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
        private void Saveropespl(LooseEquipDiscardClass rpspc)
        {
            try
            {
                MainViewModelWorkHours.CommonValue = false;

                if (rpspc.LooseETypeId == 1)
                {
                    var local = sc.Set<JoiningShackleClass>()
                     .Local
                     .FirstOrDefault(f => f.Id == rpspc.Id);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }
                }
                else if (rpspc.LooseETypeId == 5)
                {
                    var local = sc.Set<ChainStopperClass>()
                        .Local
                        .FirstOrDefault(f => f.Id == rpspc.Id);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }
                }
                else if(rpspc.LooseETypeId == 7)
                {
                    var local = sc.Set<ChafeGuardClass>()
                     .Local
                     .FirstOrDefault(f => f.Id == rpspc.Id);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }
                }
                else if(rpspc.LooseETypeId == 8)
                {
                    var local = sc.Set<WBTestKitClass>()
                        .Local
                        .FirstOrDefault(f => f.Id == rpspc.Id);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }
                }
                else
                {
                    var local = sc.Set<RopeTailClass>()
                         .Local
                         .FirstOrDefault(f => f.Id == rpspc.Id);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }
                }

                if (rpspc.ReasonOutofService == null)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please Select Discard Reason", "LooseE Discard", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (rpspc.DamageObserved == "Mooring Operation")
                {
                    if (rpspc.MOPId == null)
                    {
                        MainViewModelWorkHours.CommonValue = true;
                        MessageBox.Show("Please Select Mooring Operation", "LooseE Discard", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                if (rpspc.LooseETypeId == 1)
                {
                    rpspc.LooseETypeId = rpspc.LooseETypeId;
                    //var result = sc.JoiningShackles.SingleOrDefault(b => b.LooseETypeId == rpspc.LooseETypeId && b.CertificateNumber==rpspc.CertificateNumber);
                    var result = sc.JoiningShackles.SingleOrDefault(b => b.LooseETypeId == rpspc.LooseETypeId && b.Id == rpspc.Id);
                    if (result != null)
                    {
                        result.OutofServiceDate = rpspc.OutofServiceDate;
                        result.ReasonOutofService = rpspc.ReasonOutofService;
                        // result.CertificateNumber = rpspc.CertificateNumber;
                        result.OtherReason = rpspc.OtherReason;
                        result.ModifiedBy = "Admin";

                        result.ModifiedDate = DateTime.Now;
                        sc.Entry(result).State = EntityState.Modified;
                        sc.SaveChanges();
                    }
                }
                else if (rpspc.LooseETypeId == 5)
                {
                    rpspc.LooseETypeId = rpspc.LooseETypeId;
                    //var result = sc.ChainStoppers.SingleOrDefault(b => b.LooseETypeId == rpspc.LooseETypeId && b.CertificateNumber == rpspc.CertificateNumber);
                    var result = sc.ChainStoppers.SingleOrDefault(b => b.LooseETypeId == rpspc.LooseETypeId && b.Id == rpspc.Id);
                    if (result != null)
                    {
                        result.OutofServiceDate = rpspc.OutofServiceDate;
                        result.ReasonOutofService = rpspc.ReasonOutofService;
                        // result.CertificateNumber = rpspc.CertificateNumber;
                        result.OtherReason = rpspc.OtherReason;
                        result.ModifiedBy = "Admin";

                        result.ModifiedDate = DateTime.Now;
                        sc.Entry(result).State = EntityState.Modified;
                        sc.SaveChanges();
                    }
                }
                else if (rpspc.LooseETypeId == 7)
                {
                    rpspc.LooseETypeId = rpspc.LooseETypeId;
                    //var result = sc.ChafeGuard.SingleOrDefault(b =>  b.CertificateNumber == rpspc.CertificateNumber);
                    var result = sc.ChafeGuard.SingleOrDefault(b => b.Id == rpspc.Id);
                    if (result != null)
                    {
                        result.OutofServiceDate = rpspc.OutofServiceDate;
                        result.OtherReason = rpspc.OtherReason;
                        result.ReasonOutofService = rpspc.ReasonOutofService;
                        //result.CertificateNumber = rpspc.CertificateNumber;   
                        sc.Entry(result).State = EntityState.Modified;
                        sc.SaveChanges();
                    }
                }
                else if (rpspc.LooseETypeId == 8)
                {
                    rpspc.LooseETypeId = rpspc.LooseETypeId;
                    //var result = sc.WBTestKit.SingleOrDefault(b => b.CertificateNumber == rpspc.CertificateNumber);
                    var result = sc.WBTestKit.SingleOrDefault(b => b.Id == rpspc.Id);
                    if (result != null)
                    {
                        result.OutofServiceDate = rpspc.OutofServiceDate;
                        result.OtherReason = rpspc.OtherReason;
                        result.ReasonOutofService = rpspc.ReasonOutofService;
                        // result.CertificateNumber = rpspc.CertificateNumber;  
                        sc.Entry(result).State = EntityState.Modified;
                        sc.SaveChanges();
                    }
                }
                else
                {
                    rpspc.LooseETypeId = rpspc.LooseETypeId;
                    //var result = sc.RopeTails.SingleOrDefault(b => b.LooseETypeId == rpspc.LooseETypeId && b.CertificateNumber == rpspc.CertificateNumber);
                    var result = sc.RopeTails.SingleOrDefault(b => b.LooseETypeId == rpspc.LooseETypeId && b.Id == rpspc.Id);
                    if (result != null)
                    {
                        result.OutofServiceDate = rpspc.OutofServiceDate;
                        result.ReasonOutofService = rpspc.ReasonOutofService;
                        //result.CertificateNumber = rpspc.CertificateNumber;
                        result.OtherReason = rpspc.OtherReason;
                        result.ModifiedBy = "Admin";

                        result.ModifiedDate = DateTime.Now;
                        sc.Entry(result).State = EntityState.Modified;
                        sc.SaveChanges();
                    }
                }

                if (rpspc.ReasonOutofService == "Damaged")
                {
                    LooseEDamageRecordClass rpdm = new LooseEDamageRecordClass();
                    rpdm.LooseETypeId = rpspc.LooseETypeId;
                    rpdm.DamageObserved = rpspc.DamageObserved;
                    rpdm.MOpId = rpspc.MOPId;
                    rpdm.CertificateNumber = rpspc.CertificateNumber;
                    rpdm.CreatedDate = DateTime.Now;
                    rpdm.DamageDate = DateTime.Now;
                    rpdm.CreatedBy = "Admin";
                    rpdm.IsActive = true;
                    sc.LooseEDamageR.Add(rpdm);
                    sc.SaveChanges();

                    int getid = sc.LooseEDamageR.Max(x => x.Id);

                    var looseE = sc.LooseEDamageR.Where(x => x.Id == getid && x.LooseETypeId == rpspc.LooseETypeId).FirstOrDefault();

                    var looseEname = sc.LooseETypes.Where(x => x.Id == looseE.LooseETypeId).Select(x => x.LooseEquipmentType).SingleOrDefault();

                    var notification = "Damaged - Loose Equipment " + looseEname + " CertificateNo- " + looseE.CertificateNumber + "";

                    NotificationsClass noti = new NotificationsClass();
                    noti.Acknowledge = false;
                    noti.AckRecord = "Not yet acknowledged";
                    noti.Notification = notification;
                    noti.NotificationType = 1;
                    noti.RopeId = 0;
                    noti.IsActive = true;
                    //noti.NotificationDueDate = DBNull.Value;
                    noti.CreatedDate = DateTime.Now;
                    noti.CreatedBy = "Admin";
                    noti.LooseCertificateNum = looseE.CertificateNumber ;
                    noti.LooseEqType = rpspc.LooseETypeId;
                    sc.Notifications.Add(noti);
                    sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, true);
                }

                MessageBox.Show("Record updated successfully ", "LooseE Discard", MessageBoxButton.OK, MessageBoxImage.Information);

                LooseE_Notification(rpspc.LooseETypeId, rpspc.Id);
            }



            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        public void LooseE_Notification(int looseETypeId, int id)
        {
            try
            {

                if (looseETypeId == 1)
                {
                    var looseE = sc.JoiningShackles.Where(x => x.Id == id && x.LooseETypeId == looseETypeId).FirstOrDefault();
                                     
                    var looseEname = sc.LooseETypes.Where(x => x.Id == looseE.LooseETypeId).Select(x => x.LooseEquipmentType).SingleOrDefault();
                                     
                   var notification = "Out of Service / discarded - Loose Equipment " + looseEname + " CertificateNo- " + looseE.CertificateNumber + " - "+ looseE.UniqueID + "";
                   
                    NotificationsClass noti = new NotificationsClass();
                    noti.Acknowledge = false;
                    noti.AckRecord = "Not yet acknowledged";
                    noti.Notification = notification;
                    noti.NotificationType = 1;
                    noti.RopeId = 0;
                    noti.IsActive = true;
                   
                    //noti.NotificationDueDate = DBNull.Value;
                    noti.NotificationAlertType = (int)NotificationAlertType.JoiningShackle_LooseEquipmentDisCard;
                    noti.CreatedDate = DateTime.Now;
                    noti.CreatedBy = "Admin";
                    noti.LooseCertificateNum = looseE.CertificateNumber + " - " + looseE.UniqueID; 
                    noti.LooseEqType = looseETypeId;
                    sc.Notifications.Add(noti);
                    sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, true);
                }
                else if (looseETypeId == 5)
                {


                  
                    var looseE = sc.ChainStoppers.Where(x => x.Id == id && x.LooseETypeId == looseETypeId).FirstOrDefault();

                    var looseEname = sc.LooseETypes.Where(x => x.Id == looseE.LooseETypeId).Select(x => x.LooseEquipmentType).SingleOrDefault();

                    var notification = "Out of Service / discarded - Loose Equipment " + looseEname + " CertificateNo- " + looseE.CertificateNumber + " - " + looseE.UniqueID + "";

                    NotificationsClass noti = new NotificationsClass();
                    noti.Acknowledge = false;
                    noti.AckRecord = "Not yet acknowledged";
                    noti.Notification = notification;
                    noti.NotificationType = 1;
                    noti.RopeId = 0;
                    noti.IsActive = true;
                    //noti.NotificationDueDate = DBNull.Value;
                    noti.NotificationAlertType = (int)NotificationAlertType.ChainStopper_LooseEquipmentDisCard;
                    noti.CreatedDate = DateTime.Now;
                    noti.CreatedBy = "Admin";
                    noti.LooseCertificateNum = looseE.CertificateNumber + " - " + looseE.UniqueID;
                    noti.LooseEqType = looseETypeId;
                    sc.Notifications.Add(noti);
                    sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, true);
                }
                else if (looseETypeId == 7)
                {

                    var looseE = sc.ChafeGuard.Where(x => x.Id == id).FirstOrDefault();

                    var looseEname = sc.LooseETypes.Where(x => x.Id == looseETypeId).Select(x => x.LooseEquipmentType).SingleOrDefault();

                    var notification = "Out of Service / discarded - Loose Equipment " + looseEname + " CertificateNo- " + looseE.CertificateNumber + " - " + looseE.UniqueID + "";
                   
                    NotificationsClass noti = new NotificationsClass();
                    noti.Acknowledge = false;
                    noti.AckRecord = "Not yet acknowledged";
                    noti.Notification = notification;
                    noti.NotificationType = 1;
                    noti.RopeId = 0;
                    noti.IsActive = true;
                    //noti.NotificationDueDate = DBNull.Value;
                    noti.NotificationAlertType = (int)NotificationAlertType.ChainStopper_LooseEquipmentDisCard;
                    noti.CreatedDate = DateTime.Now;
                    noti.CreatedBy = "Admin";
                    noti.LooseCertificateNum = looseE.CertificateNumber + " - " + looseE.UniqueID;
                    noti.LooseEqType = looseETypeId;
                    sc.Notifications.Add(noti);
                    sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, true);

                }
                else if (looseETypeId == 8)
                {



                    var looseE = sc.WBTestKit.Where(x => x.Id == id).FirstOrDefault();

                    var looseEname = sc.LooseETypes.Where(x => x.Id == looseETypeId).Select(x => x.LooseEquipmentType).SingleOrDefault();

                    var notification = "Out of Service / discarded - Loose Equipment " + looseEname + " CertificateNo- " + looseE.CertificateNumber + " - " + looseE.UniqueID + "";

                    NotificationsClass noti = new NotificationsClass();
                    noti.Acknowledge = false;
                    noti.AckRecord = "Not yet acknowledged";
                    noti.Notification = notification;
                    noti.NotificationType = 1;
                    noti.RopeId = 0;
                    noti.IsActive = true;
                    //noti.NotificationDueDate = DBNull.Value;
                    noti.NotificationAlertType = (int)NotificationAlertType.ChainStopper_LooseEquipmentDisCard;
                    noti.CreatedDate = DateTime.Now;
                    noti.CreatedBy = "Admin";
                    noti.LooseCertificateNum = looseE.CertificateNumber + " - " + looseE.UniqueID; 
                    noti.LooseEqType = looseETypeId;
                    sc.Notifications.Add(noti);
                    sc.SaveChanges();


                    StaticHelper.AlarmFunction(1, true);
                }
                else
                {
                  
                    var looseE = sc.RopeTails.Where(x => x.Id == id && x.LooseETypeId == looseETypeId).FirstOrDefault();

                    var looseEname = sc.LooseETypes.Where(x => x.Id == looseE.LooseETypeId).Select(x => x.LooseEquipmentType).SingleOrDefault();

                    var notification = "Out of Service / discarded - Loose Equipment " + looseEname + " CertificateNo- " + looseE.CertificateNumber + " - " + looseE.UniqueID + "";

                    int Typeid = looseE.LooseETypeId == 7 ? (int)NotificationAlertType.ChafeGuard_LooseEquipmentDisCard : looseE.LooseETypeId == 8 ? (int)NotificationAlertType.WinchBreakTest_LooseEquipmentDisCard : (int)NotificationAlertType.RopeStopper_FireWire_Messanger_LooseEquipmentDisCard;

                    NotificationsClass noti = new NotificationsClass();
                    noti.Acknowledge = false;
                    noti.AckRecord = "Not yet acknowledged";
                    noti.Notification = notification;
                    noti.NotificationType = 1;
                    noti.RopeId = 0;
                    noti.IsActive = true;
                    //noti.NotificationDueDate = DBNull.Value;
                    noti.NotificationAlertType = Typeid;
                    noti.CreatedDate = DateTime.Now;
                    noti.CreatedBy = "Admin";
                    noti.LooseCertificateNum = looseE.CertificateNumber + " - " + looseE.UniqueID; 
                    noti.LooseEqType = looseETypeId;
                    sc.Notifications.Add(noti);
                    sc.SaveChanges();


                    StaticHelper.AlarmFunction(1, true);
                }
            }
            catch (Exception ex) { }
        }
        private void UpdateRopeDamage(LooseEDamageRecordClass moorwinch)
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
            var lostdata = new ObservableCollection<LooseEDamageRecordClass>(sc.LooseEDamageR.ToList());
            LooseEDamageRecordListViewModel cc = new LooseEDamageRecordListViewModel(lostdata);

            ChildWindowManager.Instance.CloseChildWindow();
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}

