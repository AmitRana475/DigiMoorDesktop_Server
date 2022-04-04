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
    /// Interaction logic for AssignRopeToWinchView.xaml
    /// </summary>
    public partial class AssignTailToWinchView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public AssignTailToWinchView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            sc = new ShipmentContaxt();
        }

        private void comboAssrope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = sc.MooringWinch.Where(x => x.Id == AssignTailToWinchViewModel._sropeass.Id && x.IsActive == true).FirstOrDefault();

            txtNewAssignedLocation.Text = data.Location;          
            txtLead.Text = data.Lead;

            if (comboRank.Text != "--Select--")
            {
                btnSave.IsEnabled = true;
            }
        }

        private void comboRank_DropDownClosed(object sender, EventArgs e)
        {
            //btnSave.IsEnabled = true;
        }

        private void comboRank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //var data = sc.MooringWinch.Where(x => x.Id == AssignTailToWinchViewModel._sropeass.Id).FirstOrDefault();

                //txtNewAssignedLocation.Text = data.Location;
                //txtLead.Text = data.Lead;

                if (comboAssrope.Text != "--Select--")
                {
                    btnSave.IsEnabled = true;
                }
            }
            catch { }
        }

        private void DpAssinDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = sc.MooringWinchRope.Where(x => x.Id == AssignTailToWinchViewModel.sropetype.Id && x.DeleteStatus == false).Select(x => x.InstalledDate).SingleOrDefault();

                var installeddate = data;

                var assigndate = dpAssinDate.Text;

                if (Convert.ToDateTime(assigndate) < installeddate)
                {
                    MessageBox.Show("Assign Date can not less than Rope Tail Installed date!", "Assign Rope Tail To Winch", MessageBoxButton.OK, MessageBoxImage.Error);

                    dpAssinDate.Text = installeddate.ToString();
                }
            }
            catch { }
        }
    }
}
