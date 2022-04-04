using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddMooringWinchDetail.xaml
    /// </summary>
    public partial class AddMooringWinchDetail : UserControl
    {
        private readonly ShipmentContaxt sc;
        public AddMooringWinchDetail()
        {
            InitializeComponent();
            //rana
            sc = new ShipmentContaxt();
        }

        private void txtMBL_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
            //txtMBL.Text = "";

        }

        private void txtMBL_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string akk = txtMBL.Text;
                if (akk != "")
                {
                    bool check = sc.DecimalCheck1(Convert.ToDecimal(txtMBL.Text));
                    if (check == false)
                    {
                        txtMBL.Text = "";
                    }
                }

            }
            catch { }
        }
    }
}
