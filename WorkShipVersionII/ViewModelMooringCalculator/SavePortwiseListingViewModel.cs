using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
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
using WorkShipVersionII.ViewsMooringCalulator;

namespace WorkShipVersionII.ViewModelMooringCalculator
{
    public class SavePortwiseListingViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public ICommand HelpCommand { get; private set; }

        public SavePortwiseListingViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            openSaveForm = new RelayCommand(() => ExecuteSaveForm());
            deleteCommand = new RelayCommand<PortWiseListingClass>(DeleteMooringList);
            editCommand = new RelayCommand<PortWiseListingClass>(EditMooringList);

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            GetLoadMooringCalculation();

        }
        public SavePortwiseListingViewModel(ObservableCollection<PortWiseListingClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            //LoadUserAccess.Clear();
            GetLoadMooringCalculation();


        }


        private void ExecuteSaveForm()
        {

            SaveCurrentValuesViewModel vm = new SaveCurrentValuesViewModel();
            ChildWindowManager.Instance.ShowChildWindow(new SaveCurrentValuesView() { DataContext = vm });
        }
        private ICommand openSaveForm;

        public ICommand OpenSaveForm
        {
            get { return openSaveForm; }
            set { openSaveForm = value; }
        }
        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }
        private ICommand editCommand;
        public ICommand EditCommand
        {
            get { return editCommand; }
            set { editCommand = value; }
        }

        private static ObservableCollection<PortWiseListingClass> loadMooringList = new ObservableCollection<PortWiseListingClass>();

        public ObservableCollection<PortWiseListingClass> LoadMooringList
        {
            get
            {
                return loadMooringList;
            }
            set
            {
                loadMooringList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringList"));

            }
        }

        public static ObservableCollection<PortWiseListingClass> loadUserAccess = new ObservableCollection<PortWiseListingClass>();
        public ObservableCollection<PortWiseListingClass> LoadUserAccess
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

        private void GetLoadMooringCalculation()
        {

            try
            {
                LoadUserAccess.Clear();
                string qry = "SELECT DISTINCT PortId,PortName,InputDate FROM InputMooringCalculation order by InputDate Desc";
                SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    LoadUserAccess.Add(new PortWiseListingClass()
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

        private void EditMooringList(PortWiseListingClass pw)
        {
            try
            {

                if ((MessageBox.Show("Do you want to Edit?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
                {

                   
                    //var oldInputId = pw.OldInputId;
                    var qry = "Select * from InputGeneralParticulars where PortId=" + pw.PortId+ "";
                    SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (sc.con.State == ConnectionState.Closed)
                        sc.con.Open();


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int Oldid = Convert.ToInt32(dt.Rows[i]["OldInputId"]);
                        SqlCommand cmd = new SqlCommand("Update tblGeneralP Set Name =@Name,Description=@Description,MainValue=@MainValue,DefaultValue=@DefaultValue,Units=@Units  where Id=" + Oldid + "", sc.con);
                      //  SqlCommand cmd = new SqlCommand("UpdatetblGeneralP", sc.con);
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", Oldid);
                        cmd.Parameters.AddWithValue("@Name", dt.Rows[i]["Name"].ToString());
                        cmd.Parameters.AddWithValue("@Description", dt.Rows[i]["Description"].ToString());
                        cmd.Parameters.AddWithValue("@MainValue", Convert.ToDecimal(dt.Rows[i]["MainValue"]));
                        string myvalues = dt.Rows[i]["DefaultValue"].ToString();
                        if (string.IsNullOrEmpty(myvalues))
                        {
                            cmd.Parameters.AddWithValue("@DefaultValue", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DefaultValue", Convert.ToDecimal(dt.Rows[i]["DefaultValue"]));
                        }
                        cmd.Parameters.AddWithValue("@Units", dt.Rows[i]["Units"].ToString());
                        cmd.ExecuteNonQuery();

                    }
                    sc.con.Close();

                    //==================================================================================

                    var qry1 = "Select * from InputPrincipalParticulars where PortId=" + pw.PortId + "";
                    SqlDataAdapter adp1 = new SqlDataAdapter(qry1, sc.con);
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        if (sc.con.State == ConnectionState.Closed)
                            sc.con.Open();


                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            int Oldid = Convert.ToInt32(dt1.Rows[i]["OldInputId"]);
                            //SqlCommand cmd = new SqlCommand("Update tblVesselP Set Name =@Name,Description=@Description,MainValue=@MainValue,DefaultValue=@DefaultValue,Units=@Units  where Id=" + Oldid + "", sc.con);
                            SqlCommand cmd = new SqlCommand("UpdatetblVesselP", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", Oldid);
                            cmd.Parameters.AddWithValue("@Name", dt1.Rows[i]["Name"].ToString());
                            cmd.Parameters.AddWithValue("@Description", dt1.Rows[i]["Description"].ToString());
                            cmd.Parameters.AddWithValue("@MainValue", Convert.ToDecimal(dt1.Rows[i]["MainValue"]));
                            string myvalues = dt1.Rows[i]["DefaultValue"].ToString();
                            if (string.IsNullOrEmpty(myvalues))
                            {
                                cmd.Parameters.AddWithValue("@DefaultValue", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@DefaultValue", Convert.ToDecimal(dt1.Rows[i]["DefaultValue"]));
                            }

                            cmd.Parameters.AddWithValue("@Units", dt1.Rows[i]["Units"].ToString());
                            cmd.ExecuteNonQuery();
                        }
                        sc.con.Close();
                    }

                    //===============================================================================

                    var qry2 = "Select * from InputWindArea where PortId=" + pw.PortId + "";
                    SqlDataAdapter adp2 = new SqlDataAdapter(qry2, sc.con);
                    DataTable dt2 = new DataTable();
                    adp2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        if (sc.con.State == ConnectionState.Closed)
                            sc.con.Open();

                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            int Oldid = Convert.ToInt32(dt2.Rows[i]["OldInputId"]);
                            //SqlCommand cmd = new SqlCommand("Update tblWindAreas Set Name =@Name,Description=@Description,MainValue=@MainValue,DefaultValue=@DefaultValue,Units=@Units  where Id=" + Oldid + "", sc.con);
                            SqlCommand cmd = new SqlCommand("UpdatettblWindAreas", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", Oldid);
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
                            cmd.ExecuteNonQuery();
                        }
                        sc.con.Close();
                    }
                    //==================================================================================

                    var qry3 = "Select * from InputWindAndCurrentParameters where PortId=" + pw.PortId + "";
                    SqlDataAdapter adp3 = new SqlDataAdapter(qry3, sc.con);
                    DataTable dt3 = new DataTable();
                    adp3.Fill(dt3);
                    if (dt3.Rows.Count > 0)
                    {
                        if (sc.con.State == ConnectionState.Closed)
                            sc.con.Open();


                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            int Oldid = Convert.ToInt32(dt3.Rows[i]["OldInputId"]);
                            // SqlCommand cmd = new SqlCommand("Update tblWindandCurrent Set Name =@Name,Description=@Description,MainValue=@MainValue,DefaultValue=@DefaultValue,Units=@Units  where Id=" + Oldid + "", sc.con);
                            SqlCommand cmd = new SqlCommand("UpdatettblWindandCurrent", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", Oldid);
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
                            cmd.ExecuteNonQuery();
                        }
                        sc.con.Close();
                    }
                    //==========================================================================

                    var qry4 = "Select * from InputMooringCalculation where PortId=" + pw.PortId + " ";
                    SqlDataAdapter adp4 = new SqlDataAdapter(qry4, sc.con);
                    DataTable dt4 = new DataTable();
                    adp4.Fill(dt4);
                    if (dt4.Rows.Count > 0)
                    {
                        if (sc.con.State == ConnectionState.Closed)
                            sc.con.Open();

                        for (int i = 0; i < dt4.Rows.Count; i++)
                        {
                            int Oldid = Convert.ToInt32(dt4.Rows[i]["OldInputId"]);
                            //SqlCommand cmd = new SqlCommand("Update tblMooringLines Set Xch=@Xch,Ych=@Ych,Zch=@Zch,Xbl=@Xbl,Ybl=@Ybl,Zbl=@Zbl,l0=@l0,E=@E,n=@n,a=@a,MBSrope=@MBSrope,RopeId=@RopeId  where Id=" + Oldid + "", sc.con);
                            SqlCommand cmd = new SqlCommand("UpdatettblMooringLines", sc.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Id", Oldid);
                            cmd.Parameters.AddWithValue("@Xch", Convert.ToDecimal(dt4.Rows[i]["Xch"]));
                            cmd.Parameters.AddWithValue("@Ych", Convert.ToDecimal(dt4.Rows[i]["Ych"]));
                            cmd.Parameters.AddWithValue("@Zch", Convert.ToDecimal(dt4.Rows[i]["Zch"]));
                            cmd.Parameters.AddWithValue("@Xbl", Convert.ToDecimal(dt4.Rows[i]["Xbl"]));
                            cmd.Parameters.AddWithValue("@Ybl", Convert.ToDecimal(dt4.Rows[i]["Ybl"]));
                            cmd.Parameters.AddWithValue("@Zbl", Convert.ToDecimal(dt4.Rows[i]["Zbl"]));
                            cmd.Parameters.AddWithValue("@l0", Convert.ToDecimal(dt4.Rows[i]["l0"]));
                            cmd.Parameters.AddWithValue("@E", Convert.ToDecimal(dt4.Rows[i]["E"]));
                            cmd.Parameters.AddWithValue("@n", Convert.ToDecimal(dt4.Rows[i]["n"]));
                            cmd.Parameters.AddWithValue("@a", Convert.ToDecimal(dt4.Rows[i]["a"]));
                            cmd.Parameters.AddWithValue("@MBSrope", Convert.ToDecimal(dt4.Rows[i]["MBSrope"]));
                            cmd.Parameters.AddWithValue("@RopeId", Convert.ToInt32(dt4.Rows[i]["RopeId"]));
                            cmd.ExecuteNonQuery();
                        }
                        sc.con.Close();
                        MessageBox.Show("The data from this operation has now been filled in the 'Input and Output sheets of this mooring calculator', you may edit the data and Save data under a new name.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    //MessageBox.Show("Please Edit List", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }






        private void DeleteMooringList(PortWiseListingClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    sc.con.Open();
                    var portId = mw.PortId;
                    SqlCommand cmd = new SqlCommand("Delete from InputGeneralParticulars where PortId='" + portId + "'; Delete from InputPrincipalParticulars where PortId='" + portId + "';Delete from InputWindArea where PortId='" + portId + "';Delete from InputWindAndCurrentParameters where PortId = '" + portId + "'; Delete from InputMooringCalculation where PortId = '" + portId + "'", sc.con);
                    cmd.ExecuteNonQuery();
                }
                sc.con.Close();
                GetLoadMooringCalculation();
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
