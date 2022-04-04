using System.Windows;
using System.Windows.Controls;

namespace WorkShipVersionII.LoginView
{
    /// <summary>
    /// Interaction logic for ShipDetailView.xaml
    /// </summary>
    public partial class ShipDetailView : UserControl
    {
        public ShipDetailView()
        {
            InitializeComponent();

            TxtSerialKey.Text = StaticHelper.CPU_ProcessorID;
        }

        private void BtnCopy_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Clipboard.SetText(TxtSerialKey.Text);
        }
    }
}
