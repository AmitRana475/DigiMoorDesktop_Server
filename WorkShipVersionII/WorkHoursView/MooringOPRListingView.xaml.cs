using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for MooringOPRListingView.xaml
    /// </summary>
    public partial class MooringOPRListingView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public MooringOPRListingView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
        }
        int check = 0; int msgchk = 0;
        private void txtInspectBy_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var ss = txtInspectBy.Text;

                check = 0;
                var PortNames = new ObservableCollection<string>();
                var data = sc.PortListtbl.Where(x => x.PortName.ToLower().Contains(ss.Trim().ToLower())).Select(x => new { x.PortName }).ToList().Distinct();

                foreach (var item in data)
                {
                    PortNames.Add(item.PortName);
                    check = 1;
                    msgchk = 0;
                }
                if (check == 0)
                {
                    if (msgchk == 0)
                    {
                        if (ss != "")
                        {
                            MessageBox.Show("Port Name does not exist in the list, please search and select name from the list !", "Wrong Input", MessageBoxButton.OK, MessageBoxImage.Warning);

                            check = 1;
                            msgchk = 1;
                            txtInspectBy.Text = string.Empty;
                        }
                    }
                }
            }
            catch { }
        }

        private void lbSuggestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbSuggestion.SelectedIndex > -1)
            {
                if (e.Key == Key.Enter)
                {
                    txtInspectBy.Text = lbSuggestion.SelectedItem.ToString();
                    lbSuggestion.Visibility = Visibility.Collapsed;
                }


            }
        }

        private void lbSuggestion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbSuggestion.SelectedIndex > -1)
            {

                txtInspectBy.Text = lbSuggestion.SelectedItem.ToString();

                if (txtInspectBy.Text == "Others")
                {
                    txtfac.Visibility = Visibility.Collapsed;
                   // txtoth.Visibility = Visibility.Visible;
                    txtportname.Visibility = Visibility.Visible;
                    txtprtnm.Visibility = Visibility.Visible;
                }
                else
                {
                    txtfac.Visibility = Visibility.Visible;
                   // txtoth.Visibility = Visibility.Collapsed;
                    txtportname.Visibility = Visibility.Collapsed;
                    txtprtnm.Visibility = Visibility.Collapsed;
                }

                lbSuggestion.Visibility = Visibility.Collapsed;

            }
        }

        private void DpFastDate_CalendarClosed(object sender, RoutedEventArgs e)
        {

        }

        private void txtInspectBy_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                lbSuggestion.Focus();
            }
        }

        private void lbSuggestion_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lbSuggestion.SelectedIndex > -1)
            {

                txtInspectBy.Text = lbSuggestion.SelectedItem.ToString();

                if (txtInspectBy.Text == "Others")
                {
                    txtfac.Visibility = Visibility.Collapsed;
                    //txtoth.Visibility = Visibility.Visible;
                    txtportname.Visibility = Visibility.Visible;
                    txtprtnm.Visibility = Visibility.Visible;
                }
                else
                {
                    txtfac.Visibility = Visibility.Visible;
                   // txtoth.Visibility = Visibility.Collapsed;
                    txtportname.Visibility = Visibility.Collapsed;
                    txtprtnm.Visibility = Visibility.Collapsed;
                }

                lbSuggestion.Visibility = Visibility.Collapsed;

            }
        }
    }
}
