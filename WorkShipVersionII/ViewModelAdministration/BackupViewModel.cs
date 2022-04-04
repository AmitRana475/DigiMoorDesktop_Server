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
using System.Windows;
using System.Windows.Input;
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

        public BackupViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
                con = sc.con;

                con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxtSVR"].ConnectionString);

            }
            //createCommand = new RelayCommand(CreateDatabase);
            deleteCommand = new RelayCommand<BackupFileClass>(DeleteDatabase);
            restoreCommand = new RelayCommand<BackupFileClass>(RestoreDatabase);
            saveAsCommand = new RelayCommand<BackupFileClass>(SaveAsDatabase);

            createCommand = new RelayCommand(() => _Worker.RunWorkerAsync(), () => !_Worker.IsBusy);
            _Worker = new BackgroundWorker();
            _Worker.WorkerReportsProgress = true;
            _Worker.DoWork += CreateDatabase;
            _Worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            _Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CreateBakupCompleted);

            loadBackupFiles = GetBackupFile();


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
        private ObservableCollection<BackupFileClass> GetBackupFile()
        {
            var filelist = new ObservableCollection<BackupFileClass>();
            string path01 = @"C:\Work-Ship_DB_Backup";
            /* if (Directory.Exists(path1))
             {

                 var folders = System.IO.Directory.GetDirectories(@"C:\Work-Ship_DB_Backup\", "*", System.IO.SearchOption.AllDirectories);
                 foreach (var item in folders)
                 {
                     string path = @"C:\Work-Ship_DB_Backup\";
                     string DB_PATH1 = item + "\\" + "shipment.sdf";

                     string path1 = @"\DBbackup2.xml";
                     string path2 = path01 + path1;

                     if (File.Exists(DB_PATH1))
                     {
                         var d1 = item.Replace(path, "");

                         var d = DateTime.ParseExact(d1, "dd-MM-yyyy hh-mm-ss tt", CultureInfo.InvariantCulture);
                         var date = Convert.ToDateTime(d).ToString("dd-MM-yyyy");
                         var time = Convert.ToDateTime(d).ToString("hh-mm-ss tt");

                         var fls = new BackupFileClass()
                         {
                             Dates = Convert.ToDateTime(d),
                             Times = time,
                             FileName = "shipment.sdf",
                             Location = DB_PATH1

                         };

                         filelist.Add(fls);


                     }
                 }

             }*/

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

                            var d = Convert.ToDateTime(d1);
                            var date = d.ToString("dd-MM-yyyy");
                            var time = d.ToString("hh-mm-ss tt");
                            var DB_PATH1 = dts.Tables[0].Rows[i]["Location"].ToString();
                            var fls = new BackupFileClass()
                            {
                                Dates = d,
                                Times = time,
                                FileName = "shipment5.bak",
                                Location = DB_PATH1

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
            //        FileName = "shipment5.bak",
            //        Location = DB_PATH1

            //    };

            //    filelist.Add(fls);
            //}
            return filelist;
        }

        private static ObservableCollection<BackupFileClass> loadBackupFiles = new ObservableCollection<BackupFileClass>();
        public ObservableCollection<BackupFileClass> LoadBackupFiles
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



        private void CreateDatabase(object sender, DoWorkEventArgs e)
        {


            try
            {
                //Create Backup from SQL DB
                string dtm = DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss tt");

                string path0 = @"C:\Work-Ship_DB_Backup\" + dtm; // Application.StartupPath + "\\DBBackup";
                if (!Directory.Exists(path0))
                {
                    Directory.CreateDirectory(path0);
                }


                string path;
                //...........
                int ib = 10;
                _Worker.ReportProgress(ib);
                PVisible = "Visible";
                RaisePropertyChanged("CurrentProgress");
                //...............


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                path = path0 + "\\" + "shipment5.bak";
                if (File.Exists(path))
                {
                    _Worker.ReportProgress(ib + 5);
                    File.Delete(path);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandText = "BACKUP DATABASE shipment5 TO DISK= '" + path + "'";
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
                    string strbm = String.Format("{0:MM/dd/yyyy}", ddtm);



                    string path01 = @"C:\Work-Ship_DB_Backup";
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
                            xmlWriter.WriteElementString("Times", DateTime.Now.ToString("hh:mm tt"));
                            xmlWriter.WriteElementString("FileNames", "shipment5.bak");
                            xmlWriter.WriteElementString("Location", path);

                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndDocument();
                            xmlWriter.Flush();
                            xmlWriter.Close();
                        }



                    }
                    else
                    {
                        string timenow = DateTime.Now.ToString("hh:mm tt");
                        XDocument xDocument = XDocument.Load(path2);
                        XElement root = xDocument.Element("BackupListing");
                        IEnumerable<XElement> rows = root.Descendants("BackupList");
                        XElement firstRow = rows.First();
                        firstRow.AddBeforeSelf(new XElement("BackupList", new XElement("Dates", strbm.ToString()), new XElement("Times", timenow), new XElement("FileNames", "shipment5.bak"), new XElement("Location", path)));

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
                    cmd.CommandText = "BACKUP DATABASE shipment5 TO DISK= '" + path + "'";
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
                    string strbm = String.Format("{0:MM/dd/yyyy}", ddtm);


                    string path01 = @"C:\Work-Ship_DB_Backup";

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
                            xmlWriter.WriteElementString("Times", DateTime.Now.ToString("hh:mm tt"));
                            xmlWriter.WriteElementString("FileNames", "shipment5.bak");
                            xmlWriter.WriteElementString("Location", path);

                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndDocument();
                            xmlWriter.Flush();
                            xmlWriter.Close();
                        }



                    }
                    else
                    {
                        string timenow = DateTime.Now.ToString("hh:mm tt");
                        XDocument xDocument = XDocument.Load(path2);

                        XElement root = xDocument.Element("BackupListing");
                        IEnumerable<XElement> rows = root.Descendants("BackupList");
                        XElement firstRow = rows.First();
                        firstRow.AddBeforeSelf(new XElement("BackupList", new XElement("Dates", strbm.ToString()), new XElement("Times", timenow), new XElement("FileNames", "shipment5.bak"), new XElement("Location", path)));
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


        private void SaveAsDatabase(BackupFileClass obj)
        {
            SaveFileDialog sfd = new SaveFileDialog();


            //saveFileDialog1.Filter = "bak Files | *.bak";
            //saveFileDialog1.DefaultExt = "bak";
            //saveFileDialog1.InitialDirectory = @"C:\";
            //saveFileDialog1.RestoreDirectory = true;
            //saveFileDialog1.Title = "Save File";
            //saveFileDialog1.FileName = "shipment.bak";

            sfd.FileName = "shipment5.bak";
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

        private void RestoreDatabase(BackupFileClass obj)
        {
            if (MessageBox.Show("Restore will override complete data for 'All Crew'. Do you want to proceed with restore operation?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
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
                    string sql121 = "USE [master] ; ALTER DATABASE [shipment5] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [shipment5]; RESTORE DATABASE shipment5 FROM DISK = '" + location + "' ; ALTER DATABASE [shipment5] SET  MULTI_USER ; ALTER DATABASE [shipment5] SET  READ_WRITE ; ALTER DATABASE [shipment5] SET RECOVERY SIMPLE ";

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

        private void DeleteDatabase(BackupFileClass obj)
        {
            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                string path01 = @"C:\Work-Ship_DB_Backup";
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

                    loadBackupFiles = GetBackupFile();
                    RaisePropertyChanged("LoadBackupFiles");
                }


                //var path0 = obj.Location;
                //string root = obj.Location.Replace("\\shipment5.bak", "");

                //if (File.Exists(path0))
                //{
                //    File.Delete(path0);
                //    if (Directory.Exists(root))
                //    {
                //        Directory.Delete(root);

                //        MessageBox.Show("Backup is deleted successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                //        loadBackupFiles.Clear();
                //        loadBackupFiles = GetBackupFile();
                //        RaisePropertyChanged("loadBackupFiles");
                //    }
                //}


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
