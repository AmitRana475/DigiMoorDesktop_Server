using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Configuration;
using System.Data;
using DataBuildingLayer;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for RopeEndtoEndListView.xaml
    /// </summary>
    public partial class RopeEndtoEndListView : UserControl
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ShipmentContaxt"].ConnectionString);
        public RopeEndtoEndListView()
        {
            InitializeComponent();
            BindEndtoEnd();
        }

        public void BindEndtoEnd()
        {
            try
            {
                //con.Open();
                //SqlCommand cmd = new SqlCommand("GetRopeEndtoEnd", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //da.Fill(ds);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    dataGrid1.ItemsSource = ds.Tables[0].DefaultView;
                //}
                //con.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

       // private void Hyperlink_Click(object sender, RoutedEventArgs e)
       // {
            //AddRopeEndtoEndView form2 = new AddRopeEndtoEndView();
            //form2.Visibility = Visibility.Visible;

           

           // var id1 = (DataRowView)dataGrid1.SelectedItem;

           //int  PK_ID = Convert.ToInt32(id1.Row["Id"].ToString());

            

           // con.Open();
           // SqlCommand cmd = new SqlCommand("GetRopeEndtoEnd", con);
           // cmd.CommandType = CommandType.StoredProcedure;
           // SqlDataAdapter da = new SqlDataAdapter(cmd);
           // DataSet ds = new DataSet();
           // da.Fill(ds);

           // int i = dataGrid1.SelectedIndex;

           // RopeEndtoEndClass rpend = new RopeEndtoEndClass();

           // rpend.AssignedLocation= ds.Tables[0].Rows[i]["AssignedLocation"].ToString();

           // //string veribaslik = ds.Tables[0].Rows[i]["baslik"].ToString();
           // //string veriyazi = ds.Tables[0].Rows[i]["yazi"].ToString();
           // //string veritarih = ds.Tables[0].Rows[i]["tarih"].ToString();
           // //string verimevkii = ds.Tables[0].Rows[i]["mevkii"].ToString();
           // //string verimetrekare = ds.Tables[0].Rows[i]["metrekare"].ToString();
           // //string veritur = ds.Tables[0].Rows[i]["tur"].ToString();
           // //string veriid = ds.Tables[0].Rows[i]["id"].ToString();



           // con.Close();


           // Window win = new Window();
           // AddRopeEndtoEndView eDoc = new AddRopeEndtoEndView(rpend);
           // win.Content = eDoc;
           // win.Title = "User Control1";


           // win.Show();
        //}
    }
}
