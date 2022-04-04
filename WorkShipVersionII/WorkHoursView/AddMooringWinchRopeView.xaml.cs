using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for AddMooringWinchRopeView.xaml
    /// </summary>
    public partial class AddMooringWinchRopeView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public AddMooringWinchRopeView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            sc = new ShipmentContaxt();
        }



        private void comboReOutofSer_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = comboReOutofSer.Text.ToString();

            if (damageOb == "Other")
            {
               // lblothrs.Visibility = Visibility.Visible;
                txtotherreason.Visibility = Visibility.Visible;
                lbldmgob.Visibility = Visibility.Hidden;
                comboDamageOb.Visibility = Visibility.Hidden;


                lblincident.Visibility = Visibility.Hidden;
                comboincident.Visibility = Visibility.Hidden;
                lblMoorOp.Visibility = Visibility.Hidden;
                comboMoorOp.Visibility = Visibility.Hidden;
            }
            else
            {

                lbldmgob.Visibility = Visibility.Visible;
                comboDamageOb.Visibility = Visibility.Visible;
                //lblothrs.Visibility = Visibility.Hidden;
                txtotherreason.Visibility = Visibility.Hidden;

            }
        }

        private void comboDamageOb_DropDownClosed(object sender, EventArgs e)
        {
            string dmg = comboDamageOb.Text.ToString();

            if (dmg == "Mooring Operation")
            {
                lblincident.Visibility = Visibility.Visible;
                comboincident.Visibility = Visibility.Visible;
                lblMoorOp.Visibility = Visibility.Visible;
                comboMoorOp.Visibility = Visibility.Visible;
            }
            else
            {

                lblincident.Visibility = Visibility.Hidden;
                comboincident.Visibility = Visibility.Hidden;
                lblMoorOp.Visibility = Visibility.Hidden;
                comboMoorOp.Visibility = Visibility.Hidden;

            }
        }

        //private void comboDiameter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
            //Regex regex = new Regex("^((?:[1-9]/d*)|(?:(?=[/d.]+)(?:[1-9]/d*|0)./d+))$");
            //e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));

            //char ch = e.Text[0];

            //if ((Char.IsDigit(ch) || ch == '.'))
            //{
            //    //Here TextBox1.Text is name of your TextBox
            //    if (ch == '.' && comboDiameter.Text.Contains('.'))
            //        e.Handled = true;
            //}
            //else
            //    e.Handled = true;
       // }

        //private void comboDiameter_KeyDown(object sender, KeyEventArgs e)
        //{
            
        //}

        private void rdoNonWatchkeeper_Click(object sender, RoutedEventArgs e)
        {
            if(rdoNonWatchkeeper.IsChecked==true)
            {
                lbldtoutofS.Visibility = Visibility.Visible;
                dpOutofSer.Visibility = Visibility.Visible;
                lblReason.Visibility = Visibility.Visible;
                comboReOutofSer.Visibility = Visibility.Visible;

                AddMooringWinchRopeViewModel._AddMooringWinchRope.IsRopeOutOfS = "Yes";
                //MooringWinchRopeClass.
            }
        }

        private void rdoWatchkeeper_Click(object sender, RoutedEventArgs e)
        {
            if (rdoWatchkeeper.IsChecked == true)
            {
                lbldtoutofS.Visibility = Visibility.Hidden;
                dpOutofSer.Visibility = Visibility.Hidden;
                lblReason.Visibility = Visibility.Hidden;
                comboReOutofSer.Visibility = Visibility.Hidden;

                AddMooringWinchRopeViewModel._AddMooringWinchRope.IsRopeOutOfS = "No";

            }
        }

        private void comboDiameter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void txtLength_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void txtMBL_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void txtLDBF_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void txtWLL_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));

        }

        private void txtMBL_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
              bool check=  sc.DecimalCheck(Convert.ToInt32(txtMBL.Text));
                if(check ==false )
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

        private void txtLDBF_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool check = sc.DecimalCheck(Convert.ToInt32(txtLDBF.Text));
                if (check == false)
                {
                    txtLDBF.Text = "";
                }
            }
            catch { }
        }

        private void txtWLL_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool check = sc.DecimalCheck(Convert.ToInt32(txtWLL.Text));
                if (check == false)
                {
                    txtWLL.Text = "";
                }
            }
            catch { }
        }

        private void comboDiameter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool check = sc.DecimalCheck(Convert.ToInt32(comboDiameter.Text));
                if (check == false)
                {
                    comboDiameter.Text = "";
                }
            }
            catch { }
        }

        private void dpInsDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var Frm = dpRecDate.Text;
                var datefrom = Convert.ToDateTime(Frm);

                var to = dpInsDate.Text;
                var dateto = Convert.ToDateTime(to);

                if (dateto < datefrom)
                {
                    MessageBox.Show("Installed date can not be less than Received date!", "Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Error);
                    dpInsDate.Text = DateTime.Now.ToShortDateString();
                }


                /////==============
                ///

               





            }
            catch (Exception ex) { }
        }

        private void dpRecDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var Frm = dpRecDate.Text;
                var datefrom = Convert.ToDateTime(Frm);

                var to = dpInsDate.Text;
                var dateto = Convert.ToDateTime(to);

                if (dateto < datefrom)
                {
                    MessageBox.Show("Received date can not be greater than Installed date!", "Mooring Rope", MessageBoxButton.OK, MessageBoxImage.Error);
                    dpRecDate.Text = to;
                }
            }
            catch { }
        }

        private void txtActive_DropDownClosed(object sender, EventArgs e)
        {
            if (txtActive.Text == "Active")
            {
                AddMooringWinchRopeViewModel._AddMooringWinchRope.IsActive = true;
            }
            if (txtActive.Text == "InActive")
            {
                AddMooringWinchRopeViewModel._AddMooringWinchRope.IsActive = false;
            }
        }

        private void rdb1_Click(object sender, RoutedEventArgs e)
        {
            AddMooringWinchRopeViewModel._AddMooringWinchRope.IsRopeInstalled = "No";
            cnv1.Visibility = Visibility.Hidden;
            dpInsDate.Visibility = Visibility.Hidden;
           // AddMooringWinchRopeViewModel._AddMooringWinchRope.InstalledDate = ;

        }

        private void rdb2_Click(object sender, RoutedEventArgs e)
        {
            AddMooringWinchRopeViewModel._AddMooringWinchRope.IsRopeInstalled = "Yes";
            cnv1.Visibility = Visibility.Visible;
            dpInsDate.Visibility = Visibility.Visible;
        }

        private void txtCertNumberd_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

                 
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }
    }
}
