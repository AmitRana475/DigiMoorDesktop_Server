using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.Views
{

    public partial class WorkRestHoursView : UserControl
    {
        object tag = "";
        public WorkRestHoursView()
        {
            try
            {
                InitializeComponent();
                // MenuItemClick1();
            }
            catch (Exception ex) { }
        }

        private void MenuItemClick1()
        {
            tag = "Mooring Operation";
            var items = this.LMPRMenu.Items;
            foreach (MenuItem item in items)
            {
                if (item.Header.Equals(tag.ToString()))
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

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.Source as MenuItem;
            tag = mi.Header;
            // var items = this.LMPRMenu.Items;

            HeaderTitle.Text = tag.ToString();

            //foreach (MenuItem item in items)
            //{
            //    if (item.Header.Equals(tag))
            //    {
            //        item.Background = new SolidColorBrush(Colors.LightSteelBlue);
            //        item.Foreground = new SolidColorBrush(Colors.Black);
            //        item.BorderBrush = new SolidColorBrush(Colors.Transparent);
            //        item.BorderThickness = new Thickness(0.2);

            //    }
            //    else
            //    {
            //        item.Background = new SolidColorBrush(Colors.Transparent);
            //        item.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            //        item.BorderBrush = new SolidColorBrush(Colors.Transparent);
            //        item.BorderThickness = new Thickness(0);

            //    }
            //}
        }

        private void MoorR_Click(object sender, RoutedEventArgs e)
        {
            MainViewModelWorkHours.CommonValue = false;
        }

        private void MoorRTail_Click(object sender, RoutedEventArgs e)
        {
            MainViewModelWorkHours.CommonValue = false;
        }

        private void MoorL_Click(object sender, RoutedEventArgs e)
        {
            MainViewModelWorkHours.CommonValue = false;
        }

        private void MoorRope_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.RopeTailId = 0;
        }



        //private void OnboardGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    MessageBox.Show("Hello");
        //}
    }
}
