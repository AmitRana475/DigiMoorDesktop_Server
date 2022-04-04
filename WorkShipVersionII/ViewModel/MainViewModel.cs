using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using WorkShipVersionII.Views;
using System;
using System.Linq;
using System.Windows;
using DataBuildingLayer;

namespace WorkShipVersionII.ViewModel
{

       public class MainViewModel : ViewModelBase
       {
              private ViewModelBase _currentViewModel;

              readonly static NotificationViewModel _notificationViewModel = new NotificationViewModel();
              readonly static CrewManagementViewModel _crewManagementViewModel = new CrewManagementViewModel();
              readonly static WorkRestHoursViewModel _workRestHoursViewModel = new WorkRestHoursViewModel();
              readonly static ReportsViewModel _reportsViewModel = new ReportsViewModel();
              readonly static AdministrationViewModel _administrationViewModel = new AdministrationViewModel();
              readonly static CertificationViewModel _certificationViewModel = new CertificationViewModel();


              //readonly static WorkHoursCalenderViewModel _workHoursCalenderViewModel = new WorkHoursCalenderViewModel();


              public ViewModelBase CurrentViewModel
              {
                     get
                     {
                            return _currentViewModel;
                     }
                     set
                     {
                            if (_currentViewModel == value)
                                   return;
                            _currentViewModel = value;
                            RaisePropertyChanged("CurrentViewModel");
                     }
              }




              public ICommand NotificationCommand { get; private set; }
              public ICommand CrewManagementCommand { get; private set; }
              public ICommand WorkRestHoursCommand { get; private set; }
              public ICommand ReportsCommand { get; private set; }
              public ICommand AdministrationCommand { get; private set; }
              public ICommand CertificationCommand { get; private set; }
              public ICommand LogoutCommand { get; private set; }

              //public ICommand CalenderCommand { get; private set; }

              //private readonly AdministrationContaxt sc;

              public MainViewModel()
              {


                     //if (sc == null)
                     //{
                     //    sc = new AdministrationContaxt();
                     //    sc.Configuration.ProxyCreationEnabled = false;
                     //}


                     if (UserTypeClass.UserTypes == "HOD")
                     {
                            new NotificationViewModel();

                            var data = UserTypeClass.HODAccess;
                            if (data != null)
                            {
                                   if (data.SelectAll)
                                   {
                                   }
                                   else
                                   {
                                          //if (data.Notification)
                                          //{


                                          //}
                                          //else
                                          //{ }

                                          //if (data.CrewManagement == true)
                                          //{

                                          //}
                                          //else
                                          //{ }

                                          //if (data.RestHours == true)
                                          //{
                                          //}
                                          //else
                                          //{ }

                                          //if (data.Report == true)
                                          //{ }
                                          //else
                                          //{ }

                                          //if (data.Administration == true)
                                          //{ }
                                          //else
                                          //{ }

                                          if (data.MainCertificate == true)
                                          {
                                                 _CommonDeviation.CertificationVisible = "Visible";
                                                 _CommonDeviation.CertiWidth = "*";
                                                 RaisePropertyChanged("CommonDeviation");
                                          }
                                          else
                                          {
                                                 _CommonDeviation.CertificationVisible = "Collapsed";
                                                 RaisePropertyChanged("CommonDeviation");
                                                 _CommonDeviation.CertiWidth = "0";
                                                 RaisePropertyChanged("CommonDeviation");
                                          }


                                   }
                            }
                     }
                     else if (UserTypeClass.UserTypes == "Crew")
                     {

                            _CommonDeviation.NotificationVisible = "Hidden";
                            _CommonDeviation.CrewManagementVisible = "Hidden";
                            _CommonDeviation.AdministrationVisible = "Hidden";
                            _CommonDeviation.CertificationVisible = "Hidden";
                            _CommonDeviation.NotiWidth = "0";
                            _CommonDeviation.CrewWidth = "0";
                            _CommonDeviation.AdminWidth = "0";
                            _CommonDeviation.CertiWidth = "0";
                            CurrentViewModel = MainViewModel._workRestHoursViewModel;
                            // NotificationCommand = new RelayCommand(() => ExecuteNotificationCommand());
                            // CrewManagementCommand = new RelayCommand(() => ExecuteCrewManagementCommand());
                            WorkRestHoursCommand = new RelayCommand(() => ExecuteWorkRestHoursCommand());
                            ReportsCommand = new RelayCommand(() => ExecuteReportsCommand());
                            // AdministrationCommand = new RelayCommand(() => ExecuteAdministrationCommand());
                            // CertificationCommand = new RelayCommand(() => ExecuteCertificationCommand());
                            LogoutCommand = new RelayCommand(() => ExcuteLogoutCommand());
                            //CalenderCommand = new RelayCommand(() => ExecuteCalenderCommand());

                            //ShowChildwindowCommandCertificates = new RelayCommand(ShowPopupCertificates); //ChildWindows for Add Department
                            //ShowChildwindowCertificatesExportList = new RelayCommand(ShowPopupCertificatesExportList);
                     }
                     else if (UserTypeClass.UserTypes == "MASTER" || UserTypeClass.UserTypes == "admin")
                     {
                            // new NotificationViewModel();

                            CurrentViewModel = MainViewModel._notificationViewModel;
                            NotificationCommand = new RelayCommand(() => ExecuteNotificationCommand());
                            CrewManagementCommand = new RelayCommand(() => ExecuteCrewManagementCommand());
                            WorkRestHoursCommand = new RelayCommand(() => ExecuteWorkRestHoursCommand());
                            ReportsCommand = new RelayCommand(() => ExecuteReportsCommand());
                            AdministrationCommand = new RelayCommand(() => ExecuteAdministrationCommand());
                            CertificationCommand = new RelayCommand(() => ExecuteCertificationCommand());
                            LogoutCommand = new RelayCommand(() => ExcuteLogoutCommand());
                            //CalenderCommand = new RelayCommand(() => ExecuteCalenderCommand());

                            ShowChildwindowCommandCertificates = new RelayCommand(ShowPopupCertificates); //ChildWindows for Add Department
                            ShowChildwindowCertificatesExportList = new RelayCommand(ShowPopupCertificatesExportList);
                     }
              }



