using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace WorkShipVersionII.WorkHoursView
{
  
    public partial class OPA90View : UserControl
    {
        TimeSpan current = new TimeSpan(12, 0, 0);
        TimeSpan current1 = new TimeSpan(12, 0, 0);
        public OPA90View()
        {
            InitializeComponent();
            
        }




        private void txtTo_Spin(object sender, SpinEventArgs e)
        {
            ButtonSpinner spinner = (ButtonSpinner)sender;
            TextBox txtBox = (TextBox)spinner.Content;

            string vv = current.ToString().Remove(5, 3).Replace(":", ".").Replace("00", "0");
            decimal cc = Convert.ToDecimal(vv);

            //var value = String.IsNullOrEmpty(txtBox.Text) ? "0:30" : txtBox.Text.ToString();
            if (e.Direction == SpinDirection.Increase)
            {
                if (cc <= 23)
                    current += TimeSpan.FromMinutes(30);
                //else if (cc > 23 && cc < 24)
                //    current += TimeSpan.FromMinutes(30);
            }
            else
            {
                if (cc > 0)
                    current -= TimeSpan.FromMinutes(30);
            }
            txtBox.Text = current.ToString().Remove(5, 3);
            

        }

        private void txtFrom_Spin(object sender, SpinEventArgs e)
        {
            ButtonSpinner spinner = (ButtonSpinner)sender;
            TextBox txtBox = (TextBox)spinner.Content;

            string vv = current1.ToString().Remove(5, 3).Replace(":", ".").Replace("00", "0");
            decimal cc = Convert.ToDecimal(vv);

            if (e.Direction == SpinDirection.Increase)
            {
                if (cc <= 23)
                    current1 += TimeSpan.FromMinutes(30);

            }
            else
            {
                if (cc > 0)
                    current1 -= TimeSpan.FromMinutes(30);
            }
            txtBox.Text = current1.ToString().Remove(5, 3);
        }

        private void CalenderGrid_MouseLeftButtonUp(object sender, EventArgs e)
        {
            if (CalenderGrid.SelectedCells.Count > 0)
            {
                var CellValue = GetSelectedValue(CalenderGrid);
                if (!string.IsNullOrEmpty(CellValue))
                {
                    string s1, s2, str = "";
                    s1 = CellValue.ToString().Substring(0, 2);
                    s2 = CellValue.ToString().Substring(CellValue.ToString().Length - 3);

                    if (s2 == "IDL")
                    {
                        str = s1.Trim() + s2;
                        txtIDL.Text = s2;
                    }
                    else
                    {
                        str = s1.Trim();
                        txtIDL.Text = string.Empty;
                    }

                    var monthid = Convert.ToDateTime(s1 + "-" + cbMonth.SelectedValue + "-" + cbYear.SelectedValue);
                    txtIDLDate.Text = monthid.ToString();
                    txtDateID.Text = str;
                  
                    //((int)monthid.DayOfWeek).ToString();
                }

            }
        }

        private void CalenderGridTo_MouseLeftButtonUp(object sender, EventArgs e)
        {
            if (CalenderGridTo.SelectedCells.Count > 0)
            {
                var CellValue = GetSelectedValue(CalenderGridTo);
                if (!string.IsNullOrEmpty(CellValue))
                {
                    string s1, s2, str = "";
                    s1 = CellValue.ToString().Substring(0, 2);
                    s2 = CellValue.ToString().Substring(CellValue.ToString().Length - 3);

                    if (s2 == "IDL")
                    {
                        str = s1.Trim() + s2;
                        txtIDLTo.Text = s2;
                    }
                    else
                    {
                        str = s1.Trim();
                        txtIDLTo.Text = string.Empty;
                    }

                    var monthid = Convert.ToDateTime(s1 + "-" + cbMonthTo.SelectedValue + "-" + cbYearTo.SelectedValue);
                    txtIDLDateTo.Text = monthid.ToString();
                    txtDateIDTo.Text = str;
                   
                    //((int)monthid.DayOfWeek).ToString();
                }

            }
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
   
}



