using DataBuildingLayer;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{

    public partial class WorkHoursCalenderView : UserControl
    {
        //WebBrowser browser;
        private readonly ShipmentContaxt sc;

        public WorkHoursCalenderView()
        {
            InitializeComponent();

            if (sc == null)
                sc = new ShipmentContaxt();

        }

        private void btnMonthRight_Click(object sender, RoutedEventArgs e)
        {

            if (cbMonth.SelectedIndex < (cbMonth.Items.Count - 1))
            {
                cbMonth.SelectedIndex++;
            }
            else
            {
                cbMonth.SelectedIndex = 0;
                //btnYearNext_Click(sender, e);
            }
        }

        private void btnMonthLeft_Click(object sender, RoutedEventArgs e)
        {
            if (cbMonth.SelectedIndex > 0)
            {
                cbMonth.SelectedIndex--;
            }
            else
            {
                cbMonth.SelectedIndex = cbMonth.Items.Count - 1;
                //btnYearPrev_Click(sender, e);
            }
        }

        private void btnYearRight_Click(object sender, RoutedEventArgs e)
        {
            if (cbYear.SelectedIndex < (cbYear.Items.Count - 1))
            {
                cbYear.SelectedIndex++;

            }
        }

        private void btnYearLeft_Click(object sender, RoutedEventArgs e)
        {
            if (cbYear.SelectedIndex > 0)
            {
                cbYear.SelectedIndex--;

            }
        }

        private void CalenderGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lblLoading.Visibility = Visibility.Visible;
            if (CalenderGrid.SelectedCells.Count > 0)
            {

                //browser = new WebBrowser();
                //browser.Navigate(new Uri("http://somepage.com"));
                //browser.Navigating += new NavigatingCancelEventHandler(browser_Navigating);
                //browser.Navigated += new NavigatedEventHandler(browser_LoadCompleted);
                browser_Navigating();
                browser_LoadCompleted();

            }
        }


        void browser_Navigating()
        {
            try
            {
                lblLoading.Visibility = Visibility.Visible;

                //CellValue is a variable of type string.
                var CellValue = GetSelectedValue(CalenderGrid);

                if (!string.IsNullOrEmpty(CellValue))
                {

                    object obj = CellValue;

                    string s1, s2, str = "";
                    s1 = obj.ToString().Substring(0, 2);
                    s2 = obj.ToString().Substring(obj.ToString().Length - 3);

                    if (s2 == "IDL")
                        str = s1.Trim() + s2;
                    else
                        str = s1.Trim();

                    var monthid = Convert.ToDateTime(s1 + "-" + cbMonth.SelectedItem + "-" + cbYear.SelectedItem);
                    DateTime dates = Convert.ToDateTime(monthid);

                    DateTime oDate = dates;


                    var data1 = sc.CrewDetails.Select(x => new { x.Id, x.ServiceFrom, x.ServiceTo, x.UserName, x.name }).Where(x => x.UserName.ToLower() == txtName.Text.ToLower()).FirstOrDefault();

                    DateTime ServiceTo = Convert.ToDateTime("2014-01-01 00:00:00.000"), ServiceFrom = Convert.ToDateTime("2014-01-01 00:00:00.000");
                    ServiceFrom = data1 != null ? data1.ServiceFrom : ServiceFrom;
                    ServiceTo = data1 != null ? data1.ServiceTo : ServiceTo;


                    var data = sc.Freezes.FirstOrDefault();


                    int dayvalue = data != null ? data.FreezeDays : 30;
                    string FreezeStatus = data != null ? data.FreezeStatus : "Freeze";

                    DateTime ApplyDate = data != null ? data.ApplyDate : DateTime.Now.Date;
                    DateTime ManualFreezDateFrom = data != null ? data.DateFrom : DateTime.Now.Date;
                    DateTime ManualFreezDateTo = data != null ? data.DateTo : DateTime.Now.Date;

                    DateTime dt = DateTime.Now;

                    DateTime startDate = DateTime.Now.AddDays(-730);
                    DateTime endDate = Convert.ToDateTime(dt.AddDays(-dayvalue).ToShortDateString());

                    if ((ServiceFrom.Date <= oDate.Date && ServiceTo.Date >= oDate.Date))
                    {

                        if (FreezeStatus == "Freeze")
                        {
                            if (oDate >= endDate)
                            {

                                if ((oDate >= ManualFreezDateFrom && oDate <= ManualFreezDateTo) && ApplyDate == Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                                {
                                    MessageBox.Show("Sorry! You have Frozen on this Date " + oDate.ToString("dd-MMM-yyyy"), "", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(CellValue))
                                    {
                                        WorkHoursCalenderViewModel.ShowPopupTimeSlot(CellValue);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Sorry! You have Frozen on this Date " + oDate.ToString("dd-MMM-yyyy"), "", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {


                            if (FreezeStatus == "UnFreeze" && ApplyDate == Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                            {
                                if (oDate >= ManualFreezDateFrom && oDate <= ManualFreezDateTo)
                                {
                                    if (!string.IsNullOrEmpty(CellValue))
                                    {
                                        WorkHoursCalenderViewModel.ShowPopupTimeSlot(CellValue);
                                    }
                                }
                                else
                                {

                                    if (oDate >= endDate)
                                    {
                                        if (!string.IsNullOrEmpty(CellValue))
                                        {
                                            WorkHoursCalenderViewModel.ShowPopupTimeSlot(CellValue);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Sorry! You have Frozen on this Date " + oDate.ToString("dd-MMM-yyyy"), "", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Sorry! You have Frozen on this Date " + oDate.ToString("dd-MMM-yyyy"), "", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }


                    }
                    else
                    {
                        MessageBox.Show("Sorry! This Date is not between Service Date From " + ServiceFrom.ToString("dd-MMM-yyyy") + " To " + ServiceTo.ToString("dd-MMM-yyyy"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
        }

        //SECOND EVENT TO FIRE AFTER YOU POST INFORMATION
        void browser_LoadCompleted()
        {

            lblLoading.Visibility = Visibility.Hidden;

        }

        private string GetSelectedValue(DataGrid grid)
        {
            DataGridCellInfo cellInfo = grid.SelectedCells[0];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }





    }


    public class CalenderBackgroundClass
    {
        public static string UserName { get; set; }
        public static string Position { get; set; }
        public static string MonthName { get; set; }
        public static string Years { get; set; }
        public static string Planner { get; set; }
    }
}
