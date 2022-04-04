using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class AddRankViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;

        public AddRankViewModel(CrewRankClass edrank)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            StaticHelper.Editing = true;
            saveCommand = new RelayCommand<CrewRankClass>(UpdateRank);
            cacelCommand = new RelayCommand(CancelRank);
            EditRank(edrank);


        }

        public AddRankViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            StaticHelper.Editing = true;
            saveCommand = new RelayCommand<CrewRankClass>(SaveRank);
            cacelCommand = new RelayCommand(CancelRank);

        }


        private string rankMessage;
        public string RankMessage
        {
            get { return rankMessage; }
            set { rankMessage = value; OnPropertyChanged(new PropertyChangedEventArgs("RankMessage")); }
        }

        private CrewRankClass _AddRank = new CrewRankClass();
        public CrewRankClass AddRank
        {
            get
            {
                RankMessage = string.Empty;
                RaisePropertyChanged("RankMessage");
                return _AddRank;


            }
            set
            {
                _AddRank = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddRank"));


            }
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



        private void SaveRank(CrewRankClass ranks)
        {
            try
            {
                ranks.Rank = ranks.Rank != null ? ranks.Rank.Trim() : ranks.Rank;

                if (!string.IsNullOrEmpty(ranks.Rank))
                {
                    var findrank = sc.CrewRanks.Where(x => x.Rank == ranks.Rank).FirstOrDefault();

                    if (findrank == null)
                    {
                        var data = sc.CrewRanks.ToList();
                        var ids = data.Count > 0 ? data.Max(x => x.SrNo) : 0;
                        ids = ids + 1;

                        ranks.SrNo = ids;
                        ranks.Rank = ranks.Rank.ToUpper();
                        sc.CrewRanks.Add(ranks);
                        sc.SaveChanges();

                        StaticHelper.Editing = false;
                        MessageBox.Show("Record saved successfully", "Add Crew Rank");

                        AddRank = new CrewRankClass();
                        RaisePropertyChanged("AddRank");

                    }
                    else
                    {
                        MessageBox.Show("Crew rank already exist", "Add Crew Rank");
                    }
                }
                else
                {

                    RankMessage = "Please Enter the Rank Name";
                    RaisePropertyChanged("RankMessage");
                }

            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void UpdateRank(CrewRankClass ranks)
        {
            try
            {
                ranks.Rank = ranks.Rank != null ? ranks.Rank.Trim() : ranks.Rank;
                if (!string.IsNullOrEmpty(ranks.Rank))
                {

                    var findrank = sc.CrewRanks.Where(x => x.cid == ranks.cid).FirstOrDefault();

                    if (findrank != null)
                    {
                        ranks.Rank = ranks.Rank.ToUpper();


                        var local = sc.Set<CrewRankClass>()
                         .Local
                         .FirstOrDefault(f => f.cid == ranks.cid);
                        if (local != null)
                        {
                            sc.Entry(local).State = EntityState.Detached;
                        }

                        var UpdatedLocation = new CrewRankClass()
                        {

                            cid = ranks.cid,
                            Rank = ranks.Rank
                        };

                        sc.Entry(UpdatedLocation).State = EntityState.Modified;
                        sc.SaveChanges();


                        //Update into Crew Detail's Table
                        var user = sc.CrewDetails.Where(x => x.rid.Equals(UpdatedLocation.cid)).ToList();
                        var depat = user.Where(x => x.did.Equals(UpdatedLocation.cid)).FirstOrDefault().position;
                        user.ForEach(a =>
                        {
                            a.position = UpdatedLocation.Rank;
                        });

                        sc.SaveChanges();

                        //Update into WorkHours's Table
                        var some = sc.WorkHourss.Where(x => x.Position.Equals(depat.Trim())).ToList();
                        some.ForEach(a =>
                        {
                            a.Position = UpdatedLocation.Rank;
                        });

                        sc.SaveChanges();
                        StaticHelper.Editing = false;
                        MessageBox.Show("Record updated successfully", "Update Crew Rank", MessageBoxButton.OK, MessageBoxImage.Information);

                        CancelRank();

                    }

                }
                else
                {

                    RankMessage = "Please Enter the Rank Name";
                    RaisePropertyChanged("RankMessage");
                }


            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }


        private void EditRank(CrewRankClass ranks)
        {
            try
            {

                var findrank = sc.CrewRanks.Where(x => x.cid == ranks.cid).FirstOrDefault();
                AddRank.Rank = findrank.Rank;
                AddRank.cid = findrank.cid;
                OnPropertyChanged(new PropertyChangedEventArgs("AddRank"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }



        private void CancelRank()
        {

            //var lostdata = new ObservableCollection<CrewRankClass>(sc.CrewRanks.ToList());
            //CrewRankViewModel cc = new CrewRankViewModel(lostdata);

            new CrewRankViewModel();
            new CrewDetailViewModel();

            ChildWindowManager.Instance.CloseChildWindow();
            StaticHelper.Editing = false;
        }



        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
