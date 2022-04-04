using DataBuildingLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;

namespace WorkShipVersionII.WorkHoursViewModel.TimeSlotModels
{
    public class CommanConditionsTimeSlot
    {
        private ShipmentContaxt sc;
        public CommanConditionsTimeSlot()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
        }

        public virtual string Morethan1hrsNormal(string label42)
        {
            //........New Code Implementation For more than an hour of rest.........

            string[] arraybb = null;
            arraybb = label42.Substring(0, label42.Length - 1).Split(',');
            string bmsnewimp = string.Empty;
            int jqn = 0;
            int count = arraybb.Length;
            for (jqn = 0; jqn < count; jqn++)
            {
                if (arraybb.Length > Convert.ToInt32(jqn + 3))
                {
                    if (arraybb[jqn] == "1" && arraybb[jqn + 1] == "0" && arraybb[jqn + 2] == "0" && arraybb[jqn + 3] == "0")
                    {

                        bmsnewimp += arraybb[jqn].ToString() + ',';
                    }
                    else if (arraybb[jqn] == "1" && arraybb[jqn + 1] == "0" && arraybb[jqn + 2] == "0")
                    {

                        bmsnewimp += arraybb[jqn].ToString() + ',' + "1" + ',' + "1" + ',';

                        jqn = jqn + 2;
                    }
                    else if (arraybb[jqn] == "1" && arraybb[jqn + 1] == "0")
                    {

                        bmsnewimp += arraybb[jqn].ToString() + ',' + "1" + ',';

                        jqn = jqn + 1;
                    }

                    else
                    {
                        bmsnewimp += arraybb[jqn].ToString() + ',';
                    }
                }
                else if (arraybb.Length >= Convert.ToInt32(jqn + 2))
                {
                    if (arraybb[jqn] == "0" && arraybb[jqn + 1] == "1")
                    {
                        if (arraybb[jqn - 1] == "0" && arraybb[jqn - 2] == "0")
                        {
                            bmsnewimp += "0" + ',' + "1" + ',';
                            jqn = jqn + 1;
                        }
                        else
                        {
                            bmsnewimp += "1" + ',' + "1" + ',';

                            jqn = jqn + 1;
                        }


                    }
                    else
                    {
                        bmsnewimp += arraybb[jqn].ToString() + ',';
                    }
                }

                else
                {
                    bmsnewimp += arraybb[jqn].ToString() + ',';
                }

            }

            return bmsnewimp.ToString();

        }

        public virtual string Morethan1hrsYoung(string label42)
        {
            //........New Code Implementation For more than an hour of rest.........

            string[] array = null;
            array = label42.Substring(0, label42.Length - 1).Split(',');

            string bmsnewimp = "";
            for (int j = 0; j < array.Length; j++)
            {
                if (array.Length > Convert.ToInt32(j + 5))
                {
                    if (array[j] == "1" && array[j + 1] == "0" && array[j + 2] == "0" && array[j + 3] == "0" && array[j + 4] == "0" && array[j + 5] == "0")
                    {

                        bmsnewimp += array[j].ToString() + ',';
                    }
                    else if (array[j] == "1" && array[j + 1] == "0" && array[j + 2] == "0" && array[j + 3] == "0" && array[j + 4] == "0")
                    {

                        bmsnewimp += array[j].ToString() + ',' + "1" + ',' + "1" + ',' + "1" + ',' + "1" + ',';

                        j = j + 4;
                    }
                    else if (array[j] == "1" && array[j + 1] == "0" && array[j + 2] == "0" && array[j + 3] == "0")
                    {

                        bmsnewimp += array[j].ToString() + ',' + "1" + ',' + "1" + ',' + "1" + ',';

                        j = j + 3;
                    }
                    else if (array[j] == "1" && array[j + 1] == "0" && array[j + 2] == "0")
                    {

                        bmsnewimp += array[j].ToString() + ',' + "1" + ',' + "1" + ',';

                        j = j + 2;
                    }
                    else if (array[j] == "1" && array[j + 1] == "0")
                    {

                        bmsnewimp += array[j].ToString() + ',' + "1" + ',';

                        j = j + 1;
                    }
                    else
                    {
                        bmsnewimp += array[j].ToString() + ',';
                    }
                }
                else if (array.Length >= Convert.ToInt32(j + 4))
                {


                    if (array[j] == "0" && array[j + 1] == "0" && array[j + 2] == "0" && array[j + 3] == "1")
                    {
                        if (array[j - 1] == "0" && array[j - 2] == "0")
                        {
                            bmsnewimp += "0" + ',' + "0" + ',' + "0" + ',' + "1" + ',';
                            j = j + 3;
                        }
                        else
                        {
                            bmsnewimp += "1" + ',' + "1" + ',' + "1" + ',' + "1" + ',';
                            j = j + 3;
                        }
                    }
                    else if (array[j] == "0" && array[j + 1] == "1" && array[j + 2] == "0" && array[j + 3] == "1")
                    {
                        if (array[j - 1] == "0" && array[j - 2] == "0" && array[j - 3] == "0" && array[j - 4] == "0")
                        {
                            bmsnewimp += "0" + ',' + "1" + ',' + "1" + ',' + "1" + ',';
                            j = j + 3;
                        }
                        else
                        {
                            bmsnewimp += "1" + ',' + "1" + ',' + "1" + ',' + "1" + ',';
                            j = j + 3;
                        }
                    }
                    else if (array[j] == "1" && array[j + 1] == "0" && array[j + 2] == "0" && array[j + 3] == "1")
                    {
                        bmsnewimp += "1" + ',' + "1" + ',' + "1" + ',' + "1" + ',';
                        j = j + 3;
                    }
                    else if (array[j] == "1" && array[j + 1] == "1" && array[j + 2] == "0" && array[j + 3] == "1")
                    {
                        bmsnewimp += "1" + ',' + "1" + ',' + "1" + ',' + "1" + ',';
                        j = j + 3;
                    }
                    else if (array[j] == "0" && array[j + 1] == "0" && array[j + 2] == "1" && array[j + 3] == "1")
                    {
                        if (array[j - 1] == "0" && array[j - 2] == "0" && array[j - 3] == "0")
                        {
                            bmsnewimp += "0" + ',' + "0" + ',' + "1" + ',' + "1" + ',';
                            j = j + 3;
                        }
                        else
                        {
                            bmsnewimp += "1" + ',' + "1" + ',' + "1" + ',' + "1" + ',';
                            j = j + 3;
                        }


                    }
                    else if (array[j] == "0" && array[j + 1] == "1" && array[j + 2] == "1" && array[j + 3] == "1")
                    {
                        if (array[j - 1] == "0" && array[j - 2] == "0" && array[j - 3] == "0" && array[j - 4] == "0")
                        {
                            bmsnewimp += "0" + ',' + "1" + ',' + "1" + ',' + "1" + ',';
                            j = j + 3;
                        }
                        else
                        {
                            bmsnewimp += "1" + ',' + "1" + ',' + "1" + ',' + "1" + ',';
                            j = j + 3;
                        }

                    }
                    else if (array[j] == "0" && array[j + 1] == "1" && array[j + 2] == "0" && array[j + 3] == "0")
                    {
                        if (array[j - 1] == "0" && array[j - 2] == "0" && array[j - 3] == "0" && array[j - 4] == "0")
                        {
                            bmsnewimp += "0" + ',' + "1" + ',' + "0" + ',' + "0" + ',';
                            j = j + 3;
                        }
                        else
                        {
                            bmsnewimp += "1" + ',' + "1" + ',' + "0" + ',' + "0" + ',';
                            j = j + 3;

                        }
                    }
                    else if (array[j] == "0" && array[j + 1] == "1" && array[j + 2] == "1" && array[j + 3] == "0")
                    {
                        if (array[j - 1] == "0" && array[j - 2] == "0" && array[j - 3] == "0" && array[j - 4] == "0")
                        {
                            bmsnewimp += "0" + ',' + "1" + ',' + "0" + ',' + "0" + ',';
                            j = j + 3;
                        }
                        else
                        {
                            bmsnewimp += "1" + ',' + "1" + ',' + "0" + ',' + "0" + ',';
                            j = j + 3;

                        }
                    }
                    else
                    {
                        bmsnewimp += array[j].ToString() + ',';
                    }
                }
                else if (array.Length >= Convert.ToInt32(j + 3))
                {


                    if (array[j] == "0" && array[j + 1] == "0" && array[j + 2] == "1")
                    {
                        if (array[j - 1] == "0" && array[j - 2] == "0" && array[j - 3] == "0")
                        {
                            bmsnewimp += "0" + ',' + "0" + ',' + "1" + ',';
                            j = j + 2;
                        }
                        else
                        {
                            bmsnewimp += "1" + ',' + "1" + ',' + "1" + ',';
                            j = j + 2;
                        }
                    }
                    else if (array[j] == "0" && array[j + 1] == "1" && array[j + 2] == "1")
                    {
                        if (array[j - 1] == "0" && array[j - 2] == "0" && array[j - 3] == "0" && array[j - 4] == "0")
                        {
                            bmsnewimp += "0" + ',' + "1" + ',' + "1" + ',';
                            j = j + 2;
                        }
                        else
                        {
                            bmsnewimp += "1" + ',' + "1" + ',' + "1" + ',';
                            j = j + 2;
                        }
                    }
                    else if (array[j] == "0" && array[j + 1] == "1" && array[j + 2] == "0")
                    {
                        if (array[j - 1] == "0" && array[j - 2] == "0" && array[j - 3] == "0" && array[j - 4] == "0")
                        {
                            bmsnewimp += "0" + ',' + "1" + ',' + "0" + ',';
                            j = j + 2;
                        }
                        else
                        {
                            bmsnewimp += "1" + ',' + "1" + ',' + "0" + ',';
                            j = j + 2;
                        }
                    }
                    else
                    {
                        bmsnewimp += array[j].ToString() + ',';
                    }
                }

                else
                {
                    if (array.Length >= Convert.ToInt32(j + 2))
                    {
                        if (array[j] == "0" && array[j + 1] == "1")
                        {
                            if (array[j - 1] == "0" && array[j - 2] == "0" && array[j - 3] == "0" && array[j - 4] == "0")
                            {
                                bmsnewimp += "0" + ',' + "1" + ',';
                                j = j + 1;
                            }
                            else
                            {
                                bmsnewimp += "1" + ',' + "1" + ',';
                                j = j + 1;
                            }
                        }
                        else
                        {
                            bmsnewimp += array[j].ToString() + ',';
                        }
                    }
                    else
                    {

                        bmsnewimp += array[j].ToString() + ',';
                    }
                }

            }



            return bmsnewimp.ToString();

        }

