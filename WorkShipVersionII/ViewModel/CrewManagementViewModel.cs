using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Windows.Input;
using WorkShipVersionII.ViewModelCrewManagement;

namespace WorkShipVersionII.ViewModel
{
    public class CrewManagementViewModel : ViewModelBase
    {
        public ICommand HelpCommand { get; private set; }

        //public CrewManagementViewModel()
        //{
        //  //  DocsContant.MenuTitle = StaticHelper.MenuTitle;

        //    //Mtt = new Mt()
        //    //{
        //    //    MenuTitle = StaticHelper.MenuTitle
        //    //};
        //   // RaisePropertyChanged("Mtt");

            

        //}
        public string visicheck = "Collapsed";

        public string Visicheck
        {
            get { return visicheck; }
            set { visicheck = value; }
        }



        //public static string menutitle = "";

        //public static string MenuTitle
        //{
        //    get { return menutitle; }
        //    set
        //    {
        //        // menutitle = "";
        //        OnPropertyChanged(new PropertyChangedEventArgs("MenuTitle"));
        //    }

        //}

    

      
    }
}
