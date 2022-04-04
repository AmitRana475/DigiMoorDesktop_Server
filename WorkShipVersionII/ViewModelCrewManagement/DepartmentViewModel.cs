using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
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
    public class DepartmentViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public DepartmentViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            editCommand = new RelayCommand<DepartmentClass>(EditDepartment);
            deleteCommand = new RelayCommand<DepartmentClass>(DeleteDepartment);

            loadDepartmentList.Clear();
            sc.ObservableCollectionList(loadDepartmentList, GetDepartmentList);

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

        private static ObservableCollection<DepartmentClass> loadDepartmentList = new ObservableCollection<DepartmentClass>();

        public ObservableCollection<DepartmentClass> LoadDepartmentList
        {
            get
            {
                return loadDepartmentList;
            }
            set
            {
                loadDepartmentList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadDepartmentList"));

            }
        }
        private List<DepartmentClass> GetDepartmentList
        {
            get
            {
                try
                {
                    var data = sc.Departments.ToList();
                    return data;
                }
                catch (Exception ex)
                {
                    sc.ErrorLog(ex);
                    return null;
                }
            }
        }

        private void EditDepartment(DepartmentClass dept)
        {
            try
            {
                AddDepartmentViewModel vm = new AddDepartmentViewModel(dept);
                ChildWindowManager.Instance.ShowChildWindow(new AddDepartmentView() { DataContext = vm });
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void DeleteDepartment(DepartmentClass dept)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DepartmentClass findrank = sc.Departments.Where(x => x.did == dept.did).FirstOrDefault();
                    if (findrank != null)
                    {

                        var checkDepartment = sc.CrewDetails.Where(x => x.did.Equals(dept.did)).FirstOrDefault();
                        if (checkDepartment != null)
                        {
                            MessageBox.Show("Sorry you can't delete this Department, as it is assigned to some crew members.", "Delete  Department", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                        else
                        {

                            sc.Entry(findrank).State = EntityState.Deleted;
                            sc.SaveChanges();

                            MessageBox.Show("Record deleted successfully ", "Delete Department", MessageBoxButton.OK, MessageBoxImage.Information);

                            //.....Refresh DataGrid........


                            LoadDepartmentList.Clear();
                            sc.ObservableCollectionList(LoadDepartmentList, GetDepartmentList);

                            //.....End Refresh DataGrid........
                        }
                    }
                    else
                    {
                        MessageBox.Show("Record is not found ", "Delete Department ", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
