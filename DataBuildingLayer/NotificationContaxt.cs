using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data;


namespace DataBuildingLayer
{
    public class NotificationContaxt
    {
        private readonly ShipmentContaxt sc;
        public NotificationContaxt()
        {
            sc = new ShipmentContaxt();
        }
        public List<WorkHoursNC> GetDeviationList()
        {
            try
            {
                using (ShipmentContaxt sc1 = new ShipmentContaxt())
                {
                    List<WorkHoursNC> list = new List<WorkHoursNC>();


                    if (UserTypeClass.UserTypes == "admin" || string.IsNullOrEmpty(UserTypeClass.UserTypes))
                    {

                        var data = sc1.WorkHourss.Where(x => !string.IsNullOrEmpty(x.NonConfirmities)).Select(a => new { a.UserName, a.wid, a.WRID, a.FullName, a.dates, a.Position, a.Department, a.NonConfirmities, a.NC1, a.NC2, a.NC3, a.NC4, a.NC5, a.NC6, a.NC7, a.NC8, a.NC9, a.NC10, a.NC11, a.NC12, a.NC13, a.NC14, a.NC15, a.NC16, a.NC17, a.NC18, a.NCD1, a.NCD2, a.NCD3, a.NCD4, a.NCD5, a.NCD6, a.NCD7, a.NCD8, a.NCD9, a.NCD10, a.NCD11, a.NCD12, a.NCD13, a.NCD14, a.NCD15, a.NCD16, a.NCD17, a.NCD18, a.YNC1, a.YNCD1, a.YNC2, a.YNCD2, a.YNC3, a.YNCD3, a.YNC4, a.YNCD4, a.YNC5, a.YNCD5, a.YNC6, a.YNCD6, a.YNC7, a.YNCD7, a.YNC8, a.YNCD8 }).ToList();

                        foreach (var item in data)
                        {
                            if (!string.IsNullOrEmpty(item.NC1))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC1, NCType = "NC1", NCTypeStatus = item.NCD1, Acknowledge = item.NCD1 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD1).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC2))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC2, NCType = "NC2", NCTypeStatus = item.NCD2, Acknowledge = item.NCD2 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD2).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC3))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC3, NCType = "NC3", NCTypeStatus = item.NCD3, Acknowledge = item.NCD3 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD3).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC4))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC4, NCType = "NC4", NCTypeStatus = item.NCD4, Acknowledge = item.NCD4 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD4).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC5))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC5, NCType = "NC5", NCTypeStatus = item.NCD5, Acknowledge = item.NCD5 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD5).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC6))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC6, NCType = "NC6", NCTypeStatus = item.NCD6, Acknowledge = item.NCD6 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD6).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC7))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC7, NCType = "NC7", NCTypeStatus = item.NCD7, Acknowledge = item.NCD7 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD7).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC8))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC8, NCType = "NC8", NCTypeStatus = item.NCD8, Acknowledge = item.NCD8 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD8).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC9))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC9, NCType = "NC9", NCTypeStatus = item.NCD9, Acknowledge = item.NCD9 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD9).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC10))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC10, NCType = "NC10", NCTypeStatus = item.NCD10, Acknowledge = item.NCD10 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD10).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC11))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC11, NCType = "NC11", NCTypeStatus = item.NCD11, Acknowledge = item.NCD11 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD11).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC12))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC12, NCType = "NC12", NCTypeStatus = item.NCD12, Acknowledge = item.NCD12 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD12).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC13))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC13, NCType = "NC13", NCTypeStatus = item.NCD13, Acknowledge = item.NCD13 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD13).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC14))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC14, NCType = "NC14", NCTypeStatus = item.NCD14, Acknowledge = item.NCD14 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD14).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC15))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC15, NCType = "NC15", NCTypeStatus = item.NCD15, Acknowledge = item.NCD15 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD15).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC16))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC16, NCType = "NC16", NCTypeStatus = item.NCD16, Acknowledge = item.NCD16 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD16).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC17))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC17, NCType = "NC17", NCTypeStatus = item.NCD17, Acknowledge = item.NCD17 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD17).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC18))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC18, NCType = "NC18", NCTypeStatus = item.NCD18, Acknowledge = item.NCD18 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD18).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false });


                            if (!string.IsNullOrEmpty(item.YNC1))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC1, NCType = "YNC1", NCTypeStatus = item.YNCD1, Acknowledge = item.YNCD1 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD1).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC2))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC2, NCType = "YNC2", NCTypeStatus = item.YNCD2, Acknowledge = item.YNCD2 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD2).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC3))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC3, NCType = "YNC3", NCTypeStatus = item.YNCD3, Acknowledge = item.YNCD3 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD3).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC4))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC4, NCType = "YNC4", NCTypeStatus = item.YNCD4, Acknowledge = item.YNCD4 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD4).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC5))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC5, NCType = "YNC5", NCTypeStatus = item.YNCD5, Acknowledge = item.YNCD5 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD5).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC6))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC6, NCType = "YNC6", NCTypeStatus = item.YNCD6, Acknowledge = item.YNCD6 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD6).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC7))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC7, NCType = "YNC7", NCTypeStatus = item.YNCD7, Acknowledge = item.YNCD7 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD7).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC8))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC8, NCType = "YNC8", NCTypeStatus = item.YNCD8, Acknowledge = item.YNCD8 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD8).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "Admin", IsCheckedV = false, });


                        }

                    }
                    else if (UserTypeClass.UserTypes.ToLower() == "master")
                    {

                        var data = sc1.WorkHourss.Select(a => new { a.UserName, a.wid, a.WRID, a.FullName, a.dates, a.Position, a.Department, a.NonConfirmities, a.NC1, a.NC2, a.NC3, a.NC4, a.NC5, a.NC6, a.NC7, a.NC8, a.NC9, a.NC10, a.NC11, a.NC12, a.NC13, a.NC14, a.NC15, a.NC16, a.NC17, a.NC18, a.MNCCD1, a.MNCCD2, a.MNCCD3, a.MNCCD4, a.MNCCD5, a.MNCCD6, a.MNCCD7, a.MNCCD8, a.MNCCD9, a.MNCCD10, a.MNCCD11, a.MNCCD12, a.MNCCD13, a.MNCCD14, a.MNCCD15, a.MNCCD16, a.MNCCD17, a.MNCCD18, a.YNC1, a.MYNCCD1, a.YNC2, a.MYNCCD2, a.YNC3, a.MYNCCD3, a.YNC4, a.MYNCCD4, a.YNC5, a.MYNCCD5, a.YNC6, a.MYNCCD6, a.YNC7, a.MYNCCD7, a.YNC8, a.MYNCCD8 }).Where(x => !string.IsNullOrEmpty(x.NonConfirmities)).ToList();
                        foreach (var item in data)
                        {
                            if (!string.IsNullOrEmpty(item.NC1))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC1, NCType = "NC1", NCTypeStatus = item.MNCCD1, Acknowledge = item.MNCCD1 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD1).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC2))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC2, NCType = "NC2", NCTypeStatus = item.MNCCD2, Acknowledge = item.MNCCD2 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD2).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC3))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC3, NCType = "NC3", NCTypeStatus = item.MNCCD3, Acknowledge = item.MNCCD3 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD3).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC4))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC4, NCType = "NC4", NCTypeStatus = item.MNCCD4, Acknowledge = item.MNCCD4 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD4).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC5))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC5, NCType = "NC5", NCTypeStatus = item.MNCCD5, Acknowledge = item.MNCCD5 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD5).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC6))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC6, NCType = "NC6", NCTypeStatus = item.MNCCD6, Acknowledge = item.MNCCD6 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD6).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC7))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC7, NCType = "NC7", NCTypeStatus = item.MNCCD7, Acknowledge = item.MNCCD7 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD7).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC8))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC8, NCType = "NC8", NCTypeStatus = item.MNCCD8, Acknowledge = item.MNCCD8 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD8).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC9))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC9, NCType = "NC9", NCTypeStatus = item.MNCCD9, Acknowledge = item.MNCCD9 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD9).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC10))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC10, NCType = "NC10", NCTypeStatus = item.MNCCD10, Acknowledge = item.MNCCD10 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD10).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC11))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC11, NCType = "NC11", NCTypeStatus = item.MNCCD11, Acknowledge = item.MNCCD11 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD11).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC12))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC12, NCType = "NC12", NCTypeStatus = item.MNCCD12, Acknowledge = item.MNCCD12 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD12).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC13))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC13, NCType = "NC13", NCTypeStatus = item.MNCCD13, Acknowledge = item.MNCCD13 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD13).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC14))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC14, NCType = "NC14", NCTypeStatus = item.MNCCD14, Acknowledge = item.MNCCD14 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD14).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC15))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC15, NCType = "NC15", NCTypeStatus = item.MNCCD15, Acknowledge = item.MNCCD15 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD15).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC16))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC16, NCType = "NC16", NCTypeStatus = item.MNCCD16, Acknowledge = item.MNCCD16 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD16).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC17))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC17, NCType = "NC17", NCTypeStatus = item.MNCCD17, Acknowledge = item.MNCCD17 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD17).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC18))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC18, NCType = "NC18", NCTypeStatus = item.MNCCD18, Acknowledge = item.MNCCD18 != null ? "Acknowledged on " + Convert.ToDateTime(item.MNCCD18).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false });


                            if (!string.IsNullOrEmpty(item.YNC1))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC1, NCType = "YNC1", NCTypeStatus = item.MYNCCD1, Acknowledge = item.MYNCCD1 != null ? "Acknowledged on " + Convert.ToDateTime(item.MYNCCD1).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC2))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC2, NCType = "YNC2", NCTypeStatus = item.MYNCCD2, Acknowledge = item.MYNCCD2 != null ? "Acknowledged on " + Convert.ToDateTime(item.MYNCCD2).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC3))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC3, NCType = "YNC3", NCTypeStatus = item.MYNCCD3, Acknowledge = item.MYNCCD3 != null ? "Acknowledged on " + Convert.ToDateTime(item.MYNCCD3).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC4))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC4, NCType = "YNC4", NCTypeStatus = item.MYNCCD4, Acknowledge = item.MYNCCD4 != null ? "Acknowledged on " + Convert.ToDateTime(item.MYNCCD4).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC5))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC5, NCType = "YNC5", NCTypeStatus = item.MYNCCD5, Acknowledge = item.MYNCCD5 != null ? "Acknowledged on " + Convert.ToDateTime(item.MYNCCD5).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC6))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC6, NCType = "YNC6", NCTypeStatus = item.MYNCCD6, Acknowledge = item.MYNCCD6 != null ? "Acknowledged on " + Convert.ToDateTime(item.MYNCCD6).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC7))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC7, NCType = "YNC7", NCTypeStatus = item.MYNCCD7, Acknowledge = item.MYNCCD7 != null ? "Acknowledged on " + Convert.ToDateTime(item.MYNCCD7).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC8))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC8, NCType = "YNC8", NCTypeStatus = item.MYNCCD8, Acknowledge = item.MYNCCD8 != null ? "Acknowledged on " + Convert.ToDateTime(item.MYNCCD8).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "MASTER", IsCheckedV = false, });

                        }

                    }
                    else if (UserTypeClass.UserTypes == "HOD")
                    {

                        var data = sc1.WorkHourss.Select(a => new { a.UserName, a.wid, a.WRID, a.FullName, a.dates, a.Position, a.Department, a.NonConfirmities, a.NC1, a.NC2, a.NC3, a.NC4, a.NC5, a.NC6, a.NC7, a.NC8, a.NC9, a.NC10, a.NC11, a.NC12, a.NC13, a.NC14, a.NC15, a.NC16, a.NC17, a.NC18, a.HNCCD1, a.HNCCD2, a.HNCCD3, a.HNCCD4, a.HNCCD5, a.HNCCD6, a.HNCCD7, a.HNCCD8, a.HNCCD9, a.HNCCD10, a.HNCCD11, a.HNCCD12, a.HNCCD13, a.HNCCD14, a.HNCCD15, a.HNCCD16, a.HNCCD17, a.HNCCD18, a.YNC1, a.HYNCCD1, a.YNC2, a.HYNCCD2, a.YNC3, a.HYNCCD3, a.YNC4, a.HYNCCD4, a.YNC5, a.HYNCCD5, a.YNC6, a.HYNCCD6, a.YNC7, a.HYNCCD7, a.YNC8, a.HYNCCD8 }).Where(x => !string.IsNullOrEmpty(x.NonConfirmities)).ToList();
                        foreach (var item in data)
                        {
                            if (!string.IsNullOrEmpty(item.NC1))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC1, NCType = "NC1", NCTypeStatus = item.HNCCD1, Acknowledge = item.HNCCD1 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD1).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC2))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC2, NCType = "NC2", NCTypeStatus = item.HNCCD2, Acknowledge = item.HNCCD2 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD2).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC3))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC3, NCType = "NC3", NCTypeStatus = item.HNCCD3, Acknowledge = item.HNCCD3 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD3).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC4))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC4, NCType = "NC4", NCTypeStatus = item.HNCCD4, Acknowledge = item.HNCCD4 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD4).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC5))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC5, NCType = "NC5", NCTypeStatus = item.HNCCD5, Acknowledge = item.HNCCD5 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD5).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC6))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC6, NCType = "NC6", NCTypeStatus = item.HNCCD6, Acknowledge = item.HNCCD6 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD6).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC7))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC7, NCType = "NC7", NCTypeStatus = item.HNCCD7, Acknowledge = item.HNCCD7 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD7).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC8))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC8, NCType = "NC8", NCTypeStatus = item.HNCCD8, Acknowledge = item.HNCCD8 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD8).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC9))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC9, NCType = "NC9", NCTypeStatus = item.HNCCD9, Acknowledge = item.HNCCD9 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD9).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC10))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC10, NCType = "NC10", NCTypeStatus = item.HNCCD10, Acknowledge = item.HNCCD10 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD10).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC11))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC11, NCType = "NC11", NCTypeStatus = item.HNCCD11, Acknowledge = item.HNCCD11 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD11).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC12))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC12, NCType = "NC12", NCTypeStatus = item.HNCCD12, Acknowledge = item.HNCCD12 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD12).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC13))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC13, NCType = "NC13", NCTypeStatus = item.HNCCD13, Acknowledge = item.HNCCD13 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD13).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC14))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC14, NCType = "NC14", NCTypeStatus = item.HNCCD14, Acknowledge = item.HNCCD14 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD14).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC15))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC15, NCType = "NC15", NCTypeStatus = item.HNCCD15, Acknowledge = item.HNCCD15 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD15).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC16))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC16, NCType = "NC16", NCTypeStatus = item.HNCCD16, Acknowledge = item.HNCCD16 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD16).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC17))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC17, NCType = "NC17", NCTypeStatus = item.HNCCD17, Acknowledge = item.HNCCD17 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD17).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });
                            if (!string.IsNullOrEmpty(item.NC18))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.NC18, NCType = "NC18", NCTypeStatus = item.HNCCD18, Acknowledge = item.HNCCD18 != null ? "Acknowledged on " + Convert.ToDateTime(item.HNCCD18).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false });


                            if (!string.IsNullOrEmpty(item.YNC1))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC1, NCType = "YNC1", NCTypeStatus = item.HYNCCD1, Acknowledge = item.HYNCCD1 != null ? "Acknowledged on " + Convert.ToDateTime(item.HYNCCD1).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC2))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC2, NCType = "YNC2", NCTypeStatus = item.HYNCCD2, Acknowledge = item.HYNCCD2 != null ? "Acknowledged on " + Convert.ToDateTime(item.HYNCCD2).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC3))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC3, NCType = "YNC3", NCTypeStatus = item.HYNCCD3, Acknowledge = item.HYNCCD3 != null ? "Acknowledged on " + Convert.ToDateTime(item.HYNCCD3).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC4))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC4, NCType = "YNC4", NCTypeStatus = item.HYNCCD4, Acknowledge = item.HYNCCD4 != null ? "Acknowledged on " + Convert.ToDateTime(item.HYNCCD4).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC5))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC5, NCType = "YNC5", NCTypeStatus = item.HYNCCD5, Acknowledge = item.HYNCCD5 != null ? "Acknowledged on " + Convert.ToDateTime(item.HYNCCD5).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC6))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC6, NCType = "YNC6", NCTypeStatus = item.HYNCCD6, Acknowledge = item.HYNCCD6 != null ? "Acknowledged on " + Convert.ToDateTime(item.HYNCCD6).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC7))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC7, NCType = "YNC7", NCTypeStatus = item.HYNCCD7, Acknowledge = item.HYNCCD7 != null ? "Acknowledged on " + Convert.ToDateTime(item.HYNCCD7).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false, });
                            if (!string.IsNullOrEmpty(item.YNC8))
                                list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, position = item.Position, Department = item.Department, NonConfirmity = item.YNC8, NCType = "YNC8", NCTypeStatus = item.HYNCCD8, Acknowledge = item.HYNCCD8 != null ? "Acknowledged on " + Convert.ToDateTime(item.HYNCCD8).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", UserType = "HOD", IsCheckedV = false, });

                        }

                    }


                    return list;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                return null;
            }
        }


        public void AcknowledgeDeviation(List<WorkHoursNC> list)
        {
            try
            {
                var dt = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm "));
                var data = new List<WorkHoursClass>();
                using (ShipmentContaxt sc1 = new ShipmentContaxt())
                {
                    data = sc1.WorkHourss.ToList();


                    if (UserTypeClass.UserTypes == "admin")
                    {

                        foreach (var item in list)
                        {
                            if (item.NCType.ToString() == "NC1")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD1 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC2")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD2 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC3")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD3 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC4")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD4 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC5")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD5 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC6")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD6 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC7")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD7 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC8")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD8 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC9")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD9 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC10")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD10 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC11")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD11 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC12")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD12 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC13")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD13 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC14")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD14 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC15")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD15 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC16")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD16 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC17")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD17 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC18")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.NCD18 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC1")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.YNCD1 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC2")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.YNCD2 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC3")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.YNCD3 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC4")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.YNCD4 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC5")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.YNCD5 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC6")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.YNCD6 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC7")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.YNCD7 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC8")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.YNCD8 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }

                        }
                    }
                    else if (UserTypeClass.UserTypes == "MASTER")
                    {
                        foreach (var item in list)
                        {
                            if (item.NCType.ToString() == "NC1")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD1 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC2")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD2 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC3")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD3 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC4")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD4 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC5")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD5 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC6")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD6 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC7")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD7 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC8")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD8 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC9")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD9 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC10")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD10 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC11")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD11 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC12")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD12 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC13")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD13 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC14")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD14 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC15")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD15 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC16")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD16 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC17")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD17 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC18")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MNCCD18 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC1")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MYNCCD1 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC2")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MYNCCD2 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC3")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MYNCCD3 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC4")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MYNCCD4 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC5")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MYNCCD5 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC6")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MYNCCD6 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC7")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MYNCCD7 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC8")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.MYNCCD8 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }

                        }
                    }
                    else if (UserTypeClass.UserTypes == "HOD")
                    {
                        foreach (var item in list)
                        {
                            if (item.NCType.ToString() == "NC1")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD1 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC2")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD2 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC3")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD3 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC4")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD4 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC5")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD5 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC6")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD6 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC7")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD7 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC8")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD8 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC9")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD9 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC10")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD10 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC11")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD11 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC12")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD12 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC13")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD13 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC14")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD14 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC15")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD15 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC16")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD16 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC17")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD17 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "NC18")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HNCCD18 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC1")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HYNCCD1 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC2")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HYNCCD2 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC3")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HYNCCD3 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC4")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HYNCCD4 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC5")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HYNCCD5 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC6")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HYNCCD6 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC7")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HYNCCD7 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }
                            if (item.NCType.ToString() == "YNC8")
                            {
                                var ssd = data.Where(x => x.wid == item.wid && x.UserName == item.UserName && x.WRID == item.WRID).FirstOrDefault();
                                if (ssd != null)
                                {
                                    ssd.HYNCCD8 = dt;
                                    sc1.WorkHourss.Attach(ssd);
                                    sc1.Entry(ssd).State = EntityState.Modified;
                                }
                            }

                        }
                    }


                    sc1.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }


        }
        public List<CertificateNC> GetCertificationList()
        {
            try
            {
                DateTime dt = DateTime.Now.Date;
                var data = new List<CertificatesClass>();
                using (ShipmentContaxt sc1 = new ShipmentContaxt())
                {
                    data = sc1.Certificates.ToList();
                }
                List<CertificateNC> list = new List<CertificateNC>();

                if (UserTypeClass.UserTypes == "admin" || string.IsNullOrEmpty(UserTypeClass.UserTypes))
                {

                    foreach (var item in data)
                    {

                        var doe = item.DOE;
                        var dos = item.DOS;
                        if (item.DOE <= dt)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.OverDOE, NCType = "OverDOE", Acknowledge = item.AckDOEOver, UserType = "Admin", IsCheckedV = false });
                        if (item.DOS <= dt)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.OverDOS, NCType = "OverDOS", Acknowledge = item.AckDOSOver, UserType = "Admin", IsCheckedV = false });

                        if (doe.AddMonths(-6) <= dt && item.month6)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE6m, NCType = "NCDOE6m", Acknowledge = item.AckDOE6m, UserType = "Admin", IsCheckedV = false });
                        if (doe.AddMonths(-3) <= dt && item.month3)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE3m, NCType = "NCDOE3m", Acknowledge = item.AckDOE3m, UserType = "Admin", IsCheckedV = false });
                        if (doe.AddMonths(-1) <= dt && item.month1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE1m, NCType = "NCDOE1m", Acknowledge = item.AckDOE1m, UserType = "Admin", IsCheckedV = false });
                        if (doe.AddDays(-15) <= dt && item.Days15)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE15d, NCType = "NCDOE15d", Acknowledge = item.AckDOE15d, UserType = "Admin", IsCheckedV = false });
                        if (doe.AddDays(-7) <= dt && item.Days7)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE7d, NCType = "NCDOE7d", Acknowledge = item.AckDOE7d, UserType = "Admin", IsCheckedV = false });
                        if (doe.AddDays(-1) <= dt && item.Days1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE1d, NCType = "NCDOE1d", Acknowledge = item.AckDOE1d, UserType = "Admin", IsCheckedV = false });


                        if (dos.AddMonths(-6) <= dt && item.DOSmonth6)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS6m, NCType = "NCDOS6m", Acknowledge = item.AckDOS6m, UserType = "Admin", IsCheckedV = false });
                        if (dos.AddMonths(-3) <= dt && item.DOSmonth3)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS3m, NCType = "NCDOS3m", Acknowledge = item.AckDOS3m, UserType = "Admin", IsCheckedV = false });
                        if (dos.AddMonths(-1) <= dt && item.DOSmonth1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS1m, NCType = "NCDOS1m", Acknowledge = item.AckDOS1m, UserType = "Admin", IsCheckedV = false });
                        if (dos.AddDays(-15) <= dt && item.DOSDays15)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS15d, NCType = "NCDOS15d", Acknowledge = item.AckDOS15d, UserType = "Admin", IsCheckedV = false });
                        if (dos.AddDays(-7) <= dt && item.DOSDays7)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS7d, NCType = "NCDOS7d", Acknowledge = item.AckDOS7d, UserType = "Admin", IsCheckedV = false });
                        if (dos.AddDays(-1) <= dt && item.DOSDays1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS1d, NCType = "NCDOS1d", Acknowledge = item.AckDOS1d, UserType = "Admin", IsCheckedV = false });

                    }
                }
                else if (UserTypeClass.UserTypes == "MASTER")
                {
                    foreach (var item in data)
                    {

                        var doe = item.DOE;
                        var dos = item.DOS;
                        if (item.DOE <= dt)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.OverDOE, NCType = "OverDOE", Acknowledge = item.AckDOEOver_MASTER, UserType = "MASTER", IsCheckedV = false });
                        if (item.DOS <= dt)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.OverDOS, NCType = "OverDOS", Acknowledge = item.AckDOSOver_MASTER, UserType = "MASTER", IsCheckedV = false });

                        if (doe.AddMonths(-6) <= dt && item.month6)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE6m, NCType = "NCDOE6m", Acknowledge = item.AckDOE6m_MASTER, UserType = "MASTER", IsCheckedV = false });
                        if (doe.AddMonths(-3) <= dt && item.month3)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE3m, NCType = "NCDOE3m", Acknowledge = item.AckDOE3m_MASTER, UserType = "MASTER", IsCheckedV = false });
                        if (doe.AddMonths(-1) <= dt && item.month1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE1m, NCType = "NCDOE1m", Acknowledge = item.AckDOE1m_MASTER, UserType = "MASTER", IsCheckedV = false });
                        if (doe.AddDays(-15) <= dt && item.Days15)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE15d, NCType = "NCDOE15d", Acknowledge = item.AckDOE15d_MASTER, UserType = "MASTER", IsCheckedV = false });
                        if (doe.AddDays(-7) <= dt && item.Days7)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE7d, NCType = "NCDOE7d", Acknowledge = item.AckDOE7d_MASTER, UserType = "MASTER", IsCheckedV = false });
                        if (doe.AddDays(-1) <= dt && item.Days1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE1d, NCType = "NCDOE1d", Acknowledge = item.AckDOE1d_MASTER, UserType = "MASTER", IsCheckedV = false });


                        if (dos.AddMonths(-6) <= dt && item.DOSmonth6)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS6m, NCType = "NCDOS6m", Acknowledge = item.AckDOS6m_MASTER, UserType = "MASTER", IsCheckedV = false });
                        if (dos.AddMonths(-3) <= dt && item.DOSmonth3)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS3m, NCType = "NCDOS3m", Acknowledge = item.AckDOS3m_MASTER, UserType = "MASTER", IsCheckedV = false });
                        if (dos.AddMonths(-1) <= dt && item.DOSmonth1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS1m, NCType = "NCDOS1m", Acknowledge = item.AckDOS1m_MASTER, UserType = "MASTER", IsCheckedV = false });
                        if (dos.AddDays(-15) <= dt && item.DOSDays15)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS15d, NCType = "NCDOS15d", Acknowledge = item.AckDOS15d_MASTER, UserType = "MASTER", IsCheckedV = false });
                        if (dos.AddDays(-7) <= dt && item.DOSDays7)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS7d, NCType = "NCDOS7d", Acknowledge = item.AckDOS7d_MASTER, UserType = "MASTER", IsCheckedV = false });
                        if (dos.AddDays(-1) <= dt && item.DOSDays1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS1d, NCType = "NCDOS1d", Acknowledge = item.AckDOS1d_MASTER, UserType = "MASTER", IsCheckedV = false });

                    }
                }
                else if (UserTypeClass.UserTypes == "HOD")
                {
                    foreach (var item in data)
                    {

                        var doe = item.DOE;
                        var dos = item.DOS;
                        if (item.DOE <= dt)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.OverDOE, NCType = "OverDOE", Acknowledge = item.AckDOEOver_HOD, UserType = "HOD", IsCheckedV = false });
                        if (item.DOS <= dt)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.OverDOS, NCType = "OverDOS", Acknowledge = item.AckDOSOver_HOD, UserType = "HOD", IsCheckedV = false });

                        if (doe.AddMonths(-6) <= dt && item.month6)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE6m, NCType = "NCDOE6m", Acknowledge = item.AckDOE6m_HOD, UserType = "HOD", IsCheckedV = false });
                        if (doe.AddMonths(-3) <= dt && item.month3)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE3m, NCType = "NCDOE3m", Acknowledge = item.AckDOE3m_HOD, UserType = "HOD", IsCheckedV = false });
                        if (doe.AddMonths(-1) <= dt && item.month1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE1m, NCType = "NCDOE1m", Acknowledge = item.AckDOE1m_HOD, UserType = "HOD", IsCheckedV = false });
                        if (doe.AddDays(-15) <= dt && item.Days15)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE15d, NCType = "NCDOE15d", Acknowledge = item.AckDOE15d_HOD, UserType = "HOD", IsCheckedV = false });
                        if (doe.AddDays(-7) <= dt && item.Days7)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE7d, NCType = "NCDOE7d", Acknowledge = item.AckDOE7d_HOD, UserType = "HOD", IsCheckedV = false });
                        if (doe.AddDays(-1) <= dt && item.Days1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE1d, NCType = "NCDOE1d", Acknowledge = item.AckDOE1d_HOD, UserType = "HOD", IsCheckedV = false });


                        if (dos.AddMonths(-6) <= dt && item.DOSmonth6)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS6m, NCType = "NCDOS6m", Acknowledge = item.AckDOS6m_HOD, UserType = "HOD", IsCheckedV = false });
                        if (dos.AddMonths(-3) <= dt && item.DOSmonth3)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS3m, NCType = "NCDOS3m", Acknowledge = item.AckDOS3m_HOD, UserType = "HOD", IsCheckedV = false });
                        if (dos.AddMonths(-1) <= dt && item.DOSmonth1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS1m, NCType = "NCDOS1m", Acknowledge = item.AckDOS1m_HOD, UserType = "HOD", IsCheckedV = false });
                        if (dos.AddDays(-15) <= dt && item.DOSDays15)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS15d, NCType = "NCDOS15d", Acknowledge = item.AckDOS15d_HOD, UserType = "HOD", IsCheckedV = false });
                        if (dos.AddDays(-7) <= dt && item.DOSDays7)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS7d, NCType = "NCDOS7d", Acknowledge = item.AckDOS7d_HOD, UserType = "HOD", IsCheckedV = false });
                        if (dos.AddDays(-1) <= dt && item.DOSDays1)
                            list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS1d, NCType = "NCDOS1d", Acknowledge = item.AckDOS1d_HOD, UserType = "HOD", IsCheckedV = false });

                    }
                }

                return list;

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                return null;
            }
        }

        public void AcknowledgeCertificate(List<CertificateNC> list)
        {
            try
            {

                var dt = DateTime.Now.ToString("dd-MMM-yyyy hh:mm ");

                var data = new List<CertificatesClass>();
                using (ShipmentContaxt sc1 = new ShipmentContaxt())
                {
                    data = sc1.Certificates.ToList();

                    if (UserTypeClass.UserTypes == "admin")
                    {
                        foreach (var item in list)
                        {
                            if (item.NCType.ToString() == "OverDOE")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOEOver = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "OverDOS")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOSOver = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }


                            if (item.NCType.ToString() == "NCDOE6m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE6m = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE3m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE3m = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE1m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE1m = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE15d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE15d = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE7d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE7d = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE1d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE1d = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }



                            if (item.NCType.ToString() == "NCDOS6m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS6m = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS3m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS3m = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS1m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS1m = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS15d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS15d = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS7d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS7d = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS1d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS1d = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }

                        }
                    }
                    else if (UserTypeClass.UserTypes == "MASTER")
                    {
                        foreach (var item in list)
                        {
                            if (item.NCType.ToString() == "OverDOE")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOEOver_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "OverDOS")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOSOver_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }


                            if (item.NCType.ToString() == "NCDOE6m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE6m_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE3m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE3m_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE1m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE1m_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE15d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE15d_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE7d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE7d_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE1d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE1d_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }



                            if (item.NCType.ToString() == "NCDOS6m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS6m_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS3m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS3m_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS1m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS1m_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS15d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS15d_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS7d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS7d_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS1d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS1d_MASTER = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }

                        }
                    }
                    else if (UserTypeClass.UserTypes == "HOD")
                    {
                        foreach (var item in list)
                        {
                            if (item.NCType.ToString() == "OverDOE")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOEOver_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "OverDOS")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOSOver_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }


                            if (item.NCType.ToString() == "NCDOE6m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE6m_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE3m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE3m_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE1m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE1m_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE15d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE15d_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE7d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE7d_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOE1d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOE1d_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }



                            if (item.NCType.ToString() == "NCDOS6m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS6m_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS3m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS3m_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS1m")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS1m_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS15d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS15d_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS7d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS7d_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }
                            if (item.NCType.ToString() == "NCDOS1d")
                            {
                                var ssd = data.Where(x => x.Id == item.Id).FirstOrDefault();
                                ssd.AckDOS1d_HOD = "Acknowledged on " + dt + "Hours";

                                sc1.Certificates.Attach(ssd);
                                sc1.Entry(ssd).State = EntityState.Modified;
                            }

                        }
                    }


                    sc1.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }




    }
}
