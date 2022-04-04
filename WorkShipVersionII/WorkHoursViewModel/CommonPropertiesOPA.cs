using GalaSoft.MvvmLight;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class CommonPropertiesOPA : ViewModelBase
    {
        public CommonPropertiesOPA()
        {

        }
        private bool opa90Visibles = false;
        public bool OPA90Visibles
        {
            get
            {
                if (!opa90Visibles)
                {
                    opa90Visibles = false;
                }
                return opa90Visibles;
            }
            set
            {
                opa90Visibles = value;
                RaisePropertyChanged("OPA90Visibles");
            }
        }

        private string opa90Hidden = "Visible";
        public string OPA90Hidden
        {
            get
            {
                if (opa90Hidden == null)
                    opa90Hidden = "Visible";
                return opa90Hidden;
            }
            set
            {
                opa90Hidden = value;
                RaisePropertyChanged("OPA90Hidden");
            }
        }
    }
}
