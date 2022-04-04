using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Data;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsMooringCalulator;

namespace WorkShipVersionII.ViewModelMooringCalculator
{
    public class InputsVesselViewModel : ViewModelBase
    {
        private ShipmentContaxt sc;

        private IRepository<GeneralP> GeneralPs;
        private IRepository<VesselP> VesselPs;
        private IRepository<WindAreas> WindAreass;

        private IRepository<WindandCurrent> WindandCurrents;

        private IRepository<MooringLines> MooringLiness;


        public ICommand HelpCommand { get; private set; }
        public InputsVesselViewModel()
        {
            sc = new ShipmentContaxt();
            openSaveForm = new RelayCommand(() => ExecuteSaveForm());
            GeneralPs = new Repository<GeneralP>();
            VesselPs = new Repository<VesselP>();
            WindAreass = new Repository<WindAreas>();

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            //var list = GeneralPs.GetList().ToList();
            //list.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

            //LoadGeneralP.Clear();
            //sc.ObservableCollectionList(loadGeneralP, list);
            //RaisePropertyChanged("LoadGeneralP");

            //var list1 = VesselPs.GetList().ToList();
            //list1.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

            //loadVesselP.Clear();
            //sc.ObservableCollectionList(loadVesselP, list1);
            //RaisePropertyChanged("LoadVesselP");

            //var list2 = WindAreass.GetList().ToList();
            //list2.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

            //loadWindAreas.Clear();
            //sc.ObservableCollectionList(loadWindAreas, list2);
            //RaisePropertyChanged("LoadWindAreas");

            updateCommand = new RelayCommand<GeneralP>(UpdateGeneralP);
            updateVesselCommand = new RelayCommand<VesselP>(UpdateVesselP);
            updateWindAreasCommand = new RelayCommand<WindAreas>(UpdateWindAreas);

            WindandCurrents = new Repository<WindandCurrent>();
            updateCommand = new RelayCommand<WindandCurrent>(UpdateMethod);

            //var list3 = WindandCurrents.GetList().ToList();
            //list3.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

            //loadEnvironment.Clear();
            //sc.ObservableCollectionList(loadEnvironment, list3);
            //RaisePropertyChanged("LoadEnvironment");

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

            MooringLiness = new Repository<MooringLines>();
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            deleteCommand = new RelayCommand<MooringLines>(DeleteMethod);

            RefreshInputList();
            //saveName = "Save";
            //RaisePropertyChanged("SaveName");
            //saveCommand = new RelayCommand<MooringLines>(SaveMethod);
            // GetMooringLines();

        }

        // public List<GeneralP>()
        public List<GeneralP> GetGeneralP()
        {
            List<GeneralP> LoadMooring = new List<GeneralP>();
            DateTime dd = DateTime.Now.Date;

            string qry = "Select * from tblGeneralP";
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
            System.Data.DataTable datatbl = new System.Data.DataTable();
            sda.Fill(datatbl);



            foreach (DataRow row in datatbl.Rows)
            {
                LoadMooring.Add(new GeneralP()
                {

                    Id = (Convert.ToInt32(row["Id"])),
                    Name = (row["Name"].ToString()),

                    Description = (row["Description"].ToString()),
                    MainValue = Convert.ToDecimal((row["MainValue"] == DBNull.Value) ? 0 : row["MainValue"]),
                    MainValue1 = Convert.ToDecimal((row["MainValue"] == DBNull.Value) ? 0 : row["MainValue"]),
                    DefaultValue = Convert.ToDecimal((row["DefaultValue"] == DBNull.Value) ? 0 : row["DefaultValue"]),
                    Units = (row["Units"].ToString()),
                });
            }

            return LoadMooring;
        }


