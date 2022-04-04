using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for InputsVesselView.xaml
    /// </summary>
    public partial class InputsVesselView : UserControl
    {
        private readonly ShipmentContaxt sc;
        //string sql = null;  

        public InputsVesselView()
        {
            InitializeComponent();
            I1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            I2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            I3.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));


            I1.Background = new SolidColorBrush(Colors.LightBlue);
            I2.Background = new SolidColorBrush(Colors.LightGray);
            I3.Background = new SolidColorBrush(Colors.LightGray);


            I1.BorderThickness = new Thickness(2);
            //I2.BorderThickness = new Thickness(2);
            //I3.BorderThickness = new Thickness(2);

            I2.BorderThickness = new Thickness(0);
            I3.BorderThickness = new Thickness(0);

            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            EnvironmentTab.Visibility = Visibility.Hidden;
            MooringLineTab.Visibility = Visibility.Hidden;



        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void updatecheck(string tbname, decimal mainValue, int Id)
        {
            try
            {
                string sql = null;
                sql = "UPDATE " + tbname + " SET MainValue = " + mainValue + " Where Id = '" + Id + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, sc.con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                sc.SaveChanges();
                //adapter.UpdateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        // ---------------VesselGridTab-------------//
        /* GeneralGrid */
        private void TextBox_Value_LostFocus(object sender, RoutedEventArgs e)
        {

            TextBox clickedButton = sender as TextBox;
            GeneralP obj = clickedButton.DataContext as GeneralP;
            if (obj != null)
            {
                string mainvalue = string.IsNullOrEmpty(obj.MainValue1.ToString()) == true ? "0" : obj.MainValue1.ToString();
                //string mainvalue = obj.MainValue1.ToString();
                decimal check = Convert.ToDecimal(mainvalue);

                if (check > 999999)
                {
                    // MessageBox.Show("Value Cannot be greater than 999999");

                }
                else
                {
                    int id = obj.Id;
                    string tbname = "tblGeneralP";
                    updatecheck(tbname, check, id);
                }
            }

        }

        /* VisselGrid */
        private void TextBox_Value1_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox clickedButton = sender as TextBox;
            VesselP obj = clickedButton.DataContext as VesselP;
            if (obj != null)
            {

                string mainvalue = string.IsNullOrEmpty(obj.MainValue1.ToString()) == true ? "0" : obj.MainValue1.ToString();
                //string mainvalue = obj.MainValue1.ToString();
                decimal check = Convert.ToDecimal(mainvalue);
                if (obj.Description == "LWL" && check <= 0)
                {
                    MessageBox.Show("Value must be greater than 0");
                    return;
                }
                if (check > 999999)
                {
                    //MessageBox.Show("Value Cannot be greater than 999999");

                }
                else
                {
                    int id = obj.Id;
                    string tbname = "tblVesselP";
                    updatecheck(tbname, check, id);
                }
            }


        }

        /* WindAreasGrid */
        private void TextBox_Value2_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox clickedButton = sender as TextBox;
            WindAreas obj = clickedButton.DataContext as WindAreas;
            if (obj != null)
            {
                string mainvalue = obj.MainValue1.ToString();
                decimal check = Convert.ToDecimal(mainvalue);

                if (check > 999999)
                {
                    //MessageBox.Show("Value Cannot be greater than 999999");

                }
                else
                {
                    int id = obj.Id;
                    string tbname = "tblWindAreas";
                    updatecheck(tbname, check, id);
                }
            }

        }



        //---------------------- EnvironmentTab -------------------//
        private void TextBoxEnvironment_LostFocus(object sender, RoutedEventArgs e)
        {
            //string sql = null;
            TextBox clickedButton = sender as TextBox;
            WindandCurrent obj = clickedButton.DataContext as WindandCurrent;
            if (obj != null)
            {


                // string mainvalue = obj.MainValue.ToString();
                string mainvalue = string.IsNullOrEmpty(obj.MainValue1.ToString()) == true ? "0" : obj.MainValue1.ToString();

                decimal check = Convert.ToDecimal(mainvalue);
                if (obj.Description == "WD" && check <= 0)
                {
                    MessageBox.Show("Value must be greater than 0");
                    return;
                }
                if (check > 999999)
                {
                    // MessageBox.Show("Value Cannot be greater than 999999");

                }
                else
                {
                    int id = obj.Id;
                    string tbname = "tblWindandCurrent";
                    updatecheck(tbname, check, id);
                    // int id = obj.Id;
                    //sql = "UPDATE tblWindandCurrent SET MainValue = '" + mainvalue + "' Where Id = '" + id + "'";
                    //SqlDataAdapter adapter = new SqlDataAdapter(sql, sc.con);
                    //DataTable dt = new DataTable();
                    //adapter.Fill(dt);
                }
            }

        }

        private void TextBoxEnvironment_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {           
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }


        //---------------------- MooringLineTab ----------------------//
        private void TextBox_PreviewTextInput_mooringline(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void Txtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            string sql = null;
            TextBox clickedbutton = sender as TextBox;
            MooringLines obj = clickedbutton.DataContext as MooringLines;
            if (obj != null)
            {
                string xch = obj.Xch1.ToString();
                string ych = obj.Ych1.ToString();
                string zch = obj.Zch1.ToString();
                string xbl = obj.Xbl1.ToString();
                string ybl = obj.Ybl1.ToString();
                string zbl = obj.Zbl1.ToString();
                string l0 = obj.l01.ToString();
                string e1 = obj.E1.ToString();
                string n = obj.n1.ToString();
                string a = obj.a1.ToString();
                string mbscrope = obj.MBSrope1.ToString();
                int? ropeId = obj.RopeId;
                // int id = obj.Id;



                SqlDataAdapter adapter1 = new SqlDataAdapter("select * from tblmooringlines where RopeId=" + ropeId + "", sc.con);
                DataTable dt1 = new DataTable();
                adapter1.Fill(dt1);
                if (dt1.Rows.Count > 0)

                {
                    // sql = "update tblmooringlines set Xch = '" + xch + "',Ych = '" + ych + "',Zch = '" + zch + "',Xbl = '" + xbl + "',Ybl = '" + ybl + "', Zbl = '" + zbl + "', l0 = '" + l0 + "', E = '" + e1 + "', n = '" + n + "', a = '" + a + "',MBSrope = '" + mbscrope + "' where RopeId = '" + ropeId + "'";
                    sql = "UpdatettblMooringLines1";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, sc.con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@Xch", xch);
                    adapter.SelectCommand.Parameters.AddWithValue("@Ych", ych);
                    adapter.SelectCommand.Parameters.AddWithValue("@Zch", zch);
                    adapter.SelectCommand.Parameters.AddWithValue("@Xbl", xbl);
                    adapter.SelectCommand.Parameters.AddWithValue("@Ybl", ybl);
                    adapter.SelectCommand.Parameters.AddWithValue("@Zbl", zbl);
                    adapter.SelectCommand.Parameters.AddWithValue("@l0", l0);
                    adapter.SelectCommand.Parameters.AddWithValue("@E", e1);
                    adapter.SelectCommand.Parameters.AddWithValue("@n", n);
                    adapter.SelectCommand.Parameters.AddWithValue("@a", a);
                    adapter.SelectCommand.Parameters.AddWithValue("@MBSrope", mbscrope);
                    adapter.SelectCommand.Parameters.AddWithValue("@RopeId", ropeId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                }

                else
                {
                    //sql = "Insert Into tblmooringlines (Xch,Ych,Zch,Xbl,Ybl,Zbl,l0,E,n,a,MBSrope,RopeId) Values(" + xch + "," + ych + "," + zch + "," + xbl + "," + ybl + "," + zbl + "," + l0 + "," + e1 + "," + n + "," + a + "," + mbscrope + "," + ropeId + ")";
                    //SqlDataAdapter adapter = new SqlDataAdapter(sql, sc.con);
                     sql = "inserttblmooringlines";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, sc.con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@Xch", xch);
                    adapter.SelectCommand.Parameters.AddWithValue("@Ych", ych);
                    adapter.SelectCommand.Parameters.AddWithValue("@Zch", zch);
                    adapter.SelectCommand.Parameters.AddWithValue("@Xbl", xbl);
                    adapter.SelectCommand.Parameters.AddWithValue("@Ybl", ybl);
                    adapter.SelectCommand.Parameters.AddWithValue("@Zbl", zbl);
                    adapter.SelectCommand.Parameters.AddWithValue("@l0", l0);
                    adapter.SelectCommand.Parameters.AddWithValue("@E", e1);
                    adapter.SelectCommand.Parameters.AddWithValue("@n", n);
                    adapter.SelectCommand.Parameters.AddWithValue("@a", a);
                    adapter.SelectCommand.Parameters.AddWithValue("@MBSrope", mbscrope);
                    adapter.SelectCommand.Parameters.AddWithValue("@RopeId", ropeId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                }
            }


        }





        private void TextBoxEnvironment_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    TextBox txtbx = (TextBox)e.OriginalSource;

            //    int check = Convert.ToInt32(txtbx.Text);

            //    if (check > 999999)
            //    {
            //        //MessageBox.Show("0");
            //    }

            //}
            //catch (Exception ex)
            //{

            //}


        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try

            //{
            //    TextBox txtbx = (TextBox)e.OriginalSource;

            //    int check = Convert.ToInt32(txtbx.Text);

            //    if (check > 999999)
            //    {
            //        MessageBox.Show("");
            //    }

            //}
            //catch (Exception ex)
            //{

            //}
        }
        private void TextBox_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    TextBox txtbx = (TextBox)e.OriginalSource;

            //    int check = Convert.ToInt32(txtbx.Text);

            //    if (check > 999999)
            //    {
            //       // MessageBox.Show("hiii");
            //    }

            //}
            //catch (Exception ex)
            //{

            //}

        }

        private void I1_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.Id = 1;
            I1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            I1.BorderThickness = new Thickness(2);
            //------------------------------------
            I2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            I3.BorderBrush = new SolidColorBrush(Colors.LightGray);

            I3.Background = new SolidColorBrush(Colors.LightGray);
            I2.Background = new SolidColorBrush(Colors.LightGray);
            I1.Background = new SolidColorBrush(Colors.LightBlue);


            VesselGridTab.Visibility = Visibility.Visible;
            EnvironmentTab.Visibility = Visibility.Hidden;
            MooringLineTab.Visibility = Visibility.Hidden;

            //I2.BorderThickness = new Thickness(1);
            //I1.BorderThickness = new Thickness(1);
            //I3.BorderThickness = new Thickness(1);



        }

        private void I2_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.Id = 4;
            I1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            I2.BorderThickness = new Thickness(2);
            //------------------------------------
            I1.BorderBrush = new SolidColorBrush(Colors.LightGray);
            I3.BorderBrush = new SolidColorBrush(Colors.LightGray);


            I3.Background = new SolidColorBrush(Colors.LightGray);
            I1.Background = new SolidColorBrush(Colors.LightGray);
            I2.Background = new SolidColorBrush(Colors.LightBlue);

            VesselGridTab.Visibility = Visibility.Hidden;
            EnvironmentTab.Visibility = Visibility.Visible;
            MooringLineTab.Visibility = Visibility.Hidden;

            //I2.BorderThickness = new Thickness(1);
            //I1.BorderThickness = new Thickness(1);
            //I3.BorderThickness = new Thickness(1);



        }

        private void I3_Click(object sender, RoutedEventArgs e)
        {
            StaticHelper.Id = 5;
            I3.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"));
            I3.BorderThickness = new Thickness(2);
            //------------------------------------
            I2.BorderBrush = new SolidColorBrush(Colors.LightGray);
            I1.BorderBrush = new SolidColorBrush(Colors.LightGray);


            I2.Background = new SolidColorBrush(Colors.LightGray);
            I1.Background = new SolidColorBrush(Colors.LightGray);
            I3.Background = new SolidColorBrush(Colors.LightBlue);

            MooringLineTab.Visibility = Visibility.Visible;
            EnvironmentTab.Visibility = Visibility.Hidden;
            VesselGridTab.Visibility = Visibility.Hidden;

            //I2.BorderThickness = new Thickness(1);
            //I1.BorderThickness = new Thickness(1);
            //I3.BorderThickness = new Thickness(1);

        }
    }
}

