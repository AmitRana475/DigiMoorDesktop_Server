using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsMooringCalulator;

namespace WorkShipVersionII.ViewModelMooringCalculator
{
    public class SaveCurrentValuesViewModel : ViewModelBase
    {

        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        private ViewModelBase _currentViewModel;
        public ICommand SaveDataLoadsCommand { get; private set; }
        public ICommand HelpCommand { get; private set; }

        //===========For Output===============
        private IRepository<VesselP> VesselPs;
        private IRepository<WindAreas> WindAreass;
        private IRepository<WindandCurrent> WindandCurrents;
        private IRepository<GeneralP> GeneralPs;
        private IRepository<WindLoads> WindLoadss;
        private IRepository<CurrentLoad> CurrentLoads;
        private IRepository<MooringLines> MooringLiness;


        public List<WindLoads> BasicParameters { get; set; }
        public List<WindForceCoefficients> LoadList { get; set; }
        public List<WindForceCoefficients> ForceList { get; set; }

        public List<CurrentLoad> BasicCurrentParameter { get; set; }
        public List<WindForceCoefficients> ForceCurrentList { get; set; }
        public List<WindForceCoefficients> loadCurrentList { get; set; }

        public List<WindForceCoefficients> FinalForceLoad { get; set; }

        public List<TempMooringLine> FinalMooringForce { get; set; }

        //===========For Output===============

        public SaveCurrentValuesViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            VesselPs = new Repository<VesselP>();
            WindAreass = new Repository<WindAreas>();
            WindandCurrents = new Repository<WindandCurrent>();
            GeneralPs = new Repository<GeneralP>();
            WindLoadss = new Repository<WindLoads>();
            CurrentLoads = new Repository<CurrentLoad>();
            MooringLiness = new Repository<MooringLines>();

            BasicParameters = GetBasicParametersForsave();
            ForceList = GetWindForceForSave();
            LoadList = GetWindLoadForsave();

            BasicCurrentParameter = GetBasicCurrentParameters();
            ForceCurrentList = GetCurrentForceForSave();
            loadCurrentList = GetCurrentWindLoad();

            FinalForceLoad = GetFinalForceLoad();
            FinalMooringForce = GetFinalMooringForSave();



            SaveDataLoadsCommand = new RelayCommand(() => ExecuteSaveDataLoadsLoads());
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

            saveCommand = new RelayCommand(SavePortList);
            cancelCommand = new RelayCommand(CancelPortList);

            GetFinalMooringLinesCal();
            GetPortName();
            AutoPortName = string.Empty;
            RaisePropertyChanged("AutoPortName");
        }

