using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WorkShipVersionII.ViewsAdministration
{
    /// <summary>
    /// Interaction logic for RenewLicenceView.xaml
    /// </summary>
    public partial class RenewLicenceView : UserControl
    {
        public RenewLicenceView()
        {
            InitializeComponent();
            txSno.Text = StaticHelper.CPU_ProcessorID;


        }

        //private void Txt1_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (this.txt1.Text.Trim().Length == this.txt1.MaxLength)
        //    {
        //        this.txt2.Focus();
        //    }
        //}
        //private void Txt2_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (this.txt2.Text.Trim().Length == this.txt2.MaxLength)
        //    {
        //        this.txt3.Focus();
        //    }
        //}
        //private void Txt3_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (this.txt3.Text.Trim().Length == this.txt3.MaxLength)
        //    {
        //        this.txt4.Focus();
        //    }
        //}
        //private void Txt4_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (this.txt4.Text.Trim().Length == this.txt4.MaxLength)
        //    {
        //        this.txt5.Focus();
        //    }
        //}
        //private void Txt5_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (this.txt5.Text.Trim().Length == this.txt5.MaxLength)
        //    {
        //        this.btnEvalidity.Focus();
        //    }
        //}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtmain.Focus();
           // this.txt1.SelectAll();
        }

        private void txtmain_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
