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

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for LooseEquipmentListView.xaml
    /// </summary>
    public partial class LooseEquipmentListView : UserControl
    {
        public LooseEquipmentListView()
        {
            InitializeComponent();
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.LooseETypeId = 1;
            B1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B1.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B5.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B6.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B7.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B8.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B9.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B10.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B11.BorderBrush = new SolidColorBrush(Colors.LightGray);

            //B3.Background = new SolidColorBrush(Colors.LightGray);
            //B2.Background = new SolidColorBrush(Colors.LightGray);
            B1.Background = new SolidColorBrush(Colors.LightBlue);

            B8.Background = new SolidColorBrush(Colors.LightGray);
            B9.Background = new SolidColorBrush(Colors.LightGray);
           // B10.Background = new SolidColorBrush(Colors.LightGray);
            //B11.Background = new SolidColorBrush(Colors.LightGray);
            B4.Background = new SolidColorBrush(Colors.LightGray);
            B5.Background = new SolidColorBrush(Colors.LightGray);
            B6.Background = new SolidColorBrush(Colors.LightGray);
            B7.Background = new SolidColorBrush(Colors.LightGray);
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
           

            JShackleGrid.Visibility = Visibility.Visible;
            CStopperGrid.Visibility = Visibility.Hidden;
            RTailGrid.Visibility = Visibility.Hidden;
            MessngerRGrid.Visibility = Visibility.Hidden;
            FirewireGrid.Visibility = Visibility.Hidden;
            ChafeGuardGrid.Visibility = Visibility.Hidden;
            WinchBrkTestKitGrid.Visibility = Visibility.Hidden;
            SuezRGrid.Visibility = Visibility.Hidden;
            PennantRGrid.Visibility = Visibility.Hidden;
            GrommetRGrid.Visibility = Visibility.Hidden;
            TowingRGrid.Visibility = Visibility.Hidden;

            B2.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
            B5.BorderThickness = new Thickness(1);
            B6.BorderThickness = new Thickness(1);
            B7.BorderThickness = new Thickness(1);
           // B10.BorderThickness = new Thickness(1);
            B8.BorderThickness = new Thickness(1);
            B9.BorderThickness = new Thickness(1);
            //B11.BorderThickness = new Thickness(1);



        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.LooseETypeId = 4;
            B2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B2.BorderThickness = new Thickness(2);
            //------------------------------------
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B5.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B6.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B7.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B8.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B9.BorderBrush = new SolidColorBrush(Colors.LightGray);
           // B10.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B11.BorderBrush = new SolidColorBrush(Colors.LightGray);



            B1.Background = new SolidColorBrush(Colors.LightGray);
            B3.Background = new SolidColorBrush(Colors.LightGray);
            B4.Background = new SolidColorBrush(Colors.LightGray);
            B5.Background = new SolidColorBrush(Colors.LightGray);
            B6.Background = new SolidColorBrush(Colors.LightGray);
            B7.Background = new SolidColorBrush(Colors.LightGray);
            B8.Background = new SolidColorBrush(Colors.LightGray);
            B9.Background = new SolidColorBrush(Colors.LightGray);
            //B10.Background = new SolidColorBrush(Colors.LightGray);
           // B11.Background = new SolidColorBrush(Colors.LightGray);
            B2.Background = new SolidColorBrush(Colors.LightBlue);
            //B6.Background = new SolidColorBrush(Colors.LightGray);
            //B7.Background = new SolidColorBrush(Colors.LightGray);

            JShackleGrid.Visibility = Visibility.Hidden;
            CStopperGrid.Visibility = Visibility.Hidden;
            RTailGrid.Visibility = Visibility.Visible;
            MessngerRGrid.Visibility = Visibility.Hidden;
            FirewireGrid.Visibility = Visibility.Hidden;
            ChafeGuardGrid.Visibility = Visibility.Hidden;
            WinchBrkTestKitGrid.Visibility = Visibility.Hidden;
            SuezRGrid.Visibility = Visibility.Hidden;
            PennantRGrid.Visibility = Visibility.Hidden;
            GrommetRGrid.Visibility = Visibility.Hidden;
            TowingRGrid.Visibility = Visibility.Hidden;

            B1.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
            B5.BorderThickness = new Thickness(1);
            B6.BorderThickness = new Thickness(1);
            B7.BorderThickness = new Thickness(1);
           // B10.BorderThickness = new Thickness(1);
            B8.BorderThickness = new Thickness(1);
            B9.BorderThickness = new Thickness(1);
            //B11.BorderThickness = new Thickness(1);

        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.LooseETypeId = 5;
            B3.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B3.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B5.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B6.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B7.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B8.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B9.BorderBrush = new SolidColorBrush(Colors.LightGray);
           // B10.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B11.BorderBrush = new SolidColorBrush(Colors.LightGray);




            B1.Background = new SolidColorBrush(Colors.LightGray);
            B2.Background = new SolidColorBrush(Colors.LightGray);
            B4.Background = new SolidColorBrush(Colors.LightGray);
            B5.Background = new SolidColorBrush(Colors.LightGray);
            B6.Background = new SolidColorBrush(Colors.LightGray);
            B7.Background = new SolidColorBrush(Colors.LightGray);
            B8.Background = new SolidColorBrush(Colors.LightGray);
            B9.Background = new SolidColorBrush(Colors.LightGray);
            //B10.Background = new SolidColorBrush(Colors.LightGray);
            //B11.Background = new SolidColorBrush(Colors.LightGray);
            B3.Background = new SolidColorBrush(Colors.LightBlue);
            //B6.Background = new SolidColorBrush(Colors.LightGray);
            //B7.Background = new SolidColorBrush(Colors.LightGray);

            JShackleGrid.Visibility = Visibility.Hidden;
            CStopperGrid.Visibility = Visibility.Visible;
            RTailGrid.Visibility = Visibility.Hidden;
            MessngerRGrid.Visibility = Visibility.Hidden;
            FirewireGrid.Visibility = Visibility.Hidden;
            ChafeGuardGrid.Visibility = Visibility.Hidden;
            WinchBrkTestKitGrid.Visibility = Visibility.Hidden;
            SuezRGrid.Visibility = Visibility.Hidden;
            PennantRGrid.Visibility = Visibility.Hidden;
            GrommetRGrid.Visibility = Visibility.Hidden;
            TowingRGrid.Visibility = Visibility.Hidden;

            B2.BorderThickness = new Thickness(1);
            B1.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
            B5.BorderThickness = new Thickness(1);
            B6.BorderThickness = new Thickness(1);
            B7.BorderThickness = new Thickness(1);
           // B10.BorderThickness = new Thickness(1);
            B8.BorderThickness = new Thickness(1);
            B9.BorderThickness = new Thickness(1);
            //B11.BorderThickness = new Thickness(1);

        }

        private void B4_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.LooseETypeId = 3;
            B4.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B4.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B5.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B6.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B7.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B8.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B9.BorderBrush = new SolidColorBrush(Colors.LightGray);
           // B10.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B11.BorderBrush = new SolidColorBrush(Colors.LightGray);



            B1.Background = new SolidColorBrush(Colors.LightGray);
            B2.Background = new SolidColorBrush(Colors.LightGray);
            B3.Background = new SolidColorBrush(Colors.LightGray);
            B5.Background = new SolidColorBrush(Colors.LightGray);
            B6.Background = new SolidColorBrush(Colors.LightGray);
            B7.Background = new SolidColorBrush(Colors.LightGray);
            B8.Background = new SolidColorBrush(Colors.LightGray);
            B9.Background = new SolidColorBrush(Colors.LightGray);
            //B10.Background = new SolidColorBrush(Colors.LightGray);
           //B11.Background = new SolidColorBrush(Colors.LightGray);
            //B6.Background = new SolidColorBrush(Colors.LightGray);
            //B7.Background = new SolidColorBrush(Colors.LightGray);
            B4.Background = new SolidColorBrush(Colors.LightBlue);

            JShackleGrid.Visibility = Visibility.Hidden;
            CStopperGrid.Visibility = Visibility.Hidden;
            RTailGrid.Visibility = Visibility.Hidden;
            MessngerRGrid.Visibility = Visibility.Visible;
            FirewireGrid.Visibility = Visibility.Hidden;
            ChafeGuardGrid.Visibility = Visibility.Hidden;
            WinchBrkTestKitGrid.Visibility = Visibility.Hidden;
            SuezRGrid.Visibility = Visibility.Hidden;
            PennantRGrid.Visibility = Visibility.Hidden;
            GrommetRGrid.Visibility = Visibility.Hidden;
            TowingRGrid.Visibility = Visibility.Hidden;

            B2.BorderThickness = new Thickness(1);
            B1.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B5.BorderThickness = new Thickness(1);
            B6.BorderThickness = new Thickness(1);
            B7.BorderThickness = new Thickness(1);
            //B10.BorderThickness = new Thickness(1);
            B8.BorderThickness = new Thickness(1);
            B9.BorderThickness = new Thickness(1);
            //B11.BorderThickness = new Thickness(1);
        }

        private void B5_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.LooseETypeId = 6;
            B5.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B5.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B6.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B7.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B8.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B9.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B10.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B11.BorderBrush = new SolidColorBrush(Colors.LightGray);



            B1.Background = new SolidColorBrush(Colors.LightGray);
            B2.Background = new SolidColorBrush(Colors.LightGray);
            B3.Background = new SolidColorBrush(Colors.LightGray);
            B4.Background = new SolidColorBrush(Colors.LightGray);
            B6.Background = new SolidColorBrush(Colors.LightGray);
            B7.Background = new SolidColorBrush(Colors.LightGray);
            B8.Background = new SolidColorBrush(Colors.LightGray);
            B9.Background = new SolidColorBrush(Colors.LightGray);
            //B10.Background = new SolidColorBrush(Colors.LightGray);
           // B11.Background = new SolidColorBrush(Colors.LightGray);
            //B6.Background = new SolidColorBrush(Colors.LightGray);
            //B7.Background = new SolidColorBrush(Colors.LightGray);
            B5.Background = new SolidColorBrush(Colors.LightBlue);

            JShackleGrid.Visibility = Visibility.Hidden;
            CStopperGrid.Visibility = Visibility.Hidden;
            RTailGrid.Visibility = Visibility.Hidden;
            MessngerRGrid.Visibility = Visibility.Hidden;
            FirewireGrid.Visibility = Visibility.Visible;
            ChafeGuardGrid.Visibility = Visibility.Hidden;
            WinchBrkTestKitGrid.Visibility = Visibility.Hidden;
            SuezRGrid.Visibility = Visibility.Hidden;
            PennantRGrid.Visibility = Visibility.Hidden;
            GrommetRGrid.Visibility = Visibility.Hidden;
            TowingRGrid.Visibility = Visibility.Hidden;

            B2.BorderThickness = new Thickness(1);
            B1.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
            B6.BorderThickness = new Thickness(1);
            B7.BorderThickness = new Thickness(1);
            //B10.BorderThickness = new Thickness(1);
            B8.BorderThickness = new Thickness(1);
            B9.BorderThickness = new Thickness(1);
           // B11.BorderThickness = new Thickness(1);
        }

        private void B6_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.LooseETypeId = 7;
            B6.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B6.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B5.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B7.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B8.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B9.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B10.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B11.BorderBrush = new SolidColorBrush(Colors.LightGray);


            B1.Background = new SolidColorBrush(Colors.LightGray);
            B2.Background = new SolidColorBrush(Colors.LightGray);
            B3.Background = new SolidColorBrush(Colors.LightGray);
            B4.Background = new SolidColorBrush(Colors.LightGray);
            B8.Background = new SolidColorBrush(Colors.LightGray);
            B9.Background = new SolidColorBrush(Colors.LightGray);
            //B10.Background = new SolidColorBrush(Colors.LightGray);
           // B11.Background = new SolidColorBrush(Colors.LightGray);
            B5.Background = new SolidColorBrush(Colors.LightGray);
            B7.Background = new SolidColorBrush(Colors.LightGray);
            B6.Background = new SolidColorBrush(Colors.LightBlue);

            JShackleGrid.Visibility = Visibility.Hidden;
            CStopperGrid.Visibility = Visibility.Hidden;
            RTailGrid.Visibility = Visibility.Hidden;
            MessngerRGrid.Visibility = Visibility.Hidden;
            FirewireGrid.Visibility = Visibility.Hidden;
            ChafeGuardGrid.Visibility = Visibility.Visible;
            WinchBrkTestKitGrid.Visibility = Visibility.Hidden;
            SuezRGrid.Visibility = Visibility.Hidden;
            PennantRGrid.Visibility = Visibility.Hidden;
            GrommetRGrid.Visibility = Visibility.Hidden;
            TowingRGrid.Visibility = Visibility.Hidden;

            B2.BorderThickness = new Thickness(1);
            B1.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
            B5.BorderThickness = new Thickness(1);
            B7.BorderThickness = new Thickness(1);
            //B10.BorderThickness = new Thickness(1);
            B8.BorderThickness = new Thickness(1);
            B9.BorderThickness = new Thickness(1);
            //B11.BorderThickness = new Thickness(1);
        }

        private void B7_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.LooseETypeId = 8;
            B7.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B7.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B6.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B5.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B8.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B9.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B10.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B11.BorderBrush = new SolidColorBrush(Colors.LightGray);


            B1.Background = new SolidColorBrush(Colors.LightGray);
            B2.Background = new SolidColorBrush(Colors.LightGray);
            B3.Background = new SolidColorBrush(Colors.LightGray);
            B4.Background = new SolidColorBrush(Colors.LightGray);
            B8.Background = new SolidColorBrush(Colors.LightGray);
            B9.Background = new SolidColorBrush(Colors.LightGray);
           // B10.Background = new SolidColorBrush(Colors.LightGray);
            //B11.Background = new SolidColorBrush(Colors.LightGray);
            B6.Background = new SolidColorBrush(Colors.LightGray);
            B5.Background = new SolidColorBrush(Colors.LightGray);
            B7.Background = new SolidColorBrush(Colors.LightBlue);

            JShackleGrid.Visibility = Visibility.Hidden;
            CStopperGrid.Visibility = Visibility.Hidden;
            RTailGrid.Visibility = Visibility.Hidden;
            MessngerRGrid.Visibility = Visibility.Hidden;
            FirewireGrid.Visibility = Visibility.Hidden;
            ChafeGuardGrid.Visibility = Visibility.Hidden;
            WinchBrkTestKitGrid.Visibility = Visibility.Visible;

            SuezRGrid.Visibility = Visibility.Hidden;
            PennantRGrid.Visibility = Visibility.Hidden;
            GrommetRGrid.Visibility = Visibility.Hidden;
            TowingRGrid.Visibility = Visibility.Hidden;

            B2.BorderThickness = new Thickness(1);
            B1.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
            B6.BorderThickness = new Thickness(1);
            B5.BorderThickness = new Thickness(1);

           // B10.BorderThickness = new Thickness(1);
            B8.BorderThickness = new Thickness(1);
            B9.BorderThickness = new Thickness(1);
            //B11.BorderThickness = new Thickness(1);
        }

        private void B8_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.LooseETypeId = 9;
            B8.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B8.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B6.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B5.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B7.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B9.BorderBrush = new SolidColorBrush(Colors.LightGray);
           //B10.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B11.BorderBrush = new SolidColorBrush(Colors.LightGray);


            B1.Background = new SolidColorBrush(Colors.LightGray);
            B2.Background = new SolidColorBrush(Colors.LightGray);
            B3.Background = new SolidColorBrush(Colors.LightGray);
            B4.Background = new SolidColorBrush(Colors.LightGray);
            B7.Background = new SolidColorBrush(Colors.LightGray);
            B9.Background = new SolidColorBrush(Colors.LightGray);
           // B10.Background = new SolidColorBrush(Colors.LightGray);
            //B11.Background = new SolidColorBrush(Colors.LightGray);
            B6.Background = new SolidColorBrush(Colors.LightGray);
            B5.Background = new SolidColorBrush(Colors.LightGray);
            B8.Background = new SolidColorBrush(Colors.LightBlue);

            JShackleGrid.Visibility = Visibility.Hidden;
            CStopperGrid.Visibility = Visibility.Hidden;
            RTailGrid.Visibility = Visibility.Hidden;
            MessngerRGrid.Visibility = Visibility.Hidden;
            FirewireGrid.Visibility = Visibility.Hidden;
            ChafeGuardGrid.Visibility = Visibility.Hidden;
            WinchBrkTestKitGrid.Visibility = Visibility.Hidden;
            SuezRGrid.Visibility = Visibility.Hidden;
            PennantRGrid.Visibility = Visibility.Hidden;
            GrommetRGrid.Visibility = Visibility.Hidden;
            TowingRGrid.Visibility = Visibility.Visible;

            B2.BorderThickness = new Thickness(1);
            B1.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
            B6.BorderThickness = new Thickness(1);
            B5.BorderThickness = new Thickness(1);

            B7.BorderThickness = new Thickness(1);
            B9.BorderThickness = new Thickness(1);
            //B10.BorderThickness = new Thickness(1);
            //B11.BorderThickness = new Thickness(1);
        }

        private void B9_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.LooseETypeId = 10;
            B9.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            B9.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B6.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B5.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B7.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B8.BorderBrush = new SolidColorBrush(Colors.LightGray);
           // B10.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B11.BorderBrush = new SolidColorBrush(Colors.LightGray);


            B1.Background = new SolidColorBrush(Colors.LightGray);
            B2.Background = new SolidColorBrush(Colors.LightGray);
            B3.Background = new SolidColorBrush(Colors.LightGray);
            B4.Background = new SolidColorBrush(Colors.LightGray);
            B7.Background = new SolidColorBrush(Colors.LightGray);
            B8.Background = new SolidColorBrush(Colors.LightGray);
            //B10.Background = new SolidColorBrush(Colors.LightGray);
            //B11.Background = new SolidColorBrush(Colors.LightGray);
            B6.Background = new SolidColorBrush(Colors.LightGray);
            B5.Background = new SolidColorBrush(Colors.LightGray);
            B9.Background = new SolidColorBrush(Colors.LightBlue);

            JShackleGrid.Visibility = Visibility.Hidden;
            CStopperGrid.Visibility = Visibility.Hidden;
            RTailGrid.Visibility = Visibility.Hidden;
            MessngerRGrid.Visibility = Visibility.Hidden;
            FirewireGrid.Visibility = Visibility.Hidden;
            ChafeGuardGrid.Visibility = Visibility.Hidden;
            WinchBrkTestKitGrid.Visibility = Visibility.Hidden;
            SuezRGrid.Visibility = Visibility.Visible;
            PennantRGrid.Visibility = Visibility.Hidden;
            GrommetRGrid.Visibility = Visibility.Hidden;
            TowingRGrid.Visibility = Visibility.Hidden;

            B2.BorderThickness = new Thickness(1);
            B1.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
            B6.BorderThickness = new Thickness(1);
            B5.BorderThickness = new Thickness(1);

            B7.BorderThickness = new Thickness(1);
            B8.BorderThickness = new Thickness(1);
            //B10.BorderThickness = new Thickness(1);
            //B11.BorderThickness = new Thickness(1);
        }

        private void B10_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.LooseETypeId = 11;
           // B10.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            //B10.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B6.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B5.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B7.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B8.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B9.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B11.BorderBrush = new SolidColorBrush(Colors.LightGray);



            B1.Background = new SolidColorBrush(Colors.LightGray);
            B2.Background = new SolidColorBrush(Colors.LightGray);
            B3.Background = new SolidColorBrush(Colors.LightGray);
            B4.Background = new SolidColorBrush(Colors.LightGray);
            B8.Background = new SolidColorBrush(Colors.LightGray);
            B9.Background = new SolidColorBrush(Colors.LightGray);
            B7.Background = new SolidColorBrush(Colors.LightGray);
            //B11.Background = new SolidColorBrush(Colors.LightGray);
            B6.Background = new SolidColorBrush(Colors.LightGray);
            B5.Background = new SolidColorBrush(Colors.LightGray);
            //B10.Background = new SolidColorBrush(Colors.LightBlue);

            JShackleGrid.Visibility = Visibility.Hidden;
            CStopperGrid.Visibility = Visibility.Hidden;
            RTailGrid.Visibility = Visibility.Hidden;
            MessngerRGrid.Visibility = Visibility.Hidden;
            FirewireGrid.Visibility = Visibility.Hidden;
            ChafeGuardGrid.Visibility = Visibility.Hidden;
            WinchBrkTestKitGrid.Visibility = Visibility.Hidden;
            SuezRGrid.Visibility = Visibility.Hidden;
            PennantRGrid.Visibility = Visibility.Visible;
            GrommetRGrid.Visibility = Visibility.Hidden;
            TowingRGrid.Visibility = Visibility.Hidden;

            B2.BorderThickness = new Thickness(1);
            B1.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
            B6.BorderThickness = new Thickness(1);
            B5.BorderThickness = new Thickness(1);

            B7.BorderThickness = new Thickness(1);
            B8.BorderThickness = new Thickness(1);
            B9.BorderThickness = new Thickness(1);
           // B11.BorderThickness = new Thickness(1);
        }

        private void B11_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.LooseETypeId = 12;
            //B11.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            //B11.BorderThickness = new Thickness(2);
            //------------------------------------
            B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B4.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B6.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B5.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B7.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B8.BorderBrush = new SolidColorBrush(Colors.LightGray);
            B9.BorderBrush = new SolidColorBrush(Colors.LightGray);
            //B10.BorderBrush = new SolidColorBrush(Colors.LightGray);


            B1.Background = new SolidColorBrush(Colors.LightGray);
            B2.Background = new SolidColorBrush(Colors.LightGray);
            B3.Background = new SolidColorBrush(Colors.LightGray);
            B4.Background = new SolidColorBrush(Colors.LightGray);
            B8.Background = new SolidColorBrush(Colors.LightGray);
            B9.Background = new SolidColorBrush(Colors.LightGray);
           // B10.Background = new SolidColorBrush(Colors.LightGray);
            B7.Background = new SolidColorBrush(Colors.LightGray);
            B6.Background = new SolidColorBrush(Colors.LightGray);
            B5.Background = new SolidColorBrush(Colors.LightGray);
            //B11.Background = new SolidColorBrush(Colors.LightBlue);

            JShackleGrid.Visibility = Visibility.Hidden;
            CStopperGrid.Visibility = Visibility.Hidden;
            RTailGrid.Visibility = Visibility.Hidden;
            MessngerRGrid.Visibility = Visibility.Hidden;
            FirewireGrid.Visibility = Visibility.Hidden;
            ChafeGuardGrid.Visibility = Visibility.Hidden;
            WinchBrkTestKitGrid.Visibility = Visibility.Hidden;
            SuezRGrid.Visibility = Visibility.Hidden;
            PennantRGrid.Visibility = Visibility.Hidden;
            GrommetRGrid.Visibility = Visibility.Visible;
            TowingRGrid.Visibility = Visibility.Hidden;

            B2.BorderThickness = new Thickness(1);
            B1.BorderThickness = new Thickness(1);
            B3.BorderThickness = new Thickness(1);
            B4.BorderThickness = new Thickness(1);
            B6.BorderThickness = new Thickness(1);
            B5.BorderThickness = new Thickness(1);

            B7.BorderThickness = new Thickness(1);
            B8.BorderThickness = new Thickness(1);
            //B10.BorderThickness = new Thickness(1);
            B9.BorderThickness = new Thickness(1);
        }

        //private void B4_Click(object sender, RoutedEventArgs e)
        //{
        //    B4.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
        //    B4.BorderThickness = new Thickness(2);
        //    //------------------------------------
        //    B2.BorderBrush = new SolidColorBrush(Colors.LightGray);
        //    B3.BorderBrush = new SolidColorBrush(Colors.LightGray);
        //    B1.BorderBrush = new SolidColorBrush(Colors.LightGray);

        //    B2.BorderThickness = new Thickness(1);
        //    B3.BorderThickness = new Thickness(1);
        //    B1.BorderThickness = new Thickness(1);
        //}
    }
}