              public static CommonPropertiesDeviation _CommonDeviation;
              public CommonPropertiesDeviation CommonDeviation
              {
                     get
                     {
                            if (_CommonDeviation == null)
                                   _CommonDeviation = new CommonPropertiesDeviation();
                            return _CommonDeviation;
                     }
                     set
                     {

                            _CommonDeviation = value;
                            RaisePropertyChanged("CommonDeviation");
                     }
              }


              private void ExecuteNotificationCommand()
              {
                     //if (ChildWindowManager.Instance.XmlContent == null)
                     //       CurrentViewModel = MainViewModel._notificationViewModel;

                     if (StaticHelper.Editing == false)
                     {


                            if (ChildWindowManager.Instance.XmlContent == null)
                            {
                                   StaticHelper.TabButtonMenuName = "btnNotification";
                                   CurrentViewModel = MainViewModel._notificationViewModel;
                            }
                     }
                     else
                     {
                            if (MessageBox.Show("Do you want to leave this page without saving record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   // Yes
                                   StaticHelper.Editing = false;
                                   // CheckErrorMessage.CheckErrorMessages = false;
                                   if (ChildWindowManager.Instance.XmlContent == null)
                                   {
                                          StaticHelper.TabButtonMenuName = "btnNotification";
                                          CurrentViewModel = MainViewModel._notificationViewModel;
                                   }
                            }
                            else
                            {
                                   // No

                                   MainWindow.MyClickFunctionMainTabButton(StaticHelper.TabButtonMenuName);
                            }
                     }

              }
              private void ExecuteCrewManagementCommand()
              {
                     //if (ChildWindowManager.Instance.XmlContent == null)
                     //{
                     //       StaticHelper.TabButtonMenuName = "btnCrewManagement";
                     //       CurrentViewModel = MainViewModel._crewManagementViewModel;
                     //}
                     
                     if (StaticHelper.Editing == false)
                     {


                            if (ChildWindowManager.Instance.XmlContent == null)
                            {
                                   StaticHelper.TabButtonMenuName = "btnCrewManagement";
                                   CurrentViewModel = MainViewModel._crewManagementViewModel;
                            }
                     }
                     else
                     {
                            if (MessageBox.Show("Do you want to leave this page without saving record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   // Yes
                                   StaticHelper.Editing = false;
                                   // CheckErrorMessage.CheckErrorMessages = false;
                                   if (ChildWindowManager.Instance.XmlContent == null)
                                   {
                                          StaticHelper.TabButtonMenuName = "btnCrewManagement";
                                          CurrentViewModel = MainViewModel._crewManagementViewModel;
                                   }
                            }
                            else
                            {
                                   // No

                                   MainWindow.MyClickFunctionMainTabButton(StaticHelper.TabButtonMenuName);
                            }
                     }
              }
              private void ExecuteWorkRestHoursCommand()
              {
                     
                     if (StaticHelper.Editing == false)
                     {


                            if (ChildWindowManager.Instance.XmlContent == null)
                            {
                                   StaticHelper.TabButtonMenuName = "btnWorkRestHours";
                                   CurrentViewModel = MainViewModel._workRestHoursViewModel;
                            }
                     }
                     else
                     {
                            if (MessageBox.Show("Do you want to leave this page without saving record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   // Yes
                                   StaticHelper.Editing = false;
                                   // CheckErrorMessage.CheckErrorMessages = false;
                                   if (ChildWindowManager.Instance.XmlContent == null)
                                   {
                                          StaticHelper.TabButtonMenuName = "btnWorkRestHours";
                                          CurrentViewModel = MainViewModel._workRestHoursViewModel;
                                   }
                            }
                            else
                            {
                                   // No
                                 
                                   MainWindow.MyClickFunctionMainTabButton(StaticHelper.TabButtonMenuName);
                            }
                     }

              }
              private void ExecuteReportsCommand()
              {
                     //if (ChildWindowManager.Instance.XmlContent == null)
                     //       CurrentViewModel = MainViewModel._reportsViewModel;

                     if (StaticHelper.Editing == false)
                     {


                            if (ChildWindowManager.Instance.XmlContent == null)
                            {
                                   StaticHelper.TabButtonMenuName = "btnReports";
                                   CurrentViewModel = MainViewModel._reportsViewModel;
                            }
                     }
                     else
                     {
                            if (MessageBox.Show("Do you want to leave this page without saving record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   // Yes
                                   StaticHelper.Editing = false;
                                   // CheckErrorMessage.CheckErrorMessages = false;
                                   if (ChildWindowManager.Instance.XmlContent == null)
                                   {
                                          StaticHelper.TabButtonMenuName = "btnReports";
                                          CurrentViewModel = MainViewModel._reportsViewModel;
                                   }
                            }
                            else
                            {
                                   // No

                                   MainWindow.MyClickFunctionMainTabButton(StaticHelper.TabButtonMenuName);
                            }
                     }
              }
              private void ExecuteAdministrationCommand()
              {
                     //if (ChildWindowManager.Instance.XmlContent == null)
                     //       CurrentViewModel = MainViewModel._administrationViewModel;

                     if (StaticHelper.Editing == false)
                     {


                            if (ChildWindowManager.Instance.XmlContent == null)
                            {
                                   StaticHelper.TabButtonMenuName = "btnAdministration";
                                   CurrentViewModel = MainViewModel._administrationViewModel;
                            }
                     }
                     else
                     {
                            if (MessageBox.Show("Do you want to leave this page without saving record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   // Yes
                                   StaticHelper.Editing = false;
                                   // CheckErrorMessage.CheckErrorMessages = false;
                                   if (ChildWindowManager.Instance.XmlContent == null)
                                   {
                                          StaticHelper.TabButtonMenuName = "btnAdministration";
                                          CurrentViewModel = MainViewModel._administrationViewModel;
                                   }
                            }
                            else
                            {
                                   // No

                                   MainWindow.MyClickFunctionMainTabButton(StaticHelper.TabButtonMenuName);
                            }
                     }
              }
              private void ExecuteCertificationCommand()
              {
                     //if (ChildWindowManager.Instance.XmlContent == null)
                     //       CurrentViewModel = MainViewModel._certificationViewModel;

                     if (StaticHelper.Editing == false)
                     {


                            if (ChildWindowManager.Instance.XmlContent == null)
                            {
                                   StaticHelper.TabButtonMenuName = "btnCertification";
                                   CurrentViewModel = MainViewModel._certificationViewModel;
                            }
                     }
                     else
                     {
                            if (MessageBox.Show("Do you want to leave this page without saving record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   // Yes
                                   StaticHelper.Editing = false;
                                   // CheckErrorMessage.CheckErrorMessages = false;
                                   if (ChildWindowManager.Instance.XmlContent == null)
                                   {
                                          StaticHelper.TabButtonMenuName = "btnCertification";
                                          CurrentViewModel = MainViewModel._certificationViewModel;
                                   }
                            }
                            else
                            {
                                   // No

                                   MainWindow.MyClickFunctionMainTabButton(StaticHelper.TabButtonMenuName);
                            }
                     }
              }

              private void ExcuteLogoutCommand()
              {
                     if (ChildWindowManager.Instance.XmlContent == null)
                     {
                            if (UserTypeClass.UserTypes == "admin")
                            {
                                   MainWindow logout = App.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                                   if (logout != null)
                                   {
                                          logout.WindowState = WindowState.Minimized;
                                   }
                            }
                            else
                            {
                                   MainWindow logout = App.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                                   if (logout != null)
                                   {

                                          logout.Close();
                                   }
                            }
                     }
              }


              //private void ExecuteCalenderCommand()
              //{
              //    if (ChildWindowManager.Instance.XmlContent == null)
              //        CurrentViewModel = MainViewModel._workHoursCalenderViewModel;
              //}


              public RelayCommand ShowChildwindowCommandCertificates { get; }
              public RelayCommand ShowChildwindowCertificatesExportList { get; }

              private void ShowPopupCertificates()
              {
                     AddCertificateViewModel vmc = new AddCertificateViewModel();
                     ChildWindowManager.Instance.ShowChildWindow(new AddCertificateListView() { DataContext = vmc });
              }

              private void ShowPopupCertificatesExportList()
              {
                     CertificationExportListViewModel vmc = new CertificationExportListViewModel();
                     ChildWindowManager.Instance.ShowChildWindow(new CertificationExportList() { DataContext = vmc });
              }



       }
}