using System;
using System.Windows;
using System.Windows.Controls;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for TimeSlotYoungPlannerView.xaml
    /// </summary>
    public partial class TimeSlotYoungPlannerView : UserControl
    {
        TimeSlotYoungPlannerViewModel tsm = new TimeSlotYoungPlannerViewModel();
        public TimeSlotYoungPlannerView()
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

             tsm.CreateTimeSlotYoung_Load(obj, TimeSlotGrid);
        }
    }
   
}
