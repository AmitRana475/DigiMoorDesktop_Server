using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace WorkShipVersionII.ViewsCrewManagement
{
    /// <summary>
    /// Interaction logic for ResetPasswordView.xaml
    /// </summary>
    public partial class ResetPasswordView : UserControl
    {
        public ResetPasswordView()
        {
            InitializeComponent();
            lbSuggestion.Visibility = Visibility.Collapsed;
        }

        private void txtFullName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                lbSuggestion.Focus();
            }
        }
        private void lbSuggestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbSuggestion.SelectedIndex > -1)
            {
                if (e.Key == Key.Enter)
                {
                    txtFullName.Text = lbSuggestion.SelectedItem.ToString();
                    lbSuggestion.Visibility = Visibility.Collapsed;
                }


            }
        }

        private void lbSuggestion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbSuggestion.SelectedIndex > -1)
            {

                txtFullName.Text = lbSuggestion.SelectedItem.ToString();
                lbSuggestion.Visibility = Visibility.Collapsed;

            }
        }

        
    }
}
