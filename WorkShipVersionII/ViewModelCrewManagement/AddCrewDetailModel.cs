using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WorkShipVersionII.ViewModelCrewManagement
{
       public class AddCrewDetailModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;


              TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;

              public AddCrewDetailModel(CrewDetailClass cdc)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     try
                     {
                            erinfo = 0;
                            StaticHelper.Editing = true;
                            saveCommand = new RelayCommand<CrewDetailClass>(SaveMethod);
                            cancelCommand = new RelayCommand(cancelCrewDetail);
                            resetCommand = new RelayCommand(resetCrewDetail);
                            RadioBTNCommand = new RelayCommand<object>(RadioBTNmethod);
                            lostFocusCommand = new RelayCommand<object>(CheckUserMethod);
                            lostFocusCDCCommand = new RelayCommand<object>(CheckCDCMethod);
                            lostFocusEmpCommand = new RelayCommand<object>(CheckempnoMethod);

                            CrewDetailClass data = sc.CrewDetails.Where(x => x.Id == cdc.Id).FirstOrDefault();
                            StaticHelper.Wathckeeping = data.WatchKeeper;
                            AddCrewDetail = new CrewDetailClass()
                            {
                                   Id = data.Id,
                                   name = data.name,
                                   UserName = data.UserName,
                                   position = data.position,
                                   department = data.department,
                                   ServiceFrom = data.ServiceFrom,
                                   ServiceTo = data.ServiceTo,
                                   CDC = data.CDC,
                                   empno = data.empno,
                                   pswd = data.pswd,
                                   Confirmpswd = data.pswd,
                                   comments = data.comments,
                                   overtime = data.overtime,
                                   SeaWk = data.SeaWk,
                                   SeaNWK = data.SeaNWK,
                                   PortWk = data.PortWk,
                                   PortNWK = data.PortNWK,
                                   dates = data.dates,
                                   SeaWH = data.SeaWH,
                                   portWH = data.portWH,
                                   did = data.did,
                                   rid = data.rid,
                                   SeaWk1 = data.SeaWk1,
                                   SeaNWK1 = data.SeaNWK1,
                                   PortWk1 = data.PortWk1,
                                   PortNWK1 = data.PortNWK1,
                                   OpaStatus = data.OpaStatus,
                                   CertificateView = data.CertificateView,
                                   CertificateAdd = data.CertificateAdd,
                                   CertificateEdit = data.CertificateEdit,
                                   CertificateDelete = data.CertificateDelete,
                                   chkyoungs = data.chkyoungs,
                                   SeaWkYoung = data.SeaWkYoung,
                                   SeaNWKYoung = data.SeaNWKYoung,
                                   PortWkYoung = data.PortWkYoung,
                                   PortNWKYoung = data.PortNWKYoung,
                                   Remarks = data.Remarks,
                                   WatchKeeper = data.WatchKeeper,
                                   WatchKeeper1 = data.WatchKeeper == true ? false : true


                            };

                            if (AddCrewDetail.chkyoungs)
                            {
                                   MyDOB = data.DOB;
                                   RaisePropertyChanged("MyDOB");
                            }


                            RaisePropertyChanged("AddCrewDetail");

                            SRankName = data.position;
                            SDepartmentName = data.department;

                            MyServiceFrom = data.ServiceFrom;
                            RaisePropertyChanged("MyServiceFrom");
                            MyServiceTo = data.ServiceTo;
                            RaisePropertyChanged("MyServiceTo");

                            ChView = data.CertificateView;
                            ChAdd = data.CertificateAdd;
                            ChEdit = data.CertificateEdit;
                            ChDelete = data.CertificateDelete;


                            OnPropertyChanged(new PropertyChangedEventArgs("AddCrewDetail"));

                            refreshmessage(AddCrewDetail);

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }


              }



              public AddCrewDetailModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     try
                     {
                            //StaticHelper.Editing = true;
                            //.....Refresh Properies.........
                            erinfo = 2;
                            AddCrewMessages = new AddCrewErrorMessages();
                            OnPropertyChanged(new PropertyChangedEventArgs("AddCrewMessages"));

                            AddCrewDetail = new CrewDetailClass();
                            OnPropertyChanged(new PropertyChangedEventArgs("AddCrewDetail"));

                            //.............


                            saveCommand = new RelayCommand<CrewDetailClass>(SaveMethod);
                            cancelCommand = new RelayCommand(cancelCrewDetail);
                            resetCommand = new RelayCommand(resetCrewDetail);
                            RadioBTNCommand = new RelayCommand<object>(RadioBTNmethod);
                            lostFocusCommand = new RelayCommand<object>(CheckUserMethod);
                            lostFocusCDCCommand = new RelayCommand<object>(CheckCDCMethod);
                            lostFocusEmpCommand = new RelayCommand<object>(CheckempnoMethod);
                            StaticHelper.Wathckeeping = true;
                            rankname.Clear();
                            rankname = GetRank();
                            departmentname = GetDepartment();
                            resetCrewDetail();


                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }


              public static int erinfo { get; set; }
              public ICommand RadioBTNCommand { get; private set; }

              private ICommand saveCommand;
              public ICommand SaveCommand
              {
                     get { return saveCommand; }
              }

              private ICommand cancelCommand;
              public ICommand CancelCommand
              {
                     get { return cancelCommand; }
              }

              private ICommand resetCommand;
              public ICommand ResetCommand
              {
                     get { return resetCommand; }
              }

              private ICommand lostFocusCommand;
              public ICommand LostFocusCommand
              {
                     get { return lostFocusCommand; }
              }

              private ICommand lostFocusCDCCommand;
              public ICommand LostFocusCDCCommand
              {
                     get { return lostFocusCDCCommand; }
              }

              private ICommand lostFocusEmpCommand;
              public ICommand LostFocusEmpCommand
              {
                     get { return lostFocusEmpCommand; }
              }

              private static string srankname;
              public string SRankName
              {
                     get
                     {
                            if (srankname != null)
                                   AddCrewDetail.position = srankname;
                            if (srankname == "MASTER")
                                   AddCrewDetail.department = "All";

                            return srankname;
                     }

                     set
                     {
                            srankname = value;
                            if (srankname != null)
                                   AddCrewDetail.position = srankname;
                            OnPropertyChanged(new PropertyChangedEventArgs("SRankName"));
                     }
              }

              private static string sdepartmentname;
              public string SDepartmentName
              {
                     get
                     {
                            if (sdepartmentname != null)
                                   AddCrewDetail.department = sdepartmentname;
                            if (srankname == "MASTER")
                                   AddCrewDetail.department = "All";

                            return sdepartmentname;
                     }

                     set
                     {
                            sdepartmentname = value;
                            if (sdepartmentname != null)
                                   AddCrewDetail.department = sdepartmentname;
                            OnPropertyChanged(new PropertyChangedEventArgs("SDepartmentName"));
                     }
              }

              private static ObservableCollection<string> departmentname = new ObservableCollection<string>();
              public ObservableCollection<string> DepartmentName
              {
                     get
                     {
                            return departmentname;
                     }
                     set
                     {
                            departmentname = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("DepartmentName"));
                     }
              }


              private static ObservableCollection<string> rankname = new ObservableCollection<string>();
              public ObservableCollection<string> RankName
              {
                     get
                     {
                            return rankname;
                     }
                     set
                     {
                            rankname = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("RankName"));
                     }
              }

              private ObservableCollection<string> GetRank()
              {
                     var Ranks = new ObservableCollection<string>();
                     var data = sc.CrewRanks.OrderBy(s => s.SrNo).Select(x => x.Rank).ToList();

                     foreach (var item in data)
                     {
                            Ranks.Add(item);
                     }

                     return Ranks;
              }

              private ObservableCollection<string> GetDepartment()
              {
                     var AddDepartments = new ObservableCollection<string>();
                     var data = sc.Departments.OrderBy(s => s.did).Select(x => x.DeptName).ToList();

                     foreach (var item in data)
                     {
                            AddDepartments.Add(item);

                     }

                     return AddDepartments;
              }
              private static AddCrewErrorMessages _AddCrewMessages = new AddCrewErrorMessages();
              public AddCrewErrorMessages AddCrewMessages
              {
                     get
                     {
                            return _AddCrewMessages;
                     }
                     set
                     {
                            _AddCrewMessages = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("AddCrewMessages"));
                     }
              }

              private static CrewDetailClass _AddCrewDetail = new CrewDetailClass();
              public CrewDetailClass AddCrewDetail
              {
                     get
                     {
                            if (erinfo == 1)
                            {
                                   refreshmessage(_AddCrewDetail);
                                   refreshmessage1(_AddCrewDetail);
                                   RaisePropertyChanged("AddCrewMessages");
                            }
                            else if (erinfo == 2)
                            {
                                   AddCrewMessages = new AddCrewErrorMessages();
                                   OnPropertyChanged(new PropertyChangedEventArgs("AddCrewMessages"));
                                   AddCrewDetail = new CrewDetailClass();
                                   OnPropertyChanged(new PropertyChangedEventArgs("AddCrewDetail"));

                                   erinfo = 0;
                            }

                            return _AddCrewDetail;
                     }
                     set
                     {
                            _AddCrewDetail = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("AddCrewDetail"));



                     }
              }

              private static Nullable<DateTime> _MyServiceFrom = null;
              public Nullable<DateTime> MyServiceFrom
              {
                     get
                     {
                            if (_MyServiceFrom == null)
                            {
                                   _MyServiceFrom = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            }
                            _AddCrewDetail.ServiceFrom = (DateTime)_MyServiceFrom;
                            return _MyServiceFrom;
                     }
                     set
                     {
                            _MyServiceFrom = value;
                            RaisePropertyChanged("MyServiceFrom");
                     }
              }

              private Nullable<DateTime> _MyServiceTo = null;
              public Nullable<DateTime> MyServiceTo
              {
                     get
                     {
                            if (_MyServiceTo == null)
                            {
                                   _MyServiceTo = DateTime.Now;
                            }
                            _AddCrewDetail.ServiceTo = (DateTime)_MyServiceTo;
                            return _MyServiceTo;
                     }
                     set
                     {
                            _MyServiceTo = value;
                            RaisePropertyChanged("MyServiceTo");
                     }
              }

              private static Nullable<DateTime> _MyDOB = null;
              public Nullable<DateTime> MyDOB
              {
                     get
                     {
                            if (_MyDOB == null)
                            {
                                   _MyDOB = DateTime.Now;
                            }
                            _AddCrewDetail.DOB = (DateTime)_MyDOB;
                            return _MyDOB;
                     }
                     set
                     {
                            _MyDOB = value;
                            RaisePropertyChanged("MyDOB");
                     }
              }

              private static bool _ChView = false;
              public bool ChView
              {
                     get
                     {
                            if (_ChView == false)
                            {
                                   _ChAdd = false;
                                   _ChEdit = false;
                                   _ChDelete = false;
                                   RaisePropertyChanged("ChAdd");
                                   RaisePropertyChanged("ChEdit");
                                   RaisePropertyChanged("ChDelete");
                            }

                            _AddCrewDetail.CertificateView = _ChView;
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
                                   _ChView = true;
                                   RaisePropertyChanged("ChView");
                            }
                            _AddCrewDetail.CertificateAdd = _ChAdd;
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
                                   _ChView = true;
                                   RaisePropertyChanged("ChView");
                            }
                            _AddCrewDetail.CertificateEdit = _ChEdit;
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
                                   _ChView = true;
                                   RaisePropertyChanged("ChView");
                            }
                            _AddCrewDetail.CertificateDelete = _ChDelete;
                            return _ChDelete;
                     }
                     set
                     {
                            _ChDelete = value;
                            RaisePropertyChanged("ChDelete");
                     }
              }

              private string lblmessage;
              public string Lblmessage
              {
                     get
                     {
                            return lblmessage;
                     }
                     set
                     {
                            lblmessage = value;
                            RaisePropertyChanged("Lblmessage");
                     }
              }


              private void CheckempnoMethod(object obj)
              {
                     obj = obj == null ? string.Empty : obj;
                     if (AddCrewDetail.Id == 0)
                     {
                            var exists = (from users in sc.CrewDetails
                                          where users.empno.ToLower().Trim() == obj.ToString().Trim().ToLower()
                                          select users).FirstOrDefault();
                            if (exists != null)
                            {
                                   AddCrewMessages.EmpnoMessage = "This Code is already allotted to " + "'" + exists.name + "'";
                                   CheckErrorMessage.CheckErrorMessages = false;
                                   RaisePropertyChanged("AddCrewMessages");
                            }
                            else
                            {
                                   AddCrewMessages.EmpnoMessage = string.Empty;
                                   RaisePropertyChanged("AddCrewMessages");
                            }
                     }
              }
              private void CheckCDCMethod(object obj)
              {
                     obj = obj == null ? string.Empty : obj;
                     if (AddCrewDetail.Id == 0)
                     {
                            var exists = (from users in sc.CrewDetails
                                          where users.CDC.ToLower().Trim() == obj.ToString().Trim().ToLower()
                                          select users).FirstOrDefault();
                            if (exists != null)
                            {
                                   AddCrewMessages.SeamenMessage = "This ID is already allotted to " + "'" + exists.name + "'";
                                   CheckErrorMessage.CheckErrorMessages = false;
                                   RaisePropertyChanged("AddCrewMessages");
                            }
                            else
                            {
                                   AddCrewMessages.SeamenMessage = string.Empty;
                                   RaisePropertyChanged("AddCrewMessages");
                            }
                     }
              }

              private void CheckUserMethod(object obj)
              {
                     obj = obj == null ? string.Empty : obj;
                     if (AddCrewDetail.Id == 0)
                     {
                            bool exists = (from users in sc.CrewDetails
                                           where users.UserName.ToLower().Trim() == obj.ToString().Trim().ToLower()
                                           select users).Any();
                            if (exists)
                            {
                                   AddCrewMessages.UserNameMessage = "This user name already exist.";
                                   CheckErrorMessage.CheckErrorMessages = false;
                                   RaisePropertyChanged("AddCrewMessages");
                            }
                            else
                            {
                                   AddCrewMessages.UserNameMessage = string.Empty;
                                   RaisePropertyChanged("AddCrewMessages");
                            }
                     }

              }

              private void RadioBTNmethod(object parameter)
              {
                     var bb = (string)parameter;

                     if (bb == "WatchKeeper")
                     {
                            _AddCrewDetail.WatchKeeper = true;
                            _AddCrewDetail.WatchKeeper1 = false;
                            StaticHelper.Wathckeeping = true;
                     }
                     else
                     {
                            _AddCrewDetail.WatchKeeper = false;
                            _AddCrewDetail.WatchKeeper1 = true;
                            StaticHelper.Wathckeeping = false;
                     }

                     OnPropertyChanged(new PropertyChangedEventArgs("AddCrewDetail"));
              }


              private void refreshmessage(CrewDetailClass cdc1)
              {
                     CrewDetailClass cdc = cdc1;
                     int coun = 0;
                     CheckErrorMessage.CheckErrorMessages = true;

                     AddCrewErrorMessages m = (AddCrewMessages as AddCrewErrorMessages); //DownCasting.....

                     cdc.UserName = cdc.UserName != null ? cdc.UserName.Trim() : string.Empty;
                     cdc.name = cdc.name != null ? cdc.name.Trim() : string.Empty;
                     cdc.position = cdc.position != null ? cdc.position.Trim() : string.Empty;
                     cdc.department = cdc.department != null ? cdc.department.Trim() : string.Empty;
                     cdc.CDC = cdc.CDC != null ? cdc.CDC.Trim() : string.Empty;
                     cdc.pswd = cdc.pswd != null ? cdc.pswd.Trim() : string.Empty;
                     cdc.Confirmpswd = cdc.Confirmpswd != null ? cdc.Confirmpswd.Trim() : string.Empty;

                     if (!string.IsNullOrEmpty(cdc.UserName))
                     {
                            if (cdc.UserName.ToLower() == "admin")
                            {
                                   CheckErrorMessage.CheckErrorMessages = false;
                                   m.UserNameMessage = "This user name already already exist.";
                                   RaisePropertyChanged("AddCrewMessages");
                            }
                            else
                            {
                                   coun++;
                                   CheckErrorMessage.CheckErrorMessages = true;
                                   m.UserNameMessage = string.Empty;
                                   RaisePropertyChanged("AddCrewMessages");

                            }


                     }
                     if (!string.IsNullOrEmpty(cdc.name))
                     {
                            coun++;
                            CheckErrorMessage.CheckErrorMessages = true;
                            m.CrewNameMessage = string.Empty;
                            RaisePropertyChanged("AddCrewMessages");
                     }
                     if (!string.IsNullOrEmpty(cdc.position))
                     {
                            coun++;
                            CheckErrorMessage.CheckErrorMessages = true;
                            m.RankMessage = string.Empty;
                            RaisePropertyChanged("AddCrewMessages");
                     }
                     if (!string.IsNullOrEmpty(cdc.department))
                     {
                            coun++;
                            CheckErrorMessage.CheckErrorMessages = true;
                            m.DepartmentMessage = string.Empty;
                            RaisePropertyChanged("AddCrewMessages");
                     }
                     if (!string.IsNullOrEmpty(cdc.CDC))
                     {
                            coun++;
                            CheckErrorMessage.CheckErrorMessages = true;
                            m.SeamenMessage = string.Empty;
                            RaisePropertyChanged("AddCrewMessages");
                     }
                     if (!string.IsNullOrEmpty(cdc.pswd))
                     {
                            coun++;
                            CheckErrorMessage.CheckErrorMessages = true;
                            m.PasswordMessage = string.Empty;
                            RaisePropertyChanged("AddCrewMessages");
                     }
                     if (!string.IsNullOrEmpty(cdc.Confirmpswd))
                     {

                            if (!string.IsNullOrEmpty(cdc.pswd))
                            {
                                   if (cdc.pswd.Equals(cdc.Confirmpswd))
                                   {
                                          coun++;
                                          CheckErrorMessage.CheckErrorMessages = true;
                                          m.ConfirmPasswordMessage = string.Empty;
                                          RaisePropertyChanged("AddCrewMessages");
                                   }
                                   else
                                   {
                                          CheckErrorMessage.CheckErrorMessages = false;
                                          m.ConfirmPasswordMessage = "Password doesn't match to confirmpassword";
                                          RaisePropertyChanged("AddCrewMessages");
                                   }
                            }
                            RaisePropertyChanged("AddCrewMessages");
                     }

                     if (coun.Equals(7))
                            CheckErrorMessage.CheckErrorMessages = true;
                     else
                            CheckErrorMessage.CheckErrorMessages = false;
              }
              private void refreshmessage1(CrewDetailClass cdc1)
              {
                     CrewDetailClass cdc = cdc1;
                     CheckErrorMessage.CheckErrorMessages = true;

                     AddCrewErrorMessages m = (AddCrewMessages as AddCrewErrorMessages); //DownCasting.....

                     cdc.UserName = cdc.UserName != null ? cdc.UserName.Trim() : string.Empty;
                     cdc.name = cdc.name != null ? cdc.name.Trim() : string.Empty;
                     cdc.position = cdc.position != null ? cdc.position.Trim() : string.Empty;
                     cdc.department = cdc.department != null ? cdc.department.Trim() : string.Empty;
                     cdc.CDC = cdc.CDC != null ? cdc.CDC.Trim() : string.Empty;
                     cdc.pswd = cdc.pswd != null ? cdc.pswd.Trim() : string.Empty;
                     cdc.Confirmpswd = cdc.Confirmpswd != null ? cdc.Confirmpswd.Trim() : string.Empty;


                     if (string.IsNullOrEmpty(cdc.UserName))
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            m.UserNameMessage = "Please Enter User Name";
                            RaisePropertyChanged("AddCrewMessages");
                     }
                     if (string.IsNullOrEmpty(cdc.name))
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            m.CrewNameMessage = "Please Enter Crew Name";
                            RaisePropertyChanged("AddCrewMessages");
                     }
                     if (string.IsNullOrEmpty(cdc.position))
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            m.RankMessage = "Please Select Position / Rank";
                            RaisePropertyChanged("AddCrewMessages");
                     }
                     if (string.IsNullOrEmpty(cdc.department))
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            m.DepartmentMessage = "Please Select Department";
                            RaisePropertyChanged("AddCrewMessages");
                     }
                     if (string.IsNullOrEmpty(cdc.CDC))
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            m.SeamenMessage = "Please Enter Seamen BookId";
                            RaisePropertyChanged("AddCrewMessages");
                     }
                     if (string.IsNullOrEmpty(cdc.pswd))
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            m.PasswordMessage = "Please Enter Password";
                            RaisePropertyChanged("AddCrewMessages");
                     }
                     if (string.IsNullOrEmpty(cdc.Confirmpswd))
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            m.ConfirmPasswordMessage = "Please Enter Confirm Password";
                            RaisePropertyChanged("AddCrewMessages");
                     }

                     if (!string.IsNullOrEmpty(cdc.Confirmpswd))
                     {

                            if (!string.IsNullOrEmpty(cdc.pswd))
                            {
                                   if (cdc.pswd.Equals(cdc.Confirmpswd))
                                   {

                                          //CheckErrorMessage.CheckErrorMessages = true;
                                          //m.ConfirmPasswordMessage = string.Empty;
                                          //RaisePropertyChanged("AddCrewMessages");
                                   }
                                   else
                                   {
                                          CheckErrorMessage.CheckErrorMessages = false;
                                          m.ConfirmPasswordMessage = "Password doesn't match to confirmpassword";
                                          RaisePropertyChanged("AddCrewMessages");
                                   }
                            }
                            RaisePropertyChanged("AddCrewMessages");
                     }


              }

              private void cancelCrewDetail()
              {
                     try
                     {
                          
                            erinfo = 0;
                            resetCrewDetail();
                            CheckErrorMessage.CheckErrorMessages = false;
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              public void resetCrewDetail()
              {
                     try
                     {
                            StaticHelper.Wathckeeping = true;
                            StaticHelper.Editing = false;

                            erinfo = 0;
                            AddCrewDetail = new CrewDetailClass();
                            RaisePropertyChanged("AddCrewDetail");

                            AddCrewMessages = new AddCrewErrorMessages();
                            RaisePropertyChanged("AddCrewMessages");

                            ChView = false; RaisePropertyChanged("ChView");
                            ChAdd = false; RaisePropertyChanged("ChAdd");
                            ChEdit = false; RaisePropertyChanged("ChEdit");
                            ChDelete = false; RaisePropertyChanged("ChDelete");
                            MyServiceFrom = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("MyServiceFrom");
                            MyServiceTo = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("MyServiceTo");

                            SRankName = null; RaisePropertyChanged("SRankName");
                            SDepartmentName = null; RaisePropertyChanged("SDepartmentName");
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }
              private void SaveMethod(CrewDetailClass cdc)
              {
                     try
                     {

                            erinfo = 1;
                            refreshmessage1(cdc);
                            if (!string.IsNullOrEmpty(Lblmessage) && cdc.chkyoungs)
                                   CheckErrorMessage.CheckErrorMessages = false;

                            if (CheckErrorMessage.CheckErrorMessages)
                            {
                                   if (cdc.ServiceFrom.ToShortDateString() == cdc.ServiceTo.ToShortDateString())
                                   {
                                          if (MessageBox.Show("Sure, you want to keep same day of service term?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                          {

                                                 if (AddCrewDetail.Id != 0)
                                                 {

                                                        if ((cdc.chkyoungs == true) && CheckFromDate16(cdc.DOB, cdc.ServiceFrom) == true)
                                                        {

                                                               MessageBox.Show("As per Service Term From Date, user may be below 16 years, hence cannot be termed as Young Seafarer.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                                                               CheckErrorMessage.CheckErrorMessages = false;
                                                        }
                                                        else
                                                        {
                                                               cdc.did = sc.Departments.Where(x => x.DeptName == cdc.department).Select(p => p.did).FirstOrDefault();
                                                               cdc.rid = sc.CrewRanks.Where(x => x.Rank == cdc.position).Select(p => p.cid).FirstOrDefault();
                                                               cdc.dates = DateTime.Now;
                                                               cdc.name = textinfo.ToTitleCase(cdc.name.ToLower());
                                                               if (cdc.chkyoungs)
                                                               {
                                                                      CheckErrorMessage.chkyoungs = true;
                                                                      new AddNextYoungCrewDetailModel(cdc);
                                                               }
                                                               else
                                                               {
                                                                      CheckErrorMessage.chkyoungs = false;
                                                                      new AddNextCrewDetailModel(cdc);
                                                               }
                                                        }
                                                 }
                                                 else
                                                 {
                                                        if ((cdc.chkyoungs == true) && CheckDate(cdc.DOB, cdc.ServiceFrom) == false)
                                                        {

                                                               MessageBox.Show("As per Service Term From Date, user may be below 16 years and above 18 years, hence cannot be termed as Young Seafarer.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                                                               CheckErrorMessage.CheckErrorMessages = false;
                                                        }
                                                        else
                                                        {
                                                               cdc.did = sc.Departments.Where(x => x.DeptName == cdc.department).Select(p => p.did).FirstOrDefault();
                                                               cdc.rid = sc.CrewRanks.Where(x => x.Rank == cdc.position).Select(p => p.cid).FirstOrDefault();
                                                               cdc.dates = DateTime.Now;
                                                               cdc.name = textinfo.ToTitleCase(cdc.name.ToLower());
                                                               if (cdc.chkyoungs)
                                                               {
                                                                      CheckErrorMessage.chkyoungs = true;
                                                                      new AddNextYoungCrewDetailModel(cdc);
                                                               }
                                                               else
                                                               {
                                                                      CheckErrorMessage.chkyoungs = false;
                                                                      new AddNextCrewDetailModel(cdc);
                                                               }
                                                        }
                                                 }


                                          }
                                          else
                                          {
                                                 CheckErrorMessage.CheckErrorMessages = false;
                                          }
                                   }
                                   else
                                   {
                                          if (AddCrewDetail.Id != 0)
                                          {
                                                 if ((cdc.chkyoungs == true) && CheckFromDate16(cdc.DOB, cdc.ServiceFrom) == true)
                                                 {

                                                        MessageBox.Show("As per Service Term From Date, user may be below 16 years, hence cannot be termed as Young Seafarer.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                                                        CheckErrorMessage.CheckErrorMessages = false;
                                                 }

                                                 else
                                                 {
                                                        cdc.did = sc.Departments.Where(x => x.DeptName == cdc.department).Select(p => p.did).FirstOrDefault();
                                                        cdc.rid = sc.CrewRanks.Where(x => x.Rank == cdc.position).Select(p => p.cid).FirstOrDefault();
                                                        cdc.dates = DateTime.Now;
                                                        cdc.name = textinfo.ToTitleCase(cdc.name.ToLower());
                                                        if (cdc.chkyoungs)
                                                        {
                                                               CheckErrorMessage.chkyoungs = true;
                                                               new AddNextYoungCrewDetailModel(cdc);
                                                        }
                                                        else
                                                        {
                                                               CheckErrorMessage.chkyoungs = false;
                                                               new AddNextCrewDetailModel(cdc);
                                                        }
                                                 }

                                          }
                                          else
                                          {
                                                 if ((cdc.chkyoungs == true) && CheckDate(cdc.DOB, cdc.ServiceFrom) == false)
                                                 {

                                                        MessageBox.Show("As per Service Term From Date, user may be below 16 years and above 18, hence cannot be termed as Young Seafarer", "", MessageBoxButton.OK, MessageBoxImage.Error);
                                                        CheckErrorMessage.CheckErrorMessages = false;
                                                 }
                                                 else
                                                 {
                                                        cdc.did = sc.Departments.Where(x => x.DeptName == cdc.department).Select(p => p.did).FirstOrDefault();
                                                        cdc.rid = sc.CrewRanks.Where(x => x.Rank == cdc.position).Select(p => p.cid).FirstOrDefault();
                                                        cdc.dates = DateTime.Now;
                                                        cdc.name = textinfo.ToTitleCase(cdc.name.ToLower());
                                                        if (cdc.chkyoungs)
                                                        {
                                                               CheckErrorMessage.chkyoungs = true;
                                                               new AddNextYoungCrewDetailModel(cdc);
                                                        }
                                                        else
                                                        {
                                                               CheckErrorMessage.chkyoungs = false;
                                                               new AddNextCrewDetailModel(cdc);
                                                        }
                                                 }
                                          }
                                   }
                            }
                            // have to make save function here.......

                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }


              }

              public bool CheckDate(DateTime d1, DateTime d2)
              {
                     //var saa = d2.AddYears(-18) < d1;
                     var saa = d2.AddYears(-16) >= d1 && d2.AddYears(-18) < d1;
                     return saa;
              }

              public bool CheckFromDate16(DateTime d1, DateTime d2)
              {
                     var saa1 = d2.AddYears(-16) <= d1;
                     return saa1;
              }

              new public event PropertyChangedEventHandler PropertyChanged;
              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }


       }



       public class AddCrewErrorMessages
       {
              public string UserNameMessage { get; set; }
              public string CrewNameMessage { get; set; }
              public string RankMessage { get; set; }
              public string DepartmentMessage { get; set; }
              public string EmpnoMessage { get; set; }
              public string SeamenMessage { get; set; }
              public string DateFromMessage { get; set; }
              public string DateToMessage { get; set; }
              public string PasswordMessage { get; set; }
              public string ConfirmPasswordMessage { get; set; }

       }

       public static class CheckErrorMessage
       {
              public static bool CheckErrorMessages { get; set; }
              public static bool CheckErrorMessages1 { get; set; }
              public static bool CheckErrorMessages2 { get; set; }
              public static bool chkyoungs { get; set; }

       }

}


