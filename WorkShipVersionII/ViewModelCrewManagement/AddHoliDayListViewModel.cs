using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class AddHoliDayListViewModel : ViewModelBase
    {
        int years;
        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddHoliDayListViewModel(HolidaysClass edeps)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            saveCommand = new RelayCommand<HolidaysClass>(UpdateDepartment);
            cancelCommand = new RelayCommand(CancelDepartment);
            EditDepartment(edeps);
        }
        public AddHoliDayListViewModel()
        {
             if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            saveCommand = new RelayCommand<HolidaysClass>(SaveDepartment);
            cancelCommand = new RelayCommand(CancelDepartment);
        }


        private string holiDayName;
        public string HoliDayName
        {
            get { return holiDayName; }
            set { holiDayName = value; OnPropertyChanged(new PropertyChangedEventArgs("HoliDayName")); }
        }


        private HolidaysClass _addHoliDayList = new HolidaysClass();
        public HolidaysClass AddHoliDayList1
        {
            get
            {
                HoliDayName = string.Empty;
                //if (_addHoliDayList.HolidayDate == null)
                //    _addHoliDayList.HolidayDate = DateTime.Today;
                _addHoliDayList.Gid = HoliProperty.Gid;
                RaisePropertyChanged("HoliDayName");
                return _addHoliDayList;
            }
            set
            {
                _addHoliDayList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddHoliDayList1"));
            }
        }


        private Nullable<DateTime> myDateTimeProperty = null;
        public Nullable<DateTime> MyDateTimeProperty
        {
            get
            {
                if (myDateTimeProperty == null)
                {
                    myDateTimeProperty = DateTime.Today;
                }
                _addHoliDayList.HolidayDate = Convert.ToDateTime(myDateTimeProperty);
                return myDateTimeProperty;
            }
            set
            {

                myDateTimeProperty = value;
                RaisePropertyChanged("MyDateTimeProperty");
            }
        }


        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }


        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }

        private void SaveDepartment(HolidaysClass holic)
        {

           
            holic.Name = holic.Name != null ? holic.Name.Trim() : holic.Name;
            if (!string.IsNullOrEmpty(holic.Name))
            {
                var findrank = sc.Holidays.Where(x => x.Name == holic.Name).FirstOrDefault();
                if (findrank == null)
                {
                    holic.Name = textinfo.ToTitleCase(holic.Name.ToLower());

                    sc.Holidays.Add(holic);
                    sc.SaveChanges();
                    MessageBox.Show("Record saved successfully", "Add HoliDayList");


                    AddHoliDayList1.Name = string.Empty;
                    RaisePropertyChanged("AddHoliDayList1");

                }
                else
                {
                    MessageBox.Show("HoliDay already exist", "Add HoliDayList");
                }
            }
            else
            {

                HoliDayName = "Please Enter the HoliDay Name";
                RaisePropertyChanged("HoliDayName");
            }
        }

        private void UpdateDepartment(HolidaysClass holic)
        {
            holic.Name = holic.Name != null ? holic.Name.Trim() : holic.Name;
            if (!string.IsNullOrEmpty(holic.Name))
            {

                var findrank = sc.Holidays.Where(x => x.Id == holic.Id).FirstOrDefault();

                if (findrank != null)
                {
                    holic.Name = textinfo.ToTitleCase(holic.Name.ToLower());



                    var local = sc.Set<HolidaysClass>()
                     .Local
                     .FirstOrDefault(f => f.Id == holic.Id);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }

                    var UpdatedLocation = new HolidaysClass()
                    {

                        Id = holic.Id,
                        Name = holic.Name,
                        HolidayDate = holic.HolidayDate,
                        Gid = holic.Gid
                    };

                    sc.Entry(UpdatedLocation).State = EntityState.Modified;
                    sc.SaveChanges();
                    MessageBox.Show("Record updated successfully", "Update HoliDayList");



                    CancelDepartment();

                }

            }
            else
            {

                HoliDayName = "Please Enter the HoliDay Name";
                RaisePropertyChanged("HoliDayName");
            }


        }


        private void EditDepartment(HolidaysClass holic)
        {
            years = holic.HolidayDate.Year;
            var findrank = sc.Holidays.Where(x => x.Id == holic.Id).FirstOrDefault();
            AddHoliDayList1.Name = findrank.Name;
            MyDateTimeProperty = findrank.HolidayDate;
            AddHoliDayList1.Id = findrank.Id;
            AddHoliDayList1.Gid = findrank.Gid;
            OnPropertyChanged(new PropertyChangedEventArgs("AddHoliDayList1"));


        }

        private void CancelDepartment()
        {
            if (years == 0)
                years = DateTime.Now.Year;

            AddHoliDayList1 = new HolidaysClass();
            RaisePropertyChanged("AddHoliDayList1");

            var lostdata = new ObservableCollection<HolidaysClass>(sc.Holidays.Where(x => x.Gid == HoliProperty.Gid && x.HolidayDate.Year == years).ToList());
            HoliDayListViewModel cc = new HoliDayListViewModel(lostdata);
            ChildWindowManager.Instance.CloseChildWindow();
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }




    }
}
