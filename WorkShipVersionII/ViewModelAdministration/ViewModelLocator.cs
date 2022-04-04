
using WorkShipVersionII.ViewModelAdministration;

namespace WorkShipVersionII.ViewModelAdministration
{
    public class ViewModelLocator
    {

        private static MainAdministrationViewModel _mainadministration;

        public ViewModelLocator()
        {
            _mainadministration = new MainAdministrationViewModel();
        }

        public MainAdministrationViewModel MainAdministration
        {
            get
            {
                return _mainadministration;
            }
        }
        public static void Cleanup()
        {
           
            _mainadministration.Cleanup();
        }

    }
}
