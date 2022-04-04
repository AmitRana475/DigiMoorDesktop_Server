using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows;


namespace DataBuildingLayer
{
       public class ShipmentContaxt : DbContext, ILogBase
       {
        //public ShipmentContaxt() : base("ShipmentContaxt")
        public ShipmentContaxt() : base(new NetworkCredential("", ConnectionBulder.Dconstring).Password)
        {
                     // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ShipmentContaxt>());
                     //this.Configuration.AutoDetectChangesEnabled = false;
                     //this.Configuration.ValidateOnSaveEnabled = false;
                     // Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShipmentContaxt, DataBuildingLayer.Migrations.Configuration>());
              }

              public SqlConnection con = ConnectionBulder.con;

              public DbSet<AdminLoginClass> AdminLogins { get; set; }



              //public IList<MooringWinchClass> GetMooringWinchList(string searchCrew1)
              //{
              //    try
              //    {
              //        var data = MooringWinch.ToList();
              //        return data;
              //    }
              //    catch (Exception ex)
              //    {
              //        ErrorLog(ex);
              //        return null;
              //    }
              //}

        
              public DbSet<InfoLogClass> InfoLogs { get; set; }
            
              public DbSet<RuffCodeClass> RuffCodes { get; set; }
              public DbSet<versionClass> Versions { get; set; }
              public DbSet<VesselClass> Vessels { get; set; }
      
              public DbSet<RetardtblClass> Retardtbls { get; set; }
        public DbSet<RopeBasedCalculationsClass> RopeBasedcalculations { get; set; }
        public DbSet<LogbookClass> Logbooks { get; set; }
        public DbSet<InternationalClass> Internationals { get; set; }
        public DbSet<ReportsMataDataClass> ReportsMataDatas { get; set; }
              public DbSet<DocsPages> DocsPages { get; set; }
              public DbSet<MSMPMenus> MSMPMenus { get; set; }
              public DbSet<SmartMenu> SmartMenus { get; set; }
              public DbSet<SubMenu> SubMenus { get; set; }

              public DbSet<MOperationBirthDetail> MOperationBirthDetailTbl { get; set; }

              public DbSet<MOUsedWinchTbl> MOUsedWinchTbl { get; set; }
              // public DbSet<ShipSpecificAttachment> ShipSpecificAttachments { get; set; }

              public DbSet<ShipSpecificContent> ShipSpecificContents { get; set; }
              public DbSet<MooringWinchClass> MooringWinch { get; set; }
              public DbSet<MooringWinchRopeClass> MooringWinchRope { get; set; }
              public DbSet<AssignModuleToWinchClass> AssignRopetoWinch { get; set; }

              public DbSet<AssignLooseEquipTypeClass> AssignLooseEtoWinch { get; set; }
              public DbSet<RopeTypeClass> MooringRopeType { get; set; }
              public DbSet<OutofserviceReasonClass> OutofserviceReason { get; set; }
              public DbSet<DamageobservedClass> DamageObserved { get; set; }
              public DbSet<RopeEndtoEndClass> RopeEndtoEndTable { get; set; }
              public DbSet<RopeEndtoEnd2Class> RopeEndtoEnd2 { get; set; }

              public DbSet<MooringRopeInspectionClass> MooringRopeInspectionTbl { get; set; }
              public DbSet<CrossShiftingWinchClass> CrossShiftingWinches { get; set; }

              public DbSet<RopeSplicingClass> RopeSplicing { get; set; }
              public DbSet<RopeCroppingClass> RopeCropping { get; set; }
              public DbSet<RopeDamageRecordClass> RopeDamage { get; set; }

              public DbSet<ShipSpecificAttachment> ShipSpecificAttachments { get; set; }
              public DbSet<RevisionClass> Revisions { get; set; }
              public DbSet<LooseETypeClass> LooseETypes { get; set; }

        public DbSet<MinimumRopeAndTailsClass> MinimumRopeAndTails { get; set; }

        public DbSet<LooseEDamageRecordClass> LooseEDamageR { get; set; }
              public DbSet<JoiningShackleClass> JoiningShackles { get; set; }
              public DbSet<RopeTailClass> RopeTails { get; set; }
              public DbSet<ChainStopperClass> ChainStoppers { get; set; }

              public DbSet<NotificationsClass> Notifications { get; set; }

