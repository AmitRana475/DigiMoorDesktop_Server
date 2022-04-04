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
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursViewModel;
using System.Data;
using System.Data.SqlClient;
using DataBuildingLayer;
using System.Text.RegularExpressions;
using System.Windows.Markup;
using System.Globalization;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for AddLooseEquipmentDetailsView.xaml
    /// </summary>
    public partial class AddLooseEquipmentDetailsView : UserControl
    {
        private readonly ShipmentContaxt sc;
        static DateTime RevDate = DateTime.Now.Date;
        static DateTime InsDate = DateTime.Now.Date;
        public AddLooseEquipmentDetailsView()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();

            if (AddLooseEquipmentViewModel._ReceivedDate != null)
                RevDate = Convert.ToDateTime(AddLooseEquipmentViewModel._ReceivedDate);

            if (AddLooseEquipmentViewModel._InstallDate != null)
                InsDate = Convert.ToDateTime(AddLooseEquipmentViewModel._InstallDate);


        }

        private void comboLooseEtype_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string damageOb = comboLooseEtype.Text.ToString();

                SqlDataAdapter adp = new SqlDataAdapter("select id from LooseEType where LooseEquipmentType='" + damageOb + "'", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                Int32 id = Convert.ToInt32(dt.Rows[0][0]);

                if (damageOb == "Rope Tail" || damageOb == "FireWire" || damageOb == "Messenger Rope" || damageOb == "Rope Stopper" || damageOb == "Towing Rope" || damageOb == "Suez Rope" || damageOb == "Pennant Rope" || damageOb == "Grommet Rope")
                {
                    AddRopeTailViewModel vm = new AddRopeTailViewModel(id);
                    ChildWindowManager.Instance.ShowChildWindow(new AddRopeTailView() { DataContext = vm });
                }
                else if (damageOb == "Chain Stopper")
                {
                    AddChainStopperViewModel vm = new AddChainStopperViewModel(id);
                    ChildWindowManager.Instance.ShowChildWindow(new AddChainStopperView() { DataContext = vm });

                }
            }
            catch { }
        }

        private void comboReOutofSer_DropDownClosed(object sender, EventArgs e)
        {
        
            string damageOb = comboReOutofSer.Text.ToString();
            if(damageOb == "Other")
            {
                bntSection.SetValue(Grid.RowProperty, 25);
                lblOthR.Visibility = Visibility.Visible;
                txtotherR.Visibility = Visibility.Visible;

                lbldmgOb.Visibility = Visibility.Hidden;
                cboDmgOb.Visibility = Visibility.Hidden;
                lblmoorop.Visibility = Visibility.Hidden;
                cboDmgOb.Visibility = Visibility.Hidden;

            }

           else if (damageOb == "Damaged")
            {
                bntSection.SetValue(Grid.RowProperty, 25);
                lblOthR.Visibility = Visibility.Hidden;
                txtotherR.Visibility = Visibility.Hidden;

                lbldmgOb.Visibility = Visibility.Visible;
                cboDmgOb.Visibility = Visibility.Visible;
                lblmoorop.Visibility = Visibility.Hidden;
                cboMoorOp.Visibility = Visibility.Hidden;
            }
            else
            {
                bntSection.SetValue(Grid.RowProperty, 21);
                lblOthR.Visibility = Visibility.Hidden;
                txtotherR.Visibility = Visibility.Hidden;

                lbldmgOb.Visibility = Visibility.Hidden;
                cboDmgOb.Visibility = Visibility.Hidden;
                lblmoorop.Visibility = Visibility.Hidden;
                cboMoorOp.Visibility = Visibility.Hidden;
            }
        }

        private void cboDmgOb_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = cboDmgOb.Text.ToString();
            if (damageOb == "Mooring Operation")
            {
                bntSection.SetValue(Grid.RowProperty, 25);
                lblmoorop.Visibility = Visibility.Visible;
                cboMoorOp.Visibility = Visibility.Visible;
            }
           else
            {
                bntSection.SetValue(Grid.RowProperty, 23);
                lblmoorop.Visibility = Visibility.Hidden;
                cboMoorOp.Visibility = Visibility.Hidden;
            }
        }

        private void txtMBL_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void rdb1_Click(object sender, RoutedEventArgs e)
        {
            AddLooseEquipmentViewModel._AddJoiningShackleE.IsRopeInstalled = "No";
            cnv1.Visibility = Visibility.Hidden;
            //cnv2.Visibility = Visibility.Hidden;
            dpInsDate.Visibility = Visibility.Hidden;
        }

        private void rdb2_Click(object sender, RoutedEventArgs e)
        {
            AddLooseEquipmentViewModel._AddJoiningShackleE.IsRopeInstalled = "Yes";
            cnv1.Visibility = Visibility.Visible;
            //cnv2.Visibility = Visibility.Visible;
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
                    AddLooseEquipmentViewModel._ReceivedDate = RevDate;
                    dpRecDate.Text = RevDate.ToShortDateString();
                   // dpRecDate.Text = to;
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
                    AddLooseEquipmentViewModel._InstallDate = RevDate;
                    dpInsDate.Text = RevDate.ToShortDateString();
                    //dpInsDate.Text = DateTime.Now.ToShortDateString();
                }
            }
            catch { }
        }
    }
}
