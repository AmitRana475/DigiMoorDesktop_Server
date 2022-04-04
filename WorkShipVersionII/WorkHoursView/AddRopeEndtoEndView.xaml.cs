using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkShipVersionII.WorkHoursViewModel;
using System.Windows.Markup;
using System.Globalization;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for AddRopeEndtoEndView.xaml
    /// </summary>
    public partial class AddRopeEndtoEndView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public AddRopeEndtoEndView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            sc = new ShipmentContaxt();

        }
        //public AddRopeEndtoEndView(RopeEndtoEndClass edeps)
        //{
        //    InitializeComponent();
        //    sc = new ShipmentContaxt();

        //    txtAssignedLocation.Text = edeps.AssignedLocation.ToString();
           

        //}
 

     

        private void comboRank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try { 
            if(comboRank.Text != "--Select--")
            {
                btnSave.IsEnabled = true;
            }
            var data = sc.AssignRopetoWinch.Where(x => x.RopeId == AddRopeEndtoEndViewModel.sropetype.Id && x.IsActive == true).FirstOrDefault();


                if (data != null)
                {
                    int winchid = data.WinchId;
                    AddRopeEndtoEndViewModel._ropeEndtoEndClass.WinchId = winchid;



                    txtOutboard.Text = data.Outboard == true ? "A" : "B";
                    txtAssignedLocation.Text = sc.MooringWinch.Where(x => x.Id == data.WinchId && x.IsActive == true).Select(x => x.Location).SingleOrDefault();
                    txtOutboardcurrent.Text = data.Outboard == true ? "B" : "A";
                    txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == data.WinchId && s.IsActive == true).Select(x => x.AssignedNumber).FirstOrDefault();
                    // RopeEndToEnd.AssignedWinch = 
                }
                else
                {
                    AddRopeEndtoEndViewModel._ropeEndtoEndClass.WinchId = 0;
                }
            }
            catch { }

        }

        private void CBwasshift_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                var shift = CBwasshift.Text;
                if (shift == "Yes")
                {
                    bntSection.SetValue(Grid.RowProperty, 23);
                    //RowDefinition gridRow1 = new RowDefinition();
                    //gridRow1.Height = new GridLength(20);
                    //RowDefinition gridRow2 = new RowDefinition();
                    //gridRow2.Height = new GridLength(20);
                    //RowDefinition gridRow3 = new RowDefinition();
                    //gridRow3.Height = new GridLength(20);
                    //endtoEndGrid.RowDefinitions.Add(gridRow1);
                    //endtoEndGrid.RowDefinitions.Add(gridRow2);
                    //endtoEndGrid.RowDefinitions.Add(gridRow3);

                    lblOut.Visibility = Visibility.Visible;
                    lblAssigndt.Visibility = Visibility.Visible;
                    lblAssigntownch.Visibility = Visibility.Visible;
                    rdbOut.Visibility = Visibility.Visible;
                    dpAssinDate.Visibility = Visibility.Visible;
                    comboAssrope.Visibility = Visibility.Visible;
                    AddRopeEndtoEndViewModel._ropeEndtoEndClass.WasRopeShifted = "Yes";

                }
                if (shift == "No")
                {
                    bntSection.SetValue(Grid.RowProperty, 17);
                    lblOut.Visibility = Visibility.Hidden;
                    lblAssigndt.Visibility = Visibility.Hidden;
                    lblAssigntownch.Visibility = Visibility.Hidden;
                    rdbOut.Visibility = Visibility.Hidden;
                    dpAssinDate.Visibility = Visibility.Hidden;
                    comboAssrope.Visibility = Visibility.Hidden;
                    AddRopeEndtoEndViewModel._ropeEndtoEndClass.WasRopeShifted = "No";

                }
            }
            catch { }
        }

        private void comboRank_DropDownClosed(object sender, EventArgs e)
        {
            if (comboRank.Text != "--Select--")
            {
                btnSave.IsEnabled = true;
            }

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
                    MessageBox.Show("EndtoEnd Date can not be less than Line Installed Date!", "Line EndtoEnd", MessageBoxButton.OK, MessageBoxImage.Error);
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

                int value1 = Convert.ToInt32(comboRank.SelectedValue);

                if (value1 == 0)
                {
                    MessageBox.Show("Please Choose Line first !", "Line EndtoEnd", MessageBoxButton.OK, MessageBoxImage.Error);
                    var Frm = Convert.ToDateTime(DateTime.Now);
                    dpRecDate.Text = Frm.ToString(); 
                }
                else
                {

                    SqlDataAdapter adp1 = new SqlDataAdapter("select top(1) EndtoEndDoneDate from RopeEndtoEnd2 where RopeId=" + comboRank.SelectedValue + " order by id desc", sc.con);
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        dtt = dt1.Rows[0][0].ToString();
                        return dtt;
                    }
                    else
                    {


                        SqlDataAdapter adp = new SqlDataAdapter("select installeddate from mooringropedetail where ID=" + comboRank.SelectedValue + " ", sc.con);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);

                        dtt = dt.Rows[0][0].ToString();
                        return dtt;
                    }
                }

                return dtt;
            }
            catch { return dtt; }
        }

        //    private void btnSave_Click(object sender, RoutedEventArgs e)
        //    {
        //        RopeEndtoEndClass etoe = new RopeEndtoEndClass();

        //        etoe.CertificateNumber = comboRank.Text;
        //        etoe.OutboardEndinUse = txtOutboard.Text == "A" ? true : false;
        //        etoe.AssignedWinch = txtAssignedWinch.Text;
        //        etoe.AssignedLocation = txtAssignedLocation.Text;
        //        etoe.EndtoEndDoneDate = AddRopeEndtoEndViewModel._ReceivedDate;
        //        etoe.CurrentOutboadEndinUse = txtOutboardcurrent.Text == "A" ? true : false;
        //        //etoe.CertificateNumber = comboRank.SelectedValuePath;
        //        etoe.CreatedDate = DateTime.Now;
        //        etoe.CreatedBy = "Admin";
        //        etoe.ModifiedDate = DateTime.Now;
        //        etoe.ModifiedBy = "Admin";
        //        etoe.IsActive = true;


        //        sc.RopeEndtoEndTable.Add(etoe);
        //        sc.SaveChanges();

        //}
    }
}
