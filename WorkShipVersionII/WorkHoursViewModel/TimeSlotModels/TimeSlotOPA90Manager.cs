using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace WorkShipVersionII.WorkHoursViewModel.TimeSlotModels
{
    public class TimeSlotOPA90Manager : CommanConditionsTimeSlot, ITimeSlotSettingManager
    {
        private ShipmentContaxt sc;
        public TimeSlotOPA90Manager()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }
        }
        public void TimeSlotPageSettingSaveMethod(DateTime startdate, object enddate, BackgroundWorker Workersave, Grid DynamicGrid)
        {
            int ib = 2;
            Workersave.ReportProgress(2);
            try
            {

                List<WorkHoursClass> WorkhoursListmain;
                List<WorkHoursPlannerClass> WorkhoursListmainPlanner;

                List<CrewDetailClass> CrewList;
                List<InternationalClass> CheckIDL;
                using (ShipmentContaxt sc1 = new ShipmentContaxt())
                {
                    WorkhoursListmain = sc1.WorkHourss.ToList();
                    WorkhoursListmainPlanner = sc1.WorkHourPlanners.ToList();
                    CrewList = sc1.CrewDetails.ToList();
                    CheckIDL = sc1.Internationals.ToList();
                }

                //.....Planner.........

                var WorkhoursListP = WorkhoursListmainPlanner.Select(x => x.UserName).Distinct().ToList();
                int conP = WorkhoursListmainPlanner.Where(x => (x.dates.Date >= startdate.Date && x.dates.Date <= Convert.ToDateTime(enddate).Date)).Count();
                int conP1 = conP + 100;

                //.....End Planner


                var WorkhoursList = WorkhoursListmain.Select(x => x.UserName).Distinct().ToList();
                int con = WorkhoursListmain.Where(x => (x.dates.Date >= startdate.Date && x.dates.Date <= Convert.ToDateTime(enddate).Date)).Count();
                int con1 = con + 100;

                con1 = con1 + conP1;


                foreach (var li1 in WorkhoursList)
                {

                    //ib = ib + 1;
                    //Workersave.ReportProgress(ib * (100 / con));

                    var data = WorkhoursListmain.Where(x => x.UserName == li1 && (x.dates.Date >= startdate.Date && x.dates.Date <= Convert.ToDateTime(enddate).Date)).ToList();
                    foreach (var obj in data)
                    {

                        ib = ib + 1;
                        Workersave.ReportProgress(ib * (con1 / con));

                        var checkuser1 = CrewList.Where(x => x.UserName == li1).FirstOrDefault();
                        if (checkuser1 != null)
                        {
                            bool chkyoungs = Convert.ToBoolean(checkuser1.chkyoungs);
                            if (chkyoungs == true)
                            {

                                DateTime d1 = Convert.ToDateTime(checkuser1.DOB);
                                DateTime d2 = obj.dates; //DateTime.Now;
                                if (CheckDateYoung(d1, d2))
                                {
                                }
                                else
                                {
                                    //youngseafere has become above 18 years...


                                    if (Convert.ToBoolean(obj.opa.ToString()) == false)
                                    {

                                        obj.opa = true;

                                        obj.NC15 = null;
                                        obj.NC16 = null;

                                        string label42 = obj.hrs;

                                        label42 = Morethan1hrsYoung(label42);
                                        var checkusers = WorkhoursListmain.Select(s => new { s.UserName, s.dates, s.hrs, s.Colors, s.WRID, s.Cellcount }).ToList();

                                        string lbl1 = string.Empty;
                                        int b = 0;

                                        string[] s2 = null;
                                        string label42bmNewVal = checkbackhours(label42, obj.dates, obj.Cellcount, obj.IDL, WorkhoursListmain, CheckIDL);
                                        s2 = label42bmNewVal.Substring(0, label42bmNewVal.Length - 1).Split(',');



                                        double total = 0;
                                        int index = 0;
                                        int tblcount = s2.Length;
                                        for (int s = 0; s < tblcount; s++)
                                        {

                                            if (s2[s] == "1")
                                            {
                                                index = s;

                                                if (total == 0)
                                                {
                                                    total = s;
                                                    b = s;
                                                    lbl1 += b.ToString() + ",";
                                                    total = s + 1;

                                                }

                                            }
                                            else if (s2[s] == "0")
                                            {
                                                if (total != 0)
                                                {
                                                    lbl1 += index.ToString() + ",";

                                                    index = 0;
                                                    total = 0;
                                                }

                                            }
                                            if (s == s2.Length - 1)
                                            {
                                                if (total != 0)
                                                {
                                                    lbl1 += index.ToString() + ",";
                                                    index = 0;
                                                    total = 0;
                                                }
                                            }

                                        }
                                        //..............

                                        string[] newhr = null;
                                        newhr = label42.TrimEnd(',').Split(',');



                                        if (!string.IsNullOrEmpty(lbl1))
                                        {

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
                                                    if (countbms + cellcouns <= 96)
                                                    {
                                                        cellcouns += checkuser.Cellcount;

                                                        string startEndbbs = checkuser.hrs;
                                                        label42OneDay = startEndbbs + label42OneDay;
                                                    }
                                                    else
                                                    {
                                                        int countbms1 = checkuser.Cellcount;
                                                        int finalCellcount = 96 - cellcouns;
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
                                                    cellcouns += 96;
                                                    string startEndbbs = string.Empty;
                                                    for (int i = 0; i < 96; i++)
                                                    {
                                                        startEndbbs += "0" + ",";
                                                    }
                                                    label42OneDay = startEndbbs + label42OneDay;
                                                }

                                                if (cellcouns >= 96)
                                                    break;

                                                iDatebm++;
                                            }


                                            string[] st22 = null;
                                            st22 = label42OneDay.TrimEnd(',').Split(',');
                                            conc = st22.Concat(newhr).ToArray();


                                            //...New Implementation for an hour....
                                            string bmschange7bm = string.Join(",", conc);
                                            bmschange7bm = bmschange7bm + ',';
                                            string bmschange77bm = Morethan1hrsYoung(bmschange7bm);

                                            conc = bmschange77bm.Split(',');
                                            //...End New Implementation for an hour....


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
                                                    if (countbms + cellcouns <= 288)
                                                    {
                                                        cellcouns += checkuser.Cellcount;

                                                        string startEndbbs = checkuser.hrs;
                                                        label42OneDay = startEndbbs + label42OneDay;
                                                    }
                                                    else
                                                    {
                                                        int countbms1 = checkuser.Cellcount;
                                                        int finalCellcount = 288 - cellcouns;
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
                                                    cellcouns += 96;
                                                    string startEndbbs = string.Empty;
                                                    for (int i = 0; i < 96; i++)
                                                    {
                                                        startEndbbs += "0" + ",";
                                                    }
                                                    label42OneDay = startEndbbs + label42OneDay;

                                                }

                                                if (cellcouns >= 288)
                                                    break;

                                                iDatebm++;
                                            }

                                            string[] stopa3 = null;
                                            stopa3 = label42OneDay.TrimEnd(',').Split(',');
                                            ST4OPA3 = stopa3.Concat(newhr).ToArray();


                                            //...New Implementation for an hour....
                                            string change3opa = string.Join(",", ST4OPA3);
                                            change3opa = change3opa + ',';
                                            string change33opa = Morethan1hrsYoung(change3opa);
                                            ST4OPA3 = change33opa.Split(',');
                                            //...End New Implementation for an hour...

                                            string[] today = null;
                                            today = lbl1.TrimEnd(',').Split(new[] { ',' });
                                            int lenth = today.Length;

                                            for (int y1 = 0; y1 < lenth; y1++)
                                            {

                                                int index1 = 0;
                                                index1 = Convert.ToInt32(today[y1]) + 1;

                                                double totals = 0;
                                                List<double> lista = new List<double>();
                                                lista.Clear();



                                                string[] nums = conc.Skip(index1).Take(96).ToArray();

                                                int norm9 = 0;
                                                norm9 = nums.Length;
                                                for (long i = 0; i < norm9; i++)
                                                {

                                                    if (Convert.ToInt32(nums[i]) == 0)
                                                    {
                                                        totals += 0.25;
                                                    }
                                                    else if (Convert.ToInt32(nums[i]) == 1)
                                                    {
                                                        if (totals != 0)
                                                        {
                                                            lista.Add(totals);
                                                            totals = 0;
                                                        }
                                                    }
                                                    if (i == nums.Length - 1)
                                                    {
                                                        if (totals != 0)
                                                        {
                                                            lista.Add(totals);
                                                            totals = 0;
                                                        }
                                                    }

                                                }

                                                if (lista.Count() == 0)
                                                    lista.Add(0);
                                                double abc22 = lista.Sum();

                                                var hrsOPa1 = lista.Sum();
                                                if (hrsOPa1 < 9)
                                                {
                                                    obj.NC15 = "OPA NC : No more than 15 hrs work in any 24 hrs period";


                                                }//opa 9 hrs condition compleated


                                                double totalbms1 = 0;
                                                List<double> listopa3 = new List<double>();
                                                listopa3.Clear();

                                                string[] numsOPA3 = ST4OPA3.Skip(index1).Take(288).ToArray();

                                                int normOPA3 = 0;
                                                normOPA3 = numsOPA3.Length;
                                                for (long i = 0; i < normOPA3; i++)
                                                {

                                                    if (Convert.ToInt32(numsOPA3[i]) == 0)
                                                    {
                                                        totalbms1 += 0.25;
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


                                                }


                                            }

                                            //
                                            SaveNC1(obj);

                                            if (!string.IsNullOrEmpty(obj.NC15) || !string.IsNullOrEmpty(obj.NC16))
                                            {
                                                obj.CrewNC = true;
                                                obj.AdminAlarm = true;
                                                obj.MasterAlarm = true;
                                                obj.HODAlarm = true;
                                                obj.MasterCrewNC = true;
                                                obj.HODCrewNC = true;
                                            }

                                            obj.opa = true;

                                            var local = sc.Set<WorkHoursClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                            if (local != null)
                                            {
                                                sc.Entry(local).State = EntityState.Detached;
                                            }

                                            sc.WorkHourss.Attach(obj);
                                            sc.Entry(obj).Property(x => x.NC15).IsModified = true;
                                            sc.Entry(obj).Property(x => x.NC16).IsModified = true;
                                            sc.Entry(obj).Property(x => x.opa).IsModified = true;
                                            sc.Entry(obj).Property(x => x.CrewNC).IsModified = true;
                                            sc.Entry(obj).Property(x => x.AdminAlarm).IsModified = true;
                                            sc.Entry(obj).Property(x => x.MasterAlarm).IsModified = true;
                                            sc.Entry(obj).Property(x => x.HODAlarm).IsModified = true;
                                            sc.Entry(obj).Property(x => x.MasterCrewNC).IsModified = true;
                                            sc.Entry(obj).Property(x => x.MasterCrewNC).IsModified = true;
                                            sc.Entry(obj).Property(x => x.NonConfirmities).IsModified = true;
                                            sc.Entry(obj).Property(x => x.NonConfirmities1).IsModified = true;
                                            sc.SaveChanges();


                                        }
                                        else
                                        {
                                            obj.opa = true;

                                            var local = sc.Set<WorkHoursClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                            if (local != null)
                                            {
                                                sc.Entry(local).State = EntityState.Detached;
                                            }

                                            sc.WorkHourss.Attach(obj);
                                            sc.Entry(obj).Property(x => x.opa).IsModified = true;
                                            sc.SaveChanges();
                                        }



                                    }

                                }

                            }
                            else
                            {

                                // Non Young seaferer......


                                if (Convert.ToBoolean(obj.opa.ToString()) == false)
                                {

                                    obj.opa = true;

                                    obj.NC15 = null;
                                    obj.NC16 = null;


                                    string label42 = obj.hrs;

                                    label42 = Morethan1hrsNormal(label42);
                                    var checkusers = WorkhoursListmain.Select(s => new { s.UserName, s.dates, s.hrs, s.Colors, s.WRID, s.Cellcount }).ToList();

                                    string lbl1 = string.Empty;
                                    int b = 0;

                                    string[] s2 = null;
                                    string label42bmNewVal = checkbackhours(label42, obj.dates, obj.Cellcount, obj.IDL, WorkhoursListmain, CheckIDL);
                                    s2 = label42bmNewVal.Substring(0, label42bmNewVal.Length - 1).Split(',');



                                    double total = 0;
                                    int index = 0;
                                    int tblcount = s2.Length;
                                    for (int s = 0; s < tblcount; s++)
                                    {

                                        if (s2[s] == "1")
                                        {
                                            index = s;

                                            if (total == 0)
                                            {
                                                total = s;
                                                b = s;
                                                lbl1 += b.ToString() + ",";
                                                total = s + 1;

                                            }

                                        }
                                        else if (s2[s] == "0")
                                        {
                                            if (total != 0)
                                            {
                                                lbl1 += index.ToString() + ",";

                                                index = 0;
                                                total = 0;
                                            }

                                        }
                                        if (s == s2.Length - 1)
                                        {
                                            if (total != 0)
                                            {
                                                lbl1 += index.ToString() + ",";
                                                index = 0;
                                                total = 0;
                                            }
                                        }

                                    }
                                    //..............

                                    string[] newhr = null;
                                    newhr = label42.TrimEnd(',').Split(',');



                                    if (!string.IsNullOrEmpty(lbl1))
                                    {

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
                                        string change33opa = Morethan1hrsYoung(change3opa);
                                        ST4OPA3 = change33opa.Split(',');
                                        //...End New Implementation for an hour...

                                        string[] today = null;
                                        today = lbl1.TrimEnd(',').Split(new[] { ',' });
                                        int lenth = today.Length;

                                        for (int y1 = 0; y1 < lenth; y1++)
                                        {

                                            int index1 = 0;
                                            index1 = Convert.ToInt32(today[y1]) + 1;

                                            double totals = 0;
                                            List<double> lista = new List<double>();
                                            lista.Clear();



                                            string[] nums = conc.Skip(index1).Take(48).ToArray();

                                            int norm9 = 0;
                                            norm9 = nums.Length;
                                            for (long i = 0; i < norm9; i++)
                                            {

                                                if (Convert.ToInt32(nums[i]) == 0)
                                                {
                                                    totals += 0.5;
                                                }
                                                else if (Convert.ToInt32(nums[i]) == 1)
                                                {
                                                    if (totals != 0)
                                                    {
                                                        lista.Add(totals);
                                                        totals = 0;
                                                    }
                                                }
                                                if (i == nums.Length - 1)
                                                {
                                                    if (totals != 0)
                                                    {
                                                        lista.Add(totals);
                                                        totals = 0;
                                                    }
                                                }

                                            }

                                            if (lista.Count() == 0)
                                                lista.Add(0);
                                            double abc22 = lista.Sum();

                                            var hrsOPa1 = lista.Sum();
                                            if (hrsOPa1 < 9)
                                            {
                                                obj.NC15 = "OPA NC : No more than 15 hrs work in any 24 hrs period";


                                            }//opa 9 hrs condition compleated


                                            double totalbms1 = 0;
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


                                            }


                                        }

                                        //
                                        SaveNC1(obj);
                                        if (!string.IsNullOrEmpty(obj.NC15) || !string.IsNullOrEmpty(obj.NC16))
                                        {
                                            obj.CrewNC = true;
                                            obj.AdminAlarm = true;
                                            obj.MasterAlarm = true;
                                            obj.HODAlarm = true;
                                            obj.MasterCrewNC = true;
                                            obj.HODCrewNC = true;
                                        }

                                        obj.opa = true;


                                        var local = sc.Set<WorkHoursClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                        if (local != null)
                                        {
                                            sc.Entry(local).State = EntityState.Detached;
                                        }




                                        sc.WorkHourss.Attach(obj);
                                        sc.Entry(obj).Property(x => x.NC15).IsModified = true;
                                        sc.Entry(obj).Property(x => x.NC16).IsModified = true;
                                        sc.Entry(obj).Property(x => x.opa).IsModified = true;
                                        sc.Entry(obj).Property(x => x.CrewNC).IsModified = true;
                                        sc.Entry(obj).Property(x => x.AdminAlarm).IsModified = true;
                                        sc.Entry(obj).Property(x => x.MasterAlarm).IsModified = true;
                                        sc.Entry(obj).Property(x => x.HODAlarm).IsModified = true;
                                        sc.Entry(obj).Property(x => x.MasterCrewNC).IsModified = true;
                                        sc.Entry(obj).Property(x => x.MasterCrewNC).IsModified = true;
                                        sc.Entry(obj).Property(x => x.NonConfirmities).IsModified = true;
                                        sc.Entry(obj).Property(x => x.NonConfirmities1).IsModified = true;
                                        sc.SaveChanges();


                                    }
                                    else
                                    {
                                        obj.opa = true;

                                        var local = sc.Set<WorkHoursClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                        if (local != null)
                                        {
                                            sc.Entry(local).State = EntityState.Detached;
                                        }

                                        sc.WorkHourss.Attach(obj);
                                        sc.Entry(obj).Property(x => x.opa).IsModified = true;
                                        sc.SaveChanges();
                                    }




                                }



                            }




                        }

                    }

                }

                //.....Start Planner...........

                foreach (var li1 in WorkhoursListP)
                {
                    var data = WorkhoursListmainPlanner.Where(x => x.UserName == li1 && (x.dates.Date >= startdate.Date && x.dates.Date <= Convert.ToDateTime(enddate).Date)).ToList();
                    foreach (var obj in data)
                    {

                        ib = ib + 1;
                        Workersave.ReportProgress(ib * (con1 / con));

                        var checkuser1 = CrewList.Where(x => x.UserName == li1).FirstOrDefault();
                        if (checkuser1 != null)
                        {
                            bool chkyoungs = Convert.ToBoolean(checkuser1.chkyoungs);
                            if (chkyoungs == true)
                            {

                                DateTime d1 = Convert.ToDateTime(checkuser1.DOB);
                                DateTime d2 = obj.dates; //DateTime.Now;
                                if (CheckDateYoung(d1, d2))
                                {
                                }
                                else
                                {
                                    //youngseafere has become above 18 years...


                                    if (Convert.ToBoolean(obj.opa.ToString()) == false)
                                    {

                                        obj.opa = true;

                                        obj.NC15 = null;
                                        obj.NC16 = null;

                                        string label42 = obj.hrs;

                                        label42 = Morethan1hrsYoung(label42);
                                        var checkusers = WorkhoursListmain.Select(s => new { s.UserName, s.dates, s.hrs, s.Colors, s.WRID, s.Cellcount }).ToList();

                                        string lbl1 = string.Empty;
                                        int b = 0;

                                        string[] s2 = null;
                                        string label42bmNewVal = checkbackhours(label42, obj.dates, obj.Cellcount, obj.IDL, WorkhoursListmain, CheckIDL);
                                        s2 = label42bmNewVal.Substring(0, label42bmNewVal.Length - 1).Split(',');



                                        double total = 0;
                                        int index = 0;
                                        int tblcount = s2.Length;
                                        for (int s = 0; s < tblcount; s++)
                                        {

                                            if (s2[s] == "1")
                                            {
                                                index = s;

                                                if (total == 0)
                                                {
                                                    total = s;
                                                    b = s;
                                                    lbl1 += b.ToString() + ",";
                                                    total = s + 1;

                                                }

                                            }
                                            else if (s2[s] == "0")
                                            {
                                                if (total != 0)
                                                {
                                                    lbl1 += index.ToString() + ",";

                                                    index = 0;
                                                    total = 0;
                                                }

                                            }
                                            if (s == s2.Length - 1)
                                            {
                                                if (total != 0)
                                                {
                                                    lbl1 += index.ToString() + ",";
                                                    index = 0;
                                                    total = 0;
                                                }
                                            }

                                        }
                                        //..............

                                        string[] newhr = null;
                                        newhr = label42.TrimEnd(',').Split(',');



                                        if (!string.IsNullOrEmpty(lbl1))
                                        {

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
                                                    if (countbms + cellcouns <= 96)
                                                    {
                                                        cellcouns += checkuser.Cellcount;

                                                        string startEndbbs = checkuser.hrs;
                                                        label42OneDay = startEndbbs + label42OneDay;
                                                    }
                                                    else
                                                    {
                                                        int countbms1 = checkuser.Cellcount;
                                                        int finalCellcount = 96 - cellcouns;
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
                                                    cellcouns += 96;
                                                    string startEndbbs = string.Empty;
                                                    for (int i = 0; i < 96; i++)
                                                    {
                                                        startEndbbs += "0" + ",";
                                                    }
                                                    label42OneDay = startEndbbs + label42OneDay;
                                                }

                                                if (cellcouns >= 96)
                                                    break;

                                                iDatebm++;
                                            }


                                            string[] st22 = null;
                                            st22 = label42OneDay.TrimEnd(',').Split(',');
                                            conc = st22.Concat(newhr).ToArray();


                                            //...New Implementation for an hour....
                                            string bmschange7bm = string.Join(",", conc);
                                            bmschange7bm = bmschange7bm + ',';
                                            string bmschange77bm = Morethan1hrsYoung(bmschange7bm);

                                            conc = bmschange77bm.Split(',');
                                            //...End New Implementation for an hour....


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
                                                    if (countbms + cellcouns <= 288)
                                                    {
                                                        cellcouns += checkuser.Cellcount;

                                                        string startEndbbs = checkuser.hrs;
                                                        label42OneDay = startEndbbs + label42OneDay;
                                                    }
                                                    else
                                                    {
                                                        int countbms1 = checkuser.Cellcount;
                                                        int finalCellcount = 288 - cellcouns;
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
                                                    cellcouns += 96;
                                                    string startEndbbs = string.Empty;
                                                    for (int i = 0; i < 96; i++)
                                                    {
                                                        startEndbbs += "0" + ",";
                                                    }
                                                    label42OneDay = startEndbbs + label42OneDay;

                                                }

                                                if (cellcouns >= 288)
                                                    break;

                                                iDatebm++;
                                            }

                                            string[] stopa3 = null;
                                            stopa3 = label42OneDay.TrimEnd(',').Split(',');
                                            ST4OPA3 = stopa3.Concat(newhr).ToArray();


                                            //...New Implementation for an hour....
                                            string change3opa = string.Join(",", ST4OPA3);
                                            change3opa = change3opa + ',';
                                            string change33opa = Morethan1hrsYoung(change3opa);
                                            ST4OPA3 = change33opa.Split(',');
                                            //...End New Implementation for an hour...

                                            string[] today = null;
                                            today = lbl1.TrimEnd(',').Split(new[] { ',' });
                                            int lenth = today.Length;

                                            for (int y1 = 0; y1 < lenth; y1++)
                                            {

                                                int index1 = 0;
                                                index1 = Convert.ToInt32(today[y1]) + 1;

                                                double totals = 0;
                                                List<double> lista = new List<double>();
                                                lista.Clear();



                                                string[] nums = conc.Skip(index1).Take(96).ToArray();

                                                int norm9 = 0;
                                                norm9 = nums.Length;
                                                for (long i = 0; i < norm9; i++)
                                                {

                                                    if (Convert.ToInt32(nums[i]) == 0)
                                                    {
                                                        totals += 0.25;
                                                    }
                                                    else if (Convert.ToInt32(nums[i]) == 1)
                                                    {
                                                        if (totals != 0)
                                                        {
                                                            lista.Add(totals);
                                                            totals = 0;
                                                        }
                                                    }
                                                    if (i == nums.Length - 1)
                                                    {
                                                        if (totals != 0)
                                                        {
                                                            lista.Add(totals);
                                                            totals = 0;
                                                        }
                                                    }

                                                }

                                                if (lista.Count() == 0)
                                                    lista.Add(0);
                                                double abc22 = lista.Sum();

                                                var hrsOPa1 = lista.Sum();
                                                if (hrsOPa1 < 9)
                                                {
                                                    obj.NC15 = "OPA NC : No more than 15 hrs work in any 24 hrs period";


                                                }//opa 9 hrs condition compleated


                                                double totalbms1 = 0;
                                                List<double> listopa3 = new List<double>();
                                                listopa3.Clear();

                                                string[] numsOPA3 = ST4OPA3.Skip(index1).Take(288).ToArray();

                                                int normOPA3 = 0;
                                                normOPA3 = numsOPA3.Length;
                                                for (long i = 0; i < normOPA3; i++)
                                                {

                                                    if (Convert.ToInt32(numsOPA3[i]) == 0)
                                                    {
                                                        totalbms1 += 0.25;
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


                                                }


                                            }

                                            //
                                            SaveNC1Planner(obj);

                                            

                                            obj.opa = true;

                                            var local = sc.Set<WorkHoursPlannerClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                            if (local != null)
                                            {
                                                sc.Entry(local).State = EntityState.Detached;
                                            }

                                            sc.WorkHourPlanners.Attach(obj);
                                            sc.Entry(obj).Property(x => x.opa).IsModified = true;
                                            sc.Entry(obj).Property(x => x.NonConfirmities).IsModified = true;
                                            sc.Entry(obj).Property(x => x.NonConfirmities1).IsModified = true;
                                            sc.SaveChanges();


                                        }
                                        else
                                        {
                                            obj.opa = true;

                                            var local = sc.Set<WorkHoursPlannerClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                            if (local != null)
                                            {
                                                sc.Entry(local).State = EntityState.Detached;
                                            }

                                            sc.WorkHourPlanners.Attach(obj);
                                            sc.Entry(obj).Property(x => x.opa).IsModified = true;
                                            sc.SaveChanges();
                                        }



                                    }

                                }

                            }
                            else
                            {

                                // Non Young seaferer......


                                if (Convert.ToBoolean(obj.opa.ToString()) == false)
                                {

                                    obj.opa = true;

                                    obj.NC15 = null;
                                    obj.NC16 = null;


                                    string label42 = obj.hrs;

                                    label42 = Morethan1hrsNormal(label42);
                                    var checkusers = WorkhoursListmain.Select(s => new { s.UserName, s.dates, s.hrs, s.Colors, s.WRID, s.Cellcount }).ToList();

                                    string lbl1 = string.Empty;
                                    int b = 0;

                                    string[] s2 = null;
                                    string label42bmNewVal = checkbackhours(label42, obj.dates, obj.Cellcount, obj.IDL, WorkhoursListmain, CheckIDL);
                                    s2 = label42bmNewVal.Substring(0, label42bmNewVal.Length - 1).Split(',');



                                    double total = 0;
                                    int index = 0;
                                    int tblcount = s2.Length;
                                    for (int s = 0; s < tblcount; s++)
                                    {

                                        if (s2[s] == "1")
                                        {
                                            index = s;

                                            if (total == 0)
                                            {
                                                total = s;
                                                b = s;
                                                lbl1 += b.ToString() + ",";
                                                total = s + 1;

                                            }

                                        }
                                        else if (s2[s] == "0")
                                        {
                                            if (total != 0)
                                            {
                                                lbl1 += index.ToString() + ",";

                                                index = 0;
                                                total = 0;
                                            }

                                        }
                                        if (s == s2.Length - 1)
                                        {
                                            if (total != 0)
                                            {
                                                lbl1 += index.ToString() + ",";
                                                index = 0;
                                                total = 0;
                                            }
                                        }

                                    }
                                    //..............

                                    string[] newhr = null;
                                    newhr = label42.TrimEnd(',').Split(',');



                                    if (!string.IsNullOrEmpty(lbl1))
                                    {

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
                                        string change33opa = Morethan1hrsYoung(change3opa);
                                        ST4OPA3 = change33opa.Split(',');
                                        //...End New Implementation for an hour...

                                        string[] today = null;
                                        today = lbl1.TrimEnd(',').Split(new[] { ',' });
                                        int lenth = today.Length;

                                        for (int y1 = 0; y1 < lenth; y1++)
                                        {

                                            int index1 = 0;
                                            index1 = Convert.ToInt32(today[y1]) + 1;

                                            double totals = 0;
                                            List<double> lista = new List<double>();
                                            lista.Clear();



                                            string[] nums = conc.Skip(index1).Take(48).ToArray();

                                            int norm9 = 0;
                                            norm9 = nums.Length;
                                            for (long i = 0; i < norm9; i++)
                                            {

                                                if (Convert.ToInt32(nums[i]) == 0)
                                                {
                                                    totals += 0.5;
                                                }
                                                else if (Convert.ToInt32(nums[i]) == 1)
                                                {
                                                    if (totals != 0)
                                                    {
                                                        lista.Add(totals);
                                                        totals = 0;
                                                    }
                                                }
                                                if (i == nums.Length - 1)
                                                {
                                                    if (totals != 0)
                                                    {
                                                        lista.Add(totals);
                                                        totals = 0;
                                                    }
                                                }

                                            }

                                            if (lista.Count() == 0)
                                                lista.Add(0);
                                            double abc22 = lista.Sum();

                                            var hrsOPa1 = lista.Sum();
                                            if (hrsOPa1 < 9)
                                            {
                                                obj.NC15 = "OPA NC : No more than 15 hrs work in any 24 hrs period";


                                            }//opa 9 hrs condition compleated


                                            double totalbms1 = 0;
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


                                            }


                                        }

                                        //
                                        SaveNC1Planner(obj);
                                        

                                        obj.opa = true;


                                        var local = sc.Set<WorkHoursPlannerClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                        if (local != null)
                                        {
                                            sc.Entry(local).State = EntityState.Detached;
                                        }




                                        sc.WorkHourPlanners.Attach(obj);
                                        sc.Entry(obj).Property(x => x.opa).IsModified = true;
                                        sc.Entry(obj).Property(x => x.NonConfirmities).IsModified = true;
                                        sc.Entry(obj).Property(x => x.NonConfirmities1).IsModified = true;
                                        sc.SaveChanges();


                                    }
                                    else
                                    {
                                        obj.opa = true;

                                        var local = sc.Set<WorkHoursPlannerClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                        if (local != null)
                                        {
                                            sc.Entry(local).State = EntityState.Detached;
                                        }

                                        sc.WorkHourPlanners.Attach(obj);
                                        sc.Entry(obj).Property(x => x.opa).IsModified = true;
                                        sc.SaveChanges();
                                    }


                                }



                            }




                        }

                    }

                }

                //.....End Planner...........


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }
        public void TimeSlotPageSettingDeleteMethod(DateTime startdate, object enddate, BackgroundWorker Workersave, Grid DynamicGrid)
        {
            int ib = 2;
            Workersave.ReportProgress(ib);

            try
            {

                List<WorkHoursClass> WorkhoursListmain;
                List<WorkHoursPlannerClass> WorkhoursListmainPlanner;
                List<CrewDetailClass> CrewList;
                List<InternationalClass> CheckIDL;
                using (ShipmentContaxt sc1 = new ShipmentContaxt())
                {
                    WorkhoursListmain = sc1.WorkHourss.ToList();
                    WorkhoursListmainPlanner = sc1.WorkHourPlanners.ToList();
                    CrewList = sc1.CrewDetails.ToList();
                    CheckIDL = sc1.Internationals.ToList();
                }


                //.....Planner.........

                var WorkhoursListP = WorkhoursListmainPlanner.Select(x => x.UserName).Distinct().ToList();
                int conP = WorkhoursListmainPlanner.Where(x => (x.dates.Date >= startdate.Date && x.dates.Date <= Convert.ToDateTime(enddate).Date)).Count();
                int conP1 = conP + 100;

                //.....End Planner


                var WorkhoursList = WorkhoursListmain.Select(x => x.UserName).Distinct().ToList();

                int con = WorkhoursListmain.Where(x => (x.dates.Date >= startdate.Date && x.dates.Date <= Convert.ToDateTime(enddate).Date)).Count();
                int con1 = con + 100;

                con1 = con1 + conP1;


                foreach (var li1 in WorkhoursList)
                {

                    var data = WorkhoursListmain.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && (x.dates.Date >= startdate.Date && x.dates.Date <= Convert.ToDateTime(enddate).Date)).ToList();
                    foreach (var obj in data)
                    {

                        ib = ib + 1;
                        Workersave.ReportProgress(ib * (con1 / con));

                        var checkuser1 = CrewList.Where(x => x.UserName == li1).FirstOrDefault();
                        if (checkuser1 != null)
                        {
                            bool chkyoungs = Convert.ToBoolean(checkuser1.chkyoungs);
                            if (chkyoungs == true)
                            {

                                DateTime d1 = Convert.ToDateTime(checkuser1.DOB);
                                DateTime d2 = obj.dates; //DateTime.Now;
                                if (CheckDateYoung(d1, d2))
                                {
                                }
                                else
                                {
                                    //youngseafere has become above 18 years...


                                    if (Convert.ToBoolean(obj.opa.ToString()))
                                    {


                                        obj.NC15 = null;
                                        obj.NC16 = null;

                                        SaveNC1(obj);

                                        obj.NCD15 = null;
                                        obj.MNCCD15 = null;
                                        obj.HNCCD15 = null;
                                        obj.NCD16 = null;
                                        obj.MNCCD16 = null;
                                        obj.HNCCD16 = null;

                                        obj.opa = false;

                                        if (string.IsNullOrEmpty(obj.NonConfirmities))
                                        {
                                            obj.CrewNC = false;
                                            obj.AdminAlarm = false;
                                            obj.MasterAlarm = false;
                                            obj.HODAlarm = false;
                                            obj.MasterCrewNC = false;
                                            obj.HODCrewNC = false;
                                        }

                                        var local = sc.Set<WorkHoursClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                        if (local != null)
                                        {
                                            sc.Entry(local).State = EntityState.Detached;
                                        }


                                        var user = new WorkHoursClass()
                                        {
                                            wid = obj.wid,
                                            NC15 = obj.NC15 = null,
                                            NC16 = obj.NC16 = null,
                                            NonConfirmities = obj.NonConfirmities,
                                            NonConfirmities1 = obj.NonConfirmities1,

                                            NCD15 = obj.NCD15 = null,
                                            MNCCD15 = obj.MNCCD15 = null,
                                            HNCCD15 = obj.HNCCD15 = null,
                                            NCD16 = obj.NCD16 = null,
                                            MNCCD16 = obj.MNCCD16 = null,
                                            HNCCD16 = obj.HNCCD16 = null,

                                            opa = obj.opa = false,

                                            CrewNC = obj.CrewNC = false,
                                            AdminAlarm = obj.AdminAlarm = false,
                                            MasterAlarm = obj.MasterAlarm = false,
                                            HODAlarm = obj.HODAlarm = false,
                                            MasterCrewNC = obj.MasterCrewNC = false,
                                            HODCrewNC = obj.HODCrewNC = false
                                        };


                                        sc.WorkHourss.Attach(user);
                                        sc.Entry(user).Property(x => x.NC15).IsModified = true;
                                        sc.Entry(user).Property(x => x.NC16).IsModified = true;

                                        sc.Entry(user).Property(x => x.NCD15).IsModified = true;
                                        sc.Entry(user).Property(x => x.MNCCD15).IsModified = true;
                                        sc.Entry(user).Property(x => x.HNCCD15).IsModified = true;
                                        sc.Entry(user).Property(x => x.NCD16).IsModified = true;
                                        sc.Entry(user).Property(x => x.MNCCD16).IsModified = true;
                                        sc.Entry(user).Property(x => x.HNCCD16).IsModified = true;

                                        sc.Entry(user).Property(x => x.opa).IsModified = true;
                                        sc.Entry(user).Property(x => x.CrewNC).IsModified = true;
                                        sc.Entry(user).Property(x => x.AdminAlarm).IsModified = true;
                                        sc.Entry(user).Property(x => x.MasterAlarm).IsModified = true;
                                        sc.Entry(user).Property(x => x.HODAlarm).IsModified = true;
                                        sc.Entry(user).Property(x => x.MasterCrewNC).IsModified = true;
                                        sc.Entry(user).Property(x => x.MasterCrewNC).IsModified = true;

                                        sc.Entry(user).Property(x => x.NonConfirmities).IsModified = true;
                                        sc.Entry(user).Property(x => x.NonConfirmities1).IsModified = true;
                                        sc.SaveChanges();




                                    }

                                }

                            }
                            else
                            {

                                // Non Young seaferer......


                                if (Convert.ToBoolean(obj.opa.ToString()))
                                {

                                    obj.NC15 = null;
                                    obj.NC16 = null;

                                    string NonConfirmities = obj.NonConfirmities;
                                    string NonConfirmities1 = obj.NonConfirmities1;
                                    //MessageBox.Show($" Before Nc  date = {obj.dates.ToString()} Nac= {obj.NonConfirmities} Nc1= {obj.NonConfirmities1} ");

                                    SaveNC1(obj);

                                    NonConfirmities = obj.NonConfirmities;
                                    NonConfirmities1 = obj.NonConfirmities1;

                                    //MessageBox.Show($" After Nc  date = {obj.dates.ToString()} Nac= {obj.NonConfirmities} Nc1= {obj.NonConfirmities1} ");

                                    obj.NCD15 = null;
                                    obj.MNCCD15 = null;
                                    obj.HNCCD15 = null;
                                    obj.NCD16 = null;
                                    obj.MNCCD16 = null;
                                    obj.HNCCD16 = null;

                                    obj.opa = false;

                                    if (string.IsNullOrEmpty(obj.NonConfirmities))
                                    {
                                        obj.CrewNC = false;
                                        obj.AdminAlarm = false;
                                        obj.MasterAlarm = false;
                                        obj.HODAlarm = false;
                                        obj.MasterCrewNC = false;
                                        obj.HODCrewNC = false;
                                    }

                                    var local = sc.Set<WorkHoursClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                    if (local != null)
                                    {
                                        sc.Entry(local).State = EntityState.Detached;
                                    }

                                    var user = new WorkHoursClass()
                                    {
                                        wid = obj.wid,
                                        NC15 = obj.NC15 = null,
                                        NC16 = obj.NC16 = null,
                                        NonConfirmities = obj.NonConfirmities,
                                        NonConfirmities1 = obj.NonConfirmities1,

                                        NCD15 = obj.NCD15 = null,
                                        MNCCD15 = obj.MNCCD15 = null,
                                        HNCCD15 = obj.HNCCD15 = null,
                                        NCD16 = obj.NCD16 = null,
                                        MNCCD16 = obj.MNCCD16 = null,
                                        HNCCD16 = obj.HNCCD16 = null,

                                        opa = obj.opa = false,

                                        CrewNC = obj.CrewNC = false,
                                        AdminAlarm = obj.AdminAlarm = false,
                                        MasterAlarm = obj.MasterAlarm = false,
                                        HODAlarm = obj.HODAlarm = false,
                                        MasterCrewNC = obj.MasterCrewNC = false,
                                        HODCrewNC = obj.HODCrewNC = false
                                    };


                                    sc.WorkHourss.Attach(user);
                                    sc.Entry(user).Property(x => x.NC15).IsModified = true;
                                    sc.Entry(user).Property(x => x.NC16).IsModified = true;

                                    sc.Entry(user).Property(x => x.NCD15).IsModified = true;
                                    sc.Entry(user).Property(x => x.MNCCD15).IsModified = true;
                                    sc.Entry(user).Property(x => x.HNCCD15).IsModified = true;
                                    sc.Entry(user).Property(x => x.NCD16).IsModified = true;
                                    sc.Entry(user).Property(x => x.MNCCD16).IsModified = true;
                                    sc.Entry(user).Property(x => x.HNCCD16).IsModified = true;

                                    sc.Entry(user).Property(x => x.opa).IsModified = true;
                                    sc.Entry(user).Property(x => x.CrewNC).IsModified = true;
                                    sc.Entry(user).Property(x => x.AdminAlarm).IsModified = true;
                                    sc.Entry(user).Property(x => x.MasterAlarm).IsModified = true;
                                    sc.Entry(user).Property(x => x.HODAlarm).IsModified = true;
                                    sc.Entry(user).Property(x => x.MasterCrewNC).IsModified = true;
                                    sc.Entry(user).Property(x => x.MasterCrewNC).IsModified = true;

                                    sc.Entry(user).Property(x => x.NonConfirmities).IsModified = true;
                                    sc.Entry(user).Property(x => x.NonConfirmities1).IsModified = true;
                                    sc.SaveChanges();


                                }




                            }


                        }

                    }

                }

                //......Start Planner............



                foreach (var li1 in WorkhoursListP)
                {

                    var data = WorkhoursListmainPlanner.OrderByDescending(x => x.dates).Where(x => x.UserName == li1 && (x.dates.Date >= startdate.Date && x.dates.Date <= Convert.ToDateTime(enddate).Date)).ToList();
                    foreach (var obj in data)
                    {

                        ib = ib + 1;
                        Workersave.ReportProgress(ib * (con1 / con));

                        var checkuser1 = CrewList.Where(x => x.UserName == li1).FirstOrDefault();
                        if (checkuser1 != null)
                        {
                            bool chkyoungs = Convert.ToBoolean(checkuser1.chkyoungs);
                            if (chkyoungs == true)
                            {

                                DateTime d1 = Convert.ToDateTime(checkuser1.DOB);
                                DateTime d2 = obj.dates; //DateTime.Now;
                                if (CheckDateYoung(d1, d2))
                                {
                                }
                                else
                                {
                                    //youngseafere has become above 18 years...


                                    if (Convert.ToBoolean(obj.opa.ToString()))
                                    {


                                        obj.NC15 = null;
                                        obj.NC16 = null;

                                        SaveNC1Planner(obj);

                                       

                                        obj.opa = false;

                                       

                                        var local = sc.Set<WorkHoursPlannerClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                        if (local != null)
                                        {
                                            sc.Entry(local).State = EntityState.Detached;
                                        }


                                        var user = new WorkHoursPlannerClass()
                                        {
                                            wid = obj.wid,
                                            NonConfirmities = obj.NonConfirmities,
                                            NonConfirmities1 = obj.NonConfirmities1,
                                            opa = obj.opa = false

                                          
                                        };


                                        sc.WorkHourPlanners.Attach(user);
                                        sc.Entry(user).Property(x => x.opa).IsModified = true;
                                        sc.Entry(user).Property(x => x.NonConfirmities).IsModified = true;
                                        sc.Entry(user).Property(x => x.NonConfirmities1).IsModified = true;
                                        sc.SaveChanges();




                                    }

                                }

                            }
                            else
                            {

                                // Non Young seaferer......


                                if (Convert.ToBoolean(obj.opa.ToString()))
                                {

                                    obj.NC15 = null;
                                    obj.NC16 = null;

                                    string NonConfirmities = obj.NonConfirmities;
                                    string NonConfirmities1 = obj.NonConfirmities1;
                                    //MessageBox.Show($" Before Nc  date = {obj.dates.ToString()} Nac= {obj.NonConfirmities} Nc1= {obj.NonConfirmities1} ");

                                    SaveNC1Planner(obj);

                                    NonConfirmities = obj.NonConfirmities;
                                    NonConfirmities1 = obj.NonConfirmities1;

                                    obj.opa = false;

                                   

                                    var local = sc.Set<WorkHoursPlannerClass>().Local.FirstOrDefault(c => c.wid == obj.wid);
                                    if (local != null)
                                    {
                                        sc.Entry(local).State = EntityState.Detached;
                                    }

                                    var user = new WorkHoursPlannerClass()
                                    {
                                        wid = obj.wid,
                                        NonConfirmities = obj.NonConfirmities,
                                        NonConfirmities1 = obj.NonConfirmities1,

                                        opa = obj.opa = false

                                       
                                    };


                                    sc.WorkHourPlanners.Attach(user);

                                    sc.Entry(user).Property(x => x.opa).IsModified = true;
                                    sc.Entry(user).Property(x => x.NonConfirmities).IsModified = true;
                                    sc.Entry(user).Property(x => x.NonConfirmities1).IsModified = true;
                                    sc.SaveChanges();


                                }



                            }


                        }

                    }

                }


                //......End Planner.............


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


    }
}
