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
    /// Interaction logic for AddRopeSplicing.xaml
    /// </summary>
    public partial class AddTailSplicing : UserControl
    {
        private readonly ShipmentContaxt sc;
        public AddTailSplicing()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            sc = new ShipmentContaxt();
        }
        private void cbMonth_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                var rsn = cbMonth.SelectedItems.ToList();
                string reason = string.Empty;
                foreach (var item in rsn)
                {
                    if (item.Key != "All")
                    {
                        reason += item.Key + ",";
                    }
                }
                reason = reason.TrimEnd(',');
                txtrsn.Text = reason;
            }
            catch { }
        }
        private void comboRope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var data = sc.AssignRopetoWinch.Where(x => x.RopeId == AddTailSplicingViewModel.sropetype.Id && x.IsActive == true).FirstOrDefault();

                if (data != null)
                {
                    int winchid = data.WinchId;

                    AddTailSplicingViewModel._ropesplicingClass.WinchId = winchid;
                    txtOutboard.Text = data.Outboard == true ? "A" : "B";
                    txtAssignedLocation.Text = sc.MooringWinch.Where(x => x.Id == data.WinchId && x.IsActive == true).Select(x => x.Location).SingleOrDefault();
                    //txtOutboardcurrent.Text = data.Outboard == true ? "B" : "A";
                    txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == data.WinchId && s.IsActive == true).Select(x => x.AssignedNumber).FirstOrDefault();
                }else
                {
                    AddTailSplicingViewModel._ropesplicingClass.WinchId = 0;
                    txtOutboard.Text = "Not Assigned";
                    txtAssignedLocation.Text = "Not Assigned";
                    txtAssignedWinch.Text = "Not Assigned";
                }
            }
            catch
            {
                txtAssignedLocation.Text = "Not Assigned";
                txtAssignedWinch.Text = "Not Assigned";
            }
        }

        private void txtCroppedLength_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void comboRope_DropDownClosed(object sender, EventArgs e)
        {
            if (comboRope.Text != "--Select--")
            {
                btnSave.IsEnabled = true;
            }
        }

        private void txtCroppedLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var ss = txtCroppedLength.Text;
                if (ss == " ")
                {
                    txtCroppedLength.Text = "";
                }


                int ropeid = AddTailSplicingViewModel.sropetype.Id;
                SqlDataAdapter adp = new SqlDataAdapter("select Length from MooringRopeDetail where ID=" + ropeid + "", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                decimal ss1 = Convert.ToDecimal(dt.Rows[0][0]);


                decimal check = Convert.ToDecimal(txtCroppedLength.Text);

                if (check > ss1)
                {
                    MessageBox.Show("You cannot crop the rope tail beyond its total length of " + ss1 + " mtrs  !", "Rope Tail Splicing", MessageBoxButton.OK, MessageBoxImage.Warning);
                   // MessageBox.Show("This ropetail Cropping length cannot be more than the ropetail length of {" + ss1 + "} mtrs", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                                   txtCroppedLength.Text = "";
                    return;
                }

                //int check = Convert.ToInt32(txtCroppedLength.Text);
                if (check > 999)
                {
                    MessageBox.Show("You can not Enter Max 3 Digit", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtCroppedLength.Text = "";
                    return;
                }
                SqlDataAdapter adp1 = new SqlDataAdapter("select SUM(lengthofcroppedRope) as lengthofcroppedRope from RopeCropping where  RopeId= " + ropeid + "", sc.con);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);

                if (dt1.Rows.Count > 0 && dt1.Rows[0][0] != DBNull.Value)
                {
                    decimal totalsum = Convert.ToDecimal(dt1.Rows[0][0]);

                    decimal sumtotal = Convert.ToDecimal(dt1.Rows[0][0]) + check;

                    if (sumtotal > ss1)
                    {
                       // You cannot crop the rope beyond its total length of { total length} mtrs
                        MessageBox.Show("You cannot crop the rope tail beyond its total length of " + ss1 + " mtrs  !", "Rope Tail Splicing", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtCroppedLength.Text = "";
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                return;
                //txtCroppedLength.Text = ""; 
            }
        }

        private void CBwasshift_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                var shift = CBwasshift.Text;
                if (shift == "Yes")
                {
                    //firstcanvas.Visibility = Visibility.Visible;
                    first.Visibility = Visibility.Visible;
                    //second.Visibility = Visibility.Visible;
                    //third.Visibility = Visibility.Visible;
                    forth.Visibility = Visibility.Visible;
                    //txtCroppedLength.Visibility = Visibility.Visible;
                    cbMonth.Visibility = Visibility.Visible;
                    //totalcrpd.Visibility = Visibility.Visible;
                    //btnSection.SetValue(Grid.RowProperty, 25);
                    txtCroppedLength.Visibility = Visibility.Visible;

                    AddTailSplicingViewModel._ropesplicingClass.IsCropped = "Yes";

                }
                if (shift == "No")
                {
                    first.Visibility = Visibility.Hidden;
                    //second.Visibility = Visibility.Visible;
                    //third.Visibility = Visibility.Visible;
                    forth.Visibility = Visibility.Hidden;
                    //txtCroppedLength.Visibility = Visibility.Visible;
                    cbMonth.Visibility = Visibility.Hidden;
                    //totalcrpd.Visibility = Visibility.Visible;
                    //btnSection.SetValue(Grid.RowProperty, 25);
                    txtCroppedLength.Visibility = Visibility.Hidden;
                    AddTailSplicingViewModel._ropesplicingClass.IsCropped = "No";

                }
            }
            catch { }
        }

        private void Dpdateofsplicing_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var Frm = dpdateofsplicing.Text;
                var datefrom = Convert.ToDateTime(Frm);

                var to = GetRopeReceivedDate();
                var dateto = Convert.ToDateTime(to);

                if (dateto > datefrom)
                {
                    MessageBox.Show("Splicing Date can not be less than Rope Received Date!", "Rope Splicing", MessageBoxButton.OK, MessageBoxImage.Error);
                    dpdateofsplicing.Text = to;
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

                SqlDataAdapter adp = new SqlDataAdapter("select ReceivedDate from mooringropedetail where ID=" + comboRope.SelectedValue + " ", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                dtt = dt.Rows[0][0].ToString();
                return dtt;


            }
            catch { return dtt; }
        }
    }
}
