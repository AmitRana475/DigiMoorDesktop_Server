using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace WorkShipVersionII.ViewModelMooringCalculator
{
    public class AddMooringLineViewModel : ViewModelBase
    {

        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ICommand HelpCommand { get; private set; }

        public AddMooringLineViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            saveCommand = new RelayCommand<RopeBasedCalculationsClass>(SaveMooringLineMethod);
            cancelCommand = new RelayCommand(CancelMooringLineList);
            //SelectionChangedCommand = new RelayCommand(SelectionChanged);
            GetRopeType();


        }






        public AddMooringLineViewModel(RopeBasedCalculationsClass obj)
        {

            addMoorings = obj;
            RaisePropertyChanged("AddMoorings");
            saveCommand = new RelayCommand<RopeBasedCalculationsClass>(SaveMooringLineMethod);
            cancelCommand = new RelayCommand(CancelMooringLineList);

        }
        private void CancelMooringLineList()
        {
            GetMooringLines();
            ChildWindowManager.Instance.CloseChildWindow();
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }


        private RopeBasedCalculationsClass addMoorings = new RopeBasedCalculationsClass();
        public RopeBasedCalculationsClass AddMoorings
        {
            get
            {
                return addMoorings;
            }
            set
            {
                addMoorings = value;
                RaisePropertyChanged("AddMoorings");
            }
        }


        public void GetRopeType()
        {
            try
            {
                ropetype.Clear();
                SqlCommand cmd = new SqlCommand("select Id,CertificateNumber +' - '+ UniqueID as CertificateNumber from MooringRopeDetail where ropetail=0 and OutofServiceDate is null and DeleteStatus=0 order by Id", sc.con);
                //cmd.CommandType = CommandType.StoredProcedure;


                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    ropetype.Add(new MooringLines()
                    {
                        Id = (int)row["Id"],                       
                        Certi_No = (string)row["CertificateNumber"],
                        //UniqueId = (string)row["UniqueID"],
                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("ropetype"));

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }
        }
        public static ObservableCollection<MooringLines> ropetype = new ObservableCollection<MooringLines>();
        public ObservableCollection<MooringLines> RopeType
        {
            get
            {
                return ropetype;
            }
            set
            {
                ropetype = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
            }
        }

        public static MooringLines sropetype = new MooringLines();
        public MooringLines SRopeType
        {
            get
            {

                return sropetype;
            }
            set
            {

                sropetype = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SRopeType"));
                if (sropetype != null)
                {
                    var data = sc.AssignRopetoWinch.Where(x => x.RopeId == sropetype.Id && x.IsActive == true && x.IsDelete == false && x.RopeTail == 0).FirstOrDefault();
                 
                    addMoorings.RopeId = sropetype.Id;
                    addMoorings.CertificateNumber = sropetype.Certi_No;


                    if (data != null)
                    {
                        var data1 = sc.MooringWinch.Where(x => x.Id == data.WinchId &&  x.IsActive == true).FirstOrDefault();

                        if (data1 != null)
                        {
                            addMoorings.AssignedNumber = data1.AssignedNumber;
                            addMoorings.Location = data1.Location;
                        }
                        else
                        {
                            addMoorings.AssignedNumber = "Not Assigned";
                            addMoorings.Location = "Not Assigned";
                        }
                    }
                    else
                    {
                        addMoorings.AssignedNumber = "Not Assigned";
                        addMoorings.Location = "Not Assigned";
                    }
                    RaisePropertyChanged("AddMoorings");

                }
            }

        }

        public static MooringLines _sropeass;
        public MooringLines SRopeAss
        {
            get
            {
                if (_sropeass != null)
                {
                    var data = sc.MooringLiness.Where(x => x.Id == _sropeass.Id).FirstOrDefault();
                    if (data != null)
                    {
                        AddMoorings.RopeId = data.Id;
                    }
                    OnPropertyChanged(new PropertyChangedEventArgs("AddMoorings"));
                }
                return _sropeass;
            }
            set
            {
                _sropeass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SRopeAss"));
            }
        }


        private void SaveMooringLineMethod(RopeBasedCalculationsClass obj)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("Select * from RopeBasedCalculations where RopeId= " + obj.RopeId + "", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if(dt.Rows.Count <= 0)
                {
                   
                    var RopeBasedCalculationsClass = new RopeBasedCalculationsClass
                    {
                        RopeId = obj.RopeId,
                        CertificateNumber = obj.CertificateNumber,
                        AssignedNumber = obj.AssignedNumber,
                        Location = obj.Location

                    };
                    sc.RopeBasedcalculations.Add(RopeBasedCalculationsClass);
                    sc.SaveChanges();

                    double xch = 0; double ych = 0; double zch = 0; double xbl = 0; double ybl = 0; double zbl = 0; double l0 = 0; double e1 = 0; double a = 0; double n = 0; double mbscrope = 0;
                    //string sql = "Insert Into tblmooringlines (Xch,Ych,Zch,Xbl,Ybl,Zbl,l0,E,n,a,MBSrope,RopeId) Values(" + xch + "," + ych + "," + zch + "," + xbl + "," + ybl + "," + zbl + "," + l0 + "," + e1 + "," + n + "," + a + "," + mbscrope + "," + obj.RopeId + ")";
                    string sql = "inserttblmooringlines";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, sc.con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@Xch", xch);
                    adapter.SelectCommand.Parameters.AddWithValue("@Ych", ych);
                    adapter.SelectCommand.Parameters.AddWithValue("@Zch", zch);
                    adapter.SelectCommand.Parameters.AddWithValue("@Xbl", xbl);
                    adapter.SelectCommand.Parameters.AddWithValue("@Ybl", ybl);
                    adapter.SelectCommand.Parameters.AddWithValue("@Zbl", zbl);
                    adapter.SelectCommand.Parameters.AddWithValue("@l0", l0);
                    adapter.SelectCommand.Parameters.AddWithValue("@E", e1);
                    adapter.SelectCommand.Parameters.AddWithValue("@n", n);
                    adapter.SelectCommand.Parameters.AddWithValue("@a", a);
                    adapter.SelectCommand.Parameters.AddWithValue("@MBSrope", mbscrope);
                    adapter.SelectCommand.Parameters.AddWithValue("@RopeId", obj.RopeId);
                    DataTable dtkk = new DataTable();
                    adapter.Fill(dtkk);
                    CancelMooringLineList();
                    MessageBox.Show("Line Added to the list successfully");
                }
                else
                {
                    MessageBox.Show("Rope already exists in the list");
                    return ;
                    
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void GetMooringLines()
        {
           
            DateTime dd = DateTime.Now.Date;

            string qry = "MooringLineInputCalulator";
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
            System.Data.DataTable datatbl = new System.Data.DataTable();
            sda.Fill(datatbl);

            var ranklist = new ObservableCollection<MooringLines>();
           InputsVesselViewModel.loadMooring.Clear();
            foreach (DataRow row in datatbl.Rows)
            {
                InputsVesselViewModel.loadMooring.Add(new MooringLines()
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
            RaisePropertyChanged("LoadMooring");
            //OnPropertyChanged(new PropertyChangedEventArgs("LoadMooring"));
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
