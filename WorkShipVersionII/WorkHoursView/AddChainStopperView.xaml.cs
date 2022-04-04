using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddChainStopperView.xaml
    /// </summary>
    public partial class AddChainStopperView : UserControl
    {
        private readonly ShipmentContaxt sc;
        static DateTime RevDate = DateTime.Now.Date;
        static DateTime InsDate = DateTime.Now.Date;
        public AddChainStopperView()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
        }

        private void comboReOutofSer_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = comboReOutofSer.Text.ToString();
            if (damageOb == "Other")
            {
                bntSection.SetValue(Grid.RowProperty, 19);
                lblothrs.Visibility = Visibility.Visible;
                txtotherreason.Visibility = Visibility.Visible;

                lbldmgob.Visibility = Visibility.Hidden;
                comboDamageOb.Visibility = Visibility.Hidden;
                lblMoorOp.Visibility = Visibility.Hidden;
                comboMoorOp.Visibility = Visibility.Hidden;

            }

            else if (damageOb == "Damaged")
            {
                bntSection.SetValue(Grid.RowProperty, 21);
                lblothrs.Visibility = Visibility.Hidden;
                txtotherreason.Visibility = Visibility.Hidden;

                lbldmgob.Visibility = Visibility.Visible;
                comboDamageOb.Visibility = Visibility.Visible;
                lblMoorOp.Visibility = Visibility.Hidden;
                comboMoorOp.Visibility = Visibility.Hidden;
            }
            else
            {
                bntSection.SetValue(Grid.RowProperty, 17);
                lblothrs.Visibility = Visibility.Hidden;
                txtotherreason.Visibility = Visibility.Hidden;

                lbldmgob.Visibility = Visibility.Hidden;
                comboDamageOb.Visibility = Visibility.Hidden;
                lblMoorOp.Visibility = Visibility.Hidden;
                comboMoorOp.Visibility = Visibility.Hidden;
            }

        }

        private void cboBhp_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = comboDamageOb.Text.ToString();
            if (damageOb == "Mooring Operation")
            {
                bntSection.SetValue(Grid.RowProperty, 21);
                lblMoorOp.Visibility = Visibility.Visible;
                comboMoorOp.Visibility = Visibility.Visible;
            }
            else
            {
                bntSection.SetValue(Grid.RowProperty, 19);
                lblMoorOp.Visibility = Visibility.Hidden;
                comboMoorOp.Visibility = Visibility.Hidden;
            }
        }

        private void txtMBL_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void txtLength_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void txtMBL_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool check = sc.DecimalCheck(Convert.ToInt32(txtMBL.Text));
                if (check == false)
                {
                    txtMBL.Text = "";
                }

            }
            catch { }
        }

        private void txtLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool check = sc.DecimalCheck(Convert.ToInt32(txtLength.Text));
                if (check == false)
                {
                    txtLength.Text = "";
                }

            }
            catch { }
        }

        private void rdb1_Click(object sender, RoutedEventArgs e)
        {
            AddChainStopperViewModel._AddChainStopper.IsRopeInstalled = "No";
            cnv1.Visibility = Visibility.Hidden;

            dpInsDate.Visibility = Visibility.Hidden;
        }

        private void rdb2_Click(object sender, RoutedEventArgs e)
        {
            AddChainStopperViewModel._AddChainStopper.IsRopeInstalled = "Yes";
            cnv1.Visibility = Visibility.Visible;

            dpInsDate.Visibility = Visibility.Visible;
        }

        private void DpInsDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var Frm = dpRecDate.Text;
                var datefrom = Convert.ToDateTime(Frm);

                var to = dpInsDate.Text;
                var dateto = Convert.ToDateTime(to);

                if (dateto < datefrom)
                {
                    MessageBox.Show("Installed date can not be less than Received date!", "Chain Stopper", MessageBoxButton.OK, MessageBoxImage.Error);
                    AddChainStopperViewModel._AddChainStopper.DateInstalled = RevDate;
                    dpInsDate.Text = RevDate.ToShortDateString();
                    //dpInsDate.Text = DateTime.Now.ToShortDateString();
                }
            }
            catch { }
        }
    }
}
