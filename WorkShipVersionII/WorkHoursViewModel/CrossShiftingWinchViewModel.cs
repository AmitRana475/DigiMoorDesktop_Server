using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class CrossShiftingWinchViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public CrossShiftingWinchViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            editCommand = new RelayCommand<CrossShiftingWinchClass>(EditCrossShiftingWinch);
            deleteCommand = new RelayCommand<CrossShiftingWinchClass>(DeleteCrossShiftingWinch);
            LoadCrossShiftingWinchList = GetCrossShiftList();         
            LoadUserAccess = GetCrossShiftList();       

        }

        public CrossShiftingWinchViewModel(ObservableCollection<CrossShiftingWinchClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            LoadUserAccess.Clear();

            LoadUserAccess = GetCrossShiftList();
          
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

        private static ObservableCollection<CrossShiftingWinchClass> loadCrossShiftingWinchList = new ObservableCollection<CrossShiftingWinchClass>();

        public ObservableCollection<CrossShiftingWinchClass> LoadCrossShiftingWinchList
        {
            get
            {
                return loadCrossShiftingWinchList;
            }
            set
            {
                loadCrossShiftingWinchList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCrossShiftingWinchList"));

            }
        }


        public static ObservableCollection<CrossShiftingWinchClass> loadUserAccess = new ObservableCollection<CrossShiftingWinchClass>();
        public ObservableCollection<CrossShiftingWinchClass> LoadUserAccess
        {
            get
            {
                return loadUserAccess;
            }
            set
            {
                loadUserAccess = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
            }
        }

        private ObservableCollection<CrossShiftingWinchClass> GetCrossShiftList()
        {

            try
            {
                ObservableCollection<CrossShiftingWinchClass> shiftinglist = new ObservableCollection<CrossShiftingWinchClass>();

                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                SqlCommand cmd = new SqlCommand("GetCrossShiftingWinch", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;


                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);                 
                foreach (DataRow row in ds.Rows)
                {
                    shiftinglist.Add(new CrossShiftingWinchClass()
                    {
                        Id = (int)row["Id"],
                        AssignedNumber = (string)row["AssignedNumber"],
                        CertificateNumber = (string)row["CertificateNumber"],
                        AssignedLocation = (string)row["Location"],
                        DateofShifting = (DateTime)row["DateofShifting"],
                        CurrentOutboardEndinUse = (string)row["CurrentOutboardEndinUse"],
                    });
                }

                return shiftinglist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                return null;
            }

        }
       
        private void EditCrossShiftingWinch(CrossShiftingWinchClass mw)
        {
            try
            {
                //AddRopeEndtoEndViewModel vm = new AddRopeEndtoEndViewModel(mw);
                //ChildWindowManager.Instance.ShowChildWindow(new AssignRopeToWinchView() { DataContext = vm });
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void DeleteCrossShiftingWinch(CrossShiftingWinchClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    CrossShiftingWinchClass findcrs = sc.CrossShiftingWinches.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findcrs != null)
                    {

                        //var checkDepartment = sc.AssignRopetoWinch.Where(x => x.Id.Equals(mw.Id)).FirstOrDefault();
                        //if (checkDepartment != null)
                        //{
                        //    MessageBox.Show("Sorry you can't delete this Department, as it is assigned to some crew members.", "Delete  Department", MessageBoxButton.OK, MessageBoxImage.Stop);
                        //}
                        //else
                        //{

                        sc.Entry(findcrs).State = EntityState.Deleted;
                        sc.SaveChanges();

                        MessageBox.Show("Record deleted successfully ", "Delete CrossShifting", MessageBoxButton.OK, MessageBoxImage.Information);

                        //.....Refresh DataGrid........


                        //LoadMooringWinchList.Clear();


                        LoadUserAccess = GetCrossShiftList();
                        //sc.ObservableCollectionList(LoadMooringWinchList, GetEndtoEndList);

                        //.....End Refresh DataGrid........
                        //}
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
