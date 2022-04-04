using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WorkShipVersionII.ViewModelMooringCalculator
{
    public class InputsEnvironmentViewModel:ViewModelBase
    {
        private ShipmentContaxt sc;

        private IRepository<WindandCurrent> WindandCurrents;

        public ICommand HelpCommand { get; private set; }
        public InputsEnvironmentViewModel()
        {
            sc = new ShipmentContaxt();
            WindandCurrents = new Repository<WindandCurrent>();

            updateCommand = new RelayCommand<WindandCurrent>(UpdateMethod);

            var list = WindandCurrents.GetList().ToList();
            list.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

            LoadEnvironment.Clear();
            sc.ObservableCollectionList(LoadEnvironment, list);
            RaisePropertyChanged("LoadEnvironment");

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
        }

        

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get { return updateCommand; }
           
        }




        public static ObservableCollection<WindandCurrent> loadEnvironment = new ObservableCollection<WindandCurrent>();
        public ObservableCollection<WindandCurrent> LoadEnvironment
        {
            get
            {
                return loadEnvironment;
            }
            set
            {
                loadEnvironment = value;
                RaisePropertyChanged("LoadEnvironment");
            }
        }

        private void UpdateMethod(WindandCurrent obj)
        {
            try
            {

                //using (MooringContext sc1 = new MooringContext())
                //{
                    var finddata = WindandCurrents.GetListById(obj.Id);
                    if (finddata != null)
                    {

                        finddata.MainValue = Convert.ToDecimal(obj.MainValue1);


                        WindandCurrents.UpdateEntity(finddata);
                        WindandCurrents.Save();

                        //sc1.Entry(finddata).State = EntityState.Modified;
                        //sc1.SaveChanges();

                        var list = WindandCurrents.GetList().ToList();
                        list.ForEach(x => { x.MainValue1 = x.MainValue.ToString(); });

                        loadEnvironment.Clear();
                        sc.ObservableCollectionList(loadEnvironment, list);
                        RaisePropertyChanged("LoadVesselP");


                        MessageBox.Show("Data updeted successfully!");

                    }
                //}

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

    }
}
