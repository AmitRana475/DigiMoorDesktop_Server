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
    public class AddChafeGuardViewModel: ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddChafeGuardViewModel(ChafeGuardClass edeps)
        {
            StaticHelper.Editing = true;


           // IsEnabledCheck = false;
            IsEnabledCheck = true;
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<ChafeGuardClass>(UpdateChafeGuard);
            AddChafeGuard = new ChafeGuardClass()
            {
                Id = edeps.Id,
              CertificateNumber = edeps.CertificateNumber,
                ReceivedDate = edeps.ReceivedDate,
                ManufacturerName = edeps.ManufacturerName,
                Remarks = edeps.Remarks, 
                UniqueID = edeps.UniqueID,
            };

            //saveCommand = new RelayCommand<ChafeGuardClass>(UpdateChafeGuard);
            cancelCommand = new RelayCommand(CancelChafeGuard);
            //EditMooringWinch(edeps);

            var tesst = Convert.ToDateTime(edeps.InstalledDate);
            if (tesst.ToString() == "01/01/0001 00:00:00")
            {
                InstalledDate = DateTime.Now.Date;
            }
            else
            {
                ReceivedDate = edeps.ReceivedDate;
                InstalledDate = edeps.InstalledDate;
            }
            if(edeps.InstalledDate1=="Not Assigned")
            {
                InstalledDate = DateTime.Now;
            }

        }
        public AddChafeGuardViewModel()
        {
            StaticHelper.Editing = true;

            IsEnabledCheck = true;
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<ChafeGuardClass>(SaveChafeGuard);
            cancelCommand = new RelayCommand(CancelChafeGuard);

            resetform();
        }

        public void resetform()
        {
            try
            {
                IsEnabledCheck = true;
                AddChafeGuard = new ChafeGuardClass();
                RaisePropertyChanged("AddChafeGuard");

                ReceivedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("ReceivedDate");
                InstalledDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("InstalledDate");


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

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
        private void UpdateChafeGuard(ChafeGuardClass Cguard)
        {
            try
            {
                var local = sc.Set<ChafeGuardClass>().Local.FirstOrDefault(f => f.Id == Cguard.Id);
                if (local != null)
                {
                    sc.Entry(local).State = EntityState.Detached;
                }


                //var duplicatecheck = sc.ChafeGuard.Where(x => x.UniqueID == Cguard.UniqueID).Count();
                //if (duplicatecheck == 0)
                //{
                    if (Cguard.ManufacturerName == null)
                    {
                        MessageBox.Show("Please Enter Manufacturer Name", "Add ChafeGuard", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (Cguard.IsRopeInstalled == null)
                    {
                        MessageBox.Show("Please Choose atleast 1 Rope instsalled.", "Add ChafeGuard", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (Cguard.CertificateNumber == null || Cguard.CertificateNumber == "")
                    {
                        MessageBox.Show("Please Enter CertificateNumber", "Add ChafeGuard", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (Cguard.UniqueID == null || Cguard.UniqueID == "")
                    {
                        MessageBox.Show("Please Enter Unique Identification No", "Add ChafeGuard", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    // Cguard.InstalledDate = Cguard.InstalledDate;


                    if (Cguard.IsRopeInstalled == "No")
                    {
                        Cguard.InstalledDate = null;
                    }
                    else
                    {


                        SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType = 7", sc.con);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //decimal rating1 = Convert.ToDecimal(dt.Rows[0]["MaximumMonthsAllowed"]);
                            //int rat = Convert.ToInt32(rating1);


                            decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                            perchk = perchk * 100;
                            int near = Convert.ToInt32(perchk);
                            DateTime inspectduedate = Convert.ToDateTime(Cguard.InstalledDate).AddDays(near);

                            DateTime crntdt = DateTime.Now;
                            if (inspectduedate <= crntdt)
                            {
                                inspectduedate = DateTime.Now;
                            }

                            Cguard.InspectionDueDate = inspectduedate;
                            //DateTime inspectduedate = Convert.ToDateTime(svchainst.DateInstalled).AddMonths(rat);

                            //svchainst.InspectionDueDate = inspectduedate;
                        }


                        var UpdatedRopedetails = new ChafeGuardClass()
                        {
                            Id = Cguard.Id,
                            ManufacturerName = Cguard.ManufacturerName,
                            ReceivedDate = Cguard.ReceivedDate,
                            InstalledDate = Cguard.InstalledDate,
                            Remarks = Cguard.Remarks,
                            InspectionDueDate = Cguard.InspectionDueDate,
                            //OutofServiceDate=Cguard.OutofServiceDate,
                            CreatedDate = Cguard.CreatedDate,
                            IsActive = true,
                            DeleteStatus = false,
                            CertificateNumber = Cguard.CertificateNumber,
                            UniqueID = Cguard.UniqueID
                        };

                        sc.Entry(UpdatedRopedetails).State = EntityState.Modified;
                        sc.SaveChanges();

                        StaticHelper.Editing = false;
                        MessageBox.Show("Record updated successfully", "Update ChafeGuard", MessageBoxButton.OK, MessageBoxImage.Information);


                        var lostdata = new ObservableCollection<JoiningShackleClass>(sc.JoiningShackles.Where(x => x.DeleteStatus == false).ToList());
                        LooseEquipmentListViewModel cc = new LooseEquipmentListViewModel(lostdata);
                        ChildWindowManager.Instance.CloseChildWindow();
                    }
                //}
                //else
                //{
                //    MessageBox.Show("UniqueID already exist !", "Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Information);
                //    return;
                //}

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

        public static ChafeGuardClass _AddChafeGuard = new ChafeGuardClass();
        public ChafeGuardClass AddChafeGuard
        {
            get
            {
                //MooringWinchMessage = string.Empty;
                //RaisePropertyChanged("MooringWinchMessage");
                return _AddChafeGuard;
            }
            set
            {
                _AddChafeGuard = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddChafeGuard"));
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


        private static Nullable<DateTime> _ReceivedDate = null;
        public Nullable<DateTime> ReceivedDate
        {
            get
            {
                if (_ReceivedDate == null)
                {
                    AddChafeGuard.ReceivedDate = null;
                    _ReceivedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                else
                {
                    AddChafeGuard.ReceivedDate = (DateTime)_ReceivedDate;
                }
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
                    AddChafeGuard.InstalledDate = null;
                     _InstallDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                else
                {
                    AddChafeGuard.InstalledDate = (DateTime)_InstallDate;
                }
                return _InstallDate;
            }
            set
            {
                _InstallDate = value;
                RaisePropertyChanged("InstalledDate");
            }
        }
        private void SaveChafeGuard(ChafeGuardClass chfguard)
        {
            try
            {
                chfguard.ManufacturerName = chfguard.ManufacturerName != null ? chfguard.ManufacturerName.Trim() : chfguard.ManufacturerName;
                if (!string.IsNullOrEmpty(chfguard.ManufacturerName))
                {
                    //var findassno = sc.ChafeGuard.Where(x => x.ManufacturerName == chfguard.ManufacturerName).FirstOrDefault();

                    //if (findassno == null)
                    //{
                        chfguard.ManufacturerName = chfguard.ManufacturerName;
                        if (chfguard.ManufacturerName == null)
                        {
                            MessageBox.Show("Please Enter Manufacturer Name", "Add ChafeGuard", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }                  
                    if (chfguard.IsRopeInstalled == null)
                        {
                            MessageBox.Show("Please Choose atleast 1 Rope instsalled.", "Add ChafeGuard", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        if (chfguard.CertificateNumber == null || chfguard.CertificateNumber == "")
                        {
                            MessageBox.Show("Please Enter CertificateNumber", "Add ChafeGuard", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    if (chfguard.UniqueID == null || chfguard.UniqueID == "")
                    {
                        MessageBox.Show("Please Enter Unique Identification No", "Add ChafeGuard", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var duplicatecheck = sc.ChafeGuard.Where(x => x.UniqueID == chfguard.UniqueID).Count();
                    if (duplicatecheck == 0)
                    {
                        chfguard.ManufacturerName = chfguard.ManufacturerName;

                        chfguard.CreatedDate = DateTime.Now;

                        chfguard.CertificateNumber = chfguard.CertificateNumber;
                        chfguard.UniqueID = chfguard.UniqueID;

                        chfguard.InstalledDate = chfguard.InstalledDate;
                        chfguard.ReceivedDate = chfguard.ReceivedDate;

                        chfguard.DeleteStatus = false;
                        if (chfguard.IsRopeInstalled == "No")
                        {
                            chfguard.InstalledDate = null;
                        }
                        chfguard.IsActive = true;



                        try
                        {
                            if (chfguard.InstalledDate != null)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType=7 ", sc.con);
                                DataTable dt = new DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    //decimal rating1 = Convert.ToDecimal(dt.Rows[0]["MaximumMonthsAllowed"]);
                                    //int rat = Convert.ToInt32(rating1);


                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    DateTime inspectduedate = Convert.ToDateTime(chfguard.InstalledDate).AddDays(near);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }

                                    chfguard.InspectionDueDate = inspectduedate;
                                    //DateTime inspectduedate = Convert.ToDateTime(svchainst.DateInstalled).AddMonths(rat);

                                    //svchainst.InspectionDueDate = inspectduedate;
                                }
                            }
                        }
                        catch { }


                        sc.ChafeGuard.Add(chfguard);
                        sc.SaveChanges();


                        StaticHelper.Editing = false;
                        MessageBox.Show("Record saved successfully ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);

                        CancelChafeGuard();

                        AddChafeGuard = new ChafeGuardClass();
                        RaisePropertyChanged("AddMooringWinch");
                    }
                    else
                    {
                        MessageBox.Show("UniqueID already exist !", "Chafe Guard", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }


                    //}
                    //else
                    //{
                    //    MessageBox.Show("ChafeGuard already exist ", "Add ChafeGuard", MessageBoxButton.OK, MessageBoxImage.Information);
                    //}
                }
                else
                {
                    MessageBox.Show("Please Enter ChafeGuard", "Add ChafeGuard", MessageBoxButton.OK, MessageBoxImage.Warning);

                    MooringWinchMessage = "Please Enter the ChafeGuard";
                    RaisePropertyChanged("MooringWinchMessage");
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        //private void UpdateChafeGuard(ChafeGuardClass moorwinch)
        //{
        //    try
        //    {
        //        moorwinch.AssignedNumber = moorwinch.AssignedNumber != null ? moorwinch.AssignedNumber.Trim() : moorwinch.AssignedNumber;
        //        if (!string.IsNullOrEmpty(moorwinch.AssignedNumber))
        //        {

        //            var findrank = sc.MooringWinch.Where(x => x.Id == moorwinch.Id).FirstOrDefault();

        //            if (findrank != null)
        //            {
        //                moorwinch.AssignedNumber = textinfo.ToTitleCase(moorwinch.AssignedNumber.ToLower());



        //                var local = sc.Set<MooringWinchClass>()
        //                 .Local
        //                 .FirstOrDefault(f => f.Id == moorwinch.Id);
        //                if (local != null)
        //                {
        //                    sc.Entry(local).State = EntityState.Detached;
        //                }

        //                if (moorwinch.Location == null || moorwinch.Location == "")
        //                {
        //                    MessageBox.Show("Please Enter Assigned Location", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                    return;
        //                }
        //                if (moorwinch.MBL == null || moorwinch.MBL == 0)
        //                {
        //                    MessageBox.Show("Please Enter MBL", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                    return;
        //                }
        //                var UpdatedLocation = new MooringWinchClass()
        //                {

        //                    Id = moorwinch.Id,
        //                    AssignedNumber = moorwinch.AssignedNumber,
        //                    Location = moorwinch.Location,
        //                    MBL = moorwinch.MBL,
        //                    CreatedDate = DateTime.Now,
        //                    CreatedBy = "Admin",
        //                    ModifiedDate = DateTime.Now,
        //                    IsActive = true
        //                };

        //                sc.Entry(UpdatedLocation).State = EntityState.Modified;
        //                sc.SaveChanges();


        //                //Update into User's Table
        //                //var user = sc.CrewDetails.Where(x => x.did.Equals(UpdatedLocation.Id)).ToList();
        //                //var depat = user.Where(x => x.did.Equals(UpdatedLocation.Id)).FirstOrDefault().department;
        //                //user.ForEach(a =>
        //                //{
        //                //    a.department = UpdatedLocation.AssignedNumber;
        //                //});

        //                //sc.SaveChanges();

        //                //Update into WorkHours's Table
        //                //var some = sc.WorkHourss.Where(x => x.Department.Equals(depat.Trim())).ToList();
        //                //some.ForEach(a =>
        //                //{
        //                //    a.Department = UpdatedLocation.AssignedNumber;
        //                //});

        //                //sc.SaveChanges();
        //                //StaticHelper.Editing = false;
        //                MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);


        //                CancelMooringWinch();

        //            }

        //        }
        //        else
        //        {
        //            MessageBox.Show("Please Enter Ship Assigned Number", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Warning);

        //            MooringWinchMessage = "Please Enter the MooringWinch Name";
        //            RaisePropertyChanged("MooringWinchMessage");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //    }
        //}


        //private void EditMooringWinch(MooringWinchClass moorwinch)
        //{
        //    try
        //    {

        //        var findrank = sc.MooringWinch.Where(x => x.Id == moorwinch.Id).FirstOrDefault();
        //        AddMooringWinch.AssignedNumber = findrank.AssignedNumber;
        //        AddMooringWinch.Location = findrank.Location;
        //        AddMooringWinch.MBL = findrank.MBL;
        //        AddMooringWinch.Id = findrank.Id;
        //        OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinch"));
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //    }


        //}

        private void CancelChafeGuard()
        {
            //var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.ToList().Where(x => x.DeleteStatus == false));
            //MooringWinchRopeViewModel cc = new MooringWinchRopeViewModel(lostdata);
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

