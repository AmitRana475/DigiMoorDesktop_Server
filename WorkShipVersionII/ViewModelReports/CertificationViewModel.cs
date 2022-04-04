using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Data.Entity;
using WorkShipVersionII.Views;
using System.Collections.Generic;

namespace WorkShipVersionII.ViewModel
{
    public class CertificationViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public CertificationViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            LoadCerticate.Clear();
            sc.ObservableCollectionList(LoadCerticate, LoadMethod().ToList());

            deleteCommand = new RelayCommand<CertificatesClass>(DeleteMethod);
            editCommand = new RelayCommand<CertificatesClass>(EditMethod);
        }

        private void EditMethod(CertificatesClass obj)
        {
            AddCertificateViewModel vm = new AddCertificateViewModel(obj);
            ChildWindowManager.Instance.ShowChildWindow(new AddCertificateListView() { DataContext = vm });

        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }

        }

        private ICommand editCommand;
        public ICommand EditCommand
        {
            get
            {
                return editCommand;
            }

        }

        public static ObservableCollection<CertificatesClass> loadCerticate = new ObservableCollection<CertificatesClass>();
        public ObservableCollection<CertificatesClass> LoadCerticate
        {
            get
            {
                return loadCerticate;
            }
            set
            {
                loadCerticate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCerticate"));
            }

        }

        private List<CertificatesClass> LoadMethod()
        {
            var data = sc.Certificates.OrderBy(x => x.SrNo).ToList();


            //var ranklist = new ObservableCollection<CertificatesClass>();

            //foreach (var item in data.ToList())
            //{
            //    ranklist.Add(new CertificatesClass() { Id = item.Id, SrNo = item.SrNo, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, Remarks = item.Remarks });
            //}

            return data.ToList();
        }


        private void DeleteMethod(CertificatesClass obj)
        {
            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                CertificatesClass findrank = sc.Certificates.Where(x => x.Id == obj.Id).FirstOrDefault();
                if (findrank != null)
                {


                    sc.Entry(findrank).State = EntityState.Deleted;
                    sc.SaveChanges();


                    loadCerticate.Clear();
                    sc.ObservableCollectionList(LoadCerticate, LoadMethod().ToList());
                    RaisePropertyChanged("LoadCerticate");


                    MessageBox.Show("Record deleted successfully", "Certificate", MessageBoxButton.OK, MessageBoxImage.Information);

                    new NotificationViewModel();

                }
                else
                {

                    MessageBox.Show("Record is not found ", "Certificate", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
