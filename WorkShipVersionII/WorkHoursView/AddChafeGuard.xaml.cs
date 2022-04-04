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
    /// Interaction logic for AddChafeGuard.xaml
    /// </summary>
    public partial class AddChafeGuard : UserControl
    {
        private readonly ShipmentContaxt sc;

        static DateTime RevDate = DateTime.Now.Date;
        static DateTime InsDate = DateTime.Now.Date;
        public AddChafeGuard()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
        }

        private void rdb1_Click(object sender, RoutedEventArgs e)
        {
            AddChafeGuardViewModel._AddChafeGuard.IsRopeInstalled = "No";
            cnv1.Visibility = Visibility.Hidden;
            dpInsDate.Visibility = Visibility.Hidden;
        }

        private void rdb2_Click(object sender, RoutedEventArgs e)
        {
            AddChafeGuardViewModel._AddChafeGuard.IsRopeInstalled = "Yes";
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
                    MessageBox.Show("Installed date can not be less than Received date!", "Chafe Guard", MessageBoxButton.OK, MessageBoxImage.Error);
                    dpInsDate.Text = RevDate.ToShortDateString();
                    AddRopeTailViewModel._InstallDate = RevDate;
                }
            }
            catch { }
        }
    }
}
