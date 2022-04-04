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
using WorkShipVersionII.ViewsCrewManagement;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class CrewRankViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        public CrewRankViewModel()
        {
            sc = new ShipmentContaxt();
            sc.Configuration.ProxyCreationEnabled = false;

            editCommand = new RelayCommand<CrewRankClass>(EditRank);
            deleteCommand = new RelayCommand<CrewRankClass>(DeleteRank);

            loadRankList.Clear();
            sc.ObservableCollectionList(loadRankList, GetRankList);

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

        private void DeleteRank(CrewRankClass ranks)
        {
            try
            {
                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    CrewRankClass findrank = sc.CrewRanks.Where(x => x.cid == ranks.cid).FirstOrDefault();
                    if (findrank != null)
                    {

                        var checkRank = sc.CrewDetails.Where(x => x.rid.Equals(ranks.cid)).FirstOrDefault();
                        if (checkRank != null)
                        {
                            MessageBox.Show("Sorry you can't delete this Rank, as it is assigned to some crew members.", "Delete Crew Rank", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                        else if (ranks.Rank.ToLower() == "master")
                        {
                            MessageBox.Show("Sorry you can't delete Master Rank", "Delete Crew Rank", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {

                            sc.Entry(findrank).State = EntityState.Deleted;
                            sc.SaveChanges();


                            MessageBox.Show("Record deleted successfully", "Delete Crew Rank", MessageBoxButton.OK, MessageBoxImage.Information);

                            //.....Refresh DataGrid........


                            LoadRankList.Clear();
                            sc.ObservableCollectionList(LoadRankList, GetRankList);

                            //.....End Refresh DataGrid........
                        }
                    }
                    else
                    {

                        MessageBox.Show("Record is not found ", "Delete Crew Rank", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void EditRank(CrewRankClass crc)
        {
            try
            {
                AddRankViewModel vm = new AddRankViewModel(crc);
                ChildWindowManager.Instance.ShowChildWindow(new AddRankView() { DataContext = vm });
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }
        private List<CrewRankClass> GetRankList
        {
            get
            {
                try
                {
                    var data = sc.CrewRanks.OrderBy(x => x.SrNo).ToList();
                    return data;
                }
                catch (Exception ex)
                {
                    sc.ErrorLog(ex);
                    return null;
                }
            }

        }



        private static ObservableCollection<CrewRankClass> loadRankList = new ObservableCollection<CrewRankClass>();
        public ObservableCollection<CrewRankClass> LoadRankList
        {
            get
            {
                return loadRankList;
            }
            set
            {

                loadRankList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadRankList"));
            }
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }

}
