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
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using WorkShipVersionII.Commands;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{

    public class MooringWinchRopeViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private static int itemPerPage = 5;
        private int itemcount;
        private BackgroundWorker _Export;
        public ICommand HelpCommand { get; private set; }
        public MooringWinchRopeViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                _Export = new BackgroundWorker();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            downloadCommand = new RelayCommand<MooringWinchRopeClass>(Downloadfile);
            editCommand = new RelayCommand<MooringWinchRopeClass>(EditMooringWinch);
            deleteCommand = new RelayCommand<MooringWinchRopeClass>(DeleteMooringWinch);
            SearchCommand = new RelayCommand(Searchmethod);

            this._ExportCommands = new RelayCommand(() => _Export.RunWorkerAsync(), () => !_Export.IsBusy);
            this._Export.DoWork += new DoWorkEventHandler(ExportMethod);
            //LoadMooringWinchList.Clear();

            // LoadMooringWinchList = sc.MooringWinch.ToList();
            //var data = sc.MooringWinch.ToList();
            // sc.ObservableCollectionList(LoadMooringWinchList, GetMooringWinchList());
            //GetMooringWinchRopeList();
            // OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringWinchList"));
            GetMooringWinchRopeList();
            GetMooringWinchRopeList2();

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));


            NextCommand = new NextPageCommandMooringRope(this);
            PreviousCommand = new PreviousPageCommandMooringRope(this);
            FirstCommand = new FirstPageCommandMooringRope(this);
            LastCommand = new LastPageCommandMooringRope(this);

        }

        public MooringWinchRopeViewModel(ObservableCollection<MooringWinchRopeClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            downloadCommand = new RelayCommand<MooringWinchRopeClass>(Downloadfile);
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            LoadUserAccess.Clear();

            GetMooringWinchRopeList();
            GetMooringWinchRopeList2();


        }



        public static MooringWinchRopeClass _MooringWinchRope = new MooringWinchRopeClass();
        public MooringWinchRopeClass MooringWinchRope
        {
            get
            {
                return _MooringWinchRope;
            }
            set
            {
                _MooringWinchRope = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MooringWinchRope"));
            }
        }
        private void Searchmethod()
        {
            try
            {
                string portname = _MooringWinchRope.CertificateNumber;


                if (portname != null && SDateFrom == null)
                {

                    //GetMooringOperationBirthD(portname, facilityname);

                }
                if (SDateFrom != null && portname == "")
                {
                    if (SDateTo != null)
                    {
                        //GetMooringOperationBirthD((DateTime)SDateFrom, (DateTime)SDateTo);
                    }    
                    else
                    {
                        System.Windows.MessageBox.Show("Please Choose DateTo !", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                if (portname != "" && SDateFrom != null && SDateTo != null)
                {
                    //GetMooringOperationBirthD(portname, facilityname, (DateTime)SDateFrom, (DateTime)SDateTo);
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public static Nullable<DateTime> _SDateFrom = null;
        public Nullable<DateTime> SDateFrom
        {
            get
            {
                if (_SDateFrom == null)
                    _SDateFrom = null;

                return _SDateFrom;
            }
            set
            {
                _SDateFrom = value;
                RaisePropertyChanged("SDateFrom");
            }
        }

        public static Nullable<DateTime> _SDateTo = null;
        public Nullable<DateTime> SDateTo
        {
            get
            {
                if (_SDateTo == null)
                    _SDateTo = null;


                return _SDateTo;
            }
            set
            {
                _SDateTo = value;
                RaisePropertyChanged("SDateTo");
            }
        }


        public static Nullable<DateTime> _DateFrom = null;
        public Nullable<DateTime> DateFrom
        {
            get
            {
                if (_DateFrom == null)
                {

                    _DateFrom = null;
                }
                //_AddCrewDetail.ServiceFrom = (DateTime)_MyServiceFrom;
                return _DateFrom;
            }
            set
            {
                _DateFrom = value;
                RaisePropertyChanged("DateFrom");
            }
        }

        public static Nullable<DateTime> _DateTo = null;
        public Nullable<DateTime> DatesTo
        {
            get
            {
                if (_DateTo == null)
                {

                    _DateTo = null;
                }
                //_AddCrewDetail.ServiceTo = (DateTime)_MyServiceTo;
                return _DateTo;
            }
            set
            {
                _DateTo = value;
                RaisePropertyChanged("DatesTo");
            }
        }
        public ICommand SearchCommand { get; private set; }
        #region PaginationWork

        public ICommand PreviousCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand LastCommand { get; private set; }

        private static int _currentPageIndex;
        public int CurrentPageIndex
        {
            get
            {
                CurrentPage = _currentPageIndex;
                return _currentPageIndex;
            }
            set
            {
                _currentPageIndex = value;
                RaisePropertyChanged("CurrentPageIndex");
            }
        }




        private static int _CurrentPage;
        public int CurrentPage
        {
            get
            {
                if (_totalPages > 0)
                    return _currentPageIndex + 1;
                return _CurrentPage;
            }
            set
            {
                _CurrentPage = value;
                RaisePropertyChanged("CurrentPage");
            }
        }
        private static int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            private set
            {
                _totalPages = value;
                RaisePropertyChanged("TotalPages");
            }
        }

        public void ShowNextPage()
        {
            try
            {
                CurrentPageIndex++;
                ViewList.View.Refresh();

                //LoadUserAccess
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void ShowPreviousPage()
        {
            try
            {
                CurrentPageIndex--;
                ViewList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void ShowFirstPage()
        {
            try
            {
                CurrentPageIndex = 0;
                ViewList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public void ShowLastPage()
        {
            try
            {
                CurrentPageIndex = TotalPages - 1;
                ViewList.View.Refresh();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void CalculateTotalPages()
        {
            try
            {
                if (itemcount % itemPerPage == 0)
                {
                    TotalPages = (itemcount / itemPerPage);
                }
                else
                {
                    TotalPages = (itemcount / itemPerPage) + 1;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void View_Filter(object sender, FilterEventArgs e)
        {
            try
            {
                //int index = ((MOperationBirthDetail)e.Item).OPId + 1;
                //if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
                int index = ((MooringWinchRopeClass)e.Item).RowId - 1;
                if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        #endregion


        private ICommand downloadCommand;
        public ICommand DownloadCommand
        {
            get { return downloadCommand; }
            set { downloadCommand = value; }
        }

        private ICommand editCommand;
        public ICommand EditCommand
        {
            get { return editCommand; }
            set { editCommand = value; }
        }

        //ExportExcel
        private ICommand _ExportCommands;
        public ICommand ExportCommands
        {
            get
            {

                return _ExportCommands;
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }

        private static ObservableCollection<MooringWinchRopeClass> loadMooringWinchList = new ObservableCollection<MooringWinchRopeClass>();

        public ObservableCollection<MooringWinchRopeClass> LoadMooringWinchRopeList
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


        public static ObservableCollection<MooringWinchRopeClass> loadUserAccess = new ObservableCollection<MooringWinchRopeClass>();
        public ObservableCollection<MooringWinchRopeClass> LoadUserAccess
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



        // public static CollectionViewSource ViewList { get; set; }

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
        public static ObservableCollection<MooringWinchRopeClass> loadUserAccess1 = new ObservableCollection<MooringWinchRopeClass>();
        // public static ObservableCollection<MooringWinchRopeClass> loadUserAccess1 = new ObservableCollection<MooringWinchRopeClass>();
        public ObservableCollection<MooringWinchRopeClass> LoadUserAccess1
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

        private void Downloadfile(MooringWinchRopeClass obj)
        {
            try
            {
              // AddMooringWinchRopeViewModel vm = new AddMooringWinchRopeViewModel(obj);
                StaticHelper.ViewId = obj.Id;

                if (obj.AttachmentPath == "")
                {
                    System.Windows.Forms.MessageBox.Show("Attachment not found !", "Mooring Line", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    string ServerName = StaticHelper.ServerName;
                    string path = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + obj.AttachmentPath + "";

                    System.Diagnostics.Process.Start(path);
                    // ChildWindowManager.Instance.ShowChildWindow(new ViewTrainingAttachment());
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Attachment file not found on this path !", "Mooring Line", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //sc.ErrorLog(ex);
            }
        }
        //private ObservableCollection<MooringWinchRopeClass> GetMooringWinchRopeList()
        //{

        //    try
        //    {
        //        ObservableCollection<MooringWinchRopeClass> moringlist = new ObservableCollection<MooringWinchRopeClass>();

        //        var data = sc.MooringWinchRope.ToList();
        //        foreach (var item in data)
        //        {
        //            moringlist.Add(new MooringWinchRopeClass()
        //            {
        //                Id = item.Id,
        //                RopeType = item.RopeType,
        //                RopeConstruction = item.RopeConstruction,
        //                DiaMeter = item.DiaMeter,
        //                Length = item.Length,
        //                MBL = item.MBL,
        //                LDBF = item.LDBF,
        //                WLL = item.WLL,
        //                ManufacturerName = item.ManufacturerName,
        //                CertificateNumber = item.CertificateNumber,
        //                ReceivedDate = item.ReceivedDate,

        //                InstalledDate = item.InstalledDate,
        //                RopeTagging = item.RopeTagging,

        //                OutofServiceDate = item.OutofServiceDate,
        //                ReasonOutofService = item.ReasonOutofService,
        //                DamageObserved = item.DamageObserved,
        //                MooringOperation = item.MooringOperation,

        //            });


        //        }
        //        return moringlist;
        //    }
        //    catch (Exception ex)
        //    {
        //        sc.ErrorLog(ex);
        //        return null;
        //    }

        //}
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
        private void ExportMethod1()
        {
            try

            {
                DataSet ds = null;
                ds = new DataSet("General");
                ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

                // string qry = "GetMooringRopeDetailListExcel";

                string qry = @"Select * into #tbtemp from ( select a.Id, m.ropetype as [Line Type],a.ropeconstruction as [Line Construction],a.diameter as [Diameter],a.Length as [Length(mtrs)],a.MBL as [MBL(T)],a.LDBF as [LDBF(T)],a.WLL as [WLL(T)],k.Name as ManufacturerName,a.CertificateNumber as [Certificate Number],a.UniqueID as [Unique Identification No],
a.ReceivedDate as [Received Date],a.InstalledDate,a.RopeTagging as [Line Tagging],a.StartCounterHours as [Existing running hours (Start Counter)],a.Remarks
from MooringRopeDetail a left join tblCommon k on a.ManufacturerId=k.Id inner join MooringRopeType m on a.ropetypeid=m.id
where OutofServiceDate is null and a.DeleteStatus=0 and a.ropetail=0 ) as dd
select * from #tbtemp order by InstalledDate desc
select * into #tbtemp2 from( select a.Id,m.ropetype as [Line Type],a.ropeconstruction as [Line Construction],a.diameter as [Diameter],
a.Length as [Length(mtrs)],a.MBL as [MBL(T)],a.LDBF as [LDBF(T)],a.WLL as [WLL(T)],k.Name as ManufacturerName,a.CertificateNumber as [Certificate Number],a.UniqueID as [Unique Identification No],a.ReceivedDate ,CASE
WHEN a.InstalledDate IS null THEN 'Not Assigned'
WHEN a.InstalledDate IS Not null THEN convert(varchar, a.InstalledDate , 106)
end as [Installed Date],a.RopeTagging as [Line Tagging], a.Remarks,a.StartCounterHours as [Existing running hours (Start Counter)],a.InstalledDate
from MooringRopeDetail a inner join tblCommon k on a.ManufacturerId=k.Id inner join MooringRopeType m on a.ropetypeid=m.id

where a.deletestatus=0 and a.OutofServiceDate is not null and a.DeleteStatus=0 and a.ropetail=0)as hh

select * from #tbtemp2 order by InstalledDate desc

drop table #tbtemp
drop table #tbtemp2";
                //string qry = "Select RopeType as [Line Type], RopeConstruction as [Line Construction],Diameter as [Diameter(mm], Length as [Length(mtrs)],MBL as [MBL(T)],LDBF as [LDBF(T)],WLL as [WLL(T)],Name as [Manufacturer Name], CertificateNumber as [Certificate Number],UniqueID as [Unique Identification No],ReceivedDate as [Received Date],RopeTagging as [Line Tagging],StartCounterHours as [Existing running hours (Start Counter)],Remarks as [Remarks] from MooringRopeDetail INNER JOIN MooringRopeType ON MooringRopeDetail.RopeTypeId=MooringRopeType.Id INNER JOIN tblCommon On MooringRopeDetail.ManufacturerId=tblCommon.Id";
                SqlCommand cmd = new SqlCommand(qry, sc.con);
                //SqlCommand cmd = new SqlCommand("GetMooringRopeDetailList", sc.con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@RopeTail", 0);
                //DataTable dt = new DataTable();
                DataSet dt = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                dt.Tables[0].TableName = "ActiveLineDetail";
                dt.Tables[1].TableName = "DiscardedLineDetail";

                ds = dt;

                cmd.Dispose();
                adp.Dispose();
                dt.Dispose();

                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.FileName = "MooringLineDetails_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
                sfd.DefaultExt = "xlsx";
                sfd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (sfd.ShowDialog() == true)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch
                        {
                            System.Windows.MessageBox.Show("It seems that the earlier excel file is open, please close the excel file and download again to replace !", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }



                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (DataTable item in ds.Tables)
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
                        System.Windows.MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);



                    }
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void GetMooringWinchRopeList1()
        {

            try
            {
                LoadUserAccess.Clear();

                var data = sc.MooringWinchRope.Where(x => x.RopeTail == 0 && x.DeleteStatus == false).ToList();
                foreach (var item in data)
                {
                    LoadUserAccess.Add(new MooringWinchRopeClass()
                    {
                        Id = item.Id,
                        RopeTypeId = item.RopeTypeId,
                        RopeType = sc.MooringRopeType.Where(x => x.Id == item.RopeTypeId).Select(x => x.RopeType).SingleOrDefault(),
                        RopeConstruction = item.RopeConstruction,
                        DiaMeter = item.DiaMeter,
                        Length = item.Length,
                        MBL = item.MBL,
                        LDBF = item.LDBF,
                        WLL = item.WLL,
                        ManufacturerId = item.ManufacturerId,
                        ManufacturerName = sc.CommonManuF.Where(x => x.Id == item.ManufacturerId).Select(x => x.Name).SingleOrDefault(),
                        CertificateNumber = item.CertificateNumber,
                        ReceivedDate = item.ReceivedDate,

                        InstalledDate = item.InstalledDate,
                        RopeTagging = item.RopeTagging,

                        OutofServiceDate = item.OutofServiceDate,
                        ReasonOutofService = item.ReasonOutofService,
                        DamageObserved = item.DamageObserved,
                        MooringOperation = item.MooringOperation,
                        //RopeAttachment = item.RopeAttachment,
                        Location = sc.AssignRopetoWinch.Where(x => x.RopeId == item.Id).Select(x => x.AssignedLocation).SingleOrDefault(),

                        WinchId = sc.AssignRopetoWinch.Where(x => x.RopeId == item.Id).Select(x => x.WinchId).SingleOrDefault(),

                        Remarks = item.Remarks,
                        CurrentRunningHours = item.CurrentRunningHours
                        //AssignedWinch=sc.MooringWinch.Where(x=> x.Id==WinchId).se

                    });


                }

                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
                // return moringlist;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                // return null;
            }

        }

        public void GetMooringWinchRopeList()
        {
            try
            {
                LoadUserAccess.Clear();
                SqlCommand cmd = new SqlCommand("GetMooringRopeDetailList", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RopeTail", 0);
                cmd.Parameters.AddWithValue("@AttachedText", "Line");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count < StaticHelper.MinimumRope)
                {
                    Below21RopesAtDeleteTime();
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    LoadUserAccess.Add(new MooringWinchRopeClass()
                    {
                        // var data = Convert.ToDateTime(row["StartOn"].ToString()).ToString("MMM dd").ToString()
                        Id = (int)row["Id"],
                        RopeTail = 0,
                        //AssignedWinch = (string)row["AssignedWinch"],
                        CertificateNumber = (string)row["CertificateNumber"],
                        UniqueID = (row["UniqueID"] == DBNull.Value) ? "" : row["UniqueID"].ToString(),
                        LDBF = (decimal)row["LDBF"],
                        //MBL = (decimal)row["MBL"],
                        DiaMeter = (decimal)row["DiaMeter"],
                        Length = (decimal)row["Length"],
                        WLL = (decimal)row["WLL"],
                        ReceivedDate = (DateTime)row["ReceivedDate"],
                        RopeTagging = (string)row["RopeTagging"],
                        RopeConstruction = (string)row["RopeConstruction"],
                        //InstalledDate = (DateTime)row["InstalledDate"],
                        RopeType = (string)row["RopeType"],
                        RopeTypeId = (int)row["RopeTypeId"],
                        ManufacturerId = (int)row["ManufacturerId"],
                        //InstalledDate1 = ((DateTime)row["InstalledDate"]).ToString("d MMM, yyyy"),

                        InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),
                        //InstalledDate = Convert.tod (row["InstalledDate"] == DBNull.Value) ? null : row["InstalledDate"].ToString(),
                        InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? DateTime.Now : row["InstalledDate"]),
                        //InstalledDate1 = (row["InstalledDate"] == DBNull.Value) ? string.Empty : row["InstalledDate"].ToString(),
                        //CurrentRunningHours = Convert.ToInt32(row["CurrentRunningHours"] == DBNull.Value ? null : row["CurrentRunningHours"]),
                        //StartCounterHours = Convert.ToInt32(row["StartCounterHours"] == DBNull.Value ? null : row["StartCounterHours"]),

                        CurrentRunningHours = Convert.ToDecimal(row["CurrentRunningHours"] == DBNull.Value ? null : row["CurrentRunningHours"]),
                        StartCounterHours = Convert.ToDecimal(row["StartCounterHours"] == DBNull.Value ? null : row["StartCounterHours"]),

                        Remarks = (row["Remarks"] == DBNull.Value) ? string.Empty : row["Remarks"].ToString(),
                        ManufacturerName = (row["ManufacturerName"] == DBNull.Value) ? string.Empty : row["ManufacturerName"].ToString(),
                        Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                        // UniqueID = (row["UniqueID"] == DBNull.Value) ? "" : row["UniqueID"].ToString(),
                        AssignedWinch = (row["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : row["AssignedWinch"].ToString(),
                        AttachmentPath=(row["attachment"]== DBNull.Value)? string.Empty : row["attachment"].ToString(),
                        AttachmentVisibility = (row["attachment"] == DBNull.Value) ? "Hidden" : "Visible",
                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }
        }


        public void GetMooringWinchRopeList2()
        {
            try
            {
                int rowid = 1;
                LoadUserAccess1.Clear();
                SqlCommand cmd = new SqlCommand("GetMooringRopeDetailList", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RopeTail", 0);
                cmd.Parameters.AddWithValue("@AttachedText", "Line");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[1].Rows.Count < StaticHelper.MinimumRope)
                {
                    Below21RopesAtDeleteTime();
                }

                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    LoadUserAccess1.Add(new MooringWinchRopeClass()
                    {
                        // var data = Convert.ToDateTime(row["StartOn"].ToString()).ToString("MMM dd").ToString()
                        RowId = rowid++,
                        Id = (int)row["Id"],
                        RopeTail = 0,
                        //AssignedWinch = (string)row["AssignedWinch"],
                        CertificateNumber = (string)row["CertificateNumber"],
                        UniqueID = (row["UniqueID"] == DBNull.Value) ? "" : row["UniqueID"].ToString(),
                        LDBF = (decimal)row["LDBF"],
                        //MBL = (decimal)row["MBL"],
                        DiaMeter = (decimal)row["DiaMeter"],
                        Length = (decimal)row["Length"],
                        WLL = (decimal)row["WLL"],
                        ReceivedDate = (DateTime)row["ReceivedDate"],
                        RopeTagging = (string)row["RopeTagging"],
                        RopeConstruction = (string)row["RopeConstruction"],
                        //InstalledDate = (DateTime)row["InstalledDate"],
                        RopeType = (string)row["RopeType"],
                        RopeTypeId = (int)row["RopeTypeId"],
                        ManufacturerId = (int)row["ManufacturerId"],
                        //InstalledDate1 = ((DateTime)row["InstalledDate"]).ToString("d MMM, yyyy"),


                        InstalledDate1 = (row["InstalledDate1"] == DBNull.Value) ? null : row["InstalledDate1"].ToString(),
                        InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? DateTime.Now : row["InstalledDate"]),
                        //InstalledDate = Convert.ToDateTime((row["InstalledDate"] == DBNull.Value) ? null : row["InstalledDate"]),
                        //CurrentRunningHours = Convert.ToInt32(row["CurrentRunningHours"] == DBNull.Value ? null : row["CurrentRunningHours"]),
                        //StartCounterHours = Convert.ToInt32(row["StartCounterHours"] == DBNull.Value ? null : row["StartCounterHours"]),

                        CurrentRunningHours = Convert.ToDecimal(row["CurrentRunningHours"] == DBNull.Value ? null : row["CurrentRunningHours"]),
                        StartCounterHours = Convert.ToDecimal(row["StartCounterHours"] == DBNull.Value ? null : row["StartCounterHours"]),

                        Remarks = (row["Remarks"] == DBNull.Value) ? string.Empty : row["Remarks"].ToString(),
                        ManufacturerName = (row["ManufacturerName"] == DBNull.Value) ? string.Empty : row["ManufacturerName"].ToString(),
                        Location = (row["Location"] == DBNull.Value) ? "Not Assigned" : row["Location"].ToString(),
                        AssignedWinch = (row["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : row["AssignedWinch"].ToString(),
                        AttachmentPath=(row["attachment"]==DBNull.Value)?string.Empty:row["attachment"].ToString(),
                        AttachmentVisibility = (row["attachment"] == DBNull.Value) ? "Hidden" : "Visible",
                    });
                }
                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess1"));

                //RaisePropertyChanged("LoadUserAccess1");



                //ViewList = new CollectionViewSource
                //{
                //       Source = LoadUserAccess1
                //};


                //ViewList.Filter += new FilterEventHandler(View_Filter);


                //itemcount = LoadUserAccess1.Count(); //sc.Notifications.Count();
                //CalculateTotalPages();
                //ViewList.View.Refresh();


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);

            }
        }


        private static string searchCrew;
        public string SearchCrew
        {
            get
            {

                if (!string.IsNullOrEmpty(searchCrew))
                {
                    var data = sc.GetMooringWinchRope(searchCrew).ToList();
                    LoadUserAccess.Clear();
                    sc.ObservableCollectionList(LoadUserAccess, data);
                    RaisePropertyChanged("LoadUserAccess");

                }
                else
                {
                    loadUserAccess.Clear();
                    RaisePropertyChanged("LoadUserAccess");
                    GetMooringWinchRopeList();
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

        private static string searchCrew1;
        public string SearchCrew1
        {
            get
            {

                if (!string.IsNullOrEmpty(searchCrew1))
                {
                    var data = sc.GetMooringWinchRope1(searchCrew1).ToList();

                    LoadUserAccess1.Clear();
                    sc.ObservableCollectionList(LoadUserAccess1, data);
                    RaisePropertyChanged("LoadUserAccess1");
                    //ViewList = new CollectionViewSource
                    //{
                    //    Source = LoadUserAccess1
                    //};


                    //ViewList.Filter += new FilterEventHandler(View_Filter);


                    //itemcount = LoadUserAccess1.Count(); //sc.Notifications.Count();
                    //CalculateTotalPages();
                    //ViewList.View.Refresh();


                }
                else
                {
                    loadUserAccess1.Clear();
                    RaisePropertyChanged("LoadUserAccess1");
                    GetMooringWinchRopeList2();
                }
                return searchCrew1;


            }

            set
            {
                searchCrew1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SearchCrew1"));

            }
        }

        //private CollectionViewSource _ViewList = new CollectionViewSource();
        //public CollectionViewSource ViewList
        //{
        //    get { return _ViewList; }
        //    set
        //    {
        //        _ViewList = value;
        //        RaisePropertyChanged("ViewList");
        //    }
        //}

        private void EditMooringWinch(MooringWinchRopeClass mw)
        {
            try
            {
                AddMooringWinchRopeViewModel vm = new AddMooringWinchRopeViewModel(mw);
                //ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchRopeView() { DataContext = vm });
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        static string msgcheck = "";
        static void connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            // this gets the print statements (maybe the error statements?)
            msgcheck = e.Message;


            string[] stringSeparators = new string[] { "\r\n" };
            string text = msgcheck;
            string[] lines = text.Split(stringSeparators, StringSplitOptions.None);
            // msgcheck = "\n" + e.Message;

            msgcheck = lines[0];

            if (msgcheck != "")
            {
                return;
            }
        }
        private void DeleteMooringWinch(MooringWinchRopeClass mw)
        {
            try
            {
                if (System.Windows.MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //SqlDataAdapter adp2 = new SqlDataAdapter("select * from MooringRopeInspection where RopeId=" + mw.Id + " and IsActive=1", sc.con);
                    //DataTable dt2 = new DataTable();
                    //adp2.Fill(dt2);

                    SqlDataAdapter adp2 = new SqlDataAdapter("select distinct OperationId from MOUsedWinchTbl where RopeId=" + mw.Id + "", sc.con);
                    //adp2.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //adp2.SelectCommand.Parameters.AddWithValue("@RopeId", mw.Id);
                    DataTable dt2 = new DataTable();
                    adp2.Fill(dt2);

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        int operationid = Convert.ToInt32(dt2.Rows[i][0]);

                        SqlDataAdapter adp22 = new SqlDataAdapter("select * from MOperationBirthDetail where opid=" + operationid + " and IsActive=1", sc.con);
                        DataTable dt22 = new DataTable();
                        adp22.Fill(dt22);

                        if (dt22.Rows.Count > 0)
                        {
                            System.Windows.MessageBox.Show("This line cannot be deleted, as already marked as operation, please delete from operation records first", "Mooring Line", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }

                    //sc.Entry(obj).State = EntityState.Detached;



                    //using (var command = new SqlCommand("DeleteCheckInMooringRopeDetail", sc.con))
                    //{
                    //    command.CommandType = CommandType.StoredProcedure;
                    //    command.Parameters.AddWithValue("@RopeId", mw.Id);
                    //    command.Parameters.AddWithValue("@Ropetail", "Line");
                    //    sc.con.Open();
                    //    // wire up an event handler to the connection.InfoMessage event
                    //    sc.con.InfoMessage += connection_InfoMessage;
                    //    var result = command.ExecuteNonQuery();
                    //    sc.con.Close();
                    //}


                    SqlDataAdapter adp228 = new SqlDataAdapter("DeleteCheckInMooringRopeDetail", sc.con);
                    adp228.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adp228.SelectCommand.Parameters.AddWithValue("@RopeId", mw.Id);
                    adp228.SelectCommand.Parameters.AddWithValue("@Ropetail", "Line");
                    //    command.Parameters.AddWithValue("@Ropetail", "Line");
                    DataTable dt228 = new DataTable();
                    adp228.Fill(dt228);

                    if (dt228.Rows.Count > 0)
                    {
                        msgcheck = dt228.Rows[0][0].ToString();
                    }

                    try
                    {
                        SqlDataAdapter adp223 = new SqlDataAdapter("select * from MooringRopeAttachment where ropeid='" + mw.Id + "' and LineResidual='Line' and RopeTail=0", sc.con);
                        DataTable dt223 = new DataTable();
                        adp223.Fill(dt223);
                        if (dt223.Rows.Count > 0)
                        {
                            string filename = dt223.Rows[0]["AttachmentPath"].ToString();

                            string ServerName = StaticHelper.ServerName;
                            string mypath = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + filename + "";

                            FileInfo file = new FileInfo(mypath);
                            if (file.Exists)//check file exsit or not  
                            {
                                file.Delete();


                                SqlDataAdapter adp221 = new SqlDataAdapter("delete from MooringRopeAttachment where ropeid='" + mw.Id + "' and LineResidual='Line' and RopeTail=0", sc.con);
                                DataTable dt221 = new DataTable();
                                adp221.Fill(dt221);

                            }
                        }
                    }
                    catch { }
                   


                    //sc.con.InfoMessage += delegate (object sender, SqlInfoMessageEventArgs e)
                    //{
                    //     ddd = "\n" + e.Message;
                    //};

                    //sc.con.InfoMessage += connection_InfoMessage;

                    //if (dt2.Rows.Count > 0)
                    if (msgcheck != "")
                    {
                        //MessageBox.Show("This line cannot be deleted, as already marked as inspected, please delete from inspection records first", "Mooring Line", MessageBoxButton.OK, MessageBoxImage.Information);
                        System.Windows.MessageBox.Show(msgcheck, "Mooring Line", MessageBoxButton.OK, MessageBoxImage.Information);
                        msgcheck = "";
                        return;
                    }
                    else
                    {

                        MooringWinchRopeClass findrank = sc.MooringWinchRope.Where(x => x.Id == mw.Id && x.DeleteStatus == false).FirstOrDefault();
                        if (findrank != null)
                        {
                            //sc.Entry(findrank).State = EntityState.Deleted;
                            //sc.SaveChanges();

                            var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == mw.Id && b.DeleteStatus == false);
                            if (result != null)
                            {

                                result.DeleteStatus = true;
                                result.ModifiedDate = DateTime.Now;
                                sc.SaveChanges();

                                Below21RopesAtDeleteTime();
                            }

                            try
                            {
                                SqlDataAdapter adp1 = new SqlDataAdapter("Deletenotifications", sc.con);
                                adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
                                adp1.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                                DataTable dt1 = new DataTable();
                                adp1.Fill(dt1);
                            }
                            catch { }


                            //SqlDataAdapter adp = new SqlDataAdapter("GetRopeIdinTable", sc.con);
                            //DataTable dt = new DataTable();
                            //adp.Fill(dt);
                            //for (int i = 0; i < dt.Rows.Count; i++)
                            //{
                            //    var tblname = dt.Rows[i]["TableName"];

                            //    SqlDataAdapter adp1 = new SqlDataAdapter("delete from " + tblname + " where ropeid=" + mw.RopeId + "", sc.con);
                            //    DataTable dt1 = new DataTable();
                            //    adp1.Fill(dt1);

                            //}



                            System.Windows.MessageBox.Show("Record deleted successfully ", "Delete Mooring Line", MessageBoxButton.OK, MessageBoxImage.Information);

                            //.....Refresh DataGrid........


                            var lostdata = new ObservableCollection<MooringWinchRopeClass>(sc.MooringWinchRope.Where(x => x.DeleteStatus == false).ToList());
                            MooringWinchRopeViewModel cc = new MooringWinchRopeViewModel(lostdata);


                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Record is not found ", "Delete Department ", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void Below21RopesAtDeleteTime()
        {
            try
            {   // minimum required of 21 Ropes ---------------------------------
                SqlDataAdapter adp = new SqlDataAdapter("select COUNT(*) from MooringRopeDetail where OutofServiceDate is null and RopeTail=0 and DeleteStatus=0", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count >= 0)
                {
                    int cnt = Convert.ToInt32(dt.Rows[0][0]);
                    if (cnt < StaticHelper.MinimumRope)
                    {
                        var notification = "Active lines below minimum required of " + StaticHelper.MinimumRope + " Lines including spare";

                        SqlDataAdapter act = new SqlDataAdapter("select COUNT(*) from Notifications where Notification='Active lines below minimum required of " + StaticHelper.MinimumRope + " Lines including spare'", sc.con);
                        DataTable dd = new DataTable();
                        act.Fill(dd);

                        int cntnoti = Convert.ToInt32(dd.Rows[0][0]);
                        if (cntnoti == 0)
                        {
                            NotificationsClass noti = new NotificationsClass();
                            noti.Acknowledge = false;
                            noti.AckRecord = "Not yet acknowledged";
                            //noti.AckRecord = "Please insert minimum of {21} active ropes";
                            noti.Notification = notification;
                            noti.RopeId = 0;
                            noti.IsActive = true;
                            noti.NotificationType = 1;
                            //noti.NotificationDueDate = notidueMonth;
                            noti.CreatedDate = DateTime.Now;
                            noti.CreatedBy = "Admin";
                            noti.NotificationAlertType = (int)NotificationAlertType.Minimum21RopeCount;
                            sc.Notifications.Add(noti);
                            sc.SaveChanges();

                            StaticHelper.AlarmFunction(1, true);
                        }

                        act.Dispose();
                        dd.Dispose();
                    }
                    else
                    {
                        // delete Above Notification
                        SqlDataAdapter act = new SqlDataAdapter("delete from Notifications where NotificationAlertType = 17", sc.con);
                        DataTable dd = new DataTable();
                        act.Fill(dd);
                        act.Dispose();
                        dd.Dispose();
                    }
                }

                dt.Dispose();
                adp.Dispose();


            }
            catch { }
        }




        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }
}
