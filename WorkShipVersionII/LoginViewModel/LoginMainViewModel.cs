using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Windows.Input;
using WorkShipVersionII.ViewModelCrewManagement;

namespace WorkShipVersionII.LoginViewModel
{
    public class LoginMainViewModel : ViewModelBase
    {

        private readonly AdministrationContaxt sc;

        readonly static AdminLoginViewModel _adminLoginView = new AdminLoginViewModel();
        readonly static CongrasViewModel _congrasView = new CongrasViewModel();
        readonly static LoginViewModel _loginView = new LoginViewModel();
        readonly static ProductInfoViewModel _productInfoView = new ProductInfoViewModel();
        readonly static ShipDetailViewModel _shipDetailView = new ShipDetailViewModel();


        public ICommand AdminLoginCommand { get; private set; }
        public ICommand CongrasCommand { get; private set; }
        public ICommand LoginViewCommand { get; private set; }
        public ICommand ProductInfoCommand { get; private set; }
        public ICommand ShipDetailCommand { get; private set; }



        private ViewModelBase _loginViewModel;
        public ViewModelBase LoginViewModel
        {
            get
            {
                return _loginViewModel;
            }
            set
            {
                if (_loginViewModel == value)
                    return;
                _loginViewModel = value;
                RaisePropertyChanged("LoginViewModel");
            }
        }


        public LoginMainViewModel()
        {

            if (sc == null)
            {
                sc = new AdministrationContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            var login = sc.AdminLogins.FirstOrDefault();
            if (login != null)
            {
                if (!string.IsNullOrEmpty(login.LastLogin.ToString()))
                    LoginViewModel = LoginMainViewModel._loginView;
                else
                    LoginViewModel = LoginMainViewModel._shipDetailView;
            }

            else
            {
                LoginViewModel = LoginMainViewModel._shipDetailView;
            }




            AdminLoginCommand = new RelayCommand(() => AdminLoginMethod());
            CongrasCommand = new RelayCommand(() => CongrasMethod());
            LoginViewCommand = new RelayCommand(() => LoginViewMethod());
            ProductInfoCommand = new RelayCommand(() => ProductInfoMethod());
            ShipDetailCommand = new RelayCommand(() => ShipDetailMethod());

        }


        private void ShipDetailMethod()
        {
            LoginViewModel = LoginMainViewModel._shipDetailView;
        }

        private void ProductInfoMethod()
        {// kuldeep
            if (CheckErrorMessage.CheckErrorMessages)
            {
                if (CheckErrorMessage.CheckErrorMessages1)
                    LoginViewModel = LoginMainViewModel._congrasView;
                else
                    LoginViewModel = LoginMainViewModel._productInfoView;
            }
        }

        private void LoginViewMethod()
        {
            LoginViewModel = LoginMainViewModel._loginView;
        }

        private void CongrasMethod()
        {
            if (CheckErrorMessage.CheckErrorMessages)
                LoginViewModel = LoginMainViewModel._congrasView;
        }

        private void AdminLoginMethod()
        {

            LoginViewModel = LoginMainViewModel._adminLoginView;
        }




    }
}
