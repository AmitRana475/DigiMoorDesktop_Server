using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Views
{

    public partial class NotificationView : UserControl
    {
        public NotificationView()
        {
            InitializeComponent();

           
        }

       

        //public void bmsAdd()
        //{
        //    using (ShipmentContaxt sc = new ShipmentContaxt())
        //    {

        //        var bbb = sc.AddGroups.ToList();
        //    }
        //}

        //private void btnArchives_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
            
        //    if (btnArchives.Content.ToString() == "Archives")
        //        btnArchives.Content = "Go Back";
        //    else
        //        btnArchives.Content = "Archives";
        //}

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var svm = new NotificationViewModel("Refresh");
            this.DataContext = svm;
            Binding binding = new Binding("NotificationView") { Source = svm, Mode = BindingMode.TwoWay };

            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(OffNotificationGrid.ItemsSource);
            //view.SortDescriptions.Add(new SortDescription("FullName", ListSortDirection.Ascending));

        }

        //private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    if (chkBox1.IsChecked == true)
        //    {


        //         OffNotificationGrid.Items.OfType<CommentofVSClass>().ToList().ForEach(x => x.IsCheckedV = true);
        //        var bb = OffNotificationGrid.Items.OfType<CommentofVSClass>().ToList();
        //        OffNotificationGrid.ItemsSource = bb.ToList();

        //    }
        //    else
        //    {
        //        OffNotificationGrid.Items.OfType<CommentofVSClass>().ToList().ForEach(x => x.IsCheckedV = false);
        //        var bb = OffNotificationGrid.Items.OfType<CommentofVSClass>().ToList();
        //        OffNotificationGrid.ItemsSource = bb.ToList();
        //    }
        //}
    }
}
