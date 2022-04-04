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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class AddMooringWinchRopeViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddMooringWinchRopeViewModel(MooringWinchRopeClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;
            _BrowseCommand = new RelayCommand(BrowseMethod);
            saveCommand = new RelayCommand<MooringWinchRopeClass>(UpdateMooringWinch);
            cancelCommand = new RelayCommand(CancelMooringWinch);

            // MooringWinchRopeClass data = sc.MooringWinchRope.Where(x => x.Id == edeps.Id).FirstOrDefault();
            //IsEnabledCheck = false;

            IsEnabledCheck = true;

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
                UniqueID = edeps.UniqueID,
                RopeTagging = edeps.RopeTagging,
                ModifiedDate = DateTime.Now,
                ModifiedBy = "Admin",
                CurrentRunningHours = edeps.StartCounterHours,
                StartCounterHours = edeps.StartCounterHours,
                Remarks = edeps.Remarks,
                AttachmentPath=edeps.AttachmentPath
                
            };

            ComboValue7 = edeps.RopeTagging;
            // SRopeType = edeps.RopeTypeId;
            SRopeConst = edeps.RopeConstruction;
            //SRopeDiameter = edeps.DiaMeter;
            SRopeReasonoutofs = edeps.ReasonOutofService;
            SRopeMooringOpertaion = edeps.MooringOperation;
            SRopeDamageObserved = edeps.DamageObserved;
            ReceivedDate = edeps.ReceivedDate;
            InstalledDate = edeps.InstalledDate;
            OutofServiceDate = edeps.OutofServiceDate;

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
            // resetMooringRope();
            //EditMooringWinch(edeps);
        }
        public AddMooringWinchRopeViewModel()
        {
            IsEnabledCheck = true;
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;
            _BrowseCommand = new RelayCommand(BrowseMethod);
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
        private ICommand _BrowseCommand;
        public ICommand BrowseCommand
        {
            get
            {
                return _BrowseCommand;
            }
        }
        public void resetMooringRope()
        {
            try
            {

                IsEnabledCheck = true;
                MainViewModelWorkHours.CommonValue = false;
                erinfo = 0;
                AddMooringWinchRope = new MooringWinchRopeClass();
                RaisePropertyChanged("AddMooringWinchRope");

                AddMooringWinchRopeMessages = new AddMooringRopeErrorMessages();
                RaisePropertyChanged("AddMooringWinchRopeMessages");


                ReceivedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("ReceivedDate");
                InstalledDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("InstalledDate");

                ComboValue7 = null; RaisePropertyChanged("ComboValue7");
                SRopeType = null; RaisePropertyChanged("SRopeType");
                SManuFName = null; RaisePropertyChanged("SManuFName");

                saveCommand = new RelayCommand<MooringWinchRopeClass>(SaveMooringWinchRope);
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
            //public string MBLMessage { get; set; }
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
                if (_InstallDate == null)
                {
                    _InstallDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                _AddMooringWinchRope.InstalledDate = (DateTime)_InstallDate;
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
                _AddMooringWinchRope.OutofServiceDate = (DateTime)_OutofServiceDate;
                return _InstallDate;
            }
            set
            {
                _OutofServiceDate = value;
                RaisePropertyChanged("OutofServiceDate");
            }
        }
        #endregion

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
        
        private void BrowseMethod()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".docx";
            dlg.Filter = "Image files (*.png;*.jpeg;*.jpg;*.tiff)|*.png;*.jpeg;*.jpg;*.tiff|PDF files (*.pdf)|*.pdf;|Excel Files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm;|Word Files (*.doc;*.docx)|*.doc;*.docx";
            //"Excel Files|*.xls;*.xlsx;*.xlsm;*.pdf;*.jpg;*.png;*.jpeg";

            // Display OpenFileDialog by calling ShowDialog method
            //Nullable<bool> result1 = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox


            if (dlg.ShowDialog() == DialogResult.OK)
            {

                var size = new FileInfo(dlg.FileName).Length / 1024;

                if(size  >1024)
                {
                    System.Windows.Forms.MessageBox.Show("Attachment Size can not exceed 1MB !.", "Mooring Line", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                AddMooringWinchRope.AttachmentPath = dlg.FileName;
                RaisePropertyChanged("AddMooringWinchRope");

            }
        }

        private void SaveMooringWinchRope(MooringWinchRopeClass moorwinchrope)
        {
            try
            {
                MainViewModelWorkHours.CommonValue = false;
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

                    var duplicatecheck = sc.MooringWinchRope.Where(x => x.UniqueID == moorwinchrope.UniqueID && x.RopeTail == 0).Count();

                    if (duplicatecheck == 0)
                    {
                       //// moorwinchrope.RopeType = textinfo.ToTitleCase(moorwinchrope.RopeType.ToLower());

                        //moorwinchrope.OutOfService_WinchId = 0;
                        if (!string.IsNullOrEmpty(moorwinchrope.AttachmentPath))
                        {
                            moorwinchrope.AttachmentPath = moorwinchrope.AttachmentPath;
                            string ServerName = StaticHelper.ServerName;
                            string mypath = ServerName + "\\DigiMoorDB_Backup\\Attachment";



                            // string mypath = @"C:\DigiMoorDB_Backup\Attachment";
                            //string subdir = @"C:\Temp\Mahesh";
                            // If directory does not exist, create it. 
                            if (!Directory.Exists(mypath))
                            {
                                Directory.CreateDirectory(mypath);
                            }

                            // Guid g = Guid.NewGuid();
                            Random randon = new Random();
                            int num = randon.Next(10000);


                            if (!string.IsNullOrEmpty(moorwinchrope.AttachmentPath))
                            {
                                if (File.Exists(moorwinchrope.AttachmentPath))
                                {

                                    
                                    string fileExtention = Path.GetExtension(moorwinchrope.AttachmentPath);//Path.GetFileName(AddDepartment.AttachmentPath);

                                    string[] ExtList = { ".xls", ".xlsx", ".wmv", ".xlsm", ".pdf", ".mp4", ".jpg", ".png", ".jpeg", ".tiff", ".doc", ".docx" };
                                    if (ExtList.Contains(fileExtention.ToLower()))
                                    {

                                        //var count = sc.TrainingAttachment.Count();
                                        //if (count <= 9)
                                        //{


                                        string fileName = Path.GetFileName(moorwinchrope.AttachmentPath);
                                        var withoutextnsn = Path.GetFileNameWithoutExtension(fileName) + num;

                                        fileName = withoutextnsn + fileExtention;

                                        mypath = mypath + "\\" + fileName;
                                        //File.Delete(sfd.FileName);

                                        var ss = moorwinchrope.AttachmentPath;

                                        //AddDepartment.AttachmentPath = mypath;
                                        moorwinchrope.AttachmentPath = fileName;


                                      

                                        //sc.MooringWinchRope.Add(moorwinchrope);
                                        //sc.SaveChanges();

                                        try
                                        {
                                            File.Copy(ss, mypath);
                                        }
                                        catch (Exception ex)
                                        {
                                            sc.ErrorLog(ex);
                                        }

                                       

                                        //RefreshShipAttach();

                                        ////StaticHelper.Editing = false;
                                        //System.Windows.Forms.MessageBox.Show("Record saved successfully ", "Add Rope Attachment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //moorwinchrope = new MooringWinchRopeClass();
                                        //RaisePropertyChanged("AddMooringWinchRope");
                                        //CancelMooringWinch();
                                        //}else
                                        //{
                                        //    MessageBox.Show("Attachment could not more than 9", "Add Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        //}
                                    }
                                    else
                                    {
                                        System.Windows.Forms.MessageBox.Show("Invalid Attachment file.", "Add Rope Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                //else
                                //{
                                //    File.Copy(obj.AttachmentPath, sfd.FileName);
                                //}



                            }
                            //else
                            //{
                            //    System.Windows.Forms.MessageBox.Show("Please Browse the file for Attachment ", "Add Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //}
                        }
                        //else
                        //{
                        //    System.Windows.Forms.MessageBox.Show("Please enter the file name ", "Add Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}
                        moorwinchrope.RopeConstruction = moorwinchrope.RopeConstruction;
                        moorwinchrope.DiaMeter = Convert.ToDecimal(moorwinchrope.DiaMeter);
                        moorwinchrope.Length = Convert.ToDecimal(moorwinchrope.Length);
                        moorwinchrope.MBL = Convert.ToDecimal(moorwinchrope.MBL);
                        moorwinchrope.LDBF = Convert.ToDecimal(moorwinchrope.LDBF);
                        moorwinchrope.WLL = Convert.ToDecimal(moorwinchrope.WLL);
                        // moorwinchrope.ManufacturerName = textinfo.ToTitleCase(moorwinchrope.ManufacturerName.ToLower());
                        moorwinchrope.CertificateNumber = moorwinchrope.CertificateNumber;
                        moorwinchrope.UniqueID = moorwinchrope.UniqueID;
                        moorwinchrope.ReceivedDate = Convert.ToDateTime(moorwinchrope.ReceivedDate);
                        moorwinchrope.InstalledDate = Convert.ToDateTime(moorwinchrope.InstalledDate);
                        moorwinchrope.RopeTagging = moorwinchrope.RopeTagging;
                       // moorwinchrope.RopeAttachment = moorwinchrope.RopeAttachment;
                        moorwinchrope.StartCounterHours = moorwinchrope.CurrentRunningHours;
                        if (moorwinchrope.IsRopeOutOfS != "No" && moorwinchrope.IsRopeOutOfS != null)
                        {
                            moorwinchrope.OutofServiceDate = Convert.ToDateTime(moorwinchrope.OutofServiceDate);
                        }
                        if (moorwinchrope.IsRopeOutOfS == "No" || moorwinchrope.IsRopeOutOfS == null)
                        {
                            moorwinchrope.OutofServiceDate = null;
                        }
                        if (moorwinchrope.Remarks == null)
                        {
                            moorwinchrope.Remarks = null;
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
                        if (moorwinchrope.IsRopeInstalled == "No")
                        {
                            moorwinchrope.InstalledDate = null;
                        }
                      
                        moorwinchrope.CreatedDate = DateTime.Now;
                        moorwinchrope.CreatedBy = "Admin";
                        moorwinchrope.IsActive = true;

                        moorwinchrope.RopeTail = 0;
                        moorwinchrope.DeleteStatus = false;

                        RopeDamageRecordClass rpdm = new RopeDamageRecordClass();

                        try
                        {
                            if (moorwinchrope.InstalledDate != null)
                            {

                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblRopeInspectionSetting where MooringRopeType=" + moorwinchrope.RopeTypeId + " and ManufacturerType=" + moorwinchrope.ManufacturerId + "", sc.con);
                                DataTable dt = new DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    //decimal rating1 = Convert.ToDecimal(dt.Rows[0]["Rating1"]);
                                    //int rat = Convert.ToInt32(rating1);

                                    decimal datecheck = Convert.ToDecimal(dt.Rows[0]["Rating1"]);
                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["Rating1"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    //DateTime inspectduedate = Convert.ToDateTime(moorwinchrope.InstalledDate).AddDays(near);


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
                                    DateTime date = Convert.ToDateTime(moorwinchrope.InstalledDate);
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
                                    TimeSpan t = nextMonth - date;
                                    double NrOfDays = t.TotalDays;

                                    DateTime inspectduedate = Convert.ToDateTime(moorwinchrope.InstalledDate).AddDays(NrOfDays);



                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }

                                    moorwinchrope.InspectionDueDate = inspectduedate;
                                }
                            }
                        }
                        catch (Exception ex) { }

                        //SqlDataAdapter adp5 = new SqlDataAdapter("select COUNT(*) from MooringRopeDetail where OutofServiceDate is null and RopeTail=0 and DeleteStatus=0", sc.con);
                        //DataTable dt5 = new DataTable();
                        //adp5.Fill(dt5);
                        //if (dt5.Rows.Count >= 0)
                        //{
                        //int cnt = Convert.ToInt32(dt5.Rows[0][0]);
                        //if (cnt < StaticHelper.MinimumRope)
                        //{

                        sc.MooringWinchRope.Add(moorwinchrope);
                        sc.SaveChanges();

                        StaticHelper.Editing = false;
                        System.Windows.MessageBox.Show("Record saved successfully ", "Mooring Line Details", MessageBoxButton.OK, MessageBoxImage.Information);


                        var ropeid = ((from mrop in sc.MooringWinchRope select (int?)mrop.Id).Max() ?? 0);


                        try
                        {
                            using (SqlDataAdapter adp = new SqlDataAdapter("InsertRopeAttachment", sc.con))
                            {
                                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                adp.SelectCommand.Parameters.AddWithValue("@AttachmentPath", moorwinchrope.AttachmentPath);
                                adp.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                                adp.SelectCommand.Parameters.AddWithValue("@LineResidual", "Line");
                                adp.SelectCommand.Parameters.AddWithValue("@IsActive", true);
                                adp.SelectCommand.Parameters.AddWithValue("@DeleteStatus", false);
                                adp.SelectCommand.Parameters.AddWithValue("@ResidualID", 0);
                                DataTable dtt = new DataTable();
                                adp.Fill(dtt);
                            }
                        }
                        catch (Exception ex) { }


                        //using (SqlDataAdapter ap = new SqlDataAdapter("INSERT INTO MooringRopeAttachment VALUES (RopeId, RopeAttachment, 'True')", sc.con))
                        //{

                        //}


                        //}
                        //else
                        //{
                        //    MessageBox.Show("Rope not allowed more than "+StaticHelper.MinimumRope+". ", "Mooring Line Details", MessageBoxButton.OK, MessageBoxImage.Information);
                        //    MainViewModelWorkHours.CommonValue = true;
                        //    return;
                        //}
                        //}

                        Below21RopesAtDeleteTime();
                        var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.Where(x => x.DeleteStatus == false).ToList());
                        MooringWinchRopeViewModel vm = new MooringWinchRopeViewModel(lostdata);
                        //ChildWindowManager.Instance.ShowChildWindow();
                        ChildWindowManager.Instance.CloseChildWindow();
                        //ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchRopeView() { DataContext = vm });


                        resetMooringRope();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("UniqueID already exist !", "Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainViewModelWorkHours.CommonValue = true;
                    }

                    // CancelMooringWinch();
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

        private void Below21RopesAtDeleteTime()
        {
            try
            {   // minimum required of 21 Ropes ---------------------------------
                SqlDataAdapter adp = new SqlDataAdapter("select COUNT(*) from MooringRopeDetail where OutofServiceDate is null and RopeTail=0 and DeleteStatus=0", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count >= 0)
                {
                    int cnt = Convert.ToInt32(dt.Rows[0][0]);
                    if (cnt < StaticHelper.MinimumRope)
                    {
                        var notification = "Active lines below minimum required of " + StaticHelper.MinimumRope + " Lines including spare";

                        SqlDataAdapter act = new SqlDataAdapter("select COUNT(*) from Notifications where Notification='Active lines below minimum required of " + StaticHelper.MinimumRope + " Lines including spare'", sc.con);
                        DataTable dd = new DataTable();
                        act.Fill(dd);

                        int cntnoti = Convert.ToInt32(dd.Rows[0][0]);
                        if (cntnoti == 0)
                        {
                            NotificationsClass noti = new NotificationsClass();
                            noti.Acknowledge = false;
                            noti.AckRecord = "Not yet acknowledged";
                            //noti.AckRecord = "Please insert minimum of {21} active ropes";
                            noti.Notification = notification;
                            noti.RopeId = 0;
                            noti.IsActive = true;
                            noti.NotificationType = 1;
                            //noti.NotificationDueDate = notidueMonth;
                            noti.CreatedDate = DateTime.Now;
                            noti.CreatedBy = "Admin";
                            noti.NotificationAlertType = (int)NotificationAlertType.Minimum21RopeCount;
                            sc.Notifications.Add(noti);
                            sc.SaveChanges();
                        }

                        act.Dispose();
                        dd.Dispose();
                    }
                    else
                    {
                        // delete Above Notification
                        SqlDataAdapter act = new SqlDataAdapter("delete from Notifications where NotificationAlertType = 17", sc.con);
                        DataTable dd = new DataTable();
                        act.Fill(dd);
                        act.Dispose();
                        dd.Dispose();
                    }
                }

                dt.Dispose();
                adp.Dispose();


            }
            catch { }
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

                if (!string.IsNullOrEmpty(AddMooringWinchRope.AttachmentPath))
                {
                    AddMooringWinchRope.AttachmentPath = AddMooringWinchRope.AttachmentPath;
                    string ServerName = StaticHelper.ServerName;
                    string mypath = ServerName + "\\DigiMoorDB_Backup\\Attachment";



                    // string mypath = @"C:\DigiMoorDB_Backup\Attachment";
                    //string subdir = @"C:\Temp\Mahesh";
                    // If directory does not exist, create it. 
                    if (!Directory.Exists(mypath))
                    {
                        Directory.CreateDirectory(mypath);
                    }

                    // Guid g = Guid.NewGuid();
                    Random randon = new Random();
                    int num = randon.Next(10000);


                    if (!string.IsNullOrEmpty(AddMooringWinchRope.AttachmentPath))
                    {
                        if (File.Exists(AddMooringWinchRope.AttachmentPath))
                        {
                            string fileExtention = Path.GetExtension(AddMooringWinchRope.AttachmentPath);//Path.GetFileName(AddDepartment.AttachmentPath);

                            string[] ExtList = { ".xls", ".xlsx", ".wmv", ".xlsm", ".pdf", ".mp4", ".jpg", ".png", ".jpeg", ".tiff", ".doc", ".docx" };
                            if (ExtList.Contains(fileExtention.ToLower()))
                            {

                                try
                                {
                                    SqlDataAdapter adp223 = new SqlDataAdapter("select * from MooringRopeAttachment where ropeid='" + moorwinch.Id + "' and LineResidual='Line' and RopeTail=0", sc.con);
                                    DataTable dt223 = new DataTable();
                                    adp223.Fill(dt223);
                                    if (dt223.Rows.Count > 0)
                                    {
                                        string filename = dt223.Rows[0]["AttachmentPath"].ToString();                                      
                                        string mypath1 = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + filename + "";
                                        FileInfo file = new FileInfo(mypath1);
                                        if (file.Exists)//check file exsit or not  
                                        {
                                            file.Delete();   
                                        }
                                    }
                                }
                                catch { }

                                string fileName = Path.GetFileName(AddMooringWinchRope.AttachmentPath);
                                var withoutextnsn = Path.GetFileNameWithoutExtension(fileName) + num;

                                fileName = withoutextnsn + fileExtention;

                                mypath = mypath + "\\" + fileName;
                                //File.Delete(sfd.FileName);

                                var ss = AddMooringWinchRope.AttachmentPath;

                                //AddDepartment.AttachmentPath = mypath;
                                AddMooringWinchRope.AttachmentPath = fileName;


                                //sc.MooringWinchRope.Add(AddMooringWinchRope);
                                //sc.SaveChanges();

                                try
                                {
                                    File.Copy(ss, mypath);
                                }
                                catch (Exception ex) { }

                              
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Invalid Attachment file.", "Add Rope Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        //else
                        //{
                        //    File.Copy(obj.AttachmentPath, sfd.FileName);
                        //}



                    }
                    //else
                    //{
                    //    System.Windows.Forms.MessageBox.Show("Please Browse the file for Attachment ", "Add Rope Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
                //else
                //{
                //    System.Windows.Forms.MessageBox.Show("Please enter the file name ", "Add Rope Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                DateTime? instaldate = null; DateTime? instaldate1 = null; DateTime? recedate3 = null;
                DateTime? recedate = null; DateTime? recedate1 = null; DateTime? recedate2 = null;


                MainViewModelWorkHours.CommonValue = false;
                refreshmessage1(moorwinch);


                if (!string.IsNullOrEmpty(Lblmessage))
                    CheckErrorMessage.CheckErrorMessages = false;

                if (CheckErrorMessage.CheckErrorMessages)
                {

                    //var duplicatecheck = sc.MooringWinchRope.Where(x => x.UniqueID == moorwinch.UniqueID && x.RopeTail == 0).Count();

                    //if (duplicatecheck == 0)
                    //{


                    var local = sc.Set<MooringWinchRopeClass>()
             .Local
             .FirstOrDefault(f => f.Id == moorwinch.Id);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }

                    decimal crntRhrs = 0;
                    SqlDataAdapter adp11 = new SqlDataAdapter("select StartCounterHours,CurrentRunningHours from mooringropedetail where id =" + moorwinch.Id + "", sc.con);
                    DataTable dt11 = new DataTable();
                    adp11.Fill(dt11);
                    if (dt11.Rows.Count > 0)
                    {
                        // int cntr = Convert.ToInt32(dt11.Rows[0][0] == DBNull.Value ? 0 : dt11.Rows[0][0]);
                        decimal cntr = Convert.ToDecimal(dt11.Rows[0][0] == DBNull.Value ? 0 : dt11.Rows[0][0]);
                        //int cntr = Convert.ToInt32(dt11.Rows[0][0]);
                        crntRhrs = Convert.ToDecimal(dt11.Rows[0][1] == DBNull.Value ? 0 : dt11.Rows[0][1]);
                        // crntRhrs = Convert.ToInt32(dt11.Rows[0][1]);
                        //int cntr1 = Convert.ToInt32(moorwinch.CurrentRunningHours);
                        decimal cntr1 = Convert.ToDecimal(moorwinch.CurrentRunningHours);

                        if (cntr > cntr1)
                        {
                            cntr = cntr - cntr1;
                            crntRhrs = crntRhrs - cntr;
                        }
                        else if (cntr < cntr1)
                        {
                            cntr = cntr1 - cntr;
                            crntRhrs = crntRhrs + cntr;
                        }
                    }

                    //SqlDataAdapter adp2 = new SqlDataAdapter("select * from MooringRopeInspection where RopeId=" + moorwinch.Id + "", sc.con);
                    //DataTable dt2 = new DataTable();
                    //adp2.Fill(dt2);
                    //if (dt2.Rows.Count > 0)
                    //{
                    //    MessageBox.Show("This rope not update because this rope already inspect", "Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Information);
                    //}
                    //else
                    //{

                    if (moorwinch.IsRopeInstalled == "No")
                    {
                        moorwinch.InstalledDate = null;
                        moorwinch.InspectionDueDate = null; ;
                    }
                    else
                    {
                        try
                        {
                            SqlDataAdapter adp = new SqlDataAdapter("select * from tblRopeInspectionSetting where MooringRopeType=" + moorwinch.RopeTypeId + " and ManufacturerType=" + moorwinch.ManufacturerId + "", sc.con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                decimal rating1 = Convert.ToDecimal(dt.Rows[0]["Rating1"]);
                                int rat = Convert.ToInt32(rating1);

                                decimal datecheck = Convert.ToDecimal(dt.Rows[0]["Rating1"]);
                                decimal perchk = Convert.ToDecimal(dt.Rows[0]["Rating1"]) * 30 / 100;
                                perchk = perchk * 100;
                                int near = Convert.ToInt32(perchk);
                                //DateTime inspectduedate = Convert.ToDateTime(moorwinch.InstalledDate).AddDays(near);

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
                                DateTime date = Convert.ToDateTime(moorwinch.InstalledDate);
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
                                //DateTime date = Convert.ToDateTime(moorwinch.InstalledDate);
                                //DateTime nextMonth = date.AddMonths(chekint);
                                TimeSpan t = nextMonth - date;
                                double NrOfDays = t.TotalDays;
                                DateTime inspectduedate = Convert.ToDateTime(moorwinch.InstalledDate).AddDays(NrOfDays);

                                DateTime crntdt = DateTime.Now;
                                if (inspectduedate <= crntdt)
                                {
                                    inspectduedate = DateTime.Now;
                                }

                                moorwinch.InspectionDueDate = inspectduedate;
                            }
                        }
                        catch { }
                    }

                    try
                    {

                        try
                        {
                            if (moorwinch.IsRopeInstalled == "Yes")
                            {

                                SqlDataAdapter adpp = new SqlDataAdapter("select max(EndtoEndDoneDate) as EndtoEndDoneDate from RopeEndtoEnd2 where RopeId=" + moorwinch.Id + " and IsActive=1", sc.con);
                                DataTable ddt = new DataTable();
                                adpp.Fill(ddt);
                                if (ddt.Rows.Count > 0)
                                {
                                    // var dd = ddt.Rows[0][0].ToString();

                                    instaldate = ddt.Rows[0][0].ToString() == "" ? moorwinch.InstalledDate : Convert.ToDateTime(ddt.Rows[0][0]);

                                    if (moorwinch.InstalledDate > instaldate)
                                    {
                                        System.Windows.MessageBox.Show("Assign to Winch & End to End dates are already inserted for a future date, please remove these entries first to edit the installation date", "Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Information);
                                        MainViewModelWorkHours.CommonValue = true;
                                        return;
                                    }
                                    else
                                    {
                                        moorwinch.InstalledDate = moorwinch.InstalledDate;
                                        // moorwinch.InstalledDate = instaldate;
                                    }

                                }

                                SqlDataAdapter adpp1 = new SqlDataAdapter("select MAX(AssignedDate) as AssignedDate  from AssignRopeToWinch where RopeId=" + moorwinch.Id + " and IsActive=1 and RopeTail=0", sc.con);
                                DataTable ddt1 = new DataTable();
                                adpp1.Fill(ddt1);
                                if (ddt1.Rows.Count > 0)
                                {
                                    instaldate1 = ddt1.Rows[0][0] == DBNull.Value ? moorwinch.InstalledDate : Convert.ToDateTime(ddt1.Rows[0][0]);

                                    if (moorwinch.InstalledDate > instaldate1)
                                    {
                                        System.Windows.MessageBox.Show("Assign to Winch & End to End dates are already inserted for a future date, please remove these entries first to edit the installation date", "Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Information);
                                        MainViewModelWorkHours.CommonValue = true;
                                        return;
                                    }
                                    else
                                    {
                                        moorwinch.InstalledDate = moorwinch.InstalledDate;
                                        //moorwinch.InstalledDate = instaldate1;

                                    }

                                }
                            }
                        }
                        catch { }


                        SqlDataAdapter adpp2 = new SqlDataAdapter("select MAX(SplicingDoneDate) as SplicingDoneDate from RopeSplicingRecord where RopeId=" + moorwinch.Id + " and IsActive=1 and RopeTail=0", sc.con);
                        DataTable ddt2 = new DataTable();
                        adpp2.Fill(ddt2);
                        if (ddt2.Rows.Count > 0)
                        {
                            // instaldate = Convert.ToDateTime(ddt2.Rows[0][0]);
                            recedate1 = ddt2.Rows[0][0].ToString() == "" ? moorwinch.ReceivedDate : Convert.ToDateTime(ddt2.Rows[0][0]);


                            if (moorwinch.ReceivedDate > recedate1)
                            {
                                System.Windows.MessageBox.Show("SplicingDone Date are already inserted for a future date, please remove these entries first to edit the received date", "Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Information);
                                MainViewModelWorkHours.CommonValue = true;
                                return;
                            }
                            else
                            {
                                moorwinch.ReceivedDate = moorwinch.ReceivedDate;
                            }
                        }

                        SqlDataAdapter adpp3 = new SqlDataAdapter("select MAX(DamageDate) as DamageDate from ropedamagerecord where RopeId=" + moorwinch.Id + " and IsActive=1 and RopeTail=0", sc.con);
                        DataTable ddt3 = new DataTable();
                        adpp3.Fill(ddt3);
                        if (ddt3.Rows.Count > 0)
                        {
                            //instaldate = Convert.ToDateTime(ddt3.Rows[0][0]);

                            recedate2 = ddt3.Rows[0][0].ToString() == "" ? moorwinch.ReceivedDate : Convert.ToDateTime(ddt3.Rows[0][0]);

                            if (moorwinch.ReceivedDate > recedate2)
                            {
                                System.Windows.MessageBox.Show("Damage Date are already inserted for a future date, please remove these entries first to edit the received date", "Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Information);
                                MainViewModelWorkHours.CommonValue = true;
                                return;
                            }
                            else
                            {
                                moorwinch.ReceivedDate = moorwinch.ReceivedDate;
                            }
                        }

                        SqlDataAdapter adpp4 = new SqlDataAdapter("select MAX(CroppedDate) as CroppedDate from RopeCropping where RopeId=" + moorwinch.Id + " and IsActive=1 and RopeTail=0", sc.con);
                        DataTable ddt4 = new DataTable();
                        adpp4.Fill(ddt4);
                        if (ddt4.Rows.Count > 0)
                        {
                            //instaldate = Convert.ToDateTime(ddt4.Rows[0][0]);

                            recedate3 = ddt4.Rows[0][0].ToString() == "" ? moorwinch.ReceivedDate : Convert.ToDateTime(ddt4.Rows[0][0]);

                            if (moorwinch.ReceivedDate > recedate3)
                            {
                                System.Windows.MessageBox.Show("Cropped Date are already inserted for a future date, please remove these entries first to edit the received date", "Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Information);
                                MainViewModelWorkHours.CommonValue = true;
                                return;
                            }
                            else
                            {
                                moorwinch.ReceivedDate = moorwinch.ReceivedDate;
                            }
                        }
                    }
                    catch (Exception ex) { }

                    var UpdatedRopedetails = new MooringWinchRopeClass()
                    {

                        Id = moorwinch.Id,
                        RopeTypeId = SRopeType.Id,
                        ManufacturerId = SManuFName.Id,
                        Length = moorwinch.Length,
                        DiaMeter = moorwinch.DiaMeter,
                        WLL = moorwinch.WLL,
                        LDBF = moorwinch.LDBF,
                        MBL = 0,
                        ReasonOutofService = moorwinch.ReasonOutofService,
                        //OutofServiceDate = moorwinch.OutofServiceDate,
                        DamageObserved = moorwinch.DamageObserved,
                        InstalledDate = moorwinch.InstalledDate,
                        MooringOperation = moorwinch.MooringOperation,
                        ModifiedDate = DateTime.Now,
                        IsActive = true,
                        RopeConstruction = moorwinch.RopeConstruction,
                        RopeTagging = moorwinch.RopeTagging,
                        ReceivedDate = moorwinch.ReceivedDate,
                        CertificateNumber = moorwinch.CertificateNumber,
                        UniqueID = moorwinch.UniqueID,
                        //ManufacturerName = moorwinch.ManufacturerName,
                        CreatedBy = "Admin",
                        ModifiedBy = "Admin",
                        RopeTail = 0,
                        CurrentRunningHours = crntRhrs,
                        StartCounterHours = moorwinch.CurrentRunningHours,
                        Remarks = moorwinch.Remarks,
                        InspectionDueDate = moorwinch.InspectionDueDate,
                        CreatedDate = DateTime.Now,
                       //RopeAttachment=moorwinch.RopeAttachment
                    };

                    sc.Entry(UpdatedRopedetails).State = EntityState.Modified;
                    sc.SaveChanges();

                    try
                    {
                        using (SqlDataAdapter adp = new SqlDataAdapter("select * from MooringRopeAttachment where ropeid=" + moorwinch.Id + "", sc.con))
                        {
                            DataTable ddt = new DataTable();
                            adp.Fill(ddt);

                            if (ddt.Rows.Count > 0)
                            {
                                //using (SqlDataAdapter adp1 = new SqlDataAdapter("update MooringRopeAttachment set AttachmentPath='" + moorwinch.AttachmentPath + "' where ropeid=" + moorwinch.Id + "", sc.con))
                                //{
                                //    DataTable ddt1 = new DataTable();
                                //    adp1.Fill(ddt1);

                                //}
                                using (SqlDataAdapter adp1 = new SqlDataAdapter("UpdatetMooringRopeAttachment", sc.con))
                                {
                                    adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    adp1.SelectCommand.Parameters.AddWithValue("@RopeId", moorwinch.Id);
                                    adp1.SelectCommand.Parameters.AddWithValue("@AttachmentPath", moorwinch.AttachmentPath);
                                    DataTable ddt1 = new DataTable();
                                    adp1.Fill(ddt1);

                                }
                            }
                            else
                            {
                                var ropeid = moorwinch.Id;
                                using (SqlDataAdapter adp2 = new SqlDataAdapter("InsertRopeAttachment", sc.con))
                                {
                                    adp2.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    adp2.SelectCommand.Parameters.AddWithValue("@AttachmentPath", moorwinch.AttachmentPath);
                                    adp2.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                    adp2.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                                    adp2.SelectCommand.Parameters.AddWithValue("@LineResidual", "Line");
                                    adp2.SelectCommand.Parameters.AddWithValue("@IsActive", true);
                                    adp2.SelectCommand.Parameters.AddWithValue("@DeleteStatus", false);
                                    adp2.SelectCommand.Parameters.AddWithValue("@ResidualID", 0);
                                    DataTable dtt = new DataTable();
                                    adp2.Fill(dtt);
                                }
                            }

                        }


                    }
                    catch { }
                    //catch { }

                    StaticHelper.Editing = false;
                    System.Windows.MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                    Below21RopesAtDeleteTime();
                    //var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.ToList());
                    //MooringWinchRopeViewModel vm = new MooringWinchRopeViewModel(lostdata);

                    //ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchRopeView() { DataContext = vm });
                    resetMooringRope();
                    // CancelMooringWinch();

                    // }

                    //}
                    //else
                    //{

                    //    MooringWinchMessage = "Please Enter the MooringWinch Name";
                    //    RaisePropertyChanged("MooringWinchMessage");
                    //}

                    //}
                    //else
                    //{
                    //    MessageBox.Show("UniqueID already exist !", "Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Information);
                    //    MainViewModelWorkHours.CommonValue = true;
                    //}
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void datecheck(int ropeid)
        {
            try
            {



            }
            catch { }
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
            MainViewModelWorkHours.CommonValue = false;
            var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.Where(x => x.DeleteStatus == false).ToList());
            MooringWinchRopeViewModel cc = new MooringWinchRopeViewModel(lostdata);
            ChildWindowManager.Instance.CloseChildWindow();
            resetMooringRope();

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

            cdc.RopeTypeId = cdc.RopeTypeId != null ? cdc.RopeTypeId : null;
            cdc.RopeConstruction = cdc.RopeConstruction != null ? cdc.RopeConstruction.Trim() : string.Empty;
            cdc.DiaMeter = cdc.DiaMeter != null ? cdc.DiaMeter : null;
            cdc.Length = cdc.Length != null ? cdc.Length : null;
            //cdc.MBL = cdc.MBL != null ? cdc.MBL : null;
            //cdc.LDBF = cdc.LDBF != null ? cdc.LDBF : null;
            //cdc.WLL = cdc.WLL != null ? cdc.WLL : null;
            cdc.CertificateNumber = cdc.CertificateNumber != null ? cdc.CertificateNumber.Trim() : string.Empty;
            cdc.UniqueID = cdc.UniqueID != null ? cdc.UniqueID.Trim() : string.Empty;
            cdc.ReceivedDate = cdc.ReceivedDate != null ? cdc.ReceivedDate : null;
            //cdc.InstalledDate = cdc.InstalledDate != null ? cdc.InstalledDate : null;
            //cdc.ReasonOutofService = cdc.ReasonOutofService != null ? cdc.ReasonOutofService.Trim() : string.Empty;
            cdc.RopeTagging = cdc.RopeTagging != null ? cdc.RopeTagging.Trim() : string.Empty;
            cdc.IsRopeInstalled = cdc.IsRopeInstalled != null ? cdc.IsRopeInstalled : null;
            if (cdc.RopeTypeId == null)
            {
                MainViewModelWorkHours.CommonValue = true;
                CheckErrorMessage.CheckErrorMessages = false;
                m.RopeTypeMessage = "Please Select LineType !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (string.IsNullOrEmpty(cdc.RopeConstruction))
            {
                MainViewModelWorkHours.CommonValue = true;
                CheckErrorMessage.CheckErrorMessages = false;
                m.RopeConstructionMessage = "Please Select Line Construction !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (cdc.DiaMeter == null)
            {
                MainViewModelWorkHours.CommonValue = true;
                CheckErrorMessage.CheckErrorMessages = false;
                m.DiaMeterMessage = "Please Enter DiaMeter !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (cdc.Length == null)
            {
                MainViewModelWorkHours.CommonValue = true;
                CheckErrorMessage.CheckErrorMessages = false;
                m.LengthMessage = "Please Enter Length !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            //if (cdc.MBL == null)
            //{
            //    MainViewModelWorkHours.CommonValue = true;
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.MBLMessage = "Please Enter MBL !";
            //    RaisePropertyChanged("AddMooringWinchRopeMessages");
            //}
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
                MainViewModelWorkHours.CommonValue = true;
                CheckErrorMessage.CheckErrorMessages = false;
                m.ReceivedDateMessage = "Please Choose Received Date !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }

            if (string.IsNullOrEmpty(cdc.RopeTagging))
            {
                MainViewModelWorkHours.CommonValue = true;
                CheckErrorMessage.CheckErrorMessages = false;
                m.RopeTaggingMessage = "Please Enter Line Tagging !";
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
                MainViewModelWorkHours.CommonValue = true;
                CheckErrorMessage.CheckErrorMessages = false;
                m.CertificateNoMessage = "Please Enter Certificate Number !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (string.IsNullOrEmpty(cdc.UniqueID))
            {
                MainViewModelWorkHours.CommonValue = true;
                CheckErrorMessage.CheckErrorMessages = false;
                m.UniqueIDMessage = "Please Enter Unique Identification Number !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (cdc.ManufacturerId == 0)
            {
                MainViewModelWorkHours.CommonValue = true;
                CheckErrorMessage.CheckErrorMessages = false;
                m.ManufactureNameMessage = "Please Enter Manufacture Name !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (string.IsNullOrEmpty(cdc.IsRopeInstalled))
            {
                MainViewModelWorkHours.CommonValue = true;
                CheckErrorMessage.CheckErrorMessages = false;
                m.IsRopeInstalled = "Please Choose at least 1 !";
                RaisePropertyChanged("AddMooringWinchRopeMessages");
            }
            if (cdc.IsRopeInstalled == "Yes")
            {
                if (cdc.InstalledDate == null)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    CheckErrorMessage.CheckErrorMessages = false;
                    m.InstalledDateMessage = "Please Choose Installed Date !";
                    RaisePropertyChanged("AddMooringWinchRopeMessages");
                }
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
