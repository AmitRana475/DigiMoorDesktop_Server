using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
    public class HoliDayGroupViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public HoliDayGroupViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            editCommand = new RelayCommand<HoliDayGroupNameClass>(EditHoliDay);
            deleteCommand = new RelayCommand<HoliDayGroupNameClass>(DeleteHoliDay);

            loadHoliDayList = GetHoliDayList();

        }

        public HoliDayGroupViewModel(ObservableCollection<HoliDayGroupNameClass> holi)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            LoadHoliDayList.Clear();
            foreach (var item in holi)
            {
                LoadHoliDayList.Add(new HoliDayGroupNameClass() { Id = item.Id, GroupName = item.GroupName });
            }

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

        private static ObservableCollection<HoliDayGroupNameClass> loadHoliDayList = new ObservableCollection<HoliDayGroupNameClass>();

        public ObservableCollection<HoliDayGroupNameClass> LoadHoliDayList
        {
            get
            {
                return loadHoliDayList;
            }
            set
            {
                loadHoliDayList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadHoliDayList"));

            }
        }
        private ObservableCollection<HoliDayGroupNameClass> GetHoliDayList()
        {
            var data = sc.HoliDayGroupNames.ToList();
            var loadlist = new ObservableCollection<HoliDayGroupNameClass>();
            foreach (var item in data)
            {
                loadlist.Add(new HoliDayGroupNameClass() { Id = item.Id, GroupName = item.GroupName });
            }

            return loadlist;
        }

        private void EditHoliDay(HoliDayGroupNameClass dept)
        {
            AddHoliDayGroupViewModel vm = new AddHoliDayGroupViewModel(dept);
            ChildWindowManager.Instance.ShowChildWindow(new AddHoliDayGroupView() { DataContext = vm });
        }

        private void DeleteHoliDay(HoliDayGroupNameClass dept)
        {
            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                HoliDayGroupNameClass findrank = sc.HoliDayGroupNames.Where(x => x.Id == dept.Id).FirstOrDefault();
                if (findrank != null)
                {


                    sc.Entry(findrank).State = EntityState.Deleted;
                    sc.SaveChanges();

                    MessageBox.Show("Record deleted successfully", "Delete HoliDayGroup");


                    //.....Refresh DataGrid........

                    var data = new ObservableCollection<HoliDayGroupNameClass>(sc.HoliDayGroupNames.ToList());
                    LoadHoliDayList.Clear();
                    foreach (var item in data)
                    {
                        LoadHoliDayList.Add(new HoliDayGroupNameClass { Id = item.Id, GroupName = item.GroupName });
                    }

                    //.....End Refresh DataGrid........

                }
                else
                {
                    MessageBox.Show("Record is not found ", "Delete HoliDayGroup");
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
