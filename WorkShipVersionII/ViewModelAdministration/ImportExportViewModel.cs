using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
//using Microsoft.Office.Interop.Excel;
//using ExcelLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
//using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using System.Configuration;
using ClosedXML.Excel;
using System.Data.Entity;
//using Microsoft.Office.Interop.Excel;

//using ExcelLibrary.SpreadSheet;

namespace WorkShipVersionII.ViewModelAdministration
{

    public class ImportExportViewModel : ViewModelBase
    {
        private readonly AdministrationContaxt sc;

        private BackgroundWorker _Export;
        private BackgroundWorker _Import;
        ObservableCollection<CommentofVSClass> Employees;
        private SqlConnection con = ConnectionBulder.con;
        public ICommand HelpCommand { get; private set; }
        public ImportExportViewModel()
        {
            if (sc == null)
            {
                sc = new AdministrationContaxt();
                _Export = new BackgroundWorker();
                _Import = new BackgroundWorker();
            }

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            //_BrowseCommand = new RelayCommand(BrowseMethod);

            _BrowseCommand2 = new RelayCommand(ImportAttachment);
            _BrowseCommand3 = new RelayCommand(ExportAttachment);
            //_ImportCommand = new RelayCommand(ImportMethod);

            //_ImportCommand = new RelayCommand(() => _Import.RunWorkerAsync(), () => !_Import.IsBusy);
            //_Import = new BackgroundWorker();
            //_Import.WorkerReportsProgress = true;
            //_Import.DoWork += ImportMethod;
            //_Import.ProgressChanged += new ProgressChangedEventHandler(ImportProgressChanged);
            //_Import.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ImportCompleted);


            this._ImportCommand = new RelayCommand(() => _Import.RunWorkerAsync(), () => !_Import.IsBusy);
            this._Import.DoWork += new DoWorkEventHandler(BrowseMethod);
            this._Import.WorkerReportsProgress = true;
            _Import.WorkerSupportsCancellation = true;
            this._Import.ProgressChanged += new ProgressChangedEventHandler(ImportProgressChanged);
            this._Import.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ImportCompleted);


            this._ExportCommands = new RelayCommand(() => _Export.RunWorkerAsync(), () => !_Export.IsBusy);

            this._Export.DoWork += new DoWorkEventHandler(ExportMethod);
            this._Export.WorkerReportsProgress = true;
            _Export.WorkerSupportsCancellation = true;
            this._Export.ProgressChanged += new ProgressChangedEventHandler(ExportProgressChanged);
            this._Export.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ExportCompleted);




