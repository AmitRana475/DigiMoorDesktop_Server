using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;
//using WorkShipVersionII.ViewModel;
//using WorkShipVersionII.ViewsCrewManagement;

namespace WorkShipVersionII.WorkHoursViewModel
{
       public class LogbookViewModel : ViewModelBase
       {
              private readonly ShipmentContaxt sc;
              public LogbookViewModel()
              {
                     sc = new ShipmentContaxt();
                     sc.Configuration.ProxyCreationEnabled = false;

                     editCommand = new RelayCommand<LogbookClass>(EditRank);
                     deleteCommand = new RelayCommand<LogbookClass>(DeleteRank);

                     loadLogbook.Clear();
                     sc.ObservableCollectionList(loadLogbook, GetLogbookList);
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


              private static string searchEvent;
              public string SearchEvent
              {
                     get
                     {
                            if (searchEvent != null)
                            {
                                   loadLogbook.Clear();
                                   var data = GetLogbookList.Where(p => p.EventName.ToLower().Contains(searchEvent.Trim().ToLower())).ToList();
                                   sc.ObservableCollectionList(loadLogbook, data);
                                   RaisePropertyChanged("loadCrewDetail");
                            }
                            return searchEvent;
                     }

                     set
                     {
                            searchEvent = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("SearchEvent"));
                     }
              }


              private void DeleteRank(LogbookClass obj)
              {
                     try
                     {
                            if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                   LogbookClass findlog = sc.Logbooks.Where(x => x.Id == obj.Id).FirstOrDefault();
                                   if (findlog != null)
                                   {

                                          sc.Entry(findlog).State = EntityState.Deleted;
                                          sc.SaveChanges();


                                          MessageBox.Show("Record deleted successfully", "Delete LogBook", MessageBoxButton.OK, MessageBoxImage.Information);

                                          //.....Refresh DataGrid........

                                          loadLogbook.Clear();
                                          sc.ObservableCollectionList(loadLogbook, GetLogbookList);
                                          OnPropertyChanged(new PropertyChangedEventArgs("LoadLogbook"));
                                          //.....End Refresh DataGrid........

                                   }
                                   else
                                   {

                                          MessageBox.Show("Record is not found ", "Delete LogBook", MessageBoxButton.OK, MessageBoxImage.Information);
                                   }

                            }


                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }

              private void EditRank(LogbookClass obj)
              {
                     try
                     {
                            AddLogbookViewModel vm = new AddLogbookViewModel(obj);
                            ChildWindowManager.Instance.ShowChildWindow(new AddLogbookView() { DataContext = vm });
                     }
                     catch (Exception ex)
                     {
                            sc.ErrorLog(ex);
                     }
              }



              private static ObservableCollection<LogbookClass> loadLogbook = new ObservableCollection<LogbookClass>();
              public ObservableCollection<LogbookClass> LoadLogbook
              {
                     get
                     {
                            return loadLogbook;
                     }
                     set
                     {

                            loadLogbook = value;
                            OnPropertyChanged(new PropertyChangedEventArgs("LoadLogbook"));
                     }
              }


              private List<LogbookClass> GetLogbookList
              {
                     get
                     {
                            try
                            {
                                   var data = sc.Logbooks.OrderByDescending(d => d.DateFrom).ToList();
                                   return data;
                            }
                            catch (Exception ex)
                            {
                                   sc.ErrorLog(ex);
                                   return null;
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
