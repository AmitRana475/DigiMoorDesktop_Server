using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WorkShipVersionII.Views
{
    /// <summary>
    /// Interaction logic for CrewManagementView.xaml
    /// </summary>
    public partial class CrewManagementView : UserControl
    {
       static string tag = "";
             // StaticHelper.ItemMenu = this.CrewMenu.Items;
        public CrewManagementView()
        {
            InitializeComponent();
            MenuItemClick1();
            //this.DataContext = new MainViewModelCrewManagement();
        }

        private void MenuItemClick1()
        {
            tag = "CrewDetail";
            var items = this.CrewMenu.Items;
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
        {
            MenuItem mi = e.Source as MenuItem;
            tag = mi.Name;
            var items = this.CrewMenu.Items;
            StaticHelper.ItemMenu = this.CrewMenu.Items;
            //if (StaticHelper.Editing == false)
            {
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


        
    }
}
