using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;


namespace DataBuildingLayer
{
    public class ShipmentContaxt : DbContext, ILogBase
    {
        public ShipmentContaxt() : base("ShipmentContaxt")
        {
            //this.Configuration.AutoDetectChangesEnabled = false;
            //this.Configuration.ValidateOnSaveEnabled = false;

        }

       public SqlConnection con = ConnectionBulder.con;

        public DbSet<AdminLoginClass> AdminLogins { get; set; }
        public DbSet<OverTimeClass> OverTimes { get; set; }
        public DbSet<InfoLogClass> InfoLogs { get; set; }
        public DbSet<InternationalClass> Internationals { get; set; }
      

        public DbSet<UserAccessClass> UserAccessHOD { get; set; }
        public DbSet<CommentofVSClass> CommentofVs { get; set; }
        public DbSet<CertificatesClass> Certificates { get; set; }
        public DbSet<CrewDetailClass> CrewDetails { get; set; }
        public DbSet<WorkHoursClass> WorkHourss { get; set; }
        public DbSet<RuffCodeClass> RuffCodes { get; set; }
        public DbSet<versionClass> Versions { get; set; }
        public DbSet<VesselClass> Vessels { get; set; }
        public DbSet<WorkHoursPlannerClass> WorkHourPlanners { get; set; }

        public DbSet<OPAStartStopClass> OPAStartStops { get; set; }
        public DbSet<RetardtblClass> Retardtbls { get; set; }
        public DbSet<DefineRulesClass> DefineRules { get; set; }
        public DbSet<RuleTableClass> RuleTables { get; set; }
        public DbSet<HolidaysClass> Holidays { get; set; }
        public DbSet<CertificateNotificationClass> CertificateNotifications { get; set; }
        public DbSet<CrewRankClass> CrewRanks { get; set; }
        public DbSet<CurrencyClass> Currencys { get; set; }
        public DbSet<DepartmentClass> Departments { get; set; }
        public DbSet<FreezeClass> Freezes { get; set; }
        public DbSet<GroupPlannerClass> GroupPlanners { get; set; }
        public DbSet<AddGroupClass> AddGroups { get; set; }
        public DbSet<HoliDayGroupNameClass> HoliDayGroupNames { get; set; }
        public DbSet<LogbookClass> Logbooks { get; set; }

        public DbSet<CertificateOrderClass> CertificateOrders { get; set; }