            //_ExportCommand = new RelayCommand(() => _Export.RunWorkerAsync(), () => !_Export.IsBusy);
            //_Export = new BackgroundWorker();
            //_Export.WorkerReportsProgress = true;
            //_Export.DoWork += ExportMethod;
            //_Export.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            //_Export.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ExportCompleted);


        }



        private void ExportCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


            PVisible = "Collapsed";
            sc.CreateLog("Import / Export", "Export Data", null);
        }
        private void ExportProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _ExportProgress = e.ProgressPercentage;
            RaisePropertyChanged("ExportProgress");
        }

        private void ImportCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


            IVisibles = "Collapsed";
            ImportVisibles = false;
            Employees = new ObservableCollection<CommentofVSClass>();
         
            sc.CreateLog("Import / Export", "Import Data", null);

        }
        private void ImportProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _ImportProgress = e.ProgressPercentage;
            RaisePropertyChanged("ImportProgress");
        }



        private ICommand _ExportCommands;
        public ICommand ExportCommands
        {
            get
            {

                return _ExportCommands;
            }
        }

        private ICommand _ImportCommand;
        public ICommand ImportCommand
        {
            get
            {

                return _ImportCommand;
            }
        }


        private ICommand _BrowseCommand;
        public ICommand BrowseCommand
        {
            get
            {
                return _BrowseCommand;
            }
        }
        private ICommand _BrowseCommand2;
        public ICommand BrowseCommand2
        {
            get
            {
                return _BrowseCommand2;
            }
        }

        private ICommand _BrowseCommand3;
        public ICommand BrowseCommand3
        {
            get
            {
                return _BrowseCommand3;
            }
        }
        private double _ExportProgress;
        public double ExportProgress
        {
            get
            {

                return _ExportProgress;
            }
            set
            {
                _ExportProgress = value;
                RaisePropertyChanged("ExportProgress");
            }
        }

        private double _ImportProgress;
        public double ImportProgress
        {
            get
            {

                return _ImportProgress;
            }
            set
            {
                _ImportProgress = value;
                RaisePropertyChanged("ImportProgress");
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

        private string iVisibles;
        public string IVisibles
        {
            get
            {
                if (iVisibles == null)
                {
                    iVisibles = "Collapsed";
                }
                return iVisibles;
            }
            set
            {
                iVisibles = value;
                RaisePropertyChanged("IVisibles");
            }
        }

        private bool importVisibles = false;
        public bool ImportVisibles
        {
            get
            {
                if (!importVisibles)
                {
                    importVisibles = false;
                }
                return importVisibles;
            }
            set
            {
                importVisibles = value;
                RaisePropertyChanged("ImportVisibles");
            }
        }

        private void ExportMethod(object sender, DoWorkEventArgs eb)
        {
            try
            {
                ExportMethod1();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void InsertPortlist(string sheetName, System.Data.DataTable tbls)
        {
            try
            {
                System.Data.DataTable dtt = tbls;

                if (sheetName == "PortList")
                {

                    PortListClass obj;

                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            obj = new PortListClass();
                            string qry = "select * from PortList where PortName ='" + dtt.Rows[j]["PortName"].ToString() + "' and FacilityName='" + dtt.Rows[j]["FacilityName"].ToString() + "'";
                            SqlDataAdapter asda = new SqlDataAdapter(qry, con);
                            asda.SelectCommand.CommandType = CommandType.Text;
                            System.Data.DataTable dtps = new System.Data.DataTable();
                            asda.Fill(dtps);
                            if (dtps.Rows.Count == 0)
                            {
                                obj.CountryCode = dtt.Rows[j]["CountryCode"].ToString();
                                obj.CountryName = dtt.Rows[j]["CountryName"].ToString();
                                obj.PortName = dtt.Rows[j]["PortName"].ToString();
                                obj.FacilityName = dtt.Rows[j]["FacilityName"].ToString();
                                obj.IMOPortFacilityNumber = dtt.Rows[j]["IMOPortFacilityNumber"].ToString();
                                obj.Longitude = dtt.Rows[j]["Longitude"].ToString();
                                obj.Latitude = dtt.Rows[j]["Latitude"].ToString();

                                sc.PortListtbl.Add(obj);
                                sc.SaveChanges();

                                sc.Entry(obj).State = EntityState.Detached;
                            }
                        }


                        MessageBox.Show("PortList is has been imported.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ExportMethod1()
        {

            try
            {



                SaveFileDialog sfd = new SaveFileDialog();
                //sfd.FileName = "Work-Ship_Export_" + DateTime.Now.ToString("dd-MMM-yyyy") + "_IMO_" + Class1.IMONum;
                sfd.FileName = "DigiMoor_Export_" + DateTime.Now.ToString("dd-MMM-yyyy");
                sfd.DefaultExt = "xlsx";

                //"Text files (*.txt)|*.txt|All files (*.*)|*.*";
                //SaveFileDialog objSFD = new SaveFileDialog() { DefaultExt = "xls", Filter = "Excel Files | *.xls; *.xlsx; *.xlsm", FilterIndex = 1 };                                             

                sfd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                if (sfd.ShowDialog() == true)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        File.Delete(sfd.FileName);
                    }

                    //...........
                    int ib = 1;
                    _Export.ReportProgress(ib);
                    PVisible = "Visible";
                    //...............


                    DataSet dataSet = null;
                    dataSet = new DataSet("General");
                    dataSet.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

                    _Export.ReportProgress(ib + 1);



                    //if (con.State == ConnectionState.Closed)
                    //    con.Open();

                    // string dbConnectionString = 
                    // string dbConnectionString = 
                    //OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt2"].ConnectionString);
                    //con.Open();

                    //string qryCW = @"select v.vessel_ID,a.id as Crew_ID,a.name as FullName,a.UserName,a.Position,a.rid,a.ServiceFrom,a.ServiceTo,a.CDC as CDCNumber,a.empno as Emp_Number,a.Comments,a.department as Department,a.SeaWH,a.PortWH,a.SeaWk1, a.SeaNWK1,a.PortWk1,a.PortNWK1,a.Remarks,case when a.SeaWk1 = '' then 'No' else 'Yes' end as Watchkeeper ,a.overtime as Overtime, ot.NrmlWHrs,ot.SatWHrs,ot.SunWhrs,ot.HolidayWHrs,ot.FixedOverTime,ot.HourlyRate,ot.Currency,ot.holidays from CrewDetail a cross join Vessel v left outer join overtime ot on a.UserName = ot.UserName where a.UserName in (select distinct UserName from WorkHours where dates between '" + Convert.ToDateTime(MyDateTimeFrom).ToShortDateString() + "' and '" + Convert.ToDateTime(MyDateTimeTo).ToShortDateString() + "' ) ";

                    //string qryCW = "Select * from AssignRopeToWinch";

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    //string qryCW = "SELECT  * FROM    AssignRopeToWinch WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat( Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    string qryCW = "Select * from AssignRopeToWinch  WHERE  ( CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' ) or ( AssignedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND AssignedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )";


                    System.Data.DataTable dtbl1 = new System.Data.DataTable("AssignRopeToWinch");

                    SqlDataAdapter dap1 = new SqlDataAdapter(qryCW, con);
                   
                    dap1.Fill(dtbl1);
                    dataSet.Tables.Add(dtbl1);
                    dtbl1.Dispose();
                    dap1.Dispose();

                    //dtbl1.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    //OleDbCommand cmd1 = new OleDbCommand(qryCW, con);
                    //OleDbDataAdapter adptr = new OleDbDataAdapter();
                    //adptr.SelectCommand = cmd1;
                    //adptr.Fill(dtbl1);
                    //if (dtbl1.Rows.Count > 0)
                    //{

                    //    dataSet.Tables.Add(dtbl1);
                    //}
                    //cmd1.Dispose();
                    //adptr.Dispose();
                    //dtbl1.Dispose();
                    //con.Close();





                    _Export.ReportProgress(ib + 2);



                    //string whqry = @"select d.vessel_ID as Vessel_ID, C.* from 
                    //            (select a.wid as ID ,b.name as FullName,a.FullName as UserName,a.Position,a.TotalHours,a.RestHours,a.options as Options ,a.Remarks,a.dates as Dates , a.hrs,a.NonConfirmities,a.Department,a.DaysOfMonth,a.NormalOT1 as Overtime,a.opa as Opa,a.RuleNames,a.RestHour7day,a.NormalOT1 as NormalWH, a.RestHourAny24,a.RestHourAny7day, b.chkyoungs   from WorkHours a join  CrewDetail b on  a.UserName=b.UserName) c  cross join Vessel d where C.Dates between '" + Convert.ToDateTime(MyDateTimeFrom).ToShortDateString() + "' and '" + Convert.ToDateTime(MyDateTimeTo).ToShortDateString() + "'";

                    //string whqry = "Select * from ChainStopper";
                    string whqry = "Select * from ChainStopper";
                    SqlCommand cmd2 = new SqlCommand(whqry, con);
                    SqlDataAdapter dap2 = new SqlDataAdapter();
                    System.Data.DataTable dtbl2 = new System.Data.DataTable("ChainStopper");
                    dtbl2.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap2.SelectCommand = cmd2;
                    dap2.Fill(dtbl2);

                    if (dtbl2.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl2);
                    }
                    cmd2.Dispose();
                    dap2.Dispose();
                    dtbl2.Dispose();
                    //con.Close();

                    _Export.ReportProgress(ib + 3);

                    // string qury = "select	b.Vessel_ID,a.Id as CId,a.CName,a.DOI,a.DOE,a.DOS,a.Remarks from certificates a cross join Vessel b";

                    //string qury = "Select * from DamageObserved";
                    //SqlDataAdapter dap3 = new SqlDataAdapter(qury, con);
                    //System.Data.DataTable dtbl3 = new System.Data.DataTable("DamageObserved");
                    //dap3.Fill(dtbl3);
                    //dataSet.Tables.Add(dtbl3);
                    //dtbl3.Dispose();
                    //dap3.Dispose();

                    string qury = "Select * from DamageObserved";
                    SqlCommand cmd3 = new SqlCommand(qury, con);
                    SqlDataAdapter dap3 = new SqlDataAdapter();
                    System.Data.DataTable dtbl3 = new System.Data.DataTable("DamageObserved");
                    dtbl3.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap3.SelectCommand = cmd3;
                    dap3.Fill(dtbl3);

                    if (dtbl3.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl3);
                    }
                    cmd3.Dispose();
                    dap3.Dispose();
                    dtbl3.Dispose();

                    _Export.ReportProgress(ib + 4);


                    //string qury2 = "Select * from JoiningShackle";
                    string qury2 = "Select * from JoiningShackle";
                    SqlCommand cmd4 = new SqlCommand(qury2, con);
                    SqlDataAdapter dap4 = new SqlDataAdapter();
                    System.Data.DataTable dtbl4 = new System.Data.DataTable("JoiningShackle");
                    dtbl4.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap4.SelectCommand = cmd4;
                    dap4.Fill(dtbl4);

                    if (dtbl4.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl4);
                    }
                    cmd4.Dispose();
                    dap4.Dispose();
                    dtbl4.Dispose();



                    _Export.ReportProgress(ib + 5);


                    //string qury21 = "Select * from LooseEDamageRecord";
                    //string qury21 = "Select * from LooseEDamageRecord  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    string qury21 = "Select * from LooseEDamageRecord  WHERE  ( CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' ) or ( DamageDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND DamageDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )";


                    SqlCommand cmd5 = new SqlCommand(qury21, con);
                    SqlDataAdapter dap5 = new SqlDataAdapter();
                    System.Data.DataTable dtbl5 = new System.Data.DataTable("LooseEDamageRecord");
                    dtbl5.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap5.SelectCommand = cmd5;
                    dap5.Fill(dtbl5);
                    if (dtbl5.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl5);
                    }
                    cmd5.Dispose();
                    dap5.Dispose();
                    dtbl5.Dispose();

                    _Export.ReportProgress(ib + 6);


                    //string quryA = "Select * from LooseEDisposal";
                   // string quryA = "Select * from LooseEDisposal  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    string quryA = "Select * from LooseEDisposal  WHERE  ( CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' ) or ( DisposalDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND DisposalDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )";


                    SqlCommand cmd6 = new SqlCommand(quryA, con);
                    SqlDataAdapter dap6 = new SqlDataAdapter();
                    System.Data.DataTable dtbl6 = new System.Data.DataTable("LooseEDisposal");
                    dtbl6.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap6.SelectCommand = cmd6;
                    dap6.Fill(dtbl6);

                    if (dtbl6.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl6);
                    }
                    cmd6.Dispose();
                    dap6.Dispose();
                    dtbl6.Dispose();



                    _Export.ReportProgress(ib + 7);

                    //string quryAA = "Select * from LooseEType";
                    string quryAA = "Select * from LooseEType";
                    SqlCommand cmd7 = new SqlCommand(quryAA, con);
                    SqlDataAdapter dap7 = new SqlDataAdapter();
                    System.Data.DataTable dtbl7 = new System.Data.DataTable("LooseEType");
                    dtbl7.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap7.SelectCommand = cmd7;
                    dap7.Fill(dtbl7);
                    if (dtbl7.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl7);
                    }
                    cmd7.Dispose();
                    dap7.Dispose();
                    dtbl7.Dispose();

                    _Export.ReportProgress(ib + 8);


                    //string quryAAA = "Select * from MooringLooseEquipInspection";
                    //string quryAAA = "Select * from MooringLooseEquipInspection  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    string quryAAA = "Select * from MooringLooseEquipInspection  WHERE  ( CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' ) or ( InspectDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND InspectDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )";


                    SqlCommand cmd8 = new SqlCommand(quryAAA, con);
                    SqlDataAdapter dap8 = new SqlDataAdapter();
                    System.Data.DataTable dtbl8 = new System.Data.DataTable("MooringLooseEquipInspection");
                    dtbl8.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap8.SelectCommand = cmd8;
                    dap8.Fill(dtbl8);
                    if (dtbl8.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl8);
                    }
                    cmd8.Dispose();
                    dap8.Dispose();

                    //for (int i = 0; i < dtbl8.Rows.Count; i++)
                    //{

                    //    dtbl8.Rows[i]["Photo1"] = null;
                    //    dtbl8.Rows[i]["Photo2"] = null;

                    //}
                    dtbl8.Dispose();

                    _Export.ReportProgress(ib + 9);



                    //string quryB = "select Id as RopeId,RopeTypeId,	RopeConstruction,DiaMeter,Length,MBL,LDBF,WLL,ManufacturerId,CertificateNumber,ReceivedDate,InstalledDate,RopeTagging,	OutofServiceDate,	ReasonOutofService,	OtherReason,	DamageObserved,	IncidentReport,MooringOperationID,CurrentRunningHours,MaxRunningHours,MaxMonthsAllowed,	InspectionDueDate,	RopeTail,	DeleteStatus,	Remarks,	CreatedDate,	CreatedBy,	ModifiedDate,	ModifiedBy,	IsActive from MooringRopeDetail";
                    string quryB = "select Id as RopeId,RopeTypeId,	RopeConstruction,DiaMeter,Length,MBL,LDBF,WLL,ManufacturerId,CertificateNumber,ReceivedDate,InstalledDate,RopeTagging,	OutofServiceDate,	ReasonOutofService,	OtherReason,	DamageObserved,	IncidentReport,MooringOperationID,CurrentRunningHours,MaxRunningHours,MaxMonthsAllowed,	InspectionDueDate,	RopeTail,	DeleteStatus,	Remarks,	CreatedDate,	CreatedBy,	ModifiedDate,	ModifiedBy,	IsActive,StartCounterHours, UniqueId,CurrentLeadRunningHours from MooringRopeDetail";
                    SqlCommand cmd9 = new SqlCommand(quryB, con);
                    SqlDataAdapter dap9 = new SqlDataAdapter();
                    System.Data.DataTable dtbl9 = new System.Data.DataTable("MooringRopeDetail");
                    dtbl9.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap9.SelectCommand = cmd9;
                    dap9.Fill(dtbl9);
                    if (dtbl9.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl9);
                    }
                    cmd9.Dispose();
                    dap9.Dispose();
                    dtbl9.Dispose();

                    _Export.ReportProgress(ib + 10);


                    //string quryBB = "Select * from MooringRopeInspection";
                    //string quryBB = "Select * from MooringRopeInspection  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    string quryBB = "Select * from MooringRopeInspection  WHERE  ( CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' ) or ( InspectDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND InspectDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )";


                    SqlCommand cmd10 = new SqlCommand(quryBB, con);
                    SqlDataAdapter dap10 = new SqlDataAdapter();
                    System.Data.DataTable dtbl10 = new System.Data.DataTable("MooringRopeInspection");
                    dtbl10.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap10.SelectCommand = cmd10;
                    dap10.Fill(dtbl10);
                    if (dtbl10.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl10);
                    }
                    cmd10.Dispose();
                    dap10.Dispose();
                    //=============
                    //for (int i = 0; i < dtbl10.Rows.Count; i++)
                    //{
                    //    // dtbl10.Rows[i]["Photo1"];
                    //    dtbl10.Rows[i]["Photo1"] = null;
                    //    dtbl10.Rows[i]["Photo2"] = null;
                    //    //dtbl10.Rows[i]["Image1"] = dtbl10.Rows[i]["Image1"];
                    //    //dtbl10.Rows[i]["Image2"] = dtbl10.Rows[i]["Image2"];
                    //}

                    //=============
                    dtbl10.Dispose();

                    _Export.ReportProgress(ib + 11);


                    string quryBBB = "Select * from MooringRopeType";
                    //  string quryBBB = "Select * from MooringRopeType  WHERE   CreatedDate >= '" + Convert.ToDateTime(MyDateTimeFrom).ToShortDateString() + "' AND CreatedDate   <= '" + Convert.ToDateTime(MyDateTimeTo).ToShortDateString() + "'";
                    SqlCommand cmd11 = new SqlCommand(quryBBB, con);
                    SqlDataAdapter dap11 = new SqlDataAdapter();
                    System.Data.DataTable dtbl11 = new System.Data.DataTable("MooringRopeType");
                    dtbl11.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap11.SelectCommand = cmd11;
                    dap11.Fill(dtbl11);
                    if (dtbl11.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl11);
                    }
                    cmd11.Dispose();
                    dap11.Dispose();
                    dtbl11.Dispose();


                    _Export.ReportProgress(ib + 12);


                    string quryC = "Select * from MooringWinchDetail";
                    //string quryC = "Select * from MooringWinchDetail  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmd12 = new SqlCommand(quryC, con);
                    SqlDataAdapter dap12 = new SqlDataAdapter();
                    System.Data.DataTable dtbl12 = new System.Data.DataTable("MooringWinchDetail");
                    dtbl12.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap12.SelectCommand = cmd12;
                    dap12.Fill(dtbl12);
                    if (dtbl12.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl12);
                    }
                    cmd12.Dispose();
                    dap12.Dispose();
                    dtbl12.Dispose();

                    _Export.ReportProgress(ib + 13);

                    //string quryCC = "Select * from MOperationBirthDetail";
                    string quryCC = "Select * from MOperationBirthDetail  WHERE   FastDatetime >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND FastDatetime <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmd13 = new SqlCommand(quryCC, con);
                    SqlDataAdapter dap13 = new SqlDataAdapter();
                    System.Data.DataTable dtbl13 = new System.Data.DataTable("MOperationBirthDetail");
                    dtbl13.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap13.SelectCommand = cmd13;
                    dap13.Fill(dtbl13);
                    if (dtbl13.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl13);
                    }
                    cmd13.Dispose();
                    dap13.Dispose();
                    dtbl13.Dispose();


                    _Export.ReportProgress(ib + 14);


                    //string quryCCC = "Select * from MOUsedWinchTbl";
                    string quryCCC = "Select * from MOUsedWinchTbl  WHERE   OPDateFrom >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND OPDateFrom <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmd14 = new SqlCommand(quryCCC, con);
                    SqlDataAdapter dap14 = new SqlDataAdapter();
                    System.Data.DataTable dtbl14 = new System.Data.DataTable("MOUsedWinchTbl");
                    dtbl14.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap14.SelectCommand = cmd14;
                    dap14.Fill(dtbl14);
                    if (dtbl14.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl14);
                    }
                    cmd14.Dispose();
                    dap14.Dispose();
                    dtbl14.Dispose();

                    _Export.ReportProgress(ib + 15);


                    string qury5 = "Select * from NotificationComment  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmd15 = new SqlCommand(qury5, con);
                    SqlDataAdapter dap15 = new SqlDataAdapter();
                    System.Data.DataTable dtbl15 = new System.Data.DataTable("NotificationComment");
                    dtbl15.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap15.SelectCommand = cmd15;
                    dap15.Fill(dtbl15);
                    if (dtbl15.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl15);
                    }
                    cmd15.Dispose();
                    dap15.Dispose();
                    dtbl15.Dispose();

                    _Export.ReportProgress(ib + 16);

                    //string qury51 = "Select * from Notifications ";
                    string qury51 = "Select * from Notifications  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmd16 = new SqlCommand(qury51, con);
                    SqlDataAdapter dap16 = new SqlDataAdapter();
                    System.Data.DataTable dtbl16 = new System.Data.DataTable("Notifications");
                    dtbl16.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap16.SelectCommand = cmd16;
                    dap16.Fill(dtbl16);
                    if (dtbl16.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl16);
                    }
                    cmd16.Dispose();
                    dap16.Dispose();
                    dtbl16.Dispose();

                    _Export.ReportProgress(ib + 17);


                    //string qury511 = "Select * from RopeCropping";
                   // string qury511 = "Select * from RopeCropping  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";

                    string qury511 = "Select * from RopeCropping  WHERE  ( CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' ) or ( CroppedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CroppedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )";

                    SqlCommand cmd17 = new SqlCommand(qury511, con);
                    SqlDataAdapter dap17 = new SqlDataAdapter();
                    System.Data.DataTable dtbl17 = new System.Data.DataTable("RopeCropping");
                    dtbl17.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap17.SelectCommand = cmd17;
                    dap17.Fill(dtbl17);
                    if (dtbl17.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl17);
                    }
                    cmd17.Dispose();
                    dap17.Dispose();
                    dtbl17.Dispose();

                    _Export.ReportProgress(ib + 18);


                    //string qury5111 = "Select * from RopeDamageRecord";
                    //string qury5111 = "Select * from RopeDamageRecord  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";

                    string qury5111 = "Select * from RopeDamageRecord  WHERE  ( CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' ) or ( DamageDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND DamageDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )";

                    SqlCommand cmd18 = new SqlCommand(qury5111, con);
                    SqlDataAdapter dap18 = new SqlDataAdapter();
                    System.Data.DataTable dtbl18 = new System.Data.DataTable("RopeDamageRecord");
                    dtbl18.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap18.SelectCommand = cmd18;
                    dap18.Fill(dtbl18);
                    if (dtbl18.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl18);
                    }
                    cmd18.Dispose();
                    dap18.Dispose();
                    dtbl18.Dispose();

                    _Export.ReportProgress(ib + 19);

                    //string qury6 = "Select * from RopeDisposal";
                   // string qury6 = "Select * from RopeDisposal  WHERE  CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";

                    string qury6 = "Select * from RopeDisposal  WHERE  ( CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' ) or ( DisposalDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND DisposalDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )";
                    SqlCommand cmd19 = new SqlCommand(qury6, con);
                    SqlDataAdapter dap19 = new SqlDataAdapter();
                    System.Data.DataTable dtbl19 = new System.Data.DataTable("RopeDisposal");
                    dtbl19.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap19.SelectCommand = cmd19;
                    dap19.Fill(dtbl19);
                    if (dtbl19.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl19);
                    }
                    cmd19.Dispose();
                    dap19.Dispose();
                    dtbl19.Dispose();

                    _Export.ReportProgress(ib + 20);


                    //string qury61 = "Select * from RopeEndtoEnd2";
                   // string qury61 = "Select * from RopeEndtoEnd2 WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";

                    string qury61 = "Select * from RopeEndtoEnd2  WHERE  ( CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' ) or ( EndtoEndDoneDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND EndtoEndDoneDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )";
                    SqlCommand cmd20 = new SqlCommand(qury61, con);
                    SqlDataAdapter dap20 = new SqlDataAdapter();
                    System.Data.DataTable dtbl20 = new System.Data.DataTable("RopeEndtoEnd2");
                    dtbl20.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap20.SelectCommand = cmd20;
                    dap20.Fill(dtbl20);
                    if (dtbl20.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl20);
                    }
                    cmd20.Dispose();
                    dap20.Dispose();
                    dtbl20.Dispose();

                    _Export.ReportProgress(ib + 21);


                    // string qury611 = "Select * from RopeSplicingRecord";
                   // string qury611 = "Select * from RopeSplicingRecord  WHERE  CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";

                    string qury611 = "Select * from RopeSplicingRecord  WHERE  ( CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' ) or ( SplicingDoneDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND SplicingDoneDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )";
                    SqlCommand cmd21 = new SqlCommand(qury611, con);
                    SqlDataAdapter dap21 = new SqlDataAdapter();
                    System.Data.DataTable dtbl21 = new System.Data.DataTable("RopeSplicingRecord");
                    dtbl21.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap21.SelectCommand = cmd21;
                    dap21.Fill(dtbl21);
                    if (dtbl21.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl21);
                    }
                    cmd21.Dispose();
                    dap21.Dispose();
                    dtbl21.Dispose();

                    _Export.ReportProgress(ib + 22);


                    //string qury6111 = "Select * from RopeTail";
                    string qury6111 = "Select * from RopeTail";
                    SqlCommand cmd22 = new SqlCommand(qury6111, con);
                    SqlDataAdapter dap22 = new SqlDataAdapter();
                    System.Data.DataTable dtbl22 = new System.Data.DataTable("RopeTail");
                    dtbl22.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap22.SelectCommand = cmd22;
                    dap22.Fill(dtbl22);
                    if (dtbl22.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl22);
                    }
                    cmd22.Dispose();
                    dap22.Dispose();
                    dtbl22.Dispose();

                    _Export.ReportProgress(ib + 23);

                    //string qury61112 = "Select * from Vessel";
                    //SqlCommand cmd222 = new SqlCommand(qury61112, con);
                    //SqlDataAdapter dap222 = new SqlDataAdapter();
                    //System.Data.DataTable dtbl222 = new System.Data.DataTable("Vessel");





                    //dtbl222.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    //dap222.SelectCommand = cmd222;

                    //dtbl222.Columns.Add("DateFrom", typeof(DateTime));
                    //dtbl222.Columns.Add("DateTo", typeof(DateTime));

                    //dap222.Fill(dtbl222);

                    //DateTime dtfrom = Convert.ToDateTime(MyDateTimeFrom);
                    //DateTime dtto = Convert.ToDateTime(MyDateTimeTo);


                    //string LicExpire = "Expiring On" + " " + ss1.ToString("dd-MMM-yyyy");


                    //foreach (DataRow dr in dtbl222.Rows)
                    //{

                    //    dr["DateFrom"] = dtfrom;
                    //    dr["DateTo"] = dtto;

                    //}

                    //if (dtbl222.Rows.Count > 0)
                    //{
                    //    dataSet.Tables.Add(dtbl222);
                    //}
                    //cmd222.Dispose();
                    //dap222.Dispose();
                    //dtbl222.Dispose();

                    //_Export.ReportProgress(ib + 24);



                    //string qury99 = "Select * from ResidualLabTest";
                    //string qury99 = "Select * from ResidualLabTest  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";

                    string qury99 = "Select * from ResidualLabTest  WHERE   ( CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )  or ( LabTestDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND LabTestDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' )";
                    SqlCommand cmd99 = new SqlCommand(qury99, con);
                    SqlDataAdapter dap99 = new SqlDataAdapter();
                    System.Data.DataTable dtbl99 = new System.Data.DataTable("ResidualLabTest");
                    dtbl99.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap99.SelectCommand = cmd99;
                    dap99.Fill(dtbl99);
                    if (dtbl99.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl99);
                    }
                    cmd99.Dispose();
                    dap99.Dispose();
                    dtbl99.Dispose();

                    _Export.ReportProgress(ib + 25);




                    //string qury199 = "Select * from TrainingAttachment  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    string qury199 = "Select * from TrainingAttachment";
                    SqlCommand cmd199 = new SqlCommand(qury199, con);
                    SqlDataAdapter dap199 = new SqlDataAdapter();
                    System.Data.DataTable dtbl199 = new System.Data.DataTable("TrainingAttachment");
                    dtbl199.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap199.SelectCommand = cmd199;
                    dap199.Fill(dtbl199);
                    if (dtbl199.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl199);
                    }
                    cmd199.Dispose();
                    dap199.Dispose();
                    dtbl199.Dispose();

                    _Export.ReportProgress(ib + 26);




                   // string qury199k = "Select * from ChafeGuard  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    string qury199k = "Select * from ChafeGuard";
                    SqlCommand cmd199k = new SqlCommand(qury199k, con);
                    SqlDataAdapter dap199k = new SqlDataAdapter();
                    System.Data.DataTable dtbl199k = new System.Data.DataTable("ChafeGuard");
                    dtbl199k.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap199k.SelectCommand = cmd199k;
                    dap199k.Fill(dtbl199k);
                    if (dtbl199k.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl199k);
                    }
                    cmd199k.Dispose();
                    dap199k.Dispose();
                    dtbl199k.Dispose();

                    _Export.ReportProgress(ib + 27);


                    string qury199kk = "Select * from WinchBreakTestKit  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmd199kk = new SqlCommand(qury199kk, con);
                    SqlDataAdapter dap199kk = new SqlDataAdapter();
                    System.Data.DataTable dtbl199kk = new System.Data.DataTable("WinchBreakTestKit");
                    dtbl199kk.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap199kk.SelectCommand = cmd199kk;
                    dap199kk.Fill(dtbl199kk);
                    if (dtbl199kk.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl199kk);
                    }
                    cmd199kk.Dispose();
                    dap199kk.Dispose();
                    dtbl199kk.Dispose();

                    _Export.ReportProgress(ib + 28);

                    string quryLast = "Select * from MinimumRopeAndTailsSetting";
                    SqlCommand cmdLast = new SqlCommand(quryLast, con);
                    SqlDataAdapter dapLast = new SqlDataAdapter();
                    System.Data.DataTable dtblLast = new System.Data.DataTable("MinimumRopeAndTailsSetting");
                    dtblLast.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dapLast.SelectCommand = cmdLast;
                    dapLast.Fill(dtblLast);
                    if (dtblLast.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblLast);
                    }
                    cmdLast.Dispose();
                    dapLast.Dispose();
                    dtblLast.Dispose();

                    _Export.ReportProgress(ib + 29);

                    //string quryLast1 = "Select * from version_tbl";
                    //OleDbCommand cmdLast1 = new OleDbCommand(quryLast1, con);
                    //OleDbDataAdapter dapLast1 = new OleDbDataAdapter();
                    //System.Data.DataTable dtblLast1 = new System.Data.DataTable("VersionTable");
                    //dtblLast1.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    //dapLast1.SelectCommand = cmdLast1;
                    //dapLast1.Fill(dtblLast1);
                    //if (dtblLast1.Rows.Count > 0)
                    //{
                    //    dataSet.Tables.Add(dtblLast1);
                    //}
                    //cmdLast1.Dispose();
                    //dapLast1.Dispose();
                    //dtblLast1.Dispose();

                    //_Export.ReportProgress(ib + 30);



                    SqlDataAdapter sdakkp = new SqlDataAdapter("Select * from AdminLogin", sc.con);
                    System.Data.DataTable dtpkk = new System.Data.DataTable();
                    sdakkp.Fill(dtpkk);
                    DateTime ss1 = DateTime.Now;
                    if (dtpkk.Rows.Count > 0)
                    {
                        string productinfo = dtpkk.Rows[0]["productinfo"].ToString();
                        string RecordData = Decrypt(productinfo, "KKPrajapat");


                        string[] RecordArr = RecordData.Split(',');
                        string nxdt = RecordArr[1];

                        ss1 = Convert.ToDateTime(nxdt);
                    }


                    var dd = sc.Vessels.FirstOrDefault();
                    var dd1 = sc.Versions.FirstOrDefault();

                    _Export.ReportProgress(ib + 30);

                    //string LicExpire = "Expiring On" + " " + ss1.ToString("dd-MMM-yyyy");



                    string qury61112 = "Select * from Vessel";
                    SqlCommand cmd222 = new SqlCommand(qury61112, con);
                    SqlDataAdapter dap222 = new SqlDataAdapter();
                    System.Data.DataTable dtbl222 = new System.Data.DataTable("Vessel");





                    dtbl222.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dap222.SelectCommand = cmd222;

                    dtbl222.Columns.Add("DateFrom", typeof(DateTime));
                    dtbl222.Columns.Add("DateTo", typeof(DateTime));
                    dtbl222.Columns.Add("LicenseExpiry", typeof(DateTime));

                    dap222.Fill(dtbl222);

                    DateTime dtfrom = Convert.ToDateTime(MyDateTimeFrom);
                    DateTime dtto = Convert.ToDateTime(MyDateTimeTo);


                    DateTime LicExpire = Convert.ToDateTime( ss1.ToString("yyyy-MM-dd"));


                    foreach (DataRow dr in dtbl222.Rows)
                    {

                        dr["DateFrom"] = dtfrom;
                        dr["DateTo"] = dtto;
                        dr["LicenseExpiry"] = LicExpire;

                    }

                    if (dtbl222.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtbl222);
                    }
                    cmd222.Dispose();
                    dap222.Dispose();
                    dtbl222.Dispose();

                    _Export.ReportProgress(ib + 24);




                    string quryinputGen = "Select * from InputGeneralParticulars  WHERE   InputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND InputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdinputGen = new SqlCommand(quryinputGen, con);
                    SqlDataAdapter dtblintputGen = new SqlDataAdapter();
                    System.Data.DataTable dtblintputGeneral = new System.Data.DataTable("InputGeneralParticulars");
                    dtblintputGeneral.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dtblintputGen.SelectCommand = cmdinputGen;
                    dtblintputGen.Fill(dtblintputGeneral);
                    if (dtblintputGeneral.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblintputGeneral);
                    }
                    cmdinputGen.Dispose();
                    dtblintputGeneral.Dispose();
                    dtblintputGen.Dispose();

                    _Export.ReportProgress(ib + 31);


                    string qurIMC = "Select * from InputMooringCalculation  WHERE   InputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND InputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdIMC = new SqlCommand(qurIMC, con);
                    SqlDataAdapter adpIMC = new SqlDataAdapter();
                    System.Data.DataTable dtblIMC = new System.Data.DataTable("InputMooringCalculation");
                    dtblIMC.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpIMC.SelectCommand = cmdIMC;
                    adpIMC.Fill(dtblIMC);
                    if (dtblIMC.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblIMC);
                    }
                    cmdIMC.Dispose();
                    adpIMC.Dispose();
                    dtblIMC.Dispose();

                    _Export.ReportProgress(ib + 32);


                    string qurIPP = "Select * from InputPrincipalParticulars  WHERE   InputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND InputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdIPP = new SqlCommand(qurIPP, con);
                    SqlDataAdapter adpIPP = new SqlDataAdapter();
                    System.Data.DataTable dtblIPP = new System.Data.DataTable("InputPrincipalParticulars");
                    dtblIPP.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpIPP.SelectCommand = cmdIPP;
                    adpIPP.Fill(dtblIPP);
                    if (dtblIPP.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblIPP);
                    }
                    cmdIPP.Dispose();
                    dtblIPP.Dispose();
                    adpIPP.Dispose();

                    _Export.ReportProgress(ib + 33);



                    string qurIWCP = "Select * from InputWindAndCurrentParameters  WHERE   InputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND InputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdIWCP = new SqlCommand(qurIWCP, con);
                    SqlDataAdapter adpIWCP = new SqlDataAdapter();
                    System.Data.DataTable dtblIWCP = new System.Data.DataTable("InputWindAndCurrentParameters");
                    dtblIWCP.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpIWCP.SelectCommand = cmdIWCP;
                    adpIWCP.Fill(dtblIWCP);
                    if (dtblIWCP.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblIWCP);
                    }
                    dtblIWCP.Dispose();
                    cmdIWCP.Dispose();
                    adpIWCP.Dispose();

                    _Export.ReportProgress(ib + 34);



                    string qurIWA = "Select * from InputWindArea  WHERE   InputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND InputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdIWA = new SqlCommand(qurIWA, con);
                    SqlDataAdapter adpIWA = new SqlDataAdapter();
                    System.Data.DataTable dtblIWA = new System.Data.DataTable("InputWindArea");
                    dtblIWA.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpIWA.SelectCommand = cmdIWA;
                    adpIWA.Fill(dtblIWA);
                    if (dtblIWA.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblIWA);
                    }
                    cmdIWA.Dispose();
                    dtblIWA.Dispose();
                    adpIWA.Dispose();

                    _Export.ReportProgress(ib + 35);


                    string qurOBWP = "Select * from OutputBasicWindPartameters  WHERE   OutputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND OutputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdOBWP = new SqlCommand(qurOBWP, con);
                    SqlDataAdapter adpOBWP = new SqlDataAdapter();
                    System.Data.DataTable dtblOBWP = new System.Data.DataTable("OutputBasicWindPartameters");
                    dtblOBWP.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpOBWP.SelectCommand = cmdOBWP;
                    adpOBWP.Fill(dtblOBWP);
                    if (dtblOBWP.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblOBWP);
                    }
                    cmdOBWP.Dispose();
                    dtblOBWP.Dispose();
                    adpOBWP.Dispose();

                    _Export.ReportProgress(ib + 36);



                    string qurOCWL = "Select * from OutputCurrentWindLoads  WHERE   OutputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND OutputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdOCWL = new SqlCommand(qurOCWL, con);
                    SqlDataAdapter adpOCWL = new SqlDataAdapter();
                    System.Data.DataTable dtblOCWL = new System.Data.DataTable("OutputCurrentWindLoads");
                    dtblOCWL.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpOCWL.SelectCommand = cmdOCWL;
                    adpOCWL.Fill(dtblOCWL);
                    if (dtblOCWL.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblOCWL);
                    }
                    cmdOCWL.Dispose();
                    adpOCWL.Dispose();
                    dtblOCWL.Dispose();

                    _Export.ReportProgress(ib + 37);



                    string qurOCFC = "Select * from OutputCuurentForceCoefficients  WHERE   OutputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND OutputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdOCFC = new SqlCommand(qurOCFC, con);
                    SqlDataAdapter adpOCFC = new SqlDataAdapter();
                    System.Data.DataTable dtblOCFC = new System.Data.DataTable("OutputCuurentForceCoefficients");
                    dtblOCFC.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpOCFC.SelectCommand = cmdOCFC;
                    adpOCFC.Fill(dtblOCFC);
                    if (dtblOCFC.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblOCFC);
                    }
                    cmdOCFC.Dispose();
                    adpOCFC.Dispose();
                    dtblOCFC.Dispose();

                    _Export.ReportProgress(ib + 38);




                    string qurOFFM = "Select * from OutputFinalForcesAndMoments  WHERE   OutputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND OutputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdOFFM = new SqlCommand(qurOFFM, con);
                    SqlDataAdapter adpOFFM = new SqlDataAdapter();
                    System.Data.DataTable dtblOFFM = new System.Data.DataTable("OutputFinalForcesAndMoments");
                    dtblOFFM.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpOFFM.SelectCommand = cmdOFFM;
                    adpOFFM.Fill(dtblOFFM);
                    if (dtblOFFM.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblOFFM);
                    }
                    cmdOFFM.Dispose();
                    adpOFFM.Dispose();
                    dtblOFFM.Dispose();

                    _Export.ReportProgress(ib + 39);



                    string qurOMFC = "Select * from OutputMorringForcesCalculation  WHERE   OutputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND OutputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdOMFC = new SqlCommand(qurOMFC, con);
                    SqlDataAdapter adpOMFC = new SqlDataAdapter();
                    System.Data.DataTable dtblOMFC = new System.Data.DataTable("OutputMorringForcesCalculation");
                    dtblOMFC.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpOMFC.SelectCommand = cmdOMFC;
                    adpOMFC.Fill(dtblOMFC);
                    if (dtblOMFC.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblOMFC);
                    }
                    cmdOMFC.Dispose();
                    adpOMFC.Dispose();
                    dtblOMFC.Dispose();

                    _Export.ReportProgress(ib + 40);



                    string qurOWFC = "Select * from OutputWindForceCoefficients  WHERE   OutputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND OutputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdOWFC = new SqlCommand(qurOWFC, con);
                    SqlDataAdapter adpOWFC = new SqlDataAdapter();
                    System.Data.DataTable dtblOWFC = new System.Data.DataTable("OutputWindForceCoefficients");
                    dtblOWFC.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpOWFC.SelectCommand = cmdOWFC;
                    adpOWFC.Fill(dtblOWFC);
                    if (dtblOWFC.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblOWFC);
                    }
                    cmdOWFC.Dispose();
                    adpOWFC.Dispose();
                    dtblOWFC.Dispose();

                    _Export.ReportProgress(ib + 41);



                    string qurOWL = "Select * from OutputWindLoads  WHERE   OutputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND OutputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdOWL = new SqlCommand(qurOWL, con);
                    SqlDataAdapter adpOWL = new SqlDataAdapter();
                    System.Data.DataTable dtblOWL = new System.Data.DataTable("OutputWindLoads");
                    dtblOWL.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpOWL.SelectCommand = cmdOWL;
                    adpOWL.Fill(dtblOWL);
                    if (dtblOWL.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblOWL);
                    }
                    cmdOWL.Dispose();
                    adpOWL.Dispose();
                    dtblOWL.Dispose();

                    _Export.ReportProgress(ib + 42);


                    string qurWBTKA = "Select * from WinchBrakeTestAttachment  WHERE   CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdWBTKA = new SqlCommand(qurWBTKA, con);
                    SqlDataAdapter adpWBTKA = new SqlDataAdapter();
                    System.Data.DataTable dtblWBTKA = new System.Data.DataTable("WinchBrakeTestAttachment");
                    dtblWBTKA.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpWBTKA.SelectCommand = cmdWBTKA;
                    adpWBTKA.Fill(dtblWBTKA);
                    if (dtblWBTKA.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblWBTKA);
                    }
                    cmdWBTKA.Dispose();
                    adpWBTKA.Dispose();
                    dtblWBTKA.Dispose();

                    _Export.ReportProgress(ib + 43);



                    string quryLast10 = "Select * from WinchRotation";
                    SqlCommand cmdLast10 = new SqlCommand(quryLast10, con);
                    SqlDataAdapter dapLast10 = new SqlDataAdapter();
                    System.Data.DataTable dtblLast10 = new System.Data.DataTable("WinchRotation");
                    dtblLast10.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dapLast10.SelectCommand = cmdLast10;
                    dapLast10.Fill(dtblLast10);
                    if (dtblLast10.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblLast10);
                    }
                    cmdLast10.Dispose();
                    dapLast10.Dispose();
                    dtblLast10.Dispose();

                    _Export.ReportProgress(ib + 44);




                    string quryLast1 = "Select * from version_tbl";
                    SqlCommand cmdLast1 = new SqlCommand(quryLast1, con);
                    SqlDataAdapter dapLast1 = new SqlDataAdapter();
                    System.Data.DataTable dtblLast1 = new System.Data.DataTable("VersionTable");
                    dtblLast1.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dapLast1.SelectCommand = cmdLast1;
                    dapLast1.Fill(dtblLast1);
                    if (dtblLast1.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblLast1);
                    }
                    cmdLast1.Dispose();
                    dapLast1.Dispose();
                    dtblLast1.Dispose();

                    _Export.ReportProgress(ib + 45);




                    string qurOBCP = "Select * from OutputBasicCurrentParameters  WHERE   OutputDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND OutputDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
                    SqlCommand cmdOBCP = new SqlCommand(qurOBCP, con);
                    SqlDataAdapter adpOBCP = new SqlDataAdapter();
                    System.Data.DataTable dtblOBCP = new System.Data.DataTable("OutputBasicCurrentParameters");
                    dtblOBCP.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    adpOBCP.SelectCommand = cmdOBCP;
                    adpOBCP.Fill(dtblOBCP);
                    if (dtblOBCP.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblOBCP);
                    }
                    cmdOBCP.Dispose();
                    dtblOBCP.Dispose();
                    adpOBCP.Dispose();

                    _Export.ReportProgress(ib + 46);


                    string quryLast19 = "select a.* from MooringRopeAttachment a inner join MooringRopeDetail b on a.RopeId = b.Id WHERE b.CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND b.CreatedDate <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'";
               
                    //string quryLast19 = "Select * from version_tbl";
                    SqlCommand cmdLast19 = new SqlCommand(quryLast19, con);
                    SqlDataAdapter dapLast19 = new SqlDataAdapter();
                    System.Data.DataTable dtblLast19 = new System.Data.DataTable("MooringRopeAttachment");
                    dtblLast19.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dapLast19.SelectCommand = cmdLast19;
                    dapLast19.Fill(dtblLast19);
                    if (dtblLast19.Rows.Count > 0)
                    {
                        dataSet.Tables.Add(dtblLast19);
                    }
                    cmdLast19.Dispose();
                    dapLast19.Dispose();
                    dtblLast19.Dispose();

                    _Export.ReportProgress(ib + 47);



                    //System.Data.DataTable licKey = new System.Data.DataTable("SecurityKey");
                    //licKey.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;                    
                    //    dataSet.Tables.Add(StaticHelper.CPU_ProcessorID);
                    //licKey.Dispose();                  

                    //_Export.ReportProgress(ib + 47);


                    //SqlDataAdapter sdakkp = new SqlDataAdapter("Select * from AdminLogin", con);
                    //System.Data.DataTable dtpkk = new System.Data.DataTable();
                    //sdakkp.Fill(dtpkk);
                    //DateTime ss1 = DateTime.Now;
                    //if (dtpkk.Rows.Count > 0)
                    //{
                    //    string productinfo = dtpkk.Rows[0]["productinfo"].ToString();
                    //    string RecordData = Decrypt(productinfo, StaticHelper.Key);


                    //    string[] RecordArr = RecordData.Split(',');
                    //    string nxdt = RecordArr[1];

                    //    ss1 = Convert.ToDateTime(nxdt);
                    //}

                    //var dd = sc.Vessels.FirstOrDefault();
                    //var dd1 = sc.Versions.FirstOrDefault();

                    //_Export.ReportProgress(ib + 8);

                    //string LicExpire = "Expiring On" + " " + ss1.ToString("dd-MMM-yyyy");

                    System.Data.DataTable dtLicingo = new System.Data.DataTable("Product_Detail");
                    dtLicingo.Columns.Add("Digimoor-X7", typeof(string));

                    //string[] info = { "Version : " + dd1.versions, "SubVersion : " + dd1.SubVersions, "Ship Name : " + dd.VesselName, "IMO : " + dd.imo, "License Expiring On : " + ss1.ToString("dd-MMM-yyyy"), "Serial number : " + StaticHelper.CPU_ProcessorID };
                    string[] info = { "Version : " + dd1.versions, "Ship Name : " + dd.VesselName, "IMO : " + dd.imo, "License Expiring On : " + ss1.ToString("dd-MMM-yyyy"), "Serial number : " + StaticHelper.CPU_ProcessorID };
                    foreach (string item in info)
                    {
                        DataRow dr = dtLicingo.NewRow();
                        dr["Digimoor-X7"] = item;
                        dtLicingo.Rows.Add(dr);
                    }
                    dataSet.Tables.Add(dtLicingo);
                    dtLicingo.Dispose();




                    //CPU_ProcessorID


                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (DataTable item in dataSet.Tables)
                        {
                            var mytbl = item.TableName;
                            var protectedsheet = wb.Worksheets.Add(item);
                            protectedsheet.Name = item.TableName;
                            var projection = protectedsheet.Protect("49WEB$TREET#");
                            projection.InsertColumns = true;
                            projection.InsertRows = true;

                            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            wb.Style.Font.Bold = true;
                        }
                        wb.SaveAs(sfd.FileName);
                        MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);



                    }


                    /*
                    ApplicationClass ExcelApp = new ApplicationClass();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    Workbook xlWorkbook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                    // Loop over DataTables in DataSet.
                    dataSet.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    DataTableCollection collection = dataSet.Tables;
                    dataSet.Dispose();

                    var a = ib;
                    var cons = collection.Count;
                    for (int i = cons; i > 0; i--)
                    {

                        Sheets xlSheets = null;
                        Worksheet xlWorksheet = null;
                        //Create Excel Sheets
                        xlSheets = ExcelApp.Sheets;
                        xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                                       Type.Missing, Type.Missing, Type.Missing);

                        System.Data.DataTable table = collection[i - 1];
                        xlWorksheet.Name = table.TableName;
                        var tcoun = table.Columns.Count;
                        for (int j = 1; j < tcoun + 1; j++)
                        {
                            ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                        }


                        // Storing Each row and column value to excel sheet
                        var rcoun = table.Rows.Count;
                        var rcoun1 = table.Columns.Count;
                        for (int k = 0; k < rcoun; k++)
                        {

                            for (int l = 0; l < rcoun1; l++)
                            {
                                ExcelApp.Cells[k + 2, l + 1] = table.Rows[k].ItemArray[l];
                            }


                        }

                        ExcelApp.Columns.AutoFit();
                        ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[xlWorksheet.Name]).Protect("49WEB$TREET#");


                        ib = ib + i;
                        _Export.ReportProgress(ib * (20 / cons + a));

                    }

                    ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();

                    ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).SaveAs(sfd.FileName);
                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();

                    MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    */
                }
                //else
                //{

                //    MessageBox.Show("canceled");
                //}

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }


        private string Decrypt(string strText, string strEncrypt)
        {
            byte[] bKey = new byte[20];
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                Byte[] inputByteArray = Convert.FromBase64String(strText.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                return null;
            }
        }


        private void BrowseMethod_Old()
        {

            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result1 = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result1 == true)
            {
                ImportVisibles = true;
                RaisePropertyChanged("ImportVisibles");



                string filename = dlg.FileName;
                int vesselid = sc.Vessels.Select(x => x.vessel_ID).FirstOrDefault();

                try
                {
                    Employees = new ObservableCollection<CommentofVSClass>();

                    //string con = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=yes'", filename);
                    //string con = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=yes'", filename);
                    //OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt2"].ConnectionString);
                    //con.Open();
                    //string con = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                    using (OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt2"].ConnectionString))
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand("select * from [Revision$]", connection);
                        using (OleDbDataReader Reader = command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                while (Reader.Read())
                                {
                                    SqlBulkCopy oSqlBulk = null;

                                    if (Convert.ToInt32(Reader["Vessel_ID"]).Equals(vesselid))
                                    {
                                        string sCon = "Data Source=DigiMoorDB_V2;Persist Security Info=False;" + "Integrated Security=SSPI;" + "Initial Catalog=DNA_Classified;User Id=sa;Password=49webstreet@;" + "Connect Timeout=30;";
                                        //using (SqlConnection con2 = new SqlConnection(sCon))
                                        //{
                                        //    con2.Open();

                                        //    // FINALLY, LOAD DATA INTO THE DATABASE TABLE.
                                        //    oSqlBulk = new SqlBulkCopy(con2);
                                        //    oSqlBulk.DestinationTableName = "EmployeeDetails"; // TABLE NAME.
                                        //    oSqlBulk.WriteToServer(Reader);
                                        //}
                                        Employees.Add(new CommentofVSClass()
                                        {
                                            Vessel_ID = Convert.ToInt32(Reader["Vessel_ID"]),
                                            NC_Date = Convert.ToDateTime(Reader["NC_Date"]),
                                            FullName = Reader["FullName"].ToString(),
                                            UserName = Reader["UserName"].ToString(),
                                            Position = Reader["Position"].ToString(),
                                            DepartmentName = Reader["DepartmentName"].ToString(),
                                            Office_Comment = Reader["UserName"].ToString(),
                                            Comment_Date = Convert.ToDateTime(Reader["Comment_Date"])
                                        });
                                    }
                                    else
                                    {
                                        importVisibles = false;
                                        RaisePropertyChanged("ImportVisibles");

                                        MessageBox.Show("Invalid Vessel! ", "", MessageBoxButton.OK, MessageBoxImage.Information);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                importVisibles = false;
                                RaisePropertyChanged("ImportVisibles");
                                MessageBox.Show("This is not valid file.", "", MessageBoxButton.OK, MessageBoxImage.Information);

                            }
                        }
                    }




                }
                catch (Exception ex)
                {
                    importVisibles = false;
                    RaisePropertyChanged("ImportVisibles");
                    sc.ErrorLog(ex);
                    //MessageBox.Show("This is not valid file.", "", MessageBoxButton.OK, MessageBoxImage.Information);

                }



            }


        }



   



        //private System.Data.DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        //{


        //    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

        //    System.Data.DataTable table = new System.Data.DataTable();
        //    for (int i = 0; i < props.Count; i++)
        //    {
        //        PropertyDescriptor prop = props[i];
        //        table.Columns.Add(prop.Name, prop.PropertyType);
        //    }
        //    object[] values = new object[props.Count];
        //    foreach (T item in Linqlist)
        //    {
        //        for (int i = 0; i < values.Length; i++)
        //        {
        //            values[i] = props[i].GetValue(item);
        //        }
        //        table.Rows.Add(values);
        //    }

        //    return table;
        //}


        private Nullable<DateTime> myDateTimeFrom = null;
        public Nullable<DateTime> MyDateTimeFrom
        {
            get
            {
                if (myDateTimeFrom == null)
                {
                    myDateTimeFrom = DateTime.Now.AddMonths(-1);
                }

                return myDateTimeFrom;
            }
            set
            {

                myDateTimeFrom = value;
                RaisePropertyChanged("MyDateTimeFrom");
            }
        }

        private Nullable<DateTime> myDateTimeTo = null;
        public Nullable<DateTime> MyDateTimeTo
        {
            get
            {
                if (myDateTimeTo == null)
                {
                    myDateTimeTo = DateTime.Today;
                }

                return myDateTimeTo;
            }
            set
            {

                myDateTimeTo = value;
                RaisePropertyChanged("MyDateTimeTo");
            }
        }


        //public event ProgressChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged(ProgressChangedEventArgs e)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, e);
        //}


        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        private void ImportAttachment()
        {
            try
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    //OpenFileDialog dlg = new OpenFileDialog();

                    //Nullable<bool> result1 = dlg.ShowDialog();

                    string sourceDirectory = dialog.SelectedPath; //string targetDirectory = "";


                    //string mypath = @"C:\DigiMoorDB_Backup\Attachment";
                    string ServerName = StaticHelper.ServerName;
                    string mypath =  ServerName + "\\DigiMoorDB_Backup\\Attachment";

                    //string subdir = @"C:\Temp\Mahesh";
                    // If directory does not exist, create it. 
                    if (!Directory.Exists(mypath))
                    {
                        Directory.CreateDirectory(mypath);
                    }
                    DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
                    DirectoryInfo diTarget = new DirectoryInfo(mypath);

                    CopyAll(diSource, diTarget);

                    MessageBox.Show("Attachment Import Successfully", "Import/Export", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch { }

        }
        int aa = 0;
        private void ExportAttachment()
        {
            try
            {
                
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                string DestinationDirectory = dialog.SelectedPath;

                exprtatt(DestinationDirectory);
                string ServerName = StaticHelper.ServerName;
                string mypath = ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\";
                if (!Directory.Exists(mypath))
                {
                    Directory.CreateDirectory(mypath);
                }
                DateTime StartDate = Convert.ToDateTime(MyDateTimeFrom);
                DateTime EndDate = Convert.ToDateTime(MyDateTimeTo).AddDays(1);

               

                SqlDataAdapter adp = new SqlDataAdapter("select image1,image2 from MooringRopeInspection where CreatedDate >= @from and CreatedDate <= @to", sc.con);
                adp.SelectCommand.Parameters.AddWithValue("@from", StartDate.Date);
                adp.SelectCommand.Parameters.AddWithValue("@to", EndDate.Date);
                System.Data.DataTable dt = new System.Data.DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    aa++;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string name = dt.Rows[i][0].ToString();
                        string name1 = dt.Rows[i][1].ToString();

                        if (!string.IsNullOrEmpty(name))
                        {
                            var sourceFile = System.IO.Path.Combine(mypath, System.IO.Path.GetFileName(name));
                            var targetFile = System.IO.Path.Combine(DestinationDirectory, System.IO.Path.GetFileName(name));
                            if (!File.Exists(targetFile))
                            {
                                File.Copy(sourceFile, targetFile, true);
                                // break;
                            }
                        }

                        if (!string.IsNullOrEmpty(name1))
                        {
                            var sourceFile1 = System.IO.Path.Combine(mypath, System.IO.Path.GetFileName(name1));
                            var targetFile1 = System.IO.Path.Combine(DestinationDirectory, System.IO.Path.GetFileName(name1));
                            if (!File.Exists(targetFile1))
                            {
                                File.Copy(sourceFile1, targetFile1, true);
                                // break;
                            }
                        }
                    }
                }




                SqlDataAdapter adp1 = new SqlDataAdapter("select image1,image2 from MooringLooseEquipInspection where CreatedDate >= @from and CreatedDate <= @to", sc.con);
                adp1.SelectCommand.Parameters.AddWithValue("@from", StartDate.Date);
                adp1.SelectCommand.Parameters.AddWithValue("@to", EndDate.Date);
                System.Data.DataTable dt1 = new System.Data.DataTable();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    aa++;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string name = dt1.Rows[i][0].ToString();
                        string name1 = dt1.Rows[i][1].ToString();

                        if (!string.IsNullOrEmpty(name))
                        {
                            var sourceFile = System.IO.Path.Combine(mypath, System.IO.Path.GetFileName(name));
                            var targetFile = System.IO.Path.Combine(DestinationDirectory, System.IO.Path.GetFileName(name));
                            if (!File.Exists(targetFile))
                            {
                                File.Copy(sourceFile, targetFile, true);
                                // break;
                            }
                        }

                        //foreach (var file in Directory.GetFiles(mypath))
                        //{

                        if (!string.IsNullOrEmpty(name1))
                        {
                            var sourceFile1 = System.IO.Path.Combine(mypath, System.IO.Path.GetFileName(name1));
                            var targetFile1 = System.IO.Path.Combine(DestinationDirectory, System.IO.Path.GetFileName(name1));
                            if (!File.Exists(targetFile1))
                            {
                                File.Copy(sourceFile1, targetFile1, true);
                                // break;
                            }
                        }
                    }
                }

                if(aa>0)
                MessageBox.Show("Attachments exported successfully", "Import/Export", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { }

        }

        private void exprtatt( string destDir)
        {
            try
            {
                string startPath1 = "";


                string ServerName = StaticHelper.ServerName;
                string mypath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\";
                if (!Directory.Exists(mypath))
                {
                    Directory.CreateDirectory(mypath);
                }

                //string qry = "select * from mooringropeattachment where LineResidual='Line'";
                string qry = "select a.AttachmentPath,a.RopeId, b.id,b.createddate from MooringRopeAttachment a inner join MooringRopeDetail b on a.RopeId=b.Id WHERE   b.CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND b.CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' or b.modifieddate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND b.modifieddate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "' and a.ResidualID = 0 and b.RopeTail=0";
                SqlDataAdapter adp = new SqlDataAdapter(qry, sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    aa++;
                    string startPath = destDir + "\\LinesAttachment\\";
                    //string startPath = Server.MapPath(string.Format("~/images/Attachments/LinesAttachment"));
                    if (!Directory.Exists(startPath))
                    {
                        Directory.CreateDirectory(startPath);
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var filename1 = dt.Rows[i]["AttachmentPath"].ToString();

                        //string sourcePath = Server.MapPath(string.Format("~/images/AttachFiles/" + filename1));
                        //string targetPath = Server.MapPath(string.Format("~/images/Attachments/LinesAttachment/" + filename1));

                        string sourcePath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\"+ filename1 + "";
                        string targetPath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\LinesAttachment\\" + filename1 + "\\";


                      
                        var targetFile = System.IO.Path.Combine(startPath, System.IO.Path.GetFileName(filename1));
                        if (!File.Exists(targetFile))
                        {
                            File.Copy(sourcePath, targetFile, true);
                            // break;
                        }

                        //if (System.IO.File.Exists(sourcePath))
                        //{
                        //    System.IO.File.Copy(sourcePath, targetPath, true);
                        //}
                    }

                }

                //string qry1 = "select * from mooringropeattachment where LineResidual='RopeTail'";
                string qry1 = "select a.AttachmentPath,a.RopeId, b.id,b.createddate from MooringRopeAttachment a inner join MooringRopeDetail b on a.RopeId=b.Id WHERE   b.CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND b.CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'  or b.modifieddate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND b.modifieddate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'  and a.ResidualID = 0 and b.RopeTail=1";
                SqlDataAdapter adp1 = new SqlDataAdapter(qry1, sc.con);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);

                if (dt1.Rows.Count > 0)
                {
                    aa++;
                    // string startPath = Server.MapPath(string.Format("~/images/Attachments/RopeTailAttachment"));
                    string startPath = destDir + "\\RopeTailAttachment\\";
                    if (!Directory.Exists(startPath))
                    {
                        Directory.CreateDirectory(startPath);
                    }
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        var filename1 = dt1.Rows[i]["AttachmentPath"].ToString();

                        //string sourcePath = Server.MapPath(string.Format("~/images/AttachFiles/" + filename1));
                        //string targetPath = Server.MapPath(string.Format("~/images/Attachments/RopeTailAttachment/" + filename1));

                        string sourcePath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + filename1 + "";
                        string targetPath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\RopeTailAttachment\\" + filename1 + "\\";


                        var targetFile = System.IO.Path.Combine(startPath, System.IO.Path.GetFileName(filename1));
                        if (!File.Exists(targetFile))
                        {
                            File.Copy(sourcePath, targetFile, true);
                            // break;
                        }
                    }

                }

                //string qry11 = "select * from mooringropeattachment where LineResidual='ResidualLine'";
                string qry11 = "select a.AttachmentPath,a.RopeId, b.RopeId,b.createddate from MooringRopeAttachment a inner join ResidualLabTest b on a.RopeId=b.RopeId WHERE   b.CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND b.CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'  or b.modifieddate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND b.modifieddate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'  and a.ResidualID != 0 and b.RopeTail=0";
                SqlDataAdapter adp11 = new SqlDataAdapter(qry11, sc.con);
                DataTable dt11 = new DataTable();
                adp11.Fill(dt11);

                if (dt11.Rows.Count > 0)
                {
                    aa++;
                    //string startPath = Server.MapPath(string.Format("~/images/Attachments/ResidualLineAttachment"));
                    string startPath = destDir + "\\ResidualLineAttachment\\";
                    if (!Directory.Exists(startPath))
                    {
                        Directory.CreateDirectory(startPath);
                    }
                    for (int i = 0; i < dt11.Rows.Count; i++)
                    {
                        var filename1 = dt11.Rows[i]["AttachmentPath"].ToString();

                        //string sourcePath = Server.MapPath(string.Format("~/images/AttachFiles/" + filename1));
                        //string targetPath = Server.MapPath(string.Format("~/images/Attachments/ResidualLineAttachment/" + filename1));

                        string sourcePath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + filename1 + "";
                        string targetPath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\ResidualLineAttachment\\" + filename1 + "\\";


                        var targetFile = System.IO.Path.Combine(startPath, System.IO.Path.GetFileName(filename1));
                        if (!File.Exists(targetFile))
                        {
                            File.Copy(sourcePath, targetFile, true);
                            // break;
                        }
                    }

                }

                //string qry111 = "select * from mooringropeattachment where LineResidual='ResidualRopeTail'";
                string qry111 = "select a.AttachmentPath,a.RopeId, b.RopeId,b.createddate from MooringRopeAttachment a inner join ResidualLabTest b on a.RopeId=b.RopeId WHERE   b.CreatedDate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND b.CreatedDate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'  or b.modifieddate >= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeFrom)) + "' AND b.modifieddate   <= '" + sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date) + "'  and a.ResidualID != 0 and b.RopeTail=1";
                SqlDataAdapter adp111 = new SqlDataAdapter(qry111, sc.con);
                DataTable dt111 = new DataTable();
                adp111.Fill(dt111);

                if (dt111.Rows.Count > 0)
                {
                    aa++;
                    //string startPath = Server.MapPath(string.Format("~/images/Attachments/ResidualRopeTailAttachment"));
                    string startPath = destDir + "\\ResidualRopeTailAttachment\\";
                    if (!Directory.Exists(startPath))
                    {
                        Directory.CreateDirectory(startPath);
                    }
                    for (int i = 0; i < dt111.Rows.Count; i++)
                    {
                        var filename1 = dt111.Rows[i]["AttachmentPath"].ToString();

                        //string sourcePath = Server.MapPath(string.Format("~/images/AttachFiles/" + filename1));
                        //string targetPath = Server.MapPath(string.Format("~/images/Attachments/ResidualRopeTailAttachment/" + filename1));

                        string sourcePath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + filename1 + "";
                        string targetPath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\ResidualRopeTailAttachment\\" + filename1 + "\\";

                        var targetFile = System.IO.Path.Combine(startPath, System.IO.Path.GetFileName(filename1));
                        if (!File.Exists(targetFile))
                        {
                            File.Copy(sourcePath, targetFile, true);
                            // break;
                        }
                    }

                }



                // Download Attachments in Zip File
                //if (Directory.Exists(startPath1))
                //{
                //    string[] fileFound = Directory.GetDirectories(startPath1);
                //    if (fileFound.Length > 0)
                //    {
                //        string zipFileName = string.Format("{0}_{1}.zip", shipIMO, DateTime.Now.Ticks);
                //        string zipPath = Server.MapPath("~/images/" + zipFileName);
                //        ZipFile.CreateFromDirectory(startPath1, zipPath, CompressionLevel.Fastest, true);
                //        Response.ContentType = "application.zip";
                //        Response.AppendHeader("Content-Disposition", "attachment;filename=" + zipFileName);
                //        Response.TransmitFile(zipPath);
                //        Response.End();

                //        if (System.IO.File.Exists(zipPath))
                //        {
                //            System.IO.File.Delete(zipPath);
                //        }
                //    }
                //    else
                //    {
                //        bmsuploaddats.bmsname = "No Attachments found.";
                //    }

                //    new System.IO.DirectoryInfo(startPath1).Delete(true);

                //}
            }
            catch (Exception ex) { }
        }

        private void ExportAttachmentkuldip()
        {
            try
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    //OpenFileDialog dlg = new OpenFileDialog();

                    //Nullable<bool> result1 = dlg.ShowDialog();

                    string sourceDirectory = dialog.SelectedPath; //string targetDirectory = "";
                                                                  //string mypath = @"C:\DigiMoorDB_Backup\InspectionImages";

                    string ServerName = StaticHelper.ServerName;
                    string mypath = ServerName + "\\DigiMoorDB_Backup\\InspectionImages";
                    //string subdir = @"C:\Temp\Rana";
                    // If directory does not exist, create it. 
                    if (!Directory.Exists(mypath))
                    {
                        Directory.CreateDirectory(mypath);
                    }
                    //DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
                    //DirectoryInfo diTarget = new DirectoryInfo(mypath);


                    // DateTime StartDate = sc.DateToSQLFormat(Convert.ToDateTime(MyDateTimeTo).AddDays(1).Date);
                    //DateTime EndDate =

                    DateTime StartDate = Convert.ToDateTime(MyDateTimeFrom);
                    DateTime EndDate = Convert.ToDateTime(MyDateTimeTo).AddDays(1);

                    //DirectoryInfo di = new DirectoryInfo(@"C:\DigiMoorDB_Backup\InspectionImages");


                    //string mypath = @"\\" + ServerName + "\\DigiMoorDB_Backup\\InspectionImages";

                    DirectoryInfo di = new DirectoryInfo(ServerName + "\\DigiMoorDB_Backup\\InspectionImages");

                    // Get a reference to each directory in that directory.
                    DirectoryInfo[] diArr = di.GetDirectories();
                    int aa = 0;
                    //foreach (DirectoryInfo fi in new DirectoryInfo(mypath).GetDirectories())
                    //{
                    string mypath1 = mypath;
                    //if (fi.CreationTime >= StartDate && fi.CreationTime <= EndDate)
                    //{
                    //  var sss = fi.Name;

                    //   mypath1 = mypath + "\\" + sss;

                    DirectoryInfo diSource = new DirectoryInfo(mypath);
                    DirectoryInfo diTarget = new DirectoryInfo(sourceDirectory);

                    CopyAll(diSource, diTarget);
                    aa++;
                    //}
                    //}


                    MessageBox.Show("Attachment Export Successfully", "Import/Export", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch { }

        }


        private static IEnumerable<string> FilesWithinDates(string directory, DateTime minCreated, DateTime maxCreated)
        {
            foreach (FileInfo fi in new DirectoryInfo(directory).GetFiles())
            {
                if (fi.CreationTime >= minCreated && fi.CreationTime <= maxCreated)
                {
                    yield return fi.Name;
                }
            }
        }



        // private void BrowseMethod()
        //private void BrowseMethod(object sender, DoWorkEventArgs eb)
        //{
        //    try
        //    {
        //        int kk = 0;
        //        OpenFileDialog dlg = new OpenFileDialog();

        //        OleDbConnection excelConnection = new OleDbConnection();
        //        OleDbConnection excelConnection1 = new OleDbConnection();

        //        ObservableCollection<ShipSpecificContent> ShipspecContent;

        //        // Set filter for file extension and default file extension
        //        dlg.DefaultExt = ".xlsx";
        //        dlg.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

        //        // Display OpenFileDialog by calling ShowDialog method
        //        Nullable<bool> result1 = dlg.ShowDialog();

        //        // Get the selected file name and display in a TextBox
        //        if (result1 == true)
        //        {
        //            ImportVisibles = true;
        //            RaisePropertyChanged("ImportVisibles");

        //            int ib = 1;
        //            _Import.ReportProgress(ib);
        //            IVisibles = "Visible";


        //            DataSet dataSet = null;
        //            dataSet = new DataSet("General");
        //            dataSet.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

        //            bool vesselcheck = false;
        //            string filename = dlg.FileName;
        //            int vesselid = sc.Vessels.Select(x => x.vessel_ID).FirstOrDefault();

        //            // string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=yes'", filename);
        //            //string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", filename);

        //            //OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt2"].ConnectionString);

        //            //OleDbConnection connection = new OleDbConnection();
        //            //connection.ConnectionString = excelConnectionString;

        //            //connection.Open();

        //            string filePath = filename;
        //            // SqlDatabase objdb = new SqlDatabase(OSMC.constring_Property);
        //            DataSet importdataset = new DataSet();
        //            //Open the Excel file using ClosedXML.
        //            // XLWorkbook theWorkBook = new XLWorkbook(fullfilename);
        //            int count = 1;



        //            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;Persist Security Info=False";
        //            excelConnection = new OleDbConnection(excelConnectionString);
        //            excelConnection.Open();
        //            System.Data.DataTable dt = new System.Data.DataTable();

        //            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //            if (dt == null)
        //            {
        //                // return null;
        //            }

        //            String[] excelSheets = new String[dt.Rows.Count];
        //            int t = 0;
        //            //excel data saves in temp file here.
        //            var dtrow = dt.Rows;
        //            foreach (DataRow row in dtrow)
        //            {
        //                excelSheets[t] = row["TABLE_NAME"].ToString();
        //                t++;
        //            }

        //            excelConnection1 = new OleDbConnection(excelConnectionString);
        //            System.Data.DataTable dtt;
        //            var exlength = excelSheets.Length;


        //            for (int i = 0; i < exlength; i++)
        //            {
        //                string importsheet = excelSheets[i].ToString();

        //                if (importsheet.TrimEnd('$') == "VesselDetails")
        //                {
        //                    int vesselIMO = sc.Vessels.Select(x => x.vessel_ID).FirstOrDefault();

        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "Vessel";
        //                    olda.Fill(dtt);

        //                    int IMOno = 0;
        //                    if (dtt.Rows.Count > 0)
        //                    {
        //                        IMOno = Convert.ToInt32(dtt.Rows[0]["ImoNo"]);

        //                        try
        //                        {
        //                            sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [MinimumRopeAndTailsSetting]");
        //                            sc.SaveChanges();

        //                        }
        //                        catch { }

        //                        try
        //                        {
        //                            MinimumRopeAndTailsClass obj = new MinimumRopeAndTailsClass();
        //                            if (dtt.Rows.Count > 0)
        //                            {
        //                                for (int j = 0; j < dtt.Rows.Count; j++)
        //                                {

        //                                    obj.MinimumRopes = Convert.ToInt32(dtt.Rows[j]["MinimumRopes"]);
        //                                    obj.MinimumRopeTails = Convert.ToInt32(dtt.Rows[j]["MinimumRopeTails"]);

        //                                    sc.MinimumRopeAndTails.Add(obj);
        //                                    sc.SaveChanges();

        //                                    sc.Entry(obj).State = EntityState.Detached;
        //                                }
        //                            }
        //                        }
        //                        catch { }

        //                    }

        //                    if (IMOno == vesselIMO)
        //                    {
        //                        kk = 0;
        //                    }
        //                    else
        //                    {
        //                        kk = 1;
        //                        MessageBox.Show("IMO Number in excel file not match with Database!", "DigiMoor Export/Import", MessageBoxButton.OK, MessageBoxImage.Information);
        //                        return;
        //                    }



        //                    //ShipSpecificContent obj = new ShipSpecificContent();
        //                    //if (dtt.Rows.Count > 0)
        //                    //{
        //                    //    for (int j = 0; j < dtt.Rows.Count; j++)
        //                    //    {
        //                    //        obj.Content = dtt.Rows[j]["Content"].ToString();
        //                    //        obj.MId = Convert.ToInt32(dtt.Rows[j]["MId"]);
        //                    //        obj.ShipId = dtt.Rows[j]["ShipId"].ToString();
        //                    //        obj.CreatedDate = Convert.ToDateTime(dtt.Rows[j]["Created_Date"]);

        //                    //        sc.ShipSpecificContents.Add(obj);
        //                    //        sc.SaveChanges();

        //                    //        sc.Entry(obj).State = EntityState.Detached;
        //                    //    }



        //                    //}
        //                }
        //            }

        //            for (int i = 0; i < exlength; i++)
        //            {
        //                string importsheet = excelSheets[i].ToString();

        //                if (importsheet.TrimEnd('$') == "ShipSpecificContent")
        //                {
        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "tblShipSpecificContent";
        //                    olda.Fill(dtt);


        //                    try
        //                    {
        //                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblShipSpecificContent]");
        //                        sc.SaveChanges();
        //                    }
        //                    catch { }

        //                    ShipSpecificContent obj = new ShipSpecificContent();
        //                    if (dtt.Rows.Count > 0)
        //                    {
        //                        for (int j = 0; j < dtt.Rows.Count; j++)
        //                        {
        //                            obj.Content = dtt.Rows[j]["Content"].ToString();
        //                            obj.MId = Convert.ToInt32(dtt.Rows[j]["MId"]);
        //                            obj.ShipId = dtt.Rows[j]["ShipId"].ToString();
        //                            obj.CreatedDate = Convert.ToDateTime(dtt.Rows[j]["Created_Date"]);

        //                            sc.ShipSpecificContents.Add(obj);
        //                            sc.SaveChanges();

        //                            sc.Entry(obj).State = EntityState.Detached;
        //                        }



        //                    }
        //                }

        //                if (importsheet.TrimEnd('$') == "PortList")
        //                {

        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "PortList";
        //                    olda.Fill(dtt);
        //                    InsertPortlist(importsheet.TrimEnd('$'), dtt);
        //                }

        //                if (importsheet.TrimEnd('$') == "ShipSpecificAttachments")
        //                {
        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "tblShipSpecificAttachment";
        //                    olda.Fill(dtt);


        //                    try
        //                    {
        //                        SqlDataAdapter adp1 = new SqlDataAdapter("delete from tblShipSpecificAttachment where type is null", sc.con);
        //                        System.Data.DataTable dtt1 = new System.Data.DataTable();
        //                        adp1.Fill(dtt1);
        //                    }
        //                    catch { }

        //                    //var mypath222 = @"C:\DigiMoorDB_Backup\Attachment";

        //                    string ServerName = StaticHelper.ServerName;
        //                    string mypath222 = ServerName + "\\DigiMoorDB_Backup\\Attachment";
        //                    //var mypath222 = @"C:\DigiMoorDB_Backup\Attachment";

        //                    if (!Directory.Exists(mypath222))
        //                    {
        //                        Directory.CreateDirectory(mypath222);
        //                    }

        //                    ShipSpecificAttachment obj = new ShipSpecificAttachment();


        //                    //  System.Data.DataTable dtt = new System.Data.DataTable();

        //                    if (dtt.Rows.Count > 0)
        //                    {
        //                        for (int j = 0; j < dtt.Rows.Count; j++)
        //                        {
        //                            obj.AttachmentName = dtt.Rows[j]["AttachmentName"].ToString();
        //                            var fileName = dtt.Rows[j]["Attachment"].ToString();


        //                            //var mypath1 =  ServerName + "\\DigiMoorDB_Backup\\Attachment" + "\\" + fileName;
        //                            obj.AttachmentPath = fileName;
        //                            obj.MId = Convert.ToInt32(dtt.Rows[j]["MID"]);
        //                            obj.ShipId = dtt.Rows[j]["ShipID"].ToString();
        //                            obj.CreateBy = dtt.Rows[j]["CreatedBy"].ToString();
        //                            obj.CreatedDate = Convert.ToDateTime(dtt.Rows[j]["CreatedDate"]);
        //                            obj.ModifiedBy = dtt.Rows[j]["ModifiedBy"].ToString();
        //                            obj.ModifiedDate = Convert.ToDateTime(dtt.Rows[j]["ModifiedDate"]);
        //                            obj.Type = "Shore";

        //                            sc.ShipSpecificAttachments.Add(obj);

        //                            sc.SaveChanges();

        //                            sc.Entry(obj).State = EntityState.Detached;

        //                            //filename = "";
        //                        }
        //                    }

        //                }

        //                if (importsheet.TrimEnd('$') == "SmartMenu")
        //                {

        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "tblsmartmenus";
        //                    olda.Fill(dtt);
        //                    try
        //                    {
        //                        SqlDataAdapter adp1 = new SqlDataAdapter("truncate table tblsmartmenus", sc.con);
        //                        System.Data.DataTable dtt1 = new System.Data.DataTable();
        //                        adp1.Fill(dtt1);
        //                    }
        //                    catch { }

        //                    SmartMenu obj = new SmartMenu();


        //                    // System.Data.DataTable dtt = new System.Data.DataTable();

        //                    if (dtt.Rows.Count > 0)
        //                    {
        //                        for (int j = 0; j < dtt.Rows.Count; j++)
        //                        {
        //                            //obj.SmartMenuContent = dtt.Rows[j]["SmartMenuContent"].ToString();
        //                            //obj.HtmlContent = dtt.Rows[j]["HtmlContent"].ToString();

        //                            obj.SmartMenuContent = dtt.Rows[j]["SmartMenuContentExport"].ToString();
        //                            //obj.HtmlContent = dtt.Rows[j]["HtmlContentExport"].ToString();

        //                            sc.SmartMenus.Add(obj);
        //                            sc.SaveChanges();

        //                            sc.Entry(obj).State = EntityState.Detached;
        //                        }



        //                    }

        //                }

        //                if (importsheet.TrimEnd('$') == "DocsPages")
        //                {

        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "docspages";
        //                    olda.Fill(dtt);
        //                    try
        //                    {
        //                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [docspages]");
        //                        sc.SaveChanges();
        //                    }
        //                    catch { }
        //                    DocsPages obj = new DocsPages();


        //                    // System.Data.DataTable dtt = new System.Data.DataTable();

        //                    if (dtt.Rows.Count > 0)
        //                    {
        //                        for (int j = 0; j < dtt.Rows.Count; j++)
        //                        {
        //                            obj.Content = dtt.Rows[j]["Content"].ToString();
        //                            obj.Mid = Convert.ToInt32(dtt.Rows[j]["MID"]);
        //                            obj.ShipId = dtt.Rows[j]["ShipID"].ToString();

        //                            obj.CreateBy = dtt.Rows[j]["CreatedBy"].ToString();
        //                            obj.CreatedDate = Convert.ToDateTime(dtt.Rows[j]["CreatedDate"]);
        //                            obj.ModifiedBy = dtt.Rows[j]["ModifiedBy"].ToString();
        //                            obj.ModifiedDate = Convert.ToDateTime(dtt.Rows[j]["ModifiedDate"]);

        //                            sc.DocsPages.Add(obj);
        //                            sc.SaveChanges();

        //                            sc.Entry(obj).State = EntityState.Detached;
        //                        }



        //                    }

        //                }

        //                if (importsheet.TrimEnd('$') == "RopeInspectionSettingsData")
        //                {
        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "tblropeinspectionsetting";
        //                    olda.Fill(dtt);
        //                    try
        //                    {
        //                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblropeinspectionsetting]");
        //                        sc.SaveChanges();
        //                    }
        //                    catch { }

        //                    RopeInspectionSettingClass obj = new RopeInspectionSettingClass();

        //                    // System.Data.DataTable dtt = new System.Data.DataTable();

        //                    if (dtt.Rows.Count > 0)
        //                    {
        //                        for (int j = 0; j < dtt.Rows.Count; j++)
        //                        {
        //                            obj.MooringRopeType = Convert.ToInt32(dtt.Rows[j]["MooringRopeType"]);
        //                            obj.ManufacturerType = Convert.ToInt32(dtt.Rows[j]["ManufacturerType"]);
        //                            obj.MaximumRunningHours = Convert.ToInt32(dtt.Rows[j]["MaximumRunningHours"]);
        //                            obj.MaximumMonthsAllowed = Convert.ToInt32(dtt.Rows[j]["MaximumMonthsAllowed"]);
        //                            obj.EndtoEndMonth = Convert.ToInt32(dtt.Rows[j]["EndToEndMonth"]);
        //                            obj.RotationOnWinches = Convert.ToInt32(dtt.Rows[j]["RotationOnWinches"]);

        //                            obj.Rating1 = Convert.ToDecimal(dtt.Rows[j]["Rating1"]);
        //                            obj.Rating2 = Convert.ToDecimal(dtt.Rows[j]["Rating2"]);
        //                            obj.Rating3 = Convert.ToDecimal(dtt.Rows[j]["Rating3"]);
        //                            obj.Rating4 = Convert.ToDecimal(dtt.Rows[j]["Rating4"]);
        //                            obj.Rating5 = Convert.ToDecimal(dtt.Rows[j]["Rating5"]);
        //                            obj.Rating6 = Convert.ToDecimal(dtt.Rows[j]["Rating6"]);
        //                            obj.Rating7 = Convert.ToDecimal(dtt.Rows[j]["Rating7"]);


        //                            sc.RopeInspectionSetting.Add(obj);
        //                            sc.SaveChanges();

        //                            sc.Entry(obj).State = EntityState.Detached;




        //                        }
        //                    }

        //                    updinspectionduedate();
        //                }

        //                if (importsheet.TrimEnd('$') == "MooringRopeType")
        //                {


        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "MooringRopeType";
        //                    olda.Fill(dtt);
        //                    try
        //                    {
        //                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [MooringRopeType]");
        //                        sc.SaveChanges();
        //                    }
        //                    catch { }

        //                    RopeTypeClass obj = new RopeTypeClass();

        //                    // System.Data.DataTable dtt = new System.Data.DataTable();

        //                    if (dtt.Rows.Count > 0)
        //                    {
        //                        for (int j = 0; j < dtt.Rows.Count; j++)
        //                        {
        //                            obj.Id = Convert.ToInt32(dtt.Rows[j]["Id"]);
        //                            obj.RopeType = (dtt.Rows[j]["RopeType"]).ToString();

        //                            sc.MooringRopeType.Add(obj);
        //                            sc.SaveChanges();

        //                            sc.Entry(obj).State = EntityState.Detached;
        //                        }
        //                    }
        //                }

        //                if (importsheet.TrimEnd('$') == "WinchRotationSettingsData")
        //                {
        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "tblwinchrotationsetting";
        //                    olda.Fill(dtt);
        //                    try
        //                    {
        //                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblwinchrotationsetting]");
        //                        sc.SaveChanges();
        //                    }
        //                    catch { }

        //                    WinchRotationSettingClass obj = new WinchRotationSettingClass();

        //                    // System.Data.DataTable dtt = new System.Data.DataTable();

        //                    if (dtt.Rows.Count > 0)
        //                    {
        //                        for (int j = 0; j < dtt.Rows.Count; j++)
        //                        {
        //                            obj.MooringRopeType = Convert.ToInt32(dtt.Rows[j]["MooringRopeType"]);
        //                            obj.ManufacturerType = Convert.ToInt32(dtt.Rows[j]["ManufacturerType"]);
        //                            obj.MaximumRunningHours = Convert.ToInt32(dtt.Rows[j]["MaximumRunningHours"]);
        //                            obj.MaximumMonthsAllowed = Convert.ToInt32(dtt.Rows[j]["MaximumMonthsAllowed"]);
        //                            obj.LeadFrom = dtt.Rows[j]["LeadFrom"].ToString();
        //                            obj.LeadTo = dtt.Rows[j]["LeadTo"].ToString();
        //                            sc.WinchRotationSetting.Add(obj);
        //                            sc.SaveChanges();

        //                            sc.Entry(obj).State = EntityState.Detached;
        //                        }
        //                    }
        //                }

        //                if (importsheet.TrimEnd('$') == "RopeTailInspectionSettingsData")
        //                {
        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "tblRopeTailInspectionSetting";
        //                    olda.Fill(dtt);
        //                    try
        //                    {
        //                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblRopeTailInspectionSetting]");
        //                        sc.SaveChanges();
        //                    }
        //                    catch { }

        //                    RopeTailInspectionSettingClass obj = new RopeTailInspectionSettingClass();

        //                    // System.Data.DataTable dtt = new System.Data.DataTable();
        //                    try
        //                    {
        //                        if (dtt.Rows.Count > 0)
        //                        {
        //                            for (int j = 0; j < dtt.Rows.Count; j++)
        //                            {
        //                                obj.MooringRopeType = Convert.ToInt32(dtt.Rows[j]["MooringRopeType"]);
        //                                obj.ManufacturerType = Convert.ToInt32(dtt.Rows[j]["ManufacturerType"]);
        //                                obj.MaximumRunningHours = Convert.ToInt32(dtt.Rows[j]["MaximumRunningHours"]);
        //                                obj.MaximumMonthsAllowed = Convert.ToInt32(dtt.Rows[j]["MaximumMonthsAllowed"]);
        //                                //obj.EndtoEndMonth = Convert.ToInt32(dtt.Rows[j]["EndToEndMonth"]);
        //                                //obj.RotationOnWinches = Convert.ToInt32(dtt.Rows[j]["RotationOnWinches"]);

        //                                obj.Rating1 = Convert.ToDecimal(dtt.Rows[j]["Rating1"]);
        //                                obj.Rating2 = Convert.ToDecimal(dtt.Rows[j]["Rating2"]);
        //                                obj.Rating3 = Convert.ToDecimal(dtt.Rows[j]["Rating3"]);
        //                                obj.Rating4 = Convert.ToDecimal(dtt.Rows[j]["Rating4"]);
        //                                obj.Rating5 = Convert.ToDecimal(dtt.Rows[j]["Rating5"]);
        //                                obj.Rating6 = Convert.ToDecimal(dtt.Rows[j]["Rating6"]);
        //                                obj.Rating7 = Convert.ToDecimal(dtt.Rows[j]["Rating7"]);


        //                                sc.RopeTailInspectionSetting.Add(obj);
        //                                sc.SaveChanges();

        //                                sc.Entry(obj).State = EntityState.Detached;
        //                            }
        //                        }
        //                        updinspectionduedatetail();
        //                    }
        //                    catch { }
        //                }

        //                if (importsheet.TrimEnd('$') == "LooseEquipInspectionSettings")
        //                {
        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "tblLooseEquipInspectionSetting";
        //                    olda.Fill(dtt);

        //                    try
        //                    {
        //                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblLooseEquipInspectionSetting]");
        //                        sc.SaveChanges();
        //                    }
        //                    catch { }

        //                    LooseEquipInspectionSettingClass obj = new LooseEquipInspectionSettingClass();

        //                    //System.Data.DataTable dtt = new System.Data.DataTable();

        //                    if (dtt.Rows.Count > 0)
        //                    {
        //                        for (int j = 0; j < dtt.Rows.Count; j++)
        //                        {
        //                            obj.EquipmentType = Convert.ToInt32(dtt.Rows[j]["EquipmentType"]);
        //                            obj.InspectionFrequency = Convert.ToInt32(dtt.Rows[j]["InspectionFrequency"]);
        //                            obj.MaximumRunningHours = Convert.ToInt32(dtt.Rows[j]["MaximumRunningHours"]);
        //                            obj.MaximumMonthsAllowed = Convert.ToInt32(dtt.Rows[j]["MaximumMonthsAllowed"]);



        //                            sc.LooseEInspectionSetting.Add(obj);
        //                            sc.SaveChanges();

        //                            sc.Entry(obj).State = EntityState.Detached;
        //                        }
        //                    }

        //                    updinsJShackle();
        //                    updinsRopeTailTable();
        //                    updinsChainStopper();
        //                    updinsChafeGuard();
        //                    updinsWinchTestKit();
        //                }

        //                if (importsheet.TrimEnd('$') == "RevisionData")
        //                {
        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "Revision";
        //                    olda.Fill(dtt);

        //                    try
        //                    {
        //                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [Revision]");
        //                        sc.SaveChanges();
        //                    }
        //                    catch { }

        //                    RevisionClass obj = new RevisionClass();

        //                    //System.Data.DataTable dtt = new System.Data.DataTable();

        //                    if (dtt.Rows.Count > 0)
        //                    {
        //                        for (int j = 0; j < dtt.Rows.Count; j++)
        //                        {
        //                            obj.RPrefix = (dtt.Rows[j]["RPrefix"]).ToString();

        //                            obj.RNumber = Convert.ToDecimal(dtt.Rows[j]["RNumber"]);
        //                            obj.ReviseDate = Convert.ToDateTime(dtt.Rows[j]["ReviseDate"]);
        //                            obj.ApproveDate = Convert.ToDateTime(dtt.Rows[j]["ApproveDate"]);
        //                            obj.CreateBy = (dtt.Rows[j]["CreatedBy"]).ToString();
        //                            obj.ApprovedBy = (dtt.Rows[j]["ApproveBy"]).ToString();
        //                            obj.Mid = Convert.ToInt32(dtt.Rows[j]["Mid"]);
        //                            obj.RevisionType = (dtt.Rows[j]["RevisionType"]).ToString();
        //                            obj.Content = null;
        //                            obj.ContentPath = (dtt.Rows[j]["ContentPath"]).ToString();
        //                            obj.Status = (dtt.Rows[j]["Status"]).ToString();
        //                            obj.RevisionText = (dtt.Rows[j]["RevisionText"]).ToString();
        //                            sc.Revisions.Add(obj);
        //                            sc.SaveChanges();

        //                            sc.Entry(obj).State = EntityState.Detached;
        //                        }
        //                    }
        //                }
        //                if (importsheet.TrimEnd('$') == "CommonTableData")
        //                {
        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "tblCommon";
        //                    olda.Fill(dtt);
        //                    try
        //                    {
        //                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblCommon]");
        //                        sc.SaveChanges();
        //                    }
        //                    catch { }

        //                    TblCommonClass obj = new TblCommonClass();

        //                    //System.Data.DataTable dtt = new System.Data.DataTable();

        //                    if (dtt.Rows.Count > 0)
        //                    {
        //                        for (int j = 0; j < dtt.Rows.Count; j++)
        //                        {
        //                            //obj.Id = Convert.ToInt32(dtt.Rows[j]["Id"]);
        //                            //obj.Name = (dtt.Rows[j]["Name"]).ToString();
        //                            //obj.Type = Convert.ToInt32(dtt.Rows[j]["Type"]);
        //                            //sc.CommonManuF.Add(obj);
        //                            //sc.SaveChanges();

        //                            int id = Convert.ToInt32(dtt.Rows[j]["Id"]);
        //                            string Name = (dtt.Rows[j]["Name"]).ToString();
        //                            int Type = Convert.ToInt32(dtt.Rows[j]["Type"]);


        //                            //SqlDataAdapter cmnadp = new SqlDataAdapter("insert into tblCommon values(@id,@name,@type)", sc.con);
        //                            SqlDataAdapter cmnadp = new SqlDataAdapter("insertTblCommon", sc.con);
        //                            cmnadp.SelectCommand.CommandType = CommandType.StoredProcedure;
        //                            cmnadp.SelectCommand.Parameters.AddWithValue("@id", id);
        //                            cmnadp.SelectCommand.Parameters.AddWithValue("@name", Name);
        //                            cmnadp.SelectCommand.Parameters.AddWithValue("@type", Type);
        //                            System.Data.DataTable dtcmn = new System.Data.DataTable();
        //                            cmnadp.Fill(dtcmn);

        //                            //sc.Entry(obj).State = EntityState.Detached;


        //                        }
        //                    }
        //                }

        //                if (importsheet.TrimEnd('$') == "NotificationCommentData")
        //                {
        //                    string query = string.Format("Select * from [{0}]", excelSheets[i]);

        //                    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
        //                    dtt = new System.Data.DataTable();
        //                    dtt.TableName = "NotificationComment";
        //                    olda.Fill(dtt);
        //                    NotificationCommentsClass obj = new NotificationCommentsClass();

        //                    //System.Data.DataTable dtt = new System.Data.DataTable();
        //                    try
        //                    {
        //                        if (dtt.Rows.Count > 0)
        //                        {
        //                            for (int j = 0; j < dtt.Rows.Count; j++)
        //                            {

        //                                int notiid = Convert.ToInt32(dtt.Rows[j]["NotificationId"]);
        //                                DateTime createdate = Convert.ToDateTime(dtt.Rows[j]["CreatedDate"]);
        //                                string Comments = (dtt.Rows[j]["Comments"]).ToString();
        //                                int CommentsType = Convert.ToInt32(dtt.Rows[j]["CommentsType"]);


        //                                sc.Database.ExecuteSqlCommand("Delete from NotificationComment where CommentsType = 2 and NotificationId = " + notiid + "");
        //                                sc.SaveChanges();



        //                                //SqlDataAdapter cmnadp = new SqlDataAdapter("insert into NotificationComment values(@NotificationId,@commentstype, @comments,@createddate,@createdby,@isactive,@isRead)", sc.con);
        //                                SqlDataAdapter cmnadp = new SqlDataAdapter("insertNotificationComment", sc.con);
        //                                cmnadp.SelectCommand.CommandType = CommandType.StoredProcedure;
        //                                cmnadp.SelectCommand.Parameters.AddWithValue("@NotificationId", notiid);
        //                                cmnadp.SelectCommand.Parameters.AddWithValue("@commentstype", CommentsType);
        //                                cmnadp.SelectCommand.Parameters.AddWithValue("@comments", Comments);
        //                                cmnadp.SelectCommand.Parameters.AddWithValue("@createddate", createdate);
        //                                cmnadp.SelectCommand.Parameters.AddWithValue("@createdby", "Admin");
        //                                cmnadp.SelectCommand.Parameters.AddWithValue("@isactive", true);
        //                                cmnadp.SelectCommand.Parameters.AddWithValue("@isRead", false);
        //                                System.Data.DataTable dtcmn = new System.Data.DataTable();
        //                                cmnadp.Fill(dtcmn);
        //                            }
        //                        }
        //                    }
        //                    catch (Exception ex) { }
        //                }
        //            }


        //            //using (XLWorkbook workBook = new XLWorkbook(filePath))
        //            //{
        //            //    //Read the first Sheet from Excel file.
        //            //    //int allshheets = workBook.Worksheet.
        //            //    //  string sheetname = workBook.Table
        //            //    int worksheetcount = workBook.Worksheets.Count;
        //            //    for (int s = 1; s <= worksheetcount; s++)
        //            //    {

        //            //        IXLWorksheet workSheet = workBook.Worksheet(s);
        //            //        var sheetName = workBook.Worksheet(s).Name;
        //            //        //Create a new DataTable.
        //            //        DataTable dtx = new DataTable();

        //            //        //Loop through the Worksheet rows.
        //            //        bool firstRow = true;
        //            //        foreach (IXLRow row in workSheet.Rows())
        //            //        {

        //            //            //Use the first row to add columns to DataTable.
        //            //            if (firstRow)
        //            //            {
        //            //                foreach (IXLCell cell in row.Cells())
        //            //                {
        //            //                    dtx.Columns.Add(cell.Value.ToString());
        //            //                }
        //            //                firstRow = false;
        //            //            }
        //            //            else
        //            //            {
        //            //                //Add rows to DataTable.
        //            //                dtx.Rows.Add();
        //            //                int i = 0;

        //            //                //var col = row.Cells(row.FirstCellUsed().Address.ColumnNumber.ToString());
        //            //                //var row121 = row.LastCellUsed().Address.ColumnNumber;
        //            //                foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
        //            //                {
        //            //                    dtx.Rows[dtx.Rows.Count - 1][i] = cell.Value.ToString();
        //            //                    i++;
        //            //                }
        //            //            }
        //            //        }

        //            //        // importdataset.Tables.Add(dtx);


        //            //        if (sheetName == "VesselDetails")
        //            //        {

        //            //            try
        //            //            {

        //            //                //System.Data.DataTable dtr = new System.Data.DataTable();

        //            //                var vesselIMO = dtx.Rows[0]["ImoNo"].ToString();
        //            //                int imono = Convert.ToInt32(vesselIMO);

        //            //                // DbDataReader dr = command.ExecuteReader();

        //            //                if (vesselid == imono)
        //            //                {
        //            //                    vesselcheck = true;
        //            //                }

        //            //            }
        //            //            catch { }

        //            //        }

        //            //        if (vesselcheck == true)
        //            //        {
        //            //            _Import.ReportProgress(ib + count);
        //            //            InsertImportedData(sheetName, dtx);


        //            //        }
        //            //        else
        //            //        {
        //            //            MessageBox.Show("IMO Number in excel file not match with Database!", "DigiMoor Export/Import", MessageBoxButton.OK, MessageBoxImage.Information);
        //            //            kk = 1;
        //            //        }



        //            //    }


        //            //}

        //            if (kk == 0)
        //            {
        //                MessageBox.Show("Import process has been completed!", "DigiMoor Export/Import", MessageBoxButton.OK, MessageBoxImage.Information);
        //            }


        //            //MessageBox.Show("Data Import Successfully", "Import/Export", MessageBoxButton.OK, MessageBoxImage.Information);

        //        }
        //    }
        //    catch (Exception ex) {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //}




        private void BrowseMethod(object sender, DoWorkEventArgs eb)
        {
            try
            {
                int kk = 0;
                OpenFileDialog dlg = new OpenFileDialog();

                ObservableCollection<ShipSpecificContent> ShipspecContent;

                // Set filter for file extension and default file extension
                dlg.DefaultExt = ".xlsx";
                dlg.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result1 = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox
                if (result1 == true)
                {
                    ImportVisibles = true;
                    RaisePropertyChanged("ImportVisibles");

                    int ib = 1;
                    _Import.ReportProgress(ib);
                    IVisibles = "Visible";


                    DataSet dataSet = null;
                    dataSet = new DataSet("General");
                    dataSet.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

                    bool vesselcheck = false;
                    string filename = dlg.FileName;
                    int vesselid = sc.Vessels.Select(x => x.vessel_ID).FirstOrDefault();

                    // string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=yes'", filename);
                    //string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", filename);

                   // OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt2"].ConnectionString);




                    //OleDbConnection connection = new OleDbConnection();
                    //connection.ConnectionString = excelConnectionString;

                   // connection.Open();

                    string filePath = filename;
                    // SqlDatabase objdb = new SqlDatabase(OSMC.constring_Property);
                    DataSet importdataset = new DataSet();
                    //Open the Excel file using ClosedXML.
                    // XLWorkbook theWorkBook = new XLWorkbook(fullfilename);
                    int count = 1;
                    using (XLWorkbook workBook = new XLWorkbook(filePath))
                    {
                        //Read the first Sheet from Excel file.
                        //int allshheets = workBook.Worksheet.
                        //  string sheetname = workBook.Table
                        int worksheetcount = workBook.Worksheets.Count;
                        for (int s = 1; s <= worksheetcount; s++)
                        {

                            IXLWorksheet workSheet = workBook.Worksheet(s);
                            var sheetName = workBook.Worksheet(s).Name;
                            //Create a new DataTable.
                            DataTable dtx = new DataTable();

                            //Loop through the Worksheet rows.
                            bool firstRow = true;
                            foreach (IXLRow row in workSheet.Rows())
                            {

                                //Use the first row to add columns to DataTable.
                                if (firstRow)
                                {
                                    foreach (IXLCell cell in row.Cells())
                                    {
                                        dtx.Columns.Add(cell.Value.ToString());
                                    }
                                    firstRow = false;
                                }
                                else
                                {
                                    //Add rows to DataTable.
                                    dtx.Rows.Add();
                                    int i = 0;

                                    //var col = row.Cells(row.FirstCellUsed().Address.ColumnNumber.ToString());
                                    //var row121 = row.LastCellUsed().Address.ColumnNumber;
                                    foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                                    {
                                        dtx.Rows[dtx.Rows.Count - 1][i] = cell.Value.ToString();
                                        i++;
                                    }
                                }
                            }

                            // importdataset.Tables.Add(dtx);


                            if (sheetName == "VesselDetails")
                            {

                                try
                                {



                                    //System.Data.DataTable dtr = new System.Data.DataTable();

                                    var vesselIMO = dtx.Rows[0]["ImoNo"].ToString();
                                    int imono = Convert.ToInt32(vesselIMO);

                                    // DbDataReader dr = command.ExecuteReader();

                                    if (vesselid == imono)
                                    {
                                        vesselcheck = true;
                                    }


                                    //if (sheetName == "PortList")
                                    //{

                                    //    string query = string.Format("Select * from [{0}]", excelSheets[i]);

                                    //    OleDbDataAdapter olda = new OleDbDataAdapter(query, excelConnection1);
                                    //    dtt = new System.Data.DataTable();
                                    //    dtt.TableName = "PortList";
                                    //    olda.Fill(dtt);
                                    //    InsertPortlist(importsheet.TrimEnd('$'), dtt);
                                    //}

                                }
                                catch { }

                            }

                            if (vesselcheck == true)
                            {
                                _Import.ReportProgress(ib + count);
                                InsertImportedData(sheetName, dtx);


                            }
                            else
                            {
                                MessageBox.Show("IMO Number in excel file not match with Database!", "DigiMoor Export/Import", MessageBoxButton.OK, MessageBoxImage.Information);
                                kk = 1;
                                break;
                            }



                        }


                    }

                    if (kk == 0)
                    {
                        MessageBox.Show("Import process has been completed!", "DigiMoor Export/Import", MessageBoxButton.OK, MessageBoxImage.Information);
                    }


                    //MessageBox.Show("Data Import Successfully", "Import/Export", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (Exception ex) {
               // MessageBox.Show(ex.Message.ToString());
                MessageBox.Show("Please Import DigimoorX7 file! This is not Valid file.", "DigiMoor Export/Import", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }





        private void InsertImportedData(string sheetName, System.Data.DataTable tbls)
        {
            try
            {
                System.Data.DataTable dtt = tbls;

                if (sheetName == "ShipSpecificContent")
                {
                    try
                    {
                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblShipSpecificContent]");
                        sc.SaveChanges();
                    }
                    catch { }

                    ShipSpecificContent obj = new ShipSpecificContent();

                    // System.Data.DataTable dtt = new System.Data.DataTable();

                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            obj.Content = dtt.Rows[j]["Content"].ToString();
                            obj.MId = Convert.ToInt32(dtt.Rows[j]["MId"]);
                            obj.ShipId = dtt.Rows[j]["ShipId"].ToString();
                            obj.CreatedDate = Convert.ToDateTime(dtt.Rows[j]["Created_Date"]);

                            sc.ShipSpecificContents.Add(obj);
                            sc.SaveChanges();

                            sc.Entry(obj).State = EntityState.Detached;
                        }



                    }
                }

                if (sheetName == "ShipSpecificAttachments")
                {
                    try
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("delete from tblShipSpecificAttachment where type is null", sc.con);
                        System.Data.DataTable dtt1 = new System.Data.DataTable();
                        adp1.Fill(dtt1);
                    }
                    catch { }


                    string ServerName = StaticHelper.ServerName;
                    string mypath222 = @"\\" + ServerName + "\\DigiMoorDB_Backup\\Attachment";
                    //var mypath222 = @"C:\DigiMoorDB_Backup\Attachment";

                    if (!Directory.Exists(mypath222))
                    {
                        Directory.CreateDirectory(mypath222);
                    }

                    ShipSpecificAttachment obj = new ShipSpecificAttachment();


                    //  System.Data.DataTable dtt = new System.Data.DataTable();

                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            obj.AttachmentName = dtt.Rows[j]["AttachmentName"].ToString();
                            var fileName = dtt.Rows[j]["Attachment"].ToString();


                          
                            string mypath1 = @"\\" + ServerName + "\\DigiMoorDB_Backup\\Attachment" + "\\" + fileName;
                            //var mypath1 = @"C:\DigiMoorDB_Backup\Attachment" + "\\" + fileName;

                            obj.AttachmentPath = mypath1;


                            obj.MId = Convert.ToInt32(dtt.Rows[j]["MID"]);
                            obj.ShipId = dtt.Rows[j]["ShipID"].ToString();
                            obj.CreateBy = dtt.Rows[j]["CreatedBy"].ToString();
                            obj.CreatedDate = Convert.ToDateTime(dtt.Rows[j]["CreatedDate"]);
                            obj.ModifiedBy = dtt.Rows[j]["ModifiedBy"].ToString();
                            obj.ModifiedDate = Convert.ToDateTime(dtt.Rows[j]["ModifiedDate"]);

                            sc.ShipSpecificAttachments.Add(obj);

                            sc.SaveChanges();

                            sc.Entry(obj).State = EntityState.Detached;

                            //filename = "";
                        }



                    }

                }

                if (sheetName == "SmartMenu")
                {
                    try
                    {
                        SqlDataAdapter adp1 = new SqlDataAdapter("truncate table tblsmartmenus", sc.con);
                        System.Data.DataTable dtt1 = new System.Data.DataTable();
                        adp1.Fill(dtt1);
                    }
                    catch { }

                    SmartMenu obj = new SmartMenu();


                    // System.Data.DataTable dtt = new System.Data.DataTable();

                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            //obj.SmartMenuContent = dtt.Rows[j]["SmartMenuContent"].ToString();
                            //obj.HtmlContent = dtt.Rows[j]["HtmlContent"].ToString();

                            obj.SmartMenuContent = dtt.Rows[j]["SmartMenuContentExport"].ToString();
                            //obj.HtmlContent = dtt.Rows[j]["HtmlContentExport"].ToString();

                            sc.SmartMenus.Add(obj);
                            sc.SaveChanges();

                            sc.Entry(obj).State = EntityState.Detached;
                        }



                    }

                }

                if (sheetName == "DocsPages")
                {
                    try
                    {
                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [docspages]");
                        sc.SaveChanges();
                    }
                    catch { }
                    DocsPages obj = new DocsPages();


                    // System.Data.DataTable dtt = new System.Data.DataTable();

                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            obj.Content = dtt.Rows[j]["Content"].ToString();
                            obj.Mid = Convert.ToInt32(dtt.Rows[j]["MID"]);
                            obj.ShipId = dtt.Rows[j]["ShipID"].ToString();

                            obj.CreateBy = dtt.Rows[j]["CreatedBy"].ToString();
                            obj.CreatedDate = Convert.ToDateTime(dtt.Rows[j]["CreatedDate"]);
                            obj.ModifiedBy = dtt.Rows[j]["ModifiedBy"].ToString();
                            obj.ModifiedDate = Convert.ToDateTime(dtt.Rows[j]["ModifiedDate"]);

                            sc.DocsPages.Add(obj);
                            sc.SaveChanges();

                            sc.Entry(obj).State = EntityState.Detached;
                        }



                    }

                }

                if (sheetName == "RopeInspectionSettingsData")
                {
                    try
                    {
                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblropeinspectionsetting]");
                        sc.SaveChanges();
                    }
                    catch { }

                    RopeInspectionSettingClass obj = new RopeInspectionSettingClass();

                    // System.Data.DataTable dtt = new System.Data.DataTable();

                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            obj.MooringRopeType = Convert.ToInt32(dtt.Rows[j]["MooringRopeType"]);
                            obj.ManufacturerType = Convert.ToInt32(dtt.Rows[j]["ManufacturerType"]);
                            obj.MaximumRunningHours = Convert.ToInt32(dtt.Rows[j]["MaximumRunningHours"]);
                            obj.MaximumMonthsAllowed = Convert.ToInt32(dtt.Rows[j]["MaximumMonthsAllowed"]);
                            obj.EndtoEndMonth = Convert.ToInt32(dtt.Rows[j]["EndToEndMonth"]);
                            obj.RotationOnWinches = Convert.ToInt32(dtt.Rows[j]["RotationOnWinches"]);

                            obj.Rating1 = Convert.ToDecimal(dtt.Rows[j]["Rating1"]);
                            obj.Rating2 = Convert.ToDecimal(dtt.Rows[j]["Rating2"]);
                            obj.Rating3 = Convert.ToDecimal(dtt.Rows[j]["Rating3"]);
                            obj.Rating4 = Convert.ToDecimal(dtt.Rows[j]["Rating4"]);
                            obj.Rating5 = Convert.ToDecimal(dtt.Rows[j]["Rating5"]);
                            obj.Rating6 = Convert.ToDecimal(dtt.Rows[j]["Rating6"]);
                            obj.Rating7 = Convert.ToDecimal(dtt.Rows[j]["Rating7"]);


                            sc.RopeInspectionSetting.Add(obj);
                            sc.SaveChanges();

                            sc.Entry(obj).State = EntityState.Detached;




                        }
                    }

                    updinspectionduedate();
                }

                if (sheetName == "RopeTailInspectionSettingsData")
                {
                    try
                    {
                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblRopeTailInspectionSetting]");
                        sc.SaveChanges();
                    }
                    catch { }

                    RopeTailInspectionSettingClass obj = new RopeTailInspectionSettingClass();

                    // System.Data.DataTable dtt = new System.Data.DataTable();
                    try
                    {
                        if (dtt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtt.Rows.Count; j++)
                            {
                                obj.MooringRopeType = Convert.ToInt32(dtt.Rows[j]["MooringRopeType"]);
                                obj.ManufacturerType = Convert.ToInt32(dtt.Rows[j]["ManufacturerType"]);
                                obj.MaximumRunningHours = Convert.ToInt32(dtt.Rows[j]["MaximumRunningHours"]);
                                obj.MaximumMonthsAllowed = Convert.ToInt32(dtt.Rows[j]["MaximumMonthsAllowed"]);
                                //obj.EndtoEndMonth = Convert.ToInt32(dtt.Rows[j]["EndToEndMonth"]);
                                //obj.RotationOnWinches = Convert.ToInt32(dtt.Rows[j]["RotationOnWinches"]);

                                obj.Rating1 = Convert.ToDecimal(dtt.Rows[j]["Rating1"]);
                                obj.Rating2 = Convert.ToDecimal(dtt.Rows[j]["Rating2"]);
                                obj.Rating3 = Convert.ToDecimal(dtt.Rows[j]["Rating3"]);
                                obj.Rating4 = Convert.ToDecimal(dtt.Rows[j]["Rating4"]);
                                obj.Rating5 = Convert.ToDecimal(dtt.Rows[j]["Rating5"]);
                                obj.Rating6 = Convert.ToDecimal(dtt.Rows[j]["Rating6"]);
                                obj.Rating7 = Convert.ToDecimal(dtt.Rows[j]["Rating7"]);


                                sc.RopeTailInspectionSetting.Add(obj);
                                sc.SaveChanges();

                                sc.Entry(obj).State = EntityState.Detached;
                            }
                        }
                        updinspectionduedatetail();
                    }
                    catch { }
                }
                if (sheetName == "LooseEquipInspectionSettings")
                {
                    try
                    {
                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblLooseEquipInspectionSetting]");
                        sc.SaveChanges();
                    }
                    catch { }

                    LooseEquipInspectionSettingClass obj = new LooseEquipInspectionSettingClass();

                    //System.Data.DataTable dtt = new System.Data.DataTable();

                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            obj.EquipmentType = Convert.ToInt32(dtt.Rows[j]["EquipmentType"]);
                            obj.InspectionFrequency = Convert.ToInt32(dtt.Rows[j]["InspectionFrequency"]);
                            obj.MaximumRunningHours = Convert.ToInt32(dtt.Rows[j]["MaximumRunningHours"]);
                            obj.MaximumMonthsAllowed = Convert.ToInt32(dtt.Rows[j]["MaximumMonthsAllowed"]);



                            sc.LooseEInspectionSetting.Add(obj);
                            sc.SaveChanges();

                            sc.Entry(obj).State = EntityState.Detached;
                        }
                    }

                    updinsJShackle();
                    updinsRopeTailTable();
                    updinsChainStopper();
                    updinsChafeGuard();
                    updinsWinchTestKit();
                }

                if (sheetName == "tblShipSpecificContent")
                {
                    try
                    {
                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblcommon]");
                        sc.SaveChanges();
                    }
                    catch { }

                    TblCommonClass obj = new TblCommonClass();

                    //System.Data.DataTable dtt = new System.Data.DataTable();

                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            obj.Name = (dtt.Rows[j]["Name"]).ToString();

                            obj.Type = Convert.ToInt32(dtt.Rows[j]["Type"]);

                            sc.CommonManuF.Add(obj);
                            sc.SaveChanges();

                            sc.Entry(obj).State = EntityState.Detached;
                        }
                    }
                }

                if (sheetName == "MooringRopeType")
                {
                    try
                    {
                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [MooringRopeType]");
                        sc.SaveChanges();
                    }
                    catch { }
                    RopeTypeClass obj = new RopeTypeClass(); 
                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            obj.Id = Convert.ToInt32(dtt.Rows[j]["Id"]);
                            obj.RopeType = (dtt.Rows[j]["RopeType"]).ToString();
                          
                            sc.MooringRopeType.Add(obj);
                            sc.SaveChanges();
                            sc.Entry(obj).State = EntityState.Detached;
                        }
                    }
                }
                if (sheetName == "RevisionData")
                {
                    try
                    {
                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [Revision]");
                        sc.SaveChanges();
                    }
                    catch { }

                    RevisionClass obj = new RevisionClass();

                    //System.Data.DataTable dtt = new System.Data.DataTable();

                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            obj.RPrefix = (dtt.Rows[j]["RPrefix"]).ToString();

                            obj.RNumber = Convert.ToDecimal(dtt.Rows[j]["RNumber"]);
                            obj.ReviseDate = Convert.ToDateTime(dtt.Rows[j]["ReviseDate"]);
                            obj.ApproveDate = Convert.ToDateTime(dtt.Rows[j]["ApproveDate"]);
                            obj.CreateBy = (dtt.Rows[j]["CreatedBy"]).ToString();
                            obj.ApprovedBy = (dtt.Rows[j]["ApproveBy"]).ToString();
                            obj.Mid = Convert.ToInt32(dtt.Rows[j]["Mid"]);
                            obj.RevisionType = (dtt.Rows[j]["RevisionType"]).ToString();
                            obj.Content = null;
                            obj.ContentPath = (dtt.Rows[j]["ContentPath"]).ToString();
                            obj.Status = (dtt.Rows[j]["Status"]).ToString();
                            obj.RevisionText = (dtt.Rows[j]["RevisionText"]).ToString();
                            sc.Revisions.Add(obj);
                            sc.SaveChanges();

                            sc.Entry(obj).State = EntityState.Detached;
                        }
                    }
                }
                if (sheetName == "CommonTableData")
                {
                    try
                    {
                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblCommon]");
                        sc.SaveChanges();
                    }
                    catch { }

                    TblCommonClass obj = new TblCommonClass();

                    //System.Data.DataTable dtt = new System.Data.DataTable();

                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            //obj.Id = Convert.ToInt32(dtt.Rows[j]["Id"]);
                            //obj.Name = (dtt.Rows[j]["Name"]).ToString();
                            //obj.Type = Convert.ToInt32(dtt.Rows[j]["Type"]);
                            //sc.CommonManuF.Add(obj);
                            //sc.SaveChanges();

                            int id = Convert.ToInt32(dtt.Rows[j]["Id"]);
                            string Name = (dtt.Rows[j]["Name"]).ToString();
                            int Type = Convert.ToInt32(dtt.Rows[j]["Type"]);


                            //SqlDataAdapter cmnadp = new SqlDataAdapter("insert into tblCommon values(@id,@name,@type)", sc.con);
                            SqlDataAdapter cmnadp = new SqlDataAdapter("insertTblCommon", sc.con);
                            cmnadp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            cmnadp.SelectCommand.Parameters.AddWithValue("@id", id);
                            cmnadp.SelectCommand.Parameters.AddWithValue("@name", Name);
                            cmnadp.SelectCommand.Parameters.AddWithValue("@type", Type);
                            System.Data.DataTable dtcmn = new System.Data.DataTable();
                            cmnadp.Fill(dtcmn);

                            //sc.Entry(obj).State = EntityState.Detached;


                        }
                    }
                }

                if (sheetName == "NotificationCommentData")
                {

                    NotificationCommentsClass obj = new NotificationCommentsClass();

                    //System.Data.DataTable dtt = new System.Data.DataTable();
                    try
                    {
                        if (dtt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtt.Rows.Count; j++)
                            {

                                int notiid = Convert.ToInt32(dtt.Rows[j]["NotificationId"]);
                                DateTime createdate = Convert.ToDateTime(dtt.Rows[j]["CreatedDate"]);
                                string Comments = (dtt.Rows[j]["Comments"]).ToString();
                                int CommentsType = Convert.ToInt32(dtt.Rows[j]["CommentsType"]);


                                sc.Database.ExecuteSqlCommand("Delete from NotificationComment where CommentsType = 2 and NotificationId = " + notiid + "");
                                sc.SaveChanges();



                                //SqlDataAdapter cmnadp = new SqlDataAdapter("insert into NotificationComment values(@NotificationId,@commentstype, @comments,@createddate,@createdby,@isactive,@isRead)", sc.con);
                                SqlDataAdapter cmnadp = new SqlDataAdapter("insertNotificationComment", sc.con);
                                cmnadp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                cmnadp.SelectCommand.Parameters.AddWithValue("@NotificationId", notiid);
                                cmnadp.SelectCommand.Parameters.AddWithValue("@commentstype", CommentsType);
                                cmnadp.SelectCommand.Parameters.AddWithValue("@comments", Comments);
                                cmnadp.SelectCommand.Parameters.AddWithValue("@createddate", createdate);
                                cmnadp.SelectCommand.Parameters.AddWithValue("@createdby", "Admin");
                                cmnadp.SelectCommand.Parameters.AddWithValue("@isactive", true);
                                cmnadp.SelectCommand.Parameters.AddWithValue("@isRead", false);
                                System.Data.DataTable dtcmn = new System.Data.DataTable();
                                cmnadp.Fill(dtcmn);
                            }
                        }
                    }
                    catch (Exception ex) { }
                }

                if (sheetName == "WinchRotationSettingsData")
                {
                    try
                    {
                        sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblWinchRotationSetting]");
                        sc.SaveChanges();
                    }
                    catch { }

                    WinchRotationSettingClass obj = new WinchRotationSettingClass();

                    //System.Data.DataTable dtt = new System.Data.DataTable();

                    if (dtt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtt.Rows.Count; j++)
                        {
                            obj.MooringRopeType = Convert.ToInt32(dtt.Rows[j]["MooringRopeType"]);
                            obj.ManufacturerType = Convert.ToInt32(dtt.Rows[j]["ManufacturerType"]);
                            obj.MaximumRunningHours = Convert.ToInt32(dtt.Rows[j]["MaximumRunningHours"]);
                            obj.MaximumMonthsAllowed = Convert.ToInt32(dtt.Rows[j]["MaximumMonthsAllowed"]);
                            obj.LeadFrom = dtt.Rows[j]["LeadFrom"].ToString();
                            obj.LeadTo = dtt.Rows[j]["LeadTo"].ToString();



                            sc.WinchRotationSetting.Add(obj);
                            sc.SaveChanges();

                            sc.Entry(obj).State = EntityState.Detached;
                        }
                    }

                   
                }


                sc.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error in inserting data in Database !", "DigiMoor Export/Import", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }

        //public void UpdateEntity(T entity, bool isDetectChange = false)
        //{
        //    if (isDetectChange)
        //    {

        //        this.sc.ChangeTracker.DetectChanges();
        //    }

        //    this.sc.Entry(entity).State = EntityState.Modified;
        //}

        #region InspectionDueDate Update Method 


        private void updinsJShackle()
        {
            try
            {
                SqlDataAdapter mrp = new SqlDataAdapter("select * from JoiningShackle where deletestatus=0 and dateinstalled is not null and outofservicedate is null", sc.con);
                System.Data.DataTable dtt15 = new System.Data.DataTable();
                mrp.Fill(dtt15);

                //if (dtt1.Rows.Count > 0)
                for (int k = 0; k < dtt15.Rows.Count; k++)
                {

                    SqlDataAdapter jjj = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType = 1", sc.con);
                    System.Data.DataTable dtt1 = new System.Data.DataTable();
                    jjj.Fill(dtt1);

                    if (dtt1.Rows.Count > 0)
                    {

                        //int ropetypeid = Convert.ToInt32(dtt1.Rows[0]["MaxRunningHours"]);
                        //DateTime insduedt = Convert.ToDateTime(dtt1.Rows[0]["InspectionDueDate"]);
                        int ropeid = Convert.ToInt32(dtt15.Rows[k]["id"]);

                        //DateTime notidueMonth = Convert.ToDateTime(insduedt).AddMonths(near);

                        SqlDataAdapter mrp1 = new SqlDataAdapter("select MAX(Inspectdate) as Inspectdate from MooringLooseEquipInspection where LooseETypeId= 1", sc.con);
                        System.Data.DataTable dtt11 = new System.Data.DataTable();
                        mrp1.Fill(dtt11);
                        if (dtt11.Rows.Count > 0)
                        {
                            var insdt = dtt11.Rows[0][0] == DBNull.Value ? null : dtt11.Rows[0][0];

                            if (insdt != null)
                            {                               
                                decimal perchk = Convert.ToDecimal(dtt1.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                perchk = perchk * 100;
                                //near = Convert.ToInt32(rtc.Rows[0][0]);
                                int near = Convert.ToInt32(perchk);
                                //near = Convert.ToInt32(rtc.Rows[0][0]);
                                // }

                                DateTime notidueMonth = Convert.ToDateTime(dtt11.Rows[0]["InspectDate"]).AddDays(near);

                                var result = sc.JoiningShackles.SingleOrDefault(b => b.LooseETypeId == 1 && b.Id == ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate == null);
                                if (result != null)
                                {
                                    result.InspectionDueDate = notidueMonth;
                                    result.ModifiedBy = "Admin";
                                    result.ModifiedDate = DateTime.Now;
                                    sc.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            var insdt5 = dtt15.Rows[k]["DateInstalled"] == DBNull.Value ? null : dtt15.Rows[k]["DateInstalled"];

                            //string chk = insdt.ToString();
                            //if (chk != "01/01/0001 00:00:00" || chk != null)
                            if (insdt5 != null)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType= 1", sc.con);
                                System.Data.DataTable dt = new System.Data.DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    decimal rating1 = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]);
                                    int rat = Convert.ToInt32(rating1);

                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    DateTime inspectduedate = Convert.ToDateTime(insdt5).AddDays(near);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }

                                    //moorwinch.InspectionDueDate = inspectduedate;


                                    var result = sc.JoiningShackles.SingleOrDefault(b => b.LooseETypeId == 1 && b.Id == ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate == null);
                                    if (result != null)
                                    {
                                        result.InspectionDueDate = inspectduedate;
                                        result.ModifiedBy = "Admin";
                                        result.ModifiedDate = DateTime.Now;
                                        sc.SaveChanges();
                                    }
                                }
                            }
                        }
                    }


                }
            }
            catch (Exception ex) { }
        }

        private void updinsRopeTailTable()
        {
            try
            {
                SqlDataAdapter mrp = new SqlDataAdapter("select * from RopeTail where deletestatus=0 and installeddate is not null and outofservicedate is null", sc.con);
                System.Data.DataTable dtt15 = new System.Data.DataTable();
                mrp.Fill(dtt15);

                //if (dtt1.Rows.Count > 0)
                for (int k = 0; k < dtt15.Rows.Count; k++)
                {
                    int looseetypeid = Convert.ToInt32(dtt15.Rows[k]["LooseETypeId"]);

                    SqlDataAdapter jjj = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType = " + looseetypeid + "", sc.con);
                    System.Data.DataTable dtt1 = new System.Data.DataTable();
                    jjj.Fill(dtt1);

                    if (dtt1.Rows.Count > 0)
                    {

                        //int ropetypeid = Convert.ToInt32(dtt1.Rows[0]["MaxRunningHours"]);
                        //DateTime insduedt = Convert.ToDateTime(dtt1.Rows[0]["InspectionDueDate"]);
                        int ropeid = Convert.ToInt32(dtt15.Rows[k]["id"]);

                        //DateTime notidueMonth = Convert.ToDateTime(insduedt).AddMonths(near);

                        SqlDataAdapter mrp1 = new SqlDataAdapter("select MAX(Inspectdate) as Inspectdate from MooringLooseEquipInspection where LooseETypeId=" + looseetypeid + "", sc.con);
                        System.Data.DataTable dtt11 = new System.Data.DataTable();
                        mrp1.Fill(dtt11);
                        if (dtt11.Rows.Count > 0)
                        {
                            var insdt = dtt11.Rows[0][0] == DBNull.Value ? null : dtt11.Rows[0][0];

                            if (insdt != null)
                            {
                                decimal perchk = Convert.ToDecimal(dtt1.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                perchk = perchk * 100;
                                //near = Convert.ToInt32(rtc.Rows[0][0]);
                                int near = Convert.ToInt32(perchk);
                                //near = Convert.ToInt32(rtc.Rows[0][0]);
                                // }

                                DateTime notidueMonth = Convert.ToDateTime(dtt11.Rows[0]["InspectDate"]).AddDays(near);

                                var result = sc.RopeTails.SingleOrDefault(b => b.LooseETypeId == looseetypeid && b.Id == ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate == null);
                                if (result != null)
                                {
                                    result.InspectionDueDate = notidueMonth;
                                    result.ModifiedBy = "Admin";
                                    result.ModifiedDate = DateTime.Now;
                                    sc.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            var insdt5 = dtt15.Rows[k]["InstalledDate"] == DBNull.Value ? null : dtt15.Rows[k]["InstalledDate"];

                            //string chk = insdt.ToString();
                            //if (chk != "01/01/0001 00:00:00" || chk != null)
                            if (insdt5 != null)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType= "+ looseetypeid + "", sc.con);
                                System.Data.DataTable dt = new System.Data.DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    //decimal rating1 = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]);
                                    //int rat = Convert.ToInt32(rating1);

                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    DateTime inspectduedate = Convert.ToDateTime(insdt5).AddDays(near);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }

                                    //moorwinch.InspectionDueDate = inspectduedate;


                                    var result = sc.RopeTails.SingleOrDefault(b => b.LooseETypeId == looseetypeid && b.Id==ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate == null);
                                    if (result != null)
                                    {
                                        result.InspectionDueDate = inspectduedate;
                                        result.ModifiedBy = "Admin";
                                        result.ModifiedDate = DateTime.Now;
                                        sc.SaveChanges();
                                    }
                                }
                            }
                        }
                    }


                }
            }
            catch (Exception ex) { }
        }

        private void updinsChainStopper()
        {
            try
            {
                SqlDataAdapter mrp = new SqlDataAdapter("select * from ChainStopper where deletestatus=0 and dateinstalled is not null and outofservicedate is null", sc.con);
                System.Data.DataTable dtt15 = new System.Data.DataTable();
                mrp.Fill(dtt15);

                //if (dtt1.Rows.Count > 0)
                for (int k = 0; k < dtt15.Rows.Count; k++)
                {

                    SqlDataAdapter jjj = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType = 5", sc.con);
                    System.Data.DataTable dtt1 = new System.Data.DataTable();
                    jjj.Fill(dtt1);

                    if (dtt1.Rows.Count > 0)
                    {

                        //int ropetypeid = Convert.ToInt32(dtt1.Rows[0]["MaxRunningHours"]);
                        //DateTime insduedt = Convert.ToDateTime(dtt1.Rows[0]["InspectionDueDate"]);
                        int ropeid = Convert.ToInt32(dtt15.Rows[k]["id"]);

                        //DateTime notidueMonth = Convert.ToDateTime(insduedt).AddMonths(near);

                        SqlDataAdapter mrp1 = new SqlDataAdapter("select MAX(Inspectdate) as Inspectdate from MooringLooseEquipInspection where LooseETypeId= 5", sc.con);
                        System.Data.DataTable dtt11 = new System.Data.DataTable();
                        mrp1.Fill(dtt11);
                        if (dtt11.Rows.Count > 0)
                        {
                            var insdt = dtt11.Rows[0][0] == DBNull.Value ? null : dtt11.Rows[0][0];

                            if (insdt != null)
                            {
                                decimal perchk = Convert.ToDecimal(dtt1.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                perchk = perchk * 100;
                                //near = Convert.ToInt32(rtc.Rows[0][0]);
                                int near = Convert.ToInt32(perchk);
                                //near = Convert.ToInt32(rtc.Rows[0][0]);
                                // }

                                DateTime notidueMonth = Convert.ToDateTime(dtt11.Rows[0]["InspectDate"]).AddDays(near);

                                var result = sc.ChainStoppers.SingleOrDefault(b => b.LooseETypeId == 5  && b.Id==ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate == null);
                                if (result != null)
                                {
                                    result.InspectionDueDate = notidueMonth;
                                    result.ModifiedBy = "Admin";
                                    result.ModifiedDate = DateTime.Now;
                                    sc.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            var insdt5 = dtt15.Rows[k]["DateInstalled"] == DBNull.Value ? null : dtt15.Rows[k]["DateInstalled"];

                            //string chk = insdt.ToString();
                            //if (chk != "01/01/0001 00:00:00" || chk != null)
                            if (insdt5 != null)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType= 5", sc.con);
                                System.Data.DataTable dt = new System.Data.DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    //decimal rating1 = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]);
                                    //int rat = Convert.ToInt32(rating1);

                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    DateTime inspectduedate = Convert.ToDateTime(insdt5).AddDays(near);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }

                                    //moorwinch.InspectionDueDate = inspectduedate;


                                    var result = sc.ChainStoppers.SingleOrDefault(b => b.LooseETypeId == 5 && b.Id== ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate == null);
                                    if (result != null)
                                    {
                                        result.InspectionDueDate = inspectduedate;
                                        result.ModifiedBy = "Admin";
                                        result.ModifiedDate = DateTime.Now;
                                        sc.SaveChanges();
                                    }
                                }
                            }
                        }
                    }


                }
            }
            catch (Exception ex) { }
        }

        private void updinsChafeGuard()
        {
            try
            {
                SqlDataAdapter mrp = new SqlDataAdapter("select * from ChafeGuard where deletestatus=0 and installeddate is not null and outofservicedate is null", sc.con);
                System.Data.DataTable dtt15 = new System.Data.DataTable();
                mrp.Fill(dtt15);

                //if (dtt1.Rows.Count > 0)
                for (int k = 0; k < dtt15.Rows.Count; k++)
                {

                    SqlDataAdapter jjj = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType = 7", sc.con);
                    System.Data.DataTable dtt1 = new System.Data.DataTable();
                    jjj.Fill(dtt1);

                    if (dtt1.Rows.Count > 0)
                    {

                        //int ropetypeid = Convert.ToInt32(dtt1.Rows[0]["MaxRunningHours"]);
                        //DateTime insduedt = Convert.ToDateTime(dtt1.Rows[0]["InspectionDueDate"]);
                        int ropeid = Convert.ToInt32(dtt15.Rows[k]["id"]);

                        //DateTime notidueMonth = Convert.ToDateTime(insduedt).AddMonths(near);

                        SqlDataAdapter mrp1 = new SqlDataAdapter("select MAX(Inspectdate) as Inspectdate from MooringLooseEquipInspection where LooseETypeId= 7", sc.con);
                        System.Data.DataTable dtt11 = new System.Data.DataTable();
                        mrp1.Fill(dtt11);
                        if (dtt11.Rows.Count > 0)
                        {
                            var insdt = dtt11.Rows[0][0] == DBNull.Value ? null : dtt11.Rows[0][0];

                            if (insdt != null)
                            {
                                decimal perchk = Convert.ToDecimal(dtt1.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                perchk = perchk * 100;
                                //near = Convert.ToInt32(rtc.Rows[0][0]);
                                int near = Convert.ToInt32(perchk);
                                //near = Convert.ToInt32(rtc.Rows[0][0]);
                                // }

                                DateTime notidueMonth = Convert.ToDateTime(dtt11.Rows[0]["InspectDate"]).AddDays(near);

                                var result = sc.ChafeGuard.SingleOrDefault(b => b.Id == ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate == null);
                                if (result != null)
                                {
                                    result.InspectionDueDate = notidueMonth;                                   
                                    sc.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            var insdt5 = dtt15.Rows[k]["InstalledDate"] == DBNull.Value ? null : dtt15.Rows[k]["InstalledDate"];

                            //string chk = insdt.ToString();
                            //if (chk != "01/01/0001 00:00:00" || chk != null)
                            if (insdt5 != null)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType= 7", sc.con);
                                System.Data.DataTable dt = new System.Data.DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    //decimal rating1 = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]);
                                    //int rat = Convert.ToInt32(rating1);

                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    DateTime inspectduedate = Convert.ToDateTime(insdt5).AddDays(near);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }

                                    //moorwinch.InspectionDueDate = inspectduedate;


                                    var result = sc.ChafeGuard.SingleOrDefault(b => b.Id == ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate == null);
                                    if (result != null)
                                    {
                                        result.InspectionDueDate = inspectduedate;                                    
                                        sc.SaveChanges();
                                    }
                                }
                            }
                        }
                    }


                }
            }
            catch (Exception ex) { }
        }

        private void updinsWinchTestKit()
        {
            try
            {
                SqlDataAdapter mrp = new SqlDataAdapter("select * from WinchBreakTestKit where deletestatus=0 and installeddate is not null and outofservicedate is null", sc.con);
                System.Data.DataTable dtt15 = new System.Data.DataTable();
                mrp.Fill(dtt15);

                //if (dtt1.Rows.Count > 0)
                for (int k = 0; k < dtt15.Rows.Count; k++)
                {

                    SqlDataAdapter jjj = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType = 8", sc.con);
                    System.Data.DataTable dtt1 = new System.Data.DataTable();
                    jjj.Fill(dtt1);

                    if (dtt1.Rows.Count > 0)
                    {

                        //int ropetypeid = Convert.ToInt32(dtt1.Rows[0]["MaxRunningHours"]);
                        //DateTime insduedt = Convert.ToDateTime(dtt1.Rows[0]["InspectionDueDate"]);
                        int ropeid = Convert.ToInt32(dtt15.Rows[k]["id"]);

                        //DateTime notidueMonth = Convert.ToDateTime(insduedt).AddMonths(near);

                        SqlDataAdapter mrp1 = new SqlDataAdapter("select MAX(Inspectdate) as Inspectdate from MooringLooseEquipInspection where LooseETypeId= 8", sc.con);
                        System.Data.DataTable dtt11 = new System.Data.DataTable();
                        mrp1.Fill(dtt11);
                        if (dtt11.Rows.Count > 0)
                        {
                            var insdt = dtt11.Rows[0][0] == DBNull.Value ? null : dtt11.Rows[0][0];

                            if (insdt != null)
                            {
                                decimal perchk = Convert.ToDecimal(dtt1.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                perchk = perchk * 100;
                                //near = Convert.ToInt32(rtc.Rows[0][0]);
                                int near = Convert.ToInt32(perchk);
                                //near = Convert.ToInt32(rtc.Rows[0][0]);
                                // }

                                DateTime notidueMonth = Convert.ToDateTime(dtt11.Rows[0]["InspectDate"]).AddDays(near);

                                var result = sc.WBTestKit.SingleOrDefault(b => b.Id == ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate == null);
                                if (result != null)
                                {
                                    result.InspectionDueDate = notidueMonth;
                                    sc.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            var insdt5 = dtt15.Rows[k]["InstalledDate"] == DBNull.Value ? null : dtt15.Rows[k]["InstalledDate"];

                            //string chk = insdt.ToString();
                            //if (chk != "01/01/0001 00:00:00" || chk != null)
                            if (insdt5 != null)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType= 8", sc.con);
                                System.Data.DataTable dt = new System.Data.DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    //decimal rating1 = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]);
                                    //int rat = Convert.ToInt32(rating1);

                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    DateTime inspectduedate = Convert.ToDateTime(insdt5).AddDays(near);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }

                                    //moorwinch.InspectionDueDate = inspectduedate;


                                    var result = sc.WBTestKit.SingleOrDefault(b => b.Id == ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate == null);
                                    if (result != null)
                                    {
                                        result.InspectionDueDate = inspectduedate;
                                        sc.SaveChanges();
                                    }
                                }
                            }
                        }
                    }


                }
            }
            catch (Exception ex) { }
        }

        private void updinspectionduedatetail()
        {
            try
            {
                SqlDataAdapter mrp = new SqlDataAdapter("select * from MooringRopeDetail where deletestatus=0 and installeddate is not null and outofservicedate is null and ropetail=1", sc.con);
                System.Data.DataTable dtt15 = new System.Data.DataTable();
                mrp.Fill(dtt15);

                //if (dtt1.Rows.Count > 0)
                for (int k = 0; k < dtt15.Rows.Count; k++)
                {

                    SqlDataAdapter jjj = new SqlDataAdapter("select * from tblRopeTailInspectionSetting where MooringRopeType=" + Convert.ToInt32(dtt15.Rows[k]["RopeTypeId"]) + " and ManufacturerType=" + Convert.ToInt32(dtt15.Rows[k]["ManufacturerId"]) + "", sc.con);
                    System.Data.DataTable dtt1 = new System.Data.DataTable();
                    jjj.Fill(dtt1);

                    if (dtt1.Rows.Count > 0)
                    {

                        //int ropetypeid = Convert.ToInt32(dtt1.Rows[0]["MaxRunningHours"]);
                        //DateTime insduedt = Convert.ToDateTime(dtt1.Rows[0]["InspectionDueDate"]);
                        int ropeid = Convert.ToInt32(dtt15.Rows[k]["id"]);

                        //DateTime notidueMonth = Convert.ToDateTime(insduedt).AddMonths(near);

                        SqlDataAdapter mrp1 = new SqlDataAdapter("select MAX(Inspectdate) as Inspectdate from MooringRopeInspection where RopeId=" + ropeid + " and ropetail=1", sc.con);
                        System.Data.DataTable dtt11 = new System.Data.DataTable();
                        mrp1.Fill(dtt11);
                        if (dtt11.Rows.Count > 0)
                        {
                            var insdt = dtt11.Rows[0][0] == DBNull.Value ? null : dtt11.Rows[0][0];

                            if (insdt != null)
                            {
                                SqlDataAdapter mrp11 = new SqlDataAdapter("select * from MooringRopeInspection where RopeId=" + ropeid + " and InspectDate=" + insdt + " and ropetail=1", sc.con);
                                System.Data.DataTable dtt111 = new System.Data.DataTable();
                                mrp11.Fill(dtt111);
                                if (dtt111.Rows.Count > 0)
                                {
                                    int avg = 0;
                                    int avgrtngA = Convert.ToInt32(dtt111.Rows[0]["AverageRating_A"]);
                                    int avgrtngB = Convert.ToInt32(dtt111.Rows[0]["AverageRating_B"]);


                                    if (avgrtngA >= avgrtngB)
                                    {
                                        avg = avgrtngA;
                                    }
                                    if (avgrtngB >= avgrtngA)
                                    {
                                        avg = avgrtngB;
                                    }


                                    string rating = "Rating" + avg;

                                    int near = Convert.ToInt32(avg);
                                    SqlDataAdapter pp = new SqlDataAdapter("select " + rating + " from tblRopeTailInspectionSetting where MooringRopeType=" + Convert.ToInt32(dtt15.Rows[k]["RopeTypeId"]) + " and ManufacturerType=" + Convert.ToInt32(dtt15.Rows[k]["ManufacturerId"]) + "", sc.con);
                                    System.Data.DataTable rtc = new System.Data.DataTable();
                                    pp.Fill(rtc);
                                    if (rtc.Rows.Count > 0)
                                    {
                                        decimal perchk = Convert.ToDecimal(rtc.Rows[0][0]) * 30 / 100;
                                        perchk = perchk * 100;
                                        //near = Convert.ToInt32(rtc.Rows[0][0]);
                                        near = Convert.ToInt32(perchk);
                                        //near = Convert.ToInt32(rtc.Rows[0][0]);
                                    }

                                    DateTime notidueMonth = Convert.ToDateTime(dtt111.Rows[0]["InspectDate"]).AddDays(near);

                                    var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == ropeid && b.IsActive == true && b.DeleteStatus == false && b.RopeTail == 1);
                                    if (result != null)
                                    {
                                        result.InspectionDueDate = notidueMonth;
                                        result.ModifiedBy = "Admin";
                                        result.ModifiedDate = DateTime.Now;
                                        sc.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                var insdt5 = dtt15.Rows[k]["InstalledDate"] == DBNull.Value ? null : dtt15.Rows[k]["InstalledDate"];

                                //string chk = insdt.ToString();
                                //if (chk != "01/01/0001 00:00:00" || chk != null)
                                if (insdt5 != null)
                                {
                                    SqlDataAdapter adp = new SqlDataAdapter("select * from tblRopeTailInspectionSetting where MooringRopeType=" + Convert.ToInt32(dtt15.Rows[k]["RopeTypeId"]) + " and ManufacturerType=" + Convert.ToInt32(dtt15.Rows[k]["ManufacturerId"]) + "", sc.con);
                                    System.Data.DataTable dt = new System.Data.DataTable();
                                    adp.Fill(dt);
                                    if (dt.Rows.Count > 0)
                                    {
                                        decimal rating1 = Convert.ToDecimal(dt.Rows[0]["Rating1"]);
                                        int rat = Convert.ToInt32(rating1);

                                        decimal perchk = Convert.ToDecimal(dt.Rows[0]["Rating1"]) * 30 / 100;
                                        perchk = perchk * 100;
                                        int near = Convert.ToInt32(perchk);
                                        DateTime inspectduedate = Convert.ToDateTime(insdt5).AddDays(near);

                                        DateTime crntdt = DateTime.Now;
                                        if (inspectduedate <= crntdt)
                                        {
                                            inspectduedate = DateTime.Now;
                                        }

                                        //moorwinch.InspectionDueDate = inspectduedate;


                                        var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate == null && b.RopeTail == 1);
                                        if (result != null)
                                        {
                                            result.InspectionDueDate = inspectduedate;
                                            result.ModifiedBy = "Admin";
                                            result.ModifiedDate = DateTime.Now;
                                            sc.SaveChanges();
                                        }
                                        //SqlDataAdapter kk = new SqlDataAdapter("update MooringRopeDetail set InspectionDueDate='" + inspectduedate + "' where RopeTypeId=" + Convert.ToInt32(dtt15.Rows[k]["RopeTypeId"]) + " and ManufacturerId=" + Convert.ToInt32(dtt15.Rows[k]["ManufacturerId"]) + " and RopeTail=0 and DeleteStatus=0 and OutofServiceDate is null", sc.con);
                                        //DataTable ddtbl = new DataTable();
                                        //kk.Fill(ddtbl);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex) { }
        }
        private void updinspectionduedate()
        {
            try
            {
                SqlDataAdapter mrp = new SqlDataAdapter("select * from MooringRopeDetail where deletestatus=0 and installeddate is not null and outofservicedate is null and ropetail=0", sc.con);
                System.Data.DataTable dtt15 = new System.Data.DataTable();
                mrp.Fill(dtt15);

                //if (dtt1.Rows.Count > 0)
                for (int k = 0; k < dtt15.Rows.Count; k++)
                {

                    SqlDataAdapter jjj = new SqlDataAdapter("select * from tblRopeInspectionSetting where MooringRopeType=" + Convert.ToInt32(dtt15.Rows[k]["RopeTypeId"]) + " and ManufacturerType=" + Convert.ToInt32(dtt15.Rows[k]["ManufacturerId"]) + "", sc.con);
                    System.Data.DataTable dtt1 = new System.Data.DataTable();
                    jjj.Fill(dtt1);

                    if (dtt1.Rows.Count > 0)
                    {

                        //int ropetypeid = Convert.ToInt32(dtt1.Rows[0]["MaxRunningHours"]);
                        //DateTime insduedt = Convert.ToDateTime(dtt1.Rows[0]["InspectionDueDate"]);
                        int ropeid = Convert.ToInt32(dtt15.Rows[k]["id"]);

                        //DateTime notidueMonth = Convert.ToDateTime(insduedt).AddMonths(near);

                        SqlDataAdapter mrp1 = new SqlDataAdapter("select MAX(Inspectdate) as Inspectdate from MooringRopeInspection where RopeId=" + ropeid + " and ropetail=0", sc.con);
                        System.Data.DataTable dtt11 = new System.Data.DataTable();
                        mrp1.Fill(dtt11);
                        if (dtt11.Rows.Count > 0)
                        {
                            var insdt = dtt11.Rows[0][0] == DBNull.Value ? null: dtt11.Rows[0][0] ;

                            if(insdt !=null)
                            { 
                            SqlDataAdapter mrp11 = new SqlDataAdapter("select * from MooringRopeInspection where RopeId=" + ropeid + " and InspectDate=" + insdt + " and ropetail=0", sc.con);
                                System.Data.DataTable dtt111 = new System.Data.DataTable();
                            mrp11.Fill(dtt111);
                                if (dtt111.Rows.Count > 0)
                                {
                                    int avg = 0;
                                    int avgrtngA = Convert.ToInt32(dtt111.Rows[0]["AverageRating_A"]);
                                    int avgrtngB = Convert.ToInt32(dtt111.Rows[0]["AverageRating_B"]);


                                    if (avgrtngA >= avgrtngB)
                                    {
                                        avg = avgrtngA;
                                    }
                                    if (avgrtngB >= avgrtngA)
                                    {
                                        avg = avgrtngB;
                                    }


                                    string rating = "Rating" + avg;

                                    int near = Convert.ToInt32(avg);
                                    SqlDataAdapter pp = new SqlDataAdapter("select " + rating + " from tblRopeInspectionSetting where MooringRopeType=" + Convert.ToInt32(dtt15.Rows[k]["RopeTypeId"]) + " and ManufacturerType=" + Convert.ToInt32(dtt15.Rows[k]["ManufacturerId"]) + "", sc.con);
                                    System.Data.DataTable rtc = new System.Data.DataTable();
                                    pp.Fill(rtc);
                                    if (rtc.Rows.Count > 0)
                                    {
                                        decimal perchk = Convert.ToDecimal(rtc.Rows[0][0]) * 30 / 100;
                                        perchk = perchk * 100;
                                        //near = Convert.ToInt32(rtc.Rows[0][0]);
                                        near = Convert.ToInt32(perchk);
                                        //near = Convert.ToInt32(rtc.Rows[0][0]);
                                    }

                                    DateTime notidueMonth = Convert.ToDateTime(dtt111.Rows[0]["InspectDate"]).AddDays(near);

                                    var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == ropeid && b.IsActive == true && b.DeleteStatus == false && b.RopeTail == 0);
                                    if (result != null)
                                    {
                                        result.InspectionDueDate = notidueMonth;
                                        result.ModifiedBy = "Admin";
                                        result.ModifiedDate = DateTime.Now;
                                        sc.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                var insdt5 = dtt15.Rows[k]["InstalledDate"] == DBNull.Value ? null : dtt15.Rows[k]["InstalledDate"];

                                //string chk = insdt.ToString();
                                //if (chk != "01/01/0001 00:00:00" || chk != null)
                                if ( insdt5 != null)
                                {
                                    SqlDataAdapter adp = new SqlDataAdapter("select * from tblRopeInspectionSetting where MooringRopeType=" + Convert.ToInt32(dtt15.Rows[k]["RopeTypeId"]) + " and ManufacturerType=" + Convert.ToInt32(dtt15.Rows[k]["ManufacturerId"]) + "", sc.con);
                                    System.Data.DataTable dt = new System.Data.DataTable();
                                    adp.Fill(dt);
                                    if (dt.Rows.Count > 0)
                                    {
                                        decimal rating1 = Convert.ToDecimal(dt.Rows[0]["Rating1"]);
                                        int rat = Convert.ToInt32(rating1);

                                        decimal perchk = Convert.ToDecimal(dt.Rows[0]["Rating1"]) * 30 / 100;
                                        perchk = perchk * 100;
                                        int near = Convert.ToInt32(perchk);
                                        DateTime inspectduedate = Convert.ToDateTime(insdt5).AddDays(near);

                                        DateTime crntdt = DateTime.Now;
                                        if (inspectduedate <= crntdt)
                                        {
                                            inspectduedate = DateTime.Now;
                                        }

                                        //moorwinch.InspectionDueDate = inspectduedate;


                                        var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == ropeid && b.IsActive == true && b.DeleteStatus == false && b.OutofServiceDate==null && b.RopeTail == 0);
                                        if (result != null)
                                        {
                                            result.InspectionDueDate = inspectduedate;
                                            result.ModifiedBy = "Admin";
                                            result.ModifiedDate = DateTime.Now;
                                            sc.SaveChanges();
                                        }
                                        //SqlDataAdapter kk = new SqlDataAdapter("update MooringRopeDetail set InspectionDueDate='" + inspectduedate + "' where RopeTypeId=" + Convert.ToInt32(dtt15.Rows[k]["RopeTypeId"]) + " and ManufacturerId=" + Convert.ToInt32(dtt15.Rows[k]["ManufacturerId"]) + " and RopeTail=0 and DeleteStatus=0 and OutofServiceDate is null", sc.con);
                                        //DataTable ddtbl = new DataTable();
                                        //kk.Fill(ddtbl);
                                    }
                                }
                            }
                        }
                       
                    }
                }
            }
            catch (Exception ex){ }
        }

        #endregion
        public System.Data.DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            System.Data.DataTable dtexcel = new System.Data.DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    System.Data.DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String sheetName = dt.Rows[i]["TABLE_NAME"].ToString();
                        sheetName = sheetName.Substring(0, sheetName.Length - 1);
                        // OleDbCommand command = new OleDbCommand("select * from [" + sheetName + "$]", connection);
                        OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [" + sheetName + "$]", con); //here we read data from sheet1  
                        oleAdpt.Fill(dtexcel); //fill excel data into dataTable
                    }

                }
                catch { }
            }
            return dtexcel;
        }

    }



}
