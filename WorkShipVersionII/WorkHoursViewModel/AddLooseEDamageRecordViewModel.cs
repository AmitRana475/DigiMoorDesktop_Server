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
    public class AddLooseEDamageRecordViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;

        public AddLooseEDamageRecordViewModel(LooseEDamageRecordClass edeps)
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
        }
        public AddLooseEDamageRecordViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<LooseEDamageRecordClass>(Saveropespl);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            looseEtype = GetLooseEType();


            GetMooringOperation();
            assignrope = GetAssRope();

        }
        public void refreshform()
        {
            RopeDamage = new LooseEDamageRecordClass();
            RaisePropertyChanged("RopeDamage");


            DamageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("DamageDate");
            ComboValue1 = null; RaisePropertyChanged("ComboValue1");
            ComboValue = null; RaisePropertyChanged("ComboValue");
            Smoorop = null; RaisePropertyChanged("Smoorop");
            SRopeType = null; RaisePropertyChanged("SRopeType");
            ComboValue2 = null; RaisePropertyChanged("ComboValue2");

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
        #region BindMoorOperation



        public static MooringOperationCombo smoorop;// = new Ropetypecombo();
        public MooringOperationCombo Smoorop
        {
            get
            {

                if (smoorop != null)
                {

                    RopeDamage.MOpId = smoorop.OPId;





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

        public void GetRopeType()
        {
            //ObservableCollection<Ropetypecombo> AddRopeType = new ObservableCollection<Ropetypecombo>();
            ropetype.Clear();
            var data = sc.MooringWinchRope.Where(x => x.DeleteStatus == false).Select(x => new { x.Id, x.CertificateNumber }).ToList();
            Ropetypecombo rop;
            foreach (var item in data)
            {
                rop = new Ropetypecombo();
                rop.Id = item.Id;
                rop.CertificateNo = item.CertificateNumber;
                ropetype.Add(rop);
            }
            OnPropertyChanged(new PropertyChangedEventArgs("ropetype"));
            //return AddRopeType;
        }

        #endregion







        public static LooseEDamageRecordClass _ropedamageClass = new LooseEDamageRecordClass();

        public LooseEDamageRecordClass RopeDamage
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



        public void LooseE_Notification(int looseETypeId, int id)
        {
            try
            {

               
                var looseE = sc.LooseEDamageR.Where(x => x.Id == id && x.LooseETypeId == looseETypeId).FirstOrDefault();

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
                noti.LooseEqType = looseETypeId;
                sc.Notifications.Add(noti);
                sc.SaveChanges();

                StaticHelper.AlarmFunction(1, true);
               
            }
            catch { }
        }

        #endregion
        private void Saveropespl(LooseEDamageRecordClass rpspc)
        {
            try
            {
                MainViewModelWorkHours.CommonValue = false;
                if (rpspc.DamageObserved == "Mooring Operation")
                {
                    if (rpspc.MOpId == null)
                    {
                        MainViewModelWorkHours.CommonValue = true;
                        MessageBox.Show("Please Choose Mooring Operation", "Loose Eq. Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                //var maxNotiid = sc.Notifications.Select(x => x.Id).Max();
                //int notiid = maxNotiid + 1;

                int notiid = sc.NextNotiId();

                if (rpspc.CertificateNumber == null || rpspc.CertificateNumber == "--Select--")
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please Select Loose Eq. Certificate Number ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (rpspc.IncidentReport == null)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please enter incident report ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (rpspc.DamageReason == null || rpspc.DamageReason == "--Select--")
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please enter Damage Reason ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (rpspc.DamageObserved == null)
                {
                    MainViewModelWorkHours.CommonValue = true;
                    MessageBox.Show("Please enter damage observed ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //if (rpspc.ty == null)
                //{
                //    MainViewModelWorkHours.CommonValue = true;
                //    MessageBox.Show("Please enter damage observed ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}


                rpspc.LooseETypeId = rpspc.LooseETypeId;
                rpspc.CreatedDate = DateTime.Now;
                rpspc.CreatedBy = "Admin";
                rpspc.IsActive = true;
                rpspc.NotificationId = notiid;

                sc.LooseEDamageR.Add(rpspc);
                sc.SaveChanges();
                MessageBox.Show("Record saved successfully ", "LooseE Damage", MessageBoxButton.OK, MessageBoxImage.Information);

                LooseE_Notification(rpspc.LooseETypeId, rpspc.Id);

                CancelMooringWinch();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
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
