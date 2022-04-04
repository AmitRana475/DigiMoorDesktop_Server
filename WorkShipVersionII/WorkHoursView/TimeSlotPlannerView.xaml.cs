using System;
using System.Windows;
using System.Windows.Controls;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for TimeSlotPlannerView.xaml
    /// </summary>
    public partial class TimeSlotPlannerView : UserControl
    {
        TimeSlotPlannerViewModel tsm = new TimeSlotPlannerViewModel();
        public TimeSlotPlannerView()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var obj = new TSUserClass()
            {
                UserName = txtUserName.Text,
                FullName = txtFullName.Text,
                Position = txtPosition.Text,
                Department = txtDepartment.Text,
                Dt11 = Convert.ToDateTime(txtDate.Text),
                DateId = txtDateID.Text
            };

            tsm.CreateTimeSlot_Load(obj, TimeSlotGrid);
        }
    }
}
