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
   public class AddRopeDisposalViewModel :ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddRopeDisposalViewModel(RopeDisposalClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<RopeDisposalClass>(UpdateRopeDisposal);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            EditRopeDisposal(edeps);

            var findid = sc.RopeDisposals.Where(x => x.Id == edeps.Id).FirstOrDefault();
            DisposalDate = findid.DisposalDate;

             GetRopeType();
            Ropetypecombo abc = new Ropetypecombo()
            {
                Id = edeps.RopeId,  
                CertificateNo=edeps.CertificateNo
            };
            SRopeType = abc;
        }
        public AddRopeDisposalViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<RopeDisposalClass>(SaveRopeDisposal);
            cancelCommand = new RelayCommand(CancelMooringWinch);
             GetRopeType();
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
            Ropetypecombo rop;
            //SqlDataAdapter adp = new SqlDataAdapter("GetActiveAssignRopeType", sc.con);
            SqlDataAdapter adp = new SqlDataAdapter("GetAllRope", sc.con);
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
           // return AddRopeType;
        }

        public void resetform()
        {
            try
            {


                AddRopeDisposal = new RopeDisposalClass();
                RaisePropertyChanged("AddRopeDisposal");



                DisposalDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("DisposalDate");


              
                SRopeType = null; RaisePropertyChanged("SRopeType");
               // Smoorop = null; RaisePropertyChanged("Smoorop");
                //SManuFName = null; RaisePropertyChanged("SManuFName");
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        #endregion
        private string mooringWinchMessage;
        public string MooringWinchMessage
        {
            get { return mooringWinchMessage; }
            set { mooringWinchMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("MooringWinchMessage")); }
        }

        public static RopeDisposalClass _AddRopeDisposal = new RopeDisposalClass();
        public   RopeDisposalClass AddRopeDisposal
        {
            get
            {
                MooringWinchMessage = string.Empty;
                RaisePropertyChanged("MooringWinchMessage");
                return _AddRopeDisposal;
            }
            set
            {
                _AddRopeDisposal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddRopeDisposal"));
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
                AddRopeDisposal.DisposalDate = (DateTime)_DisposalDate;
                return _DisposalDate;
            }
            set
            {
                _DisposalDate = value;
                RaisePropertyChanged("DisposalDate");
            }
        }
        private void SaveRopeDisposal(RopeDisposalClass rpdisposal)
        {
            try
            {
                rpdisposal.RopeId = rpdisposal.RopeId != 0 ? rpdisposal.RopeId : rpdisposal.RopeId;
                if (rpdisposal.RopeId != 0)
                {
                    var findassno = sc.RopeDisposals.Where(x => x.RopeId == rpdisposal.RopeId).FirstOrDefault();

                    if (findassno == null)
                    {
                        //rpdisposal.RopeId = textinfo.ToTitleCase(rpdisposal.AssignedNumber.ToLower());
                        if (rpdisposal.DisposalPortName == null || rpdisposal.DisposalPortName =="")
                        {
                           // MainViewModelWorkHours.CommonValue = true;
                            MessageBox.Show("Please enter disposal port name ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                          
                        }
                        if (rpdisposal.ReceptionFacilityName == null || rpdisposal.ReceptionFacilityName == "")
                        {
                            MessageBox.Show("Please enter reception facility name ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        rpdisposal.DisposalPortName = rpdisposal.DisposalPortName;
                        rpdisposal.ReceptionFacilityName = rpdisposal.ReceptionFacilityName;

                        rpdisposal.DisposalDate = rpdisposal.DisposalDate;
                        rpdisposal.CreatedDate = DateTime.Now;
                        rpdisposal.CreatedBy = "Admin";
                        rpdisposal.IsActive = true;
                        rpdisposal.RopeTail = 0;

                        sc.RopeDisposals.Add(rpdisposal);
                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record saved successfully ", "Add Rope Disposal", MessageBoxButton.OK, MessageBoxImage.Information);

                        CancelMooringWinch();
                        AddRopeDisposal = new RopeDisposalClass();
                        RaisePropertyChanged("AddRopeDisposal");
                      


                    }
                    else
                    {
                        MessageBox.Show("RopeType already exist ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public static Ropetypecombo sropetype;// = new Ropetypecombo();
        public Ropetypecombo SRopeType
        {
            get
            {

                if (sropetype != null)
                {
                    //var data = sc.AssignRopetoWinch.Where(x => x.RopeId == sropetype.Id && x.IsActive==true).FirstOrDefault();
                    //if (data != null)
                    //{
                        var data1 = sc.MooringWinchRope.Where(x => x.Id == sropetype.Id && x.DeleteStatus == false).FirstOrDefault();
                        AddRopeDisposal.RopeId = data1.Id;
                        //CrossShifting.OutboadEndinUse = data.Outboard;


                    //}
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
        private void UpdateRopeDisposal(RopeDisposalClass rpdisposal)
        {
            try
            {
                rpdisposal.RopeId = rpdisposal.RopeId != 0 ? rpdisposal.RopeId : rpdisposal.RopeId;
                if (rpdisposal.RopeId !=0)
                {

                    var findrank = sc.MooringWinch.Where(x => x.Id == rpdisposal.Id && x.IsActive == true).FirstOrDefault();

                    if (findrank != null)
                    {
                        //rpdisposal.RopeId = textinfo.ToTitleCase(rpdisposal.RopeId);



                        var local = sc.Set<RopeDisposalClass>()
                         .Local
                         .FirstOrDefault(f => f.Id == rpdisposal.Id);
                        if (local != null)
                        {
                            sc.Entry(local).State = EntityState.Detached;
                        }

                        var UpdatedLocation = new RopeDisposalClass()
                        {

                            Id = rpdisposal.Id,
                            RopeId = rpdisposal.RopeId,
                            DisposalPortName = rpdisposal.DisposalPortName,
                            DisposalDate = rpdisposal.DisposalDate,
                            CreatedDate = DateTime.Now,
                            CreatedBy = "Admin",
                            IsActive = true,
                            RopeTail = 0,
                            ReceptionFacilityName = rpdisposal.ReceptionFacilityName

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

                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record updated successfully", "Update RopeDisposal", MessageBoxButton.OK, MessageBoxImage.Information);


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


        private void EditRopeDisposal(RopeDisposalClass rpds)
        {
            try
            {

                var findrank = sc.RopeDisposals.Where(x => x.Id == rpds.Id).FirstOrDefault();
                AddRopeDisposal.DisposalPortName = findrank.DisposalPortName;
                AddRopeDisposal.ReceptionFacilityName = findrank.ReceptionFacilityName;
                AddRopeDisposal.DisposalDate = findrank.DisposalDate;
                AddRopeDisposal.Id = findrank.Id;
                AddRopeDisposal.RopeId = findrank.RopeId;
                OnPropertyChanged(new PropertyChangedEventArgs("AddRopeDisposal"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }

        private void CancelMooringWinch()
        {
            var lostdata = new ObservableCollection<RopeDisposalClass>(sc.RopeDisposals.ToList());
            RopeDisposalListViewModel cc = new RopeDisposalListViewModel(lostdata);

            ChildWindowManager.Instance.CloseChildWindow();
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
      


    }
}
