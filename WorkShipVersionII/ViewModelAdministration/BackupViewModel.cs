using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

namespace WorkShipVersionII.ViewModelAdministration
{
       public class BackupViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              private SqlConnection con;
              private SqlConnection con1;

              private BackgroundWorker _Worker;
              public ICommand HelpCommand { get; private set; }

              public BackupViewModel()
              {
                     if (sc == null)
                     {
                            sc = new ShipmentContaxt();
                            sc.Configuration.ProxyCreationEnabled = false;
                            con = sc.con;
                //string getcon = ConHelper.ConString.Replace("Initial Catalog=DigiMoorDB_V2;", "");
                //con1 = new SqlConnection(getcon);
                 con1 = new SqlConnection(new NetworkCredential("", ConnectionBulder.NoDBCon).Password);
                //con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxtSVR"].ConnectionString);

                     }
                   

                     HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                     DispatcherTimer timer = new DispatcherTimer();
                     timer.Interval = TimeSpan.FromHours(24);
                     timer.Tick += timer_Tick;
                     timer.Start();


                     //createCommand = new RelayCommand(CreateDatabase);
                     deleteCommand = new RelayCommand<AutoBackup>(DeleteDatabase);
                     restoreCommand = new RelayCommand<AutoBackup>(RestoreDatabase);
                     saveAsCommand = new RelayCommand<AutoBackup>(SaveAsDatabase);

                     createCommand = new RelayCommand(() => _Worker.RunWorkerAsync(), () => !_Worker.IsBusy);
                     _Worker = new BackgroundWorker();
                     _Worker.WorkerReportsProgress = true;
                     _Worker.DoWork += CreateDatabase;
                     _Worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
                     _Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CreateBakupCompleted);

                     loadBackupFiles = GetBackupFile();

