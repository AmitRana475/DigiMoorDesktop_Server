using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class AddMooringWinchDetail : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddMooringWinchDetail(MooringWinchClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<MooringWinchClass>(UpdateMooringWinch);
            cancelCommand = new RelayCommand(CancelMooringWinch);
            EditMooringWinch(edeps);
        }
        public AddMooringWinchDetail()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<MooringWinchClass>(SaveMooringWinch);
            cancelCommand = new RelayCommand(CancelMooringWinch);
        }


        private string mooringWinchMessage;
        public string MooringWinchMessage
        {
            get { return mooringWinchMessage; }
            set { mooringWinchMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("MooringWinchMessage")); }
        }

        private MooringWinchClass _AddMooringWinch = new MooringWinchClass();
        public MooringWinchClass AddMooringWinch
        {
            get
            {
                MooringWinchMessage = string.Empty;
                RaisePropertyChanged("MooringWinchMessage");
                return _AddMooringWinch;
            }
            set
            {
                _AddMooringWinch = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinch"));
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

        private void SaveMooringWinch(MooringWinchClass moorwinch)
        {
            try
            {
                moorwinch.AssignedNumber = moorwinch.AssignedNumber != null ? moorwinch.AssignedNumber.Trim() : moorwinch.AssignedNumber;
                if (!string.IsNullOrEmpty(moorwinch.AssignedNumber))
                {
                    var findassno = sc.MooringWinch.Where(x => x.AssignedNumber == moorwinch.AssignedNumber).FirstOrDefault();

                    if (findassno == null)
                    {
                        moorwinch.AssignedNumber = textinfo.ToTitleCase(moorwinch.AssignedNumber.ToLower());
                        moorwinch.Location= textinfo.ToTitleCase(moorwinch.Location.ToLower());
                        moorwinch.CreatedDate = DateTime.Now;
                        moorwinch.CreatedBy = "Admin";
                        moorwinch.IsActive = true;

                        sc.MooringWinch.Add(moorwinch);
                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record saved successfully ", "Add MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                        AddMooringWinch = new MooringWinchClass();
                        RaisePropertyChanged("AddMooringWinch");


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

        private void UpdateMooringWinch(MooringWinchClass moorwinch)
        {
            try
            {
                moorwinch.AssignedNumber = moorwinch.AssignedNumber != null ? moorwinch.AssignedNumber.Trim() : moorwinch.AssignedNumber;
                if (!string.IsNullOrEmpty(moorwinch.AssignedNumber))
                {

                    var findrank = sc.MooringWinch.Where(x => x.Id == moorwinch.Id).FirstOrDefault();

                    if (findrank != null)
                    {
                        moorwinch.AssignedNumber = textinfo.ToTitleCase(moorwinch.AssignedNumber.ToLower());



                        var local = sc.Set<MooringWinchClass>()
                         .Local
                         .FirstOrDefault(f => f.Id == moorwinch.Id);
                        if (local != null)
                        {
                            sc.Entry(local).State = EntityState.Detached;
                        }

                        var UpdatedLocation = new MooringWinchClass()
                        {

                            Id = moorwinch.Id,
                            AssignedNumber = moorwinch.AssignedNumber
                        };

                        sc.Entry(UpdatedLocation).State = EntityState.Modified;
                        sc.SaveChanges();


                        //Update into User's Table
                        var user = sc.CrewDetails.Where(x => x.did.Equals(UpdatedLocation.Id)).ToList();
                        var depat = user.Where(x => x.did.Equals(UpdatedLocation.Id)).FirstOrDefault().department;
                        user.ForEach(a =>
                        {
                            a.department = UpdatedLocation.AssignedNumber;
                        });

                        sc.SaveChanges();

                        //Update into WorkHours's Table
                        var some = sc.WorkHourss.Where(x => x.Department.Equals(depat.Trim())).ToList();
                        some.ForEach(a =>
                        {
                            a.Department = UpdatedLocation.AssignedNumber;
                        });

                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);


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


        private void EditMooringWinch(MooringWinchClass moorwinch)
        {
            try
            {

                var findrank = sc.MooringWinch.Where(x => x.Id == moorwinch.Id).FirstOrDefault();
                AddMooringWinch.AssignedNumber = findrank.AssignedNumber;
                AddMooringWinch.Id = findrank.Id;
                OnPropertyChanged(new PropertyChangedEventArgs("AddMooringWinch"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }

        private void CancelMooringWinch()
        {
            //var lostdata = new ObservableCollection<DepartmentClass>(sc.Departments.ToList());
            //DepartmentViewModel cc = new DepartmentViewModel(lostdata);

            new MooringWinchViewModel();
            //new CrewDetailViewModel();
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
