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
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.ViewModelCrewManagement
{

    public class CrewDetailViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public CrewDetailViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            //editCommand = new RelayCommand<CrewDetailClass>(EditRank);
            deleteCommand = new RelayCommand<CrewDetailClass>(DeleteCrew);

            if (UserTypeClass.UserTypes == "HOD")
            {
                string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');

                var data = sc.GetCrewDetail(searchCrew).ToList();
                data = data.Where(x => depart.Contains(x.department)).ToList();

                var data1 = sc.GetCrewDetail1(searchCrew1).ToList();
                data1 = data1.Where(x => depart.Contains(x.department)).ToList();

                LoadCrewDetail.Clear();
                sc.ObservableCollectionList(LoadCrewDetail, data);
                LoadCrewDetail1.Clear();
                sc.ObservableCollectionList(LoadCrewDetail1, data1);

            }
            else
            {
                LoadCrewDetail.Clear();
                sc.ObservableCollectionList(LoadCrewDetail, sc.GetCrewDetail(searchCrew));
                LoadCrewDetail1.Clear();
                sc.ObservableCollectionList(LoadCrewDetail1, sc.GetCrewDetail1(searchCrew1));
            }

            //LoadCrewDetail = GetCrewDetail(searchCrew);
            //LoadCrewDetail1 = GetCrewDetail1(searchCrew1);
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


        private static string searchCrew;
        public string SearchCrew
        {
            get
            {
                if (searchCrew != null)
                {

                    if (UserTypeClass.UserTypes == "HOD")
                    {
                        string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');

                        var data = sc.GetCrewDetail(searchCrew).ToList();
                        data = data.Where(x => depart.Contains(x.department)).ToList();

                        LoadCrewDetail.Clear();
                        sc.ObservableCollectionList(LoadCrewDetail, data);
                        RaisePropertyChanged("loadCrewDetail");

                    }
                    else
                    {
                        LoadCrewDetail.Clear();
                        sc.ObservableCollectionList(LoadCrewDetail, sc.GetCrewDetail(searchCrew));
                        RaisePropertyChanged("loadCrewDetail");
                    }

                }
                return searchCrew;
            }

            set
            {
                searchCrew = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SearchCrew"));
            }
        }

        private static string searchCrew1;
        public string SearchCrew1
        {
            get
            {
                if (searchCrew1 != null)
                {

                    if (UserTypeClass.UserTypes == "HOD")
                    {
                        string[] depart = UserTypeClass.HODAccess.DepartmentName.TrimEnd(',').Split(',');

                        var data1 = sc.GetCrewDetail1(searchCrew1).ToList();
                        data1 = data1.Where(x => depart.Contains(x.department)).ToList();

                        LoadCrewDetail1.Clear();
                        sc.ObservableCollectionList(LoadCrewDetail1, data1);
                        RaisePropertyChanged("loadCrewDetail1");

                    }
                    else
                    {
                        LoadCrewDetail1.Clear();
                        sc.ObservableCollectionList(LoadCrewDetail1, sc.GetCrewDetail1(searchCrew1));
                        RaisePropertyChanged("loadCrewDetail1");
                    }

                }
                return searchCrew1;
            }

            set
            {
                searchCrew1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SearchCrew1"));
            }
        }



        private void DeleteCrew(CrewDetailClass cdc)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    CrewDetailClass findrank = sc.CrewDetails.Where(x => x.Id == cdc.Id).FirstOrDefault();
                    if (findrank != null)
                    {


                        // Detete into WorkHours's Table
                        var deleteWorkhurs = sc.WorkHourss
                        .Where(w => w.UserName == cdc.UserName.Trim() && w.Position == cdc.position.Trim()).ToList();

                        sc.WorkHourss.RemoveRange(deleteWorkhurs);
                        sc.SaveChanges();


                        // Detete into User's Table
                        sc.Entry(findrank).State = EntityState.Deleted;
                        sc.SaveChanges();





                        MessageBox.Show("Record deleted successfully", "Delete Crew Detail", MessageBoxButton.OK, MessageBoxImage.Information);

                        //.....Refresh DataGrid........


                        LoadCrewDetail.Clear();
                        sc.ObservableCollectionList(LoadCrewDetail, sc.GetCrewDetail(searchCrew));
                        LoadCrewDetail1.Clear();
                        sc.ObservableCollectionList(LoadCrewDetail1, sc.GetCrewDetail1(searchCrew1));

                        cdc = new CrewDetailClass();

                        new ViewModelReports.CrewDetailViewModel();
                        new DetailWorkRestHoursViewModel();
                        //.....End Refresh DataGrid........

                    }
                    else
                    {

                        MessageBox.Show("Record is not found ", "Delete Crew Detail", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
                     
        private static ObservableCollection<CrewDetailClass> loadCrewDetail = new ObservableCollection<CrewDetailClass>();
        public ObservableCollection<CrewDetailClass> LoadCrewDetail
        {
            get
            {
                return loadCrewDetail;
            }
            set
            {
                loadCrewDetail = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCrewDetail"));
            }
        }
        private static ObservableCollection<CrewDetailClass> loadCrewDetail1 = new ObservableCollection<CrewDetailClass>();
        public ObservableCollection<CrewDetailClass> LoadCrewDetail1
        {
            get
            {
                return loadCrewDetail1;
            }
            set
            {
                loadCrewDetail1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCrewDetail1"));
            }
        }



        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
