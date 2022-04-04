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
    /// Interaction logic for ViewRopeTail.xaml
    /// </summary>
    public partial class ViewRopeTail : UserControl
    {
        private readonly ShipmentContaxt sc;
        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewRopeTail()
        {
            InitializeComponent();


            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }
          
            int id = StaticHelper.ViewId;
            SqlDataAdapter adp = new SqlDataAdapter("select * from ropetail where id=" + id + "", sc.con);
            //adp.SelectCommand.Parameters.AddWithValue("@Action", "ViewPage");
            //adp.SelectCommand.Parameters.AddWithValue("@Id", id);
            //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txt2.Text = dt.Rows[0]["ManufactureName"] == DBNull.Value ? "-" : dt.Rows[0]["ManufactureName"].ToString();

                txt3.Text = dt.Rows[0]["RopeConstruction"] == DBNull.Value ? "-" : dt.Rows[0]["RopeConstruction"].ToString();
                //txt3.Text = Convert.ToDateTime(dt.Rows[0]["ManufactureName"]).ToString("d MMM, yyyy");
                txt4.Text = dt.Rows[0]["Diameter"] == DBNull.Value ? "-" : dt.Rows[0]["Diameter"].ToString();

                // txt4.Text = dt.Rows[0]["PortName"] == DBNull.Value ? "-" : dt.Rows[0]["PortName"].ToString();
                txt5.Text = dt.Rows[0]["Length"] == DBNull.Value ? "-" : dt.Rows[0]["Length"].ToString();
                txt6.Text = dt.Rows[0]["MBL"] == DBNull.Value ? "-" : dt.Rows[0]["MBL"].ToString();
                txt7.Text = dt.Rows[0]["LDBF"] == DBNull.Value ? "-" : dt.Rows[0]["LDBF"].ToString();
                txt8.Text = dt.Rows[0]["WLL"] == DBNull.Value ? "-" : dt.Rows[0]["WLL"].ToString();
                //txt9.Text = dt.Rows[0]["MaxRunHours"] == DBNull.Value ? "-" : dt.Rows[0]["MaxRunHours"].ToString();
                //txt10.Text = dt.Rows[0]["MaxYearServiceMonth"] == DBNull.Value ? "-" : dt.Rows[0]["MaxYearServiceMonth"].ToString();
                txt11.Text = dt.Rows[0]["CertificateNumber"] == DBNull.Value ? "-" : dt.Rows[0]["CertificateNumber"].ToString();


                txt19.Text = dt.Rows[0]["UniqueID"] == DBNull.Value ? "-" : dt.Rows[0]["UniqueID"].ToString();

                txt12.Text = Convert.ToDateTime(dt.Rows[0]["ReceivedDate"] == DBNull.Value ? null : dt.Rows[0]["ReceivedDate"]).ToString("yyyy-MM-dd");

                txt13.Text = Convert.ToDateTime(dt.Rows[0]["InstalledDate"] == DBNull.Value ? null : dt.Rows[0]["InstalledDate"]).ToString("yyyy-MM-dd");
                if (txt13.Text == "0001-01-01")
                {
                    txt13.Text = "Not Assigned";
                }
                txt15.Text = dt.Rows[0]["Remarks"] == DBNull.Value ? "-" : dt.Rows[0]["Remarks"].ToString();
                //txt12.Text = Convert.ToDateTime(dt.Rows[0]["ReceivedDate"]).ToString("d MMM, yyyy");
                //txt13.Text = Convert.ToDateTime(dt.Rows[0]["InstalledDate"]).ToString("d MMM, yyyy");
                txt14.Text = dt.Rows[0]["RopeTagging"] == DBNull.Value ? "-" : dt.Rows[0]["RopeTagging"].ToString();
                //txt15.Text = Convert.ToDateTime(dt.Rows[0]["OutofServiceDate"]).ToString("d MMM, yyyy");
                //txt15.Text = Convert.ToDateTime(dt.Rows[0]["OutofServiceDate"] == DBNull.Value ? null : dt.Rows[0]["OutofServiceDate"]).ToString("d MMM, yyyy");
                //txt16.Text = dt.Rows[0]["ReasonOutofService"] == DBNull.Value ? "-" : dt.Rows[0]["ReasonOutofService"].ToString();
                //txt17.Text = dt.Rows[0]["OtherReason"] == DBNull.Value ? "-" : dt.Rows[0]["OtherReason"].ToString();
                //txt18.Text = dt.Rows[0]["DamageObserved"] == DBNull.Value ? "-" : dt.Rows[0]["DamageObserved"].ToString();



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
