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
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Views
{
    /// <summary>
    /// Interaction logic for ViewTrainingAttachment.xaml
    /// </summary>
    public partial class ViewTrainingAttachment : UserControl
    {
        public ViewTrainingAttachment()
        {
            InitializeComponent();

            System.Diagnostics.Process.Start(@"C:\Users\49webstreet\Downloads\aa.mp4");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChildWindowManager.Instance.CloseChildWindow();
        }
    }
}
