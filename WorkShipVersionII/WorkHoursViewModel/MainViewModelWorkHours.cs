using DataBuildingLayer;
using GalaSoft.MvvmLight;
using WorkShipVersionII.ViewModelCrewManagement;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using WorkShipVersionII.ViewModelReports;
using System;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class MainViewModelWorkHours : ViewModelBase
       {
              public MainViewModelWorkHours()
              {
                     CurrentViewModelWorkHours = MainViewModelWorkHours._detailWorkRestHoursViewModel;
                     CalenderCommand = new MyCommand<CrewDetailClass>(AddCalenderViewModel);
                     PlannerCalenderCommand = new MyCommand<CrewDetailClass>(PlannerCalenderViewModel);
                     WorkHoursCommand = new RelayCommand(WorkHoursViewModel);
                     IDLClocksSettingCommand = new RelayCommand(IDLClocksSettingMethod);
                     LogBookCommand = new RelayCommand(LogbookMethod);

                     if (UserTypeClass.UserTypes == "HOD")
                     {
                            new DetailWorkRestHoursViewModel();
                     }

              }


              private ViewModelBase _currentViewModelWorkHours;
              public ViewModelBase CurrentViewModelWorkHours
              {
                     get
                     {
                            return _currentViewModelWorkHours;
                     }
                     set
                     {
                            Set(ref _currentViewModelWorkHours, value);


                     }
              }



              public MyCommand<CrewDetailClass> CalenderCommand { get; private set; }
              public MyCommand<CrewDetailClass> PlannerCalenderCommand { get; private set; }
              public ICommand WorkHoursCommand { get; private set; }
              public ICommand IDLClocksSettingCommand { get; private set; }
              public ICommand LogBookCommand { get; private set; }

              readonly static DetailWorkRestHoursViewModel _detailWorkRestHoursViewModel = new DetailWorkRestHoursViewModel();
              readonly static IDLClocksSettingViewModel _iDLClocksSettingViewModel = new IDLClocksSettingViewModel();
              readonly static LogbookViewModel _logbookViewModel = new LogbookViewModel();
              private void IDLClocksSettingMethod()
              {
                     CurrentViewModelWorkHours = _iDLClocksSettingViewModel;
              }
              private void LogbookMethod()
              {
                     CurrentViewModelWorkHours = _logbookViewModel;
              }
              private void AddCalenderViewModel(CrewDetailClass obj)
              {
                     if (obj != null)
                     {
                            obj.ActualorPlanner = "Actual";
                            CurrentViewModelWorkHours = new WorkHoursCalenderViewModel(obj);
                     }
              }
              private void PlannerCalenderViewModel(CrewDetailClass obj)
              {
                     if (obj != null)
                     {
                            obj.ActualorPlanner = "Planner";
                            CurrentViewModelWorkHours = new WorkHoursCalenderViewModel(obj);
                     }
              }

              private void WorkHoursViewModel()
              {
                     CurrentViewModelWorkHours = _detailWorkRestHoursViewModel;
              }

              public override void Cleanup()
              {
                     _detailWorkRestHoursViewModel.Cleanup();
                     _iDLClocksSettingViewModel.Cleanup();
              }


       }
}
