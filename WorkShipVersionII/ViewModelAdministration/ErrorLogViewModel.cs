using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WorkShipVersionII.ViewModelAdministration
{
       public  class ErrorLogViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private BackgroundWorker _Worker;
        public ICommand HelpCommand { get; private set; }
        public ErrorLogViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            _ExportCommand = new RelayCommand(() => _Worker.RunWorkerAsync(), () => !_Worker.IsBusy);
            _Worker = new BackgroundWorker();
            //_Worker.WorkerReportsProgress = true;
            _Worker.DoWork += ExportMethod;
            _Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerCompleted);

        }

        private ICommand _ExportCommand;
        public ICommand ExportCommand
        {
            get
            {
                return _ExportCommand;
            }

        }

        

        private void workerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("File has been exported, Please send the file Support Team", "System Info", MessageBoxButton.OK, MessageBoxImage.Information);
            sc.CreateLog("ErrorLog", "Export Data", null);
        }

    

        private void ExportMethod(object sender, DoWorkEventArgs e)
        {
            try
            {

                //string path0 = @"C:\DigiMoorDB_Backup";

                            string ServerName = StaticHelper.ServerName;
                            string path0 = ServerName + "\\DigiMoorDB_Backup";
                            string path1 = @"\Systemlogfile.txt";
                string path2 = path0 + path1;
                if (File.Exists(path2))
                {
                    SaveFileDialog sfd = new SaveFileDialog();

                    sfd.FileName = "SystemErrorlogfile";

                    sfd.DefaultExt = "txt";
                    sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                    if (sfd.ShowDialog() == true)
                    {

                        FileInfo file = new FileInfo(path2);
                        file.CopyTo(sfd.FileName, true);


                    }


                }
                else
                {
                    MessageBox.Show("System still has not found any Error Log", "System Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
              

            }
        }

        


    }
}
