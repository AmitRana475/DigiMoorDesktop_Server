using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Globalization;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for ViewAssignRopetoWinch.xaml
    /// </summary>
    public partial class ViewAssignRopetoWinch : UserControl
    {
        private readonly ShipmentContaxt sc;

       TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewAssignRopetoWinch()
        {


            InitializeComponent();

            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }
            //test.Text = "RANA";
            int id = StaticHelper.ViewId;
            int ropetailid = StaticHelper.RopeTailId;

            if (ropetailid == 0)
            {
                lbl1.Visibility = Visibility.Visible;
                txtOutboard.Visibility = Visibility.Visible;
            }
            else
            {
                lbl1.Visibility = Visibility.Hidden;
                txtOutboard.Visibility = Visibility.Hidden;
            }
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("ViewAssignRopeToWinch", sc.con);
                adp.SelectCommand.Parameters.AddWithValue("@id", id);
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", ropetailid);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (ropetailid == 0)
                {
                    hdrtitle.Content = "View Line Assign To Winch";
                    txtlead.Text = dt.Rows[0]["Lead"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["Lead"].ToString();

                    txtlead.Visibility = Visibility.Visible;
                    lbllead.Visibility = Visibility.Visible;
                }
                if (ropetailid == 1)
                {
                    hdrtitle.Content = "View RopeTail Assign To Winch";
                    txtlead.Visibility = Visibility.Hidden;
                    lbllead.Visibility = Visibility.Hidden;
                }
                if (dt.Rows.Count > 0)
                {
                    txtUniqueId.Text = dt.Rows[0]["UniqueID"].ToString();
                    txtRope.Text = dt.Rows[0]["certificatenumber"].ToString();
                    txtOutboard.Text = dt.Rows[0]["Outboard"].ToString();
                    txtAssignedDate.Text = Convert.ToDateTime(dt.Rows[0]["AssignedDate"]).ToString("yyyy-MM-dd");
                    txtAssignedLocation.Text = dt.Rows[0]["Location"].ToString();
                    txtAssignedRopeWinch.Text = dt.Rows[0]["assignednumber"].ToString();

                    
                }
            }
            catch { }
        }
    }
}
