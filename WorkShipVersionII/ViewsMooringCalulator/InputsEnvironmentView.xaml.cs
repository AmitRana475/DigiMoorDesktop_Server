using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WorkShipVersionII.ViewsMooringCalulator
{
    /// <summary>
    /// Interaction logic for InputsEnvironmentView.xaml
    /// </summary>
    public partial class InputsEnvironmentView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public InputsEnvironmentView()
        {
            InitializeComponent();           
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void TextBox_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;

                int check = Convert.ToInt32(txtbx.Text);

                if (check > 999999)
                {

                   // MessageBox.Show("");

                }

            }
            catch (Exception ex)
            {

            }

        }

        private void TextBox_Value_LostFocus(object sender, RoutedEventArgs e)
        {
            string sql = null;                   
            TextBox clickedButton = sender as TextBox;
            WindandCurrent obj = clickedButton.DataContext as WindandCurrent;
            string mainvalue = obj.MainValue1.ToString();
            int id = obj.Id;
            //sql = "UPDATE tblWindandCurrent SET MainValue = '" + mainvalue + "' Where Id = '" + id + "'";
            sql = "UpdatettblWindandCurrent1";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, sc.con);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MainValue", mainvalue);
            adapter.SelectCommand.Parameters.AddWithValue("@Id", id);
            DataTable dt = new DataTable();
            adapter.Fill(dt);                                   
        }


        
    }
}
