using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using WorkShipVersionII.Views;
using System;
using System.Linq;
using System.Windows;
using DataBuildingLayer;
using System.Windows.Threading;
using WorkShipVersionII.WorkHoursViewModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using WorkShipVersionII.ViewModelAdministration;

namespace WorkShipVersionII.ViewModel
{

       public class MainViewModel : ViewModelBase
       {


              private ViewModelBase _currentViewModel;

              //readonly static NotificationViewModel _notificationViewModel = new NotificationViewModel();
              readonly static NotificationsViewModel _notificationsViewModel = new NotificationsViewModel();
              readonly static CrewManagementViewModel _crewManagementViewModel = new CrewManagementViewModel();
              readonly static WorkRestHoursViewModel _workRestHoursViewModel = new WorkRestHoursViewModel();
              //readonly static ReportsViewModel _reportsViewModel = new ReportsViewModel();
              readonly static AdministrationViewModel _administrationViewModel = new AdministrationViewModel();
              // readonly static CertificationViewModel _certificationViewModel = new CertificationViewModel();
              readonly static TrainingAttachmentViewModell _trainingAttViewModel = new TrainingAttachmentViewModell();
              readonly static MooringCalculatorViewModel _mooringCalculatorViewModel = new MooringCalculatorViewModel();

              BackupViewModel ss = new BackupViewModel();

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



              public ICommand HelpCommand { get; private set; }
              public ICommand NotificationCommand { get; private set; }
              public ICommand CrewManagementCommand { get; private set; }
              public ICommand WorkRestHoursCommand { get; private set; }
              public ICommand ReportsCommand { get; private set; }
              public ICommand AdministrationCommand { get; private set; }
              public ICommand CertificationCommand { get; private set; }
              public ICommand LogoutCommand { get; private set; }

              public ICommand MooringCalculatorCommand { get; private set; }

              //public ICommand CalenderCommand { get; private set; }

              //private readonly AdministrationContaxt sc;

              public MainViewModel()
              {


                     DispatcherTimer timerB = new DispatcherTimer();
                     timerB.Interval = TimeSpan.FromMinutes(3);
                     timerB.Tick += TimerB_Tick;
                     timerB.Start();

                     DispatcherTimer timer = new DispatcherTimer();
                     timer.Interval = TimeSpan.FromMilliseconds(800);
                     timer.Tick += Timer_Tick;
                     timer.Start();


                     StaticHelper.HelpFor = @"2.1 NOTIFICATIONS.htm";
                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     if (UserTypeClass.UserTypes == "HOD")
                     {
                            // new NotificationViewModel();

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

                            // CurrentViewModel = MainViewModel._notificationViewModel;
                            // _notificationsViewModel.GetNotificationList();
                            CurrentViewModel = _notificationsViewModel;
                            NotificationCommand = new RelayCommand(() => ExecuteNotificationCommand());
                            CrewManagementCommand = new RelayCommand(() => ExecuteCrewManagementCommand());
                            WorkRestHoursCommand = new RelayCommand(() => ExecuteWorkRestHoursCommand());
                            ReportsCommand = new RelayCommand(() => ExecuteReportsCommand());
                            AdministrationCommand = new RelayCommand(() => ExecuteAdministrationCommand());
                            CertificationCommand = new RelayCommand(() => ExecuteCertificationCommand());
                            LogoutCommand = new RelayCommand(() => ExcuteLogoutCommand());
                            MooringCalculatorCommand = new RelayCommand(() => ExecuteMooringCalculatorCommand());
                            //CalenderCommand = new RelayCommand(() => ExecuteCalenderCommand());


                     }
              }

              int TimerOn = 0;
              private void TimerB_Tick(object sender, EventArgs e)
              {
                     if (TimerOn == 0)
                     {
                            TimerOn = 1;
                            ss.CreateAutoBackup();
                           
                     }

              }

              public static string colorchange;
              public string ColorChange
              {
                     get
                     {
                            if (colorchange == null)
                                   colorchange = "Hidden";
                            return colorchange;
                     }
                     set
                     {
                            colorchange = value;
                            RaisePropertyChanged("ColorChange");
                     }
              }

