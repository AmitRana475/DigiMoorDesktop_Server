using GalaSoft.MvvmLight;
using System;
using System.Linq;
using DataBuildingLayer;
using System.Data.Entity;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;
using System.Globalization;
using GalaSoft.MvvmLight.Command;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.ViewModelCrewManagement
{
       public class AddNextYoungCrewDetailModel : ViewModelBase, IValueConverter, IWkShedule
       {
              private readonly ShipmentContaxt sc;
              TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
              public AddNextYoungCrewDetailModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }


                     saveCommand = new RelayCommand<CrewDetailClass>(SaveMethod);
                     overtimeCommand = new RelayCommand<CrewDetailClass>(OverTimeMethod);

              }

              public AddNextYoungCrewDetailModel(CrewDetailClass cdc)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     //Youngs = "True";
                     //RaisePropertyChanged("Youngs");

                     var swh = System.Convert.ToDouble(cdc.SeaWH);
                     var pwh = System.Convert.ToDouble(cdc.portWH);
                     cdc.SeaWH = System.Convert.ToDecimal(swh);
                     cdc.portWH = System.Convert.ToDecimal(pwh);
                     AddNextCrewDetail = cdc;
                     OnPropertyChanged(new PropertyChangedEventArgs("AddNextCrewDetail"));

              }


              private static CrewDetailClass _AddNextCrewDetail = new CrewDetailClass();
              public CrewDetailClass AddNextCrewDetail
              {
                     get
                     {
                            //refreshmessage(_AddCrewDetail);
                            //OnPropertyChanged(new PropertyChangedEventArgs("AddCrewMessages"));
                            return _AddNextCrewDetail;
                     }
                     set
                     {
                            _AddNextCrewDetail = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("AddNextCrewDetail"));



                     }
              }
              private ICommand saveCommand;
              public ICommand SaveCommand
              {
                     get { return saveCommand; }
              }

              private ICommand overtimeCommand;
              public ICommand OverTimeCommand
              {
                     get { return overtimeCommand; }
              }


              private string young;
              public string Youngs
              {
                     get
                     {
                            var dt = DateTime.Now;
                            //var dt121 = (DateTime)(dt);
                            if (CheckDate(AddNextCrewDetail.DOB, dt) == false)
                                   young = "True";

                            else
                                   young = "False";
                            return young;
                     }
                     set
                     {
                            young = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("Youngs"));
                     }
              }


              private void SaveMethod(CrewDetailClass cdc)
              {
                     cdc.PortWkYoung = cdc.PortWkYoung == null ? string.Empty : cdc.PortWkYoung;
                     cdc.PortNWKYoung = cdc.PortNWKYoung == null ? string.Empty : cdc.PortNWKYoung;
                     cdc.SeaWkYoung = cdc.SeaWkYoung == null ? string.Empty : cdc.SeaWkYoung;
                     cdc.SeaNWKYoung = cdc.SeaNWKYoung == null ? string.Empty : cdc.SeaNWKYoung;

                     if (cdc.SeaNWKYoung.Replace("2", "1").Contains("1") == false && cdc.SeaWkYoung.Replace("2", "1").Contains("1") == false && (cdc.PortNWKYoung.Replace("2", "1").Contains("1") == false && cdc.PortWkYoung.Replace("2", "1").Contains("1") == false))
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            MessageBox.Show("Please Enter the WatchKeeping Working Hours as Sea and Port", "Individual Work Schedule", MessageBoxButton.OK, MessageBoxImage.Error);
                     }
                     else if (!string.IsNullOrEmpty(cdc.SeaNC) || !string.IsNullOrEmpty(cdc.PortNC) || !string.IsNullOrEmpty(cdc.SeaNC) && !string.IsNullOrEmpty(cdc.PortNC))
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            MessageBox.Show("Please remove all the Deviations", "Individual Work Schedule", MessageBoxButton.OK, MessageBoxImage.Error);
                     }
                     else
                     {
                            CheckErrorMessage.CheckErrorMessages = false;

                            if (cdc.Id == 0)
                            {

                                   cdc.SeaWk1 = SeaWkShedule(cdc.SeaWkYoung).TrimEnd(',');
                                   cdc.SeaNWK1 = SeaWkShedule(cdc.SeaNWKYoung).TrimEnd(',');
                                   cdc.PortWk1 = SeaWkShedule(cdc.PortWkYoung).TrimEnd(',');
                                   cdc.PortNWK1 = SeaWkShedule(cdc.PortNWKYoung).TrimEnd(',');

                                   sc.CrewDetails.Add(cdc);
                                   sc.SaveChanges();
                                   StaticHelper.Editing = false;
                                   StaticHelper.Wathckeeping = true;
                                   MessageBox.Show("Record saved successfully", "Add Crew Detail", MessageBoxButton.OK, MessageBoxImage.Information);

                                   AddNextCrewDetail = new CrewDetailClass();
                                   RaisePropertyChanged("AddNextCrewDetail");

                            }
                            else
                            {
                                   var findrank = sc.CrewDetails.Where(x => x.Id == cdc.Id).FirstOrDefault();

                                   //if (findrank != null)
                                   //{
                                   cdc.name = textinfo.ToTitleCase(cdc.name.ToLower());

                                   var local = sc.Set<CrewDetailClass>()
                                    .Local
                                    .FirstOrDefault(f => f.Id == cdc.Id);
                                   if (local != null)
                                   {
                                          sc.Entry(local).State = EntityState.Detached;
                                   }

                                   cdc.SeaWk1 = SeaWkShedule(cdc.SeaWkYoung).TrimEnd(',');
                                   cdc.SeaNWK1 = SeaWkShedule(cdc.SeaNWKYoung).TrimEnd(',');
                                   cdc.PortWk1 = SeaWkShedule(cdc.PortWkYoung).TrimEnd(',');
                                   cdc.PortNWK1 = SeaWkShedule(cdc.PortNWKYoung).TrimEnd(',');


                                   var UpdatedLocation = new CrewDetailClass()
                                   {
                                          Id = cdc.Id,
                                          name = cdc.name,
                                          UserName = cdc.UserName,
                                          position = cdc.position,
                                          department = cdc.department,
                                          ServiceFrom = cdc.ServiceFrom,
                                          ServiceTo = cdc.ServiceTo,
                                          CDC = cdc.CDC,
                                          empno = cdc.empno,
                                          pswd = cdc.pswd,
                                          comments = cdc.comments,
                                          overtime = cdc.overtime,
                                          SeaWk = cdc.SeaWk,
                                          SeaNWK = cdc.SeaNWK,
                                          PortWk = cdc.PortWk,
                                          PortNWK = cdc.PortNWK,
                                          dates = cdc.dates,
                                          SeaWH = cdc.SeaWH,
                                          portWH = cdc.portWH,
                                          did = cdc.did,
                                          rid = cdc.rid,
                                          SeaWk1 = cdc.SeaWk1,
                                          SeaNWK1 = cdc.SeaNWK1,
                                          PortWk1 = cdc.PortWk1,
                                          PortNWK1 = cdc.PortNWK1,
                                          OpaStatus = cdc.OpaStatus,
                                          CertificateView = cdc.CertificateView,
                                          CertificateAdd = cdc.CertificateAdd,
                                          CertificateEdit = cdc.CertificateEdit,
                                          CertificateDelete = cdc.CertificateDelete,
                                          DOB = cdc.DOB,
                                          chkyoungs = cdc.chkyoungs,
                                          SeaWkYoung = cdc.SeaWkYoung,
                                          SeaNWKYoung = cdc.SeaNWKYoung,
                                          PortWkYoung = cdc.PortWkYoung,
                                          PortNWKYoung = cdc.PortNWKYoung,
                                          Remarks = cdc.Remarks,
                                          WatchKeeper = cdc.WatchKeeper,
                                          WatchKeeper1 = cdc.WatchKeeper == true ? false : true
                                   };

                                   sc.Entry(UpdatedLocation).State = EntityState.Modified;
                                   sc.SaveChanges();

                                   var some = sc.WorkHourss.Where(x => x.UserName.Equals(UpdatedLocation.UserName.Trim())).ToList();
                                   some.ForEach(a =>
                                   {
                                          a.FullName = UpdatedLocation.name;
                                          a.Department = UpdatedLocation.department;
                                          a.Position = UpdatedLocation.position;
                                   });

                                   sc.SaveChanges();


                                   StaticHelper.Editing = false;
                                   StaticHelper.Wathckeeping = true;
                                   MessageBox.Show("Record updated successfully", "Update Crew Detail", MessageBoxButton.OK, MessageBoxImage.Information);

                                   AddNextCrewDetail = new CrewDetailClass();
                                   RaisePropertyChanged("AddNextCrewDetail");


                            }

                            new CrewDetailViewModel();
                            new ViewModelReports.CrewDetailViewModel();
                            new DetailWorkRestHoursViewModel();
                            new HODViewModel();
                     }


              }
              private void OverTimeMethod(CrewDetailClass cdc)
              {
                     if (cdc.PortNWKYoung.Replace("2", "1").Contains("1") == false && cdc.PortWkYoung.Replace("2", "1").Contains("1") == false)
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            MessageBox.Show("Please Enter the WatchKeeping Working Hours as Sea and Port", "Individual Work Schedule", MessageBoxButton.OK, MessageBoxImage.Error);
                     }
                     else if (!string.IsNullOrEmpty(cdc.SeaNC) || !string.IsNullOrEmpty(cdc.PortNC) || !string.IsNullOrEmpty(cdc.SeaNC) && !string.IsNullOrEmpty(cdc.PortNC))
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            MessageBox.Show("Please remove all the Deviations", "Individual Work Schedule", MessageBoxButton.OK, MessageBoxImage.Error);
                     }
                     else
                     {
                            CheckErrorMessage.CheckErrorMessages = true;
                            if (cdc.Id == 0)
                            {
                                   cdc.name = textinfo.ToTitleCase(cdc.name.ToLower());

                                   cdc.SeaWk1 = SeaWkShedule(cdc.SeaWkYoung).TrimEnd(',');
                                   cdc.SeaNWK1 = SeaWkShedule(cdc.SeaNWKYoung).TrimEnd(',');
                                   cdc.PortWk1 = SeaWkShedule(cdc.PortWkYoung).TrimEnd(',');
                                   cdc.PortNWK1 = SeaWkShedule(cdc.PortNWKYoung).TrimEnd(',');

                                   sc.CrewDetails.Add(cdc);
                                   sc.SaveChanges();

                                   //MessageBox.Show("Record saved successfully", "Add Crew Detail");
                                   //AddNextCrewDetail = new CrewDetailClass();
                                   //RaisePropertyChanged("AddNextCrewDetail");

                            }
                            else
                            {
                                   cdc.did = sc.Departments.Where(x => x.DeptName == cdc.department).Select(p => p.did).FirstOrDefault();
                                   cdc.rid = sc.CrewRanks.Where(x => x.Rank == cdc.position).Select(p => p.cid).FirstOrDefault();

                                   var findrank = sc.CrewDetails.Where(x => x.Id == cdc.Id).FirstOrDefault();

                                   //if (findrank != null)
                                   //{
                                   cdc.name = textinfo.ToTitleCase(cdc.name.ToLower());

                                   var local = sc.Set<CrewDetailClass>()
                                    .Local
                                    .FirstOrDefault(f => f.Id == cdc.Id);
                                   if (local != null)
                                   {
                                          sc.Entry(local).State = EntityState.Detached;
                                   }


                                   cdc.SeaWk1 = SeaWkShedule(cdc.SeaWkYoung).TrimEnd(',');
                                   cdc.SeaNWK1 = SeaWkShedule(cdc.SeaNWKYoung).TrimEnd(',');
                                   cdc.PortWk1 = SeaWkShedule(cdc.PortWkYoung).TrimEnd(',');
                                   cdc.PortNWK1 = SeaWkShedule(cdc.PortNWKYoung).TrimEnd(',');

                                   var UpdatedLocation = new CrewDetailClass()
                                   {
                                          Id = cdc.Id,
                                          name = cdc.name,
                                          UserName = cdc.UserName,
                                          position = cdc.position,
                                          department = cdc.department,
                                          ServiceFrom = cdc.ServiceFrom,
                                          ServiceTo = cdc.ServiceTo,
                                          CDC = cdc.CDC,
                                          empno = cdc.empno,
                                          pswd = cdc.pswd,
                                          comments = cdc.comments,
                                          overtime = cdc.overtime,
                                          SeaWk = cdc.SeaWk,
                                          SeaNWK = cdc.SeaNWK,
                                          PortWk = cdc.PortWk,
                                          PortNWK = cdc.PortNWK,
                                          dates = cdc.dates,
                                          SeaWH = cdc.SeaWH,
                                          portWH = cdc.portWH,
                                          did = cdc.did,
                                          rid = cdc.rid,
                                          SeaWk1 = cdc.SeaWk1,
                                          SeaNWK1 = cdc.SeaNWK1,
                                          PortWk1 = cdc.PortWk1,
                                          PortNWK1 = cdc.PortNWK1,
                                          OpaStatus = cdc.OpaStatus,
                                          CertificateView = cdc.CertificateView,
                                          CertificateAdd = cdc.CertificateAdd,
                                          CertificateEdit = cdc.CertificateEdit,
                                          CertificateDelete = cdc.CertificateDelete,
                                          DOB = cdc.DOB,
                                          chkyoungs = cdc.chkyoungs,
                                          SeaWkYoung = cdc.SeaWkYoung,
                                          SeaNWKYoung = cdc.SeaNWKYoung,
                                          PortWkYoung = cdc.PortWkYoung,
                                          PortNWKYoung = cdc.PortNWKYoung,
                                          Remarks = cdc.Remarks
                                   };

                                   sc.Entry(UpdatedLocation).State = EntityState.Modified;
                                   sc.SaveChanges();
                                   //MessageBox.Show("Record updated successfully", "Update Crew Detail");
                                   //AddNextCrewDetail = new CrewDetailClass();
                                   //RaisePropertyChanged("AddNextCrewDetail");


                            }

                            new AddOverTimeCrewDetailModel(cdc);
                     }
              }

              public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
              {
                     if ((bool)value)
                     {
                            return true;
                     }
                     return false;
              }

              public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
              {
                     throw new NotImplementedException();
              }

              public string SeaWkShedule(string lblRes)
              {
                     string timeoutPortWK = string.Empty;
                     if (lblRes != "")
                     {

                            string v = lblRes.Substring(0, lblRes.Length - 1);
                            string[] nums = v.Split(',');

                            int b = 0;

                            for (int s = 0; s <= 95; s++)
                            {

                                   if (nums[s] == "1")
                                   {
                                          if (b == 0)
                                          {
                                                 b = s;
                                                 b = b + 1;

                                                 string tym = "";

                                                 if (s == 0)
                                                        tym = "0000";

                                                 if (s == 1)
                                                        tym = "0015";

                                                 if (s == 2)
                                                        tym = "0030";

                                                 if (s == 3)
                                                        tym = "0045";

                                                 if (s == 4)
                                                        tym = "0100";

                                                 if (s == 5)
                                                        tym = "0115";

                                                 if (s == 6)
                                                        tym = "0130";

                                                 if (s == 7)
                                                        tym = "0145";

                                                 if (s == 8)
                                                        tym = "0200";

                                                 if (s == 9)
                                                        tym = "0215";

                                                 if (s == 10)
                                                        tym = "0230";

                                                 if (s == 11)
                                                        tym = "0245";

                                                 if (s == 12)
                                                        tym = "0300";

                                                 if (s == 13)
                                                        tym = "0315";

                                                 if (s == 14)
                                                        tym = "0330";

                                                 if (s == 15)
                                                        tym = "0345";

                                                 if (s == 16)
                                                        tym = "0400";

                                                 if (s == 17)
                                                        tym = "0415";

                                                 if (s == 18)
                                                        tym = "0430";

                                                 if (s == 19)
                                                        tym = "0445";

                                                 if (s == 20)
                                                        tym = "0500";

                                                 if (s == 21)
                                                        tym = "0515";

                                                 if (s == 22)
                                                        tym = "0530";

                                                 if (s == 23)
                                                        tym = "0545";

                                                 if (s == 24)
                                                        tym = "0600";

                                                 if (s == 25)
                                                        tym = "0615";

                                                 if (s == 26)
                                                        tym = "0630";

                                                 if (s == 27)
                                                        tym = "0645";

                                                 if (s == 28)
                                                        tym = "0700";

                                                 if (s == 29)
                                                        tym = "0715";

                                                 if (s == 30)
                                                        tym = "0730";

                                                 if (s == 31)
                                                        tym = "0745";

                                                 if (s == 32)
                                                        tym = "0800";

                                                 if (s == 33)
                                                        tym = "0815";

                                                 if (s == 34)
                                                        tym = "0830";

                                                 if (s == 35)
                                                        tym = "0845";

                                                 if (s == 36)
                                                        tym = "0900";

                                                 if (s == 37)
                                                        tym = "0915";

                                                 if (s == 38)
                                                        tym = "0930";

                                                 if (s == 39)
                                                        tym = "0945";

                                                 if (s == 40)
                                                        tym = "1000";

                                                 if (s == 41)
                                                        tym = "1015";

                                                 if (s == 42)
                                                        tym = "1030";

                                                 if (s == 43)
                                                        tym = "1045";

                                                 if (s == 44)
                                                        tym = "1100";

                                                 if (s == 45)
                                                        tym = "1115";

                                                 if (s == 46)
                                                        tym = "1130";

                                                 if (s == 47)
                                                        tym = "1145";

                                                 if (s == 48)
                                                        tym = "1200";

                                                 if (s == 49)
                                                        tym = "1215";

                                                 if (s == 50)
                                                        tym = "1230";

                                                 if (s == 51)
                                                        tym = "1245";

                                                 if (s == 52)
                                                        tym = "1300";

                                                 if (s == 53)
                                                        tym = "1315";

                                                 if (s == 54)
                                                        tym = "1330";

                                                 if (s == 55)
                                                        tym = "1345";

                                                 if (s == 56)
                                                        tym = "1400";

                                                 if (s == 57)
                                                        tym = "1415";

                                                 if (s == 58)
                                                        tym = "1430";

                                                 if (s == 59)
                                                        tym = "1445";

                                                 if (s == 60)
                                                        tym = "1500";

                                                 if (s == 61)
                                                        tym = "1515";

                                                 if (s == 62)
                                                        tym = "1530";

                                                 if (s == 63)
                                                        tym = "1545";

                                                 if (s == 64)
                                                        tym = "1600";

                                                 if (s == 65)
                                                        tym = "1615";

                                                 if (s == 66)
                                                        tym = "1630";

                                                 if (s == 67)
                                                        tym = "1645";

                                                 if (s == 68)
                                                        tym = "1700";

                                                 if (s == 69)
                                                        tym = "1715";

                                                 if (s == 70)
                                                        tym = "1730";

                                                 if (s == 71)
                                                        tym = "1745";

                                                 if (s == 72)
                                                        tym = "1800";

                                                 if (s == 73)
                                                        tym = "1815";

                                                 if (s == 74)
                                                        tym = "1830";

                                                 if (s == 75)
                                                        tym = "1845";

                                                 if (s == 76)
                                                        tym = "1900";

                                                 if (s == 77)
                                                        tym = "1915";

                                                 if (s == 78)
                                                        tym = "1930";

                                                 if (s == 79)
                                                        tym = "1945";

                                                 if (s == 80)
                                                        tym = "2000";

                                                 if (s == 81)
                                                        tym = "2015";

                                                 if (s == 82)
                                                        tym = "2030";

                                                 if (s == 83)
                                                        tym = "2045";

                                                 if (s == 84)
                                                        tym = "2100";

                                                 if (s == 85)
                                                        tym = "2115";

                                                 if (s == 86)
                                                        tym = "2130";

                                                 if (s == 87)
                                                        tym = "2145";

                                                 if (s == 88)
                                                        tym = "2200";

                                                 if (s == 89)
                                                        tym = "2215";

                                                 if (s == 90)
                                                        tym = "2230";

                                                 if (s == 91)
                                                        tym = "2245";

                                                 if (s == 92)
                                                        tym = "2300";

                                                 if (s == 93)
                                                        tym = "2315";

                                                 if (s == 94)
                                                        tym = "2330";

                                                 if (s == 95)
                                                        tym = "2345";

                                                 timeoutPortWK += tym.ToString() + "-";
                                          }
                                          else
                                          {
                                                 b = s;
                                          }

                                   }
                                   else
                                   {
                                          if (b != 0)
                                          {
                                                 string tym = "";

                                                 s = s - 1;
                                                 b = 0;



                                                 if (s == 0)
                                                        tym = "0015";

                                                 if (s == 1)
                                                        tym = "0030";

                                                 if (s == 2)
                                                        tym = "0045";

                                                 if (s == 3)
                                                        tym = "0100";

                                                 if (s == 4)
                                                        tym = "0115";

                                                 if (s == 5)
                                                        tym = "0130";

                                                 if (s == 6)
                                                        tym = "0145";

                                                 if (s == 7)
                                                        tym = "0200";

                                                 if (s == 8)
                                                        tym = "0215";

                                                 if (s == 9)
                                                        tym = "0230";

                                                 if (s == 10)
                                                        tym = "0245";

                                                 if (s == 11)
                                                        tym = "0300";

                                                 if (s == 12)
                                                        tym = "0315";


                                                 if (s == 13)
                                                        tym = "0330";

                                                 if (s == 14)
                                                        tym = "0345";

                                                 if (s == 15)
                                                        tym = "0400";

                                                 if (s == 16)
                                                        tym = "0415";

                                                 if (s == 17)
                                                        tym = "0430";

                                                 if (s == 18)
                                                        tym = "0445";

                                                 if (s == 19)
                                                        tym = "0500";

                                                 if (s == 20)
                                                        tym = "0515";

                                                 if (s == 21)
                                                        tym = "0530";

                                                 if (s == 22)
                                                        tym = "0545";

                                                 if (s == 23)
                                                        tym = "0600";

                                                 if (s == 24)
                                                        tym = "0615";

                                                 if (s == 25)
                                                        tym = "0630";

                                                 if (s == 26)
                                                        tym = "0645";

                                                 if (s == 27)
                                                        tym = "0700";

                                                 if (s == 28)
                                                        tym = "0715";

                                                 if (s == 29)
                                                        tym = "0730";

                                                 if (s == 30)
                                                        tym = "0745";

                                                 if (s == 31)
                                                        tym = "0800";

                                                 if (s == 32)
                                                        tym = "0815";

                                                 if (s == 33)
                                                        tym = "0830";

                                                 if (s == 34)
                                                        tym = "0845";

                                                 if (s == 35)
                                                        tym = "0900";

                                                 if (s == 36)
                                                        tym = "0915";

                                                 if (s == 37)
                                                        tym = "0930";

                                                 if (s == 38)
                                                        tym = "0945";

                                                 if (s == 39)
                                                        tym = "1000";

                                                 if (s == 40)
                                                        tym = "1015";

                                                 if (s == 41)
                                                        tym = "1030";

                                                 if (s == 42)
                                                        tym = "1045";

                                                 if (s == 43)
                                                        tym = "1100";

                                                 if (s == 44)
                                                        tym = "1115";

                                                 if (s == 45)
                                                        tym = "1130";

                                                 if (s == 46)
                                                        tym = "1145";

                                                 if (s == 47)
                                                        tym = "1200";

                                                 if (s == 48)
                                                        tym = "1215";

                                                 if (s == 49)
                                                        tym = "1230";

                                                 if (s == 50)
                                                        tym = "1245";

                                                 if (s == 51)
                                                        tym = "1300";

                                                 if (s == 52)
                                                        tym = "1315";

                                                 if (s == 53)
                                                        tym = "1330";

                                                 if (s == 54)
                                                        tym = "1345";

                                                 if (s == 55)
                                                        tym = "1400";

                                                 if (s == 56)
                                                        tym = "1415";

                                                 if (s == 57)
                                                        tym = "1430";

                                                 if (s == 58)
                                                        tym = "1445";

                                                 if (s == 59)
                                                        tym = "1500";

                                                 if (s == 60)
                                                        tym = "1515";

                                                 if (s == 61)
                                                        tym = "1530";

                                                 if (s == 62)
                                                        tym = "1545";

                                                 if (s == 63)
                                                        tym = "1600";

                                                 if (s == 64)
                                                        tym = "1615";

                                                 if (s == 65)
                                                        tym = "1630";

                                                 if (s == 66)
                                                        tym = "1645";

                                                 if (s == 67)
                                                        tym = "1700";

                                                 if (s == 68)
                                                        tym = "1715";

                                                 if (s == 69)
                                                        tym = "1730";

                                                 if (s == 70)
                                                        tym = "1745";

                                                 if (s == 71)
                                                        tym = "1800";

                                                 if (s == 72)
                                                        tym = "1815";

                                                 if (s == 73)
                                                        tym = "1830";

                                                 if (s == 74)
                                                        tym = "1845";

                                                 if (s == 75)
                                                        tym = "1900";

                                                 if (s == 76)
                                                        tym = "1915";

                                                 if (s == 77)
                                                        tym = "1930";

                                                 if (s == 78)
                                                        tym = "1945";

                                                 if (s == 79)
                                                        tym = "2000";

                                                 if (s == 80)
                                                        tym = "2015";

                                                 if (s == 81)
                                                        tym = "2030";

                                                 if (s == 82)
                                                        tym = "2045";

                                                 if (s == 83)
                                                        tym = "2100";

                                                 if (s == 84)
                                                        tym = "2115";

                                                 if (s == 85)
                                                        tym = "2130";

                                                 if (s == 86)
                                                        tym = "2145";

                                                 if (s == 87)
                                                        tym = "2200";

                                                 if (s == 88)
                                                        tym = "2215";

                                                 if (s == 89)
                                                        tym = "2230";

                                                 if (s == 90)
                                                        tym = "2245";

                                                 if (s == 91)
                                                        tym = "2300";

                                                 if (s == 92)
                                                        tym = "2315";

                                                 if (s == 93)
                                                        tym = "2330";

                                                 if (s == 94)
                                                        tym = "2345";

                                                 if (s == 95)
                                                        tym = "2400";


                                                 timeoutPortWK += tym + ",";
                                                 b = 0;
                                          }


                                   }
                                   if (s == 95)
                                   {
                                          if (b != 0)
                                          {
                                                 string tym = "";


                                                 if (s == 0)
                                                        tym = "0015";

                                                 if (s == 1)
                                                        tym = "0030";

                                                 if (s == 2)
                                                        tym = "0045";

                                                 if (s == 3)
                                                        tym = "0100";

                                                 if (s == 4)
                                                        tym = "0115";

                                                 if (s == 5)
                                                        tym = "0130";

                                                 if (s == 6)
                                                        tym = "0145";

                                                 if (s == 7)
                                                        tym = "0200";

                                                 if (s == 8)
                                                        tym = "0215";

                                                 if (s == 9)
                                                        tym = "0230";

                                                 if (s == 10)
                                                        tym = "0245";

                                                 if (s == 11)
                                                        tym = "0300";

                                                 if (s == 12)
                                                        tym = "0315";


                                                 if (s == 13)
                                                        tym = "0330";

                                                 if (s == 14)
                                                        tym = "0345";

                                                 if (s == 15)
                                                        tym = "0400";

                                                 if (s == 16)
                                                        tym = "0415";

                                                 if (s == 17)
                                                        tym = "0430";

                                                 if (s == 18)
                                                        tym = "0445";

                                                 if (s == 19)
                                                        tym = "0500";

                                                 if (s == 20)
                                                        tym = "0515";

                                                 if (s == 21)
                                                        tym = "0530";

                                                 if (s == 22)
                                                        tym = "0545";

                                                 if (s == 23)
                                                        tym = "0600";

                                                 if (s == 24)
                                                        tym = "0615";

                                                 if (s == 25)
                                                        tym = "0630";

                                                 if (s == 26)
                                                        tym = "0645";

                                                 if (s == 27)
                                                        tym = "0700";

                                                 if (s == 28)
                                                        tym = "0715";

                                                 if (s == 29)
                                                        tym = "0730";

                                                 if (s == 30)
                                                        tym = "0745";

                                                 if (s == 31)
                                                        tym = "0800";

                                                 if (s == 32)
                                                        tym = "0815";

                                                 if (s == 33)
                                                        tym = "0830";

                                                 if (s == 34)
                                                        tym = "0845";

                                                 if (s == 35)
                                                        tym = "0900";

                                                 if (s == 36)
                                                        tym = "0915";

                                                 if (s == 37)
                                                        tym = "0930";


                                                 if (s == 38)
                                                        tym = "0945";

                                                 if (s == 39)
                                                        tym = "1000";


                                                 if (s == 40)
                                                        tym = "1015";

                                                 if (s == 41)
                                                        tym = "1030";


                                                 if (s == 42)
                                                        tym = "1045";

                                                 if (s == 43)
                                                        tym = "1100";


                                                 if (s == 44)
                                                        tym = "1115";

                                                 if (s == 45)
                                                        tym = "1130";

                                                 if (s == 46)
                                                        tym = "1145";

                                                 if (s == 47)
                                                        tym = "1200";

                                                 if (s == 48)
                                                        tym = "1215";

                                                 if (s == 49)
                                                        tym = "1230";

                                                 if (s == 50)
                                                        tym = "1245";

                                                 if (s == 51)
                                                        tym = "1300";

                                                 if (s == 52)
                                                        tym = "1315";

                                                 if (s == 53)
                                                        tym = "1330";

                                                 if (s == 54)
                                                        tym = "1345";

                                                 if (s == 55)
                                                        tym = "1400";

                                                 if (s == 56)
                                                        tym = "1415";

                                                 if (s == 57)
                                                        tym = "1430";

                                                 if (s == 58)
                                                        tym = "1445";

                                                 if (s == 59)
                                                        tym = "1500";

                                                 if (s == 60)
                                                        tym = "1515";

                                                 if (s == 61)
                                                        tym = "1530";

                                                 if (s == 62)
                                                        tym = "1545";

                                                 if (s == 63)
                                                        tym = "1600";

                                                 if (s == 64)
                                                        tym = "1615";

                                                 if (s == 65)
                                                        tym = "1630";

                                                 if (s == 66)
                                                        tym = "1645";

                                                 if (s == 67)
                                                        tym = "1700";

                                                 if (s == 68)
                                                        tym = "1715";

                                                 if (s == 69)
                                                        tym = "1730";

                                                 if (s == 70)
                                                        tym = "1745";

                                                 if (s == 71)
                                                        tym = "1800";

                                                 if (s == 72)
                                                        tym = "1815";

                                                 if (s == 73)
                                                        tym = "1830";

                                                 if (s == 74)
                                                        tym = "1845";

                                                 if (s == 75)
                                                        tym = "1900";

                                                 if (s == 76)
                                                        tym = "1915";

                                                 if (s == 77)
                                                        tym = "1930";

                                                 if (s == 78)
                                                        tym = "1945";

                                                 if (s == 79)
                                                        tym = "2000";

                                                 if (s == 80)
                                                        tym = "2015";

                                                 if (s == 81)
                                                        tym = "2030";

                                                 if (s == 82)
                                                        tym = "2045";

                                                 if (s == 83)
                                                        tym = "2100";

                                                 if (s == 84)
                                                        tym = "2115";

                                                 if (s == 85)
                                                        tym = "2130";

                                                 if (s == 86)
                                                        tym = "2145";

                                                 if (s == 87)
                                                        tym = "2200";

                                                 if (s == 88)
                                                        tym = "2215";

                                                 if (s == 89)
                                                        tym = "2230";

                                                 if (s == 90)
                                                        tym = "2245";

                                                 if (s == 91)
                                                        tym = "2300";

                                                 if (s == 92)
                                                        tym = "2315";

                                                 if (s == 93)
                                                        tym = "2330";

                                                 if (s == 94)
                                                        tym = "2345";

                                                 if (s == 95)
                                                        tym = "2400";


                                                 timeoutPortWK += tym;
                                                 b = 0;
                                          }
                                   }
                            }
                     }

                     return timeoutPortWK;

              }

              public bool CheckDate(DateTime d1, DateTime d2)
              {
                     var saa = d2.AddYears(-18) < d1;
                     //var saa = d2.AddYears(-16) >= d1 && d2.AddYears(-18) < d1;
                     return saa;
              }

              new public event PropertyChangedEventHandler PropertyChanged;
              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }


       }
}
