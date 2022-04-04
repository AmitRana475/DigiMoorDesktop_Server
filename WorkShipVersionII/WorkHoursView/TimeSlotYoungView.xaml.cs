using System;
using System.Windows;
using System.Windows.Controls;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for TimeSlotYoungView.xaml
    /// </summary>
    public partial class TimeSlotYoungView : UserControl
    {
        TimeSlotYoungViewModel tsm = new TimeSlotYoungViewModel();
        public TimeSlotYoungView()
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

            tsm.CreateTimeSlotYoung_Load(obj, TimeSlotGrid, TimeSlotGridPlanner);
        }
    }
}
