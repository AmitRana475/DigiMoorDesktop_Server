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
    /// Interaction logic for ViewRopeSplicing.xaml
    /// </summary>
    public partial class ViewRopeSplicing : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewRopeSplicing()
        {
            InitializeComponent();


            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }

            int id = StaticHelper.ViewId;
            int ropetailid = StaticHelper.RopeTailId;

            if (ropetailid == 0)
            {
                lbl1.Visibility = Visibility.Visible;
                txtcrpoutend.Visibility = Visibility.Visible;
            }
            else
            {
                lbl1.Visibility = Visibility.Hidden;
                txtcrpoutend.Visibility = Visibility.Hidden;
            }

            SqlDataAdapter adp = new SqlDataAdapter("ViewRopeSplicing", sc.con);
            adp.SelectCommand.Parameters.AddWithValue("@RopeTail", ropetailid);
            adp.SelectCommand.Parameters.AddWithValue("@Id", id);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet dt = new DataSet();
            adp.Fill(dt);
                     if (ropetailid == 0)
                     {
                            hdrtitle.Content = "View Line Splicing";
                     }
                     if (ropetailid == 1)
                     {
                            hdrtitle.Content = "View RopeTail Splicing";
                     }
                     if (dt.Tables[0].Rows.Count > 0)
            {
                txtUniqueId.Text = dt.Tables[0].Rows[0]["UniqueId"].ToString();
                txtsplicingmethod.Text = dt.Tables[0].Rows[0]["SplicingMethod"] == DBNull.Value ? "Not Assigned" : dt.Tables[0].Rows[0]["SplicingMethod"].ToString();
                txtRope.Text = dt.Tables[0].Rows[0]["certificatenumber"].ToString();
                txtOutboard.Text = dt.Tables[0].Rows[0]["SplicingDoneBy"].ToString();
                txtAssignedDate.Text = Convert.ToDateTime(dt.Tables[0].Rows[0]["SplicingDoneDate"]).ToString("yyyy-MM-dd");
                txtAssignedLocation.Text = dt.Tables[0].Rows[0]["AssignedLocation"] == DBNull.Value ? "Not Assigned" : dt.Tables[0].Rows[0]["AssignedLocation"].ToString();
                txtAssignedRopeWinch.Text = dt.Tables[0].Rows[0]["assignednumber"] == DBNull.Value ? "Not Assigned" : dt.Tables[0].Rows[0]["assignednumber"].ToString();
                //txtSplicingMethod.Text = dt.Rows[0]["SplicingMethod"].ToString();
            }

            if (dt.Tables[1].Rows.Count > 0)
            {

                txtResonofcrp.Visibility = Visibility.Visible;
                txtcrpoutend.Visibility = Visibility.Visible;
                txtcrpdt.Visibility = Visibility.Visible;
                txtcrpdt.Visibility = Visibility.Visible;

                txtResonofcrp.Text = dt.Tables[1].Rows[0]["ReasonofCropping"].ToString();
                txtcrpoutend.Text = dt.Tables[1].Rows[0]["CroppedOutboardEnd"].ToString();
                txtcrpdt.Text = Convert.ToDateTime(dt.Tables[1].Rows[0]["CroppedDate"]).ToString("yyyy-MM-dd");
                txtlengthcrp.Text = dt.Tables[1].Rows[0]["LengthofCroppedRope"] == DBNull.Value ? "Not Assigned" : dt.Tables[1].Rows[0]["LengthofCroppedRope"].ToString();
            
            }
            else
            {
                txtResonofcrp.Visibility = Visibility.Hidden;
                txtcrpoutend.Visibility = Visibility.Hidden;
                txtcrpdt.Visibility = Visibility.Hidden;
                txtcrpdt.Visibility = Visibility.Hidden;
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