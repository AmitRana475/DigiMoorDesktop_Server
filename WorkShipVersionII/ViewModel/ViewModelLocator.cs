/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WorkShipVersionII"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using WorkShipVersionII.ViewModelAdministration;
using WorkShipVersionII.ViewModelCrewManagement;
using WorkShipVersionII.ViewModelReports;

namespace WorkShipVersionII.ViewModel
{
    
    public class ViewModelLocator
    {
        
   

        //private static MainViewModel _main;
        //private static MainViewModelCrewManagement _maincrew;
        //private static MainViewModelReports _mainReport;
        private static MainAdministrationViewModel _mainadministration;

        public ViewModelLocator()
        {

            //_main = new MainViewModel();
            //_maincrew = new MainViewModelCrewManagement();
            //_mainReport = new MainViewModelReports();
            _mainadministration = new MainAdministrationViewModel();



            //ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //////if (ViewModelBase.IsInDesignModeStatic)
            //////{
            //////    // Create design time view services and models
            //////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            //////}
            //////else
            //////{
            //////    // Create run time view services and models
            //////    SimpleIoc.Default.Register<IDataService, DataService>();
            //////}

            //SimpleIoc.Default.Register<MainViewModel>();
        }

        

        //public MainViewModel Main
        //{
        //    get
        //    {
        //        return _main;
        //    }
        //}

        //public MainViewModelCrewManagement MainCrew
        //{
        //    get
        //    {
        //        return _maincrew;
        //    }
        //}

        //public MainViewModelReports MainReport
        //{
        //    get
        //    {
        //        return _mainReport;
        //    }
        //}

        public MainAdministrationViewModel MainAdministration
        {
            get
            {
                return _mainadministration;
            }
        }
        public static void Cleanup()
        {
            //_main.Cleanup();
            //_maincrew.Cleanup();
            //_mainReport.Cleanup();
            _mainadministration.Cleanup();
        }
    }
}