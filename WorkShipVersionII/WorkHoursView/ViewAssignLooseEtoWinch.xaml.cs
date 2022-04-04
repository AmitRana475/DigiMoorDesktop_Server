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
    /// Interaction logic for ViewAssignLooseEtoWinch.xaml
    /// </summary>
    public partial class ViewAssignLooseEtoWinch : UserControl
    {
        private readonly ShipmentContaxt sc;

        TextInfo textinfo = new CultureInfo("en-US", false).TextInfo;
        public ViewAssignLooseEtoWinch()
        {


            InitializeComponent();

            sc = new ShipmentContaxt();
   
            int id = StaticHelper.ViewId;
            int ropetailid = StaticHelper.RopeTailId;
            SqlDataAdapter adp = new SqlDataAdapter("ViewAssignLooseEquipDetail", sc.con);
            adp.SelectCommand.Parameters.AddWithValue("@id", id);           
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtRope.Text = dt.Rows[0]["Looseequipmenttype"].ToString();              
                txtAssignedLocation.Text = dt.Rows[0]["Location"].ToString();
                txtAssignedRopeWinch.Text = dt.Rows[0]["assignednumber"].ToString();
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
