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


namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class AddDepartmentViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddDepartmentViewModel(DepartmentClass edeps)
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<DepartmentClass>(UpdateDepartment);
            cancelCommand = new RelayCommand(CancelDepartment);
            EditDepartment(edeps);
        }
        public AddDepartmentViewModel()
        {
            StaticHelper.Editing = true;

            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<DepartmentClass>(SaveDepartment);
            cancelCommand = new RelayCommand(CancelDepartment);
        }


        private string departmentMessage;
        public string DepartmentMessage
        {
            get { return departmentMessage; }
            set { departmentMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("DepartmentMessage")); }
        }

        private DepartmentClass _AddDepartment = new DepartmentClass();
        public DepartmentClass AddDepartment
        {
            get
            {
                DepartmentMessage = string.Empty;
                RaisePropertyChanged("DepartmentMessage");
                return _AddDepartment;
            }
            set
            {
                _AddDepartment = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddDepartment"));
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

        private void SaveDepartment(DepartmentClass ranks)
        {
            try
            {
                ranks.DeptName = ranks.DeptName != null ? ranks.DeptName.Trim() : ranks.DeptName;
                if (!string.IsNullOrEmpty(ranks.DeptName))
                {
                    var findrank = sc.Departments.Where(x => x.DeptName == ranks.DeptName).FirstOrDefault();

                    if (findrank == null)
                    {
                        ranks.DeptName = textinfo.ToTitleCase(ranks.DeptName.ToLower());

                        sc.Departments.Add(ranks);
                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record saved successfully ", "Add Department", MessageBoxButton.OK, MessageBoxImage.Information);
                        AddDepartment = new DepartmentClass();
                        RaisePropertyChanged("AddDepartment");


                    }
                    else
                    {
                        MessageBox.Show("Department already exist ", "Add Department", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {

                    DepartmentMessage = "Please Enter the Department Name";
                    RaisePropertyChanged("DepartmentMessage");
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void UpdateDepartment(DepartmentClass deps)
        {
            try
            {
                deps.DeptName = deps.DeptName != null ? deps.DeptName.Trim() : deps.DeptName;
                if (!string.IsNullOrEmpty(deps.DeptName))
                {

                    var findrank = sc.Departments.Where(x => x.did == deps.did).FirstOrDefault();

                    if (findrank != null)
                    {
                        deps.DeptName = textinfo.ToTitleCase(deps.DeptName.ToLower());



                        var local = sc.Set<DepartmentClass>()
                         .Local
                         .FirstOrDefault(f => f.did == deps.did);
                        if (local != null)
                        {
                            sc.Entry(local).State = EntityState.Detached;
                        }

                        var UpdatedLocation = new DepartmentClass()
                        {

                            did = deps.did,
                            DeptName = deps.DeptName
                        };

                        sc.Entry(UpdatedLocation).State = EntityState.Modified;
                        sc.SaveChanges();


                        //Update into User's Table
                        var user = sc.CrewDetails.Where(x => x.did.Equals(UpdatedLocation.did)).ToList();
                        var depat = user.Where(x => x.did.Equals(UpdatedLocation.did)).FirstOrDefault().department;
                        user.ForEach(a =>
                        {
                            a.department = UpdatedLocation.DeptName;
                        });

                        sc.SaveChanges();

                        //Update into WorkHours's Table
                        var some = sc.WorkHourss.Where(x => x.Department.Equals(depat.Trim())).ToList();
                        some.ForEach(a =>
                        {
                            a.Department = UpdatedLocation.DeptName;
                        });

                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record updated successfully", "Update Department", MessageBoxButton.OK, MessageBoxImage.Information);


                        CancelDepartment();

                    }

                }
                else
                {

                    DepartmentMessage = "Please Enter the Department Name";
                    RaisePropertyChanged("DepartmentMessage");
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void EditDepartment(DepartmentClass deps)
        {
            try
            {

                var findrank = sc.Departments.Where(x => x.did == deps.did).FirstOrDefault();
                AddDepartment.DeptName = findrank.DeptName;
                AddDepartment.did = findrank.did;
                OnPropertyChanged(new PropertyChangedEventArgs("AddDepartment"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }

        private void CancelDepartment()
        {
            //var lostdata = new ObservableCollection<DepartmentClass>(sc.Departments.ToList());
            //DepartmentViewModel cc = new DepartmentViewModel(lostdata);

            new DepartmentViewModel();
            new CrewDetailViewModel();
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
