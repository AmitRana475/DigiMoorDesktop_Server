using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows;
//using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data.Entity;
using System.Windows.Forms;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;
using System.Windows.Data;

namespace WorkShipVersionII.WorkHoursViewModel
{
  public class ModelViewMooringRopeInspection:ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        public ModelViewMooringRopeInspection()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            deletePhoto = new RelayCommand<MooringRopeInspectionClass>(DeleteImage);
            deletePhoto1 = new RelayCommand<MooringRopeInspectionClass>(DeleteImage1);

            viewCommand = new RelayCommand<MooringRopeInspectionClass>(Viewimage);
           
            viewCommand1 = new RelayCommand<MooringRopeInspectionClass>(Viewimage1);
            cancelCommand = new RelayCommand(CancelRopeInspection);
       
            LoadInspections.Clear();
            LoadInspections = GetMooringInspection();

            //DeletePhoto
        }

        //public ModelViewMooringRopeInspection(MooringRopeInspectionClass ins)
        //{
        //}
        private ICommand deletePhoto;
        public ICommand DeletePhoto
        {
            get { return deletePhoto; }
            set { deletePhoto = value; }
        }

        private ICommand deletePhoto1;
        public ICommand DeletePhoto1
        {
            get { return deletePhoto1; }
            set { deletePhoto1 = value; }
        }

        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }

        private ICommand viewCommand1;
        public ICommand ViewCommand1
        {
            get { return viewCommand1; }
            set { viewCommand1 = value; }
        }

        public ModelViewMooringRopeInspection(TotalInspections total)
        {
            // Edit Operation
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            deletePhoto = new RelayCommand<MooringRopeInspectionClass>(DeleteImage);
            deletePhoto1 = new RelayCommand<MooringRopeInspectionClass>(DeleteImage1);
            viewCommand = new RelayCommand<MooringRopeInspectionClass>(Viewimage);
            viewCommand1 = new RelayCommand<MooringRopeInspectionClass>(Viewimage1);
            cancelCommand = new RelayCommand(CancelRopeInspection);

            StaticHelper.Inspectionid = total.id;
            LoadInspections.Clear();
            LoadInspections = GetMooringInspection();
        }


        private void Viewimage(MooringRopeInspectionClass mw)
        {
            try
            {
              
                StaticHelper.ViewId = mw.Id;
                StaticHelper.Photos = "Image1";
                StaticHelper.TbName = "MooringRopeInspection";
                ChildWindowManager.Instance.ShowChildWindow(new ViewPhoto());

                
            }
            catch (Exception ex)
            {
               
            }
        }

        private void DeleteImage(MooringRopeInspectionClass mw)
        {
            try
            {

                //StaticHelper.ViewId = mw.Id;
                //StaticHelper.Photos = "Photo1";
               // StaticHelper.TbName = "MooringRopeInspection";

                SqlDataAdapter adp = new SqlDataAdapter("update MooringRopeInspection set Image1=null,Photo1=null where ID= " + mw.Id + "", sc.con);
                System.Data.DataTable dt = new System.Data.DataTable();
                adp.Fill(dt);

                LoadInspections.Clear();
                LoadInspections = GetMooringInspection();


                // int index = ((TotalInspections));
                //TotalInspections ss = new TotalInspections();

                //ModelViewMooringRopeInspection vm = new ModelViewMooringRopeInspection(ss);

                //var lostdata = new ObservableCollection<TotalInspections>().Where(x => x.id == mw.Id).SingleOrDefault();
                //ModelViewMooringRopeInspection cc = new ModelViewMooringRopeInspection(lostdata);



            }
            catch (Exception ex)
            {

            }
        }

        private void DeleteImage1(MooringRopeInspectionClass mw)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("update MooringRopeInspection set Image2=null,Photo2=null where ID= " + mw.Id + "", sc.con);
                System.Data.DataTable dt = new System.Data.DataTable();
                adp.Fill(dt);

                LoadInspections.Clear();
                LoadInspections = GetMooringInspection();

            }
            catch (Exception ex)
            {

            }
        }

        private void Viewimage1(MooringRopeInspectionClass mw)
        {
            try
            {

                StaticHelper.ViewId = mw.Id;
                StaticHelper.Photos = "Image2";
                StaticHelper.TbName = "MooringRopeInspection";
                ChildWindowManager.Instance.ShowChildWindow(new ViewPhoto());


            }
            catch (Exception ex)
            {
               
            }
        }

        private string pVisible;
        public string PVisible
        {
            get
            {
                if (pVisible == null)
                {
                    PVisible = "Collapsed";
                }
                return pVisible;
            }
            set
            {
                pVisible = value;
                RaisePropertyChanged("PVisible");
            }
        }

        private void CancelRopeInspection()
        {
            var lostdata = new ObservableCollection<MooringRopeInspectionClass>(sc.MooringRopeInspectionTbl.ToList());
            RopeInspectionListViewModel cc = new RopeInspectionListViewModel(lostdata);

            ChildWindowManager.Instance.CloseChildWindow();
        }
        public static ObservableCollection<MooringRopeInspectionClass> loadUserAccess = new ObservableCollection<MooringRopeInspectionClass>();
        public ObservableCollection<MooringRopeInspectionClass> LoadInspections
        {
            get
            {
                return loadUserAccess;
            }
            set
            {
                loadUserAccess = value;
                //OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));

                RaisePropertyChanged("LoadInspections");
            }
        }


        private CollectionViewSource _ViewList = new CollectionViewSource();
        public CollectionViewSource ViewList
        {
            get { return _ViewList; }
            set
            {
                _ViewList = value;
                RaisePropertyChanged("ViewList");
            }
        }




        private ObservableCollection<MooringRopeInspectionClass> GetMooringInspection(TotalInspections TotIns)
        {

            DateTime dd = TotIns.InspectDate;
            MooringInspect.InspectDate = TotIns.InspectDate;
            MooringInspect.InspectBy = TotIns.InspectBy;

            string qry = "select * from MooringRopeInspection where InspectDate = '" + dd.ToShortDateString() + "'";  // 2019-07-12 00:00:00.000 in sql tbl
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
            System.Data.DataTable datatbl = new System.Data.DataTable();
            sda.Fill(datatbl);

            var ranklist = new ObservableCollection<MooringRopeInspectionClass>();

            for (int i = 0; i < datatbl.Rows.Count; i++)
            {
                ranklist.Add(new MooringRopeInspectionClass()
                {

                    Id = Convert.ToInt32(datatbl.Rows[i]["Id"]),
                    InspectDate = Convert.ToDateTime(datatbl.Rows[i]["InspectDate"]),
                    ExternalRating_A = Convert.ToInt32(datatbl.Rows[i]["ExternalRating_A"]),
                    InternalRating_A = Convert.ToInt32(datatbl.Rows[i]["InternalRating_A"]),
                    AverageRating_A = Convert.ToInt32(datatbl.Rows[i]["AverageRating_A"]),

                    LengthOFAbrasion_A = datatbl.Rows[i]["LengthOFAbrasion_A"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["LengthOFAbrasion_A"]),
                    DistanceOutboard_A = datatbl.Rows[i]["DistanceOutboard_A"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["DistanceOutboard_A"]),
                    CutYarnCount_A = datatbl.Rows[i]["CutYarnCount_A"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["CutYarnCount_A"]),
                    LengthOFGlazing_A = datatbl.Rows[i]["LengthOFGlazing_A"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["LengthOFGlazing_A"]),

                    InternalRating_B = datatbl.Rows[i]["InternalRating_B"] == DBNull.Value ? 0 : Convert.ToInt32(datatbl.Rows[i]["InternalRating_B"]),
                    ExternalRating_B = datatbl.Rows[i]["ExternalRating_B"] == DBNull.Value ? 0 : Convert.ToInt32(datatbl.Rows[i]["ExternalRating_B"]),

                    AverageRating_B = datatbl.Rows[i]["AverageRating_B"] == DBNull.Value ? 0 : Convert.ToInt32(datatbl.Rows[i]["AverageRating_B"]),


                    LengthOFAbrasion_B = datatbl.Rows[i]["LengthOFAbrasion_B"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["LengthOFAbrasion_B"]),
                    DistanceOutboard_B = datatbl.Rows[i]["DistanceOutboard_B"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["DistanceOutboard_B"]),
                    CutYarnCount_B = datatbl.Rows[i]["CutYarnCount_B"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["CutYarnCount_B"]),
                    LengthOFGlazing_B = datatbl.Rows[i]["LengthOFGlazing_B"] == DBNull.Value ? 0 : Convert.ToDecimal(datatbl.Rows[i]["LengthOFGlazing_B"]),

                    Chafe_guard_condition = datatbl.Rows[i]["Chafe_guard_condition"].ToString(),

                    Twist = Convert.ToInt32(datatbl.Rows[i]["Twist"]),

                  



                });


            }

            return ranklist;
        }

        public ObservableCollection<MooringRopeInspectionClass> GetMooringInspection()
        {

            DateTime dd = DateTime.Now.Date;

            //string qry = "select a.*,b.assignednumber,b.location,c.certificatenumber from MooringRopeInspection a inner join MooringWinchDetail b on a.winchid=b.id inner join MooringRopeDetail c on a.RopeId=c.Id";

            string qry = "ViewMooringOperation";
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
            sda.SelectCommand.Parameters.AddWithValue("@inspectionid", StaticHelper.Inspectionid);
            System.Data.DataTable datatbl = new System.Data.DataTable();
            sda.Fill(datatbl);

            var ranklist = new ObservableCollection<MooringRopeInspectionClass>();

            for (int i = 0; i < datatbl.Rows.Count; i++)
            {
                ranklist.Add(new MooringRopeInspectionClass()
                {
                    Id = Convert.ToInt32(datatbl.Rows[i]["Id"]),
                    InspectBy = datatbl.Rows[i]["InspectBy"].ToString(),
                    InspectDate1 = Convert.ToDateTime(datatbl.Rows[i]["InspectDate"]).ToString("yyyy-MM-dd"),

                    AssignNumber = datatbl.Rows[i]["assignednumber"] == DBNull.Value ? "Not Assigned" : datatbl.Rows[i]["assignednumber"].ToString(),
                    Location = datatbl.Rows[i]["location"] == DBNull.Value ? "Not Assigned" : datatbl.Rows[i]["location"].ToString(),
                    //AssignNumber = datatbl.Rows[i]["assignednumber"].ToString(),
                    //Location = datatbl.Rows[i]["location"].ToString(),
                    // RpoeType = datatbl.Rows[i]["RopeType"].ToString(),
                    Certi_No = datatbl.Rows[i]["certificatenumber"].ToString(),
                    UniqueId = datatbl.Rows[i]["UniqueId"].ToString(),
                    //RopeId = Convert.ToInt32(datatbl.Rows[i]["RopeId"]),
                    //WinchId = Convert.ToInt32(datatbl.Rows[i]["WinchId"]),

                    ExternalRating_A = Convert.ToInt32(datatbl.Rows[i]["ExternalRating_A"]),
                    InternalRating_A = Convert.ToInt32(datatbl.Rows[i]["InternalRating_A"]),
                    AverageRating_A = Convert.ToInt32(datatbl.Rows[i]["AverageRating_A"]),
                    LengthOFAbrasion_A = Convert.ToDecimal(datatbl.Rows[i]["LengthOFAbrasion_A"]),

                    DistanceOutboard_A = Convert.ToDecimal(datatbl.Rows[i]["DistanceOutboard_A"]),
                    CutYarnCount_A = Convert.ToDecimal(datatbl.Rows[i]["CutYarnCount_A"]),
                    LengthOFGlazing_A = Convert.ToDecimal(datatbl.Rows[i]["LengthOFGlazing_A"]),
                    ExternalRating_B = Convert.ToInt32(datatbl.Rows[i]["ExternalRating_B"]),


                    InternalRating_B = Convert.ToInt32(datatbl.Rows[i]["InternalRating_B"]),
                    AverageRating_B = Convert.ToInt32(datatbl.Rows[i]["AverageRating_B"]),
                    LengthOFAbrasion_B = Convert.ToDecimal(datatbl.Rows[i]["LengthOFAbrasion_B"]),
                    DistanceOutboard_B = Convert.ToInt32(datatbl.Rows[i]["DistanceOutboard_B"]),


                    CutYarnCount_B = Convert.ToDecimal(datatbl.Rows[i]["CutYarnCount_B"]),
                    LengthOFGlazing_B = Convert.ToDecimal(datatbl.Rows[i]["LengthOFGlazing_B"]),
                    Chafe_guard_condition = datatbl.Rows[i]["Chafe_guard_condition"].ToString(),
                    Twist = Convert.ToInt32(datatbl.Rows[i]["Twist"]),

                    Photo11 = datatbl.Rows[i]["Photo11"].ToString(),
                    Photo12 = datatbl.Rows[i]["Photo12"].ToString(),

                    //Photo1 = (byte[])(datatbl.Rows[i]["Photo1"]),
                    //Photo2 = (byte[])(datatbl.Rows[i]["Photo2"])


                      Photo1 = (datatbl.Rows[i]["Photo1"] == DBNull.Value) ? null : (byte[])datatbl.Rows[i]["Photo1"],
                    Photo2 = (datatbl.Rows[i]["Photo2"] == DBNull.Value) ? null : (byte[])datatbl.Rows[i]["Photo2"],


                });


            }

            return ranklist;
        }

      

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }

        public static MooringRopeInspectionClass _mooringInspact = new MooringRopeInspectionClass();

        public MooringRopeInspectionClass MooringInspect
        {
            get { return _mooringInspact; }
            set
            {
                _mooringInspact = value;
                RaisePropertyChanged("MooringInspect");
            }
        }


     
        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}