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
    /// Interaction logic for ViewLooseEInspectionSetting.xaml
    /// </summary>
    public partial class ViewLooseEInspectionSetting : UserControl
    {
        private readonly ShipmentContaxt sc;
        public ViewLooseEInspectionSetting()
        {
            InitializeComponent();
            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }
            bindLooseEType();

           
        }

        private void bindLooseEType()
        {
            try
            {
                SqlDataAdapter adp1 = new SqlDataAdapter("select * from looseetype where id !=2", sc.con);
                DataSet dt1 = new DataSet();
                adp1.Fill(dt1);
                if (dt1.Tables[0].Rows.Count > 0)
                {

                    comboLooseEtype.ItemsSource = dt1.Tables[0].DefaultView;
                    comboLooseEtype.DisplayMemberPath = dt1.Tables[0].Columns["LooseEquipmentType"].ToString();
                    comboLooseEtype.SelectedValuePath = dt1.Tables[0].Columns["Id"].ToString();

                }
            }
            catch { }
        }

       
        private void bindLooseEInsSetting()
        {
            try
            {
                SqlDataAdapter adp1 = new SqlDataAdapter("select  a.*,b.looseequipmenttype from tblLooseEquipInspectionSetting a inner join looseetype b on a.EquipmentType=b.Id", sc.con);
                DataSet dt1 = new DataSet();
                adp1.Fill(dt1);
                if (dt1.Tables[0].Rows.Count > 0)
                {
                    MooringLooseEInspectionGrid.ItemsSource = dt1.Tables[0].DefaultView;
                }

            }
            catch { }
        }

        private void comboLooseEtype_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (comboLooseEtype.Text != "--Select--")
                {
                   
                    MooringLooseEInspectionGrid.Visibility = Visibility.Visible;

                    var id = comboLooseEtype.SelectedValue;

                    SqlDataAdapter adp1 = new SqlDataAdapter("select  a.*,b.looseequipmenttype from tblLooseEquipInspectionSetting a inner join looseetype b on a.EquipmentType=b.Id where a.EquipmentType=" + id + "", sc.con);
                    DataSet dt1 = new DataSet();
                    adp1.Fill(dt1);
                    if (dt1.Tables[0].Rows.Count > 0)
                    {
                        MooringLooseEInspectionGrid.ItemsSource = dt1.Tables[0].DefaultView;
                    }
                    else
                    {
                        MooringLooseEInspectionGrid.ItemsSource = null;
                    }
                }
            }
            catch { }
        }
    }
}
