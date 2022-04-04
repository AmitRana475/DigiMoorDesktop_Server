using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Data.Entity;
using System;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class AddHODViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        UserAccessClass ob;
        public AddHODViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            listVisible = "Collapsed";
            ob = new UserAccessClass();

            saveCommand = new RelayCommand<UserAccessClass>(SaveMethod);
            resetCommand = new RelayCommand(resetHODDetail);
            goBackCommand = new RelayCommand(resetHODDetail);
            forwordCommand = new RelayCommand<string>(ForwordMethod);
            backCommand = new RelayCommand<string>(BackMethod);

        }
        public AddHODViewModel(UserAccessClass obj)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            listVisible = "Collapsed";
            ob = obj;
            saveCommand = new RelayCommand<UserAccessClass>(UpdateMethod);
            resetCommand = new RelayCommand(resetHODDetail);
            goBackCommand = new RelayCommand(resetHODDetail);
            forwordCommand = new RelayCommand<string>(ForwordMethod);
            backCommand = new RelayCommand<string>(BackMethod);


            var data = sc.UserAccessHOD.Where(x => x.ID == obj.ID && x.UserName == obj.UserName).FirstOrDefault();
            AutoHODName = data.HODName;
            RaisePropertyChanged("AutoHODName");
            addUserAccess = data;
            RaisePropertyChanged("AddUserAccess");
            ChView = data.Certificate;
            RaisePropertyChanged("ChView");
            ChAdd = data.CertificateAdd;
            RaisePropertyChanged("ChAdd");
            ChEdit = data.CertificateEdit;
            RaisePropertyChanged("ChEdit");
            ChDelete = data.CertificateDelete;
            RaisePropertyChanged("ChDelete");

        }



        private void BackMethod(string obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    if (AddUserAccess.Departments != obj)
                    {
                        DepartmentName2.RemoveAt
                       (DepartmentName2.IndexOf(obj));
                    }
                    else
                    {
                        MessageBox.Show("This User already belong to own " + obj + " Department, so the " + obj + " cannot be removed. Try another User", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void ForwordMethod(string obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    var bb = DepartmentName2.Where(x => x.Equals(obj)).FirstOrDefault();
                    if (bb == null)
                        DepartmentName2.Add(obj);
                    else
                        MessageBox.Show("This Department is already selected ", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void resetHODDetail()
        {
            try
            {
                CheckErrorMessage.CheckErrorMessages2 = false;
                AddUserAccess = new UserAccessClass();
                RaisePropertyChanged("AddUserAccess");
                ChView = false;
                RaisePropertyChanged("ChView");
                AutoHODName = string.Empty;
                RaisePropertyChanged("AutoHODName");
                DepartmentName1 = new ObservableCollection<string>();
                RaisePropertyChanged("DepartmentName1");
                DepartmentName2 = new ObservableCollection<string>();
                RaisePropertyChanged("DepartmentName2");
                StaticHelper.Editing = false;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void SaveMethod(UserAccessClass obj)
        {
            try
            {
                CheckErrorMessage.CheckErrorMessages2 = true;
                obj.DepartmentName = string.Join(",", DepartmentName2);
                if (!string.IsNullOrEmpty(AutoHODName) && !string.IsNullOrEmpty(obj.DepartmentName))
                {
                    var data = sc.UserAccessHOD.ToList();
                    if (data.Count == 0)
                    {
                        sc.UserAccessHOD.Add(obj);
                        sc.SaveChanges();
                        MessageBox.Show("Record saved successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        StaticHelper.Editing = false;
                        resetHODDetail();
                        HODViewModel.loadUserAccess = GetUserAccess();

                    }
                    else
                    {
                        string department = string.Empty;
                        string[] deptarr1 = obj.DepartmentName.TrimEnd(',').Split(',');

                        foreach (var item in data)
                        {
                            string dept = item.DepartmentName;
                            string[] deptarr = dept.TrimEnd(',').Split(',');


                            var hasSameElements = deptarr1.Intersect(deptarr).ToList();

                            department += string.Join(",", hasSameElements) + ",";

                        }

                        department = department.TrimEnd(',');
                        if (string.IsNullOrEmpty(department))
                        {
                            sc.UserAccessHOD.Add(obj);
                            sc.SaveChanges();
                            MessageBox.Show("Record saved successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            StaticHelper.Editing = false;
                            resetHODDetail();
                            HODViewModel.loadUserAccess = GetUserAccess();
                        }
                        else
                        {

                            MessageBox.Show("The " + department + " Department is already assign to HOD, Please remove the " + department + " Department from list of 'Deptartment to be Assigned'", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(AutoHODName))
                        MessageBox.Show("Please fill the Crew Name", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else if (string.IsNullOrEmpty(obj.DepartmentName))
                        MessageBox.Show("Please select Department to be assign", "", MessageBoxButton.OK, MessageBoxImage.Warning);

                }

                ListVisible = "Hidden";

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void UpdateMethod(UserAccessClass obj)
        {
            try
            {
                CheckErrorMessage.CheckErrorMessages2 = true;

                obj.DepartmentName = string.Join(",", DepartmentName2);
                if (!string.IsNullOrEmpty(AutoHODName) && !string.IsNullOrEmpty(obj.DepartmentName))
                {

                    var data = sc.UserAccessHOD.Where(x => x.UserName != obj.UserName).ToList();

                    string department = string.Empty;
                    string[] deptarr1 = obj.DepartmentName.TrimEnd(',').Split(',');

                    foreach (var item in data)
                    {
                        string dept = item.DepartmentName;
                        string[] deptarr = dept.TrimEnd(',').Split(',');


                        var hasSameElements = deptarr1.Intersect(deptarr).ToList();

                        department += string.Join(",", hasSameElements) + ",";

                    }

                    department = department.TrimEnd(',');

                    if (string.IsNullOrEmpty(department))
                    {

                        sc.Entry(obj).State = EntityState.Modified;
                        sc.SaveChanges();
                        resetHODDetail();
                        MessageBox.Show("Record updated successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);

                        HODViewModel.loadUserAccess = GetUserAccess();
                    }
                    else
                    {
                        MessageBox.Show("The " + department + " Department is already assign to HOD, Please remove the " + department + " Department from list of 'Deptartment to be Assigned'", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(AutoHODName))
                        MessageBox.Show("Please fill the Crew Name", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else if (string.IsNullOrEmpty(obj.DepartmentName))
                        MessageBox.Show("Please select Department to be assign", "", MessageBoxButton.OK, MessageBoxImage.Warning);

                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

            ListVisible = "Hidden";
        }

        private ObservableCollection<UserAccessClass> GetUserAccess()
        {

            var data = sc.UserAccessHOD.Select(s => new
            {
                s.ID,
                s.UserName,
                s.CrewManagement,
                s.CrewDetail,
                s.CrewRank,
                s.Department,
                s.HolidayGroup,
                s.HOD,
                s.ResetPassword,
                s.FreezeUnfreeze,
                s.Report,
                s.OverView,
                s.OverTime,
                s.CrewWorkHours,
                s.NonConfirmity,
                s.WorkSchedule,
                s.RestHours,
                s.WorkandResthour,
                s.Administration,
                s.ImportExport,
                s.BackupRestore,
                s.ApplicationLog,
                s.Rules,
                s.MainCertificate,
                s.Certificate,
                s.Lincenc,
                s.Notification,
                s.NCNotification,
                s.CerNotification,
                s.OCNotification,
                s.ErrorLog,
                s.GroupPlanning,
                s.HODName,
                s.DepartmentName
            }).ToList();
            var ranklist = new ObservableCollection<UserAccessClass>();

            foreach (var item in data)
            {
                ranklist.Add(new UserAccessClass()
                {
                    ID = item.ID,
                    UserName = item.UserName,
                    CrewManagement = item.CrewManagement,
                    CrewDetail = item.CrewDetail,
                    CrewRank = item.CrewRank,
                    Department = item.Department,
                    HolidayGroup = item.HolidayGroup,
                    HOD = item.HOD,
                    ResetPassword = item.ResetPassword,
                    FreezeUnfreeze = item.FreezeUnfreeze,
                    Report = item.Report,
                    OverView = item.OverView,
                    OverTime = item.OverTime,
                    CrewWorkHours = item.CrewWorkHours,
                    NonConfirmity = item.NonConfirmity,
                    WorkSchedule = item.WorkSchedule,
                    RestHours = item.RestHours,
                    WorkandResthour = item.WorkandResthour,
                    Administration = item.Administration,
                    ImportExport = item.ImportExport,
                    BackupRestore = item.BackupRestore,
                    ApplicationLog = item.ApplicationLog,
                    Rules = item.Rules,
                    MainCertificate = item.MainCertificate,
                    Certificate = item.Certificate,
                    Lincenc = item.Lincenc,
                    Notification = item.Notification,
                    NCNotification = item.NCNotification,
                    CerNotification = item.CerNotification,
                    OCNotification = item.OCNotification,
                    ErrorLog = item.ErrorLog,
                    GroupPlanning = item.GroupPlanning,
                    HODName = item.HODName,
                    DepartmentName = item.DepartmentName
                });
            }

            return ranklist;
        }

        private ICommand forwordCommand;
        public ICommand ForwordCommand
        {
            get { return forwordCommand; }

        }
        private ICommand backCommand;
        public ICommand BackCommand
        {
            get { return backCommand; }

        }

        private ICommand goBackCommand;
        public ICommand GoBackCommand
        {
            get { return goBackCommand; }

        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }

        private ICommand resetCommand;
        public ICommand ResetCommand
        {
            get { return resetCommand; }
        }

        private static UserAccessClass addUserAccess = new UserAccessClass();
        public UserAccessClass AddUserAccess
        {
            get
            {

                return addUserAccess;

            }
            set
            {

                addUserAccess = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddUserAccess"));
            }
        }


        private static string autoHODName;
        public string AutoHODName
        {
            get
            {
                if (autoHODName != null)
                {
                    AddUserAccess.HODName = autoHODName;
                    AutoCrewName = GetCrewName(autoHODName);
                }
                return autoHODName;
            }

            set
            {
                autoHODName = value;
                if (autoHODName != null)
                    AddUserAccess.HODName = autoHODName;
                OnPropertyChanged(new PropertyChangedEventArgs("AutoHODName"));
            }
        }

        private static ObservableCollection<string> autoCrewName = new ObservableCollection<string>();
        public ObservableCollection<string> AutoCrewName
        {
            get
            {
                return autoCrewName;
            }
            set
            {
                autoCrewName = value;
                RaisePropertyChanged("AutoCrewName");

            }
        }

        private string S_departmentName1;
        public string S_DepartmentName1
        {
            get { return S_departmentName1; }
            set
            {
                S_departmentName1 = value;
                RaisePropertyChanged("S_DepartmentName1");
            }
        }

        private string S_departmentName2;
        public string S_DepartmentName2
        {
            get { return S_departmentName2; }
            set
            {
                S_departmentName2 = value;
                RaisePropertyChanged("S_DepartmentName2");
            }
        }


        private static ObservableCollection<string> departmentName1 = new ObservableCollection<string>();
        public ObservableCollection<string> DepartmentName1
        {
            get
            {
                return departmentName1;
            }
            set
            {
                departmentName1 = value;
                RaisePropertyChanged("DepartmentName1");

            }
        }

        private static ObservableCollection<string> departmentName2 = new ObservableCollection<string>();
        public ObservableCollection<string> DepartmentName2
        {
            get
            {
                return departmentName2;
            }
            set
            {
                departmentName2 = value;
                RaisePropertyChanged("DepartmentName2");

            }
        }

        private ObservableCollection<string> GetCrewName(string typedString)
        {
            DepartmentName1.Clear();
            if (typedString == ob.HODName)
            {
                AddUserAccess.UserName = null;
                AddUserAccess.Departments = null;
            }
            else
            {
                DepartmentName2.Clear();
                AddUserAccess.UserName = null;
                AddUserAccess.Departments = null;
            }


            DateTime dd = DateTime.Now.Date;

            var CrewNames = new ObservableCollection<string>();
            var data = sc.CrewDetails.Where(x => x.position.ToUpper() != "MASTER" && x.UserName.ToLower().Contains(typedString.Trim().ToLower()) && x.ServiceTo >= dd).Select(x => x.name).ToList();

            foreach (string item in data)
            {
                CrewNames.Add(item);

            }


            var data1 = sc.Departments.Select(x => x.DeptName).ToList();
            var data2 = sc.CrewDetails.Where(x => x.name.ToLower().Equals(typedString.Trim().ToLower())).Select(x => new { x.UserName, x.department }).ToList();




            foreach (var item in data2)
            {

                if (typedString == ob.HODName)
                {
                    AddUserAccess.UserName = ob.UserName;
                    AddUserAccess.Departments = item.department;

                    var datab = ob.DepartmentName.TrimEnd(',').Split(',');

                    foreach (var itemb in datab)
                    {
                        departmentName2.Add(itemb);
                    }
                }
                else
                {
                    DepartmentName2.Add(item.department);
                    AddUserAccess.UserName = item.UserName;
                    AddUserAccess.Departments = item.department;
                }


                foreach (var item1 in data1)
                {
                    DepartmentName1.Add(item1);

                }

            }
            RaisePropertyChanged("AddUserAccess");

            if (CrewNames.Count > 0)
            {

                ListVisible = "Visible";
            }
            else
            {
                ListVisible = "Collapsed";

            }


            return CrewNames;
        }


        private static bool _ChView = false;
        public bool ChView
        {
            get
            {

                if (_ChView)
                {
                    addUserAccess.MainCertificate = _ChView;
                    addUserAccess.CerNotification = _ChView;
                    RaisePropertyChanged("AddUserAccess");

                }
                else
                {
                    addUserAccess.MainCertificate = _ChView;
                    addUserAccess.CerNotification = _ChView;
                    _ChAdd = _ChView;
                    RaisePropertyChanged("ChAdd");
                    _ChEdit = _ChView;
                    RaisePropertyChanged("ChEdit");
                    _ChDelete = _ChView;
                    RaisePropertyChanged("ChDelete");
                    RaisePropertyChanged("AddUserAccess");
                }
                addUserAccess.Certificate = _ChView;
                return _ChView;
            }
            set
            {
                _ChView = value;
                RaisePropertyChanged("ChView");
            }
        }

        private static bool _ChAdd = false;
        public bool ChAdd
        {
            get
            {
                if (_ChAdd == true)
                {
                    addUserAccess.MainCertificate = _ChAdd;
                    addUserAccess.CerNotification = _ChAdd;
                    addUserAccess.Certificate = _ChAdd;
                    _ChView = _ChAdd;
                    RaisePropertyChanged("ChView");
                    RaisePropertyChanged("AddUserAccess");
                }
                addUserAccess.CertificateAdd = _ChAdd;
                return _ChAdd;
            }
            set
            {
                _ChAdd = value;
                RaisePropertyChanged("ChAdd");
            }
        }

        private static bool _ChEdit = false;
        public bool ChEdit
        {
            get
            {
                if (_ChEdit == true)
                {
                    addUserAccess.MainCertificate = _ChEdit;
                    addUserAccess.CerNotification = _ChEdit;
                    addUserAccess.Certificate = _ChEdit;
                    _ChView = _ChEdit;
                    RaisePropertyChanged("ChView");
                    RaisePropertyChanged("AddUserAccess");
                }
                addUserAccess.CertificateEdit = _ChEdit;
                return _ChEdit;
            }
            set
            {
                _ChEdit = value;
                RaisePropertyChanged("ChEdit");
            }
        }

        private static bool _ChDelete = false;
        public bool ChDelete
        {
            get
            {
                if (_ChDelete == true)
                {
                    addUserAccess.MainCertificate = _ChDelete;
                    addUserAccess.CerNotification = _ChDelete;
                    addUserAccess.Certificate = _ChDelete;
                    _ChView = _ChDelete;
                    RaisePropertyChanged("ChView");
                    RaisePropertyChanged("AddUserAccess");
                }
                AddUserAccess.CertificateDelete = _ChDelete;
                return _ChDelete;
            }
            set
            {
                _ChDelete = value;
                RaisePropertyChanged("ChDelete");
            }
        }

        private static bool _deviationAllCrew;
        public bool DeviationAllCrew
        {
            get
            {
                addUserAccess.CrewDetailAll = _deviationAllCrew;
                addUserAccess.GroupPlanning = _deviationAllCrew;
                addUserAccess.ResetPasswordAll = _deviationAllCrew;
                addUserAccess.FreezeUnfreezeAll = _deviationAllCrew;
                addUserAccess.WorkandRestHoursAll = _deviationAllCrew;
                addUserAccess.OverViewAll = _deviationAllCrew;
                RaisePropertyChanged("AddUserAccess");
                AddUserAccess.NonConformityAll = _deviationAllCrew;
                return _deviationAllCrew;
            }
            set
            {
                _deviationAllCrew = value;
                RaisePropertyChanged("DeviationAllCrew");
            }
        }


        //Collapsed

        private string listVisible;
        public string ListVisible
        {
            get { return listVisible; }
            set
            {
                listVisible = value;
                RaisePropertyChanged("ListVisible");
            }
        }




        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }


}
