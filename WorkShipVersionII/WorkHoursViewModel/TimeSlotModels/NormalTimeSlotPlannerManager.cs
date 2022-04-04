using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using DataBuildingLayer;

namespace WorkShipVersionII.WorkHoursViewModel.TimeSlotModels
{
    public class NormalTimeSlotPlannerManager : CommanConditionsTimeSlot, ITimeSlotManagerPlanner
    {
        private readonly ShipmentContaxt sc;
        public NormalTimeSlotPlannerManager()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
        }

        public override Tuple<DateTime, string, string> CheckIDLBack(DateTime dates, string IDLs, List<InternationalClass> CheckIDL)
        {
            return base.CheckIDLBack(dates, IDLs, CheckIDL);
        }
        public override Tuple<DateTime, string, string> CheckIDLNext(DateTime dates, string IDLs, List<InternationalClass> CheckIDL)
        {
            return base.CheckIDLNext(dates, IDLs, CheckIDL);
        }
        public override int CheckRetard(string Dateid, DateTime Dt11, List<RetardtblClass> CheckRetar)
        {
            return base.CheckRetard(Dateid, Dt11, CheckRetar);
        }
        public override void SaveMethod1Planner(WorkHoursPlannerClass ob)
        {
            base.SaveMethod1Planner(ob);
        }
        public override void SaveMethod2Planner(WorkHoursPlannerClass ob)
        {
            base.SaveMethod2Planner(ob);
        }
        public override void UpdatePlanner(WorkHoursPlannerClass ob)
        {
            base.UpdatePlanner(ob);
        }

        public override string Morethan1hrsNormal(string label42)
        {
            return base.Morethan1hrsNormal(label42);
        }

        public override bool OPAStartStopTime(string startOPAdateid, OPAStartStopClass sdaopastatr)
        {
            return base.OPAStartStopTime(startOPAdateid, sdaopastatr);
        }

        public override void GetbackhoursValuesPlanner(WorkHoursPlannerClass obj, List<WorkHoursClass> WorkHoursList, List<InternationalClass> CheckIDL)
        {
            base.GetbackhoursValuesPlanner(obj, WorkHoursList, CheckIDL);
        }


