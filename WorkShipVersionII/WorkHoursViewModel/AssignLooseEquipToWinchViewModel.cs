using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class AssignLooseEquipToWinchViewModel :ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AssignLooseEquipToWinchViewModel(AssignLooseEquipTypeClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<AssignLooseEquipTypeClass>(UpdateLooseE);
            cancelCommand = new RelayCommand(CancelLooseEWinch);

            SelectionChangedCommand = new RelayCommand(SelectionChanged);

            //ropetype = GetRopeType();
            //AssignModuleToWinchClass data = sc.AssignRopetoWinch.Where(x => x.Id == edeps.Id).FirstOrDefault();

            AssignLooseEquipToWinch = new AssignLooseEquipTypeClass()
            {
                Id = edeps.Id,
                AssignWinchId = edeps.AssignWinchId,
                AssignedLocation = edeps.AssignedLocation,
                ModifiedDate = DateTime.Now,
                ModifiedBy = "Admin",
                CreatedDate = edeps.CreatedDate,
                CreatedBy = edeps.CreatedBy
            };

            //sropetype = new Ropetypecombo()
            //{
            //    Id = data.RopeId,
            //    CertificateNo = edeps.CertificateNumber
            //};

            LooseEtypecombo abc = new LooseEtypecombo()
            {
                Id = edeps.LooseETypeId,
                LooseEquipmentType = edeps.Looseequipmenttype
            };
            SRopeType = abc;
            Winchcombo abcd = new Winchcombo()
            {
                Id = edeps.AssignWinchId,
                AssignedNumber = edeps.AssignedNumber
            };
            SRopeAss = abcd;

         


            RaisePropertyChanged("AddMooringWinchRope");
            OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinchRope"));
            
        }
        public AssignLooseEquipToWinchViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<AssignLooseEquipTypeClass>(SaveMooringWinchRope);
            cancelCommand = new RelayCommand(CancelLooseEWinch);
            resetCommand = new RelayCommand(ResetAssignLooseEToWinch);
            ropetype = GetLooseEType();
            assignrope = GetAssRope();

            ResetAssignLooseEToWinch();
        }
        private ICommand resetCommand;
        public ICommand ResetCommand
        {
            get { return resetCommand; }
        }
        public void ResetAssignLooseEToWinch()
        {
            try
            {

                erinfo = 0;
                AssignLooseEquipToWinch = new AssignLooseEquipTypeClass();
                RaisePropertyChanged("AssignLooseEquipToWinch");

             

                //AssignedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("AssignedDate");
               
                SRopeType = null; RaisePropertyChanged("SRopeType");
                SRopeAss = null; RaisePropertyChanged("SRopeAss");
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
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
        private static ObservableCollection<LooseEtypecombo> ropetype = new ObservableCollection<LooseEtypecombo>();
        public ObservableCollection<LooseEtypecombo> RopeType
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





        public ObservableCollection<LooseEtypecombo> GetLooseEType()
        {
            ObservableCollection<LooseEtypecombo> AddLooseEType = new ObservableCollection<LooseEtypecombo>();
            var data = sc.LooseETypes.Where(x=> x.Id !=2).Select(x => new { x.Id, x.LooseEquipmentType }).ToList();


            LooseEtypecombo rop;
            foreach (var item in data)
            {

                rop = new LooseEtypecombo();
                rop.Id = item.Id;
                rop.LooseEquipmentType = item.LooseEquipmentType;
                AddLooseEType.Add(rop);
            }

            return AddLooseEType;
        }

        #endregion

        #region Bind AssignRope


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
                        AssignLooseEquipToWinch.AssignWinchId = data.Id;
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

        #endregion


        private AssignLooseEquipTypeClass _AddAssLooseEWinch = new AssignLooseEquipTypeClass();
        public AssignLooseEquipTypeClass AssignLooseEquipToWinch
        {
            get
            {
                return _AddAssLooseEWinch;
            }
            set
            {
                _AddAssLooseEWinch = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AssignLooseEquipToWinch"));
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

       
        private void refreshmessage1(AssignLooseEquipTypeClass cdc1)
        {
            AssignLooseEquipTypeClass cdc = cdc1;
            CheckErrorMessage.CheckErrorMessages = true;

            AssignLooseEErrorMessages = new AssignLooseEquipErrorMessages();
            RaisePropertyChanged("AssignLooseEErrorMessages");

            AssignLooseEquipErrorMessages m = (AssignLooseEErrorMessages as AssignLooseEquipErrorMessages); //DownCasting.....

            cdc.LooseETypeId = cdc.LooseETypeId != 0 ? cdc.LooseETypeId : 0;
            cdc.AssignWinchId = cdc.AssignWinchId != 0 ? cdc.AssignWinchId : 0;
           

            if (cdc.LooseETypeId == 0)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.LooseETypeMessage = "Please Select LooseEType !";
                RaisePropertyChanged("AssignLooseEErrorMessages");
            }
            if (cdc.AssignWinchId ==0)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.AssignWinchMessage = "Please Select Winch !";
                RaisePropertyChanged("AssignLooseEErrorMessages");
            }
       
        }
        public static class CheckErrorMessage
        {
            public static bool CheckErrorMessages { get; set; }
            public static bool CheckErrorMessages1 { get; set; }
            public static bool CheckErrorMessages2 { get; set; }
            public static bool chkyoungs { get; set; }

        }

        private static AssignLooseEquipErrorMessages _AssignLooseEquipErrorMessages = new AssignLooseEquipErrorMessages();
        public AssignLooseEquipErrorMessages AssignLooseEErrorMessages
        {
            get
            {
                return _AssignLooseEquipErrorMessages;
            }
            set
            {
                _AssignLooseEquipErrorMessages = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AssignLooseEErrorMessages"));
            }
        }
        public class AssignLooseEquipErrorMessages
        {

            public string LooseETypeMessage { get; set; }
            public string AssignWinchMessage { get; set; }
            public string RopeTaggingMessage { get; set; }
            public string ReasonoutofServiceMessage { get; set; }

        }

        #region Bind Properties

        private static LooseEtypecombo sropetype = new LooseEtypecombo();
        public LooseEtypecombo SRopeType
        {
            get
            {
                if (sropetype != null)
                {
                    AssignLooseEquipToWinch.LooseETypeId = sropetype.Id;
                  
                }
                return sropetype;

            }
            set
            {
                sropetype = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SRopeType"));
            }
        }

        

        #endregion
        private void SaveMooringWinchRope(AssignLooseEquipTypeClass asswinchrope)
        {
            try
            {
                //if (asswinchrope.Outboard == true)
                //{
                //    asswinchrope.Outboard = true;
                //}
                //if (asswinchrope.Outboard == false)
                //{
                //    asswinchrope.Outboard = false;
                //}
                refreshmessage1(asswinchrope);

                if (!string.IsNullOrEmpty(Lblmessage))
                    CheckErrorMessage.CheckErrorMessages = false;

                if (CheckErrorMessage.CheckErrorMessages)
                {

                    asswinchrope.LooseETypeId = SRopeType.Id;
                    asswinchrope.AssignWinchId = SRopeAss.Id;

                    asswinchrope.CreatedDate = DateTime.Now;
                    asswinchrope.CreatedBy = "Admin";
                    asswinchrope.IsActive = true;

                    var duplcheck = sc.AssignLooseEtoWinch.Where(x => x.LooseETypeId == SRopeType.Id).FirstOrDefault();
                    if (duplcheck == null)
                    {
                        sc.AssignLooseEtoWinch.Add(asswinchrope);
                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record saved successfully ", "Assign LooseE To Winch", MessageBoxButton.OK, MessageBoxImage.Information);
                        //AssignRopeToWinch = new AssignModuleToWinchClass();
                        //RaisePropertyChanged("AssignRopeToWinch");
                        CancelLooseEWinch();

                    }
                    else
                    {
                        MessageBox.Show("LooseE Type already exist ", "Assign LooseE To Winch", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
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
        //public ICommand RadioBTNCommand { get; private set; }
        //private static AssignModuleToWinchClass _Assignmoduletowinch = new AssignModuleToWinchClass();
        //private void RadioBTNmethod(object parameter)
        //{
        //    var bb = (string)parameter;

        //    if (bb == "Outboard")
        //    {
        //        _Assignmoduletowinch.Outboard = true;
        //        _Assignmoduletowinch.Outboard1 = false;
        //        StaticHelper.Wathckeeping = true;
        //    }
        //    else
        //    {
        //        _Assignmoduletowinch.Outboard = false;
        //        _Assignmoduletowinch.Outboard1 = true;
        //        StaticHelper.Wathckeeping = false;
        //    }

        //    OnPropertyChanged(new PropertyChangedEventArgs("AssignRopeToWinch"));
        //}
        private void UpdateLooseE(AssignLooseEquipTypeClass looseEwinch)
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



                var local = sc.Set<AssignLooseEquipTypeClass>()
                 .Local
                 .FirstOrDefault(f => f.Id == looseEwinch.Id);
                if (local != null)
                {
                    sc.Entry(local).State = EntityState.Detached;
                }
                //if (moorwinch.Outboard == true)
                //{
                //    moorwinch.Outboard = true;
                //}
                //if (moorwinch.Outboard == false)
                //{
                //    moorwinch.Outboard = false;
                //}

                var UpdatedAssRopeWinch = new AssignLooseEquipTypeClass()
                {

                    Id = looseEwinch.Id,

                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    AssignedLocation = looseEwinch.AssignedLocation,
                    CreatedBy = looseEwinch.CreatedBy,
                    LooseETypeId = SRopeType.Id,
                    AssignWinchId = SRopeAss.Id,
                    CreatedDate = looseEwinch.CreatedDate,
                    //Outboard = moorwinch.Outboard
                };
                //moorwinch.RopeId = SRopeType.Id;
                //moorwinch.WinchId = SRopeAss.Id;

                var duplcheck = sc.AssignLooseEtoWinch.Where(x => x.LooseETypeId == SRopeType.Id).FirstOrDefault();
                if (duplcheck == null)
                {
                    sc.Entry(UpdatedAssRopeWinch).State = EntityState.Modified;
                    sc.SaveChanges();

                    StaticHelper.Editing = false;
                    MessageBox.Show("Record updated successfully", "Update LooseE to Winch", MessageBoxButton.OK, MessageBoxImage.Information);

                    CancelLooseEWinch();
                }
                else
                {
                    MessageBox.Show("LooseE Type already exist ", "Assign LooseE To Winch", MessageBoxButton.OK, MessageBoxImage.Information);
                }
             


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


        private void EditLooseE(MooringWinchRopeClass looseEwinch)
        {
            try
            {

                var findrank = sc.MooringWinch.Where(x => x.Id == looseEwinch.Id).FirstOrDefault();
                // AddMooringWinch.AssignedNumber = findrank.AssignedNumber;
                // AddMooringWinch.Id = findrank.Id;
                OnPropertyChanged(new PropertyChangedEventArgs("AddRopeDetail"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }

        private void CancelLooseEWinch()
        {
            var lostdata = new ObservableCollection<AssignLooseEquipTypeClass>(sc.AssignLooseEtoWinch.ToList());
            AssignLooseEquipToWinchDetailsViewModel cc = new AssignLooseEquipToWinchDetailsViewModel(lostdata);

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

    public class LooseEtypecombo
    {
        public int Id { get; set; }
        public string LooseEquipmentType { get; set; }
    }

    //public class Winchcombo
    //{
    //    public int Id { get; set; }
    //    public string AssignedNumber { get; set; }
    //}
}
