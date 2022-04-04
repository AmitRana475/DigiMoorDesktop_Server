using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class AddCurrencyViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public AddCurrencyViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            saveCommand = new RelayCommand<CurrencyClass>(SaveCurrency);
            cacelCommand = new RelayCommand(CancelCurrency);
            editCommand = new RelayCommand<CurrencyClass>(EditCurrency);
            deleteCommand = new RelayCommand<CurrencyClass>(DeleteCurrency);

            loadCurrencyList = GetCurrencyList();
        }

        private ICommand cacelCommand;
        public ICommand CancelCommand
        {
            get { return cacelCommand; }
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }
        private ICommand editCommand;
        public ICommand EditCommand
        {
            get { return editCommand; }
            set { editCommand = value; }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }


        private CurrencyErrorMessage _AddErrorMessage = new CurrencyErrorMessage();
        public CurrencyErrorMessage AddErrorMessage
        {
            get
            {
               
                return _AddErrorMessage;

            }
            set
            {
                _AddErrorMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddErrorMessage"));
            }
        }

        private CurrencyClass _AddCurrency = new CurrencyClass();
        public CurrencyClass AddCurrency
        {
            get
            {
                refreshmessage(_AddCurrency);
                OnPropertyChanged(new PropertyChangedEventArgs("AddErrorMessage"));
                return _AddCurrency;
                
            }
            set
            {
                _AddCurrency = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddCurrency"));
            }
        }

        private static ObservableCollection<CurrencyClass> loadCurrencyList = new ObservableCollection<CurrencyClass>();
        public ObservableCollection<CurrencyClass> LoadCurrencyList
        {
            get
            {
                return loadCurrencyList;
            }
            set
            {

                loadCurrencyList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCurrencyList"));
            }
        }
        
        private void refreshmessage(CurrencyClass cdc1)
        {
            CurrencyClass cdc = cdc1;
          
            CurrencyErrorMessage m = (AddErrorMessage as CurrencyErrorMessage); //DownCasting.....

            if (!string.IsNullOrEmpty(cdc.CurrencyName))
            {
                m.CurrencyMessage = string.Empty;
                RaisePropertyChanged("AddErrorMessage");
            }
            if (!string.IsNullOrEmpty(cdc.CurrencySymbol))
            {
                m.SymbolMessage = string.Empty;
                RaisePropertyChanged("AddErrorMessage");
            }
            
           
        }
        private ObservableCollection<CurrencyClass> GetCurrencyList()
        {
            var data = sc.Currencys.ToList();
            var ranklist = new ObservableCollection<CurrencyClass>();
            LoadCurrencyList.Clear();
            foreach (var item in data)
            {
                ranklist.Add(new CurrencyClass() { curid = item.curid, CurrencyName = item.CurrencyName, CurrencySymbol = item.CurrencySymbol });
            }

            return ranklist;

        }
        private void SaveCurrency(CurrencyClass cc )
        {
            cc.CurrencyName = cc.CurrencyName != null ? cc.CurrencyName.Trim() : cc.CurrencyName;
            cc.CurrencySymbol = cc.CurrencySymbol != null ? cc.CurrencySymbol.Trim() : cc.CurrencySymbol;
            if (!string.IsNullOrEmpty(cc.CurrencyName) && !string.IsNullOrEmpty(cc.CurrencySymbol))
            {
                if (cc.curid != 0)
                {
                    cc.CurrencyName = textinfo.ToTitleCase(cc.CurrencyName.ToLower());

                    var local = sc.Set<CurrencyClass>()
                     .Local
                     .FirstOrDefault(f => f.curid == cc.curid);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }

                    var UpdatedLocation = new CurrencyClass()
                    {

                        curid = cc.curid,
                        CurrencyName = cc.CurrencyName,
                        CurrencySymbol=cc.CurrencySymbol
                    };

                    sc.Entry(UpdatedLocation).State = EntityState.Modified;
                    sc.SaveChanges();
                    MessageBox.Show("Record updated successfully", "Update Currency");



                    //.............

                    loadCurrencyList = GetCurrencyList();
                    RaisePropertyChanged("LoadCurrencyList");
                    AddCurrency = new CurrencyClass();
                    RaisePropertyChanged("AddCurrency");

                    //.........

                }
                else
                {

                    var findrank = sc.Currencys.Where(x => x.CurrencyName == cc.CurrencyName && x.CurrencySymbol == cc.CurrencySymbol).FirstOrDefault();

                    if (findrank == null)
                    {
                        cc.CurrencyName = textinfo.ToTitleCase(cc.CurrencyName.ToLower());

                        sc.Currencys.Add(cc);
                        sc.SaveChanges();
                        MessageBox.Show("Record saved successfully", "Add Currency");

                        //.............

                        loadCurrencyList = GetCurrencyList();
                        RaisePropertyChanged("LoadCurrencyList");
                        AddCurrency = new CurrencyClass();
                        RaisePropertyChanged("AddCurrency");

                        //.........


                    }
                    else
                    {
                        MessageBox.Show("Currency name already exist", "Add Currency");
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(cc.CurrencyName))
                {
                    AddErrorMessage.CurrencyMessage = "Please Enter the Currency Name";
                    RaisePropertyChanged("AddErrorMessage");
                }
                if (string.IsNullOrEmpty(cc.CurrencySymbol))
                {
                    AddErrorMessage.SymbolMessage = "Please Enter the Currency Symbol";
                    RaisePropertyChanged("AddErrorMessage");
                }

            }
            
        }

        private void CancelCurrency()
        {

            //var lostdata = new ObservableCollection<CurrencyClass>(sc.Currencys.ToList());
            AddOverTimeCrewDetailModel ov = new AddOverTimeCrewDetailModel();// (lostdata);
            ChildWindowManager.Instance.CloseChildWindow();
        }

        private void DeleteCurrency(CurrencyClass cc)
        {
            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CurrencyClass findrank = sc.Currencys.Where(x => x.curid == cc.curid).FirstOrDefault();
                if (findrank != null)
                {


                    sc.Entry(findrank).State = EntityState.Deleted;
                    sc.SaveChanges();

                    MessageBox.Show("Record deleted successfully", "Delete Crew Rank");


                    //.....Refresh DataGrid........

                    //var data = new ObservableCollection<CurrencyClass>(sc.Currencys.ToList());
                    LoadCurrencyList.Clear();
                    LoadCurrencyList = GetCurrencyList();
                    RaisePropertyChanged("LoadCurrencyList");

                    //.....End Refresh DataGrid........

                }
                else
                {
                    MessageBox.Show("Record is not found ", "Delete Currency");
                }

            }
        }
        
        private void EditCurrency(CurrencyClass cc)
        {

            AddCurrency.curid = cc.curid;
            AddCurrency.CurrencyName = cc.CurrencyName;
            AddCurrency.CurrencySymbol = cc.CurrencySymbol;
            RaisePropertyChanged("AddCurrency");
        }



        new public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);

        }


    }

    public class CurrencyErrorMessage
    {
        public string CurrencyMessage { get; set; }
        public string SymbolMessage { get; set; }
    }

}
