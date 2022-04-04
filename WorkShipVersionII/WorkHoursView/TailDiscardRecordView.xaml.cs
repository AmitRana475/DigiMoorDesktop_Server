﻿using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
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
namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for RopeDiscardRecordView.xaml
    /// </summary>
    public partial class TailDiscardRecordView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public TailDiscardRecordView()
        {
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

                try
                {
                    int rpid = Convert.ToInt32(comboRope.SelectedValue);
                    int opId = sc.MOUsedWinchTbl.Where(x => x.RopeId == rpid).Select(item => item.OperationID).Distinct().Count();
                    txtnumberofop.Text = opId.ToString();
                }
                catch { txtnumberofop.Text = "0"; }

                txtRuninghrs.Text = sc.MooringWinchRope.Where(x => x.Id == TailDiscardRecordViewModel.sropetype.Id && x.DeleteStatus == false).Select(x => x.CurrentRunningHours).SingleOrDefault().ToString();
                //var data5 = sc.AssignRopetoWinch.Where(x => x.RopeId == TailDiscardRecordViewModel.sropetype.Id && x.IsActive == true).FirstOrDefault();
                //if (data5 != null)
                //{
                //    MessageBox.Show("This tail is currently assigned to a Winch and is in active list. Please go to Assign to Winch option under Mooring Tail menu and 'Shift to Inactive' list.", "Rope Discard", MessageBoxButton.OK, MessageBoxImage.Warning);
                //}
                var data = sc.AssignRopetoWinch.Where(x => x.RopeId == TailDiscardRecordViewModel.sropetype.Id && x.IsActive==true).FirstOrDefault();

                if (data != null)
                {
                    //int winchid = data.WinchId;
                    //TailDiscardRecordViewModel._ropediscardClass.OutOfService_WinchId = winchid;

                    txtOutboard.Text = data.Outboard == true ? "A" : "B";
                    txtAssignedLocation.Text = sc.MooringWinch.Where(x => x.Id == data.WinchId && x.IsActive == true).Select(x => x.Location).SingleOrDefault();
                    //txtOutboardcurrent.Text = data.Outboard == true ? "B" : "A";
                    txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == data.WinchId && s.IsActive == true).Select(x => x.AssignedNumber).FirstOrDefault();

                }
                else
                {
                   // TailDiscardRecordViewModel._ropediscardClass.OutOfService_WinchId = 0;
                    txtAssignedLocation.Text = "Not Assigned";
                    txtAssignedWinch.Text = "Not Assigned";
                    txtAssignedWinch.Text = "Not Assigned";

                }
                //int opId = sc.MOUsedWinchTbl.Select(item => item.OperationID).Distinct().Count();
                //txtnumberofop.Text = opId.ToString();
              
            }
            catch
            {
                txtAssignedLocation.Text = "Not Assigned";
                txtAssignedWinch.Text = "Not Assigned";
                txtAssignedWinch.Text = "Not Assigned";
               // txtRuninghrs.Text = "Not Assigned";
            }
            }



        private void cboBhp_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = cboBhp.Text.ToString();

            if (damageOb == "Mooring Operation")
            {
                bntSection.SetValue(Grid.RowProperty, 21);
                lblmoorop.Visibility = Visibility.Visible;
                cboBhp2.Visibility = Visibility.Visible;
            }
            else
            {
                bntSection.SetValue(Grid.RowProperty, 19);
                lblmoorop.Visibility = Visibility.Hidden;
                cboBhp2.Visibility = Visibility.Hidden;

            }
        }

        private void comboReOutofSer_DropDownClosed(object sender, EventArgs e)
        {
            string damageOb = comboReOutofSer.Text.ToString();

            if (damageOb == "Other")
            {
                lblothrs.Visibility = Visibility.Visible;
                txtotherreason.Visibility = Visibility.Visible;
                dmgobs.Visibility = Visibility.Hidden;
                cboBhp.Visibility = Visibility.Hidden;
                lblmoorop.Visibility = Visibility.Hidden;
                cboBhp2.Visibility = Visibility.Hidden;
            }
            else if (damageOb == "Damaged")
            {
                dmgobs.Visibility = Visibility.Visible;
                cboBhp.Visibility = Visibility.Visible;
                cboBhp.SelectedIndex = 1;
                bntSection.SetValue(Grid.RowProperty, 21);
                lblmoorop.Visibility = Visibility.Hidden;
                cboBhp2.Visibility = Visibility.Hidden;
                lblothrs.Visibility = Visibility.Hidden;
                txtotherreason.Visibility = Visibility.Hidden;


            }
            else
            {
                dmgobs.Visibility = Visibility.Hidden;
                cboBhp.Visibility = Visibility.Hidden;
                //cboBhp.SelectedIndex = 1;
                //bntSection.SetValue(Grid.RowProperty, 21);
                lblmoorop.Visibility = Visibility.Hidden;
                cboBhp2.Visibility = Visibility.Hidden;
                lblothrs.Visibility = Visibility.Hidden;
                txtotherreason.Visibility = Visibility.Hidden;
            }
        }

        private void comboRope_DropDownClosed(object sender, EventArgs e)
        {
            if (comboRope.Text != "--Select--")
            {
                int ropeid = Convert.ToInt32(comboRope.SelectedValue);
                var data = sc.AssignRopetoWinch.Where(x => x.RopeId == ropeid && x.IsActive == true && x.RopeTail==1).FirstOrDefault();
                if (data != null)
                {
                    MessageBox.Show("This rope is currently assigned to a Winch and is in active list. Please go to Assign to Winch option under Mooring Rope menu and 'Shift to Inactive' list.", "Rope Discard", MessageBoxButton.OK, MessageBoxImage.Warning);
                    btnSave.IsEnabled = false;
                }
                else
                {


                    btnSave.IsEnabled = true;
                }
            }
        }

        private void DpOutofSer_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                var Frm = dpOutofSer.Text;
                var datefrom = Convert.ToDateTime(Frm);

                var to = GetRopeReceivedDate();
                var dateto = Convert.ToDateTime(to);

                if (dateto > datefrom)
                {
                    MessageBox.Show("Discard Date can not be less than Rope Received Date!", "Rope Discard", MessageBoxButton.OK, MessageBoxImage.Error);
                    dpOutofSer.Text = to;
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
