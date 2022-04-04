using ClosedXML.Excel;
using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class LooseEquipmentListViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private BackgroundWorker _ExportLooseEquipmentList;
        public ICommand HelpCommand { get; private set; }

        public LooseEquipmentListViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                _ExportLooseEquipmentList = new BackgroundWorker();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

            viewCommand = new RelayCommand<JoiningShackleClass>(Viewropediscard);
            viewCommand1 = new RelayCommand<RopeTailClass>(Viewropetail);
            viewCommand2 = new RelayCommand<ChainStopperClass>(Viewchainstopper);
            viewCommandCG = new RelayCommand<ChafeGuardClass>(ViewCG);
            viewCommandWBTestKit = new RelayCommand<WBTestKitClass>(ViewWBTestKit);
            editCommandj = new RelayCommand<JoiningShackleClass>(EditJoiningShackle);
            EditCommandRT = new RelayCommand<RopeTailClass>(EditRopeTail);
            editCommandCG = new RelayCommand<ChafeGuardClass>(EditCG);
            editCommandWBT = new RelayCommand<WBTestKitClass>(EditWBT);
            deleteCommand = new RelayCommand<JoiningShackleClass>(DeleteJShcackle);
            deleteCommand1 = new RelayCommand<ChainStopperClass>(DeleteCStopper);

            deleteCommand2 = new RelayCommand<RopeTailClass>(DeleteRopeTail);

            deleteCommandCG = new RelayCommand<ChafeGuardClass>(DeleteCG);
            deleteCommandWBKit = new RelayCommand<WBTestKitClass>(DeleteWBTestKit);
            //LoadMooringWinchList.Clear();

            _ExportLooseEquipmentListCommands = new RelayCommand(() => _ExportLooseEquipmentList.RunWorkerAsync(), () => !_ExportLooseEquipmentList.IsBusy);
            _ExportLooseEquipmentList.DoWork += new DoWorkEventHandler(ExportLooseEquipmentListMethod);

            editCommandChainStopper = new RelayCommand<ChainStopperClass>(EditChainStopper);
            // LoadMooringWinchList = sc.MooringWinch.ToList();
            //var data = sc.MooringWinch.ToList();
            // sc.ObservableCollectionList(LoadMooringWinchList, GetMooringWinchList());

            // OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringWinchList"));
            GetJShackleList();

            GetRopeTailList();
            GetMRopeList();
            GetFireWireList();
            //GetRopeTailList();
            GetChafeGuardList();
            GetCStopperList();
            GetWBKitList();
            GetTowingRopeList();
            GetSuezRopeList();
            //PennantRopeList();
            //GrommetRopeList();
            // OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

        }


        private void ExportLooseEquipmentListMethod(object sender, DoWorkEventArgs eb)
        {
            try
            {
                ExportLooseEquipmentListMethod1();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void ExportLooseEquipmentListMethod1()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "LooseEquipDetail_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                if (sfd.ShowDialog() == true)
                {

                    DataSet AllTables = new DataSet();

                    SqlDataAdapter adp3 = new SqlDataAdapter("select UniqueID as [Unique Identification No.],ManufactureName as [Manufacturer Name],MBL,Type,CertificateNumber, DateReceived as [Received Date],DateInstalled as [Installed Date],Remarks from JoiningShackle   where IsActive=1", sc.con);
                    DataTable dt3 = new DataTable();
                    adp3.Fill(dt3);
                    dt3.TableName = "JoiningShackle";
                    AllTables.Tables.Add(dt3);

                    //================================================
                  
                    SqlDataAdapter adp5 = new SqlDataAdapter("select CertificateNumber as [Certificate No.] ,UniqueID as [Unique Identification No.],RopeConstruction,Diameter as [DiaMeter(mm)],Length,MBL,LDBF,WLL,ManufactureName as[Manufacture Name],ReceivedDate as [Received Date],InstalledDate as [Installed Date],RopeTagging,Remarks from ropetail where IsActive=1 and LooseETypeId=4", sc.con);
                    DataTable dt5 = new DataTable();
                    adp5.Fill(dt5);
                    dt5.TableName = "Rope Stopper";
                    AllTables.Tables.Add(dt5);

                    //================================================


                    SqlDataAdapter adp2 = new SqlDataAdapter("select CertificateNumber as [CertificateNo.], UniqueID as [Unique Identification No.],ManufactureName as [Manufacture Name],MBL,Length,DateReceived as [DateReceived],DateInstalled as [Date Installed],Remarks from  ChainStopper where IsActive=1", sc.con);
                    DataTable dt2 = new DataTable();
                    adp2.Fill(dt2);
                    dt2.TableName = "ChainStopper";
                    AllTables.Tables.Add(dt2);

                    

                    //===============================================

                    SqlDataAdapter adp4 = new SqlDataAdapter("select CertificateNumber as [Certificate No.],UniqueID as [Unique Identification No.],RopeConstruction,Diameter as [DiaMeter(mm)],Length,MBL,LDBF,WLL,ManufactureName as[Manufacture Name],ReceivedDate as [Received Date],InstalledDate as [Installed Date],RopeTagging,Remarks from ropetail where IsActive=1 and LooseETypeId=3", sc.con);
                    DataTable dt4 = new DataTable();
                    adp4.Fill(dt4);
                    dt4.TableName = "Messenger Rope";
                    AllTables.Tables.Add(dt4);

                    //================================================

                    SqlDataAdapter adp6 = new SqlDataAdapter("select CertificateNumber as [Certificate No.],UniqueID as [Unique Identification No.],RopeConstruction,Diameter as [DiaMeter(mm)],Length,MBL,LDBF,WLL,ManufactureName as[Manufacture Name],ReceivedDate as [Received Date],InstalledDate as [Installed Date],RopeTagging,Remarks from ropetail where IsActive=1 and LooseETypeId=6", sc.con);
                    DataTable dt6 = new DataTable();
                    adp6.Fill(dt6);
                    dt6.TableName = "FireWire";
                    AllTables.Tables.Add(dt6);

                    //================================================
                    

                    SqlDataAdapter adp1 = new SqlDataAdapter("ChafeguardListExcel", sc.con);
                    adp1.SelectCommand.Parameters.AddWithValue("@Id", "");
                    adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    dt1.TableName = "ChafeGuard";
                    AllTables.Tables.Add(dt1);
                    //================================================

                    SqlDataAdapter adp = new SqlDataAdapter("WBTestKitListExport", sc.con);
                    adp.SelectCommand.Parameters.AddWithValue("@Id", "");
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    dt.TableName = "WinchBrakeTestKit";
                    AllTables.Tables.Add(dt);

                    //==========================================
                                                       
                    SqlDataAdapter adp7 = new SqlDataAdapter("select CertificateNumber as [Certificate No.],UniqueID as [Unique Identification No.],RopeConstruction,Diameter as [DiaMeter(mm)],Length,MBL,LDBF,WLL,ManufactureName as[Manufacture Name],ReceivedDate as [Received Date],InstalledDate as [Installed Date],RopeTagging,Remarks from ropetail where IsActive=1 and LooseETypeId=9", sc.con);
                    DataTable dt7 = new DataTable();
                    adp7.Fill(dt7);
                    dt7.TableName = "Towing Rope";
                    AllTables.Tables.Add(dt7);

                    //================================================

                    SqlDataAdapter adp8 = new SqlDataAdapter("select CertificateNumber as [Certificate No.],UniqueID as [Unique Identification No.],RopeConstruction,Diameter as [DiaMeter(mm)],Length,MBL,LDBF,WLL,ManufactureName as[Manufacture Name],ReceivedDate as [Received Date],InstalledDate as [Installed Date],RopeTagging,Remarks from ropetail where IsActive=1 and LooseETypeId=10", sc.con);
                    DataTable dt8 = new DataTable();
                    adp8.Fill(dt8);
                    dt8.TableName = "Suez Rope";
                    AllTables.Tables.Add(dt8);

                    //================================================


                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch
                        {
                            MessageBox.Show("It seems that the earlier excel file is open, please close the excel file and download again to replace !", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                    }


                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (DataTable item in AllTables.Tables)
                        {
                            // var mytbl = item.TableName;
                            var protectedsheet = wb.Worksheets.Add(item);
                            protectedsheet.Name = item.TableName;
                            var projection = protectedsheet.Protect("49WEB$TREET#");
                            projection.InsertColumns = true;
                            projection.InsertRows = true;

                            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            wb.Style.Font.Bold = true;
                        }
                        wb.SaveAs(sfd.FileName);

                    }

                    MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);

                    dt.Dispose();
                    adp.Dispose();
                    dt1.Dispose();
                    adp1.Dispose();
                    dt2.Dispose();
                    adp2.Dispose();
                    dt3.Dispose();
                    adp3.Dispose();
                    dt4.Dispose();
                    adp4.Dispose();
                    dt5.Dispose();
                    adp5.Dispose();
                    dt6.Dispose();
                    adp6.Dispose();
                    dt7.Dispose();
                    adp7.Dispose();
                    dt8.Dispose();
                    adp8.Dispose();
                    AllTables.Dispose();
                }
            }

            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void Viewropediscard(JoiningShackleClass mw)
        {
            try
            {

                StaticHelper.ViewId = mw.Id;
                ChildWindowManager.Instance.ShowChildWindow(new ViewJoiningShackle());
                //ChildWindowManager.Instance.CloseChildWindow();
            }
            catch (Exception ex)
            {

            }
        }
        private ICommand editCommandChainStopper;
        public ICommand EditCommandChainStopper
        {
            get { return editCommandChainStopper; }
            set { editCommandChainStopper = value; }
        }

        private void EditChainStopper(ChainStopperClass mw)
        {
            try
            {
                StaticHelper.ViewId = mw.Id;

                AddChainStopperViewModel cvm = new AddChainStopperViewModel(mw);

                ChildWindowManager.Instance.ShowChildWindow(new AddChainStopperView() { DataContext = cvm });
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void Viewropetail(RopeTailClass mw)
        {
            try
            {

                StaticHelper.ViewId = mw.Id;

                ChildWindowManager.Instance.ShowChildWindow(new ViewRopeTail());

                //ChildWindowManager.Instance.CloseChildWindow();
            }
            catch (Exception ex)
            {

            }
        }
        private void Viewchainstopper(ChainStopperClass mw)
        {
            try
            {

                StaticHelper.ViewId = mw.Id;

                ChildWindowManager.Instance.ShowChildWindow(new ViewChainStopper());

                //ChildWindowManager.Instance.CloseChildWindow();
            }
            catch
            {

            }
        }
        private void ViewCG(ChafeGuardClass mw)
        {
            try
            {
                StaticHelper.ViewId = mw.Id;
                ChildWindowManager.Instance.ShowChildWindow(new ViewChafeGuard());
                //ChildWindowManager.Instance.CloseChildWindow();
            }
            catch
            { }
        }

        private void ViewWBTestKit(WBTestKitClass mw)
        {
            try
            {
                StaticHelper.ViewId = mw.Id;
                ChildWindowManager.Instance.ShowChildWindow(new ViewWBTestKit());
                //ChildWindowManager.Instance.CloseChildWindow();
            }
            catch
            { }
        }
        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }

        //ExportExcel
        private ICommand _ExportLooseEquipmentListCommands;
        public ICommand ExportLooseEquipmentListCommands
        {
            get
            {

                return _ExportLooseEquipmentListCommands;
            }
        }
        private ICommand viewCommand1;
        public ICommand ViewCommand1
        {
            get { return viewCommand1; }
            set { viewCommand1 = value; }
        }



        private ICommand viewCommand2;
        public ICommand ViewCommand2
        {
            get { return viewCommand2; }
            set { viewCommand2 = value; }
        }
        private ICommand viewCommandCG;
        public ICommand ViewCommandCG
        {
            get { return viewCommandCG; }
            set { viewCommandCG = value; }
        }
        private ICommand viewCommandWBTestKit;
        public ICommand ViewCommandWBTestKit
        {
            get { return viewCommandWBTestKit; }
            set { viewCommandWBTestKit = value; }
        }
        public LooseEquipmentListViewModel(ObservableCollection<JoiningShackleClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            editCommandChainStopper = new RelayCommand<ChainStopperClass>(EditChainStopper);
            //  LoadUserAccess.Clear();

            GetJShackleList();

            GetRopeTailList();
            GetMRopeList();
            GetFireWireList();
            GetRopeTailList();
            GetChafeGuardList();
            GetCStopperList();
            GetWBKitList();
            GetTowingRopeList();
            GetSuezRopeList();
            //PennantRopeList();
            //GrommetRopeList();


        }

        private ICommand editCommandj;
        public ICommand EditCommandJ
        {
            get { return editCommandj; }
            set { editCommandj = value; }
        }

        private ICommand editCommandRT;
        public ICommand EditCommandRT
        {
            get { return editCommandRT; }
            set { editCommandRT = value; }
        }

        private ICommand editCommandCG;
        public ICommand EditCommandCG
        {
            get { return editCommandCG; }
            set { editCommandCG = value; }
        }
        private ICommand editCommandWBT;
        public ICommand EditCommandWBT
        {
            get { return editCommandWBT; }
            set { editCommandWBT = value; }
        }
        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }

        private ICommand deleteCommand1;
        public ICommand DeleteCommand1
        {
            get { return deleteCommand1; }
            set { deleteCommand1 = value; }
        }
        private ICommand deleteCommand2;
        public ICommand DeleteCommand2
        {
            get { return deleteCommand2; }
            set { deleteCommand2 = value; }
        }

        private ICommand deleteCommandCG;
        public ICommand DeleteCommandCG
        {
            get { return deleteCommandCG; }
            set { deleteCommandCG = value; }
        }
        private ICommand deleteCommandWBKit;
        public ICommand DeleteCommandWBKit
        {
            get { return deleteCommandWBKit; }
            set { deleteCommandWBKit = value; }
        }


        private static ObservableCollection<JoiningShackleClass> loadMooringWinchList = new ObservableCollection<JoiningShackleClass>();

        public ObservableCollection<JoiningShackleClass> LoadMooringWinchRopeList
        {
            get
            {
                return loadMooringWinchList;
            }
            set
            {
                loadMooringWinchList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringWinchRopeList"));

            }
        }



        public static ObservableCollection<JoiningShackleClass> loadUserAccess = new ObservableCollection<JoiningShackleClass>();
        public ObservableCollection<JoiningShackleClass> LoadUserAccess
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


        public static ObservableCollection<RopeTailClass> loadUserAccess1 = new ObservableCollection<RopeTailClass>();
        public ObservableCollection<RopeTailClass> LoadUserAccess1
        {
            get
            {
                return loadUserAccess1;
            }
            set
            {
                loadUserAccess1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess1"));
            }
        }
        public static ObservableCollection<ChafeGuardClass> loadUserAccess11 = new ObservableCollection<ChafeGuardClass>();
        public ObservableCollection<ChafeGuardClass> LoadUserAccess11
        {
            get
            {
                return loadUserAccess11;
            }
            set
            {
                loadUserAccess11 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess11"));
            }
        }
        public static ObservableCollection<WBTestKitClass> loadUserAccess12 = new ObservableCollection<WBTestKitClass>();
        public ObservableCollection<WBTestKitClass> LoadUserAccess12
        {
            get
            {
                return loadUserAccess12;
            }
            set
            {
                loadUserAccess12 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess12"));
            }
        }
        public static ObservableCollection<RopeTailClass> loadUserAccess9 = new ObservableCollection<RopeTailClass>();
        public ObservableCollection<RopeTailClass> LoadUserAccess9
        {
            get
            {
                return loadUserAccess9;
            }
            set
            {
                loadUserAccess9 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess9"));
            }
        }

        public static ObservableCollection<RopeTailClass> loadUserAccess18 = new ObservableCollection<RopeTailClass>();
        public ObservableCollection<RopeTailClass> LoadUserAccess18
        {
            get
            {
                return loadUserAccess18;
            }
            set
            {
                loadUserAccess18 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess18"));
            }
        }
        public static ObservableCollection<RopeTailClass> loadUserAccess19 = new ObservableCollection<RopeTailClass>();
        public ObservableCollection<RopeTailClass> LoadUserAccess19
        {
            get
            {
                return loadUserAccess19;
            }
            set
            {
                loadUserAccess19 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess19"));
            }
        }
        public static ObservableCollection<RopeTailClass> loadUserAccess20 = new ObservableCollection<RopeTailClass>();
        public ObservableCollection<RopeTailClass> LoadUserAccess20
        {
            get
            {
                return loadUserAccess20;
            }
            set
            {
                loadUserAccess20 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess20"));
            }
        }
        public static ObservableCollection<RopeTailClass> loadUserAccess21 = new ObservableCollection<RopeTailClass>();
        public ObservableCollection<RopeTailClass> LoadUserAccess21
        {
            get
            {
                return loadUserAccess21;
            }
            set
            {
                loadUserAccess21 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess21"));
            }
        }
        public static ObservableCollection<RopeTailClass> loadUserAccess10 = new ObservableCollection<RopeTailClass>();
        public ObservableCollection<RopeTailClass> LoadUserAccess10
        {
            get
            {
                return loadUserAccess10;
            }
            set
            {
                loadUserAccess10 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess10"));
            }
        }

        public static ObservableCollection<ChainStopperClass> loadUserAccess2 = new ObservableCollection<ChainStopperClass>();
        public ObservableCollection<ChainStopperClass> LoadUserAccess2
        {
            get
            {
                return loadUserAccess2;
            }
            set
            {
                loadUserAccess2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess2"));
            }
        }

        private static string searchCrew;
        public string SearchCrew
        {
            get
            {
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    GetJShackleList();
                    GetRopeTailList();
                    GetMRopeList();
                    GetTowingRopeList();
                    GetSuezRopeList();
                    //PennantRopeList();
                    //GrommetRopeList();
                    GetFireWireList();
                    GetCStopperList();
                    GetChafeGuardList();
                    GetWBKitList();
                }
                else
                {
                    GetJShackleList();
                    GetRopeTailList();
                    GetMRopeList();
                    GetTowingRopeList();
                    GetSuezRopeList();
                    //PennantRopeList();
                    //GrommetRopeList();
                    GetFireWireList();
                    GetCStopperList();
                    GetChafeGuardList();
                    GetWBKitList();
                }
                return searchCrew;


            }

            set
            {
                searchCrew = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SearchCrew"));
                //GetMooringWinchRopeList();
            }
        }
        public void GetJShackleList()
        {
            //ObservableCollection<JoiningShackleClass> cropelist = new ObservableCollection<JoiningShackleClass>();
            try
            {

                LoadUserAccess.Clear();
                string SPName = "LooseEquipmentTypeDetails";
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    SPName = "LooseEquipmentTypeDetailsSearch";
                }
                else
                {
                    SPName = "LooseEquipmentTypeDetails";
                }
                SqlCommand cmd = new SqlCommand(SPName, sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "JShackle");

                if (!string.IsNullOrEmpty(searchCrew))
                {
                    cmd.Parameters.AddWithValue("@Searchtext", searchCrew);
                }

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess.Add(new JoiningShackleClass()
                    {
                        Id = (int)row["Id"],

                        IdentificationNumber = (row["UniqueID"] == DBNull.Value) ? "" : row["UniqueID"].ToString(),
                        CertificateNumber = (string)row["CertificateNumber"],
                        ManufactureName = (string)row["ManufactureName"],
                        // MBL = (decimal)row["MBL"],
                        MBL = Convert.ToDecimal((row["MBL"] == DBNull.Value) ? string.Empty : row["MBL"]),

                        Type = (string)row["Type"],
                        DateReceived = Convert.ToDateTime((row["DateReceived"] == DBNull.Value) ? null : row["DateReceived"]),
                        DateInstalled = Convert.ToDateTime((row["DateInstalled"] == DBNull.Value) ? null : row["DateInstalled"]),

                        DateReceived1 = (row["DateReceived1"] == DBNull.Value) ? null : row["DateReceived1"].ToString(),
                        DateInstalled1 = (row["DateInstalled1"] == DBNull.Value) ? null : row["DateInstalled1"].ToString(),


                        InspectionDueDate1 = (row["InspectionDueDate1"] == DBNull.Value) ? null : row["InspectionDueDate1"].ToString(),
                        Remarks = (row["Remarks"] == DBNull.Value) ? "" : row["Remarks"].ToString(),

                        UniqueID = (row["UniqueID"] == DBNull.Value) ? "" : row["UniqueID"].ToString(),
                        //OutofServiceDate = (DateTime)row["OutofServiceDate"],
                        //OutofServiceDate1 = ((DateTime)row["OutofServiceDate"]).ToString("d MMM, yyyy"),
                        // DamagedObserved = (row["DamagedObserved"] == DBNull.Value) ? string.Empty : row["DamagedObserved"].ToString(),
                        //DamagedObserved = (string)row["DamagedObserved"] ?? "",                        
                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                //return cropelist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                //return cropelist;
            }

        }

        public void GetRopeTailList()
        {
            //ObservableCollection<RopeTailClass> cropelist = new ObservableCollection<RopeTailClass>();
            try
            {

                LoadUserAccess1.Clear();
                string SPName = "LooseEquipmentTypeDetails";
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    SPName = "LooseEquipmentTypeDetailsSearch";
                }
                else
                {
                    SPName = "LooseEquipmentTypeDetails";
                }
                SqlCommand cmd = new SqlCommand(SPName, sc.con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "RTail");
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    cmd.Parameters.AddWithValue("@Searchtext", searchCrew);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess1.Add(new RopeTailClass()
                    {
                        Id = (int)row["Id"],
                        RopeConstruction = Convert.ToString((row["RopeConstruction"]) == DBNull.Value ? string.Empty : (row["RopeConstruction"])),
                        CertificateNumber = (string)row["CertificateNumber"],
                        ManufactureName = (string)row["ManufactureName"],
                        MBL = Convert.ToDecimal(row["MBL"] == DBNull.Value ? null : row["MBL"]),
                        Length = Convert.ToDecimal(row["Length"] == DBNull.Value ? null : row["Length"]),
                        LooseETypeId = (int)row["LooseETypeId"],
                        LDBF = Convert.ToDecimal(row["LDBF"] == DBNull.Value ? null : row["LDBF"]),
                        WLL = Convert.ToDecimal(row["WLL"] == DBNull.Value ? null : row["WLL"]),
                        MaxRunHours = Convert.ToInt32(row["MaxRunHours"] == DBNull.Value ? null : row["MaxRunHours"]),
                        MaxYearServiceMonth = Convert.ToInt32(row["MaxYearServiceMonth"] == DBNull.Value ? null : row["MaxYearServiceMonth"]),
                        DiaMeter = Convert.ToDecimal(row["DiaMeter"] == DBNull.Value ? null : row["DiaMeter"]),
                        //DiaMeter = (string)row["Diameter"],
                        RopeTagging = Convert.ToString((row["RopeTagging"]) == DBNull.Value ? string.Empty : (row["RopeTagging"])),

                        UniqueID = Convert.ToString((row["UniqueID"]) == DBNull.Value ? string.Empty : (row["UniqueID"])),


                        ReceivedDate = Convert.ToDateTime((row["ReceivedDate"] == DBNull.Value) ? null : row["ReceivedDate"]),
                        InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? null : row["InstalledDate"]),


                        ReceivedDate1 = (row["ReceivedDate1"] == DBNull.Value) ? null : row["ReceivedDate1"].ToString(),
                        InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),

                        InspectionDueDate1 = (row["InspectionDueDate1"] == DBNull.Value) ? null : row["InspectionDueDate1"].ToString(),
                        Remarks = (row["Remarks"] == DBNull.Value) ? "" : row["Remarks"].ToString(),
                        //InspectionDueDate1 = Convert.ToDateTime((row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"]).ToString("d MMM, yyyy"),

                        //ReceivedDate = (DateTime)row["ReceivedDate"],
                        //InstalledDate = Convert.ToDateTime( (DateTime)row["InstalledDate"]),
                        //DamageObserved = (row["DamageObserved"] == DBNull.Value) ? string.Empty : row["DamageObserved"].ToString(),
                        //DamageObserved = string.IsNullOrEmpty((string)row["DamageObserved"])),
                    });
                }

                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess1"));
                //return cropelist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                //return cropelist;
            }

        }
        public void GetMRopeList()
        {
            //ObservableCollection<RopeTailClass> cropelist = new ObservableCollection<RopeTailClass>();
            try
            {

                LoadUserAccess9.Clear();
                string SPName = "LooseEquipmentTypeDetails";
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    SPName = "LooseEquipmentTypeDetailsSearch";
                }
                else
                {
                    SPName = "LooseEquipmentTypeDetails";
                }
                SqlCommand cmd = new SqlCommand(SPName, sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "MRope");
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    cmd.Parameters.AddWithValue("@Searchtext", searchCrew);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess9.Add(new RopeTailClass()
                    {
                        Id = (int)row["Id"],
                        RopeConstruction = Convert.ToString((row["RopeConstruction"]) == DBNull.Value ? string.Empty : (row["RopeConstruction"])),
                        CertificateNumber = (string)row["CertificateNumber"],
                        ManufactureName = (string)row["ManufactureName"],
                        MBL = Convert.ToDecimal(row["MBL"] == DBNull.Value ? null : row["MBL"]),
                        Length = Convert.ToDecimal(row["Length"] == DBNull.Value ? null : row["Length"]),
                        LooseETypeId = (int)row["LooseETypeId"],
                        LDBF = Convert.ToDecimal(row["LDBF"] == DBNull.Value ? null : row["LDBF"]),
                        WLL = Convert.ToDecimal(row["WLL"] == DBNull.Value ? null : row["WLL"]),
                        MaxRunHours = Convert.ToInt32(row["MaxRunHours"] == DBNull.Value ? null : row["MaxRunHours"]),
                        MaxYearServiceMonth = Convert.ToInt32(row["MaxYearServiceMonth"] == DBNull.Value ? null : row["MaxYearServiceMonth"]),
                        DiaMeter = Convert.ToDecimal(row["DiaMeter"] == DBNull.Value ? null : row["DiaMeter"]),
                        // DiaMeter = (string)row["Diameter"],
                        RopeTagging = Convert.ToString((row["RopeTagging"]) == DBNull.Value ? string.Empty : (row["RopeTagging"])),

                        UniqueID = Convert.ToString((row["UniqueID"]) == DBNull.Value ? string.Empty : (row["UniqueID"])),


                        ReceivedDate = Convert.ToDateTime((row["ReceivedDate"] == DBNull.Value) ? null : row["ReceivedDate"]),
                        InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? null : row["InstalledDate"]),

                        ReceivedDate1 = (row["ReceivedDate1"] == DBNull.Value) ? null : row["ReceivedDate1"].ToString(),
                        InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),

                        InspectionDueDate1 = (row["InspectionDueDate1"] == DBNull.Value) ? null : row["InspectionDueDate1"].ToString(),
                        Remarks = (row["Remarks"] == DBNull.Value) ? "" : row["Remarks"].ToString(),
                        //ReceivedDate = (DateTime)row["ReceivedDate"],
                        //InstalledDate = Convert.ToDateTime( (DateTime)row["InstalledDate"]),
                        //DamageObserved = (row["DamageObserved"] == DBNull.Value) ? string.Empty : row["DamageObserved"].ToString(),
                        //DamageObserved = string.IsNullOrEmpty((string)row["DamageObserved"])),                 
                    });
                }

                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess9"));

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }

        }

        public void GetTowingRopeList()
        {
            //ObservableCollection<RopeTailClass> cropelist = new ObservableCollection<RopeTailClass>();
            try
            {

                LoadUserAccess18.Clear();
                string SPName = "LooseEquipmentTypeDetails";
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    SPName = "LooseEquipmentTypeDetailsSearch";
                }
                else
                {
                    SPName = "LooseEquipmentTypeDetails";
                }
                SqlCommand cmd = new SqlCommand(SPName, sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TowingRope");
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    cmd.Parameters.AddWithValue("@Searchtext", searchCrew);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess18.Add(new RopeTailClass()
                    {
                        Id = (int)row["Id"],
                        RopeConstruction = Convert.ToString((row["RopeConstruction"]) == DBNull.Value ? string.Empty : (row["RopeConstruction"])),
                        CertificateNumber = (string)row["CertificateNumber"],
                        ManufactureName = (string)row["ManufactureName"],
                        MBL = Convert.ToDecimal(row["MBL"] == DBNull.Value ? null : row["MBL"]),
                        Length = Convert.ToDecimal(row["Length"] == DBNull.Value ? null : row["Length"]),
                        LooseETypeId = (int)row["LooseETypeId"],
                        LDBF = Convert.ToDecimal(row["LDBF"] == DBNull.Value ? null : row["LDBF"]),
                        WLL = Convert.ToDecimal(row["WLL"] == DBNull.Value ? null : row["WLL"]),
                        MaxRunHours = Convert.ToInt32(row["MaxRunHours"] == DBNull.Value ? null : row["MaxRunHours"]),
                        MaxYearServiceMonth = Convert.ToInt32(row["MaxYearServiceMonth"] == DBNull.Value ? null : row["MaxYearServiceMonth"]),
                        DiaMeter = Convert.ToDecimal(row["DiaMeter"] == DBNull.Value ? null : row["DiaMeter"]),
                        // DiaMeter = (string)row["Diameter"],
                        RopeTagging = Convert.ToString((row["RopeTagging"]) == DBNull.Value ? string.Empty : (row["RopeTagging"])),

                        UniqueID = Convert.ToString((row["UniqueID"]) == DBNull.Value ? string.Empty : (row["UniqueID"])),


                        ReceivedDate = Convert.ToDateTime((row["ReceivedDate"] == DBNull.Value) ? null : row["ReceivedDate"]),
                        InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? null : row["InstalledDate"]),

                        ReceivedDate1 = (row["ReceivedDate1"] == DBNull.Value) ? null : row["ReceivedDate1"].ToString(),
                        InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),

                        InspectionDueDate1 = (row["InspectionDueDate1"] == DBNull.Value) ? null : row["InspectionDueDate1"].ToString(),
                        Remarks = (row["Remarks"] == DBNull.Value) ? "" : row["Remarks"].ToString(),
                        //ReceivedDate = (DateTime)row["ReceivedDate"],
                        //InstalledDate = Convert.ToDateTime( (DateTime)row["InstalledDate"]),
                        //DamageObserved = (row["DamageObserved"] == DBNull.Value) ? string.Empty : row["DamageObserved"].ToString(),
                        //DamageObserved = string.IsNullOrEmpty((string)row["DamageObserved"])),                 
                    });
                }

                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess18"));

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }

        }
        public void GetSuezRopeList()
        {
            //ObservableCollection<RopeTailClass> cropelist = new ObservableCollection<RopeTailClass>();
            try
            {

                LoadUserAccess19.Clear();
                string SPName = "LooseEquipmentTypeDetails";
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    SPName = "LooseEquipmentTypeDetailsSearch";
                }
                else
                {
                    SPName = "LooseEquipmentTypeDetails";
                }
                SqlCommand cmd = new SqlCommand(SPName, sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SuezRope");
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    cmd.Parameters.AddWithValue("@Searchtext", searchCrew);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess19.Add(new RopeTailClass()
                    {
                        Id = (int)row["Id"],
                        RopeConstruction = Convert.ToString((row["RopeConstruction"]) == DBNull.Value ? string.Empty : (row["RopeConstruction"])),
                        CertificateNumber = (string)row["CertificateNumber"],
                        ManufactureName = (string)row["ManufactureName"],
                        MBL = Convert.ToDecimal(row["MBL"] == DBNull.Value ? null : row["MBL"]),
                        Length = Convert.ToDecimal(row["Length"] == DBNull.Value ? null : row["Length"]),
                        LooseETypeId = (int)row["LooseETypeId"],
                        LDBF = Convert.ToDecimal(row["LDBF"] == DBNull.Value ? null : row["LDBF"]),
                        WLL = Convert.ToDecimal(row["WLL"] == DBNull.Value ? null : row["WLL"]),
                        MaxRunHours = Convert.ToInt32(row["MaxRunHours"] == DBNull.Value ? null : row["MaxRunHours"]),
                        MaxYearServiceMonth = Convert.ToInt32(row["MaxYearServiceMonth"] == DBNull.Value ? null : row["MaxYearServiceMonth"]),
                        DiaMeter = Convert.ToDecimal(row["DiaMeter"] == DBNull.Value ? null : row["DiaMeter"]),
                        // DiaMeter = (string)row["Diameter"],
                        RopeTagging = Convert.ToString((row["RopeTagging"]) == DBNull.Value ? string.Empty : (row["RopeTagging"])),

                        UniqueID = Convert.ToString((row["UniqueID"]) == DBNull.Value ? string.Empty : (row["UniqueID"])),


                        ReceivedDate = Convert.ToDateTime((row["ReceivedDate"] == DBNull.Value) ? null : row["ReceivedDate"]),
                        InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? null : row["InstalledDate"]),

                        ReceivedDate1 = (row["ReceivedDate1"] == DBNull.Value) ? null : row["ReceivedDate1"].ToString(),
                        InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),

                        InspectionDueDate1 = (row["InspectionDueDate1"] == DBNull.Value) ? null : row["InspectionDueDate1"].ToString(),
                        Remarks = (row["Remarks"] == DBNull.Value) ? "" : row["Remarks"].ToString(),
                        //ReceivedDate = (DateTime)row["ReceivedDate"],
                        //InstalledDate = Convert.ToDateTime( (DateTime)row["InstalledDate"]),
                        //DamageObserved = (row["DamageObserved"] == DBNull.Value) ? string.Empty : row["DamageObserved"].ToString(),
                        //DamageObserved = string.IsNullOrEmpty((string)row["DamageObserved"])),                 
                    });
                }

                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess19"));

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }

        }




        public void GetFireWireList()
        {
            //ObservableCollection<RopeTailClass> cropelist = new ObservableCollection<RopeTailClass>();
            try
            {

                LoadUserAccess10.Clear();
                string SPName = "LooseEquipmentTypeDetails";
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    SPName = "LooseEquipmentTypeDetailsSearch";
                }
                else
                {
                    SPName = "LooseEquipmentTypeDetails";
                }
                SqlCommand cmd = new SqlCommand(SPName, sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "FireWire");
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    cmd.Parameters.AddWithValue("@Searchtext", searchCrew);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess10.Add(new RopeTailClass()
                    {
                        Id = (int)row["Id"],
                        RopeConstruction = Convert.ToString((row["RopeConstruction"]) == DBNull.Value ? string.Empty : (row["RopeConstruction"])),
                        CertificateNumber = (string)row["CertificateNumber"],
                        ManufactureName = (string)row["ManufactureName"],
                        MBL = Convert.ToDecimal(row["MBL"] == DBNull.Value ? null : row["MBL"]),
                        Length = Convert.ToDecimal(row["Length"] == DBNull.Value ? null : row["Length"]),
                        LooseETypeId = (int)row["LooseETypeId"],
                        LDBF = Convert.ToDecimal(row["LDBF"] == DBNull.Value ? null : row["LDBF"]),
                        WLL = Convert.ToDecimal(row["WLL"] == DBNull.Value ? null : row["WLL"]),
                        MaxRunHours = Convert.ToInt32(row["MaxRunHours"] == DBNull.Value ? null : row["MaxRunHours"]),
                        MaxYearServiceMonth = Convert.ToInt32(row["MaxYearServiceMonth"] == DBNull.Value ? null : row["MaxYearServiceMonth"]),
                        //DiaMeter = (string)row["Diameter"],
                        DiaMeter = Convert.ToDecimal(row["DiaMeter"] == DBNull.Value ? null : row["DiaMeter"]),
                        RopeTagging = Convert.ToString((row["RopeTagging"]) == DBNull.Value ? string.Empty : (row["RopeTagging"])),

                        ReceivedDate = Convert.ToDateTime((row["ReceivedDate"] == DBNull.Value) ? null : row["ReceivedDate"]),
                        InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? null : row["InstalledDate"]),

                        ReceivedDate1 = (row["ReceivedDate1"] == DBNull.Value) ? null : row["ReceivedDate1"].ToString(),
                        InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),

                        UniqueID = Convert.ToString((row["UniqueID"]) == DBNull.Value ? string.Empty : (row["UniqueID"])),


                        //InspectionDueDate1 = Convert.ToDateTime((row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"]).ToString("d MMM, yyyy"),
                        InspectionDueDate1 = (row["InspectionDueDate1"] == DBNull.Value) ? null : row["InspectionDueDate1"].ToString(),
                        Remarks = (row["Remarks"] == DBNull.Value) ? "" : row["Remarks"].ToString(),
                        //ReceivedDate = (DateTime)row["ReceivedDate"],
                        //InstalledDate = Convert.ToDateTime( (DateTime)row["InstalledDate"]),
                        //DamageObserved = (row["DamageObserved"] == DBNull.Value) ? string.Empty : row["DamageObserved"].ToString(),
                        //DamageObserved = string.IsNullOrEmpty((string)row["DamageObserved"])),                   
                    });
                }

                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess10"));
                //return cropelist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                //return cropelist;
            }

        }

        public void GetCStopperList()
        {
            //ObservableCollection<ChainStopperClass> cropelist = new ObservableCollection<ChainStopperClass>();
            try
            {

                LoadUserAccess2.Clear();
                string SPName = "LooseEquipmentTypeDetails";
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    SPName = "LooseEquipmentTypeDetailsSearch";
                }
                else
                {
                    SPName = "LooseEquipmentTypeDetails";
                }
                SqlCommand cmd = new SqlCommand(SPName, sc.con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "CStopper");
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    cmd.Parameters.AddWithValue("@Searchtext", searchCrew);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess2.Add(new ChainStopperClass()
                    {
                        Id = (int)row["Id"],
                        //IdentificationNumber = (string)row["IdentificationNumber"],
                        CertificateNumber = (string)row["CertificateNumber"],
                        ManufactureName = (string)row["ManufactureName"],
                        MBL = (decimal)row["MBL"],
                        Length = (decimal)row["Length"],
                        InspectionDueDate1 = (row["InspectionDueDate1"] == DBNull.Value) ? null : row["InspectionDueDate1"].ToString(),
                        Remarks = (row["Remarks"] == DBNull.Value) ? "" : row["Remarks"].ToString(),

                        UniqueID = Convert.ToString((row["UniqueID"]) == DBNull.Value ? string.Empty : (row["UniqueID"])),

                        ReceivedDate1 = (row["ReceivedDate1"] == DBNull.Value) ? null : row["ReceivedDate1"].ToString(),
                        InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),

                        DateInstalled = Convert.ToDateTime((row["DateInstalled"] == DBNull.Value) ? null : row["DateInstalled"]),
                        //OutofServiceDate = (DateTime)row["OutofServiceDate"],
                        //OutofServiceDate1 = ((DateTime)row["OutofServiceDate"]).ToString("d MMM, yyyy"),
                        //DamagedObserved = (row["DamagedObserved"] == DBNull.Value) ? string.Empty : row["DamagedObserved"].ToString(),
                        //DamagedObserved = (string)row["DamagedObserved"],
                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess1"));
                //return cropelist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                //return cropelist;
            }

        }

        public void GetChafeGuardList()
        {
            //ObservableCollection<ChainStopperClass> cropelist = new ObservableCollection<ChainStopperClass>();
            try
            {

                LoadUserAccess11.Clear();
                string SPName = "LooseEquipmentTypeDetails";
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    SPName = "LooseEquipmentTypeDetailsSearch";
                }
                else
                {
                    SPName = "LooseEquipmentTypeDetails";
                }
                SqlCommand cmd = new SqlCommand(SPName, sc.con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ChafeGuard");
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    cmd.Parameters.AddWithValue("@Searchtext", searchCrew);
                }

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess11.Add(new ChafeGuardClass()
                    {
                        Id = (int)row["Id"],
                        CertificateNumber = (row["CertificateNumber"] == DBNull.Value) ? string.Empty : row["CertificateNumber"].ToString(),
                        ManufacturerName = (string)row["ManufacturerName"],
                        UniqueID = Convert.ToString((row["UniqueID"]) == DBNull.Value ? string.Empty : (row["UniqueID"])),

                        //InstalledDate1 = (row["InstalledDate"] == DBNull.Value) ? null : row["InstalledDate"].ToString(),
                        InspectionDueDate1 = (row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"].ToString(),

                        ReceivedDate = Convert.ToDateTime((row["ReceivedDate"] == DBNull.Value) ? null : row["ReceivedDate"]),
                        InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? null : row["InstalledDate"]),
                        ReceivedDate1 = (row["ReceivedDate1"] == DBNull.Value) ? null : row["ReceivedDate1"].ToString(),
                        InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),

                        Remarks = (row["Remarks"] == DBNull.Value) ? "" : row["Remarks"].ToString(),
                        //Remarks = (string)row["Remarks"],

                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess11"));
                //return cropelist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                //return cropelist;
            }

        }

        public void GetWBKitList()
        {
            //ObservableCollection<ChainStopperClass> cropelist = new ObservableCollection<ChainStopperClass>();
            try
            {

                LoadUserAccess12.Clear();
                string SPName = "LooseEquipmentTypeDetails";
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    SPName = "LooseEquipmentTypeDetailsSearch";
                }
                else
                {
                    SPName = "LooseEquipmentTypeDetails";
                }
                SqlCommand cmd = new SqlCommand(SPName, sc.con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "WinchBreakTestKit");
                if (!string.IsNullOrEmpty(searchCrew))
                {
                    cmd.Parameters.AddWithValue("@Searchtext", searchCrew);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                foreach (DataRow row in ds.Rows)
                {
                    LoadUserAccess12.Add(new WBTestKitClass()
                    {
                        Id = (int)row["Id"],
                        CertificateNumber = (row["CertificateNumber"] == DBNull.Value) ? string.Empty : row["CertificateNumber"].ToString(),
                        ManufacturerName = (string)row["ManufacturerName"],
                        //InstalledDate1 = (row["InstalledDate"] == DBNull.Value) ? null : row["InstalledDate"].ToString(),
                        InspectionDueDate1 = (row["InspectionDueDate"] == DBNull.Value) ? null : row["InspectionDueDate"].ToString(),
                        UniqueID = Convert.ToString((row["UniqueID"]) == DBNull.Value ? string.Empty : (row["UniqueID"])),

                        ReceivedDate = Convert.ToDateTime((row["ReceivedDate"] == DBNull.Value) ? null : row["ReceivedDate"]),
                        InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? null : row["InstalledDate"]),
                        ReceivedDate1 = (row["ReceivedDate1"] == DBNull.Value) ? null : row["ReceivedDate1"].ToString(),
                        InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),

                        Remarks = (row["Remarks"] == DBNull.Value) ? "" : row["Remarks"].ToString(),

                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess12"));
                //return cropelist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                //return cropelist;
            }

        }

        private void EditJoiningShackle(JoiningShackleClass mw)
        {
            try
            {
                AddLooseEquipmentViewModel vm = new AddLooseEquipmentViewModel(mw);
                ChildWindowManager.Instance.ShowChildWindow(new AddLooseEquipmentDetailsView() { DataContext = vm });
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void EditCG(ChafeGuardClass mw)
        {
            try
            {
                AddChafeGuardViewModel vm = new AddChafeGuardViewModel(mw);
                ChildWindowManager.Instance.ShowChildWindow(new AddChafeGuard() { DataContext = vm });

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void EditWBT(WBTestKitClass mw)
        {
            try
            {
                WBTestKitViewModel vm = new WBTestKitViewModel(mw);
                ChildWindowManager.Instance.ShowChildWindow(new AddWBTestKitView() { DataContext = vm });

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void EditRopeTail(RopeTailClass mw)
        {
            try
            {
                //if (mw.LooseETypeId == 4)
                //{
                AddRopeTailViewModel vm = new AddRopeTailViewModel(mw);
                ChildWindowManager.Instance.ShowChildWindow(new AddRopeTailView() { DataContext = vm });
                //}
                //if (mw.LooseETypeId == 3)
                //{
                //     vm = new AddRopeTailViewModel(mw);
                //    ChildWindowManager.Instance.ShowChildWindow(new AddRopeTailView() { DataContext = vm });
                //}
                //if (mw.LooseETypeId == 6)
                //{
                //    AddRopeTailViewModel vm = new AddRopeTailViewModel(mw);
                //    ChildWindowManager.Instance.ShowChildWindow(new AddRopeTailView() { DataContext = vm });
                //}
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void DeleteJShcackle(JoiningShackleClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    JoiningShackleClass findrank = sc.JoiningShackles.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findrank != null)
                    {

                        //SqlDataAdapter adp = new SqlDataAdapter("update JoiningShackle set deletestatus=1 where ID=" + mw.Id + "", sc.con);
                        SqlDataAdapter adp = new SqlDataAdapter("deleteJoiningShackle", sc.con);
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);

                        //sc.Entry(findrank).State = EntityState.Deleted;
                        //sc.SaveChanges();

                        MessageBox.Show("Record deleted successfully ", "Delete Department", MessageBoxButton.OK, MessageBoxImage.Information);

                        //.....Refresh DataGrid........


                        GetJShackleList();



                        //sc.ObservableCollectionList(LoadMooringWinchList, GetMooringWinchList);

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

        private void DeleteRopeTail(RopeTailClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    RopeTailClass findrank = sc.RopeTails.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findrank != null)
                    {

                        //SqlDataAdapter adp = new SqlDataAdapter("update RopeTail set deletestatus=1 where ID=" + mw.Id + "", sc.con);
                        SqlDataAdapter adp = new SqlDataAdapter("deleteRopeTail", sc.con);
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);

                        //sc.Entry(findrank).State = EntityState.Deleted;
                        //sc.SaveChanges();

                        MessageBox.Show("Record deleted successfully ", "Delete Department", MessageBoxButton.OK, MessageBoxImage.Information);

                        //.....Refresh DataGrid........


                        if (mw.LooseETypeId == 4)
                        {
                            GetRopeTailList();
                        }

                        if (mw.LooseETypeId == 3)
                        {
                            GetMRopeList();
                        }

                        if (mw.LooseETypeId == 6)
                        {
                            GetFireWireList();
                        }



                        //sc.ObservableCollectionList(LoadMooringWinchList, GetMooringWinchList);

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
        private void DeleteCStopper(ChainStopperClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ChainStopperClass findrank = sc.ChainStoppers.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findrank != null)
                    {

                        //var checkrope = sc.MooringWinchRope.Where(x => x.Id.Equals(mw.Id)).FirstOrDefault();
                        //if (checkrope != null)
                        //{
                        //    MessageBox.Show("Sorry you can't delete this Department, as it is assigned to some crew members.", "Delete  Department", MessageBoxButton.OK, MessageBoxImage.Stop);
                        //}
                        //else
                        //{

                        sc.Entry(findrank).State = EntityState.Deleted;
                        sc.SaveChanges();

                        MessageBox.Show("Record deleted successfully ", "Delete Department", MessageBoxButton.OK, MessageBoxImage.Information);

                        //.....Refresh DataGrid........


                        GetCStopperList();



                        //sc.ObservableCollectionList(LoadMooringWinchList, GetMooringWinchList);

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


        private void DeleteCG(ChafeGuardClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ChafeGuardClass findrank = sc.ChafeGuard.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findrank != null)
                    {
                        //SqlDataAdapter adp = new SqlDataAdapter("update ChafeGuard set deletestatus=1 where ID=" + mw.Id + "", sc.con);
                        SqlDataAdapter adp = new SqlDataAdapter("deleteChafeGuard", sc.con);
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);

                        //sc.Entry(findrank).State = EntityState.Deleted;
                        //sc.SaveChanges();
                        MessageBox.Show("Record deleted successfully ", "Delete Department", MessageBoxButton.OK, MessageBoxImage.Information);

                        GetChafeGuardList();

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

        private void DeleteWBTestKit(WBTestKitClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    WBTestKitClass findrank = sc.WBTestKit.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findrank != null)
                    {

                        //SqlDataAdapter adp = new SqlDataAdapter("update WinchBreakTestKit set deletestatus=1 where ID=" + mw.Id + "", sc.con);
                        SqlDataAdapter adp = new SqlDataAdapter("deleteWinchBreakTestKit", sc.con);
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);

                        //sc.Entry(findrank).State = EntityState.Deleted;
                        //sc.SaveChanges();
                        MessageBox.Show("Record deleted successfully ", "Delete Department", MessageBoxButton.OK, MessageBoxImage.Information);

                        GetWBKitList();

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
