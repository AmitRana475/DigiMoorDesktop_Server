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
    /// Interaction logic for ViewWBTestKit.xaml
    /// </summary>
    public partial class ViewWBTestKit : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewWBTestKit()
        {
            InitializeComponent();

            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }

            try
            {


                int id = StaticHelper.ViewId;
                SqlDataAdapter adp = new SqlDataAdapter("WBTestKitList", sc.con);
                adp.SelectCommand.Parameters.AddWithValue("@Id", id);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    txtCertN.Text = dt.Rows[0]["CertificateNumber"].ToString();
                    txtUniqueId.Text = dt.Rows[0]["UniqueID"] == DBNull.Value ? "-" : dt.Rows[0]["UniqueID"].ToString();
                    txtRope.Text = dt.Rows[0]["ManufacturerName"].ToString();
                    txtrecdate.Text = dt.Rows[0]["ReceivedDate1"].ToString();
                    txtOutboard.Text = dt.Rows[0]["InstalledDate1"].ToString();
                    txtAssignedDate.Text = dt.Rows[0]["InspectionDueDate"].ToString();
                    txtAssignedRopeWinch.Text = dt.Rows[0]["Remarks"].ToString();
                }
            }
            catch { }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChildWindowManager.Instance.CloseChildWindow();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ChildWindowManager.Instance.CloseChildWindow();
        }
    }
}