        public List<VesselP> GetVesselIP()
        {
            List<VesselP> LoadMooring = new List<VesselP>();
            DateTime dd = DateTime.Now.Date;

            string qry = "Select * from tblVesselP";
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
            System.Data.DataTable datatbl = new System.Data.DataTable();
            sda.Fill(datatbl);



            foreach (DataRow row in datatbl.Rows)
            {
                LoadMooring.Add(new VesselP()
                {

                    Id = (Convert.ToInt32(row["Id"])),
                    Name = (row["Name"].ToString()),

                    Description = (row["Description"].ToString()),
                    MainValue = Convert.ToDecimal((row["MainValue"] == DBNull.Value) ? 0 : row["MainValue"]),
                    MainValue1 = Convert.ToDecimal((row["MainValue"] == DBNull.Value) ? 0 : row["MainValue"]),
                    DefaultValue = Convert.ToDecimal((row["DefaultValue"] == DBNull.Value) ? 0 : row["DefaultValue"]),
                    Units = (row["Units"].ToString()),

                });
            }

            return LoadMooring;
        }

        public List<WindAreas> GetWindAreas()
        {
            List<WindAreas> LoadMooring = new List<WindAreas>();
            DateTime dd = DateTime.Now.Date;

            string qry = "Select * from tblWindAreas";
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
            System.Data.DataTable datatbl = new System.Data.DataTable();
            sda.Fill(datatbl);



            foreach (DataRow row in datatbl.Rows)
            {
                LoadMooring.Add(new WindAreas()
                {

                    Id = (Convert.ToInt32(row["Id"])),
                    Name = (row["Name"].ToString()),

                    Description = (row["Description"].ToString()),
                    MainValue = Convert.ToDecimal((row["MainValue"] == DBNull.Value) ? 0 : row["MainValue"]),
                    MainValue1 = Convert.ToDecimal((row["MainValue"] == DBNull.Value) ? 0 : row["MainValue"]),
                    DefaultValue = Convert.ToDecimal((row["DefaultValue"] == DBNull.Value) ? 0 : row["DefaultValue"]),
                    Units = (row["Units"].ToString()),

                });
            }

            return LoadMooring;
        }

        public List<WindandCurrent> GetWindandCurrent()
        {
            List<WindandCurrent> LoadMooring = new List<WindandCurrent>();
            DateTime dd = DateTime.Now.Date;

            string qry = "Select * from tblWindandCurrent";
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
            System.Data.DataTable datatbl = new System.Data.DataTable();
            sda.Fill(datatbl);



            foreach (DataRow row in datatbl.Rows)
            {
                LoadMooring.Add(new WindandCurrent()
                {

                    Id = (Convert.ToInt32(row["Id"])),
                    Name = (row["Name"].ToString()),

                    Description = (row["Description"].ToString()),
                    //  MainValue = Convert.ToDecimal((row["MainValue"] == DBNull.Value) ? 0 : row["MainValue"]),
                    //  MainValue1 = Convert.ToDecimal((row["MainValue"] == DBNull.Value) ? 0 : row["MainValue"]),
                    //  DefaultValue = Convert.ToDecimal((row["DefaultValue"] == DBNull.Value) ? 0 : row["DefaultValue"]),

                      MainValue = row["MainValue"] == DBNull.Value ? 0.00m : Convert.ToDecimal(row["MainValue"]),
                    MainValue1 = row["MainValue"] == DBNull.Value ? 0.00m : Convert.ToDecimal(row["MainValue"]),
                    DefaultValue = Convert.ToDecimal((row["DefaultValue"] == DBNull.Value) ? 0 : row["DefaultValue"]),
                    Units = (row["Units"].ToString()),

                });
            }

            return LoadMooring;
        }

        public void RefreshInputList()
        {
            var list = GetGeneralP(); // GeneralPs.GetList().ToList();
                                      //list.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

            loadGeneralP.Clear();
            sc.ObservableCollectionList(loadGeneralP, list);
            RaisePropertyChanged("LoadGeneralP");


            var list1 = GetVesselIP(); // VesselPs.GetList().ToList();
            //list1.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

            loadVesselP.Clear();
            sc.ObservableCollectionList(loadVesselP, list1);
            RaisePropertyChanged("LoadVesselP");


            var list2 = GetWindAreas(); //WindAreass.GetList().ToList();
            //list2.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

            loadWindAreas.Clear();
            sc.ObservableCollectionList(loadWindAreas, list2);
            RaisePropertyChanged("LoadWindAreas");


            var list3 = GetWindandCurrent(); //WindandCurrents.GetList().ToList();
            //list3.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

            loadEnvironment.Clear();
            sc.ObservableCollectionList(loadEnvironment, list3);
            RaisePropertyChanged("LoadEnvironment");

            GetMooringLines();
        }

