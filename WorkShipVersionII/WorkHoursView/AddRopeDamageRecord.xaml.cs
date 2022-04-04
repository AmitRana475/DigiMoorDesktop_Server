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
    /// Interaction logic for AddRopeDamageRecord.xaml
    /// </summary>
    public partial class AddRopeDamageRecord : UserControl
    {
        private readonly ShipmentContaxt sc;
        public AddRopeDamageRecord()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            sc = new ShipmentContaxt();
            ComboBox.SelectedIndex = 0;
            ComboBox2.SelectedIndex = 0;
         
        }

        private void comboRope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboRope.Text != "--Select--")
                {
                    btnSave.IsEnabled = true;
                }
                var data = sc.AssignRopetoWinch.Where(x => x.RopeId == AddRopeDamageRecordViewModel.sropetype.Id && x.IsActive == true).FirstOrDefault();

                if (data != null)
                {
                    int winchid = data.WinchId;
                    AddRopeDamageRecordViewModel._ropedamageClass.WinchId = winchid;

                    txtOutboard.Text = data.Outboard == true ? "A" : "B";
                    txtAssignedLocation.Text = sc.MooringWinch.Where(x => x.Id == data.WinchId && x.IsActive == true).Select(x => x.Location).SingleOrDefault();
                    //txtOutboardcurrent.Text = data.Outboard == true ? "B" : "A";
                    txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == data.WinchId && s.IsActive == true).Select(x => x.AssignedNumber).FirstOrDefault();
                }
                else
                {
                    AddRopeDamageRecordViewModel._ropedamageClass.WinchId = 0;
                    txtOutboard.Text = "Not Assigned";
                    txtAssignedLocation.Text = "Not Assigned";
                    txtAssignedWinch.Text = "Not Assigned";
                }
            }
            catch
            {
                txtOutboard.Text = "Not Assigned";
                txtAssignedLocation.Text = "Not Assigned";
                txtAssignedWinch.Text = "Not Assigned";
            }
        }

       

        private void cboBhp_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = cboBhp.Text.ToString();

            if(damageOb == "Mooring Operation")
            {
                bntSection.SetValue(Grid.RowProperty, 23);
                lblmoorop.Visibility = Visibility.Visible;
                cboBhp2.Visibility = Visibility.Visible;
            }
            else
            {
                bntSection.SetValue(Grid.RowProperty, 21);
                lblmoorop.Visibility = Visibility.Hidden;
                cboBhp2.Visibility = Visibility.Hidden;

            }
        }

        private void comboRope_DropDownClosed(object sender, EventArgs e)
        {
            if (comboRope.Text != "--Select--")
            {
                btnSave.IsEnabled = true;
            }
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            AddRopeDamageRecordViewModel._ropedamageClass.DamageLocation = ComboBox.Text;
        }

        private void ComboBox2_DropDownClosed(object sender, EventArgs e)
        {
            AddRopeDamageRecordViewModel._ropedamageClass.DamageReason = ComboBox2.Text;
        }

        private void DpRecDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var Frm = dpRecDate.Text;
                var datefrom = Convert.ToDateTime(Frm);

                var to = GetRopeReceivedDate();
                var dateto = Convert.ToDateTime(to);

                if (dateto > datefrom)
                {
                    MessageBox.Show("Damage Date can not be less than Line Received Date!", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Error);
                    dpRecDate.Text = to;
                }
            }
            catch { }
        }

        private string GetRopeReceivedDate()
        {
            string dtt = "";
            try
            {
               // int value = Convert.ToInt32( comboRope.SelectedItem);

                int value1 = Convert.ToInt32(comboRope.SelectedValue);
                if (value1 == 0)
                {
                    MessageBox.Show("Please Choose Line first !", "Line Damage", MessageBoxButton.OK, MessageBoxImage.Error);
                    var Frm = Convert.ToDateTime(DateTime.Now);
                    dpRecDate.Text = Frm.ToString();
                }
                else
                {

                    SqlDataAdapter adp = new SqlDataAdapter("select ReceivedDate from mooringropedetail where ID=" + comboRope.SelectedValue + " ", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    dtt = dt.Rows[0][0].ToString();
                    return dtt;

                }
                return dtt;
            }
            catch { return dtt; }
        }
    }
}