        public string checkbackhours(string label42newv, DateTime dt11b, int tablecount, string idl, List<WorkHoursClass> WorkHoursList, List<InternationalClass> CheckIDL)
        {

            var checkIDL = CheckIDLBack(dt11b, idl, CheckIDL);
            DateTime dates = checkIDL.Item1;
            string WRID = checkIDL.Item2;
            string label421 = string.Empty;


            int cellcount = 0;
            string[] st32 = null;
            string[] conc = null;

            var checkuser = WorkHoursList.Select(s => new { s.UserName, s.dates, s.hrs, s.WRID, s.Cellcount }).Where(x => x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
            if (checkuser != null)
            {
                cellcount = checkuser.Cellcount;
                label421 = checkuser.hrs;
            }
            else
            {
                cellcount = tablecount;
                for (int i = 0; i < cellcount; i++)
                {
                    label421 += "0" + ",";
                }
            }

            st32 = label421.TrimEnd(',').Split(',');
            string[] newhr = null;
            newhr = label42newv.TrimEnd(',').Split(',');// chanhed from label42 to label42bmNew 

            conc = st32.Concat(newhr).ToArray();



            //...New Implementation for an hour....
            string bmschange7bm = string.Join(",", conc);
            bmschange7bm = bmschange7bm + ',';
            string bmschange77bm = Morethan1hrsNormal(bmschange7bm);

            conc = bmschange77bm.Split(',');
            string lbl1main1New = "";

            int bbs = cellcount + tablecount;
            int count = conc.Length - 1;
            for (int sb = cellcount; sb < bbs; sb++)
            {
                if (count >= sb)
                {
                    if (conc[sb] == "1")
                    {

                        lbl1main1New += "1" + ",";

                    }
                    else
                    {
                        lbl1main1New += "0" + ",";
                    }
                }
            }

            return lbl1main1New;

        }
        public bool GetNextValues(DateTime Dt11, string idl, List<WorkHoursClass> WorkHoursList, List<InternationalClass> CheckIDL)
        {
            var checkIDL = CheckIDLNext(Dt11, idl, CheckIDL);
            DateTime dates = checkIDL.Item1;
            string WRID = checkIDL.Item2;

            string worksh = "";
            string[] st22 = null;

            var checkuser = WorkHoursList.Select(s => new { s.UserName, s.dates, s.hrs, s.WRID, s.Cellcount, s.TotalHours }).Where(x => x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
            if (checkuser != null)
            {
                worksh = checkuser.TotalHours.ToString();
                string OneDay = checkuser.hrs.ToString();
                st22 = OneDay.TrimEnd(',').Split(',');
            }
            else
            {
                string rst = "";
                for (int i = 0; i < 96; i++)
                {
                    rst += "0" + ",";
                }
                st22 = rst.TrimEnd(',').Split(',');
            }

            bool ss = st22[0] == "1" ? true : false; ;
            ss = worksh == "24" ? false : ss;

            return ss;
        }

        public virtual void GetbackhoursValues(WorkHoursClass obj, List<WorkHoursClass> WorkHoursList, List<InternationalClass> CheckIDL)
        {
            try
            {
                ArrayList ab10rest1 = new ArrayList();
                ab10rest1.Clear();
              


                string[] st42bms7 = null;
                //string[] st1bms = null;


                int cellcouns = 0;
                int iDatebm = 0;
                string IDLS = obj.IDL;
                string label42OneDay = string.Empty;
                DateTime dates = obj.dates;
                while (iDatebm < 15)
                {

                    var checkIDL = CheckIDLBack(dates, IDLS, CheckIDL);
                    dates = checkIDL.Item1;
                    string WRID = checkIDL.Item2;
                    IDLS = checkIDL.Item3;

                    var checkuser = WorkHoursList.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
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

                            startEndbbs = string.Join(",", st22bbs) + ",";
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


                st42bms7 = label42OneDay.TrimEnd(',').Split(',');

                //st1bms = st42bms7.Skip(336 - 48).Select(i => i.ToString()).ToArray();

                //...New Implementation for an hour....
                string change42 = string.Join(",", st42bms7);
                change42 = change42 + ',';
                string change742 = Morethan1hrsNormal(change42);
                st42bms7 = change742.TrimEnd(',').Split(',');
                //...End New Implementation for an hour...


                ////...New Implementation for an hour....
                //string change1 = string.Join(",", st1bms);
                //change1 = change1 + ',';
                //string change71 = Morethan1hrsNormal(change1);
                //st1bms = change71.TrimEnd(',').Split(',');
                ////...End New Implementation for an hour...




                double totalbms1 = 0;
                ArrayList listabms1 = new ArrayList();

                //double totalbms = 0;
                //ArrayList listabms = new ArrayList();
                //listabms.Clear();

                listabms1.Clear();
               

                int norm22 = 0;
                norm22 = st42bms7.Length;
                for (long i = 0; i < norm22; i++)
                {

                    if (Convert.ToInt32(st42bms7[i]) == 0)
                    {
                        totalbms1 += 0.5;
                    }
                    else if (Convert.ToInt32(st42bms7[i]) == 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }
                    if (i == st42bms7.Length - 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }

                }





                double abc221maa = 0;
                foreach (var li in listabms1)
                {
                    abc221maa += Convert.ToDouble(li.ToString());// in this label we show rest periods and how much hrs of rest he takes

                }

                abc221maa = abc221maa - Convert.ToDouble(obj.TotalHours);

                ab10rest1.Add(abc221maa);
                obj.RestHour7day = Convert.ToDecimal(abc221maa);
                obj.RestHourAny7day = abc221maa.ToString();

                // for last day calculation....

                //int norm1 = 0;
                //norm1 = st1bms.Length;
                //for (long i = 0; i < norm1; i++)
                //{

                //    if (Convert.ToInt32(st1bms[i]) == 0)
                //    {
                //        totalbms += 0.5;
                //    }
                //    else if (Convert.ToInt32(st1bms[i]) == 1)
                //    {
                //        if (totalbms != 0)
                //        {
                //            listabms.Add(totalbms);
                //            totalbms = 0;
                //        }
                //    }
                //    if (i == st1bms.Length - 1)
                //    {
                //        if (totalbms != 0)
                //        {
                //            listabms.Add(totalbms);
                //            totalbms = 0;
                //        }
                //    }

                //}

                //double abc1maa = 0;
                //foreach (var li in listabms)
                //{
                //    abc1maa += Convert.ToDouble(li.ToString());// in this label we show rest periods and how much hrs of rest he takes

                //}

                //abc1maa = abc1maa - Convert.ToDouble(obj.TotalHours);

                ////ab10rest.Add(abc1maa);
                //obj.RestHours = Convert.ToDecimal(abc1maa);
                //obj.RestHourAny24 = abc1maa.ToString();





            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        public virtual void GetbackhoursValuesPlanner(WorkHoursPlannerClass obj, List<WorkHoursClass> WorkHoursList, List<InternationalClass> CheckIDL)
        {
            try
            {
                ArrayList ab10rest1 = new ArrayList();
                ab10rest1.Clear();


                string[] st42bms7 = null;

                int cellcouns = 0;
                int iDatebm = 0;
                string IDLS = obj.IDL;
                string label42OneDay = string.Empty;
                DateTime dates = obj.dates;
                while (iDatebm < 15)
                {

                    var checkIDL = CheckIDLBack(dates, IDLS, CheckIDL);
                    dates = checkIDL.Item1;
                    string WRID = checkIDL.Item2;
                    IDLS = checkIDL.Item3;

                    var checkuser = WorkHoursList.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
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

                            startEndbbs = string.Join(",", st22bbs) + ",";
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


                st42bms7 = label42OneDay.TrimEnd(',').Split(',');



                //...New Implementation for an hour....
                string change42 = string.Join(",", st42bms7);
                change42 = change42 + ',';
                string change742 = Morethan1hrsNormal(change42);
                st42bms7 = change742.TrimEnd(',').Split(',');
                //...End New Implementation for an hour...



                double totalbms1 = 0;
                ArrayList listabms1 = new ArrayList();

                listabms1.Clear();

                int norm22 = 0;
                norm22 = st42bms7.Length;
                for (long i = 0; i < norm22; i++)
                {

                    if (Convert.ToInt32(st42bms7[i]) == 0)
                    {
                        totalbms1 += 0.5;
                    }
                    else if (Convert.ToInt32(st42bms7[i]) == 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }
                    if (i == st42bms7.Length - 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }

                }

                double abc221maa = 0;
                foreach (var li in listabms1)
                {
                    abc221maa += Convert.ToDouble(li.ToString());// in this label we show rest periods and how much hrs of rest he takes

                }

                abc221maa = abc221maa - Convert.ToDouble(obj.TotalHours);

                ab10rest1.Add(abc221maa);
                obj.RestHour7day = Convert.ToDecimal(abc221maa);
                obj.RestHourAny7day = abc221maa.ToString();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public virtual void GetbackhoursValuesYoungPlanner(WorkHoursPlannerClass obj, List<WorkHoursClass> WorkHoursList, List<InternationalClass> CheckIDL)
        {
            try
            {
                ArrayList ab10rest1 = new ArrayList();
                ab10rest1.Clear();


                string[] st42bms7 = null;

                int cellcouns = 0;
                int iDatebm = 0;
                string IDLS = obj.IDL;
                string label42OneDay = string.Empty;
                DateTime dates = obj.dates;
                while (iDatebm < 15)
                {

                    var checkIDL = CheckIDLBack(dates, IDLS, CheckIDL);
                    dates = checkIDL.Item1;
                    string WRID = checkIDL.Item2;
                    IDLS = checkIDL.Item3;

                    var checkuser = WorkHoursList.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
                    if (checkuser != null)
                    {
                        int countbms = checkuser.Cellcount;
                        if (countbms + cellcouns <= 672)
                        {
                            cellcouns += checkuser.Cellcount;

                            string startEndbbs = checkuser.hrs;



                            label42OneDay = startEndbbs + label42OneDay;
                        }
                        else
                        {
                            int countbms1 = checkuser.Cellcount;
                            int finalCellcount = 672 - cellcouns;
                            string startEndbbs = checkuser.hrs;
                            string[] st22bbs = startEndbbs.TrimEnd(',').Split(',');

                            st22bbs = st22bbs.Skip(countbms1 - finalCellcount).Select(i => i.ToString()).ToArray();



                            startEndbbs = string.Join(",", st22bbs) + ",";
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

                    if (cellcouns >= 672)
                        break;

                    iDatebm++;
                }


                st42bms7 = label42OneDay.TrimEnd(',').Split(',');



                //...New Implementation for an hour....
                string change42 = string.Join(",", st42bms7);
                change42 = change42 + ',';
                string change742 = Morethan1hrsNormal(change42);
                st42bms7 = change742.TrimEnd(',').Split(',');
                //...End New Implementation for an hour...



                double totalbms1 = 0;
                ArrayList listabms1 = new ArrayList();

                listabms1.Clear();

                int norm22 = 0;
                norm22 = st42bms7.Length;
                for (long i = 0; i < norm22; i++)
                {

                    if (Convert.ToInt32(st42bms7[i]) == 0)
                    {
                        totalbms1 += 0.25;
                    }
                    else if (Convert.ToInt32(st42bms7[i]) == 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }
                    if (i == st42bms7.Length - 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }

                }

                double abc221maa = 0;
                foreach (var li in listabms1)
                {
                    abc221maa += Convert.ToDouble(li.ToString());// in this label we show rest periods and how much hrs of rest he takes

                }

                abc221maa = abc221maa - Convert.ToDouble(obj.TotalHours);

                ab10rest1.Add(abc221maa);
                obj.RestHour7day = Convert.ToDecimal(abc221maa);
                obj.RestHourAny7day = abc221maa.ToString();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public virtual void GetbackhoursValuesYoung1Planner(WorkHoursPlannerClass obj, List<WorkHoursClass> WorkHoursList, List<InternationalClass> CheckIDL)
        {
            try
            {
                ArrayList ab10rest1 = new ArrayList();
                ab10rest1.Clear();


                string[] st42bms7 = null;

                int cellcouns = 0;
                int iDatebm = 0;
                string IDLS = obj.IDL;
                string label42OneDay = string.Empty;
                DateTime dates = obj.dates;
                while (iDatebm < 15)
                {

                    var checkIDL = CheckIDLBack(dates, IDLS, CheckIDL);
                    dates = checkIDL.Item1;
                    string WRID = checkIDL.Item2;
                    IDLS = checkIDL.Item3;

                    var checkuser = WorkHoursList.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
                    if (checkuser != null)
                    {
                        int countbms = checkuser.Cellcount;
                        if (countbms + cellcouns <= 672)
                        {
                            cellcouns += checkuser.Cellcount;

                            string startEndbbs = checkuser.hrs;
                            label42OneDay = startEndbbs + label42OneDay;
                        }
                        else
                        {
                            int countbms1 = checkuser.Cellcount;
                            int finalCellcount = 672 - cellcouns;
                            string startEndbbs = checkuser.hrs;
                            string[] st22bbs = startEndbbs.TrimEnd(',').Split(',');

                            st22bbs = st22bbs.Skip(countbms1 - finalCellcount).Select(i => i.ToString()).ToArray();

                            startEndbbs = string.Join(",", st22bbs) + ",";
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

                    if (cellcouns >= 672)
                        break;

                    iDatebm++;
                }


                st42bms7 = label42OneDay.TrimEnd(',').Split(',');



                ////...New Implementation for an hour....
                //string change42 = string.Join(",", st42bms7);
                //change42 = change42 + ',';
                //string change742 = Morethan1hrsNormal(change42);
                //st42bms7 = change742.TrimEnd(',').Split(',');
                ////...End New Implementation for an hour...



                double totalbms1 = 0;
                ArrayList listabms1 = new ArrayList();

                listabms1.Clear();

                int norm22 = 0;
                norm22 = st42bms7.Length;
                for (long i = 0; i < norm22; i++)
                {

                    if (Convert.ToInt32(st42bms7[i]) == 0)
                    {
                        totalbms1 += 0.25;
                    }
                    else if (Convert.ToInt32(st42bms7[i]) == 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }
                    if (i == st42bms7.Length - 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }

                }

                double abc221maa = 0;
                foreach (var li in listabms1)
                {
                    abc221maa += Convert.ToDouble(li.ToString());// in this label we show rest periods and how much hrs of rest he takes

                }

                abc221maa = abc221maa - Convert.ToDouble(obj.TotalHours);

                ab10rest1.Add(abc221maa);
                obj.RestHour7day = Convert.ToDecimal(abc221maa);
                obj.RestHourAny7day = abc221maa.ToString();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        public virtual void GetbackhoursValuesYoung(WorkHoursClass obj, List<WorkHoursClass> WorkHoursList, List<InternationalClass> CheckIDL)
        {
            try
            {
                ArrayList ab10rest1 = new ArrayList();
                ab10rest1.Clear();


                string[] st42bms7 = null;

                int cellcouns = 0;
                int iDatebm = 0;
                string IDLS = obj.IDL;
                string label42OneDay = string.Empty;
                DateTime dates = obj.dates;
                while (iDatebm < 15)
                {

                    var checkIDL = CheckIDLBack(dates, IDLS, CheckIDL);
                    dates = checkIDL.Item1;
                    string WRID = checkIDL.Item2;
                    IDLS = checkIDL.Item3;

                    var checkuser = WorkHoursList.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
                    if (checkuser != null)
                    {
                        int countbms = checkuser.Cellcount;
                        if (countbms + cellcouns <= 672)
                        {
                            cellcouns += checkuser.Cellcount;

                            string startEndbbs = checkuser.hrs;



                            label42OneDay = startEndbbs + label42OneDay;
                        }
                        else
                        {
                            int countbms1 = checkuser.Cellcount;
                            int finalCellcount = 672 - cellcouns;
                            string startEndbbs = checkuser.hrs;
                            string[] st22bbs = startEndbbs.TrimEnd(',').Split(',');

                            st22bbs = st22bbs.Skip(countbms1 - finalCellcount).Select(i => i.ToString()).ToArray();



                            startEndbbs = string.Join(",", st22bbs) + ",";
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

                    if (cellcouns >= 672)
                        break;

                    iDatebm++;
                }


                st42bms7 = label42OneDay.TrimEnd(',').Split(',');



                //...New Implementation for an hour....
                string change42 = string.Join(",", st42bms7);
                change42 = change42 + ',';
                string change742 = Morethan1hrsNormal(change42);
                st42bms7 = change742.TrimEnd(',').Split(',');
                //...End New Implementation for an hour...



                double totalbms1 = 0;
                ArrayList listabms1 = new ArrayList();

                listabms1.Clear();

                int norm22 = 0;
                norm22 = st42bms7.Length;
                for (long i = 0; i < norm22; i++)
                {

                    if (Convert.ToInt32(st42bms7[i]) == 0)
                    {
                        totalbms1 += 0.25;
                    }
                    else if (Convert.ToInt32(st42bms7[i]) == 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }
                    if (i == st42bms7.Length - 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }

                }

                double abc221maa = 0;
                foreach (var li in listabms1)
                {
                    abc221maa += Convert.ToDouble(li.ToString());// in this label we show rest periods and how much hrs of rest he takes

                }

                abc221maa = abc221maa - Convert.ToDouble(obj.TotalHours);

                ab10rest1.Add(abc221maa);
                obj.RestHour7day = Convert.ToDecimal(abc221maa);
                obj.RestHourAny7day = abc221maa.ToString();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        public virtual void GetbackhoursValuesYoung1(WorkHoursClass obj, List<WorkHoursClass> WorkHoursList, List<InternationalClass> CheckIDL)
        {
            try
            {
                ArrayList ab10rest1 = new ArrayList();
                ab10rest1.Clear();


                string[] st42bms7 = null;

                int cellcouns = 0;
                int iDatebm = 0;
                string IDLS = obj.IDL;
                string label42OneDay = string.Empty;
                DateTime dates = obj.dates;
                while (iDatebm < 15)
                {

                    var checkIDL = CheckIDLBack(dates, IDLS, CheckIDL);
                    dates = checkIDL.Item1;
                    string WRID = checkIDL.Item2;
                    IDLS = checkIDL.Item3;

                    var checkuser = WorkHoursList.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(dates) && x.WRID.Equals(WRID)).FirstOrDefault();
                    if (checkuser != null)
                    {
                        int countbms = checkuser.Cellcount;
                        if (countbms + cellcouns <= 672)
                        {
                            cellcouns += checkuser.Cellcount;

                            string startEndbbs = checkuser.hrs;
                            label42OneDay = startEndbbs + label42OneDay;
                        }
                        else
                        {
                            int countbms1 = checkuser.Cellcount;
                            int finalCellcount = 672 - cellcouns;
                            string startEndbbs = checkuser.hrs;
                            string[] st22bbs = startEndbbs.TrimEnd(',').Split(',');

                            st22bbs = st22bbs.Skip(countbms1 - finalCellcount).Select(i => i.ToString()).ToArray();

                            startEndbbs = string.Join(",", st22bbs) + ",";
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

                    if (cellcouns >= 672)
                        break;

                    iDatebm++;
                }


                st42bms7 = label42OneDay.TrimEnd(',').Split(',');



                ////...New Implementation for an hour....
                //string change42 = string.Join(",", st42bms7);
                //change42 = change42 + ',';
                //string change742 = Morethan1hrsNormal(change42);
                //st42bms7 = change742.TrimEnd(',').Split(',');
                ////...End New Implementation for an hour...



                double totalbms1 = 0;
                ArrayList listabms1 = new ArrayList();

                listabms1.Clear();

                int norm22 = 0;
                norm22 = st42bms7.Length;
                for (long i = 0; i < norm22; i++)
                {

                    if (Convert.ToInt32(st42bms7[i]) == 0)
                    {
                        totalbms1 += 0.25;
                    }
                    else if (Convert.ToInt32(st42bms7[i]) == 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }
                    if (i == st42bms7.Length - 1)
                    {
                        if (totalbms1 != 0)
                        {
                            listabms1.Add(totalbms1);
                            totalbms1 = 0;
                        }
                    }

                }

                double abc221maa = 0;
                foreach (var li in listabms1)
                {
                    abc221maa += Convert.ToDouble(li.ToString());// in this label we show rest periods and how much hrs of rest he takes

                }

                abc221maa = abc221maa - Convert.ToDouble(obj.TotalHours);

                ab10rest1.Add(abc221maa);
                obj.RestHour7day = Convert.ToDecimal(abc221maa);
                obj.RestHourAny7day = abc221maa.ToString();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public virtual int CheckRetard(string Dateid, DateTime Dt11, List<RetardtblClass> CheckRetar)
        {
            int columns;
            var data = CheckRetar.Where(x => x.DateID == Dateid && x.RDate == Dt11).FirstOrDefault();
            if (data != null)
            {
                string retard1 = data.RET_ADV.ToString();
                decimal times = 0;
                if (retard1 == "RETARD")
                {
                    times = Convert.ToDecimal(data.Times);
                    times = (times * 2) + 48;
                    columns = Convert.ToInt32(times);
                }
                else
                {
                    times = Convert.ToDecimal(data.Times);
                    times = 48 - (times * 2);
                    columns = Convert.ToInt32(times);
                }

            }
            else
                columns = 48;

            return columns;
        }

        public virtual Tuple<DateTime, string, string> CheckIDLNext(DateTime dates, string IDLs, List<InternationalClass> CheckIDL)
        {
            string WRIDs = string.Empty;
            var date = dates;

            var Checkretard = CheckIDL.Where(x => (x.RDate) == dates).FirstOrDefault();


            for (int i = 1; i < 30; i++)
            {
                if (i > 1)
                    date = Convert.ToDateTime(dates).AddDays(i);
                Checkretard = CheckIDL.Where(x => (x.RDate) == date).FirstOrDefault();

                if (Checkretard != null)
                {
                    if (Checkretard.RET_ADV == "RETARD")
                    {
                        if (string.IsNullOrEmpty(IDLs))
                        {
                            IDLs = "IDL";
                            int id = (int)dates.Day;
                            WRIDs = id + IDLs;
                            break;
                        }
                        else
                        {
                            //var Checkretard1 = CheckIDL.Where(x => (System.Data.Entity.DbFunctions.TruncateTime(x.RDate) == date)).FirstOrDefault();
                            date = Convert.ToDateTime(dates).AddDays(i);
                            i = i + 1;
                            var Checkretard1 = CheckIDL.Where(x => (x.RDate) == date).FirstOrDefault();

                            date = Checkretard1 == null ? date : Checkretard1.RET_ADV != "RETARD" ? date.AddDays(1) : date;

                            //var Checkretard1b = CheckIDL.Where(x => (x.RDate) == date).FirstOrDefault();

                            while (Checkretard1 != null)
                            {
                                Checkretard1 = CheckIDL.Where(x => (x.RDate) == date).FirstOrDefault();
                                date = Checkretard1 == null ? date : Checkretard1.RET_ADV != "RETARD" ? date.AddDays(1) : date;


                            }

                            int id = (int)date.Day;
                            WRIDs = id.ToString();
                            IDLs = null;
                            break;

                        }

                    }

                }
                else
                {


                    date = Convert.ToDateTime(dates).AddDays(i);

                    var Checkretard1 = CheckIDL.Where(x => (x.RDate) == date).FirstOrDefault();
                    if (Checkretard1 != null)
                    {
                        if (Checkretard1.RET_ADV == "RETARD")
                        {
                            //date = Checkretard1 == null ? date : Checkretard1.RET_ADV != "RETARD" ? date.AddDays(-1) : date;

                            IDLs = null;
                            int id = (int)date.Day;
                            WRIDs = id.ToString();
                            break;
                        }
                    }
                    else
                    {
                        IDLs = null;
                        int id = (int)date.Day;
                        WRIDs = id.ToString();
                        break;
                    }
                }



            }

            return Tuple.Create(date, WRIDs, IDLs);
        }

        public virtual Tuple<DateTime, string, string> CheckIDLBack(DateTime dates, string IDLs, List<InternationalClass> CheckIDL)
        {
            string WRIDs = string.Empty;
            var date = dates;

            //var CheckIDL = sc.Internationals.ToList();

            for (int i = 1; i < 30; i++)
            {
                if (!string.IsNullOrEmpty(IDLs))
                {
                    IDLs = null;
                    int id = (int)dates.Day;
                    WRIDs = id.ToString();

                    break;
                }
                else
                {

                    IDLs = null;
                    date = Convert.ToDateTime(dates).AddDays(-i);
                    var Checkretard = CheckIDL.Where(x => (x.RDate) == date).FirstOrDefault();

                    if (Checkretard != null)
                    {
                        if (Checkretard.RET_ADV == "RETARD")
                        {
                            IDLs = "IDL";
                            int id = (int)date.Day;
                            WRIDs = id + IDLs;
                            break;
                        }

                    }
                    else
                    {
                        date = Convert.ToDateTime(dates).AddDays(-i);
                        var Checkretard1 = CheckIDL.Where(x => (x.RDate) == date).FirstOrDefault();
                        if (Checkretard1 != null)
                        {
                            if (Checkretard1.RET_ADV == "RETARD")
                            {
                                //date = Checkretard1 == null ? date : Checkretard1.RET_ADV != "RETARD" ? date.AddDays(-1) : date;

                                IDLs = "IDL";
                                int id = (int)date.Day;
                                WRIDs = id + IDLs;
                                break;
                            }
                        }
                        else
                        {
                            IDLs = null;
                            int id = (int)date.Day;
                            WRIDs = id.ToString();
                            break;
                        }
                    }

                }
            }


            return Tuple.Create(date, WRIDs, IDLs);
        }
        public bool get77NC(WorkHoursClass obj, List<WorkHoursClass> WorkHoursList, List<InternationalClass> CheckIDL)
        {

            bool bmsnc;
            int iDateopbm1 = 7;
            int countexbm1 = 0;


            DateTime date1 = obj.dates;
            string IDLS1 = obj.IDL;

            while (iDateopbm1 > 0)
            {

                var checkIDL = CheckIDLBack(date1, IDLS1, CheckIDL);
                date1 = checkIDL.Item1;
                string WRID = checkIDL.Item2;
                IDLS1 = checkIDL.Item3;


                var checkNC = WorkHoursList.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(date1) && x.WRID.Equals(WRID) && x.RestHour7day < 70).Select(s => s.RestHour7day).FirstOrDefault();

                if (checkNC > 0)
                    countexbm1 += 1;


                iDateopbm1--;

            }



            bmsnc = countexbm1 > 0 ? true : false;
            return bmsnc;
        }

        public bool get77NCPlanner(WorkHoursPlannerClass obj, List<WorkHoursClass> WorkHoursList, List<InternationalClass> CheckIDL)
        {

            bool bmsnc;
            int iDateopbm1 = 7;
            int countexbm1 = 0;


            DateTime date1 = obj.dates;
            string IDLS1 = obj.IDL;

            while (iDateopbm1 > 0)
            {

                var checkIDL = CheckIDLBack(date1, IDLS1, CheckIDL);
                date1 = checkIDL.Item1;
                string WRID = checkIDL.Item2;
                IDLS1 = checkIDL.Item3;


                var checkNC = WorkHoursList.Where(x => x.UserName.Equals(obj.UserName) && x.dates.Equals(date1) && x.WRID.Equals(WRID) && x.RestHour7day < 70).Select(s => s.RestHour7day).FirstOrDefault();

                if (checkNC > 0)
                    countexbm1 += 1;


                iDateopbm1--;

            }



            bmsnc = countexbm1 > 0 ? true : false;
            return bmsnc;
        }
        public string Getopadate(string indexing)
        {
            string startdate = "";
            startdate = indexing == "1" ? "00:30"
                       : indexing == "2" ? "01:00"
                       : indexing == "3" ? "01:30"
                       : indexing == "4" ? "02:00"
                       : indexing == "5" ? "02:30"
                       : indexing == "6" ? "03:00"
                       : indexing == "7" ? "03:30"
                       : indexing == "8" ? "04:00"
                       : indexing == "9" ? "04:30"
                       : indexing == "10" ? "05:00"
                       : indexing == "11" ? "05:30"
                       : indexing == "12" ? "06:00"
                       : indexing == "13" ? "06:30"
                       : indexing == "14" ? "07:00"
                       : indexing == "15" ? "07:30"
                       : indexing == "16" ? "08:00"
                       : indexing == "17" ? "08:30"
                       : indexing == "18" ? "09:00"
                       : indexing == "19" ? "09:30"
                       : indexing == "20" ? "10:00"
                       : indexing == "21" ? "10:30"
                       : indexing == "22" ? "11:00"
                       : indexing == "23" ? "11:30"
                       : indexing == "24" ? "12:00"
                       : indexing == "25" ? "12:30"
                       : indexing == "26" ? "13:00"
                       : indexing == "27" ? "13:30"
                       : indexing == "28" ? "14:00"
                       : indexing == "29" ? "14:30"
                       : indexing == "30" ? "15:00"
                       : indexing == "31" ? "15:30"
                       : indexing == "32" ? "16:00"
                       : indexing == "33" ? "16:30"
                       : indexing == "34" ? "17:00"
                       : indexing == "35" ? "17:30"
                       : indexing == "36" ? "18:00"
                       : indexing == "37" ? "18:30"
                       : indexing == "38" ? "19:00"
                       : indexing == "39" ? "19:30"
                       : indexing == "40" ? "20:00"
                       : indexing == "41" ? "20:30"
                       : indexing == "42" ? "21:00"
                       : indexing == "43" ? "21:30"
                       : indexing == "44" ? "22:00"
                       : indexing == "45" ? "22:30"
                       : indexing == "46" ? "23:00"
                       : indexing == "47" ? "23:30"
                       : "00:00";

            return startdate;
        }
        public void SaveNC1(WorkHoursClass ncbm)
        {
            try
            {

                if (string.IsNullOrEmpty(ncbm.NC1) && string.IsNullOrEmpty(ncbm.NC2) && string.IsNullOrEmpty(ncbm.NC3) && string.IsNullOrEmpty(ncbm.NC4) && string.IsNullOrEmpty(ncbm.NC5) && string.IsNullOrEmpty(ncbm.NC6) && string.IsNullOrEmpty(ncbm.NC7) && string.IsNullOrEmpty(ncbm.NC8) && string.IsNullOrEmpty(ncbm.NC9) && string.IsNullOrEmpty(ncbm.NC10) && string.IsNullOrEmpty(ncbm.NC11) && string.IsNullOrEmpty(ncbm.NC12) && string.IsNullOrEmpty(ncbm.NC13) && string.IsNullOrEmpty(ncbm.NC14) && string.IsNullOrEmpty(ncbm.NC15) && string.IsNullOrEmpty(ncbm.NC16) && string.IsNullOrEmpty(ncbm.NC17) && string.IsNullOrEmpty(ncbm.NC18) && string.IsNullOrEmpty(ncbm.YNC1) && string.IsNullOrEmpty(ncbm.YNC2) && string.IsNullOrEmpty(ncbm.YNC3) && string.IsNullOrEmpty(ncbm.YNC4) && string.IsNullOrEmpty(ncbm.YNC5) && string.IsNullOrEmpty(ncbm.YNC6) && string.IsNullOrEmpty(ncbm.YNC7) && string.IsNullOrEmpty(ncbm.YNC8))
                {

                    //ncbm.txtNCma = null;
                    ncbm.NonConfirmities = string.Empty;
                }
                else
                {

                    ArrayList listnc = new ArrayList();
                    listnc.Clear();


                    if (!string.IsNullOrEmpty(ncbm.NC2))
                    {
                        ncbm.NC3 = null;
                    }
                    if (!string.IsNullOrEmpty(ncbm.NC9))
                    {
                        ncbm.NC3 = null;
                    }
                    if (!string.IsNullOrEmpty(ncbm.NC10))
                    {
                        ncbm.NC9 = null;

                    }
                    if (!string.IsNullOrEmpty(ncbm.NC11))
                    {
                        ncbm.NC12 = null;
                    }

                    if (!string.IsNullOrEmpty(ncbm.NC1))
                    {
                        listnc.Add(ncbm.NC1);
                    }
                    if (!string.IsNullOrEmpty(ncbm.NC2))
                    {
                        if (string.IsNullOrEmpty(ncbm.NC14))
                        {
                            listnc.Add(ncbm.NC2);
                        }
                    }
                    if (!string.IsNullOrEmpty(ncbm.NC3))
                        listnc.Add(ncbm.NC3);

                    if (!string.IsNullOrEmpty(ncbm.NC4))
                        listnc.Add(ncbm.NC4);

                    if (!string.IsNullOrEmpty(ncbm.NC5))
                        listnc.Add(ncbm.NC5);

                    if (!string.IsNullOrEmpty(ncbm.NC6))
                        listnc.Add(ncbm.NC6);

                    if (!string.IsNullOrEmpty(ncbm.NC7))
                        listnc.Add(ncbm.NC7);

                    if (!string.IsNullOrEmpty(ncbm.NC8))
                        listnc.Add(ncbm.NC8);


                    if (!string.IsNullOrEmpty(ncbm.NC9))
                        listnc.Add(ncbm.NC9);

                    if (!string.IsNullOrEmpty(ncbm.NC10))
                        listnc.Add(ncbm.NC10);

                    if (!string.IsNullOrEmpty(ncbm.NC11))
                        listnc.Add(ncbm.NC11);

                    if (!string.IsNullOrEmpty(ncbm.NC12))
                        listnc.Add(ncbm.NC12);

                    if (!string.IsNullOrEmpty(ncbm.NC13))
                        listnc.Add(ncbm.NC13);

                    if (!string.IsNullOrEmpty(ncbm.NC14))
                        listnc.Add(ncbm.NC14);

                    if (!string.IsNullOrEmpty(ncbm.NC17))
                        listnc.Add(ncbm.NC17);


                    if (!string.IsNullOrEmpty(ncbm.NC18))
                        listnc.Add(ncbm.NC18);


                    if (!string.IsNullOrEmpty(ncbm.NC15))
                        listnc.Add(ncbm.NC15);

                    if (!string.IsNullOrEmpty(ncbm.NC16))
                        listnc.Add(ncbm.NC16);


                    if (!string.IsNullOrEmpty(ncbm.YNC1))
                        listnc.Add(ncbm.YNC1);

                    if (!string.IsNullOrEmpty(ncbm.YNC2))
                        listnc.Add(ncbm.YNC2);

                    if (!string.IsNullOrEmpty(ncbm.YNC3))
                        listnc.Add(ncbm.YNC3);


                    if (!string.IsNullOrEmpty(ncbm.YNC4))
                        listnc.Add(ncbm.YNC4);


                    if (!string.IsNullOrEmpty(ncbm.YNC5))
                        listnc.Add(ncbm.YNC5);

                    if (!string.IsNullOrEmpty(ncbm.YNC6))
                        listnc.Add(ncbm.YNC6);

                    if (!string.IsNullOrEmpty(ncbm.YNC7))
                        listnc.Add(ncbm.YNC7);

                    if (!string.IsNullOrEmpty(ncbm.YNC8))
                        listnc.Add(ncbm.YNC8);


                    ncbm.NonConfirmities1 = string.Empty;
                    ncbm.NonConfirmities = string.Empty;
                    foreach (string lis in listnc)
                    {

                        ncbm.NonConfirmities += "- " + lis.ToString() + Environment.NewLine;

                        string result1 = lis.Substring(0, 9);
                        if (result1 != "Future NC")
                        {
                            ncbm.NonConfirmities1 += "- " + lis.ToString() + Environment.NewLine;
                        }

                    }

                    ncbm.NonConfirmities = ncbm.NonConfirmities.TrimEnd(',');


                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                sc.ErrorLog(ex);
            }

        }

        public void SaveNC1Planner(WorkHoursPlannerClass ncbm)
        {
            try
            {

                if (string.IsNullOrEmpty(ncbm.NC1) && string.IsNullOrEmpty(ncbm.NC2) && string.IsNullOrEmpty(ncbm.NC3) && string.IsNullOrEmpty(ncbm.NC4) && string.IsNullOrEmpty(ncbm.NC5) && string.IsNullOrEmpty(ncbm.NC6) && string.IsNullOrEmpty(ncbm.NC7) && string.IsNullOrEmpty(ncbm.NC8) && string.IsNullOrEmpty(ncbm.NC9) && string.IsNullOrEmpty(ncbm.NC10) && string.IsNullOrEmpty(ncbm.NC11) && string.IsNullOrEmpty(ncbm.NC12) && string.IsNullOrEmpty(ncbm.NC13) && string.IsNullOrEmpty(ncbm.NC14) && string.IsNullOrEmpty(ncbm.NC15) && string.IsNullOrEmpty(ncbm.NC16) && string.IsNullOrEmpty(ncbm.NC17) && string.IsNullOrEmpty(ncbm.NC18) && string.IsNullOrEmpty(ncbm.YNC1) && string.IsNullOrEmpty(ncbm.YNC2) && string.IsNullOrEmpty(ncbm.YNC3) && string.IsNullOrEmpty(ncbm.YNC4) && string.IsNullOrEmpty(ncbm.YNC5) && string.IsNullOrEmpty(ncbm.YNC6) && string.IsNullOrEmpty(ncbm.YNC7) && string.IsNullOrEmpty(ncbm.YNC8))
                {

                    //ncbm.txtNCma = null;
                    ncbm.NonConfirmities = string.Empty;
                }
                else
                {

                    ArrayList listnc = new ArrayList();
                    listnc.Clear();


                    if (!string.IsNullOrEmpty(ncbm.NC2))
                    {
                        ncbm.NC3 = null;
                    }
                    if (!string.IsNullOrEmpty(ncbm.NC9))
                    {
                        ncbm.NC3 = null;
                    }
                    if (!string.IsNullOrEmpty(ncbm.NC10))
                    {
                        ncbm.NC9 = null;

                    }
                    if (!string.IsNullOrEmpty(ncbm.NC11))
                    {
                        ncbm.NC12 = null;
                    }

                    if (!string.IsNullOrEmpty(ncbm.NC1))
                    {
                        listnc.Add(ncbm.NC1);
                    }
                    if (!string.IsNullOrEmpty(ncbm.NC2))
                    {
                        if (string.IsNullOrEmpty(ncbm.NC14))
                        {
                            listnc.Add(ncbm.NC2);
                        }
                    }
                    if (!string.IsNullOrEmpty(ncbm.NC3))
                        listnc.Add(ncbm.NC3);

                    if (!string.IsNullOrEmpty(ncbm.NC4))
                        listnc.Add(ncbm.NC4);

                    if (!string.IsNullOrEmpty(ncbm.NC5))
                        listnc.Add(ncbm.NC5);

                    if (!string.IsNullOrEmpty(ncbm.NC6))
                        listnc.Add(ncbm.NC6);

                    if (!string.IsNullOrEmpty(ncbm.NC7))
                        listnc.Add(ncbm.NC7);

                    if (!string.IsNullOrEmpty(ncbm.NC8))
                        listnc.Add(ncbm.NC8);


                    if (!string.IsNullOrEmpty(ncbm.NC9))
                        listnc.Add(ncbm.NC9);

                    if (!string.IsNullOrEmpty(ncbm.NC10))
                        listnc.Add(ncbm.NC10);

                    if (!string.IsNullOrEmpty(ncbm.NC11))
                        listnc.Add(ncbm.NC11);

                    if (!string.IsNullOrEmpty(ncbm.NC12))
                        listnc.Add(ncbm.NC12);

                    if (!string.IsNullOrEmpty(ncbm.NC13))
                        listnc.Add(ncbm.NC13);

                    if (!string.IsNullOrEmpty(ncbm.NC14))
                        listnc.Add(ncbm.NC14);

                    if (!string.IsNullOrEmpty(ncbm.NC17))
                        listnc.Add(ncbm.NC17);


                    if (!string.IsNullOrEmpty(ncbm.NC18))
                        listnc.Add(ncbm.NC18);


                    if (!string.IsNullOrEmpty(ncbm.NC15))
                        listnc.Add(ncbm.NC15);

                    if (!string.IsNullOrEmpty(ncbm.NC16))
                        listnc.Add(ncbm.NC16);


                    if (!string.IsNullOrEmpty(ncbm.YNC1))
                        listnc.Add(ncbm.YNC1);

                    if (!string.IsNullOrEmpty(ncbm.YNC2))
                        listnc.Add(ncbm.YNC2);

                    if (!string.IsNullOrEmpty(ncbm.YNC3))
                        listnc.Add(ncbm.YNC3);


                    if (!string.IsNullOrEmpty(ncbm.YNC4))
                        listnc.Add(ncbm.YNC4);


                    if (!string.IsNullOrEmpty(ncbm.YNC5))
                        listnc.Add(ncbm.YNC5);

                    if (!string.IsNullOrEmpty(ncbm.YNC6))
                        listnc.Add(ncbm.YNC6);

                    if (!string.IsNullOrEmpty(ncbm.YNC7))
                        listnc.Add(ncbm.YNC7);

                    if (!string.IsNullOrEmpty(ncbm.YNC8))
                        listnc.Add(ncbm.YNC8);


                    ncbm.NonConfirmities1 = string.Empty;
                    ncbm.NonConfirmities = string.Empty;
                    foreach (string lis in listnc)
                    {

                        ncbm.NonConfirmities += "- " + lis.ToString() + Environment.NewLine;

                        string result1 = lis.Substring(0, 9);
                        if (result1 != "Future NC")
                        {
                            ncbm.NonConfirmities1 += "- " + lis.ToString() + Environment.NewLine;
                        }

                    }

                    ncbm.NonConfirmities = ncbm.NonConfirmities.TrimEnd(',');


                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                sc.ErrorLog(ex);
            }

        }

        public virtual void SaveMethod2(WorkHoursClass ob)
        {
            //For blank insertion...

            try
            {
                string currentTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                var checkuser = sc.WorkHourss.Where(x => x.UserName.Equals(ob.UserName) && x.dates.Equals(ob.dates) && x.WRID.Equals(ob.WRID) && x.Position.Equals(ob.Position)).FirstOrDefault();
                if (checkuser == null)
                {
                    var obj = new WorkHoursClass()
                    {
                        UserName = ob.UserName,
                        FullName = ob.FullName,
                        Position = ob.Position,
                        Department = ob.Department,
                        TotalHours = 0.0M,
                        RestHours = 24.0M,
                        Remarks = string.Empty,
                        options = ob.options,
                        options1 = "",
                        RestHourAny24 = "24",
                        RestHourAny7day = "168",
                        dates = ob.dates,
                        NonConfirmities = ob.NonConfirmities,
                        NonConfirmities1 = ob.NonConfirmities1,
                        Last_update = Convert.ToDateTime(currentTime),
                        datetimes = Convert.ToDateTime(currentTime),
                        opa = false,   // have to set after 
                        opayoung = ob.opayoung,
                        Cellcount = ob.Cellcount,
                        AdminAlarm = !string.IsNullOrEmpty(ob.NonConfirmities) ? true : false,
                        MasterAlarm = !string.IsNullOrEmpty(ob.NonConfirmities) ? true : false,
                        HODAlarm = !string.IsNullOrEmpty(ob.NonConfirmities) ? true : false,
                        NC1 = string.Empty,
                        NCD1 = null,
                        MNCCD1 = null,
                        HNCCD1 = null,
                        NC2 = string.Empty,
                        NCD2 = null,
                        MNCCD2 = null,
                        HNCCD2 = null,
                        NC3 = string.Empty,
                        NCD3 = null,
                        MNCCD3 = null,
                        HNCCD3 = null,
                        NC4 = string.Empty,
                        NCD4 = null,
                        MNCCD4 = null,
                        HNCCD4 = null,
                        NC5 = string.Empty,
                        NCD5 = null,
                        MNCCD5 = null,
                        HNCCD5 = null,
                        NC6 = string.Empty,
                        NCD6 = null,
                        MNCCD6 = null,
                        HNCCD6 = null,
                        NC7 = string.Empty,
                        NCD7 = null,
                        MNCCD7 = null,
                        HNCCD7 = null,
                        NC8 = string.Empty,
                        NCD8 = null,
                        MNCCD8 = null,
                        HNCCD8 = null,
                        NC9 = string.Empty,
                        NCD9 = null,
                        MNCCD9 = null,
                        HNCCD9 = null,
                        NC10 = string.Empty,
                        NCD10 = null,
                        MNCCD10 = null,
                        HNCCD10 = null,
                        NC11 = string.Empty,
                        NCD11 = null,
                        MNCCD11 = null,
                        HNCCD11 = null,
                        NC12 = string.Empty,
                        NCD12 = null,
                        MNCCD12 = null,
                        HNCCD12 = null,
                        NC13 = string.Empty,
                        NCD13 = null,
                        MNCCD13 = null,
                        HNCCD13 = null,
                        NC14 = string.Empty,
                        NCD14 = null,
                        MNCCD14 = null,
                        HNCCD14 = null,
                        NC15 = string.Empty,
                        NCD15 = null,
                        MNCCD15 = null,
                        HNCCD15 = null,
                        NC16 = string.Empty,
                        NCD16 = null,
                        MNCCD16 = null,
                        HNCCD16 = null,
                        NC17 = string.Empty,
                        NCD17 = null,
                        MNCCD17 = null,
                        HNCCD17 = null,
                        NC18 = string.Empty,
                        NCD18 = null,
                        MNCCD18 = null,
                        HNCCD18 = null,
                        hrs = ob.hrs,
                        Colors = ob.Colors,
                        ColorName = string.IsNullOrEmpty(ob.NonConfirmities) ? "White" : "Red",
                        MonthName = ob.dates.ToString("MMMM"),
                        YearValue = ob.dates.Year.ToString(),
                        WRID = ob.WRID,
                        Days = ob.dates.DayOfWeek.ToString().Substring(0, 3),
                        StatusCrew = true,
                        CrewNC = string.IsNullOrEmpty(ob.NonConfirmities) ? false : true,
                        MasterCrewNC = string.IsNullOrEmpty(ob.NonConfirmities) ? false : true,
                        HODCrewNC = string.IsNullOrEmpty(ob.NonConfirmities) ? false : true


                    };

                    var cid = sc.CrewDetails.Select(s => new { s.Id, s.UserName, s.position }).Where(x => x.UserName.Equals(obj.UserName) && x.position.Equals(obj.Position)).FirstOrDefault().Id;
                    obj.cid = cid;

                    //.......OverTime Calculation....

                    string dayofFirstDay = ob.dates.DayOfWeek.ToString().Substring(0, 3);
                    var overtim = sc.OverTimes.Where(x => x.UserName.Equals(obj.UserName)).FirstOrDefault();

                    int normal = 0;
                    int Sat = 0;
                    int Sun = 0;
                    int Holiday = 0;
                    int hr = 0;

                    if (overtim != null)
                    {
                        normal = Convert.ToInt32(overtim.NrmlWHrs);
                        Sat = Convert.ToInt32(overtim.SatWHrs);
                        Sun = Convert.ToInt32(overtim.SunWhrs);
                        Holiday = Convert.ToInt32(overtim.HolidayWHrs);
                    }

                    if ((dayofFirstDay == "Mon") || (dayofFirstDay == "Tue") || (dayofFirstDay == "Wed") || (dayofFirstDay == "Thu") || (dayofFirstDay == "Fri"))
                    {
                        hr = normal;
                        obj.Normal_WKH = normal;
                    }
                    else if ((dayofFirstDay == "Sat"))
                    {
                        hr = Sat;
                        obj.Normal_WKH = Sat;

                    }
                    else if ((dayofFirstDay == "Sun"))
                    {
                        hr = Sun;
                        obj.Normal_WKH = Sun;

                    }
                    else
                    {
                        hr = Holiday;
                        obj.Normal_WKH = Holiday;
                    }
                    double wh = Convert.ToDouble(obj.TotalHours);
                    double overtime = 0.0;
                    overtime = wh - hr;
                    if (overtime > 0.0)
                    {
                        overtime = wh - hr;
                    }
                    else
                    {
                        overtime = 0.0;
                    }

                    string dayname = "";
                    dayname = dayofFirstDay + "WHrs";
                    int dayscount = 0;

                    var checkholiday = sc.Holidays.Where(x => x.HolidayDate == obj.dates).FirstOrDefault();
                    if (checkholiday != null)
                    {
                        dayscount = overtim != null ? overtim.HolidayWHrs : 0;
                    }
                    else
                    {
                        if (dayname == "SatWHrs")
                            dayscount = overtim != null ? overtim.SatWHrs : 0;

                        else if (dayname == "SunWHrs")
                            dayscount = overtim != null ? overtim.SunWhrs : 0;
                        else
                            dayscount = overtim != null ? overtim.NrmlWHrs : 0;

                    }

                    decimal ovcou = Convert.ToDecimal(obj.TotalHours);
                    decimal ovcou1 = Convert.ToDecimal(dayscount);

                    decimal finalovcou1 = ovcou - ovcou1;
                    finalovcou1 = finalovcou1 > 0 ? finalovcou1 : Convert.ToDecimal(0.0);
                    obj.NormalOT1 = dayscount;
                    obj.overtime = finalovcou1;
                    obj.RuleNames = string.IsNullOrEmpty(obj.RuleNames) ? "Normal" : obj.RuleNames;



                    string[] hrsFetch = ob.hrs.TrimEnd(',').Split(',');
                    int i = 0;
                    int count = hrsFetch.Count();

                    if (i == 0 && i < count)
                        obj.Col1 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col1 = null;
                    i++;
                    if (i == 1 && i < count)
                        obj.Col2 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col2 = null;
                    i++;
                    if (i == 2 && i < count)
                        obj.Col3 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col3 = null;
                    i++;
                    if (i == 3 && i < count)
                        obj.Col4 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col4 = null;
                    i++;
                    if (i == 4 && i < count)
                        obj.Col5 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col5 = null;
                    i++;
                    if (i == 5 && i < count)
                        obj.Col6 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col6 = null;
                    i++;
                    if (i == 6 && i < count)
                        obj.Col7 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col7 = null;
                    i++;
                    if (i == 7 && i < count)
                        obj.Col8 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col8 = null;
                    i++;
                    if (i == 8 && i < count)
                        obj.Col9 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col9 = null;
                    i++;
                    if (i == 9 && i < count)
                        obj.Col10 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col10 = null;
                    i++;
                    if (i == 10 && i < count)
                        obj.Col11 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col11 = null;
                    i++;
                    if (i == 11 && i < count)
                        obj.Col12 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col12 = null;
                    i++;
                    if (i == 12 && i < count)
                        obj.Col13 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col13 = null;
                    i++;
                    if (i == 13 && i < count)
                        obj.Col14 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col14 = null;
                    i++;
                    if (i == 14 && i < count)
                        obj.Col15 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col15 = null;
                    i++;
                    if (i == 15 && i < count)
                        obj.Col16 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col16 = null;
                    i++;
                    if (i == 16 && i < count)
                        obj.Col17 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col17 = null;
                    i++;
                    if (i == 17 && i < count)
                        obj.Col18 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col18 = null;
                    i++;
                    if (i == 18 && i < count)
                        obj.Col19 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col19 = null;
                    i++;
                    if (i == 19 && i < count)
                        obj.Col20 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col20 = null;
                    i++;
                    if (i == 20 && i < count)
                        obj.Col21 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col21 = null;
                    i++;
                    if (i == 21 && i < count)
                        obj.Col22 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col22 = null;
                    i++;
                    if (i == 22 && i < count)
                        obj.Col23 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col23 = null;
                    i++;
                    if (i == 23 && i < count)
                        obj.Col24 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col24 = null;
                    i++;
                    if (i == 24 && i < count)
                        obj.Col25 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col25 = null;
                    i++;
                    if (i == 25 && i < count)
                        obj.Col26 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col26 = null;
                    i++;
                    if (i == 26 && i < count)
                        obj.Col27 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col27 = null;
                    i++;
                    if (i == 27 && i < count)
                        obj.Col28 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col28 = null;
                    i++;
                    if (i == 28 && i < count)
                        obj.Col29 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col29 = null;
                    i++;
                    if (i == 29 && i < count)
                        obj.Col30 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col30 = null;
                    i++;
                    if (i == 30 && i < count)
                        obj.Col31 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col31 = null;
                    i++;
                    if (i == 31 && i < count)
                        obj.Col32 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col32 = null;
                    i++;
                    if (i == 32 && i < count)
                        obj.Col33 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col33 = null;
                    i++;
                    if (i == 33 && i < count)
                        obj.Col34 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col34 = null;
                    i++;
                    if (i == 34 && i < count)
                        obj.Col35 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col35 = null;
                    i++;
                    if (i == 35 && i < count)
                        obj.Col36 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col36 = null;
                    i++;
                    if (i == 36 && i < count)
                        obj.Col37 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col37 = null;
                    i++;
                    if (i == 37 && i < count)
                        obj.Col38 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col38 = null;
                    i++;
                    if (i == 38 && i < count)
                        obj.Col39 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col39 = null;
                    i++;
                    if (i == 39 && i < count)
                        obj.Col40 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col40 = null;
                    i++;
                    if (i == 40 && i < count)
                        obj.Col41 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col41 = null;
                    i++;
                    if (i == 41 && i < count)
                        obj.Col42 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col42 = null;
                    i++;
                    if (i == 42 && i < count)
                        obj.Col43 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col43 = null;
                    i++;
                    if (i == 43 && i < count)
                        obj.Col44 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col44 = null;
                    i++;
                    if (i == 44 && i < count)
                        obj.Col45 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col45 = null;
                    i++;
                    if (i == 45 && i < count)
                        obj.Col46 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col46 = null;
                    i++;
                    if (i == 46 && i < count)
                        obj.Col47 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col47 = null;
                    i++;
                    if (i == 47 && i < count)
                        obj.Col48 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col48 = null;
                    i++;
                    if (i == 48 && i < count)
                        obj.Col49 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col49 = null;
                    i++;
                    if (i == 49 && i < count)
                        obj.Col50 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col50 = null;
                    i++;
                    if (i == 50 && i < count)
                        obj.Col51 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col51 = null;
                    i++;
                    if (i == 51 && i < count)
                        obj.Col52 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col52 = null;
                    i++;
                    if (i == 52 && i < count)
                        obj.Col53 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col53 = null;
                    i++;

                    if (i == 53 && i < count)
                        obj.Col54 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col54 = null;
                    i++;
                    if (i == 54 && i < count)
                        obj.Col55 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col55 = null;
                    i++;
                    if (i == 55 && i < count)
                        obj.Col56 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col56 = null;
                    i++;
                    if (i == 56 && i < count)
                        obj.Col57 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col57 = null;
                    i++;
                    if (i == 57 && i < count)
                        obj.Col58 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col58 = null;
                    i++;
                    if (i == 58 && i < count)
                        obj.Col59 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col59 = null;
                    i++;
                    if (i == 59 && i < count)
                        obj.Col60 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col60 = null;
                    i++;
                    if (i == 60 && i < count)
                        obj.Col61 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col61 = null;
                    i++;
                    if (i == 61 && i < count)
                        obj.Col62 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col62 = null;
                    i++;
                    if (i == 62 && i < count)
                        obj.Col63 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col63 = null;
                    i++;
                    if (i == 63 && i < count)
                        obj.Col64 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col64 = null;
                    i++;
                    if (i == 64 && i < count)
                        obj.Col65 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col65 = null;
                    i++;
                    if (i == 65 && i < count)
                        obj.Col66 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col66 = null;
                    i++;
                    if (i == 66 && i < count)
                        obj.Col67 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col67 = null;
                    i++;
                    if (i == 67 && i < count)
                        obj.Col68 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col68 = null;
                    i++;
                    if (i == 68 && i < count)
                        obj.Col69 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col69 = null;
                    i++;
                    if (i == 69 && i < count)
                        obj.Col70 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col70 = null;
                    i++;
                    if (i == 70 && i < count)
                        obj.Col71 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col71 = null;
                    i++;
                    if (i == 71 && i < count)
                        obj.Col72 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col72 = null;
                    i++;
                    if (i == 72 && i < count)
                        obj.Col73 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col73 = null;
                    i++;
                    if (i == 73 && i < count)
                        obj.Col74 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col74 = null;
                    i++;
                    if (i == 74 && i < count)
                        obj.Col75 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col75 = null;
                    i++;


                    if (i == 75 && i < count)
                        obj.Col76 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col76 = null;
                    i++;
                    if (i == 76 && i < count)
                        obj.Col77 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col77 = null;
                    i++;
                    if (i == 77 && i < count)
                        obj.Col78 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col78 = null;
                    i++;
                    if (i == 78 && i < count)
                        obj.Col79 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col79 = null;
                    i++;
                    if (i == 79 && i < count)
                        obj.Col80 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col80 = null;
                    i++;
                    if (i == 80 && i < count)
                        obj.Col81 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col81 = null;
                    i++;
                    if (i == 81 && i < count)
                        obj.Col82 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col82 = null;
                    i++;
                    if (i == 82 && i < count)
                        obj.Col83 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col83 = null;
                    i++;
                    if (i == 83 && i < count)
                        obj.Col84 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col84 = null;
                    i++;
                    if (i == 84 && i < count)
                        obj.Col85 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col85 = null;
                    i++;
                    if (i == 85 && i < count)
                        obj.Col86 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col86 = null;
                    i++;
                    if (i == 86 && i < count)
                        obj.Col87 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col87 = null;
                    i++;
                    if (i == 87 && i < count)
                        obj.Col88 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col88 = null;
                    i++;
                    if (i == 88 && i < count)
                        obj.Col89 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col89 = null;
                    i++;
                    if (i == 89 && i < count)
                        obj.Col90 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col90 = null;
                    i++;
                    if (i == 90 && i < count)
                        obj.Col91 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col91 = null;
                    i++;
                    if (i == 91 && i < count)
                        obj.Col92 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col92 = null;
                    i++;
                    if (i == 92 && i < count)
                        obj.Col93 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col93 = null;
                    i++;
                    if (i == 93 && i < count)
                        obj.Col94 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col94 = null;
                    i++;
                    if (i == 94 && i < count)
                        obj.Col95 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col95 = null;
                    i++;
                    if (i == 95 && i < count)
                        obj.Col96 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col96 = null;
                    i++;
                    if (i == 96 && i < count)
                        obj.Col97 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col97 = null;
                    i++;
                    if (i == 97 && i < count)
                        obj.Col98 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col98 = null;
                    i++;
                    if (i == 98 && i < count)
                        obj.Col99 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col99 = null;
                    i++;
                    if (i == 99 && i < count)
                        obj.Col100 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col100 = null;
                    i++;
                    if (i == 100 && i < count)
                        obj.Col101 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col101 = null;
                    i++;
                    if (i == 101 && i < count)
                        obj.Col102 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col102 = null;
                    i++;
                    if (i == 102 && i < count)
                        obj.Col103 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col103 = null;
                    i++;
                    if (i == 103 && i < count)
                        obj.Col104 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col104 = null;
                    i++;
                    if (i == 104 && i < count)
                        obj.Col105 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col105 = null;
                    i++;
                    if (i == 105 && i < count)
                        obj.Col106 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col106 = null;
                    i++;
                    if (i == 106 && i < count)
                        obj.Col107 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col107 = null;
                    i++;
                    if (i == 107 && i < count)
                        obj.Col108 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col108 = null;
                    i++;
                    if (i == 108 && i < count)
                        obj.Col109 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col109 = null;
                    i++;
                    if (i == 109 && i < count)
                        obj.Col110 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col110 = null;
                    i++;
                    if (i == 110 && i < count)
                        obj.Col111 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col111 = null;
                    i++;
                    if (i == 111 && i < count)
                        obj.Col112 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col112 = null;
                    i++;
                    if (i == 112 && i < count)
                        obj.Col113 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col113 = null;
                    i++;
                    if (i == 113 && i < count)
                        obj.Col114 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col114 = null;
                    i++;
                    if (i == 114 && i < count)
                        obj.Col115 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col115 = null;
                    i++;
                    if (i == 115 && i < count)
                        obj.Col116 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col116 = null;
                    i++;
                    if (i == 116 && i < count)
                        obj.Col117 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col117 = null;
                    i++;
                    if (i == 117 && i < count)
                        obj.Col118 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col118 = null;
                    i++;
                    if (i == 118 && i < count)
                        obj.Col119 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col119 = null;
                    i++;
                    if (i == 119 && i < count)
                        obj.Col120 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col120 = null;
                    i++;



                    sc.WorkHourss.Add(obj);
                    sc.SaveChanges();





                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public virtual void SaveMethod1(WorkHoursClass ob)
        {
            try
            {
                string currentTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                var checkuser = sc.WorkHourss.Where(x => x.UserName.Equals(ob.UserName) && x.dates.Equals(ob.dates) && x.WRID.Equals(ob.WRID) && x.Position.Equals(ob.Position)).FirstOrDefault();
                if (checkuser == null)
                {
                    var obj = new WorkHoursClass()
                    {
                        UserName = ob.UserName,
                        FullName = ob.FullName,
                        Position = ob.Position,
                        Department = ob.Department,
                        TotalHours = ob.TotalHours,
                        RestHours = ob.RestHours,
                        Remarks = ob.Remarks,
                        options = ob.options,
                        options1 = ":",
                        RestHourAny24 = ob.RestHourAny24,
                        RestHourAny7day = ob.RestHourAny7day,
                        dates = ob.dates,
                        NonConfirmities = ob.NonConfirmities,
                        NonConfirmities1 = ob.NonConfirmities1,
                        Last_update = Convert.ToDateTime(currentTime),
                        datetimes = Convert.ToDateTime(currentTime),
                        opa = false,   // have to set after 
                        opayoung = ob.opayoung,
                        Cellcount = ob.Cellcount,
                        AdminAlarm = !string.IsNullOrEmpty(ob.NonConfirmities) ? true : false,
                        MasterAlarm = !string.IsNullOrEmpty(ob.NonConfirmities) ? true : false,
                        HODAlarm = !string.IsNullOrEmpty(ob.NonConfirmities) ? true : false,
                        NC1 = ob.NC1,
                        NCD1 = null,
                        MNCCD1 = null,
                        HNCCD1 = null,
                        NC2 = ob.NC2,
                        NCD2 = null,
                        MNCCD2 = null,
                        HNCCD2 = null,
                        NC3 = ob.NC3,
                        NCD3 = null,
                        MNCCD3 = null,
                        HNCCD3 = null,
                        NC4 = ob.NC4,
                        NCD4 = null,
                        MNCCD4 = null,
                        HNCCD4 = null,
                        NC5 = ob.NC5,
                        NCD5 = null,
                        MNCCD5 = null,
                        HNCCD5 = null,
                        NC6 = ob.NC6,
                        NCD6 = null,
                        MNCCD6 = null,
                        HNCCD6 = null,
                        NC7 = ob.NC7,
                        NCD7 = null,
                        MNCCD7 = null,
                        HNCCD7 = null,
                        NC8 = ob.NC8,
                        NCD8 = null,
                        MNCCD8 = null,
                        HNCCD8 = null,
                        NC9 = ob.NC9,
                        NCD9 = null,
                        MNCCD9 = null,
                        HNCCD9 = null,
                        NC10 = ob.NC10,
                        NCD10 = null,
                        MNCCD10 = null,
                        HNCCD10 = null,
                        NC11 = ob.NC11,
                        NCD11 = null,
                        MNCCD11 = null,
                        HNCCD11 = null,
                        NC12 = ob.NC12,
                        NCD12 = null,
                        MNCCD12 = null,
                        HNCCD12 = null,
                        NC13 = ob.NC13,
                        NCD13 = null,
                        MNCCD13 = null,
                        HNCCD13 = null,
                        NC14 = ob.NC14,
                        NCD14 = null,
                        MNCCD14 = null,
                        HNCCD14 = null,
                        NC15 = ob.NC15,
                        NCD15 = null,
                        MNCCD15 = null,
                        HNCCD15 = null,
                        NC16 = ob.NC16,
                        NCD16 = null,
                        MNCCD16 = null,
                        HNCCD16 = null,
                        NC17 = ob.NC17,
                        NCD17 = null,
                        MNCCD17 = null,
                        HNCCD17 = null,
                        NC18 = ob.NC18,
                        NCD18 = null,
                        MNCCD18 = null,
                        HNCCD18 = null,
                        hrs = ob.hrs,
                        Colors = ob.Colors,
                        ColorName = string.IsNullOrEmpty(ob.NonConfirmities) ? "White" : "Red",
                        MonthName = ob.dates.ToString("MMMM"),
                        YearValue = ob.dates.Year.ToString(),
                        WRID = ob.WRID,
                        Days = ob.dates.DayOfWeek.ToString().Substring(0, 3),
                        StatusCrew = true,
                        CrewNC = string.IsNullOrEmpty(ob.NonConfirmities) ? false : true,
                        MasterCrewNC = string.IsNullOrEmpty(ob.NonConfirmities) ? false : true,
                        HODCrewNC = string.IsNullOrEmpty(ob.NonConfirmities) ? false : true


                    };

                    var cid = sc.CrewDetails.Select(s => new { s.Id, s.UserName, s.position }).Where(x => x.UserName.Equals(obj.UserName) && x.position.Equals(obj.Position)).FirstOrDefault().Id;
                    obj.cid = cid;

                    //.......OverTime Calculation....

                    string dayofFirstDay = ob.dates.DayOfWeek.ToString().Substring(0, 3);
                    var overtim = sc.OverTimes.Where(x => x.UserName.Equals(obj.UserName)).FirstOrDefault();

                    int normal = 0;
                    int Sat = 0;
                    int Sun = 0;
                    int Holiday = 0;
                    int hr = 0;

                    if (overtim != null)
                    {
                        normal = Convert.ToInt32(overtim.NrmlWHrs);
                        Sat = Convert.ToInt32(overtim.SatWHrs);
                        Sun = Convert.ToInt32(overtim.SunWhrs);
                        Holiday = Convert.ToInt32(overtim.HolidayWHrs);
                    }

                    if ((dayofFirstDay == "Mon") || (dayofFirstDay == "Tue") || (dayofFirstDay == "Wed") || (dayofFirstDay == "Thu") || (dayofFirstDay == "Fri"))
                    {
                        hr = normal;
                        obj.Normal_WKH = normal;
                    }
                    else if ((dayofFirstDay == "Sat"))
                    {
                        hr = Sat;
                        obj.Normal_WKH = Sat;

                    }
                    else if ((dayofFirstDay == "Sun"))
                    {
                        hr = Sun;
                        obj.Normal_WKH = Sun;

                    }
                    else
                    {
                        hr = Holiday;
                        obj.Normal_WKH = Holiday;
                    }
                    double wh = Convert.ToDouble(obj.TotalHours);
                    double overtime = 0.0;
                    overtime = wh - hr;
                    overtime = overtime > 0 ? overtime : 0.0;



                    string dayname = "";
                    dayname = dayofFirstDay + "WHrs";
                    int dayscount = 0;

                    var checkholiday = sc.Holidays.Where(x => x.HolidayDate == obj.dates).FirstOrDefault();
                    if (checkholiday != null)
                    {
                        dayscount = overtim != null ? overtim.HolidayWHrs : 0;
                    }
                    else
                    {
                        if (dayname == "SatWHrs")
                            dayscount = overtim != null ? overtim.SatWHrs : 0;

                        else if (dayname == "SunWHrs")
                            dayscount = overtim != null ? overtim.SunWhrs : 0;
                        else
                            dayscount = overtim != null ? overtim.NrmlWHrs : 0;

                    }

                    decimal ovcou = Convert.ToDecimal(obj.TotalHours);
                    decimal ovcou1 = Convert.ToDecimal(dayscount);

                    decimal finalovcou1 = ovcou - ovcou1;
                    finalovcou1 = finalovcou1 > 0 ? finalovcou1 : Convert.ToDecimal(0.0);
                    obj.NormalOT1 = dayscount;
                    obj.overtime = finalovcou1;
                    obj.RuleNames = string.IsNullOrEmpty(obj.RuleNames) ? "Normal" : obj.RuleNames;



                    string[] hrsFetch = ob.hrs.TrimEnd(',').Split(',');
                    int i = 0;
                    int count = hrsFetch.Count();

                    if (i == 0 && i < count)
                        obj.Col1 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col1 = null;
                    i++;
                    if (i == 1 && i < count)
                        obj.Col2 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col2 = null;
                    i++;
                    if (i == 2 && i < count)
                        obj.Col3 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col3 = null;
                    i++;
                    if (i == 3 && i < count)
                        obj.Col4 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col4 = null;
                    i++;
                    if (i == 4 && i < count)
                        obj.Col5 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col5 = null;
                    i++;
                    if (i == 5 && i < count)
                        obj.Col6 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col6 = null;
                    i++;
                    if (i == 6 && i < count)
                        obj.Col7 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col7 = null;
                    i++;
                    if (i == 7 && i < count)
                        obj.Col8 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col8 = null;
                    i++;
                    if (i == 8 && i < count)
                        obj.Col9 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col9 = null;
                    i++;
                    if (i == 9 && i < count)
                        obj.Col10 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col10 = null;
                    i++;
                    if (i == 10 && i < count)
                        obj.Col11 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col11 = null;
                    i++;
                    if (i == 11 && i < count)
                        obj.Col12 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col12 = null;
                    i++;
                    if (i == 12 && i < count)
                        obj.Col13 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col13 = null;
                    i++;
                    if (i == 13 && i < count)
                        obj.Col14 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col14 = null;
                    i++;
                    if (i == 14 && i < count)
                        obj.Col15 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col15 = null;
                    i++;
                    if (i == 15 && i < count)
                        obj.Col16 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col16 = null;
                    i++;
                    if (i == 16 && i < count)
                        obj.Col17 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col17 = null;
                    i++;
                    if (i == 17 && i < count)
                        obj.Col18 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col18 = null;
                    i++;
                    if (i == 18 && i < count)
                        obj.Col19 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col19 = null;
                    i++;
                    if (i == 19 && i < count)
                        obj.Col20 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col20 = null;
                    i++;
                    if (i == 20 && i < count)
                        obj.Col21 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col21 = null;
                    i++;
                    if (i == 21 && i < count)
                        obj.Col22 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col22 = null;
                    i++;
                    if (i == 22 && i < count)
                        obj.Col23 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col23 = null;
                    i++;
                    if (i == 23 && i < count)
                        obj.Col24 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col24 = null;
                    i++;
                    if (i == 24 && i < count)
                        obj.Col25 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col25 = null;
                    i++;
                    if (i == 25 && i < count)
                        obj.Col26 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col26 = null;
                    i++;
                    if (i == 26 && i < count)
                        obj.Col27 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col27 = null;
                    i++;
                    if (i == 27 && i < count)
                        obj.Col28 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col28 = null;
                    i++;
                    if (i == 28 && i < count)
                        obj.Col29 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col29 = null;
                    i++;
                    if (i == 29 && i < count)
                        obj.Col30 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col30 = null;
                    i++;
                    if (i == 30 && i < count)
                        obj.Col31 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col31 = null;
                    i++;
                    if (i == 31 && i < count)
                        obj.Col32 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col32 = null;
                    i++;
                    if (i == 32 && i < count)
                        obj.Col33 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col33 = null;
                    i++;
                    if (i == 33 && i < count)
                        obj.Col34 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col34 = null;
                    i++;
                    if (i == 34 && i < count)
                        obj.Col35 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col35 = null;
                    i++;
                    if (i == 35 && i < count)
                        obj.Col36 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col36 = null;
                    i++;
                    if (i == 36 && i < count)
                        obj.Col37 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col37 = null;
                    i++;
                    if (i == 37 && i < count)
                        obj.Col38 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col38 = null;
                    i++;
                    if (i == 38 && i < count)
                        obj.Col39 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col39 = null;
                    i++;
                    if (i == 39 && i < count)
                        obj.Col40 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col40 = null;
                    i++;
                    if (i == 40 && i < count)
                        obj.Col41 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col41 = null;
                    i++;
                    if (i == 41 && i < count)
                        obj.Col42 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col42 = null;
                    i++;
                    if (i == 42 && i < count)
                        obj.Col43 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col43 = null;
                    i++;
                    if (i == 43 && i < count)
                        obj.Col44 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col44 = null;
                    i++;
                    if (i == 44 && i < count)
                        obj.Col45 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col45 = null;
                    i++;
                    if (i == 45 && i < count)
                        obj.Col46 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col46 = null;
                    i++;
                    if (i == 46 && i < count)
                        obj.Col47 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col47 = null;
                    i++;
                    if (i == 47 && i < count)
                        obj.Col48 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col48 = null;
                    i++;
                    if (i == 48 && i < count)
                        obj.Col49 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col49 = null;
                    i++;
                    if (i == 49 && i < count)
                        obj.Col50 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col50 = null;
                    i++;
                    if (i == 50 && i < count)
                        obj.Col51 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col51 = null;
                    i++;
                    if (i == 51 && i < count)
                        obj.Col52 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col52 = null;
                    i++;
                    if (i == 52 && i < count)
                        obj.Col53 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col53 = null;
                    i++;

                    if (i == 53 && i < count)
                        obj.Col54 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col54 = null;
                    i++;
                    if (i == 54 && i < count)
                        obj.Col55 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col55 = null;
                    i++;
                    if (i == 55 && i < count)
                        obj.Col56 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col56 = null;
                    i++;
                    if (i == 56 && i < count)
                        obj.Col57 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col57 = null;
                    i++;
                    if (i == 57 && i < count)
                        obj.Col58 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col58 = null;
                    i++;
                    if (i == 58 && i < count)
                        obj.Col59 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col59 = null;
                    i++;
                    if (i == 59 && i < count)
                        obj.Col60 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col60 = null;
                    i++;
                    if (i == 60 && i < count)
                        obj.Col61 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col61 = null;
                    i++;
                    if (i == 61 && i < count)
                        obj.Col62 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col62 = null;
                    i++;
                    if (i == 62 && i < count)
                        obj.Col63 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col63 = null;
                    i++;
                    if (i == 63 && i < count)
                        obj.Col64 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col64 = null;
                    i++;
                    if (i == 64 && i < count)
                        obj.Col65 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col65 = null;
                    i++;
                    if (i == 65 && i < count)
                        obj.Col66 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col66 = null;
                    i++;
                    if (i == 66 && i < count)
                        obj.Col67 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col67 = null;
                    i++;
                    if (i == 67 && i < count)
                        obj.Col68 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col68 = null;
                    i++;
                    if (i == 68 && i < count)
                        obj.Col69 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col69 = null;
                    i++;
                    if (i == 69 && i < count)
                        obj.Col70 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col70 = null;
                    i++;
                    if (i == 70 && i < count)
                        obj.Col71 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col71 = null;
                    i++;
                    if (i == 71 && i < count)
                        obj.Col72 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col72 = null;
                    i++;
                    if (i == 72 && i < count)
                        obj.Col73 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col73 = null;
                    i++;
                    if (i == 73 && i < count)
                        obj.Col74 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col74 = null;
                    i++;
                    if (i == 74 && i < count)
                        obj.Col75 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col75 = null;
                    i++;


                    if (i == 75 && i < count)
                        obj.Col76 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col76 = null;
                    i++;
                    if (i == 76 && i < count)
                        obj.Col77 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col77 = null;
                    i++;
                    if (i == 77 && i < count)
                        obj.Col78 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col78 = null;
                    i++;
                    if (i == 78 && i < count)
                        obj.Col79 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col79 = null;
                    i++;
                    if (i == 79 && i < count)
                        obj.Col80 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col80 = null;
                    i++;
                    if (i == 80 && i < count)
                        obj.Col81 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col81 = null;
                    i++;
                    if (i == 81 && i < count)
                        obj.Col82 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col82 = null;
                    i++;
                    if (i == 82 && i < count)
                        obj.Col83 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col83 = null;
                    i++;
                    if (i == 83 && i < count)
                        obj.Col84 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col84 = null;
                    i++;
                    if (i == 84 && i < count)
                        obj.Col85 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col85 = null;
                    i++;
                    if (i == 85 && i < count)
                        obj.Col86 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col86 = null;
                    i++;
                    if (i == 86 && i < count)
                        obj.Col87 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col87 = null;
                    i++;
                    if (i == 87 && i < count)
                        obj.Col88 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col88 = null;
                    i++;
                    if (i == 88 && i < count)
                        obj.Col89 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col89 = null;
                    i++;
                    if (i == 89 && i < count)
                        obj.Col90 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col90 = null;
                    i++;
                    if (i == 90 && i < count)
                        obj.Col91 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col91 = null;
                    i++;
                    if (i == 91 && i < count)
                        obj.Col92 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col92 = null;
                    i++;
                    if (i == 92 && i < count)
                        obj.Col93 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col93 = null;
                    i++;
                    if (i == 93 && i < count)
                        obj.Col94 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col94 = null;
                    i++;
                    if (i == 94 && i < count)
                        obj.Col95 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col95 = null;
                    i++;
                    if (i == 95 && i < count)
                        obj.Col96 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col96 = null;
                    i++;
                    if (i == 96 && i < count)
                        obj.Col97 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col97 = null;
                    i++;
                    if (i == 97 && i < count)
                        obj.Col98 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col98 = null;
                    i++;
                    if (i == 98 && i < count)
                        obj.Col99 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col99 = null;
                    i++;
                    if (i == 99 && i < count)
                        obj.Col100 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col100 = null;
                    i++;
                    if (i == 100 && i < count)
                        obj.Col101 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col101 = null;
                    i++;
                    if (i == 101 && i < count)
                        obj.Col102 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col102 = null;
                    i++;
                    if (i == 102 && i < count)
                        obj.Col103 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col103 = null;
                    i++;
                    if (i == 103 && i < count)
                        obj.Col104 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col104 = null;
                    i++;
                    if (i == 104 && i < count)
                        obj.Col105 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col105 = null;
                    i++;
                    if (i == 105 && i < count)
                        obj.Col106 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col106 = null;
                    i++;
                    if (i == 106 && i < count)
                        obj.Col107 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col107 = null;
                    i++;
                    if (i == 107 && i < count)
                        obj.Col108 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col108 = null;
                    i++;
                    if (i == 108 && i < count)
                        obj.Col109 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col109 = null;
                    i++;
                    if (i == 109 && i < count)
                        obj.Col110 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col110 = null;
                    i++;
                    if (i == 110 && i < count)
                        obj.Col111 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col111 = null;
                    i++;
                    if (i == 111 && i < count)
                        obj.Col112 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col112 = null;
                    i++;
                    if (i == 112 && i < count)
                        obj.Col113 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col113 = null;
                    i++;
                    if (i == 113 && i < count)
                        obj.Col114 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col114 = null;
                    i++;
                    if (i == 114 && i < count)
                        obj.Col115 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col115 = null;
                    i++;
                    if (i == 115 && i < count)
                        obj.Col116 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col116 = null;
                    i++;
                    if (i == 116 && i < count)
                        obj.Col117 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col117 = null;
                    i++;
                    if (i == 117 && i < count)
                        obj.Col118 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col118 = null;
                    i++;
                    if (i == 118 && i < count)
                        obj.Col119 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col119 = null;
                    i++;
                    if (i == 119 && i < count)
                        obj.Col120 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col120 = null;
                    i++;

                    sc.WorkHourss.Add(obj);
                    sc.SaveChanges();



                }

                //For current date insertion...
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public virtual void Update(WorkHoursClass ob)
        {
            try
            {
                string currentTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                var obj = new WorkHoursClass();
                var checkuser = sc.WorkHourss.Where(x => x.UserName.Equals(ob.UserName) && x.dates.Equals(ob.dates) && x.WRID.Equals(ob.WRID) && x.Position.Equals(ob.Position)).FirstOrDefault();
                if (checkuser != null)
                {
                    obj.NCD1 = checkuser.NCD1;
                    obj.MNCCD1 = checkuser.MNCCD1;
                    obj.HNCCD1 = checkuser.HNCCD1;

                    obj.NCD2 = checkuser.NCD2;
                    obj.MNCCD2 = checkuser.MNCCD2;
                    obj.HNCCD2 = checkuser.HNCCD2;

                    obj.NCD3 = checkuser.NCD3;
                    obj.MNCCD3 = checkuser.MNCCD3;
                    obj.HNCCD3 = checkuser.HNCCD3;

                    obj.NCD4 = checkuser.NCD4;
                    obj.MNCCD4 = checkuser.MNCCD4;
                    obj.HNCCD4 = checkuser.HNCCD4;

                    obj.NCD5 = checkuser.NCD5;
                    obj.MNCCD5 = checkuser.MNCCD5;
                    obj.HNCCD5 = checkuser.HNCCD5;

                    obj.NCD6 = checkuser.NCD6;
                    obj.MNCCD6 = checkuser.MNCCD6;
                    obj.HNCCD6 = checkuser.HNCCD6;

                    obj.NCD7 = checkuser.NCD7;
                    obj.MNCCD7 = checkuser.MNCCD7;
                    obj.HNCCD7 = checkuser.HNCCD7;

                    obj.NCD8 = checkuser.NCD8;
                    obj.MNCCD8 = checkuser.MNCCD8;
                    obj.HNCCD8 = checkuser.HNCCD8;

                    obj.NCD9 = checkuser.NCD9;
                    obj.MNCCD9 = checkuser.MNCCD9;
                    obj.HNCCD9 = checkuser.HNCCD9;

                    obj.NCD10 = checkuser.NCD10;
                    obj.MNCCD10 = checkuser.MNCCD10;
                    obj.HNCCD10 = checkuser.HNCCD10;

                    obj.NCD11 = checkuser.NCD11;
                    obj.MNCCD11 = checkuser.MNCCD11;
                    obj.HNCCD11 = checkuser.HNCCD11;

                    obj.NCD12 = checkuser.NCD12;
                    obj.MNCCD12 = checkuser.MNCCD12;
                    obj.HNCCD12 = checkuser.HNCCD12;

                    obj.NCD13 = checkuser.NCD13;
                    obj.MNCCD13 = checkuser.MNCCD13;
                    obj.HNCCD13 = checkuser.HNCCD13;

                    obj.NCD14 = checkuser.NCD14;
                    obj.MNCCD14 = checkuser.MNCCD14;
                    obj.HNCCD14 = checkuser.HNCCD14;

                    obj.NCD15 = checkuser.NCD15;
                    obj.MNCCD15 = checkuser.MNCCD15;
                    obj.HNCCD15 = checkuser.HNCCD15;

                    obj.NCD16 = checkuser.NCD16;
                    obj.MNCCD16 = checkuser.MNCCD16;
                    obj.HNCCD16 = checkuser.HNCCD16;

                    obj.NCD17 = checkuser.NCD17;
                    obj.MNCCD17 = checkuser.MNCCD17;
                    obj.HNCCD17 = checkuser.HNCCD17;

                    obj.NCD18 = checkuser.NCD18;
                    obj.MNCCD18 = checkuser.MNCCD18;
                    obj.HNCCD18 = checkuser.HNCCD18;


                    obj.YNCD1 = checkuser.YNCD1;
                    obj.MYNCCD1 = checkuser.MYNCCD1;
                    obj.HYNCCD1 = checkuser.HYNCCD1;


                    obj.YNCD2 = checkuser.YNCD2;
                    obj.MYNCCD2 = checkuser.MYNCCD2;
                    obj.HYNCCD2 = checkuser.HYNCCD2;

                    obj.YNCD3 = checkuser.YNCD3;
                    obj.MYNCCD3 = checkuser.MYNCCD3;
                    obj.HYNCCD3 = checkuser.HYNCCD3;

                    obj.YNCD4 = checkuser.YNCD4;
                    obj.MYNCCD4 = checkuser.MYNCCD4;
                    obj.HYNCCD4 = checkuser.HYNCCD4;

                    obj.YNCD5 = checkuser.YNCD5;
                    obj.MYNCCD5 = checkuser.MYNCCD5;
                    obj.HYNCCD5 = checkuser.HYNCCD5;

                    obj.YNCD6 = checkuser.YNCD6;
                    obj.MYNCCD6 = checkuser.MYNCCD6;
                    obj.HYNCCD6 = checkuser.HYNCCD6;

                    obj.YNCD7 = checkuser.YNCD7;
                    obj.MYNCCD7 = checkuser.MYNCCD7;
                    obj.HYNCCD7 = checkuser.HYNCCD7;

                    obj.YNCD8 = checkuser.YNCD8;
                    obj.MYNCCD8 = checkuser.MYNCCD8;
                    obj.HYNCCD8 = checkuser.HYNCCD8;
                    obj.wid = checkuser.wid;
                    ob.wid = checkuser.wid;
                }

                obj.UserName = ob.UserName;
                obj.Remarks = ob.Remarks;
                obj.FullName = ob.FullName;
                obj.Position = ob.Position;
                obj.Department = ob.Department;
                obj.TotalHours = ob.TotalHours;
                obj.RestHours = ob.RestHours;
                obj.options = ob.options;
                obj.options1 = !string.IsNullOrEmpty(ob.NonConfirmities) ? ":" : string.Empty;
                obj.RestHourAny24 = ob.RestHourAny24;
                obj.RestHourAny7day = ob.RestHourAny7day;
                obj.dates = ob.dates;
                obj.NonConfirmities = ob.NonConfirmities;
                obj.NonConfirmities1 = ob.NonConfirmities1;
                obj.Last_update = Convert.ToDateTime(currentTime);
                obj.datetimes = Convert.ToDateTime(currentTime);
                obj.opa = false;   // have to set after 
                obj.opayoung = ob.opayoung;
                obj.Cellcount = ob.Cellcount;
                obj.AdminAlarm = !string.IsNullOrEmpty(ob.NonConfirmities) ? true : false;
                obj.MasterAlarm = !string.IsNullOrEmpty(ob.NonConfirmities) ? true : false;
                obj.HODAlarm = !string.IsNullOrEmpty(ob.NonConfirmities) ? true : false;
                obj.hrs = ob.hrs;
                obj.Colors = ob.Colors;
                obj.ColorName = string.IsNullOrEmpty(ob.NonConfirmities) ? "White" : "Red";
                obj.MonthName = ob.dates.ToString("MMMM");
                obj.YearValue = ob.dates.Year.ToString();
                obj.WRID = ob.WRID;
                obj.Days = ob.dates.DayOfWeek.ToString().Substring(0, 3);
                obj.StatusCrew = true;
                obj.CrewNC = string.IsNullOrEmpty(ob.NonConfirmities) ? false : true;
                obj.MasterCrewNC = string.IsNullOrEmpty(ob.NonConfirmities) ? false : true;
                obj.HODCrewNC = string.IsNullOrEmpty(ob.NonConfirmities) ? false : true;
                obj.NC1 = ob.NC1;
                obj.NC2 = ob.NC2;
                obj.NC3 = ob.NC3;
                obj.NC4 = ob.NC4;
                obj.NC5 = ob.NC5;
                obj.NC6 = ob.NC6;
                obj.NC7 = ob.NC7;
                obj.NC8 = ob.NC8;
                obj.NC9 = ob.NC9;
                obj.NC10 = ob.NC10;
                obj.NC11 = ob.NC11;
                obj.NC12 = ob.NC12;
                obj.NC13 = ob.NC13;
                obj.NC14 = ob.NC14;
                obj.NC15 = ob.NC15;
                obj.NC16 = ob.NC16;
                obj.NC17 = ob.NC17;
                obj.NC18 = ob.NC18;

                var cid = sc.CrewDetails.Select(s => new { s.Id, s.UserName, s.position }).Where(x => x.UserName.Equals(obj.UserName) && x.position.Equals(obj.Position)).FirstOrDefault().Id;
                obj.cid = cid;

                //.......OverTime Calculation....

                string dayofFirstDay = ob.dates.DayOfWeek.ToString().Substring(0, 3);
                var overtim = sc.OverTimes.Where(x => x.UserName.Equals(obj.UserName)).FirstOrDefault();

                int normal = 0;
                int Sat = 0;
                int Sun = 0;
                int Holiday = 0;
                int hr = 0;

                if (overtim != null)
                {
                    normal = Convert.ToInt32(overtim.NrmlWHrs);
                    Sat = Convert.ToInt32(overtim.SatWHrs);
                    Sun = Convert.ToInt32(overtim.SunWhrs);
                    Holiday = Convert.ToInt32(overtim.HolidayWHrs);
                }

                if ((dayofFirstDay == "Mon") || (dayofFirstDay == "Tue") || (dayofFirstDay == "Wed") || (dayofFirstDay == "Thu") || (dayofFirstDay == "Fri"))
                {
                    hr = normal;
                    obj.Normal_WKH = normal;
                }
                else if ((dayofFirstDay == "Sat"))
                {
                    hr = Sat;
                    obj.Normal_WKH = Sat;

                }
                else if ((dayofFirstDay == "Sun"))
                {
                    hr = Sun;
                    obj.Normal_WKH = Sun;

                }
                else
                {
                    hr = Holiday;
                    obj.Normal_WKH = Holiday;
                }
                double wh = Convert.ToDouble(obj.TotalHours);
                double overtime = 0.0;
                overtime = wh - hr;
                overtime = overtime > 0 ? overtime : 0.0;



                string dayname = "";
                dayname = dayofFirstDay + "WHrs";
                int dayscount = 0;

                var checkholiday = sc.Holidays.Where(x => x.HolidayDate == obj.dates).FirstOrDefault();
                if (checkholiday != null)
                {
                    dayscount = overtim != null ? overtim.HolidayWHrs : 0;
                }
                else
                {
                    if (dayname == "SatWHrs")
                        dayscount = overtim != null ? overtim.SatWHrs : 0;

                    else if (dayname == "SunWHrs")
                        dayscount = overtim != null ? overtim.SunWhrs : 0;
                    else
                        dayscount = overtim != null ? overtim.NrmlWHrs : 0;

                }

                decimal ovcou = Convert.ToDecimal(obj.TotalHours);
                decimal ovcou1 = Convert.ToDecimal(dayscount);

                decimal finalovcou1 = ovcou - ovcou1;
                finalovcou1 = finalovcou1 > 0 ? finalovcou1 : Convert.ToDecimal(0.0);
                obj.NormalOT1 = dayscount;
                obj.overtime = finalovcou1;
                obj.RuleNames = string.IsNullOrEmpty(ob.RuleNames) ? "Normal" : ob.RuleNames;




                string[] hrsFetch = ob.hrs.TrimEnd(',').Split(',');
                int i = 0;
                int count = hrsFetch.Count();

                if (i == 0 && i < count)
                    obj.Col1 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col1 = null;
                i++;
                if (i == 1 && i < count)
                    obj.Col2 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col2 = null;
                i++;
                if (i == 2 && i < count)
                    obj.Col3 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col3 = null;
                i++;
                if (i == 3 && i < count)
                    obj.Col4 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col4 = null;
                i++;
                if (i == 4 && i < count)
                    obj.Col5 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col5 = null;
                i++;
                if (i == 5 && i < count)
                    obj.Col6 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col6 = null;
                i++;
                if (i == 6 && i < count)
                    obj.Col7 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col7 = null;
                i++;
                if (i == 7 && i < count)
                    obj.Col8 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col8 = null;
                i++;
                if (i == 8 && i < count)
                    obj.Col9 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col9 = null;
                i++;
                if (i == 9 && i < count)
                    obj.Col10 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col10 = null;
                i++;
                if (i == 10 && i < count)
                    obj.Col11 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col11 = null;
                i++;
                if (i == 11 && i < count)
                    obj.Col12 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col12 = null;
                i++;
                if (i == 12 && i < count)
                    obj.Col13 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col13 = null;
                i++;
                if (i == 13 && i < count)
                    obj.Col14 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col14 = null;
                i++;
                if (i == 14 && i < count)
                    obj.Col15 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col15 = null;
                i++;
                if (i == 15 && i < count)
                    obj.Col16 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col16 = null;
                i++;
                if (i == 16 && i < count)
                    obj.Col17 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col17 = null;
                i++;
                if (i == 17 && i < count)
                    obj.Col18 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col18 = null;
                i++;
                if (i == 18 && i < count)
                    obj.Col19 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col19 = null;
                i++;
                if (i == 19 && i < count)
                    obj.Col20 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col20 = null;
                i++;
                if (i == 20 && i < count)
                    obj.Col21 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col21 = null;
                i++;
                if (i == 21 && i < count)
                    obj.Col22 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col22 = null;
                i++;
                if (i == 22 && i < count)
                    obj.Col23 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col23 = null;
                i++;
                if (i == 23 && i < count)
                    obj.Col24 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col24 = null;
                i++;
                if (i == 24 && i < count)
                    obj.Col25 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col25 = null;
                i++;
                if (i == 25 && i < count)
                    obj.Col26 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col26 = null;
                i++;
                if (i == 26 && i < count)
                    obj.Col27 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col27 = null;
                i++;
                if (i == 27 && i < count)
                    obj.Col28 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col28 = null;
                i++;
                if (i == 28 && i < count)
                    obj.Col29 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col29 = null;
                i++;
                if (i == 29 && i < count)
                    obj.Col30 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col30 = null;
                i++;
                if (i == 30 && i < count)
                    obj.Col31 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col31 = null;
                i++;
                if (i == 31 && i < count)
                    obj.Col32 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col32 = null;
                i++;
                if (i == 32 && i < count)
                    obj.Col33 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col33 = null;
                i++;
                if (i == 33 && i < count)
                    obj.Col34 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col34 = null;
                i++;
                if (i == 34 && i < count)
                    obj.Col35 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col35 = null;
                i++;
                if (i == 35 && i < count)
                    obj.Col36 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col36 = null;
                i++;
                if (i == 36 && i < count)
                    obj.Col37 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col37 = null;
                i++;
                if (i == 37 && i < count)
                    obj.Col38 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col38 = null;
                i++;
                if (i == 38 && i < count)
                    obj.Col39 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col39 = null;
                i++;
                if (i == 39 && i < count)
                    obj.Col40 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col40 = null;
                i++;
                if (i == 40 && i < count)
                    obj.Col41 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col41 = null;
                i++;
                if (i == 41 && i < count)
                    obj.Col42 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col42 = null;
                i++;
                if (i == 42 && i < count)
                    obj.Col43 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col43 = null;
                i++;
                if (i == 43 && i < count)
                    obj.Col44 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col44 = null;
                i++;
                if (i == 44 && i < count)
                    obj.Col45 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col45 = null;
                i++;
                if (i == 45 && i < count)
                    obj.Col46 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col46 = null;
                i++;
                if (i == 46 && i < count)
                    obj.Col47 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col47 = null;
                i++;
                if (i == 47 && i < count)
                    obj.Col48 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col48 = null;
                i++;
                if (i == 48 && i < count)
                    obj.Col49 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col49 = null;
                i++;
                if (i == 49 && i < count)
                    obj.Col50 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col50 = null;
                i++;
                if (i == 50 && i < count)
                    obj.Col51 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col51 = null;
                i++;
                if (i == 51 && i < count)
                    obj.Col52 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col52 = null;
                i++;
                if (i == 52 && i < count)
                    obj.Col53 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col53 = null;
                i++;

                if (i == 53 && i < count)
                    obj.Col54 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col54 = null;
                i++;
                if (i == 54 && i < count)
                    obj.Col55 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col55 = null;
                i++;
                if (i == 55 && i < count)
                    obj.Col56 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col56 = null;
                i++;
                if (i == 56 && i < count)
                    obj.Col57 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col57 = null;
                i++;
                if (i == 57 && i < count)
                    obj.Col58 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col58 = null;
                i++;
                if (i == 58 && i < count)
                    obj.Col59 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col59 = null;
                i++;
                if (i == 59 && i < count)
                    obj.Col60 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col60 = null;
                i++;
                if (i == 60 && i < count)
                    obj.Col61 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col61 = null;
                i++;
                if (i == 61 && i < count)
                    obj.Col62 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col62 = null;
                i++;
                if (i == 62 && i < count)
                    obj.Col63 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col63 = null;
                i++;
                if (i == 63 && i < count)
                    obj.Col64 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col64 = null;
                i++;
                if (i == 64 && i < count)
                    obj.Col65 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col65 = null;
                i++;
                if (i == 65 && i < count)
                    obj.Col66 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col66 = null;
                i++;
                if (i == 66 && i < count)
                    obj.Col67 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col67 = null;
                i++;
                if (i == 67 && i < count)
                    obj.Col68 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col68 = null;
                i++;
                if (i == 68 && i < count)
                    obj.Col69 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col69 = null;
                i++;
                if (i == 69 && i < count)
                    obj.Col70 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col70 = null;
                i++;
                if (i == 70 && i < count)
                    obj.Col71 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col71 = null;
                i++;
                if (i == 71 && i < count)
                    obj.Col72 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col72 = null;
                i++;
                if (i == 72 && i < count)
                    obj.Col73 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col73 = null;
                i++;
                if (i == 73 && i < count)
                    obj.Col74 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col74 = null;
                i++;
                if (i == 74 && i < count)
                    obj.Col75 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col75 = null;
                i++;


                if (i == 75 && i < count)
                    obj.Col76 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col76 = null;
                i++;
                if (i == 76 && i < count)
                    obj.Col77 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col77 = null;
                i++;
                if (i == 77 && i < count)
                    obj.Col78 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col78 = null;
                i++;
                if (i == 78 && i < count)
                    obj.Col79 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col79 = null;
                i++;
                if (i == 79 && i < count)
                    obj.Col80 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col80 = null;
                i++;
                if (i == 80 && i < count)
                    obj.Col81 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col81 = null;
                i++;
                if (i == 81 && i < count)
                    obj.Col82 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col82 = null;
                i++;
                if (i == 82 && i < count)
                    obj.Col83 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col83 = null;
                i++;
                if (i == 83 && i < count)
                    obj.Col84 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col84 = null;
                i++;
                if (i == 84 && i < count)
                    obj.Col85 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col85 = null;
                i++;
                if (i == 85 && i < count)
                    obj.Col86 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col86 = null;
                i++;
                if (i == 86 && i < count)
                    obj.Col87 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col87 = null;
                i++;
                if (i == 87 && i < count)
                    obj.Col88 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col88 = null;
                i++;
                if (i == 88 && i < count)
                    obj.Col89 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col89 = null;
                i++;
                if (i == 89 && i < count)
                    obj.Col90 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col90 = null;
                i++;
                if (i == 90 && i < count)
                    obj.Col91 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col91 = null;
                i++;
                if (i == 91 && i < count)
                    obj.Col92 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col92 = null;
                i++;
                if (i == 92 && i < count)
                    obj.Col93 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col93 = null;
                i++;
                if (i == 93 && i < count)
                    obj.Col94 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col94 = null;
                i++;
                if (i == 94 && i < count)
                    obj.Col95 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col95 = null;
                i++;
                if (i == 95 && i < count)
                    obj.Col96 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col96 = null;
                i++;
                if (i == 96 && i < count)
                    obj.Col97 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col97 = null;
                i++;
                if (i == 97 && i < count)
                    obj.Col98 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col98 = null;
                i++;
                if (i == 98 && i < count)
                    obj.Col99 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col99 = null;
                i++;
                if (i == 99 && i < count)
                    obj.Col100 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col100 = null;
                i++;
                if (i == 100 && i < count)
                    obj.Col101 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col101 = null;
                i++;
                if (i == 101 && i < count)
                    obj.Col102 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col102 = null;
                i++;
                if (i == 102 && i < count)
                    obj.Col103 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col103 = null;
                i++;
                if (i == 103 && i < count)
                    obj.Col104 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col104 = null;
                i++;
                if (i == 104 && i < count)
                    obj.Col105 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col105 = null;
                i++;
                if (i == 105 && i < count)
                    obj.Col106 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col106 = null;
                i++;
                if (i == 106 && i < count)
                    obj.Col107 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col107 = null;
                i++;
                if (i == 107 && i < count)
                    obj.Col108 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col108 = null;
                i++;
                if (i == 108 && i < count)
                    obj.Col109 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col109 = null;
                i++;
                if (i == 109 && i < count)
                    obj.Col110 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col110 = null;
                i++;
                if (i == 110 && i < count)
                    obj.Col111 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col111 = null;
                i++;
                if (i == 111 && i < count)
                    obj.Col112 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col112 = null;
                i++;
                if (i == 112 && i < count)
                    obj.Col113 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col113 = null;
                i++;
                if (i == 113 && i < count)
                    obj.Col114 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col114 = null;
                i++;
                if (i == 114 && i < count)
                    obj.Col115 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col115 = null;
                i++;
                if (i == 115 && i < count)
                    obj.Col116 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col116 = null;
                i++;
                if (i == 116 && i < count)
                    obj.Col117 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col117 = null;
                i++;
                if (i == 117 && i < count)
                    obj.Col118 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col118 = null;
                i++;
                if (i == 118 && i < count)
                    obj.Col119 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col119 = null;
                i++;
                if (i == 119 && i < count)
                    obj.Col120 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col120 = null;
                i++;

                 UpdateSqlCe(obj);


                //var local = sc.Set<WorkHoursClass>()
                //    .Local
                //    .FirstOrDefault(f => f.wid == checkuser.wid);
                //if (local != null)
                //{
                //    sc.Entry(local).State = EntityState.Detached;
                //}

                //sc.Entry(obj).State = EntityState.Modified;
                //sc.SaveChanges();


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        private void UpdateSqlCe(WorkHoursClass obj)
        {
            try
            {
                SqlConnection con = ConnectionBulder.con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string Query = @"Update WorkHours set TotalHours=@TotalHours,RestHours=@RestHours,
                          options=@options,Remarks=@Remarks,NonConfirmities=@NonConfirmities,NonConfirmities1=@NonConfirmities1,hrs=@hrs,
                          Colors=@Colors,ColorName=@ColorName,Col1=@Col1,Col2=@Col2,Col3=@Col3,
                          Col4=@Col4,Col5=@Col5,Col6=@Col6,Col7=@Col7,Col8=@Col8,Col9=@Col9,
                          Col10=@Col10,Col11=@Col11,Col12=@Col12,Col13=@Col13,Col14=@Col14,Col15=@Col15,
                          Col16=@Col16,Col17=@Col17,Col18=@Col18,Col19=@Col19,Col20=@Col20,Col21=@Col21,
                          Col22=@Col22,Col23=@Col23,Col24=@Col24,Col25=@Col25,Col26=@Col26,Col27=@Col27,
                          Col28=@Col28,Col29=@Col29,Col30=@Col30,Col31=@Col31,Col32=@Col32,Col33=@Col33,
                          Col34=@Col34,Col35=@Col35,Col36=@Col36,Col37=@Col37,Col38=@Col38,Col39=@Col39,
                          Col40=@Col40,Col41=@Col41,Col42=@Col42,Col43=@Col43,Col44=@Col44,Col45=@Col45,
                          Col46=@Col46,Col47=@Col47,Col48=@Col48,Col49=@Col49,Col50=@Col50,Col51=@Col51,Col52=@Col52,
                          Col53=@Col53,Col54=@Col54,Col55=@Col55,Col56=@Col56,Col57=@Col57,Col58=@Col58,Col59=@Col59,
                          Col60=@Col60,Col61=@Col61,Col62=@Col62,Col63=@Col63,Col64=@Col64,Col65=@Col65,Col66=@Col66,
                          Col67=@Col67,Col68=@Col68,Col69=@Col69,Col70=@Col70,Col71=@Col71,Col72=@Col72,Col73=@Col73,
                          Col74=@Col74,Col75=@Col75,Col76=@Col76,Col77=@Col77,Col78=@Col78,Col79=@Col79,Col80=@Col80,
                          Col81=@Col81,Col82=@Col82,Col83=@Col83,Col84=@Col84,Col85=@Col85,Col86=@Col86,Col87=@Col87,
                          Col88=@Col88,Col89=@Col89,Col90=@Col90,Col91=@Col91,Col92=@Col92,Col93=@Col93,Col94=@Col94,
                          Col95=@Col95,Col96=@Col96,Col97=@Col97,Col98=@Col98,Col99=@Col99,Col100=@Col100,Col101=@Col101,
                          Col102=@Col102,Col103=@Col103,Col104=@Col104,Col105=@Col105,Col106=@Col106,Col107=@Col107,
                          Col108=@Col108,Col109=@Col109,Col110=@Col110,Col111=@Col111,Col112=@Col112,Col113=@Col113,
                          Col114=@Col114,Col115=@Col115,Col116=@Col116,Col117=@Col117,Col118=@Col118,Col119=@Col119,
                          Col120=@Col120,Normal_WKH=@Normal_WKH,overtime=@overtime,StatusCrew=@StatusCrew,CrewNC=@CrewNC,
                          MasterCrewNC=@MasterCrewNC,HODCrewNC=@HODCrewNC,datetimes=@datetimes,opa=@opa,RestHour7day=@RestHour7day,
                          NormalOT1=@NormalOT1,RuleNames=@RuleNames,
                          
                          NC1=@NC1,NCD1=@NCD1,NC2=@NC2,NCD2=@NCD2,NC3=@NC3,NCD3=@NCD3,NC4=@NC4,NCD4=@NCD4,NC5=@NC5,NCD5=@NCD5,
                          NC6=@NC6,NCD6=@NCD6,NC7=@NC7,NCD7=@NCD7,NC8=@NC8,NCD8=@NCD8,NC9=@NC9,NCD9=@NCD9,
                          NC10=@NC10,NCD10=@NCD10,NC11=@NC11,NCD11=@NCD11,NC12=@NC12,NCD12=@NCD12,NC13=@NC13,
                          NCD13=@NCD13,NC14=@NC14,NCD14=@NCD14,NC17=@NC17,NCD17=@NCD17,NC18=@NC18,NCD18=@NCD18,
                          MNCCD1=@MNCCD1,MNCCD2=@MNCCD2,MNCCD3=@MNCCD3,MNCCD4=@MNCCD4,MNCCD5=@MNCCD5,MNCCD6=@MNCCD6,
                          MNCCD7=@MNCCD7,MNCCD8=@MNCCD8,MNCCD9=@MNCCD9,MNCCD10=@MNCCD10,
                          MNCCD11=@MNCCD11,MNCCD12=@MNCCD12,MNCCD13=@MNCCD13,MNCCD14=@MNCCD14,MNCCD17=@MNCCD17,
                          MNCCD18=@MNCCD18,HNCCD1=@HNCCD1,HNCCD2=@HNCCD2,HNCCD3=@HNCCD3,HNCCD4=@HNCCD4,HNCCD5=@HNCCD5,
                          HNCCD6=@HNCCD6,HNCCD7=@HNCCD7,HNCCD8=@HNCCD8,HNCCD9=@HNCCD9,HNCCD10=@HNCCD10,HNCCD11=@HNCCD11,
                          HNCCD12=@HNCCD12,HNCCD13=@HNCCD13,HNCCD14=@HNCCD14,HNCCD17=@HNCCD17,HNCCD18=@HNCCD18,
                          YNC1=@YNC1,YNCD1=@YNCD1,YNC2=@YNC2,YNCD2=@YNCD2,YNC3=@YNC3,YNCD3=@YNCD3,YNC4=@YNC4,YNCD4=@YNCD4,
                          YNC5=@YNC5,YNCD5=@YNCD5,YNC6=@YNC6,YNCD6=@YNCD6,YNC7=@YNC7,YNCD7=@YNCD7,YNC8=@YNC8,YNCD8=@YNCD8,
                          MYNCCD1=@MYNCCD1,MYNCCD2=@MYNCCD2,MYNCCD3=@MYNCCD3,MYNCCD4=@MYNCCD4,MYNCCD5=@MYNCCD5,MYNCCD6=@MYNCCD6,
                          MYNCCD7=@MYNCCD7,MYNCCD8=@MYNCCD8,HYNCCD1=@HYNCCD1,HYNCCD2=@HYNCCD2,HYNCCD3=@HYNCCD3,HYNCCD4=@HYNCCD4,
                          HYNCCD5=@HYNCCD5,HYNCCD6=@HYNCCD6,HYNCCD7=@HYNCCD7,HYNCCD8=@HYNCCD8,
                          RestHourAny24=@RestHourAny24,RestHourAny7day=@RestHourAny7day,options1=@options1,
                          opayoung=@opayoung,Last_update=@Last_update,AdminAlarm=@AdminAlarm,MasterAlarm=@MasterAlarm,
                          HODAlarm=@HODAlarm,Days=@Days,Cellcount=@Cellcount,UnFreezeDates=@UnFreezeDates

                          where dates=@dates and UserName=@UserName and Position=@Position and WRID=@WRID and MonthName=@MonthName and YearValue=@YearValue
                          and cid=@cid";

                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@TotalHours", obj.TotalHours);
                cmd.Parameters.AddWithValue("@RestHours", obj.RestHours);
                cmd.Parameters.AddWithValue("@options", obj.options);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@NonConfirmities", obj.NonConfirmities == null ? string.Empty : obj.NonConfirmities);
                cmd.Parameters.AddWithValue("@NonConfirmities1", obj.NonConfirmities1 == null ? string.Empty : obj.NonConfirmities1);
                cmd.Parameters.AddWithValue("@hrs", obj.hrs);
                cmd.Parameters.AddWithValue("@Colors", obj.Colors);
                cmd.Parameters.AddWithValue("@ColorName", obj.ColorName);


                cmd.Parameters.AddWithValue("@Col1", obj.Col1);
                cmd.Parameters.AddWithValue("@Col2", obj.Col2);
                cmd.Parameters.AddWithValue("@Col3", obj.Col3);
                cmd.Parameters.AddWithValue("@Col4", obj.Col4);
                cmd.Parameters.AddWithValue("@Col5", obj.Col5);
                cmd.Parameters.AddWithValue("@Col6", obj.Col6);
                cmd.Parameters.AddWithValue("@Col7", obj.Col7);
                cmd.Parameters.AddWithValue("@Col8", obj.Col8);
                cmd.Parameters.AddWithValue("@Col9", obj.Col9);
                cmd.Parameters.AddWithValue("@Col10", obj.Col10);
                cmd.Parameters.AddWithValue("@Col11", obj.Col11);
                cmd.Parameters.AddWithValue("@Col12", obj.Col12);
                cmd.Parameters.AddWithValue("@Col13", obj.Col13);
                cmd.Parameters.AddWithValue("@Col14", obj.Col14);
                cmd.Parameters.AddWithValue("@Col15", obj.Col15);
                cmd.Parameters.AddWithValue("@Col16", obj.Col16);
                cmd.Parameters.AddWithValue("@Col17", obj.Col17);
                cmd.Parameters.AddWithValue("@Col18", obj.Col18);
                cmd.Parameters.AddWithValue("@Col19", obj.Col19);
                cmd.Parameters.AddWithValue("@Col20", obj.Col20);

                cmd.Parameters.AddWithValue("@Col21", obj.Col21);
                cmd.Parameters.AddWithValue("@Col22", obj.Col22);
                cmd.Parameters.AddWithValue("@Col23", obj.Col23);
                cmd.Parameters.AddWithValue("@Col24", obj.Col24);
                cmd.Parameters.AddWithValue("@Col25", obj.Col25);
                cmd.Parameters.AddWithValue("@Col26", obj.Col26);
                cmd.Parameters.AddWithValue("@Col27", obj.Col27);
                cmd.Parameters.AddWithValue("@Col28", obj.Col28);
                cmd.Parameters.AddWithValue("@Col29", obj.Col29);
                cmd.Parameters.AddWithValue("@Col30", obj.Col30);

                if (string.IsNullOrEmpty(obj.Col31))
                    cmd.Parameters.AddWithValue("@Col31", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col31", obj.Col31);

                if (string.IsNullOrEmpty(obj.Col32))
                    cmd.Parameters.AddWithValue("@Col32", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col32", obj.Col32);

                if (string.IsNullOrEmpty(obj.Col33))
                    cmd.Parameters.AddWithValue("@Col33", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col33", obj.Col33);

                if (string.IsNullOrEmpty(obj.Col34))
                    cmd.Parameters.AddWithValue("@Col34", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col34", obj.Col34);

                if (string.IsNullOrEmpty(obj.Col35))
                    cmd.Parameters.AddWithValue("@Col35", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col35", obj.Col35);

                if (string.IsNullOrEmpty(obj.Col36))
                    cmd.Parameters.AddWithValue("@Col36", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col36", obj.Col36);

                if (string.IsNullOrEmpty(obj.Col37))
                    cmd.Parameters.AddWithValue("@Col37", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col37", obj.Col37);

                if (string.IsNullOrEmpty(obj.Col38))
                    cmd.Parameters.AddWithValue("@Col38", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col38", obj.Col38);

                if (string.IsNullOrEmpty(obj.Col39))
                    cmd.Parameters.AddWithValue("@Col39", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col39", obj.Col39);

                if (string.IsNullOrEmpty(obj.Col40))
                    cmd.Parameters.AddWithValue("@Col40", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col40", obj.Col40);

                if (string.IsNullOrEmpty(obj.Col41))
                    cmd.Parameters.AddWithValue("@Col41", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col41", obj.Col41);

                if (string.IsNullOrEmpty(obj.Col42))
                    cmd.Parameters.AddWithValue("@Col42", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col42", obj.Col42);

                if (string.IsNullOrEmpty(obj.Col43))
                    cmd.Parameters.AddWithValue("@Col43", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col43", obj.Col43);

                if (string.IsNullOrEmpty(obj.Col44))
                    cmd.Parameters.AddWithValue("@Col44", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col44", obj.Col44);

                if (string.IsNullOrEmpty(obj.Col45))
                    cmd.Parameters.AddWithValue("@Col45", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col45", obj.Col45);

                if (string.IsNullOrEmpty(obj.Col46))
                    cmd.Parameters.AddWithValue("@Col46", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col46", obj.Col46);

                if (string.IsNullOrEmpty(obj.Col47))
                    cmd.Parameters.AddWithValue("@Col47", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col47", obj.Col47);

                if (string.IsNullOrEmpty(obj.Col48))
                    cmd.Parameters.AddWithValue("@Col48", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col48", obj.Col48);

                if (string.IsNullOrEmpty(obj.Col49))
                    cmd.Parameters.AddWithValue("@Col49", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col49", obj.Col49);

                if (string.IsNullOrEmpty(obj.Col50))
                    cmd.Parameters.AddWithValue("@Col50", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col50", obj.Col50);

                if (string.IsNullOrEmpty(obj.Col51))
                    cmd.Parameters.AddWithValue("@Col51", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col51", obj.Col51);

                if (string.IsNullOrEmpty(obj.Col52))
                    cmd.Parameters.AddWithValue("@Col52", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col52", obj.Col52);

                if (string.IsNullOrEmpty(obj.Col53))
                    cmd.Parameters.AddWithValue("@Col53", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col53", obj.Col53);

                if (string.IsNullOrEmpty(obj.Col54))
                    cmd.Parameters.AddWithValue("@Col54", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col54", obj.Col54);

                if (string.IsNullOrEmpty(obj.Col55))
                    cmd.Parameters.AddWithValue("@Col55", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col55", obj.Col55);

                if (string.IsNullOrEmpty(obj.Col56))
                    cmd.Parameters.AddWithValue("@Col56", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col56", obj.Col56);

                if (string.IsNullOrEmpty(obj.Col57))
                    cmd.Parameters.AddWithValue("@Col57", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col57", obj.Col57);

                if (string.IsNullOrEmpty(obj.Col58))
                    cmd.Parameters.AddWithValue("@Col58", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col58", obj.Col58);

                if (string.IsNullOrEmpty(obj.Col59))
                    cmd.Parameters.AddWithValue("@Col59", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col59", obj.Col59);

                if (string.IsNullOrEmpty(obj.Col60))
                    cmd.Parameters.AddWithValue("@Col60", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col60", obj.Col60);


                if (string.IsNullOrEmpty(obj.Col61))
                    cmd.Parameters.AddWithValue("@Col61", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col61", obj.Col61);
                if (string.IsNullOrEmpty(obj.Col62))
                    cmd.Parameters.AddWithValue("@Col62", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col62", obj.Col62);
                if (string.IsNullOrEmpty(obj.Col63))
                    cmd.Parameters.AddWithValue("@Col63", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col63", obj.Col63);
                if (string.IsNullOrEmpty(obj.Col64))
                    cmd.Parameters.AddWithValue("@Col64", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col64", obj.Col64);

                if (string.IsNullOrEmpty(obj.Col65))
                    cmd.Parameters.AddWithValue("@Col65", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col65", obj.Col65);

                if (string.IsNullOrEmpty(obj.Col66))
                    cmd.Parameters.AddWithValue("@Col66", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col66", obj.Col66);

                if (string.IsNullOrEmpty(obj.Col67))
                    cmd.Parameters.AddWithValue("@Col67", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col67", obj.Col67);

                if (string.IsNullOrEmpty(obj.Col68))
                    cmd.Parameters.AddWithValue("@Col68", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col68", obj.Col68);

                if (string.IsNullOrEmpty(obj.Col69))
                    cmd.Parameters.AddWithValue("@Col69", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col69", obj.Col69);

                if (string.IsNullOrEmpty(obj.Col70))
                    cmd.Parameters.AddWithValue("@Col70", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col70", obj.Col70);


                if (string.IsNullOrEmpty(obj.Col71))
                    cmd.Parameters.AddWithValue("@Col71", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col71", obj.Col71);

                if (string.IsNullOrEmpty(obj.Col72))

                    cmd.Parameters.AddWithValue("@Col72", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col72", obj.Col72);

                if (string.IsNullOrEmpty(obj.Col73))
                    cmd.Parameters.AddWithValue("@Col73", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col73", obj.Col73);

                if (string.IsNullOrEmpty(obj.Col74))
                    cmd.Parameters.AddWithValue("@Col74", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col74", obj.Col74);

                if (string.IsNullOrEmpty(obj.Col75))
                    cmd.Parameters.AddWithValue("@Col75", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col75", obj.Col75);

                if (string.IsNullOrEmpty(obj.Col76))
                    cmd.Parameters.AddWithValue("@Col76", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col76", obj.Col76);

                if (string.IsNullOrEmpty(obj.Col77))
                    cmd.Parameters.AddWithValue("@Col77", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col77", obj.Col77);

                if (string.IsNullOrEmpty(obj.Col78))
                    cmd.Parameters.AddWithValue("@Col78", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col78", obj.Col78);

                if (string.IsNullOrEmpty(obj.Col79))
                    cmd.Parameters.AddWithValue("@Col79", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col79", obj.Col79);

                if (string.IsNullOrEmpty(obj.Col80))
                    cmd.Parameters.AddWithValue("@Col80", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col80", obj.Col80);



                if (string.IsNullOrEmpty(obj.Col81))
                    cmd.Parameters.AddWithValue("@Col81", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col81", obj.Col81);

                if (string.IsNullOrEmpty(obj.Col82))
                    cmd.Parameters.AddWithValue("@Col82", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col82", obj.Col82);

                if (string.IsNullOrEmpty(obj.Col83))
                    cmd.Parameters.AddWithValue("@Col83", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col83", obj.Col83);

                if (string.IsNullOrEmpty(obj.Col84))
                    cmd.Parameters.AddWithValue("@Col84", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col84", obj.Col84);

                if (string.IsNullOrEmpty(obj.Col85))
                    cmd.Parameters.AddWithValue("@Col85", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col85", obj.Col85);

                if (string.IsNullOrEmpty(obj.Col86))
                    cmd.Parameters.AddWithValue("@Col86", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col86", obj.Col86);

                if (string.IsNullOrEmpty(obj.Col87))
                    cmd.Parameters.AddWithValue("@Col87", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col87", obj.Col87);

                if (string.IsNullOrEmpty(obj.Col88))
                    cmd.Parameters.AddWithValue("@Col88", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col88", obj.Col88);

                if (string.IsNullOrEmpty(obj.Col89))
                    cmd.Parameters.AddWithValue("@Col89", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col89", obj.Col89);

                if (string.IsNullOrEmpty(obj.Col90))
                    cmd.Parameters.AddWithValue("@Col90", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col90", obj.Col90);



                if (string.IsNullOrEmpty(obj.Col91))
                    cmd.Parameters.AddWithValue("@Col91", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col91", obj.Col91);

                if (string.IsNullOrEmpty(obj.Col92))
                    cmd.Parameters.AddWithValue("@Col92", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col92", obj.Col92);

                if (string.IsNullOrEmpty(obj.Col93))
                    cmd.Parameters.AddWithValue("@Col93", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col93", obj.Col93);

                if (string.IsNullOrEmpty(obj.Col94))
                    cmd.Parameters.AddWithValue("@Col94", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col94", obj.Col94);

                if (string.IsNullOrEmpty(obj.Col95))
                    cmd.Parameters.AddWithValue("@Col95", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col95", obj.Col95);

                if (string.IsNullOrEmpty(obj.Col96))
                    cmd.Parameters.AddWithValue("@Col96", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col96", obj.Col96);

                if (string.IsNullOrEmpty(obj.Col97))
                    cmd.Parameters.AddWithValue("@Col97", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col97", obj.Col97);

                if (string.IsNullOrEmpty(obj.Col98))
                    cmd.Parameters.AddWithValue("@Col98", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col98", obj.Col98);

                if (string.IsNullOrEmpty(obj.Col99))
                    cmd.Parameters.AddWithValue("@Col99", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col99", obj.Col99);

                if (string.IsNullOrEmpty(obj.Col100))
                    cmd.Parameters.AddWithValue("@Col100", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col100", obj.Col100);


                if (string.IsNullOrEmpty(obj.Col101))
                    cmd.Parameters.AddWithValue("@Col101", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col101", obj.Col101);

                if (string.IsNullOrEmpty(obj.Col102))
                    cmd.Parameters.AddWithValue("@Col102", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col102", obj.Col102);

                if (string.IsNullOrEmpty(obj.Col103))
                    cmd.Parameters.AddWithValue("@Col103", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col103", obj.Col103);

                if (string.IsNullOrEmpty(obj.Col104))
                    cmd.Parameters.AddWithValue("@Col104", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col104", obj.Col104);

                if (string.IsNullOrEmpty(obj.Col105))
                    cmd.Parameters.AddWithValue("@Col105", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col105", obj.Col105);

                if (string.IsNullOrEmpty(obj.Col106))
                    cmd.Parameters.AddWithValue("@Col106", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col106", obj.Col106);

                if (string.IsNullOrEmpty(obj.Col107))
                    cmd.Parameters.AddWithValue("@Col107", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col107", obj.Col107);

                if (string.IsNullOrEmpty(obj.Col108))
                    cmd.Parameters.AddWithValue("@Col108", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col108", obj.Col108);

                if (string.IsNullOrEmpty(obj.Col109))
                    cmd.Parameters.AddWithValue("@Col109", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col109", obj.Col109);

                if (string.IsNullOrEmpty(obj.Col110))
                    cmd.Parameters.AddWithValue("@Col110", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col110", obj.Col110);




                if (string.IsNullOrEmpty(obj.Col111))
                    cmd.Parameters.AddWithValue("@Col111", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col111", obj.Col111);

                if (string.IsNullOrEmpty(obj.Col112))
                    cmd.Parameters.AddWithValue("@Col112", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col112", obj.Col112);

                if (string.IsNullOrEmpty(obj.Col113))
                    cmd.Parameters.AddWithValue("@Col113", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col113", obj.Col113);

                if (string.IsNullOrEmpty(obj.Col114))
                    cmd.Parameters.AddWithValue("@Col114", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col114", obj.Col114);

                if (string.IsNullOrEmpty(obj.Col115))
                    cmd.Parameters.AddWithValue("@Col115", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col115", obj.Col115);

                if (string.IsNullOrEmpty(obj.Col116))
                    cmd.Parameters.AddWithValue("@Col116", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col116", obj.Col116);

                if (string.IsNullOrEmpty(obj.Col117))
                    cmd.Parameters.AddWithValue("@Col117", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col117", obj.Col117);

                if (string.IsNullOrEmpty(obj.Col118))
                    cmd.Parameters.AddWithValue("@Col118", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col118", obj.Col118);

                if (string.IsNullOrEmpty(obj.Col119))
                    cmd.Parameters.AddWithValue("@Col119", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col119", obj.Col119);

                if (string.IsNullOrEmpty(obj.Col120))
                    cmd.Parameters.AddWithValue("@Col120", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Col120", obj.Col120);


                cmd.Parameters.AddWithValue("@Normal_WKH", obj.Normal_WKH);
                cmd.Parameters.AddWithValue("@overtime", obj.overtime);
                cmd.Parameters.AddWithValue("@StatusCrew", obj.StatusCrew);
                cmd.Parameters.AddWithValue("@CrewNC", obj.CrewNC);
                cmd.Parameters.AddWithValue("@MasterCrewNC", obj.MasterCrewNC);
                cmd.Parameters.AddWithValue("@HODCrewNC", obj.HODCrewNC);
                cmd.Parameters.AddWithValue("@datetimes", obj.datetimes);
                cmd.Parameters.AddWithValue("@opa", obj.opa);

                cmd.Parameters.AddWithValue("@RestHour7day", obj.RestHour7day);
                cmd.Parameters.AddWithValue("@NormalOT1", obj.NormalOT1);
                cmd.Parameters.AddWithValue("@RuleNames", obj.RuleNames);

                if (string.IsNullOrEmpty(obj.NC1))
                    cmd.Parameters.AddWithValue("@NC1", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC1", obj.NC1);

                if (string.IsNullOrEmpty(obj.NC2))
                    cmd.Parameters.AddWithValue("@NC2", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC2", obj.NC2);

                if (string.IsNullOrEmpty(obj.NC3))
                    cmd.Parameters.AddWithValue("@NC3", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC3", obj.NC3);

                if (string.IsNullOrEmpty(obj.NC4))
                    cmd.Parameters.AddWithValue("@NC4", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC4", obj.NC4);

                if (string.IsNullOrEmpty(obj.NC5))
                    cmd.Parameters.AddWithValue("@NC5", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC5", obj.NC5);

                if (string.IsNullOrEmpty(obj.NC6))
                    cmd.Parameters.AddWithValue("@NC6", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC6", obj.NC6);

                if (string.IsNullOrEmpty(obj.NC7))
                    cmd.Parameters.AddWithValue("@NC7", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC7", obj.NC7);

                if (string.IsNullOrEmpty(obj.NC8))
                    cmd.Parameters.AddWithValue("@NC8", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC8", obj.NC8);

                if (string.IsNullOrEmpty(obj.NC9))
                    cmd.Parameters.AddWithValue("@NC9", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC9", obj.NC9);

                if (string.IsNullOrEmpty(obj.NC10))
                    cmd.Parameters.AddWithValue("@NC10", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC10", obj.NC10);

                if (string.IsNullOrEmpty(obj.NC11))
                    cmd.Parameters.AddWithValue("@NC11", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC11", obj.NC11);

                if (string.IsNullOrEmpty(obj.NC12))
                    cmd.Parameters.AddWithValue("@NC12", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC12", obj.NC12);

                if (string.IsNullOrEmpty(obj.NC13))
                    cmd.Parameters.AddWithValue("@NC13", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC13", obj.NC13);

                if (string.IsNullOrEmpty(obj.NC14))
                    cmd.Parameters.AddWithValue("@NC14", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC14", obj.NC14);

                if (string.IsNullOrEmpty(obj.NC15))
                    cmd.Parameters.AddWithValue("@NC15", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC15", obj.NC15);

                if (string.IsNullOrEmpty(obj.NC16))
                    cmd.Parameters.AddWithValue("@NC16", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC16", obj.NC16);

                if (string.IsNullOrEmpty(obj.NC17))
                    cmd.Parameters.AddWithValue("@NC17", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC17", obj.NC17);

                if (string.IsNullOrEmpty(obj.NC18))
                    cmd.Parameters.AddWithValue("@NC18", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NC18", obj.NC18);


                if (obj.NCD1 == null)
                    cmd.Parameters.AddWithValue("@NCD1", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD1", obj.NCD1);

                if (obj.NCD2 == null)
                    cmd.Parameters.AddWithValue("@NCD2", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD2", obj.NCD2);

                if (obj.NCD3 == null)
                    cmd.Parameters.AddWithValue("@NCD3", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD3", obj.NCD3);

                if (obj.NCD4 == null)
                    cmd.Parameters.AddWithValue("@NCD4", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD4", obj.NCD4);

                if (obj.NCD5 == null)
                    cmd.Parameters.AddWithValue("@NCD5", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD5", obj.NCD5);

                if (obj.NCD6 == null)
                    cmd.Parameters.AddWithValue("@NCD6", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD6", obj.NCD6);

                if (obj.NCD7 == null)
                    cmd.Parameters.AddWithValue("@NCD7", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD7", obj.NCD7);

                if (obj.NCD8 == null)
                    cmd.Parameters.AddWithValue("@NCD8", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD8", obj.NCD8);

                if (obj.NCD9 == null)
                    cmd.Parameters.AddWithValue("@NCD9", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD9", obj.NCD9);

                if (obj.NCD10 == null)
                    cmd.Parameters.AddWithValue("@NCD10", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD10", obj.NCD10);

                if (obj.NCD11 == null)
                    cmd.Parameters.AddWithValue("@NCD11", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD11", obj.NCD11);

                if (obj.NCD12 == null)
                    cmd.Parameters.AddWithValue("@NCD12", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD12", obj.NCD12);

                if (obj.NCD13 == null)
                    cmd.Parameters.AddWithValue("@NCD13", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD13", obj.NCD13);

                if (obj.NCD14 == null)
                    cmd.Parameters.AddWithValue("@NCD14", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD14", obj.NCD14);

                if (obj.NCD15 == null)
                    cmd.Parameters.AddWithValue("@NCD15", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD15", obj.NCD15);

                if (obj.NCD16 == null)
                    cmd.Parameters.AddWithValue("@NCD16", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD16", obj.NCD16);

                if (obj.NCD17 == null)
                    cmd.Parameters.AddWithValue("@NCD17", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD17", obj.NCD17);

                if (obj.NCD18 == null)
                    cmd.Parameters.AddWithValue("@NCD18", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@NCD18", obj.NCD18);


                if (obj.MNCCD1 == null)
                    cmd.Parameters.AddWithValue("@MNCCD1", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD1", obj.MNCCD1);

                if (obj.MNCCD2 == null)
                    cmd.Parameters.AddWithValue("@MNCCD2", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD2", obj.MNCCD2);

                if (obj.MNCCD3 == null)
                    cmd.Parameters.AddWithValue("@MNCCD3", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD3", obj.MNCCD3);

                if (obj.MNCCD4 == null)
                    cmd.Parameters.AddWithValue("@MNCCD4", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD4", obj.MNCCD4);

                if (obj.MNCCD5 == null)
                    cmd.Parameters.AddWithValue("@MNCCD5", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD5", obj.MNCCD5);

                if (obj.MNCCD6 == null)
                    cmd.Parameters.AddWithValue("@MNCCD6", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD6", obj.MNCCD6);

                if (obj.MNCCD7 == null)
                    cmd.Parameters.AddWithValue("@MNCCD7", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD7", obj.MNCCD7);

                if (obj.MNCCD8 == null)
                    cmd.Parameters.AddWithValue("@MNCCD8", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD8", obj.MNCCD8);

                if (obj.MNCCD9 == null)
                    cmd.Parameters.AddWithValue("@MNCCD9", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD9", obj.MNCCD9);

                if (obj.MNCCD10 == null)
                    cmd.Parameters.AddWithValue("@MNCCD10", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD10", obj.MNCCD10);

                if (obj.MNCCD11 == null)
                    cmd.Parameters.AddWithValue("@MNCCD11", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD11", obj.MNCCD11);

                if (obj.MNCCD12 == null)
                    cmd.Parameters.AddWithValue("@MNCCD12", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD12", obj.MNCCD12);

                if (obj.MNCCD13 == null)
                    cmd.Parameters.AddWithValue("@MNCCD13", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD13", obj.MNCCD13);

                if (obj.MNCCD14 == null)
                    cmd.Parameters.AddWithValue("@MNCCD14", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD14", obj.MNCCD14);

                if (obj.MNCCD15 == null)
                    cmd.Parameters.AddWithValue("@MNCCD15", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD15", obj.MNCCD15);

                if (obj.MNCCD16 == null)
                    cmd.Parameters.AddWithValue("@MNCCD16", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD16", obj.MNCCD16);

                if (obj.MNCCD17 == null)
                    cmd.Parameters.AddWithValue("@MNCCD17", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD17", obj.MNCCD17);

                if (obj.MNCCD18 == null)
                    cmd.Parameters.AddWithValue("@MNCCD18", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MNCCD18", obj.MNCCD18);



                if (obj.HNCCD1 == null)
                    cmd.Parameters.AddWithValue("@HNCCD1", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD1", obj.HNCCD1);

                if (obj.HNCCD2 == null)
                    cmd.Parameters.AddWithValue("@HNCCD2", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD2", obj.HNCCD2);

                if (obj.HNCCD3 == null)
                    cmd.Parameters.AddWithValue("@HNCCD3", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD3", obj.HNCCD3);

                if (obj.HNCCD4 == null)
                    cmd.Parameters.AddWithValue("@HNCCD4", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD4", obj.HNCCD4);

                if (obj.HNCCD5 == null)
                    cmd.Parameters.AddWithValue("@HNCCD5", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD5", obj.HNCCD5);

                if (obj.HNCCD6 == null)
                    cmd.Parameters.AddWithValue("@HNCCD6", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD6", obj.HNCCD6);

                if (obj.HNCCD7 == null)
                    cmd.Parameters.AddWithValue("@HNCCD7", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD7", obj.HNCCD7);

                if (obj.HNCCD8 == null)
                    cmd.Parameters.AddWithValue("@HNCCD8", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD8", obj.HNCCD8);

                if (obj.HNCCD9 == null)
                    cmd.Parameters.AddWithValue("@HNCCD9", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD9", obj.HNCCD9);

                if (obj.HNCCD10 == null)
                    cmd.Parameters.AddWithValue("@HNCCD10", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD10", obj.HNCCD10);

                if (obj.HNCCD11 == null)
                    cmd.Parameters.AddWithValue("@HNCCD11", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD11", obj.HNCCD11);

                if (obj.HNCCD12 == null)
                    cmd.Parameters.AddWithValue("@HNCCD12", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD12", obj.HNCCD12);

                if (obj.HNCCD13 == null)
                    cmd.Parameters.AddWithValue("@HNCCD13", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD13", obj.HNCCD13);

                if (obj.HNCCD14 == null)
                    cmd.Parameters.AddWithValue("@HNCCD14", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD14", obj.HNCCD14);

                if (obj.HNCCD15 == null)
                    cmd.Parameters.AddWithValue("@HNCCD15", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD15", obj.HNCCD15);


                if (obj.HNCCD16 == null)
                    cmd.Parameters.AddWithValue("@HNCCD16", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD16", obj.HNCCD16);

                if (obj.HNCCD17 == null)
                    cmd.Parameters.AddWithValue("@HNCCD17", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD17", obj.HNCCD17);

                if (obj.HNCCD18 == null)
                    cmd.Parameters.AddWithValue("@HNCCD18", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HNCCD18", obj.HNCCD18);



                if (obj.YNC1 == null)
                    cmd.Parameters.AddWithValue("@YNC1", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNC1", obj.YNC1);

                if (obj.YNC2 == null)
                    cmd.Parameters.AddWithValue("@YNC2", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNC2", obj.YNC2);

                if (obj.YNC3 == null)
                    cmd.Parameters.AddWithValue("@YNC3", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNC3", obj.YNC3);

                if (obj.YNC4 == null)
                    cmd.Parameters.AddWithValue("@YNC4", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNC4", obj.YNC4);

                if (obj.YNC5 == null)
                    cmd.Parameters.AddWithValue("@YNC5", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNC5", obj.YNC5);

                if (obj.YNC6 == null)
                    cmd.Parameters.AddWithValue("@YNC6", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNC6", obj.YNC6);

                if (obj.YNC7 == null)
                    cmd.Parameters.AddWithValue("@YNC7", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNC7", obj.YNC7);

                if (obj.YNC8 == null)
                    cmd.Parameters.AddWithValue("@YNC8", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNC8", obj.YNC8);



                if (obj.YNCD1 == null)
                    cmd.Parameters.AddWithValue("@YNCD1", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNCD1", obj.YNCD1);

                if (obj.YNCD2 == null)
                    cmd.Parameters.AddWithValue("@YNCD2", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNCD2", obj.YNCD2);

                if (obj.YNCD3 == null)
                    cmd.Parameters.AddWithValue("@YNCD3", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNCD3", obj.YNCD3);

                if (obj.YNCD4 == null)
                    cmd.Parameters.AddWithValue("@YNCD4", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNCD4", obj.YNCD4);

                if (obj.YNCD5 == null)
                    cmd.Parameters.AddWithValue("@YNCD5", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNCD5", obj.YNCD5);

                if (obj.YNCD6 == null)
                    cmd.Parameters.AddWithValue("@YNCD6", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNCD6", obj.YNCD6);

                if (obj.YNCD7 == null)
                    cmd.Parameters.AddWithValue("@YNCD7", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNCD7", obj.YNCD7);

                if (obj.YNCD8 == null)
                    cmd.Parameters.AddWithValue("@YNCD8", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@YNCD8", obj.YNCD8);


                if (obj.MYNCCD1 == null)
                    cmd.Parameters.AddWithValue("@MYNCCD1", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MYNCCD1", obj.MYNCCD1);

                if (obj.MYNCCD2 == null)
                    cmd.Parameters.AddWithValue("@MYNCCD2", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MYNCCD2", obj.MYNCCD2);

                if (obj.MYNCCD3 == null)
                    cmd.Parameters.AddWithValue("@MYNCCD3", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MYNCCD3", obj.MYNCCD3);

                if (obj.MYNCCD4 == null)
                    cmd.Parameters.AddWithValue("@MYNCCD4", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MYNCCD4", obj.MYNCCD4);

                if (obj.MYNCCD5 == null)
                    cmd.Parameters.AddWithValue("@MYNCCD5", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MYNCCD5", obj.MYNCCD5);

                if (obj.MYNCCD6 == null)
                    cmd.Parameters.AddWithValue("@MYNCCD6", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MYNCCD6", obj.MYNCCD6);

                if (obj.MYNCCD7 == null)
                    cmd.Parameters.AddWithValue("@MYNCCD7", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MYNCCD7", obj.MYNCCD7);

                if (obj.MYNCCD8 == null)
                    cmd.Parameters.AddWithValue("@MYNCCD8", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@MYNCCD8", obj.MYNCCD8);


                if (obj.HYNCCD1 == null)
                    cmd.Parameters.AddWithValue("@HYNCCD1", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HYNCCD1", obj.HYNCCD1);

                if (obj.HYNCCD2 == null)
                    cmd.Parameters.AddWithValue("@HYNCCD2", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HYNCCD2", obj.HYNCCD2);

                if (obj.HYNCCD3 == null)
                    cmd.Parameters.AddWithValue("@HYNCCD3", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HYNCCD3", obj.HYNCCD3);

                if (obj.HYNCCD4 == null)
                    cmd.Parameters.AddWithValue("@HYNCCD4", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HYNCCD4", obj.HYNCCD4);

                if (obj.HYNCCD5 == null)
                    cmd.Parameters.AddWithValue("@HYNCCD5", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HYNCCD5", obj.HYNCCD5);

                if (obj.HYNCCD6 == null)
                    cmd.Parameters.AddWithValue("@HYNCCD6", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HYNCCD6", obj.HYNCCD6);

                if (obj.HYNCCD7 == null)
                    cmd.Parameters.AddWithValue("@HYNCCD7", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HYNCCD7", obj.HYNCCD7);

                if (obj.HYNCCD8 == null)
                    cmd.Parameters.AddWithValue("@HYNCCD8", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HYNCCD8", obj.HYNCCD8);

                cmd.Parameters.AddWithValue("@RestHourAny24", obj.RestHourAny24);
                cmd.Parameters.AddWithValue("@RestHourAny7day", obj.RestHourAny7day);
                cmd.Parameters.AddWithValue("@options1", obj.options1);
                cmd.Parameters.AddWithValue("@opayoung", obj.opayoung);
                cmd.Parameters.AddWithValue("@Last_update", obj.Last_update);
                cmd.Parameters.AddWithValue("@AdminAlarm", obj.AdminAlarm);
                cmd.Parameters.AddWithValue("@MasterAlarm", obj.MasterAlarm);
                cmd.Parameters.AddWithValue("@HODAlarm", obj.HODAlarm);
                cmd.Parameters.AddWithValue("@Days", obj.Days);
                cmd.Parameters.AddWithValue("@Cellcount", obj.Cellcount);

                if (obj.UnFreezeDates == null)
                    cmd.Parameters.AddWithValue("@UnFreezeDates", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@UnFreezeDates", obj.HYNCCD8);

                cmd.Parameters.AddWithValue("@dates", obj.dates);
                cmd.Parameters.AddWithValue("@UserName", obj.UserName);
                cmd.Parameters.AddWithValue("@Position", obj.Position);
                cmd.Parameters.AddWithValue("@WRID", obj.WRID);
                cmd.Parameters.AddWithValue("@MonthName", obj.MonthName);
                cmd.Parameters.AddWithValue("@YearValue", obj.YearValue);
                cmd.Parameters.AddWithValue("@cid", obj.cid);


                cmd.ExecuteNonQuery();
                //cmd.Dispose();
                //sc.con.Close();

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        public virtual void SaveMethod2Planner(WorkHoursPlannerClass ob)
        {
            //For blank insertion...

            try
            {
                string currentTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                var checkuser = sc.WorkHourPlanners.Where(x => x.UserName.Equals(ob.UserName) && x.dates.Equals(ob.dates) && x.WRID.Equals(ob.WRID) && x.Position.Equals(ob.Position)).FirstOrDefault();
                if (checkuser == null)
                {
                    var obj = new WorkHoursPlannerClass()
                    {
                        UserName = ob.UserName,
                        FullName = ob.FullName,
                        Position = ob.Position,
                        Department = ob.Department,
                        TotalHours = 0.0M,
                        RestHours = 24.0M,
                        Remarks = string.Empty,
                        options = ob.options,
                        options1 = "",
                        RestHourAny24 = "24",
                        RestHourAny7day = "168",
                        dates = ob.dates,
                        NonConfirmities = ob.NonConfirmities,
                        NonConfirmities1 = ob.NonConfirmities1,
                        datetimes = Convert.ToDateTime(currentTime),
                        opa = false,   // have to set after 
                        opayoung = ob.opayoung,
                        Cellcount = ob.Cellcount,
                        hrs = ob.hrs,
                        Colors = ob.Colors,
                        MonthName = ob.dates.ToString("MMMM"),
                        YearValue = ob.dates.Year.ToString(),
                        WRID = ob.WRID,
                        Days = ob.dates.DayOfWeek.ToString().Substring(0, 3)

                    };

                    var cid = sc.CrewDetails.Select(s => new { s.Id, s.UserName, s.position }).Where(x => x.UserName.Equals(obj.UserName) && x.position.Equals(obj.Position)).FirstOrDefault().Id;
                    obj.cid = cid;

                    //.......OverTime Calculation....

                    string dayofFirstDay = ob.dates.DayOfWeek.ToString().Substring(0, 3);
                    var overtim = sc.OverTimes.Where(x => x.UserName.Equals(obj.UserName)).FirstOrDefault();

                    int normal = 0;
                    int Sat = 0;
                    int Sun = 0;
                    int Holiday = 0;
                    int hr = 0;

                    if (overtim != null)
                    {
                        normal = Convert.ToInt32(overtim.NrmlWHrs);
                        Sat = Convert.ToInt32(overtim.SatWHrs);
                        Sun = Convert.ToInt32(overtim.SunWhrs);
                        Holiday = Convert.ToInt32(overtim.HolidayWHrs);
                    }

                    if ((dayofFirstDay == "Mon") || (dayofFirstDay == "Tue") || (dayofFirstDay == "Wed") || (dayofFirstDay == "Thu") || (dayofFirstDay == "Fri"))
                    {
                        hr = normal;
                        obj.Normal_WKH = normal;
                    }
                    else if ((dayofFirstDay == "Sat"))
                    {
                        hr = Sat;
                        obj.Normal_WKH = Sat;

                    }
                    else if ((dayofFirstDay == "Sun"))
                    {
                        hr = Sun;
                        obj.Normal_WKH = Sun;

                    }
                    else
                    {
                        hr = Holiday;
                        obj.Normal_WKH = Holiday;
                    }
                    double wh = Convert.ToDouble(obj.TotalHours);
                    double overtime = 0.0;
                    overtime = wh - hr;
                    if (overtime > 0.0)
                    {
                        overtime = wh - hr;
                    }
                    else
                    {
                        overtime = 0.0;
                    }

                    string dayname = "";
                    dayname = dayofFirstDay + "WHrs";
                    int dayscount = 0;

                    var checkholiday = sc.Holidays.Where(x => x.HolidayDate == obj.dates).FirstOrDefault();
                    if (checkholiday != null)
                    {
                        dayscount = overtim != null ? overtim.HolidayWHrs : 0;
                    }
                    else
                    {
                        if (dayname == "SatWHrs")
                            dayscount = overtim != null ? overtim.SatWHrs : 0;

                        else if (dayname == "SunWHrs")
                            dayscount = overtim != null ? overtim.SunWhrs : 0;
                        else
                            dayscount = overtim != null ? overtim.NrmlWHrs : 0;

                    }

                    decimal ovcou = Convert.ToDecimal(obj.TotalHours);
                    decimal ovcou1 = Convert.ToDecimal(dayscount);

                    decimal finalovcou1 = ovcou - ovcou1;
                    finalovcou1 = finalovcou1 > 0 ? finalovcou1 : Convert.ToDecimal(0.0);
                    obj.NormalOT1 = dayscount;
                    obj.overtime = finalovcou1;
                    obj.RuleNames = string.IsNullOrEmpty(obj.RuleNames) ? "Normal" : obj.RuleNames;



                    string[] hrsFetch = ob.hrs.TrimEnd(',').Split(',');
                    int i = 0;
                    int count = hrsFetch.Count();

                    if (i == 0 && i < count)
                        obj.Col1 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col1 = null;
                    i++;
                    if (i == 1 && i < count)
                        obj.Col2 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col2 = null;
                    i++;
                    if (i == 2 && i < count)
                        obj.Col3 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col3 = null;
                    i++;
                    if (i == 3 && i < count)
                        obj.Col4 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col4 = null;
                    i++;
                    if (i == 4 && i < count)
                        obj.Col5 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col5 = null;
                    i++;
                    if (i == 5 && i < count)
                        obj.Col6 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col6 = null;
                    i++;
                    if (i == 6 && i < count)
                        obj.Col7 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col7 = null;
                    i++;
                    if (i == 7 && i < count)
                        obj.Col8 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col8 = null;
                    i++;
                    if (i == 8 && i < count)
                        obj.Col9 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col9 = null;
                    i++;
                    if (i == 9 && i < count)
                        obj.Col10 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col10 = null;
                    i++;
                    if (i == 10 && i < count)
                        obj.Col11 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col11 = null;
                    i++;
                    if (i == 11 && i < count)
                        obj.Col12 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col12 = null;
                    i++;
                    if (i == 12 && i < count)
                        obj.Col13 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col13 = null;
                    i++;
                    if (i == 13 && i < count)
                        obj.Col14 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col14 = null;
                    i++;
                    if (i == 14 && i < count)
                        obj.Col15 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col15 = null;
                    i++;
                    if (i == 15 && i < count)
                        obj.Col16 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col16 = null;
                    i++;
                    if (i == 16 && i < count)
                        obj.Col17 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col17 = null;
                    i++;
                    if (i == 17 && i < count)
                        obj.Col18 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col18 = null;
                    i++;
                    if (i == 18 && i < count)
                        obj.Col19 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col19 = null;
                    i++;
                    if (i == 19 && i < count)
                        obj.Col20 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col20 = null;
                    i++;
                    if (i == 20 && i < count)
                        obj.Col21 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col21 = null;
                    i++;
                    if (i == 21 && i < count)
                        obj.Col22 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col22 = null;
                    i++;
                    if (i == 22 && i < count)
                        obj.Col23 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col23 = null;
                    i++;
                    if (i == 23 && i < count)
                        obj.Col24 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col24 = null;
                    i++;
                    if (i == 24 && i < count)
                        obj.Col25 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col25 = null;
                    i++;
                    if (i == 25 && i < count)
                        obj.Col26 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col26 = null;
                    i++;
                    if (i == 26 && i < count)
                        obj.Col27 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col27 = null;
                    i++;
                    if (i == 27 && i < count)
                        obj.Col28 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col28 = null;
                    i++;
                    if (i == 28 && i < count)
                        obj.Col29 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col29 = null;
                    i++;
                    if (i == 29 && i < count)
                        obj.Col30 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col30 = null;
                    i++;
                    if (i == 30 && i < count)
                        obj.Col31 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col31 = null;
                    i++;
                    if (i == 31 && i < count)
                        obj.Col32 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col32 = null;
                    i++;
                    if (i == 32 && i < count)
                        obj.Col33 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col33 = null;
                    i++;
                    if (i == 33 && i < count)
                        obj.Col34 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col34 = null;
                    i++;
                    if (i == 34 && i < count)
                        obj.Col35 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col35 = null;
                    i++;
                    if (i == 35 && i < count)
                        obj.Col36 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col36 = null;
                    i++;
                    if (i == 36 && i < count)
                        obj.Col37 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col37 = null;
                    i++;
                    if (i == 37 && i < count)
                        obj.Col38 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col38 = null;
                    i++;
                    if (i == 38 && i < count)
                        obj.Col39 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col39 = null;
                    i++;
                    if (i == 39 && i < count)
                        obj.Col40 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col40 = null;
                    i++;
                    if (i == 40 && i < count)
                        obj.Col41 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col41 = null;
                    i++;
                    if (i == 41 && i < count)
                        obj.Col42 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col42 = null;
                    i++;
                    if (i == 42 && i < count)
                        obj.Col43 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col43 = null;
                    i++;
                    if (i == 43 && i < count)
                        obj.Col44 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col44 = null;
                    i++;
                    if (i == 44 && i < count)
                        obj.Col45 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col45 = null;
                    i++;
                    if (i == 45 && i < count)
                        obj.Col46 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col46 = null;
                    i++;
                    if (i == 46 && i < count)
                        obj.Col47 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col47 = null;
                    i++;
                    if (i == 47 && i < count)
                        obj.Col48 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col48 = null;
                    i++;
                    if (i == 48 && i < count)
                        obj.Col49 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col49 = null;
                    i++;
                    if (i == 49 && i < count)
                        obj.Col50 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col50 = null;
                    i++;
                    if (i == 50 && i < count)
                        obj.Col51 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col51 = null;
                    i++;
                    if (i == 51 && i < count)
                        obj.Col52 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col52 = null;
                    i++;
                    if (i == 52 && i < count)
                        obj.Col53 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col53 = null;
                    i++;

                    if (i == 53 && i < count)
                        obj.Col54 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col54 = null;
                    i++;
                    if (i == 54 && i < count)
                        obj.Col55 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col55 = null;
                    i++;
                    if (i == 55 && i < count)
                        obj.Col56 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col56 = null;
                    i++;
                    if (i == 56 && i < count)
                        obj.Col57 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col57 = null;
                    i++;
                    if (i == 57 && i < count)
                        obj.Col58 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col58 = null;
                    i++;
                    if (i == 58 && i < count)
                        obj.Col59 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col59 = null;
                    i++;
                    if (i == 59 && i < count)
                        obj.Col60 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col60 = null;
                    i++;
                    if (i == 60 && i < count)
                        obj.Col61 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col61 = null;
                    i++;
                    if (i == 61 && i < count)
                        obj.Col62 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col62 = null;
                    i++;
                    if (i == 62 && i < count)
                        obj.Col63 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col63 = null;
                    i++;
                    if (i == 63 && i < count)
                        obj.Col64 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col64 = null;
                    i++;
                    if (i == 64 && i < count)
                        obj.Col65 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col65 = null;
                    i++;
                    if (i == 65 && i < count)
                        obj.Col66 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col66 = null;
                    i++;
                    if (i == 66 && i < count)
                        obj.Col67 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col67 = null;
                    i++;
                    if (i == 67 && i < count)
                        obj.Col68 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col68 = null;
                    i++;
                    if (i == 68 && i < count)
                        obj.Col69 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col69 = null;
                    i++;
                    if (i == 69 && i < count)
                        obj.Col70 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col70 = null;
                    i++;
                    if (i == 70 && i < count)
                        obj.Col71 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col71 = null;
                    i++;
                    if (i == 71 && i < count)
                        obj.Col72 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col72 = null;
                    i++;
                    if (i == 72 && i < count)
                        obj.Col73 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col73 = null;
                    i++;
                    if (i == 73 && i < count)
                        obj.Col74 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col74 = null;
                    i++;
                    if (i == 74 && i < count)
                        obj.Col75 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col75 = null;
                    i++;


                    if (i == 75 && i < count)
                        obj.Col76 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col76 = null;
                    i++;
                    if (i == 76 && i < count)
                        obj.Col77 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col77 = null;
                    i++;
                    if (i == 77 && i < count)
                        obj.Col78 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col78 = null;
                    i++;
                    if (i == 78 && i < count)
                        obj.Col79 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col79 = null;
                    i++;
                    if (i == 79 && i < count)
                        obj.Col80 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col80 = null;
                    i++;
                    if (i == 80 && i < count)
                        obj.Col81 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col81 = null;
                    i++;
                    if (i == 81 && i < count)
                        obj.Col82 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col82 = null;
                    i++;
                    if (i == 82 && i < count)
                        obj.Col83 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col83 = null;
                    i++;
                    if (i == 83 && i < count)
                        obj.Col84 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col84 = null;
                    i++;
                    if (i == 84 && i < count)
                        obj.Col85 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col85 = null;
                    i++;
                    if (i == 85 && i < count)
                        obj.Col86 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col86 = null;
                    i++;
                    if (i == 86 && i < count)
                        obj.Col87 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col87 = null;
                    i++;
                    if (i == 87 && i < count)
                        obj.Col88 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col88 = null;
                    i++;
                    if (i == 88 && i < count)
                        obj.Col89 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col89 = null;
                    i++;
                    if (i == 89 && i < count)
                        obj.Col90 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col90 = null;
                    i++;
                    if (i == 90 && i < count)
                        obj.Col91 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col91 = null;
                    i++;
                    if (i == 91 && i < count)
                        obj.Col92 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col92 = null;
                    i++;
                    if (i == 92 && i < count)
                        obj.Col93 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col93 = null;
                    i++;
                    if (i == 93 && i < count)
                        obj.Col94 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col94 = null;
                    i++;
                    if (i == 94 && i < count)
                        obj.Col95 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col95 = null;
                    i++;
                    if (i == 95 && i < count)
                        obj.Col96 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col96 = null;
                    i++;
                    if (i == 96 && i < count)
                        obj.Col97 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col97 = null;
                    i++;
                    if (i == 97 && i < count)
                        obj.Col98 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col98 = null;
                    i++;
                    if (i == 98 && i < count)
                        obj.Col99 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col99 = null;
                    i++;
                    if (i == 99 && i < count)
                        obj.Col100 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col100 = null;
                    i++;
                    if (i == 100 && i < count)
                        obj.Col101 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col101 = null;
                    i++;
                    if (i == 101 && i < count)
                        obj.Col102 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col102 = null;
                    i++;
                    if (i == 102 && i < count)
                        obj.Col103 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col103 = null;
                    i++;
                    if (i == 103 && i < count)
                        obj.Col104 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col104 = null;
                    i++;
                    if (i == 104 && i < count)
                        obj.Col105 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col105 = null;
                    i++;
                    if (i == 105 && i < count)
                        obj.Col106 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col106 = null;
                    i++;
                    if (i == 106 && i < count)
                        obj.Col107 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col107 = null;
                    i++;
                    if (i == 107 && i < count)
                        obj.Col108 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col108 = null;
                    i++;
                    if (i == 108 && i < count)
                        obj.Col109 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col109 = null;
                    i++;
                    if (i == 109 && i < count)
                        obj.Col110 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col110 = null;
                    i++;
                    if (i == 110 && i < count)
                        obj.Col111 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col111 = null;
                    i++;
                    if (i == 111 && i < count)
                        obj.Col112 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col112 = null;
                    i++;
                    if (i == 112 && i < count)
                        obj.Col113 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col113 = null;
                    i++;
                    if (i == 113 && i < count)
                        obj.Col114 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col114 = null;
                    i++;
                    if (i == 114 && i < count)
                        obj.Col115 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col115 = null;
                    i++;
                    if (i == 115 && i < count)
                        obj.Col116 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col116 = null;
                    i++;
                    if (i == 116 && i < count)
                        obj.Col117 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col117 = null;
                    i++;
                    if (i == 117 && i < count)
                        obj.Col118 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col118 = null;
                    i++;
                    if (i == 118 && i < count)
                        obj.Col119 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col119 = null;
                    i++;
                    if (i == 119 && i < count)
                        obj.Col120 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col120 = null;
                    i++;

                    sc.WorkHourPlanners.Add(obj);
                    sc.SaveChanges();


                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public virtual void SaveMethod1Planner(WorkHoursPlannerClass ob)
        {
            try
            {
                string currentTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                var checkuser = sc.WorkHourPlanners.Where(x => x.UserName.Equals(ob.UserName) && x.dates.Equals(ob.dates) && x.WRID.Equals(ob.WRID) && x.Position.Equals(ob.Position)).FirstOrDefault();
                if (checkuser == null)
                {
                    var obj = new WorkHoursPlannerClass()
                    {
                        UserName = ob.UserName,
                        FullName = ob.FullName,
                        Position = ob.Position,
                        Department = ob.Department,
                        TotalHours = ob.TotalHours,
                        RestHours = ob.RestHours,
                        Remarks = ob.Remarks,
                        options = ob.options,
                        options1 = ":",
                        RestHourAny24 = ob.RestHourAny24,
                        RestHourAny7day = ob.RestHourAny7day,
                        dates = ob.dates,
                        NonConfirmities = ob.NonConfirmities,
                        NonConfirmities1 = ob.NonConfirmities1,
                        datetimes = Convert.ToDateTime(currentTime),
                        opa = false,   // have to set after 
                        opayoung = ob.opayoung,
                        Cellcount = ob.Cellcount,
                        hrs = ob.hrs,
                        Colors = ob.Colors,
                        MonthName = ob.dates.ToString("MMMM"),
                        YearValue = ob.dates.Year.ToString(),
                        WRID = ob.WRID,
                        Days = ob.dates.DayOfWeek.ToString().Substring(0, 3)

                    };

                    var cid = sc.CrewDetails.Select(s => new { s.Id, s.UserName, s.position }).Where(x => x.UserName.Equals(obj.UserName) && x.position.Equals(obj.Position)).FirstOrDefault().Id;
                    obj.cid = cid;

                    //.......OverTime Calculation....

                    string dayofFirstDay = ob.dates.DayOfWeek.ToString().Substring(0, 3);
                    var overtim = sc.OverTimes.Where(x => x.UserName.Equals(obj.UserName)).FirstOrDefault();

                    int normal = 0;
                    int Sat = 0;
                    int Sun = 0;
                    int Holiday = 0;
                    int hr = 0;

                    if (overtim != null)
                    {
                        normal = Convert.ToInt32(overtim.NrmlWHrs);
                        Sat = Convert.ToInt32(overtim.SatWHrs);
                        Sun = Convert.ToInt32(overtim.SunWhrs);
                        Holiday = Convert.ToInt32(overtim.HolidayWHrs);
                    }

                    if ((dayofFirstDay == "Mon") || (dayofFirstDay == "Tue") || (dayofFirstDay == "Wed") || (dayofFirstDay == "Thu") || (dayofFirstDay == "Fri"))
                    {
                        hr = normal;
                        obj.Normal_WKH = normal;
                    }
                    else if ((dayofFirstDay == "Sat"))
                    {
                        hr = Sat;
                        obj.Normal_WKH = Sat;

                    }
                    else if ((dayofFirstDay == "Sun"))
                    {
                        hr = Sun;
                        obj.Normal_WKH = Sun;

                    }
                    else
                    {
                        hr = Holiday;
                        obj.Normal_WKH = Holiday;
                    }
                    double wh = Convert.ToDouble(obj.TotalHours);
                    double overtime = 0.0;
                    overtime = wh - hr;
                    overtime = overtime > 0 ? overtime : 0.0;



                    string dayname = "";
                    dayname = dayofFirstDay + "WHrs";
                    int dayscount = 0;

                    var checkholiday = sc.Holidays.Where(x => x.HolidayDate == obj.dates).FirstOrDefault();
                    if (checkholiday != null)
                    {
                        dayscount = overtim != null ? overtim.HolidayWHrs : 0;
                    }
                    else
                    {
                        if (dayname == "SatWHrs")
                            dayscount = overtim != null ? overtim.SatWHrs : 0;

                        else if (dayname == "SunWHrs")
                            dayscount = overtim != null ? overtim.SunWhrs : 0;
                        else
                            dayscount = overtim != null ? overtim.NrmlWHrs : 0;

                    }

                    decimal ovcou = Convert.ToDecimal(obj.TotalHours);
                    decimal ovcou1 = Convert.ToDecimal(dayscount);

                    decimal finalovcou1 = ovcou - ovcou1;
                    finalovcou1 = finalovcou1 > 0 ? finalovcou1 : Convert.ToDecimal(0.0);
                    obj.NormalOT1 = dayscount;
                    obj.overtime = finalovcou1;
                    obj.RuleNames = string.IsNullOrEmpty(obj.RuleNames) ? "Normal" : obj.RuleNames;



                    string[] hrsFetch = ob.hrs.TrimEnd(',').Split(',');
                    int i = 0;
                    int count = hrsFetch.Count();

                    if (i == 0 && i < count)
                        obj.Col1 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col1 = null;
                    i++;
                    if (i == 1 && i < count)
                        obj.Col2 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col2 = null;
                    i++;
                    if (i == 2 && i < count)
                        obj.Col3 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col3 = null;
                    i++;
                    if (i == 3 && i < count)
                        obj.Col4 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col4 = null;
                    i++;
                    if (i == 4 && i < count)
                        obj.Col5 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col5 = null;
                    i++;
                    if (i == 5 && i < count)
                        obj.Col6 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col6 = null;
                    i++;
                    if (i == 6 && i < count)
                        obj.Col7 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col7 = null;
                    i++;
                    if (i == 7 && i < count)
                        obj.Col8 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col8 = null;
                    i++;
                    if (i == 8 && i < count)
                        obj.Col9 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col9 = null;
                    i++;
                    if (i == 9 && i < count)
                        obj.Col10 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col10 = null;
                    i++;
                    if (i == 10 && i < count)
                        obj.Col11 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col11 = null;
                    i++;
                    if (i == 11 && i < count)
                        obj.Col12 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col12 = null;
                    i++;
                    if (i == 12 && i < count)
                        obj.Col13 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col13 = null;
                    i++;
                    if (i == 13 && i < count)
                        obj.Col14 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col14 = null;
                    i++;
                    if (i == 14 && i < count)
                        obj.Col15 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col15 = null;
                    i++;
                    if (i == 15 && i < count)
                        obj.Col16 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col16 = null;
                    i++;
                    if (i == 16 && i < count)
                        obj.Col17 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col17 = null;
                    i++;
                    if (i == 17 && i < count)
                        obj.Col18 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col18 = null;
                    i++;
                    if (i == 18 && i < count)
                        obj.Col19 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col19 = null;
                    i++;
                    if (i == 19 && i < count)
                        obj.Col20 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col20 = null;
                    i++;
                    if (i == 20 && i < count)
                        obj.Col21 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col21 = null;
                    i++;
                    if (i == 21 && i < count)
                        obj.Col22 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col22 = null;
                    i++;
                    if (i == 22 && i < count)
                        obj.Col23 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col23 = null;
                    i++;
                    if (i == 23 && i < count)
                        obj.Col24 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col24 = null;
                    i++;
                    if (i == 24 && i < count)
                        obj.Col25 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col25 = null;
                    i++;
                    if (i == 25 && i < count)
                        obj.Col26 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col26 = null;
                    i++;
                    if (i == 26 && i < count)
                        obj.Col27 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col27 = null;
                    i++;
                    if (i == 27 && i < count)
                        obj.Col28 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col28 = null;
                    i++;
                    if (i == 28 && i < count)
                        obj.Col29 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col29 = null;
                    i++;
                    if (i == 29 && i < count)
                        obj.Col30 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col30 = null;
                    i++;
                    if (i == 30 && i < count)
                        obj.Col31 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col31 = null;
                    i++;
                    if (i == 31 && i < count)
                        obj.Col32 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col32 = null;
                    i++;
                    if (i == 32 && i < count)
                        obj.Col33 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col33 = null;
                    i++;
                    if (i == 33 && i < count)
                        obj.Col34 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col34 = null;
                    i++;
                    if (i == 34 && i < count)
                        obj.Col35 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col35 = null;
                    i++;
                    if (i == 35 && i < count)
                        obj.Col36 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col36 = null;
                    i++;
                    if (i == 36 && i < count)
                        obj.Col37 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col37 = null;
                    i++;
                    if (i == 37 && i < count)
                        obj.Col38 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col38 = null;
                    i++;
                    if (i == 38 && i < count)
                        obj.Col39 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col39 = null;
                    i++;
                    if (i == 39 && i < count)
                        obj.Col40 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col40 = null;
                    i++;
                    if (i == 40 && i < count)
                        obj.Col41 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col41 = null;
                    i++;
                    if (i == 41 && i < count)
                        obj.Col42 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col42 = null;
                    i++;
                    if (i == 42 && i < count)
                        obj.Col43 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col43 = null;
                    i++;
                    if (i == 43 && i < count)
                        obj.Col44 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col44 = null;
                    i++;
                    if (i == 44 && i < count)
                        obj.Col45 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col45 = null;
                    i++;
                    if (i == 45 && i < count)
                        obj.Col46 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col46 = null;
                    i++;
                    if (i == 46 && i < count)
                        obj.Col47 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col47 = null;
                    i++;
                    if (i == 47 && i < count)
                        obj.Col48 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col48 = null;
                    i++;
                    if (i == 48 && i < count)
                        obj.Col49 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col49 = null;
                    i++;
                    if (i == 49 && i < count)
                        obj.Col50 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col50 = null;
                    i++;
                    if (i == 50 && i < count)
                        obj.Col51 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col51 = null;
                    i++;
                    if (i == 51 && i < count)
                        obj.Col52 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col52 = null;
                    i++;
                    if (i == 52 && i < count)
                        obj.Col53 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col53 = null;
                    i++;

                    if (i == 53 && i < count)
                        obj.Col54 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col54 = null;
                    i++;
                    if (i == 54 && i < count)
                        obj.Col55 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col55 = null;
                    i++;
                    if (i == 55 && i < count)
                        obj.Col56 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col56 = null;
                    i++;
                    if (i == 56 && i < count)
                        obj.Col57 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col57 = null;
                    i++;
                    if (i == 57 && i < count)
                        obj.Col58 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col58 = null;
                    i++;
                    if (i == 58 && i < count)
                        obj.Col59 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col59 = null;
                    i++;
                    if (i == 59 && i < count)
                        obj.Col60 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col60 = null;
                    i++;
                    if (i == 60 && i < count)
                        obj.Col61 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col61 = null;
                    i++;
                    if (i == 61 && i < count)
                        obj.Col62 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col62 = null;
                    i++;
                    if (i == 62 && i < count)
                        obj.Col63 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col63 = null;
                    i++;
                    if (i == 63 && i < count)
                        obj.Col64 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col64 = null;
                    i++;
                    if (i == 64 && i < count)
                        obj.Col65 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col65 = null;
                    i++;
                    if (i == 65 && i < count)
                        obj.Col66 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col66 = null;
                    i++;
                    if (i == 66 && i < count)
                        obj.Col67 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col67 = null;
                    i++;
                    if (i == 67 && i < count)
                        obj.Col68 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col68 = null;
                    i++;
                    if (i == 68 && i < count)
                        obj.Col69 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col69 = null;
                    i++;
                    if (i == 69 && i < count)
                        obj.Col70 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col70 = null;
                    i++;
                    if (i == 70 && i < count)
                        obj.Col71 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col71 = null;
                    i++;
                    if (i == 71 && i < count)
                        obj.Col72 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col72 = null;
                    i++;
                    if (i == 72 && i < count)
                        obj.Col73 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col73 = null;
                    i++;
                    if (i == 73 && i < count)
                        obj.Col74 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col74 = null;
                    i++;
                    if (i == 74 && i < count)
                        obj.Col75 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col75 = null;
                    i++;


                    if (i == 75 && i < count)
                        obj.Col76 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col76 = null;
                    i++;
                    if (i == 76 && i < count)
                        obj.Col77 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col77 = null;
                    i++;
                    if (i == 77 && i < count)
                        obj.Col78 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col78 = null;
                    i++;
                    if (i == 78 && i < count)
                        obj.Col79 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col79 = null;
                    i++;
                    if (i == 79 && i < count)
                        obj.Col80 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col80 = null;
                    i++;
                    if (i == 80 && i < count)
                        obj.Col81 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col81 = null;
                    i++;
                    if (i == 81 && i < count)
                        obj.Col82 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col82 = null;
                    i++;
                    if (i == 82 && i < count)
                        obj.Col83 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col83 = null;
                    i++;
                    if (i == 83 && i < count)
                        obj.Col84 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col84 = null;
                    i++;
                    if (i == 84 && i < count)
                        obj.Col85 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col85 = null;
                    i++;
                    if (i == 85 && i < count)
                        obj.Col86 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col86 = null;
                    i++;
                    if (i == 86 && i < count)
                        obj.Col87 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col87 = null;
                    i++;
                    if (i == 87 && i < count)
                        obj.Col88 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col88 = null;
                    i++;
                    if (i == 88 && i < count)
                        obj.Col89 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col89 = null;
                    i++;
                    if (i == 89 && i < count)
                        obj.Col90 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col90 = null;
                    i++;
                    if (i == 90 && i < count)
                        obj.Col91 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col91 = null;
                    i++;
                    if (i == 91 && i < count)
                        obj.Col92 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col92 = null;
                    i++;
                    if (i == 92 && i < count)
                        obj.Col93 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col93 = null;
                    i++;
                    if (i == 93 && i < count)
                        obj.Col94 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col94 = null;
                    i++;
                    if (i == 94 && i < count)
                        obj.Col95 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col95 = null;
                    i++;
                    if (i == 95 && i < count)
                        obj.Col96 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col96 = null;
                    i++;
                    if (i == 96 && i < count)
                        obj.Col97 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col97 = null;
                    i++;
                    if (i == 97 && i < count)
                        obj.Col98 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col98 = null;
                    i++;
                    if (i == 98 && i < count)
                        obj.Col99 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col99 = null;
                    i++;
                    if (i == 99 && i < count)
                        obj.Col100 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col100 = null;
                    i++;
                    if (i == 100 && i < count)
                        obj.Col101 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col101 = null;
                    i++;
                    if (i == 101 && i < count)
                        obj.Col102 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col102 = null;
                    i++;
                    if (i == 102 && i < count)
                        obj.Col103 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col103 = null;
                    i++;
                    if (i == 103 && i < count)
                        obj.Col104 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col104 = null;
                    i++;
                    if (i == 104 && i < count)
                        obj.Col105 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col105 = null;
                    i++;
                    if (i == 105 && i < count)
                        obj.Col106 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col106 = null;
                    i++;
                    if (i == 106 && i < count)
                        obj.Col107 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col107 = null;
                    i++;
                    if (i == 107 && i < count)
                        obj.Col108 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col108 = null;
                    i++;
                    if (i == 108 && i < count)
                        obj.Col109 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col109 = null;
                    i++;
                    if (i == 109 && i < count)
                        obj.Col110 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col110 = null;
                    i++;
                    if (i == 110 && i < count)
                        obj.Col111 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col111 = null;
                    i++;
                    if (i == 111 && i < count)
                        obj.Col112 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col112 = null;
                    i++;
                    if (i == 112 && i < count)
                        obj.Col113 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col113 = null;
                    i++;
                    if (i == 113 && i < count)
                        obj.Col114 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col114 = null;
                    i++;
                    if (i == 114 && i < count)
                        obj.Col115 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col115 = null;
                    i++;
                    if (i == 115 && i < count)
                        obj.Col116 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col116 = null;
                    i++;
                    if (i == 116 && i < count)
                        obj.Col117 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col117 = null;
                    i++;
                    if (i == 117 && i < count)
                        obj.Col118 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col118 = null;
                    i++;
                    if (i == 118 && i < count)
                        obj.Col119 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col119 = null;
                    i++;
                    if (i == 119 && i < count)
                        obj.Col120 = hrsFetch[i] == "0" ? "0" : "1";
                    else
                        obj.Col120 = null;
                    i++;

                    sc.WorkHourPlanners.Add(obj);
                    sc.SaveChanges();



                }

                //For current date insertion...
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        public virtual void UpdatePlanner(WorkHoursPlannerClass ob)
        {
            try
            {
                var currentTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");


                var obj = new WorkHoursPlannerClass();
                var checkuser = sc.WorkHourPlanners.Where(x => x.UserName.Equals(ob.UserName) && x.dates.Equals(ob.dates) && x.WRID.Equals(ob.WRID) && x.Position.Equals(ob.Position)).FirstOrDefault();
                if (checkuser != null)
                {

                    obj.wid = checkuser.wid;
                    ob.wid = checkuser.wid;
                //}

                obj.UserName = ob.UserName;
                obj.Remarks = ob.Remarks;
                obj.FullName = ob.FullName;
                obj.Position = ob.Position;
                obj.Department = ob.Department;
                obj.TotalHours = ob.TotalHours;
                obj.RestHours = ob.RestHours;
                obj.options = ob.options;
                obj.options1 = !string.IsNullOrEmpty(ob.NonConfirmities) ? ":" : string.Empty;
                obj.RestHourAny24 = ob.RestHourAny24;
                obj.RestHourAny7day = ob.RestHourAny7day;
                obj.dates = ob.dates;
                obj.NonConfirmities = ob.NonConfirmities;
                obj.NonConfirmities1 = ob.NonConfirmities1;
                obj.datetimes = Convert.ToDateTime(currentTime);
                obj.opa = false;   // have to set after 
                obj.opayoung = ob.opayoung;
                obj.Cellcount = ob.Cellcount;
                obj.hrs = ob.hrs;
                obj.Colors = ob.Colors;
                obj.MonthName = ob.dates.ToString("MMMM");
                obj.YearValue = ob.dates.Year.ToString();
                obj.WRID = ob.WRID;
                obj.Days = ob.dates.DayOfWeek.ToString().Substring(0, 3);


                var cid = sc.CrewDetails.Select(s => new { s.Id, s.UserName, s.position }).Where(x => x.UserName.Equals(obj.UserName) && x.position.Equals(obj.Position)).FirstOrDefault().Id;
                obj.cid = cid;

                //.......OverTime Calculation....

                string dayofFirstDay = ob.dates.DayOfWeek.ToString().Substring(0, 3);
                var overtim = sc.OverTimes.Where(x => x.UserName.Equals(obj.UserName)).FirstOrDefault();

                int normal = 0;
                int Sat = 0;
                int Sun = 0;
                int Holiday = 0;
                int hr = 0;

                if (overtim != null)
                {
                    normal = Convert.ToInt32(overtim.NrmlWHrs);
                    Sat = Convert.ToInt32(overtim.SatWHrs);
                    Sun = Convert.ToInt32(overtim.SunWhrs);
                    Holiday = Convert.ToInt32(overtim.HolidayWHrs);
                }

                if ((dayofFirstDay == "Mon") || (dayofFirstDay == "Tue") || (dayofFirstDay == "Wed") || (dayofFirstDay == "Thu") || (dayofFirstDay == "Fri"))
                {
                    hr = normal;
                    obj.Normal_WKH = normal;
                }
                else if ((dayofFirstDay == "Sat"))
                {
                    hr = Sat;
                    obj.Normal_WKH = Sat;

                }
                else if ((dayofFirstDay == "Sun"))
                {
                    hr = Sun;
                    obj.Normal_WKH = Sun;

                }
                else
                {
                    hr = Holiday;
                    obj.Normal_WKH = Holiday;
                }
                double wh = Convert.ToDouble(obj.TotalHours);
                double overtime = 0.0;
                overtime = wh - hr;
                overtime = overtime > 0 ? overtime : 0.0;



                string dayname = "";
                dayname = dayofFirstDay + "WHrs";
                int dayscount = 0;

                var checkholiday = sc.Holidays.Where(x => x.HolidayDate == obj.dates).FirstOrDefault();
                if (checkholiday != null)
                {
                    dayscount = overtim != null ? overtim.HolidayWHrs : 0;
                }
                else
                {
                    if (dayname == "SatWHrs")
                        dayscount = overtim != null ? overtim.SatWHrs : 0;

                    else if (dayname == "SunWHrs")
                        dayscount = overtim != null ? overtim.SunWhrs : 0;
                    else
                        dayscount = overtim != null ? overtim.NrmlWHrs : 0;

                }

                decimal ovcou = Convert.ToDecimal(obj.TotalHours);
                decimal ovcou1 = Convert.ToDecimal(dayscount);

                decimal finalovcou1 = ovcou - ovcou1;
                finalovcou1 = finalovcou1 > 0 ? finalovcou1 : Convert.ToDecimal(0.0);
                obj.NormalOT1 = dayscount;
                obj.overtime = finalovcou1;
                obj.RuleNames = string.IsNullOrEmpty(ob.RuleNames) ? "Normal" : ob.RuleNames;




                string[] hrsFetch = ob.hrs.TrimEnd(',').Split(',');
                int i = 0;
                int count = hrsFetch.Count();

                if (i == 0 && i < count)
                    obj.Col1 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col1 = null;
                i++;
                if (i == 1 && i < count)
                    obj.Col2 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col2 = null;
                i++;
                if (i == 2 && i < count)
                    obj.Col3 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col3 = null;
                i++;
                if (i == 3 && i < count)
                    obj.Col4 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col4 = null;
                i++;
                if (i == 4 && i < count)
                    obj.Col5 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col5 = null;
                i++;
                if (i == 5 && i < count)
                    obj.Col6 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col6 = null;
                i++;
                if (i == 6 && i < count)
                    obj.Col7 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col7 = null;
                i++;
                if (i == 7 && i < count)
                    obj.Col8 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col8 = null;
                i++;
                if (i == 8 && i < count)
                    obj.Col9 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col9 = null;
                i++;
                if (i == 9 && i < count)
                    obj.Col10 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col10 = null;
                i++;
                if (i == 10 && i < count)
                    obj.Col11 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col11 = null;
                i++;
                if (i == 11 && i < count)
                    obj.Col12 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col12 = null;
                i++;
                if (i == 12 && i < count)
                    obj.Col13 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col13 = null;
                i++;
                if (i == 13 && i < count)
                    obj.Col14 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col14 = null;
                i++;
                if (i == 14 && i < count)
                    obj.Col15 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col15 = null;
                i++;
                if (i == 15 && i < count)
                    obj.Col16 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col16 = null;
                i++;
                if (i == 16 && i < count)
                    obj.Col17 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col17 = null;
                i++;
                if (i == 17 && i < count)
                    obj.Col18 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col18 = null;
                i++;
                if (i == 18 && i < count)
                    obj.Col19 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col19 = null;
                i++;
                if (i == 19 && i < count)
                    obj.Col20 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col20 = null;
                i++;
                if (i == 20 && i < count)
                    obj.Col21 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col21 = null;
                i++;
                if (i == 21 && i < count)
                    obj.Col22 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col22 = null;
                i++;
                if (i == 22 && i < count)
                    obj.Col23 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col23 = null;
                i++;
                if (i == 23 && i < count)
                    obj.Col24 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col24 = null;
                i++;
                if (i == 24 && i < count)
                    obj.Col25 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col25 = null;
                i++;
                if (i == 25 && i < count)
                    obj.Col26 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col26 = null;
                i++;
                if (i == 26 && i < count)
                    obj.Col27 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col27 = null;
                i++;
                if (i == 27 && i < count)
                    obj.Col28 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col28 = null;
                i++;
                if (i == 28 && i < count)
                    obj.Col29 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col29 = null;
                i++;
                if (i == 29 && i < count)
                    obj.Col30 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col30 = null;
                i++;
                if (i == 30 && i < count)
                    obj.Col31 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col31 = null;
                i++;
                if (i == 31 && i < count)
                    obj.Col32 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col32 = null;
                i++;
                if (i == 32 && i < count)
                    obj.Col33 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col33 = null;
                i++;
                if (i == 33 && i < count)
                    obj.Col34 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col34 = null;
                i++;
                if (i == 34 && i < count)
                    obj.Col35 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col35 = null;
                i++;
                if (i == 35 && i < count)
                    obj.Col36 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col36 = null;
                i++;
                if (i == 36 && i < count)
                    obj.Col37 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col37 = null;
                i++;
                if (i == 37 && i < count)
                    obj.Col38 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col38 = null;
                i++;
                if (i == 38 && i < count)
                    obj.Col39 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col39 = null;
                i++;
                if (i == 39 && i < count)
                    obj.Col40 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col40 = null;
                i++;
                if (i == 40 && i < count)
                    obj.Col41 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col41 = null;
                i++;
                if (i == 41 && i < count)
                    obj.Col42 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col42 = null;
                i++;
                if (i == 42 && i < count)
                    obj.Col43 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col43 = null;
                i++;
                if (i == 43 && i < count)
                    obj.Col44 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col44 = null;
                i++;
                if (i == 44 && i < count)
                    obj.Col45 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col45 = null;
                i++;
                if (i == 45 && i < count)
                    obj.Col46 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col46 = null;
                i++;
                if (i == 46 && i < count)
                    obj.Col47 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col47 = null;
                i++;
                if (i == 47 && i < count)
                    obj.Col48 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col48 = null;
                i++;
                if (i == 48 && i < count)
                    obj.Col49 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col49 = null;
                i++;
                if (i == 49 && i < count)
                    obj.Col50 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col50 = null;
                i++;
                if (i == 50 && i < count)
                    obj.Col51 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col51 = null;
                i++;
                if (i == 51 && i < count)
                    obj.Col52 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col52 = null;
                i++;
                if (i == 52 && i < count)
                    obj.Col53 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col53 = null;
                i++;

                if (i == 53 && i < count)
                    obj.Col54 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col54 = null;
                i++;
                if (i == 54 && i < count)
                    obj.Col55 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col55 = null;
                i++;
                if (i == 55 && i < count)
                    obj.Col56 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col56 = null;
                i++;
                if (i == 56 && i < count)
                    obj.Col57 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col57 = null;
                i++;
                if (i == 57 && i < count)
                    obj.Col58 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col58 = null;
                i++;
                if (i == 58 && i < count)
                    obj.Col59 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col59 = null;
                i++;
                if (i == 59 && i < count)
                    obj.Col60 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col60 = null;
                i++;
                if (i == 60 && i < count)
                    obj.Col61 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col61 = null;
                i++;
                if (i == 61 && i < count)
                    obj.Col62 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col62 = null;
                i++;
                if (i == 62 && i < count)
                    obj.Col63 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col63 = null;
                i++;
                if (i == 63 && i < count)
                    obj.Col64 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col64 = null;
                i++;
                if (i == 64 && i < count)
                    obj.Col65 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col65 = null;
                i++;
                if (i == 65 && i < count)
                    obj.Col66 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col66 = null;
                i++;
                if (i == 66 && i < count)
                    obj.Col67 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col67 = null;
                i++;
                if (i == 67 && i < count)
                    obj.Col68 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col68 = null;
                i++;
                if (i == 68 && i < count)
                    obj.Col69 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col69 = null;
                i++;
                if (i == 69 && i < count)
                    obj.Col70 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col70 = null;
                i++;
                if (i == 70 && i < count)
                    obj.Col71 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col71 = null;
                i++;
                if (i == 71 && i < count)
                    obj.Col72 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col72 = null;
                i++;
                if (i == 72 && i < count)
                    obj.Col73 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col73 = null;
                i++;
                if (i == 73 && i < count)
                    obj.Col74 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col74 = null;
                i++;
                if (i == 74 && i < count)
                    obj.Col75 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col75 = null;
                i++;


                if (i == 75 && i < count)
                    obj.Col76 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col76 = null;
                i++;
                if (i == 76 && i < count)
                    obj.Col77 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col77 = null;
                i++;
                if (i == 77 && i < count)
                    obj.Col78 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col78 = null;
                i++;
                if (i == 78 && i < count)
                    obj.Col79 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col79 = null;
                i++;
                if (i == 79 && i < count)
                    obj.Col80 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col80 = null;
                i++;
                if (i == 80 && i < count)
                    obj.Col81 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col81 = null;
                i++;
                if (i == 81 && i < count)
                    obj.Col82 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col82 = null;
                i++;
                if (i == 82 && i < count)
                    obj.Col83 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col83 = null;
                i++;
                if (i == 83 && i < count)
                    obj.Col84 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col84 = null;
                i++;
                if (i == 84 && i < count)
                    obj.Col85 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col85 = null;
                i++;
                if (i == 85 && i < count)
                    obj.Col86 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col86 = null;
                i++;
                if (i == 86 && i < count)
                    obj.Col87 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col87 = null;
                i++;
                if (i == 87 && i < count)
                    obj.Col88 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col88 = null;
                i++;
                if (i == 88 && i < count)
                    obj.Col89 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col89 = null;
                i++;
                if (i == 89 && i < count)
                    obj.Col90 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col90 = null;
                i++;
                if (i == 90 && i < count)
                    obj.Col91 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col91 = null;
                i++;
                if (i == 91 && i < count)
                    obj.Col92 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col92 = null;
                i++;
                if (i == 92 && i < count)
                    obj.Col93 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col93 = null;
                i++;
                if (i == 93 && i < count)
                    obj.Col94 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col94 = null;
                i++;
                if (i == 94 && i < count)
                    obj.Col95 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col95 = null;
                i++;
                if (i == 95 && i < count)
                    obj.Col96 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col96 = null;
                i++;
                if (i == 96 && i < count)
                    obj.Col97 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col97 = null;
                i++;
                if (i == 97 && i < count)
                    obj.Col98 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col98 = null;
                i++;
                if (i == 98 && i < count)
                    obj.Col99 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col99 = null;
                i++;
                if (i == 99 && i < count)
                    obj.Col100 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col100 = null;
                i++;
                if (i == 100 && i < count)
                    obj.Col101 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col101 = null;
                i++;
                if (i == 101 && i < count)
                    obj.Col102 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col102 = null;
                i++;
                if (i == 102 && i < count)
                    obj.Col103 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col103 = null;
                i++;
                if (i == 103 && i < count)
                    obj.Col104 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col104 = null;
                i++;
                if (i == 104 && i < count)
                    obj.Col105 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col105 = null;
                i++;
                if (i == 105 && i < count)
                    obj.Col106 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col106 = null;
                i++;
                if (i == 106 && i < count)
                    obj.Col107 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col107 = null;
                i++;
                if (i == 107 && i < count)
                    obj.Col108 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col108 = null;
                i++;
                if (i == 108 && i < count)
                    obj.Col109 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col109 = null;
                i++;
                if (i == 109 && i < count)
                    obj.Col110 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col110 = null;
                i++;
                if (i == 110 && i < count)
                    obj.Col111 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col111 = null;
                i++;
                if (i == 111 && i < count)
                    obj.Col112 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col112 = null;
                i++;
                if (i == 112 && i < count)
                    obj.Col113 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col113 = null;
                i++;
                if (i == 113 && i < count)
                    obj.Col114 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col114 = null;
                i++;
                if (i == 114 && i < count)
                    obj.Col115 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col115 = null;
                i++;
                if (i == 115 && i < count)
                    obj.Col116 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col116 = null;
                i++;
                if (i == 116 && i < count)
                    obj.Col117 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col117 = null;
                i++;
                if (i == 117 && i < count)
                    obj.Col118 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col118 = null;
                i++;
                if (i == 118 && i < count)
                    obj.Col119 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col119 = null;
                i++;
                if (i == 119 && i < count)
                    obj.Col120 = hrsFetch[i] == "0" ? "0" : "1";
                else
                    obj.Col120 = null;
                i++;

                var local = sc.Set<WorkHoursPlannerClass>()
                    .Local
                    .FirstOrDefault(f => f.wid == checkuser.wid);
                if (local != null)
                {
                    sc.Entry(local).State = EntityState.Detached;
                }

                sc.Entry(obj).State = EntityState.Modified;
                sc.SaveChanges();

            }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        public virtual bool OPAStartStopTime(string startOPAdateid, OPAStartStopClass sdaopastatr)
        {
            bool bms = false;
            if (sdaopastatr != null)
            {
                string startopaid = sdaopastatr.startDateID.ToString();
                string stopopaid = sdaopastatr.stopDateID.ToString();
                if (startopaid.Contains("IDL"))
                {
                    if (startopaid == startOPAdateid || stopopaid == startOPAdateid)
                        bms = true;
                }
                else
                    bms = true;
            }


            return bms;

        }

        // Young Seaferer......

        public virtual bool CheckDateYoung(DateTime bm1, DateTime bm2)
        {
            var ssa = bm2.AddYears(-18) < bm1;
            return ssa;

        }

        public virtual int CheckRetardYoung(string Dateid, DateTime Dt11, List<RetardtblClass> CheckRetar)
        {
            int columns;
            var data = CheckRetar.Where(x => x.DateID == Dateid && x.RDate == Dt11).FirstOrDefault();
            if (data != null)
            {
                string retard1 = data.RET_ADV.ToString();
                decimal times = 0;
                if (retard1 == "RETARD")
                {
                    times = Convert.ToDecimal(data.Times);
                    times = (times * 4) + 96;
                    columns = Convert.ToInt32(times);
                }
                else
                {
                    times = Convert.ToDecimal(data.Times);
                    times = 96 - (times * 4);
                    columns = Convert.ToInt32(times);
                }

            }
            else
                columns = 96;

            return columns;
        }



    }
}
