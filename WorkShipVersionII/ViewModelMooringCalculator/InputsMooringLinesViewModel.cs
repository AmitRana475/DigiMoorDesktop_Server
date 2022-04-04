using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace WorkShipVersionII.ViewModelMooringCalculator
{
    public class InputsMooringLinesViewModel : ViewModelBase
    {


        private ShipmentContaxt sc;
        private IRepository<MooringLines> MooringLiness;
        public ICommand HelpCommand { get; private set; }
        public InputsMooringLinesViewModel()
        {
            sc = new ShipmentContaxt();
            MooringLiness = new Repository<MooringLines>();
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            // AddMooringCommand = new RelayCommand(() => ExecuteAddMooringCommand());
            deleteCommand = new RelayCommand<MooringLines>(DeleteMethod);

            LoadMooring.Clear();
            GetMooringLines();
           
        }

        private void DeleteMethod(MooringLines obj)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MooringLines findrank = MooringLiness.GetListById(obj.Id);
                    if (findrank != null)
                    {

                        // Detete into User's Table

                        // MooringLiness.UpdateEntity(findrank);
                        // MooringLiness.Save();
                        sc.Entry(findrank).State = EntityState.Deleted;
                        sc.SaveChanges();


                        //.....Refresh DataGrid........

                        //var list = MooringLiness.GetList().ToList();
                        //list.ForEach(x =>
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
                        //sc.ObservableCollectionList(loadMooring, list);
                        //RaisePropertyChanged("LoadMooring");

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

        

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }

        }


        private List<MooringLines> GetLoadMooring()
        {
            var list = MooringLiness.GetList().ToList();

            return list;
        }

        private ObservableCollection<MooringLines> loadMooring = new ObservableCollection<MooringLines>();
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

           // var ranklist = new ObservableCollection<MooringLines>();
            
            foreach (DataRow row in datatbl.Rows)
                {
                   LoadMooring.Add(new MooringLines()
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
                       Zbl= Convert.ToDecimal((row["Zbl"] == DBNull.Value) ? 0 : row["Zbl"]),
                       l0 = Convert.ToDecimal((row["l0"] == DBNull.Value) ? 0 : row["l0"]),
                       E = Convert.ToDecimal((row["E"] == DBNull.Value) ? 0 : row["E"]),
                       n = Convert.ToDecimal((row["n"] == DBNull.Value) ? 0 : row["n"]),
                       a = Convert.ToDecimal((row["a"] == DBNull.Value) ? 0 : row["a"]),
                       MBSrope = Convert.ToDecimal((row["MBSrope"] == DBNull.Value) ? 0 : row["MBSrope"]),
                          //WinchId = (int)row["WinchId"],
                       Id = Convert.ToInt32((row["Id"] == DBNull.Value) ? 0 : row["Id"]),

                          //===============================

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
            OnPropertyChanged(new PropertyChangedEventArgs("LoadMooring"));
        }              
        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }        
    }


}