        private ICommand updateVesselCommand;
        public ICommand UpdateVesselCommand
        {
            get { return updateVesselCommand; }

        }

        private ICommand updateWindAreasCommand;
        public ICommand UpdateWindAreasCommand
        {
            get { return updateWindAreasCommand; }

        }


        private ICommand openSaveForm;
        public ICommand OpenSaveForm
        {
            get { return openSaveForm; }
            set { openSaveForm = value; }
        }

        private void ExecuteSaveForm()
        {

            AddMooringLineViewModel vm = new AddMooringLineViewModel();
            ChildWindowManager.Instance.ShowChildWindow(new AddMooringLineView() { DataContext = vm });
        }
        private void UpdateGeneralP(GeneralP obj)
        {
            try
            {

                //using (MooringContext sc1 = new MooringContext())
                //{
                var finddata = GeneralPs.GetList().Where(x => x.Id == obj.Id).FirstOrDefault();
                if (finddata != null)
                {

                    finddata.MainValue = Convert.ToDecimal(obj.MainValue1);


                    GeneralPs.UpdateEntity(finddata);
                    GeneralPs.Save();

                    //sc1.Entry(finddata).State = EntityState.Modified;
                    //sc1.SaveChanges();

                    GeneralPs = new Repository<GeneralP>();

                    var list = GeneralPs.GetList().ToList();
                    list.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

                    LoadGeneralP.Clear();
                    sc.ObservableCollectionList(loadGeneralP, list);
                    RaisePropertyChanged("LoadGeneralP");


                    MessageBox.Show("Data updeted successfully!");

                }
                //}

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateWindAreas(WindAreas obj)
        {
            try
            {

                //using (MooringContext sc1 = new MooringContext())
                //{
                var finddata = WindAreass.GetList().Where(x => x.Id == obj.Id).FirstOrDefault();
                if (finddata != null)
                {

                    finddata.MainValue = Convert.ToDecimal(obj.MainValue1);


                    WindAreass.UpdateEntity(finddata);
                    WindAreass.Save();

                    //sc1.Entry(finddata).State = EntityState.Modified;
                    //sc1.SaveChanges();

                    WindAreass = new Repository<WindAreas>();

                    var list = WindAreass.GetList().ToList();
                    list.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

                    loadWindAreas.Clear();
                    sc.ObservableCollectionList(loadWindAreas, list);
                    RaisePropertyChanged("LoadWindAreas");


                    MessageBox.Show("Data updeted successfully!");

                }
                //}

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateVesselP(VesselP obj)
        {
            try
            {

                //using (MooringContext sc1 = new MooringContext())
                //{
                var finddata = VesselPs.GetList().Where(x => x.Id == obj.Id).FirstOrDefault();
                if (finddata != null)
                {

                    finddata.MainValue = Convert.ToDecimal(obj.MainValue1);


                    VesselPs.UpdateEntity(finddata);
                    VesselPs.Save();

                    //sc1.Entry(finddata).State = EntityState.Modified;
                    //sc1.SaveChanges();

                    VesselPs = new Repository<VesselP>();
                    var list = VesselPs.GetList().ToList();
                    list.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

                    loadVesselP.Clear();
                    sc.ObservableCollectionList(loadVesselP, list);
                    RaisePropertyChanged("LoadVesselP");


                    MessageBox.Show("Data updeted successfully!");

                }
                //}

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        public static ObservableCollection<GeneralP> loadGeneralP = new ObservableCollection<GeneralP>();
        public ObservableCollection<GeneralP> LoadGeneralP
        {
            get
            {
                return loadGeneralP;
            }
            set
            {
                loadGeneralP = value;
                RaisePropertyChanged("LoadGeneralP");
            }
        }


        public static ObservableCollection<VesselP> loadVesselP = new ObservableCollection<VesselP>();
        public ObservableCollection<VesselP> LoadVesselP
        {
            get
            {
                return loadVesselP;
            }
            set
            {
                loadVesselP = value;
                RaisePropertyChanged("LoadVesselP");
            }
        }


        private ObservableCollection<WindAreas> loadWindAreas = new ObservableCollection<WindAreas>();
        public ObservableCollection<WindAreas> LoadWindAreas
        {
            get
            {
                return loadWindAreas;
            }
            set
            {
                loadWindAreas = value;
                RaisePropertyChanged("LoadWindAreas");
            }
        }


        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }

        }
        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get { return updateCommand; }

        }

        private void UpdateMethod(WindandCurrent obj)
        {
            try
            {

                //using (MooringContext sc1 = new MooringContext())
                //{
                var finddata = WindandCurrents.GetListById(obj.Id);
                if (finddata != null)
                {

                    finddata.MainValue = Convert.ToDecimal(obj.MainValue1);


                    WindandCurrents.UpdateEntity(finddata);
                    WindandCurrents.Save();

                    //sc1.Entry(finddata).State = EntityState.Modified;
                    //sc1.SaveChanges();

                    var list = WindandCurrents.GetList().ToList();
                    list.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

                    loadEnvironment.Clear();
                    sc.ObservableCollectionList(loadEnvironment, list);
                    RaisePropertyChanged("LoadVesselP");


                    MessageBox.Show("Data updeted successfully!");

                }
                //}

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public static ObservableCollection<WindandCurrent> loadEnvironment = new ObservableCollection<WindandCurrent>();
        public ObservableCollection<WindandCurrent> LoadEnvironment
        {
            get
            {
                return loadEnvironment;
            }
            set
            {
                loadEnvironment = value;
                RaisePropertyChanged("LoadEnvironment");
            }
        }



        public void GetMooringLines()
        {
            LoadMooring.Clear();
            DateTime dd = DateTime.Now.Date;

            string qry = "MooringLineInputCalulator";
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
            System.Data.DataTable datatbl = new System.Data.DataTable();
            sda.Fill(datatbl);

            var ranklist = new ObservableCollection<MooringLines>();
            loadMooring.Clear();
            foreach (DataRow row in datatbl.Rows)
            {
                loadMooring.Add(new MooringLines()
                {
                    AssignNumber = (row["AssignedNumber"] == DBNull.Value) ? "Not Assigned" : row["AssignedNumber"].ToString(),
                    Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                    Certi_No = (string)row["CertificateNumber"],
                    UniqueId = (string)row["UniqueId"],
                    RopeId = (int)row["RopeId"],
                    Xch = Convert.ToDecimal((row["Xch"] == DBNull.Value) ? 0 : row["Xch"]),
                    Ych = Convert.ToDecimal((row["Ych"] == DBNull.Value) ? 0 : row["Ych"]),
                    Zch = Convert.ToDecimal((row["Zch"] == DBNull.Value) ? 0 : row["Zch"]),
                    Xbl = Convert.ToDecimal((row["Xbl"] == DBNull.Value) ? 0 : row["Xbl"]),
                    Ybl = Convert.ToDecimal((row["Ybl"] == DBNull.Value) ? 0 : row["Ybl"]),
                    Zbl = Convert.ToDecimal((row["Zbl"] == DBNull.Value) ? 0 : row["Zbl"]),
                    l0 = Convert.ToDecimal((row["l0"] == DBNull.Value) ? 0 : row["l0"]),
                    E = Convert.ToDecimal((row["E"] == DBNull.Value) ? 0 : row["E"]),
                    n = Convert.ToDecimal((row["n"] == DBNull.Value) ? 0 : row["n"]),
                    a = Convert.ToDecimal((row["a"] == DBNull.Value) ? 0 : row["a"]),
                    MBSrope = Convert.ToDecimal((row["MBSrope"] == DBNull.Value) ? 0 : row["MBSrope"]),
                    Id = Convert.ToInt32((row["Id"] == DBNull.Value) ? 0 : row["Id"]),
                    //=========================================

                    Xch1 = Convert.ToDecimal((row["Xch"] == DBNull.Value) ? 0 : row["Xch"]),
                    Ych1 = Convert.ToDecimal((row["Ych"] == DBNull.Value) ? 0 : row["Ych"]),
                    Zch1 = Convert.ToDecimal((row["Zch"] == DBNull.Value) ? 0 : row["Zch"]),
                    Xbl1 = Convert.ToDecimal((row["Xbl"] == DBNull.Value) ? 0 : row["Xbl"]),
                    Ybl1 = Convert.ToDecimal((row["Ybl"] == DBNull.Value) ? 0 : row["Ybl"]),
                    Zbl1 = Convert.ToDecimal((row["Zbl"] == DBNull.Value) ? 0 : row["Zbl"]),
                    l01 = Convert.ToDecimal((row["l0"] == DBNull.Value) ? 0 : row["l0"]),
                    E1 = Convert.ToDecimal((row["E"] == DBNull.Value) ? 0 : row["E"]),
                    n1 = Convert.ToDecimal((row["n"] == DBNull.Value) ? 0 : row["n"]),
                    a1 = Convert.ToDecimal((row["a"] == DBNull.Value) ? 0 : row["a"]),
                    MBSrope1 = Convert.ToDecimal((row["MBSrope"] == DBNull.Value) ? 0 : row["MBSrope"]),


                });
            }
            RaisePropertyChanged("LoadMooring");
            //OnPropertyChanged(new PropertyChangedEventArgs("LoadMooring"));
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }



        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }
        private void DeleteMethod(MooringLines obj)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var abc = MooringLiness.GetList().ToList();
                    // MooringLines findrank = MooringLiness.GetListById(obj.RopeId);
                    MooringLines findrank = MooringLiness.GetList().ToList().Where(x => x.RopeId == obj.RopeId).FirstOrDefault();
                    if (findrank != null)
                    {


                        // Detete into User's Table

                        // MooringLiness.UpdateEntity(findrank);
                        // MooringLiness.Save();
                        sc.Entry(findrank).State = EntityState.Deleted;
                        sc.SaveChanges();


                        ////.....Refresh DataGrid........

                        //var list4 = MooringLiness.GetList().ToList();
                        //list4.ForEach(x =>
                        //{
                        //    x.Xch1 = x.Xch.ToString();
                        //    x.Ych1 = x.Ych.ToString();
                        //    x.Zch1 = x.Zch.ToString();
                        //    x.Xbl1 = x.Xbl.ToString();
                        //    x.Ybl1 = x.Ybl.ToString();
                        //    x.Zbl1 = x.Zbl.ToString();
                        //    x.l01 = x.l0.ToString();
                        //    x.E1 = x.E.ToString();
                        //    x.n1 = x.n.ToString();
                        //    x.a1 = x.a.ToString();
                        //    x.MBSrope1 = x.MBSrope.ToString();
                        //});

                        //loadMooring.Clear();
                        //sc.ObservableCollectionList(loadMooring, list4);
                        //RaisePropertyChanged("LoadMooring");

                        //double xch = 0; double ych = 0; double zch = 0; double xbl = 0; double ybl = 0; double zbl = 0; double l0 = 0; double e1 = 0; double a = 0; double n = 0; double mbscrope = 0;
                        string sql = "Delete from RopeBasedCalculations where RopeId=" + obj.RopeId + "";
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, sc.con);
                        DataTable dtkk = new DataTable();
                        adapter.Fill(dtkk);

                        GetMooringLines();

                        //.....End Refresh DataGrid........
                        MessageBox.Show("Record deleted successfully", "Delete Mooring", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {

                        MessageBox.Show("Record is not found ", "Delete Mooring", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static ObservableCollection<MooringLines> loadMooring = new ObservableCollection<MooringLines>();
        public ObservableCollection<MooringLines> LoadMooring
        {
            get
            {
                return loadMooring;
            }
            set
            {
                loadMooring = value;
                RaisePropertyChanged("LoadMooring");
            }
        }

    }
}
