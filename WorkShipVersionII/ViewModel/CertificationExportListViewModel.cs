using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace WorkShipVersionII.ViewModel
{
    public class CertificationExportListViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public CertificationExportListViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            _cancelCertificateCommand = new RelayCommand(CancelCertificateExportList);
        }

        
        private ICommand _cancelCertificateCommand;
        public ICommand CancelCommand
        {
            get { return _cancelCertificateCommand; }
        }





        private void CancelCertificateExportList()
        {
            try
            {
                //_AddCertificates = new CertificatesClass();
                //RaisePropertyChanged("AddCertificates");

                //CertificationViewModel.loadCerticate.Clear();
                //LoadMethod();
            
                ChildWindowManager.Instance.CloseChildWindow();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


    }
}
