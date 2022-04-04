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
    public class AddChainStopperViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddChainStopperViewModel(ChainStopperClass edeps)
        {
            //IsEnabledCheck = false;
            IsEnabledCheck = true;
            StaticHelper.Editing = true;
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            saveCommand = new RelayCommand<ChainStopperClass>(UpdateChainStopper);
            cancelCommand = new RelayCommand(CancelChainStoper);

            // MooringWinchRopeClass data = sc.MooringWinchRope.Where(x => x.Id == edeps.Id).FirstOrDefault();

            AddChainStopper = new ChainStopperClass()
            {
                Id = edeps.Id,
                LooseETypeId = edeps.LooseETypeId,
                ManufactureName = edeps.ManufactureName,
                CertificateNumber = edeps.CertificateNumber,
                MBL = edeps.MBL,
                Length = edeps.Length,
                DateReceived = edeps.DateReceived,
                DateInstalled = edeps.DateInstalled,
                OutofServiceDate = edeps.OutofServiceDate,

                ReasonOutofService = edeps.ReasonOutofService,
                OtherReason = edeps.OtherReason,
                DamagedObserved = edeps.DamagedObserved,
                Remarks = edeps.Remarks,
                MOpId = edeps.MOpId,
                IsActive = true,
                ModifiedDate = DateTime.Now,
                ModifiedBy = "Admin",
                UniqueID = edeps.UniqueID,
            };

            //SLooseEType = edeps.RopeType;
            //SRopeConst = edeps.RopeConstruction;
            //SRopeDiameter = edeps.DiaMeter;
            //SRopeReasonoutofs = edeps.ReasonOutofService;
            //SRopeMooringOpertaion = edeps.MooringOperation;
            //SRopeDamageObserved = edeps.DamageObserved;
            //ReceivedDate = edeps.ReceivedDate;
            //InstalledDate = edeps.InstalledDate;
            //OutofServiceDate = edeps.OutofServiceDate;
            var tesst = Convert.ToDateTime(edeps.DateInstalled);
            if (tesst.ToString() == "01/01/0001 00:00:00" || tesst == null)
            {
                InstalledDate = DateTime.Now;
            }
            else
            {
                InstalledDate = edeps.DateInstalled;
            }

            if (edeps.InstalledDate1 == null)
            {
                InstalledDate = DateTime.Now;
            }
            OutofServiceDate = edeps.OutofServiceDate;


            RaisePropertyChanged("AddChainStopper");
            OnPropertyChanged(new PropertyChangedEventArgs("AddChainStopper"));
            //EditMooringWinch(edeps);
        }

        public AddChainStopperViewModel(int id)
       // public AddChainStopperViewModel()
        {
            IsEnabledCheck = true;
            StaticHelper.Editing = true;

           // StaticHelper.Id = id;
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<ChainStopperClass>(SaveChainStopper);
            cancelCommand = new RelayCommand(CancelChainStoper);
            looseEtype = GetLooseEType();
            manufname = GetManuFName();
            outofsreason = GetOutofSReason();
            damageobserved = GetDamageObserved();

            GetMooringOperation();
            AddChainStopperMessages = new AddChainStopperErrorMessages();
            RaisePropertyChanged("AddChainStopperMessages");
        }

 

        public void resetform()
        {
            IsEnabledCheck = true;
            AddChainStopperMessages = new AddChainStopperErrorMessages();
            RaisePropertyChanged("AddChainStopperMessages");

            _AddChainStopperMessages = new AddChainStopperErrorMessages();
            RaisePropertyChanged("_AddChainStopperMessages");

            AddChainStopper = new ChainStopperClass();
            RaisePropertyChanged("AddChainStopper");

            ReceivedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("ReceivedDate");
            InstalledDate = null; RaisePropertyChanged("InstalledDate");

            DamageObserved = null; RaisePropertyChanged("DamageObserved");
            SRopeDamageObserved = null; RaisePropertyChanged("SRopeDamageObserved");
            Smoorop = null; RaisePropertyChanged("Smoorop");
            ComboValue = null; RaisePropertyChanged("ComboValue");
            ComboValue2 = null; RaisePropertyChanged("ComboValue2");
            //SRopeType = null; RaisePropertyChanged("SRopeType");
            SManuFName = null; RaisePropertyChanged("SManuFName");

        }
        #region BindMoorOperation



        public static MooringOperationCombo smoorop;// = new Ropetypecombo();
        public MooringOperationCombo Smoorop
        {
            get
            {

                if (smoorop != null)
                {

                    AddChainStopper.MOpId = smoorop.OPId;





                    OnPropertyChanged(new PropertyChangedEventArgs("AddChainStopper"));
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

        #region Bind ManuFName
        private static ObservableCollection<string> manufname = new ObservableCollection<string>();
        public ObservableCollection<string> ManuFName
        {
            get
            {
                return manufname;
            }
            set
            {
                manufname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ManuFName"));
            }
        }

        private ObservableCollection<string> GetManuFName()
        {
            var AddManuFname = new ObservableCollection<string>();
            var data = sc.CommonManuF.OrderBy(s => s.Id).Select(x => x.Name).ToList();

            foreach (var item in data)
            {
                AddManuFname.Add(item);

            }

            return AddManuFname;
        }


        private static string sropeManuFName;
        public string SManuFName
        {
            get
            {
                if (sropeManuFName != null)
                    AddChainStopper.ManufactureName = sropeManuFName;
                if (sropeManuFName == "MASTER")
                    AddChainStopper.ManufactureName = "All";

                return sropeManuFName;
            }

            set
            {
                sropeManuFName = value;
                if (sropeManuFName != null)
                    AddChainStopper.ManufactureName = sropeManuFName;
                OnPropertyChanged(new PropertyChangedEventArgs("SManuFName"));
            }
        }

        #endregion
        private string mooringWinchMessage;
        public string MooringWinchMessage
        {
            get { return mooringWinchMessage; }
            set { mooringWinchMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("MooringWinchMessage")); }
        }

        #region Bind LooseEtype
        private static ObservableCollection<string> looseEtype = new ObservableCollection<string>();
        public ObservableCollection<string> LooseEType
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

        private ObservableCollection<string> GetLooseEType()
        {
            var AddLooseEType = new ObservableCollection<string>();
            var data = sc.LooseETypes.OrderBy(s => s.Id).Select(x => x.LooseEquipmentType).ToList();

            foreach (var item in data)
            {
                AddLooseEType.Add(item);

            }

            return AddLooseEType;
        }

        #endregion

        #region Bind OutofServiceReason
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

        private ObservableCollection<string> GetOutofSReason()
        {
            var AddoutofSReason = new ObservableCollection<string>();
            var data = sc.OutofserviceReason.OrderBy(s => s.Id).Select(x => x.Reason).ToList();

            foreach (var item in data)
            {
                AddoutofSReason.Add(item);

            }

            return AddoutofSReason;
        }

        #endregion

        #region Bind DamageObserved
        private static ObservableCollection<string> damageobserved = new ObservableCollection<string>();
        public ObservableCollection<string> DamageObserved
        {
            get
            {
                return damageobserved;
            }
            set
            {
                damageobserved = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DamageObserved"));
            }
        }

        private ObservableCollection<string> GetDamageObserved()
        {
            var Adddamageobserved = new ObservableCollection<string>();
            var data = sc.DamageObserved.OrderBy(s => s.Id).Select(x => x.DamageObserved).ToList();

            foreach (var item in data)
            {
                Adddamageobserved.Add(item);

            }

            return Adddamageobserved;
        }

        #endregion
        public static ChainStopperClass _AddChainStopper = new ChainStopperClass();
        public ChainStopperClass AddChainStopper
        {
            get
            {

                return _AddChainStopper;
            }
            set
            {
                _AddChainStopper = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddChainStopper"));
            }
        }

        private static AddChainStopperErrorMessages _AddChainStopperMessages = new AddChainStopperErrorMessages();
        public AddChainStopperErrorMessages AddChainStopperMessages
        {
            get
            {
                return _AddChainStopperMessages;
            }
            set
            {
                _AddChainStopperMessages = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddChainStopperMessages"));
            }
        }
        public class AddChainStopperErrorMessages
        {
            public string RopeTypeMessage { get; set; }
            public string RopeConstructionMessage { get; set; }
            public string DiaMeterMessage { get; set; }
            public string LengthMessage { get; set; }
            public string MBLMessage { get; set; }
            public string LDBFMessage { get; set; }
            public string WLLMessage { get; set; }
            public string ManufactureNameMessage { get; set; }
            public string CertificateNoMessage { get; set; }
            public string UniqueIDMessage { get; set; }
            public string ReceivedDateMessage { get; set; }
            // public string InstalledDateMessage { get; set; }
            public string RopeTaggingMessage { get; set; }
            public string ReasonoutofServiceMessage { get; set; }

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

        //private static string slooseEtype;
        //public string SLooseEType
        //{
        //    get
        //    {
        //        if (slooseEtype != null)
        //            AddMooringWinchRope.RopeType = slooseEtype;
        //        if (slooseEtype == "MASTER")
        //            AddMooringWinchRope.RopeType = "All";

        //        return slooseEtype;
        //    }

        //    set
        //    {
        //        slooseEtype = value;
        //        if (slooseEtype != null)
        //            AddMooringWinchRope.RopeType = slooseEtype;
        //        OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
        //    }
        //}

        //private static string _sropeconsts;
        //public string SRopeConst
        //{
        //    get
        //    {
        //        if (_sropeconsts != null)
        //            AddMooringWinchRope.RopeConstruction = _sropeconsts;
        //        if (_sropeconsts == "MASTER")
        //            AddMooringWinchRope.RopeConstruction = "All";

        //        return _sropeconsts;
        //    }

        //    set
        //    {
        //        _sropeconsts = value;
        //        if (_sropeconsts != null)
        //            AddChainStopper.RopeConstruction = _sropeconsts;
        //        OnPropertyChanged(new PropertyChangedEventArgs("RopeConstruction"));
        //    }
        //}
        //private static string sropediameter;
        //public string SRopeDiameter
        //{
        //    get
        //    {
        //        if (sropediameter != null)
        //            AddMooringWinchRope.DiaMeter = sropediameter;
        //        if (sropediameter == "MASTER")
        //            AddMooringWinchRope.DiaMeter = "All";

        //        return sropediameter;
        //    }

        //    set
        //    {
        //        sropediameter = value;
        //        if (sropediameter != null)
        //            AddMooringWinchRope.DiaMeter = sropediameter;
        //        OnPropertyChanged(new PropertyChangedEventArgs("DiaMeter"));
        //    }
        //}

        private static string srreasonoutofservice;
        public string SRopeReasonoutofs
        {
            get
            {
                if (srreasonoutofservice != null)
                    AddChainStopper.ReasonOutofService = srreasonoutofservice;


                return srreasonoutofservice;
            }

            set
            {
                srreasonoutofservice = value;
                if (srreasonoutofservice != null)
                    AddChainStopper.ReasonOutofService = srreasonoutofservice;
                OnPropertyChanged(new PropertyChangedEventArgs("ReasonOutofService"));
            }
        }

        private static string sdamageobserved;
        public string SRopeDamageObserved
        {
            get
            {
                if (sdamageobserved != null)
                    AddChainStopper.DamagedObserved = sdamageobserved;
                if (sdamageobserved == "MASTER")
                    AddChainStopper.DamagedObserved = "All";

                return sdamageobserved;
            }

            set
            {
                sdamageobserved = value;
                if (sdamageobserved != null)
                    AddChainStopper.DamagedObserved = sdamageobserved;
                OnPropertyChanged(new PropertyChangedEventArgs("DamagedObserved"));
            }
        }

        private static string smooringoperation;
        public string SRopeMooringOpertaion
        {
            get
            {
                if (smooringoperation != null)
                    AddChainStopper.MooringOperation = smooringoperation;
                if (smooringoperation == "MASTER")
                    AddChainStopper.MooringOperation = "All";

                return smooringoperation;
            }

            set
            {
                smooringoperation = value;
                if (smooringoperation != null)
                    AddChainStopper.MooringOperation = smooringoperation;
                OnPropertyChanged(new PropertyChangedEventArgs("MooringOperation"));
            }
        }

        private static Nullable<DateTime> _ReceivedDate = null;
        public Nullable<DateTime> ReceivedDate
        {
            get
            {
                if (_ReceivedDate == null)
                {
                    _ReceivedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                _AddChainStopper.DateReceived = (DateTime)_ReceivedDate;
                return _ReceivedDate;
            }
            set
            {
                _ReceivedDate = value;
                RaisePropertyChanged("ReceivedDate");
            }
        }

        private bool _isenabledcheck = true;

        public bool IsEnabledCheck
        {
            get { return _isenabledcheck; }
            set
            {
                _isenabledcheck = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsEnabledCheck"));


            }
        }
        //private static Nullable<DateTime> _InstallDate = null;
        //public Nullable<DateTime> InstalledDate
        //{
        //    get
        //    {
        //        if (_InstallDate == null)
        //        {
        //            _AddChainStopper.DateInstalled = null;
        //            //_InstallDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        //        }
        //        else
        //        {
        //            _AddChainStopper.DateInstalled = (DateTime)_InstallDate;
        //        }
        //        return _InstallDate;
        //    }
        //    set
        //    {
        //        _InstallDate = value;
        //        RaisePropertyChanged("InstalledDate");
        //    }
        //}


        private static Nullable<DateTime> _InstallDate = null;
        public Nullable<DateTime> InstalledDate
        {
            get
            {
                if (_InstallDate == null)
                {
                    _AddChainStopper.DateInstalled = null;
                    //_InstallDate  = null;
                    _InstallDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                else
                {
                    _AddChainStopper.DateInstalled = (DateTime)_InstallDate;
                }
                return _InstallDate;
            }
            set
            {
                _InstallDate = value;
                RaisePropertyChanged("InstalledDate");
            }
        }
        private static Nullable<DateTime> _OutofServiceDate = null;
        public Nullable<DateTime> OutofServiceDate
        {
            get
            {
                if (_OutofServiceDate == null)
                {
                    _OutofServiceDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                _AddChainStopper.OutofServiceDate = (DateTime)_OutofServiceDate;
                return _InstallDate;
            }
            set
            {
                _OutofServiceDate = value;
                RaisePropertyChanged("OutofServiceDate");
            }
        }
        #endregion
        private void SaveChainStopper(ChainStopperClass svchainst)
        {
            try
            {
                refreshmessage1(svchainst);
                //moorwinchrope.AssignedNumber = moorwinchrope.AssignedNumber != null ? moorwinchrope.AssignedNumber.Trim() : moorwinch.AssignedNumber;
                //if (!string.IsNullOrEmpty(moorwinchrope.AssignedNumber))
                //{
                //    var findassno = sc.MooringWinch.Where(x => x.AssignedNumber == moorwinchrope.AssignedNumber).FirstOrDefault();

                //    if (findassno == null)
                //    {

                if (!string.IsNullOrEmpty(Lblmessage))
                    CheckErrorMessage.CheckErrorMessages = false;

                if (CheckErrorMessage.CheckErrorMessages)
                {
                    var duplicatecheck = sc.ChainStoppers.Where(x => x.UniqueID == svchainst.UniqueID).Count();
                    if (duplicatecheck == 0)
                    {

                        //var ss = sc.ChainStoppers.SingleOrDefault(b => b.LooseETypeId == 1 && b.CertificateNumber == svchainst.CertificateNumber);
                        //if (ss != null)
                        //{
                        //    MessageBox.Show("Certificate number already exist  ", "Add Loose Eq", MessageBoxButton.OK, MessageBoxImage.Information);

                        //}
                        //else
                        //{
                        //moorwinchrope.RopeType = textinfo.ToTitleCase(moorwinchrope.RopeType.ToLower());
                        //moorwinchrope.RopeConstruction = textinfo.ToTitleCase(moorwinchrope.RopeConstruction.ToLower());
                        //moorwinchrope.DiaMeter = textinfo.ToTitleCase(moorwinchrope.DiaMeter.ToLower());
                        //svchainst.LooseETypeId = StaticHelper.Id;
                        svchainst.LooseETypeId = 5;
                        svchainst.Length = Convert.ToDecimal(svchainst.Length);
                        svchainst.MBL = Convert.ToDecimal(svchainst.MBL);
                        //moorwinchrope.LDBF = Convert.ToDecimal(moorwinchrope.LDBF);
                        //moorwinchrope.WLL = Convert.ToDecimal(moorwinchrope.WLL);
                        svchainst.ManufactureName = svchainst.ManufactureName;
                        svchainst.CertificateNumber = svchainst.CertificateNumber;
                        svchainst.UniqueID = svchainst.UniqueID;
                        svchainst.DateReceived = Convert.ToDateTime(svchainst.DateReceived);
                        //svchainst.DateInstalled = Convert.ToDateTime(svchainst.DateInstalled);


                        if (svchainst.IsRopeInstalled == "No")
                        {
                            svchainst.DateInstalled = null;
                        }
                        //if (svchainst.DateInstalled == null)
                        //{
                        //    svchainst.DateInstalled = null;
                        //}

                        //moorwinchrope.RopeTagging = textinfo.ToTitleCase(moorwinchrope.RopeTagging.ToLower());
                        //svchainst.OutofServiceDate = Convert.ToDateTime(svchainst.OutofServiceDate);
                        svchainst.OutofServiceDate = null;
                        //svchainst.DateInstalled = null;
                        //svchainst.ReasonOutofService = textinfo.ToTitleCase(svchainst.ReasonOutofService.ToLower());

                        if (svchainst.OtherReason != null)
                        {
                            svchainst.OtherReason = svchainst.OtherReason;
                        }
                        if (svchainst.DamagedObserved != null)
                        {
                            svchainst.DamagedObserved = svchainst.DamagedObserved;
                        }
                        if (svchainst.MooringOperation != null)
                        {
                            svchainst.MooringOperation = svchainst.MooringOperation;
                        }
                        svchainst.CreatedDate = DateTime.Now;
                        svchainst.CreatedBy = "Admin";
                        svchainst.IsActive = true;
                        svchainst.DeleteStatus = false;


                        RopeDamageRecordClass rpdm = new RopeDamageRecordClass();


                        try
                        {
                            if (svchainst.DateInstalled != null)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType=" + 5 + "", sc.con);
                                DataTable dt = new DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    //decimal rating1 = Convert.ToDecimal(dt.Rows[0]["MaximumMonthsAllowed"]);
                                    //int rat = Convert.ToInt32(rating1);

                                    decimal datecheck = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]);
                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    //DateTime inspectduedate = Convert.ToDateTime(svchainst.DateInstalled).AddDays(near);

                                    int firstValue = 0; int secondValue = 0; DateTime nextMonth;
                                    double value = Convert.ToDouble(datecheck);
                                    string a = value.ToString();
                                    string[] b = a.Split('.');
                                    firstValue = int.Parse(b[0]);
                                    if (b.Length == 2)
                                    {
                                        secondValue = int.Parse(b[1]);
                                    }
                                    int chekint = Convert.ToInt32(datecheck);
                                    DateTime date = Convert.ToDateTime(svchainst.DateInstalled);
                                    if (secondValue == 0)
                                    {
                                        nextMonth = date.AddMonths(chekint);
                                    }
                                    else
                                    {
                                        if (chekint != 0)
                                        {
                                            nextMonth = date.AddMonths(chekint).AddDays(-15);
                                        }
                                        else
                                        {
                                            nextMonth = date.AddDays(15);
                                        }
                                    }


                                    //int chekint = Convert.ToInt32(datecheck);
                                    //DateTime date = Convert.ToDateTime(svchainst.DateInstalled);
                                    //DateTime nextMonth = date.AddMonths(chekint);
                                    TimeSpan t = nextMonth - date;
                                    double NrOfDays = t.TotalDays;
                                    DateTime inspectduedate = Convert.ToDateTime(svchainst.DateInstalled).AddDays(NrOfDays);


                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }

                                    svchainst.InspectionDueDate = inspectduedate;
                                    //DateTime inspectduedate = Convert.ToDateTime(svchainst.DateInstalled).AddMonths(rat);

                                    //svchainst.InspectionDueDate = inspectduedate;
                                }
                            }
                        }
                        catch { }

                        sc.ChainStoppers.Add(svchainst);
                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record saved successfully ", "Add Chain Stopper", MessageBoxButton.OK, MessageBoxImage.Information);

                        CancelChainStoper();
                        //}
                    }else
                    {
                        MessageBox.Show("UniqueID already exist !", "Chain Stopper", MessageBoxButton.OK, MessageBoxImage.Information);
                        //MainViewModelWorkHours.CommonValue = true;
                        return;
                    }

                }
                //}
                //else
                //{
                //    MessageBox.Show("MooringWinch already exist ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                //}
                //else
                //{

                //    MooringWinchMessage = "Please Enter the MooringWinch Name";
                //    RaisePropertyChanged("MooringWinchMessage");
                //}
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private string lblmessage;
        public string Lblmessage
        {
            get
            {
                return lblmessage;
            }
            set
            {
                lblmessage = value;
                RaisePropertyChanged("Lblmessage");
            }
        }
        private void UpdateChainStopper(ChainStopperClass svchainst)
        {
            try
            {
                refreshmessage1(svchainst);
                //moorwinchrope.AssignedNumber = moorwinchrope.AssignedNumber != null ? moorwinchrope.AssignedNumber.Trim() : moorwinch.AssignedNumber;
                //if (!string.IsNullOrEmpty(moorwinchrope.AssignedNumber))
                //{
                //    var findassno = sc.MooringWinch.Where(x => x.AssignedNumber == moorwinchrope.AssignedNumber).FirstOrDefault();

                //    if (findassno == null)
                //    {

                if (!string.IsNullOrEmpty(Lblmessage))
                    CheckErrorMessage.CheckErrorMessages = false;

                if (CheckErrorMessage.CheckErrorMessages)
                {
                    //var duplicatecheck = sc.ChainStoppers.Where(x => x.UniqueID == svchainst.UniqueID).Count();
                    //if (duplicatecheck == 0)
                    //{


                        //moorwinchrope.RopeType = textinfo.ToTitleCase(moorwinchrope.RopeType.ToLower());
                        //moorwinchrope.RopeConstruction = textinfo.ToTitleCase(moorwinchrope.RopeConstruction.ToLower());
                        //moorwinchrope.DiaMeter = textinfo.ToTitleCase(moorwinchrope.DiaMeter.ToLower());
                        svchainst.LooseETypeId = 5;
                        svchainst.Length = Convert.ToDecimal(svchainst.Length);
                        svchainst.MBL = Convert.ToDecimal(svchainst.MBL);
                        //moorwinchrope.LDBF = Convert.ToDecimal(moorwinchrope.LDBF);
                        //moorwinchrope.WLL = Convert.ToDecimal(moorwinchrope.WLL);
                        svchainst.ManufactureName = svchainst.ManufactureName;
                        svchainst.CertificateNumber = svchainst.CertificateNumber;
                        svchainst.UniqueID = svchainst.UniqueID;
                        svchainst.DateReceived = Convert.ToDateTime(svchainst.DateReceived);
                        svchainst.DateInstalled = Convert.ToDateTime(svchainst.DateInstalled);
                        //moorwinchrope.RopeTagging = textinfo.ToTitleCase(moorwinchrope.RopeTagging.ToLower());
                        //svchainst.OutofServiceDate = Convert.ToDateTime(svchainst.OutofServiceDate);
                        svchainst.OutofServiceDate = null;
                        //svchainst.ReasonOutofService = textinfo.ToTitleCase(svchainst.ReasonOutofService.ToLower());

                        if (svchainst.OtherReason != null)
                        {
                            svchainst.OtherReason = svchainst.OtherReason;
                        }
                        if (svchainst.DamagedObserved != null)
                        {
                            svchainst.DamagedObserved = svchainst.DamagedObserved;
                        }
                        if (svchainst.MooringOperation != null)
                        {
                            svchainst.MooringOperation = svchainst.MooringOperation;
                        }
                        //svchainst.ModifiedDate = DateTime.Now;
                        //svchainst.ModifiedBy = "Admin";
                        //svchainst.IsActive = true;

                        var local = sc.Set<ChainStopperClass>()
                     .Local
                     .FirstOrDefault(f => f.Id == svchainst.Id);
                        if (local != null)
                        {
                            sc.Entry(local).State = EntityState.Detached;
                        }


                        if (svchainst.IsRopeInstalled == "No")
                        {
                            svchainst.DateInstalled = null;
                        }
                        else
                        {
                            SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType = 5 ", sc.con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                //decimal rating1 = Convert.ToDecimal(dt.Rows[0]["MaximumMonthsAllowed"]);
                                //int rat = Convert.ToInt32(rating1);

                                decimal datecheck = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]);
                                decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                perchk = perchk * 100;
                                int near = Convert.ToInt32(perchk);
                                //DateTime inspectduedate = Convert.ToDateTime(svchainst.DateInstalled).AddDays(near);


                                int chekint = Convert.ToInt32(datecheck);
                                DateTime date = Convert.ToDateTime(svchainst.DateInstalled);
                                DateTime nextMonth = date.AddMonths(chekint);
                                TimeSpan t = nextMonth - date;
                                double NrOfDays = t.TotalDays;
                                DateTime inspectduedate = Convert.ToDateTime(svchainst.DateInstalled).AddDays(NrOfDays);

                                DateTime crntdt = DateTime.Now;
                                if (inspectduedate <= crntdt)
                                {
                                    inspectduedate = DateTime.Now;
                                }

                                svchainst.InspectionDueDate = inspectduedate;
                                //DateTime inspectduedate = Convert.ToDateTime(svchainst.DateInstalled).AddMonths(rat);

                                //svchainst.InspectionDueDate = inspectduedate;
                            }
                        }

                        ChainStopperClass UpdatedChainStopper = new ChainStopperClass()
                        {
                            Id = svchainst.Id,
                            LooseETypeId = 5,
                            ManufactureName = svchainst.ManufactureName,
                            CertificateNumber = svchainst.CertificateNumber,
                            MBL = svchainst.MBL,
                            Length = svchainst.Length,
                            DateReceived = svchainst.DateReceived,
                            DateInstalled = svchainst.DateInstalled,
                            // OutofServiceDate = svchainst.OutofServiceDate,
                            InspectionDueDate = svchainst.InspectionDueDate,
                            ReasonOutofService = svchainst.ReasonOutofService,
                            OtherReason = svchainst.OtherReason,
                            DamagedObserved = svchainst.DamagedObserved,
                            DeleteStatus = false,
                            MOpId = svchainst.MOpId,
                            IsActive = true,
                            ModifiedDate = DateTime.Now,
                            Remarks = svchainst.Remarks,
                            ModifiedBy = "Admin",
                            CreatedDate = DateTime.Now,
                            UniqueID = svchainst.UniqueID,
                        };
                        sc.Entry(UpdatedChainStopper).State = EntityState.Modified;
                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record updated successfully ", "Add Chain Stopper", MessageBoxButton.OK, MessageBoxImage.Information);

                        CancelChainStoper();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("UniqueID already exist !", "Chain Stopper", MessageBoxButton.OK, MessageBoxImage.Information);
                    //    //MainViewModelWorkHours.CommonValue = true;
                    //    return;
                    //}
                }

            }
            catch (Exception ex)
            {

                sc.ErrorLog(ex);
            }


        }


        private void EditMooringWinch(MooringWinchRopeClass moorwinch)
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

        private void CancelChainStoper()
        {
            var lostdata = new ObservableCollection<JoiningShackleClass>(sc.JoiningShackles.Where(x => x.DeleteStatus == false).ToList());
            LooseEquipmentListViewModel cc = new LooseEquipmentListViewModel(lostdata);
            //GetCStopperList();
            ChildWindowManager.Instance.CloseChildWindow();
            //AddChainStopper = new ChainStopperClass();
            //AddChainStopper = new ChainStopperClass();
        }

        public void GetCStopperList()
        {
            //ObservableCollection<ChainStopperClass> cropelist = new ObservableCollection<ChainStopperClass>();
            try
            {

                LooseEquipmentListViewModel.loadUserAccess2.Clear();
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                SqlCommand cmd = new SqlCommand("LooseEquipmentTypeDetails", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "CStopper");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LooseEquipmentListViewModel.loadUserAccess2.Add(new ChainStopperClass()
                    {
                        Id = (int)row["Id"],
                        //IdentificationNumber = (string)row["IdentificationNumber"],
                        CertificateNumber = (string)row["CertificateNumber"],
                        ManufactureName = (string)row["ManufactureName"],
                        MBL = (decimal)row["MBL"],
                        Length = (decimal)row["Length"],
                        //OutofServiceDate = (DateTime)row["OutofServiceDate"],
                        //OutofServiceDate1 = ((DateTime)row["OutofServiceDate"]).ToString("d MMM, yyyy"),
                        //DamagedObserved = (row["DamagedObserved"] == DBNull.Value) ? string.Empty : row["DamagedObserved"].ToString(),
                        //DamagedObserved = (string)row["DamagedObserved"],
                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess1"));
                //return cropelist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                //return cropelist;
            }

        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        //public void OnPropertyChanged(PropertyChangedEventArgs e)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, e);
        //}


        private void refreshmessage1(ChainStopperClass cdc1)
        {
            ChainStopperClass cdc = cdc1;
            CheckErrorMessage.CheckErrorMessages = true;

            AddChainStopperMessages = new AddChainStopperErrorMessages();
            RaisePropertyChanged("AddChainStopperMessages");

            AddChainStopperErrorMessages m = (AddChainStopperMessages as AddChainStopperErrorMessages); //DownCasting.....

            //cdc.RopeType = cdc.RopeType != null ? cdc.RopeType.Trim() : string.Empty;
            //cdc.RopeConstruction = cdc.RopeConstruction != null ? cdc.RopeConstruction.Trim() : string.Empty;
            cdc.ManufactureName = cdc.ManufactureName != null ? cdc.ManufactureName.Trim() : string.Empty;
            cdc.Length = cdc.Length != null ? cdc.Length : null;
            cdc.MBL = cdc.MBL != null ? cdc.MBL : null;
            //cdc.LDBF = cdc.LDBF != null ? cdc.LDBF : null;
            //cdc.WLL = cdc.WLL != null ? cdc.WLL : null;
            cdc.CertificateNumber = cdc.CertificateNumber != null ? cdc.CertificateNumber.Trim() : string.Empty;
            cdc.UniqueID = cdc.UniqueID != null ? cdc.UniqueID.Trim() : string.Empty;
            cdc.DateReceived = cdc.DateReceived != null ? cdc.DateReceived : null;
            cdc.DateInstalled = cdc.DateInstalled != null ? cdc.DateInstalled : null;
            //cdc.ReasonOutofService = cdc.ReasonOutofService != null ? cdc.ReasonOutofService.Trim() : string.Empty;
            // cdc.RopeTagging = cdc.RopeTagging != null ? cdc.RopeTagging.Trim() : string.Empty;

            //if (string.IsNullOrEmpty(cdc.RopeType))
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.RopeTypeMessage = "Please Select RopeType !";
            //    RaisePropertyChanged("AddMooringWinchRopeMessages");
            //}
            //if (string.IsNullOrEmpty(cdc.RopeConstruction))
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.RopeConstructionMessage = "Please Select Rope Construction !";
            //    RaisePropertyChanged("AddMooringWinchRopeMessages");
            //}
            //if (string.IsNullOrEmpty(cdc.DiaMeter))
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.DiaMeterMessage = "Please Enter DiaMeter !";
            //    RaisePropertyChanged("AddMooringWinchRopeMessages");
            //}
            if (cdc.Length == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.LengthMessage = "Please Enter Length !";
                RaisePropertyChanged("AddChainStopperMessages");
            }
            if (cdc.MBL == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.MBLMessage = "Please Enter MBL !";
                RaisePropertyChanged("AddChainStopperMessages");
            }
            //if (cdc.LDBF == null)
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.LDBFMessage = "Please Enter LDBF !";
            //    RaisePropertyChanged("AddMooringWinchRopeMessages");
            //}
            //if (cdc.WLL == null)
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.WLLMessage = "Please Enter WLL !";
            //    RaisePropertyChanged("AddMooringWinchRopeMessages");
            //}
            if (cdc.DateReceived == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.ReceivedDateMessage = "Please Choose Received Date !";
                RaisePropertyChanged("AddChainStopperMessages");
            }
            //if (cdc.DateReceived == null)
            //{
            //       CheckErrorMessage.CheckErrorMessages = false;
            //       m.InstalledDateMessage = "Please Choose Installed Date !";
            //       RaisePropertyChanged("ChainStopper");
            //}
            //if (string.IsNullOrEmpty(cdc.RopeTagging))
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.RopeTaggingMessage = "Please Enter Rope Tagging !";
            //    RaisePropertyChanged("AddMooringWinchRopeMessages");
            //}
            //if (string.IsNullOrEmpty(cdc.ReasonOutofService))
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.ReasonoutofServiceMessage = "Please Enter ReasonoutofService !";
            //    RaisePropertyChanged("AddChainStopperMessages");
            //}
            if (string.IsNullOrEmpty(cdc.CertificateNumber))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.CertificateNoMessage = "Please Enter Certificate Number !";
                RaisePropertyChanged("AddChainStopperMessages");
            }
            if (string.IsNullOrEmpty(cdc.UniqueID))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.UniqueIDMessage = "Please Enter Unique Identificaton No !";
                RaisePropertyChanged("AddChainStopperMessages");
            }
            if (string.IsNullOrEmpty(cdc.ManufactureName))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.ManufactureNameMessage = "Please Enter Manufacture Name !";
                RaisePropertyChanged("AddChainStopperMessages");
            }



        }
        public static class CheckErrorMessage
        {
            public static bool CheckErrorMessages { get; set; }
            public static bool CheckErrorMessages1 { get; set; }
            public static bool CheckErrorMessages2 { get; set; }
            public static bool chkyoungs { get; set; }

        }

        public static string _dmgob;
        public string ComboValue
        {

            get
            {
                if (_dmgob != null)
                    AddChainStopper.DamagedObserved = _dmgob;


                return _dmgob;
            }

            set
            {
                _dmgob = value;
                if (_dmgob != null)
                    AddChainStopper.DamagedObserved = _dmgob;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue"));
            }
        }

        public static string _moorop;
        public string ComboValue2
        {

            get
            {
                if (_moorop != null)
                    AddChainStopper.MooringOperation = _moorop;


                return _moorop;
            }

            set
            {
                _moorop = value;
                if (_moorop != null)
                    AddChainStopper.MooringOperation = _moorop;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue2"));
            }
        }

    }
}
