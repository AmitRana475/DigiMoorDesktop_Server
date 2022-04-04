using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkShipVersionII.Views
{
    /// <summary>
    /// Interaction logic for NotificationCommentsView.xaml
    /// </summary>
    public partial class NotificationCommentsView : UserControl
    {
        public NotificationCommentsView()
        {
            InitializeComponent();
            var dt = StaticHelper.CommentsType;
            if(dt==2)
            {
                canBtn.Visibility = Visibility.Hidden;
            }
            
        }

        private void btnAddComment_Click(object sender, RoutedEventArgs e)
        {
            AddGrpBox.Visibility = Visibility.Visible;
            canBtn.Visibility = Visibility.Hidden;
        }
    }
}
