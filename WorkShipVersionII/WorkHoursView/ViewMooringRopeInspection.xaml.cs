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
    /// Interaction logic for ViewMooringRopeInspection.xaml
    /// </summary>
    public partial class ViewMooringRopeInspection : UserControl
    {
        private readonly ShipmentContaxt sc;
        public ViewMooringRopeInspection()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            InitializeComponent();

           // BindViewRopeInspection();
        }

        private void BindViewRopeInspection()
        {
            string qry = "select a.*,b.assignednumber,b.location,c.certificatenumber,c.UniqueId from MooringRopeInspection a inner join MooringWinchDetail b on a.winchid=b.id inner join MooringRopeDetail c on a.RopeId=c.Id";
            SqlDataAdapter sda = new SqlDataAdapter(qry, sc.con);
            //sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            //sda.SelectCommand.Parameters.AddWithValue("@RopeTail", 0);
            System.Data.DataTable datatbl = new System.Data.DataTable();
            sda.Fill(datatbl);

            MooringRopeInspectionGrid.ItemsSource = datatbl.AsDataView();
        }
    }
}
