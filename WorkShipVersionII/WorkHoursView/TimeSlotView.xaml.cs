using System;
using System.Windows;
using System.Windows.Controls;

using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    public partial class TimeSlotView : UserControl
    {
        TimeSlotViewModel tsm = new TimeSlotViewModel();
        public TimeSlotView()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //int columns = Convert.ToInt32(48);

            var obj = new TSUserClass()
            {
                UserName = txtUserName.Text,
                FullName = txtFullName.Text,
                Position = txtPosition.Text,
                Department = txtDepartment.Text,
                Dt11 = Convert.ToDateTime(txtDate.Text),
                DateId = txtDateID.Text
            };
            
            tsm.CreateTimeSlot_Load(obj, TimeSlotGrid, TimeSlotGridPlanner);
            //var bb= GetMultipleValue();


        }


        public Tuple<int, int> GetMultipleValue()
        {
            return Tuple.Create(1, 2);
        }

        //private  void CreateTimeSlot(int columns)
        //{
        //    txtColumn.Text = columns.ToString();

        //    this.TimeSlotGrid.Children.Clear();
        //    this.TimeSlotGrid.ColumnDefinitions.Clear();
        //    this.TimeSlotGrid.RowDefinitions.Clear();
        //    //int columns = Convert.ToInt32(txtColumn.Text);
        //    //float lenth;
        //    float lenth = (float)1010 / columns;


        //    Canvas canvas = new Canvas();
        //    canvas.Height = 100;
        //    canvas.Width = 1030;//#ededef
        //    canvas.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ededef"));

        //    TextBlock txtblack;
        //    double width = 6;
        //    double txtcol = columns / 2;
        //    for (int i = 0; i <= txtcol; i++)
        //    {
        //        txtblack = new TextBlock()
        //        {
        //            Name = "TB" + i,
        //            Text = i.ToString()
        //        };

        //        Canvas.SetTop(txtblack, 20);
        //        Canvas.SetLeft(txtblack, width);
        //        canvas.Children.Add(txtblack);

        //        if (i == (txtcol - 1))
        //            width = width + (lenth * 2) - 2;
        //        else if (i > 8)
        //            width = width + (lenth * 2) - 0.5;
        //        else
        //            width = width + (lenth * 2);
        //    }

        //    TextBlock txtblack1;
        //    double width1 = 20;
        //    double txtcol1 = columns / 2;
        //    int con = columns % 2 == 0 ? columns / 2 : (columns / 2) + 1;
        //    for (int i = 0; i < con; i++)
        //    {
        //        txtblack1 = new TextBlock()
        //        {
        //            Name = "TB1" + i,
        //            Text = ((float)i+0.5).ToString(),
        //            FontSize = 11

        //        };

        //        Canvas.SetBottom(txtblack1, 15);
        //        Canvas.SetLeft(txtblack1, width1);
        //        canvas.Children.Add(txtblack1);

        //        if (i == (txtcol1 - 1))
        //            width1 = width1 + (lenth * 2) - 2;
        //        else if (i > 8)
        //            width1 = width1 + (lenth * 2) + 0.5;
        //        else
        //            width1 = width1 + (lenth * 2);
        //    }



        //    Grid DynamicGrid = new Grid();
        //    DynamicGrid.Height = 28;







        //    //var rowDefinition = new RowDefinition();
        //    //rowDefinition.Height = GridLength.Auto;
        //    //grid.RowDefinitions.Add(rowDefinition);


        //    canvas.Children.Add(DynamicGrid);

        //    for (int i = 0; i < columns; i++)
        //    {
        //        var gridcol = new ColumnDefinition()
        //        {
        //            Width = new GridLength(lenth )
        //        };
        //        DynamicGrid.ColumnDefinitions.Add(gridcol);
        //    }

        //    Border pnlTemp;
        //    int count1 = DynamicGrid.ColumnDefinitions.Count;
        //    //for (int i = 0; i < DynamicGrid.RowDefinitions.Count; i++)
        //    //{
        //    for (int j = 0; j < count1; j++)     //it will creates panels dynamically according number of cells of tablelayoutpanel
        //    {
        //        // Border pnlTemp = new Border();
        //        pnlTemp = new Border()
        //        {
        //            BorderThickness = new Thickness()
        //            {
        //                Bottom = 1,
        //                Left = 1,
        //                Right = 0,
        //                Top = 1
        //            },
        //            BorderBrush = new SolidColorBrush(Colors.Gray)
        //        };
        //        pnlTemp.Name = "B" + j;
        //        pnlTemp.AllowDrop = true;
        //        if (j == count1-1)
        //            pnlTemp.BorderThickness = new Thickness(1);

        //        pnlTemp.Background = new SolidColorBrush(Colors.White);


        //        pnlTemp.MouseDown += PnlTemp_MouseDown;
        //        pnlTemp.DragEnter += PnlTemp_DragEnter;

        //        pnlTemp.SetValue(Grid.ColumnProperty, j);
        //        pnlTemp.SetValue(Grid.RowProperty, 0);

        //        DynamicGrid.Children.Add(pnlTemp);
        //    }


        //    Canvas.SetTop(DynamicGrid, 40);
        //    Canvas.SetLeft(DynamicGrid, 10);



        //    TimeSlotGrid.Children.Add(canvas);

        //}



        //private void btnMonthRight_Click(object sender, RoutedEventArgs e)
        //{
        //    txtDate.Text = Convert.ToDateTime(txtDate.Text).AddDays(1).ToString();
        //    CreateTimeSlot(36);
        //}

        //private void btnMonthLeft_Click(object sender, RoutedEventArgs e)
        //{
        //    txtDate.Text = Convert.ToDateTime(txtDate.Text).AddDays(-1).ToString();
        //    CreateTimeSlot(60);
        //}

        //private void PnlTemp_DragEnter(object sender, DragEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

        //private void PnlTemp_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}


    }
}
