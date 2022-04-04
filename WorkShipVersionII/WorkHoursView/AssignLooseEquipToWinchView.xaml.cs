using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for AssignLooseEquipToWinchView.xaml
    /// </summary>
    public partial class AssignLooseEquipToWinchView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public AssignLooseEquipToWinchView()
        {
           XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }
        }

        private void comboAssrope_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                var data = sc.MooringWinch.Where(x => x.Id == AssignLooseEquipToWinchViewModel._sropeass.Id && x.IsActive == true).FirstOrDefault();

                txtNewAssignedLocation.Text = data.Location;

                if (comboAssrope.Text != "--Select--")
                {
                    btnSave.IsEnabled = true;
                }
            }
            catch { }

        }

        private void comboRank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
