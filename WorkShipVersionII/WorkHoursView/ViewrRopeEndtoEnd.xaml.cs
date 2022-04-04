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
    /// Interaction logic for ViewrRopeEndtoEnd.xaml
    /// </summary>
    public partial class ViewrRopeEndtoEnd : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewrRopeEndtoEnd()
        {
            InitializeComponent();


            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }

            try
            {
                int id = StaticHelper.ViewId;
                SqlDataAdapter adp = new SqlDataAdapter("GetRopeEndtoEnd", sc.con);
                adp.SelectCommand.Parameters.AddWithValue("@Action", "ViewPage");
                adp.SelectCommand.Parameters.AddWithValue("@Id", id);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    txtUniqueId.Text = dt.Rows[0]["UniqueID"].ToString();
                    txtRope.Text = dt.Rows[0]["certificatenumber"].ToString();
                    txtOutboard.Text = dt.Rows[0]["CurrentOutboadEndinUse1"].ToString();
                    txtAssignedDate.Text = Convert.ToDateTime(dt.Rows[0]["EndtoEndDoneDate"]).ToString("yyyy-MM-dd");
                    txtAssignedLocation.Text = dt.Rows[0]["Location"].ToString();
                    txtAssignedRopeWinch.Text = dt.Rows[0]["assignednumber"].ToString();
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
