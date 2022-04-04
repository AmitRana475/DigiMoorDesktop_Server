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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for ViewChainStopper.xaml
    /// </summary>
    public partial class ViewChainStopper : UserControl
    {
        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewChainStopper()
        {
            InitializeComponent();


            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }

            int id = StaticHelper.ViewId;
            SqlDataAdapter adp = new SqlDataAdapter("select * from ChainStopper where id=" + id + "", sc.con);
            //adp.SelectCommand.Parameters.AddWithValue("@Action", "ViewPage");
            //adp.SelectCommand.Parameters.AddWithValue("@Id", id);
            //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txt2.Text = dt.Rows[0]["ManufactureName"] == DBNull.Value ? "-" : dt.Rows[0]["ManufactureName"].ToString();

                txt3.Text = dt.Rows[0]["CertificateNumber"] == DBNull.Value ? "-" : dt.Rows[0]["CertificateNumber"].ToString();


                txt10.Text = dt.Rows[0]["UniqueID"] == DBNull.Value ? "-" : dt.Rows[0]["UniqueID"].ToString();
                txt4.Text = dt.Rows[0]["MBL"] == DBNull.Value ? "-" : dt.Rows[0]["MBL"].ToString();

                // txt4.Text = dt.Rows[0]["PortName"] == DBNull.Value ? "-" : dt.Rows[0]["PortName"].ToString();
                txt5.Text = dt.Rows[0]["Length"] == DBNull.Value ? "-" : dt.Rows[0]["Length"].ToString();


                //txt6.Text = Convert.ToDateTime(dt.Rows[0]["DateReceived"]).ToString("d MMM, yyyy");
                //txt7.Text = Convert.ToDateTime(dt.Rows[0]["DateInstalled"]).ToString("d MMM, yyyy");
                //txt8.Text = Convert.ToDateTime(dt.Rows[0]["OutofServiceDate"]).ToString("d MMM, yyyy");

                txt6.Text = Convert.ToDateTime(dt.Rows[0]["DateReceived"] == DBNull.Value ? null : dt.Rows[0]["DateReceived"]).ToString("yyyy-MM-dd");
                txt7.Text = Convert.ToDateTime(dt.Rows[0]["DateInstalled"] == DBNull.Value ? null : dt.Rows[0]["DateInstalled"]).ToString("yyyy-MM-dd");
                if (txt7.Text == "0001-01-01")
                {
                    txt7.Text = "Not Assigned";
                }
                //txt8.Text = Convert.ToDateTime(dt.Rows[0]["OutofServiceDate"] == DBNull.Value ? null : dt.Rows[0]["OutofServiceDate"]).ToString("d MMM, yyyy");

                txt8.Text = dt.Rows[0]["Remarks"] == DBNull.Value ? "-" : dt.Rows[0]["Remarks"].ToString();
                //txt10.Text = dt.Rows[0]["OtherReason"] == DBNull.Value ? "-" : dt.Rows[0]["OtherReason"].ToString();
                //txt11.Text = dt.Rows[0]["DamagedObserved"] == DBNull.Value ? "-" : dt.Rows[0]["DamagedObserved"].ToString();


            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ChildWindowManager.Instance.CloseChildWindow();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChildWindowManager.Instance.CloseChildWindow();
        }
    }
}
