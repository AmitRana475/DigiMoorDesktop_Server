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
   public class AddLooseEDisposalViewModel :ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddLooseEDisposalViewModel(LooseEDisposalClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<LooseEDisposalClass>(UpdateRopeDisposal);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            EditRopeDisposal(edeps);

            var findid = sc.LooseEDisposals.Where(x => x.Id == edeps.Id).FirstOrDefault();
            DisposalDate = findid.DisposalDate;
            ropetype = GetRopeType();
            Ropetypecombo abc = new Ropetypecombo()
            {
                Id = edeps.LooseETypeId,
                CertificateNo = edeps.CertificateNo
            };
            SRopeType = abc;
        }
        public AddLooseEDisposalViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<LooseEDisposalClass>(SaveLooseEDisposal);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            ropetype = GetRopeType();
        }

       
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
        private ObservableCollection<Ropetypecombo> GetRopeType()
        {
            ObservableCollection<Ropetypecombo> AddRopeType = new ObservableCollection<Ropetypecombo>();
            Ropetypecombo rop;
            SqlDataAdapter adp = new SqlDataAdapter("select * from looseetype where id !=2", sc.con);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                rop = new Ropetypecombo();
                rop.Id = (int)row["Id"];
                rop.CertificateNo = (string)row["looseequipmenttype"];
                AddRopeType.Add(rop);
            }
            return AddRopeType;
        }
        public void refreshform()
        {
            AddLooseEDisposal = new LooseEDisposalClass();
            RaisePropertyChanged("AddLooseEDisposal");
           // Smoorop = null; RaisePropertyChanged("Smoorop");
            SRopeType = null; RaisePropertyChanged("SRopeType");


        }
        public static Ropetypecombo sropetype;// = new Ropetypecombo();
        public Ropetypecombo SRopeType
        {
            get
            {

                if (sropetype != null)
                {
                    var data = sc.AssignRopetoWinch.Where(x => x.RopeId == sropetype.Id).FirstOrDefault();
                    if (data != null)
                    {
                        var data1 = sc.MooringWinchRope.Where(x => x.Id == sropetype.Id && x.DeleteStatus == false).FirstOrDefault();
                        AddLooseEDisposal.LooseETypeId = data.RopeId;
                        //CrossShifting.OutboadEndinUse = data.Outboard;


                    }
                    //RopeEndToEnd.OutboardEndinUse = data1.Outboard;
                    OnPropertyChanged(new PropertyChangedEventArgs("AddRopeDisposal"));
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

        private string mooringWinchMessage;
        public string MooringWinchMessage
        {
            get { return mooringWinchMessage; }
            set { mooringWinchMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("MooringWinchMessage")); }
        }

        public static LooseEDisposalClass _AddLooseEDisposal = new LooseEDisposalClass();
        public LooseEDisposalClass AddLooseEDisposal
        {
            get
            {
                MooringWinchMessage = string.Empty;
                RaisePropertyChanged("MooringWinchMessage");
                return _AddLooseEDisposal;
            }
            set
            {
                _AddLooseEDisposal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddLooseEDisposal"));
            }
        }


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

        private static Nullable<DateTime> _DisposalDate = null;
        public Nullable<DateTime> DisposalDate
        {
            get
            {
                if (_DisposalDate == null)
                {
                    _DisposalDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                AddLooseEDisposal.DisposalDate = (DateTime)_DisposalDate;
                return _DisposalDate;
            }
            set
            {
                _DisposalDate = value;
                RaisePropertyChanged("DisposalDate");
            }
        }
        private void SaveLooseEDisposal(LooseEDisposalClass lsedisposal)
        {
            try
            {
                lsedisposal.LooseETypeId = lsedisposal.LooseETypeId != 0 ? lsedisposal.LooseETypeId : lsedisposal.LooseETypeId;
                if (lsedisposal.LooseETypeId != 0)
                {
                    var findassno = sc.LooseEDisposals.Where(x => x.LooseETypeId == lsedisposal.LooseETypeId && x.LooseECertiNo == lsedisposal.LooseECertiNo).FirstOrDefault();

                    if (findassno == null)
                    {

                        if(lsedisposal.LooseECertiNo==null || lsedisposal.LooseECertiNo=="--Select--")
                        {
                            MessageBox.Show("Please Select Loose Eq. Certificate Number ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        if (lsedisposal.DisposalPortName == null  || lsedisposal.DisposalPortName == "")
                        {
                            MessageBox.Show("Please enter disposal port name ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        if (lsedisposal.ReceptionFacilityName == null || lsedisposal.ReceptionFacilityName == "")
                        {
                            MessageBox.Show("Please enter reception facility name ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        //rpdisposal.RopeId = textinfo.ToTitleCase(rpdisposal.AssignedNumber.ToLower());
                        lsedisposal.DisposalPortName = lsedisposal.DisposalPortName;
                        lsedisposal.ReceptionFacilityName = lsedisposal.ReceptionFacilityName;
                        lsedisposal.DisposalDate = lsedisposal.DisposalDate;
                        lsedisposal.DiscardedDate = lsedisposal.DiscardedDate;
                        lsedisposal.CreatedDate = DateTime.Now;
                        lsedisposal.CreatedBy = "Admin";
                        lsedisposal.IsActive = true;

                        sc.LooseEDisposals.Add(lsedisposal);
                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record saved successfully ", "Add Rope Disposal", MessageBoxButton.OK, MessageBoxImage.Information);
                        //AddLooseEDisposal = new LooseEDisposalClass();
                        //RaisePropertyChanged("AddLooseEDisposal");

                        CancelMooringWinch();
                    }
                    else
                    {
                        MessageBox.Show("MooringWinch already exist ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {

                    MooringWinchMessage = "Please Enter the MooringWinch Name";
                    RaisePropertyChanged("MooringWinchMessage");
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


     
        private void UpdateRopeDisposal(LooseEDisposalClass rpdisposal)
        {
            try
            {
                rpdisposal.LooseETypeId = rpdisposal.LooseETypeId != 0 ? rpdisposal.LooseETypeId : rpdisposal.LooseETypeId;
                if (rpdisposal.LooseETypeId != 0)
                {

                    var findrank = sc.LooseEDisposals.Where(x => x.Id == rpdisposal.Id).FirstOrDefault();

                    if (findrank != null)
                    {
                        //rpdisposal.RopeId = textinfo.ToTitleCase(rpdisposal.RopeId);



                        var local = sc.Set<LooseEDisposalClass>()
                         .Local
                         .FirstOrDefault(f => f.Id == rpdisposal.Id);
                        if (local != null)
                        {
                            sc.Entry(local).State = EntityState.Detached;
                        }

                        if (rpdisposal.LooseECertiNo == null || rpdisposal.LooseECertiNo == "--Select--")
                        {
                            MessageBox.Show("Please Select Loose Eq. Certificate Number ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        if (rpdisposal.DisposalPortName == null)
                        {
                            MessageBox.Show("Please enter disposal port name ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        if (rpdisposal.ReceptionFacilityName == null)
                        {
                            MessageBox.Show("Please enter reception facility name ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        var UpdatedLocation = new LooseEDisposalClass()
                        {

                            Id = rpdisposal.Id,
                            LooseETypeId = rpdisposal.LooseETypeId,
                            DisposalPortName = rpdisposal.DisposalPortName,
                            DiscardedDate = rpdisposal.DiscardedDate,
                            ReceptionFacilityName = rpdisposal.ReceptionFacilityName,
                            LooseECertiNo = rpdisposal.LooseECertiNo,
                            DisposalDate=rpdisposal.DisposalDate,
                            CreatedBy="Admin",
                            CreatedDate = DateTime.Now,
                            IsActive = true

                        };

                        sc.Entry(UpdatedLocation).State = EntityState.Modified;
                        sc.SaveChanges();


                        //Update into User's Table


                        //Update into WorkHours's Table
                        //var some = sc.WorkHourss.Where(x => x.Department.Equals(depat.Trim())).ToList();
                        //some.ForEach(a =>
                        //{
                        //    a.Department = UpdatedLocation.RopeId;
                        //});

                       // sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);

                        var looseE = sc.LooseEDisposals.Where(x => x.Id == rpdisposal.Id && x.LooseETypeId == rpdisposal.LooseETypeId).FirstOrDefault();

                        var looseEname = sc.LooseETypes.Where(x => x.Id == looseE.LooseETypeId).Select(x => x.LooseEquipmentType).SingleOrDefault();

                        var notification = "Disposed - Loose Equipment " + looseEname + " CertificateNo- " + looseE.LooseECertiNo + "";

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
                        noti.LooseCertificateNum = looseE.LooseECertiNo ;
                        noti.LooseEqType = rpdisposal.LooseETypeId;
                        sc.Notifications.Add(noti);
                        sc.SaveChanges();

                        StaticHelper.AlarmFunction(1, true);

                        CancelMooringWinch();

                    }

                }
                else
                {

                    MooringWinchMessage = "Please Enter the MooringWinch Name";
                    RaisePropertyChanged("MooringWinchMessage");
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void EditRopeDisposal(LooseEDisposalClass rpds)
        {
            try
            {

                var findrank = sc.LooseEDisposals.Where(x => x.Id == rpds.Id).FirstOrDefault();
                AddLooseEDisposal.DisposalPortName = findrank.DisposalPortName;
                AddLooseEDisposal.ReceptionFacilityName = findrank.ReceptionFacilityName;
                AddLooseEDisposal.Id = findrank.Id;
                AddLooseEDisposal.LooseETypeId = findrank.LooseETypeId;
                OnPropertyChanged(new PropertyChangedEventArgs("AddLooseEDisposal"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }

        private void CancelMooringWinch()
        {
            var lostdata = new ObservableCollection<LooseEDisposalClass>(sc.LooseEDisposals.ToList());
            LooseEDisposalListViewModel cc = new LooseEDisposalListViewModel(lostdata);

            ChildWindowManager.Instance.CloseChildWindow();
       
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
