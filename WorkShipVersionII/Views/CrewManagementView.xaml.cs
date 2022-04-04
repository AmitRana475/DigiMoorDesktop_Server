using DataBuildingLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewModelCrewManagement;
//using WorkShipVersionII.ViewModelCrewManagement.MainViewModelCrewManagement;

namespace WorkShipVersionII.Views
{
    /// <summary>
    /// Interaction logic for CrewManagementView.xaml
    /// </summary>
    public partial class CrewManagementView : UserControl, INotifyPropertyChanged
    {
        static string tag = "";
        private readonly ShipmentContaxt sc;
        // StaticHelper.ItemMenu = this.CrewMenu.Items;
        public CrewManagementView()
        {
            InitializeComponent();
           // MenuItemClick1();
            //this.DataContext = new MainViewModelCrewManagement();

            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            //Person df = new Person();
            //df.FirstName = "dfgdgddfg";
        

            //txtMBL.GetBindingExpression(TextBox.TextProperty).UpdateTarget();

            //if (StaticHelper.MenuTitle == " ")
            //{
            //    MenuTitle.Text = "ss";
            //}
            //else
            //{
            //    MenuTitle.Text = StaticHelper.MenuTitle;
            //   // crewuser.Refresh();
            //}

            //MenuTitle.Text = "dfdfddfdfdf";

            // DynamicMenus();
        }


        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

