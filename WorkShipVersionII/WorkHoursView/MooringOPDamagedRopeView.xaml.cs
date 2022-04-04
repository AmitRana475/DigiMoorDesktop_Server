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
    /// Interaction logic for MooringOPDamagedRopeView.xaml
    /// </summary>
    public partial class MooringOPDamagedRopeView : UserControl
    {
        private readonly ShipmentContaxt sc;

        public MooringOPDamagedRopeView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            sc = new ShipmentContaxt();

            comboAssrope.Text = "--Select--";
            cbRopeAction.Text = "--Select--";
            CBwasshift.Text = "--Select--";
            ComboBox.Text = "--Select--";
            cmbDmgReason.Text = "--Select--";
            CBIncident.Text = "--Select--";

            cmbcrpOut.Text = "--Select--";
            cmbcrpOut1.Text = "--Select--";
            cmbSplicedBy.Text = "--Select--";

            MooringOPDamagedRopeViewModel._MOPDamageRope.DamageObserved = "Mooring Operation";

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));

        }
        private void cbCropingReason_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                var rsn = cbCropingReason.SelectedItems.ToList();
                string reason = string.Empty;
                foreach (var item in rsn)
                {
                    if (item.Key != "All")
                    {
                        reason += item.Key + ",";
                    }
                }
                reason = reason.TrimEnd(',');
                txtCropingReason.Text = reason;

                MooringOPDamagedRopeViewModel._MOPDamageRope.ReasonofCropping = txtCropingReason.Text;

            }
            catch { }
        }

        private void cbRopeAction_DropDownClosed(object sender, EventArgs e)
        {


            SplicedGrid.Visibility = Visibility.Collapsed;
            CroppedGrid.Visibility = Visibility.Collapsed;
            DiscardedGrid.Visibility = Visibility.Collapsed;
            EndToEndGrid.Visibility = Visibility.Collapsed;

            // SaveEnable(cbRopeAction.Text);
            MooringOPDamagedRopeViewModel._MOPDamageRope.ActionAfterDamage = cbRopeAction.Text;

            if (cbRopeAction.Text == "Spliced")
            {
                SplicedGrid.Visibility = Visibility.Visible;
                MooringOPDamagedRopeViewModel._MOPDamageRope.IncidentAction = "Spliced";
            }

            if (cbRopeAction.Text == "Cropped")
            {
                CroppedGrid.Visibility = Visibility.Visible;
                MooringOPDamagedRopeViewModel._MOPDamageRope.IncidentAction = "Cropped";
            }
            if (cbRopeAction.Text == "Discarded")
            {
                DiscardedGrid.Visibility = Visibility.Visible;
                MooringOPDamagedRopeViewModel._MOPDamageRope.IncidentAction = "Discarded";
            }
            if (cbRopeAction.Text == "End-to-end")
            {
                EndToEndGrid.Visibility = Visibility.Visible;
                MooringOPDamagedRopeViewModel._MOPDamageRope.IncidentAction = "End-to-end";

                try
                {
                    if (MooringOPDamagedRopeViewModel._MOPDamageRope.RopeId != 0)
                    {

                        var chropid = MooringOPDamagedRopeViewModel._MOPDamageRope.RopeId;
                        var e2eidchek = sc.RopeEndtoEnd2.Where(x => x.RopeId == chropid).FirstOrDefault();
                        // if (sc.RopeEndtoEnd2.Count() > 0)
                        if (e2eidchek != null)
                        {

                            var e2eid = sc.RopeEndtoEnd2.Where(x => x.RopeId == chropid).Max(x => x.Id);
                            var E2ERecord = sc.RopeEndtoEnd2.Where(x => x.Id == e2eid).FirstOrDefault();
                            if (E2ERecord != null)
                            {
                                if (E2ERecord.CurrentOutboadEndinUse == true)
                                {
                                    // moorwinchrope.CurrentOutboadEndinUse = false; 
                                    MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = false;
                                    txtShiftedOutboardEnd.Text = "B";

                                }
                                else
                                {
                                    MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = true;
                                    txtShiftedOutboardEnd.Text = "A";
                                    // moorwinchrope.CurrentOutboadEndinUse = true;
                                }
                            }
                            else
                            {
                                var AssignRopeWinch = sc.AssignRopetoWinch.Where(x => x.RopeId == chropid).FirstOrDefault();
                                if (AssignRopeWinch != null)
                                {
                                    // MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = AssignRopeWinch.Outboard;

                                    if (AssignRopeWinch.Outboard == true)
                                    {

                                        MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = false;
                                        txtShiftedOutboardEnd.Text = "B";

                                    }
                                    else
                                    {
                                        MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = true;
                                        txtShiftedOutboardEnd.Text = "A";

                                    }
                                }
                            }
                        }
                        else
                        {
                            var AssignRopeWinch = sc.AssignRopetoWinch.Where(x => x.RopeId == chropid).FirstOrDefault();
                            if (AssignRopeWinch != null)
                            {
                                // MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = AssignRopeWinch.Outboard;

                                if (AssignRopeWinch.Outboard == true)
                                {

                                    MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = false;
                                    txtShiftedOutboardEnd.Text = "B";

                                }
                                else
                                {
                                    MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = true;
                                    txtShiftedOutboardEnd.Text = "A";

                                }
                            }
                            else
                            {
                                MessageBox.Show("Rope is not Assign to any Winch.", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                { throw ex; }

            }
            if (cbRopeAction.Text == "Shifted Winches")
            {
                // EndToEndGrid.Visibility = Visibility.Visible;
                //MessageBox.Show("Panding form");
            }

        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            // SaveEnable(ComboBox.Text);
            MooringOPDamagedRopeViewModel._MOPDamageRope.DamageLocation = ComboBox.Text;
        }

        private void cmbDmgReason_DropDownClosed(object sender, EventArgs e)
        {
            //SaveEnable(cmbDmgReason.Text);
            MooringOPDamagedRopeViewModel._MOPDamageRope.DamageReason = cmbDmgReason.Text;
        }

        private void comboAssrope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbRopeAction.Text == "End-to-end")
            {
                EndToEndGrid.Visibility = Visibility.Visible;
                MooringOPDamagedRopeViewModel._MOPDamageRope.IncidentAction = "End-to-end";

                try
                {
                    if (MooringOPDamagedRopeViewModel._MOPDamageRope.RopeId != 0)
                    {

                        var chropid = MooringOPDamagedRopeViewModel._MOPDamageRope.RopeId;
                        var e2eidchek = sc.RopeEndtoEnd2.Where(x => x.RopeId == chropid).FirstOrDefault();
                        // if (sc.RopeEndtoEnd2.Count() > 0)
                        if (e2eidchek != null)
                        {

                            var e2eid = sc.RopeEndtoEnd2.Where(x => x.RopeId == chropid).Max(x => x.Id);
                            var E2ERecord = sc.RopeEndtoEnd2.Where(x => x.Id == e2eid).FirstOrDefault();
                            if (E2ERecord != null)
                            {
                                if (E2ERecord.CurrentOutboadEndinUse == true)
                                {
                                    // moorwinchrope.CurrentOutboadEndinUse = false; 
                                    MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = false;
                                    txtShiftedOutboardEnd.Text = "B";

                                }
                                else
                                {
                                    MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = true;
                                    txtShiftedOutboardEnd.Text = "A";
                                    // moorwinchrope.CurrentOutboadEndinUse = true;
                                }
                            }
                            else
                            {
                                var AssignRopeWinch = sc.AssignRopetoWinch.Where(x => x.RopeId == chropid).FirstOrDefault();
                                if (AssignRopeWinch != null)
                                {
                                    // MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = AssignRopeWinch.Outboard;

                                    if (AssignRopeWinch.Outboard == true)
                                    {

                                        MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = false;
                                        txtShiftedOutboardEnd.Text = "B";

                                    }
                                    else
                                    {
                                        MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = true;
                                        txtShiftedOutboardEnd.Text = "A";

                                    }
                                }
                            }
                        }
                        else
                        {
                            var AssignRopeWinch = sc.AssignRopetoWinch.Where(x => x.RopeId == chropid).FirstOrDefault();
                            if (AssignRopeWinch != null)
                            {
                                // MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = AssignRopeWinch.Outboard;

                                if (AssignRopeWinch.Outboard == true)
                                {

                                    MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = false;
                                    txtShiftedOutboardEnd.Text = "B";

                                }
                                else
                                {
                                    MooringOPDamagedRopeViewModel._MOPDamageRope.CurrentOutboadEndinUse = true;
                                    txtShiftedOutboardEnd.Text = "A";

                                }
                            }
                            else
                            {
                                MessageBox.Show("Rope is not Assign to any Winch.", "Rope Damage", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                { throw ex; }

            }
        }

        private void CBIncident_DropDownClosed(object sender, EventArgs e)
        {
            // SaveEnable(CBIncident.Text);
            MooringOPDamagedRopeViewModel._MOPDamageRope.IncidentReport = CBIncident.Text;
        }

        private void DpSplicingDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            //MooringOPDamagedRopeViewModel._MOPDamageRope.SplicedDate = DpSplicingDate.Text;
        }

        private void txtSplicing_method_LostFocus(object sender, RoutedEventArgs e)
        {
            MooringOPDamagedRopeViewModel._MOPDamageRope.SplicedMethod = txtSplicing_method.Text;
        }

        private void ComboBox_DropDownClosed_1(object sender, EventArgs e)
        {
            MooringOPDamagedRopeViewModel._MOPDamageRope.SplicingDoneBy = cmbSplicedBy.Text;
        }

        private void DpSplicingDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // DpSplicingDate.Text = DpSplicingDate.SelectedDate.Value.Year.ToString() + "-" + DpSplicingDate.SelectedDate.Value.Month.ToString() + "-" + DpSplicingDate.SelectedDate.Value.Day.ToString();
            MooringOPDamagedRopeViewModel._MOPDamageRope.SplicedDate = Convert.ToDateTime(DpSplicingDate.Text);
        }

        private void DpCroppedDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // DpCroppedDate.Text = DpCroppedDate.SelectedDate.Value.Year.ToString() + "-" + DpCroppedDate.SelectedDate.Value.Month.ToString() + "-" + DpCroppedDate.SelectedDate.Value.Day.ToString();
            MooringOPDamagedRopeViewModel._MOPDamageRope.CroppedDate = Convert.ToDateTime(DpCroppedDate.Text);
        }

        private void ComboBox_DropDownClosed_2(object sender, EventArgs e)
        {
            MooringOPDamagedRopeViewModel._MOPDamageRope.CroppedOutboardEnd = cmbcrpOut.Text;
        }


        private void ComboBox_DropDownClosed_3(object sender, EventArgs e)
        {
            try
            {
                var routofS = lblReasOutOf.Text;
                if (routofS == "Completed running hours")
                {
                    lblDamageOb.Visibility = Visibility.Collapsed;
                    lblOtherR.Visibility = Visibility.Collapsed;
                    lblMoorOp.Visibility = Visibility.Collapsed;
                    MooringOPDamagedRopeViewModel._MOPDamageRope.ReasonOutofService = lblReasOutOf.Text;
                }
                if (routofS == "Damaged")
                {
                    lblDamageOb.Visibility = Visibility.Visible;
                    lblOtherR.Visibility = Visibility.Collapsed;
                    lblMoorOp.Visibility = Visibility.Collapsed;
                    MooringOPDamagedRopeViewModel._MOPDamageRope.ReasonOutofService = lblReasOutOf.Text;
                }
                if (routofS == "Other")
                {
                    lblDamageOb.Visibility = Visibility.Collapsed;
                    lblOtherR.Visibility = Visibility.Visible;
                    lblMoorOp.Visibility = Visibility.Collapsed;
                    MooringOPDamagedRopeViewModel._MOPDamageRope.ReasonOutofService = lblReasOutOf.Text;

                }
            }
            catch { }
        }

        private void cmbMoor_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                MooringOPDamagedRopeViewModel._MOPDamageRope.DamageObserved1 = cmbMoor.Text;
                //var routofS = cmbMoor.Text;
                //if (routofS == "Mooring Operation")
                //{
                //       lblMoorOp.Visibility = Visibility.Visible;
                //}
                //else
                //       lblMoorOp.Visibility = Visibility.Collapsed;
            }
            catch
            {

            }
        }

        private void txtcrpdrope_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                MooringOPDamagedRopeViewModel._MOPDamageRope.LengthofCroppedRope1 = Convert.ToDecimal(txtcrpdrope.Text);
            }
            catch { }
        }

        private void DpDiscardedDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                // DpDiscardedDate.Text = DpDiscardedDate.SelectedDate.Value.Year.ToString() + "-" + DpDiscardedDate.SelectedDate.Value.Month.ToString() + "-" + DpDiscardedDate.SelectedDate.Value.Day.ToString();
                MooringOPDamagedRopeViewModel._MOPDamageRope.DiscaredDate = Convert.ToDateTime(DpDiscardedDate.Text);
            }
            catch { }
        }

        private void txtotherreason_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                MooringOPDamagedRopeViewModel._MOPDamageRope.otherReason = txtotherreason.Text;
            }
            catch { }
        }

        private void ComboBox_DropDownClosed_4(object sender, EventArgs e)
        {
            try
            {
                MooringOPDamagedRopeViewModel._MOPDamageRope.MooringOperation = cmbMooringOp.Text;
            }
            catch { }
        }

        private void DpEndToEndDoneDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // DpEndToEndDoneDate.Text = DpEndToEndDoneDate.SelectedDate.Value.Year.ToString() + "-" + DpEndToEndDoneDate.SelectedDate.Value.Month.ToString() + "-" + DpEndToEndDoneDate.SelectedDate.Value.Day.ToString();
                MooringOPDamagedRopeViewModel._MOPDamageRope.EndtoEndDoneDate = Convert.ToDateTime(DpEndToEndDoneDate.Text);
            }
            catch { }
        }

        //private void AddRopeDamage_Click(object sender, RoutedEventArgs e)
        //{
        //       FieldBox2.Visibility = Visibility.Visible;
        //       GListBox1.Visibility = Visibility.Collapsed;
        //}

        //private void GoList_Click(object sender, RoutedEventArgs e)
        //{

        //              FieldBox2.Visibility = Visibility.Collapsed;
        //              GListBox1.Visibility = Visibility.Visible;


        //}

        private static void GoTOLIST()
        {
            // FieldBox2.Visibility = Visibility.Collapsed;
            // GListBox1.Visibility = Visibility.Visible;
        }


        private void TxtCropped_Length_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool check = sc.DecimalCheck1(Convert.ToDecimal(txtCropped_Length.Text));
                if (check == false)
                {
                    txtCropped_Length.Text = "";
                }
                else
                {
                    MooringOPDamagedRopeViewModel._MOPDamageRope.LengthofCroppedRope = Convert.ToDecimal(txtCropped_Length.Text);
                }
            }
            catch { }

            try
            {

                int ropeid = MooringOPDamagedRopeViewModel._MOPDamageRope.RopeId;
                SqlDataAdapter adp = new SqlDataAdapter("select Length from MooringRopeDetail where ID=" + ropeid + "", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                decimal ss = Convert.ToDecimal(dt.Rows[0][0]);


                decimal check2 = Convert.ToDecimal(txtCropped_Length.Text);

                if (check2 > ss)
                {
                                   MessageBox.Show("This Line Cropping length cannot be more than the line length of {" + ss + "} mtrs", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                                   txtCropped_Length.Text = "";
                }

                //if (check2 > 99.99m)
                //{
                //    MessageBox.Show("You can not Enter Max 2 Digit", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    txtCropped_Length.Text = "";
                //}

            }
            catch
            {
                //if (msgchk == 0)
                //{
                //    MessageBox.Show("Please Select Rope first before length", "Charcarter Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    msgchk = 1;
                txtCropped_Length.Text = "";
                return;
                //}
            }
        }

        private void comboAssrope_DropDownClosed(object sender, EventArgs e)
        {
            // SaveEnable(comboAssrope.Text);
        }

        //private void SaveEnable(string Text)
        //{
        //       if (Text != "--Select--")
        //       {
        //              btnSave.IsEnabled = true;
        //       }
        //       else
        //       {
        //              btnSave.IsEnabled = false;
        //       }
        //}

        private void txtcrpdrope_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void CBwasshift_DropDownClosed(object sender, EventArgs e)
        {
            // SaveEnable(CBwasshift.Text);
            if (CBwasshift.Text == "Yes")
            {
                MooringOPDamagedRopeViewModel._MOPDamageRope.IsCropped = "Yes";
                cnvs1.Visibility = Visibility.Visible;
                cnvs2.Visibility = Visibility.Visible;
                cnvs3.Visibility = Visibility.Visible;
            }
            else
            {
                MooringOPDamagedRopeViewModel._MOPDamageRope.IsCropped = "No";
                cnvs1.Visibility = Visibility.Hidden;
                cnvs2.Visibility = Visibility.Hidden;
                cnvs3.Visibility = Visibility.Hidden;
            }

        }

        private void cmbcrpOut1_DropDownClosed(object sender, EventArgs e)
        {
            MooringOPDamagedRopeViewModel._MOPDamageRope.CroppedOutboardEnd = cmbcrpOut1.Text;
        }

        private void cbCropingReason1_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                var rsn = cbCropingReason1.SelectedItems.ToList();
                string reason = string.Empty;
                foreach (var item in rsn)
                {
                    if (item.Key != "All")
                    {
                        reason += item.Key + ",";
                    }
                }
                reason = reason.TrimEnd(',');
                txtCropingReason1.Text = reason;

                MooringOPDamagedRopeViewModel._MOPDamageRope.ReasonofCropping = txtCropingReason1.Text;

            }
            catch { }
        }

        private void txtcrpdrope_TextChanged(object sender, TextChangedEventArgs e)
        {
                     try
                     {
                            bool check = sc.DecimalCheck1(Convert.ToDecimal(txtcrpdrope.Text));
                            if (check == false)
                            {
                                   txtcrpdrope.Text = "";
                            }
                            else
                            {
                                   MooringOPDamagedRopeViewModel._MOPDamageRope.LengthofCroppedRope1 = Convert.ToDecimal(txtcrpdrope.Text);
                            }
                     }
                     catch { }

                     try
                     {

                            int ropeid = MooringOPDamagedRopeViewModel._MOPDamageRope.RopeId;
                            SqlDataAdapter adp = new SqlDataAdapter("select Length from MooringRopeDetail where ID=" + ropeid + "", sc.con);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);

                            decimal ss = Convert.ToDecimal(dt.Rows[0][0]);


                            decimal check2 = Convert.ToDecimal(txtcrpdrope.Text);

                            if (check2 > ss)
                            {
                                   MessageBox.Show("This Line Cropping length cannot be more than the line length of {" + ss + "} mtrs", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                                   txtcrpdrope.Text = "";
                            }

                            //if (check2 > 99.99m)
                            //{
                            //    MessageBox.Show("You can not Enter Max 2 Digit", "Character Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                            //    txtCropped_Length.Text = "";
                            //}

                     }
                     catch
                     {
                            //if (msgchk == 0)
                            //{
                            //    MessageBox.Show("Please Select Rope first before length", "Charcarter Length", MessageBoxButton.OK, MessageBoxImage.Warning);
                            //    msgchk = 1;
                            txtcrpdrope.Text = "";
                            return;
                            //}
                     }

                   
              }

        private void AddRopeDamage_Click(object sender, RoutedEventArgs e)
        {
            comboAssrope.Text = "--Select--";
            cbRopeAction.Text = "--Select--";
            CBwasshift.Text = "--Select--";
            ComboBox.Text = "--Select--";
            cmbDmgReason.Text = "--Select--";
            CBIncident.Text = "--Select--";

            cmbcrpOut.Text = "--Select--";
            cmbcrpOut1.Text = "--Select--";
            cmbSplicedBy.Text = "--Select--";

            SplicedGrid.Visibility = Visibility.Collapsed;
            CroppedGrid.Visibility = Visibility.Collapsed;
            DiscardedGrid.Visibility = Visibility.Collapsed;
            EndToEndGrid.Visibility = Visibility.Collapsed;


            txtcrpdrope.Text = "";
            txtCropped_Length.Text = "";
            txtotherreason.Text = "";
            txtShiftedOutboardEnd.Text = "--";
            txtSplicing_method.Text = "";
            MooringOPDamagedRopeViewModel._MOPDamageRope.SplicedMethod = "";

        }
    }
}
