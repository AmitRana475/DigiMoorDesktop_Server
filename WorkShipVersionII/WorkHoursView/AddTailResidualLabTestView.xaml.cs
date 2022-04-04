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
    /// Interaction logic for AddResidualLabTestView.xaml
    /// </summary>
    public partial class AddTailResidualLabTestView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public AddTailResidualLabTestView()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
        }
        private string instdt;
        private void comboRope_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int ropeid = Convert.ToInt32(comboRope.SelectedValue);
                SqlDataAdapter adp = new SqlDataAdapter("ResidualTestD", sc.con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@RopeId", ropeid);
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", 1);
                DataSet dt = new DataSet();
                adp.Fill(dt);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    btnSave.IsEnabled = true;

                    txttestdate.Text = "";
                    int ropetypeid = Convert.ToInt32(dt.Tables[0].Rows[0]["ropetypeid"]);
                    int manuFid = Convert.ToInt32(dt.Tables[0].Rows[0]["manufacturerid"]);
                    //DateTime ss = Convert.ToDateTime(dt.Tables[0].Rows[0]["InstalledDate"] == DBNull.Value ? null : dt.Tables[0].Rows[0]["InstalledDate"]);

                    string ss = dt.Tables[0].Rows[0]["InstalledDate"] == DBNull.Value ? null : dt.Tables[0].Rows[0]["InstalledDate"].ToString();
                    int crntRhrs = Convert.ToInt32(dt.Tables[0].Rows[0]["CurrentRunningHours"] == DBNull.Value ? 0 : dt.Tables[0].Rows[0]["CurrentRunningHours"]);
                    string RopeType = dt.Tables[0].Rows[0]["ropetype"].ToString();
                    string ManuFname = dt.Tables[0].Rows[0]["name"].ToString();

                    txtropetype.Text = RopeType;
                    txtmanfname.Text = ManuFname;
                    if (ss == null)
                    {
                        instdt = DateTime.Now.ToString();
                    }
                    else
                    {

                        instdt = ss.ToString();
                    }
                    //txttestdate.Text =  ss.ToString();
                    txtrunninghrs.Text = crntRhrs.ToString();

                    AddTailResidualLabTestViewModel._AddResidualLabTest.RopeId = ropeid;
                    AddTailResidualLabTestViewModel._AddResidualLabTest.RopeTypeId = ropetypeid;
                    //AddTailResidualLabTestViewModel._AddResidualLabTest.ServiceTestDate = ss;
                    AddTailResidualLabTestViewModel._AddResidualLabTest.ManufacturerId = manuFid;
                    AddTailResidualLabTestViewModel._AddResidualLabTest.RunningHours = crntRhrs;

                    diffchek();



                }
                if (dt.Tables[1].Rows.Count > 0)
                {
                    string location = dt.Tables[1].Rows[0]["location"].ToString();
                    txtlocation.Text = location;
                    AddTailResidualLabTestViewModel._AddResidualLabTest.Location = location;
                }
            }
            catch { }
        }

        private void txttestresult_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void txttestresult_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    bool check = sc.DecimalCheck(Convert.ToInt32(txttestresult.Text));
            //    if (check == false)
            //    {
            //        txttestresult.Text = "";
            //    }
            //}
            //catch { }

            try
            {
                decimal check = Convert.ToDecimal(txttestresult.Text);

                if (check > 100)
                {
                    MessageBox.Show("Values can not greater then 100%", "Character length", MessageBoxButton.OK, MessageBoxImage.Warning);                  
                    txttestresult.Text = "";
                }

            }
            catch { }
        }
        private void diffchek()
        {
            try
            {
                DateTime ss = Convert.ToDateTime(dpInsDate.Text);
                DateTime ss1 = Convert.ToDateTime(instdt);
                string chk = ss1.ToString();
                if (chk == "01/01/0001 00:00:00")
                {
                    txttestdate.Text = "0";
                    AddTailResidualLabTestViewModel._AddResidualLabTest.ServiceTestDate = null;
                }
                else if (ss1 > ss)
                {
                    txttestdate.Text = "0";
                    AddTailResidualLabTestViewModel._AddResidualLabTest.ServiceTestDate = null;
                }
                else
                {
                    const double DaysPerMonth = 36524.0 / 1200.0;
                    double days = (ss - ss1).TotalDays;
                    double ddtt = days / DaysPerMonth;

                    Double d1 = Convert.ToDouble(ddtt);
                    Double dc1 = Math.Round((Double)d1, 1);


                    var ssss = (ss - ss1).TotalDays;

                    decimal diff = Convert.ToDecimal(ssss / 30);

                    //txttestdate.Text = diff.ToString();

                    Double d = Convert.ToDouble(diff);
                    Double dc = Math.Round((Double)d, 1);

                    txttestdate.Text = dc1.ToString();
                    AddTailResidualLabTestViewModel._AddResidualLabTest.ServiceTestDate = Convert.ToDecimal(dc1);
                }
            }
            catch { }
        }

        private void dpInsDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddTailResidualLabTestViewModel.sropetype.Id != null)
                {

                    var data = sc.MooringWinchRope.Where(x => x.Id == AddTailResidualLabTestViewModel.sropetype.Id && x.DeleteStatus == false && x.RopeTail == 1).Select(x => x.ReceivedDate).SingleOrDefault();

                    var installeddate = data;

                    var assigndate = dpInsDate.Text;

                    if (Convert.ToDateTime(assigndate) < installeddate)
                    {
                        MessageBox.Show("Lab Test Date can not less than RopeTail Received date!", "Residual Lab Test", MessageBoxButton.OK, MessageBoxImage.Error);

                        dpInsDate.Text = installeddate.ToString();
                    }
                    else
                    {
                        diffchek();
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Tail First !", "Residual Lab Test", MessageBoxButton.OK, MessageBoxImage.Error);

                    dpInsDate.Text = DateTime.Now.ToString();
                }
            }
            catch { }
            //diffchek();
        }
    }
}
