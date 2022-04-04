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
  public  class RopeDiscardRecordViewModel :ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public ICommand HelpCommand { get; private set; }
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;

        public RopeDiscardRecordViewModel(RopeDiscardRecordClass edeps)
        {
            StaticHelper.Editing = true;

            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            // saveCommand = new RelayCommand<RopeDiscardRecordClass>(UpdateMooringWinch);
            cancelCommand = new RelayCommand(CancelMooringWinch);
             
        HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

        OutofServiceDate = edeps.OutofServiceDate;

            Ropetypecombo abc = new Ropetypecombo()
            {
                Id = edeps.RopeId,
                CertificateNo = edeps.CertificateNumber
            };
            SRopeType = abc;

         
        }
        public RopeDiscardRecordViewModel()
        {
            StaticHelper.Editing = true;

            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            saveCommand = new RelayCommand<RopeDiscardRecordClass>(Saveropespl);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            GetRopeType();

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            GetMooringOperation();
            assignrope = GetAssRope();
            outofsreason = GetOutofSReason();

        }

        private static Nullable<DateTime> _OutofServiceDate = null;
        public Nullable<DateTime> OutofServiceDate
        {
            get
            {
                if (_OutofServiceDate == null)
                {
                    //_OutofServiceDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                else
                _ropediscardClass.OutofServiceDate = (DateTime)_OutofServiceDate;
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
        public void resetform()
        {
            try
            {

                erinfo = 0;
                RopeDiscard = new RopeDiscardRecordClass();
                RaisePropertyChanged("RopeDiscard");

                //AddMooringWinchRopeMessages = new AddMooringRopeErrorMessages();
                //RaisePropertyChanged("AddMooringWinchRopeMessages");


                OutofServiceDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("OutofServiceDate");
                //InstalledDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("InstalledDate");
              
                SRopeReasonoutofs = null; RaisePropertyChanged("SRopeReasonoutofs");
                ComboValue1 = null; RaisePropertyChanged("ComboValue1");
                ComboValue2 = null; RaisePropertyChanged("ComboValue2");
                ComboValue = null; RaisePropertyChanged("ComboValue");
                SRopeType = null; RaisePropertyChanged("SRopeType");
                //SManuFName = null; RaisePropertyChanged("SManuFName");
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
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

        public void resetRopeDiscard()
        {
            try
            {

                erinfo = 0;
                RopeDiscard = new RopeDiscardRecordClass();
                RaisePropertyChanged("RopeDiscard");

                OutofServiceDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("OutofServiceDate");
            
                SRopeType = null; RaisePropertyChanged("SRopeType");
                Smoorop = null; RaisePropertyChanged("Smoorop");
                //OutofServiceReason = null; RaisePropertyChanged("OutofServiceReason");


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
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
                    RopeDiscard.ReasonOutofService = srreasonoutofservice;
                if (srreasonoutofservice == "MASTER")
                    RopeDiscard.ReasonOutofService = "All";

                return srreasonoutofservice;
            }

            set
            {
                srreasonoutofservice = value;
                if (srreasonoutofservice != null)
                    RopeDiscard.ReasonOutofService = srreasonoutofservice;
                OnPropertyChanged(new PropertyChangedEventArgs("SRopeReasonoutofs"));
            }
        }

        public void GetRopeType()
        {
            ropetype.Clear();
            //ObservableCollection<Ropetypecombo> AddRopeType = new ObservableCollection<Ropetypecombo>();
            Ropetypecombo rop;
            SqlDataAdapter adp = new SqlDataAdapter("GetActiveAssignRopeType1", sc.con);
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
            //return ropetype;

            
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







        public static RopeDiscardRecordClass _ropediscardClass = new RopeDiscardRecordClass();

        public RopeDiscardRecordClass RopeDiscard
        {
            get
            {

                return _ropediscardClass;
            }
            set
            {
                _ropediscardClass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RopeDiscard"));
            }
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

        public static Ropetypecombo sropetype;// = new Ropetypecombo();
        public Ropetypecombo SRopeType
        {
            get
            {

                if (sropetype != null)
                {
                    var data = sc.AssignRopetoWinch.Where(x => x.RopeId == sropetype.Id && x.IsActive==true).FirstOrDefault();
                    if (data != null)
                    {
                        //MessageBox.Show("This rope is currently assigned to a Winch and is in active list. Please go to Assign to Winch option under Mooring Rope menu and 'Shift to Inactive' list.", "Rope Discard", MessageBoxButton.OK, MessageBoxImage.Warning);
                        //return sropetype;
                        //var data1 = sc.MooringWinchRope.Where(x => x.Id == sropetype.Id && x.DeleteStatus == false).FirstOrDefault();
                        //RopeDiscard.RopeId = data.RopeId;
                        //CrossShifting.OutboadEndinUse = data.Outboard;


                    }
                    else
                    {
                        var data1 = sc.MooringWinchRope.Where(x => x.Id == sropetype.Id && x.DeleteStatus == false).FirstOrDefault();
                        RopeDiscard.RopeId = data1.Id;
                    }
                    //RopeEndToEnd.OutboardEndinUse = data1.Outboard;
                    OnPropertyChanged(new PropertyChangedEventArgs("RopeDiscard"));
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


        public static MooringOperationCombo smoorop;// = new Ropetypecombo();
        public MooringOperationCombo Smoorop
        {
            get
            {

                if (smoorop != null)
                {
               
                        RopeDiscard.MOpId = smoorop.OPId;
                   


                  
                    
                    OnPropertyChanged(new PropertyChangedEventArgs("RopeDiscard"));
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
                    RopeDiscard.DamageObserved = _sdamgeobs;


                return _sdamgeobs;
            }

            set
            {
                _sdamgeobs = value;
                if (_sdamgeobs != null)
                    RopeDiscard.DamageObserved = _sdamgeobs;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue"));
            }
        }

        public static string _sincrep;
        public string ComboValue1
        {

            get
            {
                if (_sincrep != null)
                    RopeDiscard.MooringOperation = _sincrep;


                return _sincrep;
            }

            set
            {
                _sincrep = value;
                if (_sincrep != null)
                    RopeDiscard.MooringOperation = _sincrep;
                OnPropertyChanged(new PropertyChangedEventArgs("ComboValue1"));
            }
        }

        public static string _smoorop;
        public string ComboValue2
        {

            get
            {
                if (_smoorop != null)
                    RopeDiscard.MooringOperation = _smoorop;


                return _smoorop;
            }

            set
            {
                _smoorop = value;
                if (_smoorop != null)
                    RopeDiscard.MooringOperation = _smoorop;
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
        private void Saveropespl(RopeDiscardRecordClass rpspc)
        {
            try
            {
                MainViewModelWorkHours.CommonValue = false;
                rpspc.RopeId = rpspc.RopeId;


                var local = sc.Set<MooringWinchRopeClass>()
                      .Local
                      .FirstOrDefault(f => f.Id == rpspc.RopeId);
                if (local != null)
                {
                    sc.Entry(local).State = EntityState.Detached;
                }

                var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == rpspc.RopeId && b.RopeTail == 0 && b.DeleteStatus == false);
                if (result != null)
                {
                    result.OutofServiceDate = rpspc.OutofServiceDate;

                    if (rpspc.ReasonOutofService == null || rpspc.ReasonOutofService == "")
                    {
                        MainViewModelWorkHours.CommonValue = true;
                        MessageBox.Show("Please choose out of service reason.", "Line Discard", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (rpspc.DamageObserved == "--Select--")
                    {
                        MainViewModelWorkHours.CommonValue = true;
                        MessageBox.Show("Please choose damage observed", "Line Discard", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    result.ReasonOutofService = rpspc.ReasonOutofService;

                    if(rpspc.ReasonOutofService=="Damaged")
                    {
                        if(rpspc.DamageObserved=="Mooring Operation")
                        {
                            if (rpspc.MOpId == 0)
                            {
                                MainViewModelWorkHours.CommonValue = true;
                                MessageBox.Show("Please Choose Mooring Operation", "Line Discard", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                            else
                            {
                                if (rpspc.DamageObserved == "--Select--")
                                {
                                    MainViewModelWorkHours.CommonValue = true;
                                    MessageBox.Show("Please Choose Damage Observed", "Line Discard", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    return;
                                }
                                else
                                {

                                    result.DamageObserved = rpspc.DamageObserved;
                                    result.MooringOperationID = rpspc.MOpId;
                                }
                            }
                        }
                    }
                    result.DamageObserved = rpspc.DamageObserved;
                    result.OtherReason = rpspc.OtherReason;
                    //result.OutOfService_WinchId = rpspc.OutOfService_WinchId;
                    //result.MooringOperationID = rpspc.MOpId;
                    result.ModifiedBy = "Admin";
                   
                    result.ModifiedDate = DateTime.Now;

                    sc.Entry(result).State = EntityState.Modified;
                   // sc.SaveChanges();
                }


                var result1 = sc.AssignRopetoWinch.SingleOrDefault(b => b.RopeId == rpspc.RopeId && b.RopeTail == 0 && b.IsActive == true);
                if (result1 != null)
                {
                  
                    result1.IsActive = false;
                    result1.ModifiedBy = "Admin";

                    result1.ModifiedDate = DateTime.Now;
                   // sc.SaveChanges();
                }

                int notiid = sc.NextNotiId();
                RopeDamageRecordClass rpdm = new RopeDamageRecordClass();
                rpdm.RopeId = rpspc.RopeId;
                rpdm.DamageObserved = rpspc.DamageObserved;
                rpdm.DamageDate = rpspc.OutofServiceDate;
                rpdm.CreatedDate = DateTime.Now;
                rpdm.CreatedBy = "Admin";
                rpdm.IsActive = true;
                rpdm.NotificationId = notiid;
                rpdm.IncidentReport = "No";
                rpdm.RopeTail = 0;

                sc.RopeDamage.Add(rpdm);
                //sc.SaveChanges();


                MessageBox.Show("Record updated successfully ", "Line Discard", MessageBoxButton.OK, MessageBoxImage.Information);


                try
                {
                    var mrRope = sc.MooringWinchRope.Where(x=> x.DeleteStatus==false).ToList();
                    var winchid = sc.AssignRopetoWinch.Where(x => x.RopeId == rpspc.RopeId && x.IsActive == true).Select(x => x.WinchId).SingleOrDefault();
                    //var length = sc.MooringWinchRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.Length).SingleOrDefault();
                    //var percent = (length * 10) / 100;
                    //var crplength = sc.RopeCropping.Where(x => x.RopeId == rpspc.RopeId).Select(x => x.LengthofCroppedRope).Sum();
                    var ropetypeid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.RopeTypeId).SingleOrDefault();
                    var manufacid = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.ManufacturerId).SingleOrDefault();
                    //var ropename = sc.MooringRopeType.Where(x => x.Id == ropetypeid).Select(x => x.RopeType).SingleOrDefault();
                    var ropename = mrRope.Where(x => x.Id == rpspc.RopeId).Select(x => x.CertificateNumber + " - " + x.UniqueID).SingleOrDefault();

                    var WinchName = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.AssignedNumber).SingleOrDefault();
                    var WinchLocation = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Location).SingleOrDefault();


                    var notification = "";
                    if (WinchName != null && WinchName != "" && WinchName != "Not Assigned")
                    {
                        notification = "Out of Service / discarded - Line " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                    }
                    else
                    {
                        notification = "Out of Service / discarded - Line " + ropename + "";
                    }

                    //var notification = "Out of Service / discarded - Rope " + ropename + " on winch " + WinchName + " located at " + WinchLocation + "";
                    NotificationsClass noti = new NotificationsClass();
                    noti.Acknowledge = false;
                    noti.AckRecord = "Not yet acknowledged";
                    noti.Notification = notification;
                    noti.NotificationType = 1;
                    noti.RopeId = rpspc.RopeId;
                    noti.IsActive = true;
                    noti.NotificationAlertType = (int)NotificationAlertType.OutofService_discarded_Rope;
                    //noti.NotificationDueDate = DBNull.Value;
                    noti.CreatedDate = DateTime.Now;
                    noti.CreatedBy = "Admin";
                    sc.Notifications.Add(noti);
                   // sc.SaveChanges();

                    StaticHelper.AlarmFunction(1, true);


                    SqlDataAdapter adp = new SqlDataAdapter("select NotificationAlertType from Notifications where RopeId=" + rpspc.RopeId + "", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int gg = Convert.ToInt32(dt.Rows[i][0]);

                        int[] RopesNTails = { 5, 6, 7, 8, 9, 95, 10 };
                        if (RopesNTails.Contains(gg) == true)
                        {
                            //SqlDataAdapter adp1 = new SqlDataAdapter("update Notifications set IsActive='false' where NotificationAlertType=" + gg + "", sc.con);
                            //DataTable dt1 = new DataTable();
                            //adp1.Fill(dt1);
                            SqlDataAdapter adp1 = new SqlDataAdapter("UpdatetNotifications1", sc.con);
                            adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp1.SelectCommand.Parameters.AddWithValue("@NotificationAlertType", gg);
                            DataTable dt1 = new DataTable();
                            adp1.Fill(dt1);
                        }
                    }

                    sc.SaveChanges();
                }
                catch { }


                CancelMooringWinch();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

      

      
        private void CancelMooringWinch()
        {
            // var lostdata = new ObservableCollection<RopeDiscardRecordClass>(sc.MooringWinchRope.ToList().Where(x => x.DeleteStatus == false));
            MainViewModelWorkHours.CommonValue = false;
            RopeDiscardListViewModel cc = new RopeDiscardListViewModel();
            cc.GetRopeDiscardList();
            ChildWindowManager.Instance.CloseChildWindow();
            resetform();

           
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
