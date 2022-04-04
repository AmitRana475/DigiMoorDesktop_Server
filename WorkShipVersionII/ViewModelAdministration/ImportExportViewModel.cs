using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.ViewModelAdministration
{

    public class ImportExportViewModel : ViewModelBase
    {
        private readonly AdministrationContaxt sc;

        private BackgroundWorker _Export;
        private BackgroundWorker _Import;
        ObservableCollection<CommentofVSClass> Employees;
        private SqlConnection con = ConnectionBulder.con;

        public ImportExportViewModel()
        {
            if (sc == null)
            {
                sc = new AdministrationContaxt();
                _Export = new BackgroundWorker();
            }


            _BrowseCommand = new RelayCommand(BrowseMethod);
            //_ImportCommand = new RelayCommand(ImportMethod);

            _ImportCommand = new RelayCommand(() => _Import.RunWorkerAsync(), () => !_Import.IsBusy);
            _Import = new BackgroundWorker();
            _Import.WorkerReportsProgress = true;
            _Import.DoWork += ImportMethod;
            _Import.ProgressChanged += new ProgressChangedEventHandler(ImportProgressChanged);
            _Import.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ImportCompleted);


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

            MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);
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

            MessageBox.Show("Import process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            IVisibles = "Collapsed";
            ImportVisibles = false;
            Employees = new ObservableCollection<CommentofVSClass>();
            new NotificationViewModel();

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
            catch(Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void ExportMethod1()
        {

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                //sfd.FileName = "Work-Ship_Export_" + DateTime.Now.ToString("dd-MMM-yyyy") + "_IMO_" + Class1.IMONum;
                sfd.FileName = "Work-Ship_Export_" + DateTime.Now.ToString("dd-MMM-yyyy");
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

                    _Export.ReportProgress(ib + 1);

                    if (con.State == ConnectionState.Closed)
                        con.Open();


                    string qryCW = @"select v.vessel_ID,a.id as Crew_ID,a.name as FullName,a.UserName,a.Position,a.rid,a.ServiceFrom,a.ServiceTo,a.CDC as CDCNumber,a.empno as Emp_Number,a.Comments,a.department as Department,a.SeaWH,a.PortWH,a.SeaWk1, a.SeaNWK1,a.PortWk1,a.PortNWK1,a.Remarks,case when a.SeaWk1 = '' then 'No' else 'Yes' end as Watchkeeper ,a.overtime as Overtime, ot.NrmlWHrs,ot.SatWHrs,ot.SunWhrs,ot.HolidayWHrs,ot.FixedOverTime,ot.HourlyRate,ot.Currency,ot.holidays from CrewDetail a cross join Vessel v left outer join overtime ot on a.UserName = ot.UserName where a.UserName in (select distinct UserName from WorkHours where dates between '" + Convert.ToDateTime(MyDateTimeFrom).ToShortDateString() + "' and '" + Convert.ToDateTime(MyDateTimeTo).ToShortDateString() + "' ) ";
                    SqlDataAdapter dap1 = new SqlDataAdapter(qryCW, con);
                    System.Data.DataTable dtbl1 = new System.Data.DataTable("CrewDetail");
                    dap1.Fill(dtbl1);
                    dataSet.Tables.Add(dtbl1);
                    dtbl1.Dispose();
                    dap1.Dispose();




                    //var bmslist1 = sc.CrewDetails.Select(x=> new CrewDetailClass() {Vessel_ID=x.Vessel_ID,Id=x.Id,name=x.name,UserName=x.UserName,position= x.position,rid=x.rid, ServiceFrom=x.ServiceFrom,ServiceTo=x.ServiceTo,CDC=x.CDC,empno=x.empno,comments=x.comments,department=x.department,SeaWH=x.SeaWH,portWH=x.portWH,SeaWk1=x.SeaWk1,SeaNWK1=x.SeaNWK1,PortWk1=x.PortWk1,PortNWK1=x.PortNWK1,Remarks=x.Remarks,WatchKeeper=x.WatchKeeper,overtime=x.overtime }).ToList();
                    //System.Data.DataTable dtbl1 = sc.LINQResultToDataTable(bmslist1);
                    //dtbl1.TableName = "CrewDetail";
                    //dataSet.Tables.Add(dtbl1);


                    _Export.ReportProgress(ib + 2);

                    //var bmslist2 = sc.UserAccessHOD.ToList();
                    //System.Data.DataTable dtbl2 = sc.LINQResultToDataTable(bmslist2);
                    //dtbl2.TableName = "HOD Details";
                    //dataSet.Tables.Add(dtbl2);



                    string whqry = @"select d.vessel_ID as Vessel_ID, C.* from 
                                (select a.wid as ID ,b.name as FullName,a.FullName as UserName,a.Position,a.TotalHours,a.RestHours,a.options as Options ,a.Remarks,a.dates as Dates , a.hrs,a.NonConfirmities,a.Department,a.DaysOfMonth,a.NormalOT1 as Overtime,a.opa as Opa,a.RuleNames,a.RestHour7day,a.NormalOT1 as NormalWH, a.RestHourAny24,a.RestHourAny7day, b.chkyoungs   from WorkHours a join  CrewDetail b on  a.UserName=b.UserName) c  cross join Vessel d where C.Dates between '" + Convert.ToDateTime(MyDateTimeFrom).ToShortDateString() + "' and '" + Convert.ToDateTime(MyDateTimeTo).ToShortDateString() + "'";

                    SqlDataAdapter dap2 = new SqlDataAdapter(whqry, con);
                    System.Data.DataTable dtbl2 = new System.Data.DataTable("WorkHours");
                    dap2.Fill(dtbl2);
                    dataSet.Tables.Add(dtbl2);
                    dtbl2.Dispose();
                    dap2.Dispose();

                    _Export.ReportProgress(ib + 3);

                    string qury = "select	b.Vessel_ID,a.Id as CId,a.CName,a.DOI,a.DOE,a.DOS,a.Remarks from certificates a cross join Vessel b";
                    SqlDataAdapter dap3 = new SqlDataAdapter(qury, con);
                    System.Data.DataTable dtbl3 = new System.Data.DataTable("CertificateList");
                    dap3.Fill(dtbl3);
                    dataSet.Tables.Add(dtbl3);
                    dtbl3.Dispose();
                    dap3.Dispose();

                    _Export.ReportProgress(ib + 4);


                    List<CertificateNC> list4 = sc.GetCertificationList();
                    var list14 = list4.Select(x => new { x.Id, x.Vessel_ID, x.CName, x.DOI, x.DOS, x.DOE, x.AlertFrequency, x.AdminAck, x.MasterAck, x.HODAck }).ToList();

                    System.Data.DataTable dtbl4 = sc.LINQResultToDataTable(list14);
                    dtbl4.TableName = "CertificateNotificatonList";
                    dataSet.Tables.Add(dtbl4);

                    //string qurycf = "select b.Vessel_ID,a.* from CertificateNotification a cross join Vessel b";
                    //SqlCeDataAdapter dap4 = new SqlCeDataAdapter(qurycf, con);
                    //System.Data.DataTable dtbl4 = new System.Data.DataTable("CertificateNotificatonList");
                    //dap4.Fill(dtbl4);
                    //dataSet.Tables.Add(dtbl4);
                    //dtbl4.Dispose();
                    //dap4.Dispose();

                    _Export.ReportProgress(ib + 5);




                    List<WorkHoursNC> list = sc.GetDeviationList(Convert.ToDateTime(MyDateTimeFrom).Date, Convert.ToDateTime(MyDateTimeTo).Date);



                    int counter = 1;

                    list.ToList().ForEach(x => x.ROW_NUM = counter++);

                    var list1 = list.Select(x => new { x.vessel_ID, x.wid, x.WRID, x.FullName, x.UserName, x.Dates, x.NonConfirmity, x.NCType, x.ROW_NUM, x.AckAdmin, x.AckMaster, x.AckHOD, x.position }).ToList();




                    System.Data.DataTable dtbl5 = sc.LINQResultToDataTable(list1);
                    dtbl5.TableName = "WorkHoursNotificatonList";
                    dataSet.Tables.Add(dtbl5);




                    //string qurywn = @"select distinct v.vessel_ID,v.VesselName, a.wid,a.UserName as Name,a.FullName as UserNames, a.dates,a.NonConfirmities,a.NCtype,a.ROW_NUM,CASE WHEN a.CrewNC = 'To be Acknowledged' THEN 'No' ELSE 'Yes' end as AckAdmin ,Case when b.MasterCrewNC = 'To be Acknowledged' THEN 'No' ELSE 'Yes' end as AckMaster, Case when c.HODCrewNC = 'To be Acknowledged' THEN 'No' ELSE 'Yes' end as AckHOD,cd.position from dgvNC_ArchivesView_Admin a  join dgvNC_ArchivesView_Master b on a.FullName = b.FullName join dgvNC_ArchivesView_HOD c on a.FullName = c.FullName join CrewDetail cd on cd.UserName = a.FullName cross join Vessel v
                    //where a.dates between '" + Convert.ToDateTime(MyDateTimeFrom).ToShortDateString() + "' and '" + Convert.ToDateTime(MyDateTimeTo).ToShortDateString() + "' order by dates desc";
                    //SqlCeDataAdapter dap5 = new SqlCeDataAdapter(qurywn, con);
                    //System.Data.DataTable dtbl5 = new System.Data.DataTable("WorkHoursNotificatonList");
                    //dap5.Fill(dtbl5);
                    //dataSet.Tables.Add(dtbl5);
                    //dtbl5.Dispose();
                    //dap5.Dispose();



                    _Export.ReportProgress(ib + 6);



                    SqlDataAdapter sdakkp = new SqlDataAdapter("Select * from AdminLogin", con);
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

                    _Export.ReportProgress(ib + 7);

                    //string LicExpire = "Expiring On" + " " + ss1.ToString("dd-MMM-yyyy");

                    System.Data.DataTable dtLicingo = new System.Data.DataTable("Product_Detail");
                    dtLicingo.Columns.Add("Work-Ship", typeof(string));

                    string[] info = { "Version : " + dd1.versions, "Ship Name : " + dd.VesselName, "IMO : " + dd.imo, "License Expiring On : " + ss1.ToString("dd-MMM-yyyy") };
                    foreach (string item in info)
                    {
                        DataRow dr = dtLicingo.NewRow();
                        dr["Work-Ship"] = item;
                        dtLicingo.Rows.Add(dr);
                    }
                    dataSet.Tables.Add(dtLicingo);
                    dtLicingo.Dispose();




                    ApplicationClass ExcelApp = new ApplicationClass();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    Workbook xlWorkbook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                    // Loop over DataTables in DataSet.
                    DataTableCollection collection = dataSet.Tables;
                    dataSet.Dispose();

                    var a = ib;
                    var cons = collection.Count;
                    for (int i = cons; i > 0; i--)
                    {
                        ib = ib + i;
                        _Export.ReportProgress(ib * (100 / cons + a));

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
                                ExcelApp.Cells[k + 2, l + 1] =
                                table.Rows[k].ItemArray[l].ToString();
                            }


                        }

                        ExcelApp.Columns.AutoFit();
                        ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[xlWorksheet.Name]).Protect("49WEB$TREET#");

                    }

                    ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();

                    ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).SaveAs(sfd.FileName);
                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();

                    //MessageBox.Show("Export process has been completed", "", MessageBoxButton.OK, MessageBoxImage.Information);


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


        private void BrowseMethod()
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

                    string con = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=yes'", filename);
                    using (OleDbConnection connection = new OleDbConnection(con))
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand("select * from [CrewDetail$]", connection);
                        using (OleDbDataReader Reader = command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                while (Reader.Read())
                                {
                                    if (Convert.ToInt32(Reader["Vessel_ID"]).Equals(vesselid))
                                    {
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


        private void ImportMethod(object sender, DoWorkEventArgs eb)
        {
            try
            {
                //...........
                int ib = 1;
                _Import.ReportProgress(ib);
                IVisibles = "Visible";
                //...............

                var cons = Employees.Count();
                foreach (var em in Employees)
                {
                    ib = ib + 1;
                    _Import.ReportProgress(ib * (100 / cons));

                    var crew = new CommentofVSClass()
                    {
                        Vessel_ID = em.Vessel_ID,
                        FullName = em.FullName,
                        UserName = em.UserName,
                        Position = em.Position,
                        DepartmentName = em.DepartmentName,
                        NC_Date = em.NC_Date,
                        Office_Comment = em.Office_Comment,
                        Comment_Date = em.Comment_Date

                    };
                    var checkuser = sc.CommentofVs.Where(p => p.UserName.Equals(em.UserName) && p.NC_Date.Equals(em.NC_Date) && p.Office_Comment.Equals(em.Office_Comment) && p.Comment_Date.Equals(em.Comment_Date)).FirstOrDefault();
                    if (checkuser != null)
                    {

                        //sc.Entry(checkuser).State = EntityState.Modified;
                        //sc.SaveChanges();


                    }
                    else
                    {
                        sc.CommentofVs.Add(crew);
                        sc.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
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

    }



}
