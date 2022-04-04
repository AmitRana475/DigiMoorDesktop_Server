using System.Windows.Controls;

namespace WorkShipVersionII.LoginView
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        //MainWindow main = new MainWindow();
      
        public LoginView()
        {
            InitializeComponent();
        }



        private void Txt1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txt1.Text.Trim().Length == this.txt1.MaxLength)
            {
                this.txt2.Focus();
            }
        }
        private void Txt2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txt2.Text.Trim().Length == this.txt2.MaxLength)
            {
                this.txt3.Focus();
            }
        }
        private void Txt3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txt3.Text.Trim().Length == this.txt3.MaxLength)
            {
                this.txt4.Focus();
            }
        }
        private void Txt4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txt4.Text.Trim().Length == this.txt4.MaxLength)
            {
                this.txt5.Focus();
            }
        }
        private void Txt5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txt5.Text.Trim().Length == this.txt5.MaxLength)
            {
                this.btnEvalidity.Focus();
            }
        }


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //    if (txtUserName.Text.ToLower() == "admin" && txtPassword.Password == "123123")
        //    {

        //        //Thread.Sleep(2000);
        //        lblLoading.Visibility = Visibility.Visible;


        //        new MainViewModelCrewManagement();
        //        main.Show();

        //        LoginWindow login = App.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
        //        if (login != null)
        //        {
        //            login.Close();
        //        }


        //    }

        //}



    }
}
