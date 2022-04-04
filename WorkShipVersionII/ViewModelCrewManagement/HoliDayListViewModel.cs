using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class HoliDayListViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public HoliDayListViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            setGroupName = HoliProperty.GroupName;
            yearname = GetYear();
            editCommand = new RelayCommand<HolidaysClass>(EditHoliDay);
            deleteCommand = new RelayCommand<HolidaysClass>(DeleteHoliDay);

            loadHoliDayList1 = GetHoliDayList1(SYearName);

        }

        public HoliDayListViewModel(ObservableCollection<HolidaysClass> HoliList)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            var data = HoliList.OrderBy(s => s.HolidayDate).ToList();
            LoadHoliDayList1.Clear();
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.Name))
                    LoadHoliDayList1.Add(new HolidaysClass() { Id = item.Id, Name = item.Name, HolidayDate = item.HolidayDate, Gid = item.Gid });
            }
        }


        private static ObservableCollection<HolidaysClass> loadHoliDayList1 = new ObservableCollection<HolidaysClass>();
        public ObservableCollection<HolidaysClass> LoadHoliDayList1
        {
            get
            {
                return loadHoliDayList1;
            }
            set
            {
                loadHoliDayList1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadHoliDayList1"));

            }
        }
        private ObservableCollection<HolidaysClass> GetHoliDayList1(string yearnames)
        {

            var data = sc.Holidays.Where(x => x.Gid == HoliProperty.Gid && x.HolidayDate.Year.ToString() == yearnames).OrderBy(s => s.HolidayDate).ToList();
            var loadlist = new ObservableCollection<HolidaysClass>();
            foreach (var item in data)
            {
                loadlist.Add(new HolidaysClass() { Id = item.Id, Name = item.Name, HolidayDate = item.HolidayDate, Gid = item.Gid });
            }

            return loadlist;
        }


        private string syearname;
        public string SYearName
        {
            get
            {
                if (syearname == null)
                    syearname = DateTime.Now.Year.ToString();
                LoadHoliDayList1.Clear();
                var HoliList = sc.Holidays.Where(x => x.Gid == HoliProperty.Gid && x.HolidayDate.Year.ToString() == syearname).OrderBy(s => s.HolidayDate).ToList();
                foreach (var item in HoliList)
                {
                    LoadHoliDayList1.Add(new HolidaysClass() { Id = item.Id, Name = item.Name, HolidayDate = item.HolidayDate, Gid = item.Gid });
                }

                return syearname;
            }

            set { syearname = value; OnPropertyChanged(new PropertyChangedEventArgs("SYearName")); }
        }


        private static ObservableCollection<string> yearname = new ObservableCollection<string>();
        public ObservableCollection<string> YearName
        {
            get
            {
                return yearname;
            }
            set
            {
                yearname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("YearName"));
            }
        }

        private ObservableCollection<string> GetYear()
        {
            var years = new ObservableCollection<string>();
            int year = DateTime.Now.Year;
            for (int i = year - 7; i <= year + 7; i++)
            {
                years.Add(i.ToString());
            }
            return years;
        }


        private string setGroupName;
        public string SetGroupName
        {
            get { return setGroupName; }
            set { setGroupName = value; OnPropertyChanged(new PropertyChangedEventArgs("SetGroupName")); }
        }

        private ICommand editCommand;
        public ICommand EditCommand
        {
            get { return editCommand; }
            set { editCommand = value; }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }

        private void EditHoliDay(HolidaysClass dept)
        {
            AddHoliDayListViewModel vm = new AddHoliDayListViewModel(dept);
            ChildWindowManager.Instance.ShowChildWindow(new AddHoliDayListView() { DataContext = vm });
        }

        private void DeleteHoliDay(HolidaysClass dept)
        {
            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                HolidaysClass findrank = sc.Holidays.Where(x => x.Id == dept.Id).FirstOrDefault();
                if (findrank != null)
                {


                    sc.Entry(findrank).State = EntityState.Deleted;
                    sc.SaveChanges();

                    MessageBox.Show("Record deleted successfully", "Delete HoliDayList");


                    //.....Refresh DataGrid........

                    var data = new ObservableCollection<HolidaysClass>(sc.Holidays.Where(x => x.Gid == HoliProperty.Gid && x.HolidayDate.Year.ToString() == SYearName).ToList());
                    LoadHoliDayList1.Clear();
                    foreach (var item in data)
                    {
                        LoadHoliDayList1.Add(new HolidaysClass { Id = item.Id, Name = item.Name, HolidayDate = item.HolidayDate, Gid = item.Gid });
                    }

                    //.....End Refresh DataGrid........

                }
                else
                {
                    MessageBox.Show("Record is not found ", "Delete HoliDayList");
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
