using ClosedXML.Excel;
using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
//using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WorkShipVersionII.ViewModelAdministration
{
       public class ApplicationLogViewModel: ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private BackgroundWorker _Worker;
        public ApplicationLogViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            excelCommand = new RelayCommand(() => _Worker.RunWorkerAsync(), () => !_Worker.IsBusy);
            _Worker = new BackgroundWorker();
            _Worker.WorkerReportsProgress = true;
            _Worker.DoWork += ExcelExport;
            _Worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            _Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ExportCompleted);
           
            //excelCommand = new RelayCommand(ExcelExport);


            loadlogFiles = GetLogFile();
        }

       
        private ICommand excelCommand;
        public ICommand ExcelCommand
        {
            get
            {
                return excelCommand;
            }
           
        }



        private double _LogProgress;
        public double LogProgress
        {
            get
            {

                return _LogProgress;
            }
            set
            {
                _LogProgress = value;
                RaisePropertyChanged("LogProgress");
            }
        }



        private string logpVisible;
        public string logPVisible
        {
            get
            {
                if (logpVisible == null)
                {
                    logPVisible = "Collapsed";
                }
                return logpVisible;
            }
            set
            {
                logpVisible = value;
                RaisePropertyChanged("logPVisible");
            }
        }

        private static Nullable<DateTime> _DateFrom = null;
        public Nullable<DateTime> DateFrom
        {
            get
            {
                if (_DateFrom == null)
                {
                    _DateFrom = DateTime.Now;
                }

                return _DateFrom;
            }
            set
            {
                _DateFrom = value;
                RaisePropertyChanged("DateFrom");
            }
        }

        private Nullable<DateTime> _DateTo = null;
        public Nullable<DateTime> DateTo
        {
            get
            {
                if (_DateTo == null)
                {
                    _DateTo = DateTime.Now;
                }

                return _DateTo;
            }
            set
            {
                _DateTo = value;
                RaisePropertyChanged("DateTo");
            }
        }

       

        public static ObservableCollection<InfoLogClass> loadlogFiles = new ObservableCollection<InfoLogClass>();
        public ObservableCollection<InfoLogClass> LoadlogFiles
        {
            get
            {
                return loadlogFiles;
            }
            set
            {
                loadlogFiles = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadlogFiles"));
            }
        }

        private ObservableCollection<InfoLogClass> GetLogFile()
        {
            var ranklist = new ObservableCollection<InfoLogClass>();

            try
            {
                var data = sc.InfoLogs.Select(x => new { x.LogId, x.dt, x.UserName, x.ModuleName, x.ActionName, x.Description });


                foreach (var item in data.ToList())
                {
                    ranklist.Add(new InfoLogClass() { LogId = item.LogId, dt = item.dt, UserName = item.UserName, ModuleName = item.ModuleName, ActionName = item.ActionName, Description = item.Description });
                }

                return ranklist;
            }
            catch (Exception ex)
            {
               sc.ErrorLog(ex);
                ranklist.Clear();
                return ranklist;
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _LogProgress = e.ProgressPercentage;
            RaisePropertyChanged("LogProgress");
        }

        private void ExportCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                logPVisible = "Collapsed";
                sc.CreateLog("Application Log", "Export Data", null);
            }
            catch(Exception ex)
            {
                sc.ErrorLog(ex);
               
            }
        }

        private void ExcelExport(object sender, DoWorkEventArgs eb)
        {
            try
            {
                ExcelExport1();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void ExcelExport1()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "DigiMoor_X7_System_Log_" + DateTime.Now.ToString("dd-MMM-yyyy");
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (sfd.ShowDialog() == true)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        File.Delete(sfd.FileName);
                    }

                    //...........
                    int ib = 1;
                    _Worker.ReportProgress(ib);
                    logPVisible = "Visible";
                    //...............


                    DataSet dataSet = null;
                    dataSet = new DataSet("General");

                    _Worker.ReportProgress(ib + 1);

                    DateTime dt1 = Convert.ToDateTime(DateFrom).Date;
                    DateTime dt2 = Convert.ToDateTime(DateTo).Date;

                    var bmslist1 = sc.InfoLogs.Select(p => new { LogId = p.LogId, Date_Time = p.dt, UserName = p.UserName, ModuleName = p.ModuleName, ActionName = p.ActionName, Description = p.Description, Editdate = p.Editdate }).Where(x => DbFunctions.TruncateTime(x.Date_Time) >= dt1 && DbFunctions.TruncateTime(x.Date_Time) <= dt2).ToList();
                    System.Data.DataTable dtbl1 = sc.LINQResultToDataTable(bmslist1);
                    dtbl1.TableName = "ApplicationLog";
                    dataSet.Tables.Add(dtbl1);


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
                        //  MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);



                    }

                    //ApplicationClass ExcelApp = new ApplicationClass();
                    //ExcelApp.Application.Workbooks.Add(Type.Missing);
                    //Workbook xlWorkbook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                    //// Loop over DataTables in DataSet.
                    //DataTableCollection collection = dataSet.Tables;
                    //dataSet.Dispose();

                    //var a = ib;
                    //var cons = collection.Count;
                    //for (int i = cons; i > 0; i--)
                    //{
                    //    ib = ib + i;
                    //    _Worker.ReportProgress(ib * (100 / cons + a));

                    //    Sheets xlSheets = null;
                    //    Worksheet xlWorksheet = null;
                    //    //Create Excel Sheets
                    //    xlSheets = ExcelApp.Sheets;
                    //    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                    //                   Type.Missing, Type.Missing, Type.Missing);

                    //    System.Data.DataTable table = collection[i - 1];
                    //    xlWorksheet.Name = table.TableName;
                    //    var tcoun = table.Columns.Count;
                    //    for (int j = 1; j < tcoun + 1; j++)
                    //    {
                    //        ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                    //    }


                    //    // Storing Each row and column value to excel sheet
                    //    var rcoun = table.Rows.Count;
                    //    var rcoun1 = table.Columns.Count;
                    //    for (int k = 0; k < rcoun; k++)
                    //    {

                    //        for (int l = 0; l < rcoun1; l++)
                    //        {
                    //            ExcelApp.Cells[k + 2, l + 1] =
                    //            table.Rows[k].ItemArray[l].ToString();
                    //        }


                    //    }

                    //    ExcelApp.Columns.AutoFit();
                    //    ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[xlWorksheet.Name]).Protect("49WEB$TREET#");

                    //}

                    //((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();

                    //((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).SaveAs(sfd.FileName);
                    //ExcelApp.ActiveWorkbook.Saved = true;
                    //ExcelApp.Quit();


                }
            }
            catch(Exception ex)
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
