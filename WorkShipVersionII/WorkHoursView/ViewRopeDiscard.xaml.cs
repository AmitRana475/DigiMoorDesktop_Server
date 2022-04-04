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
    /// Interaction logic for ViewRopeDiscard.xaml
    /// </summary>
    public partial class ViewRopeDiscard : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewRopeDiscard()
        {
            InitializeComponent();

            try
            {
                sc = new ShipmentContaxt();

                int id = StaticHelper.ViewId;
                int ropetailid = StaticHelper.RopeTailId;
                if (ropetailid == 0)
                {
                    lbl1.Visibility = Visibility.Visible;
                    lbl2.Visibility = Visibility.Visible;
                }
                else
                {
                    lbl1.Visibility = Visibility.Hidden;
                    lbl2.Visibility = Visibility.Hidden;
                }

                SqlDataAdapter adp = new SqlDataAdapter("ViewRopeDiscardList", sc.con);
                adp.SelectCommand.Parameters.AddWithValue("@RopeTail", ropetailid);
                adp.SelectCommand.Parameters.AddWithValue("@Id", id);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                adp.Fill(dt);



                            if (ropetailid == 0)
                            {
                                   hdrtitle.Content = "View Line Discard";
                            }
                            if (ropetailid == 1)
                            {
                                   hdrtitle.Content = "View RopeTail Discard";
                            }

                            if (dt.Rows.Count > 0)
                {
                    int rpid = Convert.ToInt32(dt.Rows[0]["RopeId"]);
                    txtRope.Text = dt.Rows[0]["certificatenumber"].ToString();

                    txtUniqueId.Text = dt.Rows[0]["UniqueId"].ToString();

                    txtOutboard.Text = dt.Rows[0]["Outboard"] == DBNull.Value ? "-" : dt.Rows[0]["Outboard"].ToString();
                    txtoutofServicedt.Text = Convert.ToDateTime(dt.Rows[0]["outofservicedate"]).ToString("yyyy-MM-dd");

                    txtAssignedLocation.Text = dt.Rows[0]["AssignedLocation"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["AssignedLocation"].ToString();
                    txtAssignedRopeWinch.Text = dt.Rows[0]["assignednumber"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["assignednumber"].ToString();


                     txtDmgObserved.Text = dt.Rows[0]["DamageObserved"].ToString();
                    //txtReason.Text = dt.Rows[0]["damagereason"].ToString();


                    int opId = sc.MOUsedWinchTbl.Where(x => x.RopeId == rpid).Select(item => item.OperationID).Distinct().Count();

                    //int opId = sc.MOUsedWinchTbl.Select(item => item.OperationID).Distinct().Count();
                    txtnoofOP.Text = opId.ToString();

                    txtrunninghrs.Text= dt.Rows[0]["currentrunninghours"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["currentrunninghours"].ToString();

                    txtReason.Text = dt.Rows[0]["reasonoutofservice"] == DBNull.Value ? "Not Assigned" : dt.Rows[0]["reasonoutofservice"].ToString();
                    //txtdmglocation.Text = dt.Rows[0]["damagelocation"] == DBNull.Value ? "-" : dt.Rows[0]["damagelocation"].ToString();
                    // txtdmglocation.Text = dt.Rows[0]["damagelocation"].ToString();
                    txtMop.Text= dt.Rows[0]["MooringOperationID"] == DBNull.Value ? "-" : dt.Rows[0]["MooringOperationID"].ToString();

                    try
                    {
                        if (txtMop.Text != "-")
                        {
                            SqlDataAdapter adp1 = new SqlDataAdapter("select OPId, (portname + ' - '+ convert(varchar, fastdatetime, 106)) as Operation from MOperationBirthDetail where IsActive = 1 and OPId = " + txtMop.Text + "", sc.con);
                            DataTable dt1 = new DataTable();
                            adp1.Fill(dt1);

                            if (dt1.Rows.Count > 0)
                            {
                                txtMop.Text = dt1.Rows[0]["Operation"] == DBNull.Value ? "-" : dt1.Rows[0]["Operation"].ToString();
                            }
                        }
                    }
                    catch { }
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
