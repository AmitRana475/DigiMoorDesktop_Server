using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for AddWBTestKitView.xaml
    /// </summary>
    public partial class AddWBTestKitView : UserControl
    {
        private readonly ShipmentContaxt sc;
        static DateTime RevDate = DateTime.Now.Date;
        static DateTime InsDate = DateTime.Now.Date;
        public AddWBTestKitView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
        }

        private void rdb1_Click(object sender, RoutedEventArgs e)
        {
            WBTestKitViewModel._AddWBTestKit.IsRopeInstalled = "No";
            cnv1.Visibility = Visibility.Hidden;
            dpInsDate.Visibility = Visibility.Hidden;
        }

        private void rdb2_Click(object sender, RoutedEventArgs e)
        {
            WBTestKitViewModel._AddWBTestKit.IsRopeInstalled = "Yes";
            cnv1.Visibility = Visibility.Visible;
            dpInsDate.Visibility = Visibility.Visible;
        }

        private void DpInsDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var Frm = dpIRecDate.Text;
                var datefrom = Convert.ToDateTime(Frm);

                var to = dpInsDate.Text;
                var dateto = Convert.ToDateTime(to);

                if (dateto < datefrom)
                {
                    MessageBox.Show("Installed date can not be less than Received date!", "Winch Brake Test Kit Record", MessageBoxButton.OK, MessageBoxImage.Error);
                    WBTestKitViewModel._AddWBTestKit.InstalledDate= RevDate;
                    dpInsDate.Text = RevDate.ToShortDateString();
                    //dpInsDate.Text = DateTime.Now.ToShortDateString();
                }
            }
            catch { }
        }
    }
}
