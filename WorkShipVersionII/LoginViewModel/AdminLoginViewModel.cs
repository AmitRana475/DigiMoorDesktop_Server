using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows.Input;
using WorkShipVersionII.ViewModelCrewManagement;

namespace WorkShipVersionII.LoginViewModel
{
    public class AdminLoginViewModel : ViewModelBase
    {
        private int erinfo;
        private readonly AdministrationContaxt sc;
        public AdminLoginViewModel()
        {
            if (sc == null)
            {
                sc = new AdministrationContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            nextCommand = new RelayCommand<AdminLoginClass>(NextMethod);

            try
            {
                var data = sc.Versions.FirstOrDefault();
                if (data != null)
                {
                    versions = data.versions;
                    RaisePropertyChanged("Versions");
                }
                else
                {
                    versions = "2.0.1";
                    RaisePropertyChanged("Versions");
                }


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private ICommand nextCommand;
        public ICommand NextCommand
        {
            get { return nextCommand; }
        }

        private static AdminLoginClass adminAccess = new AdminLoginClass();
        public AdminLoginClass AdminAccess
        {
            get
            {
                if (erinfo != 0)
                {
                    Refreshmessage(adminAccess);
                    RaisePropertyChanged("AddPasswordMessage");
                }
                return adminAccess;
            }
            set
            {
                adminAccess = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AdminAccess"));
            }
        }

        private string versions;
        public string Versions
        {
            get
            {
                return versions;
            }
            set
            {
                versions = value;
                RaisePropertyChanged("Versions");
            }
        }



        private static PassErrorMessages _AddPasswordMessage = new PassErrorMessages();
        public PassErrorMessages AddPasswordMessage
        {
            get
            {
                return _AddPasswordMessage;
            }
            set
            {
                _AddPasswordMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddPasswordMessage"));
            }
        }


        private void NextMethod(AdminLoginClass obj)
        {
            try
            {
                erinfo = 1;
                Refreshmessage(obj);
                if (CheckErrorMessage.CheckErrorMessages)
                {
                    string confpwd = obj.pswd.Trim();
                    var data1 = sc.AdminLogins.FirstOrDefault();
                    if (data1 == null)
                    {
                        obj.uname = "Admin";
                        obj.pswd = sc.Encrypt(confpwd, "KKPrajapat");
                        obj.ConfirmPassword = sc.Encrypt(confpwd, "KKPrajapat");
                        sc.AdminLogins.Add(obj);
                        sc.SaveChanges();
                    }
                    else
                    {
                        data1.uname = "Admin";
                        data1.pswd = sc.Encrypt(confpwd, "KKPrajapat");
                        data1.ConfirmPassword = sc.Encrypt(confpwd, "KKPrajapat");
                        data1.Loginfo = obj.Loginfo;
                        data1.LastLogin = obj.LastLogin;
                        sc.Entry(data1).State = EntityState.Modified;
                        sc.SaveChanges();
                    }

                    var txtpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RecordWork49V2.txt");

                    if (File.Exists(txtpath) && new FileInfo(txtpath).Length > 0)
                    {

                        StreamReader srd = new StreamReader(txtpath);
                        string RecordWork49Data = srd.ReadLine();
                        string[] RecordWork = sc.Decrypt(RecordWork49Data, "KKPrajapat").Split(',');
                        srd.Close();


                        var txtpathLastLogin = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LastLoginV2.txt");
                        if (File.Exists(txtpathLastLogin))
                        {
                            StreamReader srd11 = new StreamReader(txtpathLastLogin);
                            string LastLoginData = srd11.ReadLine();
                            string[] LastLogin = sc.Decrypt(LastLoginData, "KKPrajapat").Split(',');

                            srd11.Close();

                            var data = sc.AdminLogins.FirstOrDefault();
                            data.uname = "Admin";
                            data.pswd = sc.Encrypt(confpwd, "KKPrajapat");
                            data.ConfirmPassword = sc.Encrypt(confpwd, "KKPrajapat");
                            data.Loginfo = LastLoginData;
                            data.LastLogin = Convert.ToDateTime(LastLogin[0].ToString());
                            data.productinfo = RecordWork49Data;
                            sc.Entry(data).State = EntityState.Modified;
                            sc.SaveChanges();
                        }
                        else
                        {

                            StreamWriter writer = new StreamWriter(txtpathLastLogin);
                            writer.Write(sc.Encrypt(RecordWork[0].ToString() + "," + RecordWork[1].ToString(), "KKPrajapat"));
                            writer.Close();


                            string LastLoginData = sc.Encrypt(RecordWork[0].ToString() + "," + RecordWork[1].ToString(), "KKPrajapat");

                            var data = sc.AdminLogins.FirstOrDefault();
                            data.uname = "Admin";
                            data.pswd = sc.Encrypt(confpwd, "KKPrajapat");
                            data.ConfirmPassword = sc.Encrypt(confpwd, "KKPrajapat");
                            data.Loginfo = LastLoginData;
                            data.LastLogin = Convert.ToDateTime(RecordWork[0].ToString());
                            data.productinfo = RecordWork49Data;
                            sc.Entry(data).State = EntityState.Modified;
                            sc.SaveChanges();
                        }

                        CheckErrorMessage.CheckErrorMessages1 = true;


                    }
                    else
                    {
                        // Here call License key form...
                       // CheckErrorMessage.CheckErrorMessages1 = false;
                    }



                    //CheckErrorMessage.CheckErrorMessages = false;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void Refreshmessage(AdminLoginClass cdc)
        {
            CheckErrorMessage.CheckErrorMessages = false;
            PassErrorMessages m = (AddPasswordMessage as PassErrorMessages); //DownCasting.....

            if (string.IsNullOrEmpty(cdc.pswd))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.PasswordMessage = "Please Enter  Password";
                RaisePropertyChanged("AddPasswordMessage");
            }
            else
            {
               
                if (cdc.pswd.Length < 6)
                {
                    CheckErrorMessage.CheckErrorMessages = false;
                    m.PasswordMessage = "Password should be minimum 6 characters";
                    RaisePropertyChanged("AddPasswordMessage");

                }
                else
                {
                    //CheckErrorMessage.CheckErrorMessages = true;
                    //m.PasswordMessage = string.Empty;
                    //RaisePropertyChanged("AddPasswordMessage");

                    if (!string.IsNullOrEmpty(cdc.ConfirmPassword))
                    {
                        if (cdc.pswd == cdc.ConfirmPassword)
                        {
                            CheckErrorMessage.CheckErrorMessages = true;
                            m.ConfPasswordMessage = string.Empty;
                            RaisePropertyChanged("AddPasswordMessage");
                        }
                        else
                        {
                            CheckErrorMessage.CheckErrorMessages = false;
                            m.ConfPasswordMessage = "Password doesn't match to confirmpassword";
                            RaisePropertyChanged("AddPasswordMessage");
                        }

                    }
                    else
                    {
                        CheckErrorMessage.CheckErrorMessages = false;
                        m.ConfPasswordMessage = "Please Enter Confirm Password";
                        RaisePropertyChanged("AddPasswordMessage");
                    }


                    /*
                    if (!string.IsNullOrEmpty(cdc.pswd))
                    {
                        CheckErrorMessage.CheckErrorMessages = true;
                        m.PasswordMessage = string.Empty;
                        RaisePropertyChanged("AddPasswordMessage");
                    }
                    else
                    {
                        CheckErrorMessage.CheckErrorMessages = false;
                        m.PasswordMessage = "Please Enter  Password";
                        RaisePropertyChanged("AddPasswordMessage");
                    }

                    if (!string.IsNullOrEmpty(cdc.ConfirmPassword))
                    {
                        if (cdc.pswd == cdc.ConfirmPassword)
                        {
                            CheckErrorMessage.CheckErrorMessages = true;
                            m.ConfPasswordMessage = string.Empty;
                            RaisePropertyChanged("AddPasswordMessage");
                        }
                        else
                        {
                            CheckErrorMessage.CheckErrorMessages = false;
                            m.ConfPasswordMessage = "Password doesn't match to confirmpassword";
                            RaisePropertyChanged("AddPasswordMessage");
                        }

                    }
                    else
                    {
                        CheckErrorMessage.CheckErrorMessages = false;
                        m.ConfPasswordMessage = "Please Enter Confirm Password";
                        RaisePropertyChanged("AddPasswordMessage");
                    }
                    */
                }
            }
        }




        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }
}
