using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System.Windows;
using WorkShipVersionII.Views;

namespace WorkShipVersionII.ViewModelCrewManagement
{
       public class MainViewModelCrewManagement : ViewModelBase
       {


              public MainViewModelCrewManagement()
              {
                     CurrentViewModelCrew = MainViewModelCrewManagement._crewDetailViewModel;
                     NavCommand = new MyCommand<string>(OnNavCommand);
                     NavCommand1 = new MyCommand<HoliDayGroupNameClass>(AddHolidayList);
                     NavCommand2 = new MyCommand<CrewDetailClass>(AddCrewDetailList);
                     NavCommand3 = new MyCommand<UserAccessClass>(AddUserAccess);


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

                                          if (data.CrewManagement == true)
                                          {
                                                 _CommonVisible.CrewManagement = "Visible";
                                                 RaisePropertyChanged("CommonVisible");
                                          }
                                          else
                                          {
                                                 _CommonVisible.CrewManagement = "Collapsed";
                                                 RaisePropertyChanged("CommonVisible");

                                          }

                                          if (data.CrewRank == true)
                                          {
                                                 _CommonVisible.CrewRank = "Visible";
                                                 RaisePropertyChanged("CommonVisible");
                                          }
                                          else
                                          {
                                                 _CommonVisible.CrewRank = "Collapsed";
                                                 RaisePropertyChanged("CommonVisible");

                                          }

                                          if (data.Department == true)
                                          {
                                                 _CommonVisible.Department = "Visible";
                                                 RaisePropertyChanged("CommonVisible");
                                          }
                                          else
                                          {
                                                 _CommonVisible.Department = "Collapsed";
                                                 RaisePropertyChanged("CommonVisible");

                                          }

                                          if (data.HolidayGroup == true)
                                          {
                                                 _CommonVisible.HolidayGroup = "Visible";
                                                 RaisePropertyChanged("CommonVisible");
                                          }
                                          else
                                          {
                                                 _CommonVisible.HolidayGroup = "Collapsed";
                                                 RaisePropertyChanged("CommonVisible");

                                          }

                                          if (data.HOD == true)
                                          {
                                                 _CommonVisible.HOD = "Visible";
                                                 RaisePropertyChanged("CommonVisible");
                                          }
                                          else
                                          {
                                                 _CommonVisible.HOD = "Collapsed";
                                                 RaisePropertyChanged("CommonVisible");

                                          }

                                          if (data.ResetPassword == true)
                                          {
                                                 _CommonVisible.ResetPassword = "Visible";
                                                 RaisePropertyChanged("CommonVisible");
                                          }
                                          else
                                          {
                                                 _CommonVisible.ResetPassword = "Collapsed";
                                                 RaisePropertyChanged("CommonVisible");

                                          }

                                          if (data.FreezeUnfreezeAll == true)
                                          {
                                                 _CommonVisible.FreezeUnfreezeAll = "Visible";
                                                 RaisePropertyChanged("CommonVisible");
                                          }
                                          else
                                          {
                                                 _CommonVisible.FreezeUnfreezeAll = "Collapsed";
                                                 RaisePropertyChanged("CommonVisible");

                                          }


                                   }
                            }
                     }

              }



              private ViewModelBase _currentViewModelcrew;
              public ViewModelBase CurrentViewModelCrew
              {
                     get
                     {
                            return _currentViewModelcrew;
                     }
                     set
                     {
                            Set(ref _currentViewModelcrew, value);


                     }
              }


              public static CommonPropertiesVisibilety _CommonVisible = new CommonPropertiesVisibilety();
              public CommonPropertiesVisibilety CommonVisible
              {
                     get
                     {
                            if (_CommonVisible == null)
                                   _CommonVisible = new CommonPropertiesVisibilety();
                            return _CommonVisible;
                     }
                     set
                     {

                            _CommonVisible = value;
                            RaisePropertyChanged("CommonVisible");
                     }
              }




              readonly static CrewDetailViewModel _crewDetailViewModel = new CrewDetailViewModel();
              readonly static CrewRankViewModel _crewRankViewModel = new CrewRankViewModel();
              readonly static DepartmentViewModel _departmentViewModel = new DepartmentViewModel();
              readonly static HoliDayGroupViewModel _holiDayGroupViewModel = new HoliDayGroupViewModel();
              readonly static HODViewModel _hODViewModel = new HODViewModel();
              readonly static GroupPlanningViewModel _groupPlanningViewModel = new GroupPlanningViewModel();
              readonly static LogbookVerificationViewModel _logbookVerificationViewModel = new LogbookVerificationViewModel();
              readonly static ResetPasswordViewModel _resetPasswordViewModel = new ResetPasswordViewModel();
              readonly static FreezeUnfreezeViewModel _freezeUnfreezeViewModel = new FreezeUnfreezeViewModel();
              readonly AddCrewDetailModel _addCrewDetailModel = new AddCrewDetailModel();
              readonly static AddNextCrewDetailModel _addNextCrewDetailModel = new AddNextCrewDetailModel();
              readonly static AddNextYoungCrewDetailModel _addNextYoungCrewDetailModel = new AddNextYoungCrewDetailModel();
              readonly static AddOverTimeCrewDetailModel _addOverTimeCrewDetailModel = new AddOverTimeCrewDetailModel();
              readonly static AddHODViewModel _addHODViewModel = new AddHODViewModel();

              //readonly static HoliDayListViewModel _holiDayListViewModel = new HoliDayListViewModel(BmsMessage);

              public MyCommand<string> NavCommand { get; private set; }
              public MyCommand<HoliDayGroupNameClass> NavCommand1 { get; private set; }
              public MyCommand<CrewDetailClass> NavCommand2 { get; private set; }
              public MyCommand<UserAccessClass> NavCommand3 { get; private set; }



              private void OnNavCommand(string destination)
              {
                     if (destination == "NextCrewDetail")
                     {
                            StaticHelper.LastMenuItem = "CrewDetail";
                            if (CheckErrorMessage.CheckErrorMessages)
                            {
                                   if (CheckErrorMessage.chkyoungs)
                                          CurrentViewModelCrew = _addNextYoungCrewDetailModel;
                                   else
                                          CurrentViewModelCrew = _addNextCrewDetailModel;
                                   StaticHelper.Editing = true;
                            }
                     }
                     if (destination == "OverTimeCrewDetail")
                     {
                            StaticHelper.LastMenuItem = "CrewDetail";
                            if (CheckErrorMessage.CheckErrorMessages)
                                   CurrentViewModelCrew = _addOverTimeCrewDetailModel;
                            StaticHelper.Editing = true;
                     }

                     if (StaticHelper.Editing == false)
                     {
                            switch (destination)
                            {

                                   case "CrewRank":
                                          CurrentViewModelCrew = _crewRankViewModel;
                                          break;
                                   case "Department":

                                          CurrentViewModelCrew = _departmentViewModel;

                                          break;
                                   case "HolidayGroup":

                                          CurrentViewModelCrew = _holiDayGroupViewModel;

                                          break;
                                   case "HOD":
                                          StaticHelper.LastMenuItem = "HOD";
                                          CurrentViewModelCrew = _hODViewModel;
                                          break;

                                   case "GroupPlanning":
                                          CurrentViewModelCrew = _groupPlanningViewModel;
                                          break;

                                   case "LogbookVerification":
                                          CurrentViewModelCrew = _logbookVerificationViewModel;
                                          break;
                                   case "ResetPassword":
                                          {
                                                 ResetPasswordViewModel.erinfo = 2;
                                                 CurrentViewModelCrew = _resetPasswordViewModel;

                                          }
                                          break;
                                   case "FreezeUnfreeze":
                                          CurrentViewModelCrew = _freezeUnfreezeViewModel;
                                          break;
                                   case "AddCrewDetail":
                                          {
                                                 StaticHelper.LastMenuItem = "CrewDetail";
                                                 StaticHelper.Editing = true;
                                                 AddCrewDetailModel.erinfo = 2;
                                                 _addCrewDetailModel.resetCrewDetail();
                                                 CurrentViewModelCrew = _addCrewDetailModel;
                                                 
                                          }
                                          break;

                                   case "AddCrewDetails":
                                          {
                                                 StaticHelper.LastMenuItem = "CrewDetail";
                                                 _addCrewDetailModel.resetCrewDetail();
                                                 CurrentViewModelCrew = _addCrewDetailModel;
                                          }
                                          break;

                                   case "NextCrewDetail":
                                          StaticHelper.LastMenuItem = "CrewDetail";
                                          if (CheckErrorMessage.CheckErrorMessages)
                                          {
                                                 if (CheckErrorMessage.chkyoungs)
                                                        CurrentViewModelCrew = _addNextYoungCrewDetailModel;
                                                 else
                                                        CurrentViewModelCrew = _addNextCrewDetailModel;
                                                 StaticHelper.Editing = true;
                                          }

                                          break;
                                   case "OverTimeCrewDetail":
                                          StaticHelper.LastMenuItem = "CrewDetail";
                                          if (CheckErrorMessage.CheckErrorMessages)
                                                 CurrentViewModelCrew = _addOverTimeCrewDetailModel;
                                          break;
                                   case "AddHOD":
                                          {
                                                 StaticHelper.LastMenuItem = "HOD";
                                                 StaticHelper.Editing = true;
                                                 CurrentViewModelCrew = _addHODViewModel;
                                          }
                                          break;

                                   case "CrewDetail":
                                   default:
                                          StaticHelper.LastMenuItem = "CrewDetail";
                                          if (CheckErrorMessage.CheckErrorMessages == true)
                                          {

                                          }
                                          else
                                          {
                                                 CurrentViewModelCrew = _crewDetailViewModel;

                                          }

                                          break;
                            }
                     }
                     else
                     {
                            if (destination == "NextCrewDetail")
                            {
                                  // StaticHelper.Editing = false;
                            }
                            else if(destination== "OverTimeCrewDetail")
                            { }
                            else
                            {


                                   if (MessageBox.Show("Do you want to leave this page without saving record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                   {
                                          // Yes
                                          StaticHelper.Editing = false;
                                          CheckErrorMessage.CheckErrorMessages = false;
                                          switch (destination)
                                          {

                                                 case "CrewRank":
                                                        CurrentViewModelCrew = _crewRankViewModel;
                                                        break;
                                                 case "Department":

                                                        CurrentViewModelCrew = _departmentViewModel;

                                                        break;
                                                 case "HolidayGroup":
                                                        CurrentViewModelCrew = _holiDayGroupViewModel;

                                                        break;
                                                 case "HOD":
                                                        StaticHelper.LastMenuItem = "HOD";
                                                        CurrentViewModelCrew = _hODViewModel;
                                                        break;

                                                 case "GroupPlanning":
                                                        CurrentViewModelCrew = _groupPlanningViewModel;
                                                        break;

                                                 case "LogbookVerification":
                                                        CurrentViewModelCrew = _logbookVerificationViewModel;

                                                        break;
                                                 case "ResetPassword":
                                                        {
                                                               ResetPasswordViewModel.erinfo = 2;
                                                               CurrentViewModelCrew = _resetPasswordViewModel;

                                                        }
                                                        break;
                                                 case "FreezeUnfreeze":
                                                        CurrentViewModelCrew = _freezeUnfreezeViewModel;

                                                        break;
                                                 case "AddCrewDetail":
                                                        {
                                                               StaticHelper.LastMenuItem = "CrewDetail";
                                                               AddCrewDetailModel.erinfo = 2;
                                                               CurrentViewModelCrew = _addCrewDetailModel;
                                                        }
                                                        break;

                                                 case "AddCrewDetails":
                                                        StaticHelper.LastMenuItem = "CrewDetail";
                                                        CurrentViewModelCrew = _addCrewDetailModel;
                                                        break;

                                                 case "NextCrewDetail":
                                                        {
                                                               StaticHelper.LastMenuItem = "CrewDetail";
                                                               if (CheckErrorMessage.CheckErrorMessages)
                                                               {
                                                                      if (CheckErrorMessage.chkyoungs)
                                                                             CurrentViewModelCrew = _addNextYoungCrewDetailModel;
                                                                      else
                                                                             CurrentViewModelCrew = _addNextCrewDetailModel;
                                                                      StaticHelper.Editing = true;
                                                               }
                                                        }
                                                        break;
                                                 case "OverTimeCrewDetail":
                                                        StaticHelper.LastMenuItem = "CrewDetail";
                                                        if (CheckErrorMessage.CheckErrorMessages)
                                                               CurrentViewModelCrew = _addOverTimeCrewDetailModel;
                                                        break;
                                                 case "AddHOD":
                                                        StaticHelper.LastMenuItem = "HOD";
                                                        CurrentViewModelCrew = _addHODViewModel;
                                                        break;

                                                 case "CrewDetail":
                                                 default:
                                                        StaticHelper.LastMenuItem = "CrewDetail";
                                                        if (CheckErrorMessage.CheckErrorMessages == true)
                                                        {

                                                        }
                                                        else
                                                        {

                                                               CurrentViewModelCrew = _crewDetailViewModel;

                                                        }

                                                        break;
                                          }
                                   }
                                   else
                                   {
                                          // No  
                                          CrewManagementView.RefreshMenucolor(StaticHelper.LastMenuItem);

                                   }
                            }
                     }


              }


              public void Confirmation()
              {
                     // var mes = 
                     // var abc =  MessageBox.Show("Do you want to leave this page without saving record?", MessageBoxButton.YesNo, MessageBoxImage.Question) == DialogResult
                     // (MessageBox.Show("Do you want to leave this page without saving record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                     var result = MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                     if (result == MessageBoxResult.Yes)
                     {

                     }

                     if (MessageBox.Show("Do you want to leave this page without saving record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                     {
                     }
              }

              private void AddHolidayList(HoliDayGroupNameClass holi)
              {

                     HoliProperty.GroupName = holi.GroupName;
                     HoliProperty.Gid = holi.Id;
                     CurrentViewModelCrew = new HoliDayListViewModel();
              }

              private void AddCrewDetailList(CrewDetailClass cdc)
              {
                     CurrentViewModelCrew = new AddCrewDetailModel(cdc);

              }

              private void AddUserAccess(UserAccessClass obj)
              {

                     CurrentViewModelCrew = new AddHODViewModel(obj);
              }
              public override void Cleanup()
              {
                     _crewDetailViewModel.Cleanup();
                     _crewRankViewModel.Cleanup();
                     _departmentViewModel.Cleanup();
                     _holiDayGroupViewModel.Cleanup();
                     _hODViewModel.Cleanup();
                     _groupPlanningViewModel.Cleanup();
                     _resetPasswordViewModel.Cleanup();
                     _freezeUnfreezeViewModel.Cleanup();
                     _addCrewDetailModel.Cleanup();
                     _addNextCrewDetailModel.Cleanup();
                     _addOverTimeCrewDetailModel.Cleanup();
                     // _holiDayListViewModel.Cleanup();

              }

       }

       public class HoliProperty
       {
              public static int Gid { get; set; }
              public static string GroupName { get; set; }
       }
}
