using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

namespace WorkShipVersionII
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ShipmentContaxt sc;
        public App()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            // sc.CreateLog("Login", "Login", null);
        }

        protected override void OnStartup(StartupEventArgs e)
        {


            CultureInfo culure = new CultureInfo("en-US");
            culure.DateTimeFormat.DateSeparator = "/";
            culure.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            culure.DateTimeFormat.LongDatePattern = "yyyy/MM/dd hh:mm:ss";
            Thread.CurrentThread.CurrentCulture = culure;
            Thread.CurrentThread.CurrentUICulture = culure;



            try
            {
                       //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                       // string anscript = AppDomain.CurrentDomain.BaseDirectory + "\\ws2script.sql";

                            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxtSVR"].ConnectionString);

                con1.Open();
                SqlCommand sqlCmd = new SqlCommand("SELECT name FROM sys.databases  where name = 'shipment5'", con1);
                using (SqlDataReader dr = sqlCmd.ExecuteReader())
                {
                    if (dr.Read())
                    {


                        SqlConnection con = sc.con;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlDataAdapter smd = new SqlDataAdapter("select * from AdminLogin", con);
                        DataTable dta = new DataTable();
                        smd.Fill(dta);
                        if (dta.Rows.Count > 0)
                        {
                            string lastdate = dta.Rows[0]["LastLogin"].ToString();
                            if (lastdate != "")
                            {
                                //Application.EnableVisualStyles();
                                //Application.SetCompatibleTextRenderingDefault(false);
                                //Application.Run(new LoginForm());
                            }
                            else
                            {

                                //Application.EnableVisualStyles();
                                //Application.SetCompatibleTextRenderingDefault(false);
                                //Application.Run(new WelcomeForm());
                            }

                        }
                        else
                        {

                            SqlDataAdapter smdk = new SqlDataAdapter("SELECT SERVERPROPERTY('ServerName') AS ServerName", con);
                            DataTable dtak = new DataTable();
                            smdk.Fill(dtak);
                            string servername = dtak.Rows[0]["ServerName"].ToString();


                            //Application.EnableVisualStyles();
                            //Application.SetCompatibleTextRenderingDefault(false);
                            //Application.Run(new WelcomeForm());
                        }
                        con.Close();


                    }
                    else
                    {
                        dr.Close();
                        dr.Dispose();
                        if (con1.State == ConnectionState.Closed)
                        {
                            con1.Open();
                        }

                        string pathbm1 = @"C:\Program Files (x86)\Microsoft SQL Server";
                        string script = "";

                        if (Directory.Exists(pathbm1) == true)
                        {

                            script = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\ws2script.sql");
                        }
                        else
                        {

                            script = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\ws2script.sql");

                        }

                        IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$",
                             RegexOptions.Multiline | RegexOptions.IgnoreCase);

                        if (con1.State == ConnectionState.Closed)
                        {
                            con1.Open();
                        }
                        foreach (string commandString in commandStrings)
                        {
                            if (commandString.Trim() != "")
                            {
                                using (var command = new SqlCommand(commandString, con1))
                                {
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        // con1.Close();

                        //Server server = new Server(new ServerConnection(con1));
                        //server.ConnectionContext.ExecuteNonQuery(script);

                        System.Threading.Thread.Sleep(200);
                        con1.Close();


                        //.............................................................

                        // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["newConnectionString"].ConnectionString);
                        SqlConnection con = sc.con;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlCommand cmd = new SqlCommand("select count(*) from AdminLogin", con);
                        int cn = Convert.ToInt32(cmd.ExecuteScalar());
                        if (cn >= 1)
                        {
                            //Application.EnableVisualStyles();
                            //Application.SetCompatibleTextRenderingDefault(false);
                            //Application.Run(new LoginForm());

                        }
                        else
                        {
                            //Application.EnableVisualStyles();
                            //Application.SetCompatibleTextRenderingDefault(false);
                            //Application.Run(new WelcomeForm());
                        }
                        con.Close();



                    }
                }



            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }



            //string path0 = @"C:\Work-Ship_DB_Backup";
            //if (!Directory.Exists(path0))
            //{
            //    Directory.CreateDirectory(path0);
            //}

            //var path = AppDomain.CurrentDomain.BaseDirectory + "shipment.sdf";
            //string path1 = path0 + "\\shipment.sdf";


            //if (!File.Exists(path1))
            //{

            //    File.Copy(path, path1);
            //}


        }

        private static void CreateIfNotExists(string fileName)
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Set the data directory to the users %AppData% folder 

            AppDomain.CurrentDomain.SetData("DataDirectory", path);

        }




        public void bbbs()
        {
            try
            {
                string bba = "";
                string bb = System.AppDomain.CurrentDomain.BaseDirectory;
                bb = bb.Substring(0, bb.Length - 10);
                bba = bb + "app_data";
                bb = bb + "app_data\\shipment.sdf";


                if (!File.Exists(bb))
                {
                    SqlCeConnection connection = new SqlCeConnection(@"server=(localdb)\v11.0");
                    using (connection)
                    {
                        connection.Open();

                        string sql = string.Format(@"
                                    CREATE DATABASE
                                        [shipment]
                                    ON PRIMARY (
                                       NAME=shipment,
                                       FILENAME = '{0}\shipment.sdf'
                                    )
                                    LOG ON (
                                        NAME=CommunityDBM1_log,
                                        FILENAME = '{0}\shipment.ldf'
                                    )",
                                          bba

                        );

                        SqlCeCommand command = new SqlCeCommand(sql, connection);
                        command.ExecuteNonQuery();


                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


    }
}
