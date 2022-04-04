using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.WorkHoursViewModel
{
 
    public class AddCrossShiftingWinchViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
      
        public AddCrossShiftingWinchViewModel(CrossShiftingWinchClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<CrossShiftingWinchClass>(UpdateRopeendtoend);
            cancelCommand = new RelayCommand(CancelMooringWinch);

            // MooringWinchRopeClass data = sc.MooringWinchRope.Where(x => x.Id == edeps.Id).FirstOrDefault();


            //SRopeType = edeps.RopeType;
            //SRopeConst = edeps.RopeConstruction;
            //SRopeDiameter = edeps.DiaMeter;
            //SRopeReasonoutofs = edeps.ReasonOutofService;

            //ReceivedDate = edeps.ReceivedDate;

            CrossShifting = new CrossShiftingWinchClass()
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
        public AddCrossShiftingWinchViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<CrossShiftingWinchClass>(SaveCrossShifting);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            ropetype = GetRopeType();
          
            assignrope = GetAssRope();

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





        private ObservableCollection<Ropetypecombo> GetRopeType()
        {
            ObservableCollection<Ropetypecombo> AddRopeType = new ObservableCollection<Ropetypecombo>();
            var data = sc.MooringWinchRope.Where(x=> x.DeleteStatus==false).Select(x => new { x.Id, x.CertificateNumber }).ToList();


            Ropetypecombo rop;
            foreach (var item in data)
            {

                rop = new Ropetypecombo();
                rop.Id = item.Id;
                rop.CertificateNo = item.CertificateNumber;
                AddRopeType.Add(rop);
            }

            return AddRopeType;
        }

        #endregion







        private CrossShiftingWinchClass _crossshiftingClass = new CrossShiftingWinchClass();

        public CrossShiftingWinchClass CrossShifting
        {
            get
            {

                return _crossshiftingClass;
            }
            set
            {
                _crossshiftingClass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CrossShifting"));
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
                    var data = sc.AssignRopetoWinch.Where(x => x.RopeId == sropetype.Id).FirstOrDefault();
                    if (data != null)
                    {
                        var data1 = sc.MooringWinchRope.Where(x => x.Id == sropetype.Id && x.DeleteStatus == false).FirstOrDefault();
                        CrossShifting.RopeId = data.RopeId;
                        //CrossShifting.OutboadEndinUse = data.Outboard;


                    }
                    //RopeEndToEnd.OutboardEndinUse = data1.Outboard;
                    OnPropertyChanged(new PropertyChangedEventArgs("CrossShifting"));
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
        public static Winchcombo _sropeass ;
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
                        CrossShifting.WinchId = data.Id;
                        //CrossShifting.AssignedLocation = data.Location;


                    }
                   
                    OnPropertyChanged(new PropertyChangedEventArgs("CrossShifting"));
                }
                return _sropeass;
            }
            set
            {
                _sropeass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SRopeAss"));
            }
        }

        private ObservableCollection<Winchcombo> GetAssRope()
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



        public static Nullable<DateTime> _DateofShifting = null;
        public Nullable<DateTime> DateofShifting
        {
            get
            {
                if (_DateofShifting == null)
                {
                    _DateofShifting = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                _crossshiftingClass.DateofShifting = (DateTime)_DateofShifting;
                return _DateofShifting;
            }
            set
            {
                _DateofShifting = value;
                RaisePropertyChanged("DateofShifting");
            }
        }


        #endregion
        private void SaveCrossShifting(CrossShiftingWinchClass crsshifting)
        {
            try
            {

                crsshifting.RopeId = crsshifting.RopeId;
                crsshifting.WinchId = crsshifting.WinchId;
                crsshifting.DateofShifting = CrossShifting.DateofShifting;

                if (crsshifting.OutboardEndinUse == false)
                {
                   // crsshifting.OutboardEndinUse = true;
                    var result = sc.AssignRopetoWinch.SingleOrDefault(b => b.RopeId == crsshifting.RopeId);
                    if (result != null)
                    {
                        result.Outboard = false;
                        result.WinchId = crsshifting.WinchId;
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
                    //crsshifting.OutboardEndinUse = false;

                    var result = sc.AssignRopetoWinch.SingleOrDefault(b => b.RopeId == crsshifting.RopeId);
                    if (result != null)
                    {
                        result.Outboard = true;
                        result.WinchId = crsshifting.WinchId;
                        result.ModifiedBy = "Admin";
                        result.ModifiedDate = DateTime.Now;
                        sc.SaveChanges();
                    }
                }


                crsshifting.CreatedDate = DateTime.Now;
                crsshifting.CreatedBy = "Admin";
                crsshifting.IsActive = true;

                sc.CrossShiftingWinches.Add(crsshifting);
                sc.SaveChanges();
                MessageBox.Show("Record saved successfully ", "Rope EndtoEnd", MessageBoxButton.OK, MessageBoxImage.Information);

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

        private void UpdateRopeendtoend(CrossShiftingWinchClass moorwinch)
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



                var local = sc.Set<CrossShiftingWinchClass>()
                 .Local
                 .FirstOrDefault(f => f.Id == moorwinch.Id);
                if (local != null)
                {
                    sc.Entry(local).State = EntityState.Detached;
                }

                var UpdatedRopedetails = new CrossShiftingWinchClass()
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
                CancelMooringWinch();


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


        private void EditMooringWinch(CrossShiftingWinchClass moorwinch)
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
            var lostdata = new ObservableCollection<CrossShiftingWinchClass>(sc.CrossShiftingWinches.ToList());
            CrossShiftingWinchViewModel cc = new CrossShiftingWinchViewModel(lostdata);

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
