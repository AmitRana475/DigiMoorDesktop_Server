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
using System.Data.Entity;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class MooringOPRViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public ICommand HelpCommand { get; private set; }
        public MooringOPRViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }



            // Option = GetTailsOnWinches(11);

            //               var languages = new string[]
            //       {
            //"C", "C#", "C++", "D", "Java",
            //"Rust", "Python", "ES6"
            //       };

            //               Option = new List<TailOption>();
            //               foreach (var language in languages)
            //               {
            //                      Option.Add(new TailOption
            //                      {
            //                             Name = language,
            //                             Selected = true
            //                      });
            //               }





            for (int i = 00; i < 24; i++)
            {
                Hours.Add(i < 10 ? "0" + i.ToString() : i.ToString());
                // Hours.Add(i < 10 ? "0" + i.ToString() : i.ToString());
            }

            for (int i = 00; i < 60; i++)
            {
                Mints.Add(i < 10 ? "0" + i.ToString() : i.ToString());
                // MintsSHORT.Add(i < 10 ? "0" + i.ToString() : i.ToString());
            }

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            LoadWinchlist.Clear();
            BindWinchList();
            GetPortName();

            addmore = new RelayCommand<string>(AddMethod);
            remove = new RelayCommand<string>(RemoveMethod);
            savecommand = new RelayCommand(SaveMooringOperation);
            gobackcommand = new RelayCommand(GoBackMethod);
            SaveEnable = true;


            AutoPortName = string.Empty;
            RaisePropertyChanged("AutoPortName");


            // CheckBindCommand = new RelayCommand<Option>(Checkboxlostbind);

            //CheckMarkCommand = new RelayCommand<WinchCheckClass>(MarkCheckMethod);
            CheckMarkCommand = new RelayCommand<WinchCheckClass>(MarkCheckMethod);
            CheckMarkCommand1 = new RelayCommand<WinchCheckClass>(MarkCheckMethod1);
            CheckMarkCommand2 = new RelayCommand<WinchCheckClass>(MarkCheckMethod2);
            CheckMarkCommand3 = new RelayCommand<WinchCheckClass>(MarkCheckMethod3);
            CheckMarkCommand4 = new RelayCommand<WinchCheckClass>(MarkCheckMethod4);
        }
        class Item
        {
            public string Name { get; set; }
            public bool Selected { get; set; }
        }

        // public List<TailOption> Option { get; set; }

        private List<TailOption> GetTailsOnWinches(int winchid, bool CheckTailAll)
        {
            List<TailOption> Tails = new List<TailOption>();
            try
            {


                string sqry = "select a.RopeId,a.Outboard,b.UniqueID,b.CertificateNumber,c.RopeType from AssignRopeToWinch a join MooringRopeDetail b on a.RopeId=b.Id join MooringRopeType c on c.Id=b.RopeTypeId where a.RopeTail=1 and a.IsActive=1 and a.IsDelete=0 and a.WinchId=@WinchId";
                SqlDataAdapter sda = new SqlDataAdapter(sqry, sc.con);
                sda.SelectCommand.Parameters.AddWithValue("@WinchId", winchid);
                DataTable dtp = new DataTable();
                sda.Fill(dtp);
                if (dtp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtp.Rows.Count; i++)
                    {
                        var tail = new TailOption()
                        {
                            Id = Convert.ToInt32(dtp.Rows[i]["RopeId"]),
                            Name = dtp.Rows[i]["UniqueID"].ToString() + " (" + dtp.Rows[i]["RopeType"].ToString() + ")",
                            Selected = CheckTailAll
                        };
                        Tails.Add(tail);
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Tails;
        }


        public MooringOPRViewModel(MOperationBirthDetail item)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            for (int i = 00; i < 24; i++)
            {
                Hours.Add(i < 10 ? "0" + i.ToString() : i.ToString());
                // Hours.Add(i < 10 ? "0" + i.ToString() : i.ToString());
            }

            for (int i = 00; i < 60; i++)
            {
                Mints.Add(i < 10 ? "0" + i.ToString() : i.ToString());
                // MintsSHORT.Add(i < 10 ? "0" + i.ToString() : i.ToString());
            }
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            LoadWinchlist.Clear();
            EditBindWinchList(item.OPId);

            AutoPortName = string.Empty;
            RaisePropertyChanged("AutoPortName");

            addmore = new RelayCommand<string>(AddMethod);

            CheckMarkCommand = new RelayCommand<WinchCheckClass>(MarkCheckMethod);
            CheckMarkCommand1 = new RelayCommand<WinchCheckClass>(MarkCheckMethod1);
            CheckMarkCommand2 = new RelayCommand<WinchCheckClass>(MarkCheckMethod2);
            CheckMarkCommand3 = new RelayCommand<WinchCheckClass>(MarkCheckMethod3);
            CheckMarkCommand4 = new RelayCommand<WinchCheckClass>(MarkCheckMethod4);
            //remove = new RelayCommand<string>(RemoveMethod);

            savecommand = new RelayCommand(UpdateMooringOPR);
            //savecommand = new RelayCommand<MOperationBirthDetail>(UpdateMooringOPR);
            //cancelCommand = new RelayCommand(CancelMooringWinch);
            SaveEnable = true;
            // MooringWinchRopeClass data = sc.MooringWinchRope.Where(x => x.Id == edeps.Id).FirstOrDefault();

            MOperationBirth = new MOperationBirthDetail()
            {
                OPId = item.OPId,
                PortName = item.PortName,
                FastDatetime = item.FastDatetime,
                CastDatetime = item.CastDatetime,
                BirthName = item.BirthName,
                BirthType = item.BirthType,
                MooringType = item.MooringType,
                DraftArrivalFWD = item.DraftArrivalFWD,
                DraftArrivalAFT = item.DraftArrivalAFT,
                DepthAtBerth = item.DepthAtBerth,
                DraftDepartureAFT = item.DraftDepartureAFT,
                DraftDepartureFWD = item.DraftDepartureFWD,
                BerthSide = item.BerthSide,
                Any_Affect_Passing_Traffic = item.Any_Affect_Passing_Traffic,
                Berth_exposed_SeaSwell = item.Berth_exposed_SeaSwell,
                Any_Rope_Damaged = item.Any_Rope_Damaged,
                AnySquall = item.AnySquall,
                CurrentSpeed = item.CurrentSpeed,
                VesselCondition = item.VesselCondition,
                WindDirection = item.WindDirection,
                WindSpeed = item.WindSpeed,
                RangOfTide = item.RangOfTide,
                ShipAccess = item.ShipAccess,
                Ship_was_continuously_contact_with_fender = item.Ship_was_continuously_contact_with_fender,
                SurgingObserved = item.SurgingObserved,
            };
            BirthTypeS = item.BirthType;
            MooringTypeS = item.MooringType;
            BerthSideS = item.BerthSide;

            string grid = "Grid";
            for (int i = 1; i < 5; i++)
            {
                grid = grid + i;
                SqlDataAdapter adp = new SqlDataAdapter("select * from MOUsedWinchTbl where OperationID=" + MOperationBirth.OPId + " and gridid='" + grid + "'", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (grid == "Grid1")
                    {
                        DateTime fastgrid = Convert.ToDateTime(dt.Rows[0]["OPDateFrom"]);
                        DpSubFastGrid1 = fastgrid.ToShortDateString();
                        DpSubFastGrid1_Hours = fastgrid.Hour.ToString();
                        var mmm = fastgrid.ToString("mm");
                        DpSubFastGrid1_Mint = mmm;

                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid1"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid1_Hours"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid1_Mint"));

                        DateTime castgrid = Convert.ToDateTime(dt.Rows[0]["OPDateTo"]);
                        DpSubCastGrid1 = castgrid.ToShortDateString();
                        DpSubCastGrid1_Hours = castgrid.Hour.ToString();
                        var mmmC = castgrid.ToString("mm");
                        DpSubCastGrid1_Mint = mmmC;

                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid1"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid1_Hours"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid1_Mint"));
                    }

                    if (grid == "Grid2")
                    {
                        DateTime fastgrid = Convert.ToDateTime(dt.Rows[0]["OPDateFrom"]);
                        DpSubFastGrid2 = fastgrid.ToShortDateString();
                        DpSubFastGrid2_Hours = fastgrid.Hour.ToString();
                        var mmm = fastgrid.ToString("mm");
                        DpSubFastGrid2_Mint = mmm;

                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid2"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid2_Hours"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid2_Mint"));

                        DateTime castgrid = Convert.ToDateTime(dt.Rows[0]["OPDateTo"]);
                        DpSubCastGrid2 = castgrid.ToShortDateString();
                        DpSubCastGrid2_Hours = castgrid.Hour.ToString();
                        var mmmC = castgrid.ToString("mm");
                        DpSubCastGrid2_Mint = mmmC;

                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid2"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid2_Hours"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid2_Mint"));
                    }
                    if (grid == "Grid3")
                    {
                        DateTime fastgrid = Convert.ToDateTime(dt.Rows[0]["OPDateFrom"]);
                        DpSubFastGrid3 = fastgrid.ToShortDateString();
                        DpSubFastGrid3_Hours = fastgrid.Hour.ToString();
                        var mmm = fastgrid.ToString("mm");
                        DpSubFastGrid3_Mint = mmm;

                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid3"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid3_Hours"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid3_Mint"));

                        DateTime castgrid = Convert.ToDateTime(dt.Rows[0]["OPDateTo"]);
                        DpSubCastGrid3 = castgrid.ToShortDateString();
                        DpSubCastGrid3_Hours = castgrid.Hour.ToString();
                        var mmmC = castgrid.ToString("mm");
                        DpSubCastGrid3_Mint = mmmC;

                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid3"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid3_Hours"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid3_Mint"));
                    }

                    if (grid == "Grid4")
                    {
                        DateTime fastgrid = Convert.ToDateTime(dt.Rows[0]["OPDateFrom"]);
                        DpSubFastGrid4 = fastgrid.ToShortDateString();
                        DpSubFastGrid4_Hours = fastgrid.Hour.ToString();
                        var mmm = fastgrid.ToString("mm");
                        DpSubFastGrid4_Mint = mmm;

                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid4"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid4_Hours"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid4_Mint"));

                        DateTime castgrid = Convert.ToDateTime(dt.Rows[0]["OPDateTo"]);
                        DpSubCastGrid4 = castgrid.ToShortDateString();
                        DpSubCastGrid4_Hours = castgrid.Hour.ToString();
                        var mmmC = castgrid.ToString("mm");
                        DpSubCastGrid4_Mint = mmmC;

                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid4"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid4_Hours"));
                        OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid4_Mint"));
                    }
                }

                grid = "Grid";
            }


            //DpSubFastGrid1 = MOperationBirth.FastDatetime.ToShortDateString();
            //OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid1"));

            //DpSubFastGrid1_Hours= MOperationBirth.FastDatetime.ToShortTimeString();
            //OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid1_Hours"));
            //DpSubFastGrid1_Mint = MOperationBirth.FastDatetime.ToShortTimeString();
            //OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid1_Mint"));

            //var item2 = sc.MOUsedWinchTbl.ToList().FirstOrDefault();

            //LoadWinchlist = new WinchCheckClass()
            //{
            //    GridID = item2.GridID,
            //    StartDatetime = item2.OPDateFrom,
            //    EndDatetime = item2.OPDateTo,
            //    WinId = item2.WinchId,
            //};

            EditMethod(item.OPId);
            RaisePropertyChanged("MOperationBirth");
            OnPropertyChanged(new PropertyChangedEventArgs("MOperationBirth"));
            //EditMooringWinch(edeps);
        }


        public void resetform()
        {
            try
            {
                RaisePropertyChanged("MOperationBirth");
                OnPropertyChanged(new PropertyChangedEventArgs("MOperationBirth"));


                FastDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("FastDate");
                CastDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); RaisePropertyChanged("FastDate");

                // Hours = null; 
                SHours = "00";
                RaisePropertyChanged("SHours");
                SHours2 = "00";
                RaisePropertyChanged("SHours2");
                SMints = "00";
                RaisePropertyChanged("SMints");
                SMints2 = "00";
                RaisePropertyChanged("SMints2");
                MarkAll = true;
                // RaisePropertyChanged("SHours");
                //Mints = null;
                // RaisePropertyChanged("SMints");
                //HoursSTART = null; RaisePropertyChanged("HoursSTART");
                //HoursEND = null; RaisePropertyChanged("HoursEND");
                //MintsSTART = null; RaisePropertyChanged("MintsSTART");
                //MintsEND = null; RaisePropertyChanged("MintsEND");
                //RaisePropertyChanged("HoursEND");
                //RaisePropertyChanged("Mints");
                //RaisePropertyChanged("MintsEND");



                // SRopeType = null; RaisePropertyChanged("SRopeType");

                AutoPortName = string.Empty;
                RaisePropertyChanged("AutoPortName");
            }
            catch { }
        }

        private bool saveenabled = true;

        public bool SaveEnable
        {
            get { return saveenabled; }
            set
            {
                saveenabled = value;
                RaisePropertyChanged("SaveEnable");
            }
        }

        // public static bool IsBackToList { get; set; } = false;

        private static ObservableCollection<PortNameCombo> portname = new ObservableCollection<PortNameCombo>();
        public ObservableCollection<PortNameCombo> PortName
        {
            get
            {
                return portname;
            }
            set
            {
                // var data = sc.AssignRopetoWinch.Where(x => x.Id == ropetype).FirstOrDefault();
                portname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PortName"));
            }
        }

        private static ObservableCollection<PortNameCombo> birthname = new ObservableCollection<PortNameCombo>();
        public ObservableCollection<PortNameCombo> BirthName
        {
            get
            {
                return birthname;
            }
            set
            {
                // var data = sc.AssignRopetoWinch.Where(x => x.Id == ropetype).FirstOrDefault();
                birthname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BirthName"));
            }
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

        public void GetFacilityName(string portname)
        {
            try
            {
                facilityname.Clear();
                PortNameCombo rop;
                SqlDataAdapter adp = new SqlDataAdapter("select facilityname from PortList where PortName='" + portname + "'", sc.con);
                DataTable ds = new DataTable();
                adp.Fill(ds);

                int check = 0;

                foreach (DataRow row in ds.Rows)
                {
                    check++;
                    rop = new PortNameCombo();

                    if (check == 1)
                    {
                        rop.FacilityName = "Other";
                        facilityname.Add(rop);
                        break;
                    }
                }



                foreach (DataRow row in ds.Rows)
                {

                    rop = new PortNameCombo();
                    //rop.Id = (int)row["Id"];

                    //if (check == 1)
                    //{
                    //    rop.FacilityName = "Other";
                    //    facilityname.Add(rop);
                    //    continue;

                    //}


                    rop.FacilityName = (string)row["facilityname"];




                    //facilityname.Add("Other");

                    facilityname.Add(rop);
                }

                OnPropertyChanged(new PropertyChangedEventArgs("facilityname"));
                //return AddRopeType;

            }
            catch { }
        }
        private static ObservableCollection<PortNameCombo> facilityname = new ObservableCollection<PortNameCombo>();
        public ObservableCollection<PortNameCombo> FacilityName
        {
            get
            {
                return facilityname;
            }
            set
            {
                // var data = sc.AssignRopetoWinch.Where(x => x.Id == ropetype).FirstOrDefault();
                facilityname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FacilityName"));
            }
        }
        public class PortNameCombo
        {
            public int Id { get; set; }
            public string PortName { get; set; }
            public string FacilityName { get; set; }
        }

        public static PortNameCombo sportname;// = new Ropetypecombo();
        public PortNameCombo SPortName
        {
            get
            {

                if (sportname != null)
                {

                    MOperationBirth.PortName = sportname.PortName;
                    OnPropertyChanged(new PropertyChangedEventArgs("MOperationBirth"));
                }
                return sportname;

            }
            set
            {

                sportname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SPortName"));


            }
        }


        public static PortNameCombo sfacilityname;// = new Ropetypecombo();
        public PortNameCombo SFacilityName
        {
            get
            {

                if (sfacilityname != null)
                {

                    MOperationBirth.FacilityName = sfacilityname.FacilityName;
                    OnPropertyChanged(new PropertyChangedEventArgs("MOperationBirth"));
                }
                return sfacilityname;

            }
            set
            {

                sfacilityname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SFacilityName"));


            }
        }
        private static string autoPartname;
        public string AutoPortName
        {
            get
            {
                if (autoPartname != null)
                {
                    MOperationBirth.PortName = autoPartname;
                    AutoPortNames = GetPortName(autoPartname);
                    GetFacilityName(autoPartname);
                }
                return autoPartname;
            }

            set
            {
                autoPartname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AutoPortName"));
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
                MOperationBirth.PortName = dat.PortName;
                MOperationBirth.OPId = dat.Id;
            }
            else
            {
                MOperationBirth.PortName = string.Empty;
                MOperationBirth.OPId = 0;

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

            RaisePropertyChanged("MOperationBirth");
            return PortNames;
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
        private void GoBackMethod()
        {
            MainViewModelWorkHours.MooringOperationListingGoBK = false;
        }



        private void winchrotaion_Notification(int ropeid, string OPLead)
        {
            try
            {
                using (SqlDataAdapter adp = new SqlDataAdapter("select ropetypeid,ManufacturerId from MooringRopeDetail where RopeTail=0 and DeleteStatus=0 and  id=" + ropeid + "", sc.con))
                {
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        int ropetypeid = Convert.ToInt32(dt.Rows[0]["ropetypeid"]);
                        int manuFid = Convert.ToInt32(dt.Rows[0]["ManufacturerId"]);

                        string lead = ""; int winchid = 0;

                        SqlDataAdapter pp = new SqlDataAdapter("select b.lead,a.WinchId from assignropetowinch a inner join MooringWinchDetail b on a.WinchId=b.id where a.ropeid=" + ropeid + " and a.IsActive=1", sc.con);
                        DataTable dd = new DataTable();
                        pp.Fill(dd);
                        if (dd.Rows.Count > 0)
                        {
                            winchid = Convert.ToInt32(dd.Rows[0]["WinchId"]);
                            lead = dd.Rows[0]["Lead"].ToString();
                            lead = lead.Replace(System.Environment.NewLine, "").Trim();
                        }



                        SqlDataAdapter pp1 = new SqlDataAdapter("select * from tblWinchRotationSetting where mooringropetype = " + ropetypeid + " and ManufacturerType= " + manuFid + " and LeadFrom='" + lead + "'", sc.con);
                        DataTable dd1 = new DataTable();
                        pp1.Fill(dd1);
                        if (dd1.Rows.Count > 0)
                        {
                            int maxrunhrs = Convert.ToInt32(dd1.Rows[0]["MaximumRunningHours"]);
                            int maxmnthallowed = Convert.ToInt32(dd1.Rows[0]["MaximumMonthsAllowed"]);

                            string leadto = dd1.Rows[0]["LeadTo"].ToString();



                            SqlDataAdapter pp11 = new SqlDataAdapter("select SUM(runninghours) as RunningHours from mousedwinchtbl where RopeId=" + ropeid + " and Lead='" + lead + "'", sc.con);
                            //SqlDataAdapter pp11 = new SqlDataAdapter("select SUM(runninghours) as RunningHours from mousedwinchtbl where RopeId=" + ropeid + " and Lead='" + OPLead + "'", sc.con);
                            DataTable dd11 = new DataTable();
                            pp11.Fill(dd11);
                            if (dd11.Rows.Count > 0)
                            {
                                decimal ttlrnghrs = Convert.ToDecimal(dd11.Rows[0]["RunningHours"] == DBNull.Value ? 0 : dd11.Rows[0]["RunningHours"]);

                                //if (ttlrnghrs != 0)
                                //{
                                SqlDataAdapter pp112 = new SqlDataAdapter("select SUM(runninghours) as RunningHours from WinchRotation where RopeId=" + ropeid + " and Lead='" + lead + "'", sc.con);
                                DataTable dd112 = new DataTable();
                                pp112.Fill(dd112);
                                decimal ttlrnghrs1 = Convert.ToDecimal(dd112.Rows[0]["RunningHours"] == DBNull.Value ? 0 : dd112.Rows[0]["RunningHours"]);

                                if (ttlrnghrs > ttlrnghrs1)
                                {
                                    ttlrnghrs = ttlrnghrs - ttlrnghrs1;


                                    try
                                    {
                                        //SqlDataAdapter adpt = new SqlDataAdapter("update MooringRopeDetail set CurrentLeadRunningHours=" + ttlrnghrs + " where ID=" + ropeid + "", sc.con);
                                        //DataTable ddt = new DataTable();
                                        //adpt.Fill(ddt);

                                        SqlDataAdapter adpt = new SqlDataAdapter("UpdatetCurrentLead", sc.con);
                                        adpt.SelectCommand.CommandType = CommandType.StoredProcedure;
                                        adpt.SelectCommand.Parameters.AddWithValue("@CrntLeadrngHrs", ttlrnghrs);
                                        adpt.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                                        DataTable ddt = new DataTable();
                                        adpt.Fill(ddt);
                                    }
                                    catch { }

                                    /*

                                    maxrunhrs1 = maxrunhrs;

                                    maxrunhrs = maxrunhrs * 90 / 100;


                                    if (ttlrnghrs > maxrunhrs1)
                                    {

                                        var cerno = sc.MooringWinchRope.Where(x => x.Id == ropeid).Select(x => x.CertificateNumber + " - " + x.UniqueID).SingleOrDefault();
                                        var winch = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.AssignedNumber).SingleOrDefault();


                                        var winchrotation = "Winch Rotation Exceeded for Line " + cerno + " currently on Winch " + winch + " lead  " + lead + " , Current " + ttlrnghrs + " hrs / Rotation was due at " + maxrunhrs1 + " hrs,  Please shift from " + lead + " to " + leadto + "";

                                        int NotiAlertType = (int)NotificationAlertType.Winch_Rotation_exceed;
                                        InspectNotification(ropeid, winchrotation, NotiAlertType);
                                    }
                                    else
                                    {

                                        if (maxrunhrs <= ttlrnghrs)
                                        {
                                            var cerno = sc.MooringWinchRope.Where(x => x.Id == ropeid).Select(x => x.CertificateNumber + " - " + x.UniqueID).SingleOrDefault();
                                            var winch = sc.MooringWinch.Where(x => x.Id == winchid && x.IsActive == true).Select(x => x.AssignedNumber).SingleOrDefault();


                                            var winchrotation = "Winch Rotation Approaching for Line " + cerno + " currently on Winch " + winch + " lead  " + lead + " , Current " + ttlrnghrs + " hrs / Rotation was due at " + maxrunhrs1 + " hrs,  Please shift from " + lead + " to " + leadto + "";


                                            int NotiAlertType = (int)NotificationAlertType.Winch_Rotation_approching;
                                            InspectNotification(ropeid, winchrotation, NotiAlertType);
                                        }
                                    }

                                    */
                                }
                                //}
                            }



                        }

                    }



                }
            }
            catch { }
        }


        public void InspectNotification(int RopeID, string NotiMsg, int NotiAlertType)
        {
            var result = sc.Notifications.Where(x => x.RopeId == RopeID & x.NotificationAlertType == NotiAlertType).FirstOrDefault();
            if (result == null)
            {
                NotificationsClass noti = new NotificationsClass();
                noti.Acknowledge = false;
                noti.AckRecord = "Not yet acknowledged";
                noti.Notification = NotiMsg;
                noti.RopeId = RopeID;
                noti.IsActive = true;
                noti.NotificationDueDate = DateTime.Now.Date;
                noti.CreatedDate = DateTime.Now;
                noti.CreatedBy = "Admin";
                noti.NotificationAlertType = NotiAlertType;
                noti.NotificationType = 1;
                sc.Notifications.Add(noti);
                sc.SaveChanges();
            }
        }

        private void SaveMooringOperation()
        {
            var DtimeFast = FastDate.ToShortDateString() + " " + SHours + ":" + SMints;
            MOperationBirth.FastDatetime = Convert.ToDateTime(DtimeFast);

            var DtimeCast = CastDate.ToShortDateString() + " " + SHours2 + ":" + SMints2;
            MOperationBirth.CastDatetime = Convert.ToDateTime(DtimeCast);

            MainViewModelWorkHours.MooringOperationListingGoBK = false;
            /*
            string qryhh = @"SELECT * from MOperationBirthDetail where 
(FastDatetime BETWEEN @Fdate AND @Cdate) OR 
(CastDatetime BETWEEN @Fdate AND @Cdate) OR 
(FastDatetime <= @Fdate AND CastDatetime >= @Cdate)";
            using (SqlDataAdapter hhh = new SqlDataAdapter(qryhh, sc.con))
            {
                   hhh.SelectCommand.Parameters.AddWithValue("@Fdate", MOperationBirth.FastDatetime);
                   hhh.SelectCommand.Parameters.AddWithValue("@Cdate", MOperationBirth.CastDatetime);
                   DataTable dtph = new DataTable();
                   hhh.Fill(dtph);
                   if (dtph.Rows.Count > 0)
                   {
                          MainViewModelWorkHours.MooringOperationListingGoBK = true;
                          MessageBox.Show("A Mooring Operataion is already exist between selected Fast-Date and Cast-Date.", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                          return;
                   }
            }
            */


            var asd = MOperationBirth;

            if (string.IsNullOrEmpty(MOperationBirth.PortName))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter Port Name", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(MOperationBirth.FacilityName))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter Facility Name", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(MOperationBirth.BirthName))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter Berth Name", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.BirthType))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Berth Type", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.MooringType))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Mooring Type", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MOperationBirth.DraftArrivalFWD == null)
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter Draft: Arrival FWD", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MOperationBirth.DraftArrivalAFT == null)
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter Draft: Arrival AFT", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MOperationBirth.DraftDepartureFWD == null)
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter Draft: Departure FWD", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MOperationBirth.DraftDepartureAFT == null)
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter Draft: Departure AFT", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MOperationBirth.DepthAtBerth == null)
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter Depth at Berth", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.BerthSide))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Berth Side", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.VesselCondition))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Vessel Condition", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.ShipAccess))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Ship Access", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MOperationBirth.RangOfTide == null)
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter Depth at Berth", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.WindDirection))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Wind Direction", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MOperationBirth.WindSpeed == 0)
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter Avg Wind Speed", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MOperationBirth.AirTemperature == null)
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter AirTemperature", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.AnySquall))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Any Squall / Gusts experienced", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MOperationBirth.CurrentSpeed == null)
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Enter Current Speed", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.Berth_exposed_SeaSwell))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Berth exposed to Sea Swell", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.SurgingObserved))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Surging Observed", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.Any_Affect_Passing_Traffic))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Any affect of passing traffic?", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.Ship_was_continuously_contact_with_fender))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Ship was continuously in contact with fender", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(MOperationBirth.Any_Rope_Damaged))
            {
                MainViewModelWorkHours.MooringOperationListingGoBK = true;
                MessageBox.Show("Please Select Any Line Damaged", "Mooring Operataion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int GridPositon = 0;

            #region validation check

            foreach (var item in LoadWinchlist.Where(x => x.Mark == true).ToList())
            {
                if (item.outboard1 == "--Select--")
                {
                    GridPositon = 1;
                    MainViewModelWorkHours.MooringOperationListingGoBK = true;
                    MessageBox.Show("Please choose outboardend before save this operation", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (item.Lead == "--Select--")
                {
                    GridPositon = 1;
                    MainViewModelWorkHours.MooringOperationListingGoBK = true;
                    MessageBox.Show("Please choose Lead before save this operation", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            foreach (var item in LoadWinchListGrid1.Where(x => x.Mark == true).ToList())
            {
                if (item.outboard1 == "--Select--")
                {
                    GridPositon = 1;
                    MainViewModelWorkHours.MooringOperationListingGoBK = true;
                    MessageBox.Show("Please choose outboardend before save this operation", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (item.Lead == "--Select--")
                {
                    GridPositon = 1;
                    MainViewModelWorkHours.MooringOperationListingGoBK = true;
                    MessageBox.Show("Please choose Lead before save this operation", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            foreach (var item in LoadWinchListGrid2.Where(x => x.Mark == true).ToList())
            {
                if (item.outboard1 == "--Select--")
                {
                    GridPositon = 1;
                    MainViewModelWorkHours.MooringOperationListingGoBK = true;
                    MessageBox.Show("Please choose outboardend before save this operation", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (item.Lead == "--Select--")
                {
                    GridPositon = 1;
                    MainViewModelWorkHours.MooringOperationListingGoBK = true;
                    MessageBox.Show("Please choose Lead before save this operation", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            foreach (var item in LoadWinchListGrid3.Where(x => x.Mark == true).ToList())
            {
                if (item.outboard1 == "--Select--")
                {
                    GridPositon = 1;
                    MainViewModelWorkHours.MooringOperationListingGoBK = true;
                    MessageBox.Show("Please choose outboardend before save this operation", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (item.Lead == "--Select--")
                {
                    GridPositon = 1;
                    MainViewModelWorkHours.MooringOperationListingGoBK = true;
                    MessageBox.Show("Please choose Lead before save this operation", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            foreach (var item in LoadWinchListGrid4.Where(x => x.Mark == true).ToList())
            {
                if (item.outboard1 == "--Select--")
                {
                    GridPositon = 1;
                    MainViewModelWorkHours.MooringOperationListingGoBK = true;
                    MessageBox.Show("Please choose outboardend before save this operation", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (item.Lead == "--Select--")
                {
                    GridPositon = 1;
                    MainViewModelWorkHours.MooringOperationListingGoBK = true;
                    MessageBox.Show("Please choose Lead before save this operation", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            #endregion


            if (GridPositon == 0)
            {
                RowGrid1 = RowGrid2 = RowGrid3 = RowGrid4 = 0;
                RaisePropertyChanged("RowGrid1"); RaisePropertyChanged("RowGrid2");
                RaisePropertyChanged("RowGrid3"); RaisePropertyChanged("RowGrid4");
                BerthGrid = 1000;
                RaisePropertyChanged("BerthGrid");
            }


            sc.MOperationBirthDetailTbl.Add(MOperationBirth);
            sc.SaveChanges();

            int OperationID = 0;
            OperationID = sc.MOperationBirthDetailTbl.Max(x => x.OPId);


            try
            {
                // Main Grid all Winches
                //  LoadWinchlist.Where(x => x.Mark == false).ToList();
                var ropelist = sc.MooringWinchRope.Where(x => x.IsActive == true && x.RopeTail == 0).ToList();
                var assignRope = sc.AssignRopetoWinch.Where(x => x.IsActive == true && x.RopeTail == 0).ToList();
                var rpinspsetting = sc.RopeInspectionSetting.ToList();
                var winchlist = sc.MooringWinch.Where(x => x.IsActive == true).ToList();

                int ropetypeid = 0;


                // Grid 0 Selected Winches >
                foreach (var item in LoadWinchlist.Where(x => x.Mark == true).ToList())
                {
                    item.StartDatetime = MOperationBirth.FastDatetime;
                    item.EndDatetime = MOperationBirth.CastDatetime;

                    DateTime dtto = Convert.ToDateTime(MOperationBirth.FastDatetime);
                    DateTime dtfrom = Convert.ToDateTime(MOperationBirth.CastDatetime);
                    var diff = dtfrom.Subtract(dtto);
                    //int hours = Convert.ToInt32(diff.TotalHours);
                    decimal hours = Convert.ToDecimal(diff.TotalHours);

                    hours = Math.Round(hours, 2);

                    var ropeid = item.RopeId;

                    ropetypeid = Convert.ToInt32(ropelist.Where(x => x.Id == ropeid).Select(x => x.RopeTypeId).SingleOrDefault());
                    // do save code


                    //var maxNotiid = sc.Notifications.Select(x => x.Id).Max();
                    //int notiid = maxNotiid + 1;
                    int notiid = sc.NextNotiId();


                    MOUsedWinchTbl used = new MOUsedWinchTbl();
                    used.OperationID = OperationID;
                    used.GridID = item.GridID;
                    used.OPDateFrom = item.StartDatetime;
                    used.OPDateTo = item.EndDatetime;
                    used.WinchId = item.WinId;
                    used.RopeId = ropeid;
                    used.RopeTail = 0;
                    used.RunningHours = hours > 0 ? hours : 0;
                    used.NotificationId = notiid;
                    used.Lead = item.Lead;
                    used.Lead1 = item.Lead1;

                    var mytails = item.Tails;

                    if (item.outboard1 == "A")
                    {
                        used.Outboard = true;
                    }
                    if (item.outboard1 == "B")
                    {
                        used.Outboard = false;
                    }

                   
                        used.Lead = used.Lead.Replace(System.Environment.NewLine, "").Trim();
                        sc.MOUsedWinchTbl.Add(used);
                        sc.SaveChanges();
                    




                    winchrotaion_Notification(ropeid, used.Lead);

                    if (hours > 0)
                    {

                        try
                        {
                            var rninghrs = ropelist.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                            // hours = Convert.ToInt32(rninghrs) + hours;
                            hours = Convert.ToDecimal(rninghrs) + hours;
                        }
                        catch { }

                        var result = ropelist.SingleOrDefault(b => b.Id == ropeid);
                        if (result != null)
                        {
                            result.CurrentRunningHours = hours;
                            result.ModifiedBy = "Admin";
                            result.ModifiedDate = DateTime.Now;
                            sc.SaveChanges();
                        }


                        NotificationRopeDiscard(hours, OperationID, ropeid);
                    }

                }
                //------------------------
                // Grid 1 Selected Winches >
                string Grid1FDatetime = DpSubFastGrid1 + " " + DpSubFastGrid1_Hours + ":" + DpSubFastGrid1_Mint;
                if (Grid1FDatetime.Trim() == ":" || Grid1FDatetime == null)
                {
                    Grid1FDatetime = DtimeFast;
                }
                DateTime GS1 = Convert.ToDateTime(Grid1FDatetime);

                var Grid1CDatetime = DpSubCastGrid1 + " " + DpSubCastGrid1_Hours + ":" + DpSubCastGrid1_Mint;
                if (Grid1CDatetime.Trim() == ":" || Grid1CDatetime == null)
                {
                    Grid1CDatetime = DtimeCast;
                }
                DateTime GE1 = Convert.ToDateTime(Grid1CDatetime);
                foreach (var item in LoadWinchListGrid1.Where(x => x.Mark == true).ToList())
                {
                    item.StartDatetime = GS1;
                    item.EndDatetime = GE1;
                    // do save code

                    DateTime dtto = Convert.ToDateTime(GS1);
                    DateTime dtfrom = Convert.ToDateTime(GE1);
                    var diff = dtfrom.Subtract(dtto);

                    decimal hours = Convert.ToDecimal(diff.TotalHours);

                    var ropeid = item.RopeId;

                    int notiid = sc.NextNotiId();

                    MOUsedWinchTbl used = new MOUsedWinchTbl();
                    used.OperationID = OperationID;
                    used.GridID = item.GridID;
                    used.OPDateFrom = item.StartDatetime;
                    used.OPDateTo = item.EndDatetime;
                    used.WinchId = item.WinId;
                    used.RopeId = ropeid;
                    used.RopeTail = 0;
                    //used.RunningHours = hours;
                    used.RunningHours = hours > 0 ? hours : 0;
                    used.NotificationId = notiid;
                    used.Lead = item.Lead;
                    used.Lead1 = item.Lead1;
                    if (item.outboard1 == "A")
                    {
                        used.Outboard = true;
                    }
                    if (item.outboard1 == "B")
                    {
                        used.Outboard = false;
                    }
                    //used.Outboard = sc.AssignRopetoWinch.Where(x => x.WinchId == item.WinId && x.IsActive == true).Select(x => x.Outboard).SingleOrDefault();
                    used.Lead = used.Lead.Replace(System.Environment.NewLine, "").Trim();
                    sc.MOUsedWinchTbl.Add(used);
                    sc.SaveChanges();

                    winchrotaion_Notification(ropeid, used.Lead);

                    if (hours > 0)
                    {

                        try
                        {
                            var rninghrs = ropelist.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                            // hours = Convert.ToInt32(rninghrs) + hours;
                            hours = Convert.ToDecimal(rninghrs) + hours;
                        }
                        catch { }
                        var result = ropelist.SingleOrDefault(b => b.Id == ropeid);
                        if (result != null)
                        {
                            result.CurrentRunningHours = hours;
                            result.ModifiedBy = "Admin";
                            result.ModifiedDate = DateTime.Now;
                            sc.SaveChanges();
                        }




                        NotificationRopeDiscard(hours, OperationID, ropeid);

                    }
                }


                // Grid 2 Selected Winches >
                var Grid2FDatetime = DpSubFastGrid2 + " " + DpSubFastGrid2_Hours + ":" + DpSubFastGrid2_Mint;
                if (Grid2FDatetime.Trim() == ":" || Grid2FDatetime == null)
                {
                    Grid2FDatetime = DtimeFast;
                }
                DateTime GS2 = Convert.ToDateTime(Grid2FDatetime);

                var Grid2CDatetime = DpSubCastGrid2 + " " + DpSubCastGrid2_Hours + ":" + DpSubCastGrid2_Mint;
                if (Grid2CDatetime.Trim() == ":" || Grid2CDatetime == null)
                {
                    Grid2CDatetime = DtimeCast;
                }
                DateTime GE2 = Convert.ToDateTime(Grid2CDatetime);
                foreach (var item in LoadWinchListGrid2.Where(x => x.Mark == true).ToList())
                {
                    item.StartDatetime = GS2;
                    item.EndDatetime = GE2;
                    // do save code

                    DateTime dtto = Convert.ToDateTime(GS2);
                    DateTime dtfrom = Convert.ToDateTime(GE2);
                    var diff = dtfrom.Subtract(dtto);

                    decimal hours = Convert.ToDecimal(diff.TotalHours);
                    hours = Math.Round(hours, 2);
                    var ropeid = item.RopeId;

                    int notiid = sc.NextNotiId();

                    MOUsedWinchTbl used = new MOUsedWinchTbl();
                    used.OperationID = OperationID;
                    used.GridID = item.GridID;
                    used.OPDateFrom = item.StartDatetime;
                    used.OPDateTo = item.EndDatetime;
                    used.WinchId = item.WinId;
                    used.RopeId = ropeid;
                    used.RopeTail = 0;
                    used.RunningHours = hours > 0 ? hours : 0; ;
                    used.NotificationId = notiid;
                    used.Lead = item.Lead;
                    used.Lead1 = item.Lead1;

                    if (item.outboard1 == "A")
                    {
                        used.Outboard = true;
                    }
                    if (item.outboard1 == "B")
                    {
                        used.Outboard = false;
                    }
                    used.Lead = used.Lead.Replace(System.Environment.NewLine, "").Trim();
                    sc.MOUsedWinchTbl.Add(used);
                    sc.SaveChanges();


                    winchrotaion_Notification(ropeid, used.Lead);

                    if (hours > 0)
                    {

                        try
                        {
                            var rninghrs = ropelist.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                            hours = Convert.ToDecimal(rninghrs) + hours;
                        }
                        catch { }
                        var result = ropelist.SingleOrDefault(b => b.Id == ropeid);
                        if (result != null)
                        {
                            result.CurrentRunningHours = hours;
                            result.ModifiedBy = "Admin";
                            result.ModifiedDate = DateTime.Now;
                            sc.SaveChanges();
                        }


                        NotificationRopeDiscard(hours, OperationID, ropeid);
                    }
                }

                // Grid 3 Selected Winches >
                var Grid3FDatetime = DpSubFastGrid3 + " " + DpSubFastGrid3_Hours + ":" + DpSubFastGrid3_Mint;
                if (Grid3FDatetime.Trim() == ":" || Grid3FDatetime == null)
                {
                    Grid3FDatetime = DtimeFast;
                }
                DateTime GS3 = Convert.ToDateTime(Grid3FDatetime);

                var Grid3CDatetime = DpSubCastGrid3 + " " + DpSubCastGrid3_Hours + ":" + DpSubCastGrid3_Mint;
                if (Grid3CDatetime.Trim() == ":" || Grid3CDatetime == null)
                {
                    Grid3CDatetime = DtimeCast;
                }
                DateTime GE3 = Convert.ToDateTime(Grid3CDatetime);
                foreach (var item in LoadWinchListGrid3.Where(x => x.Mark == true).ToList())
                {
                    item.StartDatetime = GS3;
                    item.EndDatetime = GE3;
                    // do save code

                    DateTime dtto = Convert.ToDateTime(GS3);
                    DateTime dtfrom = Convert.ToDateTime(GE3);
                    var diff = dtfrom.Subtract(dtto);

                    decimal hours = Convert.ToDecimal(diff.TotalHours);
                    hours = Math.Round(hours, 2);

                    var ropeid = item.RopeId;


                    int notiid = sc.NextNotiId();

                    MOUsedWinchTbl used = new MOUsedWinchTbl();
                    used.OperationID = OperationID;
                    used.GridID = item.GridID;
                    used.OPDateFrom = item.StartDatetime;
                    used.OPDateTo = item.EndDatetime;
                    used.WinchId = item.WinId;
                    used.RopeId = ropeid;
                    used.RopeTail = 0;
                    used.RunningHours = hours > 0 ? hours : 0;
                    used.NotificationId = notiid;
                    used.Lead = item.Lead;
                    used.Lead1 = item.Lead1;
                    if (item.outboard1 == "A")
                    {
                        used.Outboard = true;
                    }
                    if (item.outboard1 == "B")
                    {
                        used.Outboard = false;
                    }
                    used.Lead = used.Lead.Replace(System.Environment.NewLine, "").Trim();
                    sc.MOUsedWinchTbl.Add(used);
                    sc.SaveChanges();

                    winchrotaion_Notification(ropeid, used.Lead);

                    if (hours > 0)
                    {

                        try
                        {
                            var rninghrs = ropelist.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                            //  hours = Convert.ToInt32(rninghrs) + hours;
                            hours = Convert.ToDecimal(rninghrs) + hours;
                        }
                        catch { }
                        var result = ropelist.SingleOrDefault(b => b.Id == ropeid);
                        if (result != null)
                        {
                            result.CurrentRunningHours = hours;
                            result.ModifiedBy = "Admin";
                            result.ModifiedDate = DateTime.Now;
                            sc.SaveChanges();
                        }



                        NotificationRopeDiscard(hours, OperationID, ropeid);
                    }
                }


                // Grid 4 Selected Winches >
                var Grid4FDatetime = DpSubFastGrid4 + " " + DpSubFastGrid4_Hours + ":" + DpSubFastGrid4_Mint;
                if (Grid4FDatetime.Trim() == ":" || Grid4FDatetime == null)
                {
                    Grid4FDatetime = DtimeFast;
                }
                DateTime GS4 = Convert.ToDateTime(Grid4FDatetime);

                var Grid4CDatetime = DpSubCastGrid4 + " " + DpSubCastGrid4_Hours + ":" + DpSubCastGrid4_Mint;
                if (Grid4CDatetime.Trim() == ":" || Grid4CDatetime == null)
                {
                    Grid4CDatetime = DtimeCast;
                }
                DateTime GE4 = Convert.ToDateTime(Grid4CDatetime);
                foreach (var item in LoadWinchListGrid4.Where(x => x.Mark == true).ToList())
                {
                    item.StartDatetime = GS4;
                    item.EndDatetime = GE4;
                    // do save code

                    DateTime dtto = Convert.ToDateTime(GS4);
                    DateTime dtfrom = Convert.ToDateTime(GE4);
                    var diff = dtfrom.Subtract(dtto);
                    //int hours = Convert.ToInt32(diff.TotalHours);
                    decimal hours = Convert.ToDecimal(diff.TotalHours);
                    hours = Math.Round(hours, 2);

                    var ropeid = item.RopeId;

                    int notiid = sc.NextNotiId();

                    MOUsedWinchTbl used = new MOUsedWinchTbl();
                    used.OperationID = OperationID;
                    used.GridID = item.GridID;
                    used.OPDateFrom = item.StartDatetime;
                    used.OPDateTo = item.EndDatetime;
                    used.WinchId = item.WinId;
                    used.RopeId = ropeid;
                    used.RopeTail = 0;
                    used.RunningHours = hours > 0 ? hours : 0;
                    used.NotificationId = notiid;
                    used.Lead = item.Lead;
                    used.Lead1 = item.Lead1;
                    if (item.outboard1 == "A")
                    {
                        used.Outboard = true;
                    }
                    if (item.outboard1 == "B")
                    {
                        used.Outboard = false;
                    }
                    used.Lead = used.Lead.Replace(System.Environment.NewLine, "").Trim();
                    sc.MOUsedWinchTbl.Add(used);
                    sc.SaveChanges();

                    winchrotaion_Notification(ropeid, used.Lead);

                    if (hours > 0)
                    {

                        try
                        {
                            var rninghrs = ropelist.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                            hours = Convert.ToDecimal(rninghrs) + hours;
                        }
                        catch { }
                        var result = ropelist.SingleOrDefault(b => b.Id == ropeid);
                        if (result != null)
                        {
                            result.CurrentRunningHours = hours;
                            result.ModifiedBy = "Admin";
                            result.ModifiedDate = DateTime.Now;
                            sc.SaveChanges();
                        }


                        NotificationRopeDiscard(hours, OperationID, ropeid);
                    }
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

            SaveRopeTailMooringOperation();

            if (MOperationBirth.Any_Rope_Damaged == "Yes")
            {
                //MainViewModelWorkHours.MooringOperationListingGoBK = false;
                MessageBox.Show("Mooring Operation Record successfully saved, Now Add Damaged Record.", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Information);
                MooringOPDamagedRopeViewModel vm = new MooringOPDamagedRopeViewModel(OperationID);
                MooringOPDamagedRopeViewModel._MOPDamageRope.RopeId = MooringOPDamagedRopeViewModel._MOPDamageRope.RopeId;
                ChildWindowManager.Instance.ShowChildWindow(new MooringOPDamagedRopeView() { DataContext = vm });
            }
            if (MOperationBirth.Any_Rope_Damaged == "No")
            {
                // MainViewModelWorkHours.MooringOperationListingGoBK = false;
                MessageBox.Show("Record successfully saved !", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Information);

            }

            MarkAll = true;
            loadWinchlistGrid1.Clear();
            loadWinchlistGrid2.Clear();
            loadWinchlistGrid3.Clear();
            loadWinchlistGrid4.Clear();
            //Mark1All = true;
            //Mark2All = true;
            //Mark3All = true;
            //Mark4All = true;
            //BindWinchList();
            SFacilityName = null;
        }

        private void WinchrotationSetting_UpdateRunningHours()
        {
            try
            {


                string qry = @"select distinct a.WinchId,b.AssignedNumber,b.Location, b.lead,a.AssignedDate,R.Id as RopeID,R.CurrentLeadRunningHours,R.ManufacturerId,R.RopeTypeId,R.UniqueID,R.CertificateNumber,
	T.RopeType,M.Name from AssignRopeToWinch a inner join MooringWinchDetail b on a.WinchId=b.id
and a.IsActive=1 and a.IsDelete=0 and a.RopeTail=0 join MooringRopeDetail R on R.Id=a.RopeId 
and R.RopeTail=0 and R.DeleteStatus=0  and R.OutofServiceDate is null
join tblCommon M on M.Id=R.ManufacturerId join MooringRopeType T on T.Id=R.RopeTypeId";
                SqlDataAdapter ssda = new SqlDataAdapter(qry, sc.con);
                DataTable tbls = new DataTable();
                ssda.Fill(tbls);
                if (tbls.Rows.Count > 0)
                {
                    //int ropetypeid = Convert.ToInt32(tbls.Rows[0]["RopeTypeId"]);
                    //int manuFid = Convert.ToInt32(tbls.Rows[0]["ManufacturerId"]);
                    //decimal CurrentLeadRunningHours = Convert.ToInt32(tbls.Rows[0]["ManufacturerId"]);
                    //string lead = tbls.Rows[0]["lead"].ToString();
                    //int winchid = Convert.ToInt32(tbls.Rows[0]["WinchId"]);
                    int ApprochingCount = 0; int ExceedCount = 0;
                    for (int i = 0; i < tbls.Rows.Count; i++)
                    {
                        int ManufacturerId = Convert.ToInt32(tbls.Rows[i]["ManufacturerId"]);
                        int RopeTypeId = Convert.ToInt32(tbls.Rows[i]["RopeTypeId"]);
                        int ropeid = Convert.ToInt32(tbls.Rows[i]["RopeID"]);
                        string TestLeadRunningHours = tbls.Rows[i]["CurrentLeadRunningHours"].ToString();
                        decimal CurrentLeadRunningHours = 0;
                        if (!string.IsNullOrEmpty(TestLeadRunningHours))
                        {
                            CurrentLeadRunningHours = Convert.ToDecimal(tbls.Rows[i]["CurrentLeadRunningHours"]);
                        }
                       
                        string Lead = tbls.Rows[i]["lead"].ToString();
                        Lead = Lead.Replace(System.Environment.NewLine, "").Trim();
                        DateTime AssignedDate = Convert.ToDateTime(tbls.Rows[i]["AssignedDate"]);
                        string UniqueID = tbls.Rows[i]["UniqueID"].ToString();
                        string CertificateNum = tbls.Rows[i]["CertificateNumber"].ToString();
                        string WinchAssignedNumber = tbls.Rows[i]["AssignedNumber"].ToString();


                        SqlDataAdapter pp1 = new SqlDataAdapter("select * from tblWinchRotationSetting where mooringropetype = " + RopeTypeId + " and ManufacturerType= " + ManufacturerId + " and LeadFrom='" + Lead + "'", sc.con);
                        DataTable WRSetting = new DataTable();
                        pp1.Fill(WRSetting);
                        if (WRSetting.Rows.Count > 0)
                        {
                            //int maxrunhrs = Convert.ToInt32(dd1.Rows[0]["MaximumRunningHours"]);
                            //int maxmnthallowed = Convert.ToInt32(dd1.Rows[0]["MaximumMonthsAllowed"]);
                            string leadFrom = WRSetting.Rows[0]["LeadFrom"].ToString();
                            string leadto = WRSetting.Rows[0]["LeadTo"].ToString();

                            int maxrunhrs = Convert.ToInt32(WRSetting.Rows[0]["MaximumRunningHours"]);
                            int maxmnthallowed = Convert.ToInt32(WRSetting.Rows[0]["MaximumMonthsAllowed"]);

                            var AssignedDateAppro = AssignedDate.AddMonths(maxmnthallowed - 2);
                            var AssignedDateExceed = AssignedDate.AddMonths(maxmnthallowed);
                            var CurrentDate = DateTime.Now.Date;//.AddMonths(maxmnthallowed);



                            //Approching Count Start
                            #region
                            int RB = 0; int MA = 0;
                            var WinchMonthdiff = StaticHelper.DateDiffInMonths(AssignedDate, DateTime.Now.Date);//
                            if (CurrentDate >= AssignedDateAppro && CurrentDate < AssignedDateExceed)
                            {

                                var winchrotation = "Winch Rotation is Approaching for Line " + CertificateNum + " - " + UniqueID + " currently on Winch " + WinchAssignedNumber + " lead  " + Lead + " , Current " + WinchMonthdiff + " month / Rotation was due at " + maxmnthallowed + " month,  Please shift from " + Lead + " to " + leadto + "";

                                int NotiAlertType = (int)NotificationAlertType.Winch_Rotation_approching;
                                InspectNotification(ropeid, winchrotation, NotiAlertType);
                                MA++;

                            }

                            if (maxrunhrs > 0)
                            {
                                var maxrunhrs1 = maxrunhrs * 90 / 100;
                                if (CurrentLeadRunningHours > maxrunhrs1)
                                {

                                    var winchrotation = "Winch Rotation is Approaching for Line " + CertificateNum + " - " + UniqueID + " currently on Winch " + WinchAssignedNumber + " lead  " + Lead + " , Current " + CurrentLeadRunningHours + " hrs / Rotation was due at " + maxrunhrs + " hrs,  Please shift from " + Lead + " to " + leadto + "";

                                    int NotiAlertType = (int)NotificationAlertType.Winch_Rotation_approching;
                                    InspectNotification(ropeid, winchrotation, NotiAlertType);
                                    RB++;
                                }
                            }

                            if (RB + MA > 0)
                            {
                                ApprochingCount++;
                            }

                            //Approching Count End
                            #endregion
                            //*********************************************************

                            //Exceeded Count Start
                            #region
                            int RB2 = 0; int MA2 = 0;
                            if (CurrentDate >= AssignedDateExceed)
                            {
                                var winchrotation = "Winch Rotation was Exceeded for Line " + CertificateNum + " - " + UniqueID + " currently on Winch " + WinchAssignedNumber + " lead  " + Lead + " , Current " + WinchMonthdiff + " month / Rotation was due at " + maxmnthallowed + " month,  Please shift from " + Lead + " to " + leadto + "";

                                int NotiAlertType = (int)NotificationAlertType.Winch_Rotation_exceed;
                                InspectNotification(ropeid, winchrotation, NotiAlertType);
                                MA2++;
                            }

                            if (maxrunhrs > 0)
                            {
                                //var maxrunhrs1 = maxrunhrs * 90 / 100;
                                if (CurrentLeadRunningHours > maxrunhrs)
                                {
                                    var winchrotation = "Winch Rotation was Exceeded for Line " + CertificateNum + " - " + UniqueID + " currently on Winch " + WinchAssignedNumber + " lead  " + Lead + " , Current " + CurrentLeadRunningHours + " hrs / Rotation was due at " + maxrunhrs + " hrs,  Please shift from " + Lead + " to " + leadto + "";

                                    int NotiAlertType = (int)NotificationAlertType.Winch_Rotation_exceed;
                                    InspectNotification(ropeid, winchrotation, NotiAlertType);
                                    RB2++;
                                }
                            }

                            if (RB2 + MA2 > 0)
                            {
                                ExceedCount++;
                            }

                            //Exceeded Count End
                            #endregion


                        }
                    }
                }

            }
            catch (Exception ex)
            {

                sc.ErrorLog(ex);
            }
        }
        private void NotificationRopeDiscard(decimal hours, int OperationId, int RopeID)
        {
            WinchrotationSetting_UpdateRunningHours();
            /*
            try
            {
                /// max RunningHrs notification
                /// 
                SqlDataAdapter adp1 = new SqlDataAdapter("select * from MooringRopeDetail where RopeTail=0 and DeleteStatus=0 and  Id = " + RopeID + " ", sc.con);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    string assignednumber = ""; string location = ""; string certificatenumber = ""; string uniqueId = "";
                    int Ropeid = Convert.ToInt32(dt1.Rows[0]["Id"]);
                    int RopeTypeId = Convert.ToInt32(dt1.Rows[0]["RopeTypeId"]);
                    int ManufacturerId = Convert.ToInt32(dt1.Rows[0]["ManufacturerId"]);

                    int MaxFixMonth = 0; int maxrnghrs = 0;
                    SqlDataAdapter adpSet = new SqlDataAdapter("select * from tblRopeInspectionSetting where MooringRopeType=" + RopeTypeId + " and ManufacturerType=" + ManufacturerId + "", sc.con);
                    DataTable dtSet = new DataTable();
                    adpSet.Fill(dtSet);
                    if (dtSet.Rows.Count > 0)
                    {
                        MaxFixMonth = Convert.ToInt32(dtSet.Rows[0]["MaximumMonthsAllowed"]);

                        maxrnghrs = Convert.ToInt32(dtSet.Rows[0]["MaximumRunningHours"]);
                    }


                    SqlDataAdapter pp = new SqlDataAdapter("select winchid from AssignRopeToWinch where RopeId=" + Ropeid + "", sc.con);
                    DataTable dtt = new DataTable();
                    pp.Fill(dtt);
                    if (dtt.Rows.Count > 0)
                    {
                        SqlDataAdapter pp1 = new SqlDataAdapter("select * from MooringWinchDetail where ID=" + Convert.ToInt32(dtt.Rows[0][0]) + "", sc.con);
                        DataTable dtt1 = new DataTable();
                        pp1.Fill(dtt1);
                        if (dtt1.Rows.Count > 0)
                        {
                            assignednumber = dtt1.Rows[0]["AssignedNumber"].ToString();
                            location = dtt1.Rows[0]["Location"].ToString();
                        }
                    }


                    certificatenumber = dt1.Rows[0]["CertificateNumber"].ToString();
                    uniqueId = dt1.Rows[0]["UniqueID"].ToString();
                    //int rnghrs = Convert.ToInt32(dt1.Rows[0]["CurrentRunningHours"]);
                    decimal rnghrs = hours;


                    if (maxrnghrs != 0)
                    {
                        int total = maxrnghrs * 80 / 100;
                        if (rnghrs >= total)
                        {
                            if (rnghrs >= maxrnghrs)
                            {
                                if (!string.IsNullOrEmpty(assignednumber))
                                {


                                    var Max_running_hours_exceeded = "Max running hours exceeded (Current - " + rnghrs + "hrs / Max - " + maxrnghrs + " hrs) for Line '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                    int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Exceeded;
                                    InspectNotification3(Ropeid, Max_running_hours_exceeded, NotiAlertType, DateTime.Now.Date, OperationId);
                                }
                                else
                                {
                                    var Max_running_hours_exceeded = "Max running hours exceeded (Current - " + rnghrs + "hrs / Max - " + maxrnghrs + " hrs) for Line '" + certificatenumber + " - " + uniqueId; // + "' located at " + assignednumber + " (" + location + ") ";
                                    int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Exceeded;
                                    InspectNotification3(Ropeid, Max_running_hours_exceeded, NotiAlertType, DateTime.Now.Date, OperationId);
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(assignednumber))
                                {

                                    var Max_running_hours_Approaching = "Max running hours approaching (Current - " + rnghrs + "hrs / Max - " + maxrnghrs + " hrs) for Line '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                    int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Approaching;
                                    InspectNotification(Ropeid, Max_running_hours_Approaching, NotiAlertType, DateTime.Now.Date, OperationId);
                                }
                                else
                                {
                                    var Max_running_hours_Approaching = "Max running hours approaching (Current - " + rnghrs + "hrs / Max - " + maxrnghrs + " hrs) for Line '" + certificatenumber + " - " + uniqueId; // + "' located at " + assignednumber + " (" + location + ") ";
                                    int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Approaching;
                                    InspectNotification(Ropeid, Max_running_hours_Approaching, NotiAlertType, DateTime.Now.Date, OperationId);
                                }
                            }

                        }
                    }

                    /// max month allowed notification
                   
                    DateTime installdt = Convert.ToDateTime(dt1.Rows[0]["InstalledDate"]).Date;
                    int monthdiff = 0;

                    monthdiff = StaticHelper.DateDiffInMonths(installdt, DateTime.Now.Date);// Convert.ToInt32(kk.Rows[0][0]);
                    int approachingmonthdiff = monthdiff - 12;

                    if (MaxFixMonth != 0)
                    {
                        if (monthdiff >= approachingmonthdiff)
                        {
                            if (monthdiff > MaxFixMonth)
                            {
                                if (!string.IsNullOrEmpty(assignednumber))
                                {
                                    var Max_allowable_time_exceeded = "Max allowable time of " + monthdiff + " month exceeded for Line '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Exceeded;
                                    InspectNotification3(Ropeid, Max_allowable_time_exceeded, NotiAlertType, DateTime.Now.Date, OperationId);
                                }
                                else
                                {
                                    var Max_allowable_time_exceeded = "Max allowable time of " + monthdiff + " month exceeded for Line '" + certificatenumber + " - " + uniqueId; // + "' located at " + assignednumber + " (" + location + ") ";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Exceeded;
                                    InspectNotification3(Ropeid, Max_allowable_time_exceeded, NotiAlertType, DateTime.Now.Date, OperationId);
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(assignednumber))
                                {

                                    var Max_allowable_time_approaching = "Max allowable time approaching (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for Line '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Approaching;
                                    InspectNotification(Ropeid, Max_allowable_time_approaching, NotiAlertType, DateTime.Now.Date, OperationId);
                                }
                                else
                                {
                                    var Max_allowable_time_approaching = "Max allowable time approaching (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for Line '" + certificatenumber + " - " + uniqueId; // + "' located at " + assignednumber + " (" + location + ") ";
                                    int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Approaching;
                                    InspectNotification(Ropeid, Max_allowable_time_approaching, NotiAlertType, DateTime.Now.Date, OperationId);

                                }
                            }
                        }
                    }

                   
                }
            }
            catch { }

            */
        }


        private void NotificationTailDiscard(decimal hours, int OperationId, int TailID)
        {
            /*
            try
            {


                try
                {
                    /// max RunningHrs notification
                   
                    SqlDataAdapter adp1 = new SqlDataAdapter("select * from MooringRopeDetail where RopeTail=1 and DeleteStatus=0 and  Id = " + TailID + " ", sc.con);
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        string assignednumber = ""; string location = ""; string certificatenumber = ""; string uniqueId = "";
                        int Ropeid = Convert.ToInt32(dt1.Rows[0]["Id"]);
                        int RopeTypeId = Convert.ToInt32(dt1.Rows[0]["RopeTypeId"]);
                        int ManufacturerId = Convert.ToInt32(dt1.Rows[0]["ManufacturerId"]);

                        int MaxFixMonth = 0; int maxrnghrs = 0;
                        SqlDataAdapter adpSet = new SqlDataAdapter("select * from tblRopeTailInspectionSetting where MooringRopeType=" + RopeTypeId + " and ManufacturerType=" + ManufacturerId + "", sc.con);
                        DataTable dtSet = new DataTable();
                        adpSet.Fill(dtSet);
                        if (dtSet.Rows.Count > 0)
                        {

                            MaxFixMonth = Convert.ToInt32(dtSet.Rows[0]["MaximumMonthsAllowed"]);

                            maxrnghrs = Convert.ToInt32(dtSet.Rows[0]["MaximumRunningHours"]);
                        }


                        SqlDataAdapter pp = new SqlDataAdapter("select winchid from AssignRopeToWinch where RopeId=" + Ropeid + "", sc.con);
                        DataTable dtt = new DataTable();
                        pp.Fill(dtt);
                        if (dtt.Rows.Count > 0)
                        {
                            SqlDataAdapter pp1 = new SqlDataAdapter("select * from MooringWinchDetail where ID=" + Convert.ToInt32(dtt.Rows[0][0]) + "", sc.con);
                            DataTable dtt1 = new DataTable();
                            pp1.Fill(dtt1);
                            if (dtt1.Rows.Count > 0)
                            {
                                assignednumber = dtt1.Rows[0]["AssignedNumber"].ToString();
                                location = dtt1.Rows[0]["Location"].ToString();
                            }
                        }


                        certificatenumber = dt1.Rows[0]["CertificateNumber"].ToString();
                        uniqueId = dt1.Rows[0]["UniqueID"].ToString();
                        //int rnghrs = Convert.ToInt32(dt1.Rows[0]["CurrentRunningHours"]);
                        decimal rnghrs = hours;


                        if (maxrnghrs != 0)
                        {
                            int total = maxrnghrs * 80 / 100;
                            if (rnghrs >= total)
                            {
                                if (rnghrs >= maxrnghrs)
                                {
                                    if (!string.IsNullOrEmpty(assignednumber))
                                    {


                                        var Max_running_hours_exceeded = "Max running hours exceeded (Current - " + rnghrs + "hrs / Max - " + maxrnghrs + " hrs) for rope tail '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                        int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Exceeded;
                                        InspectNotification3(Ropeid, Max_running_hours_exceeded, NotiAlertType, DateTime.Now.Date, OperationId);
                                    }
                                    else
                                    {
                                        var Max_running_hours_exceeded = "Max running hours exceeded (Current - " + rnghrs + "hrs / Max - " + maxrnghrs + " hrs) for rope tail '" + certificatenumber + " - " + uniqueId; // + "' located at " + assignednumber + " (" + location + ") ";
                                        int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Exceeded;
                                        InspectNotification3(Ropeid, Max_running_hours_exceeded, NotiAlertType, DateTime.Now.Date, OperationId);
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(assignednumber))
                                    {

                                        var Max_running_hours_Approaching = "Max running hours approaching (Current - " + rnghrs + "hrs / Max - " + maxrnghrs + " hrs) for rope tail '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                        int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Approaching;
                                        InspectNotification(Ropeid, Max_running_hours_Approaching, NotiAlertType, DateTime.Now.Date, OperationId);
                                    }
                                    else
                                    {
                                        var Max_running_hours_Approaching = "Max running hours approaching (Current - " + rnghrs + "hrs / Max - " + maxrnghrs + " hrs) for rope tail '" + certificatenumber + " - " + uniqueId; // "' located at " + assignednumber + " (" + location + ") ";
                                        int NotiAlertType = (int)NotificationAlertType.Max_running_hours_Approaching;
                                        InspectNotification(Ropeid, Max_running_hours_Approaching, NotiAlertType, DateTime.Now.Date, OperationId);
                                    }
                                }

                            }
                        }

                        /// max month allowed notification
                        DateTime installdt = Convert.ToDateTime(dt1.Rows[0]["InstalledDate"]).Date;
                        int monthdiff = 0;

                        monthdiff = StaticHelper.DateDiffInMonths(installdt, DateTime.Now.Date);// Convert.ToInt32(kk.Rows[0][0]);
                        int approachingmonthdiff = monthdiff - 12;

                        if (MaxFixMonth != 0)
                        {
                            if (monthdiff >= approachingmonthdiff)
                            {
                                if (monthdiff > MaxFixMonth)
                                {
                                    if (assignednumber != null)
                                    {
                                        var Max_allowable_time_exceeded = "Max allowable time of " + monthdiff + " month exceeded for rope tail '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                        int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Exceeded;
                                        InspectNotification3(Ropeid, Max_allowable_time_exceeded, NotiAlertType, DateTime.Now.Date, OperationId);
                                    }
                                    else
                                    {
                                        var Max_allowable_time_exceeded = "Max allowable time of " + monthdiff + " month exceeded";
                                        int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Exceeded;
                                        InspectNotification3(Ropeid, Max_allowable_time_exceeded, NotiAlertType, DateTime.Now.Date, OperationId);
                                    }
                                }
                                else
                                {
                                    if (assignednumber != null && assignednumber != "")
                                    {

                                        var Max_allowable_time_approaching = "Max allowable time approaching (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for rope tail '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                        int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Approaching;
                                        InspectNotification(Ropeid, Max_allowable_time_approaching, NotiAlertType, DateTime.Now.Date, OperationId);
                                    }
                                    else
                                    {
                                        var Max_allowable_time_approaching = "Max allowable time approaching (Current - " + monthdiff + " month / Max - " + MaxFixMonth + " Months) for rope tail '" + certificatenumber + " - " + uniqueId + "' located at " + assignednumber + " (" + location + ") ";
                                        int NotiAlertType = (int)NotificationAlertType.Max_allowable_time_Approaching;
                                        InspectNotification(Ropeid, Max_allowable_time_approaching, NotiAlertType, DateTime.Now.Date, OperationId);

                                    }
                                }
                            }
                        }
                    }
                }
                catch { }



               

            }
            catch { }
            */
        }

        public void InspectNotification(int RopeID, string NotiMsg, int NotiAlertType, DateTime DueDate, int Mop_Id)
        {
            var result = sc.Notifications.Where(x => x.RopeId == RopeID & x.NotificationDueDate == DueDate & x.NotificationAlertType == NotiAlertType).FirstOrDefault();
            if (result == null)
            {
                NotificationsClass noti = new NotificationsClass();
                noti.Acknowledge = false;
                noti.AckRecord = "Not yet acknowledged";
                //noti.AckRecord = "This notification cannot be acknowledged, kindly discard it";
                noti.Notification = NotiMsg;
                noti.RopeId = RopeID;
                noti.IsActive = true;
                noti.NotificationDueDate = DueDate;
                noti.CreatedDate = DateTime.Now;
                noti.CreatedBy = "Admin";
                noti.NotificationAlertType = NotiAlertType;
                noti.NotificationType = 1;
                noti.MOP_Id = Mop_Id;
                sc.Notifications.Add(noti);
                sc.SaveChanges();

                StaticHelper.AlarmFunction(1, true);
            }
        }

        public void InspectNotification3(int RopeID, string NotiMsg, int NotiAlertType, DateTime DueDate, int Mop_Id)
        {
            var result = sc.Notifications.Where(x => x.RopeId == RopeID & x.NotificationDueDate == DueDate & x.NotificationAlertType == NotiAlertType).FirstOrDefault();
            if (result == null)
            {
                NotificationsClass noti = new NotificationsClass();
                noti.Acknowledge = false;
                //noti.AckRecord = "Not yet acknowledged";
                noti.AckRecord = "This notification cannot be acknowledged, kindly discard it";
                noti.Notification = NotiMsg;
                noti.RopeId = RopeID;
                noti.IsActive = true;
                noti.NotificationDueDate = DueDate;
                noti.CreatedDate = DateTime.Now;
                noti.CreatedBy = "Admin";
                noti.NotificationAlertType = NotiAlertType;
                noti.NotificationType = 1;
                noti.MOP_Id = Mop_Id;
                sc.Notifications.Add(noti);
                sc.SaveChanges();

                StaticHelper.AlarmFunction(1, true);

            }
        }
        private void SaveRopeTailMooringOperation()
        {
            var DtimeFast = FastDate.ToShortDateString() + " " + SHours + ":" + SMints;
            MOperationBirth.FastDatetime = Convert.ToDateTime(DtimeFast);

            var DtimeCast = CastDate.ToShortDateString() + " " + SHours2 + ":" + SMints2;
            MOperationBirth.CastDatetime = Convert.ToDateTime(DtimeCast);


            int OperationID = 0;
            OperationID = sc.MOperationBirthDetailTbl.Max(x => x.OPId);



            // Main Grid all Winches
            //  LoadWinchlist.Where(x => x.Mark == false).ToList();
            var ropelist = sc.MooringWinchRope.ToList();
            var assignRope = sc.AssignRopetoWinch.ToList();
            var rpinspsetting = sc.RopeInspectionSetting.ToList();

            // int ropetypeid = 0;
            try
            {     // WinchCheckClass
                foreach (var item in LoadWinchlist.Where(x => x.Mark == true && x.VisibilityCheck == "Visible").ToList())
                {
                    item.StartDatetime = MOperationBirth.FastDatetime;
                    item.EndDatetime = MOperationBirth.CastDatetime;

                    DateTime dtto = Convert.ToDateTime(MOperationBirth.FastDatetime);
                    DateTime dtfrom = Convert.ToDateTime(MOperationBirth.CastDatetime);
                    var diff = dtfrom.Subtract(dtto);
                    //int hours = Convert.ToInt32(diff.TotalHours);


                    // var ropeid = assignRope.Where(x => x.WinchId == item.WinId && x.IsActive == true && x.RopeTail == 1).Select(x => x.RopeId).SingleOrDefault();
                    // ropetypeid = Convert.ToInt32(ropelist.Where(x => x.Id == ropeid).Select(x => x.RopeTypeId).SingleOrDefault());
                    // do save code
                    var Mytails = item.Tails.Where(x => x.Selected == true).ToList();
                    foreach (var Tailitem in Mytails)
                    {
                        decimal hours = Convert.ToDecimal(diff.TotalHours);
                        hours = Math.Round(hours, 2);


                        int ropeid = Tailitem.Id;

                        int notiid = sc.NextNotiId();
                        //used.NotificationId = notiid;

                        MOUsedWinchTbl used = new MOUsedWinchTbl();
                        used.OperationID = OperationID;
                        used.GridID = item.GridID;
                        used.OPDateFrom = item.StartDatetime;
                        used.OPDateTo = item.EndDatetime;
                        used.WinchId = item.WinId;
                        used.RopeId = ropeid;
                        used.NotificationId = notiid;
                        used.RopeTail = 1;
                        used.RunningHours = hours > 0 ? hours : 0;
                        used.Lead = item.Lead;
                        used.Lead1 = item.Lead1;
                        if (item.outboard1 == "A")
                        {
                            used.Outboard = true;
                        }
                        if (item.outboard1 == "B")
                        {
                            used.Outboard = false;
                        }
                        used.Lead = used.Lead.Replace(System.Environment.NewLine, "").Trim();
                        sc.MOUsedWinchTbl.Add(used);
                        sc.SaveChanges();

                        if (hours > 0)
                        {

                            try
                            {
                                var rninghrs = ropelist.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                                hours = Convert.ToDecimal(rninghrs) + hours;
                            }
                            catch { }

                            var result = ropelist.SingleOrDefault(b => b.Id == ropeid);
                            if (result != null)
                            {
                                result.CurrentRunningHours = hours;
                                result.ModifiedBy = "Admin";
                                result.ModifiedDate = DateTime.Now;
                                sc.SaveChanges();
                            }

                            NotificationTailDiscard(hours, OperationID, ropeid);

                        }

                    }
                }
                //------------------------
                // Grid 1 Selected Winches >
                var Grid1FDatetime = DpSubFastGrid1 + " " + DpSubFastGrid1_Hours + ":" + DpSubFastGrid1_Mint;
                DateTime GS1 = Convert.ToDateTime(Grid1FDatetime);

                var Grid1CDatetime = DpSubCastGrid1 + " " + DpSubCastGrid1_Hours + ":" + DpSubCastGrid1_Mint;
                DateTime GE1 = Convert.ToDateTime(Grid1CDatetime);
                foreach (var item in LoadWinchListGrid1.Where(x => x.Mark == true && x.VisibilityCheck == "Visible").ToList())
                {
                    item.StartDatetime = GS1;
                    item.EndDatetime = GE1;
                    // do save code

                    DateTime dtto = Convert.ToDateTime(GS1);
                    DateTime dtfrom = Convert.ToDateTime(GE1);
                    var diff = dtfrom.Subtract(dtto);
                    //int hours = Convert.ToInt32(diff.TotalHours);


                    //  var ropeid = assignRope.Where(x => x.WinchId == item.WinId && x.IsActive == true && x.RopeTail == 1).Select(x => x.RopeId).SingleOrDefault();

                    var Mytails = item.Tails.Where(x => x.Selected == true).ToList();
                    foreach (var Tailitem in Mytails)
                    {
                        decimal hours = Convert.ToDecimal(diff.TotalHours);
                        hours = Math.Round(hours, 2);

                        int ropeid = Tailitem.Id;

                        int notiid = sc.NextNotiId();

                        MOUsedWinchTbl used = new MOUsedWinchTbl();
                        used.OperationID = OperationID;
                        used.GridID = item.GridID;
                        used.OPDateFrom = item.StartDatetime;
                        used.OPDateTo = item.EndDatetime;
                        used.WinchId = item.WinId;
                        used.RopeId = ropeid;
                        used.NotificationId = notiid;
                        used.RopeTail = 1;
                        used.RunningHours = hours;
                        used.Lead = item.Lead;
                        used.Lead1 = item.Lead1;
                        if (item.outboard1 == "A")
                        {
                            used.Outboard = true;
                        }
                        if (item.outboard1 == "B")
                        {
                            used.Outboard = false;
                        }
                        used.Lead = used.Lead.Replace(System.Environment.NewLine, "").Trim();
                        sc.MOUsedWinchTbl.Add(used);
                        sc.SaveChanges();

                        if (hours > 0)
                        {

                            try
                            {
                                var rninghrs = ropelist.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                                hours = Convert.ToDecimal(rninghrs) + hours;
                            }
                            catch { }
                            var result = ropelist.SingleOrDefault(b => b.Id == ropeid);
                            if (result != null)
                            {
                                result.CurrentRunningHours = hours;
                                result.ModifiedBy = "Admin";
                                result.ModifiedDate = DateTime.Now;
                                sc.SaveChanges();
                            }

                            NotificationTailDiscard(hours, OperationID, ropeid);
                        }
                    }
                }


                // Grid 2 Selected Winches >
                var Grid2FDatetime = DpSubFastGrid2 + " " + DpSubFastGrid2_Hours + ":" + DpSubFastGrid2_Mint;
                DateTime GS2 = Convert.ToDateTime(Grid2FDatetime);

                var Grid2CDatetime = DpSubCastGrid2 + " " + DpSubCastGrid2_Hours + ":" + DpSubCastGrid2_Mint;
                DateTime GE2 = Convert.ToDateTime(Grid2CDatetime);
                foreach (var item in LoadWinchListGrid2.Where(x => x.Mark == true && x.VisibilityCheck == "Visible").ToList())
                {
                    item.StartDatetime = GS2;
                    item.EndDatetime = GE2;
                    // do save code

                    DateTime dtto = Convert.ToDateTime(GS2);
                    DateTime dtfrom = Convert.ToDateTime(GE2);
                    var diff = dtfrom.Subtract(dtto);

                    var Mytails = item.Tails.Where(x => x.Selected == true).ToList();
                    foreach (var Tailitem in Mytails)
                    {
                        decimal hours = Convert.ToDecimal(diff.TotalHours);
                        hours = Math.Round(hours, 2);

                        int ropeid = Tailitem.Id;
                        int notiid = sc.NextNotiId();
                        //used.NotificationId = notiid;
                        MOUsedWinchTbl used = new MOUsedWinchTbl();
                        used.OperationID = OperationID;
                        used.GridID = item.GridID;
                        used.OPDateFrom = item.StartDatetime;
                        used.OPDateTo = item.EndDatetime;
                        used.WinchId = item.WinId;
                        used.RopeId = ropeid;
                        used.NotificationId = notiid;
                        used.RopeTail = 1;
                        used.RunningHours = hours > 0 ? hours : 0;
                        used.Lead = item.Lead;
                        used.Lead1 = item.Lead1;
                        if (item.outboard1 == "A")
                        {
                            used.Outboard = true;
                        }
                        if (item.outboard1 == "B")
                        {
                            used.Outboard = false;
                        }
                        used.Lead = used.Lead.Replace(System.Environment.NewLine, "").Trim();
                        sc.MOUsedWinchTbl.Add(used);
                        sc.SaveChanges();

                        if (hours > 0)
                        {

                            try
                            {
                                var rninghrs = ropelist.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                                hours = Convert.ToDecimal(rninghrs) + hours;
                            }
                            catch { }
                            var result = ropelist.SingleOrDefault(b => b.Id == ropeid);
                            if (result != null)
                            {
                                result.CurrentRunningHours = hours;
                                result.ModifiedBy = "Admin";
                                result.ModifiedDate = DateTime.Now;
                                sc.SaveChanges();
                            }

                            NotificationTailDiscard(hours, OperationID, ropeid);
                        }
                    }
                }

                // Grid 3 Selected Winches >
                var Grid3FDatetime = DpSubFastGrid3 + " " + DpSubFastGrid3_Hours + ":" + DpSubFastGrid3_Mint;
                DateTime GS3 = Convert.ToDateTime(Grid3FDatetime);

                var Grid3CDatetime = DpSubCastGrid3 + " " + DpSubCastGrid3_Hours + ":" + DpSubCastGrid3_Mint;
                DateTime GE3 = Convert.ToDateTime(Grid3CDatetime);
                foreach (var item in LoadWinchListGrid3.Where(x => x.Mark == true && x.VisibilityCheck == "Visible").ToList())
                {
                    item.StartDatetime = GS3;
                    item.EndDatetime = GE3;
                    // do save code

                    DateTime dtto = Convert.ToDateTime(GS3);
                    DateTime dtfrom = Convert.ToDateTime(GE3);
                    var diff = dtfrom.Subtract(dtto);

                    var Mytails = item.Tails.Where(x => x.Selected == true).ToList();
                    foreach (var Tailitem in Mytails)
                    {
                        decimal hours = Convert.ToDecimal(diff.TotalHours);
                        hours = Math.Round(hours, 2);

                        int ropeid = Tailitem.Id;
                        int notiid = sc.NextNotiId();

                        MOUsedWinchTbl used = new MOUsedWinchTbl();
                        used.OperationID = OperationID;
                        used.GridID = item.GridID;
                        used.OPDateFrom = item.StartDatetime;
                        used.OPDateTo = item.EndDatetime;
                        used.WinchId = item.WinId;
                        used.RopeId = ropeid;
                        used.NotificationId = notiid;
                        used.RopeTail = 1;
                        used.RunningHours = hours > 0 ? hours : 0;
                        used.Lead = item.Lead;
                        used.Lead1 = item.Lead1;
                        if (item.outboard1 == "A")
                        {
                            used.Outboard = true;
                        }
                        if (item.outboard1 == "B")
                        {
                            used.Outboard = false;
                        }
                        used.Lead = used.Lead.Replace(System.Environment.NewLine, "").Trim();
                        sc.MOUsedWinchTbl.Add(used);
                        sc.SaveChanges();

                        if (hours > 0)
                        {

                            try
                            {
                                var rninghrs = ropelist.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                                hours = Convert.ToDecimal(rninghrs) + hours;
                            }
                            catch { }
                            var result = ropelist.SingleOrDefault(b => b.Id == ropeid);
                            if (result != null)
                            {
                                result.CurrentRunningHours = hours;
                                result.ModifiedBy = "Admin";
                                result.ModifiedDate = DateTime.Now;
                                sc.SaveChanges();
                            }

                            NotificationTailDiscard(hours, OperationID, ropeid);
                        }
                    }
                }


                // Grid 4 Selected Winches >
                var Grid4FDatetime = DpSubFastGrid4 + " " + DpSubFastGrid4_Hours + ":" + DpSubFastGrid4_Mint;
                DateTime GS4 = Convert.ToDateTime(Grid4FDatetime);

                var Grid4CDatetime = DpSubCastGrid4 + " " + DpSubCastGrid4_Hours + ":" + DpSubCastGrid4_Mint;
                DateTime GE4 = Convert.ToDateTime(Grid4CDatetime);
                foreach (var item in LoadWinchListGrid4.Where(x => x.Mark == true && x.VisibilityCheck == "Visible").ToList())
                {
                    item.StartDatetime = GS4;
                    item.EndDatetime = GE4;
                    // do save code

                    DateTime dtto = Convert.ToDateTime(GS4);
                    DateTime dtfrom = Convert.ToDateTime(GE4);
                    var diff = dtfrom.Subtract(dtto);

                    var Mytails = item.Tails.Where(x => x.Selected == true).ToList();
                    foreach (var Tailitem in Mytails)
                    {
                        decimal hours = Convert.ToDecimal(diff.TotalHours);
                        hours = Math.Round(hours, 2);

                        int ropeid = Tailitem.Id;
                        int notiid = sc.NextNotiId();

                        MOUsedWinchTbl used = new MOUsedWinchTbl();
                        used.OperationID = OperationID;
                        used.GridID = item.GridID;
                        used.OPDateFrom = item.StartDatetime;
                        used.OPDateTo = item.EndDatetime;
                        used.WinchId = item.WinId;
                        used.RopeId = ropeid;
                        used.NotificationId = notiid;
                        used.RopeTail = 1;
                        used.RunningHours = hours > 0 ? hours : 0;
                        used.Lead = item.Lead;
                        used.Lead1 = item.Lead1;
                        if (item.outboard1 == "A")
                        {
                            used.Outboard = true;
                        }
                        if (item.outboard1 == "B")
                        {
                            used.Outboard = false;
                        }
                        used.Lead = used.Lead.Replace(System.Environment.NewLine, "").Trim();
                        sc.MOUsedWinchTbl.Add(used);
                        sc.SaveChanges();

                        if (hours > 0)
                        {

                            try
                            {
                                var rninghrs = ropelist.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                                hours = Convert.ToDecimal(rninghrs) + hours;
                            }
                            catch { }
                            var result = ropelist.SingleOrDefault(b => b.Id == ropeid);
                            if (result != null)
                            {
                                result.CurrentRunningHours = hours;
                                result.ModifiedBy = "Admin";
                                result.ModifiedDate = DateTime.Now;
                                sc.SaveChanges();
                            }

                            NotificationTailDiscard(hours, OperationID, ropeid);

                        }
                    }
                }

            }
            catch { }

        }

        private void UpdateMooringOPR()
        {
            try
            {
                MOperationBirthDetail moorOPR = new MOperationBirthDetail();
                var local = sc.Set<MOperationBirthDetail>().Local.FirstOrDefault(f => f.OPId == moorOPR.OPId);
                if (local != null)
                {
                    sc.Entry(local).State = EntityState.Detached;
                }

                var DtimeFast = FastDate.ToShortDateString() + " " + SHours + ":" + SMints;
                MOperationBirth.FastDatetime = Convert.ToDateTime(DtimeFast);

                var DtimeCast = CastDate.ToShortDateString() + " " + SHours2 + ":" + SMints2;
                MOperationBirth.CastDatetime = Convert.ToDateTime(DtimeCast);

                var asd = MOperationBirth;


                sc.Entry(MOperationBirth).State = EntityState.Modified;
                sc.SaveChanges();

                //sc.MOperationBirthDetailTbl.Add(MOperationBirth);
                //sc.SaveChanges();


                //sc.SaveChanges();
                //}

                var UpdatedRopedetails = new MOperationBirthDetail()
                {
                    OPId = moorOPR.OPId,

                };

                //sc.Entry(UpdatedRopedetails).State = EntityState.Modified;
                //sc.SaveChanges();


                foreach (var item in LoadWinchlist.Where(x => x.Mark == true).ToList())
                {
                    item.StartDatetime = MOperationBirth.FastDatetime;
                    item.EndDatetime = MOperationBirth.CastDatetime;

                    DateTime dtto = Convert.ToDateTime(MOperationBirth.FastDatetime);
                    DateTime dtfrom = Convert.ToDateTime(MOperationBirth.CastDatetime);
                    var diff = dtfrom.Subtract(dtto);
                    //int hours = Convert.ToInt32(diff.TotalHours);

                    decimal hours = Convert.ToDecimal(diff.TotalHours);
                    if (hours > 0)
                    {
                        var OPID = sc.MOUsedWinchTbl.Where(b => b.OperationID == MOperationBirth.OPId && b.GridID == "Grid0").FirstOrDefault();

                        decimal hrs = Convert.ToDecimal(OPID.RunningHours);
                        if (OPID != null)
                        {
                            OPID.OPDateFrom = item.StartDatetime;
                            OPID.OPDateTo = item.EndDatetime;
                            OPID.RunningHours = hours;
                            sc.SaveChanges();
                        }


                        var ropeid = sc.AssignRopetoWinch.Where(x => x.WinchId == item.WinId && x.IsActive == true).Select(x => x.RopeId).SingleOrDefault();

                        try
                        {
                            var rninghrs = sc.MooringWinchRope.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                            //hours = Convert.ToInt32(rninghrs) - hrs;
                            //hours = Convert.ToInt32(rninghrs) + hours;

                            hours = Convert.ToDecimal(rninghrs) - hrs;
                            hours = Convert.ToDecimal(rninghrs) + hours;
                        }
                        catch { }

                        //hours = hrs - hours;
                        var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == ropeid);
                        if (result != null)
                        {
                            result.CurrentRunningHours = hours;
                            result.ModifiedBy = "Admin";
                            result.ModifiedDate = DateTime.Now;
                            sc.SaveChanges();
                        }
                    }
                }

                //------------------------
                // Grid 1 Selected Winches >
                if (LoadWinchListGrid1.Where(x => x.Mark == true).Count() > 0)
                {
                    var Grid1FDatetime = DpSubFastGrid1 + " " + DpSubFastGrid1_Hours + ":" + DpSubFastGrid1_Mint;
                    var Grid1CDatetime = DpSubCastGrid1 + " " + DpSubCastGrid1_Hours + ":" + DpSubCastGrid1_Mint;

                    if (Grid1FDatetime != null & Grid1CDatetime != null)
                    {
                        DateTime GS1 = Convert.ToDateTime(Grid1FDatetime);
                        DateTime GE1 = Convert.ToDateTime(Grid1CDatetime);
                        foreach (var item in LoadWinchListGrid1.Where(x => x.Mark == true).ToList())
                        {
                            item.StartDatetime = GS1;
                            item.EndDatetime = GE1;
                            // do save code

                            DateTime dtto = Convert.ToDateTime(GS1);
                            DateTime dtfrom = Convert.ToDateTime(GE1);
                            var diff = dtfrom.Subtract(dtto);
                            //int hours = Convert.ToInt32(diff.TotalHours);
                            decimal hours = Convert.ToDecimal(diff.TotalHours);

                            if (hours > 0)
                            {
                                var OPID = sc.MOUsedWinchTbl.Where(b => b.OperationID == MOperationBirth.OPId && b.GridID == "Grid1").SingleOrDefault();

                                decimal hrs = Convert.ToDecimal(OPID.RunningHours);
                                if (OPID != null)
                                {
                                    OPID.OPDateFrom = item.StartDatetime;
                                    OPID.OPDateTo = item.EndDatetime;
                                    OPID.RunningHours = hours;
                                    sc.SaveChanges();
                                }


                                var ropeid = sc.AssignRopetoWinch.Where(x => x.WinchId == item.WinId && x.IsActive == true).Select(x => x.RopeId).SingleOrDefault();
                                try
                                {
                                    var rninghrs = sc.MooringWinchRope.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                                    //hours = Convert.ToInt32(rninghrs) - hrs;
                                    //hours = Convert.ToInt32(rninghrs) + hours;

                                    hours = Convert.ToDecimal(rninghrs) - hrs;
                                    hours = Convert.ToDecimal(rninghrs) + hours;
                                }
                                catch { }
                                hours = hrs - hours;
                                var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == ropeid);
                                if (result != null)
                                {
                                    result.CurrentRunningHours = hours;
                                    result.ModifiedBy = "Admin";
                                    result.ModifiedDate = DateTime.Now;
                                    sc.SaveChanges();
                                }
                            }
                        }
                    }
                }

                // Grid 2 Selected Winches >
                if (LoadWinchListGrid2.Where(x => x.Mark == true).Count() > 0)
                {

                    var Grid2FDatetime = DpSubFastGrid2 + " " + DpSubFastGrid2_Hours + ":" + DpSubFastGrid2_Mint;
                    var Grid2CDatetime = DpSubCastGrid2 + " " + DpSubCastGrid2_Hours + ":" + DpSubCastGrid2_Mint;

                    if (Grid2FDatetime != null & Grid2CDatetime != null)
                    {
                        if (Grid2FDatetime.Trim() != ":" & Grid2CDatetime.Trim() != ":")
                        {

                            DateTime GS2 = Convert.ToDateTime(Grid2FDatetime);
                            DateTime GE2 = Convert.ToDateTime(Grid2CDatetime);
                            foreach (var item in LoadWinchListGrid2.Where(x => x.Mark == true).ToList())
                            {
                                item.StartDatetime = GS2;
                                item.EndDatetime = GE2;
                                // do save code

                                DateTime dtto = Convert.ToDateTime(GS2);
                                DateTime dtfrom = Convert.ToDateTime(GE2);
                                var diff = dtfrom.Subtract(dtto);
                                //int hours = Convert.ToInt32(diff.TotalHours);
                                decimal hours = Convert.ToDecimal(diff.TotalHours);

                                if (hours > 0)
                                {
                                    var OPID = sc.MOUsedWinchTbl.SingleOrDefault(b => b.OperationID == MOperationBirth.OPId && b.GridID == "Grid2");

                                    decimal hrs = Convert.ToDecimal(OPID.RunningHours);
                                    if (OPID != null)
                                    {
                                        OPID.OPDateFrom = item.StartDatetime;
                                        OPID.OPDateTo = item.EndDatetime;
                                        OPID.RunningHours = hours;
                                        sc.SaveChanges();
                                    }


                                    var ropeid = sc.AssignRopetoWinch.Where(x => x.WinchId == item.WinId && x.IsActive == true).Select(x => x.RopeId).SingleOrDefault();
                                    try
                                    {
                                        var rninghrs = sc.MooringWinchRope.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                                        //hours = Convert.ToInt32(rninghrs) - hrs;
                                        //hours = Convert.ToInt32(rninghrs) + hours;

                                        hours = Convert.ToDecimal(rninghrs) - hrs;
                                        hours = Convert.ToDecimal(rninghrs) + hours;
                                    }
                                    catch { }
                                    hours = hrs - hours;
                                    var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == ropeid);
                                    if (result != null)
                                    {
                                        result.CurrentRunningHours = hours;
                                        result.ModifiedBy = "Admin";
                                        result.ModifiedDate = DateTime.Now;
                                        sc.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }


                // Grid 3 Selected Winches >
                if (LoadWinchListGrid3.Where(x => x.Mark == true).Count() > 0)
                {
                    var Grid3FDatetime = DpSubFastGrid3 + " " + DpSubFastGrid3_Hours + ":" + DpSubFastGrid3_Mint;
                    var Grid3CDatetime = DpSubCastGrid3 + " " + DpSubCastGrid3_Hours + ":" + DpSubCastGrid3_Mint;
                    if (Grid3FDatetime != null & Grid3CDatetime != null)
                    {
                        DateTime GS3 = Convert.ToDateTime(Grid3FDatetime);
                        DateTime GE3 = Convert.ToDateTime(Grid3CDatetime);
                        foreach (var item in LoadWinchListGrid3.Where(x => x.Mark == true).ToList())
                        {
                            item.StartDatetime = GS3;
                            item.EndDatetime = GE3;
                            // do save code

                            DateTime dtto = Convert.ToDateTime(GS3);
                            DateTime dtfrom = Convert.ToDateTime(GE3);
                            var diff = dtfrom.Subtract(dtto);
                            //int hours = Convert.ToInt32(diff.TotalHours);
                            decimal hours = Convert.ToDecimal(diff.TotalHours);
                            if (hours > 0)
                            {
                                var OPID = sc.MOUsedWinchTbl.SingleOrDefault(b => b.OperationID == MOperationBirth.OPId && b.GridID == "Grid3");

                                decimal hrs = Convert.ToDecimal(OPID.RunningHours);
                                if (OPID != null)
                                {
                                    OPID.OPDateFrom = item.StartDatetime;
                                    OPID.OPDateTo = item.EndDatetime;
                                    OPID.RunningHours = hours;
                                    sc.SaveChanges();
                                }


                                var ropeid = sc.AssignRopetoWinch.Where(x => x.WinchId == item.WinId && x.IsActive == true).Select(x => x.RopeId).SingleOrDefault();
                                try
                                {
                                    var rninghrs = sc.MooringWinchRope.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                                    //hours = Convert.ToInt32(rninghrs) - hrs;
                                    //hours = Convert.ToInt32(rninghrs) + hours;

                                    hours = Convert.ToDecimal(rninghrs) - hrs;
                                    hours = Convert.ToDecimal(rninghrs) + hours;
                                }
                                catch { }
                                hours = hrs - hours;
                                var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == ropeid);
                                if (result != null)
                                {
                                    result.CurrentRunningHours = hours;
                                    result.ModifiedBy = "Admin";
                                    result.ModifiedDate = DateTime.Now;
                                    sc.SaveChanges();
                                }
                            }
                        }
                    }
                }


                // Grid 4 Selected Winches >
                if (LoadWinchListGrid4.Where(x => x.Mark == true).Count() > 0)
                {
                    var Grid4FDatetime = DpSubFastGrid4 + " " + DpSubFastGrid4_Hours + ":" + DpSubFastGrid4_Mint;
                    var Grid4CDatetime = DpSubCastGrid4 + " " + DpSubCastGrid4_Hours + ":" + DpSubCastGrid4_Mint;

                    if (Grid4FDatetime != null & Grid4CDatetime != null)
                    {


                        DateTime GS4 = Convert.ToDateTime(Grid4FDatetime);
                        DateTime GE4 = Convert.ToDateTime(Grid4CDatetime);
                        foreach (var item in LoadWinchListGrid4.Where(x => x.Mark == true).ToList())
                        {
                            item.StartDatetime = GS4;
                            item.EndDatetime = GE4;
                            // do save code

                            DateTime dtto = Convert.ToDateTime(GS4);
                            DateTime dtfrom = Convert.ToDateTime(GE4);
                            var diff = dtfrom.Subtract(dtto);
                            //int hours = Convert.ToInt32(diff.TotalHours);

                            decimal hours = Convert.ToDecimal(diff.TotalHours);

                            if (hours > 0)
                            {
                                var OPID = sc.MOUsedWinchTbl.SingleOrDefault(b => b.OperationID == MOperationBirth.OPId && b.GridID == "Grid4");

                                decimal hrs = Convert.ToDecimal(OPID.RunningHours);
                                if (OPID != null)
                                {
                                    OPID.OPDateFrom = item.StartDatetime;
                                    OPID.OPDateTo = item.EndDatetime;
                                    OPID.RunningHours = hours;
                                    sc.SaveChanges();
                                }


                                var ropeid = sc.AssignRopetoWinch.Where(x => x.WinchId == item.WinId && x.IsActive == true).Select(x => x.RopeId).SingleOrDefault();
                                try
                                {
                                    var rninghrs = sc.MooringWinchRope.Where(x => x.Id == ropeid).Select(x => x.CurrentRunningHours).SingleOrDefault();
                                    //hours = Convert.ToInt32(rninghrs) - hrs;
                                    //hours = Convert.ToInt32(rninghrs) + hours;

                                    hours = Convert.ToDecimal(rninghrs) - hrs;
                                    hours = Convert.ToDecimal(rninghrs) + hours;
                                }
                                catch { }
                                hours = hrs - hours;
                                var result = sc.MooringWinchRope.SingleOrDefault(b => b.Id == ropeid);
                                if (result != null)
                                {
                                    result.CurrentRunningHours = hours;
                                    result.ModifiedBy = "Admin";
                                    result.ModifiedDate = DateTime.Now;
                                    sc.SaveChanges();
                                }
                            }
                        }
                    }
                }

                SaveEnable = false;
                RaisePropertyChanged("SaveEnable");

                MessageBox.Show("Record updated successfully", "Update MooringWinch", MessageBoxButton.OK, MessageBoxImage.Information);
                if (MOperationBirth.Any_Rope_Damaged == "Yes")
                {
                    MooringOPDamagedRopeViewModel vm = new MooringOPDamagedRopeViewModel(MOperationBirth.OPId);
                    ChildWindowManager.Instance.ShowChildWindow(new MooringOPDamagedRopeView() { DataContext = vm });
                }
                if (MOperationBirth.Any_Rope_Damaged == "No")
                {
                    //MessageBox.Show("Record successfully saved !", "Mooring Operation", MessageBoxButton.OK, MessageBoxImage.Information);
                }



            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        //private void SaveMooringOperation22(MOperationBirthDetail obj)
        //{

        //}


        //private static ObservableCollection<TailOption> checkboxlist = new ObservableCollection<TailOption>();

        //public ObservableCollection<TailOption> CheckBoxlist
        //{
        //       get { return checkboxlist; }
        //       set
        //       {
        //              checkboxlist = value;
        //              OnPropertyChanged(new PropertyChangedEventArgs("CheckBoxlist"));
        //       }
        //}


        //private void Checkboxlostbind(Option obch)
        //{
        //    List<Option> templist = new List<Option>();

        //    foreach (var item in CheckBoxlist)
        //    {


        //        if (item.RopeId == obch.RopeId)
        //        {
        //            item.RopeId = 1;
        //            item.WinId = 1;
        //        }

        //        templist.Add(item);
        //    }
        //    checkboxlist.Clear();
        //    foreach (var item in templist)
        //    {
        //        checkboxlist.Add(item);
        //    }

        //    OnPropertyChanged(new PropertyChangedEventArgs("CheckBoxlist"));
        //    templist.Clear();


        //}

        //  public bool MainHit { get; set; } = true;
        public bool SubHit { get; set; } = false;
        public bool SubHit1 { get; set; } = false;
        public bool SubHit2 { get; set; } = false;
        public bool SubHit3 { get; set; } = false;
        public bool SubHit4 { get; set; } = false;

        private void MarkCheckMethod(WinchCheckClass obch)
        {
            List<WinchCheckClass> templist = new List<WinchCheckClass>();
            //var getval = LoadWinchlist.Where(x => x.WinId == obch.WinId).FirstOrDefault();
            //LoadWinchlist.Remove(getval);
            //getval.Mark = obch.Mark;
            //getval.RopeTailMark = obch.Mark;
            //LoadWinchlist.Add(getval);
            SubHit = true;
            foreach (var item in LoadWinchlist)
            {
                //if (item.WinId == obch.WinId)
                //{
                //    item.Mark = obch.Mark;
                //    item.RopeTailMark = obch.Mark;
                //}

                if (item.RopeId == obch.RopeId)
                {
                    item.Mark = obch.Mark;
                    item.RopeTailMark = obch.Mark;
                    item.Tails = GetTailsOnWinches(item.WinId, obch.Mark);
                }

                templist.Add(item);
            }
            LoadWinchlist.Clear();
            foreach (var item in templist)
            {
                LoadWinchlist.Add(item);
            }
            var ChAll = LoadWinchlist.Where(x => x.Mark == false).FirstOrDefault();
            if (ChAll != null)
            {
                MarkAll = false;
                RaisePropertyChanged("MarkAll");
            }
            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchlist"));
            templist.Clear();


        }

        private void MarkCheckMethod1(WinchCheckClass obch)
        {
            List<WinchCheckClass> templist = new List<WinchCheckClass>();
            //var getval = loadWinchlistGrid1.Where(x => x.WinId == obch.WinId).FirstOrDefault();
            //LoadWinchlist.Remove(getval);
            //getval.Mark = obch.Mark;
            //getval.RopeTailMark = obch.Mark;
            //LoadWinchlist.Add(getval);
            SubHit1 = true;

            foreach (var item in loadWinchlistGrid1)
            {
                //if (item.WinId == obch.WinId)
                //{
                //    item.Mark = obch.Mark;
                //    item.RopeTailMark = obch.Mark;
                //}
                if (item.RopeId == obch.RopeId)
                {
                    item.Mark = obch.Mark;
                    item.RopeTailMark = obch.Mark;
                    item.Tails = GetTailsOnWinches(item.WinId, obch.Mark);
                }
                templist.Add(item);
            }
            loadWinchlistGrid1.Clear();
            foreach (var item in templist)
            {
                loadWinchlistGrid1.Add(item);
            }

            var ChAll = loadWinchlistGrid1.Where(x => x.Mark == false).FirstOrDefault();
            if (ChAll != null)
            {
                Mark1All = false;

                RaisePropertyChanged("Mark1All");
            }
            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid1"));
            templist.Clear();


        }

        private void MarkCheckMethod2(WinchCheckClass obch)
        {
            List<WinchCheckClass> templist = new List<WinchCheckClass>();
            SubHit2 = true;


            foreach (var item in loadWinchlistGrid2)
            {
                //if (item.WinId == obch.WinId)
                //{
                //    item.Mark = obch.Mark;
                //    item.RopeTailMark = obch.Mark;
                //}
                if (item.RopeId == obch.RopeId)
                {
                    item.Mark = obch.Mark;
                    item.RopeTailMark = obch.Mark;
                    item.Tails = GetTailsOnWinches(item.WinId, obch.Mark);
                }
                templist.Add(item);
            }
            loadWinchlistGrid2.Clear();
            foreach (var item in templist)
            {
                loadWinchlistGrid2.Add(item);
            }
            var ChAll = loadWinchlistGrid2.Where(x => x.Mark == false).FirstOrDefault();
            if (ChAll != null)
            {
                Mark2All = false;
                RaisePropertyChanged("Mark2All");
            }
            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid2"));
            templist.Clear();


        }

        private void MarkCheckMethod3(WinchCheckClass obch)
        {
            List<WinchCheckClass> templist = new List<WinchCheckClass>();
            SubHit3 = true;
            foreach (var item in loadWinchlistGrid3)
            {
                //if (item.WinId == obch.WinId)
                //{
                //    item.Mark = obch.Mark;
                //    item.RopeTailMark = obch.Mark;
                //}
                if (item.RopeId == obch.RopeId)
                {
                    item.Mark = obch.Mark;
                    item.RopeTailMark = obch.Mark;
                    item.Tails = GetTailsOnWinches(item.WinId, obch.Mark);
                }
                templist.Add(item);
            }
            loadWinchlistGrid3.Clear();
            foreach (var item in templist)
            {
                loadWinchlistGrid3.Add(item);
            }
            var ChAll = loadWinchlistGrid3.Where(x => x.Mark == false).FirstOrDefault();
            if (ChAll != null)
            {
                Mark3All = false;
                RaisePropertyChanged("Mark3All");
            }
            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid3"));
            templist.Clear();


        }

        private void MarkCheckMethod4(WinchCheckClass obch)
        {
            List<WinchCheckClass> templist = new List<WinchCheckClass>();
            SubHit4 = true;

            foreach (var item in loadWinchlistGrid4)
            {
                //if (item.WinId == obch.WinId)
                //{
                //    item.Mark = obch.Mark;
                //    item.RopeTailMark = obch.Mark;
                //}
                if (item.RopeId == obch.RopeId)
                {
                    item.Mark = obch.Mark;
                    item.RopeTailMark = obch.Mark;
                    item.Tails = GetTailsOnWinches(item.WinId, obch.Mark);
                }
                templist.Add(item);
            }
            loadWinchlistGrid4.Clear();
            foreach (var item in templist)
            {
                loadWinchlistGrid4.Add(item);
            }
            var ChAll = loadWinchlistGrid4.Where(x => x.Mark == false).FirstOrDefault();
            if (ChAll != null)
            {
                Mark4All = false;
                RaisePropertyChanged("Mark4All");
            }
            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid4"));
            templist.Clear();


        }

        private void BindWinchList1()
        {
            // For Mark1All

            List<WinchCheckClass> templist = new List<WinchCheckClass>();
            SubHit1 = false;
            foreach (var item in loadWinchlistGrid1)
            {
                item.RopeTailMark = item.Mark = Mark1All;
                item.Tails = GetTailsOnWinches(item.WinId, Mark1All);
                templist.Add(item);
            }
            loadWinchlistGrid1.Clear();
            foreach (var item in templist)
            {
                loadWinchlistGrid1.Add(item);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid1"));
            templist.Clear();
        }

        private void BindWinchList2()
        {
            // For Mark2All

            List<WinchCheckClass> templist = new List<WinchCheckClass>();
            SubHit2 = false;
            foreach (var item in loadWinchlistGrid2)
            {
                item.RopeTailMark = item.Mark = Mark2All;
                item.Tails = GetTailsOnWinches(item.WinId, Mark2All);
                templist.Add(item);
            }
            loadWinchlistGrid2.Clear();
            foreach (var item in templist)
            {
                loadWinchlistGrid2.Add(item);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid2"));
            templist.Clear();
        }

        private void BindWinchList3()
        {
            // For Mark3All

            List<WinchCheckClass> templist = new List<WinchCheckClass>();
            SubHit3 = false;
            foreach (var item in loadWinchlistGrid3)
            {
                item.RopeTailMark = item.Mark = Mark3All;
                item.Tails = GetTailsOnWinches(item.WinId, Mark3All);
                templist.Add(item);
            }
            loadWinchlistGrid3.Clear();
            foreach (var item in templist)
            {
                loadWinchlistGrid3.Add(item);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid3"));
            templist.Clear();
        }

        private void BindWinchList4()
        {
            // For Mark4All

            List<WinchCheckClass> templist = new List<WinchCheckClass>();
            SubHit4 = false;
            foreach (var item in loadWinchlistGrid4)
            {
                item.RopeTailMark = item.Mark = Mark4All;
                item.Tails = GetTailsOnWinches(item.WinId, Mark4All);
                templist.Add(item);
            }
            loadWinchlistGrid4.Clear();
            foreach (var item in templist)
            {
                loadWinchlistGrid4.Add(item);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid4"));
            templist.Clear();
        }

        private ICommand savecommand;
        public ICommand SaveCommand
        {
            get { return savecommand; }
            set { savecommand = value; }
        }

        private ICommand gobackcommand;
        public ICommand GoBackCommand
        {
            get { return gobackcommand; }
            set { gobackcommand = value; }
        }

        private ICommand remove;

        public ICommand RemoveCommand
        {
            get { return remove; }
            set { remove = value; }
        }

        private ICommand addmore;

        public ICommand AddMoreCommand
        {
            get { return addmore; }
            set { addmore = value; }
        }

        public ICommand CheckBindCommand { get; set; }

        public ICommand CheckMarkCommand { get; set; }
        public ICommand CheckMarkCommand1 { get; set; }
        public ICommand CheckMarkCommand2 { get; set; }
        public ICommand CheckMarkCommand3 { get; set; }
        public ICommand CheckMarkCommand4 { get; set; }

        public static int rowGrid1 = 0;

        public int RowGrid1
        {
            get { return rowGrid1; }
            set
            {
                rowGrid1 = value;
                RaisePropertyChanged("RowGrid1");
            }
        }

        private int rowGrid2 = 0;

        public int RowGrid2
        {
            get { return rowGrid2; }
            set
            {
                rowGrid2 = value;
                RaisePropertyChanged("RowGrid2");
            }
        }

        private int rowGrid3 = 0;

        public int RowGrid3
        {
            get { return rowGrid3; }
            set
            {
                rowGrid3 = value;
                RaisePropertyChanged("RowGrid3");
            }
        }

        private int rowGrid4 = 0;

        public int RowGrid4
        {
            get { return rowGrid4; }
            set
            {
                rowGrid4 = value;
                RaisePropertyChanged("RowGrid4");
            }
        }

        private int birthGrid = 1000;

        public int BerthGrid
        {
            get { return birthGrid; }
            set
            {
                birthGrid = value;
                RaisePropertyChanged("BerthGrid");
            }
        }

        private void EditMethod(int OpdId)
        {
            //if (para == "Grid0")
            //{

            string qry2 = "select distinct a.winchid,b.Id,b.assignednumber from AssignRopeToWinch a  inner join MooringWinchDetail b on a.WinchId=b.Id   where a.IsActive=1 and a. WinchId  in (select winchid from MOUsedWinchTbl  where GridID='Grid0' and OperationID=" + OpdId + ")";
            SqlDataAdapter sda2 = new SqlDataAdapter(qry2, sc.con);
            DataTable datatbl2 = new DataTable();
            sda2.Fill(datatbl2);

            //LoadWinchListGrid1.Clear();


            for (int i = 0; i < datatbl2.Rows.Count; i++)
            {
                LoadWinchlist.Add(new WinchCheckClass()
                {
                    WinId = Convert.ToInt32(datatbl2.Rows[i]["WinchId"]),
                    WinchNo = datatbl2.Rows[i]["AssignedNumber"].ToString(),
                    Mark = true,
                    Lead = datatbl2.Rows[i]["Lead"].ToString(),
                    Lead1 = datatbl2.Rows[i]["Lead1"].ToString(),
                    GridID = "Grid0"  // static id 

                });
            }

            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchlist"));

            string qry1 = "select distinct a.winchid,b.Id,b.assignednumber from AssignRopeToWinch a  inner join MooringWinchDetail b on a.WinchId=b.Id   where a.IsActive=1 and a. WinchId  in (select winchid from MOUsedWinchTbl  where GridID='Grid1' and OperationID=" + OpdId + ")";
            SqlDataAdapter sda1 = new SqlDataAdapter(qry1, sc.con);
            DataTable datatbl1 = new DataTable();
            sda1.Fill(datatbl1);

            LoadWinchListGrid1.Clear();


            for (int i = 0; i < datatbl1.Rows.Count; i++)
            {
                LoadWinchListGrid1.Add(new WinchCheckClass()
                {
                    WinId = Convert.ToInt32(datatbl1.Rows[i]["Id"]),
                    WinchNo = datatbl1.Rows[i]["AssignedNumber"].ToString(),
                    Mark = true,
                    Lead = datatbl1.Rows[i]["Lead"].ToString(),
                    Lead1 = datatbl1.Rows[i]["Lead1"].ToString(),
                    GridID = "Grid1"  // static id 
                });

                RowGrid1 = 25 * LoadWinchListGrid1.Count + 150;
            }



            //}


            //if (para == "Grid1")
            //{
            string qry11 = "select distinct a.winchid,b.Id,b.assignednumber from AssignRopeToWinch a  inner join MooringWinchDetail b on a.WinchId=b.Id   where a.IsActive=1 and a. WinchId  in (select winchid from MOUsedWinchTbl  where GridID='Grid2' and OperationID=" + OpdId + ")";
            SqlDataAdapter sda11 = new SqlDataAdapter(qry11, sc.con);
            DataTable datatbl11 = new DataTable();
            sda11.Fill(datatbl11);
            LoadWinchListGrid2.Clear();

            for (int i = 0; i < datatbl11.Rows.Count; i++)
            {
                LoadWinchListGrid2.Add(new WinchCheckClass()
                {
                    WinId = Convert.ToInt32(datatbl11.Rows[i]["Id"]),
                    WinchNo = datatbl11.Rows[i]["AssignedNumber"].ToString(),
                    Mark = true,
                    Lead = datatbl11.Rows[i]["Lead"].ToString(),
                    Lead1 = datatbl11.Rows[i]["Lead1"].ToString(),
                    GridID = "Grid1"  // static id 
                });

                RowGrid2 = 25 * LoadWinchListGrid2.Count + 150;
            }


            //}

            //if (para == "Grid2")
            //{
            string qry112 = "select distinct a.winchid,b.Id,b.assignednumber from AssignRopeToWinch a  inner join MooringWinchDetail b on a.WinchId=b.Id   where a.IsActive=1 and a. WinchId  in (select winchid from MOUsedWinchTbl  where GridID='Grid3' and OperationID=" + OpdId + ")";
            SqlDataAdapter sda112 = new SqlDataAdapter(qry112, sc.con);
            DataTable datatbl112 = new DataTable();
            sda112.Fill(datatbl112);
            LoadWinchListGrid3.Clear();

            for (int i = 0; i < datatbl112.Rows.Count; i++)
            {
                LoadWinchListGrid3.Add(new WinchCheckClass()
                {
                    WinId = Convert.ToInt32(datatbl112.Rows[i]["Id"]),
                    WinchNo = datatbl112.Rows[i]["AssignedNumber"].ToString(),
                    Mark = true,
                    Lead = datatbl112.Rows[i]["Lead"].ToString(),
                    Lead1 = datatbl112.Rows[i]["Lead1"].ToString(),
                    GridID = "Grid1"  // static id 
                });

                RowGrid3 = 25 * LoadWinchListGrid3.Count + 150;
            }

            //    
            //}


            //if (para == "Grid3")
            //{
            string qry1122 = "select distinct a.winchid,b.Id,b.assignednumber from AssignRopeToWinch a  inner join MooringWinchDetail b on a.WinchId=b.Id   where a.IsActive=1 and a. WinchId  in (select winchid from MOUsedWinchTbl  where GridID='Grid4' and OperationID=" + OpdId + ")";
            SqlDataAdapter sda1122 = new SqlDataAdapter(qry1122, sc.con);
            DataTable datatbl1122 = new DataTable();
            sda1122.Fill(datatbl1122);
            LoadWinchListGrid4.Clear();

            for (int i = 0; i < datatbl1122.Rows.Count; i++)
            {
                LoadWinchListGrid4.Add(new WinchCheckClass()
                {
                    WinId = Convert.ToInt32(datatbl1122.Rows[i]["Id"]),
                    WinchNo = datatbl1122.Rows[i]["AssignedNumber"].ToString(),
                    Mark = true,
                    Lead = datatbl1122.Rows[i]["Lead"].ToString(),
                    Lead1 = datatbl1122.Rows[i]["Lead1"].ToString(),
                    GridID = "Grid1"  // static id 
                });

                RowGrid4 = 25 * LoadWinchListGrid4.Count + 150;
            }
            //    
            //}


        }
        private void AddMethod(string para)
        {
            if (para == "Grid0")
            {
                var kkk = LoadWinchlist.Where(x => x.Mark == false).ToList();
                // LoadWinchListGrid1 = LoadWinchlist.Where(x => x.Mark == false).ToList();
                LoadWinchListGrid1.Clear();

                foreach (var item in kkk)
                {
                    LoadWinchListGrid1.Add(new WinchCheckClass()
                    {
                        WinId = Convert.ToInt32(item.WinId),
                        RopeId = Convert.ToInt32(item.RopeId),
                        WinchNo = item.WinchNo,
                        Location = item.Location,
                        Mark = Mark1All,
                        RopeTailMark = Mark1All,
                        IsEditable = item.IsEditable,
                        outboard1 = item.outboard1,
                        VisibilityCheck = item.VisibilityCheck,
                        //Tails = item.Tails,
                        Lead = item.Lead,
                        Lead1 = item.Lead1,
                        Tails = GetTailsOnWinches(item.WinId, Mark1All),
                        GridID = "Grid1"  // static id 
                    });
                }
                //  var asdkj = LoadWinchListGrid1.Count;
                RowGrid1 = 25 * LoadWinchListGrid1.Count + 150;
                BerthGrid = BerthGrid + RowGrid1 - 150;
            }


            if (para == "Grid1")
            {
                var kkk = LoadWinchListGrid1.Where(x => x.Mark == false).ToList();
                LoadWinchListGrid2.Clear();

                foreach (var item in kkk)
                {
                    LoadWinchListGrid2.Add(new WinchCheckClass()
                    {
                        WinId = Convert.ToInt32(item.WinId),
                        RopeId = Convert.ToInt32(item.RopeId),
                        WinchNo = item.WinchNo,
                        Location = item.Location,
                        Mark = Mark2All,
                        RopeTailMark = Mark2All,
                        IsEditable = item.IsEditable,
                        outboard1 = item.outboard1,
                        VisibilityCheck = item.VisibilityCheck,
                        //Tails = item.Tails,
                        Tails = GetTailsOnWinches(item.WinId, Mark2All),
                        Lead = item.Lead,
                        Lead1 = item.Lead1,
                        GridID = "Grid2"   // static id 
                    });
                }

                RowGrid2 = 25 * LoadWinchListGrid2.Count + 150;
                BerthGrid = BerthGrid + RowGrid2 - 150;
            }

            if (para == "Grid2")
            {
                var kkk = LoadWinchListGrid2.Where(x => x.Mark == false).ToList();
                LoadWinchListGrid3.Clear();

                foreach (var item in kkk)
                {
                    LoadWinchListGrid3.Add(new WinchCheckClass()
                    {
                        WinId = Convert.ToInt32(item.WinId),
                        RopeId = Convert.ToInt32(item.RopeId),
                        WinchNo = item.WinchNo,
                        Location = item.Location,
                        Mark = Mark3All,
                        IsEditable = item.IsEditable,
                        outboard1 = item.outboard1,
                        RopeTailMark = Mark3All,
                        VisibilityCheck = item.VisibilityCheck,
                        //Tails = item.Tails,
                        Tails = GetTailsOnWinches(item.WinId, Mark3All),
                        Lead = item.Lead,
                        Lead1 = item.Lead1,
                        GridID = "Grid3"  // static id 

                    });
                }

                RowGrid3 = 25 * LoadWinchListGrid3.Count + 150;
                BerthGrid = BerthGrid + RowGrid3 - 150;
            }


            if (para == "Grid3")
            {
                var kkk = LoadWinchListGrid3.Where(x => x.Mark == false).ToList();
                LoadWinchListGrid4.Clear();

                foreach (var item in kkk)
                {
                    LoadWinchListGrid4.Add(new WinchCheckClass()
                    {
                        WinId = Convert.ToInt32(item.WinId),
                        RopeId = Convert.ToInt32(item.RopeId),
                        WinchNo = item.WinchNo,
                        Location = item.Location,
                        Mark = Mark4All,
                        IsEditable = item.IsEditable,
                        outboard1 = item.outboard1,
                        RopeTailMark = Mark4All,
                        VisibilityCheck = item.VisibilityCheck,
                        //Tails = item.Tails,
                        Tails = GetTailsOnWinches(item.WinId, Mark4All),
                        Lead = item.Lead,
                        Lead1 = item.Lead1,
                        GridID = "Grid4"    // static id 

                    });
                }
                RowGrid4 = 25 * LoadWinchListGrid4.Count + 150;
                BerthGrid = BerthGrid + RowGrid4 - 150;
            }


        }

        private void RemoveMethod(string para)
        {
            if (para == "Grid1")
            {
                BerthGrid = BerthGrid - RowGrid1 + 150;
                LoadWinchListGrid1.Clear();
                OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid1"));
                RowGrid1 = 0;
            }


            if (para == "Grid2")
            {
                BerthGrid = BerthGrid - RowGrid2 + 150;
                LoadWinchListGrid2.Clear();
                OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid2"));
                RowGrid2 = 0;
            }

            if (para == "Grid3")
            {
                BerthGrid = BerthGrid - RowGrid3 + 150;
                LoadWinchListGrid3.Clear();
                OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid3"));
                RowGrid3 = 0;
            }


            if (para == "Grid4")
            {
                BerthGrid = BerthGrid - RowGrid4 + 150;
                LoadWinchListGrid4.Clear();
                OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid4"));
                RowGrid4 = 0;
            }


        }


        public static MOperationBirthDetail mOperationBirths = new MOperationBirthDetail();

        public MOperationBirthDetail MOperationBirth
        {
            get { return mOperationBirths; }
            set
            {
                mOperationBirths = value;
                RaisePropertyChanged("MOperationBirth");

            }
        }

        private string _BirthTypeS;

        public string BirthTypeS
        {
            get { return _BirthTypeS; }
            set
            {
                _BirthTypeS = value;
                // MOperationBirth.BirthType = _BirthTypeS;
                RaisePropertyChanged("BirthTypeS");
            }
        }

        //MooringTypeS

        private string _MooringTypeS;

        public string MooringTypeS
        {
            get { return _MooringTypeS; }
            set
            {
                _MooringTypeS = value;
                // MOperationBirth.MooringType = _MooringTypeS;
                RaisePropertyChanged("MooringTypeS");
            }
        }

        private string _BerthSideS;

        public string BerthSideS
        {
            get { return _BerthSideS; }
            set
            {
                _BerthSideS = value;
                // MOperationBirth.BerthSide = _BerthSideS;
                RaisePropertyChanged("BerthSideS");
            }
        }

        private string _VesselConditionS;

        public string VesselConditionS
        {
            get { return _VesselConditionS; }
            set
            {
                _VesselConditionS = value;
                // MOperationBirth.VesselCondition = _VesselConditionS;
                RaisePropertyChanged("VesselConditionS");
            }
        }
        private string _ShipAsscessS;

        public string ShipAsscessS
        {
            get { return _VesselConditionS; }
            set
            {
                _ShipAsscessS = value;
                MOperationBirth.ShipAccess = _ShipAsscessS;
                RaisePropertyChanged("ShipAsscessS");
            }
        }

        private string visible = "Collapsed";

        public string Visibles
        {
            get { return visible; }
            set { visible = value; }
        }


        private static ObservableCollection<WinchCheckClass> loadWinchlist = new ObservableCollection<WinchCheckClass>();

        public ObservableCollection<WinchCheckClass> LoadWinchlist
        {
            get { return loadWinchlist; }
            set
            {
                loadWinchlist = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchlist"));
            }
        }


        private static ObservableCollection<WinchCheckClass> loadWinchlistGrid1 = new ObservableCollection<WinchCheckClass>();

        public ObservableCollection<WinchCheckClass> LoadWinchListGrid1
        {
            get { return loadWinchlistGrid1; }
            set
            {
                loadWinchlistGrid1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid1"));
            }
        }


        private static ObservableCollection<WinchCheckClass> loadWinchlistGrid2 = new ObservableCollection<WinchCheckClass>();

        public ObservableCollection<WinchCheckClass> LoadWinchListGrid2
        {
            get { return loadWinchlistGrid2; }
            set
            {
                loadWinchlistGrid2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid2"));
            }
        }

        private static ObservableCollection<WinchCheckClass> loadWinchlistGrid3 = new ObservableCollection<WinchCheckClass>();

        public ObservableCollection<WinchCheckClass> LoadWinchListGrid3
        {
            get { return loadWinchlistGrid3; }
            set
            {
                loadWinchlistGrid3 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid3"));
            }
        }


        private static ObservableCollection<WinchCheckClass> loadWinchlistGrid4 = new ObservableCollection<WinchCheckClass>();

        public ObservableCollection<WinchCheckClass> LoadWinchListGrid4
        {
            get { return loadWinchlistGrid4; }
            set
            {
                loadWinchlistGrid4 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid4"));
            }
        }

        public static ObservableCollection<string> subDates = new ObservableCollection<string>();
        public ObservableCollection<string> SubDates
        {
            get
            {

                return subDates;
            }
            set
            {
                subDates = value;
                //RaisePropertyChanged("SubDates");
                OnPropertyChanged(new PropertyChangedEventArgs("SubDates"));

            }
        }

        public static string dpSubFastGrid1;

        public string DpSubFastGrid1
        {
            get { return dpSubFastGrid1; }
            set
            {
                dpSubFastGrid1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid1"));
            }
        }

        public static string dpSubCastGrid1;

        public string DpSubCastGrid1
        {
            get { return dpSubCastGrid1; }
            set
            {
                dpSubCastGrid1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid1"));
            }
        }


        public static string dpSubFastGrid2;

        public string DpSubFastGrid2
        {
            get { return dpSubFastGrid2; }
            set
            {
                dpSubFastGrid2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid2"));
            }
        }

        public static string dpSubCastGrid2;

        public string DpSubCastGrid2
        {
            get { return dpSubCastGrid2; }
            set
            {
                dpSubCastGrid2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid2"));
            }
        }

        public static string dpSubFastGrid3;

        public string DpSubFastGrid3
        {
            get { return dpSubFastGrid3; }
            set
            {
                dpSubFastGrid3 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid3"));
            }
        }

        public static string dpSubCastGrid3;

        public string DpSubCastGrid3
        {
            get { return dpSubCastGrid3; }
            set
            {
                dpSubCastGrid3 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid3"));
            }
        }


        public static string dpSubFastGrid4;

        public string DpSubFastGrid4
        {
            get { return dpSubFastGrid4; }
            set
            {
                dpSubFastGrid4 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid4"));
            }
        }

        public static string dpSubCastGrid4;

        public string DpSubCastGrid4
        {
            get { return dpSubCastGrid4; }
            set
            {
                dpSubCastGrid4 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid4"));
            }
        }

        public static string dpSubFastGrid1_Hours;
        public string DpSubFastGrid1_Hours
        {
            get { return dpSubFastGrid1_Hours; }
            set
            {
                dpSubFastGrid1_Hours = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid1_Hours"));
            }
        }

        public static string dpSubFastGrid2_Hours;
        public string DpSubFastGrid2_Hours
        {
            get { return dpSubFastGrid2_Hours; }
            set
            {
                dpSubFastGrid2_Hours = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid2_Hours"));
            }
        }


        public static string dpSubFastGrid3_Hours;
        public string DpSubFastGrid3_Hours
        {
            get { return dpSubFastGrid3_Hours; }
            set
            {
                dpSubFastGrid3_Hours = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid3_Hours"));
            }
        }

        public static string dpSubFastGrid4_Hours;
        public string DpSubFastGrid4_Hours
        {
            get { return dpSubFastGrid4_Hours; }
            set
            {
                dpSubFastGrid4_Hours = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid4_Hours"));
            }
        }

        public static string dpSubFastGrid1_Mint;
        public string DpSubFastGrid1_Mint
        {
            get { return dpSubFastGrid1_Mint; }
            set
            {
                dpSubFastGrid1_Mint = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid1_Mint"));
            }
        }

        public static string dpSubFastGrid2_Mint;
        public string DpSubFastGrid2_Mint
        {
            get { return dpSubFastGrid2_Mint; }
            set
            {
                dpSubFastGrid2_Mint = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid2_Mint"));
            }
        }

        public static string dpSubFastGrid3_Mint;
        public string DpSubFastGrid3_Mint
        {
            get { return dpSubFastGrid3_Mint; }
            set
            {
                dpSubFastGrid3_Mint = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid3_Mint"));
            }
        }

        public static string dpSubFastGrid4_Mint;
        public string DpSubFastGrid4_Mint
        {
            get { return dpSubFastGrid4_Mint; }
            set
            {
                dpSubFastGrid4_Mint = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid4_Mint"));
            }
        }


        //=====================


        public static string dpSubCastGrid1_Hours;
        public string DpSubCastGrid1_Hours
        {
            get { return dpSubCastGrid1_Hours; }
            set
            {
                dpSubCastGrid1_Hours = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid1_Hours"));
            }
        }

        public static string dpSubCastGrid2_Hours;
        public string DpSubCastGrid2_Hours
        {
            get { return dpSubCastGrid2_Hours; }
            set
            {
                dpSubCastGrid2_Hours = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid2_Hours"));
            }
        }


        public static string dpSubCastGrid3_Hours;
        public string DpSubCastGrid3_Hours
        {
            get { return dpSubCastGrid3_Hours; }
            set
            {
                dpSubCastGrid3_Hours = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid3_Hours"));
            }
        }

        public static string dpSubCastGrid4_Hours;
        public string DpSubCastGrid4_Hours
        {
            get { return dpSubCastGrid4_Hours; }
            set
            {
                dpSubCastGrid4_Hours = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid4_Hours"));
            }
        }

        public static string dpSubCastGrid1_Mint;
        public string DpSubCastGrid1_Mint
        {
            get { return dpSubCastGrid1_Mint; }
            set
            {
                dpSubCastGrid1_Mint = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid1_Mint"));
            }
        }

        public static string dpSubCastGrid2_Mint;
        public string DpSubCastGrid2_Mint
        {
            get { return dpSubCastGrid2_Mint; }
            set
            {
                dpSubCastGrid2_Mint = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid2_Mint"));
            }
        }

        public static string dpSubCastGrid3_Mint;
        public string DpSubCastGrid3_Mint
        {
            get { return dpSubCastGrid3_Mint; }
            set
            {
                dpSubCastGrid3_Mint = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid3_Mint"));
            }
        }

        public static string dpSubCastGrid4_Mint;
        public string DpSubCastGrid4_Mint
        {
            get { return dpSubCastGrid4_Mint; }
            set
            {
                dpSubCastGrid4_Mint = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubCastGrid4_Mint"));
            }
        }



        private static WinchCheckClass _WinchTail = new WinchCheckClass();

        public WinchCheckClass WinchTail
        {
            get
            {
                return _WinchTail;
            }
            set
            {
                _WinchTail = value;
                if (_WinchTail.Mark == true)
                {
                    _WinchTail.RopeTailMark = true;
                }
                else
                {
                    _WinchTail.RopeTailMark = false;
                }

                if (_WinchTail.RopeTailMark == true)
                {
                    _WinchTail.Mark = true;
                }
                else
                {
                    _WinchTail.RopeTailMark = false;
                }
                RaisePropertyChanged("WinchTail");
            }
        }

        public void BindWinchList()
        {
            try
            {

                // string qry = "select a.winchid,b.Id,b.assignednumber from AssignRopeToWinch a  inner join MooringWinchDetail b on a.WinchId=b.Id where a.IsActive=1 and a.RopeTail=0";

                //string qry = "GetAssignedRopeTail";
                //string qry = "MOperationRopeTail";
                string qry = "MOperationRopeTail2";
                SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable datatbl = new DataTable();
                sda.Fill(datatbl);
                LoadWinchlist.Clear();
                //var ranklist = new ObservableCollection<MooringRopeInspectionClass>();

                // var abc = sc.MooringRopeInspectionTbl.Where(x=> x.InspectDate == dd).Select(s=> new { s.InspectBy,s.InspectDate}).Distinct().ToList();
                //var abc = sc.MooringRopeInspectionTbl.ToList();
                for (int i = 0; i < datatbl.Rows.Count; i++)
                {
                    //_WinchTail.WinId = Convert.ToInt32(datatbl.Rows[i]["Id"]);
                    //    _WinchTail.WinchNo = datatbl.Rows[i]["AssignedNumber"].ToString();
                    //_WinchTail.Mark = true;
                    //_WinchTail.RopeTailMark = true;
                    //    _WinchTail.VisibilityCheck = datatbl.Rows[i]["VisibilityCheck"].ToString();
                    //_WinchTail.Lead = "Headline";
                    //    _WinchTail.GridID = "Grid0";  // static id 


                    //LoadWinchlist.Add(_WinchTail);  RopeTailmarkAll

                    LoadWinchlist.Add(new WinchCheckClass()
                    {
                        //WinId = Convert.ToInt32(datatbl.Rows[i]["Id"]),
                        WinId = Convert.ToInt32(datatbl.Rows[i]["winchid"]),
                        RopeId = Convert.ToInt32(datatbl.Rows[i]["RopeId"]),
                        Location = Convert.ToString(datatbl.Rows[i]["location"] == DBNull.Value ? "" : datatbl.Rows[i]["location"]).ToString(),
                        WinchNo = datatbl.Rows[i]["AssignedNumber"].ToString(),
                        Mark = MarkAll,
                        RopeTailMark = MarkAll,
                        VisibilityCheck = datatbl.Rows[i]["VisibilityCheck"].ToString(),
                        IsEditable = datatbl.Rows[i]["IsEditable"].ToString(),
                        outboard1 = Convert.ToString(datatbl.Rows[i]["outboard1"] == DBNull.Value ? "" : datatbl.Rows[i]["outboard1"]),
                        //VisibilityCheck = "Hidden",
                        //Lead = "Headline",
                        Lead = string.IsNullOrEmpty(datatbl.Rows[i]["Lead"].ToString()) == true ? "--Select--" : datatbl.Rows[i]["Lead"].ToString(),
                        Lead1 = "Direct",
                        GridID = "Grid0",  // static id 
                        Tails = GetTailsOnWinches(Convert.ToInt32(datatbl.Rows[i]["winchid"]), MarkAll)

                    });
                }
                SubHit = false;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchlist"));


            }
            catch (Exception ex) { }
        }




        private bool _markAll = true;

        public bool MarkAll
        {
            get { return _markAll; }
            set
            {
                _markAll = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MarkAll"));
                if (_markAll == false & SubHit == true)
                {

                }
                else
                {
                    // MainHit = true; SubHit = false;
                    BindWinchList();
                }

            }
        }

        private bool _mark1All = true;
        public bool Mark1All
        {
            get { return _mark1All; }
            set
            {
                _mark1All = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Mark1All"));
                if (_mark1All == false & SubHit1 == true)
                {

                }
                else
                {
                    BindWinchList1();
                }

            }
        }

        private bool _mark2All = true;
        public bool Mark2All
        {
            get { return _mark2All; }
            set
            {
                _mark2All = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Mark2All"));
                if (_mark2All == false & SubHit2 == true)
                {

                }
                else
                {
                    BindWinchList2();
                }

            }
        }

        private bool _mark3All = true;
        public bool Mark3All
        {
            get { return _mark3All; }
            set
            {
                _mark3All = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Mark3All"));
                if (_mark3All == false & SubHit3 == true)
                {

                }
                else
                {
                    BindWinchList3();
                }

            }
        }

        private bool _mark4All = true;
        public bool Mark4All
        {
            get { return _mark4All; }
            set
            {
                _mark4All = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Mark4All"));
                if (_mark4All == false & SubHit4 == true)
                {

                }
                else
                {
                    BindWinchList4();
                }

            }
        }


        private bool _RopeTailmarkAll = true;

        public bool RopeTailmarkAll
        {
            get { return _RopeTailmarkAll; }
            set
            {
                _RopeTailmarkAll = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RopeTailmarkAll"));
                // BindWinchList();

            }
        }

        private void EditBindWinchList(int OpdId)
        {

            //OpdId = 1;
            //string qry = "select a.operationid,a.GridID,a.WinchId,b.assignednumber from MOUsedWinchTbl a inner join MooringWinchDetail b on a.WinchId=b.Id  where a.operationID=" + OpdId + " and a.gridId='Grid0'";
            string qry = "select a.operationid,a.GridID,a.WinchId,b.assignednumber from MOUsedWinchTbl a inner join MooringWinchDetail b on a.WinchId=b.Id  where a.gridid!='Grid0' and a.operationID=" + OpdId + "";

            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
            DataTable datatbl = new DataTable();
            sda.Fill(datatbl);


            for (int i = 0; i < datatbl.Rows.Count; i++)
            {
                LoadWinchlist.Add(new WinchCheckClass()
                {
                    WinId = Convert.ToInt32(datatbl.Rows[i]["WinchId"]),
                    WinchNo = datatbl.Rows[i]["AssignedNumber"].ToString(),
                    //Mark = true,
                    Lead = datatbl.Rows[i]["Lead"].ToString(),
                    Lead1 = datatbl.Rows[i]["Lead1"].ToString(),
                    GridID = "Grid0"  // static id 
                });
            }

            //string qry1 = "select * from MooringWinchDetail where Id not in (select winchid from MOUsedWinchTbl where GridID='Grid0' and OperationID=" + OpdId + ")";

            string qry1 = "select distinct a.winchid,b.Id,b.assignednumber from AssignRopeToWinch a  inner join MooringWinchDetail b on a.WinchId=b.Id   where a.IsActive=1 and a. WinchId  in (select winchid from MOUsedWinchTbl  where GridID='Grid1' and OperationID=" + OpdId + ")";
            SqlDataAdapter sda1 = new SqlDataAdapter(qry1, sc.con);
            DataTable datatbl1 = new DataTable();
            sda1.Fill(datatbl1);


            for (int i = 0; i < datatbl1.Rows.Count; i++)
            {
                LoadWinchListGrid1.Add(new WinchCheckClass()
                {
                    WinId = Convert.ToInt32(datatbl1.Rows[i]["Id"]),
                    WinchNo = datatbl1.Rows[i]["AssignedNumber"].ToString(),
                    Mark = true,
                    Lead = datatbl.Rows[i]["Lead"].ToString(),
                    Lead1 = datatbl.Rows[i]["Lead1"].ToString(),
                    GridID = "Grid0"  // static id 
                });
            }


            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchListGrid1"));
            OnPropertyChanged(new PropertyChangedEventArgs("LoadWinchlist"));


        }
        private static ObservableCollection<string> hours = new ObservableCollection<string>();
        public ObservableCollection<string> Hours
        {
            get
            {

                return hours;
            }
            set
            {
                hours = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Hours"));
            }
        }

        private static ObservableCollection<string> mints = new ObservableCollection<string>();
        public ObservableCollection<string> Mints
        {
            get
            {
                return mints;
            }
            set
            {
                mints = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Mints"));
            }
        }


        private static string shours = "00";
        public string SHours
        {
            get
            {


                return shours;

            }

            set
            {

                shours = value;
                if (shours != null)
                {

                    HoursSTART.Clear();
                    for (int i = Convert.ToInt32(shours); i < 24; i++)
                    {
                        HoursSTART.Add(i < 10 ? "0" + i.ToString() : i.ToString());

                    }
                    DpSubFastGrid1_Hours = DpSubFastGrid2_Hours = DpSubFastGrid3_Hours = DpSubFastGrid4_Hours = shours;

                }
                OnPropertyChanged(new PropertyChangedEventArgs("SHours"));
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid1_Hours"));
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid2_Hours"));
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid3_Hours"));
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid4_Hours"));
            }
        }

        private static string smints = "00";
        public string SMints
        {
            get
            {

                return smints;
            }

            set
            {
                smints = value;
                if (smints != null)
                {
                    SMints11 = smints;
                    MintsSTART.Clear();
                    for (int i = Convert.ToInt32(smints); i < 60; i++)
                    {
                        MintsSTART.Add(i < 10 ? "0" + i.ToString() : i.ToString());
                    }

                    DpSubFastGrid1_Mint = DpSubFastGrid2_Mint = DpSubFastGrid3_Mint = DpSubFastGrid4_Mint = smints;
                }
                else
                {
                    smints = SMints11;
                }
                OnPropertyChanged(new PropertyChangedEventArgs("SMints"));
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid1_Mint"));
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid2_Mint"));
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid3_Mint"));
                OnPropertyChanged(new PropertyChangedEventArgs("DpSubFastGrid4_Mint"));
            }
        }

        private static string shours2 = "00";
        public string SHours2
        {
            get
            {

                return shours2;
            }

            set
            {

                shours2 = value;
                if (shours2 != null)
                {
                    DpSubCastGrid1_Hours = DpSubCastGrid2_Hours = DpSubCastGrid3_Hours = DpSubCastGrid4_Hours = shours2;
                    HoursEND.Clear();
                    //for (int i = Convert.ToInt32(shours2); i < 24; i++)
                    for (int i = 0; i <= Convert.ToInt32(shours2); i++)
                    {

                        HoursEND.Add(i < 10 ? "0" + i.ToString() : i.ToString());
                    }
                }
                OnPropertyChanged(new PropertyChangedEventArgs("SHours2"));

            }
        }

        public static string SMints11 { get; set; }

        private static string smints2 = "00";
        public string SMints2
        {
            get
            {

                return smints2;
            }

            set
            {
                smints2 = value;
                if (smints2 != null)
                {
                    DpSubCastGrid1_Mint = DpSubCastGrid2_Mint = DpSubCastGrid3_Mint = DpSubCastGrid4_Mint = smints2;
                    MintsEND.Clear();
                    //for (int i = Convert.ToInt32(smints2); i < 60; i++)
                    for (int i = 0; i <= Convert.ToInt32(smints2); i++)
                    {
                        MintsEND.Add(i < 10 ? "0" + i.ToString() : i.ToString());
                    }
                }
                OnPropertyChanged(new PropertyChangedEventArgs("SMints2"));

            }
        }


        private static ObservableCollection<string> hoursStart = new ObservableCollection<string>();
        public ObservableCollection<string> HoursSTART
        {
            get
            {

                return hoursStart;
            }
            set
            {
                hoursStart = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HoursSTART"));
            }
        }

        private static ObservableCollection<string> mintsStart = new ObservableCollection<string>();
        public ObservableCollection<string> MintsSTART
        {
            get
            {
                return mintsStart;
            }
            set
            {
                mintsStart = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MintsSTART"));
            }
        }

        private static ObservableCollection<string> hoursEnd = new ObservableCollection<string>();
        public ObservableCollection<string> HoursEND
        {
            get
            {

                return hoursEnd;
            }
            set
            {
                hoursEnd = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HoursEND"));
            }
        }

        private static ObservableCollection<string> mintsEnd = new ObservableCollection<string>();
        public ObservableCollection<string> MintsEND
        {
            get
            {
                return mintsEnd;
            }
            set
            {
                mintsEnd = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MintsEND"));
            }
        }


        public static DateTime _FastDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        public DateTime FastDate
        {
            get
            {
                if (_FastDate == null)
                {
                    _FastDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }

                return _FastDate;
            }
            set
            {
                _FastDate = value;
                DpSubFastGrid1 = DpSubFastGrid2 = DpSubFastGrid3 = DpSubFastGrid4 = _FastDate.ToShortDateString();
                MOperationBirth.FastDatetime = _FastDate;
                OnPropertyChanged(new PropertyChangedEventArgs("FastDate"));
            }
        }

        public static DateTime _CastDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        public DateTime CastDate
        {
            get
            {
                if (_CastDate == null)
                {
                    _CastDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }

                return _CastDate;
            }
            set
            {
                _CastDate = value;
                DpSubCastGrid1 = DpSubCastGrid2 = DpSubCastGrid3 = DpSubCastGrid4 = _CastDate.ToShortDateString();
                MOperationBirth.CastDatetime = _CastDate;
                OnPropertyChanged(new PropertyChangedEventArgs("CastDate"));
            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }


    public class TailOption
    {
        public int Id { get; set; } //Ropetail id.
        public string Name { get; set; }  //Unique ID.
        public bool Selected { get; set; }


    }


    public class WinchCheckClass
    {
        public WinchCheckClass()
        {
            Tails = new List<TailOption>();
        }
        public int WinId { get; set; }

        public int RopeId { get; set; }
        public string WinchNo { get; set; }
        //public static bool Mark { get; set; } = RopeTailMark == true ? true : true;

        //public static bool RopeTailMark { get; set; } = Mark == true ? true : false;

        public bool Mark { get; set; }

        public bool RopeTailMark { get; set; }

        public string Lead { get; set; }

        // public string Leadlast { get; set; }

        public string Lead1 { get; set; }

        public string Location { get; set; }

        public string outboard1 { get; set; }
        public bool outboard { get; set; }
        public string VisibilityCheck { get; set; }

        public string IsEditable { get; set; }
        public string GridID { get; set; }

        public DateTime StartDatetime { get; set; }
        public DateTime EndDatetime { get; set; }

        public List<TailOption> Tails { get; set; }

    }
}
