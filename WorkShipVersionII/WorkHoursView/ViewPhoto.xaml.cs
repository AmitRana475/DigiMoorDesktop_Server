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
using System.Data;
using System.Data.SqlClient;
using DataBuildingLayer;
using System.IO;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.WorkHoursView
{
    /// <summary>
    /// Interaction logic for ViewPhoto.xaml
    /// </summary>
    public partial class ViewPhoto : UserControl
    {
        private readonly ShipmentContaxt sc;
        public ViewPhoto()
        {
            InitializeComponent();
            if (sc == null)
            {
                sc = new ShipmentContaxt();
            }
            viewimage();
        }

        private void viewimage()
        {
            try
            {
                string photo = StaticHelper.Photos;
                int id = StaticHelper.ViewId;
                string tblname = StaticHelper.TbName;

                SqlDataAdapter adp = new SqlDataAdapter("select " + photo + " from "+ tblname + " where id=" + id + "", sc.con);
                DataSet dt = new DataSet();
                adp.Fill(dt);
                if (dt.Tables[0].Rows.Count > 0 && dt.Tables[0].Rows[0][0] != null)
                {

                    Encoding oEnc = Encoding.ASCII;
                   // byte[] photo1 = oEnc.GetBytes(dt.Tables[0].Rows[0][0].ToString());

                    string photo1 = dt.Tables[0].Rows[0][0].ToString();


                    DataTable dataTable = dt.Tables[0];

                                   //foreach (DataRow row in dataTable.Rows)
                                   //{

                                   //Store binary data read from the database in a byte array

                                   // byte[] blob = (byte[])row[0];



                                   //MemoryStream stream = new MemoryStream();
                                   //stream.Write(blob, 0, blob.Length);
                                   //stream.Position = 0;

                                   //System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                                   //BitmapImage bi = new BitmapImage();
                                   //bi.BeginInit();

                                   //MemoryStream ms = new MemoryStream();
                                   //img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                   //ms.Seek(0, SeekOrigin.Begin);
                                   //bi.StreamSource = ms;
                                   //bi.EndInit();


                                   // var uriSource = new Uri(@"C:\DigiMoorDB_Backup\InspectionImages\b3.c7886.jpg", UriKind.Relative);
                                   //foo.Source = new BitmapImage(uriSource);

                                   string ServerName = StaticHelper.ServerName;
                                   string path = ServerName + "\\DigiMoorDB_Backup\\InspectionImages\\" + photo1 + "";
                                   //var path = @"C:\DigiMoorDB_Backup\InspectionImages\"+ photo1 + "";

                                   var uriSource = new Uri(path);
                        image1.Source = new BitmapImage(uriSource);


                        
                        //image1.Source = new BitmapImage(path);

                    //}
                }
               
            }
            catch (Exception ex)
            {
                lbl1.Content = "No Photo !";
                lbl1.Visibility = Visibility.Visible;
            }
              
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ChildWindowManager.Instance.CloseChildWindow();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChildWindowManager.Instance.CloseChildWindow();
        }
    }
}
