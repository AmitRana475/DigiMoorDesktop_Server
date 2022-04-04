using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkShipVersionII.Control;

namespace WorkShipVersionII.Core
{
       public class DateHelper
       {
              public static string objbm { get; set; }

              public DateHelper()
              {
                     objbm = "bms";
              }

              //the list of months in a year
              private static readonly string[] Months = new string[]
              {
            "January",
            "Febuary",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "Novemeber",
            "December"
              };


              public static string GetMonthDisplayName(int month)
              {
                     if (month < 1 || month > 12)
                            throw new ArgumentException("Invalid month. Must be between 1 and 12", "month");

                     return Months[month - 1];
              }

              //caches the days in a dict where the key is the number of days
              //the key is a hash of month, year
              static readonly Dictionary<KeyValuePair<int, int>, DayCell[]> daysArrays = new Dictionary<KeyValuePair<int, int>, DayCell[]>();


              //static readonly ShipmentContaxt sc = new ShipmentContaxt();

              public static DayCell[] GetDaysOfMonth(int month, int year)
              {
                     KeyValuePair<int, int> key = new KeyValuePair<int, int>(month, year);
                     int daysCount = DateTime.DaysInMonth(year, month);
                     if (daysArrays.ContainsKey(key))
                            return daysArrays[key];

                     List<DayCell> days = new List<DayCell>();

                     string monthname = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);



                     if (objbm == "bms")
                     {
                            var objs = new ComCalenders();
                            var arr = objs.CommonCalender(year.ToString(), monthname);

                            int fularr = arr.Count();
                            ////loadCalender.Clear() daysCount;



                            for (int i = 0; i < fularr; i++)
                            {

                                   string IDLNumberss = string.Empty;
                                   string s1, s2, str = "";
                                   s1 = arr[i].ToString().Substring(0, 2);
                                   s2 = arr[i].ToString().Substring(arr[i].ToString().Length - 3);

                                   if (s2 == "IDL")
                                   {
                                          str = s1.Trim() + s2;
                                          IDLNumberss = s2;
                                   }
                                   else
                                   {
                                          str = s1.Trim();
                                          IDLNumberss = string.Empty;
                                   }



                                   //string WIDss = (i + 1).ToString();
                                   //if (i == 4)
                                   //{
                                   //    IDLNumberss = "IDL";
                                   //    days.Add(new DayCell(i + 1, month, year, IDLNumberss, WIDss, (i + 1).ToString()));
                                   //    days.Add(new DayCell(i + 1, month, year, string.Empty, WIDss, (i + 1).ToString()));
                                   //    //i = i + 1;
                                   //}
                                   //else if (i == 14)
                                   //{
                                   //    IDLNumberss = "IDL";
                                   //    days.Add(new DayCell(i + 1, month, year, IDLNumberss, WIDss, (i + 1).ToString()));
                                   //    days.Add(new DayCell(i + 1, month, year, string.Empty, WIDss, (i + 1).ToString()));
                                   //    //i = i + 1;
                                   //}
                                   //else
                                   //{
                                   days.Add(new DayCell(Convert.ToInt32(s1), month, year, IDLNumberss, str, arr[i].ToString()));
                                   // days.Add(new DayCell(i + 1, month, year, IDLNumberss, WIDss, (i + 1).ToString()));
                                   //}
                            }
                     }
                     else
                     {
                            for (int i = 0; i < daysCount; i++)
                            {
                                   string WIDss = (i + 1).ToString();
                                   days.Add(new DayCell(i + 1, month, year, string.Empty, WIDss, (i + 1).ToString()));
                            }
                     }


                     daysArrays[key] = days.ToArray();//cache the array

                     return days.ToArray();
              }


              //public static DayOfWeek GetDayOfWeek(int year, int month, int day)
              //{

              //    return new DateTime(year, month, day).DayOfWeek;
              //}


              // Moves one month forward
              public static void MoveMonthForward(int currentMonth, int currentYear,
                  out int monthToGetNext, out int yearTogetNext)
              {
                     monthToGetNext = currentMonth;
                     yearTogetNext = currentYear;
                     if (monthToGetNext < 12)
                            monthToGetNext++;
                     else//move a year forward
                     {
                            yearTogetNext++;
                            monthToGetNext = 1;
                     }
              }
              /// <summary>
              /// Move one month back
              /// </summary>
              /// <param name="currentMonth">The current month</param>
              /// <param name="currentYear">The current year</param>
              /// <param name="monthToGetPrevious">The previous month</param>
              /// <param name="yearToGetPrevious">The relative year for the new month</param>
              public static void MoveMonthBack(int currentMonth, int currentYear,
                  out int monthToGetPrevious, out int yearToGetPrevious)
              {
                     monthToGetPrevious = currentMonth;
                     yearToGetPrevious = currentYear;
                     if (monthToGetPrevious > 1)
                            monthToGetPrevious--;
                     else // move one year down
                     {
                            yearToGetPrevious--;
                            monthToGetPrevious = 12;
                     }
              }


              // returns a list of months

              public static readonly string[] MonthsList = new string[]
                      {
                    "January",
                    "February",
                    "March",
                    "April",
                    "May",
                    "June",
                    "July",
                    "August",
                    "September",
                    "October",
                    "November",
                    "December"
                      };
       }


       // Enum that specifies the list of months

       public enum Months
       {
              January = 1,
              February = 2,
              March = 3,
              April = 4,
              May = 5,
              June = 6,
              July = 7,
              August = 8,
              September = 9,
              October = 10,
              November = 11,
              December = 12
       }
}
