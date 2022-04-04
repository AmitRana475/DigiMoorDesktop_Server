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
   public class AddRopeEndtoEndViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddRopeEndtoEndViewModel(RopeEndtoEndClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<RopeEndtoEnd2Class>(UpdateRopeendtoend);
            cancelCommand = new RelayCommand(CancelMooringWinch);

            // MooringWinchRopeClass data = sc.MooringWinchRope.Where(x => x.Id == edeps.Id).FirstOrDefault();


            //SRopeType = edeps.RopeType;
            //SRopeConst = edeps.RopeConstruction;
            //SRopeDiameter = edeps.DiaMeter;
            //SRopeReasonoutofs = edeps.ReasonOutofService;

            //ReceivedDate = edeps.ReceivedDate;

            RopeEndToEnd = new RopeEndtoEnd2Class()
            {
                Id = edeps.Id,
                //AssignedLocation = edeps.AssignedLocation,
                ModifiedDate = DateTime.Now,
                ModifiedBy = "Admin",
                CreatedDate = edeps.CreatedDate,
                CreatedBy = edeps.CreatedBy
            };

            Ropetypecombo abc = new Ropetypecombo()
            {
                Id = edeps.Id,
                //CertificateNo = edeps.CertificateNumber
            };
            SRopeType = abc;

            RaisePropertyChanged("AddRopeEndtoEndView");
            OnPropertyChanged(new PropertyChangedEventArgs("AddRopeEndtoEndView"));
            //EditMooringWinch(edeps);
        }
        public AddRopeEndtoEndViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<RopeEndtoEnd2Class>(SaveMooringWinchRope);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            resetCommand = new RelayCommand(resetRopeEndtoEnd);
            GetRopeType();
            assignrope = GetAssRope(0);

            resetRopeEndtoEnd();
        }

        public void resetRopeEndtoEnd()
        {
            try
            {

                erinfo = 0;
                RopeEndToEnd = new RopeEndtoEnd2Class();
                RaisePropertyChanged("RopeEndToEnd");

                //AddMooringWinchRopeMessages = new AddMooringRopeErrorMessages();
                //RaisePropertyChanged("AddMooringWinchRopeMessages");


                EndtoEndDoneDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("EndtoEndDoneDate");
                //InstalledDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("InstalledDate");

               // ComboValue7 = null; RaisePropertyChanged("ComboValue7");
                SRopeType = null; RaisePropertyChanged("SRopeType");
                //SManuFName = null; RaisePropertyChanged("SManuFName");
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





        public void GetRopeType()
        {
            ropetype.Clear();
            //ObservableCollection<Ropetypecombo> AddRopeType = new ObservableCollection<Ropetypecombo>();
            //var data = sc.MooringWinchRope.Select(x => new { x.Id, x.CertificateNumber }).ToList();

            Ropetypecombo rop;
            
            //SqlDataAdapter adp = new SqlDataAdapter("GetActiveAssignRopeType1", sc.con);
            SqlDataAdapter adp = new SqlDataAdapter("select a.id,a.UniqueID + ' - ' +  a.certificatenumber as certificatenumber,b.ropeid from MooringRopeDetail a inner join AssignRopeToWinch b  on a.id=b.ropeid where a.DeleteStatus=0 and OutofServiceDate is null and b.RopeTail=@RopeTail and b.IsActive=1 order by a.UniqueID asc", sc.con);
            //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
            DataTable ds = new DataTable();
            adp.Fill(ds);
            // var data = sc.AssignRopetoWinch.ToList();          
            foreach (DataRow row in ds.Rows)
            {
               

                    rop = new Ropetypecombo();
                    rop.Id = (int)row["Id"];
                    rop.CertificateNo = (string)row["certificatenumber"];
                ropetype.Add(rop);
                
            }

            OnPropertyChanged(new PropertyChangedEventArgs("ropetype"));
            //foreach (var item in data)
            //{

            //    rop = new Ropetypecombo();
            //    rop.Id = item.Id;
            //    rop.CertificateNo = item.CertificateNumber;
            //    AddRopeType.Add(rop);
            //}

            //return AddRopeType;
        }

        public ObservableCollection<Winchcombo> GetAssRope(int id)
        {
            ObservableCollection<Winchcombo> AddWinchId = new ObservableCollection<Winchcombo>();
            // ObservableCollection<Ropetypecombo> AddRopeType = new ObservableCollection<Ropetypecombo>();
            //var data = sc.MooringWinch.Where(x => !).Select(x => new { x.Id, x.AssignedNumber }).ToList();

            SqlDataAdapter adp = new SqlDataAdapter("select * from MooringWinchDetail where id not in (select id from MooringWinchDetail where id= " + id + ")", sc.con);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            Winchcombo rop;
            foreach (DataRow row in ds.Rows)
            {

                //rop = new Winchcombo();
                //rop.Id = item.Id;
                //rop.AssignedNumber = item.AssignedNumber;
                //AddWinchId.Add(rop);

                rop = new Winchcombo();
                rop.Id = (int)row["Id"];
                rop.AssignedNumber = (string)row["AssignedNumber"];
                AddWinchId.Add(rop);
            }
            OnPropertyChanged(new PropertyChangedEventArgs("AssignRope"));
            return AddWinchId;

        }

        #endregion







        public static RopeEndtoEnd2Class _ropeEndtoEndClass = new RopeEndtoEnd2Class();

        public RopeEndtoEnd2Class RopeEndToEnd
        {
            get {

                return _ropeEndtoEndClass;
            }
            set
            {
                _ropeEndtoEndClass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RopeEndToEnd"));
            }
        }

        private ICommand resetCommand;
        public ICommand ResetCommand
        {
            get { return resetCommand; }
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

                if(sropetype != null)
                {
                    var data = sc.AssignRopetoWinch.Where(x => x.RopeId == sropetype.Id && x.IsActive==true).FirstOrDefault();
                    if (data != null)
                    {
                        var data1 = sc.MooringWinchRope.Where(x => x.Id == sropetype.Id && x.DeleteStatus == false).FirstOrDefault();
                        RopeEndToEnd.RopeId = data.RopeId;
                        RopeEndToEnd.OutboadEndinUse = data.Outboard;


                        //assignrope.Clear();
                        assignrope = GetAssRope(data.WinchId);
                        RaisePropertyChanged("AssignRope");
                        //OnPropertyChanged(new PropertyChangedEventArgs("AssignRope"));
                        //RopeEndToEnd.WinchId = data.WinchId;

                    }
                    //RopeEndToEnd.OutboardEndinUse = data1.Outboard;
                    OnPropertyChanged(new PropertyChangedEventArgs("RopeEndToEnd"));
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


        //private static int sropetype;
        //public int SRopeType
        //{
        //    get
        //    {
        //        if (sropetype != 0)
        //            AddMooringWinchRope.Id = sropetype;
        //        //if (sropetype == "MASTER")
        //        //    AddMooringWinchRope.RopeType = "All";

        //        return sropetype;
        //    }

        //    set
        //    {
        //        sropetype = value;
        //        if (sropetype != 0)
        //            AddMooringWinchRope.Id = sropetype;
        //        OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
        //    }
        //}

       


      
        
      
        public static Nullable<DateTime> _EndtoEndDoneDate = null;
        public Nullable<DateTime> EndtoEndDoneDate
        {
            get
            {
                if (_EndtoEndDoneDate == null)
                {
                    _EndtoEndDoneDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                _ropeEndtoEndClass.EndtoEndDoneDate = (DateTime)_EndtoEndDoneDate;
                return _EndtoEndDoneDate;
            }
            set
            {
                _EndtoEndDoneDate = value;
                RaisePropertyChanged("EndtoEndDoneDate");
            }
        }

      
        #endregion
        private void SaveMooringWinchRope(RopeEndtoEnd2Class moorwinchrope)
        {
            try
            {
                MainViewModelWorkHours.CommonValue = false;
                    moorwinchrope.RopeId = moorwinchrope.RopeId;
                moorwinchrope.EndtoEndDoneDate = RopeEndToEnd.EndtoEndDoneDate;
               
                if(moorwinchrope.OutboadEndinUse==false)
                {
                    moorwinchrope.CurrentOutboadEndinUse = true;
                    var result = sc.AssignRopetoWinch.SingleOrDefault(b => b.RopeId == moorwinchrope.RopeId && b.IsActive==true && b.RopeTail==0);
                    if (result != null)
                    {
                        result.Outboard = true;
                        result.ModifiedBy = "Admin";
                        result.ModifiedDate = DateTime.Now;
                        sc.SaveChanges();
                    }
                    //sc.Entry(UpdatedRopedetails).Property(x => x.Outboard).IsModified = true;
                    //sc.Entry(UpdatedRopedetails).State = EntityState.Modified;
                    //sc.SaveChanges();

                }
                else
                {
                    moorwinchrope.CurrentOutboadEndinUse = false;
                 
                    var result = sc.AssignRopetoWinch.SingleOrDefault(b => b.RopeId == moorwinchrope.RopeId && b.IsActive == true);
                    if (result != null)
                    {
                        result.Outboard = false;
                        result.ModifiedBy = "Admin";
                        result.ModifiedDate = DateTime.Now;
                        sc.SaveChanges();
                    }
                }
           
            
                moorwinchrope.CreatedDate = DateTime.Now;
                moorwinchrope.CreatedBy = "Admin";
                moorwinchrope.IsActive = true;
                moorwinchrope.WasRopeShifted = "No";

                sc.RopeEndtoEnd2.Add(moorwinchrope);
                sc.SaveChanges();


                if(moorwinchrope.WasRopeShifted=="Yes")
                {
                    AssignModuleToWinchClass rgm = new AssignModuleToWinchClass();
                    if (moorwinchrope.Outboard == true)
                    {
                        rgm.Outboard = true;//  moorwinchrope.Outboard = true;
                    }
                    if (moorwinchrope.Outboard == false)
                    {
                        rgm.Outboard = false;
                        //moorwinchrope.Outboard = false;
                    }
                    rgm.RopeId = SRopeType.Id;
                    rgm.WinchId = StaticHelper.WinchId;
                    rgm.AssignedDate = RopeEndToEnd.AssignedDate;
                    rgm.CreatedDate = DateTime.Now;
                    rgm.CreatedBy = "Admin";
                    rgm.IsActive = true;
                    rgm.RopeTail = 0;

                    var location= sc.MooringWinch.Where(x => x.Id == StaticHelper.WinchId && x.IsActive == true).Select(x=> x.Location).SingleOrDefault();
                    rgm.AssignedLocation = location;

                    //var duplcheck = sc.AssignRopetoWinch.Where(x => x.RopeId == SRopeType.Id).FirstOrDefault();
                    //if (duplcheck == null)
                    //{

                    ////-----------

                    //var duplcheck1 = sc.AssignRopetoWinch.SingleOrDefault(x => x.WinchId == StaticHelper.WinchId && x.IsActive == true);
                    //    //var duplcheck1 = sc.AssignRopetoWinch.Where(x => x.WinchId == SRopeAss.Id).FirstOrDefault();

                    //    if (duplcheck1 != null)
                    //    {
                    //        duplcheck1.IsActive = false;
                    //        sc.SaveChanges();
                    //    }

                    //    sc.AssignRopetoWinch.Add(rgm);
                    //    sc.SaveChanges();


                    /////////---


                    var duplcheck = sc.AssignRopetoWinch.Where(x => x.RopeId == SRopeType.Id && x.WinchId == StaticHelper.WinchId && x.IsActive == true && x.RopeTail == 0).FirstOrDefault();
                    if (duplcheck == null)
                    {

                        var duplcheck1 = sc.AssignRopetoWinch.SingleOrDefault(x => x.RopeId == SRopeType.Id && x.IsActive == true && x.RopeTail == 0);
                        var duplcheck2 = sc.AssignRopetoWinch.SingleOrDefault(x => x.WinchId == StaticHelper.WinchId && x.IsActive == true && x.RopeTail == 0);

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



                        sc.AssignRopetoWinch.Add(rgm);
                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                       // MessageBox.Show("Record saved successfully ", "Assign Rope To Winch", MessageBoxButton.OK, MessageBoxImage.Information);
                        //AssignRopeToWinch = new AssignModuleToWinchClass();
                        //RaisePropertyChanged("AssignRopeToWinch");
                        //CancelMooringWinch();

                    }

                    //}
                }

                MessageBox.Show("Record saved successfully ", "Rope EndtoEnd", MessageBoxButton.OK, MessageBoxImage.Information);

                NotificationsViewModel ss = new NotificationsViewModel();
                ss.NotificationEndToEnd();

                CancelMooringWinch();
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

        public static Nullable<DateTime> _AssignedDate = null;
        public Nullable<DateTime> AssignedDate
        {
            get
            {
                if (_AssignedDate == null)
                {
                    _AssignedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                RopeEndToEnd.AssignedDate = (DateTime)_AssignedDate;
                return _AssignedDate;
            }
            set
            {
                _AssignedDate = value;
                RaisePropertyChanged("AssignedDate");
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
                        RopeEndToEnd.WinchId = data.Id;
                        StaticHelper.WinchId = data.Id;
                        //CrossShifting.AssignedLocation = data.Location;


                    }

                    OnPropertyChanged(new PropertyChangedEventArgs("RopeEndToEnd"));
                }
                return _sropeass;
            }
            set
            {
                _sropeass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SRopeAss"));
            }
        }
        private void UpdateRopeendtoend(RopeEndtoEnd2Class moorwinch)
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



                var local = sc.Set<RopeEndtoEndClass>()
                 .Local
                 .FirstOrDefault(f => f.Id == moorwinch.Id);
                if (local != null)
                {
                    sc.Entry(local).State = EntityState.Detached;
                }

                var UpdatedRopedetails = new RopeEndtoEnd2Class()
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
                    


                ModifiedDate = DateTime.Now,
                    IsActive = true,
                    //RopeConstruction = moorwinch.RopeConstruction,
                    //RopeTagging = moorwinch.RopeTagging,
                    //ReceivedDate = moorwinch.ReceivedDate,
                    //CertificateNumber = moorwinch.CertificateNumber,
                    //ManufacturerName = moorwinch.ManufacturerName,
                    CreatedBy = moorwinch.CreatedBy
                };

                sc.Entry(UpdatedRopedetails).State = EntityState.Modified;
                sc.SaveChanges();


                //Update into User's Table
                //var user = sc.CrewDetails.Where(x => x.did.Equals(UpdatedRopedetails.Id)).ToList();
                //var depat = user.Where(x => x.did.Equals(UpdatedRopedetails.Id)).FirstOrDefault().department;
                //user.ForEach(a =>
                //{
                //    a.department = UpdatedRopedetails.AssignedNumber;
                //});

                //sc.SaveChanges();

                ////Update into WorkHours's Table
                //var some = sc.WorkHourss.Where(x => x.Department.Equals(depat.Trim())).ToList();
                //some.ForEach(a =>
                //{
                //    a.Department = UpdatedLocation.AssignedNumber;
                //});

                //sc.SaveChanges();
                StaticHelper.Editing = false;
                MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                CancelMooringWinch();

                // CancelMooringWinch();

                //    }

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


        private void EditMooringWinch(RopeEndtoEnd2Class moorwinch)
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
            var lostdata = new ObservableCollection<RopeEndtoEnd2Class>(sc.RopeEndtoEnd2.ToList());
            RopeEndtoEndViewModel cc = new RopeEndtoEndViewModel(lostdata);

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


    }
}
