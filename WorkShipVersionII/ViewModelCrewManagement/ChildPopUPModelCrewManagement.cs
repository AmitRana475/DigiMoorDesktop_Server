using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewModelAdministration;
using WorkShipVersionII.ViewsAdministration;
using WorkShipVersionII.ViewsCrewManagement;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class ChildPopUPModelCrewManagement : ViewModelBase
    {
        public RelayCommand ShowChildWindowAddRank { get;}
        public RelayCommand ShowChildWindowAddDepartment { get;}
        public RelayCommand ShowChildWindowAddHolyDayGroup { get; }
        public RelayCommand ShowChildWindowAddHolyDayList { get; }
        public RelayCommand ShowChildWindowCommandGroupPlanning { get; }
        public RelayCommand ShowChildWindowCommandAddCurrency { get;  }
        public RelayCommand ShowChildWindowCommandLicenceRenew { get; }
        public RelayCommand ShowChildWindowAddLogoBook { get; }



        public ChildPopUPModelCrewManagement()
        {
            ShowChildWindowAddRank = new RelayCommand(() => ShowChildWindowRank()); //ChildWindows for Add Rank
            ShowChildWindowAddDepartment = new RelayCommand(ShowChildWindowDepartment); //ChildWindows for Add Department
            ShowChildWindowAddHolyDayGroup = new RelayCommand(ShowChildWindowHolyDayGroup); //ChildWindows for Add HolyDayGroup
            ShowChildWindowAddHolyDayList = new RelayCommand(ShowChildWindowHolyDayList); //ChildWindows for Add HolyDayList
            ShowChildWindowCommandGroupPlanning = new RelayCommand(ShowChildWindowAddGroupPlan); //ChildWindows for Add Department
            ShowChildWindowCommandAddCurrency = new RelayCommand(ShowChildWindowAddCurrency); //ChildWindows for Add Currency
            ShowChildWindowCommandLicenceRenew = new RelayCommand(ShowChildWindowRenewLicence);
            ShowChildWindowAddLogoBook = new RelayCommand(ShowChildWindowLogoBookMethod);


        }

        private void ShowChildWindowLogoBookMethod()
        {
            AddLogbookViewModel vm = new AddLogbookViewModel();
            ChildWindowManager.Instance.ShowChildWindow(new AddLogbookView() { DataContext = vm });
        }

        private void ShowChildWindowRank()
        {
            AddRankViewModel vm = new AddRankViewModel();
            ChildWindowManager.Instance.ShowChildWindow(new AddRankView() { DataContext = vm });
        }

        private void ShowChildWindowDepartment()
        {
            AddDepartmentViewModel vm = new AddDepartmentViewModel();
            ChildWindowManager.Instance.ShowChildWindow(new AddDepartmentView() { DataContext = vm });
        }

        private void ShowChildWindowHolyDayGroup()
        {
            AddHoliDayGroupViewModel vm = new AddHoliDayGroupViewModel();
            ChildWindowManager.Instance.ShowChildWindow(new AddHoliDayGroupView() { DataContext = vm });
        }

        private void ShowChildWindowHolyDayList()
        {
            AddHoliDayListViewModel vm = new AddHoliDayListViewModel();
            ChildWindowManager.Instance.ShowChildWindow(new AddHoliDayListView() { DataContext = vm });
        }
        private void ShowChildWindowAddGroupPlan()
        {
            AddGroupPlanningViewModel vm = new AddGroupPlanningViewModel();
            ChildWindowManager.Instance.ShowChildWindow(new AddGroupPlanningView() { DataContext = vm });
        }

        private void ShowChildWindowAddCurrency()
        {
            AddCurrencyViewModel vm = new AddCurrencyViewModel();
            ChildWindowManager.Instance.ShowChildWindow(new AddCurrencyView() { DataContext = vm });
        }

        private void ShowChildWindowRenewLicence()
        {
            RenewLicenceViewModel vm = new RenewLicenceViewModel();
            ChildWindowManager.Instance.ShowChildWindow(new RenewLicenceView() { DataContext = vm });
        }


        public override void Cleanup()
        {
            base.Cleanup();
        }
        

    }
}
