using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Data.Entity;
using System;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class HODViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public HODViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
         
            deleteCommand = new RelayCommand<UserAccessClass>(DeleteHOD);
            LoadUserAccess.Clear();
            LoadUserAccess = GetUserAccess();

        }

       

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }
            
        }

        


        public static ObservableCollection<UserAccessClass> loadUserAccess = new ObservableCollection<UserAccessClass>();
        public ObservableCollection<UserAccessClass> LoadUserAccess
        {
            get
            {
                return loadUserAccess;
            }
            set
            {
                loadUserAccess = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
            }
        }

        private ObservableCollection<UserAccessClass> GetUserAccess()
        {

            DateTime dd = DateTime.Now.Date;
            var data = (from s in sc.UserAccessHOD
                        join dept in sc.CrewDetails
                        on s.UserName.Trim() equals dept.UserName.Trim()
                        select new
                        {
                            ServiceTo = dept.ServiceTo,
                            ID = s.ID,
                            UserName = s.UserName,
                            CrewManagement = s.CrewManagement,
                            CrewDetail = s.CrewDetail,
                            CrewRank = s.CrewRank,
                            Department = s.Department,
                            HolidayGroup = s.HolidayGroup,
                            HOD = s.HOD,
                            ResetPassword = s.ResetPassword,
                            FreezeUnfreeze = s.FreezeUnfreeze,
                            Report = s.Report,
                            OverView = s.OverView,
                            OverTime = s.OverTime,
                            CrewWorkHours = s.CrewWorkHours,
                            NonConfirmity = s.NonConfirmity,
                            WorkSchedule = s.WorkSchedule,
                            RestHours = s.RestHours,
                            WorkandResthour = s.WorkandResthour,
                            Administration = s.Administration,
                            ImportExport = s.ImportExport,
                            BackupRestore = s.BackupRestore,
                            ApplicationLog = s.ApplicationLog,
                            Rules = s.Rules,
                            MainCertificate = s.MainCertificate,
                            Certificate = s.Certificate,
                            Lincenc = s.Lincenc,
                            Notification = s.Notification,
                            NCNotification = s.NCNotification,
                            CerNotification = s.CerNotification,
                            OCNotification = s.OCNotification,
                            ErrorLog = s.ErrorLog,
                            GroupPlanning = s.GroupPlanning,
                            HODName = s.HODName,
                            DepartmentName = s.DepartmentName
                        }).Where(p => p.ServiceTo >= dd).ToList();



            //var data1 = sc.UserAccessHOD.Select(s => new
            //{
            //    s.ID,
            //    s.UserName,
            //    s.CrewManagement,
            //    s.CrewDetail,
            //    s.CrewRank,
            //    s.Department,
            //    s.HolidayGroup,
            //    s.HOD,
            //    s.ResetPassword,
            //    s.FreezeUnfreeze,
            //    s.Report,
            //    s.OverView,
            //    s.OverTime,
            //    s.CrewWorkHours,
            //    s.NonConfirmity,
            //    s.WorkSchedule,
            //    s.RestHours,
            //    s.WorkandResthour,
            //    s.Administration,
            //    s.ImportExport,
            //    s.BackupRestore,
            //    s.ApplicationLog,
            //    s.Rules,
            //    s.MainCertificate,
            //    s.Certificate,
            //    s.Lincenc,
            //    s.Notification,
            //    s.NCNotification,
            //    s.CerNotification,
            //    s.OCNotification,
            //    s.ErrorLog,
            //    s.GroupPlanning,
            //    s.HODName,
            //    s.DepartmentName
            //}).ToList();


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

        private void DeleteHOD(UserAccessClass obj)
        {
            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                UserAccessClass findrank = sc.UserAccessHOD.Where(x => x.ID == obj.ID && x.UserName==obj.UserName).FirstOrDefault();
                if (findrank != null)
                {


                    sc.Entry(findrank).State = EntityState.Deleted;
                    sc.SaveChanges();

                    MessageBox.Show("Record deleted successfully", "Delete HOD");


                    //.....Refresh DataGrid........

                    loadUserAccess = GetUserAccess();
                    RaisePropertyChanged("LoadUserAccess");
                    //.....End Refresh DataGrid........

                }
                else
                {
                    MessageBox.Show("Record is not found ", "Delete Crew Rank");
                }

            }
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
