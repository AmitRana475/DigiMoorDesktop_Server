using DataBuildingLayer;
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
using WorkShipVersionII.WorkHoursViewModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Markup;
using System.Globalization;

namespace WorkShipVersionII.WorkHoursView
{
       /// <summary>
       /// Interaction logic for AssignRopeToWinchView.xaml
       /// </summary>
       public partial class AssignRopeToWinchView : UserControl
       {
        private readonly ShipmentContaxt sc;
        public AssignRopeToWinchView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            comboLead.SelectedIndex = 0;
           // AssignRopeToWinchViewModel._AddAssWinchRope.Lead = comboLead.Text;
            sc = new ShipmentContaxt();
        }

        private void comboAssrope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = sc.MooringWinch.Where(x => x.Id == AssignRopeToWinchViewModel._sropeass.Id && x.IsActive == true).FirstOrDefault();

            txtNewAssignedLocation.Text = data.Location;
            txtLead.Text = data.Lead;

            if (comboRank.Text != "--Select--")
            {
                btnSave.IsEnabled = true;

                //var data1 = sc.MooringWinchRope.Where(x => x.Id == AssignRopeToWinchViewModel.sropetype.Id).Select(x => x.InstalledDate).SingleOrDefault();

                //var installeddate = data1;
            }
        }

        private void comboRank_DropDownClosed(object sender, EventArgs e)
        {
            //btnSave.IsEnabled = true;
        }

        private void comboRank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboAssrope.Text != "--Select--")
            {
                btnSave.IsEnabled = true;


                //var data = sc.MooringWinchRope.Where(x => x.Id == AssignRopeToWinchViewModel.sropetype.Id).Select(x => x.InstalledDate).SingleOrDefault();

                //var installeddate = data;

                //SqlDataAdapter adp = new SqlDataAdapter("",sc.con);
                //DataTable dt = new DataTable();
                //adp.Fill(dt);
            }
        }

        private void dpAssinDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = sc.MooringWinchRope.Where(x => x.Id == AssignRopeToWinchViewModel.sropetype.Id && x.DeleteStatus == false).Select(x => x.InstalledDate).SingleOrDefault();

                var installeddate = data;

                var assigndate = dpAssinDate.Text;

                if (Convert.ToDateTime(assigndate) < installeddate)
                {
                    MessageBox.Show("Assign Date can not less than Line Installed date!", "Assign Line To Winch", MessageBoxButton.OK, MessageBoxImage.Error);

                    dpAssinDate.Text = installeddate.ToString();
                }
            }
            catch { }
        }

        private void ComboLead_DropDownClosed(object sender, EventArgs e)
        {
           // AssignRopeToWinchViewModel._AddAssWinchRope.Lead = comboLead.Text;
        }
    }
}