        private void ExecuteSaveDataLoadsLoads()
        {
            // StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            // CurrentViewModel = new SavePortWiseListingViewModel();
        }
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }


        private static string autoPartname;
        public string AutoPortName
        {
            get
            {
                if (StaticHelper.Autoportname == null)
                {
                    autoPartname = "";
                }

                if (autoPartname != null)
                {
                    //RaisePropertyChanged("FacilityName");
                    StaticHelper.Autoportname = "1";
                    //MOperationBirth.PortName = autoPartname;
                    AutoPortNames = GetPortName(autoPartname);
                    //GetFacilityName(autoPartname);
                }
                return autoPartname;
            }

            set
            {
                autoPartname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AutoPortName"));
            }
        }
        private string listVisible;
        public string ListVisible
        {
            get { return listVisible; }
            set
            {
                listVisible = value;
                RaisePropertyChanged("ListVisible");
            }
        }



        private static ObservableCollection<string> autoPortNames = new ObservableCollection<string>();
        public ObservableCollection<string> AutoPortNames
        {
            get
            {
                return autoPortNames;
            }
            set
            {
                autoPortNames = value;
                RaisePropertyChanged("AutoPortNames");

            }
        }




        private ObservableCollection<string> GetPortName(string autoPortName)
        {
            var PortNames = new ObservableCollection<string>();
            var data = sc.PortListtbl.Where(x => x.PortName.ToLower().Contains(autoPortName.Trim().ToLower())).Select(x => new { x.PortName }).ToList().Distinct();

            foreach (var item in data)
            {
                PortNames.Add(item.PortName);

            }
            var dat = sc.PortListtbl.Where(x => x.PortName.ToLower().Equals(autoPortName.Trim().ToLower())).Select(x => new { x.Id, x.PortName }).Distinct().FirstOrDefault();
            if (dat != null)
            {
                //MooringLines.PortName = dat.PortName;
                //MOperationBirth.Id = dat.Id;
            }
            else
            {
                // MOperationBirth.PortName = string.Empty;
                // MOperationBirth.OPId = 0;

            }

            if (PortNames.Count > 0)
            {

                ListVisible = "Visible";
            }
            else
            {
                ListVisible = "Collapsed";
                //MessageBox.Show("PortName not exist of " + autoPortName + " !", "Wrong Input", MessageBoxButton.OK, MessageBoxImage.Warning);

            }


            return PortNames;
        }

        public void GetPortName()
        {
            // ObservableCollection<RopeTypeCombo1> AddRopeType = new ObservableCollection<RopeTypeCombo1>();
            portname.Clear();
            PortNameCombo rop;
            SqlDataAdapter adp = new SqlDataAdapter("select distinct portname from PortList", sc.con);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                rop = new PortNameCombo();
                //rop.Id = (int)row["Id"];
                rop.PortName = (string)row["portname"];
                portname.Add(rop);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("portname"));
            //return AddRopeType;


        }

        public void resetform()
        {
            try
            {
                AddCurrentValues = new PortListClass();
                RaisePropertyChanged("AddCurrentValues");
                // SaveDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("SaveDate");
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private static Nullable<DateTime> _saveDate = null;
        public Nullable<DateTime> SaveDate
        {
            get
            {
                if (_saveDate == null)
                {
                    _saveDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                //SaveCurrentValues.SaveDate = (DateTime)_saveDate;
                return _saveDate;
            }
            set
            {
                _saveDate = value;
                RaisePropertyChanged("SaveDate");
            }
        }



        private string listingMessage;
        public string ListingMessage
        {
            get { return listingMessage; }
            set { listingMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("ListingMessage")); }
        }

        public static PortListClass _addCurrentValues = new PortListClass();
        public PortListClass AddCurrentValues
        {
            get
            {
                ListingMessage = string.Empty;
                RaisePropertyChanged("ListingMessage");
                return _addCurrentValues;
            }
            set
            {
                _addCurrentValues = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddCurrentValues"));
            }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }


        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }
        public class PortNameCombo
        {
            public int Id { get; set; }
            public string PortName { get; set; }

        }
        private void CancelPortList()
        {
            ChildWindowManager.Instance.CloseChildWindow();
        }
        private void SavePortList()
        {
            try
            {
                CancelPortList();
                var SPortName = AutoPortName;
                if (string.IsNullOrEmpty(SPortName) == false && SaveDate != null)
                {
                    var qry = "Select * from PortList where PortName ='" + SPortName + "' order by Id";
                    SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);


                    int id = Convert.ToInt32(dt.Rows[0]["Id"]);

                    //=========================INPUT RESULT=====================================
                    var qry1 = "Select * from tblGeneralP";
                    SqlDataAdapter adp1 = new SqlDataAdapter(qry1, sc.con);
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        if (sc.con.State == ConnectionState.Closed)
                            sc.con.Open();

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {

                            //SqlCommand cmd = new SqlCommand("Insert Into InputGeneralParticulars(PortId,PortName,Name,Description,MainValue,DefaultValue,Units,InputDate,OldInputId) Values(@PortId,@PortName,@Name,@Description,@MainValue,@DefaultValue,@Units,@InputDate,@OldInputId)", sc.con);
                            SqlCommand cmd = new SqlCommand("InsertInputGeneralParticulars", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@PortName", SPortName);
                            cmd.Parameters.AddWithValue("@Name", dt1.Rows[i]["Name"].ToString());
                            cmd.Parameters.AddWithValue("@Description", dt1.Rows[i]["Description"].ToString());
                            cmd.Parameters.AddWithValue("@MainValue", Convert.ToDecimal(dt1.Rows[i]["MainValue"]));
                            cmd.Parameters.AddWithValue("@DefaultValue", Convert.ToDecimal(dt1.Rows[i]["DefaultValue"]));
                            cmd.Parameters.AddWithValue("@Units", dt1.Rows[i]["Units"].ToString());
                            cmd.Parameters.AddWithValue("@InputDate", Convert.ToDateTime(SaveDate));
                            cmd.Parameters.AddWithValue("@OldInputId", Convert.ToInt32(dt1.Rows[i]["Id"]));
                            cmd.ExecuteNonQuery();

                        }
                        sc.con.Close();

                    }

                    //=========================================================================================
                    var qry2 = "Select * from tblVesselP";
                    SqlDataAdapter adp2 = new SqlDataAdapter(qry2, sc.con);
                    DataTable dt2 = new DataTable();
                    adp2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        if (sc.con.State == ConnectionState.Closed)
                            sc.con.Open();

                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {

                            //SqlCommand cmd = new SqlCommand("Insert Into InputPrincipalParticulars(PortId,PortName,Name,Description,MainValue,DefaultValue,Units,InputDate,OldInputId) Values(@PortId,@PortName,@Name,@Description,@MainValue,@DefaultValue,@Units,@InputDate,@OldInputId)", sc.con);
                            SqlCommand cmd = new SqlCommand("insertInputPrincipalParticulars", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@PortName", SPortName);
                            cmd.Parameters.AddWithValue("@Name", dt2.Rows[i]["Name"].ToString());
                            cmd.Parameters.AddWithValue("@Description", dt2.Rows[i]["Description"].ToString());
                            cmd.Parameters.AddWithValue("@MainValue", Convert.ToDecimal(dt2.Rows[i]["MainValue"]));
                            string myvalues = dt2.Rows[i]["DefaultValue"].ToString();
                            if (string.IsNullOrEmpty(myvalues))
                            {
                                cmd.Parameters.AddWithValue("@DefaultValue", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DefaultValue", Convert.ToDecimal(dt2.Rows[i]["DefaultValue"]));
                            }
                            cmd.Parameters.AddWithValue("@Units", dt2.Rows[i]["Units"].ToString());
                            cmd.Parameters.AddWithValue("@InputDate", Convert.ToDateTime(SaveDate));
                            cmd.Parameters.AddWithValue("@OldInputId", Convert.ToInt32(dt2.Rows[i]["Id"]));
                            cmd.ExecuteNonQuery();

                        }
                        sc.con.Close();
                    }
                    //===============================================================================

                    var qry3 = "Select * from tblWindAreas";
                    SqlDataAdapter adp3 = new SqlDataAdapter(qry3, sc.con);
                    DataTable dt3 = new DataTable();
                    adp3.Fill(dt3);
                    if (dt3.Rows.Count > 0)
                    {
                        if (sc.con.State == ConnectionState.Closed)
                            sc.con.Open();

                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {

                            //SqlCommand cmd = new SqlCommand("Insert Into InputWindArea(PortId,PortName,Name,Description,MainValue,DefaultValue,Units,InputDate,OldInputId) Values(@PortId,@PortName,@Name,@Description,@MainValue,@DefaultValue,@Units,@InputDate,@OldInputId)", sc.con);
                            SqlCommand cmd = new SqlCommand("insertInputWindArea", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@PortName", SPortName);
                            cmd.Parameters.AddWithValue("@Name", dt3.Rows[i]["Name"].ToString());
                            cmd.Parameters.AddWithValue("@Description", dt3.Rows[i]["Description"].ToString());
                            cmd.Parameters.AddWithValue("@MainValue", Convert.ToDecimal(dt3.Rows[i]["MainValue"]));
                            string myvalues = dt3.Rows[i]["DefaultValue"].ToString();
                            if (string.IsNullOrEmpty(myvalues))
                            {
                                cmd.Parameters.AddWithValue("@DefaultValue", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DefaultValue", Convert.ToDecimal(dt3.Rows[i]["DefaultValue"]));
                            }
                            cmd.Parameters.AddWithValue("@Units", dt3.Rows[i]["Units"].ToString());
                            cmd.Parameters.AddWithValue("@InputDate", Convert.ToDateTime(SaveDate));
                            cmd.Parameters.AddWithValue("@OldInputId", Convert.ToInt32(dt3.Rows[i]["Id"]));
                            cmd.ExecuteNonQuery();

                        }
                        sc.con.Close();
                    }

                    //===========================================================================
                    var qry4 = "Select * from tblWindandCurrent";
                    SqlDataAdapter adp4 = new SqlDataAdapter(qry4, sc.con);
                    DataTable dt4 = new DataTable();
                    adp4.Fill(dt4);
                    if (dt4.Rows.Count > 0)
                    {
                        if (sc.con.State == ConnectionState.Closed)
                            sc.con.Open();

                        for (int i = 0; i < dt4.Rows.Count; i++)
                        {

                            //SqlCommand cmd = new SqlCommand("Insert Into InputWindAndCurrentParameters(PortId,PortName,Name,Description,MainValue,DefaultValue,Units,InputDate,OldInputId) Values(@PortId,@PortName,@Name,@Description,@MainValue,@DefaultValue,@Units,@InputDate,@OldInputId)", sc.con);
                            SqlCommand cmd = new SqlCommand("insertInputWindAndCurrentParameters", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@PortName", SPortName);
                            cmd.Parameters.AddWithValue("@Name", dt4.Rows[i]["Name"].ToString());
                            cmd.Parameters.AddWithValue("@Description", dt4.Rows[i]["Description"].ToString());
                            cmd.Parameters.AddWithValue("@MainValue", Convert.ToDecimal(dt4.Rows[i]["MainValue"]));
                            string myvalues = dt4.Rows[i]["DefaultValue"].ToString();
                            if (string.IsNullOrEmpty(myvalues))
                            {
                                cmd.Parameters.AddWithValue("@DefaultValue", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DefaultValue", Convert.ToDecimal(dt4.Rows[i]["DefaultValue"]));
                            }
                            cmd.Parameters.AddWithValue("@Units", dt4.Rows[i]["Units"].ToString());
                            cmd.Parameters.AddWithValue("@InputDate", Convert.ToDateTime(SaveDate));
                            cmd.Parameters.AddWithValue("@OldInputId", Convert.ToInt32(dt4.Rows[i]["Id"]));
                            cmd.ExecuteNonQuery();

                        }
                        sc.con.Close();
                    }
                    //==========================================================================
                    var qry5 = "Select * from tblMooringLines";
                    SqlDataAdapter adp5 = new SqlDataAdapter(qry5, sc.con);
                    DataTable dt5 = new DataTable();
                    adp5.Fill(dt5);
                    if (dt5.Rows.Count > 0)
                    {
                        if (sc.con.State == ConnectionState.Closed)
                            sc.con.Open();

                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {

                            //SqlCommand cmd = new SqlCommand("Insert Into InputMooringCalculation(PortId,PortName,Xch,Ych,Zch,Xbl,Ybl,Zbl,l0,E,n,a,MBSrope,RopeId,InputDate,OldInputId) Values(@PortId,@PortName,@Xch,@Ych,@Zch,@Xbl,@Ybl,@Zbl,@l0,@E,@n,@a,@MBSrope,@RopeId,@InputDate,@OldInputId)", sc.con);
                            SqlCommand cmd = new SqlCommand("InsertInputMooringCalculation", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                            cmd.Parameters.AddWithValue("@PortName", SPortName);
                            cmd.Parameters.AddWithValue("@Xch", Convert.ToDecimal(dt5.Rows[i]["Xch"]));
                            cmd.Parameters.AddWithValue("@Ych", Convert.ToDecimal(dt5.Rows[i]["Ych"]));
                            cmd.Parameters.AddWithValue("@Zch", Convert.ToDecimal(dt5.Rows[i]["Zch"]));
                            cmd.Parameters.AddWithValue("@Xbl", Convert.ToDecimal(dt5.Rows[i]["Xbl"]));
                            cmd.Parameters.AddWithValue("@Ybl", Convert.ToDecimal(dt5.Rows[i]["Ybl"]));
                            cmd.Parameters.AddWithValue("@Zbl", Convert.ToDecimal(dt5.Rows[i]["Zbl"]));
                            cmd.Parameters.AddWithValue("@l0", Convert.ToDecimal(dt5.Rows[i]["l0"]));
                            cmd.Parameters.AddWithValue("@E", Convert.ToDecimal(dt5.Rows[i]["E"]));
                            cmd.Parameters.AddWithValue("@n", Convert.ToDecimal(dt5.Rows[i]["n"]));
                            cmd.Parameters.AddWithValue("@a", Convert.ToDecimal(dt5.Rows[i]["a"]));
                            cmd.Parameters.AddWithValue("@MBSrope", Convert.ToDecimal(dt5.Rows[i]["MBSrope"]));
                            cmd.Parameters.AddWithValue("@RopeId", Convert.ToInt32(dt5.Rows[i]["RopeId"]));
                            cmd.Parameters.AddWithValue("@InputDate", Convert.ToDateTime(SaveDate));
                            cmd.Parameters.AddWithValue("@OldInputId", Convert.ToInt32(dt5.Rows[i]["Id"]));
                            cmd.ExecuteNonQuery();

                        }
                        sc.con.Close();


                    }


                    //==================================OUTPUT RESULTS====================================//

                    if (sc.con.State == ConnectionState.Closed)
                        sc.con.Open();

                    foreach (var item in BasicParameters)
                    {


                        //string qry7 = "Insert Into OutputBasicWindPartameters(PortId,PortName,Name,Notation,MainValue,Units,OutputDate) Values(@PortId,@PortName,@Name,@Notation,@MainValue,@Units,@OutputDate)";
                        string qry7 = "InsertOutputBasicWindPartameters";
                        SqlCommand cmd = new SqlCommand(qry7, sc.con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@PortName", SPortName);
                        cmd.Parameters.AddWithValue("@Name", item.Name.ToString());
                        string notation = item.Notation;
                        if (string.IsNullOrEmpty(notation))
                        {
                            cmd.Parameters.AddWithValue("@Notation", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Notation", item.Notation);
                        }

                        cmd.Parameters.AddWithValue("@MainValue", item.MainValue);
                        string units = item.Notation;
                        if (string.IsNullOrEmpty(units))
                        {
                            cmd.Parameters.AddWithValue("@Units", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Units", item.Units);
                        }

                        cmd.Parameters.AddWithValue("@OutputDate", SaveDate);
                        cmd.ExecuteNonQuery();

                    }
                    sc.con.Close();

                    //==========================================================================
                    if (sc.con.State == ConnectionState.Closed)
                        sc.con.Open();
                    foreach (var item in ForceList)
                    {


                        //string qry8 = "Insert Into OutputWindForceCoefficients(PortId,PortName,Name,Description,MainValues,Units,OutputDate) Values(@PortId,@PortName,@Name,@Description,@MainValues,@Units,@OutputDate)";
                        string qry8 = "InsertOutputWindForceCoefficients";
                        SqlCommand cmd = new SqlCommand(qry8, sc.con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@PortName", SPortName);
                        cmd.Parameters.AddWithValue("@Name", item.Name.ToString());
                        cmd.Parameters.AddWithValue("@Description", item.Description);
                        string myvalues = item.Values.ToString();
                        if (string.IsNullOrEmpty(myvalues))
                        {
                            cmd.Parameters.AddWithValue("@MainValues", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MainValues", item.Values);
                        }

                        cmd.Parameters.AddWithValue("@Units", item.Units);
                        cmd.Parameters.AddWithValue("@OutputDate", SaveDate);

                        cmd.ExecuteNonQuery();

                    }
                    sc.con.Close();

                    //============================================================================
                    if (sc.con.State == ConnectionState.Closed)
                        sc.con.Open();
                    foreach (var items in LoadList)
                    {


                        //string qry9 = "Insert Into OutputWindLoads(PortId,PortName,Name,Description,MainValues,Units,OutputDate) Values(@PortId,@PortName,@Name,@Description,@MainValues,@Units,@OutputDate)";
                        string qry9 = "InsertOutputWindLoads";
                       
                        SqlCommand cmd = new SqlCommand(qry9, sc.con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@PortName", SPortName);
                        cmd.Parameters.AddWithValue("@Name", items.Name);
                        cmd.Parameters.AddWithValue("@Description", items.Description);
                        cmd.Parameters.AddWithValue("@MainValues", items.Values);
                        cmd.Parameters.AddWithValue("@Units", items.Units);
                        cmd.Parameters.AddWithValue("@OutputDate", SaveDate);
                        cmd.ExecuteNonQuery();

                    }
                    sc.con.Close();

                    //========================================================================
                    if (sc.con.State == ConnectionState.Closed)
                        sc.con.Open();
                    foreach (var item in BasicCurrentParameter)
                    {


                        //string qry10 = "Insert Into OutputBasicCurrentParameters(PortId,PortName,Name,Notation,MainValues,Units,OutputDate) Values(@PortId,@PortName,@Name,@Notation,@MainValues,@Units,@OutputDate)";
                        string qry10 = "InsertOutputBasicCurrentParameters";

                        SqlCommand cmd = new SqlCommand(qry10, sc.con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@PortName", SPortName);
                        cmd.Parameters.AddWithValue("@Name", item.Name.ToString());
                        cmd.Parameters.AddWithValue("@Notation", item.Notation);
                        cmd.Parameters.AddWithValue("@MainValues", item.MainValue);
                        string units = item.Units;
                        if (string.IsNullOrEmpty(units))
                        {
                            cmd.Parameters.AddWithValue("@Units", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Units", item.Units);
                        }
                        cmd.Parameters.AddWithValue("@OutputDate", SaveDate);
                        cmd.ExecuteNonQuery();

                    }
                    sc.con.Close();
                    //===================================================================
                    if (sc.con.State == ConnectionState.Closed)
                        sc.con.Open();
                    foreach (var item in ForceCurrentList)
                    {
                        //string qry11 = "Insert Into OutputCuurentForceCoefficients(PortId,PortName,Name,Description,MainValues,Units,OutputDate) Values(@PortId,@PortName,@Name,@Description,@MainValues,@Units,@OutputDate)";

                        string qry11 = "InsertOutputCuurentForceCoefficients";

                        SqlCommand cmd = new SqlCommand(qry11, sc.con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@PortName", SPortName);
                        cmd.Parameters.AddWithValue("@Name", item.Name.ToString());
                        cmd.Parameters.AddWithValue("@Description", item.Description);
                        cmd.Parameters.AddWithValue("@MainValues", item.Values);
                        cmd.Parameters.AddWithValue("@Units", item.Units);
                        cmd.Parameters.AddWithValue("@OutputDate", SaveDate);
                        cmd.ExecuteNonQuery();

                    }
                    sc.con.Close();

                    //==============================================================
                    if (sc.con.State == ConnectionState.Closed)
                        sc.con.Open();
                    foreach (var items in loadCurrentList)
                    {


                        // string qry12 = "Insert Into OutputCurrentWindLoads(PortId,PortName,Name,Description,MainValues,Units,OutputDate) Values(@PortId,@PortName,@Name,@Description,@MainValues,@Units,@OutputDate)";
                        string qry12 = "InsertOutputCurrentWindLoads";

                        SqlCommand cmd = new SqlCommand(qry12, sc.con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@PortName", SPortName);
                        cmd.Parameters.AddWithValue("@Name", items.Name);
                        cmd.Parameters.AddWithValue("@Description", items.Description);
                        string myvalues = items.Values.ToString();
                        if (string.IsNullOrEmpty(myvalues))
                        {
                            cmd.Parameters.AddWithValue("@MainValues", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MainValues", items.Values);
                        }

                        cmd.Parameters.AddWithValue("@Units", items.Units);
                        cmd.Parameters.AddWithValue("@OutputDate", SaveDate);
                        cmd.ExecuteNonQuery();

                    }
                    sc.con.Close();

                    //=================================================================
                    if (sc.con.State == ConnectionState.Closed)
                        sc.con.Open();
                    foreach (var item in FinalForceLoad)
                    {
                        //string qry13 = "Insert Into OutputFinalForcesAndMoments(PortId,PortName,Name,Description,MainValues,Units,OutputDate) Values(@PortId,@PortName,@Name,@Description,@MainValues,@Units,@OutputDate)";
                        string qry12 = "InsertOutputFinalForcesAndMoments";

                        SqlCommand cmd = new SqlCommand(qry12, sc.con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@PortName", SPortName);
                        cmd.Parameters.AddWithValue("@Name", item.Name.ToString());
                        cmd.Parameters.AddWithValue("@Description", item.Description);
                        cmd.Parameters.AddWithValue("@MainValues", item.Values);
                        cmd.Parameters.AddWithValue("@Units", item.Units);
                        cmd.Parameters.AddWithValue("@OutputDate", SaveDate);
                        cmd.ExecuteNonQuery();

                    }
                    sc.con.Close();

                    //=================================================================
                    if (sc.con.State == ConnectionState.Closed)
                        sc.con.Open();
                    foreach (var item in FinalMooringForce)
                    {
                        // string qry13 = "Insert Into OutputMorringForcesCalculation(RopeId,PortId,PortName,Xch,Ych,Zch,Xbl,Ybl,Zbl,l1,ΔZi,a,n,ai,MBSrope,MBS,Ei,cosθi,φi,cosφi,l0,Li,ki,kyi,kyi_Xch,kyi_Xch2,Fyi,Ti,FSi,OutputDate) Values(@RopeId,@PortId,@PortName,@Xch,@Ych,@Zch,@Xbl,@Ybl,@Zbl,@l1,@ΔZi,@a,@n,@ai,@MBSrope,@MBS,@Ei,@cosθi,@φi,@cosφi,@l0,@Li,@ki,@kyi,@kyi_Xch,@kyi_Xch2,@Fyi,@Ti,@FSi,@OutputDate)";
                        string qry12 = "InsertOutputMorringForcesCalculation";

                        SqlCommand cmd = new SqlCommand(qry12, sc.con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RopeId", item.RopeId);
                        cmd.Parameters.AddWithValue("@PortId", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@PortName", SPortName);
                        cmd.Parameters.AddWithValue("@Xch", item.Xch);
                        cmd.Parameters.AddWithValue("@Ych", item.Ych);
                        cmd.Parameters.AddWithValue("@Zch", item.Zch);
                        cmd.Parameters.AddWithValue("@Xbl", item.Xbl);
                        cmd.Parameters.AddWithValue("@Ybl", item.Ybl);
                        cmd.Parameters.AddWithValue("@Zbl", item.Zbl);
                        cmd.Parameters.AddWithValue("@l1", item.l1);
                        cmd.Parameters.AddWithValue("@ΔZi", item.ΔZi);
                        cmd.Parameters.AddWithValue("@a", item.a);
                        cmd.Parameters.AddWithValue("@n", item.n);
                        cmd.Parameters.AddWithValue("@ai", item.ai);
                        cmd.Parameters.AddWithValue("@MBSrope", item.MBSrope);
                        cmd.Parameters.AddWithValue("@MBS", item.MBS);
                        cmd.Parameters.AddWithValue("@Ei", item.Ei);
                        cmd.Parameters.AddWithValue("@cosθi", item.cosθi);
                        cmd.Parameters.AddWithValue("@φi", item.φi);
                        cmd.Parameters.AddWithValue("@cosφi", item.cosφi);
                        cmd.Parameters.AddWithValue("@l0", item.l0);
                        cmd.Parameters.AddWithValue("@Li", item.Li);
                        cmd.Parameters.AddWithValue("@ki", item.ki);
                        cmd.Parameters.AddWithValue("@kyi", item.kyi);
                        cmd.Parameters.AddWithValue("@kyi_Xch", item.kyi_Xch);
                        cmd.Parameters.AddWithValue("@kyi_Xch2", item.kyi_Xch2);
                        cmd.Parameters.AddWithValue("@Fyi", item.Fyi);
                        cmd.Parameters.AddWithValue("@Ti", item.Ti);
                        cmd.Parameters.AddWithValue("@FSi", item.FSi);
                        cmd.Parameters.AddWithValue("@OutputDate", SaveDate);
                        cmd.ExecuteNonQuery();

                    }
                    sc.con.Close();

                    GetLoadMooringCalculation2();
                }
                else
                {
                    MessageBox.Show("Please Select Port Name And Date", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void GetLoadMooringCalculation2()
        {

            try
            {
                SavePortwiseListingViewModel.loadUserAccess.Clear();
                string qry = "SELECT DISTINCT PortId,PortName,InputDate FROM InputMooringCalculation";
                SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    SavePortwiseListingViewModel.loadUserAccess.Add(new PortWiseListingClass()
                    {
                        //Id=(int)row["Id"],
                        PortId = (int)row["PortId"],
                        PortName = (string)row["PortName"],
                        InputDate = (DateTime)row["InputDate"],


                    });

                    OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        //===================Output Basic WindLoad==========================//

        private List<WindLoads> GetBasicParametersForsave()
        {
            var vessels = VesselPs.GetList().ToList();
            var windarea = WindAreass.GetList().ToList();
            var envirement = WindandCurrents.GetList().ToList();
            var generalp = GeneralPs.GetList().ToList();

            var list = WindLoadss.GetList().ToList();

            list.ForEach(x =>
            {
                if (x.Notation == "LWL") { x.MainValue = vessels.Where(p => p.Description == "LWL").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "Ae") { x.MainValue = windarea.Where(p => p.Description == "Ae").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "As") { x.MainValue = windarea.Where(p => p.Description == "As").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "Vw") { x.MainValue = envirement.Where(p => p.Description == "Vw").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "Pair") { x.MainValue = generalp.Where(p => p.Description == "Pair").Select(s => Convert.ToDecimal(s.MainValue)).FirstOrDefault(); }
                if (x.Notation == "Qw") { x.MainValue = envirement.Where(p => p.Description == "Qw").Select(s => s.MainValue).FirstOrDefault(); }
                if (string.IsNullOrEmpty(x.Notation)) { x.MainValue = windarea.Where(p => string.IsNullOrEmpty(p.Description)).Select(s => s.MainValue).FirstOrDefault(); }

            });



            return list;
        }

        public List<WindForceCoefficients> GetWindForceForSave()
        {
            List<WindForceCoefficients> list = new List<WindForceCoefficients>()
            {
                new WindForceCoefficients{  Name="Longitudinal Wind Force Coefficient",Description="CXW",Units="See Table 1"},
                new WindForceCoefficients{  Name="Lateral Wind Force Coefficient",Description="CYW",Units="See Table 2"},
                new WindForceCoefficients{  Name="Wind Yaw Moment Coefficient",Description="CXYW",Units="See Table 3"}
            };



            double D20 = Convert.ToDouble(BasicParameters.Where(x => x.Notation == "Qw").Select(s => s.MainValue).FirstOrDefault());
            double D21 = Convert.ToDouble(BasicParameters.Where(x => string.IsNullOrEmpty(x.Notation)).Select(s => s.MainValue).FirstOrDefault());

            var CXW = (D20 == 90 ? 0 : (D21 == 1 ? Math.Round(0.000000679 * (D20 * D20 * D20) - 0.0001833333 * (D20 * D20) + 0.0010079365 * D20 + 0.8992857143, 3) : Math.Round(-0.000000000027435 * (D20 * D20 * D20 * D20 * D20) + 0.000000012345679 * (D20 * D20 * D20 * D20) - 0.000001604938272 * (D20 * D20 * D20) + 0.000033333333332 * (D20 * D20) - 0.0022 * D20 + 0.45, 3)));


            var CYW = (D20 == 90 ? 1 : ((D20 == 0 || D20 == 180) ? 0 : Math.Round(0.0000000048 * (D20 * D20 * D20 * D20) - 0.0000017172 * (D20 * D20 * D20) + 0.000068771 * (D20 * D20) + 0.0154393939 * D20 - 0.0003679654, 3)));

            var CXYW = ((D20 == 0 || D20 == 180) ? 0 : (D20 == 90 ? -0.037 : Math.Round(-1.3115325E-13 * (D20 * D20 * D20 * D20 * D20 * D20) + 4.153539343E-11 * (D20 * D20 * D20 * D20 * D20) + 3.040873708E-11 * (D20 * D20 * D20 * D20) - 7.6143340645882E-07 * (D20 * D20 * D20) + 0.0000211970749723711 * (D20 * D20) + 0.0018519671365894 * D20 + 0.00092355496042354, 3)));


            list.ForEach(x =>
            {
                if (x.Description == "CXW") { x.Values = Convert.ToDecimal(CXW); }
                if (x.Description == "CYW") { x.Values = Convert.ToDecimal(CYW); }
                if (x.Description == "CXYW") { x.Values = Convert.ToDecimal(CXYW); }


            });

            return list;
        }

        public List<WindForceCoefficients> GetWindLoadForsave()
        {
            List<WindForceCoefficients> list = new List<WindForceCoefficients>()
            {
                new WindForceCoefficients{  Name="Longitudinal Wind Force",Description="FXW=1/2*Cxw*ρair*Vw2*AE",Description1 = "CXW",Units="MT"},
                new WindForceCoefficients{  Name="Lateral Wind Force",Description="FYW=1/2*Cyw*ρair*Vw2*AS",Description1 = "CYW",Units="MT"},
                new WindForceCoefficients{  Name="Total Factored Windage Area of Hull in Lateral Direction",Description="MW=1/2*Cxyw*ρair*Vw2*AS*LWL",Description1 = "CXYW",Units="MT-m"}
            };

            double D15 = Convert.ToDouble(BasicParameters.Where(x => x.Notation == "LWL").Select(s => s.MainValue).FirstOrDefault());
            double D16 = Convert.ToDouble(BasicParameters.Where(x => x.Notation == "Ae").Select(s => s.MainValue).FirstOrDefault());
            double D17 = Convert.ToDouble(BasicParameters.Where(x => x.Notation == "As").Select(s => s.MainValue).FirstOrDefault());
            double D18 = Convert.ToDouble(BasicParameters.Where(x => x.Notation == "Vw").Select(s => s.MainValue).FirstOrDefault());
            double D19 = Convert.ToDouble(BasicParameters.Where(x => x.Notation == "Pair").Select(s => s.MainValue).FirstOrDefault());

            double D23 = Convert.ToDouble(ForceList.Where(x => x.Description == "CXW").Select(s => s.Values).FirstOrDefault());
            double D24 = Convert.ToDouble(ForceList.Where(x => x.Description == "CYW").Select(s => s.Values).FirstOrDefault());
            double D25 = Convert.ToDouble(ForceList.Where(x => x.Description == "CXYW").Select(s => s.Values).FirstOrDefault());

            var CXW = ((D19 * D23 * (D18 * D18) * D16) * 1 / 2) / 9810;
            var CYW = ((D19 * D24 * (D18 * D18) * D17) * 1 / 2) / 9810;
            var CXYW = ((D19 * D25 * (D18 * D18) * D17 * D15) * 1 / 2) / 9810;
            CXW = double.IsInfinity(CXW) == true ? 0 : CXW;
            CYW = double.IsInfinity(CYW) == true ? 0 : CYW;
            CXYW = double.IsInfinity(CXYW) == true ? 0 : CXYW;

            list.ForEach(x =>
            {
                if (x.Description1 == "CXW") { x.Values = Convert.ToDecimal(CXW); }
                if (x.Description1 == "CYW") { x.Values = Convert.ToDecimal(CYW); }
                if (x.Description1 == "CXYW") { x.Values = Convert.ToDecimal(CXYW); }


            });


            return list;
        }

        //===================OutPut Current Loads===========================//

        private List<CurrentLoad> GetBasicCurrentParameters()
        {

            var vessels = VesselPs.GetList().ToList();
            var envirement = WindandCurrents.GetList().ToList();
            var generalp = GeneralPs.GetList().ToList();

            var list = CurrentLoads.GetList().ToList();

            list.ForEach(x =>
            {
                if (x.Notation == "LWL") { x.MainValue = vessels.Where(p => p.Description == "LWL").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "B") { x.MainValue = vessels.Where(p => p.Description == "B").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "T") { x.MainValue = vessels.Where(p => p.Description == "T").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "WD") { x.MainValue = envirement.Where(p => p.Description == "WD").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "S") { x.MainValue = vessels.Where(p => p.Description == "S").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "Vc") { x.MainValue = envirement.Where(p => p.Description == "Vc").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "Pwater") { x.MainValue = generalp.Where(p => p.Description == "Pwater").Select(s => Convert.ToDecimal(s.MainValue)).FirstOrDefault(); }
                if (x.Notation == "Qc") { x.MainValue = envirement.Where(p => p.Description == "Qc").Select(s => s.MainValue).FirstOrDefault(); }

                if (x.Notation == "WD/T") { x.MainValue = envirement.Where(p => p.Description == "WD").Select(s => s.MainValue).FirstOrDefault() / vessels.Where(p => p.Description == "T").Select(s => s.MainValue).FirstOrDefault(); }

            });



            return list;
        }

        private List<WindForceCoefficients> GetCurrentForceForSave()
        {
            List<WindForceCoefficients> list = new List<WindForceCoefficients>()
            {
                new WindForceCoefficients{  Name="Longitudinal Current Skin Friction Coefficient",Description="CXCA",Units="See Table 1"},
                new WindForceCoefficients{  Name="Lateral Current Force Coefficient",Description="CYC",Units="See Table 2"},
                new WindForceCoefficients{  Name="Longitudinal Current Drag Coefficient",Description="CXCB",Units="CYC * cos2θC"},
                new WindForceCoefficients{  Name="Current Yaw Moment Coefficient",Description="CXYC",Units="See Table 3"}
            };

            double D24 = Convert.ToDouble(BasicCurrentParameter.Where(x => x.Notation == "Qc").Select(s => s.MainValue).FirstOrDefault());
            decimal D25 = Convert.ToDecimal(BasicCurrentParameter.Where(x => x.Notation == "WD/T").Select(s => s.MainValue).FirstOrDefault());
            decimal D28 = 1.19m;

            object CXCB = null;

            if (D25 > Convert.ToDecimal(1.05))
                CXCB = Convert.ToDecimal(D28) * Convert.ToDecimal(Math.Cos(D24 * (Math.PI / 180.0)) * Math.Cos(D24 * (Math.PI / 180.0)));
            else
                CXCB = "1.05"; //"Low Water Depth";

            list.ForEach(x =>
            {
                if (x.Description == "CXCA") { x.Values = Convert.ToDecimal(1.21430643318376E-17); }
                if (x.Description == "CYC") { x.Values = Convert.ToDecimal(1.19967479226758); }
                //if (x.Description == "CXCB") { x.Values = Convert.ToDecimal((D25 < 1.05 ? "Low Water Depth" : D28 * COS(RADIANS(D24)) * COS(RADIANS(D24)))); }
                if (x.Description == "CXCB") { x.Values = CXCB; }
                if (x.Description == "CXYC") { x.Values = Convert.ToDecimal(-0.0299922569589333); } else { }


            });


            return list;
        }

        private List<WindForceCoefficients> GetCurrentWindLoad()
        {
            List<WindForceCoefficients> list = new List<WindForceCoefficients>()
            {
                new WindForceCoefficients{  Name="Longitudinal Current Force",Description="FXC=1/2*ρwater*VC2*B*(CXCA*S/LWL+CXCB*T)",Description1="CXW",Units="MT"},
                new WindForceCoefficients{  Name="Lateral Current Force",Description="FYC=1/2*CYC*ρwater*VC2*LWL*T)",Description1="CYW",Units="MT"},
                new WindForceCoefficients{  Name="Current Yaw Moment",Description="FXYC=1/2*CXYC*ρwater*VC2*LWL2*T)",Description1="CXYW",Units="MT-m"}
            };

            try
            {

                double D17 = Convert.ToDouble(BasicCurrentParameter.Where(x => x.Notation == "LWL").Select(s => s.MainValue).FirstOrDefault());
                double D18 = Convert.ToDouble(BasicCurrentParameter.Where(x => x.Notation == "B").Select(s => s.MainValue).FirstOrDefault());
                double D19 = Convert.ToDouble(BasicCurrentParameter.Where(x => x.Notation == "T").Select(s => s.MainValue).FirstOrDefault());
                double D21 = Convert.ToDouble(BasicCurrentParameter.Where(x => x.Notation == "S").Select(s => s.MainValue).FirstOrDefault());
                double D22 = Convert.ToDouble(BasicCurrentParameter.Where(x => x.Notation == "Vc").Select(s => s.MainValue).FirstOrDefault());
                double D23 = Convert.ToDouble(BasicCurrentParameter.Where(x => x.Notation == "Pwater").Select(s => s.MainValue).FirstOrDefault());

                double D27 = Convert.ToDouble(ForceCurrentList.Where(x => x.Description == "CXCA").Select(s => s.Values).FirstOrDefault());
                double D28 = Convert.ToDouble(ForceCurrentList.Where(x => x.Description == "CYC").Select(s => s.Values).FirstOrDefault());
                double D29 = Convert.ToDouble(ForceCurrentList.Where(x => x.Description == "CXCB").Select(s => s.Values).FirstOrDefault());
                double D30 = Convert.ToDouble(ForceCurrentList.Where(x => x.Description == "CXYC").Select(s => s.Values).FirstOrDefault());

                var CXW = ((D23 * (D22 * D22) * D18 * (D27 * D21 / D17 + D29 * D19)) * 1 / 2) / 9810;
                var CYW = ((D23 * (D22 * D22) * D28 * D17 * D19) * 1 / 2) / 9810;
                var CXYW = ((D23 * D30 * (D22 * D22) * (D17 * D17) * D19) * 1 / 2) / 9810;


                list.ForEach(x =>
                {
                    if (x.Description1 == "CXW") { x.Values = Convert.ToDecimal(CXW); }
                    if (x.Description1 == "CYW") { x.Values = Convert.ToDecimal(CYW); }
                    if (x.Description1 == "CXYW") { x.Values = Convert.ToDecimal(CXYW); }


                });


            }
            catch { }



            return list;
        }

        //====================Final Force Load=======================
        private List<WindForceCoefficients> GetFinalForceLoad()
        {
            List<WindForceCoefficients> list = new List<WindForceCoefficients>()
            {
                new WindForceCoefficients{  Name="Longitudinal Current Force",Description="FXC=1/2*ρwater*VC2*B*(CXCA*S/LWL+CXCB*T)",Description1="CXW",Units="MT"},
                new WindForceCoefficients{  Name="Lateral Current Force",Description="FYC=1/2*CYC*ρwater*VC2*LWL*T)", Description1="CYW",Units="MT"},
                new WindForceCoefficients{  Name="Current Yaw Moment",Description="FXYC=1/2*CXYC*ρwater*VC2*LWL2*T)",Description1="CXYW",Units="MT-m"}
            };

            new OutputsWindLoadsViewModel();
            new OutputsCurrentLoadsViewModel();
            new InputsVesselViewModel();

            var CXW = Convert.ToDecimal(OutputsWindLoadsViewModel.loadWindLoad.Where(x => x.Description1 == "CXW").Select(s => s.Values).FirstOrDefault()) + Convert.ToDecimal(OutputsCurrentLoadsViewModel.loadWindLoad1.Where(x => x.Description1 == "CXW").Select(s => s.Values).FirstOrDefault());

            var CYW = Convert.ToDecimal(OutputsWindLoadsViewModel.loadWindLoad.Where(x => x.Description1 == "CYW").Select(s => s.Values).FirstOrDefault()) + Convert.ToDecimal(OutputsCurrentLoadsViewModel.loadWindLoad1.Where(x => x.Description1 == "CYW").Select(s => s.Values).FirstOrDefault());

            var CXYW = Convert.ToDecimal(OutputsWindLoadsViewModel.loadWindLoad.Where(x => x.Description1 == "CXYW").Select(s => s.Values).FirstOrDefault()) + Convert.ToDecimal(OutputsCurrentLoadsViewModel.loadWindLoad1.Where(x => x.Description1 == "CXYW").Select(s => s.Values).FirstOrDefault()) - 0.48m * Convert.ToDecimal(InputsVesselViewModel.loadVesselP.Where(x => x.Description == "LWL").Select(s => s.MainValue).FirstOrDefault()) * CXW;

            list.ForEach(x =>
            {
                if (x.Description1 == "CXW") { x.Values = Convert.ToDecimal(CXW); }
                if (x.Description1 == "CYW") { x.Values = Convert.ToDecimal(CYW); }
                if (x.Description1 == "CXYW") { x.Values = Convert.ToDecimal(CXYW); }

            });



            return list;
        }

        //====================final Mooring Calculation Load===========

        private decimal fYkN;
        public decimal FYkN
        {
            get
            {
                return fYkN;
            }
            set
            {
                fYkN = value;
                RaisePropertyChanged("FYkN");
            }
        }

        private decimal mrkN_m;
        public decimal MrkN_m
        {
            get
            {
                return mrkN_m;
            }
            set
            {
                mrkN_m = value;
                RaisePropertyChanged("MrkN_m");
            }
        }

        private decimal xcg_m;
        public decimal Xcg_m
        {
            get
            {
                return xcg_m;
            }
            set
            {
                xcg_m = value;
                RaisePropertyChanged("Xcg_m");
            }
        }

        private decimal aKN;
        public decimal AKN
        {
            get
            {
                return aKN;
            }
            set
            {
                aKN = value;
                RaisePropertyChanged("AKN");
            }
        }

        private decimal bKN;
        public decimal BKN
        {
            get
            {
                return bKN;
            }
            set
            {
                bKN = value;
                RaisePropertyChanged("BKN");
            }
        }

        private decimal cKN;
        public decimal CKN
        {
            get
            {
                return cKN;
            }
            set
            {
                cKN = value;
                RaisePropertyChanged("CKN");
            }
        }

        private decimal sYM;
        public decimal SYM
        {
            get
            {
                return sYM;
            }
            set
            {
                sYM = value;
                RaisePropertyChanged("SYM");
            }
        }

        private decimal yradian;
        public decimal Yradian
        {
            get
            {
                return yradian;
            }
            set
            {
                yradian = value;
                RaisePropertyChanged("Yradian");
            }
        }

        //GetFinalMooringForSave

        private List<TempMooringLine> GetFinalMooringForSave()
        {

            var data = MooringLiness.GetList().ToList();
            var datakk = GetFinalMooringLinesCal();
            List<TempMooringLine> list = new List<TempMooringLine>();
            try
            {
                foreach (var x in data)
                {


                    var C9 = !string.IsNullOrEmpty(x.Xch.ToString()) ? Convert.ToDouble(x.Xch) : 0;
                    var D9 = !string.IsNullOrEmpty(x.Ych.ToString()) ? Convert.ToDouble(x.Ych) : 0;
                    var E9 = !string.IsNullOrEmpty(x.Zch.ToString()) ? Convert.ToDecimal(x.Zch) : 0;
                    var F9 = !string.IsNullOrEmpty(x.Xbl.ToString()) ? Convert.ToDouble(x.Xbl) : 0;
                    var G9 = !string.IsNullOrEmpty(x.Ybl.ToString()) ? Convert.ToDouble(x.Ybl) : 0;
                    var H9 = !string.IsNullOrEmpty(x.Zbl.ToString()) ? Convert.ToDecimal(x.Zbl) : 0;
                    var A = !string.IsNullOrEmpty(x.a.ToString()) ? Convert.ToDecimal(x.a) : 0;
                    var N = !string.IsNullOrEmpty(x.n.ToString()) ? Convert.ToDecimal(x.n) : 0;
                    var MBSrope1 = !string.IsNullOrEmpty(x.MBSrope.ToString()) ? Convert.ToDecimal(x.MBSrope) : 0;
                    var l11 = Convert.ToDecimal(Math.Round(Math.Sqrt(((C9 - F9) * (C9 - F9)) + ((D9 - G9) * (D9 - G9))), 2));
                    var ΔZi1 = !string.IsNullOrEmpty(C9.ToString()) ? (E9 - H9) : 0;
                    var Ei1 = !string.IsNullOrEmpty(x.E.ToString()) ? Convert.ToDecimal(x.E) : 0;
                    var R9 = Convert.ToDouble(l11) > 0 ? Math.Round(Convert.ToDecimal((Math.Atan(Convert.ToDouble(ΔZi1) / Convert.ToDouble(l11))) * (180.0 / Math.PI)), 3) : Math.Round(Convert.ToDecimal((Math.Atan(0)) * (180.0 / Math.PI)), 3);
                    var I9 = !string.IsNullOrEmpty(x.l0.ToString()) ? Convert.ToDecimal(x.l0) : 0;
                    var S9 = R9 > 0 ? Convert.ToDecimal(Math.Round(Convert.ToDouble(Math.Cos(Convert.ToDouble(R9) * (Math.PI / 180.0))), 5)) : 0;
                    var M9 = Math.Round(N * A, 3);

                    decimal Li1 = 0; decimal V9 = 0; decimal KYI9 = 0;
                    if (S9 > 0)
                    {
                        Li1 = I9 == 0 ? 0 : Math.Round((I9 + l11 / S9), 8);
                        V9 = Li1 == 0 ? 0 : Math.Round(M9 * Ei1 / Li1, 3);
                        KYI9 = Convert.ToDouble(l11) > 0 ? V9 * Convert.ToDecimal((G9 - D9) / Convert.ToDouble(l11)) * S9 : V9 * 0 * S9;

                    }

                    list.Add(new TempMooringLine()
                    {
                        RopeId = x.RopeId,
                        //AssignNumber=x.AssignNumber,
                        //Location=x.Location,
                        //Certi_No=x.Certi_No,
                        //UniqueId=x.UniqueId,

                        Id = x.Id,
                        Xch = !string.IsNullOrEmpty(x.Xch.ToString()) ? Convert.ToDecimal(x.Xch) : 0,
                        Ych = !string.IsNullOrEmpty(x.Ych.ToString()) ? Convert.ToDecimal(x.Ych) : 0,

                        Zch = !string.IsNullOrEmpty(x.Zch.ToString()) ? Convert.ToDecimal(x.Zch) : 0,
                        Xbl = !string.IsNullOrEmpty(x.Xbl.ToString()) ? Convert.ToDecimal(x.Xbl) : 0,

                        Ybl = !string.IsNullOrEmpty(x.Ybl.ToString()) ? Convert.ToDecimal(x.Ybl) : 0,
                        Zbl = !string.IsNullOrEmpty(x.Zbl.ToString()) ? Convert.ToDecimal(x.Zbl) : 0,
                        l1 = l11,
                        ΔZi = !string.IsNullOrEmpty(C9.ToString()) ? (E9 - H9) : 0,
                        a = A,
                        n = N,
                        ai = Math.Round((N * A), 2),
                        MBSrope = MBSrope1,
                        MBS = Math.Round((MBSrope1 * N), 2),
                        Ei = Math.Round(Ei1, 3),
                        cosθi = Convert.ToDouble(l11) > 0 ? Math.Round(Convert.ToDecimal((G9 - D9) / Convert.ToDouble(l11)), 3) : 0,
                        φi = Math.Round(R9, 3),
                        cosφi = Math.Round(S9, 3),
                        l0 = Math.Round(I9, 2),
                        Li = Math.Round(Li1, 3),
                        ki = Math.Round(V9, 3),
                        kyi = Math.Round(KYI9, 3),
                        kyi_Xch = Math.Round(KYI9 * Convert.ToDecimal(C9), 3),
                        kyi_Xch2 = Math.Round((KYI9 * Convert.ToDecimal(C9) * Convert.ToDecimal(C9)), 3)



                    });
                }
                //================
                var vesselsinput = Convert.ToDecimal(VesselPs.GetList().Where(x => x.Description == "Xcg").Select(s => s.MainValue).FirstOrDefault());
                new OutputsFinalForcesViewModel();
                var finalforce = OutputsFinalForcesViewModel.loadWindLoad2.ToList();
                fYkN = Convert.ToDecimal(finalforce.Where(x => x.Description1 == "CYW").Select(s => s.Values).FirstOrDefault()) * Convert.ToDecimal(9.81);
                RaisePropertyChanged("FYkN");
                mrkN_m = Convert.ToDecimal(finalforce.Where(x => x.Description1 == "CXYW").Select(s => s.Values).FirstOrDefault()) * Convert.ToDecimal(9.81);
                RaisePropertyChanged("MrkN_m");
                xcg_m = vesselsinput;
                RaisePropertyChanged("Xcg_m");

                aKN = list.Sum(s => Convert.ToDecimal(s.kyi));
                RaisePropertyChanged("AKN");
                bKN = list.Sum(s => Convert.ToDecimal(s.kyi_Xch));
                RaisePropertyChanged("BKN");
                cKN = list.Sum(s => Math.Round(Convert.ToDecimal(s.kyi_Xch2), 2));
                RaisePropertyChanged("CKN");
                var newvel = (aKN * cKN) - (bKN * bKN);
                if (newvel > 0)
                {
                    sYM = (fYkN * cKN - (mrkN_m + fYkN * xcg_m) * bKN) / (newvel);
                }
                else
                    sYM = 0;
                RaisePropertyChanged("SYM");
                var val22 = (bKN * bKN) - (aKN * cKN);
                if (val22 > 0)
                    yradian = (fYkN * bKN - (mrkN_m + fYkN * xcg_m) * aKN) / (val22);
                else
                    yradian = 0;
                RaisePropertyChanged("yradian");


                List<TempMooringLine> list1 = new List<TempMooringLine>();

                foreach (var item in list)
                {
                    var ropes = data.Where(x => x.RopeId == item.RopeId).FirstOrDefault();

                    item.AssignNumber = ropes.AssignNumber;
                    item.Location = ropes.Location;
                    item.Certi_No = ropes.Certi_No;
                    item.UniqueId = ropes.UniqueId;

                    var kyi1 = item.kyi == 0 ? 0 : Convert.ToDecimal(item.kyi * (sYM + item.Xch * yradian)) / 9.81m;
                    var Ti1 = item.kyi == 0 ? 0 : kyi1 / (item.cosθi * item.cosφi);
                    var FSi1 = Ti1 == 0 ? 0 : item.MBS / Ti1;
                    list.Where(x => x.Id == item.Id).ToList().ForEach(x => { x.Fyi = Math.Round(kyi1, 3); x.Ti = Math.Round(Ti1, 3); x.FSi = Math.Round(FSi1, 3); });


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;



        }


        public List<MooringLines> GetFinalMooringLinesCal()
        {
            List<MooringLines> LoadMooring = new List<MooringLines>();
            DateTime dd = DateTime.Now.Date;

            string qry = "MooringLineInputCalulator";
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
            System.Data.DataTable datatbl = new System.Data.DataTable();
            sda.Fill(datatbl);



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
                    Zbl = Convert.ToDecimal((row["Zbl"] == DBNull.Value) ? 0 : row["Zbl"]),
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

            return LoadMooring;
        }

        private static ObservableCollection<PortNameCombo> portname = new ObservableCollection<PortNameCombo>();
        public ObservableCollection<PortNameCombo> PortName
        {
            get
            {
                return portname;
            }
            set
            {

                portname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PortName"));
            }
        }
        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }
}
