using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace WorkShipVersionII.ViewModelReports
{
    public class MainViewModelReports : ViewModelBase
    {
        private ViewModelBase _currentViewModelReport;

        readonly static CrewDetailViewModel _crewDetailViewModel = new CrewDetailViewModel();
        readonly static WorkScheduleViewModel _workScheduleViewModel = new WorkScheduleViewModel();
        readonly static CrewWorkHoursViewModel _crewWorkHoursViewModel = new CrewWorkHoursViewModel();
        readonly static DeviationsViewModel _deviationsViewModel = new DeviationsViewModel();
        readonly static OverTimeViewModel _overTimeViewModel = new OverTimeViewModel();
        readonly static DepartmentPlanningViewModel _departmentPlanningViewModel = new DepartmentPlanningViewModel();
        readonly static CrewWorkPlanningViewModel _crewWorkPlanningViewModel = new CrewWorkPlanningViewModel();




        public static CommonProperties _CommonData = new CommonProperties();
        public CommonProperties CommonData
        {
            get
            {
                if (_CommonData == null)
                    _CommonData = new CommonProperties();
                return _CommonData;
            }
            set
            {

                _CommonData = value;
                RaisePropertyChanged("CommonData");
            }
        }



        public ViewModelBase CurrentViewModelReport
        {
            get
            {
                return _currentViewModelReport;
            }
            set
            {
                if (_currentViewModelReport == value)
                    return;
                _currentViewModelReport = value;
                RaisePropertyChanged("CurrentViewModelReport");
            }
        }

        public ICommand CrewDetailReportCommand { get; private set; }
        public ICommand WorkScheduleReportCommand { get; private set; }
        public ICommand CrewWorkHoursReportCommand { get; private set; }
        public ICommand DeviationsReportCommand { get; private set; }
        public ICommand OverTimeReportCommand { get; private set; }
        public ICommand DepartmentPlanningReportCommand { get; private set; }
        public ICommand CrewWorkPlanningReportCommand { get; private set; }



        public MainViewModelReports()
        {

            CurrentViewModelReport = MainViewModelReports._crewDetailViewModel;
            CrewDetailReportCommand = new RelayCommand(() => ExecuteCrewDetailReportCommand());
            WorkScheduleReportCommand = new RelayCommand(() => ExecuteWorkScheduleReportCommand());
            CrewWorkHoursReportCommand = new RelayCommand(() => ExecuteCrewWorkHoursReportCommand());
            DeviationsReportCommand = new RelayCommand(() => ExecuteDeviationsReportCommand());
            OverTimeReportCommand = new RelayCommand(() => ExecuteOverTimeReportCommand());
            DepartmentPlanningReportCommand = new RelayCommand(() => ExecuteDepartmentPlanning());
            CrewWorkPlanningReportCommand = new RelayCommand(() => ExecuteCrewWorkPlanning());


            if (UserTypeClass.UserTypes == "HOD")
            {
                new CrewDetailViewModel();

                var data = UserTypeClass.HODAccess;
                if (data != null)
                {
                    if (data.SelectAll)
                    {
                    }
                    else
                    {


                        if (data.OverView == true)
                        {
                            _CommonData.OverView = "Visible";
                            RaisePropertyChanged("CommonData");
                        }
                        else
                        {
                            _CommonData.OverView = "Collapsed";
                            RaisePropertyChanged("CommonData");

                        }


                        if (data.CrewWorkHours == true)
                        {
                            _CommonData.CrewWorkHours = "Visible";
                            RaisePropertyChanged("CommonData");
                        }
                        else
                        {
                            _CommonData.CrewWorkHours = "Collapsed";
                            RaisePropertyChanged("CommonData");

                        }

                        if (data.NonConfirmity == true)
                        {
                            _CommonData.NonConfirmity = "Visible";
                            RaisePropertyChanged("CommonData");
                        }
                        else
                        {
                            _CommonData.NonConfirmity = "Collapsed";
                            RaisePropertyChanged("CommonData");

                        }

                        if (data.OverTime == true)
                        {
                            _CommonData.OverTime = "Visible";
                            RaisePropertyChanged("CommonData");
                        }
                        else
                        {
                            _CommonData.OverTime = "Collapsed";
                            RaisePropertyChanged("CommonData");

                        }


                    }
                }
            }



        }



        private void ExecuteCrewDetailReportCommand()
        {
            CurrentViewModelReport = MainViewModelReports._crewDetailViewModel;
        }
        private void ExecuteWorkScheduleReportCommand()
        {
            CurrentViewModelReport = MainViewModelReports._workScheduleViewModel;
        }

        private void ExecuteCrewWorkHoursReportCommand()
        {
            CurrentViewModelReport = MainViewModelReports._crewWorkHoursViewModel;
        }

        private void ExecuteDeviationsReportCommand()
        {
            CurrentViewModelReport = MainViewModelReports._deviationsViewModel;
        }
        private void ExecuteOverTimeReportCommand()
        {
            CurrentViewModelReport = MainViewModelReports._overTimeViewModel;
        }
        private void ExecuteCrewWorkPlanning()
        {
            CurrentViewModelReport = MainViewModelReports._crewWorkPlanningViewModel;
        }

        private void ExecuteDepartmentPlanning()
        {
            CurrentViewModelReport = MainViewModelReports._departmentPlanningViewModel;
        }


        public override void Cleanup()
        {
            _crewDetailViewModel.Cleanup();
            _workScheduleViewModel.Cleanup();
            _crewWorkHoursViewModel.Cleanup();
            _deviationsViewModel.Cleanup();
            _overTimeViewModel.Cleanup();
            _crewWorkPlanningViewModel.Cleanup();
            _departmentPlanningViewModel.Cleanup();
        }
    }
}