              public DbSet<NotificationCommentsClass> NotificationCommnent { get; set; }

              public DbSet<RopeInspectionSettingClass> RopeInspectionSetting { get; set; }

        public DbSet<WinchRotationSettingClass> WinchRotationSetting { get; set; }

        public DbSet<RopeTailInspectionSettingClass> RopeTailInspectionSetting { get; set; }

        public DbSet<TrainingAttachmentClass> TrainingAttachment { get; set; }

        public DbSet<LooseEquipInspectionSettingClass> LooseEInspectionSetting { get; set; }
              public DbSet<RopeDisposalClass> RopeDisposals { get; set; }
              public DbSet<LooseEDisposalClass> LooseEDisposals { get; set; }

              public DbSet<PortListClass> PortListtbl { get; set; }

              public DbSet<TblCommonClass> CommonManuF { get; set; }

              public DbSet<MenuNameClass> MenuNames { get; set; }

              public DbSet<ChafeGuardClass> ChafeGuard { get; set; }
              public DbSet<WBTestKitClass> WBTestKit { get; set; }
              public DbSet<MooringLooseEquipInspectionClass> LooseEquipInspection { get; set; }

              public DbSet<GeneralP> GeneralPs { get; set; }
              public DbSet<VesselP> VesselPs { get; set; }
              public DbSet<WindAreas> WindAreass { get; set; }
              public DbSet<WindandCurrent> WindandCurrents { get; set; }
              public DbSet<WindLoads> WindLoadss { get; set; }
              public DbSet<CurrentLoad> CurrentLoads { get; set; }
              public DbSet<MooringLines> MooringLiness { get; set; }
              public DbSet<WinchBrakeTestAttachmentClass> WinchBrakeTestAttachments { get; set; }
              public DbSet<ResidualLabTestClass> ResidualLabTestTbl { get; set; }


