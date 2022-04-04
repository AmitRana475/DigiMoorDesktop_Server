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
    public class AddRopeTailViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddRopeTailViewModel(RopeTailClass edeps)
        {
            StaticHelper.Editing = true;
            //IsEnabledCheck = false;
            IsEnabledCheck = true;
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<RopeTailClass>(UpdateRopetail);
            cancelCommand = new RelayCommand(CancelMooringWinch);

            // MooringWinchRopeClass data = sc.MooringWinchRope.Where(x => x.Id == edeps.Id).FirstOrDefault();

            AddRopeTail = new RopeTailClass()
            {
                Id = edeps.Id,
                LooseETypeId = edeps.LooseETypeId,
                Length = edeps.Length,
                MBL = edeps.MBL,
                LDBF = edeps.LDBF,
                WLL = edeps.WLL,
                ManufactureName = edeps.ManufactureName,
                CertificateNumber = edeps.CertificateNumber,
                RopeTagging = edeps.RopeTagging,
                ModifiedDate = DateTime.Now,
                Remarks = edeps.Remarks,
                UniqueID = edeps.UniqueID,
                ModifiedBy = "Admin",
                DiaMeter=edeps.DiaMeter
            };
            //SLooseEType = edeps.LooseETypeId;
            ComboValue7 = edeps.RopeTagging;
            SRopeConst = edeps.RopeConstruction;
            //SRopeDiameter = edeps.DiaMeter;
            SRopeReasonoutofs = edeps.ReasonOutofService;
            SRopeMooringOpertaion = edeps.MooringOperation;
            SRopeDamageObserved = edeps.DamageObserved;
            ReceivedDate = edeps.ReceivedDate;
            var tesst = Convert.ToDateTime(edeps.InstalledDate);
            if (tesst.ToString() == "01/01/0001 00:00:00")
            {
                InstalledDate = DateTime.Now;
            }
            else
            {
                InstalledDate = edeps.InstalledDate;
            }

            if(edeps.InstalledDate1==null)
            {
                InstalledDate = DateTime.Now;
            }

            OutofServiceDate = edeps.OutofServiceDate;


            RaisePropertyChanged("AddMooringWinchRope");
            OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinchRope"));
            //EditMooringWinch(edeps);
        }
        public AddRopeTailViewModel(int id)
        {
            StaticHelper.Editing = true;
            IsEnabledCheck = true;
            StaticHelper.Id = id;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<RopeTailClass>(SaveRopeTail);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            ropetype = GetRopeType();
            looseEtype = GetLooseEType();
            manufname = GetManuFName();
            outofsreason = GetOutofSReason();
            damageobserved = GetDamageObserved();

            GetMooringOperation();

            AddRopeTailMessages = new AddRopeTailErrorMessages();
            RaisePropertyChanged("AddRopeTailMessages");
        }
        public void resetform()
        {
            try
            {
                IsEnabledCheck = true;
                erinfo = 0;
                AddRopeTail = new RopeTailClass();
                RaisePropertyChanged("AddRopeTail");

                AddRopeTailMessages = new AddRopeTailErrorMessages();
                RaisePropertyChanged("AddRopeTailMessages");


                ReceivedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                RaisePropertyChanged("ReceivedDate");
                InstalledDate = null;
                RaisePropertyChanged("InstalledDate");

                ComboValue7 = null; RaisePropertyChanged("ComboValue7");
                SRopeDamageObserved = null; RaisePropertyChanged("SRopeDamageObserved");
               // SRopeDiameter = null; RaisePropertyChanged("SRopeDiameter");
                SRopeMooringOpertaion = null; RaisePropertyChanged("SRopeMooringOpertaion");
                Smoorop = null; RaisePropertyChanged("Smoorop");
                ComboValue1 = null; RaisePropertyChanged("ComboValue1");
                ComboValue2 = null; RaisePropertyChanged("ComboValue2");
                SManuFName = null; RaisePropertyChanged("SManuFName");

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
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
      
        
        #region BindMoorOperation



        public static MooringOperationCombo smoorop;// = new Ropetypecombo();
        public MooringOperationCombo Smoorop
        {
            get
            {
                if (smoorop != null)
                {
                    AddRopeTail.MOpId = smoorop.OPId;
                    OnPropertyChanged(new PropertyChangedEventArgs("AddRopeTail"));
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


        public static string _sinroptagg;
        public string ComboValue7
        {

            get
            {
                if (_sinroptagg != null)
                    AddRopeTail.RopeTagging = _sinroptagg;


                return _sinroptagg;
            }

            set
            {
                _sinroptagg = value;
                if (_sinroptagg != null)
                    AddRopeTail.RopeTagging = _sinroptagg;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue7"));
            }
        }

        private static string sropeManuFName;
        public string SManuFName
        {
            get
            {
                if (sropeManuFName != null)
                    AddRopeTail.ManufactureName = sropeManuFName;
                if (sropeManuFName == "MASTER")
                    AddRopeTail.ManufactureName = "All";

                return sropeManuFName;
            }

            set
            {
                sropeManuFName = value;
                if (sropeManuFName != null)
                    AddRopeTail.ManufactureName = sropeManuFName;
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
        public static RopeTailClass _AddRopeTail = new RopeTailClass();
        public RopeTailClass AddRopeTail
        {
            get
            {
                //MooringWinchMessage = string.Empty;
                //RaisePropertyChanged("MooringWinchMessage");

                //if (erinfo == 1)
                //{
                //    refreshmessage(_AddCrewDetail);
                //    refreshmessage1(_AddCrewDetail);
                //    RaisePropertyChanged("AddCrewMessages");
                //}
                //else if (erinfo == 2)
                //{
                //    AddCrewMessages = new AddCrewErrorMessages();
                //    OnPropertyChanged(new PropertyChangedEventArgs("AddCrewMessages"));
                //    AddCrewDetail = new CrewDetailClass();
                //    OnPropertyChanged(new PropertyChangedEventArgs("AddCrewDetail"));

                //    erinfo = 0;
                //}


                return _AddRopeTail;
            }
            set
            {
                _AddRopeTail = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddRopeTail"));
            }
        }

        private static AddRopeTailErrorMessages _AddRopeTailMessages = new AddRopeTailErrorMessages();
        public AddRopeTailErrorMessages AddRopeTailMessages
        {
            get
            {
                return _AddRopeTailMessages;
            }
            set
            {
                _AddRopeTailMessages = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddRopeTailMessages"));
            }
        }
        public class AddRopeTailErrorMessages
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
            public string InstalledDateMessage { get; set; }
            public string RopeTaggingMessage { get; set; }
            public string ReasonoutofServiceMessage { get; set; }
            public string IsRopeInstalled { get; set; }
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


        //#region Bind Properties


        #region Bind Ropetype
        private static ObservableCollection<string> ropetype = new ObservableCollection<string>();
        public ObservableCollection<string> RopeType
        {
            get
            {
                return ropetype;
            }
            set
            {
                ropetype = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
            }
        }

        private ObservableCollection<string> GetRopeType()
        {
            var AddRopeType = new ObservableCollection<string>();
            var data = sc.MooringRopeType.OrderBy(s => s.Id).Select(x => x.RopeType).ToList();

            foreach (var item in data)
            {
                AddRopeType.Add(item);

            }

            return AddRopeType;
        }
        //private static string sropetype;
        //public string SRopeType
        //{
        //    get
        //    {
        //        if (sropetype != null)
        //            AddMooringWinchRope.RopeType = sropetype;
        //        if (sropetype == "MASTER")
        //            AddMooringWinchRope.RopeType = "All";

        //        return sropetype;
        //    }

        //    set
        //    {
        //        sropetype = value;
        //        if (sropetype != null)
        //            AddMooringWinchRope.RopeType = sropetype;
        //        OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
        //    }
        //}

        private static int slooseEtype;
        public int SLooseEType
        {
            get
            {
                if (slooseEtype != 0 || slooseEtype != null)
                    AddRopeTail.LooseETypeId = slooseEtype;


                return slooseEtype;
            }

            set
            {
                slooseEtype = value;
                if (slooseEtype != null)
                    AddRopeTail.LooseETypeId = slooseEtype;
                OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
            }
        }

        private static string _sropeconsts;
        public string SRopeConst
        {
            get
            {
                if (_sropeconsts != null)
                    AddRopeTail.RopeConstruction = _sropeconsts;
                if (_sropeconsts == "MASTER")
                    AddRopeTail.RopeConstruction = "All";

                return _sropeconsts;
            }

            set
            {
                _sropeconsts = value;
                if (_sropeconsts != null)
                    AddRopeTail.RopeConstruction = _sropeconsts;
                OnPropertyChanged(new PropertyChangedEventArgs("RopeConstruction"));
            }
        }
        //private static string sropediameter;
        //public string SRopeDiameter
        //{
        //    get
        //    {
        //        if (sropediameter != null)
        //            AddRopeTail.DiaMeter = sropediameter;
        //        if (sropediameter == "MASTER")
        //            AddRopeTail.DiaMeter = "All";

        //        return sropediameter;
        //    }

        //    set
        //    {
        //        sropediameter = value;
        //        if (sropediameter != null)
        //            AddRopeTail.DiaMeter = sropediameter;
        //        OnPropertyChanged(new PropertyChangedEventArgs("DiaMeter"));
        //    }
        //}

        private static string srreasonoutofservice;
        public string SRopeReasonoutofs
        {
            get
            {
                if (srreasonoutofservice != null)
                    AddRopeTail.ReasonOutofService = srreasonoutofservice;
                if (srreasonoutofservice == "MASTER")
                    AddRopeTail.ReasonOutofService = "All";

                return srreasonoutofservice;
            }

            set
            {
                srreasonoutofservice = value;
                if (srreasonoutofservice != null)
                    AddRopeTail.ReasonOutofService = srreasonoutofservice;
                OnPropertyChanged(new PropertyChangedEventArgs("ReasonOutofService"));
            }
        }

        private static string sdamageobserved;
        public string SRopeDamageObserved
        {
            get
            {
                if (sdamageobserved != null)
                    AddRopeTail.DamageObserved = sdamageobserved;
                if (sdamageobserved == "MASTER")
                    AddRopeTail.DamageObserved = "All";

                return sdamageobserved;
            }

            set
            {
                sdamageobserved = value;
                if (sdamageobserved != null)
                    AddRopeTail.DamageObserved = sdamageobserved;
                OnPropertyChanged(new PropertyChangedEventArgs("DamageObserved"));
            }
        }

        private static string smooringoperation;
        public string SRopeMooringOpertaion
        {
            get
            {
                if (smooringoperation != null)
                    AddRopeTail.MooringOperation = smooringoperation;
                if (smooringoperation == "MASTER")
                    AddRopeTail.MooringOperation = "All";

                return smooringoperation;
            }

            set
            {
                smooringoperation = value;
                if (smooringoperation != null)
                    AddRopeTail.MooringOperation = smooringoperation;
                OnPropertyChanged(new PropertyChangedEventArgs("MooringOperation"));
            }
        }

        public static Nullable<DateTime> _ReceivedDate = null;
        public Nullable<DateTime> ReceivedDate
        {
            get
            {
                if (_ReceivedDate == null)
                {
                    _ReceivedDate = DateTime.Now.Date;
                }
                AddRopeTail.ReceivedDate = (DateTime)_ReceivedDate;
                return _ReceivedDate;
            }
            set
            {
                _ReceivedDate = value;
                RaisePropertyChanged("ReceivedDate");
            }
        }

        public static Nullable<DateTime> _InstallDate = null;
        public Nullable<DateTime> InstalledDate
        {
            get
            {
                if (_InstallDate == null)
                {
                    AddRopeTail.InstalledDate = DateTime.Now.Date;
                    _InstallDate = DateTime.Now.Date;
                }
                else
                {
                    AddRopeTail.InstalledDate = (DateTime)_InstallDate;
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
                AddRopeTail.OutofServiceDate = (DateTime)_OutofServiceDate;
                return _InstallDate;
            }
            set
            {
                _OutofServiceDate = value;
                RaisePropertyChanged("OutofServiceDate");
            }
        }
        #endregion
        private void SaveRopeTail(RopeTailClass svropetail)
        {
            try
            {
                refreshmessage1(svropetail);
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
                    var duplicatecheck = sc.RopeTails.Where(x => x.UniqueID == svropetail.UniqueID && x.LooseETypeId== StaticHelper.Id).Count();
                    if (duplicatecheck == 0)
                    {

                        svropetail.LooseETypeId = StaticHelper.Id;
                        svropetail.RopeConstruction = svropetail.RopeConstruction;
                        //svropetail.DiaMeter = textinfo.ToTitleCase(svropetail.DiaMeter.ToLower());
                        svropetail.DiaMeter = Convert.ToDecimal(svropetail.DiaMeter1);
                        svropetail.Length = Convert.ToDecimal(svropetail.Length);
                        svropetail.MBL = Convert.ToDecimal(svropetail.MBL);
                        svropetail.LDBF = Convert.ToDecimal(svropetail.LDBF);
                        svropetail.WLL = Convert.ToDecimal(svropetail.WLL);
                        svropetail.ManufactureName = svropetail.ManufactureName;
                        svropetail.CertificateNumber = svropetail.CertificateNumber;
                        svropetail.UniqueID = svropetail.UniqueID;
                        svropetail.ReceivedDate = Convert.ToDateTime(svropetail.ReceivedDate);
                        //svropetail.InstalledDate = Convert.ToDateTime(svropetail.InstalledDate);

                        if (svropetail.IsRopeInstalled == "No")
                        {
                            svropetail.InstalledDate = null;
                        }

                        if (svropetail.InstalledDate == null)
                        {
                            svropetail.InstalledDate = null;
                        }

                        svropetail.DeleteStatus = false;
                        svropetail.RopeTagging = svropetail.RopeTagging;
                        //svropetail.OutofServiceDate = Convert.ToDateTime(svropetail.InstalledDate);
                        svropetail.OutofServiceDate = null;
                        //svropetail.ReasonOutofService = textinfo.ToTitleCase(svropetail.ReasonOutofService.ToLower());

                        if (svropetail.OtherReason != null)
                        {
                            svropetail.OtherReason = svropetail.OtherReason;
                        }
                        if (svropetail.DamageObserved != null)
                        {
                            svropetail.DamageObserved = svropetail.DamageObserved;
                        }
                        if (svropetail.MooringOperation != null)
                        {
                            svropetail.MooringOperation = svropetail.MooringOperation;
                        }
                        svropetail.CreatedDate = DateTime.Now;
                        svropetail.CreatedBy = "Admin";
                        svropetail.IsActive = true;



                        RopeDamageRecordClass rpdm = new RopeDamageRecordClass();

                        try
                        {
                            if (svropetail.InstalledDate != null)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType=" + StaticHelper.Id + "", sc.con);
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
                                    // DateTime inspectduedate = Convert.ToDateTime(svropetail.InstalledDate).AddDays(near);

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
                                    DateTime date = Convert.ToDateTime(svropetail.InstalledDate);
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
                                    //DateTime date = Convert.ToDateTime(svropetail.InstalledDate);
                                    //DateTime nextMonth = date.AddMonths(chekint);
                                    TimeSpan t = nextMonth - date;
                                    double NrOfDays = t.TotalDays;
                                    DateTime inspectduedate = Convert.ToDateTime(svropetail.InstalledDate).AddDays(NrOfDays);



                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }

                                    svropetail.InspectionDueDate = inspectduedate;
                                    //DateTime inspectduedate = Convert.ToDateTime(svropetail.InstalledDate).AddMonths(rat);

                                    //svropetail.InspectionDueDate = inspectduedate;
                                }
                            }
                        }
                        catch { }

                        sc.RopeTails.Add(svropetail);
                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record saved successfully ", "Add MooringWinch Rope", MessageBoxButton.OK, MessageBoxImage.Information);

                        CancelMooringWinch();
                        // }

                    }
                    else
                    {
                        MessageBox.Show("UniqueID already exist !", "Loose Eq.", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private void UpdateRopetail(RopeTailClass rptl)
        {
            try
            {
                refreshmessage1(rptl);


                if (!string.IsNullOrEmpty(Lblmessage))
                    CheckErrorMessage.CheckErrorMessages = false;

                if (CheckErrorMessage.CheckErrorMessages)
                {

                    //var duplicatecheck = sc.RopeTails.Where(x => x.UniqueID == rptl.UniqueID && x.LooseETypeId== rptl.LooseETypeId).Count();
                    //if (duplicatecheck == 0)
                    //{

                        var local = sc.Set<RopeTailClass>().Local.FirstOrDefault(f => f.Id == rptl.Id);
                        if (local != null)
                        {
                            sc.Entry(local).State = EntityState.Detached;
                        }

                        if (rptl.IsRopeInstalled == "No")
                        {
                            rptl.InstalledDate = null;
                        }
                        else
                        {
                            SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType=" + StaticHelper.LooseETypeId + "", sc.con);
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
                                //DateTime inspectduedate = Convert.ToDateTime(rptl.InstalledDate).AddDays(near);


                                int chekint = Convert.ToInt32(datecheck);
                                DateTime date = Convert.ToDateTime(rptl.InstalledDate);
                                DateTime nextMonth = date.AddMonths(chekint);
                                TimeSpan t = nextMonth - date;
                                double NrOfDays = t.TotalDays;
                                DateTime inspectduedate = Convert.ToDateTime(rptl.InstalledDate).AddDays(NrOfDays);


                                DateTime crntdt = DateTime.Now;
                                if (inspectduedate <= crntdt)
                                {
                                    inspectduedate = DateTime.Now;
                                }

                                rptl.InspectionDueDate = inspectduedate;
                                //DateTime inspectduedate = Convert.ToDateTime(svropetail.InstalledDate).AddMonths(rat);

                                //svropetail.InspectionDueDate = inspectduedate;
                            }
                        }

                        var UpdatedRopedetails = new RopeTailClass()
                        {

                            Id = rptl.Id,
                            LooseETypeId = rptl.LooseETypeId,
                            Length = rptl.Length,
                            DiaMeter = rptl.DiaMeter,
                            WLL = rptl.WLL,
                            LDBF = rptl.LDBF,
                            MBL = rptl.MBL,
                            ReasonOutofService = rptl.ReasonOutofService,
                            //OutofServiceDate = rptl.OutofServiceDate,
                            DamageObserved = rptl.DamageObserved,
                            InstalledDate = rptl.InstalledDate,
                            InspectionDueDate = rptl.InspectionDueDate,
                            MOpId = rptl.MOpId,
                            ModifiedDate = DateTime.Now,
                            IsActive = true,
                            DeleteStatus = false,
                            RopeConstruction = rptl.RopeConstruction,
                            RopeTagging = rptl.RopeTagging,
                            ReceivedDate = rptl.ReceivedDate,
                            CertificateNumber = rptl.CertificateNumber,
                            ManufactureName = rptl.ManufactureName,
                            Remarks = rptl.Remarks,
                            UniqueID = rptl.UniqueID,
                            CreatedBy = "Admin",
                            CreatedDate = DateTime.Now,
                        };

                        sc.Entry(UpdatedRopedetails).State = EntityState.Modified;
                        sc.SaveChanges();



                        StaticHelper.Editing = false;
                        MessageBox.Show("Record updated successfully", "Update RopeTail", MessageBoxButton.OK, MessageBoxImage.Information);


                        CancelMooringWinch();

                    }
                //}else
                //{
                //    MessageBox.Show("UniqueID already exist !", "Loose Equip", MessageBoxButton.OK, MessageBoxImage.Information);
                //    //MainViewModelWorkHours.CommonValue = true;
                //    return;
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


        //private void EditMooringWinch(MooringWinchRopeClass moorwinch)
        //{
        //    try
        //    {

        //        var findrank = sc.MooringWinch.Where(x => x.Id == moorwinch.Id).FirstOrDefault();
        //        // AddMooringWinch.AssignedNumber = findrank.AssignedNumber;
        //        // AddMooringWinch.Id = findrank.Id;
        //        OnPropertyChanged(new PropertyChangedEventArgs("AddRopeDetail"));
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //    }


        // }

        private void CancelMooringWinch()
        {
            var lostdata = new ObservableCollection<JoiningShackleClass>(sc.JoiningShackles.Where(x => x.DeleteStatus == false).ToList());
            LooseEquipmentListViewModel cc = new LooseEquipmentListViewModel(lostdata);
            ChildWindowManager.Instance.CloseChildWindow();
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


        private void refreshmessage1(RopeTailClass cdc1)
        {
            RopeTailClass cdc = cdc1;
            CheckErrorMessage.CheckErrorMessages = true;

            AddRopeTailMessages = new AddRopeTailErrorMessages();
            RaisePropertyChanged("AddRopeTailMessages");

            AddRopeTailErrorMessages m = (AddRopeTailMessages as AddRopeTailErrorMessages); //DownCasting.....

            //cdc.RopeType = cdc.RopeType != null ? cdc.RopeType.Trim() : string.Empty;
            cdc.ManufactureName = cdc.ManufactureName != null ? cdc.ManufactureName.Trim() : string.Empty;
            cdc.IsRopeInstalled = cdc.IsRopeInstalled != null ? cdc.IsRopeInstalled.Trim() : string.Empty;
            cdc.RopeConstruction = cdc.RopeConstruction != null ? cdc.RopeConstruction.Trim() : string.Empty;
           // cdc.DiaMeter = cdc.DiaMeter != null ? cdc.DiaMeter.Trim() : string.Empty;
            cdc.Length = cdc.Length != null ? cdc.Length : null;
            cdc.MBL = cdc.MBL != null ? cdc.MBL : null;
            cdc.DiaMeter = cdc.DiaMeter != null ? cdc.DiaMeter : null;
            //cdc.LDBF = cdc.LDBF != null ? cdc.LDBF : null;
            //cdc.WLL = cdc.WLL != null ? cdc.WLL : null;
            cdc.CertificateNumber = cdc.CertificateNumber != null ? cdc.CertificateNumber.Trim() : string.Empty;
            cdc.UniqueID = cdc.UniqueID != null ? cdc.UniqueID.Trim() : string.Empty;
            cdc.ReceivedDate = cdc.ReceivedDate != null ? cdc.ReceivedDate : null;
            //cdc.InstalledDate = cdc.InstalledDate != null ? cdc.InstalledDate : null;
            //cdc.ReasonOutofService = cdc.ReasonOutofService != null ? cdc.ReasonOutofService.Trim() : string.Empty;
            cdc.RopeTagging = cdc.RopeTagging != null ? cdc.RopeTagging.Trim() : string.Empty;

            //if (string.IsNullOrEmpty(cdc.RopeType))
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.RopeTypeMessage = "Please Select RopeType !";
            //    RaisePropertyChanged("AddMooringWinchRopeMessages");
            //}
            if (string.IsNullOrEmpty(cdc.RopeConstruction))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.RopeConstructionMessage = "Please Select Rope Construction !";
                RaisePropertyChanged("AddRopeTailMessages");
            }
            //if (string.IsNullOrEmpty(cdc.DiaMeter))
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.DiaMeterMessage = "Please Enter DiaMeter !";
            //    RaisePropertyChanged("AddRopeTailMessages");
            //}
            if (cdc.DiaMeter == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.DiaMeterMessage = "Please Enter DiaMeter !";
                RaisePropertyChanged("AddRopeTailMessages");
            }
            if (cdc.Length == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.LengthMessage = "Please Enter Length !";
                RaisePropertyChanged("AddRopeTailMessages");
            }
            if (cdc.MBL == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.MBLMessage = "Please Enter MBL !";
                RaisePropertyChanged("AddRopeTailMessages");
            }
            //if (cdc.LDBF == null)
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.LDBFMessage = "Please Enter LDBF !";
            //    RaisePropertyChanged("AddRopeTailMessages");
            //}
            //if (cdc.WLL == null)
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.WLLMessage = "Please Enter WLL !";
            //    RaisePropertyChanged("AddRopeTailMessages");
            //}
            if (cdc.ReceivedDate == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.ReceivedDateMessage = "Please Choose Received Date !";
                RaisePropertyChanged("AddRopeTailMessages");
            }
            //if (cdc.InstalledDate == null)
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.InstalledDateMessage = "Please Choose Installed Date !";
            //    RaisePropertyChanged("AddRopeTailMessages");
            //}
            if (string.IsNullOrEmpty(cdc.RopeTagging))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.RopeTaggingMessage = "Please Enter Rope Tagging !";
                RaisePropertyChanged("AddRopeTailMessages");
            }
            if (string.IsNullOrEmpty(cdc.IsRopeInstalled))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.IsRopeInstalled = "Please Choose at least 1 !";
                RaisePropertyChanged("AddRopeTailMessages");
            }
            //if (string.IsNullOrEmpty(cdc.ReasonOutofService))
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.ReasonoutofServiceMessage = "Please Enter ReasonoutofService !";
            //    RaisePropertyChanged("AddRopeTailMessages");
            //}
            if (string.IsNullOrEmpty(cdc.CertificateNumber))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.CertificateNoMessage = "Please Enter Certificate Number !";
                RaisePropertyChanged("AddRopeTailMessages");
            }
            if (string.IsNullOrEmpty(cdc.UniqueID))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.UniqueIDMessage = "Please Enter Unique Identification No!";
                RaisePropertyChanged("AddRopeTailMessages");
            }
            if (string.IsNullOrEmpty(cdc.ManufactureName))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.ManufactureNameMessage = "Please Enter Manufacture Name !";
                RaisePropertyChanged("AddRopeTailMessages");
            }



        }
        public static class CheckErrorMessage
        {
            public static bool CheckErrorMessages { get; set; }
            public static bool CheckErrorMessages1 { get; set; }
            public static bool CheckErrorMessages2 { get; set; }
            public static bool chkyoungs { get; set; }

        }

        public static string _sincrep;
        //public string ComboValue
        //{

        //    get
        //    {
        //        if (_sincrep != null)
        //            AddRopeTail.Type = _sincrep;


        //        return _sincrep;
        //    }

        //    set
        //    {
        //        _sincrep = value;
        //        if (_sincrep != null)
        //            AddRopeTail.Type = _sincrep;
        //        OnPropertyChanged(new PropertyChangedEventArgs("ComboValue"));
        //    }
        //}

        public static string _dmgob;
        public string ComboValue1
        {

            get
            {
                if (_dmgob != null)
                    AddRopeTail.DamageObserved = _dmgob;


                return _dmgob;
            }

            set
            {
                _dmgob = value;
                if (_dmgob != null)
                    AddRopeTail.DamageObserved = _dmgob;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue1"));
            }
        }

        public static string _moorO;
        public string ComboValue2
        {

            get
            {
                if (_moorO != null)
                    AddRopeTail.MooringOperation = _moorO;


                return _moorO;
            }

            set
            {
                _moorO = value;
                if (_moorO != null)
                    AddRopeTail.MooringOperation = _moorO;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue2"));
            }
        }
    }
}
