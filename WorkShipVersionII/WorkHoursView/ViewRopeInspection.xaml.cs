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
    /// Interaction logic for ViewRopeInspection.xaml
    /// </summary>
    public partial class ViewRopeInspection : UserControl
    {
        private readonly ShipmentContaxt sc;

     TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewRopeInspection()
        {
            InitializeComponent();


            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }

            int id = StaticHelper.ViewId;
            int ropetailid = StaticHelper.RopeTailId;
            SqlDataAdapter adp = new SqlDataAdapter("select a.*, b.certificatenumber,c.assignednumber from MooringRopeInspection a inner join MooringRopeDetail b on a.RopeId=b.id inner join MooringWinchDetail c on a.WinchId=c.Id where a.id =" + id + " and a.ropetail=" + ropetailid + "", sc.con);
            //adp.SelectCommand.Parameters.AddWithValue("@Action", "ViewPage");
            //adp.SelectCommand.Parameters.AddWithValue("@Id", id);
            //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txt2.Text = dt.Rows[0]["InspectBy"] == DBNull.Value ? "-" : dt.Rows[0]["InspectBy"].ToString();

                txt3.Text = Convert.ToDateTime(dt.Rows[0]["InspectDate"]).ToString("yyyy-MM-dd");
                txt4.Text = dt.Rows[0]["certificatenumber"] == DBNull.Value ? "-" : dt.Rows[0]["certificatenumber"].ToString();

                // txt4.Text = dt.Rows[0]["PortName"] == DBNull.Value ? "-" : dt.Rows[0]["PortName"].ToString();
                txt5.Text = dt.Rows[0]["assignednumber"] == DBNull.Value ? "-" : dt.Rows[0]["assignednumber"].ToString();
                txt6.Text = dt.Rows[0]["ExternalRating_A"] == DBNull.Value ? "-" : dt.Rows[0]["ExternalRating_A"].ToString();
                txt7.Text = dt.Rows[0]["InternalRating_A"] == DBNull.Value ? "-" : dt.Rows[0]["InternalRating_A"].ToString();
                txt8.Text = dt.Rows[0]["AverageRating_A"] == DBNull.Value ? "-" : dt.Rows[0]["AverageRating_A"].ToString();
                txt9.Text = dt.Rows[0]["LengthOFAbrasion_A"] == DBNull.Value ? "-" : dt.Rows[0]["LengthOFAbrasion_A"].ToString();
                txt10.Text = dt.Rows[0]["DistanceOutboard_A"] == DBNull.Value ? "-" : dt.Rows[0]["DistanceOutboard_A"].ToString();
                txt11.Text = dt.Rows[0]["CutYarnCount_A"] == DBNull.Value ? "-" : dt.Rows[0]["CutYarnCount_A"].ToString();
                txt12.Text = dt.Rows[0]["LengthOFGlazing_A"] == DBNull.Value ? "-" : dt.Rows[0]["LengthOFGlazing_A"].ToString();
                txt13.Text = dt.Rows[0]["ExternalRating_B"] == DBNull.Value ? "-" : dt.Rows[0]["ExternalRating_B"].ToString();
                txt14.Text = dt.Rows[0]["InternalRating_B"] == DBNull.Value ? "-" : dt.Rows[0]["InternalRating_B"].ToString();
                txt15.Text = dt.Rows[0]["AverageRating_B"] == DBNull.Value ? "-" : dt.Rows[0]["AverageRating_B"].ToString();
                txt16.Text = dt.Rows[0]["LengthOFAbrasion_B"] == DBNull.Value ? "-" : dt.Rows[0]["LengthOFAbrasion_B"].ToString();
                txt17.Text = dt.Rows[0]["DistanceOutboard_B"] == DBNull.Value ? "-" : dt.Rows[0]["DistanceOutboard_B"].ToString();
                txt18.Text = dt.Rows[0]["CutYarnCount_B"] == DBNull.Value ? "-" : dt.Rows[0]["CutYarnCount_B"].ToString();
                txt19.Text = dt.Rows[0]["LengthOFGlazing_B"] == DBNull.Value ? "-" : dt.Rows[0]["LengthOFGlazing_B"].ToString();
                txt20.Text = dt.Rows[0]["Chafe_guard_condition"] == DBNull.Value ? "-" : dt.Rows[0]["Chafe_guard_condition"].ToString();
              
                if(ropetailid ==1)
                {
                    txt13.Visibility = Visibility.Collapsed;
                    txt14.Visibility = Visibility.Collapsed;
                    txt15.Visibility = Visibility.Collapsed;
                    txt16.Visibility = Visibility.Collapsed;
                    txt17.Visibility = Visibility.Collapsed;
                    txt18.Visibility = Visibility.Collapsed;
                    txt19.Visibility = Visibility.Collapsed;
                    txt20.Visibility = Visibility.Collapsed;

                    txtB.Visibility = Visibility.Collapsed;
                    txtB1.Visibility = Visibility.Collapsed;
                    txtB2.Visibility = Visibility.Collapsed;
                    txtB3.Visibility = Visibility.Collapsed;
                    txtB4.Visibility = Visibility.Collapsed;
                    txtB5.Visibility = Visibility.Collapsed;
                    txtB6.Visibility = Visibility.Collapsed;
                    txtB7.Visibility = Visibility.Collapsed;

                }
                else
                {
                    txt13.Visibility = Visibility.Visible;
                    txt14.Visibility = Visibility.Visible;
                    txt15.Visibility = Visibility.Visible;
                    txt16.Visibility = Visibility.Visible;
                    txt17.Visibility = Visibility.Visible;
                    txt18.Visibility = Visibility.Visible;
                    txt19.Visibility = Visibility.Visible;
                    txt20.Visibility = Visibility.Visible;


                    txtB.Visibility = Visibility.Visible;
                    txtB1.Visibility = Visibility.Visible;
                    txtB2.Visibility = Visibility.Visible;
                    txtB3.Visibility = Visibility.Visible;
                    txtB4.Visibility = Visibility.Visible;
                    txtB5.Visibility = Visibility.Visible;
                    txtB6.Visibility = Visibility.Visible;
                    txtB7.Visibility = Visibility.Visible;
                }
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
