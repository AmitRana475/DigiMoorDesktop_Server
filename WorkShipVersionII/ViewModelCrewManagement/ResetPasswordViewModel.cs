using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System;
using System.Data.Entity;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class ResetPasswordViewModel : ViewModelBase
    {
        private static ShipmentContaxt sc;

        public ResetPasswordViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            saveCommand = new RelayCommand<ResetPasswordClass>(SaveMethod);
            //resetCommand = new RelayCommand(ResetMethod);
            listVisible = "Collapsed";

        }




        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }

        }

        //private ICommand resetCommand;
        //public ICommand ResetCommand
        //{
        //    get { return resetCommand; }

        //}

        private void ResetMethod()
        {
            erinfo = 0;
            CheckErrorMessage.CheckErrorMessages = false;
            PasswordAccess = new ResetPasswordClass();
            RaisePropertyChanged("PasswordAccess");
            AddCrewMessage = new PassErrorMessages();
            RaisePropertyChanged("AddCrewMessage");
            AutoHODName = string.Empty;
            RaisePropertyChanged("AutoHODName");
        }
        private void SaveMethod(ResetPasswordClass obj)
        {
            try
            {

                erinfo = 1;
                refreshmessage(obj);
                refreshmessage1(obj);
                if (CheckErrorMessage.CheckErrorMessages)
                {

                    //.........have to make function for update password


                    var local = sc.Set<CrewDetailClass>()
                         .Local
                         .FirstOrDefault(f => f.Id == obj.Uid);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }

                    var user = new CrewDetailClass() { Id = obj.Uid, pswd = obj.Password };

                    sc.CrewDetails.Attach(user);
                    sc.Entry(user).Property(x => x.pswd).IsModified = true;
                    sc.SaveChanges();

                    //................
                    MessageBox.Show("Password has been changed successfully");
                    ResetMethod();

                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }


        private ObservableCollection<string> GetCrewName(string autoHODName)
        {
            var CrewNames = new ObservableCollection<string>();
            var data = sc.CrewDetails.Where(x => x.name.ToLower().Contains(autoHODName.Trim().ToLower())).Select(x => new { x.name, x.UserName }).ToList();

            foreach (var item in data)
            {
                CrewNames.Add(item.name);

            }
            var dat = sc.CrewDetails.Where(x => x.name.ToLower().Equals(autoHODName.Trim().ToLower())).Select(x => new { x.Id, x.UserName }).FirstOrDefault();
            if (dat != null)
            {
                PasswordAccess.UserName = dat.UserName;
                PasswordAccess.Uid = dat.Id;
            }
            else
            {
                PasswordAccess.UserName =string.Empty;
                PasswordAccess.Uid = 0;
            }

            if (CrewNames.Count > 0)
            {

                ListVisible = "Visible";
            }
            else
            {
                ListVisible = "Collapsed";

            }

            RaisePropertyChanged("PasswordAccess");
            return CrewNames;
        }

        private void refreshmessage(ResetPasswordClass cdc)
        {

            CheckErrorMessage.CheckErrorMessages = false;
            PassErrorMessages m = (AddCrewMessage as PassErrorMessages); //DownCasting.....

            if (!string.IsNullOrEmpty(cdc.FullName))
            {
                CheckErrorMessage.CheckErrorMessages = true;
                m.FullNameMessage = string.Empty;
                RaisePropertyChanged("AddCrewMessage");
            }

            if (!string.IsNullOrEmpty(cdc.UserName))
            {
                CheckErrorMessage.CheckErrorMessages = true;
                m.UserNameMessage = string.Empty;
                RaisePropertyChanged("AddCrewMessage");
            }

            if (!string.IsNullOrEmpty(cdc.Password))
            {
                CheckErrorMessage.CheckErrorMessages = true;
                m.PasswordMessage = string.Empty;
                RaisePropertyChanged("AddCrewMessage");
            }

            if (!string.IsNullOrEmpty(cdc.ConfirmPassword))
            {


                if (cdc.Password == cdc.ConfirmPassword)
                {
                    CheckErrorMessage.CheckErrorMessages = true;
                    m.ConfPasswordMessage = string.Empty;
                    RaisePropertyChanged("AddCrewMessage");
                }
                else
                {
                    CheckErrorMessage.CheckErrorMessages = false;
                    m.ConfPasswordMessage = "Password doesn't match to confirmpassword";
                    RaisePropertyChanged("AddCrewMessage");
                }

            }

        }

        private void refreshmessage1(ResetPasswordClass cdc)
        {

            CheckErrorMessage.CheckErrorMessages = true;

            PassErrorMessages m = (AddCrewMessage as PassErrorMessages); //DownCasting.....

            if (string.IsNullOrEmpty(cdc.FullName))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.FullNameMessage = "Please Enter Full Name";
                RaisePropertyChanged("AddCrewMessages");
            }

            if (string.IsNullOrEmpty(cdc.UserName))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.UserNameMessage = "User Name is required";
                RaisePropertyChanged("AddCrewMessages");
            }

            if (string.IsNullOrEmpty(cdc.Password))
            {
                CheckErrorMessage.CheckErrorMessages = false;
                m.PasswordMessage = "Please Enter  Password";
                RaisePropertyChanged("AddCrewMessage");
            }

            if (string.IsNullOrEmpty(cdc.ConfirmPassword))
            {

                CheckErrorMessage.CheckErrorMessages = false;
                m.ConfPasswordMessage = "Please Enter Confirm Password";
                RaisePropertyChanged("AddCrewMessage");

            }

            if (!string.IsNullOrEmpty(cdc.ConfirmPassword))
            {

                if (!string.IsNullOrEmpty(cdc.Password))
                {
                    if (cdc.Password.Equals(cdc.ConfirmPassword))
                    {

                        //CheckErrorMessage.CheckErrorMessages = true;
                        //m.ConfirmPasswordMessage = string.Empty;
                        //RaisePropertyChanged("AddCrewMessages");
                    }
                    else
                    {
                        CheckErrorMessage.CheckErrorMessages = false;
                        m.ConfPasswordMessage = "Password doesn't match to confirmpassword";
                        RaisePropertyChanged("AddCrewMessage");
                    }
                }
            }

        }

        private static ResetPasswordClass passwordAccess = new ResetPasswordClass();
        public ResetPasswordClass PasswordAccess
        {
            get
            {
                if (erinfo == 1)
                {
                    refreshmessage(passwordAccess);
                    refreshmessage1(passwordAccess);
                    RaisePropertyChanged("AddCrewMessage");
                }
                else if (erinfo == 2)
                {
                    ResetMethod();
                }
                return passwordAccess;
            }
            set
            {
                passwordAccess = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PasswordAccess"));
            }
        }



        private static PassErrorMessages _AddCrewMessage = new PassErrorMessages();
        public PassErrorMessages AddCrewMessage
        {
            get
            {
                return _AddCrewMessage;
            }
            set
            {
                _AddCrewMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddCrewMessage"));
            }
        }

        private static string autoHODName;
        public string AutoHODName
        {
            get
            {
                if (autoHODName != null)
                {
                    PasswordAccess.FullName = autoHODName;
                    AutoCrewNames = GetCrewName(autoHODName);
                }
                return autoHODName;
            }

            set
            {
                autoHODName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AutoHODName"));
            }
        }

        public static int erinfo { get; set; }

        private static ObservableCollection<string> autoCrewNames = new ObservableCollection<string>();
        public ObservableCollection<string> AutoCrewNames
        {
            get
            {
                return autoCrewNames;
            }
            set
            {
                autoCrewNames = value;
                RaisePropertyChanged("AutoCrewNames");

            }
        }




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

        //............LogInfo.............

        //string st1 = "insert into InfoLog (dt,UserName,ModuleName,ActionName,Description) values (@dt,@name,@module,@action,@desc)";
        //SqlDataAdapter sdalog = new SqlDataAdapter(st1, con);
        //sdalog.SelectCommand.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTime.Now.ToString();
        //sdalog.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = Class1.UserTypes;
        //sdalog.SelectCommand.Parameters.Add("@module", SqlDbType.VarChar, 500).Value = "Freeze/UnFreeze";
        //sdalog.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar, -1).Value = "UnFreeze From " + dtfrom.ToShortDateString() + " To " + dtTo.ToShortDateString() + "";
        //sdalog.SelectCommand.Parameters.Add("@desc", SqlDbType.VarChar, -1).Value = Class1.AccessUserName;
        //DataTable dtlog = new DataTable();
        //sdalog.Fill(dtlog);
        //......end LogInfo...............



        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }


}