       // public event PropertyChangedEventHandler PropertyChanged;
        public CrewManagementView(string title)
        {
            MenuTitle.Text = title;
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChenged(string propName)
        //{
        //    if (string.IsNullOrEmpty(propName) && PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propName));
        //    }
        //}

        //in your everty properyt you need to use this method
        //private int _Id;

        //public int Id
        //{
        //    get { return _Id; }
        //    set
        //    {
        //        _Id = value;
        //        OnPropertyChenged("Id");
        //    }
        //}

        static string test;
        public void DynamicMenus()
        {
           

            MenuNameClass mname = new MenuNameClass();

           
            sc.Database.ExecuteSqlCommand("TRUNCATE TABLE [tblMenuName]");
          

            var Menutest1 = sc.SmartMenus.ToList();// from table
            string allmenu = Menutest1.Select(x => x.SmartMenuContent).FirstOrDefault();

            if (allmenu != null)
            {


                var response = JsonConvert.DeserializeObject<List<RootObject>>(allmenu);
                MenuItem MainmnuInvoices;
                MenuItem mnuInvoices;
                foreach (var vObj in response)
                {
                    string CMparameterMain1 = vObj.id.ToString();
                    MainmnuInvoices = new MenuItem();
                    var first1 = vObj.text;

                    mname.Mid = vObj.id;
                    mname.MenuName = vObj.text;
                    mname.Type = vObj.type;

                

                    MainmnuInvoices.Header = first1;
                    MainmnuInvoices.Margin = new Thickness(5, 5, 0, 0);
                    MainmnuInvoices.Padding = new Thickness(0, 10, 0, 3);
                    // mnuInvoices.BorderBrush = new SolidColorBrush(Colors.WhiteSmoke);
                    // mnuInvoices.BorderThickness = new Thickness(1);
                    MainmnuInvoices.FontSize = 13;
                    MainmnuInvoices.FontFamily = new FontFamily("Verdana");
                    MainmnuInvoices.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                    MainmnuInvoices.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                    MainmnuInvoices.Name = "A" + vObj.id.ToString();
                    MainmnuInvoices.Style = (Style)(this.Resources["EditSubMenuitem11"]);

                    MainmnuInvoices.MinWidth = 170;
                    MainmnuInvoices.MaxWidth = 280;
                    MainmnuInvoices.Click += Submnu_Click;
                    MainmnuInvoices.CommandParameter = CMparameterMain1;
                    MainmnuInvoices.Command = WorkShipVersionII.ViewModelCrewManagement.MainViewModelCrewManagement.NavCommand;
                    this.CrewMenu.Items.Add(MainmnuInvoices);
                    sc.MenuNames.Add(mname);
                    sc.SaveChanges();

                    //foreach (var vObj in response)
                    //{
                    var childerncheck = vObj.children;
                    if (childerncheck != null)
                    {
                        //MenuItem mnuInvoices;
                        foreach (var vObj1 in vObj.children)
                        {

                            string CMparameterMain = vObj1.id.ToString();
                            mnuInvoices = new MenuItem();
                            var first = vObj1.text;

                            mname.Mid = vObj1.id;
                            mname.MenuName = vObj1.text;
                            mname.Type = vObj1.type;

                            //mnuInvoices.Header = first;
                            //mnuInvoices.Margin = new Thickness(5, 5, 0, 0);
                            mnuInvoices.Padding = new Thickness(0, 3, 0, 3);
                            // mnuInvoices.BorderBrush = new SolidColorBrush(Colors.WhiteSmoke);
                            // mnuInvoices.BorderThickness = new Thickness(1);
                            //string dd1 = vObj2.text.ToString();
                            string wordWrappedText = WordWrap(first, 50);
                            mnuInvoices.Header = wordWrappedText;
                            mnuInvoices.FontSize = 13;
                            mnuInvoices.FontFamily = new FontFamily("Verdana");
                            mnuInvoices.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                            mnuInvoices.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                            mnuInvoices.Name = "A" + vObj1.id.ToString();
                            mnuInvoices.Style = (Style)(this.Resources["EditSubMenuitem11"]);

                            mnuInvoices.MinWidth = 170;
                            mnuInvoices.Click += Submnu_Click;
                            mnuInvoices.CommandParameter = CMparameterMain;
                            mnuInvoices.Command = WorkShipVersionII.ViewModelCrewManagement.MainViewModelCrewManagement.NavCommand;
                            MainmnuInvoices.Items.Add(mnuInvoices);
                            sc.MenuNames.Add(mname);
                            sc.SaveChanges();
                            // this.CrewMenu.Items.Add(mnuInvoices);
                            int mm = 0;

                            var childerncheck1 = vObj1.children;
                            if (childerncheck1 != null)
                            {
                                foreach (var vObj2 in vObj1.children)
                                {
                                    mm = mm + 1;
                                    var bb = vObj2.text;


                                    mname.Mid = vObj2.id;
                                    mname.MenuName = vObj2.text;
                                    mname.Type = vObj2.type;

                                    StaticHelper.MenuTitle = mname.MenuName;

                                    //string CMparameter = "Submenu" + mm;
                                    string CMparameter = vObj2.id.ToString();
                                    MenuItem submnu = new MenuItem();
                                    submnu.Name = "B" + vObj2.id.ToString();
                                    //submnu.Header = vObj2.text;
                                    string dd = vObj2.text.ToString();
                                    // dd = Regex.Replace(dd, "(.{50})", "$1\n");
                                    // submnu.Header = dd;
                                    string wordWrappedText1 = WordWrap(dd, 50);
                                    submnu.Header = wordWrappedText1;

                                    submnu.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                                    submnu.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
                                    //submnu.MinWidth = 170;
                                    //submnu.MaxWidth = 250;
                                    submnu.Style = (Style)(this.Resources["EditSubMenuitem11"]);
                                    submnu.Click += Submnu_Click;
                                    submnu.CommandParameter = CMparameter;
                                    submnu.Command = WorkShipVersionII.ViewModelCrewManagement.MainViewModelCrewManagement.NavCommand;
                                    mnuInvoices.Items.Add(submnu);

                                    sc.MenuNames.Add(mname);
                                    sc.SaveChanges();
                                }
                            }
                            // }
                        }
                    }

                }
            }
        }
        private string SectionPath(int Menuid)
        {
            var Menutest1 = sc.SmartMenus.ToList();// from table
            string allmenu = Menutest1.Select(x => x.SmartMenuContent).FirstOrDefault();

            var response = JsonConvert.DeserializeObject<List<RootObject>>(allmenu);
            string path = " >> "; int check = 0;
            foreach (var vObj in response)
            {
                if (check == 1)
                {
                    break;
                }
                path += vObj.text;
                var childerncheck = vObj.children;
                if (childerncheck != null)
                {
                    foreach (var vObj1 in vObj.children)
                    {
                        //var 

                        path += " >> " + vObj1.text;
                        var childerncheck1 = vObj1.children;
                        if (childerncheck1 != null)
                        {
                            foreach (var vObj2 in vObj1.children)
                            {
                                var ids = vObj2.id;
                                if (ids == Menuid)
                                {
                                    path += " >> " + vObj2.text;
                                    check = 1;
                                    break;
                                }
                            }
                        }
                        if (check == 1)
                        {
                            break;
                        }
                    }
                }


            }

            string completepath = path;
            return completepath;
        }

        private void Submnu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.Source as MenuItem;
            //tag = mi.Name;
            string header = mi.Header.ToString();

            string dd = header.ToString();
            //dd = Regex.Replace(dd, "(.{50})", "$1\n");

            string wordWrappedText = WordWrap(dd, 50);
            //submnu.Header = wordWrappedText;


            String singleLine = wordWrappedText.Replace("\r\n", " ");
            //var asfd = String.Join("\n", header);
           MenuTitle.Text = singleLine;
            StaticHelper.MenuTitle= singleLine;

            B1_Click(this, e);
        }

