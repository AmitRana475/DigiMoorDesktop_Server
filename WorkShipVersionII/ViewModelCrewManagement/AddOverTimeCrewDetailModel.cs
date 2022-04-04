using GalaSoft.MvvmLight;
using System.Linq;
using DataBuildingLayer;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows;
using System.Data.Entity;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.ViewModelCrewManagement
{
       public class AddOverTimeCrewDetailModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              public AddOverTimeCrewDetailModel(CrewDetailClass cc)
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     int uid = sc.CrewDetails.Where(p => p.UserName == cc.UserName).Select(x => x.Id).FirstOrDefault();
                     OverTimeClass data = sc.OverTimes.Where(x => x.did.Equals(uid)).FirstOrDefault();
                     if (data != null)
                     {
                            AddCrewDetailOver = new OverTimeClass
                            {
                                   cid = data.cid,
                                   name = data.name,
                                   UserName = data.UserName,
                                   NrmlWHrs = data.NrmlWHrs,
                                   SatWHrs = data.SatWHrs,
                                   SunWhrs = data.SunWhrs,
                                   HolidayWHrs = data.HolidayWHrs,

                                   NrmlRates = data.NrmlRates,
                                   SatRates = data.SatRates,
                                   SunRates = data.SunRates,
                                   HolidayRates = data.HolidayRates,

                                   FixedOverTime = data.FixedOverTime,
                                   HourlyRate = data.HourlyRate,
                                   Currency = data.Currency,
                                   holidays = data.holidays,
                                   did = data.did

                            };
                     }
                     else
                     {
                            AddCrewDetailOver = new OverTimeClass
                            {
                                   UserName = cc.UserName,
                                   name = cc.name,
                                   did = uid

                            };
                     }

                     SCurrencyName = AddCrewDetailOver.Currency;
                     S_HolidayName = AddCrewDetailOver.holidays;
                     RaisePropertyChanged("AddCrewDetailOver");

              }
              public AddOverTimeCrewDetailModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                     }
                     saveCommand = new RelayCommand<OverTimeClass>(SaveMethod);

                     holidayname = GetHoliDay();
                     var data = new ObservableCollection<string>(sc.Currencys.Select(x => x.CurrencySymbol).ToList());
                     CurrencyName.Clear();
                     foreach (var item in data)
                     {
                            CurrencyName.Add(item);
                     }

              }


              private ICommand saveCommand;
              public ICommand SaveCommand
              {
                     get { return saveCommand; }
              }


              public static OverTimeClass _AddCrewDetailOver = new OverTimeClass();
              public OverTimeClass AddCrewDetailOver
              {
                     get
                     {
                            //if (erinfo != 0)
                            //{
                            //    refreshmessage(_AddCrewDetail);
                            //    refreshmessage1(_AddCrewDetail);
                            //    RaisePropertyChanged("AddCrewMessages");
                            //}
                            //OnPropertyChanged(new PropertyChangedEventArgs("AddCrewMessages"));
                            return _AddCrewDetailOver;
                     }
                     set
                     {
                            _AddCrewDetailOver = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("AddCrewDetail"));


                     }
              }



              private static string scurrencyname;
              public string SCurrencyName
              {
                     get
                     {
                            if (scurrencyname != null)
                                   AddCrewDetailOver.Currency = scurrencyname;
                            return scurrencyname;
                     }

                     set
                     {
                            scurrencyname = value;
                            if (scurrencyname != null)
                                   AddCrewDetailOver.Currency = scurrencyname;
                            OnPropertyChanged(new PropertyChangedEventArgs("SCurrencyName"));
                     }
              }

              private static ObservableCollection<string> currencyname = new ObservableCollection<string>();
              public ObservableCollection<string> CurrencyName
              {
                     get
                     {
                            return currencyname;
                     }
                     set
                     {
                            currencyname = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("CurrencyName"));
                     }
              }



              private static string S_holidayname;
              public string S_HolidayName
              {
                     get
                     {
                            if (S_holidayname != null)
                                   AddCrewDetailOver.holidays = S_holidayname;
                            return S_holidayname;
                     }

                     set
                     {
                            S_holidayname = value;
                            if (S_holidayname != null)
                                   AddCrewDetailOver.holidays = S_holidayname;
                            OnPropertyChanged(new PropertyChangedEventArgs("S_HolidayName"));
                     }
              }


              private static ObservableCollection<string> holidayname = new ObservableCollection<string>();
              public ObservableCollection<string> HoliDayName
              {
                     get
                     {
                            return holidayname;
                     }
                     set
                     {
                            holidayname = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("HoliDayName"));
                     }
              }

              private ObservableCollection<string> GetHoliDay()
              {
                     var AddDepartments = new ObservableCollection<string>();
                     var data = sc.HoliDayGroupNames.Select(x => x.GroupName).ToList();

                     foreach (var item in data)
                     {
                            AddDepartments.Add(item);
                     }

                     return AddDepartments;
              }


              private void SaveMethod(OverTimeClass otc)
              {
                     if (otc.cid == 0)
                     {

                            sc.OverTimes.Add(otc);
                            sc.SaveChanges();
                            StaticHelper.Wathckeeping = true;
                            StaticHelper.Editing = false;
                            MessageBox.Show("Record saved successfully", "Add Crew Detail", MessageBoxButton.OK, MessageBoxImage.Information);
                            StaticHelper.Editing = false;
                            CheckErrorMessage.CheckErrorMessages = false;
                            AddCrewDetailOver = new OverTimeClass();
                            RaisePropertyChanged("AddCrewDetailOver");

                     }
                     else
                     {
                            var findrank = sc.OverTimes.Where(x => x.cid == otc.cid).FirstOrDefault();

                            //if (findrank != null)
                            //{
                            //cdc.name = textinfo.ToTitleCase(cdc.name.ToLower());

                            var local = sc.Set<OverTimeClass>()
                             .Local
                             .FirstOrDefault(f => f.cid == otc.cid);
                            if (local != null)
                            {
                                   sc.Entry(local).State = EntityState.Detached;
                            }

                            var UpdatedLocation = new OverTimeClass()
                            {
                                   cid = otc.cid,
                                   name = otc.name,
                                   UserName = otc.UserName,
                                   NrmlWHrs = otc.NrmlWHrs,
                                   SatWHrs = otc.SatWHrs,
                                   SunWhrs = otc.SunWhrs,
                                   HolidayWHrs = otc.HolidayWHrs,

                                   NrmlRates = otc.NrmlRates,
                                   SatRates = otc.SatRates,
                                   SunRates = otc.SunRates,
                                   HolidayRates = otc.HolidayRates,

                                   FixedOverTime = otc.FixedOverTime,
                                   HourlyRate = otc.HourlyRate,
                                   Currency = otc.Currency,
                                   holidays = otc.holidays,
                                   did = otc.did
                            };

                            sc.Entry(UpdatedLocation).State = EntityState.Modified;
                            sc.SaveChanges();




                            var some = sc.OverTimes.Where(x => x.UserName.Equals(UpdatedLocation.UserName.Trim())).ToList();
                            some.ForEach(a =>
                            {
                                   a.name = UpdatedLocation.name;
                            });

                            sc.SaveChanges();

                            StaticHelper.Wathckeeping = true;
                            StaticHelper.Editing = false;

                            MessageBox.Show("Record updated successfully", "Update Crew Detail", MessageBoxButton.OK, MessageBoxImage.Information);
                            StaticHelper.Editing = false;
                            AddCrewDetailOver = new OverTimeClass();
                            RaisePropertyChanged("AddCrewDetailOver");

                            CheckErrorMessage.CheckErrorMessages = false;

                     }
                      new CrewDetailViewModel();
                   
                     new ViewModelReports.CrewDetailViewModel();
                     new DetailWorkRestHoursViewModel();
                     new HODViewModel();
              }


              new public event PropertyChangedEventHandler PropertyChanged;
              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }

       }
}
