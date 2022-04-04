using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkShipVersionII.ViewsMooringCalulator
{
    /// <summary>
    /// Interaction logic for OutputContainerView.xaml
    /// </summary>
    public partial class OutputContainerView : UserControl
    {
        public OutputContainerView()
        {
            InitializeComponent();

            O1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            O2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            O3.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            O4.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));

            O1.Background = new SolidColorBrush(Colors.LightBlue);
            O2.Background = new SolidColorBrush(Colors.LightGray);
            O3.Background = new SolidColorBrush(Colors.LightGray);
            O4.Background = new SolidColorBrush(Colors.LightGray);

            O1.BorderThickness = new Thickness(2);
            //O2.BorderThickness = new Thickness(2);
            //O3.BorderThickness = new Thickness(2);
            //O4.BorderThickness = new Thickness(2);

            O2.BorderThickness = new Thickness(0);
            O3.BorderThickness = new Thickness(0);
            O4.BorderThickness = new Thickness(0);





        }

        private void O1_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.Id = 1;
            O1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            O1.BorderThickness = new Thickness(2);
            //------------------------------------
            O2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            O3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            O4.BorderBrush = new SolidColorBrush(Colors.LightGray);

            O4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            O3.Background = new SolidColorBrush(Colors.LightGray);
            O2.Background = new SolidColorBrush(Colors.LightGray);
            O1.Background = new SolidColorBrush(Colors.LightBlue);
            O4.Background = new SolidColorBrush(Colors.LightGray);


        }

        private void O2_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.Id = 4;
            O2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            O2.BorderThickness = new Thickness(2);
            //------------------------------------
            O1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            O3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            O4.BorderBrush = new SolidColorBrush(Colors.LightGray);

            O4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            O3.Background = new SolidColorBrush(Colors.LightGray);
            O2.Background = new SolidColorBrush(Colors.LightBlue);
            O1.Background = new SolidColorBrush(Colors.LightGray);
            O4.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void O3_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.Id = 5;
            O3.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            O3.BorderThickness = new Thickness(2);
            //------------------------------------
            O4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            O2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            O1.BorderBrush = new SolidColorBrush(Colors.LightGray);


            O4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            O3.Background = new SolidColorBrush(Colors.LightBlue);
            O2.Background = new SolidColorBrush(Colors.LightGray);
            O1.Background = new SolidColorBrush(Colors.LightGray);
            O4.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void O4_Click(object sender, RoutedEventArgs e)
        {

            StaticHelper.Id = 6;
            O4.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            O4.BorderThickness = new Thickness(2);
            //------------------------------------

            O3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            O2.Background = new SolidColorBrush(Colors.LightGray);
            O1.BorderBrush = new SolidColorBrush(Colors.LightGray);

            O4.Background = new SolidColorBrush(Colors.LightBlue);
            O3.Background = new SolidColorBrush(Colors.LightGray);
            O2.Background = new SolidColorBrush(Colors.LightGray);
            O1.Background = new SolidColorBrush(Colors.LightGray);

        }
    }
}
