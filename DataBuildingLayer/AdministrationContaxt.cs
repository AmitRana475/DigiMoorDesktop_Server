using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataBuildingLayer
{
    public class AdministrationContaxt : ShipmentContaxt
    {

        //public DbSet<UserAccessClass> UserAccessHOD { get; set; }
        //public DbSet<CommentofVSClass> CommentofVs { get; set; }
        //public DbSet<CertificatesClass> Certificates { get; set; }
        //public DbSet<CrewDetailClass> CrewDetails { get; set; }
        //public DbSet<WorkHoursClass> sc.WorkHourss { get; set; }
        //public DbSet<RuffCodeClass> RuffCodes { get; set; }
        //public DbSet<versionClass> Versions { get; set; }
        //public DbSet<VesselClass> Vessels { get; set; }



        public List<RuffCodeClass> GetLicencekeys()
        {
            try
            {
                List<RuffCodeClass> objHashTable = new List<RuffCodeClass>();
                var data = RuffCodes.ToList();

                foreach (var item in data)
                {

                    objHashTable.Add(new RuffCodeClass { keyno = Decrypt(item.keyno.ToString(), "KKPrajapat"), keycode = Decrypt(item.keycode.ToString(), "KKPrajapat") });

                }


                return objHashTable;
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }


        public List<WorkHoursNC> GetDeviationList(DateTime datefrom, DateTime dateto)
        {
            try
            {
                var dd = Vessels.FirstOrDefault();
                int vessel_id;

                if (dd != null)
                    vessel_id = dd.vessel_ID;
                else
                    vessel_id = 1234567;

                var data = WorkHourss.Select(a => new { a.UserName, a.wid, a.WRID, a.FullName, a.dates, a.NonConfirmities, a.NC1, a.NC2, a.NC3, a.NC4, a.NC5, a.NC6, a.NC7, a.NC8, a.NC9, a.NC10, a.NC11, a.NC12, a.NC13, a.NC14, a.NC15, a.NC16, a.NC17, a.NC18, a.NCD1, a.NCD2, a.NCD3, a.NCD4, a.NCD5, a.NCD6, a.NCD7, a.NCD8, a.NCD9, a.NCD10, a.NCD11, a.NCD12, a.NCD13, a.NCD14, a.NCD15, a.NCD16, a.NCD17, a.NCD18, a.YNC1, a.YNCD1, a.YNC2, a.YNCD2, a.YNC3, a.YNCD3, a.YNC4, a.YNCD4, a.YNC5, a.YNCD5, a.YNC6, a.YNCD6, a.YNC7, a.YNCD7, a.YNC8, a.YNCD8, a.Position, a.AdminAlarm, a.MasterAlarm, a.HODAlarm }).Where(x => !string.IsNullOrEmpty(x.NonConfirmities) && (x.dates >= datefrom.Date && x.dates <= dateto.Date)).ToList();
                List<WorkHoursNC> list = new List<WorkHoursNC>();
                foreach (var item in data)
                {
                    if (!string.IsNullOrEmpty(item.NC1))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC1, NCType = "NCD1", NCTypeStatus = item.NCD1, Acknowledge = item.NCD1 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD1).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC2))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC2, NCType = "NCD2", NCTypeStatus = item.NCD2, Acknowledge = item.NCD2 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD2).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC3))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC3, NCType = "NCD3", NCTypeStatus = item.NCD3, Acknowledge = item.NCD3 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD3).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC4))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC4, NCType = "NCD4", NCTypeStatus = item.NCD4, Acknowledge = item.NCD4 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD4).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC5))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC5, NCType = "NCD5", NCTypeStatus = item.NCD5, Acknowledge = item.NCD5 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD5).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC6))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC6, NCType = "NCD6", NCTypeStatus = item.NCD6, Acknowledge = item.NCD6 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD6).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC7))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC7, NCType = "NCD7", NCTypeStatus = item.NCD7, Acknowledge = item.NCD7 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD7).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC8))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC8, NCType = "NCD8", NCTypeStatus = item.NCD8, Acknowledge = item.NCD8 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD8).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC9))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC9, NCType = "NCD9", NCTypeStatus = item.NCD9, Acknowledge = item.NCD9 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD9).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC10))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC10, NCType = "NCD10", NCTypeStatus = item.NCD10, Acknowledge = item.NCD10 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD10).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC11))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC11, NCType = "NCD11", NCTypeStatus = item.NCD11, Acknowledge = item.NCD11 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD11).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC12))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC12, NCType = "NCD12", NCTypeStatus = item.NCD12, Acknowledge = item.NCD12 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD12).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC13))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC13, NCType = "NCD13", NCTypeStatus = item.NCD13, Acknowledge = item.NCD13 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD13).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC14))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC14, NCType = "NCD14", NCTypeStatus = item.NCD14, Acknowledge = item.NCD14 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD14).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC15))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC15, NCType = "NCD15", NCTypeStatus = item.NCD15, Acknowledge = item.NCD15 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD15).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC16))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC16, NCType = "NCD16", NCTypeStatus = item.NCD16, Acknowledge = item.NCD16 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD16).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC17))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC17, NCType = "NCD17", NCTypeStatus = item.NCD17, Acknowledge = item.NCD17 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD17).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });
                    if (!string.IsNullOrEmpty(item.NC18))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.NC18, NCType = "NCD18", NCTypeStatus = item.NCD18, Acknowledge = item.NCD18 != null ? "Acknowledged on " + Convert.ToDateTime(item.NCD18).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false });


                    if (!string.IsNullOrEmpty(item.YNC1))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.YNC1, NCType = "YNCD1", NCTypeStatus = item.YNCD1, Acknowledge = item.YNCD1 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD1).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false, });
                    if (!string.IsNullOrEmpty(item.YNC2))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.YNC2, NCType = "YNCD2", NCTypeStatus = item.YNCD2, Acknowledge = item.YNCD2 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD2).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false, });
                    if (!string.IsNullOrEmpty(item.YNC3))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.YNC3, NCType = "YNCD3", NCTypeStatus = item.YNCD3, Acknowledge = item.YNCD3 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD3).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false, });
                    if (!string.IsNullOrEmpty(item.YNC4))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.YNC4, NCType = "YNCD4", NCTypeStatus = item.YNCD4, Acknowledge = item.YNCD4 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD4).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false, });
                    if (!string.IsNullOrEmpty(item.YNC5))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.YNC5, NCType = "YNCD5", NCTypeStatus = item.YNCD5, Acknowledge = item.YNCD5 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD5).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false, });
                    if (!string.IsNullOrEmpty(item.YNC6))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.YNC6, NCType = "YNCD6", NCTypeStatus = item.YNCD6, Acknowledge = item.YNCD6 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD6).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false, });
                    if (!string.IsNullOrEmpty(item.YNC7))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.YNC7, NCType = "YNCD7", NCTypeStatus = item.YNCD7, Acknowledge = item.YNCD7 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD7).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false, });
                    if (!string.IsNullOrEmpty(item.YNC8))
                        list.Add(new WorkHoursNC() { wid = item.wid, WRID = item.WRID, UserName = item.UserName, FullName = item.FullName, Dates = item.dates, NonConfirmity = item.YNC8, NCType = "YNCD8", NCTypeStatus = item.YNCD8, Acknowledge = item.YNCD8 != null ? "Acknowledged on " + Convert.ToDateTime(item.YNCD8).ToString("dd-MMM-yyyy hh:mm ") + "Hours" : "To be Acknowledged", AckAdmin = item.AdminAlarm == false ? "Yes" : "No", AckMaster = item.MasterAlarm == false ? "Yes" : "No", AckHOD = item.HODAlarm == false ? "Yes" : "No", position = item.Position, vessel_ID = vessel_id, UserType = "Admin", IsCheckedV = false, });


                }

                return list;
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }


        public List<CertificateNC> GetCertificationList()
        {
            try
            {

                var dd = Vessels.FirstOrDefault();
                int vessel_id;

                if (dd != null)
                    vessel_id = dd.vessel_ID;
                else
                    vessel_id = 1234567;


                DateTime dt = DateTime.Now.Date;
                //var data = Certificates.ToList();
                //var data = (from r in Certificates.Where(x=> (x.DOI >= datefrom.Date && x.DOI <= dateto.Date))

                //            select r).ToList();

                var data = (from r in Certificates

                            select r).ToList();


                List<CertificateNC> list = new List<CertificateNC>();
                foreach (var item in data)
                {

                    var doe = item.DOE;
                    var dos = item.DOS;
                    if (item.DOE <= dt)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.OverDOE, NCType = "OverDOE", Acknowledge = item.AckDOEOver, Vessel_ID =vessel_id, CName =item.CName,DOI=item.DOI,DOS=item.DOS,DOE=item.DOE, AlertFrequency=item.OverDOE, AdminAck=item.AlarmNCDOEXXX == false ? "Yes" : "No", MasterAck=item.AlarmNCDOEXXX_MASTER == false ? "Yes" : "No",HODAck=item.AlarmNCDOEXXX_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });
                    if (item.DOS <= dt)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.OverDOS, NCType = "OverDOS", Acknowledge = item.AckDOSOver, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.OverDOS, AdminAck = item.AlarmNCDOSXXX == false ? "Yes" : "No", MasterAck = item.AlarmNCDOSXXX_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOSXXX_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });

                    if (doe.AddMonths(-6) <= dt && item.month6)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE6m, NCType = "NCDOE6m", Acknowledge = item.AckDOE6m, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOE6m, AdminAck = item.AlarmNCDOE6m == false ? "Yes" : "No", MasterAck = item.AlarmNCDOE6m_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOE6m_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });
                    if (doe.AddMonths(-3) <= dt && item.month3)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE3m, NCType = "NCDOE3m", Acknowledge = item.AckDOE3m, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOE3m, AdminAck = item.AlarmNCDOE3m == false ? "Yes" : "No", MasterAck = item.AlarmNCDOE3m_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOE3m_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });
                    if (doe.AddMonths(-1) <= dt && item.month1)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE1m, NCType = "NCDOE1m", Acknowledge = item.AckDOE1m, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOE1m, AdminAck = item.AlarmNCDOE1m == false ? "Yes" : "No", MasterAck = item.AlarmNCDOE1m_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOE1m_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });
                    if (doe.AddDays(-15) <= dt && item.Days15)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE15d, NCType = "NCDOE15d", Acknowledge = item.AckDOE15d, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOE15d, AdminAck = item.AlarmNCDOE15d == false ? "Yes" : "No", MasterAck = item.AlarmNCDOE15d_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOE15d_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });
                    if (doe.AddDays(-7) <= dt && item.Days7)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE7d, NCType = "NCDOE7d", Acknowledge = item.AckDOE7d, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOE7d, AdminAck = item.AlarmNCDOE7d == false ? "Yes" : "No", MasterAck = item.AlarmNCDOE7d_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOE7d_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });
                    if (doe.AddDays(-1) <= dt && item.Days1)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOE1d, NCType = "NCDOE1d", Acknowledge = item.AckDOE1d, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOE1d, AdminAck = item.AlarmNCDOE1d == false ? "Yes" : "No", MasterAck = item.AlarmNCDOE1d_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOE1d_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });


                    if (dos.AddMonths(-6) <= dt && item.DOSmonth6)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS6m, NCType = "NCDOS6m", Acknowledge = item.AckDOS6m, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOS6m, AdminAck = item.AlarmNCDOS6m == false ? "Yes" : "No", MasterAck = item.AlarmNCDOS6m_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOS6m_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });
                    if (dos.AddMonths(-3) <= dt && item.DOSmonth3)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS3m, NCType = "NCDOS3m", Acknowledge = item.AckDOS3m, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOS3m, AdminAck = item.AlarmNCDOS3m == false ? "Yes" : "No", MasterAck = item.AlarmNCDOS3m_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOS3m_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });
                    if (dos.AddMonths(-1) <= dt && item.DOSmonth1)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS1m, NCType = "NCDOS1m", Acknowledge = item.AckDOS1m, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOS1m, AdminAck = item.AlarmNCDOS1m == false ? "Yes" : "No", MasterAck = item.AlarmNCDOS1m_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOS1m_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });
                    if (dos.AddDays(-15) <= dt && item.DOSDays15)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS15d, NCType = "NCDOS15d", Acknowledge = item.AckDOS15d, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOS15d, AdminAck = item.AlarmNCDOS15d == false ? "Yes" : "No", MasterAck = item.AlarmNCDOS15d_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOS15d_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });
                    if (dos.AddDays(-7) <= dt && item.DOSDays7)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS7d, NCType = "NCDOS7d", Acknowledge = item.AckDOS7d, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOS7d, AdminAck = item.AlarmNCDOS7d == false ? "Yes" : "No", MasterAck = item.AlarmNCDOS7d_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOS7d_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });
                    if (dos.AddDays(-1) <= dt && item.DOSDays1)
                        list.Add(new CertificateNC() { Id = item.Id, CertificateName = item.CName, IssueDate = item.DOI, ExpiryDate = item.DOE, SurveyDate = item.DOS, AlertFrequencyType = item.NCDOS1d, NCType = "NCDOS1d", Acknowledge = item.AckDOS1d, Vessel_ID = vessel_id, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, AlertFrequency = item.NCDOS1d, AdminAck = item.AlarmNCDOS1d == false ? "Yes" : "No", MasterAck = item.AlarmNCDOS1d_MASTER == false ? "Yes" : "No", HODAck = item.AlarmNCDOS1d_HOD == false ? "Yes" : "No", UserType = "Admin", IsCheckedV = false });

                }

                return list;

            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }



    }
}
