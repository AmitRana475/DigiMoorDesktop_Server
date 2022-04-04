using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using System.Windows.Threading;
using WorkShipVersionII.ViewModelCrewManagement;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class MainViewModelWorkHours : ViewModelBase
       {
              public ICommand HelpCommand { get; private set; }
              public MainViewModelWorkHours()
              {


                     CurrentViewModelWorkHours = MainViewModelWorkHours._mooringOPRListingViewModel;

                     NavCommand = new MyCommand<string>(OnNavCommand);
                     NavCommand2 = new MyCommand<MooringWinchRopeClass>(AddRopeDetailList);

                     NavCommand32 = new MyCommand<MooringWinchRopeClass>(AddRopeTailDetailList);

                     NavCommand30 = new MyCommand<MooringLooseEquipInspectionClass>(LooseEInspectionDetailList);

                     NavCommand20 = new MyCommand<TotalInspections>(ViewMooringRopeInspection);
                     NavCommand21 = new MyCommand<TotalInspections>(ViewMooringTailInspection);

                     NavCommandRopeViewDetail = new MyCommand<MooringWinchRopeClass>(RopeDetailList);

                     NavCommandMooringRope = new MyCommand<MooringWinchRopeClass>(ViewRopeDetailList);

                     NavCommand10 = new MyCommand<RopeDiscardRecordClass>(RopeDiscardList);

                     NavCommand7 = new MyCommand<MOperationBirthDetail>(MOpertaionBirth);
                     NavCommand3 = new MyCommand<AssignModuleToWinchClass>(AssignRopeWinchDetailList);

                     NavCommand6 = new MyCommand<AssignLooseEquipTypeClass>(AssignLooseEquipWinchDetailList);

                     NavCommand4 = new MyCommand<RopeEndtoEndClass>(RopeEndtoend);
                     NavCommand5 = new MyCommand<TotalInspections>(EditRopeInspection);

                     NavCommand55 = new MyCommand<TotalInspections>(EditRopeTailInspection);


                     AddMooringWinch = new RelayCommand(AddMooringWinchModel);
                     MooringWinchDetail = new RelayCommand(MooringWinchDetailModel);
                     MooringWinchRope = new RelayCommand(MooringWinchRopeModel);




              }


              private void AddRopeDetailList(MooringWinchRopeClass cdc)
              {

                     _addmooringWinchRopeModel = new AddMooringWinchRopeViewModel(cdc);
                     CurrentViewModelWorkHours = _addmooringWinchRopeModel;

              }

              private void AddRopeTailDetailList(MooringWinchRopeClass cdc)
              {

                     _addmooringWinchtailModel = new AddMooringWinchTailViewModel(cdc);
                     CurrentViewModelWorkHours = _addmooringWinchtailModel;

              }

              private void LooseEInspectionDetailList(MooringLooseEquipInspectionClass cdc)
              {

                     _leinspection = new LooseEquipInspectionDetailsViewModel(cdc);
                     CurrentViewModelWorkHours = _leinspection;

              }

              private void ViewMooringRopeInspection(TotalInspections cdc)
              {

                     _viewmooringRopeInspectionViewModel = new ModelViewMooringRopeInspection(cdc);
                     CurrentViewModelWorkHours = _viewmooringRopeInspectionViewModel;

              }

              private void ViewMooringTailInspection(TotalInspections cdc)
              {

                     _viewmooringTailInspectionViewModel = new ModelViewMooringTailInspection(cdc);
                     CurrentViewModelWorkHours = _viewmooringTailInspectionViewModel;

              }

              private void RopeDetailList(MooringWinchRopeClass cdc)
              {
                     StaticHelper.RopeId_Status = cdc.Id;

                     StaticHelper.RopeTail = cdc.RopeTail;
                     _ropedetaillistview = new RopeDetailListViewModel();
                     CurrentViewModelWorkHours = _ropedetaillistview;

              }
              private void ViewRopeDetailList(MooringWinchRopeClass cdc)
              {

                     viewmooring = new ViewModelMooringRopeDetail(cdc);
                     CurrentViewModelWorkHours = viewmooring;

                     if (cdc.RopeTail == 0)
                     {
                            StaticHelper.RopeTailId = 0;
                     }
                     if (cdc.RopeTail == 1)
                     {
                            StaticHelper.RopeTailId = 1;
                     }



              }


              private void RopeDiscardList(RopeDiscardRecordClass cdc)
              {

                     _ropediscardview = new RopeDiscardRecordViewModel(cdc);
                     CurrentViewModelWorkHours = _ropediscardview;



              }

              private void MOpertaionBirth(MOperationBirthDetail cdc)
              {

                     _mooringOPRModel = new MooringOPRViewModel(cdc);
                     CurrentViewModelWorkHours = _mooringOPRModel;
              }



              private void AssignRopeWinchDetailList(AssignModuleToWinchClass cdc)
              {

                     _assmooringWinchRopeModel = new AssignRopeToWinchViewModel(cdc);
                     CurrentViewModelWorkHours = _assmooringWinchRopeModel;

              }

              private void AssignLooseEquipWinchDetailList(AssignLooseEquipTypeClass cdc)
              {

                     _assignLooseEtoWnch = new AssignLooseEquipToWinchViewModel(cdc);
                     CurrentViewModelWorkHours = _assignLooseEtoWnch;



              }

              private void RopeEndtoend(RopeEndtoEndClass cdc)
              {

                     _addropeendtoendModel = new AddRopeEndtoEndViewModel(cdc);
                     CurrentViewModelWorkHours = _addropeendtoendModel;
              }

              private void EditRopeInspection(TotalInspections totalins)
              {
                     _mooringRopeInspectionViewModel = new MooringRopeInspectionViewModel(totalins);
                     CurrentViewModelWorkHours = _mooringRopeInspectionViewModel;
              }
              private void EditRopeTailInspection(TotalInspections totalins)
              {
                     _mooringRopeTailInspectionViewModel = new MooringTailInspectionViewModel(totalins);
                     CurrentViewModelWorkHours = _mooringRopeTailInspectionViewModel;
              }
              private void CrossShiftingWinch(CrossShiftingWinchClass cdc)
              {

                     _addcrossshiftingModel = new AddCrossShiftingWinchViewModel(cdc);
                     CurrentViewModelWorkHours = _addcrossshiftingModel;

              }

              private void RopeSplicing(RopeSplicingClass cdc)
              {

                     _addropesplicing = new AddRopeSplicingViewModel(cdc);
                     CurrentViewModelWorkHours = _addropesplicingModel;



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

              private ViewModelBase _addmooringwinch;
              public ViewModelBase Addmooringwinch
              {
                     get
                     {
                            return _addmooringwinch;
                     }
                     set
                     {
                            Set(ref _addmooringwinch, value);


                     }
              }


              private ViewModelBase _addcrossshiftingwinch;
              public ViewModelBase AddCrossShiftingWinch
              {
                     get
                     {
                            return _addcrossshiftingwinch;
                     }
                     set
                     {
                            Set(ref _addcrossshiftingwinch, value);


                     }
              }
              private ViewModelBase _addropesplicing;
              public ViewModelBase AddRopeSplicing
              {
                     get
                     {
                            return _addropesplicing;
                     }
                     set
                     {
                            Set(ref _addropesplicing, value);


                     }
              }
              private ViewModelBase _addropedamage;
              public ViewModelBase AddRopeDamage
              {
                     get
                     {
                            return _addropedamage;
                     }
                     set
                     {
                            Set(ref _addropedamage, value);


                     }
              }

              private ViewModelBase _asslooseE;
              public ViewModelBase AssignLooseE
              {
                     get
                     {
                            return _asslooseE;
                     }
                     set
                     {
                            Set(ref _asslooseE, value);


                     }
              }

              private ViewModelBase _addropecropping;
              public ViewModelBase AddRopeCropping
              {
                     get
                     {
                            return _addropecropping;
                     }
                     set
                     {
                            Set(ref _addropecropping, value);


                     }
              }
              private ViewModelBase _addmropeendtoend;
              public ViewModelBase AddRopeEndtoEnd
              {
                     get
                     {
                            return _addmropeendtoend;
                     }
                     set
                     {
                            Set(ref _addmropeendtoend, value);


                     }
              }

              private ViewModelBase _mooringwinchdetail;
              public ViewModelBase MooringwinchDetail
              {
                     get
                     {
                            return _mooringwinchdetail;
                     }
                     set
                     {
                            Set(ref _mooringwinchdetail, value);


                     }
              }

              private ViewModelBase _addLooseE;
              public ViewModelBase AddLooseE
              {
                     get
                     {
                            return _addLooseE;
                     }
                     set
                     {
                            Set(ref _addLooseE, value);


                     }
              }

              private ViewModelBase _looseEList;
              public ViewModelBase LooseEList
              {
                     get
                     {
                            return _looseEList;
                     }
                     set
                     {
                            Set(ref _looseEList, value);


                     }
              }

              private ViewModelBase _addmooringwinchrope;
              public ViewModelBase AddMooringWinchRope
              {
                     get
                     {
                            return _addmooringwinchrope;
                     }
                     set
                     {
                            Set(ref _addmooringwinchrope, value);


                     }
              }

              private ViewModelBase _mooringwinchrope;
              public ViewModelBase MooringwinchRope
              {
                     get
                     {
                            return _mooringwinchrope;
                     }
                     set
                     {
                            Set(ref _mooringwinchrope, value);


                     }
              }

              public static MyCommand<string> NavCommand { get; private set; }

              public ICommand AddMooringWinch { get; private set; }
              public MyCommand<MooringWinchRopeClass> NavCommand2 { get; private set; }

              public MyCommand<MooringWinchRopeClass> NavCommand32 { get; private set; }

              public MyCommand<MooringLooseEquipInspectionClass> NavCommand30 { get; private set; }

              public MyCommand<TotalInspections> NavCommand20 { get; private set; }
              public MyCommand<TotalInspections> NavCommand21 { get; private set; }

              public MyCommand<MooringWinchRopeClass> NavCommandRopeViewDetail { get; private set; }

              public MyCommand<MooringWinchRopeClass> NavCommandMooringRope { get; private set; }

              public MyCommand<RopeDiscardRecordClass> NavCommand10 { get; private set; }
              public MyCommand<MOperationBirthDetail> NavCommand7 { get; private set; }

              public MyCommand<MooringOPDamagedRopeViewModel> NavCommand8 { get; private set; }
              public MyCommand<AssignModuleToWinchClass> NavCommand3 { get; private set; }

              public MyCommand<AssignLooseEquipTypeClass> NavCommand6 { get; private set; }
              public MyCommand<RopeEndtoEndClass> NavCommand4 { get; private set; }

              public MyCommand<TotalInspections> NavCommand5 { get; set; }
              public MyCommand<TotalInspections> NavCommand55 { get; set; }
              public ICommand MooringWinchDetail { get; private set; }
              public ICommand MooringWinchRope { get; private set; }
            


              readonly static AddMooringWinchDetailsViewModel _addMooringWinchDetailModel = new AddMooringWinchDetailsViewModel();
              readonly static AddMooringWinchRopeViewModel _addRopeDetailModel = new AddMooringWinchRopeViewModel();
              readonly static CrossShiftingWinchViewModel _crossshiftingwinchViewModel = new CrossShiftingWinchViewModel();
              readonly static AssignRopeToWinchViewModel _assignRopeToWinchViewModel = new AssignRopeToWinchViewModel();
              readonly static AssignTailToWinchViewModel _assignTailToWinchViewModel = new AssignTailToWinchViewModel();
              readonly static MooringTailInspectionViewModel _mooringTailInspectionViewModel = new MooringTailInspectionViewModel();
              readonly static LooseEquipInspectionDetailsViewModel _mooringLooseEInspectionViewModel = new LooseEquipInspectionDetailsViewModel();
              readonly static RopeInspectionListViewModel _ropeInspectionListViewModel = new RopeInspectionListViewModel();
              readonly static TailInspectionListViewModel _tailInspectionListViewModel = new TailInspectionListViewModel();
              readonly static MooringOPRViewModel _mooringOPRViewModel = new MooringOPRViewModel();
              readonly static MooringOPRListingViewModel _mooringOPRListingViewModel = new MooringOPRListingViewModel();
              readonly static AssignRopeToWinchDetailViewModel _assignRopeToWinchDetailViewModel = new AssignRopeToWinchDetailViewModel();
              readonly static AssignTailToWinchDetailViewModel _assignTailToWinchDetailViewModel = new AssignTailToWinchDetailViewModel();
              readonly static MooringWinchViewModel _mooringWinchDetailModel = new MooringWinchViewModel();
              readonly static RopeDisposalListViewModel _ropdisposalListViewModel = new RopeDisposalListViewModel();
              readonly static TailDisposalListViewModel _taildisposalListViewModel = new TailDisposalListViewModel();
              readonly static LooseEDisposalListViewModel _looseEdisposalListViewModel = new LooseEDisposalListViewModel();
              readonly static MooringWinchRopeViewModel _mooringWinchRopeModel = new MooringWinchRopeViewModel();
              readonly static MooringWinchTailViewModel _mooringWinchTailModel = new MooringWinchTailViewModel();
              readonly static AddMooringWinchTailViewModel _addmooringWinchTailModel = new AddMooringWinchTailViewModel();
              readonly static AddRopeSplicingViewModel _addropesplicingModel = new AddRopeSplicingViewModel();
              readonly static AddTailSplicingViewModel _addtailsplicingModel = new AddTailSplicingViewModel();
              readonly static RopeSplicingViewModel _ropesplicingModel = new RopeSplicingViewModel();
              readonly static TailSplicingViewModel _tailsplicingModel = new TailSplicingViewModel();
              readonly static LooseETypeViewModel _looseetype = new LooseETypeViewModel();
              readonly static AddLooseEquipmentViewModel _addLooseEModel = new AddLooseEquipmentViewModel();
              readonly static LooseEquipmentListViewModel _looseEModel = new LooseEquipmentListViewModel();
              readonly static AssignLooseEquipToWinchDetailsViewModel _assignLooseEModel = new AssignLooseEquipToWinchDetailsViewModel();
              readonly static AddRopeCroppingViewModel _addropecroppingModel = new AddRopeCroppingViewModel();
              readonly static RopeCroppingListViewModel _ropecroppingModel = new RopeCroppingListViewModel();
              readonly static AddTailCroppingViewModel _addtailcroppingModel = new AddTailCroppingViewModel();
              readonly static TailCroppingListViewModel _tailcroppingModel = new TailCroppingListViewModel();
              readonly static AddRopeDamageRecordViewModel _addropedamageModel = new AddRopeDamageRecordViewModel();
              readonly static RopeDamageRecordViewModel _ropedamageModel = new RopeDamageRecordViewModel();
              readonly static AddTailDamageRecordViewModel _addtaildamageModel = new AddTailDamageRecordViewModel();
              readonly static TailDamageRecordViewModel _taildamageModel = new TailDamageRecordViewModel();
              readonly static LooseEquipDiscardRecordViewModel _looseEDiscardModel = new LooseEquipDiscardRecordViewModel();
              readonly static AddLooseEDamageRecordViewModel _addlooseEdamageModel = new AddLooseEDamageRecordViewModel();
              readonly static RopeDiscardListViewModel _ropediscardModel = new RopeDiscardListViewModel();
              readonly static TailDiscardListViewModel _taildiscardModel = new TailDiscardListViewModel();
              readonly static LooseEquipInspectionListViewModel _looseEInspectModel = new LooseEquipInspectionListViewModel();

              readonly static ViewModelInspectionSetting _inspectSettingModel = new ViewModelInspectionSetting();
              readonly static ViewModelLooseEInspecSetting _looseinspectSettingModel = new ViewModelLooseEInspecSetting();

              readonly static LooseEDamageRecordListViewModel _looseEdamageModel = new LooseEDamageRecordListViewModel();
              readonly static LooseEDiscardListViewModel _looseEdiscardModel = new LooseEDiscardListViewModel();
              readonly static RopeDiscardRecordViewModel _addropediscardModel = new RopeDiscardRecordViewModel();
              readonly static TailDiscardRecordViewModel _addtaildiscardModel = new TailDiscardRecordViewModel();

              readonly static RopeStatusViewModel rpstatusviewmodel = new RopeStatusViewModel();

              readonly static AddResidualLabTestViewModel addResidualLabTestViewmodel = new AddResidualLabTestViewModel();

              readonly static AddTailResidualLabTestViewModel addtailResidualLabTestViewmodel = new AddTailResidualLabTestViewModel();


              readonly static RopeEndtoEndViewModel _ropeendtoendModel = new RopeEndtoEndViewModel();

              readonly static ResidualLabTestListViewModel _residualListViewModel = new ResidualLabTestListViewModel();

              readonly static TailResidualLabTestListViewModel _tailresidualListViewModel = new TailResidualLabTestListViewModel();

              ModelViewMooringRopeInspection _viewmooringRopeInspectionViewModel = new ModelViewMooringRopeInspection();
              ModelViewMooringTailInspection _viewmooringTailInspectionViewModel = new ModelViewMooringTailInspection();
              MooringRopeInspectionViewModel _mooringRopeInspectionViewModel = new MooringRopeInspectionViewModel();
              MooringTailInspectionViewModel _mooringRopeTailInspectionViewModel = new MooringTailInspectionViewModel();
              AddRopeEndtoEndViewModel _addropeendtoendModel = new AddRopeEndtoEndViewModel();
              AssignRopeToWinchViewModel _assmooringWinchRopeModel = new AssignRopeToWinchViewModel();
              AddMooringWinchRopeViewModel _addmooringWinchRopeModel = new AddMooringWinchRopeViewModel();

              AddMooringWinchTailViewModel _addmooringWinchtailModel = new AddMooringWinchTailViewModel();

              LooseEquipInspectionDetailsViewModel _leinspection = new LooseEquipInspectionDetailsViewModel();

              RopeDetailListViewModel _ropedetaillistview = new RopeDetailListViewModel();

              ViewModelMooringRopeDetail viewmooring = new ViewModelMooringRopeDetail();

              RopeDiscardRecordViewModel _ropediscardview = new RopeDiscardRecordViewModel();
              MooringOPRViewModel _mooringOPRModel = new MooringOPRViewModel();
              AssignLooseEquipToWinchViewModel _assignLooseEtoWnch = new AssignLooseEquipToWinchViewModel();
              AddCrossShiftingWinchViewModel _addcrossshiftingModel = new AddCrossShiftingWinchViewModel();
              WinchBrakeTestRecordViewModel _WinchBrakeTR = new WinchBrakeTestRecordViewModel();
              WinchRotationSettingViewModel _WinchRotatSetting = new WinchRotationSettingViewModel();

              public static bool MooringOperationListingGoBK { get; set; } = false;

              public static bool CommonValue { get; set; } = false;


              private void OnNavCommand(string destination)
              {
                     Cleanup();
                     //CurrentViewModelWorkHours = _addMooringWinchDetailModel;
                     //CurrentViewModelWorkHours = _RecordWinchViewModel;

                     //4.9  ROPE RESIDUAL LAB TEST

                     switch (destination)
                     {
                            case "TailResidualLabTest":
                                   StaticHelper.HelpFor = @"4.10__TAIL_RESIDUAL_LAB_TEST.htm";
                                   addtailResidualLabTestViewmodel.GetRopeType();
                                   addtailResidualLabTestViewmodel.resetMooringRope();
                                   CurrentViewModelWorkHours = addtailResidualLabTestViewmodel;
                                   break;
                            case "TailResidualLabTestList":
                                   StaticHelper.HelpFor = @"4.10__TAIL_RESIDUAL_LAB_TEST.htm";
                                   _tailresidualListViewModel.GetResidualtestList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _tailresidualListViewModel;
                                   break;
                            case "ResidualLabTest":
                                   StaticHelper.HelpFor = @"4.9__LINE_RESIDUAL_LAB_TEST.htm";
                                   addResidualLabTestViewmodel.GetRopeType();
                                   addResidualLabTestViewmodel.resetform();
                                   CurrentViewModelWorkHours = addResidualLabTestViewmodel;
                                   break;
                            case "ResidualLabTestList":
                                   StaticHelper.HelpFor = @"4.9__LINE_RESIDUAL_LAB_TEST.htm";
                                   _residualListViewModel.GetResidualtestList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _residualListViewModel;
                                   break;
                            case "LooseInspectionSetting":
                                   StaticHelper.HelpFor = @"4.8__INSPECTION_DISCARD_CRITERIA.htm";
                                   CurrentViewModelWorkHours = _looseinspectSettingModel;
                                   break;
                            case "InspectionSetting":
                                   StaticHelper.HelpFor = @"4.8__INSPECTION_DISCARD_CRITERIA.htm";
                                   _inspectSettingModel.GetInspectionList();
                                   CurrentViewModelWorkHours = _inspectSettingModel;
                                   break;
                            case "LooseEInspectList":
                                   StaticHelper.HelpFor = @"4.4.1  LOOSE EQUIPMENT INSPECTION.htm";
                                   _looseEInspectModel.GetInspectionList(1, DateTime.Now.ToString("yyyy"));
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _looseEInspectModel;
                                   break;
                            case "RopeDiscardList":
                                   StaticHelper.HelpFor = @"4.2.8__LINE_DISCARD.htm";
                                   _ropediscardModel.GetRopeDiscardList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _ropediscardModel;
                                   break;
                            case "RopeTailDiscardList":
                                   StaticHelper.HelpFor = @"4.3.7  ROPE TAIL DISCARD.htm";
                                   _taildiscardModel.GetRopeDiscardList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _taildiscardModel;
                                   break;
                            case "LooseEDisposalList":
                                   StaticHelper.HelpFor = @"4.4.5  LOOSE EQUIPMENT DISPOSAL.htm";
                                   _looseEdisposalListViewModel.GetLooseEDisposalList();
                                   CurrentViewModelWorkHours = _looseEdisposalListViewModel;
                                   break;
                            case "RopeDisposalListView":
                                   StaticHelper.HelpFor = @"4.2.9__LINE_DISPOSAL.htm";
                                   _ropdisposalListViewModel.GetRopeDisposalList();
                                   CurrentViewModelWorkHours = _ropdisposalListViewModel;
                                   break;
                            case "RopeTailDisposalListView":
                                   StaticHelper.HelpFor = @"4.3.8  ROPE TAIL DISPOSAL.htm";
                                   _taildisposalListViewModel.GetRopeDisposalList();
                                   CurrentViewModelWorkHours = _taildisposalListViewModel;
                                   break;
                            case "LooseEDiscard":
                                   StaticHelper.HelpFor = @"4.4.4  LOOSE EQUIPMENT DISCARD.htm";
                                   _looseEDiscardModel.GetLooseEType();
                                   _looseEDiscardModel.GetOutofSReason();
                                   _looseEDiscardModel.GetAssRope();
                                   _looseEDiscardModel.refreshform();

                                   CurrentViewModelWorkHours = _looseEDiscardModel;
                                   break;
                            case "AddLooseEDamageR":
                                   StaticHelper.HelpFor = @"4.4.3  LOOSE EQUIPMENT DAMAGE.htm";
                                   _addlooseEdamageModel.GetAssRope();
                                   _addlooseEdamageModel.GetLooseEType();
                                   _addlooseEdamageModel.refreshform();
                                   CurrentViewModelWorkHours = _addlooseEdamageModel;
                                   break;
                            case "LooseEDamageRList":
                                   StaticHelper.HelpFor = @"4.4.3  LOOSE EQUIPMENT DAMAGE.htm";
                                   _looseEdamageModel.GetRopeDamageList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _looseEdamageModel;
                                   break;
                            case "LooseEDiscardList":
                                   StaticHelper.HelpFor = @"4.4.4  LOOSE EQUIPMENT DISCARD.htm";
                                   _looseEdiscardModel.GetLooseEDiscardList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _looseEdiscardModel;
                                   break;
                            case "AssignLooseEquipToWinch":
                                   _assignLooseEtoWnch.GetLooseEType();
                                   _assignLooseEtoWnch.GetAssRope();
                                   _assignLooseEtoWnch.ResetAssignLooseEToWinch();
                                   CurrentViewModelWorkHours = _assignLooseEtoWnch;
                                   break;
                            case "AssignLooseEquipDetails":
                                   StaticHelper.HelpFor = @"General\2.0 Notification\2.1 NOTIFICATIONS.htm";
                                   _assignLooseEModel.GetAssignList();
                                   CurrentViewModelWorkHours = _assignLooseEModel;
                                   break;
                            case "AddLooseEDetail":
                                   StaticHelper.HelpFor = @"4.4.2  LOOSE EQUIPMENT DETAILS.htm";
                                   _assignLooseEModel.GetAssignList();
                                   //_assignLooseEModel.ref();
                                   CurrentViewModelWorkHours = _looseetype;
                                   break;
                            case "LooseEDetails":
                                   StaticHelper.HelpFor = @"4.4.2  LOOSE EQUIPMENT DETAILS.htm";
                                   _looseEModel.GetJShackleList();
                                   _looseEModel.GetRopeTailList();
                                   _looseEModel.GetCStopperList();
                                   _looseEModel.GetChafeGuardList();
                                   _looseEModel.GetFireWireList();
                                   _looseEModel.GetWBKitList();
                                   _looseEModel.GetMRopeList();
                                   _looseEModel.GetTowingRopeList();
                                   _looseEModel.GetSuezRopeList();
                                   //_looseEModel.PennantRopeList();
                                  // _looseEModel.GrommetRopeList();
                                   CurrentViewModelWorkHours = _looseEModel;
                                   break;
                            case "MooringOperationListing":
                                   StaticHelper.HelpFor = @"4.1.1  Mooring Operation Records.htm";
                                   _mooringOPRListingViewModel.GetMooringOperationBirthD();
                                   bool ss = MooringOperationListingGoBK;

                                   if (ss == false)
                                   {
                                          StaticHelper.Autoportname = null;
                                          CurrentViewModelWorkHours = _mooringOPRListingViewModel;
                                   }
                                   break;
                            case "MooringOperation":
                                   StaticHelper.HelpFor = @"4.1 Mooring Operations.htm";
                                   MooringOPRViewModel.mOperationBirths = new MOperationBirthDetail();
                                   CurrentViewModelWorkHours = _mooringOPRViewModel;
                                   _mooringOPRViewModel.GetPortName();
                                   _mooringOPRViewModel.BindWinchList();
                                   _mooringOPRViewModel.GetFacilityName(" ");
                                   _mooringOPRViewModel.resetform();
                                   break;
                            case "RopeInspectionList":
                                   StaticHelper.HelpFor = @"4.2.1__MOORING_LINE_INSPECTION.htm";
                                   _ropeInspectionListViewModel.GetMooringInspection(DateTime.Now.Year.ToString());
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _ropeInspectionListViewModel;

                                   break;
                            case "MooringRopeInspection":
                                   StaticHelper.HelpFor = @"4.2.1__MOORING_LINE_INSPECTION.htm";
                                   _mooringRopeInspectionViewModel.GetMooringInspection();
                                   _mooringRopeInspectionViewModel.resetform();

                                   _mooringRopeInspectionViewModel = new MooringRopeInspectionViewModel();
                                   CurrentViewModelWorkHours = _mooringRopeInspectionViewModel;

                                   // CurrentViewModelWorkHours = _mooringRopeInspectionViewModel;
                                   break;
                            case "ViewMooringRopeInspection":
                                   StaticHelper.HelpFor = @"4.3.1  MOORING ROPE TAIL INSPECTION.htm";
                                   _viewmooringRopeInspectionViewModel.GetMooringInspection();
                                   CurrentViewModelWorkHours = _viewmooringRopeInspectionViewModel;
                                   break;

                            case "RopeTailInspectionList":
                                   StaticHelper.HelpFor = @"4.3.1  MOORING ROPE TAIL INSPECTION.htm";
                                   _tailInspectionListViewModel.GetMooringInspection(DateTime.Now.Year.ToString());
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _tailInspectionListViewModel;
                                   break;
                            case "MooringRopeTailInspection":
                                   StaticHelper.HelpFor = @"4.3.1  MOORING ROPE TAIL INSPECTION.htm";
                                   _mooringTailInspectionViewModel.GetMooringInspection();

                                   _mooringTailInspectionViewModel.resetform();

                                   _mooringRopeTailInspectionViewModel = new MooringTailInspectionViewModel();
                                   CurrentViewModelWorkHours = _mooringTailInspectionViewModel;
                                   //CurrentViewModelWorkHours = _mooringTailInspectionViewModel;
                                   break;
                            case "MooringLooseEInspection":
                                   StaticHelper.HelpFor = @"4.4.1  LOOSE EQUIPMENT INSPECTION.htm";

                                   _mooringLooseEInspectionViewModel.resetform();
                                   CurrentViewModelWorkHours = _mooringLooseEInspectionViewModel;

                                   break;
                            case "RopeDiscard":
                                   StaticHelper.HelpFor = @"4.2.8__LINE_DISCARD.htm";
                                   _addropediscardModel.GetRopeType();
                                   _addropediscardModel.GetAssRope();
                                   _addropediscardModel.GetOutofSReason();
                                   _addropediscardModel.resetform();
                                   _addropediscardModel.GetMooringOperation();
                                   CurrentViewModelWorkHours = _addropediscardModel;
                                   break;
                            case "RopeTailDiscard":
                                   StaticHelper.HelpFor = @"4.3.7  ROPE TAIL DISCARD.htm";
                                   _addtaildiscardModel.GetRopeType();
                                   _addtaildiscardModel.GetAssRope();
                                   _addtaildiscardModel.GetOutofSReason();
                                   _addtaildiscardModel.resetform();
                                   _addtaildiscardModel.GetMooringOperation();
                                   CurrentViewModelWorkHours = _addtaildiscardModel;
                                   break;
                            case "RopeStatusView":
                                   StaticHelper.HelpFor = @"4.6__LINE_STATUS.htm";
                                   rpstatusviewmodel.GetMooringWinchRopeList(0,"Line");
                                   rpstatusviewmodel.GetMooringWinchRopeList1(0,"Line");

                    CurrentViewModelWorkHours = rpstatusviewmodel;
                                   break;
                            case "RopeTailStatusView":
                                   StaticHelper.HelpFor = @"4.7  ROPE TAIL STATUS.htm";
                                   rpstatusviewmodel.GetMooringWinchRopeList(1, "RopeTail");
                    rpstatusviewmodel.GetMooringWinchRopeList1(1, "RopeTail");

                    CurrentViewModelWorkHours = rpstatusviewmodel;
                                   break;
                            case "AddRopeTailDamage":
                                   StaticHelper.HelpFor = @"4.3.6  ROPE TAIL DAMAGE.htm";
                                   _addtaildamageModel.GetRopeType();
                                   _addtaildamageModel.GetAssRope();
                                   _addtaildamageModel.resetRopeDamage();
                                   _addtaildamageModel.GetMooringOperation();
                                   CurrentViewModelWorkHours = _addtaildamageModel;
                                   break;
                            case "RopeTailDamageListView":
                                   StaticHelper.HelpFor = @"4.3.6  ROPE TAIL DAMAGE.htm";
                                   _taildamageModel.GetRopeDamageList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _taildamageModel;
                                   break;
                            case "AddRopeDamage":
                                   StaticHelper.HelpFor = @"4.2.7__ROPE_DAMAGE.htm";
                    _addropedamageModel.GetRopeType();
                    _addropedamageModel.GetAssRope();
                    _addropedamageModel.GetMooringOperation();
                    _addropedamageModel.resetRopeDamage();

                    CurrentViewModelWorkHours = _addropedamageModel;
                                   break;
                            case "RopeDamageListView":
                                   StaticHelper.HelpFor = @"4.2.7__ROPE_DAMAGE.htm";
                                   _ropedamageModel.GetRopeDamageList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _ropedamageModel;
                                   break;
                            case "AddRopeTailCropping":
                                   StaticHelper.HelpFor = @"4.3.5  ROPE TAIL CROPPING.htm";
                                   _addtailcroppingModel.GetRopeType();
                                   _addtailcroppingModel.GetAssRope();
                                   _addtailcroppingModel.resetRopeCropping();

                                   CurrentViewModelWorkHours = _addtailcroppingModel;
                                   break;
                            case "RopeTailCroppingListView":
                                   StaticHelper.HelpFor = @"4.3.5  ROPE TAIL CROPPING.htm";
                                   _tailcroppingModel.GetropecropeList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _tailcroppingModel;
                                   break;
                            case "AddRopeCropping":
                                   StaticHelper.HelpFor = @"4.2.6__ROPE_CROPPING.htm";
                                   _addropecroppingModel.GetRopeType();
                                   _addropecroppingModel.GetAssRope();
                                   _addropecroppingModel.resetRopeCropping();
                                   CurrentViewModelWorkHours = _addropecroppingModel;
                                   break;
                            case "RopeCroppingListView":
                                   StaticHelper.HelpFor = @"4.2.6__ROPE_CROPPING.htm";
                                   _ropecroppingModel.GetropecropeList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _ropecroppingModel;
                                   break;
                            case "AddRopeTailSplicing":
                                   StaticHelper.HelpFor = @"4.3.4  ROPE TAIL SPLICING.htm";
                                   _addtailsplicingModel.GetRopeType();
                                   _addtailsplicingModel.GetAssRope();
                                   _addtailsplicingModel.resetRopeSplicing();
                                   CurrentViewModelWorkHours = _addtailsplicingModel;
                                   break;
                            case "RopeTailSplicingListView":
                                   StaticHelper.HelpFor = @"4.3.4  ROPE TAIL SPLICING.htm";
                                   _tailsplicingModel.GetRopeSplicingList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _tailsplicingModel;
                                   break;
                            case "AddRopeSplicing":
                                   StaticHelper.HelpFor = @"4.2.5__LINE_SPLICING.htm";
                                   _addropesplicingModel.GetRopeType();
                                   _addropesplicingModel.GetAssRope();
                                   _addropesplicingModel.resetRopeSplicing();
                                   CurrentViewModelWorkHours = _addropesplicingModel;
                                   break;
                            case "RopeSplicingListView":

                                   StaticHelper.HelpFor = @"4.2.5__LINE_SPLICING.htm";
                                   _ropesplicingModel.GetRopeSplicingList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _ropesplicingModel;
                                   //StaticHelper.HelpFor = @"LMPR\rope\4.2.5  ROPE SPLICING.htm";
                                   //HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                                   break;
                            case "CrossShiftingWinch":
                                   CurrentViewModelWorkHours = _crossshiftingwinchViewModel;
                                   break;
                            case "AddCrossShiftingWinch":
                                   CurrentViewModelWorkHours = _addcrossshiftingModel;
                                   break;
                            case "AssignRopeToWinch":
                                   StaticHelper.HelpFor = @"4.2.3__ASSIGN_LINE_TO_WINCH_.htm";
                                   _assignRopeToWinchViewModel.GetRopeType();
                                   _assignRopeToWinchViewModel.GetAssRope();
                                   _assignRopeToWinchViewModel.ResetAssignRopeToWinch();
                                   CurrentViewModelWorkHours = _assignRopeToWinchViewModel;
                                   break;
                            case "AssignRopeToWinchDetail":

                                   StaticHelper.HelpFor = @"4.2.3__ASSIGN_LINE_TO_WINCH_.htm";
                                   _assignRopeToWinchDetailViewModel.GetAssignList();
                                   _assignRopeToWinchDetailViewModel.GetAssignList1();
                                   CurrentViewModelWorkHours = _assignRopeToWinchDetailViewModel;
                                   //StaticHelper.HelpFor = @"LMPR\rope\4.2.3  ASSIGN ROPE TO WINCH.htm";
                                   //HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
                                   break;

                            case "AssignRopeTailToWinch":
                                   StaticHelper.HelpFor = @"4.3.3  ASSIGN ROPE TAIL TO WINCH.htm";
                                   _assignTailToWinchViewModel.GetRopeType();
                                   _assignTailToWinchViewModel.GetAssRope();
                                   _assignTailToWinchViewModel.ResetAssignRopeToWinch();
                                   CurrentViewModelWorkHours = _assignTailToWinchViewModel;
                                   break;
                            case "AssignRopeTailToWinchDetail":
                                   StaticHelper.HelpFor = @"4.3.3  ASSIGN ROPE TAIL TO WINCH.htm";
                                   _assignTailToWinchDetailViewModel.GetAssignList();
                                   _assignTailToWinchDetailViewModel.GetAssignList1(); ;
                                   CurrentViewModelWorkHours = _assignTailToWinchDetailViewModel;
                                   break;
                            case "ViewMooringRopeDetail":

                                   CurrentViewModelWorkHours = viewmooring;
                                   break;
                            case "AddRopeDetail":
                                   StaticHelper.HelpFor = @"4.2.2__MOORING_LINE_DETAILS.htm";
                                   _addRopeDetailModel.GetRopeType();
                                   _addRopeDetailModel.GetManuFName();
                                   _addRopeDetailModel.GetDamageObserved();
                                   _addRopeDetailModel.GetOutofSReason();
                                   _addRopeDetailModel.resetMooringRope();
                                   CurrentViewModelWorkHours = _addmooringWinchRopeModel;
                                   break;
                            case "AddRopeTailDetail":
                                   StaticHelper.HelpFor = @"4.3.2  MOORING ROPE TAIL DETAILS.htm";
                                   _addmooringWinchTailModel.GetRopeType();
                                   _addmooringWinchTailModel.GetManuFName();
                                   _addmooringWinchTailModel.GetDamageObserved();
                                   _addmooringWinchTailModel.GetOutofSReason();
                                   _addmooringWinchTailModel.resetMooringRope();
                                   CurrentViewModelWorkHours = _addmooringWinchTailModel;
                                   break;
                            case "AddRopeEndtoEndView":
                                   StaticHelper.HelpFor = @"4.2.4_LINE_END_TO_END.htm";
                                   _addropeendtoendModel.GetRopeType();
                                   _addropeendtoendModel.GetAssRope(0);
                                   _addropeendtoendModel.resetRopeEndtoEnd();
                                   CurrentViewModelWorkHours = _addropeendtoendModel;
                                   break;
                            case "RopeEndtoEndListView":
                                   StaticHelper.HelpFor = @"4.2.4_LINE_END_TO_END.htm";
                                   _ropeendtoendModel.GetEndtoEndList();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _ropeendtoendModel;
                                   break;
                            case "AddMooringWinchView":
                                   StaticHelper.HelpFor = @"4.5  MOORING WINCH.htm";
                                   CurrentViewModelWorkHours = _mooringWinchDetailModel;
                                   _mooringWinchDetailModel.GetMooringWinchList();
                                   break;

                            case "MooringWinchRopeTailView":
                                   StaticHelper.HelpFor = @"4.3.2  MOORING ROPE TAIL DETAILS.htm";
                                   _mooringWinchTailModel.GetMooringWinchRopeList();
                                   _mooringWinchTailModel.GetMooringWinchRopeList2();
                                   if (CommonValue == false)
                                          CurrentViewModelWorkHours = _mooringWinchTailModel;
                                   break;
                            case "WinchBrakeTestRecord":
                                   StaticHelper.HelpFor = @"4.11_WINCH_BRAKE_TEST_RECORDS.htm";
                                   //  _mooringWinchTailModel.GetMooringWinchRopeList();
                                   CurrentViewModelWorkHours = _WinchBrakeTR;
                                   break;
                            case "WinchRotationSetting":
                                   StaticHelper.HelpFor = @"4.3.2  MOORING ROPE TAIL DETAILS.htm";
                                   CurrentViewModelWorkHours = _WinchRotatSetting;
                                   break;
                            case "MooringWinchRopeView":
                                   if (StaticHelper.RopeTailId == 1)
                                   {
                                          StaticHelper.HelpFor = @"4.3.2  MOORING ROPE TAIL DETAILS.htm";
                                          _mooringWinchTailModel.GetMooringWinchRopeList();
                                          _mooringWinchTailModel.GetMooringWinchRopeList2();
                                          if (CommonValue == false)
                                                 CurrentViewModelWorkHours = _mooringWinchTailModel;
                                          break;
                                   }
                                   else
                                   {
                                          StaticHelper.HelpFor = @"4.2.2__MOORING_LINE_DETAILS.htm";
                                          _mooringWinchRopeModel.GetMooringWinchRopeList();
                                          _mooringWinchRopeModel.GetMooringWinchRopeList2();
                                          if (CommonValue == false)
                                                 CurrentViewModelWorkHours = _mooringWinchRopeModel;
                                          break;
                                   }
                            default:
                                   CurrentViewModelWorkHours = _mooringOPRListingViewModel;
                                   break;

                     }
              }


              private void AddMooringWinchModel()
              {
                     Addmooringwinch = _addMooringWinchDetailModel;
              }

              private void ViewAssignRopetoWinchModel()
              {
                     Addmooringwinch = _addMooringWinchDetailModel;
              }

              private void AddRopeendttoendModel()
              {
                     AddRopeEndtoEnd = _addropeendtoendModel;
              }
              private void MooringWinchDetailModel()
              {
                     MooringwinchDetail = _mooringWinchDetailModel;
              }
              private void MooringWinchRopeModel()
              {
                     MooringwinchRope = _mooringWinchRopeModel;
              }
              private void AddMooringWinchRopeModel()
              {
                     AddMooringWinchRope = _addmooringWinchRopeModel;
              }
              private void AddCrossShiftingWinchModel()
              {
                     AddCrossShiftingWinch = _addcrossshiftingModel;
              }
              private void AddRopeSplicingModel()
              {
                     AddRopeSplicing = _addropesplicingModel;
              }
              private void AddRopeCroppingModel()
              {
                     AddRopeCropping = _addropecroppingModel;
              }

              private void AddRopeDamageModel()
              {
                     AddRopeDamage = _addropedamageModel;
              }

              private void AssignLooseEModel()
              {
                     AssignLooseE = _assignLooseEtoWnch;
              }

              public override void Cleanup()
              {

                     _addMooringWinchDetailModel.Cleanup();
                     _addRopeDetailModel.Cleanup();
                     _crossshiftingwinchViewModel.Cleanup();
                     _assignRopeToWinchViewModel.Cleanup();
                     _assignTailToWinchViewModel.Cleanup();
                     _mooringTailInspectionViewModel.Cleanup();
                     _mooringLooseEInspectionViewModel.Cleanup();
                     _ropeInspectionListViewModel.Cleanup();
                     _tailInspectionListViewModel.Cleanup();
                     _mooringOPRViewModel.Cleanup();
                     _mooringOPRListingViewModel.Cleanup();
                     _assignRopeToWinchDetailViewModel.Cleanup();
                     _assignTailToWinchDetailViewModel.Cleanup();
                     _mooringWinchDetailModel.Cleanup();
                     _ropdisposalListViewModel.Cleanup();
                     _taildisposalListViewModel.Cleanup();
                     _looseEdisposalListViewModel.Cleanup();
                     _mooringWinchRopeModel.Cleanup();
                     _mooringWinchTailModel.Cleanup();
                     _addmooringWinchTailModel.Cleanup();
                     _addropesplicingModel.Cleanup();
                     _addtailsplicingModel.Cleanup();
                     _ropesplicingModel.Cleanup();
                     _tailsplicingModel.Cleanup();
                     _looseetype.Cleanup();
                     _addLooseEModel.Cleanup();
                     _looseEModel.Cleanup();
                     _assignLooseEModel.Cleanup();
                     _addropecroppingModel.Cleanup();
                     _ropecroppingModel.Cleanup();
                     _addtailcroppingModel.Cleanup();
                     _tailcroppingModel.Cleanup();
                     _addropedamageModel.Cleanup();
                     _ropedamageModel.Cleanup();
                     _addtaildamageModel.Cleanup();
                     _taildamageModel.Cleanup();
                     _looseEDiscardModel.Cleanup();
                     _addlooseEdamageModel.Cleanup();
                     _ropediscardModel.Cleanup();
                     _taildiscardModel.Cleanup();
                     _looseEInspectModel.Cleanup();
                     _looseEdamageModel.Cleanup();
                     _addropediscardModel.Cleanup();
                     _addtaildiscardModel.Cleanup();
                     _ropeendtoendModel.Cleanup();

                     _mooringRopeInspectionViewModel.Cleanup();
                     _addropeendtoendModel.Cleanup();
                     _assmooringWinchRopeModel.Cleanup();
                     _addmooringWinchRopeModel.Cleanup();
                     _mooringOPRModel.Cleanup();
                     _assignLooseEtoWnch.Cleanup();
                     _addcrossshiftingModel.Cleanup();

                     //_mooringWinchDetailModel.Cleanup();
              }


       }
}