        // ON SIGNBOARD........
        public List<CrewDetailClass> GetCrewDetail(string searchCrew)
        {
            try
            {


                DateTime dd = DateTime.Now.Date;
                var data = CrewDetails.Where(p => p.ServiceTo >= dd).ToList();

                if (UserTypeClass.UserTypes == "Crew")
                {
                    data = CrewDetails.Where(p => p.ServiceTo >= dd & p.UserName == UserTypeClass.UserName).ToList();
                }

                if (!string.IsNullOrEmpty(searchCrew))
                {
                    data = data.Where(p => p.name.ToLower().Contains(searchCrew.Trim().ToLower()) || p.position.ToLower().Contains(searchCrew.Trim().ToLower())).ToList();

                }
                var workhrs1 = WorkHourss.ToList(); //.Where(s => s.Last_update == workhrs1.Max(x => x.Last_update))
                var workhrs = workhrs1.OrderByDescending(o => o.Last_update).Select(s => new WorkHoursClass { wid = s.wid, UserName = s.UserName, dates = s.dates, Last_update = s.Last_update }).GroupBy(n => n.UserName).Select(g => g.FirstOrDefault()).ToList();

                var LeftJoinData = (from emp in data
                                    join dept in workhrs
                                    on emp.UserName.Trim() equals dept.UserName.Trim() into JoinedEmpDept
                                    from dept in JoinedEmpDept.DefaultIfEmpty()
                                    select new CrewDetailClass()
                                    {
                                        Id = emp.Id,
                                        name = emp.name,
                                        UserName = emp.UserName,
                                        position = emp.position,
                                        department = emp.department,
                                        ServiceFrom = emp.ServiceFrom,
                                        ServiceTo = emp.ServiceTo,
                                        CDC = emp.CDC,
                                        empno = emp.empno,
                                        DateAvailble = dept != null ? Convert.ToDateTime(dept.Last_update).ToString("dd-MMM-yyyy") : "No Entry"
                                    }).ToList();


                return LeftJoinData;
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }

        // OFF SIGNBOARD........
        public List<CrewDetailClass> GetCrewDetail1(string searchCrew)
        {
            try
            {
                DateTime dd = DateTime.Now.Date;
                var data = CrewDetails.Where(p => p.ServiceTo < dd).ToList();
                if (UserTypeClass.UserTypes == "Crew")
                {
                    data = CrewDetails.Where(p => p.ServiceTo < dd & p.UserName == UserTypeClass.UserName).ToList();
                }

                if (!string.IsNullOrEmpty(searchCrew))
                {
                    data = data.Where(p => p.name.ToLower().Contains(searchCrew.Trim().ToLower()) || p.position.ToLower().Contains(searchCrew.Trim().ToLower())).ToList();
                }

                var workhrs1 = WorkHourss.ToList(); //.Where(s => s.Last_update == workhrs1.Max(x => x.Last_update))
                var workhrs = workhrs1.OrderByDescending(o => o.Last_update).Select(s => new WorkHoursClass { wid = s.wid, UserName = s.UserName, dates = s.dates, Last_update = s.Last_update }).GroupBy(n => n.UserName).Select(g => g.FirstOrDefault()).ToList();

                var LeftJoinData = (from emp in data
                                    join dept in workhrs
                                    on emp.UserName equals dept.UserName into JoinedEmpDept
                                    from dept in JoinedEmpDept.DefaultIfEmpty()
                                    select new CrewDetailClass()
                                    {
                                        Id = emp.Id,
                                        name = emp.name,
                                        UserName = emp.UserName,
                                        position = emp.position,
                                        department = emp.department,
                                        ServiceFrom = emp.ServiceFrom,
                                        ServiceTo = emp.ServiceTo,
                                        CDC = emp.CDC,
                                        empno = emp.empno,
                                        DateAvailble = dept != null ? Convert.ToDateTime(dept.Last_update).ToString("dd-MMM-yyyy") : "No Entry"
                                    }).ToList();


                return LeftJoinData;
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }




        public List<WorkHoursClass> GetWorkHoursList(DateTime stdate, DateTime estdate, string UserName)
        {
            try
            {
                var dt = stdate.ToShortDateString();
                var dt1 = estdate.ToShortDateString();

                var dateAndTime = DateTime.Now;
                var date = dateAndTime.Date;

                SqlConnection con = ConnectionBulder.con;
                List<WorkHoursClass> workHoursList = new List<WorkHoursClass>();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = @"select wid , UserName , FullName ,Position ,TotalHours , RestHours ,options ,Remarks ,dates ,NonConfirmities , hrs ,Colors ,
        
         ColorName , Department ,MonthName , YearValue , cid ,Normal_WKH ,DaysOfMonth , overtime , StatusCrew , CrewNC ,  MasterCrewNC , HODCrewNC , datetimes ,UserType ,
        
         opa , RestHour7day ,NormalOT1 , RuleNames , NC1 ,NCD1 ,NC2 , NCD2 , NC3 , NCD3 , NC4 , NCD4 , NC5 ,NCD5 ,NC6 , NCD6 ,NC7 ,
       
         NCD7 ,NC8 , NCD8 ,NC9 ,NCD9 ,NC10 ,NCD10 , NC11 ,NCD11 ,NC12 , NCD12 ,NC13 ,NCD13 ,NC14 ,NCD14 , NC15 ,
         
         NCD15 ,NC16 , NCD16 , NC17 ,NCD17 , NC18 , NCD18 , YNC1 ,YNCD1 ,YNC2 ,YNCD2 ,YNC3 , YNCD3 , YNC4 , YNCD4 , YNC5 ,YNCD5 ,YNC6 ,YNCD6 ,YNC7 ,YNCD7 ,
         
         YNC8 , YNCD8 , RestHourAny24 , RestHourAny7day ,options1 , opayoung ,MNCCD1 , MNCCD2 ,MNCCD3 , MNCCD4 , MNCCD5 ,MNCCD6 ,MNCCD7 , MNCCD8 ,MNCCD9 ,MNCCD10 ,
        
         MNCCD11 ,MNCCD12 ,MNCCD13 ,MNCCD14 ,MNCCD15 , MNCCD16 ,MNCCD17 , MNCCD18 , MYNCCD1 ,MYNCCD2 ,MYNCCD3 , MYNCCD4 ,MYNCCD5 ,MYNCCD6 ,MYNCCD7 ,
         
         MYNCCD8 ,Last_update ,AdminAlarm , MasterAlarm ,HODAlarm , HNCCD1 ,HNCCD2 ,HNCCD3, HNCCD4 ,HNCCD5 ,HNCCD6 ,HNCCD7 ,HNCCD8 ,
         
         HNCCD9 ,HNCCD10 ,HNCCD11 ,HNCCD12 ,HNCCD13 ,HNCCD14 , HNCCD15 ,HNCCD16 ,HNCCD17 , HNCCD18, HYNCCD1 ,HYNCCD2 ,HYNCCD3 , HYNCCD4 ,HYNCCD5 , HYNCCD6 
         ,HYNCCD7 ,HYNCCD8 ,WRID ,Days ,Cellcount ,UnFreezeDates,NonConfirmities1 from WorkHours where UserName ='" + UserName + "' and  dates  between '" + stdate.ToShortDateString() + "' and '" + estdate.ToShortDateString() + "' ";//where dates >= " + stdate + " and dates <= " + estdate + "

                SqlCommand cmd = new SqlCommand(query, con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        WorkHoursClass ob = new WorkHoursClass();
                        ob.wid = Convert.ToInt32(dr["wid"].ToString());
                        ob.UserName = dr["UserName"].ToString();

                        ob.FullName = dr["FullName"].ToString();
                        ob.Position = dr["Position"].ToString();
                        ob.TotalHours = Convert.ToDecimal(dr["TotalHours"].ToString());
                        ob.RestHours = Convert.ToDecimal(dr["RestHours"].ToString());
                        ob.options = dr["options"].ToString();
                        ob.Remarks = dr["Remarks"].ToString();
                        ob.dates = Convert.ToDateTime(dr["dates"].ToString());
                        ob.NonConfirmities = dr["NonConfirmities"].ToString();

                        ob.hrs = dr["hrs"].ToString();
                        ob.Colors = dr["Colors"].ToString();
                        ob.Department = dr["Department"].ToString();
                        ob.MonthName = dr["MonthName"].ToString();
                        ob.YearValue = dr["YearValue"].ToString();
                        ob.cid = Convert.ToInt32(dr["cid"].ToString());
                        ob.Normal_WKH = Convert.ToInt32(dr["Normal_WKH"].ToString());
                        ob.DaysOfMonth = dr["DaysOfMonth"].ToString();
                        ob.overtime = Convert.ToDecimal(dr["overtime"].ToString());
                        ob.StatusCrew = Convert.ToBoolean(dr["StatusCrew"].ToString());


                        ob.CrewNC = Convert.ToBoolean(dr["CrewNC"].ToString());
                        ob.MasterCrewNC = Convert.ToBoolean(dr["MasterCrewNC"].ToString());
                        ob.HODCrewNC = Convert.ToBoolean(dr["HODCrewNC"].ToString());
                        ob.datetimes = Convert.ToDateTime(dr["datetimes"].ToString());
                        ob.UserType = dr["UserType"].ToString();
                        ob.opa = Convert.ToBoolean(dr["opa"].ToString());
                        ob.RestHour7day = Convert.ToDecimal(dr["RestHour7day"].ToString());

                        ob.NormalOT1 = Convert.ToDecimal(dr["NormalOT1"].ToString());
                        ob.RuleNames = dr["RuleNames"].ToString();


                        if (!string.IsNullOrEmpty(dr["NC1"].ToString()))
                            ob.NC1 = dr["NC1"].ToString();



                        if (!string.IsNullOrEmpty(dr["NCD1"].ToString()))
                            ob.NCD1 = Convert.ToDateTime(dr["NCD1"].ToString());
                        if (!string.IsNullOrEmpty(dr["NC2"].ToString()))
                            ob.NC2 = dr["NC2"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD2"].ToString()))
                            ob.NCD2 = Convert.ToDateTime(dr["NCD2"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC3"].ToString()))
                            ob.NC3 = dr["NC3"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD3"].ToString()))
                            ob.NCD3 = Convert.ToDateTime(dr["NCD3"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC4"].ToString()))
                            ob.NC4 = dr["NC4"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD4"].ToString()))
                            ob.NCD4 = Convert.ToDateTime(dr["NCD4"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC5"].ToString()))
                            ob.NC5 = dr["NC5"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD5"].ToString()))
                            ob.NCD5 = Convert.ToDateTime(dr["NCD5"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC6"].ToString()))
                            ob.NC6 = dr["NC6"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD6"].ToString()))
                            ob.NCD6 = Convert.ToDateTime(dr["NCD6"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC7"].ToString()))
                            ob.NC7 = dr["NC7"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD7"].ToString()))
                            ob.NCD7 = Convert.ToDateTime(dr["NCD7"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC8"].ToString()))
                            ob.NC8 = dr["NC8"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD8"].ToString()))
                            ob.NCD8 = Convert.ToDateTime(dr["NCD8"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC9"].ToString()))
                            ob.NC9 = dr["NC9"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD9"].ToString()))
                            ob.NCD9 = Convert.ToDateTime(dr["NCD9"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC10"].ToString()))
                            ob.NC10 = dr["NC10"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD10"].ToString()))
                            ob.NCD10 = Convert.ToDateTime(dr["NCD10"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC11"].ToString()))
                            ob.NC11 = dr["NC11"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD11"].ToString()))
                            ob.NCD11 = Convert.ToDateTime(dr["NCD11"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC12"].ToString()))
                            ob.NC12 = dr["NC12"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD12"].ToString()))
                            ob.NCD12 = Convert.ToDateTime(dr["NCD12"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC13"].ToString()))
                            ob.NC13 = dr["NC13"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD13"].ToString()))
                            ob.NCD13 = Convert.ToDateTime(dr["NCD13"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC14"].ToString()))
                            ob.NC14 = dr["NC14"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD14"].ToString()))
                            ob.NCD14 = Convert.ToDateTime(dr["NCD14"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC15"].ToString()))
                            ob.NC15 = dr["NC15"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD15"].ToString()))
                            ob.NCD15 = Convert.ToDateTime(dr["NCD15"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC16"].ToString()))
                            ob.NC16 = dr["NC16"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD16"].ToString()))
                            ob.NCD16 = Convert.ToDateTime(dr["NCD16"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC17"].ToString()))
                            ob.NC17 = dr["NC17"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD17"].ToString()))
                            ob.NCD17 = Convert.ToDateTime(dr["NCD17"].ToString());

                        if (!string.IsNullOrEmpty(dr["NC18"].ToString()))
                            ob.NC18 = dr["NC18"].ToString();

                        if (!string.IsNullOrEmpty(dr["NCD18"].ToString()))
                            ob.NCD18 = Convert.ToDateTime(dr["NCD18"].ToString());

                        if (!string.IsNullOrEmpty(dr["YNC1"].ToString()))
                            ob.YNC1 = dr["YNC1"].ToString();

                        if (!string.IsNullOrEmpty(dr["YNCD1"].ToString()))
                            ob.YNCD1 = Convert.ToDateTime(dr["YNCD1"].ToString());

                        if (!string.IsNullOrEmpty(dr["YNC2"].ToString()))
                            ob.YNC2 = dr["YNC2"].ToString();

                        if (!string.IsNullOrEmpty(dr["YNCD2"].ToString()))
                            ob.YNCD2 = Convert.ToDateTime(dr["YNCD2"].ToString());

                        if (!string.IsNullOrEmpty(dr["YNC3"].ToString()))
                            ob.YNC3 = dr["YNC3"].ToString();

                        if (!string.IsNullOrEmpty(dr["YNCD3"].ToString()))
                            ob.YNCD3 = Convert.ToDateTime(dr["YNCD3"].ToString());

                        if (!string.IsNullOrEmpty(dr["YNC4"].ToString()))
                            ob.YNC4 = dr["YNC4"].ToString();

                        if (!string.IsNullOrEmpty(dr["YNCD4"].ToString()))
                            ob.YNCD4 = Convert.ToDateTime(dr["YNCD4"].ToString());

                        if (!string.IsNullOrEmpty(dr["YNC5"].ToString()))
                            ob.YNC5 = dr["YNC5"].ToString();

                        if (!string.IsNullOrEmpty(dr["YNCD5"].ToString()))
                            ob.YNCD5 = Convert.ToDateTime(dr["YNCD5"].ToString());

                        if (!string.IsNullOrEmpty(dr["YNC6"].ToString()))
                            ob.YNC6 = dr["YNC6"].ToString();

                        if (!string.IsNullOrEmpty(dr["YNCD6"].ToString()))
                            ob.YNCD6 = Convert.ToDateTime(dr["YNCD6"].ToString());

                        if (!string.IsNullOrEmpty(dr["YNC7"].ToString()))
                            ob.YNC7 = dr["YNC7"].ToString();

                        if (!string.IsNullOrEmpty(dr["YNCD7"].ToString()))
                            ob.YNCD7 = Convert.ToDateTime(dr["YNCD7"].ToString());

                        if (!string.IsNullOrEmpty(dr["YNC8"].ToString()))
                            ob.YNC8 = dr["YNC8"].ToString();

                        if (!string.IsNullOrEmpty(dr["YNCD8"].ToString()))
                            ob.YNCD8 = Convert.ToDateTime(dr["YNCD8"].ToString());


                        ob.RestHourAny24 = dr["RestHourAny24"].ToString();
                        ob.RestHourAny7day = dr["RestHourAny7day"].ToString();
                        ob.options1 = dr["options1"].ToString();
                        ob.opayoung = Convert.ToBoolean(dr["opayoung"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD1"].ToString()))
                            ob.MNCCD1 = Convert.ToDateTime(dr["MNCCD1"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD2"].ToString()))
                            ob.MNCCD2 = Convert.ToDateTime(dr["MNCCD2"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD3"].ToString()))
                            ob.MNCCD3 = Convert.ToDateTime(dr["MNCCD3"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD4"].ToString()))
                            ob.MNCCD4 = Convert.ToDateTime(dr["MNCCD4"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD5"].ToString()))
                            ob.MNCCD5 = Convert.ToDateTime(dr["MNCCD5"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD6"].ToString()))
                            ob.MNCCD6 = Convert.ToDateTime(dr["MNCCD6"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD7"].ToString()))
                            ob.MNCCD7 = Convert.ToDateTime(dr["MNCCD7"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD8"].ToString()))
                            ob.MNCCD8 = Convert.ToDateTime(dr["MNCCD8"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD9"].ToString()))
                            ob.MNCCD9 = Convert.ToDateTime(dr["MNCCD9"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD10"].ToString()))
                            ob.MNCCD10 = Convert.ToDateTime(dr["MNCCD10"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD11"].ToString()))
                            ob.MNCCD11 = Convert.ToDateTime(dr["MNCCD11"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD12"].ToString()))
                            ob.MNCCD12 = Convert.ToDateTime(dr["MNCCD12"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD13"].ToString()))
                            ob.MNCCD13 = Convert.ToDateTime(dr["MNCCD13"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD14"].ToString()))
                            ob.MNCCD14 = Convert.ToDateTime(dr["MNCCD14"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD15"].ToString()))
                            ob.MNCCD15 = Convert.ToDateTime(dr["MNCCD15"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD16"].ToString()))
                            ob.MNCCD16 = Convert.ToDateTime(dr["MNCCD16"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD17"].ToString()))
                            ob.MNCCD17 = Convert.ToDateTime(dr["MNCCD17"].ToString());

                        if (!string.IsNullOrEmpty(dr["MNCCD18"].ToString()))
                            ob.MNCCD18 = Convert.ToDateTime(dr["MNCCD18"].ToString());



                        if (!string.IsNullOrEmpty(dr["MYNCCD1"].ToString()))
                            ob.MYNCCD1 = Convert.ToDateTime(dr["MYNCCD1"].ToString());

                        if (!string.IsNullOrEmpty(dr["MYNCCD2"].ToString()))
                            ob.MYNCCD2 = Convert.ToDateTime(dr["MYNCCD2"].ToString());

                        if (!string.IsNullOrEmpty(dr["MYNCCD3"].ToString()))
                            ob.MYNCCD3 = Convert.ToDateTime(dr["MYNCCD3"].ToString());

                        if (!string.IsNullOrEmpty(dr["MYNCCD4"].ToString()))
                            ob.MYNCCD4 = Convert.ToDateTime(dr["MYNCCD4"].ToString());

                        if (!string.IsNullOrEmpty(dr["MYNCCD5"].ToString()))
                            ob.MYNCCD5 = Convert.ToDateTime(dr["MYNCCD5"].ToString());

                        if (!string.IsNullOrEmpty(dr["MYNCCD6"].ToString()))
                            ob.MYNCCD6 = Convert.ToDateTime(dr["MYNCCD6"].ToString());

                        if (!string.IsNullOrEmpty(dr["MYNCCD7"].ToString()))
                            ob.MYNCCD7 = Convert.ToDateTime(dr["MYNCCD7"].ToString());

                        if (!string.IsNullOrEmpty(dr["MYNCCD8"].ToString()))
                            ob.MYNCCD8 = Convert.ToDateTime(dr["MYNCCD8"].ToString());

                        if (!string.IsNullOrEmpty(dr["Last_update"].ToString()))
                            ob.Last_update = Convert.ToDateTime(dr["Last_update"].ToString());

                        ob.AdminAlarm = Convert.ToBoolean(dr["AdminAlarm"].ToString());
                        ob.MasterAlarm = Convert.ToBoolean(dr["MasterAlarm"].ToString());
                        ob.HODAlarm = Convert.ToBoolean(dr["HODAlarm"].ToString());


                        if (!string.IsNullOrEmpty(dr["HNCCD1"].ToString()))
                            ob.HNCCD1 = Convert.ToDateTime(dr["HNCCD1"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD2"].ToString()))
                            ob.HNCCD2 = Convert.ToDateTime(dr["HNCCD2"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD3"].ToString()))
                            ob.HNCCD3 = Convert.ToDateTime(dr["HNCCD3"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD4"].ToString()))
                            ob.HNCCD4 = Convert.ToDateTime(dr["HNCCD4"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD5"].ToString()))
                            ob.HNCCD5 = Convert.ToDateTime(dr["HNCCD5"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD6"].ToString()))
                            ob.HNCCD6 = Convert.ToDateTime(dr["HNCCD6"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD7"].ToString()))
                            ob.HNCCD7 = Convert.ToDateTime(dr["HNCCD7"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD8"].ToString()))
                            ob.HNCCD8 = Convert.ToDateTime(dr["HNCCD8"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD9"].ToString()))
                            ob.HNCCD9 = Convert.ToDateTime(dr["HNCCD9"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD10"].ToString()))
                            ob.HNCCD10 = Convert.ToDateTime(dr["HNCCD10"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD11"].ToString()))
                            ob.HNCCD11 = Convert.ToDateTime(dr["HNCCD11"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD12"].ToString()))
                            ob.HNCCD12 = Convert.ToDateTime(dr["HNCCD12"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD13"].ToString()))
                            ob.HNCCD13 = Convert.ToDateTime(dr["HNCCD13"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD14"].ToString()))
                            ob.HNCCD14 = Convert.ToDateTime(dr["HNCCD14"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD15"].ToString()))
                            ob.HNCCD15 = Convert.ToDateTime(dr["HNCCD15"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD16"].ToString()))
                            ob.HNCCD16 = Convert.ToDateTime(dr["HNCCD16"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD17"].ToString()))
                            ob.HNCCD17 = Convert.ToDateTime(dr["HNCCD17"].ToString());

                        if (!string.IsNullOrEmpty(dr["HNCCD18"].ToString()))
                            ob.HNCCD18 = Convert.ToDateTime(dr["HNCCD18"].ToString());



                        if (!string.IsNullOrEmpty(dr["HYNCCD1"].ToString()))
                            ob.HYNCCD1 = Convert.ToDateTime(dr["HYNCCD1"].ToString());

                        if (!string.IsNullOrEmpty(dr["HYNCCD2"].ToString()))
                            ob.HYNCCD2 = Convert.ToDateTime(dr["HYNCCD2"].ToString());

                        if (!string.IsNullOrEmpty(dr["HYNCCD3"].ToString()))
                            ob.HYNCCD3 = Convert.ToDateTime(dr["HYNCCD3"].ToString());

                        if (!string.IsNullOrEmpty(dr["HYNCCD4"].ToString()))
                            ob.HYNCCD4 = Convert.ToDateTime(dr["HYNCCD4"].ToString());

                        if (!string.IsNullOrEmpty(dr["HYNCCD5"].ToString()))
                            ob.HYNCCD5 = Convert.ToDateTime(dr["HYNCCD5"].ToString());

                        if (!string.IsNullOrEmpty(dr["HYNCCD6"].ToString()))
                            ob.HYNCCD6 = Convert.ToDateTime(dr["HYNCCD6"].ToString());

                        if (!string.IsNullOrEmpty(dr["HYNCCD7"].ToString()))
                            ob.HYNCCD7 = Convert.ToDateTime(dr["HYNCCD7"].ToString());

                        if (!string.IsNullOrEmpty(dr["HYNCCD8"].ToString()))
                            ob.HYNCCD8 = Convert.ToDateTime(dr["HYNCCD8"].ToString());

                        ob.WRID = dr["WRID"].ToString();
                        ob.Days = dr["Days"].ToString();
                        ob.Cellcount = Convert.ToInt32(dr["Cellcount"].ToString());
                        if (!string.IsNullOrEmpty(dr["UnFreezeDates"].ToString()))
                            ob.UnFreezeDates = Convert.ToDateTime(dr["UnFreezeDates"].ToString());
                        ob.NonConfirmities1 = dr["NonConfirmities1"].ToString();


                        workHoursList.Add(ob);
                    }

                }

                return workHoursList;

            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }



        public async void CreateLog(string modulename, string actionname, DateTime? edate)
        {
            try
            {
                DateTime dt1 = Convert.ToDateTime(DateTime.Now.ToString());
                var obj = new InfoLogClass()
                {

                    dt = dt1,
                    Description = "Admin",  //Position/Rank
                    UserName = "admin",   //Username
                    ModuleName = modulename,
                    ActionName = actionname,
                    Editdate = edate

                };

                InfoLogs.Add(obj);
                await SaveChangesAsync();

            }
            catch (Exception ex)
            {
                ErrorLog(ex);
            }
        }

        public void ErrorLog(Exception ex)
        {
            //var errorMessage = string.Format("An exception occurred !  {0}", "Please contact to support team");


            var st = new StackTrace(ex, true);
            var frames = st.GetFrames();
            var traceString = new StringBuilder();

            //var ss = AppDomain.CurrentDomain.BaseDirectory;

            foreach (var frame in frames)
            {
                if (frame.GetFileLineNumber() < 1)
                    continue;

                traceString.Append("Data Time: " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt") + Environment.NewLine);
                traceString.Append("Exception Name: " + ex.Message + Environment.NewLine);
                traceString.Append("File Name: " + frame.GetFileName() + Environment.NewLine);
                traceString.Append("Function Name: " + frame.GetMethod().Name + Environment.NewLine);
                traceString.Append("Line Number: " + frame.GetFileLineNumber() + Environment.NewLine + Environment.NewLine);

                MessageBox.Show("An exception occurred ! Please contact to support team", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            //Write log containing our exception information

            string path0 = @"C:\Work-Ship_DB_Backup";
            if (!Directory.Exists(path0))
            {
                Directory.CreateDirectory(path0);
            }

            string path1 = @"\Systemlogfile.txt";
            string path2 = path0 + path1;
            if (!File.Exists(path2))
            {

                File.WriteAllText("C:\\Work-Ship_DB_Backup" + "\\Systemlogfile.txt", traceString.ToString());

            }
            else
            {
                StreamWriter log = null;
                log = File.AppendText(path2);
                log.Write(traceString.ToString());
                log.Close();
                log.Dispose();


            }


            //MessageBox.Show("An exception occurred ! Please contact to support team", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //MessageBox.Show("An exception occurred ! Please contact to support team", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);


        }

        public void refreshmessage(CertificatesClass obj)
        {
            //CertificatesClass cdc = obj;
            CertificatesClass m = (obj as CertificatesClass); //DownCasting.....

            if (m.ExpiryStatus == true)
            {
                m.DOE = m.DOE.Date.AddYears(100);
                m.DOS = m.DOS.Date.AddYears(100);
            }
            if (m.month6)
            {
                m.AckDOE6m = "To be Acknowledged";
                m.AckDOE6m_MASTER = "To be Acknowledged";
                m.AckDOE6m_HOD = "To be Acknowledged";
                m.NCDOE6m = "6 Months to expire";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOE6m = null;
                m.AckDOE6m_MASTER = null;
                m.AckDOE6m_HOD = null;
                m.NCDOE6m = null;
                //RaisePropertyChanged("AddCertificates");
            }
            if (m.month3)
            {
                m.AckDOE3m = "To be Acknowledged";
                m.AckDOE3m_MASTER = "To be Acknowledged";
                m.AckDOE3m_HOD = "To be Acknowledged";
                m.NCDOE3m = "3 Months to expire";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOE3m = null;
                m.AckDOE3m_MASTER = null;
                m.AckDOE3m_HOD = null;
                m.NCDOE3m = null;
                //RaisePropertyChanged("AddCertificates");
            }
            if (m.month1)
            {
                m.AckDOE1m = "To be Acknowledged";
                m.AckDOE1m_MASTER = "To be Acknowledged";
                m.AckDOE1m_HOD = "To be Acknowledged";
                m.NCDOE1m = "1 Month to expire";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOE1m = null;
                m.AckDOE1m_MASTER = null;
                m.AckDOE1m_HOD = null;
                m.NCDOE1m = null;
                //RaisePropertyChanged("AddCertificates");
            }
            if (m.Days15)
            {
                m.AckDOE15d = "To be Acknowledged";
                m.AckDOE15d_MASTER = "To be Acknowledged";
                m.AckDOE15d_HOD = "To be Acknowledged";
                m.NCDOE15d = "15 Days to expire";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOE15d = null;
                m.AckDOE15d_MASTER = null;
                m.AckDOE15d_HOD = null;
                m.NCDOE15d = null;
                //RaisePropertyChanged("AddCertificates");
            }
            if (m.Days7)
            {
                m.AckDOE7d = "To be Acknowledged";
                m.AckDOE7d_MASTER = "To be Acknowledged";
                m.AckDOE7d_HOD = "To be Acknowledged";
                m.NCDOE7d = "7 Days to expire";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOE7d = null;
                m.AckDOE7d_MASTER = null;
                m.AckDOE7d_HOD = null;
                m.NCDOE7d = null;
                //RaisePropertyChanged("AddCertificates");
            }
            if (m.Days1)
            {
                m.AckDOE1d = "To be Acknowledged";
                m.AckDOE1d_MASTER = "To be Acknowledged";
                m.AckDOE1d_HOD = "To be Acknowledged";
                m.NCDOE1d = "1 Day to expire";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOE1d = null;
                m.AckDOE1d_MASTER = null;
                m.AckDOE1d_HOD = null;
                m.NCDOE1d = null;
                //RaisePropertyChanged("AddCertificates");
            }

            if (m.DOSmonth6)
            {
                m.AckDOS6m = "To be Acknowledged";
                m.AckDOS6m_MASTER = "To be Acknowledged";
                m.AckDOS6m_HOD = "To be Acknowledged";
                m.NCDOS6m = "6 Months to survey";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOS6m = null;
                m.AckDOS6m_MASTER = null;
                m.AckDOS6m_HOD = null;
                m.NCDOS6m = null;
                //RaisePropertyChanged("AddCertificates");
            }
            if (m.DOSmonth3)
            {
                m.AckDOS3m = "To be Acknowledged";
                m.AckDOS3m_MASTER = "To be Acknowledged";
                m.AckDOS3m_HOD = "To be Acknowledged";
                m.NCDOS3m = "3 Months to survey";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOS3m = null;
                m.AckDOS3m_MASTER = null;
                m.AckDOS3m_HOD = null;
                m.NCDOS3m = null;
                //RaisePropertyChanged("AddCertificates");
            }
            if (m.DOSmonth1)
            {
                m.AckDOS1m = "To be Acknowledged";
                m.AckDOS1m_MASTER = "To be Acknowledged";
                m.AckDOS1m_HOD = "To be Acknowledged";
                m.NCDOS1m = "1 Month to survey";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOS1m = null;
                m.AckDOS1m_MASTER = null;
                m.AckDOS1m_HOD = null;
                m.NCDOS1m = null;
                //RaisePropertyChanged("AddCertificates");
            }
            if (m.DOSDays15)
            {
                m.AckDOS15d = "To be Acknowledged";
                m.AckDOS15d_MASTER = "To be Acknowledged";
                m.AckDOS15d_HOD = "To be Acknowledged";
                m.NCDOS15d = "15 Days to survey";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOS15d = null;
                m.AckDOS15d_MASTER = null;
                m.AckDOS15d_HOD = null;
                m.NCDOS15d = null;
                //RaisePropertyChanged("AddCertificates");
            }
            if (m.DOSDays7)
            {
                m.AckDOS7d = "To be Acknowledged";
                m.AckDOS7d_MASTER = "To be Acknowledged";
                m.AckDOS7d_HOD = "To be Acknowledged";
                m.NCDOS7d = "7 Days to survey";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOS7d = null;
                m.AckDOS7d_MASTER = null;
                m.AckDOS7d_HOD = null;
                m.NCDOS7d = null;
                //RaisePropertyChanged("AddCertificates");
            }
            if (m.DOSDays1)
            {
                m.AckDOS1d = "To be Acknowledged";
                m.AckDOS1d_MASTER = "To be Acknowledged";
                m.AckDOS1d_HOD = "To be Acknowledged";
                m.NCDOS1d = "1 Day to survey";
                //RaisePropertyChanged("AddCertificates");
            }
            else
            {
                m.AckDOS1d = null;
                m.AckDOS1d_MASTER = null;
                m.AckDOS1d_HOD = null;
                m.NCDOS1d = null;
                //RaisePropertyChanged("AddCertificates");
            }

            m.OverDOE = "Expired";
            m.OverDOS = "Survey over due";
            m.AckDOEOver = "To be Acknowledged";
            m.AckDOSOver = "To be Acknowledged";
            m.AckDOEOver_MASTER = "To be Acknowledged";
            m.AckDOSOver_MASTER = "To be Acknowledged";
            m.AckDOEOver_HOD = "To be Acknowledged";
            m.AckDOSOver_HOD = "To be Acknowledged";

            m.AlarmNCDOE6m = true;
            m.AlarmNCDOE3m = true;
            m.AlarmNCDOE1m = true;
            m.AlarmNCDOE15d = true;
            m.AlarmNCDOE7d = true;
            m.AlarmNCDOE1d = true;
            m.AlarmNCDOEXXX = true;
            m.AlarmNCDOS6m = true;
            m.AlarmNCDOS3m = true;
            m.AlarmNCDOS1m = true;
            m.AlarmNCDOS15d = true;
            m.AlarmNCDOS7d = true;
            m.AlarmNCDOS1d = true;
            m.AlarmNCDOSXXX = true;

            m.AlarmNCDOE6m_MASTER = true;
            m.AlarmNCDOE3m_MASTER = true;
            m.AlarmNCDOE1m_MASTER = true;
            m.AlarmNCDOE15d_MASTER = true;
            m.AlarmNCDOE7d_MASTER = true;
            m.AlarmNCDOE1d_MASTER = true;
            m.AlarmNCDOEXXX_MASTER = true;
            m.AlarmNCDOS6m_MASTER = true;
            m.AlarmNCDOS3m_MASTER = true;
            m.AlarmNCDOS1m_MASTER = true;
            m.AlarmNCDOS15d_MASTER = true;
            m.AlarmNCDOS7d_MASTER = true;
            m.AlarmNCDOS1d_MASTER = true;
            m.AlarmNCDOSXXX_MASTER = true;
            m.AlarmNCDOE6m_HOD = true;
            m.AlarmNCDOE3m_HOD = true;
            m.AlarmNCDOE1m_HOD = true;
            m.AlarmNCDOE15d_HOD = true;
            m.AlarmNCDOE7d_HOD = true;
            m.AlarmNCDOE1d_HOD = true;
            m.AlarmNCDOEXXX_HOD = true;
            m.AlarmNCDOS6m_HOD = true;
            m.AlarmNCDOS3m_HOD = true;
            m.AlarmNCDOS1m_HOD = true;
            m.AlarmNCDOS15d_HOD = true;
            m.AlarmNCDOS7d_HOD = true;
            m.AlarmNCDOS1d_HOD = true;
            m.AlarmNCDOSXXX_HOD = true;



        }


        public System.Data.DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            System.Data.DataTable table = new System.Data.DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in Linqlist)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }

            return table;
        }

        public string[] CommonCalender(string syearname, string smonthname)
        {
            syearname = syearname ?? DateTime.Now.Year.ToString();
            smonthname = smonthname ?? DateTime.Now.ToString("MMMM");

            int years = Convert.ToInt32(syearname);
            var monthid = DateTime.ParseExact(smonthname, "MMMM", CultureInfo.CurrentCulture).Month;
            int days = DateTime.DaysInMonth(years, monthid);

            DateTime TodayDate = new DateTime(years, monthid, 1);
            DateTime EndDate = TodayDate.AddMonths(1).AddDays(-1);
            var data = Internationals.Where(x => (System.Data.Entity.DbFunctions.TruncateTime(x.RDate) >= TodayDate.Date && System.Data.Entity.DbFunctions.TruncateTime(x.RDate) <= EndDate.Date)).ToList();


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

            string[] arr = noofdays.TrimEnd(',').Split(',');

            return arr;
        }


      



        public ObservableCollection<string> GetMonth()
        {
            var years = new ObservableCollection<string>();
            for (int i = 0; i < 12; i++)
            {
                years.Add(CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i]);

            }
            return years;
        }

        public ObservableCollection<string> GetYear()
        {
            var years = new ObservableCollection<string>();
            int year = DateTime.Now.Year;
            for (int i = year - 7; i <= year + 7; i++)
            {
                years.Add(i.ToString());
            }
            return years;
        }

        public void ObservableCollectionList<T>(ObservableCollection<T> collection, IList<T> items)
        {
            items.ToList().ForEach(collection.Add);
        }



        public string Encrypt(string strText, string strEncrypt)
        {
            byte[] byKey = new byte[20];
            byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, dv), CryptoStreamMode.Write);
                cs.Write(inputArray, 0, inputArray.Length); cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }


        //===============

        public string Decrypt(string strText, string strEncrypt)
        {
            byte[] bKey = new byte[20];
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
                Byte[] inputByteArray = Convert.FromBase64String(strText.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }






        //public DbSet<ReportsMataDataClass> ReportsMataDatas { get; set; } // unable to create this table

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }


    }
}
