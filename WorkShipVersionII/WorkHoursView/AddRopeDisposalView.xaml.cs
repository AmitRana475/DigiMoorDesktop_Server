using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
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
    /// Interaction logic for AddRopeDisposalView.xaml
    /// </summary>
    public partial class AddRopeDisposalView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public AddRopeDisposalView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            sc = new ShipmentContaxt();
        }

        private void comboRank_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                btnSave.IsEnabled = true;
                var cerno = comboRank.Text;

                // var outofserdt = sc.MooringWinchRope.Where(x => x.CertificateNumber == cerno && x.DeleteStatus == false).Select(x => x.OutofServiceDate).SingleOrDefault();


                var data = sc.AssignRopetoWinch.Where(x => x.RopeId == AddRopeDisposalViewModel._AddRopeDisposal.RopeId && x.IsActive == true).FirstOrDefault();

                if (data != null)
                {
                    int winchid = data.WinchId;
                    AddRopeDisposalViewModel._AddRopeDisposal.WinchId = winchid;
                }
                else
                {
                    AddRopeDisposalViewModel._AddRopeDisposal.WinchId = 0;
                }

                 var outofserdt = sc.MooringWinchRope.Where(x => x.Id == AddRopeDisposalViewModel._AddRopeDisposal.RopeId && x.DeleteStatus == false).Select(x => x.OutofServiceDate).SingleOrDefault();


                if (outofserdt ==null)
                {
                    txtmessage.Visibility = Visibility.Visible;

                    lblDisDt.Visibility = Visibility.Hidden;
                    lbldtoutofs.Visibility = Visibility.Hidden;
                    lblprtname.Visibility = Visibility.Hidden;
                    lblRecep.Visibility = Visibility.Hidden;

                    rpdis.Height = 250;

                    lbl1.Visibility = Visibility.Hidden;
                    lbl2.Visibility = Visibility.Hidden;
                    lbl3.Visibility = Visibility.Hidden;

                    txtDisPrtN.Visibility = Visibility.Hidden;
                    txtdtOutofS.Visibility = Visibility.Hidden;
                    txtReceFaciN.Visibility = Visibility.Hidden;
                    dpRecDate.Visibility = Visibility.Hidden;

                    btnSave.Visibility = Visibility.Hidden;
                    btnCancel.Visibility = Visibility.Hidden;

                    txtmessage.Text = "Line not yet Out of Service, Please fill Line Discard Form before filling the Disposal Form.";
                   

                }
                else
                {
                    rpdis.Height = 410;
                    txtmessage.Visibility = Visibility.Hidden;

                    lblDisDt.Visibility= Visibility.Visible;
                    lbldtoutofs.Visibility = Visibility.Visible;
                    lblprtname.Visibility = Visibility.Visible;
                    lblRecep.Visibility = Visibility.Visible;


                    lbl1.Visibility = Visibility.Visible;
                    lbl2.Visibility = Visibility.Visible;
                    lbl3.Visibility = Visibility.Visible;
                    txtDisPrtN.Visibility = Visibility.Visible;
                    txtdtOutofS.Visibility = Visibility.Visible;
                    txtReceFaciN.Visibility = Visibility.Visible;
                    dpRecDate.Visibility = Visibility.Visible;

                    lblDisDt.Visibility = Visibility.Visible;

                    btnSave.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Visible;

                    DateTime dt = Convert.ToDateTime(outofserdt);
                    string ss = dt.ToString("dddd, dd MMMM yyyy");
                    txtdtOutofS.Text = ss;

                }

             

            }
            catch
            {

            }
            //SqlDataAdapter adp = new SqlDataAdapter();
            //DataTable dt = new DataTable();
            //adp.Fill(dt);


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
                    MessageBox.Show("Disposal Date can not be less than Line Discard Date!", "Line Disposal", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (Convert.ToInt32(comboRank.SelectedValue) == 0)
                {
                    MessageBox.Show("Please select Line First !", "Line Disposal", MessageBoxButton.OK, MessageBoxImage.Error);
                    string Frm = Convert.ToString(DateTime.Now);
                    dpRecDate.Text = Frm;
                }
                else
                {
                    SqlDataAdapter adp = new SqlDataAdapter("select OutofServiceDate from mooringropedetail where ID=" + comboRank.SelectedValue + " ", sc.con);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {

                        dtt = dt.Rows[0][0].ToString();

                    }
                }
                return dtt;
            }
            catch { return dtt; }
        }
    }
}
