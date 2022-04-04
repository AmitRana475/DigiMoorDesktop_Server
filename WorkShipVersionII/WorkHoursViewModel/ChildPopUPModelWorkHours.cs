using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class ChildPopUPModelWorkHours : ViewModelBase
       {
              // public RelayCommand ShowChildWindowAddLogoBook { get; }
              public RelayCommand ShowChildWindowAddMooringWinch { get; }

              public RelayCommand ShowChildViewAssignRopeToWinch { get; }
              public RelayCommand ShowChildWindowAddDamagedRope { get; }

              public RelayCommand ShowChildWindowAddRopeDisposal { get; }
              public RelayCommand ShowChildWindowAddRopeTailDisposal { get; }
              public RelayCommand ShowChildWindowAddLooseEDisposal { get; }

             

              //  public RelayCommand ShowVerifyPlaner { get; }
              public ChildPopUPModelWorkHours()
              {
                     //ShowChildWindowAddLogoBook = new RelayCommand(ShowChildWindowLogoBookMethod);
                     ShowChildWindowAddMooringWinch = new RelayCommand(ShowChildWindowMooringWinchMethod);
                     ShowChildWindowAddDamagedRope = new RelayCommand(ShowChildWindowAddDamagedRopeMethod);

                     ShowChildViewAssignRopeToWinch = new RelayCommand(ShowChildAssignRopeToWinchMethod);

                     ShowChildWindowAddRopeDisposal = new RelayCommand(ShowChildWindowRopeDisposalMethod);
                     ShowChildWindowAddRopeTailDisposal = new RelayCommand(ShowChildWindowRopeTailDisposalMethod);
                     ShowChildWindowAddLooseEDisposal = new RelayCommand(ShowChildWindowLooseEDisposalMethod);
                     
                     //ShowVerifyPlaner = new RelayCommand(ShowChildwindowLogbookPlaner);
              }


              private void ShowChildWindowMooringWinchMethod()
              {
                     AddMooringWinchDetailsViewModel vm = new AddMooringWinchDetailsViewModel();
            
                     ChildWindowManager.Instance.ShowChildWindow(new AddMooringWinchDetail() { DataContext = vm });
              }


              private void ShowChildAssignRopeToWinchMethod()
              {
                     ViewModelAssignRopeToWinch vm = new ViewModelAssignRopeToWinch();
          
                     ChildWindowManager.Instance.ShowChildWindow(new ViewAssignRopetoWinch() { DataContext = vm });
              }

              private void ShowChildWindowRopeDisposalMethod()
              {
                     AddRopeDisposalViewModel vm = new AddRopeDisposalViewModel();
                     vm.GetRopeType();
            vm.resetform();
                     ChildWindowManager.Instance.ShowChildWindow(new AddRopeDisposalView() { DataContext = vm });
              }
              private void ShowChildWindowRopeTailDisposalMethod()
              {
                     AddTailDisposalViewModel vm = new AddTailDisposalViewModel();
            vm.resetform();
            ChildWindowManager.Instance.ShowChildWindow(new AddTailDisposalView() { DataContext = vm });
              }


        private void ShowChildWindowLooseEDisposalMethod()
        {
            AddLooseEDisposalViewModel vm = new AddLooseEDisposalViewModel();
            vm.refreshform();
            ChildWindowManager.Instance.ShowChildWindow(new AddLooseEDisposalView() { DataContext = vm });
        }

              private void ShowChildWindowAddDamagedRopeMethod()
              {
                     //MooringOPDamagedRopeViewModel vm = new MooringOPDamagedRopeViewModel();
                     //ChildWindowManager.Instance.ShowChildWindow(new MooringOPDamagedRopeView() { DataContext = vm });
              }

       }
}