        //else

        //{

        //       MenuItem mnuInvoiceStatus = new MenuItem();

        //       mnuInvoiceStatus.Header = "Invoice Status";

        //       mnuInvoiceStatus.Height = 50;

        //       mnuInvoices.Items.Add(mnuInvoiceStatus);

        //}






        public void MenuItemClick1()
        {
            
            var items = this.CrewMenu.Items;
            foreach (MenuItem item in items)
            {
                foreach (MenuItem Subitem1 in item.Items)
                {
                    foreach (MenuItem Subitem2 in Subitem1.Items)
                    {
                        tag = Subitem2.Header.ToString();
                        string wordWrappedText = WordWrap(tag, 50);
                        
                        String singleLine = wordWrappedText.Replace("\r\n", " ");
                        //var asfd = String.Join("\n", header);
                        MenuTitle.Text = singleLine;
                        StaticHelper.MenuTitle = singleLine;
                        if (!string.IsNullOrEmpty(tag))
                            break;
                    }

                    if(!string.IsNullOrEmpty(tag))
                        break;
                }

                if (!string.IsNullOrEmpty(tag))
                    break;
            }


         
        }


        public static void RefreshMenucolor(string itemName)
        {
            //StaticHelper.ItemMenu = this.CrewMenu.Items;
            tag = itemName;
            var items = StaticHelper.ItemMenu;
            //if (StaticHelper.Editing == false)
            {
                foreach (MenuItem item in items)
                {
                    if (item.Name.Equals(tag))
                    {

                        item.Background = new SolidColorBrush(Colors.LightSteelBlue);
                        item.Foreground = new SolidColorBrush(Colors.Black);
                        item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        item.BorderThickness = new Thickness(0.2);


                    }
                    else
                    {
                        item.Background = new SolidColorBrush(Colors.Transparent);
                        item.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                        item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        item.BorderThickness = new Thickness(0);

                    }
                }
            }
        }

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {// MenuItem.Click="MenuItemClick"
            MenuItem mi = e.Source as MenuItem;
            //if(mi==null)
            //{
            //    mi.Name = "B7";
            //}
            tag = mi.Name;
            var items = this.CrewMenu.Items;
            StaticHelper.ItemMenu = this.CrewMenu.Items;

            //if (StaticHelper.Editing == false)
            
             foreach (MenuItem item in items)
                {
                    if (item.Name.Equals(tag.ToString()))
                    {

                        item.Background = new SolidColorBrush(Colors.LightSteelBlue);
                        item.Foreground = new SolidColorBrush(Colors.Black);
                        item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        item.BorderThickness = new Thickness(0.2);


                    }
                    else
                    {
                        item.Background = new SolidColorBrush(Colors.Transparent);
                        item.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                        item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        item.BorderThickness = new Thickness(0);

                    }
                }


          

        }

        public class Menut
        {
            public string MenuTitle { get; set; }
        }

