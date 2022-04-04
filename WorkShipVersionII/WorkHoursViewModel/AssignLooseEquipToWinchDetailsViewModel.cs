using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class AssignLooseEquipToWinchDetailsViewModel :ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public AssignLooseEquipToWinchDetailsViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            viewCommand = new RelayCommand<AssignLooseEquipTypeClass>(Viewropediscard);
            editCommand = new RelayCommand<AssignLooseEquipTypeClass>(EditLooseE);
            deleteCommand = new RelayCommand<AssignLooseEquipTypeClass>(DeleteAssignLooseE);

            //LoadMooringWinchList.Clear();

            // LoadMooringWinchList = sc.MooringWinch.ToList();
            //var data = sc.MooringWinch.ToList();
            // sc.ObservableCollectionList(LoadMooringWinchList, GetMooringWinchList());
            // GetAssignList();
            // OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringWinchList"));
             GetAssignList();
            // OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

        }
        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }
        public AssignLooseEquipToWinchDetailsViewModel(ObservableCollection<AssignLooseEquipTypeClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

           // LoadUserAccess.Clear();

            GetAssignList();
            //foreach (var item in ass)
            //{
            //    LoadUserAccess.Add(new AssignModuleToWinchClass() { Id = item.Id, AssignedNumber=item.AssignedNumber,CertificateNumber=item.CertificateNumber,CreatedDate=item.CreatedDate });
            //}

        }
        private void Viewropediscard(AssignLooseEquipTypeClass mw)
        {
            try
            {

                StaticHelper.ViewId = mw.Id;

                ChildWindowManager.Instance.ShowChildWindow(new ViewAssignLooseEtoWinch());

                //ChildWindowManager.Instance.CloseChildWindow();
            }
            catch
            {

            }
        }
        private static string searchCrew1;
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

        private static ObservableCollection<AssignLooseEquipTypeClass> loadMooringWinchList = new ObservableCollection<AssignLooseEquipTypeClass>();

        public ObservableCollection<AssignLooseEquipTypeClass> LoadMooringWinchList
        {
            get
            {
                return loadMooringWinchList;
            }
            set
            {
                loadMooringWinchList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringWinchList"));

            }
        }


        public static ObservableCollection<AssignLooseEquipTypeClass> loadUserAccess = new ObservableCollection<AssignLooseEquipTypeClass>();
        public ObservableCollection<AssignLooseEquipTypeClass> LoadUserAccess
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

        public void GetAssignList()
        {

            try
            {
                LoadUserAccess.Clear();
                //ObservableCollection<AssignLooseEquipTypeClass> moringlist = new ObservableCollection<AssignLooseEquipTypeClass>();

                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);

                SqlCommand cmd = new SqlCommand("AssignLooseEquipDetail", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;


                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                
               
                DataTable ds = new DataTable();
                adp.Fill(ds);
                // var data = sc.AssignRopetoWinch.ToList();          
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess.Add(new AssignLooseEquipTypeClass()
                    {
                        Id = (int)row["Id"],
                        AssignedNumber = (string)row["assignednumber"],
                        Location = (string)row["location"],
                        Looseequipmenttype = (string)row["Looseequipmenttype"],
                        //CreatedDate = (DateTime)row["createddate"],
                        CreatedDate1 = ((DateTime)row["CreatedDate"]).ToString("d MMM, yyyy"),
                        LooseETypeId = (int)row["LooseETypeId"],
                        AssignWinchId = (int)row["AssignWinchId"],
                        CreatedBy = (string)row["Createdby"]

                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                //return moringlist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                //return null;
            }

        }
        //private static DBManager dm;
        //public DataSet GetHolidays()
        //{
        //    dm.Open();
        //    var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetHolidays");
        //    dm.Close();

        //    return data;
        //}
        private void EditLooseE(AssignLooseEquipTypeClass mw)
        {
            try
            {
                AssignLooseEquipToWinchViewModel vm = new AssignLooseEquipToWinchViewModel(mw);
                ChildWindowManager.Instance.ShowChildWindow(new AssignLooseEquipToWinchView() { DataContext = vm });
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void DeleteAssignLooseE(AssignLooseEquipTypeClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AssignLooseEquipTypeClass findrank = sc.AssignLooseEtoWinch.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findrank != null)
                    {
                        sc.Entry(findrank).State = EntityState.Deleted;
                        sc.SaveChanges();
                        MessageBox.Show("Record deleted successfully ", "Delete Assign RopetoWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetAssignList();                       
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
