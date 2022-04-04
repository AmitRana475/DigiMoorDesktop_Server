using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using WorkShipVersionII.LoginViewModel;
using WorkShipVersionII.ViewModelCrewManagement;

namespace WorkShipVersionII
{
   
    public partial class LoginWindow : Window
    {


        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);


        public LoginWindow()
        {
            Process currentProcess = Process.GetCurrentProcess();
            var runningProcess = (from process in Process.GetProcesses()
                                  where
                                    process.Id != currentProcess.Id &&
                                    process.ProcessName.Equals(
                                      currentProcess.ProcessName,
                                      StringComparison.Ordinal)
                                  select process).FirstOrDefault();
            if (runningProcess != null)
            {

                if (MessageBox.Show("You are already logged in. Do you want to close already logged in session?", "DigiMoorX7 Server", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    runningProcess.Kill();
                    currentProcess.Kill();
                    // ShowWindow(runningProcess.MainWindowHandle, 1);
                    return;
                }
                else
                {
                    currentProcess.Kill();
                    ShowWindow(runningProcess.MainWindowHandle, 1);
                    return;
                }

            }
            else
            {

                InitializeComponent();
                // this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            }
        }

        //double screenWidth = 1360;
        //// MainWindow main = new MainWindow();
        //public LoginWindow()
        //{
        //    try
        //    {
        //        InitializeComponent();



        //        screenWidth = SystemParameters.PrimaryScreenWidth;

        //        //if (screenWidth > 1366)
        //        //{
        //        //    this.MaxHeight = 768;// SystemParameters.MaximizedPrimaryScreenHeight;
        //        //    this.Height = 768;
        //        //    this.Width = 1366;
        //        //    this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //        //}
        //        //else
        //        //{
        //        //    this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        //        //    this.WindowState = WindowState.Maximized;
        //        //}
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //    //this.DataContext = new LoginMainViewModel();

        //    //lblLoading.Visibility = Visibility.Hidden;
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //    if (txtUserName.Text.ToLower() == "admin" && txtPassword.Password == "123123")
        //    {

        //        //Thread.Sleep(2000);
        //        lblLoading.Visibility = Visibility.Visible;


        //        new MainViewModelCrewManagement();
        //        main.Show();
        //        this.Close();
        //        //System.Windows.Threading.Dispatcher.Run();



        //    }



        //}







    }
}
