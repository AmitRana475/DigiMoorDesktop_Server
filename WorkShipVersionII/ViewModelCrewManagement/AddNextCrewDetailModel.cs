using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.ViewModelCrewManagement
{
       public class AddNextCrewDetailModel : ViewModelBase, IValueConverter, IWkShedule
       {
              private static ShipmentContaxt sc;
              TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
              public AddNextCrewDetailModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }

                     saveCommand = new RelayCommand<CrewDetailClass>(SaveMethod);
                     overtimeCommand = new RelayCommand<CrewDetailClass>(OverTimeMethod);


              }



              public AddNextCrewDetailModel(CrewDetailClass cdc)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }


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



              private void SaveMethod(CrewDetailClass cdc)
              {
                     try
                     {


                            cdc.PortNWK = string.IsNullOrEmpty(cdc.PortNWK) ? string.Empty : cdc.PortNWK;

                            cdc.SeaNWK = string.IsNullOrEmpty(cdc.SeaNWK) ? string.Empty : cdc.SeaNWK;
                            // if (StaticHelper.Wathckeeping == true)

                            cdc.SeaWk = string.IsNullOrEmpty(cdc.SeaWk) ? string.Empty : cdc.SeaWk;
                            cdc.PortWk = string.IsNullOrEmpty(cdc.PortWk) ? string.Empty : cdc.PortWk;


                            if ((cdc.SeaNWK.Replace("2", "1").Contains("1") == false && cdc.SeaWk.Replace("2", "1").Contains("1") == false) && (cdc.PortNWK.Replace("2", "1").Contains("1") == false && cdc.PortWk.Replace("2", "1").Contains("1") == false))
                            {
                                   CheckErrorMessage.CheckErrorMessages = true;
                                   MessageBox.Show("Please Enter the WatchKeeping Working Hours as Sea and Port", "Individual Work Schedule", MessageBoxButton.OK, MessageBoxImage.Error);
                                   //new AddNextCrewDetailModel(cdc);
                            }
                            else if (!string.IsNullOrEmpty(cdc.SeaNC) || !string.IsNullOrEmpty(cdc.PortNC) || !string.IsNullOrEmpty(cdc.SeaNC) && !string.IsNullOrEmpty(cdc.PortNC))
                            {
                                   CheckErrorMessage.CheckErrorMessages = true;
                                   MessageBox.Show("Please remove all the Deviations", "Individual Work Schedule", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                   CheckErrorMessage.CheckErrorMessages = false;

                                   if (cdc.Id == 0)
                                   {
                                          cdc.SeaWk1 = SeaWkShedule(cdc.SeaWk).TrimEnd(',');
                                          cdc.SeaNWK1 = SeaWkShedule(cdc.SeaNWK).TrimEnd(',');
                                          cdc.PortWk1 = SeaWkShedule(cdc.PortWk).TrimEnd(',');
                                          cdc.PortNWK1 = SeaWkShedule(cdc.PortNWK).TrimEnd(',');

                                          sc.CrewDetails.Add(cdc);
                                          sc.SaveChanges();

                                          MessageBox.Show("Record saved successfully", "Add Crew Detail", MessageBoxButton.OK, MessageBoxImage.Information);
                                          StaticHelper.Editing = false;
                                          StaticHelper.Wathckeeping = true;
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

                                          cdc.SeaWk1 = SeaWkShedule(cdc.SeaWk).TrimEnd(',');
                                          cdc.SeaNWK1 = SeaWkShedule(cdc.SeaNWK).TrimEnd(',');
                                          cdc.PortWk1 = SeaWkShedule(cdc.PortWk).TrimEnd(',');
                                          cdc.PortNWK1 = SeaWkShedule(cdc.PortNWK).TrimEnd(',');

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

                                          //Update into WorkHour's Table
                                          var some = sc.WorkHourss.Where(x => x.UserName.Equals(UpdatedLocation.UserName.Trim())).ToList();
                                          some.ForEach(a =>
                                                          {
                                                                 a.FullName = UpdatedLocation.name;
                                                                 a.Department = UpdatedLocation.department;
                                                                 a.Position = UpdatedLocation.position;
                                                          });

                                          sc.SaveChanges();

                                          StaticHelper.Wathckeeping = true;
                                          StaticHelper.Editing = false;
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
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }

              }
              private void OverTimeMethod(CrewDetailClass cdc)
              {
                     try
                     {
                            cdc.PortWk = string.IsNullOrEmpty(cdc.PortWk) ? string.Empty : cdc.PortWk;
                            cdc.PortNWK = string.IsNullOrEmpty(cdc.PortNWK) ? string.Empty : cdc.PortNWK;
                            cdc.SeaWk = string.IsNullOrEmpty(cdc.SeaWk) ? string.Empty : cdc.SeaWk;
                            cdc.SeaNWK = string.IsNullOrEmpty(cdc.SeaNWK) ? string.Empty : cdc.SeaNWK;

                            if (cdc.PortNWK.Replace("2", "1").Contains("1") == false && cdc.PortWk.Replace("2", "1").Contains("1") == false)
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

                                          cdc.SeaWk1 = SeaWkShedule(cdc.SeaWk).TrimEnd(',');
                                          cdc.SeaNWK1 = SeaWkShedule(cdc.SeaNWK).TrimEnd(',');
                                          cdc.PortWk1 = SeaWkShedule(cdc.PortWk).TrimEnd(',');
                                          cdc.PortNWK1 = SeaWkShedule(cdc.PortNWK).TrimEnd(',');

                                          cdc.name = textinfo.ToTitleCase(cdc.name.ToLower());
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

                                          cdc.SeaWk1 = SeaWkShedule(cdc.SeaWk).TrimEnd(',');
                                          cdc.SeaNWK1 = SeaWkShedule(cdc.SeaNWK).TrimEnd(',');
                                          cdc.PortWk1 = SeaWkShedule(cdc.PortWk).TrimEnd(',');
                                          cdc.PortNWK1 = SeaWkShedule(cdc.PortNWK).TrimEnd(',');

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
                                                 WatchKeeper = cdc.WatchKeeper
                                          };

                                          sc.Entry(UpdatedLocation).State = EntityState.Modified;
                                          sc.SaveChanges();


                                          //Update into WorkHour's Table
                                          var some = sc.WorkHourss.Where(x => x.UserName.Equals(UpdatedLocation.UserName.Trim())).ToList();
                                          some.ForEach(a =>
                                          {
                                                 a.FullName = UpdatedLocation.name;
                                                 a.Department = UpdatedLocation.department;
                                                 a.Position = UpdatedLocation.position;
                                          });

                                          sc.SaveChanges();

                                          //MessageBox.Show("Record updated successfully", "Update Crew Detail");
                                          //AddNextCrewDetail = new CrewDetailClass();
                                          //RaisePropertyChanged("AddNextCrewDetail");


                                   }

                                   new AddOverTimeCrewDetailModel(cdc);
                            }

                     }
                     catch (Exception ex)
                     {
                            CheckErrorMessage.CheckErrorMessages = false;
                            sc.ErrorLog(ex);
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
                     string timeoutSeaWK = string.Empty;
                     if (lblRes != "")
                     {
                            string v = lblRes.Substring(0, lblRes.Length - 1);
                            string[] nums = v.Split(',');

                            int b = 0;
                            for (int s = 0; s <= 47; s++)
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
                                                        tym = "0030";

                                                 if (s == 2)
                                                        tym = "0100";

                                                 if (s == 3)
                                                        tym = "0130";

                                                 if (s == 4)
                                                        tym = "0200";

                                                 if (s == 5)
                                                        tym = "0230";

                                                 if (s == 6)
                                                        tym = "0300";

                                                 if (s == 7)
                                                        tym = "0330";

                                                 if (s == 8)
                                                        tym = "0400";


                                                 if (s == 9)
                                                        tym = "0430";

                                                 if (s == 10)
                                                        tym = "0500";

                                                 if (s == 11)
                                                        tym = "0530";

                                                 if (s == 12)
                                                        tym = "0600";

                                                 if (s == 13)
                                                        tym = "0630";

                                                 if (s == 14)
                                                        tym = "0700";

                                                 if (s == 15)
                                                        tym = "0730";

                                                 if (s == 16)
                                                        tym = "0800";

                                                 if (s == 17)
                                                        tym = "0830";

                                                 if (s == 18)
                                                        tym = "0900";

                                                 if (s == 19)
                                                        tym = "0930";

                                                 if (s == 20)
                                                        tym = "1000";

                                                 if (s == 21)
                                                        tym = "1030";

                                                 if (s == 22)
                                                        tym = "1100";

                                                 if (s == 23)
                                                        tym = "1130";

                                                 if (s == 24)
                                                        tym = "1200";

                                                 if (s == 25)
                                                        tym = "1230";

                                                 if (s == 26)
                                                        tym = "1300";

                                                 if (s == 27)
                                                        tym = "1330";

                                                 if (s == 28)
                                                        tym = "1400";

                                                 if (s == 29)
                                                        tym = "1430";

                                                 if (s == 30)
                                                        tym = "1500";

                                                 if (s == 31)
                                                        tym = "1530";

                                                 if (s == 32)
                                                        tym = "1600";

                                                 if (s == 33)
                                                        tym = "1630";


                                                 if (s == 34)
                                                        tym = "1700";

                                                 if (s == 35)
                                                        tym = "1730";

                                                 if (s == 36)
                                                        tym = "1800";

                                                 if (s == 37)
                                                        tym = "1830";


                                                 if (s == 38)
                                                        tym = "1900";

                                                 if (s == 39)
                                                        tym = "1930";


                                                 if (s == 40)
                                                        tym = "2000";

                                                 if (s == 41)
                                                        tym = "2030";

                                                 if (s == 42)
                                                        tym = "2100";

                                                 if (s == 43)
                                                        tym = "2130";

                                                 if (s == 44)
                                                        tym = "2200";

                                                 if (s == 45)
                                                        tym = "2230";

                                                 if (s == 46)
                                                        tym = "2300";

                                                 if (s == 47)
                                                        tym = "2330";

                                                 timeoutSeaWK += tym.ToString() + "-";
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

                                                 b = 0;
                                                 if (s == 0)
                                                        tym = "0000";

                                                 if (s == 1)
                                                        tym = "0030";

                                                 if (s == 2)
                                                        tym = "0100";

                                                 if (s == 3)
                                                        tym = "0130";

                                                 if (s == 4)
                                                        tym = "0200";

                                                 if (s == 5)
                                                        tym = "0230";

                                                 if (s == 6)
                                                        tym = "0300";

                                                 if (s == 7)
                                                        tym = "0330";

                                                 if (s == 8)
                                                        tym = "0400";

                                                 if (s == 9)
                                                        tym = "0430";

                                                 if (s == 10)
                                                        tym = "0500";

                                                 if (s == 11)
                                                        tym = "0530";

                                                 if (s == 12)
                                                        tym = "0600";

                                                 if (s == 13)
                                                        tym = "0630";

                                                 if (s == 14)
                                                        tym = "0700";

                                                 if (s == 15)
                                                        tym = "0730";

                                                 if (s == 16)
                                                        tym = "0800";

                                                 if (s == 17)
                                                        tym = "0830";

                                                 if (s == 18)
                                                        tym = "0900";

                                                 if (s == 19)
                                                        tym = "0930";

                                                 if (s == 20)
                                                        tym = "1000";

                                                 if (s == 21)
                                                        tym = "1030";

                                                 if (s == 22)
                                                        tym = "1100";

                                                 if (s == 23)
                                                        tym = "1130";

                                                 if (s == 24)
                                                        tym = "1200";

                                                 if (s == 25)
                                                        tym = "1230";

                                                 if (s == 26)
                                                        tym = "1300";

                                                 if (s == 27)
                                                        tym = "1330";

                                                 if (s == 28)
                                                        tym = "1400";

                                                 if (s == 29)
                                                        tym = "1430";

                                                 if (s == 30)
                                                        tym = "1500";

                                                 if (s == 31)
                                                        tym = "1530";

                                                 if (s == 32)
                                                        tym = "1600";

                                                 if (s == 33)
                                                        tym = "1630";


                                                 if (s == 34)
                                                        tym = "1700";

                                                 if (s == 35)
                                                        tym = "1730";


                                                 if (s == 36)
                                                        tym = "1800";

                                                 if (s == 37)
                                                        tym = "1830";

                                                 if (s == 38)
                                                        tym = "1900";

                                                 if (s == 39)
                                                        tym = "1930";

                                                 if (s == 40)
                                                        tym = "2000";

                                                 if (s == 41)
                                                        tym = "2030";

                                                 if (s == 42)
                                                        tym = "2100";

                                                 if (s == 43)
                                                        tym = "2130";

                                                 if (s == 44)
                                                        tym = "2200";

                                                 if (s == 45)
                                                        tym = "2230";

                                                 if (s == 46)
                                                        tym = "2300";

                                                 if (s == 47)
                                                        tym = "2330";

                                                 timeoutSeaWK += tym + ",";
                                                 b = 0;
                                          }

                                   }
                                   if (s == 47)
                                   {
                                          if (b != 0)
                                          {
                                                 string tym = "";

                                                 if (s == 0)
                                                        tym = "0030";

                                                 if (s == 1)
                                                        tym = "0100";

                                                 if (s == 2)
                                                        tym = "0130";

                                                 if (s == 3)
                                                        tym = "0200";

                                                 if (s == 4)
                                                        tym = "0230";

                                                 if (s == 5)
                                                        tym = "0300";

                                                 if (s == 6)
                                                        tym = "0330";

                                                 if (s == 7)
                                                        tym = "0400";

                                                 if (s == 8)
                                                        tym = "0430";

                                                 if (s == 9)
                                                        tym = "0500";

                                                 if (s == 10)
                                                        tym = "0530";

                                                 if (s == 11)
                                                        tym = "0600";

                                                 if (s == 12)
                                                        tym = "0630";

                                                 if (s == 13)
                                                        tym = "0700";

                                                 if (s == 14)
                                                        tym = "0730";

                                                 if (s == 15)
                                                        tym = "0800";

                                                 if (s == 16)
                                                        tym = "0830";

                                                 if (s == 17)
                                                        tym = "0900";

                                                 if (s == 18)
                                                        tym = "0930";

                                                 if (s == 19)
                                                        tym = "1000";

                                                 if (s == 20)
                                                        tym = "1030";

                                                 if (s == 21)
                                                        tym = "1100";

                                                 if (s == 22)
                                                        tym = "1130";

                                                 if (s == 23)
                                                        tym = "1200";

                                                 if (s == 24)
                                                        tym = "1230";

                                                 if (s == 25)
                                                        tym = "1300";

                                                 if (s == 26)
                                                        tym = "1330";

                                                 if (s == 27)
                                                        tym = "1400";

                                                 if (s == 28)
                                                        tym = "1430";

                                                 if (s == 29)
                                                        tym = "1500";

                                                 if (s == 30)
                                                        tym = "1530";

                                                 if (s == 31)
                                                        tym = "1600";

                                                 if (s == 32)
                                                        tym = "1630";

                                                 if (s == 33)
                                                        tym = "1700";

                                                 if (s == 34)
                                                        tym = "1730";

                                                 if (s == 35)
                                                        tym = "1800";

                                                 if (s == 36)
                                                        tym = "1830";

                                                 if (s == 37)
                                                        tym = "1900";

                                                 if (s == 38)
                                                        tym = "1930";

                                                 if (s == 39)
                                                        tym = "2000";

                                                 if (s == 40)
                                                        tym = "2030";

                                                 if (s == 41)
                                                        tym = "2100";

                                                 if (s == 42)
                                                        tym = "2130";

                                                 if (s == 43)
                                                        tym = "2200";


                                                 if (s == 44)
                                                        tym = "2230";

                                                 if (s == 45)
                                                        tym = "2300";

                                                 if (s == 46)
                                                        tym = "2330";

                                                 if (s == 47)
                                                        tym = "2400";

                                                 timeoutSeaWK += tym;
                                                 b = 0;
                                          }
                                   }
                            }
                     }

                     return timeoutSeaWK;

              }


              new public event PropertyChangedEventHandler PropertyChanged;
              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }


       }
}
