using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkShipVersionII.Core
{
       public class ComCalenders
       {
              private readonly ShipmentContaxt sc;

              public ComCalenders()
              {
                     sc = new ShipmentContaxt();
              }

        public List<string> CommonCalender(string syearname, string smonthname)
        {


            syearname = syearname ?? DateTime.Now.Year.ToString();
            smonthname = smonthname ?? DateTime.Now.ToString("MMMM");

            int years = Convert.ToInt32(syearname);
            var monthid = DateTime.ParseExact(smonthname, "MMMM", CultureInfo.CurrentCulture).Month;
            int days = DateTime.DaysInMonth(years, monthid);

            DateTime TodayDate = new DateTime(years, monthid, 1);
            DateTime EndDate = TodayDate.AddMonths(1).AddDays(-1);
            var data = sc.Internationals.Where(x => (System.Data.Entity.DbFunctions.TruncateTime(x.RDate) >= TodayDate.Date && System.Data.Entity.DbFunctions.TruncateTime(x.RDate) <= EndDate.Date)).ToList();


            string weeks = "";
            string noofdays = "";
            for (int i = 1; i <= days; i++)
            {

                var birthDate = new DateTime(years, monthid, i);
                var thisYear = new DateTime(years, birthDate.Month, birthDate.Day);
                var dayOfWeek = thisYear.ToString("ddd");
                weeks = dayOfWeek;

                var Checkretard = data.Where(x => x.RDate == birthDate).FirstOrDefault();
                string dateis = i + " " + Environment.NewLine + weeks;

                if (Checkretard != null)
                {
                    if (Checkretard.RET_ADV == "RETARD")
                    {
                        noofdays += dateis + ",";
                        noofdays += dateis + Environment.NewLine + "IDL" + ",";
                    }
                }
                else
                {
                    noofdays += dateis + ",";
                }



            }

            List<string> arr = noofdays.TrimEnd(',').Split(',').ToList();

            return arr.ToList();

        }


    }
}
