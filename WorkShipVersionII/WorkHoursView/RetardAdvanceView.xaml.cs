using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WorkShipVersionII.WorkHoursView
{

    public partial class RetardAdvanceView : UserControl
    {
        public RetardAdvanceView()
        {
            InitializeComponent();

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
                        str = s1.Trim() + s2;
                    else
                        str = s1.Trim();

                    var monthid = Convert.ToDateTime(s1 + "-" + cbMonth.SelectedValue + "-" + cbYear.SelectedValue);
                    txtIDLDate.Text = monthid.ToString();
                    txtDateID.Text = str;
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

        //private void CalenderGrid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //CalenderGrid.CurrentCell = new DataGridCellInfo(CalenderGrid.ItemsSource= "10 Wed",);
        //    //CalenderGrid.SelectedCells.Add(CalenderGrid.CurrentCell);

        //    CalenderGrid.SelectedItem = "10 Wed";
        //    CalenderGrid.SelectedValue= "10 Wed";

        //}
    }
}
