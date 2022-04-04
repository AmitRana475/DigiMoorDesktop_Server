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
    /// Interaction logic for ViewRopeDisposal.xaml
    /// </summary>
    public partial class ViewRopeDisposal : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewRopeDisposal()
        {
            InitializeComponent();


            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }

            int id = StaticHelper.ViewId;
            int ropetailid = StaticHelper.RopeTailId;
            SqlDataAdapter adp = new SqlDataAdapter("select a.*,b.certificatenumber,b.UniqueId from ropedisposal a inner join MooringRopeDetail b on a.RopeId=b.Id where a.id=" + id + " and a.ropetail=" + ropetailid + "", sc.con);
           
            DataTable dt = new DataTable();
            adp.Fill(dt);




                     if (ropetailid == 0)
                     {
                            hdrtitle.Content = "View Line Disposal";
                     }
                     if (ropetailid == 1)
                     {
                            hdrtitle.Content = "View RopeTail Disposal";
                     }

                     if (dt.Rows.Count > 0)
            {
                txt1.Text = dt.Rows[0]["certificatenumber"].ToString();
                txtUniqueId.Text = dt.Rows[0]["UniqueId"].ToString();

                txt2.Text = dt.Rows[0]["DisposalPortName"] == DBNull.Value ? "-" : dt.Rows[0]["DisposalPortName"].ToString();
                //txtAssignedDate.Text = Convert.ToDateTime(dt.Rows[0]["CroppedDate"]).ToString("d MMM, yyyy");
                txt3.Text = dt.Rows[0]["ReceptionFacilityName"] == DBNull.Value ? "-" : dt.Rows[0]["ReceptionFacilityName"].ToString();
                txt4.Text = Convert.ToDateTime(dt.Rows[0]["DisposalDate"]).ToString("yyyy-MM-dd");

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
