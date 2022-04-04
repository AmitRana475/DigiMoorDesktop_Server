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
    /// Interaction logic for AddRopeCropping.xaml
    /// </summary>
    public partial class AddTailCropping : UserControl
    {
        private readonly ShipmentContaxt sc;
        public AddTailCropping()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            sc = new ShipmentContaxt();


        }
        private void comboRope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboRope.Text != "--Select--")
                {
                    btnSave.IsEnabled = true;
                }
                var data = sc.AssignRopetoWinch.Where(x => x.RopeId == AddTailCroppingViewModel.sropetype.Id && x.IsActive == true).FirstOrDefault();

                if (data != null)
                {
                    int winchid = data.WinchId;
                    AddTailCroppingViewModel._ropecroppingClass.WinchId = winchid;

                    txtOutboard.Text = data.Outboard == true ? "A" : "B";
                    txtAssignedLocation.Text = sc.MooringWinch.Where(x => x.Id == data.WinchId && x.IsActive == true).Select(x => x.Location).SingleOrDefault();
                    //txtOutboardcurrent.Text = data.Outboard == true ? "B" : "A";
                    txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == data.WinchId && s.IsActive == true).Select(x => x.AssignedNumber).FirstOrDefault();
                }
                else
                {
                    AddTailCroppingViewModel._ropecroppingClass.WinchId = 0;
                    txtOutboard.Text = "Not Assigned";
                    txtAssignedLocation.Text = "Not Assigned";
                    txtAssignedWinch.Text = "Not Assigned";
                }

                var crplength = sc.RopeCropping.Where(x => x.RopeId == AddTailCroppingViewModel.sropetype.Id && x.IsActive == true).Select(x => x.LengthofCroppedRope).Sum();

                if (crplength != null)
                {
                    txtTotalCrplen.Text = "Total Cropped Length :- " + crplength + " (mtrs)";
                }
                else
                {
                    txtTotalCrplen.Text = "";
                }

            }
            catch
            {

                txtAssignedLocation.Text = "Not Assigned";
                txtAssignedWinch.Text = "Not Assigned";
            }
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

        private void txtcrpdrope_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void txtcrpdrope_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                int ropeid = AddTailCroppingViewModel.sropetype.Id;
                SqlDataAdapter adp = new SqlDataAdapter("select Length from MooringRopeDetail where ID=" + ropeid + "", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                decimal ss = Convert.ToDecimal(dt.Rows[0][0]);


                decimal check = Convert.ToDecimal(txtcrpdrope.Text);

                if (check > ss)
                {
                    MessageBox.Show("You cannot crop the rope tail beyond its total length of " + ss + " mtrs  !", "Rope Tail Cropping", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //MessageBox.Show("This rope tail Cropping length cannot be more than the rope tail length of {" + ss + "} mtrs", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                                   txtcrpdrope.Text = "";
                    return;
                }

                if (check > 999)
                {
                    MessageBox.Show("You can not Enter Max 3 Digit", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtcrpdrope.Text = "";
                    return;
                }

                SqlDataAdapter adp1 = new SqlDataAdapter("select SUM(lengthofcroppedRope) as lengthofcroppedRope from RopeCropping where  RopeId= " + ropeid + "", sc.con);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);

                if (dt1.Rows.Count > 0 && dt1.Rows[0][0] != DBNull.Value)
                {
                    decimal totalsum = Convert.ToDecimal(dt1.Rows[0][0]);

                    decimal sumtotal = Convert.ToDecimal(dt1.Rows[0][0]) + check;

                    if (sumtotal > ss)
                    {
                        MessageBox.Show("You cannot crop the rope tail beyond its total length of " + ss + " mtrs  !", "Rope Tail Cropping", MessageBoxButton.OK, MessageBoxImage.Warning);
                        //MessageBox.Show("You are not cropped rope tail more than " + ss + " !", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtcrpdrope.Text = "";
                        return;
                    }
                }

            }
            catch
            {
                //if (msgchk == 0)
                //{
                //    MessageBox.Show("Please Select Rope first before length", "Charcarter Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    msgchk = 1;
                //txtcrpdrope.Text = "";
                return;
                //}
            }
        }

        private void Dpdateofshifting_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var Frm = dpdateofshifting.Text;
                var datefrom = Convert.ToDateTime(Frm);

                var to = GetRopeReceivedDate();
                var dateto = Convert.ToDateTime(to);

                if (dateto > datefrom)
                {
                    MessageBox.Show("Cropping Date can not be less than Rope Received Date!", "Rope Cropping", MessageBoxButton.OK, MessageBoxImage.Error);
                    dpdateofshifting.Text = to;
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
