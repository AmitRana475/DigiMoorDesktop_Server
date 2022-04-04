using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Data.Entity;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class GroupPlanningViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public GroupPlanningViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            deleteCommand = new RelayCommand<AddGroupClass>(DeleteMethod);
            editCommand = new RelayCommand<AddGroupClass>(EditMethod);
            loadAddGroup.Clear();
            loadAddGroup = GetAddGroupDetail();
        }

        private void EditMethod(AddGroupClass obj)
        {
            AddGroupPlanningViewModel vm = new AddGroupPlanningViewModel(obj);
            ChildWindowManager.Instance.ShowChildWindow(new AddGroupPlanningView() { DataContext = vm });
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }

        }
        private ICommand editCommand;
        public ICommand EditCommand
        {
            get
            {
                return editCommand;
            }
        }


        public static ObservableCollection<AddGroupClass> loadAddGroup = new ObservableCollection<AddGroupClass>();
        public ObservableCollection<AddGroupClass> LoadAddGroup
        {
            get
            {

                return loadAddGroup;
            }
            set
            {
                loadAddGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadAddGroup"));
            }
        }


        private ObservableCollection<AddGroupClass> GetAddGroupDetail()
        {
            var data = sc.AddGroups.ToList();
            var ranklist = new ObservableCollection<AddGroupClass>();

            foreach (var item in data)
            {
                ranklist.Add(new AddGroupClass()
                {
                    GroupID = item.GroupID,
                    GroupName = item.GroupName,
                    GroupMember = item.GroupMember,
                    GFullName = item.GFullName,
                    GRank = item.GRank,
                    GUser = item.GUser
                });
            }

            return ranklist;
        }

        private void DeleteMethod(AddGroupClass obj)
        {
            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var local = sc.Set<AddGroupClass>()
                    .Local
                    .FirstOrDefault(f => f.GroupID == obj.GroupID);
                if (local != null)
                {
                    sc.Entry(obj).State = EntityState.Detached;
                }

                var user = sc.AddGroups.Where(x => x.GroupID.Equals(obj.GroupID)).FirstOrDefault();
                sc.Entry(user).State = EntityState.Deleted;
                sc.SaveChanges();


                MessageBox.Show("Record deleted successfully");
                loadAddGroup = GetAddGroupDetail();
                RaisePropertyChanged("LoadAddGroup");
            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
