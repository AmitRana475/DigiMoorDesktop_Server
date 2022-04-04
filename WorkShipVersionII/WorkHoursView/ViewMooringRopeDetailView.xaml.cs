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

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for ViewMooringRopeDetailView.xaml
    /// </summary>
    public partial class ViewMooringRopeDetailView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public ViewMooringRopeDetailView()
        {
            InitializeComponent();
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            if(StaticHelper.RopeTailId==0)
                     {

                            grprope.Content = "LINE CONSTRUCTION";
                            rptype.Text = "Line Type";
                            rpconstruction.Text = "Line Construction";
                            isthisrope.Text = "Is this line installed and In Use?";
                            rptagging.Text = "Line Tagging";
                            txtldbf.Text = "LDBF(T)";
                     }
                     if (StaticHelper.RopeTailId == 1)
                     {
                            grprope.Content = "ROPETAIL CONSTRUCTION";
                            rptype.Text = "RopeTail Type";
                            rpconstruction.Text = "RopeTail Construction";
                            isthisrope.Text = "Is this ropetail installed and In Use?";
                            rptagging.Text = "RopeTail Tagging";
                            txtldbf.Text = "TDBF(T)";
                     }
              }

        public ViewMooringRopeDetailView(MooringWinchRopeClass rp)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            BindRopeDetail(rp.Id);
         

        }

        private void BindRopeDetail(int id)
        {
            SqlCommand cmd = new SqlCommand("ViewMooringRopeDetailList", sc.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RopeTail", 0);
            cmd.Parameters.AddWithValue("@RopeId", id);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            if (ds.Rows.Count >= 1)
            {
                string ss = ds.Rows[0]["RopeType"].ToString();
                //lblRopeType.Text = "ghkjgyui";
                //lblRopeTagging.Content = ds.Rows[0]["RopeTagging"];
                //lblRopeConstruction.Content = ds.Rows[0]["RopeConstruction"];
                //lblManuName.Content = ds.Rows[0]["Name"];
                //lblLength.Content = ds.Rows[0]["Length"];
                //lblMbl.Content = ds.Rows[0]["MBL"];
                //lblLDBF.Content = ds.Rows[0]["LDBF"];
                //lblWLL.Content = ds.Rows[0]["WLL"];
                //lblCertNo.Content = ds.Rows[0]["CertificateNumber"];

                //lblRecevDt.Content = ds.Rows[0]["ReceivedDate"];
                //lblInstallDt.Content = ds.Rows[0]["InstalledDate"];

            }
        }
    }
}
