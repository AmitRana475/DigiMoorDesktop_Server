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
//using System.Windows.Forms;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
  public  class AddResidualLabTestViewModel : ViewModelBase
    {

        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddResidualLabTestViewModel(ResidualLabTestClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;
            _BrowseCommand = new RelayCommand(BrowseMethod);
            saveCommand = new RelayCommand<MooringWinchRopeClass>(UpdateMooringWinch);
            cancelCommand = new RelayCommand(CancelMooringWinch);

            // MooringWinchRopeClass data = sc.MooringWinchRope.Where(x => x.Id == edeps.Id).FirstOrDefault();

            AddResidualLabTest = new ResidualLabTestClass()
            {
                //Id = edeps.Id,
                //Length = edeps.Length,
                //MBL = edeps.MBL,
                //LDBF = edeps.LDBF,
                //WLL = edeps.WLL,
                //DiaMeter = edeps.DiaMeter,
                ////ManufacturerName = edeps.ManufacturerName,
                //CertificateNumber = edeps.CertificateNumber,
                //RopeTagging = edeps.RopeTagging,
                //ModifiedDate = DateTime.Now,
                //ModifiedBy = "Admin",
                //CurrentRunningHours = edeps.CurrentRunningHours,
                //Remarks = edeps.Remarks
            };

            //ComboValue7 = edeps.RopeTagging;
            //// SRopeType = edeps.RopeTypeId;
            //SRopeConst = edeps.RopeConstruction;
            ////SRopeDiameter = edeps.DiaMeter;
            //SRopeReasonoutofs = edeps.ReasonOutofService;
            //SRopeMooringOpertaion = edeps.MooringOperation;
            //SRopeDamageObserved = edeps.DamageObserved;
            //ReceivedDate = edeps.ReceivedDate;
            //InstalledDate = edeps.InstalledDate;
            //OutofServiceDate = edeps.OutofServiceDate;

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
        public AddResidualLabTestViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            _BrowseCommand = new RelayCommand(BrowseMethod);
            saveCommand = new RelayCommand<ResidualLabTestClass>(SaveResidualLabTest);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            resetCommand = new RelayCommand(resetform);
            GetRopeType();
            manufname = GetManuFName();
            outofsreason = GetOutofSReason();
            damageobserved = GetDamageObserved();

           // resetMooringRope();

            //AddMooringWinchRope = new MooringWinchRopeClass();
            //OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinchRope"));


        }

        public void resetform()
        {
            try
            {

                erinfo = 0;
                AddResidualLabTest = new ResidualLabTestClass();
                RaisePropertyChanged("AddResidualLabTest");

                AddResidualmessagess = new AddResidualmessagesClass();
                RaisePropertyChanged("AddResidualmessagess");


                LabTestDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("LabTestDate");
                

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
            SqlDataAdapter adp = new SqlDataAdapter("select Id, UniqueID +' - '+ CertificateNumber as CertificateNumber   from MooringRopeDetail where DeleteStatus=0 and RopeTail=0  and OutofServiceDate is null order by UniqueID asc", sc.con);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                rop = new RopeTypeCombo1();
                rop.Id = (int)row["Id"];
                rop.CertificateNumber = (string)row["CertificateNumber"]; 
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
        public static ResidualLabTestClass _AddResidualLabTest = new ResidualLabTestClass();
        public ResidualLabTestClass AddResidualLabTest
        {
            get
            {               

                return _AddResidualLabTest;
            }
            set
            {
                _AddResidualLabTest = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddResidualLabTest"));
            }
        }

        private static AddResidualmessagesClass _AddResidualmessagess = new AddResidualmessagesClass();
        public AddResidualmessagesClass AddResidualmessagess
        {
            get
            {
                return _AddResidualmessagess;
            }
            set
            {
                _AddResidualmessagess = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddResidualmessagess"));
            }
        }
        public class AddResidualmessagesClass
        {
            public string TestResultsMessage { get; set; }
            //public string RopeConstructionMessage { get; set; }
            //public string DiaMeterMessage { get; set; }
            //public string LengthMessage { get; set; }
            //public string MBLMessage { get; set; }
            //public string LDBFMessage { get; set; }
            //public string WLLMessage { get; set; }
            //public string ManufactureNameMessage { get; set; }
            //public string CertificateNoMessage { get; set; }
            //public string ReceivedDateMessage { get; set; }
            //public string InstalledDateMessage { get; set; }
            //public string RopeTaggingMessage { get; set; }
            //public string ReasonoutofServiceMessage { get; set; }

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
        private ICommand _BrowseCommand;
        public ICommand BrowseCommand
        {
            get
            {
                return _BrowseCommand;
            }
        }



        #region Bind Properties


        public class RopeTypeCombo1
        {
            public int Id { get; set; }
            public string CertificateNumber { get; set; }
        }

        public static RopeTypeCombo1 sropetype;// = new Ropetypecombo();
        public RopeTypeCombo1 SRopeType
        {
            get
            {

                if (sropetype != null)
                {

                    AddResidualLabTest.RopeTypeId = sropetype.Id;
                    OnPropertyChanged(new PropertyChangedEventArgs("AddResidualLabTest"));
                }
                return sropetype;

            }
            set
            {

                sropetype = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SRopeType"));


            }
        }
        private void BrowseMethod()
        {
          System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".docx";
            dlg.Filter = "Image files (*.png;*.jpeg;*.jpg;*.tiff)|*.png;*.jpeg;*.jpg;*.tiff|PDF files (*.pdf)|*.pdf;|Excel Files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm;|Word Files (*.doc;*.docx)|*.doc;*.docx";
            //"Excel Files|*.xls;*.xlsx;*.xlsm;*.pdf;*.jpg;*.png;*.jpeg";

            // Display OpenFileDialog by calling ShowDialog method
            //Nullable<bool> result1 = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var size = new FileInfo(dlg.FileName).Length / 1024;

                if (size > 1024)
                {
                    System.Windows.Forms.MessageBox.Show("Attachment Size can not exceed 1MB !.", "Residual Lab Test", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }
                AddResidualLabTest.AttachmentPath = dlg.FileName;
                RaisePropertyChanged("AddResidualLabTest");

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
                    AddResidualLabTest.ManufacturerId = sropeManuFName.Id;
                //if (sropeManuFName == "MASTER")
                //    AddMooringWinchRope.ManufacturerName = "All";

                return sropeManuFName;
            }

            set
            {
                sropeManuFName = value;
                if (sropeManuFName != null)
                    AddResidualLabTest.ManufacturerId = sropeManuFName.Id;
                OnPropertyChanged(new PropertyChangedEventArgs("SManuFName"));
            }
        }

      

     
        private static Nullable<DateTime> _LabTestDate = null;
        public Nullable<DateTime> LabTestDate
        {
            get
            {
                if (_LabTestDate == null)
                {
                    _LabTestDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                AddResidualLabTest.LabTestDate = (DateTime)_LabTestDate;
                return _LabTestDate;
            }
            set
            {
                _LabTestDate = value;
                RaisePropertyChanged("LabTestDate");
            }
        }

     
        #endregion
        private void SaveResidualLabTest(ResidualLabTestClass ResLbTst)
        {
            try
            {
                refreshmessage1(ResLbTst);


                if (!string.IsNullOrEmpty(Lblmessage))
                    CheckErrorMessage.CheckErrorMessages = false;

                if (CheckErrorMessage.CheckErrorMessages)
                {

                    if(ResLbTst.TestResults== null)
                    {
                        MessageBox.Show("Please Enter Residual Strength", "ResidualLab Test", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    ResLbTst.LabTestDate = Convert.ToDateTime(ResLbTst.LabTestDate);
                    ResLbTst.TestResults = Convert.ToDecimal(ResLbTst.TestResults);
                    if (ResLbTst.Remarks == null)
                    {
                        ResLbTst.Remarks = null;
                    }
                    else
                    {
                        ResLbTst.Remarks = ResLbTst.Remarks;
                    }
                    ResLbTst.CreatedDate = DateTime.Now;                  
                    ResLbTst.IsActive = true;
                    ResLbTst.RopeTail = 0;  
                    sc.ResidualLabTestTbl.Add(ResLbTst);

                    if (!string.IsNullOrEmpty(ResLbTst.AttachmentPath))
                    {
                        ResLbTst.AttachmentPath = ResLbTst.AttachmentPath;
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


                        if (!string.IsNullOrEmpty(ResLbTst.AttachmentPath))
                        {
                            if (File.Exists(ResLbTst.AttachmentPath))
                            {
                                string fileExtention = Path.GetExtension(ResLbTst.AttachmentPath);//Path.GetFileName(AddDepartment.AttachmentPath);

                                string[] ExtList = { ".xls", ".xlsx", ".wmv", ".xlsm", ".pdf", ".mp4", ".jpg", ".png", ".jpeg", ".tiff", ".doc", ".docx" };
                                if (ExtList.Contains(fileExtention.ToLower()))
                                {

                                    //var count = sc.TrainingAttachment.Count();
                                    //if (count <= 9)
                                    //{


                                    string fileName = Path.GetFileName(ResLbTst.AttachmentPath);
                                    var withoutextnsn = Path.GetFileNameWithoutExtension(fileName) + num;

                                    fileName = withoutextnsn + fileExtention;

                                    mypath = mypath + "\\" + fileName;
                                    //File.Delete(sfd.FileName);

                                    var ss = ResLbTst.AttachmentPath;

                                    //AddDepartment.AttachmentPath = mypath;
                                    ResLbTst.AttachmentPath = fileName;


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

                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Invalid Attachment file.", "Mooring RopeTail", MessageBoxButton.OK, MessageBoxImage.Warning);

                                }
                            }                            
                        }                     
                    }


                    //var lostdata = new ObservableCollection<ResidualLabTestClass>(sc.ResidualLabTestTbl.ToList());
                    //MooringWinchRopeViewModel vm = new MooringWinchRopeViewModel(lostdata);

                    ChildWindowManager.Instance.CloseChildWindow();

                    var minvalue = sc.ResidualLabTestTbl.Where(x => x.RopeId == ResLbTst.RopeId).Select(x=> x.TestResults).Min();

                    if(minvalue==null)
                    {
                        minvalue = ResLbTst.TestResults;
                    }

                    if (ResLbTst.TestResults <= 75)
                    {
                        if(minvalue <= 75)
                        {
                            minvalue = minvalue;
                        }
                        else
                        {
                            minvalue = ResLbTst.TestResults;
                        }

                        var mrRope = sc.MooringWinchRope.Where(x => x.DeleteStatus == false & x.Id == ResLbTst.RopeId).FirstOrDefault();
                        var ropename = mrRope.CertificateNumber;
                        var uniqueid = mrRope.UniqueID;
                        var NotiMsg = "Line " + ropename + " - " + uniqueid + " residual strength Current - " + minvalue + "% / Min. Required 75%";
                                          //var NotiMsg = "Line residual strength below minimum allowable (" + minvalue + " - / Required - 75%)";
                        int NotiAlertType = (int)NotificationAlertType.RopeResidual_StrengthCheck;
                        //InspectNotification(Ropeid, Max_running_hours_Approaching, NotiAlertType);



                        var result = sc.Notifications.Where(x => x.RopeId == ResLbTst.RopeId & x.NotificationAlertType == NotiAlertType).FirstOrDefault();

                        if (result != null)
                        {
                            sc.Notifications.Remove(result);
                            sc.SaveChanges();

                        }

                        //if (result == null)
                        //{
                            NotificationsClass noti = new NotificationsClass();
                            noti.Acknowledge = false;
                            noti.AckRecord = "This notification cannot be acknowledged, kindly discard it";
                            noti.Notification = NotiMsg;
                            noti.RopeId = ResLbTst.RopeId;
                            noti.IsActive = true;
                            noti.NotificationDueDate = DateTime.Now.Date;
                            noti.CreatedDate = DateTime.Now;
                            noti.CreatedBy = "Admin";
                            noti.NotificationAlertType = (int)NotificationAlertType.RopeResidual_StrengthCheck;
                            noti.NotificationType = 1;
                            sc.Notifications.Add(noti);
                            sc.SaveChanges();

                        StaticHelper.AlarmFunction(1, true);
                    }

                    //}


                    sc.SaveChanges();

                    var idpk = ((from mrop in sc.ResidualLabTestTbl select (int?)mrop.Id).Max() ?? 0);


                    try
                    {
                        using (SqlDataAdapter adp = new SqlDataAdapter("InsertRopeAttachment", sc.con))
                        {
                            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp.SelectCommand.Parameters.AddWithValue("@AttachmentPath", ResLbTst.AttachmentPath);
                            adp.SelectCommand.Parameters.AddWithValue("@RopeId", ResLbTst.RopeId);
                            adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                            adp.SelectCommand.Parameters.AddWithValue("@LineResidual", "ResidualLine");
                            adp.SelectCommand.Parameters.AddWithValue("@IsActive", true);
                            adp.SelectCommand.Parameters.AddWithValue("@DeleteStatus", false);
                            adp.SelectCommand.Parameters.AddWithValue("@ResidualID", idpk);
                            DataTable dtt = new DataTable();
                            adp.Fill(dtt);
                        }
                    }
                    catch (Exception ex) { }

                    MessageBox.Show("Record saved successfully ", "Add Residual Lab Test", MessageBoxButton.OK, MessageBoxImage.Information);


                    resetform();

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
                //refreshmessage1(moorwinch);


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

                    try
                    {
                        SqlDataAdapter adp = new SqlDataAdapter("select * from tblRopeInspectionSetting where MooringRopeType=" + moorwinch.RopeTypeId + " and ManufacturerType=" + moorwinch.ManufacturerId + "", sc.con);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            decimal rating1 = Convert.ToDecimal(dt.Rows[0]["Rating1"]);
                            int rat = Convert.ToInt32(rating1);

                            DateTime inspectduedate = Convert.ToDateTime(moorwinch.InstalledDate).AddMonths(rat);

                            moorwinch.InspectionDueDate = inspectduedate;
                        }
                    }
                    catch { }

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
                        //ManufacturerName = moorwinch.ManufacturerName,
                        CreatedBy = moorwinch.CreatedBy,
                        RopeTail = 0,
                        CurrentRunningHours = moorwinch.CurrentRunningHours,
                        Remarks = moorwinch.Remarks,
                        InspectionDueDate = moorwinch.InspectionDueDate
                    };






                    sc.Entry(UpdatedRopedetails).State = EntityState.Modified;
                    sc.SaveChanges();



                    StaticHelper.Editing = false;
                    MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                   
                    //var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.ToList());
                    //MooringWinchRopeViewModel vm = new MooringWinchRopeViewModel(lostdata);

                    //ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchRopeView() { DataContext = vm });
                    resetform();
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
            try
            {
                var lostdata = new ObservableCollection<ResidualLabTestClass>(sc.ResidualLabTestTbl.ToList());
                ResidualLabTestListViewModel cc = new ResidualLabTestListViewModel(lostdata);
                ChildWindowManager.Instance.CloseChildWindow();
                resetform();
            }
            catch { }

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


        private void refreshmessage1(ResidualLabTestClass cdc1)
        {
            ResidualLabTestClass cdc = cdc1;
            CheckErrorMessage.CheckErrorMessages = true;
            MainViewModelWorkHours.CommonValue = false;
            AddResidualmessagess = new AddResidualmessagesClass();
            RaisePropertyChanged("AddResidualmessagess");

            AddResidualmessagesClass m = (AddResidualmessagess as AddResidualmessagesClass); //DownCasting.....
           
            cdc.TestResults = cdc.TestResults != null ? cdc.TestResults : null;          
            if (cdc.TestResults == null)
            {
                MainViewModelWorkHours.CommonValue = true;
                CheckErrorMessage.CheckErrorMessages = false;
                m.TestResultsMessage = "Please enter test results !";
                RaisePropertyChanged("AddResidualmessagess");
            }
        }
        public static class CheckErrorMessage
        {
            public static bool CheckErrorMessages { get; set; }
            public static bool CheckErrorMessages1 { get; set; }
            public static bool CheckErrorMessages2 { get; set; }
            public static bool chkyoungs { get; set; }

        }

      

    }
}

