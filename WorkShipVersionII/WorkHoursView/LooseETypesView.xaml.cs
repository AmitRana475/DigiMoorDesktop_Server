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
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for LooseETypesView.xaml
    /// </summary>
    public partial class LooseETypesView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public LooseETypesView()
        {
            InitializeComponent();
            sc = new ShipmentContaxt();
            bindLooseEType();

            //StaticHelper.HelpMethod("dd");
        }

        private void bindLooseEType()
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

        private void comboLooseEtype_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string damageOb = comboLooseEtype.Text.ToString();

                SqlDataAdapter adp = new SqlDataAdapter("select id from LooseEType where LooseEquipmentType='" + damageOb + "'", sc.con);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                Int32 id = Convert.ToInt32(dt.Rows[0][0]);

                StaticHelper.RtailLooseEtypeid = id;

                if (damageOb == "Rope Tail" || damageOb == "FireWire" || damageOb == "Messenger Rope" || damageOb == "Rope Stopper" || damageOb == "Towing Rope" || damageOb == "Suez Rope" || damageOb == "Pennant Rope" || damageOb == "Grommet Rope")
                {
                    AddRopeTailViewModel vm = new AddRopeTailViewModel(id);
                    vm.resetform();
                    ChildWindowManager.Instance.ShowChildWindow(new AddRopeTailView() { DataContext = vm });
                }
                else if (damageOb == "Chain Stopper")
                {
                    AddChainStopperViewModel vm = new AddChainStopperViewModel(id);
                    vm.resetform();
                    ChildWindowManager.Instance.ShowChildWindow(new AddChainStopperView() { DataContext = vm });

                }
                else if (damageOb == "Joining Shackle")
                {
                    AddLooseEquipmentViewModel vm = new AddLooseEquipmentViewModel();
                    vm.refreshform();
                    ChildWindowManager.Instance.ShowChildWindow(new AddLooseEquipmentDetailsView() { DataContext = vm });
                }
                else if (damageOb == "Chafe Guard")
                {
                    AddChafeGuardViewModel vm = new AddChafeGuardViewModel();
                    vm.resetform();
                    ChildWindowManager.Instance.ShowChildWindow(new AddChafeGuard() { DataContext = vm });
                }
                else if (damageOb == "Winch Brake Test Kit")
                {
                    WBTestKitViewModel vm = new WBTestKitViewModel();
                    vm.resetform();
                    ChildWindowManager.Instance.ShowChildWindow(new AddWBTestKitView() { DataContext = vm });
                }
            }
            catch { }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.HelpMethod("4.4.2  LOOSE EQUIPMENT DETAILS.htm");
        }
    }
}