                     DeleteAutomatically_Folder();


              }

              private void DeleteAutomatically_Folder()
              {
                     try
                     {


                            int count = 0;
                            foreach (var item in loadBackupFiles.Where(x => x.BackupType == "Auto").ToList())
                            {
                                   count++;
                                   if (count > 10)
                                   {
                                          string ServerName = StaticHelper.ServerName;
                                          string path01 = ServerName + "\\DigiMoorDB_Backup";
                                          //string path01 = @"C:\DigiMoorDB_Backup";
                                          if (!Directory.Exists(path01))
                                          {
                                                 Directory.CreateDirectory(path01);
                                          }

                                          string path1 = @"\DBbackup2.xml";
                                          string path2 = path01 + path1;
                                          if (!File.Exists(path2))
                                          {

                                                 // dvgBackup.DataSource = null;
                                          }
                                          else
                                          {

                                                 var location = item.Location;
                                                 location = location.Replace(@"\DigiMoorDB_V2.bak", "");

                                                 var NetworkDry = location.Replace("C:", ServerName);
                                                 //string[] locy = null;
                                                 //locy = location.Split('\\');

                                                 //string dry = "";
                                                 //dry = locy[0] + "\\" + locy[1] + "\\" + locy[2];
                                                 if (Directory.Exists(NetworkDry))
                                                 {
                                                        // File.Delete(location);
                                                        Directory.Delete(NetworkDry, true);
                                                 }

                                                 XmlDocument xdoc = new XmlDocument();
                                                 xdoc.Load(path2);

                                                 foreach (XmlNode node in xdoc.SelectNodes("BackupListing/BackupList"))
                                                 {
                                                        if (node.SelectSingleNode("Location").InnerText == item.Location)
                                                        {
                                                               node.ParentNode.RemoveChild(node);
                                                        }
                                                 }

                                                 xdoc.Save(path2);



                                                 var bkupfile = xdoc.SelectNodes("BackupListing/BackupList").Count;
                                                 if (bkupfile == 0)
                                                 {

                                                        loadBackupFiles.Clear();
                                                        RaisePropertyChanged("LoadBackupFiles");
                                                        File.Delete(path2);

                                                 }
                                                 else
                                                 {
                                                        loadBackupFiles = GetBackupFile();
                                                        RaisePropertyChanged("LoadBackupFiles");
                                                 }
                                          }

                                   }
                            }
                            //List<string> DeletePath = new List<string>();
                            //DirectoryInfo info = new DirectoryInfo(@"C:\DigiMoorDB_Backup\");
                            //FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
                            //foreach (FileInfo file in files)
                            //{
                            //    DateTime CreationTime = file.CreationTime;
                            //    double days = (DateTime.Now - CreationTime).TotalDays;
                            //    if (days > 16)
                            //    {
                            //        string delFullPath = file.DirectoryName + "\\" + file.Name;
                            //        DeletePath.Add(delFullPath);
                            //    }
                            //}
                            //foreach (var f in DeletePath)
                            //{
                            //    if (File.Exists(f))
                            //    {
                            //        File.Delete(f);
                            //    }
                            //}


                     }
                     catch (Exception)
                     { }
              }
              private void timer_Tick(object sender, EventArgs e)
              {
                     CreateAutoBackup();
              }

              public void CreateAutoBackup()
              {
                     try
                     {
                            GetBackupFile1();



                            //Create Backup from SQL DB
                            //string dtm = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");

                            string dtm = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");


                            string ServerName = StaticHelper.ServerName;
                            string path0 = ServerName + "\\DigiMoorDB_Backup\\" + dtm;
                            //string path0 = @"C:\DigiMoorDB_Backup\" + dtm; // Application.StartupPath + "\\DBBackup";
                            if (!Directory.Exists(path0))
                            {
                                   Directory.CreateDirectory(path0);
                            }


                            //DirectoryInfo d = new DirectoryInfo(@"C:\DigiMoorDB_Backup\");
                            //if (d.CreationTime < DateTime.Now.AddDays(-1))
                            //{
                            //    d.Delete();
                            //}

                            //DirectoryInfo source = new DirectoryInfo(@"C:\DigiMoorDB_Backup\");

                            //// Get info of each file into the directory
                            //foreach (FileInfo fi in source.GetFiles())
                            //{
                            //    var creationTime = fi.CreationTime;

                            //    if (creationTime < (DateTime.Now - new TimeSpan(1, 0, 0, 0)))
                            //    {
                            //        fi.Delete();
                            //    }
                            //}

                            string path;
                            //...........
                            int ib = 10;
                            // _Worker.ReportProgress(ib);
                            // PVisible = "Visible";
                            RaisePropertyChanged("CurrentProgress");
                            //...............


                            if (con.State == ConnectionState.Closed)
                            {
                                   con.Open();
                            }
                            path = path0 + "\\" + "DigiMoorDB_V2.bak";
                            if (File.Exists(path))
                            {

                                   // _Worker.ReportProgress(ib + 5);
                                   File.Delete(path);
                                   SqlCommand cmd = new SqlCommand();
                                   SqlDataReader reader;
                                   cmd.CommandText = "BACKUP DATABASE DigiMoorDB_V2 TO DISK= '" + path + "'";
                                   cmd.CommandType = CommandType.Text;
                                   //progressBar1.Value = 3;
                                   cmd.Connection = con;
                                   reader = cmd.ExecuteReader();
                                   con.Close();
                                   // _Worker.ReportProgress(ib + 5);
                                   System.Threading.Thread.Sleep(2000);
                                   // progressBar1.Value = 8;

                                   // progressBar1.Value = 10;
                                   // _Worker.ReportProgress(ib + 5);
                                   RaisePropertyChanged("CurrentProgress");
                                   DateTime ddtm = DateTime.Now;
                                   //string strbm = String.Format("{0:MM/dd/yyyy}", ddtm);

                                   // string strbm = String.Format("{0:dd/MM/yyyy}", ddtm);

                                   string strbm = String.Format("{0:yyyy-MM-dd}", ddtm);

                                   //string ServerName = StaticHelper.ServerName;
                                   string path01 = ServerName + "\\DigiMoorDB_Backup";
                                   //string path01 = @"C:\DigiMoorDB_Backup";
                                   if (!Directory.Exists(path01))
                                   {
                                          Directory.CreateDirectory(path01);
                                   }

                                   string path1 = @"\DBbackup2.xml";
                                   string path2 = path01 + path1;
                                   if (!File.Exists(path2))
                                   {


                                          XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                                          xmlWriterSettings.Indent = true;
                                          xmlWriterSettings.NewLineOnAttributes = true;
                                          using (XmlWriter xmlWriter = XmlWriter.Create(path2, xmlWriterSettings))
                                          {
                                                 xmlWriter.WriteStartDocument();
                                                 xmlWriter.WriteStartElement("BackupListing");

                                                 xmlWriter.WriteStartElement("BackupList");
                                                 xmlWriter.WriteElementString("Dates", strbm.ToString());
                                                 //xmlWriter.WriteElementString("Times", DateTime.Now.ToString("hh:mm tt"));
                                                 xmlWriter.WriteElementString("Times", DateTime.Now.ToString("HH:mm:ss"));
                                                 xmlWriter.WriteElementString("FileNames", "DigiMoorDB_V2.bak");

                                                 string svPath = path.Replace(ServerName, "C:");

                                                 xmlWriter.WriteElementString("Location", svPath);
                                                 //xmlWriter.WriteElementString("Location", path);
                                                 xmlWriter.WriteElementString("BackupType", "Auto");

                                                 xmlWriter.WriteEndElement();
                                                 xmlWriter.WriteEndElement();
                                                 xmlWriter.WriteEndDocument();
                                                 xmlWriter.Flush();
                                                 xmlWriter.Close();
                                          }



                                   }
                                   else
                                   {
                                          //string timenow = DateTime.Now.ToString("hh:mm tt");
                                          string timenow = DateTime.Now.ToString("HH:mm:ss");
                                          XDocument xDocument = XDocument.Load(path2);
                                          XElement root = xDocument.Element("BackupListing");
                                          IEnumerable<XElement> rows = root.Descendants("BackupList");
                                          XElement firstRow = rows.First();
                                          string svPath = path.Replace(ServerName, "C:");
                                          firstRow.AddBeforeSelf(new XElement("BackupList", new XElement("Dates", strbm.ToString()), new XElement("Times", timenow), new XElement("FileNames", "DigiMoorDB_V2.bak"), new XElement("Location", svPath), new XElement("BackupType", "Auto")));

                                          xDocument.Save(path2);



                                   }

                                   //_Worker.ReportProgress(ib + 5);

                                   RaisePropertyChanged("CurrentProgress");
                                   //string st1 = "insert into InfoLog (dt,UserName,ModuleName,ActionName,Description) values (@dt,@name,@module,@action,@desc)";
                                   //SqlDataAdapter sdalog = new SqlDataAdapter(st1, con);
                                   //sdalog.SelectCommand.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                                   //sdalog.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = Class1.UserTypes;
                                   //sdalog.SelectCommand.Parameters.Add("@module", SqlDbType.VarChar, 500).Value = "Database/Resotre";
                                   //sdalog.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar, -1).Value = "Create Backup";
                                   //sdalog.SelectCommand.Parameters.Add("@desc", SqlDbType.VarChar, -1).Value = Class1.AccessUserName;
                                   //DataTable dtlog = new DataTable();
                                   //sdalog.Fill(dtlog);


                                   MessageBox.Show("Complete system backup created successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                   // _Worker.ReportProgress(ib + 5);
                                   RaisePropertyChanged("CurrentProgress");
                                   SqlCommand cmd = new SqlCommand();
                                   SqlDataReader reader;
                                   // progressBar1.Value = 3;
                                   cmd.CommandText = "BACKUP DATABASE DigiMoorDB_V2 TO DISK= '" + path + "'";
                                   cmd.CommandType = CommandType.Text;
                                   cmd.Connection = con;
                                   reader = cmd.ExecuteReader();
                                   con.Close();
                                   // _Worker.ReportProgress(ib + 5);
                                   System.Threading.Thread.Sleep(2000);
                                   // progressBar1.Value = 8;

                                   // _Worker.ReportProgress(ib + 5);
                                   RaisePropertyChanged("CurrentProgress");

                                   DateTime ddtm = DateTime.Now;
                                   //string strbm = String.Format("{0:MM/dd/yyyy}", ddtm);
                                   //string strbm = String.Format("{0:dd/MM/yyyy}", ddtm);

                                   string strbm = String.Format("{0:yyyy-MM-dd}", ddtm);

                                   string path01 = ServerName + "\\DigiMoorDB_Backup";
                                   //string path01 = @"C:\DigiMoorDB_Backup";

                                   if (!Directory.Exists(path01))
                                   {
                                          Directory.CreateDirectory(path01);
                                   }

                                   string path1 = @"\DBbackup2.xml";
                                   string path2 = path01 + path1;
                                   if (!File.Exists(path2))
                                   {


                                          XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                                          xmlWriterSettings.Indent = true;
                                          xmlWriterSettings.NewLineOnAttributes = true;
                                          using (XmlWriter xmlWriter = XmlWriter.Create(path2, xmlWriterSettings))
                                          {
                                                 xmlWriter.WriteStartDocument();
                                                 xmlWriter.WriteStartElement("BackupListing");

                                                 xmlWriter.WriteStartElement("BackupList");
                                                 xmlWriter.WriteElementString("Dates", strbm.ToString());
                                                 //xmlWriter.WriteElementString("Times", DateTime.Now.ToString("hh:mm tt"));
                                                 xmlWriter.WriteElementString("Times", DateTime.Now.ToString("HH:mm:ss"));
                                                 xmlWriter.WriteElementString("FileNames", "DigiMoorDB_V2.bak");

                                                 string svPath = path.Replace(ServerName, "C:");
                                                 xmlWriter.WriteElementString("Location", svPath);
                                                 xmlWriter.WriteElementString("BackupType", "Auto");

                                                 xmlWriter.WriteEndElement();
                                                 xmlWriter.WriteEndElement();
                                                 xmlWriter.WriteEndDocument();
                                                 xmlWriter.Flush();
                                                 xmlWriter.Close();
                                          }



                                   }
                                   else
                                   {
                                          //string timenow = DateTime.Now.ToString("hh:mm tt");
                                          string timenow = DateTime.Now.ToString("HH:mm:ss");
                                          XDocument xDocument = XDocument.Load(path2);

                                          XElement root = xDocument.Element("BackupListing");
                                          IEnumerable<XElement> rows = root.Descendants("BackupList");
                                          XElement firstRow = rows.First();
                                          string svPath = path.Replace(ServerName, "C:");

                                          firstRow.AddBeforeSelf(new XElement("BackupList", new XElement("Dates", strbm.ToString()), new XElement("Times", timenow), new XElement("FileNames", "DigiMoorDB_V2.bak"), new XElement("Location", svPath), new XElement("BackupType", "Auto")));
                                          xDocument.Save(path2);



                                   }

                                   // _Worker.ReportProgress(ib + 5);
                                   RaisePropertyChanged("CurrentProgress");

                                   loadBackupFiles = GetBackupFile();
                                   RaisePropertyChanged("LoadBackupFiles");

                                   DeleteAutomatically_Folder();

                            }


                     }

                     catch (Exception ex)
                     {

                            //ErrorLogClass obj = new ErrorLogClass();
                            //obj.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                            //MessageBox.Show("System Error: Please contact support team", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                     }
                     finally
                     {
                            // progressBar1.Visible = false;

                            con.Close();

                     }
              }

              private ICommand createCommand;
              public ICommand CreateCommand
              {
                     get
                     {
                            return createCommand;
                     }

              }

              private ICommand deleteCommand;
              public ICommand DeleteCommand
              {
                     get
                     {
                            return deleteCommand;
                     }

              }
              private ICommand restoreCommand;
              public ICommand RestoreCommand
              {
                     get
                     {
                            return restoreCommand;
                     }

              }
              private ICommand saveAsCommand;
              public ICommand SaveAsCommand
              {
                     get
                     {
                            return saveAsCommand;
                     }

              }

              private string pVisible;
              public string PVisible
              {
                     get
                     {
                            if (pVisible == null)
                            {
                                   PVisible = "Collapsed";
                            }
                            return pVisible;
                     }
                     set
                     {
                            pVisible = value;
                            RaisePropertyChanged("PVisible");
                     }
              }

              private string rVisible;
              public string RVisible
              {
                     get
                     {
                            if (rVisible == null)
                            {
                                   RVisible = "Collapsed";
                            }
                            return rVisible;
                     }
                     set
                     {
                            rVisible = value;
                            RaisePropertyChanged("RVisible");
                     }
              }

              private double _CurrentProgress;
              public double CurrentProgress
              {
                     get
                     {

                            return _CurrentProgress;
                     }
                     set
                     {
                            _CurrentProgress = value;
                            RaisePropertyChanged("CurrentProgress");
                     }
              }
              private void CreateBakupCompleted(object sender, RunWorkerCompletedEventArgs e)
              {

                     MessageBox.Show("Complete system backup created successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                     PVisible = "Collapsed";
                     sc.CreateLog("Backup / Resotre", "Create Backup", null);
              }
              private void ProgressChanged(object sender, ProgressChangedEventArgs e)
              {
                     _CurrentProgress = e.ProgressPercentage;
                     RaisePropertyChanged("CurrentProgress");
              }

              public class AutoBackup
              {
                     public string Dates { get; set; }
                     public string Times { get; set; }
                     public string FileNames { get; set; }
                     public string Location { get; set; }
                     public string BackupType { get; set; }
                     public string Visibiles { get; set; }

                     // Visibility="Hidden"

              }

              public static List<AutoBackup> autobkup = new List<AutoBackup>();
              private ObservableCollection<AutoBackup> GetBackupFile()
              {
                     var filelist = new ObservableCollection<AutoBackup>();
                     try
                     {
                            string ServerName = StaticHelper.ServerName;
                            string path01 = ServerName + "\\DigiMoorDB_Backup";


                            if (!Directory.Exists(path01))
                            {
                                   Directory.CreateDirectory(path01);
                            }

                            string path1 = @"\DBbackup2.xml";
                            string path2 = path01 + path1;
                            if (File.Exists(path2))
                            {
                                   //XmlReader xmfile;
                                   using (XmlReader xmfile = XmlReader.Create(path2))
                                   {
                                          //while (xmfile.Read())
                                          //{
                                          //    if ((xmfile.NodeType == XmlNodeType.Element) && (xmfile.Name == "Cube"))
                                          //    {
                                          //        if (xmfile.HasAttributes)
                                          //            Console.WriteLine(xmlReader.GetAttribute("currency") + ": " + xmlReader.GetAttribute("rate"));
                                          //    }
                                          //}

                                          // xmfile = XmlReader.Create(path2);
                                          DataSet dts = new DataSet();
                                          dts.ReadXml(xmfile);

                                          //dataGridView1.DataSource = dts.Tables[0];

                                          string SaveAs = "Save As";
                                          string Deletes = "Delete";
                                          string Restores = "Restore";

                                          if (dts.Tables[0].Rows.Count > 0)
                                          {


                                                 for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                                                 {




                                                        var d1 = dts.Tables[0].Rows[i]["Dates"].ToString() + " " + dts.Tables[0].Rows[i]["Times"].ToString();

                                                        //var d = Convert.ToDateTime(d1);
                                                        //var date = Convert.ToDateTime(d).ToString("dd-MM-yyyy");
                                                        //var time = Convert.ToDateTime(d).ToString("hh-mm-ss tt");

                                                        DateTime dateValue;
                                                        CultureInfo culture = CultureInfo.CurrentCulture;
                                                        DateTimeStyles styles = DateTimeStyles.None;
                                                        DateTime.TryParse(d1, culture, styles, out dateValue);

                                                        //var d = Convert.ToDateTime(d1);
                                                        var d = dateValue;
                                                        // var date = d.ToString("dd-MM-yyyy");
                                                        var date = d.ToString("yyyy-MM-dd");
                                                        var time = d.ToString("HH-mm-ss");
                                                        var DB_PATH1 = dts.Tables[0].Rows[i]["Location"].ToString();
                                                        var Type = dts.Tables[0].Rows[i]["BackupType"].ToString();
                                                        var fls = new AutoBackup()
                                                        {
                                                               //Dates = DateTime.Now,
                                                               //Times = "11:12",
                                                               // Dates = Convert.ToDateTime(date),
                                                               Dates = date,
                                                               Times = time,
                                                               FileNames = "DigiMoorDB_V2.bak",
                                                               Location = DB_PATH1,
                                                               BackupType = Type,
                                                               Visibiles = dts.Tables[0].Rows[i]["BackupType"].ToString() == "Auto" ? "Hidden" : "Visibile"
                                                        };

                                                        filelist.Add(fls);

                                                        // dvgBackup.Rows.Add(Convert.ToInt32(i + 1), Convert.ToDateTime(dts.Tables[0].Rows[i]["Dates"]).ToString("dd-MMM-yyyy"), dts.Tables[0].Rows[i]["Times"].ToString(), dts.Tables[0].Rows[i]["FileNames"].ToString(), dts.Tables[0].Rows[i]["Location"].ToString(), SaveAs, Deletes, Restores);
                                                 }

                                          }



                                   }
                            }
                            //else
                            //{
                            //    var d1 = DateTime.Now.ToString();

                            //    // var d = DateTime.ParseExact(d1, "dd-MM-yyyy hh-mm-ss tt", CultureInfo.InvariantCulture);
                            //    var date = Convert.ToDateTime(d1).ToString("dd-MM-yyyy");
                            //    var time = Convert.ToDateTime(d1).ToString("hh-mm-ss tt");
                            //    var DB_PATH1 = "";
                            //    var fls = new BackupFileClass()
                            //    {
                            //        Dates = Convert.ToDateTime(d1),
                            //        Times = time,
                            //        FileName = "MooringShipboard.bak",
                            //        Location = DB_PATH1

                            //    };

                            //    filelist.Add(fls);
                            //}

                     }
                     catch (Exception ex) { }
                     return filelist;
              }


              private void GetBackupFile1()
              {
                     string ServerName = StaticHelper.ServerName;
                     string path01 = ServerName + "\\DigiMoorDB_Backup";
                     //string path01 = @"C:\DigiMoorDB_Backup";           
                     if (!Directory.Exists(path01))
                     {
                            Directory.CreateDirectory(path01);
                     }
                     string path1 = @"\DBbackup2.xml";
                     string path2 = path01 + path1;
                     if (File.Exists(path2))
                     {
                            using (XmlReader xmfile = XmlReader.Create(path2))
                            {
                                   DataSet dts = new DataSet();
                                   dts.ReadXml(xmfile);
                                   if (dts.Tables[0].Rows.Count > 0)
                                   {
                                          for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                                          {
                                                 AutoBackup bkup = new AutoBackup()
                                                 {
                                                        Dates = dts.Tables[0].Rows[i]["Dates"].ToString(),
                                                        Times = dts.Tables[0].Rows[i]["Times"].ToString(),
                                                        FileNames = dts.Tables[0].Rows[i]["FileNames"].ToString(),
                                                        Location = dts.Tables[0].Rows[i]["Location"].ToString(),
                                                        BackupType = dts.Tables[0].Rows[i]["BackupType"].ToString(),
                                                        Visibiles = dts.Tables[0].Rows[i]["BackupType"].ToString() == "Auto" ? "Hidden" : "Visibile"

                                                 };
                                                 autobkup.Add(bkup);

                                          }

                                          var bckuplist = autobkup.ToList().Where(x => x.BackupType == "Auto");
                                          var bckuplist1 = bckuplist.Where(x => x.BackupType == "Auto").Take(3).OrderByDescending(x => x.Dates).ToList();

                                          if (bckuplist1.Count >= 3)
                                          {
                                                 for (int i = 0; i < bckuplist1.Count; i++)
                                                 {
                                                        autobkup.RemoveAll(x => x.Dates == bckuplist1[i].Dates);
                                                 }
                                          }
                                          //var ss=bckuplist
                                   }
                            }
                     }

                     if (Directory.Exists(path2))
                     {
                            Directory.Delete(path2);
                     }
              }

              private static ObservableCollection<AutoBackup> loadBackupFiles = new ObservableCollection<AutoBackup>();
              public ObservableCollection<AutoBackup> LoadBackupFiles
              {
                     get
                     {
                            return loadBackupFiles;
                     }
                     set
                     {
                            loadBackupFiles = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadBackupFiles"));
                     }
              }

              private static void GrantAccess(string file)
              {
                     bool exists = System.IO.Directory.Exists(file);
                     if (!exists)
                     {
                            DirectoryInfo di = System.IO.Directory.CreateDirectory(file);
                            Console.WriteLine("The Folder is created Sucessfully");
                     }
                     else
                     {
                            Console.WriteLine("The Folder already exists");
                     }
                     DirectoryInfo dInfo = new DirectoryInfo(file);
                     DirectorySecurity dSecurity = dInfo.GetAccessControl();
                     dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                     dInfo.SetAccessControl(dSecurity);

              }

              private void CreateDatabase(object sender, DoWorkEventArgs e)
              {


                     try
                     {
                            //Create Backup from SQL DB
                            //string dtm = DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss tt");
                            //string dtm = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");

                            string dtm = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

                            string ServerName = StaticHelper.ServerName;
                            string path0 = ServerName + "\\DigiMoorDB_Backup\\" + dtm;
                            //string path0 = @"C:\DigiMoorDB_Backup\" + dtm; // Application.StartupPath + "\\DBBackup";
                            if (!Directory.Exists(path0))
                            {
                                   //DirectoryInfo dInfo = new DirectoryInfo(path0);
                                   //DirectorySecurity dSecurity = dInfo.GetAccessControl();
                                   //dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                                   //dInfo.SetAccessControl(dSecurity);

                                   Directory.CreateDirectory(path0);
                            }


                            string path;
                            //...........
                            int ib = 10;
                            _Worker.ReportProgress(ib);
                            PVisible = "Visible";
                            RaisePropertyChanged("CurrentProgress");
                            //...............

                            //string ServerNameKKP = StaticHelper.ServerName;
                            //string pathKKP = ServerName + "\\DigiMoorDB_Backup\\Attachment\\" + obj.AttachmentPath ;

                            if (con.State == ConnectionState.Closed)
                            {
                                   con.Open();
                            }
                            path = path0 + "\\" + "DigiMoorDB_V2.bak";
                            if (File.Exists(path))
                            {
                                   _Worker.ReportProgress(ib + 5);
                                   File.Delete(path);
                                   SqlCommand cmd = new SqlCommand();
                                   SqlDataReader reader;
                                   cmd.CommandText = "BACKUP DATABASE DigiMoorDB_V2 TO DISK= '" + path + "'";
                                   cmd.CommandType = CommandType.Text;
                                   //progressBar1.Value = 3;
                                   cmd.Connection = con;
                                   reader = cmd.ExecuteReader();
                                   con.Close();
                                   _Worker.ReportProgress(ib + 5);
                                   System.Threading.Thread.Sleep(2000);
                                   // progressBar1.Value = 8;

                                   // progressBar1.Value = 10;
                                   _Worker.ReportProgress(ib + 5);
                                   RaisePropertyChanged("CurrentProgress");
                                   DateTime ddtm = DateTime.Now;
                                   //string strbm = String.Format("{0:MM/dd/yyyy}", ddtm);
                                   //string strbm = String.Format("{0:dd/MM/yyyy}", ddtm);

                                   string strbm = String.Format("{0:yyyy-MM-dd", ddtm);

                                   string path01 = @"C:\DigiMoorDB_Backup";
                                   if (!Directory.Exists(path01))
                                   {
                                          Directory.CreateDirectory(path01);
                                   }

                                   string path1 = @"\DBbackup2.xml";
                                   string path2 = path01 + path1;
                                   if (!File.Exists(path2))
                                   {


                                          XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                                          xmlWriterSettings.Indent = true;
                                          xmlWriterSettings.NewLineOnAttributes = true;
                                          using (XmlWriter xmlWriter = XmlWriter.Create(path2, xmlWriterSettings))
                                          {
                                                 xmlWriter.WriteStartDocument();
                                                 xmlWriter.WriteStartElement("BackupListing");

                                                 xmlWriter.WriteStartElement("BackupList");
                                                 xmlWriter.WriteElementString("Dates", strbm.ToString());
                                                 //xmlWriter.WriteElementString("Times", DateTime.Now.ToString("hh:mm tt"));
                                                 xmlWriter.WriteElementString("Times", DateTime.Now.ToString("HH:mm:ss"));
                                                 xmlWriter.WriteElementString("FileNames", "DigiMoorDB_V2.bak");

                                                 string svPath = path.Replace(ServerName, "C:");

                                                 xmlWriter.WriteElementString("Location", svPath);
                                                 xmlWriter.WriteElementString("BackupType", "Normal");

                                                 xmlWriter.WriteEndElement();
                                                 xmlWriter.WriteEndElement();
                                                 xmlWriter.WriteEndDocument();
                                                 xmlWriter.Flush();
                                                 xmlWriter.Close();
                                          }



                                   }
                                   else
                                   {
                                          //string timenow = DateTime.Now.ToString("hh:mm tt");
                                          string timenow = DateTime.Now.ToString("HH:mm:ss");
                                          XDocument xDocument = XDocument.Load(path2);
                                          XElement root = xDocument.Element("BackupListing");
                                          IEnumerable<XElement> rows = root.Descendants("BackupList");
                                          XElement firstRow = rows.First();
                                          string svPath = path.Replace(ServerName, "C:");
                                          firstRow.AddBeforeSelf(new XElement("BackupList", new XElement("Dates", strbm.ToString()), new XElement("Times", timenow), new XElement("FileNames", "DigiMoorDB_V2.bak"), new XElement("Location", svPath), new XElement("BackupType", "Normal")));

                                          xDocument.Save(path2);



                                   }

                                   _Worker.ReportProgress(ib + 5);

                                   RaisePropertyChanged("CurrentProgress");
                                   //string st1 = "insert into InfoLog (dt,UserName,ModuleName,ActionName,Description) values (@dt,@name,@module,@action,@desc)";
                                   //SqlDataAdapter sdalog = new SqlDataAdapter(st1, con);
                                   //sdalog.SelectCommand.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                                   //sdalog.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = Class1.UserTypes;
                                   //sdalog.SelectCommand.Parameters.Add("@module", SqlDbType.VarChar, 500).Value = "Database/Resotre";
                                   //sdalog.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar, -1).Value = "Create Backup";
                                   //sdalog.SelectCommand.Parameters.Add("@desc", SqlDbType.VarChar, -1).Value = Class1.AccessUserName;
                                   //DataTable dtlog = new DataTable();
                                   //sdalog.Fill(dtlog);


                                   MessageBox.Show("Complete system backup created successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                   _Worker.ReportProgress(ib + 5);
                                   RaisePropertyChanged("CurrentProgress");
                                   SqlCommand cmd = new SqlCommand();
                                   SqlDataReader reader;
                                   // progressBar1.Value = 3;
                                   cmd.CommandText = "BACKUP DATABASE DigiMoorDB_V2 TO DISK= '" + path + "'";
                                   cmd.CommandType = CommandType.Text;
                                   cmd.Connection = con;
                                   reader = cmd.ExecuteReader();
                                   con.Close();
                                   _Worker.ReportProgress(ib + 5);
                                   System.Threading.Thread.Sleep(2000);
                                   // progressBar1.Value = 8;

                                   _Worker.ReportProgress(ib + 5);
                                   RaisePropertyChanged("CurrentProgress");

                                   DateTime ddtm = DateTime.Now;
                                   //string strbm = String.Format("{0:MM/dd/yyyy}", ddtm);
                                   //string strbm = String.Format("{0:dd/MM/yyyy}", ddtm);

                                   string strbm = String.Format("{0:yyyy-MM-dd}", ddtm);


                                   // string ServerName = StaticHelper.ServerName;
                                   string path01 = ServerName + "\\DigiMoorDB_Backup";
                                   //string path01 = @"C:\DigiMoorDB_Backup";

                                   if (!Directory.Exists(path01))
                                   {
                                          Directory.CreateDirectory(path01);
                                   }

                                   string path1 = @"\DBbackup2.xml";
                                   string path2 = path01 + path1;
                                   if (!File.Exists(path2))
                                   {


                                          XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                                          xmlWriterSettings.Indent = true;
                                          xmlWriterSettings.NewLineOnAttributes = true;
                                          using (XmlWriter xmlWriter = XmlWriter.Create(path2, xmlWriterSettings))
                                          {
                                                 xmlWriter.WriteStartDocument();
                                                 xmlWriter.WriteStartElement("BackupListing");

                                                 xmlWriter.WriteStartElement("BackupList");
                                                 xmlWriter.WriteElementString("Dates", strbm.ToString());
                                                 //xmlWriter.WriteElementString("Times", DateTime.Now.ToString("hh:mm tt"));
                                                 xmlWriter.WriteElementString("Times", DateTime.Now.ToString("HH:mm:ss"));
                                                 xmlWriter.WriteElementString("FileNames", "DigiMoorDB_V2.bak");
                                                 string svPath = path.Replace(ServerName, "C:");

                                                 xmlWriter.WriteElementString("Location", svPath);
                                                 xmlWriter.WriteElementString("BackupType", "Normal");

                                                 xmlWriter.WriteEndElement();
                                                 xmlWriter.WriteEndElement();
                                                 xmlWriter.WriteEndDocument();
                                                 xmlWriter.Flush();
                                                 xmlWriter.Close();
                                          }



                                   }
                                   else
                                   {
                                          //string timenow = DateTime.Now.ToString("hh:mm tt");
                                          string timenow = DateTime.Now.ToString("HH:mm:ss");
                                          XDocument xDocument = XDocument.Load(path2);

                                          XElement root = xDocument.Element("BackupListing");
                                          IEnumerable<XElement> rows = root.Descendants("BackupList");
                                          XElement firstRow = rows.First();
                                          string svPath = path.Replace(ServerName, "C:");
                                          firstRow.AddBeforeSelf(new XElement("BackupList", new XElement("Dates", strbm.ToString()), new XElement("Times", timenow), new XElement("FileNames", "DigiMoorDB_V2.bak"), new XElement("Location", svPath), new XElement("BackupType", "Normal")));
                                          xDocument.Save(path2);



                                   }

                                   _Worker.ReportProgress(ib + 5);
                                   RaisePropertyChanged("CurrentProgress");
                                   //string st1 = "insert into InfoLog (dt,UserName,ModuleName,ActionName,Description) values (@dt,@name,@module,@action,@desc)";
                                   //SqlDataAdapter sdalog = new SqlDataAdapter(st1, con);
                                   //sdalog.SelectCommand.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                                   //sdalog.SelectCommand.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = Class1.UserTypes;
                                   //sdalog.SelectCommand.Parameters.Add("@module", SqlDbType.VarChar, 500).Value = "Database/Resotre";
                                   //sdalog.SelectCommand.Parameters.Add("@action", SqlDbType.VarChar, -1).Value = "Create Backup";
                                   //sdalog.SelectCommand.Parameters.Add("@desc", SqlDbType.VarChar, -1).Value = Class1.AccessUserName;
                                   //DataTable dtlog = new DataTable();
                                   //sdalog.Fill(dtlog);

                                   loadBackupFiles = GetBackupFile();
                                   RaisePropertyChanged("LoadBackupFiles");



                            }


                     }

                     catch (Exception ex)
                     {

                            //ErrorLogClass obj = new ErrorLogClass();
                            //obj.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                            //MessageBox.Show("System Error: Please contact support team", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                     }
                     finally
                     {
                            // progressBar1.Visible = false;

                            con.Close();

                     }

              }


              private void SaveAsDatabase(AutoBackup obj)
              {
                     SaveFileDialog sfd = new SaveFileDialog();


                     //saveFileDialog1.Filter = "bak Files | *.bak";
                     //saveFileDialog1.DefaultExt = "bak";
                     //saveFileDialog1.InitialDirectory = @"C:\";
                     //saveFileDialog1.RestoreDirectory = true;
                     //saveFileDialog1.Title = "Save File";
                     //saveFileDialog1.FileName = "shipment.bak";

                     sfd.FileName = "DigiMoorDB_V2.bak";
                     sfd.DefaultExt = ".bak";
                     sfd.Filter = "Backup Files|*.bak";
                     sfd.Title = "Save File";

                     if (sfd.ShowDialog() == true)
                     {
                            if (File.Exists(sfd.FileName))
                            {
                                   File.Delete(sfd.FileName);
                                   File.Copy(obj.Location, sfd.FileName);
                            }
                            else
                            {
                                   File.Copy(obj.Location, sfd.FileName);
                            }
                            MessageBox.Show("Database is saved successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);

                     }

              }

              private void RestoreDatabase(AutoBackup obj)
              {
                     if (MessageBox.Show("Restore will override complete data for 'All Mooring Data'. Do you want to proceed with restore operation?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                     {
                            //var conn = sc.Database.Connection;
                            //var path = conn.Database;
                            //if (File.Exists(path))
                            //{
                            //    File.Delete(path);
                            //    File.Copy(obj.Location, path);
                            //}

                            var location = obj.Location;

                            if (File.Exists(location))
                            {
                                   //mainf.TimerAllDeisable();
                                   //System.Threading.Thread.Sleep(1000);
                                   con.Close();
                                   con.Dispose();

                                   //...........
                                   //int ib = 20;
                                   // _Worker.ReportProgress(ib);
                                   RVisible = "Visible";
                                   RaisePropertyChanged("RVisible");
                                   //...............

                                   if (con1.State == ConnectionState.Closed)
                                   {
                                          con1.Open();
                                   }
                                   //if (Class1.Restorecouner == 1)
                                   //{
                                   //    System.Threading.Thread.Sleep(1000);
                                   //}
                                   //progressBar2.Value = 5;
                                   string sql121 = "USE [master] ; ALTER DATABASE [DigiMoorDB_V2] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [DigiMoorDB_V2]; RESTORE DATABASE DigiMoorDB_V2 FROM DISK = '" + location + "' ; ALTER DATABASE [DigiMoorDB_V2] SET  MULTI_USER ; ALTER DATABASE [DigiMoorDB_V2] SET  READ_WRITE ; ALTER DATABASE [DigiMoorDB_V2] SET RECOVERY SIMPLE ";

                                   SqlCommand cmd = new SqlCommand(sql121, con1);
                                   cmd.ExecuteNonQuery();

                                   //progressBar2.Value = 7;
                                   //System.Threading.Thread.Sleep(500);

                                   //progressBar2.Value = 10;

                                   MessageBox.Show("Backup restored successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                                   // progressBar2.Visible = false;
                                   SqlConnection.ClearPool(con);

                                   Application.Current.Shutdown();

                            }
                            else
                            {
                                   MessageBox.Show("This Backup file is not found.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }





                     }
              }

              private void DeleteDatabase(AutoBackup obj)
              {
                     if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                     {
                            string ServerName = StaticHelper.ServerName;
                            string path01 = ServerName + "\\DigiMoorDB_Backup";
                            //string path01 = @"C:\DigiMoorDB_Backup";
                            if (!Directory.Exists(path01))
                            {
                                   Directory.CreateDirectory(path01);
                            }

                            string path1 = @"\DBbackup2.xml";
                            string path2 = path01 + path1;
                            if (!File.Exists(path2))
                            {

                                   // dvgBackup.DataSource = null;
                            }
                            else
                            {
                                   var location = obj.Location;

                                   string[] locy = null;
                                   locy = location.Split('\\');

                                   string dry = "";
                                   dry = locy[0] + "\\" + locy[1] + "\\" + locy[2];
                                   if (Directory.Exists(dry))
                                   {
                                          File.Delete(location);
                                          Directory.Delete(dry, false);
                                   }

                                   XmlDocument xdoc = new XmlDocument();
                                   xdoc.Load(path2);

                                   foreach (XmlNode node in xdoc.SelectNodes("BackupListing/BackupList"))
                                   {
                                          if (node.SelectSingleNode("Location").InnerText == location)
                                          {
                                                 node.ParentNode.RemoveChild(node);
                                          }
                                   }

                                   xdoc.Save(path2);



                                   MessageBox.Show("Backup is deleted successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);



                                   var bkupfile = xdoc.SelectNodes("BackupListing/BackupList").Count;
                                   if (bkupfile == 0)
                                   {

                                          loadBackupFiles.Clear();
                                          RaisePropertyChanged("LoadBackupFiles");
                                          File.Delete(path2);

                                   }
                                   else
                                   {
                                          loadBackupFiles = GetBackupFile();
                                          RaisePropertyChanged("LoadBackupFiles");
                                   }

                            }




                     }

              }


              new public event PropertyChangedEventHandler PropertyChanged;

              protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
              {
                     PropertyChanged?.Invoke(this, e);
              }

              //public void OnPropertyChanged(PropertyChangedEventArgs e)
              //{
              //    if (PropertyChanged != null)
              //        PropertyChanged(this, e);
              //}

       }
}
