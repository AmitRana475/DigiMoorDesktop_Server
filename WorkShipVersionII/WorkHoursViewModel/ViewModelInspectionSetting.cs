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
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
   public class ViewModelInspectionSetting :ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public ICommand HelpCommand { get; private set; }
        public ViewModelInspectionSetting()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            viewCommand = new RelayCommand<MooringLooseEquipInspectionClass>(ViewLooseEInspection);
            editCommand = new RelayCommand<MooringLooseEquipInspectionClass>(EditInspection);
            deleteCommand = new RelayCommand<MooringLooseEquipInspectionClass>(DeleteInspection);
            //GetInspectionList();
        }

        public ViewModelInspectionSetting(ObservableCollection<MooringLooseEquipInspectionClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            //GetInspectionList();

        }
        private void ViewLooseEInspection(MooringLooseEquipInspectionClass mw)
        {
            try
            {

                StaticHelper.ViewId = mw.Id;
                ChildWindowManager.Instance.ShowChildWindow(new ViewLooseEInspection());


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
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

        public static ObservableCollection<MooringLooseEquipInspectionClass> loadUserAccess = new ObservableCollection<MooringLooseEquipInspectionClass>();
        public ObservableCollection<MooringLooseEquipInspectionClass> LoadUserAccess
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
        public void GetInspectionList()
        {
            try
            {
                LoadUserAccess.Clear();
                SqlCommand cmd = new SqlCommand("select  a.*,b.looseequipmenttype from tblLooseEquipInspectionSetting a inner join looseetype b on a.EquipmentType=b.Id", sc.con);
                //cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess.Add(new MooringLooseEquipInspectionClass()
                    {
                        Id = (int)row["Id"],                       
                        looseequipmenttype = (string)row["looseequipmenttype"],
                        InspectionFrequency = (int)row["InspectionFrequency"],
                        MaximumRunningHours = (int)row["MaximumRunningHours"],
                        MaximumMonthsAllowed = (int)row["MaximumMonthsAllowed"],
                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void EditInspection(MooringLooseEquipInspectionClass mw)
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
        private void DeleteInspection(MooringLooseEquipInspectionClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MooringLooseEquipInspectionClass findLooseEI = sc.LooseEquipInspection.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findLooseEI != null)
                    {
                        sc.Entry(findLooseEI).State = EntityState.Deleted;
                        sc.SaveChanges();


                        try
                        {
                            var notiid = findLooseEI.NotificationId;
                            var notidelete = sc.Notifications.Where(x => x.Id == notiid).FirstOrDefault();
                            sc.Entry(notidelete).State = EntityState.Deleted;
                            sc.SaveChanges();
                        }
                        catch { }

                        MessageBox.Show("Record deleted successfully ", "Delete Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                        var lostdata = new ObservableCollection<MooringLooseEquipInspectionClass>(sc.LooseEquipInspection.ToList());
                        LooseEquipInspectionListViewModel cc = new LooseEquipInspectionListViewModel(lostdata);
                    }
                    else
                    {
                        MessageBox.Show("Record is not found ", "Delete Inspection ", MessageBoxButton.OK, MessageBoxImage.Information);
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
