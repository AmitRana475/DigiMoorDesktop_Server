using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WorkShipVersionII.ViewsCrewManagement
{
       /// <summary>
       /// Interaction logic for AddCrewDetailView.xaml
       /// </summary>
       public partial class AddCrewDetailView : UserControl
       {
              static DateTime count = Convert.ToDateTime(DateTime.Now.ToShortDateString());
              static DateTime count1 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
              public AddCrewDetailView()
              {
                     InitializeComponent();

              }

              private void UserControl_Loaded(object sender, RoutedEventArgs e)
              {
                     if (lblIDName.Text != "0")
                     {
                            txtUserName.IsEnabled = false;
                     }

                     dpFrom.DisplayDateEnd = DateTime.Now;
                     dpTo.DisplayDateStart = dpFrom.SelectedDate;

              }

              private void dpDOB_CalendarClosed(object sender, RoutedEventArgs e)
              {


                     var dt1 = dpDOB.SelectedDate.Value;
                     var dt22 = dpFrom.SelectedDate.Value.ToShortDateString();
                     var dt2 = Convert.ToDateTime(dt22);
                     Lblmessage.Text = string.Empty;
                     if (CheckDate(dt1, dt2) == false)
                     {
                            Lblmessage.Text = "According to this Date of Birth user may not be young seafarer";
                            MessageBox.Show("According to this Date of Birth user may not be young seafarer.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                            //dpDOB.SelectedDate = DateTime.Now;
                            dpDOB.Focusable = true;
                     }


                     if (Convert.ToDateTime(dt1.ToShortDateString()) > Convert.ToDateTime(dt2.ToShortDateString()))
                     {
                            Lblmessage.Text = "According to this Date of Birth user may not be young seafarer";
                            MessageBox.Show("According to this Date of Birth user may not be young seafarer.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                            //dpDOB.SelectedDate = DateTime.Now;
                            dpDOB.Focusable = true;
                     }


              }

              private bool CheckDate(DateTime d1, DateTime d2)
              {
                     var result = d2.AddYears(-18) < d1;
                     return result;
              }

              private void txtUserName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
              {
                     e.Handled = !IsTextAllowed(e.Text, @"[^a-zA-Z]");
              }
              private static bool IsTextAllowed(string Text, string AllowedRegex)
              {
                     try
                     {
                            var regex = new Regex(AllowedRegex);
                            return !regex.IsMatch(Text);
                     }
                     catch
                     {
                            return true;
                     }
              }

              private void dpFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
              {
                     if (dpFrom.SelectedDate == null)
                            dpFrom.SelectedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); //count;

                     if (dpTo.SelectedDate == null)
                            dpTo.SelectedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());//count;

                     var dt1 = dpFrom.SelectedDate.Value;
                     DateTime dt2 = dpTo.SelectedDate.Value;//count;
                     if (dpTo.SelectedDate != null)
                     {
                            var dt = DateTime.TryParse(dpTo.SelectedDate.Value.ToShortDateString(), out dt2);
                     }

                     DateTime dts1 = Convert.ToDateTime(dt1.ToShortDateString());
                     DateTime dts2 = Convert.ToDateTime(dt2.ToShortDateString());


                     if (dts1 > dts2)
                     {
                            dpFrom.SelectedDate = dt2;
                            MessageBox.Show("Service Term From is not greater than Service Term To", "Add Crew Detail", MessageBoxButton.OK, MessageBoxImage.Warning);


                     }

                    // count = Convert.ToDateTime(dpFrom.SelectedDate);
                     dpTo.DisplayDateStart = dpFrom.SelectedDate;

              }

              private void dpTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
              {

                     if (dpFrom.SelectedDate == null)
                            dpFrom.SelectedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()); //count1;

                     if (dpTo.SelectedDate == null)
                            dpTo.SelectedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());//count1;

                     var dt1 = dpFrom.SelectedDate.Value;
                     DateTime dt2 = dpTo.SelectedDate.Value;

                     //if (dt1 > DateTime.Now)
                     //{
                     //    dpTo.SelectedDate= DateTime.Now;
                     //    MessageBox.Show("Service Term To is not less than Service Term From", "Add Crew Detail", MessageBoxButton.OK, MessageBoxImage.Warning);
                     //}

                     DateTime dts1 = Convert.ToDateTime(dt1.ToShortDateString());
                     DateTime dts2 = Convert.ToDateTime(dt2.ToShortDateString());

                     if (dt2 < dt1)
                     {
                           
                            //dt2 = dpTo.SelectedDate.Value;
                            MessageBox.Show("Service Term To is not less than Service Term From", "Add Crew Detail", MessageBoxButton.OK, MessageBoxImage.Warning);
                            dpTo.SelectedDate = dt1.AddDays(1);
                     }

                     //  count1 = Convert.ToDateTime(dpTo.SelectedDate);


              }
       }
}
