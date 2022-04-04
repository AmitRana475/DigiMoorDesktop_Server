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
      
        public RelayCommand ShowChildWindowCommandLicenceRenew { get; }
       
        public ChildPopUPModelCrewManagement()
        {
           
            ShowChildWindowCommandLicenceRenew = new RelayCommand(ShowChildWindowRenewLicence);
           
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
