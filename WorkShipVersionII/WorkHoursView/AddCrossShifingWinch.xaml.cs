using DataBuildingLayer;
using System;
using System.Collections.Generic;
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
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for AddCrossShifingWinch.xaml
    /// </summary>
    public partial class AddCrossShifingWinch : UserControl
    {
        private readonly ShipmentContaxt sc;
        public AddCrossShifingWinch()
        {
            InitializeComponent();
            sc = new ShipmentContaxt();
        }

        private void comboRope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var data = sc.AssignRopetoWinch.Where(x => x.RopeId == AddCrossShiftingWinchViewModel.sropetype.Id).FirstOrDefault();
            txtOutboard.Text = data.Outboard == true ? "A" : "B";
            txtAssignedLocation.Text = data.AssignedLocation;
            //txtOutboardcurrent.Text = data.Outboard == true ? "B" : "A";
            txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == data.WinchId).Select(x => x.AssignedNumber).FirstOrDefault();

            //var data = sc.AssignRopetoWinch.Where(x => x.RopeId == AddCrossShiftingWinchViewModel.sropetype.Id).FirstOrDefault();
            //txtOutboard.Text = data.Outboard == true ? "A" : "B";
            //txtAssignedLocation.Text = data.AssignedLocation;
            //txtOutboardcurrent.Text = data.Outboard == true ? "B" : "A";
            //txtAssignedWinch.Text = sc.MooringWinch.Where(s => s.Id == data.WinchId).Select(x => x.AssignedNumber).FirstOrDefault();
            // RopeEndToEnd.AssignedWinch = 


        }

        private void comboAssrope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = sc.MooringWinch.Where(x => x.Id == AddCrossShiftingWinchViewModel._sropeass.Id).FirstOrDefault();

            txtNewAssignedLocation.Text = data.Location;
        }
    }
}
