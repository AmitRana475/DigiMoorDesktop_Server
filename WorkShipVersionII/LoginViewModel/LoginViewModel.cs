using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModelCrewManagement;

namespace WorkShipVersionII.LoginViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private int erinfo;
        private int erinfo1;
        private readonly ShipmentContaxt sc;
        MainWindow main;




        public LoginViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            // for check security encryption

            //string ss = "wlYFkS+DZ0LtBEqpmYLb5Hy7wKzAZKJ9J5MLfK97biO96+s5b3cEcexVYDmj6VdnIAOQ2yenm0bElhbasjgk2pP9i8ybCNLg";
            //string RecordData = sc.Decrypt(ss, StaticHelper.Key);

            loginCommand = new RelayCommand<AdminLoginClass>(LoginMethod);
            validateCommand = new RelayCommand<ProductInfoClass>(ValidateMethod);
            // new MainViewModelCrewManagement();
            Checksecurity();
        }



        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get { return loginCommand; }
        }

        private ICommand validateCommand;
        public ICommand ValidateCommand
        {
            get
            {
                return validateCommand;
            }

        }


        public static string btnVisible;
        public string BtnVisible
        {
            get
            {
                if (btnVisible == null)
                {
                    var vessel = sc.Vessels.FirstOrDefault();

                    var txtpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "RecordDigiMoor.txt");
                    if (File.Exists(txtpath) && new FileInfo(txtpath).Length > 0)
                    {



                        StreamReader srd = new StreamReader(txtpath);

                        string RecordData = sc.Decrypt(srd.ReadLine(), StaticHelper.Key);
                        srd.Close();

                        string[] RecordArr = RecordData.Split(',');
                        string ss = RecordArr[1]; //Convert.ToDateTime(dtn.Rows[0][0]).ToShortDateString();
                        string shipnm = RecordArr[3];
                        if (shipnm.ToLower() == vessel.VesselName.ToLower())
                        {
                            btnVisible = "Visible";
                            RaisePropertyChanged("BtnVisible");
                            errorMessage = string.Empty;
                            RaisePropertyChanged("ErrorMessage");
                        }
                        else
                        {
                            btnVisible = "Hidden";
                            RaisePropertyChanged("BtnVisible");
                            errorMessage = "Error - 405, Vessel information is missing ! Please contact support team";
                            RaisePropertyChanged("ErrorMessage");
                        }
                    }
                    else
                    {
                        var AdminData = sc.AdminLogins.FirstOrDefault();
                        if (AdminData != null)
                        {
                            string VerifySecurtyFile = AdminData.productinfo;
                            if (VerifySecurtyFile != string.Empty)
                            {
                                btnVisible = "Hidden";
                                RaisePropertyChanged("BtnVisible");
                                errorMessage = "Error - 404, Registry File is missing ! Please contact support team";
                                RaisePropertyChanged("ErrorMessage");
                            }
                        }
                    }
                }
                return btnVisible;
            }
            set
            {
                btnVisible = value;
                RaisePropertyChanged("BtnVisible");
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }


        private static AdminLoginClass adminLogin = new AdminLoginClass();
        public AdminLoginClass AdminLogin
        {
            get
            {
                if (erinfo != 0)
                {
                    Refreshmessage(adminLogin);
                    RaisePropertyChanged("AddLoginMessage");
                }
                return adminLogin;
            }
            set
            {
                adminLogin = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AdminLogin"));
            }
        }


        private static PassErrorMessages _AddLoginMessage = new PassErrorMessages();
        public PassErrorMessages AddLoginMessage
        {
            get
            {
                return _AddLoginMessage;
            }
            set
            {
                _AddLoginMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddLoginMessage"));
            }
        }

        private string productMessage;
        public string ProductMessage
        {
            get
            {
                return productMessage;
            }
            set
            {
                productMessage = value;
                RaisePropertyChanged("ProductMessage");
            }
        }
        private string productVisible;
        public string ProductVisible
        {
            get
            {
                return productVisible;
            }
            set
            {
                productVisible = value;
                RaisePropertyChanged("ProductVisible");
            }
        }

        private string remainingMessage;
        public string RemainingMessage
        {
            get
            {
                return remainingMessage;
            }
            set
            {
                remainingMessage = value;
                RaisePropertyChanged("RemainingMessage");
            }
        }
        private string expiryMessage;
        public string ExpiryMessage
        {
            get
            {
                return expiryMessage;
            }
            set
            {
                expiryMessage = value;
                RaisePropertyChanged("ExpiryMessage");
            }
        }



        private static ProductInfoClass productInfoValidate = new ProductInfoClass();
        public ProductInfoClass ProductInfoValidate
        {
            get
            {
                if (erinfo1 != 0)
                {
                    RefreshMessageKey(productInfoValidate);
                    RaisePropertyChanged("ProductMessage");
                }
                return productInfoValidate;
            }
            set
            {
                productInfoValidate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProductInfoValidate"));
            }
        }

        private void Refreshmessage(AdminLoginClass cdc)
        {
            try
            {
                CheckErrorMessage.CheckErrorMessages = false;
                PassErrorMessages m = (AddLoginMessage as PassErrorMessages); //DownCasting.....


                if (!string.IsNullOrEmpty(cdc.uname))
                {
                    CheckErrorMessage.CheckErrorMessages = true;
                    m.UserNameMessage = string.Empty;
                    RaisePropertyChanged("AddLoginMessage");
                }
                else
                {
                    CheckErrorMessage.CheckErrorMessages = false;
                    m.UserNameMessage = "Please Enter  UserName";
                    RaisePropertyChanged("AddLoginMessage");
                }

                if (!string.IsNullOrEmpty(cdc.pswd))
                {
                    if (!string.IsNullOrEmpty(cdc.uname))
                        CheckErrorMessage.CheckErrorMessages = true;
                    else
                        CheckErrorMessage.CheckErrorMessages = false;
                    m.PasswordMessage = string.Empty;
                    RaisePropertyChanged("AddLoginMessage");
                }
                else
                {
                    CheckErrorMessage.CheckErrorMessages = false;
                    m.PasswordMessage = "Please Enter  Password";
                    RaisePropertyChanged("AddLoginMessage");
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void RefreshMessageKey(ProductInfoClass obj)
        {
            try
            {
                // if (string.IsNullOrEmpty(obj.Text1) || string.IsNullOrEmpty(obj.Text2) || string.IsNullOrEmpty(obj.Text3) || string.IsNullOrEmpty(obj.Text4) || string.IsNullOrEmpty(obj.Text5) || obj.Text1.Length < 4 || obj.Text2.Length < 4 || obj.Text3.Length < 4 || obj.Text4.Length < 4 || obj.Text5.Length < 4)
                if (string.IsNullOrEmpty(obj.TextMain))
                {
                    CheckErrorMessage.CheckErrorMessages = false;
                    productMessage = "Please Enter the Licence Key";
                    RaisePropertyChanged("ProductMessage");
                }
                else if (string.IsNullOrEmpty(obj.TextMain.Trim()))
                {
                    CheckErrorMessage.CheckErrorMessages = false;
                    productMessage = "Please Enter the Licence Key";
                    RaisePropertyChanged("ProductMessage");
                }
                else //if (!string.IsNullOrEmpty(obj.Text1) && !string.IsNullOrEmpty(obj.Text2) && !string.IsNullOrEmpty(obj.Text3) && !string.IsNullOrEmpty(obj.Text4) && !string.IsNullOrEmpty(obj.Text5))
                {
                    CheckErrorMessage.CheckErrorMessages = true;
                    productMessage = string.Empty;
                    RaisePropertyChanged("ProductMessage");
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }
        private void LoginMethod(AdminLoginClass obj)
        {
            try
            {
               // var keys = sc.GetLicencekeys().ToList();

                erinfo = 1;
                Refreshmessage(obj);
                if (CheckErrorMessage.CheckErrorMessages)
                {
                    UserTypeClass.UserName = obj.uname.ToLower();
                    UserTypeClass.UserTypes = obj.uname.ToLower();
                    if (BtnVisible == "Visible")
                    {
                        if (obj.uname.ToLower() == "admin")
                        {     //Admin Login
                            var logn = sc.AdminLogins.FirstOrDefault();
                            var password = sc.Decrypt(logn.pswd, StaticHelper.Key);
                            if (obj.uname.ToLower() == logn.uname.ToLower() && obj.pswd == password)
                            {
                                main = new MainWindow();
                                main.Show();
                                LoginWindow login = App.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
                                if (login != null)
                                {
                                    login.Close();
                                    //login.WindowState = WindowState.Minimized;
                                }
                                CheckErrorMessage.CheckErrorMessages = false;
                            }
                            else
                            {
                                MessageBox.Show("Wrong UserName or Password", "DigiMoor-X7 Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }


                        }
                        else
                        {
                            MessageBox.Show("Wrong UserName or Password", "DigiMoor-X7 Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        //else
                        //{
                        //    // var logn = sc.CrewDetails.Select(s => new { s.UserName, s.pswd, s.ServiceFrom, s.ServiceTo, s.position }).Where(x => x.UserName.ToLower() == obj.uname.ToLower() && x.pswd == obj.pswd).FirstOrDefault();
                        //    var EncrypPwd = sc.Encrypt(obj.pswd, StaticHelper.Key);
                        //    var logn = sc.CrewDetails.Where(x => x.UserName.ToLower() == obj.uname.ToLower() && x.pswd == EncrypPwd).FirstOrDefault();
                        //    if (logn != null)
                        //    {
                        //        DateTime ServerEndDate = Convert.ToDateTime(logn.ServiceTo.ToShortDateString());
                        //        DateTime cdate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        //        if (ServerEndDate >= cdate)
                        //        {

                        //            if (logn.position.ToUpper() == "MASTER")
                        //            {     // MASTER Login
                        //                UserTypeClass.UserTypes = "MASTER";
                        //                main = new MainWindow();
                        //                main.Show();

                        //                LoginWindow login = App.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
                        //                if (login != null)
                        //                {
                        //                    login.Close();
                        //                }

                        //                CheckErrorMessage.CheckErrorMessages = false;
                        //            }
                        //            else
                        //            {
                        //                var user = sc.UserAccessHOD.Where(x => x.UserName.ToLower() == obj.uname.ToLower()).FirstOrDefault();
                        //                if (user != null)
                        //                {      // HOD Login
                        //                    UserTypeClass.UserTypes = "HOD";
                        //                    UserTypeClass.HODAccess = user;
                        //                    main = new MainWindow();
                        //                    main.Show();

                        //                    LoginWindow login = App.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
                        //                    if (login != null)
                        //                    {
                        //                        login.Close();
                        //                    }

                        //                    CheckErrorMessage.CheckErrorMessages = false;
                        //                }
                        //                else
                        //                {
                        //                    // Crew Login
                        //                    UserTypeClass.UserTypes = "Crew";
                        //                    StaticHelper.MyCrewDetails = logn;
                        //                    main = new MainWindow();
                        //                    main.Show();

                        //                    LoginWindow login = App.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
                        //                    if (login != null)
                        //                    {
                        //                        login.Close();
                        //                    }

                        //                    CheckErrorMessage.CheckErrorMessages = false;
                        //                }


                        //            }


                        //        }
                        //        else
                        //        {
                        //            MessageBox.Show("Sorry! You have been off signed for login.", "Work-Ship Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                        //        }

                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Wrong UserName or Password", "Work-Ship Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                        //    }
                        //}
                    }
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }


        private void ValidateMethod(ProductInfoClass obj)
        {
            try
            {
                erinfo1 = 1;
                RefreshMessageKey(obj);
                if (CheckErrorMessage.CheckErrorMessages)
                {
                    ProductKeyClass KeyClass = new ProductKeyClass();

                    var keys = sc.GetLicencekeys().ToList();
                    var vessel = sc.Vessels.FirstOrDefault();

                    if (vessel != null)
                    {
                        KeyClass.VesselName = vessel.VesselName;
                        KeyClass.IMO = vessel.imo;
                        KeyClass.Flag = vessel.Flag;
                    }

                    KeyClass.Key1 = keys.Where(x => x.keyno == "key1").FirstOrDefault().keycode;
                    KeyClass.Key2 = keys.Where(x => x.keyno == "key2").FirstOrDefault().keycode;
                    KeyClass.Key3 = keys.Where(x => x.keyno == "key3").FirstOrDefault().keycode;
                    KeyClass.Key4 = keys.Where(x => x.keyno == "key4").FirstOrDefault().keycode;
                    KeyClass.Key5 = keys.Where(x => x.keyno == "key5").FirstOrDefault().keycode;
                    KeyClass.Key6 = keys.Where(x => x.keyno == "key6").FirstOrDefault().keycode;
                    KeyClass.Key7 = keys.Where(x => x.keyno == "key7").FirstOrDefault().keycode;
                    KeyClass.Key8 = keys.Where(x => x.keyno == "key8").FirstOrDefault().keycode;
                    KeyClass.Key9 = keys.Where(x => x.keyno == "key9").FirstOrDefault().keycode;
                    KeyClass.Key10 = keys.Where(x => x.keyno == "key10").FirstOrDefault().keycode;
                    //KeyClass.Key11 = keys.Where(x => x.keyno == "key11").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key12 = keys.Where(x => x.keyno == "key12").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key13 = keys.Where(x => x.keyno == "key13").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key14 = keys.Where(x => x.keyno == "key14").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key15 = keys.Where(x => x.keyno == "key15").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key16 = keys.Where(x => x.keyno == "key16").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key17 = keys.Where(x => x.keyno == "key17").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key18 = keys.Where(x => x.keyno == "key18").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key19 = keys.Where(x => x.keyno == "key19").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key20 = keys.Where(x => x.keyno == "key20").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key21 = keys.Where(x => x.keyno == "key21").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key22 = keys.Where(x => x.keyno == "key22").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key23 = keys.Where(x => x.keyno == "key23").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key24 = keys.Where(x => x.keyno == "key24").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key25 = keys.Where(x => x.keyno == "key25").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key26 = keys.Where(x => x.keyno == "key26").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key27 = keys.Where(x => x.keyno == "key27").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key28 = keys.Where(x => x.keyno == "key28").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key29 = keys.Where(x => x.keyno == "key29").FirstOrDefault().keycode.ToString();
                    //KeyClass.Key30 = keys.Where(x => x.keyno == "key30").FirstOrDefault().keycode.ToString();

                    // string txtLicenseKey = obj.Text1.Trim().ToUpper() + "-" + obj.Text2.Trim().ToUpper() + "-" + obj.Text3.Trim().ToUpper() + "-" + obj.Text4.Trim().ToUpper() + "-" + obj.Text5.Trim().ToUpper();
                    string[] InputkeyList = { "A", "B" };
                    string DCodeKey = sc.Decrypt(obj.TextMain, StaticHelper.Key);
                    if (DCodeKey != null)
                    {
                        InputkeyList = null;
                        InputkeyList = DCodeKey.Split(',');
                    }

                    string txtLicenseKey = InputkeyList[0];
                    string InputSerialKey = InputkeyList[1];


                    var txtpath101 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "RecordDigiMoor.txt");
                    if (File.Exists(txtpath101))
                    {
                        StreamReader srd = new StreamReader(txtpath101);
                        string RecordData = sc.Decrypt(srd.ReadLine(), StaticHelper.Key);
                        srd.Close();

                        string[] RecordArr = RecordData.Split(',');
                        string nxdt = RecordArr[1];

                        DateTime ct = Convert.ToDateTime(nxdt);

                        string dtt = RecordArr[0];
                        DateTime dtt1 = Convert.ToDateTime(dtt);

                        int mm = dtt1.Month;//.Substring(0, 1);
                        int dd = dtt1.Day;//.Substring(2, 2);
                        int yy = dtt1.Year;//.Substring(5, 4);

                        string dtt5 = RecordArr[1];
                        DateTime dtt55 = Convert.ToDateTime(dtt5);
                        int yy5 = dtt55.Year;

                        if (yy5 == 2021)
                        {
                            ct = ct.AddDays(-1);
                        }

                        KeyClass.Dt1 = KeyClass.Yr1 = new DateTime(yy, mm, dd);
                        KeyClass.Year1 = KeyClass.Yr1.Year;



                        KeyClass.Yr2 = KeyClass.Yr1.AddYears(1);
                        KeyClass.Year2 = KeyClass.Dt1.Year;
                        if (KeyClass.Yr2.Year % 4 == 0)
                        {
                            KeyClass.Dt2 = KeyClass.Dt1.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt2 = KeyClass.Dt1.AddDays(365);
                        }

                        KeyClass.Yr3 = KeyClass.Yr2.AddYears(1);
                        KeyClass.Year3 = KeyClass.Dt2.Year;
                        if (KeyClass.Yr3.Year % 4 == 0)
                        {
                            KeyClass.Dt3 = KeyClass.Dt2.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt3 = KeyClass.Dt2.AddDays(365);
                        }

                        KeyClass.Yr4 = KeyClass.Yr3.AddYears(1);
                        KeyClass.Year4 = KeyClass.Dt3.Year;
                        if (KeyClass.Yr4.Year % 4 == 0)
                        {
                            KeyClass.Dt4 = KeyClass.Dt3.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt4 = KeyClass.Dt3.AddDays(365);
                        }

                        KeyClass.Yr5 = KeyClass.Yr4.AddYears(1);
                        KeyClass.Year5 = KeyClass.Dt4.Year;
                        if (KeyClass.Yr5.Year % 4 == 0)
                        {
                            KeyClass.Dt5 = KeyClass.Dt4.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt5 = KeyClass.Dt4.AddDays(365);
                        }

                        KeyClass.Yr6 = KeyClass.Yr5.AddYears(1);
                        KeyClass.Year6 = KeyClass.Dt5.Year;
                        if (KeyClass.Yr6.Year % 4 == 0)
                        {
                            KeyClass.Dt6 = KeyClass.Dt5.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt6 = KeyClass.Dt5.AddDays(365);
                        }

                        KeyClass.Yr7 = KeyClass.Yr6.AddYears(1);
                        KeyClass.Year7 = KeyClass.Dt6.Year;
                        if (KeyClass.Yr7.Year % 4 == 0)
                        {
                            KeyClass.Dt7 = KeyClass.Dt6.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt7 = KeyClass.Dt6.AddDays(365);
                        }

                        KeyClass.Yr8 = KeyClass.Yr7.AddYears(1);
                        KeyClass.Year8 = KeyClass.Dt7.Year;
                        if (KeyClass.Yr8.Year % 4 == 0)
                        {
                            KeyClass.Dt8 = KeyClass.Dt7.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt8 = KeyClass.Dt7.AddDays(365);
                        }

                        KeyClass.Yr9 = KeyClass.Yr8.AddYears(1);
                        KeyClass.Year9 = KeyClass.Dt8.Year;
                        if (KeyClass.Yr9.Year % 4 == 0)
                        {
                            KeyClass.Dt9 = KeyClass.Dt8.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt9 = KeyClass.Dt8.AddDays(365);
                        }

                        KeyClass.Yr10 = KeyClass.Yr9.AddYears(1);
                        KeyClass.Year10 = KeyClass.Dt9.Year;
                        if (KeyClass.Yr10.Year % 4 == 0)
                        {
                            KeyClass.Dt10 = KeyClass.Dt9.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt10 = KeyClass.Dt9.AddDays(365);
                        }

                        KeyClass.Yr11 = KeyClass.Yr10.AddYears(1);
                        KeyClass.Year11 = KeyClass.Dt10.Year;
                        if (KeyClass.Yr11.Year % 4 == 0)
                        {
                            KeyClass.Dt11 = KeyClass.Dt10.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt11 = KeyClass.Dt10.AddDays(365);
                        }

                        KeyClass.Yr12 = KeyClass.Yr11.AddYears(1);
                        KeyClass.Year12 = KeyClass.Dt11.Year;
                        if (KeyClass.Yr12.Year % 4 == 0)
                        {
                            KeyClass.Dt12 = KeyClass.Dt11.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt12 = KeyClass.Dt11.AddDays(365);
                        }

                        KeyClass.Yr13 = KeyClass.Yr12.AddYears(1);
                        KeyClass.Year13 = KeyClass.Dt12.Year;
                        if (KeyClass.Yr13.Year % 4 == 0)
                        {
                            KeyClass.Dt13 = KeyClass.Dt12.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt13 = KeyClass.Dt12.AddDays(365);
                        }

                        KeyClass.Yr14 = KeyClass.Yr13.AddYears(1);
                        KeyClass.Year14 = KeyClass.Dt13.Year;
                        if (KeyClass.Yr14.Year % 4 == 0)
                        {
                            KeyClass.Dt14 = KeyClass.Dt13.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt14 = KeyClass.Dt13.AddDays(365);
                        }

                        KeyClass.Yr15 = KeyClass.Yr14.AddYears(1);
                        KeyClass.Year15 = KeyClass.Dt14.Year;
                        if (KeyClass.Yr15.Year % 4 == 0)
                        {
                            KeyClass.Dt15 = KeyClass.Dt14.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt15 = KeyClass.Dt14.AddDays(365);
                        }

                        KeyClass.Yr16 = KeyClass.Yr15.AddYears(1);
                        KeyClass.Year16 = KeyClass.Dt15.Year;
                        if (KeyClass.Yr16.Year % 4 == 0)
                        {
                            KeyClass.Dt16 = KeyClass.Dt15.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt16 = KeyClass.Dt15.AddDays(365);
                        }

                        KeyClass.Yr17 = KeyClass.Yr16.AddYears(1);
                        KeyClass.Year17 = KeyClass.Dt16.Year;
                        if (KeyClass.Yr17.Year % 4 == 0)
                        {
                            KeyClass.Dt17 = KeyClass.Dt16.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt17 = KeyClass.Dt16.AddDays(365);
                        }

                        KeyClass.Yr18 = KeyClass.Yr17.AddYears(1);
                        KeyClass.Year18 = KeyClass.Dt17.Year;
                        if (KeyClass.Yr18.Year % 4 == 0)
                        {
                            KeyClass.Dt18 = KeyClass.Dt17.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt18 = KeyClass.Dt17.AddDays(365);
                        }

                        KeyClass.Yr19 = KeyClass.Yr18.AddYears(1);
                        KeyClass.Year19 = KeyClass.Dt18.Year;
                        if (KeyClass.Yr19.Year % 4 == 0)
                        {
                            KeyClass.Dt19 = KeyClass.Dt18.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt19 = KeyClass.Dt18.AddDays(365);
                        }

                        KeyClass.Yr20 = KeyClass.Yr19.AddYears(1);
                        KeyClass.Year20 = KeyClass.Dt19.Year;
                        if (KeyClass.Yr20.Year % 4 == 0)
                        {
                            KeyClass.Dt20 = KeyClass.Dt19.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt20 = KeyClass.Dt19.AddDays(365);
                        }

                        KeyClass.Yr21 = KeyClass.Yr20.AddYears(1);
                        KeyClass.Year21 = KeyClass.Dt20.Year;
                        if (KeyClass.Yr21.Year % 4 == 0)
                        {
                            KeyClass.Dt21 = KeyClass.Dt20.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt21 = KeyClass.Dt20.AddDays(365);
                        }

                        KeyClass.Yr22 = KeyClass.Yr21.AddYears(1);
                        KeyClass.Year22 = KeyClass.Dt21.Year;
                        if (KeyClass.Yr22.Year % 4 == 0)
                        {
                            KeyClass.Dt22 = KeyClass.Dt21.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt22 = KeyClass.Dt21.AddDays(365);
                        }

                        KeyClass.Yr23 = KeyClass.Yr22.AddYears(1);
                        KeyClass.Year23 = KeyClass.Dt22.Year;
                        if (KeyClass.Yr23.Year % 4 == 0)
                        {
                            KeyClass.Dt23 = KeyClass.Dt22.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt23 = KeyClass.Dt22.AddDays(365);
                        }

                        KeyClass.Yr24 = KeyClass.Yr23.AddYears(1);
                        KeyClass.Year24 = KeyClass.Dt23.Year;
                        if (KeyClass.Yr24.Year % 4 == 0)
                        {
                            KeyClass.Dt24 = KeyClass.Dt23.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt24 = KeyClass.Dt23.AddDays(365);
                        }

                        KeyClass.Yr25 = KeyClass.Yr24.AddYears(1);
                        KeyClass.Year25 = KeyClass.Dt24.Year;
                        if (KeyClass.Yr25.Year % 4 == 0)
                        {
                            KeyClass.Dt25 = KeyClass.Dt24.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt25 = KeyClass.Dt24.AddDays(365);
                        }

                        KeyClass.Yr26 = KeyClass.Yr25.AddYears(1);
                        KeyClass.Year26 = KeyClass.Dt25.Year;
                        if (KeyClass.Yr26.Year % 4 == 0)
                        {
                            KeyClass.Dt26 = KeyClass.Dt25.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt26 = KeyClass.Dt25.AddDays(365);
                        }

                        KeyClass.Yr27 = KeyClass.Yr26.AddYears(1);
                        KeyClass.Year27 = KeyClass.Dt26.Year;
                        if (KeyClass.Yr27.Year % 4 == 0)
                        {
                            KeyClass.Dt27 = KeyClass.Dt26.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt27 = KeyClass.Dt26.AddDays(365);
                        }

                        KeyClass.Yr28 = KeyClass.Yr27.AddYears(1);
                        KeyClass.Year28 = KeyClass.Dt27.Year;
                        if (KeyClass.Yr28.Year % 4 == 0)
                        {
                            KeyClass.Dt28 = KeyClass.Dt27.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt28 = KeyClass.Dt27.AddDays(365);
                        }

                        KeyClass.Yr29 = KeyClass.Yr28.AddYears(1);
                        KeyClass.Year29 = KeyClass.Dt28.Year;
                        if (KeyClass.Yr29.Year % 4 == 0)
                        {
                            KeyClass.Dt29 = KeyClass.Dt28.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt29 = KeyClass.Dt28.AddDays(365);
                        }

                        KeyClass.Yr30 = KeyClass.Yr29.AddYears(1);
                        KeyClass.Year30 = KeyClass.Dt29.Year;
                        if (KeyClass.Yr30.Year % 4 == 0)
                        {
                            KeyClass.Dt30 = KeyClass.Dt29.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt30 = KeyClass.Dt29.AddDays(365);
                        }

                        KeyClass.Yr31 = KeyClass.Yr30.AddYears(1);
                        KeyClass.Year31 = KeyClass.Dt30.Year;
                        if (KeyClass.Yr31.Year % 4 == 0)
                        {
                            KeyClass.Dt31 = KeyClass.Dt30.AddDays(366 - 1);
                        }
                        else
                        {
                            KeyClass.Dt31 = KeyClass.Dt30.AddDays(365);
                        }



                        ////=========================

                        DateTime expirydate;
                        if (KeyClass.Dt1.Date == ct.Date)
                        {
                            // if (txtLicenseKey == KeyClass.Key1)
                            if (txtLicenseKey == KeyClass.Key1 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt2;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");

                            }

                        }

                        else if (KeyClass.Dt2.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key2 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt3;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt3.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key3 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt4;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt4.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key4 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt5;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt5.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key5 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt6;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt6.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key6 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt7;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt7.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key7 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt8;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt8.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key8 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt9;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt9.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key9 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt10;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt10.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key10 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt11;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt11.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key11 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt12;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt12.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key12 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt13;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt13.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key13 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt14;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt14.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key14 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt15;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt15.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key15 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt16;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt16.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key16 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt17;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt17.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key17 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt18;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt18.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key18 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt19;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else if (KeyClass.Dt19.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key19 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt20;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }

                        else if (KeyClass.Dt20.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key20 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt21;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else if (KeyClass.Dt21.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key21 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt22;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else if (KeyClass.Dt22.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key22 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt23;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else if (KeyClass.Dt23.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key23 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt24;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else if (KeyClass.Dt24.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key24 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt25;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else if (KeyClass.Dt25.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key25 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt26;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else if (KeyClass.Dt26.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key26 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt27;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else if (KeyClass.Dt27.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key27 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt28;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else if (KeyClass.Dt28.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key28 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt29;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else if (KeyClass.Dt29.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key29 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt30;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else if (KeyClass.Dt30.Date == ct.Date)
                        {
                            if (txtLicenseKey == KeyClass.Key30 && StaticHelper.CPU_ProcessorID == InputSerialKey)
                            {

                                expirydate = KeyClass.Dt31;
                                CreateKeyMethod(RecordArr[0], expirydate.ToString("yyyy-MM-dd"), txtLicenseKey, KeyClass.VesselName);
                            }
                            else
                            {
                                productMessage = "Invalid License Key";
                                RaisePropertyChanged("ProductMessage");
                            }

                        }
                        else
                        {
                            productMessage = "Invalid License Key";
                            RaisePropertyChanged("ProductMessage");

                        }


                    }


                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void CreateKeyMethod(string FirstDate, string NextDate, string LicenseKey, string vesselName)
        {
            try
            {
                string exp = NextDate;

                var txtpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "RecordDigiMoor.txt");
                if (File.Exists(txtpath) && new FileInfo(txtpath).Length > 0)
                {

                    StreamReader srd = new StreamReader(txtpath);

                    string LastYearRecordWork49Data = srd.ReadLine();
                    string OldRecordData = sc.Decrypt(LastYearRecordWork49Data, StaticHelper.Key);
                    srd.Close();

                    string[] lastRecord = OldRecordData.Split(',');
                    string kpl = lastRecord[2].ToString();
                    string shipnm = lastRecord[3].ToString();

                    if (shipnm.ToLower() == vesselName.ToLower())
                    {
                        StreamWriter writer = new StreamWriter(txtpath);
                        //Encrypt method has first parameter as data to be encrypted and second as key for encryption

                        writer.Write(sc.Encrypt(FirstDate + "," + NextDate + "," + LicenseKey + "," + vesselName, StaticHelper.Key));
                        writer.Close();

                        string Recordwork49file = sc.Encrypt(FirstDate + "," + NextDate + "," + LicenseKey + "," + vesselName, StaticHelper.Key);

                        var data = sc.AdminLogins.FirstOrDefault();
                        if (data != null)
                        {
                            data.productinfo = Recordwork49file;
                            sc.Entry(data).State = EntityState.Modified;
                            sc.SaveChanges();
                        }



                        expiryMessage = "Expired On" + " " + exp.ToString();
                        RaisePropertyChanged("ExpiryMessage");

                        DateTime date = Convert.ToDateTime(exp);
                        DateTime d2 = DateTime.Now;

                        TimeSpan t = date.Date - d2.Date;
                        double NrOfDays = t.TotalDays;

                        remainingMessage = "Remaining Days Left" + " " + NrOfDays.ToString();
                        RaisePropertyChanged("RemainingMessage");

                        errorMessage = string.Empty;
                        RaisePropertyChanged("ErrorMessage");

                        MessageBox.Show("Your License is validated. Thank You!", "DigiMoor-X7 Login", MessageBoxButton.OK, MessageBoxImage.Information);

                        btnVisible = "Visible";
                        RaisePropertyChanged("BtnVisible");

                        productVisible = "Hidden";
                        RaisePropertyChanged("ProductVisible");


                    }
                    else
                    {

                        MessageBox.Show("It is not Valid Ship, Your License is terminated!!!", "DigiMoor-X7 Login", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Error - 404, Registry File is missing ! Please contact support team", "DigiMoor-X7 Login", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void Checksecurity()
        {
            try
            {
                productVisible = "Hidden";
                RaisePropertyChanged("ProductVisible");

                var vessel = sc.Vessels.FirstOrDefault();
                if (vessel != null)
                {
                    var txtpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "RecordDigiMoor.txt");
                    if (File.Exists(txtpath) && new FileInfo(txtpath).Length > 0)
                    {

                        btnVisible = "Visible";
                        RaisePropertyChanged("BtnVisible");
                        errorMessage = string.Empty;
                        RaisePropertyChanged("ErrorMessage");

                        StreamReader srd = new StreamReader(txtpath);
                        //StreamReader srd = new StreamReader("x3ebu5LWr60oDzbazO5+JFpkwMECOl2w4+H3XtHj1xo=");



                        //Test Keys

                        ProductKeyClass KeyClass = new ProductKeyClass();

                        var keys = sc.GetLicencekeys().ToList();


                        KeyClass.Key1 = keys.Where(x => x.keyno == "key1").FirstOrDefault().keycode;
                        KeyClass.Key2 = keys.Where(x => x.keyno == "key2").FirstOrDefault().keycode;
                        KeyClass.Key3 = keys.Where(x => x.keyno == "key3").FirstOrDefault().keycode;
                        KeyClass.Key4 = keys.Where(x => x.keyno == "key4").FirstOrDefault().keycode;
                        KeyClass.Key5 = keys.Where(x => x.keyno == "key5").FirstOrDefault().keycode;
                        KeyClass.Key6 = keys.Where(x => x.keyno == "key6").FirstOrDefault().keycode;
                        KeyClass.Key7 = keys.Where(x => x.keyno == "key7").FirstOrDefault().keycode;
                        KeyClass.Key8 = keys.Where(x => x.keyno == "key8").FirstOrDefault().keycode;
                        KeyClass.Key9 = keys.Where(x => x.keyno == "key9").FirstOrDefault().keycode;
                        KeyClass.Key10 = keys.Where(x => x.keyno == "key10").FirstOrDefault().keycode;


                        //Test Keys










                        string RecordData = sc.Decrypt(srd.ReadLine(), StaticHelper.Key);
                        srd.Close();

                        string[] RecordArr = RecordData.Split(',');
                        string ss = RecordArr[1]; //Convert.ToDateTime(dtn.Rows[0][0]).ToShortDateString();
                        string shipnm = RecordArr[3];
                        if (shipnm.ToLower() == vessel.VesselName.ToLower())
                        //if ("Test Vessel" == "Test Vessel")
                        {

                            expiryMessage = "Expiring On" + " " + ss;
                            RaisePropertyChanged("ExpiryMessage");

                            DateTime date;
                            if (DateTime.TryParseExact(ss, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out date))
                            {

                            }
                            else
                            {
                                // do something............
                                char[] spl = { '/', '-' };
                                string[] datarr = ss.Split(spl);
                                string part1 = datarr[0];
                                string part2 = datarr[1];
                                string part3 = datarr[2];

                                if (part1.Length == 1)
                                {
                                    part1 = "0" + part1;
                                }
                                if (part2.Length == 1)
                                {
                                    part2 = "0" + part2;
                                }
                                if (part3.Length == 1)
                                {
                                    part3 = "0" + part3;
                                }

                                ss = part1 + "/" + part2 + "/" + part3;   // dd/MM/YYYY
                                date = DateTime.ParseExact(ss, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

                            }



                            DateTime d1 = DateTime.Now;

                            TimeSpan t = date.Date - d1.Date;
                            double NrOfDays = t.TotalDays;
                            if (NrOfDays <= 0)
                            {
                                //var lblRemainingDays = "Remaining Day" + " " + "0";
                                //daysleft = 0;

                                productVisible = "Visible";
                                RaisePropertyChanged("ProductVisible");
                                btnVisible = "Hidden";
                                RaisePropertyChanged("BtnVisible");
                                //txtkey1.Focus();
                            }

                            if (NrOfDays <= 30)
                            {
                                errorMessage = "Remaining Day" + " " + NrOfDays.ToString();
                                RaisePropertyChanged("ErrorMessage");
                            }


                        }
                        else
                        {
                            btnVisible = "Hidden";
                            RaisePropertyChanged("BtnVisible");
                            errorMessage = "Error - 405, Vessel information is missing ! Please contact support team";
                            RaisePropertyChanged("ErrorMessage");

                        }
                    }
                    else
                    {
                        string VerifySecurtyFile = null;
                        var AdminData = sc.AdminLogins.FirstOrDefault();
                        if (AdminData != null)
                        {
                            VerifySecurtyFile = AdminData.productinfo;
                        }
                        if (File.Exists(txtpath) == false && String.IsNullOrEmpty(VerifySecurtyFile))// != string.Empty)
                        {
                        }
                        else
                        {
                            btnVisible = "Hidden";
                            RaisePropertyChanged("BtnVisible");
                            errorMessage = "Error - 404, Registry File is missing ! Please contact support team";
                            RaisePropertyChanged("ErrorMessage");
                        }


                    }


                }





            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
