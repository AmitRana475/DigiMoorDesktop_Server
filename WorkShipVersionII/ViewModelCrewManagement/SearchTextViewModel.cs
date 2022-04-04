using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
//using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsCrewManagement;
using System.Data.SqlClient;
using System.Data;
using WorkShipVersionII.Views;
using System.Text.RegularExpressions;


namespace WorkShipVersionII.ViewModelCrewManagement
{
   public class SearchTextViewModel :ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private static int menuid = 0;
        public SearchTextViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

          
            //addCommand = new RelayCommand(AddShipAttach);
            //editCommand = new RelayCommand<ShipSpecificAttachment>(Downloadfile);
            //deleteCommand = new RelayCommand<ShipSpecificAttachment>(DeleteAttachment);
        }

        public SearchTextViewModel(MenuNameClass ss)
        {
            cancelCommand = new RelayCommand(CancelSearchText);
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }


        private void CancelSearchText()
        {
           ChildWindowManager.Instance.CloseChildWindow();
        }
      
        private void MenuC(MenuNameClass ss)
        {
           

            SearchinDataViewModel vm = new SearchinDataViewModel(ss.Mid);
            ChildWindowManager.Instance.ShowChildWindow(new SearchingDataView() { DataContext = vm });

            
        }
        public SearchTextViewModel(int mid)
        {
            var check = StaticHelper.SearchText;

            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            menuid = mid;

            menuCommand = new RelayCommand<MenuNameClass>(MenuC);

           
            GetSearchResult(check);

        }

        private ICommand menuCommand;
        public ICommand MenuCommand
        {
            get { return menuCommand; }
        }

        private void GetSearchResult(string SearchText)
        {

            try
            {
             

                LoadSearchTextDetail.Clear();
                
                SqlCommand cmd = new SqlCommand("GetSearchText", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", SearchText);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                // var data = sc.AssignRopetoWinch.ToList();          
                foreach (DataRow row in ds.Rows)
                {
                    string ss = (string)row["Content"];

                    //string ss = Regex.Replace((string)row["Content"], "<.*?>", String.Empty);

                    //ss = Regex.Replace(ss, "(<style.+?</style>)|(<script.+?</script>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    //ss = Regex.Replace(ss, "(<img.+?>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    //ss = Regex.Replace(ss, "(<o:.+?</o:.+?>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    //ss = Regex.Replace(ss, "<!--.+?-->", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    //ss = Regex.Replace(ss, "class=.+?>", ">", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    //ss = Regex.Replace(ss.Replace(System.Environment.NewLine, "<br/>"), "<[^(a|img|b|i|u|ul|ol|li)][^>]*>", " ");
                    //ss = ss.Replace("&nbsp;", "");
                    //ss = ss.Replace("\n;", string.Empty);

                    //ss = Regex.Replace(ss, @"\t|\n|\r", "");

                    //var input = ss;

                    //ss = Regex.Replace(ss, @"(?s)(?<=<!--).+?(?=-->)", "");

                    //string textOnly = string.Empty;
                    //Regex tagRemove = new Regex(@"<[^>]*(>|$)");
                    //Regex compressSpaces = new Regex(@"[\s\r\n]+");
                    //textOnly = tagRemove.Replace(ss, string.Empty);
                    //textOnly = compressSpaces.Replace(textOnly, " ");
                    //ss = textOnly;
                    //ss = ss.Replace("&nbsp;", "");
                    //ss = ss.Replace("\n;", string.Empty);


                    Regex clearMarkup = new Regex(@"(<!--\[.*\]-->)", RegexOptions.Singleline);
                    ss = clearMarkup.Replace(ss, ""); // str is the content as shown above.       


                    ss = Regex.Replace(ss, "<html><head>.*</head></html>", "", RegexOptions.Singleline);

                    ss = Regex.Replace(ss, @"(?s)(?<=<!--).+?(?=-->)", "");

                    ss = Regex.Replace(ss, "(<style.+?</style>)|(<script.+?</script>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    ss = Regex.Replace(ss, "(<img.+?>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    ss = Regex.Replace(ss, "(<o:.+?</o:.+?>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    ss = Regex.Replace(ss, "<!--.+?-->", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    ss = Regex.Replace(ss, "class=.+?>", ">", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    ss = Regex.Replace(ss.Replace(System.Environment.NewLine, "<br/>"), "<[^(a|img|b|i|u|ul|ol|li)][^>]*>", " ");
                    ss = ss.Replace("&nbsp;", "");
                    ss = ss.Replace("\n;", string.Empty);

                    ss = Regex.Replace(ss, @"\t|\n|\r", "");

                   ss= Regex.Replace(ss, "<.*?>|&.*?;", string.Empty);

                    LoadSearchTextDetail.Add(new MenuNameClass()
                    {
                        Id = (int)row["id"],
                        Content = ss,
                        // Content = (string)row["Content"],
                        // MenuName = (string)row["MenuName"],
                        MenuName = (row["MenuName"] == DBNull.Value) ? string.Empty : row["MenuName"].ToString(),

                        Mid = (int)row["Mid"],
                      
                    });
                }

                if (ds.Rows.Count <= 0)
                {
                    MessageBox.Show("We could not locate this Search Term in the text, please check spellings or try another search term", "Search Text", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                OnPropertyChanged(new PropertyChangedEventArgs("LoadSearchTextDetail"));
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                /// return null;
            }

        }

       

        public static ObservableCollection<MenuNameClass> loadsearchDetail = new ObservableCollection<MenuNameClass>();
        public ObservableCollection<MenuNameClass> LoadSearchTextDetail
        {
            get
            {
                return loadsearchDetail;
            }
            set
            {
                loadsearchDetail = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadSearchTextDetail"));
            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }

       public static class CheckErrorMessage
       {
              public static bool CheckErrorMessages { get; set; }
              public static bool CheckErrorMessages1 { get; set; }
              public static bool CheckErrorMessages2 { get; set; }
              public static bool chkyoungs { get; set; }

       }
}

