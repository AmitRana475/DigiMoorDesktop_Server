using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;


namespace WorkShipVersionII.ViewModelAdministration
{
    public class MainAdministrationViewModel : ViewModelBase
    {
       private ViewModelBase _currentViewModelAdministration;

        readonly static ImportExportViewModel _importexportViewModel = new ImportExportViewModel();
        readonly static BackupViewModel _backupViewModel = new BackupViewModel();
        readonly static ApplicationLogViewModel _applicationlogViewModel = new ApplicationLogViewModel();
        readonly static RulesViewModel _ruleViewModel = new RulesViewModel();
        readonly static LicenceViewModel _licenceViewModel = new LicenceViewModel();
        readonly static ErrorLogViewModel _errorViewModel = new ErrorLogViewModel();
        readonly static DisclaimerViewModel _disclaimerViewModel = new DisclaimerViewModel();
      

        public ViewModelBase CurrentViewModelAdministration
        {
            get
            {
                return _currentViewModelAdministration;
            }
            set
            {
                if (_currentViewModelAdministration == value)
                    return;
                _currentViewModelAdministration = value;
                RaisePropertyChanged("CurrentViewModelAdministration");
            }
        }


        public static CommonPropertiesAdministration _CommonHide = new CommonPropertiesAdministration();
        public CommonPropertiesAdministration CommonHide
        {
            get
            {
                if (_CommonHide == null)
                    _CommonHide = new CommonPropertiesAdministration();
                return _CommonHide;
            }
            set
            {

                _CommonHide = value;
                RaisePropertyChanged("CommonHide");
            }
        }




        public ICommand ImoirtExportAdminCommand { get; private set;}
        public ICommand BackupAdminCommand { get; private set; }
        public ICommand ApplicationLogAdminCommand { get; private set; }
        public ICommand RulesAdminCommand { get; private set; }
        public ICommand LicenceAdminCommand { get; private set; }
        public ICommand ErrorLogAdminCommand { get; private set; }
        public ICommand DisclaimerAdminCommand { get; private set; }
        public ICommand ExitAdminCommand { get; private set; }

        public ICommand HelpCommand { get; private set; }


        public MainAdministrationViewModel()
        {
            CurrentViewModelAdministration = MainAdministrationViewModel._importexportViewModel;
            ImoirtExportAdminCommand = new RelayCommand(()=> ExecuteImoirtExportAdminCommand());
            BackupAdminCommand = new RelayCommand(()=> ExecuteBackupAdminCommand());
            ApplicationLogAdminCommand = new RelayCommand(()=> ExecuteApplicationLogAdminCommand());
            RulesAdminCommand = new RelayCommand(()=> ExecuteRulesAdminCommand());
            LicenceAdminCommand = new RelayCommand(()=> ExecuteLicenceAdminCommand());
            ErrorLogAdminCommand = new RelayCommand(()=>ExecuteErrorLogAdminCommand());
            DisclaimerAdminCommand = new RelayCommand(()=> ExecuteDisclaimerAdminCommand());
            ExitAdminCommand = new RelayCommand(() => ExitMethod());
            HelpCommand = new RelayCommand(() => HelpMethod());


            if (UserTypeClass.UserTypes == "HOD")
            {
                var data = UserTypeClass.HODAccess;
                if (data != null)
                {
                    if (data.SelectAll)
                    {
                    }
                    else
                    {

                        if (data.ImportExport == true)
                        {
                            _CommonHide.ImportExport = "Visible";
                            RaisePropertyChanged("CommonHide");
                        }
                        else
                        {
                            _CommonHide.ImportExport = "Collapsed";
                            RaisePropertyChanged("CommonHide");

                        }

                        if (data.BackupRestore == true)
                        {
                            _CommonHide.BackupRestore = "Visible";
                            RaisePropertyChanged("CommonHide");
                        }
                        else
                        {
                            _CommonHide.BackupRestore = "Collapsed";
                            RaisePropertyChanged("CommonHide");

                        }

                        if (data.ApplicationLog == true)
                        {
                            _CommonHide.ApplicationLog = "Visible";
                            RaisePropertyChanged("CommonHide");
                        }
                        else
                        {
                            _CommonHide.ApplicationLog = "Collapsed";
                            RaisePropertyChanged("CommonHide");

                        }

                        if (data.Rules == true)
                        {
                            _CommonHide.Rules = "Visible";
                            RaisePropertyChanged("CommonHide");
                        }
                        else
                        {
                            _CommonHide.Rules = "Collapsed";
                            RaisePropertyChanged("CommonHide");

                        }

                        if (data.Lincenc == true)
                        {
                            _CommonHide.Lincenc = "Visible";
                            RaisePropertyChanged("CommonHide");
                        }
                        else
                        {
                            _CommonHide.Lincenc = "Collapsed";
                            RaisePropertyChanged("CommonHide");

                        }

                        if (data.ErrorLog == true)
                        {
                            _CommonHide.ErrorLog = "Visible";
                            RaisePropertyChanged("CommonHide");
                        }
                        else
                        {
                            _CommonHide.ErrorLog = "Collapsed";
                            RaisePropertyChanged("CommonHide");

                        }


                    }
                }
            }



        }

        private void HelpMethod()
        {
            //MessageBox.Show("may i help you!");
            try
            {
                string Class1Helpfor = "impexport";
                var path = AppDomain.CurrentDomain.BaseDirectory + "shipment.sdf"; // tt
                System.Windows.Forms.Help.ShowHelp(null, AppDomain.CurrentDomain.BaseDirectory + @"\WorkshipManual\" + "workship.chm", Class1Helpfor + ".htm");
            }
            catch
            {
            }
        }
        private void ExitMethod()
        {
            //Application.Current.MainWindow.Close();
            Application.Current.Shutdown();
        }

        public void ExecuteImoirtExportAdminCommand()
        {
            CurrentViewModelAdministration = MainAdministrationViewModel._importexportViewModel;
        }

        public void ExecuteBackupAdminCommand()
        {
            CurrentViewModelAdministration = MainAdministrationViewModel._backupViewModel;
        }

        public void ExecuteApplicationLogAdminCommand()
        {
            CurrentViewModelAdministration = MainAdministrationViewModel._applicationlogViewModel;
        }

        public void ExecuteRulesAdminCommand()
        {
            CurrentViewModelAdministration = MainAdministrationViewModel._ruleViewModel;
        }

        public void ExecuteLicenceAdminCommand()
        {
            CurrentViewModelAdministration = MainAdministrationViewModel._licenceViewModel;
        }

        public void ExecuteErrorLogAdminCommand()
        {
            CurrentViewModelAdministration = MainAdministrationViewModel._errorViewModel;
        }

        public void ExecuteDisclaimerAdminCommand()
        {
            CurrentViewModelAdministration = MainAdministrationViewModel._disclaimerViewModel;
        }

        

        public override void Cleanup()
        {
            _importexportViewModel.Cleanup();
            _backupViewModel.Cleanup();
            _applicationlogViewModel.Cleanup();
            _ruleViewModel.Cleanup();
            _licenceViewModel.Cleanup();
            _errorViewModel.Cleanup();
            _disclaimerViewModel.Cleanup();
           
        }
    }
}