        public void TimeSlotPageLoadMethod(WorkHoursPlannerClass obj, List<WorkHoursClass> WorkHoursList, CrewDetailClass CrewDetailList, Grid DynamicGrid, DefineRulesClass DefineRules, string Opa90Rule, List<InternationalClass> CheckIDL, DateTime? MaxFutureDate)
        {
            try
            {
                List<Border> DynamicGridBorders = new List<Border>();
                if (obj.TimeSlotGrid1 != null)
                    DynamicGridBorders = DynamicGrid.Children.OfType<Border>().ToList();


                obj.NC1 = null;
                obj.NC2 = null;
                obj.NC3 = null;
                obj.NC4 = null;
                obj.NC5 = null; //F
                obj.NC6 = null; //F
                obj.NC7 = null; //F
                obj.NC8 = null; //ncc1  F
                obj.NC9 = null; //mnc1
                obj.NC10 = null; //mnc2
                obj.NC11 = null; //mnc3
                obj.NC12 = null; //mnc4
                obj.NC13 = null; //mnc5
                obj.NC14 = null; //mnc6
                obj.NC15 = null; //opnc1
                obj.NC16 = null; //opnc1
                obj.NC17 = null; //mnc7
                obj.NC18 = null; //mnc8
                obj.YNC1 = null;
                obj.YNC2 = null;
                obj.YNC3 = null;
                obj.YNC4 = null;
                obj.YNC5 = null;
                obj.YNC6 = null;
                obj.YNC7 = null;
                obj.YNC8 = null;

                obj.opayoung = false;
                obj.RuleNames = "Normal";
                string label42 = string.Empty;

                label42 = obj.hrs;

                label42 = Morethan1hrsNormal(label42);
                var checkusers = WorkHoursList.Select(s => new { s.UserName, s.dates, s.hrs, s.Colors, s.WRID, s.Cellcount }).ToList();

                string lbl1 = string.Empty;
                int b = 0;
                double TotHours = 24.0;

                string[] s2 = null;
                //s2 = label42.Substring(0, label42.Length - 1).Split(',');
                string label42bmNewVal = checkbackhours(label42, obj.dates, obj.Cellcount, obj.IDL, WorkHoursList, CheckIDL);
                s2 = label42bmNewVal.Substring(0, label42bmNewVal.Length - 1).Split(',');// chanhed from label42 to label42bmNew 


                //var array2 = Array.FindAll(s2,
                //element => element == "1");
                //int pos = Array.IndexOf(s2, "1");
                //bool exists = s2.Contains("1");
                ////....................

                //string[] yourArray = { "A", "B", "C", "D", };
                //var foundedString = Array.FindAll(yourArray, str => str.ToLower().Equals("b"));


                string lbl1main = "";
                int intindex = 0;
                int tblcount = s2.Length;

                for (int s = 0; s < tblcount; s++)
                {
                    //if (s2[s] == "1")
                    //{
                    //    intindex = s;

                    //    lbl1main += s.ToString() + ",";
                    //    if (total71main == 0)
                    //    {
                    //        total71main = s;
                    //        b = s;
                    //        lbl1 += b.ToString() + ",";
                    //        total71main = s + 1;

                    //    }

                    //}
                    //else if (s2[s] == "0")
                    //{
                    //    if (total71main != 0)
                    //    {
                    //        lbl1main1 += intindex.ToString() + ",";
                    //        intindex = 0;
                    //        total71main = 0;
                    //    }

                    //}
                    //if (s == s2.Length - 1)
                    //{
                    //    if (total71main != 0)
                    //    {
                    //        lbl1main1 += intindex.ToString() + ",";
                    //        intindex = 0;
                    //        total71main = 0;
                    //    }
                    //}

                    if (s2[s] == "1")
                    {
                        intindex = s;

                        lbl1main += s.ToString() + ",";
                        lbl1 += s.ToString() + ",";

                    }
                    else if (s2[s] == "2")
                    {
                        intindex = s;

                        lbl1main += s.ToString() + ",";
                        lbl1 += s.ToString() + ",";

                    }


                }

                string[] nw = null;
                nw = lbl1main.Split(',');
                int result = nw.Count() - 1;
                double result1 = result / 2.0;
                string txtWH = "0";
                string txtRH = "0";
                txtWH = result1.ToString();
                txtRH = (TotHours - result1).ToString();

                //_CommonData1.TotalHours = Convert.ToDecimal(txtWH);
                // _CommonData1.RestHours = Convert.ToDecimal(txtRH);
                // _CommonData1.RestHour7day = Convert.ToDecimal(168);
                //OnPropertyChanged(new PropertyChangedEventArgs("CommonData1"));
                obj.TotalHours = Convert.ToDecimal(txtWH);
                obj.RestHours = Convert.ToDecimal(txtRH);
                //AddWorkHours = obj;
                //OnPropertyChanged(new PropertyChangedEventArgs("AddWorkHours"));

                string[] newhr = null;
                newhr = label42.TrimEnd(',').Split(',');

                string[] today = null;
                today = lbl1.TrimEnd(',').Split(',');




                if (!string.IsNullOrEmpty(lbl1))
                {
                    var aa = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                    string colorValue1 = ((SolidColorBrush)aa).Color.ToString();

                    foreach (Border item in DynamicGridBorders)
                    {
                        var ss1 = item.Background;

                        if (((SolidColorBrush)ss1).Color.ToString() == new SolidColorBrush(Colors.Red).ToString())
                        {
                            item.Background = aa;
                        }

                    }


                    //Get the data for one day

                    string[] conc = null;
                    int cellcouns = 0;
                    int iDatebm = 0;
                    string IDLS = obj.IDL;
                    string label42OneDay = string.Empty;
                    DateTime dates = obj.dates;
                    while (iDatebm < 4)
                    {

                        var checkIDL = CheckIDLBack(dates, IDLS, CheckIDL);
                        dates = checkIDL.Item1;
                        string WRID = checkIDL.Item2;
                        IDLS = checkIDL.Item3;

                        var checkuser = checkusers.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
                        if (checkuser != null)
                        {
                            int countbms = checkuser.Cellcount;
                            if (countbms + cellcouns <= 48)
                            {
                                cellcouns += checkuser.Cellcount;

                                string startEndbbs = checkuser.hrs;
                                label42OneDay = startEndbbs + label42OneDay;
                            }
                            else
                            {
                                int countbms1 = checkuser.Cellcount;
                                int finalCellcount = 48 - cellcouns;
                                string startEndbbs = checkuser.hrs;
                                string[] st22bbs = startEndbbs.TrimEnd(',').Split(',');

                                st22bbs = st22bbs.Skip(countbms1 - finalCellcount).Select(i => i.ToString()).ToArray();

                                startEndbbs = string.Join(",", st22bbs);
                                startEndbbs = startEndbbs + ",";
                                label42OneDay = startEndbbs + label42OneDay;

                                cellcouns += checkuser.Cellcount;

                            }
                        }
                        else
                        {
                            cellcouns += 48;
                            string startEndbbs = string.Empty;
                            for (int i = 0; i < 48; i++)
                            {
                                startEndbbs += "0" + ",";
                            }
                            label42OneDay = startEndbbs + label42OneDay;
                        }

                        if (cellcouns >= 48)
                            break;

                        iDatebm++;
                    }


                    string[] st22 = null;
                    st22 = label42OneDay.TrimEnd(',').Split(',');

                    conc = st22.Concat(newhr).ToArray();


                    //...New Implementation for an hour....
                    string bmschange7bm = string.Join(",", conc);
                    bmschange7bm = bmschange7bm + ',';
                    string bmschange77bm = Morethan1hrsNormal(bmschange7bm);

                    conc = bmschange77bm.Split(',');
                    //...End New Implementation for an hour....

                    var ab10rest = new ArrayList();
                    ab10rest.Clear();
                    ab10rest.Add(24.0);

                    var ab10rest1 = new ArrayList();
                    ab10rest1.Clear();
                    ab10rest1.Add(168);


                    //Get the data for seven days

                    string[] st4bms7 = null;

                    cellcouns = 0;
                    iDatebm = 0;
                    IDLS = obj.IDL;
                    label42OneDay = string.Empty;
                    dates = obj.dates;
                    while (iDatebm < 15)
                    {

                        var checkIDL = CheckIDLBack(dates, IDLS, CheckIDL);
                        dates = checkIDL.Item1;
                        string WRID = checkIDL.Item2;
                        IDLS = checkIDL.Item3;

                        var checkuser = checkusers.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
                        if (checkuser != null)
                        {
                            int countbms = checkuser.Cellcount;
                            if (countbms + cellcouns <= 336)
                            {
                                cellcouns += checkuser.Cellcount;

                                string startEndbbs = checkuser.hrs;
                                label42OneDay = startEndbbs + label42OneDay;
                            }
                            else
                            {
                                int countbms1 = checkuser.Cellcount;
                                int finalCellcount = 336 - cellcouns;
                                string startEndbbs = checkuser.hrs;
                                string[] st22bbs = startEndbbs.TrimEnd(',').Split(',');

                                st22bbs = st22bbs.Skip(countbms1 - finalCellcount).Select(i => i.ToString()).ToArray();

                                startEndbbs = string.Join(",", st22bbs);
                                startEndbbs = startEndbbs + ",";
                                label42OneDay = startEndbbs + label42OneDay;

                                cellcouns += checkuser.Cellcount;

                            }
                        }
                        else
                        {
                            cellcouns += 48;
                            string startEndbbs = string.Empty;
                            for (int i = 0; i < 48; i++)
                            {
                                startEndbbs += "0" + ",";
                            }
                            label42OneDay = startEndbbs + label42OneDay;

                        }

                        if (cellcouns >= 336)
                            break;

                        iDatebm++;
                    }

                    string[] stbms6 = null;
                    stbms6 = label42OneDay.TrimEnd(',').Split(',');

                    st4bms7 = stbms6.Concat(newhr).ToArray();


                    //...New Implementation for an hour....
                    string change77 = string.Join(",", st4bms7);
                    change77 = change77 + ',';
                    string change777 = Morethan1hrsNormal(change77);
                    st4bms7 = change777.Split(',');
                    //...End New Implementation for an hour...


                    string date12311 = "";
                    string date12411 = "";
                    DateTime datebms11 = Convert.ToDateTime(MaxFutureDate);
                    date12311 = datebms11.ToShortDateString();
                    date12411 = obj.dates.ToShortDateString();

                    //..........for future next days...................
                    string[] st4bmsF1 = null;

                    if (Convert.ToDateTime(date12411) < Convert.ToDateTime(date12311))
                    {

                    }
                    else
                    {

                        cellcouns = 0;
                        iDatebm = 0;
                        IDLS = obj.IDL;
                        label42OneDay = string.Empty;
                        dates = obj.dates;
                        //var userschedule = CrewDetailList.Select(x => new { x.SeaNWK, x.PortNWK }).FirstOrDefault();
                        var userschedule = CrewDetailList;
                        while (iDatebm < 7)
                        {

                            var checkIDL = CheckIDLNext(dates, IDLS, CheckIDL);
                            dates = checkIDL.Item1;
                            string WRID = checkIDL.Item2;
                            IDLS = checkIDL.Item3;


                            var checkuser = checkusers.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
                            if (checkuser != null)
                            {
                                int countbms = checkuser.Cellcount;
                                if (countbms + cellcouns <= 48)
                                {
                                    string startEndbbs = checkuser.hrs;

                                    string[] hrsvalue = startEndbbs.TrimEnd(',').Split();
                                    bool existvalues = hrsvalue.Contains("1");
                                    if (existvalues)
                                    {
                                        cellcouns += checkuser.Cellcount;
                                    }
                                    else
                                    {
                                        startEndbbs = (obj.options != "At Sea" && userschedule.PortNWK != null) ? userschedule.PortNWK.ToString() : userschedule.SeaNWK.ToString();
                                        startEndbbs = startEndbbs.Replace("2", "1");
                                        cellcouns += 48;
                                    }


                                    label42OneDay = label42OneDay + startEndbbs;
                                }
                                else
                                {
                                    int countbms1 = checkuser.Cellcount;
                                    int finalCellcount = 48 - cellcouns;
                                    string startEndbbs = checkuser.hrs;
                                    string[] st22bbs = startEndbbs.TrimEnd(',').Split(',');

                                    st22bbs = st22bbs.Skip(countbms1 - finalCellcount).Select(i => i.ToString()).ToArray();

                                    startEndbbs = string.Join(",", st22bbs) + ",";

                                    string[] hrsvalue = startEndbbs.TrimEnd(',').Split();
                                    bool existvalues = hrsvalue.Contains("1");
                                    if (existvalues)
                                    {
                                        cellcouns += checkuser.Cellcount;
                                    }
                                    else
                                    {

                                        startEndbbs = (obj.options != "At Sea" && userschedule.PortNWK != null) ? userschedule.PortNWK.ToString() : userschedule.SeaNWK.ToString();
                                        startEndbbs = startEndbbs.Replace("2", "1");
                                        cellcouns += 48;
                                    }

                                    label42OneDay = label42OneDay + startEndbbs;



                                }
                            }
                            else
                            {
                                cellcouns += 48;
                                string startEndbbs = string.Empty;
                                //for (int i = 0; i < 48; i++)
                                //{
                                //    startEndbbs += "0" + ",";
                                //}

                                startEndbbs = (obj.options != "At Sea" && userschedule.PortNWK != null) ? userschedule.PortNWK.ToString() : userschedule.SeaNWK.ToString();
                                startEndbbs = startEndbbs.Replace("2", "1");

                                label42OneDay = label42OneDay + startEndbbs;

                            }

                            if (cellcouns >= 48)
                                break;

                            iDatebm++;
                        }

                        string[] stbmsF1 = null;
                        stbmsF1 = label42OneDay.TrimEnd(',').Split(',');
                        st4bmsF1 = newhr.Concat(stbmsF1).ToArray();

                        //...New Implementation for an hour....
                        string bmschange = string.Join(",", st4bmsF1);
                        bmschange = bmschange + ',';
                        bmschange = Morethan1hrsNormal(bmschange);
                        st4bmsF1 = bmschange.TrimEnd(',').Split(',');
                        //...End New Implementation for an hour....

                        string lbl1mainf1 = "";
                        int intindexf1 = 0;

                        for (int sf1 = 0; sf1 <= 47; sf1++)
                        {

                            //if (stbmsF1[sf1] == "1")
                            //{
                            //    intindexf1 = sf1;



                            //    if (total71mainf1 == 0)
                            //    {
                            //        total71mainf1 = sf1;

                            //        b = sf1;

                            //        lbl1mainf1 += b.ToString() + ",";

                            //        total71mainf1 = sf1 + 1;

                            //    }

                            //}
                            //else if (stbmsF1[sf1] == "0")
                            //{
                            //    if (total71mainf1 != 0)
                            //    {
                            //        lbl1mainf1 += intindexf1.ToString() + ",";

                            //        intindexf1 = 0;
                            //        total71mainf1 = 0;
                            //    }

                            //}
                            //if (sf1 == stbmsF1.Length - 1)
                            //{
                            //    if (total71mainf1 != 0)
                            //    {
                            //        lbl1mainf1 += intindexf1.ToString() + ",";
                            //        intindexf1 = 0;
                            //        total71mainf1 = 0;
                            //    }
                            //}


                            if (stbmsF1[sf1] == "1")
                            {
                                intindexf1 = sf1;
                                b = sf1;

                                lbl1mainf1 += b.ToString() + ",";
                            }
                            else if (stbmsF1[sf1] == "2")
                            {
                                intindexf1 = sf1;
                                b = sf1;

                                lbl1mainf1 += b.ToString() + ",";
                            }


                        }
                        //..............
                        string[] todayf1 = null;
                        todayf1 = lbl1mainf1.TrimEnd(',').Split(new[] { ',' });
                        int lenth = todayf1.Length;
                        for (int yf11 = 0; yf11 < lenth; yf11++)
                        {



                            int index1bm = Convert.ToInt32(todayf1[yf11]) + 1;
                            int countbm = index1bm + 48;


                            double totalbm = 0;
                            List<double> listbbm = new List<double>();
                            listbbm.Clear();
                            string[] numsbm = st4bmsF1.Skip(index1bm).Take(48).ToArray();


                            int norm16 = 0;
                            norm16 = numsbm.Length;
                            for (long i = 0; i < norm16; i++)
                            {

                                if (Convert.ToInt32(numsbm[i]) == 0)
                                {
                                    totalbm += 0.5;
                                }
                                else if (Convert.ToInt32(numsbm[i]) == 1)
                                {
                                    if (totalbm != 0)
                                    {
                                        listbbm.Add(totalbm);
                                        totalbm = 0;
                                    }
                                }
                                if (i == numsbm.Length - 1)
                                {
                                    if (totalbm != 0)
                                    {
                                        listbbm.Add(totalbm);
                                        totalbm = 0;
                                    }
                                }

                            }

                            if (listbbm.Count() == 0)
                                listbbm.Add(0);
                            double abc22 = listbbm.Sum();

                            var asd = listbbm.Where(element => element >= (double)DefineRules.Rule6Hrs).FirstOrDefault();

                            if (asd == 0 && listbbm.Count > 0)
                            {
                                obj.NC5 = "Future Deviation: No minimum 6 hour consecutive rest period";


                            }

                            //10 hrs Condition....
                            var hrs10Count = listbbm.Sum();

                            int hrs10 = (int)(DefineRules.Rule10Hrs);
                            if (hrs10Count < hrs10)
                            {
                                obj.NC6 = "Future Deviation: Less than 10 hours total rest in 24 hour period";


                            }

                            if (listbbm.Count >= 2 && DefineRules.Rule10DevideTwo && hrs10Count >= 10)
                            {

                                double A = 0, B = 0, C = 0, D = 0, E = 0, F = 0, G = 0, H = 0, I = 0, J = 0, K = 0, L = 0, M = 0, N = 0, O = 0, P = 0, Q = 0, R = 0, S = 0, A1 = 0, B1 = 0, C1 = 0, D1 = 0, E1 = 0, F1 = 0, G1 = 0, H1 = 0, I1 = 0, J1 = 0, K1 = 0, L1 = 0, M1 = 0, N1 = 0, O1 = 0, P1 = 0, Q1 = 0, R1 = 0, S1 = 0;

                                int aa1 = 0;
                                foreach (var abc1 in listbbm)
                                {

                                    if (abc1 >= 10)
                                    {
                                        aa1 = aa1 + 1;
                                        break;
                                    }

                                    else
                                    {
                                        if (abc1 == 9.5)
                                        {
                                            if (A == 9.5)
                                            {
                                                A1 = abc1;
                                            }
                                            else
                                            {
                                                A = abc1;
                                            }
                                        }
                                        else if (abc1 == 9.0)
                                        {
                                            if (B == 9.0)
                                            {
                                                B1 = abc1;
                                            }
                                            else
                                            {
                                                B = abc1;
                                            }
                                        }

                                        else if (abc1 == 8.5)
                                        {
                                            if (C == 8.5)
                                            {
                                                C1 = abc1;
                                            }
                                            else
                                            {
                                                C = abc1;
                                            }
                                        }
                                        else if (abc1 == 8.0)
                                        {
                                            if (D == 8.0)
                                            {
                                                D1 = abc1;
                                            }
                                            else
                                            {
                                                D = abc1;
                                            }
                                        }
                                        else if (abc1 == 7.5)
                                        {
                                            if (E == 7.5)
                                            {
                                                E1 = abc1;
                                            }
                                            else
                                            {
                                                E = abc1;
                                            }
                                        }
                                        else if (abc1 == 7.0)
                                        {
                                            if (F == 7.0)
                                            {
                                                F1 = abc1;
                                            }
                                            else
                                            {
                                                F = abc1;
                                            }
                                        }
                                        else if (abc1 == 6.5)
                                        {
                                            if (G == 6.5)
                                            {
                                                G1 = abc1;
                                            }
                                            else
                                            {
                                                G = abc1;
                                            }
                                        }
                                        else if (abc1 == 6.0)
                                        {
                                            if (H == 6.0)
                                            {
                                                H1 = abc1;
                                            }
                                            else
                                            {
                                                H = abc1;
                                            }
                                        }
                                        else if (abc1 == 5.5)
                                        {
                                            if (I == 5.5)
                                            {
                                                I1 = abc1;
                                            }
                                            else
                                            {
                                                I = abc1;
                                            }
                                        }
                                        else if (abc1 == 5.0)
                                        {
                                            if (J == 5.0)
                                            {
                                                J1 = abc1;
                                            }
                                            else
                                            {
                                                J = abc1;
                                            }
                                        }
                                        else if (abc1 == 4.5)
                                        {
                                            if (K == 4.5)
                                            {
                                                K1 = abc1;
                                            }
                                            else
                                            {
                                                K = abc1;
                                            }
                                        }
                                        else if (abc1 == 4.0)
                                        {
                                            if (L == 4.0)
                                            {
                                                L1 = abc1;
                                            }
                                            else
                                            {
                                                L = abc1;
                                            }
                                        }
                                        else if (abc1 == 3.5)
                                        {
                                            if (M == 3.5)
                                            {
                                                M1 = abc1;
                                            }
                                            else
                                            {
                                                M = abc1;
                                            }
                                        }
                                        else if (abc1 == 3.0)
                                        {
                                            if (N == 3.0)
                                            {
                                                N1 = abc1;
                                            }
                                            else
                                            {
                                                N1 = abc1;
                                            }
                                        }
                                        else if (abc1 == 2.5)
                                        {
                                            if (O == 2.5)
                                            {
                                                O1 = abc1;
                                            }
                                            else
                                            {
                                                O = abc1;
                                            }
                                        }
                                        else if (abc1 == 2.0)
                                        {
                                            if (P == 2.0)
                                            {
                                                P1 = abc1;
                                            }
                                            else
                                            {
                                                P = abc1;
                                            }
                                        }

                                        else if (abc1 == 1.5)
                                        {
                                            if (Q == 1.5)
                                            {
                                                Q1 = abc1;
                                            }
                                            else
                                            {
                                                Q = abc1;
                                            }
                                        }
                                        else if (abc1 == 1.0)
                                        {
                                            if (R == 1.0)
                                            {
                                                R1 = abc1;
                                            }
                                            else
                                            {
                                                R = abc1;
                                            }
                                        }
                                        else if (abc1 == 0.5)
                                        {
                                            if (S == 0.5)
                                            {
                                                S1 = abc1;
                                            }
                                            else
                                            {
                                                S = abc1;
                                            }
                                        }

                                    }

                                }
                                if (aa1 != 1)
                                {
                                    if ((A + B) >= 10 || (A + C) >= 10 || (A + D) >= 10 || (A + E) >= 10 || (A + F) >= 10 || (A + G) >= 10 || (A + H) >= 10 || (A + I) >= 10 || (A + J) >= 10 || (A + K) >= 10 || (A + L) >= 10 || (A + M) >= 10 || (A + N) >= 10 || (A + O) >= 10 || (A + P) >= 10 || (A + Q) >= 10 || (A + R) >= 10 || (A + S) >= 10 || (A + A1) >= 10 || (A + B1) >= 10 || (A + C1) >= 10 || (A + D1) >= 10 || (A + E1) >= 10 || (A + F1) >= 10 || (A + G1) >= 10 || (A + H1) >= 10 || (A + I1) >= 10 || (A + J1) >= 10 || (A + K1) >= 10 || (A + L1) >= 10 || (A + M1) >= 10 || (A + N1) >= 10 || (A + O1) >= 10 || (A + P1) >= 10 || (A + Q1) >= 10 || (A + R1) >= 10 || (A + S1) >= 10 ||
                                        (B + C) >= 10 || (B + D) >= 10 || (B + E) >= 10 || (B + F) >= 10 || (B + G) >= 10 || (B + H) >= 10 || (B + I) >= 10 || (B + J) >= 10 || (B + K) >= 10 || (B + L) >= 10 || (B + M) >= 10 || (B + N) >= 10 || (B + O) >= 10 || (B + P) >= 10 || (B + Q) >= 10 || (B + R) >= 10 || (B + S) >= 10 || (B + A1) >= 10 || (B + B1) >= 10 || (B + C1) >= 10 || (B + D1) >= 10 || (B + E1) >= 10 || (B + F1) >= 10 || (B + G1) >= 10 || (B + H1) >= 10 || (B + I1) >= 10 || (B + J1) >= 10 || (B + K1) >= 10 || (B + L1) >= 10 || (B + M1) >= 10 || (B + N1) >= 10 || (B + O1) >= 10 || (B + P1) >= 10 || (B + Q1) >= 10 || (B + R1) >= 10 || (B + S1) >= 10 ||
                                        (C + D) >= 10 || (C + E) >= 10 || (C + F) >= 10 || (C + F) >= 10 || (C + G) >= 10 || (C + H) >= 10 || (C + I) >= 10 || (C + J) >= 10 || (C + K) >= 10 || (C + L) >= 10 || (C + M) >= 10 || (C + N) >= 10 || (C + O) >= 10 || (C + P) >= 10 || (C + Q) >= 10 || (C + R) >= 10 || (C + S) >= 10 || (C + A1) >= 10 || (C + B1) >= 10 || (C + C1) >= 10 || (C + D1) >= 10 || (C + E1) >= 10 || (C + F1) >= 10 || (C + G1) >= 10 || (C + H1) >= 10 || (C + I1) >= 10 || (C + J1) >= 10 || (C + K1) >= 10 || (C + L1) >= 10 || (C + M1) >= 10 || (C + N1) >= 10 || (C + O1) >= 10 || (C + P1) >= 10 || (C + Q1) >= 10 || (C + R1) >= 10 || (C + S1) >= 10 ||
                                        (D + E) >= 10 || (D + F) >= 10 || (D + G) >= 10 || (D + H) >= 10 || (D + I) >= 10 || (D + J) >= 10 || (D + K) >= 10 || (D + L) >= 10 || (D + M) >= 10 || (D + N) >= 10 || (D + O) >= 10 || (D + P) >= 10 || (D + Q) >= 10 || (D + R) >= 10 || (D + S) >= 10 || (D + A1) >= 10 || (D + B1) >= 10 || (D + C1) >= 10 || (D + D1) >= 10 || (D + E1) >= 10 || (D + F1) >= 10 || (D + G1) >= 10 || (D + H1) >= 10 || (D + I1) >= 10 || (D + J1) >= 10 || (D + K1) >= 10 || (D + L1) >= 10 || (D + M1) >= 10 || (D + N1) >= 10 || (D + O1) >= 10 || (D + P1) >= 10 || (D + Q1) >= 10 || (D + R1) >= 10 || (D + S1) >= 10 ||
                                        (E + F) >= 10 || (E + G) >= 10 || (E + H) >= 10 || (E + I) >= 10 || (E + J) >= 10 || (E + K) >= 10 || (E + L) >= 10 || (E + M) >= 10 || (E + N) >= 10 || (E + O) >= 10 || (E + P) >= 10 || (E + Q) >= 10 || (E + R) >= 10 || (E + S) >= 10 || (E + A1) >= 10 || (E + B1) >= 10 || (E + C1) >= 10 || (E + D1) >= 10 || (E + E1) >= 10 || (E + F1) >= 10 || (E + G1) >= 10 || (E + H1) >= 10 || (E + I1) >= 10 || (E + J1) >= 10 || (E + K1) >= 10 || (E + L1) >= 10 || (E + M1) >= 10 || (E + N1) >= 10 || (E + O1) >= 10 || (E + P1) >= 10 || (E + Q1) >= 10 || (E + R1) >= 10 || (E + S1) >= 10 ||
                                        (F + G) >= 10 || (F + H) >= 10 || (F + I) >= 10 || (F + J) >= 10 || (F + K) >= 10 || (F + L) >= 10 || (F + M) >= 10 || (F + N) >= 10 || (F + O) >= 10 || (F + P) >= 10 || (F + Q) >= 10 || (F + R) >= 10 || (F + S) >= 10 || (F + A1) >= 10 || (F + B1) >= 10 || (F + C1) >= 10 || (F + D1) >= 10 || (F + E1) >= 10 || (F + F1) >= 10 || (F + G1) >= 10 || (F + H1) >= 10 || (F + I1) >= 10 || (F + J1) >= 10 || (F + K1) >= 10 || (F + L1) >= 10 || (F + M1) >= 10 || (F + N1) >= 10 || (F + O1) >= 10 || (F + P1) >= 10 || (F + Q1) >= 10 || (F + R1) >= 10 || (F + S1) >= 10 ||
                                        (G + H) >= 10 || (G + I) >= 10 || (G + J) >= 10 || (G + K) >= 10 || (G + L) >= 10 || (G + M) >= 10 || (G + N) >= 10 || (G + O) >= 10 || (G + P) >= 10 || (G + Q) >= 10 || (G + R) >= 10 || (G + S) >= 10 || (G + A1) >= 10 || (G + B1) >= 10 || (G + C1) >= 10 || (G + D1) >= 10 || (G + E1) >= 10 || (G + F1) >= 10 || (G + G1) >= 10 || (G + H1) >= 10 || (G + I1) >= 10 || (G + J1) >= 10 || (G + K1) >= 10 || (G + L1) >= 10 || (G + M1) >= 10 || (G + N1) >= 10 || (G + O1) >= 10 || (G + P1) >= 10 || (G + Q1) >= 10 || (G + R1) >= 10 || (G + S1) >= 10 ||
                                        (H + I) >= 10 || (H + J) >= 10 || (H + K) >= 10 || (H + L) >= 10 || (H + M) >= 10 || (H + N) >= 10 || (H + O) >= 10 || (H + P) >= 10 || (H + Q) >= 10 || (H + R) >= 10 || (H + S) >= 10 || (H + A1) >= 10 || (H + B1) >= 10 || (H + C1) >= 10 || (H + D1) >= 10 || (H + E1) >= 10 || (H + F1) >= 10 || (H + G1) >= 10 || (H + H1) >= 10 || (H + I1) >= 10 || (H + J1) >= 10 || (H + K1) >= 10 || (H + L1) >= 10 || (H + M1) >= 10 || (H + N1) >= 10 || (H + O1) >= 10 || (H + P1) >= 10 || (H + Q1) >= 10 || (H + R1) >= 10 || (H + S1) >= 10 ||
                                        (I + J) >= 10 || (I + K) >= 10 || (I + L) >= 10 || (I + M) >= 10 || (I + N) >= 10 || (I + O) >= 10 || (I + P) >= 10 || (I + Q) >= 10 || (I + R) >= 10 || (I + S) >= 10 || (I + A1) >= 10 || (I + B1) >= 10 || (I + C1) >= 10 || (I + D1) >= 10 || (I + E1) >= 10 || (I + F1) >= 10 || (I + G1) >= 10 || (I + H1) >= 10 || (I + I1) >= 10 || (I + J1) >= 10 || (I + K1) >= 10 || (I + L1) >= 10 || (I + M1) >= 10 || (I + N1) >= 10 || (I + O1) >= 10 || (I + P1) >= 10 || (I + Q1) >= 10 || (I + R1) >= 10 || (I + S1) >= 10 ||
                                        (J + K) >= 10 || (J + L) >= 10 || (J + M) >= 10 || (J + N) >= 10 || (J + O) >= 10 || (J + P) >= 10 || (J + Q) >= 10 || (J + R) >= 10 || (J + S) >= 10 || (J + A1) >= 10 || (J + B1) >= 10 || (J + C1) >= 10 || (J + D1) >= 10 || (J + E1) >= 10 || (J + F1) >= 10 || (J + G1) >= 10 || (J + H1) >= 10 || (J + I1) >= 10 || (J + J1) >= 10 || (J + K1) >= 10 || (J + L1) >= 10 || (J + M1) >= 10 || (J + N1) >= 10 || (J + O1) >= 10 || (J + P1) >= 10 || (J + Q1) >= 10 || (J + R1) >= 10 || (J + S1) >= 10 ||
                                        (K + L) >= 10 || (K + M) >= 10 || (K + N) >= 10 || (K + O) >= 10 || (K + P) >= 10 || (K + Q) >= 10 || (K + R) >= 10 || (K + S) >= 10 || (K + A1) >= 10 || (K + B1) >= 10 || (K + C1) >= 10 || (K + D1) >= 10 || (K + E1) >= 10 || (K + F1) >= 10 || (K + G1) >= 10 || (K + H1) >= 10 || (K + I1) >= 10 || (K + J1) >= 10 || (K + K1) >= 10 || (K + L1) >= 10 || (K + M1) >= 10 || (K + N1) >= 10 || (K + O1) >= 10 || (K + P1) >= 10 || (K + Q1) >= 10 || (K + R1) >= 10 || (K + S1) >= 10 ||
                                        (L + M) >= 10 || (L + N) >= 10 || (L + O) >= 10 || (L + P) >= 10 || (L + Q) >= 10 || (L + R) >= 10 || (L + S) >= 10 || (L + A1) >= 10 || (L + B1) >= 10 || (L + C1) >= 10 || (L + D1) >= 10 || (L + E1) >= 10 || (L + F1) >= 10 || (L + G1) >= 10 || (L + H1) >= 10 || (L + I1) >= 10 || (L + J1) >= 10 || (L + K1) >= 10 || (L + L1) >= 10 || (L + M1) >= 10 || (L + N1) >= 10 || (L + O1) >= 10 || (L + P1) >= 10 || (L + Q1) >= 10 || (L + R1) >= 10 || (L + S1) >= 10 ||
                                        (M + N) >= 10 || (M + O) >= 10 || (M + P) >= 10 || (M + Q) >= 10 || (M + R) >= 10 || (M + S) >= 10 || (M + A1) >= 10 || (M + B1) >= 10 || (M + C1) >= 10 || (M + D1) >= 10 || (M + E1) >= 10 || (M + F1) >= 10 || (M + G1) >= 10 || (M + H1) >= 10 || (M + I1) >= 10 || (M + J1) >= 10 || (M + K1) >= 10 || (M + L1) >= 10 || (M + M1) >= 10 || (M + N1) >= 10 || (M + O1) >= 10 || (M + P1) >= 10 || (M + Q1) >= 10 || (M + R1) >= 10 || (M + S1) >= 10 ||
                                        (N + O) >= 10 || (N + P) >= 10 || (N + Q) >= 10 || (N + R) >= 10 || (N + S) >= 10 || (N + A1) >= 10 || (N + B1) >= 10 || (N + C1) >= 10 || (N + D1) >= 10 || (N + E1) >= 10 || (N + F1) >= 10 || (N + G1) >= 10 || (N + H1) >= 10 || (N + I1) >= 10 || (N + J1) >= 10 || (N + K1) >= 10 || (N + L1) >= 10 || (N + M1) >= 10 || (N + N1) >= 10 || (N + O1) >= 10 || (N + P1) >= 10 || (N + Q1) >= 10 || (N + R1) >= 10 || (N + S1) >= 10 ||
                                        (O + P) >= 10 || (O + Q) >= 10 || (O + R) >= 10 || (O + S) >= 10 || (O + A1) >= 10 || (O + B1) >= 10 || (O + C1) >= 10 || (O + D1) >= 10 || (O + E1) >= 10 || (O + F1) >= 10 || (O + G1) >= 10 || (O + H1) >= 10 || (O + I1) >= 10 || (O + J1) >= 10 || (O + K1) >= 10 || (O + L1) >= 10 || (O + M1) >= 10 || (O + N1) >= 10 || (O + O1) >= 10 || (O + P1) >= 10 || (O + Q1) >= 10 || (O + R1) >= 10 || (O + S1) >= 10 ||
                                        (P + Q) >= 10 || (P + R) >= 10 || (P + S) >= 10 || (P + A1) >= 10 || (P + B1) >= 10 || (P + C1) >= 10 || (P + D1) >= 10 || (P + E1) >= 10 || (P + F1) >= 10 || (P + G1) >= 10 || (P + H1) >= 10 || (P + I1) >= 10 || (P + J1) >= 10 || (P + K1) >= 10 || (P + L1) >= 10 || (P + M1) >= 10 || (P + N1) >= 10 || (P + O1) >= 10 || (P + P1) >= 10 || (P + Q1) >= 10 || (P + R1) >= 10 || (P + S1) >= 10 ||
                                        (Q + R) >= 10 || (Q + S) >= 10 || (Q + A1) >= 10 || (Q + B1) >= 10 || (Q + C1) >= 10 || (Q + D1) >= 10 || (Q + E1) >= 10 || (Q + F1) >= 10 || (Q + G1) >= 10 || (Q + H1) >= 10 || (Q + I1) >= 10 || (Q + J1) >= 10 || (Q + K1) >= 10 || (Q + L1) >= 10 || (Q + M1) >= 10 || (Q + N1) >= 10 || (Q + O1) >= 10 || (Q + P1) >= 10 || (Q + Q1) >= 10 || (Q + R1) >= 10 || (Q + S1) >= 10 ||
                                        (R + S) >= 10 || (R + A1) >= 10 || (R + B1) >= 10 || (R + C1) >= 10 || (R + D1) >= 10 || (R + E1) >= 10 || (R + F1) >= 10 || (R + G1) >= 10 || (R + H1) >= 10 || (R + I1) >= 10 || (R + J1) >= 10 || (R + K1) >= 10 || (R + L1) >= 10 || (R + M1) >= 10 || (R + N1) >= 10 || (R + O1) >= 10 || (R + P1) >= 10 || (R + Q1) >= 10 || (R + R1) >= 10 || (R + S1) >= 10 ||
                                        (S + A1) >= 10 || (S + B1) >= 10 || (S + C1) >= 10 || (S + D1) >= 10 || (S + E1) >= 10 || (S + F1) >= 10 || (S + G1) >= 10 || (S + H1) >= 10 || (S + I1) >= 10 || (S + J1) >= 10 || (S + K1) >= 10 || (S + L1) >= 10 || (S + M1) >= 10 || (S + N1) >= 10 || (S + O1) >= 10 || (S + P1) >= 10 || (S + Q1) >= 10 || (S + R1) >= 10 || (S + S1) >= 10 ||
                                        (A1 + B1) >= 10 || (A1 + C1) >= 10 || (A1 + D1) >= 10 || (A1 + E1) >= 10 || (A1 + F1) >= 10 || (A1 + G1) >= 10 || (A1 + H1) >= 10 || (A1 + I1) >= 10 || (A1 + J1) >= 10 || (A1 + K1) >= 10 || (A1 + L1) >= 10 || (A1 + M1) >= 10 || (A1 + N1) >= 10 || (A1 + O1) >= 10 || (A1 + P1) >= 10 || (A1 + Q1) >= 10 || (A1 + R1) >= 10 || (A1 + S1) >= 10 ||
                                        (B1 + C1) >= 10 || (B1 + D1) >= 10 || (B1 + E1) >= 10 || (B1 + F1) >= 10 || (B1 + G1) >= 10 || (B1 + H1) >= 10 || (B1 + I1) >= 10 || (B1 + J1) >= 10 || (B1 + K1) >= 10 || (B1 + L1) >= 10 || (B1 + M1) >= 10 || (B1 + N1) >= 10 || (B1 + O1) >= 10 || (B1 + P1) >= 10 || (B1 + Q1) >= 10 || (B1 + R1) >= 10 || (B1 + S1) >= 10 ||
                                        (C1 + D1) >= 10 || (C1 + E1) >= 10 || (C1 + F1) >= 10 || (C1 + G1) >= 10 || (C1 + H1) >= 10 || (C1 + I1) >= 10 || (C1 + J1) >= 10 || (C1 + K1) >= 10 || (C1 + L1) >= 10 || (C1 + M1) >= 10 || (C1 + N1) >= 10 || (C1 + O1) >= 10 || (C1 + P1) >= 10 || (C1 + Q1) >= 10 || (C1 + R1) >= 10 || (C1 + S1) >= 10 ||
                                        (D1 + E1) >= 10 || (D1 + F1) >= 10 || (D1 + G1) >= 10 || (D1 + H1) >= 10 || (D1 + I1) >= 10 || (D1 + J1) >= 10 || (D1 + K1) >= 10 || (D1 + L1) >= 10 || (D1 + M1) >= 10 || (D1 + N1) >= 10 || (D1 + O1) >= 10 || (D1 + P1) >= 10 || (D1 + Q1) >= 10 || (D1 + R1) >= 10 || (D1 + S1) >= 10 ||
                                        (E1 + F1) >= 10 || (E1 + G1) >= 10 || (E1 + H1) >= 10 || (E1 + I1) >= 10 || (E1 + J1) >= 10 || (E1 + K1) >= 10 || (E1 + L1) >= 10 || (E1 + M1) >= 10 || (E1 + N1) >= 10 || (E1 + O1) >= 10 || (E1 + P1) >= 10 || (E1 + Q1) >= 10 || (E1 + R1) >= 10 || (E1 + S1) >= 10 ||
                                        (F1 + G1) >= 10 || (F1 + H1) >= 10 || (F1 + I1) >= 10 || (F1 + J1) >= 10 || (F1 + K1) >= 10 || (F1 + L1) >= 10 || (F1 + M1) >= 10 || (F1 + N1) >= 10 || (F1 + O1) >= 10 || (F1 + P1) >= 10 || (F1 + Q1) >= 10 || (F1 + R1) >= 10 || (F1 + S1) >= 10 ||
                                        (G1 + H1) >= 10 || (G1 + I1) >= 10 || (G1 + J1) >= 10 || (G1 + K1) >= 10 || (G1 + L1) >= 10 || (G1 + M1) >= 10 || (G1 + N1) >= 10 || (G1 + O1) >= 10 || (G1 + P1) >= 10 || (G1 + Q1) >= 10 || (G1 + R1) >= 10 || (G1 + S1) >= 10 ||
                                        (H1 + I1) >= 10 || (H1 + J1) >= 10 || (H1 + K1) >= 10 || (H1 + L1) >= 10 || (H1 + M1) >= 10 || (H1 + N1) >= 10 || (H1 + O1) >= 10 || (H1 + P1) >= 10 || (H1 + Q1) >= 10 || (H1 + R1) >= 10 || (H1 + S1) >= 10 ||
                                        (I1 + J1) >= 10 || (I1 + K1) >= 10 || (I1 + L1) >= 10 || (I1 + M1) >= 10 || (I1 + N1) >= 10 || (I1 + O1) >= 10 || (I1 + P1) >= 10 || (I1 + Q1) >= 10 || (I1 + R1) >= 10 || (I1 + S1) >= 10 ||
                                        (J1 + K1) >= 10 || (J1 + L1) >= 10 || (J1 + M1) >= 10 || (J1 + N1) >= 10 || (J1 + O1) >= 10 || (J1 + P1) >= 10 || (J1 + Q1) >= 10 || (J1 + R1) >= 10 || (J1 + S1) >= 10 ||
                                        (K1 + L1) >= 10 || (K1 + M1) >= 10 || (K1 + N1) >= 10 || (K1 + O1) >= 10 || (K1 + P1) >= 10 || (K1 + Q1) >= 10 || (K1 + R1) >= 10 || (K1 + S1) >= 10 ||
                                        (L1 + M1) >= 10 || (L1 + N1) >= 10 || (L1 + O1) >= 10 || (L1 + P1) >= 10 || (L1 + Q1) >= 10 || (L1 + R1) >= 10 || (L1 + S1) >= 10 ||
                                        (M1 + N1) >= 10 || (M1 + O1) >= 10 || (M1 + P1) >= 10 || (M1 + Q1) >= 10 || (M1 + R1) >= 10 || (M1 + S1) >= 10 ||
                                        (N1 + O1) >= 10 || (N1 + P1) >= 10 || (N1 + Q1) >= 10 || (N1 + R1) >= 10 || (N1 + S1) >= 10 ||
                                        (O1 + P1) >= 10 || (O1 + Q1) >= 10 || (O1 + R1) >= 10 || (O1 + S1) >= 10 ||
                                        (P1 + Q1) >= 10 || (P1 + R1) >= 10 || (P1 + S1) >= 10 ||
                                        (Q1 + R1) >= 10 || (Q1 + S1) >= 10 ||
                                        (R1 + S1) >= 10)
                                    {

                                    }
                                    else
                                    {
                                        obj.NC7 = "Future Deviation: Minimum total rest comprises more than 2 period";
                                        //ncbms1 = ncbms1 + 1;
                                    }
                                }

                            }


                        }


                    }






                    //..........for future 7days...................
                    string[] st4bmsF7 = null;

                    if (Convert.ToDateTime(date12411) < Convert.ToDateTime(date12311))
                    {

                    }
                    else
                    {

                        cellcouns = 0;
                        iDatebm = 0;
                        IDLS = obj.IDL;
                        label42OneDay = string.Empty;
                        dates = obj.dates;
                        //var userschedule = CrewDetailList.Select(x => new { x.SeaNWK, x.PortNWK }).FirstOrDefault();
                        var userschedule = CrewDetailList;
                        while (iDatebm < 15)
                        {

                            var checkIDL = CheckIDLNext(dates, IDLS, CheckIDL);
                            dates = checkIDL.Item1;
                            string WRID = checkIDL.Item2;
                            IDLS = checkIDL.Item3;


                            var checkuser = checkusers.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
                            if (checkuser != null)
                            {
                                int countbms = checkuser.Cellcount;
                                if (countbms + cellcouns <= 336)
                                {
                                    string startEndbbs = checkuser.hrs;

                                    string[] hrsvalue = startEndbbs.TrimEnd(',').Split();
                                    bool existvalues = hrsvalue.Contains("1");
                                    if (existvalues)
                                    {
                                        cellcouns += checkuser.Cellcount;
                                    }
                                    else
                                    {

                                        startEndbbs = (obj.options != "At Sea" && userschedule.PortNWK != null) ? userschedule.PortNWK.ToString() : userschedule.SeaNWK.ToString();
                                        startEndbbs = startEndbbs.Replace("2", "1");
                                        cellcouns += 48;
                                    }


                                    label42OneDay = label42OneDay + startEndbbs;
                                }
                                else
                                {
                                    int countbms1 = checkuser.Cellcount;
                                    int finalCellcount = 336 - cellcouns;
                                    string startEndbbs = checkuser.hrs;
                                    string[] st22bbs = startEndbbs.TrimEnd(',').Split(',');

                                    st22bbs = st22bbs.Skip(countbms1 - finalCellcount).Select(i => i.ToString()).ToArray();

                                    startEndbbs = string.Join(",", st22bbs) + ",";

                                    string[] hrsvalue = startEndbbs.TrimEnd(',').Split();
                                    bool existvalues = hrsvalue.Contains("1");
                                    if (existvalues)
                                    {
                                        cellcouns += checkuser.Cellcount;
                                    }
                                    else
                                    {

                                        //startEndbbs = userschedule.SeaNWK.Replace("2", "1");
                                        startEndbbs = (obj.options != "At Sea" && userschedule.PortNWK != null) ? userschedule.PortNWK.ToString() : userschedule.SeaNWK.ToString();
                                        startEndbbs = startEndbbs.Replace("2", "1");
                                        cellcouns += 48;
                                    }

                                    label42OneDay = label42OneDay + startEndbbs;



                                }
                            }
                            else
                            {
                                cellcouns += 48;
                                string startEndbbs = string.Empty;
                                //for (int i = 0; i < 48; i++)
                                //{
                                //    startEndbbs += "0" + ",";
                                //}

                                startEndbbs = (obj.options != "At Sea" && userschedule.PortNWK != null) ? userschedule.PortNWK.ToString() : userschedule.SeaNWK.ToString();
                                startEndbbs = startEndbbs.Replace("2", "1");

                                label42OneDay = label42OneDay + startEndbbs;

                            }

                            if (cellcouns >= 336)
                                break;

                            iDatebm++;
                        }

                        string[] stbmsF7 = null;
                        stbmsF7 = label42OneDay.TrimEnd(',').Split(',');

                        st4bmsF7 = newhr.Concat(stbmsF7).ToArray();

                        //...New Implementation for an hour....
                        string bmschange = string.Join(",", st4bmsF7);
                        bmschange = bmschange + ',';
                        bmschange = Morethan1hrsNormal(bmschange);
                        st4bmsF7 = bmschange.TrimEnd(',').Split(',');
                        //...End New Implementation for an hour....


                        int index = st4bmsF7.Length - 48;
                        string[] numsbm = st4bmsF7.Skip(index).Take(48).ToArray();

                        string lbl1mainf1 = "";
                        int intindexf1 = 0;


                        for (int sf1 = 0; sf1 <= 47; sf1++)
                        {

                            //if (numsbm[sf1] == "1")
                            //{
                            //    intindexf1 = sf1;



                            //    if (total71mainf1 == 0)
                            //    {
                            //        total71mainf1 = sf1;

                            //        b = sf1;

                            //        lbl1mainf1 += b.ToString() + ",";

                            //        total71mainf1 = sf1 + 1;

                            //    }

                            //}
                            //else if (numsbm[sf1] == "0")
                            //{
                            //    if (total71mainf1 != 0)
                            //    {
                            //        lbl1mainf1 += intindexf1.ToString() + ",";

                            //        intindexf1 = 0;
                            //        total71mainf1 = 0;
                            //    }

                            //}
                            //if (sf1 == numsbm.Length - 1)
                            //{
                            //    if (total71mainf1 != 0)
                            //    {
                            //        lbl1mainf1 += intindexf1.ToString() + ",";
                            //        intindexf1 = 0;
                            //        total71mainf1 = 0;
                            //    }
                            //}

                            if (numsbm[sf1] == "1")
                            {
                                intindexf1 = sf1;
                                b = sf1;
                                lbl1mainf1 += b.ToString() + ",";

                            }
                            else if (numsbm[sf1] == "2")
                            {
                                intindexf1 = sf1;
                                b = sf1;
                                lbl1mainf1 += b.ToString() + ",";

                            }


                        }



                        string[] todayf = null;
                        todayf = lbl1mainf1.TrimEnd(',').Split(new[] { ',' });
                        for (int yf1 = 0; yf1 < todayf.Length; yf1++)
                        {

                            int index1 = 0;
                            index1 = Convert.ToInt32(todayf[yf1]) + 1;

                            int val1bms11 = 0;
                            val1bms11 = (index1 - 1);


                            double totalbms11 = 0;
                            List<double> listabms11 = new List<double>();
                            listabms11.Clear();


                            string[] numsbms11 = st4bmsF7.Skip(val1bms11).Take(336).ToArray();


                            int norm21 = 0;
                            norm21 = numsbms11.Length;
                            for (long i = 0; i < norm21; i++)
                            {

                                if (Convert.ToInt32(numsbms11[i]) == 0)
                                {
                                    totalbms11 += 0.5;
                                }
                                else if (Convert.ToInt32(numsbms11[i]) == 1)
                                {
                                    if (totalbms11 != 0)
                                    {
                                        listabms11.Add(totalbms11);
                                        totalbms11 = 0;
                                    }
                                }
                                if (i == numsbms11.Length - 1)
                                {
                                    if (totalbms11 != 0)
                                    {
                                        listabms11.Add(totalbms11);
                                        totalbms11 = 0;
                                    }
                                }

                            }


                            if (listabms11.Count() == 0)
                                listabms11.Add(0);

                            double flag2bm = listabms11.Sum();

                            int hrs77 = (int)(DefineRules.Rule77Hrs);
                            if (flag2bm < hrs77)
                            {

                                obj.NC8 = "Future Deviation: Less than 77 hours rest in 7 day period";

                            }


                        }



                    }


                    //..........Opa for 3 days...................

                    var sdaopastatr = sc.OPAStartStops.Where(x => (obj.dates >= x.OPAStart && obj.dates <= x.OPAStop)).FirstOrDefault();

                    bool b11;
                    bool chkOPAforallday = false;
                    string StatusOPA901 = Opa90Rule;

                    if (StatusOPA901 == "OPA90option1")
                    {
                        chkOPAforallday = false;
                        b11 = OPAStartStopTime(obj.WRID, sdaopastatr) == true ? true : false;
                    }
                    else if (StatusOPA901 == "OPA90option2")
                    {
                        b11 = true;
                        chkOPAforallday = true;
                    }
                    else
                    {
                        b11 = false;
                        chkOPAforallday = false;
                    }



                    string[] ST4OPA3 = null;

                    cellcouns = 0;
                    iDatebm = 0;
                    IDLS = obj.IDL;
                    label42OneDay = string.Empty;
                    dates = obj.dates;
                    while (iDatebm < 7)
                    {

                        var checkIDL = CheckIDLBack(dates, IDLS, CheckIDL);
                        dates = checkIDL.Item1;
                        string WRID = checkIDL.Item2;
                        IDLS = checkIDL.Item3;

                        var checkuser = checkusers.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
                        if (checkuser != null)
                        {
                            int countbms = checkuser.Cellcount;
                            if (countbms + cellcouns <= 144)
                            {
                                cellcouns += checkuser.Cellcount;

                                string startEndbbs = checkuser.hrs;
                                label42OneDay = startEndbbs + label42OneDay;
                            }
                            else
                            {
                                int countbms1 = checkuser.Cellcount;
                                int finalCellcount = 144 - cellcouns;
                                string startEndbbs = checkuser.hrs;
                                string[] st22bbs = startEndbbs.TrimEnd(',').Split(',');

                                st22bbs = st22bbs.Skip(countbms1 - finalCellcount).Select(i => i.ToString()).ToArray();

                                startEndbbs = string.Join(",", st22bbs);
                                startEndbbs = startEndbbs + ",";
                                label42OneDay = startEndbbs + label42OneDay;

                                cellcouns += checkuser.Cellcount;

                            }
                        }
                        else
                        {
                            cellcouns += 48;
                            string startEndbbs = string.Empty;
                            for (int i = 0; i < 48; i++)
                            {
                                startEndbbs += "0" + ",";
                            }
                            label42OneDay = startEndbbs + label42OneDay;

                        }

                        if (cellcouns >= 144)
                            break;

                        iDatebm++;
                    }

                    string[] stopa3 = null;
                    stopa3 = label42OneDay.TrimEnd(',').Split(',');

                    ST4OPA3 = stopa3.Concat(newhr).ToArray();


                    //...New Implementation for an hour....
                    string change3opa = string.Join(",", ST4OPA3);
                    change3opa = change3opa + ',';
                    string change33opa = Morethan1hrsNormal(change3opa);
                    ST4OPA3 = change33opa.Split(',');
                    //...End New Implementation for an hour...







                    string opdastart = sdaopastatr != null ? sdaopastatr.OPAStart.ToString() : "00:00";
                    string opastop = sdaopastatr != null ? sdaopastatr.OPAStop.ToString() : "00:00";

                    //string ops11 = "";
                    //ops11 = lbl1main1.Substring(0, lbl1main1.Length - 1);
                    //string[] opss11 = null;
                    //opss11 = ops11.Split(',');


                    int ncbms = 0;
                    //int ncbms1 = 0;
                    ab10rest = new ArrayList();
                    ab10rest.Clear();
                    ab10rest1 = new ArrayList();
                    ab10rest1.Clear();
                    for (int y1 = 0; y1 < today.Length; y1++)
                    {

                        int index1 = 0;
                        index1 = Convert.ToInt32(today[y1]) + 1;
                        int endindex = Convert.ToInt32(today[y1]);

                        string startdateop = Getopadate(endindex.ToString());
                        string enddateop = Getopadate(index1.ToString());

                        startdateop = obj.dates.Add(TimeSpan.Parse(startdateop)).ToString();
                        enddateop = obj.dates.Add(TimeSpan.Parse(enddateop)).ToString();

                        if (index1 == 48 && GetNextValues(obj.dates, obj.IDL, WorkHoursList, CheckIDL))
                        {
                            //Stop check point if filled "0" index in next day....
                            if (today.Length == 1)
                            {
                                GetbackhoursValuesPlanner(obj, WorkHoursList, CheckIDL);  //It have to Impleament to get 77 hrs values

                                //_CommonData1.RestHour7day = obj.RestHour7day;
                                //RaisePropertyChanged("CommonData1");

                            }
                        }
                        else
                        {
                            int count = 0;
                            count = index1 + 48;


                            double total = 0;
                            List<double> lista = new List<double>();
                            lista.Clear();

                            //....................
                            string[] results = new string[48];
                            Array.Copy(conc, index1, results, 0, 48);
                            //................

                            string[] nums = conc.Skip(index1).Take(48).ToArray();


                            int norm9 = 0;
                            norm9 = nums.Length;
                            for (long i = 0; i < norm9; i++)
                            {

                                if (Convert.ToInt32(nums[i]) == 0)
                                {
                                    total += 0.5;
                                }
                                else if (Convert.ToInt32(nums[i]) == 1)
                                {
                                    if (total != 0)
                                    {
                                        lista.Add(total);
                                        total = 0;
                                    }
                                }
                                if (i == nums.Length - 1)
                                {
                                    if (total != 0)
                                    {
                                        lista.Add(total);
                                        total = 0;
                                    }
                                }

                            }

                            if (lista.Count() == 0)
                                lista.Add(0);
                            double abc22 = lista.Sum();



                            ab10rest.Add(abc22);
                            ab10rest.Sort();
                            txtRH = ab10rest[0].ToString();
                            //_CommonData1.RestHours = Convert.ToDecimal(txtRH);
                            //RaisePropertyChanged("CommonData1");
                            obj.RestHours = Convert.ToDecimal(txtRH);
                            obj.RestHourAny24 = txtRH;

                            //string[] restPeriods = label34.TrimEnd(',').Split(',');

                            var asd = lista.Where(element => element >= (double)DefineRules.Rule6Hrs).FirstOrDefault();
                            if (asd == 0 && lista.Count > 0)
                            {
                                obj.NC1 = "No minimum 6 hour consecutive rest period";


                                int bc = Convert.ToInt32(today[y1]);
                                foreach (var item in DynamicGridBorders)
                                {

                                    if (item.Name == "BP" + bc)
                                    {
                                        var ss1 = item.Background;
                                        if (((SolidColorBrush)ss1).Color.ToString() == colorValue1)
                                        {
                                            item.Background = new SolidColorBrush(Colors.Red);
                                        }

                                        break;

                                    }

                                }

                            }//10 hrs Condition....


                            var hrs10Count = lista.Sum();

                            int hrs10 = (int)(DefineRules.Rule10Hrs);
                            if (hrs10Count < hrs10)
                            {
                                obj.NC2 = "Less than 10 hours total rest in 24 hour period";

                                int bc = Convert.ToInt32(today[y1]);
                                foreach (var item in DynamicGridBorders)
                                {
                                    if (item.Name == "BP" + bc)
                                    {
                                        var ss1 = item.Background;
                                        if (((SolidColorBrush)ss1).Color.ToString() == colorValue1)
                                        {
                                            item.Background = new SolidColorBrush(Colors.Red);
                                        }

                                        break;
                                    }
                                }


                            }   //for 10 / 2 next Condition


                            if (lista.Count >= 2 && DefineRules.Rule10DevideTwo && hrs10Count >= 10)
                            {

                                double A = 0, B = 0, C = 0, D = 0, E = 0, F = 0, G = 0, H = 0, I = 0, J = 0, K = 0, L = 0, M = 0, N = 0, O = 0, P = 0, Q = 0, R = 0, S = 0, A1 = 0, B1 = 0, C1 = 0, D1 = 0, E1 = 0, F1 = 0, G1 = 0, H1 = 0, I1 = 0, J1 = 0, K1 = 0, L1 = 0, M1 = 0, N1 = 0, O1 = 0, P1 = 0, Q1 = 0, R1 = 0, S1 = 0;

                                int aa1 = 0;
                                foreach (var abc1 in lista)
                                {

                                    if (abc1 >= 10)
                                    {
                                        aa1 = aa1 + 1;
                                        break;
                                    }

                                    else
                                    {
                                        if (abc1 == 9.5)
                                        {
                                            if (A == 9.5)
                                                A1 = abc1;

                                            else
                                                A = abc1;
                                        }
                                        else if (abc1 == 9.0)
                                        {
                                            if (B == 9.0)
                                                B1 = abc1;

                                            else
                                                B = abc1;

                                        }

                                        else if (abc1 == 8.5)
                                        {
                                            if (C == 8.5)
                                                C1 = abc1;

                                            else
                                                C = abc1;

                                        }
                                        else if (abc1 == 8.0)
                                        {
                                            if (D == 8.0)
                                                D1 = abc1;

                                            else
                                                D = abc1;

                                        }
                                        else if (abc1 == 7.5)
                                        {
                                            if (E == 7.5)
                                                E1 = abc1;

                                            else
                                                E = abc1;

                                        }
                                        else if (abc1 == 7.0)
                                        {
                                            if (F == 7.0)
                                                F1 = abc1;

                                            else
                                                F = abc1;

                                        }
                                        else if (abc1 == 6.5)
                                        {
                                            if (G == 6.5)
                                                G1 = abc1;

                                            else
                                                G = abc1;

                                        }
                                        else if (abc1 == 6.0)
                                        {
                                            if (H == 6.0)
                                                H1 = abc1;

                                            else
                                                H = abc1;

                                        }
                                        else if (abc1 == 5.5)
                                        {
                                            if (I == 5.5)
                                                I1 = abc1;

                                            else
                                                I = abc1;

                                        }
                                        else if (abc1 == 5.0)
                                        {
                                            if (J == 5.0)
                                                J1 = abc1;

                                            else
                                                J = abc1;

                                        }
                                        else if (abc1 == 4.5)
                                        {
                                            if (K == 4.5)
                                                K1 = abc1;

                                            else
                                                K = abc1;

                                        }
                                        else if (abc1 == 4.0)
                                        {
                                            if (L == 4.0)
                                                L1 = abc1;

                                            else
                                                L = abc1;

                                        }
                                        else if (abc1 == 3.5)
                                        {
                                            if (M == 3.5)
                                                M1 = abc1;

                                            else
                                                M = abc1;

                                        }
                                        else if (abc1 == 3.0)
                                        {
                                            if (N == 3.0)
                                                N1 = abc1;

                                            else
                                                N1 = abc1;

                                        }
                                        else if (abc1 == 2.5)
                                        {
                                            if (O == 2.5)
                                                O1 = abc1;

                                            else
                                                O = abc1;

                                        }
                                        else if (abc1 == 2.0)
                                        {
                                            if (P == 2.0)
                                                P1 = abc1;

                                            else
                                                P = abc1;

                                        }

                                        else if (abc1 == 1.5)
                                        {
                                            if (Q == 1.5)
                                                Q1 = abc1;

                                            else
                                                Q = abc1;

                                        }
                                        else if (abc1 == 1.0)
                                        {
                                            if (R == 1.0)
                                                R1 = abc1;

                                            else
                                                R = abc1;

                                        }
                                        else if (abc1 == 0.5)
                                        {
                                            if (S == 0.5)
                                                S1 = abc1;

                                            else
                                                S = abc1;

                                        }

                                    }

                                }
                                if (aa1 != 1)
                                {
                                    if ((A + B) >= 10 || (A + C) >= 10 || (A + D) >= 10 || (A + E) >= 10 || (A + F) >= 10 || (A + G) >= 10 || (A + H) >= 10 || (A + I) >= 10 || (A + J) >= 10 || (A + K) >= 10 || (A + L) >= 10 || (A + M) >= 10 || (A + N) >= 10 || (A + O) >= 10 || (A + P) >= 10 || (A + Q) >= 10 || (A + R) >= 10 || (A + S) >= 10 || (A + A1) >= 10 || (A + B1) >= 10 || (A + C1) >= 10 || (A + D1) >= 10 || (A + E1) >= 10 || (A + F1) >= 10 || (A + G1) >= 10 || (A + H1) >= 10 || (A + I1) >= 10 || (A + J1) >= 10 || (A + K1) >= 10 || (A + L1) >= 10 || (A + M1) >= 10 || (A + N1) >= 10 || (A + O1) >= 10 || (A + P1) >= 10 || (A + Q1) >= 10 || (A + R1) >= 10 || (A + S1) >= 10 ||
                                        (B + C) >= 10 || (B + D) >= 10 || (B + E) >= 10 || (B + F) >= 10 || (B + G) >= 10 || (B + H) >= 10 || (B + I) >= 10 || (B + J) >= 10 || (B + K) >= 10 || (B + L) >= 10 || (B + M) >= 10 || (B + N) >= 10 || (B + O) >= 10 || (B + P) >= 10 || (B + Q) >= 10 || (B + R) >= 10 || (B + S) >= 10 || (B + A1) >= 10 || (B + B1) >= 10 || (B + C1) >= 10 || (B + D1) >= 10 || (B + E1) >= 10 || (B + F1) >= 10 || (B + G1) >= 10 || (B + H1) >= 10 || (B + I1) >= 10 || (B + J1) >= 10 || (B + K1) >= 10 || (B + L1) >= 10 || (B + M1) >= 10 || (B + N1) >= 10 || (B + O1) >= 10 || (B + P1) >= 10 || (B + Q1) >= 10 || (B + R1) >= 10 || (B + S1) >= 10 ||
                                        (C + D) >= 10 || (C + E) >= 10 || (C + F) >= 10 || (C + F) >= 10 || (C + G) >= 10 || (C + H) >= 10 || (C + I) >= 10 || (C + J) >= 10 || (C + K) >= 10 || (C + L) >= 10 || (C + M) >= 10 || (C + N) >= 10 || (C + O) >= 10 || (C + P) >= 10 || (C + Q) >= 10 || (C + R) >= 10 || (C + S) >= 10 || (C + A1) >= 10 || (C + B1) >= 10 || (C + C1) >= 10 || (C + D1) >= 10 || (C + E1) >= 10 || (C + F1) >= 10 || (C + G1) >= 10 || (C + H1) >= 10 || (C + I1) >= 10 || (C + J1) >= 10 || (C + K1) >= 10 || (C + L1) >= 10 || (C + M1) >= 10 || (C + N1) >= 10 || (C + O1) >= 10 || (C + P1) >= 10 || (C + Q1) >= 10 || (C + R1) >= 10 || (C + S1) >= 10 ||
                                        (D + E) >= 10 || (D + F) >= 10 || (D + G) >= 10 || (D + H) >= 10 || (D + I) >= 10 || (D + J) >= 10 || (D + K) >= 10 || (D + L) >= 10 || (D + M) >= 10 || (D + N) >= 10 || (D + O) >= 10 || (D + P) >= 10 || (D + Q) >= 10 || (D + R) >= 10 || (D + S) >= 10 || (D + A1) >= 10 || (D + B1) >= 10 || (D + C1) >= 10 || (D + D1) >= 10 || (D + E1) >= 10 || (D + F1) >= 10 || (D + G1) >= 10 || (D + H1) >= 10 || (D + I1) >= 10 || (D + J1) >= 10 || (D + K1) >= 10 || (D + L1) >= 10 || (D + M1) >= 10 || (D + N1) >= 10 || (D + O1) >= 10 || (D + P1) >= 10 || (D + Q1) >= 10 || (D + R1) >= 10 || (D + S1) >= 10 ||
                                        (E + F) >= 10 || (E + G) >= 10 || (E + H) >= 10 || (E + I) >= 10 || (E + J) >= 10 || (E + K) >= 10 || (E + L) >= 10 || (E + M) >= 10 || (E + N) >= 10 || (E + O) >= 10 || (E + P) >= 10 || (E + Q) >= 10 || (E + R) >= 10 || (E + S) >= 10 || (E + A1) >= 10 || (E + B1) >= 10 || (E + C1) >= 10 || (E + D1) >= 10 || (E + E1) >= 10 || (E + F1) >= 10 || (E + G1) >= 10 || (E + H1) >= 10 || (E + I1) >= 10 || (E + J1) >= 10 || (E + K1) >= 10 || (E + L1) >= 10 || (E + M1) >= 10 || (E + N1) >= 10 || (E + O1) >= 10 || (E + P1) >= 10 || (E + Q1) >= 10 || (E + R1) >= 10 || (E + S1) >= 10 ||
                                        (F + G) >= 10 || (F + H) >= 10 || (F + I) >= 10 || (F + J) >= 10 || (F + K) >= 10 || (F + L) >= 10 || (F + M) >= 10 || (F + N) >= 10 || (F + O) >= 10 || (F + P) >= 10 || (F + Q) >= 10 || (F + R) >= 10 || (F + S) >= 10 || (F + A1) >= 10 || (F + B1) >= 10 || (F + C1) >= 10 || (F + D1) >= 10 || (F + E1) >= 10 || (F + F1) >= 10 || (F + G1) >= 10 || (F + H1) >= 10 || (F + I1) >= 10 || (F + J1) >= 10 || (F + K1) >= 10 || (F + L1) >= 10 || (F + M1) >= 10 || (F + N1) >= 10 || (F + O1) >= 10 || (F + P1) >= 10 || (F + Q1) >= 10 || (F + R1) >= 10 || (F + S1) >= 10 ||
                                        (G + H) >= 10 || (G + I) >= 10 || (G + J) >= 10 || (G + K) >= 10 || (G + L) >= 10 || (G + M) >= 10 || (G + N) >= 10 || (G + O) >= 10 || (G + P) >= 10 || (G + Q) >= 10 || (G + R) >= 10 || (G + S) >= 10 || (G + A1) >= 10 || (G + B1) >= 10 || (G + C1) >= 10 || (G + D1) >= 10 || (G + E1) >= 10 || (G + F1) >= 10 || (G + G1) >= 10 || (G + H1) >= 10 || (G + I1) >= 10 || (G + J1) >= 10 || (G + K1) >= 10 || (G + L1) >= 10 || (G + M1) >= 10 || (G + N1) >= 10 || (G + O1) >= 10 || (G + P1) >= 10 || (G + Q1) >= 10 || (G + R1) >= 10 || (G + S1) >= 10 ||
                                        (H + I) >= 10 || (H + J) >= 10 || (H + K) >= 10 || (H + L) >= 10 || (H + M) >= 10 || (H + N) >= 10 || (H + O) >= 10 || (H + P) >= 10 || (H + Q) >= 10 || (H + R) >= 10 || (H + S) >= 10 || (H + A1) >= 10 || (H + B1) >= 10 || (H + C1) >= 10 || (H + D1) >= 10 || (H + E1) >= 10 || (H + F1) >= 10 || (H + G1) >= 10 || (H + H1) >= 10 || (H + I1) >= 10 || (H + J1) >= 10 || (H + K1) >= 10 || (H + L1) >= 10 || (H + M1) >= 10 || (H + N1) >= 10 || (H + O1) >= 10 || (H + P1) >= 10 || (H + Q1) >= 10 || (H + R1) >= 10 || (H + S1) >= 10 ||
                                        (I + J) >= 10 || (I + K) >= 10 || (I + L) >= 10 || (I + M) >= 10 || (I + N) >= 10 || (I + O) >= 10 || (I + P) >= 10 || (I + Q) >= 10 || (I + R) >= 10 || (I + S) >= 10 || (I + A1) >= 10 || (I + B1) >= 10 || (I + C1) >= 10 || (I + D1) >= 10 || (I + E1) >= 10 || (I + F1) >= 10 || (I + G1) >= 10 || (I + H1) >= 10 || (I + I1) >= 10 || (I + J1) >= 10 || (I + K1) >= 10 || (I + L1) >= 10 || (I + M1) >= 10 || (I + N1) >= 10 || (I + O1) >= 10 || (I + P1) >= 10 || (I + Q1) >= 10 || (I + R1) >= 10 || (I + S1) >= 10 ||
                                        (J + K) >= 10 || (J + L) >= 10 || (J + M) >= 10 || (J + N) >= 10 || (J + O) >= 10 || (J + P) >= 10 || (J + Q) >= 10 || (J + R) >= 10 || (J + S) >= 10 || (J + A1) >= 10 || (J + B1) >= 10 || (J + C1) >= 10 || (J + D1) >= 10 || (J + E1) >= 10 || (J + F1) >= 10 || (J + G1) >= 10 || (J + H1) >= 10 || (J + I1) >= 10 || (J + J1) >= 10 || (J + K1) >= 10 || (J + L1) >= 10 || (J + M1) >= 10 || (J + N1) >= 10 || (J + O1) >= 10 || (J + P1) >= 10 || (J + Q1) >= 10 || (J + R1) >= 10 || (J + S1) >= 10 ||
                                        (K + L) >= 10 || (K + M) >= 10 || (K + N) >= 10 || (K + O) >= 10 || (K + P) >= 10 || (K + Q) >= 10 || (K + R) >= 10 || (K + S) >= 10 || (K + A1) >= 10 || (K + B1) >= 10 || (K + C1) >= 10 || (K + D1) >= 10 || (K + E1) >= 10 || (K + F1) >= 10 || (K + G1) >= 10 || (K + H1) >= 10 || (K + I1) >= 10 || (K + J1) >= 10 || (K + K1) >= 10 || (K + L1) >= 10 || (K + M1) >= 10 || (K + N1) >= 10 || (K + O1) >= 10 || (K + P1) >= 10 || (K + Q1) >= 10 || (K + R1) >= 10 || (K + S1) >= 10 ||
                                        (L + M) >= 10 || (L + N) >= 10 || (L + O) >= 10 || (L + P) >= 10 || (L + Q) >= 10 || (L + R) >= 10 || (L + S) >= 10 || (L + A1) >= 10 || (L + B1) >= 10 || (L + C1) >= 10 || (L + D1) >= 10 || (L + E1) >= 10 || (L + F1) >= 10 || (L + G1) >= 10 || (L + H1) >= 10 || (L + I1) >= 10 || (L + J1) >= 10 || (L + K1) >= 10 || (L + L1) >= 10 || (L + M1) >= 10 || (L + N1) >= 10 || (L + O1) >= 10 || (L + P1) >= 10 || (L + Q1) >= 10 || (L + R1) >= 10 || (L + S1) >= 10 ||
                                        (M + N) >= 10 || (M + O) >= 10 || (M + P) >= 10 || (M + Q) >= 10 || (M + R) >= 10 || (M + S) >= 10 || (M + A1) >= 10 || (M + B1) >= 10 || (M + C1) >= 10 || (M + D1) >= 10 || (M + E1) >= 10 || (M + F1) >= 10 || (M + G1) >= 10 || (M + H1) >= 10 || (M + I1) >= 10 || (M + J1) >= 10 || (M + K1) >= 10 || (M + L1) >= 10 || (M + M1) >= 10 || (M + N1) >= 10 || (M + O1) >= 10 || (M + P1) >= 10 || (M + Q1) >= 10 || (M + R1) >= 10 || (M + S1) >= 10 ||
                                        (N + O) >= 10 || (N + P) >= 10 || (N + Q) >= 10 || (N + R) >= 10 || (N + S) >= 10 || (N + A1) >= 10 || (N + B1) >= 10 || (N + C1) >= 10 || (N + D1) >= 10 || (N + E1) >= 10 || (N + F1) >= 10 || (N + G1) >= 10 || (N + H1) >= 10 || (N + I1) >= 10 || (N + J1) >= 10 || (N + K1) >= 10 || (N + L1) >= 10 || (N + M1) >= 10 || (N + N1) >= 10 || (N + O1) >= 10 || (N + P1) >= 10 || (N + Q1) >= 10 || (N + R1) >= 10 || (N + S1) >= 10 ||
                                        (O + P) >= 10 || (O + Q) >= 10 || (O + R) >= 10 || (O + S) >= 10 || (O + A1) >= 10 || (O + B1) >= 10 || (O + C1) >= 10 || (O + D1) >= 10 || (O + E1) >= 10 || (O + F1) >= 10 || (O + G1) >= 10 || (O + H1) >= 10 || (O + I1) >= 10 || (O + J1) >= 10 || (O + K1) >= 10 || (O + L1) >= 10 || (O + M1) >= 10 || (O + N1) >= 10 || (O + O1) >= 10 || (O + P1) >= 10 || (O + Q1) >= 10 || (O + R1) >= 10 || (O + S1) >= 10 ||
                                        (P + Q) >= 10 || (P + R) >= 10 || (P + S) >= 10 || (P + A1) >= 10 || (P + B1) >= 10 || (P + C1) >= 10 || (P + D1) >= 10 || (P + E1) >= 10 || (P + F1) >= 10 || (P + G1) >= 10 || (P + H1) >= 10 || (P + I1) >= 10 || (P + J1) >= 10 || (P + K1) >= 10 || (P + L1) >= 10 || (P + M1) >= 10 || (P + N1) >= 10 || (P + O1) >= 10 || (P + P1) >= 10 || (P + Q1) >= 10 || (P + R1) >= 10 || (P + S1) >= 10 ||
                                        (Q + R) >= 10 || (Q + S) >= 10 || (Q + A1) >= 10 || (Q + B1) >= 10 || (Q + C1) >= 10 || (Q + D1) >= 10 || (Q + E1) >= 10 || (Q + F1) >= 10 || (Q + G1) >= 10 || (Q + H1) >= 10 || (Q + I1) >= 10 || (Q + J1) >= 10 || (Q + K1) >= 10 || (Q + L1) >= 10 || (Q + M1) >= 10 || (Q + N1) >= 10 || (Q + O1) >= 10 || (Q + P1) >= 10 || (Q + Q1) >= 10 || (Q + R1) >= 10 || (Q + S1) >= 10 ||
                                        (R + S) >= 10 || (R + A1) >= 10 || (R + B1) >= 10 || (R + C1) >= 10 || (R + D1) >= 10 || (R + E1) >= 10 || (R + F1) >= 10 || (R + G1) >= 10 || (R + H1) >= 10 || (R + I1) >= 10 || (R + J1) >= 10 || (R + K1) >= 10 || (R + L1) >= 10 || (R + M1) >= 10 || (R + N1) >= 10 || (R + O1) >= 10 || (R + P1) >= 10 || (R + Q1) >= 10 || (R + R1) >= 10 || (R + S1) >= 10 ||
                                        (S + A1) >= 10 || (S + B1) >= 10 || (S + C1) >= 10 || (S + D1) >= 10 || (S + E1) >= 10 || (S + F1) >= 10 || (S + G1) >= 10 || (S + H1) >= 10 || (S + I1) >= 10 || (S + J1) >= 10 || (S + K1) >= 10 || (S + L1) >= 10 || (S + M1) >= 10 || (S + N1) >= 10 || (S + O1) >= 10 || (S + P1) >= 10 || (S + Q1) >= 10 || (S + R1) >= 10 || (S + S1) >= 10 ||
                                        (A1 + B1) >= 10 || (A1 + C1) >= 10 || (A1 + D1) >= 10 || (A1 + E1) >= 10 || (A1 + F1) >= 10 || (A1 + G1) >= 10 || (A1 + H1) >= 10 || (A1 + I1) >= 10 || (A1 + J1) >= 10 || (A1 + K1) >= 10 || (A1 + L1) >= 10 || (A1 + M1) >= 10 || (A1 + N1) >= 10 || (A1 + O1) >= 10 || (A1 + P1) >= 10 || (A1 + Q1) >= 10 || (A1 + R1) >= 10 || (A1 + S1) >= 10 ||
                                        (B1 + C1) >= 10 || (B1 + D1) >= 10 || (B1 + E1) >= 10 || (B1 + F1) >= 10 || (B1 + G1) >= 10 || (B1 + H1) >= 10 || (B1 + I1) >= 10 || (B1 + J1) >= 10 || (B1 + K1) >= 10 || (B1 + L1) >= 10 || (B1 + M1) >= 10 || (B1 + N1) >= 10 || (B1 + O1) >= 10 || (B1 + P1) >= 10 || (B1 + Q1) >= 10 || (B1 + R1) >= 10 || (B1 + S1) >= 10 ||
                                        (C1 + D1) >= 10 || (C1 + E1) >= 10 || (C1 + F1) >= 10 || (C1 + G1) >= 10 || (C1 + H1) >= 10 || (C1 + I1) >= 10 || (C1 + J1) >= 10 || (C1 + K1) >= 10 || (C1 + L1) >= 10 || (C1 + M1) >= 10 || (C1 + N1) >= 10 || (C1 + O1) >= 10 || (C1 + P1) >= 10 || (C1 + Q1) >= 10 || (C1 + R1) >= 10 || (C1 + S1) >= 10 ||
                                        (D1 + E1) >= 10 || (D1 + F1) >= 10 || (D1 + G1) >= 10 || (D1 + H1) >= 10 || (D1 + I1) >= 10 || (D1 + J1) >= 10 || (D1 + K1) >= 10 || (D1 + L1) >= 10 || (D1 + M1) >= 10 || (D1 + N1) >= 10 || (D1 + O1) >= 10 || (D1 + P1) >= 10 || (D1 + Q1) >= 10 || (D1 + R1) >= 10 || (D1 + S1) >= 10 ||
                                        (E1 + F1) >= 10 || (E1 + G1) >= 10 || (E1 + H1) >= 10 || (E1 + I1) >= 10 || (E1 + J1) >= 10 || (E1 + K1) >= 10 || (E1 + L1) >= 10 || (E1 + M1) >= 10 || (E1 + N1) >= 10 || (E1 + O1) >= 10 || (E1 + P1) >= 10 || (E1 + Q1) >= 10 || (E1 + R1) >= 10 || (E1 + S1) >= 10 ||
                                        (F1 + G1) >= 10 || (F1 + H1) >= 10 || (F1 + I1) >= 10 || (F1 + J1) >= 10 || (F1 + K1) >= 10 || (F1 + L1) >= 10 || (F1 + M1) >= 10 || (F1 + N1) >= 10 || (F1 + O1) >= 10 || (F1 + P1) >= 10 || (F1 + Q1) >= 10 || (F1 + R1) >= 10 || (F1 + S1) >= 10 ||
                                        (G1 + H1) >= 10 || (G1 + I1) >= 10 || (G1 + J1) >= 10 || (G1 + K1) >= 10 || (G1 + L1) >= 10 || (G1 + M1) >= 10 || (G1 + N1) >= 10 || (G1 + O1) >= 10 || (G1 + P1) >= 10 || (G1 + Q1) >= 10 || (G1 + R1) >= 10 || (G1 + S1) >= 10 ||
                                        (H1 + I1) >= 10 || (H1 + J1) >= 10 || (H1 + K1) >= 10 || (H1 + L1) >= 10 || (H1 + M1) >= 10 || (H1 + N1) >= 10 || (H1 + O1) >= 10 || (H1 + P1) >= 10 || (H1 + Q1) >= 10 || (H1 + R1) >= 10 || (H1 + S1) >= 10 ||
                                        (I1 + J1) >= 10 || (I1 + K1) >= 10 || (I1 + L1) >= 10 || (I1 + M1) >= 10 || (I1 + N1) >= 10 || (I1 + O1) >= 10 || (I1 + P1) >= 10 || (I1 + Q1) >= 10 || (I1 + R1) >= 10 || (I1 + S1) >= 10 ||
                                        (J1 + K1) >= 10 || (J1 + L1) >= 10 || (J1 + M1) >= 10 || (J1 + N1) >= 10 || (J1 + O1) >= 10 || (J1 + P1) >= 10 || (J1 + Q1) >= 10 || (J1 + R1) >= 10 || (J1 + S1) >= 10 ||
                                        (K1 + L1) >= 10 || (K1 + M1) >= 10 || (K1 + N1) >= 10 || (K1 + O1) >= 10 || (K1 + P1) >= 10 || (K1 + Q1) >= 10 || (K1 + R1) >= 10 || (K1 + S1) >= 10 ||
                                        (L1 + M1) >= 10 || (L1 + N1) >= 10 || (L1 + O1) >= 10 || (L1 + P1) >= 10 || (L1 + Q1) >= 10 || (L1 + R1) >= 10 || (L1 + S1) >= 10 ||
                                        (M1 + N1) >= 10 || (M1 + O1) >= 10 || (M1 + P1) >= 10 || (M1 + Q1) >= 10 || (M1 + R1) >= 10 || (M1 + S1) >= 10 ||
                                        (N1 + O1) >= 10 || (N1 + P1) >= 10 || (N1 + Q1) >= 10 || (N1 + R1) >= 10 || (N1 + S1) >= 10 ||
                                        (O1 + P1) >= 10 || (O1 + Q1) >= 10 || (O1 + R1) >= 10 || (O1 + S1) >= 10 ||
                                        (P1 + Q1) >= 10 || (P1 + R1) >= 10 || (P1 + S1) >= 10 ||
                                        (Q1 + R1) >= 10 || (Q1 + S1) >= 10 ||
                                        (R1 + S1) >= 10)
                                    {

                                    }
                                    else
                                    {

                                        obj.NC3 = "Minimum total rest comprises more than 2 period";
                                        ncbms = ncbms + 1;

                                        int bc = Convert.ToInt32(today[y1]);
                                        foreach (var item in DynamicGridBorders)
                                        {
                                            if (item.Name == "BP" + bc)
                                            {
                                                var ss1 = item.Background;
                                                if (((SolidColorBrush)ss1).Color.ToString() == colorValue1)
                                                {
                                                    item.Background = new SolidColorBrush(Colors.Red);
                                                }

                                                break;
                                            }

                                        }
                                    }
                                }

                            }

                            //10/2 next Condition compleated....

                            int count7 = 0;
                            count7 = index1 + 336;
                            double totalbms1 = 0;
                            ab10rest1.Clear();
                            List<double> list7 = new List<double>();
                            list7.Clear();


                            string[] numsbms1 = st4bms7.Skip(index1).Take(336).ToArray();

                            int norm13 = 0;
                            norm13 = numsbms1.Length;
                            for (long i = 0; i < norm13; i++)
                            {

                                if (Convert.ToInt32(numsbms1[i]) == 0)
                                {
                                    totalbms1 += 0.5;
                                }
                                else if (Convert.ToInt32(numsbms1[i]) == 1)
                                {
                                    if (totalbms1 != 0)
                                    {
                                        list7.Add(totalbms1);
                                        totalbms1 = 0;
                                    }
                                }
                                if (i == numsbms1.Length - 1)
                                {
                                    if (totalbms1 != 0)
                                    {
                                        list7.Add(totalbms1);
                                        totalbms1 = 0;
                                    }
                                }

                            }

                            if (list7.Count() == 0)
                                list7.Add(0);
                            double hrs77Count = list7.Sum();

                            ab10rest1.Add(hrs77Count);
                            ab10rest1.Sort();

                            obj.RestHour7day = Convert.ToDecimal(ab10rest1[0].ToString());
                            obj.RestHourAny7day = ab10rest1[0].ToString();

                            //_CommonData1.RestHour7day = Convert.ToDecimal(ab10rest1[0].ToString());
                            //RaisePropertyChanged("CommonData1");


                            int hrs77 = (int)(DefineRules.Rule77Hrs);
                            if (hrs77Count < hrs77)
                            {
                                obj.NC4 = "Less than 77 hours rest in 7 day period";


                                int bc = Convert.ToInt32(today[y1]);
                                foreach (var item in DynamicGridBorders)
                                {
                                    if (item.Name == "BP" + bc)
                                    {

                                        var ss1 = item.Background;
                                        if (((SolidColorBrush)ss1).Color.ToString() == colorValue1)
                                        {
                                            item.Background = new SolidColorBrush(Colors.Red);
                                        }

                                        break;
                                    }

                                }



                            } //77hrs Condition compleated....


                            //OPA 90 Conditions..................

                            //// OPA start....

                            if (b11)
                            {
                                bool bmsmopa = Convert.ToBoolean(Convert.ToDateTime(startdateop) >= Convert.ToDateTime(opdastart) && Convert.ToDateTime(enddateop) <= Convert.ToDateTime(opastop)) ? true : false;
                                if (bmsmopa || chkOPAforallday)
                                {

                                    var hrsOPa1 = lista.Sum();
                                    if (hrsOPa1 < 9)
                                    {
                                        obj.NC15 = "OPA NC : No more than 15 hrs work in any 24 hrs period";


                                        int bc = Convert.ToInt32(today[y1]);

                                        foreach (var item in DynamicGridBorders)
                                        {
                                            if (item.Name == "BP" + bc)
                                            {

                                                var ss1 = item.Background;
                                                if (((SolidColorBrush)ss1).Color.ToString() == colorValue1)
                                                {
                                                    item.Background = new SolidColorBrush(Colors.Red);
                                                }

                                                break;
                                            }
                                        }
                                    }//opa 9 hrs condition compleated

                                    totalbms1 = 0;
                                    List<double> listopa3 = new List<double>();
                                    listopa3.Clear();

                                    string[] numsOPA3 = ST4OPA3.Skip(index1).Take(144).ToArray();

                                    int normOPA3 = 0;
                                    normOPA3 = numsOPA3.Length;
                                    for (long i = 0; i < normOPA3; i++)
                                    {

                                        if (Convert.ToInt32(numsOPA3[i]) == 0)
                                        {
                                            totalbms1 += 0.5;
                                        }
                                        else if (Convert.ToInt32(numsOPA3[i]) == 1)
                                        {
                                            if (totalbms1 != 0)
                                            {
                                                listopa3.Add(totalbms1);
                                                totalbms1 = 0;
                                            }
                                        }
                                        if (i == numsOPA3.Length - 1)
                                        {
                                            if (totalbms1 != 0)
                                            {
                                                listopa3.Add(totalbms1);
                                                totalbms1 = 0;
                                            }
                                        }

                                    }

                                    if (listopa3.Count() == 0)
                                        listopa3.Add(0);
                                    double Opa3Count = listopa3.Sum();

                                    if (Opa3Count < 36)
                                    {
                                        obj.NC16 = "OPA NC : No more than 36 hrs work in any 72 hrs period";

                                        int bc = Convert.ToInt32(today[y1]);

                                        foreach (var item in DynamicGridBorders)
                                        {
                                            if (item.Name == "BP" + bc)
                                            {
                                                var ss1 = item.Background;
                                                if (((SolidColorBrush)ss1).Color.ToString() == colorValue1)
                                                {
                                                    item.Background = new SolidColorBrush(Colors.Red);
                                                }

                                                break;
                                            }

                                        }

                                    }
                                }

                            }//End OPA





                        }//End



                    } //End For loop..


                    //...End New implementation.......


                }
                else
                {
                    GetbackhoursValuesPlanner(obj, WorkHoursList, CheckIDL);


                    obj.RestHours = 24.0m;
                    obj.RestHourAny24 = "24.0";
                    obj.NC1 = null;
                    obj.NC2 = null;
                    obj.NC3 = null;
                    obj.NC4 = null;
                    obj.NC5 = null; //F
                    obj.NC6 = null; //F
                    obj.NC7 = null; //F
                    obj.NC8 = null; //ncc1  F
                    obj.NC9 = null; //mnc1
                    obj.NC10 = null; //mnc2
                    obj.NC11 = null; //mnc3
                    obj.NC12 = null; //mnc4
                    obj.NC13 = null; //mnc5
                    obj.NC14 = null; //mnc6
                    obj.NC15 = null; //opnc1
                    obj.NC16 = null; //opnc1
                    obj.NC17 = null; //mnc7
                    obj.NC18 = null; //mnc8
                    obj.YNC1 = null;
                    obj.YNC2 = null;
                    obj.YNC3 = null;
                    obj.YNC4 = null;
                    obj.YNC5 = null;
                    obj.YNC6 = null;
                    obj.YNC7 = null;
                    obj.YNC8 = null;
                    obj.NonConfirmities = string.Empty;
                    obj.NonConfirmities1 = string.Empty;

                }




                SaveNC1Planner(obj);



            }

            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }






    }
}
