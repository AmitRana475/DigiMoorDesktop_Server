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
    /// Interaction logic for AddRopeTailView.xaml
    /// </summary>
    public partial class AddRopeTailView : UserControl
    {
        private readonly ShipmentContaxt sc;

        static DateTime RevDate = DateTime.Now.Date;
        static DateTime InsDate = DateTime.Now.Date;
       
        public AddRopeTailView()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();

            if (AddRopeTailViewModel._ReceivedDate != null)
                RevDate = Convert.ToDateTime(AddRopeTailViewModel._ReceivedDate);

            if (AddRopeTailViewModel._InstallDate != null)
                InsDate = Convert.ToDateTime(AddRopeTailViewModel._InstallDate);


            try
            {
                int idd = StaticHelper.RtailLooseEtypeid;

                if (idd == 3)
                {
                    hdrtitle.Content = "MESSENGER ROPE DETAILS";
                }
                if (idd == 4)
                {
                    hdrtitle.Content = "ROPE STOPPER DETAILS";
                }
                if (idd == 6)
                {
                    hdrtitle.Content = "FIRE WIRE DETAILS";
                }
                if (idd == 9)
                {
                    hdrtitle.Content = "TOWING ROPE DETAILS";
                }
                if (idd == 10)
                {
                    hdrtitle.Content = "SUEZ ROPE DETAILS";
                }
                if (idd == 11)
                {
                    hdrtitle.Content = "PENNANT ROPE DETAILS";
                }
                if (idd == 12)
                {
                    hdrtitle.Content = "GROMMET ROPE DETAILS";
                }
            }
            catch { }
          
        }

        private void comboReOutofSer_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = comboReOutofSer.Text.ToString();
            if (damageOb == "Other")
            {
                bntSection.SetValue(Grid.RowProperty, 15);
                lblothrs.Visibility = Visibility.Visible;
                txtotherreason.Visibility = Visibility.Visible;

                lbldmgob.Visibility = Visibility.Hidden;
                comboDamageOb.Visibility = Visibility.Hidden;
                lblMoorOp.Visibility = Visibility.Hidden;
                comboMoorOp.Visibility = Visibility.Hidden;

            }

            else if (damageOb == "Damaged")
            {
                bntSection.SetValue(Grid.RowProperty, 17);
                lblothrs.Visibility = Visibility.Hidden;
                txtotherreason.Visibility = Visibility.Hidden;

                lbldmgob.Visibility = Visibility.Visible;
                comboDamageOb.Visibility = Visibility.Visible;
                lblMoorOp.Visibility = Visibility.Hidden;
                comboMoorOp.Visibility = Visibility.Hidden;
            }
            else
            {
                bntSection.SetValue(Grid.RowProperty, 14);
                lblothrs.Visibility = Visibility.Hidden;
                txtotherreason.Visibility = Visibility.Hidden;

                lbldmgob.Visibility = Visibility.Hidden;
                comboDamageOb.Visibility = Visibility.Hidden;
                lblMoorOp.Visibility = Visibility.Hidden;
                comboMoorOp.Visibility = Visibility.Hidden;
            }
        }

        private void comboDamageOb_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = comboDamageOb.Text.ToString();
            if (damageOb == "Mooring Operation")
            {
                bntSection.SetValue(Grid.RowProperty, 17);
                lblMoorOp.Visibility = Visibility.Visible;
                comboMoorOp.Visibility = Visibility.Visible;
            }
            else
            {
                bntSection.SetValue(Grid.RowProperty, 16);
                lblMoorOp.Visibility = Visibility.Hidden;
                comboMoorOp.Visibility = Visibility.Hidden;
            }
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

        private void comboDiameter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
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

        private void txtmaxyear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void txtmaxyear1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void rdb1_Click(object sender, RoutedEventArgs e)
        {
            AddRopeTailViewModel._AddRopeTail.IsRopeInstalled = "No";
            cnv1.Visibility = Visibility.Hidden;
            
            dpInsDate.Visibility = Visibility.Hidden;
        }

        private void rdb2_Click(object sender, RoutedEventArgs e)
        {
            AddRopeTailViewModel._AddRopeTail.IsRopeInstalled = "Yes";
            cnv1.Visibility = Visibility.Visible;

            dpInsDate.Visibility = Visibility.Visible;
        }

        private void DpRecDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var Frm = dpRecDate.Text;
                var datefrom = Convert.ToDateTime(Frm);

                var to = dpInsDate.Text;
                var dateto = Convert.ToDateTime(to);

                if (dateto < datefrom)
                {
                    MessageBox.Show("Received date can not be greater than Installed date!", "Joining Shackle", MessageBoxButton.OK, MessageBoxImage.Error);
                    AddRopeTailViewModel._ReceivedDate = RevDate;
                    dpRecDate.Text = RevDate.ToShortDateString();
                    // RaisePropertyChanged("InstalledDate");
                }
            }
            catch { }
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
                    MessageBox.Show("Installed date can not be less than Received date!", "Joining Shackle", MessageBoxButton.OK, MessageBoxImage.Error);
                    dpInsDate.Text = RevDate.ToShortDateString();
                    AddRopeTailViewModel._InstallDate = RevDate;
                }
            }
            catch { }
        }
    }
}
