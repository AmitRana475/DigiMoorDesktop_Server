using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
    public class AddHoliDayGroupViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddHoliDayGroupViewModel()
        {
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<HoliDayGroupNameClass>(SaveGroup);
            cancelCommand = new RelayCommand(CancelGroup);
        }

        public AddHoliDayGroupViewModel(HoliDayGroupNameClass holi)
        {
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            saveCommand = new RelayCommand<HoliDayGroupNameClass>(UpdateGroup);
            cancelCommand = new RelayCommand(CancelGroup);

            EditHoliDayGroup(holi);

        }




        private string holidayMessage;
        public string HoliDayMessage
        {
            get
            {
                return holidayMessage;
            }
            set
            {
                holidayMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HoliDayMessage"));
            }
        }

        private HoliDayGroupNameClass _AddHolidayGroup = new HoliDayGroupNameClass();
        public HoliDayGroupNameClass AddHolidayGroup
        {
            get
            {
                HoliDayMessage = string.Empty;
                RaisePropertyChanged("HoliDayMessage");
                return _AddHolidayGroup;
            }
            set
            {
                _AddHolidayGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddHolidayGroup"));
            }
        }




        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand;
            }
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand;
            }
        }

        private void SaveGroup(HoliDayGroupNameClass holi)
        {
            holi.GroupName = holi.GroupName != null ? holi.GroupName.Trim() : holi.GroupName;

            if (!string.IsNullOrEmpty(holi.GroupName))
            {
                var findrank = sc.HoliDayGroupNames.Where(x => x.GroupName == holi.GroupName).FirstOrDefault();

                if (findrank == null)
                {
                    holi.GroupName = textinfo.ToTitleCase(holi.GroupName.ToLower());

                    sc.HoliDayGroupNames.Add(holi);
                    sc.SaveChanges();
                    MessageBox.Show("Record saved successfully", "Add HolyDayGroup");

                    AddHolidayGroup = new HoliDayGroupNameClass();
                    RaisePropertyChanged("AddHolidayGroup");


                }
                else
                {
                    MessageBox.Show("Department already exist", "Add HolyDayGroup");
                }
            }
            else
            {

                HoliDayMessage = "Please Enter the HoliDayGroup Name";
                RaisePropertyChanged("HoliDayMessage");
            }


        }

        private void UpdateGroup(HoliDayGroupNameClass holi)
        {
            holi.GroupName = holi.GroupName != null ? holi.GroupName.Trim() : holi.GroupName;
            if (!string.IsNullOrEmpty(holi.GroupName))
            {

                var findrank = sc.HoliDayGroupNames.Where(x => x.Id == holi.Id).FirstOrDefault();

                if (findrank != null)
                {
                    holi.GroupName = textinfo.ToTitleCase(holi.GroupName.ToLower());



                    var local = sc.Set<HoliDayGroupNameClass>()
                     .Local
                     .FirstOrDefault(f => f.Id == holi.Id);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }

                    var UpdatedLocation = new HoliDayGroupNameClass()
                    {

                        Id = holi.Id,
                        GroupName = holi.GroupName
                    };

                    sc.Entry(UpdatedLocation).State = EntityState.Modified;
                    sc.SaveChanges();
                    MessageBox.Show("Record updated successfully", "Update HoliDayGroup");



                    CancelGroup();

                }

            }
            else
            {

                HoliDayMessage = "Please Enter the HoliDayGroup Name";
                RaisePropertyChanged("HoliDayMessage");
            }


        }


        private void EditHoliDayGroup(HoliDayGroupNameClass holi)
        {

            var findrank = sc.HoliDayGroupNames.Where(x => x.Id == holi.Id).FirstOrDefault();
            AddHolidayGroup.GroupName = findrank.GroupName;
            AddHolidayGroup.Id = findrank.Id;
            OnPropertyChanged(new PropertyChangedEventArgs("AddHolidayGroup"));


        }

        private void CancelGroup()
        {
            var lostdata = new ObservableCollection<HoliDayGroupNameClass>(sc.HoliDayGroupNames.ToList());
            HoliDayGroupViewModel cc = new HoliDayGroupViewModel(lostdata);

            ChildWindowManager.Instance.CloseChildWindow();
        }



        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