        public DataTable dynamicCheckMinimumRopeandTails()
        {
       
           
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
                SqlDataAdapter adp = new SqlDataAdapter("select * from MinimumRopeAndTailsSetting", con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                //ropemin = Convert.ToInt32(dt.Rows[0]["MinimumRopes"]);
                //tailmin = Convert.ToInt32(dt.Rows[0]["MinimumRopeTails"]);



         

            return dt;
        }

        public List<RuffCodeClass> GetLicencekeys()
        {
            try
            {
                List<RuffCodeClass> objHashTable = new List<RuffCodeClass>();
                // var data = RuffCodes.ToList();
                string qry = "select * from RuffCode";
                using (SqlDataAdapter sda = new SqlDataAdapter(qry, con))
                {
                    DataTable dtp = new DataTable();
                    sda.Fill(dtp);
                    if (dtp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtp.Rows.Count; i++)
                        {
                            objHashTable.Add(new RuffCodeClass { keyno = Decrypt(dtp.Rows[i]["keyno"].ToString(), "KKPrajapat"), keycode = Decrypt(dtp.Rows[i]["keycode"].ToString(), "KKPrajapat") });
                        }
                    }
                }


                return objHashTable;
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }



              public List<MooringWinchRopeClass> GetMooringWinchRope(string searchCrew)
              {
                     try
                     {

                            List<MooringWinchRopeClass> LeftJoinData = new List<MooringWinchRopeClass>();

                            string MYSEARCH = "%" + searchCrew + "%";

                            using (SqlDataAdapter sda = new SqlDataAdapter("GetMooringRopeDetailListSearch", con))
                            {

                                   sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                                   sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                                   sda.SelectCommand.Parameters.AddWithValue("@Searchtext", searchCrew);
                                   DataTable dt = new DataTable();
                                   sda.Fill(dt);
                                   if (dt.Rows.Count > 0)
                                   {
                                          for (int i = 0; i < dt.Rows.Count; i++)
                                          {
                                                 var MooringWinchRopeData = new MooringWinchRopeClass()
                                                 {
                                                        Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                                                        RopeTail = 0,
                                                        CertificateNumber = dt.Rows[i]["CertificateNumber"].ToString(),
                                                        UniqueID = (dt.Rows[i]["UniqueID"] == DBNull.Value) ? "" : dt.Rows[i]["UniqueID"].ToString(),
                                                        LDBF = Convert.ToDecimal(dt.Rows[i]["LDBF"]),
                                                        DiaMeter = Convert.ToDecimal(dt.Rows[i]["DiaMeter"]),
                                                        Length = Convert.ToDecimal(dt.Rows[i]["Length"]),
                                                        WLL = Convert.ToDecimal(dt.Rows[i]["WLL"]),
                                                        ReceivedDate = Convert.ToDateTime(dt.Rows[i]["ReceivedDate"]),
                                                        RopeTagging = dt.Rows[i]["RopeTagging"].ToString(),
                                                        RopeConstruction = dt.Rows[i]["RopeConstruction"].ToString(),
                                                        RopeType = dt.Rows[i]["RopeType"].ToString(),
                                                        RopeTypeId = Convert.ToInt32(dt.Rows[i]["RopeTypeId"]),
                                                        ManufacturerId = Convert.ToInt32(dt.Rows[i]["ManufacturerId"]),
                                                        InstalledDate1 = (dt.Rows[i]["InstalledDate1"] == DBNull.Value) ? null : dt.Rows[i]["InstalledDate1"].ToString(),
                                                        InstalledDate = Convert.ToDateTime((dt.Rows[i]["InstalledDate"] == DBNull.Value) ? DateTime.Now : dt.Rows[i]["InstalledDate"]),
                                                        CurrentRunningHours = Convert.ToDecimal(dt.Rows[i]["CurrentRunningHours"] == DBNull.Value ? null : dt.Rows[i]["CurrentRunningHours"]),
                                                        StartCounterHours = Convert.ToDecimal(dt.Rows[i]["StartCounterHours"] == DBNull.Value ? null : dt.Rows[i]["StartCounterHours"]),
                                                        Remarks = (dt.Rows[i]["Remarks"] == DBNull.Value) ? string.Empty : dt.Rows[i]["Remarks"].ToString(),
                                                        ManufacturerName = (dt.Rows[i]["ManufacturerName"] == DBNull.Value) ? string.Empty : dt.Rows[i]["ManufacturerName"].ToString(),
                                                        Location = (dt.Rows[i]["Location"] == DBNull.Value) ? "Not Assigned" : dt.Rows[i]["Location"].ToString(),
                                                        AssignedWinch = (dt.Rows[i]["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : dt.Rows[i]["AssignedWinch"].ToString(),
                                                 };

                                                 LeftJoinData.Add(MooringWinchRopeData);
                                          }

                                   }
                            }

                            return LeftJoinData;

                     }
                     catch (Exception ex)
                     {
                            ErrorLog(ex);
                            return null;
                     }
              }




              

              public List<MooringWinchRopeClass> GetMooringWinchRope1(string searchCrew1)
              {
                     try
                     {

                            List<MooringWinchRopeClass> LeftJoinData = new List<MooringWinchRopeClass>();

                            string MYSEARCH = "%" + searchCrew1 + "%";

                            using (SqlDataAdapter sda = new SqlDataAdapter("GetMooringRopeDetailListSearch", con))
                            {

                                   sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                                   sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
                                   sda.SelectCommand.Parameters.AddWithValue("@Searchtext", searchCrew1);
                                   DataSet dt = new DataSet();
                                   sda.Fill(dt);
                                   if (dt.Tables[1].Rows.Count > 0)
                                   {
                                          for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                                          {
                                                 var MooringWinchRopeData = new MooringWinchRopeClass()
                                                 {
                                                        Id = Convert.ToInt32(dt.Tables[1].Rows[i]["Id"]),
                                                        RopeTail = 0,
                                                        CertificateNumber = dt.Tables[1].Rows[i]["CertificateNumber"].ToString(),
                                                        UniqueID = (dt.Tables[1].Rows[i]["UniqueID"] == DBNull.Value) ? "" : dt.Tables[1].Rows[i]["UniqueID"].ToString(),
                                                        LDBF = Convert.ToDecimal(dt.Tables[1].Rows[i]["LDBF"]),
                                                        DiaMeter = Convert.ToDecimal(dt.Tables[1].Rows[i]["DiaMeter"]),
                                                        Length = Convert.ToDecimal(dt.Tables[1].Rows[i]["Length"]),
                                                        WLL = Convert.ToDecimal(dt.Tables[1].Rows[i]["WLL"]),
                                                        ReceivedDate = Convert.ToDateTime(dt.Tables[1].Rows[i]["ReceivedDate"]),
                                                        RopeTagging = dt.Tables[1].Rows[i]["RopeTagging"].ToString(),
                                                        RopeConstruction = dt.Tables[1].Rows[i]["RopeConstruction"].ToString(),
                                                        RopeType = dt.Tables[1].Rows[i]["RopeType"].ToString(),
                                                        RopeTypeId = Convert.ToInt32(dt.Tables[1].Rows[i]["RopeTypeId"]),
                                                        ManufacturerId = Convert.ToInt32(dt.Tables[1].Rows[i]["ManufacturerId"]),
                                                        InstalledDate1 = (dt.Tables[1].Rows[i]["InstalledDate1"] == DBNull.Value) ? null : dt.Tables[1].Rows[i]["InstalledDate1"].ToString(),
                                                        InstalledDate = Convert.ToDateTime((dt.Tables[1].Rows[i]["InstalledDate"] == DBNull.Value) ? DateTime.Now : dt.Tables[1].Rows[i]["InstalledDate"]),
                                                        CurrentRunningHours = Convert.ToDecimal(dt.Tables[1].Rows[i]["CurrentRunningHours"] == DBNull.Value ? null : dt.Tables[1].Rows[i]["CurrentRunningHours"]),
                                                        StartCounterHours = Convert.ToDecimal(dt.Tables[1].Rows[i]["StartCounterHours"] == DBNull.Value ? null : dt.Tables[1].Rows[i]["StartCounterHours"]),
                                                        Remarks = (dt.Tables[1].Rows[i]["Remarks"] == DBNull.Value) ? string.Empty : dt.Tables[1].Rows[i]["Remarks"].ToString(),
                                                        ManufacturerName = (dt.Tables[1].Rows[i]["ManufacturerName"] == DBNull.Value) ? string.Empty : dt.Tables[1].Rows[i]["ManufacturerName"].ToString(),
                                                        Location = (dt.Tables[1].Rows[i]["Location"] == DBNull.Value) ? "Not Assigned" : dt.Tables[1].Rows[i]["Location"].ToString(),
                                                        AssignedWinch = (dt.Tables[1].Rows[i]["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : dt.Tables[1].Rows[i]["AssignedWinch"].ToString(),
                                                 };

                                                 LeftJoinData.Add(MooringWinchRopeData);
                                          }

                                   }
                            }

                            return LeftJoinData;

                     }
                     catch (Exception ex)
                     {
                            ErrorLog(ex);
                            return null;
                     }
              }



 public List<MooringWinchRopeClass> GetMooringWinchRopeTail(string searchCrew)
              {
                     try
                     {

                            List<MooringWinchRopeClass> LeftJoinData = new List<MooringWinchRopeClass>();

                            string MYSEARCH = "%" + searchCrew + "%";

                            using (SqlDataAdapter sda = new SqlDataAdapter("GetMooringRopeDetailListSearch", con))
                            {

                                   sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                                   sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 1);
                                   sda.SelectCommand.Parameters.AddWithValue("@Searchtext", searchCrew);
                                   DataTable dt = new DataTable();
                                   sda.Fill(dt);
                                   if (dt.Rows.Count > 0)
                                   {
                                          for (int i = 0; i < dt.Rows.Count; i++)
                                          {
                                                 var MooringWinchRopeData = new MooringWinchRopeClass()
                                                 {
                                                        Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                                                        RopeTail = 0,
                                                        CertificateNumber = dt.Rows[i]["CertificateNumber"].ToString(),
                                                        UniqueID = (dt.Rows[i]["UniqueID"] == DBNull.Value) ? "" : dt.Rows[i]["UniqueID"].ToString(),
                                                        LDBF = Convert.ToDecimal(dt.Rows[i]["LDBF"]),
                                                        DiaMeter = Convert.ToDecimal(dt.Rows[i]["DiaMeter"]),
                                                        Length = Convert.ToDecimal(dt.Rows[i]["Length"]),
                                                        WLL = Convert.ToDecimal(dt.Rows[i]["WLL"]),
                                                        ReceivedDate = Convert.ToDateTime(dt.Rows[i]["ReceivedDate"]),
                                                        RopeTagging = dt.Rows[i]["RopeTagging"].ToString(),
                                                        RopeConstruction = dt.Rows[i]["RopeConstruction"].ToString(),
                                                        RopeType = dt.Rows[i]["RopeType"].ToString(),
                                                        RopeTypeId = Convert.ToInt32(dt.Rows[i]["RopeTypeId"]),
                                                        ManufacturerId = Convert.ToInt32(dt.Rows[i]["ManufacturerId"]),
                                                        InstalledDate1 = (dt.Rows[i]["InstalledDate1"] == DBNull.Value) ? null : dt.Rows[i]["InstalledDate1"].ToString(),
                                                        InstalledDate = Convert.ToDateTime((dt.Rows[i]["InstalledDate"] == DBNull.Value) ? DateTime.Now : dt.Rows[i]["InstalledDate"]),
                                                        CurrentRunningHours = Convert.ToDecimal(dt.Rows[i]["CurrentRunningHours"] == DBNull.Value ? null : dt.Rows[i]["CurrentRunningHours"]),
                                                        StartCounterHours = Convert.ToDecimal(dt.Rows[i]["StartCounterHours"] == DBNull.Value ? null : dt.Rows[i]["StartCounterHours"]),
                                                        Remarks = (dt.Rows[i]["Remarks"] == DBNull.Value) ? string.Empty : dt.Rows[i]["Remarks"].ToString(),
                                                        ManufacturerName = (dt.Rows[i]["ManufacturerName"] == DBNull.Value) ? string.Empty : dt.Rows[i]["ManufacturerName"].ToString(),
                                                        Location = (dt.Rows[i]["Location"] == DBNull.Value) ? "Not Assigned" : dt.Rows[i]["Location"].ToString(),
                                                        AssignedWinch = (dt.Rows[i]["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : dt.Rows[i]["AssignedWinch"].ToString(),
                                                 };

                                                 LeftJoinData.Add(MooringWinchRopeData);
                                          }

                                   }
                            }

                            return LeftJoinData;

                     }
                     catch (Exception ex)
                     {
                            ErrorLog(ex);
                            return null;
                     }
              }

     public List<MooringWinchRopeClass> GetMooringWinchRopeTail1(string searchCrew1)
        {
            try
            {

                List<MooringWinchRopeClass> LeftJoinData = new List<MooringWinchRopeClass>();

                string MYSEARCH = "%" + searchCrew1 + "%";

                using (SqlDataAdapter sda = new SqlDataAdapter("GetMooringRopeDetailListSearch", con))
                {

                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 1);
                    sda.SelectCommand.Parameters.AddWithValue("@Searchtext", searchCrew1);
                    DataSet dt = new DataSet();
                    sda.Fill(dt);
                    if (dt.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                        {
                            var MooringWinchRopeData = new MooringWinchRopeClass()
                            {
                                Id = Convert.ToInt32(dt.Tables[1].Rows[i]["Id"]),
                                RopeTail = 0,
                                CertificateNumber = dt.Tables[1].Rows[i]["CertificateNumber"].ToString(),
                                UniqueID = (dt.Tables[1].Rows[i]["UniqueID"] == DBNull.Value) ? "" : dt.Tables[1].Rows[i]["UniqueID"].ToString(),
                                LDBF = Convert.ToDecimal(dt.Tables[1].Rows[i]["LDBF"]),
                                DiaMeter = Convert.ToDecimal(dt.Tables[1].Rows[i]["DiaMeter"]),
                                Length = Convert.ToDecimal(dt.Tables[1].Rows[i]["Length"]),
                                WLL = Convert.ToDecimal(dt.Tables[1].Rows[i]["WLL"]),
                                ReceivedDate = Convert.ToDateTime(dt.Tables[1].Rows[i]["ReceivedDate"]),
                                RopeTagging = dt.Tables[1].Rows[i]["RopeTagging"].ToString(),
                                RopeConstruction = dt.Tables[1].Rows[i]["RopeConstruction"].ToString(),
                                RopeType = dt.Tables[1].Rows[i]["RopeType"].ToString(),
                                RopeTypeId = Convert.ToInt32(dt.Tables[1].Rows[i]["RopeTypeId"]),
                                ManufacturerId = Convert.ToInt32(dt.Tables[1].Rows[i]["ManufacturerId"]),
                                InstalledDate1 = (dt.Tables[1].Rows[i]["InstalledDate1"] == DBNull.Value) ? null : dt.Tables[1].Rows[i]["InstalledDate1"].ToString(),
                                InstalledDate = Convert.ToDateTime((dt.Tables[1].Rows[i]["InstalledDate"] == DBNull.Value) ? DateTime.Now : dt.Tables[1].Rows[i]["InstalledDate"]),
                                CurrentRunningHours = Convert.ToDecimal(dt.Tables[1].Rows[i]["CurrentRunningHours"] == DBNull.Value ? null : dt.Tables[1].Rows[i]["CurrentRunningHours"]),
                                StartCounterHours = Convert.ToDecimal(dt.Tables[1].Rows[i]["StartCounterHours"] == DBNull.Value ? null : dt.Tables[1].Rows[i]["StartCounterHours"]),
                                Remarks = (dt.Tables[1].Rows[i]["Remarks"] == DBNull.Value) ? string.Empty : dt.Tables[1].Rows[i]["Remarks"].ToString(),
                                ManufacturerName = (dt.Tables[1].Rows[i]["ManufacturerName"] == DBNull.Value) ? string.Empty : dt.Tables[1].Rows[i]["ManufacturerName"].ToString(),
                                Location = (dt.Tables[1].Rows[i]["Location"] == DBNull.Value) ? "Not Assigned" : dt.Tables[1].Rows[i]["Location"].ToString(),
                                AssignedWinch = (dt.Tables[1].Rows[i]["AssignedWinch"] == DBNull.Value) ? "Not Assigned" : dt.Tables[1].Rows[i]["AssignedWinch"].ToString(),
                            };

                            LeftJoinData.Add(MooringWinchRopeData);
                        }

                    }
                }

                return LeftJoinData;

            }
            catch (Exception ex)
            {
                ErrorLog(ex);
                return null;
            }
        }



        public string DateToSQLFormat(DateTime Date)
        {
            return Date.ToString("yyyy-MM-dd");
        }

             
              private List<MooringWinchClass> GetMooringWinchList(string searchCrew)
              {
                     //get
                     //{
                     try
                     {
                            var data = MooringWinch.ToList();
                            return data;
                     }
                     catch (Exception ex)
                     {
                            ErrorLog(ex);
                            return null;
                     }
                     // }
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


             
                     string path0 = @"C:\DigiMoorDB_Backup";
                     if (!Directory.Exists(path0))
                     {
                            Directory.CreateDirectory(path0);
                     }

                     string path1 = @"\Systemlogfile.txt";
                     string path2 = path0 + path1;
                     if (!File.Exists(path2))
                     {

                            File.WriteAllText("C:\\DigiMoorDB_Backup" + "\\Systemlogfile.txt", traceString.ToString());

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


              public bool DecimalCheck2Digit(int check)
              {

                     if (check > 99)
                     {
                            MessageBox.Show("Values can not grater then 99.99", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return false;
                     }
                     else
                     {
                            return true;
                     }
              }

          
              public int NextNotiId()
              {
                     int notiid = 0;
                     try
                     {
                            SqlDataAdapter adp = new SqlDataAdapter("SELECT IDENT_CURRENT('Notifications') + IDENT_INCR('Notifications') as Notiid", con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            notiid = Convert.ToInt32(dt.Rows[0][0]);

                            return notiid;

                     }
                     catch { return notiid; }

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


              public bool DecimalCheck(int check)
              {

                     if (check > 999)
                     {
                            MessageBox.Show("Values can not greater then 999.99", "Character length", MessageBoxButton.OK, MessageBoxImage.Warning);
                            // MessageBox.Show("You can not Enter Max 3 Digit", "Charcarter Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return false;
                     }
                     else
                     {
                            return true;
                     }
              }

        public bool DecimalCheck1(decimal check)
        {

            if (check > 999)
            {
                MessageBox.Show("Values can not greater then 999.99", "Character length", MessageBoxButton.OK, MessageBoxImage.Warning);
                // MessageBox.Show("You can not Enter Max 3 Digit", "Charcarter Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool DecimalCheckInspection(int check)
        {

            if (check > 99)
            {
                MessageBox.Show("Values can not greater then 99.99", "Charcarter Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                // MessageBox.Show("You can not Enter Max 3 Digit", "Charcarter Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }


        //public DbSet<ReportsMataDataClass> ReportsMataDatas { get; set; } // unable to create this table

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
              {
                     //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

              }


       }
}
