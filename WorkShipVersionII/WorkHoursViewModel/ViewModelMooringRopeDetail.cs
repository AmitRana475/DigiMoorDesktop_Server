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
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
  public  class ViewModelMooringRopeDetail:ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewModelMooringRopeDetail(MooringWinchRopeClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<MooringWinchRopeClass>(UpdateMooringWinch);
            cancelCommand = new RelayCommand(CancelMooringWinch);

            // MooringWinchRopeClass data = sc.MooringWinchRope.Where(x => x.Id == edeps.Id).FirstOrDefault();

            AddMooringWinchRope = new MooringWinchRopeClass()
            {
                Id = edeps.Id,
                Length = edeps.Length,
                MBL = edeps.MBL,
                LDBF = edeps.LDBF,
                WLL = edeps.WLL,
                DiaMeter = edeps.DiaMeter,
                //ManufacturerName = edeps.ManufacturerName,
                CertificateNumber = edeps.CertificateNumber,
                RopeTagging = edeps.RopeTagging,
                ModifiedDate = DateTime.Now,
                ModifiedBy = "Admin",
                CurrentRunningHours=edeps.CurrentRunningHours,
                StartCounterHours = edeps.StartCounterHours,
                Remarks =edeps.Remarks,
                InstalledDate1 = edeps.InstalledDate1,
                UniqueID = edeps.UniqueID
            };

            ComboValue7 = edeps.RopeTagging;
            // SRopeType = edeps.RopeTypeId;
            SRopeConst = edeps.RopeConstruction;
            //SRopeDiameter = edeps.DiaMeter;
            SRopeReasonoutofs = edeps.ReasonOutofService;
            SRopeMooringOpertaion = edeps.MooringOperation;
            SRopeDamageObserved = edeps.DamageObserved;
            ReceivedDate = edeps.ReceivedDate;
            
            //OutofServiceDate = edeps.OutofServiceDate;

            RaisePropertyChanged("Checking");
            RaisePropertyChanged("Checking1");

            if (edeps.InstalledDate1 == "Not Assigned")
            {
                Checking = true;
                Checking1 = false;
                Visible = "Hidden";
            }
            if (edeps.InstalledDate1 != "Not Assigned")
            {
                Checking = false;
                Checking1 = true;
                Visible = "Visible";
            }

            RopeTypeCombo1 abc = new RopeTypeCombo1()
            {
                Id = Convert.ToInt32(edeps.RopeTypeId),
                //CertificateNo = edeps.CertificateNumber
            };
            SRopeType = abc;

            ManuFacturerName ss = new ManuFacturerName()
            {
                Id = Convert.ToInt32(edeps.ManufacturerId),
            };
            SManuFName = ss;

            RaisePropertyChanged("AddMooringWinchRope");
            OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinchRope"));
            //EditMooringWinch(edeps);
        }
        public ViewModelMooringRopeDetail()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<MooringWinchRopeClass>(SaveMooringWinchRope);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            resetCommand = new RelayCommand(resetMooringRope);
            GetRopeType();
            manufname = GetManuFName();
            outofsreason = GetOutofSReason();
            damageobserved = GetDamageObserved();

            resetMooringRope();

            //AddMooringWinchRope = new MooringWinchRopeClass();
            //OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinchRope"));


        }

        private string visible;
        public string Visible
        {
            get
            {
                //if (checking != null)
                //{                  
                OnPropertyChanged(new PropertyChangedEventArgs("Visible"));
                //}
                return visible;
            }
            set
            {
                visible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Visible"));
            }
        }
        private bool checking;
        public bool Checking
        {
            get
            {
                //if (checking != null)
                //{                  
                    OnPropertyChanged(new PropertyChangedEventArgs("Checking"));
                //}
                return checking;
            }
            set
            {
                checking = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Checking"));
            }
        }


        private bool checking1;
        public bool Checking1
        {
            get
            {
                //if (checking1 != null)
                //{
                    OnPropertyChanged(new PropertyChangedEventArgs("Checking1"));
                //}
                return checking1;
            }
            set
            {
                checking1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Checking1"));
            }
        }


        public void resetMooringRope()
        {
            try
            {

                erinfo = 0;
                AddMooringWinchRope = new MooringWinchRopeClass();
                RaisePropertyChanged("AddMooringWinchRope");

                AddMooringWinchRopeMessages = new AddMooringRopeErrorMessages();
                RaisePropertyChanged("AddMooringWinchRopeMessages");


                ReceivedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("ReceivedDate");
               // InstalledDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("InstalledDate");

                ComboValue7 = null; RaisePropertyChanged("ComboValue7");
                SRopeType = null; RaisePropertyChanged("SRopeType");
                SManuFName = null; RaisePropertyChanged("SManuFName");
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private string mooringWinchMessage;
        public string MooringWinchMessage
        {
            get { return mooringWinchMessage; }
            set { mooringWinchMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("MooringWinchMessage")); }
        }

        #region Bind Ropetype
        //private static ObservableCollection<string> ropetype = new ObservableCollection<string>();
        //public ObservableCollection<string> RopeType
        //{
        //    get
        //    {
        //        return ropetype;
        //    }
        //    set
        //    {
        //        ropetype = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
        //    }
        //}

        //private ObservableCollection<string> GetRopeType()
        //{
        //    var AddRopeType = new ObservableCollection<string>();
        //    var data = sc.MooringRopeType.OrderBy(s => s.Id).Select(x => x.RopeType).ToList();

        //    foreach (var item in data)
        //    {
        //        AddRopeType.Add(item);

        //    }

        //    return AddRopeType;
        //}

        private static ObservableCollection<RopeTypeCombo1> ropetype = new ObservableCollection<RopeTypeCombo1>();
        public ObservableCollection<RopeTypeCombo1> RopeType
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
            // ObservableCollection<RopeTypeCombo1> AddRopeType = new ObservableCollection<RopeTypeCombo1>();
            ropetype.Clear();
            RopeTypeCombo1 rop;
            SqlDataAdapter adp = new SqlDataAdapter("select * from MooringRopeType", sc.con);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                rop = new RopeTypeCombo1();
                rop.Id = (int)row["Id"];
                rop.RopeType = (string)row["RopeType"];
                ropetype.Add(rop);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("ropetype"));
            //return AddRopeType;


        }

        #endregion

        #region Bind ManuFName
        private static ObservableCollection<ManuFacturerName> manufname = new ObservableCollection<ManuFacturerName>();
        public ObservableCollection<ManuFacturerName> ManuFName
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

        public ObservableCollection<ManuFacturerName> GetManuFName()
        {

            ObservableCollection<ManuFacturerName> AddManuFname = new ObservableCollection<ManuFacturerName>();
            ManuFacturerName rop;
            SqlDataAdapter adp = new SqlDataAdapter("select * from tblCommon where TYPE=1", sc.con);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                rop = new ManuFacturerName();
                rop.Id = (int)row["Id"];
                rop.Name = (string)row["Name"];
                AddManuFname.Add(rop);
            }
            return AddManuFname;
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

        public ObservableCollection<string> GetDamageObserved()
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
        public static MooringWinchRopeClass _AddMooringWinchRope = new MooringWinchRopeClass();
        public MooringWinchRopeClass AddMooringWinchRope
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


                return _AddMooringWinchRope;
            }
            set
            {
                _AddMooringWinchRope = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinchRope"));
            }
        }

        private static AddMooringRopeErrorMessages _AddMooringWinchRopeMessages = new AddMooringRopeErrorMessages();
        public AddMooringRopeErrorMessages AddMooringWinchRopeMessages
        {
            get
            {
                return _AddMooringWinchRopeMessages;
            }
            set
            {
                _AddMooringWinchRopeMessages = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinchRopeMessages"));
            }
        }
        public class AddMooringRopeErrorMessages
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
            public string ReceivedDateMessage { get; set; }
            public string InstalledDateMessage { get; set; }
            public string RopeTaggingMessage { get; set; }
            public string ReasonoutofServiceMessage { get; set; }

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

        //private static int sropetype;
        //public int SRopeType
        //{
        //    get
        //    {
        //        if (sropetype != 0)
        //            AddMooringWinchRope.RopeTypeId = sropetype;
        //        //if (sropetype == "MASTER")
        //        //    AddMooringWinchRope.RopeType = "All";

        //        return sropetype;
        //    }

        //    set
        //    {
        //        sropetype = value;
        //        if (sropetype != 0)
        //            AddMooringWinchRope.RopeTypeId = sropetype;
        //        OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
        //    }
        //}
        public class RopeTypeCombo1
        {
            public int Id { get; set; }
            public string RopeType { get; set; }
        }

        public static RopeTypeCombo1 sropetype;// = new Ropetypecombo();
        public RopeTypeCombo1 SRopeType
        {
            get
            {

                if (sropetype != null)
                {

                    AddMooringWinchRope.RopeTypeId = sropetype.Id;
                    OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinchRope"));
                }
                return sropetype;

            }
            set
            {

                sropetype = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SRopeType"));


            }
        }

        public class ManuFacturerName
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private static ManuFacturerName sropeManuFName;
        public ManuFacturerName SManuFName
        {
            get
            {
                if (sropeManuFName != null)
                    AddMooringWinchRope.ManufacturerId = sropeManuFName.Id;
                //if (sropeManuFName == "MASTER")
                //    AddMooringWinchRope.ManufacturerName = "All";

                return sropeManuFName;
            }

            set
            {
                sropeManuFName = value;
                if (sropeManuFName != null)
                    AddMooringWinchRope.ManufacturerId = sropeManuFName.Id;
                OnPropertyChanged(new PropertyChangedEventArgs("SManuFName"));
            }
        }

        private static string _sropeconsts;
        public string SRopeConst
        {
            get
            {
                if (_sropeconsts != null)
                    AddMooringWinchRope.RopeConstruction = _sropeconsts;
                if (_sropeconsts == "MASTER")
                    AddMooringWinchRope.RopeConstruction = "All";

                return _sropeconsts;
            }

            set
            {
                _sropeconsts = value;
                if (_sropeconsts != null)
                    AddMooringWinchRope.RopeConstruction = _sropeconsts;
                OnPropertyChanged(new PropertyChangedEventArgs("RopeConstruction"));
            }
        }
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
                    AddMooringWinchRope.ReasonOutofService = srreasonoutofservice;
                if (srreasonoutofservice == "MASTER")
                    AddMooringWinchRope.ReasonOutofService = "All";

                return srreasonoutofservice;
            }

            set
            {
                srreasonoutofservice = value;
                if (srreasonoutofservice != null)
                    AddMooringWinchRope.ReasonOutofService = srreasonoutofservice;
                OnPropertyChanged(new PropertyChangedEventArgs("ReasonOutofService"));
            }
        }

        private static string sdamageobserved;
        public string SRopeDamageObserved
        {
            get
            {
                if (sdamageobserved != null)
                    AddMooringWinchRope.DamageObserved = sdamageobserved;
                if (sdamageobserved == "MASTER")
                    AddMooringWinchRope.DamageObserved = "All";

                return sdamageobserved;
            }

            set
            {
                sdamageobserved = value;
                if (sdamageobserved != null)
                    AddMooringWinchRope.DamageObserved = sdamageobserved;
                OnPropertyChanged(new PropertyChangedEventArgs("DamageObserved"));
            }
        }

        private static string smooringoperation;
        public string SRopeMooringOpertaion
        {
            get
            {
                if (smooringoperation != null)
                    AddMooringWinchRope.MooringOperation = smooringoperation;
                if (smooringoperation == "MASTER")
                    AddMooringWinchRope.MooringOperation = "All";

                return smooringoperation;
            }

            set
            {
                smooringoperation = value;
                if (smooringoperation != null)
                    AddMooringWinchRope.MooringOperation = smooringoperation;
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
                _AddMooringWinchRope.ReceivedDate = (DateTime)_ReceivedDate;
                return _ReceivedDate;
            }
            set
            {
                _ReceivedDate = value;
                RaisePropertyChanged("ReceivedDate");
            }
        }

        private static Nullable<DateTime> _InstallDate = null;
        public Nullable<DateTime> InstalledDate
        {
            get
            {
                //if (_InstallDate == null)
                //{
                //  //  _InstallDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                //}
                //_AddMooringWinchRope.InstalledDate = (DateTime)_InstallDate;
                return _InstallDate;
            }
            set
            {
                _InstallDate = value;
                RaisePropertyChanged("InstalledDate");
            }
        }

        //private static Nullable<DateTime> _OutofServiceDate = null;
        //public Nullable<DateTime> OutofServiceDate
        //{
        //    get
        //    {
        //        if (_OutofServiceDate == null)
        //        {
        //            _OutofServiceDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        //        }
        //        _AddMooringWinchRope.OutofServiceDate = (DateTime)_OutofServiceDate;
        //        return _InstallDate;
        //    }
        //    set
        //    {
        //        _OutofServiceDate = value;
        //        RaisePropertyChanged("OutofServiceDate");
        //    }
        //}
        #endregion
        private void SaveMooringWinchRope(MooringWinchRopeClass moorwinchrope)
        {
            try
            {
                refreshmessage1(moorwinchrope);
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

                    //moorwinchrope.RopeType = textinfo.ToTitleCase(moorwinchrope.RopeType.ToLower());
                    moorwinchrope.RopeConstruction = moorwinchrope.RopeConstruction;
                    moorwinchrope.DiaMeter = Convert.ToDecimal(moorwinchrope.DiaMeter);
                    moorwinchrope.Length = Convert.ToDecimal(moorwinchrope.Length);
                    moorwinchrope.MBL = Convert.ToDecimal(moorwinchrope.MBL);
                    moorwinchrope.LDBF = Convert.ToDecimal(moorwinchrope.LDBF);
                    moorwinchrope.WLL = Convert.ToDecimal(moorwinchrope.WLL);
                    // moorwinchrope.ManufacturerName = textinfo.ToTitleCase(moorwinchrope.ManufacturerName.ToLower());
                    moorwinchrope.CertificateNumber = moorwinchrope.CertificateNumber;
                    moorwinchrope.ReceivedDate = Convert.ToDateTime(moorwinchrope.ReceivedDate);
                    moorwinchrope.InstalledDate = Convert.ToDateTime(moorwinchrope.InstalledDate);
                    moorwinchrope.RopeTagging = moorwinchrope.RopeTagging;

                    if (moorwinchrope.IsRopeOutOfS != "No" && moorwinchrope.IsRopeOutOfS != null)
                    {
                        moorwinchrope.OutofServiceDate = Convert.ToDateTime(moorwinchrope.OutofServiceDate);
                    }
                    if (moorwinchrope.IsRopeOutOfS == "No" || moorwinchrope.IsRopeOutOfS == null)
                    {
                        moorwinchrope.OutofServiceDate = null;
                    }

                    if (moorwinchrope.ReasonOutofService != null)
                    {
                        moorwinchrope.ReasonOutofService = moorwinchrope.ReasonOutofService;
                    }
                    if (moorwinchrope.OtherReason != null)
                    {
                        moorwinchrope.OtherReason = moorwinchrope.OtherReason;
                    }
                    if (moorwinchrope.DamageObserved != null)
                    {
                        moorwinchrope.DamageObserved = moorwinchrope.DamageObserved;
                    }
                    if (moorwinchrope.MooringOperation != null)
                    {
                        moorwinchrope.MooringOperation = moorwinchrope.MooringOperation;
                    }
                    moorwinchrope.CreatedDate = DateTime.Now;
                    moorwinchrope.CreatedBy = "Admin";
                    moorwinchrope.IsActive = true;

                    moorwinchrope.RopeTail = 0;
                    moorwinchrope.DeleteStatus = false;

                    RopeDamageRecordClass rpdm = new RopeDamageRecordClass();

                    sc.MooringWinchRope.Add(moorwinchrope);
                    sc.SaveChanges();
                    StaticHelper.Editing = false;
                    MessageBox.Show("Record saved successfully ", "Add MooringWinch Rope", MessageBoxButton.OK, MessageBoxImage.Information);

                    var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.Where(x => x.DeleteStatus == false).ToList());
                    MooringWinchRopeViewModel vm = new MooringWinchRopeViewModel(lostdata);
                    //ChildWindowManager.Instance.ShowChildWindow();
                    ChildWindowManager.Instance.CloseChildWindow();
                    //ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchRopeView() { DataContext = vm });




                    CancelMooringWinch();
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
        private void UpdateMooringWinch(MooringWinchRopeClass moorwinch)
        {
            try
            {
                refreshmessage1(moorwinch);


                if (!string.IsNullOrEmpty(Lblmessage))
                    CheckErrorMessage.CheckErrorMessages = false;

                if (CheckErrorMessage.CheckErrorMessages)
                {

                    var local = sc.Set<MooringWinchRopeClass>()
                 .Local
                 .FirstOrDefault(f => f.Id == moorwinch.Id);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }

                    var UpdatedRopedetails = new MooringWinchRopeClass()
                    {

                        Id = moorwinch.Id,
                        RopeTypeId = SRopeType.Id,
                        ManufacturerId = SManuFName.Id,
                        Length = moorwinch.Length,
                        DiaMeter = moorwinch.DiaMeter,
                        WLL = moorwinch.WLL,
                        LDBF = moorwinch.LDBF,
                        MBL = moorwinch.MBL,
                        ReasonOutofService = moorwinch.ReasonOutofService,
                        OutofServiceDate = moorwinch.OutofServiceDate,
                        DamageObserved = moorwinch.DamageObserved,
                        InstalledDate = moorwinch.InstalledDate,
                        MooringOperation = moorwinch.MooringOperation,
                        ModifiedDate = DateTime.Now,
                        IsActive = true,
                        RopeConstruction = moorwinch.RopeConstruction,
                        RopeTagging = moorwinch.RopeTagging,
                        ReceivedDate = moorwinch.ReceivedDate,
                        CertificateNumber = moorwinch.CertificateNumber,
                        //ManufacturerName = moorwinch.ManufacturerName,
                        CreatedBy = moorwinch.CreatedBy,
                        RopeTail = 0
                    };

                    sc.Entry(UpdatedRopedetails).State = EntityState.Modified;
                    sc.SaveChanges();



                    StaticHelper.Editing = false;
                    MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);

                    //var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.ToList());
                    //MooringWinchRopeViewModel vm = new MooringWinchRopeViewModel(lostdata);

                    //ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchRopeView() { DataContext = vm });

                    CancelMooringWinch();

                }

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
            var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.Where(x => x.DeleteStatus == false).ToList());
            MooringWinchRopeViewModel cc = new MooringWinchRopeViewModel(lostdata);
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


        private void refreshmessage1(MooringWinchRopeClass cdc1)
        {
            MooringWinchRopeClass cdc = cdc1;
            CheckErrorMessage.CheckErrorMessages = true;

            AddMooringWinchRopeMessages = new AddMooringRopeErrorMessages();
            RaisePropertyChanged("AddMooringWinchRopeMessages");

            AddMooringRopeErrorMessages m = (AddMooringWinchRopeMessages as AddMooringRopeErrorMessages); //DownCasting.....

            cdc.RopeTypeId = cdc.RopeTypeId != 0 ? cdc.RopeTypeId : 0;
            cdc.RopeConstruction = cdc.RopeConstruction != null ? cdc.RopeConstruction.Trim() : string.Empty;
            cdc.DiaMeter = cdc.DiaMeter != null ? cdc.DiaMeter : null;
            cdc.Length = cdc.Length != null ? cdc.Length : null;
            cdc.MBL = cdc.MBL != null ? cdc.MBL : null;
            //cdc.LDBF = cdc.LDBF != null ? cdc.LDBF : null;
            //cdc.WLL = cdc.WLL != null ? cdc.WLL : null;
            cdc.CertificateNumber = cdc.CertificateNumber != null ? cdc.CertificateNumber.Trim() : string.Empty;
            cdc.ReceivedDate = cdc.ReceivedDate != null ? cdc.ReceivedDate : null;
            //cdc.InstalledDate = cdc.InstalledDate != null ? cdc.InstalledDate : null;
            //cdc.ReasonOutofService = cdc.ReasonOutofService != null ? cdc.ReasonOutofService.Trim() : string.Empty;
            cdc.RopeTagging = cdc.RopeTagging != null ? cdc.RopeTagging.Trim() : string.Empty;

            if (cdc.RopeTypeId == 0)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.RopeTypeMessage = "Please Select RopeType !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (string.IsNullOrEmpty(cdc.RopeConstruction))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.RopeConstructionMessage = "Please Select Rope Construction !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (cdc.DiaMeter == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.DiaMeterMessage = "Please Enter DiaMeter !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (cdc.Length == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.LengthMessage = "Please Enter Length !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (cdc.MBL == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.MBLMessage = "Please Enter MBL !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
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
            if (cdc.ReceivedDate == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.ReceivedDateMessage = "Please Choose Received Date !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (cdc.InstalledDate == null)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.InstalledDateMessage = "Please Choose Installed Date !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (string.IsNullOrEmpty(cdc.RopeTagging))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.RopeTaggingMessage = "Please Enter Rope Tagging !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            //if (string.IsNullOrEmpty(cdc.ReasonOutofService))
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.ReasonoutofServiceMessage = "Please Enter ReasonoutofService !";
            //    RaisePropertyChanged("AddMooringWinchRopeMessages");
            //}
            if (string.IsNullOrEmpty(cdc.CertificateNumber))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.CertificateNoMessage = "Please Enter Certificate Number !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (cdc.ManufacturerId == 0)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.ManufactureNameMessage = "Please Enter Manufacture Name !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
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
        public string ComboValue1
        {

            get
            {
                if (_sincrep != null)
                    AddMooringWinchRope.IncidentReport = _sincrep;


                return _sincrep;
            }

            set
            {
                _sincrep = value;
                if (_sincrep != null)
                    AddMooringWinchRope.IncidentReport = _sincrep;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue1"));
            }
        }

        public static string _sinroptagg;
        public string ComboValue7
        {

            get
            {
                if (_sinroptagg != null)
                    AddMooringWinchRope.RopeTagging = _sinroptagg;


                return _sinroptagg;
            }

            set
            {
                _sinroptagg = value;
                if (_sinroptagg != null)
                    AddMooringWinchRope.RopeTagging = _sinroptagg;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue7"));
            }
        }

    }
}
