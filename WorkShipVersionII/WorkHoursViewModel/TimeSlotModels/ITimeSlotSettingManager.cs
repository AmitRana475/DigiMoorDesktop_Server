using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace WorkShipVersionII.WorkHoursViewModel.TimeSlotModels
{
    public interface ITimeSlotSettingManager
    {
        void TimeSlotPageSettingSaveMethod(DateTime startdate, object enddate, BackgroundWorker Workersave, Grid grid);

        void TimeSlotPageSettingDeleteMethod(DateTime startdate, object enddate, BackgroundWorker Workersave, Grid grid);

    }
}
