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
    public class AddLooseEquipmentViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;

        public AddLooseEquipmentViewModel(JoiningShackleClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<JoiningShackleClass>(UpdateJoiningShackle);
            cancelCommand = new RelayCommand(CancelMooringWinch);

            // MooringWinchRopeClass data = sc.MooringWinchRope.Where(x => x.Id == edeps.Id).FirstOrDefault();

            //IsEnabledCheck = false;

            IsEnabledCheck = true;

            AddJoiningShackleE = new JoiningShackleClass()
            {
                Id = edeps.Id,
                //Length = edeps.Length,
                MBL = edeps.MBL,
                Type = edeps.Type,
                IdentificationNumber = edeps.UniqueID,
                ManufactureName = edeps.ManufactureName,
                CertificateNumber = edeps.CertificateNumber,
                Remarks = edeps.Remarks,
                UniqueID = edeps.UniqueID,
                //ModifiedDate = DateTime.Now,
                //ModifiedBy = "Admin"
            };
            SLooseEType = Convert.ToInt32(edeps.LooseETypeId);
            //SRopeConst = edeps.RopeConstruction;
            //SRopeDiameter = edeps.DiaMeter;
            SRopeReasonoutofs = edeps.ReasonOutofService;
            SRopeMooringOpertaion = edeps.MooringOperation;
            SRopeDamageObserved = edeps.DamagedObserved;
            ReceivedDate = edeps.DateReceived;

            var tesst = Convert.ToDateTime(edeps.DateInstalled);
            if (tesst.ToString() == "01/01/0001 00:00:00" || tesst==null)
            {
                InstalledDate = DateTime.Now;
            }
            else
            {
                InstalledDate = edeps.DateInstalled;
            }
          

            if(edeps.DateInstalled1 ==null)
            {
                InstalledDate = DateTime.Now;
            }
            OutofServiceDate = edeps.OutofServiceDate;
            


            RaisePropertyChanged("AddJoiningShackleE");
            OnPropertyChanged(new PropertyChangedEventArgs("AddJoiningShackleE"));
            //EditMooringWinch(edeps);
        }
        public AddLooseEquipmentViewModel()
        {
            StaticHelper.Editing = true;

            IsEnabledCheck = true;
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<JoiningShackleClass>(SaveLooseE);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            looseEtype = GetLooseEType();
            manufname = GetManuFName();
            outofsreason = GetOutofSReason();
            damageobserved = GetDamageObserved();

            GetMooringOperation();
            AddLooseEQuipmentMessages = new AddLooseEMessages();
            RaisePropertyChanged("AddLooseEQuipmentMessages");
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
        public void refreshform()
        {
            IsEnabledCheck = true;
            AddJoiningShackleE = new JoiningShackleClass();
            RaisePropertyChanged("AddJoiningShackleE");
            OnPropertyChanged(new PropertyChangedEventArgs("AddJoiningShackleE"));

            AddLooseEQuipmentMessages = new AddLooseEMessages();
            RaisePropertyChanged("AddLooseEQuipmentMessages");

            ReceivedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("ReceivedDate");
            InstalledDate = null; RaisePropertyChanged("InstalledDate");

            SManuFName = null; RaisePropertyChanged("SManuFName");
            ComboValue1 = null; RaisePropertyChanged("ComboValue1");
            ComboValue = null; RaisePropertyChanged("ComboValue");
            Smoorop = null; RaisePropertyChanged("Smoorop");
            LooseEType = null; RaisePropertyChanged("LooseEType");
            ComboValue2 = null; RaisePropertyChanged("ComboValue2");

        }
        private void UpdateJoiningShackle(JoiningShackleClass jshackle)
        {
            try
            {
                refreshmessage1(jshackle);


                if (!string.IsNullOrEmpty(Lblmessage))
                    CheckErrorMessage.CheckErrorMessages = false;

                if (CheckErrorMessage.CheckErrorMessages)
                {


                    //var duplicatecheck = sc.JoiningShackles.Where(x => x.UniqueID == jshackle.UniqueID).Count();
                    //if (duplicatecheck == 0)
                    //{

                        var local = sc.Set<JoiningShackleClass>().Local.FirstOrDefault(f => f.Id == jshackle.Id);
                        if (local != null)
                        {
                            sc.Entry(local).State = EntityState.Detached;
                        }

                        if (jshackle.IsRopeInstalled == "No")
                        {
                            jshackle.DateInstalled = null;
                        }
                        else
                        {
                            SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType=" + 1 + "", sc.con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                decimal datecheck = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]);
                                decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                perchk = perchk * 100;
                                int near = Convert.ToInt32(perchk);
                                // DateTime inspectduedate = Convert.ToDateTime(jshackle.DateInstalled).AddDays(near);


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
                                DateTime date = Convert.ToDateTime(jshackle.DateInstalled);
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
                                //DateTime date = Convert.ToDateTime(jshackle.DateInstalled);
                                //DateTime nextMonth = date.AddMonths(chekint);
                                TimeSpan t = nextMonth - date;
                                double NrOfDays = t.TotalDays;
                                DateTime inspectduedate = Convert.ToDateTime(jshackle.DateInstalled).AddDays(NrOfDays);


                                DateTime crntdt = DateTime.Now;
                                if (inspectduedate <= crntdt)
                                {
                                    inspectduedate = DateTime.Now;
                                }

                                jshackle.InspectionDueDate = inspectduedate;
                                //DateTime inspectduedate = Convert.ToDateTime(joiningShackle.DateInstalled).AddMonths(rat);

                                //joiningShackle.InspectionDueDate = inspectduedate;
                            }
                        }

                        var UpdatedRopedetails = new JoiningShackleClass()
                        {

                            Id = jshackle.Id,
                            LooseETypeId = 1,
                            //OutofServiceDate=jshackle.OutofServiceDate,
                            IdentificationNumber = jshackle.UniqueID,
                            UniqueID = jshackle.UniqueID,
                            ManufactureName = jshackle.ManufactureName,
                            Type = jshackle.Type,
                            MBL = jshackle.MBL,
                            DateReceived = jshackle.DateReceived,
                            DateInstalled = jshackle.DateInstalled,
                            InspectionDueDate = jshackle.InspectionDueDate,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            IsActive = true,
                            DeleteStatus = false,
                            CertificateNumber = jshackle.CertificateNumber,
                            Remarks = jshackle.Remarks,


                            CreatedBy = "Admin",

                        };

                        sc.Entry(UpdatedRopedetails).State = EntityState.Modified;
                        sc.SaveChanges();



                        StaticHelper.Editing = false;
                        MessageBox.Show("Record updated successfully", "Update JoiningShackle", MessageBoxButton.OK, MessageBoxImage.Information);

                        //var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.ToList());
                        //MooringWinchRopeViewModel vm = new MooringWinchRopeViewModel(lostdata);

                        //ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchRopeView() { DataContext = vm });

                        CancelMooringWinch();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("UniqueID already exist !", "Joining Shackle", MessageBoxButton.OK, MessageBoxImage.Information);
                    //    // MainViewModelWorkHours.CommonValue = true;
                    //    return;
                    //}

                }

              

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
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

                    AddJoiningShackleE.MOpId = smoorop.OPId;





                    OnPropertyChanged(new PropertyChangedEventArgs("AddJoiningShackleE"));
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


        private string mooringWinchMessage;
        public string MooringWinchMessage
        {
            get { return mooringWinchMessage; }
            set { mooringWinchMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("MooringWinchMessage")); }
        }

        #endregion

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
        public static JoiningShackleClass _AddJoiningShackleE = new JoiningShackleClass();
        public JoiningShackleClass AddJoiningShackleE
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


                return _AddJoiningShackleE;
            }
            set
            {
                _AddJoiningShackleE = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddJoiningShackleE"));
            }
        }

        private static AddLooseEMessages _AddLooseEMessages = new AddLooseEMessages();
        public AddLooseEMessages AddLooseEQuipmentMessages
        {
            get
            {
                return _AddLooseEMessages;
            }
            set
            {
                _AddLooseEMessages = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddLooseEQuipmentMessages"));
            }
        }
        public class AddLooseEMessages
        {
           // public string LooseETypeMessage { get; set; }
            public string IdentificationNMessage { get; set; }
            public string DiaMeterMessage { get; set; }
            public string LengthMessage { get; set; }
            public string MBLMessage { get; set; }
            public string LDBFMessage { get; set; }
            public string WLLMessage { get; set; }
            public string ManufactureNameMessage { get; set; }
            public string CertificateNoMessage { get; set; }
            public string ReceivedDateMessage { get; set; }
            //public string InstalledDateMessage { get; set; }
            public string RopeTaggingMessage { get; set; }
            public string ReasonoutofServiceMessage { get; set; }
            public string IsRopeInstalled { get; set; }
            public string Type { get; set; }
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

        private static int slooseEtype;
        public int SLooseEType
        {
            get
            {
                if (slooseEtype != 0)
                    AddJoiningShackleE.LooseETypeId = slooseEtype;
                //if (slooseEtype == "MASTER")
                //    AddMooringWinchRope.RopeType = "All";

                return slooseEtype;
            }

            set
            {
                slooseEtype = value;
                if (slooseEtype != 0)
                    AddJoiningShackleE.LooseETypeId = slooseEtype;
                OnPropertyChanged(new PropertyChangedEventArgs("LooseEType"));
            }
        }

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
        //            AddMooringWinchRope.RopeConstruction = _sropeconsts;
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
                    AddJoiningShackleE.ReasonOutofService = srreasonoutofservice;
                if (srreasonoutofservice == "MASTER")
                    AddJoiningShackleE.ReasonOutofService = "All";

                return srreasonoutofservice;
            }

            set
            {
                srreasonoutofservice = value;
                if (srreasonoutofservice != null)
                    AddJoiningShackleE.ReasonOutofService = srreasonoutofservice;
                OnPropertyChanged(new PropertyChangedEventArgs("ReasonOutofService"));
            }
        }

        private static string sdamageobserved;
        public string SRopeDamageObserved
        {
            get
            {
                if (sdamageobserved != null)
                    AddJoiningShackleE.DamagedObserved = sdamageobserved;
                if (sdamageobserved == "MASTER")
                    AddJoiningShackleE.DamagedObserved = "All";

                return sdamageobserved;
            }

            set
            {
                sdamageobserved = value;
                if (sdamageobserved != null)
                    AddJoiningShackleE.DamagedObserved = sdamageobserved;
                OnPropertyChanged(new PropertyChangedEventArgs("DamagedObserved"));
            }
        }

        private static string smooringoperation;
        public string SRopeMooringOpertaion
        {
            get
            {
                if (smooringoperation != null)
                    AddJoiningShackleE.MooringOperation = smooringoperation;
                if (smooringoperation == "MASTER")
                    AddJoiningShackleE.MooringOperation = "All";

                return smooringoperation;
            }

            set
            {
                smooringoperation = value;
                if (smooringoperation != null)
                    AddJoiningShackleE.MooringOperation = smooringoperation;
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
                    _ReceivedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                _AddJoiningShackleE.DateReceived = (DateTime)_ReceivedDate;
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
                    _InstallDate = DateTime.Now.Date;
                    _AddJoiningShackleE.DateInstalled = DateTime.Now.Date;
                  
                }
                else
                {
                    _AddJoiningShackleE.DateInstalled = (DateTime)_InstallDate;
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
                _AddJoiningShackleE.OutofServiceDate = (DateTime)_OutofServiceDate;
                return _OutofServiceDate;
            }
            set
            {
                _OutofServiceDate = value;
                RaisePropertyChanged("OutofServiceDate");
            }
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
                    AddJoiningShackleE.ManufactureName = sropeManuFName;
                if (sropeManuFName == "MASTER")
                    AddJoiningShackleE.ManufactureName = "All";

                return sropeManuFName;
            }

            set
            {
                sropeManuFName = value;
                if (sropeManuFName != null)
                    AddJoiningShackleE.ManufactureName = sropeManuFName;
                OnPropertyChanged(new PropertyChangedEventArgs("SManuFName"));
            }
        }

        #endregion

        private void SaveLooseE(JoiningShackleClass joiningShackle)
        {
            try
            {
                refreshmessage1(joiningShackle);
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
                    var duplicatecheck = sc.JoiningShackles.Where(x => x.UniqueID == joiningShackle.UniqueID).Count();
                    if (duplicatecheck == 0)
                    {

                        joiningShackle.LooseETypeId = 1;
                        joiningShackle.IdentificationNumber = joiningShackle.UniqueID;

                        //joiningShackle.ManufactureName = textinfo.ToTitleCase(joiningShackle.ManufactureName.ToLower());
                        joiningShackle.MBL = Convert.ToDecimal(joiningShackle.MBL);
                        joiningShackle.Type = joiningShackle.Type;



                        joiningShackle.CertificateNumber = joiningShackle.CertificateNumber;


                        joiningShackle.UniqueID = joiningShackle.UniqueID;

                        joiningShackle.DateReceived = Convert.ToDateTime(joiningShackle.DateReceived);
                        // joiningShackle.DateInstalled = Convert.ToDateTime(joiningShackle.DateInstalled);

                        if (joiningShackle.IsRopeInstalled == "No")
                        {
                            joiningShackle.DateInstalled = null;
                        }

                        if (joiningShackle.DateInstalled == null)
                        {
                            joiningShackle.DateInstalled = null;
                            //joiningShackle.OutofServiceDate = Convert.ToDateTime(joiningShackle.OutofServiceDate);
                            //joiningShackle.ReasonOutofService = textinfo.ToTitleCase(joiningShackle.ReasonOutofService.ToLower());
                        }

                        if (joiningShackle.OutofServiceDate != null)
                        {
                            joiningShackle.OutofServiceDate = null;
                            //joiningShackle.OutofServiceDate = Convert.ToDateTime(joiningShackle.OutofServiceDate);
                            //joiningShackle.ReasonOutofService = textinfo.ToTitleCase(joiningShackle.ReasonOutofService.ToLower());
                        }

                        if (joiningShackle.OtherReason != null)
                        {
                            joiningShackle.OtherReason = joiningShackle.OtherReason;
                        }
                        if (joiningShackle.DamagedObserved != null)
                        {
                            joiningShackle.DamagedObserved = joiningShackle.DamagedObserved;
                        }
                        if (joiningShackle.MooringOperation != null)
                        {
                            joiningShackle.MooringOperation = joiningShackle.MooringOperation;

                        }
                        joiningShackle.CreatedDate = DateTime.Now;
                        joiningShackle.CreatedBy = "Admin";
                        joiningShackle.IsActive = true;
                        joiningShackle.DeleteStatus = false;


                        RopeDamageRecordClass rpdm = new RopeDamageRecordClass();


                        try
                        {
                            if (joiningShackle.DateInstalled != null)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType=" + 1 + "", sc.con);
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
                                    // DateTime inspectduedate = Convert.ToDateTime(joiningShackle.DateInstalled).AddDays(near);



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
                                    DateTime date = Convert.ToDateTime(joiningShackle.DateInstalled);
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
                                    //DateTime date = Convert.ToDateTime(joiningShackle.DateInstalled);
                                    //DateTime nextMonth = date.AddMonths(chekint);
                                    TimeSpan t = nextMonth - date;
                                    double NrOfDays = t.TotalDays;
                                    DateTime inspectduedate = Convert.ToDateTime(joiningShackle.DateInstalled).AddDays(NrOfDays);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }

                                    joiningShackle.InspectionDueDate = inspectduedate;
                                    //DateTime inspectduedate = Convert.ToDateTime(joiningShackle.DateInstalled).AddMonths(rat);

                                    //joiningShackle.InspectionDueDate = inspectduedate;
                                }
                            }
                        }
                        catch { }

                        sc.JoiningShackles.Add(joiningShackle);
                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record saved successfully ", "Add JoiningShackle", MessageBoxButton.OK, MessageBoxImage.Information);

                        CancelMooringWinch();
                    }
                    else
                    {
                        MessageBox.Show("UniqueID already exist !", "Joining Shackle", MessageBoxButton.OK, MessageBoxImage.Information);
                        //MainViewModelWorkHours.CommonValue = true;
                        return;
                    }
                }
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
        private void UpdateJoiningShackled(JoiningShackleClass moorwinch)
        {
            //try
            //{
            //    refreshmessage1(moorwinch);


            //    if (!string.IsNullOrEmpty(Lblmessage))
            //        CheckErrorMessage.CheckErrorMessages = false;

            //    if (CheckErrorMessage.CheckErrorMessages)
            //    {

            //        var local = sc.Set<MooringWinchRopeClass>()
            //     .Local
            //     .FirstOrDefault(f => f.Id == moorwinch.Id);
            //        if (local != null)
            //        {
            //            sc.Entry(local).State = EntityState.Detached;
            //        }

            //        var UpdatedRopedetails = new MooringWinchRopeClass()
            //        {

            //            Id = moorwinch.Id,
            //            RopeType = moorwinch.RopeType,
            //            Length = moorwinch.Length,
            //            DiaMeter = moorwinch.DiaMeter,
            //            WLL = moorwinch.WLL,
            //            LDBF = moorwinch.LDBF,
            //            MBL = moorwinch.MBL,
            //            ReasonOutofService = moorwinch.ReasonOutofService,
            //            OutofServiceDate = moorwinch.OutofServiceDate,
            //            DamageObserved = moorwinch.DamageObserved,
            //            InstalledDate = moorwinch.InstalledDate,
            //            MooringOperation = moorwinch.MooringOperation,
            //            ModifiedDate = DateTime.Now,
            //            IsActive = true,
            //            RopeConstruction = moorwinch.RopeConstruction,
            //            RopeTagging = moorwinch.RopeTagging,
            //            ReceivedDate = moorwinch.ReceivedDate,
            //            CertificateNumber = moorwinch.CertificateNumber,
            //            ManufacturerName = moorwinch.ManufacturerName,
            //            CreatedBy = moorwinch.CreatedBy
            //        };

            //        sc.Entry(UpdatedRopedetails).State = EntityState.Modified;
            //        sc.SaveChanges();



            //        StaticHelper.Editing = false;
            //        MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);


            //        CancelMooringWinch();

            //    }

            //}
            //else
            //{

            //    MooringWinchMessage = "Please Enter the MooringWinch Name";
            //    RaisePropertyChanged("MooringWinchMessage");
            //}

            //}
            //catch (Exception ex)
            //{
            //    sc.ErrorLog(ex);
            //}
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


        private void refreshmessage1(JoiningShackleClass cdc1)
        {
            JoiningShackleClass cdc = cdc1;
            CheckErrorMessage.CheckErrorMessages = true;

            AddLooseEQuipmentMessages = new AddLooseEMessages();
            RaisePropertyChanged("AddLooseEQuipmentMessages");

            AddLooseEMessages m = (AddLooseEQuipmentMessages as AddLooseEMessages); //DownCasting.....

            //cdc.LooseETypeId = cdc.LooseETypeId != 0 ? cdc.LooseETypeId : 0;
            cdc.UniqueID = cdc.UniqueID != null ? cdc.UniqueID.Trim() : string.Empty;
            cdc.ManufactureName = cdc.ManufactureName != null ? cdc.ManufactureName.Trim() : string.Empty;
            cdc.IsRopeInstalled = cdc.IsRopeInstalled != null ? cdc.IsRopeInstalled : null;
            cdc.MBL = cdc.MBL != null ? cdc.MBL : null;
            cdc.Type = cdc.Type != null ? cdc.Type : null;

            cdc.CertificateNumber = cdc.CertificateNumber != null ? cdc.CertificateNumber.Trim() : string.Empty;
            //cdc.ReceivedDate = cdc.ReceivedDate != null ? cdc.ReceivedDate : null;
            //cdc.InstalledDate = cdc.InstalledDate != null ? cdc.InstalledDate : null;
            //cdc.ReasonOutofService = cdc.ReasonOutofService != null ? cdc.ReasonOutofService.Trim() : string.Empty;
            //cdc.RopeTagging = cdc.RopeTagging != null ? cdc.RopeTagging.Trim() : string.Empty;

            //if (cdc.LooseETypeId == 0 || cdc.LooseETypeId == null)
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.LooseETypeMessage = "Please Select LooseE Type !";
            //    RaisePropertyChanged("AddLooseEQuipmentMessages");
            //}

            if (string.IsNullOrEmpty(cdc.Type))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                CheckErrorMessage.CheckErrorMessages = false;
                m.Type = "Please Choose Type !";
                RaisePropertyChanged("AddLooseEQuipmentMessages");
            }
            if (string.IsNullOrEmpty(cdc.UniqueID))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.IdentificationNMessage = "Please Enter Unique Identification No !";
                RaisePropertyChanged("AddLooseEQuipmentMessages");
            }
            if (string.IsNullOrEmpty(cdc.ManufactureName))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.ManufactureNameMessage = "Please Enter ManufactureName !";
                RaisePropertyChanged("AddLooseEQuipmentMessages");
            }
                if (cdc.MBL == null)
                {
                    CheckErrorMessage.CheckErrorMessages = false;
                    m.MBLMessage = "Please Enter MBL !";
                    RaisePropertyChanged("AddLooseEQuipmentMessages");
                }


                if (string.IsNullOrEmpty(cdc.IsRopeInstalled))
                {
                    MainViewModelWorkHours.CommonValue = true;
                    CheckErrorMessage.CheckErrorMessages = false;
                    m.IsRopeInstalled = "Please Choose at least 1 !";
                    RaisePropertyChanged("AddLooseEQuipmentMessages");
                }
                //if (cdc.IsRopeInstalled == "Yes")
                //{
                //    if (cdc.InstalledDate == null)
                //    {
                //        MainViewModelWorkHours.CommonValue = true;
                //        CheckErrorMessage.CheckErrorMessages = false;
                //        m.InstalledDateMessage = "Please Choose Installed Date !";
                //        RaisePropertyChanged("AddMooringWinchRopeMessages");
                //    }
                //}
                //if (string.IsNullOrEmpty(cdc.ReasonOutofService))
                //{
                //    CheckErrorMessage.CheckErrorMessages = false;
                //    m.ReasonoutofServiceMessage = "Please Enter ReasonoutofService !";
                //    RaisePropertyChanged("AddLooseEQuipmentMessages");
                //}
                if (string.IsNullOrEmpty(cdc.CertificateNumber))
                {
                    CheckErrorMessage.CheckErrorMessages = false;
                    m.CertificateNoMessage = "Please Enter Certificate Number !";
                    RaisePropertyChanged("AddLooseEQuipmentMessages");
                }
                //if (string.IsNullOrEmpty(cdc.ManufacturerName))
                //{
                //    CheckErrorMessage.CheckErrorMessages = false;
                //    m.ManufactureNameMessage = "Please Enter Manufacture Name !";
                //    RaisePropertyChanged("AddMooringWinchRopeMessages");
                //}


           // }
        }
        public static class CheckErrorMessage
        {
            public static bool CheckErrorMessages { get; set; }
            public static bool CheckErrorMessages1 { get; set; }
            public static bool CheckErrorMessages2 { get; set; }
            public static bool chkyoungs { get; set; }

        }

        public static string _sincrep;
        public string ComboValue
        {

            get
            {
                if (_sincrep != null)
                    AddJoiningShackleE.Type = _sincrep;


                return _sincrep;
            }

            set
            {
                _sincrep = value;
                if (_sincrep != null)
                    AddJoiningShackleE.Type = _sincrep;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue"));
            }
        }

        public static string _dmgob;
        public string ComboValue1
        {

            get
            {
                if (_dmgob != null)
                    AddJoiningShackleE.DamagedObserved = _dmgob;


                return _dmgob;
            }

            set
            {
                _dmgob = value;
                if (_dmgob != null)
                    AddJoiningShackleE.DamagedObserved = _dmgob;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue1"));
            }
        }

        public static string _moorO;
        public string ComboValue2
        {

            get
            {
                if (_moorO != null)
                    AddJoiningShackleE.MooringOperation = _moorO;


                return _moorO;
            }

            set
            {
                _moorO = value;
                if (_moorO != null)
                    AddJoiningShackleE.MooringOperation = _moorO;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue2"));
            }
        }

    }
}