        public static Menut _menutitles = new Menut();
        public Menut MenuTitles
        {
            get
            {
                //MooringWinchMessage = string.Empty;
                //RaisePropertyChanged("MooringWinchMessage");
                return _menutitles;
            }
            set
            {
                _menutitles = value;

                OnPropertyChanged(new PropertyChangedEventArgs("MenuTitles"));
                // RaisePropertyChanged("MenuTitles");

            }
        }
        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        private void MouseEnterFunction(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //if (StaticHelper.Editing == false)
            {
                var item = ((MenuItem)sender);
                if (item.Name.Equals(tag.ToString()))
                {
                    item.Background = new SolidColorBrush(Colors.LightSteelBlue);
                    //item.Background = new SolidColorBrush(Colors.Yellow);
                    //item.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#58585A"));
                    item.Foreground = new SolidColorBrush(Colors.Black);
                    item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    item.BorderThickness = new Thickness(0);

                }
                else
                {
                    item.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    item.Foreground = new SolidColorBrush(Colors.Black);
                    item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    item.BorderThickness = new Thickness(0);

                }
            }


        }

        private void MouseLeaveFunction(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var item = ((MenuItem)sender);
            if (item.Name.Equals(tag.ToString()))
            {

                item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                item.BorderThickness = new Thickness(0);

            }
            else
            {
                item.Background = new SolidColorBrush(Colors.Transparent);
                item.Foreground = new SolidColorBrush(Colors.White);
                item.BorderBrush = new SolidColorBrush(Colors.Transparent);
                item.BorderThickness = new Thickness(0);

            }

        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            B1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B1.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);

            B2.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);


        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            B2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B2.BorderThickness = new Thickness(2);
            //------------------------------------
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);

            B1.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            B3.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B3.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);

            B2.BorderThickness = new Thickness(1);
            B1.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
        }

        private void B4_Click(object sender, RoutedEventArgs e)
        {
            B4.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B4.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);

            B2.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B1.BorderThickness = new Thickness(1);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DynamicMenus();
            MenuItemClick1();
            //MenuItem submnu = new MenuItem();
            //submnu.CommandParameter = 7;
            //submnu.Command = WorkShipVersionII.ViewModelCrewManagement.MainViewModelCrewManagement.NavCommand;
        }

        protected const string _newline = "\r\n";
        public static string WordWrap(string the_string, int width)
        {
            int pos, next;
            StringBuilder sb = new StringBuilder();

            // Lucidity check
            if (width < 1)
                return the_string;

            // Parse each line of text
            for (pos = 0; pos < the_string.Length; pos = next)
            {
                // Find end of line
                int eol = the_string.IndexOf(_newline, pos);

                if (eol == -1)
                    next = eol = the_string.Length;
                else
                    next = eol + _newline.Length;

                // Copy this line of text, breaking into smaller lines as needed
                if (eol > pos)
                {
                    do
                    {
                        int len = eol - pos;

                        if (len > width)
                            len = BreakLine(the_string, pos, width);

                        sb.Append(the_string, pos, len);
                        sb.Append(_newline);

                        // Trim whitespace following break
                        pos += len;

                        while (pos < eol && Char.IsWhiteSpace(the_string[pos]))
                            pos++;

                    } while (eol > pos);
                }
                else sb.Append(_newline); // Empty line
            }

            return sb.ToString();
        }

        /// <summary>
        /// Locates position to break the given line so as to avoid
        /// breaking words.
        /// </summary>
        /// <param name="text">String that contains line of text</param>
        /// <param name="pos">Index where line of text starts</param>
        /// <param name="max">Maximum line length</param>
        /// <returns>The modified line length</returns>
        public static int BreakLine(string text, int pos, int max)
        {
            // Find last whitespace in line
            int i = max - 1;
            while (i >= 0 && !Char.IsWhiteSpace(text[pos + i]))
                i--;
            if (i < 0)
                return max; // No whitespace found; break at maximum length
                            // Find start of whitespace
            while (i >= 0 && Char.IsWhiteSpace(text[pos + i]))
                i--;
            // Return length of text before whitespace
            return i + 1;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var ss = SearchTermTextBox.Text;

            StaticHelper.SearchText = ss.ToString();
        }

      





        //public class Person : INotifyPropertyChanged
        //{

        //    public Person()
        //    {
        //        _fisrtname = "Nirav";

        //    }
        //    private string _fisrtname;
        //    public string FirstName
        //    {
        //        get { return _fisrtname; }
        //        set
        //        {
        //            _fisrtname = value;
        //            OnPropertyRaised("FirstName");

        //        }
        //    }



        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyRaised(string propertyname)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        //    }
        //}



    }
}
