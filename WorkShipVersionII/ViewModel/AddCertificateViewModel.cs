using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WorkShipVersionII.ViewModel
{
    public class AddCertificateViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public AddCertificateViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            _savecertificateCommand = new RelayCommand<CertificatesClass>(SaveCertificateList);
            _cancelCertificateCommand = new RelayCommand(CancelCertificateList);
        }

        public AddCertificateViewModel(CertificatesClass obj)
        {
            try
            {
                if (sc == null)
                {
                    sc = new ShipmentContaxt();
                    sc.Configuration.ProxyCreationEnabled = false;
                }
                _AddCertificates = sc.Certificates.Where(x => x.Id == obj.Id).FirstOrDefault();
                RaisePropertyChanged("AddCertificates");

                _savecertificateCommand = new RelayCommand<CertificatesClass>(UpdateCertificateList);
                _cancelCertificateCommand = new RelayCommand(CancelCertificateList);
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }



        private ICommand _savecertificateCommand;
        public ICommand SaveCommand
        {
            get { return _savecertificateCommand; }
        }



        private ICommand _cancelCertificateCommand;
        public ICommand CancelCommand
        {
            get { return _cancelCertificateCommand; }
        }

        private static CertificatesClass _AddCertificates = new CertificatesClass();
        public CertificatesClass AddCertificates
        {
            get
            {


                return _AddCertificates;
            }
            set
            {
                _AddCertificates = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddCertificates"));



            }
        }





        private static Nullable<DateTime> _IssueDate = null;
        public Nullable<DateTime> IssueDate
        {
            get
            {
                if (_IssueDate == null)
                {
                    _IssueDate = DateTime.Now;
                }
                _AddCertificates.DOI = (DateTime)_IssueDate;
                return _IssueDate;
            }
            set
            {
                _IssueDate = value;
                RaisePropertyChanged("IssueDate");
            }
        }

        private Nullable<DateTime> _ExpiryDate = null;
        public Nullable<DateTime> ExpiryDate
        {
            get
            {
                if (_ExpiryDate == null)
                {
                    _ExpiryDate = DateTime.Now;
                }
                _AddCertificates.DOE = (DateTime)_ExpiryDate;
                return _ExpiryDate;
            }
            set
            {
                _ExpiryDate = value;
                RaisePropertyChanged("ExpiryDate");
            }
        }

        private Nullable<DateTime> _SurveyDate = null;
        public Nullable<DateTime> SurveyDate
        {
            get
            {
                if (_SurveyDate == null)
                {
                    _SurveyDate = DateTime.Now;
                }
                _AddCertificates.DOS = (DateTime)_SurveyDate;
                return _SurveyDate;
            }
            set
            {
                _SurveyDate = value;
                RaisePropertyChanged("SurveyDate");
            }
        }



        public void SaveCertificateList(CertificatesClass obj)
        {
            try
            {
                sc.refreshmessage(obj);
                if (!string.IsNullOrEmpty(obj.CName))
                {
                    var data = sc.Certificates.ToList();

                    var ids = data.Count > 0 ? data.Max(x => x.SrNo) : 0;
                    ids = ids + 1;

                    obj.SrNo = ids;
                    sc.Certificates.Add(obj);
                    sc.SaveChanges();

                    MessageBox.Show("Record saved successfully", "Certificate list", MessageBoxButton.OK, MessageBoxImage.Information);
                    _AddCertificates = new CertificatesClass();
                    RaisePropertyChanged("AddCertificates");
                    RaisePropertyChanged("IssueDate");
                    RaisePropertyChanged("ExpiryDate");
                    RaisePropertyChanged("SurveyDate");

                    new NotificationViewModel();
                }
                else
                {

                    MessageBox.Show("Please Enter the Certificate Name", "Certificate list", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void UpdateCertificateList(CertificatesClass obj)
        {
            try
            {
                sc.refreshmessage(obj);
                if (!string.IsNullOrEmpty(obj.CName))
                {
                    //var findrank = sc.Certificates.Where(x => x.Id == obj.Id).FirstOrDefault();

                    //if (findrank != null)
                    //{

                    //var local = sc.Set<CertificatesClass>()
                    // .Local
                    // .FirstOrDefault(f => f.Id == obj.Id);
                    //if (local != null)
                    //{
                    //    sc.Entry(local).State = EntityState.Detached;
                    //}

                    sc.Entry(obj).State = EntityState.Modified;
                    sc.SaveChanges();

                    MessageBox.Show("Record updated successfully", "Certificate list", MessageBoxButton.OK, MessageBoxImage.Information);
                    CancelCertificateList();

                    new NotificationViewModel();
                }
                else
                {

                    MessageBox.Show("Please Enter the Certificate Name", "Certificate list", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        public void CancelCertificateList()
        {
            try
            {
                _AddCertificates = new CertificatesClass();
                RaisePropertyChanged("AddCertificates");

                CertificationViewModel.loadCerticate.Clear();
                LoadMethod();
                // new NotificationViewModel();
                ChildWindowManager.Instance.CloseChildWindow();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }


        private void LoadMethod()
        {
            try
            {
                var data = sc.Certificates.OrderBy(x => x.SrNo).ToList();


                sc.ObservableCollectionList(CertificationViewModel.loadCerticate, data.ToList());
                //foreach (var item in data.ToList())
                //{

                //    CertificationViewModel.loadCerticate.Add(new CertificatesClass() { Id = item.Id, SrNo = item.SrNo, CName = item.CName, DOI = item.DOI, DOS = item.DOS, DOE = item.DOE, Remarks = item.Remarks });
                //}

                //return CertificationViewModel.loadCerticate;
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
