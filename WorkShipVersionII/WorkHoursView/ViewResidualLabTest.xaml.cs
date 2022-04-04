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
    /// Interaction logic for ViewResidualLabTest.xaml
    /// </summary>
    public partial class ViewResidualLabTest : UserControl
    {
        private readonly ShipmentContaxt sc;
        public ViewResidualLabTest()
        {
            InitializeComponent();

            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }

            try
            {
                int id = StaticHelper.ViewId;
                SqlCommand cmd = new SqlCommand("ViewResidualList", sc.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RopeTail", StaticHelper.RopeTailId);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    txtCer.Text = dt.Rows[0]["certificatenumber"].ToString();
                    txtUniqueId.Text = dt.Rows[0]["UniqueId"].ToString();
                    txtLoc.Text = dt.Rows[0]["Location"].ToString();
                    txtLabTest.Text = Convert.ToDateTime(dt.Rows[0]["LabTestDate"]).ToString("yyyy-MM-dd");
                    txtRngSer.Text = dt.Rows[0]["RunningHours"].ToString();
                                   txtservicedt.Text = dt.Rows[0]["ServiceTestDate"].ToString();
                                   txtResStrength.Text = dt.Rows[0]["TestResults"].ToString();
                    txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                    txtManu.Text = dt.Rows[0]["name"].ToString();
                }
            }
            catch { }
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
