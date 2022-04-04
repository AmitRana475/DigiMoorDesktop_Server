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
using WorkShipVersionII.ViewModel;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;
using System.IO;
using WorkShipVersionII.WorkHoursView;
using ClosedXML.Excel;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class LooseEquipInspectionDetailsViewModel : ViewModelBase
    {
        public static ShipmentContaxt sc;
        public ICommand HelpCommand { get; private set; }
        public LooseEquipInspectionDetailsViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }


            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            viewCommand = new RelayCommand<TotalInspections>(ViewLooseEInspection);
            saveCommand = new RelayCommand(SaveAllInspection);

            downloadexcelCommand = new RelayCommand(DownloadInspection);
            cancelCommand = new RelayCommand(CancelIncspection);

            LoadInspections.Clear();
            //LoadInspections = GetMooringInspection(1,"Rope Tail");
            // GetMooringInspection(1, "Joining Shackle");
            ropetype = GetRopeType();
        }

        public LooseEquipInspectionDetailsViewModel(MooringLooseEquipInspectionClass total)
        {
            // Edit Operation
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            viewCommand = new RelayCommand<TotalInspections>(ViewLooseEInspection);
            cancelCommand = new RelayCommand(CancelIncspection);
            // saveCommand = new RelayCommand(EditAllInspection);
            downloadexcelCommand = new RelayCommand(DownloadInspection);
            saveCommand = new RelayCommand(SaveAllInspection);
            LoadInspections.Clear();
            ropetype = GetRopeType();
        }


        public void resetform()
        {
            MooringInspect = new MooringLooseEquipInspectionClass();
            RaisePropertyChanged("MooringInspect");

            EndtoEndDoneDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("EndtoEndDoneDate");


            SRopeType = null; RaisePropertyChanged("SRopeType");
            LoadInspections.Clear();
            //MooringInspect
        }
        private void ViewLooseEInspection(TotalInspections mw)
        {
            try
            {

                StaticHelper.ViewId = mw.id;
                ChildWindowManager.Instance.ShowChildWindow(new ViewLooseEInspection());


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }
        private ICommand downloadexcelCommand;
        public ICommand DownloadExcelCommand
        {
            get { return downloadexcelCommand; }
        }


        private BackgroundWorker _Worker; int ib = 1;
        private void DownloadInspection()
        {

            try
            {
                if (StaticHelper.LooseEId != 0)
                {

                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.FileName = "DigiMoor_X7_LooseEInspectionSheet_" + DateTime.Now.ToString("dd-MMM-yyyy");
                    dlg.DefaultExt = "xlsx";
                    dlg.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(dlg.FileName))
                        {
                            File.Delete(dlg.FileName);
                        }


                        int ib = 1;
                        //_Worker.ReportProgress(ib);
                        //PVisible = "Visible";
                        //...............


                        DataSet dataSet = new DataSet();
                        //dataSet = new DataSet("General");

                        //_Worker.ReportProgress(ib + 1);



                        string qry = "LooseEquipInspection";
                        SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);

                        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sda.SelectCommand.Parameters.AddWithValue("@id", StaticHelper.LooseEId);
                        sda.SelectCommand.Parameters.AddWithValue("@table_name", StaticHelper.TbName);
                        System.Data.DataTable datatbl = new System.Data.DataTable();
                        sda.Fill(datatbl);

                        datatbl.Columns.Remove("id");
                        datatbl.Columns.Remove("RNumber");
                        datatbl.Columns.Remove("looseetypeid");


                        datatbl.Columns.Add("Condition", typeof(string));
                        datatbl.Columns.Add("Remarks", typeof(string));



                        dataSet.Tables.Add(datatbl);

                        //dataSet = datatbl

                        var ranklist = new ObservableCollection<MooringRopeInspectionClass>();
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
                            wb.SaveAs(dlg.FileName);
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
                        //    //ib = ib + i;
                        //    //_Worker.ReportProgress(ib * (100 / cons + a));

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

                        //((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).SaveAs(dlg.FileName);
                        //ExcelApp.ActiveWorkbook.Saved = true;
                        //ExcelApp.Quit();

                        System.Windows.MessageBox.Show("Excel file download succefully !", "LooseE Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Please Choose LooseE Type !", "LooseE Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                //sc.ErrorLog(ex);

                //System.Windows.MessageBox.Show("Please close excel file !", "LooseE Inspection", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
        private void EditAllInspection()
        {

        }


        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }


        public void SaveAllInspection()
        {
            decimal avg = 0;
            var photoimglist = LooseEquipInspectionDetailsView.imagesavelist.ToList();
            var photoimglist1 = LooseEquipInspectionDetailsView.imagesavelist1.ToList();

            MainViewModelWorkHours.CommonValue = false;

            if (MooringInspect.InspectBy == null)
            {
                MainViewModelWorkHours.CommonValue = true;
                System.Windows.MessageBox.Show("Please fill Inspected By !", "Loose Eq. Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int validationcheck = 0;
            int inspectioncheck = 0;

            foreach (var item in LoadInspections)
            {
                if (item.IsCheckedV == true)
                {
                    inspectioncheck = 1;
                    //if (item.InternalRating_A == 0.00 || item.InternalRating_B == 0.00 || item.ExternalRating_A == 0.00 || item.ExternalRating_B == 0.00 || item.LengthOFAbrasion_A == 0 || item.LengthOFAbrasion_B == 0 || item.CutYarnCount_A == 0 || item.CutYarnCount_B == 0 || item.DistanceOutboard_A == 0 || item.DistanceOutboard_B == 0 || item.LengthOFGlazing_A == 0 || item.LengthOFGlazing_B == 0 || item.Chafe_guard_condition == null || item.Twist == 0)
                    if (item.Condition == "" || item.Number == null)
                    {
                        validationcheck = 1;
                    }
                }
            }

            if (validationcheck == 1)
            {
                MainViewModelWorkHours.CommonValue = true;
                System.Windows.MessageBox.Show("Please fill all fields !", "Loose Eq. Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (inspectioncheck == 0)
            {
                MainViewModelWorkHours.CommonValue = true;
                System.Windows.MessageBox.Show("Please choose atleast 1 Loose equipment to inspect by selecting checkbox in the first column !", "Loose Eq. Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string test = ""; int insdtcheck = 0;
            foreach (var item in LoadInspections)
            {
                //var maxNotiid = sc.Notifications.Select(x => x.Id).Max();
                //int notiid = maxNotiid + 1;
                if (item.IsCheckedV == true)
                {
                    var MI1 = photoimglist.Where(x => x.LooseETypeId == item.LooseETypeId && x.Number == item.Number).FirstOrDefault();
                    var MI2 = photoimglist1.Where(x => x.LooseETypeId == item.LooseETypeId && x.Number == item.Number).FirstOrDefault();

                  //  var imagename1 = MI1.imagename1; // photoimglist.Where(x => x.LooseETypeId == item.LooseETypeId && x.Number == item.Number).Select(x => x.imagename1).SingleOrDefault();
                  //  var photoname1 = MI1.photo1; // photoimglist.Where(x => x.LooseETypeId == item.LooseETypeId && x.Number == item.Number).Select(x => x.photo1).SingleOrDefault();


                   // var imagename2 = MI2.imagename2; // photoimglist1.Where(x => x.LooseETypeId == item.LooseETypeId && x.Number == item.Number).Select(x => x.imagename2).SingleOrDefault();
                   // var photoname2 = MI2.photo2; // photoimglist1.Where(x => x.LooseETypeId == item.LooseETypeId && x.Number == item.Number).Select(x => x.photo2).SingleOrDefault();


                    int notiid = sc.NextNotiId();

                    var ss = item;
                    ss.InspectBy = MooringInspect.InspectBy;
                    ss.InspectDate = MooringInspect.InspectDate;

                                   ss.Number = item.UniqueId;
                    ss.LooseEtbPK = item.Id;
                    if (MI1 != null)
                    {
                        ss.Photo1 = MI1.photo1;
                        ss.Image1 = MI1.imagename1;
                    }
                    if (MI2 != null)
                    {
                        ss.Photo2 = MI2.photo2;
                        ss.Image2 = MI2.imagename2;
                    }

                    ss.CreatedDate = DateTime.Now;
                    ss.IsActive = true;
                    ss.NotificationId = notiid;

                    if (item.LooseETypeId == 1)
                    {
                        DateTime? insdt = DateTime.Now;
                        DateTime inspectdt = Convert.ToDateTime(MooringInspect.InspectDate);
                        SqlDataAdapter adp5 = new SqlDataAdapter("select dateinstalled from JoiningShackle where ID=" + item.LooseEtbPK + "", sc.con);
                        System.Data.DataTable dt5 = new System.Data.DataTable();
                        adp5.Fill(dt5);
                        if (dt5.Rows.Count > 0)
                        {
                            insdt = Convert.ToDateTime(dt5.Rows[0][0] == DBNull.Value ? null : dt5.Rows[0][0]);
                        }
                        else
                        {
                            insdt = null;
                        }
                        if (inspectdt < insdt)
                        {
                            insdtcheck = 1;
                            if (test == "")
                            {
                                test = item.Number;
                            }
                            else
                            {
                                test = test + "," + item.Number;
                            }

                        }

                    }

                    if (item.LooseETypeId == 3 || item.LooseETypeId == 4 || item.LooseETypeId == 6)
                    {
                        DateTime? insdt = DateTime.Now;
                        DateTime inspectdt = Convert.ToDateTime(MooringInspect.InspectDate);
                        SqlDataAdapter adp5 = new SqlDataAdapter("select installeddate from RopeTail where LooseEtypeId=" + item.LooseETypeId + " and ID=" + item.LooseEtbPK + "", sc.con);
                        System.Data.DataTable dt5 = new System.Data.DataTable();
                        adp5.Fill(dt5);
                        if (dt5.Rows.Count > 0)
                        {
                            insdt = Convert.ToDateTime(dt5.Rows[0][0] == DBNull.Value ? null : dt5.Rows[0][0]);
                        }
                        else
                        {
                            insdt = null;
                        }
                        if (inspectdt < insdt)
                        {
                            insdtcheck = 1;
                            if (test == "")
                            {
                                test = item.Number;
                            }
                            else
                            {
                                test = test + "," + item.Number;
                            }

                        }

                    }

                    if (item.LooseETypeId == 5)
                    {
                        DateTime? insdt = DateTime.Now;
                        DateTime inspectdt = Convert.ToDateTime(MooringInspect.InspectDate);
                        SqlDataAdapter adp5 = new SqlDataAdapter("select dateinstalled from ChainStopper where ID=" + item.LooseEtbPK + "", sc.con);
                        System.Data.DataTable dt5 = new System.Data.DataTable();
                        adp5.Fill(dt5);
                        if (dt5.Rows.Count > 0)
                        {
                            insdt = Convert.ToDateTime(dt5.Rows[0][0] == DBNull.Value ? null : dt5.Rows[0][0]);
                        }
                        else
                        {
                            insdt = null;
                        }
                        if (inspectdt < insdt)
                        {
                            insdtcheck = 1;
                            if (test == "")
                            {
                                test = item.Number;
                            }
                            else
                            {
                                test = test + "," + item.Number;
                            }

                        }

                    }
                    if (item.LooseETypeId == 7)
                    {
                        DateTime? insdt = DateTime.Now;
                        DateTime inspectdt = Convert.ToDateTime(MooringInspect.InspectDate);
                        SqlDataAdapter adp5 = new SqlDataAdapter("select installeddate from ChafeGuard where ID=" + item.LooseEtbPK + "", sc.con);
                        System.Data.DataTable dt5 = new System.Data.DataTable();
                        adp5.Fill(dt5);
                        if (dt5.Rows.Count > 0)
                        {
                            insdt = Convert.ToDateTime(dt5.Rows[0][0] == DBNull.Value ? null : dt5.Rows[0][0]);
                        }
                        else
                        {
                            insdt = null;
                        }
                        if (inspectdt < insdt)
                        {
                            insdtcheck = 1;
                            if (test == "")
                            {
                                test = item.Number;
                            }
                            else
                            {
                                test = test + "," + item.Number;
                            }

                        }

                    }

                    if (item.LooseETypeId == 8)
                    {
                        DateTime? insdt = DateTime.Now;
                        DateTime inspectdt = Convert.ToDateTime(MooringInspect.InspectDate);
                        SqlDataAdapter adp5 = new SqlDataAdapter("select installeddate from WinchBreakTestKit where ID=" + item.LooseEtbPK + "", sc.con);
                        System.Data.DataTable dt5 = new System.Data.DataTable();
                        adp5.Fill(dt5);
                        if (dt5.Rows.Count > 0)
                        {
                            insdt = Convert.ToDateTime(dt5.Rows[0][0] == DBNull.Value ? null : dt5.Rows[0][0]);
                        }
                        else
                        {
                            insdt = null;
                        }
                        if (inspectdt < insdt)
                        {
                            insdtcheck = 1;
                            if (test == "")
                            {
                                test = item.Number;
                            }
                            else
                            {
                                test = test + "," + item.Number;
                            }

                        }

                    }

                    if (insdtcheck == 0)
                    {


                        try
                        {
                            //if (joiningShackle.DateInstalled != null)
                            //{
                            if (item.LooseETypeId == 1)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType=" + 1 + "", sc.con);
                                System.Data.DataTable dt = new System.Data.DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    decimal datecheck = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]);
                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    //DateTime inspectduedate = Convert.ToDateTime(MooringInspect.InspectDate).AddDays(near);


                                    int chekint = Convert.ToInt32(datecheck);
                                    DateTime date = Convert.ToDateTime(MooringInspect.InspectDate);
                                    DateTime nextMonth = date.AddMonths(chekint);
                                    TimeSpan t = nextMonth - date;
                                    double NrOfDays = t.TotalDays;

                                    DateTime inspectduedate = Convert.ToDateTime(MooringInspect.InspectDate).AddDays(NrOfDays);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }
                                    string dd = Convert.ToDateTime(inspectduedate).ToString("yyyy-MM-dd");
                                    //SqlDataAdapter pp = new SqlDataAdapter("update JoiningShackle set InspectionDueDate ='" + dd + "' where ID=" + item.Id + "", sc.con);
                                    SqlDataAdapter pp = new SqlDataAdapter("UpdateInspDueDtJoiningShackle", sc.con);
                                    pp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    pp.SelectCommand.Parameters.AddWithValue("@Id", item.Id);
                                    pp.SelectCommand.Parameters.AddWithValue("@InspectionDueDate", dd);
                                    System.Data.DataTable dddt = new System.Data.DataTable();
                                    pp.Fill(dddt);
                                }
                            }

                            if (item.LooseETypeId == 3 || item.LooseETypeId == 4 || item.LooseETypeId == 6)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType=" + item.LooseETypeId + "", sc.con);
                                System.Data.DataTable dt = new System.Data.DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    DateTime inspectduedate = Convert.ToDateTime(MooringInspect.InspectDate).AddDays(near);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }
                                    string dd = Convert.ToDateTime(inspectduedate).ToString("yyyy-MM-dd");
                                    //SqlDataAdapter pp = new SqlDataAdapter("update RopeTail set InspectionDueDate ='" + dd + "' where ID=" + item.Id + " and LooseETypeId=" + item.LooseETypeId + "", sc.con);
                                    SqlDataAdapter pp = new SqlDataAdapter("UpdateInspDueDtRopeTail", sc.con);
                                    pp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    pp.SelectCommand.Parameters.AddWithValue("@Id", item.Id);
                                    pp.SelectCommand.Parameters.AddWithValue("@LooseETypeId", item.LooseETypeId);
                                    pp.SelectCommand.Parameters.AddWithValue("@InspectionDueDate", dd);
                                    System.Data.DataTable dddt = new System.Data.DataTable();
                                    pp.Fill(dddt);
                                }
                            }

                            if (item.LooseETypeId == 5)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType=" + 5 + "", sc.con);
                                System.Data.DataTable dt = new System.Data.DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    DateTime inspectduedate = Convert.ToDateTime(MooringInspect.InspectDate).AddDays(near);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }
                                    string dd = Convert.ToDateTime(inspectduedate).ToString("yyyy-MM-dd");
                                    //SqlDataAdapter pp = new SqlDataAdapter("update ChainStopper set InspectionDueDate ='" + dd + "' where ID=" + item.Id + "", sc.con);
                                    //System.Data.DataTable dddt = new System.Data.DataTable();
                                    //pp.Fill(dddt);

                                    SqlDataAdapter pp = new SqlDataAdapter("UpdateInspDueDtChainStopper", sc.con);
                                    pp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    pp.SelectCommand.Parameters.AddWithValue("@Id", item.Id);                                 
                                    pp.SelectCommand.Parameters.AddWithValue("@InspectionDueDate", dd);
                                    System.Data.DataTable dddt = new System.Data.DataTable();
                                    pp.Fill(dddt);
                                }
                            }
                            if (item.LooseETypeId == 7)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType=" + 7 + "", sc.con);
                                System.Data.DataTable dt = new System.Data.DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    DateTime inspectduedate = Convert.ToDateTime(MooringInspect.InspectDate).AddDays(near);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }
                                    string dd = Convert.ToDateTime(inspectduedate).ToString("yyyy-MM-dd");
                                    //SqlDataAdapter pp = new SqlDataAdapter("update ChafeGuard set InspectionDueDate ='" + dd + "' where ID=" + item.Id + "", sc.con);
                                    //System.Data.DataTable dddt = new System.Data.DataTable();
                                    //pp.Fill(dddt);
                                    SqlDataAdapter pp = new SqlDataAdapter("UpdateInspDueDtChafeGuard", sc.con);
                                    pp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    pp.SelectCommand.Parameters.AddWithValue("@Id", item.Id);
                                    pp.SelectCommand.Parameters.AddWithValue("@InspectionDueDate", dd);
                                    System.Data.DataTable dddt = new System.Data.DataTable();
                                    pp.Fill(dddt);
                                }
                            }
                            if (item.LooseETypeId == 8)
                            {
                                SqlDataAdapter adp = new SqlDataAdapter("select * from tblLooseEquipInspectionSetting where EquipmentType=" + 8 + "", sc.con);
                                System.Data.DataTable dt = new System.Data.DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    decimal perchk = Convert.ToDecimal(dt.Rows[0]["InspectionFrequency"]) * 30 / 100;
                                    perchk = perchk * 100;
                                    int near = Convert.ToInt32(perchk);
                                    DateTime inspectduedate = Convert.ToDateTime(MooringInspect.InspectDate).AddDays(near);

                                    DateTime crntdt = DateTime.Now;
                                    if (inspectduedate <= crntdt)
                                    {
                                        inspectduedate = DateTime.Now;
                                    }
                                    string dd = Convert.ToDateTime(inspectduedate).ToString("yyyy-MM-dd");
                                    //SqlDataAdapter pp = new SqlDataAdapter("update WinchBreakTestKit set InspectionDueDate ='" + dd + "' where ID=" + item.Id + "", sc.con);
                                    //System.Data.DataTable dddt = new System.Data.DataTable();
                                    //pp.Fill(dddt);

                                    SqlDataAdapter pp = new SqlDataAdapter("UpdateInspDueDtWinchBreakTestKit", sc.con);
                                    pp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    pp.SelectCommand.Parameters.AddWithValue("@Id", item.Id);
                                    pp.SelectCommand.Parameters.AddWithValue("@InspectionDueDate", dd);
                                    System.Data.DataTable dddt = new System.Data.DataTable();
                                    pp.Fill(dddt);
                                }
                            }
                            //}
                        }
                        catch (Exception ex) { }









                        sc.LooseEquipInspection.Add(ss);


                        if (item.Condition == "Not Acceptable")
                        {
                            if (MI1 == null && MI2 == null)
                            {
                                MainViewModelWorkHours.CommonValue = true;
                                System.Windows.MessageBox.Show("Photo Can not be blank for  number '" + item.Number + "' ", "LooseEq Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }
                            else
                            {



                                var notification = "Loose Equipment condition Not Acceptable of " + item.looseequipmenttype + " on CertificateNumber - '" + item.Number + " - " + item.UniqueId + "'";

                                //var notification = "";
                                NotificationsClass noti = new NotificationsClass();
                                noti.Acknowledge = false;
                                noti.AckRecord = "Not yet acknowledged";
                                noti.Notification = notification;
                                noti.NotificationType = 1;
                                noti.RopeId = 0;
                                noti.IsActive = true;
                                // noti.NotificationDueDate = notidueMonth;
                                noti.CreatedDate = DateTime.Now;
                                noti.CreatedBy = "Admin";
                                sc.Notifications.Add(noti);
                            }
                        }
                    }
                }
            }

            if (insdtcheck == 0)
            {
                sc.SaveChanges();
                System.Windows.MessageBox.Show("Record saved successfully", "LooseEq Inspection", MessageBoxButton.OK, MessageBoxImage.Information);
                CancelIncspection();
            }
            else
            {
                MainViewModelWorkHours.CommonValue = true;
                System.Windows.MessageBox.Show("The inspection date is earlier than installed date for these Loose Equipments " + test + " , Kindly change the inspection date or installed dates of these Loose Equip from LooseEquip form", "Loose Eq. Inspection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
        }

        public static ObservableCollection<MooringLooseEquipInspectionClass> loadUserAccess = new ObservableCollection<MooringLooseEquipInspectionClass>();
        public ObservableCollection<MooringLooseEquipInspectionClass> LoadInspections
        {
            get
            {
                return loadUserAccess;
            }
            set
            {
                loadUserAccess = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));
            }
        }

        public static LooseETypeC sropetype;// = new Ropetypecombo();
        public LooseETypeC SRopeType
        {
            get
            {

                if (sropetype != null)
                {

                    GetMooringInspection(sropetype.Id, sropetype.LooseEquipmentType);

                    StaticHelper.LooseEId = sropetype.Id;
                    StaticHelper.TbName = sropetype.LooseEquipmentType;
                    //assignrope.Clear();
                    //assignrope = GetAssRope(data.WinchId);
                    RaisePropertyChanged("AssignRope");




                    OnPropertyChanged(new PropertyChangedEventArgs("MooringInspect"));
                }
                return sropetype;

            }
            set
            {

                sropetype = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SRopeType"));

                //var data = sc.AssignRopetoWinch.Where(x => x.RopeId == SRopeType.Id).FirstOrDefault();
            }
        }


        private static ObservableCollection<LooseETypeC> ropetype = new ObservableCollection<LooseETypeC>();
        public ObservableCollection<LooseETypeC> RopeType
        {
            get
            {
                return ropetype;
            }
            set
            {
                // var data = sc.AssignRopetoWinch.Where(x => x.Id == ropetype).FirstOrDefault();
                ropetype = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RopeType"));
            }
        }



        public class LooseETypeC
        {
            public int Id { get; set; }
            public string LooseEquipmentType { get; set; }
        }

        private ObservableCollection<LooseETypeC> GetRopeType()
        {
            ObservableCollection<LooseETypeC> AddRopeType = new ObservableCollection<LooseETypeC>();
            //var data = sc.MooringWinchRope.Select(x => new { x.Id, x.CertificateNumber }).ToList();

            LooseETypeC rop;

            SqlDataAdapter adp = new SqlDataAdapter("select * from LooseEType where id !=2", sc.con);
            System.Data.DataTable ds = new System.Data.DataTable();
            adp.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {


                rop = new LooseETypeC();
                rop.Id = (int)row["Id"];
                rop.LooseEquipmentType = (string)row["LooseEquipmentType"];
                AddRopeType.Add(rop);

            }




            return AddRopeType;
        }

        //public void GetRopeType()
        //{
        //    //ObservableCollection<LooseETypeC> AddRopeType = new ObservableCollection<LooseETypeC>();
        //    //var data = sc.MooringWinchRope.Select(x => new { x.Id, x.CertificateNumber }).ToList();
        //    //AddRopeType.Clear();
        //    LooseETypeC rop;

        //    SqlDataAdapter adp = new SqlDataAdapter("select * from LooseEType where id !=2", sc.con);
        //    System.Data.DataTable ds = new System.Data.DataTable();
        //    adp.Fill(ds);

        //    foreach (DataRow row in ds.Rows)
        //    {


        //        rop = new LooseETypeC();
        //        rop.Id = (int)row["Id"];
        //        rop.LooseEquipmentType = (string)row["LooseEquipmentType"];
        //       // AddRopeType.Add(rop);

        //    }

        //    OnPropertyChanged(new PropertyChangedEventArgs("AddRopeType"));


        //    // return AddRopeType;
        //}



        //private ObservableCollection<MooringRopeInspectionClass> GetMooringInspection(TotalInspections TotIns)
        //{

        //    DateTime dd = TotIns.InspectDate;
        //    MooringInspect.InspectDate = TotIns.InspectDate;
        //    MooringInspect.InspectBy = TotIns.InspectBy;

        //    string qry = "select * from MooringRopeInspection where InspectDate = '" + dd.ToShortDateString() + "'";  // 2019-07-12 00:00:00.000 in sql tbl
        //    SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
        //    DataTable datatbl = new DataTable();
        //    sda.Fill(datatbl);

        //    var ranklist = new ObservableCollection<MooringRopeInspectionClass>();

        //    for (int i = 0; i < datatbl.Rows.Count; i++)
        //    {
        //        ranklist.Add(new MooringRopeInspectionClass()
        //        {

        //            Id = Convert.ToInt32(datatbl.Rows[i]["Id"]),
        //            InspectBy = datatbl.Rows[i]["InspectBy"].ToString(),
        //            InspectDate = Convert.ToDateTime(datatbl.Rows[i]["InspectDate"]),
        //            AssignNumber = datatbl.Rows[i]["AssignNumber"].ToString(),
        //            Location = datatbl.Rows[i]["Location"].ToString(),
        //            RpoeType = datatbl.Rows[i]["RpoeType"].ToString(),
        //            Certi_No = datatbl.Rows[i]["Certi_No"].ToString(),

        //            ExternalRating_A = Convert.ToInt32(datatbl.Rows[i]["ExternalRating_A"]),
        //            InternalRating_A = Convert.ToInt32(datatbl.Rows[i]["InternalRating_A"]),
        //            AverageRating_A = Convert.ToInt32(datatbl.Rows[i]["AverageRating_A"]),
        //            LengthOFAbrasion_A = Convert.ToDecimal(datatbl.Rows[i]["LengthOFAbrasion_A"]),
        //            DistanceOutboard_A = Convert.ToDecimal(datatbl.Rows[i]["DistanceOutboard_A"]),
        //            CutYarnCount_A = Convert.ToDecimal(datatbl.Rows[i]["CutYarnCount_A"]),
        //            LengthOFGlazing_A = Convert.ToDecimal(datatbl.Rows[i]["LengthOFGlazing_A"]),

        //            ExternalRating_B = Convert.ToInt32(datatbl.Rows[i]["ExternalRating_B"]),
        //            InternalRating_B = Convert.ToInt32(datatbl.Rows[i]["InternalRating_B"]),
        //            AverageRating_B = Convert.ToInt32(datatbl.Rows[i]["AverageRating_B"]),
        //            LengthOFAbrasion_B = Convert.ToDecimal(datatbl.Rows[i]["LengthOFAbrasion_B"]),
        //            DistanceOutboard_B = Convert.ToDecimal(datatbl.Rows[i]["DistanceOutboard_B"]),
        //            CutYarnCount_B = Convert.ToDecimal(datatbl.Rows[i]["CutYarnCount_B"]),
        //            LengthOFGlazing_B = Convert.ToDecimal(datatbl.Rows[i]["LengthOFGlazing_B"]),

        //            Chafe_guard_condition = datatbl.Rows[i]["Chafe_guard_condition"].ToString(),

        //            Twist = Convert.ToInt32(datatbl.Rows[i]["Twist"])
        //        });


        //    }

        //    return ranklist;
        //}

        public void GetMooringInspection(int id, string name)
        {
            LoadInspections.Clear();

            DateTime dd = DateTime.Now.Date;

            string qry = "LooseEquipInspection";
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);

            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@id", id);
            sda.SelectCommand.Parameters.AddWithValue("@table_name", name);
            System.Data.DataTable datatbl = new System.Data.DataTable();
            sda.Fill(datatbl);

            if (name == "Chafe Guard")
            {
                for (int i = 0; i < datatbl.Rows.Count; i++)
                {
                    LoadInspections.Add(new MooringLooseEquipInspectionClass()
                    {
                        Id = Convert.ToInt32(datatbl.Rows[i]["id"]),
                        InspectDate = dd,
                        LooseETypeId = 7,
                        looseequipmenttype = "(" + datatbl.Rows[i]["RNumber"].ToString() + ") Chafe Guard",
                           Number = datatbl.Rows[i]["number"].ToString(),
                           //Number = datatbl.Rows[i]["UniqueId"].ToString(),
                           UniqueId = datatbl.Rows[i]["UniqueId"].ToString(),
                        Condition = "",
                        Remarks = ""
                    });


                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));
            }

            //  var ranklist = new ObservableCollection<MooringLooseEquipInspectionClass>();
            else if (name == "Winch Brake Test Kit")
            {
                for (int i = 0; i < datatbl.Rows.Count; i++)
                {
                    LoadInspections.Add(new MooringLooseEquipInspectionClass()
                    {
                        Id = Convert.ToInt32(datatbl.Rows[i]["id"]),
                        InspectDate = dd,
                        LooseETypeId = 8,
                        looseequipmenttype = "(" + datatbl.Rows[i]["RNumber"].ToString() + ") Winch Break Test Kit",
                        Number = datatbl.Rows[i]["number"].ToString(),
                        UniqueId = datatbl.Rows[i]["UniqueId"].ToString(),
                        Condition = "",
                        Remarks = ""
                    });


                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));
            }
            else
            {
                for (int i = 0; i < datatbl.Rows.Count; i++)
                {
                    LoadInspections.Add(new MooringLooseEquipInspectionClass()
                    {
                        Id = Convert.ToInt32(datatbl.Rows[i]["id"]),
                        InspectDate = dd,
                        LooseETypeId = Convert.ToInt32(datatbl.Rows[i]["looseetypeid"]),
                        looseequipmenttype = "(" + datatbl.Rows[i]["RNumber"].ToString() + ") " + datatbl.Rows[i]["looseequipmenttype"].ToString(),
                        Number = datatbl.Rows[i]["number"].ToString(),
                        UniqueId = datatbl.Rows[i]["UniqueId"].ToString(),
                        Condition = "",
                        Remarks = ""
                    });


                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadInspections"));
                //return ranklist;
            }
        }

        //public ObservableCollection<string> ComboItems { get; set; }
        //private string comboValue;
        //public string ComboValue
        //{
        //       get { return comboValue; }
        //       set
        //       {
        //              if (comboValue != value)
        //              {
        //                     comboValue = value;
        //                     OnPropertyChanged(new PropertyChangedEventArgs("ComboValue"));
        //              }
        //       }
        //}


        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }

        public static MooringLooseEquipInspectionClass _mooringInspact = new MooringLooseEquipInspectionClass();

        public MooringLooseEquipInspectionClass MooringInspect
        {
            get { return _mooringInspact; }
            set
            {
                _mooringInspact = value;
                RaisePropertyChanged("MooringInspect");
            }
        }

        private void CancelIncspection()
        {
            MainViewModelWorkHours.CommonValue = false;
            var lostdata = new ObservableCollection<MooringLooseEquipInspectionClass>(sc.LooseEquipInspection.ToList());
            LooseEquipInspectionListViewModel cc = new LooseEquipInspectionListViewModel(lostdata);

            ChildWindowManager.Instance.CloseChildWindow();
        }
        public static Nullable<DateTime> _EndtoEndDoneDate = null;
        public Nullable<DateTime> EndtoEndDoneDate
        {
            get
            {
                if (_EndtoEndDoneDate == null)
                {
                    _EndtoEndDoneDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                _mooringInspact.InspectDate = (DateTime)_EndtoEndDoneDate;
                return _EndtoEndDoneDate;
            }
            set
            {
                _EndtoEndDoneDate = value;
                RaisePropertyChanged("EndtoEndDoneDate");
            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
