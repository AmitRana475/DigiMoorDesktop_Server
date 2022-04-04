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
    /// Interaction logic for InputsMooringLinesView.xaml
    /// </summary>
    public partial class InputsMooringLinesView : UserControl
    {
        private readonly ShipmentContaxt sc;
        public InputsMooringLinesView()
        {
            InitializeComponent();

            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
        }
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtbx = (TextBox)e.OriginalSource;

                int check = Convert.ToInt32(txtbx.Text);

                if (check > 999999)
                {
                    MessageBox.Show("");
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }            

        private void Txtbox_LostFocus(object sender, RoutedEventArgs e)
        {            
            string sql = null;
            TextBox clickedbutton = sender as TextBox;
            MooringLines obj = clickedbutton.DataContext as MooringLines;
            string xch = obj.Xch.ToString();
            string ych = obj.Ych.ToString();
            string zch = obj.Zch.ToString();
            string xbl = obj.Xbl.ToString();
            string ybl = obj.Ybl.ToString();
            string zbl = obj.Zbl.ToString();
            string l0 = obj.l0.ToString();
            string e1 = obj.E.ToString();
            string n = obj.n.ToString();
            string a = obj.a.ToString();
            string mbscrope = obj.MBSrope.ToString();
            int? ropeId = obj.RopeId;
            //int id = obj.Id;

            SqlDataAdapter adapter1 = new SqlDataAdapter("select * from tblmooringlines where RopeId=" + ropeId + "", sc.con);            
            DataTable dt1 = new DataTable();
            adapter1.Fill(dt1);           
              if (dt1.Rows.Count > 0)
                                  
              {
                //sql = "update tblmooringlines set Xch = '" + xch + "',Ych = '" + ych + "',Zch = '" + zch + "',Xbl = '" + xbl + "',Ybl = '" + ybl + "', Zbl = '" + zbl + "', l0 = '" + l0 + "', E = '" + e1 + "', n = '" + n + "', a = '" + a + "',MBSrope = '" + mbscrope + "' where RopeId = '" + ropeId + "'";
                //SqlDataAdapter adapter = new SqlDataAdapter(sql, sc.con);
                //DataTable dt = new DataTable();
                //adapter.Fill(dt);

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

               // SqlDataAdapter adapter = new SqlDataAdapter(sql, sc.con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
            }

            

        }
       

    }
}
