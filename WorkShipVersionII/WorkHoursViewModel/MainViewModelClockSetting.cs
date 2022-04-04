using GalaSoft.MvvmLight;
using WorkShipVersionII.ViewModelCrewManagement;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class MainViewModelClockSetting : ViewModelBase
    {
        public MainViewModelClockSetting()
        {
            CurrentViewModelClockSetting = _InternationalDateLineViewModel;
            SettingCommand = new MyCommand<string>(OnSettingCommand);
        }


        public static CommonPropertiesOPA _CommonData1 = new CommonPropertiesOPA();
        public CommonPropertiesOPA CommonData1
        {
            get
            {
                if (_CommonData1 == null)
                    _CommonData1 = new CommonPropertiesOPA();
                return _CommonData1;
            }
            set
            {

                _CommonData1 = value;
                RaisePropertyChanged("CommonData1");
            }
        }


        public MyCommand<string> SettingCommand { get; private set; }

        private ViewModelBase _currentViewModelClockSetting;
        public ViewModelBase CurrentViewModelClockSetting
        {
            get
            {
                return _currentViewModelClockSetting;
            }
            set
            {
                Set(ref _currentViewModelClockSetting, value);


            }
        }

        readonly static InternationalDateLineViewModel _InternationalDateLineViewModel = new InternationalDateLineViewModel();
        readonly static RetardAdvanceViewModel _RetardAdvanceViewModel = new RetardAdvanceViewModel();
        readonly static OPA90ViewModel _OPA90ViewModel = new OPA90ViewModel();

        private void InternationalMethod()
        {
            CurrentViewModelClockSetting = _InternationalDateLineViewModel;
        }
        private void RetardAdvanceMethod()
        {
            CurrentViewModelClockSetting = _RetardAdvanceViewModel;
        }
        private void OPA90Method()
        {
            CurrentViewModelClockSetting = _OPA90ViewModel;
        }


        private void OnSettingCommand(string destination)
        {

            switch (destination)
            {

                case "RetardAdvance":
                    CurrentViewModelClockSetting = _RetardAdvanceViewModel;
                    break;
                case "OPA90":
                    CurrentViewModelClockSetting = _OPA90ViewModel;
                    break;
               
                case "InternationalDateLine":
                default:
                    CurrentViewModelClockSetting = _InternationalDateLineViewModel;
                    break;
            }
        }

        public override void Cleanup()
        {
            _InternationalDateLineViewModel.Cleanup();
            _RetardAdvanceViewModel.Cleanup();
            _OPA90ViewModel.Cleanup();
        }

    }
}
