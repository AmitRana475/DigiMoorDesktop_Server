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
    /// Interaction logic for ViewRopeCropping.xaml
    /// </summary>
    public partial class ViewRopeCropping : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewRopeCropping()
        {
            InitializeComponent();


            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }

            int id = StaticHelper.ViewId;
            int ropetailid = StaticHelper.RopeTailId;
            if(ropetailid==0)
            {
                lbl1.Visibility = Visibility.Visible;
                lbl2.Visibility = Visibility.Visible;
            }else
            {
                lbl1.Visibility = Visibility.Hidden;
                lbl2.Visibility = Visibility.Hidden;
            }

            SqlDataAdapter adp = new SqlDataAdapter("ViewRopeCropping", sc.con);
            adp.SelectCommand.Parameters.AddWithValue("@RopeTail", ropetailid);
            adp.SelectCommand.Parameters.AddWithValue("@Id", id);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adp.Fill(dt);

                     if (ropetailid == 0)
                     {
                            hdrtitle.Content = "View Line Cropping";
                            txtname.Text = "Line";
                            txtlngth.Text = "Length of Cropped Line";
                     }
                     if (ropetailid == 1)
                     {
                            hdrtitle.Content = "View RopeTail Cropping";
                            txtname.Text = "Tail";
                            txtlngth.Text = "Length of Cropped Tail";
                     }
                     if (dt.Rows.Count > 0)
            {
                txtRope.Text = dt.Rows[0]["certificatenumber"].ToString();
                txtOutboard.Text = dt.Rows[0]["CroppedOutboardEnd"].ToString();
                txtAssignedDate.Text = Convert.ToDateTime(dt.Rows[0]["CroppedDate"]).ToString("yyyy-MM-dd");
                
                txtAssignedLocation.Text = dt.Rows[0]["AssignedLocation"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["AssignedLocation"].ToString();
                txtAssignedRopeWinch.Text = dt.Rows[0]["assignednumber"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["assignednumber"].ToString();
                txtSplicingMethod.Text = dt.Rows[0]["LengthofCroppedRope"].ToString();
                txtReason.Text = dt.Rows[0]["reasonofcropping"].ToString();
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
