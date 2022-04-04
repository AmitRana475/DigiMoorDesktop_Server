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
    /// Interaction logic for ViewLooseEDisposal.xaml
    /// </summary>
    public partial class ViewLooseEDisposal : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewLooseEDisposal()
        {
            InitializeComponent();


            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }

            int id = StaticHelper.ViewId;
            int ropetailid = StaticHelper.RopeTailId;
            SqlDataAdapter adp = new SqlDataAdapter("select a.*, b.looseequipmenttype from LooseEDisposal a inner join LooseEType b on a.LooseETypeId=b.Id where a.id=" + id + "", sc.con);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txt1.Text = dt.Rows[0]["looseequipmenttype"].ToString();

                txt5.Text = dt.Rows[0]["looseecertino"].ToString();
                txt2.Text = dt.Rows[0]["DisposalPortName"] == DBNull.Value ? "-" : dt.Rows[0]["DisposalPortName"].ToString();
                //txtAssignedDate.Text = Convert.ToDateTime(dt.Rows[0]["CroppedDate"]).ToString("d MMM, yyyy");
                txt3.Text = dt.Rows[0]["ReceptionFacilityName"] == DBNull.Value ? "-" : dt.Rows[0]["ReceptionFacilityName"].ToString();
                txt4.Text = Convert.ToDateTime(dt.Rows[0]["DisposalDate"]).ToString("yyyy-MM-dd");
                txt8.Text = Convert.ToDateTime(dt.Rows[0]["DiscardedDate"]).ToString("yyyy-MM-dd");

               // int looseetypeid = Convert.ToInt32(dt.Rows[0]["LooseETypeId"]);

               

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
