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
    /// Interaction logic for ViewMooringOperation.xaml
    /// </summary>
    public partial class ViewMooringOperation : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewMooringOperation()
        {
            InitializeComponent();


            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }
            try
            {
                int id = StaticHelper.ViewId;
                SqlDataAdapter adp = new SqlDataAdapter("select * from moperationbirthdetail where Opid=" + id + "", sc.con);
                //adp.SelectCommand.Parameters.AddWithValue("@Action", "ViewPage");
                //adp.SelectCommand.Parameters.AddWithValue("@Id", id);
                //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    txt2.Text = dt.Rows[0]["PortName"] == DBNull.Value ? "-" : dt.Rows[0]["PortName"].ToString();

                                   //txt3.Text = Convert.ToDateTime(dt.Rows[0]["FastDateTime"]).ToString("d MMM, yyyy");
                                   //txt4.Text = Convert.ToDateTime(dt.Rows[0]["CastDateTime"]).ToString("d MMM, yyyy");
                                  // DateTime dateValue;
                                   CultureInfo culture = CultureInfo.CurrentCulture;
                                   var dateformat = culture.DateTimeFormat;
                                   var dfd = dateformat.FullDateTimePattern;
                                  // DateTimeStyles styles = DateTimeStyles.None;
                                  // DateTime.TryParse(d1, culture, styles, out dateValue);

                                   txt3.Text = Convert.ToDateTime(dt.Rows[0]["FastDateTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    txt4.Text = Convert.ToDateTime(dt.Rows[0]["CastDateTime"]).ToString("yyyy-MM-dd HH:mm:ss");

                                   // txt4.Text = dt.Rows[0]["PortName"] == DBNull.Value ? "-" : dt.Rows[0]["PortName"].ToString();
                                   txt5.Text = dt.Rows[0]["BirthName"] == DBNull.Value ? "-" : dt.Rows[0]["BirthName"].ToString();
                    txt6.Text = dt.Rows[0]["BirthType"] == DBNull.Value ? "-" : dt.Rows[0]["BirthType"].ToString();
                    txt7.Text = dt.Rows[0]["MooringType"] == DBNull.Value ? "-" : dt.Rows[0]["MooringType"].ToString();
                    txt8.Text = dt.Rows[0]["DraftArrivalFWD"] == DBNull.Value ? "-" : dt.Rows[0]["DraftArrivalFWD"].ToString();
                    txt9.Text = dt.Rows[0]["DraftArrivalAFT"] == DBNull.Value ? "-" : dt.Rows[0]["DraftArrivalAFT"].ToString();
                    txt10.Text = dt.Rows[0]["DraftDepartureFWD"] == DBNull.Value ? "-" : dt.Rows[0]["DraftDepartureFWD"].ToString();
                    txt11.Text = dt.Rows[0]["DraftDepartureAFT"] == DBNull.Value ? "-" : dt.Rows[0]["DraftDepartureAFT"].ToString();
                    txt12.Text = dt.Rows[0]["DepthAtBerth"] == DBNull.Value ? "-" : dt.Rows[0]["DepthAtBerth"].ToString();
                    txt13.Text = dt.Rows[0]["BerthSide"] == DBNull.Value ? "-" : dt.Rows[0]["BerthSide"].ToString();
                    txt14.Text = dt.Rows[0]["VesselCondition"] == DBNull.Value ? "-" : dt.Rows[0]["VesselCondition"].ToString();
                    txt15.Text = dt.Rows[0]["ShipAccess"] == DBNull.Value ? "-" : dt.Rows[0]["ShipAccess"].ToString();
                    txt16.Text = dt.Rows[0]["RangOfTide"] == DBNull.Value ? "-" : dt.Rows[0]["RangOfTide"].ToString();
                    txt17.Text = dt.Rows[0]["WindDirection"] == DBNull.Value ? "-" : dt.Rows[0]["WindDirection"].ToString();
                    txt18.Text = dt.Rows[0]["WindSpeed"] == DBNull.Value ? "-" : dt.Rows[0]["WindSpeed"].ToString();
                    txt19.Text = dt.Rows[0]["AnySquall"] == DBNull.Value ? "-" : dt.Rows[0]["AnySquall"].ToString();
                    txt20.Text = dt.Rows[0]["CurrentSpeed"] == DBNull.Value ? "-" : dt.Rows[0]["CurrentSpeed"].ToString();
                    txt21.Text = dt.Rows[0]["Berth_exposed_SeaSwell"] == DBNull.Value ? "-" : dt.Rows[0]["Berth_exposed_SeaSwell"].ToString();
                    txt22.Text = dt.Rows[0]["SurgingObserved"] == DBNull.Value ? "-" : dt.Rows[0]["SurgingObserved"].ToString();
                    txt23.Text = dt.Rows[0]["Any_Affect_Passing_Traffic"] == DBNull.Value ? "-" : dt.Rows[0]["Any_Affect_Passing_Traffic"].ToString();
                    txt24.Text = dt.Rows[0]["Ship_was_continuously_contact_with_fender"] == DBNull.Value ? "-" : dt.Rows[0]["Ship_was_continuously_contact_with_fender"].ToString();

                    txt25.Text = dt.Rows[0]["FacilityName"] == DBNull.Value ? "-" : dt.Rows[0]["FacilityName"].ToString();
                    txt26.Text = dt.Rows[0]["PortDetails"] == DBNull.Value ? "-" : dt.Rows[0]["PortDetails"].ToString();
                    txt27.Text = dt.Rows[0]["Any_Rope_Damaged"] == DBNull.Value ? "-" : dt.Rows[0]["Any_Rope_Damaged"].ToString();
                    //txt25.Text = dt.Rows[0]["PortName"] == DBNull.Value ? "-" : dt.Rows[0]["PortName"].ToString();
                    //txt26.Text = dt.Rows[0]["PortName"] == DBNull.Value ? "-" : dt.Rows[0]["PortName"].ToString();



                }

                //SqlDataAdapter adp1 = new SqlDataAdapter("select a.lead,a.lead1, b.assignednumber,b.location,c.certificatenumber from MOUsedWinchTbl a inner join MooringWinchDetail b on a.WinchId=b.Id inner join MooringRopeDetail c on a.RopeId=c.Id where a.OperationID=" + id + "", sc.con);

                SqlDataAdapter adp1 = new SqlDataAdapter("UsedRopesInMOp", sc.con);
                adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp1.SelectCommand.Parameters.AddWithValue("@OpertaionId", id);
                DataSet dt1 = new DataSet();
                adp1.Fill(dt1);
                if (dt1.Tables[0].Rows.Count > 0)
                {
                    WinchMainGrid1.ItemsSource = dt1.Tables[0].DefaultView;
                }

                if (dt1.Tables[1].Rows.Count > 0)
                {
                   WinchMainGrid2.ItemsSource = dt1.Tables[1].DefaultView;
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
