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
    /// Interaction logic for ViewRopeDamage.xaml
    /// </summary>
    public partial class ViewRopeDamage : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewRopeDamage()
        {
            InitializeComponent();

            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }

            int id = StaticHelper.ViewId;
            int ropetailid = StaticHelper.RopeTailId;

            if(ropetailid== 1)
            {
                lblDmgloc.Visibility = Visibility.Hidden;
                txtdmglocation.Visibility = Visibility.Hidden;
            }
            if (ropetailid == 0)
            {
                lblDmgloc.Visibility = Visibility.Visible;
                txtdmglocation.Visibility = Visibility.Visible;
            }

            SqlDataAdapter adp = new SqlDataAdapter("ViewDamageRope", sc.con);
            adp.SelectCommand.Parameters.AddWithValue("@RopeTail", ropetailid);
            adp.SelectCommand.Parameters.AddWithValue("@Id", id);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adp.Fill(dt);
                     
                     if(ropetailid ==0)
                     {
                            hdrtitle.Content = "View Line Damage";
                     }
                     if (ropetailid == 1)
                     {
                            hdrtitle.Content = "View RopeTail Damage";
                     }



                     if (dt.Rows.Count > 0)
            {
                txtRope.Text = dt.Rows[0]["certificatenumber"].ToString();
                txtUniqueId.Text = dt.Rows[0]["UniqueID"].ToString();

                txtOutboard.Text = dt.Rows[0]["Operation"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["Operation"].ToString();
                //txtAssignedDate.Text = Convert.ToDateTime(dt.Rows[0]["CroppedDate"]).ToString("d MMM, yyyy");
                txtAssignedLocation.Text = dt.Rows[0]["AssignedLocation"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["AssignedLocation"].ToString();
                txtAssignedRopeWinch.Text = dt.Rows[0]["assignednumber"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["assignednumber"].ToString();
                txtDamageObserved.Text = dt.Rows[0]["DamageObserved"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["DamageObserved"].ToString();
                //txtReason.Text = dt.Rows[0]["damagereason"].ToString();

                string ss= Convert.ToDateTime(dt.Rows[0]["damagedate"] == DBNull.Value ? null : dt.Rows[0]["damagedate"]).ToString("yyyy-MM-dd");
                if (ss == "0001-01-01")
                {
                    txtdmgdate.Text = "Not Assigned";
                }
                else
                {
                    txtdmgdate.Text = Convert.ToDateTime(dt.Rows[0]["damagedate"] == DBNull.Value ? null : dt.Rows[0]["damagedate"]).ToString("yyyy-MM-dd");
                }
                txtReason.Text = dt.Rows[0]["damagereason"] == DBNull.Value ? "Not Assigned" :  dt.Rows[0]["damagereason"].ToString();
                txtdmglocation.Text = dt.Rows[0]["damagelocation"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["damagelocation"].ToString();
                txtincident.Text = dt.Rows[0]["IncidentReport"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["IncidentReport"].ToString();

                //txtMop.Text = dt.Rows[0]["mopid"] == DBNull.Value ? "-" : dt.Rows[0]["mopid"].ToString();

                //try
                //{
                //    if (txtMop.Text != "-")
                //    {
                //        SqlDataAdapter adp1 = new SqlDataAdapter("select OPId, (portname + ' - '+ convert(varchar, fastdatetime, 106)) as Operation from MOperationBirthDetail where IsActive = 1 and OPId = " + txtMop.Text + "", sc.con);
                //        DataTable dt1 = new DataTable();
                //        adp1.Fill(dt1);

                //        if (dt1.Rows.Count > 0)
                //        {
                //            txtMop.Text = dt1.Rows[0]["Operation"] == DBNull.Value ? "-" : dt1.Rows[0]["Operation"].ToString();
                //        }
                //    }
                //}
                //catch { }

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