              public static string colorchangeRed;
              public string ColorChangeRed
              {
                     get
                     {
                            if (colorchangeRed == null)
                                   colorchangeRed = "Visible";
                            return colorchangeRed;
                     }
                     set
                     {
                            colorchangeRed = value;
                            RaisePropertyChanged("ColorChangeRed");
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
              public static string LoderVisibility = "Hidden";

              public string MyLoderVisibility
              {
                     get { return LoderVisibility; }
                     set
                     {
                            LoderVisibility = value;
                            RaisePropertyChanged("MyLoderVisibility");
                     }
              }
              private void Timer_Tick(object sender, EventArgs e)
              {
                     LoderVisibility = "Hidden";
                     RaisePropertyChanged("MyLoderVisibility");
              }
              private void ExecuteNotificationCommand()
              {
                     //if (ChildWindowManager.Instance.XmlContent == null)
                     //       CurrentViewModel = MainViewModel._notificationViewModel;

                     //if (StaticHelper.Editing == false)
                     //{


                     if (ChildWindowManager.Instance.XmlContent == null)
                     {
                            StaticHelper.HelpFor = @"2.1 NOTIFICATIONS.htm";
                            StaticHelper.TabButtonMenuName = "btnNotification";
                            //CurrentViewModel = MainViewModel._notificationViewModel;
                            // _notificationsViewModel.refreshdata();
                            _notificationsViewModel.GetNotificationList();
                            CurrentViewModel = _notificationsViewModel;
                     }

              }
              public void ExecuteCrewManagementCommand()
              {
                     LoderVisibility = "Hidden";
                     RaisePropertyChanged("MyLoderVisibility");

                     if (ChildWindowManager.Instance.XmlContent == null)
                     {
                            StaticHelper.HelpFor = @"3.0 MOORING MANUAL.htm";
                            StaticHelper.TabButtonMenuName = "btnCrewManagement";
                            CurrentViewModel = MainViewModel._crewManagementViewModel;

                     }

              }
              public void ExecuteWorkRestHoursCommand()
              {

                     //if (StaticHelper.Editing == false)
                     //{


                     if (ChildWindowManager.Instance.XmlContent == null)
                     {
                            StaticHelper.HelpFor = @"4.1.1__MOORING_OPERATION_RECORDS.htm";
                            StaticHelper.TabButtonMenuName = "btnWorkRestHours";
                            CurrentViewModel = MainViewModel._workRestHoursViewModel;

                            StaticHelper.Autoportname = null;

                            MooringOPRListingViewModel._SDateFrom = null;
                            MooringOPRListingViewModel._SDateTo = null;
                            MooringOPRListingViewModel._DateFrom = null;
                            MooringOPRListingViewModel._DateTo = null;

                     }


              }
              private void ExecuteReportsCommand()
              {


                     if (ChildWindowManager.Instance.XmlContent == null)
                     {
                            StaticHelper.TabButtonMenuName = "btnReports";
                            //CurrentViewModel = MainViewModel._reportsViewModel;
                     }

              }

              private void ExecuteMooringCalculatorCommand()
              {
                     if (ChildWindowManager.Instance.XmlContent == null)
                     {
                            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
                            StaticHelper.TabButtonMenuName = "btnReports";
                            CurrentViewModel = MainViewModel._mooringCalculatorViewModel;
                     }
              }
              private void ExecuteAdministrationCommand()
              {


                     if (ChildWindowManager.Instance.XmlContent == null)
                     {
                            StaticHelper.HelpFor = @"6.1  IMPORT  EXPORT.htm";
                            StaticHelper.TabButtonMenuName = "btnAdministration";
                            CurrentViewModel = MainViewModel._administrationViewModel;
                     }

              }
              private void ExecuteCertificationCommand()
              {
                     //if (ChildWindowManager.Instance.XmlContent == null)
                     //       CurrentViewModel = MainViewModel._certificationViewModel;

                     //if (StaticHelper.Editing == false)
                     //{


                     if (ChildWindowManager.Instance.XmlContent == null)
                     {
                            StaticHelper.HelpFor = @"7.1_TYPES_OF_FILES_ALLOWED_TO_BE_UPLOADED_.htm";
                            StaticHelper.TabButtonMenuName = "btnCertification";
                            CurrentViewModel = MainViewModel._trainingAttViewModel;
                     }
                     // }
                     //else
                     //{
                     //       if (MessageBox.Show("Do you want to leave this page without saving record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                     //       {
                     //              // Yes
                     //              StaticHelper.Editing = false;
                     //              // CheckErrorMessage.CheckErrorMessages = false;
                     //              if (ChildWindowManager.Instance.XmlContent == null)
                     //              {
                     //                     StaticHelper.TabButtonMenuName = "btnCertification";
                     //                     CurrentViewModel = MainViewModel._certificationViewModel;
                     //              }
                     //       }
                     //       else
                     //       {
                     //              // No

                     //              MainWindow.MyClickFunctionMainTabButton(StaticHelper.TabButtonMenuName);
                     //       }
                     //}
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





       }
}