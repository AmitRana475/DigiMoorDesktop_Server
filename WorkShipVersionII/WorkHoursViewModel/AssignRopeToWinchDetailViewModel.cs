using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Configuration;
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
using WorkShipVersionII.WorkHoursView;
using ClosedXML.Excel;
using System.IO;
using Microsoft.Win32;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class AssignRopeToWinchDetailViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private BackgroundWorker _ExportRopeToWinch;
        public ICommand HelpCommand { get; private set; }
        public AssignRopeToWinchDetailViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                _ExportRopeToWinch = new BackgroundWorker();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            viewCommand = new RelayCommand<AssignModuleToWinchClass>(ViewMooringWinch);
            editCommand = new RelayCommand<AssignModuleToWinchClass>(EditMooringWinch);
            deleteCommand = new RelayCommand<AssignModuleToWinchClass>(DeleteMooringWinch);
            changestatuscommand = new RelayCommand<AssignModuleToWinchClass>(ShifttoInactive);


            this._ExportRopeToWinchCommands = new RelayCommand(() => _ExportRopeToWinch.RunWorkerAsync(), () => !_ExportRopeToWinch.IsBusy);
            this._ExportRopeToWinch.DoWork += new DoWorkEventHandler(ExportRopetoWinchMethod);

            //StaticHelper.HelpFor = @"LMPR\rope\4.2.3  ASSIGN ROPE TO WINCH.htm";
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            GetAssignList();
            GetAssignList1();
            // OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));

        }
        //private void HelpMethod(string filename)
        //{

        //    try
        //    {

        //        // StaticHelper.HelpFor = @"Notification\3.0 CREW MANAGEMENT\3.0.1 CREW DETAIL.htm";
        //        var path = AppDomain.CurrentDomain.BaseDirectory + @"DigiMoorX7-Help\" + "DigiMoorX7.chm";
        //        System.Windows.Forms.Help.ShowHelp(null, path, ss);
        //    }
        //    catch
        //    {
        //    }
        //}
        public AssignRopeToWinchDetailViewModel(ObservableCollection<AssignModuleToWinchClass> ass)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            //LoadUserAccess.Clear();
            //LoadUserAccess1.Clear();
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            GetAssignList();
            GetAssignList1();

            //foreach (var item in ass)

            //{
            //    LoadUserAccess.Add(new AssignModuleToWinchClass() { Id = item.Id, AssignedNumber=item.AssignedNumber,CertificateNumber=item.CertificateNumber,CreatedDate=item.CreatedDate });
            //}

        }
        private static string searchCrew1;

        private ICommand changestatuscommand;
        public ICommand ChangeStatusCommand
        {
            get { return changestatuscommand; }
            set { changestatuscommand = value; }
        }
        private ICommand editCommand;
        public ICommand EditCommand
        {
            get { return editCommand; }
            set { editCommand = value; }
        }

        //ExportExcel
        private ICommand _ExportRopeToWinchCommands;
        public ICommand ExportRopeToWinchCommands
        {
            get
            {

                return _ExportRopeToWinchCommands;
            }
        }
        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }

        private static ObservableCollection<AssignModuleToWinchClass> loadMooringWinchList = new ObservableCollection<AssignModuleToWinchClass>();

        public ObservableCollection<AssignModuleToWinchClass> LoadMooringWinchList
        {
            get
            {
                return loadMooringWinchList;
            }
            set
            {
                loadMooringWinchList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadMooringWinchList"));

            }
        }


        public static ObservableCollection<AssignModuleToWinchClass> loadUserAccess = new ObservableCollection<AssignModuleToWinchClass>();
        public ObservableCollection<AssignModuleToWinchClass> LoadUserAccess
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

        public static ObservableCollection<AssignModuleToWinchClass> loadUserAccess1 = new ObservableCollection<AssignModuleToWinchClass>();
        public ObservableCollection<AssignModuleToWinchClass> LoadUserAccess1
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

        public void GetAssignList()
        {

            try
            {
                LoadUserAccess.Clear();
                //ObservableCollection<AssignModuleToWinchClass> moringlist = new ObservableCollection<AssignModuleToWinchClass>();

                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                //SqlDataAdapter adp = new SqlDataAdapter("select a.id,a.Outboard,a.RopeId,a.WinchId,a.CreatedBy, a.AssignedLocation,a.createddate, b.certificatenumber,c.assignednumber from AssignRopeToWinch a inner join MooringRopeDetail b on a.RopeId=b.Id inner join MooringWinchDetail c on a.WinchId=c.Id", con);

                SqlDataAdapter adp = new SqlDataAdapter("GetAssignRopeToWinch", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                // var data = sc.AssignRopetoWinch.ToList();          
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    LoadUserAccess.Add(new AssignModuleToWinchClass()
                    {
                        Id = (int)row["Id"],
                        AssignedNumber = (string)row["assignednumber"],
                        CertificateNumber = (string)row["certificatenumber"],
                        UniqueId = (string)row["UniqueId"],
                        //AssignedDate = (DateTime)row["AssignedDate"],
                        AssignedDate1 = (string)row["AssignedDate"],
                        //Lead = (row["Lead"] == DBNull.Value) ? "Not Assigned" : row["Lead"].ToString(),
                        //AssignedDate1 = ((DateTime)row["AssignedDate"]).ToString("yyyy-MM-dd"),
                        CreatedDate = (DateTime)row["createddate"],
                        RopeId = (int)row["RopeId"],
                        WinchId = (int)row["WinchId"],
                        CreatedBy = (string)row["Createdby"],
                        Status = (string)row["Status"]

                    });
                }

                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                //return null;
            }

        }

        public void GetAssignList1()
        {
            try
            {
                LoadUserAccess1.Clear();
                //ObservableCollection<AssignModuleToWinchClass> moringlist = new ObservableCollection<AssignModuleToWinchClass>();

                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                //SqlDataAdapter adp = new SqlDataAdapter("select a.id,a.Outboard,a.RopeId,a.WinchId,a.CreatedBy, a.AssignedLocation,a.createddate, b.certificatenumber,c.assignednumber from AssignRopeToWinch a inner join MooringRopeDetail b on a.RopeId=b.Id inner join MooringWinchDetail c on a.WinchId=c.Id", con);

                SqlDataAdapter adp = new SqlDataAdapter("GetAssignRopeToWinch", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                // var data = sc.AssignRopetoWinch.ToList();          
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    LoadUserAccess1.Add(new AssignModuleToWinchClass()
                    {
                        Id = (int)row["Id"],
                        AssignedNumber = (string)row["assignednumber"],
                        CertificateNumber = (string)row["certificatenumber"],

                        UniqueId = (string)row["UniqueId"],
                        //AssignedDate = (DateTime)row["AssignedDate"],

                        AssignedDate1 = (string)row["AssignedDate"],
                        //AssignedDate1 = ((DateTime)row["AssignedDate"]).ToString("yyyy-MM-dd"),
                        // Lead = (row["Lead"] == DBNull.Value) ? "Not Assigned" : row["Lead"].ToString(),
                        CreatedDate = (DateTime)row["createddate"],
                        RopeId = (int)row["RopeId"],
                        WinchId = (int)row["WinchId"],
                        CreatedBy = (string)row["Createdby"],
                        Status = (string)row["Status"]

                    });
                }

                OnPropertyChanged(new PropertyChangedEventArgs("LoadUserAccess1"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                //return null;
            }

        }
        //private static DBManager dm;
        //public DataSet GetHolidays()
        //{
        //    dm.Open();
        //    var data = dm.ExecuteDataSet(CommandType.StoredProcedure, "SPGetHolidays");
        //    dm.Close();

        //    return data;
        //}
        private void EditMooringWinch(AssignModuleToWinchClass mw)
        {
            try
            {
                AssignRopeToWinchViewModel vm = new AssignRopeToWinchViewModel(mw);
                ChildWindowManager.Instance.ShowChildWindow(new AssignRopeToWinchView() { DataContext = vm });
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void ViewMooringWinch(AssignModuleToWinchClass mw)
        {
            try
            {
                ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch(mw);
                StaticHelper.ViewId = mw.Id;
                StaticHelper.RopeTailId = 0;
                ChildWindowManager.Instance.ShowChildWindow(new ViewAssignRopetoWinch() { DataContext = vm });

                //ChildWindowManager.Instance.CloseChildWindow();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void lead_check(int ropeid, DateTime? assigneddate, int winchid)
        {
            try
            {
                if (winchid == 0)
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("select * from MOUsedWinchTbl where ropeid=" + ropeid + "", sc.con))
                    {
                        DataTable dt = new DataTable();
                        adp.Fill(dt);


                        if (dt.Rows.Count > 0)
                        {

                            //var leedd = sc.MooringWinch.Where(x => x.Id == winchid).Select(x => x.Lead).SingleOrDefault();

                            SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " group by ropeid,runninghours, Lead", sc.con);

                            //SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " and lead ='" + leedd + "' group by ropeid, Lead", sc.con);
                            DataTable dd1 = new DataTable();
                            pp1.Fill(dd1);

                            for (int i = 0; i < dd1.Rows.Count; i++)
                            //if (dd1.Rows.Count > 0)
                            {
                                int rpid = Convert.ToInt32(dd1.Rows[i]["RopeId"]);
                                string lead = dd1.Rows[i]["Lead"].ToString();
                                decimal rnghrs = Convert.ToDecimal(dd1.Rows[i]["RunningHours"]);

                                decimal rnghrs1 = 0;

                                SqlDataAdapter pp2 = new SqlDataAdapter("select * from WinchRotation where RopeId=" + rpid + " and Lead='" + lead + "'", sc.con);
                                DataTable dd2 = new DataTable();
                                pp2.Fill(dd2);
                                if (dd2.Rows.Count > 0)
                                {
                                    rnghrs1 = Convert.ToDecimal(dd2.Rows[0]["RunningHours"]);

                                    if (rnghrs == rnghrs1)
                                    {
                                        rnghrs = 0;
                                    }
                                    else
                                    {
                                        if (rnghrs1 > rnghrs)
                                        {
                                            rnghrs = rnghrs1 - rnghrs;
                                        }
                                        if (rnghrs1 < rnghrs)
                                        {
                                            rnghrs = rnghrs - rnghrs1;
                                        }

                                    }
                                }


                                if (rnghrs != 0)
                                {

                                    //SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                                    SqlDataAdapter pp = new SqlDataAdapter("InsertWinchRotation", sc.con);
                                    pp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    pp.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                    pp.SelectCommand.Parameters.AddWithValue("@AssignedDate", assigneddate);
                                    pp.SelectCommand.Parameters.AddWithValue("@WinchId", winchid);
                                    pp.SelectCommand.Parameters.AddWithValue("@Lead", lead);
                                    pp.SelectCommand.Parameters.AddWithValue("@RunningHours", rnghrs);
                                    pp.SelectCommand.Parameters.AddWithValue("@IsActive", true);
                                  
                                    DataTable dd = new DataTable();
                                    pp.Fill(dd);


                                    try
                                    {
                                        //SqlDataAdapter adpt = new SqlDataAdapter("update MooringRopeDetail set CurrentLeadRunningHours = 0 where ID=" + ropeid + "", sc.con);
                                        SqlDataAdapter adpt = new SqlDataAdapter("UpdatetCurrentLead", sc.con);
                                        adpt.SelectCommand.CommandType = CommandType.StoredProcedure;
                                        adpt.SelectCommand.Parameters.AddWithValue("@CrntLeadrngHrs", 0);
                                        adpt.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                        DataTable ddt = new DataTable();
                                        adpt.Fill(ddt);
                                    }
                                    catch { }
                                }
                                //if (rnghrs == 0)
                                //{

                                //    SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                                //    DataTable dd = new DataTable();
                                //    pp.Fill(dd);
                                //}
                            }


                        }
                        //else
                        //{
                        //    try
                        //    {
                        //        var leadcheck = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Lead).SingleOrDefault();
                        //        decimal rnghrs = 0;
                        //        SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + leadcheck + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                        //        DataTable dd = new DataTable();
                        //        pp.Fill(dd);
                        //    }
                        //    catch { }
                        //}
                    }
                }
                else
                {

                    using (SqlDataAdapter adp = new SqlDataAdapter("select * from MOUsedWinchTbl where ropeid=" + ropeid + "", sc.con))
                    {
                        DataTable dt = new DataTable();
                        adp.Fill(dt);


                        if (dt.Rows.Count > 0)
                        {

                            var leedd = sc.MooringWinch.Where(x => x.Id == winchid).Select(x => x.Lead).SingleOrDefault();
                            leedd = leedd.Replace(Environment.NewLine, "").Trim();
                            //SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " group by ropeid,runninghours, Lead", sc.con);

                            SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " and lead ='" + leedd + "' group by ropeid, Lead", sc.con);
                            DataTable dd1 = new DataTable();
                            pp1.Fill(dd1);

                            //for (int i = 0; i < dd1.Rows.Count; i++)
                            if (dd1.Rows.Count > 0)
                            {
                                int rpid = Convert.ToInt32(dd1.Rows[0]["RopeId"]);
                                string lead = dd1.Rows[0]["Lead"].ToString();
                                decimal rnghrs = Convert.ToDecimal(dd1.Rows[0]["RunningHours"]);

                                decimal rnghrs1 = 0;

                                SqlDataAdapter pp2 = new SqlDataAdapter("select * from WinchRotation where RopeId=" + rpid + " and Lead='" + lead + "'", sc.con);
                                DataTable dd2 = new DataTable();
                                pp2.Fill(dd2);
                                if (dd2.Rows.Count > 0)
                                {
                                    rnghrs1 = Convert.ToDecimal(dd2.Rows[0]["RunningHours"]);

                                    if (rnghrs == rnghrs1)
                                    {
                                        rnghrs = 0;
                                    }
                                    else
                                    {
                                        if (rnghrs1 > rnghrs)
                                        {
                                            rnghrs = rnghrs1 - rnghrs;
                                        }
                                        if (rnghrs1 < rnghrs)
                                        {
                                            rnghrs = rnghrs - rnghrs1;
                                        }

                                    }
                                }


                                if (rnghrs != 0)
                                {

                                   // SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                                    SqlDataAdapter pp = new SqlDataAdapter("InsertWinchRotation", sc.con);
                                    pp.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    pp.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                    pp.SelectCommand.Parameters.AddWithValue("@AssignedDate", assigneddate);
                                    pp.SelectCommand.Parameters.AddWithValue("@WinchId", winchid);
                                    pp.SelectCommand.Parameters.AddWithValue("@Lead", lead);
                                    pp.SelectCommand.Parameters.AddWithValue("@RunningHours", rnghrs);
                                    pp.SelectCommand.Parameters.AddWithValue("@IsActive", true);
                                    DataTable dd = new DataTable();
                                    pp.Fill(dd);


                                    try
                                    {
                                        //SqlDataAdapter adpt = new SqlDataAdapter("update MooringRopeDetail set CurrentLeadRunningHours = 0 where ID=" + ropeid + "", sc.con);
                                        //DataTable ddt = new DataTable();
                                        //adpt.Fill(ddt);

                                        SqlDataAdapter adpt = new SqlDataAdapter("UpdatetCurrentLead", sc.con);
                                        adpt.SelectCommand.CommandType = CommandType.StoredProcedure;
                                        adpt.SelectCommand.Parameters.AddWithValue("@CrntLeadrngHrs", 0);
                                        adpt.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                        DataTable ddt = new DataTable();
                                        adpt.Fill(ddt);
                                    }
                                    catch { }
                                }
                                //if (rnghrs == 0)
                                //{

                                //    SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                                //    DataTable dd = new DataTable();
                                //    pp.Fill(dd);
                                //}
                            }


                        }
                        //else
                        //{
                        //    try
                        //    {
                        //        var leadcheck = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Lead).SingleOrDefault();
                        //        decimal rnghrs = 0;
                        //        SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + leadcheck + "','" + rnghrs + "','" + true + "' ); ", sc.con);
                        //        DataTable dd = new DataTable();
                        //        pp.Fill(dd);
                        //    }
                        //    catch { }
                        //}
                    }


                }
            }
            catch (Exception ex) { }
        }
        //private void lead_check(int ropeid, DateTime? assigneddate, int winchid)
        //{
        //    try
        //    {
        //        using (SqlDataAdapter adp = new SqlDataAdapter("select * from MOUsedWinchTbl where ropeid=" + ropeid + "", sc.con))
        //        {
        //            DataTable dt = new DataTable();
        //            adp.Fill(dt);


        //            if (dt.Rows.Count > 0)
        //            {

        //                //var leedd = sc.MOUsedWinchTbl.Where(x => x.Id == winchid).Select(x => x.Lead).SingleOrDefault();

        //                SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " group by ropeid,runninghours, Lead", sc.con);

        //               // SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " and lead ='" + leedd + "' group by ropeid, Lead", sc.con);
        //                DataTable dd1 = new DataTable();
        //                pp1.Fill(dd1);

        //                for (int i = 0; i < dd1.Rows.Count; i++)
        //                {
        //                    int rpid = Convert.ToInt32(dd1.Rows[i]["RopeId"]);
        //                    string lead = dd1.Rows[i]["Lead"].ToString();
        //                    decimal rnghrs = Convert.ToDecimal(dd1.Rows[i]["RunningHours"]);

        //                    decimal rnghrs1 = 0;

        //                    SqlDataAdapter pp2 = new SqlDataAdapter("select * from WinchRotation where RopeId=" + rpid + " and Lead='" + lead + "'", sc.con);
        //                    DataTable dd2 = new DataTable();
        //                    pp2.Fill(dd2);
        //                    if (dd2.Rows.Count > 0)
        //                    {
        //                        rnghrs1 = Convert.ToDecimal(dd2.Rows[0]["RunningHours"]);

        //                        if (rnghrs == rnghrs1)
        //                        {
        //                            rnghrs = 0;
        //                        }
        //                        else
        //                        {
        //                            if (rnghrs1 > rnghrs)
        //                            {
        //                                rnghrs = rnghrs1 - rnghrs;
        //                            }
        //                            if (rnghrs1 < rnghrs)
        //                            {
        //                                rnghrs = rnghrs - rnghrs1;
        //                            }

        //                        }
        //                    }


        //                    if (rnghrs != 0)
        //                    {

        //                        SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
        //                        DataTable dd = new DataTable();
        //                        pp.Fill(dd);
        //                    }
        //                    //if (rnghrs == 0)
        //                    //{

        //                    //    SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
        //                    //    DataTable dd = new DataTable();
        //                    //    pp.Fill(dd);
        //                    //}
        //                }


        //            }
        //            else
        //            {
        //                try
        //                {
        //                    var leadcheck = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Lead).SingleOrDefault();
        //                    decimal rnghrs = 0;
        //                    SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + leadcheck + "','" + rnghrs + "','" + true + "' ); ", sc.con);
        //                    DataTable dd = new DataTable();
        //                    pp.Fill(dd);
        //                }
        //                catch { }
        //            }
        //        }


        //    }
        //    catch (Exception ex) { }
        //}

        //private void lead_check(int ropeid, DateTime? assigneddate, int winchid)
        //{
        //    try
        //    {
        //        bool Isactive = false;
        //        using (SqlDataAdapter adp = new SqlDataAdapter("select * from MOUsedWinchTbl where ropeid=" + ropeid + "", sc.con))
        //        {
        //            DataTable dt = new DataTable();
        //            adp.Fill(dt);


        //            if (dt.Rows.Count > 0)
        //            {

        //                //SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " group by ropeid,runninghours, Lead", sc.con);

        //                SqlDataAdapter pp1 = new SqlDataAdapter("select RopeId,SUM(runninghours) as RunningHours, Lead  from MOUsedWinchTbl where RopeId=" + ropeid + " group by ropeid, Lead", sc.con);
        //                DataTable dd1 = new DataTable();
        //                pp1.Fill(dd1);
        //                int counter = dd1.Rows.Count;
        //                for (int i = 0; i < counter; i++)
        //                {
        //                    int rpid = Convert.ToInt32(dd1.Rows[i]["RopeId"]);
        //                    string lead = dd1.Rows[i]["Lead"].ToString();
        //                    decimal rnghrs = Convert.ToDecimal(dd1.Rows[i]["RunningHours"]);

        //                    decimal rnghrs1 = 0;

        //                    SqlDataAdapter pp2 = new SqlDataAdapter("select * from WinchRotation where RopeId=" + rpid + " and Lead='" + lead + "'", sc.con);
        //                    DataTable dd2 = new DataTable();
        //                    pp2.Fill(dd2);
        //                    if (dd2.Rows.Count > 0)
        //                    {
        //                        rnghrs1 = Convert.ToDecimal(dd2.Rows[0]["RunningHours"]);

        //                        if (rnghrs == rnghrs1)
        //                        {
        //                            rnghrs = 0;
        //                        }
        //                        else
        //                        {
        //                            if (rnghrs1 > rnghrs)
        //                            {
        //                                rnghrs = rnghrs1 - rnghrs;
        //                            }
        //                            if (rnghrs1 < rnghrs)
        //                            {
        //                                rnghrs = rnghrs - rnghrs1;
        //                            }

        //                        }
        //                    }


        //                    if (rnghrs != 0)
        //                    {

        //                        SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
        //                        DataTable dd = new DataTable();
        //                        pp.Fill(dd);



        //                        try
        //                        {
        //                            SqlDataAdapter adpt = new SqlDataAdapter("update MooringRopeDetail set CurrentLeadRunningHours = 0 where ID=" + ropeid + "", sc.con);
        //                            DataTable ddt = new DataTable();
        //                            adpt.Fill(ddt);
        //                        }
        //                        catch { }

        //                        // break;
        //                    }
        //                    if (rnghrs == 0)
        //                    {
        //                        SqlDataAdapter pp11 = new SqlDataAdapter("select * from WinchRotation where RopeId=" + Convert.ToInt32(dd1.Rows[i]["RopeId"]) + " and lead='" + dd1.Rows[i]["lead"].ToString() + "' and RunningHours=" + Convert.ToDecimal(dd1.Rows[i]["RunningHours"]) + "", sc.con);
        //                        DataTable dd11 = new DataTable();
        //                        pp11.Fill(dd11);

        //                        if (dd11.Rows.Count == 0)
        //                        {

        //                            SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + lead + "','" + rnghrs + "','" + true + "' ); ", sc.con);
        //                            DataTable dd = new DataTable();
        //                            pp.Fill(dd);
        //                        }
        //                        else if(Isactive==false && i == counter - 1)
        //                        {

        //                            var leadcheck = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Lead).SingleOrDefault();

        //                            SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + leadcheck + "','" + rnghrs + "','" + true + "' ); ", sc.con);
        //                            DataTable dd = new DataTable();
        //                            pp.Fill(dd);
        //                            Isactive = true;
        //                        }

        //                    }
        //                }


        //            }
        //            else
        //            {
        //                try
        //                {
        //                    var leadcheck = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.Lead).SingleOrDefault();
        //                    decimal rnghrs = 0;
        //                    SqlDataAdapter pp = new SqlDataAdapter("INSERT INTO WinchRotation (RopeId, AssignedDate, WinchId,Lead,RunningHours,IsActive)VALUES(" + ropeid + ", '" + assigneddate + "', " + winchid + ",'" + leadcheck + "','" + rnghrs + "','" + true + "' ); ", sc.con);
        //                    DataTable dd = new DataTable();
        //                    pp.Fill(dd);




        //                    SqlDataAdapter adpt = new SqlDataAdapter("update MooringRopeDetail set CurrentLeadRunningHours = 0 where ID=" + ropeid + "", sc.con);
        //                    DataTable ddt = new DataTable();
        //                    adpt.Fill(ddt);

        //                }
        //                catch { }
        //            }
        //        }


        //    }
        //    catch (Exception ex) { }
        //}

        private void ShifttoInactive(AssignModuleToWinchClass mw)
        {
            try
            {

                var result1 = sc.AssignRopetoWinch.SingleOrDefault(b => b.RopeId == mw.RopeId && b.RopeTail == 0 && b.IsActive == true);
                if (result1 != null)
                {

                    result1.IsActive = false;
                    result1.ModifiedBy = "Admin";

                    result1.ModifiedDate = DateTime.Now;
                    sc.SaveChanges();

                    try
                    {
                        int ropeid = result1.RopeId;
                        int winchid = result1.WinchId;
                        DateTime? assigneddate = result1.AssignedDate;

                        lead_check(ropeid, assigneddate, winchid);
                    }
                    catch { }
                }



                //  lead_check();

                MessageBox.Show("Line Shifted to InActive ", "Assign LinetoWinch", MessageBoxButton.OK, MessageBoxImage.Information);

                //.....Refresh DataGrid........


                var lostdata = new ObservableCollection<AssignModuleToWinchClass>(sc.AssignRopetoWinch.ToList());
                AssignRopeToWinchDetailViewModel cc = new AssignRopeToWinchDetailViewModel(lostdata);








            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void DeleteMooringWinch(AssignModuleToWinchClass mw)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AssignModuleToWinchClass findrank = sc.AssignRopetoWinch.Where(x => x.Id == mw.Id).FirstOrDefault();
                    if (findrank != null)
                    {
                        //SqlDataAdapter adp = new SqlDataAdapter("update AssignRopeToWinch set isdelete='True', IsActive='False' where Id =" + mw.Id + " ", sc.con);
                        SqlDataAdapter adp = new SqlDataAdapter("UpdatetAssignRopeToWinch", sc.con);
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@Id", mw.Id);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        //sc.Entry(findrank).State = EntityState.Deleted;
                        sc.SaveChanges();
                        sc.Entry(mw).State = EntityState.Detached;
                        MessageBox.Show("Record deleted successfully ", "Delete Assign LinetoWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                        //.....Refresh DataGrid........
                        var lostdata = new ObservableCollection<AssignModuleToWinchClass>(sc.AssignRopetoWinch.ToList());
                        AssignRopeToWinchDetailViewModel cc = new AssignRopeToWinchDetailViewModel(lostdata);
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


        private void ExportRopetoWinchMethod(object sender, DoWorkEventArgs eb)
        {
            try
            {
                ExportRopetoWinchMethod1();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private void ExportRopetoWinchMethod1()
        {
            try
            {


                DataSet ds = null;
                ds = new DataSet("General");
                ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

                //string qry = "ViewAssignRopeToWinchExcel";
                //SqlCommand cmd = new SqlCommand(qry, sc.con);
                DataSet dt = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter("ViewAssignRopeToWinchExcel", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                //cmd.Parameters.AddWithValue("@RopeTail", 0);
                //adp.SelectCommand = cmd;
                adp.Fill(dt);

                dt.Tables[0].TableName = "CurrentRecord";
                dt.Tables[1].TableName = "PastRecord";

                ds = dt;

                //cmd.Dispose();
                adp.Dispose();
                dt.Dispose();
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "AssignMooringLineToWinchDetails_Export" + DateTime.Now.ToString("dd-MMM-yyyy");
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
                            MessageBox.Show("It seems that the earlier excel file is open, please close the excel file and download again to replace !", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        MessageBox.Show("Export process has been completed!", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
