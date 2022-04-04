using DataBuildingLayer;
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
    public class MainViewModelMooringCalculator : ViewModelBase
    {

        private ViewModelBase _currentViewModel;
        readonly static InputsVesselViewModel _inputsVesselViewModel = new InputsVesselViewModel();

        public ICommand HelpCommand { get; private set; }
        public ICommand InputsEnvironmentCommand { get; private set; }
        public ICommand InputsMooringLinesCommand { get; private set; }
        public ICommand InputsVesselCommand { get; private set; }



       public ICommand FinalOutputContainerCommand { get; private set; }

       public ICommand SavePortWiseListingCommand { get; private set; }

        //public ICommand SaveCurrentValuesCommand { get; private set; }


        public ICommand OutputContainerCommand { get; private set; }
        public ICommand AddMooringCommand { get; private set; }
        public ICommand AddMooringCommand1 { get; private set; }

        public MainViewModelMooringCalculator()
        {
            CurrentViewModel = MainViewModelMooringCalculator._inputsVesselViewModel;
            InputsEnvironmentCommand = new RelayCommand(() => ExecuteInputsEnvironment());
            InputsMooringLinesCommand = new RelayCommand(() => ExecuteInputsMooringLines());
            InputsVesselCommand = new RelayCommand(() => ExecuteInputsVessel());


            //OutputsCurrentLoadsCommand = new RelayCommand(() => ExecuteOutputsCurrentLoads());
            //OutputsFinalForcesCommand = new RelayCommand(() => ExecuteOutputsFinalForces());
           
            //OutputsMooringLineCommand = new RelayCommand(() => ExecuteOutputsMooringLine());
            AddMooringCommand = new RelayCommand(() => ExecuteAddMooringCommand());
            AddMooringCommand1 = new RelayCommand<RopeBasedCalculationsClass>(ExecuteAddMooringCommand1);
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

            OutputContainerCommand = new RelayCommand(() => ExecuteOutputsContainerLoads());

            FinalOutputContainerCommand = new RelayCommand(() => ExecuteFinalOutputsContainerLoads());

            SavePortWiseListingCommand = new RelayCommand(() => ExecuteSavePortWiseListing());

            //SaveCurrentValuesCommand = new RelayCommand(() => ExecuteSaveCurrentValue());


        }

        private void ExecuteAddMooringCommand1(RopeBasedCalculationsClass obj)
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            CurrentViewModel = new AddMooringLineViewModel(obj);

        }
        private void ExecuteAddMooringCommand()
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            CurrentViewModel = new AddMooringLineViewModel();
        }

        public void ExecuteInputsVessel()
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            _inputsVesselViewModel.RefreshInputList();
            CurrentViewModel = _inputsVesselViewModel;

        }
        private void ExecuteInputsEnvironment()
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            CurrentViewModel = new InputsEnvironmentViewModel();
        }
        private void ExecuteInputsMooringLines()
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            CurrentViewModel = new InputsMooringLinesViewModel();
        }


        
       

        private void ExecuteOutputsContainerLoads()
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            CurrentViewModel = new OutPutContainerViewModel();
        }


        private void ExecuteFinalOutputsContainerLoads()
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            CurrentViewModel = new FinalOutputContainerViewModel();
        }

        private void ExecuteSavePortWiseListing()
        {
            StaticHelper.HelpFor = @"5.0  MOORING CALCULATOR.htm";
            CurrentViewModel = new SavePortwiseListingViewModel();
        }
        //private void ExecuteSaveCurrentValue()
        //{
        //    StaticHelper.HelpFor= @"5.0  MOORING CALCULATOR.htm";
        //    CurrentViewModel = new SaveCurrentValuesViewModel();
        //}




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
