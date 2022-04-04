using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkShipVersionII.ViewModelMooringCalculator
{
   public class OutPutContainerViewModel:ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        public ICommand OutputsWindLoadsCommand { get; private set; }
        public ICommand OutputsCurrentLoadsCommand { get; private set; }
        public ICommand OutputsFinalForcesCommand { get; private set; }
        public ICommand OutputsMooringLineCommand { get; private set; }

        public OutPutContainerViewModel()
        {
            ExecuteOutputsWindLoads();
            OutputsWindLoadsCommand = new RelayCommand(() => ExecuteOutputsWindLoads());
            OutputsCurrentLoadsCommand = new RelayCommand(() => ExecuteOutputsCurrentLoads());
            OutputsFinalForcesCommand = new RelayCommand(() => ExecuteOutputsFinalForces());

            OutputsMooringLineCommand = new RelayCommand(() => ExecuteOutputsMooringLine());
        }


        private void ExecuteOutputsWindLoads()
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            CurrentViewModel = new OutputsWindLoadsViewModel();
        }
        private void ExecuteOutputsCurrentLoads()
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            CurrentViewModel = new OutputsCurrentLoadsViewModel();
        }
        private void ExecuteOutputsFinalForces()
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            CurrentViewModel = new OutputsFinalForcesViewModel();
        }
        private void ExecuteOutputsMooringLine()
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            CurrentViewModel = new OutputsMooringLineViewModel();
        }

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

    }
}
