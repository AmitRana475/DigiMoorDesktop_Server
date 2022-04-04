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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.WorkHoursViewModel
{
      public class AssignRopeToWinchViewModel : ViewModelBase
       {
        private readonly ShipmentContaxt sc;
        public ICommand HelpCommand { get; private set; }
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AssignRopeToWinchViewModel(AssignModuleToWinchClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<AssignModuleToWinchClass>(UpdateMooringWinch);
            cancelCommand = new RelayCommand(CancelMooringWinch);

            SelectionChangedCommand = new RelayCommand(SelectionChanged);

            //ropetype = GetRopeType();
            //AssignModuleToWinchClass data = sc.AssignRopetoWinch.Where(x => x.Id == edeps.Id).FirstOrDefault();

            AssignRopeToWinch = new AssignModuleToWinchClass()
            {
                Id = edeps.Id,
                WinchId=edeps.WinchId,
                AssignedLocation = edeps.AssignedLocation,
                ModifiedDate = DateTime.Now,
                ModifiedBy = "Admin",
                CreatedDate=edeps.CreatedDate,
                CreatedBy=edeps.CreatedBy
            };


            //StaticHelper.HelpFor = @"LMPR\rope\4.2.3  ASSIGN ROPE TO WINCH.htm";
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            //sropetype = new Ropetypecombo()
            //{
            //    Id = data.RopeId,
            //    CertificateNo = edeps.CertificateNumber
            //};

            Ropetypecombo abc = new Ropetypecombo()
            {
                   Id = edeps.RopeId,
                   CertificateNo = edeps.CertificateNumber
            };
            SRopeType = abc;
            Winchcombo abcd = new Winchcombo()
            {
                Id = edeps.WinchId,
                AssignedNumber = edeps.AssignedNumber
            };
            SRopeAss = abcd;

            //SRopeType.Id = data.RopeId;
            //SRopeType.CertificateNo = edeps.CertificateNumber;
            //OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
            // Ropetypecombo ss = new Ropetypecombo();
            //ss.Id =  Convert.ToInt32(data.RopeId);
            //SRopeType.Id = 2;
            //SRopeConst = edeps.RopeConstruction;
            //SRopeDiameter = edeps.DiaMeter;
            //SRopeReasonoutofs = edeps.ReasonOutofService;
            //SRopeMooringOpertaion = edeps.MooringOperation;
            //SRopeDamageObserved = edeps.DamageObserved;
            //ReceivedDate = edeps.ReceivedDate;
            //InstalledDate = edeps.InstalledDate;
            //OutofServiceDate = edeps.OutofServiceDate;


            RaisePropertyChanged("AddMooringWinchRope");
            OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinchRope"));
            //EditMooringWinch(edeps);
        }
        public AssignRopeToWinchViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<AssignModuleToWinchClass>(SaveMooringWinchRope);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            RadioBTNCommand = new RelayCommand<object>(RadioBTNmethod);
            resetCommand = new RelayCommand(ResetAssignRopeToWinch);


            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            //ropetype = GetRopeType();
            GetRopeType();
           GetAssRope();

            ResetAssignRopeToWinch();
        }

        public ICommand SelectionChangedCommand;
        private void SelectionChanged()
        {
            // write logic here
        }
        private string mooringWinchMessage;
        public string MooringWinchMessage
        {
            get { return mooringWinchMessage; }
            set { mooringWinchMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("MooringWinchMessage")); }
        }

        #region Bind Ropetype
        public static ObservableCollection<Ropetypecombo> ropetype = new ObservableCollection<Ropetypecombo>();
        public ObservableCollection<Ropetypecombo> RopeType
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



        public static Nullable<DateTime> _AssignedDate = null;
        public Nullable<DateTime> AssignedDate
        {
            get
            {
                if (_AssignedDate == null)
                {
                    _AssignedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                _AddAssWinchRope.AssignedDate = (DateTime)_AssignedDate;
                return _AssignedDate;
            }
            set
            {
                _AssignedDate = value;
                RaisePropertyChanged("AssignedDate");
            }
        }

        //public ObservableCollection<Ropetypecombo> GetRopeType()
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

        public void ResetAssignRopeToWinch()
        {
            try
            {

                erinfo = 0;
                AssignRopeToWinch = new AssignModuleToWinchClass();
                RaisePropertyChanged("AssignRopeToWinch");

                //AddMooringWinchRopeMessages = new AddMooringRopeErrorMessages();
                //RaisePropertyChanged("AddMooringWinchRopeMessages");


                AssignedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("AssignedDate");
                //InstalledDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("InstalledDate");

                //ComboValue7 = null; RaisePropertyChanged("ComboValue7");
                SRopeType = null; RaisePropertyChanged("SRopeType");
                SRopeAss = null; RaisePropertyChanged("SRopeAss");
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        public void GetRopeType()
        {
            try
            {
                ropetype.Clear();
                //SqlCommand cmd = new SqlCommand("select Id,CertificateNumber +' - ' + UniqueId as CertificateNumber from MooringRopeDetail where ropetail=0 and OutofServiceDate is null and DeleteStatus=0 and InstalledDate is not null", sc.con);

                SqlCommand cmd = new SqlCommand("select Id,UniqueId +' - ' + CertificateNumber as CertificateNumber from MooringRopeDetail where ropetail=0 and OutofServiceDate is null and DeleteStatus=0 and InstalledDate is not null order by UniqueID asc", sc.con);
                //cmd.CommandType = CommandType.StoredProcedure;


                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    ropetype.Add(new Ropetypecombo()
                    {
                        Id = (int)row["Id"],                       
                        CertificateNo = (string)row["CertificateNumber"],                       
                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("ropetype"));

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }
        }
        #endregion

        #region Bind AssignRope


        public static ObservableCollection<Winchcombo> assignrope = new ObservableCollection<Winchcombo>();
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



        public static Winchcombo _sropeass;
        public Winchcombo SRopeAss
        {
            get
            {
                //if (_sropeass != null)
                //{
                //   // CrossShifting.WinchId = _sropeass.Id;


                //}
                //return _sropeass;

                if (_sropeass != null)
                {
                    var data = sc.MooringWinch.Where(x => x.Id == _sropeass.Id && x.IsActive == true).FirstOrDefault();
                    if (data != null)
                    {
                        //var data1 = sc.MooringWinchRope.Where(x => x.Id == sropetype.Id).FirstOrDefault();
                        AssignRopeToWinch.WinchId = data.Id;
                        //CrossShifting.AssignedLocation = data.Location;


                    }

                    OnPropertyChanged(new PropertyChangedEventArgs("AssignRopeToWinch"));
                }
                return _sropeass;
            }
            set
            {
                _sropeass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SRopeAss"));
            }
        }

        //private ObservableCollection<string> GetAssRope()
        //{
        //    var assignrp = new ObservableCollection<string>();
        //    var data = sc.MooringWinch.OrderBy(s => s.Id).Select(x => x.AssignedNumber).ToList();

        //    foreach (var item in data)
        //    {
        //        assignrp.Add(item);

        //    }

        //    return assignrp;
        //}

        //public ObservableCollection<Winchcombo> GetAssRope()
        //{
        //    ObservableCollection<Winchcombo> AddWinchId = new ObservableCollection<Winchcombo>();          
        //    var data = sc.MooringWinch.Select(x => new { x.Id, x.AssignedNumber }).ToList();

        //    Winchcombo rop;
        //    foreach (var item in data)
        //    {

        //        rop = new Winchcombo();
        //        rop.Id = item.Id;
        //        rop.AssignedNumber = item.AssignedNumber;
        //        AddWinchId.Add(rop);
        //    }

        //    return AddWinchId;
        //}

        public void GetAssRope()
        {
            try
            {
                assignrope.Clear();
                SqlCommand cmd = new SqlCommand("select Id,AssignedNumber from MooringWinchDetail where IsActive=1 order by SortingOrder asc", sc.con);
                //cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    var AssignedNumber1 = (string)row["AssignedNumber"];
                    AssignedNumber1 = AssignedNumber1.Replace(Environment.NewLine, "").Trim();
                    assignrope.Add(new Winchcombo()
                    {
                        Id = (int)row["Id"],
                        AssignedNumber = AssignedNumber1,
                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("assignrope"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }
        }

        #endregion


        public static AssignModuleToWinchClass _AddAssWinchRope = new AssignModuleToWinchClass();
        public AssignModuleToWinchClass AssignRopeToWinch
        {
            get
            {             
                return _AddAssWinchRope;
            }
            set
            {
                _AddAssWinchRope = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AssignRopeToWinch"));
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

        private void refreshmessage1(AssignModuleToWinchClass cdc1)
        {
            AssignModuleToWinchClass cdc = cdc1;
            CheckErrorMessage.CheckErrorMessages = true;

            //AddMooringWinchRopeMessages = new AddMooringRopeErrorMessages();
            //RaisePropertyChanged("AddMooringWinchRopeMessages");

            //AddMooringRopeErrorMessages m = (AddMooringWinchRopeMessages as AddMooringRopeErrorMessages); //DownCasting.....

            //cdc.RopeType = cdc.RopeType != null ? cdc.RopeType.Trim() : string.Empty;
            //cdc.RopeConstruction = cdc.RopeConstruction != null ? cdc.RopeConstruction.Trim() : string.Empty;
            //cdc.DiaMeter = cdc.DiaMeter != null ? cdc.DiaMeter.Trim() : string.Empty;
            //cdc.Length = cdc.Length != null ? cdc.Length : null;
            //cdc.MBL = cdc.MBL != null ? cdc.MBL : null;
            //cdc.LDBF = cdc.LDBF != null ? cdc.LDBF : null;
            //cdc.WLL = cdc.WLL != null ? cdc.WLL : null;
            //cdc.CertificateNumber = cdc.CertificateNumber != null ? cdc.CertificateNumber.Trim() : string.Empty;
            //cdc.ReceivedDate = cdc.ReceivedDate != null ? cdc.ReceivedDate : null;
            //cdc.InstalledDate = cdc.InstalledDate != null ? cdc.InstalledDate : null;
            //cdc.ReasonOutofService = cdc.ReasonOutofService != null ? cdc.ReasonOutofService.Trim() : string.Empty;
            //cdc.RopeTagging = cdc.RopeTagging != null ? cdc.RopeTagging.Trim() : string.Empty;

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
            //if (cdc.Length == null)
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.LengthMessage = "Please Enter Length !";
            //    RaisePropertyChanged("AddMooringWinchRopeMessages");
            //}
            //if (cdc.MBL == null)
            //{
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
            //if (cdc.ReceivedDate == null)
            //{
            //    CheckErrorMessage.CheckErrorMessages = false;
            //    m.ReceivedDateMessage = "Please Choose Received Date !";
            //    RaisePropertyChanged("AddMooringWinchRopeMessages");
            //}
          
        }
        public static class CheckErrorMessage
        {
            public static bool CheckErrorMessages { get; set; }
            public static bool CheckErrorMessages1 { get; set; }
            public static bool CheckErrorMessages2 { get; set; }
            public static bool chkyoungs { get; set; }

        }

        private static AssignRopetoWinchErrorMessages _AssignMooringWinchRopeMessages = new AssignRopetoWinchErrorMessages();
        public AssignRopetoWinchErrorMessages AssigndMooringWinchRopeMessages
        {
            get
            {
                return _AssignMooringWinchRopeMessages;
            }
            set
            {
                _AssignMooringWinchRopeMessages = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AssigndMooringWinchRopeMessages"));
            }
        }
        public class AssignRopetoWinchErrorMessages
        {
            public string RopeTypeMessage { get; set; }
            public string RopeConstructionMessage { get; set; }
            public string RopeTaggingMessage { get; set; }
            public string ReasonoutofServiceMessage { get; set; }

        }

        #region Bind Properties

        public static Ropetypecombo sropetype = new Ropetypecombo();
        public Ropetypecombo SRopeType
        {
            //get
            //{    if(sropetype!= null)
            //    {
            //        AssignRopeToWinch.RopeId = sropetype.Id;
            //    }
            //    return sropetype;

            //}
            //set
            //{            
            //    sropetype = value;
            //    OnPropertyChanged(new PropertyChangedEventArgs("SRopeType"));
            //}

            get
            {
               
                if (sropetype != null)
                {
                   
                        AssignRopeToWinch.RopeId = sropetype.Id;
                       
                   

                    OnPropertyChanged(new PropertyChangedEventArgs("AssignRopeToWinch"));
                }
                return sropetype;
            }
            set
            {
                sropetype = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SRopeType"));
            }
        }

        //private static Winchcombo _sropeass = new Winchcombo();
        //public Winchcombo SRopeAss
        //{
        //    get
        //    {   if(_sropeass != null)
        //        {
        //            AssignRopeToWinch.WinchId = _sropeass.Id;
        //        }
        //        return _sropeass;
        //    }
        //    set
        //    {
        //        _sropeass = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("SRopeAss"));
        //    }

            //get
            //{
            //    if (_sropeass != 0)
            //        AssignRopeToWinch.WinchId = _sropeass;
            //    //if (_sropeass == "MASTER")
            //    //    AddMooringWinchRope.RopeConstruction = "All";

            //    return _sropeass;
            //}

            //set
            //{
            //    _sropeass = value;
            //    if (_sropeass != 0)
            //        AssignRopeToWinch.WinchId = _sropeass;
            //    OnPropertyChanged(new PropertyChangedEventArgs("WinchId"));
            //}
        //}
      
        #endregion
        private void SaveMooringWinchRope(AssignModuleToWinchClass asswinchrope)
        {
            try
            {
                if(asswinchrope.Outboard==true)
                {
                    asswinchrope.Outboard = true;
                }
                if (asswinchrope.Outboard == false)
                {
                    asswinchrope.Outboard = false;
                }
                asswinchrope.RopeId = SRopeType.Id;
                asswinchrope.WinchId = SRopeAss.Id;
                //asswinchrope.AssignedDate = asswinchrope.AssignedDate;
                asswinchrope.CreatedDate = DateTime.Now;
                asswinchrope.CreatedBy = "Admin";
                asswinchrope.IsActive = true;
                asswinchrope.RopeTail = 0;

                var duplcheck = sc.AssignRopetoWinch.Where(x => x.RopeId == SRopeType.Id && x.WinchId == SRopeAss.Id && x.IsActive == true && x.RopeTail == 0).FirstOrDefault();
                if (duplcheck == null)
                {
                    try
                    {
                        //var winchidcheck1 = sc.AssignRopetoWinch.Where(x => x.RopeId == SRopeType.Id && x.IsActive == false).Select(x => x.WinchId).SingleOrDefault();
                        var winchidcheck = sc.AssignRopetoWinch.Where(x => x.RopeId == SRopeType.Id && x.IsActive == true).Select(x => x.WinchId).SingleOrDefault();

                        if (winchidcheck == 0)
                        {
                            //lead_check(SRopeType.Id, asswinchrope.AssignedDate, SRopeAss.Id);
                            lead_check(SRopeType.Id, asswinchrope.AssignedDate, 0);
                        }
                        else
                        {

                            lead_check(SRopeType.Id, asswinchrope.AssignedDate, winchidcheck);
                        }
                    }
                    catch { }


                    var duplcheck1 = sc.AssignRopetoWinch.SingleOrDefault(x => x.RopeId == SRopeType.Id && x.IsActive == true && x.RopeTail == 0);
                    var duplcheck2 = sc.AssignRopetoWinch.SingleOrDefault(x => x.WinchId == SRopeAss.Id && x.IsActive == true && x.RopeTail == 0);

                    if (duplcheck1 != null)
                    {
                        duplcheck1.IsActive = false;                       
                        sc.SaveChanges();
                    }
                    if (duplcheck2 != null)
                    {
                        duplcheck2.IsActive = false;
                        sc.SaveChanges();
                    }



                    //if (asswinchrope.Lead == null || asswinchrope.Lead == "--Select--")
                    //{
                    //    MainViewModelWorkHours.CommonValue = true;
                    //    MessageBox.Show("Please Choose Lead ", "Assign Line To Winch", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //    return;
                    //}
                    
                    sc.AssignRopetoWinch.Add(asswinchrope);
                    sc.SaveChanges();
                    StaticHelper.Editing = false;
                    MessageBox.Show("Record saved successfully ", "Assign Line To Winch", MessageBoxButton.OK, MessageBoxImage.Information);
                    //AssignRopeToWinch = new AssignModuleToWinchClass();
                    //RaisePropertyChanged("AssignRopeToWinch");

                    //lead_check(SRopeType.Id, asswinchrope.AssignedDate, SRopeAss.Id);

                    CancelMooringWinch();

                }
                else
                {
                    MessageBox.Show("This Line is already assigned ! ", "Assign Line To Winch", MessageBoxButton.OK, MessageBoxImage.Information);
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


        private void lead_check(int ropeid, DateTime? assigneddate, int winchid)
        {
            try
            {
                if (winchid == 0)
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("select * from MOUsedWinchTbl where ropeid=" + ropeid + "", sc.con))
                    {
                        DataTable dt = new DataTable();
                        adp.Fill(dt);


                        if (dt.Rows.Count > 0)
                        {

                            //var leedd = sc.MooringWinch.Where(x => x.Id == winchid).Select(x => x.Lead).SingleOrDefault();

                            SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " and WinchId=0  group by ropeid,runninghours, Lead", sc.con);

                            //SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " and lead ='" + leedd + "' group by ropeid, Lead", sc.con);
                            DataTable dd1 = new DataTable();
                            pp1.Fill(dd1);

                            for (int i = 0; i < dd1.Rows.Count; i++)
                            //if (dd1.Rows.Count > 0)
                            {
                                int rpid = Convert.ToInt32(dd1.Rows[i]["RopeId"]);
                                string lead = dd1.Rows[i]["Lead"].ToString();
                                decimal rnghrs = Convert.ToDecimal(dd1.Rows[i]["RunningHours"]);

                                decimal rnghrs1 = 0;

                                SqlDataAdapter pp2 = new SqlDataAdapter("select * from WinchRotation where RopeId=" + rpid + " and Lead='" + lead + "'", sc.con);
                                DataTable dd2 = new DataTable();
                                pp2.Fill(dd2);
                                if (dd2.Rows.Count > 0)
                                {
                                    rnghrs1 = Convert.ToDecimal(dd2.Rows[0]["RunningHours"]);

                                    if (rnghrs == rnghrs1)
                                    {
                                        rnghrs = 0;
                                    }
                                    else
                                    {
                                        if (rnghrs1 > rnghrs)
                                        {
                                            rnghrs = rnghrs1 - rnghrs;
                                        }
                                        if (rnghrs1 < rnghrs)
                                        {
                                            rnghrs = rnghrs - rnghrs1;
                                        }

                                    }
                                }


                                if (rnghrs != 0)
                                {

                                    SqlDataAdapter pp5 = new SqlDataAdapter("select * from winchrotation where ropeid="+ropeid+" and Lead='"+lead+"'", sc.con);
                                    DataTable dd5 = new DataTable();
                                    pp5.Fill(dd5);
                                    if (dd5.Rows.Count == 0)
                                    {
                                        //SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                                        SqlDataAdapter pp = new SqlDataAdapter("InsertWinchRotation", sc.con);
                                        pp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                        pp.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                        pp.SelectCommand.Parameters.AddWithValue("@AssignedDate", assigneddate);
                                        pp.SelectCommand.Parameters.AddWithValue("@WinchId", winchid);
                                        pp.SelectCommand.Parameters.AddWithValue("@Lead", lead);
                                        pp.SelectCommand.Parameters.AddWithValue("@RunningHours", rnghrs);
                                        pp.SelectCommand.Parameters.AddWithValue("@IsActive", true);
                                        DataTable dd = new DataTable();
                                        pp.Fill(dd);
                                    }
                                    else
                                    {
                                        SqlDataAdapter pp511 = new SqlDataAdapter("select sum(runninghours) as rnghrs from mousedwinchtbl where WinchId=0  and ropeid=" + ropeid + " and Lead='" + lead + "'", sc.con);
                                        DataTable dd511 = new DataTable();
                                        pp511.Fill(dd511);
                                        if (dd511.Rows.Count > 0)
                                        {

                                            decimal totalsum = Convert.ToDecimal(dd511.Rows[0][0]);

                                           
                                            //SqlDataAdapter pp51 = new SqlDataAdapter("update winchrotation set RunningHours=" + totalsum + " where ropeid=" + ropeid + " and Lead='" + lead + "'", sc.con);
                                            SqlDataAdapter pp51 = new SqlDataAdapter("Updatetwinchrotation", sc.con);
                                            pp51.SelectCommand.CommandType = CommandType.StoredProcedure;
                                            pp51.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                            pp51.SelectCommand.Parameters.AddWithValue("@Runninghours", totalsum);
                                            pp51.SelectCommand.Parameters.AddWithValue("@Lead", lead);
                                            DataTable dd51 = new DataTable();
                                            pp51.Fill(dd51);
                                        }
                                    }


                                    try
                                    {
                                        //SqlDataAdapter adpt = new SqlDataAdapter("update MooringRopeDetail set CurrentLeadRunningHours = 0 where ID=" + ropeid + "", sc.con);
                                        //DataTable ddt = new DataTable();
                                        //adpt.Fill(ddt);
                                        SqlDataAdapter adpt = new SqlDataAdapter("UpdatetCurrentLead", sc.con);
                                        adpt.SelectCommand.CommandType = CommandType.StoredProcedure;
                                        adpt.SelectCommand.Parameters.AddWithValue("@CrntLeadrngHrs", 0);
                                        adpt.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                        DataTable ddt = new DataTable();
                                        adpt.Fill(ddt);
                                    }
                                    catch { }
                                }
                                //if (rnghrs == 0)
                                //{

                                //    SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                                //    DataTable dd = new DataTable();
                                //    pp.Fill(dd);
                                //}
                            }


                        }
                        //else
                        //{
                        //    try
                        //    {
                        //        var leadcheck = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Lead).SingleOrDefault();
                        //        decimal rnghrs = 0;
                        //        SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + leadcheck + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                        //        DataTable dd = new DataTable();
                        //        pp.Fill(dd);
                        //    }
                        //    catch { }
                        //}
                    }
                }
                else
                {

                    using (SqlDataAdapter adp = new SqlDataAdapter("select * from MOUsedWinchTbl where ropeid=" + ropeid + "", sc.con))
                    {
                        DataTable dt = new DataTable();
                        adp.Fill(dt);


                        if (dt.Rows.Count > 0)
                        {

                            var leedd = sc.MooringWinch.Where(x => x.Id == winchid).Select(x => x.Lead).SingleOrDefault();
                            leedd = leedd.Replace(Environment.NewLine, "").Trim();
                            //SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " group by ropeid,runninghours, Lead", sc.con);

                            SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " and lead ='" + leedd + "' group by ropeid, Lead", sc.con);
                            DataTable dd1 = new DataTable();
                            pp1.Fill(dd1);

                            //for (int i = 0; i < dd1.Rows.Count; i++)
                            if (dd1.Rows.Count > 0)
                            {
                                int rpid = Convert.ToInt32(dd1.Rows[0]["RopeId"]);
                                string lead = dd1.Rows[0]["Lead"].ToString();
                                decimal rnghrs = Convert.ToDecimal(dd1.Rows[0]["RunningHours"]);

                                decimal rnghrs1 = 0;

                                SqlDataAdapter pp2 = new SqlDataAdapter("select * from WinchRotation where RopeId=" + rpid + " and Lead='" + lead + "'", sc.con);
                                DataTable dd2 = new DataTable();
                                pp2.Fill(dd2);
                                if (dd2.Rows.Count > 0)
                                {
                                    rnghrs1 = Convert.ToDecimal(dd2.Rows[0]["RunningHours"]);

                                    if (rnghrs == rnghrs1)
                                    {
                                        rnghrs = 0;
                                    }
                                    else
                                    {
                                        if (rnghrs1 > rnghrs)
                                        {
                                            rnghrs = rnghrs1 - rnghrs;
                                        }
                                        if (rnghrs1 < rnghrs)
                                        {
                                            rnghrs = rnghrs - rnghrs1;
                                        }

                                    }
                                }


                                if (rnghrs != 0)
                                {

                                    // SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                                    SqlDataAdapter pp = new SqlDataAdapter("InsertWinchRotation", sc.con);
                                    pp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    pp.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                    pp.SelectCommand.Parameters.AddWithValue("@AssignedDate", assigneddate);
                                    pp.SelectCommand.Parameters.AddWithValue("@WinchId", winchid);
                                    pp.SelectCommand.Parameters.AddWithValue("@Lead", lead);
                                    pp.SelectCommand.Parameters.AddWithValue("@RunningHours", rnghrs);
                                    pp.SelectCommand.Parameters.AddWithValue("@IsActive", true);
                                    DataTable dd = new DataTable();
                                    pp.Fill(dd);


                                    try
                                    {
                                        //SqlDataAdapter adpt = new SqlDataAdapter("update MooringRopeDetail set CurrentLeadRunningHours = 0 where ID=" + ropeid + "", sc.con);
                                        //DataTable ddt = new DataTable();
                                        //adpt.Fill(ddt);
                                        SqlDataAdapter adpt = new SqlDataAdapter("UpdatetCurrentLead", sc.con);
                                        adpt.SelectCommand.CommandType = CommandType.StoredProcedure;
                                        adpt.SelectCommand.Parameters.AddWithValue("@CrntLeadrngHrs", 0);
                                        adpt.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                        DataTable ddt = new DataTable();
                                        adpt.Fill(ddt);
                                    }
                                    catch { }
                                }
                                //if (rnghrs == 0)
                                //{

                                    //    SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                                    //    DataTable dd = new DataTable();
                                    //    pp.Fill(dd);
                                    //}
                                }


                        }
                        //else
                        //{
                        //    try
                        //    {
                        //        var leadcheck = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Lead).SingleOrDefault();
                        //        decimal rnghrs = 0;
                        //        SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + leadcheck + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                        //        DataTable dd = new DataTable();
                        //        pp.Fill(dd);
                        //    }
                        //    catch { }
                        //}
                    }


                }
            }
            catch (Exception ex) { }
        }

        //private void lead_check(int ropeid,DateTime? assigneddate,int winchid)
        //{
        //    try
        //    {
        //        //DataTable dgg = new DataTable();
        //        //using (SqlDataAdapter adp = new SqlDataAdapter("select * from assignropetowinch	  where ropeid=" + ropeid + " and IsActive=1", sc.con))
        //        //{
        //        //    //DataTable dt = new DataTable();
        //        //    adp.Fill(dgg);
        //        //}
        //        //if (dgg.Rows.Count > 0)
        //        //{


        //            using (SqlDataAdapter adp = new SqlDataAdapter("select * from MOUsedWinchTbl where ropeid=" + ropeid + "", sc.con))
        //            {
        //                DataTable dt = new DataTable();
        //                adp.Fill(dt);


        //                if (dt.Rows.Count > 0)
        //                {

        //                    //SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " group by ropeid,runninghours, Lead", sc.con);

        //                    SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " group by ropeid, Lead", sc.con);
        //                    DataTable dd1 = new DataTable();
        //                    pp1.Fill(dd1);

        //                    for (int i = 0; i < dd1.Rows.Count; i++)
        //                    {
        //                        int rpid = Convert.ToInt32(dd1.Rows[i]["RopeId"]);
        //                        string lead = dd1.Rows[i]["Lead"].ToString();
        //                        decimal rnghrs = Convert.ToDecimal(dd1.Rows[i]["RunningHours"]);

        //                        decimal rnghrs1 = 0;

        //                        SqlDataAdapter pp2 = new SqlDataAdapter("select * from WinchRotation where RopeId=" + rpid + " and Lead='" + lead + "'", sc.con);
        //                        DataTable dd2 = new DataTable();
        //                        pp2.Fill(dd2);
        //                        if (dd2.Rows.Count > 0)
        //                        {
        //                            rnghrs1 = Convert.ToDecimal(dd2.Rows[0]["RunningHours"]);

        //                            if (rnghrs == rnghrs1)
        //                            {
        //                                rnghrs = 0;
        //                            }
        //                            else
        //                            {
        //                                if (rnghrs1 > rnghrs)
        //                                {
        //                                    rnghrs = rnghrs1 - rnghrs;
        //                                }
        //                                if (rnghrs1 < rnghrs)
        //                                {
        //                                    rnghrs = rnghrs - rnghrs1;
        //                                }

        //                            }
        //                        }


        //                        if (rnghrs != 0)
        //                        {

        //                            SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
        //                            DataTable dd = new DataTable();
        //                            pp.Fill(dd);



        //                            try
        //                            {
        //                                SqlDataAdapter adpt = new SqlDataAdapter("update MooringRopeDetail set CurrentLeadRunningHours = 0 where ID=" + ropeid + "", sc.con);
        //                                DataTable ddt = new DataTable();
        //                                adpt.Fill(ddt);
        //                            }
        //                            catch { }
        //                        }
        //                        //if (rnghrs == 0)
        //                        //{

        //                        //    SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
        //                        //    DataTable dd = new DataTable();
        //                        //    pp.Fill(dd);
        //                        //}
        //                    }


        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                        var leadcheck = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Lead).SingleOrDefault();
        //                        decimal rnghrs = 0;
        //                        SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + leadcheck + "','" + rnghrs + "','" + true + "' ); ", sc.con);
        //                        DataTable dd = new DataTable();
        //                        pp.Fill(dd);




        //                        SqlDataAdapter adpt = new SqlDataAdapter("update MooringRopeDetail set CurrentLeadRunningHours = 0 where ID=" + ropeid + "", sc.con);
        //                        DataTable ddt = new DataTable();
        //                        adpt.Fill(ddt);

        //                    }
        //                    catch { }
        //                }
        //            }
        //        //}
        //        //else
        //        //{
        //        //    try
        //        //    {
        //        //        var leadcheck = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Lead).SingleOrDefault();
        //        //        decimal rnghrs = 0;
        //        //        SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + leadcheck + "','" + rnghrs + "','" + true + "' ); ", sc.con);
        //        //        DataTable dd = new DataTable();
        //        //        pp.Fill(dd);




        //        //        SqlDataAdapter adpt = new SqlDataAdapter("update MooringRopeDetail set CurrentLeadRunningHours = 0 where ID=" + ropeid + "", sc.con);
        //        //        DataTable ddt = new DataTable();
        //        //        adpt.Fill(ddt);

        //        //    }
        //        //    catch { }
        //        //}


        //    }
        //    catch (Exception ex) { }
        //}
        public ICommand RadioBTNCommand { get; private set; }
        private static AssignModuleToWinchClass _Assignmoduletowinch = new AssignModuleToWinchClass();
        private void RadioBTNmethod(object parameter)
        {
            var bb = (string)parameter;

            if (bb == "Outboard")
            {
                _Assignmoduletowinch.Outboard = true;
                _Assignmoduletowinch.Outboard1 = false;
                StaticHelper.Wathckeeping = true;
            }
            else
            {
                _Assignmoduletowinch.Outboard = false;
                _Assignmoduletowinch.Outboard1 = true;
                StaticHelper.Wathckeeping = false;
            }

            OnPropertyChanged(new PropertyChangedEventArgs("AssignRopeToWinch"));
        }
        private void UpdateMooringWinch(AssignModuleToWinchClass moorwinch)
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



                var local = sc.Set<AssignModuleToWinchClass>()
                 .Local
                 .FirstOrDefault(f => f.Id == moorwinch.Id);
                if (local != null)
                {
                    sc.Entry(local).State = EntityState.Detached;
                }
                if (moorwinch.Outboard == true)
                {
                    moorwinch.Outboard = true;
                }
                if (moorwinch.Outboard == false)
                {
                    moorwinch.Outboard = false;
                }

                var UpdatedAssRopeWinch = new AssignModuleToWinchClass()
                {

                    Id = moorwinch.Id,

                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    AssignedLocation=moorwinch.AssignedLocation,
                    CreatedBy = moorwinch.CreatedBy,
                    RopeId= SRopeType.Id,
                    WinchId = SRopeAss.Id,
                    CreatedDate=moorwinch.CreatedDate,
                    Outboard= moorwinch.Outboard,
                    RopeTail = 0,
                    
            };
                //moorwinch.RopeId = SRopeType.Id;
                //moorwinch.WinchId = SRopeAss.Id;
                var duplcheck = sc.AssignRopetoWinch.Where(x => x.RopeId == SRopeType.Id).FirstOrDefault();
                if (duplcheck == null)
                {
                    sc.Entry(UpdatedAssRopeWinch).State = EntityState.Modified;
                    sc.SaveChanges();


                    StaticHelper.Editing = false;
                    MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);

                    CancelMooringWinch();
                }
                else
                {
                    MessageBox.Show("Line Type already exist ", "Assign Line To Winch", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void CancelMooringWinch()
        {
            var lostdata = new ObservableCollection<AssignModuleToWinchClass>(sc.AssignRopetoWinch.ToList());
            AssignRopeToWinchDetailViewModel cc = new AssignRopeToWinchDetailViewModel(lostdata);

            ChildWindowManager.Instance.CloseChildWindow();

            //var lostdata = new ObservableCollection<DepartmentClass>(sc.Departments.ToList());
            //DepartmentViewModel cc = new DepartmentViewModel(lostdata);

            //new MooringWinchViewModel();
            //new CrewDetailViewModel();
            //ChildWindowManager.Instance.CloseChildWindow();
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


    }

   //public class Ropetypecombo
   // {
   //     public int Id { get; set; }
   //     public string CertificateNo { get; set; }
   // }

   
   // public class Winchcombo
   // {
   //     public int Id { get; set; }
   //     public string AssignedNumber { get; set; }
   // }
}
